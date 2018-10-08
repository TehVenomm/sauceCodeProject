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
		AVOID = 12,
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

		public unsafe bool IsBoost()
		{
			bool[] source = isBoostByTypes;
			if (_003C_003Ef__am_0024cache3 == null)
			{
				_003C_003Ef__am_0024cache3 = new Func<bool, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			return source.Any(_003C_003Ef__am_0024cache3);
		}

		public unsafe bool IsNoBoost()
		{
			bool[] source = isBoostByTypes;
			if (_003C_003Ef__am_0024cache4 == null)
			{
				_003C_003Ef__am_0024cache4 = new Func<bool, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			return source.All(_003C_003Ef__am_0024cache4);
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

	private enum eChargeState
	{
		None,
		Charge,
		Full
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

	protected const int MAX_WEAPON_TYPE_COUNT = 5;

	protected const string ANIMATOR_SUB_STATE_BURST_TYPE = "BURST.";

	private const float REVIVAL_TIME = 3f;

	public const float BODY_HEIGHT = 1.7f;

	private const string NODE_WEAPON_RIGHT = "weaponR";

	private const string NODE_WEAPON_LEFT = "weaponL";

	private const string EFFECT_NAME_COUNTER_ATTACK = "ef_btl_ab_charge_01";

	public const string ARROW_ATTACK = "PLC05_attack_00";

	protected const int ARROW_STAND_SHOT_ATTACKID = 1;

	private const int ONEHANDSWORD_HEAT_COUNTER_ATTACKID = 98;

	private const int ONEHANDSWORD_HEAT_REVENGEBURST_ATTACKID = 96;

	private const int ONEHANDSWORD_SOUL_ATTACKID = 10;

	protected const int PAIR_SWORDS_AVOID_ATTACKID = 20;

	protected const int PAIR_SWORDS_BOOST_MODE_ATTACK_ID = 98;

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

	protected const string REVIVAL_RANGE_EFFECT = "ef_btl_rebirth_area_01";

	protected const string REVIVAL_EFFECT = "ef_btl_rebirth_01";

	public InGameRecorder.PlayerRecord record;

	protected int localDecoyId;

	protected SoulEnergyController soulEnergyCtrl;

	private BitArray disableActionFlag = new BitArray(40);

	public static readonly string[] subMotionStateName;

	private static int[] subMotionHashCaches;

	private static readonly int guardAngleID;

	private static readonly int arrowAngleID;

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

	protected Vector3 jumpFallBodyPosition = default(Vector3);

	protected Vector3 jumpRandingVector = default(Vector3);

	protected Vector3 jumpRaindngBasePos = default(Vector3);

	protected float jumpRandingBaseBodyY;

	public int useGaugeLevel;

	protected eJumpState jumpState;

	protected bool isArrowSitShot;

	private float hitSpAttackContinueTimer;

	private bool isLockedSpAttackContinue;

	private SelfController.FLICK_DIRECTION flickDirection;

	private Transform twoHandSwordsBoostLoopEffect;

	private Transform twoHandSwordsChargeMaxEffect;

	protected GameObject revivalRangEffect;

	public float deadStartTime = -1f;

	public float deadStopTime = -1f;

	protected float _rescueTime;

	public bool isInitDead;

	public float initRescueTime;

	public float initContinueTime;

	protected float prayerTime;

	private List<BoostPrayInfo> boostPrayTargetInfoList = new List<BoostPrayInfo>();

	private List<BoostPrayInfo> boostPrayedInfoList = new List<BoostPrayInfo>();

	private XorInt _autoReviveHp = 0;

	public UIPlayerStatusGizmo uiPlayerStatusGizmo;

	public List<TargetPoint> targetPointWithSpWeakList = new List<TargetPoint>();

	protected Dictionary<string, float> defaultBulletSpeedDic = new Dictionary<string, float>();

	protected CharaInfo.EquipItem changeWeaponItem;

	protected int changeWeaponIndex = -1;

	protected bool isChangingWeapon;

	protected float changeWeaponStartTime = -1f;

	private List<int> prayerEndIds = new List<int>();

	public bool isGatherInterruption;

	protected bool isSyncingCannonRotation;

	protected Quaternion syncCannonRotation = Quaternion.get_identity();

	protected Quaternion prevCannonRotation = Quaternion.get_identity();

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

	protected FieldGatherGimmickObject gatherGimmickObject;

	protected List<IWeaponController> m_weaponCtrlList = new List<IWeaponController>(5);

	public PairSwordsController pairSwordsCtrl;

	public SpearController spearCtrl;

	public TwoHandSwordController thsCtrl;

	private List<Transform> pairSwordsBoostModeAuraEffectList = new List<Transform>(2);

	private List<Transform> pairSwordsBoostModeTrailEffectList = new List<Transform>(2);

	private Transform buffShadowSealingEffect;

	private EnemyBrain _bossBrain;

	private Transform ohsChargeEffect;

	private Transform ohsMaxChargeEffect;

	private bool isJustGuard;

	private bool isSuccessParry;

	private bool notEndGuardFlag;

	private float guardingSec;

	private float revengeChargeSec;

	private eChargeState revengeChargeState;

	private AttackRestraintObject m_attackRestraint;

	private RestraintInfo m_restraintInfo;

	private float m_restrainTime;

	private float m_restraintDamgeTimer;

	private int m_restrainDamageValue;

	private DrainAttackInfo grabDrainAtkInfo;

	private float grabDrainDamageTimer;

	private List<int> a_ids = new List<int>();

	private List<int> a_pts = new List<int>();

	private List<BuffParam.BUFFTYPE> passiveSkillList = new List<BuffParam.BUFFTYPE>();

	private float timeWhenJustGuardChecked = -1f;

	private List<AttackNWayLaser> activeAttackLaserList = new List<AttackNWayLaser>();

	private Transform exRushChargeEffect;

	protected float evolveSpecialActionSec;

	private List<BarrierBulletObject> bulletBarrierObjList = new List<BarrierBulletObject>();

	public BarrierBulletObject activeBulletBarrierObject;

	private IEnumerator cancelInvincible;

	public override int id
	{
		get
		{
			return base.id;
		}
		set
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			base.id = value;
			this.get_gameObject().set_name("Player:" + value);
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

	public bool isJumpAction => jumpState != eJumpState.None;

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

	public bool isGuardAttackMode
	{
		get
		{
			if (CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL))
			{
				return false;
			}
			return (attackMode == ATTACK_MODE.ONE_HAND_SWORD) ? true : false;
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
			return (attackMode == ATTACK_MODE.ARROW) ? true : false;
		}
	}

	public bool isSpearAttackMode => (attackMode == ATTACK_MODE.SPEAR) ? true : false;

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
			return _rescueTime - (Time.get_time() - deadStartTime);
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

	public List<int> prayTargetIds
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

	public bool isNpc => base.controller != null && base.controller is NpcController;

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
			if (!object.ReferenceEquals(_bossBrain, null))
			{
				return _bossBrain;
			}
			if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				return null;
			}
			Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
			if (object.ReferenceEquals(boss, null))
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
		protected set;
	}

	public bool isActTwoHandSwordHeatCombo => base.attackID == 89 || base.attackID == 88;

	public bool isActPairSwordsSoulLaser => base.attackID == playerParameter.pairSwordsActionInfo.Soul_SpLaserShotAttackId;

	public TargetPoint RestraintTargetPoint => (!(m_attackRestraint != null)) ? null : m_attackRestraint.BreakTargetPoint;

	public Player()
	{
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		base.objectType = OBJECT_TYPE.PLAYER;
		attackMode = ATTACK_MODE.NONE;
		spAttackType = SP_ATTACK_TYPE.NONE;
		extraAttackType = EXTRA_ATTACK_TYPE.NONE;
		weaponData = null;
		weaponIndex = -1;
		hpUp = 0;
		healHp = 0;
		healHpSpeed = 0f;
		enableInputCombo = false;
		controllerInputCombo = false;
		enableComboTrans = false;
		enableTap = false;
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
		_rescueTime = 0f;
		rescueCount = 0;
		continueTime = 0f;
		deadStartTime = -1f;
		deadStopTime = -1f;
		autoReviveCount = 0;
		autoReviveHp = 0;
		isStopCounter = false;
		prayTargetIds = new List<int>();
		prayerTime = 0f;
		prayerIds = new List<int>();
		boostPrayTargetInfoList.Clear();
		boostPrayedInfoList.Clear();
		healEffectTransform = null;
		skillChargeEffectTransform = null;
		targetingPointList = new List<TargetPoint>();
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
		isBoostMode = false;
		boostModeDamageUpLevel = 0;
		boostModeDamageUpHitCount = 0;
		_bossBrain = null;
		isJustGuard = false;
		isSuccessParry = false;
		guardingSec = 0f;
		revengeChargeSec = 0f;
		isBuffShadowSealing = false;
		healAtkRate = 0f;
		localDecoyId = 0;
		isAbsorbDamageSuperArmor = false;
		evolveCtrl = new EvolveController();
		snatchCtrl = new SnatchController();
		fishingCtrl = new FishingController();
		m_weaponCtrlList.Clear();
		pairSwordsCtrl = new PairSwordsController();
		m_weaponCtrlList.Add(pairSwordsCtrl);
		spearCtrl = new SpearController();
		m_weaponCtrlList.Add(spearCtrl);
		thsCtrl = new TwoHandSwordController();
		m_weaponCtrlList.Add(thsCtrl);
	}

	static Player()
	{
		subMotionStateName = new string[24]
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
			"fishing"
		};
		subMotionHashCaches = new int[24];
		guardAngleID = 0;
		arrowAngleID = 0;
		guardAngleID = Animator.StringToHash("guard_angle");
		arrowAngleID = Animator.StringToHash("arrow_angle");
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
		if (playerParameter.rescueTimes.Length <= 0)
		{
			return false;
		}
		int num = rescueCount;
		float num2 = 0f;
		if (num >= playerParameter.rescueTimes.Length)
		{
			num = playerParameter.rescueTimes.Length;
		}
		num2 = playerParameter.rescueTimes[num];
		return num2 > 0f;
	}

	public bool IsPrayed()
	{
		return prayerIds.Count > 0;
	}

	public unsafe bool IsBoostByType(BOOST_PRAY_TYPE type)
	{
		if (boostPrayedInfoList.IsNullOrEmpty())
		{
			return false;
		}
		_003CIsBoostByType_003Ec__AnonStorey50A _003CIsBoostByType_003Ec__AnonStorey50A;
		if (boostPrayedInfoList.Count(new Func<BoostPrayInfo, bool>((object)_003CIsBoostByType_003Ec__AnonStorey50A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)) > 0)
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
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
			OnBuffEnd(BuffParam.BUFFTYPE.SHIELD, true, true);
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
		return CurrentWeaponSpActionGaugeMax > 0f && !IsBurstTwoHandSword();
	}

	public bool IsValidSpActionMemori()
	{
		return CheckAttackModeAndSpType(ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.HEAT) || CheckAttackModeAndSpType(ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.HEAT) || CheckAttackModeAndSpType(ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL);
	}

	public bool IsValidBurstBulletUI()
	{
		return IsBurstTwoHandSword();
	}

	public bool IsBurstTwoHandSword()
	{
		return CheckAttackModeAndSpType(ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.BURST);
	}

	public void ClearStoreEffect()
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		if (MonoBehaviourSingleton<EffectManager>.IsValid())
		{
			if (!pairSwordsBoostModeAuraEffectList.IsNullOrEmpty())
			{
				for (int i = 0; i < pairSwordsBoostModeAuraEffectList.Count; i++)
				{
					if (!(pairSwordsBoostModeAuraEffectList[i] == null))
					{
						EffectManager.ReleaseEffect(pairSwordsBoostModeAuraEffectList[i].get_gameObject(), true, false);
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
						EffectManager.ReleaseEffect(pairSwordsBoostModeTrailEffectList[j].get_gameObject(), true, false);
						pairSwordsBoostModeTrailEffectList[j] = null;
					}
				}
				pairSwordsBoostModeTrailEffectList.Clear();
			}
			effectTransTable.ForEach(delegate(Transform value)
			{
				ReleaseEffect(ref value, true);
			});
			effectTransTable.Clear();
			ReleaseEffect(ref twoHandSwordsChargeMaxEffect, true);
			ReleaseEffect(ref twoHandSwordsBoostLoopEffect, true);
			ReleaseEffect(ref ohsChargeEffect, true);
			ReleaseEffect(ref ohsMaxChargeEffect, true);
			ReleaseEffect(ref buffShadowSealingEffect, true);
			ReleaseEffect(ref exRushChargeEffect, true);
		}
	}

	private void ActivateStoredEffect()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		if (!pairSwordsBoostModeAuraEffectList.IsNullOrEmpty())
		{
			for (int i = 0; i < pairSwordsBoostModeAuraEffectList.Count; i++)
			{
				if (!(pairSwordsBoostModeAuraEffectList[i] == null))
				{
					pairSwordsBoostModeAuraEffectList[i].get_gameObject().SetActive(true);
				}
			}
		}
		if (!pairSwordsBoostModeTrailEffectList.IsNullOrEmpty())
		{
			for (int j = 0; j < pairSwordsBoostModeTrailEffectList.Count; j++)
			{
				if (!(pairSwordsBoostModeTrailEffectList[j] == null))
				{
					pairSwordsBoostModeTrailEffectList[j].get_gameObject().SetActive(true);
					pairSwordsBoostModeTrailEffectList[j].GetComponentsInChildren<Trail>(Temporary.trailList);
					for (int k = 0; k < Temporary.trailList.Count; k++)
					{
						Temporary.trailList[k].Reset();
					}
					Temporary.trailList.Clear();
				}
			}
		}
	}

	private void DeactivateStoredEffect()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		if (!pairSwordsBoostModeAuraEffectList.IsNullOrEmpty())
		{
			for (int i = 0; i < pairSwordsBoostModeAuraEffectList.Count; i++)
			{
				if (!(pairSwordsBoostModeAuraEffectList[i] == null))
				{
					pairSwordsBoostModeAuraEffectList[i].get_gameObject().SetActive(false);
				}
			}
		}
		if (!pairSwordsBoostModeTrailEffectList.IsNullOrEmpty())
		{
			for (int j = 0; j < pairSwordsBoostModeTrailEffectList.Count; j++)
			{
				if (!(pairSwordsBoostModeTrailEffectList[j] == null))
				{
					pairSwordsBoostModeTrailEffectList[j].get_gameObject().SetActive(false);
				}
			}
		}
	}

	public EnemyHitTypeTable.TypeData GetOverrideHitEffect(AttackedHitStatusDirection status, ref Vector3 scale, ref float delay)
	{
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		if (IsSoulOneHandSwordBoostMode())
		{
			EnemyHitTypeTable.TypeData typeData = new EnemyHitTypeTable.TypeData();
			InGameSettingsManager.Player.OneHandSwordActionInfo ohsActionInfo = playerParameter.ohsActionInfo;
			scale.Set(0.5f, 0.5f, 0.5f);
			if (!ohsActionInfo.Soul_BoostElementHitEffect.IsNullOrEmpty())
			{
				for (int i = 0; i < ohsActionInfo.Soul_BoostElementHitEffect.Length; i++)
				{
					typeData.elementEffectNames[i] = ohsActionInfo.Soul_BoostElementHitEffect[i];
				}
			}
			return typeData;
		}
		if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.COUNTER_BURST)
		{
			EnemyHitTypeTable.TypeData typeData2 = new EnemyHitTypeTable.TypeData();
			InGameSettingsManager.Player.BurstOneHandSwordActionInfo burstOHSInfo = playerParameter.ohsActionInfo.burstOHSInfo;
			scale.Set(0.5f, 0.5f, 0.5f);
			if (!burstOHSInfo.BoostElementHitEffect.IsNullOrEmpty())
			{
				for (int j = 0; j < burstOHSInfo.BoostElementHitEffect.Length; j++)
				{
					typeData2.elementEffectNames[j] = burstOHSInfo.BoostElementHitEffect[j];
				}
			}
			return typeData2;
		}
		if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.BURST_THS_SINGLE_SHOT)
		{
			EnemyHitTypeTable.TypeData typeData3 = new EnemyHitTypeTable.TypeData();
			InGameSettingsManager.Player.BurstTwoHandSwordActionInfo burstTHSInfo = playerParameter.twoHandSwordActionInfo.burstTHSInfo;
			if (!burstTHSInfo.HitEffect_SingleShot.IsNullOrEmpty())
			{
				for (int k = 0; k < burstTHSInfo.HitEffect_SingleShot.Length; k++)
				{
					typeData3.elementEffectNames[k] = burstTHSInfo.HitEffect_SingleShot[k];
				}
			}
			return typeData3;
		}
		if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.BURST_THS_FULL_BURST)
		{
			EnemyHitTypeTable.TypeData typeData4 = new EnemyHitTypeTable.TypeData();
			InGameSettingsManager.Player.BurstTwoHandSwordActionInfo burstTHSInfo2 = playerParameter.twoHandSwordActionInfo.burstTHSInfo;
			if (!burstTHSInfo2.HitEffect_FullBurst.IsNullOrEmpty())
			{
				for (int l = 0; l < burstTHSInfo2.HitEffect_FullBurst.Length; l++)
				{
					typeData4.elementEffectNames[l] = burstTHSInfo2.HitEffect_FullBurst[l];
				}
			}
			return typeData4;
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
		EnemyHitTypeTable.TypeData typeData5 = null;
		InGameSettingsManager.Player.SpearActionInfo spearActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo;
		switch (num)
		{
		default:
			typeData5 = Singleton<EnemyHitTypeTable>.I.GetData("TYPE_STAB_S", FieldManager.IsValidInGameNoQuest());
			break;
		case 1:
			typeData5 = Singleton<EnemyHitTypeTable>.I.GetData("TYPE_STAB_L", FieldManager.IsValidInGameNoQuest());
			break;
		case 2:
			scale.Set(0.8f, 0.8f, 0.8f);
			goto case 3;
		case 3:
		{
			typeData5 = new EnemyHitTypeTable.TypeData();
			int m = 0;
			for (int num2 = spearActionInfo.jumpHugeElementHitEffectNames.Length; m < num2; m++)
			{
				typeData5.elementEffectNames[m] = spearActionInfo.jumpHugeElementHitEffectNames[m];
			}
			for (int num3 = 6; m < num3; m++)
			{
				typeData5.elementEffectNames[m] = spearActionInfo.jumpHugeHitEffectName;
			}
			typeData5.baseEffectName = spearActionInfo.jumpHugeHitEffectName;
			break;
		}
		}
		if (num == 3)
		{
			delay = 0.15f;
			EffectManager.OneShot("ef_btl_wsk_spear_01_03", status.hitPos, Quaternion.get_identity(), true);
		}
		return typeData5;
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
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		base.Awake();
		loader = this.get_gameObject().AddComponent<PlayerLoader>();
		this.get_gameObject().set_layer(8);
		if (base._rigidbody == null)
		{
			base._rigidbody = this.get_gameObject().AddComponent<Rigidbody>();
		}
		base._rigidbody.set_mass(1f);
		base._rigidbody.set_angularDrag(100f);
		base._rigidbody.set_isKinematic(false);
		base._rigidbody.set_constraints(116);
		base._rigidbody.set_collisionDetectionMode(1);
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			playerParameter = MonoBehaviourSingleton<InGameSettingsManager>.I.player;
			base.damageHealRate = playerParameter.damegeHealRate;
			buffParameter = MonoBehaviourSingleton<InGameSettingsManager>.I.buff;
		}
		if (playerParameter != null)
		{
			if (base.hpMax == 0)
			{
				int num = healHp = playerParameter.hpMax;
				num = (base.hp = (base.hpMax = num));
				healHpSpeed = (float)playerParameter.hpHealSpeed;
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
		snatchCtrl.Init(this, this.get_gameObject().AddComponent<SnatchLineRenderer>());
		int j = 0;
		for (int count = m_weaponCtrlList.Count; j < count; j++)
		{
			m_weaponCtrlList[j].Init(this);
		}
		fishingCtrl.Initialize(this);
		TwoHandSwordController.InitParam initParam = new TwoHandSwordController.InitParam();
		initParam.Owner = this;
		initParam.BurstInitParam = new TwoHandSwordBurstController.InitParam
		{
			Owner = this,
			ActionInfo = playerParameter.twoHandSwordActionInfo,
			MaxBulletCount = 6,
			CurrentRestBullets = null
		};
		TwoHandSwordController.InitParam param = initParam;
		thsCtrl.InitAppend(param);
	}

	public override void OnLoadComplete()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Expected O, but got Unknown
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		if (base._collider == null)
		{
			CapsuleCollider val = this.get_gameObject().AddComponent<CapsuleCollider>();
			val.set_direction(1);
			val.set_height(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.height);
			val.set_radius(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerVisual.radius);
			val.set_center(new Vector3(0f, val.get_height() * 0.5f, 0f));
			val.get_material().set_dynamicFriction(MonoBehaviourSingleton<InGameSettingsManager>.I.player.friction);
			val.get_material().set_staticFriction(MonoBehaviourSingleton<InGameSettingsManager>.I.player.friction);
			val.get_material().set_frictionCombine(2);
			base._collider = val;
		}
		base.OnLoadComplete();
		SetAnimUpdatePhysics(this is Self);
		if (stepCtrl != null)
		{
			stepCtrl.stampDistance = playerParameter.stampDistance;
		}
		_physics = Utility.Find(this.get_transform(), "SoftPhysics");
		if (_physics != null && MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			_physics.get_transform().set_parent(MonoBehaviourSingleton<StageObjectManager>.I.physicsRoot);
		}
		if (initBuffSyncParam != null)
		{
			buffParam.SetSyncParam(initBuffSyncParam, true);
			initBuffSyncParam = null;
		}
		else
		{
			buffParam.PlayBuffLoopEffectAll();
		}
		if (base.hp <= 0 && isInitDead)
		{
			ActDeadLoop(true, initRescueTime, initContinueTime);
			isInitDead = false;
		}
		snatchCtrl.OnLoadComplete();
		int i = 0;
		for (int count = m_weaponCtrlList.Count; i < count; i++)
		{
			m_weaponCtrlList[i].OnLoadComplete();
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
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		if (enableInputCharge)
		{
			inputChargeTimeCounter += Time.get_deltaTime();
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
			timerChargeExpand += Time.get_deltaTime();
			if (isChargeExpandAutoRelease && timerChargeExpand >= timeChargeExpandMax && (IsCoopNone() || IsOriginal()))
			{
				SetChargeExpandRelease(1f);
			}
		}
		if (isActSpecialAction)
		{
			actSpecialActionTimer += Time.get_deltaTime();
		}
		UpdateCannonCharge();
		UpdateSpearAction();
		snatchCtrl.Update();
		if (!object.ReferenceEquals(fishingCtrl, null))
		{
			fishingCtrl.Update();
		}
		int i = 0;
		for (int count = m_weaponCtrlList.Count; i < count; i++)
		{
			m_weaponCtrlList[i].Update();
		}
		if (_IsGuard())
		{
			_UpdateGuard();
		}
		if (isHitSpAttack)
		{
			hitSpAttackContinueTimer += Time.get_deltaTime();
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
		if (healHp > base.hp && !buffParam.IsValidBuff(BuffParam.BUFFTYPE.CANT_HEAL_HP) && (float)buffParam.GetValue(BuffParam.BUFFTYPE.POISON, true) <= 0f && (float)buffParam.GetValue(BuffParam.BUFFTYPE.DEADLY_POISON, true) <= 0f && (float)buffParam.GetValue(BuffParam.BUFFTYPE.BURNING, true) <= 0f && !isProgressStop())
		{
			addHp += healHpSpeed * buffParam.GetHealSpeedUp() * _GetGuardingHealSpeedUp() * Time.get_deltaTime();
			int num = (int)addHp;
			addHp -= (float)num;
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
			bool flag = base.actionID == ACTION_ID.IDLE || base.actionID == ACTION_ID.MOVE || base.actionID == ACTION_ID.ATTACK || base.actionID == (ACTION_ID)18 || base.actionID == (ACTION_ID)19 || base.actionID == (ACTION_ID)33 || base.actionID == (ACTION_ID)20 || base.actionID == (ACTION_ID)25 || base.actionID == (ACTION_ID)32 || base.actionID == (ACTION_ID)21 || base.actionID == ACTION_ID.MAX || base.actionID == (ACTION_ID)36;
			prayerEndIds.Clear();
			int j = 0;
			for (int count2 = prayTargetIds.Count; j < count2; j++)
			{
				if (!flag)
				{
					prayerEndIds.Add(prayTargetIds[j]);
				}
				else
				{
					Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(prayTargetIds[j]) as Player;
					if (player == null)
					{
						prayerEndIds.Add(prayTargetIds[j]);
					}
					else if (!player.isDead || player.rescueTime <= 0f)
					{
						prayerEndIds.Add(prayTargetIds[j]);
					}
					else if (Vector3.Distance(player.get_transform().get_position(), this.get_transform().get_position()) > playerParameter.revivalRange)
					{
						prayerEndIds.Add(prayTargetIds[j]);
					}
				}
			}
			int k = 0;
			for (int count3 = prayerEndIds.Count; k < count3; k++)
			{
				OnPrayerEnd(prayerEndIds[k]);
			}
			if (flag)
			{
				int l = 0;
				for (int count4 = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count; l < count4; l++)
				{
					Player player2 = MonoBehaviourSingleton<StageObjectManager>.I.playerList[l] as Player;
					if (!(player2 == this) && player2.isDead && player2.actionID == (ACTION_ID)23 && !(player2.rescueTime <= 0f) && !(Vector3.Distance(player2.get_transform().get_position(), this.get_transform().get_position()) > playerParameter.revivalRange))
					{
						bool flag2 = false;
						int m = 0;
						for (int count5 = prayTargetIds.Count; m < count5; m++)
						{
							if (prayTargetIds[m] == player2.id)
							{
								flag2 = true;
								break;
							}
						}
						if (flag2)
						{
							CheckPrayerBoost(player2);
						}
						else
						{
							OnPrayerStart(player2.id);
						}
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

	protected virtual void OnDestroy()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		if (base._collider != null)
		{
			Object.Destroy(base._collider.get_material());
		}
		if (_physics != null)
		{
			Object.Destroy(_physics.get_gameObject());
		}
		_physics = null;
		if (fishingCtrl != null)
		{
			fishingCtrl.Finalize();
		}
		fishingCtrl = null;
		if (activeBulletBarrierObject != null)
		{
			activeBulletBarrierObject.DestroyObject();
		}
		activeBulletBarrierObject = null;
		ClearStoreEffect();
	}

	protected override void FixedUpdate()
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		if (base.actionID == (ACTION_ID)28)
		{
			ActGrabbedUpdate();
		}
		else if (base.actionID == (ACTION_ID)29)
		{
			UpdateRestraint();
		}
		else
		{
			if (!pairSwordsBoostModeAuraEffectList.IsNullOrEmpty())
			{
				for (int i = 0; i < pairSwordsBoostModeAuraEffectList.Count; i++)
				{
					if (!(pairSwordsBoostModeAuraEffectList[i] == null))
					{
						pairSwordsBoostModeAuraEffectList[i].set_position(_position);
					}
				}
			}
			if (enableRotateToTargetPoint)
			{
				Vector3 val = _forward;
				if (GetTargetPos(out Vector3 pos))
				{
					pos.y = 0f;
					val = pos - _position;
				}
				else if (base.targetPointPos != Vector3.get_zero())
				{
					pos = base.targetPointPos;
					pos.y = 0f;
					val = pos - _position;
				}
				else if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.boss != null)
				{
					pos = GetTargetPosition(MonoBehaviourSingleton<StageObjectManager>.I.boss);
					pos.y = 0f;
					val = pos - _position;
				}
				if (CheckAttackModeAndSpType(ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL) && targetPointWithSpWeak != null)
				{
					pos = targetPointWithSpWeak.param.markerPos;
					pos.y = 0f;
					val = pos - _position;
				}
				Quaternion val2 = Quaternion.LookRotation(val);
				Vector3 eulerAngles = val2.get_eulerAngles();
				rotateEventDirection = eulerAngles.y;
				if (!periodicSyncActionPositionFlag)
				{
					enableRotateToTargetPoint = false;
				}
				Vector3 forward = _forward;
				forward.y = 0f;
				forward.Normalize();
				Vector3 val3 = Quaternion.AngleAxis(rotateEventDirection, Vector3.get_up()) * Vector3.get_forward();
				Vector3 val4 = Vector3.Cross(forward, val3);
				int num = (val4.y >= 0f) ? 1 : (-1);
				float num2 = Vector3.Angle(forward, val3);
				Quaternion rotation = _rotation;
				Vector3 eulerAngles2 = rotation.get_eulerAngles();
				_rotation = Quaternion.Euler(eulerAngles2.x, eulerAngles2.y + (float)num * num2, eulerAngles2.z);
			}
			base.FixedUpdate();
			FixedUpdateOneHandSword();
			CapsuleCollider val5 = base._collider as CapsuleCollider;
			if (val5 != null)
			{
				CapsuleCollider obj = val5;
				float num3 = val5.get_height() * 0.5f;
				Vector3 position = _position;
				obj.set_center(new Vector3(0f, num3 - position.y, 0f));
			}
		}
	}

	protected override void FixedUpdatePhysics()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		if (base.isInitialized)
		{
			Vector3 position = _position;
			float height = StageManager.GetHeight(position);
			switch (base.actionID)
			{
			case (ACTION_ID)13:
				if (Time.get_time() - stumbleEndTime > 0f)
				{
					SetNextTrigger(0);
				}
				break;
			case (ACTION_ID)14:
				if (Time.get_time() - shakeEndTime > 0f)
				{
					SetNextTrigger(0);
				}
				break;
			case (ACTION_ID)15:
			case (ACTION_ID)16:
			case (ACTION_ID)17:
				if (!base.waitAddForce && (base._rigidbody.get_constraints() & 4) == 0 && position.y <= height + 0.03f)
				{
					Vector3 velocity = base._rigidbody.get_velocity();
					if (velocity.y <= 0f)
					{
						ResetIgnoreColliders();
						SetNextTrigger(0);
					}
				}
				if (base.actionID == (ACTION_ID)17 && isStunnedLoop && stunnedEndTime - Time.get_time() <= 0f)
				{
					SetStunnedEnd();
				}
				break;
			}
			base.FixedUpdatePhysics();
		}
	}

	protected override float GetAnimatorSpeed()
	{
		float animatorSpeed = base.GetAnimatorSpeed();
		animatorSpeed += GetBoostAttackSpeedUp();
		animatorSpeed += GetAttackModeWalkSpeedUp();
		if (animatorSpeed <= 0f || !enableAnimSeedRate)
		{
			return animatorSpeed;
		}
		if (isLoopingRush)
		{
			float num = animatorSpeed * GetRushDistanceRate();
			return Mathf.Max(0f, num);
		}
		if (IsBurstTwoHandSword() && thsCtrl != null && thsCtrl.IsEnableChangeReloadMotionSpeed)
		{
			return GetBurstReloadMotionSpeedRate();
		}
		float num2 = 0f;
		switch (attackMode)
		{
		case ATTACK_MODE.TWO_HAND_SWORD:
			num2 = buffParam.GetChargeSwordsTimeRate();
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
			if (actSpecialActionTimer > 0f && isSpearAttackMode)
			{
				lockedSpearCancelAction = true;
				return false;
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
		case (ACTION_ID)35:
			if (enableCancelToAvoid)
			{
				return true;
			}
			break;
		case (ACTION_ID)21:
		case (ACTION_ID)36:
			if (enableCancelToSkill)
			{
				return true;
			}
			break;
		case (ACTION_ID)32:
			if (enableCancelToSpecialAction)
			{
				return true;
			}
			break;
		case (ACTION_ID)37:
			if (enableCancelToEvolveSpecialAction)
			{
				return true;
			}
			break;
		case (ACTION_ID)28:
			if (base.actionID == (ACTION_ID)30 || base.actionID == (ACTION_ID)31)
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
			}
			else
			{
				result = buffParam.GetChargeHeatArrowTimeRate();
				if (MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.ignoreSpAttackTypeAbility)
				{
					result += buffParam.GetChargeArrowTimeRate();
				}
			}
			break;
		case SP_ATTACK_TYPE.NONE:
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
		return attackMode == mode && spAttackType == type;
	}

	public ELEMENT_TYPE GetNowWeaponElement()
	{
		if (object.ReferenceEquals(weaponState, null))
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
			if (type == SP_ATTACK_TYPE.SOUL)
			{
				_attackId = 10;
			}
			if (type == SP_ATTACK_TYPE.BURST)
			{
				_attackId = playerParameter.ohsActionInfo.burstOHSInfo.BaseAtkId;
				_motionLayerName = GetMotionLayerName(attackMode, type, _attackId);
			}
			break;
		case ATTACK_MODE.TWO_HAND_SWORD:
			if (thsCtrl != null)
			{
				thsCtrl.GetNormalAttackId(type, exType, ref _attackId, ref _motionLayerName);
			}
			break;
		case ATTACK_MODE.SPEAR:
			if (type == SP_ATTACK_TYPE.SOUL)
			{
				_attackId = playerParameter.spearActionInfo.Soul_AttackId;
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
			}
			break;
		}
		return _attackId;
	}

	public void ReleaseEffect(ref Transform t, bool isPlayEndAnimation = true)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		if (MonoBehaviourSingleton<EffectManager>.IsValid() && !object.ReferenceEquals(t, null))
		{
			EffectManager.ReleaseEffect(t.get_gameObject(), isPlayEndAnimation, false);
			t = null;
		}
	}

	private bool IsChangeableActionOnSpAttack()
	{
		bool flag = false;
		switch (attackMode)
		{
		case ATTACK_MODE.ONE_HAND_SWORD:
			if (spAttackType == SP_ATTACK_TYPE.BURST && enableSpAttackContinue)
			{
				flag = true;
			}
			break;
		case ATTACK_MODE.TWO_HAND_SWORD:
			if ((spAttackType == SP_ATTACK_TYPE.NONE || spAttackType == SP_ATTACK_TYPE.HEAT || spAttackType == SP_ATTACK_TYPE.BURST) && enableSpAttackContinue)
			{
				flag = true;
			}
			if (!flag && spAttackType == SP_ATTACK_TYPE.HEAT && isActSpecialAction && !isLockedSpAttackContinue)
			{
				isLockedSpAttackContinue = true;
				if (IsFullCharge() && isHitSpAttack && (thsCtrl == null || !thsCtrl.IsTwoHandSwordSpAttackContinueTimeOut(hitSpAttackContinueTimer)))
				{
					flag = true;
				}
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
			if (enableSpAttackContinue && (spAttackType == SP_ATTACK_TYPE.NONE || spAttackType == SP_ATTACK_TYPE.HEAT))
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
		ATTACK_MODE attackMode = this.attackMode;
		if (attackMode == ATTACK_MODE.PAIR_SWORDS && spAttackType == SP_ATTACK_TYPE.SOUL && enableAttackNext)
		{
			result = true;
		}
		return result;
	}

	public bool PlayMotionImmidate(string ctrlName, string stateName, float _transition_time = -1f)
	{
		return _PlayMotion("Base Layer." + stateName, ctrlName, _transition_time);
	}

	public override void ActIdle(bool is_sync = false, float transitionTimer = -1f)
	{
		if (base.actionID == (ACTION_ID)28)
		{
			ActGrabbedEnd(0f, 0f);
		}
		else if (base.actionID == (ACTION_ID)29)
		{
			ActRestraintEnd();
		}
		else
		{
			base.ActIdle(is_sync, transitionTimer);
			if (loader != null)
			{
				loader.eyeBlink = true;
			}
		}
	}

	public virtual void ActGuardWalk(Vector3 velocity_, float sync_speed, Vector3 move_vec)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		if (base.actionID == (ACTION_ID)18)
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
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val;
		if (base.actionTarget != null && !IsValidBuffBlind())
		{
			val = base.actionTarget._position - _position;
			val.y = 0f;
			val.Normalize();
			SetLerpRotation(val);
		}
		else
		{
			val = move_vec;
			SetLerpRotation(move_vec);
		}
		float num = Vector3.Angle(val, move_vec);
		float num2 = 0f;
		Vector3 val2 = Vector3.Cross(val, move_vec);
		num2 = ((!(val2.y >= 0f)) ? (num / 360f) : ((360f - num) / 360f));
		base.animator.SetFloat(guardAngleID, num2);
	}

	public override void ActMoveSyncVelocity(float time, Vector3 pos, int motion_id)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
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
			moveSyncDirection = -3.40282347E+38f;
			SetGuardWalkRotation(GetVelocity());
		}
		if (flag)
		{
			PlayerLoader.SetLayerWithChildren_SecondaryNoChange(base._transform, 20);
		}
	}

	public override void SetMoveSyncVelocityEnd(float time, Vector3 pos, float direction, float sync_speed, int motion_id)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		base.SetMoveSyncVelocityEnd(time, pos, direction, sync_speed, motion_id);
		if (motion_id == 122)
		{
			moveSyncDirection = -3.40282347E+38f;
			SetGuardWalkRotation(GetVelocity());
		}
	}

	public void ActAttackFailure(int id, bool isSendPacket)
	{
		base.ActAttack(id, isSendPacket, false, string.Empty);
	}

	public override void ActAttack(int id, bool send_packet = true, bool sync_immediately = false, string _motionLayerName = "")
	{
		base.ActAttack(id, send_packet, sync_immediately, _motionLayerName);
		if (isArrowAimBossMode)
		{
			SetArrowAimBossVisible(true);
		}
		if (isArrowAimLesserMode)
		{
			SetArrowAimLesserVisible(true);
		}
		UpdateArrowAngle();
	}

	public override void SetAttackActionPosition()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		if (targetingPoint != null)
		{
			SetActionPosition(targetingPoint.param.targetPos, true);
		}
		else if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.boss != null)
		{
			SetActionPosition(GetTargetPosition(MonoBehaviourSingleton<StageObjectManager>.I.boss), true);
		}
		else
		{
			SetActionPosition(Vector3.get_zero(), false);
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
			string motionLayerName = _motionLayerName;
			ActAttack(_attackId, false, false, motionLayerName);
			if (playerSender != null)
			{
				playerSender.OnActAttackCombo(_attackId, _motionLayerName);
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
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		if ((IsCoopNone() || IsOriginal()) && snatchCtrl.IsMove())
		{
			Vector3 snatchPos = snatchCtrl.GetSnatchPos();
			if (IsArrivalPosition(snatchPos, 0f))
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
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		EventMoveEnd();
		EndRotate();
		enableEventMove = true;
		base.enableAddForce = false;
		eventMoveVelocity = Vector3.get_forward() * playerParameter.ohsActionInfo.Soul_SnatchMoveVelocity;
		Vector3 val = snatchPos - _position;
		Vector3 normalized = val.get_normalized();
		SetVelocity(Quaternion.LookRotation(normalized) * eventMoveVelocity, VELOCITY_TYPE.EVENT_MOVE);
		snatchCtrl.StartMoveLoop();
		if (snatchCtrl.IsShotReleased())
		{
			SetNextTrigger(3);
		}
		else
		{
			SetNextTrigger(0);
		}
		StartWaitingPacket(WAITING_PACKET.PLAYER_ONE_HAND_SWORD_MOVE_END, true, 0f);
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
			inputCannonChargeCounter += Time.get_deltaTime();
			if (inputCannonChargeMax > 0f && !(inputCannonChargeCounter >= inputCannonChargeMax))
			{
				goto IL_0075;
			}
		}
		else
		{
			inputCannonChargeCounter -= Time.get_deltaTime();
			fieldGimmickCannonSpecial.ReleaseCharge();
		}
		goto IL_0075;
		IL_0075:
		inputCannonChargeCounter = Mathf.Clamp(inputCannonChargeCounter, 0f, inputCannonChargeMax);
	}

	public virtual void SetChargeRelease(float charge_rate)
	{
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		if (CheckAttackMode(ATTACK_MODE.TWO_HAND_SWORD) && charge_rate >= 1f)
		{
			IncrementCleaveComboCount();
		}
		if (CheckAttackModeAndSpType(ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.NONE) && charge_rate < 1f)
		{
			inputChargeAutoRelease = true;
			inputChargeMaxTiming = false;
		}
		else
		{
			chargeRate = charge_rate;
			base.enableMotionCancel = false;
			enableCancelToAvoid = false;
			enableCancelToMove = false;
			enableCancelToAttack = false;
			enableCancelToSkill = false;
			enableCancelToSpecialAction = false;
			enableCancelToEvolveSpecialAction = false;
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
						SetNextTrigger(0);
					}
					else
					{
						ActIdle(false, -1f);
					}
					ReleaseEffect(ref exRushChargeEffect, true);
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
							ActIdle(false, -1f);
						}
					}
					break;
				case SP_ATTACK_TYPE.SOUL:
					spearCtrl.StoreChargeRate(chargeRate);
					SetNextTrigger(0);
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
			else if (CheckAttackModeAndSpType(ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.NONE))
			{
				if (chargeRate >= 1f)
				{
					SetNextTrigger(0);
				}
			}
			else if (CheckAttackModeAndSpType(ATTACK_MODE.ARROW, SP_ATTACK_TYPE.SOUL))
			{
				SetNextTrigger((MonoBehaviourSingleton<TargetMarkerManager>.I.GetMultiLockNum() <= 0) ? 1 : 0);
			}
			else
			{
				SetNextTrigger(0);
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
				UpdateArrowAimLesserMode(Vector2.get_zero());
			}
			UpdateArrowAngle();
			if (playerSender != null)
			{
				playerSender.OnSetChargeRelease(charge_rate, isChargeExRush);
			}
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
		SetNextTrigger(0);
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
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		inputChargeMaxTiming = false;
		if (!isNpc && (IsCoopNone() || IsOriginal()))
		{
			switch (attackMode)
			{
			case ATTACK_MODE.TWO_HAND_SWORD:
				if (spAttackType == SP_ATTACK_TYPE.SOUL)
				{
					if (object.ReferenceEquals(twoHandSwordsChargeMaxEffect, null))
					{
						twoHandSwordsChargeMaxEffect = EffectManager.GetEffect(MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.soulIaiChargeMaxEffect, FindNode("R_Wep"));
					}
					SoundManager.PlayOneShotSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.soulIaiChargeMaxSeId, this, FindNode(string.Empty));
				}
				break;
			case ATTACK_MODE.SPEAR:
				if (spAttackType == SP_ATTACK_TYPE.NONE)
				{
					if (isChargeExRush)
					{
						ReleaseEffect(ref exRushChargeEffect, true);
						EffectManager.OneShot("ef_btl_wsk_charge_end_01", FindNode("R_Wep").get_transform().get_position(), Quaternion.get_identity(), false);
						exRushChargeEffect = EffectManager.GetEffect("ef_btl_wsk_charge_loop_02", FindNode("R_Wep"));
						SoundManager.PlayOneShotSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.exRushChargeMaxSeId, this, FindNode(string.Empty));
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
						SoundManager.PlayOneShotSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.jumpChargeMaxSeId, this, FindNode(string.Empty));
					}
				}
				else if (spAttackType == SP_ATTACK_TYPE.SOUL)
				{
					Transform val = FindNode("R_Wep");
					EffectManager.OneShot("ef_btl_wsk_charge_end_01", val.get_position() + val.get_rotation() * MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.Soul_SpAttackMaxChargeEffectOffsetPos, Quaternion.get_identity(), false);
					SoundManager.PlayOneShotSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.exRushChargeMaxSeId, this, FindNode(string.Empty));
					spearCtrl.ExecBladeEffect();
				}
				break;
			case ATTACK_MODE.PAIR_SWORDS:
				if (spAttackType == SP_ATTACK_TYPE.NONE)
				{
					EffectManager.OneShot("ef_btl_wsk_charge_end_01", FindNode("R_Wep").get_transform().get_position(), Quaternion.get_identity(), false);
					EffectManager.OneShot("ef_btl_wsk_charge_end_01", FindNode("L_Wep").get_transform().get_position(), Quaternion.get_identity(), false);
					SoundManager.PlayOneShotSE(MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.wildDanceChargeMaxSeId, this, FindNode(string.Empty));
				}
				break;
			}
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
			if (_IsRevengeChargeFull())
			{
				_AttackRevengeBurst();
				isActOneHandSwordCounter = true;
			}
			else if (enableCounterAttack && spAttackType != SP_ATTACK_TYPE.BURST)
			{
				if (spAttackType == SP_ATTACK_TYPE.HEAT)
				{
					ActAttack(98, true, true, string.Empty);
					IncrementCounterAttackCount();
					isActOneHandSwordCounter = true;
				}
				else
				{
					ActAttack(playerParameter.specialActionInfo.spAttackID, true, true, string.Empty);
					IncrementCounterAttackCount();
					isActOneHandSwordCounter = true;
				}
			}
		}
	}

	private void IncrementCounterAttackCount()
	{
		if ((IsCoopNone() || IsOriginal()) && buffParam.IncrementCounterConditionAbility())
		{
			EffectManager.GetEffect("ef_btl_ab_charge_01", base._transform);
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
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		if (enableInputRotate)
		{
			if (!startInputRotate)
			{
				startInputRotate = true;
				rotateEventSpeed = 0f;
				rotateEventDirection = 0f;
				rotateEventKeep = false;
			}
			Vector3 right = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.get_right();
			Vector3 forward = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform.get_forward();
			right.y = 0f;
			right.Normalize();
			forward.y = 0f;
			forward.Normalize();
			Vector3 val = right * input_vec.x + forward * input_vec.y;
			_rotation = Quaternion.LookRotation(val);
		}
	}

	public void ActAvoid()
	{
		EndAction();
		base.actionID = ACTION_ID.MAX;
		if (CheckAttackModeAndSpType(ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL))
		{
			PlayMotion(136, -1f);
		}
		else
		{
			PlayMotion(115, -1f);
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
		base.actionID = (ACTION_ID)35;
		if (CheckAttackModeAndSpType(ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL))
		{
			PlayMotion(137, -1f);
		}
		else
		{
			PlayMotion(135, -1f);
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

	public virtual void ActRestraint(RestraintInfo restInfo)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		EndAction();
		base.actionID = (ACTION_ID)29;
		PlayMotion(130, -1f);
		base._rigidbody.set_isKinematic(true);
		base._rigidbody.set_useGravity(false);
		base._rigidbody.set_constraints(126);
		base._rigidbody.set_velocity(Vector3.get_zero());
		m_restrainTime = Time.get_time() + restInfo.duration;
		m_restraintDamgeTimer = restInfo.damageInterval;
		m_restraintInfo = restInfo;
		GameObject val = new GameObject("AttackRestraintObject");
		AttackRestraintObject attackRestraintObject = val.AddComponent<AttackRestraintObject>();
		attackRestraintObject.Initialize(this, restInfo);
		m_attackRestraint = attackRestraintObject;
		if (restInfo.damageRate > 0)
		{
			m_restrainDamageValue = (int)((float)base.hpMax * ((float)restInfo.damageRate * 0.01f));
		}
		ClearLaser();
		pairSwordsCtrl.OnReaction();
		if (playerSender != null)
		{
			playerSender.OnRestraintStart(restInfo);
		}
	}

	public void ActRestraintEnd()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		if (base.actionID == (ACTION_ID)29)
		{
			EndAction();
			if (base.actionID != (ACTION_ID)16)
			{
				ActFallBlow(Vector3.get_down() * 10f);
			}
		}
	}

	private void PostRestraint()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		if (!(m_attackRestraint == null))
		{
			base._transform.set_position(MonoBehaviourSingleton<StageManager>.I.ClampInside(base._transform.get_position()));
			if (m_restraintInfo.isStopMotion)
			{
				setPause(false);
			}
			m_restrainDamageValue = 0;
			m_restrainTime = 0f;
			m_restraintDamgeTimer = 0f;
			m_restraintInfo = null;
			base._rigidbody.set_isKinematic(false);
			base._rigidbody.set_useGravity(true);
			base._rigidbody.set_constraints(112);
			base._rigidbody.set_velocity(Vector3.get_zero());
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
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		UpdateNextMotion();
		if (!(m_attackRestraint == null))
		{
			if (m_restraintInfo.isStopMotion && !base.isPause)
			{
				AnimatorStateInfo currentAnimatorStateInfo = base.animator.GetCurrentAnimatorStateInfo(0);
				if (currentAnimatorStateInfo.get_fullPathHash() == Animator.StringToHash("Base Layer.restraint"))
				{
					setPause(true);
				}
			}
			int restrainDamageValue = m_restrainDamageValue;
			if ((IsCoopNone() || IsOriginal()) && restrainDamageValue > 0 && base.hp > 1)
			{
				m_restraintDamgeTimer -= Time.get_deltaTime();
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
			float num = Time.get_time() - m_restrainTime;
			if (num > 0f)
			{
				ActRestraintEnd();
			}
		}
	}

	public void ReduceRestraintTime()
	{
		RestraintInfo restraintInfo = m_restraintInfo;
		if (restraintInfo != null)
		{
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
	}

	public bool IsRestraint()
	{
		return base.actionID == (ACTION_ID)29;
	}

	public override void OnActReaction()
	{
		base.OnActReaction();
		snatchCtrl.Cancel();
		pairSwordsCtrl.OnReaction();
	}

	public virtual void ActStumble(float time = 0f)
	{
		EndAction();
		base.actionID = (ACTION_ID)13;
		PlayMotion(116, -1f);
		stumbleEndTime = Time.get_time() + time;
		OnActReaction();
		ClearLaser();
	}

	public virtual void ActShake()
	{
		EndAction();
		base.actionID = (ACTION_ID)14;
		PlayMotion(117, -1f);
		shakeEndTime = Time.get_time() + buffParam.GetShakeTime(playerParameter.shakeLoopTime);
		OnActReaction();
		ClearLaser();
	}

	public virtual void ActBlow(Vector3 force)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		EndAction();
		base.actionID = (ACTION_ID)15;
		PlayMotion(118, -1f);
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
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		EndAction();
		base.actionID = (ACTION_ID)16;
		PlayMotion(119, -1f);
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
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		EndAction();
		base.actionID = (ACTION_ID)17;
		PlayMotion(120, -1f);
		if (base._rigidbody != null)
		{
			base.addForce = force;
		}
		stunnedTime = buffParam.GetStumbleTime(time);
		IgnoreEnemyColliders();
		OnActReaction();
		ClearLaser();
	}

	public virtual void ActGrabbedStart(int enemyId, GrabInfo grabInfo)
	{
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
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
			base.actionID = (ACTION_ID)28;
			PlayMotion(129, -1f);
			IgnoreEnemyColliders();
			Transform val = Utility.Find(enemy._transform, grabInfo.parentNode);
			if (val != null)
			{
				base._transform.set_parent(val);
				base._transform.set_localPosition(Vector3.get_zero());
				base._transform.set_localRotation(Quaternion.get_identity());
				base._rigidbody.set_isKinematic(true);
				base._rigidbody.set_useGravity(false);
				base._rigidbody.set_constraints(126);
			}
			CircleShadow componentInChildren = this.GetComponentInChildren<CircleShadow>();
			if (componentInChildren != null)
			{
				componentInChildren.get_gameObject().SetActive(false);
			}
			hitOffFlag |= HIT_OFF_FLAG.GRAB;
			ClearLaser();
			pairSwordsCtrl.OnReaction();
			if (playerSender != null)
			{
				playerSender.OnGrabbedStart(enemy.id, grabInfo.parentNode, grabInfo.duration, grabInfo.drainAttackId);
			}
		}
	}

	public virtual void ActGrabbedEnd(float angle = 0f, float power = 0f)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		EndAction();
		grabDrainAtkInfo = null;
		base._transform.set_position(MonoBehaviourSingleton<StageManager>.I.ClampInside(base._transform.get_position()));
		CircleShadow componentInChildren = this.GetComponentInChildren<CircleShadow>(true);
		if (componentInChildren != null)
		{
			componentInChildren.get_gameObject().SetActive(true);
			componentInChildren.get_transform().set_localPosition(Vector3.get_zero());
		}
		base._transform.set_parent(MonoBehaviourSingleton<StageObjectManager>.I._transform);
		base._transform.set_localRotation(Quaternion.get_identity());
		base._rigidbody.set_isKinematic(false);
		base._rigidbody.set_useGravity(true);
		base._rigidbody.set_constraints(112);
		base._rigidbody.set_velocity(Vector3.get_zero());
		Vector3 val = -_forward;
		val = Quaternion.AngleAxis(angle, _right) * val * power;
		ActFallBlow(val);
		hitOffFlag &= ~HIT_OFF_FLAG.GRAB;
		if (playerSender != null)
		{
			playerSender.OnGrabbedEnd();
		}
	}

	public virtual void ActGrabbedUpdate()
	{
		UpdateNextMotion();
		if (grabDrainAtkInfo != null)
		{
			int num = (int)((float)base.hpMax * (grabDrainAtkInfo.damageRate * 0.01f));
			if ((IsCoopNone() || IsOriginal()) && num > 0 && base.hp > 1)
			{
				grabDrainDamageTimer -= Time.get_deltaTime();
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
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Expected O, but got Unknown
		bool flag = false;
		float num = 0f;
		if (isStunnedLoop && stunnedTime > 0f)
		{
			float num2 = stunnedEndTime - Time.get_time();
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
		string[] stunnedEffectList = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.stunnedEffectList;
		if (flag && stunnedEffectList.Length > 0)
		{
			int num3 = 0;
			num3 = (int)(num * (float)stunnedEffectList.Length);
			if (num3 != stunnedEffectIndex)
			{
				stunnedEffectIndex = -1;
				if (stunnedEffect != null)
				{
					EffectManager.ReleaseEffect(stunnedEffect, true, false);
					stunnedEffect = null;
				}
				if (!string.IsNullOrEmpty(stunnedEffectList[num3]))
				{
					stunnedEffectIndex = num3;
					Transform effect = EffectManager.GetEffect(stunnedEffectList[num3], base.rootNode);
					if (effect != null)
					{
						stunnedEffect = effect.get_gameObject();
					}
				}
			}
		}
		else
		{
			stunnedEffectIndex = -1;
			if (stunnedEffect != null)
			{
				EffectManager.ReleaseEffect(stunnedEffect, true, false);
				stunnedEffect = null;
			}
		}
	}

	public virtual void ActGuard()
	{
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		if (base.actionID == (ACTION_ID)19 || base.actionID == (ACTION_ID)33 || base.actionID == (ACTION_ID)20 || isGuardWalk)
		{
			notEndGuardFlag = true;
		}
		EndAction();
		base.actionID = (ACTION_ID)18;
		PlayMotion(121, -1f);
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
				SetActionPosition(base.actionTarget._position, true);
			}
			else
			{
				SetActionPosition(Vector3.get_zero(), false);
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
				PlayMotionParam playMotionParam = new PlayMotionParam();
				playMotionParam.MotionID = 134;
				playMotionParam.MotionLayerName = motionLayerName;
				playMotionParam.TransitionTime = -1f;
				PlayMotionParam param = playMotionParam;
				base.actionID = (ACTION_ID)20;
				PlayMotion(param);
			}
			else
			{
				base.actionID = (ACTION_ID)33;
				PlayMotion(133, -1f);
			}
		}
		else
		{
			base.actionID = (ACTION_ID)19;
			PlayMotion(123, -1f);
		}
		OnActReaction();
	}

	public virtual bool ActBattleStart(bool effect_only = false)
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Expected O, but got Unknown
		if (isActedBattleStart)
		{
			return false;
		}
		if (base.packetReceiver != null)
		{
			base.packetReceiver.SetStopPacketUpdate(false);
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
					AddObjectList(effect.get_gameObject(), OBJECT_LIST_TYPE.STATIC);
				}
			}
		}
		else
		{
			EndAction();
			base.actionID = (ACTION_ID)22;
			PlayMotion(124, -1f);
			if (uiPlayerStatusGizmo != null)
			{
				uiPlayerStatusGizmo.SetVisible(false);
			}
			hitOffFlag |= HIT_OFF_FLAG.INVICIBLE;
			SetEnableNodeRenderer(null, false);
		}
		return true;
	}

	public virtual void WaitBattleStart()
	{
		isWaitBattleStart = true;
		if (base.packetReceiver != null)
		{
			base.packetReceiver.SetStopPacketUpdate(true);
		}
	}

	public override void ActDead(bool force_sync = false, bool recieve_direct = false)
	{
		ActGrabbedEnd(0f, 0f);
		ActRestraintEnd();
		FinishBoostMode();
		EndEvolve();
		_EndGuard();
		_EndBuffShadowSealing();
		base.badStatusTotal.Reset();
		CheckAutoRevive();
		base.ActDead(force_sync, recieve_direct);
		if (MonoBehaviourSingleton<UIDeadAnnounce>.IsValid())
		{
			MonoBehaviourSingleton<UIDeadAnnounce>.I.Announce(UIDeadAnnounce.ANNOUNCE_TYPE.DEAD, this);
		}
		skillInfo.ResetUseGauge();
		ResetSpActionGauge();
		evolveCtrl.ResetGauge(true);
		snatchCtrl.Cancel();
		int i = 0;
		for (int count = m_weaponCtrlList.Count; i < count; i++)
		{
			m_weaponCtrlList[i].OnActDead();
		}
		ClearLaser();
	}

	public virtual void ActDeadLoop(bool is_init_rescue = false, float set_rescue_time = 0f, float set_continue_time = 0f)
	{
		EndAction();
		base.actionID = (ACTION_ID)23;
		PlayMotion(125, -1f);
		base.isDead = true;
		prayerIds.Clear();
		boostPrayTargetInfoList.Clear();
		boostPrayedInfoList.Clear();
		hitOffFlag |= HIT_OFF_FLAG.DEAD;
		IgnoreEnemyColliders();
		if (!OnAutoRevive())
		{
			float rescue_time = 0f;
			float continueTime = 0f;
			if (is_init_rescue)
			{
				rescue_time = set_rescue_time;
				continueTime = set_continue_time;
			}
			else if (playerParameter.rescueTimes.Length > 0)
			{
				int num = rescueCount;
				if (playerParameter.rescueTimes.Length <= num)
				{
					num = playerParameter.rescueTimes.Length - 1;
				}
				rescue_time = playerParameter.rescueTimes[num];
				continueTime = playerParameter.continueTime;
			}
			if (IsCoopNone() || IsOriginal())
			{
				DeadCount(rescue_time, isStopCounter, false);
			}
			this.continueTime = continueTime;
			UpdateRevivalRangeEffect();
		}
	}

	private void DeactivateRescueTimer()
	{
		StopCounter(true);
	}

	private void ActivateRescueTimer()
	{
		StopCounter(false);
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
			DeadCount(rescueTime, stop || IsPrayed(), false);
		}
	}

	public void DeadCount(float rescue_time, bool stop, bool syncRequested = false)
	{
		if (!MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest())
		{
			_rescueTime = rescue_time;
			if (!stop)
			{
				deadStartTime = Time.get_time();
				deadStopTime = -1f;
			}
			else
			{
				deadStartTime = Time.get_time();
				deadStopTime = Time.get_time();
			}
			UpdateRevivalRangeEffect();
			if (playerSender != null && !syncRequested)
			{
				playerSender.OnDeadCount(_rescueTime, stop);
			}
		}
	}

	public bool IsRescuable()
	{
		return rescueTime > 0f && !isStopCounter && deadStartTime >= 0f;
	}

	public void UpdateRevivalRangeEffect()
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		if (base.actionID == (ACTION_ID)23 && (!MonoBehaviourSingleton<InGameManager>.IsValid() || !MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo()) && !QuestManager.IsValidInGameTrial() && !MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest())
		{
			if (IsRescuable())
			{
				if (revivalRangEffect == null)
				{
					Transform effect = EffectManager.GetEffect("ef_btl_rebirth_area_01", base._transform);
					if (effect != null)
					{
						Vector3 localScale = effect.get_localScale();
						effect.set_localScale(localScale * playerParameter.revivalRange);
						revivalRangEffect = effect.get_gameObject();
					}
				}
			}
			else if (revivalRangEffect != null)
			{
				EffectManager.ReleaseEffect(revivalRangEffect, true, false);
				revivalRangEffect = null;
			}
		}
	}

	public void SyncDeadCount()
	{
		if (base.isDead && !MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest() && !IsCoopNone() && IsOriginal())
		{
			DeadCount(rescueTime, IsPrayed() || isStopCounter, false);
		}
	}

	public virtual void OnEndContinueTimeEnd()
	{
		if ((MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameWaveMatch(true)) && MonoBehaviourSingleton<StageObjectManager>.I.playerList.TrueForAll(delegate(StageObject so)
		{
			Player player = so as Player;
			if (player != null)
			{
				return player.isDead && !player.IsAutoReviving();
			}
			return true;
		}))
		{
			MonoBehaviourSingleton<InGameProgress>.I.BattleRetire();
		}
	}

	public virtual void ActDeadStandup(int standup_hp, eContinueType cType)
	{
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		EndAction();
		base.actionID = (ACTION_ID)24;
		PlayMotion(126, -1f);
		hitOffFlag |= HIT_OFF_FLAG.INVICIBLE;
		base.isDead = false;
		base.hp = standup_hp;
		healHp = standup_hp;
		autoReviveHp = 0;
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
			Transform effect = EffectManager.GetEffect("ef_btl_rebirth_01", null);
			if (effect != null)
			{
				effect.set_position(_position);
				effect.set_rotation(_rotation);
				effect.set_localScale(Vector3.Scale(base._transform.get_lossyScale(), effect.get_localScale()));
			}
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
		paralyzeTime = Time.get_time() + buffParam.GetParalyzeTime();
	}

	public void ActPrayer()
	{
		EndAction();
		base.actionID = (ACTION_ID)25;
		PlayMotion(127, -1f);
		base.isControllable = true;
	}

	public void OnPrayerStart(int prayTargetId)
	{
		if (!MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest())
		{
			if (prayTargetIds.Count <= 0)
			{
				StartWaitingPacket(WAITING_PACKET.PLAYER_PRAYER_END, true, 0f);
			}
			prayTargetIds.Add(prayTargetId);
			if (playerSender != null)
			{
				playerSender.OnPrayerStart(prayTargetId);
			}
			Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(prayTargetId) as Player;
			if (!(player == null))
			{
				BoostPrayInfo boostPrayInfo = boostPrayTargetInfoList.Find((BoostPrayInfo item) => item.prayTargetId == prayTargetId);
				if (boostPrayInfo == null)
				{
					boostPrayInfo = new BoostPrayInfo();
					boostPrayInfo.prayerId = id;
					boostPrayInfo.prayTargetId = prayTargetId;
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
		}
	}

	public virtual void OnPrayerEnd(int prayTargetId)
	{
		if (playerSender != null)
		{
			playerSender.OnPrayerEnd(prayTargetId);
		}
		prayTargetIds.Remove(prayTargetId);
		if (prayTargetIds.Count <= 0)
		{
			EndWaitingPacket(WAITING_PACKET.PLAYER_PRAYER_END);
		}
		Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(prayTargetId) as Player;
		if (!(player == null))
		{
			BoostPrayInfo boostPrayInfo = boostPrayTargetInfoList.Find((BoostPrayInfo item) => item.prayTargetId == prayTargetId);
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
		if (!(player == null))
		{
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
			DeadCount(rescueTime, IsPrayed() || isStopCounter, false);
		}
	}

	public void EndPrayed(int prayerId)
	{
		prayerIds.Remove(prayerId);
		EndBoostPrayed(prayerId);
		if (IsCoopNone() || IsOriginal())
		{
			DeadCount(rescueTime, IsPrayed() || isStopCounter, false);
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

	public void ActChangeWeapon(CharaInfo.EquipItem item, int weapon_index)
	{
		EndAction();
		base.actionID = (ACTION_ID)26;
		PlayMotion(128, -1f);
		changeWeaponItem = item;
		changeWeaponIndex = weapon_index;
		if (playerSender != null)
		{
			playerSender.OnChangeWeapon();
		}
	}

	public virtual void ApplyChangeWeapon(CharaInfo.EquipItem item, int weapon_index)
	{
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Expected O, but got Unknown
		if (!base.isDead && base.actionID != (ACTION_ID)26)
		{
			ActChangeWeapon(item, weapon_index);
		}
		EndWaitingPacket(WAITING_PACKET.PLAYER_APPLY_CHANGE_WEAPON);
		isChangingWeapon = true;
		changeWeaponStartTime = Time.get_time();
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
				AddObjectList(effect.get_gameObject(), OBJECT_LIST_TYPE.CHANGE_WEAPON);
			}
		}
		OnBuffEnd(BuffParam.BUFFTYPE.LUNATIC_TEAR, true, true);
		FinishBoostMode();
		EndEvolve();
		_EndGuard();
		_EndBuffShadowSealing();
		DeleteWeaponLinkEffectAll();
		DetachRootEffectTemporary();
		LoadWeapon(item, weapon_index, delegate
		{
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			SetEnableNodeRenderer("BODY", false);
			if (loader.shadow != null)
			{
				loader.shadow.get_gameObject().SetActive(false);
			}
			pairSwordsCtrl.OnLoadComplete();
			ReAttachRootEffect();
		});
		if (playerSender != null)
		{
			playerSender.OnApplyChangeWeapon(item, weapon_index);
		}
	}

	public void ActGather(GatherPointObject gather_point)
	{
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		if (!(gather_point == null))
		{
			EndAction();
			targetGatherPoint = gather_point;
			isGatherInterruption = false;
			base.actionID = (ACTION_ID)27;
			PlayMotion(targetGatherPoint.viewData.actStateName, -1f);
			if (!string.IsNullOrEmpty(targetGatherPoint.viewData.toolModelName))
			{
				LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.gatherPointToolTable.Get(targetGatherPoint.viewData.toolModelName);
				if (loadObject != null)
				{
					actionRendererModel = (loadObject.loadedObject as GameObject);
					actionRendererNodeName = targetGatherPoint.viewData.toolNodeName;
				}
			}
			Vector3 position = targetGatherPoint._transform.get_position();
			position.y = 0f;
			if (IsCoopNone() || IsOriginal())
			{
				SetActionPosition(position, true);
			}
			Vector3 lerpRotation = position - base._transform.get_position();
			lerpRotation.y = 0f;
			SetLerpRotation(lerpRotation);
			if (playerSender != null)
			{
				playerSender.OnActGather(gather_point);
			}
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
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
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
	}

	public void ActReadStory(int id)
	{
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
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
	}

	public void ActGatherGimmick(int id)
	{
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			IFieldGimmickObject fieldGimmickObj = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.GatherGimmick, id);
			if (fieldGimmickObj != null)
			{
				FieldGatherGimmickObject fieldGatherGimmickObject = fieldGimmickObj as FieldGatherGimmickObject;
				if (!(fieldGatherGimmickObject == null))
				{
					GATHER_GIMMICK_TYPE type = fieldGatherGimmickObject.GetType();
					if (type == GATHER_GIMMICK_TYPE.FISHING && fishingCtrl.CanFishing())
					{
						EndAction();
						float add_margin_time = 10f;
						type = fieldGatherGimmickObject.GetType();
						if (type == GATHER_GIMMICK_TYPE.FISHING)
						{
							base.actionID = (ACTION_ID)39;
							PlayMotion(138, -1f);
							fishingCtrl.Start(fieldGatherGimmickObject.lotId, fieldGatherGimmickObject.GetTransform());
							LoadObject loadObject = MonoBehaviourSingleton<InGameProgress>.I.gatherPointToolTable.Get("Fishingrod");
							if (loadObject != null)
							{
								actionRendererModel = (loadObject.loadedObject as GameObject);
								actionRendererNodeName = "R_Wep";
							}
							add_margin_time = fishingCtrl.GetMaxWaitPacketSec();
						}
						Vector3 position = fieldGatherGimmickObject.GetTransform().get_position();
						position.y = 0f;
						if (IsCoopNone() || IsOriginal())
						{
							SetActionPosition(position, true);
						}
						Vector3 lerpRotation = position - base._transform.get_position();
						lerpRotation.y = 0f;
						SetLerpRotation(lerpRotation);
						fieldGatherGimmickObject.StartAction(this, IsCoopNone() || IsOriginal());
						gatherGimmickObject = fieldGatherGimmickObject;
						StartWaitingPacket(WAITING_PACKET.PLAYER_GATHER_GIMMICK, true, add_margin_time);
						if (!object.ReferenceEquals(playerSender, null))
						{
							playerSender.OnActGatherGimmick(id);
						}
					}
				}
			}
		}
	}

	public void OnGatherGimmickState(int state)
	{
		if (!(gatherGimmickObject == null))
		{
			GATHER_GIMMICK_TYPE type = gatherGimmickObject.GetType();
			if (type == GATHER_GIMMICK_TYPE.FISHING)
			{
				fishingCtrl.ChangeState((FishingController.eState)state);
			}
		}
	}

	protected void OnGatherGimmickGet()
	{
		if (!(gatherGimmickObject == null))
		{
			GATHER_GIMMICK_TYPE type = gatherGimmickObject.GetType();
			if (type == GATHER_GIMMICK_TYPE.FISHING)
			{
				fishingCtrl.Get();
			}
		}
	}

	protected void OnGatherGimmickEnd()
	{
		if (!(gatherGimmickObject == null))
		{
			GATHER_GIMMICK_TYPE type = gatherGimmickObject.GetType();
			if (type == GATHER_GIMMICK_TYPE.FISHING)
			{
				fishingCtrl.End();
			}
			gatherGimmickObject.OnUseEnd(this, IsCoopNone() || IsOriginal());
			gatherGimmickObject = null;
		}
	}

	public void ActBingo(int id)
	{
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
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
	}

	public void ActCannonStandby(int id)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			IFieldGimmickCannon fieldGimmickCannon = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.Cannon, id) as IFieldGimmickCannon;
			if (fieldGimmickCannon != null && !fieldGimmickCannon.IsUsing() && fieldGimmickCannon.IsAbleToUse())
			{
				base.actionID = (ACTION_ID)30;
				targetFieldGimmickCannon = fieldGimmickCannon;
				_position = fieldGimmickCannon.GetPosition();
				base._rigidbody.set_isKinematic(true);
				fieldGimmickCannon.OnBoard(this);
				PlayMotion(131, -1f);
				SetCannonState(CANNON_STATE.STANDBY);
				if (playerSender != null)
				{
					playerSender.OnCannonStandby(id);
				}
			}
		}
	}

	public void ActCannonShot()
	{
		if (cannonState == CANNON_STATE.READY && targetFieldGimmickCannon != null && !targetFieldGimmickCannon.IsCooling())
		{
			if (IsPlayingMotion(132, true))
			{
				SetNextTrigger(0);
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
		base._rigidbody.set_isKinematic(false);
	}

	public virtual bool ActSkillAction(int skill_index, bool isGuestUsingSecondGrade = false)
	{
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		SkillInfo.SkillParam skillParam = GetSkillParam(skill_index);
		if (skillParam == null || !skillParam.isValid)
		{
			return false;
		}
		if (!IsOriginal() && isGuestUsingSecondGrade)
		{
			skillParam.useGaugeCounter = (float)(int)skillParam.GetMaxGaugeValue();
			skillParam.isUsingSecondGrade = true;
		}
		EndAction();
		skillInfo.skillIndex = skill_index;
		base.actionID = (ACTION_ID)21;
		if (!string.IsNullOrEmpty(skillParam.tableData.castStateName))
		{
			PlayMotion(skillParam.tableData.castStateName, -1f);
			isSkillCastState = true;
		}
		else
		{
			PlayMotion(skillParam.tableData.actStateName, -1f);
		}
		skillInfo.OnActSkillAction();
		isActSkillAction = true;
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
				Vector3 localScale = effect.get_localScale();
				effect.set_localScale(localScale * (playerParameter.revivalRange / 0.5f));
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
		MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.AddSkillCount((int)skillParam.tableData.id, 1);
		return true;
	}

	public virtual void CheckSkillCastLoop()
	{
		if (isSkillCastLoop && Time.get_time() - skillCastLoopStartTime >= skillCastLoopTime)
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
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Expected O, but got Unknown
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
		if (!isAppliedSkillParam)
		{
			if (skillRangeEffect != null)
			{
				EffectManager.ReleaseEffect(skillRangeEffect.get_gameObject(), true, false);
				skillRangeEffect = null;
			}
			isAppliedSkillParam = true;
			if (!IsPuppet())
			{
				SkillInfo.SkillParam actSkillParam = skillInfo.actSkillParam;
				if (actSkillParam != null)
				{
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
							if (MonoBehaviourSingleton<StageObjectManager>.I.ExistsEnemyValiedHealAttack())
							{
								GameObject val = new GameObject("HealAttackObject");
								HealAttackObject healAttackObject = val.AddComponent<HealAttackObject>();
								healAttackObject.Initialize(this, base._transform, actSkillParam, Vector3.get_zero(), Vector3.get_zero(), 0f, 12);
							}
							List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
							List<StageObject>.Enumerator enumerator = playerList.GetEnumerator();
							while (enumerator.MoveNext())
							{
								Player player = enumerator.Current as Player;
								if (num > 0f)
								{
									Vector3 val2 = player._transform.get_position() - base._transform.get_position();
									if (val2.get_sqrMagnitude() > num)
									{
										continue;
									}
								}
								player.OnHealReceive(healData);
							}
						}
					}
					HEAL_TYPE healType = actSkillParam.tableData.healType;
					if (healType == HEAL_TYPE.RESURRECTION_ALL && MonoBehaviourSingleton<StageObjectManager>.IsValid())
					{
						List<StageObject> playerList2 = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
						for (int i = 0; i < playerList2.Count; i++)
						{
							Player player2 = playerList2[i] as Player;
							if (!(player2 == null))
							{
								player2.OnResurrectionReceive();
							}
						}
					}
					for (int j = 0; j < 3; j++)
					{
						if (actSkillParam.tableData.supportType[j] != BuffParam.BUFFTYPE.NONE && actSkillParam.tableData.supportType[j] < BuffParam.BUFFTYPE.MAX)
						{
							if (actSkillParam.tableData.selfOnly)
							{
								SetSelfBuff(actSkillParam.tableData.id, actSkillParam.tableData.supportType[j], actSkillParam.supportValue[j], actSkillParam.supportTime[j], actSkillParam.skillIndex);
							}
							else if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
							{
								List<StageObject> playerList3 = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
								List<StageObject>.Enumerator enumerator2 = playerList3.GetEnumerator();
								while (enumerator2.MoveNext())
								{
									Player player3 = enumerator2.Current as Player;
									if (num > 0f)
									{
										Vector3 val3 = player3._transform.get_position() - base._transform.get_position();
										if (val3.get_sqrMagnitude() > num)
										{
											continue;
										}
									}
									BuffParam.BuffData data = new BuffParam.BuffData();
									data.type = actSkillParam.tableData.supportType[j];
									data.time = actSkillParam.supportTime[j];
									data.value = actSkillParam.supportValue[j];
									SetFromInfo(ref data);
									data.skillId = actSkillParam.tableData.id;
									if (data.isSkillChargeType())
									{
										player3.OnChargeSkillGaugeReceive(data.type, data.value, (!(player3 == this)) ? (-1) : actSkillParam.skillIndex);
									}
									else
									{
										player3.OnBuffReceive(data);
									}
								}
							}
						}
					}
					if (!actSkillParam.tableData.buffTableIds.IsNullOrEmpty())
					{
						int[] buffTableIds = actSkillParam.tableData.buffTableIds;
						for (int k = 0; k < buffTableIds.Length; k++)
						{
							BuffTable.BuffData data2 = Singleton<BuffTable>.I.GetData((uint)buffTableIds[k]);
							if (data2 != null && data2.type > BuffParam.BUFFTYPE.NONE && data2.type < BuffParam.BUFFTYPE.MAX)
							{
								BuffParam.BuffData data3 = new BuffParam.BuffData();
								data3.type = data2.type;
								data3.interval = data2.interval;
								data3.valueType = data2.valueType;
								data3.time = data2.duration;
								data3.skillId = actSkillParam.tableData.id;
								SetFromInfo(ref data3);
								float num2 = (float)data2.value;
								GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(data2.growID, actSkillParam.baseInfo.level);
								if (growSkillItemData != null)
								{
									data3.time = data2.duration * (float)(int)growSkillItemData.supprtTime[0].rate * 0.01f + (float)growSkillItemData.supprtTime[0].add;
									num2 = (float)(data2.value * (int)growSkillItemData.supprtValue[0].rate) * 0.01f + (float)(int)growSkillItemData.supprtValue[0].add;
								}
								if (data3.valueType == BuffParam.VALUE_TYPE.RATE && BuffParam.IsTypeValueBasedOnHP(data3.type))
								{
									num2 = (float)base.hpMax * num2 * 0.01f;
								}
								data3.value = Mathf.FloorToInt(num2);
								if (actSkillParam.tableData.selfOnly)
								{
									OnBuffReceive(data3);
								}
								else if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
								{
									List<StageObject> playerList4 = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
									List<StageObject>.Enumerator enumerator3 = playerList4.GetEnumerator();
									while (enumerator3.MoveNext())
									{
										Player player4 = enumerator3.Current as Player;
										if (num > 0f)
										{
											Vector3 val4 = player4._transform.get_position() - base._transform.get_position();
											if (val4.get_sqrMagnitude() > num)
											{
												continue;
											}
										}
										player4.OnBuffReceive(data3);
									}
								}
							}
						}
					}
				}
			}
		}
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
		else
		{
			OnBuffReceive(data);
		}
	}

	public void OnHealReceive(HealData healData)
	{
		if (IsCoopNone() || IsOriginal())
		{
			ExecHealHp(healData, false);
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
		int num = ApplyAbilityForHealHp(healData.healHp, healData.applyAbilityTypeList);
		num = Mathf.Clamp(num, 1, 2147483647);
		int num3 = base.hp = Mathf.Clamp(base.hp + num, 0, base.hpMax);
		healHp = Mathf.Max(healHp, num3);
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
		return num;
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
		num = Mathf.Clamp(num, 0f, 3.40282347E+38f);
		return Mathf.FloorToInt((float)baseHealHp * num);
	}

	private void ExecHealEffect(HEAL_EFFECT_TYPE healEffectType)
	{
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
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
				AddObjectList(healEffectTransform.get_gameObject(), OBJECT_LIST_TYPE.STATIC);
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
				SetNextTrigger(0);
			}
			break;
		case HEAL_TYPE.POISON:
			OnBuffEnd(BuffParam.BUFFTYPE.POISON, false, true);
			OnBuffEnd(BuffParam.BUFFTYPE.DEADLY_POISON, false, true);
			break;
		case HEAL_TYPE.BURNING:
			OnBuffEnd(BuffParam.BUFFTYPE.BURNING, false, true);
			break;
		case HEAL_TYPE.SPEEDDOWN:
			OnBuffEnd(BuffParam.BUFFTYPE.MOVE_SPEED_DOWN, false, true);
			break;
		case HEAL_TYPE.SLIDE:
			OnBuffEnd(BuffParam.BUFFTYPE.SLIDE, false, true);
			break;
		case HEAL_TYPE.SILENCE:
			OnBuffEnd(BuffParam.BUFFTYPE.SILENCE, false, true);
			break;
		case HEAL_TYPE.ATTACK_SPEED_DOWN:
			OnBuffEnd(BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN, false, true);
			break;
		case HEAL_TYPE.CANT_HEAL_HP:
			OnBuffEnd(BuffParam.BUFFTYPE.CANT_HEAL_HP, false, true);
			break;
		case HEAL_TYPE.BLIND:
			OnBuffEnd(BuffParam.BUFFTYPE.BLIND, false, true);
			break;
		case HEAL_TYPE.ALL_BADSTATUS:
			if (base.actionID == ACTION_ID.PARALYZE)
			{
				SetNextTrigger(0);
			}
			OnBuffEnd(BuffParam.BUFFTYPE.POISON, false, true);
			OnBuffEnd(BuffParam.BUFFTYPE.DEADLY_POISON, false, true);
			OnBuffEnd(BuffParam.BUFFTYPE.BURNING, false, true);
			OnBuffEnd(BuffParam.BUFFTYPE.MOVE_SPEED_DOWN, false, true);
			OnBuffEnd(BuffParam.BUFFTYPE.SLIDE, false, true);
			OnBuffEnd(BuffParam.BUFFTYPE.SILENCE, false, true);
			OnBuffEnd(BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN, false, true);
			OnBuffEnd(BuffParam.BUFFTYPE.CANT_HEAL_HP, false, true);
			OnBuffEnd(BuffParam.BUFFTYPE.BLIND, false, true);
			break;
		}
	}

	public void OnChargeSkillGaugeReceive(BuffParam.BUFFTYPE buffType, int buffValue, int useSkillIndex)
	{
		if (IsCoopNone() || IsOriginal())
		{
			OnGetChargeSkillGauge(buffType, buffValue, useSkillIndex, false, true);
		}
		else if (playerSender != null)
		{
			playerSender.OnChargeSkillGaugeReceive(buffType, buffValue, useSkillIndex);
		}
	}

	public void OnGetChargeSkillGauge(BuffParam.BUFFTYPE buffType, int buffValue, int useSkillIndex, bool packet = false, bool isCorrectWaveMatch = true)
	{
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Expected O, but got Unknown
		if (!base.isDead)
		{
			for (int i = 0; i < 3; i++)
			{
				int num = i + skillInfo.weaponOffset;
				if (num != useSkillIndex)
				{
					SkillInfo.SkillParam skillParam = skillInfo.GetSkillParam(num);
					if (!object.ReferenceEquals(skillParam, null))
					{
						float add_gauge = (float)buffValue;
						switch (buffType)
						{
						case BuffParam.BUFFTYPE.SKILL_CHARGE_RATE:
							add_gauge = (float)(int)skillParam.useGauge * ((float)buffValue * 0.01f);
							goto default;
						case BuffParam.BUFFTYPE.SKILL_CHARGE_FIRE:
							if (skillParam.tableData.type != SKILL_SLOT_TYPE.ATTACK || !skillParam.tableData.HasElement(ELEMENT_TYPE.FIRE))
							{
								break;
							}
							goto default;
						case BuffParam.BUFFTYPE.SKILL_CHARGE_WATER:
							if (skillParam.tableData.type != SKILL_SLOT_TYPE.ATTACK || !skillParam.tableData.HasElement(ELEMENT_TYPE.WATER))
							{
								break;
							}
							goto default;
						case BuffParam.BUFFTYPE.SKILL_CHARGE_THUNDER:
							if (skillParam.tableData.type != SKILL_SLOT_TYPE.ATTACK || !skillParam.tableData.HasElement(ELEMENT_TYPE.THUNDER))
							{
								break;
							}
							goto default;
						case BuffParam.BUFFTYPE.SKILL_CHARGE_SOIL:
							if (skillParam.tableData.type != SKILL_SLOT_TYPE.ATTACK || !skillParam.tableData.HasElement(ELEMENT_TYPE.SOIL))
							{
								break;
							}
							goto default;
						case BuffParam.BUFFTYPE.SKILL_CHARGE_LIGHT:
							if (skillParam.tableData.type != SKILL_SLOT_TYPE.ATTACK || !skillParam.tableData.HasElement(ELEMENT_TYPE.LIGHT))
							{
								break;
							}
							goto default;
						case BuffParam.BUFFTYPE.SKILL_CHARGE_DARK:
							if (skillParam.tableData.type != SKILL_SLOT_TYPE.ATTACK || !skillParam.tableData.HasElement(ELEMENT_TYPE.DARK))
							{
								break;
							}
							goto default;
						default:
							skillInfo.AddUseGaugeByIndex(num, add_gauge, true, true, isCorrectWaveMatch, true);
							break;
						}
					}
				}
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
					AddObjectList(skillChargeEffectTransform.get_gameObject(), OBJECT_LIST_TYPE.STATIC);
				}
			}
			if (playerSender != null && !packet)
			{
				playerSender.OnGetChargeSkillGauge(buffType, buffValue, useSkillIndex, isCorrectWaveMatch);
			}
		}
	}

	private void OnResurrectionReceive()
	{
		if (IsCoopNone() || IsOriginal())
		{
			OnResurrection(false);
		}
		else
		{
			playerSender.OnResurrectionReceive();
		}
	}

	public unsafe void OnResurrection(bool isPacket = false)
	{
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Expected O, but got Unknown
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		if (base.isDead && !MonoBehaviourSingleton<CoopManager>.I.coopStage.IsPresentQuest() && IsAbleToRescueByRemainRescueTime() && (base.actionID != (ACTION_ID)23 || !(rescueTime <= 0f)))
		{
			if (isPacket && playerSender != null)
			{
				playerSender.OnGetResurrection();
			}
			DeactivateRescueTimer();
			this.StartCoroutine(OnPlayAndDoResurrection(new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
		}
	}

	public void OnGetResurrection()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		if (base.isDead && IsAbleToRescueByRemainRescueTime() && (base.actionID != (ACTION_ID)23 || !(rescueTime <= 0f)))
		{
			this.StartCoroutine(OnPlayResurrection());
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
			Transform resurrectionEffectTrans = EffectManager.GetEffect("ef_btl_sk_heal_04_03", base._transform);
			if (resurrectionEffectTrans != null)
			{
				Animator effectAnim = resurrectionEffectTrans.get_gameObject().GetComponent<Animator>();
				if (effectAnim != null)
				{
					while (true)
					{
						AnimatorStateInfo currentAnimatorStateInfo = effectAnim.GetCurrentAnimatorStateInfo(0);
						if (currentAnimatorStateInfo.get_fullPathHash() == Animator.StringToHash("Base Layer.START"))
						{
							break;
						}
						yield return (object)null;
					}
					while (true)
					{
						AnimatorStateInfo currentAnimatorStateInfo2 = effectAnim.GetCurrentAnimatorStateInfo(0);
						if (!(currentAnimatorStateInfo2.get_normalizedTime() <= 1f))
						{
							break;
						}
						yield return (object)null;
					}
				}
			}
		}
		if (base.isDead && callback != null)
		{
			callback.Invoke();
		}
		ActivateRescueTimer();
	}

	private void CheckAutoRevive()
	{
		autoReviveHp = 0;
		if (buffParam.IsValidBuff(BuffParam.BUFFTYPE.AUTO_REVIVE))
		{
			autoReviveHp = Mathf.CeilToInt((float)buffParam.GetValue(BuffParam.BUFFTYPE.AUTO_REVIVE, true) * 0.01f * (float)base.hpMax);
		}
	}

	public unsafe bool OnAutoRevive()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		if (!base.isDead)
		{
			return false;
		}
		if ((float)autoReviveHp <= 0f)
		{
			return false;
		}
		DeactivateRescueTimer();
		this.StartCoroutine(OnPlayAndDoResurrection(new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
		return true;
	}

	public override bool OnBuffEnd(BuffParam.BUFFTYPE type, bool sync, bool isPlayEndEffect = true)
	{
		DeleteWeaponLinkEffect("BUFF_LOOP_" + type.ToString());
		return base.OnBuffEnd(type, sync, isPlayEndEffect);
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
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected O, but got Unknown
		foreach (WEAPON_EFFECT_DATA weaponEffectData in m_weaponEffectDataList)
		{
			EffectPlayProcessor.EffectSetting setting = weaponEffectData.setting;
			if (!(setting.name != settingName))
			{
				Transform effect = EffectManager.GetEffect(setting.effectName, FindNode(setting.nodeName));
				if (effect != null)
				{
					effect.set_localPosition(setting.position);
					effect.set_localRotation(Quaternion.Euler(setting.rotation));
					float num = setting.scale;
					if (num == 0f)
					{
						num = 1f;
					}
					effect.set_localScale(Vector3.get_one() * num);
					weaponEffectData.effectObj = effect.get_gameObject();
				}
			}
		}
	}

	public void DeleteWeaponLinkEffect(string settingName)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		foreach (WEAPON_EFFECT_DATA weaponEffectData in m_weaponEffectDataList)
		{
			if (!(weaponEffectData.setting.name != settingName) && weaponEffectData.effectObj != null)
			{
				weaponEffectData.effectObj.get_transform().SetParent(null);
				EffectManager.ReleaseEffect(weaponEffectData.effectObj, true, false);
			}
		}
	}

	public void DeleteWeaponLinkEffectAll()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		foreach (WEAPON_EFFECT_DATA weaponEffectData in m_weaponEffectDataList)
		{
			if (weaponEffectData.effectObj != null)
			{
				weaponEffectData.effectObj.get_transform().SetParent(null);
				EffectManager.ReleaseEffect(weaponEffectData.effectObj, true, false);
			}
		}
		m_weaponEffectDataList.Clear();
	}

	private unsafe void DetachRootEffectTemporary()
	{
		Transform attachTrans = (!MonoBehaviourSingleton<EffectManager>.IsValid()) ? MonoBehaviourSingleton<StageObjectManager>.I._transform : MonoBehaviourSingleton<EffectManager>.I._transform;
		_003CDetachRootEffectTemporary_003Ec__AnonStorey510 _003CDetachRootEffectTemporary_003Ec__AnonStorey;
		effectTransTable.ForEachKeyAndValue(new Action<string, Transform>((object)_003CDetachRootEffectTemporary_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private void ReAttachRootEffect()
	{
		rootEffectDetachTemporaryTable.ForEach(delegate(Transform value)
		{
			if (value != null)
			{
				value.set_parent(base.rootNode);
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
			return spAttackType == SP_ATTACK_TYPE.NONE || spAttackType == SP_ATTACK_TYPE.SOUL;
		default:
			return false;
		}
	}

	public virtual bool ActSpecialAction(bool start_effect = true, bool isSuccess = true)
	{
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Expected O, but got Unknown
		bool isActSpecialAction = true;
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
				ActAttack(playerParameter.ohsActionInfo.Soul_AlteredSpAttackId, false, false, string.Empty);
				snatchCtrl.OnShot();
				break;
			}
			break;
		case ATTACK_MODE.TWO_HAND_SWORD:
		{
			int _attackId = playerParameter.specialActionInfo.spAttackID;
			string _motionLayerName = "Base Layer.";
			if (thsCtrl != null)
			{
				thsCtrl.GetSpActionInfo(spAttackType, extraAttackType, ref _attackId, ref _motionLayerName);
			}
			string motionLayerName = _motionLayerName;
			ActAttack(_attackId, false, false, motionLayerName);
			break;
		}
		case ATTACK_MODE.SPEAR:
		{
			int id = playerParameter.specialActionInfo.spAttackID;
			switch (spAttackType)
			{
			case SP_ATTACK_TYPE.NONE:
				id = playerParameter.spearActionInfo.rushLoopAttackID;
				isActSpecialAction = false;
				break;
			case SP_ATTACK_TYPE.HEAT:
				id = 97;
				break;
			case SP_ATTACK_TYPE.SOUL:
				id = playerParameter.spearActionInfo.Soul_SpAttackId;
				break;
			}
			ActAttack(id, false, false, string.Empty);
			break;
		}
		case ATTACK_MODE.PAIR_SWORDS:
			switch (spAttackType)
			{
			case SP_ATTACK_TYPE.NONE:
				if (base.attackID == 20)
				{
					ActAttack(playerParameter.pairSwordsActionInfo.wildDanceNoneChargeAttackID, false, false, string.Empty);
				}
				else
				{
					ActAttack(playerParameter.pairSwordsActionInfo.wildDanceAttackID, false, false, string.Empty);
				}
				break;
			case SP_ATTACK_TYPE.HEAT:
				if (!isSuccess)
				{
					ActAttackFailure(97, false);
				}
				else
				{
					if (isBoostMode)
					{
						return true;
					}
					ActAttack(98, false, false, string.Empty);
					StartBoostMode();
					if (MonoBehaviourSingleton<EffectManager>.IsValid())
					{
						Transform effect = EffectManager.GetEffect("ef_btl_wsk_twinsword_01_02", null);
						if (effect != null)
						{
							effect.set_position(_position);
							pairSwordsBoostModeAuraEffectList.Add(effect);
						}
						effect = EffectManager.GetEffect("ef_btl_wsk_twinsword_01_03", FindNode("R_Wep"));
						if (effect != null)
						{
							effect.set_localPosition(new Vector3(0.2f, 0f, 0f));
							pairSwordsBoostModeTrailEffectList.Add(effect);
						}
						effect = EffectManager.GetEffect("ef_btl_wsk_twinsword_01_03", FindNode("L_Wep"));
						if (effect != null)
						{
							effect.set_localPosition(new Vector3(-0.2f, 0f, 0f));
							pairSwordsBoostModeTrailEffectList.Add(effect);
						}
					}
					if (playerSender != null)
					{
						playerSender.OnSyncSpActionGauge();
					}
				}
				break;
			case SP_ATTACK_TYPE.SOUL:
				ActAttack(playerParameter.pairSwordsActionInfo.Soul_SpLaserWaitAttackId, false, false, string.Empty);
				pairSwordsCtrl.OnStartCharge();
				break;
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
				AddObjectList(effect2.get_gameObject(), OBJECT_LIST_TYPE.STATIC);
			}
		}
		if (playerSender != null)
		{
			playerSender.OnActSpecialAction(start_effect, isSuccess);
		}
		return true;
	}

	public bool isSpecialActionHit(ATTACK_MODE attack_mode, AttackHitInfo attack_info, AttackHitColliderProcessor.HitParam hit_param)
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
				flag = bulletObject.isAimBossMode;
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
		return attack_info.toEnemy.isSpecialAttack;
	}

	private void SetValueSpActionGaugeMax(EquipItemTable.EquipItemData equipItemData, int weaponIndex)
	{
		if (equipItemData != null)
		{
			float num = 1000f;
			bool flag = true;
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
					num = 1000f;
					flag = false;
					break;
				}
				break;
			case EQUIPMENT_TYPE.PAIR_SWORDS:
				switch (equipItemData.spAttackType)
				{
				case SP_ATTACK_TYPE.NONE:
					return;
				case SP_ATTACK_TYPE.HEAT:
					flag = true;
					break;
				case SP_ATTACK_TYPE.SOUL:
					flag = false;
					break;
				}
				break;
			case EQUIPMENT_TYPE.ARROW:
				if (equipItemData.spAttackType != SP_ATTACK_TYPE.SOUL)
				{
					return;
				}
				flag = false;
				break;
			}
			if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
			{
				MonoBehaviourSingleton<UIPlayerStatus>.I.SetGaugeEffectColor(equipItemData.spAttackType);
			}
			if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
			{
				MonoBehaviourSingleton<UIEnduranceStatus>.I.SetGaugeEffectColor(equipItemData.spAttackType);
			}
			spActionGaugeMax[weaponIndex] = num;
			if (flag)
			{
				spActionGauge[weaponIndex] = num;
			}
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
		if (!IsBurstTwoHandSword())
		{
			return false;
		}
		if (thsCtrl == null)
		{
			return false;
		}
		return thsCtrl.IsEnableChangeActionByLongTap();
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
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		if (CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.SOUL) && snatchCtrl.IsSnatching())
		{
			Vector3 forward = _forward;
			forward.y = 0f;
			forward.Normalize();
			Vector3 val = snatchCtrl.GetSnatchPos() - _position;
			val.y = 0f;
			Vector3 val2 = Vector3.Cross(forward, val);
			int num = (val2.y >= 0f) ? 1 : (-1);
			float num2 = Vector3.Angle(forward, val);
			Quaternion rotation = _rotation;
			Vector3 eulerAngles = rotation.get_eulerAngles();
			float num3 = num2;
			if (rotateEventSpeed > 0f)
			{
				num3 = rotateEventSpeed * Time.get_deltaTime();
				if (num2 <= num3)
				{
					num3 = num2;
				}
			}
			_rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y + (float)num * num3, eulerAngles.z);
		}
	}

	public void DeactiveSnatchMove()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		enableEventMove = false;
		base.enableAddForce = false;
		eventMoveVelocity = Vector3.get_zero();
		SetVelocity(Vector3.get_zero(), VELOCITY_TYPE.NONE);
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

	private float GetBoostAttackSpeedUp()
	{
		if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			return 0f;
		}
		if (base.actionID == ACTION_ID.ATTACK)
		{
			switch (attackMode)
			{
			case ATTACK_MODE.ONE_HAND_SWORD:
			{
				SP_ATTACK_TYPE spAttackType = this.spAttackType;
				if (spAttackType == SP_ATTACK_TYPE.BURST)
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
				return 0f;
			}
			case ATTACK_MODE.PAIR_SWORDS:
				switch (this.spAttackType)
				{
				case SP_ATTACK_TYPE.HEAT:
					if (!isBoostMode)
					{
						return 0f;
					}
					if (base.attackID == 98)
					{
						return 0f;
					}
					return MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.boostAttackAndMoveSpeedUpRate;
				case SP_ATTACK_TYPE.SOUL:
					return pairSwordsCtrl.GetAttackSpeedUpRate();
				default:
					return 0f;
				}
			case ATTACK_MODE.TWO_HAND_SWORD:
				if (!isBoostMode)
				{
					return 0f;
				}
				if (this.spAttackType != SP_ATTACK_TYPE.SOUL)
				{
					return 0f;
				}
				if (thsCtrl == null)
				{
					return 0f;
				}
				return thsCtrl.TwoHandSwordBoostAttackSpeed;
			default:
				return 0f;
			}
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
			if (spAttackType == SP_ATTACK_TYPE.SOUL)
			{
				return MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.soulWalkSpeed;
			}
			break;
		case ATTACK_MODE.PAIR_SWORDS:
			if (spAttackType == SP_ATTACK_TYPE.HEAT && isBoostMode)
			{
				return MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.boostAttackAndMoveSpeedUpRate;
			}
			break;
		case ATTACK_MODE.SPEAR:
			switch (spAttackType)
			{
			case SP_ATTACK_TYPE.NONE:
				return 0f;
			case SP_ATTACK_TYPE.HEAT:
				return MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.heatWalkSpeed;
			case SP_ATTACK_TYPE.SOUL:
				return MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.Soul_WalkSpeedUpRate;
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
			if (isBoostMode)
			{
				return MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.boostAvoidUpRate;
			}
			break;
		}
		return 0f;
	}

	private void UpdateSpActionGauge()
	{
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid() && isBoostMode)
		{
			float num = 0f;
			switch (attackMode)
			{
			case ATTACK_MODE.ONE_HAND_SWORD:
				if (spAttackType == SP_ATTACK_TYPE.SOUL)
				{
					num = ((snatchCtrl.state != SnatchController.STATE.SNATCH) ? (playerParameter.ohsActionInfo.Soul_BoostGaugeDecreasePerSecond * Time.get_deltaTime()) : (playerParameter.ohsActionInfo.Soul_BoostSnatchGaugeDecreasePerSecond * Time.get_deltaTime()));
				}
				else
				{
					if (spAttackType != SP_ATTACK_TYPE.BURST)
					{
						return;
					}
					num = 1000f / playerParameter.ohsActionInfo.burstOHSInfo.GaugeTime * Time.get_deltaTime();
				}
				break;
			case ATTACK_MODE.TWO_HAND_SWORD:
				if (spAttackType != SP_ATTACK_TYPE.SOUL)
				{
					return;
				}
				num = ((!enableInputCharge) ? (playerParameter.twoHandSwordActionInfo.soulBoostGaugeDecreasePerSecond * Time.get_deltaTime()) : (playerParameter.twoHandSwordActionInfo.soulBoostChargeGaugeDecreasePerSecond * Time.get_deltaTime()));
				break;
			case ATTACK_MODE.ARROW:
				if (spAttackType != SP_ATTACK_TYPE.SOUL)
				{
					return;
				}
				num = playerParameter.arrowActionInfo.soulBoostGaugeDecreasePerSecond * Time.get_deltaTime();
				break;
			}
			float num2 = 1f + buffParam.GetGaugeDecreaseRate();
			spActionGauge[weaponIndex] -= num * num2;
			if (spActionGauge[weaponIndex] <= 0f)
			{
				spActionGauge[weaponIndex] = 0f;
			}
		}
	}

	private void CheckContinueBoostMode()
	{
		if (isBoostMode)
		{
			switch (attackMode)
			{
			case ATTACK_MODE.ONE_HAND_SWORD:
				if ((spAttackType != SP_ATTACK_TYPE.SOUL && spAttackType != SP_ATTACK_TYPE.BURST) || (!IsCoopNone() && !IsOriginal()) || spActionGauge[weaponIndex] > 0f)
				{
					return;
				}
				break;
			case ATTACK_MODE.TWO_HAND_SWORD:
			case ATTACK_MODE.SPEAR:
			case ATTACK_MODE.ARROW:
				if (spAttackType != SP_ATTACK_TYPE.SOUL || (!IsCoopNone() && !IsOriginal()) || spActionGauge[weaponIndex] > 0f)
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
	}

	public void IncreaseSpActonGauge(AttackHitInfo.ATTACK_TYPE attackType, Vector3 hitPosition, float baseValue = 0f, float attackRate = 0f, bool isSpecialAttack = false, bool dontIncreaseGauge = false)
	{
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		if (!dontIncreaseGauge && MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			if (IsEvolveWeapon())
			{
				evolveCtrl.IncreaseCurrentGauge(attackType, attackMode);
			}
			else
			{
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
							_increaseValue = baseValue / (float)base.hpMax * playerParameter.ohsActionInfo.Heat_RevengeValue * ((!_CheckJustGuardSec()) ? playerParameter.ohsActionInfo.Heat_RevengeGuardRate : playerParameter.ohsActionInfo.Heat_RevengeJustGuardRate);
							break;
						case AttackHitInfo.ATTACK_TYPE.COUNTER2:
							_increaseValue = ((!isJustGuard) ? playerParameter.ohsActionInfo.Heat_RevengeCounterValue : playerParameter.ohsActionInfo.Heat_RevengeJustCounterValue);
							break;
						}
						break;
					case SP_ATTACK_TYPE.SOUL:
						if (soulEnergyCtrl != null)
						{
							SoulEnergy soulEnergy = null;
							if (base.attackID == playerParameter.ohsActionInfo.Soul_AlteredSpAttackId && isSpecialAttack)
							{
								soulEnergy = soulEnergyCtrl.Get(playerParameter.ohsActionInfo.Soul_ComboGaugeIncreaseValue);
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
							}
						}
						return;
					case SP_ATTACK_TYPE.BURST:
						if (attackType == AttackHitInfo.ATTACK_TYPE.COUNTER_BURST)
						{
							_increaseValue = 1000f;
						}
						flag = true;
						break;
					}
					break;
				case ATTACK_MODE.TWO_HAND_SWORD:
					if (thsCtrl == null || !thsCtrl.GetSpGaugeIncreaseValue(attackType, base.attackID, attackRate, soulEnergyCtrl, hitPosition, chargeRate, ref _gaugeMax, ref _increaseValue))
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
						float num2 = (float)weaponEquipItemDataList[weaponIndex].spAttackRate;
						_increaseValue = MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.jumpGaugeIncreaseBase * num2 * 0.01f;
						break;
					}
					case SP_ATTACK_TYPE.SOUL:
						if (!isSpecialAttack && soulEnergyCtrl != null)
						{
							SoulEnergy soulEnergy2 = null;
							soulEnergy2 = soulEnergyCtrl.Get(playerParameter.spearActionInfo.Soul_GaugeIncreaseValue);
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
						}
						return;
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
						float num = (float)weaponEquipItemDataList[weaponIndex].spAttackRate;
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
					}
					break;
				case ATTACK_MODE.ARROW:
					if (spAttackType != SP_ATTACK_TYPE.SOUL || isBoostMode)
					{
						return;
					}
					flag = true;
					_increaseValue = playerParameter.arrowActionInfo.soulGaugeIncreaseValue;
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
		}
	}

	public void IncreaseSoulGauge(float baseValue, bool isJust)
	{
		if (!base.isDead && MonoBehaviourSingleton<InGameSettingsManager>.IsValid() && spAttackType == SP_ATTACK_TYPE.SOUL)
		{
			switch (attackMode)
			{
			case ATTACK_MODE.ONE_HAND_SWORD:
			{
				InGameSettingsManager.Player.OneHandSwordActionInfo ohsActionInfo = playerParameter.ohsActionInfo;
				float num2 = baseValue * ((!isJust) ? 1f : ohsActionInfo.Soul_JustTapGaugeRate) * ((!isBoostMode) ? 1f : ohsActionInfo.Soul_BoostModeGaugeRate);
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
				float num4 = baseValue * ((!isJust) ? 1f : twoHandSwordActionInfo.soulJustTapGaugeRate) * ((!isBoostMode) ? 1f : twoHandSwordActionInfo.soulBoostModeGaugeRate);
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
				float num = baseValue * ((!isJust) ? 1f : spearActionInfo.Soul_JustTapGaugeRate) * ((!isBoostMode) ? 1f : spearActionInfo.Soul_BoostModeGaugeRate);
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
	}

	public float CalcWaveMatchSpGauge(float value)
	{
		if (!QuestManager.IsValidInGameWaveMatch(false) || !MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			return value;
		}
		if (!CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.BURST))
		{
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
		return value;
	}

	public void UpdateBoostHitCount()
	{
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		if (isBoostMode)
		{
			switch (attackMode)
			{
			case ATTACK_MODE.SPEAR:
				break;
			case ATTACK_MODE.PAIR_SWORDS:
				if (spAttackType == SP_ATTACK_TYPE.HEAT)
				{
					boostModeDamageUpHitCount++;
					if (boostModeDamageUpHitCount >= MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.boostDamageUpLevelUpHitCount)
					{
						int boostDamageUpLevelMax = MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo.boostDamageUpLevelMax;
						if (MonoBehaviourSingleton<EffectManager>.IsValid())
						{
							if (boostModeDamageUpLevel < boostDamageUpLevelMax)
							{
								EffectManager.OneShot("ef_btl_wsk_twinsword_01_04", _position, Quaternion.get_identity(), false);
							}
							if (boostModeDamageUpLevel == boostDamageUpLevelMax - 1)
							{
								Transform effect = EffectManager.GetEffect("ef_btl_wsk_twinsword_01_05", null);
								effect.set_position(_position);
								pairSwordsBoostModeAuraEffectList.Add(effect);
							}
						}
						boostModeDamageUpHitCount = 0;
						boostModeDamageUpLevel++;
						if (boostModeDamageUpLevel >= boostDamageUpLevelMax)
						{
							boostModeDamageUpLevel = boostDamageUpLevelMax;
						}
					}
				}
				break;
			case ATTACK_MODE.TWO_HAND_SWORD:
				if (spAttackType == SP_ATTACK_TYPE.SOUL && thsCtrl != null)
				{
					thsCtrl.SetTwoHandSwordBoostAttackSpeed(thsCtrl.TwoHandSwordBoostAttackSpeed + playerParameter.twoHandSwordActionInfo.soulBoostAddAttackSpeed);
				}
				break;
			}
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

	protected virtual bool StartBoostMode()
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		if (isBoostMode)
		{
			return false;
		}
		switch (attackMode)
		{
		case ATTACK_MODE.ONE_HAND_SWORD:
			if (spAttackType == SP_ATTACK_TYPE.SOUL)
			{
				SoundManager.PlayOneShotSE(playerParameter.twoHandSwordActionInfo.soulBoostSeId, this, FindNode(string.Empty));
				Transform effect2 = EffectManager.GetEffect("ef_btl_wsk2_longsword_02_01", null);
				if (effect2 != null)
				{
					effect2.set_position(_position);
				}
				if (object.ReferenceEquals(twoHandSwordsBoostLoopEffect, null))
				{
					twoHandSwordsBoostLoopEffect = EffectManager.GetEffect("ef_btl_wsk2_longsword_03_01", FindNode("Root"));
				}
				StartWaitingPacket(WAITING_PACKET.PLAYER_SOUL_BOOST, false, playerParameter.twoHandSwordActionInfo.soulBoostWaitPacketSec);
				if (playerSender != null)
				{
					playerSender.OnSyncSoulBoost(true);
				}
			}
			else
			{
				if (spAttackType != SP_ATTACK_TYPE.BURST)
				{
					return false;
				}
				if (object.ReferenceEquals(twoHandSwordsBoostLoopEffect, null))
				{
					twoHandSwordsBoostLoopEffect = EffectManager.GetEffect("ef_btl_wsk3_sword_aura_01", FindNode("Root"));
				}
				if (playerSender != null)
				{
					playerSender.OnSyncSoulBoost(true);
				}
			}
			break;
		case ATTACK_MODE.TWO_HAND_SWORD:
		{
			if (spAttackType != SP_ATTACK_TYPE.SOUL)
			{
				return false;
			}
			SoundManager.PlayOneShotSE(playerParameter.twoHandSwordActionInfo.soulBoostSeId, this, FindNode(string.Empty));
			Transform effect5 = EffectManager.GetEffect("ef_btl_wsk2_longsword_02_01", null);
			if (effect5 != null)
			{
				effect5.set_position(_position);
			}
			if (object.ReferenceEquals(twoHandSwordsBoostLoopEffect, null))
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
			StartWaitingPacket(WAITING_PACKET.PLAYER_SOUL_BOOST, false, playerParameter.twoHandSwordActionInfo.soulBoostWaitPacketSec);
			if (playerSender != null)
			{
				playerSender.OnSyncSoulBoost(true);
			}
			break;
		}
		case ATTACK_MODE.SPEAR:
		{
			if (spAttackType != SP_ATTACK_TYPE.SOUL)
			{
				return false;
			}
			SoundManager.PlayOneShotSE(playerParameter.twoHandSwordActionInfo.soulBoostSeId, this, FindNode(string.Empty));
			Transform effect3 = EffectManager.GetEffect("ef_btl_wsk2_longsword_02_01", null);
			if (effect3 != null)
			{
				effect3.set_position(_position);
			}
			if (twoHandSwordsBoostLoopEffect == null)
			{
				twoHandSwordsBoostLoopEffect = EffectManager.GetEffect("ef_btl_wsk2_longsword_03_01", FindNode("Root"));
			}
			StartWaitingPacket(WAITING_PACKET.PLAYER_SOUL_BOOST, false, playerParameter.twoHandSwordActionInfo.soulBoostWaitPacketSec);
			if (playerSender != null)
			{
				playerSender.OnSyncSoulBoost(true);
			}
			break;
		}
		case ATTACK_MODE.PAIR_SWORDS:
			switch (spAttackType)
			{
			case SP_ATTACK_TYPE.HEAT:
				boostModeDamageUpLevel = 1;
				break;
			case SP_ATTACK_TYPE.SOUL:
			{
				SoundManager.PlayOneShotSE(playerParameter.twoHandSwordActionInfo.soulBoostSeId, this, FindNode(string.Empty));
				Transform effect4 = EffectManager.GetEffect("ef_btl_wsk2_longsword_02_01", null);
				if (effect4 != null)
				{
					effect4.set_position(_position);
				}
				if (object.ReferenceEquals(twoHandSwordsBoostLoopEffect, null))
				{
					twoHandSwordsBoostLoopEffect = EffectManager.GetEffect("ef_btl_wsk2_longsword_03_01", FindNode("Root"));
				}
				StartWaitingPacket(WAITING_PACKET.PLAYER_SOUL_BOOST, false, playerParameter.twoHandSwordActionInfo.soulBoostWaitPacketSec);
				if (playerSender != null)
				{
					playerSender.OnSyncSoulBoost(true);
				}
				break;
			}
			}
			break;
		case ATTACK_MODE.ARROW:
		{
			if (spAttackType != SP_ATTACK_TYPE.SOUL)
			{
				return false;
			}
			SoundManager.PlayOneShotSE(playerParameter.twoHandSwordActionInfo.soulBoostSeId, this, FindNode(string.Empty));
			SoundManager.PlayOneShotUISE(40000359);
			Transform effect = EffectManager.GetEffect("ef_btl_wsk2_longsword_02_01", null);
			if (effect != null)
			{
				effect.set_position(_position);
			}
			if (object.ReferenceEquals(twoHandSwordsBoostLoopEffect, null))
			{
				twoHandSwordsBoostLoopEffect = EffectManager.GetEffect("ef_btl_wsk2_longsword_03_01", FindNode("Root"));
			}
			float num = 1f + buffParam.GetGaugeDecreaseRate();
			float add_margin_time = 1000f / (playerParameter.arrowActionInfo.soulBoostGaugeDecreasePerSecond * num);
			StartWaitingPacket(WAITING_PACKET.PLAYER_SOUL_BOOST, false, add_margin_time);
			if (playerSender != null)
			{
				playerSender.OnSyncSoulBoost(true);
			}
			break;
		}
		}
		isBoostMode = true;
		return true;
	}

	private void FinishBoostMode()
	{
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Expected O, but got Unknown
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Expected O, but got Unknown
		if (isBoostMode)
		{
			switch (attackMode)
			{
			case ATTACK_MODE.ONE_HAND_SWORD:
				if (spAttackType != SP_ATTACK_TYPE.SOUL && spAttackType != SP_ATTACK_TYPE.BURST)
				{
					return;
				}
				ReleaseEffect(ref twoHandSwordsBoostLoopEffect, true);
				EndWaitingPacket(WAITING_PACKET.PLAYER_SOUL_BOOST);
				if (playerSender != null)
				{
					playerSender.OnSyncSoulBoost(false);
				}
				break;
			case ATTACK_MODE.TWO_HAND_SWORD:
				if (spAttackType != SP_ATTACK_TYPE.SOUL)
				{
					return;
				}
				ReleaseEffect(ref twoHandSwordsBoostLoopEffect, true);
				EndWaitingPacket(WAITING_PACKET.PLAYER_SOUL_BOOST);
				if (playerSender != null)
				{
					playerSender.OnSyncSoulBoost(false);
				}
				break;
			case ATTACK_MODE.SPEAR:
				if (!CheckSpAttackType(SP_ATTACK_TYPE.SOUL))
				{
					return;
				}
				ReleaseEffect(ref twoHandSwordsBoostLoopEffect, true);
				EndWaitingPacket(WAITING_PACKET.PLAYER_SOUL_BOOST);
				if (playerSender != null)
				{
					playerSender.OnSyncSoulBoost(false);
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
								EffectManager.ReleaseEffect(pairSwordsBoostModeTrailEffectList[i].get_gameObject(), true, false);
								pairSwordsBoostModeTrailEffectList[i] = null;
							}
							pairSwordsBoostModeTrailEffectList.Clear();
						}
						if (!pairSwordsBoostModeAuraEffectList.IsNullOrEmpty())
						{
							for (int j = 0; j < pairSwordsBoostModeAuraEffectList.Count; j++)
							{
								EffectManager.ReleaseEffect(pairSwordsBoostModeAuraEffectList[j].get_gameObject(), true, false);
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
					ReleaseEffect(ref twoHandSwordsBoostLoopEffect, true);
					EndWaitingPacket(WAITING_PACKET.PLAYER_SOUL_BOOST);
					if (playerSender != null)
					{
						playerSender.OnSyncSoulBoost(false);
					}
					break;
				}
				break;
			case ATTACK_MODE.ARROW:
				if (spAttackType != SP_ATTACK_TYPE.SOUL)
				{
					return;
				}
				ReleaseEffect(ref twoHandSwordsBoostLoopEffect, true);
				if (MonoBehaviourSingleton<TargetMarkerManager>.IsValid())
				{
					MonoBehaviourSingleton<TargetMarkerManager>.I.EndMultiLockBoost();
				}
				EndWaitingPacket(WAITING_PACKET.PLAYER_SOUL_BOOST);
				if (playerSender != null)
				{
					playerSender.OnSyncSoulBoost(false);
				}
				break;
			}
			isBoostMode = false;
			ResetBoostModeAtkLevelUpAndHitCount();
		}
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
			if (!IsAbleToFlickAttackBySoulOneHandSword())
			{
				return false;
			}
			ActAttack(snatchCtrl.GetAttackId(flickDirection), true, false, string.Empty);
			break;
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
		ActAttack(89, false, false, string.Empty);
		return true;
	}

	protected override void UpdateAction()
	{
		base.UpdateAction();
		switch (base.actionID)
		{
		case ACTION_ID.IDLE:
			if (prayTargetIds.Count > 0)
			{
				int num2 = 0;
				int count2 = prayTargetIds.Count;
				while (true)
				{
					if (num2 >= count2)
					{
						return;
					}
					Player player2 = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(prayTargetIds[num2]) as Player;
					if (player2 != null && player2.isDead)
					{
						break;
					}
					num2++;
				}
				ActPrayer();
			}
			break;
		case (ACTION_ID)17:
			if (isStunnedLoop)
			{
				UpdateStunnedEffect();
			}
			break;
		case (ACTION_ID)23:
			if (!isStopCounter)
			{
				if (IsPrayed() && deadStopTime > 0f)
				{
					float num = Time.get_deltaTime() * ((!IsBoostByType(BOOST_PRAY_TYPE.GUARD_ONE_HAND_SWORD_NORMAL)) ? 1f : playerParameter.ohsActionInfo.Normal_PrayBoostRate);
					switch (prayerIds.Count)
					{
					case 2:
						num *= 1.2f;
						break;
					case 3:
						num *= 1.4f;
						break;
					}
					num *= ((!IsBoostByType(BOOST_PRAY_TYPE.IN_BARRIER)) ? 1f : playerParameter.rescueSpeedRateInBarrier);
					prayerTime += num;
				}
				else if (!IsPrayed())
				{
					prayerTime -= Time.get_deltaTime();
					if (prayerTime < 0f)
					{
						prayerTime = 0f;
					}
				}
			}
			if (rescueTime <= 0f && deadStartTime >= 0f)
			{
				UpdateRevivalRangeEffect();
				if ((MonoBehaviourSingleton<InGameManager>.I.IsRush() || QuestManager.IsValidInGameWaveMatch(true)) && continueTime > 0f)
				{
					continueTime = 0f;
					OnEndContinueTimeEnd();
				}
				else if (continueTime > 0f && !isProgressStop())
				{
					continueTime -= Time.get_deltaTime();
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
		case (ACTION_ID)25:
		{
			bool flag = true;
			int i = 0;
			for (int count = prayTargetIds.Count; i < count; i++)
			{
				Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(prayTargetIds[i]) as Player;
				if (player != null && player.isDead)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				ActIdle(false, -1f);
			}
			break;
		}
		case (ACTION_ID)26:
			if (isChangingWeapon && !base.isLoading && changeWeaponStartTime >= 0f && Time.get_time() - changeWeaponStartTime >= playerParameter.changeWeaponMinTime)
			{
				ActIdle(false, -1f);
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
				PlayMotion(125, -1f);
				OnEndContinueTimeEnd();
			}
			else
			{
				ActDeadLoop(false, 0f, 0f);
			}
			return;
		case (ACTION_ID)21:
			if (isSkillCastState)
			{
				isSkillCastState = false;
				SkillInfo.SkillParam actSkillParam = skillInfo.actSkillParam;
				PlayMotion(actSkillParam.tableData.actStateName, -1f);
				return;
			}
			break;
		}
		base.OnPlayingEndMotion();
	}

	protected override void EndAction()
	{
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Expected O, but got Unknown
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0603: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Unknown result type (might be due to invalid IL or missing references)
		//IL_060e: Unknown result type (might be due to invalid IL or missing references)
		if (base.isInitialized)
		{
			ACTION_ID actionID = base.actionID;
			MOVE_TYPE moveType = base.moveType;
			bool isPlayingEndMotion = base.isPlayingEndMotion;
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
			case (ACTION_ID)13:
				stumbleEndTime = 0f;
				break;
			case (ACTION_ID)14:
				shakeEndTime = 0f;
				break;
			case (ACTION_ID)15:
			case (ACTION_ID)16:
			case (ACTION_ID)17:
				base._rigidbody.set_constraints(base._rigidbody.get_constraints() | 4);
				ResetIgnoreColliders();
				if (actionID == (ACTION_ID)17)
				{
					isStunnedLoop = false;
					stunnedEndTime = 0f;
					stunnedTime = 0f;
					stunnedReduceEnableTime = 0f;
					UpdateStunnedEffect();
				}
				break;
			case (ACTION_ID)27:
				if (!isAppliedGather && (IsCoopNone() || IsOriginal()))
				{
					ApplyGather();
				}
				break;
			case (ACTION_ID)21:
				if (skillRangeEffect != null)
				{
					EffectManager.ReleaseEffect(skillRangeEffect.get_gameObject(), true, false);
					skillRangeEffect = null;
				}
				isUsingSecondGradeSkill = false;
				skillInfo.ResetSecondGradeFlags();
				break;
			case (ACTION_ID)22:
				if (uiPlayerStatusGizmo != null)
				{
					uiPlayerStatusGizmo.SetVisible(true);
				}
				SetHitOffTimer(HIT_OFF_FLAG.BATTLE_START, playerParameter.battleStartHitOffTime);
				break;
			case (ACTION_ID)23:
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
					EffectManager.ReleaseEffect(revivalRangEffect, true, false);
					revivalRangEffect = null;
				}
				break;
			case (ACTION_ID)24:
				SetHitOffTimer(HIT_OFF_FLAG.DEAD_STANDUP, playerParameter.deadStandupHitOffTime);
				break;
			case (ACTION_ID)29:
				PostRestraint();
				break;
			}
			EndWaitingPacket(WAITING_PACKET.PLAYER_CHARGE_RELEASE);
			EndWaitingPacket(WAITING_PACKET.PLAYER_APPLY_CHANGE_WEAPON);
			if (loader.shadow != null && !loader.shadow.get_gameObject().get_activeSelf())
			{
				loader.shadow.get_gameObject().SetActive(true);
			}
			if (!isArrowAimKeep)
			{
				if (isArrowAimBossMode)
				{
					SetArrowAimBossMode(false);
				}
				if (isArrowAimLesserMode)
				{
					SetArrowAimLesserMode(false);
				}
			}
			isArrowAimKeep = false;
			isArrowAimEnd = false;
			CancelCannonMode();
			skillInfo.skillIndex = -1;
			enableInputCombo = false;
			controllerInputCombo = false;
			inputComboID = -1;
			enableComboTrans = false;
			inputComboFlag = false;
			enableInputCharge = false;
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
			enableCounterAttack = false;
			enableSpAttackContinue = false;
			enableAnimSeedRate = false;
			prayerTime = 0f;
			isGuardWalk = false;
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
			if (jumpState != 0)
			{
				base.body.get_transform().set_localPosition(Vector3.get_zero());
			}
			jumpFallBodyPosition = Vector3.get_zero();
			jumpRandingVector = Vector3.get_zero();
			jumpRaindngBasePos = Vector3.get_zero();
			jumpRandingBaseBodyY = 0f;
			jumpState = eJumpState.None;
			ReleaseEffect(ref twoHandSwordsChargeMaxEffect, true);
			evolveSpecialActionSec = 0f;
			if (!notEndGuardFlag)
			{
				_EndGuard();
			}
			ReleaseEffect(ref exRushChargeEffect, true);
			isAbsorbDamageSuperArmor = false;
			SetFlickDirection(SelfController.FLICK_DIRECTION.NONE);
			snatchCtrl.Cancel();
			int i = 0;
			for (int count = m_weaponCtrlList.Count; i < count; i++)
			{
				m_weaponCtrlList[i].OnEndAction();
			}
			OnGatherGimmickEnd();
			if (cancelInvincible != null)
			{
				this.StopCoroutine(cancelInvincible);
				cancelInvincible = null;
			}
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
			Character.stateNameBuilder.Append((!(_layerName == string.Empty)) ? _layerName : "Base Layer.");
			Character.stateNameBuilder.Append(subMotionStateName[motion_id - 115]);
			return Character.stateNameBuilder.ToString();
		}
		return base.GetMotionStateName(motion_id, _layerName);
	}

	protected override int _GetCachedHash(int motion_id)
	{
		if (motion_id < 115 || motion_id >= 139)
		{
			return base._GetCachedHash(motion_id);
		}
		return subMotionHashCaches[motion_id - 115];
	}

	protected override void _CacheHash(int motion_id, int hash)
	{
		if (motion_id < 115 || motion_id >= 139)
		{
			base._CacheHash(motion_id, hash);
		}
		else
		{
			subMotionHashCaches[motion_id - 115] = hash;
		}
	}

	public virtual void Load(PlayerLoadInfo load_info, PlayerLoader.OnCompleteLoad callback = null)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (_physics != null)
		{
			Object.Destroy(_physics.get_gameObject());
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
		loader.StartLoad(load_info, 8, num, true, true, true, true, true, false, true, IsDiviedLoadAndInstantiate(), ShaderGlobal.GetCharacterShaderType(), callback, true, -1);
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
			TwoHandSwordController.InitParam initParam = new TwoHandSwordController.InitParam();
			initParam.Owner = this;
			initParam.BurstInitParam = new TwoHandSwordBurstController.InitParam
			{
				Owner = this,
				ActionInfo = playerParameter.twoHandSwordActionInfo,
				MaxBulletCount = num,
				CurrentRestBullets = thsCtrl.GetAllCurrentRestBulletCount
			};
			TwoHandSwordController.InitParam param = initParam;
			thsCtrl.InitAppend(param);
		}
	}

	public void LoadWeapon(CharaInfo.EquipItem item, int weapon_index, PlayerLoader.OnCompleteLoad callback = null)
	{
		if (item != null && !(loader == null) && loader.loadInfo != null)
		{
			SetNowWeapon(item, weapon_index);
			InitParameter();
			int sex = 0;
			if (createInfo != null && createInfo.charaInfo != null)
			{
				sex = createInfo.charaInfo.sex;
			}
			loader.loadInfo.SetEquipWeapon(sex, (uint)item.eId);
			Load(loader.loadInfo, callback);
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
			break;
		case BuffParam.BUFFTYPE.ATTACK_NORMAL:
		{
			List<int> atkList4;
			List<int> list27 = atkList4 = passive.atkList;
			int index;
			int index28 = index = 0;
			index = atkList4[index];
			list27[index28] = index + value;
			break;
		}
		case BuffParam.BUFFTYPE.ATTACK_FIRE:
		{
			List<int> atkList3;
			List<int> list18 = atkList3 = passive.atkList;
			int index;
			int index19 = index = 1;
			index = atkList3[index];
			list18[index19] = index + value;
			break;
		}
		case BuffParam.BUFFTYPE.ATTACK_WATER:
		{
			List<int> atkList2;
			List<int> list10 = atkList2 = passive.atkList;
			int index;
			int index11 = index = 2;
			index = atkList2[index];
			list10[index11] = index + value;
			break;
		}
		case BuffParam.BUFFTYPE.ATTACK_THUNDER:
		{
			List<int> atkList;
			List<int> list2 = atkList = passive.atkList;
			int index;
			int index3 = index = 3;
			index = atkList[index];
			list2[index3] = index + value;
			break;
		}
		case BuffParam.BUFFTYPE.ATTACK_SOIL:
		{
			List<int> atkList14;
			List<int> list44 = atkList14 = passive.atkList;
			int index;
			int index45 = index = 4;
			index = atkList14[index];
			list44[index45] = index + value;
			break;
		}
		case BuffParam.BUFFTYPE.ATTACK_LIGHT:
		{
			List<int> atkList13;
			List<int> list43 = atkList13 = passive.atkList;
			int index;
			int index44 = index = 5;
			index = atkList13[index];
			list43[index44] = index + value;
			break;
		}
		case BuffParam.BUFFTYPE.ATTACK_DARK:
		{
			List<int> atkList12;
			List<int> list42 = atkList12 = passive.atkList;
			int index;
			int index43 = index = 6;
			index = atkList12[index];
			list42[index43] = index + value;
			break;
		}
		case BuffParam.BUFFTYPE.ATTACK_ALLELEMENT:
			passive.atkAllElement += (float)value;
			break;
		case BuffParam.BUFFTYPE.ATTACK_DOWN_NORMAL:
		{
			List<int> atkList11;
			List<int> list41 = atkList11 = passive.atkList;
			int index;
			int index42 = index = 0;
			index = atkList11[index];
			list41[index42] = index - value;
			break;
		}
		case BuffParam.BUFFTYPE.ATTACK_DOWN_FIRE:
		{
			List<int> atkList10;
			List<int> list40 = atkList10 = passive.atkList;
			int index;
			int index41 = index = 1;
			index = atkList10[index];
			list40[index41] = index - value;
			break;
		}
		case BuffParam.BUFFTYPE.ATTACK_DOWN_WATER:
		{
			List<int> atkList9;
			List<int> list39 = atkList9 = passive.atkList;
			int index;
			int index40 = index = 2;
			index = atkList9[index];
			list39[index40] = index - value;
			break;
		}
		case BuffParam.BUFFTYPE.ATTACK_DOWN_THUNDER:
		{
			List<int> atkList8;
			List<int> list38 = atkList8 = passive.atkList;
			int index;
			int index39 = index = 3;
			index = atkList8[index];
			list38[index39] = index - value;
			break;
		}
		case BuffParam.BUFFTYPE.ATTACK_DOWN_SOIL:
		{
			List<int> atkList7;
			List<int> list37 = atkList7 = passive.atkList;
			int index;
			int index38 = index = 4;
			index = atkList7[index];
			list37[index38] = index - value;
			break;
		}
		case BuffParam.BUFFTYPE.ATTACK_DOWN_LIGHT:
		{
			List<int> atkList6;
			List<int> list36 = atkList6 = passive.atkList;
			int index;
			int index37 = index = 5;
			index = atkList6[index];
			list36[index37] = index - value;
			break;
		}
		case BuffParam.BUFFTYPE.ATTACK_DOWN_DARK:
		{
			List<int> atkList5;
			List<int> list35 = atkList5 = passive.atkList;
			int index;
			int index36 = index = 6;
			index = atkList5[index];
			list35[index36] = index - value;
			break;
		}
		case BuffParam.BUFFTYPE.ATTACK_DOWN_ALLELEMENT:
			passive.atkAllElement -= (float)value;
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
		{
			List<int> defList16;
			List<int> list34 = defList16 = passive.defList;
			int index;
			int index35 = index = 0;
			index = defList16[index];
			list34[index35] = index + value;
			break;
		}
		case BuffParam.BUFFTYPE.DEFENCE_FIRE:
		{
			List<int> defList15;
			List<int> list33 = defList15 = passive.defList;
			int index;
			int index34 = index = 1;
			index = defList15[index];
			list33[index34] = index + value;
			break;
		}
		case BuffParam.BUFFTYPE.DEFENCE_WATER:
		{
			List<int> defList14;
			List<int> list32 = defList14 = passive.defList;
			int index;
			int index33 = index = 2;
			index = defList14[index];
			list32[index33] = index + value;
			break;
		}
		case BuffParam.BUFFTYPE.DEFENCE_THUNDER:
		{
			List<int> defList13;
			List<int> list31 = defList13 = passive.defList;
			int index;
			int index32 = index = 3;
			index = defList13[index];
			list31[index32] = index + value;
			break;
		}
		case BuffParam.BUFFTYPE.DEFENCE_SOIL:
		{
			List<int> defList12;
			List<int> list30 = defList12 = passive.defList;
			int index;
			int index31 = index = 4;
			index = defList12[index];
			list30[index31] = index + value;
			break;
		}
		case BuffParam.BUFFTYPE.DEFENCE_LIGHT:
		{
			List<int> defList11;
			List<int> list29 = defList11 = passive.defList;
			int index;
			int index30 = index = 5;
			index = defList11[index];
			list29[index30] = index + value;
			break;
		}
		case BuffParam.BUFFTYPE.DEFENCE_DARK:
		{
			List<int> defList10;
			List<int> list28 = defList10 = passive.defList;
			int index;
			int index29 = index = 6;
			index = defList10[index];
			list28[index29] = index + value;
			break;
		}
		case BuffParam.BUFFTYPE.DEFENCE_ALLELEMENT:
			for (int l = 1; l < 7; l++)
			{
				List<int> defList9;
				List<int> list26 = defList9 = passive.defList;
				int index;
				int index27 = index = l;
				index = defList9[index];
				list26[index27] = index + value;
			}
			break;
		case BuffParam.BUFFTYPE.DEFENCE_DOWN_NORMAL:
		{
			List<int> defList8;
			List<int> list25 = defList8 = passive.defList;
			int index;
			int index26 = index = 0;
			index = defList8[index];
			list25[index26] = index - value;
			break;
		}
		case BuffParam.BUFFTYPE.DEFENCE_DOWN_FIRE:
		{
			List<int> defList7;
			List<int> list24 = defList7 = passive.defList;
			int index;
			int index25 = index = 1;
			index = defList7[index];
			list24[index25] = index - value;
			break;
		}
		case BuffParam.BUFFTYPE.DEFENCE_DOWN_WATER:
		{
			List<int> defList6;
			List<int> list23 = defList6 = passive.defList;
			int index;
			int index24 = index = 2;
			index = defList6[index];
			list23[index24] = index - value;
			break;
		}
		case BuffParam.BUFFTYPE.DEFENCE_DOWN_THUNDER:
		{
			List<int> defList5;
			List<int> list22 = defList5 = passive.defList;
			int index;
			int index23 = index = 3;
			index = defList5[index];
			list22[index23] = index - value;
			break;
		}
		case BuffParam.BUFFTYPE.DEFENCE_DOWN_SOIL:
		{
			List<int> defList4;
			List<int> list21 = defList4 = passive.defList;
			int index;
			int index22 = index = 4;
			index = defList4[index];
			list21[index22] = index - value;
			break;
		}
		case BuffParam.BUFFTYPE.DEFENCE_DOWN_LIGHT:
		{
			List<int> defList3;
			List<int> list20 = defList3 = passive.defList;
			int index;
			int index21 = index = 5;
			index = defList3[index];
			list20[index21] = index - value;
			break;
		}
		case BuffParam.BUFFTYPE.DEFENCE_DOWN_DARK:
		{
			List<int> defList2;
			List<int> list19 = defList2 = passive.defList;
			int index;
			int index20 = index = 6;
			index = defList2[index];
			list19[index20] = index - value;
			break;
		}
		case BuffParam.BUFFTYPE.DEFENCE_DOWN_ALLELEMENT:
			for (int k = 1; k < 7; k++)
			{
				List<int> defList;
				List<int> list17 = defList = passive.defList;
				int index;
				int index18 = index = k;
				index = defList[index];
				list17[index18] = index - value;
			}
			break;
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
			base.atkBadStatus.paralyze += (float)value;
			break;
		case BuffParam.BUFFTYPE.ATTACK_POISON:
			base.atkBadStatus.poison += (float)value;
			break;
		case BuffParam.BUFFTYPE.ATTACK_FREEZE:
			base.atkBadStatus.freeze += (float)value;
			break;
		case BuffParam.BUFFTYPE.BAD_STATUS_DOWN_RATE_UP:
			passive.badStatusRateUp[2] += (float)value * 0.01f;
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
		{
			List<int> tolList14;
			List<int> list16 = tolList14 = passive.tolList;
			int index;
			int index17 = index = 0;
			index = tolList14[index];
			list16[index17] = index + value;
			break;
		}
		case BuffParam.BUFFTYPE.TOLERANCE_WATER:
		{
			List<int> tolList13;
			List<int> list15 = tolList13 = passive.tolList;
			int index;
			int index16 = index = 1;
			index = tolList13[index];
			list15[index16] = index + value;
			break;
		}
		case BuffParam.BUFFTYPE.TOLERANCE_THUNDER:
		{
			List<int> tolList12;
			List<int> list14 = tolList12 = passive.tolList;
			int index;
			int index15 = index = 2;
			index = tolList12[index];
			list14[index15] = index + value;
			break;
		}
		case BuffParam.BUFFTYPE.TOLERANCE_SOIL:
		{
			List<int> tolList11;
			List<int> list13 = tolList11 = passive.tolList;
			int index;
			int index14 = index = 3;
			index = tolList11[index];
			list13[index14] = index + value;
			break;
		}
		case BuffParam.BUFFTYPE.TOLERANCE_LIGHT:
		{
			List<int> tolList10;
			List<int> list12 = tolList10 = passive.tolList;
			int index;
			int index13 = index = 4;
			index = tolList10[index];
			list12[index13] = index + value;
			break;
		}
		case BuffParam.BUFFTYPE.TOLERANCE_DARK:
		{
			List<int> tolList9;
			List<int> list11 = tolList9 = passive.tolList;
			int index;
			int index12 = index = 5;
			index = tolList9[index];
			list11[index12] = index + value;
			break;
		}
		case BuffParam.BUFFTYPE.TOLERANCE_ALLELEMENT:
			for (int j = 0; j < 6; j++)
			{
				List<int> tolList8;
				List<int> list9 = tolList8 = passive.tolList;
				int index;
				int index10 = index = j;
				index = tolList8[index];
				list9[index10] = index + value;
			}
			break;
		case BuffParam.BUFFTYPE.TOLERANCE_DOWN_FIRE:
		{
			List<int> tolList7;
			List<int> list8 = tolList7 = passive.tolList;
			int index;
			int index9 = index = 0;
			index = tolList7[index];
			list8[index9] = index - value;
			break;
		}
		case BuffParam.BUFFTYPE.TOLERANCE_DOWN_WATER:
		{
			List<int> tolList6;
			List<int> list7 = tolList6 = passive.tolList;
			int index;
			int index8 = index = 1;
			index = tolList6[index];
			list7[index8] = index - value;
			break;
		}
		case BuffParam.BUFFTYPE.TOLERANCE_DOWN_THUNDER:
		{
			List<int> tolList5;
			List<int> list6 = tolList5 = passive.tolList;
			int index;
			int index7 = index = 2;
			index = tolList5[index];
			list6[index7] = index - value;
			break;
		}
		case BuffParam.BUFFTYPE.TOLERANCE_DOWN_SOIL:
		{
			List<int> tolList4;
			List<int> list5 = tolList4 = passive.tolList;
			int index;
			int index6 = index = 3;
			index = tolList4[index];
			list5[index6] = index - value;
			break;
		}
		case BuffParam.BUFFTYPE.TOLERANCE_DOWN_LIGHT:
		{
			List<int> tolList3;
			List<int> list4 = tolList3 = passive.tolList;
			int index;
			int index5 = index = 4;
			index = tolList3[index];
			list4[index5] = index - value;
			break;
		}
		case BuffParam.BUFFTYPE.TOLERANCE_DOWN_DARK:
		{
			List<int> tolList2;
			List<int> list3 = tolList2 = passive.tolList;
			int index;
			int index4 = index = 5;
			index = tolList2[index];
			list3[index4] = index - value;
			break;
		}
		case BuffParam.BUFFTYPE.TOLERANCE_DOWN_ALLELEMENT:
			for (int i = 0; i < 6; i++)
			{
				List<int> tolList;
				List<int> list = tolList = passive.tolList;
				int index;
				int index2 = index = i;
				index = tolList[index];
				list[index2] = index - value;
			}
			break;
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

	public virtual void OnSetPlayerStatus(int _level, int _atk, int _def, int _hp, bool send_packet = true, StageObjectManager.PlayerTransferInfo transfer_info = null)
	{
		playerAtk = (float)_atk;
		playerDef = (float)_def;
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
		}
		else
		{
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
			abilityData.ids.Clear();
			abilityData.APs.Clear();
			abilityItem.Clear();
			hpUp = 0;
			List<int> list = null;
			if (create_info.extentionInfo != null)
			{
				list = create_info.extentionInfo.weaponIndexList;
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
				if (SetEqState(equipItem, false))
				{
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
				SetNowWeapon(equipItem2, index);
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
							if (equipItemData2 != null)
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
				if (transfer_info.spActionGauges != null && transfer_info.spActionGauges.Length > 0)
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
					TwoHandSwordController.InitParam initParam = new TwoHandSwordController.InitParam();
					initParam.Owner = this;
					initParam.BurstInitParam = new TwoHandSwordBurstController.InitParam
					{
						Owner = this,
						ActionInfo = playerParameter.twoHandSwordActionInfo,
						MaxBulletCount = transfer_info.maxBulletCount,
						CurrentRestBullets = transfer_info.burstCurrentRestBulletCount,
						IsNeedFullBullet = false
					};
					TwoHandSwordController.InitParam param = initParam;
					thsCtrl.InitAppend(param);
				}
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
		playerTransferInfo.buffSyncParam = buffParam.CreateSyncParam(BuffParam.BUFFTYPE.NONE);
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
		if (spActionGauge != null && spActionGauge.Length > 0)
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
			List<int> list2;
			List<int> list3 = list2 = list;
			int index;
			int index2 = index = 0;
			index = list2[index];
			list3[index2] = index + (int)exceedParam.atk;
			for (int k = 0; k < 6; k++)
			{
				List<int> list4;
				List<int> list5 = list4 = list;
				int index3 = index = k + 1;
				index = list4[index];
				list5[index3] = index + exceedParam.atkElement[k];
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
		List<int> list6 = new List<int>();
		if (growEquipItemData != null)
		{
			list6.Add(growEquipItemData.GetGrowParamDef(equipItemData.baseDef));
			int[] growParamElemDef = growEquipItemData.GetGrowParamElemDef(equipItemData.defElement);
			for (int m = 0; m < 6; m++)
			{
				int num3 = growParamElemDef[m];
				if (equipItemData.isFormer)
				{
					num3 *= 10;
				}
				list6.Add(num3);
			}
		}
		else
		{
			list6.Add(equipItemData.baseDef);
			for (int n = 0; n < 6; n++)
			{
				int num4 = equipItemData.defElement[n];
				if (equipItemData.isFormer)
				{
					num4 *= 10;
				}
				list6.Add(num4);
			}
		}
		int num5 = 0;
		if (exceedParam != null)
		{
			List<int> list7;
			List<int> list8 = list7 = list6;
			int index;
			int index4 = index = 0;
			index = list7[index];
			list8[index4] = index + (int)exceedParam.def;
			for (int num6 = 0; num6 < 6; num6++)
			{
				List<int> list9;
				List<int> list10 = list9 = list6;
				int index5 = index = num6 + 1;
				index = list9[index];
				list10[index5] = index + exceedParam.defElement[num6];
			}
			num5 += (int)exceedParam.hp;
		}
		if (is_weapon_set && flag)
		{
			weaponState.atkList = list;
			weaponState.defList = list6;
			int num7 = growEquipItemData?.GetGrowParamHp(equipItemData.baseHp) ?? ((int)equipItemData.baseHp);
			weaponState.hp = num7 + num5;
		}
		else
		{
			if (!flag)
			{
				for (int num8 = 0; num8 < 7; num8++)
				{
					List<int> atkList;
					List<int> list11 = atkList = baseState.atkList;
					int index;
					int index6 = index = num8;
					index = atkList[index];
					list11[index6] = index + list[num8];
					List<int> defList;
					List<int> list12 = defList = baseState.defList;
					int index7 = index = num8;
					index = defList[index];
					list12[index7] = index + list6[num8];
					List<int> list13;
					List<int> list14 = list13 = guardEquipDef;
					int index8 = index = num8;
					index = list13[index];
					list14[index8] = index + list6[num8];
				}
				int num9 = growEquipItemData?.GetGrowParamHp(equipItemData.baseHp) ?? ((int)equipItemData.baseHp);
				num9 += num5;
				hpUp += num9;
			}
			int num10 = 0;
			for (int count = item.sIds.Count; num10 < count; num10++)
			{
				SetSkillState(item.sIds[num10], item.sLvs[num10], is_weapon_set, equipItemData.type);
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
						List<int> aPs;
						List<int> list15 = aPs = abilityData.APs;
						int index;
						int index9 = index = num14;
						index = aPs[index];
						list15[index9] = index + a_pts[num13];
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
		bool flag = skillInfo.IsActSkillAction(skill_index);
		bool flag2 = IsValidBuffSilence();
		return flag && !flag2;
	}

	private void SetSkillState(int skill_id, int level, bool is_weapon_set, EQUIPMENT_TYPE type)
	{
		if (!MonoBehaviourSingleton<InGameManager>.I.ContainsArenaCondition(ARENA_CONDITION.FORBID_MAGI_INCARNATION) || (skill_id != 404500100 && skill_id != 404500200 && skill_id != 404500300 && skill_id != 404500400))
		{
			SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint)skill_id);
			GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(skillItemData.growID, level);
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
					List<int> atkList;
					List<int> list3 = atkList = skillConstState.atkList;
					int index;
					int index2 = index = k;
					index = atkList[index];
					list3[index2] = index + list[k];
					List<int> defList;
					List<int> list4 = defList = skillConstState.defList;
					int index3 = index = k;
					index = defList[index];
					list4[index3] = index + list2[k];
				}
				int growParamHp = growSkillItemData.GetGrowParamHp(skillItemData.baseHp);
				skillConstState.hp += growParamHp;
			}
			if (skillItemData.IsPassive())
			{
				skillData.ids.Add(skill_id);
				skillData.lvs.Add(level);
			}
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

	public void SetNowWeapon(CharaInfo.EquipItem now_weapon, int index)
	{
		SetEqState(now_weapon, true);
		weaponData = now_weapon;
		weaponIndex = index;
		evolveCtrl.SetWeaponInfo();
	}

	public void SetPassiveParam()
	{
		buffParam.passive.Reset();
		base.atkBadStatus.Reset();
		for (int i = 0; i < 7; i++)
		{
			List<int> atkList;
			List<int> list = atkList = buffParam.passive.atkList;
			int index;
			int index2 = index = i;
			index = atkList[index];
			list[index2] = index + skillConstState.atkList[i];
			List<int> defList;
			List<int> list2 = defList = buffParam.passive.defList;
			int index3 = index = i;
			index = defList[index];
			list2[index3] = index + skillConstState.defList[i];
		}
		buffParam.passive.hp += skillConstState.hp;
		if (attackMode != 0)
		{
			int type_index = (int)(attackMode - 1);
			for (int j = 0; j < skillData.ids.Count; j++)
			{
				SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData((uint)skillData.ids[j]);
				if (skillItemData.IsEnableEquipType(type_index))
				{
					SP_ATTACK_TYPE supportPassiveSpAttackType = skillItemData.supportPassiveSpAttackType;
					GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(skillItemData.growID, skillData.lvs[j]);
					for (int k = 0; k < 3; k++)
					{
						if (skillItemData.IsEnableSupportEquipType(type_index, k) && Utility.IsEnableSpAttackType(supportPassiveSpAttackType, spAttackType))
						{
							SetPassiveBuff(skillItemData.supportType[k], growSkillItemData.GetGrowParamSupprtValue(skillItemData.supportValue, k));
						}
					}
				}
			}
			int currentEventID = Utility.GetCurrentEventID();
			EQUIPMENT_TYPE equipType = ConvertAttackModeToEquipmentType(attackMode);
			int l = 0;
			for (int count = abilityData.ids.Count; l < count; l++)
			{
				buffParam.AddAbility((uint)abilityData.ids[l], abilityData.APs[l], equipType, spAttackType, currentEventID);
			}
			foreach (AbilityItem item in abilityItem)
			{
				buffParam.AddAbilityItemParam(AbilityItemInfo.ConvertAbilityItemToInfo(item).ToArray(), equipType, spAttackType, currentEventID);
			}
			ApplyConditionAbilityValue();
			buffParam.passive.firstInitialized = true;
			buffParam.UpdateConditionsAbility();
		}
	}

	private void ApplyConditionAbilityValue()
	{
		if (abilityCounterAttackNumList != null)
		{
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
	}

	private void ApplyCleaveComboConditionAbilityValue()
	{
		if (abilityCleaveComboNumList != null)
		{
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
	}

	public virtual void InitParameter()
	{
		ResetStatusParam();
		base.attack.normal += (float)weaponState.atkList[0];
		base.attack.fire += (float)weaponState.atkList[1];
		base.attack.water += (float)weaponState.atkList[2];
		base.attack.thunder += (float)weaponState.atkList[3];
		base.attack.soil += (float)weaponState.atkList[4];
		base.attack.light += (float)weaponState.atkList[5];
		base.attack.dark += (float)weaponState.atkList[6];
		base.defense.normal += (float)baseState.defList[0] + playerDef + (float)weaponState.defList[0];
		base.defense.fire += (float)baseState.defList[0] + playerDef + (float)weaponState.defList[0];
		base.defense.water += (float)baseState.defList[0] + playerDef + (float)weaponState.defList[0];
		base.defense.thunder += (float)baseState.defList[0] + playerDef + (float)weaponState.defList[0];
		base.defense.soil += (float)baseState.defList[0] + playerDef + (float)weaponState.defList[0];
		base.defense.light += (float)baseState.defList[0] + playerDef + (float)weaponState.defList[0];
		base.defense.dark += (float)baseState.defList[0] + playerDef + (float)weaponState.defList[0];
		AtkAttribute tolerance = base.tolerance;
		tolerance.normal = tolerance.normal;
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
		base.defense.normal += (float)buffParam.passive.defList[0];
		base.defense.fire += (float)(buffParam.passive.defList[1] + buffParam.passive.defList[0]);
		base.defense.water += (float)(buffParam.passive.defList[2] + buffParam.passive.defList[0]);
		base.defense.thunder += (float)(buffParam.passive.defList[3] + buffParam.passive.defList[0]);
		base.defense.soil += (float)(buffParam.passive.defList[4] + buffParam.passive.defList[0]);
		base.defense.light += (float)(buffParam.passive.defList[5] + buffParam.passive.defList[0]);
		base.defense.dark += (float)(buffParam.passive.defList[6] + buffParam.passive.defList[0]);
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
		return (!(defenseValue > (float)playerDefenseThreshold)) ? 4.5f : (4.5f / (1f + (defenseValue - (float)playerDefenseThreshold) / (float)playerDefenseCoefficient));
	}

	protected void SetAttackMode(ATTACK_MODE attack_mode)
	{
		if (base.isInitialized && base.actionID != (ACTION_ID)26)
		{
			ActIdle(false, -1f);
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
			AttackInfo[] array = Utility.CreateMergedArray(attackInfos, playerParameter.weaponAttackInfoList[(int)(attack_mode - 1)].attackHitInfos);
			attackInfos = Utility.DistinctArray(array);
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
		return base.OnBuffStart(buffData);
	}

	public override void OnBuffRoutine(BuffParam.BuffData buffData, bool packet = false)
	{
		base.OnBuffRoutine(buffData, packet);
		BuffParam.BUFFTYPE type = buffData.type;
		if ((type == BuffParam.BUFFTYPE.REGENERATE || type == BuffParam.BUFFTYPE.REGENERATE_PROPORTION) && base.hp > healHp)
		{
			healHp = base.hp;
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
			buffData.interval = Time.get_deltaTime();
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

	public override void OnBuffCancellation()
	{
		if (!IsInBarrier())
		{
			bool flag = false;
			List<BuffParam.BUFFTYPE> ignoreBuffCancellation = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.ignoreBuffCancellation;
			int i = 0;
			for (int num = 192; i < num; i++)
			{
				BuffParam.BUFFTYPE bUFFTYPE = (BuffParam.BUFFTYPE)i;
				if (!ignoreBuffCancellation.Contains(bUFFTYPE) && OnBuffEnd(bUFFTYPE, false, true))
				{
					flag = true;
				}
			}
			if (flag)
			{
				SendBuffSync(BuffParam.BUFFTYPE.NONE);
			}
		}
	}

	public override bool IsValidBuff(BuffParam.BUFFTYPE targetType)
	{
		if (object.ReferenceEquals(buffParam, null))
		{
			return false;
		}
		return buffParam.GetValue(targetType, true) > 0;
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
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		if (hit_param.toObject is Enemy)
		{
			Enemy enemy = hit_param.toObject as Enemy;
			if (enemy.regionInfos.Length > hit_param.regionID && !enemy.regionInfos[hit_param.regionID].isAtkColliderHit)
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
		case AttackHitInfo.ATTACK_TYPE.HEAL_ATTACK:
		{
			Enemy enemy3 = hit_param.toObject as Enemy;
			if (!(enemy3 == null))
			{
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
				thsCtrl.Set3rdComboAttackHitFlag(true);
			}
			break;
		case AttackHitInfo.ATTACK_TYPE.FROM_AVOID:
			if (thsCtrl != null)
			{
				thsCtrl.SetIsHitAvoidAttack(true);
			}
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
		if (to_object is Enemy)
		{
			Enemy enemy = to_object as Enemy;
			SetHitStop(status.attackInfo.toEnemy.hitStopTime);
			enemy.SetHitStop(status.attackInfo.toEnemy.enemyHitStopTime);
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
				SetNextTrigger(0);
			}
		}
	}

	protected override void OnPlayAttackedHitEffect(AttackedHitStatusDirection status)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		if (IsValidAttackedHit(status.fromObject))
		{
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
		if (!status.attackInfo.isSkillReference)
		{
			List<BuffParam.BuffData> hitAbsorbBuffDataList = buffParam.GetHitAbsorbBuffDataList();
			if (!hitAbsorbBuffDataList.IsNullOrEmpty() && !IsValidBuff(BuffParam.BUFFTYPE.SHIELD))
			{
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
						num += CalcAbsorbValueByBuff((float)status.damage, hitAbsorbBuffDataList[i]);
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
		AtkAttribute atkAttribute = new AtkAttribute();
		for (int i = 0; i < absorbBuffDataList.Count; i++)
		{
			switch (absorbBuffDataList[i].type)
			{
			case BuffParam.BUFFTYPE.ABSORB_NORMAL:
				atkAttribute.normal = CalcAbsorbValueByBuff(status.damageDetails.normal, absorbBuffDataList[i]);
				break;
			case BuffParam.BUFFTYPE.ABSORB_FIRE:
				atkAttribute.fire = CalcAbsorbValueByBuff(status.damageDetails.fire, absorbBuffDataList[i]);
				break;
			case BuffParam.BUFFTYPE.ABSORB_WATER:
				atkAttribute.water = CalcAbsorbValueByBuff(status.damageDetails.water, absorbBuffDataList[i]);
				break;
			case BuffParam.BUFFTYPE.ABSORB_THUNDER:
				atkAttribute.thunder = CalcAbsorbValueByBuff(status.damageDetails.thunder, absorbBuffDataList[i]);
				break;
			case BuffParam.BUFFTYPE.ABSORB_SOIL:
				atkAttribute.soil = CalcAbsorbValueByBuff(status.damageDetails.soil, absorbBuffDataList[i]);
				break;
			case BuffParam.BUFFTYPE.ABSORB_LIGHT:
				atkAttribute.light = CalcAbsorbValueByBuff(status.damageDetails.light, absorbBuffDataList[i]);
				break;
			case BuffParam.BUFFTYPE.ABSORB_DARK:
				atkAttribute.dark = CalcAbsorbValueByBuff(status.damageDetails.dark, absorbBuffDataList[i]);
				break;
			}
		}
		status.damageDetails.Sub(atkAttribute);
		status.damage = Mathf.FloorToInt(status.damageDetails.CalcTotal());
		if (status.damage < 0)
		{
			status.damage = 0;
		}
		if (atkAttribute.CalcTotal() > 0f)
		{
			isAbsorbDamageSuperArmor = true;
		}
		int num = Mathf.FloorToInt(atkAttribute.CalcTotal());
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid() && num > MonoBehaviourSingleton<InGameSettingsManager>.I.buff.absorbDamageParam.limitPlayerAbsorbDamage)
		{
			num = MonoBehaviourSingleton<InGameSettingsManager>.I.buff.absorbDamageParam.limitPlayerAbsorbDamage;
		}
		if (num <= 0)
		{
			return false;
		}
		HealData healData = new HealData(num, HEAL_TYPE.NONE, HEAL_EFFECT_TYPE.HIT_ABSORB, new List<int>
		{
			10
		});
		OnHealReceive(healData);
		return true;
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
			result = ((!(damage < (float)absorbBuff.value)) ? ((float)absorbBuff.value) : damage);
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
		int value = buffParam.GetValue(BuffParam.BUFFTYPE.SKILL_CHARGE_WHEN_DAMAGED, true);
		if (value <= 0)
		{
			return false;
		}
		OnChargeSkillGaugeReceive(BuffParam.BUFFTYPE.SKILL_CHARGE_WHEN_DAMAGED, value, -1);
		return true;
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
		damage_details.normal = (float)(int)(atkAttribute.normal * InGameUtility.CalcDamageRateToPlayer(levelRate, atkAttribute3.normal, 0f, -1, 1f));
		damage_details.fire = (float)(int)(atkAttribute.fire * InGameUtility.CalcDamageRateToPlayer(levelRate, atkAttribute3.fire, atkAttribute2.fire, defenseThreshold, defenseCoefficient.fire));
		damage_details.water = (float)(int)(atkAttribute.water * InGameUtility.CalcDamageRateToPlayer(levelRate, atkAttribute3.water, atkAttribute2.water, defenseThreshold, defenseCoefficient.water));
		damage_details.thunder = (float)(int)(atkAttribute.thunder * InGameUtility.CalcDamageRateToPlayer(levelRate, atkAttribute3.thunder, atkAttribute2.thunder, defenseThreshold, defenseCoefficient.thunder));
		damage_details.soil = (float)(int)(atkAttribute.soil * InGameUtility.CalcDamageRateToPlayer(levelRate, atkAttribute3.soil, atkAttribute2.soil, defenseThreshold, defenseCoefficient.soil));
		damage_details.light = (float)(int)(atkAttribute.light * InGameUtility.CalcDamageRateToPlayer(levelRate, atkAttribute3.light, atkAttribute2.light, defenseThreshold, defenseCoefficient.light));
		damage_details.dark = (float)(int)(atkAttribute.dark * InGameUtility.CalcDamageRateToPlayer(levelRate, atkAttribute3.dark, atkAttribute2.dark, defenseThreshold, defenseCoefficient.dark));
		damage_details.CheckMinus();
		Character character = status.fromObject as Character;
		if (character != null)
		{
			damage_details.Mul(character.buffParam.GetAbilityDamageRate(this, status));
		}
		int num = (int)damage_details.CalcTotal();
		if (_IsGuard())
		{
			float num2 = _GetGuardDamageCutRate();
			damage_details.Mul(num2);
			num = (int)((float)num * num2);
			_AddRevengeCounter(num);
		}
		num = (int)((float)num * buffParam.GetDamageDownRate());
		if (num < 1)
		{
			num = 1;
		}
		return num;
	}

	public override void GetAtk(AttackHitInfo info, ref AtkAttribute atk)
	{
		SkillInfo.SkillParam skillParam = skillInfo.actSkillParam;
		if (base.TrackingTargetBullet != null && base.TrackingTargetBullet.IsReplaceSkill)
		{
			skillParam = base.TrackingTargetBullet.SkillParamForBullet;
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
		playerAtkCalcData.buffAtkAllElementConstant = (float)buffParam.GetValue(BuffParam.BUFFTYPE.ATTACK_ALLELEMENT, true);
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
		atkAttribute.normal += (float)baseState.atkList[0];
		atkAttribute.fire += (float)baseState.atkList[1];
		atkAttribute.water += (float)baseState.atkList[2];
		atkAttribute.thunder += (float)baseState.atkList[3];
		atkAttribute.soil += (float)baseState.atkList[4];
		atkAttribute.light += (float)baseState.atkList[5];
		atkAttribute.dark += (float)baseState.atkList[6];
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
		float num = 0f;
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
		CalcDefenceBuffAndPassive(ref baseDefence, true);
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
		baseDefence.fire = (float)(guardEquipDef[1] + weaponState.defList[1]);
		baseDefence.water = (float)(guardEquipDef[2] + weaponState.defList[2]);
		baseDefence.thunder = (float)(guardEquipDef[3] + weaponState.defList[3]);
		baseDefence.soil = (float)(guardEquipDef[4] + weaponState.defList[4]);
		baseDefence.light = (float)(guardEquipDef[5] + weaponState.defList[5]);
		baseDefence.dark = (float)(guardEquipDef[6] + weaponState.defList[6]);
		baseDefence.Div(MonoBehaviourSingleton<GlobalSettingsManager>.I.playerWeaponAttackRate[1]);
		CalcDefenceBuffAndPassive(ref baseDefence, false);
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
		baseDefence.normal += (float)buffParam.passive.defList[0];
		baseDefence.fire += (float)buffParam.passive.defList[1];
		baseDefence.water += (float)buffParam.passive.defList[2];
		baseDefence.thunder += (float)buffParam.passive.defList[3];
		baseDefence.soil += (float)buffParam.passive.defList[4];
		baseDefence.light += (float)buffParam.passive.defList[5];
		baseDefence.dark += (float)buffParam.passive.defList[6];
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
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		ApplyInvicibleCount(status);
		bool flag = ApplyInvicibleBadStatus(status);
		if (_IsGuard() && _CheckJustGuardSec() && spAttackType == SP_ATTACK_TYPE.BURST)
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
		if ((IsCoopNone() || IsOriginal()) && _IsGuard() && _CheckJustGuardSec())
		{
			if (MonoBehaviourSingleton<EffectManager>.IsValid() && spAttackType != SP_ATTACK_TYPE.BURST)
			{
				EffectManager.OneShot("ef_btl_wsk_sword_01_01", _position, _rotation, false);
			}
			if (EnablePlaySound() && spAttackType != SP_ATTACK_TYPE.BURST)
			{
				SoundManager.PlayOneShotSE(10000042, status.hitPos);
			}
			if (spAttackType == SP_ATTACK_TYPE.HEAT)
			{
				float heat_JustGuardSkillHealValue = playerParameter.ohsActionInfo.Heat_JustGuardSkillHealValue;
				skillInfo.AddUseGauge(heat_JustGuardSkillHealValue, true, true);
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
		RestraintInfo restraintInfo = attackInfo.restraintInfo;
		if (restraintInfo.enable && flag2 && base.actionID != (ACTION_ID)29 && !flag3)
		{
			ActRestraint(attackInfo.restraintInfo);
			status.reactionType = 0;
		}
		if (IsValidShield())
		{
			ShieldDamageData shieldDamageData = CalcShieldDamage(status.damage);
			status.damage = shieldDamageData.hpDamage;
			status.shieldDamage = shieldDamageData.shieldDamage;
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
	}

	protected override bool IsNarrowEscape(AttackedHitStatusOwner status)
	{
		if (timeWhenJustGuardChecked == Time.get_time())
		{
			return true;
		}
		if (status.afterHP > 0)
		{
			return false;
		}
		if (CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.BURST) && _IsGuard() && _CheckJustGuardSec())
		{
			timeWhenJustGuardChecked = Time.get_time();
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

	protected override void UseNarrowEscape(AttackedHitStatusOwner status)
	{
		if (timeWhenJustGuardChecked != Time.get_time() && buffParam.IsNarrowEscape())
		{
			buffParam.UseNarrowEscape();
		}
	}

	protected override bool IsHitReactionValid(AttackedHitStatusOwner status)
	{
		if (status.fromType == OBJECT_TYPE.PLAYER && !playerParameter.playerHitReactionValid)
		{
			return false;
		}
		return base.IsHitReactionValid(status) && status.validDamage;
	}

	protected override REACTION_TYPE OnHitReaction(AttackedHitStatusOwner status)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		if (_IsGuard())
		{
			Vector3 fromPos = status.fromPos;
			Vector3 position = _position;
			fromPos.y = position.y;
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
		}
		if (flag)
		{
			Vector3 fromPos2 = status.fromPos;
			Vector3 position2 = _position;
			fromPos2.y = position2.y;
			_LookAt(fromPos2);
		}
		if (flag2)
		{
			Vector3 val = -_forward;
			val = Quaternion.AngleAxis(status.attackInfo.toPlayer.reactionBlowAngle, _right) * val;
			val = (status.blowForce = val * status.attackInfo.toPlayer.reactionBlowForce);
		}
		if (rEACTION_TYPE != 0)
		{
			return rEACTION_TYPE;
		}
		return REACTION_TYPE.NONE;
	}

	private bool IsValidSuperArmor()
	{
		if (IsRestraint() || IsParalyze())
		{
			return false;
		}
		return enableSuperArmor || IsValidBuff(BuffParam.BUFFTYPE.SUPER_ARMOR) || IsValidBuff(BuffParam.BUFFTYPE.SHIELD_SUPER_ARMOR) || IsValidBuff(BuffParam.BUFFTYPE.INVINCIBLECOUNT) || isAbsorbDamageSuperArmor;
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
			pairSwordsCtrl.DecreaseSoulGaugeByDamage();
		}
	}

	public override void ActReaction(ReactionInfo info, bool isSync = false)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		base.ActReaction(info, isSync);
		switch (info.reactionType)
		{
		case REACTION_TYPE.DOWN:
		case REACTION_TYPE.DEAD:
			break;
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
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_0576: Unknown result type (might be due to invalid IL or missing references)
		//IL_0587: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0591: Unknown result type (might be due to invalid IL or missing references)
		//IL_0594: Unknown result type (might be due to invalid IL or missing references)
		//IL_0596: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ffe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1014: Unknown result type (might be due to invalid IL or missing references)
		//IL_1019: Unknown result type (might be due to invalid IL or missing references)
		//IL_1023: Unknown result type (might be due to invalid IL or missing references)
		//IL_1028: Unknown result type (might be due to invalid IL or missing references)
		//IL_1044: Unknown result type (might be due to invalid IL or missing references)
		//IL_1049: Unknown result type (might be due to invalid IL or missing references)
		//IL_104b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1050: Unknown result type (might be due to invalid IL or missing references)
		//IL_1054: Unknown result type (might be due to invalid IL or missing references)
		//IL_1060: Unknown result type (might be due to invalid IL or missing references)
		//IL_1065: Unknown result type (might be due to invalid IL or missing references)
		//IL_106b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1094: Unknown result type (might be due to invalid IL or missing references)
		//IL_109a: Unknown result type (might be due to invalid IL or missing references)
		switch (data.id)
		{
		case AnimEventFormat.ID.TARGET_LOCK_ON:
		case AnimEventFormat.ID.TARGET_LOCK_OFF:
		case AnimEventFormat.ID.TWO_HAND_SWORD_BURST_BASE_ATK:
			break;
		case AnimEventFormat.ID.SE_SKILL_ONESHOT:
		{
			SkillInfo.SkillParam actSkillParam = skillInfo.actSkillParam;
			if (actSkillParam == null || actSkillParam.tableData == null)
			{
				Log.Error(LOG.INGAME, "SE_SKILL_ONESHOT skill param none.");
			}
			else
			{
				int num11 = 0;
				switch (data.intArgs[0])
				{
				case 0:
					num11 = actSkillParam.tableData.startSEID;
					break;
				case 1:
					num11 = actSkillParam.tableData.actSEID;
					break;
				}
				string name2 = data.stringArgs[0];
				if (num11 == 0)
				{
					base.OnAnimEvent(data);
				}
				else if (EnablePlaySound())
				{
					SoundManager.PlayOneShotSE(num11, this, FindNode(name2));
				}
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
			bool flag2 = false;
			int num13 = 0;
			if (data.intArgs != null)
			{
				if (data.intArgs.Length > 0)
				{
					flag2 = ((data.intArgs[0] > 0) ? true : false);
				}
				if (data.intArgs.Length > 1)
				{
					num13 = data.intArgs[1];
				}
			}
			string text = playerParameter.arrowActionInfo.attackInfoNames[(int)spAttackType];
			if (data.stringArgs != null && data.stringArgs.Length > 0 && flag2)
			{
				text = playerParameter.arrowActionInfo.attackInfoForSitShotNames[(int)spAttackType] + num13.ToString();
			}
			AttackInfo attackInfo = FindAttackInfo(text, true, false);
			shotArrowCount++;
			if (attackInfo != null && attackMode == ATTACK_MODE.ARROW)
			{
				float num14;
				if (defaultBulletSpeedDic.ContainsKey(text))
				{
					num14 = defaultBulletSpeedDic[text];
				}
				else
				{
					num14 = attackInfo.bulletData.data.speed;
					defaultBulletSpeedDic.Add(text, attackInfo.bulletData.data.speed);
				}
				if (flag2)
				{
					num14 *= 1f + MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.sitShotBulletSpeedUpRate;
				}
				attackInfo.bulletData.data.speed = num14;
				if (IsCoopNone() || IsOriginal())
				{
					Vector3 bulletAppearPos = GetBulletAppearPos();
					Vector3 bulletShotVec = GetBulletShotVec(bulletAppearPos);
					Quaternion val3 = Quaternion.LookRotation(bulletShotVec);
					if (CheckAttackModeAndSpType(ATTACK_MODE.ARROW, SP_ATTACK_TYPE.HEAT) && flag2 && MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
					{
						float sitShotSideAngle = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.sitShotSideAngle;
						if (num13 > 1)
						{
							float num15 = (float)((num13 % 2 != 1) ? 1 : (-1));
							val3 *= Quaternion.Euler(0f, num15 * sitShotSideAngle, 0f);
						}
					}
					ShotArrow(bulletAppearPos, val3, attackInfo, flag2, !flag2 || num13 == 3);
				}
			}
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
			int num = data.intArgs[0];
			enableInputCombo = true;
			controllerInputCombo = true;
			inputComboID = num;
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
			if (isBoostMode)
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
			int num10 = data.intArgs[0];
			enableInputCombo = true;
			inputComboID = num10;
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
			float num2 = data.floatArgs[0];
			float num3 = data.floatArgs[1];
			bool flag = (data.intArgs[0] != 0) ? true : false;
			if (data.intArgs != null && data.intArgs.Length > 1)
			{
				isArrowSitShot = ((data.intArgs[1] > 0) ? true : false);
			}
			float num4 = 0f;
			switch (attackMode)
			{
			case ATTACK_MODE.TWO_HAND_SWORD:
				num4 = buffParam.GetChargeSwordsTimeRate();
				break;
			case ATTACK_MODE.ARROW:
				num4 = GetChargeArrowTimeRate();
				break;
			case ATTACK_MODE.PAIR_SWORDS:
				num4 = buffParam.GetChargePairSwordsTimeRate();
				break;
			case ATTACK_MODE.SPEAR:
				switch (spAttackType)
				{
				case SP_ATTACK_TYPE.NONE:
					num3 = 0f;
					num2 = playerParameter.spearActionInfo.exRushValidSec;
					if (evolveCtrl.IsExecLeviathan())
					{
						num4 = 1f;
					}
					break;
				case SP_ATTACK_TYPE.SOUL:
					num4 = buffParam.GetChargeSpearTimeRate();
					break;
				}
				break;
			}
			if (data.floatArgs.Length == 4)
			{
				isInputChargeExistOffset = true;
				num2 = data.floatArgs[2];
				num3 = data.floatArgs[3];
				if (num4 >= 1f)
				{
					num4 = 1f;
				}
				num2 = (num2 - num3) * (1f - num4) + num3;
			}
			else
			{
				isInputChargeExistOffset = false;
				num2 *= 1f - num4;
			}
			enableInputCharge = true;
			inputChargeAutoRelease = flag;
			inputChargeMaxTiming = true;
			inputChargeTimeMax = num2;
			inputChargeTimeOffset = num3;
			inputChargeTimeCounter = 0f;
			chargeRate = 0f;
			isChargeExRush = false;
			exRushChargeRate = 0f;
			StartWaitingPacket(WAITING_PACKET.PLAYER_CHARGE_RELEASE, true, 0f);
			CheckInputCharge();
			break;
		}
		case AnimEventFormat.ID.SKILL_CAST_LOOP_START:
		{
			string value = (data.stringArgs.Length <= 0) ? null : data.stringArgs[0];
			if (string.IsNullOrEmpty(value))
			{
				value = "next";
			}
			if (!isSkillCastLoop && skillInfo.actSkillParam != null && skillInfo.actSkillParam.tableData != null)
			{
				isSkillCastLoop = true;
				skillCastLoopStartTime = Time.get_time();
				skillCastLoopTrigger = value;
				XorFloat castTimeRate = skillInfo.actSkillParam.castTimeRate;
				float num12 = 1f - (buffParam.GetSkillTimeRate() + (float)castTimeRate);
				if (num12 <= 0f)
				{
					num12 = 0.1f;
				}
				skillCastLoopTime = skillInfo.actSkillParam.tableData.castTime * num12;
				CheckSkillCastLoop();
			}
			break;
		}
		case AnimEventFormat.ID.ROTATE_TO_TARGET_START:
		case AnimEventFormat.ID.ROTATE_KEEP_TO_TARGET_START:
		case AnimEventFormat.ID.ROTATE_TO_ANGLE_START:
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
			enableCounterAttack = true;
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
			stunnedEndTime = Time.get_time() + stunnedTime;
			stunnedReduceEnableTime = stunnedTime * playerParameter.stunnedReduceTimeMaxRate;
			break;
		case AnimEventFormat.ID.APPLY_SKILL_PARAM:
			ApplySkillParam();
			break;
		case AnimEventFormat.ID.APPLY_BLOW_FORCE:
			base.waitAddForce = false;
			SetVelocity(Vector3.get_zero(), VELOCITY_TYPE.NONE);
			break;
		case AnimEventFormat.ID.APPLY_CHANGE_WEAPON:
			if (IsCoopNone() || IsOriginal())
			{
				ApplyChangeWeapon(changeWeaponItem, changeWeaponIndex);
			}
			else if (IsValidWaitingPacket(WAITING_PACKET.PLAYER_APPLY_CHANGE_WEAPON))
			{
				Log.Error(LOG.INGAME, "Player APPLY_CHANGE_WEAPON Err. ( StartWaitingPacket already. )");
			}
			else
			{
				StartWaitingPacket(WAITING_PACKET.PLAYER_APPLY_CHANGE_WEAPON, false, 0f);
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
				float num8 = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.timeChargeExpandMax;
				float num9 = buffParam.GetChargeSwordsTimeRate();
				if (num9 > 1f)
				{
					num9 = 1f;
				}
				num8 *= 1f - num9;
				if (num8 < MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.minTimeChargeExpandMax)
				{
					timerChargeExpandOffset = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.minTimeChargeExpandMax - num8;
					num8 = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.minTimeChargeExpandMax;
				}
				isChargeExpandAutoRelease = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.isChargeExpandAutoRelease;
				timeChargeExpandMax = num8;
				timerChargeExpand = 0f;
				chargeExpandRate = 0f;
				StartWaitingPacket(WAITING_PACKET.PLAYER_CHARGE_RELEASE, true, 0f);
				CheckChargeExpand();
			}
			break;
		case AnimEventFormat.ID.TWO_HAND_SWORD_CHARGE_SOUL_START:
			if (CheckAttackModeAndSpType(ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.SOUL) && !enableInputCharge)
			{
				enableInputCharge = true;
				InGameSettingsManager.Player.TwoHandSwordActionInfo twoHandSwordActionInfo = playerParameter.twoHandSwordActionInfo;
				float num7 = buffParam.GetSoulChargeTimeRate();
				if (MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.ignoreSpAttackTypeAbility)
				{
					num7 += buffParam.GetChargeSwordsTimeRate();
				}
				float soulIaiChargeTime = twoHandSwordActionInfo.soulIaiChargeTime;
				soulIaiChargeTime *= 1f - num7;
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
				StartWaitingPacket(WAITING_PACKET.PLAYER_CHARGE_RELEASE, true, 0f);
				CheckInputCharge();
			}
			break;
		case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE:
			if (jumpState == eJumpState.None)
			{
				CreateAttackCollider(data, true);
			}
			else
			{
				InGameSettingsManager.Player.SpearActionInfo spearActionInfo3 = MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo;
				AnimEventData.EventData eventData = new AnimEventData.EventData();
				eventData.Copy(data, true);
				eventData.stringArgs[0] = spearActionInfo3.jumpWaveAttackInfoPrefix + useGaugeLevel;
				eventData.floatArgs[6] = spearActionInfo3.jumpWaveColliderRadius[useGaugeLevel];
				CreateAttackCollider(eventData, true);
			}
			break;
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
				StartWaitingPacket(WAITING_PACKET.PLAYER_CHARGE_RELEASE, true, 0f);
				CheckInputCharge();
			}
			break;
		case AnimEventFormat.ID.SPEAR_JUMP_FALL_WAIT:
			if (object.ReferenceEquals((object)base.body, null))
			{
				ActIdle(false, -1f);
			}
			else
			{
				InGameSettingsManager.Player.SpearActionInfo spearActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo;
				Vector3 position = _position;
				float num6 = position.x + jumpFallBodyPosition.x;
				Vector3 position2 = _position;
				float y = position2.y;
				Vector3 position3 = _position;
				Vector3 val = default(Vector3);
				val._002Ector(num6, y, position3.z + jumpFallBodyPosition.z);
				Vector3 val2 = _position - val;
				jumpRandingVector = val2.get_normalized() * spearActionInfo.jumpRandingLength;
				_position = val;
				jumpFallBodyPosition.Set(0f, spearActionInfo.jumpStartHeight, 0f);
				base.body.get_transform().set_localPosition(jumpFallBodyPosition);
				jumpActionCounter = spearActionInfo.jumpFallWaitSec;
				jumpState = eJumpState.FallWait;
			}
			break;
		case AnimEventFormat.ID.SE_ONESHOT_DEPEND_WEAPON_ELEMENT:
		{
			int currentWeaponElement = GetCurrentWeaponElement();
			if (currentWeaponElement <= 6 && data.intArgs.Length > currentWeaponElement && data.intArgs[currentWeaponElement] != 0)
			{
				string name = string.Empty;
				if (data.stringArgs.Length > 0)
				{
					name = data.stringArgs[0];
				}
				int num5 = data.intArgs[currentWeaponElement];
				if (num5 != 0 && EnablePlaySound())
				{
					SoundManager.PlayOneShotSE(num5, this, FindNode(name));
				}
			}
			break;
		}
		case AnimEventFormat.ID.WARP_VIEW_START:
			SetEnableNodeRenderer(string.Empty, false);
			Utility.SetLayerWithChildren(base._transform, 16, 12);
			DeactivateStoredEffect();
			break;
		case AnimEventFormat.ID.WARP_VIEW_END:
			SetEnableNodeRenderer(string.Empty, true);
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
				thsCtrl.SetReadyForShoot(true);
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
				thsCtrl.SetStartReloading(true);
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
				thsCtrl.SetChangeReloadMotionSpeedFlag(true);
				enableAnimSeedRate = true;
				UpdateAnimatorSpeed();
			}
			break;
		case AnimEventFormat.ID.THS_BURST_RELOAD_VARIABLE_SPEED_OFF:
			if (thsCtrl != null)
			{
				thsCtrl.SetChangeReloadMotionSpeedFlag(false);
				enableAnimSeedRate = false;
				UpdateAnimatorSpeed();
			}
			break;
		case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE_MULTI:
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				MonoBehaviourSingleton<StageObjectManager>.I.InvokeCoroutineImmidiately(CreateMultiAttackCollider(data, true));
			}
			break;
		case AnimEventFormat.ID.THS_BURST_TRANSITION_AVOID_ATK_ON:
			if (thsCtrl != null)
			{
				thsCtrl.SetEnableTransitionFromAvoidAtkFlag(true);
			}
			break;
		case AnimEventFormat.ID.THS_BURST_TRANSITION_AVOID_ATK_OFF:
			if (thsCtrl != null)
			{
				thsCtrl.SetEnableTransitionFromAvoidAtkFlag(false);
			}
			break;
		case AnimEventFormat.ID.GATHER_GIMMICK_GET:
			OnGatherGimmickGet();
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
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		float num = data.floatArgs[0];
		if (data.intArgs != null && data.intArgs.Length > 0 && data.intArgs[0] == 1 && data.floatArgs.Length > (int)spAttackType)
		{
			num = data.floatArgs[(int)spAttackType];
		}
		if (isActSpecialAction && isSpearAttackMode)
		{
			num *= GetRushDistanceRate();
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
			return base.attackID == 20;
		}
		return false;
	}

	private void EventPlayerFunnelAttack(AnimEventData.EventData data)
	{
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Expected O, but got Unknown
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		string text = data.stringArgs[0];
		string text2 = data.stringArgs[1];
		Transform val = FindNode(text2);
		if (val == null)
		{
			Log.Error("Not found transform for launch!! name:" + text2);
		}
		else
		{
			AttackInfo attackInfo = FindAttackInfo(text, true, false);
			if (attackInfo == null)
			{
				Log.Error("Not found AttackInfo!! name:" + text);
			}
			else
			{
				BulletData bulletData = attackInfo.bulletData;
				if (bulletData == null || bulletData.dataFunnel == null)
				{
					Log.Error("Not found BulletData!! atkInfoName:" + text);
				}
				else
				{
					Vector3 offsetPos = default(Vector3);
					offsetPos._002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
					Quaternion offsetRot = Quaternion.Euler(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
					GameObject val2 = new GameObject("PlayerAttackFunnelBit");
					PlayerAttackFunnelBit playerAttackFunnelBit = val2.AddComponent<PlayerAttackFunnelBit>();
					playerAttackFunnelBit.Initialize(this, attackInfo, (!IsValidBuffBlind()) ? base.actionTarget : null, val, offsetPos, offsetRot);
				}
			}
		}
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
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		string name = data.stringArgs[0];
		string name2 = data.stringArgs[1];
		int numLaser = data.intArgs[0];
		Transform val = Utility.Find(base._transform, name2);
		if (!(val == null))
		{
			AttackInfo attackInfo = FindAttackInfo(name, true, false);
			if (attackInfo != null)
			{
				BulletData bulletData = attackInfo.bulletData;
				if (!(bulletData == null) && bulletData.dataLaser != null)
				{
					GameObject val2 = new GameObject("AttackNWayLaser");
					AttackNWayLaser attackNWayLaser = val2.AddComponent<AttackNWayLaser>();
					attackNWayLaser.Initialize(this, val, attackInfo, numLaser);
					activeAttackLaserList.Add(attackNWayLaser);
				}
			}
		}
	}

	protected override void EventShotPresent(AnimEventData.EventData data)
	{
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		if (data.stringArgs.Length <= 0)
		{
			Log.Error(LOG.INGAME, "Not set bullet name. Please check AnimEvent 'SHOT_PRESENT'.");
		}
		else if (data.floatArgs.Length <= 0)
		{
			Log.Error(LOG.INGAME, "Not set Range. Please check AnimEvent 'SHOT_PRESENT'.");
		}
		else if (base.cachedBulletDataTable == null)
		{
			Log.Error(LOG.INGAME, "Not set bullet data. Please check bullet data exist.");
		}
		else
		{
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
			if (!list.IsNullOrEmpty())
			{
				int j = 0;
				for (int count = list.Count; j < count; j++)
				{
					string text = list[j];
					if (!string.IsNullOrEmpty(text))
					{
						BulletData bulletData = base.cachedBulletDataTable.Get(text);
						if (!(bulletData == null))
						{
							BulletData.BulletPresent dataPresent = bulletData.dataPresent;
							if (dataPresent != null)
							{
								int result = 0;
								string s = id.ToString() + (MonoBehaviourSingleton<StageObjectManager>.I.presentBulletObjIndex + 1).ToString();
								float num2 = data.floatArgs[0];
								float num3 = 360f / (float)(count - 1) * (float)j;
								if (int.TryParse(s, out result))
								{
									Vector3 val = base._transform.get_localRotation() * Quaternion.AngleAxis(num3, Vector3.get_up()) * base._transform.get_forward();
									Vector3 position = base._transform.get_position() + val * num2;
									if (j == 0)
									{
										position = base._transform.get_position();
									}
									SetPresentBullet(result, dataPresent.type, position, text);
									if (playerSender != null)
									{
										playerSender.OnSetPresentBullet(result, dataPresent.type, position, text);
									}
								}
							}
						}
					}
				}
			}
		}
	}

	public void SetPresentBullet(int presentBulletId, BulletData.BulletPresent.TYPE type, Vector3 position, string bulletName)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		BulletData bulletData = base.cachedBulletDataTable.Get(bulletName);
		if (!(bulletData == null))
		{
			GameObject val = new GameObject();
			IPresentBulletObject presentBulletObject = null;
			switch (type)
			{
			case BulletData.BulletPresent.TYPE.HEAL:
				presentBulletObject = val.AddComponent<PresentBulletObject>();
				break;
			}
			if (presentBulletObject != null)
			{
				if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
				{
					Object.Destroy(val);
				}
				else
				{
					presentBulletObject.Initialize(presentBulletId, bulletData, base._transform);
					presentBulletObject.SetPosition(position);
					presentBulletObject.SetSkillParam(skillInfo.actSkillParam);
					if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
					{
						MonoBehaviourSingleton<StageObjectManager>.I.presentBulletObjList.Add(presentBulletObject);
						MonoBehaviourSingleton<StageObjectManager>.I.presentBulletObjIndex++;
					}
				}
			}
		}
	}

	public void DestroyPresentBulletObject(int presentBulletId)
	{
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			List<IPresentBulletObject> presentBulletObjList = MonoBehaviourSingleton<StageObjectManager>.I.presentBulletObjList;
			if (presentBulletObjList != null && presentBulletObjList.Count > 0)
			{
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
		}
	}

	protected override void EventShotZone(AnimEventData.EventData data)
	{
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		if (data.stringArgs.Length <= 0)
		{
			Log.Error(LOG.INGAME, "Not set bullet name. Please check AnimEvent 'SHOT_ZONE'.");
		}
		else if (base.cachedBulletDataTable == null)
		{
			Log.Error(LOG.INGAME, "Not set bullet data. Please check bullet data exist.");
		}
		else
		{
			string text = data.stringArgs[0];
			if (!string.IsNullOrEmpty(text))
			{
				BulletData bulletData = base.cachedBulletDataTable.Get(text);
				if (!object.ReferenceEquals(bulletData, null))
				{
					BulletData.BulletZone dataZone = bulletData.dataZone;
					if (!object.ReferenceEquals(dataZone, null))
					{
						ShotZoneBullet(this, text, base._transform.get_position(), MonoBehaviourSingleton<StageObjectManager>.I.ExistsEnemyValiedHealAttack(), true);
						if (playerSender != null)
						{
							playerSender.OnShotZoneBullet(text, base._transform.get_position());
						}
					}
				}
			}
		}
	}

	public void ShotZoneBullet(Player onwerPlayer, string bulletName, Vector3 position, bool isHealDamgeEnemy = false, bool isOwner = false)
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && !object.ReferenceEquals(skillInfo.actSkillParam, null) && !object.ReferenceEquals(skillInfo.actSkillParam.tableData, null))
		{
			BulletData bulletData = base.cachedBulletDataTable.Get(bulletName);
			if (!object.ReferenceEquals(bulletData, null) && !object.ReferenceEquals(bulletData.dataZone, null))
			{
				GameObject val = new GameObject();
				ZoneBulletObject zoneBulletObject = null;
				BulletData.BulletZone.TYPE type = bulletData.dataZone.type;
				if (type == BulletData.BulletZone.TYPE.HEAL)
				{
					zoneBulletObject = val.AddComponent<ZoneBulletObject>();
				}
				else
				{
					isHealDamgeEnemy = false;
				}
				if (!object.ReferenceEquals(zoneBulletObject, null))
				{
					zoneBulletObject.Initialize(onwerPlayer, bulletData, position, skillInfo.actSkillParam, isHealDamgeEnemy, isOwner);
				}
			}
		}
	}

	public override void EventShotDecoy(AnimEventData.EventData data)
	{
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		if (data.stringArgs.Length <= 0)
		{
			Log.Error(LOG.INGAME, "Not set bullet name. Please check AnimEvent 'SHOT_DECOY'.");
		}
		else if (base.cachedBulletDataTable == null)
		{
			Log.Error(LOG.INGAME, "Not set bullet data. Please check bullet data exist.");
		}
		else
		{
			string text = data.stringArgs[0];
			if (!string.IsNullOrEmpty(text))
			{
				BulletData bulletData = base.cachedBulletDataTable.Get(text);
				if (!object.ReferenceEquals(bulletData, null))
				{
					BulletData.BulletDecoy dataDecoy = bulletData.dataDecoy;
					if (!object.ReferenceEquals(dataDecoy, null))
					{
						int decoyId = id * 10 + localDecoyId;
						if (++localDecoyId >= 10)
						{
							localDecoyId = 0;
						}
						Vector3 position = base._transform.get_position();
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
						ShotDecoyBullet(id, skIndex, decoyId, text, position, true);
						if (playerSender != null)
						{
							playerSender.OnShotDecoyBullet(skIndex, decoyId, text, position);
						}
					}
				}
			}
		}
	}

	public void ShotDecoyBullet(int playerId, int skIndex, int decoyId, string bulletName, Vector3 position, bool isHit)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			BulletData bulletData = base.cachedBulletDataTable.Get(bulletName);
			if (!object.ReferenceEquals(bulletData, null) && !object.ReferenceEquals(bulletData.dataDecoy, null))
			{
				GameObject val = new GameObject();
				DecoyBulletObject decoyBulletObject = val.AddComponent<DecoyBulletObject>();
				if (!object.ReferenceEquals(decoyBulletObject, null))
				{
					decoyBulletObject.Initialize(playerId, decoyId, bulletData, position, skillInfo.GetSkillParam(skIndex), isHit);
					Enemy boss = MonoBehaviourSingleton<StageObjectManager>.I.boss;
					if (!object.ReferenceEquals(boss, null) && (boss.IsOriginal() || boss.IsCoopNone()))
					{
						decoyBulletObject.HateCtrl();
					}
				}
			}
		}
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
			if (!object.ReferenceEquals(stageObject, null))
			{
				DecoyBulletObject decoyBulletObject = stageObject as DecoyBulletObject;
				if (!object.ReferenceEquals(decoyBulletObject, null))
				{
					decoyBulletObject.OnDisappear(false);
				}
			}
		}
	}

	private void EventPairSwordsShotBullet(AnimEventData.EventData data)
	{
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		if (pairSwordsCtrl == null)
		{
			Log.Error("EventPairSwordsShotBullet. pairSwordsCtrl is null!!");
		}
		else
		{
			string text = data.stringArgs[0];
			string text2 = data.stringArgs[1];
			Transform val = FindNode(text2);
			if (val == null)
			{
				Log.Error("EventPairSwordsShotBullet. Not found transform for launch!! name:" + text2);
			}
			else
			{
				AttackInfo attackInfo = FindAttackInfo(text, true, false);
				if (attackInfo == null)
				{
					Log.Error("EventPairSwordsShotBullet. Not found AttackInfo!! name:" + text);
				}
				else
				{
					BulletData bulletData = attackInfo.bulletData;
					if (bulletData == null || bulletData.dataPairSwordsSoul == null)
					{
						Log.Error("EventPairSwordsShotBullet. Not found BulletData!! atkInfoName:" + text);
					}
					else
					{
						string change_effect = null;
						if (!playerParameter.pairSwordsActionInfo.Soul_EffectsForBullet.IsNullOrEmpty())
						{
							int nowWeaponElement = (int)GetNowWeaponElement();
							if (playerParameter.pairSwordsActionInfo.Soul_EffectsForBullet.Length >= nowWeaponElement)
							{
								change_effect = playerParameter.pairSwordsActionInfo.Soul_EffectsForBullet[nowWeaponElement];
							}
						}
						Vector3 val2 = default(Vector3);
						val2._002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
						Quaternion val3 = Quaternion.Euler(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
						Vector3 val4 = val.get_position() + Quaternion.LookRotation(_forward) * val2;
						Vector3 bulletShotVecPositiveY = GetBulletShotVecPositiveY(val4);
						Quaternion rot = Quaternion.LookRotation(bulletShotVecPositiveY) * val3;
						AnimEventShot.Create(this, attackInfo, val4, rot, null, true, change_effect, null, null, ATTACK_MODE.NONE, null, null);
					}
				}
			}
		}
	}

	private void EventPairSwordsShotLaser(AnimEventData.EventData data)
	{
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		string text = data.stringArgs[0];
		string text2 = data.stringArgs[1];
		Transform val = FindNode(text2);
		if (val == null)
		{
			Log.Error("EventPairSwordsShotLaser. Not found transform for launch!! name:" + text2);
		}
		else
		{
			int comboLv = pairSwordsCtrl.GetComboLv();
			string[] soul_AttackInfoNamesForLaserByComboLv = playerParameter.pairSwordsActionInfo.Soul_AttackInfoNamesForLaserByComboLv;
			int[] soul_NumOfLaserByComboLv = playerParameter.pairSwordsActionInfo.Soul_NumOfLaserByComboLv;
			if (!soul_AttackInfoNamesForLaserByComboLv.IsNullOrEmpty() && soul_AttackInfoNamesForLaserByComboLv.Length >= comboLv && !soul_NumOfLaserByComboLv.IsNullOrEmpty() && soul_NumOfLaserByComboLv.Length >= comboLv)
			{
				text = soul_AttackInfoNamesForLaserByComboLv[comboLv - 1];
				if (!text.IsNullOrWhiteSpace())
				{
					AttackInfo attackInfo = FindAttackInfo(text, true, false);
					if (attackInfo == null)
					{
						Log.Error("EventPairSwordsShotLaser. Not found AttackInfo!! name:" + text);
					}
					else
					{
						BulletData bulletData = attackInfo.bulletData;
						if (bulletData == null)
						{
							Log.Error("EventPairSwordsShotLaser. Not found BulletData!! atkInfoName:" + text);
						}
						else
						{
							Vector3 val2 = default(Vector3);
							val2._002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
							Quaternion val3 = Quaternion.Euler(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
							int num = soul_NumOfLaserByComboLv[comboLv - 1];
							Vector3 bulletShotVecForSpWeak = GetBulletShotVecForSpWeak(val.get_position());
							for (int i = 1; i <= num; i++)
							{
								Vector3 zero = Vector3.get_zero();
								if (num == 1)
								{
									zero = val.get_position() + Quaternion.LookRotation(_forward) * val2;
									bulletShotVecForSpWeak = GetBulletShotVecForSpWeak(zero);
								}
								else
								{
									float num2 = (float)(360 / num * i) * 0.0174532924f;
									zero = val.get_position() + Quaternion.LookRotation(_forward) * (val2 + new Vector3(Mathf.Cos(num2), Mathf.Sin(num2), 0f) * playerParameter.pairSwordsActionInfo.Soul_RadiusForLaser);
								}
								Quaternion rot = Quaternion.LookRotation(bulletShotVecForSpWeak) * val3;
								AnimEventShot animEventShot = AnimEventShot.Create(this, attackInfo, zero, rot, null, true, null, null, null, ATTACK_MODE.NONE, null, null);
								pairSwordsCtrl.AddBulletLaser(animEventShot);
								animEventShot._transform.set_parent(base._transform);
								Vector3 val4 = bulletShotVecForSpWeak;
								val4.y = 0f;
								_rotation = Quaternion.LookRotation(val4);
							}
							this.StartCoroutine(PlayPairSwordsShotLaserSE());
							pairSwordsCtrl.SetGaugePercentForLaser();
							pairSwordsCtrl.SetEventShotLaserExec();
							StartWaitingPacket(WAITING_PACKET.PLAYER_PAIR_SWORDS_LASER_END, true, 0f);
						}
					}
				}
			}
		}
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
			yield return (object)new WaitForSeconds(playerParameter.pairSwordsActionInfo.Soul_TimeForPlayLoopSE);
			if (seIds[1] > 0)
			{
				SoundManager.PlayLoopSE(seIds[1], this, base._transform);
			}
		}
	}

	private void EventShotHealingHoming(AnimEventData.EventData data)
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		Transform val = FindNode(model.launchNodeName);
		if (!(val == null))
		{
			AttackInfo attackInfo = FindAttackInfo(model.atkInfoName, true, false);
			if (attackInfo != null)
			{
				BulletData bulletData = attackInfo.bulletData;
				if (!(bulletData == null) && bulletData.dataHealingHomingBullet != null && !model.targetPlayerIDs.IsNullOrEmpty() && MonoBehaviourSingleton<StageObjectManager>.IsValid() && model.targetNum > 0)
				{
					Vector3 pos = val.get_position() + Quaternion.LookRotation(_forward) * model.offsetPos;
					Quaternion rot = Quaternion.LookRotation(_forward) * Quaternion.Euler(model.offsetRot);
					int num = Mathf.Min(model.targetNum, model.targetPlayerIDs.Length);
					for (int i = 0; i < num; i++)
					{
						AnimEventShot animEventShot = AnimEventShot.Create(this, attackInfo, pos, rot, null, true, null, null, null, ATTACK_MODE.NONE, null, null);
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
			}
		}
	}

	private unsafe int[] GetSortedArrayByPlayerDistance(int targetNum)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
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
		list.Sort(delegate(StageObject a, StageObject b)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			Vector3 val = a.get_transform().get_position() - targetPos;
			float magnitude = val.get_magnitude();
			Vector3 val2 = b.get_transform().get_position() - targetPos;
			return Mathf.RoundToInt(magnitude - val2.get_magnitude());
		});
		int num = targetNum;
		if (num <= 0 || list.Count <= num)
		{
			num = list.Count;
		}
		List<StageObject> range = list.GetRange(0, num);
		if (_003C_003Ef__am_0024cacheE3 == null)
		{
			_003C_003Ef__am_0024cacheE3 = new Func<StageObject, int>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		return range.Select<StageObject, int>(_003C_003Ef__am_0024cacheE3).ToArray();
	}

	private void EventHealHp()
	{
	}

	public virtual void ShotArrow(Vector3 shot_pos, Quaternion shot_rot, AttackInfo attack_info, bool isSitShot, bool isAimEnd)
	{
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		if (attack_info != null)
		{
			GameObject attach_object = ResourceUtility.Instantiate<GameObject>(MonoBehaviourSingleton<InGameLinkResourcesCommon>.I.arrow);
			string change_effect = null;
			if (isArrowAimBossMode && attack_info.rateInfoRate >= 1f)
			{
				change_effect = playerParameter.specialActionInfo.arrowChargeAimEffectName;
			}
			bool isBossPierce = false;
			bool isCharacterHitDelete = attack_info.bulletData.data.isCharacterHitDelete;
			float num = -1f;
			if (!object.ReferenceEquals(attack_info.bulletData.dataFall, null))
			{
				num = attack_info.bulletData.dataFall.gravityStartTime;
			}
			if (1f <= chargeRate)
			{
				if (!object.ReferenceEquals(bossBrain, null))
				{
					if (!isSitShot && spAttackType == SP_ATTACK_TYPE.HEAT)
					{
						attack_info.bulletData.data.isCharacterHitDelete = false;
						if (num != -1f)
						{
							attack_info.bulletData.dataFall.gravityStartTime = -1f;
						}
						isBossPierce = true;
					}
				}
				else
				{
					attack_info.bulletData.data.isCharacterHitDelete = false;
				}
			}
			EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)weaponData.eId);
			DamageDistanceTable.DamageDistanceData data = Singleton<DamageDistanceTable>.I.GetData((uint)equipItemData.damageDistanceId);
			AnimEventShot animEventShot = AnimEventShot.CreateArrow(this, attack_info, shot_pos, shot_rot, attach_object, true, change_effect, data);
			animEventShot.SetArrowInfo(isArrowAimBossMode, isBossPierce);
			attack_info.bulletData.data.isCharacterHitDelete = isCharacterHitDelete;
			if (num != -1f)
			{
				attack_info.bulletData.dataFall.gravityStartTime = num;
			}
			isArrowAimEnd = isAimEnd;
			if (playerSender != null)
			{
				playerSender.OnShotArrow(shot_pos, shot_rot, attack_info, isSitShot, isAimEnd);
			}
		}
	}

	public unsafe virtual void ShotSoulArrow()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Expected O, but got Unknown
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		List<TargetMarker> targetMarkerList = MonoBehaviourSingleton<TargetMarkerManager>.I.GetTargetMarkerList();
		if (targetMarkerList != null && targetMarkerList.Count > 0)
		{
			InGameSettingsManager.Player.ArrowActionInfo arrowActionInfo = playerParameter.arrowActionInfo;
			Vector3 shotPos = GetBulletAppearPos();
			Vector3 bulletShotVec = GetBulletShotVec(shotPos);
			shotPos -= bulletShotVec * arrowActionInfo.soulShotPosVec;
			Quaternion bowRot = Quaternion.LookRotation(bulletShotVec);
			List<Vector3> targetPosList = new List<Vector3>();
			List<SoulArrowInfo> list = new List<SoulArrowInfo>();
			int num = (!isBoostMode) ? arrowActionInfo.soulLockMax : arrowActionInfo.soulBoostLockMax;
			for (int i = 0; i < targetMarkerList.Count; i++)
			{
				TargetMarker targetMarker = targetMarkerList[i];
				if (targetMarker != null)
				{
					List<int> multiLockOrder = targetMarker.GetMultiLockOrder();
					if (multiLockOrder != null)
					{
						int count = multiLockOrder.Count;
						if (count != 0)
						{
							bool isSpecialAttack = count == arrowActionInfo.soulLockRegionMax;
							float num2 = arrowActionInfo.soulLockNumAtkRate[count - 1];
							for (int j = 0; j < count; j++)
							{
								AttackHitInfo attackHitInfo = (AttackHitInfo)FindAttackInfo(arrowActionInfo.attackInfoNames[2], true, true);
								if (attackHitInfo != null)
								{
									int num3 = multiLockOrder[j];
									float num4 = (num3 != num) ? (arrowActionInfo.soulLockOrderAtkRateBase + arrowActionInfo.soulLockOrderAtkRateCoefficient * (float)(num3 - 1)) : ((!isBoostMode) ? arrowActionInfo.soulLockOrderAtkRateMax : arrowActionInfo.soulLockOrderAtkRateMaxBoost);
									attackHitInfo.atkRate = num2 * num4;
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
					}
				}
			}
			_003CShotSoulArrow_003Ec__AnonStorey512 _003CShotSoulArrow_003Ec__AnonStorey;
			this.StartCoroutine(_ShotSoulArrow(shotPos, bowRot, list, arrowActionInfo.soulShotInterval, new Action((object)_003CShotSoulArrow_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
		}
	}

	private IEnumerator _ShotSoulArrow(Vector3 shotPos, Quaternion bowRot, List<SoulArrowInfo> saInfo, float interval, Action endCallback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < saInfo.Count; i++)
		{
			SoulArrowInfo info = saInfo[i];
			AnimEventShot shot = AnimEventShot.CreateArrow(attach_object: ResourceUtility.Instantiate<GameObject>(MonoBehaviourSingleton<InGameLinkResourcesCommon>.I.arrow), stage_object: this, atk_info: info.attackInfo, pos: shotPos, rot: bowRot, isScaling: true, change_effect: string.Empty, damageDistanceData: null);
			shot.SetArrowInfo(isArrowAimBossMode, false);
			shot.SetTarget(info.point);
			yield return (object)new WaitForSeconds(interval);
		}
		endCallback.Invoke();
	}

	public void ShotSoulArrowPuppet(Vector3 shotPos, Quaternion bowRot, List<Vector3> targetPosList)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		AttackInfo atk_info = FindAttackInfo(playerParameter.arrowActionInfo.attackInfoNames[2], true, false);
		for (int i = 0; i < targetPosList.Count; i++)
		{
			GameObject attach_object = ResourceUtility.Instantiate<GameObject>(MonoBehaviourSingleton<InGameLinkResourcesCommon>.I.arrow);
			AnimEventShot animEventShot = AnimEventShot.CreateArrow(this, atk_info, shotPos, bowRot, attach_object, true, string.Empty, null);
			animEventShot.SetArrowInfo(isArrowAimBossMode, false);
			animEventShot.SetPuppetTargetPos(targetPosList[i]);
		}
		SetNextTrigger(0);
	}

	public virtual Vector3 GetBulletAppearPos()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		if (loader.socketWepR == null)
		{
			Bounds bounds = base._collider.get_bounds();
			return bounds.get_center() + Vector3.get_up() * 0.3f;
		}
		return loader.socketWepR.get_position();
	}

	public virtual Vector3 GetBulletShotVec(Vector3 appear_pos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = _forward;
		if (!IsValidBuffBlind() && GetTargetPos(out Vector3 pos))
		{
			val = pos - appear_pos;
		}
		return val.get_normalized();
	}

	public Vector3 GetBulletShotVecPositiveY(Vector3 appearPos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = _forward;
		if (!IsValidBuffBlind() && GetTargetPos(out Vector3 pos))
		{
			if (pos.y < 0f)
			{
				pos.y = 0f;
			}
			val = pos - appearPos;
		}
		return val.get_normalized();
	}

	public Vector3 GetBulletShotVecForSpWeak(Vector3 appearPos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = GetBulletShotVec(appearPos);
		if (!IsValidBuffBlind())
		{
			if (base.targetPointPos != Vector3.get_zero())
			{
				val = base.targetPointPos - appearPos;
			}
			if (targetPointWithSpWeak != null)
			{
				val = targetPointWithSpWeak.param.markerPos - appearPos;
			}
		}
		return val.get_normalized();
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
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if (base.animator != null && attackMode == ATTACK_MODE.ARROW)
		{
			Vector3 bulletAppearPos = GetBulletAppearPos();
			Vector3 bulletShotVec = GetBulletShotVec(bulletAppearPos);
			if (bulletShotVec != Vector3.get_zero())
			{
				Quaternion val = Quaternion.LookRotation(bulletShotVec);
				Vector3 eulerAngles = val.get_eulerAngles();
				float num = eulerAngles.x;
				if (num >= 180f)
				{
					num -= 360f;
				}
				num = 0f - num;
				base.animator.SetFloat(arrowAngleID, num);
			}
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
		return chargeRate;
	}

	public override AttackInfo FindAttackInfoExternal(string name, bool fix_rate, float rate)
	{
		AttackInfo[] attackInfosAll = playerParameter.attackInfosAll;
		return _FindAttackInfo(attackInfosAll, name, fix_rate, rate, false);
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		pos = Vector3.get_zero();
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
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (uiPlayerStatusGizmo != null && uiPlayerStatusGizmo.get_gameObject().get_activeInHierarchy())
		{
			uiPlayerStatusGizmo.SayChat(message);
		}
		OnSendChatMessage(message);
		base.ChatSay(message);
	}

	public override void ChatSayStamp(int stamp_id)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (uiPlayerStatusGizmo != null && uiPlayerStatusGizmo.get_gameObject().get_activeInHierarchy())
		{
			uiPlayerStatusGizmo.SayChatStamp(stamp_id);
		}
		OnSendChatStamp(stamp_id);
		base.ChatSayStamp(stamp_id);
	}

	protected virtual void OnSendChatMessage(string message)
	{
		if (MonoBehaviourSingleton<UIInGameMessageBar>.IsValid() && MonoBehaviourSingleton<UIInGameMessageBar>.I.get_isActiveAndEnabled())
		{
			MonoBehaviourSingleton<UIInGameMessageBar>.I.Announce(base.charaName, message);
		}
	}

	protected virtual void OnSendChatStamp(int stamp_id)
	{
		if (MonoBehaviourSingleton<UIInGameMessageBar>.IsValid() && MonoBehaviourSingleton<UIInGameMessageBar>.I.get_isActiveAndEnabled())
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
			ActIdle(false, -1f);
			break;
		case WAITING_PACKET.PLAYER_PRAYER_END:
		{
			int i = 0;
			for (int count = prayTargetIds.Count; i < count; i++)
			{
				Player player = MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(prayTargetIds[i]) as Player;
				if (player == null)
				{
					return;
				}
				player.EndPrayed(id);
			}
			prayTargetIds.Clear();
			boostPrayTargetInfoList.Clear();
			boostPrayedInfoList.Clear();
			break;
		}
		case WAITING_PACKET.PLAYER_JUMP_END:
			ActIdle(false, -1f);
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
			ActIdle(false, -1f);
			break;
		}
		base.OnFailedWaitingPacket(type);
	}

	public virtual void SetAppearPosField()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		if (FieldManager.IsValidInGame())
		{
			Vector3 zero = Vector3.get_zero();
			zero.x = MonoBehaviourSingleton<FieldManager>.I.currentStartMapX;
			zero.z = MonoBehaviourSingleton<FieldManager>.I.currentStartMapZ;
			_position = zero;
			_rotation = Quaternion.AngleAxis(MonoBehaviourSingleton<FieldManager>.I.currentStartMapDir, Vector3.get_up());
			SetAppearPos(zero);
		}
	}

	public virtual void SetAppearPosOwner(Vector3 enemy_pos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = enemy_pos + Vector3.get_forward() * playerParameter.appearPosDistance;
		if (MonoBehaviourSingleton<StageManager>.I.CheckPosInside(val))
		{
			_position = val;
			LookAt(enemy_pos, false);
			SetAppearPos(val);
		}
		else
		{
			SetAppearPosGuest(enemy_pos);
		}
	}

	public virtual void SetAppearPosGuest(Vector3 center_pos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		SetAppearRandomPosFixDistance(center_pos, playerParameter.appearPosDistance, playerParameter.appearPosTryCount);
		LookAt(center_pos, false);
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
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		if (!(enemy == null) && !(base.effectPlayProcessor == null))
		{
			List<EffectPlayProcessor.EffectSetting> settings = base.effectPlayProcessor.GetSettings("EFFECT_DRAIN_DAMAGE");
			if (settings != null && settings.Count > 0)
			{
				GameObject val = new GameObject("EffectDrain");
				EffectDrain effectDrain = val.AddComponent<EffectDrain>();
				effectDrain.Initialize(this, enemy, settings[0]);
			}
		}
	}

	public int GetCurrentWeaponElement()
	{
		if (weaponData == null)
		{
			return 6;
		}
		EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)weaponData.eId);
		return equipItemData.GetElemAtkType(null);
	}

	public Vector3 GetCannonVector()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		if (targetFieldGimmickCannon == null)
		{
			return Vector3.get_zero();
		}
		Transform cannonTransform = targetFieldGimmickCannon.GetCannonTransform();
		if (cannonTransform != null)
		{
			return cannonTransform.get_forward();
		}
		return Vector3.get_zero();
	}

	public void ApplyCannonVector(Vector3 cannonVec)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = cannonVec;
		val.y = 0f;
		_rotation = Quaternion.LookRotation(val);
		if (targetFieldGimmickCannon != null)
		{
			targetFieldGimmickCannon.ApplyCannonVector(cannonVec);
		}
	}

	public void SetSyncCannonRotation(Vector3 cannonVec)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		if (cannonVec != Vector3.get_zero())
		{
			syncCannonRotation = Quaternion.LookRotation(cannonVec);
		}
		if (GetCannonVector() != Vector3.get_zero())
		{
			prevCannonRotation = Quaternion.LookRotation(GetCannonVector());
		}
		syncCannonVecTimer = 0f;
		isSyncingCannonRotation = true;
	}

	protected void UpdateSyncCannonRotation()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		if (isSyncingCannonRotation)
		{
			if (base.actionID != (ACTION_ID)30)
			{
				isSyncingCannonRotation = false;
			}
			else
			{
				float num = Mathf.Clamp01(syncCannonVecTimer / 1f);
				Quaternion val = Quaternion.SlerpUnclamped(prevCannonRotation, syncCannonRotation, num);
				Vector3 cannonVec = val * Vector3.get_forward();
				ApplyCannonVector(cannonVec);
				syncCannonVecTimer += Time.get_deltaTime();
			}
		}
	}

	public void SetSyncUsingCannon(int id)
	{
		if (QuestManager.IsValidInGame() && !IsOnCannonMode() && MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			IFieldGimmickCannon fieldGimmickCannon = MonoBehaviourSingleton<InGameProgress>.I.GetFieldGimmickObj(InGameProgress.eFieldGimmick.Cannon, id) as IFieldGimmickCannon;
			if (fieldGimmickCannon != null)
			{
				base.actionID = (ACTION_ID)30;
				targetFieldGimmickCannon = fieldGimmickCannon;
				fieldGimmickCannon.OnBoard(this);
				PlayMotion(131, -1f);
				SetCannonState(CANNON_STATE.READY);
			}
		}
	}

	protected override void EventStatusUpDefenceON(AnimEventData.EventData data)
	{
		isAnimEventStatusUpDefence = true;
		if (data.floatArgs.Length > 0)
		{
			animEventStatusUpDefenceRate = data.floatArgs[0];
		}
	}

	protected override void EventStatusUpDefenceOFF()
	{
		isAnimEventStatusUpDefence = false;
		animEventStatusUpDefenceRate = 1f;
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

	private bool _IsGuard()
	{
		return isGuardWalk || base.actionID == (ACTION_ID)18 || base.actionID == (ACTION_ID)19 || base.actionID == (ACTION_ID)33 || base.actionID == (ACTION_ID)20;
	}

	private bool _IsGuard(SP_ATTACK_TYPE spType)
	{
		if (!_IsGuard())
		{
			return false;
		}
		return CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, spType);
	}

	private void _StartGuard()
	{
		isJustGuard = false;
		isSuccessParry = false;
		if (spAttackType == SP_ATTACK_TYPE.BURST)
		{
			guardingSec = playerParameter.ohsActionInfo.burstOHSInfo.JustGuardValidSec * buffParam.GetJustGuardExtendRate();
		}
		else
		{
			guardingSec = playerParameter.ohsActionInfo.Common_JustGuardValidSec * buffParam.GetJustGuardExtendRate();
		}
		_StartRevengeCharge();
	}

	private void _EndGuard()
	{
		guardingSec = 0f;
		_EndRevengeCharge();
	}

	private void _UpdateGuard()
	{
		if (guardingSec > 0f)
		{
			guardingSec -= Time.get_deltaTime();
		}
		if (spAttackType == SP_ATTACK_TYPE.HEAT)
		{
			_UpdateRevengeCharge();
		}
	}

	protected bool _CheckJustGuardSec()
	{
		return guardingSec > 0f;
	}

	private float _GetGuardDamageCutRate()
	{
		float num = (!CheckAttackModeAndSpType(ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.NONE) || !_CheckJustGuardSec()) ? playerParameter.ohsActionInfo.Common_GuardDamageCutRate : playerParameter.ohsActionInfo.Normal_JustGuardDamageCutRate;
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

	private void _AddRevengeCounter(int damage)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		if (base.hpMax != 0)
		{
			IncreaseSpActonGauge(AttackHitInfo.ATTACK_TYPE.NORMAL, Vector3.get_zero(), (float)damage, 0f, false, false);
			_StartRevengeCharge();
		}
	}

	private void _StartRevengeCharge()
	{
		if (IsSpActionGaugeFullCharged() && revengeChargeState == eChargeState.None)
		{
			if (playerParameter.ohsActionInfo.Heat_RevengeAttackChargeSec == 0f)
			{
				revengeChargeState = eChargeState.Full;
				revengeChargeSec = 0f;
				if (object.ReferenceEquals(ohsMaxChargeEffect, null))
				{
					ohsMaxChargeEffect = EffectManager.GetEffect("ef_btl_wsk_sword_01_04", FindNode("R_Wep"));
				}
			}
			else
			{
				revengeChargeState = eChargeState.Charge;
				revengeChargeSec = playerParameter.ohsActionInfo.Heat_RevengeAttackChargeSec;
				if (object.ReferenceEquals(ohsChargeEffect, null))
				{
					ohsChargeEffect = EffectManager.GetEffect("ef_btl_pl05_attack_01", FindNode("R_Wep"));
				}
			}
		}
	}

	private void _UpdateRevengeCharge()
	{
		if (revengeChargeState == eChargeState.Charge)
		{
			revengeChargeSec -= Time.get_deltaTime();
			if (revengeChargeSec <= 0f)
			{
				revengeChargeState = eChargeState.Full;
				ReleaseEffect(ref ohsChargeEffect, true);
				if (object.ReferenceEquals(ohsMaxChargeEffect, null))
				{
					ohsMaxChargeEffect = EffectManager.GetEffect("ef_btl_wsk_sword_01_04", FindNode("R_Wep"));
				}
			}
		}
	}

	private void _EndRevengeCharge()
	{
		revengeChargeState = eChargeState.None;
		revengeChargeSec = 0f;
		ReleaseEffect(ref ohsChargeEffect, true);
		ReleaseEffect(ref ohsMaxChargeEffect, true);
	}

	private bool _IsRevengeChargeFull()
	{
		return revengeChargeState == eChargeState.Full;
	}

	private void _AttackRevengeBurst()
	{
		ActAttack(96, true, true, string.Empty);
		CurrentWeaponSpActionGauge = 0f;
		_EndRevengeCharge();
	}

	public virtual void CheckBuffShadowSealing()
	{
	}

	protected void _StartBuffShadowSealing()
	{
		if (!isBuffShadowSealing)
		{
			isBuffShadowSealing = true;
			if (object.ReferenceEquals(buffShadowSealingEffect, null))
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
			ReleaseEffect(ref buffShadowSealingEffect, true);
		}
	}

	protected void UpdateSpearAction()
	{
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		InGameSettingsManager.Player.SpearActionInfo spearActionInfo = playerParameter.spearActionInfo;
		if (hitSpearSpecialAction)
		{
			hitSpearSpActionTimer += Time.get_deltaTime();
			if (hitSpearSpActionTimer > spearActionInfo.rushCancellableTime)
			{
				enableCancelToAttack = false;
			}
		}
		if (isLoopingRush)
		{
			actRushLoopTimer += Time.get_deltaTime();
			if (actRushLoopTimer > spearActionInfo.rushLoopTime)
			{
				isLoopingRush = false;
				SetNextTrigger(0);
			}
			else if (actRushLoopTimer > spearActionInfo.rushCanAvoidTime)
			{
				enableCancelToAvoid = true;
			}
		}
		if (isSpearHundred)
		{
			spearHundredSecFromStart += Time.get_deltaTime();
			spearHundredSecFromLastTap += Time.get_deltaTime();
			if (spearHundredSecFromStart >= spearActionInfo.hundredLoopLimitSec)
			{
				isSpearHundred = false;
				ActAttack(22, true, false, string.Empty);
			}
			else if (spearHundredSecFromLastTap >= spearActionInfo.hundredTapIntervalSec)
			{
				isSpearHundred = false;
				SetNextTrigger(0);
			}
		}
		switch (jumpState)
		{
		case eJumpState.FallWait:
			jumpActionCounter -= Time.get_deltaTime();
			if (jumpActionCounter <= 0f)
			{
				SetNextTrigger(1);
				jumpState = eJumpState.Fall;
			}
			break;
		case eJumpState.Fall:
			jumpFallBodyPosition.y -= spearActionInfo.jumpFallSpeed * Time.get_deltaTime();
			if (jumpFallBodyPosition.y <= 0f)
			{
				OnJumpEnd(_position, false, 0f);
			}
			base.body.get_transform().set_localPosition(jumpFallBodyPosition);
			break;
		case eJumpState.HitStop:
			jumpActionCounter += Time.get_deltaTime();
			if (jumpActionCounter >= spearActionInfo.jumpHitStop)
			{
				jumpActionCounter = 0f;
				SetNextTrigger(0);
				jumpState = eJumpState.Randing;
			}
			break;
		case eJumpState.Randing:
			jumpActionCounter += Time.get_deltaTime();
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
				base.body.get_transform().set_localPosition(jumpFallBodyPosition);
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
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		OnJumpEnd(_position, true, jumpFallBodyPosition.y);
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		jumpFallBodyPosition = dir;
		useGaugeLevel = level;
		SetNextTrigger(0);
		jumpState = eJumpState.Rize;
		ForceClearInBarrier();
		StartWaitingPacket(WAITING_PACKET.PLAYER_JUMP_END, true, 0f);
		if (!object.ReferenceEquals(playerSender, null))
		{
			playerSender.OnJumpRize(dir, level);
		}
	}

	public void OnJumpEnd(Vector3 pos, bool isSuccess, float y)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		if (jumpState == eJumpState.Fall)
		{
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
				base.body.get_transform().set_localPosition(jumpFallBodyPosition);
				SetNextTrigger(2);
				jumpState = eJumpState.Failure;
			}
			EndWaitingPacket(WAITING_PACKET.PLAYER_JUMP_END);
			if (!object.ReferenceEquals(playerSender, null))
			{
				playerSender.OnJumpEnd(pos, isSuccess, y);
			}
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
		if (object.ReferenceEquals(exRushChargeEffect, null))
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
			StartWaitingPacket(WAITING_PACKET.EVOLVE, false, rSec);
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
		base.actionID = (ACTION_ID)36;
		PlayMotion("@evolve_" + evolveWeaponId, -1f);
		evolveCtrl.Start(evolveWeaponId);
		if (playerSender != null)
		{
			playerSender.OnSyncEvolveAction(true);
		}
	}

	private void EndEvolve()
	{
		evolveCtrl.End();
		EndWaitingPacket(WAITING_PACKET.EVOLVE);
		if (playerSender != null)
		{
			playerSender.OnSyncEvolveAction(false);
		}
	}

	private void UpdateEvolve()
	{
		if (base.actionID == (ACTION_ID)37 && evolveSpecialActionSec > 0f)
		{
			evolveSpecialActionSec -= Time.get_deltaTime();
			if (evolveSpecialActionSec <= 0f)
			{
				SetNextTrigger(0);
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
		base.actionID = (ACTION_ID)37;
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

	public void StartFieldBuff(uint fieldBuffId)
	{
		buffParam.ClearFieldBuff();
		if (fieldBuffId != 0 && Singleton<FieldBuffTable>.IsValid() && Singleton<BuffTable>.IsValid())
		{
			FieldBuffTable.FieldBuffData data = Singleton<FieldBuffTable>.I.GetData(fieldBuffId);
			if (!object.ReferenceEquals(data, null))
			{
				for (int i = 0; i < data.buffTableIds.Count; i++)
				{
					BuffTable.BuffData data2 = Singleton<BuffTable>.I.GetData(data.buffTableIds[i]);
					if (!object.ReferenceEquals(data2, null))
					{
						buffParam.StartFieldBuff(fieldBuffId, data2);
					}
				}
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

	public void StartBuffByBuffTableId(uint id, SkillInfo.SkillParam skillParam)
	{
		if (Singleton<BuffTable>.IsValid())
		{
			BuffTable.BuffData data = Singleton<BuffTable>.I.GetData(id);
			if (data != null)
			{
				BuffParam.BuffData buffData = new BuffParam.BuffData();
				buffData.type = data.type;
				buffData.interval = data.interval;
				buffData.valueType = data.valueType;
				buffData.time = data.duration;
				float num = (float)data.value;
				if (skillParam != null && Singleton<GrowSkillItemTable>.IsValid())
				{
					GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(data.growID, skillParam.baseInfo.level);
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
		}
	}

	public bool IsInBarrier()
	{
		return !bulletBarrierObjList.IsNullOrEmpty();
	}

	public unsafe bool IsInAliveBarrier()
	{
		List<BarrierBulletObject> source = bulletBarrierObjList;
		if (_003C_003Ef__am_0024cacheE4 == null)
		{
			_003C_003Ef__am_0024cacheE4 = new Func<BarrierBulletObject, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		return source.Any(_003C_003Ef__am_0024cacheE4);
	}

	private void ForceClearInBarrier()
	{
		if (!bulletBarrierObjList.IsNullOrEmpty())
		{
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

	public void MakeInvincibleByBrokenBarrier()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		hitOffFlag |= HIT_OFF_FLAG.INVICIBLE;
		cancelInvincible = CancelInvincibleByBrokenBarrier();
		this.StartCoroutine(cancelInvincible);
	}

	private IEnumerator CancelInvincibleByBrokenBarrier()
	{
		yield return (object)new WaitForSeconds(playerParameter.barrierBrokenReaction.invincibleDuration);
		hitOffFlag &= ~HIT_OFF_FLAG.INVICIBLE;
	}

	protected override void OnAttackedContinuationStart(AttackedContinuationStatus status)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		BarrierBulletObject componentInParent = status.fromCollider.get_gameObject().GetComponentInParent<BarrierBulletObject>();
		if (componentInParent != null)
		{
			bulletBarrierObjList.Add(componentInParent);
			componentInParent.AddPlayer(this);
			string effectNameInBarrier = componentInParent.GetEffectNameInBarrier();
			Transform val = effectTransTable.Get(effectNameInBarrier);
			if (val == null)
			{
				val = EffectManager.GetEffect(effectNameInBarrier, base.rootNode);
				if (val != null)
				{
					effectTransTable.Add(effectNameInBarrier, val);
				}
			}
		}
	}

	protected override void OnAttackedContinuationEnd(AttackedContinuationStatus status)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		BarrierBulletObject componentInParent = status.fromCollider.get_gameObject().GetComponentInParent<BarrierBulletObject>();
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
	}

	protected override void OnCollisionEnter(Collision collision)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		if ((IsCoopNone() || IsOriginal()) && snatchCtrl.IsMoveLoop())
		{
			int layer = collision.get_gameObject().get_layer();
			if (layer == 11 || layer == 10 || layer == 9 || layer == 17 || layer == 18)
			{
				OnSnatchMoveEnd(0);
			}
		}
	}

	protected override void OnCollisionStay(Collision collision)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		if (collision.get_gameObject().get_layer() == 9 || collision.get_gameObject().get_layer() == 17 || collision.get_gameObject().get_layer() == 18)
		{
			isWallStay = true;
		}
		if ((IsCoopNone() || IsOriginal()) && snatchCtrl.IsMoveLoop())
		{
			int layer = collision.get_gameObject().get_layer();
			if (layer == 11 || layer == 10 || layer == 9 || layer == 17 || layer == 18)
			{
				if (!snatchCtrl.IsMoveLoopStart())
				{
					snatchCtrl.ActivateLoopStart();
				}
				else
				{
					OnSnatchMoveEnd(0);
				}
			}
		}
	}

	public virtual string GetMotionLayerName(ATTACK_MODE _attackMode, SP_ATTACK_TYPE _spAtkType, int _motionId)
	{
		string text = "Base Layer.";
		switch (_attackMode)
		{
		case ATTACK_MODE.ONE_HAND_SWORD:
		{
			InGameSettingsManager.Player.BurstOneHandSwordActionInfo burstOHSInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.burstOHSInfo;
			if (_spAtkType == SP_ATTACK_TYPE.BURST && burstOHSInfo != null)
			{
				bool flag = burstOHSInfo.BaseAtkId <= _motionId && _motionId <= burstOHSInfo.AvoidAttackID;
				bool flag2 = burstOHSInfo.CounterAttackId == _motionId;
				if (flag || flag2)
				{
					text += "BURST.";
				}
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
			break;
		}
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
}
