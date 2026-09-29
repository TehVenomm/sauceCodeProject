// Decompiled with JetBrains decompiler
// Type: InGameMain
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class InGameMain : GameSection
{
  private Transform inGameMembers;
  private Transform inGameUIMembers;
  private int prevLevel = -1;
  private bool is_collect_tutorial;
  private bool execTutorial4_1;

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "EnemyAngryTable";
      yield return "EnemyActionTable";
      yield return "NpcLevelTable";
      yield return "NpcLevelSpecialTable";
    }
  }

  public CutScenePlayer cutScenePlayer { get; private set; }

  public override bool useOnPressBackKey => true;

  public override void OnPressBackKey()
  {
    if (QuestManager.IsValidInGame())
    {
      if (MonoBehaviourSingleton<UIInGameMenu>.IsValid() && ((Component) MonoBehaviourSingleton<UIInGameMenu>.I).gameObject.activeInHierarchy)
        MonoBehaviourSingleton<UIInGameMenu>.I.Close();
      else
        this.DispatchEvent("RETIRE");
    }
    else
    {
      if (!UIInGameFieldMenu.IsValid())
        return;
      if (UIInGameFieldMenu.I.IsPopMenu())
        UIInGameFieldMenu.I.OnClickPopMenu();
      else
        this.DispatchEvent("RETURN");
    }
  }

  public override void Initialize()
  {
    if (MonoBehaviourSingleton<ShopManager>.IsValid())
      MonoBehaviourSingleton<ShopManager>.I.trackPlayerDie = false;
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<InGameManager>.I.selfCacheObject, (Object) null))
      MonoBehaviourSingleton<InGameManager>.I.DestroySelfCache();
    this.StartCoroutine(this.DoInitialize_GG_Optimization());
  }

  private IEnumerator DoInitialize()
  {
    UILabel.OutlineLimit = MonoBehaviourSingleton<InGameManager>.I.graphicOptionType <= 0;
    PredownloadManager.Stop(PredownloadManager.STOP_FLAG.INGAME_MAIN, true);
    while (MonoBehaviourSingleton<DataTableManager>.I.IsLoading())
      yield return (object) null;
    if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.isRetry)
    {
      bool isWaitQuestLoad = true;
      if (MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
        CoopApp.EnterArenaQuestOffline((Action<bool, bool, bool, bool>) ((isMatching, isConnect, isRegist, isStart) => isWaitQuestLoad = !isStart));
      else if (MonoBehaviourSingleton<StatusManager>.IsValid() && MonoBehaviourSingleton<StatusManager>.I.assignedCharaInfo != null && MonoBehaviourSingleton<StatusManager>.I.assignedEquipmentData != null)
      {
        CoopApp.EnterQuestOfflineAssignedEquipment((AssignedEquipmentTable.AssignedEquipmentData) null, (CharaInfo) null, (Action<bool, bool, bool, bool>) ((isMatching, isConnect, isRegist, isStart) => isWaitQuestLoad = !isStart));
      }
      else
      {
        MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID((uint) GameSaveData.instance.lastQusetID);
        if (MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeriesArena())
          CoopApp.EnterSeriesArenaQuestOffline((Action<bool, bool, bool, bool>) ((isMatching, isConnect, isRegist, isStart) => isWaitQuestLoad = !isStart));
      }
      while (isWaitQuestLoad)
        yield return (object) null;
      if (MonoBehaviourSingleton<QuestManager>.IsValid())
        SoundManager.RequestBGM(MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestBGMID());
      MonoBehaviourSingleton<InGameManager>.I.isRetry = false;
    }
    this.SetActive((Enum) InGameMain.UI.BTN_QUEST_MENU, QuestManager.IsValidInGame());
    this.InitializeChatUI();
    if (MonoBehaviourSingleton<WorldMapManager>.I.GetOpenRegionIdListInWorldMap(REGION_DIFFICULTY_TYPE.NORMAL).Length >= 2)
      this.SetEvent((Enum) InGameMain.UI.BTN_REQUEST, "QUEST_SELECT_WINDOW", (int) MonoBehaviourSingleton<FieldManager>.I.currentMapData.regionId);
    if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      MonoBehaviourSingleton<CoopManager>.I.coopMyClient.LoadingStart();
    while (MonoBehaviourSingleton<LoadingProcess>.IsValid())
      yield return (object) null;
    if (QuestManager.IsValidInGame() && MonoBehaviourSingleton<AppMain>.IsValid() && !MonoBehaviourSingleton<InGameRecorder>.IsValid())
      ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<InGameRecorder>();
    if (MonoBehaviourSingleton<AppMain>.IsValid())
      ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<AttackColliderManager>();
    this.inGameMembers = new GameObject("InGameMembers").transform;
    Transform _transform = this.inGameMembers;
    ((Component) _transform).transform.parent = MonoBehaviourSingleton<AppMain>.I._transform;
    this.inGameUIMembers = Utility.CreateGameObject("InGameUIMembers", MonoBehaviourSingleton<UIManager>.I._transform, 5);
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    ResourceManager.enableCache = false;
    LoadObject lo_prefabs = load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemInGame", new string[8]
    {
      "InGameSettingsManager",
      "InGameCameraManager",
      "PuniConManager",
      "PlayerAttackInfo_00",
      "PlayerAttackInfo_01",
      "PlayerAttackInfo_02",
      "PlayerAttackInfo_04",
      "PlayerAttackInfo_05"
    });
    LoadObject lo_tutorial_prefab = (LoadObject) null;
    if (!TutorialStep.HasFirstDeliveryCompleted() && TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.USER_CREATE_02))
      lo_tutorial_prefab = load_queue.Load(RESOURCE_CATEGORY.UI, "UI_TutorialFieldHelper");
    LoadObject lo_link_resources = (LoadObject) null;
    if (FieldManager.IsValidInTutorial())
    {
      lo_link_resources = load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemInGameLinkResources", new string[3]
      {
        "InGameLinkResourcesCommon",
        "InGameLinkResourcesQuest",
        "InGameLinkResourcesField"
      });
    }
    else
    {
      List<string> stringList = new List<string>(3);
      stringList.Add("InGameLinkResourcesCommon");
      if (QuestManager.IsValidInGame() || InGameManager.IsValidInGameTest())
        stringList.Add("InGameLinkResourcesQuest");
      if (FieldManager.IsValidInGameNoBoss() || InGameManager.IsValidInGameTest())
        stringList.Add("InGameLinkResourcesField");
      lo_link_resources = load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemInGameLinkResources", stringList.ToArray());
    }
    ResourceManager.enableCache = true;
    VorgonPreEventController vorgonPreEventController = (VorgonPreEventController) null;
    if (MonoBehaviourSingleton<QuestManager>.IsValid())
    {
      QuestManager.VorgonQuetType vorgonQuestType = MonoBehaviourSingleton<QuestManager>.I.GetVorgonQuestType();
      if (vorgonQuestType != QuestManager.VorgonQuetType.NONE)
      {
        GameObject gameObject = new GameObject("VorgonQuestController");
        vorgonPreEventController = gameObject.AddComponent<VorgonPreEventController>();
        ((Behaviour) vorgonPreEventController).enabled = false;
        gameObject.transform.parent = _transform;
        if (vorgonQuestType == QuestManager.VorgonQuetType.BATTLE_WITH_WYBURN)
        {
          string cutSceneDataPath = "ev001_CutScene";
          bool wait = true;
          this.cutScenePlayer = gameObject.AddComponent<CutScenePlayer>();
          this.cutScenePlayer.Init(cutSceneDataPath, (Action<bool>) (success => wait = false));
          while (wait)
            yield return (object) null;
        }
      }
    }
    bool isExploreMiniMapActive = MonoBehaviourSingleton<ExploreMiniMap>.IsValid() && ((Component) MonoBehaviourSingleton<ExploreMiniMap>.I).gameObject.activeSelf;
    if (isExploreMiniMapActive)
      MonoBehaviourSingleton<ExploreMiniMap>.I.Preload(load_queue);
    LoadObject lo_ingame_rush = (LoadObject) null;
    if (MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameWaveMatch(true))
      lo_ingame_rush = load_queue.Load(RESOURCE_CATEGORY.UI, "InGameRush");
    LoadObject loAdditionalDamageNum = load_queue.Load(RESOURCE_CATEGORY.UI, "InGameAdditionalDamageNum");
    LoadObject loPlayerDamageNum = load_queue.Load(RESOURCE_CATEGORY.UI, "InGamePlayerDamageNum");
    LoadObject loDamageNum = load_queue.Load(RESOURCE_CATEGORY.UI, "InGameDamageNum");
    if (load_queue.IsLoading())
      yield return (object) load_queue.Wait();
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      MonoBehaviourSingleton<CoopManager>.I.coopMyClient.SetLoadingPer(20);
    Transform parent = (Transform) null;
    InGameSettingsManager.Player player1 = (InGameSettingsManager.Player) null;
    foreach (ResourceObject loadedObject in lo_prefabs.loadedObjects)
    {
      string name = loadedObject.obj.name;
      if (name.Contains("PlayerAttackInfo"))
      {
        if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
        {
          if (Object.op_Equality((Object) parent, (Object) null))
            parent = MonoBehaviourSingleton<InGameSettingsManager>.I._transform;
          if (player1 == null)
          {
            player1 = MonoBehaviourSingleton<InGameSettingsManager>.I.player;
            player1.attackInfosAll = new AttackInfo[0];
          }
          AttackInfos component = ((Component) ResourceUtility.Realizes(loadedObject.obj, parent)).gameObject.GetComponent<AttackInfos>();
          player1.weaponAttackInfoList.Add(component);
          player1.attackInfosAll = Utility.CreateMergedArray<AttackInfo>(player1.attackInfosAll, (AttackInfo[]) component.attackHitInfos);
        }
      }
      else if (name.Contains("PuniConManager"))
      {
        ResourceUtility.Realizes(loadedObject.obj, this.inGameUIMembers, 5);
        MonoBehaviourSingleton<PuniConManager>.I.enableMultiTouch = true;
      }
      else
        ResourceUtility.Realizes(loadedObject.obj, _transform);
    }
    foreach (ResourceObject loadedObject in lo_link_resources.loadedObjects)
      ResourceUtility.Realizes(loadedObject.obj, MonoBehaviourSingleton<InGameSettingsManager>.I._transform);
    Utility.CreateGameObjectAndComponent("StageObjectManager", _transform);
    Utility.CreateGameObjectAndComponent("TargetMarkerManager", _transform);
    Utility.CreateGameObjectAndComponent("InGameProgress", _transform);
    Utility.CreateGameObjectAndComponent("AIManager", _transform);
    if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.invitationInGameButton, (Object) null))
    {
      ((Component) MonoBehaviourSingleton<UIManager>.I.invitationInGameButton).gameObject.SetActive(true);
      if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite)
        MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Open();
    }
    if (lo_tutorial_prefab != null)
    {
      Transform transform = ResourceUtility.Realizes(lo_tutorial_prefab.loadedObject, this.inGameUIMembers);
      if (Object.op_Inequality((Object) transform, (Object) null))
      {
        UITutorialFieldHelper component = ((Component) transform).GetComponent<UITutorialFieldHelper>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.Setup(this);
      }
    }
    if (MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameWaveMatch(true))
    {
      GameObject gameObject = Object.Instantiate(lo_ingame_rush.loadedObject) as GameObject;
      gameObject.transform.parent = MonoBehaviourSingleton<UIContinueButton>.I._transform.parent;
      gameObject.transform.localPosition = Vector3.zero;
      gameObject.transform.localRotation = Quaternion.identity;
      gameObject.transform.localScale = Vector3.one;
      MonoBehaviourSingleton<UISpectatorButton>.I.Initialize(gameObject.GetComponentInParent<UIStaticPanelChanger>());
    }
    if (MonoBehaviourSingleton<UIDamageManager>.IsValid())
      MonoBehaviourSingleton<UIDamageManager>.I.RegisterDamageNumResources(loDamageNum.loadedObject, loPlayerDamageNum.loadedObject, loAdditionalDamageNum.loadedObject);
    string str = MonoBehaviourSingleton<InGameManager>.IsValid() ? MonoBehaviourSingleton<InGameManager>.I.GetCurrentStageName() : "ST011D_01";
    if (MonoBehaviourSingleton<GameSceneManager>.IsValid())
      MonoBehaviourSingleton<GameSceneManager>.I.SetExternalStageName(str);
    MonoBehaviourSingleton<StageManager>.I.LoadStage(str);
    while (MonoBehaviourSingleton<StageManager>.I.isLoading)
      yield return (object) null;
    int num = MonoBehaviourSingleton<StageManager>.I.isValidInside ? 1 : 0;
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      MonoBehaviourSingleton<CoopManager>.I.coopMyClient.SetLoadingPer(40);
    InGameSettingsManager i1 = MonoBehaviourSingleton<InGameSettingsManager>.I;
    GlobalSettingsManager.LinkResources linkResources = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources;
    List<string> loadEffectNames = new List<string>();
    for (int index = 0; index < 3; ++index)
    {
      InGameSettingsManager.UseResources useResources = (InGameSettingsManager.UseResources) null;
      switch (index)
      {
        case 0:
          useResources = linkResources.inGameCommonResources;
          break;
        case 1:
          if (FieldManager.IsValidInGameNoQuest() || FieldManager.IsValidInGameNoBoss())
          {
            useResources = i1.useResourcesField;
            break;
          }
          break;
        case 2:
          if (QuestManager.IsValidInGame() || InGameManager.IsValidInGameTest() || FieldManager.IsValidInTutorial())
          {
            useResources = linkResources.inGameQuestResources;
            break;
          }
          break;
      }
      if (useResources != null)
      {
        foreach (string effect in useResources.effects)
        {
          load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effect);
          loadEffectNames.Add(effect);
        }
        foreach (string uiEffect in useResources.uiEffects)
          load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, uiEffect);
      }
    }
    InGameSettingsManager.Player player2 = i1.player;
    foreach (string stunnedEffect in linkResources.stunnedEffectList)
    {
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, stunnedEffect);
      loadEffectNames.Add(stunnedEffect);
    }
    foreach (string charmEffect in linkResources.charmEffectList)
    {
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, charmEffect);
      loadEffectNames.Add(charmEffect);
    }
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.battleStartEffectName);
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.changeWeaponEffectName);
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.spActionStartEffectName);
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.arrowBleedOtherEffectName);
    loadEffectNames.Add(linkResources.battleStartEffectName);
    loadEffectNames.Add(linkResources.changeWeaponEffectName);
    loadEffectNames.Add(linkResources.spActionStartEffectName);
    loadEffectNames.Add(linkResources.arrowBleedOtherEffectName);
    foreach (int elementHitSeiD in i1.enemy.elementHitSEIDs)
      load_queue.CacheSE(elementHitSeiD);
    if (FieldManager.IsValidInGameNoBoss())
    {
      foreach (string effectName in i1.portal.effectNames)
      {
        load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectName);
        loadEffectNames.Add(effectName);
      }
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, i1.fieldDrop.tresureBoxOpenEffect);
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, i1.portal.pointGetEffectName);
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, i1.portal.pointEffect.normalEffectName);
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, i1.portal.pointEffect.largeEffectName);
      loadEffectNames.Add(i1.fieldDrop.tresureBoxOpenEffect);
      loadEffectNames.Add(i1.portal.pointGetEffectName);
      loadEffectNames.Add(i1.portal.pointEffect.normalEffectName);
      loadEffectNames.Add(i1.portal.pointEffect.largeEffectName);
      if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
        MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Load(load_queue);
      load_queue.CacheAnimDataUseResource(MonoBehaviourSingleton<InGameSettingsManager>.I.fieldDrop.animEventData);
    }
    else
    {
      InGameSettingsManager.ShadowSealingParam shadowSealingParam = i1.debuff.shadowSealingParam;
      if (shadowSealingParam.startSeId != 0)
        load_queue.CacheSE(shadowSealingParam.startSeId);
      if (shadowSealingParam.loopSeId != 0)
        load_queue.CacheSE(shadowSealingParam.loopSeId);
      if (shadowSealingParam.endSeId != 0)
        load_queue.CacheSE(shadowSealingParam.endSeId);
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.shadowSealingEffectName);
      loadEffectNames.Add(linkResources.shadowSealingEffectName);
    }
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.enemyOtherSimpleHitEffectName);
    loadEffectNames.Add(linkResources.enemyOtherSimpleHitEffectName);
    string name1 = "ef_btl_pl_frozen_01";
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name1);
    loadEffectNames.Add(name1);
    string name2 = "ef_btl_wsk_bow_01_04";
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name2);
    loadEffectNames.Add(name2);
    string name3 = "ef_btl_enm_flinch_01";
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name3);
    loadEffectNames.Add(name3);
    InGameSettingsManager.Concussion concussion = i1.debuff.concussion;
    if (concussion.startSeId != 0)
      load_queue.CacheSE(concussion.startSeId);
    if (concussion.loopSeId != 0)
      load_queue.CacheSE(concussion.loopSeId);
    if (concussion.endSeId != 0)
      load_queue.CacheSE(concussion.endSeId);
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.bleedingParam.effectName);
    MonoBehaviourSingleton<InGameProgress>.I.CacheUseResources(load_queue, ref loadEffectNames);
    MonoBehaviourSingleton<ResourceManager>.I.cache.AddIgnoreCategorySpecifiedReleaseList(loadEffectNames);
    if (QuestManager.IsValidInGameWaveMatch())
    {
      InGameSettingsManager.WaveMatchParam waveMatchParam = i1.GetWaveMatchParam();
      load_queue.CacheSE(waveMatchParam.waveJingleId);
      load_queue.CacheSE(waveMatchParam.targetHitSeId);
      load_queue.CacheSE(waveMatchParam.targetBreakSeId);
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, waveMatchParam.targetChangeAnimEffect);
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, waveMatchParam.targetHitEffect);
    }
    this.CacheAudio(load_queue);
    if (load_queue.IsLoading())
      yield return (object) load_queue.Wait();
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      MonoBehaviourSingleton<CoopManager>.I.coopMyClient.SetLoadingPer(70);
    yield return (object) this.StartCoroutine(MonoBehaviourSingleton<InGameManager>.I.InitializeEnemyPop());
    InGameManager.IntervalTransferInfo intervalTransferInfo = (InGameManager.IntervalTransferInfo) null;
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
      intervalTransferInfo = MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo;
    if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
      MonoBehaviourSingleton<StageObjectManager>.I.InitForArena(intervalTransferInfo);
    else
      MonoBehaviourSingleton<StageObjectManager>.I.Init(intervalTransferInfo);
    List<Character> load_check_list = new List<Character>();
    int index1 = 0;
    for (int count = MonoBehaviourSingleton<StageObjectManager>.I.characterList.Count; index1 < count; ++index1)
    {
      Character character = MonoBehaviourSingleton<StageObjectManager>.I.characterList[index1] as Character;
      if (character.IsCoopNone() && character.isLoading && Object.op_Equality((Object) MonoBehaviourSingleton<StageObjectManager>.I.FindNonPlayer(character.id), (Object) null))
        load_check_list.Add(character);
    }
    int i = 0;
    for (int len = load_check_list.Count; i < len; ++i)
    {
      Character character = load_check_list[i];
      while (character.isLoading)
        yield return (object) null;
      character = (Character) null;
    }
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
      MonoBehaviourSingleton<StageObjectManager>.I.self.hitOffFlag |= StageObject.HIT_OFF_FLAG.INITIALIZE;
    if (isExploreMiniMapActive)
      MonoBehaviourSingleton<ExploreMiniMap>.I.Initialize();
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      MonoBehaviourSingleton<CoopManager>.I.coopMyClient.LoadingFinish();
    if (!FieldManager.IsValidInTutorial())
      yield return (object) MonoBehaviourSingleton<AppMain>.I.ClearMemory(false, false);
    if (MonoBehaviourSingleton<InGameProgress>.IsValid())
    {
      float currentLimitTime = MonoBehaviourSingleton<InGameManager>.I.GetCurrentLimitTime();
      MonoBehaviourSingleton<InGameProgress>.I.SetLimitTime(currentLimitTime);
      if (MonoBehaviourSingleton<InGameManager>.I.IsRush())
        MonoBehaviourSingleton<InGameProgress>.I.SetRushRemainTime((int) currentLimitTime);
      if (MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
        MonoBehaviourSingleton<InGameProgress>.I.SetArenaRemainTime((XorInt) Mathf.FloorToInt(currentLimitTime));
      if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsDefenseBattle())
      {
        MonoBehaviourSingleton<InGameProgress>.I.SetDefenseBattleEnduranceMax(MonoBehaviourSingleton<InGameSettingsManager>.I.defenseBattleParam.defenseEndurance);
        if ((double) MonoBehaviourSingleton<QuestManager>.I.GetRemainEndurance() > 0.0)
          MonoBehaviourSingleton<InGameProgress>.I.SetDefenseBattleEndurance(MonoBehaviourSingleton<QuestManager>.I.GetRemainEndurance());
        else
          MonoBehaviourSingleton<InGameProgress>.I.SetDefenseBattleEndurance(MonoBehaviourSingleton<InGameSettingsManager>.I.defenseBattleParam.defenseEndurance);
      }
      if (QuestManager.IsValidInGameExplore() && MonoBehaviourSingleton<CoopManager>.I.isStageHost && MonoBehaviourSingleton<InGameManager>.I.isAlreadyBattleStarted && MonoBehaviourSingleton<InGameProgress>.I.enableLimitTime)
        MonoBehaviourSingleton<InGameProgress>.I.StartTimer();
      if (FieldManager.IsValidInGameNoQuest())
        MonoBehaviourSingleton<InGameProgress>.I.SetAfkLimitTime();
    }
    MonoBehaviourSingleton<InGameManager>.I.CheckStageInitialState();
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      yield return (object) this.StartCoroutine(MonoBehaviourSingleton<CoopManager>.I.coopStage.DoActivate());
    MonoBehaviourSingleton<StageObjectManager>.I.objectList.ForEach((Action<StageObject>) (o =>
    {
      if (!Object.op_Inequality((Object) o.controller, (Object) null))
        return;
      o.controller.SetEnableControll(true, ControllerBase.DISABLE_FLAG.BATTLE_START);
    }));
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (Object.op_Inequality((Object) self, (Object) null))
    {
      self.hitOffFlag &= ~StageObject.HIT_OFF_FLAG.INITIALIZE;
      if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.boss, (Object) null))
        self.SetActionTarget((StageObject) MonoBehaviourSingleton<StageObjectManager>.I.boss);
    }
    if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
    {
      this.prevLevel = (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
      if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.levelUp, (Object) null))
        MonoBehaviourSingleton<UIManager>.I.levelUp.GetNowStatus();
    }
    if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
    {
      if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
        MonoBehaviourSingleton<InGameCameraManager>.I.target = MonoBehaviourSingleton<StageObjectManager>.I.self._transform;
      MonoBehaviourSingleton<InGameCameraManager>.I.AdjustCameraPosition();
    }
    if (FieldManager.IsValidInGameNoQuest() && MonoBehaviourSingleton<DeliveryManager>.IsValid())
      DropTargetMarkerManeger.Create();
    if (MonoBehaviourSingleton<EffectManager>.IsValid())
      MonoBehaviourSingleton<EffectManager>.I.enableStock = true;
    if (QuestManager.IsValidInGameDefenseBattle())
    {
      if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
        MonoBehaviourSingleton<UIPlayerStatus>.I.DoDisable();
    }
    else if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
      MonoBehaviourSingleton<UIEnduranceStatus>.I.DoDisable();
    yield return (object) 0;
    PredownloadManager.Stop(PredownloadManager.STOP_FLAG.INGAME_MAIN, false);
    if (Object.op_Inequality((Object) vorgonPreEventController, (Object) null))
      ((Behaviour) vorgonPreEventController).enabled = true;
    if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
      MonoBehaviourSingleton<UIPlayerStatus>.I.ResetSpActionGaugeState();
    if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
      MonoBehaviourSingleton<UIEnduranceStatus>.I.ResetSpActionGaugeState();
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
    {
      MonoBehaviourSingleton<StageObjectManager>.I.self.ResetShadowSealingUI();
      MonoBehaviourSingleton<StageObjectManager>.I.self.ResetConcussionUI();
    }
    this.SyncRotatePosition();
    if (UIInGameFieldMenu.IsValid() && !UIInGameFieldMenu.I.IsPopMenu())
    {
      FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
      if (fieldMapData != null)
      {
        if (!FieldManager.HasWorldMap(fieldMapData.mapID))
          UIInGameFieldMenu.I.SetDisableMapButton(true);
      }
      else
        UIInGameFieldMenu.I.SetDisableMapButton(true);
      if ((int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < MonoBehaviourSingleton<GlobalSettingsManager>.I.unlockEventLevel)
        UIInGameFieldMenu.I.SetDisableEventButton(true);
    }
    bool waitGetAutoTime = true;
    if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
      MonoBehaviourSingleton<UIPlayerStatus>.I.autoBattleButton.GetAutoPlayTime((Action<bool>) (is_success => waitGetAutoTime = false));
    while (waitGetAutoTime)
      yield return (object) null;
    int tutorialStep = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep;
    if (MonoBehaviourSingleton<UIQuestRepeat>.IsValid())
      MonoBehaviourSingleton<UIQuestRepeat>.I.InitData();
    Debug.Log((object) "STEP FINISH");
    base.Initialize();
  }

  private IEnumerator DoInitialize_GG_Optimization()
  {
    UILabel.OutlineLimit = MonoBehaviourSingleton<InGameManager>.I.graphicOptionType <= 0;
    Application.backgroundLoadingPriority = (ThreadPriority) 4;
    PredownloadManager.Stop(PredownloadManager.STOP_FLAG.INGAME_MAIN, true);
    while (MonoBehaviourSingleton<DataTableManager>.I.IsLoading())
      yield return (object) null;
    bool isWaitQuestLoad = true;
    this.StartCoroutine(this.DoInitializeQuest((System.Action) (() => isWaitQuestLoad = false)));
    while (isWaitQuestLoad)
      yield return (object) null;
    bool isWaitChatUILoad = true;
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    if (MonoBehaviourSingleton<AppMain>.IsValid())
      ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<AttackColliderManager>();
    this.inGameMembers = new GameObject("InGameMembers").transform;
    Transform _transform = this.inGameMembers;
    ((Component) _transform).transform.parent = MonoBehaviourSingleton<AppMain>.I._transform;
    this.inGameUIMembers = Utility.CreateGameObject("InGameUIMembers", MonoBehaviourSingleton<UIManager>.I._transform, 5);
    ResourceManager.enableCache = false;
    LoadObject lo_prefabs = load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemInGame", new string[8]
    {
      "InGameSettingsManager",
      "InGameCameraManager",
      "PuniConManager",
      "PlayerAttackInfo_00",
      "PlayerAttackInfo_01",
      "PlayerAttackInfo_02",
      "PlayerAttackInfo_04",
      "PlayerAttackInfo_05"
    });
    LoadObject lo_tutorial_prefab = (LoadObject) null;
    if (!TutorialStep.HasFirstDeliveryCompleted() && TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.USER_CREATE_02))
      lo_tutorial_prefab = load_queue.Load(RESOURCE_CATEGORY.UI, "UI_TutorialFieldHelper");
    LoadObject lo_link_resources = (LoadObject) null;
    ResourceManager.enableCache = true;
    VorgonPreEventController vorgonPreEventController = (VorgonPreEventController) null;
    bool isWaitCutScreen = true;
    this.StartCoroutine(this.DoInitializeCutScreen((Action<VorgonPreEventController>) (o =>
    {
      vorgonPreEventController = o;
      isWaitCutScreen = false;
    })));
    bool isExploreMiniMapActive = MonoBehaviourSingleton<ExploreMiniMap>.IsValid() && ((Component) MonoBehaviourSingleton<ExploreMiniMap>.I).gameObject.activeSelf;
    if (isExploreMiniMapActive)
      MonoBehaviourSingleton<ExploreMiniMap>.I.Preload(load_queue);
    LoadObject lo_ingame_rush = (LoadObject) null;
    LoadObject loAdditionalDamageNum = load_queue.Load(RESOURCE_CATEGORY.UI, "InGameAdditionalDamageNum");
    LoadObject loPlayerDamageNum = load_queue.Load(RESOURCE_CATEGORY.UI, "InGamePlayerDamageNum");
    LoadObject loDamageNum = load_queue.Load(RESOURCE_CATEGORY.UI, "InGameDamageNum");
    bool isNotLinkResource = true;
    while (isWaitQuestLoad | isWaitChatUILoad | isWaitCutScreen | isNotLinkResource || load_queue.IsLoading())
    {
      if (!isWaitQuestLoad & isNotLinkResource)
      {
        isNotLinkResource = false;
        this.StartCoroutine(this.DoInitializeChatUI((System.Action) (() => isWaitChatUILoad = false)));
        if (QuestManager.IsValidInGame() && MonoBehaviourSingleton<AppMain>.IsValid() && !MonoBehaviourSingleton<InGameRecorder>.IsValid())
          ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.AddComponent<InGameRecorder>();
        ResourceManager.enableCache = false;
        if (FieldManager.IsValidInTutorial())
        {
          lo_link_resources = load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemInGameLinkResources", new string[3]
          {
            "InGameLinkResourcesCommon",
            "InGameLinkResourcesQuest",
            "InGameLinkResourcesField"
          });
        }
        else
        {
          List<string> stringList = new List<string>(3);
          stringList.Add("InGameLinkResourcesCommon");
          if (QuestManager.IsValidInGame() || InGameManager.IsValidInGameTest())
            stringList.Add("InGameLinkResourcesQuest");
          if (FieldManager.IsValidInGameNoBoss() || InGameManager.IsValidInGameTest())
            stringList.Add("InGameLinkResourcesField");
          lo_link_resources = load_queue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemInGameLinkResources", stringList.ToArray());
        }
        ResourceManager.enableCache = true;
        if (MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameWaveMatch(true))
          lo_ingame_rush = load_queue.Load(RESOURCE_CATEGORY.UI, "InGameRush");
        while (load_queue.IsLoading())
          yield return (object) null;
      }
      if (load_queue.IsStop())
        yield return (object) null;
      else
        yield return (object) load_queue.Wait();
    }
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      MonoBehaviourSingleton<CoopManager>.I.coopMyClient.SetLoadingPer(20);
    Transform parent = (Transform) null;
    InGameSettingsManager.Player player1 = (InGameSettingsManager.Player) null;
    foreach (ResourceObject loadedObject in lo_prefabs.loadedObjects)
    {
      string name = loadedObject.obj.name;
      if (name.Contains("PlayerAttackInfo"))
      {
        if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
        {
          if (Object.op_Equality((Object) parent, (Object) null))
            parent = MonoBehaviourSingleton<InGameSettingsManager>.I._transform;
          if (player1 == null)
          {
            player1 = MonoBehaviourSingleton<InGameSettingsManager>.I.player;
            player1.attackInfosAll = new AttackInfo[0];
          }
          AttackInfos component = ((Component) ResourceUtility.Realizes(loadedObject.obj, parent)).gameObject.GetComponent<AttackInfos>();
          player1.weaponAttackInfoList.Add(component);
          player1.attackInfosAll = Utility.CreateMergedArray<AttackInfo>(player1.attackInfosAll, (AttackInfo[]) component.attackHitInfos);
        }
      }
      else if (name.Contains("PuniConManager"))
      {
        ResourceUtility.Realizes(loadedObject.obj, this.inGameUIMembers, 5);
        MonoBehaviourSingleton<PuniConManager>.I.enableMultiTouch = true;
      }
      else
        ResourceUtility.Realizes(loadedObject.obj, _transform);
    }
    foreach (ResourceObject loadedObject in lo_link_resources.loadedObjects)
      ResourceUtility.Realizes(loadedObject.obj, MonoBehaviourSingleton<InGameSettingsManager>.I._transform);
    Utility.CreateGameObjectAndComponent("StageObjectManager", _transform);
    Utility.CreateGameObjectAndComponent("TargetMarkerManager", _transform);
    Utility.CreateGameObjectAndComponent("InGameProgress", _transform);
    Utility.CreateGameObjectAndComponent("AIManager", _transform);
    if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.invitationInGameButton, (Object) null))
    {
      ((Component) MonoBehaviourSingleton<UIManager>.I.invitationInGameButton).gameObject.SetActive(true);
      if (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite)
        MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Open();
    }
    if (lo_tutorial_prefab != null)
    {
      Transform transform = ResourceUtility.Realizes(lo_tutorial_prefab.loadedObject, this.inGameUIMembers);
      if (Object.op_Inequality((Object) transform, (Object) null))
      {
        UITutorialFieldHelper component = ((Component) transform).GetComponent<UITutorialFieldHelper>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.Setup(this);
      }
    }
    if (MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameWaveMatch(true))
    {
      GameObject gameObject = Object.Instantiate(lo_ingame_rush.loadedObject) as GameObject;
      gameObject.transform.parent = MonoBehaviourSingleton<UIContinueButton>.I._transform.parent;
      gameObject.transform.localPosition = Vector3.zero;
      gameObject.transform.localRotation = Quaternion.identity;
      gameObject.transform.localScale = Vector3.one;
      MonoBehaviourSingleton<UISpectatorButton>.I.Initialize(gameObject.GetComponentInParent<UIStaticPanelChanger>());
    }
    if (MonoBehaviourSingleton<UIDamageManager>.IsValid())
      MonoBehaviourSingleton<UIDamageManager>.I.RegisterDamageNumResources(loDamageNum.loadedObject, loPlayerDamageNum.loadedObject, loAdditionalDamageNum.loadedObject);
    bool isWaitLoadScene = true;
    bool isWaitLoadCharacter = true;
    bool isWaitLoadEnemy = true;
    bool isBreak = false;
    this.StartCoroutine(this.DoInitializeSceneLoad((System.Action) (() =>
    {
      isWaitLoadScene = false;
      InGameManager.IntervalTransferInfo intervalTransferInfo = (InGameManager.IntervalTransferInfo) null;
      if (MonoBehaviourSingleton<InGameManager>.IsValid())
        intervalTransferInfo = MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo;
      if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
        MonoBehaviourSingleton<StageObjectManager>.I.InitForArena(intervalTransferInfo);
      else
        MonoBehaviourSingleton<StageObjectManager>.I.Init(intervalTransferInfo);
      this.StartCoroutine(MonoBehaviourSingleton<InGameManager>.I.InitializeEnemyPop_GG_Optimize((Action<bool>) (o =>
      {
        isWaitLoadEnemy = false;
        if (o)
          return;
        isBreak = true;
      }), (System.Action) (() => { })));
      this.StartCoroutine(this.DoInitializeCharacter((System.Action) (() => isWaitLoadCharacter = false)));
    })));
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      MonoBehaviourSingleton<CoopManager>.I.coopMyClient.SetLoadingPer(40);
    InGameSettingsManager i = MonoBehaviourSingleton<InGameSettingsManager>.I;
    GlobalSettingsManager.LinkResources linkResources = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources;
    List<string> loadEffectNames = new List<string>();
    for (int index = 0; index < 3; ++index)
    {
      InGameSettingsManager.UseResources useResources = (InGameSettingsManager.UseResources) null;
      switch (index)
      {
        case 0:
          useResources = linkResources.inGameCommonResources;
          break;
        case 1:
          if (FieldManager.IsValidInGameNoQuest() || FieldManager.IsValidInGameNoBoss())
          {
            useResources = i.useResourcesField;
            break;
          }
          break;
        case 2:
          if (QuestManager.IsValidInGame() || InGameManager.IsValidInGameTest() || FieldManager.IsValidInTutorial())
          {
            useResources = linkResources.inGameQuestResources;
            break;
          }
          break;
      }
      if (useResources != null)
      {
        foreach (string effect in useResources.effects)
        {
          load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effect);
          loadEffectNames.Add(effect);
        }
        foreach (string uiEffect in useResources.uiEffects)
          load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, uiEffect);
      }
    }
    InGameSettingsManager.Player player2 = i.player;
    foreach (string stunnedEffect in linkResources.stunnedEffectList)
    {
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, stunnedEffect);
      loadEffectNames.Add(stunnedEffect);
    }
    foreach (string charmEffect in linkResources.charmEffectList)
    {
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, charmEffect);
      loadEffectNames.Add(charmEffect);
    }
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.battleStartEffectName);
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.changeWeaponEffectName);
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.spActionStartEffectName);
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.arrowBleedOtherEffectName);
    loadEffectNames.Add(linkResources.battleStartEffectName);
    loadEffectNames.Add(linkResources.changeWeaponEffectName);
    loadEffectNames.Add(linkResources.spActionStartEffectName);
    loadEffectNames.Add(linkResources.arrowBleedOtherEffectName);
    foreach (int elementHitSeiD in i.enemy.elementHitSEIDs)
      load_queue.CacheSE(elementHitSeiD);
    if (FieldManager.IsValidInGameNoBoss())
    {
      foreach (string effectName in i.portal.effectNames)
      {
        load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effectName);
        loadEffectNames.Add(effectName);
      }
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, i.fieldDrop.tresureBoxOpenEffect);
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, i.portal.pointGetEffectName);
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, i.portal.pointEffect.normalEffectName);
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, i.portal.pointEffect.largeEffectName);
      loadEffectNames.Add(i.fieldDrop.tresureBoxOpenEffect);
      loadEffectNames.Add(i.portal.pointGetEffectName);
      loadEffectNames.Add(i.portal.pointEffect.normalEffectName);
      loadEffectNames.Add(i.portal.pointEffect.largeEffectName);
      if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
        MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Load(load_queue);
      load_queue.CacheAnimDataUseResource(MonoBehaviourSingleton<InGameSettingsManager>.I.fieldDrop.animEventData);
    }
    else
    {
      InGameSettingsManager.ShadowSealingParam shadowSealingParam = i.debuff.shadowSealingParam;
      if (shadowSealingParam.startSeId != 0)
        load_queue.CacheSE(shadowSealingParam.startSeId);
      if (shadowSealingParam.loopSeId != 0)
        load_queue.CacheSE(shadowSealingParam.loopSeId);
      if (shadowSealingParam.endSeId != 0)
        load_queue.CacheSE(shadowSealingParam.endSeId);
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.shadowSealingEffectName);
      loadEffectNames.Add(linkResources.shadowSealingEffectName);
    }
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, linkResources.enemyOtherSimpleHitEffectName);
    loadEffectNames.Add(linkResources.enemyOtherSimpleHitEffectName);
    string name1 = "ef_btl_pl_frozen_01";
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name1);
    loadEffectNames.Add(name1);
    string name2 = "ef_btl_wsk_bow_01_04";
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name2);
    loadEffectNames.Add(name2);
    string name3 = "ef_btl_enm_flinch_01";
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, name3);
    loadEffectNames.Add(name3);
    InGameSettingsManager.Concussion concussion = i.debuff.concussion;
    if (concussion.startSeId != 0)
      load_queue.CacheSE(concussion.startSeId);
    if (concussion.loopSeId != 0)
      load_queue.CacheSE(concussion.loopSeId);
    if (concussion.endSeId != 0)
      load_queue.CacheSE(concussion.endSeId);
    load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.bleedingParam.effectName);
    MonoBehaviourSingleton<InGameProgress>.I.CacheUseResources(load_queue, ref loadEffectNames);
    MonoBehaviourSingleton<ResourceManager>.I.cache.AddIgnoreCategorySpecifiedReleaseList(loadEffectNames);
    if (QuestManager.IsValidInGameWaveMatch())
    {
      InGameSettingsManager.WaveMatchParam waveMatchParam = i.GetWaveMatchParam();
      load_queue.CacheSE(waveMatchParam.waveJingleId);
      load_queue.CacheSE(waveMatchParam.targetHitSeId);
      load_queue.CacheSE(waveMatchParam.targetBreakSeId);
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, waveMatchParam.targetChangeAnimEffect);
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, waveMatchParam.targetHitEffect);
    }
    bool isNotInitCharacter = true;
    while (isWaitLoadScene | isWaitLoadEnemy | isWaitLoadCharacter || load_queue.IsLoading())
    {
      if (!isWaitLoadScene & isNotInitCharacter)
      {
        isNotInitCharacter = false;
        this.CacheAudio(load_queue);
        if (MonoBehaviourSingleton<CoopManager>.IsValid())
          MonoBehaviourSingleton<CoopManager>.I.coopMyClient.SetLoadingPer(70);
      }
      if (load_queue.IsStop())
        yield return (object) null;
      else
        yield return (object) load_queue.Wait();
    }
    int num = MonoBehaviourSingleton<StageManager>.I.isValidInside ? 1 : 0;
    Application.backgroundLoadingPriority = (ThreadPriority) 0;
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
      MonoBehaviourSingleton<StageObjectManager>.I.self.hitOffFlag |= StageObject.HIT_OFF_FLAG.INITIALIZE;
    if (isExploreMiniMapActive)
      MonoBehaviourSingleton<ExploreMiniMap>.I.Initialize();
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      MonoBehaviourSingleton<CoopManager>.I.coopMyClient.LoadingFinish();
    if (!FieldManager.IsValidInTutorial())
      yield return (object) MonoBehaviourSingleton<AppMain>.I.ClearMemory(false, false);
    if (MonoBehaviourSingleton<InGameProgress>.IsValid())
    {
      float currentLimitTime = MonoBehaviourSingleton<InGameManager>.I.GetCurrentLimitTime();
      MonoBehaviourSingleton<InGameProgress>.I.SetLimitTime(currentLimitTime);
      if (MonoBehaviourSingleton<InGameManager>.I.IsRush())
        MonoBehaviourSingleton<InGameProgress>.I.SetRushRemainTime((int) currentLimitTime);
      if (MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
        MonoBehaviourSingleton<InGameProgress>.I.SetArenaRemainTime((XorInt) Mathf.FloorToInt(currentLimitTime));
      if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsDefenseBattle())
      {
        MonoBehaviourSingleton<InGameProgress>.I.SetDefenseBattleEnduranceMax(MonoBehaviourSingleton<InGameSettingsManager>.I.defenseBattleParam.defenseEndurance);
        if ((double) MonoBehaviourSingleton<QuestManager>.I.GetRemainEndurance() > 0.0)
          MonoBehaviourSingleton<InGameProgress>.I.SetDefenseBattleEndurance(MonoBehaviourSingleton<QuestManager>.I.GetRemainEndurance());
        else
          MonoBehaviourSingleton<InGameProgress>.I.SetDefenseBattleEndurance(MonoBehaviourSingleton<InGameSettingsManager>.I.defenseBattleParam.defenseEndurance);
      }
      if (QuestManager.IsValidInGameExplore() && MonoBehaviourSingleton<CoopManager>.I.isStageHost && MonoBehaviourSingleton<InGameManager>.I.isAlreadyBattleStarted && MonoBehaviourSingleton<InGameProgress>.I.enableLimitTime)
        MonoBehaviourSingleton<InGameProgress>.I.StartTimer();
      if (FieldManager.IsValidInGameNoQuest())
        MonoBehaviourSingleton<InGameProgress>.I.SetAfkLimitTime();
    }
    MonoBehaviourSingleton<InGameManager>.I.CheckStageInitialState();
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      yield return (object) this.StartCoroutine(MonoBehaviourSingleton<CoopManager>.I.coopStage.DoActivate());
    MonoBehaviourSingleton<StageObjectManager>.I.objectList.ForEach((Action<StageObject>) (o =>
    {
      if (!Object.op_Inequality((Object) o.controller, (Object) null))
        return;
      o.controller.SetEnableControll(true, ControllerBase.DISABLE_FLAG.BATTLE_START);
    }));
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (Object.op_Inequality((Object) self, (Object) null))
    {
      self.hitOffFlag &= ~StageObject.HIT_OFF_FLAG.INITIALIZE;
      if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.boss, (Object) null))
        self.SetActionTarget((StageObject) MonoBehaviourSingleton<StageObjectManager>.I.boss);
    }
    if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
    {
      this.prevLevel = (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
      if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.levelUp, (Object) null))
        MonoBehaviourSingleton<UIManager>.I.levelUp.GetNowStatus();
    }
    if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
    {
      if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
        MonoBehaviourSingleton<InGameCameraManager>.I.target = MonoBehaviourSingleton<StageObjectManager>.I.self._transform;
      MonoBehaviourSingleton<InGameCameraManager>.I.AdjustCameraPosition();
    }
    if (FieldManager.IsValidInGameNoQuest() && MonoBehaviourSingleton<DeliveryManager>.IsValid())
      DropTargetMarkerManeger.Create();
    if (MonoBehaviourSingleton<EffectManager>.IsValid())
      MonoBehaviourSingleton<EffectManager>.I.enableStock = true;
    if (QuestManager.IsValidInGameDefenseBattle())
    {
      if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
        MonoBehaviourSingleton<UIPlayerStatus>.I.DoDisable();
    }
    else if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
      MonoBehaviourSingleton<UIEnduranceStatus>.I.DoDisable();
    yield return (object) 0;
    PredownloadManager.Stop(PredownloadManager.STOP_FLAG.INGAME_MAIN, false);
    if (Object.op_Inequality((Object) vorgonPreEventController, (Object) null))
      ((Behaviour) vorgonPreEventController).enabled = true;
    if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
      MonoBehaviourSingleton<UIPlayerStatus>.I.ResetSpActionGaugeState();
    if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
      MonoBehaviourSingleton<UIEnduranceStatus>.I.ResetSpActionGaugeState();
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
    {
      MonoBehaviourSingleton<StageObjectManager>.I.self.ResetShadowSealingUI();
      MonoBehaviourSingleton<StageObjectManager>.I.self.ResetConcussionUI();
    }
    this.SyncRotatePosition();
    if (UIInGameFieldMenu.IsValid() && !UIInGameFieldMenu.I.IsPopMenu())
    {
      FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
      if (fieldMapData != null)
      {
        if (!FieldManager.HasWorldMap(fieldMapData.mapID))
          UIInGameFieldMenu.I.SetDisableMapButton(true);
      }
      else
        UIInGameFieldMenu.I.SetDisableMapButton(true);
      if ((int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < MonoBehaviourSingleton<GlobalSettingsManager>.I.unlockEventLevel)
        UIInGameFieldMenu.I.SetDisableEventButton(true);
    }
    bool waitGetAutoTime = true;
    if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
      MonoBehaviourSingleton<UIPlayerStatus>.I.autoBattleButton.GetAutoPlayTime((Action<bool>) (is_success => waitGetAutoTime = false));
    while (waitGetAutoTime)
      yield return (object) null;
    int tutorialStep = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep;
    if (MonoBehaviourSingleton<UIQuestRepeat>.IsValid())
      MonoBehaviourSingleton<UIQuestRepeat>.I.InitData();
    Application.backgroundLoadingPriority = (ThreadPriority) 0;
    base.Initialize();
  }

  private IEnumerator DoInitializeQuest(System.Action callBack)
  {
    if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.isRetry)
    {
      bool isWaitQuestLoad = true;
      if (MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
        CoopApp.EnterArenaQuestOffline((Action<bool, bool, bool, bool>) ((isMatching, isConnect, isRegist, isStart) => isWaitQuestLoad = !isStart));
      else if (MonoBehaviourSingleton<StatusManager>.IsValid() && MonoBehaviourSingleton<StatusManager>.I.assignedCharaInfo != null && MonoBehaviourSingleton<StatusManager>.I.assignedEquipmentData != null)
      {
        CoopApp.EnterQuestOfflineAssignedEquipment((AssignedEquipmentTable.AssignedEquipmentData) null, (CharaInfo) null, (Action<bool, bool, bool, bool>) ((isMatching, isConnect, isRegist, isStart) => isWaitQuestLoad = !isStart));
      }
      else
      {
        MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID((uint) GameSaveData.instance.lastQusetID);
        if (MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeriesArena())
          CoopApp.EnterSeriesArenaQuestOffline((Action<bool, bool, bool, bool>) ((isMatching, isConnect, isRegist, isStart) => isWaitQuestLoad = !isStart));
      }
      while (isWaitQuestLoad)
        yield return (object) null;
      if (MonoBehaviourSingleton<QuestManager>.IsValid())
        SoundManager.RequestBGM(MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestBGMID());
      MonoBehaviourSingleton<InGameManager>.I.isRetry = false;
    }
    Debug.Log((object) "Finish Load Quest ");
    callBack();
  }

  private IEnumerator DoInitializeChatUI(System.Action callBack)
  {
    this.SetActive((Enum) InGameMain.UI.BTN_QUEST_MENU, QuestManager.IsValidInGame());
    this.InitializeChatUI();
    if (MonoBehaviourSingleton<WorldMapManager>.I.GetOpenRegionIdListInWorldMap(REGION_DIFFICULTY_TYPE.NORMAL).Length >= 2)
      this.SetEvent((Enum) InGameMain.UI.BTN_REQUEST, "QUEST_SELECT_WINDOW", (int) MonoBehaviourSingleton<FieldManager>.I.currentMapData.regionId);
    if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      MonoBehaviourSingleton<CoopManager>.I.coopMyClient.LoadingStart();
    while (MonoBehaviourSingleton<LoadingProcess>.IsValid())
      yield return (object) null;
    callBack();
  }

  private IEnumerator DoInitializeCutScreen(Action<VorgonPreEventController> callBack)
  {
    VorgonPreEventController vorgonPreEventController = (VorgonPreEventController) null;
    if (MonoBehaviourSingleton<QuestManager>.IsValid())
    {
      QuestManager.VorgonQuetType vorgonQuestType = MonoBehaviourSingleton<QuestManager>.I.GetVorgonQuestType();
      if (vorgonQuestType != QuestManager.VorgonQuetType.NONE)
      {
        GameObject gameObject = new GameObject("VorgonQuestController");
        vorgonPreEventController = gameObject.AddComponent<VorgonPreEventController>();
        ((Behaviour) vorgonPreEventController).enabled = false;
        gameObject.transform.parent = this._transform;
        if (vorgonQuestType == QuestManager.VorgonQuetType.BATTLE_WITH_WYBURN)
        {
          string cutSceneDataPath = "ev001_CutScene";
          bool wait = true;
          this.cutScenePlayer = gameObject.AddComponent<CutScenePlayer>();
          this.cutScenePlayer.Init(cutSceneDataPath, (Action<bool>) (success => wait = false));
          while (wait)
            yield return (object) null;
        }
      }
    }
    callBack(vorgonPreEventController);
  }

  private IEnumerator DoInitializeSceneLoad(System.Action callBack)
  {
    string str = MonoBehaviourSingleton<InGameManager>.IsValid() ? MonoBehaviourSingleton<InGameManager>.I.GetCurrentStageName() : "ST011D_01";
    if (MonoBehaviourSingleton<GameSceneManager>.IsValid())
      MonoBehaviourSingleton<GameSceneManager>.I.SetExternalStageName(str);
    MonoBehaviourSingleton<StageManager>.I.LoadStage(str);
    while (MonoBehaviourSingleton<StageManager>.I.isLoading)
      yield return (object) null;
    callBack();
  }

  private IEnumerator DoInitializeCharacter(System.Action callBack)
  {
    List<Character> load_check_list = new List<Character>();
    int index = 0;
    for (int count = MonoBehaviourSingleton<StageObjectManager>.I.characterList.Count; index < count; ++index)
    {
      Character character = MonoBehaviourSingleton<StageObjectManager>.I.characterList[index] as Character;
      if (character.IsCoopNone() && character.isLoading && Object.op_Equality((Object) MonoBehaviourSingleton<StageObjectManager>.I.FindNonPlayer(character.id), (Object) null))
        load_check_list.Add(character);
    }
    int i = 0;
    for (int len = load_check_list.Count; i < len; ++i)
    {
      Character character = load_check_list[i];
      while (character.isLoading)
        yield return (object) null;
      character = (Character) null;
    }
    callBack();
  }

  public void SetMapButtonState()
  {
    if (!UIInGameFieldMenu.IsValid())
      return;
    FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
    if (fieldMapData != null)
    {
      if (!FieldManager.HasWorldMap(fieldMapData.mapID))
        UIInGameFieldMenu.I.SetDisableMapButton(true);
    }
    else
      UIInGameFieldMenu.I.SetDisableMapButton(true);
    if ((int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < MonoBehaviourSingleton<GlobalSettingsManager>.I.unlockEventLevel)
      UIInGameFieldMenu.I.SetDisableEventButton(true);
    else
      UIInGameFieldMenu.I.SetDisableEventButton(false);
  }

  protected override void OnOpen()
  {
    UIExplorePlayerStatusList componentInChildren = ((Component) this).GetComponentInChildren<UIExplorePlayerStatusList>(true);
    if (Object.op_Implicit((Object) componentInChildren))
      ((Component) componentInChildren).gameObject.SetActive(false);
    this.OnScreenRotate(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
    base.OnOpen();
  }

  public override void StartSection()
  {
    if (UITutorialFieldHelper.IsValid())
    {
      MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
      switch (UITutorialFieldHelper.m_State)
      {
        case UITutorialFieldHelper.MessageState.CollectItemImg:
        case UITutorialFieldHelper.MessageState.CollectItem:
          UITutorialFieldHelper.I.OpenTutorialFirstDelivery();
          break;
        case UITutorialFieldHelper.MessageState.BackHome:
          this.NoticeTutorialOnCollectItem();
          this.TutorialStep4_1();
          break;
      }
    }
    PortalUnlockEvent portalUnlockEvent = (PortalUnlockEvent) null;
    List<PortalObject> portalObjectList = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList;
    if (portalObjectList != null)
    {
      for (int index = 0; index < portalObjectList.Count; ++index)
      {
        if (portalObjectList[index].isClearOrder && GameSaveData.instance.isNewReleasePortal(portalObjectList[index].portalID))
        {
          if (Object.op_Equality((Object) portalUnlockEvent, (Object) null))
          {
            portalUnlockEvent = ((Component) MonoBehaviourSingleton<InGameManager>.I).gameObject.AddComponent<PortalUnlockEvent>();
            portalUnlockEvent.SetOnEndAllEvent(new System.Action(this.SetCoopStageReady));
          }
          portalUnlockEvent.AddPortal(portalObjectList[index]);
          GameSaveData.instance.newReleasePortals.Remove(portalObjectList[index].portalID);
        }
      }
    }
    if (QuestManager.IsValidInGameExplore())
      this.InitializeExplorePlayerStatuses();
    if (Object.op_Equality((Object) portalUnlockEvent, (Object) null))
      this.SetCoopStageReady();
    if (MonoBehaviourSingleton<ClanMatchingManager>.IsValid() && MonoBehaviourSingleton<ClanMatchingManager>.I.EnableClanChat)
      MonoBehaviourSingleton<ClanMatchingManager>.I.UpdateUnreadMessage();
    base.StartSection();
  }

  private void SetCoopStageReady()
  {
    if (!MonoBehaviourSingleton<CoopManager>.IsValid())
      return;
    MonoBehaviourSingleton<CoopManager>.I.coopStage.SetReadyToPlay();
  }

  public override void UpdateUI()
  {
    int completableDeliveryNum = MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableDeliveryNum();
    int eventDeliveryNum = MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableEventDeliveryNum();
    this.SetBadge((Enum) InGameMain.UI.BTN_REQUEST, completableDeliveryNum - eventDeliveryNum, (SpriteAlignment) 3, -5, -5);
    this.SetBadge((Enum) InGameMain.UI.BTN_EVENT, eventDeliveryNum, (SpriteAlignment) 3, -5, -5);
    if (LoungeMatchingManager.IsValidInLounge())
      this.SetBadge((Enum) InGameMain.UI.BTN_LOUNGE_MEMBER, MonoBehaviourSingleton<LoungeMatchingManager>.I.GetMemberCount() - 1, (SpriteAlignment) 3, -5, -5);
    if (!UIInGameFieldMenu.IsValid() || UIInGameFieldMenu.I.IsPopMenu())
      return;
    this.SetBadge((Enum) InGameMain.UI.BTN_INGAME_MENU, completableDeliveryNum, (SpriteAlignment) 3, -5, -5);
  }

  public void NoticeTutorialOnCollectItem() => this.is_collect_tutorial = true;

  public void OnQuery_MENU_POP_INGAME()
  {
    if (!UIInGameFieldMenu.IsValid())
      return;
    this.TutorialStep4_2();
    if (!UIInGameFieldMenu.I.IsPopMenu())
      this.SetBadge((Enum) InGameMain.UI.BTN_INGAME_MENU, MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableDeliveryNum(), (SpriteAlignment) 3, -5, -5);
    else
      this.SetBadge((Enum) InGameMain.UI.BTN_INGAME_MENU, 0, (SpriteAlignment) 3, -5, -5);
  }

  private void TutorialStep4_1()
  {
    if (!this.is_collect_tutorial)
      return;
    this.execTutorial4_1 = true;
    MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, true);
    MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("InGameScene", "TutorialStep4_1_1");
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid() || !Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
      return;
    MonoBehaviourSingleton<StageObjectManager>.I.self.hitOffFlag |= StageObject.HIT_OFF_FLAG.TUTORIAL;
  }

  private void TutorialStep4_2()
  {
    MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_TUTORIAL, false);
    if (!this.is_collect_tutorial)
      return;
    MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("InGameScene", "TutorialStep4_1_2");
  }

  private void OnCloseDialog()
  {
    if (!this.is_collect_tutorial || this.execTutorial4_1)
      return;
    this.execTutorial4_1 = true;
    this.TutorialStep4_1();
  }

  public void OnNoticeCompletedDelivery()
  {
    this.RefreshUI();
    this.TutorialStep4_1();
  }

  private void OnDisable()
  {
    if (Object.op_Inequality((Object) this.inGameMembers, (Object) null))
    {
      MonoBehaviourSingleton<InGameManager>.I.SetIntervalTransferSelf();
      Object.DestroyImmediate((Object) ((Component) this.inGameMembers).gameObject);
      this.inGameMembers = (Transform) null;
    }
    if (Object.op_Inequality((Object) this.inGameUIMembers, (Object) null))
    {
      Object.DestroyImmediate((Object) ((Component) this.inGameUIMembers).gameObject);
      this.inGameUIMembers = (Transform) null;
    }
    if (MonoBehaviourSingleton<DropTargetMarkerManeger>.IsValid())
      Object.DestroyImmediate((Object) MonoBehaviourSingleton<DropTargetMarkerManeger>.I);
    if (MonoBehaviourSingleton<EffectManager>.IsValid())
      MonoBehaviourSingleton<EffectManager>.I.enableStock = false;
    if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= new ScreenOrientationManager.OnScreenRotateDelegate(this.OnScreenRotate);
    if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null))
      MonoBehaviourSingleton<UIManager>.I.mainChat.RemoveObserver((UIBehaviour) this);
    if (MonoBehaviourSingleton<AttackColliderManager>.IsValid())
      Object.DestroyImmediate((Object) MonoBehaviourSingleton<AttackColliderManager>.I);
    if (!MonoBehaviourSingleton<UIManager>.IsValid() || !Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.invitationInGameButton, (Object) null))
      return;
    MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Close();
  }

  private void OnQuery_BINGO()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<QuestManager>.I.SendGetBingoEventList((Action<bool>) (b =>
    {
      List<Network.EventData> dataListInSection = MonoBehaviourSingleton<QuestManager>.I.GetValidBingoDataListInSection();
      if (dataListInSection != null && dataListInSection.Count > 0)
      {
        if (dataListInSection.Count == 1)
        {
          Network.EventData firstEvent = dataListInSection[0];
          List<DeliveryTable.DeliveryData> deliveryTableDataList = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryTableDataList(false);
          List<ClearStatusDelivery> clearStatusDelivery1 = MonoBehaviourSingleton<DeliveryManager>.I.clearStatusDelivery;
          Func<DeliveryTable.DeliveryData, bool> predicate = (Func<DeliveryTable.DeliveryData, bool>) (d => d.IsEvent() && d.eventID == firstEvent.eventId);
          int num1 = deliveryTableDataList.Where<DeliveryTable.DeliveryData>(predicate).Count<DeliveryTable.DeliveryData>();
          int num2 = 0;
          for (int index = 0; index < clearStatusDelivery1.Count; ++index)
          {
            ClearStatusDelivery clearStatusDelivery2 = clearStatusDelivery1[index];
            DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) clearStatusDelivery2.deliveryId);
            if (deliveryTableData == null)
              Log.Warning("DeliveryTable Not Found : dId " + (object) clearStatusDelivery2.deliveryId);
            else if (deliveryTableData.IsEvent() && deliveryTableData.eventID == firstEvent.eventId && clearStatusDelivery2.deliveryStatus == 3)
              ++num2;
          }
          if (num1 + num2 == 18)
            GameSection.ChangeStayEvent("MINI_BINGO");
        }
        else
          GameSection.ChangeStayEvent("MINI_BINGO");
      }
      GameSection.ResumeEvent(true);
    }));
  }

  private void OnQuery_RETIRE()
  {
  }

  private void OnQuery_LOUNGE_MEMBER_LIST()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInfo((Action<bool>) (isSuccess => GameSection.ResumeEvent(isSuccess)));
  }

  private void OnQuery_CONTINUE()
  {
    if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsTutorialCurrentQuest())
    {
      GameSection.SetEventData((object) StringTable.Get(STRING_CATEGORY.IN_GAME, 1007U));
    }
    else
    {
      int num = 0;
      if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
        num = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.QUEST_CONTINUE_USE_CRYSTAL;
      GameSection.SetEventData((object) new object[1]
      {
        (object) num
      });
    }
  }

  private void OnQuery_WORLDMAP()
  {
    int event_data = 0;
    FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
    if (fieldMapData != null)
      event_data = (int) fieldMapData.regionId;
    MonoBehaviourSingleton<WorldMapManager>.I.ignoreTutorial = true;
    if (FieldManager.HasWorldMap(fieldMapData.mapID))
      this.RequestEvent("DIRECT_REGION", (object) event_data);
    else
      GameSceneEvent.Cancel();
    this.CloseChatWindow();
  }

  private void OnQuery_InGameMainRetireConfirm_YES()
  {
    if (!MonoBehaviourSingleton<UIContinueButton>.IsValid())
      return;
    MonoBehaviourSingleton<UIContinueButton>.I.DoRetire();
  }

  private void OnQuery_InGameMainContinueConfirm_YES()
  {
    if (!MonoBehaviourSingleton<UIContinueButton>.IsValid())
      return;
    MonoBehaviourSingleton<UIContinueButton>.I.DoContinue();
  }

  private void OnQuery_InGameMainRestartConfirm_YES()
  {
    if (!MonoBehaviourSingleton<UIContinueButton>.IsValid())
      return;
    MonoBehaviourSingleton<UIContinueButton>.I.DoRestart();
  }

  private void OnQuery_InGameMainRetryConfirm_YES()
  {
    if (!MonoBehaviourSingleton<UIContinueButton>.IsValid())
      return;
    MonoBehaviourSingleton<UIContinueButton>.I.DoRetry();
  }

  private void OnQuery_InGameMainPortalNextConfirm_YES()
  {
    if (FieldManager.IsValidInTutorial())
    {
      MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapPortalID(MonoBehaviourSingleton<InGameProgress>.I.checkPortalObject.portalID);
      MonoBehaviourSingleton<FieldManager>.I.useFastTravel = true;
      GameSceneEvent.Cancel();
      MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Title", "CharaMake");
    }
    else
      this.PortalNext();
  }

  private void OnQuery_InGameMainPortalHomeConfirm_YES() => this.PortalNext();

  private void OnQuery_InGameMainPortalLoungeConfirm_YES() => this.PortalNext();

  private void OnQuery_InGameMainReturnHomeConfirm_YES()
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    MonoBehaviourSingleton<InGameProgress>.I.FieldToHome();
  }

  private void OnQuery_InGameReturnLoungeConfirm_YES()
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    MonoBehaviourSingleton<InGameProgress>.I.FieldToHome();
  }

  private void OnQuery_InGameMainPortalQuestConfirm_YES() => this.PortalNext();

  private void OnQuery_InGameMainPortalQuestLockConfirm_YES() => this.PortalNext();

  private void OnQuery_InGameClearedMainStory_YES()
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    MonoBehaviourSingleton<InGameProgress>.I.FieldToHome();
  }

  private void OnQuery_InGameClearedMainStory_NO()
  {
    if (!MonoBehaviourSingleton<DeliveryManager>.IsValid())
      return;
    MonoBehaviourSingleton<DeliveryManager>.I.DeleteCleardDeliveryId();
  }

  private void PortalNext()
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    MonoBehaviourSingleton<InGameProgress>.I.PortalNext(MonoBehaviourSingleton<InGameProgress>.I.checkPortalObject.portalID);
  }

  private void OnCloseDialog_InGameLoungeKickedDialog()
  {
    if (!MonoBehaviourSingleton<CoopNetworkManager>.IsValid())
      return;
    MonoBehaviourSingleton<CoopNetworkManager>.I.KickRoomLeave();
  }

  private void OnCloseDialog_InGameKickedMessage()
  {
    Protocol.Force((System.Action) (() => MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInfo((Action<bool>) (is_success => { }))));
    Protocol.Force((System.Action) (() => MonoBehaviourSingleton<ClanMatchingManager>.I.SendInfo((Action<bool>) (is_success => { }))));
  }

  private void OnQuery_QUEST_WINDOW() => this.CloseChatWindow();

  private void OnQuery_QUEST_SELECT_WINDOW()
  {
    this.CloseChatWindow();
    int eventData = (int) GameSection.GetEventData();
    int count = MonoBehaviourSingleton<DeliveryManager>.I.GetNormalDeliveryList(eventData).Count;
    List<EventData> eventDataList = new List<EventData>();
    eventDataList.Add(new EventData("QUEST_WINDOW", (object) null));
    eventDataList.Add(new EventData("SELECT_NORMAL", (object) null));
    if (count > 0)
      eventDataList.Add(new EventData("SELECT_AREA", (object) eventData));
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(eventDataList.ToArray());
  }

  private void OnQuery_RETURN() => this.CloseChatWindow();

  private void OnQuery_PLAYER_LIST() => this.CloseChatWindow();

  private void OnQuery_OPTION() => this.CloseChatWindow();

  private void OnQuery_EVENT_WINDOW()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent())
      return;
    FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
    if (MonoBehaviourSingleton<DeliveryManager>.I.GetEventDeliveryList(fieldMapData.eventId).Count <= 0)
      return;
    foreach (Network.EventData _data in MonoBehaviourSingleton<QuestManager>.I.eventList)
    {
      if (_data.eventId == fieldMapData.eventId)
      {
        MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[1]
        {
          new EventData("SELECT", (object) _data)
        });
        break;
      }
    }
  }

  private void OnQuery_RALLY_INVITE()
  {
    MonoBehaviourSingleton<UIPlayerStatus>.I.SetDisableRalltBtn(true);
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SendRally((Action<bool>) (is_success => UIInGamePopupDialog.PushOpen(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 35U), false, 3f)));
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((GameSection.NOTIFY_FLAG.UPDATE_USER_STATUS & flags) != (GameSection.NOTIFY_FLAG) 0 && (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level != this.prevLevel)
    {
      if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
        MonoBehaviourSingleton<StageObjectManager>.I.self.OnSetPlayerStatus((int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level, (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.atk, (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.def, (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.hp);
      if (MonoBehaviourSingleton<UIManager>.IsValid())
        MonoBehaviourSingleton<UIManager>.I.levelUp.PlayLevelUp();
      this.prevLevel = (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level;
      if ((int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < MonoBehaviourSingleton<GlobalSettingsManager>.I.unlockEventLevel)
        UIInGameFieldMenu.I.SetDisableEventButton(true);
      else
        UIInGameFieldMenu.I.SetDisableEventButton(false);
    }
    if ((GameSection.NOTIFY_FLAG.UPDATE_DELIVERY_UPDATE & flags) != (GameSection.NOTIFY_FLAG) 0)
    {
      if (MonoBehaviourSingleton<DropTargetMarkerManeger>.IsValid())
        MonoBehaviourSingleton<DropTargetMarkerManeger>.I.UpdateList();
      if (MonoBehaviourSingleton<InGameProgress>.IsValid())
        MonoBehaviourSingleton<InGameProgress>.I.DeliveryAddCheck();
    }
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_PARTY_INVITE) != (GameSection.NOTIFY_FLAG) 0)
    {
      if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.invitationInGameButton, (Object) null) && TutorialStep.HasAllTutorialCompleted())
      {
        if (MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite && !MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.isOpen && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == nameof (InGameMain))
          MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Open();
        else if (!MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite && MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.isOpen)
          MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Close();
      }
    }
    else if ((flags & GameSection.NOTIFY_FLAG.UPDATE_RALLY_INVITE) != (GameSection.NOTIFY_FLAG) 0)
    {
      if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.invitationInGameButton, (Object) null) && TutorialStep.HasAllTutorialCompleted())
      {
        if (MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite && !MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.isOpen && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == nameof (InGameMain))
          MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Open();
        else if (!MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite && MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.isOpen)
          MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Close();
      }
    }
    else if ((flags & GameSection.NOTIFY_FLAG.RESET_DARK_MARKET) != (GameSection.NOTIFY_FLAG) 0 && (int) GoGameTimeManager.GetRemainTime(GameSaveData.instance.resetMarketTime).TotalSeconds > 0)
    {
      GameSaveData.instance.canShowNoteDarkMarket = true;
      MonoBehaviourSingleton<UIAnnounceBand>.I.SetAnnounce(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 37U), "");
    }
    if ((flags & GameSection.NOTIFY_FLAG.LOUNGE_KICKED) != (GameSection.NOTIFY_FLAG) 0)
    {
      this.DispatchEvent("LOUNGE_KICKED");
    }
    else
    {
      if ((flags & GameSection.NOTIFY_FLAG.RECEIVE_COOP_ROOM_UPDATE) != (GameSection.NOTIFY_FLAG) 0 && MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge())
        this.RefreshUI();
      if ((flags & GameSection.NOTIFY_FLAG.TRANSITION_END) != (GameSection.NOTIFY_FLAG) 0)
      {
        if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss, (Object) null))
        {
          MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss.ClearAnnounce();
          if (MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss.IsKnockDownRaidBossByRaidBossHp())
            MonoBehaviourSingleton<UIManager>.I.knockDownRaidBoss.PlayKnockDown();
        }
        if (MonoBehaviourSingleton<FieldManager>.IsValid())
          MonoBehaviourSingleton<FieldManager>.I.MatchingNotice();
      }
      base.OnNotify(flags);
    }
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_QUEST_CLEAR_STATUS | base.GetUpdateUINotifyFlags();
  }

  private void InitializeExplorePlayerStatuses()
  {
    UIExplorePlayerStatusList componentInChildren = ((Component) this).GetComponentInChildren<UIExplorePlayerStatusList>(true);
    if (!Object.op_Implicit((Object) componentInChildren))
      return;
    ((Component) componentInChildren).gameObject.SetActive(true);
    ExploreStatus exploreStatus = MonoBehaviourSingleton<QuestManager>.I.GetExploreStatus();
    componentInChildren.Initialize(exploreStatus);
  }

  private void InitializeChatUI()
  {
    MonoBehaviourSingleton<ChatManager>.I.CreateRoomChatWithCoopIfNeeded();
    MonoBehaviourSingleton<ChatManager>.I.roomChat.JoinRoom(0);
    UIButton component = ((Component) this.GetCtrl((Enum) InGameMain.UI.BTN_CHAT)).GetComponent<UIButton>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    if (!TutorialStep.HasAllTutorialCompleted() || !UserInfoManager.IsFinishTutorial())
      ((Component) component).gameObject.SetActive(false);
    else if (QuestManager.IsValidInGameSeriesArena())
      ((Component) component).gameObject.SetActive(false);
    else if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null))
    {
      MonoBehaviourSingleton<UIManager>.I.mainChat.addObserver((UIBehaviour) this);
      component.onClick.Clear();
      component.onClick.Add(new EventDelegate(new EventDelegate.Callback(MonoBehaviourSingleton<UIManager>.I.mainChat.ShowInputOnly)));
      MonoBehaviourSingleton<UIManager>.I.mainChat.SetRoomChatNameType(!QuestManager.IsValidInGame());
    }
    else
      ((Component) component).gameObject.SetActive(false);
  }

  private void CloseChatWindow()
  {
    if (!MonoBehaviourSingleton<UIManager>.IsValid() || !Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null))
      return;
    MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
  }

  public override void OnModifyChat(MainChat.NOTIFY_FLAG flag)
  {
    if (!MonoBehaviourSingleton<UIManager>.IsValid() || !Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null))
      return;
    if ((flag & MainChat.NOTIFY_FLAG.ARRIVED_MESSAGE) != (MainChat.NOTIFY_FLAG) 0)
      this.SetBadge((Enum) InGameMain.UI.BTN_CHAT, MonoBehaviourSingleton<UIManager>.I.mainChat.GetPendingQueueNum(), (SpriteAlignment) 1, -5, -5);
    if ((flag & MainChat.NOTIFY_FLAG.CLOSE_WINDOW) != (MainChat.NOTIFY_FLAG) 0)
      ((Component) this.GetCtrl((Enum) InGameMain.UI.BTN_CHAT)).gameObject.SetActive(true);
    if ((flag & MainChat.NOTIFY_FLAG.OPEN_WINDOW) != (MainChat.NOTIFY_FLAG) 0)
      ((Component) this.GetCtrl((Enum) InGameMain.UI.BTN_CHAT)).gameObject.SetActive(false);
    if ((flag & MainChat.NOTIFY_FLAG.OPEN_WINDOW_INPUT_ONLY) == (MainChat.NOTIFY_FLAG) 0)
      return;
    ((Component) this.GetCtrl((Enum) InGameMain.UI.BTN_CHAT)).gameObject.SetActive(false);
  }

  private void OnScreenRotate(bool is_portrait) => this.SyncRotatePosition();

  private void SyncRotatePosition()
  {
    bool isPortrait = SpecialDeviceManager.IsPortrait;
    Transform ctrl = this.GetCtrl((Enum) InGameMain.UI.WGT_CHAT_PARENT);
    if (!Object.op_Implicit((Object) ctrl))
      return;
    UIWidget component = ((Component) ctrl).gameObject.GetComponent<UIWidget>();
    if (!Object.op_Implicit((Object) component))
      return;
    UIStaticPanelChanger staticPanelChanger = ((Component) ctrl).GetComponentInParent<UIStaticPanelChanger>();
    if (!Object.op_Implicit((Object) staticPanelChanger))
      return;
    staticPanelChanger.UnLock();
    if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.NeedModifyInGameChatOpenPosition)
    {
      DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
      if (SpecialDeviceManager.IsPortrait)
      {
        component.leftAnchor.Set(0.0f, (float) specialDeviceInfo.ChatButtonAnchorPortrait.left);
        component.rightAnchor.Set(0.0f, (float) specialDeviceInfo.ChatButtonAnchorPortrait.right);
        component.bottomAnchor.Set(0.0f, (float) specialDeviceInfo.ChatButtonAnchorPortrait.bottom);
        component.topAnchor.Set(0.0f, (float) specialDeviceInfo.ChatButtonAnchorPortrait.top);
      }
      else
      {
        component.leftAnchor.Set(0.0f, (float) specialDeviceInfo.ChatButtonAnchorLandscape.left);
        component.rightAnchor.Set(0.0f, (float) specialDeviceInfo.ChatButtonAnchorLandscape.right);
        component.bottomAnchor.Set(0.0f, (float) specialDeviceInfo.ChatButtonAnchorLandscape.bottom);
        component.topAnchor.Set(0.0f, (float) specialDeviceInfo.ChatButtonAnchorLandscape.top);
      }
    }
    else
    {
      float absolute = !isPortrait ? 10f : 323f;
      component.topAnchor.Set(0.0f, absolute + (float) component.height);
      component.bottomAnchor.Set(0.0f, absolute);
    }
    component.UpdateAnchors();
    MonoBehaviourSingleton<AppMain>.I.onDelayCall += (System.Action) (() => staticPanelChanger.Lock());
  }

  private void OnQuery_EVOLVE()
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid() || MonoBehaviourSingleton<StageObjectManager>.I.self == null || !MonoBehaviourSingleton<UIPlayerStatus>.I.IsEnableWeaponChangeButton() || !MonoBehaviourSingleton<StageObjectManager>.I.self.ExecEvolve())
      return;
    if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
      MonoBehaviourSingleton<UIPlayerStatus>.I.RestrictPopMenu(true);
    if (!MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
      return;
    MonoBehaviourSingleton<UIEnduranceStatus>.I.RestrictPopMenu(true);
  }

  private void OnQuery_EVOLVE_TOUCH()
  {
    if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
      MonoBehaviourSingleton<UIPlayerStatus>.I.RestrictPopMenu(false);
    if (!MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
      return;
    MonoBehaviourSingleton<UIEnduranceStatus>.I.RestrictPopMenu(false);
  }

  public enum UI
  {
    BTN_INGAME_MENU,
    BTN_QUEST_MENU,
    BTN_REQUEST,
    BTN_EVENT,
    BTN_CHAT,
    WGT_CHAT_PARENT,
    BTN_LOUNGE_MEMBER,
    SPR_BTN_2,
  }

  public enum AUDIO
  {
    DROP_ITEM_HEAVY = 10000061, // 0x009896BD
    DROP_ITEM_LITE = 10000062, // 0x009896BE
    GET_NORMAL_ITEM = 10000063, // 0x009896BF
    GET_RARE_ITEM = 10000064, // 0x009896C0
    GOLD_DROP = 10000065, // 0x009896C1
    OPEN_TREASURE = 10000071, // 0x009896C7
    CHANGE_WEAPON = 40000025, // 0x02625A19
    ANNOUNCE_CLEAR = 40000030, // 0x02625A1E
    PORTAL_CHARGE = 40000068, // 0x02625A44
    PORTAL_FULL_INFO = 40000069, // 0x02625A45
    PORTAL_CHARGE_FIRE = 40000070, // 0x02625A46
    ANNOUNCE_NEW_PORTAL = 40000071, // 0x02625A47
    SKILL_MAX_ATTACK = 40000120, // 0x02625A78
    GET_ITEM_NORMAL = 40000153, // 0x02625A99
    GET_ITEM_RARE = 40000154, // 0x02625A9A
    GET_ITEM_DELIVERY = 40000155, // 0x02625A9B
    BREAK_REGION = 40000156, // 0x02625A9C
    PORTAL_UNLOCK = 40000159, // 0x02625A9F
    HALF_CHARGE = 40000358, // 0x02625B66
    FULL_CHARGE = 40000359, // 0x02625B67
  }
}
