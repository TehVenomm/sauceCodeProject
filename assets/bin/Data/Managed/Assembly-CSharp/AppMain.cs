// Decompiled with JetBrains decompiler
// Type: AppMain
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using App.Scripts.GoGame.Optimization;
using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;

#nullable disable
public class AppMain : MonoBehaviourSingleton<AppMain>
{
  public const int BASE_RESOLUTION_HEIGHT = 854;
  public const int BASE_RESOLUTION_HEIGHT_HIGH = 1280 /*0x0500*/;
  public static int amountMemoryClear = 500;
  public string startScene = "Title";
  public System.Action onDelayCall;
  private int periodicGCCollectCount;
  private List<DateTime> localNotificationGuildRequestTime = new List<DateTime>(5);
  private DateTime localPurchaseItemListRequestTime;
  public ServerListTable.ServerData currentServer;
  public string email;
  public string fbId;
  public string uid;
  private bool changedResolution;
  private IEnumerator changedResolutionWork;
  private bool showingTableLoadError;
  private static Version appVer = (Version) null;
  private readonly string[] RUNTIME_PERMISSIONS = new string[1]
  {
    "android.permission.WRITE_EXTERNAL_STORAGE"
  };

  public int defaultScreenWidth { get; private set; }

  public int defaultScreenHeight { get; private set; }

  public int mainScreenWidth { get; private set; }

  public int mainScreenHeight { get; private set; }

  public static int totalReservedMemory
  {
    get => (int) Profiler.GetTotalAllocatedMemoryLong() / 1048576 /*0x100000*/;
  }

  public static bool needClearMemory => AppMain.totalReservedMemory > AppMain.amountMemoryClear;

  public static bool isApplicationQuit { get; private set; }

  public static bool isReset { get; private set; }

  public static bool isInitialized { get; private set; }

  public static string appStr { get; set; }

  public Camera mainCamera { get; private set; }

  public Transform mainCameraTransform { get; private set; }

  public bool enablePeriodicGCCollect { get; set; }

  public bool isExecutingClearMemory { get; private set; }

  public bool isExecutingUnloadUnusedAssets { get; private set; }

  public int frameExecutedUnloadUnusedAssets { get; private set; }

  public void ClearChangeServerData()
  {
    this.currentServer = (ServerListTable.ServerData) null;
    this.email = (string) null;
    this.fbId = (string) null;
    this.uid = (string) null;
  }

  public static void Startup()
  {
    Application.targetFrameRate = 30;
    ShaderGlobal.Initialize();
    EffectManager.Startup();
  }

  public void SetMainCamera(Camera _camera)
  {
    this.mainCamera = _camera;
    if (Object.op_Inequality((Object) _camera, (Object) null))
      this.mainCameraTransform = ((Component) _camera).transform;
    else
      this.mainCameraTransform = (Transform) null;
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
    Utility.SetCollideLayers(31 /*0x1F*/, 8, 12, 14);
    Utility.SetCollideLayers(31 /*0x1F*/, 13, 15, 10);
    Utility.SetCollideLayers(31 /*0x1F*/, 18, 9, 17, 21);
    Utility.SetCollideLayers(16 /*0x10*/, 17, 9, 18);
  }

  protected override void Awake()
  {
    base.Awake();
    Object.DontDestroyOnLoad((Object) this);
    SpecialDeviceManager.StartUp();
    CrashlyticsReporter.EnableReport();
    this.defaultScreenWidth = Screen.width;
    this.defaultScreenHeight = Screen.height;
    if (this.defaultScreenWidth > this.defaultScreenHeight)
    {
      int defaultScreenWidth = this.defaultScreenWidth;
      this.defaultScreenWidth = this.defaultScreenHeight;
      this.defaultScreenHeight = defaultScreenWidth;
    }
    this.SetupScreen();
    this.UpdateResolution(Screen.width < Screen.height);
    TitleTop.isFirstBoot = true;
    GC.Collect();
    int length = Mathf.Max((int) (52428800L /*0x03200000*/ - GC.GetTotalMemory(false)) / 1024 /*0x0400*/, 1);
    foreach (object obj in new object[1024 /*0x0400*/])
      obj = (object) new byte[length];
    GC.Collect();
  }

