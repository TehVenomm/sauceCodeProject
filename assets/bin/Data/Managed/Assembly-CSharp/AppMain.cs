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
		Application.set_targetFrameRate(30);
		ShaderGlobal.Initialize();
		EffectManager.Startup();
	}

	public void SetMainCamera(Camera _camera)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		mainCamera = _camera;
		if (_camera != null)
		{
			mainCameraTransform = _camera.get_transform();
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
		Object.DontDestroyOnLoad(this);
		CrashlyticsReporter.EnableReport();
		this.defaultScreenWidth = Screen.get_width();
		defaultScreenHeight = Screen.get_height();
		if (this.defaultScreenWidth > defaultScreenHeight)
		{
			int defaultScreenWidth = this.defaultScreenWidth;
			this.defaultScreenWidth = defaultScreenHeight;
			defaultScreenHeight = defaultScreenWidth;
		}
		SetupScreen();
		UpdateResolution(Screen.get_width() < Screen.get_height());
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
				onDelayCall.Invoke();
			}
			onDelayCall = null;
		}
	}

	private unsafe void Start()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Invalid comparison between Unknown and I4
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Invalid comparison between Unknown and I4
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Invalid comparison between Unknown and I4
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Invalid comparison between Unknown and I4
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Expected O, but got Unknown
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		Screen.set_sleepTimeout(-1);
		isInitialized = false;
		isApplicationQuit = false;
		isReset = false;
		showingTableLoadError = false;
		Startup();
		InitCollideLayers();
		if ((int)Screen.get_orientation() == 0 || (int)Screen.get_orientation() == 1 || (int)Screen.get_orientation() == 3 || (int)Screen.get_orientation() == 3 || (int)Screen.get_orientation() == 4)
		{
			Screen.set_orientation(1);
		}
		else
		{
			Screen.set_orientation(2);
		}
		Utility.Initialize();
		Temporary.Initialize();
		Protocol.Initialize();
		HomeSelfCharacter.CTRL = true;
		appVer = NetworkNative.getNativeVersionFromName();
		GameObject val = this.get_gameObject();
		val.AddComponent<GoWrapManager>();
		val.AddComponent<FCMManager>();
		val.AddComponent<DefaultTimeUpdater>();
		val.AddComponent<ResourceManager>();
		MonoBehaviourSingleton<ResourceManager>.I.onAsyncLoadQuery = new Func<bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		val.AddComponent<InstantiateManager>();
		DataTableManager dataTableManager = new GameObject("DataTableManager").AddComponent<DataTableManager>();
		dataTableManager.get_transform().set_parent(base._transform);
		dataTableManager.onError += new Action<DataTableLoadError, Action>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		ResourceManager.enableLoadDirect = false;
		CreateDefaultCamera();
		val.AddComponent<ScreenOrientationManager>();
		MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
		UpdateResolution(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		Utility.CreateGameObjectAndComponent("AudioListenerManager", MonoBehaviourSingleton<AppMain>.I._transform, -1);
		Utility.CreateGameObjectAndComponent("SoundManager", base._transform, -1);
		Utility.CreateGameObjectAndComponent("AudioObjectPool", base._transform, -1);
		Utility.CreateGameObjectAndComponent("EffectManager", base._transform, -1);
		val.AddComponent<NetworkManager>();
		val.AddComponent<ProtocolManager>();
		val.AddComponent<AccountManager>();
		val.AddComponent<TimeManager>();
		val.AddComponent<GoGameTimeManager>();
		Utility.CreateGameObjectAndComponent("NativeReceiver", base._transform, -1);
		Utility.CreateGameObjectAndComponent("ShopReceiver", base._transform, -1);
		Utility.CreateGameObjectAndComponent("ChatManager", base._transform, -1);
		Utility.CreateGameObjectAndComponent("NativeShare", base._transform, -1);
		val.AddComponent<CoopApp>();
		val.AddComponent<BootProcess>();
	}

	public void UpdateResolution(bool is_portrait)
	{
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
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
			this.StopCoroutine(changedResolutionWork);
		}
		changedResolutionWork = _UpdateResolution(num, num2);
		this.StartCoroutine(changedResolutionWork);
	}

	private IEnumerator _UpdateResolution(int w, int h)
	{
		UIRenderTexture[] renderTextures = this.get_gameObject().GetComponentsInChildren<UIRenderTexture>(true);
		int l = 0;
		for (int k = renderTextures.Length; l < k; l++)
		{
			renderTextures[l].set_enabled(false);
		}
		Screen.SetResolution(w, h, true);
		yield return (object)new WaitForEndOfFrame();
		yield return (object)new WaitForEndOfFrame();
		int j = 0;
		for (int i = renderTextures.Length; j < i; j++)
		{
			renderTextures[j].set_enabled(true);
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
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		if (Camera.get_main() == null)
		{
			ResourceUtility.Realizes(Resources.Load("System/DefaultMainCamera"), base._transform, -1);
		}
	}

	private unsafe void OnApplicationPause(bool pause_status)
	{
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		if (pause_status)
		{
			GameSaveData.Save();
			Screen.set_sleepTimeout(-2);
			Native.CancelAllLocalNotification();
			RegisterLocalNotify();
		}
		else
		{
			Screen.set_sleepTimeout(-1);
			if (isInitialized && !CheckInvitedClanBySNS() && !CheckInvitedPartyBySNS() && !CheckInvitedLoungeBySNS() && !CheckMutualFollowBySNS())
			{
				if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "HomeTop" || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "LoungeTop")
				{
					if (_003C_003Ef__am_0024cache17 == null)
					{
						_003C_003Ef__am_0024cache17 = new Action((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
					}
					Protocol.Force(_003C_003Ef__am_0024cache17);
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
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
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
				this.StartCoroutine(SetAutoEventLoungeToLounge(@string));
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
			if (currentScene != null && currentScene is InGameScene)
			{
				GameSection currentSection = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection();
				if (MonoBehaviourSingleton<InGameProgress>.IsValid() && currentSection != null && currentSection is InGameMain)
				{
					return MonoBehaviourSingleton<InGameProgress>.I.isBattleStart;
				}
			}
		}
		return true;
	}

	public Coroutine ClearMemory(bool clearObjCaches, bool clearPreloaded)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		return this.StartCoroutine(DoClearMemory(clearObjCaches, clearPreloaded));
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
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		return this.StartCoroutine(DoClearEnemyAssets());
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
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		return this.StartCoroutine(DoUnloadUnusedAssets(need_gc_collect));
	}

	private IEnumerator DoUnloadUnusedAssets(bool need_gc_collect)
	{
		while (isExecutingUnloadUnusedAssets)
		{
			yield return (object)null;
		}
		isExecutingUnloadUnusedAssets = true;
		frameExecutedUnloadUnusedAssets = Time.get_frameCount();
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
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<ResourceManager>.IsValid())
		{
			this.StartCoroutine(DoReset(need_clear_cache, need_predownload));
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
			Object.DestroyImmediate(MonoBehaviourSingleton<PredownloadManager>.I);
		}
		if (MonoBehaviourSingleton<LoadingProcess>.IsValid())
		{
			Object.DestroyImmediate(MonoBehaviourSingleton<LoadingProcess>.I);
		}
		if (MonoBehaviourSingleton<DataTableManager>.IsValid())
		{
			MonoBehaviourSingleton<DataTableManager>.I.Clear();
			Object.DestroyImmediate(MonoBehaviourSingleton<DataTableManager>.I);
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
			if (Singleton<StringTable>.IsValid() && MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.loading != null)
			{
				MonoBehaviourSingleton<UIManager>.I.loading.ShowSystemMessage(StringTable.Get(STRING_CATEGORY.COMMON, 20u));
			}
			yield return (object)UnloadUnusedAssets(true);
			PlayerPrefs.SetInt("AppMain.Reset", (need_clear_cache ? 1 : 0) | (need_predownload ? 2 : 0));
			yield return (object)this.StartCoroutine(ResourceManager.ClearCache());
			yield return (object)new WaitForSeconds(1f);
			if (need_predownload && Singleton<StringTable>.IsValid() && MonoBehaviourSingleton<UIManager>.IsValid() && MonoBehaviourSingleton<UIManager>.I.loading != null)
			{
				this.get_gameObject().AddComponent<PredownloadManager>();
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
		this.get_gameObject().BroadcastMessage("OnApplicationQuit", 1);
		MonoBehaviour[] monos = this.GetComponents<MonoBehaviour>();
		for (int i = base._transform.get_childCount() - 1; i >= 0; i--)
		{
			base._transform.GetChild(i).get_gameObject().SetActive(false);
		}
		MonoBehaviour[] array = monos;
		foreach (MonoBehaviour mono in array)
		{
			if (mono != this)
			{
				mono.set_enabled(false);
			}
		}
		SetMainCamera(null);
		for (int j = base._transform.get_childCount() - 1; j >= 0; j--)
		{
			GameObject game_object = base._transform.GetChild(j).get_gameObject();
			Object.DestroyImmediate(game_object);
		}
		MonoBehaviour[] array2 = monos;
		foreach (MonoBehaviour mono2 in array2)
		{
			if (mono2 != this)
			{
				Object.DestroyImmediate(mono2);
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
					retry.Invoke();
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
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		if (object.ReferenceEquals(MonoBehaviourSingleton<AppMain>.I, null))
		{
			func.Invoke();
		}
		else
		{
			MonoBehaviourSingleton<AppMain>.I.StartCoroutine(MonoBehaviourSingleton<AppMain>.I._Delay(sec, func));
		}
	}

	private IEnumerator _Delay(float sec, Action func)
	{
		yield return (object)new WaitForSeconds(sec);
		func.Invoke();
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
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(CRChangeScene(scene, section, callback));
	}

	private unsafe IEnumerator CRChangeScene(string scene, string section, Action callback)
	{
		if (_003CCRChangeScene_003Ec__Iterator24A._003C_003Ef__am_0024cache8 == null)
		{
			_003CCRChangeScene_003Ec__Iterator24A._003C_003Ef__am_0024cache8 = new Func<bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		yield return (object)new WaitUntil(_003CCRChangeScene_003Ec__Iterator24A._003C_003Ef__am_0024cache8);
		if (callback != null)
		{
			callback.Invoke();
		}
		MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene(scene, section, UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false);
	}
}
