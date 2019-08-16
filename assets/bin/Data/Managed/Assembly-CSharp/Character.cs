using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Character : StageObject, IAnimEvent
{
	public enum ACTION_ID
	{
		NONE,
		IDLE,
		MOVE,
		ROTATE,
		DAMAGE,
		DEAD,
		ATTACK,
		PARALYZE,
		FREEZE,
		HIDE,
		MOVE_POINT,
		MOVE_LOOKAT,
		POSE,
		MAX
	}

	public enum MOTION_ID
	{
		NONE = 0,
		END = 1,
		IDLE = 2,
		WALK = 3,
		ROTATE_L = 4,
		ROTATE_R = 5,
		DAMAGE = 6,
		DEAD = 7,
		PARALYZE = 8,
		MOVE_SIDE_R = 9,
		MOVE_SIDE_L = 10,
		HIDE = 11,
		HIDE_END = 12,
		MOVE_POINT = 13,
		MOVE_LOOKAT = 14,
		ATTACK_ID_BEGIN = 0xF,
		ATTACK_ID_END = 114,
		MAX = 115,
		ATTACK_ID_NUM = 100
	}

	protected class MotionHashTable : StringKeyTableBase
	{
		public void Add(string key, int value)
		{
			_Add(key, value);
		}

		public object Get(string key)
		{
			return _Get(key);
		}
	}

	[Serializable]
	public class PeriodicSyncActionPositionInfo
	{
		public float applyTime;

		public Vector3 actionPosition = Vector3.get_zero();

		public Vector3 targetPointPos = Vector3.get_zero();

		public bool actionPositionFlag;
	}

	public enum MOVE_TYPE
	{
		NONE,
		VELOCITY,
		SYNC_VELOCITY,
		TO_POSITION,
		HOMING,
		SIDEWAYS
	}

	public enum ROTATE_TYPE
	{
		NONE,
		TO_DIRECTION,
		MOTION_TO_TARGET,
		MOTION_TO_DIRECTION
	}

	public enum VELOCITY_TYPE
	{
		NONE,
		ROOT_MOTION,
		EVENT_MOVE,
		ACT_MOVE
	}

	public enum OBJECT_LIST_TYPE
	{
		DEFAULT,
		STATIC,
		ANIM_EVENT,
		CHANGE_WEAPON,
		NUM
	}

	public enum REACTION_TYPE
	{
		NONE,
		DAMAGE,
		BLOW,
		STUNNED_BLOW,
		STUMBLE,
		FALL_BLOW,
		SHAKE,
		DOWN,
		DEAD,
		GUARD_DAMAGE,
		PARALYZE,
		ANGRY,
		FREEZE,
		COUNTER,
		ELECTRIC_SHOCK,
		INK_SPLASH,
		DIZZY,
		SHADOWSEALING,
		MAD_MODE,
		LIGHT_RING,
		BIND,
		STONE,
		DEAD_REVIVE,
		SOIL_SHOCK,
		CONCUSSION,
		CHARM_BLOW
	}

	public class ReactionInfo
	{
		public REACTION_TYPE reactionType;

		public Vector3 blowForce = Vector3.get_zero();

		public float loopTime;

		public int targetId;

		public int deadReviveCount;
	}

	[Serializable]
	public class DelayReactionInfo
	{
		public REACTION_TYPE type;

		public int targetId;

		public float reactionLoopTime;
	}

	public enum STATE_MOVE_POINT
	{
		NONE,
		INIT,
		ROTATE,
		CHECK,
		FINISH
	}

	protected enum STATE_MOVE_LOOKAT
	{
		NONE,
		INIT,
		MOVE,
		FINISH
	}

	public class PlayMotionParam
	{
		public int MotionID;

		public string MotionLayerName = "Base Layer.";

		public float TransitionTime = -1f;
	}

	public class HealData
	{
		public int healHp;

		public HEAL_TYPE healType;

		public HEAL_EFFECT_TYPE effectType;

		public List<int> applyAbilityTypeList = new List<int>();

		public HealData(int healHp, HEAL_TYPE healType, HEAL_EFFECT_TYPE healEffectType, List<int> applyAbilityTypeList)
		{
			this.healHp = healHp;
			this.healType = healType;
			effectType = healEffectType;
			this.applyAbilityTypeList = applyAbilityTypeList;
		}

		public override string ToString()
		{
			return $"HealData( healHp: {healHp}, healType: {healType}, effectType: {effectType}, applyAbilityTypeList: {applyAbilityTypeList.ToJoinString()}";
		}
	}

	public static readonly string[] motionStateName;

	public static readonly string poseStateName;

	private static Dictionary<string, int> motionHashCaches;

	public const string ANIMATOR_DEF_LAYER_NAME = "Base Layer.";

	protected const string ANIMATOR_NEXT_TRIGGER_NAME = "next";

	private static readonly MotionHashTable motionHash;

	protected string lastAnimTrigger;

	protected List<string> changeTriggerList = new List<string>();

	protected bool periodicSyncActionPositionFlag;

	protected float periodicSyncActionPositionLastTime;

	protected List<PeriodicSyncActionPositionInfo> periodicSyncActionPositionList = new List<PeriodicSyncActionPositionInfo>();

	protected StageObject periodicSyncTarget;

	protected bool enableRootMotion = true;

	protected bool enableEventMove;

	protected Vector3 eventMoveVelocity = Vector3.get_zero();

	protected float eventMoveTimeCount;

	public AnimEventData animEventData;

	protected CharacterStampCtrl stepCtrl;

	[Tooltip("最短移動回転時間（回転始動と終了のスム\u30fcズに関係")]
	public float moveRotateMinimumTime = 0.3f;

	[Tooltip("移動回転最大速度（角度/s")]
	public float moveRotateMaxSpeed = 60f;

	[Tooltip("移動回転最大速度変動掛け率")]
	public float actionMoveRotateMaxSpeedRate = 1f;

	[Tooltip("移動停止範囲")]
	public float moveStopRange = 5f;

	protected float moveSyncTime;

	protected float moveSyncDirection;

	protected float moveSyncDirectionTime;

	protected bool moveSyncEnd;

	protected float moveSyncEndDirection;

	public int moveSyncMotionID;

	protected Vector3 moveBeforePos = Vector3.get_zero();

	protected float moveNowDistance;

	protected float moveMaxDistance;

	[Tooltip("最短回転時間（回転始動と終了のスム\u30fcズに関係")]
	public float rotateMinimumTime = 0.1f;

	[Tooltip("回転最大速度（角度/s")]
	public float rotateMaxSpeed = 120f;

	protected int rotateSign;

	protected float rotateVelocity;

	protected float rootRotationRate = 1f;

	protected int rotateTargetCnt;

	protected bool rotateTargetEnd;

	protected GameObject damegeRemainEffect;

	protected float rotateEventSpeed;

	protected float rotateEventDirection;

	protected bool rotateEventKeep;

	protected bool rotateToTargetFlag;

	protected float rotateToTargetDiffAngle;

	protected bool rotateSafeMode;

	private Vector3 _velocity = Vector3.get_zero();

	public VELOCITY_TYPE velocityType;

	protected float actionMoveRate = 1f;

	protected float rootMotionMoveRate = 1f;

	protected List<DelayReactionInfo> m_reactionDelayList = new List<DelayReactionInfo>();

	protected bool isReactionDelaySet;

	private XorInt _hpMax = 0;

	private XorInt _hp = 0;

	protected int localDamage;

	protected XorInt m_shieldHpMax = 0;

	public XorInt m_shieldHp = 0;

	protected AttackInfo[] attackInfos;

	private XorFloat _damageHealRate;

	private XorFloat _attackWeakRate;

	private XorFloat _attackDownRate;

	private XorFloat _downPowerWeak;

	private XorFloat _downPowerSimpleWeak;

	private XorFloat _elementWeakRate;

	private XorFloat _elementSkillWeakRate;

	private XorFloat _skillWeakRate;

	private XorFloat _healWeakRate;

	private XorFloat _elementSpAttackWeakRate;

	private List<AttackColliderObject> m_exAtkColliderObjectList = new List<AttackColliderObject>();

	protected bool[] objectTypeAutoDelete = new bool[4]
	{
		true,
		false,
		true,
		true
	};

	protected List<List<GameObject>> objectList;

	protected float hitStopTimer = float.MinValue;

	protected AnimEventProcessor animEventProcessor;

	protected bool animUpdatePhysics;

	protected List<string> animatorBoolList = new List<string>();

	protected List<AnimEventCollider> animEventColliderList = new List<AnimEventCollider>();

	protected AttackHitChecker attackHitChecker = new AttackHitChecker();

	protected bool referenceCheckerFlag;

	protected List<string> hideRendererList = new List<string>();

	protected bool isPlayingEndMotion;

	private bool isImmortal;

	public ContinusAttackParam continusAttackParam;

	public List<AnimEventData.EventData> continusAtkEventDataList = new List<AnimEventData.EventData>();

	public BuffParam buffParam;

	protected float buffSyncLastTime;

	public BadStatus badStatusMax = new BadStatus();

	public BadStatus badStatusBase = new BadStatus();

	protected float paralyzeTime;

	public float paralyzeEffectScale = 1f;

	public string paralyzeEffectName = "ef_btl_wyvern_paralyz_01";

	protected Transform paralyzeEffectTrans;

	protected string nowAnimCtrlName;

	protected string nextAnimCtrlName;

	protected int nextMotionHash;

	protected float nextMotionTransitionTime = -1f;

	protected GameObject actionRendererModel;

	protected string actionRendererNodeName;

	protected Transform actionRendererInstance;

	protected float actMotionStartTime = -1f;

	public bool onTheGround = true;

	public bool isUseInvincibleBuff;

	public float actionReceiveDamageRate = 1f;

	public bool isUseInvincibleBadStatusBuff;

	private List<GameObject> hittingIceFloor = new List<GameObject>(10);

	public const float FREEZE_START_NORMALIZED_TIME = 0.1f;

	public const float FREEZE_EFFECT_HEIGHT = 30f;

	public const float FREEZE_EFFECT_SPEED = 5f;

	private Renderer[] m_rendererList;

	private GameObject m_effectFreeze;

	private float m_freezeTimer;

	private float m_freezeHeight;

	private float m_emissionRadius;

	protected bool m_isStopMotionByDebuff;

	public float stopMotionByDebuffNormalizedTime = -1f;

	protected List<ACTION_ID> shadowSealingStackDebuff = new List<ACTION_ID>();

	protected GameObject m_effectElectricShock;

	private const float DEFAULT_MOVE_ANGLE = 45f;

	private const float DEFAULT_MOVE_ANGLE_SPEED_MAX = 20f;

	private float m_moveAngle_deg = 45f;

	private float m_moveAngleSpeed_deg = 20f;

	private float m_movedAngle_deg;

	private float m_diffAngle_deg;

	private int m_moveAngleSign;

	public static readonly Vector3 DEFAULT_MOVE_POINT;

	protected int m_rotateForActMotionId = 4;

	protected float m_rotateForActTime;

	protected float m_rotateForActFinishTime;

	protected Quaternion m_rotateForActStart_Quat = Quaternion.get_identity();

	protected Quaternion m_rotateForActEnd_Quat = Quaternion.get_identity();

	protected Vector3 m_moveLookAtInitTargetDir = Vector3.get_zero();

	protected static StringBuilder stateNameBuilder;

	private bool dbgTimeCountFlag;

	private float dbgTimeCount;

	public override Vector3 _position
	{
		get
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			return (!base.isInitialized) ? base._position : base._rigidbody.get_position();
		}
		set
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Unknown result type (might be due to invalid IL or missing references)
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_0093: Unknown result type (might be due to invalid IL or missing references)
			//IL_0098: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			if (!base.isInitialized)
			{
				base._position = value;
				return;
			}
			bool flag = false;
			if ((base._rigidbody.get_constraints() & 2) != 0)
			{
				Vector3 position = base._rigidbody.get_position();
				if (position.x != value.x)
				{
					flag = true;
				}
			}
			if ((base._rigidbody.get_constraints() & 4) != 0)
			{
				Vector3 position2 = base._rigidbody.get_position();
				if (position2.y != value.y)
				{
					flag = true;
				}
			}
			if ((base._rigidbody.get_constraints() & 8) != 0)
			{
				Vector3 position3 = base._rigidbody.get_position();
				if (position3.z != value.z)
				{
					flag = true;
				}
			}
			if (!base._rigidbody.get_gameObject().get_activeInHierarchy())
			{
				flag = true;
			}
			base._rigidbody.set_position(value);
			if (flag)
			{
				base._transform.set_position(value);
			}
		}
	}

	public override Quaternion _rotation
	{
		get
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			return (!base.isInitialized) ? base._rotation : base._rigidbody.get_rotation();
		}
		set
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			if (!base.isInitialized)
			{
				base._rotation = value;
				return;
			}
			bool flag = false;
			if (!base._rigidbody.get_gameObject().get_activeInHierarchy())
			{
				flag = true;
			}
			base._rigidbody.set_rotation(value);
			if (flag)
			{
				base._transform.set_rotation(value);
			}
		}
	}

	public override Vector3 _forward
	{
		get
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			return (!base.isInitialized) ? base._forward : (base._rigidbody.get_rotation() * Vector3.get_forward());
		}
		set
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			if (base.isInitialized)
			{
				base._rigidbody.set_rotation(Quaternion.LookRotation(value));
			}
			else
			{
				base._forward = value;
			}
		}
	}

	public override Vector3 _right
	{
		get
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			return (!base.isInitialized) ? base._right : (base._rigidbody.get_rotation() * Vector3.get_right());
		}
		set
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (base.isInitialized)
			{
				base._rigidbody.set_rotation(Quaternion.FromToRotation(Vector3.get_right(), value));
			}
			else
			{
				base._right = value;
			}
		}
	}

	public override Vector3 _up
	{
		get
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			return (!base.isInitialized) ? base._up : (base._rigidbody.get_rotation() * Vector3.get_up());
		}
		set
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (base.isInitialized)
			{
				base._rigidbody.set_rotation(Quaternion.FromToRotation(Vector3.get_up(), value));
			}
			else
			{
				base._up = value;
			}
		}
	}

	public InGameSettingsManager.Character charaParameter
	{
		get;
		private set;
	}

	public string charaName
	{
		get;
		set;
	}

	public string fullName
	{
		get;
		set;
	}

	public Animator animator
	{
		get;
		protected set;
	}

	public Transform body
	{
		get;
		set;
	}

	public Transform rootNode
	{
		get;
		protected set;
	}

	public ACTION_ID actionID
	{
		get;
		protected set;
	}

	public ACTION_ID lastActionID
	{
		get;
		protected set;
	}

	public int attackID
	{
		get;
		protected set;
	}

	public bool isControllable
	{
		get;
		protected set;
	}

	public bool isDead
	{
		get;
		protected set;
	}

	public StageObject actionTarget
	{
		get;
		protected set;
	}

	public Vector3 actionPosition
	{
		get;
		protected set;
	}

	public bool actionPositionFlag
	{
		get;
		protected set;
	}

	public bool actionPositionThroughFlag
	{
		get;
		protected set;
	}

	public Vector3 targetPointPos
	{
		get;
		protected set;
	}

	public StageObject attackStartTarget
	{
		get;
		protected set;
	}

	public bool actionPositionWaitSync
	{
		get;
		protected set;
	}

	public string actionPositionWaitTrigger
	{
		get;
		protected set;
	}

	public bool directionWaitSync
	{
		get;
		protected set;
	}

	public string directionWaitTrigger
	{
		get;
		protected set;
	}

	public List<Character> periodicSyncOwnerList
	{
		get;
		protected set;
	}

	public Vector3 lerpRotateVec
	{
		get;
		protected set;
	}

	public MOVE_TYPE moveType
	{
		get;
		protected set;
	}

	public Vector3 moveTargetPos
	{
		get;
		protected set;
	}

	public float moveSyncSpeed
	{
		get;
		protected set;
	}

	public ROTATE_TYPE rotateType
	{
		get;
		protected set;
	}

	public float rotateDirection
	{
		get;
		protected set;
	}

	public bool rotateDisableMotion
	{
		get;
		set;
	}

	public Vector3 externalVelocity
	{
		get;
		protected set;
	}

	public Vector3 addForce
	{
		get;
		protected set;
	}

	public bool enableAddForce
	{
		get;
		protected set;
	}

	public Vector3 addForceBeforePos
	{
		get;
		protected set;
	}

	public bool waitAddForce
	{
		get;
		protected set;
	}

	public bool enableMotionCancel
	{
		get;
		protected set;
	}

	public bool enableMoveSuppress
	{
		get;
		protected set;
	}

	public bool enableReactionDelay
	{
		get;
		protected set;
	}

	public int hpMax
	{
		get
		{
			return _hpMax;
		}
		set
		{
			_hpMax = value;
		}
	}

	public int hp
	{
		get
		{
			return _hp;
		}
		set
		{
			_hp = value;
		}
	}

	public int hpShow
	{
		get
		{
			if (isLocalDamageApply)
			{
				int num = hp - localDamage;
				if (isDead)
				{
					if (num < 0)
					{
						num = 0;
					}
				}
				else if (num < 1)
				{
					num = 1;
				}
				return num;
			}
			return hp;
		}
	}

	public bool isLocalDamageApply
	{
		get;
		protected set;
	}

	public AtkAttribute ShieldTolerance
	{
		get;
		set;
	}

	public XorInt ShieldHpMax
	{
		get
		{
			return m_shieldHpMax;
		}
		set
		{
			m_shieldHpMax = value;
		}
	}

	public XorInt ShieldHp
	{
		get
		{
			return m_shieldHp;
		}
		set
		{
			if (IsValidShield() && (int)value <= 0)
			{
				ActShieldBreak();
			}
			m_shieldHp = value;
		}
	}

	public AtkAttribute attack
	{
		get;
		protected set;
	}

	public AtkAttribute tolerance
	{
		get;
		protected set;
	}

	public AtkAttribute defense
	{
		get;
		protected set;
	}

	public float damageHealRate
	{
		get
		{
			return _damageHealRate;
		}
		protected set
		{
			_damageHealRate = value;
		}
	}

	public float attackWeakRate
	{
		get
		{
			return _attackWeakRate;
		}
		protected set
		{
			_attackWeakRate = value;
		}
	}

	public float attackDownRate
	{
		get
		{
			return _attackDownRate;
		}
		protected set
		{
			_attackDownRate = value;
		}
	}

	public float downPowerWeak
	{
		get
		{
			return _downPowerWeak;
		}
		protected set
		{
			_downPowerWeak = value;
		}
	}

	public float downPowerSimpleWeak
	{
		get
		{
			return _downPowerSimpleWeak;
		}
		protected set
		{
			_downPowerSimpleWeak = value;
		}
	}

	public float elementWeakRate
	{
		get
		{
			return _elementWeakRate;
		}
		protected set
		{
			_elementWeakRate = value;
		}
	}

	public float elementSkillWeakRate
	{
		get
		{
			return _elementSkillWeakRate;
		}
		protected set
		{
			_elementSkillWeakRate = value;
		}
	}

	public float skillWeakRate
	{
		get
		{
			return _skillWeakRate;
		}
		protected set
		{
			_skillWeakRate = value;
		}
	}

	public float healWeakRate
	{
		get
		{
			return _healWeakRate;
		}
		protected set
		{
			_healWeakRate = value;
		}
	}

	public float elementSpAttackWeakRate
	{
		get
		{
			return _elementSpAttackWeakRate;
		}
		protected set
		{
			_elementSpAttackWeakRate = value;
		}
	}

	public CharacterPacketReceiver characterReceiver => (CharacterPacketReceiver)base.packetReceiver;

	public CharacterPacketSender characterSender => (CharacterPacketSender)base.packetSender;

	public BadStatus atkBadStatus
	{
		get;
		protected set;
	}

	public BadStatus badStatusTotal
	{
		get;
		protected set;
	}

	public EffectPlayProcessor effectPlayProcessor
	{
		get;
		set;
	}

	public bool isSetAppearPos
	{
		get;
		protected set;
	}

	public Vector3 appearPos
	{
		get;
		protected set;
	}

	public AttackTrackingTarget TrackingTargetBullet
	{
		get;
		set;
	}

	public int SyncRandomSeed
	{
		get;
		set;
	}

	public StringKeyTable<BulletData> cachedBulletDataTable
	{
		get;
		set;
	}

	protected Renderer[] _rendererArray
	{
		get
		{
			return m_rendererList;
		}
		set
		{
			m_rendererList = value;
		}
	}

	public bool isPause
	{
		get;
		private set;
	}

	public float moveAngle_deg
	{
		get
		{
			return m_moveAngle_deg;
		}
		set
		{
			m_moveAngle_deg = value;
		}
	}

	public float moveAngleSpeed_deg
	{
		get
		{
			return m_moveAngleSpeed_deg;
		}
		set
		{
			m_moveAngleSpeed_deg = value;
		}
	}

	public Vector3 movePointPos
	{
		get;
		set;
	}

	protected STATE_MOVE_POINT stateMovePoint
	{
		get;
		private set;
	}

	public Vector3 moveLookAtPos
	{
		get;
		set;
	}

	public float moveLookAtAngle
	{
		get;
		set;
	}

	protected STATE_MOVE_LOOKAT stateMoveLookAt
	{
		get;
		private set;
	}

	static Character()
	{
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		motionStateName = new string[16]
		{
			string.Empty,
			"end",
			"idle",
			"walk",
			"rotate_l",
			"rotate_r",
			"damage",
			"dead",
			"paralyze",
			"move_side_r",
			"move_side_l",
			"hide",
			"hide_end",
			"move_point",
			"move_lookat",
			"attack_{0:00}"
		};
		poseStateName = "pose";
		motionHashCaches = new Dictionary<string, int>();
		DEFAULT_MOVE_POINT = Vector3.get_zero();
		stateNameBuilder = new StringBuilder(1024);
		motionHash = new MotionHashTable();
	}

	public Character()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		base.objectType = OBJECT_TYPE.CHARACTER;
		animator = null;
		body = null;
		actionID = ACTION_ID.IDLE;
		lastActionID = ACTION_ID.NONE;
		attackID = 0;
		isControllable = true;
		periodicSyncOwnerList = new List<Character>();
		lerpRotateVec = Vector3.get_zero();
		moveType = MOVE_TYPE.NONE;
		moveTargetPos = Vector3.get_zero();
		rotateType = ROTATE_TYPE.NONE;
		rotateDirection = 0f;
		rootRotationRate = 1f;
		externalVelocity = Vector3.get_zero();
		addForce = Vector3.get_zero();
		enableAddForce = false;
		addForceBeforePos = Vector3.get_zero();
		waitAddForce = false;
		enableMotionCancel = false;
		enableMoveSuppress = false;
		actionPositionFlag = false;
		actionPositionWaitSync = false;
		actionPositionWaitTrigger = null;
		directionWaitSync = false;
		directionWaitTrigger = null;
		hpMax = 0;
		hp = hpMax;
		attack = new AtkAttribute();
		tolerance = new AtkAttribute();
		defense = new AtkAttribute();
		damageHealRate = 1f;
		downPowerWeak = 0f;
		downPowerSimpleWeak = 0f;
		actionReceiveDamageRate = 1f;
		actionMoveRotateMaxSpeedRate = 1f;
		objectList = new List<List<GameObject>>();
		for (int i = 0; i < 4; i++)
		{
			objectList.Add(new List<GameObject>());
		}
		atkBadStatus = new BadStatus();
		badStatusTotal = new BadStatus();
		badStatusMax = new BadStatus(1f);
		buffParam = new BuffParam(this);
		continusAttackParam = new ContinusAttackParam(this);
		effectPlayProcessor = null;
		isSetAppearPos = false;
		isUseInvincibleBuff = false;
		isUseInvincibleBadStatusBuff = false;
		cachedBulletDataTable = new StringKeyTable<BulletData>();
	}

	public override void _LookAt(Vector3 pos)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		if (base.isInitialized)
		{
			base._rigidbody.set_rotation(Quaternion.LookRotation(pos - _position));
		}
		else
		{
			base._LookAt(pos);
		}
	}

	public void SetVelocity(Vector3 set_vec, VELOCITY_TYPE type = VELOCITY_TYPE.NONE)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		_velocity = set_vec;
		velocityType = type;
	}

	public Vector3 GetVelocity()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return _velocity;
	}

	public virtual void ActShieldBreak()
	{
	}

	public virtual float GetEffectScaleDependValue()
	{
		return 1f;
	}

	public void SetImmortal()
	{
		isImmortal = true;
	}

	public bool IsAbleToInvincibleBuff()
	{
		if (isDead)
		{
			return false;
		}
		if (IsValidBuff(BuffParam.BUFFTYPE.AUTO_REVIVE))
		{
			return false;
		}
		if (IsValidBuff(BuffParam.BUFFTYPE.INVINCIBLE_BADSTATUS))
		{
			return false;
		}
		if (IsValidBuff(BuffParam.BUFFTYPE.INVINCIBLECOUNT))
		{
			return false;
		}
		return !isUseInvincibleBuff;
	}

	public bool IsAbleToInvincibleBadStatusBuff()
	{
		if (isDead)
		{
			return false;
		}
		if (IsValidBuff(BuffParam.BUFFTYPE.AUTO_REVIVE))
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
		return !isUseInvincibleBadStatusBuff;
	}

	public override void LookAt(Vector3 pos, bool isBlindEnable = false)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		if (!isBlindEnable || !IsValidBuffBlind())
		{
			Vector3 position = _position;
			pos.y = position.y;
			_LookAt(pos);
		}
	}

	protected override void Awake()
	{
		base.Awake();
		charaParameter = MonoBehaviourSingleton<InGameSettingsManager>.I.character;
	}

	protected override void Clear()
	{
		base.Clear();
		animator = null;
		animEventProcessor = null;
		int i = 0;
		for (int count = objectList.Count; i < count; i++)
		{
			int num = 0;
			while (num < objectList[i].Count)
			{
				GameObject val = objectList[i][num];
				if (val.get_transform().get_parent() == base._transform)
				{
					num++;
				}
				else
				{
					objectList[i].RemoveAt(num);
				}
			}
		}
		animatorBoolList.Clear();
		changeTriggerList.Clear();
		animEventColliderList.ForEach(delegate(AnimEventCollider o)
		{
			o.Destroy();
		});
		animEventColliderList.Clear();
	}

	public override void OnLoadComplete()
	{
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		base.OnLoadComplete();
		rootNode = Utility.Find(base._transform, "Root");
		if (base._collider != null && base._rigidbody == null)
		{
			base._rigidbody = this.get_gameObject().AddComponent<Rigidbody>();
		}
		if (!object.ReferenceEquals(body, null))
		{
			animator = body.GetComponent<Animator>();
		}
		if (object.ReferenceEquals(animator, null))
		{
			animator = this.get_gameObject().GetComponentInChildren<Animator>();
		}
		if (animator != null)
		{
			animator.set_applyRootMotion(true);
			animator.set_cullingMode(0);
			animator.Update(0f);
			if (animUpdatePhysics)
			{
				animator.set_updateMode(1);
			}
			else
			{
				animator.set_updateMode(0);
			}
			Transform transform = animator.get_transform();
			if (transform != base._transform)
			{
				base._transform.set_localScale(transform.get_localScale());
				transform.set_localScale(Vector3.get_one());
			}
		}
		nowAnimCtrlName = null;
		nextAnimCtrlName = null;
		nextMotionHash = 0;
		nextMotionTransitionTime = -1f;
		stepCtrl = this.get_gameObject().GetComponentInChildren<CharacterStampCtrl>();
		if (animEventData != null && animator != null)
		{
			animEventProcessor = new AnimEventProcessor(animEventData, animator, this);
		}
		else
		{
			AnimEventComponent component = this.get_gameObject().GetComponent<AnimEventComponent>();
			if (component != null && component.get_enabled() && component.animEventData != null)
			{
				animEventProcessor = new AnimEventProcessor(component.animEventData, animator, this);
				Object.Destroy(component);
			}
		}
		m_rendererList = this.get_transform().GetComponentsInChildren<Renderer>(true);
	}

	protected override void Initialize()
	{
		base.Initialize();
		if (base.controller != null)
		{
			base.controller.OnCharacterInitialized();
		}
	}

	protected override void Update()
	{
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		base.Update();
		UpdateAction();
		if (hitStopTimer >= 0f)
		{
			hitStopTimer -= Time.get_deltaTime();
			if (hitStopTimer <= 0f)
			{
				SetHitStop(-1f);
			}
		}
		continusAttackParam.Update();
		buffParam.UpdateConditionsAbility();
		buffParam.Update();
		if (IsOriginal() && buffSyncLastTime != 0f && Time.get_time() > buffSyncLastTime + charaParameter.buffSyncUpdateInterval)
		{
			SendBuffSync();
		}
		if (lerpRotateVec != Vector3.get_zero())
		{
			Quaternion val = Quaternion.LookRotation(lerpRotateVec);
			float num = Mathf.Abs(Vector3.Angle(_forward, lerpRotateVec));
			float num2 = moveRotateMaxSpeed * actionMoveRotateMaxSpeedRate * Time.get_deltaTime() / num;
			if (num2 > 1f)
			{
				lerpRotateVec = Vector3.get_zero();
				num2 = 1f;
			}
			_rotation = Quaternion.Lerp(_rotation, val, num2);
		}
		UpdateReactionDelay();
	}

	protected override void FixedUpdate()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		if (rotateToTargetFlag)
		{
			if (actionPositionFlag)
			{
				Vector3 val = actionPosition - _position;
				val.y = 0f;
				if (val.get_magnitude() < 0.1f)
				{
					val = _forward;
				}
				else if (rotateSafeMode)
				{
					float num = Vector3.Angle(-_forward, val);
					if (num < 1f)
					{
						val = _forward;
					}
				}
				Quaternion val2 = Quaternion.LookRotation(val);
				Vector3 eulerAngles = val2.get_eulerAngles();
				rotateEventDirection = eulerAngles.y + rotateToTargetDiffAngle;
				if (rotateEventSpeed <= 0f)
				{
					_rotation = Quaternion.AngleAxis(rotateEventDirection, Vector3.get_up());
				}
			}
			else
			{
				Quaternion rotation = _rotation;
				Vector3 eulerAngles2 = rotation.get_eulerAngles();
				rotateEventDirection = eulerAngles2.y;
			}
			if (!periodicSyncActionPositionFlag)
			{
				rotateToTargetFlag = false;
			}
		}
		if (rotateEventKeep)
		{
			if (attackStartTarget != null)
			{
				Vector3 forward = _forward;
				forward.y = 0f;
				forward.Normalize();
				Vector3 val3 = attackStartTarget._position - _position;
				val3.y = 0f;
				Vector3 val4 = Vector3.Cross(forward, val3);
				int num2 = (val4.y >= 0f) ? 1 : (-1);
				float num3 = Vector3.Angle(forward, val3);
				Quaternion rotation2 = _rotation;
				Vector3 eulerAngles3 = rotation2.get_eulerAngles();
				float num4 = num3;
				if (rotateEventSpeed > 0f)
				{
					num4 = rotateEventSpeed * Time.get_deltaTime();
					if (num3 <= num4)
					{
						num4 = num3;
					}
				}
				_rotation = Quaternion.Euler(eulerAngles3.x, eulerAngles3.y + (float)num2 * num4, eulerAngles3.z);
			}
		}
		else if (rotateEventSpeed != 0f)
		{
			Vector3 forward2 = _forward;
			forward2.y = 0f;
			forward2.Normalize();
			Vector3 val5 = Quaternion.AngleAxis(rotateEventDirection, Vector3.get_up()) * Vector3.get_forward();
			Vector3 val6 = Vector3.Cross(forward2, val5);
			int num5 = (val6.y >= 0f) ? 1 : (-1);
			float num6 = Vector3.Angle(forward2, val5);
			Quaternion rotation3 = _rotation;
			Vector3 eulerAngles4 = rotation3.get_eulerAngles();
			float num7 = rotateEventSpeed * Time.get_deltaTime();
			if (num6 <= num7)
			{
				num7 = num6;
				if (!rotateToTargetFlag)
				{
					rotateEventSpeed = 0f;
					rotateEventDirection = 0f;
				}
			}
			_rotation = Quaternion.Euler(eulerAngles4.x, eulerAngles4.y + (float)num5 * num7, eulerAngles4.z);
		}
		if ((IsCoopNone() || IsOriginal()) && periodicSyncActionPositionFlag)
		{
			if (periodicSyncTarget != actionTarget)
			{
				SetPeriodicSyncTarget(actionTarget);
			}
			float actMotionTime = GetActMotionTime();
			float num8 = actMotionTime - periodicSyncActionPositionLastTime;
			if (num8 >= charaParameter.periodicSyncActionPositionCheckTime)
			{
				Vector3 targetPosition = GetTargetPosition(actionTarget);
				bool flag = actionTarget != null;
				if (targetPosition != actionPosition || flag != actionPositionFlag)
				{
					PeriodicSyncActionPositionInfo periodicSyncActionPositionInfo = new PeriodicSyncActionPositionInfo();
					periodicSyncActionPositionInfo.applyTime = actMotionTime + charaParameter.periodicSyncActionPositionApplyTime;
					if (actionTarget != null)
					{
						periodicSyncActionPositionInfo.actionPosition = GetTargetPosition(actionTarget);
						periodicSyncActionPositionInfo.actionPositionFlag = true;
						GetTargetPos(out periodicSyncActionPositionInfo.targetPointPos);
					}
					AddPeriodicSyncActionPosition(periodicSyncActionPositionInfo);
					periodicSyncActionPositionLastTime = actMotionTime + charaParameter.periodicSyncActionPositionApplyTime;
				}
			}
		}
		if (periodicSyncActionPositionList.Count > 0)
		{
			int num9 = 0;
			while (num9 < periodicSyncActionPositionList.Count)
			{
				float actMotionTime2 = GetActMotionTime();
				PeriodicSyncActionPositionInfo periodicSyncActionPositionInfo2 = periodicSyncActionPositionList[num9];
				if (actMotionTime2 >= periodicSyncActionPositionInfo2.applyTime)
				{
					SetActionPosition(periodicSyncActionPositionInfo2.actionPosition, periodicSyncActionPositionInfo2.actionPositionFlag);
					targetPointPos = periodicSyncActionPositionInfo2.targetPointPos;
					periodicSyncActionPositionList.RemoveAt(num9);
				}
				else
				{
					num9++;
				}
			}
		}
		if (eventMoveTimeCount > 0f)
		{
			eventMoveTimeCount -= Time.get_deltaTime();
			if (eventMoveTimeCount <= 0f)
			{
				eventMoveTimeCount = 0f;
				enableEventMove = false;
				enableAddForce = false;
				SetVelocity(Vector3.get_zero());
				eventMoveVelocity = Vector3.get_zero();
			}
		}
		if (enableEventMove)
		{
			SetVelocity(Quaternion.LookRotation(GetTransformForward()) * eventMoveVelocity, VELOCITY_TYPE.EVENT_MOVE);
		}
		base.FixedUpdate();
		if (animEventProcessor != null)
		{
			animEventProcessor.Update();
		}
		FixedUpdatePhysics();
		UpdateNextMotion();
	}

	protected virtual void FixedUpdatePhysics()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		if (!base.isInitialized)
		{
			return;
		}
		if (enableAddForce)
		{
			Vector3 addForceBeforePos = this.addForceBeforePos;
			addForceBeforePos.y = 0.1f;
			Vector3 position = _position;
			position.y = 0.1f;
			if (addForceBeforePos != position)
			{
				Vector3 val = position - addForceBeforePos;
				if (Physics.Raycast(addForceBeforePos, val, val.get_magnitude(), 393728))
				{
					Vector3 position2 = _position;
					addForceBeforePos.y = position2.y;
					_position = addForceBeforePos;
				}
				Vector3 position3 = _position;
				position3.y = 0f;
				this.addForceBeforePos = position3;
			}
		}
		else if (!IsHitStop())
		{
			float num = 1f;
			if (enableMoveSuppress && actionPositionFlag && !actionPositionThroughFlag)
			{
				Vector3 val2 = actionPosition - _position;
				val2.y = 0f;
				if (val2.get_magnitude() <= charaParameter.moveSuppressLength)
				{
					num = charaParameter.moveSuppressRate;
				}
			}
			Vector3 velocity = GetVelocity() * num * actionMoveRate;
			Vector3 velocity2 = base._rigidbody.get_velocity();
			velocity.y = velocity2.y;
			base._rigidbody.set_velocity(velocity);
		}
		else
		{
			base._rigidbody.set_velocity(Vector3.get_zero());
		}
		Vector3 position4 = _position;
		float height = StageManager.GetHeight(position4);
		if (!waitAddForce)
		{
			if (this.addForce != Vector3.get_zero())
			{
				base._rigidbody.set_velocity(Vector3.get_zero());
				base._rigidbody.AddForce(this.addForce * (0.02f / Time.get_fixedDeltaTime()));
				Vector3 addForce = this.addForce;
				if (addForce.y > 0f)
				{
					base._rigidbody.set_constraints(base._rigidbody.get_constraints() & -5);
				}
				this.addForce = Vector3.get_zero();
				enableAddForce = true;
				Vector3 position5 = _position;
				position5.y = 0f;
				this.addForceBeforePos = position5;
			}
			else if ((base._rigidbody.get_constraints() & 4) == 0 && position4.y <= height + 0.03f)
			{
				Vector3 velocity3 = base._rigidbody.get_velocity();
				if (velocity3.y <= 0f)
				{
					base._rigidbody.set_constraints(base._rigidbody.get_constraints() | 4);
					Vector3 velocity4 = base._rigidbody.get_velocity();
					velocity4.y = 0f;
					base._rigidbody.set_velocity(velocity4);
					enableAddForce = false;
				}
			}
		}
		Vector3 externalVelocity = this.externalVelocity;
		externalVelocity.y = 0f;
		Rigidbody rigidbody = base._rigidbody;
		rigidbody.set_velocity(rigidbody.get_velocity() + externalVelocity);
		this.externalVelocity = Vector3.get_zero();
		if (!onTheGround)
		{
			return;
		}
		if ((base._rigidbody.get_constraints() & 4) != 0)
		{
			if (Mathf.Abs(position4.y - height) > 0.01f)
			{
				position4.y = height;
				_position = position4;
			}
		}
		else if (position4.y < height)
		{
			position4.y = height;
			_position = position4;
		}
	}

	protected void UpdateNextMotion()
	{
		if (animEventProcessor == null || nextMotionHash == 0 || !(animator != null))
		{
			return;
		}
		hitOffFlag &= ~HIT_OFF_FLAG.PLAY_MOTION;
		string text = nextAnimCtrlName;
		int num = nextMotionHash;
		float motionTransitionTime = nextMotionTransitionTime;
		if (motionTransitionTime < 0f)
		{
			motionTransitionTime = charaParameter.motionTransitionTime;
		}
		nextAnimCtrlName = null;
		nextMotionHash = 0;
		nextMotionTransitionTime = -1f;
		if (text != nowAnimCtrlName)
		{
			RuntimeAnimatorController animCtrl = GetAnimCtrl(text);
			AnimEventData animEvent = GetAnimEvent(text);
			if (animCtrl != null && animEvent != null)
			{
				animEventProcessor.ChangeAnimCtrl(animCtrl, animEvent);
				nowAnimCtrlName = text;
			}
			else
			{
				Log.Error(LOG.INGAME, "Character.UpdateNextMotion() anim_ctrl or anim_event is null. ctrlName = {0}", text);
			}
		}
		if (charaName == "Hellish Zaark" && num == 423958061)
		{
			Debug.Log((object)("temp hash: " + num + " " + nowAnimCtrlName));
		}
		animEventProcessor.CrossFade(num, motionTransitionTime);
		UpdateAnimatorSpeed();
		if (actMotionStartTime < 0f)
		{
			actMotionStartTime = Time.get_time();
		}
	}

	public override void OnDetachedObject(StageObject stage_object)
	{
		base.OnDetachedObject(stage_object);
		if (actionTarget == stage_object)
		{
			SetActionTarget(null, send: false);
		}
		if (attackStartTarget == stage_object)
		{
			attackStartTarget = null;
		}
		if (periodicSyncTarget == stage_object)
		{
			periodicSyncTarget = null;
		}
		int num = periodicSyncOwnerList.IndexOf(stage_object as Character);
		if (num >= 0)
		{
			periodicSyncOwnerList.RemoveAt(num);
		}
		if (base.controller != null)
		{
			base.controller.OnDetachedObject(stage_object);
		}
	}

	public override void OnAnimatorMove()
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		if (!(animator != null) || !animator.get_applyRootMotion() || !enableRootMotion || enableEventMove || enableAddForce)
		{
			return;
		}
		float num = (!(Time.get_deltaTime() > 0f)) ? 0f : (1f / Time.get_deltaTime());
		Vector3 val = animator.get_deltaPosition();
		val.x *= num;
		val.z *= num;
		val *= rootMotionMoveRate;
		if (lerpRotateVec != Vector3.get_zero())
		{
			SetVelocity(Quaternion.FromToRotation(_forward, lerpRotateVec) * val, VELOCITY_TYPE.ROOT_MOTION);
		}
		else
		{
			SetVelocity(val, VELOCITY_TYPE.ROOT_MOTION);
		}
		if (animator.get_deltaRotation() != Quaternion.get_identity())
		{
			if (rootRotationRate == 1f)
			{
				_rotation *= animator.get_deltaRotation();
			}
			else
			{
				_rotation = Quaternion.Lerp(_rotation, _rotation * animator.get_deltaRotation(), rootRotationRate);
			}
		}
	}

	public virtual void SetAnimUpdatePhysics(bool enable)
	{
		animUpdatePhysics = enable;
		if (animator != null)
		{
			if (animUpdatePhysics)
			{
				animator.set_updateMode(1);
			}
			else
			{
				animator.set_updateMode(0);
			}
		}
	}

	public virtual void SetActionTarget(StageObject target, bool send = true)
	{
		bool flag = false;
		if (actionTarget != target)
		{
			flag = true;
		}
		actionTarget = target;
		if (send && flag && characterSender != null)
		{
			characterSender.OnSetActionTarget(target);
		}
	}

	public void SetActionPosition(Vector3 position, bool flag)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (!flag || IsValidBuffBlind())
		{
			position = _forward * 3f;
			flag = false;
		}
		actionPosition = position;
		actionPositionFlag = flag;
	}

	public virtual void UpdateActionPosition(string trigger)
	{
		if (IsCoopNone() || IsOriginal())
		{
			SetAttackActionPosition();
		}
		SetChangeTrigger(trigger);
		EndWaitingPacket(WAITING_PACKET.CHARACTER_UPDATE_ACTION_POSITION);
		actionPositionWaitSync = false;
		actionPositionWaitTrigger = null;
		if (characterSender != null)
		{
			characterSender.OnUpdateActionPosition(trigger);
		}
	}

	public virtual void UpdateDirection(string trigger)
	{
		SetChangeTrigger(trigger);
		EndWaitingPacket(WAITING_PACKET.CHARACTER_UPDATE_DIRECTION);
		directionWaitSync = false;
		directionWaitTrigger = null;
		if (characterSender != null)
		{
			characterSender.OnUpdateDirection(trigger);
		}
	}

	public void AddPeriodicSyncActionPosition(PeriodicSyncActionPositionInfo info)
	{
		if (info != null)
		{
			int num = 0;
			int i = 0;
			for (int count = periodicSyncActionPositionList.Count; i < count && !(info.applyTime < periodicSyncActionPositionList[i].applyTime); i++)
			{
				num++;
			}
			periodicSyncActionPositionList.Insert(num, info);
			if (characterSender != null)
			{
				characterSender.OnPeriodicSyncActionPosition(info);
			}
		}
	}

	public void SetHitStop(float time)
	{
		if (time < 0f)
		{
			time = float.MinValue;
		}
		bool flag = time != float.MinValue;
		if (flag != (hitStopTimer != float.MinValue))
		{
			int i = 0;
			for (int count = objectList.Count; i < count; i++)
			{
				List<GameObject> list = objectList[i];
				int j = 0;
				for (int count2 = list.Count; j < count2; j++)
				{
					list[j].GetComponentsInChildren<Trail>(Temporary.trailList);
					int k = 0;
					for (int count3 = Temporary.trailList.Count; k < count3; k++)
					{
						Temporary.trailList[k].pause = flag;
					}
					Temporary.trailList.Clear();
				}
			}
		}
		if (actMotionStartTime >= 0f && time > 0f)
		{
			actMotionStartTime += time;
		}
		hitStopTimer = time;
		UpdateAnimatorSpeed();
	}

	public bool IsHitStop()
	{
		return hitStopTimer > 0f;
	}

	public void setPause(bool pause)
	{
		isPause = pause;
		UpdateAnimatorSpeed();
	}

	public void setPauseWithAnim(bool pause)
	{
		if (pause)
		{
			ActIdle();
		}
	}

	protected virtual float GetAnimatorSpeed()
	{
		if (IsHitStop())
		{
			return 0f;
		}
		if (isPause)
		{
			return 0f;
		}
		switch (actionID)
		{
		case ACTION_ID.ATTACK:
			return buffParam.GetAtkSpeed();
		case ACTION_ID.MOVE:
			return buffParam.GetMoveSpeed();
		default:
			return 1f;
		}
	}

	public void UpdateAnimatorSpeed()
	{
		if (animator != null)
		{
			animator.set_speed(GetAnimatorSpeed());
		}
	}

	public virtual float GetActMotionTime()
	{
		float result = 0f;
		if (actMotionStartTime >= 0f)
		{
			result = Time.get_time() - actMotionStartTime;
		}
		return result;
	}

	public virtual bool IsChangeableAction(ACTION_ID action_id)
	{
		if (base.isLoading)
		{
			return false;
		}
		if (!isControllable && !enableMotionCancel)
		{
			return false;
		}
		return true;
	}

	public virtual void OnActReaction()
	{
		if (base.controller != null)
		{
			base.controller.OnActReaction();
		}
	}

	public virtual void ActIdle(bool is_sync = false, float transitionTime = -1f)
	{
		bool flag = false;
		ACTION_ID lastActionID = this.lastActionID;
		if (actionID == ACTION_ID.IDLE)
		{
			flag = true;
		}
		EndAction();
		if (flag)
		{
			this.lastActionID = lastActionID;
		}
		actionID = ACTION_ID.IDLE;
		if (!IsPlayingMotion(2))
		{
			PlayMotion(2, transitionTime);
		}
		isControllable = true;
		if (characterSender != null)
		{
			characterSender.OnActIdle(is_sync);
		}
	}

	public virtual void SafeActIdle()
	{
		if (!base.isLoading && !isDead && CanSafeActIdle())
		{
			ActIdle();
		}
	}

	protected virtual bool CanSafeActIdle()
	{
		return true;
	}

	public void SetLerpRotation(Vector3 velocity)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		velocity.y = 0f;
		lerpRotateVec = velocity;
	}

	public bool IsArrivalPosition(Vector3 pos, float margin = 0f)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = pos - _position;
		val.y = 0f;
		return val.get_magnitude() < moveStopRange + margin;
	}

	public bool IsHittingIceFloor()
	{
		return hittingIceFloor.Count > 0 && !isDead;
	}

	public void OnHitEnterIceFloor(GameObject iceFloor)
	{
		if (!hittingIceFloor.Contains(iceFloor))
		{
			hittingIceFloor.Add(iceFloor);
		}
	}

	public void OnHitExitIceFloor(GameObject iceFloor)
	{
		hittingIceFloor.Remove(iceFloor);
	}

	public void ActMoveInertia(ref Vector3 slideVerocity)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		SetVelocity(slideVerocity, VELOCITY_TYPE.ACT_MOVE);
	}

	public virtual void ActMoveVelocity(Vector3 velocity_, float sync_speed, MOTION_ID motion_id = MOTION_ID.WALK)
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		if (actionID != ACTION_ID.MOVE || !IsPlayingMotion((int)motion_id))
		{
			EndAction();
			actionID = ACTION_ID.MOVE;
			if (!IsPlayingMotion((int)motion_id))
			{
				PlayMotion((int)motion_id);
			}
			if (characterSender != null)
			{
				characterSender.OnActMoveVelocity((int)motion_id);
			}
		}
		moveType = MOVE_TYPE.VELOCITY;
		moveSyncSpeed = sync_speed;
		isControllable = true;
		if (GetVelocity() != velocity_ && velocity_ != Vector3.get_zero())
		{
			SetVelocity(velocity_, VELOCITY_TYPE.ACT_MOVE);
		}
	}

	public virtual void ActMoveSyncVelocity(float time, Vector3 pos, int motion_id)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		if (actionID != ACTION_ID.MOVE || !IsPlayingMotion(motion_id))
		{
			EndAction();
			actionID = ACTION_ID.MOVE;
			PlayMotion(motion_id);
		}
		moveType = MOVE_TYPE.SYNC_VELOCITY;
		enableRootMotion = false;
		moveSyncTime = time;
		moveTargetPos = pos;
		moveSyncEnd = false;
		moveSyncMotionID = motion_id;
		Vector3 val = moveTargetPos - _position;
		val.y = 0f;
		if (moveSyncTime > 0f)
		{
			SetVelocity(val.get_normalized() * (val.get_magnitude() / moveSyncTime), VELOCITY_TYPE.ACT_MOVE);
		}
		if (val == Vector3.get_zero())
		{
			moveSyncDirection = float.MinValue;
		}
		else
		{
			Quaternion val2 = Quaternion.LookRotation(val);
			Vector3 eulerAngles = val2.get_eulerAngles();
			moveSyncDirection = eulerAngles.y;
		}
		moveSyncDirectionTime = charaParameter.moveSyncRotateTime;
		StartWaitingPacket(WAITING_PACKET.CHARACTER_MOVE_VELOCITY, keep_sync: false, charaParameter.moveSendInterval);
	}

	public virtual void SetMoveSyncVelocityEnd(float time, Vector3 pos, float direction, float sync_speed, int motion_id)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		moveSyncTime += time;
		moveTargetPos = pos;
		moveSyncEnd = true;
		moveSyncEndDirection = direction;
		moveSyncSpeed = sync_speed;
		moveSyncMotionID = motion_id;
		Vector3 val = moveTargetPos - _position;
		val.y = 0f;
		if (moveSyncSpeed > 0f)
		{
			float num = val.get_magnitude() / sync_speed;
			if (num < moveSyncTime)
			{
				moveSyncTime = num;
			}
		}
		if (moveSyncTime > 0f)
		{
			SetVelocity(val.get_normalized() * (val.get_magnitude() / moveSyncTime), VELOCITY_TYPE.ACT_MOVE);
		}
		if (val == Vector3.get_zero())
		{
			moveSyncDirection = float.MinValue;
		}
		else
		{
			Quaternion val2 = Quaternion.LookRotation(val);
			Vector3 eulerAngles = val2.get_eulerAngles();
			moveSyncDirection = eulerAngles.y;
		}
		moveSyncDirectionTime = charaParameter.moveSyncRotateTime;
		EndWaitingPacket(WAITING_PACKET.CHARACTER_MOVE_VELOCITY);
	}

	public virtual bool ActMoveToTarget(float max_length = 0f, bool fix_rotate = false)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		if (actionTarget == null)
		{
			return false;
		}
		Vector3 val = GetTargetPosition(actionTarget) - _position;
		val.y = 0f;
		if (max_length > 0f)
		{
			float magnitude = val.get_magnitude();
			if (magnitude > max_length)
			{
				val *= max_length / magnitude;
			}
		}
		return ActMoveToPosition(_position + val, fix_rotate);
	}

	public virtual bool ActMoveToPosition(Vector3 target_pos, bool fix_rotate = false)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		if (IsArrivalPosition(target_pos))
		{
			return false;
		}
		EndAction();
		actionID = ACTION_ID.MOVE;
		PlayMotion(3);
		moveTargetPos = target_pos;
		moveType = MOVE_TYPE.TO_POSITION;
		if (fix_rotate)
		{
			LookAt(moveTargetPos, isBlindEnable: true);
		}
		if (characterSender != null)
		{
			characterSender.OnActMoveToPosition(target_pos);
		}
		return true;
	}

	public virtual bool ActMoveHoming(float max_length = 0f)
	{
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		if ((IsCoopNone() || IsOriginal()) && actionTarget == null)
		{
			return false;
		}
		EndAction();
		if (IsCoopNone() || IsOriginal())
		{
			SetAttackActionPosition();
		}
		actionID = ACTION_ID.MOVE;
		PlayMotion(3);
		moveType = MOVE_TYPE.HOMING;
		periodicSyncActionPositionLastTime = GetActMotionTime();
		periodicSyncActionPositionFlag = true;
		SetPeriodicSyncTarget(actionTarget);
		moveBeforePos = _position;
		moveMaxDistance = max_length;
		if (characterSender != null)
		{
			characterSender.OnActMoveHoming(max_length);
		}
		return true;
	}

	public virtual bool ActRotateToTarget()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		if (actionTarget == null)
		{
			return false;
		}
		Vector3 val = GetTargetPosition(actionTarget) - _position;
		val.y = 0f;
		if (val == Vector3.get_zero())
		{
			return false;
		}
		Quaternion val2 = Quaternion.LookRotation(val);
		Vector3 eulerAngles = val2.get_eulerAngles();
		float y = eulerAngles.y;
		return ActRotateToDirection(y);
	}

	public virtual bool ActRotateToDirection(float direction)
	{
		float diff_angle = 0f;
		int num = CalcDiffAngle(direction, ref diff_angle);
		if (diff_angle < 1f)
		{
			return false;
		}
		EndAction();
		actionID = ACTION_ID.ROTATE;
		if (!rotateDisableMotion)
		{
			if (num > 0)
			{
				PlayMotion(5);
			}
			else
			{
				PlayMotion(4);
			}
		}
		enableRootMotion = false;
		rotateType = ROTATE_TYPE.TO_DIRECTION;
		rotateDirection = direction;
		rotateSign = num;
		rotateVelocity = 0f;
		if (characterSender != null)
		{
			characterSender.OnActRotate(rotateDirection);
		}
		return true;
	}

	public virtual bool ActRotateMotionToTarget(bool keep_rotate = false)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		if (actionTarget == null)
		{
			return false;
		}
		Vector3 val = GetTargetPosition(actionTarget) - _position;
		val.y = 0f;
		val.Normalize();
		if (val == Vector3.get_zero())
		{
			return false;
		}
		Quaternion val2 = Quaternion.LookRotation(val);
		Vector3 eulerAngles = val2.get_eulerAngles();
		float y = eulerAngles.y;
		float diff_angle = 0f;
		int num = CalcDiffAngle(y, ref diff_angle);
		float num2 = CalcRotateMotionRate(diff_angle);
		int num3 = 0;
		if (keep_rotate)
		{
			num3 = rotateTargetCnt + 1;
		}
		bool flag = false;
		float direction = y;
		if (num2 == 1f && num3 < charaParameter.rotateTargetMaxNum - 1)
		{
			Quaternion rotation = _rotation;
			Vector3 eulerAngles2 = rotation.get_eulerAngles();
			direction = eulerAngles2.y + (float)num * 90f;
		}
		else
		{
			flag = true;
		}
		bool flag2 = ActRotateMotionToDirection(direction);
		if (flag2)
		{
			rotateType = ROTATE_TYPE.MOTION_TO_TARGET;
			rotateTargetEnd = flag;
			rotateTargetCnt = num3;
		}
		return flag2;
	}

	public virtual bool ActRotateMotionToDirection(float direction)
	{
		float diff_angle = 0f;
		int num = CalcDiffAngle(direction, ref diff_angle);
		if (diff_angle < 1f)
		{
			return false;
		}
		EndAction();
		actionID = ACTION_ID.ROTATE;
		if (num > 0)
		{
			PlayMotion(5);
		}
		else
		{
			PlayMotion(4);
		}
		rotateDirection = direction;
		rotateSign = num;
		rootRotationRate = CalcRotateMotionRate(diff_angle);
		rotateType = ROTATE_TYPE.MOTION_TO_DIRECTION;
		if (characterSender != null)
		{
			characterSender.OnActRotateMotion(direction);
		}
		return true;
	}

	protected float CalcRotateMotionRate(float diff_angle)
	{
		float num = Mathf.Abs(diff_angle) / 90f;
		if (num > 1.2f)
		{
			num = 1f;
		}
		return num;
	}

	protected int CalcDiffAngle(float direction, ref float diff_angle)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		Vector3 forward = _forward;
		forward.y = 0f;
		forward.Normalize();
		Vector3 val = Quaternion.AngleAxis(direction, Vector3.get_up()) * Vector3.get_forward();
		diff_angle = Vector3.Angle(forward, val);
		Vector3 val2 = Vector3.Cross(forward, val);
		return (val2.y >= 0f) ? 1 : (-1);
	}

	public virtual void ActDamage()
	{
		EndAction();
		actionID = ACTION_ID.DAMAGE;
		PlayMotion(6);
		OnActReaction();
	}

	public virtual void ActDead(bool force_sync = false, bool recieve = false)
	{
		EndAction();
		actionID = ACTION_ID.DEAD;
		PlayMotion(7);
		Die();
		OnActReaction();
		if (characterSender != null && force_sync)
		{
			characterSender.OnActDead();
		}
	}

	public void Die()
	{
		hp = 0;
		isDead = true;
		hitOffFlag |= HIT_OFF_FLAG.DEAD;
		base._collider.set_enabled(false);
		buffParam.AllBuffEnd(sync: false);
		continusAttackParam.RemoveAll();
	}

	public virtual void VanishLocal()
	{
		PrepareVanishLocal();
	}

	public virtual void PrepareVanishLocal()
	{
		EndAction();
		int hp = this.hp;
		Die();
		this.hp = hp;
	}

	public virtual void OnDeadEnd()
	{
	}

	public virtual void ActParalyze()
	{
		if (IsDebuffShadowSealing())
		{
			if (shadowSealingStackDebuff.Contains(ACTION_ID.PARALYZE))
			{
				return;
			}
			shadowSealingStackDebuff.Add(ACTION_ID.PARALYZE);
			if (object.ReferenceEquals(paralyzeEffectTrans, null))
			{
				AnimEventData.EventData eventData = new AnimEventData.EventData();
				eventData.intArgs = new int[0];
				eventData.floatArgs = new float[1]
				{
					paralyzeEffectScale
				};
				eventData.stringArgs = new string[2]
				{
					paralyzeEffectName,
					string.Empty
				};
				paralyzeEffectTrans = AnimEventFormat.EffectEventExec(AnimEventFormat.ID.EFFECT, eventData, base._transform, is_oneshot_priority: true, EffectNameAnalyzer, ((StageObject)this).FindNode, this);
			}
		}
		else
		{
			EndAction();
			actionID = ACTION_ID.PARALYZE;
			PlayMotion(8);
		}
		OnActReaction();
	}

	protected bool UpdateParalyzeAction()
	{
		if (paralyzeTime - Time.get_time() > 0f)
		{
			return false;
		}
		ActParalyzeEnd();
		if (!IsDebuffShadowSealing())
		{
			SetNextTrigger();
		}
		return true;
	}

	protected virtual void ActParalyzeEnd()
	{
		badStatusTotal.paralyze = 0f;
		if (IsDebuffShadowSealing())
		{
			_EndDebuffAction(ACTION_ID.PARALYZE);
			if (!object.ReferenceEquals(paralyzeEffectTrans, null))
			{
				EffectManager.ReleaseEffect(paralyzeEffectTrans.get_gameObject());
				paralyzeEffectTrans = null;
			}
			if (shadowSealingStackDebuff.Contains(ACTION_ID.PARALYZE))
			{
				shadowSealingStackDebuff.Remove(ACTION_ID.PARALYZE);
			}
		}
	}

	public bool IsParalyze()
	{
		return actionID == ACTION_ID.PARALYZE || shadowSealingStackDebuff.Contains(ACTION_ID.PARALYZE);
	}

	public virtual void ActFreezeStart()
	{
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		if (IsFreeze())
		{
			return;
		}
		if (IsDebuffShadowSealing())
		{
			if (!shadowSealingStackDebuff.Contains(ACTION_ID.FREEZE))
			{
				shadowSealingStackDebuff.Add(ACTION_ID.FREEZE);
			}
		}
		else
		{
			EndAction();
			actionID = ACTION_ID.FREEZE;
			PlayMotion(6, (!(stopMotionByDebuffNormalizedTime < 0f)) ? 0f : (-1f));
		}
		CreateFreezeEffect();
		SwitchFreezeShader();
		m_freezeTimer = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.freezeParam.duration;
		m_freezeHeight = 0f;
		if (base._rigidbody != null)
		{
			base._rigidbody.set_velocity(Vector3.get_zero());
		}
		rotateEventKeep = false;
		rotateToTargetFlag = false;
		rotateEventSpeed = 0f;
		OnActReaction();
	}

	protected virtual void ActFreezeEnd()
	{
		if (!IsFreeze())
		{
			return;
		}
		if (m_effectFreeze != null)
		{
			EffectManager.ReleaseEffect(m_effectFreeze);
			m_effectFreeze = null;
		}
		RestoreShader();
		badStatusTotal.freeze = 0f;
		if (IsDebuffShadowSealing())
		{
			_EndDebuffAction(ACTION_ID.FREEZE);
			if (shadowSealingStackDebuff.Contains(ACTION_ID.FREEZE))
			{
				shadowSealingStackDebuff.Remove(ACTION_ID.FREEZE);
			}
		}
		else
		{
			setPause(pause: false);
			m_isStopMotionByDebuff = false;
		}
	}

	protected bool UpdateFreezeAction()
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		if (!IsFreeze())
		{
			return false;
		}
		UpdateFreezeShader();
		m_freezeTimer -= Time.get_deltaTime();
		if (m_freezeTimer <= 0f)
		{
			ActFreezeEnd();
			return true;
		}
		float num = 0.1f;
		if (stopMotionByDebuffNormalizedTime >= 0f)
		{
			num = stopMotionByDebuffNormalizedTime;
		}
		AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
		if (currentAnimatorStateInfo.get_normalizedTime() < num || currentAnimatorStateInfo.get_fullPathHash() != Animator.StringToHash("Base Layer.damage"))
		{
			return false;
		}
		if (m_isStopMotionByDebuff)
		{
			return false;
		}
		setPause(pause: true);
		m_isStopMotionByDebuff = true;
		return false;
	}

	private void SwitchFreezeShader()
	{
		if (m_rendererList != null)
		{
			Utility.MaterialForEach(m_rendererList, delegate(Material material)
			{
				string name = material.get_shader().get_name();
				Shader val = ResourceUtility.FindShader(name.Replace("enemy_", "freeze_enemy_"));
				if (val != null)
				{
					material.set_shader(val);
					if (material.HasProperty("_Height"))
					{
						material.SetFloat("_Height", 0f);
					}
				}
			});
		}
	}

	private void RestoreShader()
	{
		if (m_rendererList != null)
		{
			Utility.MaterialForEach(m_rendererList, delegate(Material material)
			{
				string name = material.get_shader().get_name();
				Shader val = ResourceUtility.FindShader(name.Replace("freeze_", string.Empty));
				if (val != null)
				{
					material.set_shader(val);
				}
			});
		}
	}

	private void UpdateFreezeShader()
	{
		if (m_rendererList != null)
		{
			m_freezeHeight += 5f * Time.get_deltaTime();
			if (m_freezeHeight > 30f)
			{
				m_freezeHeight = 30f;
			}
			Utility.MaterialForEach(m_rendererList, delegate(Material material)
			{
				if (material.HasProperty("_Height"))
				{
					material.SetFloat("_Height", m_freezeHeight);
				}
			});
		}
	}

	private void CreateFreezeEffect()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		Transform effect = EffectManager.GetEffect("ef_btl_pl_frozen_01", base._transform);
		if (!(effect != null))
		{
			return;
		}
		ParticleSystem[] componentsInChildren = effect.GetComponentsInChildren<ParticleSystem>(true);
		if (componentsInChildren != null)
		{
			CalcFreezeEffectEmissionRadius();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				ShapeModule shape = componentsInChildren[i].get_shape();
				shape.set_radius(m_emissionRadius);
			}
		}
		Transform obj = effect;
		obj.set_localPosition(obj.get_localPosition() + Vector3.get_up() * m_emissionRadius);
		m_effectFreeze = effect.get_gameObject();
	}

	protected void CalcFreezeEffectEmissionRadius()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if (!(m_emissionRadius > 0f))
		{
			Vector3 localScale = base._transform.get_localScale();
			m_emissionRadius = localScale.x;
			if (!(base._collider == null))
			{
				m_emissionRadius *= GetFreezeEffectRadiusRate();
			}
		}
	}

	protected virtual float GetFreezeEffectRadiusRate()
	{
		SphereCollider val = base._collider as SphereCollider;
		if (val != null)
		{
			return val.get_radius();
		}
		CapsuleCollider val2 = base._collider as CapsuleCollider;
		if (val2 != null)
		{
			return val2.get_radius();
		}
		return 1f;
	}

	public bool IsFreeze()
	{
		return m_effectFreeze != null;
	}

	protected float GetEmittionRadius()
	{
		return m_emissionRadius;
	}

	public virtual bool IsDebuffShadowSealing()
	{
		return false;
	}

	public virtual bool IsConcussion()
	{
		return false;
	}

	public virtual bool IsLightRing()
	{
		return false;
	}

	protected void CreateElectricShockEffect()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		Transform effect = EffectManager.GetEffect("ef_btl_enm_shock_01", base._transform);
		if (!(effect != null))
		{
			return;
		}
		ParticleSystem[] componentsInChildren = effect.GetComponentsInChildren<ParticleSystem>(true);
		if (componentsInChildren != null)
		{
			CalcFreezeEffectEmissionRadius();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				ShapeModule shape = componentsInChildren[i].get_shape();
				shape.set_radius(m_emissionRadius);
			}
		}
		Transform obj = effect;
		obj.set_localPosition(obj.get_localPosition() + Vector3.get_up() * m_emissionRadius);
		m_effectElectricShock = effect.get_gameObject();
	}

	public virtual bool IsInkSplash()
	{
		return false;
	}

	public virtual bool IsStone()
	{
		return false;
	}

	public virtual void ActAttack(int id, bool send_packet = true, bool sync_immediately = false, string _motionLayerName = "", string _motionStateName = "")
	{
		EndAction();
		isControllable = false;
		actionID = ACTION_ID.ATTACK;
		attackID = id;
		PlayMotionParam playMotionParam = new PlayMotionParam();
		playMotionParam.MotionID = 15 + id;
		playMotionParam.MotionLayerName = ((!string.IsNullOrEmpty(_motionLayerName)) ? _motionLayerName : "Base Layer.");
		if (_motionStateName.IsNullOrWhiteSpace())
		{
			PlayMotion(playMotionParam);
		}
		else
		{
			PlayMotionImmidate(null, _motionStateName, 0f);
		}
		if (IsCoopNone() || IsOriginal())
		{
			SyncRandomSeed = Random.Range(int.MinValue, int.MaxValue);
			SetAttackActionPosition();
		}
		attackStartTarget = actionTarget;
		if (send_packet && characterSender != null)
		{
			characterSender.OnActAttack(id, sync_immediately, SyncRandomSeed, playMotionParam.MotionLayerName, _motionStateName);
		}
	}

	public virtual void SetAttackActionPosition()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		if (actionTarget != null)
		{
			SetActionPosition(GetTargetPosition(actionTarget), flag: true);
		}
		else
		{
			SetActionPosition(Vector3.get_zero(), flag: false);
		}
	}

	protected virtual void UpdateAction()
	{
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		switch (actionID)
		{
		case ACTION_ID.FREEZE:
			UpdateFreezeAction();
			break;
		case ACTION_ID.MOVE:
			if (moveType == MOVE_TYPE.TO_POSITION || moveType == MOVE_TYPE.HOMING)
			{
				if (moveType == MOVE_TYPE.HOMING)
				{
					if (!actionPositionFlag)
					{
						ActIdle();
						break;
					}
					if (moveMaxDistance > 0f)
					{
						Vector3 val = _position - moveBeforePos;
						val.y = 0f;
						moveNowDistance += val.get_magnitude();
						moveBeforePos = _position;
						if (moveNowDistance >= moveMaxDistance)
						{
							ActIdle();
							break;
						}
					}
					moveTargetPos = actionPosition;
				}
				if (IsWallStay())
				{
					ActIdle(is_sync: true);
					break;
				}
				if (IsArrivalPosition(moveTargetPos))
				{
					ActIdle();
					break;
				}
				Vector3 val2 = moveTargetPos - _position;
				val2.y = 0f;
				val2.Normalize();
				Vector3 forward = _forward;
				forward.y = 0f;
				forward.Normalize();
				float num3 = Vector3.Angle(forward, val2);
				if (num3 > 90f)
				{
					ActIdle();
					break;
				}
				Vector3 val3 = Vector3.Cross(forward, val2);
				int num4 = (val3.y >= 0f) ? 1 : (-1);
				Quaternion rotation2 = _rotation;
				Vector3 eulerAngles2 = rotation2.get_eulerAngles();
				float num5 = Mathf.SmoothDampAngle(0f, num3 * (float)num4, ref rotateVelocity, moveRotateMinimumTime, moveRotateMaxSpeed, Time.get_deltaTime());
				_rotation = Quaternion.Euler(eulerAngles2.x, eulerAngles2.y + num5, eulerAngles2.z);
			}
			else if (moveType == MOVE_TYPE.SYNC_VELOCITY)
			{
				if (moveSyncDirection != float.MinValue)
				{
					if (moveSyncDirectionTime > 0f)
					{
						_rotation = Quaternion.Slerp(_rotation, Quaternion.AngleAxis(moveSyncDirection, Vector3.get_up()), Time.get_deltaTime() / moveSyncDirectionTime);
					}
					else
					{
						_rotation = Quaternion.AngleAxis(moveSyncDirection, Vector3.get_up());
					}
				}
				moveSyncTime -= Time.get_deltaTime();
				moveSyncDirectionTime -= Time.get_deltaTime();
				if (moveSyncTime <= 0f)
				{
					_position = moveTargetPos;
					if (moveSyncEnd)
					{
						_rotation = Quaternion.AngleAxis(moveSyncEndDirection, Vector3.get_up());
						ActIdle();
					}
				}
			}
			else if (moveType == MOVE_TYPE.SIDEWAYS)
			{
				UpdateMoveSideAction();
			}
			break;
		case ACTION_ID.ROTATE:
			if (rotateType == ROTATE_TYPE.TO_DIRECTION)
			{
				float diff_angle = 0f;
				int num = CalcDiffAngle(rotateDirection, ref diff_angle);
				if (num != rotateSign)
				{
					ActIdle();
					return;
				}
				if (diff_angle < 0.1f)
				{
					ActIdle();
					return;
				}
				Quaternion rotation = _rotation;
				Vector3 eulerAngles = rotation.get_eulerAngles();
				float num2 = Mathf.SmoothDampAngle(0f, diff_angle * (float)rotateSign, ref rotateVelocity, rotateMinimumTime, rotateMaxSpeed, Time.get_deltaTime());
				_rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y + num2, eulerAngles.z);
			}
			break;
		case ACTION_ID.PARALYZE:
			UpdateParalyzeAction();
			break;
		case ACTION_ID.MOVE_POINT:
			UpdateMovePointAction();
			break;
		case ACTION_ID.MOVE_LOOKAT:
			UpdateMoveLookAtAction();
			break;
		}
		if (IsPlayingMotion(1))
		{
			OnPlayingEndMotion();
		}
	}

	public virtual bool ActMoveSideways(int moveAngleSign = 0, bool isPacket = false)
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		if ((IsCoopNone() || IsOriginal()) && actionTarget == null)
		{
			return false;
		}
		EndAction();
		if (IsCoopNone() || IsOriginal())
		{
			SetAttackActionPosition();
		}
		actionID = ACTION_ID.MOVE;
		MOTION_ID motion_id = MOTION_ID.MOVE_SIDE_R;
		if (isPacket)
		{
			if (moveAngleSign == 0 || moveAngleSign == 1 || moveAngleSign == -1)
			{
				m_moveAngleSign = moveAngleSign;
			}
		}
		else
		{
			switch (moveAngleSign)
			{
			case 0:
			{
				Vector3 val = _position - actionPosition;
				val.y = 0f;
				Vector3 target_pos = actionPosition + Quaternion.AngleAxis(0f - moveAngle_deg, Vector3.get_up()) * val;
				Vector3 target_pos2 = actionPosition + Quaternion.AngleAxis(moveAngle_deg, Vector3.get_up()) * val;
				RaycastHit hit = default(RaycastHit);
				RaycastHit hit2 = default(RaycastHit);
				bool flag = AIUtility.RaycastObstacle(this, target_pos, out hit);
				bool flag2 = AIUtility.RaycastObstacle(this, target_pos2, out hit2);
				if (flag && flag2)
				{
					m_moveAngleSign = 0;
				}
				else if (flag)
				{
					m_moveAngleSign = 1;
				}
				else if (flag2)
				{
					m_moveAngleSign = -1;
				}
				else
				{
					m_moveAngleSign = ((Random.Range(0, 2) == 0) ? 1 : (-1));
				}
				break;
			}
			case -1:
			case 1:
				m_moveAngleSign = moveAngleSign;
				break;
			}
		}
		switch (m_moveAngleSign)
		{
		case 1:
			motion_id = MOTION_ID.MOVE_SIDE_L;
			break;
		case -1:
			motion_id = MOTION_ID.MOVE_SIDE_R;
			break;
		}
		PlayMotion((int)motion_id);
		moveType = MOVE_TYPE.SIDEWAYS;
		if (characterSender != null)
		{
			characterSender.OnActMoveSideways(m_moveAngleSign);
		}
		return true;
	}

	private void UpdateMoveSideAction()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		if (m_moveAngleSign == 0)
		{
			ActIdle();
			return;
		}
		Vector3 val = actionPosition - _position;
		val.y = 0f;
		val.Normalize();
		Vector3 forward = _forward;
		forward.y = 0f;
		forward.Normalize();
		m_diffAngle_deg = Vector3.Angle(forward, val);
		if (m_diffAngle_deg > 90f)
		{
			ActIdle();
			return;
		}
		Vector3 val2 = Vector3.Cross(forward, val);
		int num = (val2.y >= 0f) ? 1 : (-1);
		m_diffAngle_deg *= num;
		Quaternion rotation = _rotation;
		Vector3 eulerAngles = rotation.get_eulerAngles();
		float num2 = Mathf.SmoothDampAngle(0f, m_diffAngle_deg, ref rotateVelocity, rotateMinimumTime, rotateMaxSpeed, Time.get_deltaTime());
		_rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y + num2, eulerAngles.z);
		Vector3 val3 = _position - actionPosition;
		val3.Normalize();
		val3 *= AIUtility.GetLengthWithBetweenPosition(_position, actionPosition);
		float num3 = moveAngle_deg * Time.get_deltaTime();
		float num4 = moveAngleSpeed_deg * Time.get_deltaTime();
		if (num3 > num4)
		{
			num3 = num4;
		}
		val3 = Quaternion.AngleAxis(num3 * (float)m_moveAngleSign, Vector3.get_up()) * val3;
		Vector3 val4 = actionPosition + val3 - _position;
		_position += val4;
		m_movedAngle_deg += num3;
		if (moveAngle_deg <= m_movedAngle_deg)
		{
			SetNextTrigger();
		}
	}

	protected void SetStateMovePoint(STATE_MOVE_POINT state)
	{
		stateMovePoint = state;
	}

	public virtual void ActMovePoint(Vector3 targetPos)
	{
	}

	protected virtual void UpdateMovePointAction()
	{
	}

	protected bool IsNeedToRotate(Vector3 targetDir)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		float num = Vector3.Dot(_forward, targetDir);
		if (num >= 1f)
		{
			return false;
		}
		return true;
	}

	protected void SetStateMoveLookAt(STATE_MOVE_LOOKAT state)
	{
		stateMoveLookAt = state;
	}

	public virtual void ActMoveLookAt(Vector3 moveLookAtPos, bool isPacket = false)
	{
	}

	protected virtual void UpdateMoveLookAtAction()
	{
	}

	protected virtual void OnPlayingEndMotion()
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		bool flag = false;
		ACTION_ID actionID = this.actionID;
		if (actionID == ACTION_ID.ROTATE)
		{
			if (rotateType == ROTATE_TYPE.MOTION_TO_TARGET)
			{
				if (rotateTargetEnd)
				{
					ActIdle();
					return;
				}
				if (actionTarget == null)
				{
					ActIdle();
					return;
				}
				Vector3 val = GetTargetPosition(actionTarget) - _position;
				val.y = 0f;
				val.Normalize();
				if (val == Vector3.get_zero())
				{
					ActIdle();
					return;
				}
				Quaternion val2 = Quaternion.LookRotation(val);
				Vector3 eulerAngles = val2.get_eulerAngles();
				float y = eulerAngles.y;
				float diff_angle = 0f;
				int num = CalcDiffAngle(y, ref diff_angle);
				if (num != rotateSign)
				{
					ActIdle();
					return;
				}
				if (diff_angle < 10f)
				{
					ActIdle();
					return;
				}
				ActRotateMotionToTarget(keep_rotate: true);
			}
			else if (rotateType == ROTATE_TYPE.MOTION_TO_DIRECTION)
			{
				float diff_angle2 = 0f;
				int num2 = CalcDiffAngle(rotateDirection, ref diff_angle2);
				if (num2 != rotateSign)
				{
					ActIdle();
					return;
				}
				if (diff_angle2 < 10f)
				{
					ActIdle();
					return;
				}
				rootRotationRate = CalcRotateMotionRate(diff_angle2);
				if (rotateSign > 0)
				{
					PlayMotion(5);
				}
				else
				{
					PlayMotion(4);
				}
			}
			else
			{
				flag = true;
			}
		}
		else
		{
			flag = true;
		}
		if (flag)
		{
			isPlayingEndMotion = true;
			ActIdle();
		}
	}

	protected virtual void EndAction()
	{
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		if (!base.isInitialized)
		{
			return;
		}
		if (animEventProcessor != null)
		{
			animEventProcessor.ExecuteLastEvent();
		}
		if (characterSender != null)
		{
			characterSender.OnEndAction();
		}
		if (base.controller != null)
		{
			base.controller.OnCharacterEndAction((int)actionID);
		}
		switch (actionID)
		{
		case ACTION_ID.MOVE:
			moveType = MOVE_TYPE.NONE;
			rotateVelocity = 0f;
			moveTargetPos = Vector3.get_zero();
			moveSyncDirection = 0f;
			moveSyncDirectionTime = 0f;
			moveSyncTime = 0f;
			moveSyncEnd = false;
			moveSyncEndDirection = 0f;
			moveSyncSpeed = 0f;
			moveSyncMotionID = 0;
			moveBeforePos = Vector3.get_zero();
			moveNowDistance = 0f;
			moveMaxDistance = 0f;
			m_movedAngle_deg = 0f;
			m_diffAngle_deg = 0f;
			m_moveAngleSign = 0;
			break;
		case ACTION_ID.ROTATE:
			rotateDirection = 0f;
			rotateSign = 0;
			rotateVelocity = 0f;
			rootRotationRate = 1f;
			rotateTargetEnd = false;
			rotateTargetCnt = 0;
			break;
		case ACTION_ID.ATTACK:
			attackID = 0;
			break;
		case ACTION_ID.PARALYZE:
			ActParalyzeEnd();
			break;
		case ACTION_ID.FREEZE:
			ActFreezeEnd();
			break;
		}
		EndWaitingPacket(WAITING_PACKET.CHARACTER_MOVE_VELOCITY);
		EndWaitingPacket(WAITING_PACKET.CHARACTER_UPDATE_ACTION_POSITION);
		EndWaitingPacket(WAITING_PACKET.CHARACTER_UPDATE_DIRECTION);
		int i = 0;
		for (int count = objectList.Count; i < count; i++)
		{
			if (objectTypeAutoDelete[i])
			{
				DestroyObjectList((OBJECT_LIST_TYPE)i);
			}
		}
		if (animator != null)
		{
			int j = 0;
			for (int count2 = changeTriggerList.Count; j < count2; j++)
			{
				animator.ResetTrigger(changeTriggerList[j]);
			}
			changeTriggerList.Clear();
		}
		if (animator != null)
		{
			int k = 0;
			for (int count3 = animatorBoolList.Count; k < count3; k++)
			{
				animator.SetBool(animatorBoolList[k], false);
			}
			animatorBoolList.Clear();
		}
		int l = 0;
		for (int count4 = animEventColliderList.Count; l < count4; l++)
		{
			if (!animEventColliderList[l].isReleased)
			{
				animEventColliderList[l].ReserveRelease();
			}
		}
		if (hideRendererList.Count > 0 && !IsCarrying())
		{
			List<string> range = hideRendererList.GetRange(0, hideRendererList.Count);
			int m = 0;
			for (int count5 = range.Count; m < count5; m++)
			{
				SetEnableNodeRenderer(range[m], enable: true);
			}
		}
		if (referenceCheckerFlag)
		{
			attackHitChecker = new AttackHitChecker();
			referenceCheckerFlag = false;
		}
		int n = 0;
		for (int count6 = loopSeForceEndList.Count; n < count6; n++)
		{
			if (loopSeForceEndList[n] >= 0)
			{
				SoundManager.LoopOff(loopSeForceEndList[n], this);
			}
		}
		loopSeForceEndList.Clear();
		wallStayTimer = 0f;
		isControllable = false;
		enableMotionCancel = false;
		enableMoveSuppress = false;
		SetVelocity(Vector3.get_zero());
		addForce = Vector3.get_zero();
		actionMoveRate = 1f;
		rootMotionMoveRate = 1f;
		if (animator != null)
		{
			animator.set_applyRootMotion(true);
		}
		enableRootMotion = true;
		rotateEventSpeed = 0f;
		rotateEventDirection = 0f;
		rotateEventKeep = false;
		rotateToTargetFlag = false;
		rotateToTargetDiffAngle = 0f;
		rotateSafeMode = false;
		hitOffFlag &= ~HIT_OFF_FLAG.INVICIBLE;
		hitOffFlag &= ~HIT_OFF_FLAG.DEAD;
		hitOffFlag &= ~HIT_OFF_FLAG.DEAD_REVIVE;
		enableEventMove = false;
		eventMoveVelocity = Vector3.get_zero();
		eventMoveTimeCount = 0f;
		enableAddForce = false;
		addForceBeforePos = Vector3.get_zero();
		waitAddForce = false;
		lastActionID = actionID;
		actionID = ACTION_ID.NONE;
		actionPosition = Vector3.get_zero();
		targetPointPos = Vector3.get_zero();
		actionPositionFlag = false;
		actionPositionThroughFlag = false;
		actionPositionWaitSync = false;
		actionPositionWaitTrigger = null;
		directionWaitSync = false;
		directionWaitTrigger = null;
		periodicSyncActionPositionFlag = false;
		periodicSyncActionPositionLastTime = 0f;
		periodicSyncActionPositionList.Clear();
		attackStartTarget = null;
		isDead = false;
		lerpRotateVec = Vector3.get_zero();
		enableReactionDelay = false;
		isPlayingEndMotion = false;
		actionRendererModel = null;
		actionRendererNodeName = null;
		actMotionStartTime = -1f;
		actionReceiveDamageRate = 1f;
		isWallStay = false;
		wallStayTimer = 0f;
		SetPeriodicSyncTarget(null);
		if (stepCtrl != null)
		{
			stepCtrl.enableAutoStampEffect = true;
		}
		if (base._collider != null)
		{
			base._collider.set_enabled(true);
		}
		if (damegeRemainEffect != null)
		{
			EffectManager.ReleaseEffect(damegeRemainEffect);
			damegeRemainEffect = null;
		}
		if (animEventProcessor != null)
		{
			animEventProcessor.IgnoreEventByNextAnim();
		}
		if (actionRendererInstance != null)
		{
			Object.Destroy(actionRendererInstance.get_gameObject());
			actionRendererInstance = null;
		}
		DeleteExAtkColliderAll();
	}

	protected virtual void _EndDebuffAction(ACTION_ID beforeActId)
	{
	}

	private static string _GetMotionStateName(int motion_id, string _layerName)
	{
		string value = (!string.IsNullOrEmpty(_layerName)) ? _layerName : "Base Layer.";
		if (motion_id < 115)
		{
			stateNameBuilder.Length = 0;
			stateNameBuilder.Append(value);
			if (motion_id >= 15 && motion_id <= 114)
			{
				stateNameBuilder.AppendFormat(motionStateName[15], motion_id - 15);
			}
			else
			{
				stateNameBuilder.Append(motionStateName[motion_id]);
			}
			return stateNameBuilder.ToString();
		}
		return null;
	}

	protected virtual string GetMotionStateName(int motion_id, string _layerName = "")
	{
		if (motion_id < 115)
		{
			return _GetMotionStateName(motion_id, _layerName);
		}
		return null;
	}

	public int GetMotionHash(int motion_id)
	{
		string text = GetMotionStateName(motion_id, ReplaceMotionLayer(motion_id));
		if (text == null)
		{
			return 0;
		}
		int num = _GetCachedHash(text);
		if (num != 0)
		{
			return num;
		}
		num = GetMotionHash(text);
		_CacheHash(text, num);
		return num;
	}

	protected int _GetCachedHash(string motionName)
	{
		if (!motionHashCaches.ContainsKey(motionName))
		{
			return 0;
		}
		return motionHashCaches[motionName];
	}

	protected void _CacheHash(string motionName, int hash)
	{
		motionHashCaches[motionName] = hash;
	}

	public int GetMotionHash(string state_name)
	{
		int num = 0;
		object obj = motionHash.Get(state_name);
		if (obj == null)
		{
			num = Animator.StringToHash(state_name);
			motionHash.Add(state_name, num);
		}
		else
		{
			num = (int)obj;
		}
		return num;
	}

	public bool PlayMotion(PlayMotionParam _param)
	{
		_param.MotionLayerName = ReplaceMotionLayer(_param.MotionID, _param.MotionLayerName);
		string text = GetMotionStateName(_param.MotionID, _param.MotionLayerName);
		if (string.IsNullOrEmpty(text))
		{
			Log.Warning(LOG.INGAME, "Character::PlayMotion motion_id is none");
			return false;
		}
		bool result = _PlayMotion(text, null, _param.TransitionTime);
		if (base.controller != null)
		{
			base.controller.OnCharacterPlayMotion(_param.MotionID);
		}
		return result;
	}

	public bool PlayMotion(int motion_id, float transition_time = -1f)
	{
		string text = GetMotionStateName(motion_id, ReplaceMotionLayer(motion_id));
		if (string.IsNullOrEmpty(text))
		{
			Log.Warning(LOG.INGAME, "Character::PlayMotion motion_id is none");
			return false;
		}
		bool result = _PlayMotion(text, null, transition_time);
		if (base.controller != null)
		{
			base.controller.OnCharacterPlayMotion(motion_id);
		}
		return result;
	}

	protected virtual string ReplaceMotionLayer(int motionId, string layerName = "Base Layer.")
	{
		return layerName;
	}

	public bool PlayMotion(string anim_format_name, float _transition_time = -1f)
	{
		if (string.IsNullOrEmpty(anim_format_name))
		{
			Log.Warning(LOG.INGAME, "Character::PlayMotion anim_format_name is null or empty");
			return false;
		}
		SeparateAnimFormatName(anim_format_name, out string ctrl_name, out string state_name);
		if (string.IsNullOrEmpty(state_name))
		{
			Log.Warning(LOG.INGAME, "Character::PlayMotion state_name is null or empty");
			return false;
		}
		return _PlayMotion("Base Layer." + state_name, ctrl_name, _transition_time);
	}

	public static void SeparateAnimFormatName(string anim_format_name, out string ctrl_name, out string state_name)
	{
		ctrl_name = null;
		state_name = null;
		if (string.IsNullOrEmpty(anim_format_name))
		{
			return;
		}
		int num = anim_format_name.IndexOf("@");
		if (num < 0)
		{
			state_name = anim_format_name;
			return;
		}
		ctrl_name = anim_format_name.Substring(0, num);
		state_name = anim_format_name.Substring(num + 1, anim_format_name.Length - (num + 1));
		if (ctrl_name == string.Empty)
		{
			ctrl_name = state_name.ToUpper();
		}
	}

	public static string GetCtrlNameFromAnimFormatName(string anim_format_name)
	{
		SeparateAnimFormatName(anim_format_name, out string ctrl_name, out string _);
		return ctrl_name;
	}

	public bool PlayMotionImmidate(string ctrlName, string stateName, float _transition_time = -1f)
	{
		return _PlayMotion("Base Layer." + stateName, ctrlName, _transition_time);
	}

	protected bool _PlayMotion(string state_name, string controller_name = null, float _transition_time = -1f)
	{
		if (string.IsNullOrEmpty(state_name))
		{
			Log.Warning(LOG.INGAME, "Character::_PlayMotion state_name is null or empty");
			return false;
		}
		nextAnimCtrlName = controller_name;
		nextMotionHash = GetMotionHash(state_name);
		nextMotionTransitionTime = _transition_time;
		if (charaName == "Hellish Zaark" && nextMotionHash == 423958061)
		{
			Debug.Log((object)("hash: " + nextMotionHash + " state name: " + state_name));
		}
		hitOffFlag |= HIT_OFF_FLAG.PLAY_MOTION;
		return true;
	}

	public virtual RuntimeAnimatorController GetAnimCtrl(string ctrl_name)
	{
		return null;
	}

	public virtual AnimEventData GetAnimEvent(string ctrl_name)
	{
		return null;
	}

	public int GetPlayingMotionHash()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		if (animator == null)
		{
			return 0;
		}
		int num = 0;
		if (animator.IsInTransition(0))
		{
			AnimatorStateInfo nextAnimatorStateInfo = animator.GetNextAnimatorStateInfo(0);
			return nextAnimatorStateInfo.get_fullPathHash();
		}
		AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
		return currentAnimatorStateInfo.get_fullPathHash();
	}

	public bool IsPlayingMotion(int motion_id, bool check_next = true)
	{
		int num = GetMotionHash(motion_id);
		if (check_next)
		{
			if (nextMotionHash != 0)
			{
				return nextMotionHash == num;
			}
			int num2 = 0;
			if (animEventProcessor != null)
			{
				num2 = animEventProcessor.GetWaitMotionHash();
			}
			if (num2 != 0)
			{
				return num2 == num;
			}
		}
		return GetPlayingMotionHash() == num;
	}

	public void SetNextTrigger(int index = 0)
	{
		string str = string.Empty;
		if (index > 0)
		{
			str = (index + 1).ToString();
		}
		SetChangeTrigger("next" + str);
	}

	public void SetChangeTrigger(string motion_trigger)
	{
		if (!(animator == null))
		{
			animator.SetTrigger(motion_trigger);
			changeTriggerList.Add(motion_trigger);
		}
	}

	public void SendBuffSync(BuffParam.BUFFTYPE nowBuffType = BuffParam.BUFFTYPE.NONE)
	{
		if (IsOriginal())
		{
			BuffParam.BuffSyncParam sync_param = buffParam.CreateSyncParam(nowBuffType);
			if (characterSender != null)
			{
				characterSender.OnSendBuffSync(sync_param);
			}
			buffSyncLastTime = Time.get_time();
		}
	}

	public void OnBuffReceive(BuffParam.BuffData buffData)
	{
		if (IsCoopNone() || IsOriginal())
		{
			OnBuffStart(buffData);
		}
		else if (characterSender != null)
		{
			characterSender.OnBuffReceive(buffData.type, buffData.value, buffData.time);
		}
	}

	public virtual bool OnBuffStart(BuffParam.BuffData buffData)
	{
		if (!buffParam.BuffStart(buffData))
		{
			return false;
		}
		UpdateAnimatorSpeed();
		if (buffData.sync)
		{
			SendBuffSync();
		}
		return true;
	}

	protected virtual void OnUIBuffRoutine(BuffParam.BUFFTYPE type, int value)
	{
	}

	public virtual void OnBuffRoutine(BuffParam.BuffData buffData, bool packet = false)
	{
		BuffParam.BUFFTYPE type = buffData.type;
		int value = buffData.value;
		if ((type == BuffParam.BUFFTYPE.ELECTRIC_SHOCK || type == BuffParam.BUFFTYPE.SOIL_SHOCK) && !packet)
		{
			value = buffData.damage;
		}
		OnBuffRoutine(type, value, packet);
		if (characterSender != null && !packet)
		{
			characterSender.OnBuffRoutine(buffData.type, value, buffData.fromObjectID, buffData.fromEquipIndex, buffData.fromSkillIndex);
		}
	}

	private void OnBuffRoutine(BuffParam.BUFFTYPE type, int value, bool packet = false)
	{
		switch (type)
		{
		case BuffParam.BUFFTYPE.REGENERATE:
			if (buffParam.IsValidBuff(BuffParam.BUFFTYPE.CANT_HEAL_HP))
			{
				return;
			}
			value = (int)((float)value * buffParam.GetHealUp());
			if (value <= 0)
			{
				value = 1;
			}
			RecoverHp(value, isSend: false);
			EffectManager.GetEffect("ef_btl_sk_heal_02", FindNode("Hip"));
			break;
		case BuffParam.BUFFTYPE.REGENERATE_PROPORTION:
		{
			if (buffParam.IsValidBuff(BuffParam.BUFFTYPE.CANT_HEAL_HP))
			{
				return;
			}
			float num = Mathf.Clamp((float)value, 0f, 100f) / 100f;
			value = (int)((float)hpMax * num);
			RecoverHp(value, isSend: false);
			EffectManager.GetEffect("ef_btl_sk_heal_02", FindNode("Hip"));
			break;
		}
		case BuffParam.BUFFTYPE.POISON:
		case BuffParam.BUFFTYPE.DEADLY_POISON:
		{
			float poisonDamageDownRate = buffParam.GetPoisonDamageDownRate();
			if (poisonDamageDownRate > 0f)
			{
				float num3 = Mathf.Clamp(1f - poisonDamageDownRate, 0f, 1f);
				value = (int)((float)value * num3);
				if (value <= 0)
				{
					value = 1;
				}
			}
			hp -= value;
			if (hp <= 1)
			{
				hp = 1;
			}
			break;
		}
		case BuffParam.BUFFTYPE.BURNING:
		{
			float burnDamageDownRate = buffParam.GetBurnDamageDownRate();
			if (burnDamageDownRate > 0f)
			{
				float num2 = Mathf.Clamp(1f - burnDamageDownRate, 0f, 1f);
				value = (int)((float)value * num2);
				if (value <= 0)
				{
					value = 1;
				}
			}
			hp -= value;
			if (hp <= 1)
			{
				hp = 1;
			}
			break;
		}
		case BuffParam.BUFFTYPE.ELECTRIC_SHOCK:
		case BuffParam.BUFFTYPE.EROSION:
		case BuffParam.BUFFTYPE.SOIL_SHOCK:
		case BuffParam.BUFFTYPE.ACID:
		case BuffParam.BUFFTYPE.CORRUPTION:
		case BuffParam.BUFFTYPE.STIGMATA:
		case BuffParam.BUFFTYPE.CYCLONIC_THUNDERSTORM:
			hp -= value;
			if (hp <= 1)
			{
				hp = 1;
			}
			break;
		case BuffParam.BUFFTYPE.INVINCIBLECOUNT:
		case BuffParam.BUFFTYPE.INVINCIBLE_BADSTATUS:
		case BuffParam.BUFFTYPE.SUBSTITUTE:
		case BuffParam.BUFFTYPE.ORACLE_OHS_PROTECTION:
		case BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS:
			buffParam.ResetInterval(type);
			break;
		}
		OnUIBuffRoutine(type, value);
	}

	public virtual bool OnBuffEnd(BuffParam.BUFFTYPE type, bool sync, bool isPlayEndEffect = true)
	{
		if (!buffParam.BuffEnd(type, isPlayEndEffect))
		{
			return false;
		}
		switch (type)
		{
		case BuffParam.BUFFTYPE.MOVE_SPEED_DOWN:
			badStatusTotal.speedDown = 0f;
			break;
		case BuffParam.BUFFTYPE.BLEEDING:
			badStatusTotal.bleeding = 0f;
			break;
		case BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN:
			badStatusTotal.attackSpeedDown = 0f;
			break;
		case BuffParam.BUFFTYPE.POISON:
			badStatusTotal.poison = 0f;
			break;
		case BuffParam.BUFFTYPE.BURNING:
			badStatusTotal.burning = 0f;
			break;
		case BuffParam.BUFFTYPE.DEADLY_POISON:
			badStatusTotal.deadlyPoison = 0f;
			break;
		case BuffParam.BUFFTYPE.INK_SPLASH:
			badStatusTotal.inkSplash = 0f;
			break;
		case BuffParam.BUFFTYPE.SLIDE:
			badStatusTotal.slide = 0f;
			break;
		case BuffParam.BUFFTYPE.SHIELD:
			OnBuffEnd(BuffParam.BUFFTYPE.SHIELD_SUPER_ARMOR, sync: false);
			OnBuffEnd(BuffParam.BUFFTYPE.SHIELD_REFLECT, sync: false);
			OnBuffEnd(BuffParam.BUFFTYPE.SHIELD_REFLECT_DAMAGE_UP, sync: false);
			ShieldHp = 0;
			break;
		case BuffParam.BUFFTYPE.SILENCE:
			badStatusTotal.silence = 0f;
			break;
		case BuffParam.BUFFTYPE.CANT_HEAL_HP:
			badStatusTotal.cantHealHp = 0f;
			break;
		case BuffParam.BUFFTYPE.BLIND:
			badStatusTotal.blind = 0f;
			break;
		case BuffParam.BUFFTYPE.EROSION:
			badStatusTotal.erosion = 0f;
			break;
		case BuffParam.BUFFTYPE.STONE:
			badStatusTotal.stone = 0f;
			break;
		case BuffParam.BUFFTYPE.ACID:
			badStatusTotal.acid = 0f;
			break;
		case BuffParam.BUFFTYPE.DAMAGE_MOTION_STOP:
			badStatusTotal.damageMotionStop = 0f;
			break;
		case BuffParam.BUFFTYPE.CORRUPTION:
			badStatusTotal.corruption = 0f;
			break;
		}
		UpdateAnimatorSpeed();
		if (sync)
		{
			SendBuffSync();
		}
		return true;
	}

	public virtual void OnPoisonStart(int fromObjectID = 0)
	{
	}

	public virtual void OnBurningStart()
	{
	}

	public virtual void OnBleedingStart()
	{
	}

	public virtual void OnSpeedDown()
	{
	}

	public virtual void OnAttackSpeedDown()
	{
	}

	public virtual void OnDeadlyPoisonStart()
	{
	}

	public virtual void OnInkSplash(InkSplashInfo info)
	{
	}

	public virtual void OnSlideStart()
	{
	}

	public virtual void OnSilenceStart()
	{
	}

	public virtual void OnCantHealHpStart()
	{
	}

	public virtual void OnBlindStart()
	{
	}

	public virtual void OnBuffCancellation()
	{
	}

	public virtual bool IsValidLightRing()
	{
		return false;
	}

	public virtual void OnStoneStart()
	{
	}

	public virtual void OnAcidStart()
	{
	}

	public virtual void OnDamageMotionStopStart(float time)
	{
	}

	public virtual void OnCorruptionStart()
	{
	}

	public virtual bool IsValidBuff(BuffParam.BUFFTYPE targetType)
	{
		return buffParam.IsValidBuff(targetType);
	}

	public bool IsValidBuffByAbility(BuffParam.BUFFTYPE targetType)
	{
		return buffParam.IsValidBuffByAbility(targetType);
	}

	public bool IsValidBuffBlind()
	{
		return IsValidBuff(BuffParam.BUFFTYPE.BLIND);
	}

	public virtual void RecoverHp(int recoverValue, bool isSend)
	{
		hp += recoverValue;
		if (hp > hpMax)
		{
			hp = hpMax;
		}
		if (recoverValue > 0)
		{
			OnBuffEnd(BuffParam.BUFFTYPE.BLEEDING, sync: false);
		}
	}

	public override bool CheckHitAttack(AttackHitInfo info, Collider to_collider, StageObject to_object)
	{
		if (info.canAttackGrabbedPlayer)
		{
			if ((to_object.hitOffFlag & ~HIT_OFF_FLAG.GRAB) != 0)
			{
				return false;
			}
		}
		else if (info.attackType == AttackHitInfo.ATTACK_TYPE.SNATCH)
		{
			if (to_object.hitOffFlag != 0 && (to_object.hitOffFlag & (HIT_OFF_FLAG.INVICIBLE | HIT_OFF_FLAG.DEAD)) == HIT_OFF_FLAG.NONE)
			{
				return false;
			}
		}
		else if (to_object.hitOffFlag != 0)
		{
			return false;
		}
		return base.CheckHitAttack(info, to_collider, to_object);
	}

	public override void OnAttackedHit(AttackHitInfo info, AttackHitColliderProcessor.HitParam hit_param)
	{
		base.OnAttackedHit(info, hit_param);
		if (IsValidAttackedHit(hit_param.fromObject) && !IsPuppet() && !hit_param.fromObject.IsPuppet() && (IsMirror() || IsPuppet()) && isLocalDamageApply && hp - localDamage <= 0)
		{
			ActDead(force_sync: true);
		}
	}

	protected override bool IsValidAttackedHit(StageObject from_object)
	{
		if (isDead)
		{
			return false;
		}
		return base.IsValidAttackedHit(from_object);
	}

	protected override void OnAttackedHitDirection(AttackedHitStatusDirection status)
	{
		if (status.hitParam.processor != null)
		{
			BulletObject bulletObject = status.hitParam.processor.colliderInterface as BulletObject;
			if (bulletObject != null)
			{
				status.atk = bulletObject.masterAtk;
				status.skillParam = bulletObject.masterSkill;
			}
			else
			{
				AtkAttribute atk = new AtkAttribute();
				status.fromObject.GetAtk(status.attackInfo, ref atk);
				status.atk = atk;
				Player player = status.fromObject as Player;
				if (player != null)
				{
					status.skillParam = player.skillInfo.actSkillParam;
				}
			}
		}
		if (IsDamageValid(status))
		{
			status.validDamage = true;
			status.badStatusAdd.Copy(CalcBadStatus(status));
		}
		base.OnAttackedHitDirection(status);
	}

	protected virtual bool IsDamageValid(AttackedHitStatusDirection status)
	{
		return false;
	}

	protected virtual BadStatus CalcBadStatus(AttackedHitStatusDirection status)
	{
		BadStatus targetBadStatus = new BadStatus();
		if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.CANNON_BALL)
		{
			return targetBadStatus;
		}
		targetBadStatus.Copy(status.attackInfo.badStatus);
		if (status.attackInfo.isSkillReference && status.skillParam != null)
		{
			for (int i = 0; i < 3; i++)
			{
				switch (status.skillParam.tableData.supportType[i])
				{
				case BuffParam.BUFFTYPE.HIT_PARALYZE:
					targetBadStatus.paralyze += status.skillParam.supportValue[i];
					break;
				case BuffParam.BUFFTYPE.HIT_POISON:
					targetBadStatus.poison += status.skillParam.supportValue[i];
					break;
				}
			}
		}
		Character character = status.fromObject as Character;
		if (Object.op_Implicit(character))
		{
			targetBadStatus.Add(character.atkBadStatus);
			targetBadStatus.paralyze += character.buffParam.GetValue(BuffParam.BUFFTYPE.ATTACK_PARALYZE);
			targetBadStatus.poison += character.buffParam.GetValue(BuffParam.BUFFTYPE.ATTACK_POISON);
			targetBadStatus.freeze += character.buffParam.GetValue(BuffParam.BUFFTYPE.ATTACK_FREEZE);
			if (targetBadStatus.paralyze > 0f)
			{
				targetBadStatus.paralyze *= character.buffParam.GetBadStatusRateUp(BuffParam.BAD_STATUS_UP.PARALYZE);
				targetBadStatus.paralyze += character.buffParam.GetBadStatusUp(BuffParam.BAD_STATUS_UP.PARALYZE);
			}
			if (targetBadStatus.poison > 0f)
			{
				targetBadStatus.poison *= character.buffParam.GetBadStatusRateUp(BuffParam.BAD_STATUS_UP.POISON);
				targetBadStatus.poison += character.buffParam.GetBadStatusUp(BuffParam.BAD_STATUS_UP.POISON);
			}
			if (targetBadStatus.freeze > 0f)
			{
				targetBadStatus.freeze *= character.buffParam.GetBadStatusRateUp(BuffParam.BAD_STATUS_UP.FREEZE);
				targetBadStatus.freeze += character.buffParam.GetBadStatusUp(BuffParam.BAD_STATUS_UP.FREEZE);
			}
		}
		targetBadStatus.Mul(status.attackInfo.atkRate);
		buffParam.ApplyBadStatusGuard(ref targetBadStatus);
		return targetBadStatus;
	}

	protected override void OnAttackedHitLocal(AttackedHitStatusLocal status)
	{
		if (buffParam.IsValidInvincibleCountBuff())
		{
			return;
		}
		base.OnAttackedHitLocal(status);
		if (status.validDamage)
		{
			AtkAttribute damage_details = new AtkAttribute();
			status.damage = CalcDamage(status, ref damage_details);
			status.damageDetails = damage_details;
			StageObject fromObject = status.fromObject;
			if (fromObject != null)
			{
				fromObject.AbsorptionProc(this, status);
				fromObject.AbsorptionProcByBuff(status);
			}
			bool flag = CutAndAbsorbDamageByBuff(this, status);
			bool flag2 = false;
			if (!flag)
			{
				flag2 = InvincibleDamageByBuff(this, status);
			}
			if (!flag && !flag2)
			{
				ChargeSkillWhenDamagedByBuff();
			}
			if (isLocalDamageApply && (IsPuppet() || IsMirror()))
			{
				localDamage += status.damage;
			}
		}
	}

	protected virtual int CalcDamage(AttackedHitStatusLocal status, ref AtkAttribute damage_details)
	{
		return 0;
	}

	protected AtkAttribute GetInvinsibleMulRate()
	{
		AtkAttribute atkAttribute = new AtkAttribute();
		atkAttribute.Set(1f);
		List<BuffParam.BuffData> invincibleBuffDataList = buffParam.GetInvincibleBuffDataList();
		if (invincibleBuffDataList.IsNullOrEmpty())
		{
			return atkAttribute;
		}
		for (int i = 0; i < invincibleBuffDataList.Count; i++)
		{
			switch (invincibleBuffDataList[i].type)
			{
			case BuffParam.BUFFTYPE.INVINCIBLE_NORMAL:
				atkAttribute.normal = 0f;
				break;
			case BuffParam.BUFFTYPE.INVINCIBLE_FIRE:
				atkAttribute.SetTargetElement(ELEMENT_TYPE.FIRE, 0f);
				break;
			case BuffParam.BUFFTYPE.INVINCIBLE_WATER:
				atkAttribute.SetTargetElement(ELEMENT_TYPE.WATER, 0f);
				break;
			case BuffParam.BUFFTYPE.INVINCIBLE_THUNDER:
				atkAttribute.SetTargetElement(ELEMENT_TYPE.THUNDER, 0f);
				break;
			case BuffParam.BUFFTYPE.INVINCIBLE_SOIL:
				atkAttribute.SetTargetElement(ELEMENT_TYPE.SOIL, 0f);
				break;
			case BuffParam.BUFFTYPE.INVINCIBLE_LIGHT:
				atkAttribute.SetTargetElement(ELEMENT_TYPE.LIGHT, 0f);
				break;
			case BuffParam.BUFFTYPE.INVINCIBLE_DARK:
				atkAttribute.SetTargetElement(ELEMENT_TYPE.DARK, 0f);
				break;
			case BuffParam.BUFFTYPE.INVINCIBLE_ALL_ELEMENT:
				atkAttribute.SetTargetElemetAll(0f);
				break;
			case BuffParam.BUFFTYPE.INVINCIBLE_ALL:
				atkAttribute.normal = 0f;
				atkAttribute.SetTargetElemetAll(0f);
				break;
			}
		}
		return atkAttribute;
	}

	protected virtual AtkAttribute CalcAtk(AttackedHitStatusLocal status)
	{
		AtkAttribute atkAttribute = new AtkAttribute();
		atkAttribute.Add(status.atk);
		atkAttribute.Mul(status.attackInfo.atkRate);
		if (status.damageDistanceData != null)
		{
			float rate = status.damageDistanceData.GetRate(status.distanceXZ);
			atkAttribute.Mul(rate);
		}
		return atkAttribute;
	}

	protected virtual AtkAttribute CalcTolerance(AttackedHitStatusLocal status)
	{
		AtkAttribute _tolerance = new AtkAttribute();
		_tolerance.Add(tolerance);
		AtkAttribute atkAttribute = new AtkAttribute();
		atkAttribute.Set(0f);
		atkAttribute.AddElementOnly(1f);
		atkAttribute.Add(buffParam.passive.tolUpRate);
		atkAttribute.Sub(buffParam.passive.tolDownRate);
		_tolerance.Mul(atkAttribute);
		_tolerance.fire += buffParam.passive.tolList[0];
		_tolerance.water += buffParam.passive.tolList[1];
		_tolerance.thunder += buffParam.passive.tolList[2];
		_tolerance.soil += buffParam.passive.tolList[3];
		_tolerance.light += buffParam.passive.tolList[4];
		_tolerance.dark += buffParam.passive.tolList[5];
		_tolerance.CheckMinus();
		atkAttribute.Set(0f);
		atkAttribute.AddElementOnly(1f);
		atkAttribute.Add(buffParam.GetBuffToleranceRate());
		_tolerance.Mul(atkAttribute);
		AddToleranceBuff(ref _tolerance);
		_tolerance.CheckMinus();
		return _tolerance;
	}

	public void AddToleranceBuff(ref AtkAttribute _tolerance)
	{
		float num = buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_ALLELEMENT);
		_tolerance.fire += (float)buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_FIRE) + num;
		_tolerance.water += (float)buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_WATER) + num;
		_tolerance.thunder += (float)buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_THUNDER) + num;
		_tolerance.soil += (float)buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_SOIL) + num;
		_tolerance.light += (float)buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_LIGHT) + num;
		_tolerance.dark += (float)buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_DARK) + num;
	}

	protected virtual AtkAttribute CalcDefense(AttackedHitStatusLocal status)
	{
		return defense;
	}

	public void AddDefenceBuff(ref AtkAttribute _defence)
	{
		float rate = buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_NORMAL);
		_defence.AddRate(rate);
	}

	public void AddElementDefenceBuff(ref AtkAttribute _defence)
	{
		float num = buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_ALLELEMENT);
		float num2 = buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_FIRE);
		_defence.AddTargetElement(ELEMENT_TYPE.FIRE, num2 + num);
		num2 = buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_WATER);
		_defence.AddTargetElement(ELEMENT_TYPE.WATER, num2 + num);
		num2 = buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_THUNDER);
		_defence.AddTargetElement(ELEMENT_TYPE.THUNDER, num2 + num);
		num2 = buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_SOIL);
		_defence.AddTargetElement(ELEMENT_TYPE.SOIL, num2 + num);
		num2 = buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_LIGHT);
		_defence.AddTargetElement(ELEMENT_TYPE.LIGHT, num2 + num);
		num2 = buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_DARK);
		_defence.AddTargetElement(ELEMENT_TYPE.DARK, num2 + num);
	}

	public override void OnAttackedHitOwner(AttackedHitStatusOwner status)
	{
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_073d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0749: Unknown result type (might be due to invalid IL or missing references)
		//IL_074e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0752: Unknown result type (might be due to invalid IL or missing references)
		//IL_0757: Unknown result type (might be due to invalid IL or missing references)
		if (base.controller != null)
		{
			base.controller.OnCharacterAttackedHitOwner(status);
		}
		bool flag = false;
		if (status.attackInfo != null && status.attackInfo.isImmediateDeath)
		{
			status.damage = hpMax;
			flag = true;
		}
		status.afterHP = hp;
		status.afterShieldHp = ShieldHp;
		if (status.validDamage && !status.aegisParam.isChange)
		{
			if (!status.isDamageRegionOnly)
			{
				status.afterHP -= status.damage;
				if (IsNarrowEscape(status))
				{
					UseNarrowEscape(status);
					status.afterHP = 1;
				}
				if (status.afterHP < 0)
				{
					status.afterHP = 0;
				}
			}
			status.afterShieldHp -= status.shieldDamage;
			if (status.afterShieldHp < 0)
			{
				status.afterShieldHp = 0;
			}
		}
		status.badStatusTotal.Copy(badStatusTotal);
		status.badStatusTotal.Add(status.badStatusAdd);
		if (status.badStatusAdd.paralyze > 0f && status.badStatusTotal.paralyze >= badStatusMax.paralyze && actionID != ACTION_ID.PARALYZE)
		{
			if (!buffParam.IsInvalidReaction(BuffParam.TOLERANCETYPE.PARALYZE))
			{
				status.reactionType = 10;
			}
			else
			{
				status.badStatusTotal.paralyze = 0f;
			}
		}
		if (status.badStatusAdd.freeze > 0f && status.badStatusTotal.freeze >= badStatusMax.freeze && !IsFreeze())
		{
			if (!buffParam.IsInvalidReaction(BuffParam.TOLERANCETYPE.FREEZE))
			{
				status.reactionType = 12;
			}
			else
			{
				status.badStatusTotal.freeze = 0f;
			}
		}
		if (status.badStatusAdd.lightRing > 0f && status.badStatusTotal.lightRing >= badStatusMax.lightRing && !IsLightRing() && IsValidLightRing())
		{
			status.reactionType = 19;
		}
		if (status.badStatusAdd.poison > 0f && status.badStatusTotal.poison >= badStatusMax.poison)
		{
			OnPoisonStart(status.fromObjectID);
		}
		if (status.badStatusAdd.deadlyPoison > 0f && status.badStatusTotal.deadlyPoison >= badStatusMax.deadlyPoison)
		{
			OnDeadlyPoisonStart();
		}
		if (status.badStatusAdd.burning > 0f && status.badStatusTotal.burning >= badStatusMax.burning)
		{
			OnBurningStart();
		}
		if (status.badStatusAdd.speedDown > 0f && status.badStatusTotal.speedDown >= badStatusMax.speedDown)
		{
			OnSpeedDown();
		}
		if (status.badStatusAdd.bleeding > 0f && status.badStatusTotal.bleeding >= badStatusMax.bleeding)
		{
			OnBleedingStart();
		}
		if (status.badStatusAdd.attackSpeedDown > 0f && status.badStatusTotal.attackSpeedDown >= badStatusMax.attackSpeedDown)
		{
			OnAttackSpeedDown();
		}
		if (status.badStatusAdd.inkSplash > 0f && status.badStatusTotal.inkSplash >= badStatusMax.inkSplash && !IsInkSplash())
		{
			OnInkSplash(status.attackInfo.inkSplashInfo);
		}
		if (status.badStatusAdd.slide > 0f && status.badStatusTotal.slide >= badStatusMax.slide)
		{
			OnSlideStart();
		}
		if (status.badStatusAdd.silence > 0f && status.badStatusTotal.silence >= badStatusMax.silence)
		{
			OnSilenceStart();
		}
		if (status.badStatusAdd.cantHealHp > 0f && status.badStatusTotal.cantHealHp >= badStatusMax.cantHealHp)
		{
			OnCantHealHpStart();
		}
		if (status.badStatusAdd.blind > 0f && status.badStatusTotal.blind >= badStatusMax.blind)
		{
			OnBlindStart();
		}
		if (status.badStatusTotal.stone >= badStatusMax.stone && !IsStone())
		{
			if (!buffParam.IsInvalidReaction(BuffParam.TOLERANCETYPE.STONE))
			{
				status.reactionType = 21;
			}
			else
			{
				status.badStatusTotal.stone = 0f;
			}
		}
		if (status.badStatusAdd.acid > 0f && status.badStatusTotal.acid >= badStatusMax.acid)
		{
			OnAcidStart();
		}
		if (status.badStatusAdd.damageMotionStop > 0f && status.badStatusTotal.damageMotionStop >= badStatusMax.damageMotionStop)
		{
			OnDamageMotionStopStart(status.attackInfo.motionStopTime);
		}
		if (status.badStatusAdd.corruption > 0f && status.badStatusTotal.corruption >= badStatusMax.corruption)
		{
			OnCorruptionStart();
		}
		Quaternion rotation = _rotation;
		Vector3 eulerAngles = rotation.get_eulerAngles();
		float y = eulerAngles.y;
		if (status.afterHP <= 0)
		{
			int deadReviveCount = GetDeadReviveCount();
			if (0 < deadReviveCount)
			{
				status.afterHP = 1;
				status.reactionType = 22;
				status.deadReviveCount = deadReviveCount;
				status.badStatusAdd.Reset();
				status.downTotal = 0f;
				status.downAddBase = 0f;
				status.downAddWeak = 0f;
				status.concussionTotal = 0f;
				status.concussionAdd = 0f;
				status.isArrowBleed = false;
				status.isShadowSealing = false;
				status.isArrowBomb = false;
			}
			else
			{
				status.reactionType = 8;
			}
			if (flag)
			{
				PlayImmediateDeathEffect();
			}
		}
		else if (status.reactionType == 0 && !isDead)
		{
			REACTION_TYPE reactionType = REACTION_TYPE.NONE;
			if (IsHitReactionValid(status))
			{
				reactionType = OnHitReaction(status);
			}
			status.reactionType = (int)reactionType;
		}
		if (status.reactionType != 0)
		{
			status.reactionType = (int)CheckReActionTolerance(status);
			if (status.reactionType == 0)
			{
				_rotation = Quaternion.AngleAxis(y, Vector3.get_up());
			}
		}
		if (enableReactionDelay && IsReactionDelayType(status.reactionType))
		{
			DelayReactionInfo delayReactionInfo = new DelayReactionInfo();
			delayReactionInfo.type = (REACTION_TYPE)status.reactionType;
			delayReactionInfo.targetId = status.fromObjectID;
			delayReactionInfo.reactionLoopTime = status.attackInfo.toEnemy.reactionInfo.reactionLoopTime;
			RegisterReacionDelayInfo(delayReactionInfo);
			status.reactionType = 0;
			isReactionDelaySet = true;
		}
		status.hostPos = _position;
		Quaternion rotation2 = _rotation;
		Vector3 eulerAngles2 = rotation2.get_eulerAngles();
		status.hostDir = eulerAngles2.y;
		status.damageHpRate = (1f - (float)status.afterHP / (float)hpMax) * 100f;
		if (status.validDamage)
		{
			buffParam.DecreaseInvincibleCount();
		}
		base.OnAttackedHitOwner(status);
	}

	protected void ApplyInvicibleCount(AttackedHitStatusOwner status)
	{
		if (buffParam.IsValidInvincibleCountBuff())
		{
			status.damage = 0;
			status.damageDetails.Set(0f);
			status.badStatusAdd.Reset();
		}
	}

	protected virtual bool ApplyInvicibleBadStatus(AttackedHitStatusOwner status)
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

	protected void PlayImmediateDeathEffect()
	{
		if (effectPlayProcessor == null)
		{
			return;
		}
		List<EffectPlayProcessor.EffectSetting> settings = effectPlayProcessor.GetSettings("IMMEDIATE_DEATH_EFFECT");
		if (settings == null)
		{
			return;
		}
		for (int i = 0; i < settings.Count; i++)
		{
			if (settings[i] != null)
			{
				effectPlayProcessor.PlayEffect(settings[i], base._transform);
			}
		}
	}

	protected virtual bool IsNarrowEscape(AttackedHitStatusOwner status)
	{
		return false;
	}

	protected virtual void UseNarrowEscape(AttackedHitStatusOwner status)
	{
	}

	protected virtual bool IsHitReactionValid(AttackedHitStatusOwner status)
	{
		return true;
	}

	protected virtual int GetDeadReviveCount()
	{
		return 0;
	}

	protected virtual bool IsReactionDelayType(int type)
	{
		if (type == 10 || type == 12)
		{
			return true;
		}
		return false;
	}

	protected virtual REACTION_TYPE OnHitReaction(AttackedHitStatusOwner status)
	{
		return REACTION_TYPE.NONE;
	}

	protected virtual REACTION_TYPE CheckReActionTolerance(AttackedHitStatusOwner status)
	{
		REACTION_TYPE rEACTION_TYPE = (REACTION_TYPE)status.reactionType;
		if (rEACTION_TYPE == REACTION_TYPE.GUARD_DAMAGE)
		{
			return rEACTION_TYPE;
		}
		AttackHitInfo.ATTACK_TYPE attackType = status.attackInfo.attackType;
		if (attackType == AttackHitInfo.ATTACK_TYPE.SOUNDWAVE)
		{
			if (buffParam.IsHalfReaction(BuffParam.TOLERANCETYPE.SOUNDWAVE))
			{
				rEACTION_TYPE = REACTION_TYPE.DAMAGE;
			}
			else if (buffParam.IsInvalidReaction(BuffParam.TOLERANCETYPE.SOUNDWAVE))
			{
				rEACTION_TYPE = REACTION_TYPE.NONE;
			}
		}
		if (rEACTION_TYPE == REACTION_TYPE.STUNNED_BLOW && buffParam.IsInvalidReaction(BuffParam.TOLERANCETYPE.STUMBLE))
		{
			rEACTION_TYPE = REACTION_TYPE.BLOW;
		}
		if (rEACTION_TYPE == REACTION_TYPE.SHAKE && buffParam.IsInvalidReaction(BuffParam.TOLERANCETYPE.SHAKE))
		{
			rEACTION_TYPE = REACTION_TYPE.NONE;
		}
		if (rEACTION_TYPE == REACTION_TYPE.CHARM_BLOW && buffParam.IsInvalidReaction(BuffParam.TOLERANCETYPE.CHARM))
		{
			rEACTION_TYPE = REACTION_TYPE.BLOW;
		}
		return rEACTION_TYPE;
	}

	public override void OnAttackedHitFix(AttackedHitStatusFix status)
	{
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		base.OnAttackedHitFix(status);
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			MonoBehaviourSingleton<InGameProgress>.I.OnDamage(status, this);
		}
		if (isDead)
		{
			return;
		}
		if (isLocalDamageApply && MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.coopMyClient.clientId == status.fromClientID)
		{
			localDamage -= status.damage;
			if (localDamage <= 0)
			{
				localDamage = 0;
			}
		}
		hp = status.afterHP;
		ShieldHp = status.afterShieldHp;
		badStatusTotal = status.badStatusTotal;
		MakeReactionInfo(status, out ReactionInfo reactionInfo);
		if (reactionInfo.reactionType != 0)
		{
			ApplySyncPosition(status.hostPos, status.hostDir);
		}
		ActReaction(reactionInfo);
		if (!string.IsNullOrEmpty(status.attackInfo.remainEffectName) && !IsIgnoreHitEffect(status.attackInfo))
		{
			Transform effect = EffectManager.GetEffect(status.attackInfo.remainEffectName, rootNode);
			if (effect != null)
			{
				damegeRemainEffect = effect.get_gameObject();
			}
		}
	}

	protected virtual void MakeReactionInfo(AttackedHitStatusFix status, out ReactionInfo reactionInfo)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		reactionInfo = new ReactionInfo();
		reactionInfo.reactionType = (REACTION_TYPE)status.reactionType;
		reactionInfo.blowForce = status.blowForce;
		reactionInfo.loopTime = status.attackInfo.toPlayer.reactionLoopTime;
		reactionInfo.targetId = status.fromObjectID;
	}

	private bool IsIgnoreHitEffect(AttackHitInfo _info)
	{
		if (_info == null)
		{
			return false;
		}
		AttackHitInfo.ATTACK_TYPE attackType = _info.attackType;
		if (attackType == AttackHitInfo.ATTACK_TYPE.BURST_THS_SINGLE_SHOT || attackType == AttackHitInfo.ATTACK_TYPE.BURST_THS_FULL_BURST)
		{
			return true;
		}
		return false;
	}

	public virtual void ActReaction(ReactionInfo info, bool isSync = false)
	{
		switch (info.reactionType)
		{
		case REACTION_TYPE.DEAD:
			ActDead();
			break;
		case REACTION_TYPE.DAMAGE:
			ActDamage();
			break;
		case REACTION_TYPE.PARALYZE:
			ActParalyze();
			break;
		case REACTION_TYPE.FREEZE:
			ActFreezeStart();
			break;
		}
		if (characterSender != null && info.reactionType != 0)
		{
			characterSender.OnActReaction(info, isSync);
		}
	}

	protected void RegisterReacionDelayInfo(DelayReactionInfo newInfo)
	{
		if (SearchReactionDelayInfo(newInfo.type) == null)
		{
			m_reactionDelayList.Add(newInfo);
		}
	}

	protected DelayReactionInfo SearchReactionDelayInfo(REACTION_TYPE targetType)
	{
		int count = m_reactionDelayList.Count;
		for (int i = 0; i < count; i++)
		{
			if (m_reactionDelayList[i].type == targetType)
			{
				return m_reactionDelayList[i];
			}
		}
		return null;
	}

	private void UpdateReactionDelay()
	{
		if (isReactionDelaySet && !enableReactionDelay && (IsCoopNone() || IsOriginal()))
		{
			OnReactionDelay(m_reactionDelayList);
			m_reactionDelayList.Clear();
			isReactionDelaySet = false;
		}
	}

	public virtual void OnReactionDelay(List<DelayReactionInfo> reactionDelayList)
	{
		int count = reactionDelayList.Count;
		if (count <= 0)
		{
			return;
		}
		for (int i = 0; i < count; i++)
		{
			DelayReactionInfo delayReactionInfo = reactionDelayList[i];
			switch (delayReactionInfo.type)
			{
			case REACTION_TYPE.PARALYZE:
				ActParalyze();
				break;
			case REACTION_TYPE.FREEZE:
				ActFreezeStart();
				break;
			}
		}
		if (characterSender != null)
		{
			characterSender.OnReactionDelay(reactionDelayList);
		}
	}

	protected override void OnAttackedContinuationFixedUpdate(AttackedContinuationStatus status)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		base.OnAttackedContinuationFixedUpdate(status);
		if (isDead)
		{
			return;
		}
		float continuationTimeChangeRate = GetContinuationTimeChangeRate(status);
		AttackContinuationInfo.CONTINUATION_TYPE type = status.attackInfo.type;
		if (type == AttackContinuationInfo.CONTINUATION_TYPE.INHALE && (actionID != ACTION_ID.MOVE || moveType != MOVE_TYPE.SYNC_VELOCITY) && !object.ReferenceEquals(status.fromCollider, null))
		{
			Bounds bounds = status.fromCollider.get_bounds();
			Vector3 val = bounds.get_center() - _position;
			val.y = 0f;
			float num = status.attackInfo.inhale.speed * continuationTimeChangeRate;
			float magnitude = val.get_magnitude();
			if (num * Time.get_fixedDeltaTime() > magnitude)
			{
				num = magnitude / Time.get_fixedDeltaTime();
			}
			val.Normalize();
			externalVelocity = val * num;
		}
	}

	public virtual Vector3 GetTransformForward()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Vector3 result = _forward;
		if (lerpRotateVec != Vector3.get_zero())
		{
			result = lerpRotateVec;
		}
		result.y = 0f;
		return result;
	}

	public virtual void AddObjectList(GameObject game_object, OBJECT_LIST_TYPE type = OBJECT_LIST_TYPE.DEFAULT)
	{
		if (type >= OBJECT_LIST_TYPE.DEFAULT && type < OBJECT_LIST_TYPE.NUM)
		{
			DisableNotifyMonoBehaviour disableNotifyMonoBehaviour = game_object.AddComponent<DisableNotifyMonoBehaviour>();
			disableNotifyMonoBehaviour.SetNotifyMaster(this);
			objectList[(int)type].Add(game_object);
		}
	}

	public virtual void DestroyObjectList(OBJECT_LIST_TYPE type)
	{
		List<GameObject> list = objectList[(int)type];
		List<GameObject> range = list.GetRange(0, list.Count);
		range.ForEach(delegate(GameObject o)
		{
			EffectManager.ReleaseEffect(o);
		});
		list.Clear();
	}

	protected override void OnDetachServant(DisableNotifyMonoBehaviour servant)
	{
		objectList.ForEach(delegate(List<GameObject> o)
		{
			o.Remove(servant.get_gameObject());
		});
		buffParam.OnDetachServant(servant);
	}

	public virtual string EffectNameAnalyzer(string effect_name)
	{
		return effect_name;
	}

	public override Transform FindNode(string name)
	{
		if (name == "BODY" && body != null)
		{
			return body;
		}
		return base.FindNode(name);
	}

	public virtual bool CanPlayEffectEvent()
	{
		return true;
	}

	private void EventExAtkColliderStart(AnimEventData.EventData data)
	{
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		float[] floatArgs = data.floatArgs;
		Vector3 pos = default(Vector3);
		pos._002Ector(floatArgs[0], floatArgs[1], floatArgs[2]);
		Vector3 rot = default(Vector3);
		rot._002Ector(floatArgs[3], floatArgs[4], floatArgs[5]);
		float radius = floatArgs[6];
		float height = floatArgs[7];
		string name = data.stringArgs[0];
		string name2 = data.stringArgs[1];
		int uniqueID = data.intArgs[0];
		AttackInfo attackInfo = FindAttackInfo(name);
		if (attackInfo != null)
		{
			Transform val = FindNode(name2);
			if (!(val == null))
			{
				int attackLayer = (base.objectType != OBJECT_TYPE.ENEMY) ? 14 : 15;
				GameObject val2 = new GameObject("AttackColliderObject");
				AttackColliderObject attackColliderObject = val2.AddComponent<AttackColliderObject>();
				attackColliderObject.InitializeForExAtkCollider(this, val, attackInfo, pos, rot, radius, height, attackLayer);
				attackColliderObject.UniqueID = uniqueID;
				attackColliderObject.DetachRigidbody();
				m_exAtkColliderObjectList.Add(attackColliderObject);
			}
		}
	}

	private void EventExAtkColliderEnd(AnimEventData.EventData data)
	{
		int num = data.intArgs[0];
		for (int num2 = m_exAtkColliderObjectList.Count - 1; num2 >= 0; num2--)
		{
			AttackColliderObject attackColliderObject = m_exAtkColliderObjectList[num2];
			if (attackColliderObject.UniqueID == num)
			{
				Object.Destroy(m_exAtkColliderObjectList[num2].get_gameObject());
				m_exAtkColliderObjectList.RemoveAt(num2);
			}
		}
	}

	private void DeleteExAtkColliderAll()
	{
		for (int i = 0; i < m_exAtkColliderObjectList.Count; i++)
		{
			Object.Destroy(m_exAtkColliderObjectList[i].get_gameObject());
		}
		m_exAtkColliderObjectList.Clear();
	}

	private void EventRootMotionON(AnimEventData.EventData data)
	{
		animator.set_applyRootMotion(true);
	}

	private void EventRootMotionOFF(AnimEventData.EventData data)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		animator.set_applyRootMotion(false);
		if (velocityType == VELOCITY_TYPE.ROOT_MOTION)
		{
			SetVelocity(Vector3.get_zero());
		}
	}

	private void EventRootMotionMoveRate(AnimEventData.EventData data)
	{
		float num = rootMotionMoveRate = data.floatArgs[0];
	}

	private void EventHideRendererON(AnimEventData.EventData data)
	{
		string node_name = data.stringArgs[0];
		SetEnableNodeRenderer(node_name, enable: false);
	}

	private void EventHideRendererOFF(AnimEventData.EventData data)
	{
		string node_name = data.stringArgs[0];
		SetEnableNodeRenderer(node_name, enable: true);
	}

	public void EventActionRendererON(AnimEventData.EventData data)
	{
		if (actionRendererModel != null)
		{
			Transform val = FindNode(actionRendererNodeName);
			if (val != null)
			{
				actionRendererInstance = ResourceUtility.Realizes(actionRendererModel, val);
			}
		}
	}

	private void EventActionRendererOFF(AnimEventData.EventData data)
	{
		if (actionRendererInstance != null)
		{
			Object.Destroy(actionRendererInstance.get_gameObject());
			actionRendererInstance = null;
		}
	}

	private void EventEffectDelete(AnimEventData.EventData data)
	{
		string value = EffectNameAnalyzer(data.stringArgs[0]);
		bool flag = !data.intArgs.IsNullOrEmpty() && data.intArgs[0] > 0;
		int count = objectList[2].Count;
		List<GameObject> range = objectList[2].GetRange(0, count);
		for (int i = 0; i < count; i++)
		{
			if (string.IsNullOrEmpty(value) || range[i].get_name().StartsWith(value))
			{
				EffectManager.ReleaseEffect(range[i], !flag, flag);
				objectList[2].Remove(range[i]);
			}
		}
	}

	private void EventUpdateActionPosition(AnimEventData.EventData data)
	{
		string text = (data.stringArgs.Length <= 0) ? null : data.stringArgs[0];
		if (string.IsNullOrEmpty(text))
		{
			text = "next";
		}
		if (IsCoopNone() || IsOriginal())
		{
			UpdateActionPosition(text);
			return;
		}
		if (actionPositionWaitSync)
		{
			Log.Error(LOG.INGAME, "Character UPDATE_ACTION_POSITION Err. ( WaitSync already. ) trigger : " + text);
			return;
		}
		actionPositionWaitSync = true;
		actionPositionWaitTrigger = text;
		StartWaitingPacket(WAITING_PACKET.CHARACTER_UPDATE_ACTION_POSITION, keep_sync: false);
	}

	private void EventUpdateDirection(AnimEventData.EventData data)
	{
		string text = (data.stringArgs.Length <= 0) ? null : data.stringArgs[0];
		if (string.IsNullOrEmpty(text))
		{
			text = "next";
		}
		if (IsCoopNone() || IsOriginal())
		{
			UpdateDirection(text);
			return;
		}
		if (directionWaitSync)
		{
			Log.Error(LOG.INGAME, "Character UPDATE_DIRECTION Err. ( WaitSync already. ) trigger : " + text);
			return;
		}
		directionWaitSync = true;
		directionWaitTrigger = text;
		StartWaitingPacket(WAITING_PACKET.CHARACTER_UPDATE_DIRECTION, keep_sync: false);
	}

	private void EventPeriodicSyncActionPositionStart(AnimEventData.EventData data)
	{
		periodicSyncActionPositionLastTime = GetActMotionTime();
		periodicSyncActionPositionFlag = true;
		SetPeriodicSyncTarget(actionTarget);
	}

	private void EventPeriodicSyncActionPositionEnd(AnimEventData.EventData data)
	{
		periodicSyncActionPositionFlag = false;
		periodicSyncActionPositionLastTime = 0f;
		SetPeriodicSyncTarget(null);
	}

	protected virtual void EventMoveStart(AnimEventData.EventData data, Vector3 targetDir)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		float num = data.floatArgs[0];
		EventMoveEnd();
		enableEventMove = true;
		enableAddForce = false;
		eventMoveVelocity = targetDir * num;
		SetVelocity(Quaternion.LookRotation(GetTransformForward()) * eventMoveVelocity, VELOCITY_TYPE.EVENT_MOVE);
		eventMoveTimeCount = 0f;
	}

	private void EventMoveForwardToTarget(AnimEventData.EventData data)
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		float num = data.floatArgs[0];
		float num2 = (data.floatArgs.Length <= 1) ? 0f : data.floatArgs[1];
		float num3 = (data.floatArgs.Length <= 2) ? 0f : data.floatArgs[2];
		EventMoveEnd();
		if (actionPositionFlag && !(num <= 0f))
		{
			Vector3 val = actionPosition - _position;
			float num4 = val.get_magnitude() - num2;
			if (num3 != 0f && num4 > num3)
			{
				num4 = num3;
			}
			float num5 = num4 / num;
			enableEventMove = true;
			enableAddForce = false;
			eventMoveVelocity = Vector3.get_forward() * num5;
			SetVelocity(Quaternion.LookRotation(GetTransformForward()) * eventMoveVelocity, VELOCITY_TYPE.EVENT_MOVE);
			eventMoveTimeCount = num;
		}
	}

	private void EventMoveToWorldPos(AnimEventData.EventData data)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		float num = data.floatArgs[0];
		Vector3 val = default(Vector3);
		val.x = data.floatArgs[1];
		val.y = data.floatArgs[2];
		val.z = data.floatArgs[3];
		EventMoveEnd();
		Vector3 val2 = val - _position;
		float magnitude = val2.get_magnitude();
		if (!(num <= 1E-07f))
		{
			float num2 = magnitude / num;
			if (!(num2 <= 0f))
			{
				Vector3 val3 = val - _position;
				Vector3 normalized = val3.get_normalized();
				enableEventMove = false;
				enableAddForce = false;
				enableRootMotion = false;
				eventMoveVelocity = normalized * num;
				SetVelocity(eventMoveVelocity, VELOCITY_TYPE.EVENT_MOVE);
				eventMoveTimeCount = num2;
			}
		}
	}

	private void EventMoveSidewaysLookTarget(AnimEventData.EventData data)
	{
		if (data.floatArgs.Length >= 2)
		{
			moveAngle_deg = data.floatArgs[0];
			moveAngleSpeed_deg = data.floatArgs[1];
		}
	}

	private void EventMoveLookAtPosition(AnimEventData.EventData data)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		float num = data.floatArgs[0];
		float moveLookAtAngle = data.floatArgs[1];
		Vector3 moveLookAtPos = default(Vector3);
		moveLookAtPos._002Ector(data.floatArgs[2], 0f, data.floatArgs[3]);
		if (!(num <= 0f))
		{
			this.moveLookAtAngle = moveLookAtAngle;
			this.moveLookAtPos = moveLookAtPos;
		}
	}

	private void EventRotateToTargetStart(AnimEventData.EventData data)
	{
		float num = data.floatArgs[0];
		float num2 = (data.floatArgs.Length <= 1) ? 0f : data.floatArgs[1];
		EndRotate();
		rotateToTargetFlag = true;
		rotateEventSpeed = num;
		rotateToTargetDiffAngle = num2;
	}

	private void EventRotateKeepToTargetStart(AnimEventData.EventData data)
	{
		float num = data.floatArgs[0];
		EndRotate();
		rotateEventSpeed = num;
		rotateEventKeep = true;
	}

	private void EventRotateToAngleStart(AnimEventData.EventData data)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		float num = data.floatArgs[0];
		float num2 = data.floatArgs[1];
		EndRotate();
		Quaternion rotation = _rotation;
		Vector3 eulerAngles = rotation.get_eulerAngles();
		rotateEventDirection = eulerAngles.y + num2 * rootRotationRate;
		if (num > 0f)
		{
			rotateEventSpeed = num;
		}
		else
		{
			_rotation = Quaternion.AngleAxis(rotateEventDirection, Vector3.get_up());
		}
	}

	private void EventRotateToTargetOffset(AnimEventData.EventData data)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		float num = data.floatArgs[0];
		EndRotate();
		bool flag = (data.intArgs[0] != 0) ? true : false;
		Vector3 val = default(Vector3);
		val._002Ector(0f, 0f, data.floatArgs[1]);
		Vector3 pos = _position;
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.boss != null)
		{
			pos = MonoBehaviourSingleton<StageObjectManager>.I.boss._position;
			if (flag)
			{
				Quaternion rotation = MonoBehaviourSingleton<StageObjectManager>.I.boss._rotation;
				val = Quaternion.Euler(rotation.get_eulerAngles()) * val;
			}
		}
		else
		{
			GetTargetPos(out pos);
		}
		pos.y = 0f;
		if (!flag)
		{
			val = Quaternion.LookRotation(_position - pos) * val;
		}
		Vector3 val2 = pos;
		if (val2 != _position)
		{
			val2 += val;
		}
		Quaternion val3 = Quaternion.LookRotation(val2 - _position);
		Vector3 eulerAngles = val3.get_eulerAngles();
		rotateEventDirection = eulerAngles.y;
		if (num > 0f)
		{
			rotateEventSpeed = num;
		}
		else
		{
			_rotation = Quaternion.AngleAxis(rotateEventDirection, Vector3.get_up());
		}
	}

	private void EventAnimatorBoolON(AnimEventData.EventData data)
	{
		string text = data.stringArgs[0];
		animator.SetBool(text, true);
		if (animatorBoolList.IndexOf(text) < 0)
		{
			animatorBoolList.Add(text);
		}
	}

	private void EventAnimatorBoolOFF(AnimEventData.EventData data)
	{
		string text = data.stringArgs[0];
		animator.SetBool(text, false);
		animatorBoolList.Remove(text);
	}

	protected void EventShotGeneric(AnimEventData.EventData data)
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		AttackInfo attackInfo = FindAttackInfo(data.stringArgs[0]);
		if (attackInfo == null)
		{
			return;
		}
		Vector3 offset = default(Vector3);
		offset._002Ector(0f, 0f, 0f);
		if (data.intArgs.Length > 1 && data.intArgs[1] != 0)
		{
			if (actionTarget != null && !IsValidBuffBlind())
			{
				Vector3 val = default(Vector3);
				val._002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
				Quaternion val2 = Quaternion.Euler(new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]));
				val2 = _rotation * val2;
				Vector3 localScale = actionTarget._transform.get_localScale();
				val._002Ector(val.x / localScale.x, val.y / localScale.y, val.z / localScale.z);
				Matrix4x4 localToWorldMatrix = actionTarget._transform.get_localToWorldMatrix();
				val = localToWorldMatrix.MultiplyPoint3x4(val);
				AnimEventShot.Create(this, attackInfo, val, val2);
				return;
			}
			offset.z += 2f;
		}
		AnimEventShot.Create(this, data, attackInfo, offset);
	}

	protected virtual void EventShotPresent(AnimEventData.EventData data)
	{
	}

	protected virtual void EventShotZone(AnimEventData.EventData data)
	{
	}

	public virtual void EventShotDecoy(AnimEventData.EventData data)
	{
	}

	private void EventGenerateTrackingAttack(AnimEventData.EventData data)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		if (data.stringArgs == null || data.stringArgs.Length <= 0)
		{
			Log.Error(LOG.INGAME, "String Data is Empty. Check AnimEvent ( GENERATE_TRACKING ). ");
			return;
		}
		GameObject val = new GameObject("AttackTrackingTarget");
		AttackTrackingTarget attackTrackingTarget = val.AddComponent<AttackTrackingTarget>();
		attackTrackingTarget.Initialize(this, (!IsValidBuffBlind()) ? actionTarget : null, FindAttackInfo(data.stringArgs[0]));
		TrackingTargetBullet = attackTrackingTarget;
	}

	private void EventTrackingBulletOff()
	{
		if (!(TrackingTargetBullet == null))
		{
			TrackingTargetBullet.TrackOff();
		}
	}

	protected virtual void EventStatusUpDefenceON(AnimEventData.EventData data)
	{
	}

	protected virtual void EventStatusUpDefenceOFF()
	{
	}

	protected virtual void EventCameraTargetOffsetOn(AnimEventData.EventData data)
	{
	}

	protected virtual void EventCameraTargetOffsetOff()
	{
	}

	public virtual void EventCameraTargetRotateOn(AnimEventData.EventData data)
	{
	}

	public virtual void EventCameraTargetRotateOff()
	{
	}

	protected virtual void EventExecuteEvolve(AnimEventData.EventData data)
	{
	}

	protected virtual void EventCameraStopOn(AnimEventData.EventData data)
	{
	}

	protected virtual void EventCameraStopOff()
	{
	}

	protected virtual void EventCameraCutOn(AnimEventData.EventData data)
	{
	}

	protected virtual void EventCameraCutOff()
	{
	}

	public override void OnAnimEvent(AnimEventData.EventData data)
	{
		//IL_059b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ba: Unknown result type (might be due to invalid IL or missing references)
		if (CanPlayEffectEvent())
		{
			bool beforeTrailSetting = SetTrailSetting();
			bool is_oneshot_priority = IsOneShotPriority();
			bool isExecEffect = true;
			if ((data.id == AnimEventFormat.ID.EFFECT || data.id == AnimEventFormat.ID.EFFECT_LOOP_CUSTOM || data.id == AnimEventFormat.ID.EFFECT_ONESHOT || data.id == AnimEventFormat.ID.EFFECT_STATIC || data.id == AnimEventFormat.ID.EFFECT_DEPEND_SP_ATTACK_TYPE || data.id == AnimEventFormat.ID.EFFECT_DEPEND_WEAPON_ELEMENT || data.id == AnimEventFormat.ID.EFFECT_SCALE_DEPEND_VALUE || data.id == AnimEventFormat.ID.CAMERA_EFFECT || data.id == AnimEventFormat.ID.EFFECT_SWITCH_OBJECT_BY_CONDITION || data.id == AnimEventFormat.ID.EFFECT_TILING || data.id == AnimEventFormat.ID.EFFECT_ONESHOT_ON_RAIN_SHOT_POS) && data.intArgs != null && data.intArgs.Length > 1)
			{
				switch (data.intArgs[0])
				{
				case 0:
					isExecEffect = true;
					break;
				case 1:
					isExecEffect = !buffParam.IsEnableBuff((BuffParam.BUFFTYPE)data.intArgs[1]);
					break;
				default:
					Log.Error(LOG.EFFECT, "Not Defined EFFECT_EXEC_CONDITION");
					break;
				}
			}
			if (ShotAnimEvent(data, beforeTrailSetting, is_oneshot_priority, isExecEffect))
			{
				return;
			}
		}
		else if ((data.id == AnimEventFormat.ID.EFFECT || data.id == AnimEventFormat.ID.EFFECT_LOOP_CUSTOM || data.id == AnimEventFormat.ID.EFFECT_ONESHOT || data.id == AnimEventFormat.ID.EFFECT_STATIC || data.id == AnimEventFormat.ID.EFFECT_DEPEND_SP_ATTACK_TYPE || data.id == AnimEventFormat.ID.EFFECT_DEPEND_WEAPON_ELEMENT || data.id == AnimEventFormat.ID.EFFECT_SCALE_DEPEND_VALUE || data.id == AnimEventFormat.ID.EFFECT_ONESHOT_ON_RAIN_SHOT_POS) && data.intArgs != null && data.intArgs.Length > 2 && data.intArgs[2] == 1)
		{
			bool beforeTrailSetting2 = SetTrailSetting();
			bool is_oneshot_priority2 = IsOneShotPriority();
			bool isExecEffect2 = true;
			switch (data.intArgs[0])
			{
			case 0:
				isExecEffect2 = true;
				break;
			case 1:
				isExecEffect2 = !buffParam.IsEnableBuff((BuffParam.BUFFTYPE)data.intArgs[1]);
				break;
			default:
				Log.Error(LOG.EFFECT, "Not Defined EFFECT_EXEC_CONDITION");
				break;
			}
			if (ShotAnimEvent(data, beforeTrailSetting2, is_oneshot_priority2, isExecEffect2))
			{
				return;
			}
		}
		if (stepCtrl != null && stepCtrl.OnAnimEvent(data))
		{
			return;
		}
		switch (data.id)
		{
		case AnimEventFormat.ID.EFFECT:
		case AnimEventFormat.ID.EFFECT_ONESHOT:
		case AnimEventFormat.ID.EFFECT_STATIC:
		case AnimEventFormat.ID.EFFECT_LOOP_CUSTOM:
		case AnimEventFormat.ID.CAMERA_EFFECT:
		case AnimEventFormat.ID.MOVE_POINT_DATA:
		case AnimEventFormat.ID.EFFECT_DEPEND_SP_ATTACK_TYPE:
		case AnimEventFormat.ID.EFFECT_DEPEND_WEAPON_ELEMENT:
		case AnimEventFormat.ID.EFFECT_SCALE_DEPEND_VALUE:
		case AnimEventFormat.ID.MOVE_LOOKAT_DATA:
		case AnimEventFormat.ID.EFFECT_SWITCH_OBJECT_BY_CONDITION:
		case AnimEventFormat.ID.EFFECT_TILING:
		case AnimEventFormat.ID.LOAD_BULLET:
		case AnimEventFormat.ID.EFFECT_ONESHOT_ON_RAIN_SHOT_POS:
			break;
		case AnimEventFormat.ID.EXATK_COLLIDER_START:
			EventExAtkColliderStart(data);
			break;
		case AnimEventFormat.ID.EXATK_COLLIDER_END:
			EventExAtkColliderEnd(data);
			break;
		case AnimEventFormat.ID.ROOT_MOTION_ON:
			EventRootMotionON(data);
			break;
		case AnimEventFormat.ID.ROOT_MOTION_OFF:
			EventRootMotionOFF(data);
			break;
		case AnimEventFormat.ID.ROOT_MOTION_MOVE_RATE:
			EventRootMotionMoveRate(data);
			break;
		case AnimEventFormat.ID.HIDE_RENDERER_ON:
			EventHideRendererON(data);
			break;
		case AnimEventFormat.ID.HIDE_RENDERER_OFF:
			EventHideRendererOFF(data);
			break;
		case AnimEventFormat.ID.ACTION_RENDERER_ON:
			EventActionRendererON(data);
			break;
		case AnimEventFormat.ID.ACTION_RENDERER_OFF:
			EventActionRendererOFF(data);
			break;
		case AnimEventFormat.ID.EFFECT_DELETE:
			EventEffectDelete(data);
			break;
		case AnimEventFormat.ID.UPDATE_ACTION_POSITION:
			EventUpdateActionPosition(data);
			break;
		case AnimEventFormat.ID.UPDATE_DIRECTION:
			EventUpdateDirection(data);
			break;
		case AnimEventFormat.ID.PERIODIC_SYNC_ACTION_POSITION_START:
			EventPeriodicSyncActionPositionStart(data);
			break;
		case AnimEventFormat.ID.PERIODIC_SYNC_ACTION_POSITION_END:
			EventPeriodicSyncActionPositionEnd(data);
			break;
		case AnimEventFormat.ID.MOVE_FORWARD_START:
			EventMoveStart(data, Vector3.get_forward());
			break;
		case AnimEventFormat.ID.MOVE_LEFT_START:
			EventMoveStart(data, -Vector3.get_right());
			break;
		case AnimEventFormat.ID.MOVE_RIGHT_START:
			EventMoveStart(data, Vector3.get_right());
			break;
		case AnimEventFormat.ID.MOVE_FORWARD_TO_TARGET:
			EventMoveForwardToTarget(data);
			break;
		case AnimEventFormat.ID.MOVE_TO_WORLDPOS_START:
			EventMoveToWorldPos(data);
			break;
		case AnimEventFormat.ID.MOVE_END:
			EventMoveEnd();
			break;
		case AnimEventFormat.ID.ROTATE_TO_TARGET_START:
			EventRotateToTargetStart(data);
			break;
		case AnimEventFormat.ID.ROTATE_KEEP_TO_TARGET_START:
			EventRotateKeepToTargetStart(data);
			break;
		case AnimEventFormat.ID.ROTATE_TO_ANGLE_START:
			EventRotateToAngleStart(data);
			break;
		case AnimEventFormat.ID.ROTATE_TO_TARGET_OFFSET:
			EventRotateToTargetOffset(data);
			break;
		case AnimEventFormat.ID.ROTATE_END:
			EndRotate();
			break;
		case AnimEventFormat.ID.MOTION_CANCEL_ON:
			enableMotionCancel = true;
			break;
		case AnimEventFormat.ID.MOTION_CANCEL_OFF:
			enableMotionCancel = false;
			break;
		case AnimEventFormat.ID.ANIMATOR_BOOL_ON:
			EventAnimatorBoolON(data);
			break;
		case AnimEventFormat.ID.ANIMATOR_BOOL_OFF:
			EventAnimatorBoolOFF(data);
			break;
		case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE:
		case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_START:
			CreateAttackCollider(data);
			break;
		case AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_END:
			RemoveEventCollider(data.stringArgs[0]);
			break;
		case AnimEventFormat.ID.SHOT_GENERIC:
			EventShotGeneric(data);
			break;
		case AnimEventFormat.ID.MOVE_SUPPRESS_ON:
			enableMoveSuppress = true;
			break;
		case AnimEventFormat.ID.MOVE_SUPPRESS_OFF:
			enableMoveSuppress = false;
			break;
		case AnimEventFormat.ID.ROOT_COLLIDER_ON:
			base._collider.set_enabled(true);
			break;
		case AnimEventFormat.ID.ROOT_COLLIDER_OFF:
			base._collider.set_enabled(false);
			break;
		case AnimEventFormat.ID.DELETE_REMAIN_DMG_EFFECT:
			if (damegeRemainEffect != null)
			{
				EffectManager.ReleaseEffect(damegeRemainEffect);
				damegeRemainEffect = null;
			}
			break;
		case AnimEventFormat.ID.REACTON_DELAY_ON:
			enableReactionDelay = true;
			break;
		case AnimEventFormat.ID.REACTON_DELAY_OFF:
			enableReactionDelay = false;
			break;
		case AnimEventFormat.ID.BUFF_START:
		{
			if (!IsCoopNone() && !IsOriginal())
			{
				break;
			}
			if (data.intArgs.Length <= 0 || data.floatArgs.Length <= 0)
			{
				Log.Error(LOG.INGAME, "No data. Check AnimEvent ( BUFF_START ).");
			}
			float num = data.floatArgs[0];
			if (num <= 0f)
			{
				Log.Error(LOG.INGAME, "Not set Buff time. Check AnimEvent ( BUFF_START ).");
				break;
			}
			float interval = 0f;
			if (data.floatArgs.Length > 1)
			{
				interval = data.floatArgs[1];
			}
			float num2 = 0f;
			if (data.floatArgs.Length > 2)
			{
				num2 = data.floatArgs[2];
			}
			int num3 = data.intArgs[0];
			if (num3 <= -1 || num3 >= 221)
			{
				Log.Error(LOG.INGAME, "Not set valid BUFFTYPE. CHECK AnimEvent ( BUFF_START ).");
				break;
			}
			BuffParam.VALUE_TYPE valueType = BuffParam.VALUE_TYPE.CONSTANT;
			if (data.intArgs.Length >= 3)
			{
				valueType = (BuffParam.VALUE_TYPE)data.intArgs[2];
			}
			bool flag = false;
			if (data.intArgs.Length >= 4)
			{
				flag = (data.intArgs[3] > 0);
				if (flag)
				{
					num = -1f;
				}
			}
			BuffParam.BuffData data2 = new BuffParam.BuffData();
			data2.type = (BuffParam.BUFFTYPE)num3;
			data2.time = ((num2 != 0f) ? num2 : num);
			data2.interval = interval;
			data2.endless = flag;
			data2.valueType = valueType;
			data2.value = data.intArgs[1];
			SetFromInfo(ref data2);
			OnBuffStart(data2);
			break;
		}
		case AnimEventFormat.ID.BUFF_END:
			if (IsCoopNone() || IsOriginal())
			{
				OnBuffEnd((BuffParam.BUFFTYPE)data.intArgs[0], sync: true);
			}
			break;
		case AnimEventFormat.ID.CONTINUS_ATTACK:
			if (IsCoopNone() || IsOriginal())
			{
				CreateContinusAttack(data, isSync: true);
			}
			break;
		case AnimEventFormat.ID.CHANGE_SHADER_PARAM:
			EventChangeShaderParam(data);
			break;
		case AnimEventFormat.ID.PLAYER_DISABLE_MOVE:
			if (MonoBehaviourSingleton<InputManager>.IsValid() && (MonoBehaviourSingleton<InputManager>.I.disableFlags & INPUT_DISABLE_FACTOR.INGAME_COMMAND) == (INPUT_DISABLE_FACTOR)0)
			{
				MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_COMMAND, disable: true);
				this.StartCoroutine(SetEnableInputAfterSeconds(data.floatArgs[0]));
			}
			break;
		case AnimEventFormat.ID.GENERATE_TRACKING:
			EventGenerateTrackingAttack(data);
			break;
		case AnimEventFormat.ID.MOVE_SIDEWAYS_LOOK_TARGET:
			EventMoveSidewaysLookTarget(data);
			break;
		case AnimEventFormat.ID.ACTION_MINE_ATTACK:
			EventActionMineAttack(data);
			break;
		case AnimEventFormat.ID.SHOT_REFLECT_BULLET:
			EventReflectBulletAttack(data);
			break;
		case AnimEventFormat.ID.SHOT_PRESENT:
			if (IsCoopNone() || IsOriginal())
			{
				EventShotPresent(data);
			}
			break;
		case AnimEventFormat.ID.SHOT_ZONE:
			if (IsCoopNone() || IsOriginal())
			{
				EventShotZone(data);
			}
			break;
		case AnimEventFormat.ID.SHOT_DECOY:
			if (IsCoopNone() || IsOriginal())
			{
				EventShotDecoy(data);
			}
			break;
		case AnimEventFormat.ID.STATUS_UP_DEFENCE_ON:
			EventStatusUpDefenceON(data);
			break;
		case AnimEventFormat.ID.STATUS_UP_DEFENCE_OFF:
			EventStatusUpDefenceOFF();
			break;
		case AnimEventFormat.ID.ATTACKHIT_CLEAR_ALL:
			AttackHitCheckerClearAll();
			break;
		case AnimEventFormat.ID.ATTACKHIT_CLEAR_INFO:
			AttackHitCheckerClearInfo(data);
			break;
		case AnimEventFormat.ID.CAMERA_TARGET_OFFSET_ON:
			EventCameraTargetOffsetOn(data);
			break;
		case AnimEventFormat.ID.CAMERA_TARGET_OFFSET_OFF:
			EventCameraTargetOffsetOff();
			break;
		case AnimEventFormat.ID.EXECUTE_EVOLVE:
			EventExecuteEvolve(data);
			break;
		case AnimEventFormat.ID.DBG_TIME_START:
			DbgTimeCount(start: true);
			break;
		case AnimEventFormat.ID.DBG_TIME_END:
			DbgTimeCount(start: false);
			break;
		case AnimEventFormat.ID.NWAY_LASER_ATTACK:
			EventNWayLaserAttack(data);
			break;
		case AnimEventFormat.ID.CAMERA_STOP_ON:
			EventCameraStopOn(data);
			break;
		case AnimEventFormat.ID.CAMERA_STOP_OFF:
			EventCameraStopOff();
			break;
		case AnimEventFormat.ID.CAMERA_CUT_ON:
			EventCameraCutOn(data);
			break;
		case AnimEventFormat.ID.CAMERA_CUT_OFF:
			EventCameraCutOff();
			break;
		case AnimEventFormat.ID.CAMERA_TARGET_ROTATE_ON:
			EventCameraTargetRotateOn(data);
			break;
		case AnimEventFormat.ID.CAMERA_TARGET_ROTATE_OFF:
			EventCameraTargetRotateOff();
			break;
		case AnimEventFormat.ID.TRACKING_BULLET_OFF:
			EventTrackingBulletOff();
			break;
		case AnimEventFormat.ID.ACTION_RECEIVE_DAMAGE_RATE:
			if (data.floatArgs.Length > 0)
			{
				actionReceiveDamageRate = data.floatArgs[0];
			}
			else
			{
				actionReceiveDamageRate = 1f;
			}
			break;
		default:
			base.OnAnimEvent(data);
			break;
		}
	}

	private bool SetTrailSetting()
	{
		bool result = false;
		if (!animUpdatePhysics)
		{
			result = Trail.settingFixedUpdate;
			Trail.settingFixedUpdate = false;
		}
		return result;
	}

	private bool IsOneShotPriority()
	{
		bool result = this is Self;
		Enemy enemy = this as Enemy;
		if (enemy != null)
		{
			result = enemy.isBoss;
		}
		return result;
	}

	private bool ShotAnimEvent(AnimEventData.EventData data, bool beforeTrailSetting, bool is_oneshot_priority, bool isExecEffect)
	{
		Transform val = null;
		if (isExecEffect && data.id != AnimEventFormat.ID.EFFECT_TILING)
		{
			val = AnimEventFormat.EffectEventExec(data.id, data, base._transform, is_oneshot_priority, EffectNameAnalyzer, ((StageObject)this).FindNode, this);
		}
		if (!animUpdatePhysics)
		{
			Trail.settingFixedUpdate = beforeTrailSetting;
		}
		if (val != null)
		{
			if (data.id == AnimEventFormat.ID.EFFECT || data.id == AnimEventFormat.ID.EFFECT_DEPEND_SP_ATTACK_TYPE || data.id == AnimEventFormat.ID.EFFECT_DEPEND_WEAPON_ELEMENT || data.id == AnimEventFormat.ID.EFFECT_SCALE_DEPEND_VALUE || data.id == AnimEventFormat.ID.EFFECT_SWITCH_OBJECT_BY_CONDITION)
			{
				AddObjectList(val.get_gameObject(), OBJECT_LIST_TYPE.ANIM_EVENT);
			}
			return true;
		}
		if (data.id == AnimEventFormat.ID.EFFECT_TILING && isExecEffect)
		{
			Transform[] array = AnimEventFormat.EffectsEventExec(data.id, data, base._transform, is_oneshot_priority, EffectNameAnalyzer, ((StageObject)this).FindNode, this);
			if (array != null)
			{
				int i = 0;
				for (int num = array.Length; i < num; i++)
				{
					AddObjectList(array[i].get_gameObject(), OBJECT_LIST_TYPE.ANIM_EVENT);
				}
			}
			return true;
		}
		return false;
	}

	protected virtual void SetFromInfo(ref BuffParam.BuffData data)
	{
	}

	protected virtual void EventActionMineAttack(AnimEventData.EventData data)
	{
	}

	protected virtual void EventReflectBulletAttack(AnimEventData.EventData data)
	{
	}

	protected virtual void EventNWayLaserAttack(AnimEventData.EventData data)
	{
	}

	private IEnumerator SetEnableInputAfterSeconds(float seconds)
	{
		yield return (object)new WaitForSeconds(seconds);
		MonoBehaviourSingleton<InputManager>.I.SetDisable(INPUT_DISABLE_FACTOR.INGAME_COMMAND, disable: false);
	}

	public void CreateContinusAttack(AnimEventData.EventData eventData, bool isSync, float exEndTime = 0f)
	{
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		if (eventData == null)
		{
			return;
		}
		float endTime = eventData.intArgs[1];
		if (exEndTime > 0f)
		{
			endTime = exEndTime;
		}
		int eventIndex = -1;
		int count = continusAtkEventDataList.Count;
		for (int i = 0; i < count; i++)
		{
			if (continusAtkEventDataList[i] == eventData)
			{
				eventIndex = i;
				break;
			}
		}
		AnimEventCollider animEventCollider = CreateAttackCollider(eventData, isUseColliderList: false);
		animEventCollider.SetFixedUpdateFlag(flag: false);
		animEventCollider.SetFixTransformUpdateFlag(flag: false);
		animEventCollider.ValidTriggerStay();
		Transform val = null;
		string text = eventData.stringArgs[2];
		string text2 = eventData.stringArgs[3];
		if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
		{
			Vector3 zero = Vector3.get_zero();
			Quaternion localRotation = Quaternion.get_identity();
			float[] floatArgs = eventData.floatArgs;
			if (floatArgs.Length > 8)
			{
				zero._002Ector(floatArgs[8], floatArgs[9], floatArgs[10]);
				localRotation = Quaternion.Euler(floatArgs[11], floatArgs[12], floatArgs[13]);
			}
			Transform parent = Utility.Find(base._transform, text2);
			val = EffectManager.GetEffect(text, parent);
			val.set_localPosition(zero);
			val.set_localRotation(localRotation);
		}
		continusAttackParam.Register(eventIndex, endTime, animEventCollider, val);
		if (isSync)
		{
			SendContinusAttackSync();
		}
	}

	public void CreateContinusAttackBySyncData(ContinusAttackParam.SyncData syncData)
	{
		int eventIndex = syncData.eventIndex;
		if (eventIndex >= 0 && eventIndex < continusAtkEventDataList.Count)
		{
			AnimEventData.EventData eventData = continusAtkEventDataList[syncData.eventIndex];
			CreateContinusAttack(eventData, isSync: false, syncData.endTime);
		}
	}

	public void SendContinusAttackSync()
	{
		if (IsOriginal())
		{
			ContinusAttackParam.SyncParam syncParam = continusAttackParam.CreateSyncParam();
			if (characterSender != null)
			{
				characterSender.OnSendContinusAttackSync(syncParam);
			}
		}
	}

	public void ReceiveContinusAttackParam(ContinusAttackParam.SyncParam syncParam)
	{
		continusAttackParam.ApplySyncParam(syncParam);
	}

	protected AnimEventCollider CreateAttackCollider(AnimEventData.EventData eventData, bool isUseColliderList = true)
	{
		bool flag = true;
		AnimEventCollider animEventCollider = null;
		if (isUseColliderList)
		{
			int i = 0;
			for (int count = animEventColliderList.Count; i < count; i++)
			{
				if (animEventColliderList[i].isReleased)
				{
					animEventCollider = animEventColliderList[i];
					flag = false;
					break;
				}
			}
		}
		if (flag)
		{
			animEventCollider = new AnimEventCollider();
			if (isUseColliderList)
			{
				animEventColliderList.Add(animEventCollider);
			}
		}
		animEventCollider.Initialize(this, eventData, FindAttackInfo(eventData.stringArgs[0]));
		if (eventData.id == AnimEventFormat.ID.ATK_COLLIDER_CAPSULE || eventData.id == AnimEventFormat.ID.ATK_COLLIDER_CAPSULE_DEPEND_VALUE)
		{
			animEventCollider.ReserveRelease();
		}
		return animEventCollider;
	}

	public IEnumerator CreateMultiAttackCollider(AnimEventData.EventData eventData, bool isUseColliderList = true)
	{
		int generateCount = GetColliderGenerateCount(eventData);
		AnimEventCollider[] event_colliders = new AnimEventCollider[generateCount];
		List<AnimEventCollider> colList = new List<AnimEventCollider>(animEventColliderList);
		for (int colIdx = 0; colIdx < generateCount; colIdx++)
		{
			AnimEventCollider animEventCollider = event_colliders[colIdx];
			AnimEventCollider col = null;
			if (isUseColliderList)
			{
				int i = 0;
				for (int count = colList.Count; i < count; i++)
				{
					if (colList[i].isReleased)
					{
						col = colList[i];
						colList.RemoveAt(i);
						break;
					}
				}
			}
			if (col == null)
			{
				col = new AnimEventCollider();
				if (isUseColliderList)
				{
					animEventColliderList.Add(col);
				}
			}
			col.Initialize(this, eventData, FindAttackInfo(eventData.stringArgs[0]));
			col.InitTransformSettings(this, eventData);
			if (eventData.intArgs[2] != 12)
			{
				col.OverwriteObjectLayer(eventData.intArgs[2]);
			}
			col.ReserveRelease();
			yield return null;
		}
		yield return null;
	}

	protected virtual int GetColliderGenerateCount(AnimEventData.EventData eventData)
	{
		if (eventData.intArgs == null || eventData.intArgs.Length < 2)
		{
			return 1;
		}
		return eventData.intArgs[1];
	}

	protected void RemoveEventCollider(string targetName)
	{
		int count = animEventColliderList.Count;
		for (int i = 0; i < count; i++)
		{
			if (!animEventColliderList[i].isReleased)
			{
				AttackInfo attackInfo = animEventColliderList[i].attackInfo;
				if (attackInfo != null && attackInfo.name == targetName)
				{
					animEventColliderList[i].ReserveRelease();
				}
			}
		}
	}

	public void EventMoveEnd()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		enableEventMove = false;
		enableAddForce = false;
		if (velocityType == VELOCITY_TYPE.EVENT_MOVE)
		{
			SetVelocity(Vector3.get_zero());
		}
		eventMoveVelocity = Vector3.get_zero();
		eventMoveTimeCount = 0f;
	}

	protected virtual void EndRotate()
	{
		rotateEventSpeed = 0f;
		rotateEventDirection = 0f;
		rotateEventKeep = false;
		rotateToTargetFlag = false;
		rotateToTargetDiffAngle = 0f;
	}

	protected void SetPeriodicSyncTarget(StageObject target)
	{
		Character character = periodicSyncTarget as Character;
		if (character != null)
		{
			character.periodicSyncOwnerList.Remove(this);
		}
		periodicSyncTarget = null;
		periodicSyncTarget = target;
		Character character2 = periodicSyncTarget as Character;
		if (character2 != null)
		{
			character2.periodicSyncOwnerList.Add(this);
		}
	}

	public override AttackInfo[] GetAttackInfos()
	{
		return attackInfos;
	}

	public void SetEnableNodeRenderer(string node_name, bool enable)
	{
		Transform val = FindNode(node_name);
		if (!(val == null))
		{
			val.GetComponentsInChildren<Renderer>(Temporary.rendererList);
			int i = 0;
			for (int count = Temporary.rendererList.Count; i < count; i++)
			{
				Temporary.rendererList[i].set_enabled(enable);
			}
			Temporary.rendererList.Clear();
			val.GetComponentsInChildren<rymFX>(Temporary.fxList);
			int j = 0;
			for (int count2 = Temporary.fxList.Count; j < count2; j++)
			{
				Temporary.fxList[j].set_enabled(enable);
			}
			Temporary.fxList.Clear();
			val.GetComponentsInChildren<TargetPoint>(Temporary.targetPointList);
			int k = 0;
			for (int count3 = Temporary.targetPointList.Count; k < count3; k++)
			{
				Temporary.targetPointList[k].set_enabled(enable);
			}
			Temporary.targetPointList.Clear();
			if (enable)
			{
				hideRendererList.Remove(node_name);
			}
			else if (!hideRendererList.Contains(node_name))
			{
				hideRendererList.Add(node_name);
			}
		}
	}

	public void SetEnableNodeTrailRenderer(string node_name)
	{
		Transform val = FindNode(node_name);
		if (!(val == null))
		{
			val.GetComponentsInChildren<Trail>(Temporary.trailList);
			for (int i = 0; i < Temporary.trailList.Count; i++)
			{
				Temporary.trailList[i].Reset();
			}
			Temporary.trailList.Clear();
		}
	}

	public virtual void ChatSay(int chatID)
	{
		if (IsOriginal() && MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopStage.StageChat(id, chatID);
		}
		SoundManager.PlaySystemSE(SoundID.UISE.CHAT_BALOON);
	}

	public virtual void ChatSay(string message)
	{
		if (IsOriginal() && MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopStage.SendChatMessage(id, message);
		}
		SoundManager.PlaySystemSE(SoundID.UISE.CHAT_BALOON);
	}

	public virtual void ChatSayStamp(int stamp_id)
	{
		if (IsOriginal() && MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			if (QuestManager.IsValidInGameExplore())
			{
				MonoBehaviourSingleton<CoopManager>.I.coopRoom.SendChatStamp(stamp_id);
			}
			else
			{
				MonoBehaviourSingleton<CoopManager>.I.coopStage.SendChatStamp(id, stamp_id);
			}
		}
		SoundManager.PlaySystemSE(SoundID.UISE.CHAT_BALOON);
	}

	protected void ResetStatusParam()
	{
		attack.Set(0f);
		defense.Set(0f);
		tolerance.Set(0f);
	}

	public override void OnFailedWaitingPacket(WAITING_PACKET type)
	{
		switch (type)
		{
		case WAITING_PACKET.CHARACTER_MOVE_VELOCITY:
			ActIdle();
			break;
		case WAITING_PACKET.CHARACTER_UPDATE_ACTION_POSITION:
			UpdateActionPosition(actionPositionWaitTrigger);
			break;
		case WAITING_PACKET.CHARACTER_UPDATE_DIRECTION:
			UpdateDirection(directionWaitTrigger);
			break;
		case WAITING_PACKET.PLAYER_APPLY_CHANGE_WEAPON:
			ActIdle();
			break;
		}
		base.OnFailedWaitingPacket(type);
	}

	public override Vector3 GetPredictivePosition()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		if (IsPuppet() || IsMirror())
		{
			if (base.packetReceiver != null && base.packetReceiver.GetPredictivePosition(out Vector3 pos))
			{
				return pos;
			}
			if (actionID == ACTION_ID.MOVE && moveType == MOVE_TYPE.SYNC_VELOCITY)
			{
				return moveTargetPos;
			}
		}
		return base.GetPredictivePosition();
	}

	public virtual void SetAppearPos(Vector3 pos)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		isSetAppearPos = true;
		appearPos = pos;
	}

	public virtual void SetAppearRandomPosFixDistance(Vector3 center_pos, float distance, int try_count)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		List<int> list = new List<int>(try_count);
		for (int i = 0; i < try_count; i++)
		{
			list.Add(i);
		}
		float num = 360f / (float)try_count;
		float num2 = num * Random.get_value();
		Vector3 val = Vector3.get_zero();
		for (int j = 0; j < try_count; j++)
		{
			int index = (int)((float)list.Count * Random.get_value());
			int num3 = list[index];
			list.RemoveAt(index);
			float num4 = num2 + num * (float)num3;
			if (num4 >= 360f)
			{
				num4 -= 360f;
			}
			Vector3 val2 = center_pos + Quaternion.Euler(0f, num4, 0f) * Vector3.get_forward() * distance;
			if (MonoBehaviourSingleton<StageManager>.I.CheckPosInside(val2))
			{
				val = val2;
				break;
			}
		}
		_position = val;
		_rotation = Quaternion.AngleAxis(Random.get_value() * 360f, Vector3.get_up());
		SetAppearPos(val);
	}

	public override AttackHitChecker ReferenceAttackHitChecker()
	{
		referenceCheckerFlag = true;
		return attackHitChecker;
	}

	public void AttackHitCheckerClearAll()
	{
		if (!object.ReferenceEquals(attackHitChecker, null))
		{
			attackHitChecker.ClearAll();
		}
	}

	public void AttackHitCheckerClearInfo(AnimEventData.EventData evData)
	{
		if (!object.ReferenceEquals(attackHitChecker, null) && evData.stringArgs.Length != 0)
		{
			attackHitChecker.ClearHitInfo(evData.stringArgs[0]);
		}
	}

	private void EventChangeShaderParam(AnimEventData.EventData evData)
	{
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		if (evData == null)
		{
			return;
		}
		int num = evData.stringArgs.Length;
		if (num < 2)
		{
			return;
		}
		string text = evData.stringArgs[0];
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		Transform val = Utility.Find(base._transform, text);
		if (val == null)
		{
			return;
		}
		Renderer component = val.GetComponent<Renderer>();
		if (component == null)
		{
			return;
		}
		Material material = component.get_material();
		if (material == null)
		{
			return;
		}
		int result = 0;
		float result2 = 0f;
		Color white = Color.get_white();
		for (int i = 1; i < num; i++)
		{
			string[] array = evData.stringArgs[i].Split(':');
			string text2 = array[0];
			string text3 = array[1];
			string text4 = array[2];
			if (!material.HasProperty(text3) || text2 == null)
			{
				continue;
			}
			if (!(text2 == "F"))
			{
				if (!(text2 == "I"))
				{
					if (text2 == "C" && ColorUtility.TryParseHtmlString("#" + text4, ref white))
					{
						material.SetColor(text3, white);
					}
				}
				else if (int.TryParse(text4, out result))
				{
					material.SetInt(text3, result);
				}
			}
			else if (float.TryParse(text4, out result2))
			{
				material.SetFloat(text3, result2);
			}
		}
	}

	protected void SetShader(string shaderName, string containsString)
	{
		if (!string.IsNullOrEmpty(shaderName) && m_rendererList != null)
		{
			Utility.MaterialForEach(m_rendererList, delegate(Material material)
			{
				if (material.get_shader().get_name().Contains(containsString))
				{
					Shader val = ResourceUtility.FindShader(shaderName);
					if (val != null)
					{
						material.set_shader(val);
					}
				}
			});
		}
	}

	protected void ChangeGhostShaderParam(float endParam, float duration)
	{
		if (m_rendererList != null && m_rendererList.Length > 0)
		{
			string SHADER_PARAM_ALPHA = "_Alpha";
			string SHADER_PARAM_ALPHA_BLUR = "_Blend";
			Utility.MaterialForEach(m_rendererList, delegate(Material material)
			{
				if (material.HasProperty(SHADER_PARAM_ALPHA_BLUR))
				{
					this.StartCoroutine(ChangeShaderParam(material, SHADER_PARAM_ALPHA_BLUR, endParam, duration));
				}
				if (material.HasProperty(SHADER_PARAM_ALPHA) && material.get_shader().get_name().Contains("enemy_"))
				{
					this.StartCoroutine(ChangeShaderParam(material, SHADER_PARAM_ALPHA, endParam, duration));
				}
			});
		}
	}

	private IEnumerator ChangeShaderParam(Material mat, string propertyName, float endParam, float duration)
	{
		if (mat == null)
		{
			yield break;
		}
		float timer = duration;
		float inputParam = mat.GetFloat(propertyName);
		bool isPlus = endParam >= inputParam;
		bool isFinish = false;
		while (!isFinish)
		{
			timer -= duration * Time.get_deltaTime();
			if (duration <= 0f)
			{
				inputParam = endParam;
			}
			else if (isPlus)
			{
				float calcedParam2 = endParam / duration * Time.get_deltaTime();
				inputParam += calcedParam2;
			}
			else
			{
				float calcedParam2 = inputParam / duration * Time.get_deltaTime();
				inputParam -= calcedParam2;
			}
			if ((isPlus && inputParam >= endParam) || (!isPlus && inputParam <= endParam))
			{
				inputParam = endParam;
			}
			mat.SetFloat(propertyName, inputParam);
			if (timer <= 0f)
			{
				isFinish = true;
			}
			yield return null;
		}
	}

	public bool IsValidShield()
	{
		if ((int)m_shieldHpMax <= 0)
		{
			return false;
		}
		return (int)m_shieldHp > 0;
	}

	private void DbgTimeCount(bool start)
	{
	}

	protected virtual bool GetTargetPos(out Vector3 pos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		pos = Vector3.get_zero();
		return false;
	}

	public virtual bool IsCarrying()
	{
		return false;
	}

	public void OnCheckAndResizeColliderOsMapByWeapon(int weapontId)
	{
		OnResizeColliderOfMap();
	}

	private void OnResizeColliderOfMap()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		Vector3 colliderOfMapScale = MonoBehaviourSingleton<GoGameSettingsManager>.I.colliderOfMapScale;
		MonoBehaviourSingleton<SceneSettingsManager>.I.OnResizeGObjContainColliders(colliderOfMapScale);
	}

	public void OnCheckAndResizeColliderOsMapByEnemy(int enemyId)
	{
	}
}
