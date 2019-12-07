using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : Character
{
	public enum SKILL_SE_TYPE
	{
		START,
		ACT
	}

	public enum SUB_ACTION_ID
	{
		AVOID = 13,
		STUMBLE,
		SHAKE,
		BLOW,
		FALL_BLOW,
		STUNNED_BLOW,
		GUARD,
		GUARD_DAMAGE,
		GUARD_PARRY,
		SKILL_ACTION,
		BATTLE_START,
		DEAD_LOOP,
		DEAD_STANDUP,
		PRAYER,
		CHANGE_WEAPON,
		GATHER,
		GRABBED,
		RESTRAINT,
		CANNON_STANDBY,
		CANNON_SHOT,
		SPECIAL_ACTION,
		GUARD_NO_KNOCKBACK,
		SONAR,
		WARP,
		EVOLVE,
		EVOLVE_SPECIAL,
		READ_STORY,
		FISHING,
		COOP_FISHING,
		FLICK_ACTION,
		STONE,
		CARRY,
		CARRY_PUT,
		TELEPORT_AVOID,
		CHARM_BLOW,
		POSE,
		RUSH_AVOID,
		MAX
	}

	public enum eContinueType
	{
		CONTINUE,
		RESCUE,
		AUTO_REVIVE,
		REACH_NEXT_WAVE
	}

	public enum SUB_MOTION_ID
	{
		AVOID = 115,
		STUMBLE,
		SHAKE,
		BLOW,
		FALL_BLOW,
		STUNNED_BLOW,
		GUARD,
		GUARD_WALK,
		GUARD_DAMAGE,
		BATTLE_START,
		DEAD_LOOP,
		DEAD_STANDUP,
		PRAYER,
		CHANGE_WEAPON,
		GRABBED,
		RESTRAINT,
		CANNON_ENTER,
		CANNON_LOOP,
		GUARD_NO_KNOCKBACK,
		GUARD_PARRY,
		WARP,
		AVOID_ALTER,
		WARP_ALTER,
		FISHING,
		COOP_FISHING,
		STONE,
		STONE_END,
		CARRY_LIFT,
		CARRY_IDLE,
		CARRY_WALK,
		CARRY_PUT,
		TELEPORT_AVOID,
		RUSH_AVOID,
		MAX
	}

	public enum ATTACK_MODE
	{
		NONE,
		ONE_HAND_SWORD,
		TWO_HAND_SWORD,
		SPEAR,
		PAIR_SWORDS,
		ARROW,
		MAX
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
		Failure
	}

	public enum RAIN_SHOT_STATE
	{
		NONE,
		START_CHARGE,
		FINISH_CHARGE,
		START_RAIN
	}

	public enum PRAY_REASON
	{
		NONE,
		DEAD,
		STONE
	}

	public class PrayInfo
	{
		public int targetId;

		public PRAY_REASON reason;
	}

	public enum BOOST_PRAY_TYPE
	{
		GUARD_ONE_HAND_SWORD_NORMAL,
		IN_BARRIER,
		MAX
	}

	[Serializable]
	public class BoostPrayInfo
	{
		public int prayerId;

		public int prayTargetId;

		public bool[] isBoostByTypes = new bool[2];

		public bool IsBoost()
		{
			return isBoostByTypes.Any((bool item) => item);
		}

		public bool IsNoBoost()
		{
			return isBoostByTypes.All((bool item) => !item);
		}

		public void Copy(BoostPrayInfo info)
		{
			prayerId = info.prayerId;
			prayTargetId = info.prayTargetId;
			isBoostByTypes = info.isBoostByTypes;
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
		CHARGE
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
			wepTrans = null;
			wepPos = Vector3.zero;
			wepRot = Quaternion.identity;
			enable = false;
		}
	}

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

	private const int ONEHANDSWORD_HEAT_REVENGEBURST_ATTACKID = 96;

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

	public static readonly string[] subMotionStateName;

	private static readonly int guardAngleID;

	private static readonly int arrowAngleID;

	private static readonly int rushAvoidAngleID;

	public float[] attackReachs = new float[6]
	{
		0f,
		5f,
		5.5f,
		6f,
		4f,
		15f
	};

	public float[] specialReachs = new float[6]
	{
		0f,
		0f,
		5.5f,
		8.5f,
		4f,
		20f
	};

	public float[] avoidAttackReachs = new float[6]
	{
		0f,
		0f,
		8.5f,
		0f,
		0f,
		0f
	};

	protected Transform _physics;

	public StageObjectManager.CreatePlayerInfo createInfo;

	public List<CharaInfo.EquipItem> equipWeaponList = new List<CharaInfo.EquipItem>();

	protected List<EquipItemTable.EquipItemData> weaponEquipItemDataList = new List<EquipItemTable.EquipItemData>();

	private XorFloat _playerAtk = 0f;

	private XorFloat _playerDef = 0f;

	private XorInt _playerHp = 0;

	public PlayerState baseState = new PlayerState();

	public EquipState weaponState = new EquipState();

	public EquipState skillConstState = new EquipState();

	public SkillData skillData = new SkillData();

	public AbilityData abilityData = new AbilityData();

	public List<AbilityItem> abilityItem = new List<AbilityItem>();

	private List<int> guardEquipDef = new List<int>();

	private XorInt _hpUp;

	private XorInt _healHp;

	private XorFloat _healHpSpeed;

	private XorFloat _addHp = 0f;

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

	protected eJumpState jumpState;

	protected bool isArrowSitShot;

	public RAIN_SHOT_STATE rainShotState;

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

	private List<BoostPrayInfo> boostPrayTargetInfoList = new List<BoostPrayInfo>();

	private List<BoostPrayInfo> boostPrayedInfoList = new List<BoostPrayInfo>();

	private XorInt _autoReviveHp = 0;

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

	private List<PrayInfo> prayerEndInfos = new List<PrayInfo>();

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

	private List<WEAPON_EFFECT_DATA> m_weaponEffectDataList = new List<WEAPON_EFFECT_DATA>();

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

	public ShieldReflectInfo shieldReflectInfo = new ShieldReflectInfo();

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

	private FixWeaponData fixWepData = new FixWeaponData();

	private Dictionary<uint, BulletControllerTurretBit> bulletTurretList = new Dictionary<uint, BulletControllerTurretBit>();

	private CircleShadow shadow;

	public override int id
	{
		get
		{
			return base.id;
		}
		set
		{
			base.id = value;
			base.gameObject.name = "Player:" + value;
		}
	}

	public PlayerLoader loader
	{
		get;
		private set;
	}

	public InGameSettingsManager.Player playerParameter
	{
		get;
		protected set;
	}

	public InGameSettingsManager.Player.WeaponInfo weaponInfo
	{
		get;
		protected set;
	}

	public InGameSettingsManager.BuffParamInfo buffParameter
	{
		get;
		protected set;
	}

	public InGameSettingsManager.DebuffParam debuffParameter
	{
		get;
		protected set;
	}

	public ATTACK_MODE attackMode
	{
		get;
		protected set;
	}

	public SP_ATTACK_TYPE spAttackType
	{
		get;
		protected set;
	}

	public EXTRA_ATTACK_TYPE extraAttackType
	{
		get;
		protected set;
	}

	public float attackReach
	{
		get
		{
			if (CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
			{
				return 5f;
			}
			if (CheckAttackModeAndSpType(ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL))
			{
				return 8f;
			}
			if (CheckAttackModeAndSpType(ATTACK_MODE.ARROW, SP_ATTACK_TYPE.SOUL))
			{
				return 10f;
			}
			return attackReachs[(int)attackMode] * 0.8f;
		}
	}

	public float specialReach
	{
		get
		{
			if (CheckAttackModeAndSpType(ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL))
			{
				return 10f;
			}
			if (CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
			{
				return 8f;
			}
			if (CheckAttackModeAndSpType(ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.BURST))
			{
				return 8f;
			}
			if (CheckAttackModeAndSpType(ATTACK_MODE.ARROW, SP_ATTACK_TYPE.SOUL))
			{
				return 10f;
			}
			return specialReachs[(int)attackMode];
		}
	}

	public float avoidAttackReach => avoidAttackReachs[(int)attackMode];

	public bool enableCancelToAvoid
	{
		get;
		protected set;
	}

	public bool enableCancelToMove
	{
		get;
		protected set;
	}

	public bool enableCancelToAttack
	{
		get;
		protected set;
	}

	public bool enableCancelToSkill
	{
		get;
		protected set;
	}

	public bool enableCancelToSpecialAction
	{
		get;
		protected set;
	}

	public bool enableCounterAttack
	{
		get;
		protected set;
	}

	public bool disableCounterAnimEvent
	{
		get;
		protected set;
	}

	public bool disableParryAction
	{
		get;
		protected set;
	}

	public bool disableGuard
	{
		get;
		protected set;
	}

	public bool enableAnimSeedRate
	{
		get;
		protected set;
	}

	public bool enableCancelToEvolveSpecialAction
	{
		get;
		protected set;
	}

	public bool enableCancelToCarryPut
	{
		get;
		protected set;
	}

	public PlayerPacketReceiver playerReceiver => (PlayerPacketReceiver)base.packetReceiver;

	public PlayerPacketSender playerSender => (PlayerPacketSender)base.packetSender;

	public float playerAtk
	{
		get
		{
			return _playerAtk;
		}
		protected set
		{
			_playerAtk = value;
		}
	}

	protected float playerDef
	{
		get
		{
			return _playerDef;
		}
		set
		{
			_playerDef = value;
		}
	}

	protected int defenseThreshold
	{
		get;
		set;
	}

	protected AtkAttribute defenseCoefficient
	{
		get;
		set;
	}

	protected int playerHp
	{
		get
		{
			return _playerHp;
		}
		set
		{
			_playerHp = value;
		}
	}

	public CharaInfo.EquipItem weaponData
	{
		get;
		protected set;
	}

	public int weaponIndex
	{
		get;
		protected set;
	}

	public int uniqueEquipmentIndex
	{
		get;
		protected set;
	}

	public int hpUp
	{
		get
		{
			return _hpUp;
		}
		set
		{
			_hpUp = value;
		}
	}

	public int healHp
	{
		get
		{
			return _healHp;
		}
		set
		{
			_healHp = value;
		}
	}

	public float healHpSpeed
	{
		get
		{
			return _healHpSpeed;
		}
		set
		{
			_healHpSpeed = value;
		}
	}

	public float addHp
	{
		get
		{
			return _addHp;
		}
		set
		{
			_addHp = value;
		}
	}

	public bool enableInputCombo
	{
		get;
		protected set;
	}

	public bool controllerInputCombo
	{
		get;
		protected set;
	}

	public bool enableComboTrans
	{
		get;
		protected set;
	}

	public bool enableInputCharge
	{
		get;
		private set;
	}

	public bool enableTap
	{
		get;
		protected set;
	}

	public bool enableFlickAction
	{
		get;
		protected set;
	}

	public bool enableInputNextTrigger
	{
		get;
		protected set;
	}

	public bool enableNextTriggerTrans
	{
		get;
		protected set;
	}

	public bool isCountLongTouch
	{
		get;
		protected set;
	}

	public float ChargeRate => chargeRate;

	public bool isStunnedLoop
	{
		get;
		protected set;
	}

	public SkillInfo skillInfo
	{
		get;
		protected set;
	}

	public bool isActSkillAction
	{
		get;
		protected set;
	}

	public bool isUsingSecondGradeSkill
	{
		get;
		protected set;
	}

	public bool isAbleToSkipSkillAction
	{
		get;
		protected set;
	}

	public bool isSkillCastState
	{
		get;
		protected set;
	}

	public bool isAppliedSkillParam
	{
		get;
		protected set;
	}

	public bool isActSpecialAction
	{
		get;
		protected set;
	}

	public bool HitSpearSpecialAction => hitSpearSpecialAction;

	public bool isAerial
	{
		get;
		protected set;
	}

	public bool isJumpAction => jumpState != eJumpState.None;

	public bool isArrowRainShot => rainShotState > RAIN_SHOT_STATE.NONE;

	public bool isHitSpAttack
	{
		get;
		set;
	}

	public bool enableSpAttackContinue
	{
		get;
		protected set;
	}

	public bool enableAttackNext
	{
		get;
		protected set;
	}

	public bool enableWeaponAction
	{
		get;
		protected set;
	}

	public bool isGuardAttackMode
	{
		get
		{
			if (CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
			{
				return false;
			}
			if (attackMode != ATTACK_MODE.ONE_HAND_SWORD)
			{
				return false;
			}
			return true;
		}
	}

	public bool isLongAttackMode
	{
		get
		{
			if (CheckAttackModeAndSpType(ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL))
			{
				return true;
			}
			if (CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
			{
				return true;
			}
			if (attackMode != ATTACK_MODE.ARROW)
			{
				return false;
			}
			return true;
		}
	}

	public bool isSpearAttackMode
	{
		get
		{
			if (attackMode != ATTACK_MODE.SPEAR)
			{
				return false;
			}
			return true;
		}
	}

	public bool isArrowAimBossMode
	{
		get;
		protected set;
	}

	public bool isArrowAimLesserMode
	{
		get;
		protected set;
	}

	public bool isArrowAimable
	{
		get;
		protected set;
	}

	public bool isArrowAimEnd
	{
		get;
		protected set;
	}

	public bool isArrowAimKeep
	{
		get;
		protected set;
	}

	public float rescueTime
	{
		get
		{
			if (!base.isDead)
			{
				return 0f;
			}
			if (deadStartTime < 0f)
			{
				return 0f;
			}
			if (deadStopTime > 0f)
			{
				return _rescueTime - (deadStopTime - deadStartTime);
			}
			if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				return 0f;
			}
			return _rescueTime - (Time.time - deadStartTime);
		}
	}

	public float stoneRescueTime
	{
		get
		{
			if (!IsStone())
			{
				return 0f;
			}
			if (stoneStartTime < 0f)
			{
				return 0f;
			}
			if (stoneStopTime > 0f)
			{
				return _stoneRescueTime - (stoneStopTime - stoneStartTime);
			}
			if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				return 0f;
			}
			return _stoneRescueTime - (Time.time - stoneStartTime);
		}
	}

	public int rescueCount
	{
		get;
		protected set;
	}

	public float continueTime
	{
		get;
		protected set;
	}

	public List<PrayInfo> prayTargetInfos
	{
		get;
		protected set;
	}

	public List<int> prayerIds
	{
		get;
		protected set;
	}

	public bool isRevivalEnabled
	{
		get
		{
			float num = 3f;
			if (playerParameter != null)
			{
				num = playerParameter.revivalTime;
			}
			return prayerTime > num;
		}
	}

	public float revivalTimePercent
	{
		get
		{
			float num = 3f;
			if (playerParameter != null)
			{
				num = playerParameter.revivalTime;
			}
			return prayerTime / num;
		}
	}

	public bool EnableRootMotion => enableRootMotion;

	public bool EnableEventMove => enableEventMove;

	public Vector3 EventMoveVelocity => eventMoveVelocity;

	public float EventMoveTimeCount => eventMoveTimeCount;

	public bool EnableAddForce => base.enableAddForce;

	public int autoReviveCount
	{
		get;
		protected set;
	}

	public int autoReviveHp
	{
		get
		{
			return _autoReviveHp;
		}
		set
		{
			_autoReviveHp = value;
		}
	}

	public bool isWaitingResurrectionHoming
	{
		get;
		protected set;
	}

	public bool isStopCounter
	{
		get;
		protected set;
	}

	public List<TargetPoint> targetingPointList
	{
		get;
		set;
	}

	public TargetPoint targetingPoint
	{
		get
		{
			if (targetingPointList == null || targetingPointList.Count <= 0)
			{
				return null;
			}
			return targetingPointList[0];
		}
	}

	public TargetPoint targetAimAfeterPoint
	{
		get;
		set;
	}

	public TargetPoint targetPointWithSpWeak
	{
		get
		{
			if (targetPointWithSpWeakList.IsNullOrEmpty())
			{
				return null;
			}
			return targetPointWithSpWeakList[0];
		}
	}

	public List<TargetPoint> arrowRainTargetPointList
	{
		get;
		set;
	}

	public bool isActedBattleStart
	{
		get;
		set;
	}

	public bool isWaitBattleStart
	{
		get;
		protected set;
	}

	public bool isNpc
	{
		get
		{
			if (base.controller != null)
			{
				return base.controller is NpcController;
			}
			return false;
		}
	}

	public int shotArrowCount
	{
		get;
		protected set;
	}

	public bool IsChangingWeapon => isChangingWeapon;

	public GatherPointObject targetGatherPoint
	{
		get;
		protected set;
	}

	public bool isAppliedGather
	{
		get;
		protected set;
	}

	public float healAtkRate
	{
		get;
		private set;
	}

	protected bool isAbsorbDamageSuperArmor
	{
		get;
		private set;
	}

	protected bool isInvincibleDamageSuperArmor
	{
		get;
		private set;
	}

	protected bool shouldShowInvincibleDamage
	{
		get;
		set;
	}

	public CANNON_STATE cannonState
	{
		get;
		private set;
	}

	public IFieldGimmickCannon targetFieldGimmickCannon
	{
		get;
		set;
	}

	public float[] spActionGauge
	{
		get;
		protected set;
	}

	public float CurrentWeaponSpActionGauge
	{
		get
		{
			if (spActionGauge.Length - 1 < weaponIndex || weaponIndex < 0)
			{
				return 0f;
			}
			return spActionGauge[weaponIndex];
		}
		protected set
		{
			if (spActionGauge.Length - 1 >= weaponIndex && weaponIndex >= 0)
			{
				spActionGauge[weaponIndex] = value;
				if (spActionGauge[weaponIndex] > spActionGaugeMax[weaponIndex])
				{
					spActionGauge[weaponIndex] = spActionGaugeMax[weaponIndex];
				}
			}
		}
	}

	public float[] spActionGaugeMax
	{
		get;
		protected set;
	}

	public float CurrentWeaponSpActionGaugeMax
	{
		get
		{
			if (spActionGauge.Length - 1 < weaponIndex || weaponIndex < 0)
			{
				return 0f;
			}
			return spActionGaugeMax[weaponIndex];
		}
		protected set
		{
			if (spActionGauge.Length - 1 >= weaponIndex && weaponIndex >= 0)
			{
				spActionGaugeMax[weaponIndex] = value;
			}
		}
	}

	public int attackHitCount
	{
		get;
		private set;
	}

	public bool isBoostMode
	{
		get;
		private set;
	}

	public int boostModeDamageUpLevel
	{
		get;
		private set;
	}

	public int boostModeDamageUpHitCount
	{
		get;
		private set;
	}

	public bool isBuffShadowSealing
	{
		get;
		private set;
	}

	private EnemyBrain bossBrain
	{
		get
		{
			if (_bossBrain != null)
			{
				return _bossBrain;
			}
			if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				return null;
			}
			Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
			if (boss == null)
			{
				return null;
			}
			_bossBrain = boss.GetComponent<EnemyBrain>();
			return _bossBrain;
		}
	}

	public bool isActOneHandSwordCounter
	{
		get;
		set;
	}

	public bool isActTwoHandSwordHeatCombo
	{
		get
		{
			if (base.attackID != 89)
			{
				return base.attackID == 88;
			}
			return true;
		}
	}

	public bool isActPairSwordsSoulLaser => base.attackID == playerParameter.pairSwordsActionInfo.Soul_SpLaserShotAttackId;

	public TargetPoint RestraintTargetPoint
	{
		get
		{
			if (!(m_attackRestraint != null))
			{
				return null;
			}
			return m_attackRestraint.BreakTargetPoint;
		}
	}

	public void SetDiableAction(ACTION_ID id, bool disable)
	{
		disableActionFlag.Set((int)id, disable);
	}

	public static ATTACK_MODE ConvertEquipmentTypeToAttackMode(EQUIPMENT_TYPE equipment_type)
	{
		ATTACK_MODE result = ATTACK_MODE.NONE;
		switch (equipment_type)
		{
		case EQUIPMENT_TYPE.ONE_HAND_SWORD:
			result = ATTACK_MODE.ONE_HAND_SWORD;
			break;
		case EQUIPMENT_TYPE.TWO_HAND_SWORD:
			result = ATTACK_MODE.TWO_HAND_SWORD;
			break;
		case EQUIPMENT_TYPE.SPEAR:
			result = ATTACK_MODE.SPEAR;
			break;
		case EQUIPMENT_TYPE.PAIR_SWORDS:
			result = ATTACK_MODE.PAIR_SWORDS;
			break;
		case EQUIPMENT_TYPE.ARROW:
			result = ATTACK_MODE.ARROW;
			break;
		}
		return result;
	}

	public static EQUIPMENT_TYPE ConvertAttackModeToEquipmentType(ATTACK_MODE mode)
	{
		EQUIPMENT_TYPE result = EQUIPMENT_TYPE.ONE_HAND_SWORD;
		switch (mode)
		{
		case ATTACK_MODE.ONE_HAND_SWORD:
			result = EQUIPMENT_TYPE.ONE_HAND_SWORD;
			break;
		case ATTACK_MODE.TWO_HAND_SWORD:
			result = EQUIPMENT_TYPE.TWO_HAND_SWORD;
			break;
		case ATTACK_MODE.SPEAR:
			result = EQUIPMENT_TYPE.SPEAR;
			break;
		case ATTACK_MODE.PAIR_SWORDS:
			result = EQUIPMENT_TYPE.PAIR_SWORDS;
			break;
		case ATTACK_MODE.ARROW:
			result = EQUIPMENT_TYPE.ARROW;
			break;
		}
		return result;
	}

	public void SetFlickDirection(SelfController.FLICK_DIRECTION direction)
	{
		flickDirection = direction;
	}

	public bool IsFullCharge()
	{
		return chargeRate >= 1f;
	}

	public bool IsExpandFullCharge()
	{
		return chargeExpandRate >= 1f;
	}

	public float GetExRushChargeRate()
	{
		return exRushChargeRate;
	}

	public float CalcChargeExpandElementDamageUpRate()
	{
		if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			return 1f;
		}
		InGameSettingsManager.Player.TwoHandSwordActionInfo twoHandSwordActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo;
		if (chargeExpandRate <= 0f)
		{
			return twoHandSwordActionInfo.elementDamageRateMin;
		}
		if (IsExpandFullCharge())
		{
			return twoHandSwordActionInfo.elementDamageRateFullCharge;
		}
		float num = twoHandSwordActionInfo.elementDamageRateMin + (twoHandSwordActionInfo.elementDamageRate - twoHandSwordActionInfo.elementDamageRateMin) * chargeExpandRate;
		if (num < twoHandSwordActionInfo.elementDamageRateMin)
		{
			num = twoHandSwordActionInfo.elementDamageRateMin;
		}
		return num;
	}

	public bool IsAbleToRescueByRemainRescueTime()
	{
		bool num = QuestManager.IsValidInGameExplore();
		int num2 = rescueCount;
		if (num)
		{
			return playerParameter.exploreRescureCount > num2;
		}
		if (playerParameter.rescueTimes.Length == 0)
		{
			return false;
		}
		if (num2 >= playerParameter.rescueTimes.Length)
		{
			num2 = playerParameter.rescueTimes.Length;
		}
		return playerParameter.rescueTimes[num2] > 0f;
	}

	public bool IsPrayed()
	{
		return prayerIds.Count > 0;
	}

	public bool IsBoostByType(BOOST_PRAY_TYPE type)
	{
		if (IsStone())
		{
			return false;
		}
		if (boostPrayedInfoList.IsNullOrEmpty())
		{
			return false;
		}
		if (boostPrayedInfoList.Count((BoostPrayInfo item) => item.isBoostByTypes[(int)type]) > 0)
		{
			return true;
		}
		return false;
	}

	public void SetEnableRootMotion(bool _isEnable)
	{
		enableRootMotion = _isEnable;
	}

	public void SetEnableEventMove(bool _isEnable)
	{
		enableEventMove = _isEnable;
	}

	public void SetEventMoveVelocity(Vector3 _velocity)
	{
		eventMoveVelocity = _velocity;
	}

	public void SetEventMoveTimeCount(float _timeCount)
	{
		eventMoveTimeCount = _timeCount;
	}

	public void SetEnableAddForce(bool _isEnable)
	{
		base.enableAddForce = _isEnable;
	}

	public bool IsAbleToAutoReviveBuff()
	{
		if (base.isDead)
		{
			return false;
		}
		if (IsValidBuff(BuffParam.BUFFTYPE.INVINCIBLECOUNT))
		{
			return false;
		}
		if (IsValidBuff(BuffParam.BUFFTYPE.INVINCIBLE_BADSTATUS))
		{
			return false;
		}
		return autoReviveCount < buffParameter.autoReviveMaxCount;
	}

	public bool IsAutoReviving()
	{
		return autoReviveHp > 0;
	}

	public float GetCannonChargeMax()
	{
		return inputCannonChargeMax;
	}

	public void SetCannonChargeMax(float max)
	{
		inputCannonChargeMax = max;
	}

	public float GetCannonChargeRate()
	{
		if (inputCannonChargeMax <= 0f)
		{
			return 0f;
		}
		return Mathf.Clamp01(inputCannonChargeCounter / inputCannonChargeMax);
	}

	public bool IsCannonFullCharged()
	{
		return inputCannonChargeCounter >= inputCannonChargeMax;
	}

	public void ClearCannonChargeRate()
	{
		inputCannonChargeCounter = 0f;
	}

	public override void ActShieldBreak()
	{
		if (buffParam.IsEnableBuff(BuffParam.BUFFTYPE.SHIELD))
		{
			OnBuffEnd(BuffParam.BUFFTYPE.SHIELD, sync: true);
		}
		base.ActShieldBreak();
	}

	private bool IsDiviedLoadAndInstantiate()
	{
		return !FieldManager.IsValidInGameNoQuest();
	}

	public void SetCannonState(CANNON_STATE cannonState)
	{
		this.cannonState = cannonState;
	}

	public bool IsAbleMoveCannon()
	{
		return cannonState == CANNON_STATE.READY;
	}

	public bool IsOnCannonMode()
	{
		return cannonState != CANNON_STATE.NONE;
	}

	public bool IsChargingCannon()
	{
		return cannonState == CANNON_STATE.CHARGE;
	}

	public bool IsValidSpActionGauge()
	{
		if (CurrentWeaponSpActionGaugeMax > 0f)
		{
			return !IsBurstTwoHandSword();
		}
		return false;
	}

	public bool IsValidSpActionMemori()
	{
		if (!CheckAttackModeAndSpType(ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.HEAT) && !CheckAttackModeAndSpType(ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.HEAT))
		{
			return CheckAttackModeAndSpType(ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL);
		}
		return true;
	}

	public bool IsValidBurstBulletUI()
	{
		return IsBurstTwoHandSword();
	}

	public bool IsBurstTwoHandSword()
	{
		return CheckAttackModeAndSpType(ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.BURST);
	}

	public bool IsOracleTwoHandSword()
	{
		return CheckAttackModeAndSpType(ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.ORACLE);
	}

	public void ClearStoreEffect()
	{
		if (!MonoBehaviourSingleton<EffectManager>.IsValid())
		{
			return;
		}
		if (!pairSwordsBoostModeAuraEffectList.IsNullOrEmpty())
		{
			for (int i = 0; i < pairSwordsBoostModeAuraEffectList.Count; i++)
			{
				if (!(pairSwordsBoostModeAuraEffectList[i] == null))
				{
					EffectManager.ReleaseEffect(pairSwordsBoostModeAuraEffectList[i].gameObject);
					pairSwordsBoostModeAuraEffectList[i] = null;
				}
			}
			pairSwordsBoostModeAuraEffectList.Clear();
		}
		if (!pairSwordsBoostModeTrailEffectList.IsNullOrEmpty())
		{
			for (int j = 0; j < pairSwordsBoostModeTrailEffectList.Count; j++)
			{
				if (!(pairSwordsBoostModeTrailEffectList[j] == null))
				{
					EffectManager.ReleaseEffect(pairSwordsBoostModeTrailEffectList[j].gameObject);
					pairSwordsBoostModeTrailEffectList[j] = null;
				}
			}
			pairSwordsBoostModeTrailEffectList.Clear();
		}
		effectTransTable.ForEach(delegate(Transform value)
		{
			ReleaseEffect(ref value);
		});
		effectTransTable.Clear();
		ReleaseEffect(ref twoHandSwordsChargeMaxEffect);
		ReleaseEffect(ref twoHandSwordsBoostLoopEffect);
		ReleaseEffect(ref ohsMaxChargeEffect);
		ReleaseEffect(ref buffShadowSealingEffect);
		ReleaseEffect(ref exRushChargeEffect);
	}

	private void ActivateStoredEffect()
	{
		if (!pairSwordsBoostModeAuraEffectList.IsNullOrEmpty())
		{
			for (int i = 0; i < pairSwordsBoostModeAuraEffectList.Count; i++)
			{
				if (!(pairSwordsBoostModeAuraEffectList[i] == null))
				{
					pairSwordsBoostModeAuraEffectList[i].gameObject.SetActive(value: true);
				}
			}
		}
		if (pairSwordsBoostModeTrailEffectList.IsNullOrEmpty())
		{
			return;
		}
		for (int j = 0; j < pairSwordsBoostModeTrailEffectList.Count; j++)
		{
			if (!(pairSwordsBoostModeTrailEffectList[j] == null))
			{
				pairSwordsBoostModeTrailEffectList[j].gameObject.SetActive(value: true);
				pairSwordsBoostModeTrailEffectList[j].GetComponentsInChildren(Temporary.trailList);
				for (int k = 0; k < Temporary.trailList.Count; k++)
				{
					Temporary.trailList[k].Reset();
				}
				Temporary.trailList.Clear();
			}
		}
	}

	private void DeactivateStoredEffect()
	{
		if (!pairSwordsBoostModeAuraEffectList.IsNullOrEmpty())
		{
			for (int i = 0; i < pairSwordsBoostModeAuraEffectList.Count; i++)
			{
				if (!(pairSwordsBoostModeAuraEffectList[i] == null))
				{
					pairSwordsBoostModeAuraEffectList[i].gameObject.SetActive(value: false);
				}
			}
		}
		if (pairSwordsBoostModeTrailEffectList.IsNullOrEmpty())
		{
			return;
		}
		for (int j = 0; j < pairSwordsBoostModeTrailEffectList.Count; j++)
		{
			if (!(pairSwordsBoostModeTrailEffectList[j] == null))
			{
				pairSwordsBoostModeTrailEffectList[j].gameObject.SetActive(value: false);
			}
		}
	}

	public EnemyHitTypeTable.TypeData GetOverrideHitEffect(AttackedHitStatusDirection status, ref Vector3 scale, ref float delay)
	{
		EnemyHitTypeTable.TypeData _type = null;
		if (IsSoulOneHandSwordBoostMode())
		{
			_type = new EnemyHitTypeTable.TypeData();
			InGameSettingsManager.Player.OneHandSwordActionInfo ohsActionInfo = playerParameter.ohsActionInfo;
			scale.Set(0.5f, 0.5f, 0.5f);
			if (!ohsActionInfo.Soul_BoostElementHitEffect.IsNullOrEmpty())
			{
				for (int i = 0; i < ohsActionInfo.Soul_BoostElementHitEffect.Length; i++)
				{
					_type.elementEffectNames[i] = ohsActionInfo.Soul_BoostElementHitEffect[i];
				}
			}
			return _type;
		}
		if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.COUNTER_BURST)
		{
			_type = new EnemyHitTypeTable.TypeData();
			InGameSettingsManager.Player.BurstOneHandSwordActionInfo burstOHSInfo = playerParameter.ohsActionInfo.burstOHSInfo;
			scale.Set(0.5f, 0.5f, 0.5f);
			if (!burstOHSInfo.BoostElementHitEffect.IsNullOrEmpty())
			{
				for (int j = 0; j < burstOHSInfo.BoostElementHitEffect.Length; j++)
				{
					_type.elementEffectNames[j] = burstOHSInfo.BoostElementHitEffect[j];
				}
			}
			return _type;
		}
		if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.BURST_THS_SINGLE_SHOT)
		{
			_type = new EnemyHitTypeTable.TypeData();
			InGameSettingsManager.Player.BurstTwoHandSwordActionInfo burstTHSInfo = playerParameter.twoHandSwordActionInfo.burstTHSInfo;
			if (!burstTHSInfo.HitEffect_SingleShot.IsNullOrEmpty())
			{
				for (int k = 0; k < burstTHSInfo.HitEffect_SingleShot.Length; k++)
				{
					_type.elementEffectNames[k] = burstTHSInfo.HitEffect_SingleShot[k];
				}
			}
			return _type;
		}
		if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.BURST_THS_FULL_BURST)
		{
			_type = new EnemyHitTypeTable.TypeData();
			InGameSettingsManager.Player.BurstTwoHandSwordActionInfo burstTHSInfo2 = playerParameter.twoHandSwordActionInfo.burstTHSInfo;
			if (!burstTHSInfo2.HitEffect_FullBurst.IsNullOrEmpty())
			{
				for (int l = 0; l < burstTHSInfo2.HitEffect_FullBurst.Length; l++)
				{
					_type.elementEffectNames[l] = burstTHSInfo2.HitEffect_FullBurst[l];
				}
			}
			return _type;
		}
		if (pairSwordsCtrl.IsOverrideHitEffect(ref _type, ref scale))
		{
			return _type;
		}
		if (spearCtrl.TryOverrideHitEffect(ref _type, ref scale))
		{
			return _type;
		}
		int num = 0;
		if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.JUMP)
		{
			num = useGaugeLevel;
		}
		else
		{
			if (!IsExRushDamageUpAttack(status.attackInfo.attackType) && !spearCtrl.IsSoulBoostMode())
			{
				return null;
			}
			num = 2;
		}
		InGameSettingsManager.Player.SpearActionInfo spearActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo;
		switch (num)
		{
		default:
			_type = Singleton<EnemyHitTypeTable>.I.GetData("TYPE_STAB_S", FieldManager.IsValidInGameNoQuest());
			break;
		case 1:
			_type = Singleton<EnemyHitTypeTable>.I.GetData("TYPE_STAB_L", FieldManager.IsValidInGameNoQuest());
			break;
		case 2:
			scale.Set(0.8f, 0.8f, 0.8f);
			goto case 3;
		case 3:
		{
			_type = new EnemyHitTypeTable.TypeData();
			int m = 0;
			for (int num2 = spearActionInfo.jumpHugeElementHitEffectNames.Length; m < num2; m++)
			{
				_type.elementEffectNames[m] = spearActionInfo.jumpHugeElementHitEffectNames[m];
			}
			for (int num3 = 6; m < num3; m++)
			{
				_type.elementEffectNames[m] = spearActionInfo.jumpHugeHitEffectName;
			}
			_type.baseEffectName = spearActionInfo.jumpHugeHitEffectName;
			break;
		}
		}
		if (num == 3)
		{
			delay = 0.15f;
			EffectManager.OneShot("ef_btl_wsk_spear_01_03", status.hitPos, Quaternion.identity, is_priority: true);
		}
		return _type;
	}

	public override float GetEffectScaleDependValue()
	{
		if (!CheckAttackModeAndSpType(ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.HEAT))
		{
			return 1f;
		}
		if (useGaugeLevel < 0 || useGaugeLevel >= MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.jumpWaveScales.Length)
		{
			return 1f;
		}
		return MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.jumpWaveScales[useGaugeLevel];
	}

	static Player()
	{
		subMotionStateName = new string[33]
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
		guardAngleID = 0;
		arrowAngleID = 0;
		rushAvoidAngleID = 0;
		guardAngleID = Animator.StringToHash("guard_angle");
		arrowAngleID = Animator.StringToHash("arrow_angle");
		rushAvoidAngleID = Animator.StringToHash("rush_avoid_angle");
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		if (true && MonoBehaviourSingleton<UIStatusGizmoManager>.IsValid())
		{
			uiPlayerStatusGizmo = MonoBehaviourSingleton<UIStatusGizmoManager>.I.Create(this);
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (uiPlayerStatusGizmo != null)
		{
			uiPlayerStatusGizmo.targetPlayer = null;
			uiPlayerStatusGizmo = null;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		base.objectType = OBJECT_TYPE.PLAYER;
		attackMode = ATTACK_MODE.NONE;
		spAttackType = SP_ATTACK_TYPE.NONE;
		extraAttackType = EXTRA_ATTACK_TYPE.NONE;
		weaponData = null;
		weaponIndex = -1;
		uniqueEquipmentIndex = -1;
		hpUp = 0;
		healHp = 0;
		healHpSpeed = 0f;
		enableInputCombo = false;
		controllerInputCombo = false;
		enableComboTrans = false;
		enableTap = false;
		enableFlickAction = false;
		enableInputNextTrigger = false;
		enableNextTriggerTrans = false;
		inputNextTriggerIndex = 0;
		inputNextTriggerFlag = false;
		isCountLongTouch = false;
		countLongTouchSec = 0f;
		isStunnedLoop = false;
		enableInputCharge = false;
		skillInfo = new SkillInfo(this);
		isActSkillAction = false;
		isAbleToSkipSkillAction = false;
		isSkillCastState = false;
		isAppliedSkillParam = false;
		isActSpecialAction = false;
		isArrowAimBossMode = false;
		isArrowAimLesserMode = false;
		isArrowAimable = false;
		isArrowAimEnd = false;
		isArrowAimKeep = false;
		isCanRushRelease = false;
		isLoopingRush = false;
		hitSpearSpecialAction = false;
		lockedSpearCancelAction = false;
		isSpearHundred = false;
		isSpearJumpAim = false;
		jumpActionCounter = 0f;
		jumpState = eJumpState.None;
		useGaugeLevel = 0;
		isAerial = false;
		rainShotState = RAIN_SHOT_STATE.NONE;
		_rescueTime = 0f;
		_stoneRescueTime = 0f;
		rescueCount = 0;
		continueTime = 0f;
		deadStartTime = -1f;
		deadStopTime = -1f;
		stoneStartTime = -1f;
		stoneStopTime = -1f;
		autoReviveCount = 0;
		autoReviveHp = 0;
		isStopCounter = false;
		buffInfoListOnActDeadStandUp = new List<KeyValuePair<int, SkillInfo.SkillParam>>();
		isWaitingResurrectionHoming = false;
		prayTargetInfos = new List<PrayInfo>();
		prayerTime = 0f;
		prayerIds = new List<int>();
		boostPrayTargetInfoList.Clear();
		boostPrayedInfoList.Clear();
		healEffectTransform = null;
		skillChargeEffectTransform = null;
		targetingPointList = new List<TargetPoint>();
		arrowRainTargetPointList = new List<TargetPoint>();
		isActedBattleStart = false;
		isWaitBattleStart = false;
		shotArrowCount = 0;
		isSyncingCannonRotation = false;
		cannonState = CANNON_STATE.NONE;
		isAnimEventStatusUpDefence = false;
		animEventStatusUpDefenceRate = 1f;
		isHitSpAttack = false;
		spActionGauge = new float[3];
		spActionGaugeMax = new float[3];
		attackHitCount = 0;
		isBoostMode = false;
		boostModeDamageUpLevel = 0;
		boostModeDamageUpHitCount = 0;
		_bossBrain = null;
		isJustGuard = false;
		isSuccessParry = false;
		guardingSec = 0f;
		isBuffShadowSealing = false;
		healAtkRate = 0f;
		localDecoyId = 0;
		isAbsorbDamageSuperArmor = false;
		isInvincibleDamageSuperArmor = false;
		shouldShowInvincibleDamage = false;
		enabledTeleportAvoid = false;
		enabledRushAvoid = false;
		extraSpGaugeDecreasingRate = 0f;
		enabledOraclePairSwordsSP = false;
		evolveCtrl = new EvolveController();
		snatchCtrl = new SnatchController();
		fishingCtrl = new FishingController();
		m_weaponCtrlList.Clear();
		ohsCtrl = new OneHandSwordController();
		m_weaponCtrlList.Add(ohsCtrl);
		pairSwordsCtrl = new PairSwordsController();
		m_weaponCtrlList.Add(pairSwordsCtrl);
		spearCtrl = new SpearController();
		m_weaponCtrlList.Add(spearCtrl);
		thsCtrl = new TwoHandSwordController();
		m_weaponCtrlList.Add(thsCtrl);
		loader = base.gameObject.AddComponent<PlayerLoader>();
		base.gameObject.layer = 8;
		if (base._rigidbody == null)
		{
			base._rigidbody = base.gameObject.AddComponent<Rigidbody>();
		}
		base._rigidbody.mass = 1f;
		base._rigidbody.angularDrag = 100f;
		base._rigidbody.isKinematic = false;
		base._rigidbody.constraints = (RigidbodyConstraints)116;
		base._rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			playerParameter = MonoBehaviourSingleton<InGameSettingsManager>.I.player;
			base.damageHealRate = playerParameter.damegeHealRate;
			buffParameter = MonoBehaviourSingleton<InGameSettingsManager>.I.buff;
			debuffParameter = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff;
		}
		if (playerParameter != null)
		{
			if (base.hpMax == 0)
			{
				int num = healHp = playerParameter.hpMax;
				int num4 = base.hp = (base.hpMax = num);
				healHpSpeed = playerParameter.hpHealSpeed;
			}
			moveRotateMaxSpeed = playerParameter.moveRotateMaxSpeed;
		}
		playerAtk = 0f;
		playerDef = 0f;
		playerHp = 0;
		for (int i = 0; i < 7; i++)
		{
			baseState.atkList.Add(0);
			baseState.defList.Add(0);
			weaponState.atkList.Add(0);
			weaponState.defList.Add(0);
			skillConstState.atkList.Add(0);
			skillConstState.defList.Add(0);
			guardEquipDef.Add(0);
		}
		weaponState.hp = 0;
		skillConstState.hp = 0;
		defenseCoefficient = new AtkAttribute();
		defenseThreshold = 0;
		buffParam.passive.Reset();
		evolveCtrl.Init(this);
		buffParam.ownerEvolveCtrl = evolveCtrl;
		snatchCtrl.Init(this, base.gameObject.AddComponent<SnatchLineRenderer>());
		int j = 0;
		for (int count = m_weaponCtrlList.Count; j < count; j++)
		{
			m_weaponCtrlList[j].Init(this);
		}
		fishingCtrl.Initialize(this);
		TwoHandSwordController.InitParam param = new TwoHandSwordController.InitParam
		{
			Owner = this,
			BurstInitParam = new TwoHandSwordBurstController.InitParam
			{
				Owner = this,
				ActionInfo = playerParameter.twoHandSwordActionInfo,
				MaxBulletCount = 6,
				CurrentRestBullets = null
			}
		};
		thsCtrl.InitAppend(param);
	}

	public override void OnLoadComplete()
	{
		if (base._collider == null)
		{
			CapsuleCollider capsuleCollider = base.gameObject.AddComponent<CapsuleCollider>();
			capsuleCollider.direction = 1;
			capsuleCollider.height = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.height;
			capsuleCollider.radius = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.radius;
			capsuleCollider.center = new Vector3(0f, capsuleCollider.height * 0.5f, 0f);
			capsuleCollider.material.dynamicFriction = MonoBehaviourSingleton<InGameSettingsManager>.I.player.friction;
			capsuleCollider.material.staticFriction = MonoBehaviourSingleton<InGameSettingsManager>.I.player.friction;
			capsuleCollider.material.frictionCombine = PhysicMaterialCombine.Minimum;
			base._collider = capsuleCollider;
		}
		base.OnLoadComplete();
		SetAnimUpdatePhysics(this is Self);
		if (stepCtrl != null)
		{
			stepCtrl.stampDistance = playerParameter.stampDistance;
		}
		_physics = Utility.Find(base.transform, "SoftPhysics");
		if (_physics != null && MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			_physics.transform.parent = MonoBehaviourSingleton<StageObjectManager>.I.physicsRoot;
		}
		if (initBuffSyncParam != null)
		{
			buffParam.SetSyncParam(initBuffSyncParam);
			initBuffSyncParam = null;
		}
		else
		{
			buffParam.PlayBuffLoopEffectAll();
		}
		if (base.hp <= 0 && isInitDead)
		{
			ActDeadLoop(is_init_rescue: true, initRescueTime, initContinueTime);
			isInitDead = false;
		}
		snatchCtrl.OnLoadComplete();
		int i = 0;
		for (int count = m_weaponCtrlList.Count; i < count; i++)
		{
			m_weaponCtrlList[i].OnLoadComplete();
		}
		if (pairSwordsCtrl != null)
		{
			pairSwordsCtrl.ResetCombineMode();
		}
	}

	protected override uint GetVoiceChannel()
	{
		if (MonoBehaviourSingleton<SoundManager>.IsValid())
		{
			return MonoBehaviourSingleton<SoundManager>.I.GetVoiceChannel(this);
		}
		return 0u;
	}

	protected override bool EnablePlaySound()
	{
		if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			return true;
		}
		if (MonoBehaviourSingleton<InGameProgress>.I.isHappenQuestDirection)
		{
			return false;
		}
		return true;
	}

	public override bool DestroyObject()
	{
		if (base.isLoading)
		{
			base.isDestroyWaitFlag = true;
			return false;
		}
		return base.DestroyObject();
	}

	protected override void Update()
	{
		if (enableInputCharge)
		{
			inputChargeTimeCounter += Time.deltaTime;
			if (inputChargeTimeCounter >= inputChargeTimeMax)
			{
				if (inputChargeAutoRelease && (IsCoopNone() || IsOriginal()))
				{
					SetChargeRelease(1f);
				}
				if (inputChargeMaxTiming)
				{
					ExecChargeMaxOnce();
				}
			}
		}
		if (enableInputCharge && CheckAttackModeAndSpType(ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.NONE) && !isChargeExpanding && inputChargeTimeCounter >= inputChargeTimeMax)
		{
			SetNextTrigger(1);
		}
		if (isChargeExpanding)
		{
			timerChargeExpand += Time.deltaTime;
			if (isChargeExpandAutoRelease && timerChargeExpand >= timeChargeExpandMax && (IsCoopNone() || IsOriginal()))
			{
				SetChargeExpandRelease(1f);
			}
		}
		if (isCountLongTouch && enableTap)
		{
			countLongTouchSec += Time.deltaTime;
		}
		if (isActSpecialAction)
		{
			actSpecialActionTimer += Time.deltaTime;
		}
		UpdateCannonCharge();
		UpdateSpearAction();
		snatchCtrl.Update();
		if (fishingCtrl != null)
		{
			fishingCtrl.Update();
		}
		int i = 0;
		for (int count = m_weaponCtrlList.Count; i < count; i++)
		{
			m_weaponCtrlList[i].Update();
		}
		if (_IsGuard() || spearCtrl.IsGuard())
		{
			_UpdateGuard();
		}
		if (isHitSpAttack)
		{
			hitSpAttackContinueTimer += Time.deltaTime;
		}
		if (isSkillCastLoop)
		{
			CheckSkillCastLoop();
		}
		if (isArrowAimBossMode)
		{
			UpdateArrowAngle();
		}
		CheckBuffShadowSealing();
		if (healHp > base.hp && !buffParam.IsValidBuff(BuffParam.BUFFTYPE.CANT_HEAL_HP) && (float)buffParam.GetValue(BuffParam.BUFFTYPE.POISON) <= 0f && (float)buffParam.GetValue(BuffParam.BUFFTYPE.DEADLY_POISON) <= 0f && (float)buffParam.GetValue(BuffParam.BUFFTYPE.BURNING) <= 0f && (float)buffParam.GetValue(BuffParam.BUFFTYPE.ACID) <= 0f && !isProgressStop())
		{
			addHp += healHpSpeed * buffParam.GetHealSpeedUp() * _GetGuardingHealSpeedUp() * Time.deltaTime;
			int num = (int)addHp;
			addHp -= num;
			base.hp += num;
			if (base.hp > healHp)
			{
				base.hp = healHp;
			}
		}
		skillInfo.OnUpdate();
		UpdateSpActionGauge();
		CheckContinueBoostMode();
		UpdateEvolve();
		if (IsOriginal() || IsCoopNone())
		{
			bool flag = base.actionID == ACTION_ID.IDLE || base.actionID == ACTION_ID.MOVE || base.actionID == ACTION_ID.ATTACK || base.actionID == (ACTION_ID)19 || base.actionID == (ACTION_ID)20 || base.actionID == (ACTION_ID)34 || base.actionID == (ACTION_ID)21 || base.actionID == (ACTION_ID)26 || base.actionID == (ACTION_ID)33 || base.actionID == (ACTION_ID)22 || base.actionID == ACTION_ID.MAX || base.actionID == (ACTION_ID)37;
			prayerEndInfos.Clear();
			int j = 0;
			for (int count2 = prayTargetInfos.Count; j < count2; j++)
			{
				if (!flag)
				{
					prayerEndInfos.Add(prayTargetInfos[j]);
					continue;
				}
				Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(prayTargetInfos[j].targetId) as Player;
				if (player == null)
				{
					prayerEndInfos.Add(prayTargetInfos[j]);
					continue;
				}
				if (Vector3.Distance(player.transform.position, base.transform.position) > playerParameter.revivalRange)
				{
					prayerEndInfos.Add(prayTargetInfos[j]);
					continue;
				}
				switch (prayTargetInfos[j].reason)
				{
				case PRAY_REASON.DEAD:
					if (!player.isDead || player.rescueTime <= 0f)
					{
						prayerEndInfos.Add(prayTargetInfos[j]);
					}
					break;
				case PRAY_REASON.STONE:
					if (!player.IsStone() || player.stoneRescueTime <= 0f)
					{
						prayerEndInfos.Add(prayTargetInfos[j]);
					}
					break;
				}
			}
			int k = 0;
			for (int count3 = prayerEndInfos.Count; k < count3; k++)
			{
				OnPrayerEnd(prayerEndInfos[k]);
			}
			if (flag)
			{
				int l = 0;
				for (int count4 = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count; l < count4; l++)
				{
					Player player2 = MonoBehaviourSingleton<StageObjectManager>.I.playerList[l] as Player;
					if (player2 == this || Vector3.Distance(player2.transform.position, base.transform.position) > playerParameter.revivalRange)
					{
						continue;
					}
					bool flag2 = false;
					int m = 0;
					for (int count5 = prayTargetInfos.Count; m < count5; m++)
					{
						if (prayTargetInfos[m].targetId == player2.id)
						{
							flag2 = true;
							break;
						}
					}
					if (flag2)
					{
						CheckPrayerBoost(player2);
						continue;
					}
					PrayInfo prayInfo = new PrayInfo();
					prayInfo.reason = PRAY_REASON.NONE;
					if (player2.isDead && player2.actionID == (ACTION_ID)24 && player2.rescueTime > 0f)
					{
						prayInfo.reason = PRAY_REASON.DEAD;
					}
					else if (player2.IsStone() && player2.stoneRescueTime > 0f)
					{
						prayInfo.reason = PRAY_REASON.STONE;
					}
					if (prayInfo.reason != 0)
					{
						prayInfo.targetId = player2.id;
						OnPrayerStart(prayInfo);
					}
				}
			}
		}
		if (IsOnCannonMode())
		{
			if (targetFieldGimmickCannon != null)
			{
				_position = targetFieldGimmickCannon.GetPosition();
			}
			if (isSyncingCannonRotation)
			{
				UpdateSyncCannonRotation();
			}
		}
		base.Update();
	}

	protected override void LateUpdate()
	{
		if (fixWepData.enable)
		{
			fixWepData.wepTrans.position = fixWepData.wepPos;
			fixWepData.wepTrans.rotation = fixWepData.wepRot;
		}
	}

	protected virtual void OnDestroy()
	{
		if (base._collider != null)
		{
			UnityEngine.Object.Destroy(base._collider.material);
		}
		if (_physics != null)
		{
			UnityEngine.Object.Destroy(_physics.gameObject);
		}
		_physics = null;
		if (fishingCtrl != null)
		{
			fishingCtrl.TryFinalize();
		}
		fishingCtrl = null;
		if (activeBulletBarrierObject != null)
		{
			activeBulletBarrierObject.DestroyObject();
		}
		activeBulletBarrierObject = null;
		if (buffParam != null && buffParam.substituteCtrl != null)
		{
			buffParam.substituteCtrl.End();
		}
		ClearStoreEffect();
		RemoveAllBulletTurret();
		EndCarry();
	}

	protected override void FixedUpdate()
	{
		if (base.actionID == (ACTION_ID)29)
		{
			ActGrabbedUpdate();
			return;
		}
		if (base.actionID == (ACTION_ID)30)
		{
			UpdateRestraint();
			return;
		}
		if (base.actionID == (ACTION_ID)43)
		{
			UpdateStone();
			return;
		}
		if (!pairSwordsBoostModeAuraEffectList.IsNullOrEmpty())
		{
			for (int i = 0; i < pairSwordsBoostModeAuraEffectList.Count; i++)
			{
				if (!(pairSwordsBoostModeAuraEffectList[i] == null))
				{
					pairSwordsBoostModeAuraEffectList[i].position = _position;
				}
			}
		}
		if (enableRotateToTargetPoint)
		{
			Vector3 forward = _forward;
			if (GetTargetPos(out Vector3 pos))
			{
				pos.y = 0f;
				forward = pos - _position;
			}
			else if (base.targetPointPos != Vector3.zero)
			{
				pos = base.targetPointPos;
				pos.y = 0f;
				forward = pos - _position;
			}
			else if (StageObjectManager.CanTargetBoss)
			{
				pos = GetTargetPosition(MonoBehaviourSingleton<StageObjectManager>.I.boss);
				pos.y = 0f;
				forward = pos - _position;
			}
			if (CheckAttackModeAndSpType(ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL) && targetPointWithSpWeak != null)
			{
				pos = targetPointWithSpWeak.param.markerPos;
				pos.y = 0f;
				forward = pos - _position;
			}
			rotateEventDirection = Quaternion.LookRotation(forward).eulerAngles.y;
			if (!periodicSyncActionPositionFlag)
			{
				enableRotateToTargetPoint = false;
			}
			Vector3 forward2 = _forward;
			forward2.y = 0f;
			forward2.Normalize();
			Vector3 vector = Quaternion.AngleAxis(rotateEventDirection, Vector3.up) * Vector3.forward;
			int num = (Vector3.Cross(forward2, vector).y >= 0f) ? 1 : (-1);
			float num2 = Vector3.Angle(forward2, vector);
			Vector3 eulerAngles = _rotation.eulerAngles;
			_rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y + (float)num * num2, eulerAngles.z);
		}
		base.FixedUpdate();
		FixedUpdateOneHandSword();
		CapsuleCollider capsuleCollider = base._collider as CapsuleCollider;
		if (capsuleCollider != null)
		{
			capsuleCollider.center = new Vector3(0f, capsuleCollider.height * 0.5f - _position.y, 0f);
		}
	}

	protected override void FixedUpdatePhysics()
	{
		if (!base.isInitialized)
		{
			return;
		}
		Vector3 position = _position;
		float height = StageManager.GetHeight(position);
		switch (base.actionID)
		{
		case (ACTION_ID)14:
			if (Time.time - stumbleEndTime > 0f)
			{
				SetNextTrigger();
			}
			break;
		case (ACTION_ID)15:
			if (Time.time - shakeEndTime > 0f)
			{
				SetNextTrigger();
			}
			break;
		case (ACTION_ID)16:
		case (ACTION_ID)17:
		case (ACTION_ID)18:
		case (ACTION_ID)47:
			if (!base.waitAddForce && (base._rigidbody.constraints & RigidbodyConstraints.FreezePositionY) == 0 && position.y <= height + 0.03f && base._rigidbody.velocity.y <= 0f)
			{
				ResetIgnoreColliders();
				SetNextTrigger();
			}
			if (isStunnedLoop && stunnedEndTime - Time.time <= 0f)
			{
				SetStunnedEnd();
			}
			break;
		}
		base.FixedUpdatePhysics();
	}

	protected override float GetAnimatorSpeed()
	{
		float num = base.GetAnimatorSpeed();
		if (IsOracleTwoHandSword() && thsCtrl.oracleCtrl.IsHorizontalAttack)
		{
			num *= thsCtrl.oracleCtrl.GetHorizontalSpeed();
		}
		num += GetBoostAttackSpeedUp();
		num += GetAttackModeWalkSpeedUp();
		if (num <= 0f || !enableAnimSeedRate)
		{
			return num;
		}
		if (isLoopingRush)
		{
			float b = num * GetRushDistanceRate();
			return Mathf.Max(0f, b);
		}
		if (IsBurstTwoHandSword() && thsCtrl != null && thsCtrl.IsEnableChangeReloadMotionSpeed)
		{
			return GetBurstReloadMotionSpeedRate();
		}
		float num2 = 0f;
		switch (attackMode)
		{
		case ATTACK_MODE.TWO_HAND_SWORD:
			if (spAttackType != SP_ATTACK_TYPE.ORACLE)
			{
				num2 = buffParam.GetChargeSwordsTimeRate();
			}
			break;
		case ATTACK_MODE.ARROW:
			num2 = GetChargeArrowTimeRate();
			break;
		case ATTACK_MODE.PAIR_SWORDS:
			num2 = buffParam.GetChargePairSwordsTimeRate();
			break;
		}
		if (num2 >= 1f)
		{
			num2 = playerParameter.animatorSpeedMaxTimeRate;
		}
		return 1f / (1f - num2);
	}

	public override bool IsChangeableAction(ACTION_ID action_id)
	{
		if (disableActionFlag.Get((int)action_id))
		{
			return false;
		}
		switch (action_id)
		{
		case ACTION_ID.ATTACK:
			if (enableCancelToAttack && hitSpearSpecialAction && !lockedSpearCancelAction)
			{
				return true;
			}
			if (actSpecialActionTimer > 0f && CheckAttackModeAndSpType(ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.NONE))
			{
				lockedSpearCancelAction = true;
				return false;
			}
			if (enableCancelToAttack && spearCtrl.IsEnableBurstCombo())
			{
				return true;
			}
			if (IsChangeableActionOnSpAttack())
			{
				return true;
			}
			if (IsChangeableActionOnAttackNext())
			{
				return true;
			}
			if (enableCancelToAttack)
			{
				return true;
			}
			break;
		case ACTION_ID.MOVE:
			if (enableCancelToMove)
			{
				return true;
			}
			break;
		case ACTION_ID.MAX:
		case (ACTION_ID)36:
		case (ACTION_ID)46:
		case (ACTION_ID)49:
			if (enableCancelToAvoid)
			{
				return true;
			}
			break;
		case (ACTION_ID)22:
		case (ACTION_ID)37:
			if (enableCancelToSkill)
			{
				return true;
			}
			break;
		case (ACTION_ID)33:
			if (IsChangableActionOnWeaponAction())
			{
				return true;
			}
			if (enableCancelToSpecialAction)
			{
				return true;
			}
			break;
		case (ACTION_ID)38:
			if (enableCancelToEvolveSpecialAction)
			{
				return true;
			}
			break;
		case (ACTION_ID)29:
			if (base.actionID == (ACTION_ID)31 || base.actionID == (ACTION_ID)32)
			{
				return true;
			}
			break;
		case (ACTION_ID)42:
			if (enableFlickAction)
			{
				return true;
			}
			break;
		}
		return base.IsChangeableAction(action_id);
	}

	public float GetChargeArrowTimeRate()
	{
		float result = 0f;
		switch (spAttackType)
		{
		case SP_ATTACK_TYPE.HEAT:
			if (isBuffShadowSealing)
			{
				result = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.shadowSealingBuffChargeRate;
				break;
			}
			result = buffParam.GetChargeHeatArrowTimeRate();
			if (MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.ignoreSpAttackTypeAbility)
			{
				result += buffParam.GetChargeArrowTimeRate();
			}
			break;
		case SP_ATTACK_TYPE.NONE:
			result = buffParam.GetChargeArrowTimeRate();
			break;
		case SP_ATTACK_TYPE.BURST:
			result = buffParam.GetChargeArrowTimeRate();
			break;
		default:
			return result;
		}
		if (isArrowSitShot)
		{
			result += MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.sitShotChargeSpeedUpRate;
		}
		if (result >= 0.85f)
		{
			result = 0.85f;
		}
		return result;
	}

	public float GetOracleChargeTimeRate()
	{
		switch (base.attackID)
		{
		case 62:
			return buffParam.GetOracleDiveSmashChargeTimeRate();
		case 64:
			return buffParam.GetOracleWheelSmashChargeTimeRate();
		case 63:
			return buffParam.GetOracleSpinSmashChargeTimeRate();
		default:
			return 0f;
		}
	}

	public bool CheckAttackMode(ATTACK_MODE mode)
	{
		return attackMode == mode;
	}

	public bool CheckSpAttackType(SP_ATTACK_TYPE type)
	{
		return spAttackType == type;
	}

	public bool CheckAttackModeAndSpType(ATTACK_MODE mode, SP_ATTACK_TYPE type)
	{
		if (attackMode == mode)
		{
			return spAttackType == type;
		}
		return false;
	}

	public ELEMENT_TYPE GetNowWeaponElement()
	{
		if (weaponState == null)
		{
			return ELEMENT_TYPE.MAX;
		}
		ELEMENT_TYPE result = ELEMENT_TYPE.MAX;
		int num = 0;
		int i = 1;
		for (int count = weaponState.atkList.Count; i < count; i++)
		{
			if (weaponState.atkList[i] > num)
			{
				num = weaponState.atkList[i];
				result = (ELEMENT_TYPE)(i - 1);
			}
		}
		return result;
	}

	public int GetNormalAttackId(ATTACK_MODE mode, SP_ATTACK_TYPE type, EXTRA_ATTACK_TYPE exType, out string _motionLayerName)
	{
		int _attackId = 0;
		_motionLayerName = "Base Layer.";
		switch (mode)
		{
		case ATTACK_MODE.ONE_HAND_SWORD:
			if (ohsCtrl != null)
			{
				ohsCtrl.GetNormalAttackId(type, exType, ref _attackId, ref _motionLayerName);
			}
			break;
		case ATTACK_MODE.TWO_HAND_SWORD:
			if (thsCtrl != null)
			{
				thsCtrl.GetNormalAttackId(type, exType, ref _attackId, ref _motionLayerName);
			}
			break;
		case ATTACK_MODE.SPEAR:
			if (spearCtrl != null)
			{
				spearCtrl.GetNormalAttackId(type, exType, ref _attackId, ref _motionLayerName);
			}
			break;
		case ATTACK_MODE.PAIR_SWORDS:
			switch (type)
			{
			case SP_ATTACK_TYPE.HEAT:
				_attackId = playerParameter.pairSwordsActionInfo.Heat_AttackId;
				break;
			case SP_ATTACK_TYPE.SOUL:
				_attackId = playerParameter.pairSwordsActionInfo.Soul_AttackId;
				break;
			case SP_ATTACK_TYPE.BURST:
				_attackId = pairSwordsCtrl.GetBurstAttackId();
				break;
			case SP_ATTACK_TYPE.ORACLE:
				_attackId = 40;
				_motionLayerName = GetMotionLayerName(mode, type, _attackId);
				break;
			}
			break;
		case ATTACK_MODE.ARROW:
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
		if (MonoBehaviourSingleton<EffectManager>.IsValid() && (object)t != null)
		{
			EffectManager.ReleaseEffect(t.gameObject, isPlayEndAnimation);
			t = null;
		}
	}

	private bool IsChangeableActionOnSpAttack()
	{
		bool flag = false;
		switch (attackMode)
		{
		case ATTACK_MODE.ONE_HAND_SWORD:
			if (spAttackType == SP_ATTACK_TYPE.BURST)
			{
				if (enableSpAttackContinue)
				{
					flag = true;
				}
			}
			else if (spAttackType == SP_ATTACK_TYPE.ORACLE && enableSpAttackContinue)
			{
				flag = true;
			}
			break;
		case ATTACK_MODE.TWO_HAND_SWORD:
			if ((spAttackType == SP_ATTACK_TYPE.NONE || spAttackType == SP_ATTACK_TYPE.HEAT || spAttackType == SP_ATTACK_TYPE.BURST || spAttackType == SP_ATTACK_TYPE.ORACLE) && enableSpAttackContinue)
			{
				flag = true;
			}
			if (!flag && spAttackType == SP_ATTACK_TYPE.HEAT)
			{
				if (!isActSpecialAction || isLockedSpAttackContinue)
				{
					break;
				}
				isLockedSpAttackContinue = true;
				if (!IsFullCharge() || !isHitSpAttack || (thsCtrl != null && thsCtrl.IsTwoHandSwordSpAttackContinueTimeOut(hitSpAttackContinueTimer)))
				{
					break;
				}
				flag = true;
			}
			if (!flag && spAttackType == SP_ATTACK_TYPE.ORACLE && thsCtrl.oracleCtrl.IsHorizontalAttack)
			{
				thsCtrl.oracleCtrl.CheckNextHorizontal();
				flag = true;
			}
			break;
		case ATTACK_MODE.PAIR_SWORDS:
			if (enableSpAttackContinue)
			{
				flag = true;
			}
			break;
		case ATTACK_MODE.SPEAR:
			if (enableSpAttackContinue)
			{
				flag = true;
			}
			break;
		case ATTACK_MODE.ARROW:
			if (enableSpAttackContinue && (spAttackType == SP_ATTACK_TYPE.NONE || spAttackType == SP_ATTACK_TYPE.HEAT || spAttackType == SP_ATTACK_TYPE.BURST))
			{
				flag = true;
			}
			break;
		}
		return flag;
	}

	private bool IsChangeableActionOnAttackNext()
	{
		bool result = false;
		switch (attackMode)
		{
		case ATTACK_MODE.PAIR_SWORDS:
			if (spAttackType == SP_ATTACK_TYPE.SOUL && enableAttackNext)
			{
				result = true;
			}
			break;
		case ATTACK_MODE.SPEAR:
			if (CheckSpAttackType(SP_ATTACK_TYPE.BURST) && enableAttackNext)
			{
				result = true;
			}
			break;
		}
		return result;
	}

	private bool IsChangableActionOnWeaponAction()
	{
		bool result = false;
		ATTACK_MODE attackMode = this.attackMode;
		if (attackMode == ATTACK_MODE.SPEAR && CheckSpAttackType(SP_ATTACK_TYPE.BURST) && enableWeaponAction)
		{
			result = true;
		}
		return result;
	}

	protected override string ReplaceMotionLayer(int motionId, string layerName = "Base Layer.")
	{
		if (layerName.Equals("Base Layer."))
		{
			if (CheckAttackModeAndSpType(ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE))
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
				case 127:
					layerName += "ORACLE.";
					break;
				}
			}
			else if (CheckAttackModeAndSpType(ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.ORACLE) && motionId == 116)
			{
				layerName += "ORACLE.";
			}
		}
		return layerName;
	}

	public new bool PlayMotionImmidate(string ctrlName, string stateName, float _transition_time = -1f)
	{
		return _PlayMotion("Base Layer." + stateName, ctrlName, _transition_time);
	}

	protected override bool CanSafeActIdle()
	{
		if (fishingCtrl != null && fishingCtrl.IsFishing())
		{
			return false;
		}
		return true;
	}

	public override void ActIdle(bool is_sync = false, float transitionTimer = -1f)
	{
		if (base.actionID == (ACTION_ID)29)
		{
			ActGrabbedEnd();
			return;
		}
		if (base.actionID == (ACTION_ID)30)
		{
			ActRestraintEnd();
			return;
		}
		base.ActIdle(is_sync, transitionTimer);
		if (loader != null)
		{
			loader.eyeBlink = true;
		}
	}

	public virtual void ActGuardWalk(Vector3 velocity_, float sync_speed, Vector3 move_vec)
	{
		if (base.actionID == (ACTION_ID)19)
		{
			notEndGuardFlag = true;
		}
		ActMoveVelocity(velocity_, sync_speed, (MOTION_ID)122);
		notEndGuardFlag = false;
		SetGuardWalkRotation(move_vec);
		isActSpecialAction = true;
		isGuardWalk = true;
	}

	public void SetGuardWalkRotation(Vector3 move_vec)
	{
		Vector3 vector;
		if (base.actionTarget != null && !IsValidBuffBlind())
		{
			vector = base.actionTarget._position - _position;
			vector.y = 0f;
			vector.Normalize();
			SetLerpRotation(vector);
		}
		else
		{
			vector = move_vec;
			SetLerpRotation(move_vec);
		}
		float num = Vector3.Angle(vector, move_vec);
		float num2 = 0f;
		num2 = ((!(Vector3.Cross(vector, move_vec).y >= 0f)) ? (num / 360f) : ((360f - num) / 360f));
		base.animator.SetFloat(guardAngleID, num2);
	}

	public override void ActMoveSyncVelocity(float time, Vector3 pos, int motion_id)
	{
		bool flag = false;
		if (base.actionID != ACTION_ID.MOVE)
		{
			flag = true;
		}
		base.ActMoveSyncVelocity(time, pos, motion_id);
		if (motion_id == 122)
		{
			isActSpecialAction = true;
			isGuardWalk = true;
			moveSyncDirection = float.MinValue;
			SetGuardWalkRotation(GetVelocity());
		}
		if (flag)
		{
			PlayerLoader.SetLayerWithChildren_SecondaryNoChange(base._transform, 20);
		}
	}

	public override void SetMoveSyncVelocityEnd(float time, Vector3 pos, float direction, float sync_speed, int motion_id)
	{
		base.SetMoveSyncVelocityEnd(time, pos, direction, sync_speed, motion_id);
		if (motion_id == 122)
		{
			moveSyncDirection = float.MinValue;
			SetGuardWalkRotation(GetVelocity());
		}
	}

	public void ActAttackFailure(int id, bool isSendPacket)
	{
		base.ActAttack(id, isSendPacket);
	}

	public override void ActAttack(int id, bool send_packet = true, bool sync_immediately = false, string _motionLayerName = "", string _motionStateName = "")
	{
		base.ActAttack(id, send_packet, sync_immediately, _motionLayerName, _motionStateName);
		if (isArrowAimBossMode)
		{
			SetArrowAimBossVisible(enable: true);
		}
		if (isArrowAimLesserMode)
		{
			SetArrowAimLesserVisible(enable: true);
		}
		UpdateArrowAngle();
		if ((IsCoopNone() || IsOriginal()) && IsValidBuff(BuffParam.BUFFTYPE.BLEEDING))
		{
			int[] array = null;
			switch (attackMode)
			{
			case ATTACK_MODE.ONE_HAND_SWORD:
				array = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.bleedingParam.ignoreOneHandSwordAtkIds;
				break;
			case ATTACK_MODE.TWO_HAND_SWORD:
				array = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.bleedingParam.ignoreTwoHandSwordAtkIds;
				break;
			case ATTACK_MODE.SPEAR:
				array = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.bleedingParam.ignoreSpearAtkIds;
				break;
			case ATTACK_MODE.PAIR_SWORDS:
				array = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.bleedingParam.ignorePairSwordAtkIds;
				break;
			case ATTACK_MODE.ARROW:
				array = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.bleedingParam.ignoreArrowAtkIds;
				break;
			}
			if (array == null || !array.Contains(id))
			{
				buffParam.OnBleeding();
			}
		}
		for (int i = 0; i < m_weaponCtrlList.Count; i++)
		{
			m_weaponCtrlList[i].OnActAttack(id);
		}
	}

	public override void SetAttackActionPosition()
	{
		if (targetingPoint != null)
		{
			SetActionPosition(targetingPoint.param.targetPos, flag: true);
			if (targetingPoint.owner == null)
			{
				base.actionPositionThroughFlag = true;
			}
		}
		else if (StageObjectManager.CanTargetBoss)
		{
			SetActionPosition(GetTargetPosition(MonoBehaviourSingleton<StageObjectManager>.I.boss), flag: true);
		}
		else
		{
			SetActionPosition(Vector3.zero, flag: false);
		}
	}

	public bool InputAttackCombo()
	{
		if (enableInputCombo)
		{
			inputComboFlag = true;
			if (enableComboTrans)
			{
				ActAttackCombo();
			}
			return true;
		}
		return false;
	}

	public void CancelAttackCombo()
	{
		inputComboFlag = false;
	}

	public void ActAttackCombo()
	{
		int _attackId = inputComboID;
		string _motionLayerName = GetMotionLayerName(attackMode, spAttackType, _attackId);
		if ((thsCtrl == null || thsCtrl.GetActAttackComboParam(ref _attackId, ref _motionLayerName)) && (!CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.BURST) || _attackId != MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.burstOHSInfo.CounterAttackId || isSuccessParry))
		{
			if (ohsCtrl != null && ohsCtrl.CheckActAttackCombo(_attackId))
			{
				FinishBoostMode();
			}
			ActAttack(_attackId, send_packet: false, sync_immediately: false, _motionLayerName, inputComboMotionState);
			if (playerSender != null)
			{
				playerSender.OnActAttackCombo(_attackId, _motionLayerName, inputComboMotionState);
			}
		}
	}

	public float GetChargingRate()
	{
		float num = 0f;
		if (enableInputCharge)
		{
			num = ((!(inputChargeTimeMax - inputChargeTimeOffset <= 0f)) ? ((inputChargeTimeCounter - inputChargeTimeOffset) / (inputChargeTimeMax - inputChargeTimeOffset)) : ((!isInputChargeExistOffset) ? 1f : ((!(inputChargeTimeCounter >= inputChargeTimeMax)) ? 0f : 1f)));
			if (num < 0f)
			{
				num = 0f;
			}
			else if (num > 1f)
			{
				num = 1f;
			}
		}
		else
		{
			num = chargeRate;
		}
		return num;
	}

	public float GetChargeExpandingRate()
	{
		if (timeChargeExpandMax <= 0f)
		{
			return 0f;
		}
		return Mathf.Clamp01((timerChargeExpand + timerChargeExpandOffset) / timeChargeExpandMax);
	}

	private void CheckInputCharge()
	{
		if ((IsCoopNone() || IsOriginal()) && !enableTap && enableInputCharge)
		{
			SetChargeRelease(GetChargingRate());
		}
	}

	private void CheckChargeExpand()
	{
		if ((IsCoopNone() || IsOriginal()) && !enableTap && isChargeExpanding)
		{
			SetChargeExpandRelease(GetChargeExpandingRate());
		}
	}

	private void CheckCannonCharge()
	{
		if ((IsCoopNone() || IsOriginal()) && !enableTap && cannonState == CANNON_STATE.CHARGE)
		{
			SetCannonState(CANNON_STATE.READY);
			if (IsCannonFullCharged())
			{
				ActCannonShot();
			}
		}
	}

	public void CheckSnatchMove()
	{
		if ((IsCoopNone() || IsOriginal()) && snatchCtrl.IsMove())
		{
			Vector3 snatchPos = snatchCtrl.GetSnatchPos();
			if (IsArrivalPosition(snatchPos))
			{
				OnSnatchMoveEnd(2);
			}
			else
			{
				OnSnatchMoveStart(snatchPos);
			}
		}
	}

	public void OnSnatchMoveStart(Vector3 snatchPos)
	{
		EventMoveEnd();
		EndRotate();
		enableEventMove = true;
		base.enableAddForce = false;
		eventMoveVelocity = Vector3.forward * playerParameter.ohsActionInfo.Soul_SnatchMoveVelocity;
		Vector3 normalized = (snatchPos - _position).normalized;
		SetVelocity(Quaternion.LookRotation(normalized) * eventMoveVelocity, VELOCITY_TYPE.EVENT_MOVE);
		snatchCtrl.StartMoveLoop();
		if (snatchCtrl.IsShotReleased())
		{
			SetNextTrigger(3);
		}
		else
		{
			SetNextTrigger();
		}
		StartWaitingPacket(WAITING_PACKET.PLAYER_ONE_HAND_SWORD_MOVE_END, keep_sync: true);
		if (playerSender != null)
		{
			playerSender.OnSnatchMoveStart(snatchPos);
		}
	}

	public void OnSnatchMoveEnd(int triggerIndex = 0)
	{
		DeactiveSnatchMove();
		EndRotate();
		SetNextTrigger(triggerIndex);
		EndWaitingPacket(WAITING_PACKET.PLAYER_ONE_HAND_SWORD_MOVE_END);
		if (playerSender != null)
		{
			playerSender.OnSnatchMoveEnd(triggerIndex);
		}
	}

	public virtual void StartCannonCharge()
	{
		FieldGimmickCannonSpecial fieldGimmickCannonSpecial = targetFieldGimmickCannon as FieldGimmickCannonSpecial;
		if (fieldGimmickCannonSpecial != null)
		{
			if (!fieldGimmickCannonSpecial.IsAbleToShot())
			{
				return;
			}
			fieldGimmickCannonSpecial.StartCharge();
		}
		SetCannonState(CANNON_STATE.CHARGE);
	}

	public virtual void UpdateCannonCharge()
	{
		FieldGimmickCannonSpecial fieldGimmickCannonSpecial = targetFieldGimmickCannon as FieldGimmickCannonSpecial;
		if (fieldGimmickCannonSpecial == null)
		{
			return;
		}
		if (cannonState == CANNON_STATE.CHARGE)
		{
			inputCannonChargeCounter += Time.deltaTime;
			if (inputCannonChargeMax > 0f && !(inputCannonChargeCounter >= inputCannonChargeMax))
			{
			}
		}
		else
		{
			inputCannonChargeCounter -= Time.deltaTime;
			fieldGimmickCannonSpecial.ReleaseCharge();
		}
		inputCannonChargeCounter = Mathf.Clamp(inputCannonChargeCounter, 0f, inputCannonChargeMax);
	}

	public virtual void SetChargeRelease(float charge_rate)
	{
		if (CheckAttackMode(ATTACK_MODE.TWO_HAND_SWORD) && charge_rate >= 1f)
		{
			IncrementCleaveComboCount();
		}
		if (CheckAttackModeAndSpType(ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.NONE) && charge_rate < 1f)
		{
			inputChargeAutoRelease = true;
			inputChargeMaxTiming = false;
			return;
		}
		chargeRate = charge_rate;
		base.enableMotionCancel = false;
		enableCancelToAvoid = false;
		enableCancelToMove = false;
		enableCancelToAttack = false;
		enableCancelToSkill = false;
		enableCancelToSpecialAction = false;
		enableCancelToEvolveSpecialAction = false;
		enableCancelToCarryPut = false;
		if (CheckAttackMode(ATTACK_MODE.SPEAR))
		{
			switch (spAttackType)
			{
			case SP_ATTACK_TYPE.NONE:
				if (isCanRushRelease)
				{
					actSpecialActionTimer = 0f;
					isActSpecialAction = true;
					if (isChargeExRush)
					{
						exRushChargeRate = chargeRate;
						evolveCtrl.PlayLeviathanEffect();
					}
					else
					{
						exRushChargeRate = 0f;
					}
					SetNextTrigger();
				}
				else
				{
					ActIdle();
				}
				ReleaseEffect(ref exRushChargeEffect);
				break;
			case SP_ATTACK_TYPE.HEAT:
				if (isSpearJumpAim)
				{
					if (chargeRate >= 1f)
					{
						_JumpRize();
					}
					else
					{
						ActIdle();
					}
				}
				break;
			case SP_ATTACK_TYPE.SOUL:
				spearCtrl.StoreChargeRate(chargeRate);
				SetNextTrigger();
				break;
			case SP_ATTACK_TYPE.ORACLE:
				if (!spearCtrl.IsGuard() && (spearCtrl.OracleSpCharged || IsValidBuff(BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS)))
				{
					SetNextTrigger(1);
					isActSpecialAction = true;
				}
				else if (!spearCtrl.IsGuard())
				{
					SetNextTrigger();
					isActSpecialAction = true;
				}
				else
				{
					SetNextTrigger();
				}
				break;
			}
		}
		else if (CheckAttackModeAndSpType(ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
		{
			if (thsCtrl != null)
			{
				thsCtrl.SetSoulChargeRelease();
			}
		}
		else if (CheckAttackModeAndSpType(ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.ORACLE))
		{
			int attackID = base.attackID;
			if ((uint)(attackID - 62) <= 2u)
			{
				SetNextTrigger((chargeRate >= 1f) ? 1 : 0);
			}
			else
			{
				SetNextTrigger();
			}
		}
		else if (CheckAttackModeAndSpType(ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.NONE))
		{
			if (chargeRate >= 1f)
			{
				SetNextTrigger();
			}
		}
		else if (CheckAttackModeAndSpType(ATTACK_MODE.ARROW, SP_ATTACK_TYPE.SOUL))
		{
			SetNextTrigger((MonoBehaviourSingleton<TargetMarkerManager>.I.GetMultiLockNum() <= 0) ? 1 : 0);
		}
		else if (CheckAttackModeAndSpType(ATTACK_MODE.ARROW, SP_ATTACK_TYPE.BURST))
		{
			if (isArrowRainShot)
			{
				RainShotChargeRelease();
			}
			SetNextTrigger();
		}
		else if (CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.ORACLE))
		{
			if (isBoostMode)
			{
				SetNextTrigger(1);
			}
			else
			{
				SetNextTrigger();
			}
		}
		else
		{
			SetNextTrigger();
		}
		enableInputCharge = false;
		inputChargeAutoRelease = false;
		inputChargeMaxTiming = false;
		inputChargeTimeMax = 0f;
		inputChargeTimeOffset = 0f;
		inputChargeTimeCounter = 0f;
		isInputChargeExistOffset = false;
		isSpearJumpAim = false;
		if (IsCoopNone() || IsOriginal())
		{
			SetAttackActionPosition();
		}
		EndWaitingPacket(WAITING_PACKET.PLAYER_CHARGE_RELEASE);
		if (isArrowAimLesserMode)
		{
			UpdateArrowAimLesserMode(Vector2.zero);
		}
		UpdateArrowAngle();
		if (playerSender != null)
		{
			playerSender.OnSetChargeRelease(charge_rate, isChargeExRush);
		}
	}

	public virtual void SetChargeExpandRelease(float chargeExpandRate)
	{
		this.chargeExpandRate = chargeExpandRate;
		chargeRate = 1f;
		base.enableMotionCancel = false;
		enableCancelToAvoid = false;
		enableCancelToMove = false;
		enableCancelToAttack = false;
		enableCancelToSkill = false;
		enableCancelToSpecialAction = false;
		enableCancelToEvolveSpecialAction = false;
		enableCancelToCarryPut = false;
		SetNextTrigger();
		enableInputCharge = false;
		inputChargeAutoRelease = false;
		inputChargeMaxTiming = false;
		inputChargeTimeMax = 0f;
		inputChargeTimeOffset = 0f;
		inputChargeTimeCounter = 0f;
		isInputChargeExistOffset = false;
		isChargeExpanding = false;
		isChargeExpandAutoRelease = false;
		if (IsCoopNone() || IsOriginal())
		{
			SetAttackActionPosition();
		}
		EndWaitingPacket(WAITING_PACKET.PLAYER_CHARGE_RELEASE);
		if (playerSender != null)
		{
			playerSender.OnSetChargeExpandRelease(this.chargeExpandRate);
		}
	}

	public void ExecChargeMaxOnce()
	{
		inputChargeMaxTiming = false;
		if (isNpc || (!IsCoopNone() && !IsOriginal()))
		{
			return;
		}
		switch (attackMode)
		{
		case ATTACK_MODE.TWO_HAND_SWORD:
			if (spAttackType == SP_ATTACK_TYPE.SOUL)
			{
				if ((object)twoHandSwordsChargeMaxEffect == null)
				{
					twoHandSwordsChargeMaxEffect = EffectManager.GetEffect(MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.soulIaiChargeMaxEffect, FindNode("R_Wep"));
				}
				SoundManager.PlayOneShotSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.soulIaiChargeMaxSeId, this, FindNode(""));
			}
			else if (spAttackType == SP_ATTACK_TYPE.ORACLE)
			{
				thsCtrl.oracleCtrl.StartVernierEffect(isMax: true);
			}
			break;
		case ATTACK_MODE.SPEAR:
			if (spAttackType == SP_ATTACK_TYPE.NONE)
			{
				if (isChargeExRush)
				{
					ReleaseEffect(ref exRushChargeEffect);
					EffectManager.OneShot("ef_btl_wsk_charge_end_01", FindNode("R_Wep").transform.position, Quaternion.identity);
					exRushChargeEffect = EffectManager.GetEffect("ef_btl_wsk_charge_loop_02", FindNode("R_Wep"));
					SoundManager.PlayOneShotSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.exRushChargeMaxSeId, this, FindNode(""));
				}
				else
				{
					_StartExRushCharge();
				}
			}
			else if (spAttackType == SP_ATTACK_TYPE.HEAT)
			{
				if (EnablePlaySound())
				{
					SoundManager.PlayOneShotSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.jumpChargeMaxSeId, this, FindNode(""));
				}
			}
			else if (spAttackType == SP_ATTACK_TYPE.SOUL)
			{
				Transform transform = FindNode("R_Wep");
				EffectManager.OneShot("ef_btl_wsk_charge_end_01", transform.position + transform.rotation * MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.Soul_SpAttackMaxChargeEffectOffsetPos, Quaternion.identity);
				SoundManager.PlayOneShotSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.exRushChargeMaxSeId, this, FindNode(""));
				spearCtrl.ExecBladeEffect();
			}
			break;
		case ATTACK_MODE.PAIR_SWORDS:
			if (spAttackType == SP_ATTACK_TYPE.NONE)
			{
				EffectManager.OneShot("ef_btl_wsk_charge_end_01", FindNode("R_Wep").transform.position, Quaternion.identity);
				EffectManager.OneShot("ef_btl_wsk_charge_end_01", FindNode("L_Wep").transform.position, Quaternion.identity);
				SoundManager.PlayOneShotSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.wildDanceChargeMaxSeId, this, FindNode(""));
			}
			break;
		}
	}

	public void SetEnableTap(bool enable)
	{
		enableTap = enable;
		if (isChargeExpanding)
		{
			CheckChargeExpand();
		}
		else
		{
			CheckInputCharge();
		}
		if (!enable)
		{
			if (cannonState == CANNON_STATE.CHARGE)
			{
				CheckCannonCharge();
			}
			snatchCtrl.OnRelease();
			int i = 0;
			for (int count = m_weaponCtrlList.Count; i < count; i++)
			{
				m_weaponCtrlList[i].OnRelease();
			}
			countLongTouchSec = 0f;
		}
	}

	private void IncrementCleaveComboCount()
	{
		if ((IsCoopNone() || IsOriginal()) && buffParam.IncrementCleaveComboConditionAbility())
		{
			EffectManager.GetEffect("ef_btl_ab_charge_01", base._transform);
		}
		buffParam.ResetStack();
	}

	public virtual void SetInputAxis(Vector2 input_vec)
	{
		if (enableInputRotate)
		{
			if (!startInputRotate)
			{
				startInputRotate = true;
				rotateEventSpeed = 0f;
				rotateEventDirection = 0f;
				rotateEventKeep = false;
			}
			Vector3 right = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.right;
			Vector3 forward = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.forward;
			right.y = 0f;
			right.Normalize();
			forward.y = 0f;
			forward.Normalize();
			Vector3 forward2 = right * input_vec.x + forward * input_vec.y;
			_rotation = Quaternion.LookRotation(forward2);
		}
	}

	public void ActAvoid()
	{
		EndAction();
		base.actionID = ACTION_ID.MAX;
		if (CheckAttackModeAndSpType(ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL))
		{
			PlayMotion(136);
		}
		else
		{
			PlayMotion(115);
		}
		actionMoveRate = buffParam.GetAvoidUp();
		actionMoveRate += GetAttackModeAvoidUp();
		buffParam.OnAvoid();
		snatchCtrl.Cancel();
		int i = 0;
		for (int count = m_weaponCtrlList.Count; i < count; i++)
		{
			m_weaponCtrlList[i].OnActAvoid();
		}
		if (playerSender != null)
		{
			playerSender.OnActAvoid();
		}
		if (FieldManager.IsValidInTutorial())
		{
			InGameTutorialManager component = MonoBehaviourSingleton<AppMain>.I.GetComponent<InGameTutorialManager>();
			if (component != null)
			{
				component.GetState<InGameTutorialManager.TutorialRolling>()?.AddRollingCount();
			}
		}
	}

	public void ActWarp()
	{
		EndAction();
		base.actionID = (ACTION_ID)36;
		if (CheckAttackModeAndSpType(ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL))
		{
			PlayMotion(137);
		}
		else
		{
			PlayMotion(135);
		}
		actionMoveRate = buffParam.GetAvoidUp();
		actionMoveRate += GetAttackModeAvoidUp();
		buffParam.OnAvoid();
		snatchCtrl.Cancel();
		int i = 0;
		for (int count = m_weaponCtrlList.Count; i < count; i++)
		{
			m_weaponCtrlList[i].OnActAvoid();
		}
		if (playerSender != null)
		{
			playerSender.OnWarp();
		}
	}

	public void ActTeleportAvoid()
	{
		EndAction();
		base.actionID = (ACTION_ID)46;
		PlayMotion(146);
		actionMoveRate = buffParam.GetTeleportUp();
		buffParam.OnAvoid();
		snatchCtrl.Cancel();
		int i = 0;
		for (int count = m_weaponCtrlList.Count; i < count; i++)
		{
			m_weaponCtrlList[i].OnActAvoid();
		}
		if (playerSender != null)
		{
			playerSender.OnTeleportAvoid();
		}
	}

	public void ActRushAvoid(Vector3 inputVec)
	{
		EndAction();
		Vector3 vector;
		if (base.actionTarget != null && !IsValidBuffBlind())
		{
			vector = base.actionTarget._position - _position;
			vector.y = 0f;
			vector.Normalize();
			SetLerpRotation(vector);
		}
		else
		{
			vector = inputVec;
			SetLerpRotation(inputVec);
		}
		float num = Vector3.Angle(vector, inputVec);
		float num2 = 0f;
		num2 = ((!(Vector3.Cross(vector, inputVec).y >= 0f)) ? (num / 360f) : ((360f - num) / 360f));
		base.animator.SetFloat(rushAvoidAngleID, num2);
		base.actionID = (ACTION_ID)49;
		PlayMotion(147);
		buffParam.OnAvoid();
		snatchCtrl.Cancel();
		int i = 0;
		for (int count = m_weaponCtrlList.Count; i < count; i++)
		{
			m_weaponCtrlList[i].OnActAvoid();
		}
		if (playerSender != null)
		{
			playerSender.OnRushAvoid(inputVec);
		}
	}

	public virtual void ActRestraint(RestraintInfo restInfo)
	{
		EndAction();
		base.actionID = (ACTION_ID)30;
		PlayMotion(130);
		base._rigidbody.isKinematic = true;
		base._rigidbody.useGravity = false;
		base._rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		base._rigidbody.velocity = Vector3.zero;
		m_restrainTime = Time.time + restInfo.duration;
		m_restraintDamgeTimer = restInfo.damageInterval;
		m_restraintInfo = restInfo;
		AttackRestraintObject attackRestraintObject = new GameObject("AttackRestraintObject").AddComponent<AttackRestraintObject>();
		attackRestraintObject.Initialize(this, restInfo);
		m_attackRestraint = attackRestraintObject;
		if (restInfo.damageRate > 0)
		{
			m_restrainDamageValue = (int)((float)base.hpMax * ((float)restInfo.damageRate * 0.01f));
		}
		ClearLaser();
		pairSwordsCtrl.OnReaction();
		fishingCtrl.OnReaction();
		int i = 0;
		for (int count = m_weaponCtrlList.Count; i < count; i++)
		{
			m_weaponCtrlList[i].OnActReaction();
		}
		if (playerSender != null)
		{
			playerSender.OnRestraintStart(restInfo);
		}
	}

	public void ActRestraintEnd()
	{
		if (base.actionID == (ACTION_ID)30)
		{
			EndAction();
			if (base.actionID != (ACTION_ID)17)
			{
				ActFallBlow(Vector3.down * 10f);
			}
		}
	}

	private void PostRestraint()
	{
		if (!(m_attackRestraint == null))
		{
			base._transform.position = MonoBehaviourSingleton<StageManager>.I.ClampInside(base._transform.position);
			if (m_restraintInfo.isStopMotion)
			{
				setPause(pause: false);
			}
			m_restrainDamageValue = 0;
			m_restrainTime = 0f;
			m_restraintDamgeTimer = 0f;
			m_restraintInfo = null;
			base._rigidbody.isKinematic = false;
			base._rigidbody.useGravity = true;
			base._rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
			base._rigidbody.velocity = Vector3.zero;
			if (m_attackRestraint != null)
			{
				m_attackRestraint.DeleteThis();
			}
			m_attackRestraint = null;
			if (playerSender != null)
			{
				playerSender.OnRestraintEnd();
			}
		}
	}

	private void UpdateRestraint()
	{
		UpdateNextMotion();
		if (m_attackRestraint == null)
		{
			return;
		}
		if (m_restraintInfo.isStopMotion && !base.isPause && base.animator.GetCurrentAnimatorStateInfo(0).fullPathHash == Animator.StringToHash("Base Layer.restraint"))
		{
			setPause(pause: true);
		}
		int restrainDamageValue = m_restrainDamageValue;
		if ((IsCoopNone() || IsOriginal()) && restrainDamageValue > 0 && base.hp > 1)
		{
			m_restraintDamgeTimer -= Time.deltaTime;
			if (m_restraintDamgeTimer <= 0f)
			{
				if (IsValidShield())
				{
					ShieldDamageData shieldDamageData = CalcShieldDamage(restrainDamageValue);
					base.ShieldHp = (int)base.ShieldHp - shieldDamageData.shieldDamage;
					base.hp -= shieldDamageData.hpDamage;
				}
				else
				{
					base.hp -= restrainDamageValue;
				}
				if (base.hp <= 1)
				{
					base.hp = 1;
				}
				if (MonoBehaviourSingleton<UIDamageManager>.IsValid())
				{
					MonoBehaviourSingleton<UIDamageManager>.I.CreatePlayerDamage(this, restrainDamageValue, UIPlayerDamageNum.DAMAGE_COLOR.DAMAGE);
				}
				m_restraintDamgeTimer = m_restraintInfo.damageInterval;
			}
		}
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
			if (boss != null && boss.isDead)
			{
				ActRestraintEnd();
				return;
			}
		}
		if (Time.time - m_restrainTime > 0f)
		{
			ActRestraintEnd();
		}
	}

	public void ReduceRestraintTime()
	{
		RestraintInfo restraintInfo = m_restraintInfo;
		if (restraintInfo == null)
		{
			return;
		}
		float reduceTimeByFlick = restraintInfo.reduceTimeByFlick;
		if (!(reduceTimeByFlick <= 0f))
		{
			m_restrainTime -= reduceTimeByFlick;
			if (m_attackRestraint != null)
			{
				m_attackRestraint.OnFlick();
			}
		}
	}

	public bool IsRestraint()
	{
		return base.actionID == (ACTION_ID)30;
	}

	private void CreateStoneEffect()
	{
		if ((object)m_stoneEffect == null)
		{
			Transform effect = EffectManager.GetEffect("ef_btl_pl_stone_01", base._transform);
			if (!(effect == null))
			{
				m_stoneEffect = effect.gameObject;
			}
		}
	}

	public virtual void ActStone()
	{
		EndAction();
		base.actionID = (ACTION_ID)43;
		PlayMotion(140);
		FinishBoostMode();
		base._rigidbody.isKinematic = true;
		base._rigidbody.useGravity = false;
		base._rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		base._rigidbody.velocity = Vector3.zero;
		CreateStoneEffect();
		OnStoneStart();
		prayerIds.Clear();
		boostPrayTargetInfoList.Clear();
		boostPrayedInfoList.Clear();
		if (MonoBehaviourSingleton<UIDeadAnnounce>.IsValid())
		{
			MonoBehaviourSingleton<UIDeadAnnounce>.I.Announce(UIDeadAnnounce.ANNOUNCE_TYPE.STONE, this);
		}
		Utility.MaterialForEach(base._rendererArray, delegate(Material material)
		{
			if (material.HasProperty(GameDefine.SHADER_PROPERTY_NAME_GRAYSCALE_RATE))
			{
				material.SetFloat(GameDefine.SHADER_PROPERTY_NAME_GRAYSCALE_RATE, debuffParameter.stoneParam.grayScaleRate);
			}
			if (material.HasProperty(GameDefine.SHADER_PROPERTY_NAME_GRAYSCALE_POWER))
			{
				material.SetFloat(GameDefine.SHADER_PROPERTY_NAME_GRAYSCALE_POWER, debuffParameter.stoneParam.grayScalePow);
			}
		});
		if (IsCoopNone() || IsOriginal())
		{
			StoneCount(buffParam.GetStoneTime(), IsPrayed() || isStopCounter);
		}
		UpdateRevivalRangeEffect();
		OnActReaction();
		ClearLaser();
		pairSwordsCtrl.OnReaction();
		fishingCtrl.OnReaction();
	}

	public void ActStoneEnd(float countTime)
	{
		EndAction();
		OnBuffEnd(BuffParam.BUFFTYPE.STONE, sync: false);
		Utility.MaterialForEach(base._rendererArray, delegate(Material material)
		{
			if (material.HasProperty(GameDefine.SHADER_PROPERTY_NAME_GRAYSCALE_RATE))
			{
				material.SetFloat(GameDefine.SHADER_PROPERTY_NAME_GRAYSCALE_RATE, 0f);
			}
			if (material.HasProperty(GameDefine.SHADER_PROPERTY_NAME_GRAYSCALE_POWER))
			{
				material.SetFloat(GameDefine.SHADER_PROPERTY_NAME_GRAYSCALE_POWER, 0f);
			}
		});
		if (countTime > 0f)
		{
			PlayMotion(141);
			if (MonoBehaviourSingleton<UIDeadAnnounce>.IsValid())
			{
				MonoBehaviourSingleton<UIDeadAnnounce>.I.Announce(UIDeadAnnounce.ANNOUNCE_TYPE.RESCUE_STONE, this);
			}
		}
		else if (!base.isDead)
		{
			base.hp = 0;
			healHp = 0;
			ActDead();
		}
		if (playerSender != null)
		{
			playerSender.OnActStoneEnd(countTime);
		}
	}

	private void PostStone()
	{
		if (m_stoneEffect != null)
		{
			EffectManager.ReleaseEffect(m_stoneEffect);
			m_stoneEffect = null;
		}
		base._rigidbody.isKinematic = false;
		base._rigidbody.useGravity = true;
		base._rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
		base._rigidbody.velocity = Vector3.zero;
	}

	private void UpdateStone()
	{
		UpdateNextMotion();
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
			if (boss != null && boss.isDead)
			{
				ActStoneEnd(stoneRescueTime);
			}
		}
	}

	public override bool IsStone()
	{
		return base.actionID == (ACTION_ID)43;
	}

	public override void OnActReaction()
	{
		base.OnActReaction();
		int i = 0;
		for (int count = m_weaponCtrlList.Count; i < count; i++)
		{
			m_weaponCtrlList[i].OnActReaction();
		}
		snatchCtrl.Cancel();
		pairSwordsCtrl.OnReaction();
		fishingCtrl.OnReaction();
		EndCarry();
	}

	public virtual void ActStumble(float time = 0f)
	{
		EndAction();
		base.actionID = (ACTION_ID)14;
		PlayMotion(116);
		stumbleEndTime = Time.time + time;
		OnActReaction();
		ClearLaser();
	}

	public virtual void ActShake()
	{
		EndAction();
		base.actionID = (ACTION_ID)15;
		PlayMotion(117);
		shakeEndTime = Time.time + buffParam.GetShakeTime(playerParameter.shakeLoopTime);
		OnActReaction();
		ClearLaser();
	}

	public virtual void ActBlow(Vector3 force)
	{
		EndAction();
		base.actionID = (ACTION_ID)16;
		PlayMotion(118);
		if (base._rigidbody != null)
		{
			base.addForce = force;
		}
		IgnoreEnemyColliders();
		OnActReaction();
		ClearLaser();
	}

	public virtual void InputBlowClear()
	{
		if ((!IsCoopNone() && !IsOriginal()) || enableInputCombo)
		{
			inputBlowClearFlag = true;
			if (playerSender != null)
			{
				playerSender.OnInputBlowClear();
			}
		}
	}

	public virtual void SetBlowClear(int index)
	{
		enableInputCombo = false;
		inputBlowClearFlag = false;
		SetNextTrigger(index + 1);
	}

	public virtual void ActFallBlow(Vector3 force)
	{
		EndAction();
		base.actionID = (ACTION_ID)17;
		PlayMotion(119);
		if (base._rigidbody != null)
		{
			base.addForce = force;
			base.waitAddForce = true;
		}
		IgnoreEnemyColliders();
		OnActReaction();
		ClearLaser();
	}

	public virtual void ActStunnedBlow(Vector3 force, float time = 0f)
	{
		EndAction();
		base.actionID = (ACTION_ID)18;
		PlayMotion(120);
		if (base._rigidbody != null)
		{
			base.addForce = force;
		}
		stunnedTime = buffParam.GetStumbleTime(time);
		IgnoreEnemyColliders();
		OnActReaction();
		ClearLaser();
	}

	public virtual void ActCharmBlow(Vector3 force, float time)
	{
		EndAction();
		base.actionID = (ACTION_ID)47;
		PlayMotion(120);
		if (base._rigidbody != null)
		{
			base.addForce = force;
		}
		stunnedTime = buffParam.GetCharmTime(time);
		IgnoreEnemyColliders();
		OnActReaction();
		ClearLaser();
	}

	public virtual void ActGrabbedStart(int enemyId, GrabInfo grabInfo)
	{
		Enemy enemy = MonoBehaviourSingleton<StageObjectManager>.I.FindEnemy(enemyId) as Enemy;
		if (!(enemy == null))
		{
			if ((int)enemy.GrabHpMax > 0)
			{
				enemy.GrabHp = enemy.GrabHpMax;
			}
			grabDrainAtkInfo = null;
			if (grabInfo.drainAttackId > 0)
			{
				grabDrainAtkInfo = enemy.SearchDrainAttackInfo(grabInfo.drainAttackId);
			}
			EndAction();
			base.actionID = (ACTION_ID)29;
			PlayMotion(129);
			IgnoreEnemyColliders();
			Transform transform = Utility.Find(enemy._transform, grabInfo.parentNode);
			if (transform != null)
			{
				base._transform.parent = transform;
				base._transform.localPosition = Vector3.zero;
				base._transform.localRotation = Quaternion.identity;
				base._rigidbody.isKinematic = true;
				base._rigidbody.useGravity = false;
				base._rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			}
			CircleShadow componentInChildren = GetComponentInChildren<CircleShadow>();
			if (componentInChildren != null)
			{
				componentInChildren.gameObject.SetActive(value: false);
			}
			hitOffFlag |= HIT_OFF_FLAG.GRAB;
			ClearLaser();
			pairSwordsCtrl.OnReaction();
			fishingCtrl.OnReaction();
			int i = 0;
			for (int count = m_weaponCtrlList.Count; i < count; i++)
			{
				m_weaponCtrlList[i].OnActReaction();
			}
			if (playerSender != null)
			{
				playerSender.OnGrabbedStart(enemy.id, grabInfo.parentNode, grabInfo.duration, grabInfo.drainAttackId);
			}
		}
	}

	public virtual void ActGrabbedEnd(float angle = 0f, float power = 0f)
	{
		EndAction();
		grabDrainAtkInfo = null;
		base._transform.position = MonoBehaviourSingleton<StageManager>.I.ClampInside(base._transform.position);
		CircleShadow componentInChildren = GetComponentInChildren<CircleShadow>(includeInactive: true);
		if (componentInChildren != null)
		{
			componentInChildren.gameObject.SetActive(value: true);
			componentInChildren.transform.localPosition = Vector3.zero;
		}
		base._transform.parent = MonoBehaviourSingleton<StageObjectManager>.I._transform;
		base._transform.localRotation = Quaternion.identity;
		base._rigidbody.isKinematic = false;
		base._rigidbody.useGravity = true;
		base._rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
		base._rigidbody.velocity = Vector3.zero;
		Vector3 point = -_forward;
		point = Quaternion.AngleAxis(angle, _right) * point * power;
		ActFallBlow(point);
		hitOffFlag &= ~HIT_OFF_FLAG.GRAB;
		if (playerSender != null)
		{
			playerSender.OnGrabbedEnd();
		}
	}

	public virtual void ActGrabbedUpdate()
	{
		UpdateNextMotion();
		if (grabDrainAtkInfo == null)
		{
			return;
		}
		int num = (int)((float)base.hpMax * (grabDrainAtkInfo.damageRate * 0.01f));
		if ((!IsCoopNone() && !IsOriginal()) || num <= 0 || base.hp <= 1)
		{
			return;
		}
		grabDrainDamageTimer -= Time.deltaTime;
		if (grabDrainDamageTimer <= 0f)
		{
			if (IsValidShield())
			{
				ShieldDamageData shieldDamageData = CalcShieldDamage(num);
				base.ShieldHp = (int)base.ShieldHp - shieldDamageData.shieldDamage;
				base.hp -= shieldDamageData.hpDamage;
			}
			else
			{
				base.hp -= num;
			}
			if (base.hp <= 1)
			{
				base.hp = 1;
			}
			if (MonoBehaviourSingleton<UIDamageManager>.IsValid())
			{
				MonoBehaviourSingleton<UIDamageManager>.I.CreatePlayerDamage(this, num, UIPlayerDamageNum.DAMAGE_COLOR.DAMAGE);
			}
			grabDrainDamageTimer = grabDrainAtkInfo.damageInterval;
		}
	}

	public virtual void ReduceStunnedTime()
	{
		if (isStunnedLoop && !(stunnedReduceEnableTime <= 0f))
		{
			float stunnedReduceTimeValue = playerParameter.stunnedReduceTimeValue;
			if (stunnedReduceTimeValue > stunnedReduceEnableTime)
			{
				stunnedReduceTimeValue = stunnedReduceEnableTime;
			}
			stunnedEndTime -= stunnedReduceTimeValue;
			stunnedReduceEnableTime -= stunnedReduceTimeValue;
		}
	}

	public virtual void SetStunnedEnd()
	{
		if (isStunnedLoop)
		{
			SetNextTrigger(1);
			isStunnedLoop = false;
			stunnedEndTime = 0f;
			UpdateStunnedEffect();
			if (playerSender != null)
			{
				playerSender.OnSetStunnedEnd();
			}
		}
	}

	protected void UpdateStunnedEffect()
	{
		bool flag = false;
		float num = 0f;
		if (isStunnedLoop && stunnedTime > 0f)
		{
			float num2 = stunnedEndTime - Time.time;
			num = 1f - num2 / stunnedTime;
			if (num < 0f)
			{
				num = 0f;
			}
			if (num > 1f)
			{
				num = 1f;
			}
			if (num < 1f)
			{
				flag = true;
			}
		}
		string[] array = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.stunnedEffectList;
		if (base.actionID == (ACTION_ID)47)
		{
			array = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.charmEffectList;
		}
		if (flag && array.Length != 0)
		{
			int num3 = 0;
			num3 = (int)(num * (float)array.Length);
			if (num3 == stunnedEffectIndex)
			{
				return;
			}
			stunnedEffectIndex = -1;
			if (stunnedEffect != null)
			{
				EffectManager.ReleaseEffect(stunnedEffect);
				stunnedEffect = null;
			}
			if (!string.IsNullOrEmpty(array[num3]))
			{
				stunnedEffectIndex = num3;
				Transform effect = EffectManager.GetEffect(array[num3], base.rootNode);
				if (effect != null)
				{
					stunnedEffect = effect.gameObject;
				}
			}
		}
		else
		{
			stunnedEffectIndex = -1;
			if (stunnedEffect != null)
			{
				EffectManager.ReleaseEffect(stunnedEffect);
				stunnedEffect = null;
			}
		}
	}

	public virtual void ActGuard()
	{
		if (base.actionID == (ACTION_ID)20 || base.actionID == (ACTION_ID)34 || base.actionID == (ACTION_ID)21 || isGuardWalk)
		{
			notEndGuardFlag = true;
		}
		EndAction();
		base.actionID = (ACTION_ID)19;
		PlayMotion(121);
		if (!notEndGuardFlag)
		{
			_StartGuard();
		}
		notEndGuardFlag = false;
		base.isControllable = true;
		if (IsCoopNone() || IsOriginal())
		{
			if (base.actionTarget != null)
			{
				SetActionPosition(base.actionTarget._position, flag: true);
			}
			else
			{
				SetActionPosition(Vector3.zero, flag: false);
			}
		}
	}

	public virtual void ActGuardDamage()
	{
		notEndGuardFlag = true;
		EndAction();
		notEndGuardFlag = false;
		if (_CheckJustGuardSec())
		{
			isJustGuard = true;
			if (spAttackType == SP_ATTACK_TYPE.BURST)
			{
				int baseAtkId = playerParameter.ohsActionInfo.burstOHSInfo.BaseAtkId;
				string motionLayerName = GetMotionLayerName(attackMode, spAttackType, baseAtkId);
				PlayMotionParam param = new PlayMotionParam
				{
					MotionID = 134,
					MotionLayerName = motionLayerName,
					TransitionTime = -1f
				};
				base.actionID = (ACTION_ID)21;
				PlayMotion(param);
			}
			else
			{
				base.actionID = (ACTION_ID)34;
				PlayMotion(133);
			}
		}
		else
		{
			base.actionID = (ACTION_ID)20;
			PlayMotion(123);
		}
		OnActReaction();
	}

	public virtual bool ActBattleStart(bool effect_only = false)
	{
		if (isActedBattleStart)
		{
			return false;
		}
		if (base.packetReceiver != null)
		{
			base.packetReceiver.SetStopPacketUpdate(is_stop: false);
		}
		isActedBattleStart = true;
		isWaitBattleStart = false;
		if (effect_only)
		{
			if (FieldManager.IsValidInGameNoQuest())
			{
				return true;
			}
			string battleStartEffectName = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.battleStartEffectName;
			if (!string.IsNullOrEmpty(battleStartEffectName))
			{
				Transform effect = EffectManager.GetEffect(battleStartEffectName, base._transform);
				if (effect != null)
				{
					AddObjectList(effect.gameObject, OBJECT_LIST_TYPE.STATIC);
				}
			}
		}
		else
		{
			EndAction();
			base.actionID = (ACTION_ID)23;
			PlayMotion(124);
			if (uiPlayerStatusGizmo != null)
			{
				uiPlayerStatusGizmo.SetVisible(visible: false);
			}
			hitOffFlag |= HIT_OFF_FLAG.INVICIBLE;
			SetEnableNodeRenderer(null, enable: false);
			buffParam.substituteCtrl.ActiveEffectRoot();
		}
		return true;
	}

	public virtual void WaitBattleStart()
	{
		isWaitBattleStart = true;
		if (base.packetReceiver != null)
		{
			base.packetReceiver.SetStopPacketUpdate(is_stop: true);
		}
	}

	public override void ActDead(bool force_sync = false, bool recieve_direct = false)
	{
		ActGrabbedEnd();
		ActRestraintEnd();
		FinishBoostMode();
		EndEvolve();
		_EndGuard();
		_EndBuffShadowSealing();
		EndCarry();
		base.badStatusTotal.Reset();
		CheckAutoRevive();
		base.ActDead(force_sync, recieve_direct);
		if (MonoBehaviourSingleton<UIDeadAnnounce>.IsValid())
		{
			MonoBehaviourSingleton<UIDeadAnnounce>.I.Announce(UIDeadAnnounce.ANNOUNCE_TYPE.DEAD, this);
		}
		skillInfo.ResetUseGauge();
		ResetSpActionGauge();
		evolveCtrl.ResetGauge(isAll: true);
		snatchCtrl.Cancel();
		int i = 0;
		for (int count = m_weaponCtrlList.Count; i < count; i++)
		{
			m_weaponCtrlList[i].OnActDead();
		}
		ClearLaser();
		RemoveAllBulletTurret();
	}

	public virtual void ActDeadLoop(bool is_init_rescue = false, float set_rescue_time = 0f, float set_continue_time = 0f)
	{
		EndAction();
		base.actionID = (ACTION_ID)24;
		PlayMotion(125);
		base.isDead = true;
		prayerIds.Clear();
		boostPrayTargetInfoList.Clear();
		boostPrayedInfoList.Clear();
		hitOffFlag |= HIT_OFF_FLAG.DEAD;
		IgnoreEnemyColliders();
		if (OnAutoRevive())
		{
			return;
		}
		bool num = QuestManager.IsValidInGameExplore();
		float rescue_time = 0f;
		float continueTime = 0f;
		if (num)
		{
			rescue_time = playerParameter.exploreRescureTime;
			continueTime = playerParameter.continueTime;
		}
		else if (is_init_rescue)
		{
			rescue_time = set_rescue_time;
			continueTime = set_continue_time;
		}
		else if (playerParameter.rescueTimes.Length != 0)
		{
			int num2 = rescueCount;
			if (playerParameter.rescueTimes.Length <= num2)
			{
				num2 = playerParameter.rescueTimes.Length - 1;
			}
			rescue_time = playerParameter.rescueTimes[num2];
			continueTime = playerParameter.continueTime;
		}
		if (IsCoopNone() || IsOriginal())
		{
			DeadCount(rescue_time, isStopCounter);
		}
		this.continueTime = continueTime;
		UpdateRevivalRangeEffect();
	}

	private void DeactivateRescueTimer()
	{
		StopCounter(stop: true);
	}

	private void ActivateRescueTimer()
	{
		StopCounter(stop: false);
	}

	public void StopCounter(bool stop)
	{
		isStopCounter = stop;
		bool flag = false;
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			flag = MonoBehaviourSingleton<InGameProgress>.I.disableSendProgressStop;
		}
		if (!flag && playerSender != null)
		{
			playerSender.OnStopCounter(stop);
		}
		if (IsOriginal() || IsCoopNone())
		{
			DeadCount(rescueTime, stop || IsPrayed() || isWaitingResurrectionHoming);
		}
	}

	public void DeadCount(float rescue_time, bool stop, bool syncRequested = false)
	{
		if (!MonoBehaviourSingleton<CoopManager>.IsValid() || !MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest())
		{
			_rescueTime = rescue_time;
			if (!stop)
			{
				deadStartTime = Time.time;
				deadStopTime = -1f;
			}
			else
			{
				deadStartTime = Time.time;
				deadStopTime = Time.time;
			}
			UpdateRevivalRangeEffect();
			if (playerSender != null && !syncRequested)
			{
				playerSender.OnDeadCount(_rescueTime, stop);
			}
		}
	}

	public void StoneCount(float rescue_time, bool stop, bool syncRequested = false)
	{
		if (!MonoBehaviourSingleton<CoopManager>.IsValid() || !MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest())
		{
			_stoneRescueTime = rescue_time;
			if (!stop)
			{
				stoneStartTime = Time.time;
				stoneStopTime = -1f;
			}
			else
			{
				stoneStartTime = Time.time;
				stoneStopTime = Time.time;
			}
			UpdateRevivalRangeEffect();
			if (playerSender != null && !syncRequested)
			{
				playerSender.OnStoneCount(_stoneRescueTime, stop);
			}
		}
	}

	public bool IsRescuable()
	{
		if (rescueTime > 0f && !isStopCounter)
		{
			return deadStartTime >= 0f;
		}
		return false;
	}

	public void UpdateRevivalRangeEffect()
	{
		if ((base.actionID != (ACTION_ID)24 && base.actionID != (ACTION_ID)43) || (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo()) || QuestManager.IsValidInGameTrial() || (MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest()) || QuestManager.IsValidInGameSeriesArena())
		{
			return;
		}
		if (IsRescuable() || IsStone())
		{
			if (revivalRangEffect == null)
			{
				Transform effect = EffectManager.GetEffect("ef_btl_rebirth_area_01", base._transform);
				if (effect != null)
				{
					Vector3 localScale = effect.localScale;
					effect.localScale = localScale * playerParameter.revivalRange;
					revivalRangEffect = effect.gameObject;
				}
			}
		}
		else if (revivalRangEffect != null)
		{
			EffectManager.ReleaseEffect(revivalRangEffect);
			revivalRangEffect = null;
		}
	}

	public void SyncDeadCount()
	{
		if (base.isDead && !MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest() && !IsCoopNone() && IsOriginal())
		{
			DeadCount(rescueTime, IsPrayed() || isStopCounter || isWaitingResurrectionHoming);
		}
	}

	public void SyncStoneCount()
	{
		if (IsStone() && !MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest() && !IsCoopNone() && IsOriginal())
		{
			StoneCount(stoneRescueTime, IsPrayed() || isStopCounter);
		}
	}

	public virtual void OnEndContinueTimeEnd()
	{
		if ((MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameWaveMatch(isOnlyEvent: true)) && MonoBehaviourSingleton<StageObjectManager>.I.playerList.TrueForAll(delegate(StageObject so)
		{
			Player player = so as Player;
			return !(player != null) || (player.isDead && !player.IsAutoReviving());
		}))
		{
			MonoBehaviourSingleton<InGameProgress>.I.BattleRetire();
		}
	}

	public virtual void ActDeadStandup(int standup_hp, eContinueType cType)
	{
		EndAction();
		base.actionID = (ACTION_ID)25;
		PlayMotion(126);
		hitOffFlag |= HIT_OFF_FLAG.INVICIBLE;
		base.isDead = false;
		base.hp = standup_hp;
		healHp = standup_hp;
		autoReviveHp = 0;
		isWaitingResurrectionHoming = false;
		PlayVoice(93);
		switch (cType)
		{
		case eContinueType.CONTINUE:
			skillInfo.SetUseGaugeFull();
			MonoBehaviourSingleton<UIDeadAnnounce>.I.Announce(UIDeadAnnounce.ANNOUNCE_TYPE.CONTINUE, this);
			break;
		case eContinueType.RESCUE:
			if (EnableRescueCountup())
			{
				rescueCount++;
			}
			MonoBehaviourSingleton<UIDeadAnnounce>.I.Announce(UIDeadAnnounce.ANNOUNCE_TYPE.RESCURE, this);
			break;
		case eContinueType.AUTO_REVIVE:
			autoReviveCount++;
			MonoBehaviourSingleton<UIDeadAnnounce>.I.Announce(UIDeadAnnounce.ANNOUNCE_TYPE.AUTO_REVIVE, this);
			break;
		case eContinueType.REACH_NEXT_WAVE:
			if (EnableRescueCountup())
			{
				rescueCount++;
			}
			MonoBehaviourSingleton<UIDeadAnnounce>.I.Announce(UIDeadAnnounce.ANNOUNCE_TYPE.REACH_NEXT_WAVE, this);
			break;
		}
		if (standup_hp >= base.hpMax)
		{
			Transform effect = EffectManager.GetEffect("ef_btl_rebirth_01");
			if (effect != null)
			{
				effect.position = _position;
				effect.rotation = _rotation;
				effect.localScale = Vector3.Scale(base._transform.lossyScale, effect.localScale);
			}
		}
		if (buffInfoListOnActDeadStandUp.Any())
		{
			foreach (KeyValuePair<int, SkillInfo.SkillParam> item in buffInfoListOnActDeadStandUp)
			{
				StartBuffByBuffTableId(item.Key, item.Value);
			}
			buffInfoListOnActDeadStandUp.Clear();
		}
		if (playerSender != null)
		{
			playerSender.OnActDeadStandup(standup_hp, cType);
		}
	}

	public void RestartExplore()
	{
		base.isDead = false;
		base.hp = base.hpMax;
		if (EnableRescueCountup())
		{
			rescueCount++;
		}
	}

	public override void ActParalyze()
	{
		base.ActParalyze();
		ClearLaser();
		pairSwordsCtrl.OnReaction();
		fishingCtrl.OnReaction();
		paralyzeTime = Time.time + buffParam.GetParalyzeTime();
	}

	public void ActPrayer()
	{
		EndAction();
		base.actionID = (ACTION_ID)26;
		PlayMotion(127);
		base.isControllable = true;
	}

	public void OnPrayerStart(PrayInfo prayInfo)
	{
		if (MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest())
		{
			return;
		}
		if (prayTargetInfos.Count <= 0)
		{
			StartWaitingPacket(WAITING_PACKET.PLAYER_PRAYER_END, keep_sync: true);
		}
		prayTargetInfos.Add(prayInfo);
		if (playerSender != null)
		{
			playerSender.OnPrayerStart(prayInfo);
		}
		Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(prayInfo.targetId) as Player;
		if (player == null)
		{
			return;
		}
		BoostPrayInfo boostPrayInfo = boostPrayTargetInfoList.Find((BoostPrayInfo item) => item.prayTargetId == prayInfo.targetId);
		if (boostPrayInfo == null)
		{
			boostPrayInfo = new BoostPrayInfo();
			boostPrayInfo.prayerId = id;
			boostPrayInfo.prayTargetId = prayInfo.targetId;
			boostPrayInfo.isBoostByTypes[0] = (!isNpc && _IsGuard(SP_ATTACK_TYPE.NONE));
			boostPrayInfo.isBoostByTypes[1] = (IsInBarrier() && player.IsInBarrier());
			if (boostPrayInfo.IsBoost())
			{
				boostPrayTargetInfoList.Add(boostPrayInfo);
			}
			player.StartPrayed(id, boostPrayInfo);
		}
		else
		{
			boostPrayInfo.isBoostByTypes[0] = (!isNpc && _IsGuard(SP_ATTACK_TYPE.NONE));
			boostPrayInfo.isBoostByTypes[1] = (IsInBarrier() && player.IsInBarrier());
			player.StartPrayed(id, boostPrayInfo);
		}
	}

	public virtual void OnPrayerEnd(PrayInfo prayInfo)
	{
		if (playerSender != null)
		{
			playerSender.OnPrayerEnd(prayInfo);
		}
		for (int i = 0; i < prayTargetInfos.Count; i++)
		{
			if (prayTargetInfos[i].targetId == prayInfo.targetId)
			{
				prayTargetInfos.RemoveAt(i);
				break;
			}
		}
		if (prayTargetInfos.Count <= 0)
		{
			EndWaitingPacket(WAITING_PACKET.PLAYER_PRAYER_END);
		}
		Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(prayInfo.targetId) as Player;
		if (!(player == null))
		{
			BoostPrayInfo boostPrayInfo = boostPrayTargetInfoList.Find((BoostPrayInfo item) => item.prayTargetId == prayInfo.targetId);
			if (boostPrayInfo != null)
			{
				boostPrayTargetInfoList.Remove(boostPrayInfo);
			}
			player.EndPrayed(id);
		}
	}

	public void CheckPrayerBoost(Player p)
	{
		BoostPrayInfo boostPrayInfo = new BoostPrayInfo();
		boostPrayInfo.prayerId = id;
		boostPrayInfo.prayTargetId = p.id;
		boostPrayInfo.isBoostByTypes[0] = (!isNpc && _IsGuard(SP_ATTACK_TYPE.NONE));
		boostPrayInfo.isBoostByTypes[1] = (IsInBarrier() && p.IsInBarrier());
		OnChangeBoostPray(p.id, boostPrayInfo);
	}

	public void OnChangeBoostPray(int prayedId, BoostPrayInfo boostPrayInfo)
	{
		if (playerSender != null)
		{
			playerSender.OnChangePrayBoost(prayedId, boostPrayInfo);
		}
		Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(prayedId) as Player;
		if (player == null)
		{
			return;
		}
		BoostPrayInfo boostPrayInfo2 = boostPrayTargetInfoList.Find((BoostPrayInfo item) => item.prayTargetId == boostPrayInfo.prayTargetId);
		if (boostPrayInfo2 != null)
		{
			int i = 0;
			for (int num = boostPrayInfo.isBoostByTypes.Length; i < num; i++)
			{
				boostPrayInfo2.isBoostByTypes[i] = boostPrayInfo.isBoostByTypes[i];
			}
			player.ChangeBoostPrayed(id, boostPrayInfo2);
			if (boostPrayInfo2.IsNoBoost())
			{
				boostPrayTargetInfoList.Remove(boostPrayInfo2);
				player.EndBoostPrayed(id);
			}
		}
		else if (!boostPrayInfo.IsNoBoost())
		{
			boostPrayTargetInfoList.Add(boostPrayInfo);
			player.StartBoostPrayed(id, boostPrayInfo);
		}
	}

	public void StartBoostPrayed(int prayerId, BoostPrayInfo info)
	{
		if (info != null && !info.IsNoBoost() && info.prayerId == prayerId)
		{
			boostPrayedInfoList.Add(info);
		}
	}

	public void EndBoostPrayed(int prayerId)
	{
		BoostPrayInfo boostPrayInfo = boostPrayedInfoList.Find((BoostPrayInfo item) => item.prayerId == prayerId);
		if (boostPrayInfo != null)
		{
			boostPrayedInfoList.Remove(boostPrayInfo);
		}
	}

	public void ChangeBoostPrayed(int prayerId, BoostPrayInfo changedInfo)
	{
		BoostPrayInfo boostPrayInfo = boostPrayedInfoList.Find((BoostPrayInfo item) => item.prayerId == prayerId);
		if (boostPrayInfo == null)
		{
			boostPrayedInfoList.Add(changedInfo);
		}
		else
		{
			boostPrayInfo.Copy(changedInfo);
		}
	}

	public void StartPrayed(int prayerId, BoostPrayInfo info)
	{
		if (!prayerIds.Contains(prayerId))
		{
			prayerIds.Add(prayerId);
		}
		StartBoostPrayed(prayerId, info);
		if (IsCoopNone() || IsOriginal())
		{
			if (base.isDead)
			{
				DeadCount(rescueTime, IsPrayed() || isStopCounter || isWaitingResurrectionHoming);
			}
			else if (IsStone())
			{
				StoneCount(stoneRescueTime, IsPrayed() || isStopCounter);
			}
		}
	}

	public void EndPrayed(int prayerId)
	{
		prayerIds.Remove(prayerId);
		EndBoostPrayed(prayerId);
		if (IsCoopNone() || IsOriginal())
		{
			if (base.isDead)
			{
				DeadCount(rescueTime, IsPrayed() || isStopCounter || isWaitingResurrectionHoming);
			}
			else if (IsStone())
			{
				StoneCount(stoneRescueTime, IsPrayed() || isStopCounter);
			}
		}
	}

	public int GetInitRescueCount()
	{
		return 0;
	}

	public bool EnableRescueCountup()
	{
		bool result = false;
		if (MonoBehaviourSingleton<FieldManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.I.currentIsValidBoss)
		{
			result = true;
		}
		if (MonoBehaviourSingleton<QuestManager>.IsValid() && QuestManager.IsValidInGameExplore())
		{
			result = true;
		}
		return result;
	}

	public void ActChangeUniqueEquipment(StageObjectManager.CreatePlayerInfo createPlayerInfo)
	{
		EndAction();
		base.actionID = (ACTION_ID)27;
		PlayMotion(128);
		changePlayerInfo = createPlayerInfo;
	}

	public void ActChangeWeapon(CharaInfo.EquipItem item, int weapon_index)
	{
		EndAction();
		base.actionID = (ACTION_ID)27;
		PlayMotion(128);
		changeWeaponItem = item;
		changeWeaponIndex = weapon_index;
		changePlayerInfo = null;
		if (playerSender != null)
		{
			playerSender.OnChangeWeapon();
		}
	}

	public virtual void ApplyChangeWeapon(CharaInfo.EquipItem item, int weapon_index, StageObjectManager.CreatePlayerInfo createPlayerInfo = null)
	{
		if (!base.isDead && base.actionID != (ACTION_ID)27)
		{
			ActChangeWeapon(item, weapon_index);
		}
		EndWaitingPacket(WAITING_PACKET.PLAYER_APPLY_CHANGE_WEAPON);
		isChangingWeapon = true;
		changeWeaponStartTime = Time.time;
		hitOffFlag |= HIT_OFF_FLAG.INVICIBLE;
		if (base.isDead)
		{
			isInitDead = false;
		}
		string changeWeaponEffectName = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.changeWeaponEffectName;
		if (!string.IsNullOrEmpty(changeWeaponEffectName))
		{
			Transform effect = EffectManager.GetEffect(changeWeaponEffectName, base._transform);
			if (effect != null)
			{
				AddObjectList(effect.gameObject, OBJECT_LIST_TYPE.CHANGE_WEAPON);
			}
		}
		for (int i = 0; i < m_weaponCtrlList.Count; i++)
		{
			m_weaponCtrlList[i].OnChangeWeapon();
		}
		OnBuffEnd(BuffParam.BUFFTYPE.LUNATIC_TEAR, sync: true);
		FinishBoostMode();
		EndEvolve();
		_EndGuard();
		_EndBuffShadowSealing();
		EndCarry();
		DeleteWeaponLinkEffectAll();
		DetachRootEffectTemporary();
		PlayerLoader.OnCompleteLoad callback = delegate
		{
			SetEnableNodeRenderer("BODY", enable: false);
			if (loader.shadow != null)
			{
				loader.shadow.gameObject.SetActive(value: false);
			}
			pairSwordsCtrl.OnLoadComplete();
			ReAttachRootEffect();
			changePlayerInfo = null;
		};
		if (createPlayerInfo == null)
		{
			LoadWeapon(item, weapon_index, callback);
		}
		else
		{
			LoadUniqueEquipment(createPlayerInfo, callback);
		}
		if (playerSender != null)
		{
			playerSender.OnApplyChangeWeapon(item, weapon_index);
		}
		if (item != null)
		{
			OnCheckAndResizeColliderOsMapByWeapon(item.eId);
		}
	}

	public void ActGather(GatherPointObject gather_point)
	{
		if (gather_point == null)
		{
			return;
		}
		EndAction();
		targetGatherPoint = gather_point;
		isGatherInterruption = false;
		base.actionID = (ACTION_ID)28;
		PlayMotion(targetGatherPoint.viewData.actStateName);
		if (!string.IsNullOrEmpty(targetGatherPoint.viewData.toolModelName))
		{
			LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.gatherPointToolTable.Get(targetGatherPoint.viewData.toolModelName);
			if (loadObject != null)
			{
				actionRendererModel = (loadObject.loadedObject as GameObject);
				actionRendererNodeName = targetGatherPoint.viewData.toolNodeName;
			}
		}
		Vector3 position = targetGatherPoint._transform.position;
		position.y = 0f;
		if (IsCoopNone() || IsOriginal())
		{
			SetActionPosition(position, flag: true);
		}
		Vector3 lerpRotation = position - base._transform.position;
		lerpRotation.y = 0f;
		SetLerpRotation(lerpRotation);
		if (playerSender != null)
		{
			playerSender.OnActGather(gather_point);
		}
	}

	public virtual bool ApplyGather()
	{
		if (isAppliedGather)
		{
			return false;
		}
		if (targetGatherPoint == null)
		{
			return false;
		}
		if (isGatherInterruption)
		{
			return false;
		}
		isAppliedGather = true;
		return true;
	}

	public void ActSonar(int id)
	{
		if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			return;
		}
		IFieldGimmickObject fieldGimmickObj = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.Sonar, id);
		if (fieldGimmickObj != null)
		{
			FieldSonarObject fieldSonarObject = fieldGimmickObj as FieldSonarObject;
			if (!(fieldSonarObject == null))
			{
				fieldSonarObject.StartSonar();
			}
		}
	}

	public void ActReadStory(int id)
	{
		if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			return;
		}
		IFieldGimmickObject fieldGimmickObj = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.ReadStory, id);
		if (fieldGimmickObj != null)
		{
			FieldReadStoryObject fieldReadStoryObject = fieldGimmickObj as FieldReadStoryObject;
			if (!(fieldReadStoryObject == null))
			{
				fieldReadStoryObject.StartReadStory();
			}
		}
	}

	public void ActPortalGimmick(int id)
	{
		if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			return;
		}
		IFieldGimmickObject fieldGimmickObj = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.PortalGimmick, id);
		if (fieldGimmickObj != null)
		{
			FieldPortalGimmickObject fieldPortalGimmickObject = fieldGimmickObj as FieldPortalGimmickObject;
			if (!(fieldPortalGimmickObject == null))
			{
				fieldPortalGimmickObject.StartReadStory();
			}
		}
	}

	public void ActQuestGimmick(int id)
	{
		if (!MonoBehaviourSingleton<InGameProgress>.IsValid() || MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop)
		{
			return;
		}
		IFieldGimmickObject fieldGimmickObj = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.QuestGimmick, id);
		if (fieldGimmickObj == null)
		{
			return;
		}
		FieldQuestGimmickObject fieldQuestGimmickObject = fieldGimmickObj as FieldQuestGimmickObject;
		if (fieldQuestGimmickObject == null)
		{
			return;
		}
		EndAction();
		PlayMotion(fieldQuestGimmickObject.viewData.actStateName);
		if (!string.IsNullOrEmpty(fieldQuestGimmickObject.viewData.toolModelName))
		{
			LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.gatherPointToolTable.Get(fieldQuestGimmickObject.viewData.toolModelName);
			if (loadObject != null)
			{
				actionRendererModel = (loadObject.loadedObject as GameObject);
				actionRendererNodeName = fieldQuestGimmickObject.viewData.toolNodeName;
			}
		}
		Vector3 position = fieldQuestGimmickObject.GetTransform().position;
		position.y = 0f;
		if (IsCoopNone() || IsOriginal())
		{
			SetActionPosition(position, flag: true);
		}
		Vector3 lerpRotation = position - base._transform.position;
		lerpRotation.y = 0f;
		SetLerpRotation(lerpRotation);
		if ((object)playerSender != null)
		{
			playerSender.OnActQuestGimmick(id);
		}
		fieldQuestGimmickObject.StartAction(this);
		questGimmickObject = fieldQuestGimmickObject;
	}

	protected void OnQuestGimmickEnd()
	{
		if (!(questGimmickObject == null))
		{
			if (this is Self)
			{
				questGimmickObject.OnEndAction();
			}
			questGimmickObject = null;
		}
	}

	public void ActGatherGimmick(int id)
	{
		if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			return;
		}
		IFieldGimmickObject fieldGimmickObj = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.GatherGimmick, id);
		if (fieldGimmickObj == null)
		{
			return;
		}
		FieldGatherGimmickObject fieldGatherGimmickObject = fieldGimmickObj as FieldGatherGimmickObject;
		if (fieldGatherGimmickObject == null)
		{
			return;
		}
		GATHER_GIMMICK_TYPE gatherGimmickType = fieldGatherGimmickObject.GetGatherGimmickType();
		if (gatherGimmickType != GATHER_GIMMICK_TYPE.FISHING || !fishingCtrl.CanFishing())
		{
			return;
		}
		EndAction();
		float add_margin_time = 10f;
		gatherGimmickType = fieldGatherGimmickObject.GetGatherGimmickType();
		if (gatherGimmickType == GATHER_GIMMICK_TYPE.FISHING)
		{
			FieldFishingGimmickObject fieldFishingGimmickObject = fieldGatherGimmickObject as FieldFishingGimmickObject;
			base.actionID = (ACTION_ID)40;
			PlayMotion(138);
			fishingCtrl.Start(fieldGatherGimmickObject.lotId, fieldGatherGimmickObject.GetTransform(), fieldFishingGimmickObject.modelIndex);
			LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.gatherPointToolTable.Get("Fishingrod");
			if (loadObject != null)
			{
				actionRendererModel = (loadObject.loadedObject as GameObject);
				actionRendererNodeName = "R_Wep";
			}
			add_margin_time = fishingCtrl.GetMaxWaitPacketSec();
		}
		Vector3 position = fieldGatherGimmickObject.GetTransform().position;
		position.y = 0f;
		if (IsCoopNone() || IsOriginal())
		{
			SetActionPosition(position, flag: true);
		}
		Vector3 lerpRotation = position - base._transform.position;
		lerpRotation.y = 0f;
		SetLerpRotation(lerpRotation);
		fieldGatherGimmickObject.StartAction(this, IsCoopNone() || IsOriginal());
		gatherGimmickObject = fieldGatherGimmickObject;
		StartWaitingPacket(WAITING_PACKET.PLAYER_GATHER_GIMMICK, keep_sync: true, add_margin_time);
		if ((object)playerSender != null)
		{
			playerSender.OnActGatherGimmick(id);
		}
	}

	public void OnGatherGimmickState(int state)
	{
		if (!(gatherGimmickObject == null))
		{
			GATHER_GIMMICK_TYPE gatherGimmickType = gatherGimmickObject.GetGatherGimmickType();
			if ((uint)(gatherGimmickType - 1) <= 1u)
			{
				fishingCtrl.ChangeState((FishingController.eState)state);
			}
		}
	}

	protected void OnGatherGimmickGet()
	{
		if (!(gatherGimmickObject == null))
		{
			GATHER_GIMMICK_TYPE gatherGimmickType = gatherGimmickObject.GetGatherGimmickType();
			if (gatherGimmickType == GATHER_GIMMICK_TYPE.FISHING)
			{
				fishingCtrl.Get();
			}
		}
	}

	protected void OnGatherGimmickEnd()
	{
		if (!(gatherGimmickObject == null))
		{
			GATHER_GIMMICK_TYPE gatherGimmickType = gatherGimmickObject.GetGatherGimmickType();
			if (gatherGimmickType == GATHER_GIMMICK_TYPE.FISHING)
			{
				fishingCtrl.End();
			}
			gatherGimmickObject.OnUseEnd(this, IsCoopNone() || IsOriginal());
			gatherGimmickObject = null;
		}
	}

	public void ActBingo(int id)
	{
		if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			return;
		}
		IFieldGimmickObject fieldGimmickObj = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.Bingo, id);
		if (fieldGimmickObj != null)
		{
			FieldBingoObject fieldBingoObject = fieldGimmickObj as FieldBingoObject;
			if (!(fieldBingoObject == null))
			{
				fieldBingoObject.OpenBingo();
			}
		}
	}

	public void ActCannonStandby(int id)
	{
		if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			return;
		}
		IFieldGimmickCannon fieldGimmickCannon = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.Cannon, id) as IFieldGimmickCannon;
		if (fieldGimmickCannon != null && !fieldGimmickCannon.IsUsing() && fieldGimmickCannon.IsAbleToUse())
		{
			base.actionID = (ACTION_ID)31;
			targetFieldGimmickCannon = fieldGimmickCannon;
			_position = fieldGimmickCannon.GetPosition();
			base._rigidbody.isKinematic = true;
			fieldGimmickCannon.OnBoard(this);
			PlayMotion(131);
			SetCannonState(CANNON_STATE.STANDBY);
			if (playerSender != null)
			{
				playerSender.OnCannonStandby(id);
			}
		}
	}

	public void ActCannonShot()
	{
		if (cannonState == CANNON_STATE.READY && targetFieldGimmickCannon != null && !targetFieldGimmickCannon.IsCooling())
		{
			if (IsPlayingMotion(132))
			{
				SetNextTrigger();
			}
			targetFieldGimmickCannon.Shot();
			if (playerSender != null)
			{
				playerSender.OnCannonShot();
			}
		}
	}

	public virtual void CancelCannonMode()
	{
		if (targetFieldGimmickCannon != null && targetFieldGimmickCannon.IsUsing())
		{
			targetFieldGimmickCannon.OnLeave();
		}
		SetCannonState(CANNON_STATE.NONE);
		targetFieldGimmickCannon = null;
		base._rigidbody.isKinematic = false;
	}

	public void ActCoopFishingStart(int id)
	{
		if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			return;
		}
		IFieldGimmickObject fieldGimmickObj = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.CoopFishing, id);
		if (fieldGimmickObj == null)
		{
			return;
		}
		FieldGimmickCoopFishing fieldGimmickCoopFishing = fieldGimmickObj as FieldGimmickCoopFishing;
		if (!(fieldGimmickCoopFishing == null))
		{
			EndAction();
			_position = fieldGimmickCoopFishing.GetCoopPos();
			_rotation = fieldGimmickCoopFishing.GetCoopRot();
			fieldGimmickCoopFishing.SetPosition(_position);
			base.actionID = (ACTION_ID)41;
			PlayMotion(139);
			fishingCtrl.CoopStart();
			fishingCtrl.SetCoopOwnerUserId(fieldGimmickCoopFishing.GetOwnerUserId());
			fishingCtrl.SetCoopOwnerPlayerId(fieldGimmickCoopFishing.GetOwnerPlayerId());
			fishingCtrl.SetCoopOwnerClientId(fieldGimmickCoopFishing.GetOwnerClientId());
			Vector3 position = fieldGimmickCoopFishing.GetTransform().position;
			position.y = 0f;
			if (IsOriginal())
			{
				SetActionPosition(position, flag: true);
			}
			Vector3 lerpRotation = position - base._transform.position;
			lerpRotation.y = 0f;
			SetLerpRotation(lerpRotation);
			gatherGimmickObject = fieldGimmickCoopFishing;
			if (playerSender != null)
			{
				playerSender.OnActCoopFishingStart(id);
			}
		}
	}

	public virtual bool ActSkillAction(int skill_index, bool isGuestUsingSecondGrade = false)
	{
		SkillInfo.SkillParam skillParam = GetSkillParam(skill_index);
		if (skillParam == null || !skillParam.isValid)
		{
			return false;
		}
		if (!IsOriginal() && isGuestUsingSecondGrade)
		{
			skillParam.useGaugeCounter = (int)skillParam.GetMaxGaugeValue();
			skillParam.isUsingSecondGrade = true;
		}
		if (skillParam.tableData.isTeleportation)
		{
			skillInfo.skillIndex = skill_index;
			if (IsCoopNone() || IsOriginal())
			{
				SetSelfBuffByBuffTable(skillParam);
				MakeInvincible(GetInvincibleTimeOnSkillTeleportation(skillParam));
			}
			ActSkillTeleportation();
		}
		else
		{
			EndAction();
			int i = 0;
			for (int count = m_weaponCtrlList.Count; i < count; i++)
			{
				m_weaponCtrlList[i].OnActSkillAction();
			}
			skillInfo.skillIndex = skill_index;
			base.actionID = (ACTION_ID)22;
			if (!string.IsNullOrEmpty(skillParam.tableData.castStateName))
			{
				PlayMotion(skillParam.tableData.castStateName);
				isSkillCastState = true;
			}
			else
			{
				PlayMotion(skillParam.tableData.actStateName);
			}
		}
		skillInfo.OnActSkillAction();
		if (!skillParam.tableData.isTeleportation)
		{
			isActSkillAction = true;
		}
		isUsingSecondGradeSkill = skillParam.isUsingSecondGrade;
		if (IsCoopNone() || IsOriginal())
		{
			SetAttackActionPosition();
		}
		SKILL_SLOT_TYPE type = skillParam.tableData.type;
		if (type == SKILL_SLOT_TYPE.ATTACK)
		{
			base.attackStartTarget = base.actionTarget;
		}
		if ((float)skillParam.tableData.skillRange > 0f && skillRangeEffect == null)
		{
			Transform effect = EffectManager.GetEffect(playerParameter.skillRangeEffectName, base._transform);
			if (effect != null)
			{
				Vector3 localScale = effect.localScale;
				effect.localScale = localScale * (playerParameter.revivalRange / 0.5f);
				skillRangeEffect = effect;
			}
		}
		if (playerSender != null)
		{
			playerSender.OnActSkillAction(skill_index, skillParam);
		}
		if (MonoBehaviourSingleton<UIPlayerAnnounce>.IsValid())
		{
			MonoBehaviourSingleton<UIPlayerAnnounce>.I.StartSkill(skillParam.tableData.name, this);
		}
		if (bossBrain != null)
		{
			bossBrain.HandleEvent(BRAIN_EVENT.PLAYER_SKILL, this);
		}
		MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.AddSkillCount((int)skillParam.tableData.id);
		return true;
	}

	private void ActSkillTeleportation()
	{
		if (IsPuppet())
		{
			EffectManager.OneShot("ef_btl_sk_warp_02_02", _position, _rotation);
			return;
		}
		InGameSettingsManager.Player.TeleportationInfo teleportationInfo = playerParameter.teleportationInfo;
		Vector3 pos = Vector3.zero;
		bool flag = false;
		EffectManager.OneShot("ef_btl_sk_warp_02_01", base.rootNode.position, _rotation);
		if (snatchCtrl.GetSnatchPos(out pos))
		{
			flag = true;
		}
		else if (GetTargetPos(out pos))
		{
			flag = true;
		}
		else if (StageObjectManager.CanTargetBoss)
		{
			Vector3 position = MonoBehaviourSingleton<StageObjectManager>.I.boss._position;
			pos = position;
			RaycastHit hit = default(RaycastHit);
			if (AIUtility.RaycastOpponent(this, position, out hit))
			{
				pos = hit.point;
			}
			flag = true;
		}
		if (flag && !MonoBehaviourSingleton<StageManager>.I.CheckPosInside(pos))
		{
			RaycastHit hit2 = default(RaycastHit);
			if (AIUtility.RaycastWallAndBlock(this, pos, out hit2))
			{
				if (MonoBehaviourSingleton<StageManager>.I.CheckPosInside(hit2.point))
				{
					pos = hit2.point;
				}
				else
				{
					flag = false;
				}
			}
			else
			{
				flag = false;
			}
		}
		if (flag && !IsValidBuffBlind())
		{
			pos.y = _position.y;
			pos += Vector3.Normalize(_position - pos) * GetOffsetOnSkillTeleportation();
			_rotation = Quaternion.LookRotation(pos - _position, Vector3.up);
			_position = pos;
		}
		else
		{
			pos = _position + _forward * teleportationInfo.forwardScalar;
			if (MonoBehaviourSingleton<StageManager>.I.CheckPosInside(pos))
			{
				_position = pos;
			}
		}
		EffectManager.OneShot("ef_btl_sk_warp_02_02", _position + base.rootNode.localPosition, _rotation);
	}

	private float GetOffsetOnSkillTeleportation()
	{
		if (playerParameter.teleportationInfo == null)
		{
			return 1f;
		}
		InGameSettingsManager.Player.TeleportationInfo teleportationInfo = playerParameter.teleportationInfo;
		float result = teleportationInfo.offsetScalar;
		if (teleportationInfo.offsetByWeaponAndAttack.IsNullOrEmpty())
		{
			return result;
		}
		int i = 0;
		for (int num = teleportationInfo.offsetByWeaponAndAttack.Length; i < num; i++)
		{
			if (teleportationInfo.offsetByWeaponAndAttack[i].attackMode == (int)attackMode && teleportationInfo.offsetByWeaponAndAttack[i].attackID == base.attackID)
			{
				result = teleportationInfo.offsetByWeaponAndAttack[i].value;
			}
		}
		return result;
	}

	private float GetInvincibleTimeOnSkillTeleportation(SkillInfo.SkillParam skillParam)
	{
		if (skillParam == null)
		{
			return 0f;
		}
		if (skillParam.tableData == null)
		{
			return 0f;
		}
		float num = skillParam.tableData.supportTime[0];
		GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(skillParam.tableData.growID, skillParam.baseInfo.level, skillParam.baseInfo.exceedCnt);
		if (growSkillItemData == null)
		{
			return num;
		}
		return num * (float)(int)growSkillItemData.supprtTime[0].rate * 0.01f + (float)growSkillItemData.supprtTime[0].add;
	}

	public virtual void CheckSkillCastLoop()
	{
		if (isSkillCastLoop && Time.time - skillCastLoopStartTime >= skillCastLoopTime)
		{
			SetChangeTrigger(skillCastLoopTrigger);
			isSkillCastLoop = false;
			skillCastLoopStartTime = -1f;
			skillCastLoopTime = 0f;
			skillCastLoopTrigger = null;
		}
	}

	private void ApplySkillParam()
	{
		if (isAppliedSkillParam)
		{
			return;
		}
		if (skillRangeEffect != null)
		{
			EffectManager.ReleaseEffect(skillRangeEffect.gameObject);
			skillRangeEffect = null;
		}
		isAppliedSkillParam = true;
		if (IsPuppet())
		{
			return;
		}
		SkillInfo.SkillParam actSkillParam = skillInfo.actSkillParam;
		if (actSkillParam == null)
		{
			return;
		}
		float num = (float)actSkillParam.tableData.skillRange * (float)actSkillParam.tableData.skillRange;
		if (actSkillParam.healHp > 0)
		{
			HealData healData = new HealData(actSkillParam.healHp, actSkillParam.tableData.healType, HEAL_EFFECT_TYPE.BASIS, new List<int>
			{
				10
			});
			if (actSkillParam.tableData.selfOnly)
			{
				OnHealReceive(healData);
			}
			else if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				if (num > 0f && MonoBehaviourSingleton<StageObjectManager>.I.ExistsEnemyValiedHealAttack())
				{
					new GameObject("HealAttackObject").AddComponent<HealAttackObject>().Initialize(this, base._transform, actSkillParam, Vector3.zero, Vector3.zero, 0f, 12);
				}
				List<StageObject>.Enumerator enumerator = MonoBehaviourSingleton<StageObjectManager>.I.playerList.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Player player = enumerator.Current as Player;
					if (!(num > 0f) || !((player._transform.position - base._transform.position).sqrMagnitude > num))
					{
						player.OnHealReceive(healData);
					}
				}
			}
		}
		HEAL_TYPE healType = actSkillParam.tableData.healType;
		if (healType == HEAL_TYPE.RESURRECTION_ALL && MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
			for (int i = 0; i < playerList.Count; i++)
			{
				Player player2 = playerList[i] as Player;
				if (!(player2 == null))
				{
					player2.OnResurrectionReceive();
				}
			}
		}
		for (int j = 0; j < 3; j++)
		{
			if (actSkillParam.tableData.supportType[j] == BuffParam.BUFFTYPE.NONE || actSkillParam.tableData.supportType[j] >= BuffParam.BUFFTYPE.MAX)
			{
				continue;
			}
			if (actSkillParam.tableData.selfOnly)
			{
				SetSelfBuff(actSkillParam.tableData.id, actSkillParam.tableData.supportType[j], actSkillParam.supportValue[j], actSkillParam.supportTime[j], actSkillParam.skillIndex);
			}
			else
			{
				if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
				{
					continue;
				}
				List<StageObject>.Enumerator enumerator2 = MonoBehaviourSingleton<StageObjectManager>.I.playerList.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					Player player3 = enumerator2.Current as Player;
					if (!(num > 0f) || !((player3._transform.position - base._transform.position).sqrMagnitude > num))
					{
						BuffParam.BuffData data = new BuffParam.BuffData();
						data.type = actSkillParam.tableData.supportType[j];
						data.time = actSkillParam.supportTime[j];
						data.value = actSkillParam.supportValue[j];
						SetFromInfo(ref data);
						data.skillId = actSkillParam.tableData.id;
						if (data.isSkillChargeType())
						{
							player3.OnChargeSkillGaugeReceive(data.type, data.value, (player3 == this) ? actSkillParam.skillIndex : (-1));
						}
						else
						{
							player3.OnBuffReceive(data);
						}
					}
				}
			}
		}
		if (actSkillParam.tableData.buffTableIds.IsNullOrEmpty())
		{
			return;
		}
		List<BuffParam.BuffData> buffDataListByBuffTable = GetBuffDataListByBuffTable(actSkillParam);
		if (buffDataListByBuffTable.IsNullOrEmpty())
		{
			return;
		}
		int k = 0;
		for (int count = buffDataListByBuffTable.Count; k < count; k++)
		{
			if (actSkillParam.tableData.selfOnly)
			{
				OnBuffReceive(buffDataListByBuffTable[k]);
			}
			else
			{
				if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
				{
					continue;
				}
				List<StageObject>.Enumerator enumerator3 = MonoBehaviourSingleton<StageObjectManager>.I.playerList.GetEnumerator();
				while (enumerator3.MoveNext())
				{
					Player player4 = enumerator3.Current as Player;
					if (!(num > 0f) || !((player4._transform.position - base._transform.position).sqrMagnitude > num))
					{
						player4.OnBuffReceive(buffDataListByBuffTable[k]);
					}
				}
			}
		}
	}

	private List<BuffParam.BuffData> GetBuffDataListByBuffTable(SkillInfo.SkillParam skillParam)
	{
		if (skillParam == null)
		{
			return null;
		}
		if (skillParam.tableData == null)
		{
			return null;
		}
		if (skillParam.tableData.buffTableIds.IsNullOrEmpty())
		{
			return null;
		}
		List<BuffParam.BuffData> list = new List<BuffParam.BuffData>();
		int[] buffTableIds = skillParam.tableData.buffTableIds;
		for (int i = 0; i < buffTableIds.Length; i++)
		{
			BuffTable.BuffData data = Singleton<BuffTable>.I.GetData((uint)buffTableIds[i]);
			if (data != null && data.type > BuffParam.BUFFTYPE.NONE && data.type < BuffParam.BUFFTYPE.MAX)
			{
				BuffParam.BuffData data2 = new BuffParam.BuffData();
				data2.type = data.type;
				data2.interval = data.interval;
				data2.valueType = data.valueType;
				data2.time = data.duration;
				data2.skillId = skillParam.tableData.id;
				SetFromInfo(ref data2);
				float num = data.value;
				GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(data.growID, skillParam.baseInfo.level, skillParam.baseInfo.exceedCnt);
				if (growSkillItemData != null)
				{
					data2.time = data.duration * (float)(int)growSkillItemData.supprtTime[0].rate * 0.01f + (float)growSkillItemData.supprtTime[0].add;
					num = (float)(data.value * (int)growSkillItemData.supprtValue[0].rate) * 0.01f + (float)(int)growSkillItemData.supprtValue[0].add;
				}
				if (data2.valueType == BuffParam.VALUE_TYPE.RATE && BuffParam.IsTypeValueBasedOnHP(data2.type))
				{
					num = (float)base.hpMax * num * 0.01f;
				}
				data2.value = Mathf.FloorToInt(num);
				list.Add(data2);
			}
		}
		return list;
	}

	public void SetSelfBuff(uint id, BuffParam.BUFFTYPE type, int value, float time, int index)
	{
		BuffParam.BuffData data = new BuffParam.BuffData();
		data.skillId = id;
		data.type = type;
		data.value = value;
		data.time = time;
		SetFromInfo(ref data);
		if (data.isSkillChargeType())
		{
			OnChargeSkillGaugeReceive(data.type, data.value, index);
		}
		else if (data.isSkillChargeMoveType())
		{
			OnGetChargeSkillMove(data.type, data.value, index);
		}
		else
		{
			OnBuffReceive(data);
		}
	}

	public void SetSelfBuffByBuffTable(SkillInfo.SkillParam skillParam)
	{
		List<BuffParam.BuffData> buffDataListByBuffTable = GetBuffDataListByBuffTable(skillParam);
		if (!buffDataListByBuffTable.IsNullOrEmpty())
		{
			int i = 0;
			for (int count = buffDataListByBuffTable.Count; i < count; i++)
			{
				OnBuffReceive(buffDataListByBuffTable[i]);
			}
		}
	}

	public void OnHealReceive(HealData healData)
	{
		if (IsCoopNone() || IsOriginal())
		{
			ExecHealHp(healData);
		}
		else if (playerSender != null)
		{
			playerSender.OnHealReceive(healData);
		}
	}

	public virtual int ExecHealHp(HealData healData, bool isPacket = false)
	{
		if (base.isDead)
		{
			return 0;
		}
		DoHealType(healData.healType);
		if (buffParam.IsValidBuff(BuffParam.BUFFTYPE.CANT_HEAL_HP))
		{
			return 0;
		}
		if (healData.healHp > 0)
		{
			OnBuffEnd(BuffParam.BUFFTYPE.BLEEDING, sync: false);
		}
		int value = ApplyAbilityForHealHp(healData.healHp, healData.applyAbilityTypeList);
		value = Mathf.Clamp(value, 1, int.MaxValue);
		healHp = Mathf.Max(b: base.hp = Mathf.Clamp(base.hp + value, 0, base.hpMax), a: healHp);
		DoHealType(healData.healType);
		ExecHealEffect(healData.effectType);
		if (playerSender != null && !isPacket)
		{
			playerSender.OnGetHeal(healData);
		}
		if (bossBrain != null)
		{
			HateInfo hateInfo = new HateInfo();
			hateInfo.target = this;
			hateInfo.val = (int)((float)healData.healHp / (float)base.hpMax * 1000f);
			bossBrain.HandleEvent(BRAIN_EVENT.PLAYER_HEAL, hateInfo);
		}
		return value;
	}

	private int ApplyAbilityForHealHp(int baseHealHp, List<int> applyAbilityTypeList)
	{
		if (applyAbilityTypeList.IsNullOrEmpty())
		{
			return baseHealHp;
		}
		float num = 1f;
		for (int i = 0; i < applyAbilityTypeList.Count; i++)
		{
			switch (applyAbilityTypeList[i])
			{
			case 10:
				num += buffParam.GetHealHpRate();
				break;
			case 80:
				num += buffParam.GetHealUpDependsWeaponRate();
				break;
			}
		}
		num = Mathf.Clamp(num, 0f, float.MaxValue);
		return Mathf.FloorToInt((float)baseHealHp * num);
	}

	private void ExecHealEffect(HEAL_EFFECT_TYPE healEffectType)
	{
		if (!(healEffectTransform != null))
		{
			switch (healEffectType)
			{
			default:
				return;
			case HEAL_EFFECT_TYPE.BASIS:
				healEffectTransform = EffectManager.GetEffect("ef_btl_sk_heal_01", base._transform);
				break;
			case HEAL_EFFECT_TYPE.ABSORB:
				healEffectTransform = EffectManager.GetEffect("ef_btl_sk_heal_02", FindNode("Hip"));
				break;
			case HEAL_EFFECT_TYPE.HIT_ABSORB:
				healEffectTransform = EffectManager.GetEffect("ef_btl_sk_drain_01_01", FindNode("Root"));
				break;
			}
			if (!(healEffectTransform == null))
			{
				AddObjectList(healEffectTransform.gameObject, OBJECT_LIST_TYPE.STATIC);
			}
		}
	}

	public void DoHealType(HEAL_TYPE healType)
	{
		switch (healType)
		{
		case HEAL_TYPE.RESURRECTION_ALL:
			break;
		case HEAL_TYPE.PARALYZE:
			if (base.actionID == ACTION_ID.PARALYZE)
			{
				SetNextTrigger();
			}
			break;
		case HEAL_TYPE.POISON:
			OnBuffEnd(BuffParam.BUFFTYPE.POISON, sync: false);
			OnBuffEnd(BuffParam.BUFFTYPE.DEADLY_POISON, sync: false);
			break;
		case HEAL_TYPE.BURNING:
			OnBuffEnd(BuffParam.BUFFTYPE.BURNING, sync: false);
			break;
		case HEAL_TYPE.SPEEDDOWN:
			OnBuffEnd(BuffParam.BUFFTYPE.MOVE_SPEED_DOWN, sync: false);
			break;
		case HEAL_TYPE.SLIDE:
			OnBuffEnd(BuffParam.BUFFTYPE.SLIDE, sync: false);
			break;
		case HEAL_TYPE.SILENCE:
			OnBuffEnd(BuffParam.BUFFTYPE.SILENCE, sync: false);
			break;
		case HEAL_TYPE.ATTACK_SPEED_DOWN:
			OnBuffEnd(BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN, sync: false);
			break;
		case HEAL_TYPE.CANT_HEAL_HP:
			OnBuffEnd(BuffParam.BUFFTYPE.CANT_HEAL_HP, sync: false);
			break;
		case HEAL_TYPE.BLIND:
			OnBuffEnd(BuffParam.BUFFTYPE.BLIND, sync: false);
			break;
		case HEAL_TYPE.ALL_BADSTATUS:
			if (base.actionID == ACTION_ID.PARALYZE)
			{
				SetNextTrigger();
			}
			if (base.actionID == (ACTION_ID)43)
			{
				ActStoneEnd(stoneRescueTime);
			}
			OnBuffEnd(BuffParam.BUFFTYPE.POISON, sync: false);
			OnBuffEnd(BuffParam.BUFFTYPE.BLEEDING, sync: false);
			OnBuffEnd(BuffParam.BUFFTYPE.DEADLY_POISON, sync: false);
			OnBuffEnd(BuffParam.BUFFTYPE.BURNING, sync: false);
			OnBuffEnd(BuffParam.BUFFTYPE.MOVE_SPEED_DOWN, sync: false);
			OnBuffEnd(BuffParam.BUFFTYPE.SLIDE, sync: false);
			OnBuffEnd(BuffParam.BUFFTYPE.SILENCE, sync: false);
			OnBuffEnd(BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN, sync: false);
			OnBuffEnd(BuffParam.BUFFTYPE.CANT_HEAL_HP, sync: false);
			OnBuffEnd(BuffParam.BUFFTYPE.BLIND, sync: false);
			OnBuffEnd(BuffParam.BUFFTYPE.ACID, sync: false);
			break;
		}
	}

	public void OnChargeSkillGaugeReceive(BuffParam.BUFFTYPE buffType, int buffValue, int useSkillIndex)
	{
		if (IsCoopNone() || IsOriginal())
		{
			OnGetChargeSkillGauge(buffType, buffValue, useSkillIndex);
		}
		else if (playerSender != null)
		{
			playerSender.OnChargeSkillGaugeReceive(buffType, buffValue, useSkillIndex);
		}
	}

	public void OnGetChargeSkillGauge(BuffParam.BUFFTYPE buffType, int buffValue, int useSkillIndex, bool packet = false, bool isCorrectWaveMatch = true)
	{
		if (base.isDead)
		{
			return;
		}
		for (int i = 0; i < 3; i++)
		{
			int num = i + skillInfo.weaponOffset;
			if (num == useSkillIndex)
			{
				continue;
			}
			SkillInfo.SkillParam skillParam = skillInfo.GetSkillParam(num);
			if (skillParam == null)
			{
				continue;
			}
			float add_gauge = buffValue;
			switch (buffType)
			{
			case BuffParam.BUFFTYPE.SKILL_CHARGE_RATE:
				add_gauge = (float)(int)skillParam.useGauge * ((float)buffValue * 0.01f);
				break;
			case BuffParam.BUFFTYPE.SKILL_CHARGE_FIRE:
				if (skillParam.tableData.type != SKILL_SLOT_TYPE.ATTACK || !skillParam.tableData.HasElement(ELEMENT_TYPE.FIRE))
				{
					continue;
				}
				break;
			case BuffParam.BUFFTYPE.SKILL_CHARGE_WATER:
				if (skillParam.tableData.type != SKILL_SLOT_TYPE.ATTACK || !skillParam.tableData.HasElement(ELEMENT_TYPE.WATER))
				{
					continue;
				}
				break;
			case BuffParam.BUFFTYPE.SKILL_CHARGE_THUNDER:
				if (skillParam.tableData.type != SKILL_SLOT_TYPE.ATTACK || !skillParam.tableData.HasElement(ELEMENT_TYPE.THUNDER))
				{
					continue;
				}
				break;
			case BuffParam.BUFFTYPE.SKILL_CHARGE_SOIL:
				if (skillParam.tableData.type != SKILL_SLOT_TYPE.ATTACK || !skillParam.tableData.HasElement(ELEMENT_TYPE.SOIL))
				{
					continue;
				}
				break;
			case BuffParam.BUFFTYPE.SKILL_CHARGE_LIGHT:
				if (skillParam.tableData.type != SKILL_SLOT_TYPE.ATTACK || !skillParam.tableData.HasElement(ELEMENT_TYPE.LIGHT))
				{
					continue;
				}
				break;
			case BuffParam.BUFFTYPE.SKILL_CHARGE_DARK:
				if (skillParam.tableData.type != SKILL_SLOT_TYPE.ATTACK || !skillParam.tableData.HasElement(ELEMENT_TYPE.DARK))
				{
					continue;
				}
				break;
			case BuffParam.BUFFTYPE.AUTO_REVIVE_SKILL_CHARGE:
				if (skillParam.tableData.type != SKILL_SLOT_TYPE.ATTACK || num != skillInfo.weaponOffset)
				{
					continue;
				}
				add_gauge = skillParam.useGauge.value + skillParam.useGauge2.value;
				break;
			}
			skillInfo.AddUseGaugeByIndex(num, add_gauge, set_only: true, isForceSet: true, isCorrectWaveMatch);
		}
		if (skillChargeEffectTransform == null)
		{
			if (buffType == BuffParam.BUFFTYPE.SKILL_CHARGE_WHEN_DAMAGED || !isCorrectWaveMatch)
			{
				skillChargeEffectTransform = EffectManager.GetEffect("ef_btl_sk_recovery_magi_01", base._transform);
			}
			else
			{
				skillChargeEffectTransform = EffectManager.GetEffect("ef_btl_sk_charge_magi_01_02", base._transform);
			}
			if (skillChargeEffectTransform != null)
			{
				AddObjectList(skillChargeEffectTransform.gameObject, OBJECT_LIST_TYPE.STATIC);
			}
		}
		if (playerSender != null && !packet)
		{
			playerSender.OnGetChargeSkillGauge(buffType, buffValue, useSkillIndex, isCorrectWaveMatch);
		}
	}

	private void OnGetChargeSkillMove(BuffParam.BUFFTYPE type, float value, int useIndex)
	{
		int arrayIndex = -1;
		UISkillButton sameButtonIndex = MonoBehaviourSingleton<UISkillButtonGroup>.I.GetSameButtonIndex(useIndex, ref arrayIndex);
		if (sameButtonIndex == null)
		{
			return;
		}
		if (type == BuffParam.BUFFTYPE.SKILL_CHARGE_ABOVE)
		{
			EffectManager.GetUIEffect("ef_btl_sk_magi_move_01_01", sameButtonIndex.transform);
		}
		arrayIndex = ((type != BuffParam.BUFFTYPE.SKILL_CHARGE_ABOVE) ? (arrayIndex + 1) : (arrayIndex - 1));
		UISkillButton targetBtn = MonoBehaviourSingleton<UISkillButtonGroup>.I.GetUISkillButton(arrayIndex);
		if (targetBtn == null || targetBtn.buttonIndex == -1)
		{
			return;
		}
		AppMain.Delay(0.5f, delegate
		{
			if (!base.isDead)
			{
				skillInfo.AddUseGaugeByIndex(targetBtn.buttonIndex, value, set_only: true, isForceSet: true);
			}
		});
		if (skillChargeEffectTransform == null)
		{
			skillChargeEffectTransform = EffectManager.GetEffect("ef_btl_sk_recovery_magi_01", base._transform);
			if (skillChargeEffectTransform != null)
			{
				AddObjectList(skillChargeEffectTransform.gameObject, OBJECT_LIST_TYPE.STATIC);
			}
		}
	}

	public void OnResurrectionReceive()
	{
		if (IsCoopNone() || IsOriginal())
		{
			OnResurrection();
		}
		else
		{
			playerSender.OnResurrectionReceive();
		}
	}

	public void OnResurrection(bool isPacket = false)
	{
		if (base.isDead && (!MonoBehaviourSingleton<CoopManager>.IsValid() || !MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest()) && IsAbleToRescueByRemainRescueTime() && (base.actionID != (ACTION_ID)24 || !(rescueTime <= 0f)))
		{
			if (isPacket && playerSender != null)
			{
				playerSender.OnGetResurrection();
			}
			DeactivateRescueTimer();
			StartCoroutine(OnPlayAndDoResurrection(delegate
			{
				ActDeadStandup(base.hpMax, eContinueType.RESCUE);
			}));
		}
	}

	public void OnGetResurrection()
	{
		if (base.isDead && IsAbleToRescueByRemainRescueTime() && (base.actionID != (ACTION_ID)24 || !(rescueTime <= 0f)))
		{
			StartCoroutine(OnPlayResurrection());
		}
	}

	private IEnumerator OnPlayResurrection()
	{
		if (MonoBehaviourSingleton<EffectManager>.IsValid())
		{
			EffectManager.GetEffect("ef_btl_sk_heal_04_03", base._transform);
		}
		yield break;
	}

	private IEnumerator OnPlayAndDoResurrection(Action callback)
	{
		if (MonoBehaviourSingleton<EffectManager>.IsValid())
		{
			Transform effect = EffectManager.GetEffect("ef_btl_sk_heal_04_03", base._transform);
			if (effect != null)
			{
				Animator effectAnim = effect.gameObject.GetComponent<Animator>();
				if (effectAnim != null)
				{
					while (effectAnim.GetCurrentAnimatorStateInfo(0).fullPathHash != Animator.StringToHash("Base Layer.START"))
					{
						yield return null;
					}
					while (effectAnim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1f)
					{
						yield return null;
					}
				}
			}
		}
		if (base.isDead)
		{
			callback?.Invoke();
		}
		ActivateRescueTimer();
	}

	private void CheckAutoRevive()
	{
		autoReviveHp = 0;
		if (buffParam.IsValidBuff(BuffParam.BUFFTYPE.AUTO_REVIVE))
		{
			autoReviveHp = Mathf.CeilToInt((float)buffParam.GetValue(BuffParam.BUFFTYPE.AUTO_REVIVE) * 0.01f * (float)base.hpMax);
			isValidAutoReviveSkillChargeBuff = buffParam.IsValidAutoReviveSkillChargeBuff();
		}
	}

	public bool OnAutoRevive()
	{
		if (!base.isDead)
		{
			return false;
		}
		if ((float)autoReviveHp <= 0f)
		{
			return false;
		}
		DeactivateRescueTimer();
		StartCoroutine(OnPlayAndDoResurrection(delegate
		{
			ActDeadStandup(autoReviveHp, eContinueType.AUTO_REVIVE);
			if (isValidAutoReviveSkillChargeBuff)
			{
				OnGetChargeSkillGauge(BuffParam.BUFFTYPE.AUTO_REVIVE_SKILL_CHARGE, 1, -1);
			}
		}));
		return true;
	}

	public override bool OnBuffEnd(BuffParam.BUFFTYPE type, bool sync, bool isPlayEndEffect = true)
	{
		DeleteWeaponLinkEffect("BUFF_LOOP_" + type.ToString());
		bool result = base.OnBuffEnd(type, sync, isPlayEndEffect);
		int i = 0;
		for (int count = m_weaponCtrlList.Count; i < count; i++)
		{
			m_weaponCtrlList[i].OnBuffEnd(type);
		}
		return result;
	}

	public void OnSyncSpecialActionGauge(int weaponIndex, float currentSpActionGauge)
	{
		spActionGauge[weaponIndex] = currentSpActionGauge;
	}

	public void RegisterWeaponLinkEffect(EffectPlayProcessor.EffectSetting weaponEffectSetting)
	{
		foreach (WEAPON_EFFECT_DATA weaponEffectData in m_weaponEffectDataList)
		{
			if (weaponEffectData.setting.effectName == weaponEffectSetting.effectName)
			{
				return;
			}
		}
		WEAPON_EFFECT_DATA wEAPON_EFFECT_DATA = new WEAPON_EFFECT_DATA();
		wEAPON_EFFECT_DATA.setting = weaponEffectSetting;
		m_weaponEffectDataList.Add(wEAPON_EFFECT_DATA);
	}

	public void CreateWeaponLinkEffect(string settingName)
	{
		foreach (WEAPON_EFFECT_DATA weaponEffectData in m_weaponEffectDataList)
		{
			EffectPlayProcessor.EffectSetting setting = weaponEffectData.setting;
			if (!(setting.name != settingName))
			{
				Transform effect = EffectManager.GetEffect(setting.effectName, FindNode(setting.nodeName));
				if (effect != null)
				{
					effect.localPosition = setting.position;
					effect.localRotation = Quaternion.Euler(setting.rotation);
					float num = setting.scale;
					if (num == 0f)
					{
						num = 1f;
					}
					effect.localScale = Vector3.one * num;
					weaponEffectData.effectObj = effect.gameObject;
				}
			}
		}
	}

	public void DeleteWeaponLinkEffect(string settingName)
	{
		foreach (WEAPON_EFFECT_DATA weaponEffectData in m_weaponEffectDataList)
		{
			if (!(weaponEffectData.setting.name != settingName) && weaponEffectData.effectObj != null)
			{
				weaponEffectData.effectObj.transform.SetParent(null);
				EffectManager.ReleaseEffect(weaponEffectData.effectObj);
			}
		}
	}

	public void DeleteWeaponLinkEffectAll()
	{
		foreach (WEAPON_EFFECT_DATA weaponEffectData in m_weaponEffectDataList)
		{
			if (weaponEffectData.effectObj != null)
			{
				weaponEffectData.effectObj.transform.SetParent(null);
				EffectManager.ReleaseEffect(weaponEffectData.effectObj);
			}
		}
		m_weaponEffectDataList.Clear();
	}

	private void DetachRootEffectTemporary()
	{
		Transform attachTrans = MonoBehaviourSingleton<EffectManager>.IsValid() ? MonoBehaviourSingleton<EffectManager>.I._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform;
		effectTransTable.ForEachKeyAndValue(delegate(string key, Transform value)
		{
			if (value != null && value.parent == base.rootNode)
			{
				value.parent = attachTrans;
				rootEffectDetachTemporaryTable.Add(key, value);
			}
		});
	}

	private void ReAttachRootEffect()
	{
		rootEffectDetachTemporaryTable.ForEach(delegate(Transform value)
		{
			if (value != null)
			{
				value.parent = base.rootNode;
			}
		});
		rootEffectDetachTemporaryTable.Clear();
	}

	public bool IsExistSpecialAction()
	{
		switch (attackMode)
		{
		case ATTACK_MODE.ONE_HAND_SWORD:
		case ATTACK_MODE.TWO_HAND_SWORD:
		case ATTACK_MODE.PAIR_SWORDS:
			return true;
		case ATTACK_MODE.SPEAR:
			return spAttackType != SP_ATTACK_TYPE.HEAT;
		default:
			return false;
		}
	}

	public virtual bool ActSpecialAction(bool start_effect = true, bool isSuccess = true)
	{
		bool isActSpecialAction = true;
		bool flag = true;
		switch (attackMode)
		{
		case ATTACK_MODE.ONE_HAND_SWORD:
			switch (spAttackType)
			{
			case SP_ATTACK_TYPE.NONE:
			case SP_ATTACK_TYPE.HEAT:
			case SP_ATTACK_TYPE.BURST:
				ActGuard();
				break;
			case SP_ATTACK_TYPE.SOUL:
				ActAttack(playerParameter.ohsActionInfo.Soul_AlteredSpAttackId, send_packet: false);
				snatchCtrl.OnShot();
				break;
			case SP_ATTACK_TYPE.ORACLE:
			{
				int specialAttackId = playerParameter.ohsActionInfo.oracleOHSInfo.specialAttackId;
				string motionLayerName2 = GetMotionLayerName(attackMode, spAttackType, specialAttackId);
				ActAttack(specialAttackId, send_packet: false, sync_immediately: false, motionLayerName2);
				break;
			}
			}
			break;
		case ATTACK_MODE.TWO_HAND_SWORD:
		{
			int _attackId2 = playerParameter.specialActionInfo.spAttackID;
			string _motionLayerName2 = "Base Layer.";
			if (thsCtrl != null)
			{
				thsCtrl.GetSpActionInfo(spAttackType, extraAttackType, ref _attackId2, ref _motionLayerName2);
			}
			ActAttack(_attackId2, send_packet: false, sync_immediately: false, _motionLayerName2);
			break;
		}
		case ATTACK_MODE.SPEAR:
		{
			int _attackId = playerParameter.specialActionInfo.spAttackID;
			string _motionLayerName = "Base Layer.";
			if (spearCtrl != null)
			{
				spearCtrl.GetSpActionInfo(spAttackType, extraAttackType, ref _attackId, ref _motionLayerName);
			}
			if (CheckSpAttackType(SP_ATTACK_TYPE.NONE))
			{
				isActSpecialAction = false;
			}
			ActAttack(_attackId, send_packet: false, sync_immediately: false, _motionLayerName);
			break;
		}
		case ATTACK_MODE.PAIR_SWORDS:
			switch (spAttackType)
			{
			case SP_ATTACK_TYPE.NONE:
				if (base.attackID == 20)
				{
					ActAttack(playerParameter.pairSwordsActionInfo.wildDanceNoneChargeAttackID, send_packet: false);
				}
				else
				{
					ActAttack(playerParameter.pairSwordsActionInfo.wildDanceAttackID, send_packet: false);
				}
				break;
			case SP_ATTACK_TYPE.HEAT:
				if (!isSuccess)
				{
					ActAttackFailure(97, isSendPacket: false);
					break;
				}
				if (isBoostMode)
				{
					return true;
				}
				ActAttack(98, send_packet: false);
				StartBoostMode();
				if (MonoBehaviourSingleton<EffectManager>.IsValid())
				{
					Transform effect = EffectManager.GetEffect("ef_btl_wsk_twinsword_01_02");
					if (effect != null)
					{
						effect.position = _position;
						pairSwordsBoostModeAuraEffectList.Add(effect);
					}
					effect = EffectManager.GetEffect("ef_btl_wsk_twinsword_01_03", FindNode("R_Wep"));
					if (effect != null)
					{
						effect.localPosition = new Vector3(0.2f, 0f, 0f);
						pairSwordsBoostModeTrailEffectList.Add(effect);
					}
					effect = EffectManager.GetEffect("ef_btl_wsk_twinsword_01_03", FindNode("L_Wep"));
					if (effect != null)
					{
						effect.localPosition = new Vector3(-0.2f, 0f, 0f);
						pairSwordsBoostModeTrailEffectList.Add(effect);
					}
				}
				if (playerSender != null)
				{
					playerSender.OnSyncSpActionGauge();
				}
				break;
			case SP_ATTACK_TYPE.SOUL:
				ActAttack(playerParameter.pairSwordsActionInfo.Soul_SpLaserWaitAttackId, send_packet: false);
				pairSwordsCtrl.OnStartCharge();
				break;
			case SP_ATTACK_TYPE.BURST:
				if (!pairSwordsCtrl.ActBurstSpecialAction(ref start_effect))
				{
					return false;
				}
				flag = false;
				break;
			case SP_ATTACK_TYPE.ORACLE:
			{
				int num = 44;
				string motionLayerName = GetMotionLayerName(attackMode, spAttackType, num);
				ActAttack(num, send_packet: false, sync_immediately: false, motionLayerName);
				break;
			}
			}
			break;
		case ATTACK_MODE.ARROW:
			return false;
		}
		this.isActSpecialAction = isActSpecialAction;
		if (start_effect && CanPlayEffectEvent())
		{
			Transform effect2 = EffectManager.GetEffect(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.spActionStartEffectName, base._transform);
			if (effect2 != null)
			{
				AddObjectList(effect2.gameObject, OBJECT_LIST_TYPE.STATIC);
			}
		}
		if (flag && playerSender != null)
		{
			playerSender.OnActSpecialAction(start_effect, isSuccess);
		}
		return true;
	}

	public bool IsSpecialActionHit(ATTACK_MODE attack_mode, AttackHitInfo attack_info, AttackHitColliderProcessor.HitParam hit_param)
	{
		if (attack_info == null)
		{
			return false;
		}
		if (attack_info.isSkillReference)
		{
			return false;
		}
		if (attack_mode != attackMode)
		{
			return false;
		}
		bool flag = false;
		if (hit_param.processor != null)
		{
			BulletObject bulletObject = hit_param.processor.colliderInterface as BulletObject;
			if (bulletObject != null)
			{
				flag = bulletObject.isAimMode;
			}
		}
		if (flag && attack_info.rateInfoRate >= 1f)
		{
			return true;
		}
		if (CheckAttackModeAndSpType(ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.HEAT) && isBoostMode)
		{
			return true;
		}
		if (spearCtrl.IsSpecialActionHit())
		{
			return true;
		}
		return attack_info.toEnemy.isSpecialAttack;
	}

	private void SetValueSpActionGaugeMax(EquipItemTable.EquipItemData equipItemData, int weaponIndex)
	{
		if (equipItemData == null)
		{
			return;
		}
		float num = 1000f;
		bool flag = true;
		ATTACK_MODE mode;
		switch (equipItemData.type)
		{
		default:
			return;
		case (EQUIPMENT_TYPE)3:
			return;
		case EQUIPMENT_TYPE.ONE_HAND_SWORD:
			if (equipItemData.spAttackType == SP_ATTACK_TYPE.NONE)
			{
				return;
			}
			flag = false;
			mode = ATTACK_MODE.ONE_HAND_SWORD;
			break;
		case EQUIPMENT_TYPE.TWO_HAND_SWORD:
			switch (equipItemData.spAttackType)
			{
			default:
				return;
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
			}
			mode = ATTACK_MODE.TWO_HAND_SWORD;
			break;
		case EQUIPMENT_TYPE.SPEAR:
			switch (equipItemData.spAttackType)
			{
			default:
				return;
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
			}
			mode = ATTACK_MODE.SPEAR;
			break;
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
			mode = ATTACK_MODE.PAIR_SWORDS;
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
			mode = ATTACK_MODE.ARROW;
			break;
		}
		if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid() && weaponIndex == this.weaponIndex)
		{
			MonoBehaviourSingleton<UIPlayerStatus>.I.SetGaugeEffectColor(mode, equipItemData.spAttackType);
		}
		if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid() && weaponIndex == this.weaponIndex)
		{
			MonoBehaviourSingleton<UIEnduranceStatus>.I.SetGaugeEffectColor(equipItemData.spAttackType);
		}
		spActionGaugeMax[weaponIndex] = num;
		if (flag)
		{
			spActionGauge[weaponIndex] = num;
		}
	}

	public bool IsTwoHandSwordSpAttacking()
	{
		if (attackMode != ATTACK_MODE.TWO_HAND_SWORD)
		{
			return false;
		}
		if (spAttackType != 0 && spAttackType != SP_ATTACK_TYPE.HEAT)
		{
			return false;
		}
		if ((isActSpecialAction && IsFullCharge()) || isActTwoHandSwordHeatCombo)
		{
			return true;
		}
		return false;
	}

	public bool IsTwoHandSwordHeatUseGauge()
	{
		if (attackMode != ATTACK_MODE.TWO_HAND_SWORD)
		{
			return false;
		}
		if (spAttackType != SP_ATTACK_TYPE.HEAT)
		{
			return false;
		}
		if (!isActTwoHandSwordHeatCombo)
		{
			return false;
		}
		if (useGaugeLevel != 0)
		{
			return true;
		}
		UseSpGauge();
		return useGaugeLevel > 0;
	}

	public bool IsEnableChangeActionByLongTap()
	{
		ATTACK_MODE attackMode = this.attackMode;
		if (attackMode == ATTACK_MODE.TWO_HAND_SWORD)
		{
			if (thsCtrl == null)
			{
				return false;
			}
			return thsCtrl.IsEnableChangeActionByLongTap();
		}
		return false;
	}

	protected float GetBurstReloadMotionSpeedRate()
	{
		if (thsCtrl == null || !thsCtrl.IsEnableChangeReloadMotionSpeed)
		{
			return 1f;
		}
		if (buffParam == null || buffParam.passive == null)
		{
			return 1f;
		}
		float num = buffParam.passive.burstReloadActionSpeed + 1f;
		if (num < 0f)
		{
			return 1f;
		}
		return num;
	}

	private void FixedUpdateOneHandSword()
	{
		if (!CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL) || !snatchCtrl.IsSnatching())
		{
			return;
		}
		Vector3 forward = _forward;
		forward.y = 0f;
		forward.Normalize();
		Vector3 vector = snatchCtrl.GetSnatchPos() - _position;
		vector.y = 0f;
		int num = (Vector3.Cross(forward, vector).y >= 0f) ? 1 : (-1);
		float num2 = Vector3.Angle(forward, vector);
		Vector3 eulerAngles = _rotation.eulerAngles;
		float num3 = num2;
		if (rotateEventSpeed > 0f)
		{
			num3 = rotateEventSpeed * Time.deltaTime;
			if (num2 <= num3)
			{
				num3 = num2;
			}
		}
		_rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y + (float)num * num3, eulerAngles.z);
	}

	public void DeactiveSnatchMove()
	{
		enableEventMove = false;
		base.enableAddForce = false;
		eventMoveVelocity = Vector3.zero;
		SetVelocity(Vector3.zero);
		snatchCtrl.OnArrive();
	}

	public float GetRegionDamageRate(bool isDragonArmor)
	{
		float result = 1f;
		if (isDragonArmor)
		{
			return result;
		}
		if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			return result;
		}
		if (CheckAttackModeAndSpType(ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.SOUL) && thsCtrl.IsSoulSpAttackId(base.attackID) && chargeRate >= 1f)
		{
			return MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.soulRegionDamageRate;
		}
		return result;
	}

	public bool GetOneHandSwordBoostDownValue(ref int value, string attackInfoName)
	{
		value = 0;
		if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			return false;
		}
		if (!CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
		{
			return false;
		}
		if (base.actionID != ACTION_ID.ATTACK)
		{
			return false;
		}
		if (!isBoostMode)
		{
			return false;
		}
		if (attackInfoName != playerParameter.ohsActionInfo.Soul_BoostSpAttackName)
		{
			return false;
		}
		value = playerParameter.ohsActionInfo.Soul_BoostSpAttackDownValue;
		return true;
	}

	public bool GetOneHandSwordBoostDamageUpRate(ref float value)
	{
		value = 1f;
		if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			return false;
		}
		if (CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
		{
			if (base.actionID != ACTION_ID.ATTACK)
			{
				return false;
			}
			if (!isBoostMode)
			{
				return false;
			}
			value = playerParameter.ohsActionInfo.Soul_BoostElementDamageRate;
			return true;
		}
		if (CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.BURST))
		{
			if (base.actionID != ACTION_ID.ATTACK)
			{
				return false;
			}
			if (base.attackID == playerParameter.ohsActionInfo.burstOHSInfo.CounterAttackId)
			{
				value = playerParameter.ohsActionInfo.burstOHSInfo.elementDamageRate;
				return true;
			}
			if (!isBoostMode)
			{
				return false;
			}
			value = playerParameter.ohsActionInfo.burstOHSInfo.BoostElementDamageRate;
			return true;
		}
		return false;
	}

	public bool GetTwoHandSwordBoostDamageUpRate(ref float value)
	{
		value = 1f;
		if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			return false;
		}
		if (!CheckAttackModeAndSpType(ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
		{
			return false;
		}
		if (base.actionID != ACTION_ID.ATTACK)
		{
			return false;
		}
		if (!isBoostMode)
		{
			return false;
		}
		value = playerParameter.twoHandSwordActionInfo.soulBoostElementDamageRate;
		return true;
	}

	public bool GetIaiNormalDamageUp(ref float value)
	{
		value = 1f;
		if (!CheckAttackModeAndSpType(ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
		{
			return false;
		}
		if (thsCtrl == null)
		{
			return false;
		}
		return thsCtrl.GetIaiNormalDamageUp(ref value, base.attackID, chargeRate);
	}

	public bool GetArrowBoostDamageUpRate(ref float value)
	{
		value = 1f;
		if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			return false;
		}
		if (!CheckAttackModeAndSpType(ATTACK_MODE.ARROW, SP_ATTACK_TYPE.SOUL))
		{
			return false;
		}
		if (!isBoostMode)
		{
			return false;
		}
		value = playerParameter.arrowActionInfo.soulBoostElementDamageRate;
		return true;
	}

	public bool GetBurstShotNormalDamageUpRate(AttackHitInfo _info, ref float value)
	{
		value = 1f;
		if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			return false;
		}
		if (!IsBurstTwoHandSword() || thsCtrl == null)
		{
			return false;
		}
		return thsCtrl.GetBurstShotNormalDamageUpRate(_info.attackType, ref value);
	}

	public bool GetBurstShotElementDamageUpRate(AttackHitInfo _info, ref float value)
	{
		value = 1f;
		if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			return false;
		}
		if (!IsBurstTwoHandSword())
		{
			return false;
		}
		return thsCtrl.GetBurstShotElementDamageUpRate(_info.attackType, ref value);
	}

	public bool GetBurstArrowBombElementDamageUpRate(AttackHitInfo info, ref float value)
	{
		value = 1f;
		if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid() || (info.attackType != AttackHitInfo.ATTACK_TYPE.BOMB && info.attackType != AttackHitInfo.ATTACK_TYPE.BOOST_BOMB))
		{
			return false;
		}
		if (!InGameUtility.GetBombLevelByAttackInfo(info, out int lv))
		{
			return false;
		}
		if (playerParameter.arrowActionInfo.arrowBombElementDamageRateList.Count <= lv)
		{
			return false;
		}
		value = playerParameter.arrowActionInfo.arrowBombElementDamageRateList[lv];
		return true;
	}

	public bool GetOracleOHSElementDamageUpRate(AttackHitInfo info, ref float value)
	{
		value = 1f;
		if (!CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.ORACLE))
		{
			return false;
		}
		if (info.attackType == AttackHitInfo.ATTACK_TYPE.OHS_ORACLE_SP)
		{
			if (!isBoostMode)
			{
				value = playerParameter.ohsActionInfo.oracleOHSInfo.spAttackElementDamageRate;
			}
			else
			{
				value = playerParameter.ohsActionInfo.oracleOHSInfo.boostSpAttackElementDamageRate;
			}
		}
		else if (!isBoostMode)
		{
			value = playerParameter.ohsActionInfo.oracleOHSInfo.normalAttackElementDamageRate;
		}
		else
		{
			value = playerParameter.ohsActionInfo.oracleOHSInfo.boostNormalAttackElementDamageRate;
		}
		return true;
	}

	public bool GetOracleSpearElementDamageUpRate(AttackHitInfo info, ref float value)
	{
		value = 1f;
		if (!CheckAttackModeAndSpType(ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE))
		{
			return false;
		}
		value = spearCtrl.GetOracleElementDamageRate(info);
		return true;
	}

	public bool GetOraclePairSwordsElementDamageUpRate(AttackHitInfo info, ref float value)
	{
		value = 1f;
		if (!CheckAttackModeAndSpType(ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.ORACLE))
		{
			return false;
		}
		if (info.attackType == AttackHitInfo.ATTACK_TYPE.PAIR_SWORDS_ORACLE_SP)
		{
			value = playerParameter.pairSwordsActionInfo.Oracle_SpElementDamageRate - 1f;
		}
		else if (info.attackType == AttackHitInfo.ATTACK_TYPE.PAIR_SWORDS_ORACLE_RUSH_BOOST)
		{
			value = playerParameter.pairSwordsActionInfo.Oracle_RushElementDamageRate - 1f;
		}
		return true;
	}

	private float GetBoostAttackSpeedUp()
	{
		if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			return 0f;
		}
		if (base.actionID != ACTION_ID.ATTACK)
		{
			return 0f;
		}
		switch (attackMode)
		{
		case ATTACK_MODE.ONE_HAND_SWORD:
			switch (spAttackType)
			{
			case SP_ATTACK_TYPE.BURST:
			{
				InGameSettingsManager.Player.BurstOneHandSwordActionInfo burstOHSInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.burstOHSInfo;
				if (!isBoostMode)
				{
					return 0f;
				}
				if (base.attackID == burstOHSInfo.CounterAttackId)
				{
					return 0f;
				}
				return burstOHSInfo.boostAttackSpeedUpRate;
			}
			case SP_ATTACK_TYPE.ORACLE:
				if (!isBoostMode)
				{
					return 0f;
				}
				return MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.oracleOHSInfo.boostMotionSpeedAdditionRate;
			default:
				return 0f;
			}
		case ATTACK_MODE.PAIR_SWORDS:
			return pairSwordsCtrl.GetAttackSpeedUpRate();
		case ATTACK_MODE.TWO_HAND_SWORD:
			if (!isBoostMode)
			{
				return 0f;
			}
			if (spAttackType != SP_ATTACK_TYPE.SOUL)
			{
				return 0f;
			}
			if (thsCtrl == null)
			{
				return 0f;
			}
			return thsCtrl.TwoHandSwordBoostAttackSpeed;
		case ATTACK_MODE.SPEAR:
			if (spAttackType == SP_ATTACK_TYPE.ORACLE)
			{
				return spearCtrl.GetOracleAttackSpeedRate() - 1f;
			}
			break;
		}
		return 0f;
	}

	protected float GetAttackModeWalkSpeedUp()
	{
		if (base.actionID != ACTION_ID.MOVE)
		{
			return 0f;
		}
		if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			return 0f;
		}
		switch (attackMode)
		{
		case ATTACK_MODE.TWO_HAND_SWORD:
			if (thsCtrl != null)
			{
				return thsCtrl.GetWalkSpeedUp(spAttackType);
			}
			break;
		case ATTACK_MODE.PAIR_SWORDS:
			if (pairSwordsCtrl != null)
			{
				return pairSwordsCtrl.GetWalkSpeedUp(spAttackType);
			}
			break;
		case ATTACK_MODE.SPEAR:
			if (spearCtrl != null)
			{
				return spearCtrl.GetWalkSpeedUp(spAttackType);
			}
			break;
		}
		return 0f;
	}

	private float GetAttackModeAvoidUp()
	{
		if (base.actionID != ACTION_ID.MAX)
		{
			return 0f;
		}
		if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			return 0f;
		}
		switch (attackMode)
		{
		case ATTACK_MODE.PAIR_SWORDS:
			switch (spAttackType)
			{
			case SP_ATTACK_TYPE.HEAT:
			case SP_ATTACK_TYPE.SOUL:
				if (isBoostMode)
				{
					return MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.boostAvoidUpRate;
				}
				break;
			case SP_ATTACK_TYPE.BURST:
				return MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.Burst_AvoidSpeedUpRate;
			}
			break;
		case ATTACK_MODE.SPEAR:
			if (spearCtrl != null)
			{
				return spearCtrl.GetAvoidUp(spAttackType);
			}
			break;
		}
		return 0f;
	}

	private void UpdateSpActionGauge()
	{
		if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid() || !isBoostMode)
		{
			return;
		}
		float num = 0f;
		switch (attackMode)
		{
		case ATTACK_MODE.TWO_HAND_SWORD:
			if (spAttackType != SP_ATTACK_TYPE.SOUL)
			{
				return;
			}
			num = ((!enableInputCharge) ? (playerParameter.twoHandSwordActionInfo.soulBoostGaugeDecreasePerSecond * Time.deltaTime) : (playerParameter.twoHandSwordActionInfo.soulBoostChargeGaugeDecreasePerSecond * Time.deltaTime));
			break;
		case ATTACK_MODE.SPEAR:
			if (spAttackType == SP_ATTACK_TYPE.ORACLE)
			{
				return;
			}
			break;
		case ATTACK_MODE.ARROW:
			switch (spAttackType)
			{
			case SP_ATTACK_TYPE.SOUL:
				num = playerParameter.arrowActionInfo.soulBoostGaugeDecreasePerSecond * Time.deltaTime;
				break;
			case SP_ATTACK_TYPE.BURST:
				num = playerParameter.arrowActionInfo.burstBoostGaugeDecreasePerSecond * Time.deltaTime;
				break;
			}
			break;
		}
		float num2 = 1f + GetSpGaugeDecreasingRate();
		spActionGauge[weaponIndex] -= num * num2;
		if (spActionGauge[weaponIndex] <= 0f)
		{
			spActionGauge[weaponIndex] = 0f;
		}
	}

	public float GetSpGaugeDecreasingRate()
	{
		return buffParam.GetGaugeDecreaseRate() + extraSpGaugeDecreasingRate;
	}

	private void CheckContinueBoostMode()
	{
		if (!isBoostMode)
		{
			return;
		}
		switch (attackMode)
		{
		case ATTACK_MODE.ONE_HAND_SWORD:
		case ATTACK_MODE.ARROW:
			if ((spAttackType != SP_ATTACK_TYPE.SOUL && spAttackType != SP_ATTACK_TYPE.BURST && spAttackType != SP_ATTACK_TYPE.ORACLE) || (!IsCoopNone() && !IsOriginal()) || spActionGauge[weaponIndex] > 0f)
			{
				return;
			}
			break;
		case ATTACK_MODE.TWO_HAND_SWORD:
			if (spAttackType != SP_ATTACK_TYPE.SOUL || (!IsCoopNone() && !IsOriginal()) || spActionGauge[weaponIndex] > 0f)
			{
				return;
			}
			break;
		case ATTACK_MODE.SPEAR:
			if (!IsCoopNone() && !IsOriginal())
			{
				return;
			}
			if (spAttackType == SP_ATTACK_TYPE.SOUL)
			{
				if (spActionGauge[weaponIndex] > 0f)
				{
					return;
				}
			}
			else if (spAttackType != SP_ATTACK_TYPE.ORACLE || spearCtrl.StockedCount > 0)
			{
				return;
			}
			break;
		case ATTACK_MODE.PAIR_SWORDS:
			if (pairSwordsCtrl.CheckContinueBoostMode())
			{
				return;
			}
			break;
		}
		FinishBoostMode();
	}

	public void IncreaseSpActonGauge(AttackHitInfo attackInfo, Vector3 hitPosition, float baseValue = 0f)
	{
		bool dontIncreaseGauge = attackInfo.dontIncreaseGauge;
		AttackHitInfo.ATTACK_TYPE attackType = attackInfo.attackType;
		bool isSpecialAttack = attackInfo.toEnemy.isSpecialAttack;
		float atkRate = attackInfo.atkRate;
		if (dontIncreaseGauge || !MonoBehaviourSingleton<InGameSettingsManager>.IsValid() || (attackType == AttackHitInfo.ATTACK_TYPE.BOMB && !CheckAttackModeAndSpType(ATTACK_MODE.ARROW, SP_ATTACK_TYPE.BURST)))
		{
			return;
		}
		if (IsEvolveWeapon())
		{
			evolveCtrl.IncreaseCurrentGauge(attackType, attackMode);
			return;
		}
		bool flag = false;
		float _increaseValue = 0f;
		float _gaugeMax = 1000f;
		switch (attackMode)
		{
		case ATTACK_MODE.ONE_HAND_SWORD:
			switch (spAttackType)
			{
			case SP_ATTACK_TYPE.NONE:
				return;
			case SP_ATTACK_TYPE.HEAT:
				switch (attackType)
				{
				default:
					return;
				case AttackHitInfo.ATTACK_TYPE.NORMAL:
					_increaseValue = baseValue / (float)base.hpMax * playerParameter.ohsActionInfo.Heat_RevengeValue * (_CheckJustGuardSec() ? playerParameter.ohsActionInfo.Heat_RevengeJustGuardRate : playerParameter.ohsActionInfo.Heat_RevengeGuardRate);
					break;
				case AttackHitInfo.ATTACK_TYPE.COUNTER2:
					_increaseValue = (isJustGuard ? playerParameter.ohsActionInfo.Heat_RevengeJustCounterValue : playerParameter.ohsActionInfo.Heat_RevengeCounterValue);
					break;
				}
				flag = true;
				break;
			case SP_ATTACK_TYPE.SOUL:
			{
				if (soulEnergyCtrl == null)
				{
					return;
				}
				SoulEnergy soulEnergy2 = null;
				if (base.attackID != playerParameter.ohsActionInfo.Soul_AlteredSpAttackId || !isSpecialAttack)
				{
					return;
				}
				soulEnergy2 = soulEnergyCtrl.Get(playerParameter.ohsActionInfo.Soul_ComboGaugeIncreaseValue);
				if (soulEnergy2 != null)
				{
					if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
					{
						MonoBehaviourSingleton<UIPlayerStatus>.I.DirectionSoulGauge(soulEnergy2, hitPosition);
					}
					if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
					{
						MonoBehaviourSingleton<UIEnduranceStatus>.I.DirectionSoulGauge(soulEnergy2, hitPosition);
					}
				}
				return;
			}
			case SP_ATTACK_TYPE.BURST:
				if (attackType == AttackHitInfo.ATTACK_TYPE.COUNTER_BURST)
				{
					_increaseValue = 1000f;
				}
				flag = true;
				break;
			case SP_ATTACK_TYPE.ORACLE:
				_increaseValue = ((!isBoostMode) ? playerParameter.ohsActionInfo.oracleOHSInfo.spGaugeIncreasingValue : playerParameter.ohsActionInfo.oracleOHSInfo.spGaugeIncreasingValueWhileBoost);
				flag = true;
				break;
			}
			break;
		case ATTACK_MODE.TWO_HAND_SWORD:
			if (thsCtrl == null || !thsCtrl.GetSpGaugeIncreaseValue(attackType, base.attackID, atkRate, soulEnergyCtrl, hitPosition, chargeRate, ref _gaugeMax, ref _increaseValue))
			{
				return;
			}
			break;
		case ATTACK_MODE.SPEAR:
			switch (spAttackType)
			{
			case SP_ATTACK_TYPE.NONE:
				return;
			case SP_ATTACK_TYPE.HEAT:
			{
				if (attackType != 0 && attackType != AttackHitInfo.ATTACK_TYPE.FROM_AVOID)
				{
					return;
				}
				_gaugeMax = 999f;
				float num2 = weaponEquipItemDataList[weaponIndex].spAttackRate;
				_increaseValue = MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.jumpGaugeIncreaseBase * num2 * 0.01f;
				break;
			}
			case SP_ATTACK_TYPE.SOUL:
			{
				if (isSpecialAttack || soulEnergyCtrl == null)
				{
					return;
				}
				SoulEnergy soulEnergy = null;
				soulEnergy = soulEnergyCtrl.Get(playerParameter.spearActionInfo.Soul_GaugeIncreaseValue);
				if (soulEnergy != null)
				{
					if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
					{
						MonoBehaviourSingleton<UIPlayerStatus>.I.DirectionSoulGauge(soulEnergy, hitPosition);
					}
					if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
					{
						MonoBehaviourSingleton<UIEnduranceStatus>.I.DirectionSoulGauge(soulEnergy, hitPosition);
					}
				}
				return;
			}
			case SP_ATTACK_TYPE.ORACLE:
				if (IsValidBuff(BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS) || spearCtrl.FullStocked)
				{
					return;
				}
				_increaseValue = ((!(baseValue > 0f)) ? (_increaseValue + playerParameter.spearActionInfo.oracle.spChargingValue * atkRate) : (baseValue / (float)base.hpMax * CurrentWeaponSpActionGaugeMax));
				break;
			}
			break;
		case ATTACK_MODE.PAIR_SWORDS:
			switch (spAttackType)
			{
			case SP_ATTACK_TYPE.HEAT:
			{
				if ((attackType != 0 && attackType != AttackHitInfo.ATTACK_TYPE.FROM_AVOID) || isBoostMode)
				{
					return;
				}
				float num = weaponEquipItemDataList[weaponIndex].spAttackRate;
				_increaseValue = MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.boostGaugeIncreaseBase * num * 0.01f;
				break;
			}
			case SP_ATTACK_TYPE.SOUL:
				if (base.attackID == playerParameter.pairSwordsActionInfo.Soul_SpLaserShotAttackId)
				{
					return;
				}
				flag = true;
				_increaseValue = playerParameter.pairSwordsActionInfo.Soul_SoulGaugeIncreaseValueBySoulBullet;
				break;
			case SP_ATTACK_TYPE.BURST:
				if ((attackType != 0 && attackType != AttackHitInfo.ATTACK_TYPE.FROM_AVOID) || isBoostMode)
				{
					return;
				}
				if (pairSwordsCtrl.IsCombineMode())
				{
					flag = true;
				}
				_increaseValue = MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.Burst_BoostGaugeIncreaseBase;
				break;
			}
			break;
		case ATTACK_MODE.ARROW:
			switch (spAttackType)
			{
			case SP_ATTACK_TYPE.SOUL:
				if (isBoostMode)
				{
					return;
				}
				flag = true;
				_increaseValue = playerParameter.arrowActionInfo.soulGaugeIncreaseValue;
				break;
			case SP_ATTACK_TYPE.BURST:
			{
				if (isBoostMode || attackType != AttackHitInfo.ATTACK_TYPE.BOMB || !InGameUtility.GetBombLevelByAttackInfo(attackInfo, out int lv))
				{
					return;
				}
				List<float> burstGaugeIncreaseValueList = playerParameter.arrowActionInfo.burstGaugeIncreaseValueList;
				if (burstGaugeIncreaseValueList.Count <= lv)
				{
					return;
				}
				flag = true;
				_increaseValue = burstGaugeIncreaseValueList[lv];
				break;
			}
			}
			break;
		}
		float num3 = 1f + buffParam.GetGaugeIncreaseRate(spAttackType);
		spActionGauge[weaponIndex] += CalcWaveMatchSpGauge(_increaseValue * num3);
		if (spActionGauge[weaponIndex] >= _gaugeMax)
		{
			spActionGauge[weaponIndex] = _gaugeMax;
			if (!isBoostMode && flag)
			{
				StartBoostMode();
			}
		}
	}

	public void IncreaseSoulGauge(float baseValue, bool isJust)
	{
		if (base.isDead || !MonoBehaviourSingleton<InGameSettingsManager>.IsValid() || spAttackType != SP_ATTACK_TYPE.SOUL)
		{
			return;
		}
		switch (attackMode)
		{
		case ATTACK_MODE.ONE_HAND_SWORD:
		{
			InGameSettingsManager.Player.OneHandSwordActionInfo ohsActionInfo = playerParameter.ohsActionInfo;
			float num2 = baseValue * (isJust ? ohsActionInfo.Soul_JustTapGaugeRate : 1f) * (isBoostMode ? ohsActionInfo.Soul_BoostModeGaugeRate : 1f);
			if (!isBoostMode)
			{
				float num3 = 1f + buffParam.GetGaugeIncreaseRate(spAttackType);
				num2 *= num3;
			}
			spActionGauge[weaponIndex] += CalcWaveMatchSpGauge(num2);
			if (spActionGauge[weaponIndex] >= 1000f)
			{
				spActionGauge[weaponIndex] = 1000f;
				if (!isBoostMode)
				{
					StartBoostMode();
				}
			}
			break;
		}
		case ATTACK_MODE.TWO_HAND_SWORD:
		{
			InGameSettingsManager.Player.TwoHandSwordActionInfo twoHandSwordActionInfo = playerParameter.twoHandSwordActionInfo;
			float num4 = baseValue * (isJust ? twoHandSwordActionInfo.soulJustTapGaugeRate : 1f) * (isBoostMode ? twoHandSwordActionInfo.soulBoostModeGaugeRate : 1f);
			if (!isBoostMode)
			{
				float num5 = 1f + buffParam.GetGaugeIncreaseRate(spAttackType);
				num4 *= num5;
			}
			spActionGauge[weaponIndex] += CalcWaveMatchSpGauge(num4);
			if (spActionGauge[weaponIndex] >= 1000f)
			{
				spActionGauge[weaponIndex] = 1000f;
				if (!isBoostMode)
				{
					StartBoostMode();
				}
			}
			break;
		}
		case ATTACK_MODE.SPEAR:
		{
			InGameSettingsManager.Player.SpearActionInfo spearActionInfo = playerParameter.spearActionInfo;
			float num = baseValue * (isJust ? spearActionInfo.Soul_JustTapGaugeRate : 1f) * (isBoostMode ? spearActionInfo.Soul_BoostModeGaugeRate : 1f);
			if (!isBoostMode)
			{
				num *= 1f + buffParam.GetGaugeIncreaseRate(SP_ATTACK_TYPE.SOUL);
			}
			spActionGauge[weaponIndex] += CalcWaveMatchSpGauge(num);
			if (spActionGauge[weaponIndex] >= 1000f)
			{
				spActionGauge[weaponIndex] = 1000f;
				if (!isBoostMode)
				{
					StartBoostMode();
				}
			}
			break;
		}
		}
	}

	public float CalcWaveMatchSpGauge(float value)
	{
		if (!QuestManager.IsValidInGameWaveMatch() || !MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			return value;
		}
		if (CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.BURST))
		{
			return value;
		}
		InGameSettingsManager.WaveMatchParam waveMatchParam = MonoBehaviourSingleton<InGameSettingsManager>.I.GetWaveMatchParam();
		switch (waveMatchParam.spGaugeType)
		{
		case InGameSettingsManager.WaveMatchParam.eGaugeType.Zero:
			return 0f;
		case InGameSettingsManager.WaveMatchParam.eGaugeType.Rate:
			return value * waveMatchParam.spGaugeValue;
		case InGameSettingsManager.WaveMatchParam.eGaugeType.Constant:
			return waveMatchParam.spGaugeValue;
		default:
			return value;
		}
	}

	public void CountHitAttack()
	{
		attackHitCount++;
	}

	public void UpdateBoostHitCount()
	{
		if (!isBoostMode)
		{
			return;
		}
		switch (attackMode)
		{
		case ATTACK_MODE.PAIR_SWORDS:
		{
			if (spAttackType != SP_ATTACK_TYPE.HEAT)
			{
				break;
			}
			boostModeDamageUpHitCount++;
			if (boostModeDamageUpHitCount < MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.boostDamageUpLevelUpHitCount)
			{
				break;
			}
			int boostDamageUpLevelMax = MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.boostDamageUpLevelMax;
			if (MonoBehaviourSingleton<EffectManager>.IsValid())
			{
				if (boostModeDamageUpLevel < boostDamageUpLevelMax)
				{
					EffectManager.OneShot("ef_btl_wsk_twinsword_01_04", _position, Quaternion.identity);
				}
				if (boostModeDamageUpLevel == boostDamageUpLevelMax - 1)
				{
					Transform effect = EffectManager.GetEffect("ef_btl_wsk_twinsword_01_05");
					effect.position = _position;
					pairSwordsBoostModeAuraEffectList.Add(effect);
				}
			}
			boostModeDamageUpHitCount = 0;
			boostModeDamageUpLevel++;
			if (boostModeDamageUpLevel >= boostDamageUpLevelMax)
			{
				boostModeDamageUpLevel = boostDamageUpLevelMax;
			}
			break;
		}
		case ATTACK_MODE.TWO_HAND_SWORD:
			if (spAttackType == SP_ATTACK_TYPE.SOUL && thsCtrl != null)
			{
				thsCtrl.SetTwoHandSwordBoostAttackSpeed(thsCtrl.TwoHandSwordBoostAttackSpeed + playerParameter.twoHandSwordActionInfo.soulBoostAddAttackSpeed);
			}
			break;
		}
	}

	public bool IsSpActionGaugeFullCharged()
	{
		if (!IsValidSpActionGauge())
		{
			return false;
		}
		return CurrentWeaponSpActionGauge >= CurrentWeaponSpActionGaugeMax;
	}

	public bool IsSpActionGaugeHalfCharged()
	{
		if (!IsValidSpActionGauge())
		{
			return false;
		}
		switch (attackMode)
		{
		case ATTACK_MODE.ONE_HAND_SWORD:
		case ATTACK_MODE.TWO_HAND_SWORD:
		case ATTACK_MODE.SPEAR:
		case ATTACK_MODE.ARROW:
			return IsSpActionGaugeFullCharged();
		case ATTACK_MODE.PAIR_SWORDS:
			switch (spAttackType)
			{
			case SP_ATTACK_TYPE.HEAT:
				return CurrentWeaponSpActionGauge >= CurrentWeaponSpActionGaugeMax * 0.5f;
			case SP_ATTACK_TYPE.SOUL:
			case SP_ATTACK_TYPE.BURST:
			case SP_ATTACK_TYPE.ORACLE:
				return IsSpActionGaugeFullCharged();
			}
			break;
		}
		return false;
	}

	public bool IsSpActionGaugeEmpty()
	{
		if (!IsValidSpActionGauge())
		{
			return true;
		}
		return CurrentWeaponSpActionGauge == 0f;
	}

	private void ResetSpActionGauge()
	{
		for (int i = 0; i < spActionGauge.Length; i++)
		{
			spActionGauge[i] = 0f;
		}
	}

	private void ResetBoostModeAtkLevelUpAndHitCount()
	{
		boostModeDamageUpLevel = 0;
		boostModeDamageUpHitCount = 0;
		if (thsCtrl != null)
		{
			thsCtrl.SetTwoHandSwordBoostAttackSpeed(playerParameter.twoHandSwordActionInfo.soulBoostMinAttackSpeed);
		}
	}

	public void CheckBurstPairSwordBoost()
	{
		if (!isBoostMode && !(spActionGauge[weaponIndex] < 1000f))
		{
			StartBoostMode();
		}
	}

	protected virtual bool StartBoostMode()
	{
		if (isBoostMode)
		{
			return false;
		}
		switch (attackMode)
		{
		case ATTACK_MODE.ONE_HAND_SWORD:
			if (spAttackType == SP_ATTACK_TYPE.HEAT)
			{
				if (ohsMaxChargeEffect == null)
				{
					ohsMaxChargeEffect = EffectManager.GetEffect("ef_btl_wsk_sword_01_04", FindNode("R_Wep"));
				}
				if (playerSender != null)
				{
					playerSender.OnSyncSoulBoost(isBoost: true);
				}
			}
			else if (spAttackType == SP_ATTACK_TYPE.SOUL)
			{
				SoundManager.PlayOneShotSE(playerParameter.twoHandSwordActionInfo.soulBoostSeId, this, FindNode(""));
				Transform effect4 = EffectManager.GetEffect("ef_btl_wsk2_longsword_02_01");
				if (effect4 != null)
				{
					effect4.position = _position;
				}
				if ((object)twoHandSwordsBoostLoopEffect == null)
				{
					twoHandSwordsBoostLoopEffect = EffectManager.GetEffect("ef_btl_wsk2_longsword_03_01", FindNode("Root"));
				}
				StartWaitingPacket(WAITING_PACKET.PLAYER_SOUL_BOOST, keep_sync: false, playerParameter.twoHandSwordActionInfo.soulBoostWaitPacketSec);
				if (playerSender != null)
				{
					playerSender.OnSyncSoulBoost(isBoost: true);
				}
			}
			else if (spAttackType == SP_ATTACK_TYPE.BURST)
			{
				if ((object)twoHandSwordsBoostLoopEffect == null)
				{
					twoHandSwordsBoostLoopEffect = EffectManager.GetEffect("ef_btl_wsk3_sword_aura_01", FindNode("Root"));
				}
				if (playerSender != null)
				{
					playerSender.OnSyncSoulBoost(isBoost: true);
				}
			}
			else
			{
				if (spAttackType != SP_ATTACK_TYPE.ORACLE)
				{
					return false;
				}
				ohsCtrl.OnStartOracleBoost();
				Transform effect5 = EffectManager.GetEffect($"ef_btl_wsk4_sword_02_{GetCurrentWeaponElement():D2}");
				if (effect5 != null)
				{
					effect5.position = _position;
				}
				if (playerSender != null)
				{
					playerSender.OnSyncSoulBoost(isBoost: true);
				}
			}
			break;
		case ATTACK_MODE.TWO_HAND_SWORD:
		{
			if (spAttackType != SP_ATTACK_TYPE.SOUL)
			{
				return false;
			}
			SoundManager.PlayOneShotSE(playerParameter.twoHandSwordActionInfo.soulBoostSeId, this, FindNode(""));
			Transform effect6 = EffectManager.GetEffect("ef_btl_wsk2_longsword_02_01");
			if (effect6 != null)
			{
				effect6.position = _position;
			}
			if ((object)twoHandSwordsBoostLoopEffect == null)
			{
				twoHandSwordsBoostLoopEffect = EffectManager.GetEffect("ef_btl_wsk2_longsword_03_01", FindNode("Root"));
			}
			if (thsCtrl != null)
			{
				if (IsCoopNone() || IsOriginal())
				{
					thsCtrl.SetTwoHandSwordBoostAttackSpeed(playerParameter.twoHandSwordActionInfo.soulBoostMinAttackSpeed);
				}
				else
				{
					thsCtrl.SetTwoHandSwordBoostAttackSpeed(playerParameter.twoHandSwordActionInfo.soulBoostMaxAttackSpeed);
				}
			}
			StartWaitingPacket(WAITING_PACKET.PLAYER_SOUL_BOOST, keep_sync: false, playerParameter.twoHandSwordActionInfo.soulBoostWaitPacketSec);
			if (playerSender != null)
			{
				playerSender.OnSyncSoulBoost(isBoost: true);
			}
			break;
		}
		case ATTACK_MODE.SPEAR:
			if (spAttackType == SP_ATTACK_TYPE.SOUL)
			{
				SoundManager.PlayOneShotSE(playerParameter.twoHandSwordActionInfo.soulBoostSeId, this, FindNode(""));
				Transform effect3 = EffectManager.GetEffect("ef_btl_wsk2_longsword_02_01");
				if (effect3 != null)
				{
					effect3.position = _position;
				}
				if (twoHandSwordsBoostLoopEffect == null)
				{
					twoHandSwordsBoostLoopEffect = EffectManager.GetEffect("ef_btl_wsk2_longsword_03_01", FindNode("Root"));
				}
				StartWaitingPacket(WAITING_PACKET.PLAYER_SOUL_BOOST, keep_sync: false, playerParameter.twoHandSwordActionInfo.soulBoostWaitPacketSec);
			}
			else if (spAttackType == SP_ATTACK_TYPE.ORACLE)
			{
				return false;
			}
			if (playerSender != null)
			{
				playerSender.OnSyncSoulBoost(isBoost: true);
			}
			break;
		case ATTACK_MODE.PAIR_SWORDS:
			switch (spAttackType)
			{
			case SP_ATTACK_TYPE.HEAT:
				boostModeDamageUpLevel = 1;
				break;
			case SP_ATTACK_TYPE.SOUL:
			{
				SoundManager.PlayOneShotSE(playerParameter.twoHandSwordActionInfo.soulBoostSeId, this, FindNode(""));
				Transform effect2 = EffectManager.GetEffect("ef_btl_wsk2_longsword_02_01");
				if (effect2 != null)
				{
					effect2.position = _position;
				}
				if ((object)twoHandSwordsBoostLoopEffect == null)
				{
					twoHandSwordsBoostLoopEffect = EffectManager.GetEffect("ef_btl_wsk2_longsword_03_01", FindNode("Root"));
				}
				StartWaitingPacket(WAITING_PACKET.PLAYER_SOUL_BOOST, keep_sync: false, playerParameter.twoHandSwordActionInfo.soulBoostWaitPacketSec);
				if (playerSender != null)
				{
					playerSender.OnSyncSoulBoost(isBoost: true);
				}
				break;
			}
			case SP_ATTACK_TYPE.BURST:
				SoundManager.PlayOneShotSE(10000051, this, FindNode(""));
				if ((object)twoHandSwordsBoostLoopEffect == null)
				{
					twoHandSwordsBoostLoopEffect = EffectManager.GetEffect("ef_btl_wsk3_sword_aura_01", FindNode("Root"));
				}
				if (playerSender != null)
				{
					playerSender.OnSyncSoulBoost(isBoost: true);
				}
				break;
			}
			break;
		case ATTACK_MODE.ARROW:
			switch (spAttackType)
			{
			case SP_ATTACK_TYPE.SOUL:
			{
				SoundManager.PlayOneShotSE(playerParameter.twoHandSwordActionInfo.soulBoostSeId, this, FindNode(""));
				SoundManager.PlayOneShotUISE(40000359);
				Transform effect = EffectManager.GetEffect("ef_btl_wsk2_longsword_02_01");
				if (effect != null)
				{
					effect.position = _position;
				}
				if ((object)twoHandSwordsBoostLoopEffect == null)
				{
					twoHandSwordsBoostLoopEffect = EffectManager.GetEffect("ef_btl_wsk2_longsword_03_01", FindNode("Root"));
				}
				float num = 1f + GetSpGaugeDecreasingRate();
				float add_margin_time = 1000f / (playerParameter.arrowActionInfo.soulBoostGaugeDecreasePerSecond * num);
				StartWaitingPacket(WAITING_PACKET.PLAYER_SOUL_BOOST, keep_sync: false, add_margin_time);
				if (playerSender != null)
				{
					playerSender.OnSyncSoulBoost(isBoost: true);
				}
				break;
			}
			case SP_ATTACK_TYPE.BURST:
				SoundManager.PlayOneShotSE(playerParameter.arrowActionInfo.burstBoostModeSEId, this, FindNode(""));
				if ((object)twoHandSwordsBoostLoopEffect == null)
				{
					twoHandSwordsBoostLoopEffect = EffectManager.GetEffect("ef_btl_wsk3_sword_aura_01", FindNode("Root"));
				}
				if (playerSender != null)
				{
					playerSender.OnSyncSoulBoost(isBoost: true);
				}
				break;
			}
			break;
		}
		isBoostMode = true;
		return true;
	}

	public void FinishBoostMode()
	{
		if (!isBoostMode)
		{
			return;
		}
		switch (attackMode)
		{
		case ATTACK_MODE.ONE_HAND_SWORD:
			if (spAttackType == SP_ATTACK_TYPE.HEAT)
			{
				ReleaseEffect(ref ohsMaxChargeEffect);
			}
			else if (spAttackType == SP_ATTACK_TYPE.SOUL || spAttackType == SP_ATTACK_TYPE.BURST)
			{
				ReleaseEffect(ref twoHandSwordsBoostLoopEffect);
				EndWaitingPacket(WAITING_PACKET.PLAYER_SOUL_BOOST);
			}
			else if (spAttackType == SP_ATTACK_TYPE.ORACLE)
			{
				ohsCtrl.OnEndOracleBoost();
			}
			if (playerSender != null)
			{
				playerSender.OnSyncSoulBoost(isBoost: false);
			}
			break;
		case ATTACK_MODE.TWO_HAND_SWORD:
			if (spAttackType != SP_ATTACK_TYPE.SOUL)
			{
				return;
			}
			ReleaseEffect(ref twoHandSwordsBoostLoopEffect);
			EndWaitingPacket(WAITING_PACKET.PLAYER_SOUL_BOOST);
			if (playerSender != null)
			{
				playerSender.OnSyncSoulBoost(isBoost: false);
			}
			break;
		case ATTACK_MODE.SPEAR:
			if (!CheckSpAttackType(SP_ATTACK_TYPE.SOUL) && !CheckSpAttackType(SP_ATTACK_TYPE.ORACLE))
			{
				return;
			}
			ReleaseEffect(ref twoHandSwordsBoostLoopEffect);
			EndWaitingPacket(WAITING_PACKET.PLAYER_SOUL_BOOST);
			if (playerSender != null)
			{
				playerSender.OnSyncSoulBoost(isBoost: false);
			}
			break;
		case ATTACK_MODE.PAIR_SWORDS:
			switch (spAttackType)
			{
			case SP_ATTACK_TYPE.HEAT:
				if (MonoBehaviourSingleton<EffectManager>.IsValid())
				{
					if (!pairSwordsBoostModeTrailEffectList.IsNullOrEmpty())
					{
						for (int i = 0; i < pairSwordsBoostModeTrailEffectList.Count; i++)
						{
							EffectManager.ReleaseEffect(pairSwordsBoostModeTrailEffectList[i].gameObject);
							pairSwordsBoostModeTrailEffectList[i] = null;
						}
						pairSwordsBoostModeTrailEffectList.Clear();
					}
					if (!pairSwordsBoostModeAuraEffectList.IsNullOrEmpty())
					{
						for (int j = 0; j < pairSwordsBoostModeAuraEffectList.Count; j++)
						{
							EffectManager.ReleaseEffect(pairSwordsBoostModeAuraEffectList[j].gameObject);
							pairSwordsBoostModeAuraEffectList[j] = null;
						}
						pairSwordsBoostModeAuraEffectList.Clear();
					}
				}
				if (playerSender != null)
				{
					playerSender.OnSyncSpActionGauge();
				}
				break;
			case SP_ATTACK_TYPE.SOUL:
				ReleaseEffect(ref twoHandSwordsBoostLoopEffect);
				EndWaitingPacket(WAITING_PACKET.PLAYER_SOUL_BOOST);
				if (playerSender != null)
				{
					playerSender.OnSyncSoulBoost(isBoost: false);
				}
				break;
			case SP_ATTACK_TYPE.BURST:
				ReleaseEffect(ref twoHandSwordsBoostLoopEffect);
				EndWaitingPacket(WAITING_PACKET.PLAYER_SOUL_BOOST);
				if (playerSender != null)
				{
					playerSender.OnSyncSoulBoost(isBoost: false);
				}
				break;
			}
			break;
		case ATTACK_MODE.ARROW:
			switch (spAttackType)
			{
			case SP_ATTACK_TYPE.SOUL:
				ReleaseEffect(ref twoHandSwordsBoostLoopEffect);
				if (MonoBehaviourSingleton<TargetMarkerManager>.IsValid())
				{
					MonoBehaviourSingleton<TargetMarkerManager>.I.EndMultiLockBoost();
				}
				EndWaitingPacket(WAITING_PACKET.PLAYER_SOUL_BOOST);
				if (playerSender != null)
				{
					playerSender.OnSyncSoulBoost(isBoost: false);
				}
				break;
			case SP_ATTACK_TYPE.BURST:
				ReleaseEffect(ref twoHandSwordsBoostLoopEffect);
				EndWaitingPacket(WAITING_PACKET.PLAYER_SOUL_BOOST);
				if (playerSender != null)
				{
					playerSender.OnSyncSoulBoost(isBoost: false);
				}
				break;
			}
			break;
		}
		isBoostMode = false;
		ResetBoostModeAtkLevelUpAndHitCount();
	}

	private void ResetBoostPowerUpTriggerDamage()
	{
		if (isBoostMode)
		{
			ATTACK_MODE attackMode = this.attackMode;
			if (attackMode == ATTACK_MODE.TWO_HAND_SWORD && spAttackType == SP_ATTACK_TYPE.SOUL && playerParameter.twoHandSwordActionInfo.isSoulBoostResetTriggerDamage && (IsCoopNone() || IsOriginal()))
			{
				ResetBoostModeAtkLevelUpAndHitCount();
			}
		}
	}

	public void OnSoulBoost(bool isBoost)
	{
		if (isBoost)
		{
			StartBoostMode();
		}
		else
		{
			FinishBoostMode();
		}
	}

	public float CalcPairSwordsBoostModeDamageUpRate()
	{
		if (attackMode != ATTACK_MODE.PAIR_SWORDS)
		{
			return 1f;
		}
		if (spAttackType != SP_ATTACK_TYPE.HEAT)
		{
			return 1f;
		}
		if (!isBoostMode)
		{
			return 1f;
		}
		if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			return 1f;
		}
		InGameSettingsManager.Player.PairSwordsActionInfo pairSwordsActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo;
		float num = 1f + (float)boostModeDamageUpLevel * pairSwordsActionInfo.boostDamageUpRatePerLevel;
		float num2 = 1f + (float)pairSwordsActionInfo.boostDamageUpLevelMax * pairSwordsActionInfo.boostDamageUpRatePerLevel;
		if (num > num2)
		{
			num = num2;
		}
		return num;
	}

	public bool GetPairSwordsRadiusCustomRate(ref float rRate)
	{
		if (!evolveCtrl.IsExecSphinx())
		{
			return false;
		}
		rRate = evolveCtrl.GetSphinxRangeUpRate();
		return true;
	}

	public bool GetSphinxElementDamageUpRate(AttackHitInfo info, ref float rRate)
	{
		rRate = 1f;
		if (!evolveCtrl.IsExecSphinx())
		{
			return false;
		}
		if (!info.toEnemy.isSpecialAttack)
		{
			return false;
		}
		rRate = evolveCtrl.GetSphinxElementDamageUpRate();
		return true;
	}

	public bool ActSpAttackContinue()
	{
		if (!isActSpecialAction)
		{
			return false;
		}
		switch (attackMode)
		{
		case ATTACK_MODE.ONE_HAND_SWORD:
			if (IsAbleToFlickAttackBySoulOneHandSword())
			{
				ActAttack(snatchCtrl.GetAttackId(flickDirection));
				break;
			}
			return false;
		case ATTACK_MODE.TWO_HAND_SWORD:
			if (!ActTwoHandSwordSpAttackContinue())
			{
				return false;
			}
			break;
		default:
			return false;
		}
		if (playerSender != null)
		{
			playerSender.OnActSpAttackContinue();
		}
		return true;
	}

	public bool IsSoulOneHandSwordBoostMode()
	{
		if (!CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
		{
			return false;
		}
		if (!isBoostMode)
		{
			return false;
		}
		return true;
	}

	public bool IsAbleToFlickAttackBySoulOneHandSword()
	{
		if (!CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
		{
			return false;
		}
		if (!enableSpAttackContinue)
		{
			return false;
		}
		return true;
	}

	private bool ActTwoHandSwordSpAttackContinue()
	{
		if (IsOracleTwoHandSword() && thsCtrl.oracleCtrl.IsHorizontalAttack)
		{
			return true;
		}
		if (!CheckAttackModeAndSpType(ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.HEAT))
		{
			return false;
		}
		if (!isActSpecialAction)
		{
			return false;
		}
		if (!IsFullCharge())
		{
			return false;
		}
		if (!isHitSpAttack)
		{
			return false;
		}
		if (thsCtrl != null && thsCtrl.IsTwoHandSwordSpAttackContinueTimeOut(hitSpAttackContinueTimer))
		{
			return false;
		}
		isHitSpAttack = false;
		ActAttack(89, send_packet: false);
		return true;
	}

	protected override void UpdateAction()
	{
		base.UpdateAction();
		switch (base.actionID)
		{
		case ACTION_ID.IDLE:
		{
			if (prayTargetInfos.Count <= 0)
			{
				break;
			}
			int num2 = 0;
			int count = prayTargetInfos.Count;
			while (true)
			{
				if (num2 < count)
				{
					Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(prayTargetInfos[num2].targetId) as Player;
					if (player != null && (player.isDead || player.IsStone()))
					{
						break;
					}
					num2++;
					continue;
				}
				return;
			}
			ActPrayer();
			break;
		}
		case (ACTION_ID)18:
		case (ACTION_ID)47:
			if (isStunnedLoop)
			{
				UpdateStunnedEffect();
			}
			break;
		case (ACTION_ID)24:
			if (!isStopCounter)
			{
				if (IsPrayed() && deadStopTime > 0f)
				{
					float num3 = Time.deltaTime * (IsBoostByType(BOOST_PRAY_TYPE.GUARD_ONE_HAND_SWORD_NORMAL) ? playerParameter.ohsActionInfo.Normal_PrayBoostRate : 1f);
					switch (prayerIds.Count)
					{
					case 2:
						num3 *= 1.2f;
						break;
					case 3:
						num3 *= 1.4f;
						break;
					}
					num3 *= (IsBoostByType(BOOST_PRAY_TYPE.IN_BARRIER) ? playerParameter.rescueSpeedRateInBarrier : 1f);
					prayerTime += num3;
				}
				else if (!IsPrayed())
				{
					prayerTime -= Time.deltaTime;
					if (prayerTime < 0f)
					{
						prayerTime = 0f;
					}
				}
			}
			if (rescueTime <= 0f && deadStartTime >= 0f)
			{
				UpdateRevivalRangeEffect();
				if ((MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameWaveMatch(isOnlyEvent: true)) && continueTime > 0f)
				{
					continueTime = 0f;
					OnEndContinueTimeEnd();
				}
				else if (continueTime > 0f && !isProgressStop())
				{
					continueTime -= Time.deltaTime;
					if (continueTime <= 0f)
					{
						continueTime = 0f;
						OnEndContinueTimeEnd();
					}
				}
			}
			if ((IsCoopNone() || IsOriginal()) && IsPrayed() && base.isDead && isRevivalEnabled)
			{
				ActDeadStandup(base.hpMax, eContinueType.RESCUE);
			}
			break;
		case (ACTION_ID)26:
		{
			bool flag = true;
			int i = 0;
			for (int count2 = prayTargetInfos.Count; i < count2; i++)
			{
				Player player2 = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(prayTargetInfos[i].targetId) as Player;
				switch (prayTargetInfos[i].reason)
				{
				case PRAY_REASON.DEAD:
					if (player2 != null && player2.isDead)
					{
						flag = false;
					}
					break;
				case PRAY_REASON.STONE:
					if (player2 != null && player2.IsStone())
					{
						flag = false;
					}
					break;
				}
				if (!flag)
				{
					break;
				}
			}
			if (flag)
			{
				ActIdle();
			}
			break;
		}
		case (ACTION_ID)27:
			if (isChangingWeapon && !base.isLoading && changeWeaponStartTime >= 0f && Time.time - changeWeaponStartTime >= playerParameter.changeWeaponMinTime)
			{
				ActIdle();
			}
			break;
		case (ACTION_ID)43:
			if (!isStopCounter)
			{
				if (IsPrayed() && stoneStopTime > 0f)
				{
					float num = Time.deltaTime * (IsBoostByType(BOOST_PRAY_TYPE.GUARD_ONE_HAND_SWORD_NORMAL) ? playerParameter.ohsActionInfo.Normal_PrayBoostRate : 1f);
					switch (prayerIds.Count)
					{
					case 2:
						num *= 1.2f;
						break;
					case 3:
						num *= 1.4f;
						break;
					}
					num *= (IsBoostByType(BOOST_PRAY_TYPE.IN_BARRIER) ? playerParameter.rescueSpeedRateInBarrier : 1f);
					prayerTime += num;
				}
				else if (!IsPrayed())
				{
					prayerTime -= Time.deltaTime;
					if (prayerTime < 0f)
					{
						prayerTime = 0f;
					}
				}
			}
			if (stoneRescueTime <= 0f && stoneStartTime >= 0f)
			{
				UpdateRevivalRangeEffect();
				ActStoneEnd(stoneRescueTime);
			}
			if ((IsCoopNone() || IsOriginal()) && IsPrayed() && IsStone() && isRevivalEnabled)
			{
				ActStoneEnd(stoneRescueTime);
			}
			break;
		}
	}

	protected override void OnPlayingEndMotion()
	{
		switch (base.actionID)
		{
		case ACTION_ID.DEAD:
			if (MonoBehaviourSingleton<QuestManager>.IsValid() && MonoBehaviourSingleton<QuestManager>.I.IsForceDefeatQuest())
			{
				PlayMotion(125);
				OnEndContinueTimeEnd();
			}
			else
			{
				ActDeadLoop();
			}
			return;
		case (ACTION_ID)22:
			if (isSkillCastState)
			{
				isSkillCastState = false;
				SkillInfo.SkillParam actSkillParam = skillInfo.actSkillParam;
				PlayMotion(actSkillParam.tableData.actStateName);
				return;
			}
			break;
		}
		base.OnPlayingEndMotion();
	}

	protected override void EndAction()
	{
		if (!base.isInitialized)
		{
			return;
		}
		ACTION_ID actionID = base.actionID;
		MOVE_TYPE moveType = base.moveType;
		_ = isPlayingEndMotion;
		base.EndAction();
		switch (actionID)
		{
		case ACTION_ID.IDLE:
			if (loader != null)
			{
				loader.eyeBlink = false;
			}
			break;
		case ACTION_ID.MOVE:
		{
			bool flag = false;
			if (MonoBehaviourSingleton<InGameProgress>.IsValid() && (MonoBehaviourSingleton<InGameProgress>.I.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_VICTORY || MonoBehaviourSingleton<InGameProgress>.I.progressEndType == InGameProgress.PROGRESS_END_TYPE.QUEST_SERIES_INTERVAL || MonoBehaviourSingleton<InGameProgress>.I.progressEndType == InGameProgress.PROGRESS_END_TYPE.RUSH_INTERVAL || MonoBehaviourSingleton<InGameProgress>.I.progressEndType == InGameProgress.PROGRESS_END_TYPE.ARENA_INTERVAL))
			{
				flag = true;
			}
			if (!flag && moveType == MOVE_TYPE.SYNC_VELOCITY)
			{
				PlayerLoader.SetLayerWithChildren_SecondaryNoChange(base._transform, 8);
			}
			break;
		}
		case (ACTION_ID)14:
			stumbleEndTime = 0f;
			break;
		case (ACTION_ID)15:
			shakeEndTime = 0f;
			break;
		case (ACTION_ID)16:
		case (ACTION_ID)17:
		case (ACTION_ID)18:
		case (ACTION_ID)47:
			base._rigidbody.constraints = (base._rigidbody.constraints | RigidbodyConstraints.FreezePositionY);
			ResetIgnoreColliders();
			if (actionID == (ACTION_ID)18 || actionID == (ACTION_ID)47)
			{
				isStunnedLoop = false;
				stunnedEndTime = 0f;
				stunnedTime = 0f;
				stunnedReduceEnableTime = 0f;
				UpdateStunnedEffect();
			}
			break;
		case (ACTION_ID)28:
			if (!isAppliedGather && (IsCoopNone() || IsOriginal()))
			{
				ApplyGather();
			}
			break;
		case (ACTION_ID)22:
			if (skillRangeEffect != null)
			{
				EffectManager.ReleaseEffect(skillRangeEffect.gameObject);
				skillRangeEffect = null;
			}
			isUsingSecondGradeSkill = false;
			skillInfo.ResetSecondGradeFlags();
			break;
		case (ACTION_ID)23:
			if (uiPlayerStatusGizmo != null)
			{
				uiPlayerStatusGizmo.SetVisible(visible: true);
			}
			SetHitOffTimer(HIT_OFF_FLAG.BATTLE_START, playerParameter.battleStartHitOffTime);
			break;
		case (ACTION_ID)24:
			ResetIgnoreColliders();
			prayerIds.Clear();
			boostPrayTargetInfoList.Clear();
			boostPrayedInfoList.Clear();
			_rescueTime = 0f;
			continueTime = 0f;
			deadStartTime = -1f;
			deadStopTime = -1f;
			if (revivalRangEffect != null)
			{
				EffectManager.ReleaseEffect(revivalRangEffect);
				revivalRangEffect = null;
			}
			break;
		case (ACTION_ID)25:
			SetHitOffTimer(HIT_OFF_FLAG.DEAD_STANDUP, playerParameter.deadStandupHitOffTime);
			break;
		case (ACTION_ID)30:
			PostRestraint();
			break;
		case (ACTION_ID)43:
			PostStone();
			prayerIds.Clear();
			boostPrayTargetInfoList.Clear();
			boostPrayedInfoList.Clear();
			_stoneRescueTime = 0f;
			stoneStartTime = -1f;
			stoneStopTime = -1f;
			if (revivalRangEffect != null)
			{
				EffectManager.ReleaseEffect(revivalRangEffect);
				revivalRangEffect = null;
			}
			break;
		}
		EndWaitingPacket(WAITING_PACKET.PLAYER_CHARGE_RELEASE);
		EndWaitingPacket(WAITING_PACKET.PLAYER_APPLY_CHANGE_WEAPON);
		if (loader.shadow != null && !loader.shadow.gameObject.activeSelf && shadow == null)
		{
			loader.shadow.gameObject.SetActive(value: true);
		}
		if (!isArrowAimKeep)
		{
			if (isArrowAimBossMode)
			{
				SetArrowAimBossMode(enable: false);
			}
			if (isArrowAimLesserMode)
			{
				SetArrowAimLesserMode(enable: false);
			}
		}
		isArrowAimKeep = false;
		isArrowAimEnd = false;
		CancelCannonMode();
		skillInfo.skillIndex = -1;
		enableInputCombo = false;
		controllerInputCombo = false;
		inputComboID = -1;
		inputComboMotionState = "";
		enableComboTrans = false;
		inputComboFlag = false;
		enableInputCharge = false;
		enableFlickAction = false;
		enableInputNextTrigger = false;
		enableNextTriggerTrans = false;
		inputNextTriggerFlag = false;
		inputNextTriggerIndex = 0;
		isCountLongTouch = false;
		countLongTouchSec = 0f;
		inputChargeAutoRelease = false;
		inputChargeMaxTiming = false;
		inputChargeTimeMax = 0f;
		inputChargeTimeOffset = 0f;
		inputChargeTimeCounter = 0f;
		isInputChargeExistOffset = false;
		chargeRate = 0f;
		enableInputRotate = false;
		startInputRotate = false;
		inputBlowClearFlag = false;
		isActSkillAction = false;
		isSkillCastState = false;
		isSkillCastLoop = false;
		skillCastLoopStartTime = -1f;
		skillCastLoopTime = 0f;
		skillCastLoopTrigger = null;
		isAppliedSkillParam = false;
		isActSpecialAction = false;
		isActOneHandSwordCounter = false;
		hitSpearSpecialAction = false;
		lockedSpearCancelAction = false;
		actSpecialActionTimer = 0f;
		actRushLoopTimer = 0f;
		isCanRushRelease = false;
		isChargeExRush = false;
		isLoopingRush = false;
		hitSpearSpActionTimer = 0f;
		isSpearHundred = false;
		spearHundredSecFromLastTap = 0f;
		spearHundredSecFromStart = 0f;
		isArrowAimable = false;
		enableCancelToAvoid = false;
		enableCancelToMove = false;
		enableCancelToAttack = false;
		enableCancelToSkill = false;
		enableCancelToSpecialAction = false;
		enableCancelToEvolveSpecialAction = false;
		enableCancelToCarryPut = false;
		enableCounterAttack = false;
		enableSpAttackContinue = false;
		enableAnimSeedRate = false;
		prayerTime = 0f;
		isGuardWalk = false;
		isCarryWalk = false;
		enableSuperArmor = false;
		shotArrowCount = 0;
		changeWeaponItem = null;
		changeWeaponIndex = -1;
		isChangingWeapon = false;
		changeWeaponStartTime = -1f;
		targetGatherPoint = null;
		isAppliedGather = false;
		isHitSpAttack = false;
		isLockedSpAttackContinue = false;
		hitSpAttackContinueTimer = 0f;
		enableSpAttackContinue = false;
		enableAttackNext = false;
		enableWeaponAction = false;
		enableRotateToTargetPoint = false;
		isAnimEventStatusUpDefence = false;
		animEventStatusUpDefenceRate = 1f;
		arrowBulletSpeedUpRate = 0f;
		isChargeExpandAutoRelease = false;
		chargeExpandRate = 0f;
		timeChargeExpandMax = 0f;
		timerChargeExpandOffset = 0f;
		timerChargeExpand = 0f;
		isSpearJumpAim = false;
		jumpActionCounter = 0f;
		useGaugeLevel = 0;
		isAerial = false;
		if (jumpState != 0)
		{
			base.body.transform.localPosition = Vector3.zero;
		}
		jumpFallBodyPosition = Vector3.zero;
		jumpRandingVector = Vector3.zero;
		jumpRaindngBasePos = Vector3.zero;
		jumpRandingBaseBodyY = 0f;
		jumpState = eJumpState.None;
		rainShotState = RAIN_SHOT_STATE.NONE;
		rainShotFallPosition = Vector3.zero;
		rainShotFallRotateY = 0f;
		rainShotLotGroupId = 0;
		targetingGimmickObject = null;
		enabledTeleportAvoid = false;
		enabledRushAvoid = false;
		enabledOraclePairSwordsSP = false;
		actionMoveRotateMaxSpeedRate = 1f;
		extraSpGaugeDecreasingRate = 0f;
		ReleaseEffect(ref twoHandSwordsChargeMaxEffect);
		evolveSpecialActionSec = 0f;
		attackHitCount = 0;
		if (!notEndGuardFlag)
		{
			_EndGuard();
		}
		ReleaseEffect(ref exRushChargeEffect);
		isAbsorbDamageSuperArmor = false;
		isInvincibleDamageSuperArmor = false;
		SetFlickDirection(SelfController.FLICK_DIRECTION.NONE);
		snatchCtrl.Cancel();
		int i = 0;
		for (int count = m_weaponCtrlList.Count; i < count; i++)
		{
			m_weaponCtrlList[i].OnEndAction();
		}
		OnGatherGimmickEnd();
		fishingCtrl.CoopEnd();
		OnQuestGimmickEnd();
		if (cancelInvincible != null)
		{
			StopCoroutine(cancelInvincible);
			cancelInvincible = null;
		}
	}

	protected override void EndRotate()
	{
		enableRotateToTargetPoint = false;
		base.EndRotate();
	}

	public override RuntimeAnimatorController GetAnimCtrl(string ctrl_name)
	{
		if (ctrl_name == null)
		{
			ctrl_name = "BASE";
		}
		LoadObject loadObject = loader.animObjectTable.Get(ctrl_name);
		if (loadObject == null)
		{
			return null;
		}
		return loadObject.loadedObjects[0].obj as RuntimeAnimatorController;
	}

	public override AnimEventData GetAnimEvent(string ctrl_name)
	{
		if (ctrl_name == null)
		{
			ctrl_name = "BASE";
		}
		LoadObject loadObject = loader.animObjectTable.Get(ctrl_name);
		if (loadObject == null)
		{
			return null;
		}
		return loadObject.loadedObjects[1].obj as AnimEventData;
	}

	protected override string GetMotionStateName(int motion_id, string _layerName = "")
	{
		if (motion_id - 115 >= 0 && motion_id - 115 < subMotionStateName.Length)
		{
			Character.stateNameBuilder.Length = 0;
			Character.stateNameBuilder.Append((_layerName == "") ? "Base Layer." : _layerName);
			Character.stateNameBuilder.Append(subMotionStateName[motion_id - 115]);
			return Character.stateNameBuilder.ToString();
		}
		return base.GetMotionStateName(motion_id, _layerName);
	}

	public virtual void Load(PlayerLoadInfo load_info, PlayerLoader.OnCompleteLoad callback = null)
	{
		if (_physics != null)
		{
			UnityEngine.Object.Destroy(_physics.gameObject);
			_physics = null;
		}
		int num = load_info.weaponModelID / 1000;
		ATTACK_MODE aTTACK_MODE = ConvertEquipmentTypeToAttackMode((EQUIPMENT_TYPE)num);
		if (aTTACK_MODE == ATTACK_MODE.NONE)
		{
			aTTACK_MODE = ATTACK_MODE.ONE_HAND_SWORD;
		}
		SetAttackMode(aTTACK_MODE);
		UpdateTwoHandSwordController();
		if (record != null)
		{
			record.playerLoadInfo = load_info;
			record.animID = num;
		}
		loader.StartLoad_GG_Optimize(load_info, 8, num, need_anim_event: true, need_foot_stamp: true, need_shadow: true, enable_light_probes: true, need_action_voice: true, need_high_reso_tex: false, need_res_ref_count: true, IsDiviedLoadAndInstantiate(), ShaderGlobal.GetCharacterShaderType(), callback);
	}

	private void UpdateTwoHandSwordController()
	{
		if (IsBurstTwoHandSword())
		{
			int num = 6;
			if (buffParam != null && buffParam.passive != null)
			{
				num += buffParam.passive.additionalMaxBulletCnt;
			}
			TwoHandSwordController.InitParam param = new TwoHandSwordController.InitParam
			{
				Owner = this,
				BurstInitParam = new TwoHandSwordBurstController.InitParam
				{
					Owner = this,
					ActionInfo = playerParameter.twoHandSwordActionInfo,
					MaxBulletCount = num,
					CurrentRestBullets = thsCtrl.GetAllCurrentRestBulletCount
				}
			};
			thsCtrl.InitAppend(param);
		}
	}

	protected virtual void LoadUniqueEquipment(StageObjectManager.CreatePlayerInfo info, PlayerLoader.OnCompleteLoad callback = null)
	{
		spActionGauge = new float[3];
		spActionGaugeMax = new float[3];
		buffParam.AllBuffEnd(sync: false);
		evolveCtrl.Init(this);
		weaponIndex = -1;
		SetState(info);
		SetNowWeapon(equipWeaponList[0], 0, info.extentionInfo.uniqueEquipmentIndex);
		autoReviveCount = 0;
		isUseInvincibleBuff = false;
		isUseInvincibleBadStatusBuff = false;
		PlayerLoadInfo playerLoadInfo = new PlayerLoadInfo();
		playerLoadInfo.SetEquipWeapon(info.charaInfo.sex, (uint)weaponData.eId);
		playerLoadInfo.Apply(info.charaInfo, need_weapon: false, need_helm: true, need_leg: true, is_priority_visual_equip: true);
		Load(playerLoadInfo, callback);
		InitParameter();
		base.hp = base.hpMax;
	}

	public void LoadWeapon(CharaInfo.EquipItem item, int weapon_index, PlayerLoader.OnCompleteLoad callback = null)
	{
		if (item != null && !(loader == null) && loader.loadInfo != null)
		{
			SetNowWeapon(item, weapon_index, uniqueEquipmentIndex);
			int sex = 0;
			if (createInfo != null && createInfo.charaInfo != null)
			{
				sex = createInfo.charaInfo.sex;
			}
			loader.loadInfo.SetEquipWeapon(sex, (uint)item.eId);
			Load(loader.loadInfo, callback);
			InitParameter();
		}
	}

	public void SetPassiveBuff(BuffParam.BUFFTYPE type, int value)
	{
		BuffParam.PassiveBuff passive = buffParam.passive;
		switch (type)
		{
		case BuffParam.BUFFTYPE.INVINCIBLECOUNT:
		case BuffParam.BUFFTYPE.REGENERATE:
		case BuffParam.BUFFTYPE.POISON:
		case BuffParam.BUFFTYPE.BURNING:
		case BuffParam.BUFFTYPE.DEADLY_POISON:
		case BuffParam.BUFFTYPE.LUNATIC_TEAR:
		case BuffParam.BUFFTYPE.WING:
		case BuffParam.BUFFTYPE.GHOST_FORM:
		case BuffParam.BUFFTYPE.BREAK_GHOST_FORM:
		case BuffParam.BUFFTYPE.ELECTRIC_SHOCK:
		case BuffParam.BUFFTYPE.INK_SPLASH:
		case BuffParam.BUFFTYPE.DAMAGE_UP:
		case BuffParam.BUFFTYPE.SUPER_ARMOR:
		case BuffParam.BUFFTYPE.SHIELD:
		case BuffParam.BUFFTYPE.SHIELD_SUPER_ARMOR:
		case BuffParam.BUFFTYPE.SKILL_CHARGE:
		case BuffParam.BUFFTYPE.SKILL_CHARGE_RATE:
		case BuffParam.BUFFTYPE.SLIDE:
		case BuffParam.BUFFTYPE.SILENCE:
		case BuffParam.BUFFTYPE.SKILL_CHARGE_FIRE:
		case BuffParam.BUFFTYPE.SKILL_CHARGE_WATER:
		case BuffParam.BUFFTYPE.SKILL_CHARGE_THUNDER:
		case BuffParam.BUFFTYPE.SKILL_CHARGE_SOIL:
		case BuffParam.BUFFTYPE.SKILL_CHARGE_LIGHT:
		case BuffParam.BUFFTYPE.SKILL_CHARGE_DARK:
		case BuffParam.BUFFTYPE.REGENERATE_PROPORTION:
		case BuffParam.BUFFTYPE.ABSORB_NORMAL:
		case BuffParam.BUFFTYPE.ABSORB_FIRE:
		case BuffParam.BUFFTYPE.ABSORB_WATER:
		case BuffParam.BUFFTYPE.ABSORB_THUNDER:
		case BuffParam.BUFFTYPE.ABSORB_SOIL:
		case BuffParam.BUFFTYPE.ABSORB_LIGHT:
		case BuffParam.BUFFTYPE.ABSORB_DARK:
		case BuffParam.BUFFTYPE.ABSORB_ALL_ELEMENT:
		case BuffParam.BUFFTYPE.HIT_ABSORB_NORMAL:
		case BuffParam.BUFFTYPE.HIT_ABSORB_FIRE:
		case BuffParam.BUFFTYPE.HIT_ABSORB_WATER:
		case BuffParam.BUFFTYPE.HIT_ABSORB_THUNDER:
		case BuffParam.BUFFTYPE.HIT_ABSORB_SOIL:
		case BuffParam.BUFFTYPE.HIT_ABSORB_LIGHT:
		case BuffParam.BUFFTYPE.HIT_ABSORB_DARK:
		case BuffParam.BUFFTYPE.HIT_ABSORB_ALL:
		case BuffParam.BUFFTYPE.MAD_MODE:
		case BuffParam.BUFFTYPE.AUTO_REVIVE:
		case BuffParam.BUFFTYPE.WARP_BY_AVOID:
		case BuffParam.BUFFTYPE.INVINCIBLE_NORMAL:
		case BuffParam.BUFFTYPE.INVINCIBLE_FIRE:
		case BuffParam.BUFFTYPE.INVINCIBLE_WATER:
		case BuffParam.BUFFTYPE.INVINCIBLE_THUNDER:
		case BuffParam.BUFFTYPE.INVINCIBLE_SOIL:
		case BuffParam.BUFFTYPE.INVINCIBLE_ALL:
		case BuffParam.BUFFTYPE.DAMAGE_UP_NORMAL:
		case BuffParam.BUFFTYPE.DAMAGE_UP_FROM_AVOID:
		case BuffParam.BUFFTYPE.SKILL_CHARGE_WHEN_DAMAGED:
		case BuffParam.BUFFTYPE.INVINCIBLE_LIGHT:
		case BuffParam.BUFFTYPE.INVINCIBLE_DARK:
		case BuffParam.BUFFTYPE.CANT_HEAL_HP:
		case BuffParam.BUFFTYPE.BLIND:
		case BuffParam.BUFFTYPE.INVINCIBLE_BADSTATUS:
		case BuffParam.BUFFTYPE.SLIDE_ICE:
		case BuffParam.BUFFTYPE.LIGHT_RING:
		case BuffParam.BUFFTYPE.BOOST_DAMAGE_UP:
		case BuffParam.BUFFTYPE.SKILL_CHARGE_ABOVE:
		case BuffParam.BUFFTYPE.SKILL_CHARGE_UNDER:
		case BuffParam.BUFFTYPE.SUBSTITUTE:
		case BuffParam.BUFFTYPE.EROSION:
		case BuffParam.BUFFTYPE.STONE:
		case BuffParam.BUFFTYPE.SHIELD_REFLECT:
		case BuffParam.BUFFTYPE.SHIELD_REFLECT_DAMAGE_UP:
		case BuffParam.BUFFTYPE.SOIL_SHOCK:
		case BuffParam.BUFFTYPE.BLEEDING:
		case BuffParam.BUFFTYPE.INVINCIBLE_ALL_ELEMENT:
		case BuffParam.BUFFTYPE.ACID:
		case BuffParam.BUFFTYPE.INVINCIBLE_BUFF_CANCELLATION:
		case BuffParam.BUFFTYPE.INVINCIBLE_BUFF_CANCELLATION_EXPAND:
			break;
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
			passive.atkAllElement += value;
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
			passive.atkAllElement -= value;
			break;
		case BuffParam.BUFFTYPE.ATKUP_RATE_NORMAL:
			passive.atkUpRate.normal += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.ATKUP_RATE_FIRE:
			passive.atkUpRate.fire += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.ATKUP_RATE_WATER:
			passive.atkUpRate.water += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.ATKUP_RATE_THUNDER:
			passive.atkUpRate.thunder += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.ATKUP_RATE_SOIL:
			passive.atkUpRate.soil += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.ATKUP_RATE_LIGHT:
			passive.atkUpRate.light += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.ATKUP_RATE_DARK:
			passive.atkUpRate.dark += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.ATKUP_RATE_ALLELEMENT:
			passive.atkUpRate.AddElementOnly((float)value * 0.01f);
			break;
		case BuffParam.BUFFTYPE.ATKUP_RATE_ALL:
			passive.atkUpRate.AddAll((float)value * 0.01f);
			break;
		case BuffParam.BUFFTYPE.ATKDOWN_RATE_NORMAL:
			passive.atkDownRate.normal += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.ATKDOWN_RATE_FIRE:
			passive.atkDownRate.fire += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.ATKDOWN_RATE_WATER:
			passive.atkDownRate.water += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.ATKDOWN_RATE_THUNDER:
			passive.atkDownRate.thunder += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.ATKDOWN_RATE_SOIL:
			passive.atkDownRate.soil += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.ATKDOWN_RATE_LIGHT:
			passive.atkDownRate.light += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.ATKDOWN_RATE_DARK:
			passive.atkDownRate.dark += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.ATKDOWN_RATE_ALLELEMENT:
			passive.atkDownRate.AddElementOnly((float)value * 0.01f);
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
		{
			for (int l = 1; l < 7; l++)
			{
				passive.defList[l] += value;
			}
			break;
		}
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
		{
			for (int k = 1; k < 7; k++)
			{
				passive.defList[k] -= value;
			}
			break;
		}
		case BuffParam.BUFFTYPE.DEFUP_RATE_NORMAL:
			passive.defUpRate.normal += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.DEFUP_RATE_FIRE:
			passive.defUpRate.fire += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.DEFUP_RATE_WATER:
			passive.defUpRate.water += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.DEFUP_RATE_THUNDER:
			passive.defUpRate.thunder += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.DEFUP_RATE_SOIL:
			passive.defUpRate.soil += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.DEFUP_RATE_LIGHT:
			passive.defUpRate.light += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.DEFUP_RATE_DARK:
			passive.defUpRate.dark += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.DEFUP_RATE_ALLELEMENT:
			passive.defUpRate.AddElementOnly((float)value * 0.01f);
			break;
		case BuffParam.BUFFTYPE.DEFDOWN_RATE_NORMAL:
			passive.defDownRate.normal += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.DEFDOWN_RATE_FIRE:
			passive.defDownRate.fire += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.DEFDOWN_RATE_WATER:
			passive.defDownRate.water += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.DEFDOWN_RATE_THUNDER:
			passive.defDownRate.thunder += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.DEFDOWN_RATE_SOIL:
			passive.defDownRate.soil += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.DEFDOWN_RATE_LIGHT:
			passive.defDownRate.light += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.DEFDOWN_RATE_DARK:
			passive.defDownRate.dark += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.DEFDOWN_RATE_ALLELEMENT:
			passive.defDownRate.AddElementOnly((float)value * 0.01f);
			break;
		case BuffParam.BUFFTYPE.DAMAGE_DOWN:
			passive.damageDown += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.ATTACK_SPEED_UP:
			passive.attackSpeedUp += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN:
			passive.attackSpeedUp -= (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.MOVE_SPEED_UP:
			passive.moveSpeedUp += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.MOVE_SPEED_DOWN:
			passive.moveSpeedUp -= (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.HP_HEAL_SPEEDUP:
			passive.hpHealSpeedUp += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.SKILL_ABSORBUP:
			passive.skillAbsorbUp += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.SKILL_HEAL_SPEEDUP:
			passive.skillHealSpeedUp += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.DISTANCE_UP_IAI:
			passive.distanceRateIai += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.POISON_DAMAGE_DOWN:
			passive.poisonDamageDownRate += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.BURN_DAMAGE_DOWN:
			passive.burnDamageDownRate += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.POISON_GUARD:
			passive.poisonGuardWeight += value;
			break;
		case BuffParam.BUFFTYPE.BURN_GUARD:
			passive.burnGuardWeight += value;
			break;
		case BuffParam.BUFFTYPE.PARALYZE_GUARD:
			passive.paralyzeGuardWeight += value;
			break;
		case BuffParam.BUFFTYPE.SILENCE_GUARD:
			passive.silenceGuardWeight += value;
			break;
		case BuffParam.BUFFTYPE.ATTACK_PARALYZE:
			base.atkBadStatus.paralyze += value;
			break;
		case BuffParam.BUFFTYPE.ATTACK_POISON:
			base.atkBadStatus.poison += value;
			break;
		case BuffParam.BUFFTYPE.ATTACK_FREEZE:
			base.atkBadStatus.freeze += value;
			break;
		case BuffParam.BUFFTYPE.BAD_STATUS_DOWN_RATE_UP:
			passive.badStatusRateUp[2] += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.BAD_STATUS_CONCUSSION_RATE_UP:
			passive.badStatusRateUp[4] += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.HEAL_UP:
			passive.healUP += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.HP_UP:
			passive.hp += value;
			break;
		case BuffParam.BUFFTYPE.HP_DOWN:
			passive.hp -= value;
			break;
		case BuffParam.BUFFTYPE.HPUP_RATE:
			passive.hpUpRate += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.HPDOWN_RATE:
			passive.hpDownRate += (float)value * 0.01f;
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
		{
			for (int j = 0; j < 6; j++)
			{
				passive.tolList[j] += value;
			}
			break;
		}
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
		{
			for (int i = 0; i < 6; i++)
			{
				passive.tolList[i] -= value;
			}
			break;
		}
		case BuffParam.BUFFTYPE.TOLUP_RATE_FIRE:
			passive.tolUpRate.fire += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.TOLUP_RATE_WATER:
			passive.tolUpRate.water += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.TOLUP_RATE_THUNDER:
			passive.tolUpRate.thunder += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.TOLUP_RATE_SOIL:
			passive.tolUpRate.soil += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.TOLUP_RATE_LIGHT:
			passive.tolUpRate.light += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.TOLUP_RATE_DARK:
			passive.tolUpRate.dark += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.TOLUP_RATE_ALLELEMENT:
			passive.tolUpRate.AddElementOnly((float)value * 0.01f);
			break;
		case BuffParam.BUFFTYPE.TOLDOWN_RATE_FIRE:
			passive.tolDownRate.fire += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.TOLDOWN_RATE_WATER:
			passive.tolDownRate.water += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.TOLDOWN_RATE_THUNDER:
			passive.tolDownRate.thunder += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.TOLDOWN_RATE_SOIL:
			passive.tolDownRate.soil += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.TOLDOWN_RATE_LIGHT:
			passive.tolDownRate.light += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.TOLDOWN_RATE_DARK:
			passive.tolDownRate.dark += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.TOLDOWN_RATE_ALLELEMENT:
			passive.tolDownRate.AddElementOnly((float)value * 0.01f);
			break;
		case BuffParam.BUFFTYPE.JUSTGUARD_EXTEND_RATE:
			passive.justGuardExtendRate += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_FIRE:
			passive.skillAbsorbUp_OnlyAttackAndElement.fire += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_WATER:
			passive.skillAbsorbUp_OnlyAttackAndElement.water += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_THUNDER:
			passive.skillAbsorbUp_OnlyAttackAndElement.thunder += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_SOIL:
			passive.skillAbsorbUp_OnlyAttackAndElement.soil += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_LIGHT:
			passive.skillAbsorbUp_OnlyAttackAndElement.light += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_DARK:
			passive.skillAbsorbUp_OnlyAttackAndElement.dark += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.HEAT_GAUGE_INCREASE_UP:
			passive.heatGaugeIncreaseRate += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.HEAT_GAUGE_INCREASE_DOWN:
			passive.heatGaugeIncreaseRate -= (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.SOUL_GAUGE_INCREASE_UP:
			passive.soulGaugeIncreaseRate += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.SOUL_GAUGE_INCREASE_DOWN:
			passive.soulGaugeIncreaseRate -= (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.BURST_GAUGE_INCREASE_UP:
			passive.burstGaugeIncreaseRate += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.BURST_GAUGE_INCREASE_DOWN:
			passive.burstGaugeIncreaseRate -= (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.ORACLE_GAUGE_INCREASE_UP:
			passive.oracleGaugeIncreaseRate += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.ORACLE_GAUGE_INCREASE_DOWN:
			passive.oracleGaugeIncreaseRate -= (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.BOOST_DAMAGE_DOWN:
			passive.boostDamageDown += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.BOOST_ATTACK_SPEED_UP:
			passive.boostAttackSpeedUp += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.BOOST_MOVE_SPEED_UP:
			passive.boostMoveSpeedUp += (float)value * 0.01f;
			break;
		case BuffParam.BUFFTYPE.BOOST_AVOID_UP:
			passive.boostAvoidUp += (float)value * 0.01f;
			break;
		}
	}

	public virtual void OnSetPlayerStatus(int _level, int _atk, int _def, int _hp, bool send_packet = true, StageObjectManager.PlayerTransferInfo transfer_info = null, bool usingRealAtk = false)
	{
		playerAtk = _atk;
		playerDef = _def;
		playerHp = _hp;
		if (createInfo != null)
		{
			createInfo.charaInfo.level = _level;
			createInfo.charaInfo.atk = _atk;
			createInfo.charaInfo.def = _def;
			createInfo.charaInfo.hp = _hp;
		}
		InitParameter();
		if (!base.isDead)
		{
			int num2 = base.hp = (healHp = base.hpMax);
		}
		else
		{
			int num2 = base.hp = (healHp = 0);
		}
		if (transfer_info != null && !base.isDead)
		{
			base.hp = transfer_info.hp;
			healHp = transfer_info.healHp;
		}
		if (send_packet && playerSender != null)
		{
			playerSender.OnSetPlayerStatus(_level, _atk, _def, _hp);
		}
	}

	public void SetState(StageObjectManager.CreatePlayerInfo create_info, StageObjectManager.PlayerTransferInfo transfer_info = null)
	{
		if (create_info.charaInfo == null)
		{
			Log.Error("StageObjectManager.CreatePlayer() charaInfo is NULL");
			return;
		}
		createInfo = create_info;
		base.charaName = create_info.charaInfo.name;
		if (create_info.charaInfo.clanInfo != null)
		{
			string text = "FFFFFF";
			if (MonoBehaviourSingleton<GuildManager>.I.guildData != null && create_info.charaInfo.clanInfo.clanId == MonoBehaviourSingleton<GuildManager>.I.guildData.clanId)
			{
				text = "08FF00";
			}
			base.fullName = "[" + text + "][" + create_info.charaInfo.clanInfo.tag + "][-]" + base.charaName;
		}
		else
		{
			base.fullName = base.charaName;
		}
		baseState.atkList.Clear();
		baseState.defList.Clear();
		skillConstState.atkList.Clear();
		skillConstState.defList.Clear();
		guardEquipDef.Clear();
		for (int i = 0; i < 7; i++)
		{
			baseState.atkList.Add(0);
			baseState.defList.Add(0);
			skillConstState.atkList.Add(0);
			skillConstState.defList.Add(0);
			guardEquipDef.Add(0);
		}
		skillConstState.hp = 0;
		skillData.ids.Clear();
		skillData.lvs.Clear();
		skillData.exs.Clear();
		abilityData.ids.Clear();
		abilityData.APs.Clear();
		abilityItem.Clear();
		hpUp = 0;
		List<int> list = null;
		int equipIndex = 0;
		if (create_info.extentionInfo != null)
		{
			list = create_info.extentionInfo.weaponIndexList;
			equipIndex = create_info.extentionInfo.uniqueEquipmentIndex;
		}
		equipWeaponList = new List<CharaInfo.EquipItem>();
		weaponEquipItemDataList.Clear();
		for (int j = 0; j < 3; j++)
		{
			equipWeaponList.Add(null);
			weaponEquipItemDataList.Add(null);
		}
		passiveSkillList.Clear();
		int k = 0;
		for (int count = create_info.charaInfo.equipSet.Count; k < count; k++)
		{
			CharaInfo.EquipItem equipItem = create_info.charaInfo.equipSet[k];
			if (!SetEqState(equipItem, is_weapon_set: false))
			{
				continue;
			}
			int num = -1;
			if (list != null && list.Count > 0)
			{
				num = list.IndexOf(k);
			}
			else if (equipWeaponList[0] == null)
			{
				num = 0;
			}
			if (num >= 0 && num < equipWeaponList.Count)
			{
				equipWeaponList[num] = equipItem;
				EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)equipItem.eId);
				if (equipItemData != null)
				{
					weaponEquipItemDataList[num] = equipItemData;
					SetValueSpActionGaugeMax(equipItemData, num);
				}
			}
		}
		int index = -1;
		CharaInfo.EquipItem equipItem2 = null;
		if (transfer_info != null)
		{
			index = transfer_info.weaponIndex;
			equipItem2 = transfer_info.weaponData;
		}
		else
		{
			for (int l = 0; l < 3; l++)
			{
				if (equipWeaponList[l] != null)
				{
					index = l;
					equipItem2 = equipWeaponList[l];
					break;
				}
			}
		}
		if (equipItem2 != null)
		{
			SetNowWeapon(equipItem2, index, equipIndex);
		}
		SkillInfo.SkillSettingsInfo skillSettingsInfo = new SkillInfo.SkillSettingsInfo();
		if (MonoBehaviourSingleton<InGameSettingsManager>.I.player.enableTestSkill)
		{
			int m = 0;
			for (int num2 = 9; m < num2; m++)
			{
				SkillInfo.SkillSettingsInfo.Element element = new SkillInfo.SkillSettingsInfo.Element();
				element.baseInfo.id = MonoBehaviourSingleton<InGameSettingsManager>.I.player.testSkillIDs[m];
				element.baseInfo.level = 0;
				skillSettingsInfo.elementList.Add(element);
			}
		}
		else
		{
			for (int n = 0; n < 3; n++)
			{
				CharaInfo.EquipItem equipItem3 = equipWeaponList[n];
				EquipItemTable.EquipItemData equipItemData2 = null;
				if (equipItem3 != null)
				{
					equipItemData2 = Singleton<EquipItemTable>.I.GetEquipItemData((uint)equipItem3.eId);
				}
				int num3 = 0;
				if (equipItemData2 != null)
				{
					int num4 = 0;
					for (int count2 = equipItem3.sIds.Count; num4 < count2; num4++)
					{
						SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint)equipItem3.sIds[num4]);
						if (equipItemData2 == null)
						{
							continue;
						}
						bool flag = false;
						SKILL_SLOT_TYPE type = skillItemData.type;
						if ((uint)(type - 1) <= 2u)
						{
							flag = true;
						}
						if (flag)
						{
							SkillInfo.SkillSettingsInfo.Element element2 = new SkillInfo.SkillSettingsInfo.Element();
							element2.baseInfo.id = equipItem3.sIds[num4];
							element2.baseInfo.level = equipItem3.sLvs[num4];
							int exceedCnt = 0;
							if (num4 < equipItem3.sExs.Count)
							{
								exceedCnt = equipItem3.sExs[num4];
							}
							element2.baseInfo.exceedCnt = exceedCnt;
							if (transfer_info != null && n * 3 + num4 < transfer_info.useGaugeCounterList.Count)
							{
								element2.useGaugeCounter = transfer_info.useGaugeCounterList[n * 3 + num4];
							}
							skillSettingsInfo.elementList.Add(element2);
							num3++;
							if (num3 >= 3)
							{
								break;
							}
						}
					}
				}
				for (int num5 = 0; num5 < 3 - num3; num5++)
				{
					skillSettingsInfo.elementList.Add(null);
				}
			}
		}
		skillInfo.SetSettingsInfo(skillSettingsInfo, equipWeaponList);
		if (transfer_info != null)
		{
			rescueCount = transfer_info.rescueCount;
			if (!EnableRescueCountup())
			{
				rescueCount = GetInitRescueCount();
			}
			autoReviveCount = transfer_info.autoReviveCount;
			isUseInvincibleBuff = transfer_info.isUseInvincibleBuff;
			isUseInvincibleBadStatusBuff = transfer_info.isUseInvincibleBadStatusBuff;
			isInitDead = transfer_info.isInitDead;
			initRescueTime = transfer_info.initRescueTime;
			initContinueTime = transfer_info.initContinueTime;
			initBuffSyncParam = transfer_info.buffSyncParam;
			abilityCounterAttackNumList = transfer_info.abilityCounterAttackNumList;
			abilityCleaveComboNumList = transfer_info.cleaveComboNumList;
			if (transfer_info.spActionGauges != null && transfer_info.spActionGauges.Length != 0)
			{
				transfer_info.spActionGauges.CopyTo(spActionGauge, 0);
			}
			if (transfer_info.evolveGauges != null)
			{
				for (int num6 = 0; num6 < transfer_info.evolveGauges.Length; num6++)
				{
					evolveCtrl.SetGauge(transfer_info.evolveGauges[num6], num6);
				}
			}
			if (thsCtrl != null && transfer_info.burstCurrentRestBulletCount != null)
			{
				TwoHandSwordController.InitParam param = new TwoHandSwordController.InitParam
				{
					Owner = this,
					BurstInitParam = new TwoHandSwordBurstController.InitParam
					{
						Owner = this,
						ActionInfo = playerParameter.twoHandSwordActionInfo,
						MaxBulletCount = transfer_info.maxBulletCount,
						CurrentRestBullets = transfer_info.burstCurrentRestBulletCount,
						IsNeedFullBullet = false
					}
				};
				thsCtrl.InitAppend(param);
			}
			if (transfer_info.shieldReflectInfo != null)
			{
				shieldReflectInfo = transfer_info.shieldReflectInfo;
			}
			if (spearCtrl != null)
			{
				spearCtrl.stockedCounts = transfer_info.oracleSpearStockedCount;
			}
		}
		else
		{
			if (thsCtrl != null)
			{
				TwoHandSwordController.InitParam param2 = new TwoHandSwordController.InitParam
				{
					Owner = this,
					BurstInitParam = new TwoHandSwordBurstController.InitParam
					{
						Owner = this,
						ActionInfo = playerParameter.twoHandSwordActionInfo,
						MaxBulletCount = 6,
						CurrentRestBullets = null
					}
				};
				thsCtrl.InitAppend(param2);
			}
			if (spearCtrl != null)
			{
				spearCtrl.Init(this);
			}
		}
	}

	public StageObjectManager.PlayerTransferInfo CreateTransferInfo()
	{
		StageObjectManager.PlayerTransferInfo playerTransferInfo = new StageObjectManager.PlayerTransferInfo();
		playerTransferInfo.weaponIndex = weaponIndex;
		playerTransferInfo.weaponData = weaponData;
		playerTransferInfo.hp = base.hp;
		playerTransferInfo.healHp = healHp;
		playerTransferInfo.rescueCount = rescueCount;
		playerTransferInfo.autoReviveCount = autoReviveCount;
		playerTransferInfo.isUseInvincibleBuff = isUseInvincibleBuff;
		playerTransferInfo.isUseInvincibleBadStatusBuff = isUseInvincibleBadStatusBuff;
		if (playerTransferInfo.hp <= 0)
		{
			playerTransferInfo.isInitDead = true;
			playerTransferInfo.initRescueTime = rescueTime;
			playerTransferInfo.initContinueTime = continueTime;
		}
		playerTransferInfo.useGaugeCounterList = new List<float>();
		for (int i = 0; i < 9; i++)
		{
			SkillInfo.SkillParam skillParam = skillInfo.GetSkillParam(i);
			if (skillParam != null && skillParam.isValid)
			{
				playerTransferInfo.useGaugeCounterList.Add(skillParam.useGaugeCounter);
			}
			else
			{
				playerTransferInfo.useGaugeCounterList.Add(0f);
			}
		}
		playerTransferInfo.buffSyncParam = buffParam.CreateSyncParam();
		List<BuffParam.ConditionsAbility> conditionsAbilityList = buffParam.passive.conditionsAbilityList;
		if (conditionsAbilityList != null && conditionsAbilityList.Count > 0)
		{
			playerTransferInfo.abilityCounterAttackNumList = new List<int>();
			playerTransferInfo.cleaveComboNumList = new List<int>();
			foreach (BuffParam.ConditionsAbility item in conditionsAbilityList)
			{
				playerTransferInfo.abilityCounterAttackNumList.Add(item.counterAttackNum);
				playerTransferInfo.cleaveComboNumList.Add(item.cleaveComboNum);
			}
		}
		if (spActionGauge != null && spActionGauge.Length != 0)
		{
			playerTransferInfo.spActionGauges = new float[3];
			for (int j = 0; j < spActionGauge.Length; j++)
			{
				playerTransferInfo.spActionGauges[j] = spActionGauge[j];
			}
		}
		playerTransferInfo.evolveGauges = new float[3];
		for (int k = 0; k < 3; k++)
		{
			playerTransferInfo.evolveGauges[k] = evolveCtrl.GetGauge(k);
		}
		if (thsCtrl != null)
		{
			playerTransferInfo.burstCurrentRestBulletCount = thsCtrl.GetAllCurrentRestBulletCount;
			playerTransferInfo.maxBulletCount = thsCtrl.CurrentMaxBulletCount;
		}
		if (shieldReflectInfo != null)
		{
			playerTransferInfo.shieldReflectInfo = shieldReflectInfo;
		}
		if (spearCtrl != null)
		{
			playerTransferInfo.oracleSpearStockedCount = spearCtrl.stockedCounts;
		}
		return playerTransferInfo;
	}

	private bool SetEqState(CharaInfo.EquipItem item, bool is_weapon_set)
	{
		if (item == null)
		{
			return false;
		}
		EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)item.eId);
		if (equipItemData == null)
		{
			return false;
		}
		bool flag = equipItemData.IsWeapon();
		GrowEquipItemTable.GrowEquipItemData growEquipItemData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemData(equipItemData.growID, (uint)item.lv);
		List<int> list = new List<int>();
		if (growEquipItemData != null)
		{
			list.Add(growEquipItemData.GetGrowParamAtk(equipItemData.baseAtk));
			int[] growParamElemAtk = growEquipItemData.GetGrowParamElemAtk(equipItemData.atkElement);
			for (int i = 0; i < 6; i++)
			{
				list.Add(growParamElemAtk[i]);
			}
		}
		else
		{
			list.Add(equipItemData.baseAtk);
			for (int j = 0; j < 6; j++)
			{
				list.Add(equipItemData.atkElement[j]);
			}
		}
		EquipItemExceedParamTable.EquipItemExceedParamAll exceedParam = equipItemData.GetExceedParam((uint)item.exceed);
		if (exceedParam != null)
		{
			list[0] += exceedParam.atk;
			for (int k = 0; k < 6; k++)
			{
				list[k + 1] += exceedParam.atkElement[k];
			}
		}
		if (flag)
		{
			ATTACK_MODE aTTACK_MODE = ConvertEquipmentTypeToAttackMode(equipItemData.type);
			if (aTTACK_MODE != 0)
			{
				int num = (int)(aTTACK_MODE - 1);
				float num2 = MonoBehaviourSingleton<GlobalSettingsManager>.I.playerWeaponAttackRate[num];
				for (int l = 0; l < 7; l++)
				{
					list[l] = (int)((float)list[l] / num2);
				}
				spAttackType = equipItemData.spAttackType;
				extraAttackType = equipItemData.exAttackType;
			}
		}
		List<int> list2 = new List<int>();
		if (growEquipItemData != null)
		{
			list2.Add(growEquipItemData.GetGrowParamDef(equipItemData.baseDef));
			int[] growParamElemDef = growEquipItemData.GetGrowParamElemDef(equipItemData.defElement);
			for (int m = 0; m < 6; m++)
			{
				int num3 = growParamElemDef[m];
				if (equipItemData.isFormer)
				{
					num3 *= 10;
				}
				list2.Add(num3);
			}
		}
		else
		{
			list2.Add(equipItemData.baseDef);
			for (int n = 0; n < 6; n++)
			{
				int num4 = equipItemData.defElement[n];
				if (equipItemData.isFormer)
				{
					num4 *= 10;
				}
				list2.Add(num4);
			}
		}
		int num5 = 0;
		if (exceedParam != null)
		{
			list2[0] += exceedParam.def;
			for (int num6 = 0; num6 < 6; num6++)
			{
				list2[num6 + 1] += exceedParam.defElement[num6];
			}
			num5 += (int)exceedParam.hp;
		}
		if (is_weapon_set && flag)
		{
			weaponState.atkList = list;
			weaponState.defList = list2;
			int num7 = growEquipItemData?.GetGrowParamHp(equipItemData.baseHp) ?? ((int)equipItemData.baseHp);
			weaponState.hp = num7 + num5;
		}
		else
		{
			if (!flag)
			{
				for (int num8 = 0; num8 < 7; num8++)
				{
					baseState.atkList[num8] += list[num8];
					baseState.defList[num8] += list2[num8];
					guardEquipDef[num8] += list2[num8];
				}
				int num9 = growEquipItemData?.GetGrowParamHp(equipItemData.baseHp) ?? ((int)equipItemData.baseHp);
				num9 += num5;
				hpUp += num9;
			}
			int num10 = 0;
			for (int count = item.sIds.Count; num10 < count; num10++)
			{
				SetSkillState(item.sIds[num10], item.sLvs[num10], item.GetSkillExceed(num10), is_weapon_set, equipItemData.type);
			}
			a_ids.Clear();
			a_pts.Clear();
			a_ids.AddRange(item.aIds);
			a_pts.AddRange(item.aPts);
			for (int num11 = 0; num11 < equipItemData.fixedAbility.Length; num11++)
			{
				a_ids.Add(equipItemData.fixedAbility[num11].id);
				a_pts.Add(equipItemData.fixedAbility[num11].pt);
			}
			if (exceedParam != null)
			{
				for (int num12 = 0; num12 < exceedParam.ability.Length; num12++)
				{
					a_ids.Add(exceedParam.ability[num12].id);
					a_pts.Add(exceedParam.ability[num12].pt);
				}
			}
			int num13 = 0;
			for (int count2 = a_ids.Count; num13 < count2; num13++)
			{
				bool flag2 = false;
				int num14 = 0;
				for (int count3 = abilityData.ids.Count; num14 < count3; num14++)
				{
					if (abilityData.ids[num14] == a_ids[num13])
					{
						abilityData.APs[num14] += a_pts[num13];
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					abilityData.ids.Add(a_ids[num13]);
					abilityData.APs.Add(a_pts[num13]);
				}
			}
			if (item.ai != null && item.ai.abilityItemId != 0)
			{
				abilityItem.Add(item.ai);
			}
		}
		return flag;
	}

	public bool IsValidBuffSilence()
	{
		return IsValidBuff(BuffParam.BUFFTYPE.SILENCE);
	}

	public bool IsActSkillAction(int skill_index)
	{
		bool num = skillInfo.IsActSkillAction(skill_index);
		bool flag = IsValidBuffSilence();
		bool flag2 = IsCarrying() || base.actionID == (ACTION_ID)44;
		if (num && !flag)
		{
			return !flag2;
		}
		return false;
	}

	private void SetSkillState(int skill_id, int level, int exceedCnt, bool is_weapon_set, EQUIPMENT_TYPE type)
	{
		if (MonoBehaviourSingleton<InGameManager>.I.ContainsArenaCondition(ARENA_CONDITION.FORBID_MAGI_INCARNATION) && (skill_id == 404500100 || skill_id == 404500200 || skill_id == 404500300 || skill_id == 404500400))
		{
			return;
		}
		SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint)skill_id);
		GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(skillItemData.growID, level, exceedCnt);
		if (skillItemData.type == SKILL_SLOT_TYPE.PASSIVE)
		{
			RegisterPassiveSkill(skillItemData);
		}
		if (!is_weapon_set)
		{
			List<int> list = new List<int>();
			list.Add(growSkillItemData.GetGrowParamAtk(skillItemData.baseAtk));
			int[] growParamElemAtk = growSkillItemData.GetGrowParamElemAtk(skillItemData.atkElement);
			for (int i = 0; i < 6; i++)
			{
				list.Add(growParamElemAtk[i]);
			}
			List<int> list2 = new List<int>();
			list2.Add(growSkillItemData.GetGrowParamDef(skillItemData.baseDef));
			int[] growParamElemDef = growSkillItemData.GetGrowParamElemDef(skillItemData.defElement);
			for (int j = 0; j < 6; j++)
			{
				list2.Add(growParamElemDef[j]);
			}
			for (int k = 0; k < 7; k++)
			{
				skillConstState.atkList[k] += list[k];
				skillConstState.defList[k] += list2[k];
			}
			int growParamHp = growSkillItemData.GetGrowParamHp(skillItemData.baseHp);
			skillConstState.hp += growParamHp;
		}
		if (skillItemData.IsPassive())
		{
			skillData.ids.Add(skill_id);
			skillData.lvs.Add(level);
			skillData.exs.Add(exceedCnt);
		}
	}

	private void RegisterPassiveSkill(SkillItemTable.SkillItemData skillData)
	{
		for (int i = 0; i < 3; i++)
		{
			BuffParam.BUFFTYPE bUFFTYPE = skillData.supportType[i];
			if (bUFFTYPE != BuffParam.BUFFTYPE.NONE && !passiveSkillList.Contains(bUFFTYPE))
			{
				passiveSkillList.Add(bUFFTYPE);
			}
		}
	}

	private bool IsValidPassiveSkill(BuffParam.BUFFTYPE targetType)
	{
		return passiveSkillList.Contains(targetType);
	}

	public void SetNowWeapon(CharaInfo.EquipItem now_weapon, int index, int equipIndex)
	{
		SetEqState(now_weapon, is_weapon_set: true);
		weaponData = now_weapon;
		weaponIndex = index;
		uniqueEquipmentIndex = equipIndex;
		evolveCtrl.SetWeaponInfo();
	}

	public void SetPassiveParam()
	{
		base.buffParam.passive.Reset();
		base.atkBadStatus.Reset();
		for (int i = 0; i < 7; i++)
		{
			base.buffParam.passive.atkList[i] += skillConstState.atkList[i];
			base.buffParam.passive.defList[i] += skillConstState.defList[i];
		}
		base.buffParam.passive.hp += skillConstState.hp;
		if (attackMode == ATTACK_MODE.NONE)
		{
			return;
		}
		int type_index = (int)(attackMode - 1);
		for (int j = 0; j < skillData.ids.Count; j++)
		{
			SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint)skillData.ids[j]);
			if (!skillItemData.IsEnableEquipType(type_index))
			{
				continue;
			}
			SP_ATTACK_TYPE supportPassiveSpAttackType = skillItemData.supportPassiveSpAttackType;
			GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(skillItemData.growID, skillData.lvs[j], skillData.exs[j]);
			for (int k = 0; k < 3; k++)
			{
				if (skillItemData.IsEnableSupportEquipType(type_index, k) && Utility.IsEnableSpAttackType(supportPassiveSpAttackType, spAttackType))
				{
					SetPassiveBuff(skillItemData.supportType[k], growSkillItemData.GetGrowParamSupprtValue(skillItemData.supportValue, k));
				}
			}
		}
		int currentEventID = Utility.GetCurrentEventID();
		EQUIPMENT_TYPE equipType = ConvertAttackModeToEquipmentType(attackMode);
		int l = 0;
		for (int count = abilityData.ids.Count; l < count; l++)
		{
			base.buffParam.AddAbility((uint)abilityData.ids[l], abilityData.APs[l], equipType, spAttackType, currentEventID);
		}
		foreach (AbilityItem item in abilityItem)
		{
			BuffParam buffParam = base.buffParam;
			AbilityDataTable.AbilityData.AbilityInfo[] info = AbilityItemInfo.ConvertAbilityItemToInfo(item).ToArray();
			buffParam.AddAbilityItemParam(info, equipType, spAttackType, currentEventID);
		}
		ApplyConditionAbilityValue();
		base.buffParam.passive.firstInitialized = true;
		base.buffParam.UpdateConditionsAbility();
	}

	private void ApplyConditionAbilityValue()
	{
		if (abilityCounterAttackNumList == null)
		{
			return;
		}
		List<BuffParam.ConditionsAbility> conditionsAbilityList = buffParam.passive.conditionsAbilityList;
		if (conditionsAbilityList != null && conditionsAbilityList.Count > 0)
		{
			for (int i = 0; i < conditionsAbilityList.Count; i++)
			{
				conditionsAbilityList[i].counterAttackNum = abilityCounterAttackNumList[i];
			}
		}
		abilityCounterAttackNumList = null;
	}

	private void ApplyCleaveComboConditionAbilityValue()
	{
		if (abilityCleaveComboNumList == null)
		{
			return;
		}
		List<BuffParam.ConditionsAbility> conditionsAbilityList = buffParam.passive.conditionsAbilityList;
		if (conditionsAbilityList != null && conditionsAbilityList.Count > 0)
		{
			for (int i = 0; i < conditionsAbilityList.Count; i++)
			{
				conditionsAbilityList[i].cleaveComboNum = abilityCleaveComboNumList[i];
			}
		}
		abilityCleaveComboNumList = null;
	}

	public virtual void InitParameter()
	{
		ResetStatusParam();
		base.attack.normal += weaponState.atkList[0];
		base.attack.fire += weaponState.atkList[1];
		base.attack.water += weaponState.atkList[2];
		base.attack.thunder += weaponState.atkList[3];
		base.attack.soil += weaponState.atkList[4];
		base.attack.light += weaponState.atkList[5];
		base.attack.dark += weaponState.atkList[6];
		base.defense.normal += (float)baseState.defList[0] + playerDef + (float)weaponState.defList[0];
		base.defense.fire += (float)baseState.defList[0] + playerDef + (float)weaponState.defList[0];
		base.defense.water += (float)baseState.defList[0] + playerDef + (float)weaponState.defList[0];
		base.defense.thunder += (float)baseState.defList[0] + playerDef + (float)weaponState.defList[0];
		base.defense.soil += (float)baseState.defList[0] + playerDef + (float)weaponState.defList[0];
		base.defense.light += (float)baseState.defList[0] + playerDef + (float)weaponState.defList[0];
		base.defense.dark += (float)baseState.defList[0] + playerDef + (float)weaponState.defList[0];
		base.tolerance.normal += 0f;
		base.tolerance.fire += (float)baseState.defList[1] + (float)weaponState.defList[1];
		base.tolerance.water += (float)baseState.defList[2] + (float)weaponState.defList[2];
		base.tolerance.thunder += (float)baseState.defList[3] + (float)weaponState.defList[3];
		base.tolerance.soil += (float)baseState.defList[4] + (float)weaponState.defList[4];
		base.tolerance.light += (float)baseState.defList[5] + (float)weaponState.defList[5];
		base.tolerance.dark += (float)baseState.defList[6] + (float)weaponState.defList[6];
		base.hpMax = (int)((float)(playerHp + hpUp + weaponState.hp) * (1f + buffParam.GetPassiveHpRate())) + buffParam.passive.hp;
		if (base.hpMax < 1)
		{
			base.hpMax = 1;
		}
		if (base.hp > base.hpMax)
		{
			int num2 = base.hp = (healHp = base.hpMax);
		}
		AtkAttribute atkAttribute = new AtkAttribute();
		atkAttribute.Set(1f);
		atkAttribute.Add(buffParam.passive.defUpRate);
		atkAttribute.Sub(buffParam.passive.defDownRate);
		base.defense.Mul(atkAttribute);
		base.defense.normal += buffParam.passive.defList[0];
		base.defense.fire += buffParam.passive.defList[1] + buffParam.passive.defList[0];
		base.defense.water += buffParam.passive.defList[2] + buffParam.passive.defList[0];
		base.defense.thunder += buffParam.passive.defList[3] + buffParam.passive.defList[0];
		base.defense.soil += buffParam.passive.defList[4] + buffParam.passive.defList[0];
		base.defense.light += buffParam.passive.defList[5] + buffParam.passive.defList[0];
		base.defense.dark += buffParam.passive.defList[6] + buffParam.passive.defList[0];
		base.defense.CheckMinus();
		defenseCoefficient.normal = 1f;
		defenseCoefficient.fire = CalcDefenseElementCoefficient(base.defense.fire);
		defenseCoefficient.water = CalcDefenseElementCoefficient(base.defense.water);
		defenseCoefficient.thunder = CalcDefenseElementCoefficient(base.defense.thunder);
		defenseCoefficient.soil = CalcDefenseElementCoefficient(base.defense.soil);
		defenseCoefficient.light = CalcDefenseElementCoefficient(base.defense.light);
		defenseCoefficient.dark = CalcDefenseElementCoefficient(base.defense.dark);
		defenseThreshold = MonoBehaviourSingleton<InGameSettingsManager>.I.passive.playerDefenseThreshold;
	}

	private float CalcDefenseElementCoefficient(float defenseValue)
	{
		if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			return 4.5f;
		}
		int playerDefenseThreshold = MonoBehaviourSingleton<InGameSettingsManager>.I.passive.playerDefenseThreshold;
		int playerDefenseCoefficient = MonoBehaviourSingleton<InGameSettingsManager>.I.passive.playerDefenseCoefficient;
		if (playerDefenseThreshold <= 0 || playerDefenseCoefficient <= 0)
		{
			return 4.5f;
		}
		if (!(defenseValue > (float)playerDefenseThreshold))
		{
			return 4.5f;
		}
		return 4.5f / (1f + (defenseValue - (float)playerDefenseThreshold) / (float)playerDefenseCoefficient);
	}

	protected void SetAttackMode(ATTACK_MODE attack_mode)
	{
		if (base.isInitialized && base.actionID != (ACTION_ID)27)
		{
			ActIdle();
		}
		attackMode = attack_mode;
		SetPassiveParam();
		weaponInfo = playerParameter.weaponInfo[(int)(attack_mode - 1)];
		if (weaponInfo != null)
		{
			if (weaponInfo.defenceRate != 1f)
			{
				base.defense.Mul(weaponInfo.defenceRate);
			}
			base.attackWeakRate = weaponInfo.attackWeakRate;
			base.attackDownRate = weaponInfo.attackDownRate;
			base.elementWeakRate = weaponInfo.weakRateElementAttack;
			base.elementSkillWeakRate = weaponInfo.weakRateElementSkillAttack;
			base.skillWeakRate = weaponInfo.weakRateSkillAttack;
			base.healWeakRate = weaponInfo.weakRateHealAttack;
			base.elementSpAttackWeakRate = weaponInfo.weakRateElementSpAttack;
			base.downPowerWeak = weaponInfo.downPowerWeak;
			base.downPowerSimpleWeak = weaponInfo.downPowerSimpleWeak;
			AttackInfo[] attackInfos = base.attackInfos;
			AttackInfo[] attackHitInfos = playerParameter.weaponAttackInfoList[(int)(attack_mode - 1)].attackHitInfos;
			AttackInfo[] array = Utility.CreateMergedArray(attackInfos, attackHitInfos);
			AttackInfo[] array_a = array;
			attackHitInfos = playerParameter.weaponAttackInfoList[(int)(attack_mode - 1)].attackContinuationInfos;
			array = Utility.CreateMergedArray(array_a, attackHitInfos);
			base.attackInfos = Utility.DistinctArray(array);
		}
	}

	public void AddAttackInfos(AttackInfo[] addInfos)
	{
		if (playerParameter != null)
		{
			attackInfos = Utility.CreateMergedArray(attackInfos, addInfos);
		}
	}

	public override bool OnBuffStart(BuffParam.BuffData buffData)
	{
		CreateWeaponLinkEffect("BUFF_LOOP_" + buffData.type.ToString());
		bool result = base.OnBuffStart(buffData);
		int i = 0;
		for (int count = m_weaponCtrlList.Count; i < count; i++)
		{
			m_weaponCtrlList[i].OnBuffStart(buffData);
		}
		return result;
	}

	public override void OnBuffRoutine(BuffParam.BuffData buffData, bool packet = false)
	{
		base.OnBuffRoutine(buffData, packet);
		_ = buffData.type;
		int value = buffData.value;
		switch (buffData.type)
		{
		case BuffParam.BUFFTYPE.REGENERATE:
		case BuffParam.BUFFTYPE.REGENERATE_PROPORTION:
			if (base.hp > healHp)
			{
				healHp = base.hp;
			}
			break;
		case BuffParam.BUFFTYPE.BLEEDING:
		{
			base.hp -= value;
			if (base.hp <= 0)
			{
				base.hp = 0;
				healHp = 0;
				ActDead(force_sync: true);
			}
			InGameSettingsManager.BleedingParam bleedingParam = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.bleedingParam;
			Transform effect = EffectManager.GetEffect(bleedingParam.effectName, FindNode(bleedingParam.effectNodeName));
			if (effect != null)
			{
				effect.localPosition = bleedingParam.effectPosition;
				effect.localRotation = Quaternion.Euler(bleedingParam.effectRotation);
				float num = bleedingParam.effectScale;
				if (num == 0f)
				{
					num = 1f;
				}
				effect.localScale = Vector3.one * num;
			}
			break;
		}
		}
	}

	public override void OnPoisonStart(int fromObjectID = 0)
	{
		float poisonTime = buffParam.GetPoisonTime();
		if (!(poisonTime <= 0f))
		{
			BuffParam.BuffData buffData = new BuffParam.BuffData();
			buffData.type = BuffParam.BUFFTYPE.POISON;
			buffData.time = poisonTime;
			buffData.value = (int)((float)base.hpMax * 0.02f);
			buffData.valueType = BuffParam.VALUE_TYPE.RATE;
			buffData.interval = 2f;
			OnBuffStart(buffData);
		}
	}

	public override void OnBurningStart()
	{
		float burningTime = buffParam.GetBurningTime();
		if (!(burningTime <= 0f))
		{
			BuffParam.BuffData buffData = new BuffParam.BuffData();
			buffData.type = BuffParam.BUFFTYPE.BURNING;
			buffData.time = burningTime;
			buffData.value = (int)((float)base.hpMax * 0.03f);
			buffData.valueType = BuffParam.VALUE_TYPE.RATE;
			buffData.interval = 1f;
			OnBuffStart(buffData);
		}
	}

	public override void OnBleedingStart()
	{
		float bleedTime = buffParam.GetBleedTime();
		if (!(bleedTime <= 0f))
		{
			BuffParam.BuffData buffData = new BuffParam.BuffData();
			buffData.type = BuffParam.BUFFTYPE.BLEEDING;
			buffData.time = bleedTime;
			buffData.value = (int)((float)base.hpMax * MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.bleedingParam.damageHpRate);
			buffData.valueType = BuffParam.VALUE_TYPE.RATE;
			buffData.interval = 0f;
			OnBuffStart(buffData);
		}
	}

	public override void OnSpeedDown()
	{
		float speedDownTime = buffParam.GetSpeedDownTime();
		if (!(speedDownTime <= 0f))
		{
			BuffParam.BuffData buffData = new BuffParam.BuffData();
			buffData.type = BuffParam.BUFFTYPE.MOVE_SPEED_DOWN;
			buffData.time = speedDownTime;
			buffData.valueType = BuffParam.VALUE_TYPE.CONSTANT;
			buffData.value = 50;
			OnBuffStart(buffData);
		}
	}

	public override void OnAttackSpeedDown()
	{
		float attackSpeedDownTime = buffParam.GetAttackSpeedDownTime();
		if (!(attackSpeedDownTime <= 0f))
		{
			BuffParam.BuffData buffData = new BuffParam.BuffData();
			buffData.type = BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN;
			buffData.time = attackSpeedDownTime;
			buffData.valueType = BuffParam.VALUE_TYPE.CONSTANT;
			buffData.value = 50;
			if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
			{
				buffData.value = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.attackSpeedDownParam.value;
			}
			OnBuffStart(buffData);
		}
	}

	public override void OnDeadlyPoisonStart()
	{
		float deadlyPoisonTime = buffParam.GetDeadlyPoisonTime();
		if (!(deadlyPoisonTime <= 0f))
		{
			float interval = 2f;
			if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
			{
				interval = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.deadlyPosion.interval;
			}
			float num = 0.1f;
			if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
			{
				num = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.deadlyPosion.percent;
			}
			BuffParam.BuffData buffData = new BuffParam.BuffData();
			buffData.type = BuffParam.BUFFTYPE.DEADLY_POISON;
			buffData.time = deadlyPoisonTime;
			buffData.value = (int)((float)base.hpMax * num);
			buffData.valueType = BuffParam.VALUE_TYPE.RATE;
			buffData.interval = interval;
			OnBuffStart(buffData);
		}
	}

	public override void OnInkSplash(InkSplashInfo info)
	{
		if (info != null)
		{
			BuffParam.BuffData buffData = new BuffParam.BuffData();
			buffData.value = 100;
			buffData.type = BuffParam.BUFFTYPE.INK_SPLASH;
			buffData.time = info.duration;
			buffData.interval = Time.deltaTime;
			buffData.damage = 0;
			OnBuffStart(buffData);
		}
	}

	public override void OnSlideStart()
	{
		BuffParam.BuffData buffData = new BuffParam.BuffData();
		buffData.value = 100;
		buffData.type = BuffParam.BUFFTYPE.SLIDE;
		buffData.time = buffParam.GetSlideTime();
		buffData.damage = 0;
		OnBuffStart(buffData);
	}

	public override void OnSilenceStart()
	{
		float silenceTime = buffParam.GetSilenceTime();
		if (!(silenceTime <= 0f))
		{
			BuffParam.BuffData buffData = new BuffParam.BuffData();
			buffData.value = 100;
			buffData.type = BuffParam.BUFFTYPE.SILENCE;
			buffData.time = silenceTime;
			buffData.damage = 0;
			OnBuffStart(buffData);
		}
	}

	public override void OnCantHealHpStart()
	{
		float cantHealHpTime = buffParam.GetCantHealHpTime();
		if (!(cantHealHpTime <= 0f))
		{
			BuffParam.BuffData buffData = new BuffParam.BuffData();
			buffData.value = 100;
			buffData.type = BuffParam.BUFFTYPE.CANT_HEAL_HP;
			buffData.time = cantHealHpTime;
			buffData.damage = 0;
			OnBuffStart(buffData);
		}
	}

	public override void OnBlindStart()
	{
		float blindTime = buffParam.GetBlindTime();
		if (!(blindTime <= 0f))
		{
			BuffParam.BuffData buffData = new BuffParam.BuffData();
			buffData.value = 100;
			buffData.type = BuffParam.BUFFTYPE.BLIND;
			buffData.time = blindTime;
			buffData.damage = 0;
			OnBuffStart(buffData);
		}
	}

	public override void OnStoneStart()
	{
		BuffParam.BuffData buffData = new BuffParam.BuffData();
		buffData.value = 100;
		buffData.type = BuffParam.BUFFTYPE.STONE;
		buffData.time = -1f;
		buffData.endless = true;
		buffData.damage = 0;
		OnBuffStart(buffData);
	}

	public override void OnAcidStart()
	{
		float acidTime = buffParam.GetAcidTime();
		if (!(acidTime <= 0f))
		{
			float interval = 2f;
			if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
			{
				interval = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.acidParam.interval;
			}
			float num = 0.02f;
			if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
			{
				num = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.acidParam.percent;
			}
			BuffParam.BuffData buffData = new BuffParam.BuffData();
			buffData.type = BuffParam.BUFFTYPE.ACID;
			buffData.time = acidTime;
			buffData.value = (int)((float)base.hpMax * num);
			buffData.valueType = BuffParam.VALUE_TYPE.RATE;
			buffData.interval = interval;
			OnBuffStart(buffData);
		}
	}

	public override void OnBuffCancellation()
	{
		if (IsInBarrier() || IsValidBuff(BuffParam.BUFFTYPE.INVINCIBLE_BUFF_CANCELLATION_EXPAND))
		{
			return;
		}
		if (IsValidBuff(BuffParam.BUFFTYPE.INVINCIBLE_BUFF_CANCELLATION))
		{
			buffParam.DecreaseInvincibleBuffCancellation();
			return;
		}
		bool flag = false;
		List<BuffParam.BUFFTYPE> ignoreBuffCancellation = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.ignoreBuffCancellation;
		int i = 0;
		for (int num = 221; i < num; i++)
		{
			BuffParam.BUFFTYPE bUFFTYPE = (BuffParam.BUFFTYPE)i;
			if (!ignoreBuffCancellation.Contains(bUFFTYPE) && OnBuffEnd(bUFFTYPE, sync: false))
			{
				flag = true;
			}
		}
		if (flag)
		{
			SendBuffSync();
		}
	}

	public override bool IsValidBuff(BuffParam.BUFFTYPE targetType)
	{
		if (buffParam == null)
		{
			return false;
		}
		return buffParam.GetValue(targetType) > 0;
	}

	public bool CheckIgnoreBuff(BuffParam.BUFFTYPE targetType)
	{
		bool result = false;
		if (targetType == BuffParam.BUFFTYPE.GHOST_FORM)
		{
			if (IsValidBuff(BuffParam.BUFFTYPE.BREAK_GHOST_FORM) || IsValidPassiveSkill(BuffParam.BUFFTYPE.BREAK_GHOST_FORM) || IsValidBuffByAbility(BuffParam.BUFFTYPE.BREAK_GHOST_FORM))
			{
				result = true;
			}
		}
		else
		{
			result = true;
		}
		return result;
	}

	public override void OnHitAttack(AttackHitInfo info, AttackHitColliderProcessor.HitParam hit_param)
	{
		if (hit_param.toObject is Enemy)
		{
			Enemy enemy = hit_param.toObject as Enemy;
			if (enemy.isSummonAttack || (enemy.regionInfos.Length > hit_param.regionID && !enemy.regionInfos[hit_param.regionID].isAtkColliderHit))
			{
				return;
			}
		}
		switch (info.attackType)
		{
		case AttackHitInfo.ATTACK_TYPE.TWO_HAND_SWORD_SP:
			isHitSpAttack = true;
			break;
		case AttackHitInfo.ATTACK_TYPE.SPEAR_SP:
			switch (spAttackType)
			{
			case SP_ATTACK_TYPE.NONE:
				hitSpearSpecialAction = true;
				if (playerParameter.spearActionInfo.rushCancellableTime > 0f)
				{
					enableCancelToAttack = true;
				}
				break;
			case SP_ATTACK_TYPE.SOUL:
				if (hit_param.toObject is Enemy)
				{
					inputComboFlag = true;
					inputComboID = playerParameter.spearActionInfo.Soul_SpAttackContinueId;
					spearCtrl.ContinueBladeEffect();
					spearCtrl.MakeInvincible();
				}
				break;
			}
			break;
		case AttackHitInfo.ATTACK_TYPE.BURST_SPEAR_COMBO3:
			if (spAttackType == SP_ATTACK_TYPE.BURST && hit_param.toObject is Enemy)
			{
				spearCtrl.EnableHitFlag();
				enableCancelToAttack = true;
			}
			break;
		case AttackHitInfo.ATTACK_TYPE.HEAL_ATTACK:
		{
			Enemy enemy3 = hit_param.toObject as Enemy;
			if (enemy3 == null)
			{
				break;
			}
			healAtkRate = info.atkRate * enemy3.healDamageRate;
			EnemyRegionWork[] regionWorks = enemy3.regionWorks;
			for (int i = 0; i < regionWorks.Length; i++)
			{
				if (regionWorks[i] != null && Enemy.IsWeakStateHealAttack(regionWorks[i].weakState))
				{
					hit_param.regionID = i;
					break;
				}
			}
			break;
		}
		case AttackHitInfo.ATTACK_TYPE.SNATCH:
		{
			Enemy enemy2 = hit_param.toObject as Enemy;
			if (!(enemy2 == null))
			{
				moveStopRange = playerParameter.ohsActionInfo.Soul_MoveStopRange;
				if (IsCoopNone() || IsOriginal())
				{
					snatchCtrl.OnHit(enemy2.id, hit_param.point);
				}
				if (hit_param.toObject.hitOffFlag != 0)
				{
					return;
				}
			}
			break;
		}
		case AttackHitInfo.ATTACK_TYPE.BURST_THS_COMBO03:
			if (thsCtrl != null)
			{
				thsCtrl.Set3rdComboAttackHitFlag(_isHit: true);
			}
			break;
		case AttackHitInfo.ATTACK_TYPE.FROM_AVOID:
			if (thsCtrl != null)
			{
				thsCtrl.SetIsHitAvoidAttack(_isHit: true);
			}
			break;
		case AttackHitInfo.ATTACK_TYPE.THS_ORACLE_HORIZONTAL:
			thsCtrl.oracleCtrl.HitHorizontal();
			break;
		}
		if (hit_param.toObject is Enemy)
		{
			spearCtrl.SacrificeHPBySoulAttackHit();
			spearCtrl.HealHPBySoulSpAttackHit(info.spAttackType);
		}
		base.OnHitAttack(info, hit_param);
	}

	protected override bool IsValidAttackedHit(StageObject from_object)
	{
		if (from_object is Player)
		{
			return false;
		}
		return base.IsValidAttackedHit(from_object);
	}

	protected override void OnAttackFromHitDirection(AttackedHitStatusDirection status, StageObject to_object)
	{
		base.OnAttackFromHitDirection(status, to_object);
		if (!(to_object is Enemy))
		{
			return;
		}
		Enemy obj = to_object as Enemy;
		SetHitStop(status.attackInfo.toEnemy.hitStopTime);
		obj.SetHitStop(status.attackInfo.toEnemy.enemyHitStopTime);
		if (CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.BURST) && status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.JUSTGUARD_ATTACK)
		{
			isSuccessParry = true;
		}
		if (IsCoopNone() || IsOriginal())
		{
			if (isActOneHandSwordCounter)
			{
				skillInfo.OnHitCounterEnemy(status.attackInfo);
			}
			else if (!isActSkillAction)
			{
				skillInfo.OnHitAttackEnemy(status.attackInfo);
			}
		}
		if (isActSkillAction && isAbleToSkipSkillAction)
		{
			SetNextTrigger();
		}
	}

	protected override void OnPlayAttackedHitEffect(AttackedHitStatusDirection status)
	{
		if (!IsValidAttackedHit(status.fromObject))
		{
			return;
		}
		bool flag = this is Self;
		bool flag2 = false;
		if (status.attackInfo.hitSEID != 0)
		{
			if (EnablePlaySound())
			{
				SoundManager.PlayOneShotSE(status.attackInfo.hitSEID, status.hitPos);
			}
			flag2 = true;
		}
		if (MonoBehaviourSingleton<InGameManager>.I.graphicOptionType > 0 && (flag || MonoBehaviourSingleton<InGameManager>.I.graphicOptionType > 1))
		{
			if (!string.IsNullOrEmpty(status.attackInfo.hitEffectName))
			{
				EffectManager.OneShot(status.attackInfo.hitEffectName, status.hitPos, status.hitParam.rot, flag);
			}
			else if (!flag2 || status.attackInfo.playCommonHitEffect)
			{
				EffectManager.OneShot("ef_btl_pl_damage_01", status.hitPos, status.hitParam.rot, flag);
			}
		}
	}

	protected override bool IsDamageValid(AttackedHitStatusDirection status)
	{
		if (status.fromType != OBJECT_TYPE.ENEMY)
		{
			return false;
		}
		if (IsInBarrier())
		{
			return false;
		}
		return true;
	}

	public override void AbsorptionProc(Character targetChar, AttackedHitStatusLocal status)
	{
		AttackHitInfo attackInfo = status.attackInfo;
		if (attackInfo != null && !(attackInfo.absorptance <= 0f))
		{
			float num = attackInfo.absorptance * 0.01f;
			int num2 = (int)((float)status.damage * num);
			if (num2 < 1)
			{
				num2 = 1;
			}
			HealData healData = new HealData(num2, HEAL_TYPE.NONE, HEAL_EFFECT_TYPE.ABSORB, new List<int>
			{
				10
			});
			OnHealReceive(healData);
		}
	}

	public override void AbsorptionProcByBuff(AttackedHitStatusLocal status)
	{
		if (status.attackInfo.isSkillReference)
		{
			return;
		}
		List<BuffParam.BuffData> hitAbsorbBuffDataList = buffParam.GetHitAbsorbBuffDataList();
		if (hitAbsorbBuffDataList.IsNullOrEmpty() || IsValidBuff(BuffParam.BUFFTYPE.SHIELD))
		{
			return;
		}
		float num = 0f;
		for (int i = 0; i < hitAbsorbBuffDataList.Count; i++)
		{
			switch (hitAbsorbBuffDataList[i].type)
			{
			case BuffParam.BUFFTYPE.HIT_ABSORB_NORMAL:
				num += CalcAbsorbValueByBuff(status.damageDetails.normal, hitAbsorbBuffDataList[i]);
				break;
			case BuffParam.BUFFTYPE.HIT_ABSORB_FIRE:
				num += CalcAbsorbValueByBuff(status.damageDetails.fire, hitAbsorbBuffDataList[i]);
				break;
			case BuffParam.BUFFTYPE.HIT_ABSORB_WATER:
				num += CalcAbsorbValueByBuff(status.damageDetails.water, hitAbsorbBuffDataList[i]);
				break;
			case BuffParam.BUFFTYPE.HIT_ABSORB_THUNDER:
				num += CalcAbsorbValueByBuff(status.damageDetails.thunder, hitAbsorbBuffDataList[i]);
				break;
			case BuffParam.BUFFTYPE.HIT_ABSORB_SOIL:
				num += CalcAbsorbValueByBuff(status.damageDetails.soil, hitAbsorbBuffDataList[i]);
				break;
			case BuffParam.BUFFTYPE.HIT_ABSORB_LIGHT:
				num += CalcAbsorbValueByBuff(status.damageDetails.light, hitAbsorbBuffDataList[i]);
				break;
			case BuffParam.BUFFTYPE.HIT_ABSORB_DARK:
				num += CalcAbsorbValueByBuff(status.damageDetails.dark, hitAbsorbBuffDataList[i]);
				break;
			case BuffParam.BUFFTYPE.HIT_ABSORB_ALL:
				num += CalcAbsorbValueByBuff(status.damage, hitAbsorbBuffDataList[i]);
				break;
			}
		}
		int num2 = Mathf.FloorToInt(num);
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid() && num2 > MonoBehaviourSingleton<InGameSettingsManager>.I.buff.absorbDamageParam.limitPlayerHitAbsorb)
		{
			num2 = MonoBehaviourSingleton<InGameSettingsManager>.I.buff.absorbDamageParam.limitPlayerHitAbsorb;
		}
		if (num2 > 0)
		{
			HealData healData = new HealData(num2, HEAL_TYPE.NONE, HEAL_EFFECT_TYPE.HIT_ABSORB, new List<int>
			{
				10
			});
			OnHealReceive(healData);
		}
	}

	public override bool CutAndAbsorbDamageByBuff(Character targetCharacter, AttackedHitStatusLocal status)
	{
		List<BuffParam.BuffData> absorbBuffDataList = buffParam.GetAbsorbBuffDataList();
		if (absorbBuffDataList.IsNullOrEmpty())
		{
			return false;
		}
		if (IsValidBuff(BuffParam.BUFFTYPE.SHIELD))
		{
			return false;
		}
		AtkAttribute absorbAtkAttribute = GetAbsorbAtkAttribute(status.damageDetails, absorbBuffDataList);
		status.damageDetails.Sub(absorbAtkAttribute);
		status.damage = Mathf.FloorToInt(status.damageDetails.CalcTotal());
		if (status.damage < 0)
		{
			status.damage = 0;
		}
		if (absorbAtkAttribute.CalcTotal() > 0f)
		{
			isAbsorbDamageSuperArmor = true;
		}
		int num = Mathf.FloorToInt(absorbAtkAttribute.CalcTotal());
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid() && num > MonoBehaviourSingleton<InGameSettingsManager>.I.buff.absorbDamageParam.limitPlayerAbsorbDamage)
		{
			num = MonoBehaviourSingleton<InGameSettingsManager>.I.buff.absorbDamageParam.limitPlayerAbsorbDamage;
		}
		if (num <= 0)
		{
			return false;
		}
		HealData healData = new HealData(num, HEAL_TYPE.NONE, HEAL_EFFECT_TYPE.ABSORB, new List<int>
		{
			10
		});
		OnHealReceive(healData);
		return true;
	}

	private AtkAttribute GetAbsorbAtkAttribute(AtkAttribute damageDetails, List<BuffParam.BuffData> absorbBuffDataList = null)
	{
		AtkAttribute atkAttribute = new AtkAttribute();
		if (absorbBuffDataList == null)
		{
			absorbBuffDataList = buffParam.GetAbsorbBuffDataList();
			if (absorbBuffDataList.IsNullOrEmpty())
			{
				return atkAttribute;
			}
		}
		for (int i = 0; i < absorbBuffDataList.Count; i++)
		{
			BuffParam.BuffData buffData = absorbBuffDataList[i];
			switch (buffData.type)
			{
			case BuffParam.BUFFTYPE.ABSORB_NORMAL:
				atkAttribute.normal = CalcAbsorbValueByBuff(damageDetails.normal, buffData);
				break;
			case BuffParam.BUFFTYPE.ABSORB_FIRE:
				atkAttribute.fire = CalcAbsorbValueByBuff(damageDetails.fire, buffData);
				break;
			case BuffParam.BUFFTYPE.ABSORB_WATER:
				atkAttribute.water = CalcAbsorbValueByBuff(damageDetails.water, buffData);
				break;
			case BuffParam.BUFFTYPE.ABSORB_THUNDER:
				atkAttribute.thunder = CalcAbsorbValueByBuff(damageDetails.thunder, buffData);
				break;
			case BuffParam.BUFFTYPE.ABSORB_SOIL:
				atkAttribute.soil = CalcAbsorbValueByBuff(damageDetails.soil, buffData);
				break;
			case BuffParam.BUFFTYPE.ABSORB_LIGHT:
				atkAttribute.light = CalcAbsorbValueByBuff(damageDetails.light, buffData);
				break;
			case BuffParam.BUFFTYPE.ABSORB_DARK:
				atkAttribute.dark = CalcAbsorbValueByBuff(damageDetails.dark, buffData);
				break;
			}
		}
		return atkAttribute;
	}

	private float CalcAbsorbValueByBuff(float damage, BuffParam.BuffData absorbBuff)
	{
		float result = 0f;
		switch (absorbBuff.valueType)
		{
		case BuffParam.VALUE_TYPE.RATE:
			result = (float)((double)(damage * (float)absorbBuff.value) * 0.01);
			break;
		case BuffParam.VALUE_TYPE.CONSTANT:
			result = ((damage < (float)absorbBuff.value) ? damage : ((float)absorbBuff.value));
			break;
		}
		return result;
	}

	public override bool ChargeSkillWhenDamagedByBuff()
	{
		if (!IsValidBuff(BuffParam.BUFFTYPE.SKILL_CHARGE_WHEN_DAMAGED))
		{
			return false;
		}
		if (IsValidBuff(BuffParam.BUFFTYPE.SHIELD))
		{
			return false;
		}
		int value = buffParam.GetValue(BuffParam.BUFFTYPE.SKILL_CHARGE_WHEN_DAMAGED);
		if (value <= 0)
		{
			return false;
		}
		OnChargeSkillGaugeReceive(BuffParam.BUFFTYPE.SKILL_CHARGE_WHEN_DAMAGED, value, -1);
		return true;
	}

	public override bool InvincibleDamageByBuff(Character targetCharacter, AttackedHitStatusLocal status)
	{
		if (buffParam.GetInvincibleBuffDataList().IsNullOrEmpty())
		{
			return false;
		}
		AtkAttribute invinsibleMulRate = GetInvinsibleMulRate();
		status.damageDetails.Mul(invinsibleMulRate);
		int damage = Mathf.FloorToInt(status.damageDetails.CalcTotal());
		if (status.damage < 0)
		{
			damage = 0;
		}
		status.damage = damage;
		if (status.damage == 0)
		{
			isInvincibleDamageSuperArmor = true;
			shouldShowInvincibleDamage = true;
		}
		return true;
	}

	public override void OnAttackedHit(AttackHitInfo info, AttackHitColliderProcessor.HitParam hit_param)
	{
		bool disableCounterAnimEvent = disableParryAction = info.toPlayer.disableCounter;
		this.disableCounterAnimEvent = disableCounterAnimEvent;
		disableGuard = info.toPlayer.disableGuard;
		base.OnAttackedHit(info, hit_param);
		disableParryAction = false;
		disableGuard = false;
	}

	protected override int CalcDamage(AttackedHitStatusLocal status, ref AtkAttribute damage_details)
	{
		AtkAttribute atkAttribute = CalcAtk(status);
		AtkAttribute atkAttribute2 = CalcTolerance(status);
		AtkAttribute atkAttribute3 = CalcDefense(status);
		if (isAnimEventStatusUpDefence)
		{
			atkAttribute3.normal *= animEventStatusUpDefenceRate;
			atkAttribute3.AddElementOnly((playerDef + (float)baseState.defList[0] + (float)weaponState.defList[0]) * (animEventStatusUpDefenceRate - 1f));
		}
		int enemyLevel = 1;
		Enemy enemy = status.fromObject as Enemy;
		if (enemy != null)
		{
			enemyLevel = enemy.enemyLevel;
		}
		float levelRate = InGameUtility.CalcLevelRate(enemyLevel);
		damage_details.normal = (int)(atkAttribute.normal * InGameUtility.CalcDamageRateToPlayer(levelRate, atkAttribute3.normal, 0f));
		damage_details.fire = (int)(atkAttribute.fire * InGameUtility.CalcDamageRateToPlayer(levelRate, atkAttribute3.fire, atkAttribute2.fire, defenseThreshold, defenseCoefficient.fire));
		damage_details.water = (int)(atkAttribute.water * InGameUtility.CalcDamageRateToPlayer(levelRate, atkAttribute3.water, atkAttribute2.water, defenseThreshold, defenseCoefficient.water));
		damage_details.thunder = (int)(atkAttribute.thunder * InGameUtility.CalcDamageRateToPlayer(levelRate, atkAttribute3.thunder, atkAttribute2.thunder, defenseThreshold, defenseCoefficient.thunder));
		damage_details.soil = (int)(atkAttribute.soil * InGameUtility.CalcDamageRateToPlayer(levelRate, atkAttribute3.soil, atkAttribute2.soil, defenseThreshold, defenseCoefficient.soil));
		damage_details.light = (int)(atkAttribute.light * InGameUtility.CalcDamageRateToPlayer(levelRate, atkAttribute3.light, atkAttribute2.light, defenseThreshold, defenseCoefficient.light));
		damage_details.dark = (int)(atkAttribute.dark * InGameUtility.CalcDamageRateToPlayer(levelRate, atkAttribute3.dark, atkAttribute2.dark, defenseThreshold, defenseCoefficient.dark));
		damage_details.CheckMinus();
		Character character = status.fromObject as Character;
		if (character != null)
		{
			damage_details.Mul(character.buffParam.GetAbilityDamageRate(this, status));
		}
		int num = (int)damage_details.CalcTotal();
		if (_IsGuard() || spearCtrl.IsGuard())
		{
			bool num2 = GetAbsorbAtkAttribute(damage_details).CalcTotal() > 0f;
			bool flag = false;
			if (!num2 || IsValidShield())
			{
				AtkAttribute invinsibleMulRate = GetInvinsibleMulRate();
				AtkAttribute atkAttribute4 = new AtkAttribute();
				atkAttribute4.Copy(damage_details);
				atkAttribute4.Mul(invinsibleMulRate);
				flag = ((int)atkAttribute4.CalcTotal() == 0);
			}
			if (!flag)
			{
				float num3 = _GetGuardDamageCutRate();
				int num4 = Mathf.CeilToInt((float)num * (1f - num3));
				damage_details.Mul(num3);
				num = (int)((float)num * num3);
				if (CheckAttackModeAndSpType(ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE))
				{
					_AddRevengeGauge(Mathf.CeilToInt((float)num4 * playerParameter.spearActionInfo.oracle.damageConvertToSpRate));
				}
				else
				{
					_AddRevengeGauge(num);
				}
			}
		}
		num = (int)((float)num * buffParam.GetDamageDownRate());
		num = (int)((float)num * actionReceiveDamageRate);
		if (IsInSpearBurstBarrier())
		{
			num = Mathf.FloorToInt((float)num * playerParameter.spearActionInfo.burstSpearInfo.inBarrierDamageRate);
		}
		if (num < 1)
		{
			num = 1;
		}
		return num;
	}

	public override void GetAtk(AttackHitInfo info, ref AtkAttribute atk, SkillInfo.SkillParam skillParamInfo = null)
	{
		SkillInfo.SkillParam skillParam = skillParamInfo;
		if (skillParam == null)
		{
			skillParam = skillInfo.actSkillParam;
			if (base.TrackingTargetBullet != null && base.TrackingTargetBullet.IsReplaceSkill)
			{
				skillParam = base.TrackingTargetBullet.SkillParamForBullet;
			}
		}
		InGameUtility.PlayerAtkCalcData playerAtkCalcData = new InGameUtility.PlayerAtkCalcData();
		playerAtkCalcData.skillParam = skillParam;
		playerAtkCalcData.atkInfo = info;
		playerAtkCalcData.weaponAtk = base.attack;
		playerAtkCalcData.statusAtk = playerAtk;
		playerAtkCalcData.guardEquipAtk = GetGuardEquipmentAtk();
		playerAtkCalcData.buffAtkRate = buffParam.GetBuffAtkRate();
		playerAtkCalcData.passiveAtkRate = buffParam.GetPassiveAtkRate();
		playerAtkCalcData.buffAtkConstant = buffParam.GetBuffAtkConstant();
		playerAtkCalcData.buffAtkAllElementConstant = buffParam.GetValue(BuffParam.BUFFTYPE.ATTACK_ALLELEMENT);
		playerAtkCalcData.passiveAtkConstant = buffParam.GetPassiveAtkUpConstant();
		playerAtkCalcData.passiveAtkAllElementConstant = buffParam.passive.atkAllElement;
		if (CheckAttackModeAndSpType(ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL) || CheckAttackModeAndSpType(ATTACK_MODE.ARROW, SP_ATTACK_TYPE.SOUL))
		{
			playerAtkCalcData.isAtkElementOnly = true;
		}
		atk.Copy(InGameUtility.CalcPlayerATK(playerAtkCalcData));
	}

	public AtkAttribute GetGuardEquipmentAtk()
	{
		AtkAttribute atkAttribute = new AtkAttribute();
		atkAttribute.Set(0f);
		atkAttribute.normal += baseState.atkList[0];
		atkAttribute.fire += baseState.atkList[1];
		atkAttribute.water += baseState.atkList[2];
		atkAttribute.thunder += baseState.atkList[3];
		atkAttribute.soil += baseState.atkList[4];
		atkAttribute.light += baseState.atkList[5];
		atkAttribute.dark += baseState.atkList[6];
		return atkAttribute;
	}

	protected override AtkAttribute CalcDefense(AttackedHitStatusLocal status)
	{
		AtkAttribute _defence = new AtkAttribute();
		_defence.Add(base.defense);
		AtkAttribute atkAttribute = new AtkAttribute();
		atkAttribute.Set(1f);
		atkAttribute.Add(buffParam.GetBuffDefenceRate());
		_defence.Mul(atkAttribute);
		AddDefenceBuff(ref _defence);
		_defence.CheckMinus();
		return _defence;
	}

	public float GetDefForTwoHandSwordSpAttack()
	{
		EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)weaponData.eId);
		if (equipItemData == null)
		{
			return 0f;
		}
		if (equipItemData.type != EQUIPMENT_TYPE.TWO_HAND_SWORD)
		{
			return 0f;
		}
		AtkAttribute baseDefence = new AtkAttribute();
		baseDefence.normal = (float)(guardEquipDef[0] + weaponState.defList[0]) / MonoBehaviourSingleton<GlobalSettingsManager>.I.playerWeaponAttackRate[1];
		baseDefence.normal += playerDef;
		CalcDefenceBuffAndPassive(ref baseDefence, withBuff: true);
		return baseDefence.normal * 1.5f;
	}

	public float GetElementDefForTwoHandSwordHeatCombo()
	{
		EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)weaponData.eId);
		if (equipItemData == null)
		{
			return 0f;
		}
		if (equipItemData.type != EQUIPMENT_TYPE.TWO_HAND_SWORD)
		{
			return 0f;
		}
		AtkAttribute baseDefence = new AtkAttribute();
		baseDefence.normal = 0f;
		baseDefence.fire = guardEquipDef[1] + weaponState.defList[1];
		baseDefence.water = guardEquipDef[2] + weaponState.defList[2];
		baseDefence.thunder = guardEquipDef[3] + weaponState.defList[3];
		baseDefence.soil = guardEquipDef[4] + weaponState.defList[4];
		baseDefence.light = guardEquipDef[5] + weaponState.defList[5];
		baseDefence.dark = guardEquipDef[6] + weaponState.defList[6];
		baseDefence.Div(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerWeaponAttackRate[1]);
		CalcDefenceBuffAndPassive(ref baseDefence, withBuff: false);
		baseDefence.Mul(MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.heatComboElementDefRate);
		CalcDefenceOnlyBuff(ref baseDefence);
		return baseDefence.CalcTotal();
	}

	private void CalcDefenceBuffAndPassive(ref AtkAttribute baseDefence, bool withBuff)
	{
		AtkAttribute atkAttribute = new AtkAttribute();
		atkAttribute.Set(1f);
		atkAttribute.Add(buffParam.passive.defUpRate);
		atkAttribute.Sub(buffParam.passive.defDownRate);
		baseDefence.Mul(atkAttribute);
		baseDefence.normal += buffParam.passive.defList[0];
		baseDefence.fire += buffParam.passive.defList[1];
		baseDefence.water += buffParam.passive.defList[2];
		baseDefence.thunder += buffParam.passive.defList[3];
		baseDefence.soil += buffParam.passive.defList[4];
		baseDefence.light += buffParam.passive.defList[5];
		baseDefence.dark += buffParam.passive.defList[6];
		baseDefence.CheckMinus();
		if (withBuff)
		{
			atkAttribute.Set(1f);
			atkAttribute.Add(buffParam.GetBuffDefenceRate());
			baseDefence.Mul(atkAttribute);
			AddDefenceBuff(ref baseDefence);
		}
		baseDefence.CheckMinus();
	}

	private void CalcDefenceOnlyBuff(ref AtkAttribute baseDefence)
	{
		AtkAttribute atkAttribute = new AtkAttribute();
		atkAttribute.Set(1f);
		atkAttribute.Add(buffParam.GetBuffDefenceRate());
		atkAttribute.normal = 0f;
		baseDefence.Mul(atkAttribute);
		AddElementDefenceBuff(ref baseDefence);
		baseDefence.CheckMinus();
	}

	public override void OnAttackedHitOwner(AttackedHitStatusOwner status)
	{
		ApplyInvicibleCount(status);
		bool flag = ApplyInvicibleBadStatus(status);
		if (!flag)
		{
			flag = ApplyInvincibleBadStatusToRestraint(status);
		}
		if (_IsGuard() && _CheckJustGuardSec() && spAttackType == SP_ATTACK_TYPE.BURST)
		{
			status.badStatusAdd.Reset();
			flag = false;
		}
		if (IsInSpearBurstBarrier())
		{
			status.badStatusAdd.Reset();
			flag = false;
		}
		status.afterHealHp = base.hp;
		if (status.validDamage)
		{
			status.afterHealHp -= (int)Mathf.Ceil((float)status.damage * (1f - base.damageHealRate));
			if (status.afterHealHp < 0)
			{
				status.afterHealHp = 0;
			}
		}
		if (IsStone())
		{
			ActStoneEnd(stoneRescueTime);
		}
		if ((IsCoopNone() || IsOriginal()) && _IsGuard() && _CheckJustGuardSec())
		{
			if (MonoBehaviourSingleton<EffectManager>.IsValid() && spAttackType != SP_ATTACK_TYPE.BURST)
			{
				EffectManager.OneShot("ef_btl_wsk_sword_01_01", _position, _rotation);
			}
			if (EnablePlaySound() && spAttackType != SP_ATTACK_TYPE.BURST)
			{
				SoundManager.PlayOneShotSE(10000042, status.hitPos);
			}
			if (spAttackType == SP_ATTACK_TYPE.HEAT)
			{
				float heat_JustGuardSkillHealValue = playerParameter.ohsActionInfo.Heat_JustGuardSkillHealValue;
				skillInfo.AddUseGauge(heat_JustGuardSkillHealValue, set_only: true, isNotBuff: true);
			}
		}
		bool flag2 = base.hp + (int)base.ShieldHp - status.damage > 0 || IsNarrowEscape(status);
		bool flag3 = IsAntiGrabAndRestraint();
		AttackHitInfo attackInfo = status.attackInfo;
		GrabInfo grabInfo = attackInfo.grabInfo;
		if (grabInfo.enable && flag2 && !flag3)
		{
			ActGrabbedStart(status.fromObjectID, grabInfo);
			status.reactionType = 0;
		}
		if (attackInfo.restraintInfo.enable && flag2 && base.actionID != (ACTION_ID)30 && !flag3 && !IsAntiRestraint())
		{
			ActRestraint(attackInfo.restraintInfo);
			status.reactionType = 0;
		}
		if (IsValidShield())
		{
			AtkAttribute invinsibleMulRate = GetInvinsibleMulRate();
			status.damageDetails.Mul(invinsibleMulRate);
			status.damage = (int)status.damageDetails.CalcTotal();
			ShieldDamageData shieldDamageData = CalcShieldDamage(status.damage);
			status.damage = shieldDamageData.hpDamage;
			status.shieldDamage = shieldDamageData.shieldDamage;
			if (shieldDamageData.shieldDamage > 0 && IsValidBuff(BuffParam.BUFFTYPE.SHIELD_REFLECT) && shieldReflectInfo != null)
			{
				int num = shieldDamageData.shieldDamage;
				if (IsValidBuff(BuffParam.BUFFTYPE.SHIELD_REFLECT_DAMAGE_UP))
				{
					num *= buffParam.GetValue(BuffParam.BUFFTYPE.SHIELD_REFLECT_DAMAGE_UP);
				}
				shieldReflectInfo.damage = num;
				shieldReflectInfo.targetId = status.fromObjectID;
				OnShotShieldReflect(shieldReflectInfo);
			}
			if (IsValidBuff(BuffParam.BUFFTYPE.SHIELD_INVINCIBLE_BADSTATUS))
			{
				status.badStatusAdd.Reset();
				flag = false;
			}
		}
		ResetBoostPowerUpTriggerDamage();
		if (attackInfo.toPlayer.isBuffCancellation)
		{
			OnBuffCancellation();
		}
		if (flag)
		{
			buffParam.DecreaseInvincibleBadStatus();
		}
		base.OnAttackedHitOwner(status);
		isAbsorbDamageSuperArmor = false;
	}

	protected override bool ApplyInvicibleBadStatus(AttackedHitStatusOwner status)
	{
		if (!buffParam.IsValidBuff(BuffParam.BUFFTYPE.INVINCIBLE_BADSTATUS))
		{
			return false;
		}
		if (!status.badStatusAdd.isExist())
		{
			return false;
		}
		status.badStatusAdd.Reset();
		return true;
	}

	protected override bool IsNarrowEscape(AttackedHitStatusOwner status)
	{
		if (timeWhenJustGuardChecked == Time.time)
		{
			return true;
		}
		if (status.afterHP > 0)
		{
			return false;
		}
		if (CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.BURST) && _IsGuard() && _CheckJustGuardSec())
		{
			timeWhenJustGuardChecked = Time.time;
			return true;
		}
		if (CheckAttackModeAndSpType(ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE) && spearCtrl.CanConsumeOracleStock())
		{
			return true;
		}
		return buffParam.IsNarrowEscape();
	}

	protected bool IsAntiGrabAndRestraint()
	{
		if (CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.BURST) && _IsGuard() && _CheckJustGuardSec())
		{
			return true;
		}
		if (IsInAliveBarrier())
		{
			return true;
		}
		return false;
	}

	protected bool IsAntiRestraint()
	{
		if (CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.BURST) && _IsGuard() && _CheckJustGuardSec())
		{
			return true;
		}
		if (IsInAliveBarrier())
		{
			return true;
		}
		if (IsInSpearBurstBarrier())
		{
			return true;
		}
		if (buffParam.IsValidBuff(BuffParam.BUFFTYPE.INVINCIBLE_BADSTATUS))
		{
			return true;
		}
		if (buffParam.IsValidBuff(BuffParam.BUFFTYPE.SHIELD_INVINCIBLE_BADSTATUS))
		{
			return true;
		}
		if (buffParam.IsValidInvincibleCountBuff())
		{
			return true;
		}
		return false;
	}

	protected bool ApplyInvincibleBadStatusToRestraint(AttackedHitStatusOwner status)
	{
		if (!status.attackInfo.restraintInfo.enable)
		{
			return false;
		}
		if (!buffParam.IsValidBuff(BuffParam.BUFFTYPE.INVINCIBLE_BADSTATUS))
		{
			return false;
		}
		return true;
	}

	protected override void UseNarrowEscape(AttackedHitStatusOwner status)
	{
		if (timeWhenJustGuardChecked != Time.time)
		{
			if (buffParam.IsNarrowEscape())
			{
				buffParam.UseNarrowEscape();
			}
			else if (CheckAttackModeAndSpType(ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE) && spearCtrl.CanConsumeOracleStock())
			{
				spearCtrl.StartOracleGutsMode();
			}
		}
	}

	protected override bool IsHitReactionValid(AttackedHitStatusOwner status)
	{
		if (status.fromType == OBJECT_TYPE.PLAYER && !playerParameter.playerHitReactionValid)
		{
			return false;
		}
		if (base.IsHitReactionValid(status))
		{
			return status.validDamage;
		}
		return false;
	}

	protected override REACTION_TYPE OnHitReaction(AttackedHitStatusOwner status)
	{
		if (_IsGuard())
		{
			Vector3 fromPos = status.fromPos;
			fromPos.y = _position.y;
			_LookAt(fromPos);
			return REACTION_TYPE.GUARD_DAMAGE;
		}
		REACTION_TYPE rEACTION_TYPE = REACTION_TYPE.NONE;
		bool flag = false;
		bool flag2 = false;
		switch (status.attackInfo.toPlayer.reactionType)
		{
		case AttackHitInfo.ToPlayer.REACTION_TYPE.DAMAGE:
			if (!IsValidSuperArmor())
			{
				rEACTION_TYPE = REACTION_TYPE.DAMAGE;
				flag = true;
			}
			break;
		case AttackHitInfo.ToPlayer.REACTION_TYPE.BLOW:
			if (!IsValidSuperArmor())
			{
				rEACTION_TYPE = REACTION_TYPE.BLOW;
				flag = true;
				flag2 = true;
			}
			break;
		case AttackHitInfo.ToPlayer.REACTION_TYPE.STUNNED_BLOW:
			if (!IsValidSuperArmor())
			{
				rEACTION_TYPE = REACTION_TYPE.STUNNED_BLOW;
				flag = true;
				flag2 = true;
			}
			break;
		case AttackHitInfo.ToPlayer.REACTION_TYPE.STUMBLE:
			if (!IsValidSuperArmor())
			{
				rEACTION_TYPE = REACTION_TYPE.STUMBLE;
				flag = true;
			}
			break;
		case AttackHitInfo.ToPlayer.REACTION_TYPE.FALL_BLOW:
			if (!IsValidSuperArmor())
			{
				rEACTION_TYPE = REACTION_TYPE.FALL_BLOW;
				flag = true;
				flag2 = true;
			}
			break;
		case AttackHitInfo.ToPlayer.REACTION_TYPE.SHAKE:
			if (!IsValidSuperArmor())
			{
				rEACTION_TYPE = REACTION_TYPE.SHAKE;
			}
			break;
		case AttackHitInfo.ToPlayer.REACTION_TYPE.CHARM_BLOW:
			if (!IsValidSuperArmor())
			{
				rEACTION_TYPE = REACTION_TYPE.CHARM_BLOW;
				flag = true;
				flag2 = true;
			}
			break;
		}
		if (flag)
		{
			Vector3 fromPos2 = status.fromPos;
			fromPos2.y = _position.y;
			_LookAt(fromPos2);
		}
		if (flag2)
		{
			Vector3 point = -_forward;
			point = Quaternion.AngleAxis(status.attackInfo.toPlayer.reactionBlowAngle, _right) * point;
			point = (status.blowForce = point * status.attackInfo.toPlayer.reactionBlowForce);
		}
		if (rEACTION_TYPE != 0)
		{
			return rEACTION_TYPE;
		}
		return REACTION_TYPE.NONE;
	}

	private bool IsValidSuperArmor()
	{
		if (IsRestraint() || IsParalyze() || IsStone())
		{
			return false;
		}
		if (!enableSuperArmor && !IsValidBuff(BuffParam.BUFFTYPE.SUPER_ARMOR) && !IsValidBuff(BuffParam.BUFFTYPE.SHIELD_SUPER_ARMOR) && !buffParam.IsValidInvincibleCountBuff() && !IsValidBuff(BuffParam.BUFFTYPE.INVINCIBLE_BUFF_CANCELLATION_EXPAND) && !isAbsorbDamageSuperArmor && !IsInSpearBurstBarrier() && !isInvincibleDamageSuperArmor)
		{
			return spearCtrl.IsGuard();
		}
		return true;
	}

	protected override REACTION_TYPE CheckReActionTolerance(AttackedHitStatusOwner status)
	{
		REACTION_TYPE result = base.CheckReActionTolerance(status);
		if (isActSkillAction && buffParam.IsInvalidReaction(BuffParam.TOLERANCETYPE.SKILL))
		{
			result = REACTION_TYPE.NONE;
		}
		return result;
	}

	public override void OnAttackedHitFix(AttackedHitStatusFix status)
	{
		bool isDead = base.isDead;
		base.OnAttackedHitFix(status);
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopStage.battleUserLog.Add(this, status);
		}
		if (!isDead)
		{
			if (status.reactionType != 8)
			{
				healHp = status.afterHealHp;
			}
			else
			{
				healHp = 0;
			}
			int i = 0;
			for (int count = m_weaponCtrlList.Count; i < count; i++)
			{
				m_weaponCtrlList[i].OnAttackedHitFix(status);
			}
			if (status.damage != 0 || status.shieldDamage != 0)
			{
				pairSwordsCtrl.DecreaseSoulGaugeByDamage();
			}
		}
	}

	public override void ActReaction(ReactionInfo info, bool isSync = false)
	{
		base.ActReaction(info, isSync);
		switch (info.reactionType)
		{
		case REACTION_TYPE.BLOW:
			ActBlow(info.blowForce);
			break;
		case REACTION_TYPE.STUNNED_BLOW:
			ActStunnedBlow(info.blowForce, info.loopTime);
			break;
		case REACTION_TYPE.STUMBLE:
			ActStumble(info.loopTime);
			break;
		case REACTION_TYPE.FALL_BLOW:
			ActFallBlow(info.blowForce);
			break;
		case REACTION_TYPE.SHAKE:
			ActShake();
			break;
		case REACTION_TYPE.GUARD_DAMAGE:
			ActGuardDamage();
			break;
		case REACTION_TYPE.STONE:
			ActStone();
			break;
		case REACTION_TYPE.CHARM_BLOW:
			ActCharmBlow(info.blowForce, info.loopTime);
			break;
		}
	}

	public override string EffectNameAnalyzer(string effect_name)
	{
		if (!string.IsNullOrEmpty(effect_name) && effect_name[0] == '@')
		{
			SkillInfo.SkillParam actSkillParam = skillInfo.actSkillParam;
			if (actSkillParam == null)
			{
				Log.Error(LOG.INGAME, "EffectNameAnalyzer skill param is null.");
				return null;
			}
			if (actSkillParam.tableData == null)
			{
				Log.Error(LOG.INGAME, "EffectNameAnalyzer skill param table data is null.");
				return null;
			}
			if (effect_name == "@skill_start")
			{
				return actSkillParam.tableData.startEffectName;
			}
			if (effect_name == "@skill_act_local")
			{
				return actSkillParam.tableData.actLocalEffectName;
			}
			if (effect_name == "@skill_act_oneshot")
			{
				return actSkillParam.tableData.actOneshotEffectName;
			}
			if (effect_name == "@skill_enchant")
			{
				return actSkillParam.tableData.enchantEffectName;
			}
		}
		return effect_name;
	}

	public override Transform FindNode(string name)
	{
		if (name == "weaponR")
		{
			return loader.wepR;
		}
		if (name == "weaponL")
		{
			return loader.wepL;
		}
		return base.FindNode(name);
	}

	protected void IgnoreEnemyColliders()
	{
		List<Collider> list = new List<Collider>();
		int i = 0;
		for (int count = MonoBehaviourSingleton<StageObjectManager>.I.enemyList.Count; i < count; i++)
		{
			Enemy enemy = MonoBehaviourSingleton<StageObjectManager>.I.enemyList[i] as Enemy;
			if (enemy != null && enemy.colliders != null)
			{
				list.AddRange(enemy.colliders);
			}
		}
		if (list.Count > 0)
		{
			IgnoreColliders(list.ToArray());
		}
	}

	public override bool CanPlayEffectEvent()
	{
		if (skillInfo.actSkillParam == null)
		{
			return false;
		}
		return true;
	}

	public override void OnAnimEvent(AnimEventData.EventData data)
	{
		switch (data.id)
		{
		case AnimEventFormat.ID.TARGET_LOCK_ON:
		case AnimEventFormat.ID.TARGET_LOCK_OFF:
		case AnimEventFormat.ID.TWO_HAND_SWORD_BURST_BASE_ATK:
		case AnimEventFormat.ID.CAMERA_RESET_POSITION:
			break;
		case AnimEventFormat.ID.SE_SKILL_ONESHOT:
		{
			SkillInfo.SkillParam actSkillParam = skillInfo.actSkillParam;
			if (actSkillParam == null || actSkillParam.tableData == null)
			{
				Log.Error(LOG.INGAME, "SE_SKILL_ONESHOT skill param none.");
				break;
			}
			int num8 = 0;
			switch (data.intArgs[0])
			{
			case 0:
				num8 = actSkillParam.tableData.startSEID;
				break;
			case 1:
				num8 = actSkillParam.tableData.actSEID;
				break;
			}
			string name = data.stringArgs[0];
			if (num8 == 0)
			{
				base.OnAnimEvent(data);
			}
			else if (EnablePlaySound())
			{
				SoundManager.PlayOneShotSE(num8, this, FindNode(name));
			}
			break;
		}
		case AnimEventFormat.ID.VOICE:
		{
			int voice_id = data.intArgs[0];
			PlayVoice(voice_id);
			break;
		}
		case AnimEventFormat.ID.SHOT_ARROW:
		{
			bool flag = false;
			int num10 = 0;
			if (data.intArgs != null)
			{
				if (data.intArgs.Length != 0)
				{
					flag = ((data.intArgs[0] > 0) ? true : false);
				}
				if (data.intArgs.Length > 1)
				{
					num10 = data.intArgs[1];
				}
			}
			string text = playerParameter.arrowActionInfo.attackInfoNames[(int)spAttackType];
			if (data.stringArgs != null && data.stringArgs.Length != 0)
			{
				if (flag)
				{
					text = playerParameter.arrowActionInfo.attackInfoForSitShotNames[(int)spAttackType] + num10.ToString();
				}
				if (spAttackType == SP_ATTACK_TYPE.BURST && isBoostMode && !flag)
				{
					text = playerParameter.arrowActionInfo.boostArrowChargeMaxAttackInfoName;
				}
			}
			AttackInfo attackInfo = FindAttackInfo(text);
			shotArrowCount++;
			if (attackInfo == null || attackMode != ATTACK_MODE.ARROW)
			{
				break;
			}
			float num11;
			if (defaultBulletSpeedDic.ContainsKey(text))
			{
				num11 = defaultBulletSpeedDic[text];
			}
			else
			{
				num11 = attackInfo.bulletData.data.speed;
				defaultBulletSpeedDic.Add(text, attackInfo.bulletData.data.speed);
			}
			if (flag)
			{
				num11 *= 1f + MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.sitShotBulletSpeedUpRate;
			}
			attackInfo.bulletData.data.speed = num11;
			if (!IsCoopNone() && !IsOriginal())
			{
				break;
			}
			Vector3 bulletAppearPos = GetBulletAppearPos();
			Quaternion shot_rot = Quaternion.LookRotation(GetBulletShotVec(bulletAppearPos));
			bool isAimEnd = !flag || num10 == 3;
			if (CheckAttackModeAndSpType(ATTACK_MODE.ARROW, SP_ATTACK_TYPE.HEAT) && flag && MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
			{
				float sitShotSideAngle = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.sitShotSideAngle;
				if (num10 > 1)
				{
					float num12 = (num10 % 2 != 1) ? 1 : (-1);
					shot_rot *= Quaternion.Euler(0f, num12 * sitShotSideAngle, 0f);
				}
			}
			if (CheckAttackModeAndSpType(ATTACK_MODE.ARROW, SP_ATTACK_TYPE.BURST) && isBoostMode)
			{
				isAimEnd = (num10 == 3);
			}
			ShotArrow(bulletAppearPos, shot_rot, attackInfo, flag, isAimEnd);
			break;
		}
		case AnimEventFormat.ID.SHOT_SOUL_ARROW:
			if (CheckAttackModeAndSpType(ATTACK_MODE.ARROW, SP_ATTACK_TYPE.SOUL) && (IsCoopNone() || IsOriginal()))
			{
				ShotSoulArrow();
			}
			break;
		case AnimEventFormat.ID.ARROW_AIMABLE_START:
			isArrowAimable = true;
			break;
		case AnimEventFormat.ID.ARROW_AIMABLE_END:
			isArrowAimable = false;
			break;
		case AnimEventFormat.ID.COMBO_INPUT_ON:
		{
			int num4 = data.intArgs[0];
			enableInputCombo = true;
			controllerInputCombo = true;
			inputComboID = num4;
			inputComboMotionState = ((data.stringArgs.Length != 0) ? data.stringArgs[0] : "");
			break;
		}
		case AnimEventFormat.ID.COMBO_INPUT_OFF:
			enableInputCombo = false;
			controllerInputCombo = false;
			break;
		case AnimEventFormat.ID.COMBO_TRANSITION_ON:
			enableComboTrans = true;
			if (inputComboFlag)
			{
				ActAttackCombo();
			}
			break;
		case AnimEventFormat.ID.BOOSTCOMBO_TRANSITION_ON:
			if ((data.stringArgs.Length == 0 || string.IsNullOrEmpty(data.stringArgs[0]) || (SP_ATTACK_TYPE)Enum.Parse(typeof(SP_ATTACK_TYPE), data.stringArgs[0]) == spAttackType) && isBoostMode)
			{
				enableComboTrans = true;
				if (inputComboFlag)
				{
					ActAttackCombo();
				}
			}
			break;
		case AnimEventFormat.ID.BLOW_CLEAR_INPUT_ON:
		{
			int num7 = data.intArgs[0];
			enableInputCombo = true;
			inputComboID = num7;
			break;
		}
		case AnimEventFormat.ID.BLOW_CLEAR_INPUT_OFF:
			enableInputCombo = false;
			break;
		case AnimEventFormat.ID.BLOW_CLEAR_TRANSITION_ON:
			if (inputBlowClearFlag)
			{
				SetBlowClear(inputComboID);
			}
			break;
		case AnimEventFormat.ID.SUPERARMOR_ON:
			enableSuperArmor = true;
			break;
		case AnimEventFormat.ID.SUPERARMOR_OFF:
			enableSuperArmor = false;
			break;
		case AnimEventFormat.ID.CHARGE_INPUT_START:
		{
			float num13 = data.floatArgs[0];
			float num14 = data.floatArgs[1];
			bool flag2 = (data.intArgs[0] != 0) ? true : false;
			if (data.intArgs != null && data.intArgs.Length > 1)
			{
				isArrowSitShot = ((data.intArgs[1] > 0) ? true : false);
			}
			float num15 = 0f;
			switch (attackMode)
			{
			case ATTACK_MODE.TWO_HAND_SWORD:
				num15 = ((spAttackType == SP_ATTACK_TYPE.ORACLE) ? GetOracleChargeTimeRate() : buffParam.GetChargeSwordsTimeRate());
				break;
			case ATTACK_MODE.ARROW:
				num15 = GetChargeArrowTimeRate();
				break;
			case ATTACK_MODE.PAIR_SWORDS:
				num15 = buffParam.GetChargePairSwordsTimeRate();
				break;
			case ATTACK_MODE.SPEAR:
				switch (spAttackType)
				{
				case SP_ATTACK_TYPE.NONE:
					num14 = 0f;
					num13 = playerParameter.spearActionInfo.exRushValidSec;
					if (evolveCtrl.IsExecLeviathan())
					{
						num15 = 1f;
					}
					break;
				case SP_ATTACK_TYPE.SOUL:
					num15 = buffParam.GetChargeSpearTimeRate();
					break;
				case SP_ATTACK_TYPE.ORACLE:
					num15 = spearCtrl.GetOracleSpChargeTimeRate(buffParam.GetChargeSpearTimeRate());
					break;
				}
				break;
			}
			if (data.floatArgs.Length == 4)
			{
				isInputChargeExistOffset = true;
				num13 = data.floatArgs[2];
				num14 = data.floatArgs[3];
				if (num15 >= 1f)
				{
					num15 = 1f;
				}
				num13 = (num13 - num14) * (1f - num15) + num14;
			}
			else if (IsValidBuff(BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS))
			{
				isInputChargeExistOffset = false;
				num13 = 0f;
			}
			else
			{
				isInputChargeExistOffset = false;
				num13 *= 1f - num15;
			}
			enableInputCharge = true;
			inputChargeAutoRelease = flag2;
			inputChargeMaxTiming = true;
			inputChargeTimeMax = num13;
			inputChargeTimeOffset = num14;
			inputChargeTimeCounter = 0f;
			chargeRate = 0f;
			isChargeExRush = false;
			exRushChargeRate = 0f;
			StartWaitingPacket(WAITING_PACKET.PLAYER_CHARGE_RELEASE, keep_sync: true);
			CheckInputCharge();
			break;
		}
		case AnimEventFormat.ID.SKILL_CAST_LOOP_START:
		{
			string value = (data.stringArgs.Length != 0) ? data.stringArgs[0] : null;
			if (string.IsNullOrEmpty(value))
			{
				value = "next";
			}
			if (!isSkillCastLoop && skillInfo.actSkillParam != null && skillInfo.actSkillParam.tableData != null)
			{
				isSkillCastLoop = true;
				skillCastLoopStartTime = Time.time;
				skillCastLoopTrigger = value;
				XorFloat castTimeRate = skillInfo.actSkillParam.castTimeRate;
				float num6 = 1f - (buffParam.GetSkillTimeRate() + (float)castTimeRate);
				if (num6 <= 0f)
				{
					num6 = 0.1f;
				}
				skillCastLoopTime = skillInfo.actSkillParam.tableData.castTime * num6;
				CheckSkillCastLoop();
			}
			break;
		}
		case AnimEventFormat.ID.ROTATE_TO_TARGET_START:
		case AnimEventFormat.ID.ROTATE_KEEP_TO_TARGET_START:
		case AnimEventFormat.ID.ROTATE_TO_ANGLE_START:
		case AnimEventFormat.ID.ROTATE_TO_TARGET_OFFSET:
			if (!startInputRotate && !isArrowAimBossMode && !isArrowAimLesserMode && !IsValidBuffBlind())
			{
				base.OnAnimEvent(data);
			}
			break;
		case AnimEventFormat.ID.ROTATE_TO_TARGET_POINT_START:
			EventRotateToTargetPointStart();
			break;
		case AnimEventFormat.ID.ROTATE_INPUT_ON:
			enableInputRotate = true;
			startInputRotate = false;
			break;
		case AnimEventFormat.ID.ROTATE_INPUT_OFF:
			EventRotateInputOff(data);
			break;
		case AnimEventFormat.ID.CANCEL_TO_AVOID_ON:
			enableCancelToAvoid = true;
			break;
		case AnimEventFormat.ID.CANCEL_TO_AVOID_OFF:
			enableCancelToAvoid = false;
			break;
		case AnimEventFormat.ID.CANCEL_TO_MOVE_ON:
			enableCancelToMove = true;
			break;
		case AnimEventFormat.ID.CANCEL_TO_MOVE_OFF:
			enableCancelToMove = false;
			break;
		case AnimEventFormat.ID.CANCEL_TO_ATTACK_ON:
			enableCancelToAttack = true;
			break;
		case AnimEventFormat.ID.CANCEL_TO_ATTACK_OFF:
			enableCancelToAttack = false;
			break;
		case AnimEventFormat.ID.CANCEL_TO_SKILL_ON:
			enableCancelToSkill = true;
			break;
		case AnimEventFormat.ID.CANCEL_TO_SKILL_OFF:
			enableCancelToSkill = false;
			break;
		case AnimEventFormat.ID.CANCEL_TO_SPECIAL_ACTION_ON:
			enableCancelToSpecialAction = true;
			break;
		case AnimEventFormat.ID.CANCEL_TO_SPECIAL_ACTION_OFF:
			enableCancelToSpecialAction = false;
			break;
		case AnimEventFormat.ID.COUNTER_ATTACK_ON:
			if (disableCounterAnimEvent)
			{
				disableCounterAnimEvent = false;
			}
			else
			{
				enableCounterAttack = true;
			}
			break;
		case AnimEventFormat.ID.COUNTER_ATTACK_OFF:
			enableCounterAttack = false;
			break;
		case AnimEventFormat.ID.ENABLE_ANIM_RATE_ON:
			enableAnimSeedRate = true;
			UpdateAnimatorSpeed();
			break;
		case AnimEventFormat.ID.ENABLE_ANIM_RATE_OFF:
			enableAnimSeedRate = false;
			UpdateAnimatorSpeed();
			break;
		case AnimEventFormat.ID.CANCEL_TO_EVOLVE_SPECIAL_ACTION_ON:
			enableCancelToEvolveSpecialAction = true;
			break;
		case AnimEventFormat.ID.CANCEL_TO_EVOLVE_SPECIAL_ACTION_OFF:
			enableCancelToEvolveSpecialAction = false;
			break;
		case AnimEventFormat.ID.FACE:
			if (loader != null)
			{
				loader.ChangeFace((PlayerLoader.FACE_ID)data.intArgs[0]);
			}
			break;
		case AnimEventFormat.ID.STUNNED_LOOP_START:
			isStunnedLoop = true;
			stunnedEndTime = Time.time + stunnedTime;
			stunnedReduceEnableTime = stunnedTime * playerParameter.stunnedReduceTimeMaxRate;
			break;
		case AnimEventFormat.ID.APPLY_SKILL_PARAM:
			ApplySkillParam();
			break;
		case AnimEventFormat.ID.APPLY_BLOW_FORCE:
			base.waitAddForce = false;
			SetVelocity(Vector3.zero);
			break;
		case AnimEventFormat.ID.APPLY_CHANGE_WEAPON:
			if (IsCoopNone() || IsOriginal())
			{
				ApplyChangeWeapon(changeWeaponItem, changeWeaponIndex, changePlayerInfo);
			}
			else if (IsValidWaitingPacket(WAITING_PACKET.PLAYER_APPLY_CHANGE_WEAPON))
			{
				Log.Error(LOG.INGAME, "Player APPLY_CHANGE_WEAPON Err. ( StartWaitingPacket already. )");
			}
			else
			{
				StartWaitingPacket(WAITING_PACKET.PLAYER_APPLY_CHANGE_WEAPON, keep_sync: false);
			}
			break;
		case AnimEventFormat.ID.APPLY_GATHER:
			if (IsCoopNone() || IsOriginal())
			{
				ApplyGather();
			}
			break;
		case AnimEventFormat.ID.PLAYER_FUNNEL_ATTACK:
			EventPlayerFunnelAttack(data);
			break;
		case AnimEventFormat.ID.RUSH_CAN_RELEASE:
			isCanRushRelease = true;
			break;
		case AnimEventFormat.ID.RUSH_LOOP_START:
			isLoopingRush = true;
			break;
		case AnimEventFormat.ID.SPEAR_HUNDRED_START:
			if (!isSpearHundred)
			{
				isSpearHundred = true;
				spearHundredSecFromStart = 0f;
				spearHundredSecFromLastTap = 0f;
			}
			break;
		case AnimEventFormat.ID.SP_ATTACK_CONTINUE_ON:
			enableSpAttackContinue = true;
			break;
		case AnimEventFormat.ID.SP_ATTACK_CONTINUE_OFF:
			enableSpAttackContinue = false;
			break;
		case AnimEventFormat.ID.TWO_HAND_SWORD_CHARGE_EXPAND_START:
			if (!isChargeExpanding)
			{
				isChargeExpanding = true;
				timerChargeExpandOffset = 0f;
				isInputChargeExistOffset = false;
				float num2 = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.timeChargeExpandMax;
				float num3 = buffParam.GetChargeSwordsTimeRate();
				if (num3 > 1f)
				{
					num3 = 1f;
				}
				num2 *= 1f - num3;
				if (num2 < MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.minTimeChargeExpandMax)
				{
					timerChargeExpandOffset = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.minTimeChargeExpandMax - num2;
					num2 = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.minTimeChargeExpandMax;
				}
				isChargeExpandAutoRelease = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.isChargeExpandAutoRelease;
				timeChargeExpandMax = num2;
				timerChargeExpand = 0f;
				chargeExpandRate = 0f;
				StartWaitingPacket(WAITING_PACKET.PLAYER_CHARGE_RELEASE, keep_sync: true);
				CheckChargeExpand();
			}
			break;
		case AnimEventFormat.ID.TWO_HAND_SWORD_CHARGE_SOUL_START:
			if (CheckAttackModeAndSpType(ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.SOUL) && !enableInputCharge)
			{
				enableInputCharge = true;
				InGameSettingsManager.Player.TwoHandSwordActionInfo twoHandSwordActionInfo = playerParameter.twoHandSwordActionInfo;
				float num = buffParam.GetSoulChargeTimeRate();
				if (MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.ignoreSpAttackTypeAbility)
				{
					num += buffParam.GetChargeSwordsTimeRate();
				}
				float soulIaiChargeTime = twoHandSwordActionInfo.soulIaiChargeTime;
				soulIaiChargeTime *= 1f - num;
				if (soulIaiChargeTime < twoHandSwordActionInfo.soulIaiChargeTimeMin)
				{
					soulIaiChargeTime = twoHandSwordActionInfo.soulIaiChargeTimeMin;
				}
				inputChargeAutoRelease = false;
				inputChargeMaxTiming = true;
				inputChargeTimeMax = soulIaiChargeTime;
				inputChargeTimeOffset = 0f;
				inputChargeTimeCounter = 0f;
				isInputChargeExistOffset = false;
				chargeRate = 0f;
				StartWaitingPacket(WAITING_PACKET.PLAYER_CHARGE_RELEASE, keep_sync: true);
				CheckInputCharge();
			}
			break;
		case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE:
		{
			if (jumpState == eJumpState.None)
			{
				CreateAttackCollider(data);
				break;
			}
			InGameSettingsManager.Player.SpearActionInfo spearActionInfo3 = MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo;
			AnimEventData.EventData eventData = new AnimEventData.EventData();
			eventData.Copy(data, with_time: true);
			eventData.stringArgs[0] = spearActionInfo3.jumpWaveAttackInfoPrefix + useGaugeLevel;
			eventData.floatArgs[6] = spearActionInfo3.jumpWaveColliderRadius[useGaugeLevel];
			CreateAttackCollider(eventData);
			break;
		}
		case AnimEventFormat.ID.SPEAR_JUMP_CHARGE_START:
			if (!enableInputCharge)
			{
				enableInputCharge = true;
				isArrowAimable = true;
				isSpearJumpAim = true;
				jumpState = eJumpState.Charge;
				InGameSettingsManager.Player.SpearActionInfo spearActionInfo2 = MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo;
				float jumpChargeBaseSec = spearActionInfo2.jumpChargeBaseSec;
				float chargeSpearTimeRate = buffParam.GetChargeSpearTimeRate();
				jumpChargeBaseSec *= 1f - chargeSpearTimeRate;
				if (jumpChargeBaseSec < spearActionInfo2.jumpChargeMinSec)
				{
					jumpChargeBaseSec = spearActionInfo2.jumpChargeMinSec;
				}
				inputChargeAutoRelease = false;
				inputChargeMaxTiming = true;
				inputChargeTimeMax = jumpChargeBaseSec;
				inputChargeTimeOffset = 0f;
				inputChargeTimeCounter = 0f;
				isInputChargeExistOffset = false;
				chargeRate = 0f;
				isChargeExRush = false;
				exRushChargeRate = 0f;
				StartWaitingPacket(WAITING_PACKET.PLAYER_CHARGE_RELEASE, keep_sync: true);
				CheckInputCharge();
			}
			break;
		case AnimEventFormat.ID.SPEAR_JUMP_FALL_WAIT:
		{
			if ((object)base.body == null)
			{
				ActIdle();
				break;
			}
			InGameSettingsManager.Player.SpearActionInfo spearActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo;
			Vector3 vector = new Vector3(_position.x + jumpFallBodyPosition.x, _position.y, _position.z + jumpFallBodyPosition.z);
			jumpRandingVector = (_position - vector).normalized * spearActionInfo.jumpRandingLength;
			_position = vector;
			jumpFallBodyPosition.Set(0f, spearActionInfo.jumpStartHeight, 0f);
			base.body.transform.localPosition = jumpFallBodyPosition;
			jumpActionCounter = spearActionInfo.jumpFallWaitSec;
			jumpState = eJumpState.FallWait;
			break;
		}
		case AnimEventFormat.ID.SE_ONESHOT_DEPEND_WEAPON_ELEMENT:
		{
			int currentWeaponElement = GetCurrentWeaponElement();
			if (currentWeaponElement <= 6 && data.intArgs.Length > currentWeaponElement && data.intArgs[currentWeaponElement] != 0)
			{
				string name2 = string.Empty;
				if (data.stringArgs.Length != 0)
				{
					name2 = data.stringArgs[0];
				}
				int num9 = data.intArgs[currentWeaponElement];
				if (num9 != 0 && EnablePlaySound())
				{
					SoundManager.PlayOneShotSE(num9, this, FindNode(name2));
				}
			}
			break;
		}
		case AnimEventFormat.ID.WARP_VIEW_START:
			SetEnableNodeRenderer(string.Empty, enable: false);
			Utility.SetLayerWithChildren(base._transform, 16, 12);
			DeactivateStoredEffect();
			break;
		case AnimEventFormat.ID.WARP_VIEW_END:
			SetEnableNodeRenderer(string.Empty, enable: true);
			Utility.SetLayerWithChildren(base._transform, 8, 12);
			ActivateStoredEffect();
			break;
		case AnimEventFormat.ID.SKIP_TO_SKILL_ACTION_ON:
			isAbleToSkipSkillAction = true;
			break;
		case AnimEventFormat.ID.SKIP_TO_SKILL_ACTION_OFF:
			isAbleToSkipSkillAction = false;
			break;
		case AnimEventFormat.ID.CHANGE_SKILL_TO_SECOND_GRADE:
			if (data.intArgs.Length != 0)
			{
				if (data.intArgs[0] == 1)
				{
					Log.Error("CHANGE_SKILL_TO_SECOND_GRADEに「1」は設定できません");
				}
				else if (isUsingSecondGradeSkill)
				{
					int nextTrigger = data.intArgs[0] - 1;
					SetNextTrigger(nextTrigger);
					isUsingSecondGradeSkill = false;
					skillInfo.ResetSecondGradeFlags();
				}
			}
			break;
		case AnimEventFormat.ID.PAIR_SWORDS_SHOT_BULLET:
			EventPairSwordsShotBullet(data);
			break;
		case AnimEventFormat.ID.PAIR_SWORDS_SHOT_LASER:
			EventPairSwordsShotLaser(data);
			break;
		case AnimEventFormat.ID.PAIR_SWORDS_SOUL_EFFECT_START_SHOT_LASER:
			pairSwordsCtrl.GetEffectTransStartShotLaser();
			break;
		case AnimEventFormat.ID.CANCEL_TO_ATTACK_NEXT_ON:
			enableAttackNext = true;
			break;
		case AnimEventFormat.ID.CANCEL_TO_ATTACK_NEXT_OFF:
			enableAttackNext = false;
			break;
		case AnimEventFormat.ID.SHOT_HEALING_HOMING:
			EventShotHealingHoming(data);
			break;
		case AnimEventFormat.ID.SPEAR_SOUL_HEAL_HP:
			EventHealHp();
			break;
		case AnimEventFormat.ID.TWO_HAND_SWORD_BURST_FULL_BURST:
			if (thsCtrl != null)
			{
				thsCtrl.DoFullBurst();
			}
			break;
		case AnimEventFormat.ID.TWO_HAND_SWORD_BURST_READY_FOR_SHOT:
			if (thsCtrl != null && !thsCtrl.IsReadyForShoot)
			{
				thsCtrl.SetReadyForShoot(_isReady: true);
			}
			break;
		case AnimEventFormat.ID.TWO_HAND_SWORD_BURST_SINGLE_SHOT:
			if (thsCtrl != null)
			{
				thsCtrl.DoShootAction();
			}
			break;
		case AnimEventFormat.ID.TWO_HAND_SWORD_BURST_START_RELOAD:
			if (thsCtrl != null && !thsCtrl.IsReloadingNow)
			{
				thsCtrl.SetStartReloading(_isReadyForReload: true);
			}
			break;
		case AnimEventFormat.ID.TWO_HAND_SWORD_BURST_RELOAD_NOW:
			if (thsCtrl != null && thsCtrl.IsReloadingNow)
			{
				thsCtrl.DoReloadAction();
			}
			break;
		case AnimEventFormat.ID.TWO_HAND_SWORD_BURST_RELOAD_DONE:
			if (thsCtrl != null && thsCtrl.IsReloadingNow)
			{
				thsCtrl.CheckEnableNextReloadAction();
			}
			break;
		case AnimEventFormat.ID.THS_BURST_RELOAD_VARIABLE_SPEED_ON:
			if (thsCtrl != null)
			{
				thsCtrl.SetChangeReloadMotionSpeedFlag(_isEnable: true);
				enableAnimSeedRate = true;
				UpdateAnimatorSpeed();
			}
			break;
		case AnimEventFormat.ID.THS_BURST_RELOAD_VARIABLE_SPEED_OFF:
			if (thsCtrl != null)
			{
				thsCtrl.SetChangeReloadMotionSpeedFlag(_isEnable: false);
				enableAnimSeedRate = false;
				UpdateAnimatorSpeed();
			}
			break;
		case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE_MULTI:
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				MonoBehaviourSingleton<StageObjectManager>.I.InvokeCoroutineImmidiately(CreateMultiAttackCollider(data));
			}
			break;
		case AnimEventFormat.ID.THS_BURST_TRANSITION_AVOID_ATK_ON:
			if (thsCtrl != null)
			{
				thsCtrl.SetEnableTransitionFromAvoidAtkFlag(_isEnable: true);
			}
			break;
		case AnimEventFormat.ID.THS_BURST_TRANSITION_AVOID_ATK_OFF:
			if (thsCtrl != null)
			{
				thsCtrl.SetEnableTransitionFromAvoidAtkFlag(_isEnable: false);
			}
			break;
		case AnimEventFormat.ID.GATHER_GIMMICK_GET:
			OnGatherGimmickGet();
			break;
		case AnimEventFormat.ID.FLICK_ACTION_ON:
			enableFlickAction = true;
			break;
		case AnimEventFormat.ID.FLICK_ACTION_OFF:
			enableFlickAction = false;
			break;
		case AnimEventFormat.ID.NEXTTRIGGER_INPUT_ON:
			enableInputNextTrigger = true;
			if (data.intArgs.IsNullOrEmpty())
			{
				inputNextTriggerIndex = 0;
			}
			else
			{
				inputNextTriggerIndex = data.intArgs[0];
			}
			break;
		case AnimEventFormat.ID.NEXTTRIGGER_INPUT_OFF:
			enableInputNextTrigger = false;
			break;
		case AnimEventFormat.ID.NEXTTRIGGER_TRANSITION_ON:
			enableNextTriggerTrans = true;
			if (inputNextTriggerFlag)
			{
				DoInputNextTrigger();
			}
			break;
		case AnimEventFormat.ID.COUNT_LONGTOUCH_ON:
			isCountLongTouch = true;
			countLongTouchSec = 0f;
			break;
		case AnimEventFormat.ID.TRANSITION_LONGTOUCH:
			if (isCountLongTouch)
			{
				int id = data.intArgs[0];
				float num5 = data.floatArgs[0];
				if (!(countLongTouchSec < num5) && (data.intArgs.Length <= 1 || data.intArgs[1] <= 0 || isBoostMode))
				{
					ActAttack(id);
				}
			}
			break;
		case AnimEventFormat.ID.COMBINE_BURST_PAIRSWORD:
			if (IsCoopNone() || IsOriginal())
			{
				bool isCombine = false;
				if (data.intArgs[0] == -1)
				{
					isCombine = !pairSwordsCtrl.IsCombineMode();
				}
				else if (data.intArgs[0] == 1)
				{
					isCombine = true;
				}
				pairSwordsCtrl.CombineBurst(isCombine);
			}
			break;
		case AnimEventFormat.ID.AERIAL_ON:
			isAerial = true;
			break;
		case AnimEventFormat.ID.AERIAL_OFF:
			isAerial = false;
			break;
		case AnimEventFormat.ID.FISHING_SE_PLAY:
		{
			if (!EnablePlaySound())
			{
				break;
			}
			int seId = fishingCtrl.GetSeId(data.intArgs[0]);
			if (seId != 0)
			{
				if (data.intArgs.Length > 1 && data.intArgs[1] > 0)
				{
					SoundManager.PlayLoopSE(seId, this, FindNode(""));
				}
				else
				{
					SoundManager.PlayOneShotSE(seId, this, FindNode(""));
				}
			}
			break;
		}
		case AnimEventFormat.ID.FISHING_SE_STOP:
			SoundManager.StopLoopSE(fishingCtrl.GetSeId(data.intArgs[0]), this);
			break;
		case AnimEventFormat.ID.SHOT_RESURRECTION_HOMING:
			EventShotResurrectionHoming(data);
			break;
		case AnimEventFormat.ID.WEAPON_ACTION_START:
			EventWeaponActionStart();
			break;
		case AnimEventFormat.ID.WEAPON_ACTION_END:
			EventWeaponActionEnd();
			break;
		case AnimEventFormat.ID.SET_CONDITION_TRIGGER:
			EventSetConditionTrigger();
			break;
		case AnimEventFormat.ID.SET_CONDITION_TRIGGER_2:
			EventSetConditionTrigger2();
			break;
		case AnimEventFormat.ID.FIX_POSITION_WEAPON_R_ON:
			EventFixPositionWeaponR_ON();
			break;
		case AnimEventFormat.ID.FIX_POSITION_WEAPON_R_OFF:
			EventFixPositionWeaponR_OFF();
			break;
		case AnimEventFormat.ID.SPEAR_BURST_BARRIER_OFF:
			EventSpearBurstBarrier_OFF();
			break;
		case AnimEventFormat.ID.WEAPON_ACTION_ENABLED_ON:
			enableWeaponAction = true;
			break;
		case AnimEventFormat.ID.WEAPON_ACTION_ENABLED_OFF:
			enableWeaponAction = false;
			break;
		case AnimEventFormat.ID.BUFF_START_SHIELD_REFLECT:
			EventBuffStartShieldReflect(data);
			break;
		case AnimEventFormat.ID.RAIN_SHOT_CHARGE_START:
			EventArrowRainChargeStart(data);
			break;
		case AnimEventFormat.ID.SHOT_ARROW_RAIN:
			EventArrowRainStart(data);
			break;
		case AnimEventFormat.ID.START_CARRY_GIMMICK:
			EventStartCarryGimmick();
			break;
		case AnimEventFormat.ID.END_CARRY_GIMMICK:
			EventEndCarryGimmick();
			break;
		case AnimEventFormat.ID.ENABLE_CARRY_PUT_ON:
			enableCancelToCarryPut = true;
			break;
		case AnimEventFormat.ID.ENABLE_CARRY_PUT_OFF:
			enableCancelToCarryPut = false;
			break;
		case AnimEventFormat.ID.THS_ORACLE_HORIZONTAL_START:
			thsCtrl.oracleCtrl.StartHorizontal(data.intArgs[0] != 0);
			break;
		case AnimEventFormat.ID.THS_ORACLE_HORIZONTAL_NEXT_CHECK:
			if (thsCtrl.oracleCtrl.NextHorizontal())
			{
				AttackHitCheckerClearInfo(data);
				if (MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.oracleTHSInfo.isBleedingDamageOfHorizontalNext && IsValidBuff(BuffParam.BUFFTYPE.BLEEDING))
				{
					buffParam.OnBleeding();
				}
			}
			break;
		case AnimEventFormat.ID.THS_ORACLE_VERNIER_EFFECT_ON:
			thsCtrl.oracleCtrl.StartVernierEffect(data.intArgs[0] != 0);
			break;
		case AnimEventFormat.ID.THS_ORACLE_VERNIER_EFFECT_OFF:
			thsCtrl.oracleCtrl.EndVernierEffect();
			break;
		case AnimEventFormat.ID.PLAYER_BLEEDING_DAMAGE:
			if ((IsCoopNone() || IsOriginal()) && IsValidBuff(BuffParam.BUFFTYPE.BLEEDING))
			{
				buffParam.OnBleeding();
			}
			break;
		case AnimEventFormat.ID.PLAYER_TELEPORT_AVOID_ON:
			enabledTeleportAvoid = true;
			break;
		case AnimEventFormat.ID.PLAYER_TELEPORT_AVOID_OFF:
			enabledTeleportAvoid = false;
			break;
		case AnimEventFormat.ID.PLAYER_TELEPORT_TO_TARGET_OFFSET:
			EventTeleportToTargetOffset(data);
			break;
		case AnimEventFormat.ID.SET_EXTRA_SP_GAUGE_DECREASING_RATE:
			extraSpGaugeDecreasingRate = data.floatArgs[0];
			break;
		case AnimEventFormat.ID.NEXT_IF_BOOST_MODE:
			if (isBoostMode)
			{
				SetNextTrigger(data.intArgs[0]);
			}
			break;
		case AnimEventFormat.ID.NEXT_IF_NOT_BOOST_MODE:
			if (!isBoostMode)
			{
				SetNextTrigger(data.intArgs[0]);
			}
			break;
		case AnimEventFormat.ID.ORACLE_SPEAR_GUARD_ON:
			spearCtrl.GuardOn();
			break;
		case AnimEventFormat.ID.ORACLE_SPEAR_GUARD_OFF:
			spearCtrl.GuardOff();
			break;
		case AnimEventFormat.ID.SHOT_ORACLE_SPEAR_SP:
			spearCtrl.EventShotOracleSp(data);
			break;
		case AnimEventFormat.ID.DESTROY_ORACLE_SPEAR_SP:
			spearCtrl.DestroyOracleSp();
			break;
		case AnimEventFormat.ID.SHOT_ORACLE_PAIR_SWORDS_RUSH:
			pairSwordsCtrl.EventShotOracleRush(data);
			break;
		case AnimEventFormat.ID.PLAYER_RUSH_AVOID_ON:
			enabledRushAvoid = true;
			break;
		case AnimEventFormat.ID.PLAYER_RUSH_AVOID_OFF:
			enabledRushAvoid = false;
			break;
		case AnimEventFormat.ID.ORACLE_PAIR_SWORDS_SP_START:
			pairSwordsCtrl.StartOracleSp(data);
			break;
		case AnimEventFormat.ID.ORACLE_PAIR_SWORDS_SP_END:
			pairSwordsCtrl.EndOracleSp();
			break;
		default:
			base.OnAnimEvent(data);
			break;
		}
	}

	protected override void SetFromInfo(ref BuffParam.BuffData data)
	{
		data.fromObjectID = createInfo.charaInfo.userId;
		data.fromEquipIndex = weaponIndex;
		data.fromSkillIndex = skillInfo.skillIndex;
	}

	protected override void EventMoveStart(AnimEventData.EventData data, Vector3 targetDir)
	{
		float num = data.floatArgs[0];
		if (data.intArgs != null && data.intArgs.Length != 0 && data.intArgs[0] == 1 && data.floatArgs.Length > (int)spAttackType)
		{
			num = data.floatArgs[(int)spAttackType];
		}
		if (isActSpecialAction && CheckAttackMode(ATTACK_MODE.SPEAR))
		{
			num *= GetRushDistanceRate();
			num = Mathf.Max(0f, num);
		}
		if (base.actionID == ACTION_ID.ATTACK && CheckAttackMode(ATTACK_MODE.SPEAR) && spAttackType == SP_ATTACK_TYPE.BURST)
		{
			num += num * buffParam.GetSpearRushSpeedRate();
			num = Mathf.Max(0f, num);
		}
		if (IsFromAvoidAction())
		{
			num *= buffParam.GetDistanceRateFromAvoid();
		}
		EventMoveEnd();
		enableEventMove = true;
		base.enableAddForce = false;
		eventMoveVelocity = targetDir * num;
		SetVelocity(Quaternion.LookRotation(GetTransformForward()) * eventMoveVelocity, VELOCITY_TYPE.EVENT_MOVE);
		eventMoveTimeCount = 0f;
	}

	private void EventRotateToTargetPointStart()
	{
		if (!startInputRotate && !isArrowAimBossMode && !isArrowAimLesserMode && !IsValidBuffBlind())
		{
			enableRotateToTargetPoint = true;
			rotateEventSpeed = 0f;
		}
	}

	private void EventRotateInputOff(AnimEventData.EventData data)
	{
		enableInputRotate = false;
		startInputRotate = false;
		if (data.intArgs != null && data.intArgs.Length != 0 && data.intArgs[0] == 1 && playerSender != null)
		{
			playerSender.OnSyncPosition();
		}
	}

	private bool IsFromAvoidAction()
	{
		ATTACK_MODE attackMode = this.attackMode;
		if (attackMode == ATTACK_MODE.PAIR_SWORDS)
		{
			if (base.attackID != 20)
			{
				return base.attackID == 21;
			}
			return true;
		}
		return false;
	}

	private void EventPlayerFunnelAttack(AnimEventData.EventData data)
	{
		string text = data.stringArgs[0];
		string text2 = data.stringArgs[1];
		Transform transform = FindNode(text2);
		if (transform == null)
		{
			Log.Error("Not found transform for launch!! name:" + text2);
			return;
		}
		AttackInfo attackInfo = FindAttackInfo(text);
		if (attackInfo == null)
		{
			Log.Error("Not found AttackInfo!! name:" + text);
			return;
		}
		BulletData bulletData = attackInfo.bulletData;
		if (bulletData == null || bulletData.dataFunnel == null)
		{
			Log.Error("Not found BulletData!! atkInfoName:" + text);
			return;
		}
		Vector3 offsetPos = new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
		Quaternion offsetRot = Quaternion.Euler(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
		new GameObject("PlayerAttackFunnelBit").AddComponent<PlayerAttackFunnelBit>().Initialize(this, attackInfo, IsValidBuffBlind() ? null : base.actionTarget, transform, offsetPos, offsetRot);
	}

	public void OnDestroyLaser(AttackNWayLaser delLaser)
	{
		if (activeAttackLaserList.Contains(delLaser))
		{
			activeAttackLaserList.Remove(delLaser);
		}
	}

	private void ClearLaser()
	{
		if (!activeAttackLaserList.IsNullOrEmpty())
		{
			for (int i = 0; i < activeAttackLaserList.Count; i++)
			{
				activeAttackLaserList[i].RequestDestroy();
			}
		}
	}

	protected override void EventNWayLaserAttack(AnimEventData.EventData data)
	{
		string name = data.stringArgs[0];
		string name2 = data.stringArgs[1];
		int numLaser = data.intArgs[0];
		Transform transform = Utility.Find(base._transform, name2);
		if (transform == null)
		{
			return;
		}
		AttackInfo attackInfo = FindAttackInfo(name);
		if (attackInfo != null)
		{
			BulletData bulletData = attackInfo.bulletData;
			if (!(bulletData == null) && bulletData.dataLaser != null)
			{
				AttackNWayLaser attackNWayLaser = new GameObject("AttackNWayLaser").AddComponent<AttackNWayLaser>();
				attackNWayLaser.Initialize(this, transform, attackInfo, numLaser);
				activeAttackLaserList.Add(attackNWayLaser);
			}
		}
	}

	protected override void EventShotPresent(AnimEventData.EventData data)
	{
		if (data.stringArgs.Length == 0)
		{
			Log.Error(LOG.INGAME, "Not set bullet name. Please check AnimEvent 'SHOT_PRESENT'.");
			return;
		}
		if (data.floatArgs.Length == 0)
		{
			Log.Error(LOG.INGAME, "Not set Range. Please check AnimEvent 'SHOT_PRESENT'.");
			return;
		}
		if (base.cachedBulletDataTable == null)
		{
			Log.Error(LOG.INGAME, "Not set bullet data. Please check bullet data exist.");
			return;
		}
		List<string> list = new List<string>();
		int i = 0;
		for (int num = data.stringArgs.Length; i < num; i++)
		{
			if (!string.IsNullOrEmpty(data.stringArgs[i]))
			{
				string[] source = data.stringArgs[i].Split(':');
				list.AddRange(source.ToList());
			}
		}
		if (list.IsNullOrEmpty())
		{
			return;
		}
		int j = 0;
		for (int count = list.Count; j < count; j++)
		{
			string text = list[j];
			if (string.IsNullOrEmpty(text))
			{
				continue;
			}
			BulletData bulletData = base.cachedBulletDataTable.Get(text);
			if (bulletData == null)
			{
				continue;
			}
			BulletData.BulletPresent dataPresent = bulletData.dataPresent;
			if (dataPresent == null)
			{
				continue;
			}
			int result = 0;
			string s = id.ToString() + (MonoBehaviourSingleton<StageObjectManager>.I.presentBulletObjIndex + 1).ToString();
			float d = data.floatArgs[0];
			float angle = 360f / (float)(count - 1) * (float)j;
			if (int.TryParse(s, out result))
			{
				Vector3 a = base._transform.localRotation * Quaternion.AngleAxis(angle, Vector3.up) * base._transform.forward;
				Vector3 position = base._transform.position + a * d;
				if (j == 0)
				{
					position = base._transform.position;
				}
				SetPresentBullet(result, dataPresent.type, position, text);
				if (playerSender != null)
				{
					playerSender.OnSetPresentBullet(result, dataPresent.type, position, text);
				}
			}
		}
	}

	public void SetPresentBullet(int presentBulletId, BulletData.BulletPresent.TYPE type, Vector3 position, string bulletName)
	{
		BulletData bulletData = base.cachedBulletDataTable.Get(bulletName);
		if (bulletData == null)
		{
			return;
		}
		GameObject gameObject = new GameObject();
		IPresentBulletObject presentBulletObject = null;
		switch (type)
		{
		case BulletData.BulletPresent.TYPE.HEAL:
			presentBulletObject = gameObject.AddComponent<PresentBulletObject>();
			break;
		}
		if (presentBulletObject == null)
		{
			return;
		}
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			UnityEngine.Object.Destroy(gameObject);
			return;
		}
		presentBulletObject.Initialize(presentBulletId, bulletData, base._transform);
		presentBulletObject.SetPosition(position);
		presentBulletObject.SetSkillParam(skillInfo.actSkillParam);
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			MonoBehaviourSingleton<StageObjectManager>.I.presentBulletObjList.Add(presentBulletObject);
			MonoBehaviourSingleton<StageObjectManager>.I.presentBulletObjIndex++;
		}
	}

	public void DestroyPresentBulletObject(int presentBulletId)
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return;
		}
		List<IPresentBulletObject> presentBulletObjList = MonoBehaviourSingleton<StageObjectManager>.I.presentBulletObjList;
		if (presentBulletObjList == null || presentBulletObjList.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < presentBulletObjList.Count; i++)
		{
			if (presentBulletObjList[i].GetPresentBulletId() == presentBulletId)
			{
				presentBulletObjList[i].OnPicked();
				break;
			}
		}
		MonoBehaviourSingleton<StageObjectManager>.I.RemovePresentBulletObject(presentBulletId);
	}

	protected override void EventShotZone(AnimEventData.EventData data)
	{
		if (data.stringArgs.Length == 0)
		{
			Log.Error(LOG.INGAME, "Not set bullet name. Please check AnimEvent 'SHOT_ZONE'.");
			return;
		}
		if (base.cachedBulletDataTable == null)
		{
			Log.Error(LOG.INGAME, "Not set bullet data. Please check bullet data exist.");
			return;
		}
		string text = data.stringArgs[0];
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		BulletData bulletData = base.cachedBulletDataTable.Get(text);
		if ((object)bulletData != null && bulletData.dataZone != null)
		{
			ShotZoneBullet(this, text, base._transform.position, MonoBehaviourSingleton<StageObjectManager>.I.ExistsEnemyValiedHealAttack(), isOwner: true);
			if (playerSender != null)
			{
				playerSender.OnShotZoneBullet(text, base._transform.position);
			}
		}
	}

	public void ShotZoneBullet(Player onwerPlayer, string bulletName, Vector3 position, bool isHealDamgeEnemy = false, bool isOwner = false)
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid() || skillInfo.actSkillParam == null || skillInfo.actSkillParam.tableData == null)
		{
			return;
		}
		BulletData bulletData = base.cachedBulletDataTable.Get(bulletName);
		if ((object)bulletData != null && bulletData.dataZone != null)
		{
			GameObject gameObject = new GameObject();
			ZoneBulletObject zoneBulletObject = null;
			BulletData.BulletZone.TYPE type = bulletData.dataZone.type;
			if (type == BulletData.BulletZone.TYPE.HEAL)
			{
				zoneBulletObject = gameObject.AddComponent<ZoneBulletObject>();
			}
			else
			{
				isHealDamgeEnemy = false;
			}
			zoneBulletObject?.Initialize(onwerPlayer, bulletData, position, skillInfo.actSkillParam, isHealDamgeEnemy, isOwner);
		}
	}

	public override void EventShotDecoy(AnimEventData.EventData data)
	{
		if (data.stringArgs.Length == 0)
		{
			Log.Error(LOG.INGAME, "Not set bullet name. Please check AnimEvent 'SHOT_DECOY'.");
			return;
		}
		if (base.cachedBulletDataTable == null)
		{
			Log.Error(LOG.INGAME, "Not set bullet data. Please check bullet data exist.");
			return;
		}
		string text = data.stringArgs[0];
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		BulletData bulletData = base.cachedBulletDataTable.Get(text);
		if ((object)bulletData != null && bulletData.IsDecoy())
		{
			int decoyId = id * 10 + localDecoyId;
			if (++localDecoyId >= 10)
			{
				localDecoyId = 0;
			}
			Vector3 position = base._transform.position;
			if (data.floatArgs != null && data.floatArgs.Length == 3)
			{
				position.Set(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
			}
			int skIndex = -1;
			if (data.intArgs != null && data.intArgs.Length == 1)
			{
				skIndex = data.intArgs[0];
			}
			else if (skillInfo != null)
			{
				skIndex = skillInfo.skillIndex;
			}
			ShotDecoyBullet(id, skIndex, decoyId, text, position, isHit: true);
			if (playerSender != null)
			{
				playerSender.OnShotDecoyBullet(skIndex, decoyId, text, position);
			}
		}
	}

	public void ShotDecoyBullet(int playerId, int skIndex, int decoyId, string bulletName, Vector3 position, bool isHit)
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return;
		}
		BulletData bulletData = base.cachedBulletDataTable.Get(bulletName);
		if ((object)bulletData == null)
		{
			return;
		}
		DecoyBulletObject decoyBulletObject = CreateDecoyObject(bulletData);
		if ((object)decoyBulletObject != null)
		{
			decoyBulletObject.Initialize(playerId, decoyId, bulletData, position, skillInfo.GetSkillParam(skIndex), isHit);
			Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
			if ((object)boss != null && (boss.IsOriginal() || boss.IsCoopNone()))
			{
				decoyBulletObject.HateCtrl();
			}
		}
	}

	public DecoyBulletObject CreateDecoyObject(BulletData bullet)
	{
		switch (bullet.type)
		{
		case BulletData.BULLET_TYPE.DECOY:
			if (bullet.dataDecoy != null)
			{
				return new GameObject().AddComponent<DecoyBulletObject>();
			}
			break;
		case BulletData.BULLET_TYPE.DECOY_TURRET_BIT:
			if (bullet.dataDecoyTurretBit != null)
			{
				return new GameObject().AddComponent<DecoyTurretBitBulletObject>();
			}
			break;
		}
		return null;
	}

	public void ExecExplodeDecoyBullet(int decoyId)
	{
		if (playerSender != null)
		{
			playerSender.OnExplodeDecoyBullet(decoyId);
		}
	}

	public void ExplodeDecoyBullet(int decoyId)
	{
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.FindDecoy(decoyId);
			if ((object)stageObject != null)
			{
				(stageObject as DecoyBulletObject)?.OnDisappear(isExplode: false);
			}
		}
	}

	private void EventPairSwordsShotBullet(AnimEventData.EventData data)
	{
		if (pairSwordsCtrl == null)
		{
			Log.Error("EventPairSwordsShotBullet. pairSwordsCtrl is null!!");
			return;
		}
		string text = data.stringArgs[0];
		string text2 = data.stringArgs[1];
		Transform transform = FindNode(text2);
		if (transform == null)
		{
			Log.Error("EventPairSwordsShotBullet. Not found transform for launch!! name:" + text2);
			return;
		}
		AttackInfo attackInfo = FindAttackInfo(text);
		if (attackInfo == null)
		{
			Log.Error("EventPairSwordsShotBullet. Not found AttackInfo!! name:" + text);
			return;
		}
		BulletData bulletData = attackInfo.bulletData;
		if (bulletData == null || bulletData.dataPairSwordsSoul == null)
		{
			Log.Error("EventPairSwordsShotBullet. Not found BulletData!! atkInfoName:" + text);
			return;
		}
		string change_effect = null;
		if (!playerParameter.pairSwordsActionInfo.Soul_EffectsForBullet.IsNullOrEmpty())
		{
			int nowWeaponElement = (int)GetNowWeaponElement();
			if (playerParameter.pairSwordsActionInfo.Soul_EffectsForBullet.Length >= nowWeaponElement)
			{
				change_effect = playerParameter.pairSwordsActionInfo.Soul_EffectsForBullet[nowWeaponElement];
			}
		}
		Vector3 point = new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
		Quaternion rhs = Quaternion.Euler(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
		Vector3 vector = transform.position + Quaternion.LookRotation(_forward) * point;
		Quaternion rot = Quaternion.LookRotation(GetBulletShotVecPositiveY(vector)) * rhs;
		AnimEventShot.Create(this, attackInfo, vector, rot, null, isScaling: true, change_effect);
	}

	private void EventPairSwordsShotLaser(AnimEventData.EventData data)
	{
		string text = data.stringArgs[0];
		string text2 = data.stringArgs[1];
		Transform transform = FindNode(text2);
		if (transform == null)
		{
			Log.Error("EventPairSwordsShotLaser. Not found transform for launch!! name:" + text2);
			return;
		}
		int comboLv = pairSwordsCtrl.GetComboLv();
		string[] soul_AttackInfoNamesForLaserByComboLv = playerParameter.pairSwordsActionInfo.Soul_AttackInfoNamesForLaserByComboLv;
		int[] soul_NumOfLaserByComboLv = playerParameter.pairSwordsActionInfo.Soul_NumOfLaserByComboLv;
		if (soul_AttackInfoNamesForLaserByComboLv.IsNullOrEmpty() || soul_AttackInfoNamesForLaserByComboLv.Length < comboLv || soul_NumOfLaserByComboLv.IsNullOrEmpty() || soul_NumOfLaserByComboLv.Length < comboLv)
		{
			return;
		}
		text = soul_AttackInfoNamesForLaserByComboLv[comboLv - 1];
		if (text.IsNullOrWhiteSpace())
		{
			return;
		}
		AttackInfo attackInfo = FindAttackInfo(text);
		if (attackInfo == null)
		{
			Log.Error("EventPairSwordsShotLaser. Not found AttackInfo!! name:" + text);
			return;
		}
		if (attackInfo.bulletData == null)
		{
			Log.Error("EventPairSwordsShotLaser. Not found BulletData!! atkInfoName:" + text);
			return;
		}
		Vector3 vector = new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
		Quaternion rhs = Quaternion.Euler(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
		int num = soul_NumOfLaserByComboLv[comboLv - 1];
		Vector3 bulletShotVecForSpWeak = GetBulletShotVecForSpWeak(transform.position);
		for (int i = 1; i <= num; i++)
		{
			Vector3 zero = Vector3.zero;
			if (num == 1)
			{
				zero = transform.position + Quaternion.LookRotation(_forward) * vector;
				bulletShotVecForSpWeak = GetBulletShotVecForSpWeak(zero);
			}
			else
			{
				float f = (float)(360 / num * i) * ((float)Math.PI / 180f);
				zero = transform.position + Quaternion.LookRotation(_forward) * (vector + new Vector3(Mathf.Cos(f), Mathf.Sin(f), 0f) * playerParameter.pairSwordsActionInfo.Soul_RadiusForLaser);
			}
			Quaternion rot = Quaternion.LookRotation(bulletShotVecForSpWeak) * rhs;
			AnimEventShot animEventShot = AnimEventShot.Create(this, attackInfo, zero, rot);
			pairSwordsCtrl.AddBulletLaser(animEventShot);
			animEventShot._transform.parent = base._transform;
			Vector3 forward = bulletShotVecForSpWeak;
			forward.y = 0f;
			_rotation = Quaternion.LookRotation(forward);
		}
		StartCoroutine(PlayPairSwordsShotLaserSE());
		pairSwordsCtrl.SetGaugePercentForLaser();
		pairSwordsCtrl.SetEventShotLaserExec();
		StartWaitingPacket(WAITING_PACKET.PLAYER_PAIR_SWORDS_LASER_END, keep_sync: true);
	}

	private IEnumerator PlayPairSwordsShotLaserSE()
	{
		int[] seIds = playerParameter.pairSwordsActionInfo.Soul_SeIds;
		if (!seIds.IsNullOrEmpty() && seIds.Length >= 2)
		{
			if (seIds[0] > 0)
			{
				SoundManager.PlayOneShotSE(seIds[0], _position);
			}
			yield return new WaitForSeconds(playerParameter.pairSwordsActionInfo.Soul_TimeForPlayLoopSE);
			if (seIds[1] > 0)
			{
				SoundManager.PlayLoopSE(seIds[1], this, base._transform);
			}
		}
	}

	private void EventShotHealingHoming(AnimEventData.EventData data)
	{
		if (IsOriginal() || IsCoopNone())
		{
			Coop_Model_PlayerShotHealingHoming coop_Model_PlayerShotHealingHoming = new Coop_Model_PlayerShotHealingHoming();
			coop_Model_PlayerShotHealingHoming.id = id;
			coop_Model_PlayerShotHealingHoming.atkInfoName = data.stringArgs[0];
			coop_Model_PlayerShotHealingHoming.launchNodeName = data.stringArgs[1];
			coop_Model_PlayerShotHealingHoming.offsetPos = new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
			coop_Model_PlayerShotHealingHoming.offsetRot = new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
			coop_Model_PlayerShotHealingHoming.targetNum = data.intArgs[0];
			coop_Model_PlayerShotHealingHoming.targetPlayerIDs = GetSortedArrayByPlayerDistance(coop_Model_PlayerShotHealingHoming.targetNum);
			OnShotHealingHoming(coop_Model_PlayerShotHealingHoming);
		}
	}

	public void OnShotHealingHoming(Coop_Model_PlayerShotHealingHoming model)
	{
		Transform transform = FindNode(model.launchNodeName);
		if (transform == null)
		{
			return;
		}
		AttackInfo attackInfo = FindAttackInfo(model.atkInfoName);
		if (attackInfo == null)
		{
			return;
		}
		BulletData bulletData = attackInfo.bulletData;
		if (bulletData == null || bulletData.dataHealingHomingBullet == null || model.targetPlayerIDs.IsNullOrEmpty() || !MonoBehaviourSingleton<StageObjectManager>.IsValid() || model.targetNum <= 0)
		{
			return;
		}
		Vector3 pos = transform.position + Quaternion.LookRotation(_forward) * model.offsetPos;
		Quaternion rot = Quaternion.LookRotation(_forward) * Quaternion.Euler(model.offsetRot);
		int num = Mathf.Min(model.targetNum, model.targetPlayerIDs.Length);
		for (int i = 0; i < num; i++)
		{
			AnimEventShot animEventShot = AnimEventShot.Create(this, attackInfo, pos, rot);
			if (bulletData.dataHoming != null || bulletData.dataHealingHomingBullet != null)
			{
				animEventShot.SetTarget(MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(model.targetPlayerIDs[i]));
			}
		}
		if (playerSender != null)
		{
			playerSender.OnShotHealingHoming(model);
		}
	}

	private void EventShotResurrectionHoming(AnimEventData.EventData data)
	{
		if (IsOriginal() || IsCoopNone())
		{
			Coop_Model_PlayerShotResurrectionHoming coop_Model_PlayerShotResurrectionHoming = new Coop_Model_PlayerShotResurrectionHoming();
			coop_Model_PlayerShotResurrectionHoming.id = id;
			coop_Model_PlayerShotResurrectionHoming.atkInfoName = data.stringArgs[0];
			coop_Model_PlayerShotResurrectionHoming.launchNodeName = data.stringArgs[1];
			coop_Model_PlayerShotResurrectionHoming.offsetPos = new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
			coop_Model_PlayerShotResurrectionHoming.offsetRot = new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
			coop_Model_PlayerShotResurrectionHoming.targetNum = data.intArgs[0];
			coop_Model_PlayerShotResurrectionHoming.targetPlayerIDs = GetSortedArrayByPlayerDistance(coop_Model_PlayerShotResurrectionHoming.targetNum);
			OnShotResurrectionHoming(coop_Model_PlayerShotResurrectionHoming);
		}
	}

	public void OnShotResurrectionHoming(Coop_Model_PlayerShotResurrectionHoming model)
	{
		Transform transform = FindNode(model.launchNodeName);
		if (transform == null)
		{
			return;
		}
		AttackInfo attackInfo = FindAttackInfo(model.atkInfoName);
		if (attackInfo == null)
		{
			return;
		}
		BulletData bulletData = attackInfo.bulletData;
		if (bulletData == null || bulletData.dataResurrectionHomingBullet == null || model.targetPlayerIDs.IsNullOrEmpty() || !MonoBehaviourSingleton<StageObjectManager>.IsValid() || model.targetNum <= 0)
		{
			return;
		}
		Vector3 pos = transform.position + Quaternion.LookRotation(_forward) * model.offsetPos;
		Quaternion rot = Quaternion.LookRotation(_forward) * Quaternion.Euler(model.offsetRot);
		int num = Mathf.Min(model.targetNum, model.targetPlayerIDs.Length);
		for (int i = 0; i < num; i++)
		{
			AnimEventShot resurrectionHomingTarget = AnimEventShot.Create(this, attackInfo, pos, rot);
			if (bulletData.dataHoming != null || bulletData.dataResurrectionHomingBullet != null)
			{
				Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(model.targetPlayerIDs[i]) as Player;
				if (player != null)
				{
					player.SetResurrectionHomingTarget(resurrectionHomingTarget);
				}
			}
		}
		if (playerSender != null)
		{
			playerSender.OnShotResurrectionHoming(model);
		}
	}

	public void SetResurrectionHomingTarget(AnimEventShot sht)
	{
		if (!(sht == null))
		{
			sht.SetTarget(this);
			isWaitingResurrectionHoming = true;
			if (IsCoopNone() || IsOriginal())
			{
				DeadCount(rescueTime, IsPrayed() || isStopCounter || isWaitingResurrectionHoming);
			}
		}
	}

	private int[] GetSortedArrayByPlayerDistance(int targetNum)
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return null;
		}
		List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
		if (playerList.IsNullOrEmpty())
		{
			return null;
		}
		List<StageObject> list = new List<StageObject>(playerList);
		list.Remove(this);
		Vector3 targetPos = _position;
		list.Sort((StageObject a, StageObject b) => Mathf.RoundToInt((a.transform.position - targetPos).magnitude - (b.transform.position - targetPos).magnitude));
		int num = targetNum;
		if (num <= 0 || list.Count <= num)
		{
			num = list.Count;
		}
		return (from o in list.GetRange(0, num)
			select o.id).ToArray();
	}

	private void EventHealHp()
	{
	}

	public virtual void ShotArrow(Vector3 shot_pos, Quaternion shot_rot, AttackInfo attack_info, bool isSitShot, bool isAimEnd, bool isSend = true)
	{
		if (attack_info == null)
		{
			return;
		}
		GameObject attach_object = ResourceUtility.Instantiate(MonoBehaviourSingleton<InGameLinkResourcesCommon>.I.arrow);
		bool flag = isSitShot && spAttackType == SP_ATTACK_TYPE.BURST;
		string change_effect = null;
		if (attack_info.rateInfoRate >= 1f)
		{
			if (spAttackType == SP_ATTACK_TYPE.BURST && isBoostMode && !flag)
			{
				change_effect = playerParameter.arrowActionInfo.boostArrowChargeMaxEffectName;
			}
			else if (isArrowAimBossMode && !flag)
			{
				change_effect = playerParameter.specialActionInfo.arrowChargeAimEffectName;
			}
		}
		bool isBossPierce = false;
		bool isCharacterHitDelete = attack_info.bulletData.data.isCharacterHitDelete;
		float num = -1f;
		if (attack_info.bulletData.dataFall != null)
		{
			num = attack_info.bulletData.dataFall.gravityStartTime;
		}
		bool isAim = isArrowAimBossMode || spAttackType == SP_ATTACK_TYPE.BURST;
		if (1f <= chargeRate)
		{
			if (bossBrain != null)
			{
				if (!isSitShot && (spAttackType == SP_ATTACK_TYPE.HEAT || spAttackType == SP_ATTACK_TYPE.BURST))
				{
					attack_info.bulletData.data.isCharacterHitDelete = false;
					if (num != -1f)
					{
						attack_info.bulletData.dataFall.gravityStartTime = -1f;
					}
					isBossPierce = true;
				}
				if (isSitShot && spAttackType == SP_ATTACK_TYPE.BURST && !isBoostMode)
				{
					isBossPierce = true;
				}
			}
			else
			{
				attack_info.bulletData.data.isCharacterHitDelete = false;
			}
		}
		EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)weaponData.eId);
		DamageDistanceTable.DamageDistanceData damageDistanceData = null;
		if (!flag)
		{
			damageDistanceData = Singleton<DamageDistanceTable>.I.GetData((uint)equipItemData.damageDistanceId);
		}
		AnimEventShot.CreateArrow(this, attack_info, shot_pos, shot_rot, attach_object, isScaling: true, change_effect, damageDistanceData).SetArrowInfo(isAim, isBossPierce, flag);
		attack_info.bulletData.data.isCharacterHitDelete = isCharacterHitDelete;
		if (num != -1f)
		{
			attack_info.bulletData.dataFall.gravityStartTime = num;
		}
		isArrowAimEnd = isAimEnd;
		if (playerSender != null && isSend)
		{
			playerSender.OnShotArrow(shot_pos, shot_rot, attack_info, isSitShot, isAimEnd);
		}
	}

	public int GetSoulArrowLockNum()
	{
		InGameSettingsManager.Player.ArrowActionInfo arrowActionInfo = playerParameter.arrowActionInfo;
		return (isBoostMode ? arrowActionInfo.soulBoostLockMax : arrowActionInfo.soulLockMax) + buffParam.passive.addSoulArrowLockCount;
	}

	public int GetSoulArrowNormalLockNum()
	{
		return playerParameter.arrowActionInfo.soulLockMax + buffParam.passive.addSoulArrowLockCount;
	}

	public int GetSoulArrowBoostLockNum()
	{
		return playerParameter.arrowActionInfo.soulBoostLockMax + buffParam.passive.addSoulArrowLockCount;
	}

	public virtual void ShotSoulArrow()
	{
		List<TargetMarker> targetMarkerList = MonoBehaviourSingleton<TargetMarkerManager>.I.GetTargetMarkerList();
		if (targetMarkerList == null || targetMarkerList.Count <= 0)
		{
			return;
		}
		InGameSettingsManager.Player.ArrowActionInfo arrowActionInfo = playerParameter.arrowActionInfo;
		Vector3 shotPos = GetBulletAppearPos();
		Vector3 bulletShotVec = GetBulletShotVec(shotPos);
		shotPos -= bulletShotVec * arrowActionInfo.soulShotPosVec;
		Quaternion bowRot = Quaternion.LookRotation(bulletShotVec);
		List<Vector3> targetPosList = new List<Vector3>();
		List<SoulArrowInfo> list = new List<SoulArrowInfo>();
		GetSoulArrowLockNum();
		for (int i = 0; i < targetMarkerList.Count; i++)
		{
			TargetMarker targetMarker = targetMarkerList[i];
			if (targetMarker == null)
			{
				continue;
			}
			List<int> multiLockOrder = targetMarker.GetMultiLockOrder();
			if (multiLockOrder == null)
			{
				continue;
			}
			int count = multiLockOrder.Count;
			if (count == 0)
			{
				continue;
			}
			bool isSpecialAttack = count == arrowActionInfo.soulLockRegionMax;
			float num = arrowActionInfo.soulLockNumAtkRate[count - 1];
			for (int j = 0; j < count; j++)
			{
				AttackHitInfo attackHitInfo = (AttackHitInfo)FindAttackInfo(arrowActionInfo.attackInfoNames[2], fix_rate: true, isDuplicate: true);
				if (attackHitInfo != null)
				{
					int num2 = multiLockOrder[j];
					float num3 = arrowActionInfo.soulLockOrderAtkRateBase + arrowActionInfo.soulLockOrderAtkRateCoefficient * (float)(num2 - 1);
					attackHitInfo.atkRate = num * num3;
					attackHitInfo.toEnemy.isSpecialAttack = isSpecialAttack;
					attackHitInfo.damageNumAddGroup = j;
					attackHitInfo.dontIncreaseGauge = isBoostMode;
					SoulArrowInfo soulArrowInfo = new SoulArrowInfo();
					soulArrowInfo.attackInfo = attackHitInfo;
					soulArrowInfo.point = targetMarker.point;
					list.Add(soulArrowInfo);
					targetPosList.Add(targetMarker.point.GetTargetPoint());
				}
			}
		}
		StartCoroutine(_ShotSoulArrow(shotPos, bowRot, list, arrowActionInfo.soulShotInterval, delegate
		{
			SetNextTrigger();
			isArrowAimEnd = true;
			if (playerSender != null)
			{
				playerSender.OnShotSoulArrow(shotPos, bowRot, targetPosList);
			}
		}));
	}

	private IEnumerator _ShotSoulArrow(Vector3 shotPos, Quaternion bowRot, List<SoulArrowInfo> saInfo, float interval, Action endCallback)
	{
		int i = 0;
		while (i < saInfo.Count)
		{
			SoulArrowInfo soulArrowInfo = saInfo[i];
			GameObject attach_object = ResourceUtility.Instantiate(MonoBehaviourSingleton<InGameLinkResourcesCommon>.I.arrow);
			AnimEventShot animEventShot = AnimEventShot.CreateArrow(this, soulArrowInfo.attackInfo, shotPos, bowRot, attach_object, isScaling: true, "", null);
			animEventShot.SetArrowInfo(isArrowAimBossMode, isBossPierce: false);
			animEventShot.SetTarget(soulArrowInfo.point);
			yield return new WaitForSeconds(interval);
			int num = i + 1;
			i = num;
		}
		endCallback();
	}

	public void ShotSoulArrowPuppet(Vector3 shotPos, Quaternion bowRot, List<Vector3> targetPosList)
	{
		AttackInfo atk_info = FindAttackInfo(playerParameter.arrowActionInfo.attackInfoNames[2]);
		for (int i = 0; i < targetPosList.Count; i++)
		{
			GameObject attach_object = ResourceUtility.Instantiate(MonoBehaviourSingleton<InGameLinkResourcesCommon>.I.arrow);
			AnimEventShot animEventShot = AnimEventShot.CreateArrow(this, atk_info, shotPos, bowRot, attach_object, isScaling: true, "", null);
			animEventShot.SetArrowInfo(isArrowAimBossMode, isBossPierce: false);
			animEventShot.SetPuppetTargetPos(targetPosList[i]);
		}
		SetNextTrigger();
	}

	public virtual Vector3 GetBulletAppearPos()
	{
		if (loader.socketWepR == null)
		{
			return base._collider.bounds.center + Vector3.up * 0.3f;
		}
		return loader.socketWepR.position;
	}

	public virtual Vector3 GetBulletShotVec(Vector3 appear_pos)
	{
		Vector3 vector = _forward;
		if (!IsValidBuffBlind() && GetTargetPos(out Vector3 pos))
		{
			vector = pos - appear_pos;
		}
		return vector.normalized;
	}

	public Vector3 GetBulletShotVecPositiveY(Vector3 appearPos)
	{
		Vector3 vector = _forward;
		if (!IsValidBuffBlind() && GetTargetPos(out Vector3 pos))
		{
			if (pos.y < 0f)
			{
				pos.y = 0f;
			}
			vector = pos - appearPos;
		}
		return vector.normalized;
	}

	public Vector3 GetBulletShotVecForSpWeak(Vector3 appearPos)
	{
		Vector3 vector = GetBulletShotVec(appearPos);
		if (!IsValidBuffBlind())
		{
			if (base.targetPointPos != Vector3.zero)
			{
				vector = base.targetPointPos - appearPos;
			}
			if (targetPointWithSpWeak != null)
			{
				vector = targetPointWithSpWeak.param.markerPos - appearPos;
			}
		}
		return vector.normalized;
	}

	public void SetArrowAimKeep()
	{
		isArrowAimKeep = (isArrowAimBossMode || isArrowAimLesserMode);
	}

	public virtual void SetArrowAimBossMode(bool enable)
	{
		isArrowAimBossMode = enable;
		SetArrowAimBossVisible(enable);
	}

	protected virtual void SetArrowAimBossVisible(bool enable)
	{
	}

	public virtual void SetArrowAimLesserMode(bool enable)
	{
		isArrowAimLesserMode = enable;
		SetArrowAimLesserVisible(enable);
	}

	protected virtual void SetArrowAimLesserVisible(bool enable)
	{
	}

	public virtual void UpdateArrowAimLesserMode(Vector2 input_vec)
	{
	}

	protected void UpdateArrowAngle()
	{
		if (!(base.animator != null) || attackMode != ATTACK_MODE.ARROW)
		{
			return;
		}
		Vector3 bulletAppearPos = GetBulletAppearPos();
		Vector3 bulletShotVec = GetBulletShotVec(bulletAppearPos);
		if (bulletShotVec != Vector3.zero)
		{
			float num = Quaternion.LookRotation(bulletShotVec).eulerAngles.x;
			if (num >= 180f)
			{
				num -= 360f;
			}
			num = 0f - num;
			base.animator.SetFloat(arrowAngleID, num);
		}
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
		float rate = chargeRate;
		spearCtrl.GetSpinRate(ref rate);
		return rate;
	}

	public override AttackInfo FindAttackInfoExternal(string name, bool fix_rate, float rate)
	{
		AttackInfo[] attackInfosAll = playerParameter.attackInfosAll;
		return _FindAttackInfo(attackInfosAll, name, fix_rate, rate);
	}

	protected override AttackInfo _FindAttackInfo(AttackInfo[] attack_infos, string name, bool fix_rate, float rate, bool isDuplicate = false)
	{
		if (string.IsNullOrEmpty(name))
		{
			return null;
		}
		if (attack_infos == null)
		{
			return null;
		}
		if (name[0] == '@' && name.StartsWith("@skill_"))
		{
			int length = "@skill_".Length;
			int num = int.Parse(name.Substring(length, name.Length - length));
			SkillInfo.SkillParam actSkillParam = skillInfo.actSkillParam;
			if (actSkillParam == null || actSkillParam.tableData == null)
			{
				Log.Error(LOG.INGAME, "FindAttackInfo skill param none.");
				return null;
			}
			string[] attackInfoNames = actSkillParam.tableData.attackInfoNames;
			if (num < 0 || num >= attackInfoNames.Length)
			{
				Log.Error(LOG.INGAME, "FindAttackInfo skill param out of range.");
				return null;
			}
			name = attackInfoNames[num];
		}
		return base._FindAttackInfo(attack_infos, name, fix_rate, rate, isDuplicate);
	}

	protected override bool GetTargetPos(out Vector3 pos)
	{
		pos = Vector3.zero;
		bool result = false;
		if (targetingPoint != null)
		{
			pos = targetingPoint.param.targetPos;
			result = true;
		}
		return result;
	}

	public override void ChatSay(int chatID)
	{
		if (uiPlayerStatusGizmo != null)
		{
			uiPlayerStatusGizmo.SayChat(chatID);
		}
		base.ChatSay(chatID);
	}

	public override void ChatSay(string message)
	{
		if (uiPlayerStatusGizmo != null && uiPlayerStatusGizmo.gameObject.activeInHierarchy)
		{
			uiPlayerStatusGizmo.SayChat(message);
		}
		OnSendChatMessage(message);
		base.ChatSay(message);
	}

	public override void ChatSayStamp(int stamp_id)
	{
		if (uiPlayerStatusGizmo != null && uiPlayerStatusGizmo.gameObject.activeInHierarchy)
		{
			uiPlayerStatusGizmo.SayChatStamp(stamp_id);
		}
		OnSendChatStamp(stamp_id);
		base.ChatSayStamp(stamp_id);
	}

	protected virtual void OnSendChatMessage(string message)
	{
		if (MonoBehaviourSingleton<UIInGameMessageBar>.IsValid() && MonoBehaviourSingleton<UIInGameMessageBar>.I.isActiveAndEnabled)
		{
			MonoBehaviourSingleton<UIInGameMessageBar>.I.Announce(base.charaName, message);
		}
	}

	protected virtual void OnSendChatStamp(int stamp_id)
	{
		if (MonoBehaviourSingleton<UIInGameMessageBar>.IsValid() && MonoBehaviourSingleton<UIInGameMessageBar>.I.isActiveAndEnabled)
		{
			MonoBehaviourSingleton<UIInGameMessageBar>.I.Announce(base.charaName, stamp_id);
		}
	}

	public override SkillInfo.SkillParam GetSkillParam(int index)
	{
		return skillInfo.GetSkillParam(index);
	}

	public override void OnFailedWaitingPacket(WAITING_PACKET type)
	{
		switch (type)
		{
		case WAITING_PACKET.PLAYER_CHARGE_RELEASE:
			ActIdle();
			break;
		case WAITING_PACKET.PLAYER_PRAYER_END:
		{
			int i = 0;
			for (int count = prayTargetInfos.Count; i < count; i++)
			{
				Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(prayTargetInfos[i].targetId) as Player;
				if (player == null)
				{
					return;
				}
				player.EndPrayed(id);
			}
			prayTargetInfos.Clear();
			boostPrayTargetInfoList.Clear();
			boostPrayedInfoList.Clear();
			break;
		}
		case WAITING_PACKET.PLAYER_JUMP_END:
			ActIdle();
			break;
		case WAITING_PACKET.PLAYER_SOUL_BOOST:
			FinishBoostMode();
			break;
		case WAITING_PACKET.EVOLVE:
			EndEvolve();
			break;
		case WAITING_PACKET.PLAYER_PAIR_SWORDS_LASER_END:
		case WAITING_PACKET.PLAYER_ONE_HAND_SWORD_MOVE_END:
		case WAITING_PACKET.PLAYER_GATHER_GIMMICK:
			if (fishingCtrl == null || !fishingCtrl.IsFighting())
			{
				ActIdle();
			}
			break;
		}
		base.OnFailedWaitingPacket(type);
	}

	public void SetAppearPosField()
	{
		if (FieldManager.IsValidInGame())
		{
			Vector3 zero = Vector3.zero;
			zero.x = MonoBehaviourSingleton<FieldManager>.I.currentStartMapX;
			zero.z = MonoBehaviourSingleton<FieldManager>.I.currentStartMapZ;
			_position = zero;
			_rotation = Quaternion.AngleAxis(MonoBehaviourSingleton<FieldManager>.I.currentStartMapDir, Vector3.up);
			SetAppearPos(zero);
		}
	}

	public void SetAppearPosOwner(Vector3 enemy_pos)
	{
		Vector3 vector = enemy_pos + Vector3.forward * playerParameter.appearPosDistance;
		if (MonoBehaviourSingleton<StageManager>.I.CheckPosInside(vector))
		{
			_position = vector;
			LookAt(enemy_pos);
			SetAppearPos(vector);
		}
		else
		{
			SetAppearPosGuest(enemy_pos);
		}
	}

	public void SetAppearPosGuest(Vector3 center_pos)
	{
		SetAppearRandomPosFixDistance(center_pos, playerParameter.appearPosDistance, playerParameter.appearPosTryCount);
		LookAt(center_pos);
	}

	public virtual void PlayVoice(int voice_id)
	{
		if (EnablePlaySound() && !FieldManager.IsValidInTutorial())
		{
			SoundManager.PlayVoice(loader.GetVoiceAudioClip(voice_id), voice_id, 0.6f, voiceChannel, this, loader.head);
		}
	}

	public float GetRadiusCustomRate()
	{
		float num = 1f;
		float rate = 1f;
		if (GetOneHandSwordRadiusCustomRate(ref rate))
		{
			num *= rate;
		}
		if (GetPairSwordsRadiusCustomRate(ref rate))
		{
			num *= rate;
		}
		return num;
	}

	protected bool GetOneHandSwordRadiusCustomRate(ref float rate)
	{
		if (!buffParam.IsValidBuff(BuffParam.BUFFTYPE.LUNATIC_TEAR))
		{
			return false;
		}
		if (attackMode != ATTACK_MODE.ONE_HAND_SWORD)
		{
			return false;
		}
		if (isActSpecialAction)
		{
			return false;
		}
		InGameSettingsManager.AbilityParam abilityParam = MonoBehaviourSingleton<InGameSettingsManager>.I.abilityParam;
		if (abilityParam == null)
		{
			return false;
		}
		if (abilityParam.oneHandSwordRadiusCustomRate <= 0f)
		{
			return false;
		}
		rate = abilityParam.oneHandSwordRadiusCustomRate;
		return true;
	}

	public void StartEffectDrain(Enemy enemy)
	{
		if (!(enemy == null) && !(base.effectPlayProcessor == null))
		{
			List<EffectPlayProcessor.EffectSetting> settings = base.effectPlayProcessor.GetSettings("EFFECT_DRAIN_DAMAGE");
			if (settings != null && settings.Count > 0)
			{
				new GameObject("EffectDrain").AddComponent<EffectDrain>().Initialize(this, enemy, settings[0]);
			}
		}
	}

	public int GetCurrentWeaponElement()
	{
		if (weaponData == null)
		{
			return 6;
		}
		return Singleton<EquipItemTable>.I.GetEquipItemData((uint)weaponData.eId).GetElemAtkType();
	}

	public Vector3 GetCannonVector()
	{
		if (targetFieldGimmickCannon == null)
		{
			return Vector3.zero;
		}
		Transform cannonTransform = targetFieldGimmickCannon.GetCannonTransform();
		if (cannonTransform != null)
		{
			return cannonTransform.forward;
		}
		return Vector3.zero;
	}

	public void ApplyCannonVector(Vector3 cannonVec)
	{
		Vector3 forward = cannonVec;
		forward.y = 0f;
		_rotation = Quaternion.LookRotation(forward);
		if (targetFieldGimmickCannon != null)
		{
			targetFieldGimmickCannon.ApplyCannonVector(cannonVec);
		}
	}

	public void SetSyncCannonRotation(Vector3 cannonVec)
	{
		if (cannonVec != Vector3.zero)
		{
			syncCannonRotation = Quaternion.LookRotation(cannonVec);
		}
		if (GetCannonVector() != Vector3.zero)
		{
			prevCannonRotation = Quaternion.LookRotation(GetCannonVector());
		}
		syncCannonVecTimer = 0f;
		isSyncingCannonRotation = true;
	}

	protected void UpdateSyncCannonRotation()
	{
		if (isSyncingCannonRotation)
		{
			if (base.actionID != (ACTION_ID)31)
			{
				isSyncingCannonRotation = false;
				return;
			}
			float t = Mathf.Clamp01(syncCannonVecTimer / 1f);
			Vector3 cannonVec = Quaternion.SlerpUnclamped(prevCannonRotation, syncCannonRotation, t) * Vector3.forward;
			ApplyCannonVector(cannonVec);
			syncCannonVecTimer += Time.deltaTime;
		}
	}

	public void SetSyncUsingCannon(int id)
	{
		if (QuestManager.IsValidInGame() && !IsOnCannonMode() && MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			IFieldGimmickCannon fieldGimmickCannon = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.Cannon, id) as IFieldGimmickCannon;
			if (fieldGimmickCannon != null)
			{
				base.actionID = (ACTION_ID)31;
				targetFieldGimmickCannon = fieldGimmickCannon;
				fieldGimmickCannon.OnBoard(this);
				PlayMotion(131);
				SetCannonState(CANNON_STATE.READY);
			}
		}
	}

	protected override void EventStatusUpDefenceON(AnimEventData.EventData data)
	{
		isAnimEventStatusUpDefence = true;
		if (data.floatArgs.Length != 0)
		{
			animEventStatusUpDefenceRate = data.floatArgs[0];
		}
	}

	protected override void EventStatusUpDefenceOFF()
	{
		isAnimEventStatusUpDefence = false;
		animEventStatusUpDefenceRate = 1f;
	}

	public void EventWeaponActionStart()
	{
		spearCtrl.OnWeaponActionStart();
		if (playerSender != null)
		{
			playerSender.OnWeaponActionStart();
		}
	}

	public void EventWeaponActionEnd()
	{
		spearCtrl.OnWeaponActionEnd();
		if (playerSender != null)
		{
			playerSender.OnWeaponActionEnd();
		}
	}

	protected void EventSetConditionTrigger()
	{
		if (spearCtrl.CheckConditionTrigger())
		{
			SetNextTrigger();
		}
	}

	protected void EventSetConditionTrigger2()
	{
		if (spearCtrl.CheckConditionTrigger2())
		{
			SetNextTrigger(1);
		}
	}

	private void EventFixPositionWeaponR_ON()
	{
		Transform transform = FindNode("R_Wep");
		if (!(transform == null) && MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			fixWepData.wepTrans = transform;
			fixWepData.wepPos = transform.position;
			fixWepData.wepRot = transform.rotation;
			fixWepData.enable = true;
			spearCtrl.OnFixPositionWeaponR_ON(fixWepData.wepPos);
		}
	}

	private void EventFixPositionWeaponR_OFF()
	{
		if (fixWepData != null && fixWepData.enable)
		{
			spearCtrl.OnFixPositionWeaponR_OFF();
			fixWepData.ClearData();
		}
	}

	private void EventSpearBurstBarrier_OFF()
	{
		spearCtrl.EnableBarrierBulletDelete();
	}

	private void EventBuffStartShieldReflect(AnimEventData.EventData data)
	{
		if (IsCoopNone() || IsOriginal())
		{
			BuffParam.BuffData buffData = new BuffParam.BuffData();
			buffData.type = BuffParam.BUFFTYPE.SHIELD_REFLECT;
			buffData.time = -1f;
			buffData.endless = true;
			buffData.value = 1;
			OnBuffStart(buffData);
			if (data.stringArgs.Count() >= 1)
			{
				shieldReflectInfo.attackInfoName = data.stringArgs[0];
			}
			if (data.intArgs.Count() >= 1)
			{
				shieldReflectInfo.seId = data.intArgs[0];
			}
			if (data.floatArgs.Count() >= 6)
			{
				shieldReflectInfo.offsetPos = new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
				shieldReflectInfo.offsetRot = new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
			}
		}
	}

	public void EventTeleportToTargetOffset(AnimEventData.EventData data)
	{
		if (IsValidBuff(BuffParam.BUFFTYPE.BLIND))
		{
			return;
		}
		bool flag = (data.intArgs[0] != 0) ? true : false;
		Vector3 vector = new Vector3(0f, 0f, data.floatArgs[0]);
		Vector3 pos = _position;
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.boss != null)
		{
			pos = MonoBehaviourSingleton<StageObjectManager>.I.boss._position;
			if (flag)
			{
				vector = Quaternion.Euler(MonoBehaviourSingleton<StageObjectManager>.I.boss._rotation.eulerAngles) * vector;
			}
		}
		else if (!GetTargetPos(out pos))
		{
			return;
		}
		pos.y = 0f;
		if (!flag)
		{
			vector = Quaternion.LookRotation(_position - pos) * vector;
		}
		Vector3 vector2 = pos;
		if (vector2 != _position)
		{
			vector2 += vector;
		}
		if (!MonoBehaviourSingleton<StageManager>.I.CheckPosInside(vector2))
		{
			RaycastHit hit = default(RaycastHit);
			if (AIUtility.RaycastWallAndBlock(this, vector2, out hit) || AIUtility.RaycastObstacle(this, vector2, out hit))
			{
				vector2 = hit.point;
			}
		}
		vector2.y = 0f;
		if (!pos.Equals(_position))
		{
			_rotation = Quaternion.LookRotation(pos - vector2);
		}
		_position = vector2;
	}

	private ShieldDamageData CalcShieldDamage(int damage)
	{
		ShieldDamageData shieldDamageData = new ShieldDamageData();
		if ((int)base.ShieldHp <= damage)
		{
			shieldDamageData.isShieldEnd = true;
			shieldDamageData.shieldDamage = base.ShieldHp;
			shieldDamageData.hpDamage = Mathf.Max(0, damage - (int)base.ShieldHp);
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
		if (disableGuard)
		{
			return false;
		}
		if (!isGuardWalk && base.actionID != (ACTION_ID)19 && base.actionID != (ACTION_ID)20 && base.actionID != (ACTION_ID)34)
		{
			return base.actionID == (ACTION_ID)21;
		}
		return true;
	}

	private bool _IsGuard(SP_ATTACK_TYPE spType)
	{
		if (!_IsGuard())
		{
			return false;
		}
		return CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, spType);
	}

	public void _StartGuard()
	{
		isJustGuard = false;
		isSuccessParry = false;
		if (CheckAttackModeAndSpType(ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE))
		{
			InGameSettingsManager.Player.SpearActionInfo.Oracle oracle = playerParameter.spearActionInfo.oracle;
			guardingSec = Mathf.Lerp(oracle.maxJustGuardSec, oracle.minJustGuardSec, spearCtrl.StockedRate) * buffParam.GetJustGuardExtendRate();
		}
		else if (spAttackType == SP_ATTACK_TYPE.BURST)
		{
			guardingSec = playerParameter.ohsActionInfo.burstOHSInfo.JustGuardValidSec * buffParam.GetJustGuardExtendRate();
		}
		else
		{
			guardingSec = playerParameter.ohsActionInfo.Common_JustGuardValidSec * buffParam.GetJustGuardExtendRate();
		}
	}

	public void _EndGuard()
	{
		guardingSec = 0f;
	}

	public void _UpdateGuard()
	{
		if (guardingSec > 0f)
		{
			guardingSec -= Time.deltaTime;
		}
	}

	public bool _CheckJustGuardSec()
	{
		if (_CheckDisableJustGuard())
		{
			return false;
		}
		return guardingSec > 0f;
	}

	protected bool _CheckDisableJustGuard()
	{
		if (CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.BURST) && disableParryAction)
		{
			return true;
		}
		return false;
	}

	private float _GetGuardDamageCutRate()
	{
		float num = (CheckAttackModeAndSpType(ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE) && _CheckJustGuardSec()) ? playerParameter.spearActionInfo.oracle.damageCutRateWhileJustGuard : (CheckAttackModeAndSpType(ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE) ? playerParameter.spearActionInfo.oracle.damageCutRateWhileGuard : ((!CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.NONE) || !_CheckJustGuardSec()) ? playerParameter.ohsActionInfo.Common_GuardDamageCutRate : playerParameter.ohsActionInfo.Normal_JustGuardDamageCutRate));
		return num * buffParam.GetGuardUp();
	}

	private float _GetGuardingHealSpeedUp()
	{
		if (_IsGuard(SP_ATTACK_TYPE.NONE))
		{
			return playerParameter.ohsActionInfo.Normal_GuardingHealSpeedRate;
		}
		return 1f;
	}

	private void _AddRevengeGauge(int damage)
	{
		if (base.hpMax > 0)
		{
			AttackHitInfo attackInfo = new AttackHitInfo();
			IncreaseSpActonGauge(attackInfo, Vector3.zero, damage);
		}
	}

	public virtual void CheckBuffShadowSealing()
	{
	}

	protected void _StartBuffShadowSealing()
	{
		if (!isBuffShadowSealing)
		{
			isBuffShadowSealing = true;
			if ((object)buffShadowSealingEffect == null)
			{
				buffShadowSealingEffect = EffectManager.GetEffect("ef_btl_wsk_bow_01_02", FindNode("Root"));
			}
		}
	}

	protected void _EndBuffShadowSealing()
	{
		if (isBuffShadowSealing)
		{
			isBuffShadowSealing = false;
			ReleaseEffect(ref buffShadowSealingEffect);
		}
	}

	protected void UpdateSpearAction()
	{
		InGameSettingsManager.Player.SpearActionInfo spearActionInfo = playerParameter.spearActionInfo;
		if (hitSpearSpecialAction)
		{
			hitSpearSpActionTimer += Time.deltaTime;
			if (hitSpearSpActionTimer > spearActionInfo.rushCancellableTime)
			{
				enableCancelToAttack = false;
			}
		}
		if (isLoopingRush)
		{
			actRushLoopTimer += Time.deltaTime;
			if (actRushLoopTimer > spearActionInfo.rushLoopTime)
			{
				isLoopingRush = false;
				SetNextTrigger();
			}
			else if (actRushLoopTimer > spearActionInfo.rushCanAvoidTime)
			{
				enableCancelToAvoid = true;
			}
		}
		if (isSpearHundred)
		{
			spearHundredSecFromStart += Time.deltaTime;
			spearHundredSecFromLastTap += Time.deltaTime;
			if (spearHundredSecFromStart >= spearActionInfo.hundredLoopLimitSec)
			{
				isSpearHundred = false;
				ActAttack(22);
			}
			else if (spearHundredSecFromLastTap >= spearActionInfo.hundredTapIntervalSec)
			{
				isSpearHundred = false;
				SetNextTrigger();
			}
		}
		switch (jumpState)
		{
		case eJumpState.FallWait:
			jumpActionCounter -= Time.deltaTime;
			if (jumpActionCounter <= 0f)
			{
				SetNextTrigger(1);
				jumpState = eJumpState.Fall;
			}
			break;
		case eJumpState.Fall:
			jumpFallBodyPosition.y -= spearActionInfo.jumpFallSpeed * Time.deltaTime;
			if (jumpFallBodyPosition.y <= 0f)
			{
				OnJumpEnd(_position, isSuccess: false, 0f);
			}
			base.body.transform.localPosition = jumpFallBodyPosition;
			break;
		case eJumpState.HitStop:
			jumpActionCounter += Time.deltaTime;
			if (jumpActionCounter >= spearActionInfo.jumpHitStop)
			{
				jumpActionCounter = 0f;
				SetNextTrigger();
				jumpState = eJumpState.Randing;
			}
			break;
		case eJumpState.Randing:
			jumpActionCounter += Time.deltaTime;
			if (jumpActionCounter > spearActionInfo.jumpRandingHeightStartTime)
			{
				float num = (jumpActionCounter - spearActionInfo.jumpRandingHeightStartTime) / (spearActionInfo.jumpRandingHeightEndTime - spearActionInfo.jumpRandingHeightStartTime);
				if (num > 1f)
				{
					num = 1f;
				}
				jumpFallBodyPosition.y = jumpRandingBaseBodyY * (1f - num);
				if (jumpFallBodyPosition.y <= 0f)
				{
					jumpFallBodyPosition.y = 0f;
				}
				base.body.transform.localPosition = jumpFallBodyPosition;
			}
			if (jumpActionCounter > spearActionInfo.jumpRandingMoveStartTime)
			{
				float num2 = (jumpActionCounter - spearActionInfo.jumpRandingMoveStartTime) / (spearActionInfo.jumpRandingMoveEndTime - spearActionInfo.jumpRandingMoveStartTime);
				if (num2 > 1f)
				{
					num2 = 1f;
				}
				_position = jumpRaindngBasePos + jumpRandingVector * num2;
			}
			break;
		}
	}

	public float GetRushDistanceRate()
	{
		float num = buffParam.GetSpearRushDistanceRate() + playerParameter.spearActionInfo.rushDistanceRate;
		if (exRushChargeRate >= 1f)
		{
			num += evolveCtrl.GetLeviathanRushDistanceRate();
		}
		return num;
	}

	public float GetJumpElementDamageUpRate()
	{
		return playerParameter.spearActionInfo.jumpElementDamageRate[useGaugeLevel];
	}

	public virtual void HitJumpAttack()
	{
		OnJumpEnd(_position, isSuccess: true, jumpFallBodyPosition.y);
	}

	public int CheckGaugeLevel()
	{
		if (CheckAttackModeAndSpType(ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.HEAT))
		{
			return (int)(CurrentWeaponSpActionGauge / 333f);
		}
		if (CheckAttackModeAndSpType(ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.HEAT))
		{
			return (int)(CurrentWeaponSpActionGauge / 333f);
		}
		if (CheckAttackModeAndSpType(ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL))
		{
			return pairSwordsCtrl.GetComboLv() - 1;
		}
		return -1;
	}

	public void UseSpGauge()
	{
		float num = 1000f;
		useGaugeLevel = 0;
		if (CheckAttackModeAndSpType(ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.HEAT))
		{
			num = 333f;
			useGaugeLevel = (int)(CurrentWeaponSpActionGauge / 333f);
		}
		else if (CheckAttackModeAndSpType(ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.HEAT))
		{
			num = 333f;
			if (CurrentWeaponSpActionGauge >= 333f)
			{
				useGaugeLevel = 1;
			}
		}
		if (useGaugeLevel > 0)
		{
			CurrentWeaponSpActionGauge -= num * (float)useGaugeLevel;
		}
	}

	protected virtual void _JumpRize()
	{
	}

	public void OnJumpRize(Vector3 dir, int level)
	{
		jumpFallBodyPosition = dir;
		useGaugeLevel = level;
		SetNextTrigger();
		jumpState = eJumpState.Rize;
		ForceClearInBarrier();
		StartWaitingPacket(WAITING_PACKET.PLAYER_JUMP_END, keep_sync: true);
		if ((object)playerSender != null)
		{
			playerSender.OnJumpRize(dir, level);
		}
	}

	public void OnJumpEnd(Vector3 pos, bool isSuccess, float y)
	{
		if (jumpState != eJumpState.Fall)
		{
			return;
		}
		if (isSuccess)
		{
			if (bossBrain == null)
			{
				return;
			}
			jumpActionCounter = 0f;
			jumpRaindngBasePos = pos;
			jumpRandingBaseBodyY = y;
			jumpState = eJumpState.HitStop;
		}
		else
		{
			jumpFallBodyPosition.y = 0f;
			base.body.transform.localPosition = jumpFallBodyPosition;
			SetNextTrigger(2);
			jumpState = eJumpState.Failure;
		}
		EndWaitingPacket(WAITING_PACKET.PLAYER_JUMP_END);
		if ((object)playerSender != null)
		{
			playerSender.OnJumpEnd(pos, isSuccess, y);
		}
	}

	public void ApplySyncExRush(bool flag)
	{
		isChargeExRush = flag;
	}

	private void _StartExRushCharge()
	{
		float exRushChargeSec = playerParameter.spearActionInfo.exRushChargeSec;
		float num = 1f - buffParam.GetChargeSpearTimeRate();
		if (num < 0f)
		{
			num = 0f;
		}
		isChargeExRush = true;
		inputChargeMaxTiming = true;
		inputChargeTimeOffset = 0f;
		inputChargeTimeMax = exRushChargeSec * num;
		inputChargeTimeCounter = 0f;
		chargeRate = 0f;
		exRushChargeRate = 0f;
		if ((object)exRushChargeEffect == null)
		{
			exRushChargeEffect = EffectManager.GetEffect("ef_btl_wsk_charge_loop_01", FindNode("R_Wep"));
		}
	}

	public bool IsExRushDamageUpAttack(AttackHitInfo.ATTACK_TYPE type)
	{
		if (!CheckAttackModeAndSpType(ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.NONE))
		{
			return false;
		}
		if (exRushChargeRate <= 0f)
		{
			return false;
		}
		if (type == AttackHitInfo.ATTACK_TYPE.SPEAR_SP || (base.attackID >= 10 && base.attackID <= 13))
		{
			return true;
		}
		return false;
	}

	public bool GetExRushElementDamageUpRate(AttackHitInfo.ATTACK_TYPE type, ref float rRate)
	{
		rRate = 1f;
		if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			return false;
		}
		if (!IsExRushDamageUpAttack(type))
		{
			return false;
		}
		float oMin;
		float oMax;
		float oFull;
		if (evolveCtrl.IsExecLeviathan())
		{
			evolveCtrl.GetLeviathanExRushDamageRate(out oMin, out oMax, out oFull);
		}
		else
		{
			InGameSettingsManager.Player.SpearActionInfo spearActionInfo = playerParameter.spearActionInfo;
			oMin = spearActionInfo.exRushElementDamageRateMin;
			oMax = spearActionInfo.exRushElementDamageRateMax;
			oFull = spearActionInfo.exRushElementDamageRateFull;
		}
		if (exRushChargeRate >= 1f)
		{
			rRate = oFull;
		}
		else
		{
			rRate = oMin + (oMax - oMin) * exRushChargeRate;
		}
		return true;
	}

	protected override void EventExecuteEvolve(AnimEventData.EventData data)
	{
		float rSec = 0f;
		if (evolveCtrl.Execute(ref rSec))
		{
			StartWaitingPacket(WAITING_PACKET.EVOLVE, keep_sync: false, rSec);
		}
	}

	public uint GetEvolveWeaponId()
	{
		return weaponEquipItemDataList[weaponIndex].evolveId;
	}

	public bool IsEvolveWeapon()
	{
		return weaponEquipItemDataList[weaponIndex].evolveId != 0;
	}

	public void OnSyncEvolveAction(bool isAction)
	{
		if (isAction)
		{
			ActEvolve();
		}
		else
		{
			EndEvolve();
		}
	}

	public void ActEvolve()
	{
		EndAction();
		uint evolveWeaponId = GetEvolveWeaponId();
		base.actionID = (ACTION_ID)37;
		PlayMotion("@evolve_" + evolveWeaponId);
		evolveCtrl.Start(evolveWeaponId);
		if (playerSender != null)
		{
			playerSender.OnSyncEvolveAction(isAction: true);
		}
	}

	private void EndEvolve()
	{
		evolveCtrl.End();
		EndWaitingPacket(WAITING_PACKET.EVOLVE);
		if (playerSender != null)
		{
			playerSender.OnSyncEvolveAction(isAction: false);
		}
	}

	private void UpdateEvolve()
	{
		if (base.actionID == (ACTION_ID)38 && evolveSpecialActionSec > 0f)
		{
			evolveSpecialActionSec -= Time.deltaTime;
			if (evolveSpecialActionSec <= 0f)
			{
				SetNextTrigger();
			}
		}
		if (evolveCtrl.isExec && (IsCoopNone() || IsOriginal()) && evolveCtrl.Update())
		{
			EndEvolve();
		}
	}

	public void ActEvolveSpecialAction()
	{
		EndAction();
		uint evolveWeaponId = GetEvolveWeaponId();
		base.actionID = (ACTION_ID)38;
		PlayMotionImmidate("EVOLVE_" + evolveWeaponId, "special", 0f);
		evolveSpecialActionSec = evolveCtrl.GetSpecialActionSec();
		evolveCtrl.PlaySphinxActionEffect();
		if (playerSender != null)
		{
			playerSender.OnEvolveSpecialAction();
		}
	}

	public bool CanEvolveSpecialFlickAction()
	{
		if (!evolveCtrl.IsExistSpecialAction() || !evolveCtrl.isExec || !enableCancelToEvolveSpecialAction)
		{
			return false;
		}
		return true;
	}

	public bool CanSoulOneHandSwordFlickAction()
	{
		if (!CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
		{
			return false;
		}
		if (!enableSpAttackContinue)
		{
			return false;
		}
		return true;
	}

	public bool CanFlickAction()
	{
		return enableFlickAction;
	}

	public void ActFlickAction(Vector3 inputVec, bool isOriginal)
	{
		if (!isOriginal || (CheckAttackModeAndSpType(ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.BURST) && attackHitCount >= 1))
		{
			EndAction();
			base.actionID = (ACTION_ID)42;
			if (CheckAttackModeAndSpType(ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.BURST))
			{
				pairSwordsCtrl.ActAerialAvoid(inputVec);
				PlayMotionImmidate(null, "attack_32_avoid", 0f);
			}
			if (playerSender != null)
			{
				playerSender.OnActFlickAction(inputVec);
			}
		}
	}

	public void InputNextTrigger()
	{
		if (enableInputNextTrigger && !inputNextTriggerFlag)
		{
			inputNextTriggerFlag = true;
			if (enableNextTriggerTrans)
			{
				DoInputNextTrigger();
			}
		}
	}

	private void DoInputNextTrigger()
	{
		SetNextTrigger(inputNextTriggerIndex);
		enableInputNextTrigger = false;
		enableNextTriggerTrans = false;
		inputNextTriggerFlag = false;
		inputNextTriggerIndex = 0;
	}

	public void StartFieldBuff(uint fieldBuffId)
	{
		buffParam.ClearFieldBuff();
		if (fieldBuffId == 0 || !Singleton<FieldBuffTable>.IsValid() || !Singleton<BuffTable>.IsValid())
		{
			return;
		}
		FieldBuffTable.FieldBuffData data = Singleton<FieldBuffTable>.I.GetData(fieldBuffId);
		if (data == null)
		{
			return;
		}
		for (int i = 0; i < data.buffTableIds.Count; i++)
		{
			BuffTable.BuffData data2 = Singleton<BuffTable>.I.GetData(data.buffTableIds[i]);
			if (data2 != null)
			{
				buffParam.StartFieldBuff(fieldBuffId, data2);
			}
		}
	}

	public void StartBuffByBuffTableId(int id, SkillInfo.SkillParam skillParam)
	{
		if (id >= 0)
		{
			StartBuffByBuffTableId((uint)id, skillParam);
		}
	}

	public void StartBuffByBuffTableIdOnActDeadStandUp(int id, SkillInfo.SkillParam skillParam)
	{
		if (id >= 0)
		{
			buffInfoListOnActDeadStandUp.Add(new KeyValuePair<int, SkillInfo.SkillParam>(id, skillParam));
		}
	}

	public void StartBuffByBuffTableId(uint id, SkillInfo.SkillParam skillParam)
	{
		if (!Singleton<BuffTable>.IsValid())
		{
			return;
		}
		BuffTable.BuffData data = Singleton<BuffTable>.I.GetData(id);
		if (data == null)
		{
			return;
		}
		BuffParam.BuffData buffData = new BuffParam.BuffData();
		buffData.type = data.type;
		buffData.interval = data.interval;
		buffData.valueType = data.valueType;
		buffData.time = data.duration;
		float num = data.value;
		if (skillParam != null && Singleton<GrowSkillItemTable>.IsValid())
		{
			GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(data.growID, skillParam.baseInfo.level, skillParam.baseInfo.exceedCnt);
			if (growSkillItemData != null)
			{
				buffData.time = data.duration * (float)(int)growSkillItemData.supprtTime[0].rate * 0.01f + (float)growSkillItemData.supprtTime[0].add;
				num = (float)(data.value * (int)growSkillItemData.supprtValue[0].rate) * 0.01f + (float)(int)growSkillItemData.supprtValue[0].add;
			}
		}
		if (buffData.valueType == BuffParam.VALUE_TYPE.RATE && BuffParam.IsTypeValueBasedOnHP(buffData.type))
		{
			num = (float)base.hpMax * num * 0.01f;
		}
		buffData.value = Mathf.FloorToInt(num);
		OnBuffStart(buffData);
	}

	public bool IsInSpearBurstBarrier()
	{
		if (burstBarrierCounter > 0)
		{
			return !disableGuard;
		}
		return false;
	}

	public bool IsInBarrier()
	{
		return !bulletBarrierObjList.IsNullOrEmpty();
	}

	public bool IsInAliveBarrier()
	{
		return bulletBarrierObjList.Any((BarrierBulletObject barrier) => !barrier.IsDead());
	}

	private void ForceClearInBarrier()
	{
		if (bulletBarrierObjList.IsNullOrEmpty())
		{
			return;
		}
		int i = 0;
		for (int count = bulletBarrierObjList.Count; i < count; i++)
		{
			BarrierBulletObject barrierBulletObject = bulletBarrierObjList[i];
			barrierBulletObject.RemovePlayer(this);
			barrierBulletObject.ForceExitThroughProcessor(base._collider);
			Transform t = effectTransTable.Get(barrierBulletObject.GetEffectNameInBarrier());
			if (t != null)
			{
				EffectManager.ReleaseEffect(ref t);
				effectTransTable.Remove(barrierBulletObject.GetEffectNameInBarrier());
			}
		}
		bulletBarrierObjList.Clear();
	}

	public void ForceClearByBarrier(BarrierBulletObject barrier)
	{
		if (!bulletBarrierObjList.IsNullOrEmpty())
		{
			bulletBarrierObjList.Remove(barrier);
			Transform t = effectTransTable.Get(barrier.GetEffectNameInBarrier());
			if (t != null)
			{
				EffectManager.ReleaseEffect(ref t);
				effectTransTable.Remove(barrier.GetEffectNameInBarrier());
			}
		}
	}

	public void MakeInvincible(float duration)
	{
		if ((hitOffFlag & HIT_OFF_FLAG.INVICIBLE) <= HIT_OFF_FLAG.NONE)
		{
			hitOffFlag |= HIT_OFF_FLAG.INVICIBLE;
			cancelInvincible = CancelInvincible(duration);
			StartCoroutine(cancelInvincible);
		}
	}

	private IEnumerator CancelInvincible(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		hitOffFlag &= ~HIT_OFF_FLAG.INVICIBLE;
	}

	protected override void OnAttackedContinuationStart(AttackedContinuationStatus status)
	{
		BarrierBulletObject componentInParent = status.fromCollider.gameObject.GetComponentInParent<BarrierBulletObject>();
		if (componentInParent != null)
		{
			bulletBarrierObjList.Add(componentInParent);
			componentInParent.AddPlayer(this);
			string effectNameInBarrier = componentInParent.GetEffectNameInBarrier();
			Transform x = effectTransTable.Get(effectNameInBarrier);
			if (x == null)
			{
				x = EffectManager.GetEffect(effectNameInBarrier, base.rootNode);
				if (x != null)
				{
					effectTransTable.Add(effectNameInBarrier, x);
				}
			}
		}
		if (status.attackInfo.type == AttackContinuationInfo.CONTINUATION_TYPE.SPEAR_BURST)
		{
			burstBarrierCounter++;
		}
	}

	protected override void OnAttackedContinuationEnd(AttackedContinuationStatus status)
	{
		BarrierBulletObject componentInParent = status.fromCollider.gameObject.GetComponentInParent<BarrierBulletObject>();
		if (componentInParent != null)
		{
			bulletBarrierObjList.Remove(componentInParent);
			componentInParent.RemovePlayer(this);
			if (!IsInBarrier())
			{
				Transform t = effectTransTable.Get(componentInParent.GetEffectNameInBarrier());
				if (t != null)
				{
					EffectManager.ReleaseEffect(ref t);
					effectTransTable.Remove(componentInParent.GetEffectNameInBarrier());
				}
			}
		}
		if (status.attackInfo.type == AttackContinuationInfo.CONTINUATION_TYPE.SPEAR_BURST)
		{
			burstBarrierCounter--;
			if (burstBarrierCounter < 0)
			{
				burstBarrierCounter = 0;
			}
		}
	}

	protected override void OnCollisionEnter(Collision collision)
	{
		if ((IsCoopNone() || IsOriginal()) && snatchCtrl.IsMoveLoop())
		{
			int layer = collision.gameObject.layer;
			if (layer == 11 || layer == 10 || layer == 9 || layer == 17 || layer == 18)
			{
				OnSnatchMoveEnd();
			}
		}
	}

	protected override void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.layer == 9 || collision.gameObject.layer == 17 || collision.gameObject.layer == 18)
		{
			isWallStay = true;
		}
		if ((!IsCoopNone() && !IsOriginal()) || !snatchCtrl.IsMoveLoop())
		{
			return;
		}
		int layer = collision.gameObject.layer;
		if (layer == 11 || layer == 10 || layer == 9 || layer == 17 || layer == 18)
		{
			if (!snatchCtrl.IsMoveLoopStart())
			{
				snatchCtrl.ActivateLoopStart();
			}
			else
			{
				OnSnatchMoveEnd();
			}
		}
	}

	public string GetMotionLayerName(ATTACK_MODE _attackMode, SP_ATTACK_TYPE _spAtkType, int _motionId)
	{
		string text = "Base Layer.";
		switch (_attackMode)
		{
		case ATTACK_MODE.ONE_HAND_SWORD:
		{
			InGameSettingsManager.Player.BurstOneHandSwordActionInfo burstOHSInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.burstOHSInfo;
			if (_spAtkType == SP_ATTACK_TYPE.BURST && burstOHSInfo != null)
			{
				bool num = burstOHSInfo.BaseAtkId <= _motionId && _motionId <= burstOHSInfo.AvoidAttackID;
				bool flag = burstOHSInfo.CounterAttackId == _motionId;
				if (num | flag)
				{
					text += "BURST.";
				}
			}
			else if (_spAtkType == SP_ATTACK_TYPE.ORACLE && OneHandSwordController.IsOracleAttackId(_motionId))
			{
				text += "ORACLE.";
			}
			break;
		}
		case ATTACK_MODE.TWO_HAND_SWORD:
		{
			InGameSettingsManager.Player.BurstTwoHandSwordActionInfo burstTHSInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.burstTHSInfo;
			if (_spAtkType == SP_ATTACK_TYPE.BURST && burstTHSInfo != null && burstTHSInfo.BaseAtkId <= _motionId && _motionId <= burstTHSInfo.BurstAvoidAttackID)
			{
				text += "BURST.";
			}
			else if (_spAtkType == SP_ATTACK_TYPE.ORACLE && TwoHandSwordOracleController.IsOracleLayerAttackId(_motionId))
			{
				text += "ORACLE.";
			}
			break;
		}
		case ATTACK_MODE.SPEAR:
			switch (_spAtkType)
			{
			case SP_ATTACK_TYPE.BURST:
				if (MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.burstSpearInfo.baseAtkId <= _motionId)
				{
					text += "BURST.";
				}
				break;
			case SP_ATTACK_TYPE.ORACLE:
				if (SpearController.IsOracleAttackId(_motionId))
				{
					text += "ORACLE.";
				}
				break;
			}
			break;
		case ATTACK_MODE.PAIR_SWORDS:
			if (_spAtkType == SP_ATTACK_TYPE.ORACLE && pairSwordsCtrl.IsOracleAttackId(_motionId))
			{
				text += "ORACLE.";
			}
			break;
		}
		return text;
	}

	protected override int GetColliderGenerateCount(AnimEventData.EventData _eventData)
	{
		if (_eventData == null || _eventData.intArgs == null || _eventData.intArgs.Length < 1)
		{
			return base.GetColliderGenerateCount(_eventData);
		}
		AnimEventFormat.MULTI_COL_GENERATE_CONDITION mULTI_COL_GENERATE_CONDITION = (AnimEventFormat.MULTI_COL_GENERATE_CONDITION)_eventData.intArgs[0];
		if (CheckAttackModeAndSpType(ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.BURST) && thsCtrl != null && mULTI_COL_GENERATE_CONDITION == AnimEventFormat.MULTI_COL_GENERATE_CONDITION.IN_CORN)
		{
			return thsCtrl.CurrentRestBulletCount;
		}
		return base.GetColliderGenerateCount(_eventData);
	}

	public override int GetObservedID()
	{
		string s = (id % 100000).ToString() + bulletIndex.ToString();
		int result = -1;
		if (int.TryParse(s, out result))
		{
			bulletIndex++;
			return result;
		}
		return -1;
	}

	public override void OnSetSearchTarget(int observedID, int targetID)
	{
		if (bulletObservableList.IsNullOrEmpty() || bulletObservableIdList.IsNullOrEmpty() || !bulletObservableIdList.Contains(observedID))
		{
			return;
		}
		int num = 0;
		while (true)
		{
			if (num < bulletObservableList.Count)
			{
				if (bulletObservableList[num].GetObservedID() == observedID)
				{
					break;
				}
				num++;
				continue;
			}
			return;
		}
		BulletObject bulletObject = bulletObservableList[num] as BulletObject;
		if (bulletObject != null)
		{
			bulletObject.SetSearchTarget(targetID);
		}
	}

	public void RegistBulletTurret(uint id, BulletControllerTurretBit bit)
	{
		if (bulletTurretList == null)
		{
			return;
		}
		if (bulletTurretList.ContainsKey(id))
		{
			BulletControllerTurretBit bulletControllerTurretBit = bulletTurretList[id];
			if (bulletControllerTurretBit != null)
			{
				UnityEngine.Object.Destroy(bulletControllerTurretBit.gameObject);
			}
			bulletTurretList.Remove(id);
		}
		bulletTurretList.Add(id, bit);
	}

	public void RemoveAllBulletTurret()
	{
		if (bulletTurretList != null && bulletTurretList.Count != 0)
		{
			foreach (KeyValuePair<uint, BulletControllerTurretBit> bulletTurret in bulletTurretList)
			{
				if (bulletTurret.Value != null && bulletTurret.Value.gameObject != null)
				{
					UnityEngine.Object.Destroy(bulletTurret.Value.gameObject);
				}
			}
			bulletTurretList.Clear();
		}
	}

	public override void OnSetTurretBitTarget(int observedID, int targetID, int regionID)
	{
		if (bulletObservableList.IsNullOrEmpty() || bulletObservableIdList.IsNullOrEmpty() || !bulletObservableIdList.Contains(observedID))
		{
			return;
		}
		int num = 0;
		while (true)
		{
			if (num < bulletObservableList.Count)
			{
				if (bulletObservableList[num].GetObservedID() == observedID)
				{
					break;
				}
				num++;
				continue;
			}
			return;
		}
		BulletObject bulletObject = bulletObservableList[num] as BulletObject;
		if (bulletObject != null)
		{
			bulletObject.SetTurretBitTarget(targetID, regionID);
		}
	}

	public virtual void EventArrowRainChargeStart(AnimEventData.EventData data)
	{
		if (!enableInputCharge)
		{
			rainShotState = RAIN_SHOT_STATE.START_CHARGE;
			enableInputCharge = true;
			isArrowAimable = true;
			inputChargeAutoRelease = false;
			inputChargeMaxTiming = true;
			inputChargeTimeMax = data.floatArgs[0] * (1f - GetChargeArrowTimeRate());
			inputChargeTimeOffset = 0f;
			inputChargeTimeCounter = 0f;
			isInputChargeExistOffset = false;
			chargeRate = 0f;
			isChargeExRush = false;
			exRushChargeRate = 0f;
			StartWaitingPacket(WAITING_PACKET.PLAYER_CHARGE_RELEASE, keep_sync: true);
			CheckInputCharge();
		}
	}

	public virtual void RainShotChargeRelease()
	{
		rainShotState = RAIN_SHOT_STATE.FINISH_CHARGE;
		arrowRainTargetPointList = targetingPointList.GetRange(0, targetingPointList.Count);
	}

	public void OnRainShotChargeRelease(Vector3 pos, float rotY)
	{
		rainShotFallPosition = pos;
		rainShotFallRotateY = rotY;
		if (playerSender != null)
		{
			playerSender.OnRainShotChargeRelease(pos, rotY);
		}
	}

	public virtual void EventArrowRainStart(AnimEventData.EventData data, bool visibled = false)
	{
		if ((rainShotState != RAIN_SHOT_STATE.FINISH_CHARGE && rainShotState != RAIN_SHOT_STATE.START_RAIN) || data.floatArgs.Length <= 6 || data.intArgs.Length <= 1)
		{
			return;
		}
		rainShotState = RAIN_SHOT_STATE.START_RAIN;
		int num = data.intArgs[0];
		int num2 = Mathf.CeilToInt((float)data.intArgs[1] * buffParam.GetArrowRainNumRate());
		float num3 = data.floatArgs[6];
		Vector3 vector = rainShotFallPosition + new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
		Vector3 rot = new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
		float arrowRainLengthDivide = playerParameter.arrowActionInfo.arrowRainLengthDivide;
		float arrowRainAngleDivide = playerParameter.arrowActionInfo.arrowRainAngleDivide;
		int arrowRainMaxFrameInterval = playerParameter.arrowActionInfo.arrowRainMaxFrameInterval;
		string text = playerParameter.arrowActionInfo.arrowRainShotAttackInfoName;
		if (isBoostMode)
		{
			text += "_boost";
		}
		AttackInfo attackInfo = FindAttackInfo(text);
		attackInfo.rateInfoRate = GetChargingRate();
		for (int i = 0; i < num2; i++)
		{
			Vector3 pos = vector;
			if (i > 0)
			{
				float z = (float)(UnityEngine.Random.Range(0, Mathf.FloorToInt(num3 / arrowRainLengthDivide)) + 1) * arrowRainLengthDivide;
				float y = (float)UnityEngine.Random.Range(0, Mathf.FloorToInt(360f / arrowRainAngleDivide)) * arrowRainAngleDivide;
				pos += Quaternion.Euler(new Vector3(0f, y, 0f)) * new Vector3(0f, 0f, z);
			}
			StartCoroutine(DelayShotArrowRain(num, pos, rot, attackInfo));
			num += UnityEngine.Random.Range(0, arrowRainMaxFrameInterval);
		}
	}

	private IEnumerator DelayShotArrowRain(int delay, Vector3 pos, Vector3 rot, AttackInfo attackInfo)
	{
		while (delay > 0)
		{
			delay--;
			yield return null;
		}
		ShotArrow(pos, Quaternion.Euler(rot), attackInfo, isSitShot: true, isAimEnd: false, isSend: false);
	}

	public void OnShotShieldReflect(ShieldReflectInfo reflectInfo)
	{
		if (IsOriginal() || IsCoopNone())
		{
			Coop_Model_PlayerShotShieldReflect coop_Model_PlayerShotShieldReflect = new Coop_Model_PlayerShotShieldReflect();
			coop_Model_PlayerShotShieldReflect.id = id;
			coop_Model_PlayerShotShieldReflect.atkInfoName = reflectInfo.attackInfoName;
			coop_Model_PlayerShotShieldReflect.offsetPos = reflectInfo.offsetPos;
			coop_Model_PlayerShotShieldReflect.offsetRot = reflectInfo.offsetRot;
			coop_Model_PlayerShotShieldReflect.damage = reflectInfo.damage;
			coop_Model_PlayerShotShieldReflect.targetId = reflectInfo.targetId;
			if (reflectInfo.seId > 0)
			{
				SoundManager.PlayOneShotSE(reflectInfo.seId, this, FindNode(""));
			}
			OnShotShieldReflect(coop_Model_PlayerShotShieldReflect);
		}
	}

	public void OnShotShieldReflect(Coop_Model_PlayerShotShieldReflect model)
	{
		AttackHitInfo attackHitInfo = FindAttackInfo(model.atkInfoName, fix_rate: true, isDuplicate: true) as AttackHitInfo;
		if (attackHitInfo == null || attackHitInfo.bulletData == null)
		{
			return;
		}
		StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.FindEnemy(model.targetId);
		if (!(stageObject == null))
		{
			if (attackHitInfo.attackType == AttackHitInfo.ATTACK_TYPE.SHIELD_REFLECT)
			{
				attackHitInfo.atk.normal = model.damage;
			}
			Vector3 forward = stageObject._transform.position - base._transform.position;
			forward.y = 0f;
			Quaternion quaternion = Quaternion.LookRotation(forward);
			Vector3 pos = base._transform.position + quaternion * model.offsetPos;
			quaternion *= Quaternion.Euler(model.offsetRot);
			AnimEventShot.Create(this, attackHitInfo, pos, quaternion).SetTarget(stageObject);
			if (playerSender != null)
			{
				playerSender.OnShotShieldReflect(model);
			}
		}
	}

	public override bool IsCarrying()
	{
		return carryingGimmickObject != null;
	}

	public void ActCarry(InGameProgress.eFieldGimmick type, int pointId)
	{
		if (EndCarryIfSameAsOneOfSelf(pointId) || IsCarrying())
		{
			return;
		}
		EndAction();
		targetingGimmickObject = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(type, pointId);
		if (targetingGimmickObject is FieldCarriableGimmickObject)
		{
			carryingGimmickObject = (targetingGimmickObject as FieldCarriableGimmickObject);
		}
		else if (targetingGimmickObject is FieldSupplyGimmickObject)
		{
			carryingGimmickObject = (targetingGimmickObject as FieldSupplyGimmickObject).SupplyGimmick();
		}
		if (!(carryingGimmickObject == null))
		{
			base.actionID = (ACTION_ID)44;
			PlayMotion(142);
			Vector3 vector = Vector3.Normalize(carryingGimmickObject.GetTransform().position - base._transform.position);
			vector.y = 0f;
			SetLerpRotation(vector);
			Vector3 position = base._transform.position + Quaternion.LookRotation(vector) * FieldCarriableGimmickObject.kCarryOffset;
			if (IsCoopNone() || IsOriginal())
			{
				SetActionPosition(position, flag: true);
			}
			carryingGimmickObject.GetTransform().position = position;
			carryingGimmickObject.StartCarry(this);
			if (playerSender != null)
			{
				playerSender.OnActCarry(type, pointId);
			}
		}
	}

	public void ActCarryIdle()
	{
		if (!IsCarrying() || EndCarryIfSameAsOneOfSelf(carryingGimmickObject.GetId()))
		{
			ActIdle();
		}
		EndAction();
		base.isControllable = true;
		base.actionID = (ACTION_ID)44;
		PlayMotion(143);
		if (playerSender != null)
		{
			playerSender.OnActCarryIdle();
		}
	}

	public void ActCarryWalk(Vector3 velocity, float syncSpeed, Vector3 moveVec)
	{
		if (!IsCarrying() || EndCarryIfSameAsOneOfSelf(carryingGimmickObject.GetId()))
		{
			ActIdle();
		}
		ActMoveVelocity(velocity, syncSpeed, (MOTION_ID)144);
		SetLerpRotation(moveVec);
		isCarryWalk = true;
	}

	public void ActCarryPut(int pointId = 0)
	{
		if (!IsCarrying() || EndCarryIfSameAsOneOfSelf(carryingGimmickObject.GetId()))
		{
			ActIdle();
		}
		EndAction();
		if (pointId > 0)
		{
			targetingGimmickObject = (MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.CarriableGimmick, pointId) as FieldCarriableGimmickObject);
			if (targetingGimmickObject != null)
			{
				base._transform.rotation = Quaternion.LookRotation(targetingGimmickObject.GetTransform().position - base._transform.position, Vector3.up);
			}
		}
		PlayMotion(145);
		if (playerSender != null)
		{
			playerSender.OnActCarryPut(pointId);
		}
	}

	public void EventStartCarryGimmick()
	{
		if (!(carryingGimmickObject == null))
		{
			Transform transform = FindNode(FieldCarriableGimmickObject.kCarryNode);
			if (transform != null)
			{
				carryingGimmickObject.GetTransform().SetParent(transform);
				carryingGimmickObject.GetTransform().localPosition = Vector3.zero;
			}
			targetingGimmickObject = null;
		}
	}

	public void EventEndCarryGimmick()
	{
		if (IsCarrying() && !(carryingGimmickObject == null))
		{
			FieldCarriableGimmickObject fieldCarriableGimmickObject = targetingGimmickObject as FieldCarriableGimmickObject;
			FieldCarriableEvolveItemGimmickObject fieldCarriableEvolveItemGimmickObject = carryingGimmickObject as FieldCarriableEvolveItemGimmickObject;
			if (fieldCarriableEvolveItemGimmickObject != null && fieldCarriableGimmickObject != null && fieldCarriableGimmickObject.CanEvolve())
			{
				fieldCarriableEvolveItemGimmickObject.Use2Evolve(fieldCarriableGimmickObject);
				MonoBehaviourSingleton<UIPlayerAnnounce>.I.Announce(UIPlayerAnnounce.ANNOUNCE_TYPE.GIMMICK_EVOLVE, this);
			}
			EndCarry();
		}
	}

	public void EndCarry()
	{
		if (IsCarrying())
		{
			carryingGimmickObject.EndCarry();
			carryingGimmickObject = null;
			targetingGimmickObject = null;
		}
	}

	public bool EndCarryIfSameAsOneOfSelf(int pointId)
	{
		Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
		if (self == null || self.id == id)
		{
			return false;
		}
		IFieldGimmickObject fieldGimmickObject = self.targetingGimmickObject;
		if (fieldGimmickObject == null)
		{
			fieldGimmickObject = self.carryingGimmickObject;
		}
		if (fieldGimmickObject != null && fieldGimmickObject.GetId() == pointId)
		{
			EndCarry();
			self.EndCarry();
			self.ActIdle();
			return true;
		}
		return false;
	}

	public void ActiveShadow(bool isActive)
	{
		StartCoroutine(IEActiveShadow(isActive));
	}

	private IEnumerator IEActiveShadow(bool isActive)
	{
		while (shadow == null && (loader == null || loader.isLoading))
		{
			yield return null;
		}
		shadow = GetComponentInChildren<CircleShadow>(includeInactive: true);
		if (shadow != null)
		{
			shadow.gameObject.SetActive(isActive);
			if (isActive)
			{
				shadow = null;
			}
		}
	}
}
