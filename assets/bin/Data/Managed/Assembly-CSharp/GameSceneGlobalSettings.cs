// Decompiled with JetBrains decompiler
// Type: GameSceneGlobalSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using UnityEngine;

#nullable disable
public class GameSceneGlobalSettings
{
  public string externalStageName;
  private string stageName;
  private bool stageForceLoad;
  private int stageImageID;
  private Animation cameraAnim;
  private AnimationClip cameraAnimClip;
  private bool needHomeManager;
  private bool needLoungeManager;
  private bool needClanManager;
  private bool needGuildManager;
  private bool needStatusStageManager;
  private float saveCameraNear = float.MinValue;
  private float saveCameraFar = float.MinValue;
  private int mainCameraCullingMask;
  private const string INGAME_PRESET = "InGame";

  public GameSceneGlobalSettings()
  {
    this.mainCameraCullingMask = GameSceneGlobalSettings.GetDefaultMainCameraCullingMask();
  }

  public bool isInitialized
  {
    get
    {
      if (MonoBehaviourSingleton<StageManager>.IsValid() && MonoBehaviourSingleton<StageManager>.I.isLoading)
        return false;
      IHomeManager currentIhomeManager = GameSceneGlobalSettings.GetCurrentIHomeManager();
      return (currentIhomeManager == null || currentIhomeManager.IsInitialized && !currentIhomeManager.HomeCamera.isChanging) && !MonoBehaviourSingleton<UIManager>.I.isLoading && !MonoBehaviourSingleton<GameSceneManager>.I.isOpenCommonDialog && (!MonoBehaviourSingleton<StatusStageManager>.IsValid() || !MonoBehaviourSingleton<StatusStageManager>.I.isBusy);
    }
  }

