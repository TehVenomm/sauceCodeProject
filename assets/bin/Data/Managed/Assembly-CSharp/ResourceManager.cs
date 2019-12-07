using MsgPack;
using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class ResourceManager : MonoBehaviourSingleton<ResourceManager>
{
	public enum CATEGORY_TYPE
	{
		SINGLE,
		FOLDER,
		PACK,
		HASH256
	}

	public class LoadRequest
	{
		public object master;

		public RESOURCE_CATEGORY category;

		public string packageName;

		public string[] resourceNames;

		public object userData;

		public bool cachePackage;

		public bool enableCache;

		public bool downloadOnly;

		public bool internalMode;

		public bool eventAsset;

		public bool unloadAsset;

		public object progressObject;

		public LoadComplateDelegate onComplate;

		public LoadErrorDelegate onError;

		public LoadComplateDelegate onAtlasComplete;

		public List<LoadRequest> sameRequests;

		public List<LoadRequest> dependencyRequests;

		public Hash128 hash;

		public UIDependency uiDep;

		public void Clear()
		{
			master = null;
			category = RESOURCE_CATEGORY.MAX;
			packageName = null;
			resourceNames = null;
			onComplate = null;
			onError = null;
			userData = null;
			cachePackage = false;
			enableCache = true;
			downloadOnly = false;
			internalMode = false;
			progressObject = null;
			sameRequests = null;
			dependencyRequests = null;
			uiDep = null;
		}

		public void Cancel()
		{
			master = null;
			onComplate = null;
			onError = null;
			progressObject = null;
			if (sameRequests == null)
			{
				userData = null;
				enableCache = false;
				cachePackage = false;
			}
		}

		public void Setup()
		{
			if (category == RESOURCE_CATEGORY.ASSETBUNDLEINFO)
			{
				if (packageName == "EnemyPredownloadTable")
				{
					packageName = "enemypredownloadtable.dat";
				}
				else
				{
					packageName = "assetbundleinfo.dat";
				}
			}
			else if (category != RESOURCE_CATEGORY.MAX)
			{
				packageName = category.ToAssetBundleName(packageName);
			}
			else
			{
				packageName = packageName.ToLower();
			}
		}

		public bool IsValid()
		{
			if (master != null)
			{
				return true;
			}
			if (sameRequests != null)
			{
				int i = 0;
				for (int count = sameRequests.Count; i < count; i++)
				{
					if (sameRequests[i].master != null)
					{
						return true;
					}
				}
			}
			return false;
		}

		public int GetValidCount()
		{
			int num = 0;
			if (master != null)
			{
				num++;
			}
			if (sameRequests != null)
			{
				int i = 0;
				for (int count = sameRequests.Count; i < count; i++)
				{
					if (sameRequests[i].master != null)
					{
						num++;
					}
				}
			}
			return num;
		}

		public float GetProgress()
		{
			if (progressObject is ResourceRequest)
			{
				return (progressObject as ResourceRequest).progress;
			}
			if (progressObject is WWW)
			{
				return (progressObject as WWW).progress;
			}
			if (progressObject == PROGRESS_COMPLATE)
			{
				return 1f;
			}
			return 0f;
		}
	}

	public enum ERROR_CODE
	{
		NOT_FOUND,
		WWW_ERROR
	}

	public delegate void LoadComplateDelegate(LoadRequest request, ResourceObject[] objs);

	public delegate void LoadErrorDelegate(LoadRequest request, ERROR_CODE error_node);

	public static bool isDownloadAssets = false;

	public static bool enableLoadDirect = true;

	private Dictionary<string, string[]> m_cacheABDependencies = new Dictionary<string, string[]>();

	private static object PROGRESS_COMPLATE = 1f;

	public static bool enableCache = true;

	public static bool downloadOnly = false;

	public static bool internalMode = false;

	public static bool autoRetry = false;

	public Func<bool, Error, int> onDownloadErrorQuery;

	public Func<bool> onAsyncLoadQuery;

	public List<LoadRequest> loadRequests = new List<LoadRequest>(64);

	public ResourceCache cache = new ResourceCache();

	private int _manifestVersion = -1;

	private int _assetIndex = 7;

	private int _tableIndex = 1;

	private string baseURL = string.Empty;

	public Action onAddRequest;

	public Action onRemoveRequest;

	private BetterList<LoadRequest> downloadList = new BetterList<LoadRequest>();

	private static int MAX_DL_COUNT = 3;

	private static float WWW_TIME_OUT = 90f;

	private bool isDownloadError;

	public int stayCount;

	public const int DEFAULT_LOADING_ASSET_COUNT_LIMIT = 4;

	public bool isLoadingManifest
	{
		get;
		private set;
	}

	public string downloadURL
	{
		get;
		private set;
	}

	public string downloadEventUrl
	{
		get;
		private set;
	}

	public AssetBundleManifest manifest
	{
		get;
		private set;
	}

	public bool isLoadingSizeInfoManifest
	{
		get;
		private set;
	}

	public string downloadSizeInfoURL
	{
		get;
		private set;
	}

	public AssetBundleManifest sizeInfoManifest
	{
		get;
		private set;
	}

	public AssetBundleManifest event_manifest
	{
		get;
		private set;
	}

	public bool streamingAssetsMode
	{
		get;
		private set;
	}

	public int manifestVersion
	{
		get
		{
			return _manifestVersion;
		}
		set
		{
			if (_manifestVersion != value)
			{
				_manifestVersion = value;
				CrashlyticsReporter.SetManifestVersion(value);
			}
		}
	}

	public int assetIndex
	{
		get
		{
			return _assetIndex;
		}
		set
		{
			if (_assetIndex != value)
			{
				_assetIndex = value;
				CrashlyticsReporter.SetAssetIndex(value);
			}
		}
	}

	public int tableIndex
	{
		get
		{
			return _tableIndex;
		}
		set
		{
			if (_tableIndex != value)
			{
				_tableIndex = value;
			}
		}
	}

	private static string cacheDir => Path.Combine(Application.temporaryCachePath, "assets");

	private static string cachingDir => Caching.currentCacheForWriting.path;

	public bool isAllStay => loadRequests.Count == stayCount;

	public int loadingAssetCountFromAssetBundle
	{
		get;
		private set;
	}

	public int loadingAssetCountLimit
	{
		get;
		set;
	}

	public bool isLoading => loadRequests.Count > 0;

	protected override void Awake()
	{
		base.Awake();
		base.gameObject.AddComponent<GoGameResourceManager>();
		UnityEngine.Object.DontDestroyOnLoad(this);
		onAsyncLoadQuery = OnAsyncLoadQueryDefault;
		loadingAssetCountLimit = 4;
	}

	private bool OnAsyncLoadQueryDefault()
	{
		return false;
	}

	private void Update()
	{
		cache.Update();
	}

	public string GetPlatformName()
	{
		return "and";
	}

	private string GetRelativePath()
	{
		return Application.streamingAssetsPath;
	}

	public void SetURL(string url)
	{
		isDownloadAssets = true;
		baseURL = url;
		UpdateDownloadURL();
	}

	private void UpdateDownloadURL()
	{
		int assetIndex = this.assetIndex;
		downloadURL = $"{baseURL}assets/{assetIndex}/{GetPlatformName()}/";
		downloadSizeInfoURL = $"{baseURL}assets/{assetIndex}/{GetPlatformName()}_info/";
		downloadEventUrl = $"{baseURL}assets/event/{GetPlatformName()}/";
	}

	public void LoadSizeInfoManifest()
	{
		StartCoroutine(DoLoadSizeInfoManifest());
	}

	private IEnumerator DoLoadSizeInfoManifest()
	{
		while (isLoadingSizeInfoManifest || isLoading)
		{
			yield return null;
		}
		isLoadingSizeInfoManifest = true;
		sizeInfoManifest = null;
		if (isDownloadAssets)
		{
			UpdateDownloadURL();
			string url = $"{downloadSizeInfoURL}{GetPlatformName()}_info";
			int retry_count = 0;
			Error error_code;
			do
			{
				error_code = Error.None;
				UnityWebRequest _www = UnityWebRequestAssetBundle.GetAssetBundle(url);
				yield return _www.SendWebRequest();
				string error = _www.error;
				AssetBundle content = DownloadHandlerAssetBundle.GetContent(_www);
				if (!string.IsNullOrEmpty(error))
				{
					error_code = ((!error.Contains("404")) ? Error.AssetLoadFailed : Error.AssetNotFound);
				}
				else if (content != null)
				{
					sizeInfoManifest = content.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
					content.Unload(unloadAllLoadedObjects: false);
				}
				else
				{
					error_code = Error.AssetLoadFailed;
					Log.Error(LOG.RESOURCE, _www.downloadHandler.text);
				}
				_www.Dispose();
				if (error_code == Error.None)
				{
					continue;
				}
				retry_count++;
				if (retry_count >= 3)
				{
					Log.Error(LOG.RESOURCE, error);
					int query_result = 0;
					if (onDownloadErrorQuery != null)
					{
						while (onDownloadErrorQuery != null)
						{
							query_result = onDownloadErrorQuery(arg1: true, error_code);
							if (query_result == 0)
							{
								break;
							}
							yield return null;
						}
						while (query_result == 0 && onDownloadErrorQuery != null)
						{
							query_result = onDownloadErrorQuery(arg1: false, error_code);
							yield return null;
						}
						if (query_result == -1)
						{
							yield break;
						}
					}
					retry_count = 0;
				}
				yield return new WaitForSeconds(1f);
			}
			while (error_code != 0);
		}
		isLoadingSizeInfoManifest = false;
	}

	public void LoadManifest()
	{
		StartCoroutine(DoLoadManifest());
	}

	private IEnumerator DoLoadManifest()
	{
		while (isLoadingManifest || isLoading)
		{
			yield return null;
		}
		isLoadingManifest = true;
		manifest = null;
		if (isDownloadAssets)
		{
			UpdateDownloadURL();
			string url = $"{downloadURL}{GetPlatformName()}_v{manifestVersion}";
			int retry_count = 0;
			Error error_code;
			do
			{
				error_code = Error.None;
				UnityWebRequest _www = UnityWebRequestAssetBundle.GetAssetBundle(url);
				yield return _www.SendWebRequest();
				string error = _www.error;
				AssetBundle content = DownloadHandlerAssetBundle.GetContent(_www);
				if (!string.IsNullOrEmpty(error))
				{
					error_code = ((!error.Contains("404")) ? Error.AssetLoadFailed : Error.AssetNotFound);
				}
				else if (content != null)
				{
					manifest = content.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
					content.Unload(unloadAllLoadedObjects: false);
				}
				else
				{
					error_code = Error.AssetLoadFailed;
					Log.Error(LOG.RESOURCE, _www.downloadHandler.text);
				}
				_www.Dispose();
				if (error_code == Error.None)
				{
					continue;
				}
				retry_count++;
				if (retry_count >= 3)
				{
					Log.Error(LOG.RESOURCE, error);
					int query_result = 0;
					if (onDownloadErrorQuery != null)
					{
						while (onDownloadErrorQuery != null)
						{
							query_result = onDownloadErrorQuery(arg1: true, error_code);
							if (query_result == 0)
							{
								break;
							}
							yield return null;
						}
						while (query_result == 0 && onDownloadErrorQuery != null)
						{
							query_result = onDownloadErrorQuery(arg1: false, error_code);
							yield return null;
						}
						if (query_result == -1)
						{
							yield break;
						}
					}
					retry_count = 0;
				}
				yield return new WaitForSeconds(1f);
			}
			while (error_code != 0);
		}
		yield return StartCoroutine(DoLoadEventManifest());
	}

	private IEnumerator DoLoadEventManifest()
	{
		event_manifest = null;
		if (isDownloadAssets)
		{
			string url = $"{downloadEventUrl}{GetPlatformName()}_v{manifestVersion}";
			int retry_count = 0;
			Error error_code;
			do
			{
				error_code = Error.None;
				UnityWebRequest _www = UnityWebRequestAssetBundle.GetAssetBundle(url);
				yield return _www.SendWebRequest();
				string error = _www.error;
				AssetBundle content = DownloadHandlerAssetBundle.GetContent(_www);
				if (!string.IsNullOrEmpty(error))
				{
					error_code = ((!error.Contains("404")) ? Error.AssetLoadFailed : Error.AssetNotFound);
				}
				else if (content != null)
				{
					event_manifest = content.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
					content.Unload(unloadAllLoadedObjects: false);
				}
				else
				{
					error_code = Error.AssetLoadFailed;
					Log.Error(LOG.RESOURCE, _www.downloadHandler.text);
				}
				_www.Dispose();
				if (error_code == Error.None)
				{
					continue;
				}
				retry_count++;
				if (retry_count >= 3)
				{
					Log.Error(LOG.RESOURCE, error);
					int query_result = 0;
					if (onDownloadErrorQuery != null)
					{
						while (onDownloadErrorQuery != null)
						{
							query_result = onDownloadErrorQuery(arg1: true, error_code);
							if (query_result == 0)
							{
								break;
							}
							yield return null;
						}
						while (query_result == 0 && onDownloadErrorQuery != null)
						{
							query_result = onDownloadErrorQuery(arg1: false, error_code);
							yield return null;
						}
						if (query_result == -1)
						{
							yield break;
						}
					}
					retry_count = 0;
				}
				yield return new WaitForSeconds(1f);
			}
			while (error_code != 0);
		}
		isLoadingManifest = false;
	}

	public void Reset()
	{
		if (isLoading)
		{
			Debug.LogError("isLoading == true");
		}
		CancelAll();
		loadRequests.Clear();
		StopAllCoroutines();
		cache.ClearObjectCaches(clearPreloaded: true);
		cache.ClearPackageCaches();
		cache.ClearSystemPackageCaches();
		cache.ClearSENameDictionary();
		cache.ReleaseAllDelayUnloadAssetBundles();
		cache.shaderCaches.Clear();
		downloadList.Clear();
		m_cacheABDependencies.Clear();
		isDownloadError = false;
	}

	public void Load(bool isEventAsset, object master, RESOURCE_CATEGORY category, string resource_name, LoadComplateDelegate complate_func, LoadErrorDelegate error_func, bool cache_package = false, object userData = null)
	{
		LoadRequest loadRequest = new LoadRequest();
		loadRequest.master = master;
		loadRequest.category = category;
		loadRequest.packageName = resource_name;
		loadRequest.resourceNames = new string[1]
		{
			resource_name
		};
		loadRequest.onComplate = complate_func;
		loadRequest.onError = error_func;
		loadRequest.cachePackage = cache_package;
		loadRequest.userData = userData;
		loadRequest.eventAsset = isEventAsset;
		Load(loadRequest);
	}

	public void Load(object master, RESOURCE_CATEGORY category, string resource_name, LoadComplateDelegate complate_func, LoadErrorDelegate error_func, bool cache_package = false, object userData = null)
	{
		LoadRequest loadRequest = new LoadRequest();
		loadRequest.master = master;
		loadRequest.category = category;
		loadRequest.packageName = resource_name;
		loadRequest.resourceNames = new string[1]
		{
			resource_name
		};
		loadRequest.onComplate = complate_func;
		loadRequest.onError = error_func;
		loadRequest.cachePackage = cache_package;
		loadRequest.userData = userData;
		Load(loadRequest);
	}

	public void Load(bool isEventAsset, object master, RESOURCE_CATEGORY category, string package_name, string[] resource_names, LoadComplateDelegate complate_func, LoadErrorDelegate error_func, bool cache_package = false, object userData = null)
	{
		LoadRequest loadRequest = new LoadRequest();
		loadRequest.master = master;
		loadRequest.category = category;
		loadRequest.packageName = package_name;
		loadRequest.resourceNames = resource_names;
		loadRequest.onComplate = complate_func;
		loadRequest.onError = error_func;
		loadRequest.cachePackage = cache_package;
		loadRequest.userData = userData;
		loadRequest.eventAsset = isEventAsset;
		Load(loadRequest);
	}

	public void Load(object master, RESOURCE_CATEGORY category, string package_name, string[] resource_names, LoadComplateDelegate complate_func, LoadErrorDelegate error_func, bool cache_package = false, object userData = null)
	{
		LoadRequest loadRequest = new LoadRequest();
		loadRequest.master = master;
		loadRequest.category = category;
		loadRequest.packageName = package_name;
		loadRequest.resourceNames = resource_names;
		loadRequest.onComplate = complate_func;
		loadRequest.onError = error_func;
		loadRequest.cachePackage = cache_package;
		loadRequest.userData = userData;
		Load(loadRequest);
	}

	private bool Load(LoadRequest request)
	{
		request.enableCache = enableCache;
		request.downloadOnly = downloadOnly;
		request.internalMode = internalMode;
		if (request.category == RESOURCE_CATEGORY.UI)
		{
			request.internalMode = false;
		}
		List<LoadRequest> list = null;
		AssetBundleManifest x = manifest;
		if (request.category == RESOURCE_CATEGORY.ASSETBUNDLEINFO)
		{
			request.internalMode = false;
			request.downloadOnly = false;
			x = sizeInfoManifest;
		}
		if ((!request.internalMode || request.downloadOnly) && isDownloadAssets && x != null)
		{
			request.Setup();
			if (request.category == RESOURCE_CATEGORY.ASSETBUNDLEINFO)
			{
				request.hash = sizeInfoManifest.GetAssetBundleHash(request.packageName);
			}
			else if (request.category != RESOURCE_CATEGORY.UI)
			{
				if (request.eventAsset)
				{
					Debug.Log("Check hash: " + request.packageName);
					request.hash = event_manifest.GetAssetBundleHash(MonoBehaviourSingleton<GoGameResourceManager>.I.GetFullBundleName(request.packageName));
				}
				else
				{
					request.hash = manifest.GetAssetBundleHash(MonoBehaviourSingleton<GoGameResourceManager>.I.GetFullBundleName(request.packageName));
				}
				if (!request.IsValid())
				{
					return false;
				}
				list = GetManifestDependencyRequests(request);
			}
			else
			{
				request.internalMode = true;
				request.uiDep = GetUIDependency(request);
				if (request.uiDep != null && request.uiDep.atlasPaths != null && !internalMode)
				{
					list = GetUIDependencyRequests(request, request.uiDep);
				}
				request.downloadOnly = false;
			}
			if (list != null)
			{
				List<LoadRequest> list2 = list;
				for (int i = 0; i < list2.Count; i++)
				{
					bool flag = false;
					for (int j = 0; j < loadRequests.Count; j++)
					{
						if (loadRequests[j] == list2[i])
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						AddRequest(list2[i]);
					}
				}
			}
		}
		else if (request.category == RESOURCE_CATEGORY.UI)
		{
			request.uiDep = GetUIDependency(request);
		}
		LoadRequest loadRequest = null;
		int k = 0;
		for (int count = loadRequests.Count; k < count; k++)
		{
			LoadRequest loadRequest2 = loadRequests[k];
			if ((loadRequest2.master == null && loadRequest2.sameRequests == null) || loadRequest2.packageName != request.packageName)
			{
				continue;
			}
			if (loadRequest2.category != request.category)
			{
				loadRequest = loadRequest2;
				continue;
			}
			if (loadRequest2.resourceNames != null && request.resourceNames != null)
			{
				if (loadRequest2.resourceNames.Length != request.resourceNames.Length)
				{
					loadRequest = loadRequest2;
					continue;
				}
				int l = 0;
				int num;
				for (num = loadRequest2.resourceNames.Length; l < num && !(loadRequest2.resourceNames[l] != request.resourceNames[l]); l++)
				{
				}
				if (l != num)
				{
					loadRequest = loadRequest2;
					continue;
				}
			}
			if (loadRequest2.sameRequests == null)
			{
				loadRequest2.sameRequests = new List<LoadRequest>();
			}
			loadRequest2.sameRequests.Add(request);
			return true;
		}
		if (loadRequest != null)
		{
			if (list == null)
			{
				list = new List<LoadRequest>();
			}
			loadRequest.cachePackage = true;
			list.Add(loadRequest);
		}
		request.dependencyRequests = list;
		AddRequest(request);
		return true;
	}

	public void LoadAssetBundle(bool isEventAsset, object master, RESOURCE_CATEGORY category, string resource_name, LoadComplateDelegate complate_func, LoadErrorDelegate error_func, bool cache_package = false, bool unload_Asset = false, object userData = null)
	{
		LoadRequest loadRequest = new LoadRequest();
		loadRequest.master = master;
		loadRequest.category = category;
		loadRequest.packageName = resource_name;
		loadRequest.resourceNames = new string[1]
		{
			resource_name
		};
		loadRequest.onComplate = complate_func;
		loadRequest.onError = error_func;
		loadRequest.cachePackage = cache_package;
		loadRequest.userData = userData;
		loadRequest.eventAsset = isEventAsset;
		loadRequest.unloadAsset = unload_Asset;
		LoadAssetBundle(loadRequest);
	}

	public void LoadAssetBundle(object master, RESOURCE_CATEGORY category, string resource_name, LoadComplateDelegate complate_func, LoadErrorDelegate error_func, bool cache_package = false, bool unload_Asset = false, object userData = null)
	{
		LoadRequest loadRequest = new LoadRequest();
		loadRequest.master = master;
		loadRequest.category = category;
		loadRequest.packageName = resource_name;
		loadRequest.resourceNames = new string[1]
		{
			resource_name
		};
		loadRequest.onComplate = complate_func;
		loadRequest.onError = error_func;
		loadRequest.cachePackage = cache_package;
		loadRequest.userData = userData;
		loadRequest.unloadAsset = unload_Asset;
		LoadAssetBundle(loadRequest);
	}

	public void LoadAssetBundle(bool isEventAsset, object master, RESOURCE_CATEGORY category, string package_name, string[] resource_names, LoadComplateDelegate complate_func, LoadErrorDelegate error_func, bool cache_package = false, bool unload_Asset = false, object userData = null)
	{
		LoadRequest loadRequest = new LoadRequest();
		loadRequest.master = master;
		loadRequest.category = category;
		loadRequest.packageName = package_name;
		loadRequest.resourceNames = resource_names;
		loadRequest.onComplate = complate_func;
		loadRequest.onError = error_func;
		loadRequest.cachePackage = cache_package;
		loadRequest.userData = userData;
		loadRequest.eventAsset = isEventAsset;
		loadRequest.unloadAsset = unload_Asset;
		LoadAssetBundle(loadRequest);
	}

	public void LoadAssetBundle(object master, RESOURCE_CATEGORY category, string package_name, string[] resource_names, LoadComplateDelegate complate_func, LoadErrorDelegate error_func, bool cache_package = false, object userData = null)
	{
		LoadRequest loadRequest = new LoadRequest();
		loadRequest.master = master;
		loadRequest.category = category;
		loadRequest.packageName = package_name;
		loadRequest.resourceNames = resource_names;
		loadRequest.onComplate = complate_func;
		loadRequest.onError = error_func;
		loadRequest.cachePackage = cache_package;
		loadRequest.userData = userData;
		LoadAssetBundle(loadRequest);
	}

	private void LoadAssetBundle(LoadRequest request)
	{
		request.enableCache = enableCache;
		request.downloadOnly = downloadOnly;
		request.internalMode = internalMode;
		if (request.category == RESOURCE_CATEGORY.UI)
		{
			request.internalMode = false;
		}
		List<LoadRequest> list = null;
		AssetBundleManifest x = manifest;
		if (request.category == RESOURCE_CATEGORY.ASSETBUNDLEINFO)
		{
			request.internalMode = false;
			request.downloadOnly = false;
			x = sizeInfoManifest;
		}
		if ((!request.internalMode || request.downloadOnly) && x != null)
		{
			request.Setup();
			if (request.category == RESOURCE_CATEGORY.ASSETBUNDLEINFO)
			{
				request.hash = sizeInfoManifest.GetAssetBundleHash(request.packageName);
			}
			else if (request.category != RESOURCE_CATEGORY.UI)
			{
				if (request.eventAsset)
				{
					request.hash = event_manifest.GetAssetBundleHash(MonoBehaviourSingleton<GoGameResourceManager>.I.GetFullBundleName(request.packageName));
				}
				else
				{
					request.hash = manifest.GetAssetBundleHash(MonoBehaviourSingleton<GoGameResourceManager>.I.GetFullBundleName(request.packageName));
				}
				list = GetManifestDependencyRequests(request);
			}
			else
			{
				request.internalMode = true;
				request.uiDep = GetUIDependency(request);
				if (request.uiDep != null && request.uiDep.atlasPaths != null && !internalMode)
				{
					list = GetUIDependencyRequests(request, request.uiDep);
				}
				request.downloadOnly = false;
			}
			if (list != null)
			{
				List<LoadRequest> list2 = list;
				for (int i = 0; i < list2.Count; i++)
				{
					bool flag = false;
					for (int j = 0; j < loadRequests.Count; j++)
					{
						if (loadRequests[j] == list2[i])
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						AddAssetBundleRequest(list2[i]);
					}
				}
			}
		}
		else if (request.category == RESOURCE_CATEGORY.UI)
		{
			request.uiDep = GetUIDependency(request);
		}
		LoadRequest loadRequest = null;
		int k = 0;
		for (int count = loadRequests.Count; k < count; k++)
		{
			LoadRequest loadRequest2 = loadRequests[k];
			if ((loadRequest2.master == null && loadRequest2.sameRequests == null) || loadRequest2.packageName != request.packageName)
			{
				continue;
			}
			if (loadRequest2.category != request.category)
			{
				loadRequest = loadRequest2;
				continue;
			}
			if (loadRequest2.resourceNames != null && request.resourceNames != null)
			{
				if (loadRequest2.resourceNames.Length != request.resourceNames.Length)
				{
					loadRequest = loadRequest2;
					continue;
				}
				int l = 0;
				int num;
				for (num = loadRequest2.resourceNames.Length; l < num && !(loadRequest2.resourceNames[l] != request.resourceNames[l]); l++)
				{
				}
				if (l != num)
				{
					loadRequest = loadRequest2;
					continue;
				}
			}
			if (loadRequest2.sameRequests == null)
			{
				loadRequest2.sameRequests = new List<LoadRequest>();
			}
			loadRequest2.sameRequests.Add(request);
			if (request.category == RESOURCE_CATEGORY.UI)
			{
				Debug.Log("Same Request: " + request.packageName);
			}
			return;
		}
		if (loadRequest != null)
		{
			if (list == null)
			{
				list = new List<LoadRequest>();
			}
			loadRequest.cachePackage = true;
			list.Add(loadRequest);
		}
		request.dependencyRequests = list;
		if (request.hash.isValid)
		{
			AddAssetBundleRequest(request);
		}
		else
		{
			AddRequest(request);
		}
	}

	private UIDependency GetUIDependency(LoadRequest request)
	{
		string str = Path.GetFileNameWithoutExtension(request.packageName).ToLower();
		TextAsset textAsset = Resources.Load("InternalUI/Deps/" + str) as TextAsset;
		if (textAsset != null)
		{
			return new ObjectPacker().Unpack<UIDependency>(textAsset.bytes);
		}
		return null;
	}

	private List<LoadRequest> GetManifestDependencyRequests(LoadRequest request)
	{
		string fullBundleName = MonoBehaviourSingleton<GoGameResourceManager>.I.GetFullBundleName(request.packageName);
		string[] array;
		if (m_cacheABDependencies.ContainsKey(fullBundleName))
		{
			array = m_cacheABDependencies[fullBundleName];
		}
		else
		{
			array = manifest.GetAllDependencies(fullBundleName);
			m_cacheABDependencies.Add(fullBundleName, array);
		}
		List<LoadRequest> list = null;
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			string bundleNameWithoutVariant = MonoBehaviourSingleton<GoGameResourceManager>.I.GetBundleNameWithoutVariant(array[i]);
			if (bundleNameWithoutVariant == "shader" + GoGameResourceManager.GetDefaultAssetBundleExtension() || bundleNameWithoutVariant == "ui_font" + GoGameResourceManager.GetDefaultAssetBundleExtension() || cache.GetCachedPackage(bundleNameWithoutVariant) != null)
			{
				continue;
			}
			if (list == null)
			{
				list = new List<LoadRequest>();
			}
			LoadRequest loadRequest = null;
			int j = 0;
			for (int count = loadRequests.Count; j < count; j++)
			{
				if (loadRequests[j].packageName == bundleNameWithoutVariant)
				{
					loadRequest = loadRequests[j];
					break;
				}
			}
			if (loadRequest == null)
			{
				loadRequest = new LoadRequest();
				loadRequest.master = request.master;
				loadRequest.category = RESOURCE_CATEGORY.MAX;
				loadRequest.packageName = array[i];
				loadRequest.resourceNames = null;
				loadRequest.onComplate = null;
				loadRequest.onError = null;
				loadRequest.cachePackage = true;
				loadRequest.userData = null;
				loadRequest.enableCache = true;
				loadRequest.hash = manifest.GetAssetBundleHash(MonoBehaviourSingleton<GoGameResourceManager>.I.GetFullBundleName(bundleNameWithoutVariant));
				loadRequest.downloadOnly = downloadOnly;
			}
			else
			{
				loadRequest.cachePackage = true;
			}
			list.Add(loadRequest);
		}
		return list;
	}

	private List<LoadRequest> GetUIDependencyRequests(LoadRequest request, UIDependency dep)
	{
		List<LoadRequest> list = null;
		for (int i = 0; i < dep.atlasPaths.Length; i++)
		{
			string atlasPath = dep.atlasPaths[i];
			string atlasName = UIDependency.GetAtlasName(atlasPath);
			string text = RESOURCE_CATEGORY.UI_ATLAS.ToAssetBundleName(atlasName);
			string resourceName = UIDependency.GetAtlasName(atlasPath) + "_Bundle";
			PackageObject cachedPackage = cache.GetCachedPackage(text);
			if (cachedPackage != null)
			{
				LinkAtlas(cachedPackage, atlasPath, resourceName);
				continue;
			}
			LoadRequest loadRequest = null;
			int j = 0;
			for (int count = loadRequests.Count; j < count; j++)
			{
				if (loadRequests[j].packageName == text)
				{
					loadRequest = loadRequests[j];
					break;
				}
			}
			if (loadRequest == null)
			{
				loadRequest = new LoadRequest();
				loadRequest.master = request.master;
				loadRequest.category = RESOURCE_CATEGORY.UI_ATLAS;
				loadRequest.packageName = text;
				loadRequest.resourceNames = new string[1]
				{
					resourceName
				};
				loadRequest.onComplate = null;
				if (!request.downloadOnly)
				{
					loadRequest.onAtlasComplete = delegate(LoadRequest req, ResourceObject[] objs)
					{
						if (objs != null && objs.Length >= 1)
						{
							LinkAtlas(objs[0].package, atlasPath, resourceName);
						}
					};
				}
				loadRequest.onError = null;
				loadRequest.cachePackage = true;
				loadRequest.userData = null;
				loadRequest.enableCache = true;
				loadRequest.hash = manifest.GetAssetBundleHash(MonoBehaviourSingleton<GoGameResourceManager>.I.GetFullBundleName(text));
				loadRequest.downloadOnly = downloadOnly;
			}
			if (list == null)
			{
				list = new List<LoadRequest>();
			}
			list.Add(loadRequest);
		}
		return list;
	}

	private void LinkAtlas(PackageObject package, string atlasPath, string resourceName)
	{
		UIAtlas uIAtlas = package.hostAtlas = (Resources.Load(atlasPath) as GameObject).GetComponent<UIAtlas>();
		AssetBundle assetBundle = package.obj as AssetBundle;
		if (assetBundle != null)
		{
			UIAtlas uIAtlas2 = uIAtlas.replacement = assetBundle.LoadAsset<GameObject>(resourceName).GetComponent<UIAtlas>();
		}
	}

	private void AddRequest(LoadRequest request)
	{
		loadRequests.Add(request);
		if (onAddRequest != null)
		{
			onAddRequest();
		}
		StartCoroutine(DoLoad(request));
	}

	private bool CheckPackageDownloading(LoadRequest request)
	{
		if (request == null)
		{
			return false;
		}
		if (string.IsNullOrEmpty(request.packageName))
		{
			return false;
		}
		int i = 0;
		for (int size = downloadList.size; i < size; i++)
		{
			if (!string.IsNullOrEmpty(downloadList[i].packageName) && downloadList[i].packageName == request.packageName)
			{
				return true;
			}
		}
		return false;
	}

	private IEnumerator DoLoad(LoadRequest request)
	{
		if (MonoBehaviourSingleton<AppMain>.I.isExecutingClearMemory)
		{
			stayCount++;
			while (MonoBehaviourSingleton<AppMain>.I.isExecutingClearMemory)
			{
				if (!request.IsValid())
				{
					stayCount--;
					BreakRequest(ref request);
					yield break;
				}
				yield return null;
			}
			stayCount--;
		}
		if (request.dependencyRequests != null)
		{
			int m = 0;
			int n = request.dependencyRequests.Count;
			while (true)
			{
				if (!request.IsValid())
				{
					BreakRequest(ref request);
					yield break;
				}
				bool flag = false;
				LoadRequest loadRequest = request.dependencyRequests[m];
				int num = 0;
				for (int count = loadRequests.Count; num < count; num++)
				{
					if (loadRequests[num] == loadRequest)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					yield return null;
					continue;
				}
				m++;
				if (m == n)
				{
					break;
				}
			}
		}
		BetterList<PackageObject> dependency_packages = cache.GetDependencyPackages(request.dependencyRequests);
		if (!request.internalMode)
		{
			while (isDownloadError && !request.internalMode)
			{
				yield return null;
				if (!request.IsValid())
				{
					BreakRequest(ref request);
					yield break;
				}
			}
		}
		AssetBundle asset_bundle = null;
		ResourceObject[] load_objects = null;
		bool use_package_cache = false;
		bool use_package_cache_delay = false;
		bool[] use_object_caches = null;
		int load_num = (request.resourceNames != null) ? request.resourceNames.Length : 0;
		int loaded_num = 0;
		bool package_only = load_num == 0;
		if (request.downloadOnly)
		{
			package_only = true;
		}
		if (load_num > 0)
		{
			load_objects = new ResourceObject[load_num];
			use_object_caches = new bool[load_num];
		}
		StringKeyTable<ResourceObject> object_cache_category = (request.category != RESOURCE_CATEGORY.MAX) ? cache.objectCaches[(int)request.category] : null;
		PackageObject load_package = cache.GetCachedPackage(request.packageName);
		if (load_package != null)
		{
			asset_bundle = (load_package.obj as AssetBundle);
			use_package_cache = true;
		}
		if (asset_bundle == null)
		{
			load_package = null;
		}
		if (loaded_num < load_num && cache.objectCaches != null && cache.systemCaches != null && object_cache_category != null)
		{
			int num2 = 0;
			for (int num3 = load_num; num2 < num3; num2++)
			{
				if (load_objects[num2] == null)
				{
					string res_name = request.resourceNames[num2];
					load_objects[num2] = object_cache_category.Get(res_name);
					if (load_objects[num2] == null)
					{
						load_objects[num2] = cache.systemCaches.Find((ResourceObject o) => o.obj.name == res_name);
					}
					if (load_objects[num2] != null)
					{
						use_object_caches[num2] = true;
						int num4 = loaded_num + 1;
						loaded_num = num4;
					}
				}
			}
		}
		if (loaded_num < load_num && !request.downloadOnly && load_package != null && asset_bundle != null)
		{
			int n = 0;
			int m = load_num;
			while (n < m && (request.master != null || request.sameRequests != null) && !(asset_bundle == null))
			{
				int num4;
				if (load_objects[n] == null)
				{
					if (onAsyncLoadQuery())
					{
						while (IsWaitAssetBundleLoadAsync())
						{
							yield return null;
						}
						loadingAssetCountFromAssetBundle++;
						AssetBundleRequest asset_bundle_load_asset2 = asset_bundle.LoadAssetAsync(request.resourceNames[n]);
						while (!asset_bundle_load_asset2.isDone)
						{
							yield return null;
						}
						load_objects[n] = ResourceObject.Get(request.category, request.resourceNames[n], asset_bundle_load_asset2.asset);
						loadingAssetCountFromAssetBundle--;
					}
					else
					{
						load_objects[n] = ResourceObject.Get(request.category, request.resourceNames[n], asset_bundle.LoadAsset(request.resourceNames[n]));
					}
					if (load_objects[n] != null)
					{
						num4 = loaded_num + 1;
						loaded_num = num4;
					}
				}
				num4 = n + 1;
				n = num4;
			}
		}
		if (load_package == null && (loaded_num < load_num || package_only))
		{
			bool flag2 = (request.category != RESOURCE_CATEGORY.ASSETBUNDLEINFO) ? (manifest != null) : (sizeInfoManifest != null);
			if (request.category == RESOURCE_CATEGORY.MAX || (isDownloadAssets && flag2 && (!request.internalMode || request.downloadOnly)))
			{
				while (downloadList.size >= MAX_DL_COUNT || CheckPackageDownloading(request))
				{
					yield return null;
					if (!request.IsValid())
					{
						BreakRequest(ref request);
						yield break;
					}
				}
				downloadList.Add(request);
				int m = 0;
				bool is_retry;
				do
				{
					is_retry = false;
					if (!request.IsValid())
					{
						BreakRequest(ref request);
						yield break;
					}
					if (!request.downloadOnly)
					{
						load_package = cache.GetCachedPackage(request.packageName);
						if (load_package != null)
						{
							asset_bundle = (load_package.obj as AssetBundle);
							use_package_cache = true;
						}
					}
					if (load_package == null && !(asset_bundle == null))
					{
						continue;
					}
					while (isDownloadError && !request.internalMode)
					{
						yield return null;
						if (!request.IsValid())
						{
							BreakRequest(ref request);
							yield break;
						}
					}
					RESOURCE_CATEGORY? category = MonoBehaviourSingleton<GoGameResourceManager>.I.GetCategory(request.packageName);
					string url;
					if (request.eventAsset)
					{
						url = downloadEventUrl + request.packageName + "?v=" + request.hash.ToString();
					}
					else
					{
						string variantName = MonoBehaviourSingleton<GoGameResourceManager>.I.GetVariantName((!category.HasValue) ? RESOURCE_CATEGORY.MAX : category.Value);
						url = ((!request.packageName.Contains("-sd")) ? (downloadURL + request.packageName + variantName + "?v=" + request.hash.ToString()) : (downloadURL + request.packageName + "?v=" + request.hash.ToString()));
					}
					if (request.category == RESOURCE_CATEGORY.ASSETBUNDLEINFO)
					{
						url = downloadSizeInfoURL + request.packageName + "?v=" + request.hash.ToString();
					}
					string filename = Path.GetFileName(request.packageName);
					string uri = url;
					if (!request.IsValid())
					{
						continue;
					}
					bool num5 = IsVersionCached(filename, request.hash);
					UnityWebRequest www = null;
					Error error_code = Error.None;
					AssetBundle loaded_assetBundle = null;
					if (!num5)
					{
						www = UnityWebRequest.Get(uri);
						www.SendWebRequest();
						request.progressObject = www;
						float timeOut = WWW_TIME_OUT;
						float currentProgress = www.downloadProgress;
						while (!www.isDone && timeOut > 0f)
						{
							yield return null;
							if (currentProgress == www.downloadProgress)
							{
								timeOut -= Time.deltaTime;
							}
							else
							{
								currentProgress = www.downloadProgress;
								timeOut = WWW_TIME_OUT;
							}
							if (!request.IsValid())
							{
								www.Dispose();
								BreakRequest(ref request);
								yield break;
							}
						}
						if (!www.isDone)
						{
							error_code = Error.AssetLoadFailed;
						}
						else if (www.error != null)
						{
							error_code = ((!www.error.Contains("404")) ? Error.AssetLoadFailed : Error.AssetNotFound);
						}
						else
						{
							if (!request.downloadOnly)
							{
								CrashlyticsReporter.SetLoadingBundle(url);
								loaded_assetBundle = cache.PopDelayUnloadAssetBundle(request.packageName);
								if (loaded_assetBundle == null)
								{
									load_package = cache.GetCachedPackage(request.packageName);
									if (load_package != null)
									{
										loaded_assetBundle = (load_package.obj as AssetBundle);
										asset_bundle = loaded_assetBundle;
										use_package_cache = true;
										use_package_cache_delay = true;
									}
								}
								if (loaded_assetBundle == null)
								{
									AssetBundleCreateRequest loadMem2 = AssetBundle.LoadFromMemoryAsync(www.downloadHandler.data);
									yield return loadMem2;
									loaded_assetBundle = loadMem2.assetBundle;
								}
								CrashlyticsReporter.SetLoadingBundle("");
							}
							byte[] data = www.downloadHandler.data;
							www.Dispose();
							error_code = SaveAssetBundle(filename, request.hash, data);
						}
					}
					else if (!request.downloadOnly)
					{
						string path = GetCachePath(filename, request.hash);
						CrashlyticsReporter.SetLoadingBundle(url);
						loaded_assetBundle = cache.PopDelayUnloadAssetBundle(request.packageName);
						if (loaded_assetBundle == null)
						{
							load_package = cache.GetCachedPackage(request.packageName);
							if (load_package != null)
							{
								loaded_assetBundle = (load_package.obj as AssetBundle);
								asset_bundle = loaded_assetBundle;
								use_package_cache = true;
								use_package_cache_delay = true;
							}
						}
						if (loaded_assetBundle == null)
						{
							if (onAsyncLoadQuery())
							{
								AssetBundleCreateRequest loadMem2 = AssetBundle.LoadFromFileAsync(path);
								yield return loadMem2;
								loaded_assetBundle = loadMem2.assetBundle;
							}
							else
							{
								loaded_assetBundle = AssetBundle.LoadFromFile(path);
							}
						}
						CrashlyticsReporter.SetLoadingBundle("");
						if (loaded_assetBundle == null)
						{
							Log.Warning(LOG.RESOURCE, "cached file load failed: {0}", filename);
							error_code = Error.AssetLoadFailed;
							if (File.Exists(path))
							{
								File.Delete(path);
							}
						}
						if (onAsyncLoadQuery())
						{
							yield return null;
						}
					}
					if (request.progressObject != null)
					{
						request.progressObject = PROGRESS_COMPLATE;
					}
					while (isDownloadError && !request.internalMode)
					{
						yield return null;
						if (!request.IsValid())
						{
							cache.AddDelayUnloadAssetBundle(request.packageName, ref loaded_assetBundle);
							www.Dispose();
							BreakRequest(ref request);
							yield break;
						}
					}
					if (error_code == Error.None)
					{
						if (!request.downloadOnly && (request.cachePackage || !package_only || request.category == RESOURCE_CATEGORY.MAX))
						{
							load_package = PackageObject.Get(request.packageName, loaded_assetBundle);
							asset_bundle = (load_package.obj as AssetBundle);
						}
						www?.Dispose();
						if (!use_package_cache_delay)
						{
							use_package_cache = false;
						}
						continue;
					}
					load_package = cache.GetCachedPackage(request.packageName);
					if (load_package != null)
					{
						asset_bundle = (load_package.obj as AssetBundle);
						use_package_cache = true;
					}
					if (load_package != null || asset_bundle == null)
					{
						m++;
						if (autoRetry)
						{
							m = 0;
						}
						if (m < 3)
						{
							yield return new WaitForSeconds(1f);
							is_retry = true;
						}
						else
						{
							if (error_code == Error.AssetLoadFailed)
							{
								Log.Error(LOG.RESOURCE, "{0}:{1}", error_code.ToString(), url);
							}
							else
							{
								Log.Error("{0}:{1}:{2}:{3}", error_code.ToString(), request.category, request.packageName, url);
							}
							if (isDownloadError)
							{
								while (isDownloadError)
								{
									if (!request.IsValid())
									{
										BreakRequest(ref request);
										yield break;
									}
								}
								m = 0;
								is_retry = true;
							}
							else
							{
								isDownloadError = true;
								int n = 0;
								if (request.IsValid() && onDownloadErrorQuery != null)
								{
									while (onDownloadErrorQuery != null)
									{
										n = onDownloadErrorQuery(arg1: true, error_code);
										if (n == 0)
										{
											break;
										}
										yield return null;
									}
									while (n == 0 && onDownloadErrorQuery != null)
									{
										n = onDownloadErrorQuery(arg1: false, error_code);
										yield return null;
									}
									if (n == -1)
									{
										cache.AddDelayUnloadAssetBundle(request.packageName, ref loaded_assetBundle);
										BreakRequest(ref request);
										yield break;
									}
								}
								isDownloadError = false;
								if (n != 1)
								{
									if (request.onError != null)
									{
										request.onError(request, ERROR_CODE.WWW_ERROR);
									}
								}
								else
								{
									m = 0;
									is_retry = true;
								}
							}
						}
					}
					www?.Dispose();
				}
				while (is_retry);
				if (asset_bundle != null)
				{
					int n = 0;
					int j = load_num;
					while (n < j && (request.master != null || request.sameRequests != null) && !(asset_bundle == null))
					{
						int num4;
						if (load_objects[n] == null)
						{
							if (onAsyncLoadQuery())
							{
								while (IsWaitAssetBundleLoadAsync())
								{
									yield return null;
								}
								loadingAssetCountFromAssetBundle++;
								AssetBundleRequest asset_bundle_load_asset2 = asset_bundle.LoadAssetAsync(request.resourceNames[n]);
								asset_bundle.GetAllAssetNames();
								while (!asset_bundle_load_asset2.isDone)
								{
									yield return null;
								}
								load_objects[n] = ResourceObject.Get(request.category, request.resourceNames[n], asset_bundle_load_asset2.asset);
								loadingAssetCountFromAssetBundle--;
							}
							else
							{
								load_objects[n] = ResourceObject.Get(request.category, request.resourceNames[n], asset_bundle.LoadAsset(request.resourceNames[n]));
							}
							if (load_objects[n] != null)
							{
								num4 = loaded_num + 1;
								loaded_num = num4;
							}
						}
						num4 = n + 1;
						n = num4;
					}
				}
				if (asset_bundle != null && (request.downloadOnly || (!use_package_cache && !request.cachePackage)))
				{
					if (!use_package_cache && !use_package_cache_delay)
					{
						cache.AddDelayUnloadAssetBundle(request.packageName, ref asset_bundle);
					}
					load_package = null;
					cache.ReleasePackageObjects(dependency_packages);
					dependency_packages = null;
				}
				downloadList.Remove(request);
			}
			else
			{
				CATEGORY_TYPE cATEGORY_TYPE = ResourceDefine.types[(int)request.category];
				string filename = (cATEGORY_TYPE != 0 && (uint)(cATEGORY_TYPE - 2) > 1u) ? (request.packageName + "/") : string.Empty;
				if (request.category == RESOURCE_CATEGORY.PLAYER_HIGH_RESO_TEX)
				{
					string text = request.packageName.Substring(0, 3);
					string text2 = request.packageName.Substring(3, 2);
					if (!text.Equals("WEP"))
					{
						text2 = (int.Parse(text2) / 10 * 10).ToString("00");
					}
					filename = text + "/" + text + text2 + "/" + filename;
				}
				int i = 0;
				while (i < load_num)
				{
					int num4;
					if (load_objects[i] == null)
					{
						if (!request.internalMode && request.category != RESOURCE_CATEGORY.UI)
						{
							bool num6 = enableLoadDirect;
							enableLoadDirect = true;
							Coroutine coroutine = StartCoroutine(LoadDirect(request, filename + request.resourceNames[i], delegate(UnityEngine.Object o)
							{
								if (o != null)
								{
									load_objects[i] = ResourceObject.Get(request.category, request.resourceNames[i], o);
								}
							}));
							enableLoadDirect = num6;
							yield return coroutine;
						}
						else if (request.category == RESOURCE_CATEGORY.UI)
						{
							string path2 = request.uiDep.path;
							if (onAsyncLoadQuery() || request.downloadOnly)
							{
								ResourceRequest req2 = Resources.LoadAsync(path2);
								while (!req2.isDone)
								{
									yield return null;
								}
								load_objects[i] = ResourceObject.Get(request.category, request.resourceNames[i], req2.asset);
							}
							else
							{
								load_objects[i] = ResourceObject.Get(request.category, request.resourceNames[i], Resources.Load(path2));
							}
						}
						else
						{
							string path3 = $"Internal/internal__{request.category.ToString()}__{request.resourceNames[i]}";
							if (onAsyncLoadQuery() || request.downloadOnly)
							{
								ResourceRequest req2 = Resources.LoadAsync(path3);
								while (!req2.isDone)
								{
									yield return null;
								}
								load_objects[i] = ResourceObject.Get(request.category, request.resourceNames[i], req2.asset);
							}
							else
							{
								load_objects[i] = ResourceObject.Get(request.category, request.resourceNames[i], Resources.Load(path3));
							}
						}
						if (request.master == null && request.sameRequests == null)
						{
							break;
						}
						if (load_objects[i] != null)
						{
							num4 = loaded_num + 1;
							loaded_num = num4;
						}
					}
					num4 = ++i;
				}
				if (!use_package_cache && request.cachePackage)
				{
					load_package = PackageObject.Get(request.packageName, "dummy");
				}
			}
		}
		int num7 = 0;
		if (request.master != null)
		{
			num7++;
		}
		if (request.sameRequests != null)
		{
			int num8 = 0;
			for (int count2 = request.sameRequests.Count; num8 < count2; num8++)
			{
				if (request.sameRequests[num8].master != null)
				{
					num7++;
				}
			}
		}
		bool flag3 = asset_bundle != null;
		if (num7 > 0)
		{
			if (cache.objectCaches != null && request.enableCache && load_num > 0)
			{
				if (object_cache_category == null)
				{
					object_cache_category = cache.objectCaches[(int)request.category];
					if (object_cache_category == null)
					{
						object_cache_category = new StringKeyTable<ResourceObject>();
						cache.objectCaches[(int)request.category] = object_cache_category;
					}
				}
				for (int num9 = 0; num9 < load_num; num9++)
				{
					if (!use_object_caches[num9] && load_objects[num9] != null)
					{
						object_cache_category.Add(request.resourceNames[num9], load_objects[num9]);
					}
				}
			}
			if (!request.downloadOnly && request.cachePackage && !string.IsNullOrEmpty(request.packageName) && !use_package_cache && load_package != null && cache.GetCachedPackage(request.packageName) == null)
			{
				cache.packageCaches.Add(request.packageName, load_package);
				flag3 = false;
			}
			if (load_package != null && load_objects != null)
			{
				int num10 = 0;
				for (int num11 = load_objects.Length; num10 < num11; num10++)
				{
					if (load_objects[num10] != null)
					{
						load_package.refCount += num7;
						load_objects[num10].package = load_package;
					}
				}
				if (dependency_packages != null)
				{
					int num12 = 0;
					for (int size = dependency_packages.size; num12 < size; num12++)
					{
						PackageObject packageObject = dependency_packages[num12];
						if (packageObject != null)
						{
							packageObject.refCount += num7;
							load_package.linkPackages.Add(packageObject);
						}
					}
				}
			}
		}
		if (flag3)
		{
			cache.AddDelayUnloadAssetBundle(request.packageName, ref asset_bundle);
		}
		if (request.downloadOnly)
		{
			loaded_num = load_num;
		}
		if (request.master != null || request.sameRequests != null)
		{
			if (loaded_num == load_num)
			{
				if (request.onAtlasComplete != null && request.category == RESOURCE_CATEGORY.UI_ATLAS)
				{
					request.onAtlasComplete(request, load_objects);
				}
				if (request.onComplate != null)
				{
					request.onComplate(request, load_objects);
				}
				if (request.sameRequests != null)
				{
					int num13 = 0;
					for (int count3 = request.sameRequests.Count; num13 < count3; num13++)
					{
						LoadRequest loadRequest2 = request.sameRequests[num13];
						if (loadRequest2.onAtlasComplete != null && loadRequest2.category == RESOURCE_CATEGORY.UI_ATLAS)
						{
							loadRequest2.onAtlasComplete(loadRequest2, load_objects);
						}
						if (loadRequest2.master != null && loadRequest2.onComplate != null)
						{
							loadRequest2.onComplate(loadRequest2, load_objects);
						}
					}
				}
			}
			else
			{
				if (request.onError != null)
				{
					request.onError(request, ERROR_CODE.NOT_FOUND);
				}
				if (request.sameRequests != null)
				{
					int num14 = 0;
					for (int count4 = request.sameRequests.Count; num14 < count4; num14++)
					{
						LoadRequest loadRequest3 = request.sameRequests[num14];
						if (loadRequest3.master != null && loadRequest3.onError != null)
						{
							loadRequest3.onError(loadRequest3, ERROR_CODE.NOT_FOUND);
						}
					}
				}
			}
		}
		RemoveRequest(ref request);
	}

	private void AddAssetBundleRequest(LoadRequest request)
	{
		loadRequests.Add(request);
		if (onAddRequest != null)
		{
			onAddRequest();
		}
		StartCoroutine(DoLoadAssetBundle(request));
	}

	private IEnumerator DoLoadAssetBundle(LoadRequest request)
	{
		if (MonoBehaviourSingleton<AppMain>.I.isExecutingClearMemory)
		{
			stayCount++;
			while (MonoBehaviourSingleton<AppMain>.I.isExecutingClearMemory)
			{
				if (!request.IsValid())
				{
					stayCount--;
					BreakRequest(ref request);
					yield break;
				}
				yield return null;
			}
			stayCount--;
		}
		if (request.dependencyRequests != null)
		{
			int m = 0;
			int n = request.dependencyRequests.Count;
			while (true)
			{
				if (!request.IsValid())
				{
					BreakRequest(ref request);
					yield break;
				}
				bool flag = false;
				LoadRequest loadRequest = request.dependencyRequests[m];
				int num = 0;
				for (int count = loadRequests.Count; num < count; num++)
				{
					if (loadRequests[num] == loadRequest)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					yield return null;
					continue;
				}
				m++;
				if (m == n)
				{
					break;
				}
			}
		}
		BetterList<PackageObject> dependency_packages = cache.GetDependencyPackages(request.dependencyRequests);
		if (!request.internalMode)
		{
			while (isDownloadError)
			{
				yield return null;
				if (!request.IsValid())
				{
					BreakRequest(ref request);
					yield break;
				}
			}
		}
		AssetBundle asset_bundle = null;
		ResourceObject[] load_objects = null;
		bool use_package_cache = false;
		bool use_package_cache_delay = false;
		bool[] use_object_caches = null;
		int load_num = (request.resourceNames != null) ? request.resourceNames.Length : 0;
		int loaded_num = 0;
		bool package_only = load_num == 0;
		if (request.downloadOnly)
		{
			package_only = true;
		}
		if (load_num > 0)
		{
			load_objects = new ResourceObject[load_num];
			use_object_caches = new bool[load_num];
		}
		StringKeyTable<ResourceObject> object_cache_category = (request.category != RESOURCE_CATEGORY.MAX) ? cache.objectCaches[(int)request.category] : null;
		PackageObject load_package = cache.GetCachedPackage(request.packageName);
		if (load_package != null)
		{
			asset_bundle = (load_package.obj as AssetBundle);
			use_package_cache = true;
		}
		if (asset_bundle == null)
		{
			load_package = null;
		}
		if (loaded_num < load_num && cache.objectCaches != null && cache.systemCaches != null && object_cache_category != null)
		{
			int num2 = 0;
			for (int num3 = load_num; num2 < num3; num2++)
			{
				if (load_objects[num2] == null)
				{
					string res_name = request.resourceNames[num2];
					load_objects[num2] = object_cache_category.Get(res_name);
					if (load_objects[num2] == null)
					{
						load_objects[num2] = cache.systemCaches.Find((ResourceObject o) => o.obj.name == res_name);
					}
					if (load_objects[num2] != null)
					{
						use_object_caches[num2] = true;
						int num4 = loaded_num + 1;
						loaded_num = num4;
					}
				}
			}
		}
		if (loaded_num < load_num && !request.downloadOnly && load_package != null && asset_bundle != null)
		{
			int n = 0;
			int m = load_num;
			while (n < m && (request.master != null || request.sameRequests != null))
			{
				int num4;
				if (load_objects[n] == null)
				{
					if (onAsyncLoadQuery())
					{
						while (IsWaitAssetBundleLoadAsync())
						{
							yield return null;
						}
						loadingAssetCountFromAssetBundle++;
						AssetBundleRequest asset_bundle_load_asset2 = asset_bundle.LoadAssetAsync(request.resourceNames[n]);
						while (!asset_bundle_load_asset2.isDone)
						{
							yield return null;
						}
						load_objects[n] = ResourceObject.Get(request.category, request.resourceNames[n], asset_bundle_load_asset2.asset);
						loadingAssetCountFromAssetBundle--;
					}
					else
					{
						load_objects[n] = ResourceObject.Get(request.category, request.resourceNames[n], asset_bundle.LoadAsset(request.resourceNames[n]));
					}
					if (load_objects[n] != null)
					{
						num4 = loaded_num + 1;
						loaded_num = num4;
					}
				}
				num4 = n + 1;
				n = num4;
			}
		}
		if (load_package == null && (loaded_num < load_num || package_only))
		{
			bool flag2 = (request.category != RESOURCE_CATEGORY.ASSETBUNDLEINFO) ? (manifest != null) : (sizeInfoManifest != null);
			if (request.category == RESOURCE_CATEGORY.MAX || (isDownloadAssets && flag2 && (!request.internalMode || request.downloadOnly)))
			{
				while (downloadList.size >= MAX_DL_COUNT || CheckPackageDownloading(request))
				{
					yield return null;
					if (!request.IsValid())
					{
						BreakRequest(ref request);
						yield break;
					}
				}
				downloadList.Add(request);
				int m = 0;
				bool is_retry;
				do
				{
					is_retry = false;
					if (!request.IsValid())
					{
						BreakRequest(ref request);
						yield break;
					}
					if (!request.downloadOnly)
					{
						load_package = cache.GetCachedPackage(request.packageName);
						if (load_package != null)
						{
							asset_bundle = (load_package.obj as AssetBundle);
							use_package_cache = true;
						}
					}
					if (load_package == null && !(asset_bundle == null))
					{
						continue;
					}
					while (isDownloadError && !request.internalMode)
					{
						yield return null;
						if (!request.IsValid())
						{
							BreakRequest(ref request);
							yield break;
						}
					}
					Hash128 hash = request.hash;
					RESOURCE_CATEGORY? category = MonoBehaviourSingleton<GoGameResourceManager>.I.GetCategory(request.packageName);
					string url;
					if (request.eventAsset)
					{
						url = downloadEventUrl + request.packageName + "?v=" + request.hash.ToString();
					}
					else
					{
						string variantName = MonoBehaviourSingleton<GoGameResourceManager>.I.GetVariantName((!category.HasValue) ? RESOURCE_CATEGORY.MAX : category.Value);
						url = ((!request.packageName.Contains("-sd")) ? (downloadURL + request.packageName + variantName + "?v=" + request.hash.ToString()) : (downloadURL + request.packageName + "?v=" + request.hash.ToString()));
					}
					if (request.category == RESOURCE_CATEGORY.ASSETBUNDLEINFO)
					{
						url = downloadSizeInfoURL + request.packageName + "?v=" + request.hash.ToString();
					}
					Path.GetFileName(request.packageName);
					string text = url;
					if (request.IsValid())
					{
						bool num5 = Caching.IsVersionCached(text, hash);
						Error error_code = Error.None;
						AssetBundle loaded_assetBundle = null;
						UnityWebRequest assetBundle;
						UnityWebRequest www = assetBundle = UnityWebRequestAssetBundle.GetAssetBundle(text, hash);
						using (assetBundle)
						{
							if (!num5)
							{
								int num7 = www.timeout = (int)WWW_TIME_OUT;
								yield return www.SendWebRequest();
								request.progressObject = www;
								if (!www.isDone || www.isNetworkError || www.isHttpError)
								{
									error_code = Error.AssetLoadFailed;
								}
								else if (www.error != null)
								{
									error_code = ((!www.error.Contains("404")) ? Error.AssetLoadFailed : Error.AssetNotFound);
								}
								else
								{
									if (!request.downloadOnly)
									{
										CrashlyticsReporter.SetLoadingBundle(url);
										loaded_assetBundle = DownloadHandlerAssetBundle.GetContent(www);
										if (loaded_assetBundle == null)
										{
											load_package = cache.GetCachedPackage(request.packageName);
											if (load_package != null)
											{
												loaded_assetBundle = (load_package.obj as AssetBundle);
												asset_bundle = loaded_assetBundle;
												use_package_cache = true;
												use_package_cache_delay = true;
											}
										}
										CrashlyticsReporter.SetLoadingBundle("");
									}
									www.Dispose();
								}
							}
							else if (!request.downloadOnly)
							{
								if (loaded_assetBundle == null)
								{
									load_package = cache.GetCachedPackage(request.packageName);
									if (load_package != null)
									{
										loaded_assetBundle = (load_package.obj as AssetBundle);
										asset_bundle = loaded_assetBundle;
										use_package_cache = true;
										use_package_cache_delay = true;
									}
								}
								if (loaded_assetBundle == null)
								{
									loaded_assetBundle = GetLoadedAssetBundle(request.packageName);
								}
								if (loaded_assetBundle == null)
								{
									yield return www.SendWebRequest();
									if (onAsyncLoadQuery())
									{
										yield return null;
									}
									if (!www.isDone || www.isNetworkError || www.isHttpError)
									{
										error_code = Error.AssetLoadFailed;
									}
									else if (www.error != null)
									{
										error_code = ((!www.error.Contains("404")) ? Error.AssetLoadFailed : Error.AssetNotFound);
									}
									else
									{
										if (!request.downloadOnly)
										{
											CrashlyticsReporter.SetLoadingBundle(url);
											loaded_assetBundle = DownloadHandlerAssetBundle.GetContent(www);
											if (loaded_assetBundle == null)
											{
												load_package = cache.GetCachedPackage(request.packageName);
												if (load_package != null)
												{
													loaded_assetBundle = (load_package.obj as AssetBundle);
													asset_bundle = loaded_assetBundle;
													use_package_cache = true;
													use_package_cache_delay = true;
												}
											}
											CrashlyticsReporter.SetLoadingBundle("");
										}
										www.Dispose();
									}
								}
							}
							if (request.progressObject != null)
							{
								request.progressObject = PROGRESS_COMPLATE;
							}
							while (isDownloadError && !request.internalMode)
							{
								yield return null;
								if (!request.IsValid())
								{
									cache.AddDelayUnloadAssetBundle(request.packageName, ref loaded_assetBundle);
									www.Dispose();
									BreakRequest(ref request);
									yield break;
								}
							}
							if (error_code == Error.None)
							{
								if (!request.downloadOnly && (request.cachePackage || !package_only || request.category == RESOURCE_CATEGORY.MAX))
								{
									load_package = PackageObject.Get(request.packageName, loaded_assetBundle);
									asset_bundle = (load_package.obj as AssetBundle);
								}
								www?.Dispose();
								if (!use_package_cache_delay)
								{
									use_package_cache = false;
								}
							}
							else
							{
								load_package = cache.GetCachedPackage(request.packageName);
								if (load_package != null)
								{
									asset_bundle = (load_package.obj as AssetBundle);
									use_package_cache = true;
								}
								if (load_package != null || asset_bundle == null)
								{
									m++;
									if (autoRetry)
									{
										m = 0;
									}
									if (m < 3)
									{
										yield return new WaitForSeconds(1f);
										is_retry = true;
									}
									else
									{
										if (error_code == Error.AssetLoadFailed)
										{
											Log.Error(LOG.RESOURCE, "{0}:{1}", error_code.ToString(), url);
										}
										else
										{
											Log.Error("{0}:{1}:{2}:{3}", error_code.ToString(), request.category, request.packageName, url);
										}
										if (isDownloadError)
										{
											while (isDownloadError)
											{
												if (!request.IsValid())
												{
													BreakRequest(ref request);
													yield break;
												}
											}
											m = 0;
											is_retry = true;
										}
										else
										{
											isDownloadError = true;
											int n = 0;
											if (request.IsValid() && onDownloadErrorQuery != null)
											{
												while (onDownloadErrorQuery != null)
												{
													n = onDownloadErrorQuery(arg1: true, error_code);
													if (n == 0)
													{
														break;
													}
													yield return null;
												}
												while (n == 0 && onDownloadErrorQuery != null)
												{
													n = onDownloadErrorQuery(arg1: false, error_code);
													yield return null;
												}
												if (n == -1)
												{
													cache.AddDelayUnloadAssetBundle(request.packageName, ref loaded_assetBundle);
													BreakRequest(ref request);
													yield break;
												}
											}
											isDownloadError = false;
											if (n != 1)
											{
												if (request.onError != null)
												{
													request.onError(request, ERROR_CODE.WWW_ERROR);
												}
											}
											else
											{
												m = 0;
												is_retry = true;
											}
										}
									}
								}
								www?.Dispose();
							}
						}
					}
				}
				while (is_retry);
				if (asset_bundle != null)
				{
					int n = 0;
					int j = load_num;
					while (n < j && (request.master != null || request.sameRequests != null) && !(asset_bundle == null))
					{
						int num4;
						if (load_objects[n] == null)
						{
							if (onAsyncLoadQuery())
							{
								while (IsWaitAssetBundleLoadAsync())
								{
									yield return null;
								}
								loadingAssetCountFromAssetBundle++;
								AssetBundleRequest asset_bundle_load_asset2 = asset_bundle.LoadAssetAsync(request.resourceNames[n]);
								while (!asset_bundle_load_asset2.isDone)
								{
									yield return null;
								}
								load_objects[n] = ResourceObject.Get(request.category, request.resourceNames[n], asset_bundle_load_asset2.asset);
								loadingAssetCountFromAssetBundle--;
							}
							else
							{
								load_objects[n] = ResourceObject.Get(request.category, request.resourceNames[n], asset_bundle.LoadAsset(request.resourceNames[n]));
							}
							if (load_objects[n] != null)
							{
								num4 = loaded_num + 1;
								loaded_num = num4;
							}
						}
						num4 = n + 1;
						n = num4;
					}
				}
				if (asset_bundle != null && (request.downloadOnly || (!use_package_cache && !request.cachePackage)))
				{
					if (!use_package_cache && !use_package_cache_delay)
					{
						cache.AddDelayUnloadAssetBundle(request.packageName, ref asset_bundle);
					}
					load_package = null;
					cache.ReleasePackageObjects(dependency_packages);
					dependency_packages = null;
				}
				downloadList.Remove(request);
			}
			else
			{
				CATEGORY_TYPE cATEGORY_TYPE = ResourceDefine.types[(int)request.category];
				string url = (cATEGORY_TYPE != 0 && (uint)(cATEGORY_TYPE - 2) > 1u) ? (request.packageName + "/") : string.Empty;
				if (request.category == RESOURCE_CATEGORY.PLAYER_HIGH_RESO_TEX)
				{
					string text2 = request.packageName.Substring(0, 3);
					string text3 = request.packageName.Substring(3, 2);
					if (!text2.Equals("WEP"))
					{
						text3 = (int.Parse(text3) / 10 * 10).ToString("00");
					}
					url = text2 + "/" + text2 + text3 + "/" + url;
				}
				int i = 0;
				while (i < load_num)
				{
					int num4;
					if (load_objects[i] == null)
					{
						if (!request.internalMode && request.category != RESOURCE_CATEGORY.UI)
						{
							bool num8 = enableLoadDirect;
							enableLoadDirect = true;
							Coroutine coroutine = StartCoroutine(LoadDirect(request, url + request.resourceNames[i], delegate(UnityEngine.Object o)
							{
								if (o != null)
								{
									load_objects[i] = ResourceObject.Get(request.category, request.resourceNames[i], o);
								}
							}));
							enableLoadDirect = num8;
							yield return coroutine;
						}
						else if (request.category == RESOURCE_CATEGORY.UI)
						{
							string path = request.uiDep.path;
							if (onAsyncLoadQuery() || request.downloadOnly)
							{
								ResourceRequest req2 = Resources.LoadAsync(path);
								while (!req2.isDone)
								{
									yield return null;
								}
								load_objects[i] = ResourceObject.Get(request.category, request.resourceNames[i], req2.asset);
							}
							else
							{
								load_objects[i] = ResourceObject.Get(request.category, request.resourceNames[i], Resources.Load(path));
							}
						}
						else
						{
							string path2 = $"Internal/internal__{request.category.ToString()}__{request.resourceNames[i]}";
							if (onAsyncLoadQuery() || request.downloadOnly)
							{
								ResourceRequest req2 = Resources.LoadAsync(path2);
								while (!req2.isDone)
								{
									yield return null;
								}
								load_objects[i] = ResourceObject.Get(request.category, request.resourceNames[i], req2.asset);
							}
							else
							{
								load_objects[i] = ResourceObject.Get(request.category, request.resourceNames[i], Resources.Load(path2));
							}
						}
						if (request.master == null && request.sameRequests == null)
						{
							break;
						}
						if (load_objects[i] != null)
						{
							num4 = loaded_num + 1;
							loaded_num = num4;
						}
					}
					num4 = ++i;
				}
				if (!use_package_cache && request.cachePackage)
				{
					load_package = PackageObject.Get(request.packageName, "dummy");
				}
			}
		}
		int num9 = 0;
		if (request.master != null)
		{
			num9++;
		}
		if (request.sameRequests != null)
		{
			int num10 = 0;
			for (int count2 = request.sameRequests.Count; num10 < count2; num10++)
			{
				if (request.sameRequests[num10].master != null)
				{
					num9++;
				}
			}
		}
		if (num9 > 0)
		{
			if (cache.objectCaches != null && request.enableCache && load_num > 0)
			{
				if (object_cache_category == null)
				{
					object_cache_category = cache.objectCaches[(int)request.category];
					if (object_cache_category == null)
					{
						object_cache_category = new StringKeyTable<ResourceObject>();
						cache.objectCaches[(int)request.category] = object_cache_category;
					}
				}
				for (int num11 = 0; num11 < load_num; num11++)
				{
					if (!use_object_caches[num11] && load_objects[num11] != null)
					{
						object_cache_category.Add(request.resourceNames[num11], load_objects[num11]);
					}
				}
			}
			if (!request.downloadOnly && request.cachePackage && !string.IsNullOrEmpty(request.packageName) && !use_package_cache && load_package != null && cache.GetCachedPackage(request.packageName) == null)
			{
				cache.packageCaches.Add(request.packageName, load_package);
			}
			if (load_package != null && load_objects != null)
			{
				int num12 = 0;
				for (int num13 = load_objects.Length; num12 < num13; num12++)
				{
					if (load_objects[num12] != null)
					{
						load_package.refCount += num9;
						load_objects[num12].package = load_package;
					}
				}
				if (dependency_packages != null)
				{
					int num14 = 0;
					for (int size = dependency_packages.size; num14 < size; num14++)
					{
						PackageObject packageObject = dependency_packages[num14];
						if (packageObject != null)
						{
							packageObject.refCount += num9;
							load_package.linkPackages.Add(packageObject);
						}
					}
				}
			}
		}
		if (request.downloadOnly)
		{
		}
		if (request.master != null || request.sameRequests != null)
		{
			if (request.onAtlasComplete != null && request.category == RESOURCE_CATEGORY.UI_ATLAS)
			{
				request.onAtlasComplete(request, load_objects);
			}
			if (request.onComplate != null)
			{
				request.onComplate(request, load_objects);
			}
			if (request.sameRequests != null)
			{
				int num15 = 0;
				for (int count3 = request.sameRequests.Count; num15 < count3; num15++)
				{
					LoadRequest loadRequest2 = request.sameRequests[num15];
					if (loadRequest2.onAtlasComplete != null && loadRequest2.category == RESOURCE_CATEGORY.UI_ATLAS)
					{
						loadRequest2.onAtlasComplete(loadRequest2, load_objects);
					}
					if (loadRequest2.master != null && loadRequest2.onComplate != null)
					{
						loadRequest2.onComplate(loadRequest2, load_objects);
					}
				}
			}
		}
		if (request.unloadAsset && asset_bundle != null)
		{
			asset_bundle.Unload(unloadAllLoadedObjects: false);
		}
		RemoveRequest(ref request);
	}

	private AssetBundle GetLoadedAssetBundle(string name)
	{
		foreach (AssetBundle allLoadedAssetBundle in AssetBundle.GetAllLoadedAssetBundles())
		{
			if (allLoadedAssetBundle.name == name)
			{
				return allLoadedAssetBundle;
			}
		}
		return null;
	}

	private void BreakRequest(ref LoadRequest request)
	{
		RemoveRequest(ref request);
	}

	private void RemoveRequest(ref LoadRequest request)
	{
		downloadList.Remove(request);
		request.Cancel();
		if (request.sameRequests != null)
		{
			int i = 0;
			for (int count = request.sameRequests.Count; i < count; i++)
			{
				request.sameRequests[i].Cancel();
			}
		}
		loadRequests.Remove(request);
		request = null;
		if (onRemoveRequest != null)
		{
			onRemoveRequest();
		}
	}

	private bool IsWaitAssetBundleLoadAsync()
	{
		if (loadingAssetCountFromAssetBundle >= loadingAssetCountLimit)
		{
			return true;
		}
		return false;
	}

	public void Cancel(object master)
	{
		int i = 0;
		for (int count = loadRequests.Count; i < count; i++)
		{
			LoadRequest loadRequest = loadRequests[i];
			if (loadRequest.sameRequests != null)
			{
				int j = 0;
				for (int count2 = loadRequest.sameRequests.Count; j < count2; j++)
				{
					if (loadRequest.sameRequests[j].master == master)
					{
						loadRequest.sameRequests[j].Cancel();
					}
				}
			}
			if (loadRequest.master == master)
			{
				loadRequest.Cancel();
			}
		}
	}

	public void CancelAll()
	{
		int i = 0;
		for (int count = loadRequests.Count; i < count; i++)
		{
			LoadRequest loadRequest = loadRequests[i];
			loadRequest.Cancel();
			if (loadRequest.sameRequests != null)
			{
				int j = 0;
				for (int count2 = loadRequest.sameRequests.Count; j < count2; j++)
				{
					loadRequest.sameRequests[j].Cancel();
				}
			}
		}
	}

	protected override void OnDetachServant(DisableNotifyMonoBehaviour servant)
	{
		Cancel(servant);
	}

	public bool ExistRequest(RESOURCE_CATEGORY category, string resource_name)
	{
		int i = 0;
		for (int count = loadRequests.Count; i < count; i++)
		{
			LoadRequest loadRequest = loadRequests[i];
			if (loadRequest.category == category && loadRequest.packageName == resource_name)
			{
				return true;
			}
		}
		return false;
	}

	public bool ExistRequest(RESOURCE_CATEGORY category, string package_name, string[] resource_names)
	{
		int i = 0;
		for (int count = loadRequests.Count; i < count; i++)
		{
			LoadRequest loadRequest = loadRequests[i];
			if (loadRequest.category != category || !(loadRequest.packageName == package_name))
			{
				continue;
			}
			if (loadRequest.resourceNames == null && resource_names == null)
			{
				return true;
			}
			if (loadRequest.resourceNames.Length == resource_names.Length)
			{
				int j = 0;
				int num;
				for (num = loadRequest.resourceNames.Length; j < num && !(loadRequest.resourceNames[j] != resource_names[j]); j++)
				{
				}
				if (j == num)
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool IsCached(RESOURCE_CATEGORY category, string packageName)
	{
		if (manifest != null)
		{
			string bundleName = category.ToAssetBundleName(packageName);
			Hash128 assetBundleHash = manifest.GetAssetBundleHash(MonoBehaviourSingleton<GoGameResourceManager>.I.GetFullBundleName(bundleName));
			if (assetBundleHash.isValid)
			{
				return Caching.IsVersionCached(GetAssetBundleURL(category, packageName, assetBundleHash), assetBundleHash);
			}
		}
		return IsCached(category.ToAssetBundleName(packageName));
	}

	private string GetAssetBundleURL(RESOURCE_CATEGORY category, string packageName, Hash128 hash)
	{
		string variantName = MonoBehaviourSingleton<GoGameResourceManager>.I.GetVariantName(category);
		string result = (!packageName.Contains("-sd")) ? (downloadURL + packageName + variantName + "?v=" + hash.ToString()) : (downloadURL + packageName + "?v=" + hash.ToString());
		if (category == RESOURCE_CATEGORY.ASSETBUNDLEINFO)
		{
			result = downloadSizeInfoURL + packageName + "?v=" + hash.ToString();
		}
		return result;
	}

	public bool IsCached(string dependency)
	{
		if (manifest == null)
		{
			return false;
		}
		return IsVersionCached(Path.GetFileName(dependency), manifest.GetAssetBundleHash(MonoBehaviourSingleton<GoGameResourceManager>.I.GetFullBundleName(dependency)));
	}

	public bool IsCachedDependencies(RESOURCE_CATEGORY category, string packageName)
	{
		string assetName = category.ToAssetBundleName(packageName);
		string[] aLLDependenciesFileName = GetALLDependenciesFileName(assetName);
		foreach (string dependency in aLLDependenciesFileName)
		{
			if (!IsCached(dependency))
			{
				return false;
			}
		}
		return true;
	}

	public bool IsCachedWithDependencies(RESOURCE_CATEGORY category, string packageName)
	{
		if (IsCached(category, packageName))
		{
			return IsCachedDependencies(category, packageName);
		}
		return false;
	}

	public string[] GetALLDependenciesFileName(string assetName)
	{
		HashSet<string> hashSet = new HashSet<string>();
		if (manifest != null)
		{
			string[] allDependencies = manifest.GetAllDependencies(MonoBehaviourSingleton<GoGameResourceManager>.I.GetFullBundleName(assetName));
			for (int i = 0; i < allDependencies.Length; i++)
			{
				hashSet.UnionWith(GetALLDependenciesFileName(MonoBehaviourSingleton<GoGameResourceManager>.I.GetBundleNameWithoutVariant(allDependencies[i])));
			}
		}
		string[] array = new string[hashSet.Count];
		hashSet.CopyTo(array);
		return array;
	}

	public static bool IsVersionCached(string filename, Hash128 hash)
	{
		return File.Exists(GetCachePath(filename, hash));
	}

	public static string GetCachePath(string filename, Hash128 hash)
	{
		return Path.Combine(GetCacheDir(filename), filename + "." + hash.ToString());
	}

	public static string GetCacheDir(string filename)
	{
		string path = ((byte)(filename.GetHashCode() % 256)).ToString("X2");
		return Path.Combine(cacheDir, path);
	}

	private Error SaveAssetBundle(string filename, Hash128 hash, byte[] bytes)
	{
		if (bytes.Length == 0)
		{
			return Error.AssetLoadFailed;
		}
		Error result = Error.None;
		string cacheDir = GetCacheDir(filename);
		if (Directory.Exists(cacheDir))
		{
			string[] files = Directory.GetFiles(cacheDir, filename + "*");
			for (int i = 0; i < files.Length; i++)
			{
				File.Delete(files[i]);
			}
		}
		else
		{
			Directory.CreateDirectory(cacheDir);
		}
		string cachePath = GetCachePath(filename, hash);
		try
		{
			File.WriteAllBytes(cachePath, bytes);
			return result;
		}
		catch
		{
			return Error.AssetSaveFailed;
		}
	}

	public static IEnumerator ClearCache()
	{
		string[] dirs = Directory.GetDirectories(cacheDir);
		for (int j = 0; j < dirs.Length; j++)
		{
			string path = dirs[j];
			try
			{
				Directory.Delete(path, recursive: true);
			}
			catch
			{
			}
			if (j % 2 == 0)
			{
				yield return null;
			}
		}
		if (!Directory.Exists(cachingDir))
		{
			yield break;
		}
		string[] cdirs = Directory.GetDirectories(cachingDir);
		for (int j = 0; j < cdirs.Length; j++)
		{
			string path2 = cdirs[j];
			try
			{
				Directory.Delete(path2, recursive: true);
			}
			catch
			{
			}
			if (j % 2 == 0)
			{
				yield return null;
			}
		}
	}

	public UnityEngine.Object LoadDirect(RESOURCE_CATEGORY category, string resource_name)
	{
		if (!enableLoadDirect)
		{
			Log.Error(LOG.RESOURCE, "can not LoadDirect. " + category.ToString() + " : " + resource_name);
			return null;
		}
		UnityEngine.Object @object = null;
		string[] array = ResourceDefine.subPaths[(int)category];
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			@object = ExternalResources.Load<UnityEngine.Object>(array[i] + resource_name);
			if (@object != null)
			{
				break;
			}
		}
		return @object;
	}

	public IEnumerator LoadDirect(LoadRequest request, string resource_name, Action<UnityEngine.Object> onComplete)
	{
		RESOURCE_CATEGORY category = request.category;
		UnityEngine.Object load_object = null;
		if (!enableLoadDirect)
		{
			Log.Error(LOG.RESOURCE, "can not LoadDirect. " + category.ToString() + " : " + resource_name);
			onComplete(load_object);
			yield break;
		}
		string[] paths = ResourceDefine.subPaths[(int)category];
		int i = 0;
		int j = paths.Length;
		while (i < j)
		{
			string path2 = paths[i] + resource_name;
			if (path2.Contains("StreamingAssets"))
			{
				path2 = Application.streamingAssetsPath + path2.Replace("StreamingAssets", "").ToLower();
				UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(path2);
				www.SendWebRequest();
				request.progressObject = www;
				while (!www.isDone)
				{
					yield return null;
				}
				request.progressObject = PROGRESS_COMPLATE;
				load_object = DownloadHandlerAssetBundle.GetContent(www);
				www.Dispose();
			}
			else if (onAsyncLoadQuery() || request.downloadOnly)
			{
				yield return ExternalResources.LoadAsync(path2, delegate(ResourceRequest progress)
				{
					request.progressObject = progress;
				}, delegate(UnityEngine.Object asset)
				{
					request.progressObject = PROGRESS_COMPLATE;
					load_object = asset;
				});
			}
			else
			{
				yield return null;
				load_object = ExternalResources.Load<UnityEngine.Object>(path2);
			}
			if (load_object != null)
			{
				break;
			}
			int num = i + 1;
			i = num;
		}
		onComplete(load_object);
	}

	public static UnityEngine.Object LoadDirect(RESOURCE_CATEGORY category, string package_name, string resource_name)
	{
		if (!enableLoadDirect)
		{
			Log.Error(LOG.RESOURCE, "can not LoadDirect. " + category.ToString() + " : " + package_name + " : " + resource_name);
			return null;
		}
		UnityEngine.Object @object = null;
		string[] array = ResourceDefine.subPaths[(int)category];
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			@object = ExternalResources.Load<UnityEngine.Object>(array[i] + package_name + "/" + resource_name);
			if (@object != null)
			{
				break;
			}
		}
		return @object;
	}

	public UnityEngine.Object __LoadDirect(RESOURCE_CATEGORY category, string resource_name)
	{
		bool flag = enableLoadDirect;
		enableLoadDirect = true;
		UnityEngine.Object result = LoadDirect(category, resource_name);
		enableLoadDirect = flag;
		return result;
	}

	public UnityEngine.Object __LoadDirect(RESOURCE_CATEGORY category, string package_name, string resource_name)
	{
		bool flag = enableLoadDirect;
		enableLoadDirect = true;
		UnityEngine.Object result = LoadDirect(category, package_name, resource_name);
		enableLoadDirect = flag;
		return result;
	}

	public static IEnumerator UnleaseAllLoadedAssetBundle()
	{
		foreach (AssetBundle allLoadedAssetBundle in AssetBundle.GetAllLoadedAssetBundles())
		{
			allLoadedAssetBundle.Unload(unloadAllLoadedObjects: false);
			yield return null;
		}
	}
}
