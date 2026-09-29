// Decompiled with JetBrains decompiler
// Type: GameSceneManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GameSceneManager : MonoBehaviourSingleton<GameSceneManager>
{
  public const string STR_EVENT_BACK = "[BACK]";
  private const string STR_EVENT_VERSION_RESTRICTION = "APP_VERSION_RESTRICTION";
  private const string STR_EVENT_VERSION_RESTRICTION_AUTO = "APP_VERSION_RESTRICTION_AUTO";
  private const string STR_RECOMMENDED_VERSION_CEHCK_EVENT = "RecommendedVersionCheck";
  private static bool isAutoEventTeleportMode = true;
  private GameSceneTables tables;
  private GameSectionHistory history;
  private GameSectionHierarchy hierarchy;
  private GameSceneGlobalSettings global;
  private long notifyFlags;
  private IEnumerator notifyCoroutine;
  private int downloadErrorResult;
  private static readonly List<GameSceneTables.TextData> emptyTextList = new List<GameSceneTables.TextData>();
  private static bool Use_Force = true;
  private static bool Lock_Change_Scene = false;
  private PriorityQueue<GameSceneManager.GameSceneTask> q_ForceTask = new PriorityQueue<GameSceneManager.GameSceneTask>();
  private int doWaitEventCount;
  private EventData[] autoEvents;
  private System.Action onAutoEventFinished;
  private Action<string> commonDialogCallback;
  private GameSceneEvent commonDialogSaveCurrentEvent;
  private string commonDialogResult;

  public static bool isAutoEventSkip
  {
    get
    {
      return MonoBehaviourSingleton<GameSceneManager>.IsValid() && MonoBehaviourSingleton<GameSceneManager>.I.autoEvents != null && GameSceneManager.isAutoEventTeleportMode;
    }
  }

  public bool isInitialized { get; private set; }

  protected override void Awake()
  {
    base.Awake();
    this.global = new GameSceneGlobalSettings();
    this.history = new GameSectionHistory();
    this.hierarchy = new GameSectionHierarchy();
    Object.DontDestroyOnLoad((Object) this);
    GameSceneEvent.Initialize();
    MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.global.OnScreenRotate);
  }

  public void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    this.isInitialized = false;
    this.tables = new GameSceneTables();
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    ResourceManager.enableCache = false;
    LoadObject lo_common_table = loadingQueue.Load(RESOURCE_CATEGORY.TABLE, "CommonDialogTable");
    ResourceManager.enableCache = true;
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    this.tables.CreateCommonResourceTable(lo_common_table.loadedObject as TextAsset);
    this.isInitialized = true;
  }

  private void OnEnable()
  {
    MonoBehaviourSingleton<ResourceManager>.I.onDownloadErrorQuery = new Func<bool, Error, int>(this.OnDownloadErrorQuery);
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    if (!MonoBehaviourSingleton<ResourceManager>.IsValid())
      return;
    MonoBehaviourSingleton<ResourceManager>.I.onDownloadErrorQuery = (Func<bool, Error, int>) null;
  }

  private int OnDownloadErrorQuery(bool is_init, Error error_code)
  {
    if (is_init)
    {
      if (this.isOpenCommonDialog)
        return -1;
      this.downloadErrorResult = 0;
      this.OpenCommonDialog_(new CommonDialog.Desc(CommonDialog.TYPE.YES_NO, StringTable.GetErrorMessage((uint) error_code), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 110U), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 111U)), (Action<string>) (btn =>
      {
        if (btn == "YES")
        {
          this.downloadErrorResult = 1;
        }
        else
        {
          MonoBehaviourSingleton<AppMain>.I.Reset();
          this.downloadErrorResult = -1;
        }
      }), true, true);
    }
    return this.downloadErrorResult;
  }

  public virtual void SetNotify(GameSection.NOTIFY_FLAG flag)
  {
    this.notifyFlags = (long) ((GameSection.NOTIFY_FLAG) this.notifyFlags | flag);
    if (this.notifyCoroutine != null)
      return;
    this.StartCoroutine(this.notifyCoroutine = this.DoNotifyUpdate());
  }

  private IEnumerator DoNotifyUpdate()
  {
    while (this.notifyFlags != 0L)
    {
      yield return (object) null;
      if (this.notifyFlags != 0L && !GameSceneEvent.IsStay() && !Protocol.isBusy && !this.isWaiting)
      {
        bool save_isWaiting = this.isWaiting;
        this.isWaiting = true;
        MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.NOTIFY, true);
        try
        {
          this.DoNotify((GameSection.NOTIFY_FLAG) this.notifyFlags);
        }
        catch (Exception ex)
        {
          Log.Exception(ex);
        }
        this.notifyFlags = 0L;
        while (Protocol.isBusy)
          yield return (object) null;
        MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.NOTIFY, false);
        this.isWaiting = save_isWaiting;
      }
    }
    this.notifyCoroutine = (IEnumerator) null;
  }

  private void DoNotify(GameSection.NOTIFY_FLAG flags)
  {
    this.hierarchy.DoNotify(flags);
    MonoBehaviourSingleton<UIManager>.I.OnNotify(flags);
  }

  private void Send(bool set_wait_flag, MonoBehaviour target, string func, object param = null)
  {
    if (this.isOpenCommonDialog)
      return;
    bool isWaiting = this.isWaiting;
    if (set_wait_flag)
      this.isWaiting = true;
    if (param == null)
      ((Component) target).SendMessage(func, (SendMessageOptions) 1);
    else
      ((Component) target).SendMessage(func, param, (SendMessageOptions) 1);
    this.isWaiting = isWaiting;
  }

  public GameSection GetCurrentScene() => this.hierarchy.GetTyped(GAME_SECTION_TYPE.SCENE)?.section;

  public GameSection GetCurrentScreen()
  {
    return this.hierarchy.GetTyped(GAME_SECTION_TYPE.SCREEN)?.section;
  }

  public GameSection GetCurrentSection() => this.hierarchy.GetLast()?.section;

  public GameSection GetLastSectionExcludeDialog()
  {
    return this.hierarchy.GetLastExcludeDialog()?.section;
  }

  public GameSection GetLastSectionExcludeCommonDialog()
  {
    return this.hierarchy.GetLastExcludeCommonDialog()?.section;
  }

  public GameSection FindSection(string section_name) => this.hierarchy.Find(section_name)?.section;

  public bool ExistHistory(string section_name) => this.history.Exist(section_name);

  public void RemoveHistory(string section_name) => this.history.RemoveSection(section_name);

  public string GetCurrentSceneName()
  {
    GameSectionHierarchy.HierarchyData typed = this.hierarchy.GetTyped(GAME_SECTION_TYPE.SCENE);
    return typed == null ? string.Empty : typed.data.sectionName;
  }

  public string GetCurrentScreenName()
  {
    GameSectionHierarchy.HierarchyData typed = this.hierarchy.GetTyped(GAME_SECTION_TYPE.SCREEN);
    return typed == null ? string.Empty : typed.data.sectionName;
  }

  public string GetCurrentSectionName() => this.hierarchy.GetLast()?.data.sectionName;

  public string GetPrevSectionNameFromHistory() => this.history.GetLast(2)?.sectionName;

  public GAME_SECTION_TYPE GetCurrentSectionType()
  {
    GameSectionHierarchy.HierarchyData last = this.hierarchy.GetLast();
    return last == null ? GAME_SECTION_TYPE.NONE : last.data.type;
  }

  public string[] GetCurrentSectionTypeParams() => this.hierarchy.GetLast()?.data.typeParams;

  public List<GameSceneTables.TextData> GetCurrentSectionTextList()
  {
    GameSectionHierarchy.HierarchyData last = this.hierarchy.GetLast();
    if (last == null)
      return (List<GameSceneTables.TextData>) null;
    return last.data.textList == null ? GameSceneManager.emptyTextList : last.data.textList;
  }

  public void ClearHistory() => this.history.Clear();

  public List<GameSectionHistory.HistoryData> GetHistoryList() => this.history.GetHistoryList();

  public List<GameSectionHierarchy.HierarchyData> GetHierarchyList()
  {
    return this.hierarchy.GetHierarchyList();
  }

  public bool isChangeing { get; private set; }

  public bool isWaiting { get; private set; }

  public bool isCallingOnQuery { get; private set; }

  public bool skipTrantisionEnd { get; set; }

  public string prev_scene_name { get; private set; }

  public static void StopForce() => GameSceneManager.Use_Force = !GameSceneManager.Use_Force;

  public static void LockChangeScene(bool enable) => GameSceneManager.Lock_Change_Scene = enable;

  public void AddHighForceChangeScene(
    string scene_name,
    string section_name = null,
    bool internal_res = false,
    UITransition.TYPE close_type = UITransition.TYPE.CLOSE,
    UITransition.TYPE open_type = UITransition.TYPE.OPEN,
    bool error = false,
    bool reloadSceneFlag = false)
  {
    this.AddForceScene(0, scene_name, section_name, internal_res, close_type, open_type, error, reloadSceneFlag);
  }

  public void AddNormalForceChangeScene(
    string scene_name,
    string section_name = null,
    bool internal_res = false,
    UITransition.TYPE close_type = UITransition.TYPE.CLOSE,
    UITransition.TYPE open_type = UITransition.TYPE.OPEN,
    bool error = false,
    bool reloadSceneFlag = false)
  {
    this.AddForceScene(10000, scene_name, section_name, internal_res, close_type, open_type, error, reloadSceneFlag);
  }

  public void AddLowForceChangeScene(
    string scene_name,
    string section_name = null,
    bool internal_res = false,
    UITransition.TYPE close_type = UITransition.TYPE.CLOSE,
    UITransition.TYPE open_type = UITransition.TYPE.OPEN,
    bool error = false,
    bool reloadSceneFlag = false)
  {
    this.AddForceScene(100000, scene_name, section_name, internal_res, close_type, open_type, error, reloadSceneFlag);
  }

  private void AddForceScene(
    int piority,
    string scene_name,
    string section_name = null,
    bool internal_res = false,
    UITransition.TYPE close_type = UITransition.TYPE.CLOSE,
    UITransition.TYPE open_type = UITransition.TYPE.OPEN,
    bool error = false,
    bool reloadSceneFlag = false)
  {
    if (!GameSceneManager.Use_Force || this.q_ForceTask.Find($"{scene_name}_{(string.IsNullOrEmpty(section_name) ? "" : section_name)}") != null)
      return;
    this.q_ForceTask.Enqueue(new GameSceneManager.GameSceneTask(piority)
    {
      SceneName = scene_name,
      SectionName = section_name,
      Error = error,
      InternalRes = internal_res,
      CloseType = close_type,
      OpenType = open_type,
      ReloadSceneFlag = reloadSceneFlag
    });
  }

  public void RemoveForceChangeScene(string scene_name, string section_name = null)
  {
    string id = $"{scene_name}_{(string.IsNullOrEmpty(section_name) ? "" : section_name)}";
    GameSceneManager.GameSceneTask gameSceneTask = this.q_ForceTask.Find(id);
    if (id == null)
      return;
    this.q_ForceTask.Remove(gameSceneTask);
  }

  public void RemoveForceChangeSceneAll() => this.q_ForceTask.Clear();

  public void ChangeScene(
    string scene_name,
    string section_name = null,
    UITransition.TYPE close_type = UITransition.TYPE.CLOSE,
    UITransition.TYPE open_type = UITransition.TYPE.OPEN,
    bool error = false)
  {
    if (AppMain.isApplicationQuit)
      return;
    this.StartCoroutine(this.DoChangeScene(scene_name, section_name, error, ResourceManager.internalMode, close_type, open_type, false));
  }

  private void ChangeCommonDialog(
    string scene_name,
    string section_name,
    bool error,
    bool internal_res)
  {
    this.StartCoroutine(this.DoChangeScene(scene_name, section_name, error, internal_res, UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false));
  }

  public void ReloadScene(UITransition.TYPE close_type = UITransition.TYPE.CLOSE, UITransition.TYPE open_type = UITransition.TYPE.OPEN, bool error = false)
  {
    this.StartCoroutine(this.DoChangeScene(this.GetCurrentSceneName().Replace("Scene", ""), (string) null, error, ResourceManager.internalMode, close_type, open_type, true));
  }

  public void ChangeSectionBack() => this.ChangeScene("[BACK]");

  private IEnumerator DoChangeScene(
    string scene_name,
    string section_name,
    bool error,
    bool internal_res,
    UITransition.TYPE close_type,
    UITransition.TYPE open_type,
    bool reloadSceneFlag)
  {
    if (this.isChangeing && !error && (!this.isWaiting || this.commonDialogCallback == null))
    {
      Log.Error("Error DoChangeScene : scene={0} ,section={1}", (object) scene_name, (object) section_name);
      GameSceneEvent.request = (GameSceneEvent) null;
    }
    else
    {
      while (GameSceneManager.Lock_Change_Scene)
        yield return (object) null;
      if (this.q_ForceTask.Count() > 0 && this.GetCurrentScreenName() != "InGameScene" && GameSceneManager.Use_Force)
      {
        GameSceneManager.GameSceneTask gameSceneTask = this.q_ForceTask.Dequeue();
        yield return (object) this.DoChangeScene(gameSceneTask.SceneName, gameSceneTask.SectionName, gameSceneTask.Error, gameSceneTask.InternalRes, gameSceneTask.CloseType, gameSceneTask.OpenType, gameSceneTask.ReloadSceneFlag);
      }
      else
      {
        bool save_isChangeing = this.isChangeing;
        this.isChangeing = true;
        CrashlyticsReporter.SetSceneInfo(scene_name, section_name);
        CrashlyticsReporter.SetSceneStatus(this.isChangeing);
        MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.SCENE_CHANGE, true);
        GameSectionHierarchy.HierarchyData dialog_hierarchy_data;
        if (this.commonDialogResult != null)
        {
          dialog_hierarchy_data = this.hierarchy.GetLast();
          dialog_hierarchy_data.section.Close(close_type);
          while (dialog_hierarchy_data.section.state != UIBehaviour.STATE.CLOSE)
            yield return (object) null;
          this.hierarchy.DestroyHierarchy(dialog_hierarchy_data);
          Action<string> commonDialogCallback = this.commonDialogCallback;
          string commonDialogResult = this.commonDialogResult;
          this.commonDialogResult = (string) null;
          this.commonDialogCallback = (Action<string>) null;
          this.isOpenImportantDialog = false;
          bool isWaiting = this.isWaiting;
          this.isWaiting = true;
          if (commonDialogCallback != null)
            commonDialogCallback(commonDialogResult);
          this.isWaiting = isWaiting;
          GameSceneEvent.current = this.commonDialogSaveCurrentEvent;
          this.commonDialogSaveCurrentEvent = (GameSceneEvent) null;
          MonoBehaviourSingleton<UIManager>.I.UpdateDialogBlocker(this.hierarchy, (GameSceneTables.SectionData) null);
          if (!save_isChangeing)
            MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.SCENE_CHANGE, false);
          this.isChangeing = save_isChangeing;
          CrashlyticsReporter.SetSceneStatus(this.isChangeing);
        }
        else
        {
          this.prev_scene_name = this.GetCurrentSceneName();
          string prev_section_name = this.GetCurrentSectionName();
          GameSectionHistory.HistoryData historyData = (GameSectionHistory.HistoryData) null;
          if (scene_name == "[BACK]")
          {
            this.history.PopSection();
            this.history.CutSingleDialog();
            historyData = this.history.GetLast();
          }
          if (historyData != null)
          {
            scene_name = historyData.sceneName;
            section_name = historyData.sectionName;
          }
          string scene_section_name;
          if (string.IsNullOrEmpty(scene_name))
          {
            scene_section_name = this.GetCurrentSceneName();
            GameSceneTables.SceneData dataFromSectionName = this.tables.GetSceneDataFromSectionName(section_name);
            if (dataFromSectionName != (GameSceneTables.SceneData) null)
            {
              scene_name = dataFromSectionName.sceneName;
            }
            else
            {
              GameSectionHistory.HistoryData last = this.history.GetLast();
              scene_name = last == null ? scene_section_name.Replace("Scene", "") : last.sceneName;
            }
          }
          else
            scene_section_name = scene_name + "Scene";
          if (scene_name != this.prev_scene_name)
          {
            bool isWait = false;
            if ((scene_name == "Home" || scene_name == "Clan") && MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge())
            {
              isWait = true;
              Protocol.Force((System.Action) (() => MonoBehaviourSingleton<LoungeMatchingManager>.I.SendLeave((Action<bool>) (isSuccess => isWait = false))));
            }
            if ((scene_name == "Home" || scene_name == "Lounge") && MonoBehaviourSingleton<ClanMatchingManager>.I.IsInClan())
            {
              isWait = true;
              Protocol.Force((System.Action) (() => MonoBehaviourSingleton<ClanMatchingManager>.I.SendLeaveFromClanBase((Action<bool>) (isSuccess => isWait = false))));
            }
            while (isWait)
              yield return (object) null;
          }
          this.DoNotify(GameSection.NOTIFY_FLAG.PRETREAT_SCENE);
          GameSection new_scene_section = (GameSection) null;
          GameSection new_section = (GameSection) null;
          GameSceneTables.SceneData new_scene_data = (GameSceneTables.SceneData) null;
          GameSceneTables.SectionData new_scene_section_data = (GameSceneTables.SectionData) null;
          GameSceneTables.SectionData new_section_data = (GameSceneTables.SectionData) null;
          bool global_init_section = false;
          if (this.isOpenCommonDialog)
            global_init_section = true;
          LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
          new_scene_data = this.tables.GetSceneData(scene_name, section_name);
          if (new_scene_data == (GameSceneTables.SceneData) null)
          {
            string resource_name = scene_section_name + "Table";
            bool enableCache = ResourceManager.enableCache;
            bool internalMode = ResourceManager.internalMode;
            bool flag = internal_res;
            if (!flag && Object.op_Inequality((Object) MonoBehaviourSingleton<ResourceManager>.I.manifest, (Object) null))
            {
              flag = true;
              if (MonoBehaviourSingleton<GlobalSettingsManager>.IsValid() && !AppMain.CheckApplicationVersion(MonoBehaviourSingleton<GlobalSettingsManager>.I.ignoreExternalSceneTableNamesAppVer))
              {
                List<string> externalSceneTableNames = MonoBehaviourSingleton<GlobalSettingsManager>.I.useExternalSceneTableNames;
                if (externalSceneTableNames != null)
                {
                  int index = 0;
                  for (int count = externalSceneTableNames.Count; index < count; ++index)
                  {
                    if (externalSceneTableNames[index] == resource_name)
                    {
                      flag = false;
                      break;
                    }
                  }
                }
              }
            }
            ResourceManager.enableCache = false;
            ResourceManager.internalMode = flag;
            LoadObject lo_scene_table = load_queue.Load(RESOURCE_CATEGORY.TABLE, resource_name);
            ResourceManager.enableCache = enableCache;
            ResourceManager.internalMode = internalMode;
            while (load_queue.IsLoading())
              yield return (object) load_queue.Wait();
            if (Object.op_Equality(lo_scene_table.loadedObject, (Object) null))
              yield break;
            new_scene_data = this.tables.CreateSceneData(scene_name, lo_scene_table.loadedObject as TextAsset);
            lo_scene_table = (LoadObject) null;
          }
          new_scene_section_data = new_scene_data.GetSectionData(scene_section_name);
          GameSectionHierarchy.HierarchyData typed = this.hierarchy.GetTyped(GAME_SECTION_TYPE.SCENE);
          List<GameSectionHierarchy.HierarchyData> exclusive_list;
          LoadObject[] load_objs;
          bool save_isWaiting;
          if (new_scene_section_data != (GameSceneTables.SectionData) null && ((typed == null ? 1 : (typed.data != new_scene_section_data ? 1 : 0)) | (reloadSceneFlag ? 1 : 0)) != 0)
          {
            this.global.ChangeSection(new_scene_data, (GameSceneTables.SectionData) null);
            exclusive_list = this.isOpenImportantDialog ? new List<GameSectionHierarchy.HierarchyData>() : this.hierarchy.GetExclusiveList(GAME_SECTION_TYPE.SCENE);
            exclusive_list.ForEach((Action<GameSectionHierarchy.HierarchyData>) (o => o.section.Close(close_type)));
            while (MonoBehaviourSingleton<UIManager>.I.IsTransitioning())
              yield return (object) null;
            if (!GameSceneManager.isAutoEventSkip)
            {
              if (!MonoBehaviourSingleton<TransitionManager>.I.isTransing && !this.isOpenCommonDialog)
              {
                TransitionManager.TYPE transitionType = this.global.GetTransitionType(this.prev_scene_name, prev_section_name, scene_section_name, section_name);
                if (transitionType != TransitionManager.TYPE.NONE)
                  yield return (object) MonoBehaviourSingleton<TransitionManager>.I.Out(transitionType);
              }
              else
              {
                while (MonoBehaviourSingleton<TransitionManager>.I.isChanging)
                  yield return (object) null;
              }
            }
            save_isWaiting = this.isWaiting;
            this.isWaiting = true;
            exclusive_list.ForEach((Action<GameSectionHierarchy.HierarchyData>) (o => o.section.Exit()));
            while (exclusive_list.Find((Predicate<GameSectionHierarchy.HierarchyData>) (o => !o.section.isExited)) != null)
              yield return (object) null;
            this.isWaiting = save_isWaiting;
            this.hierarchy.DestroyHierarchy(exclusive_list);
            if (this.global.SceneClear(this.prev_scene_name, prev_section_name, scene_section_name))
            {
              if (MonoBehaviourSingleton<InstantiateManager>.IsValid())
                MonoBehaviourSingleton<InstantiateManager>.I.ClearStocks();
              int num = new_scene_section_data.sectionName == "InGameScene" ? 1 : 0;
              yield return (object) MonoBehaviourSingleton<AppMain>.I.ClearMemory(true, true);
            }
            save_isWaiting = this.isWaiting;
            this.isWaiting = true;
            this.global.SceneInitialize(this.prev_scene_name, scene_section_name, scene_name.Contains("TutorialWeaponSelect"));
            while (!this.global.isInitialized || this.IsBusy(error))
              yield return (object) null;
            this.isWaiting = save_isWaiting;
            int num1 = ResourceManager.internalMode ? 1 : 0;
            ResourceManager.internalMode = internal_res;
            load_objs = new_scene_section_data.LoadUseResources(load_queue);
            ResourceManager.internalMode = num1 != 0;
            if (load_queue.IsLoading())
              yield return (object) load_queue.Wait();
            new_scene_section = this.hierarchy.CreateSection(new_scene_section_data, load_objs);
            if (string.IsNullOrEmpty(section_name))
            {
              GameSceneTables.EventData eventData = new_scene_section_data.GetEventData("");
              if (eventData != null)
                section_name = eventData.toSectionName;
            }
            exclusive_list = (List<GameSectionHierarchy.HierarchyData>) null;
            load_objs = (LoadObject[]) null;
          }
          else
          {
            scene_section_name = this.GetCurrentSceneName();
            if (string.IsNullOrEmpty(section_name))
            {
              if (new_scene_section_data == (GameSceneTables.SectionData) null)
                section_name = scene_name + "Top";
              if (typed.data == new_scene_section_data)
              {
                GameSectionHierarchy.HierarchyData last = this.hierarchy.GetLast();
                if (last != null && last.data.type == GAME_SECTION_TYPE.COMMON_DIALOG)
                {
                  GameSectionHierarchy.HierarchyData excludeCommonDialog = this.hierarchy.GetLastExcludeCommonDialog();
                  if (excludeCommonDialog != null && excludeCommonDialog.data.type != GAME_SECTION_TYPE.SCENE)
                    section_name = excludeCommonDialog.data.sectionName;
                }
              }
            }
          }
          if (!string.IsNullOrEmpty(section_name))
          {
            new_section_data = new_scene_data.GetSectionData(section_name);
            if (new_section_data == (GameSceneTables.SectionData) null)
            {
              Log.Error(LOG.GAMESCENE, "[ {0} ] is not found, in {1}", (object) section_name, (object) new_scene_data.sceneName);
              yield break;
            }
            this.global.ChangeSection((GameSceneTables.SceneData) null, new_section_data);
            this.global.StageSetup(this.prev_scene_name, scene_section_name, section_name, new_section_data);
            dialog_hierarchy_data = this.hierarchy.GetLast();
            GameSectionHierarchy.HierarchyData now_hierarchy_data = this.hierarchy.FindIgnoreSingle(new_section_data);
            if (now_hierarchy_data == null)
            {
              MonoBehaviourSingleton<UIManager>.I.UpdateMainUI(scene_section_name, section_name);
              exclusive_list = this.isOpenImportantDialog ? new List<GameSectionHierarchy.HierarchyData>() : this.hierarchy.GetExclusiveList(new_section_data.type);
              exclusive_list.ForEach((Action<GameSectionHierarchy.HierarchyData>) (o => o.section.Close(close_type)));
              MonoBehaviourSingleton<UIManager>.I.UpdateDialogBlocker(this.hierarchy, new_section_data);
              while (MonoBehaviourSingleton<UIManager>.I.IsTransitioning())
                yield return (object) null;
              if (!GameSceneManager.isAutoEventSkip)
              {
                if (!MonoBehaviourSingleton<TransitionManager>.I.isTransing && !new_section_data.type.IsDialog())
                {
                  TransitionManager.TYPE transitionType = this.global.GetTransitionType(this.prev_scene_name, prev_section_name, scene_section_name, section_name);
                  if (transitionType != TransitionManager.TYPE.NONE)
                    yield return (object) MonoBehaviourSingleton<TransitionManager>.I.Out(transitionType);
                }
                else
                {
                  while (MonoBehaviourSingleton<TransitionManager>.I.isChanging)
                    yield return (object) null;
                }
              }
              if (dialog_hierarchy_data != null)
                this.Send(true, (MonoBehaviour) dialog_hierarchy_data.section, "OnChangePretreat", (object) $"{section_name}@{scene_name}");
              if (new_section_data.type == GAME_SECTION_TYPE.PAGE && !new_section_data.isTop)
              {
                List<GameSectionHierarchy.HierarchyData> list = new List<GameSectionHierarchy.HierarchyData>();
                exclusive_list.ForEach((Action<GameSectionHierarchy.HierarchyData>) (o =>
                {
                  if (o.data.type == GAME_SECTION_TYPE.PAGE)
                    return;
                  list.Add(o);
                }));
                exclusive_list = list;
              }
              else if (new_section_data.type == GAME_SECTION_TYPE.PAGE_DIALOG)
              {
                List<GameSectionHierarchy.HierarchyData> list = new List<GameSectionHierarchy.HierarchyData>();
                exclusive_list.ForEach((Action<GameSectionHierarchy.HierarchyData>) (o =>
                {
                  if (o.data.type == GAME_SECTION_TYPE.PAGE_DIALOG)
                    return;
                  list.Add(o);
                }));
                exclusive_list = list;
              }
              save_isWaiting = this.isWaiting;
              this.isWaiting = true;
              exclusive_list.ForEach((Action<GameSectionHierarchy.HierarchyData>) (o => o.section.Exit()));
              while (exclusive_list.Find((Predicate<GameSectionHierarchy.HierarchyData>) (o => !o.section.isExited)) != null)
                yield return (object) null;
              this.isWaiting = save_isWaiting;
              this.hierarchy.DestroyHierarchy(exclusive_list);
              MonoBehaviourSingleton<UIManager>.I.UpdateDialogBlocker(this.hierarchy, new_section_data);
              load_objs = (LoadObject[]) null;
              int num = ResourceManager.internalMode ? 1 : 0;
              ResourceManager.internalMode = internal_res;
              if (new_section_data.type != GAME_SECTION_TYPE.COMMON_DIALOG)
              {
                load_objs = new_section_data.LoadUseResources(load_queue);
              }
              else
              {
                string commonResourceName = this.tables.GetCommonResourceName(new_section_data.typeParams[0]);
                load_objs = new LoadObject[1]
                {
                  load_queue.Load(RESOURCE_CATEGORY.UI, commonResourceName)
                };
              }
              ResourceManager.internalMode = num != 0;
              if (load_queue.IsLoading())
                yield return (object) load_queue.Wait();
              while (this.IsBusy(error))
                yield return (object) null;
              if (scene_name.Contains("TutorialWeaponSelect"))
              {
                while (MonoBehaviourSingleton<LoadingProcess>.IsValid())
                  yield return (object) null;
              }
              new_section = this.hierarchy.CreateSection(new_section_data, load_objs);
              exclusive_list = (List<GameSectionHierarchy.HierarchyData>) null;
              load_objs = (LoadObject[]) null;
            }
            else if (dialog_hierarchy_data != now_hierarchy_data)
            {
              exclusive_list = this.isOpenImportantDialog ? new List<GameSectionHierarchy.HierarchyData>() : this.hierarchy.GetCutList(now_hierarchy_data);
              exclusive_list.ForEach((Action<GameSectionHierarchy.HierarchyData>) (o => o.section.Close(close_type)));
              MonoBehaviourSingleton<UIManager>.I.UpdateDialogBlocker(this.hierarchy, new_section_data);
              while (MonoBehaviourSingleton<UIManager>.I.IsTransitioning())
                yield return (object) null;
              if (!GameSceneManager.isAutoEventSkip)
              {
                if (!MonoBehaviourSingleton<TransitionManager>.I.isTransing && !new_section_data.type.IsDialog())
                {
                  GameSectionHierarchy.HierarchyData lastExcludeDialog = this.hierarchy.GetLastExcludeDialog();
                  if ((dialog_hierarchy_data.data.type == GAME_SECTION_TYPE.PAGE || dialog_hierarchy_data.data.type.IsDialog() && lastExcludeDialog != null && lastExcludeDialog.data.type == GAME_SECTION_TYPE.PAGE && lastExcludeDialog != now_hierarchy_data) && (!dialog_hierarchy_data.data.type.IsDialog() || !new_section_data.type.IsDialog()))
                  {
                    TransitionManager.TYPE transitionType = this.global.GetTransitionType(this.prev_scene_name, prev_section_name, scene_section_name, section_name);
                    if (transitionType != TransitionManager.TYPE.NONE)
                      yield return (object) MonoBehaviourSingleton<TransitionManager>.I.Out(transitionType);
                  }
                }
                else
                {
                  while (MonoBehaviourSingleton<TransitionManager>.I.isChanging)
                    yield return (object) null;
                }
              }
              if (!global_init_section)
              {
                save_isWaiting = this.isWaiting;
                this.isWaiting = true;
                global_init_section = true;
                this.global.SectionInitialize(scene_section_name, section_name, new_section_data);
                while (!this.global.isInitialized || this.IsBusy(error))
                  yield return (object) null;
                this.isWaiting = save_isWaiting;
              }
              MonoBehaviourSingleton<UIManager>.I.UpdateMainUI(scene_section_name, section_name);
              if (now_hierarchy_data.section.state != UIBehaviour.STATE.OPEN)
              {
                save_isWaiting = this.isWaiting;
                this.isWaiting = true;
                now_hierarchy_data.section.isReOpenInitialized = false;
                now_hierarchy_data.section.InitializeReopen();
                while (!now_hierarchy_data.section.isReOpenInitialized || this.IsBusy(error))
                  yield return (object) null;
                this.isWaiting = save_isWaiting;
                now_hierarchy_data.section.Open(open_type);
              }
              while (MonoBehaviourSingleton<UIManager>.I.IsTransitioning())
                yield return (object) null;
              save_isWaiting = this.isWaiting;
              this.isWaiting = true;
              exclusive_list.ForEach((Action<GameSectionHierarchy.HierarchyData>) (o => o.section.Exit()));
              while (exclusive_list.Find((Predicate<GameSectionHierarchy.HierarchyData>) (o => !o.section.isExited)) != null)
                yield return (object) null;
              this.isWaiting = save_isWaiting;
              this.hierarchy.DestroyHierarchy(exclusive_list);
              MonoBehaviourSingleton<UIManager>.I.UpdateDialogBlocker(this.hierarchy, new_section_data);
              if (dialog_hierarchy_data != null && dialog_hierarchy_data.data.type.IsDialog())
              {
                this.Send(true, (MonoBehaviour) now_hierarchy_data.section, "OnCloseDialog", (object) dialog_hierarchy_data.data.sectionName);
                this.Send(true, (MonoBehaviour) now_hierarchy_data.section, "OnCloseDialog_" + dialog_hierarchy_data.data.sectionName);
                while (this.IsBusy(error))
                  yield return (object) null;
              }
              exclusive_list = (List<GameSectionHierarchy.HierarchyData>) null;
            }
            dialog_hierarchy_data = (GameSectionHierarchy.HierarchyData) null;
            now_hierarchy_data = (GameSectionHierarchy.HierarchyData) null;
          }
          if (new_section_data != (GameSceneTables.SectionData) null && !this.isOpenCommonDialog)
            this.history.Push(scene_name, section_name, new_section_data.type);
          if (!global_init_section)
          {
            save_isWaiting = this.isWaiting;
            this.isWaiting = true;
            global_init_section = true;
            this.global.SectionInitialize(scene_section_name, section_name, new_section_data);
            while (!this.global.isInitialized || this.IsBusy(error))
              yield return (object) null;
            this.isWaiting = save_isWaiting;
          }
          if (Object.op_Inequality((Object) new_scene_section, (Object) null))
          {
            save_isWaiting = this.isWaiting;
            this.isWaiting = true;
            new_section.LoadRequireDataTable();
            while (!new_section.isLoadedRequireDataTable)
              yield return (object) null;
            new_scene_section.Initialize();
            while (!new_scene_section.isInitialized || this.IsBusy(error))
              yield return (object) null;
            this.isWaiting = save_isWaiting;
            new_scene_section.Open(open_type);
            if (new_scene_section_data != (GameSceneTables.SectionData) null)
            {
              int num = ResourceManager.internalMode ? 1 : 0;
              ResourceManager.internalMode = internal_res;
              new_scene_section_data.LoadPreloadResources(load_queue);
              ResourceManager.internalMode = num != 0;
            }
          }
          bool shouldPreOpenCamera = false;
          if (!save_isChangeing && Object.op_Inequality((Object) new_scene_section, (Object) null) && MonoBehaviourSingleton<GlobalSettingsManager>.IsValid() && MonoBehaviourSingleton<GlobalSettingsManager>.I.stageShouldPreOpenCamera.Contains(section_name))
          {
            shouldPreOpenCamera = true;
            this.global.SectionSetup(scene_section_name, section_name, new_section_data);
            if (!GameSceneManager.isAutoEventSkip && !this.isOpenCommonDialog && (save_isChangeing || !this.skipTrantisionEnd) && this.global.IsTransitionEnd(this.prev_scene_name, prev_section_name, scene_section_name, section_name))
              MonoBehaviourSingleton<TransitionManager>.I.In();
          }
          if (Object.op_Inequality((Object) new_section, (Object) null))
          {
            save_isWaiting = this.isWaiting;
            this.isWaiting = true;
            new_section.LoadRequireDataTable();
            while (!new_section.isLoadedRequireDataTable)
              yield return (object) null;
            new_section.Initialize();
            while (!new_section.isInitialized)
              yield return (object) null;
            while (this.IsBusy(error))
              yield return (object) null;
            this.isWaiting = save_isWaiting;
            MonoBehaviourSingleton<UIManager>.I.UpdateDialogBlocker(this.hierarchy, new_section_data);
            new_section.Open(open_type);
            if (new_section_data != (GameSceneTables.SectionData) null)
            {
              int num = ResourceManager.internalMode ? 1 : 0;
              ResourceManager.internalMode = internal_res;
              new_section_data.LoadPreloadResources(load_queue);
              ResourceManager.internalMode = num != 0;
            }
          }
          if (!save_isChangeing)
          {
            this.DoNotify(GameSection.NOTIFY_FLAG.CHANGED_SCENE);
            MonoBehaviourSingleton<UIManager>.I.UpdateMainUI();
            if (MonoBehaviourSingleton<TransitionManager>.I.isTransing && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainMenu, (Object) null))
            {
              while (MonoBehaviourSingleton<UIManager>.I.mainMenu.state == UIBehaviour.STATE.TO_CLOSE)
                yield return (object) null;
            }
            if (!shouldPreOpenCamera)
            {
              this.global.SectionSetup(scene_section_name, section_name, new_section_data);
              if (!GameSceneManager.isAutoEventSkip && !this.isOpenCommonDialog && (save_isChangeing || !this.skipTrantisionEnd) && this.global.IsTransitionEnd(this.prev_scene_name, prev_section_name, scene_section_name, section_name))
                yield return (object) MonoBehaviourSingleton<TransitionManager>.I.In();
            }
            while (MonoBehaviourSingleton<UIManager>.I.IsTransitioning())
              yield return (object) null;
            MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.SCENE_CHANGE, false);
          }
          this.global.SectionStart(scene_section_name, section_name, Object.op_Inequality((Object) new_section, (Object) null));
          MonoBehaviourSingleton<UIManager>.I.UpdateDialogBlocker(this.hierarchy, new_section_data);
          this.isChangeing = save_isChangeing;
          CrashlyticsReporter.SetSceneStatus(this.isChangeing);
          if (Object.op_Inequality((Object) new_section, (Object) null))
            new_section.StartSection();
          if (!save_isChangeing && this.skipTrantisionEnd)
          {
            this.skipTrantisionEnd = false;
            if (!this.isChangeing)
              MonoBehaviourSingleton<TransitionManager>.I.In();
          }
          if (GameSceneEvent.request != null)
          {
            this.ExecuteSceneEvent("REQUEST", ((Component) this).gameObject, GameSceneEvent.request.eventName, GameSceneEvent.request.userData);
            GameSceneEvent.request = (GameSceneEvent) null;
          }
        }
      }
    }
  }

  private bool IsBusy() => Protocol.isBusy;

  private bool IsBusy(bool important) => !important && this.IsBusy();

  public void ExecuteSceneEvent(
    string caller,
    GameObject sender,
    string event_name,
    object user_data = null,
    string check_app_ver = null,
    bool is_send_query = true)
  {
    GameSectionHierarchy.HierarchyData last1 = this.hierarchy.GetLast();
    if (last1 == null)
      return;
    UIBehaviour uiBehaviour = (UIBehaviour) null;
    string sender_name;
    bool flag1;
    if (Object.op_Equality((Object) sender, (Object) ((Component) this).gameObject))
    {
      sender_name = string.Empty;
      flag1 = true;
    }
    else
    {
      if (Object.op_Inequality((Object) sender, (Object) null))
      {
        try
        {
          uiBehaviour = sender.GetComponentInParent<UIBehaviour>();
        }
        catch (Exception ex)
        {
          Log.Warning(LOG.SYSTEM, ex.ToString());
          uiBehaviour = (UIBehaviour) null;
        }
      }
      sender_name = Object.op_Inequality((Object) uiBehaviour, (Object) null) ? ((Object) uiBehaviour).name : string.Empty;
      flag1 = false;
    }
    if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.tutorialMessage, (Object) null))
    {
      if (MonoBehaviourSingleton<UIManager>.I.tutorialMessage.IsEnableMessage())
        MonoBehaviourSingleton<UIManager>.I.tutorialMessage.SubmitCursor(sender_name, event_name);
      MonoBehaviourSingleton<UIManager>.I.tutorialMessage.TriggerRun(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName(), MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName(), event_name);
    }
    if (string.IsNullOrEmpty(caller))
      Log.Error(LOG.GAMESCENE, "caller is empty.");
    else if (this.isChangeing && !this.isOpenImportantDialog)
    {
      if (!Object.op_Inequality((Object) sender, (Object) null))
        return;
      Log.Warning(LOG.GAMESCENE, "during scene change, so an event is ignored. {0}", (object) event_name);
    }
    else if (MonoBehaviourSingleton<UIManager>.I.IsTransitioning())
      Log.Warning(LOG.GAMESCENE, "during UI transitioning, so an event is ignored. {0}", (object) event_name);
    else if (this.IsBusy(this.isOpenImportantDialog))
    {
      if (!Protocol.strict && this.doWaitEventCount == 0)
        this.StartCoroutine(this.DoWaitEvent(caller, sender, event_name, user_data, check_app_ver, is_send_query));
      else
        Log.Warning(LOG.GAMESCENE, "protocol is busy, so an event is ignored. {0}", (object) event_name);
    }
    else if (GameSceneEvent.IsStay())
    {
      Log.Warning(LOG.GAMESCENE, "now staying, so an event is ignored. {0}", (object) event_name);
    }
    else
    {
      GameSection gameSection = (GameSection) null;
      if (Object.op_Inequality((Object) sender, (Object) null))
        gameSection = sender.GetComponentInParent<GameSection>();
      if (Object.op_Inequality((Object) gameSection, (Object) null) && !gameSection.isInitialized)
      {
        Log.Warning(LOG.GAMESCENE, "It's initialized, so an event is ignored. {0}", (object) event_name);
      }
      else
      {
        if (Object.op_Inequality((Object) gameSection, (Object) null) && last1.data.type.IsDialog() && Object.op_Inequality((Object) gameSection, (Object) last1.section) && !GameSceneGlobalSettings.IsGlobalEvent(event_name))
          return;
        GameSceneEvent.request = (GameSceneEvent) null;
        GameSceneEvent.current.eventName = event_name;
        GameSceneEvent.current.isExecute = true;
        GameSceneEvent.current.sender = sender;
        GameSceneEvent.current.userData = user_data;
        bool flag2 = event_name == "[BACK]";
        GameSceneTables.EventData eventData1 = !flag2 ? last1.data.GetEventData(GameSceneEvent.current.eventName) : last1.data.GetEventData("SECTION_BACK");
        if (eventData1 != null)
        {
          bool flag3 = true;
          if (!AppMain.CheckApplicationVersion(eventData1.appVer))
            flag3 = false;
          if (flag3 && !string.IsNullOrEmpty(check_app_ver) && !AppMain.CheckApplicationVersion(check_app_ver))
            flag3 = false;
          if (!flag3)
          {
            if (this.IsExecutionAutoEvent())
            {
              event_name = GameSceneEvent.current.eventName = "RecommendedVersionCheck";
              this.isOpenImportantDialog = true;
            }
            else
              event_name = GameSceneEvent.current.eventName = "APP_VERSION_RESTRICTION";
            GameSceneEvent.current.isExecute = false;
          }
        }
        if (is_send_query)
        {
          bool isCallingOnQuery = this.isCallingOnQuery;
          this.isCallingOnQuery = true;
          if (!flag2)
          {
            if (last1.data.type == GAME_SECTION_TYPE.COMMON_DIALOG && (sender_name == last1.data.sectionName || flag1))
            {
              if (this.commonDialogCallback == null)
              {
                GameSectionHierarchy.HierarchyData excludeCommonDialog = this.hierarchy.GetLastExcludeCommonDialog();
                if (excludeCommonDialog != null)
                  this.Send(false, (MonoBehaviour) excludeCommonDialog.section, $"OnQuery_{last1.data.sectionName}_{event_name}");
              }
            }
            else
              this.Send(false, (MonoBehaviour) last1.section, $"OnQuery_{event_name}");
          }
          else if (last1.data.type == GAME_SECTION_TYPE.COMMON_DIALOG)
          {
            if (this.commonDialogCallback == null)
            {
              GameSectionHierarchy.HierarchyData excludeCommonDialog = this.hierarchy.GetLastExcludeCommonDialog();
              if (excludeCommonDialog != null)
                this.Send(false, (MonoBehaviour) excludeCommonDialog.section, $"OnQuery_{last1.data.sectionName}_SECTION_BACK");
            }
          }
          else
            this.Send(false, (MonoBehaviour) last1.section, "OnQuery_SECTION_BACK");
          this.isCallingOnQuery = isCallingOnQuery;
        }
        if (!GameSceneEvent.current.isExecute)
          return;
        GameSceneEvent.current.isExecute = false;
        bool flag4 = GameSceneEvent.current.eventName == "[BACK]";
        bool error = false;
        if (this.commonDialogCallback != null)
        {
          this.commonDialogResult = GameSceneEvent.current.eventName;
          if (this.isOpenImportantDialog)
            error = true;
        }
        string scene_name = (string) null;
        string section_name = (string) null;
        UITransition.TYPE close_type = UITransition.TYPE.CLOSE;
        UITransition.TYPE open_type = UITransition.TYPE.OPEN;
        GameSceneTables.EventData eventData2;
        if (flag4)
        {
          eventData2 = last1.data.GetEventData("SECTION_BACK");
          if (eventData2 == null)
            scene_name = GameSceneEvent.current.eventName;
        }
        else
          eventData2 = last1.data.GetEventData(GameSceneEvent.current.eventName);
        if (eventData2 != null)
        {
          close_type = eventData2.closeType;
          open_type = eventData2.openType;
          string toSectionName = eventData2.toSectionName;
          if (toSectionName.Length > 0)
          {
            if (toSectionName.StartsWith("[HISTORY_"))
            {
              GameSectionHistory.HistoryData last2 = this.history.GetLast(int.Parse(toSectionName.Substring(9, toSectionName.IndexOf(']') - 9)), true);
              if (last2 != null)
              {
                scene_name = last2.sceneName;
                section_name = last2.sectionName;
              }
            }
            else
            {
              int length = toSectionName.IndexOf("@");
              switch (length)
              {
                case -1:
                  section_name = toSectionName;
                  break;
                case 0:
                  scene_name = toSectionName.Substring(1);
                  break;
                default:
                  section_name = toSectionName.Substring(0, length);
                  scene_name = toSectionName.Substring(length + 1);
                  break;
              }
              if (LoungeMatchingManager.IsValidInLounge())
              {
                if (scene_name == "Home")
                  scene_name = "Lounge";
                if (section_name == "HomeTop")
                  section_name = "LoungeTop";
              }
              else if (ClanMatchingManager.IsValidInClan() || MonoBehaviourSingleton<ClanManager>.IsValid())
              {
                if (scene_name == "Home")
                  scene_name = "Clan";
                if (section_name == "HomeTop")
                  section_name = "ClanTop";
              }
            }
          }
        }
        if (scene_name == null && section_name == null && (this.commonDialogCallback != null || last1.data.type == GAME_SECTION_TYPE.COMMON_DIALOG && (sender_name == last1.data.sectionName || flag1)))
          scene_name = "[BACK]";
        if (scene_name != null || section_name != null)
          this.ChangeScene(scene_name, section_name, close_type, open_type, error);
        else
          GameSceneEvent.request = (GameSceneEvent) null;
      }
    }
  }

  public bool IsEventExecutionPossible()
  {
    return !MonoBehaviourSingleton<UIManager>.I.IsDisable() && !GameSceneEvent.IsStay() && !Protocol.isBusy && !this.isOpenCommonDialog && !MonoBehaviourSingleton<UIManager>.I.IsTransitioning();
  }

  public bool IsBackKeyEventExecutionPossible()
  {
    return this.isOpenCommonDialog || this.IsEventExecutionPossible();
  }

  private IEnumerator DoWaitEvent(
    string caller,
    GameObject sender,
    string event_name,
    object user_data,
    string check_app_ver,
    bool is_send_query)
  {
    ++this.doWaitEventCount;
    do
    {
      yield return (object) null;
      if (Protocol.strict)
        goto label_4;
    }
    while (!this.IsEventExecutionPossible());
    --this.doWaitEventCount;
    this.ExecuteSceneEvent(caller, sender, event_name, user_data, check_app_ver, is_send_query);
    yield break;
label_4:
    --this.doWaitEventCount;
  }

  public bool IsExecutionAutoEvent() => this.autoEvents != null;

  public void SetAutoEvents(EventData[] event_datas)
  {
    if (event_datas == null || this.autoEvents != null)
    {
      if (event_datas == null)
        Log.Error(LOG.GAMESCENE, "event_datas == null");
      else
        Log.Error(LOG.GAMESCENE, "autoEvents != null");
    }
    else
    {
      this.autoEvents = event_datas;
      if (Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.tutorialMessage, (Object) null))
        MonoBehaviourSingleton<UIManager>.I.tutorialMessage.SetSkipSectionRunCount(this.autoEvents.Length - 1);
      this.StartCoroutine(this.DoAutoEvent());
    }
  }

  public void StopAutoEvent(System.Action on_finished = null)
  {
    if (this.autoEvents == null)
    {
      if (on_finished == null)
        return;
      on_finished();
    }
    else
    {
      this.autoEvents = (EventData[]) null;
      this.onAutoEventFinished = on_finished;
    }
  }

  private IEnumerator DoAutoEvent()
  {
    MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.AUTO_EVENT, true);
    if (!GameSceneManager.isAutoEventSkip)
      MonoBehaviourSingleton<OutGameEffectManager>.I.ShowAutoEventEffect();
    int index = 0;
    bool is_change_version_check_section = false;
    while (this.autoEvents != null)
    {
      yield return (object) null;
      if (this.notifyFlags == 0L && !this.isChangeing && !this.isOpenCommonDialog && !GameSceneEvent.IsStay() && (MonoBehaviourSingleton<UIManager>.I.disableFlags & UIManager.DISABLE_FACTOR.AUTO_EVENT) == UIManager.DISABLE_FACTOR.AUTO_EVENT)
      {
        if (this.autoEvents == null || index < this.autoEvents.Length)
        {
          if (GameSceneManager.isAutoEventSkip && !MonoBehaviourSingleton<TransitionManager>.I.isTransing)
            yield return (object) MonoBehaviourSingleton<TransitionManager>.I.Out(TransitionManager.TYPE.AUTO_EVENT);
          EventData event_data = this.autoEvents != null ? this.autoEvents[index] : (EventData) null;
          if (event_data != null)
          {
            EventData eventData = this.GetCurrentSection().CheckAutoEvent(event_data.name, event_data.data);
            if (eventData != null)
              event_data = eventData;
          }
          bool is_execute_event = event_data != null && !string.IsNullOrEmpty(event_data.name);
          if (!GameSceneManager.isAutoEventSkip && is_execute_event && MonoBehaviourSingleton<OutGameEffectManager>.IsValid())
          {
            UIGameSceneEventSender sender = (UIGameSceneEventSender) null;
            Utility.ForEach(MonoBehaviourSingleton<UIManager>.I._transform, (Predicate<Transform>) (t =>
            {
              UIGameSceneEventSender component = ((Component) t).GetComponent<UIGameSceneEventSender>();
              if (Object.op_Equality((Object) component, (Object) null) || component.eventName != event_data.name || component.eventData != null && event_data.data != null && !component.eventData.Equals(event_data.data))
                return false;
              sender = component;
              return true;
            }));
            if (Object.op_Inequality((Object) sender, (Object) null))
            {
              UIButton button = ((Component) sender).GetComponent<UIButton>();
              if (Object.op_Inequality((Object) button, (Object) null))
              {
                UIScrollView componentInParent = ((Component) button).GetComponentInParent<UIScrollView>();
                if (Object.op_Inequality((Object) componentInParent, (Object) null) && ((Behaviour) componentInParent).enabled)
                {
                  UIPanel component = ((Component) componentInParent).GetComponent<UIPanel>();
                  if (!component.IsVisible(((Component) button).GetComponent<UIWidget>()))
                  {
                    Vector3 pos1 = Vector3.op_UnaryNegation(component.cachedTransform.InverseTransformPoint(((Component) button).transform.position));
                    if (!componentInParent.canMoveHorizontally)
                      pos1.x = component.cachedTransform.localPosition.x;
                    if (!componentInParent.canMoveVertically)
                      pos1.y = component.cachedTransform.localPosition.y;
                    SpringPanel sp = SpringPanel.Begin(component.cachedGameObject, pos1, 16f);
                    bool wait = true;
                    SpringPanel.OnFinished func = (SpringPanel.OnFinished) (() => wait = false);
                    sp.onFinished += func;
                    while (wait)
                      yield return (object) null;
                    sp.onFinished -= func;
                    sp = (SpringPanel) null;
                    func = (SpringPanel.OnFinished) null;
                  }
                }
                Vector3 pos = ((Component) button).transform.position;
                yield return (object) MonoBehaviourSingleton<OutGameEffectManager>.I.MoveAutoEventEffect(pos);
                button.SetState(UIButtonColor.State.Pressed, false);
                MonoBehaviourSingleton<OutGameEffectManager>.I.PopTouchEffect(pos);
                yield return (object) new WaitForSeconds(0.2f);
                button.SetState(UIButtonColor.State.Normal, false);
                pos = new Vector3();
              }
              button = (UIButton) null;
            }
          }
          if (is_execute_event)
          {
            if (event_data.data != null && event_data.data.GetType() == typeof (EventListData))
            {
              if (((Network.EventData) event_data.data).eventType == 15)
              {
                if ((int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < 50)
                {
                  MonoBehaviourSingleton<GameSceneManager>.I.StopAutoEvent();
                  break;
                }
                this.ExecuteSceneEvent("AUTO", ((Component) this).gameObject, event_data.name + "_ARENA", event_data.data);
              }
              else
                this.ExecuteSceneEvent("AUTO", ((Component) this).gameObject, event_data.name, event_data.data);
            }
            else
              this.ExecuteSceneEvent("AUTO", ((Component) this).gameObject, event_data.name, event_data.data);
            if (GameSceneEvent.current.eventName == "RecommendedVersionCheck")
            {
              Array.Resize<EventData>(ref this.autoEvents, 1);
              this.autoEvents[0].name = "APP_VERSION_RESTRICTION_AUTO";
              this.autoEvents[0].data = (object) 0;
              index = -1;
              is_change_version_check_section = true;
            }
          }
          ++index;
          if ((this.autoEvents == null || index >= this.autoEvents.Length) && MonoBehaviourSingleton<OutGameEffectManager>.IsValid())
            MonoBehaviourSingleton<OutGameEffectManager>.I.HideAutoEventEffect();
        }
        else
          break;
      }
    }
    if (!is_change_version_check_section && MonoBehaviourSingleton<TransitionManager>.I.isTransing)
      yield return (object) MonoBehaviourSingleton<TransitionManager>.I.In();
    if (!GameSceneManager.isAutoEventSkip)
      MonoBehaviourSingleton<OutGameEffectManager>.I.HideAutoEventEffect();
    MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.AUTO_EVENT, false);
    this.autoEvents = (EventData[]) null;
    if (this.onAutoEventFinished != null)
    {
      this.onAutoEventFinished();
      this.onAutoEventFinished = (System.Action) null;
    }
  }

  public bool isOpenImportantDialog { get; private set; }

  public bool isOpenCommonDialog => this.commonDialogCallback != null;

  public void OpenCommonDialog(
    CommonDialog.Desc desc,
    Action<string> callback,
    bool error = false,
    int errorCode = 0)
  {
    this.OpenCommonDialog_(desc, callback, error, true, errorCode);
  }

  private void OpenCommonDialog_(
    CommonDialog.Desc desc,
    Action<string> callback,
    bool error,
    bool internal_res,
    int errorCode = 0)
  {
    if (callback == null)
      return;
    if (this.isOpenCommonDialog)
      this.StartCoroutine(this.DoWaitOpenCommonDialog(desc, callback, error, internal_res, errorCode));
    else if (this.isChangeing && !error)
    {
      Log.Error(LOG.GAMESCENE, "during scene change. error={0} isWaiting={1}", (object) error, (object) this.isWaiting);
    }
    else
    {
      this.commonDialogSaveCurrentEvent = GameSceneEvent.current;
      this.commonDialogCallback = callback;
      GameSceneEvent.current = new GameSceneEvent();
      GameSceneEvent.current.userData = (object) desc;
      this.isOpenImportantDialog = error;
      switch (errorCode)
      {
        case 1002:
          this.ChangeCommonDialog("CommonDialog", "CommonDialogMaintenanceError", error, internal_res);
          break;
        case 1020:
        case 1023 /*0x03FF*/:
          this.ChangeCommonDialog("CommonDialog", "CommonDialogTop", error, internal_res);
          break;
        case 70800:
          this.ChangeCommonDialog("CommonDialog", "YesNoDialogImportant", error, internal_res);
          break;
        default:
          if (errorCode > 500000 && errorCode < 600000)
          {
            this.ChangeCommonDialog("CommonDialog", error ? "CustomDialogError" : "CommonDialogTop", error, internal_res);
            break;
          }
          if (errorCode > 600000 && errorCode < 700000)
          {
            this.ChangeCommonDialog("CommonDialog", error ? "CustomDialogError" : "CommonDialogTop", error, internal_res);
            break;
          }
          this.ChangeCommonDialog("CommonDialog", error ? "CommonDialogError" : "CommonDialogTop", error, internal_res);
          break;
      }
    }
  }

  private IEnumerator DoWaitOpenCommonDialog(
    CommonDialog.Desc desc,
    Action<string> callback,
    bool error,
    bool internal_res,
    int errorCode = 0)
  {
    while (this.isOpenCommonDialog)
      yield return (object) null;
    this.OpenCommonDialog_(desc, callback, error, internal_res, errorCode);
  }

  public void OpenInfoDialog(Action<string> callback, bool error = false)
  {
    this.commonDialogSaveCurrentEvent = GameSceneEvent.current;
    this.commonDialogCallback = callback;
    this.isOpenImportantDialog = error;
    this.ChangeCommonDialog("CommonDialog", "InformationDialog", true, ResourceManager.internalMode);
  }

  public void OpenUpdateAppDialog(uint msg_id, bool is_yes_no, System.Action onCancel = null)
  {
    GameSceneEvent.PushStay();
    this.OpenCommonDialog(new CommonDialog.Desc(is_yes_no ? CommonDialog.TYPE.YES_NO : CommonDialog.TYPE.OK, StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, msg_id), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 101U), StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 102U)), (Action<string>) (ret =>
    {
      GameSceneEvent.PopStay();
      if (ret == "YES" || ret == "OK")
      {
        Native.launchMyselfMarket();
        MonoBehaviourSingleton<AppMain>.I.Reset();
      }
      else
      {
        if (onCancel == null)
          return;
        onCancel();
      }
    }), true);
  }

  public bool CheckPortalAndOpenUpdateAppDialog(
    uint portal_id,
    bool check_dst_quest,
    bool is_yes_no = true)
  {
    return portal_id == 0U || this.CheckPortalAndOpenUpdateAppDialog(Singleton<FieldMapTable>.I.GetPortalData(portal_id), check_dst_quest, is_yes_no);
  }

  public bool CheckPortalAndOpenUpdateAppDialog(
    FieldMapTable.PortalTableData portal_data,
    bool check_dst_quest,
    bool is_yes_no = true)
  {
    return portal_data == null || (!check_dst_quest || portal_data.dstQuestID == 0U || this.CheckQuestAndOpenUpdateAppDialog(portal_data.dstQuestID, is_yes_no)) && (portal_data.dstMapID == 0U || this.CheckMapAndOpenUpdateAppDialog(portal_data.dstMapID));
  }

  public bool CheckMapAndOpenUpdateAppDialog(uint map_id, bool is_yes_no = true)
  {
    if (map_id == 0U)
      return true;
    List<FieldMapTable.EnemyPopTableData> enemyPopList = Singleton<FieldMapTable>.I.GetEnemyPopList(map_id);
    if (enemyPopList != null)
    {
      int index = 0;
      for (int count = enemyPopList.Count; index < count; ++index)
      {
        if (enemyPopList[index] != null && enemyPopList[index].enemyID != 0U)
        {
          EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData(enemyPopList[index].enemyID);
          if (enemyData != null && !enemyData.IsEnableNowApplicationVersion())
          {
            this.OpenUpdateAppDialog(2003U, is_yes_no);
            return false;
          }
        }
      }
    }
    return true;
  }

  public bool CheckQuestAndOpenUpdateAppDialog(uint quest_id, bool is_yes_no = true)
  {
    return quest_id == 0U || this.CheckQuestAndOpenUpdateAppDialog(Singleton<QuestTable>.I.GetQuestData(quest_id), is_yes_no);
  }

  public bool CheckQuestAndOpenUpdateAppDialog(
    QuestTable.QuestTableData quest_data,
    bool is_yes_no = true,
    bool is_happen_quest = false)
  {
    if (quest_data != null)
    {
      int index = 0;
      for (int length = quest_data.enemyID.Length; index < length; ++index)
      {
        if (quest_data.enemyID[index] != 0)
        {
          EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint) quest_data.enemyID[index]);
          if (enemyData != null && !enemyData.IsEnableNowApplicationVersion())
          {
            this.OpenUpdateAppDialog(is_happen_quest ? 2002U : 2003U, is_yes_no, (System.Action) (() => GameSceneEvent.Cancel()));
            return false;
          }
        }
      }
    }
    return true;
  }

  public bool CheckEquipItemAndOpenUpdateAppDialog(uint equip_item_id, System.Action onCancel = null)
  {
    return equip_item_id == 0U || this.CheckEquipItemAndOpenUpdateAppDialog(Singleton<EquipItemTable>.I.GetEquipItemData(equip_item_id));
  }

  public bool CheckEquipItemAndOpenUpdateAppDialog(
    EquipItemTable.EquipItemData equip_item_data,
    System.Action onCancel = null)
  {
    if (equip_item_data == null || equip_item_data.IsEnableNowApplicationVersion())
      return true;
    this.OpenUpdateAppDialog(2000U, true, onCancel);
    return false;
  }

  public bool CheckSkillItemAndOpenUpdateAppDialog(uint skill_item_id, System.Action onCancel = null)
  {
    return skill_item_id == 0U || this.CheckSkillItemAndOpenUpdateAppDialog(Singleton<SkillItemTable>.I.GetSkillItemData(skill_item_id));
  }

  public bool CheckSkillItemAndOpenUpdateAppDialog(
    SkillItemTable.SkillItemData skill_item_data,
    System.Action onCancel = null)
  {
    if (skill_item_data == null || skill_item_data.IsEnableNowApplicationVersion())
      return true;
    this.OpenUpdateAppDialog(2001U, true, onCancel);
    return false;
  }

  [Obsolete]
  public bool CheckEquipAbilityAndOpenUpdateAppDialog(EquipItemInfo equipItemInfo, System.Action onCancel = null)
  {
    return true;
  }

  public void OpinionBox() => this.StartCoroutine(this.DoOpenOpinionBox());

  private IEnumerator DoOpenOpinionBox()
  {
    while (this.notifyFlags != 0L || this.isChangeing || this.isOpenCommonDialog || GameSceneEvent.IsStay())
      yield return (object) null;
    this.ChangeScene("OpinionBox", "OpinionTop");
  }

  public bool IsCurrentSceneHomeOrLounge()
  {
    return this.GetCurrentSceneName() == "HomeScene" || this.GetCurrentSceneName() == "LoungeScene" || this.GetCurrentSceneName() == "ClanScene" || this.GetCurrentSceneName() == "GuildScene";
  }

  public bool IsCurrentSceneMejorOutGameScene()
  {
    return this.GetCurrentSceneName() == "HomeScene" || this.GetCurrentSceneName() == "LoungeScene" || this.GetCurrentSceneName() == "ClanScene" || this.GetCurrentSceneName() == "StatusScene" || this.GetCurrentSceneName() == "ShopScene";
  }

  public void SetExternalStageName(string stage_name) => this.global.externalStageName = stage_name;

  public void SetMainCameraCullingMask(int mask) => this.global.SetMainCameraCullingMask(mask);

  public bool isAvailableScreenRotationScene()
  {
    return this.global != null && this.global.isAvailableScreenRotation(this.GetCurrentSceneName(), this.GetCurrentSectionName());
  }

  private class GameSceneTask : IComparable<GameSceneManager.GameSceneTask>
  {
    public int Priority;
    public string SceneName;
    public string SectionName;
    public bool Error;
    public bool InternalRes;
    public UITransition.TYPE CloseType;
    public UITransition.TYPE OpenType;
    public bool ReloadSceneFlag;

    public override string ToString() => $"{this.SceneName}_{this.SectionName}";

    public GameSceneTask(int priority) => this.Priority = priority;

    public int CompareTo(GameSceneManager.GameSceneTask other)
    {
      if (this == null && other == null)
        return 0;
      if (this == null)
        return -1;
      return other == null ? 1 : this.Priority.CompareTo(other.Priority);
    }
  }
}
