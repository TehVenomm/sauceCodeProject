using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppMain : MonoBehaviourSingleton<AppMain>
{
	public enum LocalNotificationType
	{
		GUILD_REQUEST_1 = 0,
		GUILD_REQUEST_2 = 1,
		GUILD_REQUEST_3 = 2,
		GUILD_REQUEST_4 = 3,
		GUILD_REQUEST_5 = 4,
		IAP_BUNDLE = 900001,
		BLACK_MARKET = 910001
	}

	public const int BASE_RESOLUTION_HEIGHT = 854;

	public const int BASE_RESOLUTION_HEIGHT_HIGH = 1280;

	public string startScene = "Title";

	public Action onDelayCall;

	private int periodicGCCollectCount;

	private List<DateTime> localNotificationGuildRequestTime = new List<DateTime>(5);

	private DateTime localPurchaseItemListRequestTime;

	private bool changedResolution;

	private IEnumerator changedResolutionWork;

	private bool showingTableLoadError;

	private static Version appVer;

	public int defaultScreenWidth
	{
		get;
		private set;
	}

	public int defaultScreenHeight
	{
		get;
		private set;
	}

	public int mainScreenWidth
	{
		get;
		private set;
	}

	public int mainScreenHeight
	{
		get;
		private set;
	}

	public static bool isApplicationQuit
	{
		get;
		private set;
	}

	public static bool isReset
	{
		get;
		private set;
	}

	public static bool isInitialized
	{
		get;
		private set;
	}

	public static string appStr
	{
		get;
		set;
	}

	public Camera mainCamera
	{
		get;
		private set;
	}

	public Transform mainCameraTransform
	{
		get;
		private set;
	}

	public bool enablePeriodicGCCollect
	{
		get;
		set;
	}

	public bool isExecutingClearMemory
	{
		get;
		private set;
	}

	public bool isExecutingUnloadUnusedAssets
	{
		get;
		private set;
	}

	public int frameExecutedUnloadUnusedAssets
	{
		get;
		private set;
	}

	public static void Startup()
	{
		string text = "Startup()";
		Application.targetFrameRate = 30;
		ShaderGlobal.Initialize();
		EffectManager.Startup();
	}

	public void SetMainCamera(Camera _camera)
	{
		mainCamera = _camera;
		if ((UnityEngine.Object)_camera != (UnityEngine.Object)null)
		{
			mainCameraTransform = _camera.transform;
		}
		else
		{
			mainCameraTransform = null;
		}
	}

	public void InitCollideLayers()
	{
		Utility.SetAllNotCollideLayers();
		Utility.SetCollideLayers(8, 9, 19, 18, 17, 15, 13, 11, 2);
		Utility.SetCollideLayers(20, 19, 15, 13, 2);
		Utility.SetCollideLayers(10, 9, 18, 17);
		Utility.SetCollideLayers(11, 14, 12);
		Utility.SetCollideLayers(13, 18);
		Utility.SetCollideLayers(14, 9, 18, 21);
		Utility.SetCollideLayers(30, 14, 12);
		Utility.SetCollideLayers(15, 9, 18, 21);
		Utility.SetCollideLayers(23, 23);
		Utility.SetCollideLayers(29, 9, 18, 17);
		Utility.SetCollideLayers(31, 8, 12, 14);
		Utility.SetCollideLayers(31, 13, 15, 10);
		Utility.SetCollideLayers(31, 18, 9, 17, 21);
		Utility.SetCollideLayers(16, 17, 9, 18);
	}

	protected override void Awake()
	{
		base.Awake();
		UnityEngine.Object.DontDestroyOnLoad(this);
		CrashlyticsReporter.EnableReport();
		this.defaultScreenWidth = Screen.width;
		defaultScreenHeight = Screen.height;
		if (this.defaultScreenWidth > defaultScreenHeight)
		{
			int defaultScreenWidth = this.defaultScreenWidth;
			this.defaultScreenWidth = defaultScreenHeight;
			defaultScreenHeight = defaultScreenWidth;
		}
		SetupScreen();
		UpdateResolution(Screen.width < Screen.height);
		TitleTop.isFirstBoot = true;
		GC.Collect();
		long totalMemory = GC.GetTotalMemory(false);
		int num = (int)(52428800 - totalMemory);
		int num2 = Mathf.Max(num / 1024, 1);
		object[] array = new object[1024];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = new byte[num2];
		}
		array = null;
		GC.Collect();
	}

	public void SetupScreen()
	{
		if (defaultScreenHeight > 854)
		{
			int num = (defaultScreenHeight <= 1280) ? 854 : 1280;
			mainScreenWidth = (int)((float)defaultScreenWidth * ((float)num / (float)defaultScreenHeight) + 0.1f);
			mainScreenHeight = num;
		}
		else
		{
			mainScreenWidth = defaultScreenWidth;
			mainScreenHeight = defaultScreenHeight;
		}
	}

	private void Update()
	{
		if (enablePeriodicGCCollect && ++periodicGCCollectCount >= 30)
		{
			periodicGCCollectCount = 0;
			GC.Collect();
		}
		if (onDelayCall != null)
		{
			if (!isApplicationQuit)
			{
				onDelayCall();
			}
			onDelayCall = null;
		}
	}

	private void Start()
	{
		Screen.sleepTimeout = -1;
		isInitialized = false;
		isApplicationQuit = false;
		isReset = false;
		showingTableLoadError = false;
		Startup();
		InitCollideLayers();
		if (Screen.orientation == ScreenOrientation.Unknown || Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
		{
			Screen.orientation = ScreenOrientation.Portrait;
		}
		else
		{
			Screen.orientation = ScreenOrientation.PortraitUpsideDown;
		}
		Utility.Initialize();
		Temporary.Initialize();
		Protocol.Initialize();
		HomeSelfCharacter.CTRL = true;
		appVer = NetworkNative.getNativeVersionFromName();
		GameObject gameObject = base.gameObject;
		gameObject.AddComponent<GoWrapManager>();
		gameObject.AddComponent<FCMManager>();
		gameObject.AddComponent<DefaultTimeUpdater>();
		gameObject.AddComponent<ResourceManager>();
		MonoBehaviourSingleton<ResourceManager>.I.onAsyncLoadQuery = onAsyncLoadQuery;
		gameObject.AddComponent<InstantiateManager>();
		DataTableManager dataTableManager = new GameObject("DataTableManager").AddComponent<DataTableManager>();
		dataTableManager.transform.parent = base._transform;
		dataTableManager.onError += OnTableDownloadError;
		ResourceManager.enableLoadDirect = false;
		CreateDefaultCamera();
		gameObject.AddComponent<ScreenOrientationManager>();
		MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
		UpdateResolution(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		Utility.CreateGameObjectAndComponent("AudioListenerManager", MonoBehaviourSingleton<AppMain>.I._transform, -1);
		Utility.CreateGameObjectAndComponent("SoundManager", base._transform, -1);
		Utility.CreateGameObjectAndComponent("AudioObjectPool", base._transform, -1);
		Utility.CreateGameObjectAndComponent("EffectManager", base._transform, -1);
		gameObject.AddComponent<NetworkManager>();
		gameObject.AddComponent<ProtocolManager>();
		gameObject.AddComponent<AccountManager>();
		gameObject.AddComponent<TimeManager>();
		gameObject.AddComponent<GoGameTimeManager>();
		Utility.CreateGameObjectAndComponent("NativeReceiver", base._transform, -1);
		Utility.CreateGameObjectAndComponent("ShopReceiver", base._transform, -1);
		Utility.CreateGameObjectAndComponent("ChatManager", base._transform, -1);
		Utility.CreateGameObjectAndComponent("NativeShare", base._transform, -1);
		gameObject.AddComponent<CoopApp>();
		gameObject.AddComponent<BootProcess>();
	}

	public void UpdateResolution(bool is_portrait)
	{
		bool flag = false;
		if (GameSaveData.instance != null)
		{
			flag = (GameSaveData.instance.graphicOptionKey == "low");
		}
		int num;
		int num2;
		if (flag)
		{
			num = mainScreenWidth;
			num2 = mainScreenHeight;
			changedResolution = true;
		}
		else
		{
			if (!changedResolution)
			{
				return;
			}
			num = defaultScreenWidth;
			num2 = defaultScreenHeight;
		}
		if (!is_portrait)
		{
			int num3 = num;
			num = num2;
			num2 = num3;
		}
		if (changedResolutionWork != null)
		{
			StopCoroutine(changedResolutionWork);
		}
		changedResolutionWork = _UpdateResolution(num, num2);
		StartCoroutine(changedResolutionWork);
	}

	private IEnumerator _UpdateResolution(int w, int h)
	{
		UIRenderTexture[] renderTextures = base.gameObject.GetComponentsInChildren<UIRenderTexture>(true);
		int l = 0;
		for (int k = renderTextures.Length; l < k; l++)
		{
			renderTextures[l].enabled = false;
		}
		Screen.SetResolution(w, h, true);
		yield return (object)new WaitForEndOfFrame();
		yield return (object)new WaitForEndOfFrame();
		int j = 0;
		for (int i = renderTextures.Length; j < i; j++)
		{
			renderTextures[j].enabled = true;
		}
		changedResolutionWork = null;
	}

	private void OnScreenRotate(bool is_portrait)
	{
		UpdateResolution(is_portrait);
	}

	public void OnLoadFinished()
	{
		isInitialized = true;
	}

	private void CreateDefaultCamera()
	{
		if ((UnityEngine.Object)Camera.main == (UnityEngine.Object)null)
		{
			ResourceUtility.Realizes(Resources.Load("System/DefaultMainCamera"), base._transform, -1);
		}
	}

	private void OnApplicationPause(bool pause_status)
	{
		if (pause_status)
		{
			GameSaveData.Save();
			Screen.sleepTimeout = -2;
			Native.CancelAllLocalNotification();
			RegisterLocalNotify();
		}
		else
		{
			Screen.sleepTimeout = -1;
			if (isInitialized && !CheckInvitedClanBySNS() && !CheckInvitedPartyBySNS() && !CheckInvitedLoungeBySNS() && !CheckMutualFollowBySNS())
			{
				if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "HomeTop" || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "LoungeTop")
				{
					Protocol.Force(delegate
					{
						MonoBehaviourSingleton<PartyManager>.I.SendInvitedParty(delegate
						{
						}, true);
					});
				}
				Native.CancelAllLocalNotification();
			}
		}
	}

	private bool CheckInvitedClanBySNS()
	{
		string @string = PlayerPrefs.GetString("ic");
		if (!string.IsNullOrEmpty(@string))
		{
			PlayerPrefs.SetString("ic", string.Empty);
			if (MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && TutorialStep.HasAllTutorialCompleted())
			{
				MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent(null);
			}
			string name = "MAIN_MENU_HOME";
			if (LoungeMatchingManager.IsValidInLounge())
			{
				name = "MAIN_MENU_LOUNGE";
			}
			EventData[] autoEvents = new EventData[4]
			{
				new EventData(name, null),
				new EventData("GUILD", null),
				new EventData("SEARCH", null),
				new EventData("INFO", 97)
			};
			if (TutorialStep.HasAllTutorialCompleted())
			{
				MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
				return true;
			}
		}
		return false;
	}

	private bool CheckInvitedPartyBySNS()
	{
		string @string = PlayerPrefs.GetString("im");
		if (!string.IsNullOrEmpty(@string))
		{
			MonoBehaviourSingleton<PartyManager>.I.InviteValue = @string;
			PlayerPrefs.SetString("im", string.Empty);
			if (MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && TutorialStep.HasAllTutorialCompleted())
			{
				MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent(null);
			}
			string name = "MAIN_MENU_HOME";
			if (LoungeMatchingManager.IsValidInLounge())
			{
				name = "MAIN_MENU_LOUNGE";
			}
			EventData[] autoEvents = new EventData[3]
			{
				new EventData(name, null),
				new EventData("GACHA_QUEST_COUNTER", null),
				new EventData("INVITED_ROOM", null)
			};
			if (TutorialStep.HasAllTutorialCompleted())
			{
				MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
				return true;
			}
		}
		return false;
	}

	private bool CheckInvitedLoungeBySNS()
	{
		string @string = PlayerPrefs.GetString("il");
		if (!string.IsNullOrEmpty(@string))
		{
			MonoBehaviourSingleton<LoungeMatchingManager>.I.InviteValue = @string;
			PlayerPrefs.SetString("il", string.Empty);
			if (MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && TutorialStep.HasAllTutorialCompleted())
			{
				MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent(null);
			}
			if (!TutorialStep.HasAllTutorialCompleted())
			{
				return false;
			}
			if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < 15)
			{
				return false;
			}
			EventData[] array = null;
			if (MonoBehaviourSingleton<LoungeManager>.IsValid())
			{
				StartCoroutine(SetAutoEventLoungeToLounge(@string));
			}
			else
			{
				array = new EventData[3]
				{
					new EventData("MAIN_MENU_HOME", null),
					new EventData("LOUNGE", null),
					new EventData("INVITED_LOUNGE", null)
				};
				MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array);
			}
			return true;
		}
		return false;
	}

	private IEnumerator SetAutoEventLoungeToLounge(string inviteLoungeValue)
	{
		while (!LoungeMatchingManager.IsValidInLounge())
		{
			yield return (object)null;
		}
		string[] values = inviteLoungeValue.Split('_');
		if (!(values[0] == MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.loungeNumber))
		{
			EventData[] auto_event_data = new EventData[5]
			{
				new EventData("MAIN_MENU_LOUNGE", null),
				new EventData("LOUNGE_SETTINGS", null),
				new EventData("EXIT", null),
				new EventData("LOUNGE", null),
				new EventData("INVITED_LOUNGE", null)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(auto_event_data);
		}
	}

	private bool CheckMutualFollowBySNS()
	{
		string @string = PlayerPrefs.GetString("fc");
		if (!string.IsNullOrEmpty(@string))
		{
			MonoBehaviourSingleton<FriendManager>.I.MutualFollowValue = @string;
			PlayerPrefs.SetString("fc", string.Empty);
			if (MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && TutorialStep.HasAllTutorialCompleted())
			{
				MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent(null);
			}
			string name = "MAIN_MENU_HOME";
			if (LoungeMatchingManager.IsValidInLounge())
			{
				name = "MAIN_MENU_LOUNGE";
			}
			EventData[] autoEvents = new EventData[6]
			{
				new EventData(name, null),
				new EventData("MUTUAL_FOLLOW", null),
				new EventData("MAIN_MENU_MENU", null),
				new EventData("FRIEND", null),
				new EventData("FOLLOW_LIST", null),
				new EventData("MUTUAL_FOLLOW_MESSAGE", null)
			};
			if (TutorialStep.HasAllTutorialCompleted())
			{
				PlayerPrefs.SetString("fc", string.Empty);
				MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
				return true;
			}
		}
		return false;
	}

	private void OnApplicationQuit()
	{
		isApplicationQuit = true;
		GameSaveData.Save();
		Native.CancelAllLocalNotification();
		RegisterLocalNotify();
	}

	private bool onAsyncLoadQuery()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.IsValid())
		{
			GameSection currentScene = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentScene();
			if ((UnityEngine.Object)currentScene != (UnityEngine.Object)null && currentScene is InGameScene)
			{
				GameSection currentSection = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection();
				if (MonoBehaviourSingleton<InGameProgress>.IsValid() && (UnityEngine.Object)currentSection != (UnityEngine.Object)null && currentSection is InGameMain)
				{
					return MonoBehaviourSingleton<InGameProgress>.I.isBattleStart;
				}
			}
		}
		return true;
	}

	public Coroutine ClearMemory(bool clearObjCaches, bool clearPreloaded)
	{
		return StartCoroutine(DoClearMemory(clearObjCaches, clearPreloaded));
	}

	private IEnumerator DoClearMemory(bool clearObjCaches, bool clearPreloaded)
	{
		while (isExecutingClearMemory)
		{
			yield return (object)null;
		}
		isExecutingClearMemory = true;
		if (MonoBehaviourSingleton<ResourceManager>.IsValid())
		{
			while (!MonoBehaviourSingleton<ResourceManager>.I.isAllStay)
			{
				yield return (object)null;
			}
			if (clearObjCaches)
			{
				MonoBehaviourSingleton<ResourceManager>.I.cache.ClearPackageCaches();
				MonoBehaviourSingleton<ResourceManager>.I.cache.ClearObjectCaches(clearPreloaded);
			}
			MonoBehaviourSingleton<ResourceManager>.I.cache.ClearSENameDictionary();
		}
		ClearPoolObjects();
		yield return (object)UnloadUnusedAssets(true);
		isExecutingClearMemory = false;
	}

	public Coroutine ClearEnemyAssets()
	{
		return StartCoroutine(DoClearEnemyAssets());
	}

	private IEnumerator DoClearEnemyAssets()
	{
		while (isExecutingClearMemory)
		{
			yield return (object)null;
		}
		isExecutingClearMemory = true;
		if (MonoBehaviourSingleton<ResourceManager>.IsValid())
		{
			while (!MonoBehaviourSingleton<ResourceManager>.I.isAllStay)
			{
				yield return (object)null;
			}
			MonoBehaviourSingleton<ResourceManager>.I.cache.ClearObjectCaches(new RESOURCE_CATEGORY[8]
			{
				RESOURCE_CATEGORY.ENEMY_ANIM,
				RESOURCE_CATEGORY.ENEMY_CAMERA,
				RESOURCE_CATEGORY.ENEMY_ICON,
				RESOURCE_CATEGORY.ENEMY_ICON_ITEM,
				RESOURCE_CATEGORY.ENEMY_MATERIAL,
				RESOURCE_CATEGORY.ENEMY_MODEL,
				RESOURCE_CATEGORY.EFFECT_ACTION,
				RESOURCE_CATEGORY.EFFECT_TEX
			});
		}
		EffectManager.ClearPoolObjects();
		EnemyLoader.ClearPoolObjects();
		yield return (object)UnloadUnusedAssets(true);
		isExecutingClearMemory = false;
	}

	public Coroutine UnloadUnusedAssets(bool need_gc_collect)
	{
		return StartCoroutine(DoUnloadUnusedAssets(need_gc_collect));
	}

	private IEnumerator DoUnloadUnusedAssets(bool need_gc_collect)
	{
		while (isExecutingUnloadUnusedAssets)
		{
			yield return (object)null;
		}
		isExecutingUnloadUnusedAssets = true;
		frameExecutedUnloadUnusedAssets = Time.frameCount;
		if (need_gc_collect)
		{
			GC.Collect();
			yield return (object)new WaitForEndOfFrame();
			yield return (object)new WaitForEndOfFrame();
			yield return (object)new WaitForEndOfFrame();
		}
		yield return (object)Resources.UnloadUnusedAssets();
		isExecutingUnloadUnusedAssets = false;
		yield return (object)new WaitForEndOfFrame();
		yield return (object)new WaitForEndOfFrame();
		yield return (object)new WaitForEndOfFrame();
	}

	public void ClearPoolObjects()
	{
		EffectManager.ClearPoolObjects();
		CoopNetworkManager.ClearPoolObjects();
		ChatNetworkManager.ClearPoolObjects();
		TargetMarkerManager.ClearPoolObjects();
		EnemyLoader.ClearPoolObjects();
		InstantiateManager.ClearPoolObjects();
		ResourceObject.ClearPoolObjects();
		PackageObject.ClearPoolObjects();
		DelayUnloadAssetBundle.ClearPoolObjects();
	}

	public void Reset()
	{
		Reset(false, false);
	}

	public void Reset(bool need_clear_cache, bool need_predownload)
	{
		if (MonoBehaviourSingleton<ResourceManager>.IsValid())
		{
			StartCoroutine(DoReset(need_clear_cache, need_predownload));
		}
	}

	private IEnumerator DoReset(bool need_clear_cache, bool need_predownload)
	{
		isReset = true;
		if (MonoBehaviourSingleton<UIManager>.IsValid())
		{
			MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.RESET, true);
		}
		if (MonoBehaviourSingleton<TransitionManager>.IsValid())
		{
			yield return (object)MonoBehaviourSingleton<TransitionManager>.I.Out(TransitionManager.TYPE.BLACK);
		}
		if (MonoBehaviourSingleton<SoundManager>.IsValid() && MonoBehaviourSingleton<SoundManager>.I.playingBGMID != 0)
		{
			MonoBehaviourSingleton<SoundManager>.I.requestBGMID = 0;
			MonoBehaviourSingleton<SoundManager>.I.fadeOutTime = 1f;
			yield return (object)new WaitForSeconds(1f);
		}
		if (MonoBehaviourSingleton<PredownloadManager>.IsValid())
		{
			UnityEngine.Object.DestroyImmediate(MonoBehaviourSingleton<PredownloadManager>.I);
		}
		if (MonoBehaviourSingleton<LoadingProcess>.IsValid())
		{
			UnityEngine.Object.DestroyImmediate(MonoBehaviourSingleton<LoadingProcess>.I);
		}
		if (MonoBehaviourSingleton<DataTableManager>.IsValid())
		{
			MonoBehaviourSingleton<DataTableManager>.I.Clear();
			UnityEngine.Object.DestroyImmediate(MonoBehaviourSingleton<DataTableManager>.I);
		}
		if (MonoBehaviourSingleton<ResourceManager>.IsValid())
		{
			MonoBehaviourSingleton<ResourceManager>.I.CancelAll();
			while (MonoBehaviourSingleton<ResourceManager>.I.isLoading)
			{
				yield return (object)null;
			}
		}
		MonoBehaviourSingleton<ResourceManager>.I.Reset();
		SceneManager.LoadScene("Empty");
		if (need_clear_cache)
		{
			if (Singleton<StringTable>.IsValid() && MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.loading != (UnityEngine.Object)null)
			{
				MonoBehaviourSingleton<UIManager>.I.loading.ShowSystemMessage(StringTable.Get(STRING_CATEGORY.COMMON, 20u));
			}
			yield return (object)UnloadUnusedAssets(true);
			PlayerPrefs.SetInt("AppMain.Reset", (need_clear_cache ? 1 : 0) | (need_predownload ? 2 : 0));
			yield return (object)StartCoroutine(ResourceManager.ClearCache());
			yield return (object)new WaitForSeconds(1f);
			if (need_predownload && Singleton<StringTable>.IsValid() && MonoBehaviourSingleton<UIManager>.IsValid() && (UnityEngine.Object)MonoBehaviourSingleton<UIManager>.I.loading != (UnityEngine.Object)null)
			{
				base.gameObject.AddComponent<PredownloadManager>();
				while (MonoBehaviourSingleton<PredownloadManager>.I.totalCount == 0)
				{
					yield return (object)null;
				}
				int total = MonoBehaviourSingleton<PredownloadManager>.I.totalCount;
				int count = -1;
				while (count < total)
				{
					if (count != MonoBehaviourSingleton<PredownloadManager>.I.loadedCount)
					{
						count = MonoBehaviourSingleton<PredownloadManager>.I.loadedCount;
						MonoBehaviourSingleton<UIManager>.I.loading.ShowSystemMessage(StringTable.Format(STRING_CATEGORY.COMMON, 21u, count, total));
					}
					yield return (object)null;
				}
				MonoBehaviourSingleton<UIManager>.I.loading.ShowSystemMessage(null);
				MonoBehaviourSingleton<ResourceManager>.I.Reset();
			}
			PlayerPrefs.SetInt("AppMain.Reset", 0);
		}
		base.gameObject.BroadcastMessage("OnApplicationQuit", SendMessageOptions.DontRequireReceiver);
		MonoBehaviour[] monos = GetComponents<MonoBehaviour>();
		for (int i = base._transform.childCount - 1; i >= 0; i--)
		{
			base._transform.GetChild(i).gameObject.SetActive(false);
		}
		MonoBehaviour[] array = monos;
		foreach (MonoBehaviour mono in array)
		{
			if ((UnityEngine.Object)mono != (UnityEngine.Object)this)
			{
				mono.enabled = false;
			}
		}
		SetMainCamera(null);
		for (int j = base._transform.childCount - 1; j >= 0; j--)
		{
			GameObject game_object = base._transform.GetChild(j).gameObject;
			UnityEngine.Object.DestroyImmediate(game_object);
		}
		MonoBehaviour[] array2 = monos;
		foreach (MonoBehaviour mono2 in array2)
		{
			if ((UnityEngine.Object)mono2 != (UnityEngine.Object)this)
			{
				UnityEngine.Object.DestroyImmediate(mono2);
			}
		}
		CreateDefaultCamera();
		yield return (object)new WaitForEndOfFrame();
		yield return (object)new WaitForEndOfFrame();
		yield return (object)new WaitForEndOfFrame();
		SingletonBase.RemoveAllInstance();
		yield return (object)Resources.UnloadUnusedAssets();
		GC.Collect();
		yield return (object)new WaitForEndOfFrame();
		yield return (object)new WaitForEndOfFrame();
		yield return (object)new WaitForEndOfFrame();
		MonoBehaviourSingleton<AppMain>.I.startScene = "Title";
		Start();
	}

	public void OnTableDownloadError(DataTableLoadError error, Action retry)
	{
		if (!showingTableLoadError)
		{
			showingTableLoadError = true;
			Error error2;
			switch (error)
			{
			case DataTableLoadError.AssetNotFoundError:
				error2 = Error.AssetNotFound;
				break;
			case DataTableLoadError.FileWriteError:
				error2 = Error.AssetSaveFailed;
				break;
			case DataTableLoadError.VerifyError:
				error2 = Error.AssetVerifyFailed;
				break;
			default:
				error2 = Error.AssetLoadFailed;
				break;
			}
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, StringTable.GetErrorMessage((uint)error2), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 110u), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 111u), null, null), delegate(string btn)
			{
				showingTableLoadError = false;
				if (btn == "YES")
				{
					retry();
				}
				else
				{
					MonoBehaviourSingleton<AppMain>.I.Reset();
				}
			}, true, (int)error2);
		}
	}

	public static bool CheckApplicationVersion(string check_version_text)
	{
		if (string.IsNullOrEmpty(check_version_text))
		{
			return true;
		}
		if (appVer == (Version)null)
		{
			appVer = NetworkNative.getNativeVersionFromName();
		}
		return appVer.CompareTo(new Version(check_version_text)) >= 0;
	}

	public static void Delay(float sec, Action func)
	{
		if (object.ReferenceEquals(MonoBehaviourSingleton<AppMain>.I, null))
		{
			func();
		}
		else
		{
			MonoBehaviourSingleton<AppMain>.I.StartCoroutine(MonoBehaviourSingleton<AppMain>.I._Delay(sec, func));
		}
	}

	private IEnumerator _Delay(float sec, Action func)
	{
		yield return (object)new WaitForSeconds(sec);
		func();
	}

	private void ApplicationResume()
	{
		Native.CancelAllLocalNotification();
	}

	public void RegisterLocalNotify()
	{
		RegisterGuildRequestLocalNotification();
		RegisterBundleOffersLocalNotification();
		RegisterBlackMarketLocalNotification();
	}

	private void RegisterGuildRequestLocalNotification()
	{
		if (MonoBehaviourSingleton<GuildRequestManager>.IsValid())
		{
			MonoBehaviourSingleton<GuildRequestManager>.I.RegisterGuildRequestLocalNotification();
		}
		for (int i = 0; i < localNotificationGuildRequestTime.Count; i++)
		{
			DateTime dateTime = localNotificationGuildRequestTime[i];
			TimeSpan timeSpan = new TimeSpan(dateTime.Ticks - DateTime.Now.Ticks);
			int num = (int)timeSpan.TotalSeconds;
			if (0 < num)
			{
				int num2 = i;
				int hours = dateTime.TimeOfDay.Hours;
				string title = StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 9u);
				if (hours >= 8)
				{
					string body = StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 10u);
					Native.RegisterLocalNotification(num2, title, body, num);
				}
				DateTime dateTime2 = dateTime.Add(new TimeSpan(6, 0, 0));
				int hours2 = dateTime2.TimeOfDay.Hours;
				if (hours2 >= 8)
				{
					TimeSpan timeSpan2 = new TimeSpan(dateTime2.Ticks - DateTime.Now.Ticks);
					int afterSeconds = (int)timeSpan2.TotalSeconds;
					string body2 = StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 20u);
					Native.RegisterLocalNotification(num2 + 100, title, body2, afterSeconds);
				}
			}
		}
	}

	private void RegisterBundleOffersLocalNotification()
	{
		if (MonoBehaviourSingleton<ShopManager>.IsValid() && MonoBehaviourSingleton<ShopManager>.I.purchaseItemList != null)
		{
			TimeSpan timeSpan = new TimeSpan(localPurchaseItemListRequestTime.Ticks - DateTime.Now.Ticks);
			int num = (int)timeSpan.TotalSeconds;
			int id = 900001;
			int num2 = 0;
			if (MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups.Count > 0)
			{
				for (int i = 0; i < MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups.Count; i++)
				{
					if (MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups[i].remainTimes > 86400 && !GameSaveData.instance.iAPBundleBought.Contains(MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups[i].productId) && num2 < MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups[i].remainTimes)
					{
						num2 = MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups[i].remainTimes;
					}
				}
			}
			num2 = num2 - num - 86400;
			if (num2 > 0)
			{
				string title = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 30u);
				string body = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 31u);
				Native.RegisterLocalNotification(id, title, body, num2);
			}
		}
	}

	private void RegisterBlackMarketLocalNotification()
	{
		if (GoGameTimeManager.HasValue() && !string.IsNullOrEmpty(GameSaveData.instance.resetMarketTime))
		{
			int num = 910001;
			int num2 = (int)GoGameTimeManager.GetRemainTime(GameSaveData.instance.resetMarketTime).TotalSeconds;
			if (num2 > 0)
			{
				Native.RegisterLocalNotification(num, "Ahoy! Ange just got a new haul", "Check out Pirate's Loot for the latest limited offers!", num2);
			}
			if (num2 > 300)
			{
				int id = num + 1;
				int afterSeconds = num2 - 300;
				Native.RegisterLocalNotification(id, "ATTENTION ALL HUNTERS!!!", "Ange’s new shipment will arrive in 5 minutes. Special offers await!", afterSeconds);
			}
		}
	}

	public void SetGuildRequestConstructLocalNotification(List<DateTime> time)
	{
		localNotificationGuildRequestTime = time;
	}

	public void UpdatePurchaseItemListRequestTime()
	{
		localPurchaseItemListRequestTime = DateTime.Now;
	}

	public void ChangeScene(string scene, string section, Action callback)
	{
		StartCoroutine(CRChangeScene(scene, section, callback));
	}

	private IEnumerator CRChangeScene(string scene, string section, Action callback)
	{
		yield return (object)new WaitUntil(() => MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() && !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing);
		callback?.Invoke();
		MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene(scene, section, UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false);
	}
}
