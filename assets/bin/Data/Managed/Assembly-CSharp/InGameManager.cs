// Decompiled with JetBrains decompiler
// Type: InGameManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using App.Scripts.GoGame.Optimization;
using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class InGameManager : MonoBehaviourSingleton<InGameManager>
{
  public const string defaultVariant = "en-sd";
  public static string[] voiceVariants = new string[2]
  {
    "en-sd",
    "jp-sd"
  };
  public static string[] languageVariants = new string[8]
  {
    "en-sd",
    "fr-sd",
    "ge-sd",
    "it-sd",
    "po-sd",
    "th-sd",
    "vn-sd",
    "es-sd"
  };
  public const int GRAPHIC_OPTION_LOW = 0;
  public const int GRAPHIC_OPTION_STANDARD = 1;
  public const int GRAPHIC_OPTION_HIGH = 2;
  public const int GRAPHIC_OPTION_HIGHEST = 3;
  public const string GRAPHIC_OPTION_KEY_LOW = "low";
  public const string GRAPHIC_OPTION_KEY_STANDARD = "standard";
  public const string GRAPHIC_OPTION_KEY_HIGH = "high";
  public const string GRAPHIC_OPTION_KEY_HIGHEST = "highest";
  public const int ARROW_CAMERA_OPTION_A = 0;
  public const int ARROW_CAMERA_OPTION_B = 1;
  public const string ARROW_CAMERA_OPTION_KEY_A = "typea";
  public const string ARROW_CAMERA_OPTION_KEY_B = "typeb";
  [NonSerialized]
  public int graphicOptionType;
  [NonSerialized]
  public int arrowCameraType = 1;
  public List<UIInGamePopupDialog.Desc> dialogOpenInfoList = new List<UIInGamePopupDialog.Desc>();
  protected List<FieldDropObject> dropList = new List<FieldDropObject>();
  protected List<GameObject> dropNCaches = new List<GameObject>();
  protected List<GameObject> dropHNCaches = new List<GameObject>();
  protected List<GameObject> dropRCaches = new List<GameObject>();
  protected List<GameObject> dropLoungeCaches = new List<GameObject>();
  protected List<GameObject> bossDropNCaches = new List<GameObject>();
  protected List<GameObject> bossDropRCaches = new List<GameObject>();
  protected List<GameObject> bossDropRegionBreakCaches = new List<GameObject>();
  protected List<GameObject> dropSPNCaches = new List<GameObject>();
  protected List<GameObject> dropSPHNCaches = new List<GameObject>();
  protected List<GameObject> dropSPRCaches = new List<GameObject>();
  protected List<GameObject> dropHalloweenCaches = new List<GameObject>();
  protected List<GameObject> dropESPNCaches = new List<GameObject>();
  protected List<GameObject> dropESPHNCaches = new List<GameObject>();
  protected List<GameObject> dropESPRCaches = new List<GameObject>();
  protected List<GameObject> dropSeasonalCaches = new List<GameObject>();
  public InGameManager.IntervalTransferInfo intervalTransferInfo;
  [NonSerialized]
  public DeliveryBattleChecker deliveryBattleChecker = new DeliveryBattleChecker();
  [NonSerialized]
  public bool isAlreadyBattleStarted;
  public EventData[] requestEventData;
  [NonSerialized]
  public List<uint> disableHappenQuestIdList = new List<uint>();
  public List<Coop_Model_EventHappenQuestStatus.Status> happenQuestStatusList;
  public InGameManager.QuestTransferInfo questTransferInfo;
  private int rushIndex;
  private PartyModel.RushInfo rushInfo;
  private Coop_Model_EnemyInitialize rushBossBackup;
  private int arenaIndex;
  private PartyModel.ArenaInfo arenaInfo;
  private Coop_Model_EnemyInitialize seriesBossBackup;
  [NonSerialized]
  public CoopClient.CLIENT_JOIN_TYPE currentJoinType;
  private IEnumerator updateIntervalTransferInfoRemaindTime;
  private static string[] disableEffectsGraphicLow = new string[23]
  {
    "ef_btl_pl_runsmoke_",
    "ef_ui_skillgauge_",
    "ef_btl_pl_avoidsmoke_",
    "ef_btl_pl_landingsmoke_",
    "ef_btl_bg_takingspot_",
    "ef_btl_damage_add_blood",
    "ef_btl_wyvern_downsmoke_",
    "ef_btl_pl_downsmoke_",
    "ef_btl_bg_birds_",
    "ef_btl_bg_spray_01",
    "ef_btl_bg_rain_01",
    "ef_btl_pl_attacksmoke_",
    "ef_btl_pl01_attack02smoke_",
    "ef_btl_pl02_attack03smoke_",
    "ef_btl_bg_magma_01",
    "ef_btl_bg_sparks_",
    "ef_ui_downgauge_",
    "ef_btl_dragon_downsmoke_",
    "ef_btl_dragon_walksmoke",
    "ef_btl_enemy_landingsmoke_m_",
    "ef_btl_pl_jump_01",
    "ef_btl_wyvern_walksmoke1_",
    "ef_btl_pl_impactsmoke_"
  };

  public List<FieldDropObject> dropItemList => this.dropList;

  public GameObject selfCacheObject { get; private set; }

  public bool isValidGimmickObject { get; protected set; }

  public bool IsQuestInField()
  {
    return this.isQuestHappen || this.isQuestGate || this.isQuestPortal || this.isQuestFromGimmick;
  }

  public bool IsQuestInPortal() => this.isQuestGate || this.isQuestPortal;

  public bool isQuestHappen { get; set; }

  public bool isQuestGate { get; set; }

  public bool isQuestPortal { get; set; }

  public bool isQuestFromGimmick { get; set; }

  public bool isStoryPortal { get; set; }

  public bool isGateQuestClear { get; set; }

  public bool isTransitionFieldToQuest { get; set; }

  public bool isTransitionQuestToField { get; set; }

  public bool isTransitionQuestToFieldExplore { get; set; }

  public bool isTransitionFieldReentry { get; set; }

  public bool isRetry { get; set; }

  public bool isTransitionQuestToQuest { get; set; }

  public static bool IsReentry()
  {
    return InGameManager.IsValidRush() || FieldManager.IsValidInGameNoQuest() || QuestManager.IsValidInGameExplore() || QuestManager.IsValidInGameSeries() || QuestManager.IsValidInGameWaveMatch();
  }

  public static bool IsReentryNotLeaveParty()
  {
    return InGameManager.IsValidRush() || QuestManager.IsValidInGameExplore() || QuestManager.IsValidInGameSeries() || QuestManager.IsValidInGameWaveMatch();
  }

  public static bool IsReentryMapId()
  {
    return InGameManager.IsValidRush() || QuestManager.IsValidInGameExplore() || QuestManager.IsValidInGameSeries() || QuestManager.IsValidInGameWaveMatch();
  }

  public bool isQuestResultFieldLeave { get; set; }

  public int readStoryID { get; set; }

  public uint beforePortalID { get; set; }

  public FieldManager.FieldTransitionInfo backTransitionInfo { get; set; }

  public int rushId { get; private set; }

  public bool isResultedRush { get; private set; }

  public List<QuestCompleteRewardList> rushRewards { get; private set; }

  public List<PointShopResultData> rushPointShops { get; private set; }

  public List<PointEventCurrentData> rushPointEvents { get; private set; }

  public bool isRushReentry { get; private set; }

  public List<InGameManager.RushWaveSyncData> rushWaveSyncDataList { get; private set; }

  public bool IsRush() => this.rushInfo != null;

  public static bool IsValidRush()
  {
    return MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.IsRush();
  }

  public void ClearRush()
  {
    this.rushInfo = (PartyModel.RushInfo) null;
    this.rushId = 0;
    this.rushIndex = 0;
    this.rushRewards = (List<QuestCompleteRewardList>) null;
    this.rushPointEvents = (List<PointEventCurrentData>) null;
    this.rushPointShops = (List<PointShopResultData>) null;
    this.rushWaveSyncDataList = (List<InGameManager.RushWaveSyncData>) null;
    this.isResultedRush = false;
  }

  public void SetRushInfo(int rushId, PartyModel.RushInfo rushInfo)
  {
    this.rushId = rushId;
    this.rushInfo = rushInfo;
    this.rushIndex = 0;
    this.rushRewards = new List<QuestCompleteRewardList>();
    this.rushPointShops = new List<PointShopResultData>();
    this.rushPointEvents = new List<PointEventCurrentData>();
    this.rushWaveSyncDataList = new List<InGameManager.RushWaveSyncData>();
  }

  public void SetResultedRush() => this.isResultedRush = true;

  public void ProgressRush() => ++this.rushIndex;

  public bool IsLastRash() => this.rushInfo.waves.Count - 1 <= this.rushIndex;

  public int GetRushIndex() => this.rushIndex;

  public uint GetCurrentRushQuestId() => (uint) this.rushInfo.waves[this.rushIndex].questId;

  public int GetCurrentWaveNum() => this.rushInfo.waves[this.rushIndex].wave;

  public int GetWaveNum(int index) => this.rushInfo.waves[index].wave;

  public int GetCurrentRushRescureResetNum() => this.rushInfo.waves[this.rushIndex].rescueResetNum;

  public bool CanRushPayContinue() => this.rushInfo.continueFlag > 0;

  public int GetRushQuestId(int wave)
  {
    return this.rushInfo.waves.Find((Predicate<PartyModel.RushInfo.WaveInfo>) (w => w.wave == wave)).questId;
  }

  public void AddWaveResult(
    QuestCompleteRewardList reward,
    List<PointEventCurrentData> pointEvent,
    List<PointShopResultData> pointShop)
  {
    this.rushRewards.Add(reward);
    this.rushPointEvents.AddRange((IEnumerable<PointEventCurrentData>) pointEvent);
    this.AddRushPointShop(pointShop);
  }

  private void AddRushPointShop(List<PointShopResultData> pointShop)
  {
    foreach (PointShopResultData pointShopResultData1 in pointShop)
    {
      PointShopResultData add_data = pointShopResultData1;
      PointShopResultData pointShopResultData2 = this.rushPointShops.Find((Predicate<PointShopResultData>) (list_data => list_data.pointShopId == add_data.pointShopId));
      if (pointShopResultData2 == null)
      {
        this.rushPointShops.Add(add_data);
      }
      else
      {
        pointShopResultData2.getPoint += add_data.getPoint;
        pointShopResultData2.totalPoint = add_data.totalPoint;
      }
    }
  }

  public void BackupRushStageInReentry()
  {
    this.rushBossBackup = (Coop_Model_EnemyInitialize) null;
    Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
    if (Object.op_Inequality((Object) boss, (Object) null))
      this.rushBossBackup = boss.CreateBackup();
    this.isRushReentry = true;
  }

  public void RestoreRushInReentry()
  {
    Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
    if (!Object.op_Inequality((Object) boss, (Object) null) || this.rushBossBackup == null)
      return;
    boss.enemyReceiver.Set(new CoopPacket()
    {
      model = (Coop_Model_Base) this.rushBossBackup
    });
  }

  public void ResetRushInReentry()
  {
    this.isRushReentry = false;
    this.rushBossBackup = (Coop_Model_EnemyInitialize) null;
  }

  public int GetCurrentRushStandupHpPer() => this.rushInfo.waves[this.rushIndex].standupHpPer;

  public void RecordRushWaveSyncData()
  {
    CoopStage coopStage = MonoBehaviourSingleton<CoopManager>.I.coopStage;
    this.rushWaveSyncDataList.Add(new InGameManager.RushWaveSyncData()
    {
      bossBreakIds = coopStage.bossBreakIDLists[0],
      elapsedTime = MonoBehaviourSingleton<InGameProgress>.I.GetElapsedTime()
    });
  }

  public InGameManager.RushWaveSyncData GetRushSyncData(int index)
  {
    return this.rushWaveSyncDataList.Count > index ? this.rushWaveSyncDataList[index] : (InGameManager.RushWaveSyncData) null;
  }

  public List<QuestCompleteRewardList> arenaRewards { get; private set; }

  public List<PointShopResultData> arenaPointShops { get; private set; }

  public void SetArenaInfo(int arenaId)
  {
    ArenaTable.ArenaData arenaData = Singleton<ArenaTable>.I.GetArenaData(arenaId);
    this.arenaInfo = new PartyModel.ArenaInfo();
    this.arenaInfo.arenaData = arenaData;
    for (int index = 0; index < 5; ++index)
    {
      if (arenaData.questIds[index] > 0)
        this.arenaInfo.waves.Add(new PartyModel.ArenaInfo.WaveInfo()
        {
          wave = index + 1,
          questId = arenaData.questIds[index]
        });
    }
    this.arenaIndex = 0;
    this.arenaRewards = new List<QuestCompleteRewardList>();
    this.arenaPointShops = new List<PointShopResultData>();
  }

  public void ClearArenaInfo()
  {
    this.arenaInfo = (PartyModel.ArenaInfo) null;
    this.arenaIndex = 0;
    this.arenaRewards = (List<QuestCompleteRewardList>) null;
    this.arenaPointShops = (List<PointShopResultData>) null;
  }

  public bool HasArenaInfo() => this.arenaInfo != null;

  public bool IsArenaTimeAttack()
  {
    return this.HasArenaInfo() && this.arenaInfo.arenaData.rank == ARENA_RANK.S;
  }

  public ARENA_CONDITION[] GetArenaConditions()
  {
    if (this.arenaInfo == null)
      return (ARENA_CONDITION[]) null;
    return this.arenaInfo.arenaData == null ? (ARENA_CONDITION[]) null : this.arenaInfo.arenaData.conditions;
  }

  public void ProgressArena() => ++this.arenaIndex;

  public bool IsArenaFirstWave() => this.arenaIndex <= 0;

  public bool IsArenaFinalWave() => this.arenaInfo.waves.Count - 1 <= this.arenaIndex;

  public int GetArenaWaveMax() => this.arenaInfo.waves.Count;

  public int GetCurrentArenaWaveNum() => this.arenaInfo.waves[this.arenaIndex].wave;

  public uint GetCurrentArenaQuestId() => (uint) this.arenaInfo.waves[this.arenaIndex].questId;

  public uint GetFirstArenaQuestId() => (uint) this.arenaInfo.waves[0].questId;

  public bool ContainsArenaCondition(ARENA_CONDITION condition)
  {
    if (!this.HasArenaInfo())
      return false;
    for (int index = 0; index < this.arenaInfo.arenaData.conditions.Length; ++index)
    {
      if (this.arenaInfo.arenaData.conditions[index] == condition)
        return true;
    }
    return false;
  }

  public ARENA_GROUP GetCurrentArenaGroup() => this.arenaInfo.arenaData.group;

  public ARENA_RANK GetCurrentArenaRank() => this.arenaInfo.arenaData.rank;

  public void AddArenaWaveResult(
    QuestCompleteRewardList reward,
    List<PointShopResultData> pointShop)
  {
    this.arenaRewards.Add(reward);
    this.AddArenaPointShop(pointShop);
  }

  private void AddArenaPointShop(List<PointShopResultData> pointShop)
  {
    foreach (PointShopResultData pointShopResultData1 in pointShop)
    {
      PointShopResultData addData = pointShopResultData1;
      PointShopResultData pointShopResultData2 = this.arenaPointShops.Find((Predicate<PointShopResultData>) (listData => listData.pointShopId == addData.pointShopId));
      if (pointShopResultData2 == null)
      {
        this.arenaPointShops.Add(addData);
      }
      else
      {
        pointShopResultData2.getPoint += addData.getPoint;
        pointShopResultData2.totalPoint = addData.totalPoint;
      }
    }
  }

  public bool isSeriesReentry { get; private set; }

  public void BackupSeriesStageInReentry()
  {
    this.seriesBossBackup = (Coop_Model_EnemyInitialize) null;
    Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
    if (Object.op_Inequality((Object) boss, (Object) null) && !boss.isDead)
      this.seriesBossBackup = boss.CreateBackup();
    this.isSeriesReentry = true;
  }

  public void RestoreSeriesInReentry()
  {
    Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
    if (!Object.op_Inequality((Object) boss, (Object) null) || this.seriesBossBackup == null)
      return;
    boss.enemyReceiver.Set(new CoopPacket()
    {
      model = (Coop_Model_Base) this.seriesBossBackup
    });
  }

  public void ResetSeriesInReentry()
  {
    this.isSeriesReentry = false;
    this.seriesBossBackup = (Coop_Model_EnemyInitialize) null;
  }

  public static bool IsValidInGameTest()
  {
    bool flag = false;
    return MonoBehaviourSingleton<InGameManager>.IsValid() & flag;
  }

  public static bool IsValidInGameTestField()
  {
    bool flag = false;
    return MonoBehaviourSingleton<InGameManager>.IsValid() & flag;
  }

  protected override void Awake()
  {
    base.Awake();
    this.UpdateConfig();
  }

  public static int GetGraphicOptionType(string key)
  {
    int graphicOptionType = 1;
    switch (key)
    {
      case "low":
        graphicOptionType = 0;
        break;
      case "standard":
        graphicOptionType = 1;
        break;
      case "high":
        graphicOptionType = 2;
        break;
      case "highest":
        graphicOptionType = 3;
        break;
    }
    return graphicOptionType;
  }

  public void UpdateConfig()
  {
    this.graphicOptionType = 0;
    this.arrowCameraType = 1;
    if (GameSaveData.instance == null)
      return;
    this.graphicOptionType = InGameManager.GetGraphicOptionType(GameSaveData.instance.graphicOptionKey);
    this.arrowCameraType = InGameManager.GetArrowCameraType(GameSaveData.instance.arrowCameraKey);
  }

  public static int GetArrowCameraType(string key)
  {
    int arrowCameraType = 1;
    switch (key)
    {
      case "typea":
        arrowCameraType = 0;
        break;
      case "typeb":
        arrowCameraType = 1;
        break;
    }
    return arrowCameraType;
  }

  public void OnEndInGameScene()
  {
    this.dialogOpenInfoList = new List<UIInGamePopupDialog.Desc>();
    this.intervalTransferInfo = (InGameManager.IntervalTransferInfo) null;
    this.isQuestHappen = false;
    this.isQuestGate = false;
    this.isQuestPortal = false;
    this.isQuestFromGimmick = false;
    this.isGateQuestClear = false;
    this.isTransitionFieldToQuest = false;
    this.isTransitionQuestToField = false;
    this.isTransitionQuestToFieldExplore = false;
    this.isTransitionFieldReentry = false;
    this.isStoryPortal = false;
    this.isAlreadyBattleStarted = false;
    this.beforePortalID = 0U;
    this.backTransitionInfo = (FieldManager.FieldTransitionInfo) null;
    this.currentJoinType = CoopClient.CLIENT_JOIN_TYPE.NONE;
    MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_GRAB, false);
    MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_COMMAND, false);
    this.ClearAllDrop();
    this.StopIntervalTransferInfoRemaindTimeUpdate();
  }

  public string GetCurrentStageName()
  {
    string currentStageName = "ST011D_01";
    if (MonoBehaviourSingleton<FieldManager>.IsValid())
      currentStageName = MonoBehaviourSingleton<FieldManager>.I.GetCurrentMapStageName();
    return currentStageName;
  }

  public float GetCurrentLimitTime()
  {
    float currentLimitTime = 0.0f;
    if (QuestManager.IsValidInGameExplore())
    {
      if (MonoBehaviourSingleton<QuestManager>.I.currentQuestID != 0U)
        currentLimitTime = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestLimitTime();
    }
    else if (this.IsRush())
    {
      int questId = this.rushInfo.waves[0].questId;
      currentLimitTime = Singleton<QuestTable>.I.GetQuestData((uint) questId).limitTime;
      if (this.intervalTransferInfo != null && (double) this.intervalTransferInfo.remaindTime >= 0.0)
        currentLimitTime = this.intervalTransferInfo.remaindTime;
    }
    else if (this.HasArenaInfo())
    {
      currentLimitTime = (float) this.arenaInfo.arenaData.timeLimit * (1f / 1000f);
      if (this.intervalTransferInfo != null && (double) this.intervalTransferInfo.remaindTime >= 0.0)
        currentLimitTime = this.intervalTransferInfo.remaindTime;
    }
    else if (QuestManager.IsValidInGame())
    {
      if (this.intervalTransferInfo != null && (double) this.intervalTransferInfo.remaindTime >= 0.0)
        currentLimitTime = this.intervalTransferInfo.remaindTime;
      else if (MonoBehaviourSingleton<QuestManager>.I.currentQuestID != 0U)
        currentLimitTime = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestLimitTime();
    }
    else
      currentLimitTime = 0.0f;
    return currentLimitTime;
  }

  public bool IsNeedInitBoss()
  {
    bool flag = false;
    if (QuestManager.IsValidInGame())
    {
      if (MonoBehaviourSingleton<QuestManager>.I.IsExplore())
      {
        flag = (int) MonoBehaviourSingleton<FieldManager>.I.currentMapID == (int) Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID).mapId;
        if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsExploreBossDead())
          flag = false;
      }
      else
        flag = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID).enemyID[(int) MonoBehaviourSingleton<QuestManager>.I.currentQuestSeriesIndex] > 0;
    }
    else if (FieldManager.IsValidInGame())
      flag = false;
    return flag;
  }

  public void CheckStageInitialState()
  {
    this.isValidGimmickObject = MonoBehaviourSingleton<StageObjectManager>.I.gimmickList.Count > 0;
    if (FieldManager.IsValidInGameNoBoss())
    {
      MonoBehaviourSingleton<InGameCameraManager>.I.hitObjectType = InGameCameraManager.CAM_HIT_OBJ_TYPE.NONE;
    }
    else
    {
      bool flag = false;
      Transform[] componentsInChildren = ((Component) MonoBehaviourSingleton<StageManager>.I._transform).GetComponentsInChildren<Transform>();
      int index = 0;
      for (int length = componentsInChildren.Length; index < length; ++index)
      {
        if (((Component) componentsInChildren[index]).gameObject.layer == 18 || ((Component) componentsInChildren[index]).gameObject.layer == 9 || ((Component) componentsInChildren[index]).gameObject.layer == 21)
        {
          flag = true;
          break;
        }
      }
      if (flag)
        MonoBehaviourSingleton<InGameCameraManager>.I.hitObjectType = InGameCameraManager.CAM_HIT_OBJ_TYPE.ZOOM;
      else
        MonoBehaviourSingleton<InGameCameraManager>.I.hitObjectType = InGameCameraManager.CAM_HIT_OBJ_TYPE.NONE;
    }
  }

  public IEnumerator InitializeEnemyPop()
  {
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.IsValid())
    {
      if (MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeries() || MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeriesArena())
      {
        this.StartCoroutine(this.InitializeEnemyPopForSeries());
      }
      else
      {
        List<FieldMapTable.EnemyPopTableData> enemyPopList = Singleton<FieldMapTable>.I.GetEnemyPopList(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
        if (enemyPopList != null && enemyPopList.Count > 0)
        {
          uint num1 = 0;
          int num2 = 0;
          if (QuestManager.IsValidInGame())
          {
            num1 = (uint) MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestEnemyID();
            num2 = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestEnemyLv();
          }
          if (this.HasArenaInfo())
            num2 = this.arenaInfo.arenaData.level;
          List<uint> enemy_list = new List<uint>();
          int load_count = 0;
          int index = 0;
          for (int count = enemyPopList.Count; index < count; ++index)
          {
            FieldMapTable.EnemyPopTableData enemyPopTableData = enemyPopList[index];
            if (enemyPopTableData != null)
            {
              uint enemy_id;
              int enemy_lv;
              if (enemyPopTableData.enemyID > 0U)
              {
                enemy_id = enemyPopTableData.enemyID;
                enemy_lv = (int) enemyPopTableData.enemyLv;
              }
              else if (num1 > 0U)
              {
                enemy_id = num1;
                enemy_lv = num2;
              }
              else
                continue;
              if (!enemy_list.Contains(enemy_id))
              {
                enemy_list.Add(enemyPopTableData.enemyID);
                MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemy(0, Vector3.zero, 0.0f, (int) enemy_id, enemy_lv, enemyPopTableData.bossFlag, enemyPopTableData.bigMonsterFlag, willStock: true, callback: (EnemyLoader.OnCompleteLoad) (o =>
                {
                  ((Component) o).gameObject.SetActive(false);
                  MonoBehaviourSingleton<StageObjectManager>.I.enemyStokeList.Add(o);
                  ++load_count;
                }));
              }
            }
          }
          while (load_count < enemy_list.Count)
            yield return (object) null;
        }
      }
    }
  }

  public IEnumerator InitializeEnemyPop_GG_Optimize(Action<bool> callBack, System.Action effectCallBack)
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      callBack(false);
    else if (!MonoBehaviourSingleton<FieldManager>.IsValid())
      callBack(false);
    else if (MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeries() || MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeriesArena())
    {
      this.StartCoroutine(this.InitializeEnemyPopForSeries());
      callBack(true);
    }
    else
    {
      List<FieldMapTable.EnemyPopTableData> enemyPopList = Singleton<FieldMapTable>.I.GetEnemyPopList(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
      if (enemyPopList != null && enemyPopList.Count > 0)
      {
        uint num1 = 0;
        int num2 = 0;
        if (QuestManager.IsValidInGame())
        {
          num1 = (uint) MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestEnemyID();
          num2 = MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestEnemyLv();
        }
        if (this.HasArenaInfo())
          num2 = this.arenaInfo.arenaData.level;
        List<uint> enemy_list = new List<uint>();
        int load_count = 0;
        int index = 0;
        for (int count = enemyPopList.Count; index < count; ++index)
        {
          FieldMapTable.EnemyPopTableData enemyPopTableData = enemyPopList[index];
          if (enemyPopTableData != null)
          {
            uint enemy_id;
            int enemy_lv;
            if (enemyPopTableData.enemyID > 0U)
            {
              enemy_id = enemyPopTableData.enemyID;
              enemy_lv = (int) enemyPopTableData.enemyLv;
            }
            else if (num1 > 0U)
            {
              enemy_id = num1;
              enemy_lv = num2;
            }
            else
              continue;
            if (!enemy_list.Contains(enemy_id))
            {
              enemy_list.Add(enemyPopTableData.enemyID);
              MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemy_GG_Optimize(0, Vector3.zero, 0.0f, (int) enemy_id, enemy_lv, enemyPopTableData.bossFlag, enemyPopTableData.bigMonsterFlag, willStock: true, callback: (EnemyLoader.OnCompleteLoad) (o =>
              {
                ((Component) o).gameObject.SetActive(false);
                MonoBehaviourSingleton<StageObjectManager>.I.enemyStokeList.Add(o);
                ++load_count;
              }), EffectCallBack: effectCallBack, use_later_load: enemyPopTableData.bossFlag);
            }
          }
        }
        while (load_count < enemy_list.Count)
          yield return (object) null;
        callBack(true);
      }
    }
  }

  private IEnumerator InitializeEnemyPopForSeries()
  {
    QuestManager i = MonoBehaviourSingleton<QuestManager>.I;
    int count = i.GetCurrentQuestSeriesNum();
    int loadCount = 0;
    for (int index = 0; index < count; ++index)
    {
      uint currentQuestEnemyId = (uint) i.GetCurrentQuestEnemyID(index);
      int currentQuestEnemyLv = i.GetCurrentQuestEnemyLv(index);
      if (currentQuestEnemyId > 0U && currentQuestEnemyLv > 0)
        MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemy(0, Vector3.zero, 0.0f, (int) currentQuestEnemyId, currentQuestEnemyLv, true, true, willStock: true, callback: (EnemyLoader.OnCompleteLoad) (o =>
        {
          ((Component) o).gameObject.SetActive(false);
          MonoBehaviourSingleton<StageObjectManager>.I.enemyStokeList.Add(o);
          ++loadCount;
        }));
    }
    while (loadCount < count)
      yield return (object) null;
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    loadingQueue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_enemy_entry_01");
    while (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
  }

  public IEnumerator InitializeEnemyPopForSummon(int enemyId, int enemyLv)
  {
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
    {
      bool isLoading = true;
      MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemy(0, Vector3.zero, 0.0f, enemyId, enemyLv, false, false, willStock: true, callback: (EnemyLoader.OnCompleteLoad) (o =>
      {
        ((Component) o).gameObject.SetActive(false);
        MonoBehaviourSingleton<StageObjectManager>.I.enemyStokeList.Add(o);
        isLoading = false;
      }));
      while (isLoading)
        yield return (object) null;
    }
  }

  public IEnumerator InitializeEnemyPopForSummonAttack(int enemyId, int enemyLv)
  {
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
    {
      bool isLoading = true;
      MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemyForSummonAttack(0, Vector3.zero, 0.0f, enemyId, enemyLv, true, (EnemyLoader.OnCompleteLoad) (o =>
      {
        ((Component) o).gameObject.SetActive(false);
        MonoBehaviourSingleton<StageObjectManager>.I.enemySummonStokeList.Add(o);
        isLoading = false;
      }));
      while (isLoading)
        yield return (object) null;
    }
  }

  public GameObject CreateBossDropObject(int rarity)
  {
    GameObject bossDropObject = (GameObject) null;
    switch (rarity)
    {
      case 1:
        int index1 = 0;
        for (int count = this.bossDropRCaches.Count; index1 < count; ++index1)
        {
          if (!this.bossDropRCaches[index1].activeSelf)
          {
            bossDropObject = this.bossDropRCaches[index1];
            bossDropObject.SetActive(true);
            break;
          }
        }
        if (Object.op_Equality((Object) bossDropObject, (Object) null))
        {
          Transform transform = ResourceUtility.Realizes((Object) MonoBehaviourSingleton<InGameLinkResourcesQuest>.I.bossDropR, MonoBehaviourSingleton<StageObjectManager>.I._transform);
          if (Object.op_Inequality((Object) transform, (Object) null))
          {
            bossDropObject = ((Component) transform).gameObject;
            this.bossDropRCaches.Add(bossDropObject);
            break;
          }
          break;
        }
        break;
      case 2:
        int index2 = 0;
        for (int count = this.bossDropRegionBreakCaches.Count; index2 < count; ++index2)
        {
          if (!this.bossDropRegionBreakCaches[index2].activeSelf)
          {
            bossDropObject = this.bossDropRegionBreakCaches[index2];
            bossDropObject.SetActive(true);
            break;
          }
        }
        if (Object.op_Equality((Object) bossDropObject, (Object) null))
        {
          Transform transform = ResourceUtility.Realizes((Object) MonoBehaviourSingleton<InGameLinkResourcesQuest>.I.bossDropRegionBreak, MonoBehaviourSingleton<StageObjectManager>.I._transform);
          if (Object.op_Inequality((Object) transform, (Object) null))
          {
            bossDropObject = ((Component) transform).gameObject;
            this.bossDropRegionBreakCaches.Add(bossDropObject);
            break;
          }
          break;
        }
        break;
      default:
        int index3 = 0;
        for (int count = this.bossDropNCaches.Count; index3 < count; ++index3)
        {
          if (!this.bossDropNCaches[index3].activeSelf)
          {
            bossDropObject = this.bossDropNCaches[index3];
            bossDropObject.SetActive(true);
            break;
          }
        }
        if (Object.op_Equality((Object) bossDropObject, (Object) null))
        {
          Transform transform = ResourceUtility.Realizes((Object) MonoBehaviourSingleton<InGameLinkResourcesQuest>.I.bossDropN, MonoBehaviourSingleton<StageObjectManager>.I._transform);
          if (Object.op_Inequality((Object) transform, (Object) null))
          {
            bossDropObject = ((Component) transform).gameObject;
            this.bossDropNCaches.Add(bossDropObject);
            break;
          }
          break;
        }
        break;
    }
    return bossDropObject;
  }

  public void CreateDropObject(
    Coop_Model_EnemyDefeat model,
    List<InGameManager.DropDeliveryInfo> deliveryList,
    List<InGameManager.DropItemInfo> itemList)
  {
    FieldDropObject fieldDropObject = FieldDropObject.Create(model, deliveryList, itemList);
    if (!Object.op_Inequality((Object) fieldDropObject, (Object) null))
      return;
    this.dropList.Add(fieldDropObject);
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid() || !MonoBehaviourSingleton<InGameProgress>.I.isHappenQuestDirection)
      return;
    this.OpenAllDropObject();
  }

  public void CreateDropInfoList(
    Coop_Model_EnemyDefeat model,
    out List<InGameManager.DropDeliveryInfo> deliveryList,
    out List<InGameManager.DropItemInfo> itemList)
  {
    itemList = new List<InGameManager.DropItemInfo>();
    deliveryList = new List<InGameManager.DropDeliveryInfo>();
    int index1 = 0;
    for (int count = model.dropIds.Count; index1 < count; ++index1)
      itemList.Add(new InGameManager.DropItemInfo((REWARD_TYPE) model.dropTypes[index1], (uint) model.dropItemIds[index1], model.dropNums[index1]));
    int mapId = MonoBehaviourSingleton<FieldManager>.I.GetMapId();
    Delivery[] deliveryList1 = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList(false);
    int index2 = 0;
    for (int length1 = deliveryList1.Length; index2 < length1; ++index2)
    {
      DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) deliveryList1[index2].dId);
      if (deliveryTableData != null)
      {
        int num1 = 0;
        for (int length2 = deliveryTableData.needs.Length; num1 < length2; ++num1)
        {
          uint num2 = (uint) num1;
          if (deliveryTableData.IsNeedTarget(num2, (uint) model.eid, (uint) mapId) && (model.deliver & 1 << (int) (deliveryTableData.GetRateType(num2) & DELIVERY_RATE_TYPE.RATE_1)) > 0)
          {
            int have = 0;
            int need = 0;
            MonoBehaviourSingleton<DeliveryManager>.I.GetProgressDelivery(deliveryList1[index2].dId, out have, out need, num2);
            if (have < need)
            {
              int _num = 1;
              if ((model.boostBit & 1 << (int) (deliveryTableData.GetRateType(num2) & DELIVERY_RATE_TYPE.RATE_1)) > 0)
                _num += model.boostNum;
              deliveryList.Add(new InGameManager.DropDeliveryInfo(deliveryList1[index2].dId, (int) num2, deliveryTableData.name, deliveryTableData.GetNeedItemName(num2), _num, new List<DELIVERY_CONDITION_TYPE>()
              {
                deliveryTableData.GetConditionType(),
                deliveryTableData.GetConditionType(1U),
                deliveryTableData.GetConditionType(2U),
                deliveryTableData.GetConditionType(3U),
                deliveryTableData.GetConditionType(4U)
              }));
            }
          }
        }
      }
    }
  }

  public void OpenAllDropObject()
  {
    for (int index = 0; index < this.dropList.Count; ++index)
      this.dropList[index].OpenDropObject();
  }

  public void DeleteDropObject(FieldDropObject obj) => this.dropList.Remove(obj);

  public void DeleteDropObject(int reward_id, bool is_get)
  {
    FieldDropObject fieldDropObject = this.dropList.Find((Predicate<FieldDropObject>) (o => o.rewardId == reward_id));
    if (Object.op_Inequality((Object) fieldDropObject, (Object) null))
      fieldDropObject.Delete(is_get);
    this.dropList.Remove(fieldDropObject);
  }

  public void ClearAllDrop()
  {
    this.dropList.Clear();
    this.ClearDrop(this.dropNCaches);
    this.ClearDrop(this.dropHNCaches);
    this.ClearDrop(this.dropRCaches);
    this.ClearDrop(this.dropLoungeCaches);
    this.ClearDrop(this.bossDropNCaches);
    this.ClearDrop(this.bossDropRCaches);
    this.ClearDrop(this.bossDropRegionBreakCaches);
    this.ClearDrop(this.dropSPNCaches);
    this.ClearDrop(this.dropSPHNCaches);
    this.ClearDrop(this.dropSPRCaches);
    this.ClearDrop(this.dropHalloweenCaches);
    this.ClearDrop(this.dropESPNCaches);
    this.ClearDrop(this.dropESPHNCaches);
    this.ClearDrop(this.dropESPRCaches);
    this.ClearDrop(this.dropSeasonalCaches);
  }

  public void ClearDrop(List<GameObject> dropCaches)
  {
    int index = 0;
    for (int count = dropCaches.Count; index < count; ++index)
      Object.Destroy((Object) dropCaches[index]);
    dropCaches.Clear();
  }

  public GameObject CreateTreasureBox(UIDropAnnounce.COLOR color)
  {
    switch (color)
    {
      case UIDropAnnounce.COLOR.NORMAL:
        return this.RealizeTreasureBox(this.dropNCaches, MonoBehaviourSingleton<InGameLinkResourcesField>.I.dropItemBoxN);
      case UIDropAnnounce.COLOR.RARE:
        return this.RealizeTreasureBox(this.dropRCaches, MonoBehaviourSingleton<InGameLinkResourcesField>.I.dropItemBoxR);
      case UIDropAnnounce.COLOR.DELIVERY:
        return this.RealizeTreasureBox(this.dropHNCaches, MonoBehaviourSingleton<InGameLinkResourcesField>.I.dropItemBoxHN);
      case UIDropAnnounce.COLOR.LOUNGE:
        return this.RealizeTreasureBox(this.dropLoungeCaches, MonoBehaviourSingleton<InGameLinkResourcesField>.I.dropItemLoungeShare);
      case UIDropAnnounce.COLOR.SP_N:
        return this.RealizeTreasureBox(this.dropSPNCaches, MonoBehaviourSingleton<InGameLinkResourcesField>.I.dropItemBoxSPN);
      case UIDropAnnounce.COLOR.SP_HN:
        return this.RealizeTreasureBox(this.dropSPHNCaches, MonoBehaviourSingleton<InGameLinkResourcesField>.I.dropItemBoxSPHN);
      case UIDropAnnounce.COLOR.SP_R:
        return this.RealizeTreasureBox(this.dropSPRCaches, MonoBehaviourSingleton<InGameLinkResourcesField>.I.dropItemBoxSPR);
      case UIDropAnnounce.COLOR.HALLOWEEN:
        return this.RealizeTreasureBox(this.dropHalloweenCaches, MonoBehaviourSingleton<InGameLinkResourcesField>.I.dropItemHalloween);
      case UIDropAnnounce.COLOR.ESP_N:
        return this.RealizeTreasureBox(this.dropESPNCaches, MonoBehaviourSingleton<InGameLinkResourcesField>.I.dropItemBoxESPN);
      case UIDropAnnounce.COLOR.ESP_HN:
        return this.RealizeTreasureBox(this.dropESPHNCaches, MonoBehaviourSingleton<InGameLinkResourcesField>.I.dropItemBoxESPHN);
      case UIDropAnnounce.COLOR.ESP_R:
        return this.RealizeTreasureBox(this.dropESPRCaches, MonoBehaviourSingleton<InGameLinkResourcesField>.I.dropItemBoxESPR);
      case UIDropAnnounce.COLOR.SEASONAL:
        return this.RealizeTreasureBox(this.dropSeasonalCaches, MonoBehaviourSingleton<InGameLinkResourcesField>.I.dropItemSeasonal);
      default:
        return (GameObject) null;
    }
  }

  private GameObject RealizeTreasureBox(List<GameObject> caches, GameObject prefab)
  {
    int index = 0;
    for (int count = caches.Count; index < count; ++index)
    {
      if (Object.op_Inequality((Object) caches[index], (Object) null) && !caches[index].activeSelf)
      {
        GameObject cach = caches[index];
        cach.SetActive(true);
        return cach;
      }
    }
    Transform transform = ResourceUtility.Realizes((Object) prefab, MonoBehaviourSingleton<StageObjectManager>.I._transform);
    if (!Object.op_Inequality((Object) transform, (Object) null))
      return (GameObject) null;
    GameObject gameObject = ((Component) transform).gameObject;
    caches.Add(gameObject);
    return gameObject;
  }

  public List<UIDropAnnounce.DropAnnounceInfo> CreateDropAnnounceInfoList(
    List<InGameManager.DropDeliveryInfo> deliveryInfo,
    List<InGameManager.DropItemInfo> itemInfo,
    bool isTreasureBox)
  {
    List<UIDropAnnounce.DropAnnounceInfo> announceInfoList = new List<UIDropAnnounce.DropAnnounceInfo>();
    int index1 = 0;
    for (int count = deliveryInfo.Count; index1 < count; ++index1)
    {
      bool flag = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery(deliveryInfo[index1].id);
      MonoBehaviourSingleton<DeliveryManager>.I.ProgressDelivery(deliveryInfo[index1].id, deliveryInfo[index1].index, deliveryInfo[index1].num);
      int have = 0;
      int need = 0;
      MonoBehaviourSingleton<DeliveryManager>.I.GetProgressDelivery(deliveryInfo[index1].id, out have, out need, (uint) deliveryInfo[index1].index);
      announceInfoList.Add(new UIDropAnnounce.DropAnnounceInfo()
      {
        text = StringTable.Format(STRING_CATEGORY.IN_GAME, 2001U, (object) deliveryInfo[index1].itemName, (object) deliveryInfo[index1].num, (object) have, (object) need),
        color = UIDropAnnounce.COLOR.DELIVERY
      });
      if (have >= need)
      {
        GameSection currentSection = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection();
        if (Object.op_Inequality((Object) currentSection, (Object) null))
        {
          InGameMain component = ((Component) currentSection).GetComponent<InGameMain>();
          if (Object.op_Inequality((Object) component, (Object) null))
            component.OnNoticeCompletedDelivery();
        }
        if (flag != MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery(deliveryInfo[index1].id) && MonoBehaviourSingleton<UIAnnounceBand>.IsValid())
        {
          string conditionTitle = !DeliveryManager.IsDeliveryBingo((uint) deliveryInfo[index1].id) ? StringTable.Get(STRING_CATEGORY.DELIVERY_COMPLETE, 0U) : StringTable.Get(STRING_CATEGORY.DELIVERY_COMPLETE, 2U);
          MonoBehaviourSingleton<UIAnnounceBand>.I.SetAnnounce(deliveryInfo[index1].name, conditionTitle);
          SoundManager.PlayOneshotJingle(40000030);
          MonoBehaviourSingleton<CoopManager>.I.coopStage.fieldRewardPool.SendFieldDrop();
        }
        if (MonoBehaviourSingleton<DropTargetMarkerManeger>.IsValid())
          MonoBehaviourSingleton<DropTargetMarkerManeger>.I.UpdateList();
      }
    }
    if (MonoBehaviourSingleton<InventoryManager>.IsValid() & isTreasureBox)
    {
      int index2 = 0;
      for (int count = itemInfo.Count; index2 < count; ++index2)
      {
        MonoBehaviourSingleton<InventoryManager>.I.AddInGameTempItem(itemInfo[index2].id, itemInfo[index2].num);
        bool is_rare = false;
        announceInfoList.Add(itemInfo[index2].CreateAnnounceInfo(out is_rare));
        if (is_rare)
          MonoBehaviourSingleton<StageObjectManager>.I.self.OnGetRareDrop(itemInfo[index2].type, (int) itemInfo[index2].id);
      }
    }
    return announceInfoList;
  }

  public void SetIntervalTransferInfo(
    bool enable_limit_time,
    float remaind_time,
    float elapsed_time,
    bool transfer_other,
    bool keep_dead,
    bool isReentry,
    bool isQuestToField)
  {
    this.intervalTransferInfo = new InGameManager.IntervalTransferInfo();
    this.intervalTransferInfo.remaindTime = !enable_limit_time ? -1f : remaind_time;
    this.intervalTransferInfo.elapsedTime = elapsed_time;
    int index = 0;
    for (int count = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count; index < count; ++index)
    {
      Player player = MonoBehaviourSingleton<StageObjectManager>.I.playerList[index] as Player;
      if (!Object.op_Equality((Object) player, (Object) null) && (transfer_other || player is Self))
      {
        if (!keep_dead && player.hp <= 0)
          player.hp = 1;
        if (isQuestToField)
          player.hp = player.hpMax;
        if (MonoBehaviourSingleton<InGameManager>.I.IsRush() && !keep_dead)
        {
          int rushStandupHpPer = MonoBehaviourSingleton<InGameManager>.I.GetCurrentRushStandupHpPer();
          int num = (int) ((double) player.hpMax * ((double) rushStandupHpPer / 100.0));
          player.hp = Mathf.Max(player.hp, num);
        }
        InGameManager.IntervalTransferInfo.PlayerInfo playerInfo = new InGameManager.IntervalTransferInfo.PlayerInfo();
        playerInfo.id = player.id;
        playerInfo.createInfo = player.createInfo;
        playerInfo.transferInfo = player.CreateTransferInfo();
        if (MonoBehaviourSingleton<InGameManager>.I.IsRush() && !isReentry)
        {
          int rushRescureResetNum = MonoBehaviourSingleton<InGameManager>.I.GetCurrentRushRescureResetNum();
          if (rushRescureResetNum > 0)
            playerInfo.transferInfo.rescueCount = Mathf.Max(0, playerInfo.transferInfo.rescueCount - rushRescureResetNum);
        }
        playerInfo.isSelf = player is Self;
        playerInfo.coopMode = player.coopMode;
        playerInfo.coopClientId = player.coopClientId;
        playerInfo.isNpcController = player.controller is NpcController;
        playerInfo.isCoopPlayer = false;
        if (player is Self)
        {
          Self self = player as Self;
          if (Object.op_Inequality((Object) self, (Object) null))
            playerInfo.taskChecker = self.taskChecker;
        }
        if (player.coopClientId != 0 && MonoBehaviourSingleton<CoopManager>.IsValid())
        {
          CoopClient byPlayerId = MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.FindByPlayerId(player.id);
          if (Object.op_Inequality((Object) byPlayerId, (Object) null))
          {
            playerInfo.coopClientId = byPlayerId.clientId;
            playerInfo.isCoopPlayer = true;
          }
        }
        this.intervalTransferInfo.playerInfoList.Add(playerInfo);
      }
    }
  }

  public void SetIntervalTransferSelf()
  {
    if (Object.op_Inequality((Object) this.selfCacheObject, (Object) null))
      return;
    this.selfCacheObject = new GameObject();
    ((Object) this.selfCacheObject).name = "SelfCacheObject";
    this.selfCacheObject.transform.parent = MonoBehaviourSingleton<AppMain>.I._transform;
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    self.OnCached();
    GameObject gameObject = ((Component) self).gameObject;
    gameObject.transform.parent = this.selfCacheObject.transform;
    ((Object) gameObject.gameObject).name = "SelfCache";
    gameObject.gameObject.SetActive(false);
  }

  public void DestroySelfCache()
  {
    if (Object.op_Equality((Object) this.selfCacheObject, (Object) null))
      return;
    MonoBehaviourSingleton<GoGameCacheManager>.I.CacheSelfPlayerModel();
    Object.Destroy((Object) this.selfCacheObject);
    this.selfCacheObject = (GameObject) null;
  }

  public void SetEnableIntervalTransferInfoRemaindTimeUpdate()
  {
    if (this.updateIntervalTransferInfoRemaindTime != null)
      return;
    this.updateIntervalTransferInfoRemaindTime = this.UpdateIntervalTransferInfoRemaindTime();
    this.StartCoroutine(this.updateIntervalTransferInfoRemaindTime);
  }

  public void StopIntervalTransferInfoRemaindTimeUpdate()
  {
    if (this.updateIntervalTransferInfoRemaindTime == null)
      return;
    this.StopCoroutine(this.updateIntervalTransferInfoRemaindTime);
    this.updateIntervalTransferInfoRemaindTime = (IEnumerator) null;
  }

  private IEnumerator UpdateIntervalTransferInfoRemaindTime()
  {
    float startTime = Time.realtimeSinceStartup;
    float startRemaindTime = this.intervalTransferInfo.remaindTime;
    while (this.intervalTransferInfo != null)
    {
      this.intervalTransferInfo.remaindTime = startRemaindTime - (Time.realtimeSinceStartup - startTime);
      yield return (object) null;
    }
  }

  public void SaveQuestTransferInfo()
  {
    if (this.questTransferInfo == null)
      this.questTransferInfo = new InGameManager.QuestTransferInfo();
    this.questTransferInfo.intervalTransferInfo = this.intervalTransferInfo;
    this.questTransferInfo.isQuestHappen = this.isQuestHappen;
    this.questTransferInfo.isQuestGate = this.isQuestGate;
    this.questTransferInfo.isQuestPortal = this.isQuestPortal;
    this.questTransferInfo.isQuestFromGimmick = this.isQuestFromGimmick;
    this.questTransferInfo.isGateQuestClear = this.isGateQuestClear;
    this.questTransferInfo.isTransitionFieldToQuest = this.isTransitionFieldToQuest;
    this.questTransferInfo.isTransitionQuestToField = this.isTransitionQuestToField;
    this.questTransferInfo.isTransitionFieldReentry = this.isTransitionFieldReentry;
    this.questTransferInfo.isStoryPortal = this.isStoryPortal;
    this.questTransferInfo.readStoryID = this.readStoryID;
    this.questTransferInfo.beforePortalID = this.beforePortalID;
    this.questTransferInfo.backTransitionInfo = this.backTransitionInfo;
  }

  public void ResumeQuestTransferInfo()
  {
    if (this.questTransferInfo == null)
      return;
    this.intervalTransferInfo = this.questTransferInfo.intervalTransferInfo;
    this.isQuestHappen = this.questTransferInfo.isQuestHappen;
    this.isQuestGate = this.questTransferInfo.isQuestGate;
    this.isQuestPortal = this.questTransferInfo.isQuestPortal;
    this.isQuestFromGimmick = this.questTransferInfo.isQuestFromGimmick;
    this.isGateQuestClear = this.questTransferInfo.isGateQuestClear;
    this.isTransitionFieldToQuest = this.questTransferInfo.isTransitionFieldToQuest;
    this.isTransitionQuestToField = this.questTransferInfo.isTransitionQuestToField;
    this.isTransitionFieldReentry = this.questTransferInfo.isTransitionFieldReentry;
    this.isStoryPortal = this.questTransferInfo.isStoryPortal;
    this.readStoryID = this.questTransferInfo.readStoryID;
    this.beforePortalID = this.questTransferInfo.beforePortalID;
    this.backTransitionInfo = this.questTransferInfo.backTransitionInfo;
    this.questTransferInfo = (InGameManager.QuestTransferInfo) null;
  }

  public bool IsDisableEffectGraphicLow(string effectName)
  {
    if (effectName.Length == 0 || this.graphicOptionType > 0)
      return false;
    for (int index = 0; index < InGameManager.disableEffectsGraphicLow.Length; ++index)
    {
      if (effectName.StartsWith(InGameManager.disableEffectsGraphicLow[index]))
        return true;
    }
    return false;
  }

  public enum VoiceOption
  {
    ENGLISH,
    JAPANESE,
    MUTE,
  }

  public enum LanguageOption
  {
    ENGLISH,
    FRENCH,
    GERMAN,
    ITALIAN,
    PORTUGUESE,
    THAI,
    VIETNAM,
    SPANISH,
  }

  public class IntervalTransferInfo
  {
    public float remaindTime = -1f;
    public float elapsedTime = -1f;
    public List<InGameManager.IntervalTransferInfo.PlayerInfo> playerInfoList = new List<InGameManager.IntervalTransferInfo.PlayerInfo>();

    public class PlayerInfo
    {
      public int id;
      public StageObjectManager.CreatePlayerInfo createInfo;
      public StageObjectManager.PlayerTransferInfo transferInfo;
      public bool isSelf;
      public StageObject.COOP_MODE_TYPE coopMode;
      public int coopClientId;
      public bool isNpcController;
      public bool isCoopPlayer;
      public TaskChecker taskChecker = new TaskChecker();
    }
  }

  public class DropItemInfo
  {
    public REWARD_TYPE type;
    public uint id;
    public int num;

    public DropItemInfo(REWARD_TYPE _type, uint _id, int _num)
    {
      this.type = _type;
      this.id = _id;
      this.num = _num;
    }

    public UIDropAnnounce.DropAnnounceInfo CreateAnnounceInfo(out bool is_rare)
    {
      is_rare = false;
      UIDropAnnounce.DropAnnounceInfo announceInfo;
      switch (this.type)
      {
        case REWARD_TYPE.EQUIP_ITEM:
          announceInfo = UIDropAnnounce.DropAnnounceInfo.CreateEquipItemInfo(this.id, this.num, out is_rare);
          break;
        case REWARD_TYPE.SKILL_ITEM:
          announceInfo = UIDropAnnounce.DropAnnounceInfo.CreateSkillItemInfo(this.id, this.num, out is_rare);
          break;
        case REWARD_TYPE.ACCESSORY:
          announceInfo = UIDropAnnounce.DropAnnounceInfo.CreateAccessoryItemInfo(this.id, this.num, out is_rare);
          break;
        default:
          announceInfo = UIDropAnnounce.DropAnnounceInfo.CreateItemInfo(this.id, this.num, out is_rare);
          break;
      }
      return announceInfo;
    }
  }

  public class DropDeliveryInfo
  {
    public int id;
    public int index;
    public string name;
    public string itemName;
    public int num;
    public List<DELIVERY_CONDITION_TYPE> conditionTypes;
    private bool? isCountUpAtKillFieldEnemy;

    public DropDeliveryInfo(
      int delivery_id,
      int delivery_index,
      string _name,
      string item_name,
      int _num,
      List<DELIVERY_CONDITION_TYPE> condition_type)
    {
      this.id = delivery_id;
      this.index = delivery_index;
      this.name = _name;
      this.itemName = item_name;
      this.num = _num;
      this.conditionTypes = condition_type;
    }

    public bool IsCountUpAtDefeatFieldEnemy()
    {
      if (this.isCountUpAtKillFieldEnemy.HasValue)
        return this.isCountUpAtKillFieldEnemy.Value;
      int index = 0;
      for (int count = this.conditionTypes.Count; index < count; ++index)
      {
        if (MonoBehaviourSingleton<DeliveryManager>.I.IsDefeatFieldConditionType(this.conditionTypes[index]))
        {
          this.isCountUpAtKillFieldEnemy = new bool?(true);
          return true;
        }
      }
      this.isCountUpAtKillFieldEnemy = new bool?(false);
      return false;
    }
  }

  public class QuestTransferInfo
  {
    public InGameManager.IntervalTransferInfo intervalTransferInfo;
    public bool isQuestHappen;
    public bool isQuestGate;
    public bool isQuestPortal;
    public bool isQuestFromGimmick;
    public bool isGateQuestClear;
    public bool isTransitionFieldToQuest;
    public bool isTransitionQuestToField;
    public bool isTransitionFieldReentry;
    public bool isStoryPortal;
    public int readStoryID;
    public uint beforePortalID;
    public FieldManager.FieldTransitionInfo backTransitionInfo;
  }

  public class RushWaveSyncData
  {
    public float elapsedTime;
    public List<int> bossBreakIds;
  }
}