  public void ChangeSection(
    GameSceneTables.SceneData scene_data,
    GameSceneTables.SectionData section_data)
  {
    if (!Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.npcMessage, (Object) null))
      return;
    MonoBehaviourSingleton<UIManager>.I.npcMessage.UpdateMessage(section_data, false);
  }

  public TransitionManager.TYPE GetTransitionType(
    string prev_scene_name,
    string prev_section_name,
    string next_scene_name,
    string next_section_name)
  {
    if (prev_scene_name == null)
      prev_scene_name = string.Empty;
    if (prev_section_name == null)
      prev_section_name = string.Empty;
    if (next_scene_name == null)
      next_scene_name = string.Empty;
    if (next_section_name == null)
      next_section_name = string.Empty;
    if ((prev_section_name == "InGameMain" || prev_section_name == "QuestResultTop" || prev_section_name == "QuestResultDirection" || prev_section_name == "CarnivalResultPoint") && (next_section_name == "QuestResultDirection" || next_section_name == "QuestResultFriend" || next_section_name == "QuestResultTrialEnd"))
      return TransitionManager.TYPE.WHITE;
    if (prev_section_name == "InGameFieldQuestConfirm" || next_section_name == "InGameFieldQuestConfirm")
      return TransitionManager.TYPE.NONE;
    if (prev_scene_name != "InGameScene" && next_scene_name == "InGameScene")
      return TransitionManager.TYPE.LOADING;
    if ((prev_section_name == "WorldMapOpenNewField" || prev_section_name == "StoryMain") && (next_section_name == "InGameMain" || next_section_name == "InGameInterval"))
      return TransitionManager.TYPE.BLACK;
    if (next_section_name == "InGameMain" || next_section_name == "InGameInterval" || prev_scene_name == "TitleScene" && next_scene_name == "HomeScene" || prev_scene_name == "TitleScene" && next_scene_name == "LoungeScene" || prev_scene_name == "TitleScene" && next_scene_name == "ClanScene")
      return TransitionManager.TYPE.LOADING;
    bool flag = next_scene_name == "HomeScene" || next_scene_name == "LoungeScene" || next_scene_name == "ClanScene";
    if (((!(prev_scene_name == "InGameScene") ? 0 : (prev_section_name == "InGameQuestAcceptInvitation" ? 1 : 0)) & (flag ? 1 : 0)) != 0)
      return TransitionManager.TYPE.LOADING;
    if (prev_scene_name != next_scene_name && (prev_scene_name == "InGameScene" || next_scene_name == "ShopScene" || next_scene_name == "GachaScene"))
      return TransitionManager.TYPE.BLACK;
    switch (next_section_name)
    {
      case "GachaPerformanceSkill":
      case "GachaPerformanceQuest":
        return TransitionManager.TYPE.BLACK;
      case "SmithCreatePerformance":
      case "SmithGrowPerformance":
      case "SmithEvolvePerformance":
      case "SmithAbilityChangePerformance":
      case "SmithGrowSkillPerformance":
      case "SmithAbilityItemPerformance":
      case "SmithShadowEvolvePerformance":
      case "SmithExceedPerformance":
        return TransitionManager.TYPE.BLACK;
      default:
        if (!(prev_scene_name == "StoryScene"))
        {
          switch (next_scene_name)
          {
            case "StoryScene":
              goto label_27;
            case "FriendScene":
              if (prev_scene_name != "FriendScene")
                break;
              goto default;
            default:
              if ((!(next_scene_name != "FriendScene") || !(prev_scene_name == "FriendScene")) && (!(next_scene_name == "ProfileScene") || !(prev_scene_name != "ProfileScene")) && (!(next_scene_name != "ProfileScene") || !(prev_scene_name == "ProfileScene")) && (!(prev_scene_name == "TitleScene") || !(next_scene_name == "StatusScene")) && (!(prev_scene_name == "StatusScene") || !(next_scene_name == "TitleScene")) && MonoBehaviourSingleton<StageManager>.IsValid() && (MonoBehaviourSingleton<StageManager>.I.currentStageName == this.stageName || MonoBehaviourSingleton<StageManager>.I.backgroundImageID == this.stageImageID))
                return TransitionManager.TYPE.NONE;
              break;
          }
          return TransitionManager.TYPE.BLACK;
        }
label_27:
        return TransitionManager.TYPE.BLACK;
    }
  }

  public bool IsTransitionEnd(
    string prev_scene_name,
    string prev_section_name,
    string next_scene_name,
    string next_section_name)
  {
    return !next_section_name.StartsWith("InGameInterval");
  }

  public bool SceneClear(string prev_scene_name, string prev_section_name, string next_scene_name)
  {
    bool flag = true;
    if (prev_scene_name == "TitleScene" && next_scene_name == "InGameScene")
      flag = false;
    if (next_scene_name == "InGameScene")
    {
      MonoBehaviourSingleton<GameSceneManager>.I.ClearHistory();
      MonoBehaviourSingleton<UIManager>.I.DeleteUI();
    }
    else
    {
      switch (next_scene_name)
      {
        case "ClanScene":
        case "FriendScene":
        case "GachaScene":
        case "HomeScene":
        case "LoungeScene":
        case "ProfileScene":
        case "ShopScene":
        case "SmithScene":
        case "StatusScene":
        case "UniqueStatusScene":
          MonoBehaviourSingleton<GameSceneManager>.I.ClearHistory();
          break;
      }
      if (next_scene_name == "StatusScene" && prev_scene_name == "SmithScene" || next_scene_name == "SmithScene" && prev_scene_name == "StatusScene")
        flag = false;
      if (next_scene_name == "UniqueStatusScene" && prev_scene_name == "SmithScene" || next_scene_name == "SmithScene" && prev_scene_name == "UniqueStatusScene")
        flag = false;
      if (Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainMenu, (Object) null))
        MonoBehaviourSingleton<UIManager>.I.mainMenu.UpdateSceneButtons(next_scene_name);
    }
    return flag && AppMain.needClearMemory;
  }

  public void SceneInitialize(string prev_scene_name, string next_scene_name, bool skipChat = false)
  {
    if (next_scene_name != "InGameScene")
      MonoBehaviourSingleton<UIManager>.I.LoadUI(true, true, UserInfoManager.IsNeedsTutorialMessage(), skipChat);
    if (next_scene_name != "GachaScene" && next_scene_name != "ShopScene" && MonoBehaviourSingleton<GachaManager>.IsValid())
      MonoBehaviourSingleton<GachaManager>.I.ResetGachaType();
    this.ResetAudioRestener();
    this.UpdateBGM(next_scene_name, (string) null);
  }

  public void StageSetup(
    string prev_scene_name,
    string scene_name,
    string section_name,
    GameSceneTables.SectionData section_data)
  {
    this.stageForceLoad = false;
    if (!string.IsNullOrEmpty(this.externalStageName))
    {
      this.stageName = this.externalStageName;
      this.stageImageID = -1;
    }
    else
    {
      if (section_data != (GameSceneTables.SectionData) null && section_data.type.IsDialog())
        return;
      string stageName = this.stageName;
      this.stageName = (string) null;
      this.stageImageID = -1;
      this.needHomeManager = false;
      this.needLoungeManager = false;
      this.needClanManager = false;
      this.needGuildManager = false;
      this.needStatusStageManager = false;
      switch (section_name)
      {
        case "InGameMain":
          break;
        case "MenuReset":
          break;
        default:
          if (scene_name == "HomeScene")
          {
            HomeThemeTable.HomeThemeData homeThemeData = Singleton<HomeThemeTable>.I.GetHomeThemeData(TimeManager.GetNow());
            this.stageName = homeThemeData.sceneName;
            if (MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory("HomeTop") && prev_scene_name == "HomeScene")
              this.stageName = stageName;
            if (string.IsNullOrEmpty(this.stageName))
              this.stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.mainStage;
            if (this.stageName == homeThemeData.sceneName)
              Singleton<HomeThemeTable>.I.SetCurrentHomeThemeName(homeThemeData.name);
            this.needHomeManager = true;
          }
          if (this.stageName == null && scene_name == "LoungeScene")
          {
            this.stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene.mainStage;
            this.needLoungeManager = true;
          }
          if (this.stageName == null && scene_name == "ClanScene")
          {
            this.stageName = !(prev_scene_name == "ClanScene") ? ClanLvUnlockManager.CallGetLoadStageName(MonoBehaviourSingleton<UserInfoManager>.I.userClan.level) : stageName;
            this.needClanManager = true;
          }
          if (this.stageName == null && (scene_name == "StatusScene" || scene_name == "ProfileScene"))
          {
            this.stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createStage;
            if (scene_name == "StatusScene")
              this.needStatusStageManager = true;
          }
          if (this.stageName == null && scene_name == "UniqueStatusScene")
          {
            this.stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createUniqueStage;
            this.needStatusStageManager = true;
          }
          if (this.stageName == null && scene_name == "SmithScene")
          {
            switch (section_name)
            {
              case "SmithGrowSkillSelect":
              case "SmithGrowSkillPerformance":
              case "SmithGrowSkillSecond":
                this.stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.glowSkillStage;
                break;
              case "SmithCreateTypeSelect":
              case "SmithGrowItemSelect":
                this.stageName = !StatusManager.IsUnique() ? MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createStage : MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createUniqueStage;
                break;
              default:
                if (!section_name.EndsWith("Performance"))
                {
                  this.stageName = prev_scene_name == "HomeScene" || prev_scene_name == "LoungeScene" || prev_scene_name == "ClanScene" ? MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createStage : stageName;
                  break;
                }
                goto case "SmithCreateTypeSelect";
            }
            if (this.stageName == MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createStage || this.stageName == MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createUniqueStage)
              this.needStatusStageManager = true;
          }
          if (this.stageName == null && scene_name == "GatherScene")
            this.stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.gatherScene.mainStage;
          if (this.stageName == null && scene_name == "ForestScene")
            this.stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.gatherScene.mainStage;
          if (this.stageName == null && (scene_name == "ShopScene" || scene_name == "GachaScene"))
          {
            switch (section_name)
            {
              case "ShopTop":
                this.stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.shopScene.mainStage;
                break;
              case "GachaPerformanceSkill":
                this.stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.gachaScene.SkillGachaStage;
                break;
              case "GachaPerformanceQuest":
                if (MonoBehaviourSingleton<GachaManager>.IsValid() && MonoBehaviourSingleton<GachaManager>.I.IsReam())
                {
                  if (MonoBehaviourSingleton<GachaManager>.I.enableFeverDirector)
                  {
                    this.stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.gachaScene.QuestFeverGachaStage;
                    this.stageForceLoad = true;
                    break;
                  }
                  this.stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.gachaScene.QuestReamGachaStage;
                  break;
                }
                this.stageName = MonoBehaviourSingleton<OutGameSettingsManager>.I.gachaScene.QuestSingleGachaStage;
                break;
              default:
                this.stageName = stageName;
                break;
            }
          }
          if (this.stageName == null && scene_name == "TitleScene")
            this.stageName = !(section_name == "CharaMake") ? string.Empty : (GameSceneGlobalSettings.ExistHistorySection("StatusTop") || GameSceneGlobalSettings.ExistHistorySection("ProfileTop") ? MonoBehaviourSingleton<OutGameSettingsManager>.I.charaEditScene.stage : MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.stage);
          if (scene_name == "EnemyDownloadScene")
            this.stageName = string.Empty;
          if (this.stageName == null && this.stageImageID == -1)
            this.stageImageID = scene_name == "StatusScene" || scene_name == "ItemStorageScene" ? 10000000 : (!scene_name.Contains("WeaponSelect") ? 10000001 : 99999999);
          if ((!(prev_scene_name == "ShopScene") || !(scene_name == "GachaScene")) && (!(prev_scene_name == "GachaScene") || !(scene_name == "ShopScene")))
            break;
          this.stageForceLoad = true;
          break;
      }
    }
  }

  public void SectionInitialize(
    string scene_name,
    string section_name,
    GameSceneTables.SectionData section_data)
  {
    this.InitGlobal(scene_name, section_name, section_data);
    this.InitGlobalUI(scene_name, section_name);
    this.InitGlobalStage(scene_name, section_name, section_data);
    this.InitOrientation(scene_name, section_name);
    this.UpdateBGM(scene_name, section_name);
    if (!MonoBehaviourSingleton<SmithManager>.IsValid())
      return;
    MonoBehaviourSingleton<SmithManager>.I.CheckSmithSectionBlur(scene_name, section_name, section_data);
  }

  public void SectionSetup(
    string scene_name,
    string section_name,
    GameSceneTables.SectionData section_data)
  {
    this.InitCamera(scene_name, section_name);
    if (!Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.npcMessage, (Object) null))
      return;
    MonoBehaviourSingleton<UIManager>.I.npcMessage.UpdateMessage(section_data, true);
  }

  public void SectionStart(string scene_name, string section_name, bool is_new_section)
  {
    if (!MonoBehaviourSingleton<UIManager>.IsValid() || !Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.tutorialMessage, (Object) null))
      return;
    MonoBehaviourSingleton<UIManager>.I.tutorialMessage.Run(scene_name, section_name, is_new_section, false);
  }

  private void InitGlobal(
    string scene_name,
    string section_name,
    GameSceneTables.SectionData section_data)
  {
    if (scene_name != "InGameScene" && !GameSceneGlobalSettings.ExistSection("QuestAcceptRoom") && !GameSceneGlobalSettings.ExistSection("WorldMapOpenNewField") && MonoBehaviourSingleton<CoopApp>.IsValid())
      MonoBehaviourSingleton<CoopApp>.I.LeaveWithParty();
    Protocol.strict = scene_name != "InGameScene";
    if (scene_name != "InGameScene" && scene_name != "DebugScene")
    {
      if (!MonoBehaviourSingleton<OutGameEffectManager>.IsValid())
        ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<OutGameEffectManager>();
    }
    else if (MonoBehaviourSingleton<OutGameEffectManager>.IsValid())
      Object.DestroyImmediate((Object) MonoBehaviourSingleton<OutGameEffectManager>.I);
    if (!MonoBehaviourSingleton<StatusStageManager>.IsValid())
      return;
    MonoBehaviourSingleton<StatusStageManager>.I.UpdateCamera(scene_name, section_name, section_data);
  }

  private void InitGlobalUI(string scene_name, string section_name)
  {
    if (!(section_name == "QuestResultFriend"))
      return;
    MonoBehaviourSingleton<UIManager>.I.LoadUI(true, false, false);
  }

  public static IHomeManager GetCurrentIHomeManager()
  {
    if (MonoBehaviourSingleton<HomeManager>.IsValid())
      return (IHomeManager) MonoBehaviourSingleton<HomeManager>.I;
    if (MonoBehaviourSingleton<LoungeManager>.IsValid())
      return (IHomeManager) MonoBehaviourSingleton<LoungeManager>.I;
    return MonoBehaviourSingleton<ClanManager>.IsValid() ? (IHomeManager) MonoBehaviourSingleton<ClanManager>.I : (IHomeManager) null;
  }

  private void InitGlobalStage(
    string scene_name,
    string section_name,
    GameSceneTables.SectionData section_data)
  {
    if (this.needHomeManager)
    {
      if (!MonoBehaviourSingleton<HomeManager>.IsValid())
        Utility.CreateGameObjectAndComponent("HomeManager", MonoBehaviourSingleton<AppMain>.I._transform);
    }
    else if (MonoBehaviourSingleton<HomeManager>.IsValid())
      Object.DestroyImmediate((Object) ((Component) MonoBehaviourSingleton<HomeManager>.I).gameObject);
    if (this.needLoungeManager)
    {
      if (!MonoBehaviourSingleton<LoungeManager>.IsValid())
        Utility.CreateGameObjectAndComponent("LoungeManager", MonoBehaviourSingleton<AppMain>.I._transform);
    }
    else if (MonoBehaviourSingleton<LoungeManager>.IsValid())
      Object.DestroyImmediate((Object) ((Component) MonoBehaviourSingleton<LoungeManager>.I).gameObject);
    if (this.needClanManager)
    {
      if (!MonoBehaviourSingleton<ClanManager>.IsValid())
        Utility.CreateGameObjectAndComponent("ClanManager", MonoBehaviourSingleton<AppMain>.I._transform);
    }
    else if (MonoBehaviourSingleton<ClanManager>.IsValid())
      Object.DestroyImmediate((Object) ((Component) MonoBehaviourSingleton<ClanManager>.I).gameObject);
    if (this.needGuildManager)
    {
      if (!MonoBehaviourSingleton<GuildStageManager>.IsValid())
        Utility.CreateGameObjectAndComponent("GuildStageManager", MonoBehaviourSingleton<AppMain>.I._transform);
    }
    else if (MonoBehaviourSingleton<GuildStageManager>.IsValid())
      Object.DestroyImmediate((Object) ((Component) MonoBehaviourSingleton<GuildStageManager>.I).gameObject);
    if (this.needStatusStageManager)
    {
      if (!MonoBehaviourSingleton<StatusStageManager>.IsValid())
        Utility.CreateGameObjectAndComponent("StatusStageManager", MonoBehaviourSingleton<AppMain>.I._transform);
      MonoBehaviourSingleton<StatusStageManager>.I.UpdateCamera(scene_name, section_name, section_data);
    }
    else if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
      Object.DestroyImmediate((Object) ((Component) MonoBehaviourSingleton<StatusStageManager>.I).gameObject);
    if (!MonoBehaviourSingleton<StageManager>.IsValid())
      return;
    if (!string.IsNullOrEmpty(this.stageName))
    {
      if (this.stageForceLoad)
      {
        MonoBehaviourSingleton<StageManager>.I.UnloadStage();
        this.stageForceLoad = false;
      }
      if (!MonoBehaviourSingleton<StageManager>.I.LoadStage(this.stageName) || !Object.op_Inequality((Object) this.cameraAnim, (Object) null))
        return;
      ((Behaviour) this.cameraAnim).enabled = false;
    }
    else if (this.stageImageID > 0)
      MonoBehaviourSingleton<StageManager>.I.LoadBackgoundImage(this.stageImageID);
    else
      MonoBehaviourSingleton<StageManager>.I.UnloadStage();
  }

  private void InitOrientation(string scene_name, string section_name)
  {
    GameSceneGlobalSettings.SetOrientation(this.isAvailableScreenRotation(scene_name, section_name));
  }

  public bool isAvailableScreenRotation(string scene_name, string section_name)
  {
    return scene_name == "InGameScene" && (section_name.StartsWith("InGame") || section_name == "WorldMap" || section_name == "RegionMap" || section_name == "RegionMapDescriptionList" || section_name == "RegionMapDescriptionDetailDelivery" || section_name == "WorldMapOpenNewField" || section_name == "WorldMapOpenNewRegion" || section_name.StartsWith("ExploreMap") || section_name == "InformationDialog") && section_name != "InGameStoryMain";
  }

  public static void SetOrientation(bool ingame)
  {
    bool flag = ingame;
    if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userInfo != null && MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name == "/colopl_rob")
      flag = false;
    if (!TutorialStep.HasFirstDeliveryCompleted())
      flag = false;
    if (GameSaveData.instance != null && !GameSaveData.instance.enableLandscape)
      flag = false;
    if (!Native.GetDeviceAutoRotateSetting())
      flag = false;
    if (flag)
    {
      Screen.autorotateToLandscapeLeft = true;
      Screen.autorotateToLandscapeRight = true;
      Screen.autorotateToPortrait = true;
      Screen.autorotateToPortraitUpsideDown = true;
      Screen.orientation = (ScreenOrientation) 5;
    }
    else
    {
      Screen.autorotateToLandscapeLeft = false;
      Screen.autorotateToLandscapeRight = false;
      Screen.autorotateToPortrait = true;
      Screen.autorotateToPortraitUpsideDown = true;
      if (Screen.orientation == 3 || Screen.orientation == 3 || Screen.orientation == 4)
        MonoBehaviourSingleton<AppMain>.I.UpdateResolution(true, (System.Action) (() => Screen.orientation = (ScreenOrientation) 5));
      else
        Screen.orientation = (ScreenOrientation) 5;
    }
  }

  private void InitCamera(string scene_name, string section_name)
  {
    Camera mainCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
    if (Object.op_Equality((Object) mainCamera, (Object) null) || !((Behaviour) mainCamera).enabled || !MonoBehaviourSingleton<StageManager>.IsValid())
      return;
    mainCamera.cullingMask = this.mainCameraCullingMask;
    if ((double) this.saveCameraNear == -3.4028234663852886E+38)
    {
      this.saveCameraNear = mainCamera.nearClipPlane;
      this.saveCameraFar = mainCamera.farClipPlane;
    }
    float num = this.saveCameraNear;
    float saveCameraFar = this.saveCameraFar;
    Color black = Color.black;
    CameraClearFlags cameraClearFlags = (CameraClearFlags) 2;
    if (Object.op_Equality((Object) MonoBehaviourSingleton<StageManager>.I.stageObject, (Object) null))
      num = 0.01f;
    mainCamera.nearClipPlane = num;
    mainCamera.farClipPlane = saveCameraFar;
    mainCamera.clearFlags = cameraClearFlags;
    mainCamera.backgroundColor = black;
    if (!MonoBehaviourSingleton<StatusStageManager>.IsValid())
    {
      if (GameSceneGlobalSettings.ExistSection("CharaMake"))
      {
        Vector3 pos;
        Vector3 rot;
        CharaMake.GetCameraPosRot(out pos, out rot, GameSceneGlobalSettings.ExistHistorySection("StatusTop") || GameSceneGlobalSettings.ExistHistorySection("ProfileTop"));
        ((Component) mainCamera).transform.Set(pos, rot);
      }
      else if ((scene_name == "StoryScene" || section_name == "InGameStoryMain") && MonoBehaviourSingleton<OutGameSettingsManager>.IsValid())
        ((Component) mainCamera).transform.Set(new Vector3(0.0f, MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.cameraHeight, 0.0f), Vector3.zero);
      else if (scene_name == "ProfileScene")
        ((Component) mainCamera).transform.Set(MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.friendPos, MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.friendRot);
      else if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid() && MonoBehaviourSingleton<StageManager>.I.currentStageName == MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createStage)
        ((Component) mainCamera).transform.Set(MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createCameraPos, MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createCameraRot);
      else if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid() && MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene != null && MonoBehaviourSingleton<StageManager>.I.currentStageName == MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.glowSkillStage && section_name != "SmithGrowSkillResult")
        ((Component) mainCamera).transform.Set(MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.glowSkillCameraPos, MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.glowSkillCameraRot);
      else if (scene_name == "StatusScene" || scene_name == "UniqueStatusScene")
      {
        ((Component) mainCamera).transform.Set(MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.myhousePos, MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.myhouseRot);
      }
      else
      {
        switch (section_name)
        {
          case "GachaPerformanceSkill":
          case "GachaPerformanceQuest":
            if (Object.op_Inequality((Object) AnimationDirector.I, (Object) null))
            {
              AnimationDirector.I.SetLinkCamera(true);
              break;
            }
            break;
          case "ShopTop":
            if (Object.op_Inequality((Object) AnimationDirector.I, (Object) null))
              AnimationDirector.I.SetLinkCamera(false);
            ((Component) mainCamera).transform.Set(MonoBehaviourSingleton<OutGameSettingsManager>.I.shopScene.cameraPos, MonoBehaviourSingleton<OutGameSettingsManager>.I.shopScene.cameraRot);
            break;
          case "HomeTop":
            float selfCameraHeight1;
            float cameraTagetHeight1;
            if (MonoBehaviourSingleton<HomeManager>.IsValid())
            {
              selfCameraHeight1 = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.GetSelfCameraHeight();
              cameraTagetHeight1 = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.selfCameraTagetHeight;
            }
            else
            {
              selfCameraHeight1 = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.GetSelfCameraHeight();
              cameraTagetHeight1 = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.selfCameraTagetHeight;
            }
            Vector3 defaultCameraPos1 = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.defaultCameraPos;
            defaultCameraPos1.y += selfCameraHeight1;
            Vector3 defaultTargetPos1 = MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.defaultTargetPos;
            defaultTargetPos1.y += cameraTagetHeight1;
            ((Component) mainCamera).transform.position = defaultCameraPos1;
            ((Component) mainCamera).transform.LookAt(defaultTargetPos1);
            mainCamera.fieldOfView = MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.outGameFieldOfView;
            break;
          case "LoungeTop":
            float selfCameraHeight2;
            float cameraTagetHeight2;
            if (MonoBehaviourSingleton<HomeManager>.IsValid())
            {
              selfCameraHeight2 = MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene.GetSelfCameraHeight();
              cameraTagetHeight2 = MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene.selfCameraTagetHeight;
            }
            else
            {
              selfCameraHeight2 = MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene.GetSelfCameraHeight();
              cameraTagetHeight2 = MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene.selfCameraTagetHeight;
            }
            Vector3 defaultCameraPos2 = MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene.defaultCameraPos;
            defaultCameraPos2.y += selfCameraHeight2;
            Vector3 defaultTargetPos2 = MonoBehaviourSingleton<OutGameSettingsManager>.I.loungeScene.defaultTargetPos;
            defaultTargetPos2.y += cameraTagetHeight2;
            ((Component) mainCamera).transform.position = defaultCameraPos2;
            ((Component) mainCamera).transform.LookAt(defaultTargetPos2);
            mainCamera.fieldOfView = MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.outGameFieldOfView;
            break;
        }
      }
    }
    this.UpdateCameraFieldOfView(scene_name, section_name, mainCamera, MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
    string name = (string) null;
    if (scene_name == "ShopScene")
      name = "CameraAnim_01";
    if (name != null)
    {
      AnimationClip linkResource = SceneSettingsManager.GetLinkResource<AnimationClip>(name);
      if (Object.op_Inequality((Object) linkResource, (Object) null))
      {
        if (Object.op_Inequality((Object) this.cameraAnimClip, (Object) linkResource))
        {
          if (Object.op_Equality((Object) this.cameraAnim, (Object) null))
            this.cameraAnim = ((Component) MonoBehaviourSingleton<AppMain>.I.mainCameraTransform).gameObject.AddComponent<Animation>();
          if (Object.op_Inequality((Object) this.cameraAnimClip, (Object) null))
            this.cameraAnim.RemoveClip(this.cameraAnimClip);
          this.cameraAnimClip = linkResource;
          this.cameraAnim.AddClip(this.cameraAnimClip, name);
          this.cameraAnim.Stop();
          this.cameraAnim.Play(name);
        }
        if (Object.op_Inequality((Object) this.cameraAnim, (Object) null))
          ((Behaviour) this.cameraAnim).enabled = true;
      }
      else
        name = (string) null;
    }
    if (name != null)
      return;
    this.cameraAnimClip = (AnimationClip) null;
    if (!Object.op_Inequality((Object) this.cameraAnim, (Object) null))
      return;
    Object.Destroy((Object) this.cameraAnim);
    this.cameraAnim = (Animation) null;
  }

  public void SetMainCameraCullingMask(int mask)
  {
    this.mainCameraCullingMask = mask;
    Camera mainCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
    if (Object.op_Equality((Object) mainCamera, (Object) null))
      return;
    mainCamera.cullingMask = mask;
  }

  public static int GetDefaultMainCameraCullingMask() => 1620966657;

  private void UpdateCameraFieldOfView(
    string scene_name,
    string section_name,
    Camera camera,
    bool is_portrait)
  {
    if (Object.op_Equality((Object) camera, (Object) null) || !((Behaviour) camera).enabled || !MonoBehaviourSingleton<OutGameSettingsManager>.IsValid())
      return;
    float num = -1f;
    string str = (string) null;
    if (MonoBehaviourSingleton<StageManager>.IsValid())
      str = MonoBehaviourSingleton<StageManager>.I.currentStageName;
    if (MonoBehaviourSingleton<StatusStageManager>.IsValid())
      return;
    if (GameSceneGlobalSettings.ExistSection("CharaMake"))
      num = MonoBehaviourSingleton<OutGameSettingsManager>.I.charaMakeScene.cameraFieldOfView;
    else if (scene_name == "StoryScene" || section_name == "InGameStoryMain")
      num = MonoBehaviourSingleton<OutGameSettingsManager>.I.storyScene.cameraFieldOfView;
    else if (scene_name == "ProfileScene")
      num = MonoBehaviourSingleton<OutGameSettingsManager>.I.profileScene.cameraFieldOfView;
    else if (str == MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createStage)
      num = MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.createCameraFieldOfView;
    else if (str == MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.glowSkillStage)
      num = MonoBehaviourSingleton<OutGameSettingsManager>.I.smithScene.glowSkillCameraFieldOfView;
    else if (str == MonoBehaviourSingleton<OutGameSettingsManager>.I.gatherScene.mainStage)
    {
      num = MonoBehaviourSingleton<OutGameSettingsManager>.I.gatherScene.cameraFieldOfView;
    }
    else
    {
      switch (section_name)
      {
        case "HomeLoginBonusTheater":
          return;
        case "HomeLoginBonus":
          return;
        case "HomeLoginBonusNoticeTwo":
          return;
        case "HomeLoginBonusNoticeOne":
          return;
        default:
          switch (scene_name)
          {
            case "InGameScene":
              if (!section_name.Contains("InGame"))
                return;
              GlobalSettingsManager.CameraParam cameraParam = MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam;
              num = !is_portrait ? cameraParam.inGameLandscapeFieldOfView : cameraParam.inGamePortraitFieldOfView;
              break;
            case "UniqueStatusScene":
            case "StatusScene":
              num = MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.myhouseFieldOfView;
              break;
          }
          break;
      }
    }
    if ((double) num == -1.0)
      num = MonoBehaviourSingleton<GlobalSettingsManager>.I.cameraParam.outGameFieldOfView;
    camera.fieldOfView = num;
  }

  public void OnScreenRotate(bool is_portrait)
  {
    this.UpdateCameraFieldOfView(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName(), MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName(), MonoBehaviourSingleton<AppMain>.I.mainCamera, is_portrait);
  }

  public static bool IsGlobalEvent(string event_name)
  {
    switch (event_name)
    {
      case "APP_VERSION_RESTRICTION":
      case "CHAT_AGE_CONFIRM":
      case "EXPAND_STORAGE":
      case "MAIN_MENU_CLAN":
      case "MAIN_MENU_GACHA":
      case "MAIN_MENU_GATHER":
      case "MAIN_MENU_HOME":
      case "MAIN_MENU_LOUNGE":
      case "MAIN_MENU_MENU":
      case "MAIN_MENU_MY_HOUSE":
      case "MAIN_MENU_QUEST":
      case "MAIN_MENU_SHOP":
      case "OPINIONBOX":
      case "QUEST_ROOM_IN_GAME":
      case "TUTORIAL_NEXT":
      case "TUTORIL_TO_FIELD":
        return true;
      default:
        return false;
    }
  }

  public static bool IsNonPopupError(BaseModel model)
  {
    if (model is CheckRegisterModel || model is RegistCreateModel || model is OptionBirthdayModel || model is LinkRobModel && model.Error == Error.WRN_LINK_ROB_LINKED_WITH_ROB || model is RegistLinkFacebookModel && model.Error == Error.WRN_REGISTER_FACEBOOK_ACCOUNT_LINKED || model is GoPayDepositModel && model.Error == Error.WRN_PAYMENT_GOPAY_PENDING || model is ScreenshotSharingModel && model.Error == Error.WRN_USER_HAD_SCREEN_SHOT || model is OptionSetParentPassModel || model is OptionResetParentPassModel || model is GoldCanPurchaseModel && model.Error == Error.WRN_GOLD_OVER_LIMITTER_OVERUSE || model is FriendFollowModel || model is FriendSearchByCodeModel && model.Error == Error.WRN_FRIEND_CODE_NOT_FOUND || (model is ShopBuyModel || model is GachaGachaModel) && model.Error == Error.ERR_CRYSTAL_NOT_ENOUGH)
      return true;
    QuestCompleteModel questCompleteModel = model as QuestCompleteModel;
    return model is QuestContinueModel || model is PartySearchModel && model.Error == Error.WRN_PARTY_SEARCH_NOT_FOUND_QUEST || model is PartyModel && (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "QuestAcceptEntryPassRoom" && model.Error == Error.WRN_PARTY_SEARCH_NOT_FOUND_PARTY || model.Error == Error.WRN_PARTY_OWNER_REJOIN || model.Error == Error.WRN_QUEST_IS_ORDER || model.Error == Error.WRN_PARTY_TOO_MANY_PARTIES || model.Error == Error.WRN_PARTY_ALREADY_FINISH || model.Error == Error.WRN_PARTY_EXPIRED_OVER) || model is PartyLeaveModel && model.Error == Error.ERR_PARTY_NOT_FOUND_PARTY || model is FieldModel && model.Error == Error.WRN_QUEST_IS_ORDER || model is PresentReceiveModel && (model.Error == Error.WRN_PRESENT_OVER_MONEY || model.Error == Error.WRN_PRESENT_OVER_ITEM || model.Error == Error.WRN_PRESENT_OVER_EQUIP_ITEM || model.Error == Error.WRN_PRESENT_OVER_SKILL_ITEM || model.Error == Error.WRN_PRESENT_OVER_QUEST_ITEM || model.Error == Error.WRN_PRESENT_OVER_EQUIP_AND_SKILL || model.Error == Error.WRN_PRESENT_OVER_ETC) || model is SmithCreateModel && model.Error == Error.WRN_SMITH_OVER_EQUIP_ITEM_NUM || model is ClanRoomQuestModel && model.Error == Error.WRN_CLAN_NOT_JOINED || model is ClanAcceptInviteModel && (model.Error == Error.WRN_CLAN_NOT_EXISTS_INVITE || model.Error == Error.WRN_CLAN_NOT_EXISTS_CLAN) || model is GuildStatisticModel && model.Error == Error.WRN_GUILD_DELETE_GET_DATA || model is DarkMarketBuyModel && (model.Error == Error.ERR_BM_NOT_ENOUGH_GOLD || model.Error == Error.ERR_BM_NOT_ENOUGH_GEM || model.Error == Error.ERR_BM_ITEM_UNAVAILABLE || model.Error == Error.ERR_BLACK_MARKET_BUY || model.Error == Error.ERR_BM_ITEM_SOLD_OUT);
  }

  private static bool ExistSection(string section_name)
  {
    return Object.op_Inequality((Object) MonoBehaviourSingleton<GameSceneManager>.I.FindSection(section_name), (Object) null);
  }

  private static bool ExistHistorySection(string section_name)
  {
    return MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory(section_name);
  }

  public static bool forceIgnoreMainUI { get; set; }

  public static bool IsDisplayMainUI(string scene_name, string section_name, bool check_tutorial = true)
  {
    if (check_tutorial && !TutorialStep.HasFirstDeliveryCompleted())
      return false;
    switch (scene_name)
    {
      case "DebugScene":
      case "EnemyDownloadPage":
      case "GachaScene":
      case "InGameScene":
      case "QuestResultScene":
      case "StoryScene":
      case "TitleScene":
      case "UniqueStatusScene":
        return false;
      default:
        if (scene_name.Contains("TutorialWeaponSelect"))
          return false;
        if (scene_name == "HomeScene" || scene_name == "LoungeScene" || scene_name == "ClanScene")
        {
          if (section_name == "MenuReset" || GameSceneGlobalSettings.ExistSection("QuestAcceptRoom") || GameSceneGlobalSettings.ExistSection("HomeLoginBonusTheater") || section_name.Contains("GuildDonateMaterialSelectDialog") || section_name.Contains("GuildSmithGrowItemSelect") || section_name.Contains("GuildSmithGrow"))
            return false;
          string sectionNameFromHistory = MonoBehaviourSingleton<GameSceneManager>.I.GetPrevSectionNameFromHistory();
          if (sectionNameFromHistory != null && (sectionNameFromHistory.Contains("GuildDonateMaterialSelectDialog") || sectionNameFromHistory.Contains("GuildSmithGrow") || sectionNameFromHistory.Contains("GuildSmithGrowItemSelect")))
            return false;
        }
        return (!(scene_name == "SmithScene") || !section_name.Contains("Performance")) && (!(scene_name == "StatusScene") || !GameSceneGlobalSettings.ExistSection("StatusScreenShot")) && (!(scene_name == "TitleScene") || !(section_name == "MenuReset")) && !GameSceneGlobalSettings.forceIgnoreMainUI;
    }
  }

  public static bool IsDisplayMainStatusUI(string scene_name, string section_name)
  {
    if (scene_name == "StatusScene")
    {
      string str = section_name;
      GameSection sectionExcludeDialog = MonoBehaviourSingleton<GameSceneManager>.I.GetLastSectionExcludeDialog();
      if (Object.op_Inequality((Object) sectionExcludeDialog, (Object) null) && sectionExcludeDialog.sectionData != (GameSceneTables.SectionData) null)
        str = sectionExcludeDialog.sectionData.sectionName;
      if (str == "StatusAvatarEquipSelect" || str == "StatusEquip" || str == "StatusEquipSort" || str == "StatusEquipList" || str == "StatusEnemyList" || str == "StatusEquipListAchievement")
        return false;
    }
    if (scene_name == "HomeScene" || scene_name == "LoungeScene")
    {
      if (section_name.Contains("GuildDonateMaterialSelectDialog") || section_name.Contains("GuildSmithGrowItemSelect") || section_name.Contains("GuildSmithGrow"))
        return true;
      string sectionNameFromHistory = MonoBehaviourSingleton<GameSceneManager>.I.GetPrevSectionNameFromHistory();
      if (sectionNameFromHistory != null && (sectionNameFromHistory.Contains("GuildDonateMaterialSelectDialog") || sectionNameFromHistory.Contains("GuildSmithGrowItemSelect") || sectionNameFromHistory.Contains("GuildSmithGrow")))
        return true;
    }
    return GameSceneGlobalSettings.IsDisplayMainUI(scene_name, section_name, false);
  }

  public static bool IsActiveMainUI()
  {
    string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
    GameSection currentSection = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection();
    if (Object.op_Inequality((Object) currentSection, (Object) null) && currentSection.sectionData != (GameSceneTables.SectionData) null && currentSection.sectionData.type.IsDialog())
      return false;
    switch (currentSectionName)
    {
      case "PresentTop":
      case "QuestRoomInvalid":
      case "QuestRoomInvalid_EquipChange":
      case "QuestRoomInvalid_UserDetail":
      case "QuestRoomInvalid_UserDetailItem":
      case "QuestStartChangeEquipSet":
      case "QuestStorageOverflowEquip":
      case "QuestStorageOverflowSkill":
        return false;
      default:
        return true;
    }
  }

  private void ResetAudioRestener()
  {
    if (!MonoBehaviourSingleton<AudioListenerManager>.IsValid())
      return;
    MonoBehaviourSingleton<AudioListenerManager>.I.ReSetTargetObject();
  }

  private void UpdateBGM(string scene_name, string section_name)
  {
    SoundManager i = MonoBehaviourSingleton<SoundManager>.I;
    i.fadeOutTime = 1f;
    int playingBgmid = MonoBehaviourSingleton<SoundManager>.I.playingBGMID;
    int num = -1;
    string snapshotName = "Default";
    switch (scene_name)
    {
      case "ClanScene":
        num = 153;
        snapshotName = "Lounge";
        MonoBehaviourSingleton<SoundManager>.I.TransitionPreset(3U);
        break;
      case "GachaScene":
        switch (section_name)
        {
          case "GachaPerformanceQuest":
            num = 8;
            break;
          case "GachaPerformanceSkill":
            num = 9;
            break;
          default:
            num = 7;
            break;
        }
        snapshotName = "Gacha";
        break;
      case "GatherScene":
        num = 108;
        break;
      case "HomeScene":
        num = Singleton<HomeThemeTable>.I.GetHomeThemeData(TimeManager.GetNow()).bgmId;
        if (MonoBehaviourSingleton<GameSceneManager>.I.ExistHistory("HomeTop") && MonoBehaviourSingleton<GameSceneManager>.I.prev_scene_name == "HomeScene")
          num = playingBgmid;
        if (num <= 0)
          num = 2;
        snapshotName = "Home";
        break;
      case "InGameScene":
        this.UpdateIngameBGM(section_name);
        this.UpdateIngamePreset(section_name);
        snapshotName = "InGame";
        break;
      case "LoungeScene":
        num = 153;
        snapshotName = "Lounge";
        MonoBehaviourSingleton<SoundManager>.I.TransitionPreset(3U);
        break;
      case "QuestResultScene":
        num = 0;
        snapshotName = "QuestResult";
        break;
      case "ShopScene":
        num = 7;
        snapshotName = "Gacha";
        break;
      case "StatusScene":
        num = 6;
        snapshotName = "Home";
        break;
      case "StoryScene":
        snapshotName = "Story";
        break;
      case "TitleScene":
        if (section_name == "Opening")
          num = 13;
        if (section_name == "Opening")
        {
          snapshotName = "Opening";
          break;
        }
        break;
      case "UniqueStatusScene":
        num = 6;
        snapshotName = "Home";
        break;
    }
    if (num != -1)
      i.requestBGMID = num;
    if (!(snapshotName != "InGame"))
      return;
    i.TransitionTo(snapshotName);
  }

  public static void RequestSoundSettingIngameField()
  {
    int bgmId = -1;
    if (MonoBehaviourSingleton<FieldManager>.IsValid())
      bgmId = MonoBehaviourSingleton<FieldManager>.I.GetCurrentMapBGMID();
    if (bgmId <= 0)
      return;
    SoundManager.RequestBGM(bgmId);
  }

  private void UpdateIngamePreset(string section_name)
  {
    switch (section_name)
    {
      case "QuestResultDirection":
      case "QuestResultFriend":
        MonoBehaviourSingleton<SoundManager>.I.TransitionTo("QuestResult");
        break;
      case "InGameMain":
        MonoBehaviourSingleton<SoundManager>.I.TransitionPreset(QuestManager.IsValidInGame() ? 1U : 2U);
        break;
    }
  }

  private void UpdateIngameBGM(string section_name)
  {
    int bgmId = -1;
    switch (section_name)
    {
      case "QuestResultDirection":
      case "QuestResultFriend":
        if (MonoBehaviourSingleton<InGameRecorder>.IsValid() && MonoBehaviourSingleton<InGameRecorder>.I.isVictory)
        {
          bgmId = 4;
          break;
        }
        bgmId = -1;
        SoundManager.RequestBGM(10, false);
        break;
      case "InGameFieldQuestConfirm":
        bgmId = 12;
        break;
      case "InGameMain":
        if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.isEnding)
        {
          bgmId = -1;
          break;
        }
        if (QuestManager.IsValidInGameExplore())
        {
          if (MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap())
          {
            int currentQuestBgmid = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestBGMID();
            if (currentQuestBgmid > 0)
              bgmId = currentQuestBgmid;
          }
          if (bgmId < 0 && MonoBehaviourSingleton<FieldManager>.IsValid())
          {
            int currentMapBgmid = MonoBehaviourSingleton<FieldManager>.I.GetCurrentMapBGMID();
            if (currentMapBgmid > 0)
            {
              bgmId = currentMapBgmid;
              break;
            }
            break;
          }
          break;
        }
        if (QuestManager.IsValidInGame())
        {
          if (MonoBehaviourSingleton<QuestManager>.IsValid())
            bgmId = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestBGMID();
          if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.isQuestHappen && MonoBehaviourSingleton<FieldManager>.IsValid())
          {
            int happenMapBgmid = MonoBehaviourSingleton<FieldManager>.I.GetHappenMapBGMID();
            if (happenMapBgmid > 0)
            {
              bgmId = happenMapBgmid;
              break;
            }
            break;
          }
          break;
        }
        break;
    }
    if (bgmId <= 0)
      return;
    SoundManager.RequestBGM(bgmId);
  }
}