  public void SetupScreen()
  {
    if (this.defaultScreenHeight > 854)
    {
      int num = this.defaultScreenHeight > 1280 /*0x0500*/ ? 1280 /*0x0500*/ : 854;
      this.mainScreenWidth = (int) ((double) this.defaultScreenWidth * ((double) num / (double) this.defaultScreenHeight) + 0.10000000149011612);
      this.mainScreenHeight = num;
    }
    else
    {
      this.mainScreenWidth = this.defaultScreenWidth;
      this.mainScreenHeight = this.defaultScreenHeight;
    }
  }

  private void Update()
  {
    if (this.enablePeriodicGCCollect && ++this.periodicGCCollectCount >= 30)
    {
      this.periodicGCCollectCount = 0;
      GC.Collect();
    }
    if (this.onDelayCall == null)
      return;
    if (!AppMain.isApplicationQuit)
      this.onDelayCall();
    this.onDelayCall = (System.Action) null;
  }

  private void Start()
  {
    this.CheckRuntimePermission();
    this.StartCoroutine(this.OnDelayToInit());
  }

  private IEnumerator OnDelayToInit()
  {
    Screen.sleepTimeout = -1;
    AppMain.isInitialized = false;
    AppMain.isApplicationQuit = false;
    AppMain.isReset = false;
    this.showingTableLoadError = false;
    AppMain.Startup();
    this.InitCollideLayers();
    yield return (object) null;
    string path = Path.Combine(Application.temporaryCachePath, "assetbundles");
    if (!Directory.Exists(path))
      Directory.CreateDirectory(path);
    Cache cacheByPath = Caching.GetCacheByPath(path);
    if (((Cache) ref cacheByPath).valid)
      Caching.currentCacheForWriting = cacheByPath;
    Screen.orientation = Screen.orientation == null || Screen.orientation == 1 || Screen.orientation == 3 || Screen.orientation == 3 || Screen.orientation == 4 ? (ScreenOrientation) 1 : (ScreenOrientation) 2;
    Utility.Initialize();
    Temporary.Initialize();
    Protocol.Initialize();
    HomeSelfCharacter.CTRL = true;
    AppMain.appVer = NetworkNative.getNativeVersionFromName();
    yield return (object) null;
    GameObject go = ((Component) this).gameObject;
    go.AddComponent<GoWrapManager>();
    go.AddComponent<FCMManager>();
    go.AddComponent<DefaultTimeUpdater>();
    go.AddComponent<ResourceManager>();
    MonoBehaviourSingleton<ResourceManager>.I.onAsyncLoadQuery = new Func<bool>(this.onAsyncLoadQuery);
    go.AddComponent<InstantiateManager>();
    go.AddComponent<GoGameCacheManager>();
    yield return (object) null;
    DataTableManager dataTableManager = new GameObject("DataTableManager").AddComponent<DataTableManager>();
    ((Component) dataTableManager).transform.parent = this._transform;
    dataTableManager.onError += new Action<DataTableLoadError, System.Action>(this.OnTableDownloadError);
    ResourceManager.enableLoadDirect = false;
    yield return (object) null;
    this.CreateDefaultCamera();
    go.AddComponent<ScreenOrientationManager>();
    MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
    this.UpdateResolution(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
    yield return (object) null;
    Utility.CreateGameObjectAndComponent("AudioListenerManager", MonoBehaviourSingleton<AppMain>.I._transform);
    yield return (object) null;
    Utility.CreateGameObjectAndComponent("SoundManager", this._transform);
    yield return (object) null;
    Utility.CreateGameObjectAndComponent("AudioObjectPool", this._transform);
    yield return (object) null;
    Utility.CreateGameObjectAndComponent("EffectManager", this._transform);
    yield return (object) null;
    ServerAccountSaveData.Load();
    go.AddComponent<NetworkManager>();
    go.AddComponent<ProtocolManager>();
    go.AddComponent<AccountManager>();
    go.AddComponent<TimeManager>();
    go.AddComponent<GoGameTimeManager>();
    yield return (object) null;
    Utility.CreateGameObjectAndComponent("NativeReceiver", this._transform);
    Utility.CreateGameObjectAndComponent("ShopReceiver", this._transform);
    Utility.CreateGameObjectAndComponent("ChatManager", this._transform);
    Utility.CreateGameObjectAndComponent("GGNativeShare", this._transform);
    go.AddComponent<AssetPreDownloadManager>();
    Application.backgroundLoadingPriority = (ThreadPriority) 2;
    go.AddComponent<HelpshiftManager>();
    yield return (object) null;
    go.AddComponent<CoopApp>();
    go.AddComponent<BootProcess>();
  }

  public void UpdateResolution(bool is_portrait, System.Action onDelay)
  {
    this.StartCoroutine(this.WaitToUpdateResolution(is_portrait, onDelay));
  }

  private IEnumerator WaitToUpdateResolution(bool is_portrait, System.Action onDelay)
  {
    yield return (object) null;
    Screen.orientation = (ScreenOrientation) 1;
    yield return (object) null;
    this.UpdateResolution(is_portrait);
    yield return (object) null;
    MonoBehaviourSingleton<AppMain>.I.onDelayCall += (System.Action) (() => Screen.orientation = (ScreenOrientation) 5);
  }

  public void UpdateResolution(bool is_portrait)
  {
    bool flag = false;
    if (GameSaveData.instance != null)
      flag = GameSaveData.instance.graphicOptionKey == "low";
    int w;
    int h;
    if (flag)
    {
      w = this.mainScreenWidth;
      h = this.mainScreenHeight;
      this.changedResolution = true;
    }
    else
    {
      if (!this.changedResolution)
        return;
      w = this.defaultScreenWidth;
      h = this.defaultScreenHeight;
    }
    if (!is_portrait)
    {
      int num = w;
      w = h;
      h = num;
    }
    if (this.changedResolutionWork != null)
      this.StopCoroutine(this.changedResolutionWork);
    this.changedResolutionWork = this._UpdateResolution(w, h);
    this.StartCoroutine(this.changedResolutionWork);
  }

  private IEnumerator _UpdateResolution(int w, int h)
  {
    UIRenderTexture[] renderTextures = ((Component) this).gameObject.GetComponentsInChildren<UIRenderTexture>(true);
    int index1 = 0;
    for (int length = renderTextures.Length; index1 < length; ++index1)
      ((Behaviour) renderTextures[index1]).enabled = false;
    Screen.SetResolution(w, h, true);
    yield return (object) new WaitForEndOfFrame();
    yield return (object) new WaitForEndOfFrame();
    int index2 = 0;
    for (int length = renderTextures.Length; index2 < length; ++index2)
      ((Behaviour) renderTextures[index2]).enabled = true;
    this.changedResolutionWork = (IEnumerator) null;
  }

  private void OnScreenRotate(bool is_portrait) => this.UpdateResolution(is_portrait);

  public void OnLoadFinished() => AppMain.isInitialized = true;

  private void CreateDefaultCamera()
  {
    if (!Object.op_Equality((Object) Camera.main, (Object) null))
      return;
    ResourceUtility.Realizes(Resources.Load("System/DefaultMainCamera"), this._transform);
  }

  private void OnApplicationPause(bool pause_status)
  {
    if (pause_status)
    {
      GameSaveData.Save();
      Screen.sleepTimeout = -2;
      Native.CancelAllLocalNotification();
      this.RegisterLocalNotify();
    }
    else
    {
      Screen.sleepTimeout = -1;
      if (!AppMain.isInitialized || this.CheckInvitedClanBySNS() || this.CheckInvitedPartyBySNS() || this.CheckInvitedLoungeBySNS() || this.CheckMutualFollowBySNS())
        return;
      if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "HomeTop" || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "LoungeTop" || MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "ClanTop")
        Protocol.Force((System.Action) (() => MonoBehaviourSingleton<PartyManager>.I.SendInvitedParty((Action<bool>) (is_success => { }), true)));
      Native.CancelAllLocalNotification();
    }
  }

