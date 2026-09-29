// Decompiled with JetBrains decompiler
// Type: InGameProgress
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class InGameProgress : MonoBehaviourSingleton<InGameProgress>
{
  public const string HAPPEN_QUEST_HIDE_OBJECT_NAME = "HideObjects";
  private Transform viewFx;
  private bool isRecvQuestComplete;
  private bool isRecvRushProgress;
  private bool isRecvArenaProgress;
  public List<MissionCheckBase> missionCheck = new List<MissionCheckBase>();
  private bool isEndFieldBuffAnnounce;
  protected bool _isGameProgressStop;
  protected float startTime = -1f;
  protected float stopTime = -1f;
  private float elapsedTime;
  private float rushRemainTime;
  public List<QuestRushProgressData.RushTimeBonus> rushTimeBonus;
  private float bossMoveRemainTime;
  private float exploreHostDCRemainTime;
  private bool requestedExploreAlive;
  private XorFloat arenaRemainSec;
  private XorFloat arenaElapsedSec;
  public List<QuestArenaProgressData.ArenaTimeBonus> arenaTimeBonus;
  private const float TIME_AFK_LIMIT = 480f;
  private float afkTime;
  private bool enableAfkTime;
  protected float startVictoryIntervalTime = -1f;
  protected float npcCheckTimeCount;
  [NonSerialized]
  public PortalObject checkPortalObject;
  private InGameProgress.GimmickSearchInfo[] gimmickSearchInfo;
  protected int carriableGimmickDeploiedCount;
  private WaveMatchDropResource wmDropResource;
  protected uint toFieldPortalID;
  protected uint toQuestID;
  protected uint toQuestPortalID;
  protected bool toQuestGate;
  protected bool toQuestFromGimmick;
  protected int fieldReadStoryId;
  protected bool isFieldReadStorySend;
  protected EventData[] fieldReadStoryRequestEvent;
  protected bool isDecidedHappenQuestDialog;
  protected bool isYesHappenQuestDialog;
  protected bool _endHappenQuestDirection;
  private bool forceComplete;
  protected float waveMatchHostRetireDelay = 1f;
  private Coroutine waitNetworkCoroutine;
  private bool isRewardToPortalRelease;

  public bool isBattleStart { get; protected set; }

  public InGameProgress.PROGRESS_END_TYPE progressEndType { get; protected set; }

  public bool isEnding => this.progressEndType != 0;

  public bool isGameProgressStop
  {
    get => this._isGameProgressStop;
    protected set
    {
      this._isGameProgressStop = value;
      if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
        return;
      List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
      int index = 0;
      for (int count = playerList.Count; index < count; ++index)
      {
        Player player = playerList[index] as Player;
        if (player.IsOriginal() || player.IsCoopNone())
          player.StopCounter(value);
      }
    }
  }

  public bool disableSendProgressStop { get; protected set; }

  public bool isWaitContinueProtocol { get; set; }

  public float limitTime { get; protected set; }

  public bool enableLimitTime { get; protected set; }

  public bool isInitStartTime { get; protected set; }

  public float remaindTime
  {
    get
    {
      float remaindTime = this.limitTime - this.GetElapsedTime();
      if ((double) remaindTime < 0.0)
        remaindTime = 0.0f;
      return remaindTime;
    }
  }

  private float remainedAfkTime
  {
    get
    {
      float remainedAfkTime = this.afkTime;
      if ((double) remainedAfkTime < 0.0)
        remainedAfkTime = 0.0f;
      return remainedAfkTime;
    }
  }

  public int defeatCount { get; protected set; }

  public int defeatBossCount { get; protected set; }

  public int partyDefeatBossCount { get; protected set; }

  public int partyDefeatCount { get; protected set; }

  public bool enableVictoryIntervalTime { get; protected set; }

  public float victoryIntervalTime
  {
    get
    {
      float victoryIntervalTime = MonoBehaviourSingleton<InGameSettingsManager>.I.inGameProgress.victoryIntervalTime;
      if ((double) this.startVictoryIntervalTime >= 0.0)
        victoryIntervalTime -= Time.time - this.startVictoryIntervalTime;
      if ((double) victoryIntervalTime < 0.0)
        victoryIntervalTime = 0.0f;
      return victoryIntervalTime;
    }
  }

  public int waveMatchWave { get; protected set; }

  public List<PortalObject> portalObjectList { get; protected set; }

  public List<GatherPointObject> gatherPointList { get; protected set; }

  public List<IFieldGimmickObject>[] fieldGimmickList { get; protected set; }

  public UIntKeyTable<LoadObject> gatherPointModelTable { get; protected set; }

  public StringKeyTable<LoadObject> gatherPointToolTable { get; protected set; }

  public LoadObject fieldHealingPointModel { get; protected set; }

  public UIntKeyTable<LoadObject> fieldGimmickModelTable { get; protected set; }

  protected bool isQuestHappen
  {
    get => this.toQuestID > 0U && this.toQuestPortalID == 0U && !this.toQuestFromGimmick;
  }

  protected bool isQuestGate
  {
    get => this.toQuestID > 0U && this.toQuestPortalID > 0U && this.toQuestGate;
  }

  protected bool isQuestPortal
  {
    get => this.toQuestID > 0U && this.toQuestPortalID > 0U && !this.toQuestGate;
  }

  protected bool isQuestFromGimmick
  {
    get => this.toQuestID > 0U && this.toQuestPortalID == 0U && this.toQuestFromGimmick;
  }

  public bool isSendCompleteError { get; protected set; }

  public bool isHappenQuestDirection { get; protected set; }

  public bool endHappenQuestDirection
  {
    get => this._endHappenQuestDirection;
    protected set => this._endHappenQuestDirection = value;
  }

  public float defenseBattleEndurance { get; private set; }

  public float defenseBattleEnduranceMax { get; private set; }

  public void SetDefenseBattleEndurance(float endurance) => this.defenseBattleEndurance = endurance;

  public void SetDefenseBattleEnduranceMax(float endurance)
  {
    this.defenseBattleEnduranceMax = endurance;
  }

  public void DamageToEndurance(int damage)
  {
    if (damage < 0 || !MonoBehaviourSingleton<CoopManager>.I.coopRoom.isOfflinePlay && !MonoBehaviourSingleton<CoopManager>.I.isStageHost)
      return;
    this.defenseBattleEndurance -= (float) damage;
    MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendSyncDefenseBattle(this.defenseBattleEndurance);
  }

  public InGameProgress()
  {
    this.isBattleStart = false;
    this.progressEndType = InGameProgress.PROGRESS_END_TYPE.NONE;
    this.isGameProgressStop = false;
    this.isHappenQuestDirection = false;
  }

  protected override void Awake()
  {
    base.Awake();
    if (QuestManager.IsValidInGameExplore())
    {
      List<MissionCheckBase> missions = MonoBehaviourSingleton<QuestManager>.I.GetExploreStatus().GetMissions();
      if (missions != null)
        this.missionCheck = missions;
      this.bossMoveRemainTime = MonoBehaviourSingleton<QuestManager>.I.GetExploreBossMoveRemainTime();
      this.exploreHostDCRemainTime = MonoBehaviourSingleton<QuestManager>.I.GetExploreHostDCRemainTime();
    }
    this.waveMatchHostRetireDelay = !MonoBehaviourSingleton<InGameSettingsManager>.IsValid() ? 1f : MonoBehaviourSingleton<InGameSettingsManager>.I.GetWaveMatchParam().hostRetireDelay;
    this._InitGimmickList();
  }

  private void Update()
  {
    if (!this.isBattleStart || this.progressEndType != InGameProgress.PROGRESS_END_TYPE.NONE)
      return;
    if (InGameManager.IsReentry() && MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.coopMyClient.isLeave)
      MonoBehaviourSingleton<InGameProgress>.I.FieldReentry();
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (QuestManager.IsValidInGame())
    {
      if (!this.IsValidEnemy())
      {
        this.BattleComplete();
        return;
      }
      if ((double) this.remaindTime <= 0.0 && (MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.isStageHost || !MonoBehaviourSingleton<CoopManager>.IsValid()))
      {
        this.BattleTimeup();
        return;
      }
      if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsDefenseBattle() && (double) this.defenseBattleEndurance <= 0.0)
      {
        this.BattleRetire();
        return;
      }
      if (QuestManager.IsValidInGameWaveMatch() && (!MonoBehaviourSingleton<StageObjectManager>.IsValid() || MonoBehaviourSingleton<StageObjectManager>.I.IsWaveMatchTargetAllDead()))
      {
        if (!this.BattleRetire())
          return;
        UIInGamePopupDialog.PushOpen(StringTable.Get(STRING_CATEGORY.IN_GAME, 112U /*0x70*/), true);
        return;
      }
      if (MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.coopRoom.forceRetire)
      {
        if (!this.BattleRetire())
          return;
        if (MonoBehaviourSingleton<CoopManager>.I.coopRoom.ownerRetire)
        {
          UIInGamePopupDialog.PushOpen(StringTable.Get(STRING_CATEGORY.IN_GAME, 110U), true);
          return;
        }
        UIInGamePopupDialog.PushOpen(StringTable.Get(STRING_CATEGORY.IN_GAME, 111U), true);
        return;
      }
      this.npcCheckTimeCount -= Time.deltaTime;
      if ((double) this.npcCheckTimeCount <= 0.0)
      {
        if (MonoBehaviourSingleton<CoopManager>.IsValid())
        {
          if (QuestManager.IsValidInGameDefenseBattle())
            CoopStageObjectUtility.DestroyAllNonPlayer();
          else
            CoopStageObjectUtility.ShrinkOriginalNonPlayer(4);
        }
        this.npcCheckTimeCount = MonoBehaviourSingleton<InGameSettingsManager>.I.inGameProgress.npcCheckIntervalTime;
      }
    }
    else if (FieldManager.IsValidInGameNoQuest())
    {
      if (MonoBehaviourSingleton<InputManager>.IsValid() && MonoBehaviourSingleton<InputManager>.I.IsTouchIgnoreHit())
        this.ResetAfkTimer();
      if (TutorialStep.IsTheTutorialOver(TUTORIAL_STEP.USER_CREATE_02) && !self.isAutoMode)
        this.ProgressAfkTimer();
      if ((double) this.remainedAfkTime <= 0.0 && MonoBehaviourSingleton<InGameProgress>.IsValid())
        this.FieldToHomeTimeout();
      if (MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.isStageHost && MonoBehaviourSingleton<CoopManager>.I.coopStage.HasFieldEnemyBossLimitTime() && (double) this.remaindTime <= 0.0)
      {
        this.OnHostEnmeyBossTimeUp();
        return;
      }
    }
    if (QuestManager.IsValidInGameExplore() && !MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap() && MonoBehaviourSingleton<CoopManager>.I.coopMyClient.isPartyOwner)
    {
      this.ProgressExploreBossMoveTimer();
      if ((double) this.bossMoveRemainTime <= 0.0)
      {
        int bossMapId1 = (int) MonoBehaviourSingleton<QuestManager>.I.GetBossMapId();
        MonoBehaviourSingleton<QuestManager>.I.UpdateBossAppearMap();
        int bossMapId2 = (int) MonoBehaviourSingleton<QuestManager>.I.GetBossMapId();
        if (bossMapId1 != bossMapId2)
          MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendSyncExploreBossMap(MonoBehaviourSingleton<QuestManager>.I.GetExploreBossAppearMapId());
        this.SetExploreBossMoveTimer();
      }
    }
    if (QuestManager.IsValidInGameExplore())
    {
      if (MonoBehaviourSingleton<CoopManager>.I.coopMyClient.isPartyOwner)
        this.ResetExploreHostDCTimer();
      else
        this.ProgressExploreHostDCTimer();
      if ((double) this.exploreHostDCRemainTime <= 0.0)
      {
        if (!this.requestedExploreAlive)
        {
          this.SetExploreHostDCTimer(5f, true);
          if (MonoBehaviourSingleton<CoopManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<CoopManager>.I.coopRoom, (Object) null) && Object.op_Inequality((Object) MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender, (Object) null))
            MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendExploreAliveRequest();
        }
        else
        {
          if (!this.BattleRetire())
            return;
          UIInGamePopupDialog.PushOpen(StringTable.Get(STRING_CATEGORY.IN_GAME, 111U), true, 2f);
          return;
        }
      }
      if (MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.coopRoom.ownerRetire && !MonoBehaviourSingleton<CoopManager>.I.coopMyClient.isPartyOwner)
      {
        if (!this.BattleRetire())
          return;
        UIInGamePopupDialog.PushOpen(StringTable.Get(STRING_CATEGORY.IN_GAME, 110U), true);
        return;
      }
    }
    float distance = float.MaxValue;
    GatherPointObject nearestPoint = (GatherPointObject) null;
    float nearestDistance = float.MaxValue;
    Enemy nearestEnemy = (Enemy) null;
    if (this.gatherPointList != null)
      this.GetNearestGatherPoint(out distance, out nearestPoint);
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
      this.GetNearestCamouflagingEnemy(out nearestDistance, out nearestEnemy);
    this._ClearGimmickSearchInfo();
    int typeIndex = 0;
    for (int index = 11; typeIndex < index; ++typeIndex)
    {
      if (typeIndex != 0 || self.cannonState == Player.CANNON_STATE.NONE)
        this._SearchNearestGimmickPoint(typeIndex);
    }
    float num;
    if ((double) distance <= (double) nearestDistance)
    {
      self.nearGatherPoint = nearestPoint;
      nearestEnemy = (Enemy) null;
      num = distance;
    }
    else
    {
      self.nearGatherPoint = (GatherPointObject) null;
      nearestPoint = (GatherPointObject) null;
      num = nearestDistance;
    }
    self.nearFieldGimmick = (IFieldGimmickObject) null;
    int index1 = 0;
    int length;
    for (length = this.gimmickSearchInfo.Length; index1 < length; ++index1)
    {
      InGameProgress.GimmickSearchInfo gimmickSearchInfo = this.gimmickSearchInfo[index1];
      if (gimmickSearchInfo.obj != null && (double) gimmickSearchInfo.dist <= (double) num)
      {
        num = gimmickSearchInfo.dist;
        self.nearFieldGimmick = gimmickSearchInfo.obj;
        self.nearGatherPoint = (GatherPointObject) null;
        nearestPoint = (GatherPointObject) null;
        nearestEnemy = (Enemy) null;
      }
    }
    if (self.nearFieldGimmick == null)
    {
      for (int index2 = 0; index2 < length; ++index2)
        this.gimmickSearchInfo[index2].obj = (IFieldGimmickObject) null;
    }
    if (this.gatherPointList != null)
    {
      int index3 = 0;
      for (int count = this.gatherPointList.Count; index3 < count; ++index3)
        this.gatherPointList[index3].UpdateTargetMarker(Object.op_Equality((Object) this.gatherPointList[index3], (Object) nearestPoint));
    }
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
    {
      List<Enemy> enemyList = MonoBehaviourSingleton<StageObjectManager>.I.EnemyList;
      for (int index4 = 0; index4 < enemyList.Count; ++index4)
      {
        Enemy enemy = enemyList[index4];
        if (enemy.isHiding)
        {
          bool isNear = Object.op_Equality((Object) enemy, (Object) nearestEnemy);
          enemy.UpdateGatherTargetMarker(isNear);
        }
      }
    }
    this._UpdateGimmickTargetMarker();
    if (this.isEndFieldBuffAnnounce || !MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid() || !((Behaviour) MonoBehaviourSingleton<UIEnemyAnnounce>.I).isActiveAndEnabled)
      return;
    MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestFieldBuffAnnounce();
    this.isEndFieldBuffAnnounce = true;
  }

  protected override void _OnDestroy()
  {
    if (QuestManager.IsValidInGameExplore())
    {
      MonoBehaviourSingleton<QuestManager>.I.UpdateExploreBossMoveRemainTime(this.bossMoveRemainTime);
      MonoBehaviourSingleton<QuestManager>.I.UpdateExploreHostDCTime(this.exploreHostDCRemainTime);
    }
    if (this.wmDropResource != null)
      this.wmDropResource.Clear();
    this.wmDropResource = (WaveMatchDropResource) null;
    List<IFieldGimmickObject> fieldGimmick = this.fieldGimmickList[7];
    for (int index = 0; index < fieldGimmick.Count; ++index)
    {
      FieldCarriableGimmickObject carriableGimmickObject = fieldGimmick[index] as FieldCarriableGimmickObject;
      if (carriableGimmickObject.isCarrying)
        carriableGimmickObject.RequestDestroy();
    }
    MonoBehaviourSingleton<AppMain>.I.ClearMemory(false, false, true);
  }

  private void GetNearestGatherPoint(out float distance, out GatherPointObject nearestPoint)
  {
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    float num = float.MaxValue;
    GatherPointObject gatherPointObject = (GatherPointObject) null;
    int index = 0;
    for (int count = this.gatherPointList.Count; index < count; ++index)
    {
      GatherPointObject gatherPoint = this.gatherPointList[index];
      Vector3 vector3 = Vector3.op_Subtraction(gatherPoint._transform.position, self._position);
      float magnitude = ((Vector3) ref vector3).magnitude;
      if (!gatherPoint.isGathered && (double) magnitude < (double) num && (double) magnitude < (double) gatherPoint.viewData.targetRadius)
      {
        gatherPointObject = gatherPoint;
        num = magnitude;
      }
    }
    distance = num;
    nearestPoint = gatherPointObject;
  }

  private void GetNearestCamouflagingEnemy(out float nearestDistance, out Enemy nearestEnemy)
  {
    Vector3 position1 = MonoBehaviourSingleton<StageObjectManager>.I.self._transform.position;
    nearestDistance = float.MaxValue;
    nearestEnemy = (Enemy) null;
    List<Enemy> enemyList = MonoBehaviourSingleton<StageObjectManager>.I.EnemyList;
    for (int index = 0; index < enemyList.Count; ++index)
    {
      Enemy enemy = enemyList[index];
      if (enemy.isHiding)
      {
        Vector3 position2 = enemy._transform.position;
        float num = Vector3.Distance(position1, position2);
        if ((double) num < (double) nearestDistance)
        {
          nearestDistance = num;
          nearestEnemy = enemy;
        }
      }
    }
  }

  public void AddFieldGimmickObj(InGameProgress.eFieldGimmick type, IFieldGimmickObject obj)
  {
    this.fieldGimmickList[(int) type].Add(obj);
  }

  public void RemoveFieldGimmickObj(InGameProgress.eFieldGimmick type, IFieldGimmickObject obj)
  {
    this.fieldGimmickList[(int) type].Remove(obj);
  }

  public IFieldGimmickObject GetFieldGimmickObj(InGameProgress.eFieldGimmick type, int id)
  {
    int index1 = (int) type;
    if (this.fieldGimmickList[index1].IsNullOrEmpty<IFieldGimmickObject>())
      return (IFieldGimmickObject) null;
    for (int index2 = 0; index2 < this.fieldGimmickList[index1].Count; ++index2)
    {
      IFieldGimmickObject fieldGimmickObj = this.fieldGimmickList[index1][index2];
      if (fieldGimmickObj != null && id == fieldGimmickObj.GetId())
        return fieldGimmickObj;
    }
    return (IFieldGimmickObject) null;
  }

  public List<IFieldGimmickObject> GetFieldGimmicksObjs(InGameProgress.eFieldGimmick type)
  {
    List<IFieldGimmickObject> fieldGimmicksObjs = new List<IFieldGimmickObject>();
    if (!this.fieldGimmickList[(int) type].IsNullOrEmpty<IFieldGimmickObject>())
    {
      for (int index = 0; index < this.fieldGimmickList[(int) type].Count; ++index)
      {
        IFieldGimmickObject fieldGimmickObject = this.fieldGimmickList[(int) type][index];
        if (fieldGimmickObject != null)
          fieldGimmicksObjs.Add(fieldGimmickObject);
      }
    }
    return fieldGimmicksObjs;
  }

  public void UpdatGatherGimmickInfo(int id, int playerId, bool isUsed)
  {
    FieldGatherGimmickObject fieldGimmickObj = this.GetFieldGimmickObj(InGameProgress.eFieldGimmick.GatherGimmick, id) as FieldGatherGimmickObject;
    if (Object.op_Equality((Object) fieldGimmickObj, (Object) null))
      return;
    Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(playerId) as Player;
    if (Object.op_Equality((Object) player, (Object) null))
      return;
    if (isUsed)
      fieldGimmickObj.OnUseStart(player);
    else
      fieldGimmickObj.OnUseEnd(player);
  }

  protected void _InitGimmickList()
  {
    int length = 11;
    this.fieldGimmickList = new List<IFieldGimmickObject>[length];
    this.gimmickSearchInfo = new InGameProgress.GimmickSearchInfo[length];
    for (int index = 0; index < length; ++index)
    {
      this.fieldGimmickList[index] = new List<IFieldGimmickObject>();
      this.gimmickSearchInfo[index] = new InGameProgress.GimmickSearchInfo();
    }
    this.carriableGimmickDeploiedCount = 0;
  }

  protected void _ClearGimmickList()
  {
    int index = 0;
    for (int length = this.fieldGimmickList.Length; index < length; ++index)
      this.fieldGimmickList[index].Clear();
  }

  protected void _ClearGimmickSearchInfo()
  {
    int index = 0;
    for (int length = this.gimmickSearchInfo.Length; index < length; ++index)
    {
      this.gimmickSearchInfo[index].dist = float.MaxValue;
      this.gimmickSearchInfo[index].obj = (IFieldGimmickObject) null;
    }
  }

  private void _SearchNearestGimmickPoint(int typeIndex)
  {
    if (this.fieldGimmickList[typeIndex].IsNullOrEmpty<IFieldGimmickObject>())
      return;
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    float num = float.MaxValue;
    int index = 0;
    for (int count = this.fieldGimmickList[typeIndex].Count; index < count; ++index)
    {
      IFieldGimmickObject fieldGimmickObject = this.fieldGimmickList[typeIndex][index];
      if (fieldGimmickObject.IsSearchableNearest())
      {
        Vector3 vector3 = Vector3.op_Subtraction(fieldGimmickObject.GetTransform().position, self._position);
        float sqrMagnitude = ((Vector3) ref vector3).sqrMagnitude;
        float targetSqrRadius = fieldGimmickObject.GetTargetSqrRadius();
        if ((double) sqrMagnitude < (double) num && (double) sqrMagnitude < (double) targetSqrRadius)
        {
          num = sqrMagnitude;
          this.gimmickSearchInfo[typeIndex].obj = fieldGimmickObject;
          this.gimmickSearchInfo[typeIndex].dist = sqrMagnitude;
        }
      }
    }
  }

  private void _UpdateGimmickTargetMarker()
  {
    int index1 = 0;
    for (int index2 = 11; index1 < index2; ++index1)
    {
      if (!this.fieldGimmickList[index1].IsNullOrEmpty<IFieldGimmickObject>())
      {
        int index3 = 0;
        for (int count = this.fieldGimmickList[index1].Count; index3 < count; ++index3)
        {
          IFieldGimmickObject fieldGimmickObject = this.fieldGimmickList[index1][index3];
          fieldGimmickObject.UpdateTargetMarker(fieldGimmickObject == this.gimmickSearchInfo[index1].obj);
        }
      }
    }
  }

  public void CountDeploiedCarriableGimmick() => ++this.carriableGimmickDeploiedCount;

  public int GetCarriableGimmickDeploiedCount() => this.carriableGimmickDeploiedCount;

  public void CacheUseResources(LoadingQueue load_queue, ref List<string> loadEffectNames)
  {
    this.gatherPointModelTable = new UIntKeyTable<LoadObject>();
    this.gatherPointToolTable = new StringKeyTable<LoadObject>();
    this.fieldGimmickModelTable = new UIntKeyTable<LoadObject>();
    List<FieldMapTable.FieldGimmickPointTableData> collection = new List<FieldMapTable.FieldGimmickPointTableData>();
    if (FieldManager.IsValidInGameNoBoss())
    {
      List<FieldMapTable.GatherPointTableData> pointListByMapId = Singleton<FieldMapTable>.I.GetGatherPointListByMapID(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
      if (pointListByMapId != null)
      {
        int index = 0;
        for (int count = pointListByMapId.Count; index < count; ++index)
        {
          FieldMapTable.GatherPointViewTableData gatherPointViewData = Singleton<FieldMapTable>.I.GetGatherPointViewData(pointListByMapId[index].viewID);
          if (gatherPointViewData != null)
          {
            if (this.gatherPointModelTable.Get(gatherPointViewData.viewID) == null)
              this.gatherPointModelTable.Add(gatherPointViewData.viewID, load_queue.Load(RESOURCE_CATEGORY.INGAME_GATHER_POINT, ResourceName.GetGatherPointModel(gatherPointViewData.modelID)));
            if (!string.IsNullOrEmpty(gatherPointViewData.toolModelName) && this.gatherPointToolTable.Get(gatherPointViewData.toolModelName) == null)
              this.gatherPointToolTable.Add(gatherPointViewData.toolModelName, load_queue.Load(RESOURCE_CATEGORY.INGAME_GATHER_POINT, gatherPointViewData.toolModelName));
            if (!string.IsNullOrEmpty(gatherPointViewData.gatherEffectName))
            {
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, gatherPointViewData.gatherEffectName);
              loadEffectNames.Add(gatherPointViewData.gatherEffectName);
            }
            if (!string.IsNullOrEmpty(gatherPointViewData.targetEffectName))
            {
              load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, gatherPointViewData.targetEffectName);
              loadEffectNames.Add(gatherPointViewData.targetEffectName);
            }
            if (pointListByMapId[index].gimmickType != FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.NONE)
              collection.Add(pointListByMapId[index].CloneAsGimmickData());
          }
        }
      }
    }
    List<FieldMapTable.FieldGimmickPointTableData> pointListByMapId1 = Singleton<FieldMapTable>.I.GetFieldGimmickPointListByMapID(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
    List<FieldMapTable.FieldGimmickPointTableData> gimmickPointTableDataList = pointListByMapId1 == null ? new List<FieldMapTable.FieldGimmickPointTableData>() : new List<FieldMapTable.FieldGimmickPointTableData>((IEnumerable<FieldMapTable.FieldGimmickPointTableData>) pointListByMapId1);
    gimmickPointTableDataList.AddRange((IEnumerable<FieldMapTable.FieldGimmickPointTableData>) collection);
    if (gimmickPointTableDataList.Count > 0)
    {
      for (int index1 = 0; index1 < gimmickPointTableDataList.Count; ++index1)
      {
        FieldMapTable.FieldGimmickPointTableData gimmickPointTableData = gimmickPointTableDataList[index1];
        string[] effectNameList = (string[]) null;
        int[] seIdList = (int[]) null;
        List<int> intList = new List<int>();
        switch (gimmickPointTableData.gimmickType)
        {
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.HEALING:
            effectNameList = new string[2]
            {
              "ef_btl_heal_spot_01_01",
              "ef_btl_heal_spot_01_02"
            };
            seIdList = new int[1]{ 30000038 };
            break;
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON:
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_HEAVY:
            effectNameList = new string[9]
            {
              ResourceName.GetFieldGimmickCannonTargetEffect(),
              "ef_btl_target_cannon_02",
              "ef_btl_target_cannon_03",
              "ef_btl_magibullet_landing_01",
              "ef_btl_magibullet_landing_02",
              "ef_btl_magibullet_landing_03",
              "ef_btl_magibullet_shot_01",
              "ef_btl_goldbird_aura_01_01",
              "ef_btl_cannon_tap"
            };
            seIdList = new int[1]{ 10000079 };
            break;
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.BOMBROCK:
            effectNameList = new string[2]
            {
              "ef_btl_enemy_explosion_01_02",
              "ef_btl_bg_bombrock_01"
            };
            seIdList = new int[1]{ 30000102 };
            break;
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.GEYSER:
            effectNameList = new string[1]
            {
              "ef_btl_bg_geyser_01"
            };
            seIdList = new int[0];
            break;
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.SONAR:
            effectNameList = new string[3]
            {
              "ef_btl_sonar_01",
              "ef_btl_sonar_02",
              ResourceName.GetSonarTargetEffect()
            };
            seIdList = new int[1]{ 40000107 };
            break;
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_RAPID:
            effectNameList = new string[2]
            {
              ResourceName.GetFieldGimmickCannonTargetEffect(),
              "ef_btl_cannon_tap"
            };
            seIdList = new int[2]
            {
              10000079,
              MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.seIdForRapid
            };
            break;
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_SPECIAL:
            effectNameList = new string[1]
            {
              FieldGimmickCannonSpecial.NAME_EFFECT_CHARGE
            };
            seIdList = new int[4]
            {
              MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.seIdForSpecial,
              MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.seIdForSpecialCharge,
              MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.seIdForSpecialChargeMax,
              MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.seIdForSpecialOnBoard
            };
            break;
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET:
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET2:
            effectNameList = new string[0];
            seIdList = new int[0];
            break;
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_FIELD:
            effectNameList = new string[2]
            {
              ResourceName.GetFieldGimmickCannonTargetEffect(),
              "ef_btl_cannon_tap"
            };
            seIdList = new int[2]
            {
              10000079,
              MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.seIdForField
            };
            intList.Add(FieldGimmickCannonField.GetModelIndex(gimmickPointTableData.value2));
            break;
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.READ_STORY:
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.BINGO:
            effectNameList = new string[1]
            {
              ResourceName.GetReadStoryTargetEffectName()
            };
            seIdList = new int[0];
            break;
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.FISHING:
            int modelIndex1 = FieldFishingGimmickObject.GetModelIndex(gimmickPointTableData.value2);
            intList.Add(modelIndex1);
            effectNameList = FieldFishingGimmickObject.GetEffectNames(modelIndex1);
            InGameSettingsManager.FishingParam fishingParam = MonoBehaviourSingleton<InGameSettingsManager>.I.fishingParam;
            seIdList = new int[5 + fishingParam.hitSeIds.Length];
            seIdList[0] = fishingParam.se0Id[modelIndex1];
            seIdList[1] = fishingParam.se1Id[modelIndex1];
            seIdList[2] = fishingParam.se2Id[modelIndex1];
            seIdList[3] = fishingParam.se3Id[modelIndex1];
            seIdList[4] = fishingParam.hookSeId;
            for (int index2 = 0; index2 < fishingParam.hitSeIds.Length; ++index2)
              seIdList[5 + index2] = fishingParam.hitSeIds[index2];
            if (this.gatherPointToolTable.Get("Fishingrod") == null)
            {
              this.gatherPointToolTable.Add("Fishingrod", load_queue.Load(RESOURCE_CATEGORY.INGAME_GATHER_POINT, "Fishingrod"));
              break;
            }
            break;
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CHAT:
            effectNameList = new string[1]
            {
              "ef_btl_target_readstory_01"
            };
            seIdList = new int[0];
            break;
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET3:
            int modelIndex2 = FieldWaveTargetObject.GetModelIndex(gimmickPointTableData.value2);
            effectNameList = FieldWaveTargetObject.GetEffectNamesByModelIndex(modelIndex2);
            seIdList = new int[0];
            intList.Add(modelIndex2);
            break;
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.GENERATOR:
            effectNameList = GimmickGeneratorObject.GetEffectNames(gimmickPointTableData.value2);
            seIdList = new int[0];
            break;
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANDYWOOD:
            effectNameList = new string[0];
            seIdList = new int[0];
            break;
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.SUPPLY:
            intList.Add(FieldSupplyGimmickObject.GetModelIndex(gimmickPointTableData.value2));
            effectNameList = new string[2]
            {
              FieldSupplyGimmickObject.kSupplyMarkerName,
              FieldSupplyGimmickObject.kBreakEffectName
            };
            seIdList = new int[1]
            {
              FieldSupplyGimmickObject.kBreakSEId
            };
            break;
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_TURRET:
            intList.AddRange((IEnumerable<int>) FieldCarriableGimmickObject.GetModelIndexes(gimmickPointTableData.value2));
            effectNameList = new string[3]
            {
              FieldCarriableGimmickObject.kCarryMarkerName,
              FieldCarriableTurretGimmickObject.kShotEffectName,
              FieldCarriableTurretGimmickObject.kPutEffectName
            };
            seIdList = new int[2]
            {
              FieldCarriableTurretGimmickObject.GetShotSEId(gimmickPointTableData.value2),
              FieldCarriableTurretGimmickObject.kPutSEId
            };
            break;
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_EVOLVE_ITEM:
            intList.AddRange((IEnumerable<int>) FieldCarriableGimmickObject.GetModelIndexes(gimmickPointTableData.value2));
            effectNameList = new string[3]
            {
              FieldCarriableGimmickObject.kCarryMarkerName,
              FieldCarriableGimmickObject.kEvolveMarkerName,
              FieldCarriableGimmickObject.kEvolveEffectName
            };
            seIdList = new int[1]
            {
              FieldCarriableGimmickObject.kEvolveSEId
            };
            break;
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_DECOY:
            intList.AddRange((IEnumerable<int>) FieldCarriableGimmickObject.GetModelIndexes(gimmickPointTableData.value2));
            effectNameList = new string[4]
            {
              FieldCarriableGimmickObject.kCarryMarkerName,
              FieldCarriableDecoyGimmickObject.kBreakEffectName,
              FieldCarriableDecoyGimmickObject.kDecoyEffectName,
              FieldCarriableDecoyGimmickObject.kPutEffectName
            };
            seIdList = new int[2]
            {
              FieldCarriableDecoyGimmickObject.kPutSEId,
              FieldCarriableDecoyGimmickObject.kBreakSEId
            };
            break;
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_BUFF_POINT:
            intList.AddRange((IEnumerable<int>) FieldCarriableGimmickObject.GetModelIndexes(gimmickPointTableData.value2));
            List<string> stringList1 = new List<string>();
            stringList1.Add(FieldCarriableGimmickObject.kCarryMarkerName);
            stringList1.Add(FieldCarriableBuffPointGimmickObject.kPutEffectName);
            for (int index3 = 0; index3 < intList.Count; ++index3)
            {
              stringList1.Add(FieldCarriableBuffPointGimmickObject.GetBuffEffectNameByModelIndex(intList[index3]));
              stringList1.Add(FieldCarriableBuffPointGimmickObject.GetHeadEffectNameByModelIndex(intList[index3]));
            }
            effectNameList = stringList1.ToArray();
            seIdList = new int[1]
            {
              FieldCarriableBuffPointGimmickObject.kPutSEId
            };
            break;
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.PORTAL_GIMMICK:
            effectNameList = new string[1]
            {
              ResourceName.GetReadStoryTargetEffectName()
            };
            seIdList = new int[0];
            break;
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_BOMB:
            intList.AddRange((IEnumerable<int>) FieldCarriableGimmickObject.GetModelIndexes(gimmickPointTableData.value2));
            List<string> stringList2 = new List<string>()
            {
              FieldCarriableGimmickObject.kCarryMarkerName,
              FieldCarriableBombGimmickObject.kPutEffectName
            };
            for (int index4 = 0; index4 < intList.Count; ++index4)
              stringList2.Add(FieldCarriableBombGimmickObject.GetFuseEffectNameByModelIndex(intList[index4]));
            effectNameList = stringList2.ToArray();
            seIdList = new int[2]
            {
              FieldCarriableBombGimmickObject.kBombSEId,
              FieldCarriableBombGimmickObject.kPutSEId
            };
            break;
          case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.QUEST:
            FieldMapTable.GatherPointViewTableData gatherPointData = FieldQuestGimmickObject.GetGatherPointData(gimmickPointTableData.value2);
            if (gatherPointData == null)
            {
              Log.Error(LOG.DATA_TABLE, "gvidに設定したデータに異常があります。\nvalue2 :: " + gimmickPointTableData.value2);
              break;
            }
            intList.Add((int) gatherPointData.modelID);
            effectNameList = new string[2]
            {
              gatherPointData.gatherEffectName,
              gatherPointData.targetEffectName
            };
            seIdList = new int[0];
            break;
        }
        if (intList.Count == 0)
          intList.Add(0);
        if (gimmickPointTableData.gimmickType != FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.NONE)
          FieldGimmickObject.CacheResources(load_queue, gimmickPointTableData.gimmickType, effectNameList, seIdList, intList.ToArray());
      }
    }
    if (QuestManager.IsValidInGameWaveMatch())
    {
      this.wmDropResource = new WaveMatchDropResource();
      this.wmDropResource.Cache(load_queue);
    }
    this.CacheAudio(load_queue);
  }

  public void BattleStart()
  {
    this.isBattleStart = true;
    bool flag = false;
    if (FieldManager.IsValidInGameNoBoss())
    {
      if (QuestManager.IsValidInGame() && (MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestSeriesNum() > 1 || MonoBehaviourSingleton<QuestManager>.I.IsExplore()))
        MonoBehaviourSingleton<FieldManager>.I.InitPortalPointForExplore(MonoBehaviourSingleton<QuestManager>.I.GetExploreStatus());
      this.portalObjectList = new List<PortalObject>();
      List<FieldMapPortalInfo> fieldPortalInfoList = MonoBehaviourSingleton<FieldManager>.I.currentFieldPortalInfoList;
      if (fieldPortalInfoList != null)
      {
        int index = 0;
        for (int count = fieldPortalInfoList.Count; index < count; ++index)
        {
          FieldMapPortalInfo portal_info = fieldPortalInfoList[index];
          if (portal_info.IsValid() && FieldManager.IsShowPortal(portal_info.portalData))
            this.portalObjectList.Add(PortalObject.Create(portal_info, this._transform));
        }
      }
      this.gatherPointList = new List<GatherPointObject>();
      List<FieldMapTable.GatherPointTableData> pointListByMapId = Singleton<FieldMapTable>.I.GetGatherPointListByMapID(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
      if (pointListByMapId != null)
      {
        int index = 0;
        for (int count = pointListByMapId.Count; index < count; ++index)
        {
          FieldMapTable.GatherPointTableData gatherPointTableData = pointListByMapId[index];
          GatherPointObject gatherPointObject;
          switch (FieldMapTable.GatherPointTableData.GetGatherType(gatherPointTableData))
          {
            case FieldMapTable.GatherPointTableData.GatherType.Growth:
              gatherPointObject = (GatherPointObject) GatherPointObject.Create<GrowthGatherPointObject>(gatherPointTableData, this._transform);
              break;
            default:
              gatherPointObject = (GatherPointObject) GatherPointObject.Create<BasicGatherPointObject>(gatherPointTableData, this._transform);
              break;
          }
          this.gatherPointList.Add(gatherPointObject);
          if (gatherPointTableData.gimmickType != FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.NONE)
          {
            IFieldGimmickObject gimmick = this.CreateGimmick(gatherPointTableData.CloneAsGimmickData());
            if (gimmick != null)
            {
              gatherPointObject.gimmick = gimmick as FieldGimmickObject;
              gatherPointObject.UpdateView();
            }
          }
        }
      }
      if (FieldManager.IsValidInGameNoQuest())
        this.DeliveryAddCheck();
      GameSceneGlobalSettings.RequestSoundSettingIngameField();
    }
    List<FieldMapTable.FieldGimmickPointTableData> pointListByMapId1 = Singleton<FieldMapTable>.I.GetFieldGimmickPointListByMapID(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
    if (pointListByMapId1 != null)
    {
      for (int index = 0; index < pointListByMapId1.Count; ++index)
        this.CreateGimmick(pointListByMapId1[index]);
    }
    if (QuestManager.IsValidInGame())
    {
      if (MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.coopStage.isQuestClose)
      {
        if (MonoBehaviourSingleton<CoopManager>.I.coopStage.isQuestSucceed)
        {
          this.BattleComplete();
          flag = true;
        }
        else
          this.BattleRetire();
      }
      else if (QuestManager.IsValidInGameExplore())
      {
        if (!MonoBehaviourSingleton<InGameManager>.I.isAlreadyBattleStarted)
        {
          this.PlayBattleStartEffect(false);
          MonoBehaviourSingleton<InGameManager>.I.isAlreadyBattleStarted = true;
        }
      }
      else
        this.PlayBattleStartEffect(MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestSeriesNum() > 1);
      if (QuestManager.IsValidInGameExplore())
      {
        if (this.missionCheck.Count == 0)
        {
          this.InitMissionCheck();
          MonoBehaviourSingleton<QuestManager>.I.GetExploreStatus().SetMissions(this.missionCheck);
        }
      }
      else
        this.InitMissionCheck();
    }
    int index1 = 0;
    for (int count = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count; index1 < count; ++index1)
    {
      Player player = MonoBehaviourSingleton<StageObjectManager>.I.playerList[index1] as Player;
      if (!player.isDead && !player.isLoading && !player.IsPuppet())
        player.ActBattleStart();
    }
    if (MonoBehaviourSingleton<UIInGamePopupDialog>.IsValid())
      MonoBehaviourSingleton<UIInGamePopupDialog>.I.SetEnableDialog(true);
    if (MonoBehaviourSingleton<InGameManager>.I.isGateQuestClear)
      MonoBehaviourSingleton<InGameManager>.I.isGateQuestClear = false;
    if (QuestManager.IsValidInGameExplore() && MonoBehaviourSingleton<QuestManager>.I.IsExploreBossMap())
    {
      if (MonoBehaviourSingleton<QuestManager>.I.IsExploreBossDead() && !flag && !MonoBehaviourSingleton<CoopManager>.I.coopStage.isQuestClose)
        this.BattleComplete();
      else if (!MonoBehaviourSingleton<QuestManager>.I.IsEncountered())
      {
        int bossMapIndex = MonoBehaviourSingleton<QuestManager>.I.ExploreMapIdToIndex((uint) MonoBehaviourSingleton<QuestManager>.I.GetExploreBossBatlleMapId());
        bool inOtherMap = false;
        MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.ForEach((Action<CoopClient>) (x => inOtherMap |= x.exploreMapIndex != bossMapIndex));
        if (inOtherMap)
          MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendNotifyEncounterBoss(MonoBehaviourSingleton<QuestManager>.I.GetExploreBossAppearMapId(), (int) MonoBehaviourSingleton<QuestManager>.I.GetLastPortalId());
      }
      MonoBehaviourSingleton<QuestManager>.I.ResetMemberEncountered();
    }
    if (MonoBehaviourSingleton<FieldManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.I.currentFieldBuffId > 0U)
      this.isEndFieldBuffAnnounce = false;
    else
      this.isEndFieldBuffAnnounce = true;
  }

  public IFieldGimmickObject CreateGimmick(
    FieldMapTable.FieldGimmickPointTableData gimmickPointTableData)
  {
    if (gimmickPointTableData == null)
      return (IFieldGimmickObject) null;
    IFieldGimmickObject gimmick = (IFieldGimmickObject) null;
    switch (gimmickPointTableData.gimmickType)
    {
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.HEALING:
        gimmick = FieldGimmickObject.Create<FieldHealingPointObject>(gimmickPointTableData, 19, this._transform);
        break;
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON:
        Transform parent1 = MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : this._transform;
        gimmick = FieldGimmickObject.Create<FieldGimmickCannonObject>(gimmickPointTableData, 19, parent1);
        this.AddFieldGimmickObj(InGameProgress.eFieldGimmick.Cannon, gimmick);
        break;
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.BOMBROCK:
        gimmick = FieldGimmickObject.Create<FieldGimmickBombRockObject>(gimmickPointTableData, 18, this._transform);
        break;
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.GEYSER:
        gimmick = FieldGimmickObject.Create<FieldGimmickGeyserObject>(gimmickPointTableData, 19, this._transform);
        break;
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.SONAR:
        gimmick = FieldGimmickObject.Create<FieldSonarObject>(gimmickPointTableData, 19, this._transform);
        this.AddFieldGimmickObj(InGameProgress.eFieldGimmick.Sonar, gimmick);
        break;
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_HEAVY:
        Transform parent2 = MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : this._transform;
        gimmick = FieldGimmickObject.Create<FieldGimmickCannonHeavy>(gimmickPointTableData, 19, parent2);
        this.AddFieldGimmickObj(InGameProgress.eFieldGimmick.Cannon, gimmick);
        break;
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_RAPID:
        Transform parent3 = MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : this._transform;
        gimmick = FieldGimmickObject.Create<FieldGimmickCannonRapid>(gimmickPointTableData, 19, parent3);
        this.AddFieldGimmickObj(InGameProgress.eFieldGimmick.Cannon, gimmick);
        break;
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_SPECIAL:
        Transform parent4 = MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : this._transform;
        gimmick = FieldGimmickObject.Create<FieldGimmickCannonSpecial>(gimmickPointTableData, 19, parent4);
        this.AddFieldGimmickObj(InGameProgress.eFieldGimmick.Cannon, gimmick);
        break;
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET:
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET2:
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.WAVE_TARGET3:
        gimmick = FieldGimmickObject.Create<FieldWaveTargetObject>(gimmickPointTableData, 18, this._transform);
        break;
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANNON_FIELD:
        Transform parent5 = MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : this._transform;
        gimmick = FieldGimmickObject.Create<FieldGimmickCannonField>(gimmickPointTableData, 19, parent5);
        this.AddFieldGimmickObj(InGameProgress.eFieldGimmick.Cannon, gimmick);
        break;
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.READ_STORY:
        if (FieldReadStoryObject.IsValid(gimmickPointTableData.value2))
        {
          gimmick = FieldGimmickObject.Create<FieldReadStoryObject>(gimmickPointTableData, 19, this._transform);
          this.AddFieldGimmickObj(InGameProgress.eFieldGimmick.ReadStory, gimmick);
          break;
        }
        break;
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.FISHING:
        gimmick = FieldGimmickObject.Create<FieldFishingGimmickObject>(gimmickPointTableData, 19, this._transform);
        this.AddFieldGimmickObj(InGameProgress.eFieldGimmick.GatherGimmick, gimmick);
        break;
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.BINGO:
        if (FieldBingoObject.IsValid(gimmickPointTableData.value2))
        {
          gimmick = FieldGimmickObject.Create<FieldBingoObject>(gimmickPointTableData, 19, this._transform);
          this.AddFieldGimmickObj(InGameProgress.eFieldGimmick.Bingo, gimmick);
          break;
        }
        break;
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CHAT:
        if (FieldChatGimmickObject.IsValid(gimmickPointTableData.value2))
        {
          gimmick = FieldGimmickObject.Create<FieldChatGimmickObject>(gimmickPointTableData, 19, this._transform);
          this.AddFieldGimmickObj(InGameProgress.eFieldGimmick.Chat, gimmick);
          break;
        }
        break;
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.GENERATOR:
        Transform parent6 = MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : this._transform;
        gimmick = FieldGimmickObject.Create<GimmickGeneratorObject>(gimmickPointTableData, 19, parent6);
        break;
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CANDYWOOD:
        gimmick = FieldGimmickObject.Create<FieldGimmickCandyWoodObject>(gimmickPointTableData, 19, this._transform);
        break;
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.SUPPLY:
        gimmick = FieldGimmickObject.Create<FieldSupplyGimmickObject>(gimmickPointTableData, 19, this._transform);
        this.AddFieldGimmickObj(InGameProgress.eFieldGimmick.SupplyGimmick, gimmick);
        break;
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_TURRET:
        gimmick = FieldGimmickObject.Create<FieldCarriableTurretGimmickObject>(gimmickPointTableData, 19, this._transform);
        this.AddFieldGimmickObj(InGameProgress.eFieldGimmick.CarriableGimmick, gimmick);
        break;
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_EVOLVE_ITEM:
        gimmick = FieldGimmickObject.Create<FieldCarriableEvolveItemGimmickObject>(gimmickPointTableData, 19, this._transform);
        this.AddFieldGimmickObj(InGameProgress.eFieldGimmick.CarriableGimmick, gimmick);
        break;
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_DECOY:
        gimmick = FieldGimmickObject.Create<FieldCarriableDecoyGimmickObject>(gimmickPointTableData, 19, this._transform);
        this.AddFieldGimmickObj(InGameProgress.eFieldGimmick.CarriableGimmick, gimmick);
        break;
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_BUFF_POINT:
        gimmick = FieldGimmickObject.Create<FieldCarriableBuffPointGimmickObject>(gimmickPointTableData, 19, this._transform);
        this.AddFieldGimmickObj(InGameProgress.eFieldGimmick.CarriableGimmick, gimmick);
        break;
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.PORTAL_GIMMICK:
        if (FieldPortalGimmickObject.IsValid(gimmickPointTableData.value2))
        {
          gimmick = FieldGimmickObject.Create<FieldPortalGimmickObject>(gimmickPointTableData, 19, this._transform);
          this.AddFieldGimmickObj(InGameProgress.eFieldGimmick.PortalGimmick, gimmick);
          break;
        }
        break;
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.CARRIABLE_BOMB:
        gimmick = FieldGimmickObject.Create<FieldCarriableBombGimmickObject>(gimmickPointTableData, 19, this._transform);
        this.AddFieldGimmickObj(InGameProgress.eFieldGimmick.CarriableGimmick, gimmick);
        break;
      case FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.QUEST:
        if (FieldQuestGimmickObject.IsValidParam(gimmickPointTableData.value2))
        {
          gimmick = FieldGimmickObject.Create<FieldQuestGimmickObject>(gimmickPointTableData, 19, this._transform);
          this.AddFieldGimmickObj(InGameProgress.eFieldGimmick.QuestGimmick, gimmick);
          break;
        }
        break;
    }
    gimmick?.Initialize(gimmickPointTableData);
    return gimmick;
  }

  public bool OnRecvWaveMatchDrop(Coop_Model_WaveMatchDrop model)
  {
    if (this.wmDropResource != null)
      this.wmDropResource.Create(model);
    return true;
  }

  public bool OnRecvWaveMatchDropCreate(Coop_Model_WaveMatchDropCreate model)
  {
    if (this.wmDropResource != null)
      this.wmDropResource.OnCreate(model.managedId, model.dataId, model.basePos, model.offset, model.sec);
    return true;
  }

  public bool OnRecvWaveMatchDropPicked(Coop_Model_WaveMatchDropPicked model)
  {
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
      MonoBehaviourSingleton<StageObjectManager>.I.PickedWaveMatchDropObject(model, true);
    return true;
  }

  public void CheckGatherPointList()
  {
    if (this.gatherPointList == null)
      return;
    int index = 0;
    for (int count = this.gatherPointList.Count; index < count; ++index)
      this.gatherPointList[index].CheckGather();
  }

  private void InitMissionCheck()
  {
    QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
    if (questData == null)
      return;
    QuestTable.MissionTableData[] missionData = Singleton<QuestTable>.I.GetMissionData(questData.missionID);
    int index = 0;
    for (int length = missionData.Length; index < length; ++index)
    {
      if (missionData[index] != null && missionData[index].missionID != 0U)
      {
        MissionCheckBase missionCheck = MissionCheckBase.CreateMissionCheck(missionData[index]);
        if (missionCheck != null)
          this.missionCheck.Add(missionCheck);
      }
    }
  }

  public void PlayBattleStartEffect(bool is_phase_number)
  {
    this.CreateUIEffect(MonoBehaviourSingleton<InGameLinkResourcesQuest>.I.questStart);
    this.PlayAudio(InGameProgress.AUDIO.QUEST_START);
  }

  public bool BattleComplete(bool forceComplete = false)
  {
    this.forceComplete = forceComplete;
    if (!forceComplete && (!this.isBattleStart || this.progressEndType != InGameProgress.PROGRESS_END_TYPE.NONE))
      return false;
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      MonoBehaviourSingleton<CoopManager>.I.coopStage.CheckEntryClose(true);
    this.isGameProgressStop = true;
    this.StopTimer();
    if (QuestManager.IsValidInGame() && MonoBehaviourSingleton<QuestManager>.I.GetVorgonQuestType() == QuestManager.VorgonQuetType.BATTLE_WITH_WYBURN)
    {
      this.isSendCompleteError = false;
      this.SendComplete();
      if (MonoBehaviourSingleton<GameSceneManager>.IsValid())
      {
        InGameMain gameMain = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection() as InGameMain;
        if (Object.op_Inequality((Object) gameMain, (Object) null))
          gameMain.cutScenePlayer.Play((System.Action) (() =>
          {
            if (gameMain.cutScenePlayer.hasStory)
              return;
            MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("InGameMain", ((Component) this).gameObject, "HOME");
          }));
        return true;
      }
    }
    int num = !MonoBehaviourSingleton<InGameManager>.I.IsRush() ? 0 : (!MonoBehaviourSingleton<InGameManager>.I.IsLastRash() ? 1 : 0);
    bool flag = MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo() && !MonoBehaviourSingleton<InGameManager>.I.IsArenaFinalWave();
    if (num != 0)
    {
      this.SendRushProgress();
      this.StartCoroutine(this.OnProgressEnd(InGameProgress.PROGRESS_END_TYPE.RUSH_INTERVAL));
    }
    else if (flag)
    {
      this.SendArenaProgress();
      this.StartCoroutine(this.OnProgressEnd(InGameProgress.PROGRESS_END_TYPE.ARENA_INTERVAL));
    }
    else
    {
      this.isSendCompleteError = false;
      if (MonoBehaviourSingleton<InGameManager>.I.IsRush())
        this.SendRushProgress();
      else if (MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
        this.SendArenaProgress();
      else
        this.SendComplete();
      if (MonoBehaviourSingleton<CoopManager>.IsValid())
        MonoBehaviourSingleton<CoopManager>.I.coopStage.SetQuestClose(true);
      this.StartCoroutine(this.OnProgressEnd(InGameProgress.PROGRESS_END_TYPE.QUEST_VICTORY));
    }
    return true;
  }

  public bool PortalNext(uint next_portal_id)
  {
    if (next_portal_id == 0U)
      return false;
    FieldMapTable.PortalTableData portal_data = Singleton<FieldMapTable>.I.GetPortalData(next_portal_id);
    if (portal_data == null)
      return false;
    if (MonoBehaviourSingleton<QuestManager>.I.IsExplore())
    {
      MonoBehaviourSingleton<QuestManager>.I.UpdateLastPortalData(portal_data);
      this.toFieldPortalID = next_portal_id;
      this.isGameProgressStop = true;
      MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.ForEach((Action<CoopClient>) (x =>
      {
        if (!Object.op_Inequality((Object) x.GetPlayer(), (Object) null))
          return;
        MonoBehaviourSingleton<QuestManager>.I.UpdateExplorePlayerStatus(x);
      }));
      this.StartCoroutine(this.OnProgressEnd(InGameProgress.PROGRESS_END_TYPE.EXPLORE_MOVE_INTERVAL));
      return true;
    }
    if (MonoBehaviourSingleton<QuestManager>.I.GetCurrentQuestSeriesNum() > 1)
    {
      this.isGameProgressStop = true;
      this.StopTimer();
      this.StartCoroutine(this.OnProgressEnd(InGameProgress.PROGRESS_END_TYPE.QUEST_SERIES_INTERVAL));
      return true;
    }
    bool is_gate = false;
    if (portal_data.dstMapID != 0U && portal_data.dstQuestID != 0U)
    {
      int num = 0;
      ClearStatusQuest clearStatusQuest = MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.Find((Predicate<ClearStatusQuest>) (data => (long) data.questId == (long) portal_data.dstQuestID));
      if (clearStatusQuest != null)
        num = clearStatusQuest.questStatus;
      if (num != 3 && num != 4)
        is_gate = true;
    }
    if (portal_data.dstMapID != 0U && !is_gate)
      this.FieldMapInterval(next_portal_id);
    else if (portal_data.dstQuestID != 0U)
      this.FieldToQuestInterval(portal_data.dstQuestID, next_portal_id, is_gate);
    else
      this.FieldToHome();
    return true;
  }

  public bool FieldMapInterval(uint to_portal_id)
  {
    if (!this.isBattleStart || this.progressEndType != InGameProgress.PROGRESS_END_TYPE.NONE)
      return false;
    this.toFieldPortalID = to_portal_id;
    this.isGameProgressStop = true;
    if (MonoBehaviourSingleton<FieldManager>.I.isTutorialField)
      this.StartCoroutine(this.OnProgressEnd(InGameProgress.PROGRESS_END_TYPE.FIELD_MAP_INTERVAL_TUTORIAL));
    else
      this.StartCoroutine(this.OnProgressEnd(InGameProgress.PROGRESS_END_TYPE.FIELD_MAP_INTERVAL));
    return true;
  }

  public bool FieldToQuestInterval(
    uint to_quest_id,
    uint from_portal_id,
    bool is_gate,
    bool from_gimmick = false)
  {
    if (!this.isBattleStart || this.progressEndType != InGameProgress.PROGRESS_END_TYPE.NONE)
      return false;
    this.toQuestID = to_quest_id;
    this.toQuestPortalID = from_portal_id;
    this.toQuestGate = is_gate;
    this.toQuestFromGimmick = from_gimmick;
    this.isGameProgressStop = true;
    this.SetLimitTime(0.0f);
    MonoBehaviourSingleton<InGameManager>.I.StopIntervalTransferInfoRemaindTimeUpdate();
    InGameProgress.PROGRESS_END_TYPE type = InGameProgress.PROGRESS_END_TYPE.FIELD_TO_QUEST_INTERVAL;
    if (Singleton<QuestTable>.IsValid())
    {
      QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(to_quest_id);
      if (questData != null && questData.storyId != 0)
        type = InGameProgress.PROGRESS_END_TYPE.FIELD_TO_STORY;
    }
    this.StartCoroutine(this.OnProgressEnd(type));
    return true;
  }

  public bool FieldReadStory(int storyId, bool isSend, EventData[] requestEventData = null)
  {
    if (!this.isBattleStart || this.progressEndType != InGameProgress.PROGRESS_END_TYPE.NONE)
      return false;
    this.fieldReadStoryId = storyId;
    this.isFieldReadStorySend = isSend;
    this.fieldReadStoryRequestEvent = requestEventData;
    this.StartCoroutine(this.OnProgressEnd(InGameProgress.PROGRESS_END_TYPE.FIELD_READ_STORY));
    return true;
  }

  public bool ExploreFieldToQuestInterval()
  {
    if (!this.isBattleStart || this.progressEndType != InGameProgress.PROGRESS_END_TYPE.NONE)
      return false;
    this.isGameProgressStop = true;
    this.StartCoroutine(this.OnProgressEnd(InGameProgress.PROGRESS_END_TYPE.EXPLORE_HAPPEN_INTERVAL));
    return true;
  }

  public bool FieldToHome()
  {
    if (!this.isBattleStart || this.progressEndType != InGameProgress.PROGRESS_END_TYPE.NONE)
      return false;
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      MonoBehaviourSingleton<CoopManager>.I.coopMyClient.BattleRetire();
    if (QuestManager.IsValidInGame())
      return false;
    this.StartCoroutine(this.OnProgressEnd(InGameProgress.PROGRESS_END_TYPE.FIELD_TO_HOME));
    return true;
  }

  public bool FieldToHomeTimeout()
  {
    if (!this.isBattleStart || this.progressEndType != InGameProgress.PROGRESS_END_TYPE.NONE)
      return false;
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      MonoBehaviourSingleton<CoopManager>.I.coopMyClient.BattleRetire();
    if (QuestManager.IsValidInGame())
      return false;
    this.StartCoroutine(this.OnProgressEnd(InGameProgress.PROGRESS_END_TYPE.FIELD_TO_HOME_TIMEOUT));
    return true;
  }

  public bool InviteInQuest()
  {
    if (!this.isBattleStart || this.progressEndType != InGameProgress.PROGRESS_END_TYPE.NONE)
      return false;
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      MonoBehaviourSingleton<CoopManager>.I.coopMyClient.BattleRetire();
    this.disableSendProgressStop = true;
    this.isGameProgressStop = true;
    this.StopTimer();
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
    {
      MonoBehaviourSingleton<InGameManager>.I.ClearRush();
      MonoBehaviourSingleton<InGameManager>.I.ClearArenaInfo();
    }
    if (QuestManager.IsValidInGame())
    {
      if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
        this.SendArenaRetire();
      else
        this.SendRetire();
      MonoBehaviourSingleton<CoopManager>.I.coopStage.SetRetireQuestClose();
      this.StartCoroutine(this.OnProgressEnd(InGameProgress.PROGRESS_END_TYPE.QUEST_INVITEQUIT));
    }
    else
      this.StartCoroutine(this.OnProgressEnd(InGameProgress.PROGRESS_END_TYPE.FIELD_TO_HOME));
    return true;
  }

  public bool QuestToField(uint fieldPortalID)
  {
    if (!this.isBattleStart || this.progressEndType != InGameProgress.PROGRESS_END_TYPE.NONE)
      return false;
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      MonoBehaviourSingleton<CoopManager>.I.coopMyClient.BattleRetire();
    this.disableSendProgressStop = true;
    this.isGameProgressStop = true;
    this.StopTimer();
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
    {
      MonoBehaviourSingleton<InGameManager>.I.ClearRush();
      MonoBehaviourSingleton<InGameManager>.I.ClearArenaInfo();
    }
    this.toFieldPortalID = fieldPortalID;
    if (QuestManager.IsValidInGame())
    {
      if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
        this.SendArenaRetire();
      else
        this.SendRetire();
      MonoBehaviourSingleton<CoopManager>.I.coopStage.SetRetireQuestClose();
      this.StartCoroutine(this.OnProgressEnd(InGameProgress.PROGRESS_END_TYPE.QUEST_TO_FIELD));
    }
    else
      this.StartCoroutine(this.OnProgressEnd(InGameProgress.PROGRESS_END_TYPE.FIELD_TO_HOME));
    return true;
  }

  public bool QuestRepeat()
  {
    if (this.progressEndType != InGameProgress.PROGRESS_END_TYPE.QUEST_VICTORY)
      return false;
    this.isGameProgressStop = true;
    this.StartCoroutine(this.OnProgressEnd(InGameProgress.PROGRESS_END_TYPE.QUEST_TO_QUEST_REPEAT));
    return true;
  }

  public bool BattleRetire()
  {
    if (!this.isBattleStart || this.progressEndType != InGameProgress.PROGRESS_END_TYPE.NONE)
      return false;
    if (MonoBehaviourSingleton<ShopManager>.IsValid())
      MonoBehaviourSingleton<ShopManager>.I.trackPlayerDie = true;
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      MonoBehaviourSingleton<CoopManager>.I.coopMyClient.BattleRetire();
    this.disableSendProgressStop = true;
    this.isGameProgressStop = true;
    this.StopTimer();
    if (QuestManager.IsValidInGame())
    {
      if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
        this.SendArenaRetire();
      else
        this.SendRetire();
      MonoBehaviourSingleton<CoopManager>.I.coopStage.SetRetireQuestClose();
      this.StartCoroutine(this.OnProgressEnd(InGameProgress.PROGRESS_END_TYPE.QUEST_RETIRE));
    }
    else
      this.StartCoroutine(this.OnProgressEnd(InGameProgress.PROGRESS_END_TYPE.FIELD_RETIRE));
    return true;
  }

  public bool BattleRetry()
  {
    if (!this.isBattleStart || this.progressEndType != InGameProgress.PROGRESS_END_TYPE.NONE)
      return false;
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      MonoBehaviourSingleton<CoopManager>.I.coopMyClient.BattleRetire();
    this.disableSendProgressStop = true;
    this.isGameProgressStop = true;
    this.StopTimer();
    if (QuestManager.IsValidInGame())
    {
      if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
        this.SendArenaRetire();
      MonoBehaviourSingleton<CoopManager>.I.coopStage.SetRetireQuestClose();
      this.StartCoroutine(this.OnProgressEnd(InGameProgress.PROGRESS_END_TYPE.QUEST_RETRY));
    }
    return true;
  }

  public bool BattleForceDefeatsEvent()
  {
    if (!this.isBattleStart || this.progressEndType != InGameProgress.PROGRESS_END_TYPE.NONE)
      return false;
    List<List<int>> breakIds = new List<List<int>>(5);
    for (int index = 0; index < 4; ++index)
    {
      breakIds.Add(new List<int>());
      breakIds[index].Add(0);
    }
    MonoBehaviourSingleton<QuestManager>.I.SendQuestComplete(breakIds, (List<int>) null, (List<int>) null, 0.0f, (List<QuestCompleteModel.BattleUserLog>) null, (Action<bool, Error>) ((is_success, result) =>
    {
      this.isRecvQuestComplete = true;
      MonoBehaviourSingleton<CoopManager>.I.coopMyClient.EndBattle();
      int status = 0;
      ClearStatusQuest clearStatusQuest = MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.Find((Predicate<ClearStatusQuest>) (data => (long) data.questId == (long) MonoBehaviourSingleton<QuestManager>.I.currentQuestID));
      if (clearStatusQuest != null)
        status = clearStatusQuest.questStatus;
      if (!is_success)
        return;
      MonoBehaviourSingleton<QuestManager>.I.SaveLastNewClearQuest(status);
    }));
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      MonoBehaviourSingleton<CoopManager>.I.coopStage.SetQuestClose(true);
    if (QuestManager.IsValidInGame())
      this.StartCoroutine(this.OnProgressEnd(InGameProgress.PROGRESS_END_TYPE.FORCE_DEFEAT));
    return true;
  }

  public bool BattleTimeup()
  {
    if (!this.isBattleStart || this.progressEndType != InGameProgress.PROGRESS_END_TYPE.NONE)
      return false;
    this.isGameProgressStop = true;
    this.StopTimer();
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
    {
      if (MonoBehaviourSingleton<CoopManager>.I.isStageHost)
        MonoBehaviourSingleton<CoopManager>.I.coopStage.StageTimeup();
      MonoBehaviourSingleton<CoopManager>.I.coopStage.SetQuestClose(false);
    }
    if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsWaveStrategyMatch())
    {
      this.BattleComplete();
      for (int index = 0; index < MonoBehaviourSingleton<StageObjectManager>.I.waveTargetList.Count; ++index)
      {
        FieldWaveTargetObject waveTarget = MonoBehaviourSingleton<StageObjectManager>.I.waveTargetList[index] as FieldWaveTargetObject;
        if (!Object.op_Equality((Object) waveTarget, (Object) null))
          waveTarget.Barrier();
      }
    }
    else
    {
      if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
        this.SendArenaRetire();
      else
        this.SendRetire();
      this.StartCoroutine(this.OnProgressEnd(InGameProgress.PROGRESS_END_TYPE.QUEST_TIMEUP));
    }
    return true;
  }

  public void OnHostEnmeyBossTimeUp()
  {
    if (!MonoBehaviourSingleton<CoopManager>.IsValid())
      return;
    MonoBehaviourSingleton<CoopManager>.I.coopStage.EscapeHostEnmeyBoss();
  }

  public bool FieldReentry()
  {
    if (this.progressEndType != InGameProgress.PROGRESS_END_TYPE.NONE || MonoBehaviourSingleton<FieldManager>.I.isTutorialField)
      return false;
    this.isGameProgressStop = true;
    if (!QuestManager.IsValidInGameExplore())
    {
      this.StopTimer();
      if (MonoBehaviourSingleton<InGameManager>.I.IsRush())
        MonoBehaviourSingleton<InGameManager>.I.BackupRushStageInReentry();
      if (MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeries() || MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeriesArena())
        MonoBehaviourSingleton<InGameManager>.I.BackupSeriesStageInReentry();
    }
    else
    {
      Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
      if (this.isBattleStart && Object.op_Implicit((Object) boss))
        MonoBehaviourSingleton<QuestManager>.I.UpdateExploreBossStatus(boss);
    }
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      MonoBehaviourSingleton<CoopManager>.I.coopStage.SetIsEnterFieldEnemyBossBattle(MonoBehaviourSingleton<CoopManager>.I.coopStage.GetIsInFieldEnemyBossBattle());
    this.StartCoroutine(this.OnProgressEnd(InGameProgress.PROGRESS_END_TYPE.FIELD_REENTRY));
    return true;
  }

  private IEnumerator OnProgressEnd(InGameProgress.PROGRESS_END_TYPE type)
  {
    this.progressEndType = type;
    if (type == InGameProgress.PROGRESS_END_TYPE.FIELD_RETIRE)
      this.progressEndType = InGameProgress.PROGRESS_END_TYPE.FIELD_TO_HOME;
    if (this.progressEndType == InGameProgress.PROGRESS_END_TYPE.FIELD_REENTRY)
    {
      while (!this.isBattleStart)
        yield return (object) null;
    }
    if (this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_VICTORY && QuestManager.IsValidInGameExplore())
    {
      Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
      if (Object.op_Implicit((Object) boss))
      {
        yield return (object) null;
        MonoBehaviourSingleton<QuestManager>.I.UpdateExploreBossStatus(boss);
        if (MonoBehaviourSingleton<CoopManager>.I.isStageHost)
          MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendExploreBossDead(boss, MonoBehaviourSingleton<QuestManager>.I.GetExplorePlayerStatusList());
      }
      boss = (Enemy) null;
    }
    while (!MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection().isInitialized)
      yield return (object) null;
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
    {
      if (this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_VICTORY || this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_SERIES_INTERVAL || this.progressEndType == InGameProgress.PROGRESS_END_TYPE.RUSH_INTERVAL || this.progressEndType == InGameProgress.PROGRESS_END_TYPE.ARENA_INTERVAL)
      {
        MonoBehaviourSingleton<StageObjectManager>.I.self.hitOffFlag |= StageObject.HIT_OFF_FLAG.FORCE;
      }
      else
      {
        MonoBehaviourSingleton<StageObjectManager>.I.self.hitOffFlag |= StageObject.HIT_OFF_FLAG.FORCE;
        if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self.controller, (Object) null))
          MonoBehaviourSingleton<StageObjectManager>.I.self.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.BATTLE_END);
      }
    }
    if (MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
      MonoBehaviourSingleton<SceneSettingsManager>.I.WeatherForceReturn = true;
    if (this.isHappenQuestDirection)
    {
      this.EndHappenQuestDirection();
      while (this.isHappenQuestDirection)
        yield return (object) null;
    }
    bool needRushIntervalEffects = this.progressEndType == InGameProgress.PROGRESS_END_TYPE.RUSH_INTERVAL && !this.forceComplete;
    bool needArenaIntervalEffects = this.progressEndType == InGameProgress.PROGRESS_END_TYPE.ARENA_INTERVAL && !this.forceComplete;
    if (((this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_VICTORY ? 1 : (this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_SERIES_INTERVAL ? 1 : 0)) | (needRushIntervalEffects ? 1 : 0) | (needArenaIntervalEffects ? 1 : 0)) != 0)
    {
      InGameMain currentScreen = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentScreen() as InGameMain;
      if (Object.op_Inequality((Object) currentScreen, (Object) null))
      {
        Transform ctrl = currentScreen.GetCtrl((Enum) InGameMain.UI.BTN_QUEST_MENU);
        if (Object.op_Inequality((Object) ctrl, (Object) null))
          ((Component) ctrl).gameObject.SetActive(false);
      }
      if (MonoBehaviourSingleton<UIInGameMenu>.IsValid())
        ((Component) MonoBehaviourSingleton<UIInGameMenu>.I).gameObject.SetActive(false);
      if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
        ((Component) MonoBehaviourSingleton<UIEnemyStatus>.I).gameObject.SetActive(false);
      if (MonoBehaviourSingleton<UIInGameSelfAnnounceManager>.IsValid())
        ((Component) MonoBehaviourSingleton<UIInGameSelfAnnounceManager>.I).gameObject.SetActive(false);
      if (MonoBehaviourSingleton<UIQuestRepeat>.IsValid())
        MonoBehaviourSingleton<UIQuestRepeat>.I.OnVictory();
    }
    else
      this.ViewUI(false);
    yield return (object) this.StartCoroutine(this.DoCloseDialog());
    if (MonoBehaviourSingleton<TargetMarkerManager>.IsValid())
      MonoBehaviourSingleton<TargetMarkerManager>.I.showMarker = false;
    if (((this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_VICTORY ? 1 : (this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_SERIES_INTERVAL ? 1 : 0)) | (needRushIntervalEffects ? 1 : 0) | (needArenaIntervalEffects ? 1 : 0)) != 0 && Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.boss, (Object) null) && !MonoBehaviourSingleton<StageObjectManager>.I.boss.isDead)
      MonoBehaviourSingleton<StageObjectManager>.I.boss.ActDead(false, false);
    if ((this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_RETIRE || this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_TIMEUP) && MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null) && MonoBehaviourSingleton<QuestManager>.IsValid() && !MonoBehaviourSingleton<QuestManager>.I.IsTutorialOrderQuest(MonoBehaviourSingleton<QuestManager>.I.currentQuestID))
      MonoBehaviourSingleton<UIManager>.I.mainChat.ShowOpenButton();
    yield return (object) null;
    if (this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_VICTORY || this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_SERIES_INTERVAL || this.progressEndType == InGameProgress.PROGRESS_END_TYPE.RUSH_INTERVAL || this.progressEndType == InGameProgress.PROGRESS_END_TYPE.ARENA_INTERVAL)
    {
      int index = 0;
      for (int count = MonoBehaviourSingleton<StageObjectManager>.I.characterList.Count; index < count; ++index)
        (MonoBehaviourSingleton<StageObjectManager>.I.characterList[index] as Character).hitOffFlag |= StageObject.HIT_OFF_FLAG.FORCE;
    }
    else if (this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_RETIRE || this.progressEndType == InGameProgress.PROGRESS_END_TYPE.FIELD_TO_HOME || this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_INVITEQUIT || this.progressEndType == InGameProgress.PROGRESS_END_TYPE.FIELD_TO_HOME_TIMEOUT)
    {
      this.SetBattleEndCharacter((Character) MonoBehaviourSingleton<StageObjectManager>.I.self);
    }
    else
    {
      this.SetBattleEndAllCharacters();
      this.SetBattleEndAllPlayers();
    }
    while (Object.op_Inequality((Object) this.viewFx, (Object) null))
      yield return (object) null;
    switch (this.progressEndType)
    {
      case InGameProgress.PROGRESS_END_TYPE.QUEST_VICTORY:
      case InGameProgress.PROGRESS_END_TYPE.QUEST_RETIRE:
      case InGameProgress.PROGRESS_END_TYPE.QUEST_RETRY:
      case InGameProgress.PROGRESS_END_TYPE.RUSH_INTERVAL:
        if (this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_VICTORY)
        {
          yield return (object) new WaitForSeconds(1f);
          while (!this.isRecvQuestComplete)
            yield return (object) null;
        }
        if (this.progressEndType == InGameProgress.PROGRESS_END_TYPE.RUSH_INTERVAL)
        {
          yield return (object) new WaitForSeconds(1f);
          while (!this.isRecvRushProgress)
            yield return (object) null;
        }
        if (((this.progressEndType != InGameProgress.PROGRESS_END_TYPE.QUEST_VICTORY ? 0 : (!this.isSendCompleteError ? 1 : 0)) | (needRushIntervalEffects ? 1 : 0)) != 0)
        {
          this.CreateUIEffect(MonoBehaviourSingleton<InGameLinkResourcesQuest>.I.questWin);
          this.PlayAudio(InGameProgress.AUDIO.RESULT_WIN);
          SoundManager.RequestBGM(0);
          MonoBehaviourSingleton<SoundManager>.I.TransitionTo("PreVictory");
          break;
        }
        if (this.progressEndType != InGameProgress.PROGRESS_END_TYPE.RUSH_INTERVAL)
        {
          this.CreateUIEffect(MonoBehaviourSingleton<InGameLinkResourcesQuest>.I.questFaild);
          MonoBehaviourSingleton<SoundManager>.I.TransitionTo("QuestFailed", 0.1f);
          this.PlayAudio(InGameProgress.AUDIO.RESULT_LOSE);
          break;
        }
        break;
      case InGameProgress.PROGRESS_END_TYPE.QUEST_TIMEUP:
        this.CreateUIEffect(MonoBehaviourSingleton<InGameLinkResourcesQuest>.I.questTimeUp);
        MonoBehaviourSingleton<SoundManager>.I.TransitionTo("QuestFailed", 0.1f);
        this.PlayAudio(InGameProgress.AUDIO.RESULT_LOSE);
        while (Object.op_Inequality((Object) this.viewFx, (Object) null))
          yield return (object) null;
        this.CreateUIEffect(MonoBehaviourSingleton<InGameLinkResourcesQuest>.I.questFaild);
        break;
      case InGameProgress.PROGRESS_END_TYPE.FIELD_REENTRY:
        if (MonoBehaviourSingleton<LoungeMatchingManager>.I.isKicked)
        {
          MonoBehaviourSingleton<LoungeMatchingManager>.I.CompleteKick();
          break;
        }
        UIInGamePopupDialog.PushOpen(StringTable.Get(STRING_CATEGORY.IN_GAME, 120U), true);
        break;
      case InGameProgress.PROGRESS_END_TYPE.FIELD_TO_HOME_TIMEOUT:
        UIInGamePopupDialog.PushOpen(StringTable.Get(STRING_CATEGORY.IN_GAME, 130U), true);
        break;
      case InGameProgress.PROGRESS_END_TYPE.ARENA_INTERVAL:
        yield return (object) new WaitForSeconds(1f);
        while (!this.isRecvArenaProgress)
          yield return (object) null;
        if (needArenaIntervalEffects)
        {
          this.CreateUIEffect(MonoBehaviourSingleton<InGameLinkResourcesQuest>.I.questWin);
          this.PlayAudio(InGameProgress.AUDIO.RESULT_WIN);
          SoundManager.RequestBGM(0);
          MonoBehaviourSingleton<SoundManager>.I.TransitionTo("PreVictory");
          break;
        }
        break;
      case InGameProgress.PROGRESS_END_TYPE.QUEST_TO_QUEST_REPEAT:
        if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null) && MonoBehaviourSingleton<QuestManager>.IsValid() && !MonoBehaviourSingleton<QuestManager>.I.IsTutorialOrderQuest(MonoBehaviourSingleton<QuestManager>.I.currentQuestID))
        {
          MonoBehaviourSingleton<UIManager>.I.mainChat.HideOpenButton();
          break;
        }
        break;
    }
    while (Object.op_Inequality((Object) this.viewFx, (Object) null))
      yield return (object) null;
    if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
    {
      while (!MonoBehaviourSingleton<InGameCameraManager>.I.IsEndMotionCamera())
        yield return (object) null;
    }
    if (MonoBehaviourSingleton<UIInGamePopupDialog>.IsValid())
    {
      while (MonoBehaviourSingleton<UIInGamePopupDialog>.I.IsShowingDialog())
        yield return (object) null;
      MonoBehaviourSingleton<UIInGamePopupDialog>.I.SetEnableDialog(false);
    }
    if (MonoBehaviourSingleton<UIManager>.IsValid())
    {
      MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(true);
      yield return (object) new WaitForEndOfFrame();
      GC.Collect();
      MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(false);
      yield return (object) new WaitForEndOfFrame();
    }
    if (this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_VICTORY && !this.isSendCompleteError)
    {
      if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo() || QuestManager.IsValidInGameSeriesArena())
      {
        yield return (object) null;
        this.SetBattleEndAllCharacters();
        this.SetBattleEndAllPlayers();
        this.ViewUI(false);
        SoundManager.RequestBGM(14);
      }
      else
      {
        if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null) && UserInfoManager.IsFinishTutorial() && !MonoBehaviourSingleton<UIManager>.I.mainChat.IsOpeningWindow())
        {
          bool flag = true;
          List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
          if (playerList != null && playerList.Count > 1)
          {
            foreach (StageObject stageObject in playerList)
            {
              if (!Object.op_Equality((Object) stageObject, (Object) null) && !Object.op_Inequality((Object) ((Component) stageObject).GetComponent<SelfController>(), (Object) null) && !Object.op_Inequality((Object) ((Component) stageObject).GetComponent<NpcController>(), (Object) null))
              {
                flag = false;
                break;
              }
            }
          }
          if (!flag)
            MonoBehaviourSingleton<UIManager>.I.mainChat.ShowInputOnly();
        }
        SoundManager.RequestBGM(14);
        this.enableVictoryIntervalTime = true;
        this.startVictoryIntervalTime = Time.time;
        while ((double) this.victoryIntervalTime > 0.0)
          yield return (object) null;
        this.enableVictoryIntervalTime = false;
        this.startVictoryIntervalTime = -1f;
        this.SetBattleEndAllCharacters();
        this.SetBattleEndAllPlayers();
        this.ViewUI(false);
        if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null) && MonoBehaviourSingleton<QuestManager>.IsValid() && !MonoBehaviourSingleton<QuestManager>.I.IsTutorialOrderQuest(MonoBehaviourSingleton<QuestManager>.I.currentQuestID))
          MonoBehaviourSingleton<UIManager>.I.mainChat.ShowOpenButton();
      }
    }
    this.waitNetworkCoroutine = this.StartCoroutine(this.DoWaitNetwork());
    if (this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_VICTORY || this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_RETIRE || this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_INVITEQUIT || this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_TIMEUP || this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_TO_FIELD)
    {
      while (!this.isRecvQuestComplete)
        yield return (object) null;
    }
    if (this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_RETIRE || this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_RETRY || this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_INVITEQUIT || this.progressEndType == InGameProgress.PROGRESS_END_TYPE.FIELD_TO_HOME || this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_TO_FIELD)
    {
      this.SetBattleEndAllCharacters();
      this.SetBattleEndAllPlayers();
    }
    if (this.progressEndType != InGameProgress.PROGRESS_END_TYPE.FIELD_REENTRY)
    {
      float start_time = Time.time;
      while (MonoBehaviourSingleton<KtbWebSocket>.IsValid() && !MonoBehaviourSingleton<KtbWebSocket>.I.IsCompleteSendAll() && (double) Time.time - (double) start_time <= (double) MonoBehaviourSingleton<InGameSettingsManager>.I.inGameProgress.checkCompleteSendTimeout)
        yield return (object) 0;
      if (MonoBehaviourSingleton<KtbWebSocket>.IsValid())
        MonoBehaviourSingleton<KtbWebSocket>.I.LoggingResendPackets("InGameProgressEnd: Timeout removing... ");
    }
    MonoBehaviourSingleton<InGameManager>.I.isQuestResultFieldLeave = false;
    bool do_leave_coop = false;
    bool flag1 = false;
    bool online_stage_change = false;
    bool online_quest_series = false;
    bool transfer_other = false;
    bool keep_dead = false;
    bool chat_switch_to_party = false;
    switch (this.progressEndType)
    {
      case InGameProgress.PROGRESS_END_TYPE.QUEST_VICTORY:
      case InGameProgress.PROGRESS_END_TYPE.QUEST_RETIRE:
      case InGameProgress.PROGRESS_END_TYPE.QUEST_RETRY:
      case InGameProgress.PROGRESS_END_TYPE.QUEST_TIMEUP:
        if (MonoBehaviourSingleton<InGameManager>.I.isQuestHappen && CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
          online_stage_change = true;
        else if (PartyManager.IsValidInParty())
        {
          do_leave_coop = true;
          chat_switch_to_party = true;
        }
        else
          MonoBehaviourSingleton<InGameManager>.I.isQuestResultFieldLeave = true;
        if (MonoBehaviourSingleton<InGameManager>.I.IsQuestInField())
        {
          flag1 = true;
          break;
        }
        break;
      case InGameProgress.PROGRESS_END_TYPE.QUEST_INVITEQUIT:
      case InGameProgress.PROGRESS_END_TYPE.FIELD_TO_HOME:
      case InGameProgress.PROGRESS_END_TYPE.FIELD_TO_HOME_TIMEOUT:
        do_leave_coop = true;
        break;
      case InGameProgress.PROGRESS_END_TYPE.QUEST_SERIES_INTERVAL:
        if (MonoBehaviourSingleton<CoopManager>.IsValid())
        {
          MonoBehaviourSingleton<CoopManager>.I.coopMyClient.SeriesProgress((int) MonoBehaviourSingleton<QuestManager>.I.currentQuestSeriesIndex);
          MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestSeriesIndex(MonoBehaviourSingleton<QuestManager>.I.currentQuestSeriesIndex + 1U);
          while (MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.HasSeriesProgress())
            yield return (object) null;
        }
        if (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
          online_quest_series = true;
        flag1 = true;
        transfer_other = true;
        online_stage_change = true;
        break;
      case InGameProgress.PROGRESS_END_TYPE.FIELD_MAP_INTERVAL:
        do_leave_coop = true;
        flag1 = true;
        break;
      case InGameProgress.PROGRESS_END_TYPE.FIELD_TO_QUEST_INTERVAL:
      case InGameProgress.PROGRESS_END_TYPE.FIELD_TO_STORY:
      case InGameProgress.PROGRESS_END_TYPE.FIELD_READ_STORY:
        if (this.isQuestHappen && CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
          online_stage_change = true;
        else
          do_leave_coop = true;
        flag1 = true;
        break;
      case InGameProgress.PROGRESS_END_TYPE.FIELD_REENTRY:
        do_leave_coop = true;
        flag1 = true;
        keep_dead = true;
        break;
      case InGameProgress.PROGRESS_END_TYPE.FORCE_DEFEAT:
        do_leave_coop = true;
        flag1 = true;
        break;
      case InGameProgress.PROGRESS_END_TYPE.EXPLORE_MOVE_INTERVAL:
      case InGameProgress.PROGRESS_END_TYPE.EXPLORE_HAPPEN_INTERVAL:
        if (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
          online_quest_series = true;
        flag1 = true;
        transfer_other = false;
        online_stage_change = true;
        break;
      case InGameProgress.PROGRESS_END_TYPE.RUSH_INTERVAL:
        if (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
          online_quest_series = true;
        flag1 = true;
        transfer_other = true;
        online_stage_change = true;
        break;
      case InGameProgress.PROGRESS_END_TYPE.ARENA_INTERVAL:
        flag1 = true;
        break;
      case InGameProgress.PROGRESS_END_TYPE.QUEST_TO_FIELD:
        do_leave_coop = true;
        flag1 = true;
        break;
      case InGameProgress.PROGRESS_END_TYPE.QUEST_TO_QUEST_REPEAT:
        if (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
          online_quest_series = true;
        flag1 = true;
        transfer_other = false;
        online_stage_change = true;
        this.isInitStartTime = false;
        break;
    }
    if (flag1 && MonoBehaviourSingleton<InGameManager>.IsValid())
    {
      float remaind_time = this.remaindTime;
      float elapsed_time = this.GetElapsedTime();
      if (MonoBehaviourSingleton<InGameManager>.I.IsRush())
        remaind_time = this.rushRemainTime;
      if (MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
      {
        remaind_time = (float) this.arenaRemainSec;
        elapsed_time = (float) this.arenaElapsedSec + this.GetElapsedTime();
      }
      if (QuestManager.IsValidInGameSeries() || QuestManager.IsValidInGameWaveMatch() || QuestManager.IsValidInGameSeriesArena())
        remaind_time = this.limitTime;
      bool isReentry = this.progressEndType == InGameProgress.PROGRESS_END_TYPE.FIELD_REENTRY;
      bool isQuestToField = MonoBehaviourSingleton<InGameManager>.I.IsQuestInField() || MonoBehaviourSingleton<InGameManager>.I.IsQuestInPortal();
      if (this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_TO_QUEST_REPEAT)
        MonoBehaviourSingleton<InGameManager>.I.SetIntervalTransferInfo(this.enableLimitTime, remaind_time, elapsed_time, transfer_other, keep_dead, isReentry, true);
      else
        MonoBehaviourSingleton<InGameManager>.I.SetIntervalTransferInfo(this.enableLimitTime, remaind_time, elapsed_time, transfer_other, keep_dead, isReentry, isQuestToField);
      if (!this.IsStopTimer())
        MonoBehaviourSingleton<InGameManager>.I.SetEnableIntervalTransferInfoRemaindTimeUpdate();
    }
    CoopStageObjectUtility.SetCoopModeForAll(StageObject.COOP_MODE_TYPE.NONE, 0);
    while (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
      yield return (object) null;
    if (FieldManager.IsValidInGame() && MonoBehaviourSingleton<CoopManager>.IsValid())
    {
      bool wait = true;
      MonoBehaviourSingleton<CoopManager>.I.coopStage.fieldRewardPool.SendFieldDrop((Action<bool>) (b => wait = false));
      while (wait)
        yield return (object) null;
    }
    if (do_leave_coop)
    {
      bool toHome = this.progressEndType == InGameProgress.PROGRESS_END_TYPE.FIELD_TO_HOME || this.progressEndType == InGameProgress.PROGRESS_END_TYPE.FIELD_TO_HOME_TIMEOUT;
      bool fieldRetire = type == InGameProgress.PROGRESS_END_TYPE.FIELD_RETIRE;
      bool wait = true;
      MonoBehaviourSingleton<CoopApp>.I.Leave((Action<bool>) (b => wait = false), toHome, fieldRetire);
      while (wait)
        yield return (object) null;
      if (chat_switch_to_party && PartyManager.IsValidInParty())
        MonoBehaviourSingleton<ChatManager>.I.SwitchRoomChatConnectionToPartyConnection();
    }
    else if (online_stage_change)
    {
      if (MonoBehaviourSingleton<CoopNetworkManager>.IsValid())
        MonoBehaviourSingleton<CoopNetworkManager>.I.packetReceiver.EraseLostReceiverPackets();
    }
    else
    {
      int num = online_quest_series ? 1 : 0;
    }
    this.StopCoroutine(this.waitNetworkCoroutine);
    MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.MANUAL_NETWORK, false);
    if (this.progressEndType != InGameProgress.PROGRESS_END_TYPE.FIELD_REENTRY)
      MonoBehaviourSingleton<InGameManager>.I.disableHappenQuestIdList.Clear();
    MonoBehaviourSingleton<InGameManager>.I.happenQuestStatusList = (List<Coop_Model_EventHappenQuestStatus.Status>) null;
    MonoBehaviourSingleton<InGameManager>.I.isStoryPortal = false;
    switch (this.progressEndType)
    {
      case InGameProgress.PROGRESS_END_TYPE.QUEST_VICTORY:
      case InGameProgress.PROGRESS_END_TYPE.QUEST_RETIRE:
      case InGameProgress.PROGRESS_END_TYPE.QUEST_TIMEUP:
        if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
          MonoBehaviourSingleton<InGameRecorder>.I.OnInGameEnd(this.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_VICTORY && !this.isSendCompleteError);
        this.OverwritePlayerRecorderForExplore(false);
        MonoBehaviourSingleton<SoundManager>.I.TransitionTo("Victory");
        this.ChangeSceneToResult();
        break;
      case InGameProgress.PROGRESS_END_TYPE.QUEST_RETRY:
        this.ReloadScene();
        break;
      case InGameProgress.PROGRESS_END_TYPE.QUEST_INVITEQUIT:
      case InGameProgress.PROGRESS_END_TYPE.FIELD_TO_HOME:
      case InGameProgress.PROGRESS_END_TYPE.FORCE_DEFEAT:
      case InGameProgress.PROGRESS_END_TYPE.FIELD_TO_HOME_TIMEOUT:
        uint completableStoryDelivery = MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableStoryDelivery();
        if (MonoBehaviourSingleton<DeliveryManager>.I.HasClearEventID(completableStoryDelivery))
        {
          this.ChangeSceneToStory(completableStoryDelivery);
          break;
        }
        this.ChangeSceneToHome();
        break;
      case InGameProgress.PROGRESS_END_TYPE.QUEST_SERIES_INTERVAL:
        this.ChangeSceneToInterval();
        break;
      case InGameProgress.PROGRESS_END_TYPE.FIELD_MAP_INTERVAL:
        if (!MonoBehaviourSingleton<FieldManager>.IsValid())
          break;
        MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapPortalID(this.toFieldPortalID);
        MonoBehaviourSingleton<InGameManager>.I.beforePortalID = this.toFieldPortalID;
        this.ChangeSceneToInterval();
        break;
      case InGameProgress.PROGRESS_END_TYPE.FIELD_TO_QUEST_INTERVAL:
        if (!MonoBehaviourSingleton<FieldManager>.IsValid())
          break;
        MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(this.toQuestID);
        MonoBehaviourSingleton<InGameManager>.I.isTransitionFieldToQuest = true;
        MonoBehaviourSingleton<InGameManager>.I.isQuestGate = this.isQuestGate;
        MonoBehaviourSingleton<InGameManager>.I.isQuestPortal = this.isQuestPortal;
        MonoBehaviourSingleton<InGameManager>.I.isQuestHappen = this.isQuestHappen;
        MonoBehaviourSingleton<InGameManager>.I.isQuestFromGimmick = this.isQuestFromGimmick;
        FieldManager.FieldTransitionInfo fieldTransitionInfo1 = new FieldManager.FieldTransitionInfo();
        fieldTransitionInfo1.portalID = MonoBehaviourSingleton<FieldManager>.I.currentPortalID;
        fieldTransitionInfo1.mapID = MonoBehaviourSingleton<FieldManager>.I.currentMapID;
        Self self1 = MonoBehaviourSingleton<StageObjectManager>.I.self;
        if (Object.op_Inequality((Object) self1, (Object) null))
        {
          fieldTransitionInfo1.mapX = self1._position.x;
          fieldTransitionInfo1.mapZ = self1._position.z;
          FieldManager.FieldTransitionInfo fieldTransitionInfo2 = fieldTransitionInfo1;
          Quaternion rotation = self1._rotation;
          double y = (double) ((Quaternion) ref rotation).eulerAngles.y;
          fieldTransitionInfo2.mapDir = (float) y;
        }
        MonoBehaviourSingleton<InGameManager>.I.backTransitionInfo = fieldTransitionInfo1;
        MonoBehaviourSingleton<InGameManager>.I.beforePortalID = this.toQuestPortalID;
        this.ChangeSceneToInterval();
        break;
      case InGameProgress.PROGRESS_END_TYPE.FIELD_REENTRY:
        if (InGameManager.IsReentryMapId())
        {
          Self self2 = MonoBehaviourSingleton<StageObjectManager>.I.self;
          if (Object.op_Inequality((Object) self2, (Object) null))
          {
            FieldManager i = MonoBehaviourSingleton<FieldManager>.I;
            int currentMapId = (int) MonoBehaviourSingleton<FieldManager>.I.currentMapID;
            double x = (double) self2._position.x;
            double z = (double) self2._position.z;
            Quaternion rotation = self2._rotation;
            double y = (double) ((Quaternion) ref rotation).eulerAngles.y;
            i.SetCurrentFieldMapID((uint) currentMapId, (float) x, (float) z, (float) y);
          }
          MonoBehaviourSingleton<InGameManager>.I.isTransitionFieldReentry = true;
          this.ChangeSceneToInterval();
          break;
        }
        if (!MonoBehaviourSingleton<FieldManager>.IsValid())
          break;
        Self self3 = MonoBehaviourSingleton<StageObjectManager>.I.self;
        if (Object.op_Inequality((Object) self3, (Object) null))
        {
          FieldManager i = MonoBehaviourSingleton<FieldManager>.I;
          int currentPortalId = (int) MonoBehaviourSingleton<FieldManager>.I.currentPortalID;
          double x = (double) self3._position.x;
          double z = (double) self3._position.z;
          Quaternion rotation = self3._rotation;
          double y = (double) ((Quaternion) ref rotation).eulerAngles.y;
          i.SetCurrentFieldMapPortalID((uint) currentPortalId, (float) x, (float) z, (float) y);
        }
        MonoBehaviourSingleton<InGameManager>.I.isTransitionFieldReentry = true;
        MonoBehaviourSingleton<InGameManager>.I.beforePortalID = MonoBehaviourSingleton<FieldManager>.I.currentPortalID;
        this.ChangeSceneToInterval();
        break;
      case InGameProgress.PROGRESS_END_TYPE.FIELD_TO_STORY:
        if (!MonoBehaviourSingleton<FieldManager>.IsValid())
          break;
        MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(this.toQuestID);
        MonoBehaviourSingleton<InGameManager>.I.isTransitionFieldToQuest = true;
        MonoBehaviourSingleton<InGameManager>.I.isQuestGate = this.isQuestGate;
        MonoBehaviourSingleton<InGameManager>.I.isQuestPortal = this.isQuestPortal;
        MonoBehaviourSingleton<InGameManager>.I.isQuestHappen = this.isQuestHappen;
        MonoBehaviourSingleton<InGameManager>.I.isQuestFromGimmick = this.isQuestFromGimmick;
        MonoBehaviourSingleton<InGameManager>.I.isStoryPortal = true;
        FieldManager.FieldTransitionInfo fieldTransitionInfo3 = new FieldManager.FieldTransitionInfo();
        fieldTransitionInfo3.portalID = MonoBehaviourSingleton<FieldManager>.I.currentPortalID;
        fieldTransitionInfo3.mapID = MonoBehaviourSingleton<FieldManager>.I.currentMapID;
        Self self4 = MonoBehaviourSingleton<StageObjectManager>.I.self;
        if (Object.op_Inequality((Object) self4, (Object) null))
        {
          fieldTransitionInfo3.mapX = self4._position.x;
          fieldTransitionInfo3.mapZ = self4._position.z;
          FieldManager.FieldTransitionInfo fieldTransitionInfo4 = fieldTransitionInfo3;
          Quaternion rotation = self4._rotation;
          double y = (double) ((Quaternion) ref rotation).eulerAngles.y;
          fieldTransitionInfo4.mapDir = (float) y;
        }
        MonoBehaviourSingleton<InGameManager>.I.backTransitionInfo = fieldTransitionInfo3;
        MonoBehaviourSingleton<InGameManager>.I.beforePortalID = this.toQuestPortalID;
        this.ChangeSceneToFieldQuestStory();
        break;
      case InGameProgress.PROGRESS_END_TYPE.EXPLORE_MOVE_INTERVAL:
        if (MonoBehaviourSingleton<QuestManager>.I.IsBossAppearMap((int) Singleton<FieldMapTable>.I.GetPortalData(this.toFieldPortalID).dstMapID))
          MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapID((uint) MonoBehaviourSingleton<QuestManager>.I.GetExploreBossBatlleMapId(), 0.0f, 0.0f, 0.0f);
        else
          MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapPortalID(this.toFieldPortalID);
        MonoBehaviourSingleton<InGameManager>.I.beforePortalID = this.toFieldPortalID;
        this.ChangeSceneToInterval();
        break;
      case InGameProgress.PROGRESS_END_TYPE.EXPLORE_HAPPEN_INTERVAL:
        MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapID((uint) MonoBehaviourSingleton<QuestManager>.I.GetExploreBossBatlleMapId(), 0.0f, 0.0f, 0.0f);
        MonoBehaviourSingleton<InGameManager>.I.beforePortalID = this.toFieldPortalID;
        this.ChangeSceneToInterval();
        break;
      case InGameProgress.PROGRESS_END_TYPE.RUSH_INTERVAL:
        if (MonoBehaviourSingleton<CoopManager>.I.isStageHost)
          MonoBehaviourSingleton<CoopManager>.I.coopStage.SendSyncPlayerRecord(0, false);
        MonoBehaviourSingleton<InGameManager>.I.ProgressRush();
        MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(MonoBehaviourSingleton<InGameManager>.I.GetCurrentRushQuestId());
        this.ChangeSceneToInterval();
        break;
      case InGameProgress.PROGRESS_END_TYPE.ARENA_INTERVAL:
        MonoBehaviourSingleton<InGameManager>.I.ProgressArena();
        MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(MonoBehaviourSingleton<InGameManager>.I.GetCurrentArenaQuestId());
        this.ChangeSceneToInterval();
        break;
      case InGameProgress.PROGRESS_END_TYPE.FIELD_READ_STORY:
        if (!MonoBehaviourSingleton<FieldManager>.IsValid())
          break;
        MonoBehaviourSingleton<InGameManager>.I.isTransitionQuestToField = true;
        MonoBehaviourSingleton<InGameManager>.I.isQuestGate = false;
        MonoBehaviourSingleton<InGameManager>.I.isQuestHappen = true;
        MonoBehaviourSingleton<InGameManager>.I.isQuestFromGimmick = false;
        MonoBehaviourSingleton<InGameManager>.I.readStoryID = this.isFieldReadStorySend ? this.fieldReadStoryId : 0;
        MonoBehaviourSingleton<InGameManager>.I.requestEventData = this.fieldReadStoryRequestEvent;
        FieldManager.FieldTransitionInfo fieldTransitionInfo5 = new FieldManager.FieldTransitionInfo();
        fieldTransitionInfo5.portalID = MonoBehaviourSingleton<FieldManager>.I.currentPortalID;
        fieldTransitionInfo5.mapID = MonoBehaviourSingleton<FieldManager>.I.currentMapID;
        Self self5 = MonoBehaviourSingleton<StageObjectManager>.I.self;
        if (Object.op_Inequality((Object) self5, (Object) null))
        {
          fieldTransitionInfo5.mapX = self5._position.x;
          fieldTransitionInfo5.mapZ = self5._position.z;
          FieldManager.FieldTransitionInfo fieldTransitionInfo6 = fieldTransitionInfo5;
          Quaternion rotation = self5._rotation;
          double y = (double) ((Quaternion) ref rotation).eulerAngles.y;
          fieldTransitionInfo6.mapDir = (float) y;
        }
        MonoBehaviourSingleton<InGameManager>.I.backTransitionInfo = fieldTransitionInfo5;
        this.ChangeSceneFieldReadStory();
        break;
      case InGameProgress.PROGRESS_END_TYPE.QUEST_TO_FIELD:
        MonoBehaviourSingleton<FieldManager>.I.SetCurrentFieldMapPortalID(this.toFieldPortalID);
        MonoBehaviourSingleton<InGameManager>.I.beforePortalID = this.toFieldPortalID;
        this.ChangeSceneToInterval();
        break;
      case InGameProgress.PROGRESS_END_TYPE.QUEST_TO_QUEST_REPEAT:
        MonoBehaviourSingleton<InGameManager>.I.isTransitionQuestToQuest = true;
        this.ChangeSceneToInterval();
        break;
    }
  }

  private IEnumerator DoWaitNetwork()
  {
    yield return (object) new WaitForSeconds(MonoBehaviourSingleton<InGameSettingsManager>.I.inGameProgress.waitNetworkMarginTime);
    MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.MANUAL_NETWORK, true);
  }

  public void CreatePortalPoint(FieldMapPortalInfo portal_info, Coop_Model_EnemyDefeat model)
  {
    if (model.ppt <= 0 || !FieldManager.IsValidInGameNoBoss() || !MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    int index = 0;
    for (int count = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList.Count; index < count; ++index)
    {
      PortalObject portalObject = MonoBehaviourSingleton<InGameProgress>.I.portalObjectList[index];
      if ((int) portalObject.portalID == (int) portal_info.portalData.portalID)
        PortalPointEffect.Create(portalObject, model);
      else
        portalObject.UpdateView();
    }
  }

  public void RealizesLinkResourcesFieldEnemyBoss(System.Action callback)
  {
    this.StartCoroutine(this._RealizesLinkResourcesFieldEnemyBoss(callback));
  }

  public IEnumerator _RealizesLinkResourcesFieldEnemyBoss(System.Action callback)
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject linkObj = loadingQueue.Load(RESOURCE_CATEGORY.SYSTEM, "SystemInGameLinkResources", new string[1]
    {
      "InGameLinkResourcesFieldEnemyBoss"
    });
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    foreach (ResourceObject loadedObject in linkObj.loadedObjects)
      ResourceUtility.Realizes(loadedObject.obj, MonoBehaviourSingleton<InGameSettingsManager>.I._transform);
    callback.SafeInvoke();
  }

  public void PlayFieldEnemyBossVictoryEffect()
  {
    if (!MonoBehaviourSingleton<InGameLinkResourcesFieldEnemyBoss>.IsValid())
      return;
    this.CreateUIEffect(MonoBehaviourSingleton<InGameLinkResourcesFieldEnemyBoss>.I.questWin);
    this.PlayAudio(InGameProgress.AUDIO.RESULT_WIN);
  }

  public void PlayFieldEnemyBossTimesUpEffect()
  {
    if (!MonoBehaviourSingleton<InGameLinkResourcesFieldEnemyBoss>.IsValid())
      return;
    this.CreateUIEffect(MonoBehaviourSingleton<InGameLinkResourcesFieldEnemyBoss>.I.questTimeUp);
    this.PlayAudio(InGameProgress.AUDIO.RESULT_LOSE);
  }

  private void CreateUIEffect(string effect_name)
  {
    if (Object.op_Inequality((Object) this.viewFx, (Object) null))
      Object.DestroyImmediate((Object) ((Component) this.viewFx).gameObject);
    this.viewFx = EffectManager.GetUIEffect(effect_name);
    if (Object.op_Equality((Object) this.viewFx, (Object) null))
      return;
    ((Component) this.viewFx).gameObject.AddComponent<DisableNotifyMonoBehaviour>().SetNotifyMaster((DisableNotifyMonoBehaviour) this);
  }

  private void CreateUIEffect(GameObject obj)
  {
    if (Object.op_Inequality((Object) this.viewFx, (Object) null))
      Object.DestroyImmediate((Object) ((Component) this.viewFx).gameObject);
    this.viewFx = ResourceUtility.Realizes((Object) obj, MonoBehaviourSingleton<GameSceneManager>.I.GetLastSectionExcludeCommonDialog()._transform);
    if (Object.op_Equality((Object) this.viewFx, (Object) null))
      return;
    ((Component) this.viewFx).gameObject.AddComponent<DisableNotifyMonoBehaviour>().SetNotifyMaster((DisableNotifyMonoBehaviour) this);
  }

  protected override void OnDetachServant(DisableNotifyMonoBehaviour servant)
  {
    base.OnDetachServant(servant);
    if (!Object.op_Equality((Object) ((Component) this.viewFx).gameObject, (Object) ((Component) servant).gameObject))
      return;
    this.viewFx = (Transform) null;
  }

  private void ViewUI(bool enable)
  {
    Transform transform = MonoBehaviourSingleton<UIManager>.I.Find("InGameMain");
    if (Object.op_Inequality((Object) transform, (Object) null))
    {
      ((Component) transform).GetComponent<UIBehaviour>().uiVisible = enable;
      if (enable)
      {
        UIRect[] componentsInChildren = ((Component) transform).GetComponentsInChildren<UIRect>();
        int index = 0;
        for (int length = componentsInChildren.Length; index < length; ++index)
          componentsInChildren[index].UpdateAnchors();
      }
    }
    if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null))
    {
      if (enable)
        MonoBehaviourSingleton<UIManager>.I.mainChat.Open();
      else
        MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
    }
    if (!MonoBehaviourSingleton<UIManager>.IsValid() || !Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.invitationInGameButton, (Object) null))
      return;
    if (enable && MonoBehaviourSingleton<UserInfoManager>.I.ExistsPartyInvite)
      MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Open();
    else
      MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.Close();
  }

  private bool IsValidPlayer()
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return false;
    int index = 0;
    for (int count = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count; index < count; ++index)
    {
      if (!(MonoBehaviourSingleton<StageObjectManager>.I.playerList[index] as Player).isDead)
        return true;
    }
    return false;
  }

  private bool IsValidEnemy()
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return false;
    if ((QuestManager.IsValidInGameSeries() || QuestManager.IsValidInGameSeriesArena()) && !MonoBehaviourSingleton<QuestManager>.I.IsOverCurrentQuestSeries())
      return true;
    if (FieldManager.IsValidInGame() && MonoBehaviourSingleton<CoopManager>.IsValid())
    {
      Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
      if (Object.op_Inequality((Object) boss, (Object) null))
      {
        if (boss.isDead)
          return false;
      }
      else if (MonoBehaviourSingleton<CoopManager>.I.coopStage.isEnemyExtermination)
        return false;
    }
    else
    {
      Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
      if (Object.op_Equality((Object) boss, (Object) null) || boss.isDead)
        return false;
    }
    return true;
  }

  private void SetBattleEndCharacter(Character character)
  {
    if (Object.op_Equality((Object) character, (Object) null))
      return;
    character.hitOffFlag |= StageObject.HIT_OFF_FLAG.FORCE;
    if (Object.op_Inequality((Object) character.controller, (Object) null))
      character.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.BATTLE_END);
    if (Object.op_Inequality((Object) character.packetSender, (Object) null))
      character.packetSender.enableSend = false;
    if (!Object.op_Inequality((Object) character.packetReceiver, (Object) null))
      return;
    character.packetReceiver.SetStopPacketUpdate(true);
  }

  private void SetBattleEndAllCharacters()
  {
    int index = 0;
    for (int count = MonoBehaviourSingleton<StageObjectManager>.I.characterList.Count; index < count; ++index)
      this.SetBattleEndCharacter(MonoBehaviourSingleton<StageObjectManager>.I.characterList[index] as Character);
  }

  private void SetBattleEndAllPlayer(Player player)
  {
    if (Object.op_Equality((Object) player, (Object) null))
      return;
    player.ClearStoreEffect();
  }

  private void SetBattleEndAllPlayers()
  {
    for (int index = 0; index < MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count; ++index)
      this.SetBattleEndAllPlayer(MonoBehaviourSingleton<StageObjectManager>.I.playerList[index] as Player);
  }

  public bool DeliveryAddCheck()
  {
    if (!MonoBehaviourSingleton<DeliveryManager>.IsValid() || MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtInGame.Count <= 0 || this.isHappenQuestDirection)
      return false;
    this.StartCoroutine(this.WaitQuestDetail());
    return true;
  }

  private IEnumerator WaitQuestDetail()
  {
    while (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
      yield return (object) null;
    while (GameSceneEvent.current.eventName == "PORTAL_RELEASE")
    {
      this.isRewardToPortalRelease = true;
      yield return (object) null;
    }
    int user_data = MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtInGame[0];
    MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtInGame.RemoveAt(0);
    MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(nameof (InGameProgress), ((Component) this).gameObject, "QUEST_DETAIL", (object) user_data);
    while (GameSceneEvent.current.eventName == "QUEST_DETAIL")
      yield return (object) null;
    if (!this.DeliveryAddCheck() && this.isRewardToPortalRelease)
    {
      this.isRewardToPortalRelease = false;
      MonoBehaviourSingleton<DeliveryManager>.I.CheckAnnouncePortalOpen();
    }
  }

  private void SendComplete() => this.StartCoroutine(this.DoSendComplete());

  private IEnumerator DoSendComplete()
  {
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
    {
      this.isRecvQuestComplete = false;
      yield return (object) null;
      if (QuestManager.IsValidInGameExplore())
      {
        Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
        if (Object.op_Implicit((Object) boss))
          MonoBehaviourSingleton<QuestManager>.I.UpdateExploreBossStatus(boss);
      }
      while (!MonoBehaviourSingleton<QuestManager>.I.IsUnLockedTimeForCompleteSend())
        yield return (object) null;
      float start_time = Time.time;
      while (!MonoBehaviourSingleton<CoopManager>.I.coopRoom.IsValidBattleComplete() && (double) Time.time - (double) start_time < (double) MonoBehaviourSingleton<InGameSettingsManager>.I.inGameProgress.waitCompleteOwnerTimeout)
        yield return (object) null;
      while (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
        yield return (object) null;
      int status = 0;
      ClearStatusQuest clearStatusQuest = MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.Find((Predicate<ClearStatusQuest>) (data => (long) data.questId == (long) MonoBehaviourSingleton<QuestManager>.I.currentQuestID));
      if (clearStatusQuest != null)
        status = clearStatusQuest.questStatus;
      this.OverwritePlayerRecorderForExplore(true);
      CoopApp.QuestComplete((Action<bool, Error>) ((is_success, result) =>
      {
        this.isRecvQuestComplete = true;
        MonoBehaviourSingleton<CoopManager>.I.coopMyClient.EndBattle();
        if (is_success)
          MonoBehaviourSingleton<QuestManager>.I.SaveLastNewClearQuest(status);
        else
          this.isSendCompleteError = true;
      }));
    }
  }

  private void SendArenaComplete() => this.StartCoroutine(this.DoSendArenaComplete());

  private IEnumerator DoSendArenaComplete()
  {
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
    {
      this.isRecvQuestComplete = false;
      yield return (object) null;
      while (!MonoBehaviourSingleton<QuestManager>.I.IsUnLockedTimeForCompleteSend())
        yield return (object) null;
      float startTime = Time.time;
      while (!MonoBehaviourSingleton<CoopManager>.I.coopRoom.IsValidBattleComplete() && (double) Time.time - (double) startTime < (double) MonoBehaviourSingleton<InGameSettingsManager>.I.inGameProgress.waitCompleteOwnerTimeout)
        yield return (object) null;
      while (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
        yield return (object) null;
      int status = 0;
      ClearStatusQuest clearStatusQuest = MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.Find((Predicate<ClearStatusQuest>) (data => (long) data.questId == (long) MonoBehaviourSingleton<QuestManager>.I.currentQuestID));
      if (clearStatusQuest != null)
        status = clearStatusQuest.questStatus;
      CoopApp.ArenaComplete((Action<bool, Error>) ((isSuccess, result) =>
      {
        this.isRecvQuestComplete = true;
        if (isSuccess)
          MonoBehaviourSingleton<QuestManager>.I.SaveLastNewClearQuest(status);
        else
          this.isSendCompleteError = true;
      }));
    }
  }

  private void SendArenaRetire() => this.StartCoroutine(this.DoSendArenaRetire());

  private IEnumerator DoSendArenaRetire()
  {
    this.isRecvQuestComplete = false;
    while (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
      yield return (object) null;
    CoopApp.ArenaRetire((double) this.remaindTime <= 0.0, (Action<bool>) (b => this.isRecvQuestComplete = true));
  }

  private void SendRetire() => this.StartCoroutine(this.DoSendRetire());

  private IEnumerator DoSendRetire()
  {
    this.isRecvQuestComplete = false;
    while (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
      yield return (object) null;
    CoopApp.QuestRetire((double) this.remaindTime <= 0.0, (Action<bool>) (b => this.isRecvQuestComplete = true));
  }

  private void SendRushProgress() => this.StartCoroutine(this.DoSendRushProgress());

  private IEnumerator DoSendRushProgress()
  {
    yield return (object) null;
    int wave = MonoBehaviourSingleton<InGameManager>.I.GetCurrentWaveNum();
    int remainSec = (int) this.remaindTime;
    List<int> breakIds = MonoBehaviourSingleton<CoopManager>.I.coopStage.bossBreakIDLists[0];
    List<int> clearMissions = this.GetMissionClearStatuses();
    float hpRate = MonoBehaviourSingleton<CoopManager>.I.coopStage.bossStartHpDamageRate;
    List<QuestCompleteModel.BattleUserLog> logs = MonoBehaviourSingleton<CoopManager>.I.coopStage.battleUserLog.list;
    List<int> memIds = (List<int>) null;
    if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
      memIds = MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.GetUserIdList(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id);
    this.isRecvRushProgress = false;
    MonoBehaviourSingleton<InGameManager>.I.RecordRushWaveSyncData();
    yield return (object) null;
    MonoBehaviourSingleton<QuestManager>.I.SendQuestRushProgress(wave, remainSec, breakIds, clearMissions, memIds, hpRate, logs, (Action<bool, Error>) ((b, e) =>
    {
      this.isRecvRushProgress = true;
      if (this.progressEndType != InGameProgress.PROGRESS_END_TYPE.QUEST_VICTORY)
        return;
      this.SendComplete();
    }));
  }

  private void SendArenaProgress() => this.StartCoroutine(this.DoSendArenaProgress());

  private IEnumerator DoSendArenaProgress()
  {
    yield return (object) null;
    ArenaProgressModel.RequestSendForm requestData = new ArenaProgressModel.RequestSendForm();
    requestData.wave = MonoBehaviourSingleton<InGameManager>.I.GetCurrentArenaWaveNum();
    requestData.remainMilliSec = (XorInt) Mathf.FloorToInt(this.remaindTime * 1000f);
    requestData.elapseMilliSec = (XorInt) Mathf.FloorToInt(this.GetElapsedTime() * 1000f);
    requestData.breakIds = MonoBehaviourSingleton<CoopManager>.I.coopStage.bossBreakIDLists[0];
    requestData.logs = MonoBehaviourSingleton<CoopManager>.I.coopStage.battleUserLog.list;
    requestData.enemyHp = MonoBehaviourSingleton<InGameRecorder>.I.GetTotalEnemyHP();
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
    {
      requestData.actioncount = MonoBehaviourSingleton<StageObjectManager>.I.self.taskChecker.GetTaskCount();
      MonoBehaviourSingleton<StageObjectManager>.I.self.taskChecker.Clear();
    }
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
    {
      requestData.deliveryBattleInfo = MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.GetInfo();
      MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.ClearInfo();
    }
    this.isRecvArenaProgress = false;
    yield return (object) null;
    MonoBehaviourSingleton<QuestManager>.I.SendQuestArenaProgress(requestData, (Action<bool, Error>) ((b, e) =>
    {
      this.isRecvArenaProgress = true;
      if (this.progressEndType != InGameProgress.PROGRESS_END_TYPE.QUEST_VICTORY)
        return;
      this.SendArenaComplete();
    }));
  }

  public void OnDamage(AttackedHitStatusFix status, Character chara)
  {
    this.missionCheck.ForEach((Action<MissionCheckBase>) (mission => mission.OnDamage(status, chara)));
  }

  public void OnSkillUse(SkillInfo.SkillParam param)
  {
    this.missionCheck.ForEach((Action<MissionCheckBase>) (mission => mission.OnSkillUse(param)));
  }

  private void ChangeSceneToInterval() => this.StartCoroutine(this.DoChangeSceneToInterval());

  private IEnumerator DoChangeSceneToInterval()
  {
    if (MonoBehaviourSingleton<UIManager>.IsValid())
    {
      MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(true);
      yield return (object) new WaitForEndOfFrame();
    }
    yield return (object) this.StartCoroutine(this.DoCloseDialog());
    yield return (object) MonoBehaviourSingleton<AppMain>.I.ClearEnemyAssets();
    if (MonoBehaviourSingleton<UIManager>.IsValid())
    {
      yield return (object) new WaitForEndOfFrame();
      MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(false);
      yield return (object) new WaitForEndOfFrame();
    }
    this.isGameProgressStop = false;
    MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(nameof (InGameProgress), ((Component) this).gameObject, "INTERVAL");
  }

  private void ChangeSceneToFieldQuestStory()
  {
    MonoBehaviourSingleton<InGameManager>.I.SaveQuestTransferInfo();
    this.StartCoroutine(this.DoChangeSceneToFieldQuestStory());
  }

  private IEnumerator DoChangeSceneToFieldQuestStory()
  {
    yield return (object) this.StartCoroutine(this.DoCloseDialog());
    MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(nameof (InGameProgress), ((Component) this).gameObject, "STORY", (object) new object[3]
    {
      (object) Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID).storyId,
      (object) 0,
      (object) 0
    });
  }

  private void ChangeSceneToResult() => this.StartCoroutine(this.DoChangeSceneToResult());

  private IEnumerator DoChangeSceneToResult()
  {
    MonoBehaviourSingleton<UIManager>.I.loading.SetActiveDragon(true);
    yield return (object) new WaitForEndOfFrame();
    yield return (object) this.StartCoroutine(this.DoCloseDialog());
    string event_name = !MonoBehaviourSingleton<InGameRecorder>.IsValid() || !MonoBehaviourSingleton<InGameRecorder>.I.isVictory ? "FRIEND" : "RESULT";
    if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.IsRush())
      event_name = MonoBehaviourSingleton<InGameManager>.I.GetRushIndex() != 0 ? (!MonoBehaviourSingleton<DeliveryManager>.I.IsCarnivalEvent(MonoBehaviourSingleton<QuestManager>.I.currentQuestData.eventId) ? "RUSH_RESULT" : "CARNIVAL_RESULT") : "FRIEND";
    if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
      event_name = "ARENA_RESULT";
    if (QuestManager.IsValidInGameSeriesArena())
      event_name = "SERIES_ARENA_RESULT";
    if (QuestManager.IsValidInGameWaveMatch(true))
      event_name = !MonoBehaviourSingleton<DeliveryManager>.I.IsCarnivalEvent(MonoBehaviourSingleton<QuestManager>.I.currentQuestData.eventId) ? "WAVE_RESULT" : "CARNIVAL_RESULT";
    if (QuestManager.IsValidInGameTrial())
      event_name = !MonoBehaviourSingleton<InGameRecorder>.IsValid() || !MonoBehaviourSingleton<InGameRecorder>.I.isVictory ? "TRIAL_RETIRE" : "TRIAL_RESULT";
    this.CleanupInGame();
    MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(nameof (InGameProgress), ((Component) this).gameObject, event_name);
  }

  private void CleanupInGame()
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid() || MonoBehaviourSingleton<StageObjectManager>.I.playerList.IsNullOrEmpty<StageObject>())
      return;
    int index = 0;
    for (int count = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count; index < count; ++index)
    {
      Player player = MonoBehaviourSingleton<StageObjectManager>.I.playerList[index] as Player;
      if (!Object.op_Equality((Object) player, (Object) null) && player.buffParam != null && player.buffParam.substituteCtrl != null)
        player.buffParam.substituteCtrl.End();
    }
  }

  public void ReloadScene() => this.StartCoroutine(this.DoReloadScene());

  private IEnumerator DoReloadScene()
  {
    yield return (object) this.StartCoroutine(this.DoCloseDialog());
    this.isBattleStart = false;
    this.progressEndType = InGameProgress.PROGRESS_END_TYPE.NONE;
    this.isGameProgressStop = false;
    this.isHappenQuestDirection = false;
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      MonoBehaviourSingleton<CoopManager>.I.Clear();
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
      MonoBehaviourSingleton<InGameManager>.I.isRetry = true;
    MonoBehaviourSingleton<GameSceneManager>.I.ReloadScene();
  }

  private void ChangeSceneToHome() => this.StartCoroutine(this.DoChangeSceneToHome());

  private IEnumerator DoChangeSceneToHome()
  {
    yield return (object) this.StartCoroutine(this.DoCloseDialog());
    MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(nameof (InGameProgress), ((Component) this).gameObject, "HOME");
  }

  private void ChangeSceneToStory(uint deliveryID)
  {
    this.StartCoroutine(this.DoChangeSceneToStory(deliveryID));
  }

  private IEnumerator DoChangeSceneToStory(uint deliveryID)
  {
    yield return (object) this.StartCoroutine(this.DoCloseDialog());
    DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData(deliveryID);
    Delivery delivery = Array.Find<Delivery>(MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList(), (Predicate<Delivery>) (o => o.dId == (int) deliveryID));
    if (delivery == null)
    {
      this.ChangeSceneToHome();
    }
    else
    {
      int clearEventID = (int) deliveryTableData.clearEventID;
      bool enable_clear_event = clearEventID != 0;
      bool is_tutorial = !TutorialStep.HasFirstDeliveryCompleted();
      MonoBehaviourSingleton<DeliveryManager>.I.SendDeliveryComplete(delivery.uId, enable_clear_event, (Action<bool, DeliveryRewardList>) ((is_success, recv_reward) =>
      {
        if (is_success)
        {
          List<FieldMapTable.PortalTableData> relationPortalData = Singleton<FieldMapTable>.I.GetDeliveryRelationPortalData(deliveryID);
          for (int index = 0; index < relationPortalData.Count; ++index)
            GameSaveData.instance.newReleasePortals.Add(relationPortalData[index].portalID);
          if (is_tutorial)
            TutorialStep.isSendFirstRewardComplete = true;
          if (is_tutorial && clearEventID == 10000002)
            MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
            {
              new EventData("MAIN_MENU_HOME", (object) null),
              new EventData("DELIVERY_CLEAR_REWARD", (object) new object[2]
              {
                (object) (int) deliveryID,
                (object) recv_reward
              })
            });
          else
            MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(nameof (InGameProgress), ((Component) this).gameObject, "STORY", (object) new object[3]
            {
              (object) clearEventID,
              (object) (int) deliveryID,
              (object) recv_reward
            });
          MonoBehaviourSingleton<DeliveryManager>.I.noticeNewDeliveryAtInGame.Clear();
        }
        else
          this.ChangeSceneToHome();
      }));
    }
  }

  private void ChangeSceneFieldReadStory()
  {
    MonoBehaviourSingleton<InGameManager>.I.SaveQuestTransferInfo();
    this.StartCoroutine(this.DoChangeSceneFieldReadStory(this.fieldReadStoryId));
  }

  private IEnumerator DoChangeSceneFieldReadStory(int storyId)
  {
    yield return (object) this.StartCoroutine(this.DoCloseDialog());
    MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(nameof (InGameProgress), ((Component) this).gameObject, "STORY", (object) new object[3]
    {
      (object) storyId,
      (object) 0,
      (object) 0
    });
  }

  public void CloseDialog() => this.StartCoroutine(this.DoCloseDialog());

  private IEnumerator DoCloseDialog()
  {
    if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null) && MonoBehaviourSingleton<UIManager>.I.mainChat.IsOpeningWindow())
      MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
    if (MonoBehaviourSingleton<UIInGameMenu>.IsValid())
      MonoBehaviourSingleton<UIInGameMenu>.I.Close();
    yield return (object) this.StartCoroutine(this._DoCloseDialog());
  }

  private IEnumerator _DoCloseDialog()
  {
    while (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
      yield return (object) null;
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionType().IsDialog())
    {
      MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(nameof (InGameProgress), ((Component) this).gameObject, "[BACK]");
      yield return (object) this.StartCoroutine(this._DoCloseDialog());
    }
  }

  public void SetDisableUIOpen(bool disable)
  {
    if (UIInGameFieldMenu.IsValid())
    {
      UIInGameFieldMenu.I.SetDisableButtons(disable);
      InGameMain currentScreen = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentScreen() as InGameMain;
      if (Object.op_Inequality((Object) currentScreen, (Object) null))
        currentScreen.SetMapButtonState();
    }
    if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
      MonoBehaviourSingleton<UIPlayerStatus>.I.SetDisableButtons(disable);
    if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
      MonoBehaviourSingleton<UIEnduranceStatus>.I.SetDisableButtons(disable);
    if (MonoBehaviourSingleton<UIContinueButton>.IsValid())
      MonoBehaviourSingleton<UIContinueButton>.I.SetDisableButtons(disable);
    if (MonoBehaviourSingleton<UIManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.invitationInGameButton, (Object) null))
      MonoBehaviourSingleton<UIManager>.I.invitationInGameButton.SetDisableButton(disable);
    InGameMain currentScreen1 = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentScreen() as InGameMain;
    if (!Object.op_Inequality((Object) currentScreen1, (Object) null))
      return;
    Transform ctrl1 = currentScreen1.GetCtrl((Enum) InGameMain.UI.BTN_CHAT);
    if (Object.op_Inequality((Object) ctrl1, (Object) null))
    {
      UIButton component = ((Component) ctrl1).GetComponent<UIButton>();
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        component.isEnabled = !disable;
        if (!UserInfoManager.IsFinishTutorial())
          component.isEnabled = false;
      }
    }
    Transform ctrl2 = currentScreen1.GetCtrl((Enum) InGameMain.UI.BTN_QUEST_MENU);
    if (!Object.op_Inequality((Object) ctrl2, (Object) null))
      return;
    UIButton component1 = ((Component) ctrl2).GetComponent<UIButton>();
    if (!Object.op_Inequality((Object) component1, (Object) null))
      return;
    component1.isEnabled = !disable;
  }

  public void StartTimer(float elapsed_time = 0.0f)
  {
    if (this.isInitStartTime || !this.enableLimitTime)
      return;
    this.isInitStartTime = true;
    this.elapsedTime = elapsed_time;
    this.startTime = Time.time;
    this.stopTime = -1f;
    MonoBehaviourSingleton<InGameManager>.I.StopIntervalTransferInfoRemaindTimeUpdate();
    bool flag = QuestManager.IsValidInGameExplore() || MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameSeries() || QuestManager.IsValidInGameWaveMatch() || QuestManager.IsValidInGameSeriesArena();
    if (MonoBehaviourSingleton<CoopNetworkManager>.IsValid() & flag && MonoBehaviourSingleton<CoopManager>.I.isStageHost)
    {
      float num1 = this.limitTime;
      if (MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo != null && (double) MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo.remaindTime >= 0.0)
        num1 = MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo.remaindTime;
      float num2 = this.limitTime - num1;
      if (MonoBehaviourSingleton<InGameManager>.I.IsRush() && MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo != null && MonoBehaviourSingleton<InGameManager>.I.isRushReentry)
        num2 = MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo.elapsedTime;
      if ((QuestManager.IsValidInGameSeries() || QuestManager.IsValidInGameSeriesArena()) && MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo != null && MonoBehaviourSingleton<InGameManager>.I.isSeriesReentry)
        num2 = MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo.elapsedTime;
      if (QuestManager.IsValidInGameWaveMatch() && MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo != null)
        num2 = MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo.elapsedTime;
      if (QuestManager.IsValidInGameExplore())
        MonoBehaviourSingleton<CoopNetworkManager>.I.RoomTimeCheck(num2);
      this.SetElapsedTime(num2);
    }
    if (!MonoBehaviourSingleton<InGameManager>.I.IsArenaTimeAttack() || MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo == null)
      return;
    this.arenaElapsedSec = (XorFloat) MonoBehaviourSingleton<InGameManager>.I.intervalTransferInfo.elapsedTime;
  }

  public void ResetStartTimer(float limit_time, float elapsed_time = 0.0f)
  {
    this.isInitStartTime = false;
    this.SetLimitTime(limit_time);
    this.StartTimer(elapsed_time);
  }

  public void SetElapsedTime(float elapsed_time)
  {
    if (!this.enableLimitTime)
      return;
    float num = Time.time - this.startTime;
    this.elapsedTime = elapsed_time - num;
  }

  public float GetElapsedTime()
  {
    if (!this.isInitStartTime || !this.enableLimitTime)
      return 0.0f;
    float elapsedTime = 0.0f;
    if (this.IsStartTimer())
      elapsedTime += Time.time - this.startTime + this.elapsedTime;
    if (this.IsStopTimer())
      elapsedTime -= Time.time - this.stopTime;
    if ((double) elapsedTime < 0.0)
      elapsedTime = 0.0f;
    return elapsedTime;
  }

  public void SetLimitTime(float limit_time)
  {
    this.limitTime = limit_time;
    this.enableLimitTime = (double) limit_time != 0.0;
  }

  public void AddLimitTime(float add_time) => this.limitTime += add_time;

  public bool IsStartTimer() => (double) this.startTime >= 0.0 && this.isInitStartTime;

  public bool IsStopTimer() => (double) this.stopTime >= 0.0;

  public void StopTimer() => this.stopTime = Time.time;

  public void RestartTimer()
  {
    if (!this.isInitStartTime || !this.IsStopTimer())
      return;
    this.startTime += Time.time - this.stopTime;
    this.stopTime = -1f;
  }

  public void SetRushRemainTime(int remainTime) => this.rushRemainTime = (float) remainTime;

  public string GetRushRemainTimeToString()
  {
    return InGameProgress.GetTimeToString(Mathf.CeilToInt(this.remaindTime));
  }

  public static string GetTimeToString(int time_int)
  {
    char[] chArray = new char[32 /*0x20*/];
    if (time_int < 0)
      time_int = 0;
    int num1 = 48 /*0x30*/;
    chArray[0] = (char) (num1 + time_int / 3600);
    chArray[1] = ':';
    int num2 = time_int / 60 % 60;
    chArray[2] = (char) (num1 + num2 / 10);
    chArray[3] = (char) (num1 + num2 % 10);
    chArray[4] = ':';
    int num3 = time_int % 60;
    chArray[5] = (char) (num1 + num3 / 10);
    chArray[6] = (char) (num1 + num3 % 10);
    return new string(chArray);
  }

  public static string GetTimeToStringMMSS(int time_int)
  {
    if (time_int < 0)
      time_int = 0;
    int num1 = time_int % 60;
    int num2 = time_int / 60;
    int num3 = num2 / 60;
    int num4 = num2 - num3 * 60;
    string str = "";
    if (num3 > 0)
      str = num3.ToString() + ":";
    return str + $"{num4:D2}:{num1:D2}";
  }

  public static string GetTimeWithMilliSecToString(float time)
  {
    char[] chArray = new char[32 /*0x20*/];
    if ((double) time < 0.0)
      time = 0.0f;
    int num1 = 48 /*0x30*/;
    int num2 = Mathf.FloorToInt(time);
    int num3 = num2 / 60 % 60;
    chArray[0] = (char) (num1 + num3 / 10);
    chArray[1] = (char) (num1 + num3 % 10);
    chArray[2] = ':';
    int num4 = num2 % 60;
    chArray[3] = (char) (num1 + num4 / 10);
    chArray[4] = (char) (num1 + num4 % 10);
    chArray[5] = '.';
    int num5 = Mathf.FloorToInt((float) ((double) time % 1.0 * 1000.0));
    chArray[6] = (char) (num1 + num5 / 100);
    chArray[7] = (char) (num1 + num5 % 100 / 10);
    chArray[8] = (char) (num1 + num5 % 100 % 10);
    return new string(chArray);
  }

  public static string GetSeriesArenaTimeWithMilliSecToString(float time)
  {
    return InGameProgress.GetTimeWithMilliSecToString(time).Remove(8);
  }

  public string GetRemainTime()
  {
    int time_int = 0;
    bool flag = true;
    if (MonoBehaviourSingleton<InGameProgress>.IsValid())
    {
      if (MonoBehaviourSingleton<InGameProgress>.I.enableVictoryIntervalTime)
      {
        time_int = Mathf.CeilToInt(MonoBehaviourSingleton<InGameProgress>.I.victoryIntervalTime);
        flag = true;
      }
      else
      {
        time_int = Mathf.CeilToInt(MonoBehaviourSingleton<InGameProgress>.I.remaindTime);
        flag = MonoBehaviourSingleton<InGameProgress>.I.enableLimitTime;
      }
    }
    return !flag ? "-:--:--" : InGameProgress.GetTimeToString(time_int);
  }

  public void SetRushTimeBonus(
    List<QuestRushProgressData.RushTimeBonus> rushTimeBonus)
  {
    this.rushTimeBonus = rushTimeBonus;
  }

  public void SetArenaRemainTime(XorInt remainTime)
  {
    this.arenaRemainSec = (XorFloat) ((float) (int) remainTime * (1f / 1000f));
  }

  public XorInt GetArenaRemainMilliSec()
  {
    return (XorInt) Mathf.FloorToInt((float) this.arenaRemainSec * 1000f);
  }

  public string GetArenaRemainTimeToString()
  {
    return InGameProgress.GetTimeToString(Mathf.CeilToInt(this.remaindTime));
  }

  public float GetArenaElapsedTime() => (float) this.arenaElapsedSec + this.GetElapsedTime();

  public XorInt GetArenaElapsedMilliSec()
  {
    return (XorInt) Mathf.FloorToInt(this.GetArenaElapsedTime() * 1000f);
  }

  public string GetArenaElapseTimeToString()
  {
    return InGameProgress.GetTimeWithMilliSecToString(this.GetArenaElapsedTime());
  }

  public void SetArenaTimeBonus(
    List<QuestArenaProgressData.ArenaTimeBonus> arenaTimeBonus)
  {
    this.arenaTimeBonus = arenaTimeBonus;
  }

  public void SetAfkLimitTime()
  {
    float num = 480f;
    this.afkTime = num;
    this.enableAfkTime = (double) num > 0.0;
  }

  public void StartAfkTimer()
  {
    if (!this.enableAfkTime)
      return;
    this.afkTime = 480f;
  }

  private void ResetAfkTimer()
  {
    if (!MonoBehaviourSingleton<FieldManager>.IsValid())
      return;
    this.StartAfkTimer();
  }

  private void ProgressAfkTimer()
  {
    if (!MonoBehaviourSingleton<FieldManager>.IsValid() || (double) this.remainedAfkTime <= 0.0)
      return;
    this.afkTime -= Time.deltaTime;
  }

  private void SetExploreBossMoveTimer()
  {
    int bossMoveTime = MonoBehaviourSingleton<PartyManager>.I.partyData.quest.explore.bossMoveTime;
    this.bossMoveRemainTime = bossMoveTime <= 0 ? 45f : (float) bossMoveTime;
  }

  private void ProgressExploreBossMoveTimer()
  {
    if ((double) this.bossMoveRemainTime <= 0.0)
      return;
    this.bossMoveRemainTime -= Time.deltaTime;
  }

  public void ResetExploreHostDCTimer()
  {
    this.exploreHostDCRemainTime = 30f;
    this.requestedExploreAlive = false;
  }

  private void SetExploreHostDCTimer(float time, bool requestAlive)
  {
    this.exploreHostDCRemainTime = time;
    this.requestedExploreAlive = requestAlive;
  }

  private void ProgressExploreHostDCTimer() => this.exploreHostDCRemainTime -= Time.deltaTime;

  public void ExploreHappenQuestDirection()
  {
    if (this.progressEndType != InGameProgress.PROGRESS_END_TYPE.NONE || MonoBehaviourSingleton<QuestManager>.I.GetExploreBossAppearMapId() != (int) MonoBehaviourSingleton<FieldManager>.I.currentMapID)
      return;
    this.StartCoroutine(this.DoExploreHappenQuestDirection());
  }

  private IEnumerator DoExploreHappenQuestDirection()
  {
    this.isGameProgressStop = true;
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (Object.op_Inequality((Object) self, (Object) null))
      self.hitOffFlag |= StageObject.HIT_OFF_FLAG.FORCE;
    while (!this.isBattleStart)
      yield return (object) null;
    if (MonoBehaviourSingleton<QuestManager>.I.GetExploreBossAppearMapId() == (int) MonoBehaviourSingleton<FieldManager>.I.currentMapID)
    {
      while (MonoBehaviourSingleton<UIInGamePopupDialog>.I.IsShowingDialog())
        yield return (object) null;
      while (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
        yield return (object) null;
      this.SetDisableUIOpen(true);
      yield return (object) this.StartCoroutine(this.DoCloseDialog());
      PartyModel.ExploreInfo explore = MonoBehaviourSingleton<PartyManager>.I.partyData.quest.explore;
      int rareBossType = 0;
      if (explore != null)
        rareBossType = explore.isRare;
      if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
        MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Play(ENEMY_TYPE.NONE, rareBossType);
      yield return (object) new WaitForSeconds(3f);
      this.SetDisableUIOpen(false);
      if (Object.op_Inequality((Object) self, (Object) null) && Object.op_Inequality((Object) self.controller, (Object) null))
        self.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.BATTLE_END);
      this.ExploreFieldToQuestInterval();
    }
  }

  public bool GimmickQuestDirection(uint link_quest_id)
  {
    if (link_quest_id == 0U)
      return false;
    this.StartCoroutine(this.DoGimmickQuestDirection(link_quest_id));
    return true;
  }

  private IEnumerator DoGimmickQuestDirection(uint link_quest_id)
  {
    this.isGameProgressStop = true;
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (Object.op_Inequality((Object) self, (Object) null))
      self.hitOffFlag |= StageObject.HIT_OFF_FLAG.FORCE;
    while (!this.isBattleStart)
      yield return (object) null;
    while (MonoBehaviourSingleton<UIInGamePopupDialog>.I.IsShowingDialog())
      yield return (object) null;
    while (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
      yield return (object) null;
    if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
      MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Play(ENEMY_TYPE.NONE);
    yield return (object) new WaitForSeconds(3f);
    this.SetDisableUIOpen(false);
    if (Object.op_Inequality((Object) self, (Object) null) && Object.op_Inequality((Object) self.controller, (Object) null))
      self.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.BATTLE_END);
    this.FieldToQuestInterval(link_quest_id, 0U, false, true);
  }

  public bool HappenQuestDirection(
    uint link_quest_id,
    QuestInfoData.Quest.Reward[] reward = null,
    int rareBossType = 0)
  {
    if (this.progressEndType != InGameProgress.PROGRESS_END_TYPE.NONE || this.isHappenQuestDirection || !FieldManager.IsValidInGameNoQuest() || MonoBehaviourSingleton<FieldManager>.I.currentMapData == null || link_quest_id == 0U || !MonoBehaviourSingleton<QuestManager>.I.IsOpenedQuest(link_quest_id))
      return false;
    this.isHappenQuestDirection = true;
    this.StartCoroutine(this.DoHappenQuestDirection(link_quest_id, reward, rareBossType));
    return true;
  }

  private IEnumerator DoHappenQuestDirection(
    uint link_quest_id,
    QuestInfoData.Quest.Reward[] reward = null,
    int rareBossType = 0)
  {
    InGameSettingsManager.HappenQuestDirection parameter = MonoBehaviourSingleton<InGameSettingsManager>.I.happenQuestDirection;
    this.isGameProgressStop = true;
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (Object.op_Inequality((Object) self, (Object) null))
      self.hitOffFlag |= StageObject.HIT_OFF_FLAG.FORCE;
    while (!this.isBattleStart)
      yield return (object) null;
    while (MonoBehaviourSingleton<UIInGamePopupDialog>.I.IsShowingDialog())
      yield return (object) null;
    while (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
      yield return (object) null;
    this.SetDisableUIOpen(true);
    yield return (object) this.StartCoroutine(this.DoCloseDialog());
    uint id = 0;
    QuestTable.QuestTableData quest_data = Singleton<QuestTable>.I.GetQuestData(link_quest_id);
    if (quest_data == null)
      Log.Error(LOG.INGAME, "InGameProgress.DoHappenQuestDirection() LinkQuestID is invalid. quest_id : " + (object) link_quest_id);
    else
      id = (uint) quest_data.GetMainEnemyID();
    EnemyTable.EnemyData enemy_data = Singleton<EnemyTable>.I.GetEnemyData(id);
    if (enemy_data == null)
      Log.Error(LOG.INGAME, "InGameProgress.DoHappenQuestDirection() EnemyID is invalid. enemy_id : " + (object) id);
    if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
      MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Play(enemy_data.type, rareBossType);
    float before_time = Time.time;
    while ((double) Time.time - (double) before_time < (double) parameter.warningTime && !this.endHappenQuestDirection)
      yield return (object) null;
    if (!this.endHappenQuestDirection && !MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog(quest_data, is_happen_quest: true))
      this.endHappenQuestDirection = true;
    if (this.endHappenQuestDirection)
    {
      if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
        ((Component) MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I).gameObject.SetActive(false);
      if (this.progressEndType == InGameProgress.PROGRESS_END_TYPE.NONE)
      {
        this.isGameProgressStop = false;
        if (Object.op_Inequality((Object) self, (Object) null))
          self.hitOffFlag &= ~StageObject.HIT_OFF_FLAG.FORCE;
      }
      this.isHappenQuestDirection = false;
      this.endHappenQuestDirection = false;
    }
    else
    {
      yield return (object) MonoBehaviourSingleton<TransitionManager>.I.Out();
      this.SetDisableUIOpen(false);
      this.ViewUI(false);
      if (Object.op_Inequality((Object) self, (Object) null) && Object.op_Inequality((Object) self.controller, (Object) null))
        self.controller.SetEnableControll(false, ControllerBase.DISABLE_FLAG.BATTLE_END);
      if (MonoBehaviourSingleton<TargetMarkerManager>.IsValid())
        MonoBehaviourSingleton<TargetMarkerManager>.I.showMarker = false;
      if (MonoBehaviourSingleton<DropTargetMarkerManeger>.IsValid())
        MonoBehaviourSingleton<DropTargetMarkerManeger>.I.active = false;
      Transform model = (Transform) null;
      EnemyLoader enemy_loader = (EnemyLoader) null;
      EnemyAnimCtrl enemyAnimCtrl = (EnemyAnimCtrl) null;
      Vector3 enemyInitPos = Vector3.zero;
      LoadObject lo_cam_portrait = (LoadObject) null;
      LoadObject lo_cam_landscape = (LoadObject) null;
      Vector3[] camera_offsets = (Vector3[]) null;
      if (enemy_data != null)
      {
        model = Utility.CreateGameObject("HappenQuestModel", ((Component) this).transform);
        enemy_loader = ((Component) model).gameObject.AddComponent<EnemyLoader>();
        model.position = Vector3.zero;
        int animId = enemy_data.animId;
        OutGameSettingsManager.EnemyDisplayInfo enemyDisplayInfo1 = MonoBehaviourSingleton<OutGameSettingsManager>.I.SearchEnemyDisplayInfoForQuestSelect(enemy_data);
        InGameSettingsManager.HappenQuestDirection.EnemyDisplayInfo enemyDisplayInfo2 = quest_data.questStyle == QUEST_STYLE.NORMAL ? parameter.GetEnemyDisplayInfo(enemy_data) : parameter.GetEnemyDisplayInfoByQuestType(enemy_data, quest_data.questStyle) ?? parameter.GetEnemyDisplayInfo(enemy_data);
        if (enemyDisplayInfo1 != null && enemyDisplayInfo1.animID > 0)
          animId = enemyDisplayInfo1.animID;
        enemyInitPos = parameter.enemyInitPos;
        if (enemyDisplayInfo2 != null && Vector3.op_Inequality(enemyDisplayInfo2.modelOffset, Vector3.zero))
          enemyInitPos = enemyDisplayInfo2.modelOffset;
        LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
        if (enemyDisplayInfo2 != null && !string.IsNullOrEmpty(enemyDisplayInfo2.cameraNamePortrait))
          lo_cam_portrait = load_queue.Load(RESOURCE_CATEGORY.ENEMY_CAMERA, enemyDisplayInfo2.cameraNamePortrait);
        if (enemyDisplayInfo2 != null && !string.IsNullOrEmpty(enemyDisplayInfo2.cameraNameLandscape))
          lo_cam_landscape = load_queue.Load(RESOURCE_CATEGORY.ENEMY_CAMERA, enemyDisplayInfo2.cameraNameLandscape);
        if (enemyDisplayInfo2 != null)
          camera_offsets = new Vector3[2]
          {
            enemyDisplayInfo2.cameraOffsetPortrait,
            enemyDisplayInfo2.cameraOffsetLandscape
          };
        bool load_finish = false;
        enemy_loader.StartLoad(enemy_data.modelId, animId, enemy_data.modelScale, enemy_data.baseEffectName, enemy_data.baseEffectNode, true, true, true, ShaderGlobal.GetCharacterShaderType(), 18, need_stamp_effect: true, weather_effect: enemy_data.weatherChangeEffect, callback: (EnemyLoader.OnCompleteLoad) (enemy => load_finish = true));
        while (!load_finish)
          yield return (object) null;
        yield return (object) load_queue.Wait();
        enemyAnimCtrl = ((Component) model).gameObject.AddComponent<EnemyAnimCtrl>();
        enemyAnimCtrl.Init(enemy_loader, (Camera) null, true);
        Utility.SetLayerWithChildren(model, 18);
        load_queue = (LoadingQueue) null;
      }
      List<GameObject> change_layer_list = new List<GameObject>();
      List<GameObject> hideObjectList = new List<GameObject>();
      if (MonoBehaviourSingleton<StageManager>.IsValid())
      {
        Transform[] componentsInChildren = ((Component) MonoBehaviourSingleton<StageManager>.I).GetComponentsInChildren<Transform>();
        int index = 0;
        for (int length = componentsInChildren.Length; index < length; ++index)
        {
          if (((Component) componentsInChildren[index]).gameObject.layer == 0)
          {
            ((Component) componentsInChildren[index]).gameObject.layer = 18;
            change_layer_list.Add(((Component) componentsInChildren[index]).gameObject);
          }
          if (((Object) componentsInChildren[index]).name.Contains("HideObjects"))
          {
            ((Component) componentsInChildren[index]).gameObject.SetActive(false);
            hideObjectList.Add(((Component) componentsInChildren[index]).gameObject);
          }
        }
      }
      if (MonoBehaviourSingleton<InGameCameraManager>.IsValid() && Object.op_Inequality((Object) enemy_loader, (Object) null) && Object.op_Inequality((Object) enemy_loader.body, (Object) null))
      {
        Object[] cameras = new Object[2]
        {
          lo_cam_portrait.loadedObject,
          lo_cam_landscape.loadedObject
        };
        MonoBehaviourSingleton<InGameCameraManager>.I.OnHappenQuestDirection(true, enemy_loader.body, cameras, camera_offsets);
      }
      if (Object.op_Inequality((Object) model, (Object) null) && Object.op_Inequality((Object) enemy_loader, (Object) null) && Object.op_Inequality((Object) enemy_loader.body, (Object) null))
      {
        ((Component) model).transform.position = Vector3.Scale(enemyInitPos, enemy_loader.body.lossyScale);
        ((Component) model).transform.rotation = Quaternion.AngleAxis(parameter.enemyInitDir, Vector3.up);
      }
      bool anim_end = false;
      enemyAnimCtrl.PlayQuestStartAnim((System.Action) (() => anim_end = true));
      if (!this.forceComplete)
        yield return (object) MonoBehaviourSingleton<TransitionManager>.I.In();
      if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
        ((Component) MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I).gameObject.SetActive(false);
      while (!anim_end && !this.endHappenQuestDirection)
        yield return (object) null;
      if (!this.endHappenQuestDirection)
      {
        this.isDecidedHappenQuestDialog = false;
        this.isYesHappenQuestDialog = false;
        while (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
          yield return (object) null;
        MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(nameof (InGameProgress), ((Component) this).gameObject, "HAPPEN_QUEST", (object) new InGameFieldQuestConfirm.Desc()
        {
          questData = quest_data,
          reward = reward
        });
        while (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
          yield return (object) null;
        while (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection() is InGameFieldQuestConfirm)
        {
          if (this.endHappenQuestDirection)
          {
            while (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
              yield return (object) null;
            if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSection() is InGameFieldQuestConfirm)
            {
              MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(nameof (InGameProgress), ((Component) this).gameObject, "[BACK]");
              break;
            }
            break;
          }
          yield return (object) null;
        }
        while (!MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible())
          yield return (object) null;
      }
      if (this.isYesHappenQuestDialog && !this.endHappenQuestDirection)
      {
        this.isHappenQuestDirection = false;
        this.FieldToQuestInterval(link_quest_id, 0U, false);
      }
      else
      {
        GameSceneGlobalSettings.RequestSoundSettingIngameField();
        if (this.isDecidedHappenQuestDialog && !this.isYesHappenQuestDialog)
          MonoBehaviourSingleton<InGameManager>.I.disableHappenQuestIdList.Add(link_quest_id);
        yield return (object) MonoBehaviourSingleton<TransitionManager>.I.Out();
        if (Object.op_Inequality((Object) enemy_loader, (Object) null))
        {
          enemy_loader.DeleteLoadedObjects();
          Object.DestroyImmediate((Object) enemy_loader);
          enemy_loader = (EnemyLoader) null;
        }
        if (Object.op_Inequality((Object) model, (Object) null))
        {
          Object.Destroy((Object) ((Component) model).gameObject);
          model = (Transform) null;
        }
        int index = 0;
        for (int count = change_layer_list.Count; index < count; ++index)
          change_layer_list[index].layer = 0;
        foreach (GameObject gameObject in hideObjectList)
        {
          if (Object.op_Inequality((Object) gameObject, (Object) null))
            gameObject.SetActive(true);
        }
        if (this.progressEndType == InGameProgress.PROGRESS_END_TYPE.NONE)
        {
          this.ViewUI(true);
          this.isGameProgressStop = false;
          if (MonoBehaviourSingleton<TargetMarkerManager>.IsValid())
            MonoBehaviourSingleton<TargetMarkerManager>.I.showMarker = true;
          if (MonoBehaviourSingleton<DropTargetMarkerManeger>.IsValid())
            MonoBehaviourSingleton<DropTargetMarkerManeger>.I.active = true;
          if (Object.op_Inequality((Object) self, (Object) null))
          {
            self.hitOffFlag &= ~StageObject.HIT_OFF_FLAG.FORCE;
            if (Object.op_Inequality((Object) self.controller, (Object) null))
              self.controller.SetEnableControll(true, ControllerBase.DISABLE_FLAG.BATTLE_END);
          }
        }
        if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
        {
          MonoBehaviourSingleton<InGameCameraManager>.I.OnHappenQuestDirection(false);
          MonoBehaviourSingleton<InGameCameraManager>.I.EndRadialBlurFilter();
        }
        yield return (object) MonoBehaviourSingleton<TransitionManager>.I.In();
        this.isHappenQuestDirection = false;
        this.endHappenQuestDirection = false;
      }
    }
  }

  public void OnFieldQuestConfirm(bool is_yes)
  {
    this.isDecidedHappenQuestDialog = true;
    this.isYesHappenQuestDialog = is_yes;
  }

  public bool EndHappenQuestDirection()
  {
    if (!this.isHappenQuestDirection)
      return false;
    this.endHappenQuestDirection = true;
    return true;
  }

  public void CacheAudio(LoadingQueue load_queue)
  {
    foreach (int se_id in (int[]) Enum.GetValues(typeof (InGameProgress.AUDIO)))
      load_queue.CacheSE(se_id);
  }

  private void PlayAudio(InGameProgress.AUDIO type) => SoundManager.PlayOneshotJingle((int) type);

  public void PlayTimeBonusSE() => SoundManager.PlayOneShotUISE(40000158);

  public List<int> GetMissionClearStatuses()
  {
    List<int> clearMissions = new List<int>();
    this.missionCheck.ForEach((Action<MissionCheckBase>) (mission => clearMissions.Add(mission.IsMissionClear() ? 1 : 0)));
    return clearMissions;
  }

  private void OverwritePlayerRecorderForExplore(bool isInGame)
  {
    if (!QuestManager.IsValidInGameExplore())
      return;
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      MonoBehaviourSingleton<CoopManager>.I.coopRoom.clients.ForEach((Action<CoopClient>) (x =>
      {
        if (!Object.op_Inequality((Object) x.GetPlayer(), (Object) null))
          return;
        MonoBehaviourSingleton<QuestManager>.I.UpdateExplorePlayerStatus(x);
      }));
    List<ExplorePlayerStatus> playerStatusList = MonoBehaviourSingleton<QuestManager>.I.GetExplorePlayerStatusList();
    ExploreBossStatus exploreBossStatus = MonoBehaviourSingleton<QuestManager>.I.GetExploreBossStatus();
    PartyModel.Party party = (PartyModel.Party) null;
    if (MonoBehaviourSingleton<PartyManager>.IsValid())
      party = MonoBehaviourSingleton<PartyManager>.I.partyData;
    MonoBehaviourSingleton<InGameRecorder>.I.SetRecordsForExplore(playerStatusList, party, exploreBossStatus, isInGame);
  }

  public void AddDefeatCount(bool isBoss)
  {
    ++this.defeatCount;
    if (!isBoss)
      return;
    ++this.defeatBossCount;
  }

  public void AddPartyDefeatCount() => ++this.partyDefeatCount;

  public void AddPartyDefeatBossCount() => ++this.partyDefeatBossCount;

  public void ClearDefeatCount()
  {
    this.defeatCount = 0;
    this.defeatBossCount = 0;
    this.partyDefeatBossCount = 0;
    this.partyDefeatCount = 0;
  }

  public void SetWaveMatchWave(int wave) => this.waveMatchWave = wave;

  public void NextBattleStartForSeriesArena()
  {
    if (!MonoBehaviourSingleton<CoopOfflineManager>.IsValid() || MonoBehaviourSingleton<StageObjectManager>.I.self.isDead && (double) MonoBehaviourSingleton<StageObjectManager>.I.self.autoReviveHp <= 0.0)
      return;
    this.StartCoroutine(this._NextBattleStartForSeriesArena());
  }

  private IEnumerator _NextBattleStartForSeriesArena()
  {
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    SelfController selfController = self.controller as SelfController;
    this.isGameProgressStop = true;
    this.StopTimer();
    self.SetHitOffFlag(true, StageObject.HIT_OFF_FLAG.FORCE);
    selfController.SetEnableControll(false, ControllerBase.DISABLE_FLAG.CHANGE_UNIQUE_EQUIPMENT);
    MonoBehaviourSingleton<UIPlayerStatus>.I.SetEnableWeaponChangeButton(false);
    int nextIndex = (int) MonoBehaviourSingleton<QuestManager>.I.currentQuestSeriesIndex + 1;
    yield return (object) new WaitWhile((Func<bool>) (() => self.actionID != Character.ACTION_ID.IDLE && !self.enableCancelToAvoid));
    self.ActChangeUniqueEquipment(MonoBehaviourSingleton<StatusManager>.I.GetCreateUniquePlayerInfo(nextIndex + 1));
    while (self.actionID == (Character.ACTION_ID) 27)
      yield return (object) null;
    MonoBehaviourSingleton<UIPlayerStatus>.I.SetEnableWeaponChangeButton(true);
    selfController.SetEnableControll(true, ControllerBase.DISABLE_FLAG.CHANGE_UNIQUE_EQUIPMENT);
    if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.boss, (Object) null))
      yield return (object) MonoBehaviourSingleton<StageObjectManager>.I.boss.WaitForDeadMotionEnd(false);
    MonoBehaviourSingleton<CoopOfflineManager>.I.EnemyPopForSeriesArena(nextIndex);
    self.SetHitOffFlag(false, StageObject.HIT_OFF_FLAG.FORCE);
    this.RestartTimer();
    this.isGameProgressStop = false;
  }

  private enum AUDIO
  {
    RESULT_WIN = 40000067, // 0x02625A43
    QUEST_START = 40000072, // 0x02625A48
    RESULT_LOSE = 40000073, // 0x02625A49
    TIME_BONUS = 40000158, // 0x02625A9E
  }

  public enum PROGRESS_END_TYPE
  {
    NONE,
    QUEST_VICTORY,
    QUEST_RETIRE,
    QUEST_RETRY,
    QUEST_INVITEQUIT,
    QUEST_TIMEUP,
    QUEST_SERIES_INTERVAL,
    FIELD_MAP_INTERVAL,
    FIELD_TO_QUEST_INTERVAL,
    FIELD_TO_HOME,
    FIELD_RETIRE,
    FIELD_REENTRY,
    FIELD_MAP_INTERVAL_TUTORIAL,
    FORCE_DEFEAT,
    FIELD_TO_STORY,
    FIELD_TO_HOME_TIMEOUT,
    EXPLORE_MOVE_INTERVAL,
    EXPLORE_HAPPEN_INTERVAL,
    RUSH_INTERVAL,
    ARENA_INTERVAL,
    FIELD_READ_STORY,
    QUEST_TO_FIELD,
    QUEST_TO_QUEST_REPEAT,
  }

  public enum eFieldGimmick
  {
    Cannon,
    Sonar,
    ReadStory,
    GatherGimmick,
    Bingo,
    Chat,
    CoopFishing,
    CarriableGimmick,
    SupplyGimmick,
    PortalGimmick,
    QuestGimmick,
    Max,
  }

  private class GimmickSearchInfo
  {
    public float dist;
    public IFieldGimmickObject obj;
  }
}
