using MsgPack;
using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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
			if (category != RESOURCE_CATEGORY.MAX)
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
				return (progressObject as ResourceRequest).get_progress();
			}
			if (progressObject is WWW)
			{
				return (progressObject as WWW).get_progress();
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

	public const int DEFAULT_LOADING_ASSET_COUNT_LIMIT = 4;

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

	private int _assetIndex = 4;

	private int _tableIndex = 1;

	private string baseURL = string.Empty;

	public Action onAddRequest;

	public Action onRemoveRequest;

	private BetterList<LoadRequest> downloadList = new BetterList<LoadRequest>();

	private static int MAX_DL_COUNT = 3;

	private static float WWW_TIME_OUT = 90f;

	private bool isDownloadError;

	public int stayCount;

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

	public AssetBundleManifest manifest
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

	private static string cacheDir => Path.Combine(Application.get_temporaryCachePath(), "assets");

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
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		base.Awake();
		this.get_gameObject().AddComponent<GoGameResourceManager>();
		Object.DontDestroyOnLoad(this);
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
		return Application.get_streamingAssetsPath();
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
	}

	public void LoadManifest()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoLoadManifest());
	}

	private IEnumerator DoLoadManifest()
	{
		while (isLoadingManifest || isLoading)
		{
			yield return (object)null;
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
				string www_url = url;
				WWW _www = new WWW(www_url);
				yield return (object)_www;
				string www_error = _www.get_error();
				if (!string.IsNullOrEmpty(www_error))
				{
					error_code = ((!www_error.Contains("404")) ? Error.AssetLoadFailed : Error.AssetNotFound);
				}
				else if (_www.get_assetBundle() != null)
				{
					manifest = _www.get_assetBundle().LoadAsset<AssetBundleManifest>("AssetBundleManifest");
					_www.get_assetBundle().Unload(false);
				}
				else
				{
					error_code = Error.AssetLoadFailed;
					Log.Error(LOG.RESOURCE, _www.get_text());
				}
				_www.Dispose();
				if (error_code != 0)
				{
					retry_count++;
					if (retry_count >= 3)
					{
						Log.Error(LOG.RESOURCE, www_error);
						int query_result = 0;
						if (onDownloadErrorQuery != null)
						{
							while (onDownloadErrorQuery != null)
							{
								query_result = onDownloadErrorQuery(true, error_code);
								if (query_result == 0)
								{
									break;
								}
								yield return (object)null;
							}
							while (query_result == 0 && onDownloadErrorQuery != null)
							{
								query_result = onDownloadErrorQuery(false, error_code);
								yield return (object)null;
							}
							if (query_result == -1)
							{
								yield break;
							}
						}
						retry_count = 0;
					}
					yield return (object)new WaitForSeconds(1f);
				}
			}
			while (error_code != 0);
		}
		isLoadingManifest = false;
	}

	public void Reset()
	{
		if (isLoading)
		{
			Debug.LogError((object)"isLoading == true");
		}
		CancelAll();
		loadRequests.Clear();
		this.StopAllCoroutines();
		cache.ClearObjectCaches(true);
		cache.ClearPackageCaches();
		cache.ClearSystemPackageCaches();
		cache.ClearSENameDictionary();
		cache.ReleaseAllDelayUnloadAssetBundles();
		cache.shaderCaches.Clear();
		downloadList.Clear();
		m_cacheABDependencies.Clear();
		isDownloadError = false;
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

	private void Load(LoadRequest request)
	{
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		request.enableCache = enableCache;
		request.downloadOnly = downloadOnly;
		request.internalMode = internalMode;
		if (request.category == RESOURCE_CATEGORY.UI)
		{
			request.internalMode = false;
		}
		List<LoadRequest> list = null;
		if ((!request.internalMode || request.downloadOnly) && isDownloadAssets && manifest != null)
		{
			request.Setup();
			if (request.category != RESOURCE_CATEGORY.UI)
			{
				request.hash = manifest.GetAssetBundleHash(MonoBehaviourSingleton<GoGameResourceManager>.I.GetFullBundleName(request.packageName));
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
			if ((loadRequest2.master != null || loadRequest2.sameRequests != null) && !(loadRequest2.packageName != request.packageName))
			{
				if (loadRequest2.category == request.category)
				{
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
					return;
				}
				loadRequest = loadRequest2;
			}
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
	}

	private UIDependency GetUIDependency(LoadRequest request)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		string str = Path.GetFileNameWithoutExtension(request.packageName).ToLower();
		TextAsset val = Resources.Load("InternalUI/Deps/" + str) as TextAsset;
		if (val != null)
		{
			return new ObjectPacker().Unpack<UIDependency>(val.get_bytes());
		}
		return null;
	}

	private List<LoadRequest> GetManifestDependencyRequests(LoadRequest request)
	{
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
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
			if (!(bundleNameWithoutVariant == "shader" + GoGameResourceManager.GetDefaultAssetBundleExtension()) && !(bundleNameWithoutVariant == "ui_font" + GoGameResourceManager.GetDefaultAssetBundleExtension()) && cache.GetCachedPackage(bundleNameWithoutVariant) == null)
			{
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
		}
		return list;
	}

	private List<LoadRequest> GetUIDependencyRequests(LoadRequest request, UIDependency dep)
	{
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
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
			}
			else
			{
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
		}
		return list;
	}

	private void LinkAtlas(PackageObject package, string atlasPath, string resourceName)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		GameObject val = Resources.Load(atlasPath) as GameObject;
		UIAtlas uIAtlas = package.hostAtlas = val.GetComponent<UIAtlas>();
		AssetBundle val2 = package.obj as AssetBundle;
		if (val2 != null)
		{
			GameObject val3 = val2.LoadAsset<GameObject>(resourceName);
			UIAtlas uIAtlas2 = uIAtlas.replacement = val3.GetComponent<UIAtlas>();
		}
	}

	private void AddRequest(LoadRequest request)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		loadRequests.Add(request);
		if (onAddRequest != null)
		{
			onAddRequest();
		}
		this.StartCoroutine(DoLoad(request));
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
				yield return (object)null;
			}
			stayCount--;
		}
		if (request.dependencyRequests != null)
		{
			int k = 0;
			int l = request.dependencyRequests.Count;
			while (true)
			{
				if (!request.IsValid())
				{
					BreakRequest(ref request);
					yield break;
				}
				bool wait = false;
				LoadRequest dependency_request = request.dependencyRequests[k];
				int m = 0;
				for (int n = loadRequests.Count; m < n; m++)
				{
					if (loadRequests[m] == dependency_request)
					{
						wait = true;
						break;
					}
				}
				if (wait)
				{
					yield return (object)null;
				}
				else
				{
					k++;
					if (k == l)
					{
						break;
					}
				}
			}
		}
		BetterList<PackageObject> dependency_packages = cache.GetDependencyPackages(request.dependencyRequests);
		if (!request.internalMode)
		{
			while (isDownloadError && !request.internalMode)
			{
				yield return (object)null;
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
		StringKeyTable<ResourceObject> object_cache_category = (request.category == RESOURCE_CATEGORY.MAX) ? null : cache.objectCaches[(int)request.category];
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
			int i2 = 0;
			for (int n2 = load_num; i2 < n2; i2++)
			{
				if (load_objects[i2] == null)
				{
					string res_name = request.resourceNames[i2];
					load_objects[i2] = object_cache_category.Get(res_name);
					if (load_objects[i2] == null)
					{
						load_objects[i2] = cache.systemCaches.Find((ResourceObject o) => o.obj.get_name() == ((_003CDoLoad_003Ec__Iterator23D)/*Error near IL_04da: stateMachine*/)._003Cres_name_003E__19);
					}
					if (load_objects[i2] != null)
					{
						use_object_caches[i2] = true;
						loaded_num++;
					}
				}
			}
		}
		if (loaded_num < load_num && !request.downloadOnly && load_package != null && asset_bundle != null)
		{
			int i8 = 0;
			for (int n6 = load_num; i8 < n6; i8++)
			{
				if (request.master == null && request.sameRequests == null)
				{
					break;
				}
				if (asset_bundle == null)
				{
					break;
				}
				if (load_objects[i8] == null)
				{
					if (onAsyncLoadQuery())
					{
						while (IsWaitAssetBundleLoadAsync())
						{
							yield return (object)null;
						}
						loadingAssetCountFromAssetBundle++;
						AssetBundleRequest asset_bundle_load_asset2 = asset_bundle.LoadAssetAsync(request.resourceNames[i8]);
						while (!asset_bundle_load_asset2.get_isDone())
						{
							yield return (object)null;
						}
						load_objects[i8] = ResourceObject.Get(request.category, request.resourceNames[i8], asset_bundle_load_asset2.get_asset());
						loadingAssetCountFromAssetBundle--;
					}
					else
					{
						load_objects[i8] = ResourceObject.Get(request.category, request.resourceNames[i8], asset_bundle.LoadAsset(request.resourceNames[i8]));
					}
					if (load_objects[i8] != null)
					{
						loaded_num++;
					}
				}
			}
		}
		if (load_package == null && (loaded_num < load_num || package_only))
		{
			if (request.category == RESOURCE_CATEGORY.MAX || (isDownloadAssets && manifest != null && (!request.internalMode || request.downloadOnly)))
			{
				while (downloadList.size >= MAX_DL_COUNT || CheckPackageDownloading(request))
				{
					yield return (object)null;
					if (!request.IsValid())
					{
						BreakRequest(ref request);
						yield break;
					}
				}
				downloadList.Add(request);
				int retry_count = 0;
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
					if (load_package != null || asset_bundle == null)
					{
						while (isDownloadError && !request.internalMode)
						{
							yield return (object)null;
							if (!request.IsValid())
							{
								BreakRequest(ref request);
								yield break;
							}
						}
						RESOURCE_CATEGORY? category = MonoBehaviourSingleton<GoGameResourceManager>.I.GetCategory(request.packageName);
						string tail = MonoBehaviourSingleton<GoGameResourceManager>.I.GetVariantName(category.HasValue ? category.Value : RESOURCE_CATEGORY.MAX);
						string url = (!request.packageName.Contains("-sd")) ? (downloadURL + request.packageName + tail + "?v=" + request.hash.ToString()) : (downloadURL + request.packageName + "?v=" + request.hash.ToString());
						string filename = Path.GetFileName(request.packageName);
						string www_url = url;
						if (request.IsValid())
						{
							bool is_cached = IsVersionCached(filename, request.hash);
							WWW www = null;
							Error error_code = Error.None;
							AssetBundle loaded_assetBundle = null;
							if (!is_cached)
							{
								www = (request.progressObject = (object)new WWW(www_url));
								float timeOut = WWW_TIME_OUT;
								float currentProgress = www.get_progress();
								while (!www.get_isDone() && timeOut > 0f)
								{
									yield return (object)null;
									if (currentProgress == www.get_progress())
									{
										timeOut -= Time.get_deltaTime();
									}
									else
									{
										currentProgress = www.get_progress();
										timeOut = WWW_TIME_OUT;
									}
									if (!request.IsValid())
									{
										www.Dispose();
										BreakRequest(ref request);
										yield break;
									}
								}
								if (!www.get_isDone())
								{
									error_code = Error.AssetLoadFailed;
								}
								else if (www.get_error() != null)
								{
									error_code = ((!www.get_error().Contains("404")) ? Error.AssetLoadFailed : Error.AssetNotFound);
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
											loaded_assetBundle = www.get_assetBundle();
										}
										CrashlyticsReporter.SetLoadingBundle(string.Empty);
									}
									error_code = SaveAssetBundle(filename, request.hash, www.get_bytes());
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
									AssetBundleCreateRequest req = AssetBundle.LoadFromFileAsync(path);
									yield return (object)req;
									loaded_assetBundle = req.get_assetBundle();
								}
								CrashlyticsReporter.SetLoadingBundle(string.Empty);
								if (loaded_assetBundle == null)
								{
									Log.Warning(LOG.RESOURCE, "cached file load failed: {0}", filename);
									error_code = Error.AssetLoadFailed;
									if (File.Exists(path))
									{
										File.Delete(path);
									}
								}
								yield return (object)null;
							}
							if (request.progressObject != null)
							{
								request.progressObject = PROGRESS_COMPLATE;
							}
							while (isDownloadError && !request.internalMode)
							{
								yield return (object)null;
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
								if (www != null)
								{
									www.Dispose();
								}
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
									retry_count++;
									if (autoRetry)
									{
										retry_count = 0;
									}
									if (retry_count < 3)
									{
										yield return (object)new WaitForSeconds(1f);
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
											retry_count = 0;
											is_retry = true;
										}
										else
										{
											isDownloadError = true;
											int query_result = 0;
											if (request.IsValid() && onDownloadErrorQuery != null)
											{
												while (onDownloadErrorQuery != null)
												{
													query_result = onDownloadErrorQuery(true, error_code);
													if (query_result == 0)
													{
														break;
													}
													yield return (object)null;
												}
												while (query_result == 0 && onDownloadErrorQuery != null)
												{
													query_result = onDownloadErrorQuery(false, error_code);
													yield return (object)null;
												}
												if (query_result == -1)
												{
													cache.AddDelayUnloadAssetBundle(request.packageName, ref loaded_assetBundle);
													BreakRequest(ref request);
													yield break;
												}
											}
											isDownloadError = false;
											if (query_result != 1)
											{
												if (request.onError != null)
												{
													request.onError(request, ERROR_CODE.WWW_ERROR);
												}
											}
											else
											{
												retry_count = 0;
												is_retry = true;
											}
										}
									}
								}
								if (www != null)
								{
									www.Dispose();
								}
							}
						}
					}
				}
				while (is_retry);
				if (asset_bundle != null)
				{
					int i6 = 0;
					for (int n5 = load_num; i6 < n5; i6++)
					{
						if (request.master == null && request.sameRequests == null)
						{
							break;
						}
						if (asset_bundle == null)
						{
							break;
						}
						if (load_objects[i6] == null)
						{
							if (onAsyncLoadQuery())
							{
								while (IsWaitAssetBundleLoadAsync())
								{
									yield return (object)null;
								}
								loadingAssetCountFromAssetBundle++;
								AssetBundleRequest asset_bundle_load_asset = asset_bundle.LoadAssetAsync(request.resourceNames[i6]);
								while (!asset_bundle_load_asset.get_isDone())
								{
									yield return (object)null;
								}
								load_objects[i6] = ResourceObject.Get(request.category, request.resourceNames[i6], asset_bundle_load_asset.get_asset());
								loadingAssetCountFromAssetBundle--;
							}
							else
							{
								load_objects[i6] = ResourceObject.Get(request.category, request.resourceNames[i6], asset_bundle.LoadAsset(request.resourceNames[i6]));
							}
							if (load_objects[i6] != null)
							{
								loaded_num++;
							}
						}
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
				string sub_path;
				switch (ResourceDefine.types[(int)request.category])
				{
				case CATEGORY_TYPE.SINGLE:
				case CATEGORY_TYPE.PACK:
				case CATEGORY_TYPE.HASH256:
					sub_path = string.Empty;
					break;
				default:
					sub_path = request.packageName + "/";
					break;
				}
				if (request.category == RESOURCE_CATEGORY.PLAYER_HIGH_RESO_TEX)
				{
					string f3 = request.packageName.Substring(0, 3);
					string f2 = request.packageName.Substring(3, 2);
					if (!f3.Equals("WEP"))
					{
						f2 = (int.Parse(f2) / 10 * 10).ToString("00");
					}
					sub_path = f3 + "/" + f3 + f2 + "/" + sub_path;
				}
				for (int i7 = 0; i7 < load_num; i7++)
				{
					if (load_objects[i7] == null)
					{
						if (!request.internalMode && request.category != RESOURCE_CATEGORY.UI)
						{
							bool save_enableLoadDirect = enableLoadDirect;
							enableLoadDirect = true;
							Coroutine c = this.StartCoroutine(LoadDirect(request, sub_path + request.resourceNames[i7], delegate(Object o)
							{
								if (o != null)
								{
									((_003CDoLoad_003Ec__Iterator23D)/*Error near IL_177d: stateMachine*/)._003Cload_objects_003E__9[((_003CDoLoad_003Ec__Iterator23D)/*Error near IL_177d: stateMachine*/)._003Ci_003E__45] = ResourceObject.Get(((_003CDoLoad_003Ec__Iterator23D)/*Error near IL_177d: stateMachine*/).request.category, ((_003CDoLoad_003Ec__Iterator23D)/*Error near IL_177d: stateMachine*/).request.resourceNames[((_003CDoLoad_003Ec__Iterator23D)/*Error near IL_177d: stateMachine*/)._003Ci_003E__45], o);
								}
							}));
							enableLoadDirect = save_enableLoadDirect;
							yield return (object)c;
						}
						else if (request.category == RESOURCE_CATEGORY.UI)
						{
							string path3 = request.uiDep.path;
							if (onAsyncLoadQuery() || request.downloadOnly)
							{
								ResourceRequest req3 = Resources.LoadAsync(path3);
								while (!req3.get_isDone())
								{
									yield return (object)null;
								}
								load_objects[i7] = ResourceObject.Get(request.category, request.resourceNames[i7], req3.get_asset());
							}
							else
							{
								load_objects[i7] = ResourceObject.Get(request.category, request.resourceNames[i7], Resources.Load(path3));
							}
						}
						else
						{
							string path2 = $"Internal/internal__{request.category.ToString()}__{request.resourceNames[i7]}";
							if (onAsyncLoadQuery() || request.downloadOnly)
							{
								ResourceRequest req2 = Resources.LoadAsync(path2);
								while (!req2.get_isDone())
								{
									yield return (object)null;
								}
								load_objects[i7] = ResourceObject.Get(request.category, request.resourceNames[i7], req2.get_asset());
							}
							else
							{
								load_objects[i7] = ResourceObject.Get(request.category, request.resourceNames[i7], Resources.Load(path2));
							}
						}
						if (request.master == null && request.sameRequests == null)
						{
							break;
						}
						if (load_objects[i7] != null)
						{
							loaded_num++;
						}
					}
				}
				if (!use_package_cache && request.cachePackage)
				{
					load_package = PackageObject.Get(request.packageName, "dummy");
				}
			}
		}
		int enable_request_count = 0;
		if (request.master != null)
		{
			enable_request_count++;
		}
		if (request.sameRequests != null)
		{
			int i5 = 0;
			for (int n4 = request.sameRequests.Count; i5 < n4; i5++)
			{
				if (request.sameRequests[i5].master != null)
				{
					enable_request_count++;
				}
			}
		}
		bool need_unload_asset_bundle = asset_bundle != null;
		if (enable_request_count > 0)
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
				for (int i4 = 0; i4 < load_num; i4++)
				{
					if (!use_object_caches[i4] && load_objects[i4] != null)
					{
						object_cache_category.Add(request.resourceNames[i4], load_objects[i4]);
					}
				}
			}
			if (!request.downloadOnly && request.cachePackage && !string.IsNullOrEmpty(request.packageName) && !use_package_cache && load_package != null && cache.GetCachedPackage(request.packageName) == null)
			{
				cache.packageCaches.Add(request.packageName, load_package);
				need_unload_asset_bundle = false;
			}
			if (load_package != null && load_objects != null)
			{
				int j3 = 0;
				for (int m3 = load_objects.Length; j3 < m3; j3++)
				{
					if (load_objects[j3] != null)
					{
						load_package.refCount += enable_request_count;
						load_objects[j3].package = load_package;
					}
				}
				if (dependency_packages != null)
				{
					int j2 = 0;
					for (int m2 = dependency_packages.size; j2 < m2; j2++)
					{
						PackageObject pakobj = dependency_packages[j2];
						if (pakobj != null)
						{
							pakobj.refCount += enable_request_count;
							load_package.linkPackages.Add(pakobj);
						}
					}
				}
			}
		}
		if (need_unload_asset_bundle)
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
					int i3 = 0;
					for (int n3 = request.sameRequests.Count; i3 < n3; i3++)
					{
						LoadRequest same_request2 = request.sameRequests[i3];
						if (same_request2.onAtlasComplete != null && same_request2.category == RESOURCE_CATEGORY.UI_ATLAS)
						{
							same_request2.onAtlasComplete(same_request2, load_objects);
						}
						if (same_request2.master != null && same_request2.onComplate != null)
						{
							same_request2.onComplate(same_request2, load_objects);
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
					int j = 0;
					for (int i = request.sameRequests.Count; j < i; j++)
					{
						LoadRequest same_request = request.sameRequests[j];
						if (same_request.master != null && same_request.onError != null)
						{
							same_request.onError(same_request, ERROR_CODE.NOT_FOUND);
						}
					}
				}
			}
		}
		RemoveRequest(ref request);
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
			if (loadRequest.category == category && loadRequest.packageName == package_name)
			{
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
		}
		return false;
	}

	public bool IsCached(RESOURCE_CATEGORY category, string packageName)
	{
		return IsCached(category.ToAssetBundleName(packageName));
	}

	public bool IsCached(string dependency)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
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
		string[] array = aLLDependenciesFileName;
		foreach (string dependency in array)
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
		return IsCached(category, packageName) && IsCachedDependencies(category, packageName);
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

	private bool IsVersionCached(string filename, Hash128 hash)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		string cachePath = GetCachePath(filename, hash);
		return File.Exists(cachePath);
	}

	private string GetCachePath(string filename, Hash128 hash)
	{
		string cacheDir = GetCacheDir(filename);
		return Path.Combine(cacheDir, filename + "." + hash.ToString());
	}

	private string GetCacheDir(string filename)
	{
		string path = ((byte)(filename.GetHashCode() % 256)).ToString("X2");
		return Path.Combine(cacheDir, path);
	}

	private Error SaveAssetBundle(string filename, Hash128 hash, byte[] bytes)
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		if (bytes.Length != 0)
		{
			Error result = Error.None;
			string cacheDir = GetCacheDir(filename);
			if (Directory.Exists(cacheDir))
			{
				string[] files = Directory.GetFiles(cacheDir, filename + "*");
				string[] array = files;
				foreach (string path in array)
				{
					File.Delete(path);
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
		return Error.AssetLoadFailed;
	}

	public static IEnumerator ClearCache()
	{
		string[] dirs = Directory.GetDirectories(cacheDir);
		for (int i = 0; i < dirs.Length; i++)
		{
			string dir = dirs[i];
			Directory.Delete(dir, true);
			if (i % 2 == 0)
			{
				yield return (object)null;
			}
		}
	}

	public Object LoadDirect(RESOURCE_CATEGORY category, string resource_name)
	{
		if (!enableLoadDirect)
		{
			Log.Error(LOG.RESOURCE, "can not LoadDirect. " + category.ToString() + " : " + resource_name);
			return null;
		}
		Object val = null;
		string[] array = ResourceDefine.subPaths[(int)category];
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			string path = array[i] + resource_name;
			val = ExternalResources.Load<Object>(path);
			if (val != null)
			{
				break;
			}
		}
		return val;
	}

	public IEnumerator LoadDirect(LoadRequest request, string resource_name, Action<Object> onComplete)
	{
		RESOURCE_CATEGORY category = request.category;
		Object load_object = null;
		if (!enableLoadDirect)
		{
			Log.Error(LOG.RESOURCE, "can not LoadDirect. " + category.ToString() + " : " + resource_name);
			onComplete(load_object);
		}
		else
		{
			string[] paths = ResourceDefine.subPaths[(int)category];
			int j = 0;
			for (int i = paths.Length; j < i; j++)
			{
				string path2 = paths[j] + resource_name;
				if (path2.Contains("StreamingAssets"))
				{
					path2 = Application.get_streamingAssetsPath() + path2.Replace("StreamingAssets", string.Empty).ToLower();
					WWW www = request.progressObject = (object)new WWW(path2);
					while (!www.get_isDone())
					{
						yield return (object)null;
					}
					request.progressObject = PROGRESS_COMPLATE;
					load_object = www.get_assetBundle();
					www.Dispose();
				}
				else if (onAsyncLoadQuery() || request.downloadOnly)
				{
					yield return (object)ExternalResources.LoadAsync<Object>(path2, (Action<ResourceRequest>)delegate(ResourceRequest progress)
					{
						((_003CLoadDirect_003Ec__Iterator23F)/*Error near IL_01c2: stateMachine*/).request.progressObject = progress;
					}, (Action<Object>)delegate(Object asset)
					{
						((_003CLoadDirect_003Ec__Iterator23F)/*Error near IL_01ce: stateMachine*/).request.progressObject = PROGRESS_COMPLATE;
						((_003CLoadDirect_003Ec__Iterator23F)/*Error near IL_01ce: stateMachine*/)._003Cload_object_003E__1 = asset;
					});
				}
				else
				{
					yield return (object)null;
					load_object = ExternalResources.Load<Object>(path2);
				}
				if (load_object != null)
				{
					break;
				}
			}
			onComplete(load_object);
		}
	}

	public static Object LoadDirect(RESOURCE_CATEGORY category, string package_name, string resource_name)
	{
		if (!enableLoadDirect)
		{
			Log.Error(LOG.RESOURCE, "can not LoadDirect. " + category.ToString() + " : " + package_name + " : " + resource_name);
			return null;
		}
		Object val = null;
		string[] array = ResourceDefine.subPaths[(int)category];
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			string path = array[i] + package_name + "/" + resource_name;
			val = ExternalResources.Load<Object>(path);
			if (val != null)
			{
				break;
			}
		}
		return val;
	}

	public Object __LoadDirect(RESOURCE_CATEGORY category, string resource_name)
	{
		bool flag = enableLoadDirect;
		enableLoadDirect = true;
		Object result = LoadDirect(category, resource_name);
		enableLoadDirect = flag;
		return result;
	}

	public Object __LoadDirect(RESOURCE_CATEGORY category, string package_name, string resource_name)
	{
		bool flag = enableLoadDirect;
		enableLoadDirect = true;
		Object result = LoadDirect(category, package_name, resource_name);
		enableLoadDirect = flag;
		return result;
	}
}
