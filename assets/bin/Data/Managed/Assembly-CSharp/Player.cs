// Decompiled with JetBrains decompiler
// Type: Player
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
public class Player : Character
{
  protected const int MAX_WEAPON_TYPE_COUNT = 5;
  protected const string ANIMATOR_SUB_STATE_BURST_TYPE = "BURST.";
  protected const string ANIMATOR_SUB_STATE_ORACLE_TYPE = "ORACLE.";
  private const float REVIVAL_TIME = 3f;
  public const float BODY_HEIGHT = 1.7f;
  public const string NODE_WEAPON_RIGHT = "weaponR";
  private const string NODE_WEAPON_LEFT = "weaponL";
  private const string EFFECT_NAME_COUNTER_ATTACK = "ef_btl_ab_charge_01";
  public const string ARROW_ATTACK = "PLC05_attack_00";
  protected const int ARROW_STAND_SHOT_ATTACKID = 1;
  private const int ONEHANDSWORD_HEAT_REVENGEBURST_ATTACKID = 96 /*0x60*/;
  protected const int PAIR_SWORDS_AVOID_ATTACKID = 20;
  public const int PAIR_SWORDS_BURST_AVOID_ATTACKID = 21;
  public const int PAIR_SWORDS_BOOST_MODE_ATTACK_ID = 98;
  protected const int PAIR_SWORDS_BOOST_MODE_FAILURE_ATTACK_ID = 97;
  private const float SP_ACTION_GAUGE_MAX = 1000f;
  public const int SPEAR_RUSH_COMBO_START_ATTACKID = 10;
  public const int SPEAR_RUSH_COMBO_END_ATTACKID = 13;
  protected const int SPEAR_HUNDRED_START_ATTACKID = 20;
  protected const int SPEAR_HUNDRED_FINISH_ATTACKID = 22;
  public const int SPEAR_JUMP_START_ATTACKID = 97;
  private const float SP_ACTION_JUMP_GAUGE_MAX = 999f;
  private const float SP_ACTION_JUMP_GAUGE_UNIT = 333f;
  private const int JUMP_LEVEL_MAX = 3;
  private const float PRAYER2_TIME_RATEUP = 1.2f;
  private const float PRAYER3_TIME_RATEUP = 1.4f;
  private const float CHARGE_ARROW_TIME_RATE_MAX = 0.85f;
  public InGameRecorder.PlayerRecord record;
  protected int localDecoyId;
  protected SoulEnergyController soulEnergyCtrl;
  protected Material[] playerMaterials;
  private BitArray disableActionFlag = new BitArray(50);
  public static readonly string[] subMotionStateName = new string[33]
  {
    "avoid",
    "stumble",
    "shake",
    "blow",
    "fall_blow",
    "stunned_blow",
    "guard",
    "guard_walk",
    "guard_dmg",
    "battle_start",
    "dead_loop",
    "dead_standup",
    "prayer",
    "change_weapon",
    "struggle",
    "restraint",
    "cannon_enter",
    "cannon_loop",
    "guard_no_knockback",
    "guard_parry",
    "warp",
    "avoid_alter",
    "warp_alter",
    "fishing",
    "coop_fishing",
    "stone",
    "stone_end",
    "carry_lift",
    "carry_idle",
    "carry_walk",
    "carry_put",
    "teleport_avoid",
    "rush_avoid"
  };
  private static readonly int guardAngleID = 0;
  private static readonly int arrowAngleID = 0;
  private static readonly int rushAvoidAngleID = 0;
  public float[] attackReachs = new float[6]
  {
    0.0f,
    5f,
    5.5f,
    6f,
    4f,
    15f
  };
  public float[] specialReachs = new float[6]
  {
    0.0f,
    0.0f,
    5.5f,
    8.5f,
    4f,
    20f
  };
  public float[] avoidAttackReachs = new float[6]
  {
    0.0f,
    0.0f,
    8.5f,
    0.0f,
    0.0f,
    0.0f
  };
  protected Transform _physics;
  public StageObjectManager.CreatePlayerInfo createInfo;
  public List<CharaInfo.EquipItem> equipWeaponList = new List<CharaInfo.EquipItem>();
  protected List<EquipItemTable.EquipItemData> weaponEquipItemDataList = new List<EquipItemTable.EquipItemData>();
  private XorFloat _playerAtk = (XorFloat) 0.0f;
  private XorFloat _playerDef = (XorFloat) 0.0f;
  private XorInt _playerHp = (XorInt) 0;
  public Player.PlayerState baseState = new Player.PlayerState();
  public Player.EquipState weaponState = new Player.EquipState();
  public Player.EquipState skillConstState = new Player.EquipState();
  public Player.SkillData skillData = new Player.SkillData();
  public Player.AbilityData abilityData = new Player.AbilityData();
  public List<AbilityItem> abilityItem = new List<AbilityItem>();
  private List<int> guardEquipDef = new List<int>();
  private XorInt _hpUp;
  private XorInt _healHp;
  private XorFloat _healHpSpeed;
  private XorFloat _addHp = (XorFloat) 0.0f;
  protected BuffParam.BuffSyncParam initBuffSyncParam;
  protected List<int> abilityCounterAttackNumList;
  protected List<int> abilityCleaveComboNumList;
  public int inputComboID = -1;
  public string inputComboMotionState = "";
  public bool inputComboFlag;
  private bool inputChargeAutoRelease;
  private bool inputChargeMaxTiming;
  private float inputChargeTimeMax;
  private float inputChargeTimeOffset;
  private float inputChargeTimeCounter;
  private bool isInputChargeExistOffset;
  private bool isChargeExpanding;
  private bool isChargeExpandAutoRelease;
  private float timeChargeExpandMax;
  private float timerChargeExpandOffset;
  private float timerChargeExpand;
  public bool inputNextTriggerFlag;
  public int inputNextTriggerIndex;
  public float countLongTouchSec;
  private float chargeRate;
  private float chargeExpandRate;
  public bool enableInputRotate;
  public bool startInputRotate;
  private bool enableRotateToTargetPoint;
  public bool inputBlowClearFlag;
  protected float stumbleEndTime;
  protected float shakeEndTime;
  protected float stunnedTime;
  protected float stunnedEndTime;
  protected float stunnedReduceEnableTime;
  protected GameObject stunnedEffect;
  protected int stunnedEffectIndex = -1;
  public bool isGuardWalk;
  public bool isCarryWalk;
  public bool enableSuperArmor;
  protected bool isSkillCastLoop;
  protected float skillCastLoopStartTime = -1f;
  protected float skillCastLoopTime = -1f;
  protected string skillCastLoopTrigger;
  protected Transform skillRangeEffect;
  protected float actSpecialActionTimer;
  private Transform healEffectTransform;
  private Transform skillChargeEffectTransform;
  private StringKeyTable<Transform> effectTransTable = new StringKeyTable<Transform>();
  private StringKeyTable<Transform> rootEffectDetachTemporaryTable = new StringKeyTable<Transform>();
  protected bool isCanRushRelease;
  protected bool isChargeExRush;
  protected bool isLoopingRush;
  protected float actRushLoopTimer;
  protected float hitSpearSpActionTimer;
  protected bool hitSpearSpecialAction;
  protected bool lockedSpearCancelAction;
  protected float exRushChargeRate;
  protected bool isSpearHundred;
  protected float spearHundredSecFromStart;
  protected float spearHundredSecFromLastTap;
  public bool isSpearJumpAim;
  protected float jumpActionCounter;
  protected Vector3 jumpFallBodyPosition;
  protected Vector3 jumpRandingVector;
  protected Vector3 jumpRaindngBasePos;
  protected float jumpRandingBaseBodyY;
  public int useGaugeLevel;
  protected Player.eJumpState jumpState;
  protected bool isArrowSitShot;
  public Player.RAIN_SHOT_STATE rainShotState;
  public Vector3 rainShotFallPosition;
  public float rainShotFallRotateY;
  public int rainShotLotGroupId;
  private float hitSpAttackContinueTimer;
  private bool isLockedSpAttackContinue;
  private SelfController.FLICK_DIRECTION flickDirection;
  private Transform twoHandSwordsBoostLoopEffect;
  private Transform twoHandSwordsChargeMaxEffect;
  protected const string REVIVAL_RANGE_EFFECT = "ef_btl_rebirth_area_01";
  protected const string REVIVAL_EFFECT = "ef_btl_rebirth_01";
  protected GameObject revivalRangEffect;
  public float deadStartTime = -1f;
  public float deadStopTime = -1f;
  protected float _rescueTime;
  public float stoneStartTime = -1f;
  public float stoneStopTime = -1f;
  protected float _stoneRescueTime;
  private bool isInitDead;
  private float initRescueTime;
  private float initContinueTime;
  protected float prayerTime;
  private List<Player.BoostPrayInfo> boostPrayTargetInfoList = new List<Player.BoostPrayInfo>();
  private List<Player.BoostPrayInfo> boostPrayedInfoList = new List<Player.BoostPrayInfo>();
  private XorInt _autoReviveHp = (XorInt) 0;
  public bool isValidAutoReviveSkillChargeBuff;
  protected List<KeyValuePair<int, SkillInfo.SkillParam>> buffInfoListOnActDeadStandUp = new List<KeyValuePair<int, SkillInfo.SkillParam>>();
  public UIPlayerStatusGizmo uiPlayerStatusGizmo;
  public List<TargetPoint> targetPointWithSpWeakList = new List<TargetPoint>();
  protected Dictionary<string, float> defaultBulletSpeedDic = new Dictionary<string, float>();
  protected CharaInfo.EquipItem changeWeaponItem;
  protected int changeWeaponIndex = -1;
  protected StageObjectManager.CreatePlayerInfo changePlayerInfo;
  protected bool isChangingWeapon;
  protected float changeWeaponStartTime = -1f;
  private List<Player.PrayInfo> prayerEndInfos = new List<Player.PrayInfo>();
  public bool isGatherInterruption;
  public FieldCarriableGimmickObject carryingGimmickObject;
  public IFieldGimmickObject targetingGimmickObject;
  protected bool isSyncingCannonRotation;
  protected Quaternion syncCannonRotation = Quaternion.identity;
  protected Quaternion prevCannonRotation = Quaternion.identity;
  protected float syncCannonVecTimer;
  private float inputCannonChargeCounter;
  private float inputCannonChargeMax;
  private bool isAnimEventStatusUpDefence;
  private float animEventStatusUpDefenceRate = 1f;
  private float arrowBulletSpeedUpRate;
  private List<Player.WEAPON_EFFECT_DATA> m_weaponEffectDataList = new List<Player.WEAPON_EFFECT_DATA>();
  protected EvolveController evolveCtrl;
  public SnatchController snatchCtrl;
  public FishingController fishingCtrl;
  public FieldGatherGimmickObject gatherGimmickObject;
  public FieldQuestGimmickObject questGimmickObject;
  protected List<IWeaponController> m_weaponCtrlList = new List<IWeaponController>(5);
  public OneHandSwordController ohsCtrl;
  public PairSwordsController pairSwordsCtrl;
  public SpearController spearCtrl;
  public TwoHandSwordController thsCtrl;
  private List<Transform> pairSwordsBoostModeAuraEffectList = new List<Transform>(2);
  private List<Transform> pairSwordsBoostModeTrailEffectList = new List<Transform>(2);
  private Transform buffShadowSealingEffect;
  private EnemyBrain _bossBrain;
  private Transform ohsMaxChargeEffect;
  private bool isJustGuard;
  private bool isSuccessParry;
  private bool notEndGuardFlag;
  private float guardingSec;
  public Player.ShieldReflectInfo shieldReflectInfo = new Player.ShieldReflectInfo();
  public bool enabledTeleportAvoid;
  public bool enabledRushAvoid;
  public bool enabledOraclePairSwordsSP;
  public float extraSpGaugeDecreasingRate;
  private AttackRestraintObject m_attackRestraint;
  private RestraintInfo m_restraintInfo;
  private float m_restrainTime;
  private float m_restraintDamgeTimer;
  private int m_restrainDamageValue;
  private GameObject m_stoneEffect;
  private DrainAttackInfo grabDrainAtkInfo;
  private float grabDrainDamageTimer;
  private List<int> a_ids = new List<int>();
  private List<int> a_pts = new List<int>();
  private List<BuffParam.BUFFTYPE> passiveSkillList = new List<BuffParam.BUFFTYPE>();
  private float timeWhenJustGuardChecked = -1f;
  private List<AttackNWayLaser> activeAttackLaserList = new List<AttackNWayLaser>();
  private Transform exRushChargeEffect;
  protected float evolveSpecialActionSec;
  private int burstBarrierCounter;
  private List<BarrierBulletObject> bulletBarrierObjList = new List<BarrierBulletObject>();
  public BarrierBulletObject activeBulletBarrierObject;
  private IEnumerator cancelInvincible;
  private Player.FixWeaponData fixWepData = new Player.FixWeaponData();
  private Dictionary<uint, BulletControllerTurretBit> bulletTurretList = new Dictionary<uint, BulletControllerTurretBit>();
  private CircleShadow shadow;

  public override int id
  {
    get => base.id;
    set
    {
      base.id = value;
      ((Object) ((Component) this).gameObject).name = "Player:" + (object) value;
    }
  }

  public PlayerLoader loader { get; private set; }

  public InGameSettingsManager.Player playerParameter { get; protected set; }

  public InGameSettingsManager.Player.WeaponInfo weaponInfo { get; protected set; }

  public InGameSettingsManager.BuffParamInfo buffParameter { get; protected set; }

  public InGameSettingsManager.DebuffParam debuffParameter { get; protected set; }

  public void SetDiableAction(Character.ACTION_ID id, bool disable)
  {
    this.disableActionFlag.Set((int) id, disable);
  }

  public Player.ATTACK_MODE attackMode { get; protected set; }

  public SP_ATTACK_TYPE spAttackType { get; protected set; }

  public EXTRA_ATTACK_TYPE extraAttackType { get; protected set; }

  public static Player.ATTACK_MODE ConvertEquipmentTypeToAttackMode(EQUIPMENT_TYPE equipment_type)
  {
    Player.ATTACK_MODE attackMode = Player.ATTACK_MODE.NONE;
    switch (equipment_type)
    {
      case EQUIPMENT_TYPE.ONE_HAND_SWORD:
        attackMode = Player.ATTACK_MODE.ONE_HAND_SWORD;
        break;
      case EQUIPMENT_TYPE.TWO_HAND_SWORD:
        attackMode = Player.ATTACK_MODE.TWO_HAND_SWORD;
        break;
      case EQUIPMENT_TYPE.SPEAR:
        attackMode = Player.ATTACK_MODE.SPEAR;
        break;
      case EQUIPMENT_TYPE.PAIR_SWORDS:
        attackMode = Player.ATTACK_MODE.PAIR_SWORDS;
        break;
      case EQUIPMENT_TYPE.ARROW:
        attackMode = Player.ATTACK_MODE.ARROW;
        break;
    }
    return attackMode;
  }

  public static EQUIPMENT_TYPE ConvertAttackModeToEquipmentType(Player.ATTACK_MODE mode)
  {
    EQUIPMENT_TYPE equipmentType = EQUIPMENT_TYPE.ONE_HAND_SWORD;
    switch (mode)
    {
      case Player.ATTACK_MODE.ONE_HAND_SWORD:
        equipmentType = EQUIPMENT_TYPE.ONE_HAND_SWORD;
        break;
      case Player.ATTACK_MODE.TWO_HAND_SWORD:
        equipmentType = EQUIPMENT_TYPE.TWO_HAND_SWORD;
        break;
      case Player.ATTACK_MODE.SPEAR:
        equipmentType = EQUIPMENT_TYPE.SPEAR;
        break;
      case Player.ATTACK_MODE.PAIR_SWORDS:
        equipmentType = EQUIPMENT_TYPE.PAIR_SWORDS;
        break;
      case Player.ATTACK_MODE.ARROW:
        equipmentType = EQUIPMENT_TYPE.ARROW;
        break;
    }
    return equipmentType;
  }

  public float attackReach
  {
    get
    {
      if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
        return 5f;
      if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL))
        return 8f;
      return this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.SOUL) ? 10f : this.attackReachs[(int) this.attackMode] * 0.8f;
    }
  }

  public float specialReach
  {
    get
    {
      if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL))
        return 10f;
      if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL) || this.CheckAttackModeAndSpType(Player.ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.BURST))
        return 8f;
      return this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.SOUL) ? 10f : this.specialReachs[(int) this.attackMode];
    }
  }

  public float avoidAttackReach => this.avoidAttackReachs[(int) this.attackMode];

  public bool enableCancelToAvoid { get; protected set; }

  public bool enableCancelToMove { get; protected set; }

  public bool enableCancelToAttack { get; protected set; }

  public bool enableCancelToSkill { get; protected set; }

  public bool enableCancelToSpecialAction { get; protected set; }

  public bool enableCounterAttack { get; protected set; }

  public bool disableCounterAnimEvent { get; protected set; }

  public bool disableParryAction { get; protected set; }

  public bool disableGuard { get; protected set; }

  public bool enableAnimSeedRate { get; protected set; }

  public bool enableCancelToEvolveSpecialAction { get; protected set; }

  public bool enableCancelToCarryPut { get; protected set; }

  public PlayerPacketReceiver playerReceiver => (PlayerPacketReceiver) this.packetReceiver;

  public PlayerPacketSender playerSender => (PlayerPacketSender) this.packetSender;

  public float playerAtk
  {
    get => (float) this._playerAtk;
    protected set => this._playerAtk = (XorFloat) value;
  }

  protected float playerDef
  {
    get => (float) this._playerDef;
    set => this._playerDef = (XorFloat) value;
  }

  protected int defenseThreshold { get; set; }

  protected AtkAttribute defenseCoefficient { get; set; }

  protected int playerHp
  {
    get => (int) this._playerHp;
    set => this._playerHp = (XorInt) value;
  }

  public CharaInfo.EquipItem weaponData { get; protected set; }

  public int weaponIndex { get; protected set; }

  public int uniqueEquipmentIndex { get; protected set; }

  public int hpUp
  {
    get => (int) this._hpUp;
    set => this._hpUp = (XorInt) value;
  }

  public int healHp
  {
    get => (int) this._healHp;
    set => this._healHp = (XorInt) value;
  }

  public float healHpSpeed
  {
    get => (float) this._healHpSpeed;
    set => this._healHpSpeed = (XorFloat) value;
  }

  public float addHp
  {
    get => (float) this._addHp;
    set => this._addHp = (XorFloat) value;
  }

  public bool enableInputCombo { get; protected set; }

  public bool controllerInputCombo { get; protected set; }

  public bool enableComboTrans { get; protected set; }

  public bool enableInputCharge { get; private set; }

  public bool enableTap { get; protected set; }

  public bool enableFlickAction { get; protected set; }

  public bool enableInputNextTrigger { get; protected set; }

  public bool enableNextTriggerTrans { get; protected set; }

  public bool isCountLongTouch { get; protected set; }

  public float ChargeRate => this.chargeRate;

  public bool isStunnedLoop { get; protected set; }

  public SkillInfo skillInfo { get; protected set; }

  public bool isActSkillAction { get; protected set; }

  public bool isUsingSecondGradeSkill { get; protected set; }

  public bool isAbleToSkipSkillAction { get; protected set; }

  public bool isSkillCastState { get; protected set; }

  public bool isAppliedSkillParam { get; protected set; }

  public bool isActSpecialAction { get; protected set; }

  public bool HitSpearSpecialAction => this.hitSpearSpecialAction;

  public bool isAerial { get; protected set; }

  public bool isJumpAction => this.jumpState != 0;

  public bool isArrowRainShot => this.rainShotState > Player.RAIN_SHOT_STATE.NONE;

  public bool isHitSpAttack { get; set; }

  public bool enableSpAttackContinue { get; protected set; }

  public bool enableAttackNext { get; protected set; }

  public bool enableWeaponAction { get; protected set; }

  public void SetFlickDirection(SelfController.FLICK_DIRECTION direction)
  {
    this.flickDirection = direction;
  }

  public bool IsFullCharge() => (double) this.chargeRate >= 1.0;

  public bool IsExpandFullCharge() => (double) this.chargeExpandRate >= 1.0;

  public float GetExRushChargeRate() => this.exRushChargeRate;

  public float CalcChargeExpandElementDamageUpRate()
  {
    if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      return 1f;
    InGameSettingsManager.Player.TwoHandSwordActionInfo handSwordActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo;
    if ((double) this.chargeExpandRate <= 0.0)
      return handSwordActionInfo.elementDamageRateMin;
    if (this.IsExpandFullCharge())
      return handSwordActionInfo.elementDamageRateFullCharge;
    float num = handSwordActionInfo.elementDamageRateMin + (handSwordActionInfo.elementDamageRate - handSwordActionInfo.elementDamageRateMin) * this.chargeExpandRate;
    if ((double) num < (double) handSwordActionInfo.elementDamageRateMin)
      num = handSwordActionInfo.elementDamageRateMin;
    return num;
  }

  public bool isGuardAttackMode
  {
    get
    {
      return !this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL) && this.attackMode == Player.ATTACK_MODE.ONE_HAND_SWORD;
    }
  }

  public bool isLongAttackMode
  {
    get
    {
      return this.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL) || this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL) || this.attackMode == Player.ATTACK_MODE.ARROW;
    }
  }

  public bool isSpearAttackMode => this.attackMode == Player.ATTACK_MODE.SPEAR;

  public bool isArrowAimBossMode { get; protected set; }

  public bool isArrowAimLesserMode { get; protected set; }

  public bool isArrowAimable { get; protected set; }

  public bool isArrowAimEnd { get; protected set; }

  public bool isArrowAimKeep { get; protected set; }

  public float rescueTime
  {
    get
    {
      if (!this.isDead || (double) this.deadStartTime < 0.0)
        return 0.0f;
      if ((double) this.deadStopTime > 0.0)
        return this._rescueTime - (this.deadStopTime - this.deadStartTime);
      return !MonoBehaviourSingleton<InGameProgress>.IsValid() ? 0.0f : this._rescueTime - (Time.time - this.deadStartTime);
    }
  }

  public float stoneRescueTime
  {
    get
    {
      if (!this.IsStone() || (double) this.stoneStartTime < 0.0)
        return 0.0f;
      if ((double) this.stoneStopTime > 0.0)
        return this._stoneRescueTime - (this.stoneStopTime - this.stoneStartTime);
      return !MonoBehaviourSingleton<InGameProgress>.IsValid() ? 0.0f : this._stoneRescueTime - (Time.time - this.stoneStartTime);
    }
  }

  public int rescueCount { get; protected set; }

  public bool IsAbleToRescueByRemainRescueTime()
  {
    int num = QuestManager.IsValidInGameExplore() ? 1 : 0;
    int index = this.rescueCount;
    if (num != 0)
      return this.playerParameter.exploreRescureCount > index;
    if (this.playerParameter.rescueTimes.Length == 0)
      return false;
    if (index >= this.playerParameter.rescueTimes.Length)
      index = this.playerParameter.rescueTimes.Length;
    return (double) this.playerParameter.rescueTimes[index] > 0.0;
  }

  public float continueTime { get; protected set; }

  public List<Player.PrayInfo> prayTargetInfos { get; protected set; }

  public List<int> prayerIds { get; protected set; }

  public bool IsPrayed() => this.prayerIds.Count > 0;

  public bool IsBoostByType(Player.BOOST_PRAY_TYPE type)
  {
    return !this.IsStone() && !this.boostPrayedInfoList.IsNullOrEmpty<Player.BoostPrayInfo>() && this.boostPrayedInfoList.Count<Player.BoostPrayInfo>((Func<Player.BoostPrayInfo, bool>) (item => item.isBoostByTypes[(int) type])) > 0;
  }

  public bool isRevivalEnabled
  {
    get
    {
      float num = 3f;
      if (this.playerParameter != null)
        num = this.playerParameter.revivalTime;
      return (double) this.prayerTime > (double) num;
    }
  }

  public float revivalTimePercent
  {
    get
    {
      float num = 3f;
      if (this.playerParameter != null)
        num = this.playerParameter.revivalTime;
      return this.prayerTime / num;
    }
  }

  public bool EnableRootMotion => this.enableRootMotion;

  public void SetEnableRootMotion(bool _isEnable) => this.enableRootMotion = _isEnable;

  public bool EnableEventMove => this.enableEventMove;

  public void SetEnableEventMove(bool _isEnable) => this.enableEventMove = _isEnable;

  public Vector3 EventMoveVelocity => this.eventMoveVelocity;

  public void SetEventMoveVelocity(Vector3 _velocity) => this.eventMoveVelocity = _velocity;

  public float EventMoveTimeCount => this.eventMoveTimeCount;

  public void SetEventMoveTimeCount(float _timeCount) => this.eventMoveTimeCount = _timeCount;

  public bool EnableAddForce => this.enableAddForce;

  public void SetEnableAddForce(bool _isEnable) => this.enableAddForce = _isEnable;

  public int autoReviveCount { get; protected set; }

  public int autoReviveHp
  {
    get => (int) this._autoReviveHp;
    set => this._autoReviveHp = (XorInt) value;
  }

  public bool IsAbleToAutoReviveBuff()
  {
    return !this.isDead && !this.IsValidBuff(BuffParam.BUFFTYPE.INVINCIBLECOUNT) && !this.IsValidBuff(BuffParam.BUFFTYPE.INVINCIBLE_BADSTATUS) && this.autoReviveCount < this.buffParameter.autoReviveMaxCount;
  }

  public bool IsAutoReviving() => this.autoReviveHp > 0;

  public bool isWaitingResurrectionHoming { get; protected set; }

  public bool isStopCounter { get; protected set; }

  public List<TargetPoint> targetingPointList { get; set; }

  public TargetPoint targetingPoint
  {
    get
    {
      return this.targetingPointList == null || this.targetingPointList.Count <= 0 ? (TargetPoint) null : this.targetingPointList[0];
    }
  }

  public TargetPoint targetAimAfeterPoint { get; set; }

  public TargetPoint targetPointWithSpWeak
  {
    get
    {
      return this.targetPointWithSpWeakList.IsNullOrEmpty<TargetPoint>() ? (TargetPoint) null : this.targetPointWithSpWeakList[0];
    }
  }

  public List<TargetPoint> arrowRainTargetPointList { get; set; }

  public bool isActedBattleStart { get; set; }

  public bool isWaitBattleStart { get; protected set; }

  public bool isNpc
  {
    get
    {
      return Object.op_Inequality((Object) this.controller, (Object) null) && this.controller is NpcController;
    }
  }

  public int shotArrowCount { get; protected set; }

  public bool IsChangingWeapon => this.isChangingWeapon;

  public GatherPointObject targetGatherPoint { get; protected set; }

  public bool isAppliedGather { get; protected set; }

  public float GetCannonChargeMax() => this.inputCannonChargeMax;

  public void SetCannonChargeMax(float max) => this.inputCannonChargeMax = max;

  public float GetCannonChargeRate()
  {
    return (double) this.inputCannonChargeMax <= 0.0 ? 0.0f : Mathf.Clamp01(this.inputCannonChargeCounter / this.inputCannonChargeMax);
  }

  public bool IsCannonFullCharged()
  {
    return (double) this.inputCannonChargeCounter >= (double) this.inputCannonChargeMax;
  }

  public void ClearCannonChargeRate() => this.inputCannonChargeCounter = 0.0f;

  public float healAtkRate { get; private set; }

  protected bool isAbsorbDamageSuperArmor { get; private set; }

  protected bool isInvincibleDamageSuperArmor { get; private set; }

  protected bool shouldShowInvincibleDamage { get; set; }

  public override void ActShieldBreak()
  {
    if (this.buffParam.IsEnableBuff(BuffParam.BUFFTYPE.SHIELD))
      this.OnBuffEnd(BuffParam.BUFFTYPE.SHIELD, true, true);
    base.ActShieldBreak();
  }

  private bool IsDiviedLoadAndInstantiate() => !FieldManager.IsValidInGameNoQuest();

  public Player.CANNON_STATE cannonState { get; private set; }

  public void SetCannonState(Player.CANNON_STATE cannonState) => this.cannonState = cannonState;

  public bool IsAbleMoveCannon() => this.cannonState == Player.CANNON_STATE.READY;

  public bool IsOnCannonMode() => this.cannonState != 0;

  public bool IsChargingCannon() => this.cannonState == Player.CANNON_STATE.CHARGE;

  public IFieldGimmickCannon targetFieldGimmickCannon { get; set; }

  public float[] spActionGauge { get; protected set; }

  public float CurrentWeaponSpActionGauge
  {
    get
    {
      return this.spActionGauge.Length - 1 < this.weaponIndex || this.weaponIndex < 0 ? 0.0f : this.spActionGauge[this.weaponIndex];
    }
    protected set
    {
      if (this.spActionGauge.Length - 1 < this.weaponIndex || this.weaponIndex < 0)
        return;
      this.spActionGauge[this.weaponIndex] = value;
      if ((double) this.spActionGauge[this.weaponIndex] <= (double) this.spActionGaugeMax[this.weaponIndex])
        return;
      this.spActionGauge[this.weaponIndex] = this.spActionGaugeMax[this.weaponIndex];
    }
  }

  public float[] spActionGaugeMax { get; protected set; }

  public float CurrentWeaponSpActionGaugeMax
  {
    get
    {
      return this.spActionGauge.Length - 1 < this.weaponIndex || this.weaponIndex < 0 ? 0.0f : this.spActionGaugeMax[this.weaponIndex];
    }
    protected set
    {
      if (this.spActionGauge.Length - 1 < this.weaponIndex || this.weaponIndex < 0)
        return;
      this.spActionGaugeMax[this.weaponIndex] = value;
    }
  }

  public bool IsValidSpActionGauge()
  {
    return (double) this.CurrentWeaponSpActionGaugeMax > 0.0 && !this.IsBurstTwoHandSword();
  }

  public bool IsValidSpActionMemori()
  {
    return this.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.HEAT) || this.CheckAttackModeAndSpType(Player.ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.HEAT) || this.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL);
  }

  public bool IsValidBurstBulletUI() => this.IsBurstTwoHandSword();

  public bool IsBurstTwoHandSword()
  {
    return this.CheckAttackModeAndSpType(Player.ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.BURST);
  }

  public bool IsOracleTwoHandSword()
  {
    return this.CheckAttackModeAndSpType(Player.ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.ORACLE);
  }

  public int attackHitCount { get; private set; }

  public bool isBoostMode { get; private set; }

  public int boostModeDamageUpLevel { get; private set; }

  public int boostModeDamageUpHitCount { get; private set; }

  public bool isBuffShadowSealing { get; private set; }

  public void ClearStoreEffect()
  {
    if (!MonoBehaviourSingleton<EffectManager>.IsValid())
      return;
    if (!this.pairSwordsBoostModeAuraEffectList.IsNullOrEmpty<Transform>())
    {
      for (int index = 0; index < this.pairSwordsBoostModeAuraEffectList.Count; ++index)
      {
        if (!Object.op_Equality((Object) this.pairSwordsBoostModeAuraEffectList[index], (Object) null))
        {
          EffectManager.ReleaseEffect(((Component) this.pairSwordsBoostModeAuraEffectList[index]).gameObject);
          this.pairSwordsBoostModeAuraEffectList[index] = (Transform) null;
        }
      }
      this.pairSwordsBoostModeAuraEffectList.Clear();
    }
    if (!this.pairSwordsBoostModeTrailEffectList.IsNullOrEmpty<Transform>())
    {
      for (int index = 0; index < this.pairSwordsBoostModeTrailEffectList.Count; ++index)
      {
        if (!Object.op_Equality((Object) this.pairSwordsBoostModeTrailEffectList[index], (Object) null))
        {
          EffectManager.ReleaseEffect(((Component) this.pairSwordsBoostModeTrailEffectList[index]).gameObject);
          this.pairSwordsBoostModeTrailEffectList[index] = (Transform) null;
        }
      }
      this.pairSwordsBoostModeTrailEffectList.Clear();
    }
    this.effectTransTable.ForEach((Action<Transform>) (value => this.ReleaseEffect(ref value)));
    this.effectTransTable.Clear();
    this.ReleaseEffect(ref this.twoHandSwordsChargeMaxEffect);
    this.ReleaseEffect(ref this.twoHandSwordsBoostLoopEffect);
    this.ReleaseEffect(ref this.ohsMaxChargeEffect);
    this.ReleaseEffect(ref this.buffShadowSealingEffect);
    this.ReleaseEffect(ref this.exRushChargeEffect);
  }

  public void ClearEvent() => this.Clear();

  private void ActivateStoredEffect()
  {
    if (!this.pairSwordsBoostModeAuraEffectList.IsNullOrEmpty<Transform>())
    {
      for (int index = 0; index < this.pairSwordsBoostModeAuraEffectList.Count; ++index)
      {
        if (!Object.op_Equality((Object) this.pairSwordsBoostModeAuraEffectList[index], (Object) null))
          ((Component) this.pairSwordsBoostModeAuraEffectList[index]).gameObject.SetActive(true);
      }
    }
    if (this.pairSwordsBoostModeTrailEffectList.IsNullOrEmpty<Transform>())
      return;
    for (int index1 = 0; index1 < this.pairSwordsBoostModeTrailEffectList.Count; ++index1)
    {
      if (!Object.op_Equality((Object) this.pairSwordsBoostModeTrailEffectList[index1], (Object) null))
      {
        ((Component) this.pairSwordsBoostModeTrailEffectList[index1]).gameObject.SetActive(true);
        ((Component) this.pairSwordsBoostModeTrailEffectList[index1]).GetComponentsInChildren<Trail>(Temporary.trailList);
        for (int index2 = 0; index2 < Temporary.trailList.Count; ++index2)
          Temporary.trailList[index2].Reset();
        Temporary.trailList.Clear();
      }
    }
  }

  private void DeactivateStoredEffect()
  {
    if (!this.pairSwordsBoostModeAuraEffectList.IsNullOrEmpty<Transform>())
    {
      for (int index = 0; index < this.pairSwordsBoostModeAuraEffectList.Count; ++index)
      {
        if (!Object.op_Equality((Object) this.pairSwordsBoostModeAuraEffectList[index], (Object) null))
          ((Component) this.pairSwordsBoostModeAuraEffectList[index]).gameObject.SetActive(false);
      }
    }
    if (this.pairSwordsBoostModeTrailEffectList.IsNullOrEmpty<Transform>())
      return;
    for (int index = 0; index < this.pairSwordsBoostModeTrailEffectList.Count; ++index)
    {
      if (!Object.op_Equality((Object) this.pairSwordsBoostModeTrailEffectList[index], (Object) null))
        ((Component) this.pairSwordsBoostModeTrailEffectList[index]).gameObject.SetActive(false);
    }
  }

  public EnemyHitTypeTable.TypeData GetOverrideHitEffect(
    AttackedHitStatusDirection status,
    ref Vector3 scale,
    ref float delay)
  {
    EnemyHitTypeTable.TypeData overrideHitEffect1 = (EnemyHitTypeTable.TypeData) null;
    if (this.IsSoulOneHandSwordBoostMode())
    {
      EnemyHitTypeTable.TypeData overrideHitEffect2 = new EnemyHitTypeTable.TypeData();
      InGameSettingsManager.Player.OneHandSwordActionInfo ohsActionInfo = this.playerParameter.ohsActionInfo;
      ((Vector3) ref scale).Set(0.5f, 0.5f, 0.5f);
      if (!((IList<string>) ohsActionInfo.Soul_BoostElementHitEffect).IsNullOrEmpty<string>())
      {
        for (int index = 0; index < ohsActionInfo.Soul_BoostElementHitEffect.Length; ++index)
          overrideHitEffect2.elementEffectNames[index] = ohsActionInfo.Soul_BoostElementHitEffect[index];
      }
      return overrideHitEffect2;
    }
    if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.COUNTER_BURST)
    {
      EnemyHitTypeTable.TypeData overrideHitEffect3 = new EnemyHitTypeTable.TypeData();
      InGameSettingsManager.Player.BurstOneHandSwordActionInfo burstOhsInfo = this.playerParameter.ohsActionInfo.burstOHSInfo;
      ((Vector3) ref scale).Set(0.5f, 0.5f, 0.5f);
      if (!((IList<string>) burstOhsInfo.BoostElementHitEffect).IsNullOrEmpty<string>())
      {
        for (int index = 0; index < burstOhsInfo.BoostElementHitEffect.Length; ++index)
          overrideHitEffect3.elementEffectNames[index] = burstOhsInfo.BoostElementHitEffect[index];
      }
      return overrideHitEffect3;
    }
    if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.BURST_THS_SINGLE_SHOT)
    {
      EnemyHitTypeTable.TypeData overrideHitEffect4 = new EnemyHitTypeTable.TypeData();
      InGameSettingsManager.Player.BurstTwoHandSwordActionInfo burstThsInfo = this.playerParameter.twoHandSwordActionInfo.burstTHSInfo;
      if (!((IList<string>) burstThsInfo.HitEffect_SingleShot).IsNullOrEmpty<string>())
      {
        for (int index = 0; index < burstThsInfo.HitEffect_SingleShot.Length; ++index)
          overrideHitEffect4.elementEffectNames[index] = burstThsInfo.HitEffect_SingleShot[index];
      }
      return overrideHitEffect4;
    }
    if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.BURST_THS_FULL_BURST)
    {
      EnemyHitTypeTable.TypeData overrideHitEffect5 = new EnemyHitTypeTable.TypeData();
      InGameSettingsManager.Player.BurstTwoHandSwordActionInfo burstThsInfo = this.playerParameter.twoHandSwordActionInfo.burstTHSInfo;
      if (!((IList<string>) burstThsInfo.HitEffect_FullBurst).IsNullOrEmpty<string>())
      {
        for (int index = 0; index < burstThsInfo.HitEffect_FullBurst.Length; ++index)
          overrideHitEffect5.elementEffectNames[index] = burstThsInfo.HitEffect_FullBurst[index];
      }
      return overrideHitEffect5;
    }
    if (this.pairSwordsCtrl.IsOverrideHitEffect(ref overrideHitEffect1, ref scale) || this.spearCtrl.TryOverrideHitEffect(ref overrideHitEffect1, ref scale))
      return overrideHitEffect1;
    int num;
    if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.JUMP)
    {
      num = this.useGaugeLevel;
    }
    else
    {
      if (!this.IsExRushDamageUpAttack(status.attackInfo.attackType) && !this.spearCtrl.IsSoulBoostMode())
        return (EnemyHitTypeTable.TypeData) null;
      num = 2;
    }
    InGameSettingsManager.Player.SpearActionInfo spearActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo;
    EnemyHitTypeTable.TypeData overrideHitEffect6;
    switch (num)
    {
      case 1:
        overrideHitEffect6 = Singleton<EnemyHitTypeTable>.I.GetData("TYPE_STAB_L", FieldManager.IsValidInGameNoQuest());
        break;
      case 2:
      case 3:
        if (num == 2)
          ((Vector3) ref scale).Set(0.8f, 0.8f, 0.8f);
        overrideHitEffect6 = new EnemyHitTypeTable.TypeData();
        int index1 = 0;
        for (int length = spearActionInfo.jumpHugeElementHitEffectNames.Length; index1 < length; ++index1)
          overrideHitEffect6.elementEffectNames[index1] = spearActionInfo.jumpHugeElementHitEffectNames[index1];
        for (int index2 = 6; index1 < index2; ++index1)
          overrideHitEffect6.elementEffectNames[index1] = spearActionInfo.jumpHugeHitEffectName;
        overrideHitEffect6.baseEffectName = spearActionInfo.jumpHugeHitEffectName;
        break;
      default:
        overrideHitEffect6 = Singleton<EnemyHitTypeTable>.I.GetData("TYPE_STAB_S", FieldManager.IsValidInGameNoQuest());
        break;
    }
    if (num == 3)
    {
      delay = 0.15f;
      EffectManager.OneShot("ef_btl_wsk_spear_01_03", status.hitPos, Quaternion.identity, true);
    }
    return overrideHitEffect6;
  }

  public override float GetEffectScaleDependValue()
  {
    return !this.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.HEAT) || this.useGaugeLevel < 0 || this.useGaugeLevel >= MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.jumpWaveScales.Length ? 1f : MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.jumpWaveScales[this.useGaugeLevel];
  }

  private EnemyBrain bossBrain
  {
    get
    {
      if (Object.op_Inequality((Object) this._bossBrain, (Object) null))
        return this._bossBrain;
      if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
        return (EnemyBrain) null;
      Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
      if (Object.op_Equality((Object) boss, (Object) null))
        return (EnemyBrain) null;
      this._bossBrain = ((Component) boss).GetComponent<EnemyBrain>();
      return this._bossBrain;
    }
  }

  public bool isActOneHandSwordCounter { get; set; }

  public bool isActTwoHandSwordHeatCombo => this.attackID == 89 || this.attackID == 88;

  public bool isActPairSwordsSoulLaser
  {
    get => this.attackID == this.playerParameter.pairSwordsActionInfo.Soul_SpLaserShotAttackId;
  }

  static Player()
  {
    Player.guardAngleID = Animator.StringToHash("guard_angle");
    Player.arrowAngleID = Animator.StringToHash("arrow_angle");
    Player.rushAvoidAngleID = Animator.StringToHash("rush_avoid_angle");
  }

  protected override void OnEnable()
  {
    base.OnEnable();
    if (!MonoBehaviourSingleton<UIStatusGizmoManager>.IsValid())
      return;
    this.uiPlayerStatusGizmo = MonoBehaviourSingleton<UIStatusGizmoManager>.I.Create(this);
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    if (!Object.op_Inequality((Object) this.uiPlayerStatusGizmo, (Object) null))
      return;
    this.uiPlayerStatusGizmo.targetPlayer = (Player) null;
    this.uiPlayerStatusGizmo = (UIPlayerStatusGizmo) null;
  }

  protected override void Awake()
  {
    base.Awake();
    this.objectType = StageObject.OBJECT_TYPE.PLAYER;
    this.attackMode = Player.ATTACK_MODE.NONE;
    this.spAttackType = SP_ATTACK_TYPE.NONE;
    this.extraAttackType = EXTRA_ATTACK_TYPE.NONE;
    this.weaponData = (CharaInfo.EquipItem) null;
    this.weaponIndex = -1;
    this.uniqueEquipmentIndex = -1;
    this.hpUp = 0;
    this.healHp = 0;
    this.healHpSpeed = 0.0f;
    this.enableInputCombo = false;
    this.controllerInputCombo = false;
    this.enableComboTrans = false;
    this.enableTap = false;
    this.enableFlickAction = false;
    this.enableInputNextTrigger = false;
    this.enableNextTriggerTrans = false;
    this.inputNextTriggerIndex = 0;
    this.inputNextTriggerFlag = false;
    this.isCountLongTouch = false;
    this.countLongTouchSec = 0.0f;
    this.isStunnedLoop = false;
    this.enableInputCharge = false;
    this.skillInfo = new SkillInfo(this);
    this.isActSkillAction = false;
    this.isAbleToSkipSkillAction = false;
    this.isSkillCastState = false;
    this.isAppliedSkillParam = false;
    this.isActSpecialAction = false;
    this.isArrowAimBossMode = false;
    this.isArrowAimLesserMode = false;
    this.isArrowAimable = false;
    this.isArrowAimEnd = false;
    this.isArrowAimKeep = false;
    this.isCanRushRelease = false;
    this.isLoopingRush = false;
    this.hitSpearSpecialAction = false;
    this.lockedSpearCancelAction = false;
    this.isSpearHundred = false;
    this.isSpearJumpAim = false;
    this.jumpActionCounter = 0.0f;
    this.jumpState = Player.eJumpState.None;
    this.useGaugeLevel = 0;
    this.isAerial = false;
    this.rainShotState = Player.RAIN_SHOT_STATE.NONE;
    this._rescueTime = 0.0f;
    this._stoneRescueTime = 0.0f;
    this.rescueCount = 0;
    this.continueTime = 0.0f;
    this.deadStartTime = -1f;
    this.deadStopTime = -1f;
    this.stoneStartTime = -1f;
    this.stoneStopTime = -1f;
    this.autoReviveCount = 0;
    this.autoReviveHp = 0;
    this.isStopCounter = false;
    this.buffInfoListOnActDeadStandUp = new List<KeyValuePair<int, SkillInfo.SkillParam>>();
    this.isWaitingResurrectionHoming = false;
    this.prayTargetInfos = new List<Player.PrayInfo>();
    this.prayerTime = 0.0f;
    this.prayerIds = new List<int>();
    this.boostPrayTargetInfoList.Clear();
    this.boostPrayedInfoList.Clear();
    this.healEffectTransform = (Transform) null;
    this.skillChargeEffectTransform = (Transform) null;
    this.targetingPointList = new List<TargetPoint>();
    this.arrowRainTargetPointList = new List<TargetPoint>();
    this.isActedBattleStart = false;
    this.isWaitBattleStart = false;
    this.shotArrowCount = 0;
    this.isSyncingCannonRotation = false;
    this.cannonState = Player.CANNON_STATE.NONE;
    this.isAnimEventStatusUpDefence = false;
    this.animEventStatusUpDefenceRate = 1f;
    this.isHitSpAttack = false;
    this.spActionGauge = new float[3];
    this.spActionGaugeMax = new float[3];
    this.attackHitCount = 0;
    this.isBoostMode = false;
    this.boostModeDamageUpLevel = 0;
    this.boostModeDamageUpHitCount = 0;
    this._bossBrain = (EnemyBrain) null;
    this.isJustGuard = false;
    this.isSuccessParry = false;
    this.guardingSec = 0.0f;
    this.isBuffShadowSealing = false;
    this.healAtkRate = 0.0f;
    this.localDecoyId = 0;
    this.isAbsorbDamageSuperArmor = false;
    this.isInvincibleDamageSuperArmor = false;
    this.shouldShowInvincibleDamage = false;
    this.enabledTeleportAvoid = false;
    this.enabledRushAvoid = false;
    this.extraSpGaugeDecreasingRate = 0.0f;
    this.enabledOraclePairSwordsSP = false;
    this.evolveCtrl = new EvolveController();
    this.snatchCtrl = new SnatchController();
    this.fishingCtrl = new FishingController();
    this.m_weaponCtrlList.Clear();
    this.ohsCtrl = new OneHandSwordController();
    this.m_weaponCtrlList.Add((IWeaponController) this.ohsCtrl);
    this.pairSwordsCtrl = new PairSwordsController();
    this.m_weaponCtrlList.Add((IWeaponController) this.pairSwordsCtrl);
    this.spearCtrl = new SpearController();
    this.m_weaponCtrlList.Add((IWeaponController) this.spearCtrl);
    this.thsCtrl = new TwoHandSwordController();
    this.m_weaponCtrlList.Add((IWeaponController) this.thsCtrl);
    this.loader = ((Component) this).gameObject.AddComponent<PlayerLoader>();
    ((Component) this).gameObject.layer = 8;
    if (Object.op_Equality((Object) this._rigidbody, (Object) null))
      this._rigidbody = ((Component) this).gameObject.AddComponent<Rigidbody>();
    this._rigidbody.mass = 1f;
    this._rigidbody.angularDrag = 100f;
    this._rigidbody.isKinematic = false;
    this._rigidbody.constraints = (RigidbodyConstraints) 116;
    this._rigidbody.collisionDetectionMode = (CollisionDetectionMode) 1;
    if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
    {
      this.playerParameter = MonoBehaviourSingleton<InGameSettingsManager>.I.player;
      this.damageHealRate = this.playerParameter.damegeHealRate;
      this.buffParameter = MonoBehaviourSingleton<InGameSettingsManager>.I.buff;
      this.debuffParameter = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff;
    }
    if (this.playerParameter != null)
    {
      if (this.hpMax == 0)
      {
        this.hp = this.hpMax = this.healHp = this.playerParameter.hpMax;
        this.healHpSpeed = (float) this.playerParameter.hpHealSpeed;
      }
      this.moveRotateMaxSpeed = this.playerParameter.moveRotateMaxSpeed;
    }
    this.playerAtk = 0.0f;
    this.playerDef = 0.0f;
    this.playerHp = 0;
    for (int index = 0; index < 7; ++index)
    {
      this.baseState.atkList.Add(0);
      this.baseState.defList.Add(0);
      this.weaponState.atkList.Add(0);
      this.weaponState.defList.Add(0);
      this.skillConstState.atkList.Add(0);
      this.skillConstState.defList.Add(0);
      this.guardEquipDef.Add(0);
    }
    this.weaponState.hp = 0;
    this.skillConstState.hp = 0;
    this.defenseCoefficient = new AtkAttribute();
    this.defenseThreshold = 0;
    this.buffParam.passive.Reset();
    this.evolveCtrl.Init(this);
    this.buffParam.ownerEvolveCtrl = this.evolveCtrl;
    this.snatchCtrl.Init(this, ((Component) this).gameObject.AddComponent<SnatchLineRenderer>());
    int index1 = 0;
    for (int count = this.m_weaponCtrlList.Count; index1 < count; ++index1)
      this.m_weaponCtrlList[index1].Init(this);
    this.fishingCtrl.Initialize(this);
    this.thsCtrl.InitAppend(new TwoHandSwordController.InitParam()
    {
      Owner = this,
      BurstInitParam = new TwoHandSwordBurstController.InitParam()
      {
        Owner = this,
        ActionInfo = this.playerParameter.twoHandSwordActionInfo,
        MaxBulletCount = 6,
        CurrentRestBullets = (int[]) null
      }
    });
  }

  public override void OnLoadComplete()
  {
    if (Object.op_Equality((Object) this._collider, (Object) null))
    {
      CapsuleCollider capsuleCollider = ((Component) this).gameObject.AddComponent<CapsuleCollider>();
      capsuleCollider.direction = 1;
      capsuleCollider.height = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.height;
      capsuleCollider.radius = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.radius;
      capsuleCollider.center = new Vector3(0.0f, capsuleCollider.height * 0.5f, 0.0f);
      ((Collider) capsuleCollider).material.dynamicFriction = MonoBehaviourSingleton<InGameSettingsManager>.I.player.friction;
      ((Collider) capsuleCollider).material.staticFriction = MonoBehaviourSingleton<InGameSettingsManager>.I.player.friction;
      ((Collider) capsuleCollider).material.frictionCombine = (PhysicMaterialCombine) 2;
      this._collider = (Collider) capsuleCollider;
    }
    base.OnLoadComplete();
    this.SetAnimUpdatePhysics(this is Self);
    if (Object.op_Inequality((Object) this.stepCtrl, (Object) null))
      this.stepCtrl.stampDistance = this.playerParameter.stampDistance;
    this._physics = Utility.Find(((Component) this).transform, "SoftPhysics");
    if (Object.op_Inequality((Object) this._physics, (Object) null) && MonoBehaviourSingleton<StageObjectManager>.IsValid())
      ((Component) this._physics).transform.parent = MonoBehaviourSingleton<StageObjectManager>.I.physicsRoot;
    if (this.initBuffSyncParam != null)
    {
      this.buffParam.SetSyncParam(this.initBuffSyncParam);
      this.initBuffSyncParam = (BuffParam.BuffSyncParam) null;
    }
    else
      this.buffParam.PlayBuffLoopEffectAll();
    if (this.hp <= 0 && this.isInitDead)
    {
      this.ActDeadLoop(true, this.initRescueTime, this.initContinueTime);
      this.isInitDead = false;
    }
    this.snatchCtrl.OnLoadComplete();
    int index = 0;
    for (int count = this.m_weaponCtrlList.Count; index < count; ++index)
      this.m_weaponCtrlList[index].OnLoadComplete();
    if (this.pairSwordsCtrl == null)
      return;
    this.pairSwordsCtrl.ResetCombineMode();
  }

  protected override uint GetVoiceChannel()
  {
    return MonoBehaviourSingleton<SoundManager>.IsValid() ? MonoBehaviourSingleton<SoundManager>.I.GetVoiceChannel((StageObject) this) : 0U;
  }

  protected override bool EnablePlaySound()
  {
    return !MonoBehaviourSingleton<InGameProgress>.IsValid() || !MonoBehaviourSingleton<InGameProgress>.I.isHappenQuestDirection;
  }

  public override bool DestroyObject()
  {
    if (!this.isLoading)
      return base.DestroyObject();
    this.isDestroyWaitFlag = true;
    return false;
  }

  protected override void Update()
  {
    if (this.enableInputCharge)
    {
      this.inputChargeTimeCounter += Time.deltaTime;
      if ((double) this.inputChargeTimeCounter >= (double) this.inputChargeTimeMax)
      {
        if (this.inputChargeAutoRelease && (this.IsCoopNone() || this.IsOriginal()))
          this.SetChargeRelease(1f);
        if (this.inputChargeMaxTiming)
          this.ExecChargeMaxOnce();
      }
    }
    if (this.enableInputCharge && this.CheckAttackModeAndSpType(Player.ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.NONE) && !this.isChargeExpanding && (double) this.inputChargeTimeCounter >= (double) this.inputChargeTimeMax)
      this.SetNextTrigger(1);
    if (this.isChargeExpanding)
    {
      this.timerChargeExpand += Time.deltaTime;
      if (this.isChargeExpandAutoRelease && (double) this.timerChargeExpand >= (double) this.timeChargeExpandMax && (this.IsCoopNone() || this.IsOriginal()))
        this.SetChargeExpandRelease(1f);
    }
    if (this.isCountLongTouch && this.enableTap)
      this.countLongTouchSec += Time.deltaTime;
    if (this.isActSpecialAction)
      this.actSpecialActionTimer += Time.deltaTime;
    this.UpdateCannonCharge();
    this.UpdateSpearAction();
    this.snatchCtrl.Update();
    if (this.fishingCtrl != null)
      this.fishingCtrl.Update();
    int index1 = 0;
    for (int count = this.m_weaponCtrlList.Count; index1 < count; ++index1)
      this.m_weaponCtrlList[index1].Update();
    if (this._IsGuard() || this.spearCtrl.IsGuard())
      this._UpdateGuard();
    if (this.isHitSpAttack)
      this.hitSpAttackContinueTimer += Time.deltaTime;
    if (this.isSkillCastLoop)
      this.CheckSkillCastLoop();
    if (this.isArrowAimBossMode)
      this.UpdateArrowAngle();
    this.CheckBuffShadowSealing();
    if (this.healHp > this.hp && !this.buffParam.IsValidBuff(BuffParam.BUFFTYPE.CANT_HEAL_HP) && (double) this.buffParam.GetValue(BuffParam.BUFFTYPE.POISON) <= 0.0 && (double) this.buffParam.GetValue(BuffParam.BUFFTYPE.DEADLY_POISON) <= 0.0 && (double) this.buffParam.GetValue(BuffParam.BUFFTYPE.BURNING) <= 0.0 && (double) this.buffParam.GetValue(BuffParam.BUFFTYPE.ACID) <= 0.0 && !this.isProgressStop())
    {
      this.addHp += this.healHpSpeed * this.buffParam.GetHealSpeedUp() * this._GetGuardingHealSpeedUp() * Time.deltaTime;
      int addHp = (int) this.addHp;
      this.addHp -= (float) addHp;
      this.hp += addHp;
      if (this.hp > this.healHp)
        this.hp = this.healHp;
    }
    this.skillInfo.OnUpdate();
    this.UpdateSpActionGauge();
    this.CheckContinueBoostMode();
    this.UpdateEvolve();
    if (this.IsOriginal() || this.IsCoopNone())
    {
      bool flag1 = this.actionID == Character.ACTION_ID.IDLE || this.actionID == Character.ACTION_ID.MOVE || this.actionID == Character.ACTION_ID.ATTACK || this.actionID == (Character.ACTION_ID) 19 || this.actionID == (Character.ACTION_ID) 20 || this.actionID == (Character.ACTION_ID) 34 || this.actionID == (Character.ACTION_ID) 21 || this.actionID == (Character.ACTION_ID) 26 || this.actionID == (Character.ACTION_ID) 33 || this.actionID == (Character.ACTION_ID) 22 || this.actionID == Character.ACTION_ID.MAX || this.actionID == (Character.ACTION_ID) 37;
      this.prayerEndInfos.Clear();
      int index2 = 0;
      for (int count = this.prayTargetInfos.Count; index2 < count; ++index2)
      {
        if (!flag1)
        {
          this.prayerEndInfos.Add(this.prayTargetInfos[index2]);
        }
        else
        {
          Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(this.prayTargetInfos[index2].targetId) as Player;
          if (Object.op_Equality((Object) player, (Object) null))
            this.prayerEndInfos.Add(this.prayTargetInfos[index2]);
          else if ((double) Vector3.Distance(((Component) player).transform.position, ((Component) this).transform.position) > (double) this.playerParameter.revivalRange)
          {
            this.prayerEndInfos.Add(this.prayTargetInfos[index2]);
          }
          else
          {
            switch (this.prayTargetInfos[index2].reason)
            {
              case Player.PRAY_REASON.DEAD:
                if (!player.isDead || (double) player.rescueTime <= 0.0)
                {
                  this.prayerEndInfos.Add(this.prayTargetInfos[index2]);
                  continue;
                }
                continue;
              case Player.PRAY_REASON.STONE:
                if (!player.IsStone() || (double) player.stoneRescueTime <= 0.0)
                {
                  this.prayerEndInfos.Add(this.prayTargetInfos[index2]);
                  continue;
                }
                continue;
              default:
                continue;
            }
          }
        }
      }
      int index3 = 0;
      for (int count = this.prayerEndInfos.Count; index3 < count; ++index3)
        this.OnPrayerEnd(this.prayerEndInfos[index3]);
      if (flag1)
      {
        int index4 = 0;
        for (int count1 = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count; index4 < count1; ++index4)
        {
          Player player = MonoBehaviourSingleton<StageObjectManager>.I.playerList[index4] as Player;
          if (!Object.op_Equality((Object) player, (Object) this) && (double) Vector3.Distance(((Component) player).transform.position, ((Component) this).transform.position) <= (double) this.playerParameter.revivalRange)
          {
            bool flag2 = false;
            int index5 = 0;
            for (int count2 = this.prayTargetInfos.Count; index5 < count2; ++index5)
            {
              if (this.prayTargetInfos[index5].targetId == player.id)
              {
                flag2 = true;
                break;
              }
            }
            if (flag2)
            {
              this.CheckPrayerBoost(player);
            }
            else
            {
              Player.PrayInfo prayInfo = new Player.PrayInfo();
              prayInfo.reason = Player.PRAY_REASON.NONE;
              if (player.isDead && player.actionID == (Character.ACTION_ID) 24 && (double) player.rescueTime > 0.0)
                prayInfo.reason = Player.PRAY_REASON.DEAD;
              else if (player.IsStone() && (double) player.stoneRescueTime > 0.0)
                prayInfo.reason = Player.PRAY_REASON.STONE;
              if (prayInfo.reason != Player.PRAY_REASON.NONE)
              {
                prayInfo.targetId = player.id;
                this.OnPrayerStart(prayInfo);
              }
            }
          }
        }
      }
    }
    if (this.IsOnCannonMode())
    {
      if (this.targetFieldGimmickCannon != null)
        this._position = this.targetFieldGimmickCannon.GetPosition();
      if (this.isSyncingCannonRotation)
        this.UpdateSyncCannonRotation();
    }
    base.Update();
  }

  protected override void LateUpdate()
  {
    if (!this.fixWepData.enable)
      return;
    this.fixWepData.wepTrans.position = this.fixWepData.wepPos;
    this.fixWepData.wepTrans.rotation = this.fixWepData.wepRot;
  }

  protected virtual void OnDestroy()
  {
    if (Object.op_Inequality((Object) this._collider, (Object) null))
      Object.Destroy((Object) this._collider.material);
    if (Object.op_Inequality((Object) this._physics, (Object) null))
      Object.Destroy((Object) ((Component) this._physics).gameObject);
    this._physics = (Transform) null;
    if (this.fishingCtrl != null)
      this.fishingCtrl.TryFinalize();
    this.fishingCtrl = (FishingController) null;
    if (Object.op_Inequality((Object) this.activeBulletBarrierObject, (Object) null))
      this.activeBulletBarrierObject.DestroyObject();
    this.activeBulletBarrierObject = (BarrierBulletObject) null;
    if (this.buffParam != null && this.buffParam.substituteCtrl != null)
      this.buffParam.substituteCtrl.End();
    this.ClearStoreEffect();
    this.RemoveAllBulletTurret();
    this.EndCarry();
  }

  protected override void FixedUpdate()
  {
    if (this.actionID == (Character.ACTION_ID) 29)
      this.ActGrabbedUpdate();
    else if (this.actionID == (Character.ACTION_ID) 30)
      this.UpdateRestraint();
    else if (this.actionID == (Character.ACTION_ID) 43)
    {
      this.UpdateStone();
    }
    else
    {
      if (!this.pairSwordsBoostModeAuraEffectList.IsNullOrEmpty<Transform>())
      {
        for (int index = 0; index < this.pairSwordsBoostModeAuraEffectList.Count; ++index)
        {
          if (!Object.op_Equality((Object) this.pairSwordsBoostModeAuraEffectList[index], (Object) null))
            this.pairSwordsBoostModeAuraEffectList[index].position = this._position;
        }
      }
      if (this.enableRotateToTargetPoint)
      {
        Vector3 vector3_1 = this._forward;
        Vector3 pos;
        Vector3 vector3_2;
        if (this.GetTargetPos(out pos))
        {
          pos.y = 0.0f;
          vector3_1 = Vector3.op_Subtraction(pos, this._position);
        }
        else if (Vector3.op_Inequality(this.targetPointPos, Vector3.zero))
        {
          vector3_2 = this.targetPointPos;
          vector3_2.y = 0.0f;
          vector3_1 = Vector3.op_Subtraction(vector3_2, this._position);
        }
        else if (StageObjectManager.CanTargetBoss)
        {
          vector3_2 = this.GetTargetPosition((StageObject) MonoBehaviourSingleton<StageObjectManager>.I.boss);
          vector3_2.y = 0.0f;
          vector3_1 = Vector3.op_Subtraction(vector3_2, this._position);
        }
        if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL) && Object.op_Inequality((Object) this.targetPointWithSpWeak, (Object) null))
        {
          vector3_2 = this.targetPointWithSpWeak.param.markerPos;
          vector3_2.y = 0.0f;
          vector3_1 = Vector3.op_Subtraction(vector3_2, this._position);
        }
        Quaternion quaternion = Quaternion.LookRotation(vector3_1);
        this.rotateEventDirection = ((Quaternion) ref quaternion).eulerAngles.y;
        if (!this.periodicSyncActionPositionFlag)
          this.enableRotateToTargetPoint = false;
        Vector3 forward = this._forward;
        forward.y = 0.0f;
        ((Vector3) ref forward).Normalize();
        Vector3 vector3_3 = Quaternion.op_Multiply(Quaternion.AngleAxis(this.rotateEventDirection, Vector3.up), Vector3.forward);
        int num1 = (double) Vector3.Cross(forward, vector3_3).y >= 0.0 ? 1 : -1;
        float num2 = Vector3.Angle(forward, vector3_3);
        Quaternion rotation = this._rotation;
        Vector3 eulerAngles = ((Quaternion) ref rotation).eulerAngles;
        this._rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y + (float) num1 * num2, eulerAngles.z);
      }
      base.FixedUpdate();
      this.FixedUpdateOneHandSword();
      CapsuleCollider collider = this._collider as CapsuleCollider;
      if (!Object.op_Inequality((Object) collider, (Object) null))
        return;
      collider.center = new Vector3(0.0f, collider.height * 0.5f - this._position.y, 0.0f);
    }
  }

  protected override void FixedUpdatePhysics()
  {
    if (!this.isInitialized)
      return;
    Vector3 position = this._position;
    float height = StageManager.GetHeight(position);
    switch (this.actionID)
    {
      case Character.ACTION_ID.ATTACK | Character.ACTION_ID.FREEZE:
        if ((double) Time.time - (double) this.stumbleEndTime > 0.0)
        {
          this.SetNextTrigger();
          break;
        }
        break;
      case Character.ACTION_ID.PARALYZE | Character.ACTION_ID.FREEZE:
        if ((double) Time.time - (double) this.shakeEndTime > 0.0)
        {
          this.SetNextTrigger();
          break;
        }
        break;
      case (Character.ACTION_ID) 16 /*0x10*/:
      case (Character.ACTION_ID) 17:
      case (Character.ACTION_ID) 18:
      case (Character.ACTION_ID) 47:
        if (!this.waitAddForce && (this._rigidbody.constraints & 4) == null && (double) position.y <= (double) height + 0.029999999329447746 && (double) this._rigidbody.velocity.y <= 0.0)
        {
          this.ResetIgnoreColliders();
          this.SetNextTrigger();
        }
        if (this.isStunnedLoop && (double) this.stunnedEndTime - (double) Time.time <= 0.0)
        {
          this.SetStunnedEnd();
          break;
        }
        break;
    }
    base.FixedUpdatePhysics();
  }

  protected override float GetAnimatorSpeed()
  {
    float animatorSpeed1 = base.GetAnimatorSpeed();
    if (this.IsOracleTwoHandSword() && this.thsCtrl.oracleCtrl.IsHorizontalAttack)
      animatorSpeed1 *= this.thsCtrl.oracleCtrl.GetHorizontalSpeed();
    float animatorSpeed2 = animatorSpeed1 + this.GetBoostAttackSpeedUp() + this.GetAttackModeWalkSpeedUp();
    if ((double) animatorSpeed2 <= 0.0 || !this.enableAnimSeedRate)
      return animatorSpeed2;
    if (this.isLoopingRush)
      return Mathf.Max(0.0f, animatorSpeed2 * this.GetRushDistanceRate());
    if (this.IsBurstTwoHandSword() && this.thsCtrl != null && this.thsCtrl.IsEnableChangeReloadMotionSpeed)
      return this.GetBurstReloadMotionSpeedRate();
    float num = 0.0f;
    switch (this.attackMode)
    {
      case Player.ATTACK_MODE.TWO_HAND_SWORD:
        if (this.spAttackType != SP_ATTACK_TYPE.ORACLE)
        {
          num = this.buffParam.GetChargeSwordsTimeRate();
          break;
        }
        break;
      case Player.ATTACK_MODE.PAIR_SWORDS:
        num = this.buffParam.GetChargePairSwordsTimeRate();
        break;
      case Player.ATTACK_MODE.ARROW:
        num = this.GetChargeArrowTimeRate();
        break;
    }
    if ((double) num >= 1.0)
      num = this.playerParameter.animatorSpeedMaxTimeRate;
    return (float) (1.0 / (1.0 - (double) num));
  }

  public override bool IsChangeableAction(Character.ACTION_ID action_id)
  {
    if (this.disableActionFlag.Get((int) action_id))
      return false;
    switch (action_id)
    {
      case Character.ACTION_ID.MOVE:
        if (this.enableCancelToMove)
          return true;
        break;
      case Character.ACTION_ID.ATTACK:
        if (this.enableCancelToAttack && this.hitSpearSpecialAction && !this.lockedSpearCancelAction)
          return true;
        if ((double) this.actSpecialActionTimer > 0.0 && this.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.NONE))
        {
          this.lockedSpearCancelAction = true;
          return false;
        }
        if (this.enableCancelToAttack && this.spearCtrl.IsEnableBurstCombo() || this.IsChangeableActionOnSpAttack() || this.IsChangeableActionOnAttackNext() || this.enableCancelToAttack)
          return true;
        break;
      case Character.ACTION_ID.MAX:
      case (Character.ACTION_ID) 36:
      case (Character.ACTION_ID) 46:
      case (Character.ACTION_ID) 49:
        if (this.enableCancelToAvoid)
          return true;
        break;
      case (Character.ACTION_ID) 22:
      case (Character.ACTION_ID) 37:
        if (this.enableCancelToSkill)
          return true;
        break;
      case (Character.ACTION_ID) 29:
        if (this.actionID == (Character.ACTION_ID) 31 /*0x1F*/ || this.actionID == (Character.ACTION_ID) 32 /*0x20*/)
          return true;
        break;
      case (Character.ACTION_ID) 33:
        if (this.IsChangableActionOnWeaponAction() || this.enableCancelToSpecialAction)
          return true;
        break;
      case (Character.ACTION_ID) 38:
        if (this.enableCancelToEvolveSpecialAction)
          return true;
        break;
      case (Character.ACTION_ID) 42:
        if (this.enableFlickAction)
          return true;
        break;
    }
    return base.IsChangeableAction(action_id);
  }

  public float GetChargeArrowTimeRate()
  {
    float chargeArrowTimeRate1 = 0.0f;
    float chargeArrowTimeRate2;
    switch (this.spAttackType)
    {
      case SP_ATTACK_TYPE.NONE:
        chargeArrowTimeRate2 = this.buffParam.GetChargeArrowTimeRate();
        break;
      case SP_ATTACK_TYPE.HEAT:
        if (this.isBuffShadowSealing)
        {
          chargeArrowTimeRate2 = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.shadowSealingBuffChargeRate;
          break;
        }
        chargeArrowTimeRate2 = this.buffParam.GetChargeHeatArrowTimeRate();
        if (MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.ignoreSpAttackTypeAbility)
        {
          chargeArrowTimeRate2 += this.buffParam.GetChargeArrowTimeRate();
          break;
        }
        break;
      case SP_ATTACK_TYPE.BURST:
        chargeArrowTimeRate2 = this.buffParam.GetChargeArrowTimeRate();
        break;
      default:
        return chargeArrowTimeRate1;
    }
    if (this.isArrowSitShot)
      chargeArrowTimeRate2 += MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.sitShotChargeSpeedUpRate;
    if ((double) chargeArrowTimeRate2 >= 0.85000002384185791)
      chargeArrowTimeRate2 = 0.85f;
    return chargeArrowTimeRate2;
  }

  public float GetOracleChargeTimeRate()
  {
    switch (this.attackID)
    {
      case 62:
        return this.buffParam.GetOracleDiveSmashChargeTimeRate();
      case 63 /*0x3F*/:
        return this.buffParam.GetOracleSpinSmashChargeTimeRate();
      case 64 /*0x40*/:
        return this.buffParam.GetOracleWheelSmashChargeTimeRate();
      default:
        return 0.0f;
    }
  }

  public bool CheckAttackMode(Player.ATTACK_MODE mode) => this.attackMode == mode;

  public bool CheckSpAttackType(SP_ATTACK_TYPE type) => this.spAttackType == type;

  public bool CheckAttackModeAndSpType(Player.ATTACK_MODE mode, SP_ATTACK_TYPE type)
  {
    return this.attackMode == mode && this.spAttackType == type;
  }

  public ELEMENT_TYPE GetNowWeaponElement()
  {
    if (this.weaponState == null)
      return ELEMENT_TYPE.MAX;
    ELEMENT_TYPE nowWeaponElement = ELEMENT_TYPE.MAX;
    int num = 0;
    int index = 1;
    for (int count = this.weaponState.atkList.Count; index < count; ++index)
    {
      if (this.weaponState.atkList[index] > num)
      {
        num = this.weaponState.atkList[index];
        nowWeaponElement = (ELEMENT_TYPE) (index - 1);
      }
    }
    return nowWeaponElement;
  }

  public int GetNormalAttackId(
    Player.ATTACK_MODE mode,
    SP_ATTACK_TYPE type,
    EXTRA_ATTACK_TYPE exType,
    out string _motionLayerName)
  {
    int _attackId = 0;
    _motionLayerName = "Base Layer.";
    switch (mode)
    {
      case Player.ATTACK_MODE.ONE_HAND_SWORD:
        if (this.ohsCtrl != null)
        {
          this.ohsCtrl.GetNormalAttackId(type, exType, ref _attackId, ref _motionLayerName);
          break;
        }
        break;
      case Player.ATTACK_MODE.TWO_HAND_SWORD:
        if (this.thsCtrl != null)
        {
          this.thsCtrl.GetNormalAttackId(type, exType, ref _attackId, ref _motionLayerName);
          break;
        }
        break;
      case Player.ATTACK_MODE.SPEAR:
        if (this.spearCtrl != null)
        {
          this.spearCtrl.GetNormalAttackId(type, exType, ref _attackId, ref _motionLayerName);
          break;
        }
        break;
      case Player.ATTACK_MODE.PAIR_SWORDS:
        switch (type)
        {
          case SP_ATTACK_TYPE.HEAT:
            _attackId = this.playerParameter.pairSwordsActionInfo.Heat_AttackId;
            break;
          case SP_ATTACK_TYPE.SOUL:
            _attackId = this.playerParameter.pairSwordsActionInfo.Soul_AttackId;
            break;
          case SP_ATTACK_TYPE.BURST:
            _attackId = this.pairSwordsCtrl.GetBurstAttackId();
            break;
          case SP_ATTACK_TYPE.ORACLE:
            _attackId = 40;
            _motionLayerName = this.GetMotionLayerName(mode, type, _attackId);
            break;
        }
        break;
      case Player.ATTACK_MODE.ARROW:
        switch (type)
        {
          case SP_ATTACK_TYPE.HEAT:
            _attackId = 1;
            break;
          case SP_ATTACK_TYPE.SOUL:
            _attackId = 2;
            break;
          case SP_ATTACK_TYPE.BURST:
            _attackId = 3;
            break;
        }
        break;
    }
    return _attackId;
  }

  public void ReleaseEffect(ref Transform t, bool isPlayEndAnimation = true)
  {
    if (!MonoBehaviourSingleton<EffectManager>.IsValid() || t == null)
      return;
    EffectManager.ReleaseEffect(((Component) t).gameObject, isPlayEndAnimation);
    t = (Transform) null;
  }

  private bool IsChangeableActionOnSpAttack()
  {
    bool flag = false;
    switch (this.attackMode)
    {
      case Player.ATTACK_MODE.ONE_HAND_SWORD:
        if (this.spAttackType == SP_ATTACK_TYPE.BURST)
        {
          if (this.enableSpAttackContinue)
          {
            flag = true;
            break;
          }
          break;
        }
        if (this.spAttackType == SP_ATTACK_TYPE.ORACLE && this.enableSpAttackContinue)
        {
          flag = true;
          break;
        }
        break;
      case Player.ATTACK_MODE.TWO_HAND_SWORD:
        if ((this.spAttackType == SP_ATTACK_TYPE.NONE || this.spAttackType == SP_ATTACK_TYPE.HEAT || this.spAttackType == SP_ATTACK_TYPE.BURST || this.spAttackType == SP_ATTACK_TYPE.ORACLE) && this.enableSpAttackContinue)
          flag = true;
        if (!flag && this.spAttackType == SP_ATTACK_TYPE.HEAT)
        {
          if (this.isActSpecialAction && !this.isLockedSpAttackContinue)
          {
            this.isLockedSpAttackContinue = true;
            if (this.IsFullCharge() && this.isHitSpAttack && (this.thsCtrl == null || !this.thsCtrl.IsTwoHandSwordSpAttackContinueTimeOut(this.hitSpAttackContinueTimer)))
              flag = true;
            else
              break;
          }
          else
            break;
        }
        if (!flag && this.spAttackType == SP_ATTACK_TYPE.ORACLE && this.thsCtrl.oracleCtrl.IsHorizontalAttack)
        {
          this.thsCtrl.oracleCtrl.CheckNextHorizontal();
          flag = true;
          break;
        }
        break;
      case Player.ATTACK_MODE.SPEAR:
        if (this.enableSpAttackContinue)
        {
          flag = true;
          break;
        }
        break;
      case Player.ATTACK_MODE.PAIR_SWORDS:
        if (this.enableSpAttackContinue)
        {
          flag = true;
          break;
        }
        break;
      case Player.ATTACK_MODE.ARROW:
        if (this.enableSpAttackContinue && (this.spAttackType == SP_ATTACK_TYPE.NONE || this.spAttackType == SP_ATTACK_TYPE.HEAT || this.spAttackType == SP_ATTACK_TYPE.BURST))
        {
          flag = true;
          break;
        }
        break;
    }
    return flag;
  }

  private bool IsChangeableActionOnAttackNext()
  {
    bool flag = false;
    switch (this.attackMode)
    {
      case Player.ATTACK_MODE.SPEAR:
        if (this.CheckSpAttackType(SP_ATTACK_TYPE.BURST) && this.enableAttackNext)
        {
          flag = true;
          break;
        }
        break;
      case Player.ATTACK_MODE.PAIR_SWORDS:
        if (this.spAttackType == SP_ATTACK_TYPE.SOUL && this.enableAttackNext)
        {
          flag = true;
          break;
        }
        break;
    }
    return flag;
  }

  private bool IsChangableActionOnWeaponAction()
  {
    bool flag = false;
    if (this.attackMode == Player.ATTACK_MODE.SPEAR && this.CheckSpAttackType(SP_ATTACK_TYPE.BURST) && this.enableWeaponAction)
      flag = true;
    return flag;
  }

  protected override string ReplaceMotionLayer(int motionId, string layerName = "Base Layer.")
  {
    if (layerName.Equals("Base Layer."))
    {
      if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE))
      {
        switch (motionId)
        {
          case 2:
          case 3:
          case 6:
          case 7:
          case 8:
          case 115:
          case 117:
          case 118:
          case 119:
          case 120:
          case 124:
          case 126:
          case (int) sbyte.MaxValue:
            layerName += "ORACLE.";
            break;
        }
      }
      else if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.ORACLE) && motionId == 116)
        layerName += "ORACLE.";
    }
    return layerName;
  }

  public new bool PlayMotionImmidate(string ctrlName, string stateName, float _transition_time = -1f)
  {
    return this._PlayMotion("Base Layer." + stateName, ctrlName, _transition_time);
  }

  protected override bool CanSafeActIdle()
  {
    return this.fishingCtrl == null || !this.fishingCtrl.IsFishing();
  }

  public override void ActIdle(bool is_sync = false, float transitionTimer = -1f)
  {
    if (this.actionID == (Character.ACTION_ID) 29)
      this.ActGrabbedEnd();
    else if (this.actionID == (Character.ACTION_ID) 30)
    {
      this.ActRestraintEnd();
    }
    else
    {
      base.ActIdle(is_sync, transitionTimer);
      if (!Object.op_Inequality((Object) this.loader, (Object) null))
        return;
      this.loader.eyeBlink = true;
    }
  }

  public virtual void ActGuardWalk(Vector3 velocity_, float sync_speed, Vector3 move_vec)
  {
    if (this.actionID == (Character.ACTION_ID) 19)
      this.notEndGuardFlag = true;
    this.ActMoveVelocity(velocity_, sync_speed, Character.MOTION_ID.ATTACK_ID_END | Character.MOTION_ID.PARALYZE);
    this.notEndGuardFlag = false;
    this.SetGuardWalkRotation(move_vec);
    this.isActSpecialAction = true;
    this.isGuardWalk = true;
  }

  public void SetGuardWalkRotation(Vector3 move_vec)
  {
    Vector3 velocity;
    if (Object.op_Inequality((Object) this.actionTarget, (Object) null) && !this.IsValidBuffBlind())
    {
      velocity = Vector3.op_Subtraction(this.actionTarget._position, this._position);
      velocity.y = 0.0f;
      ((Vector3) ref velocity).Normalize();
      this.SetLerpRotation(velocity);
    }
    else
    {
      velocity = move_vec;
      this.SetLerpRotation(move_vec);
    }
    float num1 = Vector3.Angle(velocity, move_vec);
    float num2 = (double) Vector3.Cross(velocity, move_vec).y < 0.0 ? num1 / 360f : (float) ((360.0 - (double) num1) / 360.0);
    this.animator.SetFloat(Player.guardAngleID, num2);
  }

  public override void ActMoveSyncVelocity(float time, Vector3 pos, int motion_id)
  {
    bool flag = false;
    if (this.actionID != Character.ACTION_ID.MOVE)
      flag = true;
    base.ActMoveSyncVelocity(time, pos, motion_id);
    if (motion_id == 122)
    {
      this.isActSpecialAction = true;
      this.isGuardWalk = true;
      this.moveSyncDirection = float.MinValue;
      this.SetGuardWalkRotation(this.GetVelocity());
    }
    if (!flag)
      return;
    PlayerLoader.SetLayerWithChildren_SecondaryNoChange(this._transform, 20);
  }

  public override void SetMoveSyncVelocityEnd(
    float time,
    Vector3 pos,
    float direction,
    float sync_speed,
    int motion_id)
  {
    base.SetMoveSyncVelocityEnd(time, pos, direction, sync_speed, motion_id);
    if (motion_id != 122)
      return;
    this.moveSyncDirection = float.MinValue;
    this.SetGuardWalkRotation(this.GetVelocity());
  }

  public void ActAttackFailure(int id, bool isSendPacket) => base.ActAttack(id, isSendPacket);

  public override void ActAttack(
    int id,
    bool send_packet = true,
    bool sync_immediately = false,
    string _motionLayerName = "",
    string _motionStateName = "")
  {
    base.ActAttack(id, send_packet, sync_immediately, _motionLayerName, _motionStateName);
    if (this.isArrowAimBossMode)
      this.SetArrowAimBossVisible(true);
    if (this.isArrowAimLesserMode)
      this.SetArrowAimLesserVisible(true);
    this.UpdateArrowAngle();
    if ((this.IsCoopNone() || this.IsOriginal()) && this.IsValidBuff(BuffParam.BUFFTYPE.BLEEDING))
    {
      int[] source = (int[]) null;
      switch (this.attackMode)
      {
        case Player.ATTACK_MODE.ONE_HAND_SWORD:
          source = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.bleedingParam.ignoreOneHandSwordAtkIds;
          break;
        case Player.ATTACK_MODE.TWO_HAND_SWORD:
          source = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.bleedingParam.ignoreTwoHandSwordAtkIds;
          break;
        case Player.ATTACK_MODE.SPEAR:
          source = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.bleedingParam.ignoreSpearAtkIds;
          break;
        case Player.ATTACK_MODE.PAIR_SWORDS:
          source = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.bleedingParam.ignorePairSwordAtkIds;
          break;
        case Player.ATTACK_MODE.ARROW:
          source = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.bleedingParam.ignoreArrowAtkIds;
          break;
      }
      if (source == null || !((IEnumerable<int>) source).Contains<int>(id))
        this.buffParam.OnBleeding();
    }
    for (int index = 0; index < this.m_weaponCtrlList.Count; ++index)
      this.m_weaponCtrlList[index].OnActAttack(id);
  }

  public override void SetAttackActionPosition()
  {
    if (Object.op_Inequality((Object) this.targetingPoint, (Object) null))
    {
      this.SetActionPosition(this.targetingPoint.param.targetPos, true);
      if (!Object.op_Equality((Object) this.targetingPoint.owner, (Object) null))
        return;
      this.actionPositionThroughFlag = true;
    }
    else if (StageObjectManager.CanTargetBoss)
      this.SetActionPosition(this.GetTargetPosition((StageObject) MonoBehaviourSingleton<StageObjectManager>.I.boss), true);
    else
      this.SetActionPosition(Vector3.zero, false);
  }

  public bool InputAttackCombo()
  {
    if (!this.enableInputCombo)
      return false;
    this.inputComboFlag = true;
    if (this.enableComboTrans)
      this.ActAttackCombo();
    return true;
  }

  public void CancelAttackCombo() => this.inputComboFlag = false;

  public void ActAttackCombo()
  {
    int inputComboId = this.inputComboID;
    string motionLayerName = this.GetMotionLayerName(this.attackMode, this.spAttackType, inputComboId);
    if (this.thsCtrl != null && !this.thsCtrl.GetActAttackComboParam(ref inputComboId, ref motionLayerName) || this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.BURST) && inputComboId == MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.burstOHSInfo.CounterAttackId && !this.isSuccessParry)
      return;
    if (this.ohsCtrl != null && this.ohsCtrl.CheckActAttackCombo(inputComboId))
      this.FinishBoostMode();
    this.ActAttack(inputComboId, false, false, motionLayerName, this.inputComboMotionState);
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnActAttackCombo(inputComboId, motionLayerName, this.inputComboMotionState);
  }

  public float GetChargingRate()
  {
    float chargingRate;
    if (this.enableInputCharge)
    {
      chargingRate = (double) this.inputChargeTimeMax - (double) this.inputChargeTimeOffset > 0.0 ? (float) (((double) this.inputChargeTimeCounter - (double) this.inputChargeTimeOffset) / ((double) this.inputChargeTimeMax - (double) this.inputChargeTimeOffset)) : (!this.isInputChargeExistOffset ? 1f : ((double) this.inputChargeTimeCounter < (double) this.inputChargeTimeMax ? 0.0f : 1f));
      if ((double) chargingRate < 0.0)
        chargingRate = 0.0f;
      else if ((double) chargingRate > 1.0)
        chargingRate = 1f;
    }
    else
      chargingRate = this.chargeRate;
    return chargingRate;
  }

  public float GetChargeExpandingRate()
  {
    return (double) this.timeChargeExpandMax <= 0.0 ? 0.0f : Mathf.Clamp01((this.timerChargeExpand + this.timerChargeExpandOffset) / this.timeChargeExpandMax);
  }

  private void CheckInputCharge()
  {
    if (!this.IsCoopNone() && !this.IsOriginal() || this.enableTap || !this.enableInputCharge)
      return;
    this.SetChargeRelease(this.GetChargingRate());
  }

  private void CheckChargeExpand()
  {
    if (!this.IsCoopNone() && !this.IsOriginal() || this.enableTap || !this.isChargeExpanding)
      return;
    this.SetChargeExpandRelease(this.GetChargeExpandingRate());
  }

  private void CheckCannonCharge()
  {
    if (!this.IsCoopNone() && !this.IsOriginal() || this.enableTap || this.cannonState != Player.CANNON_STATE.CHARGE)
      return;
    this.SetCannonState(Player.CANNON_STATE.READY);
    if (!this.IsCannonFullCharged())
      return;
    this.ActCannonShot();
  }

  public void CheckSnatchMove()
  {
    if (!this.IsCoopNone() && !this.IsOriginal() || !this.snatchCtrl.IsMove())
      return;
    Vector3 snatchPos = this.snatchCtrl.GetSnatchPos();
    if (this.IsArrivalPosition(snatchPos))
      this.OnSnatchMoveEnd(2);
    else
      this.OnSnatchMoveStart(snatchPos);
  }

  public void OnSnatchMoveStart(Vector3 snatchPos)
  {
    this.EventMoveEnd();
    this.EndRotate();
    this.enableEventMove = true;
    this.enableAddForce = false;
    this.eventMoveVelocity = Vector3.op_Multiply(Vector3.forward, this.playerParameter.ohsActionInfo.Soul_SnatchMoveVelocity);
    Vector3 vector3 = Vector3.op_Subtraction(snatchPos, this._position);
    this.SetVelocity(Quaternion.op_Multiply(Quaternion.LookRotation(((Vector3) ref vector3).normalized), this.eventMoveVelocity), Character.VELOCITY_TYPE.EVENT_MOVE);
    this.snatchCtrl.StartMoveLoop();
    if (this.snatchCtrl.IsShotReleased())
      this.SetNextTrigger(3);
    else
      this.SetNextTrigger();
    this.StartWaitingPacket(StageObject.WAITING_PACKET.PLAYER_ONE_HAND_SWORD_MOVE_END, true);
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnSnatchMoveStart(snatchPos);
  }

  public void OnSnatchMoveEnd(int triggerIndex = 0)
  {
    this.DeactiveSnatchMove();
    this.EndRotate();
    this.SetNextTrigger(triggerIndex);
    this.EndWaitingPacket(StageObject.WAITING_PACKET.PLAYER_ONE_HAND_SWORD_MOVE_END);
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnSnatchMoveEnd(triggerIndex);
  }

  public virtual void StartCannonCharge()
  {
    FieldGimmickCannonSpecial fieldGimmickCannon = this.targetFieldGimmickCannon as FieldGimmickCannonSpecial;
    if (Object.op_Inequality((Object) fieldGimmickCannon, (Object) null))
    {
      if (!fieldGimmickCannon.IsAbleToShot())
        return;
      fieldGimmickCannon.StartCharge();
    }
    this.SetCannonState(Player.CANNON_STATE.CHARGE);
  }

  public virtual void UpdateCannonCharge()
  {
    FieldGimmickCannonSpecial fieldGimmickCannon = this.targetFieldGimmickCannon as FieldGimmickCannonSpecial;
    if (Object.op_Equality((Object) fieldGimmickCannon, (Object) null))
      return;
    if (this.cannonState == Player.CANNON_STATE.CHARGE)
    {
      this.inputCannonChargeCounter += Time.deltaTime;
      if ((double) this.inputCannonChargeMax <= 0.0 || (double) this.inputCannonChargeCounter < (double) this.inputCannonChargeMax)
        ;
    }
    else
    {
      this.inputCannonChargeCounter -= Time.deltaTime;
      fieldGimmickCannon.ReleaseCharge();
    }
    this.inputCannonChargeCounter = Mathf.Clamp(this.inputCannonChargeCounter, 0.0f, this.inputCannonChargeMax);
  }

  public virtual void SetChargeRelease(float charge_rate)
  {
    if (this.CheckAttackMode(Player.ATTACK_MODE.TWO_HAND_SWORD) && (double) charge_rate >= 1.0)
      this.IncrementCleaveComboCount();
    if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.NONE) && (double) charge_rate < 1.0)
    {
      this.inputChargeAutoRelease = true;
      this.inputChargeMaxTiming = false;
    }
    else
    {
      this.chargeRate = charge_rate;
      this.enableMotionCancel = false;
      this.enableCancelToAvoid = false;
      this.enableCancelToMove = false;
      this.enableCancelToAttack = false;
      this.enableCancelToSkill = false;
      this.enableCancelToSpecialAction = false;
      this.enableCancelToEvolveSpecialAction = false;
      this.enableCancelToCarryPut = false;
      if (this.CheckAttackMode(Player.ATTACK_MODE.SPEAR))
      {
        switch (this.spAttackType)
        {
          case SP_ATTACK_TYPE.NONE:
            if (this.isCanRushRelease)
            {
              this.actSpecialActionTimer = 0.0f;
              this.isActSpecialAction = true;
              if (this.isChargeExRush)
              {
                this.exRushChargeRate = this.chargeRate;
                this.evolveCtrl.PlayLeviathanEffect();
              }
              else
                this.exRushChargeRate = 0.0f;
              this.SetNextTrigger();
            }
            else
              this.ActIdle(false, -1f);
            this.ReleaseEffect(ref this.exRushChargeEffect);
            break;
          case SP_ATTACK_TYPE.HEAT:
            if (this.isSpearJumpAim)
            {
              if ((double) this.chargeRate >= 1.0)
              {
                this._JumpRize();
                break;
              }
              this.ActIdle(false, -1f);
              break;
            }
            break;
          case SP_ATTACK_TYPE.SOUL:
            this.spearCtrl.StoreChargeRate(this.chargeRate);
            this.SetNextTrigger();
            break;
          case SP_ATTACK_TYPE.ORACLE:
            if (!this.spearCtrl.IsGuard() && (this.spearCtrl.OracleSpCharged || this.IsValidBuff(BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS)))
            {
              this.SetNextTrigger(1);
              this.isActSpecialAction = true;
              break;
            }
            if (!this.spearCtrl.IsGuard())
            {
              this.SetNextTrigger();
              this.isActSpecialAction = true;
              break;
            }
            this.SetNextTrigger();
            break;
        }
      }
      else if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
      {
        if (this.thsCtrl != null)
          this.thsCtrl.SetSoulChargeRelease();
      }
      else if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.ORACLE))
      {
        switch (this.attackID)
        {
          case 62:
          case 63 /*0x3F*/:
          case 64 /*0x40*/:
            this.SetNextTrigger((double) this.chargeRate >= 1.0 ? 1 : 0);
            break;
          default:
            this.SetNextTrigger();
            break;
        }
      }
      else if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.NONE))
      {
        if ((double) this.chargeRate >= 1.0)
          this.SetNextTrigger();
      }
      else if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.SOUL))
        this.SetNextTrigger(MonoBehaviourSingleton<TargetMarkerManager>.I.GetMultiLockNum() > 0 ? 0 : 1);
      else if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.BURST))
      {
        if (this.isArrowRainShot)
          this.RainShotChargeRelease();
        this.SetNextTrigger();
      }
      else if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.ORACLE))
      {
        if (this.isBoostMode)
          this.SetNextTrigger(1);
        else
          this.SetNextTrigger();
      }
      else
        this.SetNextTrigger();
      this.enableInputCharge = false;
      this.inputChargeAutoRelease = false;
      this.inputChargeMaxTiming = false;
      this.inputChargeTimeMax = 0.0f;
      this.inputChargeTimeOffset = 0.0f;
      this.inputChargeTimeCounter = 0.0f;
      this.isInputChargeExistOffset = false;
      this.isSpearJumpAim = false;
      if (this.IsCoopNone() || this.IsOriginal())
        this.SetAttackActionPosition();
      this.EndWaitingPacket(StageObject.WAITING_PACKET.PLAYER_CHARGE_RELEASE);
      if (this.isArrowAimLesserMode)
        this.UpdateArrowAimLesserMode(Vector2.zero);
      this.UpdateArrowAngle();
      if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
        return;
      this.playerSender.OnSetChargeRelease(charge_rate, this.isChargeExRush);
    }
  }

  public virtual void SetChargeExpandRelease(float chargeExpandRate)
  {
    this.chargeExpandRate = chargeExpandRate;
    this.chargeRate = 1f;
    this.enableMotionCancel = false;
    this.enableCancelToAvoid = false;
    this.enableCancelToMove = false;
    this.enableCancelToAttack = false;
    this.enableCancelToSkill = false;
    this.enableCancelToSpecialAction = false;
    this.enableCancelToEvolveSpecialAction = false;
    this.enableCancelToCarryPut = false;
    this.SetNextTrigger();
    this.enableInputCharge = false;
    this.inputChargeAutoRelease = false;
    this.inputChargeMaxTiming = false;
    this.inputChargeTimeMax = 0.0f;
    this.inputChargeTimeOffset = 0.0f;
    this.inputChargeTimeCounter = 0.0f;
    this.isInputChargeExistOffset = false;
    this.isChargeExpanding = false;
    this.isChargeExpandAutoRelease = false;
    if (this.IsCoopNone() || this.IsOriginal())
      this.SetAttackActionPosition();
    this.EndWaitingPacket(StageObject.WAITING_PACKET.PLAYER_CHARGE_RELEASE);
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnSetChargeExpandRelease(this.chargeExpandRate);
  }

  public void ExecChargeMaxOnce()
  {
    this.inputChargeMaxTiming = false;
    if (this.isNpc || !this.IsCoopNone() && !this.IsOriginal())
      return;
    switch (this.attackMode)
    {
      case Player.ATTACK_MODE.TWO_HAND_SWORD:
        if (this.spAttackType == SP_ATTACK_TYPE.SOUL)
        {
          if (this.twoHandSwordsChargeMaxEffect == null)
            this.twoHandSwordsChargeMaxEffect = EffectManager.GetEffect(MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.soulIaiChargeMaxEffect, this.FindNode("R_Wep"));
          SoundManager.PlayOneShotSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.soulIaiChargeMaxSeId, (DisableNotifyMonoBehaviour) this, this.FindNode(""));
          break;
        }
        if (this.spAttackType != SP_ATTACK_TYPE.ORACLE)
          break;
        this.thsCtrl.oracleCtrl.StartVernierEffect(true);
        break;
      case Player.ATTACK_MODE.SPEAR:
        if (this.spAttackType == SP_ATTACK_TYPE.NONE)
        {
          if (this.isChargeExRush)
          {
            this.ReleaseEffect(ref this.exRushChargeEffect);
            EffectManager.OneShot("ef_btl_wsk_charge_end_01", ((Component) this.FindNode("R_Wep")).transform.position, Quaternion.identity);
            this.exRushChargeEffect = EffectManager.GetEffect("ef_btl_wsk_charge_loop_02", this.FindNode("R_Wep"));
            SoundManager.PlayOneShotSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.exRushChargeMaxSeId, (DisableNotifyMonoBehaviour) this, this.FindNode(""));
            break;
          }
          this._StartExRushCharge();
          break;
        }
        if (this.spAttackType == SP_ATTACK_TYPE.HEAT)
        {
          if (!this.EnablePlaySound())
            break;
          SoundManager.PlayOneShotSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.jumpChargeMaxSeId, (DisableNotifyMonoBehaviour) this, this.FindNode(""));
          break;
        }
        if (this.spAttackType != SP_ATTACK_TYPE.SOUL)
          break;
        Transform node = this.FindNode("R_Wep");
        EffectManager.OneShot("ef_btl_wsk_charge_end_01", Vector3.op_Addition(node.position, Quaternion.op_Multiply(node.rotation, MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.Soul_SpAttackMaxChargeEffectOffsetPos)), Quaternion.identity);
        SoundManager.PlayOneShotSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.exRushChargeMaxSeId, (DisableNotifyMonoBehaviour) this, this.FindNode(""));
        this.spearCtrl.ExecBladeEffect();
        break;
      case Player.ATTACK_MODE.PAIR_SWORDS:
        if (this.spAttackType != SP_ATTACK_TYPE.NONE)
          break;
        EffectManager.OneShot("ef_btl_wsk_charge_end_01", ((Component) this.FindNode("R_Wep")).transform.position, Quaternion.identity);
        EffectManager.OneShot("ef_btl_wsk_charge_end_01", ((Component) this.FindNode("L_Wep")).transform.position, Quaternion.identity);
        SoundManager.PlayOneShotSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.wildDanceChargeMaxSeId, (DisableNotifyMonoBehaviour) this, this.FindNode(""));
        break;
    }
  }

  public void SetEnableTap(bool enable)
  {
    this.enableTap = enable;
    if (this.isChargeExpanding)
      this.CheckChargeExpand();
    else
      this.CheckInputCharge();
    if (enable)
      return;
    if (this.cannonState == Player.CANNON_STATE.CHARGE)
      this.CheckCannonCharge();
    this.snatchCtrl.OnRelease();
    int index = 0;
    for (int count = this.m_weaponCtrlList.Count; index < count; ++index)
      this.m_weaponCtrlList[index].OnRelease();
    this.countLongTouchSec = 0.0f;
  }

  private void IncrementCleaveComboCount()
  {
    if ((this.IsCoopNone() || this.IsOriginal()) && this.buffParam.IncrementCleaveComboConditionAbility())
      EffectManager.GetEffect("ef_btl_ab_charge_01", this._transform);
    this.buffParam.ResetStack();
  }

  public virtual void SetInputAxis(Vector2 input_vec)
  {
    if (!this.enableInputRotate)
      return;
    if (!this.startInputRotate)
    {
      this.startInputRotate = true;
      this.rotateEventSpeed = 0.0f;
      this.rotateEventDirection = 0.0f;
      this.rotateEventKeep = false;
    }
    Vector3 right = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.right;
    Vector3 forward = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.forward;
    right.y = 0.0f;
    ((Vector3) ref right).Normalize();
    forward.y = 0.0f;
    ((Vector3) ref forward).Normalize();
    this._rotation = Quaternion.LookRotation(Vector3.op_Addition(Vector3.op_Multiply(right, input_vec.x), Vector3.op_Multiply(forward, input_vec.y)));
  }

  public void ActAvoid()
  {
    this.EndAction();
    this.actionID = Character.ACTION_ID.MAX;
    if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL))
      this.PlayMotion(136);
    else
      this.PlayMotion(115);
    this.actionMoveRate = this.buffParam.GetAvoidUp();
    this.actionMoveRate += this.GetAttackModeAvoidUp();
    this.buffParam.OnAvoid();
    this.snatchCtrl.Cancel();
    int index = 0;
    for (int count = this.m_weaponCtrlList.Count; index < count; ++index)
      this.m_weaponCtrlList[index].OnActAvoid();
    if (Object.op_Inequality((Object) this.playerSender, (Object) null))
      this.playerSender.OnActAvoid();
    if (!FieldManager.IsValidInTutorial())
      return;
    InGameTutorialManager component = ((Component) MonoBehaviourSingleton<AppMain>.I).GetComponent<InGameTutorialManager>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.GetState<InGameTutorialManager.TutorialRolling>()?.AddRollingCount();
  }

  public void ActWarp()
  {
    this.EndAction();
    this.actionID = (Character.ACTION_ID) 36;
    if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL))
      this.PlayMotion(137);
    else
      this.PlayMotion(135);
    this.actionMoveRate = this.buffParam.GetAvoidUp();
    this.actionMoveRate += this.GetAttackModeAvoidUp();
    this.buffParam.OnAvoid();
    this.snatchCtrl.Cancel();
    int index = 0;
    for (int count = this.m_weaponCtrlList.Count; index < count; ++index)
      this.m_weaponCtrlList[index].OnActAvoid();
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnWarp();
  }

  public void ActTeleportAvoid()
  {
    this.EndAction();
    this.actionID = (Character.ACTION_ID) 46;
    this.PlayMotion(146);
    this.actionMoveRate = this.buffParam.GetTeleportUp();
    this.buffParam.OnAvoid();
    this.snatchCtrl.Cancel();
    int index = 0;
    for (int count = this.m_weaponCtrlList.Count; index < count; ++index)
      this.m_weaponCtrlList[index].OnActAvoid();
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnTeleportAvoid();
  }

  public void ActRushAvoid(Vector3 inputVec)
  {
    this.EndAction();
    Vector3 velocity;
    if (Object.op_Inequality((Object) this.actionTarget, (Object) null) && !this.IsValidBuffBlind())
    {
      velocity = Vector3.op_Subtraction(this.actionTarget._position, this._position);
      velocity.y = 0.0f;
      ((Vector3) ref velocity).Normalize();
      this.SetLerpRotation(velocity);
    }
    else
    {
      velocity = inputVec;
      this.SetLerpRotation(inputVec);
    }
    float num1 = Vector3.Angle(velocity, inputVec);
    float num2 = (double) Vector3.Cross(velocity, inputVec).y < 0.0 ? num1 / 360f : (float) ((360.0 - (double) num1) / 360.0);
    this.animator.SetFloat(Player.rushAvoidAngleID, num2);
    this.actionID = (Character.ACTION_ID) 49;
    this.PlayMotion(147);
    this.buffParam.OnAvoid();
    this.snatchCtrl.Cancel();
    int index = 0;
    for (int count = this.m_weaponCtrlList.Count; index < count; ++index)
      this.m_weaponCtrlList[index].OnActAvoid();
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnRushAvoid(inputVec);
  }

  public virtual void ActRestraint(RestraintInfo restInfo)
  {
    this.EndAction();
    this.actionID = (Character.ACTION_ID) 30;
    this.PlayMotion(130);
    this._rigidbody.isKinematic = true;
    this._rigidbody.useGravity = false;
    this._rigidbody.constraints = (RigidbodyConstraints) 126;
    this._rigidbody.velocity = Vector3.zero;
    this.m_restrainTime = Time.time + restInfo.duration;
    this.m_restraintDamgeTimer = restInfo.damageInterval;
    this.m_restraintInfo = restInfo;
    AttackRestraintObject attackRestraintObject = new GameObject("AttackRestraintObject").AddComponent<AttackRestraintObject>();
    attackRestraintObject.Initialize(this, restInfo);
    this.m_attackRestraint = attackRestraintObject;
    if (restInfo.damageRate > 0)
      this.m_restrainDamageValue = (int) ((double) this.hpMax * (double) ((float) restInfo.damageRate * 0.01f));
    this.ClearLaser();
    this.pairSwordsCtrl.OnReaction();
    this.fishingCtrl.OnReaction();
    int index = 0;
    for (int count = this.m_weaponCtrlList.Count; index < count; ++index)
      this.m_weaponCtrlList[index].OnActReaction();
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnRestraintStart(restInfo);
  }

  public void ActRestraintEnd()
  {
    if (this.actionID != (Character.ACTION_ID) 30)
      return;
    this.EndAction();
    if (this.actionID == (Character.ACTION_ID) 17)
      return;
    this.ActFallBlow(Vector3.op_Multiply(Vector3.down, 10f));
  }

  private void PostRestraint()
  {
    if (Object.op_Equality((Object) this.m_attackRestraint, (Object) null))
      return;
    this._transform.position = MonoBehaviourSingleton<StageManager>.I.ClampInside(this._transform.position);
    if (this.m_restraintInfo.isStopMotion)
      this.setPause(false);
    this.m_restrainDamageValue = 0;
    this.m_restrainTime = 0.0f;
    this.m_restraintDamgeTimer = 0.0f;
    this.m_restraintInfo = (RestraintInfo) null;
    this._rigidbody.isKinematic = false;
    this._rigidbody.useGravity = true;
    this._rigidbody.constraints = (RigidbodyConstraints) 112 /*0x70*/;
    this._rigidbody.velocity = Vector3.zero;
    if (Object.op_Inequality((Object) this.m_attackRestraint, (Object) null))
      this.m_attackRestraint.DeleteThis();
    this.m_attackRestraint = (AttackRestraintObject) null;
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnRestraintEnd();
  }

  private void UpdateRestraint()
  {
    this.UpdateNextMotion();
    if (Object.op_Equality((Object) this.m_attackRestraint, (Object) null))
      return;
    if (this.m_restraintInfo.isStopMotion && !this.isPause)
    {
      AnimatorStateInfo animatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
      if (((AnimatorStateInfo) ref animatorStateInfo).fullPathHash == Animator.StringToHash("Base Layer.restraint"))
        this.setPause(true);
    }
    int restrainDamageValue = this.m_restrainDamageValue;
    if ((this.IsCoopNone() ? 1 : (this.IsOriginal() ? 1 : 0)) != 0 && restrainDamageValue > 0 && this.hp > 1)
    {
      this.m_restraintDamgeTimer -= Time.deltaTime;
      if ((double) this.m_restraintDamgeTimer <= 0.0)
      {
        if (this.IsValidShield())
        {
          Player.ShieldDamageData shieldDamageData = this.CalcShieldDamage(restrainDamageValue);
          this.ShieldHp = (XorInt) ((int) this.ShieldHp - shieldDamageData.shieldDamage);
          this.hp -= shieldDamageData.hpDamage;
        }
        else
          this.hp -= restrainDamageValue;
        if (this.hp <= 1)
          this.hp = 1;
        if (MonoBehaviourSingleton<UIDamageManager>.IsValid())
          MonoBehaviourSingleton<UIDamageManager>.I.CreatePlayerDamage((Character) this, restrainDamageValue, UIPlayerDamageNum.DAMAGE_COLOR.DAMAGE);
        this.m_restraintDamgeTimer = this.m_restraintInfo.damageInterval;
      }
    }
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
    {
      Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
      if (Object.op_Inequality((Object) boss, (Object) null) && boss.isDead)
      {
        this.ActRestraintEnd();
        return;
      }
    }
    if ((double) Time.time - (double) this.m_restrainTime <= 0.0)
      return;
    this.ActRestraintEnd();
  }

  public void ReduceRestraintTime()
  {
    RestraintInfo restraintInfo = this.m_restraintInfo;
    if (restraintInfo == null)
      return;
    float reduceTimeByFlick = restraintInfo.reduceTimeByFlick;
    if ((double) reduceTimeByFlick <= 0.0)
      return;
    this.m_restrainTime -= reduceTimeByFlick;
    if (!Object.op_Inequality((Object) this.m_attackRestraint, (Object) null))
      return;
    this.m_attackRestraint.OnFlick();
  }

  public TargetPoint RestraintTargetPoint
  {
    get
    {
      return !Object.op_Inequality((Object) this.m_attackRestraint, (Object) null) ? (TargetPoint) null : this.m_attackRestraint.BreakTargetPoint;
    }
  }

  public bool IsRestraint() => this.actionID == (Character.ACTION_ID) 30;

  private void CreateStoneEffect()
  {
    if (this.m_stoneEffect != null)
      return;
    Transform effect = EffectManager.GetEffect("ef_btl_pl_stone_01", this._transform);
    if (Object.op_Equality((Object) effect, (Object) null))
      return;
    this.m_stoneEffect = ((Component) effect).gameObject;
  }

  public virtual void ActStone()
  {
    this.EndAction();
    this.actionID = (Character.ACTION_ID) 43;
    this.PlayMotion(140);
    this.FinishBoostMode();
    this._rigidbody.isKinematic = true;
    this._rigidbody.useGravity = false;
    this._rigidbody.constraints = (RigidbodyConstraints) 126;
    this._rigidbody.velocity = Vector3.zero;
    this.CreateStoneEffect();
    this.OnStoneStart();
    this.prayerIds.Clear();
    this.boostPrayTargetInfoList.Clear();
    this.boostPrayedInfoList.Clear();
    if (MonoBehaviourSingleton<UIDeadAnnounce>.IsValid())
      MonoBehaviourSingleton<UIDeadAnnounce>.I.Announce(UIDeadAnnounce.ANNOUNCE_TYPE.STONE, this);
    Utility.MaterialForEach(this._rendererArray, (Action<Material>) (material =>
    {
      if (material.HasProperty(GameDefine.SHADER_PROPERTY_NAME_GRAYSCALE_RATE))
        material.SetFloat(GameDefine.SHADER_PROPERTY_NAME_GRAYSCALE_RATE, this.debuffParameter.stoneParam.grayScaleRate);
      if (!material.HasProperty(GameDefine.SHADER_PROPERTY_NAME_GRAYSCALE_POWER))
        return;
      material.SetFloat(GameDefine.SHADER_PROPERTY_NAME_GRAYSCALE_POWER, this.debuffParameter.stoneParam.grayScalePow);
    }));
    if (this.IsCoopNone() || this.IsOriginal())
      this.StoneCount(this.buffParam.GetStoneTime(), this.IsPrayed() || this.isStopCounter);
    this.UpdateRevivalRangeEffect();
    this.OnActReaction();
    this.ClearLaser();
    this.pairSwordsCtrl.OnReaction();
    this.fishingCtrl.OnReaction();
  }

  public void ActStoneEnd(float countTime)
  {
    this.EndAction();
    this.OnBuffEnd(BuffParam.BUFFTYPE.STONE, false, true);
    Utility.MaterialForEach(this._rendererArray, (Action<Material>) (material =>
    {
      if (material.HasProperty(GameDefine.SHADER_PROPERTY_NAME_GRAYSCALE_RATE))
        material.SetFloat(GameDefine.SHADER_PROPERTY_NAME_GRAYSCALE_RATE, 0.0f);
      if (!material.HasProperty(GameDefine.SHADER_PROPERTY_NAME_GRAYSCALE_POWER))
        return;
      material.SetFloat(GameDefine.SHADER_PROPERTY_NAME_GRAYSCALE_POWER, 0.0f);
    }));
    if ((double) countTime > 0.0)
    {
      this.PlayMotion(141);
      if (MonoBehaviourSingleton<UIDeadAnnounce>.IsValid())
        MonoBehaviourSingleton<UIDeadAnnounce>.I.Announce(UIDeadAnnounce.ANNOUNCE_TYPE.RESCUE_STONE, this);
    }
    else if (!this.isDead)
    {
      this.hp = 0;
      this.healHp = 0;
      this.ActDead(false, false);
    }
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnActStoneEnd(countTime);
  }

  private void PostStone()
  {
    if (Object.op_Inequality((Object) this.m_stoneEffect, (Object) null))
    {
      EffectManager.ReleaseEffect(this.m_stoneEffect);
      this.m_stoneEffect = (GameObject) null;
    }
    this._rigidbody.isKinematic = false;
    this._rigidbody.useGravity = true;
    this._rigidbody.constraints = (RigidbodyConstraints) 112 /*0x70*/;
    this._rigidbody.velocity = Vector3.zero;
  }

  private void UpdateStone()
  {
    this.UpdateNextMotion();
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
    if (!Object.op_Inequality((Object) boss, (Object) null) || !boss.isDead)
      return;
    this.ActStoneEnd(this.stoneRescueTime);
  }

  public override bool IsStone() => this.actionID == (Character.ACTION_ID) 43;

  public override void OnActReaction()
  {
    base.OnActReaction();
    int index = 0;
    for (int count = this.m_weaponCtrlList.Count; index < count; ++index)
      this.m_weaponCtrlList[index].OnActReaction();
    this.snatchCtrl.Cancel();
    this.pairSwordsCtrl.OnReaction();
    this.fishingCtrl.OnReaction();
    this.EndCarry();
  }

  public virtual void ActStumble(float time = 0.0f)
  {
    this.EndAction();
    this.actionID = Character.ACTION_ID.ATTACK | Character.ACTION_ID.FREEZE;
    this.PlayMotion(116);
    this.stumbleEndTime = Time.time + time;
    this.OnActReaction();
    this.ClearLaser();
  }

  public virtual void ActShake()
  {
    this.EndAction();
    this.actionID = Character.ACTION_ID.PARALYZE | Character.ACTION_ID.FREEZE;
    this.PlayMotion(117);
    this.shakeEndTime = Time.time + this.buffParam.GetShakeTime(this.playerParameter.shakeLoopTime);
    this.OnActReaction();
    this.ClearLaser();
  }

  public virtual void ActBlow(Vector3 force)
  {
    this.EndAction();
    this.actionID = (Character.ACTION_ID) 16 /*0x10*/;
    this.PlayMotion(118);
    if (Object.op_Inequality((Object) this._rigidbody, (Object) null))
      this.addForce = force;
    this.IgnoreEnemyColliders();
    this.OnActReaction();
    this.ClearLaser();
  }

  public virtual void InputBlowClear()
  {
    if ((this.IsCoopNone() || this.IsOriginal()) && !this.enableInputCombo)
      return;
    this.inputBlowClearFlag = true;
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnInputBlowClear();
  }

  public virtual void SetBlowClear(int index)
  {
    this.enableInputCombo = false;
    this.inputBlowClearFlag = false;
    this.SetNextTrigger(index + 1);
  }

  public virtual void ActFallBlow(Vector3 force)
  {
    this.EndAction();
    this.actionID = (Character.ACTION_ID) 17;
    this.PlayMotion(119);
    if (Object.op_Inequality((Object) this._rigidbody, (Object) null))
    {
      this.addForce = force;
      this.waitAddForce = true;
    }
    this.IgnoreEnemyColliders();
    this.OnActReaction();
    this.ClearLaser();
  }

  public virtual void ActStunnedBlow(Vector3 force, float time = 0.0f)
  {
    this.EndAction();
    this.actionID = (Character.ACTION_ID) 18;
    this.PlayMotion(120);
    if (Object.op_Inequality((Object) this._rigidbody, (Object) null))
      this.addForce = force;
    this.stunnedTime = this.buffParam.GetStumbleTime(time);
    this.IgnoreEnemyColliders();
    this.OnActReaction();
    this.ClearLaser();
  }

  public virtual void ActCharmBlow(Vector3 force, float time)
  {
    this.EndAction();
    this.actionID = (Character.ACTION_ID) 47;
    this.PlayMotion(120);
    if (Object.op_Inequality((Object) this._rigidbody, (Object) null))
      this.addForce = force;
    this.stunnedTime = this.buffParam.GetCharmTime(time);
    this.IgnoreEnemyColliders();
    this.OnActReaction();
    this.ClearLaser();
  }

  public virtual void ActGrabbedStart(int enemyId, GrabInfo grabInfo)
  {
    Enemy enemy = MonoBehaviourSingleton<StageObjectManager>.I.FindEnemy(enemyId) as Enemy;
    if (Object.op_Equality((Object) enemy, (Object) null))
      return;
    if ((int) enemy.GrabHpMax > 0)
      enemy.GrabHp = enemy.GrabHpMax;
    this.grabDrainAtkInfo = (DrainAttackInfo) null;
    if (grabInfo.drainAttackId > 0)
      this.grabDrainAtkInfo = enemy.SearchDrainAttackInfo(grabInfo.drainAttackId);
    this.EndAction();
    this.actionID = (Character.ACTION_ID) 29;
    this.PlayMotion(129);
    this.IgnoreEnemyColliders();
    Transform transform = Utility.Find(enemy._transform, grabInfo.parentNode);
    if (Object.op_Inequality((Object) transform, (Object) null))
    {
      this._transform.parent = transform;
      this._transform.localPosition = Vector3.zero;
      this._transform.localRotation = Quaternion.identity;
      this._rigidbody.isKinematic = true;
      this._rigidbody.useGravity = false;
      this._rigidbody.constraints = (RigidbodyConstraints) 126;
    }
    CircleShadow componentInChildren = ((Component) this).GetComponentInChildren<CircleShadow>();
    if (Object.op_Inequality((Object) componentInChildren, (Object) null))
      ((Component) componentInChildren).gameObject.SetActive(false);
    this.hitOffFlag |= StageObject.HIT_OFF_FLAG.GRAB;
    this.ClearLaser();
    this.pairSwordsCtrl.OnReaction();
    this.fishingCtrl.OnReaction();
    int index = 0;
    for (int count = this.m_weaponCtrlList.Count; index < count; ++index)
      this.m_weaponCtrlList[index].OnActReaction();
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnGrabbedStart(enemy.id, grabInfo.parentNode, grabInfo.duration, grabInfo.drainAttackId);
  }

  public virtual void ActGrabbedEnd(float angle = 0.0f, float power = 0.0f)
  {
    this.EndAction();
    this.grabDrainAtkInfo = (DrainAttackInfo) null;
    this._transform.position = MonoBehaviourSingleton<StageManager>.I.ClampInside(this._transform.position);
    CircleShadow componentInChildren = ((Component) this).GetComponentInChildren<CircleShadow>(true);
    if (Object.op_Inequality((Object) componentInChildren, (Object) null))
    {
      ((Component) componentInChildren).gameObject.SetActive(true);
      ((Component) componentInChildren).transform.localPosition = Vector3.zero;
    }
    this._transform.parent = MonoBehaviourSingleton<StageObjectManager>.I._transform;
    this._transform.localRotation = Quaternion.identity;
    this._rigidbody.isKinematic = false;
    this._rigidbody.useGravity = true;
    this._rigidbody.constraints = (RigidbodyConstraints) 112 /*0x70*/;
    this._rigidbody.velocity = Vector3.zero;
    Vector3 vector3 = Vector3.op_UnaryNegation(this._forward);
    this.ActFallBlow(Vector3.op_Multiply(Quaternion.op_Multiply(Quaternion.AngleAxis(angle, this._right), vector3), power));
    this.hitOffFlag &= ~StageObject.HIT_OFF_FLAG.GRAB;
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnGrabbedEnd();
  }

  public virtual void ActGrabbedUpdate()
  {
    this.UpdateNextMotion();
    if (this.grabDrainAtkInfo == null)
      return;
    int damage = (int) ((double) this.hpMax * (double) (this.grabDrainAtkInfo.damageRate * 0.01f));
    if ((this.IsCoopNone() ? 1 : (this.IsOriginal() ? 1 : 0)) == 0 || damage <= 0 || this.hp <= 1)
      return;
    this.grabDrainDamageTimer -= Time.deltaTime;
    if ((double) this.grabDrainDamageTimer > 0.0)
      return;
    if (this.IsValidShield())
    {
      Player.ShieldDamageData shieldDamageData = this.CalcShieldDamage(damage);
      this.ShieldHp = (XorInt) ((int) this.ShieldHp - shieldDamageData.shieldDamage);
      this.hp -= shieldDamageData.hpDamage;
    }
    else
      this.hp -= damage;
    if (this.hp <= 1)
      this.hp = 1;
    if (MonoBehaviourSingleton<UIDamageManager>.IsValid())
      MonoBehaviourSingleton<UIDamageManager>.I.CreatePlayerDamage((Character) this, damage, UIPlayerDamageNum.DAMAGE_COLOR.DAMAGE);
    this.grabDrainDamageTimer = this.grabDrainAtkInfo.damageInterval;
  }

  public virtual void ReduceStunnedTime()
  {
    if (!this.isStunnedLoop || (double) this.stunnedReduceEnableTime <= 0.0)
      return;
    float num = this.playerParameter.stunnedReduceTimeValue;
    if ((double) num > (double) this.stunnedReduceEnableTime)
      num = this.stunnedReduceEnableTime;
    this.stunnedEndTime -= num;
    this.stunnedReduceEnableTime -= num;
  }

  public virtual void SetStunnedEnd()
  {
    if (!this.isStunnedLoop)
      return;
    this.SetNextTrigger(1);
    this.isStunnedLoop = false;
    this.stunnedEndTime = 0.0f;
    this.UpdateStunnedEffect();
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnSetStunnedEnd();
  }

  protected void UpdateStunnedEffect()
  {
    bool flag = false;
    float num = 0.0f;
    if (this.isStunnedLoop && (double) this.stunnedTime > 0.0)
    {
      num = (float) (1.0 - (double) (this.stunnedEndTime - Time.time) / (double) this.stunnedTime);
      if ((double) num < 0.0)
        num = 0.0f;
      if ((double) num > 1.0)
        num = 1f;
      if ((double) num < 1.0)
        flag = true;
    }
    string[] strArray = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.stunnedEffectList;
    if (this.actionID == (Character.ACTION_ID) 47)
      strArray = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.charmEffectList;
    if (flag && strArray.Length != 0)
    {
      int index = (int) ((double) num * (double) strArray.Length);
      if (index == this.stunnedEffectIndex)
        return;
      this.stunnedEffectIndex = -1;
      if (Object.op_Inequality((Object) this.stunnedEffect, (Object) null))
      {
        EffectManager.ReleaseEffect(this.stunnedEffect);
        this.stunnedEffect = (GameObject) null;
      }
      if (string.IsNullOrEmpty(strArray[index]))
        return;
      this.stunnedEffectIndex = index;
      Transform effect = EffectManager.GetEffect(strArray[index], this.rootNode);
      if (!Object.op_Inequality((Object) effect, (Object) null))
        return;
      this.stunnedEffect = ((Component) effect).gameObject;
    }
    else
    {
      this.stunnedEffectIndex = -1;
      if (!Object.op_Inequality((Object) this.stunnedEffect, (Object) null))
        return;
      EffectManager.ReleaseEffect(this.stunnedEffect);
      this.stunnedEffect = (GameObject) null;
    }
  }

  public virtual void ActGuard()
  {
    if (this.actionID == (Character.ACTION_ID) 20 || this.actionID == (Character.ACTION_ID) 34 || this.actionID == (Character.ACTION_ID) 21 || this.isGuardWalk)
      this.notEndGuardFlag = true;
    this.EndAction();
    this.actionID = (Character.ACTION_ID) 19;
    this.PlayMotion(121);
    if (!this.notEndGuardFlag)
      this._StartGuard();
    this.notEndGuardFlag = false;
    this.isControllable = true;
    if (!this.IsCoopNone() && !this.IsOriginal())
      return;
    if (Object.op_Inequality((Object) this.actionTarget, (Object) null))
      this.SetActionPosition(this.actionTarget._position, true);
    else
      this.SetActionPosition(Vector3.zero, false);
  }

  public virtual void ActGuardDamage()
  {
    this.notEndGuardFlag = true;
    this.EndAction();
    this.notEndGuardFlag = false;
    if (this._CheckJustGuardSec())
    {
      this.isJustGuard = true;
      if (this.spAttackType == SP_ATTACK_TYPE.BURST)
      {
        string motionLayerName = this.GetMotionLayerName(this.attackMode, this.spAttackType, this.playerParameter.ohsActionInfo.burstOHSInfo.BaseAtkId);
        Character.PlayMotionParam playMotionParam = new Character.PlayMotionParam()
        {
          MotionID = 134,
          MotionLayerName = motionLayerName,
          TransitionTime = -1f
        };
        this.actionID = (Character.ACTION_ID) 21;
        this.PlayMotion(playMotionParam);
      }
      else
      {
        this.actionID = (Character.ACTION_ID) 34;
        this.PlayMotion(133);
      }
    }
    else
    {
      this.actionID = (Character.ACTION_ID) 20;
      this.PlayMotion(123);
    }
    this.OnActReaction();
  }

  public virtual bool ActBattleStart(bool effect_only = false)
  {
    if (this.isActedBattleStart)
      return false;
    if (Object.op_Inequality((Object) this.packetReceiver, (Object) null))
      this.packetReceiver.SetStopPacketUpdate(false);
    this.isActedBattleStart = true;
    this.isWaitBattleStart = false;
    if (effect_only)
    {
      if (FieldManager.IsValidInGameNoQuest())
        return true;
      string battleStartEffectName = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.battleStartEffectName;
      if (!string.IsNullOrEmpty(battleStartEffectName))
      {
        Transform effect = EffectManager.GetEffect(battleStartEffectName, this._transform);
        if (Object.op_Inequality((Object) effect, (Object) null))
          this.AddObjectList(((Component) effect).gameObject, Character.OBJECT_LIST_TYPE.STATIC);
      }
    }
    else
    {
      this.EndAction();
      this.actionID = (Character.ACTION_ID) 23;
      this.PlayMotion(124);
      if (Object.op_Inequality((Object) this.uiPlayerStatusGizmo, (Object) null))
        this.uiPlayerStatusGizmo.SetVisible(false);
      this.hitOffFlag |= StageObject.HIT_OFF_FLAG.INVICIBLE;
      this.SetEnableNodeRenderer((string) null, false);
      this.buffParam.substituteCtrl.ActiveEffectRoot();
    }
    return true;
  }

  public virtual void WaitBattleStart()
  {
    this.isWaitBattleStart = true;
    if (!Object.op_Inequality((Object) this.packetReceiver, (Object) null))
      return;
    this.packetReceiver.SetStopPacketUpdate(true);
  }

  public override void ActDead(bool force_sync = false, bool recieve_direct = false)
  {
    this.ActGrabbedEnd();
    this.ActRestraintEnd();
    this.FinishBoostMode();
    this.EndEvolve();
    this._EndGuard();
    this._EndBuffShadowSealing();
    this.EndCarry();
    this.badStatusTotal.Reset();
    this.CheckAutoRevive();
    base.ActDead(force_sync, recieve_direct);
    if (MonoBehaviourSingleton<UIDeadAnnounce>.IsValid())
      MonoBehaviourSingleton<UIDeadAnnounce>.I.Announce(UIDeadAnnounce.ANNOUNCE_TYPE.DEAD, this);
    this.skillInfo.ResetUseGauge();
    this.ResetSpActionGauge();
    this.evolveCtrl.ResetGauge(true);
    this.snatchCtrl.Cancel();
    int index = 0;
    for (int count = this.m_weaponCtrlList.Count; index < count; ++index)
      this.m_weaponCtrlList[index].OnActDead();
    this.ClearLaser();
    this.RemoveAllBulletTurret();
  }

  public virtual void ActDeadLoop(
    bool is_init_rescue = false,
    float set_rescue_time = 0.0f,
    float set_continue_time = 0.0f)
  {
    this.EndAction();
    this.actionID = (Character.ACTION_ID) 24;
    this.PlayMotion(125);
    this.isDead = true;
    this.prayerIds.Clear();
    this.boostPrayTargetInfoList.Clear();
    this.boostPrayedInfoList.Clear();
    this.hitOffFlag |= StageObject.HIT_OFF_FLAG.DEAD;
    this.IgnoreEnemyColliders();
    if (this.OnAutoRevive())
      return;
    int num1 = QuestManager.IsValidInGameExplore() ? 1 : 0;
    float rescue_time = 0.0f;
    float num2 = 0.0f;
    if (num1 != 0)
    {
      rescue_time = this.playerParameter.exploreRescureTime;
      num2 = this.playerParameter.continueTime;
    }
    else if (is_init_rescue)
    {
      rescue_time = set_rescue_time;
      num2 = set_continue_time;
    }
    else if (this.playerParameter.rescueTimes.Length != 0)
    {
      int index = this.rescueCount;
      if (this.playerParameter.rescueTimes.Length <= index)
        index = this.playerParameter.rescueTimes.Length - 1;
      rescue_time = this.playerParameter.rescueTimes[index];
      num2 = this.playerParameter.continueTime;
    }
    if (this.IsCoopNone() || this.IsOriginal())
      this.DeadCount(rescue_time, this.isStopCounter);
    this.continueTime = num2;
    this.UpdateRevivalRangeEffect();
  }

  private void DeactivateRescueTimer() => this.StopCounter(true);

  private void ActivateRescueTimer() => this.StopCounter(false);

  public void StopCounter(bool stop)
  {
    this.isStopCounter = stop;
    bool flag = false;
    if (MonoBehaviourSingleton<InGameProgress>.IsValid())
      flag = MonoBehaviourSingleton<InGameProgress>.I.disableSendProgressStop;
    if (!flag && Object.op_Inequality((Object) this.playerSender, (Object) null))
      this.playerSender.OnStopCounter(stop);
    if (!this.IsOriginal() && !this.IsCoopNone())
      return;
    this.DeadCount(this.rescueTime, stop || this.IsPrayed() || this.isWaitingResurrectionHoming);
  }

  public void DeadCount(float rescue_time, bool stop, bool syncRequested = false)
  {
    if (MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest())
      return;
    this._rescueTime = rescue_time;
    if (!stop)
    {
      this.deadStartTime = Time.time;
      this.deadStopTime = -1f;
    }
    else
    {
      this.deadStartTime = Time.time;
      this.deadStopTime = Time.time;
    }
    this.UpdateRevivalRangeEffect();
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null) || syncRequested)
      return;
    this.playerSender.OnDeadCount(this._rescueTime, stop);
  }

  public void StoneCount(float rescue_time, bool stop, bool syncRequested = false)
  {
    if (MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest())
      return;
    this._stoneRescueTime = rescue_time;
    if (!stop)
    {
      this.stoneStartTime = Time.time;
      this.stoneStopTime = -1f;
    }
    else
    {
      this.stoneStartTime = Time.time;
      this.stoneStopTime = Time.time;
    }
    this.UpdateRevivalRangeEffect();
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null) || syncRequested)
      return;
    this.playerSender.OnStoneCount(this._stoneRescueTime, stop);
  }

  public bool IsRescuable()
  {
    return (double) this.rescueTime > 0.0 && !this.isStopCounter && (double) this.deadStartTime >= 0.0;
  }

  public void UpdateRevivalRangeEffect()
  {
    if (this.actionID != (Character.ACTION_ID) 24 && this.actionID != (Character.ACTION_ID) 43 || MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo() || QuestManager.IsValidInGameTrial() || MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest() || QuestManager.IsValidInGameSeriesArena())
      return;
    if (this.IsRescuable() || this.IsStone())
    {
      if (!Object.op_Equality((Object) this.revivalRangEffect, (Object) null))
        return;
      Transform effect = EffectManager.GetEffect("ef_btl_rebirth_area_01", this._transform);
      if (!Object.op_Inequality((Object) effect, (Object) null))
        return;
      Vector3 localScale = effect.localScale;
      effect.localScale = Vector3.op_Multiply(localScale, this.playerParameter.revivalRange);
      this.revivalRangEffect = ((Component) effect).gameObject;
    }
    else
    {
      if (!Object.op_Inequality((Object) this.revivalRangEffect, (Object) null))
        return;
      EffectManager.ReleaseEffect(this.revivalRangEffect);
      this.revivalRangEffect = (GameObject) null;
    }
  }

  public void SyncDeadCount()
  {
    if (!this.isDead || MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest() || this.IsCoopNone() || !this.IsOriginal())
      return;
    this.DeadCount(this.rescueTime, this.IsPrayed() || this.isStopCounter || this.isWaitingResurrectionHoming);
  }

  public void SyncStoneCount()
  {
    if (!this.IsStone() || MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest() || this.IsCoopNone() || !this.IsOriginal())
      return;
    this.StoneCount(this.stoneRescueTime, this.IsPrayed() || this.isStopCounter);
  }

  public virtual void OnEndContinueTimeEnd()
  {
    if (!MonoBehaviourSingleton<InGameManager>.I.IsRush() && !QuestManager.IsValidInGameWaveMatch(true) || !MonoBehaviourSingleton<StageObjectManager>.I.playerList.TrueForAll((Predicate<StageObject>) (so =>
    {
      Player player = so as Player;
      if (!Object.op_Inequality((Object) player, (Object) null))
        return true;
      return player.isDead && !player.IsAutoReviving();
    })))
      return;
    MonoBehaviourSingleton<InGameProgress>.I.BattleRetire();
  }

  public virtual void ActDeadStandup(int standup_hp, Player.eContinueType cType)
  {
    this.EndAction();
    this.actionID = (Character.ACTION_ID) 25;
    this.PlayMotion(126);
    this.hitOffFlag |= StageObject.HIT_OFF_FLAG.INVICIBLE;
    this.isDead = false;
    this.hp = standup_hp;
    this.healHp = standup_hp;
    this.autoReviveHp = 0;
    this.isWaitingResurrectionHoming = false;
    this.PlayVoice(93);
    switch (cType)
    {
      case Player.eContinueType.CONTINUE:
        this.skillInfo.SetUseGaugeFull();
        MonoBehaviourSingleton<UIDeadAnnounce>.I.Announce(UIDeadAnnounce.ANNOUNCE_TYPE.CONTINUE, this);
        break;
      case Player.eContinueType.RESCUE:
        if (this.EnableRescueCountup())
          ++this.rescueCount;
        MonoBehaviourSingleton<UIDeadAnnounce>.I.Announce(UIDeadAnnounce.ANNOUNCE_TYPE.RESCURE, this);
        break;
      case Player.eContinueType.AUTO_REVIVE:
        ++this.autoReviveCount;
        MonoBehaviourSingleton<UIDeadAnnounce>.I.Announce(UIDeadAnnounce.ANNOUNCE_TYPE.AUTO_REVIVE, this);
        break;
      case Player.eContinueType.REACH_NEXT_WAVE:
        if (this.EnableRescueCountup())
          ++this.rescueCount;
        MonoBehaviourSingleton<UIDeadAnnounce>.I.Announce(UIDeadAnnounce.ANNOUNCE_TYPE.REACH_NEXT_WAVE, this);
        break;
    }
    if (standup_hp >= this.hpMax)
    {
      Transform effect = EffectManager.GetEffect("ef_btl_rebirth_01");
      if (Object.op_Inequality((Object) effect, (Object) null))
      {
        effect.position = this._position;
        effect.rotation = this._rotation;
        effect.localScale = Vector3.Scale(this._transform.lossyScale, effect.localScale);
      }
    }
    if (this.buffInfoListOnActDeadStandUp.Any<KeyValuePair<int, SkillInfo.SkillParam>>())
    {
      foreach (KeyValuePair<int, SkillInfo.SkillParam> keyValuePair in this.buffInfoListOnActDeadStandUp)
        this.StartBuffByBuffTableId(keyValuePair.Key, keyValuePair.Value);
      this.buffInfoListOnActDeadStandUp.Clear();
    }
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnActDeadStandup(standup_hp, cType);
  }

  public void RestartExplore()
  {
    this.isDead = false;
    this.hp = this.hpMax;
    if (!this.EnableRescueCountup())
      return;
    ++this.rescueCount;
  }

  public override void ActParalyze()
  {
    base.ActParalyze();
    this.ClearLaser();
    this.pairSwordsCtrl.OnReaction();
    this.fishingCtrl.OnReaction();
    this.paralyzeTime = Time.time + this.buffParam.GetParalyzeTime();
  }

  public void ActPrayer()
  {
    this.EndAction();
    this.actionID = (Character.ACTION_ID) 26;
    this.PlayMotion((int) sbyte.MaxValue);
    this.isControllable = true;
  }

  public void OnPrayerStart(Player.PrayInfo prayInfo)
  {
    if (MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest())
      return;
    if (this.prayTargetInfos.Count <= 0)
      this.StartWaitingPacket(StageObject.WAITING_PACKET.PLAYER_PRAYER_END, true);
    this.prayTargetInfos.Add(prayInfo);
    if (Object.op_Inequality((Object) this.playerSender, (Object) null))
      this.playerSender.OnPrayerStart(prayInfo);
    Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(prayInfo.targetId) as Player;
    if (Object.op_Equality((Object) player, (Object) null))
      return;
    Player.BoostPrayInfo info1 = this.boostPrayTargetInfoList.Find((Predicate<Player.BoostPrayInfo>) (item => item.prayTargetId == prayInfo.targetId));
    if (info1 == null)
    {
      Player.BoostPrayInfo info2 = new Player.BoostPrayInfo();
      info2.prayerId = this.id;
      info2.prayTargetId = prayInfo.targetId;
      info2.isBoostByTypes[0] = !this.isNpc && this._IsGuard(SP_ATTACK_TYPE.NONE);
      info2.isBoostByTypes[1] = this.IsInBarrier() && player.IsInBarrier();
      if (info2.IsBoost())
        this.boostPrayTargetInfoList.Add(info2);
      player.StartPrayed(this.id, info2);
    }
    else
    {
      info1.isBoostByTypes[0] = !this.isNpc && this._IsGuard(SP_ATTACK_TYPE.NONE);
      info1.isBoostByTypes[1] = this.IsInBarrier() && player.IsInBarrier();
      player.StartPrayed(this.id, info1);
    }
  }

  public virtual void OnPrayerEnd(Player.PrayInfo prayInfo)
  {
    if (Object.op_Inequality((Object) this.playerSender, (Object) null))
      this.playerSender.OnPrayerEnd(prayInfo);
    for (int index = 0; index < this.prayTargetInfos.Count; ++index)
    {
      if (this.prayTargetInfos[index].targetId == prayInfo.targetId)
      {
        this.prayTargetInfos.RemoveAt(index);
        break;
      }
    }
    if (this.prayTargetInfos.Count <= 0)
      this.EndWaitingPacket(StageObject.WAITING_PACKET.PLAYER_PRAYER_END);
    Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(prayInfo.targetId) as Player;
    if (Object.op_Equality((Object) player, (Object) null))
      return;
    Player.BoostPrayInfo boostPrayInfo = this.boostPrayTargetInfoList.Find((Predicate<Player.BoostPrayInfo>) (item => item.prayTargetId == prayInfo.targetId));
    if (boostPrayInfo != null)
      this.boostPrayTargetInfoList.Remove(boostPrayInfo);
    player.EndPrayed(this.id);
  }

  public void CheckPrayerBoost(Player p)
  {
    Player.BoostPrayInfo boostPrayInfo = new Player.BoostPrayInfo();
    boostPrayInfo.prayerId = this.id;
    boostPrayInfo.prayTargetId = p.id;
    boostPrayInfo.isBoostByTypes[0] = !this.isNpc && this._IsGuard(SP_ATTACK_TYPE.NONE);
    boostPrayInfo.isBoostByTypes[1] = this.IsInBarrier() && p.IsInBarrier();
    this.OnChangeBoostPray(p.id, boostPrayInfo);
  }

  public void OnChangeBoostPray(int prayedId, Player.BoostPrayInfo boostPrayInfo)
  {
    if (Object.op_Inequality((Object) this.playerSender, (Object) null))
      this.playerSender.OnChangePrayBoost(prayedId, boostPrayInfo);
    Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(prayedId) as Player;
    if (Object.op_Equality((Object) player, (Object) null))
      return;
    Player.BoostPrayInfo changedInfo = this.boostPrayTargetInfoList.Find((Predicate<Player.BoostPrayInfo>) (item => item.prayTargetId == boostPrayInfo.prayTargetId));
    if (changedInfo != null)
    {
      int index = 0;
      for (int length = boostPrayInfo.isBoostByTypes.Length; index < length; ++index)
        changedInfo.isBoostByTypes[index] = boostPrayInfo.isBoostByTypes[index];
      player.ChangeBoostPrayed(this.id, changedInfo);
      if (!changedInfo.IsNoBoost())
        return;
      this.boostPrayTargetInfoList.Remove(changedInfo);
      player.EndBoostPrayed(this.id);
    }
    else
    {
      if (boostPrayInfo.IsNoBoost())
        return;
      this.boostPrayTargetInfoList.Add(boostPrayInfo);
      player.StartBoostPrayed(this.id, boostPrayInfo);
    }
  }

  public void StartBoostPrayed(int prayerId, Player.BoostPrayInfo info)
  {
    if (info == null || info.IsNoBoost() || info.prayerId != prayerId)
      return;
    this.boostPrayedInfoList.Add(info);
  }

  public void EndBoostPrayed(int prayerId)
  {
    Player.BoostPrayInfo boostPrayInfo = this.boostPrayedInfoList.Find((Predicate<Player.BoostPrayInfo>) (item => item.prayerId == prayerId));
    if (boostPrayInfo == null)
      return;
    this.boostPrayedInfoList.Remove(boostPrayInfo);
  }

  public void ChangeBoostPrayed(int prayerId, Player.BoostPrayInfo changedInfo)
  {
    Player.BoostPrayInfo boostPrayInfo = this.boostPrayedInfoList.Find((Predicate<Player.BoostPrayInfo>) (item => item.prayerId == prayerId));
    if (boostPrayInfo == null)
      this.boostPrayedInfoList.Add(changedInfo);
    else
      boostPrayInfo.Copy(changedInfo);
  }

  public void StartPrayed(int prayerId, Player.BoostPrayInfo info)
  {
    if (!this.prayerIds.Contains(prayerId))
      this.prayerIds.Add(prayerId);
    this.StartBoostPrayed(prayerId, info);
    if (!this.IsCoopNone() && !this.IsOriginal())
      return;
    if (this.isDead)
    {
      this.DeadCount(this.rescueTime, this.IsPrayed() || this.isStopCounter || this.isWaitingResurrectionHoming);
    }
    else
    {
      if (!this.IsStone())
        return;
      this.StoneCount(this.stoneRescueTime, this.IsPrayed() || this.isStopCounter);
    }
  }

  public void EndPrayed(int prayerId)
  {
    this.prayerIds.Remove(prayerId);
    this.EndBoostPrayed(prayerId);
    if (!this.IsCoopNone() && !this.IsOriginal())
      return;
    if (this.isDead)
    {
      this.DeadCount(this.rescueTime, this.IsPrayed() || this.isStopCounter || this.isWaitingResurrectionHoming);
    }
    else
    {
      if (!this.IsStone())
        return;
      this.StoneCount(this.stoneRescueTime, this.IsPrayed() || this.isStopCounter);
    }
  }

  public int GetInitRescueCount() => 0;

  public bool EnableRescueCountup()
  {
    bool flag = false;
    if (MonoBehaviourSingleton<FieldManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.I.currentIsValidBoss)
      flag = true;
    if (MonoBehaviourSingleton<QuestManager>.IsValid() && QuestManager.IsValidInGameExplore())
      flag = true;
    return flag;
  }

  public void ActChangeUniqueEquipment(
    StageObjectManager.CreatePlayerInfo createPlayerInfo)
  {
    this.EndAction();
    this.actionID = (Character.ACTION_ID) 27;
    this.PlayMotion(128 /*0x80*/);
    this.changePlayerInfo = createPlayerInfo;
  }

  public void ActChangeWeapon(CharaInfo.EquipItem item, int weapon_index)
  {
    this.EndAction();
    this.actionID = (Character.ACTION_ID) 27;
    this.PlayMotion(128 /*0x80*/);
    this.changeWeaponItem = item;
    this.changeWeaponIndex = weapon_index;
    this.changePlayerInfo = (StageObjectManager.CreatePlayerInfo) null;
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnChangeWeapon();
  }

  public virtual void ApplyChangeWeapon(
    CharaInfo.EquipItem item,
    int weapon_index,
    StageObjectManager.CreatePlayerInfo createPlayerInfo = null)
  {
    if (!this.isDead && this.actionID != (Character.ACTION_ID) 27)
      this.ActChangeWeapon(item, weapon_index);
    this.EndWaitingPacket(StageObject.WAITING_PACKET.PLAYER_APPLY_CHANGE_WEAPON);
    this.isChangingWeapon = true;
    this.changeWeaponStartTime = Time.time;
    this.hitOffFlag |= StageObject.HIT_OFF_FLAG.INVICIBLE;
    if (this.isDead)
      this.isInitDead = false;
    string weaponEffectName = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.changeWeaponEffectName;
    if (!string.IsNullOrEmpty(weaponEffectName))
    {
      Transform effect = EffectManager.GetEffect(weaponEffectName, this._transform);
      if (Object.op_Inequality((Object) effect, (Object) null))
        this.AddObjectList(((Component) effect).gameObject, Character.OBJECT_LIST_TYPE.CHANGE_WEAPON);
    }
    for (int index = 0; index < this.m_weaponCtrlList.Count; ++index)
      this.m_weaponCtrlList[index].OnChangeWeapon();
    this.OnBuffEnd(BuffParam.BUFFTYPE.LUNATIC_TEAR, true, true);
    this.FinishBoostMode();
    this.EndEvolve();
    this._EndGuard();
    this._EndBuffShadowSealing();
    this.EndCarry();
    this.DeleteWeaponLinkEffectAll();
    this.DetachRootEffectTemporary();
    PlayerLoader.OnCompleteLoad callback = (PlayerLoader.OnCompleteLoad) (o =>
    {
      this.SetEnableNodeRenderer("BODY", false);
      if (Object.op_Inequality((Object) this.loader.shadow, (Object) null))
        ((Component) this.loader.shadow).gameObject.SetActive(false);
      this.pairSwordsCtrl.OnLoadComplete();
      this.ReAttachRootEffect();
      this.changePlayerInfo = (StageObjectManager.CreatePlayerInfo) null;
    });
    if (createPlayerInfo == null)
      this.LoadWeapon(item, weapon_index, callback);
    else
      this.LoadUniqueEquipment(createPlayerInfo, callback);
    if (Object.op_Inequality((Object) this.playerSender, (Object) null))
      this.playerSender.OnApplyChangeWeapon(item, weapon_index);
    if (item == null)
      return;
    this.OnCheckAndResizeColliderOsMapByWeapon(item.eId);
  }

  public void ActGather(GatherPointObject gather_point)
  {
    if (Object.op_Equality((Object) gather_point, (Object) null))
      return;
    this.EndAction();
    this.targetGatherPoint = gather_point;
    this.isGatherInterruption = false;
    this.actionID = (Character.ACTION_ID) 28;
    this.PlayMotion(this.targetGatherPoint.viewData.actStateName);
    if (!string.IsNullOrEmpty(this.targetGatherPoint.viewData.toolModelName))
    {
      LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.gatherPointToolTable.Get(this.targetGatherPoint.viewData.toolModelName);
      if (loadObject != null)
      {
        this.actionRendererModel = loadObject.loadedObject as GameObject;
        this.actionRendererNodeName = this.targetGatherPoint.viewData.toolNodeName;
      }
    }
    Vector3 position = this.targetGatherPoint._transform.position;
    position.y = 0.0f;
    if (this.IsCoopNone() || this.IsOriginal())
      this.SetActionPosition(position, true);
    Vector3 velocity = Vector3.op_Subtraction(position, this._transform.position);
    velocity.y = 0.0f;
    this.SetLerpRotation(velocity);
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnActGather(gather_point);
  }

  public virtual bool ApplyGather()
  {
    if (this.isAppliedGather || Object.op_Equality((Object) this.targetGatherPoint, (Object) null) || this.isGatherInterruption)
      return false;
    this.isAppliedGather = true;
    return true;
  }

  public void ActSonar(int id)
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    IFieldGimmickObject fieldGimmickObj = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.Sonar, id);
    if (fieldGimmickObj == null)
      return;
    FieldSonarObject fieldSonarObject = fieldGimmickObj as FieldSonarObject;
    if (Object.op_Equality((Object) fieldSonarObject, (Object) null))
      return;
    fieldSonarObject.StartSonar();
  }

  public void ActReadStory(int id)
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    IFieldGimmickObject fieldGimmickObj = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.ReadStory, id);
    if (fieldGimmickObj == null)
      return;
    FieldReadStoryObject fieldReadStoryObject = fieldGimmickObj as FieldReadStoryObject;
    if (Object.op_Equality((Object) fieldReadStoryObject, (Object) null))
      return;
    fieldReadStoryObject.StartReadStory();
  }

  public void ActPortalGimmick(int id)
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    IFieldGimmickObject fieldGimmickObj = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.PortalGimmick, id);
    if (fieldGimmickObj == null)
      return;
    FieldPortalGimmickObject portalGimmickObject = fieldGimmickObj as FieldPortalGimmickObject;
    if (Object.op_Equality((Object) portalGimmickObject, (Object) null))
      return;
    portalGimmickObject.StartReadStory();
  }

  public void ActQuestGimmick(int id)
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid() || MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop)
      return;
    IFieldGimmickObject fieldGimmickObj = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.QuestGimmick, id);
    if (fieldGimmickObj == null)
      return;
    FieldQuestGimmickObject questGimmickObject = fieldGimmickObj as FieldQuestGimmickObject;
    if (Object.op_Equality((Object) questGimmickObject, (Object) null))
      return;
    this.EndAction();
    this.PlayMotion(questGimmickObject.viewData.actStateName);
    if (!string.IsNullOrEmpty(questGimmickObject.viewData.toolModelName))
    {
      LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.gatherPointToolTable.Get(questGimmickObject.viewData.toolModelName);
      if (loadObject != null)
      {
        this.actionRendererModel = loadObject.loadedObject as GameObject;
        this.actionRendererNodeName = questGimmickObject.viewData.toolNodeName;
      }
    }
    Vector3 position = questGimmickObject.GetTransform().position;
    position.y = 0.0f;
    if (this.IsCoopNone() || this.IsOriginal())
      this.SetActionPosition(position, true);
    Vector3 velocity = Vector3.op_Subtraction(position, this._transform.position);
    velocity.y = 0.0f;
    this.SetLerpRotation(velocity);
    if (this.playerSender != null)
      this.playerSender.OnActQuestGimmick(id);
    questGimmickObject.StartAction(this);
    this.questGimmickObject = questGimmickObject;
  }

  protected void OnQuestGimmickEnd()
  {
    if (Object.op_Equality((Object) this.questGimmickObject, (Object) null))
      return;
    if (this is Self)
      this.questGimmickObject.OnEndAction();
    this.questGimmickObject = (FieldQuestGimmickObject) null;
  }

  public void ActGatherGimmick(int id)
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    IFieldGimmickObject fieldGimmickObj = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.GatherGimmick, id);
    if (fieldGimmickObj == null)
      return;
    FieldGatherGimmickObject gatherGimmickObject = fieldGimmickObj as FieldGatherGimmickObject;
    if (Object.op_Equality((Object) gatherGimmickObject, (Object) null) || gatherGimmickObject.GetGatherGimmickType() != GATHER_GIMMICK_TYPE.FISHING || !this.fishingCtrl.CanFishing())
      return;
    this.EndAction();
    float add_margin_time = 10f;
    if (gatherGimmickObject.GetGatherGimmickType() == GATHER_GIMMICK_TYPE.FISHING)
    {
      FieldFishingGimmickObject fishingGimmickObject = gatherGimmickObject as FieldFishingGimmickObject;
      this.actionID = (Character.ACTION_ID) 40;
      this.PlayMotion(138);
      this.fishingCtrl.Start(gatherGimmickObject.lotId, gatherGimmickObject.GetTransform(), fishingGimmickObject.modelIndex);
      LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.gatherPointToolTable.Get("Fishingrod");
      if (loadObject != null)
      {
        this.actionRendererModel = loadObject.loadedObject as GameObject;
        this.actionRendererNodeName = "R_Wep";
      }
      add_margin_time = this.fishingCtrl.GetMaxWaitPacketSec();
    }
    Vector3 position = gatherGimmickObject.GetTransform().position;
    position.y = 0.0f;
    if (this.IsCoopNone() || this.IsOriginal())
      this.SetActionPosition(position, true);
    Vector3 velocity = Vector3.op_Subtraction(position, this._transform.position);
    velocity.y = 0.0f;
    this.SetLerpRotation(velocity);
    gatherGimmickObject.StartAction(this, this.IsCoopNone() || this.IsOriginal());
    this.gatherGimmickObject = gatherGimmickObject;
    this.StartWaitingPacket(StageObject.WAITING_PACKET.PLAYER_GATHER_GIMMICK, true, add_margin_time);
    if (this.playerSender == null)
      return;
    this.playerSender.OnActGatherGimmick(id);
  }

  public void OnGatherGimmickState(int state)
  {
    if (Object.op_Equality((Object) this.gatherGimmickObject, (Object) null))
      return;
    switch (this.gatherGimmickObject.GetGatherGimmickType())
    {
      case GATHER_GIMMICK_TYPE.FISHING:
      case GATHER_GIMMICK_TYPE.COOP_FISHING:
        this.fishingCtrl.ChangeState((FishingController.eState) state);
        break;
    }
  }

  protected void OnGatherGimmickGet()
  {
    if (Object.op_Equality((Object) this.gatherGimmickObject, (Object) null) || this.gatherGimmickObject.GetGatherGimmickType() != GATHER_GIMMICK_TYPE.FISHING)
      return;
    this.fishingCtrl.Get();
  }

  protected void OnGatherGimmickEnd()
  {
    if (Object.op_Equality((Object) this.gatherGimmickObject, (Object) null))
      return;
    if (this.gatherGimmickObject.GetGatherGimmickType() == GATHER_GIMMICK_TYPE.FISHING)
      this.fishingCtrl.End();
    this.gatherGimmickObject.OnUseEnd(this, this.IsCoopNone() || this.IsOriginal());
    this.gatherGimmickObject = (FieldGatherGimmickObject) null;
  }

  public void ActBingo(int id)
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    IFieldGimmickObject fieldGimmickObj = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.Bingo, id);
    if (fieldGimmickObj == null)
      return;
    FieldBingoObject fieldBingoObject = fieldGimmickObj as FieldBingoObject;
    if (Object.op_Equality((Object) fieldBingoObject, (Object) null))
      return;
    fieldBingoObject.OpenBingo();
  }

  public void ActCannonStandby(int id)
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid() || !(MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.Cannon, id) is IFieldGimmickCannon fieldGimmickObj) || fieldGimmickObj.IsUsing() || !fieldGimmickObj.IsAbleToUse())
      return;
    this.actionID = (Character.ACTION_ID) 31 /*0x1F*/;
    this.targetFieldGimmickCannon = fieldGimmickObj;
    this._position = fieldGimmickObj.GetPosition();
    this._rigidbody.isKinematic = true;
    fieldGimmickObj.OnBoard(this);
    this.PlayMotion(131);
    this.SetCannonState(Player.CANNON_STATE.STANDBY);
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnCannonStandby(id);
  }

  public void ActCannonShot()
  {
    if (this.cannonState != Player.CANNON_STATE.READY || this.targetFieldGimmickCannon == null || this.targetFieldGimmickCannon.IsCooling())
      return;
    if (this.IsPlayingMotion(132))
      this.SetNextTrigger();
    this.targetFieldGimmickCannon.Shot();
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnCannonShot();
  }

  public virtual void CancelCannonMode()
  {
    if (this.targetFieldGimmickCannon != null && this.targetFieldGimmickCannon.IsUsing())
      this.targetFieldGimmickCannon.OnLeave();
    this.SetCannonState(Player.CANNON_STATE.NONE);
    this.targetFieldGimmickCannon = (IFieldGimmickCannon) null;
    this._rigidbody.isKinematic = false;
  }

  public void ActCoopFishingStart(int id)
  {
    if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
      return;
    IFieldGimmickObject fieldGimmickObj = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.CoopFishing, id);
    if (fieldGimmickObj == null)
      return;
    FieldGimmickCoopFishing gimmickCoopFishing = fieldGimmickObj as FieldGimmickCoopFishing;
    if (Object.op_Equality((Object) gimmickCoopFishing, (Object) null))
      return;
    this.EndAction();
    this._position = gimmickCoopFishing.GetCoopPos();
    this._rotation = gimmickCoopFishing.GetCoopRot();
    gimmickCoopFishing.SetPosition(this._position);
    this.actionID = (Character.ACTION_ID) 41;
    this.PlayMotion(139);
    this.fishingCtrl.CoopStart();
    this.fishingCtrl.SetCoopOwnerUserId(gimmickCoopFishing.GetOwnerUserId());
    this.fishingCtrl.SetCoopOwnerPlayerId(gimmickCoopFishing.GetOwnerPlayerId());
    this.fishingCtrl.SetCoopOwnerClientId(gimmickCoopFishing.GetOwnerClientId());
    Vector3 position = gimmickCoopFishing.GetTransform().position;
    position.y = 0.0f;
    if (this.IsOriginal())
      this.SetActionPosition(position, true);
    Vector3 velocity = Vector3.op_Subtraction(position, this._transform.position);
    velocity.y = 0.0f;
    this.SetLerpRotation(velocity);
    this.gatherGimmickObject = (FieldGatherGimmickObject) gimmickCoopFishing;
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnActCoopFishingStart(id);
  }

  public virtual bool ActSkillAction(int skill_index, bool isGuestUsingSecondGrade = false)
  {
    SkillInfo.SkillParam skillParam = this.GetSkillParam(skill_index);
    if (skillParam == null || !skillParam.isValid)
      return false;
    if (!this.IsOriginal() && isGuestUsingSecondGrade)
    {
      skillParam.useGaugeCounter = (float) (int) skillParam.GetMaxGaugeValue();
      skillParam.isUsingSecondGrade = true;
    }
    if (skillParam.tableData.isTeleportation)
    {
      this.skillInfo.skillIndex = skill_index;
      if (this.IsCoopNone() || this.IsOriginal())
      {
        this.SetSelfBuffByBuffTable(skillParam);
        this.MakeInvincible(this.GetInvincibleTimeOnSkillTeleportation(skillParam));
      }
      this.ActSkillTeleportation();
    }
    else
    {
      this.EndAction();
      int index = 0;
      for (int count = this.m_weaponCtrlList.Count; index < count; ++index)
        this.m_weaponCtrlList[index].OnActSkillAction();
      this.skillInfo.skillIndex = skill_index;
      this.actionID = (Character.ACTION_ID) 22;
      if (!string.IsNullOrEmpty(skillParam.tableData.castStateName))
      {
        this.PlayMotion(skillParam.tableData.castStateName);
        this.isSkillCastState = true;
      }
      else
        this.PlayMotion(skillParam.tableData.actStateName);
    }
    this.skillInfo.OnActSkillAction();
    if (!skillParam.tableData.isTeleportation)
      this.isActSkillAction = true;
    this.isUsingSecondGradeSkill = skillParam.isUsingSecondGrade;
    if (this.IsCoopNone() || this.IsOriginal())
      this.SetAttackActionPosition();
    if (skillParam.tableData.type == SKILL_SLOT_TYPE.ATTACK)
      this.attackStartTarget = this.actionTarget;
    if ((double) (float) skillParam.tableData.skillRange > 0.0 && Object.op_Equality((Object) this.skillRangeEffect, (Object) null))
    {
      Transform effect = EffectManager.GetEffect(this.playerParameter.skillRangeEffectName, this._transform);
      if (Object.op_Inequality((Object) effect, (Object) null))
      {
        Vector3 localScale = effect.localScale;
        effect.localScale = Vector3.op_Multiply(localScale, this.playerParameter.revivalRange / 0.5f);
        this.skillRangeEffect = effect;
      }
    }
    if (Object.op_Inequality((Object) this.playerSender, (Object) null))
      this.playerSender.OnActSkillAction(skill_index, skillParam);
    if (MonoBehaviourSingleton<UIPlayerAnnounce>.IsValid())
      MonoBehaviourSingleton<UIPlayerAnnounce>.I.StartSkill(skillParam.tableData.name, this);
    if (Object.op_Inequality((Object) this.bossBrain, (Object) null))
      this.bossBrain.HandleEvent(BRAIN_EVENT.PLAYER_SKILL, (object) this);
    MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.AddSkillCount((int) skillParam.tableData.id);
    return true;
  }

  private void ActSkillTeleportation()
  {
    if (this.IsPuppet())
    {
      EffectManager.OneShot("ef_btl_sk_warp_02_02", this._position, this._rotation);
    }
    else
    {
      InGameSettingsManager.Player.TeleportationInfo teleportationInfo = this.playerParameter.teleportationInfo;
      Vector3 pos = Vector3.zero;
      bool flag = false;
      EffectManager.OneShot("ef_btl_sk_warp_02_01", this.rootNode.position, this._rotation);
      if (this.snatchCtrl.GetSnatchPos(out pos))
        flag = true;
      else if (this.GetTargetPos(out pos))
        flag = true;
      else if (StageObjectManager.CanTargetBoss)
      {
        Vector3 position = MonoBehaviourSingleton<StageObjectManager>.I.boss._position;
        pos = position;
        RaycastHit hit = new RaycastHit();
        if (AIUtility.RaycastOpponent((StageObject) this, position, out hit))
          pos = ((RaycastHit) ref hit).point;
        flag = true;
      }
      if (flag && !MonoBehaviourSingleton<StageManager>.I.CheckPosInside(pos))
      {
        RaycastHit hit = new RaycastHit();
        if (AIUtility.RaycastWallAndBlock((StageObject) this, pos, out hit))
        {
          if (MonoBehaviourSingleton<StageManager>.I.CheckPosInside(((RaycastHit) ref hit).point))
            pos = ((RaycastHit) ref hit).point;
          else
            flag = false;
        }
        else
          flag = false;
      }
      if (flag && !this.IsValidBuffBlind())
      {
        pos.y = this._position.y;
        pos = Vector3.op_Addition(pos, Vector3.op_Multiply(Vector3.Normalize(Vector3.op_Subtraction(this._position, pos)), this.GetOffsetOnSkillTeleportation()));
        this._rotation = Quaternion.LookRotation(Vector3.op_Subtraction(pos, this._position), Vector3.up);
        this._position = pos;
      }
      else
      {
        pos = Vector3.op_Addition(this._position, Vector3.op_Multiply(this._forward, teleportationInfo.forwardScalar));
        if (MonoBehaviourSingleton<StageManager>.I.CheckPosInside(pos))
          this._position = pos;
      }
      EffectManager.OneShot("ef_btl_sk_warp_02_02", Vector3.op_Addition(this._position, this.rootNode.localPosition), this._rotation);
    }
  }

  private float GetOffsetOnSkillTeleportation()
  {
    if (this.playerParameter.teleportationInfo == null)
      return 1f;
    InGameSettingsManager.Player.TeleportationInfo teleportationInfo = this.playerParameter.teleportationInfo;
    float offsetScalar = teleportationInfo.offsetScalar;
    if (((IList<InGameSettingsManager.Player.TeleportationInfo.OffsetByWeaponAndAttack>) teleportationInfo.offsetByWeaponAndAttack).IsNullOrEmpty<InGameSettingsManager.Player.TeleportationInfo.OffsetByWeaponAndAttack>())
      return offsetScalar;
    int index = 0;
    for (int length = teleportationInfo.offsetByWeaponAndAttack.Length; index < length; ++index)
    {
      if ((Player.ATTACK_MODE) teleportationInfo.offsetByWeaponAndAttack[index].attackMode == this.attackMode && teleportationInfo.offsetByWeaponAndAttack[index].attackID == this.attackID)
        offsetScalar = teleportationInfo.offsetByWeaponAndAttack[index].value;
    }
    return offsetScalar;
  }

  private float GetInvincibleTimeOnSkillTeleportation(SkillInfo.SkillParam skillParam)
  {
    if (skillParam == null || skillParam.tableData == null)
      return 0.0f;
    float num = skillParam.tableData.supportTime[0];
    GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(skillParam.tableData.growID, skillParam.baseInfo.level, skillParam.baseInfo.exceedCnt);
    return growSkillItemData == null ? num : (float) ((double) num * (double) (int) growSkillItemData.supprtTime[0].rate * 0.0099999997764825821) + (float) growSkillItemData.supprtTime[0].add;
  }

  public virtual void CheckSkillCastLoop()
  {
    if (!this.isSkillCastLoop || (double) Time.time - (double) this.skillCastLoopStartTime < (double) this.skillCastLoopTime)
      return;
    this.SetChangeTrigger(this.skillCastLoopTrigger);
    this.isSkillCastLoop = false;
    this.skillCastLoopStartTime = -1f;
    this.skillCastLoopTime = 0.0f;
    this.skillCastLoopTrigger = (string) null;
  }

  private void ApplySkillParam()
  {
    if (this.isAppliedSkillParam)
      return;
    if (Object.op_Inequality((Object) this.skillRangeEffect, (Object) null))
    {
      EffectManager.ReleaseEffect(((Component) this.skillRangeEffect).gameObject);
      this.skillRangeEffect = (Transform) null;
    }
    this.isAppliedSkillParam = true;
    if (this.IsPuppet())
      return;
    SkillInfo.SkillParam actSkillParam = this.skillInfo.actSkillParam;
    if (actSkillParam == null)
      return;
    float num = (float) actSkillParam.tableData.skillRange * (float) actSkillParam.tableData.skillRange;
    Vector3 vector3;
    if (actSkillParam.healHp > 0)
    {
      Character.HealData healData = new Character.HealData(actSkillParam.healHp, actSkillParam.tableData.healType, HEAL_EFFECT_TYPE.BASIS, new List<int>()
      {
        10
      });
      if (actSkillParam.tableData.selfOnly)
        this.OnHealReceive(healData);
      else if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
      {
        if ((double) num > 0.0 && MonoBehaviourSingleton<StageObjectManager>.I.ExistsEnemyValiedHealAttack())
          new GameObject("HealAttackObject").AddComponent<HealAttackObject>().Initialize((StageObject) this, this._transform, actSkillParam, Vector3.zero, Vector3.zero, 0.0f, 12);
        List<StageObject>.Enumerator enumerator = MonoBehaviourSingleton<StageObjectManager>.I.playerList.GetEnumerator();
        while (enumerator.MoveNext())
        {
          Player current = enumerator.Current as Player;
          if ((double) num > 0.0)
          {
            vector3 = Vector3.op_Subtraction(current._transform.position, this._transform.position);
            if ((double) ((Vector3) ref vector3).sqrMagnitude > (double) num)
              continue;
          }
          current.OnHealReceive(healData);
        }
      }
    }
    if (actSkillParam.tableData.healType == HEAL_TYPE.RESURRECTION_ALL && MonoBehaviourSingleton<StageObjectManager>.IsValid())
    {
      List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
      for (int index = 0; index < playerList.Count; ++index)
      {
        Player player = playerList[index] as Player;
        if (!Object.op_Equality((Object) player, (Object) null))
          player.OnResurrectionReceive();
      }
    }
    for (int index = 0; index < 3; ++index)
    {
      if (actSkillParam.tableData.supportType[index] != BuffParam.BUFFTYPE.NONE && actSkillParam.tableData.supportType[index] < BuffParam.BUFFTYPE.MAX)
      {
        if (actSkillParam.tableData.selfOnly)
          this.SetSelfBuff(actSkillParam.tableData.id, actSkillParam.tableData.supportType[index], actSkillParam.supportValue[index], actSkillParam.supportTime[index], actSkillParam.skillIndex);
        else if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
        {
          List<StageObject>.Enumerator enumerator = MonoBehaviourSingleton<StageObjectManager>.I.playerList.GetEnumerator();
          while (enumerator.MoveNext())
          {
            Player current = enumerator.Current as Player;
            if ((double) num > 0.0)
            {
              vector3 = Vector3.op_Subtraction(current._transform.position, this._transform.position);
              if ((double) ((Vector3) ref vector3).sqrMagnitude > (double) num)
                continue;
            }
            BuffParam.BuffData data = new BuffParam.BuffData();
            data.type = actSkillParam.tableData.supportType[index];
            data.time = actSkillParam.supportTime[index];
            data.value = actSkillParam.supportValue[index];
            this.SetFromInfo(ref data);
            data.skillId = actSkillParam.tableData.id;
            if (data.isSkillChargeType())
              current.OnChargeSkillGaugeReceive(data.type, data.value, Object.op_Equality((Object) current, (Object) this) ? actSkillParam.skillIndex : -1);
            else
              current.OnBuffReceive(data);
          }
        }
      }
    }
    if (((IList<int>) actSkillParam.tableData.buffTableIds).IsNullOrEmpty<int>())
      return;
    List<BuffParam.BuffData> dataListByBuffTable = this.GetBuffDataListByBuffTable(actSkillParam);
    if (dataListByBuffTable.IsNullOrEmpty<BuffParam.BuffData>())
      return;
    int index1 = 0;
    for (int count = dataListByBuffTable.Count; index1 < count; ++index1)
    {
      if (actSkillParam.tableData.selfOnly)
        this.OnBuffReceive(dataListByBuffTable[index1]);
      else if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
      {
        List<StageObject>.Enumerator enumerator = MonoBehaviourSingleton<StageObjectManager>.I.playerList.GetEnumerator();
        while (enumerator.MoveNext())
        {
          Player current = enumerator.Current as Player;
          if ((double) num > 0.0)
          {
            vector3 = Vector3.op_Subtraction(current._transform.position, this._transform.position);
            if ((double) ((Vector3) ref vector3).sqrMagnitude > (double) num)
              continue;
          }
          current.OnBuffReceive(dataListByBuffTable[index1]);
        }
      }
    }
  }

  private List<BuffParam.BuffData> GetBuffDataListByBuffTable(SkillInfo.SkillParam skillParam)
  {
    if (skillParam == null)
      return (List<BuffParam.BuffData>) null;
    if (skillParam.tableData == null)
      return (List<BuffParam.BuffData>) null;
    if (((IList<int>) skillParam.tableData.buffTableIds).IsNullOrEmpty<int>())
      return (List<BuffParam.BuffData>) null;
    List<BuffParam.BuffData> dataListByBuffTable = new List<BuffParam.BuffData>();
    foreach (uint buffTableId in skillParam.tableData.buffTableIds)
    {
      BuffTable.BuffData data1 = Singleton<BuffTable>.I.GetData(buffTableId);
      if (data1 != null && data1.type > BuffParam.BUFFTYPE.NONE && data1.type < BuffParam.BUFFTYPE.MAX)
      {
        BuffParam.BuffData data2 = new BuffParam.BuffData();
        data2.type = data1.type;
        data2.interval = data1.interval;
        data2.valueType = data1.valueType;
        data2.time = data1.duration;
        data2.skillId = skillParam.tableData.id;
        this.SetFromInfo(ref data2);
        float num = (float) data1.value;
        GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(data1.growID, skillParam.baseInfo.level, skillParam.baseInfo.exceedCnt);
        if (growSkillItemData != null)
        {
          data2.time = (float) ((double) data1.duration * (double) (int) growSkillItemData.supprtTime[0].rate * 0.0099999997764825821) + (float) growSkillItemData.supprtTime[0].add;
          num = (float) (data1.value * (int) growSkillItemData.supprtValue[0].rate) * 0.01f + (float) (int) growSkillItemData.supprtValue[0].add;
        }
        if (data2.valueType == BuffParam.VALUE_TYPE.RATE && BuffParam.IsTypeValueBasedOnHP(data2.type))
          num = (float) ((double) this.hpMax * (double) num * 0.0099999997764825821);
        data2.value = Mathf.FloorToInt(num);
        dataListByBuffTable.Add(data2);
      }
    }
    return dataListByBuffTable;
  }

  public void SetSelfBuff(uint id, BuffParam.BUFFTYPE type, int value, float time, int index)
  {
    BuffParam.BuffData data = new BuffParam.BuffData();
    data.skillId = id;
    data.type = type;
    data.value = value;
    data.time = time;
    this.SetFromInfo(ref data);
    if (data.isSkillChargeType())
      this.OnChargeSkillGaugeReceive(data.type, data.value, index);
    else if (data.isSkillChargeMoveType())
      this.OnGetChargeSkillMove(data.type, (float) data.value, index);
    else
      this.OnBuffReceive(data);
  }

  public void SetSelfBuffByBuffTable(SkillInfo.SkillParam skillParam)
  {
    List<BuffParam.BuffData> dataListByBuffTable = this.GetBuffDataListByBuffTable(skillParam);
    if (dataListByBuffTable.IsNullOrEmpty<BuffParam.BuffData>())
      return;
    int index = 0;
    for (int count = dataListByBuffTable.Count; index < count; ++index)
      this.OnBuffReceive(dataListByBuffTable[index]);
  }

  public void OnHealReceive(Character.HealData healData)
  {
    if (this.IsCoopNone() || this.IsOriginal())
    {
      this.ExecHealHp(healData);
    }
    else
    {
      if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
        return;
      this.playerSender.OnHealReceive(healData);
    }
  }

  public virtual int ExecHealHp(Character.HealData healData, bool isPacket = false)
  {
    if (this.isDead)
      return 0;
    this.DoHealType(healData.healType);
    if (this.buffParam.IsValidBuff(BuffParam.BUFFTYPE.CANT_HEAL_HP))
      return 0;
    if (healData.healHp > 0)
      this.OnBuffEnd(BuffParam.BUFFTYPE.BLEEDING, false, true);
    int num1 = Mathf.Clamp(this.ApplyAbilityForHealHp(healData.healHp, healData.applyAbilityTypeList), 1, int.MaxValue);
    int num2 = Mathf.Clamp(this.hp + num1, 0, this.hpMax);
    this.hp = num2;
    this.healHp = Mathf.Max(this.healHp, num2);
    this.DoHealType(healData.healType);
    this.ExecHealEffect(healData.effectType);
    if (Object.op_Inequality((Object) this.playerSender, (Object) null) && !isPacket)
      this.playerSender.OnGetHeal(healData);
    if (Object.op_Inequality((Object) this.bossBrain, (Object) null))
      this.bossBrain.HandleEvent(BRAIN_EVENT.PLAYER_HEAL, (object) new Player.HateInfo()
      {
        target = (StageObject) this,
        val = (int) ((double) healData.healHp / (double) this.hpMax * 1000.0)
      });
    return num1;
  }

  private int ApplyAbilityForHealHp(int baseHealHp, List<int> applyAbilityTypeList)
  {
    if (applyAbilityTypeList.IsNullOrEmpty<int>())
      return baseHealHp;
    float num1 = 1f;
    for (int index = 0; index < applyAbilityTypeList.Count; ++index)
    {
      switch (applyAbilityTypeList[index])
      {
        case 10:
          num1 += this.buffParam.GetHealHpRate();
          break;
        case 80 /*0x50*/:
          num1 += this.buffParam.GetHealUpDependsWeaponRate();
          break;
      }
    }
    float num2 = Mathf.Clamp(num1, 0.0f, float.MaxValue);
    return Mathf.FloorToInt((float) baseHealHp * num2);
  }

  private void ExecHealEffect(HEAL_EFFECT_TYPE healEffectType)
  {
    if (Object.op_Inequality((Object) this.healEffectTransform, (Object) null))
      return;
    switch (healEffectType)
    {
      case HEAL_EFFECT_TYPE.BASIS:
        this.healEffectTransform = EffectManager.GetEffect("ef_btl_sk_heal_01", this._transform);
        break;
      case HEAL_EFFECT_TYPE.ABSORB:
        this.healEffectTransform = EffectManager.GetEffect("ef_btl_sk_heal_02", this.FindNode("Hip"));
        break;
      case HEAL_EFFECT_TYPE.HIT_ABSORB:
        this.healEffectTransform = EffectManager.GetEffect("ef_btl_sk_drain_01_01", this.FindNode("Root"));
        break;
      default:
        return;
    }
    if (Object.op_Equality((Object) this.healEffectTransform, (Object) null))
      return;
    this.AddObjectList(((Component) this.healEffectTransform).gameObject, Character.OBJECT_LIST_TYPE.STATIC);
  }

  public void DoHealType(HEAL_TYPE healType)
  {
    switch (healType)
    {
      case HEAL_TYPE.PARALYZE:
        if (this.actionID != Character.ACTION_ID.PARALYZE)
          break;
        this.SetNextTrigger();
        break;
      case HEAL_TYPE.POISON:
        this.OnBuffEnd(BuffParam.BUFFTYPE.POISON, false, true);
        this.OnBuffEnd(BuffParam.BUFFTYPE.DEADLY_POISON, false, true);
        break;
      case HEAL_TYPE.BURNING:
        this.OnBuffEnd(BuffParam.BUFFTYPE.BURNING, false, true);
        break;
      case HEAL_TYPE.SPEEDDOWN:
        this.OnBuffEnd(BuffParam.BUFFTYPE.MOVE_SPEED_DOWN, false, true);
        break;
      case HEAL_TYPE.ALL_BADSTATUS:
        if (this.actionID == Character.ACTION_ID.PARALYZE)
          this.SetNextTrigger();
        if (this.actionID == (Character.ACTION_ID) 43)
          this.ActStoneEnd(this.stoneRescueTime);
        this.OnBuffEnd(BuffParam.BUFFTYPE.POISON, false, true);
        this.OnBuffEnd(BuffParam.BUFFTYPE.BLEEDING, false, true);
        this.OnBuffEnd(BuffParam.BUFFTYPE.DEADLY_POISON, false, true);
        this.OnBuffEnd(BuffParam.BUFFTYPE.BURNING, false, true);
        this.OnBuffEnd(BuffParam.BUFFTYPE.MOVE_SPEED_DOWN, false, true);
        this.OnBuffEnd(BuffParam.BUFFTYPE.SLIDE, false, true);
        this.OnBuffEnd(BuffParam.BUFFTYPE.SILENCE, false, true);
        this.OnBuffEnd(BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN, false, true);
        this.OnBuffEnd(BuffParam.BUFFTYPE.CANT_HEAL_HP, false, true);
        this.OnBuffEnd(BuffParam.BUFFTYPE.BLIND, false, true);
        this.OnBuffEnd(BuffParam.BUFFTYPE.ACID, false, true);
        break;
      case HEAL_TYPE.SLIDE:
        this.OnBuffEnd(BuffParam.BUFFTYPE.SLIDE, false, true);
        break;
      case HEAL_TYPE.SILENCE:
        this.OnBuffEnd(BuffParam.BUFFTYPE.SILENCE, false, true);
        break;
      case HEAL_TYPE.ATTACK_SPEED_DOWN:
        this.OnBuffEnd(BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN, false, true);
        break;
      case HEAL_TYPE.CANT_HEAL_HP:
        this.OnBuffEnd(BuffParam.BUFFTYPE.CANT_HEAL_HP, false, true);
        break;
      case HEAL_TYPE.BLIND:
        this.OnBuffEnd(BuffParam.BUFFTYPE.BLIND, false, true);
        break;
    }
  }

  public void OnChargeSkillGaugeReceive(
    BuffParam.BUFFTYPE buffType,
    int buffValue,
    int useSkillIndex)
  {
    if (this.IsCoopNone() || this.IsOriginal())
    {
      this.OnGetChargeSkillGauge(buffType, buffValue, useSkillIndex);
    }
    else
    {
      if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
        return;
      this.playerSender.OnChargeSkillGaugeReceive(buffType, buffValue, useSkillIndex);
    }
  }

  public void OnGetChargeSkillGauge(
    BuffParam.BUFFTYPE buffType,
    int buffValue,
    int useSkillIndex,
    bool packet = false,
    bool isCorrectWaveMatch = true)
  {
    if (this.isDead)
      return;
    for (int index = 0; index < 3; ++index)
    {
      int num = index + this.skillInfo.weaponOffset;
      if (num != useSkillIndex)
      {
        SkillInfo.SkillParam skillParam = this.skillInfo.GetSkillParam(num);
        if (skillParam != null)
        {
          float add_gauge = (float) buffValue;
          switch (buffType)
          {
            case BuffParam.BUFFTYPE.SKILL_CHARGE_RATE:
              add_gauge = (float) (int) skillParam.useGauge * ((float) buffValue * 0.01f);
              break;
            case BuffParam.BUFFTYPE.SKILL_CHARGE_FIRE:
              if (skillParam.tableData.type != SKILL_SLOT_TYPE.ATTACK || !skillParam.tableData.HasElement(ELEMENT_TYPE.FIRE))
                continue;
              break;
            case BuffParam.BUFFTYPE.SKILL_CHARGE_WATER:
              if (skillParam.tableData.type != SKILL_SLOT_TYPE.ATTACK || !skillParam.tableData.HasElement(ELEMENT_TYPE.WATER))
                continue;
              break;
            case BuffParam.BUFFTYPE.SKILL_CHARGE_THUNDER:
              if (skillParam.tableData.type != SKILL_SLOT_TYPE.ATTACK || !skillParam.tableData.HasElement(ELEMENT_TYPE.THUNDER))
                continue;
              break;
            case BuffParam.BUFFTYPE.SKILL_CHARGE_SOIL:
              if (skillParam.tableData.type != SKILL_SLOT_TYPE.ATTACK || !skillParam.tableData.HasElement(ELEMENT_TYPE.SOIL))
                continue;
              break;
            case BuffParam.BUFFTYPE.SKILL_CHARGE_LIGHT:
              if (skillParam.tableData.type != SKILL_SLOT_TYPE.ATTACK || !skillParam.tableData.HasElement(ELEMENT_TYPE.LIGHT))
                continue;
              break;
            case BuffParam.BUFFTYPE.SKILL_CHARGE_DARK:
              if (skillParam.tableData.type != SKILL_SLOT_TYPE.ATTACK || !skillParam.tableData.HasElement(ELEMENT_TYPE.DARK))
                continue;
              break;
            case BuffParam.BUFFTYPE.AUTO_REVIVE_SKILL_CHARGE:
              if (skillParam.tableData.type == SKILL_SLOT_TYPE.ATTACK && num == this.skillInfo.weaponOffset)
              {
                add_gauge = (float) (skillParam.useGauge.value + skillParam.useGauge2.value);
                break;
              }
              continue;
          }
          this.skillInfo.AddUseGaugeByIndex(num, add_gauge, true, true, isCorrectWaveMatch);
        }
      }
    }
    if (Object.op_Equality((Object) this.skillChargeEffectTransform, (Object) null))
    {
      this.skillChargeEffectTransform = buffType == BuffParam.BUFFTYPE.SKILL_CHARGE_WHEN_DAMAGED || !isCorrectWaveMatch ? EffectManager.GetEffect("ef_btl_sk_recovery_magi_01", this._transform) : EffectManager.GetEffect("ef_btl_sk_charge_magi_01_02", this._transform);
      if (Object.op_Inequality((Object) this.skillChargeEffectTransform, (Object) null))
        this.AddObjectList(((Component) this.skillChargeEffectTransform).gameObject, Character.OBJECT_LIST_TYPE.STATIC);
    }
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null) || packet)
      return;
    this.playerSender.OnGetChargeSkillGauge(buffType, buffValue, useSkillIndex, isCorrectWaveMatch);
  }

  private void OnGetChargeSkillMove(BuffParam.BUFFTYPE type, float value, int useIndex)
  {
    int arrayIndex = -1;
    UISkillButton sameButtonIndex = MonoBehaviourSingleton<UISkillButtonGroup>.I.GetSameButtonIndex(useIndex, ref arrayIndex);
    if (Object.op_Equality((Object) sameButtonIndex, (Object) null))
      return;
    if (type == BuffParam.BUFFTYPE.SKILL_CHARGE_ABOVE)
      EffectManager.GetUIEffect("ef_btl_sk_magi_move_01_01", ((Component) sameButtonIndex).transform);
    UISkillButton targetBtn = MonoBehaviourSingleton<UISkillButtonGroup>.I.GetUISkillButton(type != BuffParam.BUFFTYPE.SKILL_CHARGE_ABOVE ? arrayIndex + 1 : arrayIndex - 1);
    if (Object.op_Equality((Object) targetBtn, (Object) null) || targetBtn.buttonIndex == -1)
      return;
    AppMain.Delay(0.5f, (System.Action) (() =>
    {
      if (this.isDead)
        return;
      this.skillInfo.AddUseGaugeByIndex(targetBtn.buttonIndex, value, true, true);
    }));
    if (!Object.op_Equality((Object) this.skillChargeEffectTransform, (Object) null))
      return;
    this.skillChargeEffectTransform = EffectManager.GetEffect("ef_btl_sk_recovery_magi_01", this._transform);
    if (!Object.op_Inequality((Object) this.skillChargeEffectTransform, (Object) null))
      return;
    this.AddObjectList(((Component) this.skillChargeEffectTransform).gameObject, Character.OBJECT_LIST_TYPE.STATIC);
  }

  public void OnResurrectionReceive()
  {
    if (this.IsCoopNone() || this.IsOriginal())
      this.OnResurrection();
    else
      this.playerSender.OnResurrectionReceive();
  }

  public void OnResurrection(bool isPacket = false)
  {
    if (!this.isDead || MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest() || !this.IsAbleToRescueByRemainRescueTime() || this.actionID == (Character.ACTION_ID) 24 && (double) this.rescueTime <= 0.0)
      return;
    if (isPacket && Object.op_Inequality((Object) this.playerSender, (Object) null))
      this.playerSender.OnGetResurrection();
    this.DeactivateRescueTimer();
    this.StartCoroutine(this.OnPlayAndDoResurrection((System.Action) (() => this.ActDeadStandup(this.hpMax, Player.eContinueType.RESCUE))));
  }

  public void OnGetResurrection()
  {
    if (!this.isDead || !this.IsAbleToRescueByRemainRescueTime() || this.actionID == (Character.ACTION_ID) 24 && (double) this.rescueTime <= 0.0)
      return;
    this.StartCoroutine(this.OnPlayResurrection());
  }

  private IEnumerator OnPlayResurrection()
  {
    if (MonoBehaviourSingleton<EffectManager>.IsValid())
    {
      EffectManager.GetEffect("ef_btl_sk_heal_04_03", this._transform);
      yield break;
    }
  }

  private IEnumerator OnPlayAndDoResurrection(System.Action callback)
  {
    if (MonoBehaviourSingleton<EffectManager>.IsValid())
    {
      Animator effectAnim = (Animator) null;
      Transform effect = EffectManager.GetEffect("ef_btl_sk_heal_04_03", this._transform);
      if (Object.op_Inequality((Object) effect, (Object) null))
      {
        effectAnim = ((Component) effect).gameObject.GetComponent<Animator>();
        if (Object.op_Inequality((Object) effectAnim, (Object) null))
        {
          AnimatorStateInfo animatorStateInfo;
          while (true)
          {
            animatorStateInfo = effectAnim.GetCurrentAnimatorStateInfo(0);
            if (((AnimatorStateInfo) ref animatorStateInfo).fullPathHash != Animator.StringToHash("Base Layer.START"))
              yield return (object) null;
            else
              break;
          }
          while (true)
          {
            animatorStateInfo = effectAnim.GetCurrentAnimatorStateInfo(0);
            if ((double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime <= 1.0)
              yield return (object) null;
            else
              break;
          }
        }
      }
      effectAnim = (Animator) null;
    }
    if (this.isDead && callback != null)
      callback();
    this.ActivateRescueTimer();
  }

  private void CheckAutoRevive()
  {
    this.autoReviveHp = 0;
    if (!this.buffParam.IsValidBuff(BuffParam.BUFFTYPE.AUTO_REVIVE))
      return;
    this.autoReviveHp = Mathf.CeilToInt((float) this.buffParam.GetValue(BuffParam.BUFFTYPE.AUTO_REVIVE) * 0.01f * (float) this.hpMax);
    this.isValidAutoReviveSkillChargeBuff = this.buffParam.IsValidAutoReviveSkillChargeBuff();
  }

  public bool OnAutoRevive()
  {
    if (!this.isDead || (double) this.autoReviveHp <= 0.0)
      return false;
    this.DeactivateRescueTimer();
    this.StartCoroutine(this.OnPlayAndDoResurrection((System.Action) (() =>
    {
      this.ActDeadStandup(this.autoReviveHp, Player.eContinueType.AUTO_REVIVE);
      if (!this.isValidAutoReviveSkillChargeBuff)
        return;
      this.OnGetChargeSkillGauge(BuffParam.BUFFTYPE.AUTO_REVIVE_SKILL_CHARGE, 1, -1);
    })));
    return true;
  }

  public override bool OnBuffEnd(BuffParam.BUFFTYPE type, bool sync, bool isPlayEndEffect = true)
  {
    this.DeleteWeaponLinkEffect("BUFF_LOOP_" + type.ToString());
    bool flag = base.OnBuffEnd(type, sync, isPlayEndEffect);
    int index = 0;
    for (int count = this.m_weaponCtrlList.Count; index < count; ++index)
      this.m_weaponCtrlList[index].OnBuffEnd(type);
    return flag;
  }

  public void OnSyncSpecialActionGauge(int weaponIndex, float currentSpActionGauge)
  {
    this.spActionGauge[weaponIndex] = currentSpActionGauge;
  }

  public void RegisterWeaponLinkEffect(
    EffectPlayProcessor.EffectSetting weaponEffectSetting)
  {
    foreach (Player.WEAPON_EFFECT_DATA weaponEffectData in this.m_weaponEffectDataList)
    {
      if (weaponEffectData.setting.effectName == weaponEffectSetting.effectName)
        return;
    }
    this.m_weaponEffectDataList.Add(new Player.WEAPON_EFFECT_DATA()
    {
      setting = weaponEffectSetting
    });
  }

  public void CreateWeaponLinkEffect(string settingName)
  {
    foreach (Player.WEAPON_EFFECT_DATA weaponEffectData in this.m_weaponEffectDataList)
    {
      EffectPlayProcessor.EffectSetting setting = weaponEffectData.setting;
      if (!(setting.name != settingName))
      {
        Transform effect = EffectManager.GetEffect(setting.effectName, this.FindNode(setting.nodeName));
        if (Object.op_Inequality((Object) effect, (Object) null))
        {
          effect.localPosition = setting.position;
          effect.localRotation = Quaternion.Euler(setting.rotation);
          float num = setting.scale;
          if ((double) num == 0.0)
            num = 1f;
          effect.localScale = Vector3.op_Multiply(Vector3.one, num);
          weaponEffectData.effectObj = ((Component) effect).gameObject;
        }
      }
    }
  }

  public void DeleteWeaponLinkEffect(string settingName)
  {
    foreach (Player.WEAPON_EFFECT_DATA weaponEffectData in this.m_weaponEffectDataList)
    {
      if (!(weaponEffectData.setting.name != settingName) && Object.op_Inequality((Object) weaponEffectData.effectObj, (Object) null))
      {
        weaponEffectData.effectObj.transform.SetParent((Transform) null);
        EffectManager.ReleaseEffect(weaponEffectData.effectObj);
      }
    }
  }

  public void DeleteWeaponLinkEffectAll()
  {
    foreach (Player.WEAPON_EFFECT_DATA weaponEffectData in this.m_weaponEffectDataList)
    {
      if (Object.op_Inequality((Object) weaponEffectData.effectObj, (Object) null))
      {
        weaponEffectData.effectObj.transform.SetParent((Transform) null);
        EffectManager.ReleaseEffect(weaponEffectData.effectObj);
      }
    }
    this.m_weaponEffectDataList.Clear();
  }

  private void DetachRootEffectTemporary()
  {
    Transform attachTrans = MonoBehaviourSingleton<EffectManager>.IsValid() ? MonoBehaviourSingleton<EffectManager>.I._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform;
    this.effectTransTable.ForEachKeyAndValue((Action<string, Transform>) ((key, value) =>
    {
      if (!Object.op_Inequality((Object) value, (Object) null) || !Object.op_Equality((Object) value.parent, (Object) this.rootNode))
        return;
      value.parent = attachTrans;
      this.rootEffectDetachTemporaryTable.Add(key, value);
    }));
  }

  private void ReAttachRootEffect()
  {
    this.rootEffectDetachTemporaryTable.ForEach((Action<Transform>) (value =>
    {
      if (!Object.op_Inequality((Object) value, (Object) null))
        return;
      value.parent = this.rootNode;
    }));
    this.rootEffectDetachTemporaryTable.Clear();
  }

  public bool IsExistSpecialAction()
  {
    switch (this.attackMode)
    {
      case Player.ATTACK_MODE.ONE_HAND_SWORD:
      case Player.ATTACK_MODE.TWO_HAND_SWORD:
      case Player.ATTACK_MODE.PAIR_SWORDS:
        return true;
      case Player.ATTACK_MODE.SPEAR:
        return this.spAttackType != SP_ATTACK_TYPE.HEAT;
      default:
        return false;
    }
  }

  public virtual bool ActSpecialAction(bool start_effect = true, bool isSuccess = true)
  {
    bool flag1 = true;
    bool flag2 = true;
    switch (this.attackMode)
    {
      case Player.ATTACK_MODE.ONE_HAND_SWORD:
        switch (this.spAttackType)
        {
          case SP_ATTACK_TYPE.NONE:
          case SP_ATTACK_TYPE.HEAT:
          case SP_ATTACK_TYPE.BURST:
            this.ActGuard();
            break;
          case SP_ATTACK_TYPE.SOUL:
            this.ActAttack(this.playerParameter.ohsActionInfo.Soul_AlteredSpAttackId, false, false, "", "");
            this.snatchCtrl.OnShot();
            break;
          case SP_ATTACK_TYPE.ORACLE:
            int specialAttackId = this.playerParameter.ohsActionInfo.oracleOHSInfo.specialAttackId;
            string motionLayerName1 = this.GetMotionLayerName(this.attackMode, this.spAttackType, specialAttackId);
            this.ActAttack(specialAttackId, false, false, motionLayerName1, "");
            break;
        }
        break;
      case Player.ATTACK_MODE.TWO_HAND_SWORD:
        int spAttackId1 = this.playerParameter.specialActionInfo.spAttackID;
        string _motionLayerName1 = "Base Layer.";
        if (this.thsCtrl != null)
          this.thsCtrl.GetSpActionInfo(this.spAttackType, this.extraAttackType, ref spAttackId1, ref _motionLayerName1);
        this.ActAttack(spAttackId1, false, false, _motionLayerName1, "");
        break;
      case Player.ATTACK_MODE.SPEAR:
        int spAttackId2 = this.playerParameter.specialActionInfo.spAttackID;
        string _motionLayerName2 = "Base Layer.";
        if (this.spearCtrl != null)
          this.spearCtrl.GetSpActionInfo(this.spAttackType, this.extraAttackType, ref spAttackId2, ref _motionLayerName2);
        if (this.CheckSpAttackType(SP_ATTACK_TYPE.NONE))
          flag1 = false;
        this.ActAttack(spAttackId2, false, false, _motionLayerName2, "");
        break;
      case Player.ATTACK_MODE.PAIR_SWORDS:
        switch (this.spAttackType)
        {
          case SP_ATTACK_TYPE.NONE:
            if (this.attackID == 20)
            {
              this.ActAttack(this.playerParameter.pairSwordsActionInfo.wildDanceNoneChargeAttackID, false, false, "", "");
              break;
            }
            this.ActAttack(this.playerParameter.pairSwordsActionInfo.wildDanceAttackID, false, false, "", "");
            break;
          case SP_ATTACK_TYPE.HEAT:
            if (!isSuccess)
            {
              this.ActAttackFailure(97, false);
              break;
            }
            if (this.isBoostMode)
              return true;
            this.ActAttack(98, false, false, "", "");
            this.StartBoostMode();
            if (MonoBehaviourSingleton<EffectManager>.IsValid())
            {
              Transform effect1 = EffectManager.GetEffect("ef_btl_wsk_twinsword_01_02");
              if (Object.op_Inequality((Object) effect1, (Object) null))
              {
                effect1.position = this._position;
                this.pairSwordsBoostModeAuraEffectList.Add(effect1);
              }
              Transform effect2 = EffectManager.GetEffect("ef_btl_wsk_twinsword_01_03", this.FindNode("R_Wep"));
              if (Object.op_Inequality((Object) effect2, (Object) null))
              {
                effect2.localPosition = new Vector3(0.2f, 0.0f, 0.0f);
                this.pairSwordsBoostModeTrailEffectList.Add(effect2);
              }
              Transform effect3 = EffectManager.GetEffect("ef_btl_wsk_twinsword_01_03", this.FindNode("L_Wep"));
              if (Object.op_Inequality((Object) effect3, (Object) null))
              {
                effect3.localPosition = new Vector3(-0.2f, 0.0f, 0.0f);
                this.pairSwordsBoostModeTrailEffectList.Add(effect3);
              }
            }
            if (Object.op_Inequality((Object) this.playerSender, (Object) null))
            {
              this.playerSender.OnSyncSpActionGauge();
              break;
            }
            break;
          case SP_ATTACK_TYPE.SOUL:
            this.ActAttack(this.playerParameter.pairSwordsActionInfo.Soul_SpLaserWaitAttackId, false, false, "", "");
            this.pairSwordsCtrl.OnStartCharge();
            break;
          case SP_ATTACK_TYPE.BURST:
            if (!this.pairSwordsCtrl.ActBurstSpecialAction(ref start_effect))
              return false;
            flag2 = false;
            break;
          case SP_ATTACK_TYPE.ORACLE:
            int num = 44;
            string motionLayerName2 = this.GetMotionLayerName(this.attackMode, this.spAttackType, num);
            this.ActAttack(num, false, false, motionLayerName2, "");
            break;
        }
        break;
      case Player.ATTACK_MODE.ARROW:
        return false;
    }
    this.isActSpecialAction = flag1;
    if (start_effect && this.CanPlayEffectEvent())
    {
      Transform effect = EffectManager.GetEffect(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.spActionStartEffectName, this._transform);
      if (Object.op_Inequality((Object) effect, (Object) null))
        this.AddObjectList(((Component) effect).gameObject, Character.OBJECT_LIST_TYPE.STATIC);
    }
    if (flag2 && Object.op_Inequality((Object) this.playerSender, (Object) null))
      this.playerSender.OnActSpecialAction(start_effect, isSuccess);
    return true;
  }

  public bool IsSpecialActionHit(
    Player.ATTACK_MODE attack_mode,
    AttackHitInfo attack_info,
    AttackHitColliderProcessor.HitParam hit_param)
  {
    if (attack_info == null || attack_info.isSkillReference || attack_mode != this.attackMode)
      return false;
    bool flag = false;
    if (hit_param.processor != null)
    {
      BulletObject colliderInterface = hit_param.processor.colliderInterface as BulletObject;
      if (Object.op_Inequality((Object) colliderInterface, (Object) null))
        flag = colliderInterface.isAimMode;
    }
    return flag && (double) attack_info.rateInfoRate >= 1.0 || this.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.HEAT) && this.isBoostMode || this.spearCtrl.IsSpecialActionHit() || attack_info.toEnemy.isSpecialAttack;
  }

  private void SetValueSpActionGaugeMax(EquipItemTable.EquipItemData equipItemData, int weaponIndex)
  {
    if (equipItemData == null)
      return;
    float num = 1000f;
    bool flag = true;
    Player.ATTACK_MODE mode;
    switch (equipItemData.type)
    {
      case EQUIPMENT_TYPE.ONE_HAND_SWORD:
        if (equipItemData.spAttackType == SP_ATTACK_TYPE.NONE)
          return;
        flag = false;
        mode = Player.ATTACK_MODE.ONE_HAND_SWORD;
        break;
      case EQUIPMENT_TYPE.TWO_HAND_SWORD:
        switch (equipItemData.spAttackType)
        {
          case SP_ATTACK_TYPE.HEAT:
            num = 999f;
            flag = true;
            break;
          case SP_ATTACK_TYPE.SOUL:
            flag = false;
            break;
          case SP_ATTACK_TYPE.BURST:
            flag = false;
            break;
          default:
            return;
        }
        mode = Player.ATTACK_MODE.TWO_HAND_SWORD;
        break;
      case EQUIPMENT_TYPE.SPEAR:
        switch (equipItemData.spAttackType)
        {
          case SP_ATTACK_TYPE.NONE:
            return;
          case SP_ATTACK_TYPE.HEAT:
            num = 999f;
            flag = false;
            break;
          case SP_ATTACK_TYPE.SOUL:
          case SP_ATTACK_TYPE.BURST:
          case SP_ATTACK_TYPE.ORACLE:
            num = 1000f;
            flag = false;
            break;
          default:
            return;
        }
        mode = Player.ATTACK_MODE.SPEAR;
        break;
      case EQUIPMENT_TYPE.TWO_HAND_SWORD | EQUIPMENT_TYPE.SPEAR:
        return;
      case EQUIPMENT_TYPE.PAIR_SWORDS:
        switch (equipItemData.spAttackType)
        {
          case SP_ATTACK_TYPE.NONE:
            return;
          case SP_ATTACK_TYPE.HEAT:
          case SP_ATTACK_TYPE.BURST:
            flag = true;
            break;
          case SP_ATTACK_TYPE.SOUL:
            flag = false;
            break;
        }
        mode = Player.ATTACK_MODE.PAIR_SWORDS;
        break;
      case EQUIPMENT_TYPE.ARROW:
        switch (equipItemData.spAttackType)
        {
          case SP_ATTACK_TYPE.NONE:
          case SP_ATTACK_TYPE.HEAT:
            return;
          case SP_ATTACK_TYPE.SOUL:
          case SP_ATTACK_TYPE.BURST:
            flag = false;
            break;
        }
        mode = Player.ATTACK_MODE.ARROW;
        break;
      default:
        return;
    }
    if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid() && weaponIndex == this.weaponIndex)
      MonoBehaviourSingleton<UIPlayerStatus>.I.SetGaugeEffectColor(mode, equipItemData.spAttackType);
    if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid() && weaponIndex == this.weaponIndex)
      MonoBehaviourSingleton<UIEnduranceStatus>.I.SetGaugeEffectColor(equipItemData.spAttackType);
    this.spActionGaugeMax[weaponIndex] = num;
    if (!flag)
      return;
    this.spActionGauge[weaponIndex] = num;
  }

  public bool IsTwoHandSwordSpAttacking()
  {
    return this.attackMode == Player.ATTACK_MODE.TWO_HAND_SWORD && (this.spAttackType == SP_ATTACK_TYPE.NONE || this.spAttackType == SP_ATTACK_TYPE.HEAT) && (this.isActSpecialAction && this.IsFullCharge() || this.isActTwoHandSwordHeatCombo);
  }

  public bool IsTwoHandSwordHeatUseGauge()
  {
    if (this.attackMode != Player.ATTACK_MODE.TWO_HAND_SWORD || this.spAttackType != SP_ATTACK_TYPE.HEAT || !this.isActTwoHandSwordHeatCombo)
      return false;
    if (this.useGaugeLevel != 0)
      return true;
    this.UseSpGauge();
    return this.useGaugeLevel > 0;
  }

  public bool IsEnableChangeActionByLongTap()
  {
    return this.attackMode == Player.ATTACK_MODE.TWO_HAND_SWORD && this.thsCtrl != null && this.thsCtrl.IsEnableChangeActionByLongTap();
  }

  protected float GetBurstReloadMotionSpeedRate()
  {
    if (this.thsCtrl == null || !this.thsCtrl.IsEnableChangeReloadMotionSpeed || this.buffParam == null || this.buffParam.passive == null)
      return 1f;
    float num = this.buffParam.passive.burstReloadActionSpeed + 1f;
    return (double) num < 0.0 ? 1f : num;
  }

  private void FixedUpdateOneHandSword()
  {
    if (!this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL) || !this.snatchCtrl.IsSnatching())
      return;
    Vector3 forward = this._forward;
    forward.y = 0.0f;
    ((Vector3) ref forward).Normalize();
    Vector3 vector3 = Vector3.op_Subtraction(this.snatchCtrl.GetSnatchPos(), this._position);
    vector3.y = 0.0f;
    int num1 = (double) Vector3.Cross(forward, vector3).y >= 0.0 ? 1 : -1;
    float num2 = Vector3.Angle(forward, vector3);
    Quaternion rotation = this._rotation;
    Vector3 eulerAngles = ((Quaternion) ref rotation).eulerAngles;
    float num3 = num2;
    if ((double) this.rotateEventSpeed > 0.0)
    {
      num3 = this.rotateEventSpeed * Time.deltaTime;
      if ((double) num2 <= (double) num3)
        num3 = num2;
    }
    this._rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y + (float) num1 * num3, eulerAngles.z);
  }

  public void DeactiveSnatchMove()
  {
    this.enableEventMove = false;
    this.enableAddForce = false;
    this.eventMoveVelocity = Vector3.zero;
    this.SetVelocity(Vector3.zero);
    this.snatchCtrl.OnArrive();
  }

  public float GetRegionDamageRate(bool isDragonArmor)
  {
    float num = 1f;
    return isDragonArmor || !MonoBehaviourSingleton<InGameSettingsManager>.IsValid() || !this.CheckAttackModeAndSpType(Player.ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.SOUL) || !this.thsCtrl.IsSoulSpAttackId(this.attackID) || (double) this.chargeRate < 1.0 ? num : MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.soulRegionDamageRate;
  }

  public bool GetOneHandSwordBoostDownValue(ref int value, string attackInfoName)
  {
    value = 0;
    if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid() || !this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL) || this.actionID != Character.ACTION_ID.ATTACK || !this.isBoostMode || attackInfoName != this.playerParameter.ohsActionInfo.Soul_BoostSpAttackName)
      return false;
    value = this.playerParameter.ohsActionInfo.Soul_BoostSpAttackDownValue;
    return true;
  }

  public bool GetOneHandSwordBoostDamageUpRate(ref float value)
  {
    value = 1f;
    if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      return false;
    if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
    {
      if (this.actionID != Character.ACTION_ID.ATTACK || !this.isBoostMode)
        return false;
      value = this.playerParameter.ohsActionInfo.Soul_BoostElementDamageRate;
      return true;
    }
    if (!this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.BURST) || this.actionID != Character.ACTION_ID.ATTACK)
      return false;
    if (this.attackID == this.playerParameter.ohsActionInfo.burstOHSInfo.CounterAttackId)
    {
      value = this.playerParameter.ohsActionInfo.burstOHSInfo.elementDamageRate;
      return true;
    }
    if (!this.isBoostMode)
      return false;
    value = this.playerParameter.ohsActionInfo.burstOHSInfo.BoostElementDamageRate;
    return true;
  }

  public bool GetTwoHandSwordBoostDamageUpRate(ref float value)
  {
    value = 1f;
    if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid() || !this.CheckAttackModeAndSpType(Player.ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.SOUL) || this.actionID != Character.ACTION_ID.ATTACK || !this.isBoostMode)
      return false;
    value = this.playerParameter.twoHandSwordActionInfo.soulBoostElementDamageRate;
    return true;
  }

  public bool GetIaiNormalDamageUp(ref float value)
  {
    value = 1f;
    return this.CheckAttackModeAndSpType(Player.ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.SOUL) && this.thsCtrl != null && this.thsCtrl.GetIaiNormalDamageUp(ref value, this.attackID, this.chargeRate);
  }

  public bool GetArrowBoostDamageUpRate(ref float value)
  {
    value = 1f;
    if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid() || !this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.SOUL) || !this.isBoostMode)
      return false;
    value = this.playerParameter.arrowActionInfo.soulBoostElementDamageRate;
    return true;
  }

  public bool GetBurstShotNormalDamageUpRate(AttackHitInfo _info, ref float value)
  {
    value = 1f;
    return MonoBehaviourSingleton<InGameSettingsManager>.IsValid() && this.IsBurstTwoHandSword() && this.thsCtrl != null && this.thsCtrl.GetBurstShotNormalDamageUpRate(_info.attackType, ref value);
  }

  public bool GetBurstShotElementDamageUpRate(AttackHitInfo _info, ref float value)
  {
    value = 1f;
    return MonoBehaviourSingleton<InGameSettingsManager>.IsValid() && this.IsBurstTwoHandSword() && this.thsCtrl.GetBurstShotElementDamageUpRate(_info.attackType, ref value);
  }

  public bool GetBurstArrowBombElementDamageUpRate(AttackHitInfo info, ref float value)
  {
    value = 1f;
    int lv;
    if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid() || info.attackType != AttackHitInfo.ATTACK_TYPE.BOMB && info.attackType != AttackHitInfo.ATTACK_TYPE.BOOST_BOMB || !InGameUtility.GetBombLevelByAttackInfo((AttackInfo) info, out lv) || this.playerParameter.arrowActionInfo.arrowBombElementDamageRateList.Count <= lv)
      return false;
    value = this.playerParameter.arrowActionInfo.arrowBombElementDamageRateList[lv];
    return true;
  }

  public bool GetOracleOHSElementDamageUpRate(AttackHitInfo info, ref float value)
  {
    value = 1f;
    if (!this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.ORACLE))
      return false;
    value = info.attackType != AttackHitInfo.ATTACK_TYPE.OHS_ORACLE_SP ? (this.isBoostMode ? this.playerParameter.ohsActionInfo.oracleOHSInfo.boostNormalAttackElementDamageRate : this.playerParameter.ohsActionInfo.oracleOHSInfo.normalAttackElementDamageRate) : (this.isBoostMode ? this.playerParameter.ohsActionInfo.oracleOHSInfo.boostSpAttackElementDamageRate : this.playerParameter.ohsActionInfo.oracleOHSInfo.spAttackElementDamageRate);
    return true;
  }

  public bool GetOracleSpearElementDamageUpRate(AttackHitInfo info, ref float value)
  {
    value = 1f;
    if (!this.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE))
      return false;
    value = this.spearCtrl.GetOracleElementDamageRate(info);
    return true;
  }

  public bool GetOraclePairSwordsElementDamageUpRate(AttackHitInfo info, ref float value)
  {
    value = 1f;
    if (!this.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.ORACLE))
      return false;
    if (info.attackType == AttackHitInfo.ATTACK_TYPE.PAIR_SWORDS_ORACLE_SP)
      value = this.playerParameter.pairSwordsActionInfo.Oracle_SpElementDamageRate - 1f;
    else if (info.attackType == AttackHitInfo.ATTACK_TYPE.PAIR_SWORDS_ORACLE_RUSH_BOOST)
      value = this.playerParameter.pairSwordsActionInfo.Oracle_RushElementDamageRate - 1f;
    return true;
  }

  private float GetBoostAttackSpeedUp()
  {
    if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid() || this.actionID != Character.ACTION_ID.ATTACK)
      return 0.0f;
    switch (this.attackMode)
    {
      case Player.ATTACK_MODE.ONE_HAND_SWORD:
        switch (this.spAttackType)
        {
          case SP_ATTACK_TYPE.BURST:
            InGameSettingsManager.Player.BurstOneHandSwordActionInfo burstOhsInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.burstOHSInfo;
            return !this.isBoostMode || this.attackID == burstOhsInfo.CounterAttackId ? 0.0f : burstOhsInfo.boostAttackSpeedUpRate;
          case SP_ATTACK_TYPE.ORACLE:
            return !this.isBoostMode ? 0.0f : MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.oracleOHSInfo.boostMotionSpeedAdditionRate;
          default:
            return 0.0f;
        }
      case Player.ATTACK_MODE.TWO_HAND_SWORD:
        return !this.isBoostMode || this.spAttackType != SP_ATTACK_TYPE.SOUL || this.thsCtrl == null ? 0.0f : this.thsCtrl.TwoHandSwordBoostAttackSpeed;
      case Player.ATTACK_MODE.SPEAR:
        if (this.spAttackType == SP_ATTACK_TYPE.ORACLE)
          return this.spearCtrl.GetOracleAttackSpeedRate() - 1f;
        break;
      case Player.ATTACK_MODE.PAIR_SWORDS:
        return this.pairSwordsCtrl.GetAttackSpeedUpRate();
    }
    return 0.0f;
  }

  protected float GetAttackModeWalkSpeedUp()
  {
    if (this.actionID != Character.ACTION_ID.MOVE || !MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      return 0.0f;
    switch (this.attackMode)
    {
      case Player.ATTACK_MODE.TWO_HAND_SWORD:
        if (this.thsCtrl != null)
          return this.thsCtrl.GetWalkSpeedUp(this.spAttackType);
        break;
      case Player.ATTACK_MODE.SPEAR:
        if (this.spearCtrl != null)
          return this.spearCtrl.GetWalkSpeedUp(this.spAttackType);
        break;
      case Player.ATTACK_MODE.PAIR_SWORDS:
        if (this.pairSwordsCtrl != null)
          return this.pairSwordsCtrl.GetWalkSpeedUp(this.spAttackType);
        break;
    }
    return 0.0f;
  }

  private float GetAttackModeAvoidUp()
  {
    if (this.actionID != Character.ACTION_ID.MAX || !MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      return 0.0f;
    switch (this.attackMode)
    {
      case Player.ATTACK_MODE.SPEAR:
        if (this.spearCtrl != null)
          return this.spearCtrl.GetAvoidUp(this.spAttackType);
        break;
      case Player.ATTACK_MODE.PAIR_SWORDS:
        switch (this.spAttackType)
        {
          case SP_ATTACK_TYPE.HEAT:
          case SP_ATTACK_TYPE.SOUL:
            if (this.isBoostMode)
              return MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.boostAvoidUpRate;
            break;
          case SP_ATTACK_TYPE.BURST:
            return MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.Burst_AvoidSpeedUpRate;
        }
        break;
    }
    return 0.0f;
  }

  private void UpdateSpActionGauge()
  {
    if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid() || !this.isBoostMode)
      return;
    float num1 = 0.0f;
    switch (this.attackMode)
    {
      case Player.ATTACK_MODE.TWO_HAND_SWORD:
        if (this.spAttackType != SP_ATTACK_TYPE.SOUL)
          return;
        num1 = !this.enableInputCharge ? this.playerParameter.twoHandSwordActionInfo.soulBoostGaugeDecreasePerSecond * Time.deltaTime : this.playerParameter.twoHandSwordActionInfo.soulBoostChargeGaugeDecreasePerSecond * Time.deltaTime;
        break;
      case Player.ATTACK_MODE.SPEAR:
        if (this.spAttackType == SP_ATTACK_TYPE.ORACLE)
          return;
        break;
      case Player.ATTACK_MODE.ARROW:
        switch (this.spAttackType)
        {
          case SP_ATTACK_TYPE.SOUL:
            num1 = this.playerParameter.arrowActionInfo.soulBoostGaugeDecreasePerSecond * Time.deltaTime;
            break;
          case SP_ATTACK_TYPE.BURST:
            num1 = this.playerParameter.arrowActionInfo.burstBoostGaugeDecreasePerSecond * Time.deltaTime;
            break;
        }
        break;
    }
    float num2 = 1f + this.GetSpGaugeDecreasingRate();
    this.spActionGauge[this.weaponIndex] -= num1 * num2;
    if ((double) this.spActionGauge[this.weaponIndex] > 0.0)
      return;
    this.spActionGauge[this.weaponIndex] = 0.0f;
  }

  public float GetSpGaugeDecreasingRate()
  {
    return this.buffParam.GetGaugeDecreaseRate() + this.extraSpGaugeDecreasingRate;
  }

  private void CheckContinueBoostMode()
  {
    if (!this.isBoostMode)
      return;
    switch (this.attackMode)
    {
      case Player.ATTACK_MODE.ONE_HAND_SWORD:
      case Player.ATTACK_MODE.ARROW:
        if (this.spAttackType != SP_ATTACK_TYPE.SOUL && this.spAttackType != SP_ATTACK_TYPE.BURST && this.spAttackType != SP_ATTACK_TYPE.ORACLE || !this.IsCoopNone() && !this.IsOriginal() || (double) this.spActionGauge[this.weaponIndex] > 0.0)
          return;
        break;
      case Player.ATTACK_MODE.TWO_HAND_SWORD:
        if (this.spAttackType != SP_ATTACK_TYPE.SOUL || !this.IsCoopNone() && !this.IsOriginal() || (double) this.spActionGauge[this.weaponIndex] > 0.0)
          return;
        break;
      case Player.ATTACK_MODE.SPEAR:
        if (!this.IsCoopNone() && !this.IsOriginal())
          return;
        if (this.spAttackType == SP_ATTACK_TYPE.SOUL)
        {
          if ((double) this.spActionGauge[this.weaponIndex] > 0.0)
            return;
          break;
        }
        if (this.spAttackType != SP_ATTACK_TYPE.ORACLE || this.spearCtrl.StockedCount > 0)
          return;
        break;
      case Player.ATTACK_MODE.PAIR_SWORDS:
        if (this.pairSwordsCtrl.CheckContinueBoostMode())
          return;
        break;
    }
    this.FinishBoostMode();
  }

  public void IncreaseSpActonGauge(AttackHitInfo attackInfo, Vector3 hitPosition, float baseValue = 0.0f)
  {
    int num1 = attackInfo.dontIncreaseGauge ? 1 : 0;
    AttackHitInfo.ATTACK_TYPE attackType = attackInfo.attackType;
    bool isSpecialAttack = attackInfo.toEnemy.isSpecialAttack;
    float atkRate = attackInfo.atkRate;
    if (num1 != 0 || !MonoBehaviourSingleton<InGameSettingsManager>.IsValid() || attackType == AttackHitInfo.ATTACK_TYPE.BOMB && !this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.BURST))
      return;
    if (this.IsEvolveWeapon())
    {
      this.evolveCtrl.IncreaseCurrentGauge(attackType, this.attackMode);
    }
    else
    {
      bool flag = false;
      float _increaseValue = 0.0f;
      float _gaugeMax = 1000f;
      switch (this.attackMode)
      {
        case Player.ATTACK_MODE.ONE_HAND_SWORD:
          switch (this.spAttackType)
          {
            case SP_ATTACK_TYPE.NONE:
              return;
            case SP_ATTACK_TYPE.HEAT:
              switch (attackType)
              {
                case AttackHitInfo.ATTACK_TYPE.NORMAL:
                  _increaseValue = (float) ((double) baseValue / (double) this.hpMax * (double) this.playerParameter.ohsActionInfo.Heat_RevengeValue * (this._CheckJustGuardSec() ? (double) this.playerParameter.ohsActionInfo.Heat_RevengeJustGuardRate : (double) this.playerParameter.ohsActionInfo.Heat_RevengeGuardRate));
                  break;
                case AttackHitInfo.ATTACK_TYPE.COUNTER2:
                  _increaseValue = this.isJustGuard ? this.playerParameter.ohsActionInfo.Heat_RevengeJustCounterValue : this.playerParameter.ohsActionInfo.Heat_RevengeCounterValue;
                  break;
                default:
                  return;
              }
              flag = true;
              break;
            case SP_ATTACK_TYPE.SOUL:
              if (this.soulEnergyCtrl == null)
                return;
              if (this.attackID != this.playerParameter.ohsActionInfo.Soul_AlteredSpAttackId || !isSpecialAttack)
                return;
              SoulEnergy soulEnergy1 = this.soulEnergyCtrl.Get(this.playerParameter.ohsActionInfo.Soul_ComboGaugeIncreaseValue);
              if (soulEnergy1 == null)
                return;
              if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
                MonoBehaviourSingleton<UIPlayerStatus>.I.DirectionSoulGauge(soulEnergy1, hitPosition);
              if (!MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
                return;
              MonoBehaviourSingleton<UIEnduranceStatus>.I.DirectionSoulGauge(soulEnergy1, hitPosition);
              return;
            case SP_ATTACK_TYPE.BURST:
              if (attackType == AttackHitInfo.ATTACK_TYPE.COUNTER_BURST)
                _increaseValue = 1000f;
              flag = true;
              break;
            case SP_ATTACK_TYPE.ORACLE:
              _increaseValue = !this.isBoostMode ? this.playerParameter.ohsActionInfo.oracleOHSInfo.spGaugeIncreasingValue : this.playerParameter.ohsActionInfo.oracleOHSInfo.spGaugeIncreasingValueWhileBoost;
              flag = true;
              break;
          }
          break;
        case Player.ATTACK_MODE.TWO_HAND_SWORD:
          if (this.thsCtrl == null || !this.thsCtrl.GetSpGaugeIncreaseValue(attackType, this.attackID, atkRate, this.soulEnergyCtrl, hitPosition, this.chargeRate, ref _gaugeMax, ref _increaseValue))
            return;
          break;
        case Player.ATTACK_MODE.SPEAR:
          switch (this.spAttackType)
          {
            case SP_ATTACK_TYPE.NONE:
              return;
            case SP_ATTACK_TYPE.HEAT:
              if (attackType != AttackHitInfo.ATTACK_TYPE.NORMAL && attackType != AttackHitInfo.ATTACK_TYPE.FROM_AVOID)
                return;
              _gaugeMax = 999f;
              _increaseValue = (float) ((double) MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.jumpGaugeIncreaseBase * (double) this.weaponEquipItemDataList[this.weaponIndex].spAttackRate * 0.0099999997764825821);
              break;
            case SP_ATTACK_TYPE.SOUL:
              if (isSpecialAttack || this.soulEnergyCtrl == null)
                return;
              SoulEnergy soulEnergy2 = this.soulEnergyCtrl.Get(this.playerParameter.spearActionInfo.Soul_GaugeIncreaseValue);
              if (soulEnergy2 == null)
                return;
              if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
                MonoBehaviourSingleton<UIPlayerStatus>.I.DirectionSoulGauge(soulEnergy2, hitPosition);
              if (!MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
                return;
              MonoBehaviourSingleton<UIEnduranceStatus>.I.DirectionSoulGauge(soulEnergy2, hitPosition);
              return;
            case SP_ATTACK_TYPE.ORACLE:
              if (this.IsValidBuff(BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS) || this.spearCtrl.FullStocked)
                return;
              if ((double) baseValue > 0.0)
              {
                _increaseValue = baseValue / (float) this.hpMax * this.CurrentWeaponSpActionGaugeMax;
                break;
              }
              _increaseValue += this.playerParameter.spearActionInfo.oracle.spChargingValue * atkRate;
              break;
          }
          break;
        case Player.ATTACK_MODE.PAIR_SWORDS:
          switch (this.spAttackType)
          {
            case SP_ATTACK_TYPE.HEAT:
              if (attackType != AttackHitInfo.ATTACK_TYPE.NORMAL && attackType != AttackHitInfo.ATTACK_TYPE.FROM_AVOID || this.isBoostMode)
                return;
              _increaseValue = (float) ((double) MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.boostGaugeIncreaseBase * (double) this.weaponEquipItemDataList[this.weaponIndex].spAttackRate * 0.0099999997764825821);
              break;
            case SP_ATTACK_TYPE.SOUL:
              if (this.attackID == this.playerParameter.pairSwordsActionInfo.Soul_SpLaserShotAttackId)
                return;
              flag = true;
              _increaseValue = this.playerParameter.pairSwordsActionInfo.Soul_SoulGaugeIncreaseValueBySoulBullet;
              break;
            case SP_ATTACK_TYPE.BURST:
              if (attackType != AttackHitInfo.ATTACK_TYPE.NORMAL && attackType != AttackHitInfo.ATTACK_TYPE.FROM_AVOID || this.isBoostMode)
                return;
              if (this.pairSwordsCtrl.IsCombineMode())
                flag = true;
              _increaseValue = MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.Burst_BoostGaugeIncreaseBase;
              break;
          }
          break;
        case Player.ATTACK_MODE.ARROW:
          switch (this.spAttackType)
          {
            case SP_ATTACK_TYPE.SOUL:
              if (this.isBoostMode)
                return;
              flag = true;
              _increaseValue = this.playerParameter.arrowActionInfo.soulGaugeIncreaseValue;
              break;
            case SP_ATTACK_TYPE.BURST:
              int lv;
              if (this.isBoostMode || attackType != AttackHitInfo.ATTACK_TYPE.BOMB || !InGameUtility.GetBombLevelByAttackInfo((AttackInfo) attackInfo, out lv))
                return;
              List<float> increaseValueList = this.playerParameter.arrowActionInfo.burstGaugeIncreaseValueList;
              if (increaseValueList.Count <= lv)
                return;
              flag = true;
              _increaseValue = increaseValueList[lv];
              break;
          }
          break;
      }
      float num2 = 1f + this.buffParam.GetGaugeIncreaseRate(this.spAttackType);
      this.spActionGauge[this.weaponIndex] += this.CalcWaveMatchSpGauge(_increaseValue * num2);
      if ((double) this.spActionGauge[this.weaponIndex] < (double) _gaugeMax)
        return;
      this.spActionGauge[this.weaponIndex] = _gaugeMax;
      if (!(!this.isBoostMode & flag))
        return;
      this.StartBoostMode();
    }
  }

  public void IncreaseSoulGauge(float baseValue, bool isJust)
  {
    if (this.isDead || !MonoBehaviourSingleton<InGameSettingsManager>.IsValid() || this.spAttackType != SP_ATTACK_TYPE.SOUL)
      return;
    switch (this.attackMode)
    {
      case Player.ATTACK_MODE.ONE_HAND_SWORD:
        InGameSettingsManager.Player.OneHandSwordActionInfo ohsActionInfo = this.playerParameter.ohsActionInfo;
        float num1 = (float) ((double) baseValue * (isJust ? (double) ohsActionInfo.Soul_JustTapGaugeRate : 1.0) * (this.isBoostMode ? (double) ohsActionInfo.Soul_BoostModeGaugeRate : 1.0));
        if (!this.isBoostMode)
        {
          float num2 = 1f + this.buffParam.GetGaugeIncreaseRate(this.spAttackType);
          num1 *= num2;
        }
        this.spActionGauge[this.weaponIndex] += this.CalcWaveMatchSpGauge(num1);
        if ((double) this.spActionGauge[this.weaponIndex] < 1000.0)
          break;
        this.spActionGauge[this.weaponIndex] = 1000f;
        if (this.isBoostMode)
          break;
        this.StartBoostMode();
        break;
      case Player.ATTACK_MODE.TWO_HAND_SWORD:
        InGameSettingsManager.Player.TwoHandSwordActionInfo handSwordActionInfo = this.playerParameter.twoHandSwordActionInfo;
        float num3 = (float) ((double) baseValue * (isJust ? (double) handSwordActionInfo.soulJustTapGaugeRate : 1.0) * (this.isBoostMode ? (double) handSwordActionInfo.soulBoostModeGaugeRate : 1.0));
        if (!this.isBoostMode)
        {
          float num4 = 1f + this.buffParam.GetGaugeIncreaseRate(this.spAttackType);
          num3 *= num4;
        }
        this.spActionGauge[this.weaponIndex] += this.CalcWaveMatchSpGauge(num3);
        if ((double) this.spActionGauge[this.weaponIndex] < 1000.0)
          break;
        this.spActionGauge[this.weaponIndex] = 1000f;
        if (this.isBoostMode)
          break;
        this.StartBoostMode();
        break;
      case Player.ATTACK_MODE.SPEAR:
        InGameSettingsManager.Player.SpearActionInfo spearActionInfo = this.playerParameter.spearActionInfo;
        float num5 = (float) ((double) baseValue * (isJust ? (double) spearActionInfo.Soul_JustTapGaugeRate : 1.0) * (this.isBoostMode ? (double) spearActionInfo.Soul_BoostModeGaugeRate : 1.0));
        if (!this.isBoostMode)
          num5 *= 1f + this.buffParam.GetGaugeIncreaseRate(SP_ATTACK_TYPE.SOUL);
        this.spActionGauge[this.weaponIndex] += this.CalcWaveMatchSpGauge(num5);
        if ((double) this.spActionGauge[this.weaponIndex] < 1000.0)
          break;
        this.spActionGauge[this.weaponIndex] = 1000f;
        if (this.isBoostMode)
          break;
        this.StartBoostMode();
        break;
    }
  }

  public float CalcWaveMatchSpGauge(float value)
  {
    if (!QuestManager.IsValidInGameWaveMatch() || !MonoBehaviourSingleton<InGameSettingsManager>.IsValid() || this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.BURST))
      return value;
    InGameSettingsManager.WaveMatchParam waveMatchParam = MonoBehaviourSingleton<InGameSettingsManager>.I.GetWaveMatchParam();
    switch (waveMatchParam.spGaugeType)
    {
      case InGameSettingsManager.WaveMatchParam.eGaugeType.Zero:
        return 0.0f;
      case InGameSettingsManager.WaveMatchParam.eGaugeType.Rate:
        return value * waveMatchParam.spGaugeValue;
      case InGameSettingsManager.WaveMatchParam.eGaugeType.Constant:
        return waveMatchParam.spGaugeValue;
      default:
        return value;
    }
  }

  public void CountHitAttack() => ++this.attackHitCount;

  public void UpdateBoostHitCount()
  {
    if (!this.isBoostMode)
      return;
    switch (this.attackMode)
    {
      case Player.ATTACK_MODE.TWO_HAND_SWORD:
        if (this.spAttackType != SP_ATTACK_TYPE.SOUL || this.thsCtrl == null)
          break;
        this.thsCtrl.SetTwoHandSwordBoostAttackSpeed(this.thsCtrl.TwoHandSwordBoostAttackSpeed + this.playerParameter.twoHandSwordActionInfo.soulBoostAddAttackSpeed);
        break;
      case Player.ATTACK_MODE.PAIR_SWORDS:
        if (this.spAttackType != SP_ATTACK_TYPE.HEAT)
          break;
        ++this.boostModeDamageUpHitCount;
        if (this.boostModeDamageUpHitCount < MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.boostDamageUpLevelUpHitCount)
          break;
        int damageUpLevelMax = MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.boostDamageUpLevelMax;
        if (MonoBehaviourSingleton<EffectManager>.IsValid())
        {
          if (this.boostModeDamageUpLevel < damageUpLevelMax)
            EffectManager.OneShot("ef_btl_wsk_twinsword_01_04", this._position, Quaternion.identity);
          if (this.boostModeDamageUpLevel == damageUpLevelMax - 1)
          {
            Transform effect = EffectManager.GetEffect("ef_btl_wsk_twinsword_01_05");
            effect.position = this._position;
            this.pairSwordsBoostModeAuraEffectList.Add(effect);
          }
        }
        this.boostModeDamageUpHitCount = 0;
        ++this.boostModeDamageUpLevel;
        if (this.boostModeDamageUpLevel < damageUpLevelMax)
          break;
        this.boostModeDamageUpLevel = damageUpLevelMax;
        break;
    }
  }

  public bool IsSpActionGaugeFullCharged()
  {
    return this.IsValidSpActionGauge() && (double) this.CurrentWeaponSpActionGauge >= (double) this.CurrentWeaponSpActionGaugeMax;
  }

  public bool IsSpActionGaugeHalfCharged()
  {
    if (!this.IsValidSpActionGauge())
      return false;
    switch (this.attackMode)
    {
      case Player.ATTACK_MODE.ONE_HAND_SWORD:
      case Player.ATTACK_MODE.TWO_HAND_SWORD:
      case Player.ATTACK_MODE.SPEAR:
      case Player.ATTACK_MODE.ARROW:
        return this.IsSpActionGaugeFullCharged();
      case Player.ATTACK_MODE.PAIR_SWORDS:
        switch (this.spAttackType)
        {
          case SP_ATTACK_TYPE.HEAT:
            return (double) this.CurrentWeaponSpActionGauge >= (double) this.CurrentWeaponSpActionGaugeMax * 0.5;
          case SP_ATTACK_TYPE.SOUL:
          case SP_ATTACK_TYPE.BURST:
          case SP_ATTACK_TYPE.ORACLE:
            return this.IsSpActionGaugeFullCharged();
        }
        break;
    }
    return false;
  }

  public bool IsSpActionGaugeEmpty()
  {
    return !this.IsValidSpActionGauge() || (double) this.CurrentWeaponSpActionGauge == 0.0;
  }

  private void ResetSpActionGauge()
  {
    for (int index = 0; index < this.spActionGauge.Length; ++index)
      this.spActionGauge[index] = 0.0f;
  }

  private void ResetBoostModeAtkLevelUpAndHitCount()
  {
    this.boostModeDamageUpLevel = 0;
    this.boostModeDamageUpHitCount = 0;
    if (this.thsCtrl == null)
      return;
    this.thsCtrl.SetTwoHandSwordBoostAttackSpeed(this.playerParameter.twoHandSwordActionInfo.soulBoostMinAttackSpeed);
  }

  public void CheckBurstPairSwordBoost()
  {
    if (this.isBoostMode || (double) this.spActionGauge[this.weaponIndex] < 1000.0)
      return;
    this.StartBoostMode();
  }

  protected virtual bool StartBoostMode()
  {
    if (this.isBoostMode)
      return false;
    switch (this.attackMode)
    {
      case Player.ATTACK_MODE.ONE_HAND_SWORD:
        if (this.spAttackType == SP_ATTACK_TYPE.HEAT)
        {
          if (Object.op_Equality((Object) this.ohsMaxChargeEffect, (Object) null))
            this.ohsMaxChargeEffect = EffectManager.GetEffect("ef_btl_wsk_sword_01_04", this.FindNode("R_Wep"));
          if (Object.op_Inequality((Object) this.playerSender, (Object) null))
          {
            this.playerSender.OnSyncSoulBoost(true);
            break;
          }
          break;
        }
        if (this.spAttackType == SP_ATTACK_TYPE.SOUL)
        {
          SoundManager.PlayOneShotSE(this.playerParameter.twoHandSwordActionInfo.soulBoostSeId, (DisableNotifyMonoBehaviour) this, this.FindNode(""));
          Transform effect = EffectManager.GetEffect("ef_btl_wsk2_longsword_02_01");
          if (Object.op_Inequality((Object) effect, (Object) null))
            effect.position = this._position;
          if (this.twoHandSwordsBoostLoopEffect == null)
            this.twoHandSwordsBoostLoopEffect = EffectManager.GetEffect("ef_btl_wsk2_longsword_03_01", this.FindNode("Root"));
          this.StartWaitingPacket(StageObject.WAITING_PACKET.PLAYER_SOUL_BOOST, false, this.playerParameter.twoHandSwordActionInfo.soulBoostWaitPacketSec);
          if (Object.op_Inequality((Object) this.playerSender, (Object) null))
          {
            this.playerSender.OnSyncSoulBoost(true);
            break;
          }
          break;
        }
        if (this.spAttackType == SP_ATTACK_TYPE.BURST)
        {
          if (this.twoHandSwordsBoostLoopEffect == null)
            this.twoHandSwordsBoostLoopEffect = EffectManager.GetEffect("ef_btl_wsk3_sword_aura_01", this.FindNode("Root"));
          if (Object.op_Inequality((Object) this.playerSender, (Object) null))
          {
            this.playerSender.OnSyncSoulBoost(true);
            break;
          }
          break;
        }
        if (this.spAttackType != SP_ATTACK_TYPE.ORACLE)
          return false;
        this.ohsCtrl.OnStartOracleBoost();
        Transform effect1 = EffectManager.GetEffect($"ef_btl_wsk4_sword_02_{this.GetCurrentWeaponElement():D2}");
        if (Object.op_Inequality((Object) effect1, (Object) null))
          effect1.position = this._position;
        if (Object.op_Inequality((Object) this.playerSender, (Object) null))
        {
          this.playerSender.OnSyncSoulBoost(true);
          break;
        }
        break;
      case Player.ATTACK_MODE.TWO_HAND_SWORD:
        if (this.spAttackType != SP_ATTACK_TYPE.SOUL)
          return false;
        SoundManager.PlayOneShotSE(this.playerParameter.twoHandSwordActionInfo.soulBoostSeId, (DisableNotifyMonoBehaviour) this, this.FindNode(""));
        Transform effect2 = EffectManager.GetEffect("ef_btl_wsk2_longsword_02_01");
        if (Object.op_Inequality((Object) effect2, (Object) null))
          effect2.position = this._position;
        if (this.twoHandSwordsBoostLoopEffect == null)
          this.twoHandSwordsBoostLoopEffect = EffectManager.GetEffect("ef_btl_wsk2_longsword_03_01", this.FindNode("Root"));
        if (this.thsCtrl != null)
        {
          if (this.IsCoopNone() || this.IsOriginal())
            this.thsCtrl.SetTwoHandSwordBoostAttackSpeed(this.playerParameter.twoHandSwordActionInfo.soulBoostMinAttackSpeed);
          else
            this.thsCtrl.SetTwoHandSwordBoostAttackSpeed(this.playerParameter.twoHandSwordActionInfo.soulBoostMaxAttackSpeed);
        }
        this.StartWaitingPacket(StageObject.WAITING_PACKET.PLAYER_SOUL_BOOST, false, this.playerParameter.twoHandSwordActionInfo.soulBoostWaitPacketSec);
        if (Object.op_Inequality((Object) this.playerSender, (Object) null))
        {
          this.playerSender.OnSyncSoulBoost(true);
          break;
        }
        break;
      case Player.ATTACK_MODE.SPEAR:
        if (this.spAttackType == SP_ATTACK_TYPE.SOUL)
        {
          SoundManager.PlayOneShotSE(this.playerParameter.twoHandSwordActionInfo.soulBoostSeId, (DisableNotifyMonoBehaviour) this, this.FindNode(""));
          Transform effect3 = EffectManager.GetEffect("ef_btl_wsk2_longsword_02_01");
          if (Object.op_Inequality((Object) effect3, (Object) null))
            effect3.position = this._position;
          if (Object.op_Equality((Object) this.twoHandSwordsBoostLoopEffect, (Object) null))
            this.twoHandSwordsBoostLoopEffect = EffectManager.GetEffect("ef_btl_wsk2_longsword_03_01", this.FindNode("Root"));
          this.StartWaitingPacket(StageObject.WAITING_PACKET.PLAYER_SOUL_BOOST, false, this.playerParameter.twoHandSwordActionInfo.soulBoostWaitPacketSec);
        }
        else if (this.spAttackType == SP_ATTACK_TYPE.ORACLE)
          return false;
        if (Object.op_Inequality((Object) this.playerSender, (Object) null))
        {
          this.playerSender.OnSyncSoulBoost(true);
          break;
        }
        break;
      case Player.ATTACK_MODE.PAIR_SWORDS:
        switch (this.spAttackType)
        {
          case SP_ATTACK_TYPE.HEAT:
            this.boostModeDamageUpLevel = 1;
            break;
          case SP_ATTACK_TYPE.SOUL:
            SoundManager.PlayOneShotSE(this.playerParameter.twoHandSwordActionInfo.soulBoostSeId, (DisableNotifyMonoBehaviour) this, this.FindNode(""));
            Transform effect4 = EffectManager.GetEffect("ef_btl_wsk2_longsword_02_01");
            if (Object.op_Inequality((Object) effect4, (Object) null))
              effect4.position = this._position;
            if (this.twoHandSwordsBoostLoopEffect == null)
              this.twoHandSwordsBoostLoopEffect = EffectManager.GetEffect("ef_btl_wsk2_longsword_03_01", this.FindNode("Root"));
            this.StartWaitingPacket(StageObject.WAITING_PACKET.PLAYER_SOUL_BOOST, false, this.playerParameter.twoHandSwordActionInfo.soulBoostWaitPacketSec);
            if (Object.op_Inequality((Object) this.playerSender, (Object) null))
            {
              this.playerSender.OnSyncSoulBoost(true);
              break;
            }
            break;
          case SP_ATTACK_TYPE.BURST:
            SoundManager.PlayOneShotSE(10000051, (DisableNotifyMonoBehaviour) this, this.FindNode(""));
            if (this.twoHandSwordsBoostLoopEffect == null)
              this.twoHandSwordsBoostLoopEffect = EffectManager.GetEffect("ef_btl_wsk3_sword_aura_01", this.FindNode("Root"));
            if (Object.op_Inequality((Object) this.playerSender, (Object) null))
            {
              this.playerSender.OnSyncSoulBoost(true);
              break;
            }
            break;
        }
        break;
      case Player.ATTACK_MODE.ARROW:
        switch (this.spAttackType)
        {
          case SP_ATTACK_TYPE.SOUL:
            SoundManager.PlayOneShotSE(this.playerParameter.twoHandSwordActionInfo.soulBoostSeId, (DisableNotifyMonoBehaviour) this, this.FindNode(""));
            SoundManager.PlayOneShotUISE(40000359);
            Transform effect5 = EffectManager.GetEffect("ef_btl_wsk2_longsword_02_01");
            if (Object.op_Inequality((Object) effect5, (Object) null))
              effect5.position = this._position;
            if (this.twoHandSwordsBoostLoopEffect == null)
              this.twoHandSwordsBoostLoopEffect = EffectManager.GetEffect("ef_btl_wsk2_longsword_03_01", this.FindNode("Root"));
            this.StartWaitingPacket(StageObject.WAITING_PACKET.PLAYER_SOUL_BOOST, false, (float) (1000.0 / ((double) this.playerParameter.arrowActionInfo.soulBoostGaugeDecreasePerSecond * (double) (1f + this.GetSpGaugeDecreasingRate()))));
            if (Object.op_Inequality((Object) this.playerSender, (Object) null))
            {
              this.playerSender.OnSyncSoulBoost(true);
              break;
            }
            break;
          case SP_ATTACK_TYPE.BURST:
            SoundManager.PlayOneShotSE(this.playerParameter.arrowActionInfo.burstBoostModeSEId, (DisableNotifyMonoBehaviour) this, this.FindNode(""));
            if (this.twoHandSwordsBoostLoopEffect == null)
              this.twoHandSwordsBoostLoopEffect = EffectManager.GetEffect("ef_btl_wsk3_sword_aura_01", this.FindNode("Root"));
            if (Object.op_Inequality((Object) this.playerSender, (Object) null))
            {
              this.playerSender.OnSyncSoulBoost(true);
              break;
            }
            break;
        }
        break;
    }
    this.isBoostMode = true;
    return true;
  }

  public void FinishBoostMode()
  {
    if (!this.isBoostMode)
      return;
    switch (this.attackMode)
    {
      case Player.ATTACK_MODE.ONE_HAND_SWORD:
        if (this.spAttackType == SP_ATTACK_TYPE.HEAT)
          this.ReleaseEffect(ref this.ohsMaxChargeEffect);
        else if (this.spAttackType == SP_ATTACK_TYPE.SOUL || this.spAttackType == SP_ATTACK_TYPE.BURST)
        {
          this.ReleaseEffect(ref this.twoHandSwordsBoostLoopEffect);
          this.EndWaitingPacket(StageObject.WAITING_PACKET.PLAYER_SOUL_BOOST);
        }
        else if (this.spAttackType == SP_ATTACK_TYPE.ORACLE)
          this.ohsCtrl.OnEndOracleBoost();
        if (Object.op_Inequality((Object) this.playerSender, (Object) null))
        {
          this.playerSender.OnSyncSoulBoost(false);
          break;
        }
        break;
      case Player.ATTACK_MODE.TWO_HAND_SWORD:
        if (this.spAttackType != SP_ATTACK_TYPE.SOUL)
          return;
        this.ReleaseEffect(ref this.twoHandSwordsBoostLoopEffect);
        this.EndWaitingPacket(StageObject.WAITING_PACKET.PLAYER_SOUL_BOOST);
        if (Object.op_Inequality((Object) this.playerSender, (Object) null))
        {
          this.playerSender.OnSyncSoulBoost(false);
          break;
        }
        break;
      case Player.ATTACK_MODE.SPEAR:
        if (!this.CheckSpAttackType(SP_ATTACK_TYPE.SOUL) && !this.CheckSpAttackType(SP_ATTACK_TYPE.ORACLE))
          return;
        this.ReleaseEffect(ref this.twoHandSwordsBoostLoopEffect);
        this.EndWaitingPacket(StageObject.WAITING_PACKET.PLAYER_SOUL_BOOST);
        if (Object.op_Inequality((Object) this.playerSender, (Object) null))
        {
          this.playerSender.OnSyncSoulBoost(false);
          break;
        }
        break;
      case Player.ATTACK_MODE.PAIR_SWORDS:
        switch (this.spAttackType)
        {
          case SP_ATTACK_TYPE.HEAT:
            if (MonoBehaviourSingleton<EffectManager>.IsValid())
            {
              if (!this.pairSwordsBoostModeTrailEffectList.IsNullOrEmpty<Transform>())
              {
                for (int index = 0; index < this.pairSwordsBoostModeTrailEffectList.Count; ++index)
                {
                  EffectManager.ReleaseEffect(((Component) this.pairSwordsBoostModeTrailEffectList[index]).gameObject);
                  this.pairSwordsBoostModeTrailEffectList[index] = (Transform) null;
                }
                this.pairSwordsBoostModeTrailEffectList.Clear();
              }
              if (!this.pairSwordsBoostModeAuraEffectList.IsNullOrEmpty<Transform>())
              {
                for (int index = 0; index < this.pairSwordsBoostModeAuraEffectList.Count; ++index)
                {
                  EffectManager.ReleaseEffect(((Component) this.pairSwordsBoostModeAuraEffectList[index]).gameObject);
                  this.pairSwordsBoostModeAuraEffectList[index] = (Transform) null;
                }
                this.pairSwordsBoostModeAuraEffectList.Clear();
              }
            }
            if (Object.op_Inequality((Object) this.playerSender, (Object) null))
            {
              this.playerSender.OnSyncSpActionGauge();
              break;
            }
            break;
          case SP_ATTACK_TYPE.SOUL:
            this.ReleaseEffect(ref this.twoHandSwordsBoostLoopEffect);
            this.EndWaitingPacket(StageObject.WAITING_PACKET.PLAYER_SOUL_BOOST);
            if (Object.op_Inequality((Object) this.playerSender, (Object) null))
            {
              this.playerSender.OnSyncSoulBoost(false);
              break;
            }
            break;
          case SP_ATTACK_TYPE.BURST:
            this.ReleaseEffect(ref this.twoHandSwordsBoostLoopEffect);
            this.EndWaitingPacket(StageObject.WAITING_PACKET.PLAYER_SOUL_BOOST);
            if (Object.op_Inequality((Object) this.playerSender, (Object) null))
            {
              this.playerSender.OnSyncSoulBoost(false);
              break;
            }
            break;
        }
        break;
      case Player.ATTACK_MODE.ARROW:
        switch (this.spAttackType)
        {
          case SP_ATTACK_TYPE.SOUL:
            this.ReleaseEffect(ref this.twoHandSwordsBoostLoopEffect);
            if (MonoBehaviourSingleton<TargetMarkerManager>.IsValid())
              MonoBehaviourSingleton<TargetMarkerManager>.I.EndMultiLockBoost();
            this.EndWaitingPacket(StageObject.WAITING_PACKET.PLAYER_SOUL_BOOST);
            if (Object.op_Inequality((Object) this.playerSender, (Object) null))
            {
              this.playerSender.OnSyncSoulBoost(false);
              break;
            }
            break;
          case SP_ATTACK_TYPE.BURST:
            this.ReleaseEffect(ref this.twoHandSwordsBoostLoopEffect);
            this.EndWaitingPacket(StageObject.WAITING_PACKET.PLAYER_SOUL_BOOST);
            if (Object.op_Inequality((Object) this.playerSender, (Object) null))
            {
              this.playerSender.OnSyncSoulBoost(false);
              break;
            }
            break;
        }
        break;
    }
    this.isBoostMode = false;
    this.ResetBoostModeAtkLevelUpAndHitCount();
  }

  private void ResetBoostPowerUpTriggerDamage()
  {
    if (!this.isBoostMode || this.attackMode != Player.ATTACK_MODE.TWO_HAND_SWORD || this.spAttackType != SP_ATTACK_TYPE.SOUL || !this.playerParameter.twoHandSwordActionInfo.isSoulBoostResetTriggerDamage || !this.IsCoopNone() && !this.IsOriginal())
      return;
    this.ResetBoostModeAtkLevelUpAndHitCount();
  }

  public void OnSoulBoost(bool isBoost)
  {
    if (isBoost)
      this.StartBoostMode();
    else
      this.FinishBoostMode();
  }

  public float CalcPairSwordsBoostModeDamageUpRate()
  {
    if (this.attackMode != Player.ATTACK_MODE.PAIR_SWORDS || this.spAttackType != SP_ATTACK_TYPE.HEAT || !this.isBoostMode || !MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      return 1f;
    InGameSettingsManager.Player.PairSwordsActionInfo swordsActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo;
    float num1 = (float) (1.0 + (double) this.boostModeDamageUpLevel * (double) swordsActionInfo.boostDamageUpRatePerLevel);
    float num2 = (float) (1.0 + (double) swordsActionInfo.boostDamageUpLevelMax * (double) swordsActionInfo.boostDamageUpRatePerLevel);
    if ((double) num1 > (double) num2)
      num1 = num2;
    return num1;
  }

  public bool GetPairSwordsRadiusCustomRate(ref float rRate)
  {
    if (!this.evolveCtrl.IsExecSphinx())
      return false;
    rRate = this.evolveCtrl.GetSphinxRangeUpRate();
    return true;
  }

  public bool GetSphinxElementDamageUpRate(AttackHitInfo info, ref float rRate)
  {
    rRate = 1f;
    if (!this.evolveCtrl.IsExecSphinx() || !info.toEnemy.isSpecialAttack)
      return false;
    rRate = this.evolveCtrl.GetSphinxElementDamageUpRate();
    return true;
  }

  public bool ActSpAttackContinue()
  {
    if (!this.isActSpecialAction)
      return false;
    switch (this.attackMode)
    {
      case Player.ATTACK_MODE.ONE_HAND_SWORD:
        if (!this.IsAbleToFlickAttackBySoulOneHandSword())
          return false;
        this.ActAttack(this.snatchCtrl.GetAttackId(this.flickDirection), true, false, "", "");
        break;
      case Player.ATTACK_MODE.TWO_HAND_SWORD:
        if (!this.ActTwoHandSwordSpAttackContinue())
          return false;
        break;
      default:
        return false;
    }
    if (Object.op_Inequality((Object) this.playerSender, (Object) null))
      this.playerSender.OnActSpAttackContinue();
    return true;
  }

  public bool IsSoulOneHandSwordBoostMode()
  {
    return this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL) && this.isBoostMode;
  }

  public bool IsAbleToFlickAttackBySoulOneHandSword()
  {
    return this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL) && this.enableSpAttackContinue;
  }

  private bool ActTwoHandSwordSpAttackContinue()
  {
    if (this.IsOracleTwoHandSword() && this.thsCtrl.oracleCtrl.IsHorizontalAttack)
      return true;
    if (!this.CheckAttackModeAndSpType(Player.ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.HEAT) || !this.isActSpecialAction || !this.IsFullCharge() || !this.isHitSpAttack || this.thsCtrl != null && this.thsCtrl.IsTwoHandSwordSpAttackContinueTimeOut(this.hitSpAttackContinueTimer))
      return false;
    this.isHitSpAttack = false;
    this.ActAttack(89, false, false, "", "");
    return true;
  }

  protected override void UpdateAction()
  {
    base.UpdateAction();
    switch (this.actionID)
    {
      case Character.ACTION_ID.IDLE:
        if (this.prayTargetInfos.Count <= 0)
          break;
        int index1 = 0;
        for (int count = this.prayTargetInfos.Count; index1 < count; ++index1)
        {
          Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(this.prayTargetInfos[index1].targetId) as Player;
          if (Object.op_Inequality((Object) player, (Object) null) && (player.isDead || player.IsStone()))
          {
            this.ActPrayer();
            break;
          }
        }
        break;
      case (Character.ACTION_ID) 18:
      case (Character.ACTION_ID) 47:
        if (!this.isStunnedLoop)
          break;
        this.UpdateStunnedEffect();
        break;
      case (Character.ACTION_ID) 24:
        if (!this.isStopCounter)
        {
          if (this.IsPrayed() && (double) this.deadStopTime > 0.0)
          {
            float num = Time.deltaTime * (this.IsBoostByType(Player.BOOST_PRAY_TYPE.GUARD_ONE_HAND_SWORD_NORMAL) ? this.playerParameter.ohsActionInfo.Normal_PrayBoostRate : 1f);
            switch (this.prayerIds.Count)
            {
              case 2:
                num *= 1.2f;
                break;
              case 3:
                num *= 1.4f;
                break;
            }
            this.prayerTime += num * (this.IsBoostByType(Player.BOOST_PRAY_TYPE.IN_BARRIER) ? this.playerParameter.rescueSpeedRateInBarrier : 1f);
          }
          else if (!this.IsPrayed())
          {
            this.prayerTime -= Time.deltaTime;
            if ((double) this.prayerTime < 0.0)
              this.prayerTime = 0.0f;
          }
        }
        if ((double) this.rescueTime <= 0.0 && (double) this.deadStartTime >= 0.0)
        {
          this.UpdateRevivalRangeEffect();
          if ((MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameWaveMatch(true)) && (double) this.continueTime > 0.0)
          {
            this.continueTime = 0.0f;
            this.OnEndContinueTimeEnd();
          }
          else if ((double) this.continueTime > 0.0 && !this.isProgressStop())
          {
            this.continueTime -= Time.deltaTime;
            if ((double) this.continueTime <= 0.0)
            {
              this.continueTime = 0.0f;
              this.OnEndContinueTimeEnd();
            }
          }
        }
        if (!this.IsCoopNone() && !this.IsOriginal() || !this.IsPrayed() || !this.isDead || !this.isRevivalEnabled)
          break;
        this.ActDeadStandup(this.hpMax, Player.eContinueType.RESCUE);
        break;
      case (Character.ACTION_ID) 26:
        bool flag = true;
        int index2 = 0;
        for (int count = this.prayTargetInfos.Count; index2 < count; ++index2)
        {
          Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(this.prayTargetInfos[index2].targetId) as Player;
          switch (this.prayTargetInfos[index2].reason)
          {
            case Player.PRAY_REASON.DEAD:
              if (Object.op_Inequality((Object) player, (Object) null) && player.isDead)
              {
                flag = false;
                break;
              }
              break;
            case Player.PRAY_REASON.STONE:
              if (Object.op_Inequality((Object) player, (Object) null) && player.IsStone())
              {
                flag = false;
                break;
              }
              break;
          }
          if (!flag)
            break;
        }
        if (!flag)
          break;
        this.ActIdle(false, -1f);
        break;
      case (Character.ACTION_ID) 27:
        if (!this.isChangingWeapon || this.isLoading || (double) this.changeWeaponStartTime < 0.0 || (double) Time.time - (double) this.changeWeaponStartTime < (double) this.playerParameter.changeWeaponMinTime)
          break;
        this.ActIdle(false, -1f);
        break;
      case (Character.ACTION_ID) 43:
        if (!this.isStopCounter)
        {
          if (this.IsPrayed() && (double) this.stoneStopTime > 0.0)
          {
            float num = Time.deltaTime * (this.IsBoostByType(Player.BOOST_PRAY_TYPE.GUARD_ONE_HAND_SWORD_NORMAL) ? this.playerParameter.ohsActionInfo.Normal_PrayBoostRate : 1f);
            switch (this.prayerIds.Count)
            {
              case 2:
                num *= 1.2f;
                break;
              case 3:
                num *= 1.4f;
                break;
            }
            this.prayerTime += num * (this.IsBoostByType(Player.BOOST_PRAY_TYPE.IN_BARRIER) ? this.playerParameter.rescueSpeedRateInBarrier : 1f);
          }
          else if (!this.IsPrayed())
          {
            this.prayerTime -= Time.deltaTime;
            if ((double) this.prayerTime < 0.0)
              this.prayerTime = 0.0f;
          }
        }
        if ((double) this.stoneRescueTime <= 0.0 && (double) this.stoneStartTime >= 0.0)
        {
          this.UpdateRevivalRangeEffect();
          this.ActStoneEnd(this.stoneRescueTime);
        }
        if (!this.IsCoopNone() && !this.IsOriginal() || !this.IsPrayed() || !this.IsStone() || !this.isRevivalEnabled)
          break;
        this.ActStoneEnd(this.stoneRescueTime);
        break;
    }
  }

  protected override void OnPlayingEndMotion()
  {
    switch (this.actionID)
    {
      case Character.ACTION_ID.DEAD:
        if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsForceDefeatQuest())
        {
          this.PlayMotion(125);
          this.OnEndContinueTimeEnd();
          return;
        }
        this.ActDeadLoop();
        return;
      case (Character.ACTION_ID) 22:
        if (this.isSkillCastState)
        {
          this.isSkillCastState = false;
          this.PlayMotion(this.skillInfo.actSkillParam.tableData.actStateName);
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
    Character.MOVE_TYPE moveType = this.moveType;
    int num = this.isPlayingEndMotion ? 1 : 0;
    base.EndAction();
    switch (actionId)
    {
      case Character.ACTION_ID.IDLE:
        if (Object.op_Inequality((Object) this.loader, (Object) null))
        {
          this.loader.eyeBlink = false;
          break;
        }
        break;
      case Character.ACTION_ID.MOVE:
        bool flag = false;
        if (MonoBehaviourSingleton<InGameProgress>.IsValid() && (MonoBehaviourSingleton<InGameProgress>.I.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_VICTORY || MonoBehaviourSingleton<InGameProgress>.I.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_SERIES_INTERVAL || MonoBehaviourSingleton<InGameProgress>.I.progressEndType == InGameProgress.PROGRESS_END_TYPE.RUSH_INTERVAL || MonoBehaviourSingleton<InGameProgress>.I.progressEndType == InGameProgress.PROGRESS_END_TYPE.ARENA_INTERVAL))
          flag = true;
        if (!flag && moveType == Character.MOVE_TYPE.SYNC_VELOCITY)
        {
          PlayerLoader.SetLayerWithChildren_SecondaryNoChange(this._transform, 8);
          break;
        }
        break;
      case Character.ACTION_ID.ATTACK | Character.ACTION_ID.FREEZE:
        this.stumbleEndTime = 0.0f;
        break;
      case Character.ACTION_ID.PARALYZE | Character.ACTION_ID.FREEZE:
        this.shakeEndTime = 0.0f;
        break;
      case (Character.ACTION_ID) 16 /*0x10*/:
      case (Character.ACTION_ID) 17:
      case (Character.ACTION_ID) 18:
      case (Character.ACTION_ID) 47:
        this._rigidbody.constraints = (RigidbodyConstraints) (this._rigidbody.constraints | 4);
        this.ResetIgnoreColliders();
        if (actionId == (Character.ACTION_ID) 18 || actionId == (Character.ACTION_ID) 47)
        {
          this.isStunnedLoop = false;
          this.stunnedEndTime = 0.0f;
          this.stunnedTime = 0.0f;
          this.stunnedReduceEnableTime = 0.0f;
          this.UpdateStunnedEffect();
          break;
        }
        break;
      case (Character.ACTION_ID) 22:
        if (Object.op_Inequality((Object) this.skillRangeEffect, (Object) null))
        {
          EffectManager.ReleaseEffect(((Component) this.skillRangeEffect).gameObject);
          this.skillRangeEffect = (Transform) null;
        }
        this.isUsingSecondGradeSkill = false;
        this.skillInfo.ResetSecondGradeFlags();
        break;
      case (Character.ACTION_ID) 23:
        if (Object.op_Inequality((Object) this.uiPlayerStatusGizmo, (Object) null))
          this.uiPlayerStatusGizmo.SetVisible(true);
        this.SetHitOffTimer(StageObject.HIT_OFF_FLAG.BATTLE_START, this.playerParameter.battleStartHitOffTime);
        break;
      case (Character.ACTION_ID) 24:
        this.ResetIgnoreColliders();
        this.prayerIds.Clear();
        this.boostPrayTargetInfoList.Clear();
        this.boostPrayedInfoList.Clear();
        this._rescueTime = 0.0f;
        this.continueTime = 0.0f;
        this.deadStartTime = -1f;
        this.deadStopTime = -1f;
        if (Object.op_Inequality((Object) this.revivalRangEffect, (Object) null))
        {
          EffectManager.ReleaseEffect(this.revivalRangEffect);
          this.revivalRangEffect = (GameObject) null;
          break;
        }
        break;
      case (Character.ACTION_ID) 25:
        this.SetHitOffTimer(StageObject.HIT_OFF_FLAG.DEAD_STANDUP, this.playerParameter.deadStandupHitOffTime);
        break;
      case (Character.ACTION_ID) 28:
        if (!this.isAppliedGather && (this.IsCoopNone() || this.IsOriginal()))
        {
          this.ApplyGather();
          break;
        }
        break;
      case (Character.ACTION_ID) 30:
        this.PostRestraint();
        break;
      case (Character.ACTION_ID) 43:
        this.PostStone();
        this.prayerIds.Clear();
        this.boostPrayTargetInfoList.Clear();
        this.boostPrayedInfoList.Clear();
        this._stoneRescueTime = 0.0f;
        this.stoneStartTime = -1f;
        this.stoneStopTime = -1f;
        if (Object.op_Inequality((Object) this.revivalRangEffect, (Object) null))
        {
          EffectManager.ReleaseEffect(this.revivalRangEffect);
          this.revivalRangEffect = (GameObject) null;
          break;
        }
        break;
    }
    this.EndWaitingPacket(StageObject.WAITING_PACKET.PLAYER_CHARGE_RELEASE);
    this.EndWaitingPacket(StageObject.WAITING_PACKET.PLAYER_APPLY_CHANGE_WEAPON);
    if (Object.op_Inequality((Object) this.loader.shadow, (Object) null) && !((Component) this.loader.shadow).gameObject.activeSelf && Object.op_Equality((Object) this.shadow, (Object) null))
      ((Component) this.loader.shadow).gameObject.SetActive(true);
    if (!this.isArrowAimKeep)
    {
      if (this.isArrowAimBossMode)
        this.SetArrowAimBossMode(false);
      if (this.isArrowAimLesserMode)
        this.SetArrowAimLesserMode(false);
    }
    this.isArrowAimKeep = false;
    this.isArrowAimEnd = false;
    this.CancelCannonMode();
    this.skillInfo.skillIndex = -1;
    this.enableInputCombo = false;
    this.controllerInputCombo = false;
    this.inputComboID = -1;
    this.inputComboMotionState = "";
    this.enableComboTrans = false;
    this.inputComboFlag = false;
    this.enableInputCharge = false;
    this.enableFlickAction = false;
    this.enableInputNextTrigger = false;
    this.enableNextTriggerTrans = false;
    this.inputNextTriggerFlag = false;
    this.inputNextTriggerIndex = 0;
    this.isCountLongTouch = false;
    this.countLongTouchSec = 0.0f;
    this.inputChargeAutoRelease = false;
    this.inputChargeMaxTiming = false;
    this.inputChargeTimeMax = 0.0f;
    this.inputChargeTimeOffset = 0.0f;
    this.inputChargeTimeCounter = 0.0f;
    this.isInputChargeExistOffset = false;
    this.chargeRate = 0.0f;
    this.enableInputRotate = false;
    this.startInputRotate = false;
    this.inputBlowClearFlag = false;
    this.isActSkillAction = false;
    this.isSkillCastState = false;
    this.isSkillCastLoop = false;
    this.skillCastLoopStartTime = -1f;
    this.skillCastLoopTime = 0.0f;
    this.skillCastLoopTrigger = (string) null;
    this.isAppliedSkillParam = false;
    this.isActSpecialAction = false;
    this.isActOneHandSwordCounter = false;
    this.hitSpearSpecialAction = false;
    this.lockedSpearCancelAction = false;
    this.actSpecialActionTimer = 0.0f;
    this.actRushLoopTimer = 0.0f;
    this.isCanRushRelease = false;
    this.isChargeExRush = false;
    this.isLoopingRush = false;
    this.hitSpearSpActionTimer = 0.0f;
    this.isSpearHundred = false;
    this.spearHundredSecFromLastTap = 0.0f;
    this.spearHundredSecFromStart = 0.0f;
    this.isArrowAimable = false;
    this.enableCancelToAvoid = false;
    this.enableCancelToMove = false;
    this.enableCancelToAttack = false;
    this.enableCancelToSkill = false;
    this.enableCancelToSpecialAction = false;
    this.enableCancelToEvolveSpecialAction = false;
    this.enableCancelToCarryPut = false;
    this.enableCounterAttack = false;
    this.enableSpAttackContinue = false;
    this.enableAnimSeedRate = false;
    this.prayerTime = 0.0f;
    this.isGuardWalk = false;
    this.isCarryWalk = false;
    this.enableSuperArmor = false;
    this.shotArrowCount = 0;
    this.changeWeaponItem = (CharaInfo.EquipItem) null;
    this.changeWeaponIndex = -1;
    this.isChangingWeapon = false;
    this.changeWeaponStartTime = -1f;
    this.targetGatherPoint = (GatherPointObject) null;
    this.isAppliedGather = false;
    this.isHitSpAttack = false;
    this.isLockedSpAttackContinue = false;
    this.hitSpAttackContinueTimer = 0.0f;
    this.enableSpAttackContinue = false;
    this.enableAttackNext = false;
    this.enableWeaponAction = false;
    this.enableRotateToTargetPoint = false;
    this.isAnimEventStatusUpDefence = false;
    this.animEventStatusUpDefenceRate = 1f;
    this.arrowBulletSpeedUpRate = 0.0f;
    this.isChargeExpandAutoRelease = false;
    this.chargeExpandRate = 0.0f;
    this.timeChargeExpandMax = 0.0f;
    this.timerChargeExpandOffset = 0.0f;
    this.timerChargeExpand = 0.0f;
    this.isSpearJumpAim = false;
    this.jumpActionCounter = 0.0f;
    this.useGaugeLevel = 0;
    this.isAerial = false;
    if (this.jumpState != Player.eJumpState.None)
      ((Component) this.body).transform.localPosition = Vector3.zero;
    this.jumpFallBodyPosition = Vector3.zero;
    this.jumpRandingVector = Vector3.zero;
    this.jumpRaindngBasePos = Vector3.zero;
    this.jumpRandingBaseBodyY = 0.0f;
    this.jumpState = Player.eJumpState.None;
    this.rainShotState = Player.RAIN_SHOT_STATE.NONE;
    this.rainShotFallPosition = Vector3.zero;
    this.rainShotFallRotateY = 0.0f;
    this.rainShotLotGroupId = 0;
    this.targetingGimmickObject = (IFieldGimmickObject) null;
    this.enabledTeleportAvoid = false;
    this.enabledRushAvoid = false;
    this.enabledOraclePairSwordsSP = false;
    this.actionMoveRotateMaxSpeedRate = 1f;
    this.extraSpGaugeDecreasingRate = 0.0f;
    this.ReleaseEffect(ref this.twoHandSwordsChargeMaxEffect);
    this.evolveSpecialActionSec = 0.0f;
    this.attackHitCount = 0;
    if (!this.notEndGuardFlag)
      this._EndGuard();
    this.ReleaseEffect(ref this.exRushChargeEffect);
    this.isAbsorbDamageSuperArmor = false;
    this.isInvincibleDamageSuperArmor = false;
    this.SetFlickDirection(SelfController.FLICK_DIRECTION.NONE);
    this.snatchCtrl.Cancel();
    int index = 0;
    for (int count = this.m_weaponCtrlList.Count; index < count; ++index)
      this.m_weaponCtrlList[index].OnEndAction();
    this.OnGatherGimmickEnd();
    this.fishingCtrl.CoopEnd();
    this.OnQuestGimmickEnd();
    if (this.cancelInvincible == null)
      return;
    this.StopCoroutine(this.cancelInvincible);
    this.cancelInvincible = (IEnumerator) null;
  }

  protected override void EndRotate()
  {
    this.enableRotateToTargetPoint = false;
    base.EndRotate();
  }

  public override RuntimeAnimatorController GetAnimCtrl(string ctrl_name)
  {
    if (ctrl_name == null)
      ctrl_name = "BASE";
    LoadObject loadObject = this.loader.animObjectTable.Get(ctrl_name);
    return loadObject == null ? (RuntimeAnimatorController) null : loadObject.loadedObjects[0].obj as RuntimeAnimatorController;
  }

  public override AnimEventData GetAnimEvent(string ctrl_name)
  {
    if (ctrl_name == null)
      ctrl_name = "BASE";
    LoadObject loadObject = this.loader.animObjectTable.Get(ctrl_name);
    return loadObject == null ? (AnimEventData) null : loadObject.loadedObjects[1].obj as AnimEventData;
  }

  protected override string GetMotionStateName(int motion_id, string _layerName = "")
  {
    if (motion_id - 115 < 0 || motion_id - 115 >= Player.subMotionStateName.Length)
      return base.GetMotionStateName(motion_id, _layerName);
    Character.stateNameBuilder.Length = 0;
    Character.stateNameBuilder.Append(_layerName == "" ? "Base Layer." : _layerName);
    Character.stateNameBuilder.Append(Player.subMotionStateName[motion_id - 115]);
    return Character.stateNameBuilder.ToString();
  }

  public virtual void Load(PlayerLoadInfo load_info, PlayerLoader.OnCompleteLoad callback = null)
  {
    if (Object.op_Inequality((Object) this._physics, (Object) null))
    {
      Object.Destroy((Object) ((Component) this._physics).gameObject);
      this._physics = (Transform) null;
    }
    int anim_id = load_info.weaponModelID / 1000;
    Player.ATTACK_MODE attack_mode = Player.ConvertEquipmentTypeToAttackMode((EQUIPMENT_TYPE) anim_id);
    if (attack_mode == Player.ATTACK_MODE.NONE)
      attack_mode = Player.ATTACK_MODE.ONE_HAND_SWORD;
    this.SetAttackMode(attack_mode);
    this.UpdateTwoHandSwordController();
    if (this.record != null)
    {
      this.record.playerLoadInfo = load_info;
      this.record.animID = anim_id;
    }
    this.loader.StartLoad_GG_Optimize(load_info, 8, anim_id, true, true, true, true, true, false, true, this.IsDiviedLoadAndInstantiate(), ShaderGlobal.GetCharacterShaderType(), callback);
  }

  private void UpdateTwoHandSwordController()
  {
    if (!this.IsBurstTwoHandSword())
      return;
    int num = 6;
    if (this.buffParam != null && this.buffParam.passive != null)
      num += this.buffParam.passive.additionalMaxBulletCnt;
    this.thsCtrl.InitAppend(new TwoHandSwordController.InitParam()
    {
      Owner = this,
      BurstInitParam = new TwoHandSwordBurstController.InitParam()
      {
        Owner = this,
        ActionInfo = this.playerParameter.twoHandSwordActionInfo,
        MaxBulletCount = num,
        CurrentRestBullets = this.thsCtrl.GetAllCurrentRestBulletCount
      }
    });
  }

  protected virtual void LoadUniqueEquipment(
    StageObjectManager.CreatePlayerInfo info,
    PlayerLoader.OnCompleteLoad callback = null)
  {
    this.spActionGauge = new float[3];
    this.spActionGaugeMax = new float[3];
    this.buffParam.AllBuffEnd(false);
    this.evolveCtrl.Init(this);
    this.weaponIndex = -1;
    this.SetState(info);
    this.SetNowWeapon(this.equipWeaponList[0], 0, info.extentionInfo.uniqueEquipmentIndex);
    this.autoReviveCount = 0;
    this.isUseInvincibleBuff = false;
    this.isUseInvincibleBadStatusBuff = false;
    PlayerLoadInfo load_info = new PlayerLoadInfo();
    load_info.SetEquipWeapon(info.charaInfo.sex, (uint) this.weaponData.eId);
    load_info.Apply(info.charaInfo, false, true, true, true);
    this.Load(load_info, callback);
    this.InitParameter();
    this.hp = this.hpMax;
  }

  public void LoadWeapon(
    CharaInfo.EquipItem item,
    int weapon_index,
    PlayerLoader.OnCompleteLoad callback = null)
  {
    if (item == null || Object.op_Equality((Object) this.loader, (Object) null) || this.loader.loadInfo == null)
      return;
    this.SetNowWeapon(item, weapon_index, this.uniqueEquipmentIndex);
    int sex = 0;
    if (this.createInfo != null && this.createInfo.charaInfo != null)
      sex = this.createInfo.charaInfo.sex;
    this.loader.loadInfo.SetEquipWeapon(sex, (uint) item.eId);
    this.Load(this.loader.loadInfo, callback);
    this.InitParameter();
  }

  public void SetPassiveBuff(BuffParam.BUFFTYPE type, int value)
  {
    BuffParam.PassiveBuff passive = this.buffParam.passive;
    switch (type)
    {
      case BuffParam.BUFFTYPE.ATTACK_NORMAL:
        passive.atkList[0] += value;
        break;
      case BuffParam.BUFFTYPE.ATTACK_FIRE:
        passive.atkList[1] += value;
        break;
      case BuffParam.BUFFTYPE.ATTACK_WATER:
        passive.atkList[2] += value;
        break;
      case BuffParam.BUFFTYPE.ATTACK_THUNDER:
        passive.atkList[3] += value;
        break;
      case BuffParam.BUFFTYPE.ATTACK_SOIL:
        passive.atkList[4] += value;
        break;
      case BuffParam.BUFFTYPE.ATTACK_LIGHT:
        passive.atkList[5] += value;
        break;
      case BuffParam.BUFFTYPE.ATTACK_DARK:
        passive.atkList[6] += value;
        break;
      case BuffParam.BUFFTYPE.ATTACK_ALLELEMENT:
        passive.atkAllElement += (float) value;
        break;
      case BuffParam.BUFFTYPE.ATTACK_PARALYZE:
        this.atkBadStatus.paralyze += (float) value;
        break;
      case BuffParam.BUFFTYPE.ATTACK_POISON:
        this.atkBadStatus.poison += (float) value;
        break;
      case BuffParam.BUFFTYPE.DEFENCE_NORMAL:
        passive.defList[0] += value;
        break;
      case BuffParam.BUFFTYPE.DEFENCE_FIRE:
        passive.defList[1] += value;
        break;
      case BuffParam.BUFFTYPE.DEFENCE_WATER:
        passive.defList[2] += value;
        break;
      case BuffParam.BUFFTYPE.DEFENCE_THUNDER:
        passive.defList[3] += value;
        break;
      case BuffParam.BUFFTYPE.DEFENCE_SOIL:
        passive.defList[4] += value;
        break;
      case BuffParam.BUFFTYPE.DEFENCE_LIGHT:
        passive.defList[5] += value;
        break;
      case BuffParam.BUFFTYPE.DEFENCE_DARK:
        passive.defList[6] += value;
        break;
      case BuffParam.BUFFTYPE.DEFENCE_ALLELEMENT:
        for (int index = 1; index < 7; ++index)
          passive.defList[index] += value;
        break;
      case BuffParam.BUFFTYPE.MOVE_SPEED_UP:
        passive.moveSpeedUp += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.MOVE_SPEED_DOWN:
        passive.moveSpeedUp -= (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.ATTACK_SPEED_UP:
        passive.attackSpeedUp += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.HP_HEAL_SPEEDUP:
        passive.hpHealSpeedUp += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.SKILL_ABSORBUP:
        passive.skillAbsorbUp += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.SKILL_HEAL_SPEEDUP:
        passive.skillHealSpeedUp += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.DAMAGE_DOWN:
        passive.damageDown += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.ATKUP_RATE_NORMAL:
        passive.atkUpRate.normal += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.ATKUP_RATE_FIRE:
        passive.atkUpRate.fire += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.ATKUP_RATE_WATER:
        passive.atkUpRate.water += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.ATKUP_RATE_THUNDER:
        passive.atkUpRate.thunder += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.ATKUP_RATE_SOIL:
        passive.atkUpRate.soil += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.ATKUP_RATE_LIGHT:
        passive.atkUpRate.light += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.ATKUP_RATE_DARK:
        passive.atkUpRate.dark += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.ATKUP_RATE_ALLELEMENT:
        passive.atkUpRate.AddElementOnly((float) value * 0.01f);
        break;
      case BuffParam.BUFFTYPE.DEFUP_RATE_NORMAL:
        passive.defUpRate.normal += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.DEFUP_RATE_FIRE:
        passive.defUpRate.fire += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.DEFUP_RATE_WATER:
        passive.defUpRate.water += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.DEFUP_RATE_THUNDER:
        passive.defUpRate.thunder += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.DEFUP_RATE_SOIL:
        passive.defUpRate.soil += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.DEFUP_RATE_LIGHT:
        passive.defUpRate.light += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.DEFUP_RATE_DARK:
        passive.defUpRate.dark += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.DEFUP_RATE_ALLELEMENT:
        passive.defUpRate.AddElementOnly((float) value * 0.01f);
        break;
      case BuffParam.BUFFTYPE.POISON_DAMAGE_DOWN:
        passive.poisonDamageDownRate += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.POISON_GUARD:
        passive.poisonGuardWeight += value;
        break;
      case BuffParam.BUFFTYPE.BURN_DAMAGE_DOWN:
        passive.burnDamageDownRate += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.BURN_GUARD:
        passive.burnGuardWeight += value;
        break;
      case BuffParam.BUFFTYPE.DEFDOWN_RATE_NORMAL:
        passive.defDownRate.normal += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.DEFDOWN_RATE_ALLELEMENT:
        passive.defDownRate.AddElementOnly((float) value * 0.01f);
        break;
      case BuffParam.BUFFTYPE.HP_UP:
        passive.hp += value;
        break;
      case BuffParam.BUFFTYPE.HP_DOWN:
        passive.hp -= value;
        break;
      case BuffParam.BUFFTYPE.HPUP_RATE:
        passive.hpUpRate += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.HPDOWN_RATE:
        passive.hpDownRate += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.ATTACK_DOWN_NORMAL:
        passive.atkList[0] -= value;
        break;
      case BuffParam.BUFFTYPE.ATTACK_DOWN_FIRE:
        passive.atkList[1] -= value;
        break;
      case BuffParam.BUFFTYPE.ATTACK_DOWN_WATER:
        passive.atkList[2] -= value;
        break;
      case BuffParam.BUFFTYPE.ATTACK_DOWN_THUNDER:
        passive.atkList[3] -= value;
        break;
      case BuffParam.BUFFTYPE.ATTACK_DOWN_SOIL:
        passive.atkList[4] -= value;
        break;
      case BuffParam.BUFFTYPE.ATTACK_DOWN_LIGHT:
        passive.atkList[5] -= value;
        break;
      case BuffParam.BUFFTYPE.ATTACK_DOWN_DARK:
        passive.atkList[6] -= value;
        break;
      case BuffParam.BUFFTYPE.ATTACK_DOWN_ALLELEMENT:
        passive.atkAllElement -= (float) value;
        break;
      case BuffParam.BUFFTYPE.ATKDOWN_RATE_NORMAL:
        passive.atkDownRate.normal += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.ATKDOWN_RATE_FIRE:
        passive.atkDownRate.fire += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.ATKDOWN_RATE_WATER:
        passive.atkDownRate.water += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.ATKDOWN_RATE_THUNDER:
        passive.atkDownRate.thunder += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.ATKDOWN_RATE_SOIL:
        passive.atkDownRate.soil += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.ATKDOWN_RATE_LIGHT:
        passive.atkDownRate.light += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.ATKDOWN_RATE_DARK:
        passive.atkDownRate.dark += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.ATKDOWN_RATE_ALLELEMENT:
        passive.atkDownRate.AddElementOnly((float) value * 0.01f);
        break;
      case BuffParam.BUFFTYPE.DEFENCE_DOWN_NORMAL:
        passive.defList[0] -= value;
        break;
      case BuffParam.BUFFTYPE.DEFENCE_DOWN_FIRE:
        passive.defList[1] -= value;
        break;
      case BuffParam.BUFFTYPE.DEFENCE_DOWN_WATER:
        passive.defList[2] -= value;
        break;
      case BuffParam.BUFFTYPE.DEFENCE_DOWN_THUNDER:
        passive.defList[3] -= value;
        break;
      case BuffParam.BUFFTYPE.DEFENCE_DOWN_SOIL:
        passive.defList[4] -= value;
        break;
      case BuffParam.BUFFTYPE.DEFENCE_DOWN_LIGHT:
        passive.defList[5] -= value;
        break;
      case BuffParam.BUFFTYPE.DEFENCE_DOWN_DARK:
        passive.defList[6] -= value;
        break;
      case BuffParam.BUFFTYPE.DEFENCE_DOWN_ALLELEMENT:
        for (int index = 1; index < 7; ++index)
          passive.defList[index] -= value;
        break;
      case BuffParam.BUFFTYPE.DEFDOWN_RATE_FIRE:
        passive.defDownRate.fire += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.DEFDOWN_RATE_WATER:
        passive.defDownRate.water += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.DEFDOWN_RATE_THUNDER:
        passive.defDownRate.thunder += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.DEFDOWN_RATE_SOIL:
        passive.defDownRate.soil += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.DEFDOWN_RATE_LIGHT:
        passive.defDownRate.light += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.DEFDOWN_RATE_DARK:
        passive.defDownRate.dark += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.TOLERANCE_FIRE:
        passive.tolList[0] += value;
        break;
      case BuffParam.BUFFTYPE.TOLERANCE_WATER:
        passive.tolList[1] += value;
        break;
      case BuffParam.BUFFTYPE.TOLERANCE_THUNDER:
        passive.tolList[2] += value;
        break;
      case BuffParam.BUFFTYPE.TOLERANCE_SOIL:
        passive.tolList[3] += value;
        break;
      case BuffParam.BUFFTYPE.TOLERANCE_LIGHT:
        passive.tolList[4] += value;
        break;
      case BuffParam.BUFFTYPE.TOLERANCE_DARK:
        passive.tolList[5] += value;
        break;
      case BuffParam.BUFFTYPE.TOLERANCE_ALLELEMENT:
        for (int index = 0; index < 6; ++index)
          passive.tolList[index] += value;
        break;
      case BuffParam.BUFFTYPE.TOLERANCE_DOWN_FIRE:
        passive.tolList[0] -= value;
        break;
      case BuffParam.BUFFTYPE.TOLERANCE_DOWN_WATER:
        passive.tolList[1] -= value;
        break;
      case BuffParam.BUFFTYPE.TOLERANCE_DOWN_THUNDER:
        passive.tolList[2] -= value;
        break;
      case BuffParam.BUFFTYPE.TOLERANCE_DOWN_SOIL:
        passive.tolList[3] -= value;
        break;
      case BuffParam.BUFFTYPE.TOLERANCE_DOWN_LIGHT:
        passive.tolList[4] -= value;
        break;
      case BuffParam.BUFFTYPE.TOLERANCE_DOWN_DARK:
        passive.tolList[5] -= value;
        break;
      case BuffParam.BUFFTYPE.TOLERANCE_DOWN_ALLELEMENT:
        for (int index = 0; index < 6; ++index)
          passive.tolList[index] -= value;
        break;
      case BuffParam.BUFFTYPE.TOLUP_RATE_FIRE:
        passive.tolUpRate.fire += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.TOLUP_RATE_WATER:
        passive.tolUpRate.water += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.TOLUP_RATE_THUNDER:
        passive.tolUpRate.thunder += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.TOLUP_RATE_SOIL:
        passive.tolUpRate.soil += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.TOLUP_RATE_LIGHT:
        passive.tolUpRate.light += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.TOLUP_RATE_DARK:
        passive.tolUpRate.dark += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.TOLUP_RATE_ALLELEMENT:
        passive.tolUpRate.AddElementOnly((float) value * 0.01f);
        break;
      case BuffParam.BUFFTYPE.TOLDOWN_RATE_FIRE:
        passive.tolDownRate.fire += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.TOLDOWN_RATE_WATER:
        passive.tolDownRate.water += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.TOLDOWN_RATE_THUNDER:
        passive.tolDownRate.thunder += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.TOLDOWN_RATE_SOIL:
        passive.tolDownRate.soil += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.TOLDOWN_RATE_LIGHT:
        passive.tolDownRate.light += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.TOLDOWN_RATE_DARK:
        passive.tolDownRate.dark += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.TOLDOWN_RATE_ALLELEMENT:
        passive.tolDownRate.AddElementOnly((float) value * 0.01f);
        break;
      case BuffParam.BUFFTYPE.JUSTGUARD_EXTEND_RATE:
        passive.justGuardExtendRate += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.PARALYZE_GUARD:
        passive.paralyzeGuardWeight += value;
        break;
      case BuffParam.BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_FIRE:
        passive.skillAbsorbUp_OnlyAttackAndElement.fire += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_WATER:
        passive.skillAbsorbUp_OnlyAttackAndElement.water += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_THUNDER:
        passive.skillAbsorbUp_OnlyAttackAndElement.thunder += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_SOIL:
        passive.skillAbsorbUp_OnlyAttackAndElement.soil += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_LIGHT:
        passive.skillAbsorbUp_OnlyAttackAndElement.light += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_DARK:
        passive.skillAbsorbUp_OnlyAttackAndElement.dark += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.SILENCE_GUARD:
        passive.silenceGuardWeight += value;
        break;
      case BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN:
        passive.attackSpeedUp -= (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.HEAT_GAUGE_INCREASE_UP:
        passive.heatGaugeIncreaseRate += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.HEAT_GAUGE_INCREASE_DOWN:
        passive.heatGaugeIncreaseRate -= (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.SOUL_GAUGE_INCREASE_UP:
        passive.soulGaugeIncreaseRate += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.SOUL_GAUGE_INCREASE_DOWN:
        passive.soulGaugeIncreaseRate -= (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.ATTACK_FREEZE:
        this.atkBadStatus.freeze += (float) value;
        break;
      case BuffParam.BUFFTYPE.BAD_STATUS_DOWN_RATE_UP:
        passive.badStatusRateUp[2] += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.DISTANCE_UP_IAI:
        passive.distanceRateIai += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.BOOST_DAMAGE_DOWN:
        passive.boostDamageDown += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.BOOST_ATTACK_SPEED_UP:
        passive.boostAttackSpeedUp += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.BOOST_MOVE_SPEED_UP:
        passive.boostMoveSpeedUp += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.BOOST_AVOID_UP:
        passive.boostAvoidUp += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.HEAL_UP:
        passive.healUP += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.BAD_STATUS_CONCUSSION_RATE_UP:
        passive.badStatusRateUp[4] += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.ATKUP_RATE_ALL:
        passive.atkUpRate.AddAll((float) value * 0.01f);
        break;
      case BuffParam.BUFFTYPE.BURST_GAUGE_INCREASE_UP:
        passive.burstGaugeIncreaseRate += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.BURST_GAUGE_INCREASE_DOWN:
        passive.burstGaugeIncreaseRate -= (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.ORACLE_GAUGE_INCREASE_UP:
        passive.oracleGaugeIncreaseRate += (float) value * 0.01f;
        break;
      case BuffParam.BUFFTYPE.ORACLE_GAUGE_INCREASE_DOWN:
        passive.oracleGaugeIncreaseRate -= (float) value * 0.01f;
        break;
    }
  }

  public virtual void OnSetPlayerStatus(
    int _level,
    int _atk,
    int _def,
    int _hp,
    bool send_packet = true,
    StageObjectManager.PlayerTransferInfo transfer_info = null,
    bool usingRealAtk = false)
  {
    this.playerAtk = (float) _atk;
    this.playerDef = (float) _def;
    this.playerHp = _hp;
    if (this.createInfo != null)
    {
      this.createInfo.charaInfo.level = (XorInt) _level;
      this.createInfo.charaInfo.atk = (XorInt) _atk;
      this.createInfo.charaInfo.def = (XorInt) _def;
      this.createInfo.charaInfo.hp = (XorInt) _hp;
    }
    this.InitParameter();
    if (this.isDead)
      this.hp = this.healHp = 0;
    else
      this.hp = this.healHp = this.hpMax;
    if (transfer_info != null && !this.isDead)
    {
      this.hp = transfer_info.hp;
      this.healHp = transfer_info.healHp;
    }
    if (!send_packet || !Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnSetPlayerStatus(_level, _atk, _def, _hp);
  }

  public void SetState(
    StageObjectManager.CreatePlayerInfo create_info,
    StageObjectManager.PlayerTransferInfo transfer_info = null)
  {
    if (create_info.charaInfo == null)
    {
      Log.Error("StageObjectManager.CreatePlayer() charaInfo is NULL");
    }
    else
    {
      this.createInfo = create_info;
      this.charaName = create_info.charaInfo.name;
      if (create_info.charaInfo.clanInfo != null)
      {
        string str = "FFFFFF";
        if (MonoBehaviourSingleton<GuildManager>.I.guildData != null && create_info.charaInfo.clanInfo.clanId == MonoBehaviourSingleton<GuildManager>.I.guildData.clanId)
          str = "08FF00";
        this.fullName = $"[{str}][{create_info.charaInfo.clanInfo.tag}][-]{this.charaName}";
      }
      else
        this.fullName = this.charaName;
      this.baseState.atkList.Clear();
      this.baseState.defList.Clear();
      this.skillConstState.atkList.Clear();
      this.skillConstState.defList.Clear();
      this.guardEquipDef.Clear();
      for (int index = 0; index < 7; ++index)
      {
        this.baseState.atkList.Add(0);
        this.baseState.defList.Add(0);
        this.skillConstState.atkList.Add(0);
        this.skillConstState.defList.Add(0);
        this.guardEquipDef.Add(0);
      }
      this.skillConstState.hp = 0;
      this.skillData.ids.Clear();
      this.skillData.lvs.Clear();
      this.skillData.exs.Clear();
      this.abilityData.ids.Clear();
      this.abilityData.APs.Clear();
      this.abilityItem.Clear();
      this.hpUp = 0;
      List<int> intList = (List<int>) null;
      int equipIndex = 0;
      if (create_info.extentionInfo != null)
      {
        intList = create_info.extentionInfo.weaponIndexList;
        equipIndex = create_info.extentionInfo.uniqueEquipmentIndex;
      }
      this.equipWeaponList = new List<CharaInfo.EquipItem>();
      this.weaponEquipItemDataList.Clear();
      for (int index = 0; index < 3; ++index)
      {
        this.equipWeaponList.Add((CharaInfo.EquipItem) null);
        this.weaponEquipItemDataList.Add((EquipItemTable.EquipItemData) null);
      }
      this.passiveSkillList.Clear();
      int index1 = 0;
      for (int count = create_info.charaInfo.equipSet.Count; index1 < count; ++index1)
      {
        CharaInfo.EquipItem equip = create_info.charaInfo.equipSet[index1];
        if (this.SetEqState(equip, false))
        {
          int num = -1;
          if (intList != null && intList.Count > 0)
            num = intList.IndexOf(index1);
          else if (this.equipWeaponList[0] == null)
            num = 0;
          if (num >= 0 && num < this.equipWeaponList.Count)
          {
            this.equipWeaponList[num] = equip;
            EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint) equip.eId);
            if (equipItemData != null)
            {
              this.weaponEquipItemDataList[num] = equipItemData;
              this.SetValueSpActionGaugeMax(equipItemData, num);
            }
          }
        }
      }
      int index2 = -1;
      CharaInfo.EquipItem now_weapon = (CharaInfo.EquipItem) null;
      if (transfer_info != null)
      {
        index2 = transfer_info.weaponIndex;
        now_weapon = transfer_info.weaponData;
      }
      else
      {
        for (int index3 = 0; index3 < 3; ++index3)
        {
          if (this.equipWeaponList[index3] != null)
          {
            index2 = index3;
            now_weapon = this.equipWeaponList[index3];
            break;
          }
        }
      }
      if (now_weapon != null)
        this.SetNowWeapon(now_weapon, index2, equipIndex);
      SkillInfo.SkillSettingsInfo skill_settings = new SkillInfo.SkillSettingsInfo();
      if (MonoBehaviourSingleton<InGameSettingsManager>.I.player.enableTestSkill)
      {
        int index4 = 0;
        for (int index5 = 9; index4 < index5; ++index4)
          skill_settings.elementList.Add(new SkillInfo.SkillSettingsInfo.Element()
          {
            baseInfo = {
              id = MonoBehaviourSingleton<InGameSettingsManager>.I.player.testSkillIDs[index4],
              level = 0
            }
          });
      }
      else
      {
        for (int index6 = 0; index6 < 3; ++index6)
        {
          CharaInfo.EquipItem equipWeapon = this.equipWeaponList[index6];
          EquipItemTable.EquipItemData equipItemData = (EquipItemTable.EquipItemData) null;
          if (equipWeapon != null)
            equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint) equipWeapon.eId);
          int num1 = 0;
          if (equipItemData != null)
          {
            int num2 = 0;
            int index7 = 0;
            for (int count = equipWeapon.sIds.Count; index7 < count; ++index7)
            {
              SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint) equipWeapon.sIds[index7]);
              if (equipItemData != null)
              {
                bool flag = false;
                switch (skillItemData.type)
                {
                  case SKILL_SLOT_TYPE.ATTACK:
                  case SKILL_SLOT_TYPE.SUPPORT:
                  case SKILL_SLOT_TYPE.HEAL:
                    flag = true;
                    break;
                }
                if (flag)
                {
                  SkillInfo.SkillSettingsInfo.Element element = new SkillInfo.SkillSettingsInfo.Element();
                  element.baseInfo.id = equipWeapon.sIds[index7];
                  element.baseInfo.level = equipWeapon.sLvs[index7];
                  int num3 = 0;
                  if (index7 < equipWeapon.sExs.Count)
                    num3 = equipWeapon.sExs[index7];
                  element.baseInfo.exceedCnt = num3;
                  if (transfer_info != null && index6 * 3 + num2 < transfer_info.useGaugeCounterList.Count)
                    element.useGaugeCounter = transfer_info.useGaugeCounterList[index6 * 3 + num2];
                  skill_settings.elementList.Add(element);
                  ++num1;
                  ++num2;
                  if (num1 >= 3)
                    break;
                }
              }
            }
          }
          for (int index8 = 0; index8 < 3 - num1; ++index8)
            skill_settings.elementList.Add((SkillInfo.SkillSettingsInfo.Element) null);
        }
      }
      this.skillInfo.SetSettingsInfo(skill_settings, this.equipWeaponList);
      if (transfer_info != null)
      {
        this.rescueCount = transfer_info.rescueCount;
        if (!this.EnableRescueCountup())
          this.rescueCount = this.GetInitRescueCount();
        this.autoReviveCount = transfer_info.autoReviveCount;
        this.isUseInvincibleBuff = transfer_info.isUseInvincibleBuff;
        this.isUseInvincibleBadStatusBuff = transfer_info.isUseInvincibleBadStatusBuff;
        this.isInitDead = transfer_info.isInitDead;
        this.initRescueTime = transfer_info.initRescueTime;
        this.initContinueTime = transfer_info.initContinueTime;
        this.initBuffSyncParam = transfer_info.buffSyncParam;
        this.abilityCounterAttackNumList = transfer_info.abilityCounterAttackNumList;
        this.abilityCleaveComboNumList = transfer_info.cleaveComboNumList;
        if (transfer_info.spActionGauges != null && transfer_info.spActionGauges.Length != 0)
          transfer_info.spActionGauges.CopyTo((Array) this.spActionGauge, 0);
        if (transfer_info.evolveGauges != null)
        {
          for (int index9 = 0; index9 < transfer_info.evolveGauges.Length; ++index9)
            this.evolveCtrl.SetGauge(transfer_info.evolveGauges[index9], index9);
        }
        if (this.thsCtrl != null && transfer_info.burstCurrentRestBulletCount != null)
          this.thsCtrl.InitAppend(new TwoHandSwordController.InitParam()
          {
            Owner = this,
            BurstInitParam = new TwoHandSwordBurstController.InitParam()
            {
              Owner = this,
              ActionInfo = this.playerParameter.twoHandSwordActionInfo,
              MaxBulletCount = transfer_info.maxBulletCount,
              CurrentRestBullets = transfer_info.burstCurrentRestBulletCount,
              IsNeedFullBullet = false
            }
          });
        if (transfer_info.shieldReflectInfo != null)
          this.shieldReflectInfo = transfer_info.shieldReflectInfo;
        if (this.spearCtrl == null)
          return;
        this.spearCtrl.stockedCounts = transfer_info.oracleSpearStockedCount;
      }
      else
      {
        if (this.thsCtrl != null)
          this.thsCtrl.InitAppend(new TwoHandSwordController.InitParam()
          {
            Owner = this,
            BurstInitParam = new TwoHandSwordBurstController.InitParam()
            {
              Owner = this,
              ActionInfo = this.playerParameter.twoHandSwordActionInfo,
              MaxBulletCount = 6,
              CurrentRestBullets = (int[]) null
            }
          });
        if (this.spearCtrl == null)
          return;
        this.spearCtrl.Init(this);
      }
    }
  }

  public StageObjectManager.PlayerTransferInfo CreateTransferInfo()
  {
    StageObjectManager.PlayerTransferInfo transferInfo = new StageObjectManager.PlayerTransferInfo();
    transferInfo.weaponIndex = this.weaponIndex;
    transferInfo.weaponData = this.weaponData;
    transferInfo.hp = this.hp;
    transferInfo.healHp = this.healHp;
    transferInfo.rescueCount = this.rescueCount;
    transferInfo.autoReviveCount = this.autoReviveCount;
    transferInfo.isUseInvincibleBuff = this.isUseInvincibleBuff;
    transferInfo.isUseInvincibleBadStatusBuff = this.isUseInvincibleBadStatusBuff;
    if (transferInfo.hp <= 0)
    {
      transferInfo.isInitDead = true;
      transferInfo.initRescueTime = this.rescueTime;
      transferInfo.initContinueTime = this.continueTime;
    }
    transferInfo.useGaugeCounterList = new List<float>();
    for (int skill_index = 0; skill_index < 9; ++skill_index)
    {
      SkillInfo.SkillParam skillParam = this.skillInfo.GetSkillParam(skill_index);
      if (skillParam != null && skillParam.isValid)
        transferInfo.useGaugeCounterList.Add(skillParam.useGaugeCounter);
      else
        transferInfo.useGaugeCounterList.Add(0.0f);
    }
    transferInfo.buffSyncParam = this.buffParam.CreateSyncParam();
    List<BuffParam.ConditionsAbility> conditionsAbilityList = this.buffParam.passive.conditionsAbilityList;
    if (conditionsAbilityList != null && conditionsAbilityList.Count > 0)
    {
      transferInfo.abilityCounterAttackNumList = new List<int>();
      transferInfo.cleaveComboNumList = new List<int>();
      foreach (BuffParam.ConditionsAbility conditionsAbility in conditionsAbilityList)
      {
        transferInfo.abilityCounterAttackNumList.Add(conditionsAbility.counterAttackNum);
        transferInfo.cleaveComboNumList.Add(conditionsAbility.cleaveComboNum);
      }
    }
    if (this.spActionGauge != null && this.spActionGauge.Length != 0)
    {
      transferInfo.spActionGauges = new float[3];
      for (int index = 0; index < this.spActionGauge.Length; ++index)
        transferInfo.spActionGauges[index] = this.spActionGauge[index];
    }
    transferInfo.evolveGauges = new float[3];
    for (int index = 0; index < 3; ++index)
      transferInfo.evolveGauges[index] = this.evolveCtrl.GetGauge(index);
    if (this.thsCtrl != null)
    {
      transferInfo.burstCurrentRestBulletCount = this.thsCtrl.GetAllCurrentRestBulletCount;
      transferInfo.maxBulletCount = this.thsCtrl.CurrentMaxBulletCount;
    }
    if (this.shieldReflectInfo != null)
      transferInfo.shieldReflectInfo = this.shieldReflectInfo;
    if (this.spearCtrl != null)
      transferInfo.oracleSpearStockedCount = this.spearCtrl.stockedCounts;
    return transferInfo;
  }

  private bool SetEqState(CharaInfo.EquipItem item, bool is_weapon_set)
  {
    if (item == null)
      return false;
    EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint) item.eId);
    if (equipItemData == null)
      return false;
    bool flag1 = equipItemData.IsWeapon();
    GrowEquipItemTable.GrowEquipItemData growEquipItemData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemData(equipItemData.growID, (uint) item.lv);
    List<int> intList1 = new List<int>();
    if (growEquipItemData != null)
    {
      intList1.Add(growEquipItemData.GetGrowParamAtk((int) equipItemData.baseAtk));
      int[] growParamElemAtk = growEquipItemData.GetGrowParamElemAtk(equipItemData.atkElement);
      for (int index = 0; index < 6; ++index)
        intList1.Add(growParamElemAtk[index]);
    }
    else
    {
      intList1.Add((int) equipItemData.baseAtk);
      for (int index = 0; index < 6; ++index)
        intList1.Add(equipItemData.atkElement[index]);
    }
    EquipItemExceedParamTable.EquipItemExceedParamAll exceedParam = equipItemData.GetExceedParam((uint) item.exceed);
    if (exceedParam != null)
    {
      intList1[0] += (int) exceedParam.atk;
      for (int index = 0; index < 6; ++index)
        intList1[index + 1] += exceedParam.atkElement[index];
    }
    if (flag1)
    {
      Player.ATTACK_MODE attackMode = Player.ConvertEquipmentTypeToAttackMode(equipItemData.type);
      if (attackMode != Player.ATTACK_MODE.NONE)
      {
        float num = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerWeaponAttackRate[(int) (attackMode - 1)];
        for (int index = 0; index < 7; ++index)
          intList1[index] = (int) ((double) intList1[index] / (double) num);
        this.spAttackType = equipItemData.spAttackType;
        this.extraAttackType = equipItemData.exAttackType;
      }
    }
    List<int> intList2 = new List<int>();
    if (growEquipItemData != null)
    {
      intList2.Add(growEquipItemData.GetGrowParamDef((int) equipItemData.baseDef));
      int[] growParamElemDef = growEquipItemData.GetGrowParamElemDef(equipItemData.defElement);
      for (int index = 0; index < 6; ++index)
      {
        int num = growParamElemDef[index];
        if (equipItemData.isFormer)
          num *= 10;
        intList2.Add(num);
      }
    }
    else
    {
      intList2.Add((int) equipItemData.baseDef);
      for (int index = 0; index < 6; ++index)
      {
        int num = equipItemData.defElement[index];
        if (equipItemData.isFormer)
          num *= 10;
        intList2.Add(num);
      }
    }
    int num1 = 0;
    if (exceedParam != null)
    {
      intList2[0] += (int) exceedParam.def;
      for (int index = 0; index < 6; ++index)
        intList2[index + 1] += exceedParam.defElement[index];
      num1 += (int) exceedParam.hp;
    }
    if (is_weapon_set & flag1)
    {
      this.weaponState.atkList = intList1;
      this.weaponState.defList = intList2;
      this.weaponState.hp = (growEquipItemData != null ? growEquipItemData.GetGrowParamHp((int) equipItemData.baseHp) : (int) equipItemData.baseHp) + num1;
    }
    else
    {
      if (!flag1)
      {
        for (int index = 0; index < 7; ++index)
        {
          this.baseState.atkList[index] += intList1[index];
          this.baseState.defList[index] += intList2[index];
          this.guardEquipDef[index] += intList2[index];
        }
        this.hpUp += (growEquipItemData != null ? growEquipItemData.GetGrowParamHp((int) equipItemData.baseHp) : (int) equipItemData.baseHp) + num1;
      }
      int num2 = 0;
      for (int count = item.sIds.Count; num2 < count; ++num2)
        this.SetSkillState(item.sIds[num2], item.sLvs[num2], item.GetSkillExceed(num2), is_weapon_set, equipItemData.type);
      this.a_ids.Clear();
      this.a_pts.Clear();
      this.a_ids.AddRange((IEnumerable<int>) item.aIds);
      this.a_pts.AddRange((IEnumerable<int>) item.aPts);
      for (int index = 0; index < equipItemData.fixedAbility.Length; ++index)
      {
        this.a_ids.Add(equipItemData.fixedAbility[index].id);
        this.a_pts.Add(equipItemData.fixedAbility[index].pt);
      }
      if (exceedParam != null)
      {
        for (int index = 0; index < exceedParam.ability.Length; ++index)
        {
          this.a_ids.Add(exceedParam.ability[index].id);
          this.a_pts.Add(exceedParam.ability[index].pt);
        }
      }
      int index1 = 0;
      for (int count1 = this.a_ids.Count; index1 < count1; ++index1)
      {
        bool flag2 = false;
        int index2 = 0;
        for (int count2 = this.abilityData.ids.Count; index2 < count2; ++index2)
        {
          if (this.abilityData.ids[index2] == this.a_ids[index1])
          {
            this.abilityData.APs[index2] += this.a_pts[index1];
            flag2 = true;
            break;
          }
        }
        if (!flag2)
        {
          this.abilityData.ids.Add(this.a_ids[index1]);
          this.abilityData.APs.Add(this.a_pts[index1]);
        }
      }
      if (item.ai != null && item.ai.abilityItemId != 0)
        this.abilityItem.Add(item.ai);
    }
    return flag1;
  }

  public bool IsValidBuffSilence() => this.IsValidBuff(BuffParam.BUFFTYPE.SILENCE);

  public bool IsActSkillAction(int skill_index)
  {
    int num = this.skillInfo.IsActSkillAction(skill_index) ? 1 : 0;
    bool flag1 = this.IsValidBuffSilence();
    bool flag2 = this.IsCarrying() || this.actionID == (Character.ACTION_ID) 44;
    return num != 0 && !flag1 && !flag2;
  }

  private void SetSkillState(
    int skill_id,
    int level,
    int exceedCnt,
    bool is_weapon_set,
    EQUIPMENT_TYPE type)
  {
    if (MonoBehaviourSingleton<InGameManager>.I.ContainsArenaCondition(ARENA_CONDITION.FORBID_MAGI_INCARNATION) && (skill_id == 404500100 || skill_id == 404500200 || skill_id == 404500300 || skill_id == 404500400))
      return;
    SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint) skill_id);
    GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(skillItemData.growID, level, exceedCnt);
    if (skillItemData.type == SKILL_SLOT_TYPE.PASSIVE)
      this.RegisterPassiveSkill(skillItemData);
    if (!is_weapon_set)
    {
      List<int> intList1 = new List<int>();
      intList1.Add(growSkillItemData.GetGrowParamAtk((int) skillItemData.baseAtk));
      int[] growParamElemAtk = growSkillItemData.GetGrowParamElemAtk(skillItemData.atkElement);
      for (int index = 0; index < 6; ++index)
        intList1.Add(growParamElemAtk[index]);
      List<int> intList2 = new List<int>();
      intList2.Add(growSkillItemData.GetGrowParamDef((int) skillItemData.baseDef));
      int[] growParamElemDef = growSkillItemData.GetGrowParamElemDef(skillItemData.defElement);
      for (int index = 0; index < 6; ++index)
        intList2.Add(growParamElemDef[index]);
      for (int index = 0; index < 7; ++index)
      {
        this.skillConstState.atkList[index] += intList1[index];
        this.skillConstState.defList[index] += intList2[index];
      }
      this.skillConstState.hp += growSkillItemData.GetGrowParamHp((int) skillItemData.baseHp);
    }
    if (!skillItemData.IsPassive())
      return;
    this.skillData.ids.Add(skill_id);
    this.skillData.lvs.Add(level);
    this.skillData.exs.Add(exceedCnt);
  }

  private void RegisterPassiveSkill(SkillItemTable.SkillItemData skillData)
  {
    for (int index = 0; index < 3; ++index)
    {
      BuffParam.BUFFTYPE bufftype = skillData.supportType[index];
      if (bufftype != BuffParam.BUFFTYPE.NONE && !this.passiveSkillList.Contains(bufftype))
        this.passiveSkillList.Add(bufftype);
    }
  }

  private bool IsValidPassiveSkill(BuffParam.BUFFTYPE targetType)
  {
    return this.passiveSkillList.Contains(targetType);
  }

  public void SetNowWeapon(CharaInfo.EquipItem now_weapon, int index, int equipIndex)
  {
    this.SetEqState(now_weapon, true);
    this.weaponData = now_weapon;
    this.weaponIndex = index;
    this.uniqueEquipmentIndex = equipIndex;
    this.evolveCtrl.SetWeaponInfo();
  }

  public void SetPassiveParam()
  {
    this.buffParam.passive.Reset();
    this.atkBadStatus.Reset();
    for (int index = 0; index < 7; ++index)
    {
      this.buffParam.passive.atkList[index] += this.skillConstState.atkList[index];
      this.buffParam.passive.defList[index] += this.skillConstState.defList[index];
    }
    this.buffParam.passive.hp += this.skillConstState.hp;
    if (this.attackMode == Player.ATTACK_MODE.NONE)
      return;
    int type_index = (int) (this.attackMode - 1);
    for (int index1 = 0; index1 < this.skillData.ids.Count; ++index1)
    {
      SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint) this.skillData.ids[index1]);
      if (skillItemData.IsEnableEquipType(type_index))
      {
        SP_ATTACK_TYPE passiveSpAttackType = skillItemData.supportPassiveSpAttackType;
        GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(skillItemData.growID, this.skillData.lvs[index1], this.skillData.exs[index1]);
        for (int index2 = 0; index2 < 3; ++index2)
        {
          if (skillItemData.IsEnableSupportEquipType(type_index, index2) && Utility.IsEnableSpAttackType(passiveSpAttackType, this.spAttackType))
            this.SetPassiveBuff(skillItemData.supportType[index2], growSkillItemData.GetGrowParamSupprtValue(skillItemData.supportValue, index2));
        }
      }
    }
    int currentEventId = Utility.GetCurrentEventID();
    EQUIPMENT_TYPE equipmentType = Player.ConvertAttackModeToEquipmentType(this.attackMode);
    int index3 = 0;
    for (int count = this.abilityData.ids.Count; index3 < count; ++index3)
      this.buffParam.AddAbility((uint) this.abilityData.ids[index3], this.abilityData.APs[index3], equipmentType, this.spAttackType, currentEventId);
    foreach (AbilityItem recv in this.abilityItem)
      this.buffParam.AddAbilityItemParam((AbilityDataTable.AbilityData.AbilityInfo[]) AbilityItemInfo.ConvertAbilityItemToInfo(recv).ToArray(), equipmentType, this.spAttackType, currentEventId);
    this.ApplyConditionAbilityValue();
    this.buffParam.passive.firstInitialized = true;
    this.buffParam.UpdateConditionsAbility();
  }

  private void ApplyConditionAbilityValue()
  {
    if (this.abilityCounterAttackNumList == null)
      return;
    List<BuffParam.ConditionsAbility> conditionsAbilityList = this.buffParam.passive.conditionsAbilityList;
    if (conditionsAbilityList != null && conditionsAbilityList.Count > 0)
    {
      for (int index = 0; index < conditionsAbilityList.Count; ++index)
        conditionsAbilityList[index].counterAttackNum = this.abilityCounterAttackNumList[index];
    }
    this.abilityCounterAttackNumList = (List<int>) null;
  }

  private void ApplyCleaveComboConditionAbilityValue()
  {
    if (this.abilityCleaveComboNumList == null)
      return;
    List<BuffParam.ConditionsAbility> conditionsAbilityList = this.buffParam.passive.conditionsAbilityList;
    if (conditionsAbilityList != null && conditionsAbilityList.Count > 0)
    {
      for (int index = 0; index < conditionsAbilityList.Count; ++index)
        conditionsAbilityList[index].cleaveComboNum = this.abilityCleaveComboNumList[index];
    }
    this.abilityCleaveComboNumList = (List<int>) null;
  }

  public virtual void InitParameter()
  {
    this.ResetStatusParam();
    this.attack.normal += (float) this.weaponState.atkList[0];
    this.attack.fire += (float) this.weaponState.atkList[1];
    this.attack.water += (float) this.weaponState.atkList[2];
    this.attack.thunder += (float) this.weaponState.atkList[3];
    this.attack.soil += (float) this.weaponState.atkList[4];
    this.attack.light += (float) this.weaponState.atkList[5];
    this.attack.dark += (float) this.weaponState.atkList[6];
    this.defense.normal += (float) this.baseState.defList[0] + this.playerDef + (float) this.weaponState.defList[0];
    this.defense.fire += (float) this.baseState.defList[0] + this.playerDef + (float) this.weaponState.defList[0];
    this.defense.water += (float) this.baseState.defList[0] + this.playerDef + (float) this.weaponState.defList[0];
    this.defense.thunder += (float) this.baseState.defList[0] + this.playerDef + (float) this.weaponState.defList[0];
    this.defense.soil += (float) this.baseState.defList[0] + this.playerDef + (float) this.weaponState.defList[0];
    this.defense.light += (float) this.baseState.defList[0] + this.playerDef + (float) this.weaponState.defList[0];
    this.defense.dark += (float) this.baseState.defList[0] + this.playerDef + (float) this.weaponState.defList[0];
    this.tolerance.normal += 0.0f;
    this.tolerance.fire += (float) this.baseState.defList[1] + (float) this.weaponState.defList[1];
    this.tolerance.water += (float) this.baseState.defList[2] + (float) this.weaponState.defList[2];
    this.tolerance.thunder += (float) this.baseState.defList[3] + (float) this.weaponState.defList[3];
    this.tolerance.soil += (float) this.baseState.defList[4] + (float) this.weaponState.defList[4];
    this.tolerance.light += (float) this.baseState.defList[5] + (float) this.weaponState.defList[5];
    this.tolerance.dark += (float) this.baseState.defList[6] + (float) this.weaponState.defList[6];
    this.hpMax = (int) ((double) (this.playerHp + this.hpUp + this.weaponState.hp) * (1.0 + (double) this.buffParam.GetPassiveHpRate())) + this.buffParam.passive.hp;
    if (this.hpMax < 1)
      this.hpMax = 1;
    if (this.hp > this.hpMax)
      this.hp = this.healHp = this.hpMax;
    AtkAttribute val = new AtkAttribute();
    val.Set(1f);
    val.Add(this.buffParam.passive.defUpRate);
    val.Sub(this.buffParam.passive.defDownRate);
    this.defense.Mul(val);
    this.defense.normal += (float) this.buffParam.passive.defList[0];
    this.defense.fire += (float) (this.buffParam.passive.defList[1] + this.buffParam.passive.defList[0]);
    this.defense.water += (float) (this.buffParam.passive.defList[2] + this.buffParam.passive.defList[0]);
    this.defense.thunder += (float) (this.buffParam.passive.defList[3] + this.buffParam.passive.defList[0]);
    this.defense.soil += (float) (this.buffParam.passive.defList[4] + this.buffParam.passive.defList[0]);
    this.defense.light += (float) (this.buffParam.passive.defList[5] + this.buffParam.passive.defList[0]);
    this.defense.dark += (float) (this.buffParam.passive.defList[6] + this.buffParam.passive.defList[0]);
    this.defense.CheckMinus();
    this.defenseCoefficient.normal = 1f;
    this.defenseCoefficient.fire = this.CalcDefenseElementCoefficient(this.defense.fire);
    this.defenseCoefficient.water = this.CalcDefenseElementCoefficient(this.defense.water);
    this.defenseCoefficient.thunder = this.CalcDefenseElementCoefficient(this.defense.thunder);
    this.defenseCoefficient.soil = this.CalcDefenseElementCoefficient(this.defense.soil);
    this.defenseCoefficient.light = this.CalcDefenseElementCoefficient(this.defense.light);
    this.defenseCoefficient.dark = this.CalcDefenseElementCoefficient(this.defense.dark);
    this.defenseThreshold = MonoBehaviourSingleton<InGameSettingsManager>.I.passive.playerDefenseThreshold;
  }

  private float CalcDefenseElementCoefficient(float defenseValue)
  {
    if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      return 4.5f;
    int defenseThreshold = MonoBehaviourSingleton<InGameSettingsManager>.I.passive.playerDefenseThreshold;
    int defenseCoefficient = MonoBehaviourSingleton<InGameSettingsManager>.I.passive.playerDefenseCoefficient;
    return defenseThreshold <= 0 || defenseCoefficient <= 0 || (double) defenseValue <= (double) defenseThreshold ? 4.5f : (float) (4.5 / (1.0 + ((double) defenseValue - (double) defenseThreshold) / (double) defenseCoefficient));
  }

  protected void SetAttackMode(Player.ATTACK_MODE attack_mode)
  {
    if (this.isInitialized && this.actionID != (Character.ACTION_ID) 27)
      this.ActIdle(false, -1f);
    this.attackMode = attack_mode;
    this.SetPassiveParam();
    this.weaponInfo = this.playerParameter.weaponInfo[(int) (attack_mode - 1)];
    if (this.weaponInfo == null)
      return;
    if ((double) this.weaponInfo.defenceRate != 1.0)
      this.defense.Mul(this.weaponInfo.defenceRate);
    this.attackWeakRate = this.weaponInfo.attackWeakRate;
    this.attackDownRate = this.weaponInfo.attackDownRate;
    this.elementWeakRate = this.weaponInfo.weakRateElementAttack;
    this.elementSkillWeakRate = this.weaponInfo.weakRateElementSkillAttack;
    this.skillWeakRate = this.weaponInfo.weakRateSkillAttack;
    this.healWeakRate = this.weaponInfo.weakRateHealAttack;
    this.elementSpAttackWeakRate = this.weaponInfo.weakRateElementSpAttack;
    this.downPowerWeak = this.weaponInfo.downPowerWeak;
    this.downPowerSimpleWeak = this.weaponInfo.downPowerSimpleWeak;
    this.attackInfos = Utility.DistinctArray<AttackInfo>(Utility.CreateMergedArray<AttackInfo>(Utility.CreateMergedArray<AttackInfo>(this.attackInfos, (AttackInfo[]) this.playerParameter.weaponAttackInfoList[(int) (attack_mode - 1)].attackHitInfos), (AttackInfo[]) this.playerParameter.weaponAttackInfoList[(int) (attack_mode - 1)].attackContinuationInfos));
  }

  public void AddAttackInfos(AttackInfo[] addInfos)
  {
    if (this.playerParameter == null)
      return;
    this.attackInfos = Utility.CreateMergedArray<AttackInfo>(this.attackInfos, addInfos);
  }

  public override bool OnBuffStart(BuffParam.BuffData buffData)
  {
    this.CreateWeaponLinkEffect("BUFF_LOOP_" + buffData.type.ToString());
    bool flag = base.OnBuffStart(buffData);
    int index = 0;
    for (int count = this.m_weaponCtrlList.Count; index < count; ++index)
      this.m_weaponCtrlList[index].OnBuffStart(buffData);
    return flag;
  }

  public override void OnBuffRoutine(BuffParam.BuffData buffData, bool packet = false)
  {
    base.OnBuffRoutine(buffData, packet);
    int type = (int) buffData.type;
    int num1 = buffData.value;
    switch (buffData.type)
    {
      case BuffParam.BUFFTYPE.REGENERATE:
      case BuffParam.BUFFTYPE.REGENERATE_PROPORTION:
        if (this.hp <= this.healHp)
          break;
        this.healHp = this.hp;
        break;
      case BuffParam.BUFFTYPE.BLEEDING:
        this.hp -= num1;
        if (this.hp <= 0)
        {
          this.hp = 0;
          this.healHp = 0;
          this.ActDead(true, false);
        }
        InGameSettingsManager.BleedingParam bleedingParam = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.bleedingParam;
        Transform effect = EffectManager.GetEffect(bleedingParam.effectName, this.FindNode(bleedingParam.effectNodeName));
        if (!Object.op_Inequality((Object) effect, (Object) null))
          break;
        effect.localPosition = bleedingParam.effectPosition;
        effect.localRotation = Quaternion.Euler(bleedingParam.effectRotation);
        float num2 = bleedingParam.effectScale;
        if ((double) num2 == 0.0)
          num2 = 1f;
        effect.localScale = Vector3.op_Multiply(Vector3.one, num2);
        break;
    }
  }

  public override void OnPoisonStart(int fromObjectID = 0)
  {
    float poisonTime = this.buffParam.GetPoisonTime();
    if ((double) poisonTime <= 0.0)
      return;
    this.OnBuffStart(new BuffParam.BuffData()
    {
      type = BuffParam.BUFFTYPE.POISON,
      time = poisonTime,
      value = (int) ((double) this.hpMax * 0.019999999552965164),
      valueType = BuffParam.VALUE_TYPE.RATE,
      interval = 2f
    });
  }

  public override void OnBurningStart()
  {
    float burningTime = this.buffParam.GetBurningTime();
    if ((double) burningTime <= 0.0)
      return;
    this.OnBuffStart(new BuffParam.BuffData()
    {
      type = BuffParam.BUFFTYPE.BURNING,
      time = burningTime,
      value = (int) ((double) this.hpMax * 0.029999999329447746),
      valueType = BuffParam.VALUE_TYPE.RATE,
      interval = 1f
    });
  }

  public override void OnBleedingStart()
  {
    float bleedTime = this.buffParam.GetBleedTime();
    if ((double) bleedTime <= 0.0)
      return;
    this.OnBuffStart(new BuffParam.BuffData()
    {
      type = BuffParam.BUFFTYPE.BLEEDING,
      time = bleedTime,
      value = (int) ((double) this.hpMax * (double) MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.bleedingParam.damageHpRate),
      valueType = BuffParam.VALUE_TYPE.RATE,
      interval = 0.0f
    });
  }

  public override void OnSpeedDown()
  {
    float speedDownTime = this.buffParam.GetSpeedDownTime();
    if ((double) speedDownTime <= 0.0)
      return;
    this.OnBuffStart(new BuffParam.BuffData()
    {
      type = BuffParam.BUFFTYPE.MOVE_SPEED_DOWN,
      time = speedDownTime,
      valueType = BuffParam.VALUE_TYPE.CONSTANT,
      value = 50
    });
  }

  public override void OnAttackSpeedDown()
  {
    float attackSpeedDownTime = this.buffParam.GetAttackSpeedDownTime();
    if ((double) attackSpeedDownTime <= 0.0)
      return;
    BuffParam.BuffData buffData = new BuffParam.BuffData();
    buffData.type = BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN;
    buffData.time = attackSpeedDownTime;
    buffData.valueType = BuffParam.VALUE_TYPE.CONSTANT;
    buffData.value = 50;
    if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      buffData.value = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.attackSpeedDownParam.value;
    this.OnBuffStart(buffData);
  }

  public override void OnDeadlyPoisonStart()
  {
    float deadlyPoisonTime = this.buffParam.GetDeadlyPoisonTime();
    if ((double) deadlyPoisonTime <= 0.0)
      return;
    float num1 = 2f;
    if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      num1 = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.deadlyPosion.interval;
    float num2 = 0.1f;
    if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      num2 = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.deadlyPosion.percent;
    this.OnBuffStart(new BuffParam.BuffData()
    {
      type = BuffParam.BUFFTYPE.DEADLY_POISON,
      time = deadlyPoisonTime,
      value = (int) ((double) this.hpMax * (double) num2),
      valueType = BuffParam.VALUE_TYPE.RATE,
      interval = num1
    });
  }

  public override void OnInkSplash(InkSplashInfo info)
  {
    if (info == null)
      return;
    this.OnBuffStart(new BuffParam.BuffData()
    {
      value = 100,
      type = BuffParam.BUFFTYPE.INK_SPLASH,
      time = info.duration,
      interval = Time.deltaTime,
      damage = 0
    });
  }

  public override void OnSlideStart()
  {
    this.OnBuffStart(new BuffParam.BuffData()
    {
      value = 100,
      type = BuffParam.BUFFTYPE.SLIDE,
      time = this.buffParam.GetSlideTime(),
      damage = 0
    });
  }

  public override void OnSilenceStart()
  {
    float silenceTime = this.buffParam.GetSilenceTime();
    if ((double) silenceTime <= 0.0)
      return;
    this.OnBuffStart(new BuffParam.BuffData()
    {
      value = 100,
      type = BuffParam.BUFFTYPE.SILENCE,
      time = silenceTime,
      damage = 0
    });
  }

  public override void OnCantHealHpStart()
  {
    float cantHealHpTime = this.buffParam.GetCantHealHpTime();
    if ((double) cantHealHpTime <= 0.0)
      return;
    this.OnBuffStart(new BuffParam.BuffData()
    {
      value = 100,
      type = BuffParam.BUFFTYPE.CANT_HEAL_HP,
      time = cantHealHpTime,
      damage = 0
    });
  }

  public override void OnBlindStart()
  {
    float blindTime = this.buffParam.GetBlindTime();
    if ((double) blindTime <= 0.0)
      return;
    this.OnBuffStart(new BuffParam.BuffData()
    {
      value = 100,
      type = BuffParam.BUFFTYPE.BLIND,
      time = blindTime,
      damage = 0
    });
  }

  public override void OnStoneStart()
  {
    this.OnBuffStart(new BuffParam.BuffData()
    {
      value = 100,
      type = BuffParam.BUFFTYPE.STONE,
      time = -1f,
      endless = new bool?(true),
      damage = 0
    });
  }

  public override void OnAcidStart()
  {
    float acidTime = this.buffParam.GetAcidTime();
    if ((double) acidTime <= 0.0)
      return;
    float num1 = 2f;
    if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      num1 = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.acidParam.interval;
    float num2 = 0.02f;
    if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      num2 = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.acidParam.percent;
    this.OnBuffStart(new BuffParam.BuffData()
    {
      type = BuffParam.BUFFTYPE.ACID,
      time = acidTime,
      value = (int) ((double) this.hpMax * (double) num2),
      valueType = BuffParam.VALUE_TYPE.RATE,
      interval = num1
    });
  }

  public override void OnBuffCancellation()
  {
    if (this.IsInBarrier() || this.IsValidBuff(BuffParam.BUFFTYPE.INVINCIBLE_BUFF_CANCELLATION_EXPAND))
      return;
    if (this.IsValidBuff(BuffParam.BUFFTYPE.INVINCIBLE_BUFF_CANCELLATION))
    {
      this.buffParam.DecreaseInvincibleBuffCancellation();
    }
    else
    {
      bool flag = false;
      List<BuffParam.BUFFTYPE> buffCancellation = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.ignoreBuffCancellation;
      int num = 0;
      for (int index = 221; num < index; ++num)
      {
        BuffParam.BUFFTYPE type = (BuffParam.BUFFTYPE) num;
        if (!buffCancellation.Contains(type) && this.OnBuffEnd(type, false, true))
          flag = true;
      }
      if (!flag)
        return;
      this.SendBuffSync();
    }
  }

  public override bool IsValidBuff(BuffParam.BUFFTYPE targetType)
  {
    return this.buffParam != null && this.buffParam.GetValue(targetType) > 0;
  }

  public bool CheckIgnoreBuff(BuffParam.BUFFTYPE targetType)
  {
    bool flag = false;
    if (targetType == BuffParam.BUFFTYPE.GHOST_FORM)
    {
      if (this.IsValidBuff(BuffParam.BUFFTYPE.BREAK_GHOST_FORM) || this.IsValidPassiveSkill(BuffParam.BUFFTYPE.BREAK_GHOST_FORM) || this.IsValidBuffByAbility(BuffParam.BUFFTYPE.BREAK_GHOST_FORM))
        flag = true;
    }
    else
      flag = true;
    return flag;
  }

  public override void OnHitAttack(
    AttackHitInfo info,
    AttackHitColliderProcessor.HitParam hit_param)
  {
    if (hit_param.toObject is Enemy)
    {
      Enemy toObject = hit_param.toObject as Enemy;
      if (toObject.isSummonAttack || toObject.regionInfos.Length > hit_param.regionID && !toObject.regionInfos[hit_param.regionID].isAtkColliderHit)
        return;
    }
    switch (info.attackType)
    {
      case AttackHitInfo.ATTACK_TYPE.TWO_HAND_SWORD_SP:
        this.isHitSpAttack = true;
        break;
      case AttackHitInfo.ATTACK_TYPE.HEAL_ATTACK:
        Enemy toObject1 = hit_param.toObject as Enemy;
        if (!Object.op_Equality((Object) toObject1, (Object) null))
        {
          this.healAtkRate = info.atkRate * toObject1.healDamageRate;
          EnemyRegionWork[] regionWorks = toObject1.regionWorks;
          for (int index = 0; index < regionWorks.Length; ++index)
          {
            if (regionWorks[index] != null && Enemy.IsWeakStateHealAttack(regionWorks[index].weakState))
            {
              hit_param.regionID = index;
              break;
            }
          }
          break;
        }
        break;
      case AttackHitInfo.ATTACK_TYPE.SPEAR_SP:
        switch (this.spAttackType)
        {
          case SP_ATTACK_TYPE.NONE:
            this.hitSpearSpecialAction = true;
            if ((double) this.playerParameter.spearActionInfo.rushCancellableTime > 0.0)
            {
              this.enableCancelToAttack = true;
              break;
            }
            break;
          case SP_ATTACK_TYPE.SOUL:
            if (hit_param.toObject is Enemy)
            {
              this.inputComboFlag = true;
              this.inputComboID = this.playerParameter.spearActionInfo.Soul_SpAttackContinueId;
              this.spearCtrl.ContinueBladeEffect();
              this.spearCtrl.MakeInvincible();
              break;
            }
            break;
        }
        break;
      case AttackHitInfo.ATTACK_TYPE.FROM_AVOID:
        if (this.thsCtrl != null)
        {
          this.thsCtrl.SetIsHitAvoidAttack(true);
          break;
        }
        break;
      case AttackHitInfo.ATTACK_TYPE.SNATCH:
        Enemy toObject2 = hit_param.toObject as Enemy;
        if (!Object.op_Equality((Object) toObject2, (Object) null))
        {
          this.moveStopRange = this.playerParameter.ohsActionInfo.Soul_MoveStopRange;
          if (this.IsCoopNone() || this.IsOriginal())
            this.snatchCtrl.OnHit(toObject2.id, hit_param.point);
          if (hit_param.toObject.hitOffFlag != StageObject.HIT_OFF_FLAG.NONE)
            return;
          break;
        }
        break;
      case AttackHitInfo.ATTACK_TYPE.BURST_THS_COMBO03:
        if (this.thsCtrl != null)
        {
          this.thsCtrl.Set3rdComboAttackHitFlag(true);
          break;
        }
        break;
      case AttackHitInfo.ATTACK_TYPE.BURST_SPEAR_COMBO3:
        if (this.spAttackType == SP_ATTACK_TYPE.BURST && hit_param.toObject is Enemy)
        {
          this.spearCtrl.EnableHitFlag();
          this.enableCancelToAttack = true;
          break;
        }
        break;
      case AttackHitInfo.ATTACK_TYPE.THS_ORACLE_HORIZONTAL:
        this.thsCtrl.oracleCtrl.HitHorizontal();
        break;
    }
    if (hit_param.toObject is Enemy)
    {
      this.spearCtrl.SacrificeHPBySoulAttackHit();
      this.spearCtrl.HealHPBySoulSpAttackHit(info.spAttackType);
    }
    base.OnHitAttack(info, hit_param);
  }

  protected override bool IsValidAttackedHit(StageObject from_object)
  {
    return !(from_object is Player) && base.IsValidAttackedHit(from_object);
  }

  protected override void OnAttackFromHitDirection(
    AttackedHitStatusDirection status,
    StageObject to_object)
  {
    base.OnAttackFromHitDirection(status, to_object);
    if (!(to_object is Enemy))
      return;
    Enemy enemy = to_object as Enemy;
    this.SetHitStop(status.attackInfo.toEnemy.hitStopTime);
    double enemyHitStopTime = (double) status.attackInfo.toEnemy.enemyHitStopTime;
    enemy.SetHitStop((float) enemyHitStopTime);
    if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.BURST) && status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.JUSTGUARD_ATTACK)
      this.isSuccessParry = true;
    if (this.IsCoopNone() || this.IsOriginal())
    {
      if (this.isActOneHandSwordCounter)
        this.skillInfo.OnHitCounterEnemy(status.attackInfo);
      else if (!this.isActSkillAction)
        this.skillInfo.OnHitAttackEnemy(status.attackInfo);
    }
    if (!this.isActSkillAction || !this.isAbleToSkipSkillAction)
      return;
    this.SetNextTrigger();
  }

  protected override void OnPlayAttackedHitEffect(AttackedHitStatusDirection status)
  {
    if (!this.IsValidAttackedHit(status.fromObject))
      return;
    bool is_priority = this is Self;
    bool flag = false;
    if (status.attackInfo.hitSEID != 0)
    {
      if (this.EnablePlaySound())
        SoundManager.PlayOneShotSE(status.attackInfo.hitSEID, status.hitPos);
      flag = true;
    }
    if (MonoBehaviourSingleton<InGameManager>.I.graphicOptionType <= 0 || !is_priority && MonoBehaviourSingleton<InGameManager>.I.graphicOptionType <= 1)
      return;
    if (!string.IsNullOrEmpty(status.attackInfo.hitEffectName))
    {
      EffectManager.OneShot(status.attackInfo.hitEffectName, status.hitPos, status.hitParam.rot, is_priority);
    }
    else
    {
      if (flag && !status.attackInfo.playCommonHitEffect)
        return;
      EffectManager.OneShot("ef_btl_pl_damage_01", status.hitPos, status.hitParam.rot, is_priority);
    }
  }

  protected override bool IsDamageValid(AttackedHitStatusDirection status)
  {
    return status.fromType == StageObject.OBJECT_TYPE.ENEMY && !this.IsInBarrier();
  }

  public override void AbsorptionProc(Character targetChar, AttackedHitStatusLocal status)
  {
    AttackHitInfo attackInfo = status.attackInfo;
    if (attackInfo == null || (double) attackInfo.absorptance <= 0.0)
      return;
    float num = attackInfo.absorptance * 0.01f;
    int healHp = (int) ((double) status.damage * (double) num);
    if (healHp < 1)
      healHp = 1;
    this.OnHealReceive(new Character.HealData(healHp, HEAL_TYPE.NONE, HEAL_EFFECT_TYPE.ABSORB, new List<int>()
    {
      10
    }));
  }

  public override void AbsorptionProcByBuff(AttackedHitStatusLocal status)
  {
    if (status.attackInfo.isSkillReference)
      return;
    List<BuffParam.BuffData> absorbBuffDataList = this.buffParam.GetHitAbsorbBuffDataList();
    if (absorbBuffDataList.IsNullOrEmpty<BuffParam.BuffData>() || this.IsValidBuff(BuffParam.BUFFTYPE.SHIELD))
      return;
    float num = 0.0f;
    for (int index = 0; index < absorbBuffDataList.Count; ++index)
    {
      switch (absorbBuffDataList[index].type)
      {
        case BuffParam.BUFFTYPE.HIT_ABSORB_NORMAL:
          num += this.CalcAbsorbValueByBuff(status.damageDetails.normal, absorbBuffDataList[index]);
          break;
        case BuffParam.BUFFTYPE.HIT_ABSORB_FIRE:
          num += this.CalcAbsorbValueByBuff(status.damageDetails.fire, absorbBuffDataList[index]);
          break;
        case BuffParam.BUFFTYPE.HIT_ABSORB_WATER:
          num += this.CalcAbsorbValueByBuff(status.damageDetails.water, absorbBuffDataList[index]);
          break;
        case BuffParam.BUFFTYPE.HIT_ABSORB_THUNDER:
          num += this.CalcAbsorbValueByBuff(status.damageDetails.thunder, absorbBuffDataList[index]);
          break;
        case BuffParam.BUFFTYPE.HIT_ABSORB_SOIL:
          num += this.CalcAbsorbValueByBuff(status.damageDetails.soil, absorbBuffDataList[index]);
          break;
        case BuffParam.BUFFTYPE.HIT_ABSORB_LIGHT:
          num += this.CalcAbsorbValueByBuff(status.damageDetails.light, absorbBuffDataList[index]);
          break;
        case BuffParam.BUFFTYPE.HIT_ABSORB_DARK:
          num += this.CalcAbsorbValueByBuff(status.damageDetails.dark, absorbBuffDataList[index]);
          break;
        case BuffParam.BUFFTYPE.HIT_ABSORB_ALL:
          num += this.CalcAbsorbValueByBuff((float) status.damage, absorbBuffDataList[index]);
          break;
      }
    }
    int limitPlayerHitAbsorb = Mathf.FloorToInt(num);
    if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid() && limitPlayerHitAbsorb > MonoBehaviourSingleton<InGameSettingsManager>.I.buff.absorbDamageParam.limitPlayerHitAbsorb)
      limitPlayerHitAbsorb = MonoBehaviourSingleton<InGameSettingsManager>.I.buff.absorbDamageParam.limitPlayerHitAbsorb;
    if (limitPlayerHitAbsorb <= 0)
      return;
    this.OnHealReceive(new Character.HealData(limitPlayerHitAbsorb, HEAL_TYPE.NONE, HEAL_EFFECT_TYPE.HIT_ABSORB, new List<int>()
    {
      10
    }));
  }

  public override bool CutAndAbsorbDamageByBuff(
    Character targetCharacter,
    AttackedHitStatusLocal status)
  {
    List<BuffParam.BuffData> absorbBuffDataList = this.buffParam.GetAbsorbBuffDataList();
    if (absorbBuffDataList.IsNullOrEmpty<BuffParam.BuffData>() || this.IsValidBuff(BuffParam.BUFFTYPE.SHIELD))
      return false;
    AtkAttribute absorbAtkAttribute = this.GetAbsorbAtkAttribute(status.damageDetails, absorbBuffDataList);
    status.damageDetails.Sub(absorbAtkAttribute);
    status.damage = Mathf.FloorToInt(status.damageDetails.CalcTotal());
    if (status.damage < 0)
      status.damage = 0;
    if ((double) absorbAtkAttribute.CalcTotal() > 0.0)
      this.isAbsorbDamageSuperArmor = true;
    int playerAbsorbDamage = Mathf.FloorToInt(absorbAtkAttribute.CalcTotal());
    if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid() && playerAbsorbDamage > MonoBehaviourSingleton<InGameSettingsManager>.I.buff.absorbDamageParam.limitPlayerAbsorbDamage)
      playerAbsorbDamage = MonoBehaviourSingleton<InGameSettingsManager>.I.buff.absorbDamageParam.limitPlayerAbsorbDamage;
    if (playerAbsorbDamage <= 0)
      return false;
    this.OnHealReceive(new Character.HealData(playerAbsorbDamage, HEAL_TYPE.NONE, HEAL_EFFECT_TYPE.ABSORB, new List<int>()
    {
      10
    }));
    return true;
  }

  private AtkAttribute GetAbsorbAtkAttribute(
    AtkAttribute damageDetails,
    List<BuffParam.BuffData> absorbBuffDataList = null)
  {
    AtkAttribute absorbAtkAttribute = new AtkAttribute();
    if (absorbBuffDataList == null)
    {
      absorbBuffDataList = this.buffParam.GetAbsorbBuffDataList();
      if (absorbBuffDataList.IsNullOrEmpty<BuffParam.BuffData>())
        return absorbAtkAttribute;
    }
    for (int index = 0; index < absorbBuffDataList.Count; ++index)
    {
      BuffParam.BuffData absorbBuffData = absorbBuffDataList[index];
      switch (absorbBuffData.type)
      {
        case BuffParam.BUFFTYPE.ABSORB_NORMAL:
          absorbAtkAttribute.normal = this.CalcAbsorbValueByBuff(damageDetails.normal, absorbBuffData);
          break;
        case BuffParam.BUFFTYPE.ABSORB_FIRE:
          absorbAtkAttribute.fire = this.CalcAbsorbValueByBuff(damageDetails.fire, absorbBuffData);
          break;
        case BuffParam.BUFFTYPE.ABSORB_WATER:
          absorbAtkAttribute.water = this.CalcAbsorbValueByBuff(damageDetails.water, absorbBuffData);
          break;
        case BuffParam.BUFFTYPE.ABSORB_THUNDER:
          absorbAtkAttribute.thunder = this.CalcAbsorbValueByBuff(damageDetails.thunder, absorbBuffData);
          break;
        case BuffParam.BUFFTYPE.ABSORB_SOIL:
          absorbAtkAttribute.soil = this.CalcAbsorbValueByBuff(damageDetails.soil, absorbBuffData);
          break;
        case BuffParam.BUFFTYPE.ABSORB_LIGHT:
          absorbAtkAttribute.light = this.CalcAbsorbValueByBuff(damageDetails.light, absorbBuffData);
          break;
        case BuffParam.BUFFTYPE.ABSORB_DARK:
          absorbAtkAttribute.dark = this.CalcAbsorbValueByBuff(damageDetails.dark, absorbBuffData);
          break;
      }
    }
    return absorbAtkAttribute;
  }

  private float CalcAbsorbValueByBuff(float damage, BuffParam.BuffData absorbBuff)
  {
    float num = 0.0f;
    switch (absorbBuff.valueType)
    {
      case BuffParam.VALUE_TYPE.RATE:
        num = (float) ((double) damage * (double) absorbBuff.value * 0.01);
        break;
      case BuffParam.VALUE_TYPE.CONSTANT:
        num = (double) damage < (double) absorbBuff.value ? damage : (float) absorbBuff.value;
        break;
    }
    return num;
  }

  public override bool ChargeSkillWhenDamagedByBuff()
  {
    if (!this.IsValidBuff(BuffParam.BUFFTYPE.SKILL_CHARGE_WHEN_DAMAGED) || this.IsValidBuff(BuffParam.BUFFTYPE.SHIELD))
      return false;
    int buffValue = this.buffParam.GetValue(BuffParam.BUFFTYPE.SKILL_CHARGE_WHEN_DAMAGED);
    if (buffValue <= 0)
      return false;
    this.OnChargeSkillGaugeReceive(BuffParam.BUFFTYPE.SKILL_CHARGE_WHEN_DAMAGED, buffValue, -1);
    return true;
  }

  public override bool InvincibleDamageByBuff(
    Character targetCharacter,
    AttackedHitStatusLocal status)
  {
    if (this.buffParam.GetInvincibleBuffDataList().IsNullOrEmpty<BuffParam.BuffData>())
      return false;
    AtkAttribute invinsibleMulRate = this.GetInvinsibleMulRate();
    status.damageDetails.Mul(invinsibleMulRate);
    int num = Mathf.FloorToInt(status.damageDetails.CalcTotal());
    if (status.damage < 0)
      num = 0;
    status.damage = num;
    if (status.damage == 0)
    {
      this.isInvincibleDamageSuperArmor = true;
      this.shouldShowInvincibleDamage = true;
    }
    return true;
  }

  public override void OnAttackedHit(
    AttackHitInfo info,
    AttackHitColliderProcessor.HitParam hit_param)
  {
    this.disableCounterAnimEvent = this.disableParryAction = info.toPlayer.disableCounter;
    this.disableGuard = info.toPlayer.disableGuard;
    base.OnAttackedHit(info, hit_param);
    this.disableParryAction = false;
    this.disableGuard = false;
  }

  protected override int CalcDamage(AttackedHitStatusLocal status, ref AtkAttribute damage_details)
  {
    AtkAttribute atkAttribute1 = this.CalcAtk(status);
    AtkAttribute atkAttribute2 = this.CalcTolerance(status);
    AtkAttribute atkAttribute3 = this.CalcDefense(status);
    if (this.isAnimEventStatusUpDefence)
    {
      atkAttribute3.normal *= this.animEventStatusUpDefenceRate;
      atkAttribute3.AddElementOnly((float) (((double) this.playerDef + (double) this.baseState.defList[0] + (double) this.weaponState.defList[0]) * ((double) this.animEventStatusUpDefenceRate - 1.0)));
    }
    int enemyLevel = 1;
    Enemy fromObject1 = status.fromObject as Enemy;
    if (Object.op_Inequality((Object) fromObject1, (Object) null))
      enemyLevel = (int) fromObject1.enemyLevel;
    float levelRate = InGameUtility.CalcLevelRate(enemyLevel);
    damage_details.normal = (float) (int) ((double) atkAttribute1.normal * (double) InGameUtility.CalcDamageRateToPlayer(levelRate, atkAttribute3.normal, 0.0f));
    damage_details.fire = (float) (int) ((double) atkAttribute1.fire * (double) InGameUtility.CalcDamageRateToPlayer(levelRate, atkAttribute3.fire, atkAttribute2.fire, this.defenseThreshold, this.defenseCoefficient.fire));
    damage_details.water = (float) (int) ((double) atkAttribute1.water * (double) InGameUtility.CalcDamageRateToPlayer(levelRate, atkAttribute3.water, atkAttribute2.water, this.defenseThreshold, this.defenseCoefficient.water));
    damage_details.thunder = (float) (int) ((double) atkAttribute1.thunder * (double) InGameUtility.CalcDamageRateToPlayer(levelRate, atkAttribute3.thunder, atkAttribute2.thunder, this.defenseThreshold, this.defenseCoefficient.thunder));
    damage_details.soil = (float) (int) ((double) atkAttribute1.soil * (double) InGameUtility.CalcDamageRateToPlayer(levelRate, atkAttribute3.soil, atkAttribute2.soil, this.defenseThreshold, this.defenseCoefficient.soil));
    damage_details.light = (float) (int) ((double) atkAttribute1.light * (double) InGameUtility.CalcDamageRateToPlayer(levelRate, atkAttribute3.light, atkAttribute2.light, this.defenseThreshold, this.defenseCoefficient.light));
    damage_details.dark = (float) (int) ((double) atkAttribute1.dark * (double) InGameUtility.CalcDamageRateToPlayer(levelRate, atkAttribute3.dark, atkAttribute2.dark, this.defenseThreshold, this.defenseCoefficient.dark));
    damage_details.CheckMinus();
    Character fromObject2 = status.fromObject as Character;
    if (Object.op_Inequality((Object) fromObject2, (Object) null))
      damage_details.Mul(fromObject2.buffParam.GetAbilityDamageRate((Character) this, status));
    int damage = (int) damage_details.CalcTotal();
    if (this._IsGuard() || this.spearCtrl.IsGuard())
    {
      int num1 = (double) this.GetAbsorbAtkAttribute(damage_details).CalcTotal() > 0.0 ? 1 : 0;
      bool flag = false;
      if (num1 == 0 || this.IsValidShield())
      {
        AtkAttribute invinsibleMulRate = this.GetInvinsibleMulRate();
        AtkAttribute atkAttribute4 = new AtkAttribute();
        atkAttribute4.Copy(damage_details);
        atkAttribute4.Mul(invinsibleMulRate);
        flag = (int) atkAttribute4.CalcTotal() == 0;
      }
      if (!flag)
      {
        float guardDamageCutRate = this._GetGuardDamageCutRate();
        int num2 = Mathf.CeilToInt((float) damage * (1f - guardDamageCutRate));
        damage_details.Mul(guardDamageCutRate);
        damage = (int) ((double) damage * (double) guardDamageCutRate);
        if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE))
          this._AddRevengeGauge(Mathf.CeilToInt((float) num2 * this.playerParameter.spearActionInfo.oracle.damageConvertToSpRate));
        else
          this._AddRevengeGauge(damage);
      }
    }
    int num = (int) ((double) (int) ((double) damage * (double) this.buffParam.GetDamageDownRate()) * (double) this.actionReceiveDamageRate);
    if (this.IsInSpearBurstBarrier())
      num = Mathf.FloorToInt((float) num * this.playerParameter.spearActionInfo.burstSpearInfo.inBarrierDamageRate);
    if (num < 1)
      num = 1;
    return num;
  }

  public override void GetAtk(
    AttackHitInfo info,
    ref AtkAttribute atk,
    SkillInfo.SkillParam skillParamInfo = null)
  {
    SkillInfo.SkillParam skillParam = skillParamInfo;
    if (skillParam == null)
    {
      skillParam = this.skillInfo.actSkillParam;
      if (Object.op_Inequality((Object) this.TrackingTargetBullet, (Object) null) && this.TrackingTargetBullet.IsReplaceSkill)
        skillParam = this.TrackingTargetBullet.SkillParamForBullet;
    }
    InGameUtility.PlayerAtkCalcData calcData = new InGameUtility.PlayerAtkCalcData();
    calcData.skillParam = skillParam;
    calcData.atkInfo = info;
    calcData.weaponAtk = this.attack;
    calcData.statusAtk = this.playerAtk;
    calcData.guardEquipAtk = this.GetGuardEquipmentAtk();
    calcData.buffAtkRate = this.buffParam.GetBuffAtkRate();
    calcData.passiveAtkRate = this.buffParam.GetPassiveAtkRate();
    calcData.buffAtkConstant = this.buffParam.GetBuffAtkConstant();
    calcData.buffAtkAllElementConstant = (float) this.buffParam.GetValue(BuffParam.BUFFTYPE.ATTACK_ALLELEMENT);
    calcData.passiveAtkConstant = this.buffParam.GetPassiveAtkUpConstant();
    calcData.passiveAtkAllElementConstant = this.buffParam.passive.atkAllElement;
    if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL) || this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.SOUL))
      calcData.isAtkElementOnly = true;
    atk.Copy(InGameUtility.CalcPlayerATK(calcData));
  }

  public AtkAttribute GetGuardEquipmentAtk()
  {
    AtkAttribute guardEquipmentAtk = new AtkAttribute();
    guardEquipmentAtk.Set(0.0f);
    guardEquipmentAtk.normal += (float) this.baseState.atkList[0];
    guardEquipmentAtk.fire += (float) this.baseState.atkList[1];
    guardEquipmentAtk.water += (float) this.baseState.atkList[2];
    guardEquipmentAtk.thunder += (float) this.baseState.atkList[3];
    guardEquipmentAtk.soil += (float) this.baseState.atkList[4];
    guardEquipmentAtk.light += (float) this.baseState.atkList[5];
    guardEquipmentAtk.dark += (float) this.baseState.atkList[6];
    return guardEquipmentAtk;
  }

  protected override AtkAttribute CalcDefense(AttackedHitStatusLocal status)
  {
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

  public float GetDefForTwoHandSwordSpAttack()
  {
    EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint) this.weaponData.eId);
    if (equipItemData == null || equipItemData.type != EQUIPMENT_TYPE.TWO_HAND_SWORD)
      return 0.0f;
    AtkAttribute baseDefence = new AtkAttribute();
    baseDefence.normal = (float) (this.guardEquipDef[0] + this.weaponState.defList[0]) / MonoBehaviourSingleton<GlobalSettingsManager>.I.playerWeaponAttackRate[1];
    baseDefence.normal += this.playerDef;
    this.CalcDefenceBuffAndPassive(ref baseDefence, true);
    return baseDefence.normal * 1.5f;
  }

  public float GetElementDefForTwoHandSwordHeatCombo()
  {
    EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint) this.weaponData.eId);
    if (equipItemData == null || equipItemData.type != EQUIPMENT_TYPE.TWO_HAND_SWORD)
      return 0.0f;
    AtkAttribute baseDefence = new AtkAttribute();
    baseDefence.normal = 0.0f;
    baseDefence.fire = (float) (this.guardEquipDef[1] + this.weaponState.defList[1]);
    baseDefence.water = (float) (this.guardEquipDef[2] + this.weaponState.defList[2]);
    baseDefence.thunder = (float) (this.guardEquipDef[3] + this.weaponState.defList[3]);
    baseDefence.soil = (float) (this.guardEquipDef[4] + this.weaponState.defList[4]);
    baseDefence.light = (float) (this.guardEquipDef[5] + this.weaponState.defList[5]);
    baseDefence.dark = (float) (this.guardEquipDef[6] + this.weaponState.defList[6]);
    baseDefence.Div(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerWeaponAttackRate[1]);
    this.CalcDefenceBuffAndPassive(ref baseDefence, false);
    baseDefence.Mul(MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.heatComboElementDefRate);
    this.CalcDefenceOnlyBuff(ref baseDefence);
    return baseDefence.CalcTotal();
  }

  private void CalcDefenceBuffAndPassive(ref AtkAttribute baseDefence, bool withBuff)
  {
    AtkAttribute val = new AtkAttribute();
    val.Set(1f);
    val.Add(this.buffParam.passive.defUpRate);
    val.Sub(this.buffParam.passive.defDownRate);
    baseDefence.Mul(val);
    baseDefence.normal += (float) this.buffParam.passive.defList[0];
    baseDefence.fire += (float) this.buffParam.passive.defList[1];
    baseDefence.water += (float) this.buffParam.passive.defList[2];
    baseDefence.thunder += (float) this.buffParam.passive.defList[3];
    baseDefence.soil += (float) this.buffParam.passive.defList[4];
    baseDefence.light += (float) this.buffParam.passive.defList[5];
    baseDefence.dark += (float) this.buffParam.passive.defList[6];
    baseDefence.CheckMinus();
    if (withBuff)
    {
      val.Set(1f);
      val.Add(this.buffParam.GetBuffDefenceRate());
      baseDefence.Mul(val);
      this.AddDefenceBuff(ref baseDefence);
    }
    baseDefence.CheckMinus();
  }

  private void CalcDefenceOnlyBuff(ref AtkAttribute baseDefence)
  {
    AtkAttribute val = new AtkAttribute();
    val.Set(1f);
    val.Add(this.buffParam.GetBuffDefenceRate());
    val.normal = 0.0f;
    baseDefence.Mul(val);
    this.AddElementDefenceBuff(ref baseDefence);
    baseDefence.CheckMinus();
  }

  public override void OnAttackedHitOwner(AttackedHitStatusOwner status)
  {
    this.ApplyInvicibleCount(status);
    bool flag1 = this.ApplyInvicibleBadStatus(status);
    if (!flag1)
      flag1 = this.ApplyInvincibleBadStatusToRestraint(status);
    if (this._IsGuard() && this._CheckJustGuardSec() && this.spAttackType == SP_ATTACK_TYPE.BURST)
    {
      status.badStatusAdd.Reset();
      flag1 = false;
    }
    if (this.IsInSpearBurstBarrier())
    {
      status.badStatusAdd.Reset();
      flag1 = false;
    }
    status.afterHealHp = this.hp;
    if (status.validDamage)
    {
      status.afterHealHp -= (int) Mathf.Ceil((float) status.damage * (1f - this.damageHealRate));
      if (status.afterHealHp < 0)
        status.afterHealHp = 0;
    }
    if (this.IsStone())
      this.ActStoneEnd(this.stoneRescueTime);
    if ((this.IsCoopNone() || this.IsOriginal()) && this._IsGuard() && this._CheckJustGuardSec())
    {
      if (MonoBehaviourSingleton<EffectManager>.IsValid() && this.spAttackType != SP_ATTACK_TYPE.BURST)
        EffectManager.OneShot("ef_btl_wsk_sword_01_01", this._position, this._rotation);
      if (this.EnablePlaySound() && this.spAttackType != SP_ATTACK_TYPE.BURST)
        SoundManager.PlayOneShotSE(10000042, status.hitPos);
      if (this.spAttackType == SP_ATTACK_TYPE.HEAT)
        this.skillInfo.AddUseGauge(this.playerParameter.ohsActionInfo.Heat_JustGuardSkillHealValue, true, true);
    }
    bool flag2 = this.hp + (int) this.ShieldHp - status.damage > 0 || this.IsNarrowEscape(status);
    bool flag3 = this.IsAntiGrabAndRestraint();
    AttackHitInfo attackInfo = status.attackInfo;
    GrabInfo grabInfo = attackInfo.grabInfo;
    if (grabInfo.enable & flag2 && !flag3)
    {
      this.ActGrabbedStart(status.fromObjectID, grabInfo);
      status.reactionType = 0;
    }
    if (attackInfo.restraintInfo.enable & flag2 && this.actionID != (Character.ACTION_ID) 30 && !flag3 && !this.IsAntiRestraint())
    {
      this.ActRestraint(attackInfo.restraintInfo);
      status.reactionType = 0;
    }
    if (this.IsValidShield())
    {
      AtkAttribute invinsibleMulRate = this.GetInvinsibleMulRate();
      status.damageDetails.Mul(invinsibleMulRate);
      status.damage = (int) status.damageDetails.CalcTotal();
      Player.ShieldDamageData shieldDamageData = this.CalcShieldDamage(status.damage);
      status.damage = shieldDamageData.hpDamage;
      status.shieldDamage = shieldDamageData.shieldDamage;
      if (shieldDamageData.shieldDamage > 0 && this.IsValidBuff(BuffParam.BUFFTYPE.SHIELD_REFLECT) && this.shieldReflectInfo != null)
      {
        int shieldDamage = shieldDamageData.shieldDamage;
        if (this.IsValidBuff(BuffParam.BUFFTYPE.SHIELD_REFLECT_DAMAGE_UP))
          shieldDamage *= this.buffParam.GetValue(BuffParam.BUFFTYPE.SHIELD_REFLECT_DAMAGE_UP);
        this.shieldReflectInfo.damage = shieldDamage;
        this.shieldReflectInfo.targetId = status.fromObjectID;
        this.OnShotShieldReflect(this.shieldReflectInfo);
      }
      if (this.IsValidBuff(BuffParam.BUFFTYPE.SHIELD_INVINCIBLE_BADSTATUS))
      {
        status.badStatusAdd.Reset();
        flag1 = false;
      }
    }
    this.ResetBoostPowerUpTriggerDamage();
    if (attackInfo.toPlayer.isBuffCancellation)
      this.OnBuffCancellation();
    if (flag1)
      this.buffParam.DecreaseInvincibleBadStatus();
    base.OnAttackedHitOwner(status);
    this.isAbsorbDamageSuperArmor = false;
  }

  protected override bool ApplyInvicibleBadStatus(AttackedHitStatusOwner status)
  {
    if (!this.buffParam.IsValidBuff(BuffParam.BUFFTYPE.INVINCIBLE_BADSTATUS) || !status.badStatusAdd.isExist())
      return false;
    status.badStatusAdd.Reset();
    return true;
  }

  protected override bool IsNarrowEscape(AttackedHitStatusOwner status)
  {
    if ((double) this.timeWhenJustGuardChecked == (double) Time.time)
      return true;
    if (status.afterHP > 0)
      return false;
    if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.BURST) && this._IsGuard() && this._CheckJustGuardSec())
    {
      this.timeWhenJustGuardChecked = Time.time;
      return true;
    }
    return this.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE) && this.spearCtrl.CanConsumeOracleStock() || this.buffParam.IsNarrowEscape();
  }

  protected bool IsAntiGrabAndRestraint()
  {
    return this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.BURST) && this._IsGuard() && this._CheckJustGuardSec() || this.IsInAliveBarrier();
  }

  protected bool IsAntiRestraint()
  {
    return this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.BURST) && this._IsGuard() && this._CheckJustGuardSec() || this.IsInAliveBarrier() || this.IsInSpearBurstBarrier() || this.buffParam.IsValidBuff(BuffParam.BUFFTYPE.INVINCIBLE_BADSTATUS) || this.buffParam.IsValidBuff(BuffParam.BUFFTYPE.SHIELD_INVINCIBLE_BADSTATUS) || this.buffParam.IsValidInvincibleCountBuff();
  }

  protected bool ApplyInvincibleBadStatusToRestraint(AttackedHitStatusOwner status)
  {
    return status.attackInfo.restraintInfo.enable && this.buffParam.IsValidBuff(BuffParam.BUFFTYPE.INVINCIBLE_BADSTATUS);
  }

  protected override void UseNarrowEscape(AttackedHitStatusOwner status)
  {
    if ((double) this.timeWhenJustGuardChecked == (double) Time.time)
      return;
    if (this.buffParam.IsNarrowEscape())
    {
      this.buffParam.UseNarrowEscape();
    }
    else
    {
      if (!this.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE) || !this.spearCtrl.CanConsumeOracleStock())
        return;
      this.spearCtrl.StartOracleGutsMode();
    }
  }

  protected override bool IsHitReactionValid(AttackedHitStatusOwner status)
  {
    return (status.fromType != StageObject.OBJECT_TYPE.PLAYER || this.playerParameter.playerHitReactionValid) && base.IsHitReactionValid(status) && status.validDamage;
  }

  protected override Character.REACTION_TYPE OnHitReaction(AttackedHitStatusOwner status)
  {
    if (this._IsGuard())
    {
      Vector3 fromPos = status.fromPos;
      fromPos.y = this._position.y;
      this._LookAt(fromPos);
      return Character.REACTION_TYPE.GUARD_DAMAGE;
    }
    Character.REACTION_TYPE reactionType = Character.REACTION_TYPE.NONE;
    bool flag1 = false;
    bool flag2 = false;
    switch (status.attackInfo.toPlayer.reactionType)
    {
      case AttackHitInfo.ToPlayer.REACTION_TYPE.DAMAGE:
        if (!this.IsValidSuperArmor())
        {
          reactionType = Character.REACTION_TYPE.DAMAGE;
          flag1 = true;
          break;
        }
        break;
      case AttackHitInfo.ToPlayer.REACTION_TYPE.BLOW:
        if (!this.IsValidSuperArmor())
        {
          reactionType = Character.REACTION_TYPE.BLOW;
          flag1 = true;
          flag2 = true;
          break;
        }
        break;
      case AttackHitInfo.ToPlayer.REACTION_TYPE.STUNNED_BLOW:
        if (!this.IsValidSuperArmor())
        {
          reactionType = Character.REACTION_TYPE.STUNNED_BLOW;
          flag1 = true;
          flag2 = true;
          break;
        }
        break;
      case AttackHitInfo.ToPlayer.REACTION_TYPE.STUMBLE:
        if (!this.IsValidSuperArmor())
        {
          reactionType = Character.REACTION_TYPE.STUMBLE;
          flag1 = true;
          break;
        }
        break;
      case AttackHitInfo.ToPlayer.REACTION_TYPE.FALL_BLOW:
        if (!this.IsValidSuperArmor())
        {
          reactionType = Character.REACTION_TYPE.FALL_BLOW;
          flag1 = true;
          flag2 = true;
          break;
        }
        break;
      case AttackHitInfo.ToPlayer.REACTION_TYPE.SHAKE:
        if (!this.IsValidSuperArmor())
        {
          reactionType = Character.REACTION_TYPE.SHAKE;
          break;
        }
        break;
      case AttackHitInfo.ToPlayer.REACTION_TYPE.CHARM_BLOW:
        if (!this.IsValidSuperArmor())
        {
          reactionType = Character.REACTION_TYPE.CHARM_BLOW;
          flag1 = true;
          flag2 = true;
          break;
        }
        break;
    }
    if (flag1)
    {
      Vector3 fromPos = status.fromPos;
      fromPos.y = this._position.y;
      this._LookAt(fromPos);
    }
    if (flag2)
    {
      Vector3 vector3_1 = Vector3.op_UnaryNegation(this._forward);
      Vector3 vector3_2 = Vector3.op_Multiply(Quaternion.op_Multiply(Quaternion.AngleAxis(status.attackInfo.toPlayer.reactionBlowAngle, this._right), vector3_1), status.attackInfo.toPlayer.reactionBlowForce);
      status.blowForce = vector3_2;
    }
    return reactionType != Character.REACTION_TYPE.NONE ? reactionType : Character.REACTION_TYPE.NONE;
  }

  private bool IsValidSuperArmor()
  {
    if (this.IsRestraint() || this.IsParalyze() || this.IsStone())
      return false;
    return this.enableSuperArmor || this.IsValidBuff(BuffParam.BUFFTYPE.SUPER_ARMOR) || this.IsValidBuff(BuffParam.BUFFTYPE.SHIELD_SUPER_ARMOR) || this.buffParam.IsValidInvincibleCountBuff() || this.IsValidBuff(BuffParam.BUFFTYPE.INVINCIBLE_BUFF_CANCELLATION_EXPAND) || this.isAbsorbDamageSuperArmor || this.IsInSpearBurstBarrier() || this.isInvincibleDamageSuperArmor || this.spearCtrl.IsGuard();
  }

  protected override Character.REACTION_TYPE CheckReActionTolerance(AttackedHitStatusOwner status)
  {
    Character.REACTION_TYPE reactionType = base.CheckReActionTolerance(status);
    if (this.isActSkillAction && this.buffParam.IsInvalidReaction(BuffParam.TOLERANCETYPE.SKILL))
      reactionType = Character.REACTION_TYPE.NONE;
    return reactionType;
  }

  public override void OnAttackedHitFix(AttackedHitStatusFix status)
  {
    int num = this.isDead ? 1 : 0;
    base.OnAttackedHitFix(status);
    if (MonoBehaviourSingleton<CoopManager>.IsValid())
      MonoBehaviourSingleton<CoopManager>.I.coopStage.battleUserLog.Add((Character) this, status);
    if (num != 0)
      return;
    this.healHp = status.reactionType == 8 ? 0 : status.afterHealHp;
    int index = 0;
    for (int count = this.m_weaponCtrlList.Count; index < count; ++index)
      this.m_weaponCtrlList[index].OnAttackedHitFix(status);
    if (status.damage == 0 && status.shieldDamage == 0)
      return;
    this.pairSwordsCtrl.DecreaseSoulGaugeByDamage();
  }

  public override void ActReaction(Character.ReactionInfo info, bool isSync = false)
  {
    base.ActReaction(info, isSync);
    switch (info.reactionType)
    {
      case Character.REACTION_TYPE.BLOW:
        this.ActBlow(info.blowForce);
        break;
      case Character.REACTION_TYPE.STUNNED_BLOW:
        this.ActStunnedBlow(info.blowForce, info.loopTime);
        break;
      case Character.REACTION_TYPE.STUMBLE:
        this.ActStumble(info.loopTime);
        break;
      case Character.REACTION_TYPE.FALL_BLOW:
        this.ActFallBlow(info.blowForce);
        break;
      case Character.REACTION_TYPE.SHAKE:
        this.ActShake();
        break;
      case Character.REACTION_TYPE.GUARD_DAMAGE:
        this.ActGuardDamage();
        break;
      case Character.REACTION_TYPE.STONE:
        this.ActStone();
        break;
      case Character.REACTION_TYPE.CHARM_BLOW:
        this.ActCharmBlow(info.blowForce, info.loopTime);
        break;
    }
  }

  public override string EffectNameAnalyzer(string effect_name)
  {
    if (!string.IsNullOrEmpty(effect_name) && effect_name[0] == '@')
    {
      SkillInfo.SkillParam actSkillParam = this.skillInfo.actSkillParam;
      if (actSkillParam == null)
      {
        Log.Error(LOG.INGAME, "EffectNameAnalyzer skill param is null.");
        return (string) null;
      }
      if (actSkillParam.tableData == null)
      {
        Log.Error(LOG.INGAME, "EffectNameAnalyzer skill param table data is null.");
        return (string) null;
      }
      switch (effect_name)
      {
        case "@skill_start":
          return actSkillParam.tableData.startEffectName;
        case "@skill_act_local":
          return actSkillParam.tableData.actLocalEffectName;
        case "@skill_act_oneshot":
          return actSkillParam.tableData.actOneshotEffectName;
        case "@skill_enchant":
          return actSkillParam.tableData.enchantEffectName;
      }
    }
    return effect_name;
  }

  public override Transform FindNode(string name)
  {
    switch (name)
    {
      case "weaponR":
        return this.loader.wepR;
      case "weaponL":
        return this.loader.wepL;
      default:
        return base.FindNode(name);
    }
  }

  protected void IgnoreEnemyColliders()
  {
    List<Collider> colliderList = new List<Collider>();
    int index = 0;
    for (int count = MonoBehaviourSingleton<StageObjectManager>.I.enemyList.Count; index < count; ++index)
    {
      Enemy enemy = MonoBehaviourSingleton<StageObjectManager>.I.enemyList[index] as Enemy;
      if (Object.op_Inequality((Object) enemy, (Object) null) && enemy.colliders != null)
        colliderList.AddRange((IEnumerable<Collider>) enemy.colliders);
    }
    if (colliderList.Count <= 0)
      return;
    this.IgnoreColliders(colliderList.ToArray());
  }

  public override bool CanPlayEffectEvent() => this.skillInfo.actSkillParam != null;

  public override void OnAnimEvent(AnimEventData.EventData data)
  {
    switch (data.id)
    {
      case AnimEventFormat.ID.SHOT_ARROW:
        bool isSitShot = false;
        int num1 = 0;
        if (data.intArgs != null)
        {
          if (data.intArgs.Length != 0)
            isSitShot = data.intArgs[0] > 0;
          if (data.intArgs.Length > 1)
            num1 = data.intArgs[1];
        }
        string str1 = this.playerParameter.arrowActionInfo.attackInfoNames[(int) this.spAttackType];
        if (data.stringArgs != null && data.stringArgs.Length != 0)
        {
          if (isSitShot)
            str1 = this.playerParameter.arrowActionInfo.attackInfoForSitShotNames[(int) this.spAttackType] + num1.ToString();
          if (this.spAttackType == SP_ATTACK_TYPE.BURST && this.isBoostMode && !isSitShot)
            str1 = this.playerParameter.arrowActionInfo.boostArrowChargeMaxAttackInfoName;
        }
        AttackInfo attackInfo = this.FindAttackInfo(str1);
        ++this.shotArrowCount;
        if (attackInfo == null || this.attackMode != Player.ATTACK_MODE.ARROW)
          break;
        float speed;
        if (this.defaultBulletSpeedDic.ContainsKey(str1))
        {
          speed = this.defaultBulletSpeedDic[str1];
        }
        else
        {
          speed = attackInfo.bulletData.data.speed;
          this.defaultBulletSpeedDic.Add(str1, attackInfo.bulletData.data.speed);
        }
        if (isSitShot)
          speed *= 1f + MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.sitShotBulletSpeedUpRate;
        attackInfo.bulletData.data.speed = speed;
        if (!this.IsCoopNone() && !this.IsOriginal())
          break;
        Vector3 bulletAppearPos = this.GetBulletAppearPos();
        Quaternion shot_rot = Quaternion.LookRotation(this.GetBulletShotVec(bulletAppearPos));
        bool isAimEnd = !isSitShot || num1 == 3;
        if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.HEAT) & isSitShot && MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
        {
          float sitShotSideAngle = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.sitShotSideAngle;
          if (num1 > 1)
          {
            float num2 = num1 % 2 == 1 ? -1f : 1f;
            shot_rot = Quaternion.op_Multiply(shot_rot, Quaternion.Euler(0.0f, num2 * sitShotSideAngle, 0.0f));
          }
        }
        if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.BURST) && this.isBoostMode)
          isAimEnd = num1 == 3;
        this.ShotArrow(bulletAppearPos, shot_rot, attackInfo, isSitShot, isAimEnd);
        break;
      case AnimEventFormat.ID.ARROW_AIMABLE_START:
        this.isArrowAimable = true;
        break;
      case AnimEventFormat.ID.ARROW_AIMABLE_END:
        this.isArrowAimable = false;
        break;
      case AnimEventFormat.ID.COMBO_INPUT_ON:
        int intArg1 = data.intArgs[0];
        this.enableInputCombo = true;
        this.controllerInputCombo = true;
        this.inputComboID = intArg1;
        this.inputComboMotionState = data.stringArgs.Length != 0 ? data.stringArgs[0] : "";
        break;
      case AnimEventFormat.ID.COMBO_INPUT_OFF:
        this.enableInputCombo = false;
        this.controllerInputCombo = false;
        break;
      case AnimEventFormat.ID.COMBO_TRANSITION_ON:
        this.enableComboTrans = true;
        if (!this.inputComboFlag)
          break;
        this.ActAttackCombo();
        break;
      case AnimEventFormat.ID.CHARGE_INPUT_START:
        float num3 = data.floatArgs[0];
        float num4 = data.floatArgs[1];
        bool flag = data.intArgs[0] != 0;
        if (data.intArgs != null && data.intArgs.Length > 1)
          this.isArrowSitShot = data.intArgs[1] > 0;
        float num5 = 0.0f;
        switch (this.attackMode)
        {
          case Player.ATTACK_MODE.TWO_HAND_SWORD:
            num5 = this.spAttackType == SP_ATTACK_TYPE.ORACLE ? this.GetOracleChargeTimeRate() : this.buffParam.GetChargeSwordsTimeRate();
            break;
          case Player.ATTACK_MODE.SPEAR:
            switch (this.spAttackType)
            {
              case SP_ATTACK_TYPE.NONE:
                num4 = 0.0f;
                num3 = this.playerParameter.spearActionInfo.exRushValidSec;
                if (this.evolveCtrl.IsExecLeviathan())
                {
                  num5 = 1f;
                  break;
                }
                break;
              case SP_ATTACK_TYPE.SOUL:
                num5 = this.buffParam.GetChargeSpearTimeRate();
                break;
              case SP_ATTACK_TYPE.ORACLE:
                num5 = this.spearCtrl.GetOracleSpChargeTimeRate(this.buffParam.GetChargeSpearTimeRate());
                break;
            }
            break;
          case Player.ATTACK_MODE.PAIR_SWORDS:
            num5 = this.buffParam.GetChargePairSwordsTimeRate();
            break;
          case Player.ATTACK_MODE.ARROW:
            num5 = this.GetChargeArrowTimeRate();
            break;
        }
        float num6;
        if (data.floatArgs.Length == 4)
        {
          this.isInputChargeExistOffset = true;
          float floatArg = data.floatArgs[2];
          num4 = data.floatArgs[3];
          if ((double) num5 >= 1.0)
            num5 = 1f;
          num6 = (float) (((double) floatArg - (double) num4) * (1.0 - (double) num5)) + num4;
        }
        else if (this.IsValidBuff(BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS))
        {
          this.isInputChargeExistOffset = false;
          num6 = 0.0f;
        }
        else
        {
          this.isInputChargeExistOffset = false;
          num6 = num3 * (1f - num5);
        }
        this.enableInputCharge = true;
        this.inputChargeAutoRelease = flag;
        this.inputChargeMaxTiming = true;
        this.inputChargeTimeMax = num6;
        this.inputChargeTimeOffset = num4;
        this.inputChargeTimeCounter = 0.0f;
        this.chargeRate = 0.0f;
        this.isChargeExRush = false;
        this.exRushChargeRate = 0.0f;
        this.StartWaitingPacket(StageObject.WAITING_PACKET.PLAYER_CHARGE_RELEASE, true);
        this.CheckInputCharge();
        break;
      case AnimEventFormat.ID.SKILL_CAST_LOOP_START:
        string str2 = data.stringArgs.Length != 0 ? data.stringArgs[0] : (string) null;
        if (string.IsNullOrEmpty(str2))
          str2 = "next";
        if (this.isSkillCastLoop || this.skillInfo.actSkillParam == null || this.skillInfo.actSkillParam.tableData == null)
          break;
        this.isSkillCastLoop = true;
        this.skillCastLoopStartTime = Time.time;
        this.skillCastLoopTrigger = str2;
        XorFloat castTimeRate = this.skillInfo.actSkillParam.castTimeRate;
        float num7 = (float) (1.0 - ((double) this.buffParam.GetSkillTimeRate() + (double) (float) castTimeRate));
        if ((double) num7 <= 0.0)
          num7 = 0.1f;
        this.skillCastLoopTime = this.skillInfo.actSkillParam.tableData.castTime * num7;
        this.CheckSkillCastLoop();
        break;
      case AnimEventFormat.ID.BLOW_CLEAR_INPUT_ON:
        int intArg2 = data.intArgs[0];
        this.enableInputCombo = true;
        this.inputComboID = intArg2;
        break;
      case AnimEventFormat.ID.BLOW_CLEAR_INPUT_OFF:
        this.enableInputCombo = false;
        break;
      case AnimEventFormat.ID.BLOW_CLEAR_TRANSITION_ON:
        if (!this.inputBlowClearFlag)
          break;
        this.SetBlowClear(this.inputComboID);
        break;
      case AnimEventFormat.ID.SUPERARMOR_ON:
        this.enableSuperArmor = true;
        break;
      case AnimEventFormat.ID.SUPERARMOR_OFF:
        this.enableSuperArmor = false;
        break;
      case AnimEventFormat.ID.ROTATE_TO_TARGET_START:
      case AnimEventFormat.ID.ROTATE_KEEP_TO_TARGET_START:
      case AnimEventFormat.ID.ROTATE_TO_ANGLE_START:
      case AnimEventFormat.ID.ROTATE_TO_TARGET_OFFSET:
        if (this.startInputRotate || this.isArrowAimBossMode || this.isArrowAimLesserMode || this.IsValidBuffBlind())
          break;
        base.OnAnimEvent(data);
        break;
      case AnimEventFormat.ID.ROTATE_INPUT_ON:
        this.enableInputRotate = true;
        this.startInputRotate = false;
        break;
      case AnimEventFormat.ID.ROTATE_INPUT_OFF:
        this.EventRotateInputOff(data);
        break;
      case AnimEventFormat.ID.WARP_VIEW_START:
        this.SetEnableNodeRenderer(string.Empty, false);
        Utility.SetLayerWithChildren(this._transform, 16 /*0x10*/, 12);
        this.DeactivateStoredEffect();
        break;
      case AnimEventFormat.ID.WARP_VIEW_END:
        this.SetEnableNodeRenderer(string.Empty, true);
        Utility.SetLayerWithChildren(this._transform, 8, 12);
        this.ActivateStoredEffect();
        break;
      case AnimEventFormat.ID.SE_SKILL_ONESHOT:
        SkillInfo.SkillParam actSkillParam = this.skillInfo.actSkillParam;
        if (actSkillParam == null || actSkillParam.tableData == null)
        {
          Log.Error(LOG.INGAME, "SE_SKILL_ONESHOT skill param none.");
          break;
        }
        int se_id = 0;
        switch ((Player.SKILL_SE_TYPE) data.intArgs[0])
        {
          case Player.SKILL_SE_TYPE.START:
            se_id = actSkillParam.tableData.startSEID;
            break;
          case Player.SKILL_SE_TYPE.ACT:
            se_id = actSkillParam.tableData.actSEID;
            break;
        }
        string stringArg = data.stringArgs[0];
        if (se_id == 0)
        {
          base.OnAnimEvent(data);
          break;
        }
        if (!this.EnablePlaySound())
          break;
        SoundManager.PlayOneShotSE(se_id, (DisableNotifyMonoBehaviour) this, this.FindNode(stringArg));
        break;
      case AnimEventFormat.ID.VOICE:
        this.PlayVoice(data.intArgs[0]);
        break;
      case AnimEventFormat.ID.CANCEL_TO_AVOID_ON:
        this.enableCancelToAvoid = true;
        break;
      case AnimEventFormat.ID.CANCEL_TO_AVOID_OFF:
        this.enableCancelToAvoid = false;
        break;
      case AnimEventFormat.ID.CANCEL_TO_MOVE_ON:
        this.enableCancelToMove = true;
        break;
      case AnimEventFormat.ID.CANCEL_TO_MOVE_OFF:
        this.enableCancelToMove = false;
        break;
      case AnimEventFormat.ID.CANCEL_TO_ATTACK_ON:
        this.enableCancelToAttack = true;
        break;
      case AnimEventFormat.ID.CANCEL_TO_ATTACK_OFF:
        this.enableCancelToAttack = false;
        break;
      case AnimEventFormat.ID.CANCEL_TO_SKILL_ON:
        this.enableCancelToSkill = true;
        break;
      case AnimEventFormat.ID.CANCEL_TO_SKILL_OFF:
        this.enableCancelToSkill = false;
        break;
      case AnimEventFormat.ID.CANCEL_TO_SPECIAL_ACTION_ON:
        this.enableCancelToSpecialAction = true;
        break;
      case AnimEventFormat.ID.CANCEL_TO_SPECIAL_ACTION_OFF:
        this.enableCancelToSpecialAction = false;
        break;
      case AnimEventFormat.ID.COUNTER_ATTACK_ON:
        if (this.disableCounterAnimEvent)
        {
          this.disableCounterAnimEvent = false;
          break;
        }
        this.enableCounterAttack = true;
        break;
      case AnimEventFormat.ID.COUNTER_ATTACK_OFF:
        this.enableCounterAttack = false;
        break;
      case AnimEventFormat.ID.ENABLE_ANIM_RATE_ON:
        this.enableAnimSeedRate = true;
        this.UpdateAnimatorSpeed();
        break;
      case AnimEventFormat.ID.ENABLE_ANIM_RATE_OFF:
        this.enableAnimSeedRate = false;
        this.UpdateAnimatorSpeed();
        break;
      case AnimEventFormat.ID.FACE:
        if (!Object.op_Inequality((Object) this.loader, (Object) null))
          break;
        this.loader.ChangeFace((PlayerLoader.FACE_ID) data.intArgs[0]);
        break;
      case AnimEventFormat.ID.STUNNED_LOOP_START:
        this.isStunnedLoop = true;
        this.stunnedEndTime = Time.time + this.stunnedTime;
        this.stunnedReduceEnableTime = this.stunnedTime * this.playerParameter.stunnedReduceTimeMaxRate;
        break;
      case AnimEventFormat.ID.APPLY_SKILL_PARAM:
        this.ApplySkillParam();
        break;
      case AnimEventFormat.ID.APPLY_BLOW_FORCE:
        this.waitAddForce = false;
        this.SetVelocity(Vector3.zero);
        break;
      case AnimEventFormat.ID.APPLY_CHANGE_WEAPON:
        if (this.IsCoopNone() || this.IsOriginal())
        {
          this.ApplyChangeWeapon(this.changeWeaponItem, this.changeWeaponIndex, this.changePlayerInfo);
          break;
        }
        if (this.IsValidWaitingPacket(StageObject.WAITING_PACKET.PLAYER_APPLY_CHANGE_WEAPON))
        {
          Log.Error(LOG.INGAME, "Player APPLY_CHANGE_WEAPON Err. ( StartWaitingPacket already. )");
          break;
        }
        this.StartWaitingPacket(StageObject.WAITING_PACKET.PLAYER_APPLY_CHANGE_WEAPON, false);
        break;
      case AnimEventFormat.ID.APPLY_GATHER:
        if (!this.IsCoopNone() && !this.IsOriginal())
          break;
        this.ApplyGather();
        break;
      case AnimEventFormat.ID.TARGET_LOCK_ON:
        break;
      case AnimEventFormat.ID.TARGET_LOCK_OFF:
        break;
      case AnimEventFormat.ID.PLAYER_FUNNEL_ATTACK:
        this.EventPlayerFunnelAttack(data);
        break;
      case AnimEventFormat.ID.RUSH_LOOP_START:
        this.isLoopingRush = true;
        break;
      case AnimEventFormat.ID.SP_ATTACK_CONTINUE_ON:
        this.enableSpAttackContinue = true;
        break;
      case AnimEventFormat.ID.SP_ATTACK_CONTINUE_OFF:
        this.enableSpAttackContinue = false;
        break;
      case AnimEventFormat.ID.TWO_HAND_SWORD_CHARGE_EXPAND_START:
        if (this.isChargeExpanding)
          break;
        this.isChargeExpanding = true;
        this.timerChargeExpandOffset = 0.0f;
        this.isInputChargeExistOffset = false;
        float timeChargeExpandMax = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.timeChargeExpandMax;
        float num8 = this.buffParam.GetChargeSwordsTimeRate();
        if ((double) num8 > 1.0)
          num8 = 1f;
        float num9 = timeChargeExpandMax * (1f - num8);
        if ((double) num9 < (double) MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.minTimeChargeExpandMax)
        {
          this.timerChargeExpandOffset = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.minTimeChargeExpandMax - num9;
          num9 = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.minTimeChargeExpandMax;
        }
        this.isChargeExpandAutoRelease = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.isChargeExpandAutoRelease;
        this.timeChargeExpandMax = num9;
        this.timerChargeExpand = 0.0f;
        this.chargeExpandRate = 0.0f;
        this.StartWaitingPacket(StageObject.WAITING_PACKET.PLAYER_CHARGE_RELEASE, true);
        this.CheckChargeExpand();
        break;
      case AnimEventFormat.ID.SE_ONESHOT_DEPEND_WEAPON_ELEMENT:
        int currentWeaponElement = this.GetCurrentWeaponElement();
        if (currentWeaponElement > 6 || data.intArgs.Length <= currentWeaponElement || data.intArgs[currentWeaponElement] == 0)
          break;
        string name = string.Empty;
        if (data.stringArgs.Length != 0)
          name = data.stringArgs[0];
        int intArg3 = data.intArgs[currentWeaponElement];
        if (intArg3 == 0 || !this.EnablePlaySound())
          break;
        SoundManager.PlayOneShotSE(intArg3, (DisableNotifyMonoBehaviour) this, this.FindNode(name));
        break;
      case AnimEventFormat.ID.SPEAR_HUNDRED_START:
        if (this.isSpearHundred)
          break;
        this.isSpearHundred = true;
        this.spearHundredSecFromStart = 0.0f;
        this.spearHundredSecFromLastTap = 0.0f;
        break;
      case AnimEventFormat.ID.SPEAR_JUMP_CHARGE_START:
        if (this.enableInputCharge)
          break;
        this.enableInputCharge = true;
        this.isArrowAimable = true;
        this.isSpearJumpAim = true;
        this.jumpState = Player.eJumpState.Charge;
        InGameSettingsManager.Player.SpearActionInfo spearActionInfo1 = MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo;
        float num10 = spearActionInfo1.jumpChargeBaseSec * (1f - this.buffParam.GetChargeSpearTimeRate());
        if ((double) num10 < (double) spearActionInfo1.jumpChargeMinSec)
          num10 = spearActionInfo1.jumpChargeMinSec;
        this.inputChargeAutoRelease = false;
        this.inputChargeMaxTiming = true;
        this.inputChargeTimeMax = num10;
        this.inputChargeTimeOffset = 0.0f;
        this.inputChargeTimeCounter = 0.0f;
        this.isInputChargeExistOffset = false;
        this.chargeRate = 0.0f;
        this.isChargeExRush = false;
        this.exRushChargeRate = 0.0f;
        this.StartWaitingPacket(StageObject.WAITING_PACKET.PLAYER_CHARGE_RELEASE, true);
        this.CheckInputCharge();
        break;
      case AnimEventFormat.ID.SPEAR_JUMP_FALL_WAIT:
        if (this.body == null)
        {
          this.ActIdle(false, -1f);
          break;
        }
        InGameSettingsManager.Player.SpearActionInfo spearActionInfo2 = MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo;
        Vector3 vector3_1;
        // ISSUE: explicit constructor call
        ((Vector3) ref vector3_1).\u002Ector(this._position.x + this.jumpFallBodyPosition.x, this._position.y, this._position.z + this.jumpFallBodyPosition.z);
        Vector3 vector3_2 = Vector3.op_Subtraction(this._position, vector3_1);
        this.jumpRandingVector = Vector3.op_Multiply(((Vector3) ref vector3_2).normalized, spearActionInfo2.jumpRandingLength);
        this._position = vector3_1;
        ((Vector3) ref this.jumpFallBodyPosition).Set(0.0f, spearActionInfo2.jumpStartHeight, 0.0f);
        ((Component) this.body).transform.localPosition = this.jumpFallBodyPosition;
        this.jumpActionCounter = spearActionInfo2.jumpFallWaitSec;
        this.jumpState = Player.eJumpState.FallWait;
        break;
      case AnimEventFormat.ID.RUSH_CAN_RELEASE:
        this.isCanRushRelease = true;
        break;
      case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE:
        if (this.jumpState == Player.eJumpState.None)
        {
          this.CreateAttackCollider(data);
          break;
        }
        InGameSettingsManager.Player.SpearActionInfo spearActionInfo3 = MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo;
        AnimEventData.EventData eventData = new AnimEventData.EventData();
        eventData.Copy(data, true);
        eventData.stringArgs[0] = spearActionInfo3.jumpWaveAttackInfoPrefix + (object) this.useGaugeLevel;
        eventData.floatArgs[6] = spearActionInfo3.jumpWaveColliderRadius[this.useGaugeLevel];
        this.CreateAttackCollider(eventData);
        break;
      case AnimEventFormat.ID.BOOSTCOMBO_TRANSITION_ON:
        if (data.stringArgs.Length != 0 && !string.IsNullOrEmpty(data.stringArgs[0]) && (SP_ATTACK_TYPE) Enum.Parse(typeof (SP_ATTACK_TYPE), data.stringArgs[0]) != this.spAttackType || !this.isBoostMode)
          break;
        this.enableComboTrans = true;
        if (!this.inputComboFlag)
          break;
        this.ActAttackCombo();
        break;
      case AnimEventFormat.ID.TWO_HAND_SWORD_CHARGE_SOUL_START:
        if (!this.CheckAttackModeAndSpType(Player.ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.SOUL) || this.enableInputCharge)
          break;
        this.enableInputCharge = true;
        InGameSettingsManager.Player.TwoHandSwordActionInfo handSwordActionInfo = this.playerParameter.twoHandSwordActionInfo;
        float soulChargeTimeRate = this.buffParam.GetSoulChargeTimeRate();
        if (MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.ignoreSpAttackTypeAbility)
          soulChargeTimeRate += this.buffParam.GetChargeSwordsTimeRate();
        float num11 = handSwordActionInfo.soulIaiChargeTime * (1f - soulChargeTimeRate);
        if ((double) num11 < (double) handSwordActionInfo.soulIaiChargeTimeMin)
          num11 = handSwordActionInfo.soulIaiChargeTimeMin;
        this.inputChargeAutoRelease = false;
        this.inputChargeMaxTiming = true;
        this.inputChargeTimeMax = num11;
        this.inputChargeTimeOffset = 0.0f;
        this.inputChargeTimeCounter = 0.0f;
        this.isInputChargeExistOffset = false;
        this.chargeRate = 0.0f;
        this.StartWaitingPacket(StageObject.WAITING_PACKET.PLAYER_CHARGE_RELEASE, true);
        this.CheckInputCharge();
        break;
      case AnimEventFormat.ID.SKIP_TO_SKILL_ACTION_ON:
        this.isAbleToSkipSkillAction = true;
        break;
      case AnimEventFormat.ID.SKIP_TO_SKILL_ACTION_OFF:
        this.isAbleToSkipSkillAction = false;
        break;
      case AnimEventFormat.ID.CHANGE_SKILL_TO_SECOND_GRADE:
        if (data.intArgs.Length == 0)
          break;
        if (data.intArgs[0] == 1)
        {
          Log.Error("CHANGE_SKILL_TO_SECOND_GRADEに「1」は設定できません");
          break;
        }
        if (!this.isUsingSecondGradeSkill)
          break;
        this.SetNextTrigger(data.intArgs[0] - 1);
        this.isUsingSecondGradeSkill = false;
        this.skillInfo.ResetSecondGradeFlags();
        break;
      case AnimEventFormat.ID.CANCEL_TO_EVOLVE_SPECIAL_ACTION_ON:
        this.enableCancelToEvolveSpecialAction = true;
        break;
      case AnimEventFormat.ID.CANCEL_TO_EVOLVE_SPECIAL_ACTION_OFF:
        this.enableCancelToEvolveSpecialAction = false;
        break;
      case AnimEventFormat.ID.PAIR_SWORDS_SHOT_BULLET:
        this.EventPairSwordsShotBullet(data);
        break;
      case AnimEventFormat.ID.PAIR_SWORDS_SHOT_LASER:
        this.EventPairSwordsShotLaser(data);
        break;
      case AnimEventFormat.ID.PAIR_SWORDS_SOUL_EFFECT_START_SHOT_LASER:
        this.pairSwordsCtrl.GetEffectTransStartShotLaser();
        break;
      case AnimEventFormat.ID.CANCEL_TO_ATTACK_NEXT_ON:
        this.enableAttackNext = true;
        break;
      case AnimEventFormat.ID.CANCEL_TO_ATTACK_NEXT_OFF:
        this.enableAttackNext = false;
        break;
      case AnimEventFormat.ID.ROTATE_TO_TARGET_POINT_START:
        this.EventRotateToTargetPointStart();
        break;
      case AnimEventFormat.ID.SHOT_HEALING_HOMING:
        this.EventShotHealingHoming(data);
        break;
      case AnimEventFormat.ID.SHOT_SOUL_ARROW:
        if (!this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.SOUL) || !this.IsCoopNone() && !this.IsOriginal())
          break;
        this.ShotSoulArrow();
        break;
      case AnimEventFormat.ID.SPEAR_SOUL_HEAL_HP:
        this.EventHealHp();
        break;
      case AnimEventFormat.ID.TWO_HAND_SWORD_BURST_BASE_ATK:
        break;
      case AnimEventFormat.ID.TWO_HAND_SWORD_BURST_FULL_BURST:
        if (this.thsCtrl == null)
          break;
        this.thsCtrl.DoFullBurst();
        break;
      case AnimEventFormat.ID.TWO_HAND_SWORD_BURST_READY_FOR_SHOT:
        if (this.thsCtrl == null || this.thsCtrl.IsReadyForShoot)
          break;
        this.thsCtrl.SetReadyForShoot(true);
        break;
      case AnimEventFormat.ID.TWO_HAND_SWORD_BURST_SINGLE_SHOT:
        if (this.thsCtrl == null)
          break;
        this.thsCtrl.DoShootAction();
        break;
      case AnimEventFormat.ID.TWO_HAND_SWORD_BURST_START_RELOAD:
        if (this.thsCtrl == null || this.thsCtrl.IsReloadingNow)
          break;
        this.thsCtrl.SetStartReloading(true);
        break;
      case AnimEventFormat.ID.TWO_HAND_SWORD_BURST_RELOAD_NOW:
        if (this.thsCtrl == null || !this.thsCtrl.IsReloadingNow)
          break;
        this.thsCtrl.DoReloadAction();
        break;
      case AnimEventFormat.ID.TWO_HAND_SWORD_BURST_RELOAD_DONE:
        if (this.thsCtrl == null || !this.thsCtrl.IsReloadingNow)
          break;
        this.thsCtrl.CheckEnableNextReloadAction();
        break;
      case AnimEventFormat.ID.THS_BURST_RELOAD_VARIABLE_SPEED_ON:
        if (this.thsCtrl == null)
          break;
        this.thsCtrl.SetChangeReloadMotionSpeedFlag(true);
        this.enableAnimSeedRate = true;
        this.UpdateAnimatorSpeed();
        break;
      case AnimEventFormat.ID.THS_BURST_RELOAD_VARIABLE_SPEED_OFF:
        if (this.thsCtrl == null)
          break;
        this.thsCtrl.SetChangeReloadMotionSpeedFlag(false);
        this.enableAnimSeedRate = false;
        this.UpdateAnimatorSpeed();
        break;
      case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE_MULTI:
        if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
          break;
        MonoBehaviourSingleton<StageObjectManager>.I.InvokeCoroutineImmidiately(this.CreateMultiAttackCollider(data));
        break;
      case AnimEventFormat.ID.THS_BURST_TRANSITION_AVOID_ATK_ON:
        if (this.thsCtrl == null)
          break;
        this.thsCtrl.SetEnableTransitionFromAvoidAtkFlag(true);
        break;
      case AnimEventFormat.ID.THS_BURST_TRANSITION_AVOID_ATK_OFF:
        if (this.thsCtrl == null)
          break;
        this.thsCtrl.SetEnableTransitionFromAvoidAtkFlag(false);
        break;
      case AnimEventFormat.ID.GATHER_GIMMICK_GET:
        this.OnGatherGimmickGet();
        break;
      case AnimEventFormat.ID.FLICK_ACTION_ON:
        this.enableFlickAction = true;
        break;
      case AnimEventFormat.ID.FLICK_ACTION_OFF:
        this.enableFlickAction = false;
        break;
      case AnimEventFormat.ID.NEXTTRIGGER_INPUT_ON:
        this.enableInputNextTrigger = true;
        if (((IList<int>) data.intArgs).IsNullOrEmpty<int>())
        {
          this.inputNextTriggerIndex = 0;
          break;
        }
        this.inputNextTriggerIndex = data.intArgs[0];
        break;
      case AnimEventFormat.ID.NEXTTRIGGER_INPUT_OFF:
        this.enableInputNextTrigger = false;
        break;
      case AnimEventFormat.ID.NEXTTRIGGER_TRANSITION_ON:
        this.enableNextTriggerTrans = true;
        if (!this.inputNextTriggerFlag)
          break;
        this.DoInputNextTrigger();
        break;
      case AnimEventFormat.ID.COUNT_LONGTOUCH_ON:
        this.isCountLongTouch = true;
        this.countLongTouchSec = 0.0f;
        break;
      case AnimEventFormat.ID.TRANSITION_LONGTOUCH:
        if (!this.isCountLongTouch)
          break;
        int intArg4 = data.intArgs[0];
        if ((double) this.countLongTouchSec < (double) data.floatArgs[0] || data.intArgs.Length > 1 && data.intArgs[1] > 0 && !this.isBoostMode)
          break;
        this.ActAttack(intArg4, true, false, "", "");
        break;
      case AnimEventFormat.ID.COMBINE_BURST_PAIRSWORD:
        if (!this.IsCoopNone() && !this.IsOriginal())
          break;
        bool isCombine = false;
        if (data.intArgs[0] == -1)
          isCombine = !this.pairSwordsCtrl.IsCombineMode();
        else if (data.intArgs[0] == 1)
          isCombine = true;
        this.pairSwordsCtrl.CombineBurst(isCombine);
        break;
      case AnimEventFormat.ID.AERIAL_ON:
        this.isAerial = true;
        break;
      case AnimEventFormat.ID.AERIAL_OFF:
        this.isAerial = false;
        break;
      case AnimEventFormat.ID.FISHING_SE_PLAY:
        if (!this.EnablePlaySound())
          break;
        int seId = this.fishingCtrl.GetSeId(data.intArgs[0]);
        if (seId == 0)
          break;
        if ((data.intArgs.Length <= 1 ? 0 : (data.intArgs[1] > 0 ? 1 : 0)) != 0)
        {
          SoundManager.PlayLoopSE(seId, (DisableNotifyMonoBehaviour) this, this.FindNode(""));
          break;
        }
        SoundManager.PlayOneShotSE(seId, (DisableNotifyMonoBehaviour) this, this.FindNode(""));
        break;
      case AnimEventFormat.ID.FISHING_SE_STOP:
        SoundManager.StopLoopSE(this.fishingCtrl.GetSeId(data.intArgs[0]), (DisableNotifyMonoBehaviour) this);
        break;
      case AnimEventFormat.ID.SHOT_RESURRECTION_HOMING:
        this.EventShotResurrectionHoming(data);
        break;
      case AnimEventFormat.ID.WEAPON_ACTION_START:
        this.EventWeaponActionStart();
        break;
      case AnimEventFormat.ID.WEAPON_ACTION_END:
        this.EventWeaponActionEnd();
        break;
      case AnimEventFormat.ID.SET_CONDITION_TRIGGER:
        this.EventSetConditionTrigger();
        break;
      case AnimEventFormat.ID.SET_CONDITION_TRIGGER_2:
        this.EventSetConditionTrigger2();
        break;
      case AnimEventFormat.ID.FIX_POSITION_WEAPON_R_ON:
        this.EventFixPositionWeaponR_ON();
        break;
      case AnimEventFormat.ID.FIX_POSITION_WEAPON_R_OFF:
        this.EventFixPositionWeaponR_OFF();
        break;
      case AnimEventFormat.ID.SPEAR_BURST_BARRIER_OFF:
        this.EventSpearBurstBarrier_OFF();
        break;
      case AnimEventFormat.ID.WEAPON_ACTION_ENABLED_ON:
        this.enableWeaponAction = true;
        break;
      case AnimEventFormat.ID.WEAPON_ACTION_ENABLED_OFF:
        this.enableWeaponAction = false;
        break;
      case AnimEventFormat.ID.BUFF_START_SHIELD_REFLECT:
        this.EventBuffStartShieldReflect(data);
        break;
      case AnimEventFormat.ID.RAIN_SHOT_CHARGE_START:
        this.EventArrowRainChargeStart(data);
        break;
      case AnimEventFormat.ID.SHOT_ARROW_RAIN:
        this.EventArrowRainStart(data);
        break;
      case AnimEventFormat.ID.THS_ORACLE_HORIZONTAL_START:
        this.thsCtrl.oracleCtrl.StartHorizontal(data.intArgs[0] != 0);
        break;
      case AnimEventFormat.ID.THS_ORACLE_HORIZONTAL_NEXT_CHECK:
        if (!this.thsCtrl.oracleCtrl.NextHorizontal())
          break;
        this.AttackHitCheckerClearInfo(data);
        if (!MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.oracleTHSInfo.isBleedingDamageOfHorizontalNext || !this.IsValidBuff(BuffParam.BUFFTYPE.BLEEDING))
          break;
        this.buffParam.OnBleeding();
        break;
      case AnimEventFormat.ID.THS_ORACLE_VERNIER_EFFECT_ON:
        this.thsCtrl.oracleCtrl.StartVernierEffect(data.intArgs[0] != 0);
        break;
      case AnimEventFormat.ID.THS_ORACLE_VERNIER_EFFECT_OFF:
        this.thsCtrl.oracleCtrl.EndVernierEffect();
        break;
      case AnimEventFormat.ID.START_CARRY_GIMMICK:
        this.EventStartCarryGimmick();
        break;
      case AnimEventFormat.ID.END_CARRY_GIMMICK:
        this.EventEndCarryGimmick();
        break;
      case AnimEventFormat.ID.ENABLE_CARRY_PUT_ON:
        this.enableCancelToCarryPut = true;
        break;
      case AnimEventFormat.ID.ENABLE_CARRY_PUT_OFF:
        this.enableCancelToCarryPut = false;
        break;
      case AnimEventFormat.ID.PLAYER_BLEEDING_DAMAGE:
        if (!this.IsCoopNone() && !this.IsOriginal() || !this.IsValidBuff(BuffParam.BUFFTYPE.BLEEDING))
          break;
        this.buffParam.OnBleeding();
        break;
      case AnimEventFormat.ID.PLAYER_TELEPORT_AVOID_ON:
        this.enabledTeleportAvoid = true;
        break;
      case AnimEventFormat.ID.PLAYER_TELEPORT_AVOID_OFF:
        this.enabledTeleportAvoid = false;
        break;
      case AnimEventFormat.ID.PLAYER_TELEPORT_TO_TARGET_OFFSET:
        this.EventTeleportToTargetOffset(data);
        break;
      case AnimEventFormat.ID.CAMERA_RESET_POSITION:
        break;
      case AnimEventFormat.ID.SET_EXTRA_SP_GAUGE_DECREASING_RATE:
        this.extraSpGaugeDecreasingRate = data.floatArgs[0];
        break;
      case AnimEventFormat.ID.NEXT_IF_BOOST_MODE:
        if (!this.isBoostMode)
          break;
        this.SetNextTrigger(data.intArgs[0]);
        break;
      case AnimEventFormat.ID.NEXT_IF_NOT_BOOST_MODE:
        if (this.isBoostMode)
          break;
        this.SetNextTrigger(data.intArgs[0]);
        break;
      case AnimEventFormat.ID.ORACLE_SPEAR_GUARD_ON:
        this.spearCtrl.GuardOn();
        break;
      case AnimEventFormat.ID.ORACLE_SPEAR_GUARD_OFF:
        this.spearCtrl.GuardOff();
        break;
      case AnimEventFormat.ID.SHOT_ORACLE_SPEAR_SP:
        this.spearCtrl.EventShotOracleSp(data);
        break;
      case AnimEventFormat.ID.DESTROY_ORACLE_SPEAR_SP:
        this.spearCtrl.DestroyOracleSp();
        break;
      case AnimEventFormat.ID.SHOT_ORACLE_PAIR_SWORDS_RUSH:
        this.pairSwordsCtrl.EventShotOracleRush(data);
        break;
      case AnimEventFormat.ID.PLAYER_RUSH_AVOID_ON:
        this.enabledRushAvoid = true;
        break;
      case AnimEventFormat.ID.PLAYER_RUSH_AVOID_OFF:
        this.enabledRushAvoid = false;
        break;
      case AnimEventFormat.ID.ORACLE_PAIR_SWORDS_SP_START:
        this.pairSwordsCtrl.StartOracleSp(data);
        break;
      case AnimEventFormat.ID.ORACLE_PAIR_SWORDS_SP_END:
        this.pairSwordsCtrl.EndOracleSp();
        break;
      default:
        base.OnAnimEvent(data);
        break;
    }
  }

  protected override void SetFromInfo(ref BuffParam.BuffData data)
  {
    data.fromObjectID = this.createInfo.charaInfo.userId;
    data.fromEquipIndex = this.weaponIndex;
    data.fromSkillIndex = this.skillInfo.skillIndex;
  }

  protected override void EventMoveStart(AnimEventData.EventData data, Vector3 targetDir)
  {
    float num = data.floatArgs[0];
    if (data.intArgs != null && data.intArgs.Length != 0 && data.intArgs[0] == 1 && (SP_ATTACK_TYPE) data.floatArgs.Length > this.spAttackType)
      num = data.floatArgs[(int) this.spAttackType];
    if (this.isActSpecialAction && this.CheckAttackMode(Player.ATTACK_MODE.SPEAR))
      num = Mathf.Max(0.0f, num * this.GetRushDistanceRate());
    if (this.actionID == Character.ACTION_ID.ATTACK && this.CheckAttackMode(Player.ATTACK_MODE.SPEAR) && this.spAttackType == SP_ATTACK_TYPE.BURST)
      num = Mathf.Max(0.0f, num + num * this.buffParam.GetSpearRushSpeedRate());
    if (this.IsFromAvoidAction())
      num *= this.buffParam.GetDistanceRateFromAvoid();
    this.EventMoveEnd();
    this.enableEventMove = true;
    this.enableAddForce = false;
    this.eventMoveVelocity = Vector3.op_Multiply(targetDir, num);
    this.SetVelocity(Quaternion.op_Multiply(Quaternion.LookRotation(this.GetTransformForward()), this.eventMoveVelocity), Character.VELOCITY_TYPE.EVENT_MOVE);
    this.eventMoveTimeCount = 0.0f;
  }

  private void EventRotateToTargetPointStart()
  {
    if (this.startInputRotate || this.isArrowAimBossMode || this.isArrowAimLesserMode || this.IsValidBuffBlind())
      return;
    this.enableRotateToTargetPoint = true;
    this.rotateEventSpeed = 0.0f;
  }

  private void EventRotateInputOff(AnimEventData.EventData data)
  {
    this.enableInputRotate = false;
    this.startInputRotate = false;
    if (data.intArgs == null || data.intArgs.Length == 0 || data.intArgs[0] != 1 || !Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnSyncPosition();
  }

  private bool IsFromAvoidAction()
  {
    if (this.attackMode != Player.ATTACK_MODE.PAIR_SWORDS)
      return false;
    return this.attackID == 20 || this.attackID == 21;
  }

  private void EventPlayerFunnelAttack(AnimEventData.EventData data)
  {
    string stringArg1 = data.stringArgs[0];
    string stringArg2 = data.stringArgs[1];
    Transform node = this.FindNode(stringArg2);
    if (Object.op_Equality((Object) node, (Object) null))
    {
      Log.Error("Not found transform for launch!! name:" + stringArg2);
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
        if (Object.op_Equality((Object) bulletData, (Object) null) || bulletData.dataFunnel == null)
        {
          Log.Error("Not found BulletData!! atkInfoName:" + stringArg1);
        }
        else
        {
          Vector3 offsetPos;
          // ISSUE: explicit constructor call
          ((Vector3) ref offsetPos).\u002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
          Quaternion offsetRot = Quaternion.Euler(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
          new GameObject("PlayerAttackFunnelBit").AddComponent<PlayerAttackFunnelBit>().Initialize((StageObject) this, attackInfo, this.IsValidBuffBlind() ? (StageObject) null : this.actionTarget, node, offsetPos, offsetRot);
        }
      }
    }
  }

  public void OnDestroyLaser(AttackNWayLaser delLaser)
  {
    if (!this.activeAttackLaserList.Contains(delLaser))
      return;
    this.activeAttackLaserList.Remove(delLaser);
  }

  private void ClearLaser()
  {
    if (this.activeAttackLaserList.IsNullOrEmpty<AttackNWayLaser>())
      return;
    for (int index = 0; index < this.activeAttackLaserList.Count; ++index)
      this.activeAttackLaserList[index].RequestDestroy();
  }

  protected override void EventNWayLaserAttack(AnimEventData.EventData data)
  {
    string stringArg1 = data.stringArgs[0];
    string stringArg2 = data.stringArgs[1];
    int intArg = data.intArgs[0];
    Transform parentTrans = Utility.Find(this._transform, stringArg2);
    if (Object.op_Equality((Object) parentTrans, (Object) null))
      return;
    AttackInfo attackInfo = this.FindAttackInfo(stringArg1);
    if (attackInfo == null)
      return;
    BulletData bulletData = attackInfo.bulletData;
    if (Object.op_Equality((Object) bulletData, (Object) null) || bulletData.dataLaser == null)
      return;
    AttackNWayLaser attackNwayLaser = new GameObject("AttackNWayLaser").AddComponent<AttackNWayLaser>();
    attackNwayLaser.Initialize((StageObject) this, parentTrans, attackInfo, intArg);
    this.activeAttackLaserList.Add(attackNwayLaser);
  }

  protected override void EventShotPresent(AnimEventData.EventData data)
  {
    if (data.stringArgs.Length == 0)
      Log.Error(LOG.INGAME, "Not set bullet name. Please check AnimEvent 'SHOT_PRESENT'.");
    else if (data.floatArgs.Length == 0)
      Log.Error(LOG.INGAME, "Not set Range. Please check AnimEvent 'SHOT_PRESENT'.");
    else if (this.cachedBulletDataTable == null)
    {
      Log.Error(LOG.INGAME, "Not set bullet data. Please check bullet data exist.");
    }
    else
    {
      List<string> self = new List<string>();
      int index1 = 0;
      for (int length = data.stringArgs.Length; index1 < length; ++index1)
      {
        if (!string.IsNullOrEmpty(data.stringArgs[index1]))
        {
          string[] source = data.stringArgs[index1].Split(':');
          self.AddRange((IEnumerable<string>) ((IEnumerable<string>) source).ToList<string>());
        }
      }
      if (self.IsNullOrEmpty<string>())
        return;
      int index2 = 0;
      for (int count = self.Count; index2 < count; ++index2)
      {
        string str1 = self[index2];
        if (!string.IsNullOrEmpty(str1))
        {
          BulletData bulletData = this.cachedBulletDataTable.Get(str1);
          if (!Object.op_Equality((Object) bulletData, (Object) null))
          {
            BulletData.BulletPresent dataPresent = bulletData.dataPresent;
            if (dataPresent != null)
            {
              int presentBulletId = 0;
              int num1 = this.id;
              string str2 = num1.ToString();
              num1 = MonoBehaviourSingleton<StageObjectManager>.I.presentBulletObjIndex + 1;
              string str3 = num1.ToString();
              string s = str2 + str3;
              float floatArg = data.floatArgs[0];
              float num2 = 360f / (float) (count - 1) * (float) index2;
              ref int local = ref presentBulletId;
              if (int.TryParse(s, out local))
              {
                Vector3 position = Vector3.op_Addition(this._transform.position, Vector3.op_Multiply(Quaternion.op_Multiply(Quaternion.op_Multiply(this._transform.localRotation, Quaternion.AngleAxis(num2, Vector3.up)), this._transform.forward), floatArg));
                if (index2 == 0)
                  position = this._transform.position;
                this.SetPresentBullet(presentBulletId, dataPresent.type, position, str1);
                if (Object.op_Inequality((Object) this.playerSender, (Object) null))
                  this.playerSender.OnSetPresentBullet(presentBulletId, dataPresent.type, position, str1);
              }
            }
          }
        }
      }
    }
  }

  public void SetPresentBullet(
    int presentBulletId,
    BulletData.BulletPresent.TYPE type,
    Vector3 position,
    string bulletName)
  {
    BulletData bulletData = this.cachedBulletDataTable.Get(bulletName);
    if (Object.op_Equality((Object) bulletData, (Object) null))
      return;
    GameObject gameObject = new GameObject();
    IPresentBulletObject presentBulletObject = (IPresentBulletObject) null;
    if (type != BulletData.BulletPresent.TYPE.HEAL)
    {
      if (type == BulletData.BulletPresent.TYPE.BOMB)
        ;
    }
    else
      presentBulletObject = (IPresentBulletObject) gameObject.AddComponent<PresentBulletObject>();
    if (presentBulletObject == null)
      return;
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
    {
      Object.Destroy((Object) gameObject);
    }
    else
    {
      presentBulletObject.Initialize(presentBulletId, bulletData, this._transform);
      presentBulletObject.SetPosition(position);
      presentBulletObject.SetSkillParam(this.skillInfo.actSkillParam);
      if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
        return;
      MonoBehaviourSingleton<StageObjectManager>.I.presentBulletObjList.Add(presentBulletObject);
      ++MonoBehaviourSingleton<StageObjectManager>.I.presentBulletObjIndex;
    }
  }

  public void DestroyPresentBulletObject(int presentBulletId)
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    List<IPresentBulletObject> presentBulletObjList = MonoBehaviourSingleton<StageObjectManager>.I.presentBulletObjList;
    if (presentBulletObjList == null || presentBulletObjList.Count <= 0)
      return;
    for (int index = 0; index < presentBulletObjList.Count; ++index)
    {
      if (presentBulletObjList[index].GetPresentBulletId() == presentBulletId)
      {
        presentBulletObjList[index].OnPicked();
        break;
      }
    }
    MonoBehaviourSingleton<StageObjectManager>.I.RemovePresentBulletObject(presentBulletId);
  }

  protected override void EventShotZone(AnimEventData.EventData data)
  {
    if (data.stringArgs.Length == 0)
      Log.Error(LOG.INGAME, "Not set bullet name. Please check AnimEvent 'SHOT_ZONE'.");
    else if (this.cachedBulletDataTable == null)
    {
      Log.Error(LOG.INGAME, "Not set bullet data. Please check bullet data exist.");
    }
    else
    {
      string stringArg = data.stringArgs[0];
      if (string.IsNullOrEmpty(stringArg))
        return;
      BulletData bulletData = this.cachedBulletDataTable.Get(stringArg);
      if (bulletData == null || bulletData.dataZone == null)
        return;
      this.ShotZoneBullet(this, stringArg, this._transform.position, MonoBehaviourSingleton<StageObjectManager>.I.ExistsEnemyValiedHealAttack(), true);
      if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
        return;
      this.playerSender.OnShotZoneBullet(stringArg, this._transform.position);
    }
  }

  public void ShotZoneBullet(
    Player onwerPlayer,
    string bulletName,
    Vector3 position,
    bool isHealDamgeEnemy = false,
    bool isOwner = false)
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid() || this.skillInfo.actSkillParam == null || this.skillInfo.actSkillParam.tableData == null)
      return;
    BulletData bullet = this.cachedBulletDataTable.Get(bulletName);
    if (bullet == null || bullet.dataZone == null)
      return;
    GameObject gameObject = new GameObject();
    ZoneBulletObject zoneBulletObject = (ZoneBulletObject) null;
    if (bullet.dataZone.type == BulletData.BulletZone.TYPE.HEAL)
      zoneBulletObject = gameObject.AddComponent<ZoneBulletObject>();
    else
      isHealDamgeEnemy = false;
    zoneBulletObject?.Initialize(onwerPlayer, bullet, position, this.skillInfo.actSkillParam, isHealDamgeEnemy, isOwner);
  }

  public override void EventShotDecoy(AnimEventData.EventData data)
  {
    if (data.stringArgs.Length == 0)
      Log.Error(LOG.INGAME, "Not set bullet name. Please check AnimEvent 'SHOT_DECOY'.");
    else if (this.cachedBulletDataTable == null)
    {
      Log.Error(LOG.INGAME, "Not set bullet data. Please check bullet data exist.");
    }
    else
    {
      string stringArg = data.stringArgs[0];
      if (string.IsNullOrEmpty(stringArg))
        return;
      BulletData bulletData = this.cachedBulletDataTable.Get(stringArg);
      if (bulletData == null || !bulletData.IsDecoy())
        return;
      int decoyId = this.id * 10 + this.localDecoyId;
      if (++this.localDecoyId >= 10)
        this.localDecoyId = 0;
      Vector3 position = this._transform.position;
      if (data.floatArgs != null && data.floatArgs.Length == 3)
        ((Vector3) ref position).Set(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
      int skIndex = -1;
      if (data.intArgs != null && data.intArgs.Length == 1)
        skIndex = data.intArgs[0];
      else if (this.skillInfo != null)
        skIndex = this.skillInfo.skillIndex;
      this.ShotDecoyBullet(this.id, skIndex, decoyId, stringArg, position, true);
      if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
        return;
      this.playerSender.OnShotDecoyBullet(skIndex, decoyId, stringArg, position);
    }
  }

  public void ShotDecoyBullet(
    int playerId,
    int skIndex,
    int decoyId,
    string bulletName,
    Vector3 position,
    bool isHit)
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    BulletData bullet = this.cachedBulletDataTable.Get(bulletName);
    if (bullet == null)
      return;
    DecoyBulletObject decoyObject = this.CreateDecoyObject(bullet);
    if (decoyObject == null)
      return;
    decoyObject.Initialize(playerId, decoyId, bullet, position, this.skillInfo.GetSkillParam(skIndex), isHit);
    Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
    if (boss == null || !boss.IsOriginal() && !boss.IsCoopNone())
      return;
    decoyObject.HateCtrl();
  }

  public DecoyBulletObject CreateDecoyObject(BulletData bullet)
  {
    switch (bullet.type)
    {
      case BulletData.BULLET_TYPE.DECOY:
        if (bullet.dataDecoy != null)
          return new GameObject().AddComponent<DecoyBulletObject>();
        break;
      case BulletData.BULLET_TYPE.DECOY_TURRET_BIT:
        if (bullet.dataDecoyTurretBit != null)
          return (DecoyBulletObject) new GameObject().AddComponent<DecoyTurretBitBulletObject>();
        break;
    }
    return (DecoyBulletObject) null;
  }

  public void ExecExplodeDecoyBullet(int decoyId)
  {
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnExplodeDecoyBullet(decoyId);
  }

  public void ExplodeDecoyBullet(int decoyId)
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    StageObject decoy = MonoBehaviourSingleton<StageObjectManager>.I.FindDecoy(decoyId);
    if (decoy == null || !(decoy is DecoyBulletObject decoyBulletObject))
      return;
    decoyBulletObject.OnDisappear(false);
  }

  private void EventPairSwordsShotBullet(AnimEventData.EventData data)
  {
    if (this.pairSwordsCtrl == null)
    {
      Log.Error("EventPairSwordsShotBullet. pairSwordsCtrl is null!!");
    }
    else
    {
      string stringArg1 = data.stringArgs[0];
      string stringArg2 = data.stringArgs[1];
      Transform node = this.FindNode(stringArg2);
      if (Object.op_Equality((Object) node, (Object) null))
      {
        Log.Error("EventPairSwordsShotBullet. Not found transform for launch!! name:" + stringArg2);
      }
      else
      {
        AttackInfo attackInfo = this.FindAttackInfo(stringArg1);
        if (attackInfo == null)
        {
          Log.Error("EventPairSwordsShotBullet. Not found AttackInfo!! name:" + stringArg1);
        }
        else
        {
          BulletData bulletData = attackInfo.bulletData;
          if (Object.op_Equality((Object) bulletData, (Object) null) || bulletData.dataPairSwordsSoul == null)
          {
            Log.Error("EventPairSwordsShotBullet. Not found BulletData!! atkInfoName:" + stringArg1);
          }
          else
          {
            string change_effect = (string) null;
            if (!((IList<string>) this.playerParameter.pairSwordsActionInfo.Soul_EffectsForBullet).IsNullOrEmpty<string>())
            {
              int nowWeaponElement = (int) this.GetNowWeaponElement();
              if (this.playerParameter.pairSwordsActionInfo.Soul_EffectsForBullet.Length >= nowWeaponElement)
                change_effect = this.playerParameter.pairSwordsActionInfo.Soul_EffectsForBullet[nowWeaponElement];
            }
            Vector3 vector3_1;
            // ISSUE: explicit constructor call
            ((Vector3) ref vector3_1).\u002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
            Quaternion quaternion = Quaternion.Euler(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
            Vector3 vector3_2 = Vector3.op_Addition(node.position, Quaternion.op_Multiply(Quaternion.LookRotation(this._forward), vector3_1));
            Quaternion rot = Quaternion.op_Multiply(Quaternion.LookRotation(this.GetBulletShotVecPositiveY(vector3_2)), quaternion);
            AnimEventShot.Create((StageObject) this, attackInfo, vector3_2, rot, change_effect: change_effect);
          }
        }
      }
    }
  }

  private void EventPairSwordsShotLaser(AnimEventData.EventData data)
  {
    string stringArg1 = data.stringArgs[0];
    string stringArg2 = data.stringArgs[1];
    Transform node = this.FindNode(stringArg2);
    if (Object.op_Equality((Object) node, (Object) null))
    {
      Log.Error("EventPairSwordsShotLaser. Not found transform for launch!! name:" + stringArg2);
    }
    else
    {
      int comboLv = this.pairSwordsCtrl.GetComboLv();
      string[] forLaserByComboLv = this.playerParameter.pairSwordsActionInfo.Soul_AttackInfoNamesForLaserByComboLv;
      int[] ofLaserByComboLv = this.playerParameter.pairSwordsActionInfo.Soul_NumOfLaserByComboLv;
      if (((IList<string>) forLaserByComboLv).IsNullOrEmpty<string>() || forLaserByComboLv.Length < comboLv || ((IList<int>) ofLaserByComboLv).IsNullOrEmpty<int>() || ofLaserByComboLv.Length < comboLv)
        return;
      string str = forLaserByComboLv[comboLv - 1];
      if (str.IsNullOrWhiteSpace())
        return;
      AttackInfo attackInfo = this.FindAttackInfo(str);
      if (attackInfo == null)
        Log.Error("EventPairSwordsShotLaser. Not found AttackInfo!! name:" + str);
      else if (Object.op_Equality((Object) attackInfo.bulletData, (Object) null))
      {
        Log.Error("EventPairSwordsShotLaser. Not found BulletData!! atkInfoName:" + str);
      }
      else
      {
        Vector3 vector3_1;
        // ISSUE: explicit constructor call
        ((Vector3) ref vector3_1).\u002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
        Quaternion quaternion = Quaternion.Euler(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
        int num1 = ofLaserByComboLv[comboLv - 1];
        Vector3 shotVecForSpWeak = this.GetBulletShotVecForSpWeak(node.position);
        for (int index = 1; index <= num1; ++index)
        {
          Vector3 zero = Vector3.zero;
          Vector3 vector3_2;
          if (num1 == 1)
          {
            vector3_2 = Vector3.op_Addition(node.position, Quaternion.op_Multiply(Quaternion.LookRotation(this._forward), vector3_1));
            shotVecForSpWeak = this.GetBulletShotVecForSpWeak(vector3_2);
          }
          else
          {
            float num2 = (float) (360 / num1 * index) * ((float) Math.PI / 180f);
            vector3_2 = Vector3.op_Addition(node.position, Quaternion.op_Multiply(Quaternion.LookRotation(this._forward), Vector3.op_Addition(vector3_1, Vector3.op_Multiply(new Vector3(Mathf.Cos(num2), Mathf.Sin(num2), 0.0f), this.playerParameter.pairSwordsActionInfo.Soul_RadiusForLaser))));
          }
          Quaternion rot = Quaternion.op_Multiply(Quaternion.LookRotation(shotVecForSpWeak), quaternion);
          AnimEventShot bullet = AnimEventShot.Create((StageObject) this, attackInfo, vector3_2, rot);
          this.pairSwordsCtrl.AddBulletLaser(bullet);
          bullet._transform.parent = this._transform;
          Vector3 vector3_3 = shotVecForSpWeak;
          vector3_3.y = 0.0f;
          this._rotation = Quaternion.LookRotation(vector3_3);
        }
        this.StartCoroutine(this.PlayPairSwordsShotLaserSE());
        this.pairSwordsCtrl.SetGaugePercentForLaser();
        this.pairSwordsCtrl.SetEventShotLaserExec();
        this.StartWaitingPacket(StageObject.WAITING_PACKET.PLAYER_PAIR_SWORDS_LASER_END, true);
      }
    }
  }

  private IEnumerator PlayPairSwordsShotLaserSE()
  {
    int[] seIds = this.playerParameter.pairSwordsActionInfo.Soul_SeIds;
    if (!((IList<int>) seIds).IsNullOrEmpty<int>() && seIds.Length >= 2)
    {
      if (seIds[0] > 0)
        SoundManager.PlayOneShotSE(seIds[0], this._position);
      yield return (object) new WaitForSeconds(this.playerParameter.pairSwordsActionInfo.Soul_TimeForPlayLoopSE);
      if (seIds[1] > 0)
        SoundManager.PlayLoopSE(seIds[1], (DisableNotifyMonoBehaviour) this, this._transform);
    }
  }

  private void EventShotHealingHoming(AnimEventData.EventData data)
  {
    if (!this.IsOriginal() && !this.IsCoopNone())
      return;
    Coop_Model_PlayerShotHealingHoming model = new Coop_Model_PlayerShotHealingHoming();
    model.id = this.id;
    model.atkInfoName = data.stringArgs[0];
    model.launchNodeName = data.stringArgs[1];
    model.offsetPos = new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
    model.offsetRot = new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
    model.targetNum = data.intArgs[0];
    model.targetPlayerIDs = this.GetSortedArrayByPlayerDistance(model.targetNum);
    this.OnShotHealingHoming(model);
  }

  public void OnShotHealingHoming(Coop_Model_PlayerShotHealingHoming model)
  {
    Transform node = this.FindNode(model.launchNodeName);
    if (Object.op_Equality((Object) node, (Object) null))
      return;
    AttackInfo attackInfo = this.FindAttackInfo(model.atkInfoName);
    if (attackInfo == null)
      return;
    BulletData bulletData = attackInfo.bulletData;
    if (Object.op_Equality((Object) bulletData, (Object) null) || bulletData.dataHealingHomingBullet == null || ((IList<int>) model.targetPlayerIDs).IsNullOrEmpty<int>() || !MonoBehaviourSingleton<StageObjectManager>.IsValid() || model.targetNum <= 0)
      return;
    Vector3 pos = Vector3.op_Addition(node.position, Quaternion.op_Multiply(Quaternion.LookRotation(this._forward), model.offsetPos));
    Quaternion rot = Quaternion.op_Multiply(Quaternion.LookRotation(this._forward), Quaternion.Euler(model.offsetRot));
    int num = Mathf.Min(model.targetNum, model.targetPlayerIDs.Length);
    for (int index = 0; index < num; ++index)
    {
      AnimEventShot animEventShot = AnimEventShot.Create((StageObject) this, attackInfo, pos, rot);
      if (bulletData.dataHoming != null || bulletData.dataHealingHomingBullet != null)
        animEventShot.SetTarget(MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(model.targetPlayerIDs[index]));
    }
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnShotHealingHoming(model);
  }

  private void EventShotResurrectionHoming(AnimEventData.EventData data)
  {
    if (!this.IsOriginal() && !this.IsCoopNone())
      return;
    Coop_Model_PlayerShotResurrectionHoming model = new Coop_Model_PlayerShotResurrectionHoming();
    model.id = this.id;
    model.atkInfoName = data.stringArgs[0];
    model.launchNodeName = data.stringArgs[1];
    model.offsetPos = new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
    model.offsetRot = new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
    model.targetNum = data.intArgs[0];
    model.targetPlayerIDs = this.GetSortedArrayByPlayerDistance(model.targetNum);
    this.OnShotResurrectionHoming(model);
  }

  public void OnShotResurrectionHoming(Coop_Model_PlayerShotResurrectionHoming model)
  {
    Transform node = this.FindNode(model.launchNodeName);
    if (Object.op_Equality((Object) node, (Object) null))
      return;
    AttackInfo attackInfo = this.FindAttackInfo(model.atkInfoName);
    if (attackInfo == null)
      return;
    BulletData bulletData = attackInfo.bulletData;
    if (Object.op_Equality((Object) bulletData, (Object) null) || bulletData.dataResurrectionHomingBullet == null || ((IList<int>) model.targetPlayerIDs).IsNullOrEmpty<int>() || !MonoBehaviourSingleton<StageObjectManager>.IsValid() || model.targetNum <= 0)
      return;
    Vector3 pos = Vector3.op_Addition(node.position, Quaternion.op_Multiply(Quaternion.LookRotation(this._forward), model.offsetPos));
    Quaternion rot = Quaternion.op_Multiply(Quaternion.LookRotation(this._forward), Quaternion.Euler(model.offsetRot));
    int num = Mathf.Min(model.targetNum, model.targetPlayerIDs.Length);
    for (int index = 0; index < num; ++index)
    {
      AnimEventShot sht = AnimEventShot.Create((StageObject) this, attackInfo, pos, rot);
      if (bulletData.dataHoming != null || bulletData.dataResurrectionHomingBullet != null)
      {
        Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(model.targetPlayerIDs[index]) as Player;
        if (Object.op_Inequality((Object) player, (Object) null))
          player.SetResurrectionHomingTarget(sht);
      }
    }
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnShotResurrectionHoming(model);
  }

  public void SetResurrectionHomingTarget(AnimEventShot sht)
  {
    if (Object.op_Equality((Object) sht, (Object) null))
      return;
    sht.SetTarget((StageObject) this);
    this.isWaitingResurrectionHoming = true;
    if (!this.IsCoopNone() && !this.IsOriginal())
      return;
    this.DeadCount(this.rescueTime, this.IsPrayed() || this.isStopCounter || this.isWaitingResurrectionHoming);
  }

  private int[] GetSortedArrayByPlayerDistance(int targetNum)
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return (int[]) null;
    List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
    if (playerList.IsNullOrEmpty<StageObject>())
      return (int[]) null;
    List<StageObject> stageObjectList = new List<StageObject>((IEnumerable<StageObject>) playerList);
    stageObjectList.Remove((StageObject) this);
    Vector3 targetPos = this._position;
    stageObjectList.Sort((Comparison<StageObject>) ((a, b) =>
    {
      Vector3 vector3 = Vector3.op_Subtraction(((Component) a).transform.position, targetPos);
      double magnitude1 = (double) ((Vector3) ref vector3).magnitude;
      vector3 = Vector3.op_Subtraction(((Component) b).transform.position, targetPos);
      double magnitude2 = (double) ((Vector3) ref vector3).magnitude;
      return Mathf.RoundToInt((float) (magnitude1 - magnitude2));
    }));
    int count = targetNum;
    if (count <= 0 || stageObjectList.Count <= count)
      count = stageObjectList.Count;
    return stageObjectList.GetRange(0, count).Select<StageObject, int>((Func<StageObject, int>) (o => o.id)).ToArray<int>();
  }

  private void EventHealHp()
  {
  }

  public virtual void ShotArrow(
    Vector3 shot_pos,
    Quaternion shot_rot,
    AttackInfo attack_info,
    bool isSitShot,
    bool isAimEnd,
    bool isSend = true)
  {
    if (attack_info == null)
      return;
    GameObject attach_object = ResourceUtility.Instantiate<GameObject>(MonoBehaviourSingleton<InGameLinkResourcesCommon>.I.arrow);
    bool isArrowRain = isSitShot && this.spAttackType == SP_ATTACK_TYPE.BURST;
    string change_effect = (string) null;
    if ((double) attack_info.rateInfoRate >= 1.0)
    {
      if (this.spAttackType == SP_ATTACK_TYPE.BURST && this.isBoostMode && !isArrowRain)
        change_effect = this.playerParameter.arrowActionInfo.boostArrowChargeMaxEffectName;
      else if (this.isArrowAimBossMode && !isArrowRain)
        change_effect = this.playerParameter.specialActionInfo.arrowChargeAimEffectName;
    }
    bool isBossPierce = false;
    bool characterHitDelete = attack_info.bulletData.data.isCharacterHitDelete;
    float num = -1f;
    if (attack_info.bulletData.dataFall != null)
      num = attack_info.bulletData.dataFall.gravityStartTime;
    bool isAim = this.isArrowAimBossMode || this.spAttackType == SP_ATTACK_TYPE.BURST;
    if (1.0 <= (double) this.chargeRate)
    {
      if (Object.op_Inequality((Object) this.bossBrain, (Object) null))
      {
        if (!isSitShot && (this.spAttackType == SP_ATTACK_TYPE.HEAT || this.spAttackType == SP_ATTACK_TYPE.BURST))
        {
          attack_info.bulletData.data.isCharacterHitDelete = false;
          if ((double) num != -1.0)
            attack_info.bulletData.dataFall.gravityStartTime = -1f;
          isBossPierce = true;
        }
        if (isSitShot && this.spAttackType == SP_ATTACK_TYPE.BURST && !this.isBoostMode)
          isBossPierce = true;
      }
      else
        attack_info.bulletData.data.isCharacterHitDelete = false;
    }
    EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint) this.weaponData.eId);
    DamageDistanceTable.DamageDistanceData damageDistanceData = (DamageDistanceTable.DamageDistanceData) null;
    if (!isArrowRain)
      damageDistanceData = Singleton<DamageDistanceTable>.I.GetData((uint) equipItemData.damageDistanceId);
    AnimEventShot.CreateArrow((StageObject) this, attack_info, shot_pos, shot_rot, attach_object, true, change_effect, damageDistanceData).SetArrowInfo(isAim, isBossPierce, isArrowRain);
    attack_info.bulletData.data.isCharacterHitDelete = characterHitDelete;
    if ((double) num != -1.0)
      attack_info.bulletData.dataFall.gravityStartTime = num;
    this.isArrowAimEnd = isAimEnd;
    if (!(Object.op_Inequality((Object) this.playerSender, (Object) null) & isSend))
      return;
    this.playerSender.OnShotArrow(shot_pos, shot_rot, attack_info, isSitShot, isAimEnd);
  }

  public int GetSoulArrowLockNum()
  {
    InGameSettingsManager.Player.ArrowActionInfo arrowActionInfo = this.playerParameter.arrowActionInfo;
    return (this.isBoostMode ? arrowActionInfo.soulBoostLockMax : arrowActionInfo.soulLockMax) + this.buffParam.passive.addSoulArrowLockCount;
  }

  public int GetSoulArrowNormalLockNum()
  {
    return this.playerParameter.arrowActionInfo.soulLockMax + this.buffParam.passive.addSoulArrowLockCount;
  }

  public int GetSoulArrowBoostLockNum()
  {
    return this.playerParameter.arrowActionInfo.soulBoostLockMax + this.buffParam.passive.addSoulArrowLockCount;
  }

  public virtual void ShotSoulArrow()
  {
    List<TargetMarker> targetMarkerList = MonoBehaviourSingleton<TargetMarkerManager>.I.GetTargetMarkerList();
    if (targetMarkerList == null || targetMarkerList.Count <= 0)
      return;
    InGameSettingsManager.Player.ArrowActionInfo arrowActionInfo = this.playerParameter.arrowActionInfo;
    Vector3 shotPos = this.GetBulletAppearPos();
    Vector3 bulletShotVec = this.GetBulletShotVec(shotPos);
    shotPos = Vector3.op_Subtraction(shotPos, Vector3.op_Multiply(bulletShotVec, arrowActionInfo.soulShotPosVec));
    Quaternion bowRot = Quaternion.LookRotation(bulletShotVec);
    List<Vector3> targetPosList = new List<Vector3>();
    List<Player.SoulArrowInfo> saInfo = new List<Player.SoulArrowInfo>();
    this.GetSoulArrowLockNum();
    for (int index1 = 0; index1 < targetMarkerList.Count; ++index1)
    {
      TargetMarker targetMarker = targetMarkerList[index1];
      if (targetMarker != null)
      {
        List<int> multiLockOrder = targetMarker.GetMultiLockOrder();
        if (multiLockOrder != null)
        {
          int count = multiLockOrder.Count;
          if (count != 0)
          {
            bool flag = count == arrowActionInfo.soulLockRegionMax;
            float num1 = arrowActionInfo.soulLockNumAtkRate[count - 1];
            for (int index2 = 0; index2 < count; ++index2)
            {
              AttackHitInfo attackInfo = (AttackHitInfo) this.FindAttackInfo(arrowActionInfo.attackInfoNames[2], isDuplicate: true);
              if (attackInfo != null)
              {
                int num2 = multiLockOrder[index2];
                float num3 = arrowActionInfo.soulLockOrderAtkRateBase + arrowActionInfo.soulLockOrderAtkRateCoefficient * (float) (num2 - 1);
                attackInfo.atkRate = num1 * num3;
                attackInfo.toEnemy.isSpecialAttack = flag;
                attackInfo.damageNumAddGroup = index2;
                attackInfo.dontIncreaseGauge = this.isBoostMode;
                saInfo.Add(new Player.SoulArrowInfo()
                {
                  attackInfo = attackInfo,
                  point = targetMarker.point
                });
                targetPosList.Add(targetMarker.point.GetTargetPoint());
              }
            }
          }
        }
      }
    }
    this.StartCoroutine(this._ShotSoulArrow(shotPos, bowRot, saInfo, arrowActionInfo.soulShotInterval, (System.Action) (() =>
    {
      this.SetNextTrigger();
      this.isArrowAimEnd = true;
      if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
        return;
      this.playerSender.OnShotSoulArrow(shotPos, bowRot, targetPosList);
    })));
  }

  private IEnumerator _ShotSoulArrow(
    Vector3 shotPos,
    Quaternion bowRot,
    List<Player.SoulArrowInfo> saInfo,
    float interval,
    System.Action endCallback)
  {
    for (int i = 0; i < saInfo.Count; ++i)
    {
      Player.SoulArrowInfo soulArrowInfo = saInfo[i];
      GameObject attach_object = ResourceUtility.Instantiate<GameObject>(MonoBehaviourSingleton<InGameLinkResourcesCommon>.I.arrow);
      AnimEventShot arrow = AnimEventShot.CreateArrow((StageObject) this, (AttackInfo) soulArrowInfo.attackInfo, shotPos, bowRot, attach_object, true, "", (DamageDistanceTable.DamageDistanceData) null);
      arrow.SetArrowInfo(this.isArrowAimBossMode, false);
      arrow.SetTarget(soulArrowInfo.point);
      yield return (object) new WaitForSeconds(interval);
    }
    endCallback();
  }

  public void ShotSoulArrowPuppet(Vector3 shotPos, Quaternion bowRot, List<Vector3> targetPosList)
  {
    AttackInfo attackInfo = this.FindAttackInfo(this.playerParameter.arrowActionInfo.attackInfoNames[2]);
    for (int index = 0; index < targetPosList.Count; ++index)
    {
      GameObject attach_object = ResourceUtility.Instantiate<GameObject>(MonoBehaviourSingleton<InGameLinkResourcesCommon>.I.arrow);
      AnimEventShot arrow = AnimEventShot.CreateArrow((StageObject) this, attackInfo, shotPos, bowRot, attach_object, true, "", (DamageDistanceTable.DamageDistanceData) null);
      arrow.SetArrowInfo(this.isArrowAimBossMode, false);
      arrow.SetPuppetTargetPos(targetPosList[index]);
    }
    this.SetNextTrigger();
  }

  public virtual Vector3 GetBulletAppearPos()
  {
    if (!Object.op_Equality((Object) this.loader.socketWepR, (Object) null))
      return this.loader.socketWepR.position;
    Bounds bounds = this._collider.bounds;
    return Vector3.op_Addition(((Bounds) ref bounds).center, Vector3.op_Multiply(Vector3.up, 0.3f));
  }

  public virtual Vector3 GetBulletShotVec(Vector3 appear_pos)
  {
    Vector3 vector3 = this._forward;
    Vector3 pos;
    if (!this.IsValidBuffBlind() && this.GetTargetPos(out pos))
      vector3 = Vector3.op_Subtraction(pos, appear_pos);
    return ((Vector3) ref vector3).normalized;
  }

  public Vector3 GetBulletShotVecPositiveY(Vector3 appearPos)
  {
    Vector3 vector3 = this._forward;
    Vector3 pos;
    if (!this.IsValidBuffBlind() && this.GetTargetPos(out pos))
    {
      if ((double) pos.y < 0.0)
        pos.y = 0.0f;
      vector3 = Vector3.op_Subtraction(pos, appearPos);
    }
    return ((Vector3) ref vector3).normalized;
  }

  public Vector3 GetBulletShotVecForSpWeak(Vector3 appearPos)
  {
    Vector3 vector3 = this.GetBulletShotVec(appearPos);
    if (!this.IsValidBuffBlind())
    {
      if (Vector3.op_Inequality(this.targetPointPos, Vector3.zero))
        vector3 = Vector3.op_Subtraction(this.targetPointPos, appearPos);
      if (Object.op_Inequality((Object) this.targetPointWithSpWeak, (Object) null))
        vector3 = Vector3.op_Subtraction(this.targetPointWithSpWeak.param.markerPos, appearPos);
    }
    return ((Vector3) ref vector3).normalized;
  }

  public void SetArrowAimKeep()
  {
    this.isArrowAimKeep = this.isArrowAimBossMode || this.isArrowAimLesserMode;
  }

  public virtual void SetArrowAimBossMode(bool enable)
  {
    this.isArrowAimBossMode = enable;
    this.SetArrowAimBossVisible(enable);
  }

  protected virtual void SetArrowAimBossVisible(bool enable)
  {
  }

  public virtual void SetArrowAimLesserMode(bool enable)
  {
    this.isArrowAimLesserMode = enable;
    this.SetArrowAimLesserVisible(enable);
  }

  protected virtual void SetArrowAimLesserVisible(bool enable)
  {
  }

  public virtual void UpdateArrowAimLesserMode(Vector2 input_vec)
  {
  }

  protected void UpdateArrowAngle()
  {
    if (!Object.op_Inequality((Object) this.animator, (Object) null) || this.attackMode != Player.ATTACK_MODE.ARROW)
      return;
    Vector3 bulletShotVec = this.GetBulletShotVec(this.GetBulletAppearPos());
    if (!Vector3.op_Inequality(bulletShotVec, Vector3.zero))
      return;
    Quaternion quaternion = Quaternion.LookRotation(bulletShotVec);
    float x = ((Quaternion) ref quaternion).eulerAngles.x;
    if ((double) x >= 180.0)
      x -= 360f;
    float num = -x;
    this.animator.SetFloat(Player.arrowAngleID, num);
  }

  public virtual void SetCannonAimMode()
  {
  }

  public virtual void SetCannonBeamMode()
  {
  }

  public virtual void UpdateCannonAimMode(Vector2 inputVec, Vector2 inputPos)
  {
  }

  public override float GetAttackInfoRate()
  {
    float chargeRate = this.chargeRate;
    this.spearCtrl.GetSpinRate(ref chargeRate);
    return chargeRate;
  }

  public override AttackInfo FindAttackInfoExternal(string name, bool fix_rate, float rate)
  {
    return this._FindAttackInfo(this.playerParameter.attackInfosAll, name, fix_rate, rate, false);
  }

  protected override AttackInfo _FindAttackInfo(
    AttackInfo[] attack_infos,
    string name,
    bool fix_rate,
    float rate,
    bool isDuplicate = false)
  {
    if (string.IsNullOrEmpty(name))
      return (AttackInfo) null;
    if (attack_infos == null)
      return (AttackInfo) null;
    if (name[0] == '@' && name.StartsWith("@skill_"))
    {
      int length = "@skill_".Length;
      int index = int.Parse(name.Substring(length, name.Length - length));
      SkillInfo.SkillParam actSkillParam = this.skillInfo.actSkillParam;
      if (actSkillParam == null || actSkillParam.tableData == null)
      {
        Log.Error(LOG.INGAME, "FindAttackInfo skill param none.");
        return (AttackInfo) null;
      }
      string[] attackInfoNames = actSkillParam.tableData.attackInfoNames;
      if (index < 0 || index >= attackInfoNames.Length)
      {
        Log.Error(LOG.INGAME, "FindAttackInfo skill param out of range.");
        return (AttackInfo) null;
      }
      name = attackInfoNames[index];
    }
    return base._FindAttackInfo(attack_infos, name, fix_rate, rate, isDuplicate);
  }

  protected override bool GetTargetPos(out Vector3 pos)
  {
    pos = Vector3.zero;
    bool targetPos = false;
    if (Object.op_Inequality((Object) this.targetingPoint, (Object) null))
    {
      pos = this.targetingPoint.param.targetPos;
      targetPos = true;
    }
    return targetPos;
  }

  public override void ChatSay(int chatID)
  {
    if (Object.op_Inequality((Object) this.uiPlayerStatusGizmo, (Object) null))
      this.uiPlayerStatusGizmo.SayChat(chatID);
    base.ChatSay(chatID);
  }

  public override void ChatSay(string message)
  {
    if (Object.op_Inequality((Object) this.uiPlayerStatusGizmo, (Object) null) && ((Component) this.uiPlayerStatusGizmo).gameObject.activeInHierarchy)
      this.uiPlayerStatusGizmo.SayChat(message);
    this.OnSendChatMessage(message);
    base.ChatSay(message);
  }

  public override void ChatSayStamp(int stamp_id)
  {
    if (Object.op_Inequality((Object) this.uiPlayerStatusGizmo, (Object) null) && ((Component) this.uiPlayerStatusGizmo).gameObject.activeInHierarchy)
      this.uiPlayerStatusGizmo.SayChatStamp(stamp_id);
    this.OnSendChatStamp(stamp_id);
    base.ChatSayStamp(stamp_id);
  }

  protected virtual void OnSendChatMessage(string message)
  {
    if (!MonoBehaviourSingleton<UIInGameMessageBar>.IsValid() || !((Behaviour) MonoBehaviourSingleton<UIInGameMessageBar>.I).isActiveAndEnabled)
      return;
    MonoBehaviourSingleton<UIInGameMessageBar>.I.Announce(this.charaName, message);
  }

  protected virtual void OnSendChatStamp(int stamp_id)
  {
    if (!MonoBehaviourSingleton<UIInGameMessageBar>.IsValid() || !((Behaviour) MonoBehaviourSingleton<UIInGameMessageBar>.I).isActiveAndEnabled)
      return;
    MonoBehaviourSingleton<UIInGameMessageBar>.I.Announce(this.charaName, stamp_id);
  }

  public override SkillInfo.SkillParam GetSkillParam(int index)
  {
    return this.skillInfo.GetSkillParam(index);
  }

  public override void OnFailedWaitingPacket(StageObject.WAITING_PACKET type)
  {
    switch (type)
    {
      case StageObject.WAITING_PACKET.PLAYER_CHARGE_RELEASE:
        this.ActIdle(false, -1f);
        break;
      case StageObject.WAITING_PACKET.PLAYER_PRAYER_END:
        int index = 0;
        for (int count = this.prayTargetInfos.Count; index < count; ++index)
        {
          Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(this.prayTargetInfos[index].targetId) as Player;
          if (Object.op_Equality((Object) player, (Object) null))
            return;
          player.EndPrayed(this.id);
        }
        this.prayTargetInfos.Clear();
        this.boostPrayTargetInfoList.Clear();
        this.boostPrayedInfoList.Clear();
        break;
      case StageObject.WAITING_PACKET.PLAYER_JUMP_END:
        this.ActIdle(false, -1f);
        break;
      case StageObject.WAITING_PACKET.PLAYER_SOUL_BOOST:
        this.FinishBoostMode();
        break;
      case StageObject.WAITING_PACKET.EVOLVE:
        this.EndEvolve();
        break;
      case StageObject.WAITING_PACKET.PLAYER_PAIR_SWORDS_LASER_END:
      case StageObject.WAITING_PACKET.PLAYER_ONE_HAND_SWORD_MOVE_END:
      case StageObject.WAITING_PACKET.PLAYER_GATHER_GIMMICK:
        if (this.fishingCtrl == null || !this.fishingCtrl.IsFighting())
        {
          this.ActIdle(false, -1f);
          break;
        }
        break;
    }
    base.OnFailedWaitingPacket(type);
  }

  public void SetAppearPosField()
  {
    if (!FieldManager.IsValidInGame())
      return;
    Vector3 zero = Vector3.zero;
    zero.x = MonoBehaviourSingleton<FieldManager>.I.currentStartMapX;
    zero.z = MonoBehaviourSingleton<FieldManager>.I.currentStartMapZ;
    this._position = zero;
    this._rotation = Quaternion.AngleAxis(MonoBehaviourSingleton<FieldManager>.I.currentStartMapDir, Vector3.up);
    this.SetAppearPos(zero);
  }

  public void SetAppearPosOwner(Vector3 enemy_pos)
  {
    Vector3 vector3 = Vector3.op_Addition(enemy_pos, Vector3.op_Multiply(Vector3.forward, this.playerParameter.appearPosDistance));
    if (MonoBehaviourSingleton<StageManager>.I.CheckPosInside(vector3))
    {
      this._position = vector3;
      this.LookAt(enemy_pos, false);
      this.SetAppearPos(vector3);
    }
    else
      this.SetAppearPosGuest(enemy_pos);
  }

  public void SetAppearPosGuest(Vector3 center_pos)
  {
    this.SetAppearRandomPosFixDistance(center_pos, this.playerParameter.appearPosDistance, this.playerParameter.appearPosTryCount);
    this.LookAt(center_pos, false);
  }

  public virtual void PlayVoice(int voice_id)
  {
    if (!this.EnablePlaySound() || FieldManager.IsValidInTutorial())
      return;
    SoundManager.PlayVoice(this.loader.GetVoiceAudioClip(voice_id), voice_id, 0.6f, this.voiceChannel, (DisableNotifyMonoBehaviour) this, this.loader.head);
  }

  public float GetRadiusCustomRate()
  {
    float radiusCustomRate = 1f;
    float num = 1f;
    if (this.GetOneHandSwordRadiusCustomRate(ref num))
      radiusCustomRate *= num;
    if (this.GetPairSwordsRadiusCustomRate(ref num))
      radiusCustomRate *= num;
    return radiusCustomRate;
  }

  protected bool GetOneHandSwordRadiusCustomRate(ref float rate)
  {
    if (!this.buffParam.IsValidBuff(BuffParam.BUFFTYPE.LUNATIC_TEAR) || this.attackMode != Player.ATTACK_MODE.ONE_HAND_SWORD || this.isActSpecialAction)
      return false;
    InGameSettingsManager.AbilityParam abilityParam = MonoBehaviourSingleton<InGameSettingsManager>.I.abilityParam;
    if (abilityParam == null || (double) abilityParam.oneHandSwordRadiusCustomRate <= 0.0)
      return false;
    rate = abilityParam.oneHandSwordRadiusCustomRate;
    return true;
  }

  public void StartEffectDrain(Enemy enemy)
  {
    if (Object.op_Equality((Object) enemy, (Object) null) || Object.op_Equality((Object) this.effectPlayProcessor, (Object) null))
      return;
    List<EffectPlayProcessor.EffectSetting> settings = this.effectPlayProcessor.GetSettings("EFFECT_DRAIN_DAMAGE");
    if (settings == null || settings.Count <= 0)
      return;
    new GameObject("EffectDrain").AddComponent<EffectDrain>().Initialize((StageObject) this, (StageObject) enemy, settings[0]);
  }

  public int GetCurrentWeaponElement()
  {
    return this.weaponData == null ? 6 : Singleton<EquipItemTable>.I.GetEquipItemData((uint) this.weaponData.eId).GetElemAtkType();
  }

  public Vector3 GetCannonVector()
  {
    if (this.targetFieldGimmickCannon == null)
      return Vector3.zero;
    Transform cannonTransform = this.targetFieldGimmickCannon.GetCannonTransform();
    return Object.op_Inequality((Object) cannonTransform, (Object) null) ? cannonTransform.forward : Vector3.zero;
  }

  public void ApplyCannonVector(Vector3 cannonVec)
  {
    Vector3 vector3 = cannonVec;
    vector3.y = 0.0f;
    this._rotation = Quaternion.LookRotation(vector3);
    if (this.targetFieldGimmickCannon == null)
      return;
    this.targetFieldGimmickCannon.ApplyCannonVector(cannonVec);
  }

  public void SetSyncCannonRotation(Vector3 cannonVec)
  {
    if (Vector3.op_Inequality(cannonVec, Vector3.zero))
      this.syncCannonRotation = Quaternion.LookRotation(cannonVec);
    if (Vector3.op_Inequality(this.GetCannonVector(), Vector3.zero))
      this.prevCannonRotation = Quaternion.LookRotation(this.GetCannonVector());
    this.syncCannonVecTimer = 0.0f;
    this.isSyncingCannonRotation = true;
  }

  protected void UpdateSyncCannonRotation()
  {
    if (!this.isSyncingCannonRotation)
      return;
    if (this.actionID != (Character.ACTION_ID) 31 /*0x1F*/)
    {
      this.isSyncingCannonRotation = false;
    }
    else
    {
      this.ApplyCannonVector(Quaternion.op_Multiply(Quaternion.SlerpUnclamped(this.prevCannonRotation, this.syncCannonRotation, Mathf.Clamp01(this.syncCannonVecTimer / 1f)), Vector3.forward));
      this.syncCannonVecTimer += Time.deltaTime;
    }
  }

  public void SetSyncUsingCannon(int id)
  {
    if (!QuestManager.IsValidInGame() || this.IsOnCannonMode() || !MonoBehaviourSingleton<InGameProgress>.IsValid() || !(MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.Cannon, id) is IFieldGimmickCannon fieldGimmickObj))
      return;
    this.actionID = (Character.ACTION_ID) 31 /*0x1F*/;
    this.targetFieldGimmickCannon = fieldGimmickObj;
    fieldGimmickObj.OnBoard(this);
    this.PlayMotion(131);
    this.SetCannonState(Player.CANNON_STATE.READY);
  }

  protected override void EventStatusUpDefenceON(AnimEventData.EventData data)
  {
    this.isAnimEventStatusUpDefence = true;
    if (data.floatArgs.Length == 0)
      return;
    this.animEventStatusUpDefenceRate = data.floatArgs[0];
  }

  protected override void EventStatusUpDefenceOFF()
  {
    this.isAnimEventStatusUpDefence = false;
    this.animEventStatusUpDefenceRate = 1f;
  }

  public void EventWeaponActionStart()
  {
    this.spearCtrl.OnWeaponActionStart();
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnWeaponActionStart();
  }

  public void EventWeaponActionEnd()
  {
    this.spearCtrl.OnWeaponActionEnd();
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnWeaponActionEnd();
  }

  protected void EventSetConditionTrigger()
  {
    if (!this.spearCtrl.CheckConditionTrigger())
      return;
    this.SetNextTrigger();
  }

  protected void EventSetConditionTrigger2()
  {
    if (!this.spearCtrl.CheckConditionTrigger2())
      return;
    this.SetNextTrigger(1);
  }

  private void EventFixPositionWeaponR_ON()
  {
    Transform node = this.FindNode("R_Wep");
    if (Object.op_Equality((Object) node, (Object) null) || !MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    this.fixWepData.wepTrans = node;
    this.fixWepData.wepPos = node.position;
    this.fixWepData.wepRot = node.rotation;
    this.fixWepData.enable = true;
    this.spearCtrl.OnFixPositionWeaponR_ON(this.fixWepData.wepPos);
  }

  private void EventFixPositionWeaponR_OFF()
  {
    if (this.fixWepData == null || !this.fixWepData.enable)
      return;
    this.spearCtrl.OnFixPositionWeaponR_OFF();
    this.fixWepData.ClearData();
  }

  private void EventSpearBurstBarrier_OFF() => this.spearCtrl.EnableBarrierBulletDelete();

  private void EventBuffStartShieldReflect(AnimEventData.EventData data)
  {
    if (!this.IsCoopNone() && !this.IsOriginal())
      return;
    this.OnBuffStart(new BuffParam.BuffData()
    {
      type = BuffParam.BUFFTYPE.SHIELD_REFLECT,
      time = -1f,
      endless = new bool?(true),
      value = 1
    });
    if (((IEnumerable<string>) data.stringArgs).Count<string>() >= 1)
      this.shieldReflectInfo.attackInfoName = data.stringArgs[0];
    if (((IEnumerable<int>) data.intArgs).Count<int>() >= 1)
      this.shieldReflectInfo.seId = data.intArgs[0];
    if (((IEnumerable<float>) data.floatArgs).Count<float>() < 6)
      return;
    this.shieldReflectInfo.offsetPos = new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
    this.shieldReflectInfo.offsetRot = new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
  }

  public void EventTeleportToTargetOffset(AnimEventData.EventData data)
  {
    if (this.IsValidBuff(BuffParam.BUFFTYPE.BLIND))
      return;
    bool flag = data.intArgs[0] != 0;
    Vector3 vector3_1;
    // ISSUE: explicit constructor call
    ((Vector3) ref vector3_1).\u002Ector(0.0f, 0.0f, data.floatArgs[0]);
    Vector3 pos = this._position;
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.boss, (Object) null))
    {
      pos = MonoBehaviourSingleton<StageObjectManager>.I.boss._position;
      if (flag)
      {
        Quaternion rotation = MonoBehaviourSingleton<StageObjectManager>.I.boss._rotation;
        vector3_1 = Quaternion.op_Multiply(Quaternion.Euler(((Quaternion) ref rotation).eulerAngles), vector3_1);
      }
    }
    else if (!this.GetTargetPos(out pos))
      return;
    pos.y = 0.0f;
    if (!flag)
      vector3_1 = Quaternion.op_Multiply(Quaternion.LookRotation(Vector3.op_Subtraction(this._position, pos)), vector3_1);
    Vector3 vector3_2 = pos;
    if (Vector3.op_Inequality(vector3_2, this._position))
      vector3_2 = Vector3.op_Addition(vector3_2, vector3_1);
    if (!MonoBehaviourSingleton<StageManager>.I.CheckPosInside(vector3_2))
    {
      RaycastHit hit = new RaycastHit();
      if (AIUtility.RaycastWallAndBlock((StageObject) this, vector3_2, out hit) || AIUtility.RaycastObstacle((StageObject) this, vector3_2, out hit))
        vector3_2 = ((RaycastHit) ref hit).point;
    }
    vector3_2.y = 0.0f;
    if (!((Vector3) ref pos).Equals(this._position))
      this._rotation = Quaternion.LookRotation(Vector3.op_Subtraction(pos, vector3_2));
    this._position = vector3_2;
  }

  private Player.ShieldDamageData CalcShieldDamage(int damage)
  {
    Player.ShieldDamageData shieldDamageData = new Player.ShieldDamageData();
    if ((int) this.ShieldHp <= damage)
    {
      shieldDamageData.isShieldEnd = true;
      shieldDamageData.shieldDamage = (int) this.ShieldHp;
      shieldDamageData.hpDamage = Mathf.Max(0, damage - (int) this.ShieldHp);
    }
    else
    {
      shieldDamageData.shieldDamage = damage;
      shieldDamageData.hpDamage = 0;
    }
    return shieldDamageData;
  }

  public bool _IsGuard()
  {
    if (this.disableGuard)
      return false;
    return this.isGuardWalk || this.actionID == (Character.ACTION_ID) 19 || this.actionID == (Character.ACTION_ID) 20 || this.actionID == (Character.ACTION_ID) 34 || this.actionID == (Character.ACTION_ID) 21;
  }

  private bool _IsGuard(SP_ATTACK_TYPE spType)
  {
    return this._IsGuard() && this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, spType);
  }

  public void _StartGuard()
  {
    this.isJustGuard = false;
    this.isSuccessParry = false;
    if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE))
    {
      InGameSettingsManager.Player.SpearActionInfo.Oracle oracle = this.playerParameter.spearActionInfo.oracle;
      this.guardingSec = Mathf.Lerp(oracle.maxJustGuardSec, oracle.minJustGuardSec, this.spearCtrl.StockedRate) * this.buffParam.GetJustGuardExtendRate();
    }
    else if (this.spAttackType == SP_ATTACK_TYPE.BURST)
      this.guardingSec = this.playerParameter.ohsActionInfo.burstOHSInfo.JustGuardValidSec * this.buffParam.GetJustGuardExtendRate();
    else
      this.guardingSec = this.playerParameter.ohsActionInfo.Common_JustGuardValidSec * this.buffParam.GetJustGuardExtendRate();
  }

  public void _EndGuard() => this.guardingSec = 0.0f;

  public void _UpdateGuard()
  {
    if ((double) this.guardingSec <= 0.0)
      return;
    this.guardingSec -= Time.deltaTime;
  }

  public bool _CheckJustGuardSec()
  {
    return !this._CheckDisableJustGuard() && (double) this.guardingSec > 0.0;
  }

  protected bool _CheckDisableJustGuard()
  {
    return this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.BURST) && this.disableParryAction;
  }

  private float _GetGuardDamageCutRate()
  {
    return (!this.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE) || !this._CheckJustGuardSec() ? (!this.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE) ? (!this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.NONE) || !this._CheckJustGuardSec() ? this.playerParameter.ohsActionInfo.Common_GuardDamageCutRate : this.playerParameter.ohsActionInfo.Normal_JustGuardDamageCutRate) : this.playerParameter.spearActionInfo.oracle.damageCutRateWhileGuard) : this.playerParameter.spearActionInfo.oracle.damageCutRateWhileJustGuard) * this.buffParam.GetGuardUp();
  }

  private float _GetGuardingHealSpeedUp()
  {
    return this._IsGuard(SP_ATTACK_TYPE.NONE) ? this.playerParameter.ohsActionInfo.Normal_GuardingHealSpeedRate : 1f;
  }

  private void _AddRevengeGauge(int damage)
  {
    if (this.hpMax <= 0)
      return;
    this.IncreaseSpActonGauge(new AttackHitInfo(), Vector3.zero, (float) damage);
  }

  public virtual void CheckBuffShadowSealing()
  {
  }

  protected void _StartBuffShadowSealing()
  {
    if (this.isBuffShadowSealing)
      return;
    this.isBuffShadowSealing = true;
    if (this.buffShadowSealingEffect != null)
      return;
    this.buffShadowSealingEffect = EffectManager.GetEffect("ef_btl_wsk_bow_01_02", this.FindNode("Root"));
  }

  protected void _EndBuffShadowSealing()
  {
    if (!this.isBuffShadowSealing)
      return;
    this.isBuffShadowSealing = false;
    this.ReleaseEffect(ref this.buffShadowSealingEffect);
  }

  protected void UpdateSpearAction()
  {
    InGameSettingsManager.Player.SpearActionInfo spearActionInfo = this.playerParameter.spearActionInfo;
    if (this.hitSpearSpecialAction)
    {
      this.hitSpearSpActionTimer += Time.deltaTime;
      if ((double) this.hitSpearSpActionTimer > (double) spearActionInfo.rushCancellableTime)
        this.enableCancelToAttack = false;
    }
    if (this.isLoopingRush)
    {
      this.actRushLoopTimer += Time.deltaTime;
      if ((double) this.actRushLoopTimer > (double) spearActionInfo.rushLoopTime)
      {
        this.isLoopingRush = false;
        this.SetNextTrigger();
      }
      else if ((double) this.actRushLoopTimer > (double) spearActionInfo.rushCanAvoidTime)
        this.enableCancelToAvoid = true;
    }
    if (this.isSpearHundred)
    {
      this.spearHundredSecFromStart += Time.deltaTime;
      this.spearHundredSecFromLastTap += Time.deltaTime;
      if ((double) this.spearHundredSecFromStart >= (double) spearActionInfo.hundredLoopLimitSec)
      {
        this.isSpearHundred = false;
        this.ActAttack(22, true, false, "", "");
      }
      else if ((double) this.spearHundredSecFromLastTap >= (double) spearActionInfo.hundredTapIntervalSec)
      {
        this.isSpearHundred = false;
        this.SetNextTrigger();
      }
    }
    switch (this.jumpState)
    {
      case Player.eJumpState.FallWait:
        this.jumpActionCounter -= Time.deltaTime;
        if ((double) this.jumpActionCounter > 0.0)
          break;
        this.SetNextTrigger(1);
        this.jumpState = Player.eJumpState.Fall;
        break;
      case Player.eJumpState.Fall:
        this.jumpFallBodyPosition.y -= spearActionInfo.jumpFallSpeed * Time.deltaTime;
        if ((double) this.jumpFallBodyPosition.y <= 0.0)
          this.OnJumpEnd(this._position, false, 0.0f);
        ((Component) this.body).transform.localPosition = this.jumpFallBodyPosition;
        break;
      case Player.eJumpState.HitStop:
        this.jumpActionCounter += Time.deltaTime;
        if ((double) this.jumpActionCounter < (double) spearActionInfo.jumpHitStop)
          break;
        this.jumpActionCounter = 0.0f;
        this.SetNextTrigger();
        this.jumpState = Player.eJumpState.Randing;
        break;
      case Player.eJumpState.Randing:
        this.jumpActionCounter += Time.deltaTime;
        if ((double) this.jumpActionCounter > (double) spearActionInfo.jumpRandingHeightStartTime)
        {
          float num = (float) (((double) this.jumpActionCounter - (double) spearActionInfo.jumpRandingHeightStartTime) / ((double) spearActionInfo.jumpRandingHeightEndTime - (double) spearActionInfo.jumpRandingHeightStartTime));
          if ((double) num > 1.0)
            num = 1f;
          this.jumpFallBodyPosition.y = this.jumpRandingBaseBodyY * (1f - num);
          if ((double) this.jumpFallBodyPosition.y <= 0.0)
            this.jumpFallBodyPosition.y = 0.0f;
          ((Component) this.body).transform.localPosition = this.jumpFallBodyPosition;
        }
        if ((double) this.jumpActionCounter <= (double) spearActionInfo.jumpRandingMoveStartTime)
          break;
        float num1 = (float) (((double) this.jumpActionCounter - (double) spearActionInfo.jumpRandingMoveStartTime) / ((double) spearActionInfo.jumpRandingMoveEndTime - (double) spearActionInfo.jumpRandingMoveStartTime));
        if ((double) num1 > 1.0)
          num1 = 1f;
        this._position = Vector3.op_Addition(this.jumpRaindngBasePos, Vector3.op_Multiply(this.jumpRandingVector, num1));
        break;
    }
  }

  public float GetRushDistanceRate()
  {
    float rushDistanceRate = this.buffParam.GetSpearRushDistanceRate() + this.playerParameter.spearActionInfo.rushDistanceRate;
    if ((double) this.exRushChargeRate >= 1.0)
      rushDistanceRate += this.evolveCtrl.GetLeviathanRushDistanceRate();
    return rushDistanceRate;
  }

  public float GetJumpElementDamageUpRate()
  {
    return this.playerParameter.spearActionInfo.jumpElementDamageRate[this.useGaugeLevel];
  }

  public virtual void HitJumpAttack()
  {
    this.OnJumpEnd(this._position, true, this.jumpFallBodyPosition.y);
  }

  public int CheckGaugeLevel()
  {
    if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.HEAT) || this.CheckAttackModeAndSpType(Player.ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.HEAT))
      return (int) ((double) this.CurrentWeaponSpActionGauge / 333.0);
    return this.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL) ? this.pairSwordsCtrl.GetComboLv() - 1 : -1;
  }

  public void UseSpGauge()
  {
    float num = 1000f;
    this.useGaugeLevel = 0;
    if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.HEAT))
    {
      num = 333f;
      this.useGaugeLevel = (int) ((double) this.CurrentWeaponSpActionGauge / 333.0);
    }
    else if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.HEAT))
    {
      num = 333f;
      if ((double) this.CurrentWeaponSpActionGauge >= 333.0)
        this.useGaugeLevel = 1;
    }
    if (this.useGaugeLevel <= 0)
      return;
    this.CurrentWeaponSpActionGauge -= num * (float) this.useGaugeLevel;
  }

  protected virtual void _JumpRize()
  {
  }

  public void OnJumpRize(Vector3 dir, int level)
  {
    this.jumpFallBodyPosition = dir;
    this.useGaugeLevel = level;
    this.SetNextTrigger();
    this.jumpState = Player.eJumpState.Rize;
    this.ForceClearInBarrier();
    this.StartWaitingPacket(StageObject.WAITING_PACKET.PLAYER_JUMP_END, true);
    if (this.playerSender == null)
      return;
    this.playerSender.OnJumpRize(dir, level);
  }

  public void OnJumpEnd(Vector3 pos, bool isSuccess, float y)
  {
    if (this.jumpState != Player.eJumpState.Fall)
      return;
    if (isSuccess)
    {
      if (Object.op_Equality((Object) this.bossBrain, (Object) null))
        return;
      this.jumpActionCounter = 0.0f;
      this.jumpRaindngBasePos = pos;
      this.jumpRandingBaseBodyY = y;
      this.jumpState = Player.eJumpState.HitStop;
    }
    else
    {
      this.jumpFallBodyPosition.y = 0.0f;
      ((Component) this.body).transform.localPosition = this.jumpFallBodyPosition;
      this.SetNextTrigger(2);
      this.jumpState = Player.eJumpState.Failure;
    }
    this.EndWaitingPacket(StageObject.WAITING_PACKET.PLAYER_JUMP_END);
    if (this.playerSender == null)
      return;
    this.playerSender.OnJumpEnd(pos, isSuccess, y);
  }

  public void ApplySyncExRush(bool flag) => this.isChargeExRush = flag;

  private void _StartExRushCharge()
  {
    float exRushChargeSec = this.playerParameter.spearActionInfo.exRushChargeSec;
    float num = 1f - this.buffParam.GetChargeSpearTimeRate();
    if ((double) num < 0.0)
      num = 0.0f;
    this.isChargeExRush = true;
    this.inputChargeMaxTiming = true;
    this.inputChargeTimeOffset = 0.0f;
    this.inputChargeTimeMax = exRushChargeSec * num;
    this.inputChargeTimeCounter = 0.0f;
    this.chargeRate = 0.0f;
    this.exRushChargeRate = 0.0f;
    if (this.exRushChargeEffect != null)
      return;
    this.exRushChargeEffect = EffectManager.GetEffect("ef_btl_wsk_charge_loop_01", this.FindNode("R_Wep"));
  }

  public bool IsExRushDamageUpAttack(AttackHitInfo.ATTACK_TYPE type)
  {
    return this.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.NONE) && (double) this.exRushChargeRate > 0.0 && (type == AttackHitInfo.ATTACK_TYPE.SPEAR_SP || this.attackID >= 10 && this.attackID <= 13);
  }

  public bool GetExRushElementDamageUpRate(AttackHitInfo.ATTACK_TYPE type, ref float rRate)
  {
    rRate = 1f;
    if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid() || !this.IsExRushDamageUpAttack(type))
      return false;
    float oMin;
    float oMax;
    float oFull;
    if (this.evolveCtrl.IsExecLeviathan())
    {
      this.evolveCtrl.GetLeviathanExRushDamageRate(out oMin, out oMax, out oFull);
    }
    else
    {
      InGameSettingsManager.Player.SpearActionInfo spearActionInfo = this.playerParameter.spearActionInfo;
      oMin = spearActionInfo.exRushElementDamageRateMin;
      oMax = spearActionInfo.exRushElementDamageRateMax;
      oFull = spearActionInfo.exRushElementDamageRateFull;
    }
    rRate = (double) this.exRushChargeRate < 1.0 ? oMin + (oMax - oMin) * this.exRushChargeRate : oFull;
    return true;
  }

  protected override void EventExecuteEvolve(AnimEventData.EventData data)
  {
    float rSec = 0.0f;
    if (!this.evolveCtrl.Execute(ref rSec))
      return;
    this.StartWaitingPacket(StageObject.WAITING_PACKET.EVOLVE, false, rSec);
  }

  public uint GetEvolveWeaponId() => this.weaponEquipItemDataList[this.weaponIndex].evolveId;

  public bool IsEvolveWeapon() => this.weaponEquipItemDataList[this.weaponIndex].evolveId > 0U;

  public void OnSyncEvolveAction(bool isAction)
  {
    if (isAction)
      this.ActEvolve();
    else
      this.EndEvolve();
  }

  public void ActEvolve()
  {
    this.EndAction();
    uint evolveWeaponId = this.GetEvolveWeaponId();
    this.actionID = (Character.ACTION_ID) 37;
    this.PlayMotion("@evolve_" + (object) evolveWeaponId);
    this.evolveCtrl.Start(evolveWeaponId);
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnSyncEvolveAction(true);
  }

  private void EndEvolve()
  {
    this.evolveCtrl.End();
    this.EndWaitingPacket(StageObject.WAITING_PACKET.EVOLVE);
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnSyncEvolveAction(false);
  }

  private void UpdateEvolve()
  {
    if (this.actionID == (Character.ACTION_ID) 38 && (double) this.evolveSpecialActionSec > 0.0)
    {
      this.evolveSpecialActionSec -= Time.deltaTime;
      if ((double) this.evolveSpecialActionSec <= 0.0)
        this.SetNextTrigger();
    }
    if (!this.evolveCtrl.isExec || !this.IsCoopNone() && !this.IsOriginal() || !this.evolveCtrl.Update())
      return;
    this.EndEvolve();
  }

  public void ActEvolveSpecialAction()
  {
    this.EndAction();
    uint evolveWeaponId = this.GetEvolveWeaponId();
    this.actionID = (Character.ACTION_ID) 38;
    this.PlayMotionImmidate("EVOLVE_" + (object) evolveWeaponId, "special", 0.0f);
    this.evolveSpecialActionSec = this.evolveCtrl.GetSpecialActionSec();
    this.evolveCtrl.PlaySphinxActionEffect();
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnEvolveSpecialAction();
  }

  public bool CanEvolveSpecialFlickAction()
  {
    return this.evolveCtrl.IsExistSpecialAction() && this.evolveCtrl.isExec && this.enableCancelToEvolveSpecialAction;
  }

  public bool CanSoulOneHandSwordFlickAction()
  {
    return this.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL) && this.enableSpAttackContinue;
  }

  public bool CanFlickAction() => this.enableFlickAction;

  public void ActFlickAction(Vector3 inputVec, bool isOriginal)
  {
    if (isOriginal && (!this.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.BURST) || this.attackHitCount < 1))
      return;
    this.EndAction();
    this.actionID = (Character.ACTION_ID) 42;
    if (this.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.BURST))
    {
      this.pairSwordsCtrl.ActAerialAvoid(inputVec);
      this.PlayMotionImmidate((string) null, "attack_32_avoid", 0.0f);
    }
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnActFlickAction(inputVec);
  }

  public void InputNextTrigger()
  {
    if (!this.enableInputNextTrigger || this.inputNextTriggerFlag)
      return;
    this.inputNextTriggerFlag = true;
    if (!this.enableNextTriggerTrans)
      return;
    this.DoInputNextTrigger();
  }

  private void DoInputNextTrigger()
  {
    this.SetNextTrigger(this.inputNextTriggerIndex);
    this.enableInputNextTrigger = false;
    this.enableNextTriggerTrans = false;
    this.inputNextTriggerFlag = false;
    this.inputNextTriggerIndex = 0;
  }

  public void StartFieldBuff(uint fieldBuffId)
  {
    this.buffParam.ClearFieldBuff();
    if (fieldBuffId <= 0U || !Singleton<FieldBuffTable>.IsValid() || !Singleton<BuffTable>.IsValid())
      return;
    FieldBuffTable.FieldBuffData data1 = Singleton<FieldBuffTable>.I.GetData(fieldBuffId);
    if (data1 == null)
      return;
    for (int index = 0; index < data1.buffTableIds.Count; ++index)
    {
      BuffTable.BuffData data2 = Singleton<BuffTable>.I.GetData(data1.buffTableIds[index]);
      if (data2 != null)
        this.buffParam.StartFieldBuff(fieldBuffId, data2);
    }
  }

  public void StartBuffByBuffTableId(int id, SkillInfo.SkillParam skillParam)
  {
    if (id < 0)
      return;
    this.StartBuffByBuffTableId((uint) id, skillParam);
  }

  public void StartBuffByBuffTableIdOnActDeadStandUp(int id, SkillInfo.SkillParam skillParam)
  {
    if (id < 0)
      return;
    this.buffInfoListOnActDeadStandUp.Add(new KeyValuePair<int, SkillInfo.SkillParam>(id, skillParam));
  }

  public void StartBuffByBuffTableId(uint id, SkillInfo.SkillParam skillParam)
  {
    if (!Singleton<BuffTable>.IsValid())
      return;
    BuffTable.BuffData data = Singleton<BuffTable>.I.GetData(id);
    if (data == null)
      return;
    BuffParam.BuffData buffData = new BuffParam.BuffData();
    buffData.type = data.type;
    buffData.interval = data.interval;
    buffData.valueType = data.valueType;
    buffData.time = data.duration;
    float num = (float) data.value;
    if (skillParam != null && Singleton<GrowSkillItemTable>.IsValid())
    {
      GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(data.growID, skillParam.baseInfo.level, skillParam.baseInfo.exceedCnt);
      if (growSkillItemData != null)
      {
        buffData.time = (float) ((double) data.duration * (double) (int) growSkillItemData.supprtTime[0].rate * 0.0099999997764825821) + (float) growSkillItemData.supprtTime[0].add;
        num = (float) (data.value * (int) growSkillItemData.supprtValue[0].rate) * 0.01f + (float) (int) growSkillItemData.supprtValue[0].add;
      }
    }
    if (buffData.valueType == BuffParam.VALUE_TYPE.RATE && BuffParam.IsTypeValueBasedOnHP(buffData.type))
      num = (float) ((double) this.hpMax * (double) num * 0.0099999997764825821);
    buffData.value = Mathf.FloorToInt(num);
    this.OnBuffStart(buffData);
  }

  public bool IsInSpearBurstBarrier() => this.burstBarrierCounter > 0 && !this.disableGuard;

  public bool IsInBarrier() => !this.bulletBarrierObjList.IsNullOrEmpty<BarrierBulletObject>();

  public bool IsInAliveBarrier()
  {
    return this.bulletBarrierObjList.Any<BarrierBulletObject>((Func<BarrierBulletObject, bool>) (barrier => !barrier.IsDead()));
  }

  private void ForceClearInBarrier()
  {
    if (this.bulletBarrierObjList.IsNullOrEmpty<BarrierBulletObject>())
      return;
    int index = 0;
    for (int count = this.bulletBarrierObjList.Count; index < count; ++index)
    {
      BarrierBulletObject bulletBarrierObj = this.bulletBarrierObjList[index];
      bulletBarrierObj.RemovePlayer(this);
      bulletBarrierObj.ForceExitThroughProcessor(this._collider);
      Transform t = this.effectTransTable.Get(bulletBarrierObj.GetEffectNameInBarrier());
      if (Object.op_Inequality((Object) t, (Object) null))
      {
        EffectManager.ReleaseEffect(ref t);
        this.effectTransTable.Remove(bulletBarrierObj.GetEffectNameInBarrier());
      }
    }
    this.bulletBarrierObjList.Clear();
  }

  public void ForceClearByBarrier(BarrierBulletObject barrier)
  {
    if (this.bulletBarrierObjList.IsNullOrEmpty<BarrierBulletObject>())
      return;
    this.bulletBarrierObjList.Remove(barrier);
    Transform t = this.effectTransTable.Get(barrier.GetEffectNameInBarrier());
    if (!Object.op_Inequality((Object) t, (Object) null))
      return;
    EffectManager.ReleaseEffect(ref t);
    this.effectTransTable.Remove(barrier.GetEffectNameInBarrier());
  }

  public void MakeInvincible(float duration)
  {
    if ((this.hitOffFlag & StageObject.HIT_OFF_FLAG.INVICIBLE) > StageObject.HIT_OFF_FLAG.NONE)
      return;
    this.hitOffFlag |= StageObject.HIT_OFF_FLAG.INVICIBLE;
    this.cancelInvincible = this.CancelInvincible(duration);
    this.StartCoroutine(this.cancelInvincible);
  }

  private IEnumerator CancelInvincible(float waitTime)
  {
    yield return (object) new WaitForSeconds(waitTime);
    this.hitOffFlag &= ~StageObject.HIT_OFF_FLAG.INVICIBLE;
  }

  protected override void OnAttackedContinuationStart(StageObject.AttackedContinuationStatus status)
  {
    BarrierBulletObject componentInParent = ((Component) status.fromCollider).gameObject.GetComponentInParent<BarrierBulletObject>();
    if (Object.op_Inequality((Object) componentInParent, (Object) null))
    {
      this.bulletBarrierObjList.Add(componentInParent);
      componentInParent.AddPlayer(this);
      string effectNameInBarrier = componentInParent.GetEffectNameInBarrier();
      if (Object.op_Equality((Object) this.effectTransTable.Get(effectNameInBarrier), (Object) null))
      {
        Transform effect = EffectManager.GetEffect(effectNameInBarrier, this.rootNode);
        if (Object.op_Inequality((Object) effect, (Object) null))
          this.effectTransTable.Add(effectNameInBarrier, effect);
      }
    }
    if (status.attackInfo.type != AttackContinuationInfo.CONTINUATION_TYPE.SPEAR_BURST)
      return;
    ++this.burstBarrierCounter;
  }

  protected override void OnAttackedContinuationEnd(StageObject.AttackedContinuationStatus status)
  {
    BarrierBulletObject componentInParent = ((Component) status.fromCollider).gameObject.GetComponentInParent<BarrierBulletObject>();
    if (Object.op_Inequality((Object) componentInParent, (Object) null))
    {
      this.bulletBarrierObjList.Remove(componentInParent);
      componentInParent.RemovePlayer(this);
      if (!this.IsInBarrier())
      {
        Transform t = this.effectTransTable.Get(componentInParent.GetEffectNameInBarrier());
        if (Object.op_Inequality((Object) t, (Object) null))
        {
          EffectManager.ReleaseEffect(ref t);
          this.effectTransTable.Remove(componentInParent.GetEffectNameInBarrier());
        }
      }
    }
    if (status.attackInfo.type != AttackContinuationInfo.CONTINUATION_TYPE.SPEAR_BURST)
      return;
    --this.burstBarrierCounter;
    if (this.burstBarrierCounter >= 0)
      return;
    this.burstBarrierCounter = 0;
  }

  protected override void OnCollisionEnter(Collision collision)
  {
    if (!this.IsCoopNone() && !this.IsOriginal() || !this.snatchCtrl.IsMoveLoop())
      return;
    switch (collision.gameObject.layer)
    {
      case 9:
      case 10:
      case 11:
      case 17:
      case 18:
        this.OnSnatchMoveEnd();
        break;
    }
  }

  protected override void OnCollisionStay(Collision collision)
  {
    if (collision.gameObject.layer == 9 || collision.gameObject.layer == 17 || collision.gameObject.layer == 18)
      this.isWallStay = true;
    if (!this.IsCoopNone() && !this.IsOriginal() || !this.snatchCtrl.IsMoveLoop())
      return;
    switch (collision.gameObject.layer)
    {
      case 9:
      case 10:
      case 11:
      case 17:
      case 18:
        if (!this.snatchCtrl.IsMoveLoopStart())
        {
          this.snatchCtrl.ActivateLoopStart();
          break;
        }
        this.OnSnatchMoveEnd();
        break;
    }
  }

  public string GetMotionLayerName(
    Player.ATTACK_MODE _attackMode,
    SP_ATTACK_TYPE _spAtkType,
    int _motionId)
  {
    string motionLayerName = "Base Layer.";
    switch (_attackMode)
    {
      case Player.ATTACK_MODE.ONE_HAND_SWORD:
        InGameSettingsManager.Player.BurstOneHandSwordActionInfo burstOhsInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.burstOHSInfo;
        if (_spAtkType == SP_ATTACK_TYPE.BURST && burstOhsInfo != null)
        {
          if (((burstOhsInfo.BaseAtkId > _motionId ? 0 : (_motionId <= burstOhsInfo.AvoidAttackID ? 1 : 0)) | (burstOhsInfo.CounterAttackId == _motionId ? 1 : 0)) != 0)
          {
            motionLayerName += "BURST.";
            break;
          }
          break;
        }
        if (_spAtkType == SP_ATTACK_TYPE.ORACLE && OneHandSwordController.IsOracleAttackId(_motionId))
        {
          motionLayerName += "ORACLE.";
          break;
        }
        break;
      case Player.ATTACK_MODE.TWO_HAND_SWORD:
        InGameSettingsManager.Player.BurstTwoHandSwordActionInfo burstThsInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.burstTHSInfo;
        if (_spAtkType == SP_ATTACK_TYPE.BURST && burstThsInfo != null && burstThsInfo.BaseAtkId <= _motionId && _motionId <= burstThsInfo.BurstAvoidAttackID)
        {
          motionLayerName += "BURST.";
          break;
        }
        if (_spAtkType == SP_ATTACK_TYPE.ORACLE && TwoHandSwordOracleController.IsOracleLayerAttackId(_motionId))
        {
          motionLayerName += "ORACLE.";
          break;
        }
        break;
      case Player.ATTACK_MODE.SPEAR:
        switch (_spAtkType)
        {
          case SP_ATTACK_TYPE.BURST:
            if (MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.burstSpearInfo.baseAtkId <= _motionId)
            {
              motionLayerName += "BURST.";
              break;
            }
            break;
          case SP_ATTACK_TYPE.ORACLE:
            if (SpearController.IsOracleAttackId(_motionId))
            {
              motionLayerName += "ORACLE.";
              break;
            }
            break;
        }
        break;
      case Player.ATTACK_MODE.PAIR_SWORDS:
        if (_spAtkType == SP_ATTACK_TYPE.ORACLE && this.pairSwordsCtrl.IsOracleAttackId(_motionId))
        {
          motionLayerName += "ORACLE.";
          break;
        }
        break;
    }
    return motionLayerName;
  }

  protected override int GetColliderGenerateCount(AnimEventData.EventData _eventData)
  {
    if (_eventData == null || _eventData.intArgs == null || _eventData.intArgs.Length < 1)
      return base.GetColliderGenerateCount(_eventData);
    AnimEventFormat.MULTI_COL_GENERATE_CONDITION intArg = (AnimEventFormat.MULTI_COL_GENERATE_CONDITION) _eventData.intArgs[0];
    return this.CheckAttackModeAndSpType(Player.ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.BURST) && this.thsCtrl != null && intArg == AnimEventFormat.MULTI_COL_GENERATE_CONDITION.IN_CORN ? this.thsCtrl.CurrentRestBulletCount : base.GetColliderGenerateCount(_eventData);
  }

  public override int GetObservedID()
  {
    string s = (this.id % 100000).ToString() + this.bulletIndex.ToString();
    int observedId = -1;
    ref int local = ref observedId;
    if (!int.TryParse(s, out local))
      return -1;
    ++this.bulletIndex;
    return observedId;
  }

  public override void OnSetSearchTarget(int observedID, int targetID)
  {
    if (this.bulletObservableList.IsNullOrEmpty<IBulletObservable>() || this.bulletObservableIdList.IsNullOrEmpty<int>() || !this.bulletObservableIdList.Contains(observedID))
      return;
    for (int index = 0; index < this.bulletObservableList.Count; ++index)
    {
      if (this.bulletObservableList[index].GetObservedID() == observedID)
      {
        BulletObject bulletObservable = this.bulletObservableList[index] as BulletObject;
        if (!Object.op_Inequality((Object) bulletObservable, (Object) null))
          break;
        bulletObservable.SetSearchTarget(targetID);
        break;
      }
    }
  }

  public void RegistBulletTurret(uint id, BulletControllerTurretBit bit)
  {
    if (this.bulletTurretList == null)
      return;
    if (this.bulletTurretList.ContainsKey(id))
    {
      BulletControllerTurretBit bulletTurret = this.bulletTurretList[id];
      if (Object.op_Inequality((Object) bulletTurret, (Object) null))
        Object.Destroy((Object) ((Component) bulletTurret).gameObject);
      this.bulletTurretList.Remove(id);
    }
    this.bulletTurretList.Add(id, bit);
  }

  public void RemoveAllBulletTurret()
  {
    if (this.bulletTurretList == null || this.bulletTurretList.Count == 0)
      return;
    foreach (KeyValuePair<uint, BulletControllerTurretBit> bulletTurret in this.bulletTurretList)
    {
      if (Object.op_Inequality((Object) bulletTurret.Value, (Object) null) && Object.op_Inequality((Object) ((Component) bulletTurret.Value).gameObject, (Object) null))
        Object.Destroy((Object) ((Component) bulletTurret.Value).gameObject);
    }
    this.bulletTurretList.Clear();
  }

  public override void OnSetTurretBitTarget(int observedID, int targetID, int regionID)
  {
    if (this.bulletObservableList.IsNullOrEmpty<IBulletObservable>() || this.bulletObservableIdList.IsNullOrEmpty<int>() || !this.bulletObservableIdList.Contains(observedID))
      return;
    for (int index = 0; index < this.bulletObservableList.Count; ++index)
    {
      if (this.bulletObservableList[index].GetObservedID() == observedID)
      {
        BulletObject bulletObservable = this.bulletObservableList[index] as BulletObject;
        if (!Object.op_Inequality((Object) bulletObservable, (Object) null))
          break;
        bulletObservable.SetTurretBitTarget(targetID, regionID);
        break;
      }
    }
  }

  public virtual void EventArrowRainChargeStart(AnimEventData.EventData data)
  {
    if (this.enableInputCharge)
      return;
    this.rainShotState = Player.RAIN_SHOT_STATE.START_CHARGE;
    this.enableInputCharge = true;
    this.isArrowAimable = true;
    this.inputChargeAutoRelease = false;
    this.inputChargeMaxTiming = true;
    this.inputChargeTimeMax = data.floatArgs[0] * (1f - this.GetChargeArrowTimeRate());
    this.inputChargeTimeOffset = 0.0f;
    this.inputChargeTimeCounter = 0.0f;
    this.isInputChargeExistOffset = false;
    this.chargeRate = 0.0f;
    this.isChargeExRush = false;
    this.exRushChargeRate = 0.0f;
    this.StartWaitingPacket(StageObject.WAITING_PACKET.PLAYER_CHARGE_RELEASE, true);
    this.CheckInputCharge();
  }

  public virtual void RainShotChargeRelease()
  {
    this.rainShotState = Player.RAIN_SHOT_STATE.FINISH_CHARGE;
    this.arrowRainTargetPointList = this.targetingPointList.GetRange(0, this.targetingPointList.Count);
  }

  public void OnRainShotChargeRelease(Vector3 pos, float rotY)
  {
    this.rainShotFallPosition = pos;
    this.rainShotFallRotateY = rotY;
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnRainShotChargeRelease(pos, rotY);
  }

  public virtual void EventArrowRainStart(AnimEventData.EventData data, bool visibled = false)
  {
    if (this.rainShotState != Player.RAIN_SHOT_STATE.FINISH_CHARGE && this.rainShotState != Player.RAIN_SHOT_STATE.START_RAIN || data.floatArgs.Length <= 6 || data.intArgs.Length <= 1)
      return;
    this.rainShotState = Player.RAIN_SHOT_STATE.START_RAIN;
    int intArg = data.intArgs[0];
    int num1 = Mathf.CeilToInt((float) data.intArgs[1] * this.buffParam.GetArrowRainNumRate());
    float floatArg = data.floatArgs[6];
    Vector3 vector3 = Vector3.op_Addition(this.rainShotFallPosition, new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]));
    Vector3 rot;
    // ISSUE: explicit constructor call
    ((Vector3) ref rot).\u002Ector(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
    float rainLengthDivide = this.playerParameter.arrowActionInfo.arrowRainLengthDivide;
    float arrowRainAngleDivide = this.playerParameter.arrowActionInfo.arrowRainAngleDivide;
    int maxFrameInterval = this.playerParameter.arrowActionInfo.arrowRainMaxFrameInterval;
    string shotAttackInfoName = this.playerParameter.arrowActionInfo.arrowRainShotAttackInfoName;
    if (this.isBoostMode)
      shotAttackInfoName += "_boost";
    AttackInfo attackInfo = this.FindAttackInfo(shotAttackInfoName);
    attackInfo.rateInfoRate = this.GetChargingRate();
    for (int index = 0; index < num1; ++index)
    {
      Vector3 pos = vector3;
      if (index > 0)
      {
        float num2 = (float) (Random.Range(0, Mathf.FloorToInt(floatArg / rainLengthDivide)) + 1) * rainLengthDivide;
        float num3 = (float) Random.Range(0, Mathf.FloorToInt(360f / arrowRainAngleDivide)) * arrowRainAngleDivide;
        pos = Vector3.op_Addition(pos, Quaternion.op_Multiply(Quaternion.Euler(new Vector3(0.0f, num3, 0.0f)), new Vector3(0.0f, 0.0f, num2)));
      }
      this.StartCoroutine(this.DelayShotArrowRain(intArg, pos, rot, attackInfo));
      intArg += Random.Range(0, maxFrameInterval);
    }
  }

  private IEnumerator DelayShotArrowRain(
    int delay,
    Vector3 pos,
    Vector3 rot,
    AttackInfo attackInfo)
  {
    while (delay > 0)
    {
      --delay;
      yield return (object) null;
    }
    this.ShotArrow(pos, Quaternion.Euler(rot), attackInfo, true, false, false);
  }

  public void OnShotShieldReflect(Player.ShieldReflectInfo reflectInfo)
  {
    if (!this.IsOriginal() && !this.IsCoopNone())
      return;
    Coop_Model_PlayerShotShieldReflect model = new Coop_Model_PlayerShotShieldReflect();
    model.id = this.id;
    model.atkInfoName = reflectInfo.attackInfoName;
    model.offsetPos = reflectInfo.offsetPos;
    model.offsetRot = reflectInfo.offsetRot;
    model.damage = reflectInfo.damage;
    model.targetId = reflectInfo.targetId;
    if (reflectInfo.seId > 0)
      SoundManager.PlayOneShotSE(reflectInfo.seId, (DisableNotifyMonoBehaviour) this, this.FindNode(""));
    this.OnShotShieldReflect(model);
  }

  public void OnShotShieldReflect(Coop_Model_PlayerShotShieldReflect model)
  {
    if (!(this.FindAttackInfo(model.atkInfoName, isDuplicate: true) is AttackHitInfo attackInfo) || Object.op_Equality((Object) attackInfo.bulletData, (Object) null))
      return;
    StageObject enemy = MonoBehaviourSingleton<StageObjectManager>.I.FindEnemy(model.targetId);
    if (Object.op_Equality((Object) enemy, (Object) null))
      return;
    if (attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.SHIELD_REFLECT)
      attackInfo.atk.normal = (float) model.damage;
    Vector3 vector3 = Vector3.op_Subtraction(enemy._transform.position, this._transform.position);
    vector3.y = 0.0f;
    Quaternion quaternion = Quaternion.LookRotation(vector3);
    Vector3 pos = Vector3.op_Addition(this._transform.position, Quaternion.op_Multiply(quaternion, model.offsetPos));
    Quaternion rot = Quaternion.op_Multiply(quaternion, Quaternion.Euler(model.offsetRot));
    AnimEventShot.Create((StageObject) this, (AttackInfo) attackInfo, pos, rot).SetTarget(enemy);
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnShotShieldReflect(model);
  }

  public override bool IsCarrying()
  {
    return Object.op_Inequality((Object) this.carryingGimmickObject, (Object) null);
  }

  public void ActCarry(InGameProgress.eFieldGimmick type, int pointId)
  {
    if (this.EndCarryIfSameAsOneOfSelf(pointId) || this.IsCarrying())
      return;
    this.EndAction();
    this.targetingGimmickObject = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(type, pointId);
    if (this.targetingGimmickObject is FieldCarriableGimmickObject)
      this.carryingGimmickObject = this.targetingGimmickObject as FieldCarriableGimmickObject;
    else if (this.targetingGimmickObject is FieldSupplyGimmickObject)
      this.carryingGimmickObject = (this.targetingGimmickObject as FieldSupplyGimmickObject).SupplyGimmick();
    if (Object.op_Equality((Object) this.carryingGimmickObject, (Object) null))
      return;
    this.actionID = (Character.ACTION_ID) 44;
    this.PlayMotion(142);
    Vector3 velocity = Vector3.Normalize(Vector3.op_Subtraction(this.carryingGimmickObject.GetTransform().position, this._transform.position));
    velocity.y = 0.0f;
    this.SetLerpRotation(velocity);
    Vector3 position = Vector3.op_Addition(this._transform.position, Quaternion.op_Multiply(Quaternion.LookRotation(velocity), FieldCarriableGimmickObject.kCarryOffset));
    if (this.IsCoopNone() || this.IsOriginal())
      this.SetActionPosition(position, true);
    this.carryingGimmickObject.GetTransform().position = position;
    this.carryingGimmickObject.StartCarry(this);
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnActCarry(type, pointId);
  }

  public void ActCarryIdle()
  {
    if (!this.IsCarrying() || this.EndCarryIfSameAsOneOfSelf(this.carryingGimmickObject.GetId()))
      this.ActIdle(false, -1f);
    this.EndAction();
    this.isControllable = true;
    this.actionID = (Character.ACTION_ID) 44;
    this.PlayMotion(143);
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnActCarryIdle();
  }

  public void ActCarryWalk(Vector3 velocity, float syncSpeed, Vector3 moveVec)
  {
    if (!this.IsCarrying() || this.EndCarryIfSameAsOneOfSelf(this.carryingGimmickObject.GetId()))
      this.ActIdle(false, -1f);
    this.ActMoveVelocity(velocity, syncSpeed, (Character.MOTION_ID) 144 /*0x90*/);
    this.SetLerpRotation(moveVec);
    this.isCarryWalk = true;
  }

  public void ActCarryPut(int pointId = 0)
  {
    if (!this.IsCarrying() || this.EndCarryIfSameAsOneOfSelf(this.carryingGimmickObject.GetId()))
      this.ActIdle(false, -1f);
    this.EndAction();
    if (pointId > 0)
    {
      this.targetingGimmickObject = (IFieldGimmickObject) (MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.CarriableGimmick, pointId) as FieldCarriableGimmickObject);
      if (this.targetingGimmickObject != null)
        this._transform.rotation = Quaternion.LookRotation(Vector3.op_Subtraction(this.targetingGimmickObject.GetTransform().position, this._transform.position), Vector3.up);
    }
    this.PlayMotion(145);
    if (!Object.op_Inequality((Object) this.playerSender, (Object) null))
      return;
    this.playerSender.OnActCarryPut(pointId);
  }

  public void EventStartCarryGimmick()
  {
    if (Object.op_Equality((Object) this.carryingGimmickObject, (Object) null))
      return;
    Transform node = this.FindNode(FieldCarriableGimmickObject.kCarryNode);
    if (Object.op_Inequality((Object) node, (Object) null))
    {
      this.carryingGimmickObject.GetTransform().SetParent(node);
      this.carryingGimmickObject.GetTransform().localPosition = Vector3.zero;
    }
    this.targetingGimmickObject = (IFieldGimmickObject) null;
  }

  public void EventEndCarryGimmick()
  {
    if (!this.IsCarrying() || Object.op_Equality((Object) this.carryingGimmickObject, (Object) null))
      return;
    FieldCarriableGimmickObject targetingGimmickObject = this.targetingGimmickObject as FieldCarriableGimmickObject;
    FieldCarriableEvolveItemGimmickObject carryingGimmickObject = this.carryingGimmickObject as FieldCarriableEvolveItemGimmickObject;
    if (Object.op_Inequality((Object) carryingGimmickObject, (Object) null) && Object.op_Inequality((Object) targetingGimmickObject, (Object) null) && targetingGimmickObject.CanEvolve())
    {
      carryingGimmickObject.Use2Evolve(targetingGimmickObject);
      MonoBehaviourSingleton<UIPlayerAnnounce>.I.Announce(UIPlayerAnnounce.ANNOUNCE_TYPE.GIMMICK_EVOLVE, this);
    }
    this.EndCarry();
  }

  public void EndCarry()
  {
    if (!this.IsCarrying())
      return;
    this.carryingGimmickObject.EndCarry();
    this.carryingGimmickObject = (FieldCarriableGimmickObject) null;
    this.targetingGimmickObject = (IFieldGimmickObject) null;
  }

  public bool EndCarryIfSameAsOneOfSelf(int pointId)
  {
    Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
    if (Object.op_Equality((Object) self, (Object) null) || self.id == this.id)
      return false;
    IFieldGimmickObject fieldGimmickObject = self.targetingGimmickObject ?? (IFieldGimmickObject) self.carryingGimmickObject;
    if (fieldGimmickObject == null || fieldGimmickObject.GetId() != pointId)
      return false;
    this.EndCarry();
    self.EndCarry();
    self.ActIdle(false, -1f);
    return true;
  }

  public void ActiveShadow(bool isActive) => this.StartCoroutine(this.IEActiveShadow(isActive));

  private IEnumerator IEActiveShadow(bool isActive)
  {
    while (Object.op_Equality((Object) this.shadow, (Object) null) && (Object.op_Equality((Object) this.loader, (Object) null) || this.loader.isLoading))
      yield return (object) null;
    this.shadow = ((Component) this).GetComponentInChildren<CircleShadow>(true);
    if (Object.op_Inequality((Object) this.shadow, (Object) null))
    {
      ((Component) this.shadow).gameObject.SetActive(isActive);
      if (isActive)
        this.shadow = (CircleShadow) null;
    }
  }

  public enum SKILL_SE_TYPE
  {
    START,
    ACT,
  }

  public enum SUB_ACTION_ID
  {
    AVOID = 13, // 0x0000000D
    STUMBLE = 14, // 0x0000000E
    SHAKE = 15, // 0x0000000F
    BLOW = 16, // 0x00000010
    FALL_BLOW = 17, // 0x00000011
    STUNNED_BLOW = 18, // 0x00000012
    GUARD = 19, // 0x00000013
    GUARD_DAMAGE = 20, // 0x00000014
    GUARD_PARRY = 21, // 0x00000015
    SKILL_ACTION = 22, // 0x00000016
    BATTLE_START = 23, // 0x00000017
    DEAD_LOOP = 24, // 0x00000018
    DEAD_STANDUP = 25, // 0x00000019
    PRAYER = 26, // 0x0000001A
    CHANGE_WEAPON = 27, // 0x0000001B
    GATHER = 28, // 0x0000001C
    GRABBED = 29, // 0x0000001D
    RESTRAINT = 30, // 0x0000001E
    CANNON_STANDBY = 31, // 0x0000001F
    CANNON_SHOT = 32, // 0x00000020
    SPECIAL_ACTION = 33, // 0x00000021
    GUARD_NO_KNOCKBACK = 34, // 0x00000022
    SONAR = 35, // 0x00000023
    WARP = 36, // 0x00000024
    EVOLVE = 37, // 0x00000025
    EVOLVE_SPECIAL = 38, // 0x00000026
    READ_STORY = 39, // 0x00000027
    FISHING = 40, // 0x00000028
    COOP_FISHING = 41, // 0x00000029
    FLICK_ACTION = 42, // 0x0000002A
    STONE = 43, // 0x0000002B
    CARRY = 44, // 0x0000002C
    CARRY_PUT = 45, // 0x0000002D
    TELEPORT_AVOID = 46, // 0x0000002E
    CHARM_BLOW = 47, // 0x0000002F
    POSE = 48, // 0x00000030
    RUSH_AVOID = 49, // 0x00000031
    MAX = 50, // 0x00000032
  }

  public enum eContinueType
  {
    CONTINUE,
    RESCUE,
    AUTO_REVIVE,
    REACH_NEXT_WAVE,
  }

  public enum SUB_MOTION_ID
  {
    AVOID = 115, // 0x00000073
    STUMBLE = 116, // 0x00000074
    SHAKE = 117, // 0x00000075
    BLOW = 118, // 0x00000076
    FALL_BLOW = 119, // 0x00000077
    STUNNED_BLOW = 120, // 0x00000078
    GUARD = 121, // 0x00000079
    GUARD_WALK = 122, // 0x0000007A
    GUARD_DAMAGE = 123, // 0x0000007B
    BATTLE_START = 124, // 0x0000007C
    DEAD_LOOP = 125, // 0x0000007D
    DEAD_STANDUP = 126, // 0x0000007E
    PRAYER = 127, // 0x0000007F
    CHANGE_WEAPON = 128, // 0x00000080
    GRABBED = 129, // 0x00000081
    RESTRAINT = 130, // 0x00000082
    CANNON_ENTER = 131, // 0x00000083
    CANNON_LOOP = 132, // 0x00000084
    GUARD_NO_KNOCKBACK = 133, // 0x00000085
    GUARD_PARRY = 134, // 0x00000086
    WARP = 135, // 0x00000087
    AVOID_ALTER = 136, // 0x00000088
    WARP_ALTER = 137, // 0x00000089
    FISHING = 138, // 0x0000008A
    COOP_FISHING = 139, // 0x0000008B
    STONE = 140, // 0x0000008C
    STONE_END = 141, // 0x0000008D
    CARRY_LIFT = 142, // 0x0000008E
    CARRY_IDLE = 143, // 0x0000008F
    CARRY_WALK = 144, // 0x00000090
    CARRY_PUT = 145, // 0x00000091
    TELEPORT_AVOID = 146, // 0x00000092
    RUSH_AVOID = 147, // 0x00000093
    MAX = 148, // 0x00000094
  }

  public enum ATTACK_MODE
  {
    NONE,
    ONE_HAND_SWORD,
    TWO_HAND_SWORD,
    SPEAR,
    PAIR_SWORDS,
    ARROW,
    MAX,
  }

  public class PlayerState
  {
    public List<int> atkList = new List<int>();
    public List<int> defList = new List<int>();
  }

  public class EquipState
  {
    public List<int> atkList = new List<int>();
    public List<int> defList = new List<int>();
    public int hp;
  }

  public class SkillData
  {
    public List<int> ids = new List<int>();
    public List<int> lvs = new List<int>();
    public List<int> exs = new List<int>();
  }

  public class AbilityData
  {
    public List<int> ids = new List<int>();
    public List<int> APs = new List<int>();
  }

  public enum eJumpState
  {
    None,
    Charge,
    Charged,
    Rize,
    FallWait,
    Fall,
    HitStop,
    Randing,
    Failure,
  }

  public enum RAIN_SHOT_STATE
  {
    NONE,
    START_CHARGE,
    FINISH_CHARGE,
    START_RAIN,
  }

  public enum PRAY_REASON
  {
    NONE,
    DEAD,
    STONE,
  }

  public class PrayInfo
  {
    public int targetId;
    public Player.PRAY_REASON reason;
  }

  public enum BOOST_PRAY_TYPE
  {
    GUARD_ONE_HAND_SWORD_NORMAL,
    IN_BARRIER,
    MAX,
  }

  [Serializable]
  public class BoostPrayInfo
  {
    public int prayerId;
    public int prayTargetId;
    public bool[] isBoostByTypes = new bool[2];

    public bool IsBoost()
    {
      return ((IEnumerable<bool>) this.isBoostByTypes).Any<bool>((Func<bool, bool>) (item => item));
    }

    public bool IsNoBoost()
    {
      return ((IEnumerable<bool>) this.isBoostByTypes).All<bool>((Func<bool, bool>) (item => !item));
    }

    public void Copy(Player.BoostPrayInfo info)
    {
      this.prayerId = info.prayerId;
      this.prayTargetId = info.prayTargetId;
      this.isBoostByTypes = info.isBoostByTypes;
    }
  }

  public class WEAPON_EFFECT_DATA
  {
    public GameObject effectObj;
    public EffectPlayProcessor.EffectSetting setting;
  }

  public enum CANNON_STATE
  {
    NONE,
    STANDBY,
    READY,
    CHARGE,
  }

  public class ShieldReflectInfo
  {
    public string attackInfoName;
    public int seId;
    public Vector3 offsetPos;
    public Vector3 offsetRot;
    public int damage;
    public int targetId;
  }

  public class HateInfo
  {
    public StageObject target;
    public int val;
  }

  public class SoulArrowInfo
  {
    public AttackHitInfo attackInfo;
    public TargetPoint point;
  }

  private class ShieldDamageData
  {
    public int shieldDamage;
    public int hpDamage;
    public bool isShieldEnd;
  }

  private class FixWeaponData
  {
    public Transform wepTrans;
    public Vector3 wepPos;
    public Quaternion wepRot;
    public bool enable;

    public void ClearData()
    {
      this.wepTrans = (Transform) null;
      this.wepPos = Vector3.zero;
      this.wepRot = Quaternion.identity;
      this.enable = false;
    }
  }
}