  private bool CheckInvitedClanBySNS()
  {
    if (!string.IsNullOrEmpty(PlayerPrefs.GetString("ic")))
    {
      PlayerPrefs.SetString("ic", "");
      if (MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && TutorialStep.HasAllTutorialCompleted())
        MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent();
      string _name = "MAIN_MENU_HOME";
      if (LoungeMatchingManager.IsValidInLounge())
        _name = "MAIN_MENU_LOUNGE";
      EventData[] event_datas = new EventData[4]
      {
        new EventData(_name, (object) null),
        new EventData("GUILD", (object) null),
        new EventData("SEARCH", (object) null),
        new EventData("INFO", (object) 97)
      };
      if (TutorialStep.HasAllTutorialCompleted())
      {
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(event_datas);
        return true;
      }
    }
    return false;
  }

  private bool CheckInvitedPartyBySNS()
  {
    string str = PlayerPrefs.GetString("im");
    if (!string.IsNullOrEmpty(str))
    {
      MonoBehaviourSingleton<PartyManager>.I.InviteValue = str;
      PlayerPrefs.SetString("im", "");
      if (MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && TutorialStep.HasAllTutorialCompleted())
        MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent();
      EventData[] event_datas = new EventData[3]
      {
        new EventData(GameSection.GetGoingHomeEvent(), (object) null),
        new EventData("GACHA_QUEST_COUNTER", (object) null),
        new EventData("INVITED_ROOM", (object) null)
      };
      if (TutorialStep.HasAllTutorialCompleted())
      {
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(event_datas);
        return true;
      }
    }
    return false;
  }

