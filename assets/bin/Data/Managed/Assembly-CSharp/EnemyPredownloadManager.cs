using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPredownloadManager : MonoBehaviourSingleton<EnemyPredownloadManager>
{
	private class LoadInfo
	{
		public int iconId;

		public string modelname;

		public string mateName;

		public string animName;
	}

	public enum DownloadState
	{
		Init,
		Ready,
		CheckingFile,
		ReadyDownload,
		FinishChecking,
		Downloading,
		FinishDownload
	}

	private class LoadPackage
	{
		public RESOURCE_CATEGORY category;

		public string packageName;

		public float size;
	}

	public delegate void OnCheckDownloadFinish(bool avaiDownload);

	public delegate void OnCheckDownload();

	public delegate void OnDownLoadPackage();

	public delegate void OnStopDownloadPackage(bool succeed);

	private List<LoadInfo> LoadInfoList;

	private List<LoadPackage> loading_packages;

	private int Version;

	private bool isDownload;

	private bool isFN;

	private float totalSize;

	private float loadedSize;

	private bool stopFlags;

	private OnDownLoadPackage m_OnDownloadPackage = delegate
	{
	};

	private OnStopDownloadPackage m_OnStopDownloadPackage = delegate
	{
	};

	private OnCheckDownload m_OnCheckDownload = delegate
	{
	};

	private OnCheckDownloadFinish m_OnCheckDownLoadFinish = delegate
	{
	};

	private EnemyPredownloadTable enemyInfo;

	private string BackScene = "";

	public int totalCount
	{
		get;
		private set;
	}

	public int loadedCount
	{
		get;
		private set;
	}

	public int checkedCount
	{
		get;
		private set;
	}

	public int totalPackage
	{
		get;
		private set;
	}

	public DownloadState CurrentState
	{
		get;
		private set;
	}

	public bool isLoading
	{
		get
		{
			if (totalCount == 0)
			{
				return true;
			}
			return loadedCount < totalCount;
		}
	}

	public bool IsAvai
	{
		get;
		private set;
	}

	public float FileSize => Mathf.Clamp(totalSize - loadedSize, 0f, float.PositiveInfinity);

	public static void Stop()
	{
		if (MonoBehaviourSingleton<EnemyPredownloadManager>.IsValid())
		{
			MonoBehaviourSingleton<EnemyPredownloadManager>.I.SetStopFlag(is_stop: true);
		}
	}

	public bool IsAvaiDownload()
	{
		if (IsAvai)
		{
			return true;
		}
		if (CurrentState == DownloadState.Downloading || CurrentState == DownloadState.ReadyDownload)
		{
			if (totalCount == 0)
			{
				CurrentState = DownloadState.FinishChecking;
				return false;
			}
			return true;
		}
		return false;
	}

	protected override void Awake()
	{
		CurrentState = DownloadState.Init;
		checkedCount = 0;
		totalSize = 0f;
		loadedSize = 0f;
		totalCount = 0;
		loadedCount = 0;
		IsAvai = false;
		base.Awake();
	}

	private IEnumerator Start()
	{
		CurrentState = DownloadState.Ready;
		while (!MonoBehaviourSingleton<GameSceneManager>.IsValid())
		{
			yield return null;
		}
		while (string.IsNullOrEmpty(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName()))
		{
			yield return null;
		}
		while (!MonoBehaviourSingleton<UserInfoManager>.IsValid() || MonoBehaviourSingleton<UserInfoManager>.I.userStatus == null || !MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady)
		{
			yield return null;
		}
		bool flag = TutorialStep.HasAllTutorialCompleted() && (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.AFTER_MAINSTATUS) || MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.UPGRADE_ITEM));
		if ((MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "HomeScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "ClanScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "Lounge") || !flag)
		{
			MonoBehaviourSingleton<EnemyPredownloadManager>.I.enabled = false;
			yield break;
		}
		BackScene = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName();
		while (!Singleton<EnemyTable>.IsValid() || !Singleton<EnemyTable>.I.IsAvailable())
		{
			yield return null;
		}
		CheckDownload(delegate(bool isSucceed)
		{
			IsAvai = isSucceed;
		});
	}

	private void OnEnable()
	{
		if (CurrentState == DownloadState.Ready)
		{
			StartCoroutine(Start());
		}
		else if (CurrentState == DownloadState.CheckingFile)
		{
			StartCoroutine(ResumeCheckDownloadFiles());
		}
	}

	private void SetStopFlag(bool is_stop)
	{
		stopFlags = is_stop;
	}

	private IEnumerator LoadEnemyPredownloadData()
	{
		while (!MonoBehaviourSingleton<ResourceManager>.IsValid())
		{
			yield return null;
		}
		while (MonoBehaviourSingleton<ResourceManager>.I.sizeInfoManifest == null)
		{
			yield return null;
		}
		Hash128 hash = default(Hash128);
		if (MonoBehaviourSingleton<ResourceManager>.I.sizeInfoManifest != null)
		{
			hash = MonoBehaviourSingleton<ResourceManager>.I.sizeInfoManifest.GetAssetBundleHash(RESOURCE_CATEGORY.ASSETBUNDLEINFO.ToAssetBundleName());
		}
		if (hash.isValid)
		{
			LoadingQueue load_queue = new LoadingQueue(MonoBehaviourSingleton<AppMain>.I);
			LoadObject lo = load_queue.Load(RESOURCE_CATEGORY.ASSETBUNDLEINFO, "EnemyPredownloadTable");
			while (load_queue.IsLoading())
			{
				yield return null;
			}
			enemyInfo = (lo.loadedObject as EnemyPredownloadTable);
		}
	}

	private IEnumerator CheckDownloadFiles(Action<bool> onFinish = null)
	{
		CurrentState = DownloadState.CheckingFile;
		if (MonoBehaviourSingleton<ResourceManager>.I.sizeInfoManifest == null && !ResourceSizeInfo.IsValid())
		{
			yield return ResourceSizeInfo.ReInit();
		}
		yield return LoadEnemyPredownloadData();
		EnemyPredownloadTable table = enemyInfo;
		if (table == null)
		{
			yield break;
		}
		loading_packages = null;
		IsAvai = false;
		totalSize = 0f;
		loadedSize = 0f;
		totalCount = 0;
		loadedCount = 0;
		isDownload = false;
		if (table == null)
		{
			CurrentState = DownloadState.FinishChecking;
			isFN = true;
			onFinish?.Invoke(obj: false);
			if (m_OnCheckDownLoadFinish != null)
			{
				m_OnCheckDownLoadFinish(avaiDownload: false);
			}
			yield break;
		}
		int id = PlayerPrefs.GetInt("ENEMY_ASSET_VERSION", 0);
		if (id == table.Version)
		{
			CurrentState = DownloadState.FinishChecking;
			isFN = true;
			onFinish?.Invoke(obj: false);
			if (m_OnCheckDownLoadFinish != null)
			{
				m_OnCheckDownLoadFinish(avaiDownload: false);
			}
			yield break;
		}
		IsAvai = true;
		if (m_OnCheckDownLoadFinish != null)
		{
			m_OnCheckDownLoadFinish(avaiDownload: true);
		}
		Version = table.Version;
		totalPackage = table.EnemyDatas.Count;
		Type category_type = typeof(RESOURCE_CATEGORY);
		loading_packages = new List<LoadPackage>();
		checkedCount = 0;
		while (checkedCount < totalPackage)
		{
			EnemyPredownloadTable.Data data = table.EnemyDatas[checkedCount];
			RESOURCE_CATEGORY category = (RESOURCE_CATEGORY)Enum.Parse(category_type, data.categoryName);
			if (!MonoBehaviourSingleton<ResourceManager>.I.IsCached(category, data.packageName) && IsValidAssetURL(category, data.packageName))
			{
				LoadPackage loadPackage = new LoadPackage();
				loadPackage.category = category;
				loadPackage.packageName = data.packageName;
				loadPackage.size = ResourceSizeInfo.ConvertBToMB(data.Size);
				totalSize += loadPackage.size;
				loading_packages.Add(loadPackage);
			}
			checkedCount++;
			if (m_OnCheckDownload != null)
			{
				m_OnCheckDownload();
			}
			if (checkedCount % 5 == 0)
			{
				yield return null;
			}
			if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "HomeScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "EnemyDownloadScene")
			{
				MonoBehaviourSingleton<EnemyPredownloadManager>.I.enabled = false;
				yield break;
			}
		}
		if (loading_packages != null && loading_packages.Count > 0)
		{
			IsAvai = true;
		}
		Debug.Log("FINISH CHECK DONWLOAD FILES " + loading_packages.Count + " WITH SIZE " + totalSize);
		totalCount = loading_packages.Count;
		if (totalCount == 0)
		{
			PlayerPrefs.SetInt("ENEMY_ASSET_VERSION", id);
			isFN = true;
			IsAvai = false;
		}
		CurrentState = DownloadState.FinishChecking;
		if (totalCount > 0)
		{
			CurrentState = DownloadState.ReadyDownload;
		}
		onFinish?.Invoke(obj: true);
		if (m_OnCheckDownLoadFinish != null)
		{
			m_OnCheckDownLoadFinish(totalCount > 0);
		}
	}

	private IEnumerator ResumeCheckDownloadFiles()
	{
		CurrentState = DownloadState.CheckingFile;
		if (!ResourceSizeInfo.IsValid())
		{
			yield return ResourceSizeInfo.Init();
		}
		if (enemyInfo == null)
		{
			yield return LoadEnemyPredownloadData();
		}
		EnemyPredownloadTable table = enemyInfo;
		if (table == null)
		{
			yield break;
		}
		IsAvai = false;
		totalSize = 0f;
		loadedSize = 0f;
		totalCount = 0;
		loadedCount = 0;
		isDownload = false;
		if (table == null)
		{
			CurrentState = DownloadState.FinishChecking;
			isFN = true;
			if (m_OnCheckDownLoadFinish != null)
			{
				m_OnCheckDownLoadFinish(avaiDownload: false);
			}
			yield break;
		}
		int id = PlayerPrefs.GetInt("ENEMY_ASSET_VERSION", 0);
		if (id == table.Version)
		{
			CurrentState = DownloadState.FinishChecking;
			isFN = true;
			if (m_OnCheckDownLoadFinish != null)
			{
				m_OnCheckDownLoadFinish(avaiDownload: false);
			}
			yield break;
		}
		IsAvai = true;
		Version = table.Version;
		totalPackage = table.EnemyDatas.Count;
		Type category_type = typeof(RESOURCE_CATEGORY);
		while (checkedCount < totalPackage)
		{
			EnemyPredownloadTable.Data data = table.EnemyDatas[checkedCount];
			RESOURCE_CATEGORY category = (RESOURCE_CATEGORY)Enum.Parse(category_type, data.categoryName);
			if (!MonoBehaviourSingleton<ResourceManager>.I.IsCached(category, data.packageName) && IsValidAssetURL(category, data.packageName))
			{
				LoadPackage loadPackage = new LoadPackage();
				loadPackage.category = category;
				loadPackage.packageName = data.packageName;
				loadPackage.size = ResourceSizeInfo.ConvertBToMB(data.Size);
				totalSize += loadPackage.size;
				loading_packages.Add(loadPackage);
			}
			checkedCount++;
			if (m_OnCheckDownload != null)
			{
				m_OnCheckDownload();
			}
			if (checkedCount % 5 == 0)
			{
				yield return null;
			}
			if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "HomeScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "EnemyDownloadScene")
			{
				MonoBehaviourSingleton<EnemyPredownloadManager>.I.enabled = false;
				yield break;
			}
		}
		if (loading_packages != null && loading_packages.Count > 0)
		{
			IsAvai = true;
		}
		Debug.Log("FINISH CHECK DONWLOAD FILES " + loading_packages.Count + " WITH SIZE " + totalSize);
		totalCount = loading_packages.Count;
		if (totalCount == 0)
		{
			PlayerPrefs.SetInt("ENEMY_ASSET_VERSION", id);
			isFN = true;
			IsAvai = false;
		}
		CurrentState = DownloadState.FinishChecking;
		if (totalCount > 0)
		{
			CurrentState = DownloadState.ReadyDownload;
		}
		if (m_OnCheckDownLoadFinish != null)
		{
			m_OnCheckDownLoadFinish(totalCount > 0);
		}
	}

	private IEnumerator StartDownload()
	{
		if (loading_packages == null)
		{
			if (m_OnStopDownloadPackage != null)
			{
				m_OnStopDownloadPackage(succeed: false);
			}
			yield break;
		}
		isDownload = true;
		CurrentState = DownloadState.Downloading;
		Debug.Log("START LOAD ENEMY DATA ");
		LoadingQueue loading_queue = new LoadingQueue(this);
		yield return new WaitForSeconds(0.5f);
		if (totalCount > 0)
		{
			List<LoadPackage> load_packages = loading_packages;
			List<LoadObject> loading_list = new List<LoadObject>();
			int count = 0;
			if (load_packages != null)
			{
				bool waitOne = false;
				while (loadedCount < totalCount)
				{
					yield return null;
					if (stopFlags)
					{
						while (loading_list.Count > 0)
						{
							int i = 0;
							for (int num = loading_list.Count; i < num; i++)
							{
								if (!loading_list[i].isLoading)
								{
									loading_list[i] = null;
									loading_list.RemoveAt(i);
									i--;
									num--;
									loadedCount++;
								}
							}
							yield return null;
						}
						yield return MonoBehaviourSingleton<AppMain>.I.ClearMemory(clearObjCaches: false, clearPreloaded: true);
						isDownload = false;
						if (loadedCount == totalCount)
						{
							PlayerPrefs.SetInt("ENEMY_ASSET_VERSION", Version);
							isFN = true;
							IsAvai = false;
							if (m_OnStopDownloadPackage != null)
							{
								m_OnStopDownloadPackage(succeed: true);
							}
						}
						else if (m_OnStopDownloadPackage != null)
						{
							m_OnStopDownloadPackage(succeed: false);
						}
						yield break;
					}
					if (count % 100 == 0 && waitOne)
					{
						if (loading_list.Count == 0)
						{
							yield return MonoBehaviourSingleton<AppMain>.I.ClearMemory(clearObjCaches: false, clearPreloaded: false);
							waitOne = false;
						}
					}
					else
					{
						while (loading_list.Count < 10 && count < totalCount)
						{
							LoadPackage loadPackage = load_packages[count];
							bool enableCache = ResourceManager.enableCache;
							bool downloadOnly = ResourceManager.downloadOnly;
							bool internalMode = ResourceManager.internalMode;
							ResourceManager.enableCache = false;
							ResourceManager.downloadOnly = true;
							ResourceManager.internalMode = false;
							if (!string.IsNullOrEmpty(loadPackage.packageName))
							{
								LoadObject item = loading_queue.LoadAssetBundleToCache(loadPackage.category, loadPackage.packageName, cache_package: true);
								loading_list.Add(item);
							}
							ResourceManager.enableCache = enableCache;
							ResourceManager.downloadOnly = downloadOnly;
							ResourceManager.internalMode = internalMode;
							waitOne = true;
							count++;
							if (count % 100 == 0)
							{
								break;
							}
						}
					}
					int j = 0;
					for (int num2 = loading_list.Count; j < num2; j++)
					{
						if (!loading_list[j].isLoading)
						{
							loading_list[j] = null;
							loading_list.RemoveAt(j);
							j--;
							num2--;
							loadedCount++;
							if (m_OnDownloadPackage != null)
							{
								m_OnDownloadPackage();
							}
						}
					}
				}
			}
			Debug.Log("FINISH LOAD ENEMY DATA " + loadedCount);
		}
		int version = Version;
		PlayerPrefs.SetInt("ENEMY_ASSET_VERSION", version);
		yield return MonoBehaviourSingleton<AppMain>.I.ClearMemory(clearObjCaches: false, clearPreloaded: true);
		Debug.Log("END LOAD");
		isDownload = false;
		isFN = true;
		IsAvai = false;
		CurrentState = DownloadState.FinishDownload;
		if (BackScene == "ClanScene")
		{
			MonoBehaviourSingleton<GameSceneManager>.I.AddHighForceChangeScene("Clan", "ClanTop");
		}
		else if (BackScene == "Lounge")
		{
			MonoBehaviourSingleton<GameSceneManager>.I.AddHighForceChangeScene("Lounge", "LoungeTop");
		}
		if (m_OnStopDownloadPackage != null)
		{
			m_OnStopDownloadPackage(succeed: true);
		}
		yield return new WaitForSeconds(0.5f);
		MonoBehaviourSingleton<AppMain>.I.Reset();
	}

	private float GetPackageTotalSizeMB(string[] loadPackageName)
	{
		if (!ResourceSizeInfo.IsValid())
		{
			return 0f;
		}
		return ResourceSizeInfo.GetAssetsSizeMB(loadPackageName);
	}

	public void CheckDownload(Action<bool> callback)
	{
		StartCoroutine(CheckDownloadFiles(callback));
	}

	public void ProceedDownload()
	{
		SetStopFlag(is_stop: false);
		if (!isDownload)
		{
			StartCoroutine(StartDownload());
		}
	}

	private bool IsValidAssetURL(RESOURCE_CATEGORY category, string packageName)
	{
		if (MonoBehaviourSingleton<ResourceManager>.I == null || MonoBehaviourSingleton<ResourceManager>.I.manifest == null)
		{
			return false;
		}
		string bundleName = category.ToAssetBundleName(packageName);
		if (MonoBehaviourSingleton<ResourceManager>.I.manifest.GetAssetBundleHash(MonoBehaviourSingleton<GoGameResourceManager>.I.GetFullBundleName(bundleName)).isValid)
		{
			return true;
		}
		return false;
	}

	public void AddListenerDownload(OnDownLoadPackage onDownload = null, OnStopDownloadPackage onStop = null)
	{
		m_OnDownloadPackage = (OnDownLoadPackage)Delegate.Combine(m_OnDownloadPackage, onDownload);
		m_OnStopDownloadPackage = (OnStopDownloadPackage)Delegate.Combine(m_OnStopDownloadPackage, onStop);
	}

	public void RemoveListenerDownload(OnDownLoadPackage onDownload = null, OnStopDownloadPackage onStop = null)
	{
		m_OnDownloadPackage = (OnDownLoadPackage)Delegate.Remove(m_OnDownloadPackage, onDownload);
		m_OnStopDownloadPackage = (OnStopDownloadPackage)Delegate.Remove(m_OnStopDownloadPackage, onStop);
	}

	public void AddListenerCheck(OnCheckDownload onCheckDownload, OnCheckDownloadFinish onCheckDownloadFinish)
	{
		m_OnCheckDownload = (OnCheckDownload)Delegate.Combine(m_OnCheckDownload, onCheckDownload);
		m_OnCheckDownLoadFinish = (OnCheckDownloadFinish)Delegate.Combine(m_OnCheckDownLoadFinish, onCheckDownloadFinish);
	}

	public void RemoveListenerCheck(OnCheckDownload onCheckDownload, OnCheckDownloadFinish onCheckDownloadFinish)
	{
		m_OnCheckDownload = (OnCheckDownload)Delegate.Remove(m_OnCheckDownload, onCheckDownload);
		m_OnCheckDownLoadFinish = (OnCheckDownloadFinish)Delegate.Remove(m_OnCheckDownLoadFinish, onCheckDownloadFinish);
	}
}
