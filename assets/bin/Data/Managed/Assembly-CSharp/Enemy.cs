// Decompiled with JetBrains decompiler
// Type: Enemy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class Enemy : Character
{
  private readonly uint kStrIdx_EnemyReaction_ElemTolChange;
  private readonly uint kStrIdx_EnemyReaction_Counter = 1;
  private readonly uint kStrIdx_EnemyReaction_MadMode = 2;
  private readonly uint kStrIdx_EnemyReaction_BuffCancellation = 3;
  private readonly uint kStrIdx_EnemyReaction_BreakCounterRegion = 4;
  private readonly uint kStrIdx_EnemyReaction_ReviveCounterRegion = 5;
  public const int INVALID_REGIONID = -1;
  public const int REGIONID_BODY = 0;
  private static int updateFrame = -1;
  private static float selfHitEffectCool = 0.0f;
  private static float otherHitEffectCool = 0.0f;
  public bool isStoke;
  private uint m_nowAngryId;
  private List<uint> m_execAngryIds = new List<uint>();
  public XorInt enemyLevel;
  public QuestStartData.EnemyReward enemyReward;
  public bool isRareSpecies;
  public float walkSpeedRateFromTable = 1f;
  public int enemyServantId = 490000;
  public static readonly string[] subMotionStateName = new string[16 /*0x10*/]
  {
    "step",
    "step_back",
    "down",
    "down_time",
    "counter",
    "escape_start",
    "escape",
    "dizzy",
    "mad_mode",
    "appear",
    "dead_revive_01",
    "dead_revive_02",
    "dead_revive_03",
    "dead_revive_04",
    "dead_revive_05",
    "angry_{0:00}"
  };
  private const string DEF_HEAD_NAME = "Head";
  private const string DEF_HIP_NAME = "Hip";
  [Tooltip("頭のオブジェクト名(頭からの距離測定起点)")]
  public string headObjectName = "Head";
  [Tooltip("尻のオブジェクト名(尻からの距離測定起点)")]
  public string hipObjectName = "Hip";
  private Transform _head;
  private Transform _hip;
  protected float bleedCounter;
  [Tooltip("体の大きさ半径（マップとの当たり、影の大きさ")]
  public float bodyRadius = 1f;
  [Tooltip("バフ系(凍結,腐敗等)のエフェクト大きさ調整(0の場合は無視する")]
  public float effectFreezeRadiusRate;
  [Tooltip("シャドウシーリング/コンカッションのエフェクト大きさ調整(0の場合は無視する")]
  public float effectShadowSealingRadiusRate;
  [Tooltip("光輪のエフェクト大きさ調整(0の場合は無視する)")]
  public float effectLightRingRadiusRate;
  [Tooltip("UI高さ")]
  public float uiHeight = 2f;
  [Tooltip("UI表示距離")]
  public float uiShowDistance;
  public AttackInfo[] convertAttackInfos;
  public Enemy.RegionInfo[] regionInfos;
  public float downHeal = 10f;
  public float downTotal;
  public float downHealInterval;
  public int downCount;
  public float[] downMaxRate;
  public int _downMax = 100;
  protected bool hitShockLightFlag;
  protected bool hitShockOffsetFlag;
  protected float hitShockLightTime;
  protected float hitShockOffsetTime;
  protected Vector3 hitShockVec = Vector3.zero;
  protected Vector3 hitShockOffset = Vector3.zero;
  private bool canHitShockEffect = true;
  protected Vector3 dashBeforePos = Vector3.zero;
  protected float dashNowDistance;
  protected float dashOverDistance;
  protected float dashMinDistance;
  protected float dashMaxDistance;
  protected string dashEndTrigger;
  protected bool dashOverFlag;
  protected float dashOverCheckDistance;
  protected bool warpViewFlag;
  protected float warpViewRate;
  protected float warpViewRatePerTime;
  public float damageHpRate;
  public float healDamageRate;
  public const int SHOT_POINT_ALL_PLAYER = 0;
  public const int SHOT_POINT_RANDOM_PICKUP = 1;
  public const int SHOT_POINT_CURRENT_TARGET = 3;
  protected List<Enemy.RandomShotInfo> randomShotInfo = new List<Enemy.RandomShotInfo>();
  protected List<Enemy.RandomShotInfo> shotNetworkInfoQueue = new List<Enemy.RandomShotInfo>();
  protected List<Enemy.RandomShotInfo> shotEventInfoQueue = new List<Enemy.RandomShotInfo>();
  protected bool radialBlurEnable;
  public UIEnemyStatusGizmo uiEnemyStatusGizmo;
  private Coroutine forceEnemyOutCoroutine;
  private List<AttackNWayLaser> m_activeAttackLaserList = new List<AttackNWayLaser>();
  private List<AttackFunnelBit> m_activeAttackFunnelList = new List<AttackFunnelBit>();
  private List<AttackDig> m_activeAttackDigList = new List<AttackDig>();
  private List<AttackActionMine> m_activeAttackActionMineList = new List<AttackActionMine>();
  private List<AttackShotNodeLink> m_activeAttackObstacleList = new List<AttackShotNodeLink>();
  private List<EnemyEffectObject> m_enemyEffectList = new List<EnemyEffectObject>();
  public DrainAttackInfo[] drainAtkInfos;
  private XorInt m_barrierHpMax = (XorInt) 0;
  private XorInt m_barrierHp = (XorInt) 0;
  public bool isRequireGhostShaderParam;
  public float ghostBuffEndParam;
  public float ghostBuffDuration;
  public bool willStock;
  public bool isHideSpawn;
  public bool isHiding;
  public float turnUpDistance;
  public uint gatherPointViewId;
  protected FieldMapTable.GatherPointViewTableData viewData;
  protected Transform targetEffect;
  protected Transform gatherEffect;
  public float paralyzeLoopTime;
  public ConverteElementToleranceTable[] converteElementToleranceTable = new ConverteElementToleranceTable[0];
  public int madModeHpThreshold;
  public int madModeLvThreshold;
  private int madModeHp;
  public bool isFirstMadMode;
  public float shadowSealingBindResist;
  public float m_dizzyTime;
  public TailController tailController;
  public bool useDownLoopTime;
  public float downLoopStartTime;
  public float downLoopTime;
  private float downTime;
  private float downGaugeDecreaseStartTime;
  private float[] downDecreaseRates;
  private ARENA_CONDITION[] arenaConditionList;
  private bool isArenaDamageOffWeapon;
  private bool isArenaDamageOffMagi;
  private List<Enemy.AnimationLayerWeightChangeInfo> animLayerWeightChangeInfo = new List<Enemy.AnimationLayerWeightChangeInfo>();
  public ELEMENT_TYPE changeElementIcon = ELEMENT_TYPE.MAX;
  public ELEMENT_TYPE changeWeakElementIcon = ELEMENT_TYPE.MAX;
  public int changeToleranceRegionId = -1;
  public int changeToleranceScroll = -1;
  public BlendColorCtrl blendColorCtrl = new BlendColorCtrl();
  private SkinnedMeshRenderer[] skinnedMeshRendererList;
  private Enemy.eCounterRegionState m_CounterRegionState;
  public int deadReviveCount;
  public int deadReviveCountMax;
  public int actDeadReviveCount;
  public bool forceActMovePoint;
  public bool enableAssimilation;
  private GameObject effectDrainRecover;
  private float grabDrainRecoverTimer;
  private bool cachedIsFieldEnemyBoss;
  private bool isCachedIsFieldEnemyBoss;
  private float bindEndTime;
  protected GameObject m_effectSoilShock;
  private GameObject m_effectBurning;
  private GameObject m_effectErosion;
  private GameObject m_effectAcid;
  private GameObject m_effectCorruption;
  private GameObject m_effectStigmata;
  private GameObject m_effectCyclonicThunderstorm;
  private GameObject m_effectSpeedDown;
  private List<AttackHitColliderProcessor.HitParam> checkHitParam = new List<AttackHitColliderProcessor.HitParam>();
  private List<float> checkLength = new List<float>();
  private List<int> checkPriority = new List<int>();
  private List<int> targetRegionIds = new List<int>();
  public const float HPGAUGE_SHAKE_POWER = 5f;
  public const float HPGAUGE_SHAKE_TIME = 0.5f;
  public const float HPGAUGE_SHAKE_CYCLETIME = 0.05f;
  private GameObject m_effectHitWhenGhost;
  private EnemyAegisController aegisCtrl;
  private List<ResidentEffectObject> m_residentEffectList = new List<ResidentEffectObject>();
  private SystemEffectSetting m_residentEffectSetting;
  public const float ROTATE_RAD_PER_FRAME = 0.0349065848f;
  private const string SHIELD_PROPERTY_MATCAP_POW = "_MatCapPow";
  public float lightRingHeightOffset;
  private float lightRingTime;
  private float lightRingRadius;
  private float lightRingHeight;
  protected Transform effectLightRing;
  public const float SHADOWSEALING_START_NORMALIZED_TIME = 0.1f;
  private Transform debuffShadowSealingEffect;
  private float debuffShadowSealingTimer;
  private float debuffShadowSealingTimerDuration;
  private float debuffShadowSealingRadius;
  private int shadowSealingTarget;
  public float concussionTotal;
  public float concussionMax;
  public float concussionExtend = 1f;
  public float concussionTime;
  public float concussionStartTime;
  private Transform concussionEffect;
  private float concussionRadius;
  private Vector3 concussionPosition;
  private List<int> concussionAddPlayerIdList = new List<int>();

  public override int id
  {
    get => base.id;
    set
    {
      try
      {
        base.id = value;
        ((Object) ((Component) this).gameObject).name = "Enemy:" + (object) value;
      }
      catch (UnityException ex)
      {
      }
    }
  }

  public EnemyLoader loader { get; private set; }

  public InGameSettingsManager.Enemy enemyParameter { get; private set; }

  public bool isPierceAfterTarget { get; private set; }

  public int enemyID { get; set; }

  public bool isBoss { get; set; }

  public bool isWaveMatchBoss { get; set; }

  public bool isBigMonster { get; set; }

  public int enemyPopIndex { get; set; }

  public bool isSummonAttack { get; set; }

  public EnemyTable.EnemyData enemyTableData { get; set; }

  public GrowEnemyTable.GrowEnemyData growTableData { get; set; }

  public ENEMY_TYPE GetEnemyType()
  {
    return this.enemyTableData == null ? ENEMY_TYPE.NONE : this.enemyTableData.type;
  }

  public BrainParam brainParam { get; set; }

  public Transform head
  {
    get
    {
      if (Object.op_Inequality((Object) this._head, (Object) null))
        return this._head;
      this._head = Utility.Find(this._transform, this.headObjectName);
      if (Object.op_Equality((Object) this._head, (Object) null))
        this._head = this._transform;
      this._head = Utility.Find(this._transform, "Head");
      if (Object.op_Equality((Object) this._head, (Object) null))
        this._head = this._transform;
      return this._head;
    }
  }

  public Transform hip
  {
    get
    {
      if (Object.op_Inequality((Object) this._hip, (Object) null))
        return this._hip;
      this._hip = Utility.Find(this._transform, this.hipObjectName);
      if (Object.op_Equality((Object) this._hip, (Object) null))
        this._hip = this._transform;
      this._hip = Utility.Find(this._transform, "Hip");
      if (Object.op_Equality((Object) this._hip, (Object) null))
        this._hip = this._transform;
      return this._hip;
    }
  }

  public static bool IsWeakStateCheckAlreadyHit(Enemy.WEAK_STATE state)
  {
    bool flag = false;
    if (state == Enemy.WEAK_STATE.WEAK || state == Enemy.WEAK_STATE.WEAK_SP_ATTACK || state == Enemy.WEAK_STATE.WEAK_SP_DOWN_MAX || state == Enemy.WEAK_STATE.WEAK_ELEMENT_ATTACK || state == Enemy.WEAK_STATE.WEAK_ELEMENT_SKILL_ATTACK || state == Enemy.WEAK_STATE.WEAK_SKILL_ATTACK || state == Enemy.WEAK_STATE.WEAK_HEAL_ATTACK || state == Enemy.WEAK_STATE.WEAK_ELEMENT_SP_ATTACK)
      flag = true;
    return flag;
  }

  public static bool IsWeakStateSpAttack(Enemy.WEAK_STATE state)
  {
    bool flag = false;
    if (state == Enemy.WEAK_STATE.WEAK_SP_ATTACK || state == Enemy.WEAK_STATE.WEAK_SP_DOWN_MAX || state == Enemy.WEAK_STATE.WEAK_ELEMENT_SP_ATTACK)
      flag = true;
    return flag;
  }

  public static bool IsWeakStateElementAttack(Enemy.WEAK_STATE state)
  {
    bool flag = false;
    if (state == Enemy.WEAK_STATE.WEAK_ELEMENT_ATTACK || state == Enemy.WEAK_STATE.WEAK_ELEMENT_SKILL_ATTACK || state == Enemy.WEAK_STATE.WEAK_ELEMENT_SP_ATTACK)
      flag = true;
    return flag;
  }

  public static bool IsWeakStateSkillAttack(Enemy.WEAK_STATE state)
  {
    bool flag = false;
    if (state == Enemy.WEAK_STATE.WEAK_ELEMENT_SKILL_ATTACK || state == Enemy.WEAK_STATE.WEAK_SKILL_ATTACK)
      flag = true;
    return flag;
  }

  public static bool IsWeakStateHealAttack(Enemy.WEAK_STATE state)
  {
    return state == Enemy.WEAK_STATE.WEAK_HEAL_ATTACK;
  }

  public static bool IsWeakStateDisplaySign(Enemy.WEAK_STATE state)
  {
    return state != Enemy.WEAK_STATE.NONE && state != Enemy.WEAK_STATE.DOWN;
  }

  public static bool IsWeakStateCannonAttack(Enemy.WEAK_STATE state)
  {
    return state == Enemy.WEAK_STATE.WEAK_CANNON;
  }

  public static TargetMarker.EFFECT_TYPE WeakStateToEffectType(Enemy.WEAK_STATE weakState)
  {
    TargetMarker.EFFECT_TYPE effectType = TargetMarker.EFFECT_TYPE.NONE;
    switch (weakState)
    {
      case Enemy.WEAK_STATE.WEAK_ELEMENT_ATTACK:
        effectType = TargetMarker.EFFECT_TYPE.WEAK_ELEMENT_ATTACK;
        break;
      case Enemy.WEAK_STATE.WEAK_ELEMENT_SKILL_ATTACK:
        effectType = TargetMarker.EFFECT_TYPE.WEAK_ELEMENT_SKILL_ATTACK;
        break;
      case Enemy.WEAK_STATE.WEAK_SKILL_ATTACK:
        effectType = TargetMarker.EFFECT_TYPE.WEAK_SKILL_ATTACK;
        break;
      case Enemy.WEAK_STATE.WEAK_HEAL_ATTACK:
        effectType = TargetMarker.EFFECT_TYPE.WEAK_HEAL_ATTACK;
        break;
      case Enemy.WEAK_STATE.WEAK_CANNON:
        effectType = TargetMarker.EFFECT_TYPE.WEAK_CANNON;
        break;
      case Enemy.WEAK_STATE.WEAK_ELEMENT_SP_ATTACK:
        effectType = TargetMarker.EFFECT_TYPE.WEAK_ELEMENT_SP_ATTACK;
        break;
    }
    return effectType;
  }

  public Enemy.RegionInfo[] convertRegionInfos { get; set; }

  public Collider[] colliders { get; protected set; }

  public TargetPoint[] targetPoints { get; protected set; }

  public RegionRoot[] regionRoots { get; protected set; }

  public bool enableTargetPoint { get; protected set; }

  public EnemyRegionWork[] regionWorks { get; set; }

  public bool isDispWeakMark()
  {
    for (int index = 0; index < this.regionWorks.Length; ++index)
    {
      if (this.regionWorks[index].weakState == Enemy.WEAK_STATE.WEAK)
        return true;
    }
    return false;
  }

  public int downMax
  {
    get
    {
      if (this.downMaxRate != null)
      {
        int length = this.downMaxRate.Length;
        if (length > 0)
          return (int) ((double) this._downMax * (double) this.downMaxRate[this.downCount < length - 1 ? this.downCount : length - 1]);
      }
      return this._downMax;
    }
  }

  public bool reviveRegionWaitSync { get; protected set; }

  public bool enableDash { get; protected set; }

  public bool warpWaitSync { get; protected set; }

  public string baseHitMaterialName { get; set; }

  public EnemyPacketReceiver enemyReceiver => (EnemyPacketReceiver) this.packetReceiver;

  public EnemyPacketSender enemySender => (EnemyPacketSender) this.packetSender;

  public List<AttackActionMine> GetActionMineList() => this.m_activeAttackActionMineList;

  public DrainAttackInfo SearchDrainAttackInfo(int id)
  {
    if (this.drainAtkInfos == null)
      return (DrainAttackInfo) null;
    foreach (DrainAttackInfo drainAtkInfo in this.drainAtkInfos)
    {
      if (drainAtkInfo.id == id)
        return drainAtkInfo;
    }
    return (DrainAttackInfo) null;
  }

  public int BarrierHpMax
  {
    get => (int) this.m_barrierHpMax;
    set => this.m_barrierHpMax = (XorInt) value;
  }

  public XorInt BarrierHp
  {
    get => this.m_barrierHp;
    set => this.m_barrierHp = value;
  }

  public bool IsValidBarrier => this.BarrierHpMax > 0 && (int) this.BarrierHp > 0;

  public AtkAttribute GhostFormParam { get; set; }

  public GhostFormShaderParam GhostFormShaderParam { get; set; }

  public AutoBuffParam[] AutoBuffParamList { get; set; }

  public float DizzyReactionLoopTime { get; set; }

  public XorInt GrabHpMax { get; set; }

  public XorInt GrabHp { get; set; }

  public XorInt GrabCannonDamage { get; set; }

  public bool IsValidGrabHp => (int) this.GrabHpMax > 0 && (int) this.GrabHp > 0;

  public int ExActionID { get; set; }

  public int ExActionCondition { get; set; }

  public int ExActionConditionValue { get; set; }

  public StackBuffController stackBuffCtrl { get; protected set; }

  public bool isAbleToSkipAction { get; protected set; }

  public bool enableToSkipActionByDamage { get; protected set; }

  protected override void OnEnable()
  {
    base.OnEnable();
    if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid() && this.isBoss && Object.op_Equality((Object) MonoBehaviourSingleton<StageObjectManager>.I.boss, (Object) this))
      MonoBehaviourSingleton<UIEnemyStatus>.I.SetTarget(this);
    if (MonoBehaviourSingleton<DropTargetMarkerManeger>.IsValid())
      MonoBehaviourSingleton<DropTargetMarkerManeger>.I.CheckTarget(this);
    if (this.isRequireGhostShaderParam)
    {
      this.isRequireGhostShaderParam = false;
      this.ChangeGhostShaderParam(this.ghostBuffEndParam, this.ghostBuffDuration);
    }
    if (!this.isHideSpawn)
      return;
    if (this.isHiding)
      this.InitHide();
    else
      this.TurnUpImmediate();
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    if (this.forceEnemyOutCoroutine != null)
    {
      this.StopForceEnemyOut();
      MonoBehaviourSingleton<CoopNetworkManager>.I.EnemyOut(this.id, this._position);
    }
    if (this.isBoss && Object.op_Equality((Object) MonoBehaviourSingleton<StageObjectManager>.I.boss, (Object) this) && MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
      MonoBehaviourSingleton<UIEnemyStatus>.I.SetTarget((Enemy) null);
    this.DeleteStatusGizmo();
  }

  public void CreateStatusGizmo()
  {
    if (this.isBoss || this.isSummonAttack || Object.op_Inequality((Object) this.uiEnemyStatusGizmo, (Object) null) || this.isHiding || !MonoBehaviourSingleton<UIStatusGizmoManager>.IsValid())
      return;
    this.uiEnemyStatusGizmo = MonoBehaviourSingleton<UIStatusGizmoManager>.I.Create(this);
  }

  public void DeleteStatusGizmo()
  {
    if (!Object.op_Inequality((Object) this.uiEnemyStatusGizmo, (Object) null))
      return;
    this.uiEnemyStatusGizmo.targetEnemy = (Enemy) null;
    this.uiEnemyStatusGizmo = (UIEnemyStatusGizmo) null;
  }

  protected override void Awake()
  {
    base.Awake();
    this.enemyPopIndex = -1;
    this.enemyTableData = (EnemyTable.EnemyData) null;
    this.objectType = StageObject.OBJECT_TYPE.ENEMY;
    this.enableTargetPoint = true;
    this.reviveRegionWaitSync = false;
    this.warpWaitSync = false;
    this.stackBuffCtrl = new StackBuffController();
    this.loader = ((Component) this).gameObject.AddComponent<EnemyLoader>();
    this.enemyParameter = MonoBehaviourSingleton<InGameSettingsManager>.I.enemy;
    this.downMaxRate = this.enemyParameter.downMaxRate;
    this.ResetConcussion(true);
    this.isPierceAfterTarget = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.isPierceAfterTarget;
    EnemyParam componentInChildren = ((Component) this).gameObject.GetComponentInChildren<EnemyParam>();
    if (Object.op_Inequality((Object) componentInChildren, (Object) null))
    {
      componentInChildren.SetParam(this);
      Object.DestroyImmediate((Object) componentInChildren);
    }
    if (Object.op_Equality((Object) this._rigidbody, (Object) null))
      this._rigidbody = ((Component) this).gameObject.AddComponent<Rigidbody>();
    this._rigidbody.collisionDetectionMode = (CollisionDetectionMode) 1;
    this._rigidbody.mass = 1000f;
    this._rigidbody.angularDrag = 100f;
    this._rigidbody.isKinematic = false;
    this._rigidbody.constraints = (RigidbodyConstraints) 116;
    ((Component) this).gameObject.layer = 10;
    this.stackBuffCtrl.Init();
  }

  protected override void Clear()
  {
    base.Clear();
    this.regionInfos = (Enemy.RegionInfo[]) null;
    this.colliders = (Collider[]) null;
    this.targetPoints = (TargetPoint[]) null;
    this.regionRoots = (RegionRoot[]) null;
    this.m_nowAngryId = 0U;
    if (this.m_execAngryIds == null)
      return;
    this.m_execAngryIds.Clear();
  }

  public override void OnLoadComplete()
  {
    if (Object.op_Equality((Object) this._collider, (Object) null))
    {
      SphereCollider sphereCollider = ((Component) this).gameObject.AddComponent<SphereCollider>();
      sphereCollider.radius = this.bodyRadius;
      sphereCollider.center = new Vector3(0.0f, this.bodyRadius, 0.0f);
      this._collider = (Collider) sphereCollider;
    }
    Rigidbody rigidbody = ((Component) this.body).gameObject.GetComponentInChildren<Rigidbody>();
    if (Object.op_Equality((Object) rigidbody, (Object) null))
      rigidbody = ((Component) this.body).gameObject.AddComponent<Rigidbody>();
    rigidbody.useGravity = false;
    rigidbody.isKinematic = true;
    this.SetEnemyTableData();
    base.OnLoadComplete();
    this.SetAnimUpdatePhysics(this.isBoss);
    this.InitializeRegionWork();
    this.BarrierHp = (XorInt) this.BarrierHpMax;
    this.ignoreHitAttackColliders.Clear();
    EnemyColliderSettings[] componentsInChildren1 = ((Component) this.body).GetComponentsInChildren<EnemyColliderSettings>();
    int index1 = 0;
    for (int length = componentsInChildren1.Length; index1 < length; ++index1)
    {
      if (!Object.op_Equality((Object) componentsInChildren1[index1].targetCollider, (Object) null) && componentsInChildren1[index1].ignoreHitAttack)
        this.ignoreHitAttackColliders.Add(componentsInChildren1[index1].targetCollider);
    }
    if (this.isSummonAttack)
    {
      Utility.SetLayerWithChildren(this._transform, 15);
      ((Component) this).gameObject.layer = 15;
    }
    else
    {
      Utility.SetLayerWithChildren(this._transform, 11);
      ((Component) this).gameObject.layer = 10;
    }
    this.colliders = ((Component) this).gameObject.GetComponentsInChildren<Collider>();
    this.targetPoints = ((Component) this).gameObject.GetComponentsInChildren<TargetPoint>();
    this.regionRoots = ((Component) this).gameObject.GetComponentsInChildren<RegionRoot>();
    int index2 = 0;
    for (int length1 = this.targetPoints.Length; index2 < length1; ++index2)
    {
      this.targetPoints[index2].owner = (StageObject) this;
      int index3 = 0;
      for (int length2 = this.regionRoots.Length; index3 < length2; ++index3)
      {
        if (Array.IndexOf<int>(this.regionRoots[index3].subRegionIDs, this.targetPoints[index2].regionID) >= 0)
          this.targetPoints[index2].subRegionRoot = this.regionRoots[index3];
      }
    }
    int index4 = 0;
    for (int length = this.regionRoots.Length; index4 < length; ++index4)
    {
      if (this.regionRoots[index4].isDeactive)
        ((Component) this.regionRoots[index4]).gameObject.SetActive(false);
    }
    if (Object.op_Inequality((Object) this.stepCtrl, (Object) null))
      this.stepCtrl.stampDistance = this.enemyParameter.stampDistance;
    if (MonoBehaviourSingleton<TargetMarkerManager>.IsValid())
      MonoBehaviourSingleton<TargetMarkerManager>.I.updateShadowSealingFlag = true;
    if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid() && this.isBoss)
      MonoBehaviourSingleton<UIEnemyStatus>.I.SetTarget(this);
    if (MonoBehaviourSingleton<DropTargetMarkerManeger>.IsValid())
      MonoBehaviourSingleton<DropTargetMarkerManeger>.I.CheckTarget(this);
    if (!this.willStock)
    {
      this.AutoBuffProc();
      if (this.isHideSpawn)
      {
        this.isHiding = true;
        this.InitHide();
      }
    }
    this.InitializeBarrierEffect();
    this.tailController = ((Component) this).gameObject.GetComponentInChildren<TailController>(true);
    this.willStock = false;
    ColliderWeightCtl[] componentsInChildren2 = ((Component) this).gameObject.GetComponentsInChildren<ColliderWeightCtl>();
    int index5 = 0;
    for (int length = componentsInChildren2.Length; index5 < length; ++index5)
      componentsInChildren2[index5].SetAnimator(this.loader.GetAnimator());
    TargetPointWeightCtl[] componentsInChildren3 = ((Component) this).gameObject.GetComponentsInChildren<TargetPointWeightCtl>();
    int index6 = 0;
    for (int length = componentsInChildren3.Length; index6 < length; ++index6)
      componentsInChildren3[index6].SetAnimator(this.loader.GetAnimator());
    this.skinnedMeshRendererList = ((Component) this).GetComponentsInChildren<SkinnedMeshRenderer>();
    if (!MonoBehaviourSingleton<InGameManager>.IsValid() || !MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
      return;
    this.arenaConditionList = MonoBehaviourSingleton<InGameManager>.I.GetArenaConditions();
    if (((IList<ARENA_CONDITION>) this.arenaConditionList).IsNullOrEmpty<ARENA_CONDITION>())
      return;
    for (int index7 = 0; index7 < this.arenaConditionList.Length; ++index7)
    {
      switch (this.arenaConditionList[index7])
      {
        case ARENA_CONDITION.DAMAGE_OFF_WEAPON:
          this.isArenaDamageOffWeapon = true;
          break;
        case ARENA_CONDITION.DAMAGE_OFF_MAGI:
          this.isArenaDamageOffMagi = true;
          break;
      }
    }
  }

  private void AutoBuffProc()
  {
    if (this.AutoBuffParamList == null || this.AutoBuffParamList.Length == 0)
      return;
    foreach (AutoBuffParam autoBuffParam in this.AutoBuffParamList)
    {
      float num = autoBuffParam.time;
      if ((double) num < 0.0)
        num = 43200f;
      this.OnBuffStart(new BuffParam.BuffData()
      {
        type = autoBuffParam.type,
        time = num,
        value = autoBuffParam.value
      });
    }
  }

  private void InitializeRegionWork()
  {
    if (this.regionWorks == null)
      this.regionWorks = new EnemyRegionWork[this.regionInfos.Length];
    for (int _regionId = 0; _regionId < this.regionWorks.Length; ++_regionId)
    {
      Enemy.RegionInfo regionInfo1 = this.regionInfos[_regionId];
      int parentRegionId = this.SearchRegionID(regionInfo1.parentRegionName);
      if (this.regionWorks[_regionId] == null)
        this.regionWorks[_regionId] = new EnemyRegionWork();
      this.regionWorks[_regionId].Initialize(regionInfo1, parentRegionId, _regionId);
      if ((int) this.regionWorks[_regionId].hp > 0)
      {
        Enemy.RegionInfo regionInfo2 = this.regionInfos[_regionId];
        for (int index = 0; index < regionInfo2.deactivateObjects.Length; ++index)
        {
          Transform node = this.FindNode(regionInfo2.deactivateObjects[index]);
          if (!Object.op_Equality((Object) node, (Object) null))
            ((Component) node).gameObject.SetActive(true);
        }
      }
    }
    if (this.regionWorks.Length == 0)
      return;
    this.hpMax = this.hp = (int) this.regionWorks[0].hp;
    if (this.madModeHpThreshold > 0 && (int) this.enemyLevel >= this.madModeLvThreshold)
      this.madModeHp = Mathf.CeilToInt((float) this.madModeHpThreshold * 0.01f * (float) this.hpMax);
    else
      this.madModeHp = 0;
  }

  private int SearchRegionID(string targetRegionName)
  {
    if (string.IsNullOrEmpty(targetRegionName) || this.regionInfos == null || this.regionInfos.Length == 0)
      return -1;
    for (int index = 0; index < this.regionInfos.Length; ++index)
    {
      if (this.regionInfos[index].name == targetRegionName)
        return index;
    }
    return -1;
  }

  private void SetEnemyTableData()
  {
    if (this.enemyTableData == null)
      return;
    float hpRate;
    float atkRate;
    if (this.growTableData != null)
    {
      hpRate = this.growTableData.hpRate;
      atkRate = this.growTableData.atkRate;
    }
    else
    {
      hpRate = (float) this.enemyTableData.hpRate;
      atkRate = (float) this.enemyTableData.atkRate;
    }
    if (this.regionInfos == null)
      Log.Error("regionInfos is null. Check enemy data.");
    if (!string.IsNullOrEmpty(this.enemyTableData.convertRegionKey))
    {
      int index1 = 0;
      for (int length1 = this.attackInfos.Length; index1 < length1; ++index1)
      {
        string str = $"{this.attackInfos[index1].name}_{this.enemyTableData.convertRegionKey}";
        int index2 = 0;
        for (int length2 = this.convertAttackInfos.Length; index2 < length2; ++index2)
        {
          if (this.convertAttackInfos[index2].name == str)
          {
            this.convertAttackInfos[index2].name = this.attackInfos[index1].name;
            this.attackInfos[index1] = this.convertAttackInfos[index2];
            break;
          }
        }
      }
      int index3 = 0;
      for (int length3 = this.regionInfos.Length; index3 < length3; ++index3)
      {
        string str = $"{this.regionInfos[index3].name}_{this.enemyTableData.convertRegionKey}";
        int index4 = 0;
        for (int length4 = this.convertRegionInfos.Length; index4 < length4; ++index4)
        {
          if (this.convertRegionInfos[index4].name == str)
          {
            this.convertRegionInfos[index4].name = this.regionInfos[index3].name;
            this.regionInfos[index3] = this.convertRegionInfos[index4];
            break;
          }
        }
      }
    }
    int index5 = 0;
    for (int length = this.regionInfos.Length; index5 < length; ++index5)
    {
      float num = (float) this.regionInfos[index5].maxHP * hpRate;
      this.regionInfos[index5].maxHP = (int) ((double) num + 1.0 / 1000.0);
      this.regionInfos[index5].tolerance.InitializeElementTolerance(this.converteElementToleranceTable);
    }
    int index6 = 0;
    for (int length = this.attackInfos.Length; index6 < length; ++index6)
    {
      if (this.attackInfos[index6] is AttackHitInfo attackInfo)
        attackInfo.atkRate = atkRate;
    }
  }

  protected override void Initialize()
  {
    base.Initialize();
    if (this.isBoss)
      return;
    this.isLocalDamageApply = true;
    this.localDamage = 0;
  }

  public void ApplyExploreBossStatus(ExploreBossStatus status)
  {
    if (status == null)
      return;
    this.hp = (int) status.hp;
    this.BarrierHp = status.barrierHp;
    this.downCount = (int) status.downCount;
    this.ShieldHp = status.shieldHp;
    this.deadReviveCount = status.deadReviveCount;
    if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
      MonoBehaviourSingleton<InGameRecorder>.I.SetEnemyRecoveredHP(this.id, status.recoveredHP);
    if (status.regionWorks != null)
    {
      for (int index = 0; index < this.regionWorks.Length; ++index)
      {
        if (status.regionWorks.Length > index)
          this.regionWorks[index].CopyFrom(status.regionWorks[index]);
      }
    }
    this.UpdateRegionVisual();
    if (this.IsValidShield())
      this.RequestShieldShaderEffect();
    this.NowAngryID = status.nowAngryId;
    this.m_execAngryIds.Clear();
    if (status.execAngryIds != null && status.execAngryIds.Length != 0)
      this.m_execAngryIds.AddRange((IEnumerable<uint>) status.execAngryIds);
    if (!status.isMadMode)
      return;
    this.LocalMadModeStart();
  }

  protected override uint GetVoiceChannel() => 1;

  protected override bool EnablePlaySound()
  {
    return !MonoBehaviourSingleton<InGameProgress>.IsValid() || this.isBoss || !MonoBehaviourSingleton<InGameProgress>.I.isHappenQuestDirection;
  }

  public override bool DestroyObject()
  {
    if (this.isLoading)
    {
      this.isDestroyWaitFlag = true;
      return false;
    }
    this.aegisCtrl = (EnemyAegisController) null;
    if (this.isBoss)
      return base.DestroyObject();
    if (Object.op_Inequality((Object) this.packetSender, (Object) null))
      this.packetSender.OnDestroyObject();
    ((Component) this).gameObject.SetActive(false);
    if (!this.isSummonAttack)
      MonoBehaviourSingleton<StageObjectManager>.I.enemyStokeList.Add(this);
    this.isDestroyWaitFlag = false;
    return true;
  }

  public void ClearDead()
  {
    this._collider.enabled = true;
    if (this.colliders != null)
    {
      int index = 0;
      for (int length = this.colliders.Length; index < length; ++index)
        this.colliders[index].enabled = true;
    }
    this.badStatusMax.Copy(this.badStatusBase);
    this.isDead = false;
    this.hitOffFlag = StageObject.HIT_OFF_FLAG.NONE;
    this.isSetAppearPos = false;
    this.isStoke = true;
    this.isCoopInitialized = false;
    this.animatorBoolList.Clear();
    this.changeTriggerList.Clear();
    this.ActIdle();
    this.localDamage = 0;
    this.isRequireGhostShaderParam = false;
    this.isHiding = this.isHideSpawn;
    this.isFirstMadMode = false;
    EnemyController controller = this.controller as EnemyController;
    if (Object.op_Inequality((Object) controller, (Object) null))
      controller.Reset();
    this.InitializeRegionWork();
    this.BarrierHp = (XorInt) this.BarrierHpMax;
    this.deadReviveCount = 0;
    this.StopForceEnemyOut();
    this.AutoBuffProc();
  }

  protected override void Update()
  {
    base.Update();
    if (this.hitShockLightFlag)
    {
      this.hitShockLightTime += Time.deltaTime;
      if ((double) this.hitShockLightTime >= (double) this.enemyParameter.hitShockLightTime)
      {
        this.loader.ResetRimParams();
        this.hitShockLightFlag = false;
        this.hitShockLightTime = 0.0f;
      }
      else if (this.loader.materialParamsList != null)
      {
        int ID_RIM_POWER = Shader.PropertyToID("_RimPower");
        int ID_RIM_WIDTH = Shader.PropertyToID("_RimWidth");
        float sin = Mathf.Sin(3.14159274f * (this.hitShockLightTime / this.enemyParameter.hitShockLightTime));
        this.loader.materialParamsList.ForEach((Action<EnemyLoader.MaterialParams>) (prm =>
        {
          if (prm.hasRimPower)
          {
            float num = prm.defaultRimPower + (this.enemyParameter.hitShockLightRimPower - prm.defaultRimPower) * sin;
            prm.material.SetFloat(ID_RIM_POWER, num);
          }
          if (!prm.hasRimWidth)
            return;
          float num1 = prm.defaultRimWidth + (this.enemyParameter.hitShockLightRimWidth - prm.defaultRimWidth) * sin;
          prm.material.SetFloat(ID_RIM_WIDTH, num1);
        }));
      }
    }
    if (this.warpViewFlag)
    {
      this.warpViewRate += this.warpViewRatePerTime * Time.deltaTime;
      if ((double) this.warpViewRatePerTime >= 0.0 && (double) this.warpViewRate >= 1.0)
      {
        this.warpViewRate = 1f;
        this.warpViewFlag = false;
      }
      else if ((double) this.warpViewRatePerTime < 0.0 && (double) this.warpViewRate <= 0.0)
      {
        this.warpViewRate = 0.0f;
        this.warpViewFlag = false;
      }
      this.SetWarpVisible(this.warpViewRate);
    }
    else if ((double) this.warpViewRate > 0.0)
      this.SetWarpVisible(this.warpViewRate);
    if (!this.IsConcussion())
    {
      this.downHealInterval -= Time.deltaTime;
      if ((double) this.downHealInterval <= 0.0)
      {
        this.downHealInterval = 0.0f;
        this.downTotal -= this.downHeal * Time.deltaTime;
        if ((double) this.downTotal < 0.0)
          this.downTotal = 0.0f;
      }
    }
    if (this.hitShockOffsetFlag)
    {
      this.hitShockOffsetTime += Time.deltaTime;
      if ((double) this.hitShockOffsetTime >= (double) this.enemyParameter.hitShockOffsetTime)
      {
        this.body.localPosition = Vector3.zero;
        this.hitShockOffset = Vector3.zero;
        this.hitShockOffsetFlag = false;
        this.hitShockOffsetTime = 0.0f;
      }
      else
      {
        float num = this.hitShockOffsetTime / this.enemyParameter.hitShockOffsetTime;
        Transform body1 = this.body;
        body1.position = Vector3.op_Subtraction(body1.position, this.hitShockOffset);
        this.hitShockOffset = Vector3.op_Multiply(this.hitShockVec, this.enemyParameter.hitShockOffsetLength * Mathf.Sin(3.14159274f * num));
        Transform body2 = this.body;
        body2.position = Vector3.op_Addition(body2.position, this.hitShockOffset);
      }
    }
    this.UpdateRandomShot();
    if (this.regionWorks != null)
    {
      int length = this.regionWorks.Length;
      for (int index = 0; index < length; ++index)
        this.regionWorks[index].Update();
    }
    this._UpdateBleed();
    this._UpdateShadowSealing();
    this._UpdateBombArrow();
    if (this.isInitialized && this.isBoss)
      this.DrainRecoverProc();
    int count = this.animLayerWeightChangeInfo.Count;
    if (count > 0)
    {
      Animator animator = this.loader.GetAnimator();
      for (int index = 0; index < count; ++index)
      {
        Enemy.AnimationLayerWeightChangeInfo weightChangeInfo = this.animLayerWeightChangeInfo[index];
        if (weightChangeInfo.aliveFlag)
        {
          float num = weightChangeInfo.spd * Time.deltaTime;
          if ((double) num > 0.0)
          {
            if ((double) weightChangeInfo.weight + (double) num >= (double) weightChangeInfo.target)
            {
              weightChangeInfo.weight = weightChangeInfo.target;
              weightChangeInfo.aliveFlag = false;
            }
            else
              weightChangeInfo.weight += num;
          }
          else if ((double) weightChangeInfo.weight + (double) num <= (double) weightChangeInfo.target)
          {
            weightChangeInfo.weight = weightChangeInfo.target;
            weightChangeInfo.aliveFlag = false;
          }
          else
            weightChangeInfo.weight += num;
          animator.SetLayerWeight(weightChangeInfo.layerIndex, weightChangeInfo.weight);
        }
      }
    }
    if (this.blendColorCtrl != null)
      this.blendColorCtrl.Update();
    if (!MonoBehaviourSingleton<InGameCameraCuller>.IsValid() || this.isBoss || !((Component) this).transform.hasChanged && !MonoBehaviourSingleton<InGameCameraCuller>.I.IsUpdated)
      return;
    ((Component) this).transform.hasChanged = false;
    if (this.skinnedMeshRendererList == null)
      return;
    int index1 = 0;
    for (int length = this.skinnedMeshRendererList.Length; index1 < length; ++index1)
      ((Renderer) this.skinnedMeshRendererList[index1]).enabled = this.CheckShow(((Renderer) this.skinnedMeshRendererList[index1]).bounds);
  }

  private void _UpdateBleed()
  {
    if (!this.IsCoopNone() && !this.IsOriginal() || this.regionWorks == null)
      return;
    double bleedCounter = (double) this.bleedCounter;
    this.bleedCounter += Time.deltaTime;
    float bleedTimeInterval = MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.arrowBleedTimeInterval;
    double num1 = (double) bleedTimeInterval;
    if ((int) (bleedCounter / num1) == (int) ((double) this.bleedCounter / (double) bleedTimeInterval))
      return;
    bool flag1 = false;
    Enemy.BleedSyncData sync_data = new Enemy.BleedSyncData();
    sync_data.afterHP = this.hp;
    int index1 = 0;
    for (int length = this.regionWorks.Length; index1 < length; ++index1)
    {
      bool flag2 = false;
      if ((int) this.regionWorks[index1].hp <= 0 && this.regionInfos[index1].maxHP > 0)
        flag2 = true;
      if (this.regionInfos[index1].breakAfterHit || !flag2)
      {
        Enemy.BleedSyncData.BleedRegionWork bleedRegionWork = (Enemy.BleedSyncData.BleedRegionWork) null;
        int index2 = 0;
        for (int count = this.regionWorks[index1].bleedList.Count; index2 < count; ++index2)
        {
          Enemy.BleedData bleed = this.regionWorks[index1].bleedList[index2];
          flag1 = true;
          if (bleedRegionWork == null)
          {
            bleedRegionWork = new Enemy.BleedSyncData.BleedRegionWork();
            bleedRegionWork.id = index1;
            bleedRegionWork.afterHP = (int) this.regionWorks[index1].hp;
            bleedRegionWork.damageList = new List<Enemy.BleedSyncData.BleedDamageData>();
          }
          Enemy.BleedSyncData.BleedDamageData bleedDamageData = new Enemy.BleedSyncData.BleedDamageData();
          bleedRegionWork.damageList.Add(bleedDamageData);
          if (bleed.skipFirst)
          {
            bleedDamageData.ownerID = bleed.ownerID;
            bleedDamageData.damage = 0;
          }
          else
          {
            int num2 = Mathf.CeilToInt((float) bleed.damage);
            if (sync_data.afterHP > 0 && !this.regionInfos[index1].dragonArmorInfo.enabled)
            {
              sync_data.afterHP -= num2;
              if (sync_data.afterHP < 1)
                sync_data.afterHP = 1;
            }
            if (bleedRegionWork.afterHP > 0)
            {
              bleedRegionWork.afterHP -= num2;
              if (bleedRegionWork.afterHP < 1)
                bleedRegionWork.afterHP = 1;
            }
            bleedDamageData.ownerID = bleed.ownerID;
            bleedDamageData.damage = num2;
          }
        }
        if (bleedRegionWork != null)
          sync_data.regionWorks.Add(bleedRegionWork);
      }
    }
    if (!flag1)
      return;
    this.OnUpdateBleedDamage(sync_data);
  }

  private void _UpdateShadowSealing()
  {
    if (this.IsDebuffShadowSealing() || !this.IsCoopNone() && !this.IsOriginal() || this.regionWorks == null)
      return;
    int index = 0;
    for (int length = this.regionWorks.Length; index < length; ++index)
    {
      Enemy.ShadowSealingData shadowSealingData = this.regionWorks[index].shadowSealingData;
      if (shadowSealingData.ownerID != 0)
      {
        shadowSealingData.existSec -= Time.deltaTime;
        if ((double) shadowSealingData.existSec <= 0.0)
          this.OnUpdateShadowSealing(new Enemy.ShadowSealingSyncData()
          {
            regionIndex = index
          });
      }
    }
  }

  private void _UpdateBombArrow()
  {
    if (!this.IsCoopNone() && !this.IsOriginal() || this.regionWorks == null)
      return;
    for (int regionId = 0; regionId < this.regionWorks.Length; ++regionId)
    {
      Enemy.BombArrowData bombArrowData = this.regionWorks[regionId].GetBombArrowData();
      if (bombArrowData != null && (bombArrowData.ownerID == 0 || this.isDead || (double) bombArrowData.GetRemainingCount() <= 0.0))
      {
        this.OnUpdateBombArrow(regionId);
        if (Object.op_Inequality((Object) this.enemySender, (Object) null))
          this.enemySender.OnUpdateBombArrow(regionId);
      }
    }
  }

  private void DrainRecoverProc()
  {
    if (!this.IsCoopNone() && !this.IsOriginal())
      return;
    EnemyBrain brain = this.controller.brain as EnemyBrain;
    if (Object.op_Equality((Object) brain, (Object) null))
      return;
    GrabController grabController = brain.actionCtrl.grabController;
    if (grabController == null || !grabController.IsGrabing())
      return;
    DrainAttackInfo drainAtkInfo = grabController.drainAtkInfo;
    if (drainAtkInfo == null || !grabController.IsAliveGrabbedPlayerAll())
      return;
    int recoverValue = (int) ((double) this.hpMax * (double) (drainAtkInfo.recoverRate * 0.01f));
    if (recoverValue <= 0 || this.hp >= this.hpMax)
      return;
    this.grabDrainRecoverTimer -= Time.deltaTime;
    if ((double) this.grabDrainRecoverTimer > 0.0)
      return;
    this.RecoverHp(recoverValue, true);
    this.grabDrainRecoverTimer = drainAtkInfo.recoverInterval;
  }

  public override void RecoverHp(int recoverValue, bool isSend)
  {
    int num = 0;
    this.hp += recoverValue;
    if (this.hp > this.hpMax)
    {
      num = this.hp - this.hpMax;
      this.hp = this.hpMax;
    }
    if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
      MonoBehaviourSingleton<InGameRecorder>.I.RecordEnemyRecoveredHP(this.id, recoverValue - num);
    if (MonoBehaviourSingleton<UIDamageManager>.IsValid())
      MonoBehaviourSingleton<UIDamageManager>.I.CreateEnemyRecoverHp((Character) this, recoverValue, UIPlayerDamageNum.DAMAGE_COLOR.HEAL);
    if (Object.op_Inequality((Object) this.enemySender, (Object) null) & isSend)
      this.enemySender.OnRecoverHp(recoverValue);
    if (!Object.op_Inequality((Object) this.effectPlayProcessor, (Object) null) || !Object.op_Equality((Object) this.effectDrainRecover, (Object) null))
      return;
    List<EffectPlayProcessor.EffectSetting> settings = this.effectPlayProcessor.GetSettings("RECOVER_HP");
    if (settings == null || settings.Count <= 0)
      return;
    Transform transform = this.effectPlayProcessor.PlayEffect(settings[0], this._transform);
    if (!Object.op_Inequality((Object) transform, (Object) null))
      return;
    this.effectDrainRecover = ((Component) transform).gameObject;
  }

  protected override void FixedUpdate()
  {
    if (this.actionID == Character.ACTION_ID.ATTACK && this.enableDash)
    {
      if (this.IsWallStay())
      {
        this.SetDashEnd();
      }
      else
      {
        Vector3 vector3_1 = Vector3.op_Subtraction(this._position, this.dashBeforePos);
        vector3_1.y = 0.0f;
        this.dashNowDistance += ((Vector3) ref vector3_1).magnitude;
        this.dashBeforePos = this._position;
        bool flag = (double) this.dashNowDistance >= (double) this.dashMinDistance;
        if (!this.actionPositionFlag)
        {
          if (flag)
          {
            this.SetDashEnd();
            goto label_13;
          }
        }
        else if (!this.dashOverFlag)
        {
          if ((double) this.dashNowDistance >= (double) this.dashMaxDistance)
          {
            this.dashOverFlag = true;
            this.dashOverCheckDistance = this.dashNowDistance;
          }
          else
          {
            Vector3 vector3_2 = Vector3.op_Subtraction(this.actionPosition, this._position);
            vector3_2.y = 0.0f;
            Vector3 forward = this._forward;
            forward.y = 0.0f;
            ((Vector3) ref forward).Normalize();
            if ((double) Vector3.Angle(forward, vector3_2) > 90.0)
            {
              this.dashOverFlag = true;
              this.dashOverCheckDistance = this.dashNowDistance;
            }
          }
        }
        if (this.dashOverFlag && flag && (double) this.dashNowDistance - (double) this.dashOverCheckDistance >= (double) this.dashOverDistance)
          this.SetDashEnd();
      }
    }
label_13:
    base.FixedUpdate();
  }

  protected override void LateUpdate()
  {
    base.LateUpdate();
    if (Enemy.updateFrame == Time.frameCount)
      return;
    Enemy.updateFrame = Time.frameCount;
    Enemy.selfHitEffectCool -= Time.deltaTime;
    if ((double) Enemy.selfHitEffectCool < 0.0)
      Enemy.selfHitEffectCool = 0.0f;
    Enemy.otherHitEffectCool -= Time.deltaTime;
    if ((double) Enemy.otherHitEffectCool >= 0.0)
      return;
    Enemy.otherHitEffectCool = 0.0f;
  }

  public override void SetActionTarget(StageObject target, bool send = true)
  {
    bool flag = false;
    if (Object.op_Inequality((Object) this.actionTarget, (Object) target))
      flag = true;
    this.actionTarget = target;
    if (!(send & flag) || !Object.op_Inequality((Object) this.characterSender, (Object) null))
      return;
    this.characterSender.OnSetActionTarget(target);
  }

  public void OnUpdateBleedDamage(Enemy.BleedSyncData sync_data)
  {
    int num1 = this.hp - sync_data.afterHP;
    if (num1 < 0)
      num1 = 0;
    this.hp = sync_data.afterHP;
    if (this.regionWorks != null)
    {
      int index1 = 0;
      for (int length = this.regionWorks.Length; index1 < length; ++index1)
      {
        Enemy.BleedSyncData.BleedRegionWork bleedRegionWork = (Enemy.BleedSyncData.BleedRegionWork) null;
        int index2 = 0;
        for (int count = sync_data.regionWorks.Count; index2 < count; ++index2)
        {
          if (sync_data.regionWorks[index2].id == index1)
          {
            bleedRegionWork = sync_data.regionWorks[index2];
            this.regionWorks[index1].hp = (XorInt) bleedRegionWork.afterHP;
            break;
          }
        }
        int index3 = 0;
        while (index3 < this.regionWorks[index1].bleedList.Count)
        {
          Enemy.BleedData bleed = this.regionWorks[index1].bleedList[index3];
          Enemy.BleedWork bleedWork = (Enemy.BleedWork) null;
          int index4 = 0;
          for (int count = this.regionWorks[index1].bleedWorkList.Count; index4 < count; ++index4)
          {
            if (this.regionWorks[index1].bleedWorkList[index4].ownerID == bleed.ownerID)
            {
              bleedWork = this.regionWorks[index1].bleedWorkList[index4];
              break;
            }
          }
          Enemy.BleedSyncData.BleedDamageData bleed_damage = (Enemy.BleedSyncData.BleedDamageData) null;
          if (bleedRegionWork != null)
          {
            int index5 = 0;
            for (int count = bleedRegionWork.damageList.Count; index5 < count; ++index5)
            {
              if (bleedRegionWork.damageList[index5].ownerID == bleed.ownerID)
              {
                bleed_damage = bleedRegionWork.damageList[index5];
                break;
              }
            }
          }
          bool flag1 = false;
          bool flag2 = false;
          if (bleed.skipFirst)
          {
            bleed.skipFirst = false;
            if (bleed_damage != null && bleed_damage.damage == 0)
              flag2 = true;
          }
          if (bleed_damage == null)
            flag1 = true;
          else if (!flag2)
          {
            --bleed.cnt;
            if (bleed.cnt <= 0)
              flag1 = true;
          }
          if (bleed_damage != null && !flag2)
          {
            if (MonoBehaviourSingleton<CoopManager>.IsValid())
              MonoBehaviourSingleton<CoopManager>.I.coopStage.battleUserLog.Add((Character) this, bleed_damage);
            int damage1 = bleed_damage.damage;
            if (damage1 > num1)
              damage1 = num1;
            if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
              MonoBehaviourSingleton<InGameRecorder>.I.RecordGivenDamage(bleed_damage.ownerID, damage1);
            if (QuestManager.IsValidInGameExplore() && this.isBoss && bleed.IsOwnerSelf())
            {
              ExplorePlayerStatus explorePlayerStatus = MonoBehaviourSingleton<QuestManager>.I.GetMyExplorePlayerStatus();
              int num2 = explorePlayerStatus.givenTotalDamage + damage1;
              explorePlayerStatus.SyncTotalDamageToBoss(num2);
              MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendExploreBossDamage(num2);
            }
            num1 -= damage1;
            if (bleed.IsOwnerSelf() && bleedWork != null && Object.op_Inequality((Object) bleedWork.bleedEffect, (Object) null))
            {
              if (this.enemyParameter.showDamageNum && MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.arrowBleedShowDamage)
              {
                AtkAttribute damage2 = new AtkAttribute();
                damage2.normal = (float) bleed_damage.damage;
                bool enabled = this.regionWorks[index1].regionInfo.dragonArmorInfo.enabled;
                this.CreateDamageNum(bleedWork.bleedEffect.position, damage2, false, enabled);
              }
              if ((double) this.warpViewRate <= 0.0)
              {
                Transform effect = EffectManager.GetEffect(MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.arrowBleedDamageEffectName, bleedWork.bleedEffect.parent);
                if (Object.op_Inequality((Object) effect, (Object) null))
                {
                  effect.localScale = bleedWork.bleedEffect.localScale;
                  effect.localPosition = bleedWork.bleedEffect.localPosition;
                  effect.localRotation = bleedWork.bleedEffect.localRotation;
                }
              }
            }
          }
          if (flag1)
          {
            if (bleedWork != null)
            {
              if (Object.op_Inequality((Object) bleedWork.bleedEffect, (Object) null))
              {
                EffectManager.ReleaseEffect(((Component) bleedWork.bleedEffect).gameObject);
                bleedWork.bleedEffect = (Transform) null;
              }
              this.regionWorks[index1].bleedWorkList.Remove(bleedWork);
            }
            this.regionWorks[index1].bleedList.RemoveAt(index3);
          }
          else
            ++index3;
        }
      }
      if (this.IsMirror() || this.IsPuppet())
      {
        bool flag = false;
        int index6 = 0;
        for (int length = this.regionWorks.Length; index6 < length; ++index6)
        {
          if (this.regionWorks[index6].bleedList.Count > 0)
            flag = true;
        }
        if (flag)
          this.StartWaitingPacket(StageObject.WAITING_PACKET.ENEMY_UPDATE_BLEED_DAMAGE, false, MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.arrowBleedTimeInterval * 2f);
        else
          this.EndWaitingPacket(StageObject.WAITING_PACKET.ENEMY_UPDATE_BLEED_DAMAGE);
      }
    }
    if (!Object.op_Inequality((Object) this.enemySender, (Object) null))
      return;
    this.enemySender.OnUpdateBleedDamage(sync_data);
  }

  public void ClearBleedDamageAll()
  {
    if (this.regionWorks != null)
    {
      int index1 = 0;
      for (int length = this.regionWorks.Length; index1 < length; ++index1)
      {
        this.regionWorks[index1].bleedList.Clear();
        int index2 = 0;
        for (int count = this.regionWorks[index1].bleedWorkList.Count; index2 < count; ++index2)
        {
          Enemy.BleedWork bleedWork = this.regionWorks[index1].bleedWorkList[index2];
          if (Object.op_Inequality((Object) bleedWork.bleedEffect, (Object) null))
          {
            EffectManager.ReleaseEffect(((Component) bleedWork.bleedEffect).gameObject);
            bleedWork.bleedEffect = (Transform) null;
          }
        }
        this.regionWorks[index1].bleedWorkList.Clear();
      }
    }
    if (!this.IsMirror() && !this.IsPuppet())
      return;
    this.EndWaitingPacket(StageObject.WAITING_PACKET.ENEMY_UPDATE_BLEED_DAMAGE);
  }

  public void OnUpdateShadowSealing(Enemy.ShadowSealingSyncData syncData)
  {
    if (this.regionWorks != null)
    {
      EnemyRegionWork regionWork = this.regionWorks[syncData.regionIndex];
      Enemy.ShadowSealingData shadowSealingData = regionWork.shadowSealingData;
      shadowSealingData.ownerID = 0;
      shadowSealingData.existSec = 0.0f;
      shadowSealingData.extendRate = 1f;
      if (regionWork.shadowSealingEffect != null)
      {
        EffectManager.ReleaseEffect(((Component) regionWork.shadowSealingEffect).gameObject);
        regionWork.shadowSealingEffect = (Transform) null;
      }
    }
    if (this.IsMirror() || this.IsPuppet())
      this.EndWaitingPacket(StageObject.WAITING_PACKET.ENEMY_UPDATE_SHADOWSEALING);
    if (!Object.op_Inequality((Object) this.enemySender, (Object) null))
      return;
    this.enemySender.OnUpdateShadowSealing(syncData);
  }

  public void ClearShadowSealingAll(bool isClearOwnerID = true, bool isEndPacket = true)
  {
    if (this.regionWorks != null)
    {
      int index = 0;
      for (int length = this.regionWorks.Length; index < length; ++index)
      {
        Enemy.ShadowSealingData shadowSealingData = this.regionWorks[index].shadowSealingData;
        if (isClearOwnerID)
          shadowSealingData.ownerID = 0;
        shadowSealingData.existSec = 0.0f;
        shadowSealingData.extendRate = 1f;
        if (this.regionWorks[index].shadowSealingEffect != null)
        {
          EffectManager.ReleaseEffect(((Component) this.regionWorks[index].shadowSealingEffect).gameObject);
          this.regionWorks[index].shadowSealingEffect = (Transform) null;
        }
      }
    }
    if (!isEndPacket || !this.IsMirror() && !this.IsPuppet())
      return;
    this.EndWaitingPacket(StageObject.WAITING_PACKET.ENEMY_UPDATE_SHADOWSEALING);
  }

  public void OnUpdateBombArrow(int regionId)
  {
    if (this.IsMirror() || this.IsPuppet())
      this.EndWaitingPacket(StageObject.WAITING_PACKET.ENEMY_UPDATE_BOMBARROW);
    if (this.regionWorks == null)
      return;
    EnemyRegionWork regionWork = this.regionWorks[regionId];
    TargetPoint targetPoint = (TargetPoint) null;
    for (int index = 0; index < this.targetPoints.Length; ++index)
    {
      if (this.targetPoints[index].regionID == regionWork.regionId && this.targetPoints[index].isAimEnable)
      {
        targetPoint = this.targetPoints[index];
        break;
      }
    }
    if (Object.op_Equality((Object) targetPoint, (Object) null))
      return;
    List<Enemy.BombArrowData> arrowDataHistory = regionWork.bombArrowDataHistory;
    for (int index = 0; index < arrowDataHistory.Count; ++index)
    {
      Enemy.BombArrowData bombArrowData = arrowDataHistory[index];
      if (bombArrowData != null)
      {
        Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(bombArrowData.ownerID) as Player;
        if (!Object.op_Equality((Object) player, (Object) null))
        {
          float delay = 0.0f;
          Vector3 pos = targetPoint._transform.position;
          List<float> bombDelayFrameList = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.bombDelayFrameList;
          List<Vector3> offsetPositionList = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.bombOffsetPositionList;
          if (bombDelayFrameList.Count > index && offsetPositionList.Count > index)
          {
            delay = bombDelayFrameList[index];
            pos = Vector3.op_Addition(pos, Quaternion.op_Multiply(Quaternion.Euler(player._transform.eulerAngles), offsetPositionList[index]));
          }
          this.StartCoroutine(this.FireBombArrow(player, bombArrowData.atk, index + 1, pos, player.isBoostMode, delay: delay));
        }
      }
    }
    regionWork.bombArrowDataHistory.Clear();
    if (!Object.op_Inequality((Object) regionWork.bombArrowEffect, (Object) null))
      return;
    EffectManager.ReleaseEffect(((Component) regionWork.bombArrowEffect).gameObject, immediate: true);
    regionWork.bombArrowEffect = (Transform) null;
  }

  public void ClearBombArrowAll()
  {
    if (this.regionWorks == null)
      return;
    for (int index = 0; index < this.regionWorks.Length; ++index)
      this.OnUpdateBombArrow(this.regionWorks[index].regionId);
  }

  private IEnumerator FireBombArrow(
    Player fromObject,
    AtkAttribute atk,
    int lv,
    Vector3 pos,
    bool boost,
    bool visibled = true,
    float delay = 0.0f)
  {
    while ((double) delay > 0.0)
    {
      delay -= Time.deltaTime;
      yield return (object) null;
    }
    if (atk != null)
    {
      ELEMENT_TYPE elementType = atk.GetElementType();
      if (visibled && elementType != ELEMENT_TYPE.MAX)
      {
        if (MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.bombSEIdList.Count >= lv)
          SoundManager.PlayOneShotSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.bombSEIdList[lv - 1], (DisableNotifyMonoBehaviour) this);
        Transform effect = EffectManager.GetEffect(MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.GetBombEffectName(elementType));
        List<float> burstEffectScaleList = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.bombArrowBurstEffectScaleList;
        if (Object.op_Inequality((Object) effect, (Object) null))
        {
          effect.localPosition = pos;
          if (burstEffectScaleList.Count >= lv)
          {
            Transform transform = effect;
            transform.localScale = Vector3.op_Multiply(transform.localScale, burstEffectScaleList[lv - 1]);
          }
        }
      }
      if (Object.op_Inequality((Object) fromObject, (Object) null))
      {
        string arrowAttackInfoName = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.bombArrowAttackInfoName;
        if (boost)
          arrowAttackInfoName += "boost_";
        AnimEventShot.Create((StageObject) fromObject, fromObject.FindAttackInfo(arrowAttackInfoName + (lv - 1).ToString()), pos, Quaternion.identity, exAtk: atk);
      }
    }
  }

  public void SetDashEnd()
  {
    if (!this.enableDash)
      return;
    this.SetChangeTrigger(this.dashEndTrigger);
    this.enableDash = false;
    this.dashBeforePos = Vector3.zero;
    this.dashNowDistance = 0.0f;
    this.dashOverDistance = 0.0f;
    this.dashMinDistance = 0.0f;
    this.dashMaxDistance = 0.0f;
    this.dashEndTrigger = (string) null;
    this.dashOverFlag = false;
    this.dashOverCheckDistance = 0.0f;
    this.rotateSafeMode = false;
  }

  public override void ActDead(bool force_sync = false, bool recieve_direct = false)
  {
    this.ActReleaseGrabbedPlayers(false, false, true);
    this.badStatusTotal.Reset();
    this.badStatusMax.Copy(this.badStatusBase);
    base.ActDead(force_sync, recieve_direct);
    this.ResetConcussion(true);
    this.PlayMotion(7, 0.0f);
    this.PrepareEnemyOut();
    if (MonoBehaviourSingleton<CoopNetworkManager>.IsValid())
    {
      if (this.IsCoopNone() || this.IsOriginal())
      {
        this.StopForceEnemyOut();
        MonoBehaviourSingleton<CoopNetworkManager>.I.EnemyOut(this.id, this._position);
      }
      else if (force_sync)
        this.ForceEnemyOut();
    }
    bool flag = false;
    if (Object.op_Equality((Object) MonoBehaviourSingleton<StageObjectManager>.I.boss, (Object) this))
    {
      this.UpdateBreakIDLists();
      if (QuestManager.IsValidInGame() && MonoBehaviourSingleton<InGameProgress>.IsValid())
      {
        flag = true;
        if (MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeries() || MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeriesArena())
        {
          if (MonoBehaviourSingleton<QuestManager>.I.IsLastEnemyCurrentQuestSeries())
            MonoBehaviourSingleton<InGameProgress>.I.BattleComplete();
          else if (MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeriesArena())
            MonoBehaviourSingleton<InGameProgress>.I.NextBattleStartForSeriesArena();
        }
        else
          MonoBehaviourSingleton<InGameProgress>.I.BattleComplete();
      }
    }
    if (!flag)
      this.SetNextTrigger();
    if (this.IsFieldEnemyBoss())
      MonoBehaviourSingleton<CoopManager>.I.coopStage.OnDefeatFieldEnemyBoss();
    if (!QuestManager.IsValidInGameWaveStrategy())
      return;
    FieldMapTable.EnemyPopTableData enemyPopData = Singleton<FieldMapTable>.I.GetEnemyPopData(MonoBehaviourSingleton<FieldManager>.I.currentMapID, this.enemyPopIndex);
    if (enemyPopData != null)
      MonoBehaviourSingleton<StageObjectManager>.I.CountDownByWaveNo(enemyPopData.waveNo, enemyPopData.GeneratePopPosVec3());
    MonoBehaviourSingleton<StageObjectManager>.I.ClearWaveTargetLine();
  }

  public IEnumerator WaitForDeadMotionEnd(bool isSetNextTrigger = true)
  {
    yield return (object) null;
    if (!Object.op_Equality((Object) this.animator, (Object) null))
    {
      AnimatorStateInfo animatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
      if (((AnimatorStateInfo) ref animatorStateInfo).fullPathHash == Animator.StringToHash("Base Layer.dead_loop"))
      {
        if (isSetNextTrigger)
          this.SetNextTrigger();
      }
      else
      {
        do
        {
          animatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
          if (((AnimatorStateInfo) ref animatorStateInfo).fullPathHash != Animator.StringToHash("Base Layer.dead"))
            yield return (object) null;
          else
            goto label_11;
        }
        while (!Object.op_Equality((Object) this.animator, (Object) null));
        yield break;
        do
        {
          animatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
          if ((double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime < 1.0)
          {
            yield return (object) null;
            continue;
          }
          goto label_12;
label_11:;
        }
        while (!Object.op_Equality((Object) this.animator, (Object) null));
        yield break;
label_12:
        if (isSetNextTrigger)
          this.SetNextTrigger();
      }
    }
  }

  protected override int GetDeadReviveCount()
  {
    return this.deadReviveCount < this.deadReviveCountMax ? this.deadReviveCount + 1 : base.GetDeadReviveCount();
  }

  public void ActDeadRevive(int deadReviveCount)
  {
    this.ActReleaseGrabbedPlayers(false, false, true);
    this.EndAction();
    this.actDeadReviveCount = deadReviveCount;
    this.actionID = (Character.ACTION_ID) 24;
    this.PlayMotion(124 + deadReviveCount);
    this.hp = 1;
    this.hitOffFlag |= StageObject.HIT_OFF_FLAG.DEAD_REVIVE;
    this.downTotal = 0.0f;
    this.downCount = 0;
    this.ResetConcussion(true);
    this.badStatusTotal.Reset();
    this.BarrierHp = (XorInt) this.BarrierHpMax;
    this.ResetBadReaction(true);
    this.buffParam.AllBuffEnd(false);
    this.badStatusMax.Copy(this.badStatusBase);
    this.continusAttackParam.RemoveAll();
    this.OnActReaction();
  }

  private bool IsFieldEnemyBoss()
  {
    if (this.isCachedIsFieldEnemyBoss)
      return this.cachedIsFieldEnemyBoss;
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid() || MonoBehaviourSingleton<CoopManager>.IsValid() && !MonoBehaviourSingleton<CoopManager>.I.coopStage.GetIsInFieldEnemyBossBattle() || !MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return false;
    if (MonoBehaviourSingleton<StageObjectManager>.I.IsFieldEnemyBoss(this.id))
    {
      this.isCachedIsFieldEnemyBoss = true;
      this.cachedIsFieldEnemyBoss = true;
      return this.cachedIsFieldEnemyBoss;
    }
    this.isCachedIsFieldEnemyBoss = true;
    this.cachedIsFieldEnemyBoss = false;
    return this.cachedIsFieldEnemyBoss;
  }

  private void PrepareEnemyOut()
  {
    this.DeleteStatusGizmo();
    if (MonoBehaviourSingleton<DropTargetMarkerManeger>.IsValid())
      MonoBehaviourSingleton<DropTargetMarkerManeger>.I.RemoveTarget(this._transform);
    this.UpdateNextMotion();
    this.SetVelocity(Vector3.zero);
    this._rigidbody.velocity = Vector3.zero;
    this._collider.enabled = false;
    if (!this.isBoss && this.colliders != null)
    {
      int index = 0;
      for (int length = this.colliders.Length; index < length; ++index)
        this.colliders[index].enabled = false;
    }
    this.ClearBleedDamageAll();
    this.ClearShadowSealingAll();
    this.ClearBombArrowAll();
    for (int index = this.m_activeAttackObstacleList.Count - 1; index >= 0; --index)
      this.m_activeAttackObstacleList[index].RequestDestroy();
    if (Object.op_Inequality((Object) this.m_effectElectricShock, (Object) null))
      Object.Destroy((Object) this.m_effectElectricShock);
    if (Object.op_Inequality((Object) this.m_effectSoilShock, (Object) null))
      Object.Destroy((Object) this.m_effectSoilShock);
    if (Object.op_Inequality((Object) this.m_effectBurning, (Object) null))
      Object.Destroy((Object) this.m_effectBurning);
    if (Object.op_Inequality((Object) this.m_effectSpeedDown, (Object) null))
      Object.Destroy((Object) this.m_effectSpeedDown);
    if (Object.op_Inequality((Object) this.effectLightRing, (Object) null))
      Object.Destroy((Object) this.effectLightRing);
    if (Object.op_Inequality((Object) this.m_effectErosion, (Object) null))
      Object.Destroy((Object) this.m_effectErosion);
    this.m_effectElectricShock = (GameObject) null;
    this.m_effectSoilShock = (GameObject) null;
    this.m_effectBurning = (GameObject) null;
    this.m_effectSpeedDown = (GameObject) null;
    this.effectLightRing = (Transform) null;
    this.m_effectErosion = (GameObject) null;
  }

  public override void VanishLocal()
  {
    this.PrepareVanishLocal();
    this.OnDeadEnd();
  }

  public override void PrepareVanishLocal()
  {
    this.ActReleaseGrabbedPlayers(false, false, true);
    this.badStatusTotal.Reset();
    this.badStatusMax.Copy(this.badStatusBase);
    base.PrepareVanishLocal();
    this.PrepareEnemyOut();
    if (Object.op_Equality((Object) MonoBehaviourSingleton<StageObjectManager>.I.boss, (Object) this))
      this.UpdateBreakIDLists();
    if (false)
      return;
    this.SetNextTrigger();
  }

  public void OnEndEscape()
  {
    if (!MonoBehaviourSingleton<CoopNetworkManager>.IsValid())
      return;
    MonoBehaviourSingleton<CoopNetworkManager>.I.EnemyOutEscape(this.id, this._position);
  }

  private void ForceEnemyOut()
  {
    this.StopForceEnemyOut();
    this.forceEnemyOutCoroutine = this.StartCoroutine(this.DoForceEnemyOut());
  }

  private IEnumerator DoForceEnemyOut()
  {
    yield return (object) new WaitForSeconds(this.enemyParameter.guestEnemyOutTime);
    if (this.forceEnemyOutCoroutine != null)
      this.forceEnemyOutCoroutine = (Coroutine) null;
    MonoBehaviourSingleton<CoopNetworkManager>.I.EnemyOut(this.id, this._position);
  }

  public void StopForceEnemyOut()
  {
    if (this.forceEnemyOutCoroutine == null)
      return;
    this.StopCoroutine(this.forceEnemyOutCoroutine);
    this.forceEnemyOutCoroutine = (Coroutine) null;
  }

  public void UpdateBreakIDLists()
  {
    if (!Object.op_Equality((Object) MonoBehaviourSingleton<StageObjectManager>.I.boss, (Object) this) || !MonoBehaviourSingleton<CoopManager>.IsValid() || MonoBehaviourSingleton<CoopManager>.I.coopStage.bossBreakIDLists == null)
      return;
    int index = 0;
    if (QuestManager.IsValidInGame())
      index = (int) MonoBehaviourSingleton<QuestManager>.I.currentQuestSeriesIndex;
    MonoBehaviourSingleton<CoopManager>.I.coopStage.bossBreakIDLists[index] = this.GetBreakRegionIDList();
  }

  public override void OnDeadEnd() => this.DestroyObject();

  public virtual void ActStep(int motion_id = 0)
  {
    if (motion_id == 0)
      motion_id = 115;
    this.EndAction();
    this.actionID = Character.ACTION_ID.MAX;
    this.PlayMotion(motion_id);
    if (!Object.op_Inequality((Object) this.enemySender, (Object) null))
      return;
    this.enemySender.OnActStep(motion_id);
  }

  public virtual void ActAngry(int angryActionId, uint angryId)
  {
    this.EndAction();
    this.NowAngryID = angryId;
    this.actionID = Character.ACTION_ID.PARALYZE | Character.ACTION_ID.FREEZE;
    this.PlayMotion(130 + angryActionId);
    if (!Object.op_Inequality((Object) this.enemySender, (Object) null))
      return;
    this.enemySender.OnActAngry(angryActionId, angryId);
  }

  public void RegisterAngryID(uint angryId)
  {
    if (this.m_execAngryIds == null || this.m_execAngryIds.Contains(angryId))
      return;
    this.m_execAngryIds.Add(angryId);
  }

  public void UnRegisterAngryID(uint angryId)
  {
    if (this.m_execAngryIds == null || !this.m_execAngryIds.Contains(angryId))
      return;
    this.m_execAngryIds.Remove(angryId);
  }

  public bool CheckAngryID(uint angryId) => this.m_execAngryIds.Contains(angryId);

  public uint NowAngryID
  {
    get => this.m_nowAngryId;
    set => this.m_nowAngryId = value;
  }

  public List<uint> ExecAngryIDList
  {
    get => this.m_execAngryIds;
    set => this.m_execAngryIds = value;
  }

  public void ActDown()
  {
    int num = this.IsDebuffShadowSealing() ? 0 : (!this.IsConcussion() ? 1 : 0);
    bool useDownTime = this.IsAbleToUseDownTime();
    if (num != 0)
      this.EndAction();
    this.ActReleaseGrabbedPlayers(false, false, true);
    if (useDownTime)
    {
      this.downTime = Time.time + this.downLoopStartTime + this.downLoopTime;
      this.downGaugeDecreaseStartTime = Time.time + this.downLoopStartTime;
      this.downDecreaseRates = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.Soul_DownGaugeDecreaseRates;
    }
    if (num != 0)
    {
      this.actionID = Character.ACTION_ID.ATTACK | Character.ACTION_ID.FREEZE;
      this.PlayMotion(useDownTime ? 118 : 117);
    }
    else
    {
      if (this.IsConcussion())
      {
        this.ActConcussionEnd();
        this.actionID = Character.ACTION_ID.ATTACK | Character.ACTION_ID.FREEZE;
      }
      else if (!this.shadowSealingStackDebuff.Contains(Character.ACTION_ID.ATTACK | Character.ACTION_ID.FREEZE))
        this.shadowSealingStackDebuff.Add(Character.ACTION_ID.ATTACK | Character.ACTION_ID.FREEZE);
      this.EventWeakPointAllON(new AnimEventData.EventData()
      {
        attackMode = Player.ATTACK_MODE.NONE,
        intArgs = new int[1]{ 1 }
      });
    }
    this.OnActReaction();
  }

  private bool UpdateDownAction()
  {
    if (!this.IsAbleToUseDownTime())
      return false;
    if ((double) this.downGaugeDecreaseStartTime - (double) Time.time < 0.0)
    {
      int stackCount = this.stackBuffCtrl.GetStackCount(StackBuffController.STACK_TYPE.SNATCH);
      if (!((IList<float>) this.downDecreaseRates).IsNullOrEmpty<float>())
      {
        int index = Mathf.Min(stackCount, this.downDecreaseRates.Length - 1);
        if (stackCount > 0)
          this.downTime += this.downDecreaseRates[index] * Time.deltaTime;
      }
    }
    if ((double) this.downTime - (double) Time.time > 0.0)
      return false;
    this.ActDownEnd();
    if (!this.IsDebuffShadowSealing())
      this.SetNextTrigger();
    return true;
  }

  private void ActDownEnd()
  {
    if (!this.IsDebuffShadowSealing())
      return;
    this._EndDebuffAction(Character.ACTION_ID.ATTACK | Character.ACTION_ID.FREEZE);
    this.EventWeakPointAllOFF((AnimEventData.EventData) null);
    if (!this.shadowSealingStackDebuff.Contains(Character.ACTION_ID.ATTACK | Character.ACTION_ID.FREEZE))
      return;
    this.shadowSealingStackDebuff.Remove(Character.ACTION_ID.ATTACK | Character.ACTION_ID.FREEZE);
  }

  public bool IsActDown()
  {
    if (!this.IsAbleToUseDownTime())
      return false;
    return this.actionID == (Character.ACTION_ID.ATTACK | Character.ACTION_ID.FREEZE) || this.shadowSealingStackDebuff.Contains(Character.ACTION_ID.ATTACK | Character.ACTION_ID.FREEZE);
  }

  private bool IsAbleToUseDownTime()
  {
    bool useDownTime = false;
    if (this.useDownLoopTime && (double) this.downLoopStartTime >= 0.0 && (double) this.downLoopTime >= 0.0)
      useDownTime = true;
    return useDownTime;
  }

  public float GetDownTimeRate()
  {
    float downTimeRate = 0.0f;
    if (this.IsAbleToUseDownTime() && (double) this.downLoopTime > 0.0)
      downTimeRate = Mathf.Clamp((this.downTime - Time.time) / this.downLoopTime, 0.0f, 1f);
    return downTimeRate;
  }

  public void IncreaseDownTimeByAttack(float down)
  {
    if ((double) down <= 0.0 || this.downMax <= 0)
      return;
    float num = this.downTime + this.downLoopTime * (down / (float) this.downMax);
    if ((double) num - (double) Time.time > (double) this.downLoopTime)
      num = this.downLoopTime + Time.time;
    this.downTime = num;
  }

  public void ActDizzy()
  {
    this.EndAction();
    this.ActReleaseGrabbedPlayers(false, false, true);
    this.m_dizzyTime = Time.time + this.DizzyReactionLoopTime;
    this.actionID = (Character.ACTION_ID) 18;
    this.PlayMotion(122);
    this.OnActReaction();
  }

  public void ActCounter(int targetId)
  {
    this.EndAction();
    this.counterFlag = true;
    this.actionID = (Character.ACTION_ID) 17;
    this.PlayMotion(119);
    if (MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
      MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestAnnounce(this.enemyTableData.name, STRING_CATEGORY.ENEMY_REACTION, this.kStrIdx_EnemyReaction_Counter);
    if (this.IsOriginal() || this.IsCoopNone())
    {
      EnemyBrain brain = this.controller.brain as EnemyBrain;
      if (brain.targetCtrl != null)
      {
        StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.FindObject(targetId);
        if (Object.op_Inequality((Object) stageObject, (Object) null))
        {
          brain.targetCtrl.SetCurrentTarget(stageObject);
          this.SetActionTarget(stageObject, true);
          this.SetActionPosition(stageObject._position, true);
          brain.fsm.ChangeState(STATE_TYPE.SELECT);
        }
      }
    }
    this.OnActReaction();
  }

  public bool counterFlag { get; set; }

  public override void ActFreezeStart()
  {
    if (this.IsFreeze())
      return;
    this.ActReleaseGrabbedPlayers(false, false, true);
    base.ActFreezeStart();
    if (!MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
      return;
    MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
  }

  protected override void ActFreezeEnd()
  {
    if (!this.IsFreeze())
      return;
    base.ActFreezeEnd();
    if (!MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
      return;
    MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
  }

  public override void ActParalyze()
  {
    if (this.IsDebuffShadowSealing() && this.shadowSealingStackDebuff.Contains(Character.ACTION_ID.PARALYZE))
      return;
    this.ActReleaseGrabbedPlayers(false, false, true);
    base.ActParalyze();
    this.paralyzeTime = Time.time + this.paralyzeLoopTime;
    if (!MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
      return;
    MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
  }

  protected override void ActParalyzeEnd()
  {
    if (!this.IsParalyze())
      return;
    base.ActParalyzeEnd();
    if (!MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
      return;
    MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
  }

  public void ActElectricShock()
  {
    this.ActDamage();
    this.ActReleaseGrabbedPlayers(false, false, true);
    this.CreateElectricShockEffect();
  }

  public void ActSoilShock()
  {
    this.ActDamage();
    this.ActReleaseGrabbedPlayers(false, false, true);
  }

  public void ActBind(float loopTime)
  {
    bool flag1 = !this.IsDebuffShadowSealing();
    bool flag2 = this.shadowSealingStackDebuff.Contains((Character.ACTION_ID) 23);
    if (!flag1 & flag2)
      return;
    if (!flag1 && !flag2)
      this.shadowSealingStackDebuff.Add((Character.ACTION_ID) 23);
    this.ActReleaseGrabbedPlayers(false, false, true);
    this.bindEndTime = Time.time + this.downLoopStartTime + loopTime;
    if (flag1)
    {
      this.EndAction();
      this.actionID = (Character.ACTION_ID) 23;
      this.PlayMotion(118);
    }
    this.OnActReaction();
  }

  private bool UpdateBindAction()
  {
    if ((double) this.bindEndTime - (double) Time.time > 0.0)
      return false;
    this.ActBindEnd();
    if (!this.IsDebuffShadowSealing())
      this.SetNextTrigger();
    return true;
  }

  private void ActBindEnd()
  {
    if (!this.IsDebuffShadowSealing() || !this.shadowSealingStackDebuff.Contains((Character.ACTION_ID) 23))
      return;
    this.shadowSealingStackDebuff.Remove((Character.ACTION_ID) 23);
  }

  private bool IsActBind()
  {
    return this.actionID == (Character.ACTION_ID) 23 || this.shadowSealingStackDebuff.Contains((Character.ACTION_ID) 23);
  }

  public void ActDamageMotionStopStart(float loopTime)
  {
    if (!this.IsReactionDamageMotionStop())
      return;
    this.EndAction();
    this.PlayMotion(6, (double) this.stopMotionByDebuffNormalizedTime < 0.0 ? -1f : 0.0f);
    if (Object.op_Inequality((Object) this._rigidbody, (Object) null))
      this._rigidbody.velocity = Vector3.zero;
    this.rotateEventKeep = false;
    this.rotateToTargetFlag = false;
    this.rotateEventSpeed = 0.0f;
    this.OnActReaction();
  }

  public void ActDamageMotionStopEnd()
  {
    this.setPause(false);
    this.m_isStopMotionByDebuff = false;
  }

  public bool UpdateDamageMotionStop()
  {
    float num = 0.1f;
    AnimatorStateInfo animatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
    if (this.actionID == Character.ACTION_ID.NONE && ((double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime < (double) num || ((AnimatorStateInfo) ref animatorStateInfo).fullPathHash != Animator.StringToHash("Base Layer.damage")) || this.m_isStopMotionByDebuff)
      return false;
    this.setPause(true);
    this.m_isStopMotionByDebuff = true;
    return false;
  }

  public bool IsReactionDamageMotionStop()
  {
    switch (this.actionID)
    {
      case Character.ACTION_ID.PARALYZE:
      case Character.ACTION_ID.FREEZE:
      case Character.ACTION_ID.ATTACK | Character.ACTION_ID.FREEZE:
      case (Character.ACTION_ID) 19:
      case (Character.ACTION_ID) 22:
      case (Character.ACTION_ID) 23:
      case (Character.ACTION_ID) 25:
        return false;
      default:
        return true;
    }
  }

  public override void ActMovePoint(Vector3 targetPos)
  {
    if (this.IsArrivalPosition(targetPos) && !this.forceActMovePoint)
      return;
    this.EndAction();
    this.actionID = Character.ACTION_ID.MOVE_POINT;
    this.SetStateMovePoint(Character.STATE_MOVE_POINT.INIT);
    if (!Object.op_Inequality((Object) this.enemySender, (Object) null))
      return;
    this.enemySender.OnActMovePoint(targetPos);
  }

  protected override void UpdateMovePointAction()
  {
    switch (this.stateMovePoint)
    {
      case Character.STATE_MOVE_POINT.INIT:
        if (this.IsArrivalPosition(this.movePointPos) && !this.forceActMovePoint)
        {
          this.SetStateMovePoint(Character.STATE_MOVE_POINT.FINISH);
          break;
        }
        Vector3 targetDir = this._forward;
        Vector3 vector3 = Vector3.op_Subtraction(this.movePointPos, this._position);
        vector3.y = 0.0f;
        if (Vector3.op_Inequality(vector3, Vector3.zero))
          targetDir = ((Vector3) ref vector3).normalized;
        if (!this.IsNeedToRotate(targetDir))
        {
          this.PlayMotion(13);
          this.SetStateMovePoint(Character.STATE_MOVE_POINT.CHECK);
          break;
        }
        this.m_rotateForActTime = 0.0f;
        this.m_rotateForActFinishTime = (float) ((double) Mathf.Acos(Vector3.Dot(this._forward, targetDir)) / (Math.PI / 90.0) * (1.0 / (double) Application.targetFrameRate));
        this.m_rotateForActStart_Quat = Quaternion.LookRotation(this._forward);
        this.m_rotateForActEnd_Quat = Quaternion.LookRotation(targetDir);
        this.m_rotateForActMotionId = (double) Vector3.Cross(this._forward, targetDir).y >= 0.0 ? 5 : 4;
        this.PlayMotion(this.m_rotateForActMotionId);
        this.SetStateMovePoint(Character.STATE_MOVE_POINT.ROTATE);
        break;
      case Character.STATE_MOVE_POINT.ROTATE:
        this.m_rotateForActTime += Time.deltaTime;
        float num = Mathf.Clamp(this.m_rotateForActTime / this.m_rotateForActFinishTime, 0.0f, 1f);
        if (!this.IsPlayingMotion(1))
        {
          this._rotation = Quaternion.Lerp(this.m_rotateForActStart_Quat, this.m_rotateForActEnd_Quat, num);
          break;
        }
        if ((double) num < 1.0)
        {
          this.PlayMotion(this.m_rotateForActMotionId);
          break;
        }
        this.PlayMotion(13);
        this.SetStateMovePoint(Character.STATE_MOVE_POINT.CHECK);
        break;
      case Character.STATE_MOVE_POINT.CHECK:
        if (!this.IsArrivalPosition(this.movePointPos))
          break;
        this.SetNextTrigger();
        this.SetStateMovePoint(Character.STATE_MOVE_POINT.FINISH);
        break;
      case Character.STATE_MOVE_POINT.FINISH:
        this.SetStateMovePoint(Character.STATE_MOVE_POINT.NONE);
        break;
    }
  }

  public override void ActMoveLookAt(Vector3 moveLookAtPos, bool isPacket = false)
  {
    this.EndAction();
    this.actionID = Character.ACTION_ID.MOVE_LOOKAT;
    this.SetStateMoveLookAt(Character.STATE_MOVE_LOOKAT.INIT);
    if (isPacket)
      this.moveLookAtPos = moveLookAtPos;
    if (!Object.op_Inequality((Object) this.enemySender, (Object) null))
      return;
    this.enemySender.OnActMoveLookAt(moveLookAtPos);
  }

  protected override void UpdateMoveLookAtAction()
  {
    switch (this.stateMoveLookAt)
    {
      case Character.STATE_MOVE_LOOKAT.INIT:
        Vector3 vector3_1 = Vector3.op_Subtraction(this.moveLookAtPos, this._position);
        this.m_moveLookAtInitTargetDir = ((Vector3) ref vector3_1).normalized;
        this.PlayMotion(14);
        this.SetStateMoveLookAt(Character.STATE_MOVE_LOOKAT.MOVE);
        break;
      case Character.STATE_MOVE_LOOKAT.MOVE:
        Vector3 vector3_2 = Vector3.op_Subtraction(this._position, this.moveLookAtPos);
        Vector3 vector3_3 = Vector3.op_Subtraction(Vector3.op_Addition(this.moveLookAtPos, Quaternion.op_Multiply(Quaternion.AngleAxis(this.moveLookAtAngle * Time.deltaTime, Vector3.up), vector3_2)), this._position);
        Vector3 vector3_4;
        if (!this.IsPlayingMotion(1))
        {
          vector3_4 = Vector3.op_Subtraction(this.moveLookAtPos, this._position);
          this._rotation = Quaternion.LookRotation(((Vector3) ref vector3_4).normalized, Vector3.up);
          this._position = Vector3.op_Addition(this._position, vector3_3);
        }
        else
          this.PlayMotion(14);
        vector3_4 = Vector3.op_Subtraction(this.moveLookAtPos, this._position);
        if ((double) Vector3.Angle(this.m_moveLookAtInitTargetDir, ((Vector3) ref vector3_4).normalized) < (double) this.moveLookAtAngle)
          break;
        this.SetNextTrigger();
        this.SetStateMoveLookAt(Character.STATE_MOVE_LOOKAT.FINISH);
        break;
      case Character.STATE_MOVE_LOOKAT.FINISH:
        this.SetStateMoveLookAt(Character.STATE_MOVE_LOOKAT.NONE);
        break;
    }
  }

  protected override void OnPlayingEndMotion()
  {
    switch (this.actionID)
    {
      case Character.ACTION_ID.DEAD:
        this.OnDeadEnd();
        return;
      case (Character.ACTION_ID) 24:
        if (this.isFirstMadMode)
        {
          this.ActMadMode();
          return;
        }
        break;
    }
    base.OnPlayingEndMotion();
  }

  protected override void EndAction()
  {
    if (!this.isInitialized)
      return;
    Character.ACTION_ID actionId = this.actionID;
    base.EndAction();
    this._EndDebuffAction(actionId);
    this.EndWaitingPacket(StageObject.WAITING_PACKET.ENEMY_WARP);
    if (Object.op_Inequality((Object) this.loader.shadow, (Object) null) && !((Component) this.loader.shadow).gameObject.activeSelf)
      ((Component) this.loader.shadow).gameObject.SetActive(true);
    this.enableTargetPoint = true;
    this.reviveRegionWaitSync = false;
    this.enableDash = false;
    this.dashBeforePos = Vector3.zero;
    this.dashNowDistance = 0.0f;
    this.dashOverDistance = 0.0f;
    this.dashMinDistance = 0.0f;
    this.dashMaxDistance = 0.0f;
    this.dashEndTrigger = (string) null;
    this.dashOverFlag = false;
    this.dashOverCheckDistance = 0.0f;
    this.canHitShockEffect = true;
    this.isAbleToSkipAction = false;
    this.enableAssimilation = false;
    this.enableToSkipActionByDamage = false;
    int length = this.regionWorks.Length;
    for (int index = 0; index < length; ++index)
    {
      if (!this.regionWorks[index].IsValidDisplayTimer)
        this.regionWorks[index].ResetWeakState();
    }
    this.shotEventInfoQueue.Clear();
    this.shotNetworkInfoQueue.Clear();
    this.warpWaitSync = false;
    if (this.warpViewFlag || (double) this.warpViewRate != 0.0)
    {
      this.warpViewFlag = true;
      this.warpViewRatePerTime = -2f;
    }
    if (this.radialBlurEnable)
      MonoBehaviourSingleton<InGameCameraManager>.I.EndRadialBlurFilter(0.1f);
    this.radialBlurEnable = false;
    if (Object.op_Inequality((Object) this.loader.baseEffect, (Object) null) && !((Component) this.loader.baseEffect).gameObject.activeSelf)
      ((Component) this.loader.baseEffect).gameObject.SetActive(true);
    int count = this.animLayerWeightChangeInfo.Count;
    if (count > 0)
    {
      this.loader.GetAnimator();
      for (int index = 0; index < count; ++index)
      {
        Enemy.AnimationLayerWeightChangeInfo weightChangeInfo = this.animLayerWeightChangeInfo[index];
        if (weightChangeInfo.aliveFlag && weightChangeInfo.forceEndFlag)
          weightChangeInfo.aliveFlag = false;
      }
    }
    if (this.blendColorCtrl != null)
      this.blendColorCtrl.ForceEnd();
    if (!this.isSummonAttack || actionId != Character.ACTION_ID.ATTACK)
      return;
    ((Component) this).gameObject.SetActive(false);
  }

  protected override void _EndDebuffAction(Character.ACTION_ID beforeActId)
  {
    switch (beforeActId)
    {
      case Character.ACTION_ID.PARALYZE:
        this.badStatusMax.paralyze *= 1.5f;
        if (!MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
          break;
        MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
        break;
      case Character.ACTION_ID.FREEZE:
        this.badStatusMax.freeze *= 1.5f;
        if (!MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
          break;
        MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
        break;
      case Character.ACTION_ID.ATTACK | Character.ACTION_ID.FREEZE:
        this.downTotal = 0.0f;
        ++this.downCount;
        using (List<MissionCheckBase>.Enumerator enumerator = MonoBehaviourSingleton<InGameProgress>.I.missionCheck.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            if (enumerator.Current is MissionCheckDownCount current)
              current.SetCount(this.downCount);
          }
          break;
        }
      case (Character.ACTION_ID) 19:
        this.ActDebuffShadowSealingEnd();
        break;
      case (Character.ACTION_ID) 22:
        this.ActLightRingEnd();
        break;
      case (Character.ACTION_ID) 25:
        this.ActConcussionEnd();
        break;
    }
  }

  protected override string GetMotionStateName(int motion_id, string _layerName = "")
  {
    if (motion_id >= 130 && motion_id <= 146)
    {
      int index = 15;
      Character.stateNameBuilder.Length = 0;
      Character.stateNameBuilder.Append("Base Layer.");
      Character.stateNameBuilder.AppendFormat(Enemy.subMotionStateName[index], (object) (motion_id - 130));
      return Character.stateNameBuilder.ToString();
    }
    if (motion_id - 115 < 0 || motion_id - 115 >= Enemy.subMotionStateName.Length)
      return base.GetMotionStateName(motion_id, _layerName);
    Character.stateNameBuilder.Length = 0;
    Character.stateNameBuilder.Append("Base Layer.");
    string str = Enemy.subMotionStateName[motion_id - 115];
    if (motion_id == 119)
    {
      EnemyBrain brain = this.controller.brain as EnemyBrain;
      if (Object.op_Inequality((Object) brain, (Object) null) && brain.actionCtrl != null)
      {
        int modeCounterModeId = brain.actionCtrl.GetNowModeCounterModeId();
        if (modeCounterModeId >= 2)
          str = $"{str}_{$"{modeCounterModeId:D2}"}";
      }
    }
    Character.stateNameBuilder.Append(str);
    return Character.stateNameBuilder.ToString();
  }

  protected override float GetAnimatorSpeed()
  {
    if (this.IsHitStop() || this.isPause)
      return 0.0f;
    switch (this.actionID)
    {
      case Character.ACTION_ID.MOVE:
      case Character.ACTION_ID.ROTATE:
      case Character.ACTION_ID.MOVE_POINT:
      case Character.ACTION_ID.MAX:
        return this.buffParam.GetMoveSpeed() * this.walkSpeedRateFromTable;
      case Character.ACTION_ID.ATTACK:
        return this.buffParam.GetAtkSpeed();
      default:
        return 1f;
    }
  }

  public override bool OnBuffStart(BuffParam.BuffData buffData)
  {
    if (this.CheckDisableBuffTypeByShield(buffData.type) || this.CheckDisableBuffTypeByMadMode(buffData.type) || !this.buffParam.BuffStart(buffData))
      return false;
    this.UpdateAnimatorSpeed();
    if (buffData.sync)
      this.SendBuffSync(buffData.type);
    if (this.IsCoopNone() || this.IsOriginal())
      buffData.isOwnerEnemyBuffStart = true;
    switch (buffData.type)
    {
      case BuffParam.BUFFTYPE.MOVE_SPEED_DOWN:
      case BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN:
        this.CreateSpeedDownEffect();
        break;
      case BuffParam.BUFFTYPE.BURNING:
        this.CreateBurningEffect();
        break;
      case BuffParam.BUFFTYPE.GHOST_FORM:
        if (((Component) this).gameObject.activeSelf)
        {
          this.ChangeGhostShaderParam(this.GhostFormShaderParam.disappearParam, this.GhostFormShaderParam.duration);
          break;
        }
        this.isRequireGhostShaderParam = true;
        this.ghostBuffEndParam = this.GhostFormShaderParam.disappearParam;
        this.ghostBuffDuration = this.GhostFormShaderParam.duration;
        break;
      case BuffParam.BUFFTYPE.EROSION:
        this.CreateErosionEffect();
        break;
      case BuffParam.BUFFTYPE.SOIL_SHOCK:
        this.CreateSoilShockEffect();
        break;
      case BuffParam.BUFFTYPE.ACID:
        this.CreateAcidEffect();
        break;
      case BuffParam.BUFFTYPE.DAMAGE_MOTION_STOP:
        this.ActDamageMotionStopStart(buffData.time);
        break;
      case BuffParam.BUFFTYPE.CORRUPTION:
        this.CreateCorruptionEffect();
        break;
      case BuffParam.BUFFTYPE.STIGMATA:
        this.CreateStigmataEffect();
        break;
      case BuffParam.BUFFTYPE.CYCLONIC_THUNDERSTORM:
        this.CreateCyclonicThunderstormEffect();
        break;
    }
    if (MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid() && buffData.isOwnerEnemyBuffStart)
      MonoBehaviourSingleton<UIEnemyAnnounce>.I.StartBuff(this.enemyTableData.name, buffData.type);
    if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
      MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
    buffData.isOwnerEnemyBuffStart = false;
    return true;
  }

  public override void OnBuffRoutine(BuffParam.BuffData buffData, bool packet = false)
  {
    int hp1 = this.hp;
    base.OnBuffRoutine(buffData, packet);
    int hp2 = this.hp;
    int damage1 = hp1 - hp2;
    if (MonoBehaviourSingleton<InGameRecorder>.IsValid() && buffData.fromObjectID > 0)
      MonoBehaviourSingleton<InGameRecorder>.I.RecordGivenDamage(buffData.fromObjectID, damage1);
    if (QuestManager.IsValidInGameExplore() && this.isBoss && MonoBehaviourSingleton<CoopManager>.I.GetSelfID() == buffData.fromObjectID)
    {
      ExplorePlayerStatus explorePlayerStatus = MonoBehaviourSingleton<QuestManager>.I.GetMyExplorePlayerStatus();
      int num = explorePlayerStatus.givenTotalDamage + damage1;
      explorePlayerStatus.SyncTotalDamageToBoss(num);
      MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendExploreBossDamage(num);
    }
    switch (buffData.type)
    {
      case BuffParam.BUFFTYPE.ELECTRIC_SHOCK:
      case BuffParam.BUFFTYPE.SOIL_SHOCK:
        if (!this.IsDebuffShadowSealing() && !this.IsConcussion())
        {
          Character.ReactionInfo info = new Character.ReactionInfo();
          info.reactionType = buffData.type != BuffParam.BUFFTYPE.ELECTRIC_SHOCK ? Character.REACTION_TYPE.SOIL_SHOCK : Character.REACTION_TYPE.ELECTRIC_SHOCK;
          if (this.enableReactionDelay && this.IsReactionDelayType((int) info.reactionType))
          {
            this.RegisterReacionDelayInfo(new Character.DelayReactionInfo()
            {
              type = info.reactionType
            });
            info.reactionType = Character.REACTION_TYPE.NONE;
            this.isReactionDelaySet = true;
          }
          this.ActReaction(info, false);
        }
        if (!packet)
        {
          buffData.value = buffData.damage;
          break;
        }
        break;
      case BuffParam.BUFFTYPE.DAMAGE_MOTION_STOP:
        this.UpdateDamageMotionStop();
        break;
    }
    if (BuffParam.IsTypeShowDamageOnEnemy(buffData.type))
    {
      AtkAttribute damage2 = new AtkAttribute();
      damage2.normal = (float) buffData.value;
      Vector3 position = this._position;
      GameObject loopEffect = this.buffParam.GetLoopEffect(buffData);
      if (Object.op_Inequality((Object) loopEffect, (Object) null))
        position = loopEffect.transform.position;
      this.CreateDamageNum(position, damage2, false, false);
    }
    if (!MonoBehaviourSingleton<CoopManager>.IsValid())
      return;
    MonoBehaviourSingleton<CoopManager>.I.coopStage.battleUserLog.Add((Character) this, buffData.type, damage1);
  }

  public override bool OnBuffEnd(BuffParam.BUFFTYPE type, bool sync, bool isPlayEndEffect = true)
  {
    if (!base.OnBuffEnd(type, sync, isPlayEndEffect))
      return false;
    switch (type)
    {
      case BuffParam.BUFFTYPE.MOVE_SPEED_DOWN:
        this.badStatusMax.speedDown *= 1.5f;
        if (Object.op_Inequality((Object) this.m_effectSpeedDown, (Object) null) && !this.buffParam.IsValidBuff(BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN))
        {
          Object.Destroy((Object) this.m_effectSpeedDown);
          this.m_effectSpeedDown = (GameObject) null;
          break;
        }
        break;
      case BuffParam.BUFFTYPE.POISON:
        this.badStatusMax.poison *= 1.5f;
        break;
      case BuffParam.BUFFTYPE.BURNING:
        this.badStatusMax.burning *= 1.5f;
        if (Object.op_Inequality((Object) this.m_effectBurning, (Object) null))
        {
          Object.Destroy((Object) this.m_effectBurning);
          this.m_effectBurning = (GameObject) null;
          break;
        }
        break;
      case BuffParam.BUFFTYPE.DEADLY_POISON:
        this.badStatusMax.deadlyPoison *= 1.5f;
        break;
      case BuffParam.BUFFTYPE.GHOST_FORM:
        if (((Component) this).gameObject.activeSelf)
        {
          this.ChangeGhostShaderParam(this.GhostFormShaderParam.appearParam, this.GhostFormShaderParam.duration);
          break;
        }
        this.isRequireGhostShaderParam = true;
        this.ghostBuffEndParam = this.GhostFormShaderParam.appearParam;
        this.ghostBuffDuration = this.GhostFormShaderParam.duration;
        break;
      case BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN:
        this.badStatusMax.attackSpeedDown *= 1.5f;
        if (Object.op_Inequality((Object) this.m_effectSpeedDown, (Object) null) && !this.buffParam.IsValidBuff(BuffParam.BUFFTYPE.MOVE_SPEED_DOWN))
        {
          Object.Destroy((Object) this.m_effectSpeedDown);
          this.m_effectSpeedDown = (GameObject) null;
          break;
        }
        break;
      case BuffParam.BUFFTYPE.EROSION:
        this.badStatusMax.erosion *= 1.5f;
        if (Object.op_Inequality((Object) this.m_effectErosion, (Object) null))
        {
          Object.Destroy((Object) this.m_effectErosion);
          this.m_effectErosion = (GameObject) null;
          break;
        }
        break;
      case BuffParam.BUFFTYPE.SOIL_SHOCK:
        this.badStatusMax.soilShock *= 1.5f;
        if (Object.op_Inequality((Object) this.m_effectSoilShock, (Object) null))
        {
          EffectManager.ReleaseEffect(this.m_effectSoilShock.gameObject);
          this.m_effectSoilShock = (GameObject) null;
          break;
        }
        break;
      case BuffParam.BUFFTYPE.ACID:
        this.badStatusMax.acid *= 1.5f;
        if (Object.op_Inequality((Object) this.m_effectAcid, (Object) null))
        {
          EffectManager.ReleaseEffect(this.m_effectAcid.gameObject);
          this.m_effectAcid = (GameObject) null;
          break;
        }
        break;
      case BuffParam.BUFFTYPE.DAMAGE_MOTION_STOP:
        this.ActDamageMotionStopEnd();
        break;
      case BuffParam.BUFFTYPE.CORRUPTION:
        this.badStatusMax.corruption *= 1.5f;
        if (Object.op_Inequality((Object) this.m_effectCorruption, (Object) null))
        {
          EffectManager.ReleaseEffect(this.m_effectCorruption.gameObject);
          this.m_effectCorruption = (GameObject) null;
          break;
        }
        break;
      case BuffParam.BUFFTYPE.STIGMATA:
        if (Object.op_Inequality((Object) this.m_effectStigmata, (Object) null))
        {
          EffectManager.ReleaseEffect(this.m_effectStigmata.gameObject);
          this.m_effectStigmata = (GameObject) null;
          break;
        }
        break;
      case BuffParam.BUFFTYPE.CYCLONIC_THUNDERSTORM:
        if (Object.op_Inequality((Object) this.m_effectCyclonicThunderstorm, (Object) null))
        {
          EffectManager.ReleaseEffect(this.m_effectCyclonicThunderstorm.gameObject);
          this.m_effectCyclonicThunderstorm = (GameObject) null;
          break;
        }
        break;
    }
    if (MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
      MonoBehaviourSingleton<UIEnemyAnnounce>.I.EndBuff(this.enemyTableData.name, type);
    if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
      MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
    return true;
  }

  protected override void OnUIBuffRoutine(BuffParam.BUFFTYPE type, int value)
  {
    if (!MonoBehaviourSingleton<UIDamageManager>.IsValid() || type != BuffParam.BUFFTYPE.REGENERATE && type != BuffParam.BUFFTYPE.REGENERATE_PROPORTION)
      return;
    MonoBehaviourSingleton<UIDamageManager>.I.CreateEnemyRecoverHp((Character) this, value, UIPlayerDamageNum.DAMAGE_COLOR.HEAL);
  }

  public override void OnPoisonStart(int fromObjectID = 0)
  {
    this.OnBuffStart(new BuffParam.BuffData()
    {
      type = BuffParam.BUFFTYPE.POISON,
      time = 20f,
      valueType = BuffParam.VALUE_TYPE.RATE,
      value = (int) ((double) this.hpMax * 0.014999999664723873),
      interval = 5f,
      fromObjectID = fromObjectID
    });
  }

  public void OnElectricShockStart(AttackHitInfo atkHitInfo, Player player)
  {
    if (atkHitInfo == null)
      return;
    ElectricShockInfo electricShockInfo = atkHitInfo.electricShockInfo;
    if (electricShockInfo == null)
      return;
    BuffParam.BuffData buffData = new BuffParam.BuffData();
    buffData.type = BuffParam.BUFFTYPE.ELECTRIC_SHOCK;
    buffData.time = electricShockInfo.duration;
    buffData.interval = electricShockInfo.damageInterval;
    buffData.value = 1;
    buffData.fromObjectID = player.id;
    if (Object.op_Inequality((Object) player, (Object) null))
    {
      BuffParam buffParam = player.buffParam;
      AtkAttribute atkAttribute = InGameUtility.CalcPlayerATK(new InGameUtility.PlayerAtkCalcData()
      {
        weaponAtk = player.attack,
        statusAtk = player.playerAtk,
        guardEquipAtk = player.GetGuardEquipmentAtk(),
        buffAtkRate = buffParam.GetBuffAtkRate(),
        passiveAtkRate = buffParam.GetPassiveAtkRate(),
        buffAtkConstant = buffParam.GetBuffAtkConstant(),
        buffAtkAllElementConstant = (float) buffParam.GetValue(BuffParam.BUFFTYPE.ATTACK_ALLELEMENT),
        passiveAtkConstant = buffParam.GetPassiveAtkUpConstant(),
        passiveAtkAllElementConstant = buffParam.passive.atkAllElement
      });
      buffData.damage = Mathf.FloorToInt(atkAttribute.CalcTotal() * ((float) electricShockInfo.atkRate * 0.01f));
    }
    this.OnBuffStart(buffData);
  }

  public override void OnDamageMotionStopStart(float time)
  {
    this.OnBuffStart(new BuffParam.BuffData()
    {
      type = BuffParam.BUFFTYPE.DAMAGE_MOTION_STOP,
      time = time,
      valueType = BuffParam.VALUE_TYPE.NONE
    });
  }

  public void OnSoilShockStart(AttackHitInfo atkHitInfo, Player player)
  {
    if (atkHitInfo == null)
      return;
    ElectricShockInfo soilShockInfo = atkHitInfo.soilShockInfo;
    if (soilShockInfo == null)
      return;
    BuffParam.BuffData buffData = new BuffParam.BuffData();
    buffData.type = BuffParam.BUFFTYPE.SOIL_SHOCK;
    buffData.time = soilShockInfo.duration;
    buffData.interval = soilShockInfo.damageInterval;
    buffData.value = 1;
    buffData.fromObjectID = player.id;
    if (Object.op_Inequality((Object) player, (Object) null))
    {
      BuffParam buffParam = player.buffParam;
      AtkAttribute atkAttribute = InGameUtility.CalcPlayerATK(new InGameUtility.PlayerAtkCalcData()
      {
        weaponAtk = player.attack,
        statusAtk = player.playerAtk,
        guardEquipAtk = player.GetGuardEquipmentAtk(),
        buffAtkRate = buffParam.GetBuffAtkRate(),
        passiveAtkRate = buffParam.GetPassiveAtkRate(),
        buffAtkConstant = buffParam.GetBuffAtkConstant(),
        buffAtkAllElementConstant = (float) buffParam.GetValue(BuffParam.BUFFTYPE.ATTACK_ALLELEMENT),
        passiveAtkConstant = buffParam.GetPassiveAtkUpConstant(),
        passiveAtkAllElementConstant = buffParam.passive.atkAllElement
      });
      buffData.damage = Mathf.FloorToInt(atkAttribute.CalcTotal() * ((float) soilShockInfo.atkRate * 0.01f));
    }
    this.OnBuffStart(buffData);
  }

  protected void CreateSoilShockEffect()
  {
    if (this.m_effectSoilShock != null)
      return;
    Transform effect = EffectManager.GetEffect("ef_btl_enm_gravity_01", this._transform);
    if (Object.op_Equality((Object) effect, (Object) null) || ((Component) effect).GetComponentsInChildren<ParticleSystem>(true) == null)
      return;
    this.CalcLightRingRadius();
    Transform transform1 = effect;
    transform1.localScale = Vector3.op_Multiply(transform1.localScale, this.lightRingRadius);
    float num = this.lightRingHeight + this.lightRingHeightOffset;
    Transform transform2 = effect;
    transform2.localPosition = Vector3.op_Addition(transform2.localPosition, Vector3.op_Multiply(Vector3.up, num));
    this.m_effectSoilShock = ((Component) effect).gameObject;
  }

  private void CreateBurningEffect()
  {
    if (this.m_effectBurning != null)
      return;
    Transform effect = EffectManager.GetEffect("ef_btl_enm_fire_01", this._transform);
    if (Object.op_Equality((Object) effect, (Object) null))
      return;
    ParticleSystem[] componentsInChildren = ((Component) effect).GetComponentsInChildren<ParticleSystem>(true);
    if (componentsInChildren == null)
      return;
    this.CalcFreezeEffectEmissionRadius();
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      ParticleSystem.ShapeModule shape = componentsInChildren[index].shape;
      ((ParticleSystem.ShapeModule) ref shape).radius = this.GetEmittionRadius();
    }
    Transform transform = effect;
    transform.localPosition = Vector3.op_Addition(transform.localPosition, Vector3.op_Multiply(Vector3.up, this.GetEmittionRadius()));
    this.m_effectBurning = ((Component) effect).gameObject;
  }

  private void CreateErosionEffect()
  {
    if (this.m_effectErosion != null)
      return;
    Transform effect = EffectManager.GetEffect("ef_btl_enm_erosion_01", this._transform);
    if (Object.op_Equality((Object) effect, (Object) null))
      return;
    ParticleSystem[] componentsInChildren = ((Component) effect).GetComponentsInChildren<ParticleSystem>(true);
    if (componentsInChildren == null)
      return;
    this.CalcFreezeEffectEmissionRadius();
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      ParticleSystem.ShapeModule shape = componentsInChildren[index].shape;
      ((ParticleSystem.ShapeModule) ref shape).radius = this.GetEmittionRadius();
    }
    Transform transform = effect;
    transform.localPosition = Vector3.op_Addition(transform.localPosition, Vector3.op_Multiply(Vector3.up, this.GetEmittionRadius()));
    this.m_effectErosion = ((Component) effect).gameObject;
  }

  private void CreateAcidEffect()
  {
    if (this.m_effectAcid != null)
      return;
    Transform effect = EffectManager.GetEffect("ef_btl_enm_acid_01", this._transform);
    if (Object.op_Equality((Object) effect, (Object) null))
      return;
    ParticleSystem[] componentsInChildren = ((Component) effect).GetComponentsInChildren<ParticleSystem>(true);
    if (componentsInChildren == null)
      return;
    this.CalcFreezeEffectEmissionRadius();
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      ParticleSystem.ShapeModule shape = componentsInChildren[index].shape;
      ((ParticleSystem.ShapeModule) ref shape).radius = this.GetEmittionRadius();
    }
    Transform transform = effect;
    transform.localPosition = Vector3.op_Addition(transform.localPosition, Vector3.op_Multiply(Vector3.up, this.GetEmittionRadius()));
    this.m_effectAcid = ((Component) effect).gameObject;
  }

  private void CreateCorruptionEffect()
  {
    if (this.m_effectCorruption != null)
      return;
    Transform effect = EffectManager.GetEffect("ef_btl_enm_corruption_01", this._transform);
    if (Object.op_Equality((Object) effect, (Object) null))
      return;
    ParticleSystem[] componentsInChildren = ((Component) effect).GetComponentsInChildren<ParticleSystem>(true);
    if (componentsInChildren == null)
      return;
    this.CalcFreezeEffectEmissionRadius();
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      ParticleSystem.ShapeModule shape = componentsInChildren[index].shape;
      ((ParticleSystem.ShapeModule) ref shape).radius = this.GetEmittionRadius();
    }
    Transform transform = effect;
    transform.localPosition = Vector3.op_Addition(transform.localPosition, Vector3.op_Multiply(Vector3.up, this.GetEmittionRadius()));
    this.m_effectCorruption = ((Component) effect).gameObject;
  }

  private void CreateStigmataEffect()
  {
    if (this.m_effectStigmata != null)
      return;
    Transform effect = EffectManager.GetEffect(MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.stigmataParam.effectName, this._transform);
    if (Object.op_Equality((Object) effect, (Object) null))
      return;
    ParticleSystem[] componentsInChildren = ((Component) effect).GetComponentsInChildren<ParticleSystem>(true);
    if (componentsInChildren == null)
      return;
    this.CalcFreezeEffectEmissionRadius();
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      ParticleSystem.ShapeModule shape = componentsInChildren[index].shape;
      ((ParticleSystem.ShapeModule) ref shape).radius = this.GetEmittionRadius();
    }
    Transform transform = effect;
    transform.localPosition = Vector3.op_Addition(transform.localPosition, Vector3.op_Multiply(Vector3.up, this.GetEmittionRadius()));
    this.m_effectStigmata = ((Component) effect).gameObject;
  }

  private void CreateCyclonicThunderstormEffect()
  {
    if (this.m_effectCyclonicThunderstorm != null)
      return;
    Transform effect = EffectManager.GetEffect(MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.cyclonicThunderstormParam.effectName, this._transform);
    if (Object.op_Equality((Object) effect, (Object) null))
      return;
    ParticleSystem[] componentsInChildren = ((Component) effect).GetComponentsInChildren<ParticleSystem>(true);
    if (componentsInChildren == null)
      return;
    this.CalcFreezeEffectEmissionRadius();
    for (int index = 0; index < componentsInChildren.Length; ++index)
    {
      ParticleSystem.ShapeModule shape = componentsInChildren[index].shape;
      ((ParticleSystem.ShapeModule) ref shape).radius = this.GetEmittionRadius();
    }
    Transform transform = effect;
    transform.localPosition = Vector3.op_Addition(transform.localPosition, Vector3.op_Multiply(Vector3.up, this.GetEmittionRadius()));
    this.m_effectCyclonicThunderstorm = ((Component) effect).gameObject;
  }

  private void CreateSpeedDownEffect()
  {
    if (this.m_effectSpeedDown != null)
      return;
    Transform effect = EffectManager.GetEffect("ef_btl_pl_movedown_01", this._transform);
    if (Object.op_Equality((Object) effect, (Object) null))
      return;
    this.CalcFreezeEffectEmissionRadius();
    float num = this.GetEmittionRadius() * MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.attackSpeedDownParam.enemyEffectSize;
    Transform transform = effect;
    transform.localScale = Vector3.op_Multiply(transform.localScale, num);
    this.m_effectSpeedDown = ((Component) effect).gameObject;
  }

  public override AttackHitColliderProcessor.HitParam SelectHitCollider(
    AttackHitColliderProcessor processor,
    List<AttackHitColliderProcessor.HitParam> hit_params)
  {
    this.checkHitParam.Clear();
    this.checkLength.Clear();
    this.checkPriority.Clear();
    this.targetRegionIds.Clear();
    if (processor.targetPointList != null)
    {
      int index = 0;
      for (int count = processor.targetPointList.Count; index < count; ++index)
      {
        TargetPoint targetPoint = processor.targetPointList[index];
        if (targetPoint.IsEneble() && targetPoint.regionID >= 0 && Object.op_Equality((Object) targetPoint.owner, (Object) this))
          this.targetRegionIds.Add(targetPoint.regionID);
      }
    }
    AnimEventCollider.AtkColliderHiter atkColliderHiter = (AnimEventCollider.AtkColliderHiter) null;
    BulletObject colliderInterface = processor.colliderInterface as BulletObject;
    if (Object.op_Equality((Object) colliderInterface, (Object) null))
      atkColliderHiter = processor.colliderInterface as AnimEventCollider.AtkColliderHiter;
    float num1 = 0.0f;
    Vector3 vector3_1 = Vector3.zero;
    Vector3 vector3_2 = Vector3.zero;
    Vector3 vector3_3;
    if (Object.op_Inequality((Object) colliderInterface, (Object) null))
    {
      Vector3 velocity = colliderInterface._rigidbody.velocity;
      num1 = ((Vector3) ref velocity).magnitude;
      vector3_3 = colliderInterface._rigidbody.velocity;
      vector3_1 = ((Vector3) ref vector3_3).normalized;
      vector3_2 = colliderInterface._transform.position;
    }
    float num2 = float.MaxValue;
    int index1 = 0;
    for (int count1 = hit_params.Count; index1 < count1; ++index1)
    {
      int regionId = this.GetRegionID(hit_params[index1].toCollider, this.targetRegionIds);
      hit_params[index1].regionID = regionId;
      int num3 = 0;
      EnemyRegionWork regionWork = this.regionWorks[regionId];
      Enemy.WEAK_STATE state = regionWork.weakState;
      if (regionId > 0)
        num3 = 1;
      if (Enemy.IsWeakStateCheckAlreadyHit(state) && Object.op_Inequality((Object) hit_params[index1].fromObject, (Object) null) && regionWork.weakAttackIDs.Contains(hit_params[index1].fromObject.id))
        state = Enemy.WEAK_STATE.NONE;
      if (Enemy.IsWeakStateSpAttack(state))
      {
        bool flag = false;
        Player fromObject = hit_params[index1].fromObject as Player;
        if (Object.op_Inequality((Object) fromObject, (Object) null))
          flag = fromObject.IsSpecialActionHit((Player.ATTACK_MODE) regionWork.weakSubParam, processor.attackInfo as AttackHitInfo, hit_params[index1]);
        hit_params[index1].isSpAttackHit = flag;
        if (!flag)
          state = Enemy.WEAK_STATE.NONE;
      }
      if (Object.op_Inequality((Object) colliderInterface, (Object) null) && colliderInterface.isAimMode)
      {
        bool flag = false;
        if (processor.targetPointList != null)
        {
          int index2 = 0;
          for (int count2 = processor.targetPointList.Count; index2 < count2; ++index2)
          {
            TargetPoint targetPoint = processor.targetPointList[index2];
            if (targetPoint.IsEneble() && targetPoint.regionID == regionId && Object.op_Equality((Object) targetPoint.owner, (Object) this))
            {
              Vector3 markerPos = targetPoint.param.markerPos;
              float magnitude;
              float num4;
              if (Vector3.op_Equality(vector3_1, Vector3.zero))
              {
                vector3_3 = Vector3.op_Subtraction(markerPos, hit_params[index1].point);
                magnitude = ((Vector3) ref vector3_3).magnitude;
                num4 = magnitude;
              }
              else
              {
                vector3_3 = Vector3.Cross(vector3_1, Vector3.op_Subtraction(markerPos, vector3_2));
                magnitude = ((Vector3) ref vector3_3).magnitude;
                num4 = Vector3.Dot(Vector3.op_Subtraction(markerPos, hit_params[index1].point), vector3_1);
              }
              float num5 = this.enemyParameter.aimMarkerHitRadius * targetPoint.param.aimMarkerScale;
              if ((double) magnitude <= (double) num5 && (targetPoint.isSkipDotCalc || (double) Mathf.Abs(num4) <= (double) this.enemyParameter.hitCompareAimDepthLimit))
              {
                flag = true;
                hit_params[index1].isHitAim = true;
                break;
              }
            }
          }
        }
        if (Enemy.IsWeakStateSpAttack(state))
          num3 = 4;
        else if (flag)
          num3 = 3;
        else if (regionId > 0 && (int) regionWork.hp > 0)
          num3 = 2;
      }
      else if (state != Enemy.WEAK_STATE.NONE)
      {
        num3 = 3;
        if (regionId > 0 && (int) regionWork.hp > 0)
          num3 = 4;
      }
      else if (regionId > 0 && (int) regionWork.hp > 0)
        num3 = 2;
      if (!this.regionInfos[hit_params[index1].regionID].isAtkColliderHit)
        num3 = 0;
      float num6 = 0.0f;
      if (Object.op_Inequality((Object) colliderInterface, (Object) null))
      {
        if (Object.op_Inequality((Object) colliderInterface._rigidbody, (Object) null) && Vector3.op_Inequality(vector3_1, Vector3.zero))
        {
          num6 = Vector3.Dot(hit_params[index1].point, vector3_1);
        }
        else
        {
          vector3_3 = Vector3.op_Subtraction(hit_params[index1].point, colliderInterface._transform.position);
          num6 = ((Vector3) ref vector3_3).magnitude;
        }
      }
      else if (Object.op_Inequality((Object) atkColliderHiter, (Object) null))
      {
        vector3_3 = Vector3.op_Subtraction(hit_params[index1].point, hit_params[index1].crossCheckPoint);
        num6 = ((Vector3) ref vector3_3).magnitude;
      }
      this.checkHitParam.Add(hit_params[index1]);
      this.checkLength.Add(num6);
      this.checkPriority.Add(num3);
      if ((double) num6 < (double) num2)
        num2 = num6;
    }
    AttackHitColliderProcessor.HitParam hitParam = (AttackHitColliderProcessor.HitParam) null;
    float maxValue = float.MaxValue;
    int num7 = 0;
    int index3 = 0;
    for (int count = this.checkHitParam.Count; index3 < count; ++index3)
    {
      if ((!Object.op_Inequality((Object) colliderInterface, (Object) null) || (double) num1 < (double) this.enemyParameter.hitCompareSpeed || (double) this.checkLength[index3] - (double) num2 <= (double) this.enemyParameter.hitCompareLengthLimit) && (this.checkPriority[index3] > num7 || this.checkPriority[index3] == num7 && (double) this.checkLength[index3] < (double) maxValue))
      {
        hitParam = this.checkHitParam[index3];
        maxValue = this.checkLength[index3];
        num7 = this.checkPriority[index3];
      }
    }
    return hitParam;
  }

  protected override bool IsValidAttackedHit(StageObject from_object)
  {
    return !(from_object is Enemy) && base.IsValidAttackedHit(from_object);
  }

  protected override void OnAttackedHitDirection(AttackedHitStatusDirection status)
  {
    Player fromObject1 = status.fromObject as Player;
    status.regionID = status.hitParam.regionID;
    EnemyRegionWork regionWork = (EnemyRegionWork) null;
    if (status.regionID >= 0 && status.regionID < this.regionInfos.Length)
      regionWork = this.regionWorks[status.regionID];
    if (regionWork == null)
    {
      status.weakState = Enemy.WEAK_STATE.NONE;
    }
    else
    {
      status.weakState = regionWork.weakState;
      if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.CANNON_BALL)
        status.weakState = Enemy.WEAK_STATE.NONE;
      if (Enemy.IsWeakStateSpAttack(status.weakState))
      {
        bool flag = false;
        if (Object.op_Inequality((Object) fromObject1, (Object) null))
          flag = fromObject1.IsSpecialActionHit((Player.ATTACK_MODE) regionWork.weakSubParam, status.attackInfo, status.hitParam);
        if (!flag)
          status.weakState = Enemy.WEAK_STATE.NONE;
      }
      if (Enemy.IsWeakStateElementAttack(status.weakState) || Enemy.IsWeakStateSkillAttack(status.weakState))
      {
        AtkAttribute atk = new AtkAttribute();
        if (status.hitParam.processor != null)
        {
          BulletObject colliderInterface = status.hitParam.processor.colliderInterface as BulletObject;
          if (Object.op_Inequality((Object) colliderInterface, (Object) null))
            atk = colliderInterface.masterAtk;
          else
            status.fromObject.GetAtk(status.attackInfo, ref atk);
        }
        if (Enemy.IsWeakStateElementAttack(status.weakState) && (ELEMENT_TYPE) regionWork.validElementType != atk.GetElementType())
          status.weakState = Enemy.WEAK_STATE.NONE;
        if (Enemy.IsWeakStateSkillAttack(status.weakState) && !status.attackInfo.isSkillReference)
          status.weakState = Enemy.WEAK_STATE.NONE;
      }
      if (Enemy.IsWeakStateHealAttack(status.weakState) && status.attackInfo.attackType != AttackHitInfo.ATTACK_TYPE.HEAL_ATTACK)
        status.weakState = Enemy.WEAK_STATE.NONE;
      if (Enemy.IsWeakStateCheckAlreadyHit(status.weakState))
      {
        if (regionWork.weakAttackIDs.Contains(status.fromObjectID))
          status.weakState = Enemy.WEAK_STATE.NONE;
        else if (!this.IsCoopNone() && !this.IsOriginal())
          regionWork.weakAttackIDs.Add(status.fromObjectID);
      }
      if (Enemy.IsWeakStateDisplaySign(status.weakState))
      {
        Self fromObject2 = status.fromObject as Self;
        if (Object.op_Inequality((Object) fromObject2, (Object) null))
          fromObject2.taskChecker.OnWeakAttack(status.weakState);
      }
      if (this.IsCannonBallHitShieldRegion(regionWork, status.attackInfo) && MonoBehaviourSingleton<UIEnemyStatus>.IsValid() && regionWork.isShieldCriticalDamage)
        MonoBehaviourSingleton<UIEnemyStatus>.I.PlayShakeHpGauge(5f, 0.5f, 0.05f);
      if (regionWork.regionInfo != null && regionWork.regionInfo.dragonArmorInfo.enabled)
        status.isDamageRegionOnly = true;
    }
    if (fromObject1 != null && status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.JUMP)
      fromObject1.HitJumpAttack();
    base.OnAttackedHitDirection(status);
  }

  protected override void OnPlayAttackedHitEffect(AttackedHitStatusDirection status)
  {
    bool flag1 = true;
    bool is_self = status.fromObject is Self;
    if (is_self)
    {
      if ((double) Enemy.selfHitEffectCool > 0.0)
        return;
      this.SetHitShock(Vector3.op_Subtraction(status.hitPos, status.fromObject._position));
      if (MonoBehaviourSingleton<InGameManager>.I.graphicOptionType <= 1)
        Enemy.selfHitEffectCool = 0.1f;
    }
    else
    {
      if ((double) Enemy.otherHitEffectCool > 0.0)
        return;
      string simpleHitEffectName = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.enemyOtherSimpleHitEffectName;
      if (!string.IsNullOrEmpty(simpleHitEffectName))
        EffectManager.OneShot(simpleHitEffectName, status.hitPos, status.hitParam.rot, is_self);
      flag1 = false;
      if (MonoBehaviourSingleton<InGameManager>.I.graphicOptionType <= 1)
        Enemy.otherHitEffectCool = 0.5f;
    }
    if (this.regionInfos == null)
      return;
    int regionId = status.regionID;
    if (regionId < 0 || regionId >= this.regionInfos.Length)
      return;
    Enemy.RegionInfo regionInfo = this.regionInfos[regionId];
    if (status.weakState != 0 & is_self)
      this.SetHitLight();
    if ((double) status.badStatusAdd.paralyze > 0.0)
    {
      string paralyzeHitEffectName = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.enemyParalyzeHitEffectName;
      if (!string.IsNullOrEmpty(paralyzeHitEffectName) & flag1)
        EffectManager.OneShot(paralyzeHitEffectName, status.hitPos, status.hitParam.rot, is_self);
    }
    if ((double) status.badStatusAdd.poison > 0.0)
    {
      string poisonHitEffectName = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.enemyPoisonHitEffectName;
      if (!string.IsNullOrEmpty(poisonHitEffectName) & flag1)
        EffectManager.OneShot(poisonHitEffectName, status.hitPos, status.hitParam.rot, is_self);
    }
    if ((double) status.badStatusAdd.freeze > 0.0)
    {
      string freezeHitEffectName = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.enemyFreezeHitEffectName;
      if (!string.IsNullOrEmpty(freezeHitEffectName) & flag1)
        EffectManager.OneShot(freezeHitEffectName, status.hitPos, status.hitParam.rot, is_self);
    }
    if (status.skillParam != null && status.skillParam.tableData != null)
    {
      bool flag2 = false;
      if (status.skillParam.tableData.hitSEID != 0)
      {
        if (this.EnablePlaySound())
          SoundManager.PlayOneShotSE(status.skillParam.tableData.hitSEID, status.hitPos);
        flag2 = true;
      }
      if (!string.IsNullOrEmpty(status.skillParam.tableData.hitEffectName))
      {
        if (flag1)
          EffectManager.OneShot(status.skillParam.tableData.hitEffectName, status.hitPos, status.hitParam.rot, is_self);
        flag2 = true;
      }
      if (flag2)
        return;
    }
    bool flag3 = false;
    bool flag4 = false;
    bool flag5 = false;
    if (status.attackInfo.hitSEID != 0)
    {
      if (this.EnablePlaySound())
        SoundManager.PlayOneShotSE(status.attackInfo.hitSEID, status.hitPos);
      flag4 = true;
      if (!status.attackInfo.playCommonHitEffect)
        flag3 = true;
    }
    if (!string.IsNullOrEmpty(status.attackInfo.hitEffectName))
    {
      if (flag1)
        EffectManager.OneShot(status.attackInfo.hitEffectName, status.hitPos, status.hitParam.rot, is_self);
      flag5 = true;
      if (!status.attackInfo.playCommonHitSe)
        flag3 = true;
    }
    if (flag3 || status.skillParam != null)
      return;
    ELEMENT_TYPE elementType = status.atk.GetElementType();
    Enemy.EFFECTIVE_TYPE effectiveType = Enemy.GetEffectiveType(elementType, this.GetElementType());
    string str = status.attackInfo.toEnemy.hitTypeName;
    EnemyHitTypeTable.TypeData typeData = (EnemyHitTypeTable.TypeData) null;
    Vector3 scale = Vector3.one;
    float delay = 0.0f;
    if (is_self && status.fromObject is Player fromObject)
      typeData = fromObject.GetOverrideHitEffect(status, ref scale, ref delay);
    if (typeData == null && !string.IsNullOrEmpty(str))
    {
      char ch = str[str.Length - 1];
      if (ELEMENT_TYPE.MAX == elementType)
        typeData = Singleton<EnemyHitTypeTable>.I.GetData(str, FieldManager.IsValidInGameNoQuest());
      else if (effectiveType == Enemy.EFFECTIVE_TYPE.GOOD && ch == 'S')
      {
        str = str.Substring(0, str.Length - 1) + "L";
        typeData = Singleton<EnemyHitTypeTable>.I.GetData(str, FieldManager.IsValidInGameNoQuest());
      }
      else if (effectiveType != Enemy.EFFECTIVE_TYPE.GOOD && ch == 'L')
      {
        str = str.Substring(0, str.Length - 1) + "S";
        typeData = Singleton<EnemyHitTypeTable>.I.GetData(str, FieldManager.IsValidInGameNoQuest());
      }
      else
        typeData = Singleton<EnemyHitTypeTable>.I.GetData(str, FieldManager.IsValidInGameNoQuest());
    }
    string name = regionInfo.hitMaterialName;
    if (string.IsNullOrEmpty(name))
      name = this.baseHitMaterialName;
    EnemyHitMaterialTable.MaterialData materialData = (EnemyHitMaterialTable.MaterialData) null;
    if (!string.IsNullOrEmpty(name))
      materialData = Singleton<EnemyHitMaterialTable>.I.GetData(name);
    if (typeData != null && !flag5)
    {
      string element_effect_name = (string) null;
      element_effect_name = elementType != ELEMENT_TYPE.MAX ? typeData.elementEffectNames[(int) elementType] : typeData.baseEffectName;
      if (status.damageDistanceData != null && status.attackInfo.name.Contains("PLC05_attack_00") && status.damageDistanceData.IsMaxRate(status.distanceXZ))
        element_effect_name = MonoBehaviourSingleton<InGameSettingsManager>.I.player.bestDistanceEffect;
      if (!string.IsNullOrEmpty(element_effect_name) && flag1)
      {
        if ((double) delay > 0.0)
          AppMain.Delay(delay, (System.Action) (() => EffectManager.OneShot(element_effect_name, status.hitPos, status.hitParam.rot, scale, is_self)));
        else
          EffectManager.OneShot(element_effect_name, status.hitPos, status.hitParam.rot, scale, is_self);
      }
    }
    if (elementType != ELEMENT_TYPE.MAX && !flag4)
    {
      int elementHitSeiD = this.enemyParameter.elementHitSEIDs[(int) elementType];
      if (elementHitSeiD != 0 && this.EnablePlaySound())
        SoundManager.PlayOneShotSE(elementHitSeiD, status.hitPos);
    }
    if (materialData != null)
    {
      if (!string.IsNullOrEmpty(materialData.addEffectName) && flag1)
        EffectManager.OneShot(materialData.addEffectName, status.hitPos, status.hitParam.rot, is_self);
      if (typeData != null && !flag4)
      {
        int typeSeid = materialData.GetTypeSEID(str);
        if (typeSeid != 0 && this.EnablePlaySound())
          SoundManager.PlayOneShotSE(typeSeid, status.hitPos);
      }
    }
    EnemyRegionWork regionWork = this.regionWorks[regionId];
    if (status.attackInfo.attackType != AttackHitInfo.ATTACK_TYPE.CANNON_BALL)
      return;
    string effect_name = "ef_btl_magibullet_landing_03";
    if (this.IsCannonBallHitShieldRegion(regionWork, status.attackInfo))
    {
      effect_name = "ef_btl_magibullet_landing_01";
      if (regionWork.isShieldCriticalDamage)
        effect_name = "ef_btl_magibullet_landing_02";
    }
    else if (regionWork.isShieldDamage && regionWork.weakState == Enemy.WEAK_STATE.WEAK_GRAB)
      effect_name = "ef_btl_magibullet_landing_01";
    Transform effect = EffectManager.GetEffect(effect_name);
    effect.position = status.exHitPos;
    effect.rotation = status.hitParam.rot;
    effect.localScale = Vector3.one;
  }

  private void OnHitWeakPoint(string deleteAtkName, bool isSpWeak)
  {
    this.ActReleaseGrabbedPlayers(true, isSpWeak, false);
    if (string.IsNullOrEmpty(deleteAtkName))
      return;
    for (int index = this.m_activeAttackLaserList.Count - 1; index >= 0; --index)
    {
      AttackNWayLaser activeAttackLaser = this.m_activeAttackLaserList[index];
      if (activeAttackLaser.AttackInfoName == deleteAtkName)
        activeAttackLaser.RequestDestroy();
    }
    for (int index = this.m_activeAttackFunnelList.Count - 1; index >= 0; --index)
    {
      AttackFunnelBit activeAttackFunnel = this.m_activeAttackFunnelList[index];
      if (activeAttackFunnel.AttackInfoName == deleteAtkName)
        activeAttackFunnel.RequestDestroy();
    }
    for (int index = this.m_activeAttackDigList.Count - 1; index >= 0; --index)
    {
      AttackDig activeAttackDig = this.m_activeAttackDigList[index];
      if (activeAttackDig.AttackInfoName == deleteAtkName)
        activeAttackDig.RequestDestroy();
    }
    for (int index = this.m_activeAttackActionMineList.Count - 1; index >= 0; --index)
    {
      AttackActionMine attackActionMine = this.m_activeAttackActionMineList[index];
      if (attackActionMine.AttackInfoName == deleteAtkName)
        attackActionMine.RequestDestroy(false);
    }
    for (int index = this.m_activeAttackObstacleList.Count - 1; index >= 0; --index)
    {
      AttackShotNodeLink activeAttackObstacle = this.m_activeAttackObstacleList[index];
      if (activeAttackObstacle.AttackInfoName == deleteAtkName)
        activeAttackObstacle.RequestDestroy();
    }
  }

  public void OnDestroyFunnel(AttackFunnelBit delFunnel)
  {
    if (!this.m_activeAttackFunnelList.Contains(delFunnel))
      return;
    this.m_activeAttackFunnelList.Remove(delFunnel);
  }

  public void OnDestroyLaser(AttackNWayLaser delLaser)
  {
    if (!this.m_activeAttackLaserList.Contains(delLaser))
      return;
    this.m_activeAttackLaserList.Remove(delLaser);
  }

  public void OnDestroyDig(AttackDig delDig)
  {
    if (!this.m_activeAttackDigList.Contains(delDig))
      return;
    this.m_activeAttackDigList.Remove(delDig);
  }

  public void ActDestroyActionMine(int objId, bool isExplode)
  {
    AttackActionMine attackActionMine = this.m_activeAttackActionMineList.Find((Predicate<AttackActionMine>) (x => x.objId == objId));
    if (!Object.op_Inequality((Object) attackActionMine, (Object) null))
      return;
    attackActionMine.RequestDestroy(isExplode);
  }

  public void OnDestroyActionMine(AttackActionMine mine)
  {
    if (!this.m_activeAttackActionMineList.Contains(mine))
      return;
    this.m_activeAttackActionMineList.Remove(mine);
  }

  public void OnDestroyObstacle(AttackShotNodeLink delObstacle)
  {
    if (!this.m_activeAttackObstacleList.Contains(delObstacle))
      return;
    this.m_activeAttackObstacleList.Remove(delObstacle);
  }

  protected override bool IsDamageValid(AttackedHitStatusDirection status)
  {
    return status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.GIMMICK_GENERATED || status.fromType == StageObject.OBJECT_TYPE.PLAYER;
  }

  public override void AbsorptionProc(Character targetChar, AttackedHitStatusLocal status)
  {
    AttackHitInfo attackInfo = status.attackInfo;
    if (attackInfo == null || (double) attackInfo.absorptance <= 0.0)
      return;
    int recoverValue = (int) ((double) this.hpMax * (double) (attackInfo.absorptance * 0.01f));
    if (recoverValue <= 0)
      return;
    Player player = targetChar as Player;
    if (Object.op_Inequality((Object) player, (Object) null))
      player.StartEffectDrain(this);
    this.RecoverHp(recoverValue, true);
  }

  public override bool CutAndAbsorbDamageByBuff(
    Character targetCharacter,
    AttackedHitStatusLocal status)
  {
    List<BuffParam.BuffData> absorbBuffDataList = this.buffParam.GetAbsorbBuffDataList();
    if (absorbBuffDataList.IsNullOrEmpty<BuffParam.BuffData>())
      return false;
    AtkAttribute val = new AtkAttribute();
    for (int index = 0; index < absorbBuffDataList.Count; ++index)
    {
      switch (absorbBuffDataList[index].type)
      {
        case BuffParam.BUFFTYPE.ABSORB_NORMAL:
          val.normal += status.damageDetails.normal;
          break;
        case BuffParam.BUFFTYPE.ABSORB_FIRE:
          val.fire += status.damageDetails.fire;
          break;
        case BuffParam.BUFFTYPE.ABSORB_WATER:
          val.water += status.damageDetails.water;
          break;
        case BuffParam.BUFFTYPE.ABSORB_THUNDER:
          val.thunder += status.damageDetails.thunder;
          break;
        case BuffParam.BUFFTYPE.ABSORB_SOIL:
          val.soil += status.damageDetails.soil;
          break;
        case BuffParam.BUFFTYPE.ABSORB_LIGHT:
          val.light += status.damageDetails.light;
          break;
        case BuffParam.BUFFTYPE.ABSORB_DARK:
          val.dark += status.damageDetails.dark;
          break;
      }
    }
    val.CheckMinus();
    status.damageDetails.Sub(val);
    status.damage = Mathf.FloorToInt(status.damageDetails.CalcTotal());
    int recoverValue = Mathf.FloorToInt(val.CalcTotal());
    int num = Mathf.FloorToInt((float) this.hpMax * MonoBehaviourSingleton<InGameSettingsManager>.I.buff.absorbDamageParam.limitRateEnemyAbsorbDamage);
    if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid() && recoverValue > num)
      recoverValue = num;
    if (recoverValue <= 0)
      return false;
    this.RecoverHp(recoverValue, true);
    return true;
  }

  protected override void OnAttackedHitLocal(AttackedHitStatusLocal status)
  {
    base.OnAttackedHitLocal(status);
    this._CheckHitLocalArrow(status);
    if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.BOMBROCK)
    {
      status.damage = Mathf.FloorToInt((float) ((double) this.hpMax * (double) status.attackInfo.atk.normal * 0.0099999997764825821));
      status.damageDetails = new AtkAttribute();
      status.damageDetails.normal = (float) status.damage;
    }
    if (status.attackInfo.isSkillReference && (this.isAvailableCounter(this.actionID) || this.actionID == (Character.ACTION_ID) 17) && this.GetEnabledCounterRegion() != null)
    {
      status.damage = 0;
      status.damageDetails = new AtkAttribute();
    }
    if (this.enemyParameter.showDamageNum && (status.fromObject is Self || status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.GIMMICK_GENERATED) && status.attackInfo.attackType != AttackHitInfo.ATTACK_TYPE.CANNON_BALL)
      this.CreateDamageNum(status.hitPos, status.damageDetails, status.weakState != 0, status.isDamageRegionOnly, status.attackInfo.damageNumAddGroup);
    Player fromObject = status.fromObject as Player;
    if (Object.op_Inequality((Object) fromObject, (Object) null) && !status.attackInfo.isSkillReference)
    {
      fromObject.CountHitAttack();
      fromObject.IncreaseSpActonGauge(status.attackInfo, status.hitPos);
      if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.NORMAL || status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.FROM_AVOID)
        fromObject.UpdateBoostHitCount();
      if (fromObject is Self && MonoBehaviourSingleton<InGameManager>.IsValid())
        MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.AddDamageByWeapon(fromObject.weaponIndex, status.damage);
    }
    status.downAddBase = 0.0f;
    status.downAddWeak = 0.0f;
    if (status.damage > 0)
    {
      status.downAddBase = status.attackInfo.down;
      status.isForceDown = status.attackInfo.isForceDown;
      if (Object.op_Inequality((Object) fromObject, (Object) null))
      {
        int num = 0;
        if (fromObject.GetOneHandSwordBoostDownValue(ref num, status.attackInfo.name))
        {
          status.downAddBase += (float) num;
        }
        else
        {
          switch (status.weakState)
          {
            case Enemy.WEAK_STATE.WEAK:
              status.downAddWeak = fromObject.downPowerSimpleWeak * status.attackInfo.atkRate;
              if ((double) status.attackInfo.toEnemy.concussion > 0.0)
              {
                status.concussionAdd = status.attackInfo.toEnemy.concussion;
                break;
              }
              break;
            case Enemy.WEAK_STATE.WEAK_SP_ATTACK:
            case Enemy.WEAK_STATE.WEAK_ELEMENT_ATTACK:
            case Enemy.WEAK_STATE.WEAK_ELEMENT_SKILL_ATTACK:
            case Enemy.WEAK_STATE.WEAK_SKILL_ATTACK:
            case Enemy.WEAK_STATE.WEAK_HEAL_ATTACK:
            case Enemy.WEAK_STATE.WEAK_ELEMENT_SP_ATTACK:
              status.downAddWeak = fromObject.downPowerWeak * status.attackInfo.atkRate;
              if ((double) status.attackInfo.toEnemy.concussion > 0.0)
              {
                status.concussionAdd = status.attackInfo.toEnemy.concussion;
                break;
              }
              break;
            case Enemy.WEAK_STATE.WEAK_SP_DOWN_MAX:
              status.downAddWeak = (float) this.downMax;
              break;
          }
        }
        float hitHealRate = status.attackInfo.hitHealRate;
        if ((double) hitHealRate > 0.0 && fromObject.hp < fromObject.hpMax)
        {
          Character.HealData healData = new Character.HealData(Mathf.FloorToInt((float) status.damage * hitHealRate), HEAL_TYPE.NONE, HEAL_EFFECT_TYPE.BASIS, new List<int>()
          {
            80 /*0x50*/
          });
          fromObject.OnHealReceive(healData);
        }
      }
      if ((double) status.downAddBase > 0.0)
      {
        status.downAddBase *= fromObject.buffParam.GetBadStatusRateUp(BuffParam.BAD_STATUS_UP.DOWN);
        status.downAddBase += fromObject.buffParam.GetBadStatusUp(BuffParam.BAD_STATUS_UP.DOWN);
      }
      else if ((double) status.downAddWeak > 0.0)
      {
        status.downAddWeak *= fromObject.buffParam.GetBadStatusRateUp(BuffParam.BAD_STATUS_UP.DOWN);
        status.downAddWeak += fromObject.buffParam.GetBadStatusUp(BuffParam.BAD_STATUS_UP.DOWN);
      }
      if ((double) status.concussionAdd > 0.0)
      {
        status.concussionAdd *= fromObject.buffParam.GetBadStatusRateUp(BuffParam.BAD_STATUS_UP.CONCUSSION);
        status.concussionAdd += fromObject.buffParam.GetBadStatusUp(BuffParam.BAD_STATUS_UP.CONCUSSION);
      }
    }
    if (MonoBehaviourSingleton<CoopNetworkManager>.IsValid())
      MonoBehaviourSingleton<CoopNetworkManager>.I.EnemyAttack(this.id, status.damage);
    this.RecordDeliveryBattleCheckerOnAttacked(status);
  }

  private void RecordDeliveryBattleCheckerOnAttacked(AttackedHitStatusLocal status)
  {
    Self fromObject = status.fromObject as Self;
    if (Object.op_Equality((Object) fromObject, (Object) null) || !MonoBehaviourSingleton<InGameManager>.IsValid())
      return;
    MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.SetMaxDamageSelf(status.damage);
    BattleCheckerBase.JudgementParam judgementParam = BattleCheckerBase.JudgementParam.Create((AttackInfo) status.attackInfo, fromObject);
    MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnAttackHit(status.attackInfo.name, judgementParam, status.damage);
    if (Enemy.IsWeakStateDisplaySign(status.weakState))
      MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnWeakAttack(status.weakState, status.origin.damage);
    if (status.attackInfo.attackType != AttackHitInfo.ATTACK_TYPE.JUMP)
      return;
    MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnJump(status.origin.damage);
  }

  private void _CheckHitLocalArrow(AttackedHitStatusLocal status)
  {
    if (status.hitParam.processor == null || !(status.hitParam.processor.colliderInterface is BulletObject colliderInterface) || (double) status.attackInfo.rateInfoRate < 1.0)
      return;
    Character fromObject = status.fromObject as Character;
    status.isArrowBleed = false;
    status.isShadowSealing = false;
    status.isArrowBomb = false;
    if (!status.hitParam.isHitAim)
      return;
    switch (status.attackInfo.spAttackType)
    {
      case SP_ATTACK_TYPE.NONE:
        status.isArrowBleed = true;
        float val = Object.op_Inequality((Object) fromObject, (Object) null) ? fromObject.buffParam.GetBleedUp() : 1f;
        status.arrowBleedDamage = Mathf.CeilToInt((float) status.damage * MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.arrowBleedDamageRate * val);
        if (status.arrowBleedDamage < 1)
          status.arrowBleedDamage = 1;
        double normal = (double) status.damageDetails.normal;
        ELEMENT_TYPE elementType = status.damageDetails.GetElementType();
        status.damage = (int) ((double) status.damage * (double) val);
        status.damageDetails.Mul(val);
        if (normal > 0.0 && (double) status.damageDetails.normal < 1.0)
        {
          status.damageDetails.normal = 1f;
          ++status.damage;
        }
        if (elementType != ELEMENT_TYPE.MAX && status.damageDetails.GetElementType() == ELEMENT_TYPE.MAX)
        {
          status.damageDetails.SetTargetElement(elementType, 1f);
          ++status.damage;
        }
        if (status.damage < 1)
        {
          status.damageDetails.normal = 1f;
          status.damage = 1;
        }
        Enemy.BleedData bleedData = (Enemy.BleedData) null;
        EnemyRegionWork regionWork = this.regionWorks[status.regionID];
        int index = 0;
        for (int count = regionWork.bleedList.Count; index < count; ++index)
        {
          if (regionWork.bleedList[index].ownerID == status.fromObjectID)
          {
            bleedData = regionWork.bleedList[index];
            break;
          }
        }
        if (bleedData == null || Enemy.BleedData.MaxLv != bleedData.lv + 1)
          break;
        InGameSettingsManager.Player.SpecialActionInfo specialActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo;
        int num1 = status.arrowBleedDamage / specialActionInfo.arrowBleedCount;
        if (num1 < 1)
          num1 = 1;
        float num2 = (float) (bleedData.damage + num1) * specialActionInfo.arrowBurstDamageRate;
        status.arrowBurstDamage = (int) num2;
        status.damage += status.arrowBurstDamage;
        break;
      case SP_ATTACK_TYPE.HEAT:
        status.isShadowSealing = true;
        if (!colliderInterface.isBossPierceArrow || this.isPierceAfterTarget || this.regionWorks[status.regionID].shadowSealingData.ownerID != 0)
          break;
        colliderInterface.EndBossPierceArrow();
        break;
      case SP_ATTACK_TYPE.BURST:
        status.isArrowBomb = true;
        if (!colliderInterface.isBossPierceArrow || this.isPierceAfterTarget || this.regionWorks[status.regionID].shadowSealingData.ownerID != 0)
          break;
        colliderInterface.EndBossPierceArrow();
        break;
    }
  }

  protected override void OnIgnoreHitAttack()
  {
    base.OnIgnoreHitAttack();
    if (!this.IsValidBuff(BuffParam.BUFFTYPE.GHOST_FORM) || !Object.op_Equality((Object) this.m_effectHitWhenGhost, (Object) null) || Object.op_Equality((Object) this.effectPlayProcessor, (Object) null))
      return;
    List<EffectPlayProcessor.EffectSetting> settings = this.effectPlayProcessor.GetSettings("GHOST_EFFECT");
    if (settings == null || settings[0] == null)
      return;
    Transform transform = this.effectPlayProcessor.PlayEffect(settings[0], this._transform);
    if (!Object.op_Inequality((Object) transform, (Object) null))
      return;
    this.m_effectHitWhenGhost = ((Component) transform).gameObject;
  }

  protected override bool CheckStatusForHitEffect(AttackedHitStatusDirection status)
  {
    if (!this.IsValidBuff(BuffParam.BUFFTYPE.GHOST_FORM))
      return base.CheckStatusForHitEffect(status);
    Player fromObject = status.fromObject as Player;
    if (Object.op_Inequality((Object) fromObject, (Object) null) && fromObject.CheckIgnoreBuff(BuffParam.BUFFTYPE.GHOST_FORM))
      return true;
    AtkAttribute damage_details = new AtkAttribute();
    return this.CalcDamage(new AttackedHitStatusLocal(this.nowAttackedHitStatus), ref damage_details) > 0;
  }

  public override void GetAtk(
    AttackHitInfo info,
    ref AtkAttribute atk,
    SkillInfo.SkillParam skillParamInfo = null)
  {
    atk.Copy(InGameUtility.CalcEnemyATK(new InGameUtility.EnemyAtkCalcData()
    {
      atkInfo = info,
      buffAtkRate = this.buffParam.GetBuffAtkRate(),
      buffAtkConstant = this.buffParam.GetBuffAtkConstant(),
      buffAtkAllElementConstant = (float) this.buffParam.GetValue(BuffParam.BUFFTYPE.ATTACK_ALLELEMENT)
    }));
  }

  protected override int CalcDamage(AttackedHitStatusLocal status, ref AtkAttribute damage_details)
  {
    if (this.regionInfos == null)
      return 0;
    if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.CANNON_BALL_DIRECT)
    {
      damage_details.Copy(status.attackInfo.atk);
      return Mathf.FloorToInt(status.attackInfo.atk.CalcTotal());
    }
    if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.SHIELD_REFLECT)
    {
      float num = status.attackInfo.atk.normal;
      if ((int) this.BarrierHp > 0)
        num = 1f;
      if (this.buffParam.IsValidInvincibleBuff())
      {
        AtkAttribute invinsibleMulRate = this.GetInvinsibleMulRate();
        num *= invinsibleMulRate.normal;
        if ((double) num < 0.0)
          num = 0.0f;
      }
      if (this.IsValidBuff(BuffParam.BUFFTYPE.GHOST_FORM))
      {
        num *= this.GhostFormParam.normal;
        if ((double) num < 0.0)
          num = 0.0f;
      }
      if (this.IsValidShield())
      {
        num *= this.ShieldTolerance.normal;
        if ((double) num < 0.0)
          num = 0.0f;
      }
      damage_details.normal = num;
      return Mathf.FloorToInt(num);
    }
    AtkAttribute atkAttribute1 = this.CalcAtk(status);
    AtkAttribute atkAttribute2 = this.CalcTolerance(status);
    AtkAttribute atkAttribute3 = this.CalcDefense(status);
    if (status.attackInfo.toEnemy.damageToRegionInfo.ignoreWeakElementDefence && atkAttribute1.GetElementType() == this.GetAntiElementTypeByRegion())
      atkAttribute2.SetTargetElement(atkAttribute1.GetElementType(), 0.0f);
    damage_details.normal = (float) (int) InGameUtility.CalcDamageDetailToEnemy(atkAttribute1.normal, atkAttribute3.normal, atkAttribute2.normal);
    damage_details.fire = (float) (int) InGameUtility.CalcDamageDetailToEnemy(atkAttribute1.fire, atkAttribute3.fire, atkAttribute2.fire);
    damage_details.water = (float) (int) InGameUtility.CalcDamageDetailToEnemy(atkAttribute1.water, atkAttribute3.water, atkAttribute2.water);
    damage_details.thunder = (float) (int) InGameUtility.CalcDamageDetailToEnemy(atkAttribute1.thunder, atkAttribute3.thunder, atkAttribute2.thunder);
    damage_details.soil = (float) (int) InGameUtility.CalcDamageDetailToEnemy(atkAttribute1.soil, atkAttribute3.soil, atkAttribute2.soil);
    damage_details.light = (float) (int) InGameUtility.CalcDamageDetailToEnemy(atkAttribute1.light, atkAttribute3.light, atkAttribute2.light);
    damage_details.dark = (float) (int) InGameUtility.CalcDamageDetailToEnemy(atkAttribute1.dark, atkAttribute3.dark, atkAttribute2.dark);
    damage_details.CheckMinus();
    float normal = atkAttribute1.normal;
    ELEMENT_TYPE elementType = atkAttribute1.GetElementType();
    if (this.regionInfos[status.regionID].isDamageMinimum)
    {
      damage_details.Set(0.0f);
      if ((double) normal > 0.0)
        damage_details.normal = 1f;
      if (elementType != ELEMENT_TYPE.MAX)
        damage_details.SetTargetElement(elementType, 1f);
      int num = Mathf.FloorToInt(damage_details.CalcTotal());
      if (num < 1)
      {
        damage_details.normal = 1f;
        num = 1;
      }
      return num;
    }
    Player fromObject = status.fromObject as Player;
    if (Object.op_Inequality((Object) fromObject, (Object) null))
    {
      this.CalcDamageByRegionWeaponTypeRate(fromObject, status, ref damage_details);
      BuffParam buffParam = fromObject.buffParam;
      AtkAttribute abilityDamageRate = buffParam.GetAbilityDamageRate((Character) this, status);
      abilityDamageRate.CheckMinus();
      float damageUpRate = buffParam.GetDamageUpRate(fromObject, status);
      if ((double) damageUpRate > 0.0)
        abilityDamageRate.AddRate(damageUpRate);
      damage_details.Mul(abilityDamageRate);
      if (this.aegisCtrl != null && this.aegisCtrl.IsValid())
        damage_details.Mul(status.attackInfo.toEnemy.aegisDamageRate);
    }
    if (status.attackInfo.isSkillReference && this.IsValidBuff(BuffParam.BUFFTYPE.MAD_MODE))
      damage_details.Mul(MonoBehaviourSingleton<InGameSettingsManager>.I.madModeParam.skillDamagedRate);
    if (this.regionInfos[status.regionID].dragonArmorInfo.enabled)
      damage_details.Mul(status.attackInfo.toEnemy.damageToRegionInfo.dragonArmorDamageRate + fromObject.buffParam.GetDragonArmorDamageRate());
    int num1 = (int) damage_details.CalcTotal();
    if ((double) normal > 0.0 && (double) damage_details.normal < 1.0)
    {
      damage_details.normal = 1f;
      ++num1;
    }
    if (elementType != ELEMENT_TYPE.MAX && damage_details.GetElementType() == ELEMENT_TYPE.MAX)
    {
      damage_details.SetTargetElement(elementType, 1f);
      ++num1;
    }
    if (num1 < 1)
    {
      damage_details.normal = 1f;
      num1 = 1;
    }
    if (Object.op_Inequality((Object) fromObject, (Object) null))
    {
      if (!status.attackInfo.isSkillReference)
      {
        this.CalcElementDamageByAttackerWeapon(fromObject, status, ref damage_details);
        num1 = (int) damage_details.CalcTotal();
        if (num1 < 1)
          num1 = 1;
        if (this.isArenaDamageOffWeapon)
        {
          damage_details.Mul(0.0f);
          int num2 = (int) damage_details.CalcTotal();
          if (num2 < 0)
            num2 = 0;
          return num2;
        }
      }
      else if (this.isArenaDamageOffMagi)
      {
        damage_details.Mul(0.0f);
        int num3 = (int) damage_details.CalcTotal();
        if (num3 < 0)
          num3 = 0;
        return num3;
      }
    }
    if (this.buffParam.IsValidInvincibleBuff())
    {
      AtkAttribute invinsibleMulRate = this.GetInvinsibleMulRate();
      damage_details.Mul(invinsibleMulRate);
      num1 = (int) damage_details.CalcTotal();
      if (num1 < 0)
        num1 = 0;
    }
    if (this.IsValidBuff(BuffParam.BUFFTYPE.GHOST_FORM))
    {
      if (Object.op_Inequality((Object) fromObject, (Object) null) && fromObject.CheckIgnoreBuff(BuffParam.BUFFTYPE.GHOST_FORM))
        return num1;
      damage_details.Mul(this.GhostFormParam);
      num1 = (int) damage_details.CalcTotal();
      if (num1 < 0)
        num1 = 0;
    }
    if (this.IsValidShield())
    {
      damage_details.Mul(this.ShieldTolerance);
      num1 = (int) damage_details.CalcTotal();
      if (num1 < 0)
        num1 = 0;
    }
    return num1;
  }

  private void CalcDamageByRegionWeaponTypeRate(
    Player attacker,
    AttackedHitStatusLocal status,
    ref AtkAttribute damage_details)
  {
    Enemy.RegionInfo regionInfo = this.regionInfos[status.regionID];
    if (status.attackInfo.isSkillReference || regionInfo.dragonArmorInfo.enabled)
      return;
    float val = 1f;
    for (int index = 0; index < regionInfo.weaponTypeRate.Length; ++index)
    {
      if (Player.ConvertEquipmentTypeToAttackMode(regionInfo.weaponTypeRate[index].equipmentType) == attacker.attackMode)
        val = regionInfo.weaponTypeRate[index].rate;
    }
    damage_details.Mul(val);
  }

  private void CalcElementDamageByAttackerWeapon(
    Player attacker,
    AttackedHitStatusLocal status,
    ref AtkAttribute damage_details)
  {
    if (attacker.CheckAttackModeAndSpType(Player.ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.NONE))
      damage_details.MulElementOnly(attacker.CalcChargeExpandElementDamageUpRate());
    float num = 1f;
    if (attacker.GetOneHandSwordBoostDamageUpRate(ref num))
      damage_details.MulElementOnly(num);
    if (attacker.GetTwoHandSwordBoostDamageUpRate(ref num))
      damage_details.MulElementOnly(num);
    if (attacker.GetIaiNormalDamageUp(ref num))
      damage_details.normal *= num;
    if (attacker.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.HEAT))
      damage_details.Mul(attacker.CalcPairSwordsBoostModeDamageUpRate());
    if (attacker.pairSwordsCtrl.GetElementDamageUpRate(ref num))
      damage_details.MulElementOnly(num);
    if (attacker.GetSphinxElementDamageUpRate(status.attackInfo, ref num))
      damage_details.MulElementOnly(num);
    if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.JUMP)
      damage_details.MulElementOnly(attacker.GetJumpElementDamageUpRate());
    if (attacker.GetExRushElementDamageUpRate(status.attackInfo.attackType, ref num))
      damage_details.MulElementOnly(num);
    if (attacker.spearCtrl.GetBoostDamageUpRate(ref num))
      damage_details.MulElementOnly(num);
    if (attacker.GetArrowBoostDamageUpRate(ref num))
      damage_details.MulElementOnly(num);
    if (attacker.GetBurstShotNormalDamageUpRate(status.attackInfo, ref num))
      damage_details.normal *= num;
    if (attacker.GetBurstShotElementDamageUpRate(status.attackInfo, ref num))
      damage_details.MulElementOnly(num);
    if (attacker.GetBurstArrowBombElementDamageUpRate(status.attackInfo, ref num))
      damage_details.MulElementOnly(num);
    if (attacker.IsOracleTwoHandSword())
    {
      if (attacker.thsCtrl.oracleCtrl.GetHorizontalDamageUpRate(status.attackInfo.name, ref num))
        damage_details.Mul(num);
      if (attacker.thsCtrl.oracleCtrl.GetChargeNormalDamageUpRate(status.attackInfo.attackType, ref num))
        damage_details.normal *= num;
      if (attacker.thsCtrl.oracleCtrl.GetChargeElementDamageUpRate(status.attackInfo.attackType, ref num))
        damage_details.MulElementOnly(num);
      if (attacker.thsCtrl.oracleCtrl.GetConcussionEnemyElementDamageUpRate(this, ref num))
        damage_details.MulElementOnly(num);
    }
    if (attacker.GetOracleOHSElementDamageUpRate(status.attackInfo, ref num))
      damage_details.MulElementOnly(num);
    if (attacker.GetOracleSpearElementDamageUpRate(status.attackInfo, ref num))
      damage_details.MulElementOnly(num);
    if (!attacker.GetOraclePairSwordsElementDamageUpRate(status.attackInfo, ref num))
      return;
    damage_details.MulElementOnly(num);
  }

  protected override AtkAttribute CalcAtk(AttackedHitStatusLocal status)
  {
    Player fromObject = status.fromObject as Player;
    if (Object.op_Equality((Object) fromObject, (Object) null))
      return base.CalcAtk(status);
    AtkAttribute atkAttribute = new AtkAttribute();
    atkAttribute.Add(status.atk);
    if (fromObject.IsTwoHandSwordSpAttacking())
      atkAttribute.normal += fromObject.GetDefForTwoHandSwordSpAttack();
    if (fromObject.IsTwoHandSwordHeatUseGauge())
    {
      ELEMENT_TYPE elementType = atkAttribute.GetElementType();
      atkAttribute.AddTargetElement(elementType, fromObject.GetElementDefForTwoHandSwordHeatCombo());
    }
    if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.HEAL_ATTACK)
    {
      atkAttribute.Mul(fromObject.healAtkRate);
    }
    else
    {
      atkAttribute.Mul(status.attackInfo.atkRate);
      if (!status.attackInfo.isSkillReference)
        atkAttribute.Mul(fromObject.pairSwordsCtrl.GetAtkRate());
      if ((status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.BURST_THS_SINGLE_SHOT || status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.BURST_THS_FULL_BURST) && fromObject.thsCtrl != null)
        atkAttribute.Mul(fromObject.thsCtrl.GetAtkRate(this, fromObject));
    }
    if (status.damageDistanceData != null)
    {
      float val = !fromObject.isBuffShadowSealing || (double) fromObject.playerParameter.arrowActionInfo.shadowSealingBuffDistanceRate == 0.0 ? status.damageDistanceData.GetRate(status.distanceXZ) : fromObject.playerParameter.arrowActionInfo.shadowSealingBuffDistanceRate;
      atkAttribute.Mul(val);
    }
    float val1 = 1f;
    if (this.IsFreeze())
      val1 = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.freezeParam.damageRate;
    else if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.TWO_HAND_SWORD_SP || status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.THS_HEAT_COMBO)
    {
      if (status.weakState != Enemy.WEAK_STATE.NONE)
        val1 = fromObject.playerParameter.specialActionInfo.twoHandSwordWeakRate;
    }
    else
    {
      switch (status.weakState)
      {
        case Enemy.WEAK_STATE.WEAK:
        case Enemy.WEAK_STATE.WEAK_SP_ATTACK:
        case Enemy.WEAK_STATE.WEAK_SP_DOWN_MAX:
          val1 = fromObject.attackWeakRate;
          break;
        case Enemy.WEAK_STATE.DOWN:
          val1 = fromObject.attackDownRate;
          break;
        case Enemy.WEAK_STATE.WEAK_ELEMENT_ATTACK:
          val1 = fromObject.elementWeakRate;
          break;
        case Enemy.WEAK_STATE.WEAK_ELEMENT_SKILL_ATTACK:
          val1 = fromObject.elementSkillWeakRate;
          break;
        case Enemy.WEAK_STATE.WEAK_SKILL_ATTACK:
          val1 = fromObject.skillWeakRate;
          break;
        case Enemy.WEAK_STATE.WEAK_HEAL_ATTACK:
          val1 = fromObject.healWeakRate;
          break;
        case Enemy.WEAK_STATE.WEAK_ELEMENT_SP_ATTACK:
          val1 = fromObject.elementSpAttackWeakRate;
          break;
      }
    }
    atkAttribute.Mul(val1);
    return atkAttribute;
  }

  protected override AtkAttribute CalcTolerance(AttackedHitStatusLocal status)
  {
    int regionId = status.regionID;
    if (regionId < 0 || regionId >= this.regionInfos.Length)
      return base.CalcTolerance(status);
    Enemy.RegionInfo regionInfo = this.regionInfos[status.regionID];
    AtkAttribute _tolerance = new AtkAttribute();
    _tolerance.Add(regionInfo.tolerance);
    _tolerance.normal -= (float) this.buffParam.GetValue(BuffParam.BUFFTYPE.DEFDOWN_RATE_NORMAL) * 0.01f;
    if ((double) _tolerance.normal < 0.0)
      _tolerance.normal = 0.0f;
    AtkAttribute val = new AtkAttribute();
    val.SetTargetElement(ELEMENT_TYPE.FIRE, (float) this.buffParam.GetValue(BuffParam.BUFFTYPE.DEFDOWN_RATE_FIRE) * 0.01f);
    val.SetTargetElement(ELEMENT_TYPE.WATER, (float) this.buffParam.GetValue(BuffParam.BUFFTYPE.DEFDOWN_RATE_WATER) * 0.01f);
    val.SetTargetElement(ELEMENT_TYPE.THUNDER, (float) this.buffParam.GetValue(BuffParam.BUFFTYPE.DEFDOWN_RATE_THUNDER) * 0.01f);
    val.SetTargetElement(ELEMENT_TYPE.SOIL, (float) this.buffParam.GetValue(BuffParam.BUFFTYPE.DEFDOWN_RATE_SOIL) * 0.01f);
    val.SetTargetElement(ELEMENT_TYPE.LIGHT, (float) this.buffParam.GetValue(BuffParam.BUFFTYPE.DEFDOWN_RATE_LIGHT) * 0.01f);
    val.SetTargetElement(ELEMENT_TYPE.DARK, (float) this.buffParam.GetValue(BuffParam.BUFFTYPE.DEFDOWN_RATE_DARK) * 0.01f);
    val.AddElementOnly((float) this.buffParam.GetValue(BuffParam.BUFFTYPE.DEFDOWN_RATE_ALLELEMENT) * 0.01f);
    _tolerance.Sub(val);
    _tolerance.CheckMinus();
    if (this.CheckApplyDefenceUpBuff(status, this.regionWorks[regionId]))
    {
      this.AddToleranceBuff(ref _tolerance);
      _tolerance.normal += (float) (this.buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_NORMAL) + this.buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_ALLELEMENT)) * 0.01f;
    }
    if (this.IsValidBarrier)
      _tolerance.AddRate(this.regionWorks[regionId].GetBarrierToleranceRate());
    return _tolerance;
  }

  private bool CheckApplyDefenceUpBuff(AttackedHitStatusLocal status, EnemyRegionWork regionWork)
  {
    bool flag = true;
    if (this.GetEnemyType() == ENEMY_TYPE.CRAB && regionWork.weakState == Enemy.WEAK_STATE.WEAK_ELEMENT_SKILL_ATTACK && (ELEMENT_TYPE) regionWork.validElementType == status.atk.GetElementType() && status.attackInfo.isSkillReference && this.IsValidBuff(BuffParam.BUFFTYPE.DEFENCE_ALLELEMENT) && this.IsValidBuff(BuffParam.BUFFTYPE.DEFENCE_NORMAL))
    {
      this.OnBuffEnd(BuffParam.BUFFTYPE.DEFENCE_ALLELEMENT, true, true);
      this.OnBuffEnd(BuffParam.BUFFTYPE.DEFENCE_NORMAL, true, true);
      flag = false;
    }
    return flag;
  }

  protected override AtkAttribute CalcDefense(AttackedHitStatusLocal status)
  {
    if (status.regionID >= 0 && status.regionID < this.regionInfos.Length)
      return this.regionInfos[status.regionID].defence;
    AtkAttribute _defence = new AtkAttribute();
    _defence.Add(this.defense);
    AtkAttribute val = new AtkAttribute();
    val.Set(1f);
    val.Add(this.buffParam.GetBuffDefenceRate());
    _defence.Mul(val);
    this.AddDefenceBuff(ref _defence);
    _defence.CheckMinus();
    return _defence;
  }

  public void CheckCounterRegion()
  {
    bool flag = this.GetEnabledCounterRegionIndex() >= 0;
    uint stringID = this.kStrIdx_EnemyReaction_BreakCounterRegion;
    switch (this.m_CounterRegionState)
    {
      case Enemy.eCounterRegionState.NONE:
        this.m_CounterRegionState = flag ? Enemy.eCounterRegionState.EXIST : Enemy.eCounterRegionState.NOT_EXIST;
        return;
      case Enemy.eCounterRegionState.EXIST:
        if (flag)
          return;
        this.m_CounterRegionState = Enemy.eCounterRegionState.NOT_EXIST;
        stringID = this.kStrIdx_EnemyReaction_BreakCounterRegion;
        break;
      case Enemy.eCounterRegionState.NOT_EXIST:
        if (!flag)
          return;
        this.m_CounterRegionState = Enemy.eCounterRegionState.EXIST;
        stringID = this.kStrIdx_EnemyReaction_ReviveCounterRegion;
        break;
    }
    if (!MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
      return;
    MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestAnnounce("", STRING_CATEGORY.ENEMY_REACTION, stringID);
  }

  protected bool isAvailableCounter(Character.ACTION_ID currentId)
  {
    return currentId != Character.ACTION_ID.FREEZE && currentId != Character.ACTION_ID.PARALYZE && currentId != (Character.ACTION_ID.ATTACK | Character.ACTION_ID.FREEZE) && currentId != (Character.ACTION_ID) 17 && currentId != (Character.ACTION_ID) 18 && currentId != (Character.ACTION_ID) 20 && currentId != (Character.ACTION_ID) 23 && !this.IsDebuffShadowSealing() && !this.IsLightRing() && !this.IsConcussion() && currentId != Character.ACTION_ID.NONE;
  }

  protected bool CheckCounter(AttackedHitStatusOwner status)
  {
    if (!this.isBoss || !this.isAvailableCounter(this.actionID) || this.GetEnabledCounterRegion() == null)
      return false;
    EnemyController controller = this.controller as EnemyController;
    if (Object.op_Equality((Object) controller, (Object) null))
      return false;
    EnemyBrain brain = controller.brain as EnemyBrain;
    if (Object.op_Equality((Object) brain, (Object) null))
      return false;
    int counterAttackId = brain.actionCtrl.GetCounterAttackId();
    return counterAttackId != int.MaxValue && (long) (uint) this.attackID != (long) counterAttackId;
  }

  public override void OnAttackedHitOwner(AttackedHitStatusOwner status)
  {
    this.ApplyInvicibleCount(status);
    bool flag1 = this.ApplyInvicibleBadStatus(status);
    if (this.IsValidBuff(BuffParam.BUFFTYPE.MAD_MODE))
      status.badStatusAdd.Mul(MonoBehaviourSingleton<InGameSettingsManager>.I.madModeParam.badStatusRate);
    status.aegisParam.isChange = false;
    if (!this.isDead && status.validDamage)
    {
      if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.CANNON_BALL)
      {
        status.damage = 0;
        status.damageDetails.Set(0.0f);
      }
      int shieldHp = (int) this.ShieldHp;
      int grabHp = (int) this.GrabHp;
      status.afterGrabHp = (int) this.GrabHp;
      status.afterBarrierHp = (int) this.BarrierHp;
      status.downTotal = this.downTotal;
      status.concussionTotal = this.concussionTotal;
      if (status.regionID >= 0 && status.regionID < this.regionWorks.Length)
        status.afterRegionHP = (int) this.regionWorks[status.regionID].hp;
      if (status.attackInfo.isSkillReference)
      {
        if ((double) status.badStatusAdd.electricShock > 0.0 && this.GetElementType() == ELEMENT_TYPE.WATER)
        {
          Player fromObject = status.fromObject as Player;
          if (Object.op_Inequality((Object) fromObject, (Object) null))
            this.OnElectricShockStart(status.attackInfo, fromObject);
        }
        if ((double) status.badStatusAdd.soilShock > 0.0 && this.GetElementType() == ELEMENT_TYPE.THUNDER)
        {
          Player fromObject = status.fromObject as Player;
          if (Object.op_Inequality((Object) fromObject, (Object) null))
            this.OnSoilShockStart(status.attackInfo, fromObject);
        }
        if (status.attackInfo.buffIDs != null && status.attackInfo.buffIDs.Length != 0)
          this.ApplyBuffsByTable(status);
        if (this.CheckCounter(status))
        {
          status.reactionType = 13;
          if (MonoBehaviourSingleton<CoopManager>.IsValid())
            MonoBehaviourSingleton<CoopManager>.I.coopStage.battleUserLog.Add((Character) this, status);
          base.OnAttackedHitOwner(status);
          return;
        }
      }
      bool flag2 = false;
      if (this.aegisCtrl != null)
      {
        flag2 = this.aegisCtrl.IsValid();
        this.aegisCtrl.FlagReset();
      }
      if (!flag2 && this.CheckMadMode(status))
      {
        status.reactionType = 18;
        status.badStatusAdd.Reset();
        this.downTotal = 0.0f;
        status.downAddBase = 0.0f;
        status.downAddWeak = 0.0f;
        this.ResetConcussion();
        status.concussionTotal = 0.0f;
        status.concussionAdd = 0.0f;
        status.isArrowBleed = false;
        status.isShadowSealing = false;
        status.isArrowBomb = false;
      }
      if (MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.shadowSealingParam.isReactionDamage && status.isShadowSealing && !this.IsDebuffShadowSealing())
        status.reactionType = 1;
      if (status.regionID >= 0 && status.regionID < this.regionWorks.Length)
      {
        EnemyRegionWork regionWork = this.regionWorks[status.regionID];
        Enemy.RegionInfo regionInfo = this.regionInfos[status.regionID];
        int customDownRate = regionInfo.customDownRate;
        float num1 = this.CalcRegionDamageRate(status);
        int damage = (int) ((double) status.damage * (double) num1);
        if (flag2)
        {
          if (this.aegisCtrl.Damage(damage))
            status.aegisParam.Copy(this.aegisCtrl.syncParam);
        }
        else
        {
          status.afterRegionHP = (int) regionWork.hp - damage;
          if (status.afterRegionHP < 0)
            status.afterRegionHP = 0;
        }
        if (status.regionID != 0 && (int) regionWork.hp > 0 && status.afterRegionHP <= 0)
        {
          status.breakRegion = true;
          if (!this.IsDebuffShadowSealing() && !this.IsConcussion() && status.reactionType != 18)
          {
            if (regionInfo.breakInDown)
              status.reactionType = 7;
            else if (regionInfo.breakInDamage)
              status.reactionType = 1;
          }
        }
        if (this.IsValidBarrier)
        {
          int num2 = this.CalcBarrierDamage(regionWork, status);
          status.afterBarrierHp = (int) this.BarrierHp - num2;
          if (status.afterBarrierHp < 0)
            status.afterBarrierHp = 0;
        }
        if (regionInfo.isEnableShieldDamage && status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.CANNON_BALL)
        {
          bool isElementCritical = Enemy.GetEffectiveType(status.attackInfo.elementType, this.GetElementType()) == Enemy.EFFECTIVE_TYPE.GOOD;
          status.shieldDamage = this.CalcShieldDamage(regionWork.isShieldCriticalDamage, isElementCritical, status.attackInfo);
          if (regionWork.isShieldCriticalDamage && this.IsValidGrabHp)
          {
            status.afterGrabHp = (int) this.GrabHp - (int) this.GrabCannonDamage;
            if (status.afterGrabHp < 0)
              status.afterGrabHp = 0;
          }
        }
        bool flag3 = false;
        if ((int) regionWork.hp <= 0 && regionInfo.maxHP > 0)
          flag3 = true;
        if (status.breakRegion | flag3 && !regionInfo.breakAfterHit)
          status.isArrowBleed = false;
        if (status.isArrowBleed)
        {
          float bleedTimeInterval = MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.arrowBleedTimeInterval;
          float num3 = 1f - MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.arrowBleedSkipTimeRate;
          float num4 = this.bleedCounter % bleedTimeInterval;
          status.arrowBleedSkipFirst = (double) num4 >= (double) bleedTimeInterval * (double) num3;
        }
        if (flag3 && !regionInfo.breakAfterHit || status.breakRegion)
          status.isShadowSealing = false;
        if (Enemy.IsWeakStateCheckAlreadyHit(status.weakState) && (double) status.downAddWeak > 0.0 && regionWork.weakAttackIDs.Count > 0)
          status.downAddWeak = 0.0f;
      }
      if (this.actionID == (Character.ACTION_ID.ATTACK | Character.ACTION_ID.FREEZE))
      {
        if (!status.isForceDown)
          status.downAddBase = status.downAddWeak = 0.0f;
      }
      else
      {
        float num = this.CalcWorkDownTotal(status.downAddBase, status.downAddWeak, status.regionID);
        status.downTotal = this.downTotal + num;
        if ((double) status.downTotal >= (double) this.downMax)
        {
          status.reactionType = 7;
          status.downTotal = (float) this.downMax;
        }
        if (this.actionID != (Character.ACTION_ID) 25 && (double) status.concussionAdd > 0.0)
        {
          status.concussionTotal = this.concussionTotal + status.concussionAdd;
          if ((double) status.concussionTotal >= (double) this.concussionMax)
          {
            status.reactionType = 24;
            status.concussionTotal = this.concussionMax;
          }
        }
      }
      if (this.actionID != (Character.ACTION_ID) 18 && (int) this.ShieldHp - status.shieldDamage <= 0 && shieldHp > 0 && (int) this.ShieldHpMax > 0)
        status.reactionType = 16 /*0x10*/;
      if (status.afterGrabHp <= 0 && grabHp > 0 && (int) this.GrabHpMax > 0)
        this.ActReleaseGrabbedPlayers(false, false, true);
      if (MonoBehaviourSingleton<CoopManager>.IsValid())
        MonoBehaviourSingleton<CoopManager>.I.coopStage.battleUserLog.Add((Character) this, status);
    }
    if (flag1)
      this.buffParam.DecreaseInvincibleBadStatus();
    base.OnAttackedHitOwner(status);
  }

  private void ApplyBuffsByTable(AttackedHitStatusOwner status)
  {
    if (!Singleton<BuffTable>.IsValid())
      return;
    foreach (uint buffId in status.attackInfo.buffIDs)
    {
      if (buffId > 0U)
      {
        BuffTable.BuffData data = Singleton<BuffTable>.I.GetData(buffId);
        if (data != null)
        {
          BuffParam.BuffData buffData = new BuffParam.BuffData();
          buffData.type = data.type;
          buffData.interval = data.interval;
          buffData.valueType = data.valueType;
          buffData.time = data.duration;
          buffData.fromObjectID = status.fromObjectID;
          float num = (float) data.value;
          if (status.skillParam != null && status.skillParam.baseInfo != null)
          {
            GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(data.growID, status.skillParam.baseInfo.level, status.skillParam.baseInfo.exceedCnt);
            if (growSkillItemData != null)
            {
              buffData.time = (float) ((double) data.duration * (double) (int) growSkillItemData.supprtTime[0].rate * 0.0099999997764825821) + (float) growSkillItemData.supprtTime[0].add;
              num = (float) (data.value * (int) growSkillItemData.supprtValue[0].rate) * 0.01f + (float) (int) growSkillItemData.supprtValue[0].add;
            }
            num *= 1f + status.skillParam.GetSupportValueTotalAsRateByType(BuffParam.BUFFTYPE.TO_ENEMY_DEBUFF_VALUE_UP);
          }
          if (buffData.valueType == BuffParam.VALUE_TYPE.RATE && BuffParam.IsTypeValueBasedOnHP(buffData.type))
            num = (float) ((double) this.hpMax * (double) num * 0.0099999997764825821);
          buffData.value = Mathf.FloorToInt(num);
          switch (buffData.type)
          {
            case BuffParam.BUFFTYPE.MOVE_SPEED_DOWN:
            case BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN:
            case BuffParam.BUFFTYPE.SOIL_SHOCK:
              if (this.GetElementType() == ELEMENT_TYPE.THUNDER)
              {
                this.OnBuffStart(buffData);
                continue;
              }
              continue;
            case BuffParam.BUFFTYPE.BURNING:
              if (this.GetElementType() == ELEMENT_TYPE.SOIL)
              {
                this.OnBuffStart(buffData);
                continue;
              }
              continue;
            case BuffParam.BUFFTYPE.ELECTRIC_SHOCK:
              if (this.GetElementType() == ELEMENT_TYPE.WATER)
              {
                this.OnBuffStart(buffData);
                continue;
              }
              continue;
            case BuffParam.BUFFTYPE.EROSION:
              if (this.GetElementType() == ELEMENT_TYPE.LIGHT)
              {
                this.OnBuffStart(buffData);
                continue;
              }
              continue;
            case BuffParam.BUFFTYPE.ACID:
              if (this.GetElementType() == ELEMENT_TYPE.FIRE)
              {
                this.OnBuffStart(buffData);
                continue;
              }
              continue;
            case BuffParam.BUFFTYPE.CORRUPTION:
              if (this.GetElementType() == ELEMENT_TYPE.THUNDER)
              {
                this.OnBuffStart(buffData);
                continue;
              }
              continue;
            case BuffParam.BUFFTYPE.STIGMATA:
              if (this.GetElementType() == ELEMENT_TYPE.DARK)
              {
                this.OnBuffStart(buffData);
                continue;
              }
              continue;
            case BuffParam.BUFFTYPE.CYCLONIC_THUNDERSTORM:
              if (this.GetElementType() == ELEMENT_TYPE.WATER)
              {
                this.OnBuffStart(buffData);
                continue;
              }
              continue;
            default:
              this.OnBuffStart(buffData);
              continue;
          }
        }
      }
    }
  }

  private void ResetElementDebuff(ELEMENT_TYPE prev, ELEMENT_TYPE now)
  {
    if (prev == now)
      return;
    switch (prev)
    {
      case ELEMENT_TYPE.FIRE:
        this.OnBuffEnd(BuffParam.BUFFTYPE.ACID, true, true);
        break;
      case ELEMENT_TYPE.WATER:
        this.OnBuffEnd(BuffParam.BUFFTYPE.ELECTRIC_SHOCK, true, true);
        this.OnBuffEnd(BuffParam.BUFFTYPE.CYCLONIC_THUNDERSTORM, true, true);
        break;
      case ELEMENT_TYPE.THUNDER:
        this.OnBuffEnd(BuffParam.BUFFTYPE.MOVE_SPEED_DOWN, true, true);
        this.OnBuffEnd(BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN, true, true);
        this.OnBuffEnd(BuffParam.BUFFTYPE.SOIL_SHOCK, true, true);
        this.OnBuffEnd(BuffParam.BUFFTYPE.CORRUPTION, true, true);
        break;
      case ELEMENT_TYPE.SOIL:
        this.OnBuffEnd(BuffParam.BUFFTYPE.BURNING, true, true);
        break;
      case ELEMENT_TYPE.LIGHT:
        this.OnBuffEnd(BuffParam.BUFFTYPE.EROSION, true, true);
        break;
      case ELEMENT_TYPE.DARK:
        this.OnBuffEnd(BuffParam.BUFFTYPE.LIGHT_RING, true, true);
        this.OnBuffEnd(BuffParam.BUFFTYPE.STIGMATA, true, true);
        break;
    }
  }

  private float CalcRegionDamageRate(AttackedHitStatusOwner status)
  {
    if (!(status.fromObject is Player fromObject))
      return 1f;
    Enemy.RegionInfo regionInfo = (Enemy.RegionInfo) null;
    if (this.regionInfos != null && status.regionID < this.regionInfos.Length)
      regionInfo = this.regionInfos[status.regionID];
    float num = 1f;
    if (regionInfo != null)
      num = fromObject.GetRegionDamageRate(regionInfo.dragonArmorInfo.enabled);
    AttackHitInfo.ToEnemy.DamageToRegionInfo damageToRegionInfo = status.attackInfo.toEnemy.damageToRegionInfo;
    if (!damageToRegionInfo.isDamageUp || damageToRegionInfo.damageUpPercent <= 0)
      return num;
    int base_value = damageToRegionInfo.damageUpPercent;
    if (status.attackInfo.isSkillReference)
    {
      SkillInfo.SkillParam skillParam = status.skillParam;
      if (skillParam != null)
      {
        GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(skillParam.tableData.growID, skillParam.baseInfo.level, skillParam.baseInfo.exceedCnt);
        if (growSkillItemData != null)
          base_value = growSkillItemData.GetGrowResultSupportValue(base_value, 0);
      }
    }
    return num + (float) base_value * 0.01f;
  }

  private int CalcBarrierDamage(EnemyRegionWork regionWork, AttackedHitStatusOwner status)
  {
    int num = 0;
    AtkAttribute atkBarrierDamage = regionWork.regionInfo.atkBarrierDamage;
    Player fromObject = status.fromObject as Player;
    if (Object.op_Inequality((Object) fromObject, (Object) null) && fromObject.buffParam.IsValidBuff(BuffParam.BUFFTYPE.LUNATIC_TEAR))
      num = regionWork.regionInfo.barrierDamageSp;
    AtkAttribute damageDetails = status.damageDetails;
    if ((double) damageDetails.normal > 0.0)
      num += (int) atkBarrierDamage.normal;
    if ((double) damageDetails.fire > 0.0)
      num += (int) atkBarrierDamage.fire;
    if ((double) damageDetails.water > 0.0)
      num += (int) atkBarrierDamage.water;
    if ((double) damageDetails.thunder > 0.0)
      num += (int) atkBarrierDamage.thunder;
    if ((double) damageDetails.soil > 0.0)
      num += (int) atkBarrierDamage.soil;
    if ((double) damageDetails.light > 0.0)
      num += (int) atkBarrierDamage.light;
    if ((double) damageDetails.dark > 0.0)
      num += (int) atkBarrierDamage.dark;
    return Mathf.FloorToInt((float) num * status.attackInfo.toEnemy.damageToRegionInfo.barrierDamageRate);
  }

  public override bool IsEnableAttackedHitOwner()
  {
    return (!MonoBehaviourSingleton<CoopManager>.IsValid() || !MonoBehaviourSingleton<InGameProgress>.IsValid() || !MonoBehaviourSingleton<CoopManager>.I.coopRoom.isOwnerFirstClear || !Object.op_Equality((Object) MonoBehaviourSingleton<StageObjectManager>.I.boss, (Object) this) || !MonoBehaviourSingleton<InGameProgress>.I.isEnding) && this.actionID != (Character.ACTION_ID) 24 && base.IsEnableAttackedHitOwner();
  }

  protected override bool IsHitReactionValid(AttackedHitStatusOwner status)
  {
    return this.actionID != (Character.ACTION_ID.ATTACK | Character.ACTION_ID.FREEZE) && !this.IsDebuffShadowSealing() && status.fromType != StageObject.OBJECT_TYPE.ENEMY && base.IsHitReactionValid(status);
  }

  protected override bool IsReactionDelayType(int type)
  {
    switch ((Character.REACTION_TYPE) type)
    {
      case Character.REACTION_TYPE.DOWN:
      case Character.REACTION_TYPE.ELECTRIC_SHOCK:
      case Character.REACTION_TYPE.SOIL_SHOCK:
        return true;
      case Character.REACTION_TYPE.COUNTER:
        return true;
      case Character.REACTION_TYPE.DIZZY:
        return true;
      case Character.REACTION_TYPE.SHADOWSEALING:
        return true;
      case Character.REACTION_TYPE.MAD_MODE:
        return true;
      case Character.REACTION_TYPE.BIND:
        return true;
      default:
        return base.IsReactionDelayType(type);
    }
  }

  protected override Character.REACTION_TYPE OnHitReaction(AttackedHitStatusOwner status)
  {
    AttackHitInfo.ToEnemy.REACTION_TYPE reactionType = status.attackInfo.toEnemy.reactionType;
    if (this.isBoss || this.isWaveMatchBoss || this.isBigMonster || this.IsFieldEnemyBoss())
    {
      switch (reactionType)
      {
        case AttackHitInfo.ToEnemy.REACTION_TYPE.NONE:
          if (status.weakState == Enemy.WEAK_STATE.WEAK && status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.CANNON_BALL_DIRECT && this.actionID != Character.ACTION_ID.DAMAGE && this.actionID != (Character.ACTION_ID.ATTACK | Character.ACTION_ID.FREEZE) || status.weakState == Enemy.WEAK_STATE.WEAK && status.attackInfo.toEnemy.isWeakHitReaction && this.actionID != Character.ACTION_ID.DAMAGE && this.actionID != (Character.ACTION_ID.ATTACK | Character.ACTION_ID.FREEZE) || (Enemy.IsWeakStateSpAttack(status.weakState) || Enemy.IsWeakStateElementAttack(status.weakState) || Enemy.IsWeakStateSkillAttack(status.weakState) || Enemy.IsWeakStateHealAttack(status.weakState)) && this.actionID != Character.ACTION_ID.DAMAGE && this.actionID != (Character.ACTION_ID.ATTACK | Character.ACTION_ID.FREEZE))
            return Character.REACTION_TYPE.DAMAGE;
          break;
        case AttackHitInfo.ToEnemy.REACTION_TYPE.DAMAGE:
          return Character.REACTION_TYPE.DAMAGE;
        case AttackHitInfo.ToEnemy.REACTION_TYPE.DOWN:
          return Character.REACTION_TYPE.DOWN;
        case AttackHitInfo.ToEnemy.REACTION_TYPE.BIND:
          return Character.REACTION_TYPE.BIND;
      }
    }
    return Character.REACTION_TYPE.NONE;
  }

  protected override Character.REACTION_TYPE CheckReActionTolerance(AttackedHitStatusOwner status)
  {
    Character.REACTION_TYPE reactionType = base.CheckReActionTolerance(status);
    if (this.actionID == (Character.ACTION_ID) 25)
    {
      switch ((Character.REACTION_TYPE) status.reactionType)
      {
        case Character.REACTION_TYPE.DAMAGE:
        case Character.REACTION_TYPE.DOWN:
        case Character.REACTION_TYPE.PARALYZE:
        case Character.REACTION_TYPE.FREEZE:
        case Character.REACTION_TYPE.LIGHT_RING:
          reactionType = Character.REACTION_TYPE.NONE;
          break;
      }
    }
    return reactionType;
  }

  public override void OnHitAttack(
    AttackHitInfo info,
    AttackHitColliderProcessor.HitParam hit_param)
  {
    EnemyController controller = this.controller as EnemyController;
    if (Object.op_Inequality((Object) controller, (Object) null))
      controller.OnHitAttack(hit_param.toObject);
    GrabInfo grabInfo = info.grabInfo;
    if (grabInfo.enable)
    {
      Player toObject = hit_param.toObject as Player;
      if (Object.op_Inequality((Object) toObject, (Object) null))
      {
        EnemyBrain brain = controller.brain as EnemyBrain;
        if (Object.op_Inequality((Object) brain, (Object) null))
        {
          DrainAttackInfo _drainAtkInfo = this.SearchDrainAttackInfo(grabInfo.drainAttackId);
          brain.actionCtrl.grabController.Grab(toObject, grabInfo, _drainAtkInfo);
          if (_drainAtkInfo != null)
            this.grabDrainRecoverTimer = _drainAtkInfo.recoverInterval;
        }
      }
    }
    if (this.isAbleToSkipAction)
    {
      this.SetNextTrigger();
      this.isAbleToSkipAction = false;
    }
    base.OnHitAttack(info, hit_param);
  }

  public float CalcWorkDownTotal(float downAddBase, float downAddWeak, int regionID)
  {
    float num1 = downAddBase + downAddWeak;
    int num2 = 0;
    if (regionID >= 0 && regionID < this.regionWorks.Length)
      num2 = this.regionInfos[regionID].customDownRate;
    if (num2 != 0)
    {
      float num3 = (float) num2 * 0.01f;
      num1 += num1 * num3;
      if ((double) num1 < 0.0)
        num1 = 0.0f;
    }
    return num1;
  }

  public override void OnAttackedHitFix(AttackedHitStatusFix status)
  {
    Player fromObject = status.fromObject as Player;
    bool isDead = this.isDead;
    int hp = this.hp;
    int shieldHp = (int) this.ShieldHp;
    if (!this.isDead)
    {
      this.concussionTotal = status.concussionTotal;
      if ((double) status.concussionAdd > 0.0)
      {
        float concussionExtend = fromObject.buffParam.GetConcussionExtend();
        if ((double) this.concussionExtend < (double) concussionExtend)
          this.concussionExtend = concussionExtend;
        if (!this.concussionAddPlayerIdList.Contains(status.fromObjectID))
          this.concussionAddPlayerIdList.Add(status.fromObjectID);
        if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
          MonoBehaviourSingleton<UIEnemyStatus>.I.DirectionConcussionGauge(status.hitPos);
      }
    }
    base.OnAttackedHitFix(status);
    if (!isDead)
    {
      this.downHealInterval = 1f;
      this.downTotal = status.downTotal;
      if ((double) status.downAddBase + (double) status.downAddWeak > 0.0 && MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
        MonoBehaviourSingleton<UIEnemyStatus>.I.DirectionDownGauge(status);
    }
    this.BarrierHp = (XorInt) status.afterBarrierHp;
    this.GrabHp = (XorInt) status.afterGrabHp;
    if (this.aegisCtrl != null)
      this.aegisCtrl.Sync(status.aegisParam);
    if ((int) this.ShieldHp <= 0 && shieldHp > 0)
    {
      if (MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
        MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestAnnounce(this.enemyTableData.name, STRING_CATEGORY.ENEMY_SHIELD, 1U);
      EffectManager.GetEffect("ef_btl_goldbird_aura_01_01", this._transform);
      this.ResetShieldShaderParam();
      if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
      {
        List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
        for (int index = 0; index < playerList.Count; ++index)
        {
          Self self = playerList[index] as Self;
          if (Object.op_Inequality((Object) self, (Object) null))
          {
            self.CancelCannonMode();
            self.ActIdle(false, -1f);
          }
          else
          {
            Player player = playerList[index] as Player;
            if (Object.op_Inequality((Object) player, (Object) null))
            {
              player.CancelCannonMode();
              player.ActIdle(false, -1f);
            }
          }
        }
      }
    }
    if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.BOOST_ARROW_RAIN)
    {
      int rainBoostBombLevel = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.arrowRainBoostBombLevel;
      Vector3 vector3;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3).\u002Ector(0.0f, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.arrowRainBoostBombOffsetY, 0.0f);
      if (Object.op_Inequality((Object) fromObject, (Object) null))
      {
        AtkAttribute atk = new AtkAttribute();
        fromObject.GetAtk(status.attackInfo, ref atk, (SkillInfo.SkillParam) null);
        this.StartCoroutine(this.FireBombArrow(fromObject, atk, rainBoostBombLevel, Vector3.op_Addition(status.hitPos, vector3), true));
      }
    }
    if (status.regionID >= 0 && status.regionID < this.regionWorks.Length)
    {
      EnemyRegionWork regionWork = this.regionWorks[status.regionID];
      Enemy.RegionInfo region_info = this.regionInfos[status.regionID];
      regionWork.hp = (XorInt) status.afterRegionHP;
      if (!isDead)
      {
        if (Enemy.IsWeakStateCheckAlreadyHit(status.weakState) && !regionWork.weakAttackIDs.Contains(status.fromObjectID))
          regionWork.weakAttackIDs.Add(status.fromObjectID);
        switch (status.weakState)
        {
          case Enemy.WEAK_STATE.WEAK:
            this.OnHitWeakPoint(regionWork.deleteAtkName, false);
            break;
          case Enemy.WEAK_STATE.WEAK_SP_ATTACK:
          case Enemy.WEAK_STATE.WEAK_SP_DOWN_MAX:
            if (status.IsSpAttackHit)
            {
              this.OnHitWeakPoint(regionWork.deleteAtkName, true);
              break;
            }
            break;
          case Enemy.WEAK_STATE.WEAK_ELEMENT_ATTACK:
            if ((ELEMENT_TYPE) regionWork.validElementType == status.damageDetails.GetElementType())
            {
              this.OnHitWeakPoint(regionWork.deleteAtkName, true);
              break;
            }
            break;
          case Enemy.WEAK_STATE.WEAK_ELEMENT_SKILL_ATTACK:
            if (status.attackInfo.isSkillReference && (ELEMENT_TYPE) regionWork.validElementType == status.attackInfo.atk.GetElementType())
            {
              this.OnHitWeakPoint(regionWork.deleteAtkName, true);
              break;
            }
            break;
          case Enemy.WEAK_STATE.WEAK_SKILL_ATTACK:
            if (status.attackInfo.isSkillReference)
            {
              this.OnHitWeakPoint(regionWork.deleteAtkName, true);
              break;
            }
            break;
          case Enemy.WEAK_STATE.WEAK_HEAL_ATTACK:
            if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.HEAL_ATTACK)
            {
              this.OnHitWeakPoint(regionWork.deleteAtkName, true);
              break;
            }
            break;
          case Enemy.WEAK_STATE.WEAK_ELEMENT_SP_ATTACK:
            if ((ELEMENT_TYPE) regionWork.validElementType == status.damageDetails.GetElementType() && status.IsSpAttackHit)
            {
              this.OnHitWeakPoint(regionWork.deleteAtkName, true);
              break;
            }
            break;
        }
        if (Enemy.IsWeakStateDisplaySign(status.weakState))
          regionWork.displayTimer = 0.0f;
        this._CheckHitFixArrow(status, regionWork);
      }
      if (status.breakRegion)
      {
        this.ActReleaseGrabbedPlayers(false, false, true);
        bool isBroke = regionWork.isBroke;
        regionWork.isBroke = true;
        regionWork.breakTime = Time.time;
        foreach (string deactivateObject in region_info.deactivateObjects)
        {
          Transform node = this.FindNode(deactivateObject);
          if (Object.op_Inequality((Object) node, (Object) null))
            ((Component) node).gameObject.SetActive(false);
        }
        int index1 = 0;
        for (int length = this.regionWorks.Length; index1 < length; ++index1)
          this.regionWorks[index1].OnBreakRegion(status.regionID);
        if (!region_info.breakAfterHit)
        {
          int index2 = 0;
          for (int count = regionWork.bleedWorkList.Count; index2 < count; ++index2)
          {
            Enemy.BleedWork bleedWork = regionWork.bleedWorkList[index2];
            if (Object.op_Inequality((Object) bleedWork.bleedEffect, (Object) null))
            {
              EffectManager.ReleaseEffect(((Component) bleedWork.bleedEffect).gameObject);
              bleedWork.bleedEffect = (Transform) null;
            }
          }
          regionWork.bleedWorkList.Clear();
          regionWork.bleedList.Clear();
        }
        regionWork.shadowSealingData.ownerID = 0;
        regionWork.shadowSealingData.existSec = 0.0f;
        regionWork.shadowSealingData.extendRate = 1f;
        if (regionWork.shadowSealingEffect != null)
        {
          EffectManager.ReleaseEffect(((Component) regionWork.shadowSealingEffect).gameObject, false, true);
          regionWork.shadowSealingEffect = (Transform) null;
        }
        if (MonoBehaviourSingleton<TargetMarkerManager>.IsValid())
          MonoBehaviourSingleton<TargetMarkerManager>.I.updateShadowSealingFlag = true;
        if (region_info.breakEffect != null && !string.IsNullOrEmpty(region_info.breakEffect.effectName))
          this.PlayRegionBreakEffect(region_info);
        if (this.enemyReward != null && !isBroke)
          this.enemyReward.reward.ForEach((Action<QuestStartData.RegionDropItem>) (item =>
          {
            if (item.regionId != status.regionID || item.breakReward.Count == 0)
              return;
            item.breakReward.ForEach((Action<QuestStartData.BreakItem>) (breakItem => this.CreateDropItemFromRegionBreak(region_info, (StageObject) MonoBehaviourSingleton<StageObjectManager>.I.self, breakItem.rarity)));
          }));
        if (Object.op_Inequality((Object) fromObject, (Object) null) && MonoBehaviourSingleton<UIPlayerAnnounce>.IsValid())
        {
          if (fromObject is Self)
          {
            if (region_info.dragonArmorInfo.enabled)
              MonoBehaviourSingleton<UIInGameSelfAnnounceManager>.I.PlayDragonArmorBreak();
            else
              MonoBehaviourSingleton<UIInGameSelfAnnounceManager>.I.PlayRegionBreak();
            SoundManager.PlayOneshotJingle(40000156);
          }
          else if (region_info.dragonArmorInfo.enabled)
            MonoBehaviourSingleton<UIPlayerAnnounce>.I.Announce(UIPlayerAnnounce.ANNOUNCE_TYPE.DRAGON_ARMOR, fromObject);
          else
            MonoBehaviourSingleton<UIPlayerAnnounce>.I.Announce(UIPlayerAnnounce.ANNOUNCE_TYPE.REGION, fromObject);
        }
        this.ShotRegionBreakBullet(region_info.breakBullet);
        this.OnUpdateBombArrow(status.regionID);
        this.UpdateBreakIDLists();
      }
    }
    if (this.enemyReward != null)
    {
      int index = 0;
      for (int count = this.enemyReward.drop.hpRate.Count; index < count; ++index)
      {
        if ((double) this.enemyReward.drop.hpRate[index] >= (double) this.damageHpRate)
        {
          if ((double) this.enemyReward.drop.hpRate[index] <= (double) status.damageHpRate)
            this.CreateDropItem(status.hitPos, (StageObject) MonoBehaviourSingleton<StageObjectManager>.I.self, this.enemyReward.drop.rarity[index], false);
          else
            break;
        }
      }
    }
    this.damageHpRate = status.damageHpRate;
    if (fromObject != null && MonoBehaviourSingleton<UIPlayerAnnounce>.IsValid())
    {
      if (Enemy.IsWeakStateCheckAlreadyHit(status.weakState))
        MonoBehaviourSingleton<UIPlayerAnnounce>.I.Announce(UIPlayerAnnounce.ANNOUNCE_TYPE.WEAK, fromObject);
      if (status.reactionType == 7)
        MonoBehaviourSingleton<UIPlayerAnnounce>.I.Announce(UIPlayerAnnounce.ANNOUNCE_TYPE.DOWN, fromObject);
    }
    if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
    {
      int damage = status.damage;
      int num1 = hp - this.hp;
      if (num1 < 0)
        num1 = 0;
      if (damage > num1)
        damage = num1;
      MonoBehaviourSingleton<InGameRecorder>.I.RecordGivenDamage(status.fromObjectID, damage);
      if (QuestManager.IsValidInGameExplore() && this.isBoss && MonoBehaviourSingleton<CoopManager>.I.coopMyClient.clientId == status.fromClientID)
      {
        ExplorePlayerStatus explorePlayerStatus = MonoBehaviourSingleton<QuestManager>.I.GetMyExplorePlayerStatus();
        int num2 = explorePlayerStatus.givenTotalDamage + damage;
        explorePlayerStatus.SyncTotalDamageToBoss(num2);
        MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendExploreBossDamage(num2);
      }
    }
    if (this.hp <= 0 && Object.op_Inequality((Object) fromObject, (Object) null))
    {
      if (fromObject is Self)
        MonoBehaviourSingleton<InGameProgress>.I.AddDefeatCount(this.isWaveMatchBoss);
      if (this.isWaveMatchBoss)
        MonoBehaviourSingleton<InGameProgress>.I.AddPartyDefeatBossCount();
      MonoBehaviourSingleton<InGameProgress>.I.AddPartyDefeatCount();
    }
    if (!this.enableToSkipActionByDamage)
      return;
    this.enableToSkipActionByDamage = false;
    this.SetNextTrigger();
  }

  protected override void MakeReactionInfo(
    AttackedHitStatusFix status,
    out Character.ReactionInfo reactionInfo)
  {
    reactionInfo = new Character.ReactionInfo();
    reactionInfo.reactionType = (Character.REACTION_TYPE) status.reactionType;
    reactionInfo.targetId = status.fromObjectID;
    switch (reactionInfo.reactionType)
    {
      case Character.REACTION_TYPE.BIND:
        reactionInfo.loopTime = status.attackInfo.toEnemy.reactionInfo.reactionLoopTime;
        break;
      case Character.REACTION_TYPE.DEAD_REVIVE:
        reactionInfo.deadReviveCount = status.deadReviveCount;
        break;
    }
  }

  private void _CheckHitFixArrow(AttackedHitStatusFix status, EnemyRegionWork region_work)
  {
    if (this.isDead)
      return;
    TargetPoint targetPoint = (TargetPoint) null;
    int index1 = 0;
    for (int length = this.targetPoints.Length; index1 < length; ++index1)
    {
      if (this.targetPoints[index1].regionID == status.regionID && this.targetPoints[index1].isAimEnable)
      {
        targetPoint = this.targetPoints[index1];
        break;
      }
    }
    if (status.isShadowSealing)
    {
      if (this.IsDebuffShadowSealing())
        return;
      Enemy.ShadowSealingData shadowSealingData = region_work.shadowSealingData;
      if (shadowSealingData.ownerID == 0)
      {
        shadowSealingData.ownerID = status.fromObjectID;
        shadowSealingData.existSec = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.shadowSealingExistSec * this.badStatusMax.shadowSealing;
        if (status.fromObject is Player fromObject)
        {
          shadowSealingData.extendRate = fromObject.buffParam.GetShadowSealingExtend();
          shadowSealingData.existSec *= fromObject.buffParam.GetShadowSealingExtendArrow();
        }
        else
          shadowSealingData.extendRate = 1f;
        if ((double) shadowSealingData.existSec <= (double) MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.shadowSealingExistMinSec)
          shadowSealingData.existSec = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.shadowSealingExistMinSec;
        if (targetPoint != null && region_work.shadowSealingEffect == null)
        {
          region_work.shadowSealingEffect = targetPoint.PlayArrowBleedEffect(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.shadowSealingEffectName, 0);
          if (region_work.shadowSealingEffect != null)
            ((Component) region_work.shadowSealingEffect).GetComponent<EffectSizeCtrl>()?.Work(shadowSealingData.existSec);
        }
        if ((this.IsMirror() || this.IsPuppet()) && !this.IsValidWaitingPacket(StageObject.WAITING_PACKET.ENEMY_UPDATE_SHADOWSEALING))
          this.StartWaitingPacket(StageObject.WAITING_PACKET.ENEMY_UPDATE_SHADOWSEALING, false, shadowSealingData.existSec);
      }
      if (this._CheckShadowSealingFullStuck())
      {
        this.ActDebuffShadowSealingStart();
      }
      else
      {
        if (!MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
          return;
        MonoBehaviourSingleton<UIEnemyStatus>.I.DirectionShadowSealingGauge(status.hitPos);
      }
    }
    else if (status.isArrowBleed)
    {
      Enemy.BleedData bleedData = (Enemy.BleedData) null;
      int index2 = 0;
      for (int count = region_work.bleedList.Count; index2 < count; ++index2)
      {
        if (region_work.bleedList[index2].ownerID == status.fromObjectID)
        {
          bleedData = region_work.bleedList[index2];
          break;
        }
      }
      if (bleedData == null)
      {
        bleedData = new Enemy.BleedData();
        region_work.bleedList.Add(bleedData);
      }
      InGameSettingsManager.Player.SpecialActionInfo specialActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo;
      bleedData.ownerID = status.fromObjectID;
      if (bleedData.lv == 0)
      {
        bleedData.cnt = specialActionInfo.arrowBleedCount;
        bleedData.skipFirst = status.arrowBleedSkipFirst;
      }
      int num1 = status.arrowBleedDamage / specialActionInfo.arrowBleedCount;
      if (num1 < 1)
        num1 = 1;
      bleedData.damage += num1;
      if (bleedData.IsOwnerSelf())
        ++bleedData.lv;
      bool flag = true;
      if (!specialActionInfo.arrowBleedOther.enable && !bleedData.IsOwnerSelf())
        flag = false;
      if (Object.op_Inequality((Object) targetPoint, (Object) null) & flag)
      {
        Enemy.BleedWork bleedWork = (Enemy.BleedWork) null;
        int num2 = 1;
        int index3 = 0;
        int index4 = 0;
        for (int count = region_work.bleedWorkList.Count; index4 < count; ++index4)
        {
          if (region_work.bleedWorkList[index4].ownerID == status.fromObjectID)
          {
            bleedWork = region_work.bleedWorkList[index4];
            break;
          }
          if (num2 == region_work.bleedWorkList[index4].showIndex)
          {
            ++num2;
            index3 = index4 + 1;
          }
        }
        if (bleedWork == null)
        {
          bleedWork = new Enemy.BleedWork();
          if (bleedData.IsOwnerSelf())
          {
            region_work.bleedWorkList.Insert(0, bleedWork);
            bleedWork.showIndex = 0;
          }
          else
          {
            region_work.bleedWorkList.Insert(index3, bleedWork);
            bleedWork.showIndex = num2;
          }
          bleedWork.ownerID = status.fromObjectID;
        }
        if (Object.op_Equality((Object) bleedWork.bleedEffect, (Object) null))
        {
          string effect_name = specialActionInfo.arrowBleedEffectName;
          if (bleedWork.showIndex != 0)
            effect_name = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.arrowBleedOtherEffectName;
          bleedWork.bleedEffect = targetPoint.PlayArrowBleedEffect(effect_name, bleedWork.showIndex);
        }
        else if (bleedWork.showIndex == 0)
        {
          Animator component = ((Component) bleedWork.bleedEffect).GetComponent<Animator>();
          if (Object.op_Inequality((Object) null, (Object) component))
            component.Play("ACT" + (bleedData.lv - 1).ToString());
        }
        if (bleedData.IsMaxLv())
        {
          string burstEffectName = specialActionInfo.GetBurstEffectName(status.damageDetails.GetElementType());
          Transform trs = (Transform) null;
          if (bleedWork != null)
            trs = bleedWork.bleedEffect;
          targetPoint.PlayArrowBurstEffect(burstEffectName, trs);
          int groupOffset = 2;
          if (status.damageDetails.GetElementType() != ELEMENT_TYPE.MAX)
            ++groupOffset;
          MonoBehaviourSingleton<UIDamageManager>.I.Create(status.hitPos, status.arrowBurstDamage, UIDamageNum.DAMAGE_COLOR.NONE, groupOffset, isRegionOnly: status.isDamageRegionOnly);
        }
      }
      if (!this.IsMirror() && !this.IsPuppet() || this.IsValidWaitingPacket(StageObject.WAITING_PACKET.ENEMY_UPDATE_BLEED_DAMAGE))
        return;
      this.StartWaitingPacket(StageObject.WAITING_PACKET.ENEMY_UPDATE_BLEED_DAMAGE, false, specialActionInfo.arrowBleedTimeInterval * 2f);
    }
    else
    {
      if (!status.isArrowBomb || status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.BOOST_ARROW_RAIN || region_work.IsBombArrowLevelMax())
        return;
      InGameSettingsManager.Player.ArrowActionInfo arrowActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo;
      int num = 1;
      Player fromObject = status.fromObject as Player;
      if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.BOOST_BOMB_ARROW && Object.op_Inequality((Object) fromObject, (Object) null) && fromObject.isBoostMode)
        num = arrowActionInfo.bombArrowMaxLevel;
      for (int index5 = 0; index5 < num; ++index5)
      {
        Enemy.BombArrowData data = new Enemy.BombArrowData();
        data.ownerID = status.fromObjectID;
        data.startTime = Time.time;
        data.atk = new AtkAttribute();
        if (Object.op_Inequality((Object) fromObject, (Object) null))
          fromObject.GetAtk(status.attackInfo, ref data.atk, (SkillInfo.SkillParam) null);
        region_work.StackBombArrow(data);
      }
      if (Object.op_Inequality((Object) targetPoint, (Object) null) && !region_work.IsBombArrowLevelMax())
      {
        Enemy.BombArrowData bombArrowData = region_work.GetBombArrowData();
        if (Object.op_Equality((Object) region_work.bombArrowEffect, (Object) null) && bombArrowData != null && bombArrowData.atk != null && bombArrowData.atk.GetElementType() != ELEMENT_TYPE.MAX)
        {
          string bombArrowEffectName = arrowActionInfo.GetBombArrowEffectName(bombArrowData.atk.GetElementType());
          region_work.bombArrowEffect = targetPoint.PlayArrowBleedEffect(bombArrowEffectName, 0);
        }
        int count = region_work.bombArrowDataHistory.Count;
        Animator component = ((Component) region_work.bombArrowEffect).GetComponent<Animator>();
        if (Object.op_Inequality((Object) null, (Object) component))
          component.Play("AROW" + (count - 1).ToString());
        List<int> bombArrowSeIdList = arrowActionInfo.bombArrowSEIdList;
        if (bombArrowSeIdList.Count >= count)
          SoundManager.PlayOneShotSE(bombArrowSeIdList[count - 1], (DisableNotifyMonoBehaviour) this, this.rootNode);
      }
      else if (region_work.IsBombArrowLevelMax())
        this.OnUpdateBombArrow(region_work.regionId);
      if (!this.IsMirror() && !this.IsPuppet() || this.IsValidWaitingPacket(StageObject.WAITING_PACKET.ENEMY_UPDATE_BOMBARROW))
        return;
      this.StartWaitingPacket(StageObject.WAITING_PACKET.ENEMY_UPDATE_BOMBARROW, false, arrowActionInfo.bombArrowCountSec * 2f);
    }
  }

  public override void ActReaction(Character.ReactionInfo info, bool isSync = false)
  {
    base.ActReaction(info, isSync);
    switch (info.reactionType)
    {
      case Character.REACTION_TYPE.DOWN:
        this.ActDown();
        break;
      case Character.REACTION_TYPE.COUNTER:
        this.ActCounter(info.targetId);
        break;
      case Character.REACTION_TYPE.ELECTRIC_SHOCK:
        this.ActElectricShock();
        break;
      case Character.REACTION_TYPE.DIZZY:
        this.ActDizzy();
        break;
      case Character.REACTION_TYPE.SHADOWSEALING:
        this.ActDebuffShadowSealingStart();
        break;
      case Character.REACTION_TYPE.MAD_MODE:
        this.ActMadMode();
        break;
      case Character.REACTION_TYPE.LIGHT_RING:
        this.ActLightRing();
        break;
      case Character.REACTION_TYPE.BIND:
        this.ActBind(info.loopTime);
        break;
      case Character.REACTION_TYPE.DEAD_REVIVE:
        this.ActDeadRevive(info.deadReviveCount);
        break;
      case Character.REACTION_TYPE.SOIL_SHOCK:
        this.ActSoilShock();
        break;
      case Character.REACTION_TYPE.CONCUSSION:
        this.ActConcussionStart();
        break;
    }
  }

  public override void OnReactionDelay(
    List<Character.DelayReactionInfo> reactionDelayList)
  {
    if (this.m_reactionDelayList.Count >= 2)
    {
      Character.DelayReactionInfo delayReactionInfo = this.SearchReactionDelayInfo(Character.REACTION_TYPE.COUNTER);
      if (delayReactionInfo != null)
        this.m_reactionDelayList.Remove(delayReactionInfo);
    }
    base.OnReactionDelay(reactionDelayList);
    int count = reactionDelayList.Count;
    if (count <= 0)
      return;
    for (int index = 0; index < count; ++index)
    {
      Character.DelayReactionInfo reactionDelay = reactionDelayList[index];
      switch (reactionDelay.type)
      {
        case Character.REACTION_TYPE.DOWN:
          this.ActDown();
          break;
        case Character.REACTION_TYPE.COUNTER:
          this.ActCounter(reactionDelay.targetId);
          break;
        case Character.REACTION_TYPE.DIZZY:
          this.ActDizzy();
          break;
        case Character.REACTION_TYPE.SHADOWSEALING:
          this.ActDebuffShadowSealingStart();
          break;
        case Character.REACTION_TYPE.MAD_MODE:
          this.ActMadMode();
          break;
        case Character.REACTION_TYPE.LIGHT_RING:
          this.ActLightRing();
          break;
        case Character.REACTION_TYPE.BIND:
          this.ActBind(reactionDelay.reactionLoopTime);
          break;
      }
    }
  }

  private int GetRegionID(Collider collider, List<int> target_region_ids)
  {
    int regionId = 0;
    if (Object.op_Inequality((Object) collider, (Object) null))
    {
      RegionRoot componentInParent = ((Component) collider).gameObject.GetComponentInParent<RegionRoot>();
      if (Object.op_Inequality((Object) componentInParent, (Object) null))
      {
        for (int index1 = componentInParent.regionIDArray.Length - 1; index1 >= 0; --index1)
        {
          int index2 = componentInParent.regionIDArray[index1];
          if (index2 >= 0 && index2 < this.regionWorks.Length)
          {
            EnemyRegionWork regionWork = this.regionWorks[index2];
            Enemy.RegionInfo regionInfo = this.regionInfos[index2];
            while (!regionWork.enabled && regionWork.parentRegionID >= 0)
            {
              index2 = regionWork.parentRegionID;
              regionWork = this.regionWorks[index2];
              regionInfo = this.regionInfos[index2];
            }
            if (index1 == 0 || target_region_ids != null && target_region_ids.Contains(index2))
            {
              bool flag = false;
              if ((int) regionWork.hp <= 0 && regionInfo.maxHP > 0)
                flag = true;
              if (regionInfo.breakAfterHit || !flag)
              {
                regionId = index2;
                break;
              }
            }
          }
        }
      }
    }
    return regionId;
  }

  public int GetEnabledCounterRegionIndex()
  {
    for (int counterRegionIndex = 0; counterRegionIndex < this.regionWorks.Length; ++counterRegionIndex)
    {
      EnemyRegionWork regionWork = this.regionWorks[counterRegionIndex];
      if (regionWork != null)
      {
        Enemy.RegionInfo regionInfo = this.regionInfos[counterRegionIndex];
        if (regionInfo != null && !regionWork.isBroke && regionInfo.counterInfo.enabled)
          return counterRegionIndex;
      }
    }
    return -1;
  }

  public EnemyRegionWork GetEnabledCounterRegion()
  {
    int counterRegionIndex = this.GetEnabledCounterRegionIndex();
    return counterRegionIndex < 0 ? (EnemyRegionWork) null : this.regionWorks[counterRegionIndex];
  }

  public EnemyRegionWork SearchRegionWork(int regionId)
  {
    return regionId < 0 || regionId >= this.regionWorks.Length ? (EnemyRegionWork) null : this.regionWorks[regionId];
  }

  public int GetRegionID(string name)
  {
    int regionId = 0;
    for (int length = this.regionInfos.Length; regionId < length; ++regionId)
    {
      if (this.regionInfos[regionId].name == name)
        return regionId;
    }
    return 0;
  }

  public List<int> GetBreakRegionIDList()
  {
    List<int> breakRegionIdList = new List<int>();
    breakRegionIdList.Add(0);
    if (this.regionInfos == null)
      return breakRegionIdList;
    int index = 1;
    for (int length = this.regionInfos.Length; index < length; ++index)
    {
      if (this.regionWorks[index].isBroke)
        breakRegionIdList.Add(index);
    }
    return breakRegionIdList;
  }

  public void SetAttackInfos(AttackInfo[] attack_infos) => this.attackInfos = attack_infos;

  protected void PlayRegionEffect(int region_id, string effect_name)
  {
    if (this.targetPoints == null)
      return;
    Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
    Quaternion rotation = cameraTransform.rotation;
    Vector3 position = cameraTransform.position;
    int index = 0;
    for (int length = this.targetPoints.Length; index < length; ++index)
    {
      TargetPoint targetPoint = this.targetPoints[index];
      if (targetPoint.regionID == region_id && targetPoint.isTargetEnable)
      {
        Vector3 vector3_1 = Vector3.op_Addition(targetPoint._transform.position, Quaternion.op_Multiply(targetPoint._transform.rotation, targetPoint.scaledOffset));
        Vector3 vector3_2 = Vector3.op_Subtraction(position, vector3_1);
        Vector3 pos = Vector3.op_Addition(Vector3.op_Multiply(((Vector3) ref vector3_2).normalized, targetPoint.scaledMarkerZShift), vector3_1);
        Quaternion rot = rotation;
        EffectManager.OneShot(effect_name, pos, rot, true);
      }
    }
  }

  private void CreateDropItem(Vector3 pos, StageObject target, int rarity, bool is_region_break)
  {
    if (!(target is Self))
      return;
    DropObject.Create(rarity, is_region_break, pos);
  }

  private void CreateDropItemFromRegionBreak(
    Enemy.RegionInfo region_info,
    StageObject target,
    int rarity)
  {
    if (region_info.breakDrop == null)
      return;
    Transform node = this.FindNode(region_info.breakDrop.dropNodeName);
    if (Object.op_Equality((Object) node, (Object) null))
      return;
    this.CreateDropItem(node.position, target, rarity, true);
  }

  private void ShotRegionBreakBullet(Enemy.RegionInfo.BreakBullet[] bullets)
  {
    if (((IList<Enemy.RegionInfo.BreakBullet>) bullets).IsNullOrEmpty<Enemy.RegionInfo.BreakBullet>())
      return;
    int index = 0;
    for (int length = bullets.Length; index < length; ++index)
    {
      Enemy.RegionInfo.BreakBullet bullet = bullets[index];
      AnimEventData.EventData data = new AnimEventData.EventData();
      if (!((IList<string>) bullet.stringArgs).IsNullOrEmpty<string>())
      {
        data.stringArgs = new string[bullet.stringArgs.Length];
        bullet.stringArgs.CopyTo((Array) data.stringArgs, 0);
      }
      if (!((IList<float>) bullet.floatArgs).IsNullOrEmpty<float>())
      {
        data.floatArgs = new float[bullet.floatArgs.Length];
        bullet.floatArgs.CopyTo((Array) data.floatArgs, 0);
      }
      if (!((IList<int>) bullet.intArgs).IsNullOrEmpty<int>())
      {
        data.intArgs = new int[bullet.intArgs.Length];
        bullet.intArgs.CopyTo((Array) data.intArgs, 0);
      }
      this.EventShotGeneric(data);
    }
  }

  private void CreateDamageNum(
    Vector3 pos,
    AtkAttribute damage,
    bool buff,
    bool isRegionOnly,
    int addGroup = 0)
  {
    int num1 = addGroup;
    if ((double) damage.normal != 0.0)
      MonoBehaviourSingleton<UIDamageManager>.I.Create(pos, (int) damage.normal, buff ? UIDamageNum.DAMAGE_COLOR.BUFF : UIDamageNum.DAMAGE_COLOR.NONE, num1++, isRegionOnly: isRegionOnly);
    float damage1 = damage.fire + damage.water + damage.thunder + damage.soil + damage.light + damage.dark;
    if ((double) damage1 != 0.0)
    {
      UIDamageNum.DAMAGE_COLOR color = UIDamageNum.DAMAGE_COLOR.NONE;
      switch (damage.GetElementType())
      {
        case ELEMENT_TYPE.FIRE:
          color = UIDamageNum.DAMAGE_COLOR.FIRE;
          if (this.IsActDown())
          {
            color = UIDamageNum.DAMAGE_COLOR.GOOD;
            break;
          }
          break;
        case ELEMENT_TYPE.WATER:
          color = UIDamageNum.DAMAGE_COLOR.WATER;
          if (this.IsActDown())
          {
            color = UIDamageNum.DAMAGE_COLOR.GOOD;
            break;
          }
          break;
        case ELEMENT_TYPE.THUNDER:
          color = UIDamageNum.DAMAGE_COLOR.THUNDER;
          if (this.IsActDown())
          {
            color = UIDamageNum.DAMAGE_COLOR.GOOD;
            break;
          }
          break;
        case ELEMENT_TYPE.SOIL:
          color = UIDamageNum.DAMAGE_COLOR.SOIL;
          if (this.IsActDown())
          {
            color = UIDamageNum.DAMAGE_COLOR.GOOD;
            break;
          }
          break;
        case ELEMENT_TYPE.LIGHT:
          color = UIDamageNum.DAMAGE_COLOR.LIGHT;
          if (this.IsActDown())
          {
            color = UIDamageNum.DAMAGE_COLOR.GOOD;
            break;
          }
          break;
        case ELEMENT_TYPE.DARK:
          color = UIDamageNum.DAMAGE_COLOR.DARK;
          if (this.IsActDown())
          {
            color = UIDamageNum.DAMAGE_COLOR.GOOD;
            break;
          }
          break;
      }
      Enemy.EFFECTIVE_TYPE effectiveType = Enemy.GetEffectiveType(damage.GetElementType(), this.GetElementType());
      int effective = 0;
      if (effectiveType == Enemy.EFFECTIVE_TYPE.GOOD)
        effective = 1;
      else if (Enemy.EFFECTIVE_TYPE.BAD == effectiveType)
        effective = -1;
      MonoBehaviourSingleton<UIDamageManager>.I.Create(pos, (int) damage1, color, num1++, effective, isRegionOnly);
    }
    if (num1 != 0)
      return;
    UIDamageManager i = MonoBehaviourSingleton<UIDamageManager>.I;
    Vector3 pos1 = pos;
    int normal = (int) damage.normal;
    int color1 = buff ? 1 : 0;
    int groupOffset = num1;
    int num2 = groupOffset + 1;
    int num3 = isRegionOnly ? 1 : 0;
    i.Create(pos1, normal, (UIDamageNum.DAMAGE_COLOR) color1, groupOffset, isRegionOnly: num3 != 0);
  }

  private void PlayRegionBreakEffect(Enemy.RegionInfo region_info)
  {
    Transform node = this.FindNode(region_info.breakEffect.nodeName);
    if (Object.op_Equality((Object) node, (Object) null))
      return;
    Transform effect = EffectManager.GetEffect(region_info.breakEffect.effectName);
    if (Object.op_Equality((Object) effect, (Object) null))
      return;
    Transform transform = ((Component) node).transform;
    effect.position = transform.position;
    effect.rotation = Quaternion.op_Multiply(transform.rotation, Quaternion.Euler(region_info.breakEffect.effectAngle));
  }

  public void UpdateRegionVisual()
  {
    int index1 = 0;
    for (int length1 = this.regionWorks.Length; index1 < length1; ++index1)
    {
      EnemyRegionWork regionWork = this.regionWorks[index1];
      if ((int) regionWork.hp <= 0)
      {
        foreach (string deactivateObject in this.regionInfos[index1].deactivateObjects)
        {
          Transform node = this.FindNode(deactivateObject);
          if (Object.op_Inequality((Object) node, (Object) null))
            ((Component) node).gameObject.SetActive(false);
        }
        int index2 = 0;
        for (int length2 = this.regionWorks.Length; index2 < length2; ++index2)
        {
          if (this.regionWorks[index2].parentRegionID >= 0 && this.regionWorks[index2].parentRegionID == index1)
            this.regionWorks[index2].enabled = true;
        }
      }
      int index3 = 0;
      while (index3 < regionWork.bleedWorkList.Count)
      {
        Enemy.BleedWork bleedWork = regionWork.bleedWorkList[index3];
        bool flag = false;
        int index4 = 0;
        for (int count = regionWork.bleedList.Count; index4 < count; ++index4)
        {
          if (regionWork.bleedList[index4].ownerID == bleedWork.ownerID)
          {
            flag = true;
            break;
          }
        }
        if (flag)
        {
          ++index3;
        }
        else
        {
          if (Object.op_Inequality((Object) bleedWork.bleedEffect, (Object) null))
          {
            EffectManager.ReleaseEffect(((Component) bleedWork.bleedEffect).gameObject);
            bleedWork.bleedEffect = (Transform) null;
          }
          regionWork.bleedWorkList.RemoveAt(index3);
        }
      }
      if (regionWork.shadowSealingData.ownerID == 0 && regionWork.shadowSealingEffect != null)
      {
        EffectManager.ReleaseEffect(((Component) regionWork.shadowSealingEffect).gameObject, false, true);
        regionWork.shadowSealingEffect = (Transform) null;
      }
      TargetPoint targetPoint = (TargetPoint) null;
      int index5 = 0;
      for (int length3 = this.targetPoints.Length; index5 < length3; ++index5)
      {
        if (this.targetPoints[index5].regionID == index1 && this.targetPoints[index5].isAimEnable)
        {
          targetPoint = this.targetPoints[index5];
          break;
        }
      }
      if (Object.op_Inequality((Object) targetPoint, (Object) null))
      {
        int index6 = 0;
        for (int count1 = regionWork.bleedList.Count; index6 < count1; ++index6)
        {
          Enemy.BleedData bleed = regionWork.bleedList[index6];
          bool flag = true;
          if (!MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.arrowBleedOther.enable && !bleed.IsOwnerSelf())
            flag = false;
          if (flag)
          {
            Enemy.BleedWork bleedWork = (Enemy.BleedWork) null;
            int num = 1;
            int index7 = 0;
            int index8 = 0;
            for (int count2 = regionWork.bleedWorkList.Count; index8 < count2; ++index8)
            {
              if (regionWork.bleedWorkList[index8].ownerID == bleed.ownerID)
              {
                bleedWork = regionWork.bleedWorkList[index8];
                break;
              }
              if (num == regionWork.bleedWorkList[index8].showIndex)
              {
                ++num;
                index7 = index8 + 1;
              }
            }
            if (bleedWork == null)
            {
              bleedWork = new Enemy.BleedWork();
              if (bleed.IsOwnerSelf())
              {
                regionWork.bleedWorkList.Insert(0, bleedWork);
                bleedWork.showIndex = 0;
              }
              else
              {
                regionWork.bleedWorkList.Insert(index7, bleedWork);
                bleedWork.showIndex = num;
              }
              bleedWork.ownerID = bleed.ownerID;
            }
            if (Object.op_Equality((Object) bleedWork.bleedEffect, (Object) null))
            {
              string effect_name = MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.arrowBleedEffectName;
              if (bleedWork.showIndex != 0)
                effect_name = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.arrowBleedOtherEffectName;
              bleedWork.bleedEffect = targetPoint.PlayArrowBleedEffect(effect_name, bleedWork.showIndex);
            }
          }
        }
        if (regionWork.shadowSealingData.ownerID != 0 && regionWork.shadowSealingEffect == null)
          regionWork.shadowSealingEffect = targetPoint.PlayArrowBleedEffect(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.shadowSealingEffectName, 0);
      }
    }
    this.InitializeBarrierEffect();
    this.UpdateBreakIDLists();
  }

  private void InitializeBarrierEffect()
  {
    List<AnimEventData.EventData> eventDataList = this.animEventProcessor.ListUpEventData(AnimEventFormat.ID.EFFECT_LOOP_CUSTOM);
    if (eventDataList == null || eventDataList.Count <= 0)
      return;
    foreach (AnimEventData.EventData data in eventDataList)
    {
      if (this.IsValidBarrier)
        this.EventEffectLoopCustom(data);
    }
  }

  public void ReviveRegion(int region_id)
  {
    if (region_id < 0 || region_id >= this.regionInfos.Length)
      return;
    EnemyRegionWork regionWork = this.regionWorks[region_id];
    Enemy.RegionInfo regionInfo = this.regionInfos[region_id];
    if (regionWork == null)
      return;
    regionWork.hp = (XorInt) regionInfo.maxHP;
    foreach (string deactivateObject in regionInfo.deactivateObjects)
    {
      Transform node = this.FindNode(deactivateObject);
      if (Object.op_Inequality((Object) node, (Object) null))
        ((Component) node).gameObject.SetActive(true);
    }
    int index = 0;
    for (int length = this.regionWorks.Length; index < length; ++index)
      this.regionWorks[index].OnReviveRegion(region_id);
    EnemyController controller = this.controller as EnemyController;
    if (Object.op_Inequality((Object) controller, (Object) null))
      controller.OnReviveRegion(region_id);
    this.reviveRegionWaitSync = false;
    if (Object.op_Inequality((Object) this.enemySender, (Object) null))
      this.enemySender.OnReviveRegion(region_id);
    if (!MonoBehaviourSingleton<TargetMarkerManager>.IsValid())
      return;
    MonoBehaviourSingleton<TargetMarkerManager>.I.updateShadowSealingFlag = true;
  }

  public bool IsEnableReviveRegion(int region_id)
  {
    if (region_id < 0 || region_id >= this.regionInfos.Length)
    {
      Log.Error("RegionId is out of range!!");
      return false;
    }
    EnemyRegionWork regionWork = this.regionWorks[region_id];
    Enemy.RegionInfo regionInfo = this.regionInfos[region_id];
    return regionInfo != null && regionWork != null && regionInfo.enableRevive && (int) regionWork.hp <= 0 && regionInfo.maxHP > 0 && (double) Time.time - (double) regionWork.breakTime >= (double) regionInfo.reviveIntervalTime;
  }

  public void ActivateRegionNode(int[] regionIDs, bool isRandom = false, int randomSelectedID = -1)
  {
    if (((IList<int>) regionIDs).IsNullOrEmpty<int>() || ((IList<RegionRoot>) this.regionRoots).IsNullOrEmpty<RegionRoot>())
      return;
    for (int index1 = 0; index1 < regionIDs.Length; ++index1)
    {
      int regionId = regionIDs[index1];
      for (int index2 = 0; index2 < this.regionRoots.Length; ++index2)
      {
        if (this.regionRoots[index2].regionID == regionId)
        {
          if (!isRandom)
            ((Component) this.regionRoots[index2]).gameObject.SetActive(true);
          else if (this.regionRoots[index2].regionID != randomSelectedID)
          {
            ((Component) this.regionRoots[index2]).gameObject.SetActive(false);
          }
          else
          {
            ((Component) this.regionRoots[index2]).gameObject.SetActive(true);
            Enemy.RegionInfo regionInfo = this.regionInfos[randomSelectedID];
            if (regionInfo != null && regionInfo.modeChangeInfo.enabled)
            {
              EnemyBrain brain = this.controller.brain as EnemyBrain;
              if (!Object.op_Equality((Object) brain, (Object) null))
              {
                EnemyActionController actionCtrl = brain.actionCtrl;
                if (actionCtrl != null)
                  actionCtrl.modeId = regionInfo.modeChangeInfo.modeID;
              }
            }
          }
        }
      }
    }
    if (!Object.op_Inequality((Object) this.enemySender, (Object) null))
      return;
    this.enemySender.OnEnemyRegionNodeActivate(regionIDs, isRandom, randomSelectedID);
  }

  public bool IsBleedFromSelf(int region_id)
  {
    if (region_id < 0 || region_id >= this.regionWorks.Length)
    {
      Log.Error("RegionId is out of range!!");
      return false;
    }
    EnemyRegionWork regionWork = this.regionWorks[region_id];
    if (regionWork == null)
      return false;
    int index = 0;
    for (int count = regionWork.bleedList.Count; index < count; ++index)
    {
      if (regionWork.bleedList[index].IsOwnerSelf())
        return true;
    }
    return false;
  }

  public bool IsMaxLvBleedFromSelf(int region_id)
  {
    if (region_id < 0 || region_id >= this.regionWorks.Length)
    {
      Log.Error("RegionId is out of range!!");
      return false;
    }
    EnemyRegionWork regionWork = this.regionWorks[region_id];
    if (regionWork == null)
      return false;
    int index = 0;
    for (int count = regionWork.bleedList.Count; index < count; ++index)
    {
      if (regionWork.bleedList[index].IsOwnerSelf())
        return regionWork.bleedList[index].IsMaxLv();
    }
    return false;
  }

  public bool IsShadowSealingStuck(int regionId)
  {
    if (this.IsDebuffShadowSealing())
      return true;
    if (regionId < 0 || regionId >= this.regionWorks.Length)
    {
      Log.Error("RegionId is out of range!!");
      return false;
    }
    EnemyRegionWork regionWork = this.regionWorks[regionId];
    return regionWork != null && regionWork.shadowSealingData.ownerID != 0;
  }

  public void SetHitShock(Vector3 vec)
  {
    if (this.canHitShockEffect)
      this.hitShockOffsetFlag = true;
    this.hitShockLightTime = 0.0f;
    this.hitShockOffsetTime = 0.0f;
    this.hitShockVec = vec;
    this.hitShockVec.y = 0.0f;
    ((Vector3) ref this.hitShockVec).Normalize();
  }

  public void SetHitLight() => this.hitShockLightFlag = true;

  public override string EffectNameAnalyzer(string effect_name)
  {
    if (!string.IsNullOrEmpty(effect_name) && this.enemyTableData != null && !string.IsNullOrEmpty(this.enemyTableData.effectEnemyKey))
      effect_name = effect_name.Replace("[ENEMY_KEY]", this.enemyTableData.effectEnemyKey);
    return effect_name;
  }

  private void EventReviveRegion(AnimEventData.EventData data)
  {
    int intArg = data.intArgs[0];
    if (intArg < 0 || intArg >= this.regionInfos.Length)
      Log.Error("Out of region index !! id:" + (object) intArg);
    else if (this.IsCoopNone() || this.IsOriginal())
    {
      if (!this.IsEnableReviveRegion(intArg))
        return;
      this.ReviveRegion(intArg);
    }
    else
    {
      if (this.reviveRegionWaitSync)
        return;
      this.reviveRegionWaitSync = true;
    }
  }

  private void EventDashStart(AnimEventData.EventData data)
  {
    if (this.enableDash)
      return;
    string stringArg = data.stringArgs.Length != 0 ? data.stringArgs[0] : (string) null;
    float num1 = data.floatArgs.Length != 0 ? data.floatArgs[0] : 0.0f;
    float num2 = data.floatArgs.Length > 1 ? data.floatArgs[1] : 0.0f;
    this.wallStayTimer = 0.0f;
    this.enableDash = true;
    this.dashBeforePos = this._position;
    this.dashNowDistance = 0.0f;
    this.dashOverDistance = num1;
    this.dashMinDistance = num2;
    this.dashOverFlag = false;
    this.dashOverCheckDistance = 0.0f;
    this.dashEndTrigger = !string.IsNullOrEmpty(stringArg) ? stringArg : "next";
    this.rotateSafeMode = true;
    this.dashMaxDistance = 0.0f;
    if (this.actionPositionFlag)
    {
      Vector3 vector3 = Vector3.op_Subtraction(this.actionPosition, this._position);
      vector3.y = 0.0f;
      this.dashMaxDistance = ((Vector3) ref vector3).magnitude;
    }
    if ((double) this.dashMaxDistance < (double) this.dashMinDistance)
      this.dashMaxDistance = this.dashMinDistance;
    this.dashMaxDistance *= this.enemyParameter.dashMaxDistanceRate;
  }

  private void EventWarpViewStart(AnimEventData.EventData data)
  {
    float floatArg = data.floatArgs[0];
    if ((double) floatArg <= 0.0)
    {
      this.warpViewFlag = false;
      this.warpViewRate = 1f;
      this.warpViewRatePerTime = 0.0f;
      this.SetWarpVisible(this.warpViewRate);
    }
    else
    {
      this.warpViewFlag = true;
      this.warpViewRatePerTime = (1f - this.warpViewRate) / floatArg;
    }
  }

  private void EventWarpViewEnd(AnimEventData.EventData data)
  {
    float floatArg = data.floatArgs[0];
    if ((double) floatArg <= 0.0)
    {
      this.warpViewFlag = false;
      this.warpViewRate = 0.0f;
      this.warpViewRatePerTime = 0.0f;
      this.SetWarpVisible(this.warpViewRate);
    }
    else
    {
      this.warpViewFlag = true;
      this.warpViewRatePerTime = -this.warpViewRate / floatArg;
    }
  }

  private void EventWarpToTarget(AnimEventData.EventData data)
  {
    this.SetWarpToTarget(data.floatArgs[0]);
  }

  private void EventWarpToReverseTarget(AnimEventData.EventData data)
  {
    this.SetWarpToTarget(data.floatArgs[0], true);
  }

  private void EventWarpToRandom(AnimEventData.EventData data)
  {
    this.SetWarpToRandom(data.floatArgs[0], data.floatArgs.Length > 1 ? data.floatArgs[1] : 0.0f);
  }

  private void EventRadialBlurStart(AnimEventData.EventData data)
  {
    if (MonoBehaviourSingleton<GlobalSettingsManager>.I.noBlurEffectForDeviceModel.Any<string>((Func<string, bool>) (model => SystemInfo.deviceModel.ContainIgnoreCase(model))))
    {
      Log.Error("{0} is not allow to do blur effect", (object) SystemInfo.deviceModel);
    }
    else
    {
      float floatArg1 = data.floatArgs[0];
      float floatArg2 = data.floatArgs[1];
      string stringArg = data.stringArgs[0];
      bool flag = data.intArgs[0] != 0;
      Transform node = this.FindNode(stringArg);
      if (Object.op_Equality((Object) node, (Object) null))
      {
        Log.Error("Not found node for RadialBlur!! " + stringArg);
      }
      else
      {
        if (flag)
        {
          if (MonoBehaviourSingleton<GlobalSettingsManager>.I.noBlurEffectBossId.Contains(this.enemyID) && MonoBehaviourSingleton<GlobalSettingsManager>.I.noBlurEffectEventId.Contains(Utility.GetCurrentEventID()))
            return;
          MonoBehaviourSingleton<InGameCameraManager>.I.StartRadialBlurFilter(floatArg1, floatArg2, node);
        }
        else
          MonoBehaviourSingleton<InGameCameraManager>.I.StartRadialBlurFilter(floatArg1, floatArg2, node.position);
        this.radialBlurEnable = true;
      }
    }
  }

  private void EventRadialBlurChange(AnimEventData.EventData data)
  {
    float floatArg1 = data.floatArgs[0];
    float floatArg2 = data.floatArgs[1];
    if ((double) floatArg2 <= 0.0)
    {
      MonoBehaviourSingleton<InGameCameraManager>.I.EndRadialBlurFilter(floatArg1);
      this.radialBlurEnable = false;
    }
    else
      MonoBehaviourSingleton<InGameCameraManager>.I.ChangeRadialBlurFilter(floatArg1, floatArg2);
  }

  private void EventRadialBlurEnd(AnimEventData.EventData data)
  {
    MonoBehaviourSingleton<InGameCameraManager>.I.EndRadialBlurFilter(data.floatArgs[0]);
    this.radialBlurEnable = false;
  }

  private void EventShotTarget(AnimEventData.EventData data)
  {
    AttackInfo attackInfo = this.FindAttackInfo(data.stringArgs[0]);
    if (attackInfo == null)
    {
      Log.Error("Not found AttackInfo !! " + data.stringArgs[0]);
    }
    else
    {
      Transform node = this.FindNode(data.stringArgs[1]);
      if (Object.op_Equality((Object) node, (Object) null))
      {
        Log.Error("Not found node for ShotTarget!! " + data.stringArgs[1]);
      }
      else
      {
        Vector3 vector3;
        // ISSUE: explicit constructor call
        ((Vector3) ref vector3).\u002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
        Matrix4x4 localToWorldMatrix = node.localToWorldMatrix;
        Vector3 pos = ((Matrix4x4) ref localToWorldMatrix).MultiplyPoint3x4(vector3);
        switch (data.intArgs[0])
        {
          case 0:
            List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
            if (playerList.IsNullOrEmpty<StageObject>())
              break;
            int index1 = 0;
            for (int count = playerList.Count; index1 < count; ++index1)
            {
              Player player = playerList[index1] as Player;
              if (!Object.op_Equality((Object) player, (Object) null) && !player.isDead)
              {
                Vector3 position = player._position;
                ++position.y;
                Quaternion rot = Quaternion.LookRotation(Vector3.op_Subtraction(position, pos));
                AnimEventShot.Create((StageObject) this, attackInfo, pos, rot).SetTarget((StageObject) player);
              }
            }
            break;
          case 1:
            int intArg1 = data.intArgs[1];
            if (this.IsOriginal() || this.IsCoopNone())
            {
              List<Enemy.RandomShotInfo.TargetInfo> targets = new List<Enemy.RandomShotInfo.TargetInfo>(intArg1);
              List<Player> alivePlayerList = MonoBehaviourSingleton<StageObjectManager>.I.GetAlivePlayerList();
              int count = alivePlayerList.Count;
              for (int index2 = 0; index2 < intArg1; ++index2)
              {
                int index3 = Random.Range(0, count);
                Player player = alivePlayerList[index3];
                if (Object.op_Equality((Object) player, (Object) null))
                {
                  targets.Add(new Enemy.RandomShotInfo.TargetInfo(node.rotation, -1));
                }
                else
                {
                  Vector3 position = player._position;
                  ++position.y;
                  Quaternion rot = Quaternion.LookRotation(Vector3.op_Subtraction(position, pos));
                  targets.Add(new Enemy.RandomShotInfo.TargetInfo(rot, player.id));
                }
              }
              this.TargetRandamShotEvent(targets);
            }
            this.SetRandomShotInfoForShotTarget(intArg1, pos, attackInfo, data);
            break;
          case 2:
            int intArg2 = data.intArgs[1];
            if (this.IsOriginal() || this.IsCoopNone())
            {
              List<Enemy.RandomShotInfo.TargetInfo> targets = new List<Enemy.RandomShotInfo.TargetInfo>(intArg2);
              float floatArg1 = data.floatArgs[4];
              float floatArg2 = data.floatArgs[5];
              for (int index4 = 0; index4 < intArg2; ++index4)
              {
                Quaternion rot = Quaternion.op_Multiply(Quaternion.Euler(new Vector3(Random.Range(-floatArg1, floatArg1), Random.Range(-floatArg2, floatArg2), 0.0f)), node.rotation);
                targets.Add(new Enemy.RandomShotInfo.TargetInfo(rot, -1));
              }
              this.TargetRandamShotEvent(targets);
            }
            this.SetRandomShotInfoForShotTarget(intArg2, pos, attackInfo, data);
            break;
        }
      }
    }
  }

  private void SetRandomShotInfoForShotTarget(
    int numBullet,
    Vector3 pos,
    AttackInfo info,
    AnimEventData.EventData data)
  {
    bool flag = false;
    Enemy.RandomShotInfo randomShotInfo;
    if (this.shotNetworkInfoQueue.Count > 0)
    {
      randomShotInfo = this.shotNetworkInfoQueue[0];
      this.shotNetworkInfoQueue.Remove(randomShotInfo);
      flag = true;
    }
    else
      randomShotInfo = new Enemy.RandomShotInfo();
    randomShotInfo.atkInfo = info;
    for (int index = 0; index < numBullet; ++index)
      randomShotInfo.points[index] = pos;
    randomShotInfo.shotCount = 0;
    randomShotInfo.countTime = 0.0f;
    randomShotInfo.interval = data.floatArgs[3];
    if (flag)
      this.randomShotInfo.Add(randomShotInfo);
    else
      this.shotEventInfoQueue.Add(randomShotInfo);
  }

  private void EventShotPoint(AnimEventData.EventData data)
  {
    AttackInfo attackInfo = this.FindAttackInfo(data.stringArgs[0]);
    if (attackInfo == null)
    {
      Log.Error("Not found AttackInfo !! " + data.stringArgs[0]);
    }
    else
    {
      bool isFixedY = data.intArgs.Length > 2;
      Vector3 vector3;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3).\u002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
      switch (data.intArgs[0])
      {
        case 0:
          List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
          if (playerList.IsNullOrEmpty<StageObject>())
            break;
          int index1 = 0;
          for (int count = playerList.Count; index1 < count; ++index1)
          {
            Player player = playerList[index1] as Player;
            if (!Object.op_Equality((Object) player, (Object) null) && !player.isDead)
            {
              Matrix4x4 localToWorldMatrix = player._transform.localToWorldMatrix;
              Vector3 pos = ((Matrix4x4) ref localToWorldMatrix).MultiplyPoint3x4(vector3);
              if (isFixedY && data.intArgs[2] != 0)
                pos.y = data.floatArgs[1];
              Quaternion rot = Quaternion.op_Multiply(this._rotation, Quaternion.Euler(new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5])));
              AnimEventShot.Create((StageObject) this, attackInfo, pos, rot).SetTarget((StageObject) player);
            }
          }
          break;
        case 1:
          int num1 = Mathf.Max(1, data.intArgs[1]);
          if (this.IsOriginal() || this.IsCoopNone())
          {
            List<Vector3> points = new List<Vector3>(num1);
            List<Player> alivePlayerList = MonoBehaviourSingleton<StageObjectManager>.I.GetAlivePlayerList();
            int count = alivePlayerList.Count;
            for (int index2 = 0; index2 < num1; ++index2)
            {
              int index3 = Random.Range(0, count);
              Player player = alivePlayerList[index3];
              if (!Object.op_Equality((Object) player, (Object) null))
              {
                Matrix4x4 localToWorldMatrix = player._transform.localToWorldMatrix;
                Vector3 shotPoint = this.CreateShotPoint(((Matrix4x4) ref localToWorldMatrix).MultiplyPoint3x4(vector3), player._transform, isFixedY, data);
                points.Add(shotPoint);
              }
            }
            this.PointRandamShotEvent(points);
          }
          this.SetRandomShotInfo(num1, attackInfo, data);
          break;
        case 3:
          int num2 = Mathf.Max(1, data.intArgs[1]);
          if (this.IsOriginal() || this.IsCoopNone())
          {
            List<Vector3> points = new List<Vector3>(num2);
            for (int index4 = 0; index4 < num2; ++index4)
            {
              if (!Object.op_Equality((Object) this.actionTarget, (Object) null))
              {
                Matrix4x4 localToWorldMatrix = this.actionTarget._transform.localToWorldMatrix;
                Vector3 shotPoint = this.CreateShotPoint(((Matrix4x4) ref localToWorldMatrix).MultiplyPoint3x4(vector3), this.actionTarget._transform, isFixedY, data);
                points.Add(shotPoint);
              }
            }
            this.PointRandamShotEvent(points);
          }
          this.SetRandomShotInfo(num2, attackInfo, data);
          break;
      }
    }
  }

  private Vector3 CreateShotPoint(
    Vector3 pos,
    Transform targetTrans,
    bool isFixedY,
    AnimEventData.EventData data)
  {
    if (isFixedY && data.intArgs[2] != 0)
      pos.y = data.floatArgs[1];
    Quaternion quaternion = Quaternion.Euler(new Vector3(0.0f, (float) Random.Range(-180, 180), 0.0f));
    pos = Vector3.op_Addition(pos, Vector3.op_Multiply(Quaternion.op_Multiply(quaternion, targetTrans.forward), Random.Range(0.0f, data.floatArgs[8])));
    pos = Vector3.op_Addition(pos, Vector3.op_Multiply(targetTrans.up, Random.Range(-data.floatArgs[7], data.floatArgs[7])));
    return pos;
  }

  private void SetRandomShotInfo(int numBullet, AttackInfo info, AnimEventData.EventData data)
  {
    Quaternion rot = Quaternion.op_Multiply(this._rotation, Quaternion.Euler(new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5])));
    bool flag = false;
    Enemy.RandomShotInfo randomShotInfo;
    if (this.shotNetworkInfoQueue.Count > 0)
    {
      randomShotInfo = this.shotNetworkInfoQueue[0];
      this.shotNetworkInfoQueue.Remove(randomShotInfo);
      flag = true;
    }
    else
      randomShotInfo = new Enemy.RandomShotInfo();
    randomShotInfo.targets = new List<Enemy.RandomShotInfo.TargetInfo>(numBullet);
    randomShotInfo.atkInfo = info;
    for (int index = 0; index < numBullet; ++index)
      randomShotInfo.targets.Add(new Enemy.RandomShotInfo.TargetInfo(rot, -1));
    randomShotInfo.shotCount = 0;
    randomShotInfo.countTime = 0.0f;
    randomShotInfo.interval = data.floatArgs[6];
    if (flag)
      this.randomShotInfo.Add(randomShotInfo);
    else
      this.shotEventInfoQueue.Add(randomShotInfo);
  }

  private void EventShotWorldPoint(AnimEventData.EventData data)
  {
    AttackInfo attackInfo = this.FindAttackInfo(data.stringArgs[0]);
    if (attackInfo == null)
      return;
    Vector3 pos;
    // ISSUE: explicit constructor call
    ((Vector3) ref pos).\u002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
    Quaternion rot = Quaternion.Euler(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
    AnimEventShot.Create((StageObject) this, attackInfo, pos, rot);
  }

  private void EventWeakPointON(AnimEventData.EventData data)
  {
    if (!this.CanSetWeakPoint())
      return;
    int intArg = data.intArgs[0];
    if (intArg < 0 || intArg >= this.regionInfos.Length)
    {
      Log.Error("Region Index is out of range!! ");
    }
    else
    {
      int weakType = -1;
      if (data.intArgs.Length > 1)
        weakType = data.intArgs[1];
      int validElement = -1;
      if (data.intArgs.Length > 2)
        validElement = data.intArgs[2];
      float displayTime = 0.0f;
      if (data.floatArgs.Length != 0)
        displayTime = data.floatArgs[0];
      string deleteAttackName = "";
      if (data.stringArgs.Length > 1)
        deleteAttackName = data.stringArgs[1];
      if (this.regionWorks[intArg].IsValidDisplayTimer)
        return;
      this.regionWorks[intArg].SetupWeakPoint(weakType, data.attackMode, displayTime, deleteAttackName, validElement);
    }
  }

  private void EventWeakPointOFF(AnimEventData.EventData data)
  {
    int intArg = data.intArgs[0];
    if (intArg < 0 || intArg >= this.regionInfos.Length)
    {
      Log.Error("Region Index is out of range!! ");
    }
    else
    {
      EnemyRegionWork regionWork = this.regionWorks[intArg];
      if (regionWork.IsValidDisplayTimer)
        return;
      regionWork.weakState = Enemy.WEAK_STATE.NONE;
    }
  }

  private void EventWeakPointAllON(AnimEventData.EventData data)
  {
    if (!this.CanSetWeakPoint())
      return;
    int length = this.regionWorks.Length;
    for (int index = 0; index < length; ++index)
    {
      if (!this.regionWorks[index].IsValidDisplayTimer)
        this.regionWorks[index].SetupWeakPoint(data.intArgs[0], data.attackMode);
    }
  }

  private void EventWeakPointAllOFF(AnimEventData.EventData data)
  {
    int index = 0;
    for (int length = this.regionWorks.Length; index < length; ++index)
    {
      if (!this.regionWorks[index].IsValidDisplayTimer)
        this.regionWorks[index].weakState = Enemy.WEAK_STATE.NONE;
    }
  }

  private bool CanSetWeakPoint() => !this.IsActBind() && !this.IsActConcussion();

  private void EventHideBaseEffectON(AnimEventData.EventData data)
  {
    this.SetBaseEffecActivateFlag(false);
  }

  private void EventHideBaseEffectOFF(AnimEventData.EventData data)
  {
    this.SetBaseEffecActivateFlag(true);
  }

  protected override void EventNWayLaserAttack(AnimEventData.EventData data)
  {
    string stringArg1 = data.stringArgs[0];
    string stringArg2 = data.stringArgs[1];
    int intArg = data.intArgs[0];
    Transform parentTrans = Utility.Find(this._transform, stringArg2);
    if (Object.op_Equality((Object) parentTrans, (Object) null))
    {
      Log.Error("Not found node!! name:" + stringArg2);
    }
    else
    {
      AttackInfo attackInfo = this.FindAttackInfo(stringArg1);
      if (attackInfo == null)
      {
        Log.Error("Not found AttackInfo!! name:" + stringArg1);
      }
      else
      {
        BulletData bulletData = attackInfo.bulletData;
        if (Object.op_Equality((Object) bulletData, (Object) null) || bulletData.dataLaser == null)
        {
          Log.Error("Not found BulletData!! atkInfoName:" + stringArg1);
        }
        else
        {
          AttackNWayLaser attackNwayLaser = new GameObject("AttackNWayLaser").AddComponent<AttackNWayLaser>();
          attackNwayLaser.Initialize((StageObject) this, parentTrans, attackInfo, intArg);
          this.m_activeAttackLaserList.Add(attackNwayLaser);
        }
      }
    }
  }

  private void EventObstacleNodeLinkAttack(AnimEventData.EventData data)
  {
    string stringArg1 = data.stringArgs[0];
    string stringArg2 = data.stringArgs[1];
    Transform parentTrans = Utility.Find(this._transform, stringArg2);
    if (Object.op_Equality((Object) parentTrans, (Object) null))
    {
      Log.Error("Not found node!! name:" + stringArg2);
    }
    else
    {
      if (!((Component) parentTrans).gameObject.activeSelf)
        return;
      AttackInfo attackInfo = this.FindAttackInfo(stringArg1);
      if (attackInfo == null)
      {
        Log.Error("Not found AttackInfo!! name:" + stringArg1);
      }
      else
      {
        AnimEventShot childEventShot = (AnimEventShot) null;
        Vector3 offset;
        // ISSUE: explicit constructor call
        ((Vector3) ref offset).\u002Ector(0.0f, 0.0f, 0.0f);
        if (data.intArgs.Length > 1 && data.intArgs[1] != 0)
        {
          if (Object.op_Inequality((Object) this.actionTarget, (Object) null))
          {
            Vector3 vector3;
            // ISSUE: explicit constructor call
            ((Vector3) ref vector3).\u002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
            Quaternion rot = Quaternion.op_Multiply(this._rotation, Quaternion.Euler(new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5])));
            Matrix4x4 localToWorldMatrix = this.actionTarget._transform.localToWorldMatrix;
            Vector3 pos = ((Matrix4x4) ref localToWorldMatrix).MultiplyPoint3x4(vector3);
            childEventShot = AnimEventShot.Create((StageObject) this, attackInfo, pos, rot);
          }
          else
            offset.z += 2f;
        }
        if (Object.op_Equality((Object) childEventShot, (Object) null))
          childEventShot = AnimEventShot.Create((StageObject) this, data, attackInfo, offset);
        AttackShotNodeLink attackShotNodeLink = new GameObject("AttackObstacleNodeLink").AddComponent<AttackShotNodeLink>();
        attackShotNodeLink.Initialize((StageObject) this, parentTrans, data, attackInfo, childEventShot);
        this.m_activeAttackObstacleList.Add(attackShotNodeLink);
      }
    }
  }

  private void EventFunnelAttack(AnimEventData.EventData data)
  {
    string stringArg1 = data.stringArgs[0];
    string stringArg2 = data.stringArgs[1];
    Transform launchTrans = this.FindNode(stringArg2);
    if (Object.op_Equality((Object) launchTrans, (Object) null))
    {
      Log.Error("Not found transform for launch!! name:" + stringArg2);
    }
    else
    {
      AttackInfo atkInfo = this.FindAttackInfo(stringArg1);
      if (atkInfo == null)
      {
        Log.Error("Not found AttackInfo!! name:" + stringArg1);
      }
      else
      {
        BulletData bulletData = atkInfo.bulletData;
        if (Object.op_Equality((Object) bulletData, (Object) null) || bulletData.dataFunnel == null)
        {
          Log.Error("Not found BulletData!! atkInfoName:" + stringArg1);
        }
        else
        {
          Vector3 offsetPos = new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
          Quaternion offsetRot = Quaternion.Euler(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
          MonoBehaviourSingleton<StageObjectManager>.I.GetAlivePlayerList().ForEach((Action<Player>) (targetChar =>
          {
            AttackFunnelBit attackFunnelBit = new GameObject("AttackFunnelBit").AddComponent<AttackFunnelBit>();
            attackFunnelBit.Initialize((StageObject) this, atkInfo, (StageObject) targetChar, launchTrans, offsetPos, offsetRot);
            this.m_activeAttackFunnelList.Add(attackFunnelBit);
          }));
        }
      }
    }
  }

  private void EventUndeadAttack(AnimEventData.EventData data)
  {
    string stringArg1 = data.stringArgs[0];
    string stringArg2 = data.stringArgs[1];
    Transform launchTrans = this.FindNode(stringArg2);
    if (Object.op_Equality((Object) launchTrans, (Object) null))
    {
      Log.Error("Not found transform for launch!! name:" + stringArg2);
    }
    else
    {
      AttackInfo atkInfo = this.FindAttackInfo(stringArg1);
      if (atkInfo == null)
      {
        Log.Error("Not found AttackInfo!! name:" + stringArg1);
      }
      else
      {
        BulletData bulletData = atkInfo.bulletData;
        if (Object.op_Equality((Object) bulletData, (Object) null) || bulletData.dataUndead == null)
        {
          Log.Error("Not found BulletData!! atkInfoName:" + stringArg1);
        }
        else
        {
          Vector3 offsetPos = new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
          Quaternion offsetRot = Quaternion.Euler(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
          MonoBehaviourSingleton<StageObjectManager>.I.GetAlivePlayerList().ForEach((Action<Player>) (targetChar => new GameObject("AttackUndead").AddComponent<AttackUndead>().Initialize((StageObject) this, atkInfo, (StageObject) targetChar, launchTrans, offsetPos, offsetRot)));
        }
      }
    }
  }

  private void EventDigAttack(AnimEventData.EventData data)
  {
    string stringArg1 = data.stringArgs[0];
    string stringArg2 = data.stringArgs[1];
    Transform launchTrans = this.FindNode(stringArg2);
    if (Object.op_Equality((Object) launchTrans, (Object) null))
    {
      Log.Error("Not found transform for launch!! name:" + stringArg2);
    }
    else
    {
      AttackInfo atkInfo = this.FindAttackInfo(stringArg1);
      if (atkInfo == null)
      {
        Log.Error("Not found AttackInfo!! name:" + stringArg1);
      }
      else
      {
        BulletData bulletData = atkInfo.bulletData;
        if (Object.op_Equality((Object) bulletData, (Object) null) || bulletData.dataDig == null)
        {
          Log.Error("Not found BulletData!! atkInfoName:" + stringArg1);
        }
        else
        {
          Vector3 offsetPos = new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
          Quaternion offsetRot = Quaternion.Euler(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
          MonoBehaviourSingleton<StageObjectManager>.I.playerList.ForEach((Action<StageObject>) (targetChar =>
          {
            AttackDig attackDig = new GameObject("AttackDig").AddComponent<AttackDig>();
            attackDig.Initialize((StageObject) this, atkInfo, targetChar, launchTrans, offsetPos, offsetRot);
            this.m_activeAttackDigList.Add(attackDig);
          }));
        }
      }
    }
  }

  private void EventCancelAction(AnimEventData.EventData data)
  {
    if (!this.IsCoopNone() && !this.IsOriginal() || this.isDead)
      return;
    Enemy.CANCEL_CONDITION intArg = (Enemy.CANCEL_CONDITION) data.intArgs[0];
    if (intArg == Enemy.CANCEL_CONDITION.NONE)
      return;
    float floatArg = data.floatArgs[0];
    bool flag = false;
    if (intArg == Enemy.CANCEL_CONDITION.FAILED_GRAB)
    {
      EnemyBrain brain = this.controller.brain as EnemyBrain;
      if (!Object.op_Equality((Object) brain, (Object) null))
      {
        GrabController grabController = brain.actionCtrl.grabController;
        if (grabController != null && !grabController.IsGrabing())
          flag = true;
      }
    }
    if (!flag)
      return;
    this.ActIdle(true, floatArg);
  }

  private void EventReleaseGrab(AnimEventData.EventData data)
  {
    this.ActReleaseGrabbedPlayers(false, false, true);
  }

  private void EventFloatingMineAttack(AnimEventData.EventData data)
  {
    string stringArg1 = data.stringArgs[0];
    string stringArg2 = data.stringArgs[1];
    Transform node = this.FindNode(stringArg2);
    if (Object.op_Equality((Object) node, (Object) null))
    {
      Log.Error("Not found transform for launch!! name: " + stringArg2);
    }
    else
    {
      AttackInfo attackInfo = this.FindAttackInfo(stringArg1);
      if (attackInfo == null)
      {
        Log.Error("Not found AttackInfo!! name: " + stringArg1);
      }
      else
      {
        BulletData bulletData = attackInfo.bulletData;
        if (Object.op_Equality((Object) bulletData, (Object) null) || bulletData.dataMine == null)
        {
          Log.Error("Not found BulletData!! atkInfoName: " + stringArg1);
        }
        else
        {
          Vector3 vector3;
          // ISSUE: explicit constructor call
          ((Vector3) ref vector3).\u002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
          Quaternion quaternion = Quaternion.Euler(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
          new GameObject("AttackFloatingMine").AddComponent<AttackFloatingMine>().Initialize(new AttackFloatingMine.InitParamFloatingMine()
          {
            attacker = (StageObject) this,
            atkInfo = attackInfo,
            launchTrans = node,
            offsetPos = vector3,
            offsetRot = quaternion
          });
        }
      }
    }
  }

  protected override void EventActionMineAttack(AnimEventData.EventData data)
  {
    if (!this.IsOriginal() && !this.IsCoopNone())
      return;
    this.ActCreateActionMine(data.stringArgs[0]);
  }

  private void ActCreateActionMine(string atkInfoName)
  {
    int randSeed = Random.Range(int.MinValue, int.MaxValue);
    this.ActCreateActionMine(atkInfoName, randSeed);
    if (!Object.op_Inequality((Object) this.enemySender, (Object) null))
      return;
    this.enemySender.OnCreateActionMine(atkInfoName, randSeed);
  }

  public void ActCreateActionMine(string atkInfoName, int randSeed)
  {
    AttackInfo attackInfo = this.FindAttackInfo(atkInfoName);
    if (attackInfo == null)
    {
      Log.Error("Not found AttackInfo!! name:" + atkInfoName);
    }
    else
    {
      BulletData bulletData = attackInfo.bulletData;
      if (Object.op_Equality((Object) bulletData, (Object) null) || bulletData.dataActionMine == null)
      {
        Log.Error("Not found BulletData!! atkInfoName:" + atkInfoName);
      }
      else
      {
        Vector3[] randomPosition = this.GetRandomPosition(bulletData.dataActionMine.settingNum, ((Component) this).transform.position, bulletData.dataActionMine.settingRadius, bulletData.dataActionMine.centerConcentration, bulletData.dataActionMine.settingHeight, bulletData.dataActionMine.settingNearLimit, randSeed);
        Random random = new Random(randSeed);
        for (int index1 = 0; index1 < randomPosition.Length; ++index1)
        {
          AttackActionMine.InitParamActionMine initParam = new AttackActionMine.InitParamActionMine();
          initParam.attacker = (StageObject) this;
          initParam.atkInfo = attackInfo;
          initParam.position = randomPosition[index1];
          initParam.rotation = Quaternion.LookRotation(Vector3.op_Subtraction(((Component) this).transform.position, randomPosition[index1]));
          initParam.randomSeed = randSeed;
          int tmp_id = 0;
          for (int index2 = 0; index2 < 10; ++index2)
          {
            tmp_id = random.Next(1, int.MaxValue);
            if (!this.m_activeAttackActionMineList.Exists((Predicate<AttackActionMine>) (x => x.objId == tmp_id)))
              break;
          }
          initParam.id = tmp_id;
          AttackActionMine attackActionMine = new GameObject("AttackActionMine").AddComponent<AttackActionMine>();
          attackActionMine.Initialize(initParam);
          this.m_activeAttackActionMineList.Add(attackActionMine);
        }
      }
    }
  }

  private Vector3[] GetRandomPosition(
    int num,
    Vector3 center,
    float radius,
    float concentration,
    float height,
    float nearLimit,
    int randomSeed)
  {
    Random random = new Random(randomSeed);
    int num1 = 50;
    List<Vector3> vector3List = new List<Vector3>();
    for (int index1 = 0; index1 < num; ++index1)
    {
      for (int index2 = 0; index2 < num1; ++index2)
      {
        double num2 = (double) radius * (double) Mathf.Pow((float) random.NextDouble(), concentration);
        float num3 = 6.28f * (float) random.NextDouble();
        float num4 = (float) num2 * Mathf.Cos(num3);
        float num5 = (float) num2 * Mathf.Sin(num3);
        Vector3 pos = new Vector3(num4 + center.x, height, num5 + center.z);
        if (MonoBehaviourSingleton<StageManager>.I.CheckPosInside(pos) && !vector3List.Exists((Predicate<Vector3>) (v => (double) Vector3.Distance(v, pos) < (double) nearLimit)) && !this.m_activeAttackActionMineList.Exists((Predicate<AttackActionMine>) (m => (double) Vector3.Distance(((Component) m).transform.position, pos) < (double) nearLimit)))
        {
          vector3List.Add(pos);
          break;
        }
      }
    }
    return vector3List.ToArray();
  }

  protected override void EventReflectBulletAttack(AnimEventData.EventData data)
  {
    if (!this.IsOriginal() && !this.IsCoopNone())
      return;
    string stringArg1 = data.stringArgs[0];
    string stringArg2 = data.stringArgs[1];
    int num = Random.Range(int.MinValue, int.MaxValue);
    this.ActCreateReflectBullet(stringArg1, stringArg2, -1, num);
    if (!Object.op_Inequality((Object) this.enemySender, (Object) null))
      return;
    this.enemySender.OnReflectBulletAttack(stringArg1, stringArg2, num);
  }

  public void ActCreateReflectBullet(string atkInfoName, string nodeName, int objId, int seed)
  {
    this.ActResetActionMineRandom(seed);
    if (objId > 0)
    {
      AttackActionMine attackActionMine = this.m_activeAttackActionMineList.Find((Predicate<AttackActionMine>) (x => x.objId == objId));
      if (!Object.op_Inequality((Object) attackActionMine, (Object) null))
        return;
      attackActionMine.CreateReflectBullet();
    }
    else
    {
      if (string.IsNullOrEmpty(atkInfoName))
        return;
      AttackInfo attackInfo = this.FindAttackInfo(atkInfoName);
      if (attackInfo == null)
      {
        Log.Error("Not found AttackInfo!! name:" + atkInfoName);
      }
      else
      {
        Transform transform = string.IsNullOrEmpty(nodeName) ? this._transform : this.FindNode(nodeName);
        if (Object.op_Equality((Object) transform, (Object) null))
        {
          Log.Error("Not found transform for launch!! name:" + nodeName);
        }
        else
        {
          Vector3 position = transform.position;
          Quaternion reflectBulletRotation = this.GetReflectBulletRotation(position, seed);
          ((Component) AnimEventShot.Create((StageObject) this, attackInfo, position, reflectBulletRotation)).gameObject.AddComponent<AttackActionMine.ReflectBulletCondition>();
        }
      }
    }
  }

  private Quaternion GetReflectBulletRotation(Vector3 nodePos, int randSeed)
  {
    this.m_activeAttackActionMineList.RemoveAll((Predicate<AttackActionMine>) (x => Object.op_Equality((Object) x, (Object) null)));
    AttackActionMine[] array = this.m_activeAttackActionMineList.ToArray();
    Random random = new Random(randSeed);
    if (array.Length != 0)
      return Quaternion.LookRotation(Vector3.op_Subtraction(((Component) array[random.Next(0, this.m_activeAttackActionMineList.Count)]).transform.position, nodePos));
    List<StageObject> stageObjectList = new List<StageObject>((IEnumerable<StageObject>) MonoBehaviourSingleton<StageObjectManager>.I.playerList);
    stageObjectList.RemoveAll((Predicate<StageObject>) (obj =>
    {
      Player player = obj as Player;
      return Object.op_Inequality((Object) player, (Object) null) && player.hp <= 0;
    }));
    stageObjectList.Sort((Comparison<StageObject>) ((a, b) => a.id - b.id));
    return stageObjectList.Count > 0 ? Quaternion.LookRotation(Vector3.op_Subtraction(stageObjectList[random.Next(0, stageObjectList.Count)]._position, nodePos)) : this._rotation;
  }

  public void ActResetActionMineRandom(int seed)
  {
    for (int index = 0; index < this.m_activeAttackActionMineList.Count; ++index)
      this.m_activeAttackActionMineList[index].ResetRandomSeed(seed + index);
  }

  private void EventWeatherChange(AnimEventData.EventData data)
  {
    if (data.floatArgs.Length < 2)
      return;
    MonoBehaviourSingleton<SceneSettingsManager>.I.ChangeWeather(data.floatArgs[0], data.floatArgs[1]);
  }

  private void EventWeatherChangeOff()
  {
    MonoBehaviourSingleton<SceneSettingsManager>.I.WeatherForceReturn = true;
  }

  private void EventRecoverBarrierHp(AnimEventData.EventData data)
  {
    int intArg = data.intArgs[0];
    if (intArg >= 0 && intArg < this.regionInfos.Length)
      return;
    Log.Error("Region Index is out of range!! ");
  }

  private void EventRecoverBarrierHpAll(AnimEventData.EventData data)
  {
    int num = (int) ((double) this.BarrierHpMax * (double) ((float) data.intArgs[0] * 0.01f));
    if (num <= 0)
      return;
    this.m_barrierHp = (XorInt) ((int) this.m_barrierHp + num);
    if ((int) this.m_barrierHp <= this.BarrierHpMax)
      return;
    this.m_barrierHp = (XorInt) this.BarrierHpMax;
  }

  private void EventEnemyRecoverHp(AnimEventData.EventData data)
  {
    int intArg1 = data.intArgs[0];
    if (intArg1 > 0)
      this.RecoverHp(intArg1, false);
    int intArg2 = data.intArgs[1];
    if (intArg2 <= 0)
      return;
    this.RecoverHp((int) ((double) this.hpMax * (double) ((float) intArg2 * 0.01f)), false);
  }

  private void EventEnemyDeadRevive(AnimEventData.EventData data)
  {
    int num1 = data.intArgs[0];
    if (num1 <= 0)
      num1 = 1;
    int num2 = (int) ((double) this.hpMax * (double) ((float) num1 * 0.01f) + 0.0099999997764825821);
    if (num2 > this.hpMax)
      num2 = this.hpMax;
    this.hp = num2;
    this.deadReviveCount = this.actDeadReviveCount;
    if (MonoBehaviourSingleton<UIDamageManager>.IsValid())
      MonoBehaviourSingleton<UIDamageManager>.I.CreateEnemyRecoverHp((Character) this, num2, UIPlayerDamageNum.DAMAGE_COLOR.HEAL);
    if (Object.op_Inequality((Object) this.effectPlayProcessor, (Object) null) && Object.op_Equality((Object) this.effectDrainRecover, (Object) null))
    {
      List<EffectPlayProcessor.EffectSetting> settings = this.effectPlayProcessor.GetSettings("RECOVER_HP");
      if (settings != null && settings.Count > 0)
      {
        Transform transform = this.effectPlayProcessor.PlayEffect(settings[0], this._transform);
        if (Object.op_Inequality((Object) transform, (Object) null))
          this.effectDrainRecover = ((Component) transform).gameObject;
      }
    }
    if (!MonoBehaviourSingleton<InGameRecorder>.IsValid())
      return;
    MonoBehaviourSingleton<InGameRecorder>.I.RecordEnemyRecoveredHP(this.id, num2);
  }

  private void EventShieldON(AnimEventData.EventData data)
  {
    this.ShieldHp = this.ShieldHpMax;
    List<EnemyRegionWork> enemyRegionWorkList = new List<EnemyRegionWork>();
    for (int index = 0; index < this.regionInfos.Length; ++index)
    {
      EnemyRegionWork regionWork = this.regionWorks[index];
      regionWork.isShieldCriticalDamage = false;
      if (regionWork.enabled && !regionWork.regionInfo.isGrabRelease && regionWork.isShieldDamage)
        enemyRegionWorkList.Add(this.regionWorks[index]);
    }
    if (enemyRegionWorkList.Count <= 0)
      return;
    int index1 = 0;
    if (enemyRegionWorkList.Count > 1)
    {
      Random.State state = Random.state;
      Random.InitState(this.SyncRandomSeed);
      index1 = Random.Range(0, enemyRegionWorkList.Count);
      Random.state = state;
    }
    if (MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
      MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestAnnounce(this.enemyTableData.name, STRING_CATEGORY.ENEMY_SHIELD, 0U);
    enemyRegionWorkList[index1].isShieldCriticalDamage = true;
    this.RequestShieldShaderEffect();
  }

  public void RequestShieldShaderEffect() => this.StartCoroutine(this.SetShieldShaderParam());

  private void EventGenerateAegis(AnimEventData.EventData data)
  {
    if (this.aegisCtrl == null)
    {
      this.aegisCtrl = new GameObject("AegisParent").AddComponent<EnemyAegisController>();
      this.aegisCtrl.Init(this);
    }
    if (!Object.op_Inequality((Object) this.aegisCtrl, (Object) null))
      return;
    this.aegisCtrl.Generate(data);
  }

  public EnemyAegisController.SetupParam GetAegisSetupParam()
  {
    return this.aegisCtrl == null ? (EnemyAegisController.SetupParam) null : this.aegisCtrl.GetSetupParam();
  }

  public float GetAegisPercent() => this.aegisCtrl == null ? 0.0f : this.aegisCtrl.GetPercent();

  public void SetupAegis(EnemyAegisController.SetupParam param)
  {
    if (param == null)
      return;
    if (this.aegisCtrl == null)
    {
      this.aegisCtrl = new GameObject("AegisParent").AddComponent<EnemyAegisController>();
      this.aegisCtrl.Init(this);
    }
    this.aegisCtrl.Setup(param, false);
  }

  private void EventEffectLoopCustom(AnimEventData.EventData data)
  {
    string uniqueName = data.stringArgs[0] + data.stringArgs[1];
    foreach (EnemyEffectObject enemyEffect in this.m_enemyEffectList)
    {
      if (enemyEffect.UniqueName == uniqueName)
        return;
    }
    Transform transform = AnimEventFormat.EffectEventExec(data.id, data, this._transform, this.isBoss, new AnimEventFormat.EffectNameAnalyzer(((Character) this).EffectNameAnalyzer), new AnimEventFormat.NodeFinder(((StageObject) this).FindNode));
    if (Object.op_Equality((Object) transform, (Object) null))
      return;
    int intArg1 = data.intArgs[0];
    int intArg2 = data.intArgs[1];
    EnemyEffectObject enemyEffectObject = ((Component) transform).gameObject.AddComponent<EnemyEffectObject>();
    enemyEffectObject.Initialize(this, this.regionWorks[intArg1], intArg2, uniqueName);
    this.m_enemyEffectList.Add(enemyEffectObject);
  }

  public void OnNotifyDeleteEnemyEffect(EnemyEffectObject del)
  {
    if (!Object.op_Inequality((Object) del, (Object) null) || !this.m_enemyEffectList.Contains(del))
      return;
    this.m_enemyEffectList.Remove(del);
  }

  public void SetResidentEffectSetting(SystemEffectSetting setting)
  {
    this.m_residentEffectSetting = setting;
  }

  private void EventGroupEffectON(AnimEventData.EventData data)
  {
    int intArg = data.intArgs[0];
    AnimEventData.ResidentEffectData[] residentEffectDataList = this.animEventData.residentEffectDataList;
    if (residentEffectDataList != null && residentEffectDataList.Length != 0)
    {
      foreach (AnimEventData.ResidentEffectData effectData in residentEffectDataList)
      {
        if (!string.IsNullOrEmpty(effectData.effectName) && !string.IsNullOrEmpty(effectData.linkNodeName) && effectData.groupID == intArg)
        {
          Transform transform = Utility.Find(((Component) this.body).transform, effectData.linkNodeName);
          if (Object.op_Equality((Object) transform, (Object) null))
            transform = ((Component) this.body).transform;
          if (!this.IsExistResidentEffect(effectData.UniqueName))
          {
            Transform effect = EffectManager.GetEffect(effectData.effectName, transform);
            if (Object.op_Inequality((Object) effect, (Object) null))
            {
              Vector3 localScale = effect.localScale;
              effect.localScale = Vector3.op_Multiply(localScale, effectData.scale);
              effect.localPosition = effectData.offsetPos;
              effect.localRotation = Quaternion.Euler(effectData.offsetRot);
              ResidentEffectObject effectObj = ((Component) effect).gameObject.AddComponent<ResidentEffectObject>();
              effectObj.Initialize(effectData);
              this.RegisterResidentEffect(effectObj);
            }
          }
        }
      }
    }
    if (!Object.op_Inequality((Object) this.m_residentEffectSetting, (Object) null))
      return;
    SystemEffectSetting.Data[] effectDataList = this.m_residentEffectSetting.effectDataList;
    if (effectDataList == null || effectDataList.Length == 0)
      return;
    foreach (SystemEffectSetting.Data effectData in effectDataList)
    {
      if (!string.IsNullOrEmpty(effectData.effectName) && !string.IsNullOrEmpty(effectData.linkNodeName) && effectData.groupID == intArg)
      {
        Transform transform = Utility.Find(((Component) this.body).transform, effectData.linkNodeName);
        if (Object.op_Equality((Object) transform, (Object) null))
          transform = ((Component) this.body).transform;
        if (!this.IsExistResidentEffect(effectData.UniqueName))
        {
          Transform effect = EffectManager.GetEffect(effectData.effectName, transform);
          if (Object.op_Inequality((Object) effect, (Object) null))
          {
            Vector3 localScale = effect.localScale;
            effect.localScale = Vector3.op_Multiply(localScale, effectData.scale);
            effect.localPosition = effectData.offsetPos;
            effect.localRotation = Quaternion.Euler(effectData.offsetRot);
            ResidentEffectObject effectObj = ((Component) effect).gameObject.AddComponent<ResidentEffectObject>();
            effectObj.Initialize(effectData);
            this.RegisterResidentEffect(effectObj);
          }
        }
      }
    }
  }

  private void EventGroupEffectOFF(AnimEventData.EventData data)
  {
    int intArg = data.intArgs[0];
    List<ResidentEffectObject> residentEffectObjectList = new List<ResidentEffectObject>();
    foreach (ResidentEffectObject residentEffect in this.m_residentEffectList)
    {
      if (residentEffect.GroupID == intArg)
        residentEffectObjectList.Add(residentEffect);
    }
    foreach (ResidentEffectObject residentEffectObject in residentEffectObjectList)
    {
      EffectManager.ReleaseEffect(((Component) residentEffectObject).gameObject);
      this.m_residentEffectList.Remove(residentEffectObject);
    }
  }

  public void RegisterResidentEffect(ResidentEffectObject effectObj)
  {
    this.m_residentEffectList.Add(effectObj);
  }

  private bool IsExistResidentEffect(string uniqueName)
  {
    foreach (ResidentEffectObject residentEffect in this.m_residentEffectList)
    {
      if (residentEffect.UniqueName == uniqueName)
        return true;
    }
    return false;
  }

  private void EventTailControllON(AnimEventData.EventData data)
  {
    int intArg = data.intArgs[0];
    if (Object.op_Equality((Object) this.tailController, (Object) null))
    {
      Debug.LogError((object) ("Not found TailController!! ID:" + (object) intArg));
    }
    else
    {
      if (this.tailController.UniqueID != intArg)
        return;
      this.tailController.SetUpdateFlag(true);
    }
  }

  private void EventTailControllOFF(AnimEventData.EventData data)
  {
    int num = data.GetInt(0);
    float lerpFinishTime = data.GetFloat(0, 0.8f);
    if (Object.op_Equality((Object) this.tailController, (Object) null))
    {
      Debug.LogError((object) ("Not found TailController!! ID:" + (object) num));
    }
    else
    {
      if (this.tailController.UniqueID != num)
        return;
      this.tailController.SetUpdateFlag(false);
      this.tailController.RequestLerp(lerpFinishTime);
    }
  }

  private void EventRegionNodeActivate(AnimEventData.EventData data)
  {
    if (data.intArgs.Length == 0 || data.stringArgs.Length == 0 || string.IsNullOrEmpty(data.stringArgs[0]) || !this.IsCoopNone() && !this.IsOriginal())
      return;
    bool isRandom = data.intArgs[0] == 1;
    int[] array = ((IEnumerable<string>) data.stringArgs[0].Split(':')).Select<string, int>((Func<string, int>) (a => int.Parse(a))).ToArray<int>();
    int randomSelectedID = isRandom ? array[Random.Range(0, array.Length)] : 0;
    this.ActivateRegionNode(array, isRandom, randomSelectedID);
  }

  private void EventRegionNodeDeactivate(AnimEventData.EventData data)
  {
    if (data.stringArgs.Length == 0 || string.IsNullOrEmpty(data.stringArgs[0]))
      return;
    string stringArg = data.stringArgs[0];
    char[] chArray = new char[1]{ ':' };
    foreach (int num in ((IEnumerable<string>) stringArg.Split(chArray)).Select<string, int>((Func<string, int>) (a => int.Parse(a))).ToArray<int>())
    {
      for (int index = 0; index < this.regionRoots.Length; ++index)
      {
        if (this.regionRoots[index].regionID == num)
          ((Component) this.regionRoots[index]).gameObject.SetActive(false);
      }
    }
  }

  protected override void EventCameraCutOn(AnimEventData.EventData data)
  {
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid() || FieldManager.IsValidInGameNoBoss() || data.floatArgs.Length < 6)
      return;
    Vector3 cutPos;
    // ISSUE: explicit constructor call
    ((Vector3) ref cutPos).\u002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
    Vector3 vector3;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3).\u002Ector(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
    InGameCameraManager i = MonoBehaviourSingleton<InGameCameraManager>.I;
    i.SetCutPos(cutPos);
    i.SetCutRot(Quaternion.Euler(vector3));
    i.SetCameraMode(InGameCameraManager.CAMERA_MODE.CUT);
  }

  protected override void EventCameraCutOff()
  {
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
      return;
    MonoBehaviourSingleton<InGameCameraManager>.I.ClearCameraMode(InGameCameraManager.CAMERA_MODE.CUT);
  }

  private void EventSummonEnemy(AnimEventData.EventData data)
  {
    int summonedServantId = this.GetSummonedServantId();
    int intArg1 = data.intArgs[0];
    int intArg2 = data.intArgs[1];
    if (intArg2 == 0 && data.intArgs[2] > 0)
      intArg2 = Mathf.FloorToInt((float) ((int) this.enemyLevel / data.intArgs[2]));
    Vector3 vector3;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3).\u002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
    Quaternion quaternion = Quaternion.Euler(new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]));
    if (data.intArgs.Length > 3 && data.intArgs[3] != 0)
    {
      vector3 = Vector3.op_Addition(this._position, Quaternion.op_Multiply(this._rotation, vector3));
      quaternion = Quaternion.op_Multiply(this._rotation, quaternion);
    }
    if (!MonoBehaviourSingleton<StageManager>.I.CheckPosInside(vector3))
      return;
    MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemyWithAI(summonedServantId, vector3, ((Quaternion) ref quaternion).eulerAngles.y, intArg1, intArg2, false, false, (EnemyLoader.OnCompleteLoad) (enemy =>
    {
      string summonEffectName = "ef_btl_enm_summon_01";
      if (data.stringArgs != null && data.stringArgs.Length >= 1 && !string.IsNullOrEmpty(data.stringArgs[0]))
        summonEffectName = data.stringArgs[0];
      MonoBehaviourSingleton<StageObjectManager>.I.ShowEnemyFromUnderGroundForSummon(enemy, summonEffectName);
      if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
        MonoBehaviourSingleton<InGameRecorder>.I.RecordEnemyHP(enemy.id, enemy.hpMax);
      if (this.IsOriginal())
        enemy.SetCoopMode(StageObject.COOP_MODE_TYPE.ORIGINAL, 0);
      else if (this.IsMirror())
        enemy.SetCoopMode(StageObject.COOP_MODE_TYPE.MIRROR, this.coopClientId);
      if (!MonoBehaviourSingleton<CoopManager>.IsValid() || !MonoBehaviourSingleton<CoopManager>.I.isStageHost)
        return;
      MonoBehaviourSingleton<CoopManager>.I.coopStage.SendSyncPlayerRecord(0, false);
    }));
  }

  private void EventSummonAttack(AnimEventData.EventData data)
  {
    if (!this.IsCoopNone() && !this.IsOriginal())
      return;
    int intArg1 = data.intArgs[0];
    int intArg2 = data.intArgs[1];
    Vector3 pos;
    // ISSUE: explicit constructor call
    ((Vector3) ref pos).\u002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
    Vector3 rot;
    // ISSUE: explicit constructor call
    ((Vector3) ref rot).\u002Ector(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
    if (data.intArgs[2] == 1)
    {
      pos = Vector3.op_Addition(this._transform.position, Quaternion.op_Multiply(Quaternion.Euler(this._transform.eulerAngles), pos));
      rot = Vector3.op_Addition(rot, this._transform.eulerAngles);
    }
    this.ActSummonAttack(intArg1, intArg2, pos, rot);
  }

  public void ActSummonAttack(int enemyId, int attackId, Vector3 pos, Vector3 rot)
  {
    MonoBehaviourSingleton<StageObjectManager>.I.CreateEnemyForSummonAttack(this.GetSummonedServantId(), pos, rot.y, enemyId, (int) this.enemyLevel, false, (EnemyLoader.OnCompleteLoad) (enemy =>
    {
      ((Component) enemy).gameObject.SetActive(true);
      enemy.SetActionTarget(this.actionTarget, false);
      enemy.ActAttack(attackId, false);
      enemy.hitOffFlag |= StageObject.HIT_OFF_FLAG.INVICIBLE;
    }));
    if (!Object.op_Inequality((Object) this.enemySender, (Object) null))
      return;
    int id = Object.op_Inequality((Object) this.actionTarget, (Object) null) ? this.actionTarget.id : 0;
    this.enemySender.OnSummonAttack(enemyId, attackId, pos, rot, id);
  }

  private int GetSummonedServantId()
  {
    int summonedServantId = this.enemyServantId;
    ++this.enemyServantId;
    if (summonedServantId > 499999)
      summonedServantId = this.enemyServantId = 490000;
    return summonedServantId;
  }

  public override void OnAnimEvent(AnimEventData.EventData data)
  {
    switch (data.id)
    {
      case AnimEventFormat.ID.HIDE_BASE_EFFECT_ON:
        this.EventHideBaseEffectON(data);
        break;
      case AnimEventFormat.ID.HIDE_BASE_EFFECT_OFF:
        this.EventHideBaseEffectOFF(data);
        break;
      case AnimEventFormat.ID.EFFECT_LOOP_CUSTOM:
        this.EventEffectLoopCustom(data);
        break;
      case AnimEventFormat.ID.GROUP_EFFECT_ON:
        this.EventGroupEffectON(data);
        break;
      case AnimEventFormat.ID.GROUP_EFFECT_OFF:
        this.EventGroupEffectOFF(data);
        break;
      case AnimEventFormat.ID.REVIVE_REGION:
        this.EventReviveRegion(data);
        break;
      case AnimEventFormat.ID.DASH_START:
        this.EventDashStart(data);
        break;
      case AnimEventFormat.ID.WARP_VIEW_START:
        this.EventWarpViewStart(data);
        break;
      case AnimEventFormat.ID.WARP_VIEW_END:
        this.EventWarpViewEnd(data);
        break;
      case AnimEventFormat.ID.WARP_TO_TARGET:
        this.EventWarpToTarget(data);
        break;
      case AnimEventFormat.ID.WARP_TO_REVERSE_TARGET:
        this.EventWarpToReverseTarget(data);
        break;
      case AnimEventFormat.ID.WARP_TO_RANDOM:
        this.EventWarpToRandom(data);
        break;
      case AnimEventFormat.ID.RADIAL_BLUR_START:
        if (QuestManager.IsValidInGameWaveMatch())
          break;
        this.EventRadialBlurStart(data);
        break;
      case AnimEventFormat.ID.RADIAL_BLUR_CHANGE:
        if (QuestManager.IsValidInGameWaveMatch())
          break;
        this.EventRadialBlurChange(data);
        break;
      case AnimEventFormat.ID.RADIAL_BLUR_END:
        if (QuestManager.IsValidInGameWaveMatch())
          break;
        this.EventRadialBlurEnd(data);
        break;
      case AnimEventFormat.ID.SHOT_TARGET:
        this.EventShotTarget(data);
        break;
      case AnimEventFormat.ID.SHOT_POINT:
        this.EventShotPoint(data);
        break;
      case AnimEventFormat.ID.WEAKPOINT_ON:
        this.EventWeakPointON(data);
        break;
      case AnimEventFormat.ID.WEAKPOINT_OFF:
        this.EventWeakPointOFF(data);
        break;
      case AnimEventFormat.ID.WEAKPOINT_ALL_ON:
        this.EventWeakPointAllON(data);
        break;
      case AnimEventFormat.ID.WEAKPOINT_ALL_OFF:
        this.EventWeakPointAllOFF(data);
        break;
      case AnimEventFormat.ID.FUNNEL_ATTACK:
        this.EventFunnelAttack(data);
        break;
      case AnimEventFormat.ID.CANCEL_ACTION:
        this.EventCancelAction(data);
        break;
      case AnimEventFormat.ID.RELEASE_GRAB:
        this.EventReleaseGrab(data);
        break;
      case AnimEventFormat.ID.FLOATING_MINE_ATTACK:
        this.EventFloatingMineAttack(data);
        break;
      case AnimEventFormat.ID.WEATHER_CHANGE:
        this.EventWeatherChange(data);
        break;
      case AnimEventFormat.ID.WEATHER_CHANGE_OFF:
        this.EventWeatherChangeOff();
        break;
      case AnimEventFormat.ID.SHOT_RANDOM_AUTO:
        this.KeepRandomShot(data);
        break;
      case AnimEventFormat.ID.RECOVER_BARRIER_HP:
        this.EventRecoverBarrierHp(data);
        break;
      case AnimEventFormat.ID.RECOVER_BARRIER_HP_ALL:
        this.EventRecoverBarrierHpAll(data);
        break;
      case AnimEventFormat.ID.SHIELD_ON:
        this.EventShieldON(data);
        break;
      case AnimEventFormat.ID.DAMAGE_SHAKE_ON:
        this.canHitShockEffect = true;
        break;
      case AnimEventFormat.ID.DAMAGE_SHAKE_OFF:
        this.canHitShockEffect = false;
        break;
      case AnimEventFormat.ID.UNDEAD_ATTACK:
        this.EventUndeadAttack(data);
        break;
      case AnimEventFormat.ID.ICE_FLOOR_CREATE:
        this.EventCreateIceFloor(data);
        break;
      case AnimEventFormat.ID.DIG_ATTACK:
        this.EventDigAttack(data);
        break;
      case AnimEventFormat.ID.ACTION_MINE_ATTACK:
        this.EventActionMineAttack(data);
        break;
      case AnimEventFormat.ID.TAIL_CONTROL_ON:
        this.EventTailControllON(data);
        break;
      case AnimEventFormat.ID.TAIL_CONTROL_OFF:
        this.EventTailControllOFF(data);
        break;
      case AnimEventFormat.ID.ACTION_MODE_ID_CHANGE:
        this.EventActionModeIdChange(data);
        break;
      case AnimEventFormat.ID.ANIMATION_LAYER_WEIGHT:
        this.AnimationLayerWeightChange(data);
        break;
      case AnimEventFormat.ID.ELEMENT_CHANGE:
        this.EventElementToleranceChange(data);
        break;
      case AnimEventFormat.ID.BLEND_COLOR_CHANGE:
        this.EventBlendColorChange(data);
        break;
      case AnimEventFormat.ID.SHOT_NODE_LINK:
        this.EventObstacleNodeLinkAttack(data);
        break;
      case AnimEventFormat.ID.TARGET_CHANGE_HATE_RANKING:
        this.EventTargetChangeHateRanking(data);
        break;
      case AnimEventFormat.ID.ELEMENT_ICON_CHANGE:
        this.EventElementIconChange(data);
        break;
      case AnimEventFormat.ID.WEAK_ELEMENT_ICON_CHANGE:
        this.EventWeakElementIconChange(data);
        break;
      case AnimEventFormat.ID.REGION_COLLIDER_ATK_HIT_ON:
        this.EventRegionColliderAtkHitOn(data);
        break;
      case AnimEventFormat.ID.REGION_COLLIDER_ATK_HIT_OFF:
        this.EventRegionColliderAtkHitOff(data);
        break;
      case AnimEventFormat.ID.COUNTER_ENABLED_ON:
        this.EventCounterEnabledOn(data);
        break;
      case AnimEventFormat.ID.COUNTER_ENABLED_OFF:
        this.EventCounterEnabledOff(data);
        break;
      case AnimEventFormat.ID.BUFF_CANCELLATION:
        this.EventBuffCancellation(data);
        break;
      case AnimEventFormat.ID.DAMAGE_TO_ENDURANCE:
        this.EventDamageToEndurance(data);
        break;
      case AnimEventFormat.ID.SHOT_WORLD_POINT:
        this.EventShotWorldPoint(data);
        break;
      case AnimEventFormat.ID.SYNC_ACTION_TARGET:
        this.EventSyncActionTarget(data);
        break;
      case AnimEventFormat.ID.SKIP_TO_SKILL_ACTION_ON:
        this.isAbleToSkipAction = true;
        break;
      case AnimEventFormat.ID.SKIP_TO_SKILL_ACTION_OFF:
        this.isAbleToSkipAction = false;
        break;
      case AnimEventFormat.ID.GENERATE_AEGIS:
        this.EventGenerateAegis(data);
        break;
      case AnimEventFormat.ID.BLEND_COLOR_ON:
        this.EventBlendColorEnable(data, true);
        break;
      case AnimEventFormat.ID.BLEND_COLOR_OFF:
        this.EventBlendColorEnable(data, false);
        break;
      case AnimEventFormat.ID.REGION_NODE_ACTIVATE:
        this.EventRegionNodeActivate(data);
        break;
      case AnimEventFormat.ID.REGION_NODE_DEACTIVATE:
        this.EventRegionNodeDeactivate(data);
        break;
      case AnimEventFormat.ID.ENEMY_RECOVER_HP:
        this.EventEnemyRecoverHp(data);
        break;
      case AnimEventFormat.ID.SUMMON_ENEMY:
        this.EventSummonEnemy(data);
        break;
      case AnimEventFormat.ID.SUMMON_ATTACK:
        this.EventSummonAttack(data);
        break;
      case AnimEventFormat.ID.ENEMY_DEAD_REVIVE:
        this.EventEnemyDeadRevive(data);
        break;
      case AnimEventFormat.ID.ENEMY_ASSIMILATION:
        this.enableAssimilation = true;
        break;
      case AnimEventFormat.ID.ENEMY_DISSIMILATION:
        this.enableAssimilation = false;
        break;
      case AnimEventFormat.ID.SKIP_BY_DAMAGE_ON:
        this.enableToSkipActionByDamage = true;
        break;
      case AnimEventFormat.ID.SKIP_BY_DAMAGE_OFF:
        this.enableToSkipActionByDamage = false;
        break;
      default:
        base.OnAnimEvent(data);
        break;
    }
  }

  private void SetBaseEffecActivateFlag(bool flag)
  {
    if (Object.op_Equality((Object) this.loader, (Object) null))
      return;
    if (Object.op_Equality((Object) this.loader.baseEffect, (Object) null))
      Log.Warning("Not found baseEffect!!");
    else
      ((Component) this.loader.baseEffect).gameObject.SetActive(flag);
  }

  public void SetWarpVisible(float rate)
  {
    if (this.loader.materialParamsList == null)
      return;
    rate = Mathf.Clamp(rate, 0.0f, 1f);
    int ID_VANISH_FLAG = Shader.PropertyToID("_Vanish_flag");
    int ID_VANISH_RATE = Shader.PropertyToID("_Vanish_rate");
    this.loader.materialParamsList.ForEach((Action<EnemyLoader.MaterialParams>) (prm =>
    {
      if (prm.hasVanishFlag)
      {
        float num = (double) rate <= 0.0 ? 0.0f : 1f;
        prm.material.SetFloat(ID_VANISH_FLAG, num);
      }
      if (!prm.hasVanishRate)
        return;
      float num1 = 1f - rate;
      prm.material.SetFloat(ID_VANISH_RATE, num1);
    }));
    if ((double) rate <= 0.0)
    {
      this.hitOffFlag &= ~StageObject.HIT_OFF_FLAG.INVICIBLE;
      this.enableTargetPoint = true;
      if (Object.op_Inequality((Object) this.loader.shadow, (Object) null))
        ((Component) this.loader.shadow).gameObject.SetActive(true);
      ((Component) this._transform).GetComponentsInChildren<rymFX>(Temporary.fxList);
      int index1 = 0;
      for (int count = Temporary.fxList.Count; index1 < count; ++index1)
        this.SetWarpInvisibleEffect(Temporary.fxList[index1], false);
      Temporary.fxList.Clear();
      int index2 = 0;
      for (int length = this.regionWorks.Length; index2 < length; ++index2)
      {
        int index3 = 0;
        for (int count = this.regionWorks[index2].bleedWorkList.Count; index3 < count; ++index3)
        {
          Enemy.BleedWork bleedWork = this.regionWorks[index2].bleedWorkList[index3];
          if (Object.op_Inequality((Object) bleedWork.bleedEffect, (Object) null))
            ((Component) bleedWork.bleedEffect).GetComponent<EffectCtrl>()?.Pause(false);
        }
        if (this.regionWorks[index2].shadowSealingEffect != null)
          ((Component) this.regionWorks[index2].shadowSealingEffect).GetComponent<EffectCtrl>()?.Pause(false);
      }
    }
    else
    {
      this.hitOffFlag |= StageObject.HIT_OFF_FLAG.INVICIBLE;
      this.enableTargetPoint = false;
      if (Object.op_Inequality((Object) this.loader.shadow, (Object) null))
        ((Component) this.loader.shadow).gameObject.SetActive(false);
      ((Component) this._transform).GetComponentsInChildren<rymFX>(Temporary.fxList);
      int index4 = 0;
      for (int count = Temporary.fxList.Count; index4 < count; ++index4)
        this.SetWarpInvisibleEffect(Temporary.fxList[index4], true);
      Temporary.fxList.Clear();
      int index5 = 0;
      for (int length = this.regionWorks.Length; index5 < length; ++index5)
      {
        int index6 = 0;
        for (int count = this.regionWorks[index5].bleedWorkList.Count; index6 < count; ++index6)
        {
          Enemy.BleedWork bleedWork = this.regionWorks[index5].bleedWorkList[index6];
          if (Object.op_Inequality((Object) bleedWork.bleedEffect, (Object) null))
            ((Component) bleedWork.bleedEffect).GetComponent<EffectCtrl>()?.Pause(true);
        }
        if (this.regionWorks[index5].shadowSealingEffect != null)
          ((Component) this.regionWorks[index5].shadowSealingEffect).GetComponent<EffectCtrl>()?.Pause(true);
      }
    }
  }

  private void SetWarpInvisibleEffect(rymFX rym_fx, bool invisible)
  {
    if (Object.op_Equality((Object) rym_fx, (Object) null))
      return;
    int num = invisible ? 1 : 0;
    if (rym_fx.InvisibleFlags == num)
      return;
    rym_fx.InvisibleFlags = num;
    ((Component) rym_fx).gameObject.GetComponentsInChildren<Renderer>(Temporary.rendererList);
    int index = 0;
    for (int count = Temporary.rendererList.Count; index < count; ++index)
      Temporary.rendererList[index].enabled = !invisible;
    Temporary.rendererList.Clear();
  }

  public void SetWarpToTarget(float warp_distance, bool reverse = false)
  {
    if (this.IsOriginal() || this.IsCoopNone())
    {
      Vector3 check_pos = this._position;
      if (Object.op_Inequality((Object) this.actionTarget, (Object) null))
      {
        Vector3 position = this._position;
        position.y = 0.0f;
        Vector3 targetPosition = this.GetTargetPosition(this.actionTarget);
        targetPosition.y = 0.0f;
        if (Vector3.op_Equality(targetPosition, position))
        {
          check_pos = position;
        }
        else
        {
          Vector3 vector3_1 = Vector3.op_Subtraction(targetPosition, position);
          float magnitude = ((Vector3) ref vector3_1).magnitude;
          Vector3 vector3_2 = Vector3.op_Division(vector3_1, magnitude);
          float num;
          if (reverse)
          {
            vector3_2 = Vector3.op_UnaryNegation(vector3_2);
            num = warp_distance;
          }
          else
          {
            num = magnitude - warp_distance;
            if ((double) num < 0.0)
              num = 0.0f;
          }
          while (true)
          {
            do
            {
              check_pos = Vector3.op_Addition(position, Vector3.op_Multiply(vector3_2, num));
              if ((double) num > 0.0 && !MonoBehaviourSingleton<StageManager>.I.CheckPosInside(check_pos))
                num -= 0.5f;
              else
                goto label_11;
            }
            while ((double) num >= 0.0);
            num = 0.0f;
          }
        }
      }
label_11:
      if (Vector3.op_Inequality(this._position, check_pos))
      {
        Vector3 vector3 = Vector3.op_Subtraction(check_pos, this._position);
        vector3.y = 0.0f;
        if (reverse)
          vector3 = Vector3.op_UnaryNegation(vector3);
        this._rotation = Quaternion.LookRotation(vector3);
      }
      this._position = check_pos;
      this.SetWarp();
    }
    else
    {
      if (this.warpWaitSync)
        return;
      this.warpWaitSync = true;
      this.StartWaitingPacket(StageObject.WAITING_PACKET.ENEMY_WARP, false);
    }
  }

  public void SetWarpToRandom(float warpMax, float warpMin)
  {
    if (this.IsOriginal() || this.IsCoopNone())
    {
      if (Object.op_Equality((Object) (this._collider as SphereCollider), (Object) null))
        return;
      if ((double) warpMin > (double) warpMax)
        warpMin = warpMax;
      Vector3 position = this._position;
      position.y = 0.0f;
      bool valid = false;
      Vector3 randomPosByInsideInfo = MonoBehaviourSingleton<StageManager>.I.GetRandomPosByInsideInfo(position, warpMax, warpMin, ref valid);
      if (!valid)
        Log.Error(LOG.INGAME, "Enemy.SetWarpToRandom() position is failed. from_pos : {0}, warp_max : {1}, warp_min : {2}", (object) ((Vector3) ref position).ToString("F1"), (object) warpMax, (object) warpMin);
      this._position = randomPosByInsideInfo;
      if (Vector3.op_Inequality(position, randomPosByInsideInfo))
      {
        Vector3 vector3 = Vector3.op_Subtraction(randomPosByInsideInfo, position);
        vector3.y = 0.0f;
        this._rotation = Quaternion.LookRotation(vector3);
      }
      this.SetWarp();
    }
    else
    {
      if (this.warpWaitSync)
        return;
      this.warpWaitSync = true;
      this.StartWaitingPacket(StageObject.WAITING_PACKET.ENEMY_WARP, false);
    }
  }

  public void SetWarp()
  {
    this.SetNextTrigger();
    this.EndWaitingPacket(StageObject.WAITING_PACKET.ENEMY_WARP);
    this.warpWaitSync = false;
    if (!Object.op_Inequality((Object) this.enemySender, (Object) null))
      return;
    this.enemySender.OnSetWarp();
  }

  public void TargetRandamShotEvent(List<Enemy.RandomShotInfo.TargetInfo> targets)
  {
    bool flag = false;
    Enemy.RandomShotInfo randomShotInfo;
    if (this.shotEventInfoQueue.Count > 0)
    {
      randomShotInfo = this.shotEventInfoQueue[0];
      this.shotEventInfoQueue.Remove(randomShotInfo);
      flag = true;
    }
    else
      randomShotInfo = new Enemy.RandomShotInfo();
    randomShotInfo.targets = targets;
    if (flag)
      this.randomShotInfo.Add(randomShotInfo);
    else
      this.shotNetworkInfoQueue.Add(randomShotInfo);
    if (!Object.op_Inequality((Object) this.enemySender, (Object) null))
      return;
    this.enemySender.TargetRandamShotEvent(targets);
  }

  public void PointRandamShotEvent(List<Vector3> points)
  {
    bool flag = false;
    Enemy.RandomShotInfo randomShotInfo;
    if (this.shotEventInfoQueue.Count > 0)
    {
      randomShotInfo = this.shotEventInfoQueue[0];
      this.shotEventInfoQueue.Remove(randomShotInfo);
      flag = true;
    }
    else
      randomShotInfo = new Enemy.RandomShotInfo();
    randomShotInfo.points = points;
    if (flag)
      this.randomShotInfo.Add(randomShotInfo);
    else
      this.shotNetworkInfoQueue.Add(randomShotInfo);
    if (!Object.op_Inequality((Object) this.enemySender, (Object) null))
      return;
    this.enemySender.TargetRandamShotEvent(points);
  }

  private void UpdateRandomShot()
  {
    int count = this.randomShotInfo.Count;
    if (count <= 0)
      return;
    for (int index = 0; index < count; ++index)
    {
      Enemy.RandomShotInfo randomShotInfo = this.randomShotInfo[index];
      randomShotInfo.countTime -= Time.deltaTime;
      if (randomShotInfo.targets.Count > randomShotInfo.shotCount)
      {
        randomShotInfo.countTime -= Time.deltaTime;
        if ((double) randomShotInfo.countTime <= 0.0)
        {
          AnimEventShot.Create((StageObject) this, randomShotInfo.atkInfo, randomShotInfo.points[randomShotInfo.shotCount], randomShotInfo.targets[randomShotInfo.shotCount].rot);
          int targetId = randomShotInfo.targets[randomShotInfo.shotCount].targetId;
          if (targetId != -1)
            MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(targetId);
          ++randomShotInfo.shotCount;
          randomShotInfo.countTime = randomShotInfo.interval;
        }
      }
      else
      {
        this.randomShotInfo.RemoveAt(index);
        --count;
        --index;
      }
    }
  }

  public override void OnFailedWaitingPacket(StageObject.WAITING_PACKET type)
  {
    switch (type)
    {
      case StageObject.WAITING_PACKET.ENEMY_WARP:
        this.ActIdle();
        break;
      case StageObject.WAITING_PACKET.ENEMY_UPDATE_BLEED_DAMAGE:
        this.ClearBleedDamageAll();
        break;
      case StageObject.WAITING_PACKET.ENEMY_UPDATE_SHADOWSEALING:
        this.ClearShadowSealingAll();
        break;
      case StageObject.WAITING_PACKET.ENEMY_UPDATE_BOMBARROW:
        this.ClearBombArrowAll();
        break;
    }
    base.OnFailedWaitingPacket(type);
  }

  public override Vector3 GetTargetPosition(StageObject target)
  {
    return Object.op_Equality((Object) target, (Object) null) ? Vector3.zero : target.GetPredictivePosition();
  }

  public bool isValidPush() => !this.isLoading && !this.isDead && !this.isBoss;

  public override void ApplySyncPosition(Vector3 pos, float dir, bool force_sync = false)
  {
    this._rotation = Quaternion.AngleAxis(dir, Vector3.up);
    if (!force_sync && !this.isBoss)
    {
      float enemiesPositionMargin = this.enemyParameter.lesserEnemiesPositionMargin;
      Vector3 vector3 = Vector3.op_Subtraction(this._position, pos);
      if ((double) ((Vector3) ref vector3).sqrMagnitude < (double) enemiesPositionMargin * (double) enemiesPositionMargin)
        return;
    }
    this._position = pos;
  }

  public void SetAppearPosEnemy()
  {
    if (this.enemyPopIndex >= 0 && FieldManager.IsValidInGame())
    {
      FieldMapTable.EnemyPopTableData enemyPopData = Singleton<FieldMapTable>.I.GetEnemyPopData(MonoBehaviourSingleton<FieldManager>.I.currentMapID, this.enemyPopIndex);
      if (enemyPopData == null)
        return;
      bool valid = false;
      Vector3 vector3 = Vector3.zero;
      Quaternion quaternion = Quaternion.identity;
      float num = 0.0f;
      if (enemyPopData.enablePopY)
      {
        this.onTheGround = false;
        num = enemyPopData.popY;
      }
      if (MonoBehaviourSingleton<StageManager>.I.insideColliderData != null && (double) enemyPopData.popRadius < (double) MonoBehaviourSingleton<StageManager>.I.insideColliderData.chipSize * 0.5 * 1.4199999570846558)
      {
        // ISSUE: explicit constructor call
        ((Vector3) ref vector3).\u002Ector(enemyPopData.popX, num, enemyPopData.popZ);
        valid = MonoBehaviourSingleton<StageManager>.I.CheckPosInside(vector3);
      }
      else
      {
        vector3 = MonoBehaviourSingleton<StageManager>.I.GetRandomPosByInsideInfo(new Vector3(enemyPopData.popX, num, enemyPopData.popZ), enemyPopData.popRadius, 0.0f, ref valid);
        if (!this.isHideSpawn)
          quaternion = !enemyPopData.enableRotY ? Quaternion.AngleAxis(Random.value * 360f, Vector3.up) : Quaternion.AngleAxis(enemyPopData.rotY, Vector3.up);
      }
      if (!valid)
        Log.Error(LOG.INGAME, "FieldMapEnemyPop position is failed. mapID:{0} enemyID:{1} popIndex:{2}", (object) enemyPopData.mapID, (object) enemyPopData.enemyID, (object) this.enemyPopIndex);
      if (QuestManager.IsValidInGameDefenseBattle())
      {
        Vector3 bossAppearOffsetPos = MonoBehaviourSingleton<InGameSettingsManager>.I.defenseBattleParam.bossAppearOffsetPos;
        float bossAppearAngleY = MonoBehaviourSingleton<InGameSettingsManager>.I.defenseBattleParam.bossAppearAngleY;
        vector3 = bossAppearOffsetPos;
        quaternion = Quaternion.Euler(0.0f, bossAppearAngleY, 0.0f);
      }
      this.walkSpeedRateFromTable = enemyPopData.GenerateWalkSpeed();
      this._position = vector3;
      this._rotation = quaternion;
      this.SetAppearPos(vector3);
    }
    else
      this.SetAppearPos(Vector3.zero);
  }

  public void SetAppearPosForce(Vector3 pos, FieldMapTable.EnemyPopTableData popData)
  {
    if (popData != null)
      this.walkSpeedRateFromTable = popData.GenerateWalkSpeed();
    this._position = pos;
    this.SetAppearPos(pos);
  }

  private static ELEMENT_TYPE GetWeakType(ELEMENT_TYPE type)
  {
    switch (type)
    {
      case ELEMENT_TYPE.FIRE:
        return ELEMENT_TYPE.SOIL;
      case ELEMENT_TYPE.WATER:
        return ELEMENT_TYPE.FIRE;
      case ELEMENT_TYPE.THUNDER:
        return ELEMENT_TYPE.WATER;
      case ELEMENT_TYPE.SOIL:
        return ELEMENT_TYPE.THUNDER;
      case ELEMENT_TYPE.LIGHT:
        return ELEMENT_TYPE.DARK;
      case ELEMENT_TYPE.DARK:
        return ELEMENT_TYPE.LIGHT;
      default:
        return ELEMENT_TYPE.MAX;
    }
  }

  private static ELEMENT_TYPE GetStrongType(ELEMENT_TYPE type)
  {
    switch (type)
    {
      case ELEMENT_TYPE.FIRE:
        return ELEMENT_TYPE.WATER;
      case ELEMENT_TYPE.WATER:
        return ELEMENT_TYPE.THUNDER;
      case ELEMENT_TYPE.THUNDER:
        return ELEMENT_TYPE.SOIL;
      case ELEMENT_TYPE.SOIL:
        return ELEMENT_TYPE.FIRE;
      case ELEMENT_TYPE.LIGHT:
        return ELEMENT_TYPE.MAX;
      case ELEMENT_TYPE.DARK:
        return ELEMENT_TYPE.MAX;
      default:
        return ELEMENT_TYPE.MAX;
    }
  }

  private static Enemy.EFFECTIVE_TYPE GetEffectiveType(ELEMENT_TYPE attack, ELEMENT_TYPE defense)
  {
    if (ELEMENT_TYPE.MAX == attack || ELEMENT_TYPE.MAX == defense)
      return Enemy.EFFECTIVE_TYPE.NORMAL;
    if (defense == Enemy.GetWeakType(attack))
      return Enemy.EFFECTIVE_TYPE.GOOD;
    return defense == Enemy.GetStrongType(attack) ? Enemy.EFFECTIVE_TYPE.BAD : Enemy.EFFECTIVE_TYPE.NORMAL;
  }

  public void ActReleaseGrabbedPlayers(
    bool isWeakHit,
    bool isSpWeakhit,
    bool forceRelease,
    float angle = 0.0f,
    float power = 0.0f)
  {
    if (!this.isBoss && !this.enemyID.ToString().StartsWith("9944"))
      return;
    EnemyBrain brain = this.controller.brain as EnemyBrain;
    if (Object.op_Equality((Object) brain, (Object) null))
      return;
    GrabController grabController = brain.actionCtrl.grabController;
    if (((grabController.releaseByWeakHit & isWeakHit ? 1 : (grabController.releaseBySpWeakHit & isSpWeakhit ? 1 : 0)) | (forceRelease ? 1 : 0)) == 0)
      return;
    brain.actionCtrl.grabController.ReleaseAll(angle, power);
    if (Object.op_Inequality((Object) this.enemySender, (Object) null))
      this.enemySender.OnReleaseGrabbed(angle, power);
    this.GrabHp = (XorInt) 0;
  }

  private void KeepRandomShot(AnimEventData.EventData data)
  {
    if (!this.IsOriginal() && !this.IsCoopNone())
      return;
    this.StartCoroutine(this.DoShotAutomaticRandom(data.stringArgs[0], data.floatArgs[0], data.floatArgs[1], data.floatArgs[2], data.intArgs[0]));
  }

  private IEnumerator DoShotAutomaticRandom(
    string atkName,
    float interval,
    float duration,
    float range,
    int shotNum)
  {
    if (shotNum <= 0)
      shotNum = 1;
    List<Vector3> pointList = new List<Vector3>(shotNum);
    List<Quaternion> rotList = new List<Quaternion>(shotNum);
    float timer = 0.0f;
    float lastShotTime = 0.0f;
    while ((double) timer < (double) duration && !this.isDead)
    {
      timer += Time.deltaTime;
      if ((double) timer - (double) lastShotTime > (double) interval)
      {
        pointList.Clear();
        for (int index = 0; index < shotNum; ++index)
        {
          Vector3 vector3 = Vector3.op_Multiply(Vector3.forward, Random.Range(3f, range));
          pointList.Add(Vector3.op_Addition(Quaternion.op_Multiply(Quaternion.AngleAxis(Random.Range(0.0f, 360f), Vector3.up), vector3), this._transform.position));
        }
        this.ActShotBullet(atkName, pointList, rotList);
        lastShotTime = timer;
      }
      yield return (object) null;
    }
  }

  public void ActShotBullet(string atkName, List<Vector3> posList, List<Quaternion> rotList)
  {
    AttackInfo attackInfo = this.FindAttackInfo(atkName);
    Vector3 position = this._transform.position;
    for (int index = 0; index < posList.Count; ++index)
    {
      Vector3 pos = Vector3.op_Subtraction(posList[index], position);
      Quaternion rot = Quaternion.identity;
      if (index < rotList.Count)
        rot = rotList[index];
      AnimEventShot.Create((StageObject) this, attackInfo, pos, rot);
    }
    if (!Object.op_Inequality((Object) this.enemySender, (Object) null))
      return;
    this.enemySender.OnShotBullet(atkName, posList, rotList);
  }

  private void EventCreateIceFloor(AnimEventData.EventData data)
  {
    string stringArg = data.stringArgs[0];
    BulletData.BulletIceFloor.TARGETING_TYPE intArg = (BulletData.BulletIceFloor.TARGETING_TYPE) data.intArgs[0];
    List<Vector3> vector3List = new List<Vector3>(4);
    List<Quaternion> quaternionList = new List<Quaternion>(4);
    AttackInfo attackInfo = this.FindAttackInfo(stringArg);
    if (attackInfo == null)
    {
      Log.Error("[CREATE_ICE_FLOOR]Attack info is not found");
    }
    else
    {
      BulletData bulletData = attackInfo.bulletData;
      if (Object.op_Equality((Object) bulletData, (Object) null))
        Log.Error("[CREATE_ICE_FLOOR]BulletData is not found");
      else if (bulletData.dataIceFloor == null)
      {
        Log.Error("[CREATE_ICE_FLOOR]BulletData.IceFloor is NULL");
      }
      else
      {
        List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
        int count = playerList.Count;
        switch (intArg)
        {
          case BulletData.BulletIceFloor.TARGETING_TYPE.NONE:
            Vector3 zero1 = Vector3.zero;
            if (data.floatArgs.Length >= 3)
            {
              // ISSUE: explicit constructor call
              ((Vector3) ref zero1).\u002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
            }
            vector3List.Add(zero1);
            break;
          case BulletData.BulletIceFloor.TARGETING_TYPE.NODE_OFFSET:
            if (data.stringArgs.Length < 2)
            {
              Log.Error("[CREATE_ICE_FLOOR]AnimEventData Node Name is not found");
              return;
            }
            Transform node = this.FindNode(data.stringArgs[1]);
            if (Object.op_Equality((Object) node, (Object) null))
            {
              Log.Error("[CREATE_ICE_FLOOR]AnimEventData Node is not found");
              return;
            }
            Vector3 zero2 = Vector3.zero;
            if (data.floatArgs.Length >= 3)
            {
              // ISSUE: explicit constructor call
              ((Vector3) ref zero2).\u002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
            }
            Vector3 vector3 = Vector3.op_Addition(node.position, zero2);
            vector3List.Add(vector3);
            break;
          case BulletData.BulletIceFloor.TARGETING_TYPE.ALL_PLAYERS:
            for (int index = 0; index < count; ++index)
              vector3List.Add(playerList[index]._position);
            break;
          case BulletData.BulletIceFloor.TARGETING_TYPE.RANDOM_CHOICE:
            vector3List.Add(playerList[Random.Range(0, count)]._position);
            break;
        }
        this.ActCreateIceFloor(bulletData, vector3List, quaternionList);
        if (!Object.op_Inequality((Object) this.enemySender, (Object) null))
          return;
        this.enemySender.OnCreateIceFloor(stringArg, vector3List, quaternionList);
      }
    }
  }

  public void ActCreateIceFloor(
    string attackInfoName,
    List<Vector3> posList,
    List<Quaternion> rotList)
  {
    AttackInfo attackInfo = this.FindAttackInfo(attackInfoName);
    if (attackInfo == null || Object.op_Equality((Object) attackInfo.bulletData, (Object) null))
      return;
    BulletData bulletData = attackInfo.bulletData;
    if (Object.op_Equality((Object) bulletData, (Object) null))
      return;
    this.ActCreateIceFloor(bulletData, posList, rotList);
  }

  public void ActCreateIceFloor(
    BulletData bulletData,
    List<Vector3> posList,
    List<Quaternion> rotList)
  {
    string effectName = bulletData.data.effectName;
    for (int index = 0; index < posList.Count; ++index)
    {
      Transform gameObject = Utility.CreateGameObject(((Object) bulletData).name, MonoBehaviourSingleton<StageObjectManager>.I._transform);
      gameObject.position = posList[index];
      ((Component) gameObject).gameObject.layer = LayerMask.NameToLayer("EnemyBullet");
      Transform effect = EffectManager.GetEffect(effectName, gameObject);
      if (Object.op_Inequality((Object) effect, (Object) null))
        effect.localScale = Vector3.one;
      IceFloor iceFloor = ((Component) gameObject).gameObject.AddComponent<IceFloor>();
      iceFloor.duration = bulletData.dataIceFloor.duration;
      iceFloor.SetCollider(bulletData.data.radius);
      iceFloor.SetEffect(effect);
    }
  }

  public void EventActionModeIdChange(AnimEventData.EventData data)
  {
    EnemyBrain brain = this.controller.brain as EnemyBrain;
    if (Object.op_Equality((Object) brain, (Object) null))
      return;
    EnemyActionController actionCtrl = brain.actionCtrl;
    if (actionCtrl == null)
      return;
    actionCtrl.modeId = data.intArgs[0];
  }

  public void AnimationLayerWeightChange(AnimEventData.EventData data)
  {
    Enemy.AnimationLayerWeightChangeInfo weightChangeInfo = new Enemy.AnimationLayerWeightChangeInfo();
    Animator animator = this.loader.GetAnimator();
    weightChangeInfo.aliveFlag = true;
    weightChangeInfo.layerIndex = data.intArgs[0];
    weightChangeInfo.target = data.floatArgs[1];
    weightChangeInfo.weight = animator.GetLayerWeight(weightChangeInfo.layerIndex);
    weightChangeInfo.forceEndFlag = data.intArgs[1] != 0;
    if ((double) weightChangeInfo.weight == (double) weightChangeInfo.target)
      return;
    weightChangeInfo.spd = (double) weightChangeInfo.weight >= (double) weightChangeInfo.target ? -data.floatArgs[0] : data.floatArgs[0];
    if ((double) weightChangeInfo.spd == 0.0)
    {
      animator.SetLayerWeight(weightChangeInfo.layerIndex, weightChangeInfo.target);
      weightChangeInfo.aliveFlag = false;
    }
    bool flag = false;
    int index = 0;
    for (int count = this.animLayerWeightChangeInfo.Count; index < count; ++index)
    {
      if (this.animLayerWeightChangeInfo[index].layerIndex == weightChangeInfo.layerIndex)
      {
        this.animLayerWeightChangeInfo[index] = weightChangeInfo;
        flag = true;
        break;
      }
    }
    if (flag)
      return;
    this.animLayerWeightChangeInfo.Add(weightChangeInfo);
  }

  public void EventTargetChangeHateRanking(AnimEventData.EventData data)
  {
    if (data == null || Object.op_Equality((Object) this.controller, (Object) null) || Object.op_Equality((Object) this.controller.brain, (Object) null))
      return;
    EnemyBrain brain = this.controller.brain as EnemyBrain;
    if (Object.op_Equality((Object) brain, (Object) null) || brain.opponentMem == null || !brain.opponentMem.haveHateControl)
      return;
    int intArg = data.intArgs[0];
    OpponentMemory.OpponentRecord[] hateRankingObjects = this.GetHateRankingObjects(data.intArgs[1] == 0);
    if (hateRankingObjects == null)
      return;
    int index = intArg - 1;
    if (intArg == 0)
      index = Random.Range(0, hateRankingObjects.Length);
    if (0 > index || index >= hateRankingObjects.Length)
      return;
    brain.targetCtrl.SetCurrentTarget(hateRankingObjects[index].obj);
  }

  public void EventElementToleranceChange(AnimEventData.EventData data)
  {
    int intArg1 = data.intArgs[0];
    if (intArg1 >= this.regionInfos.Length)
    {
      Log.Error("Region Index is out of range!! ");
    }
    else
    {
      int intArg2 = data.intArgs[1];
      this.ProcessElementToleranceChange(intArg1, intArg2);
      if (MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
        MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestAnnounce(this.enemyTableData.name, STRING_CATEGORY.ENEMY_REACTION, this.kStrIdx_EnemyReaction_ElemTolChange);
      this.changeToleranceRegionId = intArg1;
      this.changeToleranceScroll = intArg2;
    }
  }

  public void ProcessElementToleranceChange(int regionIndex, int scroll)
  {
    if (scroll < 0)
      return;
    if (regionIndex >= 0)
    {
      this.regionInfos[regionIndex].tolerance.ChangeElementTolerance(scroll);
    }
    else
    {
      int index = 0;
      for (int length = this.regionInfos.Length; index < length; ++index)
        this.regionInfos[index].tolerance.ChangeElementTolerance(scroll);
    }
  }

  public void SetBlendColor(List<BlendColorCtrl.ShaderSyncParam> list)
  {
    if (this.blendColorCtrl == null)
      return;
    this.blendColorCtrl.Sync(this.skinnedMeshRendererList, list);
  }

  public void EventBlendColorChange(AnimEventData.EventData data)
  {
    if (this.blendColorCtrl == null)
      return;
    this.blendColorCtrl.Change(data, this.skinnedMeshRendererList);
  }

  public void EventBlendColorEnable(AnimEventData.EventData data, bool isEnable)
  {
    if (this.blendColorCtrl == null)
      return;
    this.blendColorCtrl.Enable(data, isEnable, this.skinnedMeshRendererList);
  }

  public void EventElementIconChange(AnimEventData.EventData data)
  {
    ELEMENT_TYPE elementType = this.GetElementType();
    ELEMENT_TYPE intArg = (ELEMENT_TYPE) data.intArgs[0];
    MonoBehaviourSingleton<UIEnemyStatus>.I.SetElementIcon(intArg);
    this.changeElementIcon = intArg;
    this.ResetElementDebuff(elementType, this.changeElementIcon);
  }

  public void EventWeakElementIconChange(AnimEventData.EventData data)
  {
    ELEMENT_TYPE intArg = (ELEMENT_TYPE) data.intArgs[0];
    MonoBehaviourSingleton<UIEnemyStatus>.I.SetWeakElementIcon(intArg);
    this.changeWeakElementIcon = intArg;
  }

  public ELEMENT_TYPE GetElementType()
  {
    return this.changeElementIcon != ELEMENT_TYPE.MAX ? this.changeElementIcon : this.GetElementTypeByRegion();
  }

  private void EventRegionColliderAtkHitOn(AnimEventData.EventData data)
  {
    int intArg = data.intArgs[0];
    if (intArg < 0 || intArg >= this.regionInfos.Length)
      Log.Error("Region Index is out of range!! ");
    else
      this.regionInfos[intArg].isAtkColliderHit = true;
  }

  private void EventRegionColliderAtkHitOff(AnimEventData.EventData data)
  {
    int intArg = data.intArgs[0];
    if (intArg < 0 || intArg >= this.regionInfos.Length)
      Log.Error("Region Index is out of range!! ");
    else
      this.regionInfos[intArg].isAtkColliderHit = false;
  }

  public void EventCounterEnabledOn(AnimEventData.EventData data)
  {
    int intArg = data.intArgs[0];
    if (intArg < 0 || intArg >= this.regionInfos.Length)
      Log.Error("Region Index is out of range!! ");
    else
      this.regionInfos[intArg].counterInfo.enabled = true;
  }

  public void EventCounterEnabledOff(AnimEventData.EventData data)
  {
    int intArg = data.intArgs[0];
    if (intArg < 0 || intArg >= this.regionInfos.Length)
      Log.Error("Region Index is out of range!! ");
    else
      this.regionInfos[intArg].counterInfo.enabled = false;
  }

  public void EventBuffCancellation(AnimEventData.EventData data)
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
    int index = 0;
    for (int count = playerList.Count; index < count; ++index)
    {
      Player player = playerList[index] as Player;
      if (!Object.op_Equality((Object) player, (Object) null) && (player.IsCoopNone() || player.IsOriginal()))
        player.OnBuffCancellation();
    }
    if (!MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid() || MonoBehaviourSingleton<StageObjectManager>.I.self.IsInBarrier())
      return;
    MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestAnnounce(string.Empty, STRING_CATEGORY.ENEMY_REACTION, this.kStrIdx_EnemyReaction_BuffCancellation);
  }

  private void EventDamageToEndurance(AnimEventData.EventData data)
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    MonoBehaviourSingleton<InGameProgress>.I.DamageToEndurance(data.intArgs[0]);
  }

  private void EventSyncActionTarget(AnimEventData.EventData data)
  {
    if (!Object.op_Inequality((Object) this.enemySender, (Object) null))
      return;
    this.enemySender.OnEnemySyncTarget(this.actionTarget);
  }

  protected override void EventCameraTargetOffsetOn(AnimEventData.EventData data)
  {
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
      return;
    float[] floatArgs = data.floatArgs;
    InGameCameraManager.TargetOffset targetOffset = new InGameCameraManager.TargetOffset();
    targetOffset.pos = new Vector3(floatArgs[0], floatArgs[1], floatArgs[2]);
    targetOffset.rot = new Vector3(floatArgs[3], floatArgs[4], floatArgs[5]);
    if (floatArgs.Length > 6)
      targetOffset.smoothMaxSpeed = floatArgs[6];
    MonoBehaviourSingleton<InGameCameraManager>.I.SetAnimEventTargetOffsetByEnemy(targetOffset);
  }

  protected override void EventCameraTargetOffsetOff()
  {
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
      return;
    MonoBehaviourSingleton<InGameCameraManager>.I.ClearAnimEventTargetOffsetByEnemy();
  }

  public override void EventCameraTargetRotateOn(AnimEventData.EventData data)
  {
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
      return;
    InGameCameraManager.TargetPosition targetPosition = new InGameCameraManager.TargetPosition();
    if (data.intArgs[0] > 0)
    {
      Vector3 pos = Vector3.zero;
      if (!this.GetTargetPos(out pos))
        return;
      targetPosition.pos = Vector3.op_Addition(pos, new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]));
    }
    else
      targetPosition.pos = Vector3.op_Addition(this._position, new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]));
    if (data.floatArgs.Length > 3)
      targetPosition.smoothMaxSpeed = data.floatArgs[3];
    MonoBehaviourSingleton<InGameCameraManager>.I.SetAnimEventTargetPositionByEnemy(targetPosition);
  }

  public override void EventCameraTargetRotateOff()
  {
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
      return;
    MonoBehaviourSingleton<InGameCameraManager>.I.ClearAnimEventTargetPositionByEnemy();
  }

  protected override void UpdateAction()
  {
    base.UpdateAction();
    switch (this.actionID)
    {
      case Character.ACTION_ID.ATTACK | Character.ACTION_ID.FREEZE:
        this.UpdateDownAction();
        break;
      case (Character.ACTION_ID) 18:
        if ((double) this.m_dizzyTime - (double) Time.time <= 0.0)
          this.SetNextTrigger();
        if (!this.IsPlayingMotion(1))
          break;
        this.OnPlayingEndMotion();
        break;
      case (Character.ACTION_ID) 19:
        this.UpdateDebuffShadowSealingAction();
        break;
      case (Character.ACTION_ID) 22:
        this.UpdateLightRingAction();
        break;
      case (Character.ACTION_ID) 23:
        this.UpdateBindAction();
        break;
      case (Character.ACTION_ID) 25:
        this.UpdateConcussion();
        break;
    }
  }

  public bool HasValidTargetPoint()
  {
    return !this.isDead && this.enableTargetPoint && this.targetPoints != null && this.targetPoints.Length != 0 && !this.isHiding && !this.isSummonAttack;
  }

  public Coop_Model_EnemyInitialize CreateBackup(bool isEndAction = true)
  {
    if (isEndAction)
      this.EndAction();
    Coop_Model_EnemyInitialize model = new Coop_Model_EnemyInitialize();
    this.enemySender.SetupEnemyInitializeModel(model, false, false);
    return model;
  }

  public void InitHide()
  {
    if (!this.isHiding)
      return;
    this.PlayMotion(11, 0.0f);
    this.actionID = Character.ACTION_ID.HIDE;
    this.SetColliderStatuses(false);
    this.viewData = Singleton<FieldMapTable>.I.GetGatherPointViewData(this.gatherPointViewId);
    if (this.viewData == null)
    {
      Log.Error(LOG.INGAME, $"Invalid GatherPointViewID enemyId:{(object) this.enemyID} ViewID:{(object) this.gatherPointViewId}");
    }
    else
    {
      if (string.IsNullOrEmpty(this.viewData.gatherEffectName) || !Object.op_Equality((Object) this.gatherEffect, (Object) null))
        return;
      this.gatherEffect = EffectManager.GetEffect(this.viewData.gatherEffectName, this._transform);
    }
  }

  public void TurnUp()
  {
    this.SetNextTrigger();
    this._TurnUp();
  }

  public void TurnUpImmediate()
  {
    this.ActIdle(transitionTime: 0.0f);
    this._TurnUp();
  }

  private void _TurnUp()
  {
    this.isHiding = false;
    this.enemySender.OnTurnUp();
    this.SetColliderStatuses(true);
    if (Object.op_Inequality((Object) this.targetEffect, (Object) null))
      EffectManager.ReleaseEffect(((Component) this.targetEffect).gameObject);
    if (!Object.op_Inequality((Object) this.gatherEffect, (Object) null))
      return;
    EffectManager.ReleaseEffect(((Component) this.gatherEffect).gameObject);
  }

  public void UpdateGatherTargetMarker(bool isNear)
  {
    if (!this.isHiding)
      return;
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    float num = 0.0f;
    if (Object.op_Inequality((Object) self, (Object) null))
      num = Vector3.Distance(this._transform.position, self._position);
    if (Object.op_Inequality((Object) self, (Object) null) && (double) num <= (double) this.viewData.targetRadius)
    {
      if (Object.op_Equality((Object) this.targetEffect, (Object) null) && !string.IsNullOrEmpty(this.viewData.targetEffectName))
        this.targetEffect = EffectManager.GetEffect(this.viewData.targetEffectName, this._transform);
      if (!Object.op_Inequality((Object) this.targetEffect, (Object) null))
        return;
      Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
      Vector3 position = cameraTransform.position;
      Quaternion rotation = cameraTransform.rotation;
      Vector3 vector3 = Vector3.op_Subtraction(position, this._transform.position);
      this.targetEffect.Set(Vector3.op_Addition(Vector3.op_Addition(Vector3.op_Multiply(((Vector3) ref vector3).normalized, this.viewData.targetEffectShift), Vector3.op_Multiply(Vector3.up, this.viewData.targetEffectHeight)), this._transform.position), rotation);
    }
    else
    {
      if (!Object.op_Inequality((Object) this.targetEffect, (Object) null))
        return;
      EffectManager.ReleaseEffect(((Component) this.targetEffect).gameObject);
    }
  }

  private void SetColliderStatuses(bool enabled)
  {
    this._collider.enabled = enabled;
    if (this.colliders == null)
      return;
    for (int index = 0; index < this.colliders.Length; ++index)
      this.colliders[index].enabled = enabled;
  }

  public override void SafeActIdle()
  {
    if (this.isHiding)
      return;
    base.SafeActIdle();
  }

  public bool IsHideMotionPlaying() => this.IsPlayingMotion(11) || this.IsPlayingMotion(12);

  private bool IsCannonBallHitShieldRegion(EnemyRegionWork regionWork, AttackHitInfo atkInfo)
  {
    return this.IsValidShield() && regionWork.isShieldDamage && atkInfo.attackType == AttackHitInfo.ATTACK_TYPE.CANNON_BALL;
  }

  private bool CheckDisableBuffTypeByShield(BuffParam.BUFFTYPE targetType)
  {
    bool flag = false;
    switch (targetType)
    {
      case BuffParam.BUFFTYPE.POISON:
      case BuffParam.BUFFTYPE.BURNING:
      case BuffParam.BUFFTYPE.DEADLY_POISON:
      case BuffParam.BUFFTYPE.ELECTRIC_SHOCK:
      case BuffParam.BUFFTYPE.EROSION:
      case BuffParam.BUFFTYPE.SOIL_SHOCK:
      case BuffParam.BUFFTYPE.ACID:
      case BuffParam.BUFFTYPE.CORRUPTION:
      case BuffParam.BUFFTYPE.STIGMATA:
      case BuffParam.BUFFTYPE.CYCLONIC_THUNDERSTORM:
        return flag;
      default:
        return flag;
    }
  }

  private void OnBreakShield()
  {
  }

  public EnemyRegionWork SearchShieldCriticalRegionWork()
  {
    for (int index = 0; index < this.regionWorks.Length; ++index)
    {
      if (this.regionWorks[index].isShieldCriticalDamage)
        return this.regionWorks[index];
    }
    return (EnemyRegionWork) null;
  }

  private int CalcShieldDamage(bool isCritical, bool isElementCritical, AttackHitInfo atkInfo)
  {
    int toShieldAtk = atkInfo.toShieldAtk;
    float num = 1f;
    if (isCritical)
      num += atkInfo.toShieldCriticalRate - 1f;
    if (isElementCritical)
      num += atkInfo.toShieldElementCriticalRate - 1f;
    return (int) ((double) toShieldAtk * (double) num);
  }

  private IEnumerator SetShieldShaderParam()
  {
    if (this._rendererArray != null)
    {
      float duration = 1f;
      float matCapPow = 0.0f;
      while ((double) duration > 0.0)
      {
        duration -= Time.deltaTime;
        matCapPow += Time.deltaTime;
        if ((double) matCapPow >= 1.0)
          matCapPow = 1f;
        Utility.MaterialForEach(this._rendererArray, (Action<Material>) (material =>
        {
          if (!material.HasProperty("_MatCapPow"))
            return;
          material.SetFloat("_MatCapPow", matCapPow);
        }));
        yield return (object) null;
      }
    }
  }

  private void ResetShieldShaderParam()
  {
    if (this._rendererArray == null)
      return;
    Utility.MaterialForEach(this._rendererArray, (Action<Material>) (material =>
    {
      if (!material.HasProperty("_MatCapPow"))
        return;
      material.SetFloat("_MatCapPow", 0.0f);
    }));
  }

  public TargetPoint SearchTargetPoint(int regionID)
  {
    int length = this.targetPoints.Length;
    for (int index = 0; index < length; ++index)
    {
      if (this.targetPoints[index].regionID == regionID)
        return this.targetPoints[index];
    }
    return (TargetPoint) null;
  }

  public override bool IsValidLightRing() => this.GetElementType() == ELEMENT_TYPE.DARK;

  public virtual void ActLightRing()
  {
    if (this.IsDebuffShadowSealing() && this.shadowSealingStackDebuff.Contains((Character.ACTION_ID) 22))
      return;
    this.ActReleaseGrabbedPlayers(false, false, true);
    if (this.IsDebuffShadowSealing())
    {
      if (this.shadowSealingStackDebuff.Contains((Character.ACTION_ID) 22))
        return;
      this.shadowSealingStackDebuff.Add((Character.ACTION_ID) 22);
    }
    else
    {
      this.EndAction();
      this.actionID = (Character.ACTION_ID) 22;
      this.PlayMotion(6, (double) this.stopMotionByDebuffNormalizedTime < 0.0 ? -1f : 0.0f);
      if (Object.op_Inequality((Object) this._rigidbody, (Object) null))
        this._rigidbody.velocity = Vector3.zero;
      this.rotateEventKeep = false;
      this.rotateToTargetFlag = false;
      this.rotateEventSpeed = 0.0f;
    }
    this.CreateLightRingEffect();
    InGameSettingsManager.LightRingParam lightRingParam = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.lightRingParam;
    if (lightRingParam.startSeId != 0)
      SoundManager.PlayOneShotSE(lightRingParam.startSeId, this._transform.position);
    if (lightRingParam.loopSeId != 0)
      SoundManager.PlayLoopSE(lightRingParam.loopSeId, (DisableNotifyMonoBehaviour) this, this._transform);
    this.OnActReaction();
    this.lightRingTime = Time.time + lightRingParam.duration;
    if (!MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
      return;
    MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
  }

  protected bool UpdateLightRingAction()
  {
    if ((double) this.lightRingTime - (double) Time.time <= 0.0)
    {
      this.ActLightRingEnd();
      return true;
    }
    float num = 0.1f;
    if ((double) this.stopMotionByDebuffNormalizedTime >= 0.0)
      num = this.stopMotionByDebuffNormalizedTime;
    AnimatorStateInfo animatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
    if ((double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime < (double) num || ((AnimatorStateInfo) ref animatorStateInfo).fullPathHash != Animator.StringToHash("Base Layer.damage") || this.m_isStopMotionByDebuff)
      return false;
    this.setPause(true);
    this.m_isStopMotionByDebuff = true;
    return false;
  }

  protected virtual void ActLightRingEnd()
  {
    if (!this.IsLightRing())
      return;
    this.badStatusMax.lightRing *= 1.5f;
    if (Object.op_Inequality((Object) this.effectLightRing, (Object) null))
    {
      EffectManager.ReleaseEffect(((Component) this.effectLightRing).gameObject);
      this.effectLightRing = (Transform) null;
    }
    if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
      MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
    this.badStatusTotal.lightRing = 0.0f;
    InGameSettingsManager.LightRingParam lightRingParam = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.lightRingParam;
    if (lightRingParam.loopSeId != 0)
      SoundManager.StopLoopSE(lightRingParam.loopSeId, (DisableNotifyMonoBehaviour) this);
    if (lightRingParam.endSeId != 0)
      SoundManager.PlayOneShotSE(lightRingParam.endSeId, this._transform.position);
    if (this.IsDebuffShadowSealing())
    {
      if (!this.shadowSealingStackDebuff.Contains((Character.ACTION_ID) 22))
        return;
      this.shadowSealingStackDebuff.Remove((Character.ACTION_ID) 22);
    }
    else
    {
      this.setPause(false);
      this.m_isStopMotionByDebuff = false;
    }
  }

  public override bool IsLightRing()
  {
    return Object.op_Inequality((Object) this.effectLightRing, (Object) null);
  }

  protected void CreateLightRingEffect()
  {
    if (this.effectLightRing != null)
      return;
    Transform effect = EffectManager.GetEffect("ef_btl_enm_bindring_01", this._transform);
    if (Object.op_Equality((Object) effect, (Object) null) || ((Component) effect).GetComponentsInChildren<ParticleSystem>(true) == null)
      return;
    this.CalcLightRingRadius();
    Transform transform1 = effect;
    transform1.localScale = Vector3.op_Multiply(transform1.localScale, this.lightRingRadius);
    float num = this.lightRingHeight + this.lightRingHeightOffset;
    Transform transform2 = effect;
    transform2.localPosition = Vector3.op_Addition(transform2.localPosition, Vector3.op_Multiply(Vector3.up, num));
    this.effectLightRing = effect;
  }

  private void CalcLightRingRadius()
  {
    if ((double) this.lightRingRadius > 0.0)
      return;
    this.lightRingRadius = 1f;
    this.lightRingHeight = 1f;
    if (Object.op_Equality((Object) this._collider, (Object) null))
      return;
    float num1 = 1f;
    SphereCollider collider1 = this._collider as SphereCollider;
    if (Object.op_Inequality((Object) collider1, (Object) null))
      num1 = collider1.radius;
    CapsuleCollider collider2 = this._collider as CapsuleCollider;
    if (Object.op_Inequality((Object) collider2, (Object) null))
      num1 = collider2.radius;
    if ((double) this.effectLightRingRadiusRate > 0.0)
      num1 = this.effectLightRingRadiusRate;
    float num2 = num1 * this._transform.localScale.x;
    this.lightRingHeight = num2;
    this.lightRingRadius = num2 * 0.33f;
    this.lightRingRadius = Mathf.Clamp(this.lightRingRadius, 0.5f, 1.5f);
  }

  public virtual void ActDebuffShadowSealingStart()
  {
    if (this.IsDebuffShadowSealing())
      return;
    this.EndAction();
    this.ActReleaseGrabbedPlayers(false, false, true);
    float sealingExtendRate = this._GetShadowSealingExtendRate();
    InGameSettingsManager.ShadowSealingParam shadowSealingParam = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.shadowSealingParam;
    this.debuffShadowSealingTimerDuration = shadowSealingParam.duration * this.badStatusMax.shadowSealingBind * sealingExtendRate;
    if ((double) this.debuffShadowSealingTimerDuration < (double) shadowSealingParam.minDuration)
      this.debuffShadowSealingTimerDuration = shadowSealingParam.minDuration;
    this.debuffShadowSealingTimer = this.debuffShadowSealingTimerDuration;
    this.actionID = (Character.ACTION_ID) 19;
    this.PlayMotion(6, (double) this.stopMotionByDebuffNormalizedTime < 0.0 ? -1f : 0.0f);
    this.CreateShadowSealingEffect();
    if (shadowSealingParam.startSeId != 0)
      SoundManager.PlayOneShotSE(shadowSealingParam.startSeId, this._transform.position);
    if (shadowSealingParam.loopSeId != 0)
      SoundManager.PlayLoopSE(shadowSealingParam.loopSeId, (DisableNotifyMonoBehaviour) this, this._transform);
    if (Object.op_Inequality((Object) this._rigidbody, (Object) null))
      this._rigidbody.velocity = Vector3.zero;
    this.rotateEventKeep = false;
    this.rotateToTargetFlag = false;
    this.rotateEventSpeed = 0.0f;
    this.ClearShadowSealingAll(false);
    this.OnActReaction();
    if (!MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
      return;
    MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
  }

  private void CreateShadowSealingEffect()
  {
    this.debuffShadowSealingEffect = EffectManager.GetEffect("ef_btl_wsk_bow_01_04", this._transform);
    if (this.debuffShadowSealingEffect == null)
      return;
    this.CalcShadowSealingEffectRadius();
    Transform shadowSealingEffect = this.debuffShadowSealingEffect;
    shadowSealingEffect.localScale = Vector3.op_Multiply(shadowSealingEffect.localScale, this.debuffShadowSealingRadius);
  }

  private void CalcShadowSealingEffectRadius()
  {
    if ((double) this.debuffShadowSealingRadius > 0.0)
      return;
    this.debuffShadowSealingRadius = 1f;
    if (Object.op_Equality((Object) this._collider, (Object) null))
      return;
    if ((double) this.effectShadowSealingRadiusRate > 0.0)
    {
      this.debuffShadowSealingRadius = this.effectShadowSealingRadiusRate / 4f * this._transform.localScale.x;
    }
    else
    {
      SphereCollider collider1 = this._collider as SphereCollider;
      if (Object.op_Inequality((Object) collider1, (Object) null))
      {
        this.debuffShadowSealingRadius = collider1.radius / 4f * this._transform.localScale.x;
      }
      else
      {
        CapsuleCollider collider2 = this._collider as CapsuleCollider;
        if (!Object.op_Inequality((Object) collider2, (Object) null))
          return;
        this.debuffShadowSealingRadius = collider2.radius / 4f * this._transform.localScale.x;
      }
    }
  }

  protected override float GetFreezeEffectRadiusRate()
  {
    return (double) this.effectFreezeRadiusRate > 0.0 ? this.effectFreezeRadiusRate : base.GetFreezeEffectRadiusRate();
  }

  protected virtual void ActDebuffShadowSealingEnd()
  {
    if (!this.IsDebuffShadowSealing())
      return;
    bool flag1 = true;
    bool flag2 = false;
    if (this.shadowSealingStackDebuff.Contains(Character.ACTION_ID.ATTACK | Character.ACTION_ID.FREEZE))
    {
      this.EndAction();
      this.actionID = Character.ACTION_ID.ATTACK | Character.ACTION_ID.FREEZE;
      this.PlayMotion(this.IsAbleToUseDownTime() ? 118 : 117);
      flag2 = true;
    }
    if (this.shadowSealingStackDebuff.Contains((Character.ACTION_ID) 22))
    {
      if (flag2)
      {
        this.ActLightRingEnd();
      }
      else
      {
        this.EndAction();
        this.actionID = (Character.ACTION_ID) 22;
        this.PlayMotion(6, (double) this.stopMotionByDebuffNormalizedTime < 0.0 ? -1f : 0.0f);
        if (Object.op_Inequality((Object) this._rigidbody, (Object) null))
          this._rigidbody.velocity = Vector3.zero;
        this.rotateEventKeep = false;
        this.rotateToTargetFlag = false;
        this.rotateEventSpeed = 0.0f;
      }
      flag2 = true;
    }
    if (this.shadowSealingStackDebuff.Contains(Character.ACTION_ID.PARALYZE))
    {
      if (flag2)
      {
        this.ActParalyzeEnd();
      }
      else
      {
        this.EndAction();
        this.actionID = Character.ACTION_ID.PARALYZE;
        this.PlayMotion(8);
      }
      flag2 = true;
    }
    if (this.shadowSealingStackDebuff.Contains(Character.ACTION_ID.FREEZE))
    {
      if (flag2)
      {
        this.ActFreezeEnd();
      }
      else
      {
        flag1 = false;
        this.actionID = Character.ACTION_ID.FREEZE;
      }
    }
    if (this.shadowSealingStackDebuff.Contains((Character.ACTION_ID) 23))
    {
      if (flag2)
      {
        this.ActBindEnd();
      }
      else
      {
        this.EndAction();
        this.actionID = (Character.ACTION_ID) 23;
        this.PlayMotion(118);
      }
    }
    if (this.paralyzeEffectTrans != null)
    {
      EffectManager.ReleaseEffect(((Component) this.paralyzeEffectTrans).gameObject);
      this.paralyzeEffectTrans = (Transform) null;
    }
    if (this.debuffShadowSealingEffect != null)
    {
      EffectManager.ReleaseEffect(((Component) this.debuffShadowSealingEffect).gameObject);
      this.debuffShadowSealingEffect = (Transform) null;
    }
    InGameSettingsManager.ShadowSealingParam shadowSealingParam = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.shadowSealingParam;
    if (shadowSealingParam.loopSeId != 0)
      SoundManager.StopLoopSE(shadowSealingParam.loopSeId, (DisableNotifyMonoBehaviour) this);
    if (shadowSealingParam.endSeId != 0)
      SoundManager.PlayOneShotSE(shadowSealingParam.endSeId, this._transform.position);
    this.badStatusMax.shadowSealing *= shadowSealingParam.resistRate;
    this.badStatusMax.shadowSealingBind *= this.shadowSealingBindResist;
    if (flag1)
    {
      this.setPause(false);
      this.m_isStopMotionByDebuff = false;
    }
    this._CheckShadowSealingTask();
    int index = 0;
    for (int length = this.regionWorks.Length; index < length; ++index)
      this.regionWorks[index].shadowSealingData.ownerID = 0;
    if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
      MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
    this.shadowSealingStackDebuff.Clear();
  }

  private void UpdateDebuffShadowSealingAction()
  {
    if (!this.IsDebuffShadowSealing())
      return;
    this.debuffShadowSealingTimer -= Time.deltaTime;
    if ((double) this.debuffShadowSealingTimer <= 0.0)
    {
      this.ActDebuffShadowSealingEnd();
    }
    else
    {
      for (int index = 0; index < this.shadowSealingStackDebuff.Count; ++index)
      {
        bool flag = false;
        switch (this.shadowSealingStackDebuff[index])
        {
          case Character.ACTION_ID.PARALYZE:
            flag = this.UpdateParalyzeAction();
            break;
          case Character.ACTION_ID.FREEZE:
            flag = this.UpdateFreezeAction();
            break;
          case Character.ACTION_ID.ATTACK | Character.ACTION_ID.FREEZE:
            flag = this.UpdateDownAction();
            break;
          case (Character.ACTION_ID) 22:
            flag = this.UpdateLightRingAction();
            break;
          case (Character.ACTION_ID) 23:
            flag = this.UpdateBindAction();
            break;
        }
        if (flag)
          --index;
      }
      float num = 0.1f;
      if ((double) this.stopMotionByDebuffNormalizedTime >= 0.0)
        num = this.stopMotionByDebuffNormalizedTime;
      AnimatorStateInfo animatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
      if ((double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime < (double) num || ((AnimatorStateInfo) ref animatorStateInfo).fullPathHash != Animator.StringToHash("Base Layer.damage") || this.m_isStopMotionByDebuff)
        return;
      this.setPause(true);
      this.m_isStopMotionByDebuff = true;
    }
  }

  public override bool IsDebuffShadowSealing()
  {
    return Object.op_Inequality((Object) this.debuffShadowSealingEffect, (Object) null);
  }

  public float GetDebuffShadowSealingTimeRate()
  {
    return this.debuffShadowSealingTimer / this.debuffShadowSealingTimerDuration;
  }

  public void CountShadowSealingTarget()
  {
    this.shadowSealingTarget = 0;
    if (!this.isBoss || this.regionWorks == null || ((IList<TargetPoint>) this.targetPoints).IsNullOrEmpty<TargetPoint>())
      return;
    int index1 = 0;
    for (int length = this.regionWorks.Length; index1 < length; ++index1)
      this.regionWorks[index1].shadowSealingData.isTarget = false;
    List<int> intList = new List<int>();
    int index2 = 0;
    for (int length = this.targetPoints.Length; index2 < length; ++index2)
    {
      TargetPoint targetPoint = this.targetPoints[index2];
      if (targetPoint.regionID >= 0 && targetPoint.regionID < this.regionWorks.Length)
      {
        EnemyRegionWork regionWork = this.regionWorks[targetPoint.regionID];
        if (!targetPoint.isAimEnable || !targetPoint.param.isTargetEnable)
        {
          int index3 = 0;
          for (int count = regionWork.bleedWorkList.Count; index3 < count; ++index3)
          {
            Enemy.BleedWork bleedWork = regionWork.bleedWorkList[index3];
            if (bleedWork != null && bleedWork.bleedEffect != null)
            {
              EffectManager.ReleaseEffect(((Component) bleedWork.bleedEffect).gameObject);
              bleedWork.bleedEffect = (Transform) null;
            }
          }
          regionWork.bleedList.Clear();
          regionWork.bleedWorkList.Clear();
          regionWork.shadowSealingData.ownerID = 0;
          regionWork.shadowSealingData.existSec = 0.0f;
          regionWork.shadowSealingData.extendRate = 1f;
          if (regionWork.shadowSealingEffect != null)
          {
            EffectManager.ReleaseEffect(((Component) regionWork.shadowSealingEffect).gameObject, false, true);
            regionWork.shadowSealingEffect = (Transform) null;
          }
        }
        else if (!intList.Contains(targetPoint.regionID))
        {
          regionWork.shadowSealingData.isTarget = true;
          intList.Add(targetPoint.regionID);
        }
      }
    }
    this.shadowSealingTarget = intList.Count;
    if (this._CheckShadowSealingFullStuck())
      this.ActDebuffShadowSealingStart();
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid() || !Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
      return;
    MonoBehaviourSingleton<StageObjectManager>.I.self.ResetShadowSealingUI();
  }

  private bool _CheckShadowSealingFullStuck()
  {
    bool flag = false;
    int index = 0;
    for (int length = this.regionWorks.Length; index < length; ++index)
    {
      Enemy.ShadowSealingData shadowSealingData = this.regionWorks[index].shadowSealingData;
      if (shadowSealingData.isTarget)
      {
        flag = true;
        if (shadowSealingData.ownerID == 0)
          return false;
      }
    }
    return flag;
  }

  private void _CheckShadowSealingTask()
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (self == null)
      return;
    int index = 0;
    for (int length = this.regionWorks.Length; index < length; ++index)
    {
      Enemy.ShadowSealingData shadowSealingData = this.regionWorks[index].shadowSealingData;
      if (shadowSealingData.isTarget && shadowSealingData.ownerID == self.id)
      {
        self.taskChecker.OnShadowSealing();
        if (!MonoBehaviourSingleton<InGameManager>.IsValid())
          break;
        MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnShadowSealing();
        break;
      }
    }
  }

  private float _GetShadowSealingExtendRate()
  {
    if (this.regionWorks == null)
      return 1f;
    float sealingExtendRate = 1f;
    int index = 0;
    for (int length = this.regionWorks.Length; index < length; ++index)
    {
      Enemy.ShadowSealingData shadowSealingData = this.regionWorks[index].shadowSealingData;
      if (shadowSealingData.isTarget && shadowSealingData.ownerID != 0 && (double) sealingExtendRate < (double) shadowSealingData.extendRate)
        sealingExtendRate = shadowSealingData.extendRate;
    }
    return sealingExtendRate;
  }

  public int GetShadowSealingStuckNum()
  {
    if (this.regionWorks == null)
      return 0;
    int shadowSealingStuckNum = 0;
    int index = 0;
    for (int length = this.regionWorks.Length; index < length; ++index)
    {
      Enemy.ShadowSealingData shadowSealingData = this.regionWorks[index].shadowSealingData;
      if (shadowSealingData.isTarget && shadowSealingData.ownerID != 0)
        ++shadowSealingStuckNum;
    }
    return shadowSealingStuckNum;
  }

  public int GetShadowSealingNum() => this.shadowSealingTarget;

  public void ResetConcussion(bool isInitialize = false, bool isResistUp = false)
  {
    this.concussionTotal = 0.0f;
    if (isInitialize)
      this.concussionMax = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.concussion.resistBase;
    if (isResistUp)
      this.concussionMax *= MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.concussion.resistRate;
    this.concussionExtend = 1f;
    this.concussionAddPlayerIdList.Clear();
  }

  public bool IsActConcussion() => this.actionID == (Character.ACTION_ID) 25;

  private void CreateConcussionEffect()
  {
    this.concussionEffect = EffectManager.GetEffect("ef_btl_enm_flinch_01", this._transform);
    if (this.concussionEffect == null)
      return;
    this.CalcShadowSealingEffectRadius();
    Transform concussionEffect = this.concussionEffect;
    concussionEffect.localScale = Vector3.op_Multiply(concussionEffect.localScale, this.debuffShadowSealingRadius);
  }

  public void ActConcussionStart()
  {
    if (this.IsConcussion())
      return;
    this.EndAction();
    this.ActReleaseGrabbedPlayers(false, false, true);
    this.actionID = (Character.ACTION_ID) 25;
    InGameSettingsManager.Concussion concussion = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.concussion;
    this.concussionTime = (float) ((double) Time.time + (double) this.downLoopStartTime + (double) concussion.duration * (double) this.concussionExtend);
    this.concussionStartTime = Time.time + this.downLoopStartTime;
    this.PlayMotion(118);
    this.CreateConcussionEffect();
    if (concussion.startSeId != 0)
      SoundManager.PlayOneShotSE(concussion.startSeId, this._transform.position);
    if (concussion.loopSeId != 0)
      SoundManager.PlayLoopSE(concussion.loopSeId, (DisableNotifyMonoBehaviour) this, this._transform);
    this.OnActReaction();
    if (!MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
      return;
    MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
  }

  private void UpdateConcussion()
  {
    if (!this.IsConcussion() || (double) this.concussionTime > (double) Time.time)
      return;
    if ((double) this.downTotal >= (double) this.downMax)
    {
      this.ActDown();
    }
    else
    {
      this.SetNextTrigger();
      this.ActConcussionEnd();
    }
  }

  public void ActConcussionEnd()
  {
    if (!this.IsConcussion())
      return;
    if (this.concussionEffect != null)
    {
      EffectManager.ReleaseEffect(((Component) this.concussionEffect).gameObject);
      this.concussionEffect = (Transform) null;
    }
    InGameSettingsManager.Concussion concussion = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.concussion;
    if (concussion.loopSeId != 0)
      SoundManager.StopLoopSE(concussion.loopSeId, (DisableNotifyMonoBehaviour) this);
    if (concussion.endSeId != 0)
      SoundManager.PlayOneShotSE(concussion.endSeId, this._transform.position);
    if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
      MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
    this.CheckConcussionTask();
    this.ResetConcussion(isResistUp: true);
  }

  public override bool IsConcussion()
  {
    return Object.op_Inequality((Object) this.concussionEffect, (Object) null);
  }

  public float GetConcussionTimeRate()
  {
    return Mathf.Clamp((float) (((double) this.concussionTime - (double) Time.time) / ((double) this.concussionTime - (double) this.concussionStartTime)), 0.0f, 1f);
  }

  private void CheckConcussionTask()
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (self == null || !this.concussionAddPlayerIdList.Contains(self.id))
      return;
    self.taskChecker.OnConcussion();
    if (!MonoBehaviourSingleton<InGameManager>.IsValid())
      return;
    MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnConcussion();
  }

  public OpponentMemory.OpponentRecord[] GetHateRankingObjects(bool isIncludeDead)
  {
    EnemyBrain brain = this.controller.brain as EnemyBrain;
    return Object.op_Inequality((Object) brain, (Object) null) ? brain.opponentMem.GetOpponentWithRankingHate(isIncludeDead) : (OpponentMemory.OpponentRecord[]) null;
  }

  protected bool CheckMadMode(AttackedHitStatusOwner status)
  {
    if (this.madModeHp == 0 || status.isDamageRegionOnly || this.IsValidBuff(BuffParam.BUFFTYPE.MAD_MODE))
      return false;
    int num = this.hp - status.damage;
    if (num > this.madModeHp)
      return false;
    status.damage -= this.madModeHp - num;
    return true;
  }

  private bool CheckDisableBuffTypeByMadMode(BuffParam.BUFFTYPE targetType)
  {
    return this.IsValidBuff(BuffParam.BUFFTYPE.MAD_MODE) && !MonoBehaviourSingleton<InGameSettingsManager>.I.madModeParam.onlyResistDebuff.Contains(targetType) && MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.ignoreBuffCancellation.Contains(targetType);
  }

  public void ActMadMode()
  {
    this.EndAction();
    if (!this.PlayMotion(123))
    {
      this.ActIdle();
    }
    else
    {
      this.actionID = (Character.ACTION_ID) 20;
      this.downTotal = 0.0f;
      this.ResetConcussion();
      this.badStatusTotal.Reset();
      this.ResetBadReaction(MonoBehaviourSingleton<InGameSettingsManager>.I.madModeParam.isClearStuckArrow);
      bool flag = false;
      List<BuffParam.BUFFTYPE> buffCancellation = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.ignoreBuffCancellation;
      int index = 0;
      for (int count = buffCancellation.Count; index < count; ++index)
      {
        if (this.OnBuffEnd(buffCancellation[index], false, true))
          flag = true;
      }
      if (flag)
        this.SendBuffSync();
      if (MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
        MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestAnnounce(this.enemyTableData.name, STRING_CATEGORY.ENEMY_REACTION, this.kStrIdx_EnemyReaction_MadMode);
      this.OnActReaction();
    }
  }

  private void ResetBadReaction(bool clearStuck)
  {
    if (clearStuck)
      this.ClearBleedDamageAll();
    if (this.IsLightRing())
      this.ActLightRingEnd();
    if (this.IsFreeze())
      this.ActFreezeEnd();
    if (!(this.IsDebuffShadowSealing() | clearStuck))
      return;
    this.ClearShadowSealingAll();
  }

  public void CheckFirstMadMode()
  {
    if (!this.IsCoopNone() && !this.IsOriginal() || this.madModeHpThreshold < 100 || (int) this.enemyLevel < this.madModeLvThreshold)
      return;
    this.isFirstMadMode = true;
    this.LocalMadModeStart();
  }

  private void LocalMadModeStart()
  {
    BuffParam.BuffData data = new BuffParam.BuffData();
    data.type = BuffParam.BUFFTYPE.MAD_MODE;
    data.time = -1f;
    data.endless = new bool?(true);
    data.valueType = BuffParam.VALUE_TYPE.CONSTANT;
    data.value = 1;
    this.SetFromInfo(ref data);
    this.OnBuffStart(data);
  }

  public override int GetObservedID()
  {
    string s = (this.id % 500000).ToString() + this.bulletIndex.ToString();
    int observedId = -1;
    ref int local = ref observedId;
    if (!int.TryParse(s, out local))
      return -1;
    ++this.bulletIndex;
    return observedId;
  }

  public override void OnBreak(int brokenBulletID, bool isSendOnlyOrigin)
  {
    if (this.bulletObservableList.IsNullOrEmpty<IBulletObservable>() || this.bulletObservableIdList.IsNullOrEmpty<int>() || !this.bulletObservableIdList.Contains(brokenBulletID))
      return;
    for (int index = 0; index < this.bulletObservableList.Count; ++index)
    {
      if (this.bulletObservableList[index].GetObservedID() == brokenBulletID)
        this.bulletObservableList[index].ForceBreak();
    }
    this.bulletObservableList.RemoveAll((Predicate<IBulletObservable>) (o => o.GetObservedID() == brokenBulletID));
    this.bulletObservableIdList.RemoveAll((Predicate<int>) (o => o == brokenBulletID));
    if (!Object.op_Inequality((Object) this.enemySender, (Object) null))
      return;
    this.enemySender.OnBulletObservableBroken(brokenBulletID, isSendOnlyOrigin);
  }

  public override void OnBulletDestroy(int observedID)
  {
    this.bulletObservableList.RemoveAll((Predicate<IBulletObservable>) (o => o.GetObservedID() == observedID));
    this.bulletObservableIdList.RemoveAll((Predicate<int>) (o => o == observedID));
  }

  public ELEMENT_TYPE GetElementTypeByRegion()
  {
    return Utility.GetEffectiveElementType(this.GetAntiElementTypeByRegion());
  }

  public ELEMENT_TYPE GetAntiElementTypeByRegion()
  {
    if (((IList<Enemy.RegionInfo>) this.regionInfos).IsNullOrEmpty<Enemy.RegionInfo>())
      return ELEMENT_TYPE.MAX;
    Enemy.RegionInfo regionInfo = (Enemy.RegionInfo) null;
    int index = 0;
    for (int length = this.regionInfos.Length; index < length; ++index)
    {
      if (this.regionInfos[index].name == "body")
      {
        regionInfo = this.regionInfos[index];
        break;
      }
    }
    return regionInfo == null ? ELEMENT_TYPE.MAX : regionInfo.tolerance.GetAntiElementType();
  }

  public bool CheckShow(Bounds bound)
  {
    return !MonoBehaviourSingleton<InGameCameraCuller>.IsValid() || MonoBehaviourSingleton<InGameCameraCuller>.I.IsVisible(bound);
  }

  private Bounds getBounds(GameObject objeto)
  {
    Bounds renderBounds = this.getRenderBounds(objeto);
    if ((double) ((Bounds) ref renderBounds).extents.x == 0.0)
    {
      // ISSUE: explicit constructor call
      ((Bounds) ref renderBounds).\u002Ector(objeto.transform.position, Vector3.zero);
      foreach (Transform transform in objeto.transform)
      {
        Renderer component = ((Component) transform).GetComponent<Renderer>();
        if (Object.op_Implicit((Object) component))
          ((Bounds) ref renderBounds).Encapsulate(component.bounds);
        else
          ((Bounds) ref renderBounds).Encapsulate(this.getBounds(((Component) transform).gameObject));
      }
    }
    return renderBounds;
  }

  private Bounds getBounds(GameObject objeto, Renderer[] renders)
  {
    Bounds bounds;
    // ISSUE: explicit constructor call
    ((Bounds) ref bounds).\u002Ector(Vector3.zero, Vector3.zero);
    if (renders != null && renders.Length != 0)
    {
      int index = 0;
      for (int length = renders.Length; index < length; ++index)
      {
        if (Object.op_Inequality((Object) renders[index], (Object) null))
          ((Bounds) ref bounds).Encapsulate(renders[index].bounds);
      }
    }
    return bounds;
  }

  private Bounds getRenderBounds(GameObject objeto)
  {
    Bounds bounds;
    // ISSUE: explicit constructor call
    ((Bounds) ref bounds).\u002Ector(Vector3.zero, Vector3.zero);
    Renderer component = objeto.GetComponent<Renderer>();
    return Object.op_Inequality((Object) component, (Object) null) ? component.bounds : bounds;
  }

  public enum SUB_ACTION_ID
  {
    STEP = 13, // 0x0000000D
    DOWN = 14, // 0x0000000E
    ANGRY = 15, // 0x0000000F
    ESCAPE = 16, // 0x00000010
    COUNTER = 17, // 0x00000011
    DIZZY = 18, // 0x00000012
    SHADOWSEALING = 19, // 0x00000013
    MAD_MODE = 20, // 0x00000014
    APPEAR = 21, // 0x00000015
    LIGHT_RING = 22, // 0x00000016
    BIND = 23, // 0x00000017
    DEAD_REVIVE = 24, // 0x00000018
    CONCUSSION = 25, // 0x00000019
    MAX = 26, // 0x0000001A
  }

  public enum SUB_MOTION_ID
  {
    STEP = 115, // 0x00000073
    STEP_BACK = 116, // 0x00000074
    DOWN = 117, // 0x00000075
    DOWN_TIME = 118, // 0x00000076
    COUNTER = 119, // 0x00000077
    ESCAPE_START = 120, // 0x00000078
    ESCAPE = 121, // 0x00000079
    DIZZY = 122, // 0x0000007A
    MAD_MODE = 123, // 0x0000007B
    APPEAR = 124, // 0x0000007C
    DEAD_REVIVE_01 = 125, // 0x0000007D
    DEAD_REVIVE_02 = 126, // 0x0000007E
    DEAD_REVIVE_03 = 127, // 0x0000007F
    DEAD_REVIVE_04 = 128, // 0x00000080
    DEAD_REVIVE_05 = 129, // 0x00000081
    ANGRY_BEGIN = 130, // 0x00000082
    ANGRY_END = 146, // 0x00000092
    MAX = 147, // 0x00000093
  }

  [Serializable]
  public class RegionInfo
  {
    public string name;
    public int maxHP = 10;
    public string[] deactivateObjects;
    [Tooltip("ヒット素材名(EnemyHitMaterialTable)")]
    public string hitMaterialName;
    public Enemy.RegionInfo.BreakEffect breakEffect;
    public Enemy.RegionInfo.BreakDrop breakDrop;
    [Tooltip("部位破壊時ダウンモーション")]
    public bool breakInDown;
    [Tooltip("部位破壊時ダメージモーション")]
    public bool breakInDamage;
    [Tooltip("部位破壊後ヒット有効フラグ")]
    public bool breakAfterHit = true;
    [Tooltip("部位破壊後生成バレット")]
    public Enemy.RegionInfo.BreakBullet[] breakBullet;
    [Tooltip("耐性(%)")]
    public AtkAttribute tolerance = new AtkAttribute();
    [Tooltip("防御力(+)")]
    public AtkAttribute defence = new AtkAttribute();
    [Tooltip("親部位名（親破壊まで無効設定")]
    public string parentRegionName;
    [Tooltip("復活可能フラグ")]
    public bool enableRevive;
    [Tooltip("復活までの時間")]
    public float reviveIntervalTime;
    [Tooltip("特殊バフ適用時のバリアへのダメージ値")]
    public int barrierDamageSp = 100;
    [Tooltip("通常時のバリアへのダメージ値")]
    public AtkAttribute atkBarrierDamage = new AtkAttribute();
    [Tooltip("バリアによる耐性値上昇率(0.0〜1.0)")]
    public float barrierToleranceRate;
    [Tooltip("カウンター情報")]
    public Enemy.RegionInfo.CounterInfo counterInfo = new Enemy.RegionInfo.CounterInfo();
    [Tooltip("ダウン値係数")]
    public int customDownRate;
    [Tooltip("シールドダメージ有効フラグ")]
    public bool isEnableShieldDamage;
    [Tooltip("シールド時の掴み解除フラグ")]
    public bool isGrabRelease;
    [Tooltip("最終ダメージを最小にする")]
    public bool isDamageMinimum;
    public Enemy.RegionInfo.WeaponTypeRate[] weaponTypeRate;
    [Tooltip("プレイヤーの攻撃が当たるか")]
    public bool isAtkColliderHit = true;
    [Tooltip("モード変更情報")]
    public Enemy.RegionInfo.ModeChangeInfo modeChangeInfo;
    [Tooltip("竜装情報")]
    public Enemy.RegionInfo.DragonArmorInfo dragonArmorInfo;

    [Serializable]
    public class BreakEffect
    {
      public string effectName;
      public Vector3 effectAngle;
      public string nodeName;
    }

    [Serializable]
    public class BreakDrop
    {
      public string dropNodeName;
    }

    [Serializable]
    public class BreakBullet
    {
      public string[] stringArgs;
      public float[] floatArgs;
      public int[] intArgs;
    }

    [Serializable]
    public class CounterInfo
    {
      [Tooltip("この部位があるとカウンターが有効かどうか")]
      public bool enabled;
      [Tooltip("カウンターが発動するのは何発受けたときか")]
      public int counterLimitNum = 1;
    }

    [Serializable]
    public class WeaponTypeRate
    {
      [Tooltip("対応する武器（EQUIPMENT_TYPE）")]
      public EQUIPMENT_TYPE equipmentType = EQUIPMENT_TYPE.NONE;
      [Tooltip("武器種倍率（小数）")]
      public float rate = 1f;
    }

    [Serializable]
    public class ModeChangeInfo
    {
      [Tooltip("設定されているモードへの変更を有効にする")]
      public bool enabled;
      [Tooltip("変更後モードID")]
      public int modeID;
    }

    [Serializable]
    public class DragonArmorInfo
    {
      [Tooltip("竜装が有効か")]
      public bool enabled;
      [Tooltip("両手剣バーストSP攻撃の倍率")]
      public float thsBurstSpAttackRate = 1f;
      [Tooltip("その他攻撃の倍率")]
      public float otherAttackRate = 1f;
    }
  }

  public enum WEAK_STATE
  {
    NONE,
    WEAK,
    DOWN,
    WEAK_SP_ATTACK,
    WEAK_SP_DOWN_MAX,
    WEAK_ELEMENT_ATTACK,
    WEAK_ELEMENT_SKILL_ATTACK,
    WEAK_SKILL_ATTACK,
    WEAK_HEAL_ATTACK,
    WEAK_GRAB,
    WEAK_CANNON,
    WEAK_ELEMENT_SP_ATTACK,
  }

  [Serializable]
  public class BleedData
  {
    public int ownerID;
    public int cnt;
    public int damage;
    public bool skipFirst;
    public int lv;
    public static readonly int MaxLv = 3;

    public bool IsOwnerSelf()
    {
      return MonoBehaviourSingleton<StageObjectManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null) && MonoBehaviourSingleton<StageObjectManager>.I.self.id == this.ownerID;
    }

    public bool IsMaxLv() => Enemy.BleedData.MaxLv <= this.lv;
  }

  [Serializable]
  public class BleedWork
  {
    public int ownerID;
    public int showIndex;
    public Transform bleedEffect;
  }

  [Serializable]
  public class BleedSyncData
  {
    public int afterHP;
    public List<Enemy.BleedSyncData.BleedRegionWork> regionWorks = new List<Enemy.BleedSyncData.BleedRegionWork>();

    [Serializable]
    public class BleedDamageData
    {
      public int ownerID;
      public int damage;
    }

    [Serializable]
    public class BleedRegionWork
    {
      public int id;
      public int afterHP;
      public List<Enemy.BleedSyncData.BleedDamageData> damageList = new List<Enemy.BleedSyncData.BleedDamageData>();
    }
  }

  [Serializable]
  public class ShadowSealingData
  {
    public bool isTarget;
    public int ownerID;
    public float existSec;
    public float extendRate = 1f;

    public bool IsOwnerSelf()
    {
      return MonoBehaviourSingleton<StageObjectManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null) && MonoBehaviourSingleton<StageObjectManager>.I.self.id == this.ownerID;
    }
  }

  [Serializable]
  public class ShadowSealingSyncData
  {
    public int regionIndex;
  }

  [Serializable]
  public class BombArrowData
  {
    public int ownerID;
    public float startTime;
    public AtkAttribute atk;

    public float GetRemainingCount()
    {
      return Mathf.Max(0.0f, this.startTime + MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.bombArrowCountSec - Time.time);
    }
  }

  [Serializable]
  public class RegionWorkSyncData
  {
    public int hp;
    public bool isBroke;
    public List<Enemy.BleedData> bleedList;
    public int barrierHp;
    public bool isShieldDamage;
    public bool isShieldCriticalDamage;
    public Enemy.ShadowSealingData shadowSealingData;
    public List<Enemy.BombArrowData> bombArrowDataHistory;
  }

  public class RandomShotInfo
  {
    public const int TARGET_NONE = -1;
    public AttackInfo atkInfo;
    public float interval;
    public float countTime;
    public int shotCount;

    public List<Vector3> points { get; set; }

    public List<Enemy.RandomShotInfo.TargetInfo> targets { get; set; }

    public class TargetInfo
    {
      public Quaternion rot;
      public int targetId;

      public TargetInfo()
      {
      }

      public TargetInfo(Quaternion rot, int id)
      {
        this.rot = rot;
        this.targetId = id;
      }
    }
  }

  public class AnimationLayerWeightChangeInfo
  {
    public float weight;
    public float spd;
    public float target;
    public int layerIndex;
    public bool forceEndFlag;
    public bool aliveFlag;
  }

  private enum eCounterRegionState
  {
    NONE,
    EXIST,
    NOT_EXIST,
  }

  public enum CANCEL_CONDITION
  {
    NONE,
    FAILED_GRAB,
  }

  private enum EFFECTIVE_TYPE
  {
    GOOD,
    NORMAL,
    BAD,
  }
}