  private bool CheckInvitedLoungeBySNS()
  {
    string inviteLoungeValue = PlayerPrefs.GetString("il");
    if (string.IsNullOrEmpty(inviteLoungeValue))
      return false;
    MonoBehaviourSingleton<LoungeMatchingManager>.I.InviteValue = inviteLoungeValue;
    PlayerPrefs.SetString("il", "");
    if (MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && TutorialStep.HasAllTutorialCompleted())
      MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent();
    if (!TutorialStep.HasAllTutorialCompleted() || (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < 15)
      return false;
    if (MonoBehaviourSingleton<LoungeManager>.IsValid())
      this.StartCoroutine(this.SetAutoEventLoungeToLounge(inviteLoungeValue));
    else
      MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[3]
      {
        new EventData("MAIN_MENU_HOME", (object) null),
        new EventData("LOUNGE", (object) null),
        new EventData("INVITED_LOUNGE", (object) null)
      });
    return true;
  }

  private IEnumerator SetAutoEventLoungeToLounge(string inviteLoungeValue)
  {
    while (!LoungeMatchingManager.IsValidInLounge())
      yield return (object) null;
    if (!(inviteLoungeValue.Split('_')[0] == MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.loungeNumber))
      MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[5]
      {
        new EventData("MAIN_MENU_LOUNGE", (object) null),
        new EventData("LOUNGE_SETTINGS", (object) null),
        new EventData("EXIT", (object) null),
        new EventData("LOUNGE", (object) null),
        new EventData("INVITED_LOUNGE", (object) null)
      });
  }

  private bool CheckMutualFollowBySNS()
  {
    string str = PlayerPrefs.GetString("fc");
    if (!string.IsNullOrEmpty(str))
    {
      MonoBehaviourSingleton<FriendManager>.I.MutualFollowValue = str;
      PlayerPrefs.SetString("fc", "");
      if (MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && TutorialStep.HasAllTutorialCompleted())
        MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent();
      EventData[] event_datas = new EventData[6]
      {
        new EventData(GameSection.GetGoingHomeEvent(), (object) null),
        new EventData("MUTUAL_FOLLOW", (object) null),
        new EventData("MAIN_MENU_MENU", (object) null),
        new EventData("FRIEND", (object) null),
        new EventData("FOLLOW_LIST", (object) null),
        new EventData("MUTUAL_FOLLOW_MESSAGE", (object) null)
      };
      if (TutorialStep.HasAllTutorialCompleted())
      {
        PlayerPrefs.SetString("fc", "");
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(event_datas);
        return true;
      }
    }
    return false;
  }

  private void OnApplicationQuit()
  {
    AppMain.isApplicationQuit = true;
    GameSaveData.Save();
    Native.CancelAllLocalNotification();
    this.RegisterLocalNotify();
  }

  private bool onAsyncLoadQuery()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.IsValid())
    {
      GameSection currentScene = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentScene();
      if (Object.op_Inequality((Object) currentScene, (Object) null) && currentScene is InGameScene)
      {
        GameSection currentSection = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection();
        if (MonoBehaviourSingleton<InGameProgress>.IsValid() && Object.op_Inequality((Object) currentSection, (Object) null) && currentSection is InGameMain)
          return MonoBehaviourSingleton<InGameProgress>.I.isBattleStart;
      }
    }
    return true;
  }

  public Coroutine ClearMemory(bool clearObjCaches, bool clearPreloaded, bool IsLowPriority = false)
  {
    return this.StartCoroutine(this.DoClearMemory(clearObjCaches, clearPreloaded, IsLowPriority));
  }

  private IEnumerator DoClearMemory(bool clearObjCaches, bool clearPreloaded, bool IsLowPriority)
  {
    while (this.isExecutingClearMemory)
      yield return (object) null;
    this.isExecutingClearMemory = true;
    if (MonoBehaviourSingleton<ResourceManager>.IsValid())
    {
      while (!MonoBehaviourSingleton<ResourceManager>.I.isAllStay)
        yield return (object) null;
      if (clearObjCaches)
      {
        if (IsLowPriority)
        {
          yield return (object) MonoBehaviourSingleton<ResourceManager>.I.cache.DoClearPackageCaches();
          yield return (object) MonoBehaviourSingleton<ResourceManager>.I.cache.DoClearObjectCaches(clearPreloaded);
        }
        else
        {
          MonoBehaviourSingleton<ResourceManager>.I.cache.ClearPackageCaches();
          MonoBehaviourSingleton<ResourceManager>.I.cache.ClearObjectCaches(clearPreloaded);
        }
      }
      MonoBehaviourSingleton<ResourceManager>.I.cache.ClearSENameDictionary();
    }
    if (MonoBehaviourSingleton<GoGameCacheManager>.IsValid())
      MonoBehaviourSingleton<GoGameCacheManager>.I.Delete();
    this.ClearPoolObjects();
    yield return (object) this.UnloadUnusedAssets(true);
    this.isExecutingClearMemory = false;
  }

  public Coroutine ClearEnemyAssets() => this.StartCoroutine(this.DoClearEnemyAssets());

  private IEnumerator DoClearEnemyAssets()
  {
    while (this.isExecutingClearMemory)
      yield return (object) null;
    this.isExecutingClearMemory = true;
    if (MonoBehaviourSingleton<ResourceManager>.IsValid())
    {
      while (!MonoBehaviourSingleton<ResourceManager>.I.isAllStay)
        yield return (object) null;
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
    yield return (object) this.UnloadUnusedAssets(true);
    this.isExecutingClearMemory = false;
  }

  public Coroutine UnloadUnusedAssets(bool need_gc_collect)
  {
    return this.StartCoroutine(this.DoUnloadUnusedAssets(need_gc_collect));
  }

  private IEnumerator DoUnloadUnusedAssets(bool need_gc_collect)
  {
    while (this.isExecutingUnloadUnusedAssets)
      yield return (object) null;
    this.isExecutingUnloadUnusedAssets = true;
    this.frameExecutedUnloadUnusedAssets = Time.frameCount;
    if (need_gc_collect)
    {
      GC.Collect();
      yield return (object) new WaitForEndOfFrame();
      yield return (object) new WaitForEndOfFrame();
      yield return (object) new WaitForEndOfFrame();
    }
    yield return (object) Resources.UnloadUnusedAssets();
    this.isExecutingUnloadUnusedAssets = false;
    yield return (object) new WaitForEndOfFrame();
    yield return (object) new WaitForEndOfFrame();
    yield return (object) new WaitForEndOfFrame();
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

  public void Reset() => this.Reset(false, false);

  public void Reset(bool need_clear_cache, bool need_predownload)
  {
    if (!MonoBehaviourSingleton<ResourceManager>.IsValid())
      return;
    this.StartCoroutine(this.DoReset(need_clear_cache, need_predownload));
  }

  private IEnumerator DoReset(bool need_clear_cache, bool need_predownload)
  {
    AppMain.isReset = true;
    if (MonoBehaviourSingleton<UIManager>.IsValid())
      MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.RESET, true);
    if (MonoBehaviourSingleton<TransitionManager>.IsValid())
      yield return (object) MonoBehaviourSingleton<TransitionManager>.I.Out();
    if (MonoBehaviourSingleton<SoundManager>.IsValid() && MonoBehaviourSingleton<SoundManager>.I.playingBGMID != 0)
    {
      MonoBehaviourSingleton<SoundManager>.I.requestBGMID = 0;
      MonoBehaviourSingleton<SoundManager>.I.fadeOutTime = 1f;
      yield return (object) new WaitForSeconds(1f);
    }
    if (MonoBehaviourSingleton<PredownloadManager>.IsValid())
      Object.DestroyImmediate((Object) MonoBehaviourSingleton<PredownloadManager>.I);
    if (MonoBehaviourSingleton<LoadingProcess>.IsValid())
      Object.DestroyImmediate((Object) MonoBehaviourSingleton<LoadingProcess>.I);
    if (MonoBehaviourSingleton<DataTableManager>.IsValid())
    {
      MonoBehaviourSingleton<DataTableManager>.I.Clear();
      Object.DestroyImmediate((Object) MonoBehaviourSingleton<DataTableManager>.I);
    }
    if (MonoBehaviourSingleton<ResourceManager>.IsValid())
    {
      MonoBehaviourSingleton<ResourceManager>.I.CancelAll();
      while (MonoBehaviourSingleton<ResourceManager>.I.isLoading)
        yield return (object) null;
    }
    MonoBehaviourSingleton<ResourceManager>.I.Reset();
    SceneManager.LoadScene("Empty");
    if (need_clear_cache)
    {
      if (Singleton<StringTable>.IsValid() && MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.loading, (Object) null))
        MonoBehaviourSingleton<UIManager>.I.loading.ShowSystemMessage(StringTable.Get(STRING_CATEGORY.COMMON, 20U));
      yield return (object) this.UnloadUnusedAssets(true);
      PlayerPrefs.SetInt("AppMain.Reset", (need_clear_cache ? 1 : 0) | (need_predownload ? 2 : 0));
      yield return (object) this.StartCoroutine(ResourceManager.ClearCache());
      yield return (object) new WaitForSeconds(1f);
      if (need_predownload && Singleton<StringTable>.IsValid() && MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.loading, (Object) null))
      {
        ((Component) this).gameObject.AddComponent<PredownloadManager>();
        while (MonoBehaviourSingleton<PredownloadManager>.I.totalCount == 0)
          yield return (object) null;
        int total = MonoBehaviourSingleton<PredownloadManager>.I.totalCount;
        int count = -1;
        while (count < total)
        {
          if (count != MonoBehaviourSingleton<PredownloadManager>.I.loadedCount)
          {
            count = MonoBehaviourSingleton<PredownloadManager>.I.loadedCount;
            MonoBehaviourSingleton<UIManager>.I.loading.ShowSystemMessage(StringTable.Format(STRING_CATEGORY.COMMON, 21U, (object) count, (object) total));
          }
          yield return (object) null;
        }
        MonoBehaviourSingleton<UIManager>.I.loading.ShowSystemMessage((string) null);
        MonoBehaviourSingleton<ResourceManager>.I.Reset();
      }
      PlayerPrefs.SetInt("AppMain.Reset", 0);
    }
    ((Component) this).gameObject.BroadcastMessage("OnApplicationQuit", (SendMessageOptions) 1);
    MonoBehaviour[] components = ((Component) this).GetComponents<MonoBehaviour>();
    for (int index = this._transform.childCount - 1; index >= 0; --index)
      ((Component) this._transform.GetChild(index)).gameObject.SetActive(false);
    foreach (MonoBehaviour monoBehaviour in components)
    {
      if (Object.op_Inequality((Object) monoBehaviour, (Object) null) && Object.op_Inequality((Object) monoBehaviour, (Object) this))
        ((Behaviour) monoBehaviour).enabled = false;
    }
    this.SetMainCamera((Camera) null);
    for (int index = this._transform.childCount - 1; index >= 0; --index)
      Object.DestroyImmediate((Object) ((Component) this._transform.GetChild(index)).gameObject);
    foreach (MonoBehaviour monoBehaviour in components)
    {
      if (Object.op_Inequality((Object) monoBehaviour, (Object) this))
        Object.DestroyImmediate((Object) monoBehaviour);
    }
    this.CreateDefaultCamera();
    yield return (object) new WaitForEndOfFrame();
    yield return (object) new WaitForEndOfFrame();
    yield return (object) new WaitForEndOfFrame();
    SingletonBase.RemoveAllInstance();
    yield return (object) Resources.UnloadUnusedAssets();
    GC.Collect();
    yield return (object) new WaitForEndOfFrame();
    yield return (object) new WaitForEndOfFrame();
    yield return (object) new WaitForEndOfFrame();
    MonoBehaviourSingleton<AppMain>.I.startScene = "Title";
    this.Start();
  }

  public void OnTableDownloadError(DataTableLoadError error, System.Action retry)
  {
    if (this.showingTableLoadError)
      return;
    this.showingTableLoadError = true;
    Error error1;
    switch (error)
    {
      case DataTableLoadError.AssetNotFoundError:
        error1 = Error.AssetNotFound;
        break;
      case DataTableLoadError.VerifyError:
        error1 = Error.AssetVerifyFailed;
        break;
      case DataTableLoadError.FileWriteError:
        error1 = Error.AssetSaveFailed;
        break;
      default:
        error1 = Error.AssetLoadFailed;
        break;
    }
    MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, StringTable.GetErrorMessage((uint) error1), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 110U), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 111U)), (Action<string>) (btn =>
    {
      this.showingTableLoadError = false;
      if (btn == "YES")
        retry();
      else
        MonoBehaviourSingleton<AppMain>.I.Reset();
    }), true, (int) error1);
  }

  public static bool CheckApplicationVersion(string check_version_text)
  {
    if (string.IsNullOrEmpty(check_version_text))
      return true;
    if (AppMain.appVer == (Version) null)
      AppMain.appVer = NetworkNative.getNativeVersionFromName();
    return AppMain.appVer.CompareTo(new Version(check_version_text)) >= 0;
  }

  public static void Delay(float sec, System.Action func)
  {
    if (MonoBehaviourSingleton<AppMain>.I == null)
      func();
    else
      MonoBehaviourSingleton<AppMain>.I.StartCoroutine(MonoBehaviourSingleton<AppMain>.I._Delay(sec, func));
  }

  private IEnumerator _Delay(float sec, System.Action func)
  {
    yield return (object) new WaitForSeconds(sec);
    func();
  }

  private void ApplicationResume() => Native.CancelAllLocalNotification();

  public void RegisterLocalNotify()
  {
    this.RegisterGuildRequestLocalNotification();
    this.RegisterBundleOffersLocalNotification();
    this.RegisterBlackMarketLocalNotification();
  }

  private void RegisterGuildRequestLocalNotification()
  {
    if (MonoBehaviourSingleton<GuildRequestManager>.IsValid())
      MonoBehaviourSingleton<GuildRequestManager>.I.RegisterGuildRequestLocalNotification();
    for (int index = 0; index < this.localNotificationGuildRequestTime.Count; ++index)
    {
      DateTime dateTime1 = this.localNotificationGuildRequestTime[index];
      TimeSpan timeSpan1;
      ref TimeSpan local1 = ref timeSpan1;
      long ticks1 = dateTime1.Ticks;
      DateTime now = DateTime.Now;
      long ticks2 = now.Ticks;
      long ticks3 = ticks1 - ticks2;
      local1 = new TimeSpan(ticks3);
      int totalSeconds1 = (int) timeSpan1.TotalSeconds;
      if (0 < totalSeconds1)
      {
        int id = index;
        TimeSpan timeOfDay = dateTime1.TimeOfDay;
        int hours = timeOfDay.Hours;
        string title = StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 9U);
        if (hours >= 8)
        {
          string body = StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 10U);
          Native.RegisterLocalNotification(id, title, body, totalSeconds1);
        }
        DateTime dateTime2 = dateTime1.Add(new TimeSpan(6, 0, 0));
        timeOfDay = dateTime2.TimeOfDay;
        if (timeOfDay.Hours >= 8)
        {
          TimeSpan timeSpan2;
          ref TimeSpan local2 = ref timeSpan2;
          long ticks4 = dateTime2.Ticks;
          now = DateTime.Now;
          long ticks5 = now.Ticks;
          long ticks6 = ticks4 - ticks5;
          local2 = new TimeSpan(ticks6);
          int totalSeconds2 = (int) timeSpan2.TotalSeconds;
          string body = StringTable.Get(STRING_CATEGORY.GUILD_REQUEST, 20U);
          Native.RegisterLocalNotification(id + 100, title, body, totalSeconds2);
        }
      }
    }
  }

  private void RegisterBundleOffersLocalNotification()
  {
    if (!MonoBehaviourSingleton<ShopManager>.IsValid() || MonoBehaviourSingleton<ShopManager>.I.purchaseItemList == null)
      return;
    int totalSeconds = (int) new TimeSpan(this.localPurchaseItemListRequestTime.Ticks - DateTime.Now.Ticks).TotalSeconds;
    int id = 900001;
    int num = 0;
    if (MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups.Count > 0)
    {
      for (int index = 0; index < MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups.Count; ++index)
      {
        if (MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups[index].remainTimes > 86400 && !GameSaveData.instance.iAPBundleBought.Contains(MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups[index].productId) && num < MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups[index].remainTimes)
          num = MonoBehaviourSingleton<ShopManager>.I.purchaseItemList.skuPopups[index].remainTimes;
      }
    }
    int afterSeconds = num - totalSeconds - 86400;
    if (afterSeconds <= 0)
      return;
    string title = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 30U);
    string body = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 31U /*0x1F*/);
    Native.RegisterLocalNotification(id, title, body, afterSeconds);
  }

  private void RegisterBlackMarketLocalNotification()
  {
    if (!GoGameTimeManager.HasValue() || string.IsNullOrEmpty(GameSaveData.instance.resetMarketTime))
      return;
    int id = 910001;
    int totalSeconds = (int) GoGameTimeManager.GetRemainTime(GameSaveData.instance.resetMarketTime).TotalSeconds;
    if (totalSeconds > 0)
      Native.RegisterLocalNotification(id, "Ahoy! Ange just got a new haul", "Check out Pirate's Loot for the latest limited offers!", totalSeconds);
    if (totalSeconds <= 300)
      return;
    Native.RegisterLocalNotification(id + 1, "ATTENTION ALL HUNTERS!!!", "Ange’s new shipment will arrive in 5 minutes. Special offers await!", totalSeconds - 300);
  }

  public void SetGuildRequestConstructLocalNotification(List<DateTime> time)
  {
    this.localNotificationGuildRequestTime = time;
  }

  public void UpdatePurchaseItemListRequestTime()
  {
    this.localPurchaseItemListRequestTime = DateTime.Now;
  }

  public void ChangeScene(string scene, string section, System.Action callback)
  {
    this.StartCoroutine(this.CRChangeScene(scene, section, callback));
  }

  private IEnumerator CRChangeScene(string scene, string section, System.Action callback)
  {
    yield return (object) new WaitUntil((Func<bool>) (() => MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() && !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing));
    if (callback != null)
      callback();
    MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene(scene, section);
  }

  private void CheckRuntimePermission()
  {
    if (AndroidRuntimePermissionChecker.CheckPermissions(this.RUNTIME_PERMISSIONS))
      return;
    List<string> stringList = new List<string>();
    for (int index = 0; index < this.RUNTIME_PERMISSIONS.Length; ++index)
      stringList.Add(this.RUNTIME_PERMISSIONS[index]);
    if (stringList.Count <= 0)
      return;
    AndroidRuntimePermissionChecker.RequestPermission(stringList.ToArray());
  }

  public enum LocalNotificationType
  {
    GUILD_REQUEST_1 = 0,
    GUILD_REQUEST_2 = 1,
    GUILD_REQUEST_3 = 2,
    GUILD_REQUEST_4 = 3,
    GUILD_REQUEST_5 = 4,
    IAP_BUNDLE = 900001, // 0x000DBBA1
    BLACK_MARKET = 910001, // 0x000DE2B1
  }
}
