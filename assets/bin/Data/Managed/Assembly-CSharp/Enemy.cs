using Network;
using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : Character
{
	public enum SUB_ACTION_ID
	{
		STEP = 12,
		DOWN,
		ANGRY,
		ESCAPE,
		COUNTER,
		DIZZY,
		SHADOWSEALING,
		MAD_MODE,
		APPEAR,
		LIGHT_RING,
		MAX
	}

	public enum SUB_MOTION_ID
	{
		STEP = 115,
		STEP_BACK = 116,
		DOWN = 117,
		DOWN_TIME = 118,
		COUNTER = 119,
		ESCAPE_START = 120,
		ESCAPE = 121,
		DIZZY = 122,
		MAD_MODE = 123,
		APPEAR = 124,
		ANGRY_BEGIN = 125,
		ANGRY_END = 141,
		MAX = 142
	}

	[Serializable]
	public class RegionInfo
	{
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
		public class CounterInfo
		{
			[Tooltip("この部位があるとカウンタ\u30fcが有効かどうか")]
			public bool enabled;

			[Tooltip("カウンタ\u30fcが発動するのは何発受けたときか")]
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
			[Tooltip("設定されているモ\u30fcドへの変更を有効にする")]
			public bool enabled;

			[Tooltip("変更後モ\u30fcドID")]
			public int modeID;
		}

		[Serializable]
		public class DragonArmorInfo
		{
			[Tooltip("竜装が有効か")]
			public bool enabled;

			[Tooltip("両手剣バ\u30fcストSP攻撃の倍率")]
			public float thsBurstSpAttackRate = 1f;

			[Tooltip("その他攻撃の倍率")]
			public float otherAttackRate = 1f;
		}

		public string name;

		public int maxHP = 10;

		public string[] deactivateObjects;

		[Tooltip("ヒット素材名(EnemyHitMaterialTable)")]
		public string hitMaterialName;

		public BreakEffect breakEffect;

		public BreakDrop breakDrop;

		[Tooltip("部位破壊時ダウンモ\u30fcション")]
		public bool breakInDown;

		[Tooltip("部位破壊時ダメ\u30fcジモ\u30fcション")]
		public bool breakInDamage;

		[Tooltip("部位破壊後ヒット有効フラグ")]
		public bool breakAfterHit = true;

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

		[Tooltip("特殊バフ適用時のバリアへのダメ\u30fcジ値")]
		public int barrierDamageSp = 100;

		[Tooltip("通常時のバリアへのダメ\u30fcジ値")]
		public AtkAttribute atkBarrierDamage = new AtkAttribute();

		[Tooltip("バリアによる耐性値上昇率(0.0〜1.0)")]
		public float barrierToleranceRate;

		[Tooltip("カウンタ\u30fc情報")]
		public CounterInfo counterInfo = new CounterInfo();

		[Tooltip("ダウン値係数")]
		public int customDownRate;

		[Tooltip("シ\u30fcルドダメ\u30fcジ有効フラグ")]
		public bool isEnableShieldDamage;

		[Tooltip("シ\u30fcルド時の掴み解除フラグ")]
		public bool isGrabRelease;

		[Tooltip("最終ダメ\u30fcジを最小にする")]
		public bool isDamageMinimum;

		public WeaponTypeRate[] weaponTypeRate;

		[Tooltip("プレイヤ\u30fcの攻撃が当たるか")]
		public bool isAtkColliderHit = true;

		[Tooltip("モ\u30fcド変更情報")]
		public ModeChangeInfo modeChangeInfo;

		[Tooltip("竜装情報")]
		public DragonArmorInfo dragonArmorInfo;
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
		WEAK_ELEMENT_SP_ATTACK
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
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.self != null && MonoBehaviourSingleton<StageObjectManager>.I.self.id == ownerID)
			{
				return true;
			}
			return false;
		}

		public bool IsMaxLv()
		{
			return MaxLv <= lv;
		}
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

			public List<BleedDamageData> damageList = new List<BleedDamageData>();
		}

		public int afterHP;

		public List<BleedRegionWork> regionWorks = new List<BleedRegionWork>();
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
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.self != null && MonoBehaviourSingleton<StageObjectManager>.I.self.id == ownerID)
			{
				return true;
			}
			return false;
		}
	}

	[Serializable]
	public class ShadowSealingSyncData
	{
		public int regionIndex;
	}

	[Serializable]
	public class RegionWorkSyncData
	{
		public int hp;

		public bool isBroke;

		public List<BleedData> bleedList;

		public int barrierHp;

		public bool isShieldDamage;

		public bool isShieldCriticalDamage;

		public ShadowSealingData shadowSealingData;
	}

	public class RandomShotInfo
	{
		public class TargetInfo
		{
			public Quaternion rot;

			public int targetId;

			public TargetInfo()
			{
			}

			public TargetInfo(Quaternion rot, int id)
			{
				//IL_0007: Unknown result type (might be due to invalid IL or missing references)
				//IL_0008: Unknown result type (might be due to invalid IL or missing references)
				this.rot = rot;
				targetId = id;
			}
		}

		public const int TARGET_NONE = -1;

		public AttackInfo atkInfo;

		public float interval;

		public float countTime;

		public int shotCount;

		public List<Vector3> points
		{
			get;
			set;
		}

		public List<TargetInfo> targets
		{
			get;
			set;
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
		NOT_EXIST
	}

	public enum CANCEL_CONDITION
	{
		NONE,
		FAILED_GRAB
	}

	private enum EFFECTIVE_TYPE
	{
		GOOD,
		NORMAL,
		BAD
	}

	public const int INVALID_REGIONID = -1;

	public const int REGIONID_BODY = 0;

	private const string DEF_HEAD_NAME = "Head";

	private const string DEF_HIP_NAME = "Hip";

	public const int SHOT_POINT_ALL_PLAYER = 0;

	public const int SHOT_POINT_RANDOM_PICKUP = 1;

	public const int SHOT_POINT_CURRENT_TARGET = 3;

	public const float HPGAUGE_SHAKE_POWER = 5f;

	public const float HPGAUGE_SHAKE_TIME = 0.5f;

	public const float HPGAUGE_SHAKE_CYCLETIME = 0.05f;

	public const float ROTATE_RAD_PER_FRAME = 0.0349065848f;

	private const string SHIELD_PROPERTY_MATCAP_POW = "_MatCapPow";

	public const float SHADOWSEALING_START_NORMALIZED_TIME = 0.1f;

	private readonly uint kStrIdx_EnemyReaction_ElemTolChange;

	private readonly uint kStrIdx_EnemyReaction_Counter = 1u;

	private readonly uint kStrIdx_EnemyReaction_MadMode = 2u;

	private readonly uint kStrIdx_EnemyReaction_BuffCancellation = 3u;

	private readonly uint kStrIdx_EnemyReaction_BreakCounterRegion = 4u;

	private readonly uint kStrIdx_EnemyReaction_ReviveCounterRegion = 5u;

	private static int updateFrame = -1;

	private static float selfHitEffectCool = 0f;

	private static float otherHitEffectCool = 0f;

	public bool isStoke;

	private uint m_nowAngryId;

	private List<uint> m_execAngryIds = new List<uint>();

	public XorInt enemyLevel;

	public QuestStartData.EnemyReward enemyReward;

	public bool isRareSpecies;

	public float walkSpeedRateFromTable = 1f;

	public static readonly string[] subMotionStateName = new string[11]
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
		"angry_{0:00}"
	};

	private static int[] subMotionHashCaches = new int[27];

	[Tooltip("頭のオブジェクト名(頭からの距離測定起点)")]
	public string headObjectName = "Head";

	[Tooltip("尻のオブジェクト名(尻からの距離測定起点)")]
	public string hipObjectName = "Hip";

	private Transform _head;

	private Transform _hip;

	protected float bleedCounter;

	[Tooltip("体の大きさ半径（マップとの当たり、影の大きさ")]
	public float bodyRadius = 1f;

	[Tooltip("UI高さ")]
	public float uiHeight = 2f;

	[Tooltip("UI表示距離")]
	public float uiShowDistance;

	public AttackInfo[] convertAttackInfos;

	public RegionInfo[] regionInfos;

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

	protected Vector3 hitShockVec = Vector3.get_zero();

	protected Vector3 hitShockOffset = Vector3.get_zero();

	private bool canHitShockEffect = true;

	protected Vector3 dashBeforePos = Vector3.get_zero();

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

	protected List<RandomShotInfo> randomShotInfo = new List<RandomShotInfo>();

	protected List<RandomShotInfo> shotNetworkInfoQueue = new List<RandomShotInfo>();

	protected List<RandomShotInfo> shotEventInfoQueue = new List<RandomShotInfo>();

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

	private XorInt m_barrierHpMax = 0;

	private XorInt m_barrierHp = 0;

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

	private List<AnimationLayerWeightChangeInfo> animLayerWeightChangeInfo = new List<AnimationLayerWeightChangeInfo>();

	public ELEMENT_TYPE changeElementIcon = ELEMENT_TYPE.MAX;

	public ELEMENT_TYPE changeWeakElementIcon = ELEMENT_TYPE.MAX;

	public int changeToleranceRegionId = -1;

	public int changeToleranceScroll = -1;

	public BlendColorCtrl blendColorCtrl = new BlendColorCtrl();

	private SkinnedMeshRenderer[] skinnedMeshRendererList;

	private eCounterRegionState m_CounterRegionState;

	private GameObject effectDrainRecover;

	private float grabDrainRecoverTimer;

	private bool cachedIsFieldEnemyBoss;

	private bool isCachedIsFieldEnemyBoss;

	private GameObject m_effectBurning;

	private GameObject m_effectSpeedDown;

	private List<AttackHitColliderProcessor.HitParam> checkHitParam = new List<AttackHitColliderProcessor.HitParam>();

	private List<float> checkLength = new List<float>();

	private List<int> checkPriority = new List<int>();

	private List<int> targetRegionIds = new List<int>();

	private GameObject m_effectHitWhenGhost;

	private EnemyAegisController aegisCtrl;

	private List<ResidentEffectObject> m_residentEffectList = new List<ResidentEffectObject>();

	private SystemEffectSetting m_residentEffectSetting;

	public float lightRingHeightOffset;

	private float lightRingTime;

	private float lightRingRadius;

	private float lightRingHeight;

	protected Transform effectLightRing;

	private Transform debuffShadowSealingEffect;

	private float debuffShadowSealingTimer;

	private float debuffShadowSealingTimerDuration;

	private float debuffShadowSealingRadius;

	private int shadowSealingTarget;

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
			this.get_gameObject().set_name("Enemy:" + value);
		}
	}

	public EnemyLoader loader
	{
		get;
		private set;
	}

	public InGameSettingsManager.Enemy enemyParameter
	{
		get;
		private set;
	}

	public bool isPierceAfterTarget
	{
		get;
		private set;
	}

	public int enemyID
	{
		get;
		set;
	}

	public bool isBoss
	{
		get;
		set;
	}

	public bool isWaveMatchBoss
	{
		get;
		set;
	}

	public bool isBigMonster
	{
		get;
		set;
	}

	public int enemyPopIndex
	{
		get;
		set;
	}

	public EnemyTable.EnemyData enemyTableData
	{
		get;
		set;
	}

	public GrowEnemyTable.GrowEnemyData growTableData
	{
		get;
		set;
	}

	public BrainParam brainParam
	{
		get;
		set;
	}

	public Transform head
	{
		get
		{
			if (_head != null)
			{
				return _head;
			}
			_head = Utility.Find(base._transform, headObjectName);
			if (_head == null)
			{
				_head = base._transform;
			}
			_head = Utility.Find(base._transform, "Head");
			if (_head == null)
			{
				_head = base._transform;
			}
			return _head;
		}
	}

	public Transform hip
	{
		get
		{
			if (_hip != null)
			{
				return _hip;
			}
			_hip = Utility.Find(base._transform, hipObjectName);
			if (_hip == null)
			{
				_hip = base._transform;
			}
			_hip = Utility.Find(base._transform, "Hip");
			if (_hip == null)
			{
				_hip = base._transform;
			}
			return _hip;
		}
	}

	public RegionInfo[] convertRegionInfos
	{
		get;
		set;
	}

	public Collider[] colliders
	{
		get;
		protected set;
	}

	public TargetPoint[] targetPoints
	{
		get;
		protected set;
	}

	public RegionRoot[] regionRoots
	{
		get;
		protected set;
	}

	public bool enableTargetPoint
	{
		get;
		protected set;
	}

	public EnemyRegionWork[] regionWorks
	{
		get;
		set;
	}

	public int downMax
	{
		get
		{
			if (downMaxRate != null)
			{
				int num = downMaxRate.Length;
				if (num > 0)
				{
					return (int)((float)_downMax * downMaxRate[(downCount >= num - 1) ? (num - 1) : downCount]);
				}
			}
			return _downMax;
		}
	}

	public bool reviveRegionWaitSync
	{
		get;
		protected set;
	}

	public bool enableDash
	{
		get;
		protected set;
	}

	public bool warpWaitSync
	{
		get;
		protected set;
	}

	public string baseHitMaterialName
	{
		get;
		set;
	}

	public EnemyPacketReceiver enemyReceiver => (EnemyPacketReceiver)base.packetReceiver;

	public EnemyPacketSender enemySender => (EnemyPacketSender)base.packetSender;

	public int BarrierHpMax
	{
		get
		{
			return m_barrierHpMax;
		}
		set
		{
			m_barrierHpMax = value;
		}
	}

	public XorInt BarrierHp
	{
		get
		{
			return m_barrierHp;
		}
		set
		{
			m_barrierHp = value;
		}
	}

	public bool IsValidBarrier => BarrierHpMax > 0 && (int)BarrierHp > 0;

	public AtkAttribute GhostFormParam
	{
		get;
		set;
	}

	public GhostFormShaderParam GhostFormShaderParam
	{
		get;
		set;
	}

	public AutoBuffParam[] AutoBuffParamList
	{
		get;
		set;
	}

	public float DizzyReactionLoopTime
	{
		get;
		set;
	}

	public XorInt GrabHpMax
	{
		get;
		set;
	}

	public XorInt GrabHp
	{
		get;
		set;
	}

	public XorInt GrabCannonDamage
	{
		get;
		set;
	}

	public bool IsValidGrabHp => (int)GrabHpMax > 0 && (int)GrabHp > 0;

	public int ExActionID
	{
		get;
		set;
	}

	public int ExActionCondition
	{
		get;
		set;
	}

	public int ExActionConditionValue
	{
		get;
		set;
	}

	public StackBuffController stackBuffCtrl
	{
		get;
		protected set;
	}

	public uint NowAngryID
	{
		get
		{
			return m_nowAngryId;
		}
		set
		{
			m_nowAngryId = value;
		}
	}

	public List<uint> ExecAngryIDList
	{
		get
		{
			return m_execAngryIds;
		}
		set
		{
			m_execAngryIds = value;
		}
	}

	public bool counterFlag
	{
		get;
		set;
	}

	public Enemy()
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		enemyPopIndex = -1;
		enemyTableData = null;
		base.objectType = OBJECT_TYPE.ENEMY;
		enableTargetPoint = true;
		reviveRegionWaitSync = false;
		warpWaitSync = false;
		stackBuffCtrl = new StackBuffController();
	}

	public ENEMY_TYPE GetEnemyType()
	{
		return (enemyTableData != null) ? enemyTableData.type : ENEMY_TYPE.NONE;
	}

	public static bool IsWeakStateCheckAlreadyHit(WEAK_STATE state)
	{
		bool result = false;
		if (state == WEAK_STATE.WEAK || state == WEAK_STATE.WEAK_SP_ATTACK || state == WEAK_STATE.WEAK_SP_DOWN_MAX || state == WEAK_STATE.WEAK_ELEMENT_ATTACK || state == WEAK_STATE.WEAK_ELEMENT_SKILL_ATTACK || state == WEAK_STATE.WEAK_SKILL_ATTACK || state == WEAK_STATE.WEAK_HEAL_ATTACK || state == WEAK_STATE.WEAK_ELEMENT_SP_ATTACK)
		{
			result = true;
		}
		return result;
	}

	public static bool IsWeakStateSpAttack(WEAK_STATE state)
	{
		bool result = false;
		if (state == WEAK_STATE.WEAK_SP_ATTACK || state == WEAK_STATE.WEAK_SP_DOWN_MAX || state == WEAK_STATE.WEAK_ELEMENT_SP_ATTACK)
		{
			result = true;
		}
		return result;
	}

	public static bool IsWeakStateElementAttack(WEAK_STATE state)
	{
		bool result = false;
		if (state == WEAK_STATE.WEAK_ELEMENT_ATTACK || state == WEAK_STATE.WEAK_ELEMENT_SKILL_ATTACK || state == WEAK_STATE.WEAK_ELEMENT_SP_ATTACK)
		{
			result = true;
		}
		return result;
	}

	public static bool IsWeakStateSkillAttack(WEAK_STATE state)
	{
		bool result = false;
		if (state == WEAK_STATE.WEAK_ELEMENT_SKILL_ATTACK || state == WEAK_STATE.WEAK_SKILL_ATTACK)
		{
			result = true;
		}
		return result;
	}

	public static bool IsWeakStateHealAttack(WEAK_STATE state)
	{
		return state == WEAK_STATE.WEAK_HEAL_ATTACK;
	}

	public static bool IsWeakStateDisplaySign(WEAK_STATE state)
	{
		return state != 0 && state != WEAK_STATE.DOWN;
	}

	public static bool IsWeakStateCannonAttack(WEAK_STATE state)
	{
		return state == WEAK_STATE.WEAK_CANNON;
	}

	public static TargetMarker.EFFECT_TYPE WeakStateToEffectType(WEAK_STATE weakState)
	{
		TargetMarker.EFFECT_TYPE result = TargetMarker.EFFECT_TYPE.NONE;
		switch (weakState)
		{
		case WEAK_STATE.WEAK_ELEMENT_ATTACK:
			result = TargetMarker.EFFECT_TYPE.WEAK_ELEMENT_ATTACK;
			break;
		case WEAK_STATE.WEAK_ELEMENT_SKILL_ATTACK:
			result = TargetMarker.EFFECT_TYPE.WEAK_ELEMENT_SKILL_ATTACK;
			break;
		case WEAK_STATE.WEAK_SKILL_ATTACK:
			result = TargetMarker.EFFECT_TYPE.WEAK_SKILL_ATTACK;
			break;
		case WEAK_STATE.WEAK_HEAL_ATTACK:
			result = TargetMarker.EFFECT_TYPE.WEAK_HEAL_ATTACK;
			break;
		case WEAK_STATE.WEAK_CANNON:
			result = TargetMarker.EFFECT_TYPE.WEAK_CANNON;
			break;
		case WEAK_STATE.WEAK_ELEMENT_SP_ATTACK:
			result = TargetMarker.EFFECT_TYPE.WEAK_ELEMENT_SP_ATTACK;
			break;
		}
		return result;
	}

	public bool isDispWeakMark()
	{
		for (int i = 0; i < regionWorks.Length; i++)
		{
			if (regionWorks[i].weakState == WEAK_STATE.WEAK)
			{
				return true;
			}
		}
		return false;
	}

	public List<AttackActionMine> GetActionMineList()
	{
		return m_activeAttackActionMineList;
	}

	public DrainAttackInfo SearchDrainAttackInfo(int id)
	{
		if (drainAtkInfos == null)
		{
			return null;
		}
		DrainAttackInfo[] array = drainAtkInfos;
		foreach (DrainAttackInfo drainAttackInfo in array)
		{
			if (drainAttackInfo.id == id)
			{
				return drainAttackInfo;
			}
		}
		return null;
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid() && isBoss && MonoBehaviourSingleton<StageObjectManager>.I.boss == this)
		{
			MonoBehaviourSingleton<UIEnemyStatus>.I.SetTarget(this);
		}
		if (MonoBehaviourSingleton<DropTargetMarkerManeger>.IsValid())
		{
			MonoBehaviourSingleton<DropTargetMarkerManeger>.I.CheckTarget(this);
		}
		if (isRequireGhostShaderParam)
		{
			isRequireGhostShaderParam = false;
			ChangeGhostShaderParam(ghostBuffEndParam, ghostBuffDuration);
		}
		if (isHideSpawn)
		{
			if (isHiding)
			{
				InitHide();
			}
			else
			{
				TurnUpImmediate();
			}
		}
	}

	protected override void OnDisable()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		base.OnDisable();
		if (forceEnemyOutCoroutine != null)
		{
			StopForceEnemyOut();
			MonoBehaviourSingleton<CoopNetworkManager>.I.EnemyOut(id, _position);
		}
		if (isBoss && MonoBehaviourSingleton<StageObjectManager>.I.boss == this && MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIEnemyStatus>.I.SetTarget(null);
		}
		DeleteStatusGizmo();
	}

	public void CreateStatusGizmo()
	{
		if (!isBoss && !(uiEnemyStatusGizmo != null) && !isHiding && MonoBehaviourSingleton<UIStatusGizmoManager>.IsValid())
		{
			uiEnemyStatusGizmo = MonoBehaviourSingleton<UIStatusGizmoManager>.I.Create(this);
		}
	}

	public void DeleteStatusGizmo()
	{
		if (uiEnemyStatusGizmo != null)
		{
			uiEnemyStatusGizmo.targetEnemy = null;
			uiEnemyStatusGizmo = null;
		}
	}

	protected override void Awake()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		base.Awake();
		loader = this.get_gameObject().AddComponent<EnemyLoader>();
		enemyParameter = MonoBehaviourSingleton<InGameSettingsManager>.I.enemy;
		downMaxRate = enemyParameter.downMaxRate;
		isPierceAfterTarget = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.isPierceAfterTarget;
		EnemyParam componentInChildren = this.get_gameObject().GetComponentInChildren<EnemyParam>();
		if (componentInChildren != null)
		{
			componentInChildren.SetParam(this);
			Object.DestroyImmediate(componentInChildren);
			componentInChildren = null;
		}
		if (base._rigidbody == null)
		{
			base._rigidbody = this.get_gameObject().AddComponent<Rigidbody>();
		}
		base._rigidbody.set_collisionDetectionMode(1);
		base._rigidbody.set_mass(1000f);
		base._rigidbody.set_angularDrag(100f);
		base._rigidbody.set_isKinematic(false);
		base._rigidbody.set_constraints(116);
		this.get_gameObject().set_layer(10);
		stackBuffCtrl.Init();
	}

	protected override void Clear()
	{
		base.Clear();
		regionInfos = null;
		colliders = null;
		targetPoints = null;
		regionRoots = null;
		m_nowAngryId = 0u;
		if (m_execAngryIds != null)
		{
			m_execAngryIds.Clear();
		}
	}

	public override void OnLoadComplete()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ca: Unknown result type (might be due to invalid IL or missing references)
		if (base._collider == null)
		{
			SphereCollider val = this.get_gameObject().AddComponent<SphereCollider>();
			val.set_radius(bodyRadius);
			val.set_center(new Vector3(0f, bodyRadius, 0f));
			base._collider = val;
		}
		Rigidbody val2 = base.body.get_gameObject().GetComponentInChildren<Rigidbody>();
		if (val2 == null)
		{
			val2 = base.body.get_gameObject().AddComponent<Rigidbody>();
		}
		val2.set_useGravity(false);
		val2.set_isKinematic(true);
		EnemyTable.EnemyData enemyTableData = this.enemyTableData;
		if (enemyTableData != null)
		{
			float num = 0f;
			float num2 = 0f;
			if (growTableData != null)
			{
				num = growTableData.hpRate;
				num2 = growTableData.atkRate;
			}
			else
			{
				num = enemyTableData.hpRate;
				num2 = enemyTableData.atkRate;
			}
			if (regionInfos == null)
			{
				Log.Error("regionInfos is null. Check enemy data.");
			}
			if (!string.IsNullOrEmpty(enemyTableData.convertRegionKey))
			{
				int i = 0;
				for (int num3 = attackInfos.Length; i < num3; i++)
				{
					string b = attackInfos[i].name + "_" + enemyTableData.convertRegionKey;
					int j = 0;
					for (int num4 = convertAttackInfos.Length; j < num4; j++)
					{
						if (convertAttackInfos[j].name == b)
						{
							convertAttackInfos[j].name = attackInfos[i].name;
							attackInfos[i] = convertAttackInfos[j];
							break;
						}
					}
				}
				int k = 0;
				for (int num5 = regionInfos.Length; k < num5; k++)
				{
					string b2 = regionInfos[k].name + "_" + enemyTableData.convertRegionKey;
					int l = 0;
					for (int num6 = convertRegionInfos.Length; l < num6; l++)
					{
						if (convertRegionInfos[l].name == b2)
						{
							convertRegionInfos[l].name = regionInfos[k].name;
							regionInfos[k] = convertRegionInfos[l];
							break;
						}
					}
				}
			}
			int m = 0;
			for (int num7 = regionInfos.Length; m < num7; m++)
			{
				float num8 = (float)regionInfos[m].maxHP * num;
				regionInfos[m].maxHP = (int)(num8 + 0.001f);
				regionInfos[m].tolerance.InitializeElementTolerance(converteElementToleranceTable);
			}
			int n = 0;
			for (int num9 = attackInfos.Length; n < num9; n++)
			{
				AttackHitInfo attackHitInfo = attackInfos[n] as AttackHitInfo;
				if (attackHitInfo != null)
				{
					attackHitInfo.atkRate = num2;
				}
			}
		}
		base.OnLoadComplete();
		SetAnimUpdatePhysics(isBoss);
		InitializeRegionWork();
		BarrierHp = BarrierHpMax;
		base.ignoreHitAttackColliders.Clear();
		EnemyColliderSettings[] componentsInChildren = base.body.GetComponentsInChildren<EnemyColliderSettings>();
		int num10 = 0;
		for (int num11 = componentsInChildren.Length; num10 < num11; num10++)
		{
			if (!(componentsInChildren[num10].targetCollider == null) && componentsInChildren[num10].ignoreHitAttack)
			{
				base.ignoreHitAttackColliders.Add(componentsInChildren[num10].targetCollider);
			}
		}
		Utility.SetLayerWithChildren(base._transform, 11);
		this.get_gameObject().set_layer(10);
		colliders = this.get_gameObject().GetComponentsInChildren<Collider>();
		targetPoints = this.get_gameObject().GetComponentsInChildren<TargetPoint>();
		regionRoots = this.get_gameObject().GetComponentsInChildren<RegionRoot>();
		int num12 = 0;
		for (int num13 = targetPoints.Length; num12 < num13; num12++)
		{
			targetPoints[num12].owner = this;
			int num14 = 0;
			for (int num15 = regionRoots.Length; num14 < num15; num14++)
			{
				if (Array.IndexOf(regionRoots[num14].subRegionIDs, targetPoints[num12].regionID) >= 0)
				{
					targetPoints[num12].subRegionRoot = regionRoots[num14];
				}
			}
		}
		int num16 = 0;
		for (int num17 = regionRoots.Length; num16 < num17; num16++)
		{
			if (regionRoots[num16].isDeactive)
			{
				regionRoots[num16].get_gameObject().SetActive(false);
			}
		}
		if (stepCtrl != null)
		{
			stepCtrl.stampDistance = enemyParameter.stampDistance;
		}
		if (MonoBehaviourSingleton<TargetMarkerManager>.IsValid())
		{
			MonoBehaviourSingleton<TargetMarkerManager>.I.updateShadowSealingFlag = true;
		}
		if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid() && isBoss)
		{
			MonoBehaviourSingleton<UIEnemyStatus>.I.SetTarget(this);
		}
		if (MonoBehaviourSingleton<DropTargetMarkerManeger>.IsValid())
		{
			MonoBehaviourSingleton<DropTargetMarkerManeger>.I.CheckTarget(this);
		}
		if (!willStock)
		{
			AutoBuffProc();
			if (isHideSpawn)
			{
				isHiding = true;
				InitHide();
			}
		}
		InitializeBarrierEffect();
		tailController = this.get_gameObject().GetComponentInChildren<TailController>(true);
		willStock = false;
		ColliderWeightCtl[] componentsInChildren2 = this.get_gameObject().GetComponentsInChildren<ColliderWeightCtl>();
		int num18 = 0;
		for (int num19 = componentsInChildren2.Length; num18 < num19; num18++)
		{
			componentsInChildren2[num18].SetAnimator(loader.GetAnimator());
		}
		TargetPointWeightCtl[] componentsInChildren3 = this.get_gameObject().GetComponentsInChildren<TargetPointWeightCtl>();
		int num20 = 0;
		for (int num21 = componentsInChildren3.Length; num20 < num21; num20++)
		{
			componentsInChildren3[num20].SetAnimator(loader.GetAnimator());
		}
		skinnedMeshRendererList = this.GetComponentsInChildren<SkinnedMeshRenderer>();
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.HasArenaInfo())
		{
			arenaConditionList = MonoBehaviourSingleton<InGameManager>.I.GetArenaConditions();
			if (!arenaConditionList.IsNullOrEmpty())
			{
				for (int num22 = 0; num22 < arenaConditionList.Length; num22++)
				{
					switch (arenaConditionList[num22])
					{
					case ARENA_CONDITION.DAMAGE_OFF_WEAPON:
						isArenaDamageOffWeapon = true;
						break;
					case ARENA_CONDITION.DAMAGE_OFF_MAGI:
						isArenaDamageOffMagi = true;
						break;
					}
				}
			}
		}
	}

	private void AutoBuffProc()
	{
		if (AutoBuffParamList != null && AutoBuffParamList.Length > 0)
		{
			AutoBuffParam[] autoBuffParamList = AutoBuffParamList;
			foreach (AutoBuffParam autoBuffParam in autoBuffParamList)
			{
				float num = autoBuffParam.time;
				if (num < 0f)
				{
					num = 43200f;
				}
				BuffParam.BuffData buffData = new BuffParam.BuffData();
				buffData.type = autoBuffParam.type;
				buffData.time = num;
				buffData.value = autoBuffParam.value;
				OnBuffStart(buffData);
			}
		}
	}

	private void InitializeRegionWork()
	{
		if (regionWorks == null)
		{
			regionWorks = new EnemyRegionWork[regionInfos.Length];
		}
		for (int i = 0; i < regionWorks.Length; i++)
		{
			RegionInfo regionInfo = regionInfos[i];
			int parentRegionId = SearchRegionID(regionInfo.parentRegionName);
			if (regionWorks[i] == null)
			{
				regionWorks[i] = new EnemyRegionWork();
			}
			regionWorks[i].Initialize(regionInfo, parentRegionId, i);
		}
		if (regionWorks.Length > 0)
		{
			int num3 = base.hpMax = (base.hp = regionWorks[0].hp);
			if (madModeHpThreshold > 0 && (int)enemyLevel >= madModeLvThreshold)
			{
				madModeHp = Mathf.CeilToInt((float)madModeHpThreshold * 0.01f * (float)base.hpMax);
			}
			else
			{
				madModeHp = 0;
			}
		}
	}

	private int SearchRegionID(string targetRegionName)
	{
		if (string.IsNullOrEmpty(targetRegionName))
		{
			return -1;
		}
		if (regionInfos == null || regionInfos.Length <= 0)
		{
			return -1;
		}
		for (int i = 0; i < regionInfos.Length; i++)
		{
			if (regionInfos[i].name == targetRegionName)
			{
				return i;
			}
		}
		return -1;
	}

	protected override void Initialize()
	{
		base.Initialize();
		if (!isBoss)
		{
			base.isLocalDamageApply = true;
			localDamage = 0;
		}
	}

	public void ApplyExploreBossStatus(ExploreBossStatus status)
	{
		if (status != null)
		{
			base.hp = status.hp;
			BarrierHp = status.barrierHp;
			downCount = status.downCount;
			base.ShieldHp = status.shieldHp;
			if (status.regionWorks != null)
			{
				for (int i = 0; i < regionWorks.Length; i++)
				{
					if (status.regionWorks.Length > i)
					{
						regionWorks[i].CopyFrom(status.regionWorks[i]);
					}
				}
			}
			UpdateRegionVisual();
			if (IsValidShield())
			{
				RequestShieldShaderEffect();
			}
			NowAngryID = status.nowAngryId;
			m_execAngryIds.Clear();
			if (status.execAngryIds != null && status.execAngryIds.Length > 0)
			{
				m_execAngryIds.AddRange(status.execAngryIds);
			}
			if (status.isMadMode)
			{
				LocalMadModeStart();
			}
		}
	}

	protected override uint GetVoiceChannel()
	{
		return 1u;
	}

	protected override bool EnablePlaySound()
	{
		if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			return true;
		}
		if (isBoss)
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
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if (base.isLoading)
		{
			base.isDestroyWaitFlag = true;
			return false;
		}
		aegisCtrl = null;
		if (isBoss)
		{
			return base.DestroyObject();
		}
		if (base.packetSender != null)
		{
			base.packetSender.OnDestroyObject();
		}
		this.get_gameObject().SetActive(false);
		MonoBehaviourSingleton<StageObjectManager>.I.enemyStokeList.Add(this);
		base.isDestroyWaitFlag = false;
		return true;
	}

	public void ClearDead()
	{
		base._collider.set_enabled(true);
		if (colliders != null)
		{
			int i = 0;
			for (int num = colliders.Length; i < num; i++)
			{
				colliders[i].set_enabled(true);
			}
		}
		badStatusMax.Copy(badStatusBase);
		base.isDead = false;
		hitOffFlag = HIT_OFF_FLAG.NONE;
		base.isSetAppearPos = false;
		isStoke = true;
		base.isCoopInitialized = false;
		animatorBoolList.Clear();
		changeTriggerList.Clear();
		ActIdle(false, -1f);
		localDamage = 0;
		isRequireGhostShaderParam = false;
		isHiding = isHideSpawn;
		EnemyController enemyController = base.controller as EnemyController;
		if (enemyController != null)
		{
			enemyController.Reset();
		}
		InitializeRegionWork();
		BarrierHp = BarrierHpMax;
		StopForceEnemyOut();
		AutoBuffProc();
	}

	protected override void Update()
	{
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		base.Update();
		if (hitShockLightFlag)
		{
			hitShockLightTime += Time.get_deltaTime();
			if (hitShockLightTime >= enemyParameter.hitShockLightTime)
			{
				loader.ResetRimParams();
				hitShockLightFlag = false;
				hitShockLightTime = 0f;
			}
			else if (loader.materialParamsList != null)
			{
				int ID_RIM_POWER = Shader.PropertyToID("_RimPower");
				int ID_RIM_WIDTH = Shader.PropertyToID("_RimWidth");
				float num = hitShockLightTime / enemyParameter.hitShockLightTime;
				float sin = Mathf.Sin(3.14159274f * num);
				loader.materialParamsList.ForEach(delegate(EnemyLoader.MaterialParams prm)
				{
					if (prm.hasRimPower)
					{
						float num5 = prm.defaultRimPower + (enemyParameter.hitShockLightRimPower - prm.defaultRimPower) * sin;
						prm.material.SetFloat(ID_RIM_POWER, num5);
					}
					if (prm.hasRimWidth)
					{
						float num6 = prm.defaultRimWidth + (enemyParameter.hitShockLightRimWidth - prm.defaultRimWidth) * sin;
						prm.material.SetFloat(ID_RIM_WIDTH, num6);
					}
				});
			}
		}
		if (warpViewFlag)
		{
			warpViewRate += warpViewRatePerTime * Time.get_deltaTime();
			if (warpViewRatePerTime >= 0f && warpViewRate >= 1f)
			{
				warpViewRate = 1f;
				warpViewFlag = false;
			}
			else if (warpViewRatePerTime < 0f && warpViewRate <= 0f)
			{
				warpViewRate = 0f;
				warpViewFlag = false;
			}
			SetWarpVisible(warpViewRate);
		}
		else if (warpViewRate > 0f)
		{
			SetWarpVisible(warpViewRate);
		}
		downHealInterval -= Time.get_deltaTime();
		if (downHealInterval <= 0f)
		{
			downHealInterval = 0f;
			downTotal -= downHeal * Time.get_deltaTime();
			if (downTotal < 0f)
			{
				downTotal = 0f;
			}
		}
		if (hitShockOffsetFlag)
		{
			hitShockOffsetTime += Time.get_deltaTime();
			if (hitShockOffsetTime >= enemyParameter.hitShockOffsetTime)
			{
				base.body.set_localPosition(Vector3.get_zero());
				hitShockOffset = Vector3.get_zero();
				hitShockOffsetFlag = false;
				hitShockOffsetTime = 0f;
			}
			else
			{
				float num2 = hitShockOffsetTime / enemyParameter.hitShockOffsetTime;
				object body = (object)base.body;
				body.set_position(body.get_position() - hitShockOffset);
				hitShockOffset = hitShockVec * (enemyParameter.hitShockOffsetLength * Mathf.Sin(3.14159274f * num2));
				object body2 = (object)base.body;
				body2.set_position(body2.get_position() + hitShockOffset);
			}
		}
		UpdateRandomShot();
		if (regionWorks != null)
		{
			int num3 = regionWorks.Length;
			for (int i = 0; i < num3; i++)
			{
				regionWorks[i].Update();
			}
		}
		_UpdateBleed();
		_UpdateShadowSealing();
		if (base.isInitialized && isBoss)
		{
			DrainRecoverProc();
		}
		int count = animLayerWeightChangeInfo.Count;
		if (count > 0)
		{
			Animator animator = loader.GetAnimator();
			for (int j = 0; j < count; j++)
			{
				AnimationLayerWeightChangeInfo animationLayerWeightChangeInfo = animLayerWeightChangeInfo[j];
				if (animationLayerWeightChangeInfo.aliveFlag)
				{
					float num4 = animationLayerWeightChangeInfo.spd * Time.get_deltaTime();
					if (num4 > 0f)
					{
						if (animationLayerWeightChangeInfo.weight + num4 >= animationLayerWeightChangeInfo.target)
						{
							animationLayerWeightChangeInfo.weight = animationLayerWeightChangeInfo.target;
							animationLayerWeightChangeInfo.aliveFlag = false;
						}
						else
						{
							animationLayerWeightChangeInfo.weight += num4;
						}
					}
					else if (animationLayerWeightChangeInfo.weight + num4 <= animationLayerWeightChangeInfo.target)
					{
						animationLayerWeightChangeInfo.weight = animationLayerWeightChangeInfo.target;
						animationLayerWeightChangeInfo.aliveFlag = false;
					}
					else
					{
						animationLayerWeightChangeInfo.weight += num4;
					}
					animator.SetLayerWeight(animationLayerWeightChangeInfo.layerIndex, animationLayerWeightChangeInfo.weight);
				}
			}
		}
		if (blendColorCtrl != null)
		{
			blendColorCtrl.Update();
		}
	}

	private void _UpdateBleed()
	{
		if ((IsCoopNone() || IsOriginal()) && !object.ReferenceEquals(regionWorks, null))
		{
			float num = bleedCounter;
			bleedCounter += Time.get_deltaTime();
			float arrowBleedTimeInterval = MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.arrowBleedTimeInterval;
			if ((int)(num / arrowBleedTimeInterval) != (int)(bleedCounter / arrowBleedTimeInterval))
			{
				bool flag = false;
				BleedSyncData bleedSyncData = new BleedSyncData();
				bleedSyncData.afterHP = base.hp;
				int i = 0;
				for (int num2 = regionWorks.Length; i < num2; i++)
				{
					bool flag2 = false;
					if ((int)regionWorks[i].hp <= 0 && regionInfos[i].maxHP > 0)
					{
						flag2 = true;
					}
					if (regionInfos[i].breakAfterHit || !flag2)
					{
						BleedSyncData.BleedRegionWork bleedRegionWork = null;
						int j = 0;
						for (int count = regionWorks[i].bleedList.Count; j < count; j++)
						{
							BleedData bleedData = regionWorks[i].bleedList[j];
							flag = true;
							if (bleedRegionWork == null)
							{
								bleedRegionWork = new BleedSyncData.BleedRegionWork();
								bleedRegionWork.id = i;
								bleedRegionWork.afterHP = regionWorks[i].hp;
								bleedRegionWork.damageList = new List<BleedSyncData.BleedDamageData>();
							}
							BleedSyncData.BleedDamageData bleedDamageData = new BleedSyncData.BleedDamageData();
							bleedRegionWork.damageList.Add(bleedDamageData);
							if (bleedData.skipFirst)
							{
								bleedDamageData.ownerID = bleedData.ownerID;
								bleedDamageData.damage = 0;
							}
							else
							{
								int num3 = Mathf.CeilToInt((float)bleedData.damage);
								if (bleedSyncData.afterHP > 0 && !regionInfos[i].dragonArmorInfo.enabled)
								{
									bleedSyncData.afterHP -= num3;
									if (bleedSyncData.afterHP < 1)
									{
										bleedSyncData.afterHP = 1;
									}
								}
								if (bleedRegionWork.afterHP > 0)
								{
									bleedRegionWork.afterHP -= num3;
									if (bleedRegionWork.afterHP < 1)
									{
										bleedRegionWork.afterHP = 1;
									}
								}
								bleedDamageData.ownerID = bleedData.ownerID;
								bleedDamageData.damage = num3;
							}
						}
						if (bleedRegionWork != null)
						{
							bleedSyncData.regionWorks.Add(bleedRegionWork);
						}
					}
				}
				if (flag)
				{
					OnUpdateBleedDamage(bleedSyncData);
				}
			}
		}
	}

	private void _UpdateShadowSealing()
	{
		if (!IsDebuffShadowSealing() && (IsCoopNone() || IsOriginal()) && !object.ReferenceEquals(regionWorks, null))
		{
			int i = 0;
			for (int num = regionWorks.Length; i < num; i++)
			{
				ShadowSealingData shadowSealingData = regionWorks[i].shadowSealingData;
				if (shadowSealingData.ownerID != 0)
				{
					shadowSealingData.existSec -= Time.get_deltaTime();
					if (shadowSealingData.existSec <= 0f)
					{
						ShadowSealingSyncData shadowSealingSyncData = new ShadowSealingSyncData();
						shadowSealingSyncData.regionIndex = i;
						OnUpdateShadowSealing(shadowSealingSyncData);
					}
				}
			}
		}
	}

	private void DrainRecoverProc()
	{
		if (IsCoopNone() || IsOriginal())
		{
			EnemyBrain enemyBrain = base.controller.brain as EnemyBrain;
			if (!(enemyBrain == null))
			{
				GrabController grabController = enemyBrain.actionCtrl.grabController;
				if (grabController != null && grabController.IsGrabing())
				{
					DrainAttackInfo drainAtkInfo = grabController.drainAtkInfo;
					if (drainAtkInfo != null && grabController.IsAliveGrabbedPlayerAll())
					{
						int num = (int)((float)base.hpMax * (drainAtkInfo.recoverRate * 0.01f));
						if (num > 0 && base.hp < base.hpMax)
						{
							grabDrainRecoverTimer -= Time.get_deltaTime();
							if (grabDrainRecoverTimer <= 0f)
							{
								RecoverHp(num, true);
								grabDrainRecoverTimer = drainAtkInfo.recoverInterval;
							}
						}
					}
				}
			}
		}
	}

	public void RecoverHp(int recoverValue, bool isSend)
	{
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		base.hp += recoverValue;
		if (base.hp > base.hpMax)
		{
			base.hp = base.hpMax;
		}
		if (MonoBehaviourSingleton<UIDamageManager>.IsValid())
		{
			MonoBehaviourSingleton<UIDamageManager>.I.CreateEnemyRecoverHp(this, recoverValue, UIPlayerDamageNum.DAMAGE_COLOR.HEAL);
		}
		if (enemySender != null && isSend)
		{
			enemySender.OnRecoverHp(recoverValue);
		}
		if (base.effectPlayProcessor != null && effectDrainRecover == null)
		{
			List<EffectPlayProcessor.EffectSetting> settings = base.effectPlayProcessor.GetSettings("RECOVER_HP");
			if (settings != null && settings.Count > 0)
			{
				Transform val = base.effectPlayProcessor.PlayEffect(settings[0], base._transform);
				if (val != null)
				{
					effectDrainRecover = val.get_gameObject();
				}
			}
		}
	}

	protected override void FixedUpdate()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		ACTION_ID actionID = base.actionID;
		if (actionID == ACTION_ID.ATTACK && enableDash)
		{
			if (IsWallStay())
			{
				SetDashEnd();
			}
			else
			{
				Vector3 val = _position - dashBeforePos;
				val.y = 0f;
				dashNowDistance += val.get_magnitude();
				dashBeforePos = _position;
				bool flag = dashNowDistance >= dashMinDistance;
				if (!base.actionPositionFlag)
				{
					if (flag)
					{
						SetDashEnd();
						goto IL_0174;
					}
				}
				else if (!dashOverFlag)
				{
					if (dashNowDistance >= dashMaxDistance)
					{
						dashOverFlag = true;
						dashOverCheckDistance = dashNowDistance;
					}
					else
					{
						Vector3 val2 = base.actionPosition - _position;
						val2.y = 0f;
						Vector3 forward = _forward;
						forward.y = 0f;
						forward.Normalize();
						float num = Vector3.Angle(forward, val2);
						if (num > 90f)
						{
							dashOverFlag = true;
							dashOverCheckDistance = dashNowDistance;
						}
					}
				}
				if (dashOverFlag && flag && dashNowDistance - dashOverCheckDistance >= dashOverDistance)
				{
					SetDashEnd();
				}
			}
		}
		goto IL_0174;
		IL_0174:
		base.FixedUpdate();
	}

	protected override void LateUpdate()
	{
		base.LateUpdate();
		if (updateFrame != Time.get_frameCount())
		{
			updateFrame = Time.get_frameCount();
			selfHitEffectCool -= Time.get_deltaTime();
			if (selfHitEffectCool < 0f)
			{
				selfHitEffectCool = 0f;
			}
			otherHitEffectCool -= Time.get_deltaTime();
			if (otherHitEffectCool < 0f)
			{
				otherHitEffectCool = 0f;
			}
		}
	}

	public override void SetActionTarget(StageObject target, bool send = true)
	{
		bool flag = false;
		if (base.actionTarget != target)
		{
			flag = true;
		}
		base.actionTarget = target;
		if (send && flag && base.characterSender != null)
		{
			base.characterSender.OnSetActionTarget(target);
		}
	}

	public void OnUpdateBleedDamage(BleedSyncData sync_data)
	{
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Expected O, but got Unknown
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Expected O, but got Unknown
		int num = base.hp - sync_data.afterHP;
		if (num < 0)
		{
			num = 0;
		}
		base.hp = sync_data.afterHP;
		if (regionWorks != null)
		{
			int i = 0;
			for (int num2 = regionWorks.Length; i < num2; i++)
			{
				BleedSyncData.BleedRegionWork bleedRegionWork = null;
				int j = 0;
				for (int count = sync_data.regionWorks.Count; j < count; j++)
				{
					if (sync_data.regionWorks[j].id == i)
					{
						bleedRegionWork = sync_data.regionWorks[j];
						regionWorks[i].hp = bleedRegionWork.afterHP;
						break;
					}
				}
				int num3 = 0;
				while (num3 < regionWorks[i].bleedList.Count)
				{
					BleedData bleedData = regionWorks[i].bleedList[num3];
					BleedWork bleedWork = null;
					int k = 0;
					for (int count2 = regionWorks[i].bleedWorkList.Count; k < count2; k++)
					{
						if (regionWorks[i].bleedWorkList[k].ownerID == bleedData.ownerID)
						{
							bleedWork = regionWorks[i].bleedWorkList[k];
							break;
						}
					}
					BleedSyncData.BleedDamageData bleedDamageData = null;
					if (bleedRegionWork != null)
					{
						int l = 0;
						for (int count3 = bleedRegionWork.damageList.Count; l < count3; l++)
						{
							if (bleedRegionWork.damageList[l].ownerID == bleedData.ownerID)
							{
								bleedDamageData = bleedRegionWork.damageList[l];
								break;
							}
						}
					}
					bool flag = false;
					bool flag2 = false;
					if (bleedData.skipFirst)
					{
						bleedData.skipFirst = false;
						if (bleedDamageData != null && bleedDamageData.damage == 0)
						{
							flag2 = true;
						}
					}
					if (bleedDamageData == null)
					{
						flag = true;
					}
					else if (!flag2)
					{
						bleedData.cnt--;
						if (bleedData.cnt <= 0)
						{
							flag = true;
						}
					}
					if (bleedDamageData != null && !flag2)
					{
						if (MonoBehaviourSingleton<CoopManager>.IsValid())
						{
							MonoBehaviourSingleton<CoopManager>.I.coopStage.battleUserLog.Add(this, bleedDamageData);
						}
						int num4 = bleedDamageData.damage;
						if (num4 > num)
						{
							num4 = num;
						}
						if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
						{
							MonoBehaviourSingleton<InGameRecorder>.I.RecordGivenDamage(bleedDamageData.ownerID, num4);
						}
						if (QuestManager.IsValidInGameExplore() && isBoss && bleedData.IsOwnerSelf())
						{
							ExplorePlayerStatus myExplorePlayerStatus = MonoBehaviourSingleton<QuestManager>.I.GetMyExplorePlayerStatus();
							int num5 = myExplorePlayerStatus.givenTotalDamage + num4;
							myExplorePlayerStatus.SyncTotalDamageToBoss(num5);
							MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendExploreBossDamage(num5);
						}
						num -= num4;
						if (bleedData.IsOwnerSelf() && bleedWork != null && bleedWork.bleedEffect != null)
						{
							if (enemyParameter.showDamageNum && MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.arrowBleedShowDamage)
							{
								AtkAttribute atkAttribute = new AtkAttribute();
								atkAttribute.normal = (float)bleedDamageData.damage;
								bool enabled = regionWorks[i].regionInfo.dragonArmorInfo.enabled;
								CreateDamageNum(bleedWork.bleedEffect.get_position(), atkAttribute, false, enabled, 0);
							}
							if (warpViewRate <= 0f)
							{
								Transform effect = EffectManager.GetEffect(MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.arrowBleedDamageEffectName, bleedWork.bleedEffect.get_parent());
								if (effect != null)
								{
									effect.set_localScale(bleedWork.bleedEffect.get_localScale());
									effect.set_localPosition(bleedWork.bleedEffect.get_localPosition());
									effect.set_localRotation(bleedWork.bleedEffect.get_localRotation());
								}
							}
						}
					}
					if (flag)
					{
						if (bleedWork != null)
						{
							if (bleedWork.bleedEffect != null)
							{
								EffectManager.ReleaseEffect(bleedWork.bleedEffect.get_gameObject(), true, false);
								bleedWork.bleedEffect = null;
							}
							regionWorks[i].bleedWorkList.Remove(bleedWork);
						}
						regionWorks[i].bleedList.RemoveAt(num3);
					}
					else
					{
						num3++;
					}
				}
			}
			if (IsMirror() || IsPuppet())
			{
				bool flag3 = false;
				int m = 0;
				for (int num6 = regionWorks.Length; m < num6; m++)
				{
					if (regionWorks[m].bleedList.Count > 0)
					{
						flag3 = true;
					}
				}
				if (flag3)
				{
					float arrowBleedTimeInterval = MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.arrowBleedTimeInterval;
					StartWaitingPacket(WAITING_PACKET.ENEMY_UPDATE_BLEED_DAMAGE, false, arrowBleedTimeInterval * 2f);
				}
				else
				{
					EndWaitingPacket(WAITING_PACKET.ENEMY_UPDATE_BLEED_DAMAGE);
				}
			}
		}
		if (enemySender != null)
		{
			enemySender.OnUpdateBleedDamage(sync_data);
		}
	}

	public void ClearBleedDamageAll()
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		if (regionWorks != null)
		{
			int i = 0;
			for (int num = regionWorks.Length; i < num; i++)
			{
				regionWorks[i].bleedList.Clear();
				int j = 0;
				for (int count = regionWorks[i].bleedWorkList.Count; j < count; j++)
				{
					BleedWork bleedWork = regionWorks[i].bleedWorkList[j];
					if (bleedWork.bleedEffect != null)
					{
						EffectManager.ReleaseEffect(bleedWork.bleedEffect.get_gameObject(), true, false);
						bleedWork.bleedEffect = null;
					}
				}
				regionWorks[i].bleedWorkList.Clear();
			}
		}
		if (IsMirror() || IsPuppet())
		{
			EndWaitingPacket(WAITING_PACKET.ENEMY_UPDATE_BLEED_DAMAGE);
		}
	}

	public void OnUpdateShadowSealing(ShadowSealingSyncData syncData)
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		if (!object.ReferenceEquals(regionWorks, null))
		{
			EnemyRegionWork enemyRegionWork = regionWorks[syncData.regionIndex];
			ShadowSealingData shadowSealingData = enemyRegionWork.shadowSealingData;
			shadowSealingData.ownerID = 0;
			shadowSealingData.existSec = 0f;
			shadowSealingData.extendRate = 1f;
			if (!object.ReferenceEquals(enemyRegionWork.shadowSealingEffect, null))
			{
				EffectManager.ReleaseEffect(enemyRegionWork.shadowSealingEffect.get_gameObject(), true, false);
				enemyRegionWork.shadowSealingEffect = null;
			}
		}
		if (IsMirror() || IsPuppet())
		{
			EndWaitingPacket(WAITING_PACKET.ENEMY_UPDATE_SHADOWSEALING);
		}
		if (enemySender != null)
		{
			enemySender.OnUpdateShadowSealing(syncData);
		}
	}

	public void ClearShadowSealingAll(bool isClearOwnerID = true, bool isEndPacket = true)
	{
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		if (!object.ReferenceEquals(regionWorks, null))
		{
			int i = 0;
			for (int num = regionWorks.Length; i < num; i++)
			{
				ShadowSealingData shadowSealingData = regionWorks[i].shadowSealingData;
				if (isClearOwnerID)
				{
					shadowSealingData.ownerID = 0;
				}
				shadowSealingData.existSec = 0f;
				shadowSealingData.extendRate = 1f;
				if (!object.ReferenceEquals(regionWorks[i].shadowSealingEffect, null))
				{
					EffectManager.ReleaseEffect(regionWorks[i].shadowSealingEffect.get_gameObject(), true, false);
					regionWorks[i].shadowSealingEffect = null;
				}
			}
		}
		if (isEndPacket && (IsMirror() || IsPuppet()))
		{
			EndWaitingPacket(WAITING_PACKET.ENEMY_UPDATE_SHADOWSEALING);
		}
	}

	public void SetDashEnd()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (enableDash)
		{
			SetChangeTrigger(dashEndTrigger);
			enableDash = false;
			dashBeforePos = Vector3.get_zero();
			dashNowDistance = 0f;
			dashOverDistance = 0f;
			dashMinDistance = 0f;
			dashMaxDistance = 0f;
			dashEndTrigger = null;
			dashOverFlag = false;
			dashOverCheckDistance = 0f;
			rotateSafeMode = false;
		}
	}

	public override void ActDead(bool force_sync = false, bool recieve_direct = false)
	{
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		ActReleaseGrabbedPlayers(false, false, true, 0f, 0f);
		base.badStatusTotal.Reset();
		badStatusMax.Copy(badStatusBase);
		base.ActDead(force_sync, recieve_direct);
		PlayMotion(7, 0f);
		PrepareEnemyOut();
		if (MonoBehaviourSingleton<CoopNetworkManager>.IsValid())
		{
			if (IsCoopNone() || IsOriginal())
			{
				StopForceEnemyOut();
				MonoBehaviourSingleton<CoopNetworkManager>.I.EnemyOut(id, _position);
			}
			else if (force_sync)
			{
				ForceEnemyOut();
			}
		}
		bool flag = false;
		if (MonoBehaviourSingleton<StageObjectManager>.I.boss == this)
		{
			UpdateBreakIDLists();
			if (QuestManager.IsValidInGame() && MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				flag = true;
				if (MonoBehaviourSingleton<QuestManager>.I.IsCurrentQuestTypeSeries())
				{
					if (MonoBehaviourSingleton<QuestManager>.I.IsLastEnemyCurrentQuestSeries())
					{
						MonoBehaviourSingleton<InGameProgress>.I.BattleComplete(false);
					}
				}
				else
				{
					MonoBehaviourSingleton<InGameProgress>.I.BattleComplete(false);
				}
			}
		}
		if (!flag)
		{
			SetNextTrigger(0);
		}
		if (IsFieldEnemyBoss())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopStage.OnDefeatFieldEnemyBoss();
		}
	}

	public IEnumerator WaitForDeadMotionEnd()
	{
		yield return (object)null;
		if (!(base.animator == null))
		{
			while (true)
			{
				AnimatorStateInfo currentAnimatorStateInfo = base.animator.GetCurrentAnimatorStateInfo(0);
				if (currentAnimatorStateInfo.get_fullPathHash() == Animator.StringToHash("Base Layer.dead"))
				{
					break;
				}
				yield return (object)null;
				if (base.animator == null)
				{
					yield break;
				}
			}
			while (true)
			{
				AnimatorStateInfo currentAnimatorStateInfo2 = base.animator.GetCurrentAnimatorStateInfo(0);
				if (!(currentAnimatorStateInfo2.get_normalizedTime() < 1f))
				{
					break;
				}
				yield return (object)null;
				if (base.animator == null)
				{
					yield break;
				}
			}
			SetNextTrigger(0);
		}
	}

	private bool IsFieldEnemyBoss()
	{
		if (isCachedIsFieldEnemyBoss)
		{
			return cachedIsFieldEnemyBoss;
		}
		if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			return false;
		}
		if (MonoBehaviourSingleton<CoopManager>.IsValid() && !MonoBehaviourSingleton<CoopManager>.I.coopStage.GetIsInFieldEnemyBossBattle())
		{
			return false;
		}
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return false;
		}
		if (MonoBehaviourSingleton<StageObjectManager>.I.IsFieldEnemyBoss(id))
		{
			isCachedIsFieldEnemyBoss = true;
			cachedIsFieldEnemyBoss = true;
			return cachedIsFieldEnemyBoss;
		}
		isCachedIsFieldEnemyBoss = true;
		cachedIsFieldEnemyBoss = false;
		return cachedIsFieldEnemyBoss;
	}

	private void PrepareEnemyOut()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		DeleteStatusGizmo();
		if (MonoBehaviourSingleton<DropTargetMarkerManeger>.IsValid())
		{
			MonoBehaviourSingleton<DropTargetMarkerManeger>.I.RemoveTarget(base._transform);
		}
		UpdateNextMotion();
		SetVelocity(Vector3.get_zero(), VELOCITY_TYPE.NONE);
		base._rigidbody.set_velocity(Vector3.get_zero());
		base._collider.set_enabled(false);
		if (!isBoss && colliders != null)
		{
			int i = 0;
			for (int num = colliders.Length; i < num; i++)
			{
				colliders[i].set_enabled(false);
			}
		}
		ClearBleedDamageAll();
		ClearShadowSealingAll(true, true);
		for (int num2 = m_activeAttackObstacleList.Count - 1; num2 >= 0; num2--)
		{
			AttackShotNodeLink attackShotNodeLink = m_activeAttackObstacleList[num2];
			attackShotNodeLink.RequestDestroy();
		}
		if (m_effectElectricShock != null)
		{
			Object.Destroy(m_effectElectricShock);
		}
		if (m_effectBurning != null)
		{
			Object.Destroy(m_effectBurning);
		}
		if (m_effectSpeedDown != null)
		{
			Object.Destroy(m_effectSpeedDown);
		}
		if (effectLightRing != null)
		{
			Object.Destroy(effectLightRing);
		}
		m_effectElectricShock = null;
		m_effectBurning = null;
		m_effectSpeedDown = null;
		effectLightRing = null;
	}

	public override void VanishLocal()
	{
		PrepareVanishLocal();
		OnDeadEnd();
	}

	public override void PrepareVanishLocal()
	{
		ActReleaseGrabbedPlayers(false, false, true, 0f, 0f);
		base.badStatusTotal.Reset();
		badStatusMax.Copy(badStatusBase);
		base.PrepareVanishLocal();
		PrepareEnemyOut();
		bool flag = false;
		if (MonoBehaviourSingleton<StageObjectManager>.I.boss == this)
		{
			UpdateBreakIDLists();
		}
		if (!flag)
		{
			SetNextTrigger(0);
		}
	}

	public void OnEndEscape()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<CoopNetworkManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopNetworkManager>.I.EnemyOutEscape(id, _position);
		}
	}

	private void ForceEnemyOut()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		StopForceEnemyOut();
		forceEnemyOutCoroutine = this.StartCoroutine(DoForceEnemyOut());
	}

	private IEnumerator DoForceEnemyOut()
	{
		yield return (object)new WaitForSeconds(enemyParameter.guestEnemyOutTime);
		if (forceEnemyOutCoroutine != null)
		{
			forceEnemyOutCoroutine = null;
		}
		MonoBehaviourSingleton<CoopNetworkManager>.I.EnemyOut(id, _position);
	}

	public void StopForceEnemyOut()
	{
		if (forceEnemyOutCoroutine != null)
		{
			this.StopCoroutine(forceEnemyOutCoroutine);
			forceEnemyOutCoroutine = null;
		}
	}

	public void UpdateBreakIDLists()
	{
		if (MonoBehaviourSingleton<StageObjectManager>.I.boss == this && MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.coopStage.bossBreakIDLists != null)
		{
			int index = 0;
			if (QuestManager.IsValidInGame())
			{
				index = (int)MonoBehaviourSingleton<QuestManager>.I.currentQuestSeriesIndex;
			}
			MonoBehaviourSingleton<CoopManager>.I.coopStage.bossBreakIDLists[index] = GetBreakRegionIDList();
		}
	}

	public override void OnDeadEnd()
	{
		DestroyObject();
	}

	public virtual void ActStep(int motion_id = 0)
	{
		if (motion_id == 0)
		{
			motion_id = 115;
		}
		EndAction();
		base.actionID = ACTION_ID.MAX;
		PlayMotion(motion_id, -1f);
		if (enemySender != null)
		{
			enemySender.OnActStep(motion_id);
		}
	}

	public virtual void ActAngry(int angryActionId, uint angryId)
	{
		EndAction();
		NowAngryID = angryId;
		base.actionID = (ACTION_ID)14;
		PlayMotion(125 + angryActionId, -1f);
		if (enemySender != null)
		{
			enemySender.OnActAngry(angryActionId, angryId);
		}
	}

	public void RegisterAngryID(uint angryId)
	{
		if (m_execAngryIds != null && !m_execAngryIds.Contains(angryId))
		{
			m_execAngryIds.Add(angryId);
		}
	}

	public void UnRegisterAngryID(uint angryId)
	{
		if (m_execAngryIds != null && m_execAngryIds.Contains(angryId))
		{
			m_execAngryIds.Remove(angryId);
		}
	}

	public bool CheckAngryID(uint angryId)
	{
		return m_execAngryIds.Contains(angryId);
	}

	public virtual void ActDown()
	{
		bool flag = !IsDebuffShadowSealing();
		bool flag2 = IsAbleToUseDownTime();
		if (flag)
		{
			EndAction();
		}
		ActReleaseGrabbedPlayers(false, false, true, 0f, 0f);
		if (flag2)
		{
			downTime = Time.get_time() + downLoopStartTime + downLoopTime;
			downGaugeDecreaseStartTime = Time.get_time() + downLoopStartTime;
			downDecreaseRates = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo.Soul_DownGaugeDecreaseRates;
		}
		if (flag)
		{
			base.actionID = (ACTION_ID)13;
			PlayMotion((!flag2) ? 117 : 118, -1f);
		}
		else
		{
			AnimEventData.EventData eventData = new AnimEventData.EventData();
			eventData.attackMode = Player.ATTACK_MODE.NONE;
			eventData.intArgs = new int[1]
			{
				1
			};
			EventWeakPointAllON(eventData);
			if (!shadowSealingStackDebuff.Contains((ACTION_ID)13))
			{
				shadowSealingStackDebuff.Add((ACTION_ID)13);
			}
		}
		OnActReaction();
	}

	private bool UpdateDownAction()
	{
		if (!IsAbleToUseDownTime())
		{
			return false;
		}
		if (downGaugeDecreaseStartTime - Time.get_time() < 0f)
		{
			int stackCount = stackBuffCtrl.GetStackCount(StackBuffController.STACK_TYPE.SNATCH);
			if (!downDecreaseRates.IsNullOrEmpty())
			{
				int num = Mathf.Min(stackCount, downDecreaseRates.Length - 1);
				if (stackCount > 0)
				{
					float num2 = downDecreaseRates[num] * Time.get_deltaTime();
					downTime += num2;
				}
			}
		}
		if (downTime - Time.get_time() > 0f)
		{
			return false;
		}
		ActDownEnd();
		if (!IsDebuffShadowSealing())
		{
			SetNextTrigger(0);
		}
		return true;
	}

	private void ActDownEnd()
	{
		if (IsDebuffShadowSealing())
		{
			_EndDebuffAction((ACTION_ID)13);
			EventWeakPointAllOFF(null);
			if (shadowSealingStackDebuff.Contains((ACTION_ID)13))
			{
				shadowSealingStackDebuff.Remove((ACTION_ID)13);
			}
		}
	}

	public bool IsActDown()
	{
		if (IsAbleToUseDownTime())
		{
			return base.actionID == (ACTION_ID)13 || shadowSealingStackDebuff.Contains((ACTION_ID)13);
		}
		return false;
	}

	private bool IsAbleToUseDownTime()
	{
		bool result = false;
		if (useDownLoopTime && downLoopStartTime >= 0f && downLoopTime >= 0f)
		{
			result = true;
		}
		return result;
	}

	public float GetDownTimeRate()
	{
		float result = 0f;
		if (IsAbleToUseDownTime() && downLoopTime > 0f)
		{
			float num = downTime - Time.get_time();
			result = Mathf.Clamp(num / downLoopTime, 0f, 1f);
		}
		return result;
	}

	public void IncreaseDownTimeByAttack(float down)
	{
		if (!(down <= 0f) && downMax > 0)
		{
			float num = downLoopTime * (down / (float)downMax);
			float num2 = downTime + num;
			if (num2 - Time.get_time() > downLoopTime)
			{
				num2 = downLoopTime + Time.get_time();
			}
			downTime = num2;
		}
	}

	public virtual void ActDizzy()
	{
		EndAction();
		ActReleaseGrabbedPlayers(false, false, true, 0f, 0f);
		m_dizzyTime = Time.get_time() + DizzyReactionLoopTime;
		base.actionID = (ACTION_ID)17;
		PlayMotion(122, -1f);
		OnActReaction();
	}

	public void ActCounter(int targetId)
	{
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		EndAction();
		counterFlag = true;
		base.actionID = (ACTION_ID)16;
		PlayMotion(119, -1f);
		if (MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
		{
			MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestAnnounce(enemyTableData.name, STRING_CATEGORY.ENEMY_REACTION, kStrIdx_EnemyReaction_Counter);
		}
		if (IsOriginal() || IsCoopNone())
		{
			EnemyBrain enemyBrain = base.controller.brain as EnemyBrain;
			if (enemyBrain.targetCtrl != null)
			{
				StageObject stageObject = MonoBehaviourSingleton<StageObjectManager>.I.FindObject(targetId);
				if (stageObject != null)
				{
					enemyBrain.targetCtrl.SetCurrentTarget(stageObject);
					SetActionTarget(stageObject, true);
					SetActionPosition(stageObject._position, true);
					enemyBrain.fsm.ChangeState(STATE_TYPE.SELECT);
				}
			}
		}
		OnActReaction();
	}

	public override void ActFreezeStart()
	{
		if (!IsFreeze())
		{
			ActReleaseGrabbedPlayers(false, false, true, 0f, 0f);
			base.ActFreezeStart();
			if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
			{
				MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
			}
		}
	}

	protected override void ActFreezeEnd()
	{
		if (IsFreeze())
		{
			base.ActFreezeEnd();
			if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
			{
				MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
			}
		}
	}

	public override void ActParalyze()
	{
		if (!IsDebuffShadowSealing() || !shadowSealingStackDebuff.Contains(ACTION_ID.PARALYZE))
		{
			ActReleaseGrabbedPlayers(false, false, true, 0f, 0f);
			base.ActParalyze();
			paralyzeTime = Time.get_time() + paralyzeLoopTime;
			if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
			{
				MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
			}
		}
	}

	protected override void ActParalyzeEnd()
	{
		if (IsParalyze())
		{
			base.ActParalyzeEnd();
			if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
			{
				MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
			}
		}
	}

	public void ActElectricShock()
	{
		ActDamage();
		ActReleaseGrabbedPlayers(false, false, true, 0f, 0f);
		CreateElectricShockEffect();
	}

	public override void ActMovePoint(Vector3 targetPos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		if (!IsArrivalPosition(targetPos, 0f))
		{
			EndAction();
			base.actionID = ACTION_ID.MOVE_POINT;
			SetStateMovePoint(STATE_MOVE_POINT.INIT);
			if (enemySender != null)
			{
				enemySender.OnActMovePoint(targetPos);
			}
		}
	}

	protected override void UpdateMovePointAction()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		switch (base.stateMovePoint)
		{
		case STATE_MOVE_POINT.INIT:
			if (IsArrivalPosition(base.movePointPos, 0f))
			{
				SetStateMovePoint(STATE_MOVE_POINT.FINISH);
			}
			else
			{
				Vector3 val = base.movePointPos - _position;
				Vector3 normalized = val.get_normalized();
				if (!IsNeedToRotate(normalized))
				{
					PlayMotion(13, -1f);
					SetStateMovePoint(STATE_MOVE_POINT.CHECK);
				}
				else
				{
					m_rotateForActTime = 0f;
					m_rotateForActFinishTime = Mathf.Acos(Vector3.Dot(_forward, normalized)) / 0.0349065848f * (1f / (float)Application.get_targetFrameRate());
					m_rotateForActStart_Quat = Quaternion.LookRotation(_forward);
					m_rotateForActEnd_Quat = Quaternion.LookRotation(normalized);
					Vector3 val2 = Vector3.Cross(_forward, normalized);
					m_rotateForActMotionId = ((!(val2.y >= 0f)) ? 4 : 5);
					PlayMotion(m_rotateForActMotionId, -1f);
					SetStateMovePoint(STATE_MOVE_POINT.ROTATE);
				}
			}
			break;
		case STATE_MOVE_POINT.ROTATE:
		{
			m_rotateForActTime += Time.get_deltaTime();
			float num = Mathf.Clamp(m_rotateForActTime / m_rotateForActFinishTime, 0f, 1f);
			if (!IsPlayingMotion(1, true))
			{
				_rotation = Quaternion.Lerp(m_rotateForActStart_Quat, m_rotateForActEnd_Quat, num);
			}
			else if (num < 1f)
			{
				PlayMotion(m_rotateForActMotionId, -1f);
			}
			else
			{
				PlayMotion(13, -1f);
				SetStateMovePoint(STATE_MOVE_POINT.CHECK);
			}
			break;
		}
		case STATE_MOVE_POINT.CHECK:
			if (IsArrivalPosition(base.movePointPos, 0f))
			{
				SetNextTrigger(0);
				SetStateMovePoint(STATE_MOVE_POINT.FINISH);
			}
			break;
		case STATE_MOVE_POINT.FINISH:
			SetStateMovePoint(STATE_MOVE_POINT.NONE);
			break;
		}
	}

	public override void ActMoveLookAt(Vector3 moveLookAtPos, bool isPacket = false)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		EndAction();
		base.actionID = ACTION_ID.MOVE_LOOKAT;
		SetStateMoveLookAt(STATE_MOVE_LOOKAT.INIT);
		if (isPacket)
		{
			base.moveLookAtPos = moveLookAtPos;
		}
		if (enemySender != null)
		{
			enemySender.OnActMoveLookAt(moveLookAtPos);
		}
	}

	protected override void UpdateMoveLookAtAction()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		switch (base.stateMoveLookAt)
		{
		case STATE_MOVE_LOOKAT.INIT:
		{
			Vector3 val5 = base.moveLookAtPos - _position;
			m_moveLookAtInitTargetDir = val5.get_normalized();
			PlayMotion(14, -1f);
			SetStateMoveLookAt(STATE_MOVE_LOOKAT.MOVE);
			break;
		}
		case STATE_MOVE_LOOKAT.MOVE:
		{
			Vector3 val = _position - base.moveLookAtPos;
			float num = base.moveLookAtAngle * Time.get_deltaTime();
			val = Quaternion.AngleAxis(num, Vector3.get_up()) * val;
			Vector3 val2 = base.moveLookAtPos + val - _position;
			if (!IsPlayingMotion(1, true))
			{
				Vector3 val3 = base.moveLookAtPos - _position;
				_rotation = Quaternion.LookRotation(val3.get_normalized(), Vector3.get_up());
				_position += val2;
			}
			else
			{
				PlayMotion(14, -1f);
			}
			Vector3 val4 = base.moveLookAtPos - _position;
			Vector3 normalized = val4.get_normalized();
			float num2 = Vector3.Angle(m_moveLookAtInitTargetDir, normalized);
			if (num2 >= base.moveLookAtAngle)
			{
				SetNextTrigger(0);
				SetStateMoveLookAt(STATE_MOVE_LOOKAT.FINISH);
			}
			break;
		}
		case STATE_MOVE_LOOKAT.FINISH:
			SetStateMoveLookAt(STATE_MOVE_LOOKAT.NONE);
			break;
		}
	}

	protected override void OnPlayingEndMotion()
	{
		ACTION_ID actionID = base.actionID;
		if (actionID == ACTION_ID.DEAD)
		{
			OnDeadEnd();
		}
		else
		{
			base.OnPlayingEndMotion();
		}
	}

	protected override void EndAction()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		if (base.isInitialized)
		{
			ACTION_ID actionID = base.actionID;
			base.EndAction();
			_EndDebuffAction(actionID);
			EndWaitingPacket(WAITING_PACKET.ENEMY_WARP);
			if (loader.shadow != null && !loader.shadow.get_gameObject().get_activeSelf())
			{
				loader.shadow.get_gameObject().SetActive(true);
			}
			enableTargetPoint = true;
			reviveRegionWaitSync = false;
			enableDash = false;
			dashBeforePos = Vector3.get_zero();
			dashNowDistance = 0f;
			dashOverDistance = 0f;
			dashMinDistance = 0f;
			dashMaxDistance = 0f;
			dashEndTrigger = null;
			dashOverFlag = false;
			dashOverCheckDistance = 0f;
			canHitShockEffect = true;
			int num = regionWorks.Length;
			for (int i = 0; i < num; i++)
			{
				if (!regionWorks[i].IsValidDisplayTimer)
				{
					regionWorks[i].ResetWeakState();
				}
			}
			shotEventInfoQueue.Clear();
			shotNetworkInfoQueue.Clear();
			warpWaitSync = false;
			if (warpViewFlag || warpViewRate != 0f)
			{
				warpViewFlag = true;
				warpViewRatePerTime = -2f;
			}
			if (radialBlurEnable)
			{
				MonoBehaviourSingleton<InGameCameraManager>.I.EndRadialBlurFilter(0.1f);
			}
			radialBlurEnable = false;
			if (loader.baseEffect != null && !loader.baseEffect.get_gameObject().get_activeSelf())
			{
				loader.baseEffect.get_gameObject().SetActive(true);
			}
			int count = animLayerWeightChangeInfo.Count;
			if (count > 0)
			{
				Animator animator = loader.GetAnimator();
				for (int j = 0; j < count; j++)
				{
					AnimationLayerWeightChangeInfo animationLayerWeightChangeInfo = animLayerWeightChangeInfo[j];
					if (animationLayerWeightChangeInfo.aliveFlag && animationLayerWeightChangeInfo.forceEndFlag)
					{
						animationLayerWeightChangeInfo.aliveFlag = false;
					}
				}
			}
			if (blendColorCtrl != null)
			{
				blendColorCtrl.ForceEnd();
			}
		}
	}

	protected override void _EndDebuffAction(ACTION_ID beforeActId)
	{
		switch (beforeActId)
		{
		case (ACTION_ID)13:
			downTotal = 0f;
			downCount++;
			foreach (MissionCheckBase item in MonoBehaviourSingleton<InGameProgress>.I.missionCheck)
			{
				(item as MissionCheckDownCount)?.SetCount(downCount);
			}
			break;
		case ACTION_ID.PARALYZE:
			badStatusMax.paralyze *= 1.5f;
			if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
			{
				MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
			}
			break;
		case ACTION_ID.FREEZE:
			badStatusMax.freeze *= 1.5f;
			if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
			{
				MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
			}
			break;
		case (ACTION_ID)18:
			ActDebuffShadowSealingEnd();
			break;
		case (ACTION_ID)21:
			ActLightRingEnd();
			break;
		}
	}

	protected override string GetMotionStateName(int motion_id, string _layerName = "")
	{
		if (motion_id >= 125 && motion_id <= 141)
		{
			int num = 10;
			Character.stateNameBuilder.Length = 0;
			Character.stateNameBuilder.Append("Base Layer.");
			Character.stateNameBuilder.AppendFormat(subMotionStateName[num], motion_id - 125);
			return Character.stateNameBuilder.ToString();
		}
		if (motion_id - 115 >= 0 && motion_id - 115 < subMotionStateName.Length)
		{
			Character.stateNameBuilder.Length = 0;
			Character.stateNameBuilder.Append("Base Layer.");
			string text = subMotionStateName[motion_id - 115];
			if (motion_id == 119)
			{
				EnemyBrain enemyBrain = base.controller.brain as EnemyBrain;
				if (enemyBrain != null)
				{
					EnemyActionController actionCtrl = enemyBrain.actionCtrl;
					if (actionCtrl != null)
					{
						int nowModeCounterModeId = enemyBrain.actionCtrl.GetNowModeCounterModeId();
						if (nowModeCounterModeId >= 2)
						{
							text = text + "_" + $"{nowModeCounterModeId:D2}";
						}
					}
				}
			}
			Character.stateNameBuilder.Append(text);
			return Character.stateNameBuilder.ToString();
		}
		return base.GetMotionStateName(motion_id, _layerName);
	}

	protected override int _GetCachedHash(int motion_id)
	{
		if (motion_id < 115 || motion_id >= 142)
		{
			return base._GetCachedHash(motion_id);
		}
		return subMotionHashCaches[motion_id - 115];
	}

	protected override void _CacheHash(int motion_id, int hash)
	{
		if (motion_id < 115 || motion_id >= 142)
		{
			base._CacheHash(motion_id, hash);
		}
		else
		{
			subMotionHashCaches[motion_id - 115] = hash;
		}
	}

	protected override float GetAnimatorSpeed()
	{
		if (IsHitStop())
		{
			return 0f;
		}
		if (!base.isPause)
		{
			switch (base.actionID)
			{
			case ACTION_ID.ATTACK:
				return buffParam.GetAtkSpeed();
			case ACTION_ID.MOVE:
			case ACTION_ID.ROTATE:
			case ACTION_ID.MOVE_POINT:
			case ACTION_ID.MAX:
				return buffParam.GetMoveSpeed() * walkSpeedRateFromTable;
			default:
				return 1f;
			}
		}
		return 0f;
	}

	public override bool OnBuffStart(BuffParam.BuffData buffData)
	{
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		if (CheckDisableBuffTypeByShield(buffData.type))
		{
			return false;
		}
		if (CheckDisableBuffTypeByMadMode(buffData.type))
		{
			return false;
		}
		if (!buffParam.BuffStart(buffData))
		{
			return false;
		}
		UpdateAnimatorSpeed();
		if (buffData.sync)
		{
			SendBuffSync(buffData.type);
		}
		if (IsCoopNone() || IsOriginal())
		{
			buffData.isOwnerEnemyBuffStart = true;
		}
		switch (buffData.type)
		{
		case BuffParam.BUFFTYPE.GHOST_FORM:
			if (this.get_gameObject().get_activeSelf())
			{
				ChangeGhostShaderParam(GhostFormShaderParam.disappearParam, GhostFormShaderParam.duration);
			}
			else
			{
				isRequireGhostShaderParam = true;
				ghostBuffEndParam = GhostFormShaderParam.disappearParam;
				ghostBuffDuration = GhostFormShaderParam.duration;
			}
			break;
		case BuffParam.BUFFTYPE.BURNING:
			CreateBurningEffect();
			break;
		case BuffParam.BUFFTYPE.MOVE_SPEED_DOWN:
		case BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN:
			CreateSpeedDownEffect();
			break;
		}
		if (MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid() && buffData.isOwnerEnemyBuffStart)
		{
			MonoBehaviourSingleton<UIEnemyAnnounce>.I.StartBuff(enemyTableData.name, buffData.type);
		}
		if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
		}
		buffData.isOwnerEnemyBuffStart = false;
		return true;
	}

	public override void OnBuffRoutine(BuffParam.BuffData buffData, bool packet = false)
	{
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		int hp = base.hp;
		base.OnBuffRoutine(buffData, packet);
		int num = hp - base.hp;
		if (MonoBehaviourSingleton<InGameRecorder>.IsValid() && buffData.fromObjectID > 0)
		{
			MonoBehaviourSingleton<InGameRecorder>.I.RecordGivenDamage(buffData.fromObjectID, num);
		}
		if (QuestManager.IsValidInGameExplore() && isBoss && MonoBehaviourSingleton<CoopManager>.I.GetSelfID() == buffData.fromObjectID)
		{
			ExplorePlayerStatus myExplorePlayerStatus = MonoBehaviourSingleton<QuestManager>.I.GetMyExplorePlayerStatus();
			int num2 = myExplorePlayerStatus.givenTotalDamage + num;
			myExplorePlayerStatus.SyncTotalDamageToBoss(num2);
			MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendExploreBossDamage(num2);
		}
		BuffParam.BUFFTYPE type = buffData.type;
		if (type == BuffParam.BUFFTYPE.ELECTRIC_SHOCK)
		{
			if (!IsDebuffShadowSealing())
			{
				ReactionInfo reactionInfo = new ReactionInfo();
				reactionInfo.reactionType = REACTION_TYPE.ELECTRIC_SHOCK;
				if (base.enableReactionDelay && IsReactionDelayType((int)reactionInfo.reactionType))
				{
					DelayReactionInfo delayReactionInfo = new DelayReactionInfo();
					delayReactionInfo.type = reactionInfo.reactionType;
					RegisterReacionDelayInfo(delayReactionInfo);
					reactionInfo.reactionType = REACTION_TYPE.NONE;
					isReactionDelaySet = true;
				}
				ActReaction(reactionInfo, false);
			}
			if (!packet)
			{
				buffData.value = buffData.damage;
			}
		}
		if (BuffParam.IsTypeShowDamageOnEnemy(buffData.type))
		{
			AtkAttribute atkAttribute = new AtkAttribute();
			atkAttribute.normal = (float)buffData.value;
			Vector3 position = _position;
			GameObject loopEffect = buffParam.GetLoopEffect(buffData);
			if (loopEffect != null)
			{
				position = loopEffect.get_transform().get_position();
			}
			CreateDamageNum(position, atkAttribute, false, false, 0);
		}
		if (MonoBehaviourSingleton<CoopManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopManager>.I.coopStage.battleUserLog.Add(this, buffData.type, num);
		}
	}

	public override bool OnBuffEnd(BuffParam.BUFFTYPE type, bool sync, bool isPlayEndEffect = true)
	{
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		if (!base.OnBuffEnd(type, sync, isPlayEndEffect))
		{
			return false;
		}
		switch (type)
		{
		case BuffParam.BUFFTYPE.POISON:
			badStatusMax.poison *= 1.5f;
			break;
		case BuffParam.BUFFTYPE.BURNING:
			badStatusMax.burning *= 1.5f;
			if (m_effectBurning != null)
			{
				Object.Destroy(m_effectBurning);
				m_effectBurning = null;
			}
			break;
		case BuffParam.BUFFTYPE.DEADLY_POISON:
			badStatusMax.deadlyPoison *= 1.5f;
			break;
		case BuffParam.BUFFTYPE.GHOST_FORM:
			if (this.get_gameObject().get_activeSelf())
			{
				ChangeGhostShaderParam(GhostFormShaderParam.appearParam, GhostFormShaderParam.duration);
			}
			else
			{
				isRequireGhostShaderParam = true;
				ghostBuffEndParam = GhostFormShaderParam.appearParam;
				ghostBuffDuration = GhostFormShaderParam.duration;
			}
			break;
		case BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN:
			badStatusMax.attackSpeedDown *= 1.5f;
			if (m_effectSpeedDown != null && !buffParam.IsValidBuff(BuffParam.BUFFTYPE.MOVE_SPEED_DOWN))
			{
				Object.Destroy(m_effectSpeedDown);
				m_effectSpeedDown = null;
			}
			break;
		case BuffParam.BUFFTYPE.MOVE_SPEED_DOWN:
			badStatusMax.speedDown *= 1.5f;
			if (m_effectSpeedDown != null && !buffParam.IsValidBuff(BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN))
			{
				Object.Destroy(m_effectSpeedDown);
				m_effectSpeedDown = null;
			}
			break;
		}
		if (MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
		{
			MonoBehaviourSingleton<UIEnemyAnnounce>.I.EndBuff(enemyTableData.name, type);
		}
		if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
		{
			MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
		}
		return true;
	}

	protected override void OnUIBuffRoutine(BuffParam.BUFFTYPE type, int value)
	{
		if (MonoBehaviourSingleton<UIDamageManager>.IsValid() && (type == BuffParam.BUFFTYPE.REGENERATE || type == BuffParam.BUFFTYPE.REGENERATE_PROPORTION))
		{
			MonoBehaviourSingleton<UIDamageManager>.I.CreateEnemyRecoverHp(this, value, UIPlayerDamageNum.DAMAGE_COLOR.HEAL);
		}
	}

	public override void OnPoisonStart(int fromObjectID = 0)
	{
		BuffParam.BuffData buffData = new BuffParam.BuffData();
		buffData.type = BuffParam.BUFFTYPE.POISON;
		buffData.time = 20f;
		buffData.valueType = BuffParam.VALUE_TYPE.RATE;
		buffData.value = (int)((float)base.hpMax * 0.015f);
		buffData.interval = 5f;
		buffData.fromObjectID = fromObjectID;
		OnBuffStart(buffData);
	}

	public void OnElectricShockStart(AttackHitInfo atkHitInfo, Player player)
	{
		if (atkHitInfo != null)
		{
			ElectricShockInfo electricShockInfo = atkHitInfo.electricShockInfo;
			if (electricShockInfo != null)
			{
				BuffParam.BuffData buffData = new BuffParam.BuffData();
				buffData.type = BuffParam.BUFFTYPE.ELECTRIC_SHOCK;
				buffData.time = electricShockInfo.duration;
				buffData.interval = electricShockInfo.damageInterval;
				buffData.value = 1;
				buffData.fromObjectID = player.id;
				if (player != null)
				{
					BuffParam buffParam = player.buffParam;
					InGameUtility.PlayerAtkCalcData playerAtkCalcData = new InGameUtility.PlayerAtkCalcData();
					playerAtkCalcData.weaponAtk = player.attack;
					playerAtkCalcData.statusAtk = player.playerAtk;
					playerAtkCalcData.guardEquipAtk = player.GetGuardEquipmentAtk();
					playerAtkCalcData.buffAtkRate = buffParam.GetBuffAtkRate();
					playerAtkCalcData.passiveAtkRate = buffParam.GetPassiveAtkRate();
					playerAtkCalcData.buffAtkConstant = buffParam.GetBuffAtkConstant();
					playerAtkCalcData.buffAtkAllElementConstant = (float)buffParam.GetValue(BuffParam.BUFFTYPE.ATTACK_ALLELEMENT, true);
					playerAtkCalcData.passiveAtkConstant = buffParam.GetPassiveAtkUpConstant();
					playerAtkCalcData.passiveAtkAllElementConstant = buffParam.passive.atkAllElement;
					AtkAttribute atkAttribute = InGameUtility.CalcPlayerATK(playerAtkCalcData);
					buffData.damage = Mathf.FloorToInt(atkAttribute.CalcTotal() * ((float)electricShockInfo.atkRate * 0.01f));
				}
				OnBuffStart(buffData);
			}
		}
	}

	private void CreateBurningEffect()
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
		if (object.ReferenceEquals(m_effectBurning, null))
		{
			Transform effect = EffectManager.GetEffect("ef_btl_enm_fire_01", base._transform);
			if (!(effect == null))
			{
				ParticleSystem[] componentsInChildren = effect.GetComponentsInChildren<ParticleSystem>(true);
				if (componentsInChildren != null)
				{
					CalcFreezeEffectEmissionRadius();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						ShapeModule shape = componentsInChildren[i].get_shape();
						shape.set_radius(GetEmittionRadius());
					}
					Transform obj = effect;
					obj.set_localPosition(obj.get_localPosition() + Vector3.get_up() * GetEmittionRadius());
					m_effectBurning = effect.get_gameObject();
				}
			}
		}
	}

	private void CreateSpeedDownEffect()
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Expected O, but got Unknown
		if (object.ReferenceEquals(m_effectSpeedDown, null))
		{
			Transform effect = EffectManager.GetEffect("ef_btl_pl_movedown_01", base._transform);
			if (!(effect == null))
			{
				CalcFreezeEffectEmissionRadius();
				float num = GetEmittionRadius() * MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.attackSpeedDownParam.enemyEffectSize;
				Transform obj = effect;
				obj.set_localScale(obj.get_localScale() * num);
				m_effectSpeedDown = effect.get_gameObject();
			}
		}
	}

	public override AttackHitColliderProcessor.HitParam SelectHitCollider(AttackHitColliderProcessor processor, List<AttackHitColliderProcessor.HitParam> hit_params)
	{
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		checkHitParam.Clear();
		checkLength.Clear();
		checkPriority.Clear();
		targetRegionIds.Clear();
		if (processor.targetPointList != null)
		{
			int i = 0;
			for (int count = processor.targetPointList.Count; i < count; i++)
			{
				TargetPoint targetPoint = processor.targetPointList[i];
				if (targetPoint.IsEneble() && targetPoint.regionID >= 0 && targetPoint.owner == this)
				{
					targetRegionIds.Add(targetPoint.regionID);
				}
			}
		}
		BulletObject bulletObject = null;
		AnimEventCollider.AtkColliderHiter atkColliderHiter = null;
		bulletObject = (processor.colliderInterface as BulletObject);
		if (bulletObject == null)
		{
			atkColliderHiter = (processor.colliderInterface as AnimEventCollider.AtkColliderHiter);
		}
		float num = 0f;
		Vector3 val = Vector3.get_zero();
		Vector3 val2 = Vector3.get_zero();
		if (bulletObject != null)
		{
			Vector3 velocity = bulletObject._rigidbody.get_velocity();
			num = velocity.get_magnitude();
			Vector3 velocity2 = bulletObject._rigidbody.get_velocity();
			val = velocity2.get_normalized();
			val2 = bulletObject._transform.get_position();
		}
		float num2 = 3.40282347E+38f;
		int j = 0;
		for (int count2 = hit_params.Count; j < count2; j++)
		{
			int regionID = GetRegionID(hit_params[j].toCollider, targetRegionIds);
			hit_params[j].regionID = regionID;
			int item = 0;
			EnemyRegionWork enemyRegionWork = regionWorks[regionID];
			WEAK_STATE wEAK_STATE = enemyRegionWork.weakState;
			if (regionID > 0)
			{
				item = 1;
			}
			if (IsWeakStateCheckAlreadyHit(wEAK_STATE) && hit_params[j].fromObject != null && enemyRegionWork.weakAttackIDs.Contains(hit_params[j].fromObject.id))
			{
				wEAK_STATE = WEAK_STATE.NONE;
			}
			if (IsWeakStateSpAttack(wEAK_STATE))
			{
				bool flag = false;
				Player player = hit_params[j].fromObject as Player;
				if (player != null)
				{
					flag = player.isSpecialActionHit((Player.ATTACK_MODE)enemyRegionWork.weakSubParam, processor.attackInfo as AttackHitInfo, hit_params[j]);
				}
				hit_params[j].isSpAttackHit = flag;
				if (!flag)
				{
					wEAK_STATE = WEAK_STATE.NONE;
				}
			}
			if (bulletObject != null && bulletObject.isAimBossMode)
			{
				bool flag2 = false;
				if (processor.targetPointList != null)
				{
					int k = 0;
					for (int count3 = processor.targetPointList.Count; k < count3; k++)
					{
						TargetPoint targetPoint2 = processor.targetPointList[k];
						if (targetPoint2.IsEneble() && targetPoint2.regionID == regionID && targetPoint2.owner == this)
						{
							Vector3 markerPos = targetPoint2.param.markerPos;
							float num3 = 0f;
							float num4 = 0f;
							if (val == Vector3.get_zero())
							{
								Vector3 val3 = markerPos - hit_params[j].point;
								num3 = val3.get_magnitude();
								num4 = num3;
							}
							else
							{
								Vector3 val4 = Vector3.Cross(val, markerPos - val2);
								num3 = val4.get_magnitude();
								num4 = Vector3.Dot(markerPos - hit_params[j].point, val);
							}
							float num5 = enemyParameter.aimMarkerHitRadius * targetPoint2.param.aimMarkerScale;
							if (num3 <= num5 && (targetPoint2.isSkipDotCalc || Mathf.Abs(num4) <= enemyParameter.hitCompareAimDepthLimit))
							{
								flag2 = true;
								hit_params[j].isHitAim = true;
								break;
							}
						}
					}
				}
				if (IsWeakStateSpAttack(wEAK_STATE))
				{
					item = 4;
				}
				else if (flag2)
				{
					item = 3;
				}
				else if (regionID > 0 && (int)enemyRegionWork.hp > 0)
				{
					item = 2;
				}
			}
			else if (wEAK_STATE != 0)
			{
				item = 3;
				if (regionID > 0 && (int)enemyRegionWork.hp > 0)
				{
					item = 4;
				}
			}
			else if (regionID > 0 && (int)enemyRegionWork.hp > 0)
			{
				item = 2;
			}
			if (!regionInfos[hit_params[j].regionID].isAtkColliderHit)
			{
				item = 0;
			}
			float num6 = 0f;
			if (bulletObject != null)
			{
				if (bulletObject._rigidbody != null && val != Vector3.get_zero())
				{
					num6 = Vector3.Dot(hit_params[j].point, val);
				}
				else
				{
					Vector3 val5 = hit_params[j].point - bulletObject._transform.get_position();
					num6 = val5.get_magnitude();
				}
			}
			else if (atkColliderHiter != null)
			{
				Vector3 val6 = hit_params[j].point - hit_params[j].crossCheckPoint;
				num6 = val6.get_magnitude();
			}
			checkHitParam.Add(hit_params[j]);
			checkLength.Add(num6);
			checkPriority.Add(item);
			if (num6 < num2)
			{
				num2 = num6;
			}
		}
		AttackHitColliderProcessor.HitParam result = null;
		float num7 = 3.40282347E+38f;
		int num8 = 0;
		int l = 0;
		for (int count4 = checkHitParam.Count; l < count4; l++)
		{
			if ((!(bulletObject != null) || !(num >= enemyParameter.hitCompareSpeed) || !(checkLength[l] - num2 > enemyParameter.hitCompareLengthLimit)) && (checkPriority[l] > num8 || (checkPriority[l] == num8 && checkLength[l] < num7)))
			{
				result = checkHitParam[l];
				num7 = checkLength[l];
				num8 = checkPriority[l];
			}
		}
		return result;
	}

	protected override bool IsValidAttackedHit(StageObject from_object)
	{
		if (from_object is Enemy)
		{
			return false;
		}
		return base.IsValidAttackedHit(from_object);
	}

	protected override void OnAttackedHitDirection(AttackedHitStatusDirection status)
	{
		Player player = status.fromObject as Player;
		status.regionID = status.hitParam.regionID;
		EnemyRegionWork enemyRegionWork = null;
		if (status.regionID >= 0 && status.regionID < regionInfos.Length)
		{
			enemyRegionWork = regionWorks[status.regionID];
		}
		if (enemyRegionWork == null)
		{
			status.weakState = WEAK_STATE.NONE;
		}
		else
		{
			status.weakState = enemyRegionWork.weakState;
			if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.CANNON_BALL)
			{
				status.weakState = WEAK_STATE.NONE;
			}
			if (IsWeakStateSpAttack(status.weakState))
			{
				bool flag = false;
				if (player != null)
				{
					flag = player.isSpecialActionHit((Player.ATTACK_MODE)enemyRegionWork.weakSubParam, status.attackInfo, status.hitParam);
				}
				if (!flag)
				{
					status.weakState = WEAK_STATE.NONE;
				}
			}
			if (IsWeakStateElementAttack(status.weakState) || IsWeakStateSkillAttack(status.weakState))
			{
				AtkAttribute atk = new AtkAttribute();
				if (status.hitParam.processor != null)
				{
					BulletObject bulletObject = status.hitParam.processor.colliderInterface as BulletObject;
					if (bulletObject != null)
					{
						atk = bulletObject.masterAtk;
					}
					else
					{
						status.fromObject.GetAtk(status.attackInfo, ref atk);
					}
				}
				if (IsWeakStateElementAttack(status.weakState) && enemyRegionWork.validElementType != (int)atk.GetElementType())
				{
					status.weakState = WEAK_STATE.NONE;
				}
				if (IsWeakStateSkillAttack(status.weakState) && !status.attackInfo.isSkillReference)
				{
					status.weakState = WEAK_STATE.NONE;
				}
			}
			if (IsWeakStateHealAttack(status.weakState) && status.attackInfo.attackType != AttackHitInfo.ATTACK_TYPE.HEAL_ATTACK)
			{
				status.weakState = WEAK_STATE.NONE;
			}
			if (IsWeakStateCheckAlreadyHit(status.weakState))
			{
				if (enemyRegionWork.weakAttackIDs.Contains(status.fromObjectID))
				{
					status.weakState = WEAK_STATE.NONE;
				}
				else if (!IsCoopNone() && !IsOriginal())
				{
					enemyRegionWork.weakAttackIDs.Add(status.fromObjectID);
				}
			}
			if (IsWeakStateDisplaySign(status.weakState))
			{
				Self self = status.fromObject as Self;
				if (self != null)
				{
					self.taskChecker.OnWeakAttack(status.weakState, 0);
				}
			}
			if (IsCannonBallHitShieldRegion(enemyRegionWork, status.attackInfo) && MonoBehaviourSingleton<UIEnemyStatus>.IsValid() && enemyRegionWork.isShieldCriticalDamage)
			{
				MonoBehaviourSingleton<UIEnemyStatus>.I.PlayShakeHpGauge(5f, 0.5f, 0.05f, true);
			}
			if (enemyRegionWork.regionInfo != null && enemyRegionWork.regionInfo.dragonArmorInfo.enabled)
			{
				status.isDamageRegionOnly = true;
			}
		}
		if (!object.ReferenceEquals(player, null) && status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.JUMP)
		{
			player.HitJumpAttack();
		}
		base.OnAttackedHitDirection(status);
	}

	protected unsafe override void OnPlayAttackedHitEffect(AttackedHitStatusDirection status)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0711: Unknown result type (might be due to invalid IL or missing references)
		//IL_0716: Expected O, but got Unknown
		//IL_072e: Unknown result type (might be due to invalid IL or missing references)
		//IL_073f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0746: Unknown result type (might be due to invalid IL or missing references)
		//IL_0792: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_081e: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e0: Unknown result type (might be due to invalid IL or missing references)
		bool flag = true;
		bool is_self = status.fromObject is Self;
		if (is_self)
		{
			if (selfHitEffectCool > 0f)
			{
				return;
			}
			SetHitShock(status.hitPos - status.fromObject._position);
			if (MonoBehaviourSingleton<InGameManager>.I.graphicOptionType <= 1)
			{
				selfHitEffectCool = 0.1f;
			}
		}
		else
		{
			if (otherHitEffectCool > 0f)
			{
				return;
			}
			string enemyOtherSimpleHitEffectName = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.enemyOtherSimpleHitEffectName;
			if (!string.IsNullOrEmpty(enemyOtherSimpleHitEffectName))
			{
				EffectManager.OneShot(enemyOtherSimpleHitEffectName, status.hitPos, status.hitParam.rot, is_self);
			}
			flag = false;
			if (MonoBehaviourSingleton<InGameManager>.I.graphicOptionType <= 1)
			{
				otherHitEffectCool = 0.5f;
			}
		}
		if (regionInfos != null)
		{
			int regionID = status.regionID;
			if (regionID >= 0 && regionID < regionInfos.Length)
			{
				RegionInfo regionInfo = regionInfos[regionID];
				if (status.weakState != 0 && is_self)
				{
					SetHitLight();
				}
				if (status.badStatusAdd.paralyze > 0f)
				{
					string enemyParalyzeHitEffectName = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.enemyParalyzeHitEffectName;
					if (!string.IsNullOrEmpty(enemyParalyzeHitEffectName) && flag)
					{
						EffectManager.OneShot(enemyParalyzeHitEffectName, status.hitPos, status.hitParam.rot, is_self);
					}
				}
				if (status.badStatusAdd.poison > 0f)
				{
					string enemyPoisonHitEffectName = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.enemyPoisonHitEffectName;
					if (!string.IsNullOrEmpty(enemyPoisonHitEffectName) && flag)
					{
						EffectManager.OneShot(enemyPoisonHitEffectName, status.hitPos, status.hitParam.rot, is_self);
					}
				}
				if (status.badStatusAdd.freeze > 0f)
				{
					string enemyFreezeHitEffectName = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.enemyFreezeHitEffectName;
					if (!string.IsNullOrEmpty(enemyFreezeHitEffectName) && flag)
					{
						EffectManager.OneShot(enemyFreezeHitEffectName, status.hitPos, status.hitParam.rot, is_self);
					}
				}
				if (status.skillParam != null && status.skillParam.tableData != null)
				{
					bool flag2 = false;
					if (status.skillParam.tableData.hitSEID != 0)
					{
						if (EnablePlaySound())
						{
							SoundManager.PlayOneShotSE(status.skillParam.tableData.hitSEID, status.hitPos);
						}
						flag2 = true;
					}
					if (!string.IsNullOrEmpty(status.skillParam.tableData.hitEffectName))
					{
						if (flag)
						{
							EffectManager.OneShot(status.skillParam.tableData.hitEffectName, status.hitPos, status.hitParam.rot, is_self);
						}
						flag2 = true;
					}
					if (flag2)
					{
						return;
					}
				}
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				if (status.attackInfo.hitSEID != 0)
				{
					if (EnablePlaySound())
					{
						SoundManager.PlayOneShotSE(status.attackInfo.hitSEID, status.hitPos);
					}
					flag4 = true;
					if (!status.attackInfo.playCommonHitEffect)
					{
						flag3 = true;
					}
				}
				if (!string.IsNullOrEmpty(status.attackInfo.hitEffectName))
				{
					if (flag)
					{
						EffectManager.OneShot(status.attackInfo.hitEffectName, status.hitPos, status.hitParam.rot, is_self);
					}
					flag5 = true;
					if (!status.attackInfo.playCommonHitSe)
					{
						flag3 = true;
					}
				}
				if (!flag3 && status.skillParam == null)
				{
					ELEMENT_TYPE elementType = status.atk.GetElementType();
					EFFECTIVE_TYPE effectiveType = GetEffectiveType(elementType, GetElementType());
					string text = status.attackInfo.toEnemy.hitTypeName;
					EnemyHitTypeTable.TypeData typeData = null;
					Vector3 scale = Vector3.get_one();
					float delay = 0f;
					if (is_self)
					{
						Player player = status.fromObject as Player;
						if (!object.ReferenceEquals(player, null))
						{
							typeData = player.GetOverrideHitEffect(status, ref scale, ref delay);
						}
					}
					if (typeData == null && !string.IsNullOrEmpty(text))
					{
						char c = text[text.Length - 1];
						if (elementType == ELEMENT_TYPE.MAX)
						{
							typeData = Singleton<EnemyHitTypeTable>.I.GetData(text, FieldManager.IsValidInGameNoQuest());
						}
						else if (effectiveType == EFFECTIVE_TYPE.GOOD && c == 'S')
						{
							text = text.Substring(0, text.Length - 1) + "L";
							typeData = Singleton<EnemyHitTypeTable>.I.GetData(text, FieldManager.IsValidInGameNoQuest());
						}
						else if (effectiveType != 0 && c == 'L')
						{
							text = text.Substring(0, text.Length - 1) + "S";
							typeData = Singleton<EnemyHitTypeTable>.I.GetData(text, FieldManager.IsValidInGameNoQuest());
						}
						else
						{
							typeData = Singleton<EnemyHitTypeTable>.I.GetData(text, FieldManager.IsValidInGameNoQuest());
						}
					}
					string text2 = regionInfo.hitMaterialName;
					if (string.IsNullOrEmpty(text2))
					{
						text2 = baseHitMaterialName;
					}
					EnemyHitMaterialTable.MaterialData materialData = null;
					if (!string.IsNullOrEmpty(text2))
					{
						materialData = Singleton<EnemyHitMaterialTable>.I.GetData(text2);
					}
					if (typeData != null && !flag5)
					{
						string element_effect_name = null;
						if (elementType == ELEMENT_TYPE.MAX)
						{
							element_effect_name = typeData.baseEffectName;
						}
						else
						{
							element_effect_name = typeData.elementEffectNames[(int)elementType];
						}
						if (status.damageDistanceData != null && status.attackInfo.name.Contains("PLC05_attack_00") && status.damageDistanceData.IsMaxRate(status.distanceXZ))
						{
							element_effect_name = MonoBehaviourSingleton<InGameSettingsManager>.I.player.bestDistanceEffect;
						}
						if (!string.IsNullOrEmpty(element_effect_name) && flag)
						{
							if (delay > 0f)
							{
								_003COnPlayAttackedHitEffect_003Ec__AnonStorey4F9 _003COnPlayAttackedHitEffect_003Ec__AnonStorey4F;
								AppMain.Delay(delay, new Action((object)_003COnPlayAttackedHitEffect_003Ec__AnonStorey4F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
							}
							else
							{
								EffectManager.OneShot(element_effect_name, status.hitPos, status.hitParam.rot, scale, is_self, null);
							}
						}
					}
					if (elementType != ELEMENT_TYPE.MAX && !flag4)
					{
						int num = enemyParameter.elementHitSEIDs[(int)elementType];
						if (num != 0 && EnablePlaySound())
						{
							SoundManager.PlayOneShotSE(num, status.hitPos);
						}
					}
					if (materialData != null)
					{
						if (!string.IsNullOrEmpty(materialData.addEffectName) && flag)
						{
							EffectManager.OneShot(materialData.addEffectName, status.hitPos, status.hitParam.rot, is_self);
						}
						if (typeData != null && !flag4)
						{
							int typeSEID = materialData.GetTypeSEID(text);
							if (typeSEID != 0 && EnablePlaySound())
							{
								SoundManager.PlayOneShotSE(typeSEID, status.hitPos);
							}
						}
					}
					EnemyRegionWork enemyRegionWork = regionWorks[regionID];
					if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.CANNON_BALL)
					{
						string effect_name = "ef_btl_magibullet_landing_03";
						if (IsCannonBallHitShieldRegion(enemyRegionWork, status.attackInfo))
						{
							effect_name = "ef_btl_magibullet_landing_01";
							if (enemyRegionWork.isShieldCriticalDamage)
							{
								effect_name = "ef_btl_magibullet_landing_02";
							}
						}
						else if (enemyRegionWork.isShieldDamage && enemyRegionWork.weakState == WEAK_STATE.WEAK_GRAB)
						{
							effect_name = "ef_btl_magibullet_landing_01";
						}
						Transform effect = EffectManager.GetEffect(effect_name, null);
						effect.set_position(status.exHitPos);
						effect.set_rotation(status.hitParam.rot);
						effect.set_localScale(Vector3.get_one());
					}
				}
			}
		}
	}

	private void OnHitWeakPoint(string deleteAtkName, bool isSpWeak)
	{
		ActReleaseGrabbedPlayers(true, isSpWeak, false, 0f, 0f);
		if (!string.IsNullOrEmpty(deleteAtkName))
		{
			for (int num = m_activeAttackLaserList.Count - 1; num >= 0; num--)
			{
				AttackNWayLaser attackNWayLaser = m_activeAttackLaserList[num];
				if (attackNWayLaser.AttackInfoName == deleteAtkName)
				{
					attackNWayLaser.RequestDestroy();
				}
			}
			for (int num2 = m_activeAttackFunnelList.Count - 1; num2 >= 0; num2--)
			{
				AttackFunnelBit attackFunnelBit = m_activeAttackFunnelList[num2];
				if (attackFunnelBit.AttackInfoName == deleteAtkName)
				{
					attackFunnelBit.RequestDestroy(true);
				}
			}
			for (int num3 = m_activeAttackDigList.Count - 1; num3 >= 0; num3--)
			{
				AttackDig attackDig = m_activeAttackDigList[num3];
				if (attackDig.AttackInfoName == deleteAtkName)
				{
					attackDig.RequestDestroy(true);
				}
			}
			for (int num4 = m_activeAttackActionMineList.Count - 1; num4 >= 0; num4--)
			{
				AttackActionMine attackActionMine = m_activeAttackActionMineList[num4];
				if (attackActionMine.AttackInfoName == deleteAtkName)
				{
					attackActionMine.RequestDestroy(false);
				}
			}
			for (int num5 = m_activeAttackObstacleList.Count - 1; num5 >= 0; num5--)
			{
				AttackShotNodeLink attackShotNodeLink = m_activeAttackObstacleList[num5];
				if (attackShotNodeLink.AttackInfoName == deleteAtkName)
				{
					attackShotNodeLink.RequestDestroy();
				}
			}
		}
	}

	public void OnDestroyFunnel(AttackFunnelBit delFunnel)
	{
		if (m_activeAttackFunnelList.Contains(delFunnel))
		{
			m_activeAttackFunnelList.Remove(delFunnel);
		}
	}

	public void OnDestroyLaser(AttackNWayLaser delLaser)
	{
		if (m_activeAttackLaserList.Contains(delLaser))
		{
			m_activeAttackLaserList.Remove(delLaser);
		}
	}

	public void OnDestroyDig(AttackDig delDig)
	{
		if (m_activeAttackDigList.Contains(delDig))
		{
			m_activeAttackDigList.Remove(delDig);
		}
	}

	public void ActDestroyActionMine(int objId, bool isExplode)
	{
		AttackActionMine attackActionMine = m_activeAttackActionMineList.Find((AttackActionMine x) => x.objId == objId);
		if (attackActionMine != null)
		{
			attackActionMine.RequestDestroy(isExplode);
		}
	}

	public void OnDestroyActionMine(AttackActionMine mine)
	{
		if (m_activeAttackActionMineList.Contains(mine))
		{
			m_activeAttackActionMineList.Remove(mine);
		}
	}

	public void OnDestroyObstacle(AttackShotNodeLink delObstacle)
	{
		if (m_activeAttackObstacleList.Contains(delObstacle))
		{
			m_activeAttackObstacleList.Remove(delObstacle);
		}
	}

	protected override bool IsDamageValid(AttackedHitStatusDirection status)
	{
		if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.GIMMICK_GENERATED)
		{
			return true;
		}
		return status.fromType == OBJECT_TYPE.PLAYER;
	}

	public override void AbsorptionProc(Character targetChar, AttackedHitStatusLocal status)
	{
		AttackHitInfo attackInfo = status.attackInfo;
		if (attackInfo != null && !(attackInfo.absorptance <= 0f))
		{
			float num = attackInfo.absorptance * 0.01f;
			int num2 = (int)((float)base.hpMax * num);
			if (num2 > 0)
			{
				Player player = targetChar as Player;
				if (player != null)
				{
					player.StartEffectDrain(this);
				}
				RecoverHp(num2, true);
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
		AtkAttribute atkAttribute = new AtkAttribute();
		for (int i = 0; i < absorbBuffDataList.Count; i++)
		{
			switch (absorbBuffDataList[i].type)
			{
			case BuffParam.BUFFTYPE.ABSORB_NORMAL:
				atkAttribute.normal += status.damageDetails.normal;
				break;
			case BuffParam.BUFFTYPE.ABSORB_FIRE:
				atkAttribute.fire += status.damageDetails.fire;
				break;
			case BuffParam.BUFFTYPE.ABSORB_WATER:
				atkAttribute.water += status.damageDetails.water;
				break;
			case BuffParam.BUFFTYPE.ABSORB_THUNDER:
				atkAttribute.thunder += status.damageDetails.thunder;
				break;
			case BuffParam.BUFFTYPE.ABSORB_SOIL:
				atkAttribute.soil += status.damageDetails.soil;
				break;
			case BuffParam.BUFFTYPE.ABSORB_LIGHT:
				atkAttribute.light += status.damageDetails.light;
				break;
			case BuffParam.BUFFTYPE.ABSORB_DARK:
				atkAttribute.dark += status.damageDetails.dark;
				break;
			}
		}
		atkAttribute.CheckMinus();
		status.damageDetails.Sub(atkAttribute);
		status.damage = Mathf.FloorToInt(status.damageDetails.CalcTotal());
		int num = Mathf.FloorToInt(atkAttribute.CalcTotal());
		int num2 = Mathf.FloorToInt((float)base.hpMax * MonoBehaviourSingleton<InGameSettingsManager>.I.buff.absorbDamageParam.limitRateEnemyAbsorbDamage);
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid() && num > num2)
		{
			num = num2;
		}
		if (num <= 0)
		{
			return false;
		}
		RecoverHp(num, true);
		return true;
	}

	protected override void OnAttackedHitLocal(AttackedHitStatusLocal status)
	{
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		base.OnAttackedHitLocal(status);
		_CheckHitLocalArrow(status);
		if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.BOMBROCK)
		{
			status.damage = Mathf.FloorToInt((float)base.hpMax * status.attackInfo.atk.normal * 0.01f);
			status.damageDetails = new AtkAttribute();
			status.damageDetails.normal = (float)status.damage;
		}
		if (status.attackInfo.isSkillReference && (isAvailableCounter(base.actionID) || base.actionID == (ACTION_ID)16))
		{
			EnemyRegionWork enabledCounterRegion = GetEnabledCounterRegion();
			if (enabledCounterRegion != null)
			{
				status.damage = 0;
				status.damageDetails = new AtkAttribute();
			}
		}
		if (enemyParameter.showDamageNum && (status.fromObject is Self || status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.GIMMICK_GENERATED) && status.attackInfo.attackType != AttackHitInfo.ATTACK_TYPE.CANNON_BALL)
		{
			CreateDamageNum(status.hitPos, status.damageDetails, status.weakState != WEAK_STATE.NONE, status.isDamageRegionOnly, status.attackInfo.damageNumAddGroup);
		}
		Player player = status.fromObject as Player;
		if (player != null && !status.attackInfo.isSkillReference)
		{
			player.IncreaseSpActonGauge(status.attackInfo.attackType, status.hitPos, 0f, status.attackInfo.atkRate, status.attackInfo.toEnemy.isSpecialAttack, status.attackInfo.dontIncreaseGauge);
			if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.NORMAL || status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.FROM_AVOID)
			{
				player.UpdateBoostHitCount();
			}
			if (player is Self && MonoBehaviourSingleton<InGameManager>.IsValid())
			{
				MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.AddDamageByWeapon(player.weaponIndex, status.damage);
			}
		}
		status.downAddBase = 0f;
		status.downAddWeak = 0f;
		if (status.damage > 0)
		{
			status.downAddBase = status.attackInfo.down;
			status.isForceDown = status.attackInfo.isForceDown;
			if (player != null)
			{
				int value = 0;
				if (!player.GetOneHandSwordBoostDownValue(ref value, status.attackInfo.name))
				{
					switch (status.weakState)
					{
					case WEAK_STATE.WEAK:
						status.downAddWeak = player.downPowerSimpleWeak * status.attackInfo.atkRate;
						break;
					case WEAK_STATE.WEAK_SP_ATTACK:
					case WEAK_STATE.WEAK_ELEMENT_ATTACK:
					case WEAK_STATE.WEAK_ELEMENT_SKILL_ATTACK:
					case WEAK_STATE.WEAK_SKILL_ATTACK:
					case WEAK_STATE.WEAK_HEAL_ATTACK:
					case WEAK_STATE.WEAK_ELEMENT_SP_ATTACK:
						status.downAddWeak = player.downPowerWeak * status.attackInfo.atkRate;
						break;
					case WEAK_STATE.WEAK_SP_DOWN_MAX:
						status.downAddWeak = (float)downMax;
						break;
					}
				}
				else
				{
					status.downAddBase += (float)value;
				}
			}
			if (status.downAddBase > 0f)
			{
				status.downAddBase *= player.buffParam.GetBadStatusRateUp(BuffParam.BAD_STATUS_UP.DOWN);
				status.downAddBase += player.buffParam.GetBadStatusUp(BuffParam.BAD_STATUS_UP.DOWN);
			}
			else if (status.downAddWeak > 0f)
			{
				status.downAddWeak *= player.buffParam.GetBadStatusRateUp(BuffParam.BAD_STATUS_UP.DOWN);
				status.downAddWeak += player.buffParam.GetBadStatusUp(BuffParam.BAD_STATUS_UP.DOWN);
			}
		}
		if (MonoBehaviourSingleton<CoopNetworkManager>.IsValid())
		{
			MonoBehaviourSingleton<CoopNetworkManager>.I.EnemyAttack(id, status.damage);
		}
		RecordDeliveryBattleCheckerOnAttacked(status);
	}

	private void RecordDeliveryBattleCheckerOnAttacked(AttackedHitStatusLocal status)
	{
		Self self = status.fromObject as Self;
		if (!(self == null) && MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.SetMaxDamageSelf(status.damage);
			BattleCheckerBase.JudgementParam judgementParam = BattleCheckerBase.JudgementParam.Create(status.attackInfo, self);
			MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnAttackHit(status.attackInfo.name, judgementParam, status.damage);
			if (IsWeakStateDisplaySign(status.weakState))
			{
				MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnWeakAttack(status.weakState, status.origin.damage);
			}
			if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.JUMP)
			{
				MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnJump(status.origin.damage);
			}
		}
	}

	private void _CheckHitLocalArrow(AttackedHitStatusLocal status)
	{
		if (!object.ReferenceEquals(status.hitParam.processor, null))
		{
			BulletObject bulletObject = status.hitParam.processor.colliderInterface as BulletObject;
			if (!object.ReferenceEquals(bulletObject, null) && !(status.attackInfo.rateInfoRate < 1f))
			{
				Character character = status.fromObject as Character;
				status.isArrowBleed = false;
				status.isShadowSealing = false;
				if (status.hitParam.isHitAim)
				{
					switch (status.attackInfo.spAttackType)
					{
					case SP_ATTACK_TYPE.NONE:
					{
						status.isArrowBleed = true;
						float num = (!(character != null)) ? 1f : character.buffParam.GetBleedUp();
						status.arrowBleedDamage = Mathf.CeilToInt((float)status.damage * MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.arrowBleedDamageRate * num);
						if (status.arrowBleedDamage < 1)
						{
							status.arrowBleedDamage = 1;
						}
						float normal = status.damageDetails.normal;
						ELEMENT_TYPE elementType = status.damageDetails.GetElementType();
						status.damage = (int)((float)status.damage * num);
						status.damageDetails.Mul(num);
						if (normal > 0f && status.damageDetails.normal < 1f)
						{
							status.damageDetails.normal = 1f;
							status.damage++;
						}
						if (elementType != ELEMENT_TYPE.MAX && status.damageDetails.GetElementType() == ELEMENT_TYPE.MAX)
						{
							status.damageDetails.SetTargetElement(elementType, 1f);
							status.damage++;
						}
						if (status.damage < 1)
						{
							status.damageDetails.normal = 1f;
							status.damage = 1;
						}
						BleedData bleedData = null;
						EnemyRegionWork enemyRegionWork2 = regionWorks[status.regionID];
						int i = 0;
						for (int count = enemyRegionWork2.bleedList.Count; i < count; i++)
						{
							if (enemyRegionWork2.bleedList[i].ownerID == status.fromObjectID)
							{
								bleedData = enemyRegionWork2.bleedList[i];
								break;
							}
						}
						if (bleedData != null && BleedData.MaxLv == bleedData.lv + 1)
						{
							InGameSettingsManager.Player.SpecialActionInfo specialActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo;
							int num2 = status.arrowBleedDamage / specialActionInfo.arrowBleedCount;
							if (num2 < 1)
							{
								num2 = 1;
							}
							float num3 = (float)(bleedData.damage + num2) * specialActionInfo.arrowBurstDamageRate;
							status.arrowBurstDamage = (int)num3;
							status.damage += status.arrowBurstDamage;
						}
						break;
					}
					case SP_ATTACK_TYPE.HEAT:
						status.isShadowSealing = true;
						if (bulletObject.isBossPierceArrow && !isPierceAfterTarget)
						{
							EnemyRegionWork enemyRegionWork = regionWorks[status.regionID];
							if (enemyRegionWork.shadowSealingData.ownerID == 0)
							{
								bulletObject.EndBossPierceArrow();
							}
						}
						break;
					}
				}
			}
		}
	}

	protected override void OnIgnoreHitAttack()
	{
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Expected O, but got Unknown
		base.OnIgnoreHitAttack();
		if (IsValidBuff(BuffParam.BUFFTYPE.GHOST_FORM) && m_effectHitWhenGhost == null && !(base.effectPlayProcessor == null))
		{
			List<EffectPlayProcessor.EffectSetting> settings = base.effectPlayProcessor.GetSettings("GHOST_EFFECT");
			if (settings != null && settings[0] != null)
			{
				Transform val = base.effectPlayProcessor.PlayEffect(settings[0], base._transform);
				if (val != null)
				{
					m_effectHitWhenGhost = val.get_gameObject();
				}
			}
		}
	}

	protected override bool CheckStatusForHitEffect(AttackedHitStatusDirection status)
	{
		if (IsValidBuff(BuffParam.BUFFTYPE.GHOST_FORM))
		{
			Player player = status.fromObject as Player;
			if (player != null && player.CheckIgnoreBuff(BuffParam.BUFFTYPE.GHOST_FORM))
			{
				return true;
			}
			AtkAttribute damage_details = new AtkAttribute();
			AttackedHitStatusLocal status2 = new AttackedHitStatusLocal(nowAttackedHitStatus);
			if (CalcDamage(status2, ref damage_details) > 0)
			{
				return true;
			}
			return false;
		}
		return base.CheckStatusForHitEffect(status);
	}

	public override void GetAtk(AttackHitInfo info, ref AtkAttribute atk)
	{
		InGameUtility.EnemyAtkCalcData enemyAtkCalcData = new InGameUtility.EnemyAtkCalcData();
		enemyAtkCalcData.atkInfo = info;
		enemyAtkCalcData.buffAtkRate = buffParam.GetBuffAtkRate();
		enemyAtkCalcData.buffAtkConstant = buffParam.GetBuffAtkConstant();
		enemyAtkCalcData.buffAtkAllElementConstant = (float)buffParam.GetValue(BuffParam.BUFFTYPE.ATTACK_ALLELEMENT, true);
		atk.Copy(InGameUtility.CalcEnemyATK(enemyAtkCalcData));
	}

	protected override int CalcDamage(AttackedHitStatusLocal status, ref AtkAttribute damage_details)
	{
		if (regionInfos == null)
		{
			return 0;
		}
		if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.CANNON_BALL_DIRECT)
		{
			damage_details.Copy(status.attackInfo.atk);
			return Mathf.FloorToInt(status.attackInfo.atk.CalcTotal());
		}
		AtkAttribute atkAttribute = CalcAtk(status);
		AtkAttribute atkAttribute2 = CalcTolerance(status);
		AtkAttribute atkAttribute3 = CalcDefense(status);
		damage_details.normal = (float)(int)InGameUtility.CalcDamageDetailToEnemy(atkAttribute.normal, atkAttribute3.normal, atkAttribute2.normal);
		damage_details.fire = (float)(int)InGameUtility.CalcDamageDetailToEnemy(atkAttribute.fire, atkAttribute3.fire, atkAttribute2.fire);
		damage_details.water = (float)(int)InGameUtility.CalcDamageDetailToEnemy(atkAttribute.water, atkAttribute3.water, atkAttribute2.water);
		damage_details.thunder = (float)(int)InGameUtility.CalcDamageDetailToEnemy(atkAttribute.thunder, atkAttribute3.thunder, atkAttribute2.thunder);
		damage_details.soil = (float)(int)InGameUtility.CalcDamageDetailToEnemy(atkAttribute.soil, atkAttribute3.soil, atkAttribute2.soil);
		damage_details.light = (float)(int)InGameUtility.CalcDamageDetailToEnemy(atkAttribute.light, atkAttribute3.light, atkAttribute2.light);
		damage_details.dark = (float)(int)InGameUtility.CalcDamageDetailToEnemy(atkAttribute.dark, atkAttribute3.dark, atkAttribute2.dark);
		damage_details.CheckMinus();
		float normal = atkAttribute.normal;
		ELEMENT_TYPE elementType = atkAttribute.GetElementType();
		if (regionInfos[status.regionID].isDamageMinimum)
		{
			damage_details.Set(0f);
			if (normal > 0f)
			{
				damage_details.normal = 1f;
			}
			if (elementType != ELEMENT_TYPE.MAX)
			{
				damage_details.SetTargetElement(elementType, 1f);
			}
			int num = Mathf.FloorToInt(damage_details.CalcTotal());
			if (num < 1)
			{
				damage_details.normal = 1f;
				num = 1;
			}
			return num;
		}
		Player player = status.fromObject as Player;
		RegionInfo regionInfo = regionInfos[status.regionID];
		if (player != null)
		{
			if (!status.attackInfo.isSkillReference && !regionInfo.dragonArmorInfo.enabled)
			{
				float val = 1f;
				for (int i = 0; i < regionInfo.weaponTypeRate.Length; i++)
				{
					if (Player.ConvertEquipmentTypeToAttackMode(regionInfo.weaponTypeRate[i].equipmentType) == player.attackMode)
					{
						val = regionInfo.weaponTypeRate[i].rate;
					}
				}
				damage_details.Mul(val);
			}
			BuffParam buffParam = player.buffParam;
			AtkAttribute abilityDamageRate = buffParam.GetAbilityDamageRate(this, status);
			abilityDamageRate.CheckMinus();
			float damageUpRate = buffParam.GetDamageUpRate(player, status);
			if (damageUpRate > 0f)
			{
				abilityDamageRate.AddRate(damageUpRate);
			}
			damage_details.Mul(abilityDamageRate);
		}
		if (status.attackInfo.isSkillReference && IsValidBuff(BuffParam.BUFFTYPE.MAD_MODE))
		{
			damage_details.Mul(MonoBehaviourSingleton<InGameSettingsManager>.I.madModeParam.skillDamagedRate);
		}
		if (regionInfo.dragonArmorInfo.enabled)
		{
			float dragonArmorDamageRate = player.buffParam.GetDragonArmorDamageRate();
			if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.BURST_THS_SINGLE_SHOT || status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.BURST_THS_FULL_BURST)
			{
				float val2 = regionInfo.dragonArmorInfo.thsBurstSpAttackRate + dragonArmorDamageRate;
				damage_details.Mul(val2);
			}
			else
			{
				float val3 = regionInfo.dragonArmorInfo.otherAttackRate + dragonArmorDamageRate;
				damage_details.Mul(val3);
			}
		}
		int num2 = (int)damage_details.CalcTotal();
		if (normal > 0f && damage_details.normal < 1f)
		{
			damage_details.normal = 1f;
			num2++;
		}
		if (elementType != ELEMENT_TYPE.MAX && damage_details.GetElementType() == ELEMENT_TYPE.MAX)
		{
			damage_details.SetTargetElement(elementType, 1f);
			num2++;
		}
		if (num2 < 1)
		{
			damage_details.normal = 1f;
			num2 = 1;
		}
		if (player != null)
		{
			if (!status.attackInfo.isSkillReference)
			{
				if (player.CheckAttackModeAndSpType(Player.ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.NONE))
				{
					damage_details.MulElementOnly(player.CalcChargeExpandElementDamageUpRate());
				}
				float value = 1f;
				if (player.GetOneHandSwordBoostDamageUpRate(ref value))
				{
					damage_details.MulElementOnly(value);
				}
				if (player.GetTwoHandSwordBoostDamageUpRate(ref value))
				{
					damage_details.MulElementOnly(value);
				}
				if (player.GetIaiNormalDamageUp(ref value))
				{
					damage_details.normal *= value;
				}
				if (player.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.HEAT))
				{
					damage_details.Mul(player.CalcPairSwordsBoostModeDamageUpRate());
				}
				if (player.GetSphinxElementDamageUpRate(status.attackInfo, ref value))
				{
					damage_details.MulElementOnly(value);
				}
				if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.JUMP)
				{
					damage_details.MulElementOnly(player.GetJumpElementDamageUpRate());
				}
				if (player.GetExRushElementDamageUpRate(status.attackInfo.attackType, ref value))
				{
					damage_details.MulElementOnly(value);
				}
				if (player.spearCtrl.GetBoostDamageUpRate(ref value))
				{
					damage_details.MulElementOnly(value);
				}
				if (player.GetArrowBoostDamageUpRate(ref value))
				{
					damage_details.MulElementOnly(value);
				}
				if (player.GetBurstShotNormalDamageUpRate(status.attackInfo, ref value))
				{
					damage_details.normal *= value;
				}
				if (player.GetBurstShotElementDamageUpRate(status.attackInfo, ref value))
				{
					damage_details.MulElementOnly(value);
				}
				num2 = (int)damage_details.CalcTotal();
				if (num2 < 1)
				{
					num2 = 1;
				}
				if (isArenaDamageOffWeapon)
				{
					damage_details.Mul(0f);
					num2 = (int)damage_details.CalcTotal();
					if (num2 < 0)
					{
						num2 = 0;
					}
					return num2;
				}
			}
			else if (isArenaDamageOffMagi)
			{
				damage_details.Mul(0f);
				num2 = (int)damage_details.CalcTotal();
				if (num2 < 0)
				{
					num2 = 0;
				}
				return num2;
			}
		}
		if (base.buffParam.IsValidInvinsibleBuff())
		{
			AtkAttribute invinsibleMulRate = GetInvinsibleMulRate();
			damage_details.Mul(invinsibleMulRate);
			num2 = (int)damage_details.CalcTotal();
			if (num2 < 0)
			{
				num2 = 0;
			}
		}
		if (IsValidBuff(BuffParam.BUFFTYPE.GHOST_FORM))
		{
			if (player != null && player.CheckIgnoreBuff(BuffParam.BUFFTYPE.GHOST_FORM))
			{
				return num2;
			}
			damage_details.Mul(GhostFormParam);
			num2 = (int)damage_details.CalcTotal();
			if (num2 < 0)
			{
				num2 = 0;
			}
		}
		if (IsValidShield())
		{
			damage_details.Mul(base.ShieldTolerance);
			num2 = (int)damage_details.CalcTotal();
			if (num2 < 0)
			{
				num2 = 0;
			}
		}
		return num2;
	}

	private AtkAttribute GetInvinsibleMulRate()
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
			}
		}
		return atkAttribute;
	}

	protected override AtkAttribute CalcAtk(AttackedHitStatusLocal status)
	{
		Player player = status.fromObject as Player;
		if (player == null)
		{
			return base.CalcAtk(status);
		}
		AtkAttribute atkAttribute = new AtkAttribute();
		atkAttribute.Add(status.atk);
		if (player.IsTwoHandSwordSpAttacking())
		{
			atkAttribute.normal += player.GetDefForTwoHandSwordSpAttack();
		}
		if (player.IsTwoHandSwordHeatUseGauge())
		{
			ELEMENT_TYPE elementType = atkAttribute.GetElementType();
			atkAttribute.AddTargetElement(elementType, player.GetElementDefForTwoHandSwordHeatCombo());
		}
		if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.HEAL_ATTACK)
		{
			atkAttribute.Mul(player.healAtkRate);
		}
		else
		{
			atkAttribute.Mul(status.attackInfo.atkRate);
			if (!status.attackInfo.isSkillReference)
			{
				atkAttribute.Mul(player.pairSwordsCtrl.GetAtkRate());
			}
			if ((status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.BURST_THS_SINGLE_SHOT || status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.BURST_THS_FULL_BURST) && player.thsCtrl != null)
			{
				atkAttribute.Mul(player.thsCtrl.GetAtkRate(this, player));
			}
		}
		if (status.damageDistanceData != null)
		{
			float num = 1f;
			num = ((!player.isBuffShadowSealing || player.playerParameter.arrowActionInfo.shadowSealingBuffDistanceRate == 0f) ? status.damageDistanceData.GetRate(status.distanceXZ) : player.playerParameter.arrowActionInfo.shadowSealingBuffDistanceRate);
			atkAttribute.Mul(num);
		}
		float val = 1f;
		if (IsFreeze())
		{
			val = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.freezeParam.damageRate;
		}
		else if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.TWO_HAND_SWORD_SP || status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.THS_HEAT_COMBO)
		{
			if (status.weakState != 0)
			{
				val = player.playerParameter.specialActionInfo.twoHandSwordWeakRate;
			}
		}
		else
		{
			switch (status.weakState)
			{
			case WEAK_STATE.WEAK:
			case WEAK_STATE.WEAK_SP_ATTACK:
			case WEAK_STATE.WEAK_SP_DOWN_MAX:
				val = player.attackWeakRate;
				break;
			case WEAK_STATE.DOWN:
				val = player.attackDownRate;
				break;
			case WEAK_STATE.WEAK_ELEMENT_ATTACK:
				val = player.elementWeakRate;
				break;
			case WEAK_STATE.WEAK_ELEMENT_SKILL_ATTACK:
				val = player.elementSkillWeakRate;
				break;
			case WEAK_STATE.WEAK_SKILL_ATTACK:
				val = player.skillWeakRate;
				break;
			case WEAK_STATE.WEAK_HEAL_ATTACK:
				val = player.healWeakRate;
				break;
			case WEAK_STATE.WEAK_ELEMENT_SP_ATTACK:
				val = player.elementSpAttackWeakRate;
				break;
			}
		}
		atkAttribute.Mul(val);
		return atkAttribute;
	}

	protected override AtkAttribute CalcTolerance(AttackedHitStatusLocal status)
	{
		int regionID = status.regionID;
		if (regionID < 0 || regionID >= regionInfos.Length)
		{
			return base.CalcTolerance(status);
		}
		RegionInfo regionInfo = regionInfos[status.regionID];
		AtkAttribute _tolerance = new AtkAttribute();
		_tolerance.Add(regionInfo.tolerance);
		_tolerance.normal -= (float)buffParam.GetValue(BuffParam.BUFFTYPE.DEFDOWN_RATE_NORMAL, true) * 0.01f;
		if (_tolerance.normal < 0f)
		{
			_tolerance.normal = 0f;
		}
		AtkAttribute atkAttribute = new AtkAttribute();
		atkAttribute.SetTargetElement(ELEMENT_TYPE.FIRE, (float)buffParam.GetValue(BuffParam.BUFFTYPE.DEFDOWN_RATE_FIRE, true) * 0.01f);
		atkAttribute.SetTargetElement(ELEMENT_TYPE.WATER, (float)buffParam.GetValue(BuffParam.BUFFTYPE.DEFDOWN_RATE_WATER, true) * 0.01f);
		atkAttribute.SetTargetElement(ELEMENT_TYPE.THUNDER, (float)buffParam.GetValue(BuffParam.BUFFTYPE.DEFDOWN_RATE_THUNDER, true) * 0.01f);
		atkAttribute.SetTargetElement(ELEMENT_TYPE.SOIL, (float)buffParam.GetValue(BuffParam.BUFFTYPE.DEFDOWN_RATE_SOIL, true) * 0.01f);
		atkAttribute.SetTargetElement(ELEMENT_TYPE.LIGHT, (float)buffParam.GetValue(BuffParam.BUFFTYPE.DEFDOWN_RATE_LIGHT, true) * 0.01f);
		atkAttribute.SetTargetElement(ELEMENT_TYPE.DARK, (float)buffParam.GetValue(BuffParam.BUFFTYPE.DEFDOWN_RATE_DARK, true) * 0.01f);
		atkAttribute.AddElementOnly((float)buffParam.GetValue(BuffParam.BUFFTYPE.DEFDOWN_RATE_ALLELEMENT, true) * 0.01f);
		_tolerance.Sub(atkAttribute);
		_tolerance.CheckMinus();
		if (CheckApplyDefenceUpBuff(status, regionWorks[regionID]))
		{
			AddToleranceBuff(ref _tolerance);
			_tolerance.normal += (float)(buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_NORMAL, true) + buffParam.GetValue(BuffParam.BUFFTYPE.DEFENCE_ALLELEMENT, true)) * 0.01f;
		}
		if (IsValidBarrier)
		{
			_tolerance.AddRate(regionWorks[regionID].GetBarrierToleranceRate());
		}
		return _tolerance;
	}

	private bool CheckApplyDefenceUpBuff(AttackedHitStatusLocal status, EnemyRegionWork regionWork)
	{
		bool result = true;
		ENEMY_TYPE enemyType = GetEnemyType();
		if (enemyType == ENEMY_TYPE.CRAB && regionWork.weakState == WEAK_STATE.WEAK_ELEMENT_SKILL_ATTACK && regionWork.validElementType == (int)status.atk.GetElementType() && status.attackInfo.isSkillReference && IsValidBuff(BuffParam.BUFFTYPE.DEFENCE_ALLELEMENT) && IsValidBuff(BuffParam.BUFFTYPE.DEFENCE_NORMAL))
		{
			OnBuffEnd(BuffParam.BUFFTYPE.DEFENCE_ALLELEMENT, true, true);
			OnBuffEnd(BuffParam.BUFFTYPE.DEFENCE_NORMAL, true, true);
			result = false;
		}
		return result;
	}

	protected override AtkAttribute CalcDefense(AttackedHitStatusLocal status)
	{
		if (status.regionID < 0 || status.regionID >= regionInfos.Length)
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
		RegionInfo regionInfo = regionInfos[status.regionID];
		return regionInfo.defence;
	}

	public void CheckCounterRegion()
	{
		bool flag = GetEnabledCounterRegionIndex() >= 0;
		uint stringID = kStrIdx_EnemyReaction_BreakCounterRegion;
		switch (m_CounterRegionState)
		{
		case eCounterRegionState.NONE:
			m_CounterRegionState = (flag ? eCounterRegionState.EXIST : eCounterRegionState.NOT_EXIST);
			return;
		case eCounterRegionState.EXIST:
			if (flag)
			{
				return;
			}
			m_CounterRegionState = eCounterRegionState.NOT_EXIST;
			stringID = kStrIdx_EnemyReaction_BreakCounterRegion;
			break;
		case eCounterRegionState.NOT_EXIST:
			if (!flag)
			{
				return;
			}
			m_CounterRegionState = eCounterRegionState.EXIST;
			stringID = kStrIdx_EnemyReaction_ReviveCounterRegion;
			break;
		}
		if (MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
		{
			MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestAnnounce(string.Empty, STRING_CATEGORY.ENEMY_REACTION, stringID);
		}
	}

	protected bool isAvailableCounter(ACTION_ID currentId)
	{
		if (currentId == ACTION_ID.FREEZE || currentId == ACTION_ID.PARALYZE || currentId == (ACTION_ID)13 || currentId == (ACTION_ID)16 || currentId == (ACTION_ID)17 || currentId == (ACTION_ID)19)
		{
			return false;
		}
		if (IsDebuffShadowSealing())
		{
			return false;
		}
		if (IsLightRing())
		{
			return false;
		}
		if (currentId == ACTION_ID.NONE)
		{
			return false;
		}
		return true;
	}

	protected bool CheckCounter(AttackedHitStatusOwner status)
	{
		if (!isBoss)
		{
			return false;
		}
		if (!isAvailableCounter(base.actionID))
		{
			return false;
		}
		EnemyRegionWork enabledCounterRegion = GetEnabledCounterRegion();
		if (enabledCounterRegion != null)
		{
			EnemyController enemyController = base.controller as EnemyController;
			if (enemyController == null)
			{
				return false;
			}
			EnemyBrain enemyBrain = enemyController.brain as EnemyBrain;
			if (enemyBrain == null)
			{
				return false;
			}
			int counterAttackId = enemyBrain.actionCtrl.GetCounterAttackId();
			if (counterAttackId == 2147483647)
			{
				return false;
			}
			if ((uint)base.attackID == counterAttackId)
			{
				return false;
			}
			return true;
		}
		return false;
	}

	public override void OnAttackedHitOwner(AttackedHitStatusOwner status)
	{
		ApplyInvicibleCount(status);
		bool flag = ApplyInvicibleBadStatus(status);
		if (IsValidBuff(BuffParam.BUFFTYPE.MAD_MODE))
		{
			status.badStatusAdd.Mul(MonoBehaviourSingleton<InGameSettingsManager>.I.madModeParam.badStatusRate);
		}
		status.aegisParam.isChange = false;
		if (!base.isDead && status.validDamage)
		{
			if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.CANNON_BALL)
			{
				status.damage = 0;
				status.damageDetails.Set(0f);
			}
			int num = base.ShieldHp;
			int num2 = GrabHp;
			status.afterGrabHp = GrabHp;
			status.afterBarrierHp = BarrierHp;
			status.downTotal = downTotal;
			if (status.regionID >= 0 && status.regionID < regionWorks.Length)
			{
				status.afterRegionHP = regionWorks[status.regionID].hp;
			}
			if (status.attackInfo.isSkillReference)
			{
				if (status.badStatusAdd.electricShock > 0f && GetElementType() == ELEMENT_TYPE.WATER)
				{
					Player player = status.fromObject as Player;
					if (player != null)
					{
						OnElectricShockStart(status.attackInfo, player);
					}
				}
				if (status.attackInfo.buffIDs != null && status.attackInfo.buffIDs.Length > 0)
				{
					ApplyBuffsByTable(status);
				}
				if (CheckCounter(status))
				{
					status.reactionType = 13;
					if (MonoBehaviourSingleton<CoopManager>.IsValid())
					{
						MonoBehaviourSingleton<CoopManager>.I.coopStage.battleUserLog.Add(this, status);
					}
					base.OnAttackedHitOwner(status);
					return;
				}
			}
			bool flag2 = false;
			if (!object.ReferenceEquals(aegisCtrl, null))
			{
				flag2 = aegisCtrl.IsValid();
				aegisCtrl.FlagReset();
			}
			if (!flag2 && CheckMadMode(status))
			{
				status.reactionType = 18;
				status.badStatusAdd.Reset();
				downTotal = 0f;
				status.downAddBase = 0f;
				status.downAddWeak = 0f;
				status.isArrowBleed = false;
				status.isShadowSealing = false;
			}
			if (MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.shadowSealingParam.isReactionDamage && status.isShadowSealing && !IsDebuffShadowSealing())
			{
				status.reactionType = 1;
			}
			int num3 = 0;
			if (status.regionID >= 0 && status.regionID < regionWorks.Length)
			{
				EnemyRegionWork enemyRegionWork = regionWorks[status.regionID];
				RegionInfo regionInfo = regionInfos[status.regionID];
				num3 = regionInfo.customDownRate;
				float num4 = CalcRegionDamageRate(status);
				int num5 = (int)((float)status.damage * num4);
				if (flag2)
				{
					if (aegisCtrl.Damage(num5))
					{
						status.aegisParam.Copy(aegisCtrl.syncParam);
					}
				}
				else
				{
					status.afterRegionHP = (int)enemyRegionWork.hp - num5;
					if (status.afterRegionHP < 0)
					{
						status.afterRegionHP = 0;
					}
				}
				if (status.regionID != 0 && (int)enemyRegionWork.hp > 0 && status.afterRegionHP <= 0)
				{
					status.breakRegion = true;
					if (!IsDebuffShadowSealing() && status.reactionType != 18)
					{
						if (regionInfo.breakInDown)
						{
							status.reactionType = 7;
						}
						else if (regionInfo.breakInDamage)
						{
							status.reactionType = 1;
						}
					}
				}
				if (IsValidBarrier)
				{
					int num6 = CalcBarrierDamage(enemyRegionWork, status);
					status.afterBarrierHp = (int)BarrierHp - num6;
					if (status.afterBarrierHp < 0)
					{
						status.afterBarrierHp = 0;
					}
				}
				if (regionInfo.isEnableShieldDamage && status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.CANNON_BALL)
				{
					EFFECTIVE_TYPE effectiveType = GetEffectiveType(status.attackInfo.elementType, GetElementType());
					bool isElementCritical = effectiveType == EFFECTIVE_TYPE.GOOD;
					status.shieldDamage = CalcShieldDamage(enemyRegionWork.isShieldCriticalDamage, isElementCritical, status.attackInfo);
					if (enemyRegionWork.isShieldCriticalDamage && IsValidGrabHp)
					{
						status.afterGrabHp = (int)GrabHp - (int)GrabCannonDamage;
						if (status.afterGrabHp < 0)
						{
							status.afterGrabHp = 0;
						}
					}
				}
				bool flag3 = false;
				if ((int)enemyRegionWork.hp <= 0 && regionInfo.maxHP > 0)
				{
					flag3 = true;
				}
				if ((status.breakRegion || flag3) && !regionInfo.breakAfterHit)
				{
					status.isArrowBleed = false;
				}
				if (status.isArrowBleed)
				{
					float arrowBleedTimeInterval = MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.arrowBleedTimeInterval;
					float num7 = 1f - MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.arrowBleedSkipTimeRate;
					float num8 = bleedCounter % arrowBleedTimeInterval;
					status.arrowBleedSkipFirst = (num8 >= arrowBleedTimeInterval * num7);
				}
				if ((flag3 && !regionInfo.breakAfterHit) || status.breakRegion)
				{
					status.isShadowSealing = false;
				}
				if (IsWeakStateCheckAlreadyHit(status.weakState) && status.downAddWeak > 0f && enemyRegionWork.weakAttackIDs.Count > 0)
				{
					status.downAddWeak = 0f;
				}
			}
			if (base.actionID == (ACTION_ID)13)
			{
				if (!status.isForceDown)
				{
					float num11 = status.downAddBase = (status.downAddWeak = 0f);
				}
			}
			else
			{
				float num12 = CalcWorkDownTotal(status.downAddBase, status.downAddWeak, status.regionID);
				status.downTotal = downTotal + num12;
				if (status.downTotal >= (float)downMax)
				{
					status.reactionType = 7;
					status.downTotal = (float)downMax;
				}
			}
			if (base.actionID != (ACTION_ID)17)
			{
				int num13 = (int)base.ShieldHp - status.shieldDamage;
				if (num13 <= 0 && num > 0 && (int)base.ShieldHpMax > 0)
				{
					status.reactionType = 16;
				}
			}
			if (status.afterGrabHp <= 0 && num2 > 0 && (int)GrabHpMax > 0)
			{
				ActReleaseGrabbedPlayers(false, false, true, 0f, 0f);
			}
			if (MonoBehaviourSingleton<CoopManager>.IsValid())
			{
				MonoBehaviourSingleton<CoopManager>.I.coopStage.battleUserLog.Add(this, status);
			}
		}
		if (flag)
		{
			buffParam.DecreaseInvincibleBadStatus();
		}
		base.OnAttackedHitOwner(status);
	}

	private void ApplyBuffsByTable(AttackedHitStatusOwner status)
	{
		if (Singleton<BuffTable>.IsValid())
		{
			uint[] buffIDs = status.attackInfo.buffIDs;
			foreach (uint num in buffIDs)
			{
				if (num != 0)
				{
					BuffTable.BuffData data = Singleton<BuffTable>.I.GetData(num);
					if (data != null)
					{
						BuffParam.BuffData buffData = new BuffParam.BuffData();
						buffData.type = data.type;
						buffData.interval = data.interval;
						buffData.valueType = data.valueType;
						buffData.time = data.duration;
						buffData.fromObjectID = status.fromObjectID;
						float num2 = (float)data.value;
						GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(data.growID, status.skillParam.baseInfo.level);
						if (growSkillItemData != null)
						{
							buffData.time = data.duration * (float)(int)growSkillItemData.supprtTime[0].rate * 0.01f + (float)growSkillItemData.supprtTime[0].add;
							num2 = (float)(data.value * (int)growSkillItemData.supprtValue[0].rate) * 0.01f + (float)(int)growSkillItemData.supprtValue[0].add;
						}
						if (buffData.valueType == BuffParam.VALUE_TYPE.RATE && BuffParam.IsTypeValueBasedOnHP(buffData.type))
						{
							num2 = (float)base.hpMax * num2 * 0.01f;
						}
						buffData.value = Mathf.FloorToInt(num2);
						switch (buffData.type)
						{
						case BuffParam.BUFFTYPE.ELECTRIC_SHOCK:
							if (GetElementType() == ELEMENT_TYPE.WATER)
							{
								OnBuffStart(buffData);
							}
							break;
						case BuffParam.BUFFTYPE.BURNING:
							if (GetElementType() == ELEMENT_TYPE.SOIL)
							{
								OnBuffStart(buffData);
							}
							break;
						case BuffParam.BUFFTYPE.MOVE_SPEED_DOWN:
						case BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN:
							if (GetElementType() == ELEMENT_TYPE.THUNDER)
							{
								OnBuffStart(buffData);
							}
							break;
						default:
							OnBuffStart(buffData);
							break;
						}
					}
				}
			}
		}
	}

	private void ResetElementDebuff(ELEMENT_TYPE prev, ELEMENT_TYPE now)
	{
		if (prev != now)
		{
			switch (prev)
			{
			case ELEMENT_TYPE.LIGHT:
				break;
			case ELEMENT_TYPE.WATER:
				OnBuffEnd(BuffParam.BUFFTYPE.ELECTRIC_SHOCK, true, true);
				break;
			case ELEMENT_TYPE.SOIL:
				OnBuffEnd(BuffParam.BUFFTYPE.BURNING, true, true);
				break;
			case ELEMENT_TYPE.THUNDER:
				OnBuffEnd(BuffParam.BUFFTYPE.MOVE_SPEED_DOWN, true, true);
				OnBuffEnd(BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN, true, true);
				break;
			case ELEMENT_TYPE.DARK:
				OnBuffEnd(BuffParam.BUFFTYPE.LIGHT_RING, true, true);
				break;
			}
		}
	}

	private float CalcRegionDamageRate(AttackedHitStatusOwner status)
	{
		Player player = status.fromObject as Player;
		if (object.ReferenceEquals(player, null))
		{
			return 1f;
		}
		RegionInfo regionInfo = null;
		if (regionInfos != null && status.regionID < regionInfos.Length)
		{
			regionInfo = regionInfos[status.regionID];
		}
		float num = 1f;
		if (regionInfo != null)
		{
			num = player.GetRegionDamageRate(regionInfo.dragonArmorInfo.enabled);
		}
		AttackHitInfo.ToEnemy.DamageToRegionInfo damageToRegionInfo = status.attackInfo.toEnemy.damageToRegionInfo;
		if (!damageToRegionInfo.isDamageUp)
		{
			return num;
		}
		int damageUpPercent = damageToRegionInfo.damageUpPercent;
		if (damageUpPercent <= 0)
		{
			return num;
		}
		damageUpPercent = damageToRegionInfo.damageUpPercent;
		if (status.attackInfo.isSkillReference)
		{
			SkillInfo.SkillParam skillParam = status.skillParam;
			if (skillParam != null)
			{
				GrowSkillItemTable.GrowSkillItemData growSkillItemData = Singleton<GrowSkillItemTable>.I.GetGrowSkillItemData(skillParam.tableData.growID, skillParam.baseInfo.level);
				if (growSkillItemData != null)
				{
					damageUpPercent = growSkillItemData.GetGrowResultSupportValue(damageUpPercent, 0);
				}
			}
		}
		return num + (float)damageUpPercent * 0.01f;
	}

	private int CalcBarrierDamage(EnemyRegionWork regionWork, AttackedHitStatusOwner status)
	{
		int num = 0;
		RegionInfo regionInfo = regionWork.regionInfo;
		AtkAttribute atkBarrierDamage = regionInfo.atkBarrierDamage;
		Player player = status.fromObject as Player;
		if (player != null && player.buffParam.IsValidBuff(BuffParam.BUFFTYPE.LUNATIC_TEAR))
		{
			num = regionWork.regionInfo.barrierDamageSp;
		}
		AtkAttribute damageDetails = status.damageDetails;
		if (damageDetails.normal > 0f)
		{
			num += (int)atkBarrierDamage.normal;
		}
		if (damageDetails.fire > 0f)
		{
			num += (int)atkBarrierDamage.fire;
		}
		if (damageDetails.water > 0f)
		{
			num += (int)atkBarrierDamage.water;
		}
		if (damageDetails.thunder > 0f)
		{
			num += (int)atkBarrierDamage.thunder;
		}
		if (damageDetails.soil > 0f)
		{
			num += (int)atkBarrierDamage.soil;
		}
		if (damageDetails.light > 0f)
		{
			num += (int)atkBarrierDamage.light;
		}
		if (damageDetails.dark > 0f)
		{
			num += (int)atkBarrierDamage.dark;
		}
		return num;
	}

	public override bool IsEnableAttackedHitOwner()
	{
		if (MonoBehaviourSingleton<CoopManager>.IsValid() && MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<CoopManager>.I.coopRoom.isOwnerFirstClear && MonoBehaviourSingleton<StageObjectManager>.I.boss == this && MonoBehaviourSingleton<InGameProgress>.I.isEnding)
		{
			return false;
		}
		return base.IsEnableAttackedHitOwner();
	}

	protected override bool IsHitReactionValid(AttackedHitStatusOwner status)
	{
		if (base.actionID == (ACTION_ID)13)
		{
			return false;
		}
		if (status.fromType == OBJECT_TYPE.ENEMY)
		{
			return false;
		}
		return base.IsHitReactionValid(status);
	}

	protected override bool IsReactionDelayType(int type)
	{
		switch (type)
		{
		case 7:
		case 14:
			return true;
		case 13:
			return true;
		case 16:
			return true;
		case 17:
			return true;
		case 18:
			return true;
		default:
			return base.IsReactionDelayType(type);
		}
	}

	protected override REACTION_TYPE OnHitReaction(AttackedHitStatusOwner status)
	{
		AttackHitInfo.ToEnemy.REACTION_TYPE reactionType = status.attackInfo.toEnemy.reactionType;
		if (status.attackInfo.isSkillReference && status.skillParam != null)
		{
			reactionType = status.skillParam.tableData.reactionType;
		}
		if (isBoss || isWaveMatchBoss || isBigMonster || IsFieldEnemyBoss())
		{
			if (reactionType != 0)
			{
				switch (reactionType)
				{
				case AttackHitInfo.ToEnemy.REACTION_TYPE.DAMAGE:
					return REACTION_TYPE.DAMAGE;
				case AttackHitInfo.ToEnemy.REACTION_TYPE.DOWN:
					return REACTION_TYPE.DOWN;
				}
			}
			else
			{
				if (status.weakState == WEAK_STATE.WEAK && status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.CANNON_BALL_DIRECT && base.actionID != ACTION_ID.DAMAGE && base.actionID != (ACTION_ID)13)
				{
					return REACTION_TYPE.DAMAGE;
				}
				if (status.weakState == WEAK_STATE.WEAK && status.attackInfo.toEnemy.isWeakHitReaction && base.actionID != ACTION_ID.DAMAGE && base.actionID != (ACTION_ID)13)
				{
					return REACTION_TYPE.DAMAGE;
				}
				if ((IsWeakStateSpAttack(status.weakState) || IsWeakStateElementAttack(status.weakState) || IsWeakStateSkillAttack(status.weakState) || IsWeakStateHealAttack(status.weakState)) && base.actionID != ACTION_ID.DAMAGE && base.actionID != (ACTION_ID)13)
				{
					return REACTION_TYPE.DAMAGE;
				}
			}
		}
		return REACTION_TYPE.NONE;
	}

	public override void OnHitAttack(AttackHitInfo info, AttackHitColliderProcessor.HitParam hit_param)
	{
		EnemyController enemyController = base.controller as EnemyController;
		if (enemyController != null)
		{
			enemyController.OnHitAttack(hit_param.toObject);
		}
		GrabInfo grabInfo = info.grabInfo;
		if (grabInfo.enable)
		{
			Player player = hit_param.toObject as Player;
			if (player != null)
			{
				EnemyBrain enemyBrain = enemyController.brain as EnemyBrain;
				if (enemyBrain != null)
				{
					DrainAttackInfo drainAttackInfo = SearchDrainAttackInfo(grabInfo.drainAttackId);
					enemyBrain.actionCtrl.grabController.Grab(player, grabInfo, drainAttackInfo);
					if (drainAttackInfo != null)
					{
						grabDrainRecoverTimer = drainAttackInfo.recoverInterval;
					}
				}
			}
		}
		base.OnHitAttack(info, hit_param);
	}

	public float CalcWorkDownTotal(float downAddBase, float downAddWeak, int regionID)
	{
		float num = downAddBase + downAddWeak;
		int num2 = 0;
		if (regionID >= 0 && regionID < regionWorks.Length)
		{
			RegionInfo regionInfo = regionInfos[regionID];
			num2 = regionInfo.customDownRate;
		}
		if (num2 != 0)
		{
			float num3 = (float)num2 * 0.01f;
			num += num * num3;
			if (num < 0f)
			{
				num = 0f;
			}
		}
		return num;
	}

	public override void OnAttackedHitFix(AttackedHitStatusFix status)
	{
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b5: Expected O, but got Unknown
		//IL_0631: Unknown result type (might be due to invalid IL or missing references)
		//IL_0638: Expected O, but got Unknown
		//IL_07e2: Unknown result type (might be due to invalid IL or missing references)
		Player player = status.fromObject as Player;
		bool isDead = base.isDead;
		int hp = base.hp;
		int num = base.ShieldHp;
		base.OnAttackedHitFix(status);
		if (!isDead)
		{
			downHealInterval = 1f;
			downTotal = status.downTotal;
			if (status.downAddBase + status.downAddWeak > 0f && MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
			{
				MonoBehaviourSingleton<UIEnemyStatus>.I.DirectionDownGauge(status);
			}
		}
		BarrierHp = status.afterBarrierHp;
		GrabHp = status.afterGrabHp;
		if (!object.ReferenceEquals(aegisCtrl, null))
		{
			aegisCtrl.Sync(status.aegisParam);
		}
		if ((int)base.ShieldHp <= 0 && num > 0)
		{
			if (MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
			{
				MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestAnnounce(enemyTableData.name, STRING_CATEGORY.ENEMY_SHIELD, 1u);
			}
			EffectManager.GetEffect("ef_btl_goldbird_aura_01_01", base._transform);
			ResetShieldShaderParam();
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
			{
				List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
				for (int i = 0; i < playerList.Count; i++)
				{
					Self self = playerList[i] as Self;
					if (self != null)
					{
						self.CancelCannonMode();
						self.ActIdle(false, -1f);
					}
					else
					{
						Player player2 = playerList[i] as Player;
						if (player2 != null)
						{
							player2.CancelCannonMode();
							player2.ActIdle(false, -1f);
						}
					}
				}
			}
		}
		if (status.regionID >= 0 && status.regionID < regionWorks.Length)
		{
			EnemyRegionWork enemyRegionWork = regionWorks[status.regionID];
			RegionInfo region_info = regionInfos[status.regionID];
			enemyRegionWork.hp = status.afterRegionHP;
			if (!isDead)
			{
				if (IsWeakStateCheckAlreadyHit(status.weakState) && !enemyRegionWork.weakAttackIDs.Contains(status.fromObjectID))
				{
					enemyRegionWork.weakAttackIDs.Add(status.fromObjectID);
				}
				switch (status.weakState)
				{
				case WEAK_STATE.WEAK:
					OnHitWeakPoint(enemyRegionWork.deleteAtkName, false);
					break;
				case WEAK_STATE.WEAK_SP_ATTACK:
				case WEAK_STATE.WEAK_SP_DOWN_MAX:
					if (status.IsSpAttackHit)
					{
						OnHitWeakPoint(enemyRegionWork.deleteAtkName, true);
					}
					break;
				case WEAK_STATE.WEAK_ELEMENT_ATTACK:
					if (enemyRegionWork.validElementType == (int)status.damageDetails.GetElementType())
					{
						OnHitWeakPoint(enemyRegionWork.deleteAtkName, true);
					}
					break;
				case WEAK_STATE.WEAK_ELEMENT_SKILL_ATTACK:
					if (status.attackInfo.isSkillReference && enemyRegionWork.validElementType == (int)status.attackInfo.atk.GetElementType())
					{
						OnHitWeakPoint(enemyRegionWork.deleteAtkName, true);
					}
					break;
				case WEAK_STATE.WEAK_SKILL_ATTACK:
					if (status.attackInfo.isSkillReference)
					{
						OnHitWeakPoint(enemyRegionWork.deleteAtkName, true);
					}
					break;
				case WEAK_STATE.WEAK_HEAL_ATTACK:
					if (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.HEAL_ATTACK)
					{
						OnHitWeakPoint(enemyRegionWork.deleteAtkName, true);
					}
					break;
				case WEAK_STATE.WEAK_ELEMENT_SP_ATTACK:
					if (enemyRegionWork.validElementType == (int)status.damageDetails.GetElementType() && status.IsSpAttackHit)
					{
						OnHitWeakPoint(enemyRegionWork.deleteAtkName, true);
					}
					break;
				}
				if (IsWeakStateDisplaySign(status.weakState))
				{
					enemyRegionWork.displayTimer = 0f;
				}
				_CheckHitFixArrow(status, enemyRegionWork);
			}
			if (status.breakRegion)
			{
				ActReleaseGrabbedPlayers(false, false, true, 0f, 0f);
				bool isBroke = enemyRegionWork.isBroke;
				enemyRegionWork.isBroke = true;
				enemyRegionWork.breakTime = Time.get_time();
				string[] deactivateObjects = region_info.deactivateObjects;
				foreach (string name in deactivateObjects)
				{
					Transform val = FindNode(name);
					if (val != null)
					{
						val.get_gameObject().SetActive(false);
					}
				}
				int k = 0;
				for (int num2 = regionWorks.Length; k < num2; k++)
				{
					regionWorks[k].OnBreakRegion(status.regionID);
				}
				if (!region_info.breakAfterHit)
				{
					int l = 0;
					for (int count = enemyRegionWork.bleedWorkList.Count; l < count; l++)
					{
						BleedWork bleedWork = enemyRegionWork.bleedWorkList[l];
						if (bleedWork.bleedEffect != null)
						{
							EffectManager.ReleaseEffect(bleedWork.bleedEffect.get_gameObject(), true, false);
							bleedWork.bleedEffect = null;
						}
					}
					enemyRegionWork.bleedWorkList.Clear();
					enemyRegionWork.bleedList.Clear();
				}
				enemyRegionWork.shadowSealingData.ownerID = 0;
				enemyRegionWork.shadowSealingData.existSec = 0f;
				enemyRegionWork.shadowSealingData.extendRate = 1f;
				if (!object.ReferenceEquals(enemyRegionWork.shadowSealingEffect, null))
				{
					EffectManager.ReleaseEffect(enemyRegionWork.shadowSealingEffect.get_gameObject(), false, true);
					enemyRegionWork.shadowSealingEffect = null;
				}
				if (MonoBehaviourSingleton<TargetMarkerManager>.IsValid())
				{
					MonoBehaviourSingleton<TargetMarkerManager>.I.updateShadowSealingFlag = true;
				}
				if (region_info.breakEffect != null && !string.IsNullOrEmpty(region_info.breakEffect.effectName))
				{
					PlayRegionBreakEffect(region_info);
				}
				if (enemyReward != null && !isBroke)
				{
					enemyReward.reward.ForEach(delegate(QuestStartData.RegionDropItem item)
					{
						if (item.regionId == status.regionID && item.breakReward.Count != 0)
						{
							item.breakReward.ForEach(delegate(QuestStartData.BreakItem breakItem)
							{
								CreateDropItemFromRegionBreak(region_info, MonoBehaviourSingleton<StageObjectManager>.I.self, breakItem.rarity);
							});
						}
					});
				}
				if (player != null && MonoBehaviourSingleton<UIPlayerAnnounce>.IsValid())
				{
					if (player is Self)
					{
						if (region_info.dragonArmorInfo.enabled)
						{
							MonoBehaviourSingleton<UIInGameSelfAnnounceManager>.I.PlayDragonArmorBreak();
						}
						else
						{
							MonoBehaviourSingleton<UIInGameSelfAnnounceManager>.I.PlayRegionBreak();
						}
						SoundManager.PlayOneshotJingle(40000156, null, null);
					}
					else if (region_info.dragonArmorInfo.enabled)
					{
						MonoBehaviourSingleton<UIPlayerAnnounce>.I.Announce(UIPlayerAnnounce.ANNOUNCE_TYPE.DRAGON_ARMOR, player);
					}
					else
					{
						MonoBehaviourSingleton<UIPlayerAnnounce>.I.Announce(UIPlayerAnnounce.ANNOUNCE_TYPE.REGION, player);
					}
				}
				UpdateBreakIDLists();
			}
		}
		if (enemyReward != null)
		{
			int m = 0;
			for (int count2 = enemyReward.drop.hpRate.Count; m < count2; m++)
			{
				if (!(enemyReward.drop.hpRate[m] < damageHpRate))
				{
					if (enemyReward.drop.hpRate[m] > status.damageHpRate)
					{
						break;
					}
					CreateDropItem(status.hitPos, MonoBehaviourSingleton<StageObjectManager>.I.self, enemyReward.drop.rarity[m], false);
				}
			}
		}
		damageHpRate = status.damageHpRate;
		if (!object.ReferenceEquals(player, null) && MonoBehaviourSingleton<UIPlayerAnnounce>.IsValid())
		{
			if (IsWeakStateCheckAlreadyHit(status.weakState))
			{
				MonoBehaviourSingleton<UIPlayerAnnounce>.I.Announce(UIPlayerAnnounce.ANNOUNCE_TYPE.WEAK, player);
			}
			if (status.reactionType == 7)
			{
				MonoBehaviourSingleton<UIPlayerAnnounce>.I.Announce(UIPlayerAnnounce.ANNOUNCE_TYPE.DOWN, player);
			}
		}
		if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
		{
			int num3 = status.damage;
			int num4 = hp - base.hp;
			if (num4 < 0)
			{
				num4 = 0;
			}
			if (num3 > num4)
			{
				num3 = num4;
			}
			MonoBehaviourSingleton<InGameRecorder>.I.RecordGivenDamage(status.fromObjectID, num3);
			if (QuestManager.IsValidInGameExplore() && isBoss && MonoBehaviourSingleton<CoopManager>.I.coopMyClient.clientId == status.fromClientID)
			{
				ExplorePlayerStatus myExplorePlayerStatus = MonoBehaviourSingleton<QuestManager>.I.GetMyExplorePlayerStatus();
				int num5 = myExplorePlayerStatus.givenTotalDamage + num3;
				myExplorePlayerStatus.SyncTotalDamageToBoss(num5);
				MonoBehaviourSingleton<CoopManager>.I.coopRoom.packetSender.SendExploreBossDamage(num5);
			}
		}
		if (base.hp <= 0 && player != null)
		{
			if (player is Self)
			{
				MonoBehaviourSingleton<InGameProgress>.I.AddDefeatCount(isWaveMatchBoss);
			}
			if (isWaveMatchBoss)
			{
				MonoBehaviourSingleton<InGameProgress>.I.AddPartyDefeatBossCount();
			}
		}
	}

	private void _CheckHitFixArrow(AttackedHitStatusFix status, EnemyRegionWork region_work)
	{
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		if (!base.isDead)
		{
			TargetPoint targetPoint = null;
			int i = 0;
			for (int num = targetPoints.Length; i < num; i++)
			{
				if (targetPoints[i].regionID == status.regionID && targetPoints[i].isAimEnable)
				{
					targetPoint = targetPoints[i];
					break;
				}
			}
			if (status.isShadowSealing)
			{
				if (!IsDebuffShadowSealing())
				{
					ShadowSealingData shadowSealingData = region_work.shadowSealingData;
					if (shadowSealingData.ownerID == 0)
					{
						shadowSealingData.ownerID = status.fromObjectID;
						shadowSealingData.existSec = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.shadowSealingExistSec * badStatusMax.shadowSealing;
						Player player = status.fromObject as Player;
						if (!object.ReferenceEquals(player, null))
						{
							shadowSealingData.extendRate = player.buffParam.GetShadowSealingExtend();
							shadowSealingData.existSec *= player.buffParam.GetShadowSealingExtendArrow();
						}
						else
						{
							shadowSealingData.extendRate = 1f;
						}
						if (shadowSealingData.existSec <= MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.shadowSealingExistMinSec)
						{
							shadowSealingData.existSec = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.shadowSealingExistMinSec;
						}
						if (!object.ReferenceEquals(targetPoint, null) && object.ReferenceEquals(region_work.shadowSealingEffect, null))
						{
							region_work.shadowSealingEffect = targetPoint.PlayArrowBleedEffect(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.shadowSealingEffectName, 0);
							if (!object.ReferenceEquals(region_work.shadowSealingEffect, null))
							{
								EffectSizeCtrl component = region_work.shadowSealingEffect.GetComponent<EffectSizeCtrl>();
								if (!object.ReferenceEquals(component, null))
								{
									component.Work(shadowSealingData.existSec);
								}
							}
						}
						if ((IsMirror() || IsPuppet()) && !IsValidWaitingPacket(WAITING_PACKET.ENEMY_UPDATE_SHADOWSEALING))
						{
							StartWaitingPacket(WAITING_PACKET.ENEMY_UPDATE_SHADOWSEALING, false, shadowSealingData.existSec);
						}
					}
					if (_CheckShadowSealingFullStuck())
					{
						ActDebuffShadowSealingStart();
					}
					else if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
					{
						MonoBehaviourSingleton<UIEnemyStatus>.I.DirectionShadowSealingGauge(status.hitPos);
					}
				}
			}
			else if (status.isArrowBleed)
			{
				BleedData bleedData = null;
				int j = 0;
				for (int count = region_work.bleedList.Count; j < count; j++)
				{
					if (region_work.bleedList[j].ownerID == status.fromObjectID)
					{
						bleedData = region_work.bleedList[j];
						break;
					}
				}
				if (bleedData == null)
				{
					bleedData = new BleedData();
					region_work.bleedList.Add(bleedData);
				}
				InGameSettingsManager.Player.SpecialActionInfo specialActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo;
				bleedData.ownerID = status.fromObjectID;
				if (bleedData.lv == 0)
				{
					bleedData.cnt = specialActionInfo.arrowBleedCount;
					bleedData.skipFirst = status.arrowBleedSkipFirst;
				}
				int num2 = status.arrowBleedDamage / specialActionInfo.arrowBleedCount;
				if (num2 < 1)
				{
					num2 = 1;
				}
				bleedData.damage += num2;
				if (bleedData.IsOwnerSelf())
				{
					bleedData.lv++;
				}
				bool flag = true;
				if (!specialActionInfo.arrowBleedOther.enable && !bleedData.IsOwnerSelf())
				{
					flag = false;
				}
				if (targetPoint != null && flag)
				{
					BleedWork bleedWork = null;
					int num3 = 1;
					int index = 0;
					int k = 0;
					for (int count2 = region_work.bleedWorkList.Count; k < count2; k++)
					{
						if (region_work.bleedWorkList[k].ownerID == status.fromObjectID)
						{
							bleedWork = region_work.bleedWorkList[k];
							break;
						}
						if (num3 == region_work.bleedWorkList[k].showIndex)
						{
							num3++;
							index = k + 1;
						}
					}
					if (bleedWork == null)
					{
						bleedWork = new BleedWork();
						if (bleedData.IsOwnerSelf())
						{
							region_work.bleedWorkList.Insert(0, bleedWork);
							bleedWork.showIndex = 0;
						}
						else
						{
							region_work.bleedWorkList.Insert(index, bleedWork);
							bleedWork.showIndex = num3;
						}
						bleedWork.ownerID = status.fromObjectID;
					}
					if (bleedWork.bleedEffect == null)
					{
						string effect_name = specialActionInfo.arrowBleedEffectName;
						if (bleedWork.showIndex != 0)
						{
							effect_name = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.arrowBleedOtherEffectName;
						}
						bleedWork.bleedEffect = targetPoint.PlayArrowBleedEffect(effect_name, bleedWork.showIndex);
					}
					else if (bleedWork.showIndex == 0)
					{
						Animator component2 = bleedWork.bleedEffect.GetComponent<Animator>();
						if (null != component2)
						{
							component2.Play("ACT" + (bleedData.lv - 1).ToString());
						}
					}
					if (bleedData.IsMaxLv())
					{
						string burstEffectName = specialActionInfo.GetBurstEffectName(status.damageDetails.GetElementType());
						Transform trs = null;
						if (bleedWork != null)
						{
							trs = bleedWork.bleedEffect;
						}
						targetPoint.PlayArrowBurstEffect(burstEffectName, trs);
						int num4 = 2;
						if (status.damageDetails.GetElementType() != ELEMENT_TYPE.MAX)
						{
							num4++;
						}
						MonoBehaviourSingleton<UIDamageManager>.I.Create(status.hitPos, status.arrowBurstDamage, UIDamageNum.DAMAGE_COLOR.NONE, num4, 0, status.isDamageRegionOnly);
					}
				}
				if ((IsMirror() || IsPuppet()) && !IsValidWaitingPacket(WAITING_PACKET.ENEMY_UPDATE_BLEED_DAMAGE))
				{
					float arrowBleedTimeInterval = specialActionInfo.arrowBleedTimeInterval;
					StartWaitingPacket(WAITING_PACKET.ENEMY_UPDATE_BLEED_DAMAGE, false, arrowBleedTimeInterval * 2f);
				}
			}
		}
	}

	public override void ActReaction(ReactionInfo info, bool isSync = false)
	{
		base.ActReaction(info, false);
		switch (info.reactionType)
		{
		case REACTION_TYPE.DEAD:
		case REACTION_TYPE.GUARD_DAMAGE:
		case REACTION_TYPE.PARALYZE:
		case REACTION_TYPE.ANGRY:
		case REACTION_TYPE.FREEZE:
		case REACTION_TYPE.INK_SPLASH:
			break;
		case REACTION_TYPE.DOWN:
			ActDown();
			break;
		case REACTION_TYPE.COUNTER:
			ActCounter(info.targetId);
			break;
		case REACTION_TYPE.ELECTRIC_SHOCK:
			ActElectricShock();
			break;
		case REACTION_TYPE.DIZZY:
			ActDizzy();
			break;
		case REACTION_TYPE.SHADOWSEALING:
			ActDebuffShadowSealingStart();
			break;
		case REACTION_TYPE.MAD_MODE:
			ActMadMode();
			break;
		case REACTION_TYPE.LIGHT_RING:
			ActLightRing();
			break;
		}
	}

	public override void OnReactionDelay(List<DelayReactionInfo> reactionDelayList)
	{
		if (m_reactionDelayList.Count >= 2)
		{
			DelayReactionInfo delayReactionInfo = SearchReactionDelayInfo(REACTION_TYPE.COUNTER);
			if (delayReactionInfo != null)
			{
				m_reactionDelayList.Remove(delayReactionInfo);
			}
		}
		base.OnReactionDelay(reactionDelayList);
		int count = reactionDelayList.Count;
		if (count > 0)
		{
			for (int i = 0; i < count; i++)
			{
				DelayReactionInfo delayReactionInfo2 = reactionDelayList[i];
				switch (delayReactionInfo2.type)
				{
				case REACTION_TYPE.DOWN:
					ActDown();
					break;
				case REACTION_TYPE.COUNTER:
					ActCounter(delayReactionInfo2.targetId);
					break;
				case REACTION_TYPE.DIZZY:
					ActDizzy();
					break;
				case REACTION_TYPE.SHADOWSEALING:
					ActDebuffShadowSealingStart();
					break;
				case REACTION_TYPE.MAD_MODE:
					ActMadMode();
					break;
				case REACTION_TYPE.LIGHT_RING:
					ActLightRing();
					break;
				}
			}
		}
	}

	private int GetRegionID(Collider collider, List<int> target_region_ids)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		int result = 0;
		if (collider != null)
		{
			RegionRoot componentInParent = collider.get_gameObject().GetComponentInParent<RegionRoot>();
			if (componentInParent != null)
			{
				for (int num = componentInParent.regionIDArray.Length - 1; num >= 0; num--)
				{
					int num2 = componentInParent.regionIDArray[num];
					if (num2 >= 0 && num2 < regionWorks.Length)
					{
						EnemyRegionWork enemyRegionWork = regionWorks[num2];
						RegionInfo regionInfo = regionInfos[num2];
						while (!enemyRegionWork.enabled && enemyRegionWork.parentRegionID >= 0)
						{
							num2 = enemyRegionWork.parentRegionID;
							enemyRegionWork = regionWorks[num2];
							regionInfo = regionInfos[num2];
						}
						if (num == 0 || (target_region_ids != null && target_region_ids.Contains(num2)))
						{
							bool flag = false;
							if ((int)enemyRegionWork.hp <= 0 && regionInfo.maxHP > 0)
							{
								flag = true;
							}
							if (regionInfo.breakAfterHit || !flag)
							{
								result = num2;
								break;
							}
						}
					}
				}
			}
		}
		return result;
	}

	public int GetEnabledCounterRegionIndex()
	{
		for (int i = 0; i < regionWorks.Length; i++)
		{
			EnemyRegionWork enemyRegionWork = regionWorks[i];
			if (enemyRegionWork != null)
			{
				RegionInfo regionInfo = regionInfos[i];
				if (regionInfo != null && !enemyRegionWork.isBroke && regionInfo.counterInfo.enabled)
				{
					return i;
				}
			}
		}
		return -1;
	}

	public EnemyRegionWork GetEnabledCounterRegion()
	{
		int enabledCounterRegionIndex = GetEnabledCounterRegionIndex();
		if (enabledCounterRegionIndex < 0)
		{
			return null;
		}
		return regionWorks[enabledCounterRegionIndex];
	}

	public EnemyRegionWork SearchRegionWork(int regionId)
	{
		if (regionId < 0 || regionId >= regionWorks.Length)
		{
			return null;
		}
		return regionWorks[regionId];
	}

	public int GetRegionID(string name)
	{
		int i = 0;
		for (int num = regionInfos.Length; i < num; i++)
		{
			if (regionInfos[i].name == name)
			{
				return i;
			}
		}
		return 0;
	}

	public List<int> GetBreakRegionIDList()
	{
		List<int> list = new List<int>();
		list.Add(0);
		if (regionInfos == null)
		{
			return list;
		}
		int i = 1;
		for (int num = regionInfos.Length; i < num; i++)
		{
			EnemyRegionWork enemyRegionWork = regionWorks[i];
			if (enemyRegionWork.isBroke)
			{
				list.Add(i);
			}
		}
		return list;
	}

	public void SetAttackInfos(AttackInfo[] attack_infos)
	{
		attackInfos = attack_infos;
	}

	protected void PlayRegionEffect(int region_id, string effect_name)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		if (targetPoints != null)
		{
			Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
			Quaternion rotation = cameraTransform.get_rotation();
			Vector3 position = cameraTransform.get_position();
			int i = 0;
			for (int num = targetPoints.Length; i < num; i++)
			{
				TargetPoint targetPoint = targetPoints[i];
				if (targetPoint.regionID == region_id && targetPoint.isTargetEnable)
				{
					Vector3 position2 = targetPoint._transform.get_position();
					position2 += targetPoint._transform.get_rotation() * targetPoint.scaledOffset;
					Vector3 val = position - position2;
					Vector3 pos = val.get_normalized() * targetPoint.scaledMarkerZShift + position2;
					Quaternion rot = rotation;
					EffectManager.OneShot(effect_name, pos, rot, true);
				}
			}
		}
	}

	private void CreateDropItem(Vector3 pos, StageObject target, int rarity, bool is_region_break)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (target is Self)
		{
			DropObject.Create(rarity, is_region_break, pos);
		}
	}

	private void CreateDropItemFromRegionBreak(RegionInfo region_info, StageObject target, int rarity)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		if (region_info.breakDrop != null)
		{
			Transform val = FindNode(region_info.breakDrop.dropNodeName);
			if (!(val == null))
			{
				Vector3 position = val.get_position();
				CreateDropItem(position, target, rarity, true);
			}
		}
	}

	private void CreateDamageNum(Vector3 pos, AtkAttribute damage, bool buff, bool isRegionOnly, int addGroup = 0)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		int num = addGroup;
		if (damage.normal != 0f)
		{
			MonoBehaviourSingleton<UIDamageManager>.I.Create(pos, (int)damage.normal, buff ? UIDamageNum.DAMAGE_COLOR.BUFF : UIDamageNum.DAMAGE_COLOR.NONE, num++, 0, isRegionOnly);
		}
		float num3 = damage.fire + damage.water + damage.thunder + damage.soil + damage.light + damage.dark;
		if (num3 != 0f)
		{
			UIDamageNum.DAMAGE_COLOR color = UIDamageNum.DAMAGE_COLOR.NONE;
			switch (damage.GetElementType())
			{
			case ELEMENT_TYPE.FIRE:
				color = UIDamageNum.DAMAGE_COLOR.FIRE;
				if (IsActDown())
				{
					color = UIDamageNum.DAMAGE_COLOR.GOOD;
				}
				break;
			case ELEMENT_TYPE.WATER:
				color = UIDamageNum.DAMAGE_COLOR.WATER;
				if (IsActDown())
				{
					color = UIDamageNum.DAMAGE_COLOR.GOOD;
				}
				break;
			case ELEMENT_TYPE.THUNDER:
				color = UIDamageNum.DAMAGE_COLOR.THUNDER;
				if (IsActDown())
				{
					color = UIDamageNum.DAMAGE_COLOR.GOOD;
				}
				break;
			case ELEMENT_TYPE.SOIL:
				color = UIDamageNum.DAMAGE_COLOR.SOIL;
				if (IsActDown())
				{
					color = UIDamageNum.DAMAGE_COLOR.GOOD;
				}
				break;
			case ELEMENT_TYPE.LIGHT:
				color = UIDamageNum.DAMAGE_COLOR.LIGHT;
				if (IsActDown())
				{
					color = UIDamageNum.DAMAGE_COLOR.GOOD;
				}
				break;
			case ELEMENT_TYPE.DARK:
				color = UIDamageNum.DAMAGE_COLOR.DARK;
				if (IsActDown())
				{
					color = UIDamageNum.DAMAGE_COLOR.GOOD;
				}
				break;
			}
			ELEMENT_TYPE elementType = damage.GetElementType();
			EFFECTIVE_TYPE effectiveType = GetEffectiveType(elementType, GetElementType());
			int effective = 0;
			switch (effectiveType)
			{
			case EFFECTIVE_TYPE.GOOD:
				effective = 1;
				break;
			case EFFECTIVE_TYPE.BAD:
				effective = -1;
				break;
			}
			MonoBehaviourSingleton<UIDamageManager>.I.Create(pos, (int)num3, color, num++, effective, isRegionOnly);
		}
		if (num == 0)
		{
			MonoBehaviourSingleton<UIDamageManager>.I.Create(pos, (int)damage.normal, buff ? UIDamageNum.DAMAGE_COLOR.BUFF : UIDamageNum.DAMAGE_COLOR.NONE, num++, 0, isRegionOnly);
		}
	}

	private void PlayRegionBreakEffect(RegionInfo region_info)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		Transform val = FindNode(region_info.breakEffect.nodeName);
		if (!(val == null))
		{
			Transform effect = EffectManager.GetEffect(region_info.breakEffect.effectName, null);
			if (!(effect == null))
			{
				Transform val2 = val.get_transform();
				effect.set_position(val2.get_position());
				effect.set_rotation(val2.get_rotation() * Quaternion.Euler(region_info.breakEffect.effectAngle));
			}
		}
	}

	public void UpdateRegionVisual()
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Expected O, but got Unknown
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Expected O, but got Unknown
		int i = 0;
		for (int num = regionWorks.Length; i < num; i++)
		{
			EnemyRegionWork enemyRegionWork = regionWorks[i];
			if ((int)enemyRegionWork.hp <= 0)
			{
				RegionInfo regionInfo = regionInfos[i];
				string[] deactivateObjects = regionInfo.deactivateObjects;
				foreach (string name in deactivateObjects)
				{
					Transform val = FindNode(name);
					if (val != null)
					{
						val.get_gameObject().SetActive(false);
					}
				}
				int k = 0;
				for (int num2 = regionWorks.Length; k < num2; k++)
				{
					if (regionWorks[k].parentRegionID >= 0 && regionWorks[k].parentRegionID == i)
					{
						regionWorks[k].enabled = true;
					}
				}
			}
			int num3 = 0;
			while (num3 < enemyRegionWork.bleedWorkList.Count)
			{
				BleedWork bleedWork = enemyRegionWork.bleedWorkList[num3];
				bool flag = false;
				int l = 0;
				for (int count = enemyRegionWork.bleedList.Count; l < count; l++)
				{
					if (enemyRegionWork.bleedList[l].ownerID == bleedWork.ownerID)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					num3++;
				}
				else
				{
					if (bleedWork.bleedEffect != null)
					{
						EffectManager.ReleaseEffect(bleedWork.bleedEffect.get_gameObject(), true, false);
						bleedWork.bleedEffect = null;
					}
					enemyRegionWork.bleedWorkList.RemoveAt(num3);
				}
			}
			if (enemyRegionWork.shadowSealingData.ownerID == 0 && !object.ReferenceEquals(enemyRegionWork.shadowSealingEffect, null))
			{
				EffectManager.ReleaseEffect(enemyRegionWork.shadowSealingEffect.get_gameObject(), false, true);
				enemyRegionWork.shadowSealingEffect = null;
			}
			TargetPoint targetPoint = null;
			int m = 0;
			for (int num4 = targetPoints.Length; m < num4; m++)
			{
				if (targetPoints[m].regionID == i && targetPoints[m].isAimEnable)
				{
					targetPoint = targetPoints[m];
					break;
				}
			}
			if (targetPoint != null)
			{
				int n = 0;
				for (int count2 = enemyRegionWork.bleedList.Count; n < count2; n++)
				{
					BleedData bleedData = enemyRegionWork.bleedList[n];
					bool flag2 = true;
					if (!MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.arrowBleedOther.enable && !bleedData.IsOwnerSelf())
					{
						flag2 = false;
					}
					if (flag2)
					{
						BleedWork bleedWork2 = null;
						int num5 = 1;
						int index = 0;
						int num6 = 0;
						for (int count3 = enemyRegionWork.bleedWorkList.Count; num6 < count3; num6++)
						{
							if (enemyRegionWork.bleedWorkList[num6].ownerID == bleedData.ownerID)
							{
								bleedWork2 = enemyRegionWork.bleedWorkList[num6];
								break;
							}
							if (num5 == enemyRegionWork.bleedWorkList[num6].showIndex)
							{
								num5++;
								index = num6 + 1;
							}
						}
						if (bleedWork2 == null)
						{
							bleedWork2 = new BleedWork();
							if (bleedData.IsOwnerSelf())
							{
								enemyRegionWork.bleedWorkList.Insert(0, bleedWork2);
								bleedWork2.showIndex = 0;
							}
							else
							{
								enemyRegionWork.bleedWorkList.Insert(index, bleedWork2);
								bleedWork2.showIndex = num5;
							}
							bleedWork2.ownerID = bleedData.ownerID;
						}
						if (bleedWork2.bleedEffect == null)
						{
							string effect_name = MonoBehaviourSingleton<InGameSettingsManager>.I.player.specialActionInfo.arrowBleedEffectName;
							if (bleedWork2.showIndex != 0)
							{
								effect_name = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.arrowBleedOtherEffectName;
							}
							bleedWork2.bleedEffect = targetPoint.PlayArrowBleedEffect(effect_name, bleedWork2.showIndex);
						}
					}
				}
				if (enemyRegionWork.shadowSealingData.ownerID != 0 && object.ReferenceEquals(enemyRegionWork.shadowSealingEffect, null))
				{
					enemyRegionWork.shadowSealingEffect = targetPoint.PlayArrowBleedEffect(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.shadowSealingEffectName, 0);
				}
			}
		}
		InitializeBarrierEffect();
		UpdateBreakIDLists();
	}

	private void InitializeBarrierEffect()
	{
		List<AnimEventData.EventData> list = animEventProcessor.ListUpEventData(AnimEventFormat.ID.EFFECT_LOOP_CUSTOM);
		if (list != null && list.Count > 0)
		{
			foreach (AnimEventData.EventData item in list)
			{
				if (IsValidBarrier)
				{
					EventEffectLoopCustom(item);
				}
			}
		}
	}

	public void ReviveRegion(int region_id)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		if (region_id >= 0 && region_id < regionInfos.Length)
		{
			EnemyRegionWork enemyRegionWork = regionWorks[region_id];
			RegionInfo regionInfo = regionInfos[region_id];
			if (enemyRegionWork != null)
			{
				enemyRegionWork.hp = regionInfo.maxHP;
				string[] deactivateObjects = regionInfo.deactivateObjects;
				foreach (string name in deactivateObjects)
				{
					Transform val = FindNode(name);
					if (val != null)
					{
						val.get_gameObject().SetActive(true);
					}
				}
				int j = 0;
				for (int num = regionWorks.Length; j < num; j++)
				{
					regionWorks[j].OnReviveRegion(region_id);
				}
				EnemyController enemyController = base.controller as EnemyController;
				if (enemyController != null)
				{
					enemyController.OnReviveRegion(region_id);
				}
				reviveRegionWaitSync = false;
				if (enemySender != null)
				{
					enemySender.OnReviveRegion(region_id);
				}
				if (MonoBehaviourSingleton<TargetMarkerManager>.IsValid())
				{
					MonoBehaviourSingleton<TargetMarkerManager>.I.updateShadowSealingFlag = true;
				}
			}
		}
	}

	public bool IsEnableReviveRegion(int region_id)
	{
		if (region_id < 0 || region_id >= regionInfos.Length)
		{
			Log.Error("RegionId is out of range!!");
			return false;
		}
		EnemyRegionWork enemyRegionWork = regionWorks[region_id];
		RegionInfo regionInfo = regionInfos[region_id];
		if (regionInfo == null || enemyRegionWork == null)
		{
			return false;
		}
		if (!regionInfo.enableRevive)
		{
			return false;
		}
		if ((int)enemyRegionWork.hp > 0 || regionInfo.maxHP <= 0)
		{
			return false;
		}
		if (Time.get_time() - enemyRegionWork.breakTime < regionInfo.reviveIntervalTime)
		{
			return false;
		}
		return true;
	}

	public void ActivateRegionNode(int[] regionIDs, bool isRandom = false, int randomSelectedID = -1)
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		if (!regionIDs.IsNullOrEmpty() && !regionRoots.IsNullOrEmpty())
		{
			foreach (int num in regionIDs)
			{
				for (int j = 0; j < regionRoots.Length; j++)
				{
					if (regionRoots[j].regionID == num)
					{
						if (!isRandom)
						{
							regionRoots[j].get_gameObject().SetActive(true);
						}
						else if (regionRoots[j].regionID != randomSelectedID)
						{
							regionRoots[j].get_gameObject().SetActive(false);
						}
						else
						{
							regionRoots[j].get_gameObject().SetActive(true);
							RegionInfo regionInfo = regionInfos[randomSelectedID];
							if (regionInfo != null && regionInfo.modeChangeInfo.enabled)
							{
								EnemyBrain enemyBrain = base.controller.brain as EnemyBrain;
								if (!(enemyBrain == null))
								{
									EnemyActionController actionCtrl = enemyBrain.actionCtrl;
									if (actionCtrl != null)
									{
										actionCtrl.modeId = regionInfo.modeChangeInfo.modeID;
									}
								}
							}
						}
					}
				}
			}
			if (enemySender != null)
			{
				enemySender.OnEnemyRegionNodeActivate(regionIDs, isRandom, randomSelectedID);
			}
		}
	}

	public bool IsBleedFromSelf(int region_id)
	{
		if (region_id < 0 || region_id >= regionWorks.Length)
		{
			Log.Error("RegionId is out of range!!");
			return false;
		}
		EnemyRegionWork enemyRegionWork = regionWorks[region_id];
		if (enemyRegionWork == null)
		{
			return false;
		}
		int i = 0;
		for (int count = enemyRegionWork.bleedList.Count; i < count; i++)
		{
			if (enemyRegionWork.bleedList[i].IsOwnerSelf())
			{
				return true;
			}
		}
		return false;
	}

	public bool IsMaxLvBleedFromSelf(int region_id)
	{
		if (region_id < 0 || region_id >= regionWorks.Length)
		{
			Log.Error("RegionId is out of range!!");
			return false;
		}
		EnemyRegionWork enemyRegionWork = regionWorks[region_id];
		if (enemyRegionWork == null)
		{
			return false;
		}
		int i = 0;
		for (int count = enemyRegionWork.bleedList.Count; i < count; i++)
		{
			if (enemyRegionWork.bleedList[i].IsOwnerSelf())
			{
				return enemyRegionWork.bleedList[i].IsMaxLv();
			}
		}
		return false;
	}

	public bool IsShadowSealingStuck(int regionId)
	{
		if (IsDebuffShadowSealing())
		{
			return true;
		}
		if (regionId < 0 || regionId >= regionWorks.Length)
		{
			Log.Error("RegionId is out of range!!");
			return false;
		}
		EnemyRegionWork enemyRegionWork = regionWorks[regionId];
		if (enemyRegionWork == null)
		{
			return false;
		}
		return enemyRegionWork.shadowSealingData.ownerID != 0;
	}

	public void SetHitShock(Vector3 vec)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		if (canHitShockEffect)
		{
			hitShockOffsetFlag = true;
		}
		hitShockLightTime = 0f;
		hitShockOffsetTime = 0f;
		hitShockVec = vec;
		hitShockVec.y = 0f;
		hitShockVec.Normalize();
	}

	public void SetHitLight()
	{
		hitShockLightFlag = true;
	}

	public override string EffectNameAnalyzer(string effect_name)
	{
		if (!string.IsNullOrEmpty(effect_name) && enemyTableData != null && !string.IsNullOrEmpty(enemyTableData.effectEnemyKey))
		{
			effect_name = effect_name.Replace("[ENEMY_KEY]", enemyTableData.effectEnemyKey);
		}
		return effect_name;
	}

	private void EventReviveRegion(AnimEventData.EventData data)
	{
		int num = data.intArgs[0];
		if (num < 0 || num >= regionInfos.Length)
		{
			Log.Error("Out of region index !! id:" + num);
		}
		else if (IsCoopNone() || IsOriginal())
		{
			if (IsEnableReviveRegion(num))
			{
				ReviveRegion(num);
			}
		}
		else if (!reviveRegionWaitSync)
		{
			reviveRegionWaitSync = true;
		}
	}

	private void EventDashStart(AnimEventData.EventData data)
	{
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		if (!enableDash)
		{
			string value = (data.stringArgs.Length <= 0) ? null : data.stringArgs[0];
			float num = (data.floatArgs.Length <= 0) ? 0f : data.floatArgs[0];
			float num2 = (data.floatArgs.Length <= 1) ? 0f : data.floatArgs[1];
			wallStayTimer = 0f;
			enableDash = true;
			dashBeforePos = _position;
			dashNowDistance = 0f;
			dashOverDistance = num;
			dashMinDistance = num2;
			dashOverFlag = false;
			dashOverCheckDistance = 0f;
			if (string.IsNullOrEmpty(value))
			{
				dashEndTrigger = "next";
			}
			else
			{
				dashEndTrigger = value;
			}
			rotateSafeMode = true;
			dashMaxDistance = 0f;
			if (base.actionPositionFlag)
			{
				Vector3 val = base.actionPosition - _position;
				val.y = 0f;
				dashMaxDistance = val.get_magnitude();
			}
			if (dashMaxDistance < dashMinDistance)
			{
				dashMaxDistance = dashMinDistance;
			}
			dashMaxDistance *= enemyParameter.dashMaxDistanceRate;
		}
	}

	private void EventWarpViewStart(AnimEventData.EventData data)
	{
		float num = data.floatArgs[0];
		if (num <= 0f)
		{
			warpViewFlag = false;
			warpViewRate = 1f;
			warpViewRatePerTime = 0f;
			SetWarpVisible(warpViewRate);
		}
		else
		{
			warpViewFlag = true;
			warpViewRatePerTime = (1f - warpViewRate) / num;
		}
	}

	private void EventWarpViewEnd(AnimEventData.EventData data)
	{
		float num = data.floatArgs[0];
		if (num <= 0f)
		{
			warpViewFlag = false;
			warpViewRate = 0f;
			warpViewRatePerTime = 0f;
			SetWarpVisible(warpViewRate);
		}
		else
		{
			warpViewFlag = true;
			warpViewRatePerTime = (0f - warpViewRate) / num;
		}
	}

	private void EventWarpToTarget(AnimEventData.EventData data)
	{
		float warp_distance = data.floatArgs[0];
		SetWarpToTarget(warp_distance, false);
	}

	private void EventWarpToReverseTarget(AnimEventData.EventData data)
	{
		float warp_distance = data.floatArgs[0];
		SetWarpToTarget(warp_distance, true);
	}

	private void EventWarpToRandom(AnimEventData.EventData data)
	{
		float warpMax = data.floatArgs[0];
		float warpMin = (data.floatArgs.Length <= 1) ? 0f : data.floatArgs[1];
		SetWarpToRandom(warpMax, warpMin);
	}

	private void EventRadialBlurStart(AnimEventData.EventData data)
	{
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		float time = data.floatArgs[0];
		float strength = data.floatArgs[1];
		string text = data.stringArgs[0];
		bool flag = (data.intArgs[0] != 0) ? true : false;
		Transform val = FindNode(text);
		if (val == null)
		{
			Log.Error("Not found node for RadialBlur!! " + text);
		}
		else
		{
			if (flag)
			{
				MonoBehaviourSingleton<InGameCameraManager>.I.StartRadialBlurFilter(time, strength, val);
			}
			else
			{
				MonoBehaviourSingleton<InGameCameraManager>.I.StartRadialBlurFilter(time, strength, val.get_position());
			}
			radialBlurEnable = true;
		}
	}

	private void EventRadialBlurChange(AnimEventData.EventData data)
	{
		float time = data.floatArgs[0];
		float num = data.floatArgs[1];
		if (num <= 0f)
		{
			MonoBehaviourSingleton<InGameCameraManager>.I.EndRadialBlurFilter(time);
			radialBlurEnable = false;
		}
		else
		{
			MonoBehaviourSingleton<InGameCameraManager>.I.ChangeRadialBlurFilter(time, num);
		}
	}

	private void EventRadialBlurEnd(AnimEventData.EventData data)
	{
		float time = data.floatArgs[0];
		MonoBehaviourSingleton<InGameCameraManager>.I.EndRadialBlurFilter(time);
		radialBlurEnable = false;
	}

	private void EventShotTarget(AnimEventData.EventData data)
	{
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		AttackInfo attackInfo = FindAttackInfo(data.stringArgs[0], true, false);
		if (attackInfo == null)
		{
			Log.Error("Not found AttackInfo !! " + data.stringArgs[0]);
		}
		else
		{
			Transform val = FindNode(data.stringArgs[1]);
			if (val == null)
			{
				Log.Error("Not found node for ShotTarget!! " + data.stringArgs[1]);
			}
			else
			{
				Vector3 val2 = default(Vector3);
				val2._002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
				Matrix4x4 localToWorldMatrix = val.get_localToWorldMatrix();
				val2 = localToWorldMatrix.MultiplyPoint3x4(val2);
				switch (data.intArgs[0])
				{
				case 0:
				{
					List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
					if (!playerList.IsNullOrEmpty())
					{
						int k = 0;
						for (int count2 = playerList.Count; k < count2; k++)
						{
							Player player2 = playerList[k] as Player;
							if (!(player2 == null) && !player2.isDead)
							{
								Vector3 position2 = player2._position;
								position2.y += 1f;
								Quaternion rot2 = Quaternion.LookRotation(position2 - val2);
								AnimEventShot animEventShot = AnimEventShot.Create(this, attackInfo, val2, rot2, null, true, null, null, null, Player.ATTACK_MODE.NONE, null, null);
								animEventShot.SetTarget(player2);
							}
						}
					}
					break;
				}
				case 1:
				{
					int num6 = data.intArgs[1];
					if (IsOriginal() || IsCoopNone())
					{
						List<RandomShotInfo.TargetInfo> list2 = new List<RandomShotInfo.TargetInfo>(num6);
						List<Player> alivePlayerList = MonoBehaviourSingleton<StageObjectManager>.I.GetAlivePlayerList();
						int count = alivePlayerList.Count;
						for (int j = 0; j < num6; j++)
						{
							int index = Random.Range(0, count);
							Player player = alivePlayerList[index];
							if (player == null)
							{
								list2.Add(new RandomShotInfo.TargetInfo(val.get_rotation(), -1));
							}
							else
							{
								Vector3 position = player._position;
								position.y += 1f;
								Quaternion rot = Quaternion.LookRotation(position - val2);
								list2.Add(new RandomShotInfo.TargetInfo(rot, player.id));
							}
						}
						TargetRandamShotEvent(list2);
					}
					SetRandomShotInfoForShotTarget(num6, val2, attackInfo, data);
					break;
				}
				case 2:
				{
					int num = data.intArgs[1];
					if (IsOriginal() || IsCoopNone())
					{
						List<RandomShotInfo.TargetInfo> list = new List<RandomShotInfo.TargetInfo>(num);
						float num2 = data.floatArgs[4];
						float num3 = data.floatArgs[5];
						for (int i = 0; i < num; i++)
						{
							float num4 = Random.Range(0f - num2, num2);
							float num5 = Random.Range(0f - num3, num3);
							Quaternion val3 = Quaternion.Euler(new Vector3(num4, num5, 0f));
							val3 *= val.get_rotation();
							list.Add(new RandomShotInfo.TargetInfo(val3, -1));
						}
						TargetRandamShotEvent(list);
					}
					SetRandomShotInfoForShotTarget(num, val2, attackInfo, data);
					break;
				}
				}
			}
		}
	}

	private void SetRandomShotInfoForShotTarget(int numBullet, Vector3 pos, AttackInfo info, AnimEventData.EventData data)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		RandomShotInfo randomShotInfo = null;
		bool flag = false;
		if (shotNetworkInfoQueue.Count > 0)
		{
			randomShotInfo = shotNetworkInfoQueue[0];
			shotNetworkInfoQueue.Remove(randomShotInfo);
			flag = true;
		}
		else
		{
			randomShotInfo = new RandomShotInfo();
		}
		randomShotInfo.atkInfo = info;
		for (int i = 0; i < numBullet; i++)
		{
			randomShotInfo.points[i] = pos;
		}
		randomShotInfo.shotCount = 0;
		randomShotInfo.countTime = 0f;
		randomShotInfo.interval = data.floatArgs[3];
		if (flag)
		{
			this.randomShotInfo.Add(randomShotInfo);
		}
		else
		{
			shotEventInfoQueue.Add(randomShotInfo);
		}
	}

	private void EventShotPoint(AnimEventData.EventData data)
	{
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		AttackInfo attackInfo = FindAttackInfo(data.stringArgs[0], true, false);
		if (attackInfo == null)
		{
			Log.Error("Not found AttackInfo !! " + data.stringArgs[0]);
		}
		else
		{
			bool flag = data.intArgs.Length > 2;
			Vector3 val = default(Vector3);
			val._002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
			switch (data.intArgs[0])
			{
			case 2:
				break;
			case 0:
			{
				List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
				if (!playerList.IsNullOrEmpty())
				{
					int k = 0;
					for (int count2 = playerList.Count; k < count2; k++)
					{
						Player player2 = playerList[k] as Player;
						if (!(player2 == null) && !player2.isDead)
						{
							Matrix4x4 localToWorldMatrix3 = player2._transform.get_localToWorldMatrix();
							Vector3 pos3 = localToWorldMatrix3.MultiplyPoint3x4(val);
							if (flag && data.intArgs[2] != 0)
							{
								pos3.y = data.floatArgs[1];
							}
							Quaternion rot = _rotation * Quaternion.Euler(new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]));
							AnimEventShot animEventShot = AnimEventShot.Create(this, attackInfo, pos3, rot, null, true, null, null, null, Player.ATTACK_MODE.NONE, null, null);
							animEventShot.SetTarget(player2);
						}
					}
				}
				break;
			}
			case 1:
			{
				int num2 = data.intArgs[1];
				if (IsOriginal() || IsCoopNone())
				{
					List<Vector3> list2 = new List<Vector3>(num2);
					List<Player> alivePlayerList = MonoBehaviourSingleton<StageObjectManager>.I.GetAlivePlayerList();
					int count = alivePlayerList.Count;
					for (int j = 0; j < num2; j++)
					{
						int index = Random.Range(0, count);
						Player player = alivePlayerList[index];
						if (!(player == null))
						{
							Matrix4x4 localToWorldMatrix2 = player._transform.get_localToWorldMatrix();
							Vector3 pos2 = localToWorldMatrix2.MultiplyPoint3x4(val);
							pos2 = CreateShotPoint(pos2, player._transform, flag, data);
							list2.Add(pos2);
						}
					}
					PointRandamShotEvent(list2);
				}
				SetRandomShotInfo(num2, attackInfo, data);
				break;
			}
			case 3:
			{
				int num = data.intArgs[1];
				if (IsOriginal() || IsCoopNone())
				{
					List<Vector3> list = new List<Vector3>(num);
					for (int i = 0; i < num; i++)
					{
						if (!(base.actionTarget == null))
						{
							Matrix4x4 localToWorldMatrix = base.actionTarget._transform.get_localToWorldMatrix();
							Vector3 pos = localToWorldMatrix.MultiplyPoint3x4(val);
							pos = CreateShotPoint(pos, base.actionTarget._transform, flag, data);
							list.Add(pos);
						}
					}
					PointRandamShotEvent(list);
				}
				SetRandomShotInfo(num, attackInfo, data);
				break;
			}
			}
		}
	}

	private Vector3 CreateShotPoint(Vector3 pos, Transform targetTrans, bool isFixedY, AnimEventData.EventData data)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		if (isFixedY && data.intArgs[2] != 0)
		{
			pos.y = data.floatArgs[1];
		}
		float num = (float)Random.Range(-180, 180);
		Quaternion val = Quaternion.Euler(new Vector3(0f, num, 0f));
		pos += val * targetTrans.get_forward() * Random.Range(0f, data.floatArgs[8]);
		pos += targetTrans.get_up() * Random.Range(0f - data.floatArgs[7], data.floatArgs[7]);
		return pos;
	}

	private void SetRandomShotInfo(int numBullet, AttackInfo info, AnimEventData.EventData data)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		Quaternion rot = _rotation * Quaternion.Euler(new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]));
		RandomShotInfo randomShotInfo = null;
		bool flag = false;
		if (shotNetworkInfoQueue.Count > 0)
		{
			randomShotInfo = shotNetworkInfoQueue[0];
			shotNetworkInfoQueue.Remove(randomShotInfo);
			flag = true;
		}
		else
		{
			randomShotInfo = new RandomShotInfo();
		}
		randomShotInfo.targets = new List<RandomShotInfo.TargetInfo>(numBullet);
		randomShotInfo.atkInfo = info;
		for (int i = 0; i < numBullet; i++)
		{
			randomShotInfo.targets.Add(new RandomShotInfo.TargetInfo(rot, -1));
		}
		randomShotInfo.shotCount = 0;
		randomShotInfo.countTime = 0f;
		randomShotInfo.interval = data.floatArgs[6];
		if (flag)
		{
			this.randomShotInfo.Add(randomShotInfo);
		}
		else
		{
			shotEventInfoQueue.Add(randomShotInfo);
		}
	}

	private void EventShotWorldPoint(AnimEventData.EventData data)
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		AttackInfo attackInfo = FindAttackInfo(data.stringArgs[0], true, false);
		if (attackInfo != null)
		{
			Vector3 pos = default(Vector3);
			pos._002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
			Quaternion rot = Quaternion.Euler(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
			AnimEventShot animEventShot = AnimEventShot.Create(this, attackInfo, pos, rot, null, true, null, null, null, Player.ATTACK_MODE.NONE, null, null);
		}
	}

	private void EventWeakPointON(AnimEventData.EventData data)
	{
		int num = data.intArgs[0];
		if (num < 0 || num >= regionInfos.Length)
		{
			Log.Error("Region Index is out of range!! ");
		}
		else
		{
			int weakType = -1;
			if (data.intArgs.Length > 1)
			{
				weakType = data.intArgs[1];
			}
			int validElement = -1;
			if (data.intArgs.Length > 2)
			{
				validElement = data.intArgs[2];
			}
			float displayTime = 0f;
			if (data.floatArgs.Length > 0)
			{
				displayTime = data.floatArgs[0];
			}
			string deleteAttackName = string.Empty;
			if (data.stringArgs.Length > 1)
			{
				deleteAttackName = data.stringArgs[1];
			}
			if (!regionWorks[num].IsValidDisplayTimer)
			{
				regionWorks[num].SetupWeakPoint(weakType, data.attackMode, displayTime, deleteAttackName, validElement);
			}
		}
	}

	private void EventWeakPointOFF(AnimEventData.EventData data)
	{
		int num = data.intArgs[0];
		if (num < 0 || num >= regionInfos.Length)
		{
			Log.Error("Region Index is out of range!! ");
		}
		else
		{
			EnemyRegionWork enemyRegionWork = regionWorks[num];
			if (!enemyRegionWork.IsValidDisplayTimer)
			{
				enemyRegionWork.weakState = WEAK_STATE.NONE;
			}
		}
	}

	private void EventWeakPointAllON(AnimEventData.EventData data)
	{
		int num = regionWorks.Length;
		for (int i = 0; i < num; i++)
		{
			if (!regionWorks[i].IsValidDisplayTimer)
			{
				regionWorks[i].SetupWeakPoint(data.intArgs[0], data.attackMode, 0f, string.Empty, -1);
			}
		}
	}

	private void EventWeakPointAllOFF(AnimEventData.EventData data)
	{
		int i = 0;
		for (int num = regionWorks.Length; i < num; i++)
		{
			if (!regionWorks[i].IsValidDisplayTimer)
			{
				regionWorks[i].weakState = WEAK_STATE.NONE;
			}
		}
	}

	private void EventHideBaseEffectON(AnimEventData.EventData data)
	{
		SetBaseEffecActivateFlag(false);
	}

	private void EventHideBaseEffectOFF(AnimEventData.EventData data)
	{
		SetBaseEffecActivateFlag(true);
	}

	protected override void EventNWayLaserAttack(AnimEventData.EventData data)
	{
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		string text = data.stringArgs[0];
		string text2 = data.stringArgs[1];
		int numLaser = data.intArgs[0];
		Transform val = Utility.Find(base._transform, text2);
		if (val == null)
		{
			Log.Error("Not found node!! name:" + text2);
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
				if (bulletData == null || bulletData.dataLaser == null)
				{
					Log.Error("Not found BulletData!! atkInfoName:" + text);
				}
				else
				{
					GameObject val2 = new GameObject("AttackNWayLaser");
					AttackNWayLaser attackNWayLaser = val2.AddComponent<AttackNWayLaser>();
					attackNWayLaser.Initialize(this, val, attackInfo, numLaser);
					m_activeAttackLaserList.Add(attackNWayLaser);
				}
			}
		}
	}

	private void EventObstacleNodeLinkAttack(AnimEventData.EventData data)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Expected O, but got Unknown
		string text = data.stringArgs[0];
		string text2 = data.stringArgs[1];
		Transform val = Utility.Find(base._transform, text2);
		if (val == null)
		{
			Log.Error("Not found node!! name:" + text2);
		}
		else if (val.get_gameObject().get_activeSelf())
		{
			AttackInfo attackInfo = FindAttackInfo(text, true, false);
			if (attackInfo == null)
			{
				Log.Error("Not found AttackInfo!! name:" + text);
			}
			else
			{
				AnimEventShot animEventShot = null;
				Vector3 offset = default(Vector3);
				offset._002Ector(0f, 0f, 0f);
				if (data.intArgs.Length > 1 && data.intArgs[1] != 0)
				{
					if (base.actionTarget != null)
					{
						Vector3 val2 = default(Vector3);
						val2._002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
						Quaternion val3 = Quaternion.Euler(new Vector3(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]));
						val3 = _rotation * val3;
						Matrix4x4 localToWorldMatrix = base.actionTarget._transform.get_localToWorldMatrix();
						val2 = localToWorldMatrix.MultiplyPoint3x4(val2);
						animEventShot = AnimEventShot.Create(this, attackInfo, val2, val3, null, true, null, null, null, Player.ATTACK_MODE.NONE, null, null);
					}
					else
					{
						offset.z += 2f;
					}
				}
				if (animEventShot == null)
				{
					animEventShot = AnimEventShot.Create(this, data, attackInfo, offset);
				}
				GameObject val4 = new GameObject("AttackObstacleNodeLink");
				AttackShotNodeLink attackShotNodeLink = val4.AddComponent<AttackShotNodeLink>();
				attackShotNodeLink.Initialize(this, val, data, attackInfo, animEventShot);
				m_activeAttackObstacleList.Add(attackShotNodeLink);
			}
		}
	}

	private void EventFunnelAttack(AnimEventData.EventData data)
	{
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		string text = data.stringArgs[0];
		string text2 = data.stringArgs[1];
		Transform launchTrans = FindNode(text2);
		if (launchTrans == null)
		{
			Log.Error("Not found transform for launch!! name:" + text2);
		}
		else
		{
			AttackInfo atkInfo = FindAttackInfo(text, true, false);
			if (atkInfo == null)
			{
				Log.Error("Not found AttackInfo!! name:" + text);
			}
			else
			{
				BulletData bulletData = atkInfo.bulletData;
				if (bulletData == null || bulletData.dataFunnel == null)
				{
					Log.Error("Not found BulletData!! atkInfoName:" + text);
				}
				else
				{
					Vector3 offsetPos = new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
					Quaternion offsetRot = Quaternion.Euler(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
					MonoBehaviourSingleton<StageObjectManager>.I.GetAlivePlayerList().ForEach(delegate(Player targetChar)
					{
						//IL_0005: Unknown result type (might be due to invalid IL or missing references)
						//IL_000a: Expected O, but got Unknown
						//IL_0027: Unknown result type (might be due to invalid IL or missing references)
						//IL_002d: Unknown result type (might be due to invalid IL or missing references)
						GameObject val = new GameObject("AttackFunnelBit");
						AttackFunnelBit attackFunnelBit = val.AddComponent<AttackFunnelBit>();
						attackFunnelBit.Initialize(this, atkInfo, targetChar, launchTrans, offsetPos, offsetRot);
						m_activeAttackFunnelList.Add(attackFunnelBit);
					});
				}
			}
		}
	}

	private void EventUndeadAttack(AnimEventData.EventData data)
	{
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		string text = data.stringArgs[0];
		string text2 = data.stringArgs[1];
		Transform launchTrans = FindNode(text2);
		if (launchTrans == null)
		{
			Log.Error("Not found transform for launch!! name:" + text2);
		}
		else
		{
			AttackInfo atkInfo = FindAttackInfo(text, true, false);
			if (atkInfo == null)
			{
				Log.Error("Not found AttackInfo!! name:" + text);
			}
			else
			{
				BulletData bulletData = atkInfo.bulletData;
				if (bulletData == null || bulletData.dataUndead == null)
				{
					Log.Error("Not found BulletData!! atkInfoName:" + text);
				}
				else
				{
					Vector3 offsetPos = new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
					Quaternion offsetRot = Quaternion.Euler(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
					MonoBehaviourSingleton<StageObjectManager>.I.GetAlivePlayerList().ForEach(delegate(Player targetChar)
					{
						//IL_0005: Unknown result type (might be due to invalid IL or missing references)
						//IL_000a: Expected O, but got Unknown
						//IL_0027: Unknown result type (might be due to invalid IL or missing references)
						//IL_002d: Unknown result type (might be due to invalid IL or missing references)
						GameObject val = new GameObject("AttackUndead");
						AttackUndead attackUndead = val.AddComponent<AttackUndead>();
						attackUndead.Initialize(this, atkInfo, targetChar, launchTrans, offsetPos, offsetRot);
					});
				}
			}
		}
	}

	private void EventDigAttack(AnimEventData.EventData data)
	{
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		string text = data.stringArgs[0];
		string text2 = data.stringArgs[1];
		Transform launchTrans = FindNode(text2);
		if (launchTrans == null)
		{
			Log.Error("Not found transform for launch!! name:" + text2);
		}
		else
		{
			AttackInfo atkInfo = FindAttackInfo(text, true, false);
			if (atkInfo == null)
			{
				Log.Error("Not found AttackInfo!! name:" + text);
			}
			else
			{
				BulletData bulletData = atkInfo.bulletData;
				if (bulletData == null || bulletData.dataDig == null)
				{
					Log.Error("Not found BulletData!! atkInfoName:" + text);
				}
				else
				{
					Vector3 offsetPos = new Vector3(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
					Quaternion offsetRot = Quaternion.Euler(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
					MonoBehaviourSingleton<StageObjectManager>.I.playerList.ForEach(delegate(StageObject targetChar)
					{
						//IL_0005: Unknown result type (might be due to invalid IL or missing references)
						//IL_000a: Expected O, but got Unknown
						//IL_0027: Unknown result type (might be due to invalid IL or missing references)
						//IL_002d: Unknown result type (might be due to invalid IL or missing references)
						GameObject val = new GameObject("AttackDig");
						AttackDig attackDig = val.AddComponent<AttackDig>();
						attackDig.Initialize(this, atkInfo, targetChar, launchTrans, offsetPos, offsetRot);
						m_activeAttackDigList.Add(attackDig);
					});
				}
			}
		}
	}

	private void EventCancelAction(AnimEventData.EventData data)
	{
		if ((IsCoopNone() || IsOriginal()) && !base.isDead)
		{
			CANCEL_CONDITION cANCEL_CONDITION = (CANCEL_CONDITION)data.intArgs[0];
			if (cANCEL_CONDITION != 0)
			{
				float transitionTime = data.floatArgs[0];
				bool flag = false;
				CANCEL_CONDITION cANCEL_CONDITION2 = cANCEL_CONDITION;
				if (cANCEL_CONDITION2 == CANCEL_CONDITION.FAILED_GRAB)
				{
					EnemyBrain enemyBrain = base.controller.brain as EnemyBrain;
					if (!(enemyBrain == null))
					{
						GrabController grabController = enemyBrain.actionCtrl.grabController;
						if (grabController != null && !grabController.IsGrabing())
						{
							flag = true;
						}
					}
				}
				if (flag)
				{
					ActIdle(true, transitionTime);
				}
			}
		}
	}

	private void EventReleaseGrab(AnimEventData.EventData data)
	{
		ActReleaseGrabbedPlayers(false, false, true, 0f, 0f);
	}

	private void EventFloatingMineAttack(AnimEventData.EventData data)
	{
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Expected O, but got Unknown
		string text = data.stringArgs[0];
		string text2 = data.stringArgs[1];
		Transform val = FindNode(text2);
		if (val == null)
		{
			Log.Error("Not found transform for launch!! name: " + text2);
		}
		else
		{
			AttackInfo attackInfo = FindAttackInfo(text, true, false);
			if (attackInfo == null)
			{
				Log.Error("Not found AttackInfo!! name: " + text);
			}
			else
			{
				BulletData bulletData = attackInfo.bulletData;
				if (bulletData == null || bulletData.dataMine == null)
				{
					Log.Error("Not found BulletData!! atkInfoName: " + text);
				}
				else
				{
					Vector3 offsetPos = default(Vector3);
					offsetPos._002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
					Quaternion offsetRot = Quaternion.Euler(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
					AttackFloatingMine.InitParamFloatingMine initParamFloatingMine = new AttackFloatingMine.InitParamFloatingMine();
					initParamFloatingMine.attacker = this;
					initParamFloatingMine.atkInfo = attackInfo;
					initParamFloatingMine.launchTrans = val;
					initParamFloatingMine.offsetPos = offsetPos;
					initParamFloatingMine.offsetRot = offsetRot;
					GameObject val2 = new GameObject("AttackFloatingMine");
					AttackFloatingMine attackFloatingMine = val2.AddComponent<AttackFloatingMine>();
					attackFloatingMine.Initialize(initParamFloatingMine);
				}
			}
		}
	}

	protected override void EventActionMineAttack(AnimEventData.EventData data)
	{
		if (IsOriginal() || IsCoopNone())
		{
			ActCreateActionMine(data.stringArgs[0]);
		}
	}

	private void ActCreateActionMine(string atkInfoName)
	{
		int randSeed = Random.Range(-2147483648, 2147483647);
		ActCreateActionMine(atkInfoName, randSeed);
		if (enemySender != null)
		{
			enemySender.OnCreateActionMine(atkInfoName, randSeed);
		}
	}

	public void ActCreateActionMine(string atkInfoName, int randSeed)
	{
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Expected O, but got Unknown
		AttackInfo attackInfo = FindAttackInfo(atkInfoName, true, false);
		if (attackInfo == null)
		{
			Log.Error("Not found AttackInfo!! name:" + atkInfoName);
		}
		else
		{
			BulletData bulletData = attackInfo.bulletData;
			if (bulletData == null || bulletData.dataActionMine == null)
			{
				Log.Error("Not found BulletData!! atkInfoName:" + atkInfoName);
			}
			else
			{
				int settingNum = bulletData.dataActionMine.settingNum;
				float settingRadius = bulletData.dataActionMine.settingRadius;
				float centerConcentration = bulletData.dataActionMine.centerConcentration;
				float settingHeight = bulletData.dataActionMine.settingHeight;
				float settingNearLimit = bulletData.dataActionMine.settingNearLimit;
				Vector3[] randomPosition = GetRandomPosition(settingNum, this.get_transform().get_position(), settingRadius, centerConcentration, settingHeight, settingNearLimit, randSeed);
				Random random = new Random(randSeed);
				for (int i = 0; i < randomPosition.Length; i++)
				{
					AttackActionMine.InitParamActionMine initParamActionMine = new AttackActionMine.InitParamActionMine();
					initParamActionMine.attacker = this;
					initParamActionMine.atkInfo = attackInfo;
					initParamActionMine.position = randomPosition[i];
					initParamActionMine.rotation = Quaternion.LookRotation(this.get_transform().get_position() - randomPosition[i]);
					initParamActionMine.randomSeed = randSeed;
					int tmp_id = 0;
					for (int j = 0; j < 10; j++)
					{
						tmp_id = random.Next(1, 2147483647);
						if (!m_activeAttackActionMineList.Exists((AttackActionMine x) => x.objId == tmp_id))
						{
							break;
						}
					}
					initParamActionMine.id = tmp_id;
					GameObject val = new GameObject("AttackActionMine");
					AttackActionMine attackActionMine = val.AddComponent<AttackActionMine>();
					attackActionMine.Initialize(initParamActionMine);
					m_activeAttackActionMineList.Add(attackActionMine);
				}
			}
		}
	}

	private Vector3[] GetRandomPosition(int num, Vector3 center, float radius, float concentration, float height, float nearLimit, int randomSeed)
	{
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		Random random = new Random(randomSeed);
		int num2 = 50;
		List<Vector3> list = new List<Vector3>();
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				float num3 = radius * Mathf.Pow((float)random.NextDouble(), concentration);
				float num4 = 6.28f * (float)random.NextDouble();
				float num5 = num3 * Mathf.Cos(num4);
				float num6 = num3 * Mathf.Sin(num4);
				Vector3 pos = new Vector3(num5 + center.x, height, num6 + center.z);
				if (MonoBehaviourSingleton<StageManager>.I.CheckPosInside(pos) && !list.Exists((Vector3 v) => Vector3.Distance(v, pos) < nearLimit) && !m_activeAttackActionMineList.Exists((AttackActionMine m) => Vector3.Distance(m.get_transform().get_position(), pos) < nearLimit))
				{
					list.Add(pos);
					break;
				}
			}
		}
		return list.ToArray();
	}

	protected override void EventReflectBulletAttack(AnimEventData.EventData data)
	{
		if (IsOriginal() || IsCoopNone())
		{
			string atkInfoName = data.stringArgs[0];
			string nodeName = data.stringArgs[1];
			int num = Random.Range(-2147483648, 2147483647);
			ActCreateReflectBullet(atkInfoName, nodeName, -1, num);
			if (enemySender != null)
			{
				enemySender.OnReflectBulletAttack(atkInfoName, nodeName, num);
			}
		}
	}

	public void ActCreateReflectBullet(string atkInfoName, string nodeName, int objId, int seed)
	{
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		ActResetActionMineRandom(seed);
		if (objId > 0)
		{
			AttackActionMine attackActionMine = m_activeAttackActionMineList.Find((AttackActionMine x) => x.objId == objId);
			if (attackActionMine != null)
			{
				attackActionMine.CreateReflectBullet();
			}
		}
		else if (!string.IsNullOrEmpty(atkInfoName))
		{
			AttackInfo attackInfo = FindAttackInfo(atkInfoName, true, false);
			if (attackInfo == null)
			{
				Log.Error("Not found AttackInfo!! name:" + atkInfoName);
			}
			else
			{
				Transform val = (!string.IsNullOrEmpty(nodeName)) ? ((object)FindNode(nodeName)) : ((object)base._transform);
				if (val == null)
				{
					Log.Error("Not found transform for launch!! name:" + nodeName);
				}
				else
				{
					Vector3 position = val.get_position();
					Quaternion reflectBulletRotation = GetReflectBulletRotation(position, seed);
					AnimEventShot animEventShot = AnimEventShot.Create(this, attackInfo, position, reflectBulletRotation, null, true, null, null, null, Player.ATTACK_MODE.NONE, null, null);
					animEventShot.get_gameObject().AddComponent<AttackActionMine.ReflectBulletCondition>();
				}
			}
		}
	}

	private Quaternion GetReflectBulletRotation(Vector3 nodePos, int randSeed)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		m_activeAttackActionMineList.RemoveAll((AttackActionMine x) => x == null);
		AttackActionMine[] array = m_activeAttackActionMineList.ToArray();
		Random random = new Random(randSeed);
		if (array.Length > 0)
		{
			Vector3 position = array[random.Next(0, m_activeAttackActionMineList.Count)].get_transform().get_position();
			return Quaternion.LookRotation(position - nodePos);
		}
		List<StageObject> list = new List<StageObject>(MonoBehaviourSingleton<StageObjectManager>.I.playerList);
		list.RemoveAll(delegate(StageObject obj)
		{
			Player player = obj as Player;
			if (player != null)
			{
				return player.hp <= 0;
			}
			return false;
		});
		list.Sort((StageObject a, StageObject b) => a.id - b.id);
		if (list.Count > 0)
		{
			Vector3 position2 = list[random.Next(0, list.Count)]._position;
			return Quaternion.LookRotation(position2 - nodePos);
		}
		return _rotation;
	}

	public void ActResetActionMineRandom(int seed)
	{
		for (int i = 0; i < m_activeAttackActionMineList.Count; i++)
		{
			m_activeAttackActionMineList[i].ResetRandomSeed(seed + i);
		}
	}

	private void EventWeatherChange(AnimEventData.EventData data)
	{
		if (data.floatArgs.Length >= 2)
		{
			MonoBehaviourSingleton<SceneSettingsManager>.I.ChangeWeather(data.floatArgs[0], data.floatArgs[1]);
		}
	}

	private void EventRecoverBarrierHp(AnimEventData.EventData data)
	{
		int num = data.intArgs[0];
		if (num < 0 || num >= regionInfos.Length)
		{
			Log.Error("Region Index is out of range!! ");
		}
	}

	private void EventRecoverBarrierHpAll(AnimEventData.EventData data)
	{
		int num = data.intArgs[0];
		float num2 = (float)num * 0.01f;
		int num3 = (int)((float)BarrierHpMax * num2);
		if (num3 > 0)
		{
			m_barrierHp = (int)m_barrierHp + num3;
			if ((int)m_barrierHp > BarrierHpMax)
			{
				m_barrierHp = BarrierHpMax;
			}
		}
	}

	private void EventEnemyRecoverHp(AnimEventData.EventData data)
	{
		int num = data.intArgs[0];
		if (num > 0)
		{
			RecoverHp(num, false);
		}
		int num2 = data.intArgs[1];
		if (num2 > 0)
		{
			float num3 = (float)num2 * 0.01f;
			int recoverValue = (int)((float)base.hpMax * num3);
			RecoverHp(recoverValue, false);
		}
	}

	private void EventShieldON(AnimEventData.EventData data)
	{
		base.ShieldHp = base.ShieldHpMax;
		List<EnemyRegionWork> list = new List<EnemyRegionWork>();
		for (int i = 0; i < regionInfos.Length; i++)
		{
			EnemyRegionWork enemyRegionWork = regionWorks[i];
			enemyRegionWork.isShieldCriticalDamage = false;
			if (enemyRegionWork.enabled && !enemyRegionWork.regionInfo.isGrabRelease && enemyRegionWork.isShieldDamage)
			{
				list.Add(regionWorks[i]);
			}
		}
		if (list.Count > 0)
		{
			int index = 0;
			if (list.Count > 1)
			{
				int seed = Random.get_seed();
				Random.set_seed(base.SyncRandomSeed);
				index = Random.Range(0, list.Count);
				Random.set_seed(seed);
			}
			if (MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
			{
				MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestAnnounce(enemyTableData.name, STRING_CATEGORY.ENEMY_SHIELD, 0u);
			}
			list[index].isShieldCriticalDamage = true;
			RequestShieldShaderEffect();
		}
	}

	public void RequestShieldShaderEffect()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(SetShieldShaderParam());
	}

	private void EventGenerateAegis(AnimEventData.EventData data)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		if (object.ReferenceEquals(aegisCtrl, null))
		{
			GameObject val = new GameObject("AegisParent");
			aegisCtrl = val.AddComponent<EnemyAegisController>();
			aegisCtrl.Init(this);
		}
		aegisCtrl.Generate(data);
	}

	public EnemyAegisController.SetupParam GetAegisSetupParam()
	{
		if (object.ReferenceEquals(aegisCtrl, null))
		{
			return null;
		}
		return aegisCtrl.GetSetupParam();
	}

	public float GetAegisPercent()
	{
		if (object.ReferenceEquals(aegisCtrl, null))
		{
			return 0f;
		}
		return aegisCtrl.GetPercent();
	}

	public void SetupAegis(EnemyAegisController.SetupParam param)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		if (!object.ReferenceEquals(param, null))
		{
			if (object.ReferenceEquals(aegisCtrl, null))
			{
				GameObject val = new GameObject("AegisParent");
				aegisCtrl = val.AddComponent<EnemyAegisController>();
				aegisCtrl.Init(this);
			}
			aegisCtrl.Setup(param, false);
		}
	}

	private void EventEffectLoopCustom(AnimEventData.EventData data)
	{
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		string str = data.stringArgs[0];
		string str2 = data.stringArgs[1];
		string text = str + str2;
		foreach (EnemyEffectObject enemyEffect in m_enemyEffectList)
		{
			if (enemyEffect.UniqueName == text)
			{
				return;
			}
		}
		Transform val = AnimEventFormat.EffectEventExec(data.id, data, base._transform, isBoss, ((Character)this).EffectNameAnalyzer, ((StageObject)this).FindNode, null);
		if (!(val == null))
		{
			int num = data.intArgs[0];
			int deleteCondition = data.intArgs[1];
			GameObject val2 = val.get_gameObject();
			EnemyEffectObject enemyEffectObject = val2.AddComponent<EnemyEffectObject>();
			enemyEffectObject.Initialize(this, regionWorks[num], deleteCondition, text);
			m_enemyEffectList.Add(enemyEffectObject);
		}
	}

	public void OnNotifyDeleteEnemyEffect(EnemyEffectObject del)
	{
		if (del != null && m_enemyEffectList.Contains(del))
		{
			m_enemyEffectList.Remove(del);
		}
	}

	public void SetResidentEffectSetting(SystemEffectSetting setting)
	{
		m_residentEffectSetting = setting;
	}

	private void EventGroupEffectON(AnimEventData.EventData data)
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Expected O, but got Unknown
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Expected O, but got Unknown
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		int num = data.intArgs[0];
		AnimEventData.ResidentEffectData[] residentEffectDataList = animEventData.residentEffectDataList;
		if (residentEffectDataList != null && residentEffectDataList.Length > 0)
		{
			AnimEventData.ResidentEffectData[] array = residentEffectDataList;
			foreach (AnimEventData.ResidentEffectData residentEffectData in array)
			{
				if (!string.IsNullOrEmpty(residentEffectData.effectName) && !string.IsNullOrEmpty(residentEffectData.linkNodeName) && residentEffectData.groupID == num)
				{
					Transform val = Utility.Find(base.body.get_transform(), residentEffectData.linkNodeName);
					if (val == null)
					{
						val = base.body.get_transform();
					}
					if (!IsExistResidentEffect(residentEffectData.UniqueName))
					{
						Transform effect = EffectManager.GetEffect(residentEffectData.effectName, val);
						if (effect != null)
						{
							Vector3 localScale = effect.get_localScale();
							effect.set_localScale(localScale * residentEffectData.scale);
							effect.set_localPosition(residentEffectData.offsetPos);
							effect.set_localRotation(Quaternion.Euler(residentEffectData.offsetRot));
							ResidentEffectObject residentEffectObject = effect.get_gameObject().AddComponent<ResidentEffectObject>();
							residentEffectObject.Initialize(residentEffectData);
							RegisterResidentEffect(residentEffectObject);
						}
					}
				}
			}
		}
		if (m_residentEffectSetting != null)
		{
			SystemEffectSetting.Data[] effectDataList = m_residentEffectSetting.effectDataList;
			if (effectDataList != null && effectDataList.Length > 0)
			{
				SystemEffectSetting.Data[] array2 = effectDataList;
				foreach (SystemEffectSetting.Data data2 in array2)
				{
					if (!string.IsNullOrEmpty(data2.effectName) && !string.IsNullOrEmpty(data2.linkNodeName) && data2.groupID == num)
					{
						Transform val2 = Utility.Find(base.body.get_transform(), data2.linkNodeName);
						if (val2 == null)
						{
							val2 = base.body.get_transform();
						}
						if (!IsExistResidentEffect(data2.UniqueName))
						{
							Transform effect2 = EffectManager.GetEffect(data2.effectName, val2);
							if (effect2 != null)
							{
								Vector3 localScale2 = effect2.get_localScale();
								effect2.set_localScale(localScale2 * data2.scale);
								effect2.set_localPosition(data2.offsetPos);
								effect2.set_localRotation(Quaternion.Euler(data2.offsetRot));
								ResidentEffectObject residentEffectObject2 = effect2.get_gameObject().AddComponent<ResidentEffectObject>();
								residentEffectObject2.Initialize(data2);
								RegisterResidentEffect(residentEffectObject2);
							}
						}
					}
				}
			}
		}
	}

	private void EventGroupEffectOFF(AnimEventData.EventData data)
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		int num = data.intArgs[0];
		List<ResidentEffectObject> list = new List<ResidentEffectObject>();
		foreach (ResidentEffectObject residentEffect in m_residentEffectList)
		{
			if (residentEffect.GroupID == num)
			{
				list.Add(residentEffect);
			}
		}
		foreach (ResidentEffectObject item in list)
		{
			EffectManager.ReleaseEffect(item.get_gameObject(), true, false);
			m_residentEffectList.Remove(item);
		}
	}

	public void RegisterResidentEffect(ResidentEffectObject effectObj)
	{
		m_residentEffectList.Add(effectObj);
	}

	private bool IsExistResidentEffect(string uniqueName)
	{
		foreach (ResidentEffectObject residentEffect in m_residentEffectList)
		{
			if (residentEffect.UniqueName == uniqueName)
			{
				return true;
			}
		}
		return false;
	}

	private void EventTailControllON(AnimEventData.EventData data)
	{
		int num = data.intArgs[0];
		if (tailController == null)
		{
			Debug.LogError((object)("Not found TailController!! ID:" + num));
		}
		else if (tailController.UniqueID == num)
		{
			tailController.SetUpdateFlag(true, true);
		}
	}

	private void EventTailControllOFF(AnimEventData.EventData data)
	{
		int @int = data.GetInt(0, 0);
		float @float = data.GetFloat(0, 0.8f);
		if (tailController == null)
		{
			Debug.LogError((object)("Not found TailController!! ID:" + @int));
		}
		else if (tailController.UniqueID == @int)
		{
			tailController.SetUpdateFlag(false, true);
			tailController.RequestLerp(@float);
		}
	}

	private unsafe void EventRegionNodeActivate(AnimEventData.EventData data)
	{
		if (data.intArgs.Length > 0 && data.stringArgs.Length > 0 && !string.IsNullOrEmpty(data.stringArgs[0]) && (IsCoopNone() || IsOriginal()))
		{
			bool flag = data.intArgs[0] == 1;
			string[] source = data.stringArgs[0].Split(':');
			if (_003C_003Ef__am_0024cacheA5 == null)
			{
				_003C_003Ef__am_0024cacheA5 = new Func<string, int>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			int[] array = source.Select<string, int>(_003C_003Ef__am_0024cacheA5).ToArray();
			int randomSelectedID = flag ? array[Random.Range(0, array.Length)] : 0;
			ActivateRegionNode(array, flag, randomSelectedID);
		}
	}

	private unsafe void EventRegionNodeDeactivate(AnimEventData.EventData data)
	{
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		if (data.stringArgs.Length > 0 && !string.IsNullOrEmpty(data.stringArgs[0]))
		{
			string[] source = data.stringArgs[0].Split(':');
			if (_003C_003Ef__am_0024cacheA6 == null)
			{
				_003C_003Ef__am_0024cacheA6 = new Func<string, int>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			int[] array = source.Select<string, int>(_003C_003Ef__am_0024cacheA6).ToArray();
			foreach (int num in array)
			{
				for (int j = 0; j < regionRoots.Length; j++)
				{
					if (regionRoots[j].regionID == num)
					{
						regionRoots[j].get_gameObject().SetActive(false);
					}
				}
			}
		}
	}

	protected override void EventCameraCutOn(AnimEventData.EventData data)
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<InGameCameraManager>.IsValid() && !FieldManager.IsValidInGameNoBoss() && data.floatArgs.Length >= 6)
		{
			Vector3 cutPos = default(Vector3);
			cutPos._002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
			Vector3 val = default(Vector3);
			val._002Ector(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
			InGameCameraManager i = MonoBehaviourSingleton<InGameCameraManager>.I;
			i.SetCutPos(cutPos);
			i.SetCutRot(Quaternion.Euler(val));
			i.SetCameraMode(InGameCameraManager.CAMERA_MODE.CUT);
		}
	}

	protected override void EventCameraCutOff()
	{
		if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameCameraManager>.I.ClearCutCameraMode();
		}
	}

	public override void OnAnimEvent(AnimEventData.EventData data)
	{
		switch (data.id)
		{
		case AnimEventFormat.ID.DAMAGE_SHAKE_ON:
			canHitShockEffect = true;
			break;
		case AnimEventFormat.ID.DAMAGE_SHAKE_OFF:
			canHitShockEffect = false;
			break;
		case AnimEventFormat.ID.GROUP_EFFECT_ON:
			EventGroupEffectON(data);
			break;
		case AnimEventFormat.ID.GROUP_EFFECT_OFF:
			EventGroupEffectOFF(data);
			break;
		case AnimEventFormat.ID.EFFECT_LOOP_CUSTOM:
			EventEffectLoopCustom(data);
			break;
		case AnimEventFormat.ID.RECOVER_BARRIER_HP:
			EventRecoverBarrierHp(data);
			break;
		case AnimEventFormat.ID.RECOVER_BARRIER_HP_ALL:
			EventRecoverBarrierHpAll(data);
			break;
		case AnimEventFormat.ID.SHIELD_ON:
			EventShieldON(data);
			break;
		case AnimEventFormat.ID.REVIVE_REGION:
			EventReviveRegion(data);
			break;
		case AnimEventFormat.ID.DASH_START:
			EventDashStart(data);
			break;
		case AnimEventFormat.ID.WARP_VIEW_START:
			EventWarpViewStart(data);
			break;
		case AnimEventFormat.ID.WARP_VIEW_END:
			EventWarpViewEnd(data);
			break;
		case AnimEventFormat.ID.WARP_TO_TARGET:
			EventWarpToTarget(data);
			break;
		case AnimEventFormat.ID.WARP_TO_REVERSE_TARGET:
			EventWarpToReverseTarget(data);
			break;
		case AnimEventFormat.ID.WARP_TO_RANDOM:
			EventWarpToRandom(data);
			break;
		case AnimEventFormat.ID.RADIAL_BLUR_START:
			if (!QuestManager.IsValidInGameWaveMatch(false))
			{
				EventRadialBlurStart(data);
			}
			break;
		case AnimEventFormat.ID.RADIAL_BLUR_CHANGE:
			if (!QuestManager.IsValidInGameWaveMatch(false))
			{
				EventRadialBlurChange(data);
			}
			break;
		case AnimEventFormat.ID.RADIAL_BLUR_END:
			if (!QuestManager.IsValidInGameWaveMatch(false))
			{
				EventRadialBlurEnd(data);
			}
			break;
		case AnimEventFormat.ID.SHOT_TARGET:
			EventShotTarget(data);
			break;
		case AnimEventFormat.ID.SHOT_POINT:
			EventShotPoint(data);
			break;
		case AnimEventFormat.ID.SHOT_WORLD_POINT:
			EventShotWorldPoint(data);
			break;
		case AnimEventFormat.ID.WEAKPOINT_ON:
			EventWeakPointON(data);
			break;
		case AnimEventFormat.ID.WEAKPOINT_OFF:
			EventWeakPointOFF(data);
			break;
		case AnimEventFormat.ID.WEAKPOINT_ALL_ON:
			EventWeakPointAllON(data);
			break;
		case AnimEventFormat.ID.WEAKPOINT_ALL_OFF:
			EventWeakPointAllOFF(data);
			break;
		case AnimEventFormat.ID.HIDE_BASE_EFFECT_ON:
			EventHideBaseEffectON(data);
			break;
		case AnimEventFormat.ID.HIDE_BASE_EFFECT_OFF:
			EventHideBaseEffectOFF(data);
			break;
		case AnimEventFormat.ID.FUNNEL_ATTACK:
			EventFunnelAttack(data);
			break;
		case AnimEventFormat.ID.CANCEL_ACTION:
			EventCancelAction(data);
			break;
		case AnimEventFormat.ID.RELEASE_GRAB:
			EventReleaseGrab(data);
			break;
		case AnimEventFormat.ID.FLOATING_MINE_ATTACK:
			EventFloatingMineAttack(data);
			break;
		case AnimEventFormat.ID.UNDEAD_ATTACK:
			EventUndeadAttack(data);
			break;
		case AnimEventFormat.ID.WEATHER_CHANGE:
			EventWeatherChange(data);
			break;
		case AnimEventFormat.ID.SHOT_RANDOM_AUTO:
			KeepRandomShot(data);
			break;
		case AnimEventFormat.ID.ICE_FLOOR_CREATE:
			EventCreateIceFloor(data);
			break;
		case AnimEventFormat.ID.DIG_ATTACK:
			EventDigAttack(data);
			break;
		case AnimEventFormat.ID.ACTION_MINE_ATTACK:
			EventActionMineAttack(data);
			break;
		case AnimEventFormat.ID.TAIL_CONTROL_ON:
			EventTailControllON(data);
			break;
		case AnimEventFormat.ID.TAIL_CONTROL_OFF:
			EventTailControllOFF(data);
			break;
		case AnimEventFormat.ID.ACTION_MODE_ID_CHANGE:
			EventActionModeIdChange(data);
			break;
		case AnimEventFormat.ID.ANIMATION_LAYER_WEIGHT:
			AnimationLayerWeightChange(data);
			break;
		case AnimEventFormat.ID.TARGET_CHANGE_HATE_RANKING:
			EventTargetChangeHateRanking(data);
			break;
		case AnimEventFormat.ID.ELEMENT_CHANGE:
			EventElementToleranceChange(data);
			break;
		case AnimEventFormat.ID.BLEND_COLOR_CHANGE:
			EventBlendColorChange(data);
			break;
		case AnimEventFormat.ID.BLEND_COLOR_ON:
			EventBlendColorEnable(data, true);
			break;
		case AnimEventFormat.ID.BLEND_COLOR_OFF:
			EventBlendColorEnable(data, false);
			break;
		case AnimEventFormat.ID.SHOT_NODE_LINK:
			EventObstacleNodeLinkAttack(data);
			break;
		case AnimEventFormat.ID.ELEMENT_ICON_CHANGE:
			EventElementIconChange(data);
			break;
		case AnimEventFormat.ID.WEAK_ELEMENT_ICON_CHANGE:
			EventWeakElementIconChange(data);
			break;
		case AnimEventFormat.ID.REGION_COLLIDER_ATK_HIT_ON:
			EventRegionColliderAtkHitOn(data);
			break;
		case AnimEventFormat.ID.REGION_COLLIDER_ATK_HIT_OFF:
			EventRegionColliderAtkHitOff(data);
			break;
		case AnimEventFormat.ID.COUNTER_ENABLED_ON:
			EventCounterEnabledOn(data);
			break;
		case AnimEventFormat.ID.COUNTER_ENABLED_OFF:
			EventCounterEnabledOff(data);
			break;
		case AnimEventFormat.ID.BUFF_CANCELLATION:
			EventBuffCancellation(data);
			break;
		case AnimEventFormat.ID.DAMAGE_TO_ENDURANCE:
			EventDamageToEndurance(data);
			break;
		case AnimEventFormat.ID.SYNC_ACTION_TARGET:
			EventSyncActionTarget(data);
			break;
		case AnimEventFormat.ID.GENERATE_AEGIS:
			EventGenerateAegis(data);
			break;
		case AnimEventFormat.ID.REGION_NODE_ACTIVATE:
			EventRegionNodeActivate(data);
			break;
		case AnimEventFormat.ID.REGION_NODE_DEACTIVATE:
			EventRegionNodeDeactivate(data);
			break;
		case AnimEventFormat.ID.ENEMY_RECOVER_HP:
			EventEnemyRecoverHp(data);
			break;
		default:
			base.OnAnimEvent(data);
			break;
		}
	}

	private void SetBaseEffecActivateFlag(bool flag)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		if (!(loader == null))
		{
			if (loader.baseEffect == null)
			{
				Log.Warning("Not found baseEffect!!");
			}
			else
			{
				loader.baseEffect.get_gameObject().SetActive(flag);
			}
		}
	}

	public void SetWarpVisible(float rate)
	{
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		if (loader.materialParamsList != null)
		{
			rate = Mathf.Clamp(rate, 0f, 1f);
			int ID_VANISH_FLAG = Shader.PropertyToID("_Vanish_flag");
			int ID_VANISH_RATE = Shader.PropertyToID("_Vanish_rate");
			loader.materialParamsList.ForEach(delegate(EnemyLoader.MaterialParams prm)
			{
				if (prm.hasVanishFlag)
				{
					float num3 = (!(rate <= 0f)) ? 1f : 0f;
					prm.material.SetFloat(ID_VANISH_FLAG, num3);
				}
				if (prm.hasVanishRate)
				{
					float num4 = 1f - rate;
					prm.material.SetFloat(ID_VANISH_RATE, num4);
				}
			});
			if (rate <= 0f)
			{
				hitOffFlag &= ~HIT_OFF_FLAG.INVICIBLE;
				enableTargetPoint = true;
				if (loader.shadow != null)
				{
					loader.shadow.get_gameObject().SetActive(true);
				}
				base._transform.GetComponentsInChildren<rymFX>(Temporary.fxList);
				int i = 0;
				for (int count = Temporary.fxList.Count; i < count; i++)
				{
					SetWarpInvisibleEffect(Temporary.fxList[i], false);
				}
				Temporary.fxList.Clear();
				int j = 0;
				for (int num = regionWorks.Length; j < num; j++)
				{
					int k = 0;
					for (int count2 = regionWorks[j].bleedWorkList.Count; k < count2; k++)
					{
						BleedWork bleedWork = regionWorks[j].bleedWorkList[k];
						if (bleedWork.bleedEffect != null)
						{
							EffectCtrl component = bleedWork.bleedEffect.GetComponent<EffectCtrl>();
							if (!object.ReferenceEquals(component, null))
							{
								component.Pause(false);
							}
						}
					}
					if (!object.ReferenceEquals(regionWorks[j].shadowSealingEffect, null))
					{
						EffectCtrl component2 = regionWorks[j].shadowSealingEffect.GetComponent<EffectCtrl>();
						if (!object.ReferenceEquals(component2, null))
						{
							component2.Pause(false);
						}
					}
				}
			}
			else
			{
				hitOffFlag |= HIT_OFF_FLAG.INVICIBLE;
				enableTargetPoint = false;
				if (loader.shadow != null)
				{
					loader.shadow.get_gameObject().SetActive(false);
				}
				base._transform.GetComponentsInChildren<rymFX>(Temporary.fxList);
				int l = 0;
				for (int count3 = Temporary.fxList.Count; l < count3; l++)
				{
					SetWarpInvisibleEffect(Temporary.fxList[l], true);
				}
				Temporary.fxList.Clear();
				int m = 0;
				for (int num2 = regionWorks.Length; m < num2; m++)
				{
					int n = 0;
					for (int count4 = regionWorks[m].bleedWorkList.Count; n < count4; n++)
					{
						BleedWork bleedWork2 = regionWorks[m].bleedWorkList[n];
						if (bleedWork2.bleedEffect != null)
						{
							EffectCtrl component3 = bleedWork2.bleedEffect.GetComponent<EffectCtrl>();
							if (!object.ReferenceEquals(component3, null))
							{
								component3.Pause(true);
							}
						}
					}
					if (!object.ReferenceEquals(regionWorks[m].shadowSealingEffect, null))
					{
						EffectCtrl component4 = regionWorks[m].shadowSealingEffect.GetComponent<EffectCtrl>();
						if (!object.ReferenceEquals(component4, null))
						{
							component4.Pause(true);
						}
					}
				}
			}
		}
	}

	private void SetWarpInvisibleEffect(rymFX rym_fx, bool invisible)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		if (!(rym_fx == null))
		{
			int num = invisible ? 1 : 0;
			if (rym_fx.InvisibleFlags != num)
			{
				rym_fx.InvisibleFlags = num;
				rym_fx.get_gameObject().GetComponentsInChildren<Renderer>(Temporary.rendererList);
				int i = 0;
				for (int count = Temporary.rendererList.Count; i < count; i++)
				{
					Temporary.rendererList[i].set_enabled(!invisible);
				}
				Temporary.rendererList.Clear();
			}
		}
	}

	public void SetWarpToTarget(float warp_distance, bool reverse = false)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		if (IsOriginal() || IsCoopNone())
		{
			Vector3 val = _position;
			if (base.actionTarget != null)
			{
				Vector3 position = _position;
				position.y = 0f;
				Vector3 targetPosition = GetTargetPosition(base.actionTarget);
				targetPosition.y = 0f;
				if (targetPosition == position)
				{
					val = position;
				}
				else
				{
					Vector3 val2 = targetPosition - position;
					float magnitude = val2.get_magnitude();
					val2 /= magnitude;
					float num = 0f;
					if (reverse)
					{
						val2 = -val2;
						num = warp_distance;
					}
					else
					{
						num = magnitude - warp_distance;
						if (num < 0f)
						{
							num = 0f;
						}
					}
					while (true)
					{
						val = position + val2 * num;
						if (num <= 0f || MonoBehaviourSingleton<StageManager>.I.CheckPosInside(val))
						{
							break;
						}
						num -= 0.5f;
						if (num < 0f)
						{
							num = 0f;
						}
					}
				}
			}
			if (_position != val)
			{
				Vector3 val3 = val - _position;
				val3.y = 0f;
				if (reverse)
				{
					val3 = -val3;
				}
				_rotation = Quaternion.LookRotation(val3);
			}
			_position = val;
			SetWarp();
		}
		else if (!warpWaitSync)
		{
			warpWaitSync = true;
			StartWaitingPacket(WAITING_PACKET.ENEMY_WARP, false, 0f);
		}
	}

	public void SetWarpToRandom(float warpMax, float warpMin)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		if (IsOriginal() || IsCoopNone())
		{
			SphereCollider val = base._collider as SphereCollider;
			if (!(val == null))
			{
				if (warpMin > warpMax)
				{
					warpMin = warpMax;
				}
				Vector3 position = _position;
				position.y = 0f;
				bool valid = false;
				Vector3 randomPosByInsideInfo = MonoBehaviourSingleton<StageManager>.I.GetRandomPosByInsideInfo(position, warpMax, warpMin, ref valid);
				if (!valid)
				{
					Log.Error(LOG.INGAME, "Enemy.SetWarpToRandom() position is failed. from_pos : {0}, warp_max : {1}, warp_min : {2}", position.ToString("F1"), warpMax, warpMin);
				}
				_position = randomPosByInsideInfo;
				if (position != randomPosByInsideInfo)
				{
					Vector3 val2 = randomPosByInsideInfo - position;
					val2.y = 0f;
					_rotation = Quaternion.LookRotation(val2);
				}
				SetWarp();
			}
		}
		else if (!warpWaitSync)
		{
			warpWaitSync = true;
			StartWaitingPacket(WAITING_PACKET.ENEMY_WARP, false, 0f);
		}
	}

	public void SetWarp()
	{
		SetNextTrigger(0);
		EndWaitingPacket(WAITING_PACKET.ENEMY_WARP);
		warpWaitSync = false;
		if (enemySender != null)
		{
			enemySender.OnSetWarp();
		}
	}

	public void TargetRandamShotEvent(List<RandomShotInfo.TargetInfo> targets)
	{
		RandomShotInfo randomShotInfo = null;
		bool flag = false;
		if (shotEventInfoQueue.Count > 0)
		{
			randomShotInfo = shotEventInfoQueue[0];
			shotEventInfoQueue.Remove(randomShotInfo);
			flag = true;
		}
		else
		{
			randomShotInfo = new RandomShotInfo();
		}
		randomShotInfo.targets = targets;
		if (flag)
		{
			this.randomShotInfo.Add(randomShotInfo);
		}
		else
		{
			shotNetworkInfoQueue.Add(randomShotInfo);
		}
		if (enemySender != null)
		{
			enemySender.TargetRandamShotEvent(targets);
		}
	}

	public void PointRandamShotEvent(List<Vector3> points)
	{
		RandomShotInfo randomShotInfo = null;
		bool flag = false;
		if (shotEventInfoQueue.Count > 0)
		{
			randomShotInfo = shotEventInfoQueue[0];
			shotEventInfoQueue.Remove(randomShotInfo);
			flag = true;
		}
		else
		{
			randomShotInfo = new RandomShotInfo();
		}
		randomShotInfo.points = points;
		if (flag)
		{
			this.randomShotInfo.Add(randomShotInfo);
		}
		else
		{
			shotNetworkInfoQueue.Add(randomShotInfo);
		}
		if (enemySender != null)
		{
			enemySender.TargetRandamShotEvent(points);
		}
	}

	private void UpdateRandomShot()
	{
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		int num = this.randomShotInfo.Count;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				RandomShotInfo randomShotInfo = this.randomShotInfo[i];
				randomShotInfo.countTime -= Time.get_deltaTime();
				if (randomShotInfo.targets.Count > randomShotInfo.shotCount)
				{
					randomShotInfo.countTime -= Time.get_deltaTime();
					if (randomShotInfo.countTime <= 0f)
					{
						AnimEventShot.Create(this, randomShotInfo.atkInfo, randomShotInfo.points[randomShotInfo.shotCount], randomShotInfo.targets[randomShotInfo.shotCount].rot, null, true, null, null, null, Player.ATTACK_MODE.NONE, null, null);
						int targetId = randomShotInfo.targets[randomShotInfo.shotCount].targetId;
						if (targetId != -1)
						{
							MonoBehaviourSingleton<StageObjectManager>.I.FindPlayer(targetId);
						}
						randomShotInfo.shotCount++;
						randomShotInfo.countTime = randomShotInfo.interval;
					}
				}
				else
				{
					this.randomShotInfo.RemoveAt(i);
					num--;
					i--;
				}
			}
		}
	}

	public override void OnFailedWaitingPacket(WAITING_PACKET type)
	{
		switch (type)
		{
		case WAITING_PACKET.ENEMY_WARP:
			ActIdle(false, -1f);
			break;
		case WAITING_PACKET.ENEMY_UPDATE_BLEED_DAMAGE:
			ClearBleedDamageAll();
			break;
		case WAITING_PACKET.ENEMY_UPDATE_SHADOWSEALING:
			ClearShadowSealingAll(true, true);
			break;
		}
		base.OnFailedWaitingPacket(type);
	}

	public override Vector3 GetTargetPosition(StageObject target)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		if (target == null)
		{
			return Vector3.get_zero();
		}
		return target.GetPredictivePosition();
	}

	public bool isValidPush()
	{
		if (base.isLoading || base.isDead || isBoss)
		{
			return false;
		}
		return true;
	}

	public override void ApplySyncPosition(Vector3 pos, float dir, bool force_sync = false)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		_rotation = Quaternion.AngleAxis(dir, Vector3.get_up());
		if (!force_sync && !isBoss)
		{
			float lesserEnemiesPositionMargin = enemyParameter.lesserEnemiesPositionMargin;
			Vector3 val = _position - pos;
			if (val.get_sqrMagnitude() < lesserEnemiesPositionMargin * lesserEnemiesPositionMargin)
			{
				return;
			}
		}
		_position = pos;
	}

	public void SetAppearPosEnemy()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		if (enemyPopIndex >= 0 && FieldManager.IsValidInGame())
		{
			FieldMapTable.EnemyPopTableData enemyPopData = Singleton<FieldMapTable>.I.GetEnemyPopData(MonoBehaviourSingleton<FieldManager>.I.currentMapID, enemyPopIndex);
			if (enemyPopData != null)
			{
				bool valid = false;
				Vector3 val = Vector3.get_zero();
				Quaternion rotation = Quaternion.get_identity();
				float num = 0f;
				if (enemyPopData.enablePopY)
				{
					onTheGround = false;
					num = enemyPopData.popY;
				}
				if (MonoBehaviourSingleton<StageManager>.I.insideColliderData != null && enemyPopData.popRadius < MonoBehaviourSingleton<StageManager>.I.insideColliderData.chipSize * 0.5f * 1.42f)
				{
					val._002Ector(enemyPopData.popX, num, enemyPopData.popZ);
					valid = MonoBehaviourSingleton<StageManager>.I.CheckPosInside(val);
				}
				else
				{
					val = MonoBehaviourSingleton<StageManager>.I.GetRandomPosByInsideInfo(new Vector3(enemyPopData.popX, num, enemyPopData.popZ), enemyPopData.popRadius, 0f, ref valid);
					if (!isHideSpawn)
					{
						rotation = ((!enemyPopData.enableRotY) ? Quaternion.AngleAxis(Random.get_value() * 360f, Vector3.get_up()) : Quaternion.AngleAxis(enemyPopData.rotY, Vector3.get_up()));
					}
				}
				if (!valid)
				{
					Log.Error(LOG.INGAME, "FieldMapEnemyPop position is failed. mapID:{0} enemyID:{1} popIndex:{2}", enemyPopData.mapID, enemyPopData.enemyID, enemyPopIndex);
				}
				if (QuestManager.IsValidInGameDefenseBattle())
				{
					Vector3 bossAppearOffsetPos = MonoBehaviourSingleton<InGameSettingsManager>.I.defenseBattleParam.bossAppearOffsetPos;
					float bossAppearAngleY = MonoBehaviourSingleton<InGameSettingsManager>.I.defenseBattleParam.bossAppearAngleY;
					val = bossAppearOffsetPos;
					rotation = Quaternion.Euler(0f, bossAppearAngleY, 0f);
				}
				walkSpeedRateFromTable = enemyPopData.GenerateWalkSpeed();
				_position = val;
				_rotation = rotation;
				SetAppearPos(val);
			}
		}
		else
		{
			SetAppearPos(Vector3.get_zero());
		}
	}

	public void SetAppearPosForce(Vector3 pos, FieldMapTable.EnemyPopTableData popData)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		if (popData != null)
		{
			walkSpeedRateFromTable = popData.GenerateWalkSpeed();
		}
		_position = pos;
		SetAppearPos(pos);
	}

	private static ELEMENT_TYPE GetWeakType(ELEMENT_TYPE type)
	{
		switch (type)
		{
		case ELEMENT_TYPE.WATER:
			return ELEMENT_TYPE.FIRE;
		case ELEMENT_TYPE.FIRE:
			return ELEMENT_TYPE.SOIL;
		case ELEMENT_TYPE.SOIL:
			return ELEMENT_TYPE.THUNDER;
		case ELEMENT_TYPE.THUNDER:
			return ELEMENT_TYPE.WATER;
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
		case ELEMENT_TYPE.WATER:
			return ELEMENT_TYPE.THUNDER;
		case ELEMENT_TYPE.FIRE:
			return ELEMENT_TYPE.WATER;
		case ELEMENT_TYPE.SOIL:
			return ELEMENT_TYPE.FIRE;
		case ELEMENT_TYPE.THUNDER:
			return ELEMENT_TYPE.SOIL;
		case ELEMENT_TYPE.LIGHT:
			return ELEMENT_TYPE.MAX;
		case ELEMENT_TYPE.DARK:
			return ELEMENT_TYPE.MAX;
		default:
			return ELEMENT_TYPE.MAX;
		}
	}

	private static EFFECTIVE_TYPE GetEffectiveType(ELEMENT_TYPE attack, ELEMENT_TYPE defense)
	{
		if (attack == ELEMENT_TYPE.MAX || defense == ELEMENT_TYPE.MAX)
		{
			return EFFECTIVE_TYPE.NORMAL;
		}
		if (defense == GetWeakType(attack))
		{
			return EFFECTIVE_TYPE.GOOD;
		}
		if (defense == GetStrongType(attack))
		{
			return EFFECTIVE_TYPE.BAD;
		}
		return EFFECTIVE_TYPE.NORMAL;
	}

	public void ActReleaseGrabbedPlayers(bool isWeakHit, bool isSpWeakhit, bool forceRelease, float angle = 0f, float power = 0f)
	{
		if (isBoss)
		{
			EnemyBrain enemyBrain = base.controller.brain as EnemyBrain;
			if (!(enemyBrain == null))
			{
				GrabController grabController = enemyBrain.actionCtrl.grabController;
				if ((grabController.releaseByWeakHit && isWeakHit) || (grabController.releaseBySpWeakHit && isSpWeakhit) || forceRelease)
				{
					enemyBrain.actionCtrl.grabController.ReleaseAll(angle, power);
					if (enemySender != null)
					{
						enemySender.OnReleaseGrabbed(angle, power);
					}
					GrabHp = 0;
				}
			}
		}
	}

	private void KeepRandomShot(AnimEventData.EventData data)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		if (IsOriginal() || IsCoopNone())
		{
			this.StartCoroutine(DoShotAutomaticRandom(data.stringArgs[0], data.floatArgs[0], data.floatArgs[1], data.floatArgs[2], data.intArgs[0]));
		}
	}

	private IEnumerator DoShotAutomaticRandom(string atkName, float interval, float duration, float range, int shotNum)
	{
		if (shotNum <= 0)
		{
			shotNum = 1;
		}
		List<Vector3> pointList = new List<Vector3>(shotNum);
		List<Quaternion> rotList = new List<Quaternion>(shotNum);
		float timer = 0f;
		float lastShotTime = 0f;
		while (timer < duration && !base.isDead)
		{
			timer += Time.get_deltaTime();
			if (timer - lastShotTime > interval)
			{
				pointList.Clear();
				for (int i = 0; i < shotNum; i++)
				{
					Vector3 offset2 = Vector3.get_forward() * Random.Range(3f, range);
					Quaternion dirRotation = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.get_up());
					offset2 = dirRotation * offset2;
					pointList.Add(offset2 + base._transform.get_position());
				}
				ActShotBullet(atkName, pointList, rotList);
				lastShotTime = timer;
			}
			yield return (object)null;
		}
	}

	public void ActShotBullet(string atkName, List<Vector3> posList, List<Quaternion> rotList)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		AttackInfo atk_info = FindAttackInfo(atkName, true, false);
		Vector3 position = base._transform.get_position();
		for (int i = 0; i < posList.Count; i++)
		{
			Vector3 pos = posList[i] - position;
			Quaternion rot = Quaternion.get_identity();
			if (i < rotList.Count)
			{
				rot = rotList[i];
			}
			AnimEventShot.Create(this, atk_info, pos, rot, null, true, null, null, null, Player.ATTACK_MODE.NONE, null, null);
		}
		if (enemySender != null)
		{
			enemySender.OnShotBullet(atkName, posList, rotList);
		}
	}

	private void EventCreateIceFloor(AnimEventData.EventData data)
	{
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		string text = data.stringArgs[0];
		BulletData.BulletIceFloor.TARGETING_TYPE tARGETING_TYPE = (BulletData.BulletIceFloor.TARGETING_TYPE)data.intArgs[0];
		List<Vector3> list = new List<Vector3>(4);
		List<Quaternion> list2 = new List<Quaternion>(4);
		AttackInfo attackInfo = FindAttackInfo(text, true, false);
		if (attackInfo == null)
		{
			Log.Error("[CREATE_ICE_FLOOR]Attack info is not found");
		}
		else
		{
			BulletData bulletData = attackInfo.bulletData;
			if (bulletData == null)
			{
				Log.Error("[CREATE_ICE_FLOOR]BulletData is not found");
			}
			else if (bulletData.dataIceFloor == null)
			{
				Log.Error("[CREATE_ICE_FLOOR]BulletData.IceFloor is NULL");
			}
			else
			{
				List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
				int count = playerList.Count;
				switch (tARGETING_TYPE)
				{
				case BulletData.BulletIceFloor.TARGETING_TYPE.NONE:
				{
					Vector3 zero = Vector3.get_zero();
					if (data.floatArgs.Length >= 3)
					{
						zero._002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
					}
					list.Add(zero);
					break;
				}
				case BulletData.BulletIceFloor.TARGETING_TYPE.NODE_OFFSET:
				{
					if (data.stringArgs.Length < 2)
					{
						Log.Error("[CREATE_ICE_FLOOR]AnimEventData Node Name is not found");
						return;
					}
					string name = data.stringArgs[1];
					Transform val = FindNode(name);
					if (val == null)
					{
						Log.Error("[CREATE_ICE_FLOOR]AnimEventData Node is not found");
						return;
					}
					Vector3 zero2 = Vector3.get_zero();
					if (data.floatArgs.Length >= 3)
					{
						zero2._002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
					}
					Vector3 item = val.get_position() + zero2;
					list.Add(item);
					break;
				}
				case BulletData.BulletIceFloor.TARGETING_TYPE.RANDOM_CHOICE:
					list.Add(playerList[Random.Range(0, count)]._position);
					break;
				case BulletData.BulletIceFloor.TARGETING_TYPE.ALL_PLAYERS:
					for (int i = 0; i < count; i++)
					{
						list.Add(playerList[i]._position);
					}
					break;
				}
				ActCreateIceFloor(bulletData, list, list2);
				if (enemySender != null)
				{
					enemySender.OnCreateIceFloor(text, list, list2);
				}
			}
		}
	}

	public void ActCreateIceFloor(string attackInfoName, List<Vector3> posList, List<Quaternion> rotList)
	{
		AttackInfo attackInfo = FindAttackInfo(attackInfoName, true, false);
		if (attackInfo != null && !(attackInfo.bulletData == null))
		{
			BulletData bulletData = attackInfo.bulletData;
			if (!(bulletData == null))
			{
				ActCreateIceFloor(bulletData, posList, rotList);
			}
		}
	}

	public void ActCreateIceFloor(BulletData bulletData, List<Vector3> posList, List<Quaternion> rotList)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		string effectName = bulletData.data.effectName;
		for (int i = 0; i < posList.Count; i++)
		{
			Transform val = Utility.CreateGameObject(bulletData.get_name(), MonoBehaviourSingleton<StageObjectManager>.I._transform, -1);
			val.set_position(posList[i]);
			val.get_gameObject().set_layer(LayerMask.NameToLayer("EnemyBullet"));
			Transform effect = EffectManager.GetEffect(effectName, val);
			if (effect != null)
			{
				effect.set_localScale(Vector3.get_one());
			}
			IceFloor iceFloor = val.get_gameObject().AddComponent<IceFloor>();
			iceFloor.duration = bulletData.dataIceFloor.duration;
			iceFloor.SetCollider(bulletData.data.radius, 2f);
			iceFloor.SetEffect(effect);
		}
	}

	public void EventActionModeIdChange(AnimEventData.EventData data)
	{
		EnemyBrain enemyBrain = base.controller.brain as EnemyBrain;
		if (!(enemyBrain == null))
		{
			EnemyActionController actionCtrl = enemyBrain.actionCtrl;
			if (actionCtrl != null)
			{
				actionCtrl.modeId = data.intArgs[0];
			}
		}
	}

	public void AnimationLayerWeightChange(AnimEventData.EventData data)
	{
		AnimationLayerWeightChangeInfo animationLayerWeightChangeInfo = new AnimationLayerWeightChangeInfo();
		Animator animator = loader.GetAnimator();
		animationLayerWeightChangeInfo.aliveFlag = true;
		animationLayerWeightChangeInfo.layerIndex = data.intArgs[0];
		animationLayerWeightChangeInfo.target = data.floatArgs[1];
		animationLayerWeightChangeInfo.weight = animator.GetLayerWeight(animationLayerWeightChangeInfo.layerIndex);
		animationLayerWeightChangeInfo.forceEndFlag = ((data.intArgs[1] != 0) ? true : false);
		if (animationLayerWeightChangeInfo.weight != animationLayerWeightChangeInfo.target)
		{
			if (animationLayerWeightChangeInfo.weight < animationLayerWeightChangeInfo.target)
			{
				animationLayerWeightChangeInfo.spd = data.floatArgs[0];
			}
			else
			{
				animationLayerWeightChangeInfo.spd = 0f - data.floatArgs[0];
			}
			if (animationLayerWeightChangeInfo.spd == 0f)
			{
				animator.SetLayerWeight(animationLayerWeightChangeInfo.layerIndex, animationLayerWeightChangeInfo.target);
				animationLayerWeightChangeInfo.aliveFlag = false;
			}
			bool flag = false;
			int i = 0;
			for (int count = animLayerWeightChangeInfo.Count; i < count; i++)
			{
				if (animLayerWeightChangeInfo[i].layerIndex == animationLayerWeightChangeInfo.layerIndex)
				{
					animLayerWeightChangeInfo[i] = animationLayerWeightChangeInfo;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				animLayerWeightChangeInfo.Add(animationLayerWeightChangeInfo);
			}
		}
	}

	public void EventTargetChangeHateRanking(AnimEventData.EventData data)
	{
		EnemyBrain enemyBrain = base.controller.brain as EnemyBrain;
		if (!(enemyBrain == null))
		{
			int num = data.intArgs[0];
			bool isIncludeDead = (data.intArgs[1] == 0) ? true : false;
			OpponentMemory.OpponentRecord[] hateRankingObjects = GetHateRankingObjects(isIncludeDead);
			if (hateRankingObjects != null)
			{
				int num2 = num - 1;
				if (num == 0)
				{
					num2 = Random.Range(0, hateRankingObjects.Length);
				}
				if (0 <= num2 && num2 < hateRankingObjects.Length)
				{
					enemyBrain.targetCtrl.SetCurrentTarget(hateRankingObjects[num2].obj);
				}
			}
		}
	}

	public void EventElementToleranceChange(AnimEventData.EventData data)
	{
		int num = data.intArgs[0];
		if (num >= regionInfos.Length)
		{
			Log.Error("Region Index is out of range!! ");
		}
		else
		{
			int scroll = data.intArgs[1];
			ProcessElementToleranceChange(num, scroll);
			if (MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
			{
				MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestAnnounce(enemyTableData.name, STRING_CATEGORY.ENEMY_REACTION, kStrIdx_EnemyReaction_ElemTolChange);
			}
			changeToleranceRegionId = num;
			changeToleranceScroll = scroll;
		}
	}

	public void ProcessElementToleranceChange(int regionIndex, int scroll)
	{
		if (scroll >= 0)
		{
			if (regionIndex >= 0)
			{
				regionInfos[regionIndex].tolerance.ChangeElementTolerance(scroll);
			}
			else
			{
				int i = 0;
				for (int num = regionInfos.Length; i < num; i++)
				{
					regionInfos[i].tolerance.ChangeElementTolerance(scroll);
				}
			}
		}
	}

	public void SetBlendColor(List<BlendColorCtrl.ShaderSyncParam> list)
	{
		if (blendColorCtrl != null)
		{
			blendColorCtrl.Sync(skinnedMeshRendererList, list);
		}
	}

	public void EventBlendColorChange(AnimEventData.EventData data)
	{
		if (blendColorCtrl != null)
		{
			blendColorCtrl.Change(data, skinnedMeshRendererList);
		}
	}

	public void EventBlendColorEnable(AnimEventData.EventData data, bool isEnable)
	{
		if (blendColorCtrl != null)
		{
			blendColorCtrl.Enable(data, isEnable, skinnedMeshRendererList);
		}
	}

	public void EventElementIconChange(AnimEventData.EventData data)
	{
		ELEMENT_TYPE elementType = GetElementType();
		ELEMENT_TYPE elementIcon = (ELEMENT_TYPE)data.intArgs[0];
		MonoBehaviourSingleton<UIEnemyStatus>.I.SetElementIcon(elementIcon);
		changeElementIcon = elementIcon;
		ResetElementDebuff(elementType, changeElementIcon);
	}

	public void EventWeakElementIconChange(AnimEventData.EventData data)
	{
		ELEMENT_TYPE weakElementIcon = (ELEMENT_TYPE)data.intArgs[0];
		MonoBehaviourSingleton<UIEnemyStatus>.I.SetWeakElementIcon(weakElementIcon);
		changeWeakElementIcon = weakElementIcon;
	}

	public ELEMENT_TYPE GetElementType()
	{
		if (changeElementIcon != ELEMENT_TYPE.MAX)
		{
			return changeElementIcon;
		}
		return enemyTableData.element;
	}

	private void EventRegionColliderAtkHitOn(AnimEventData.EventData data)
	{
		int num = data.intArgs[0];
		if (num < 0 || num >= regionInfos.Length)
		{
			Log.Error("Region Index is out of range!! ");
		}
		else
		{
			regionInfos[num].isAtkColliderHit = true;
		}
	}

	private void EventRegionColliderAtkHitOff(AnimEventData.EventData data)
	{
		int num = data.intArgs[0];
		if (num < 0 || num >= regionInfos.Length)
		{
			Log.Error("Region Index is out of range!! ");
		}
		else
		{
			regionInfos[num].isAtkColliderHit = false;
		}
	}

	public void EventCounterEnabledOn(AnimEventData.EventData data)
	{
		int num = data.intArgs[0];
		if (num < 0 || num >= regionInfos.Length)
		{
			Log.Error("Region Index is out of range!! ");
		}
		else
		{
			regionInfos[num].counterInfo.enabled = true;
		}
	}

	public void EventCounterEnabledOff(AnimEventData.EventData data)
	{
		int num = data.intArgs[0];
		if (num < 0 || num >= regionInfos.Length)
		{
			Log.Error("Region Index is out of range!! ");
		}
		else
		{
			regionInfos[num].counterInfo.enabled = false;
		}
	}

	public void EventBuffCancellation(AnimEventData.EventData data)
	{
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			List<StageObject> playerList = MonoBehaviourSingleton<StageObjectManager>.I.playerList;
			int i = 0;
			for (int count = playerList.Count; i < count; i++)
			{
				Player player = playerList[i] as Player;
				if (!(player == null))
				{
					player.OnBuffCancellation();
				}
			}
			if (MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid() && !MonoBehaviourSingleton<StageObjectManager>.I.self.IsInBarrier())
			{
				MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestAnnounce(string.Empty, STRING_CATEGORY.ENEMY_REACTION, kStrIdx_EnemyReaction_BuffCancellation);
			}
		}
	}

	private void EventDamageToEndurance(AnimEventData.EventData data)
	{
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			int damage = data.intArgs[0];
			MonoBehaviourSingleton<InGameProgress>.I.DamageToEndurance(damage);
		}
	}

	private void EventSyncActionTarget(AnimEventData.EventData data)
	{
		if (enemySender != null)
		{
			enemySender.OnEnemySyncTarget(base.actionTarget);
		}
	}

	protected override void EventCameraTargetOffsetOn(AnimEventData.EventData data)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			float[] floatArgs = data.floatArgs;
			InGameCameraManager.TargetOffset targetOffset = new InGameCameraManager.TargetOffset();
			targetOffset.pos = new Vector3(floatArgs[0], floatArgs[1], floatArgs[2]);
			targetOffset.rot = new Vector3(floatArgs[3], floatArgs[4], floatArgs[5]);
			MonoBehaviourSingleton<InGameCameraManager>.I.SetAnimEventTargetOffsetByEnemy(targetOffset);
		}
	}

	protected override void EventCameraTargetOffsetOff()
	{
		if (MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			MonoBehaviourSingleton<InGameCameraManager>.I.ClearAnimEventTargetOffsetByEnemy();
		}
	}

	protected override void UpdateAction()
	{
		base.UpdateAction();
		switch (base.actionID)
		{
		case (ACTION_ID)17:
			if (m_dizzyTime - Time.get_time() <= 0f)
			{
				SetNextTrigger(0);
			}
			if (IsPlayingMotion(1, true))
			{
				OnPlayingEndMotion();
			}
			break;
		case (ACTION_ID)18:
			UpdateDebuffShadowSealingAction();
			break;
		case (ACTION_ID)13:
			UpdateDownAction();
			break;
		case (ACTION_ID)21:
			UpdateLightRingAction();
			break;
		}
	}

	public bool HasValidTargetPoint()
	{
		if (base.isDead || !enableTargetPoint || targetPoints == null || targetPoints.Length <= 0 || isHiding)
		{
			return false;
		}
		return true;
	}

	public Coop_Model_EnemyInitialize CreateBackup()
	{
		Coop_Model_EnemyInitialize coop_Model_EnemyInitialize = new Coop_Model_EnemyInitialize();
		enemySender.SetupEnemyInitializeModel(coop_Model_EnemyInitialize, false, false);
		return coop_Model_EnemyInitialize;
	}

	public void InitHide()
	{
		if (isHiding)
		{
			PlayMotion(11, 0f);
			base.actionID = ACTION_ID.HIDE;
			SetColliderStatuses(false);
			viewData = Singleton<FieldMapTable>.I.GetGatherPointViewData(gatherPointViewId);
			if (viewData == null)
			{
				Log.Error(LOG.INGAME, "Invalid GatherPointViewID enemyId:" + enemyID + " ViewID:" + gatherPointViewId);
			}
			else if (!string.IsNullOrEmpty(viewData.gatherEffectName) && gatherEffect == null)
			{
				gatherEffect = EffectManager.GetEffect(viewData.gatherEffectName, base._transform);
			}
		}
	}

	public void TurnUp()
	{
		SetNextTrigger(0);
		_TurnUp();
	}

	public void TurnUpImmediate()
	{
		ActIdle(false, 0f);
		_TurnUp();
	}

	private void _TurnUp()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Expected O, but got Unknown
		isHiding = false;
		enemySender.OnTurnUp();
		SetColliderStatuses(true);
		if (targetEffect != null)
		{
			EffectManager.ReleaseEffect(targetEffect.get_gameObject(), true, false);
		}
		if (gatherEffect != null)
		{
			EffectManager.ReleaseEffect(gatherEffect.get_gameObject(), true, false);
		}
	}

	public void UpdateGatherTargetMarker(bool isNear)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Expected O, but got Unknown
		if (isHiding)
		{
			Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
			float num = 0f;
			if (self != null)
			{
				num = Vector3.Distance(base._transform.get_position(), self._position);
			}
			if (self != null && num <= viewData.targetRadius)
			{
				if (targetEffect == null && !string.IsNullOrEmpty(viewData.targetEffectName))
				{
					targetEffect = EffectManager.GetEffect(viewData.targetEffectName, base._transform);
				}
				if (targetEffect != null)
				{
					Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
					Vector3 position = cameraTransform.get_position();
					Quaternion rotation = cameraTransform.get_rotation();
					Vector3 val = position - base._transform.get_position();
					Vector3 pos = val.get_normalized() * viewData.targetEffectShift + Vector3.get_up() * viewData.targetEffectHeight + base._transform.get_position();
					targetEffect.Set(pos, rotation);
				}
			}
			else if (targetEffect != null)
			{
				EffectManager.ReleaseEffect(targetEffect.get_gameObject(), true, false);
			}
		}
	}

	private void SetColliderStatuses(bool enabled)
	{
		base._collider.set_enabled(enabled);
		if (colliders != null)
		{
			for (int i = 0; i < colliders.Length; i++)
			{
				colliders[i].set_enabled(enabled);
			}
		}
	}

	public override void SafeActIdle()
	{
		if (!isHiding)
		{
			base.SafeActIdle();
		}
	}

	public bool IsHideMotionPlaying()
	{
		return IsPlayingMotion(11, true) || IsPlayingMotion(12, true);
	}

	private bool IsCannonBallHitShieldRegion(EnemyRegionWork regionWork, AttackHitInfo atkInfo)
	{
		return IsValidShield() && regionWork.isShieldDamage && atkInfo.attackType == AttackHitInfo.ATTACK_TYPE.CANNON_BALL;
	}

	private bool CheckDisableBuffTypeByShield(BuffParam.BUFFTYPE targetType)
	{
		bool result = false;
		switch (targetType)
		{
		case BuffParam.BUFFTYPE.POISON:
		case BuffParam.BUFFTYPE.BURNING:
		case BuffParam.BUFFTYPE.DEADLY_POISON:
		case BuffParam.BUFFTYPE.ELECTRIC_SHOCK:
			return result;
		default:
			return result;
		}
	}

	private void OnBreakShield()
	{
	}

	public EnemyRegionWork SearchShieldCriticalRegionWork()
	{
		for (int i = 0; i < regionWorks.Length; i++)
		{
			if (regionWorks[i].isShieldCriticalDamage)
			{
				return regionWorks[i];
			}
		}
		return null;
	}

	private int CalcShieldDamage(bool isCritical, bool isElementCritical, AttackHitInfo atkInfo)
	{
		int toShieldAtk = atkInfo.toShieldAtk;
		float num = 1f;
		if (isCritical)
		{
			num += atkInfo.toShieldCriticalRate - 1f;
		}
		if (isElementCritical)
		{
			num += atkInfo.toShieldElementCriticalRate - 1f;
		}
		return (int)((float)toShieldAtk * num);
	}

	private IEnumerator SetShieldShaderParam()
	{
		if (base._rendererArray != null)
		{
			float duration = 1f;
			float matCapPow = 0f;
			while (duration > 0f)
			{
				duration -= Time.get_deltaTime();
				matCapPow += Time.get_deltaTime();
				if (matCapPow >= 1f)
				{
					matCapPow = 1f;
				}
				Utility.MaterialForEach(base._rendererArray, delegate(Material material)
				{
					if (material.HasProperty("_MatCapPow"))
					{
						material.SetFloat("_MatCapPow", ((_003CSetShieldShaderParam_003Ec__Iterator1D9)/*Error near IL_009b: stateMachine*/)._003CmatCapPow_003E__1);
					}
				});
				yield return (object)null;
			}
		}
	}

	private void ResetShieldShaderParam()
	{
		if (base._rendererArray != null)
		{
			Utility.MaterialForEach(base._rendererArray, delegate(Material material)
			{
				if (material.HasProperty("_MatCapPow"))
				{
					material.SetFloat("_MatCapPow", 0f);
				}
			});
		}
	}

	public TargetPoint SearchTargetPoint(int regionID)
	{
		int num = targetPoints.Length;
		for (int i = 0; i < num; i++)
		{
			if (targetPoints[i].regionID == regionID)
			{
				return targetPoints[i];
			}
		}
		return null;
	}

	public override bool IsValidLightRing()
	{
		if (GetElementType() == ELEMENT_TYPE.DARK)
		{
			return true;
		}
		return false;
	}

	public virtual void ActLightRing()
	{
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		if (!IsDebuffShadowSealing() || !shadowSealingStackDebuff.Contains((ACTION_ID)21))
		{
			ActReleaseGrabbedPlayers(false, false, true, 0f, 0f);
			if (IsDebuffShadowSealing())
			{
				if (shadowSealingStackDebuff.Contains((ACTION_ID)21))
				{
					return;
				}
				shadowSealingStackDebuff.Add((ACTION_ID)21);
			}
			else
			{
				EndAction();
				base.actionID = (ACTION_ID)21;
				PlayMotion(6, (!(stopMotionByDebuffNormalizedTime < 0f)) ? 0f : (-1f));
				if (base._rigidbody != null)
				{
					base._rigidbody.set_velocity(Vector3.get_zero());
				}
				rotateEventKeep = false;
				rotateToTargetFlag = false;
				rotateEventSpeed = 0f;
			}
			CreateLightRingEffect();
			InGameSettingsManager.LightRingParam lightRingParam = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.lightRingParam;
			if (lightRingParam.startSeId != 0)
			{
				SoundManager.PlayOneShotSE(lightRingParam.startSeId, base._transform.get_position());
			}
			if (lightRingParam.loopSeId != 0)
			{
				SoundManager.PlayLoopSE(lightRingParam.loopSeId, this, base._transform);
			}
			OnActReaction();
			lightRingTime = Time.get_time() + lightRingParam.duration;
			if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
			{
				MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
			}
		}
	}

	protected bool UpdateLightRingAction()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		if (lightRingTime - Time.get_time() <= 0f)
		{
			ActLightRingEnd();
			return true;
		}
		float num = 0.1f;
		if (stopMotionByDebuffNormalizedTime >= 0f)
		{
			num = stopMotionByDebuffNormalizedTime;
		}
		AnimatorStateInfo currentAnimatorStateInfo = base.animator.GetCurrentAnimatorStateInfo(0);
		if (currentAnimatorStateInfo.get_normalizedTime() < num || currentAnimatorStateInfo.get_fullPathHash() != Animator.StringToHash("Base Layer.damage"))
		{
			return false;
		}
		if (m_isStopMotionByDebuff)
		{
			return false;
		}
		setPause(true);
		m_isStopMotionByDebuff = true;
		return false;
	}

	protected virtual void ActLightRingEnd()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		if (IsLightRing())
		{
			badStatusMax.lightRing *= 1.5f;
			if (effectLightRing != null)
			{
				EffectManager.ReleaseEffect(effectLightRing.get_gameObject(), true, false);
				effectLightRing = null;
			}
			if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
			{
				MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
			}
			base.badStatusTotal.lightRing = 0f;
			InGameSettingsManager.LightRingParam lightRingParam = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.lightRingParam;
			if (lightRingParam.loopSeId != 0)
			{
				SoundManager.StopLoopSE(lightRingParam.loopSeId, this);
			}
			if (lightRingParam.endSeId != 0)
			{
				SoundManager.PlayOneShotSE(lightRingParam.endSeId, base._transform.get_position());
			}
			if (IsDebuffShadowSealing())
			{
				if (shadowSealingStackDebuff.Contains((ACTION_ID)21))
				{
					shadowSealingStackDebuff.Remove((ACTION_ID)21);
				}
			}
			else
			{
				setPause(false);
				m_isStopMotionByDebuff = false;
			}
		}
	}

	public override bool IsLightRing()
	{
		return effectLightRing != null;
	}

	protected void CreateLightRingEffect()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		if (object.ReferenceEquals(effectLightRing, null))
		{
			Transform effect = EffectManager.GetEffect("ef_btl_enm_bindring_01", base._transform);
			if (!(effect == null))
			{
				ParticleSystem[] componentsInChildren = effect.GetComponentsInChildren<ParticleSystem>(true);
				if (componentsInChildren != null)
				{
					CalcLightRingRadius();
					Transform obj = effect;
					obj.set_localScale(obj.get_localScale() * lightRingRadius);
					float num = lightRingHeight + lightRingHeightOffset;
					Transform obj2 = effect;
					obj2.set_localPosition(obj2.get_localPosition() + Vector3.get_up() * num);
					effectLightRing = effect;
				}
			}
		}
	}

	private void CalcLightRingRadius()
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		if (!(lightRingRadius > 0f))
		{
			lightRingRadius = 1f;
			lightRingHeight = 1f;
			if (!(base._collider == null))
			{
				float num = 1f;
				SphereCollider val = base._collider as SphereCollider;
				if (val != null)
				{
					num = val.get_radius();
				}
				CapsuleCollider val2 = base._collider as CapsuleCollider;
				if (val2 != null)
				{
					num = val2.get_radius();
				}
				float num2 = num;
				Vector3 localScale = base._transform.get_localScale();
				lightRingRadius = (lightRingHeight = num2 * localScale.x) * 0.33f;
				lightRingRadius = Mathf.Clamp(lightRingRadius, 0.5f, 1.5f);
			}
		}
	}

	public virtual void ActDebuffShadowSealingStart()
	{
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		if (!IsDebuffShadowSealing())
		{
			EndAction();
			ActReleaseGrabbedPlayers(false, false, true, 0f, 0f);
			float num = _GetShadowSealingExtendRate();
			InGameSettingsManager.ShadowSealingParam shadowSealingParam = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.shadowSealingParam;
			debuffShadowSealingTimerDuration = shadowSealingParam.duration * badStatusMax.shadowSealingBind * num;
			if (debuffShadowSealingTimerDuration < shadowSealingParam.minDuration)
			{
				debuffShadowSealingTimerDuration = shadowSealingParam.minDuration;
			}
			debuffShadowSealingTimer = debuffShadowSealingTimerDuration;
			base.actionID = (ACTION_ID)18;
			PlayMotion(6, (!(stopMotionByDebuffNormalizedTime < 0f)) ? 0f : (-1f));
			CreateShadowSealingEffect();
			if (shadowSealingParam.startSeId != 0)
			{
				SoundManager.PlayOneShotSE(shadowSealingParam.startSeId, base._transform.get_position());
			}
			if (shadowSealingParam.loopSeId != 0)
			{
				SoundManager.PlayLoopSE(shadowSealingParam.loopSeId, this, base._transform);
			}
			if (base._rigidbody != null)
			{
				base._rigidbody.set_velocity(Vector3.get_zero());
			}
			rotateEventKeep = false;
			rotateToTargetFlag = false;
			rotateEventSpeed = 0f;
			ClearShadowSealingAll(false, true);
			OnActReaction();
			if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
			{
				MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
			}
		}
	}

	private void CreateShadowSealingEffect()
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		debuffShadowSealingEffect = EffectManager.GetEffect("ef_btl_wsk_bow_01_04", base._transform);
		if (!object.ReferenceEquals(debuffShadowSealingEffect, null))
		{
			CalcShadowSealingEffectRadius();
			Transform obj = debuffShadowSealingEffect;
			obj.set_localScale(obj.get_localScale() * debuffShadowSealingRadius);
		}
	}

	private void CalcShadowSealingEffectRadius()
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		if (!(debuffShadowSealingRadius > 0f))
		{
			debuffShadowSealingRadius = 1f;
			if (!(base._collider == null))
			{
				SphereCollider val = base._collider as SphereCollider;
				if (val != null)
				{
					float num = val.get_radius() / 4f;
					Vector3 localScale = base._transform.get_localScale();
					debuffShadowSealingRadius = num * localScale.x;
				}
				else
				{
					CapsuleCollider val2 = base._collider as CapsuleCollider;
					if (val2 != null)
					{
						float num2 = val2.get_radius() / 4f;
						Vector3 localScale2 = base._transform.get_localScale();
						debuffShadowSealingRadius = num2 * localScale2.x;
					}
				}
			}
		}
	}

	protected virtual void ActDebuffShadowSealingEnd()
	{
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Expected O, but got Unknown
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Expected O, but got Unknown
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		if (IsDebuffShadowSealing())
		{
			bool flag = true;
			bool flag2 = false;
			if (shadowSealingStackDebuff.Contains((ACTION_ID)13))
			{
				EndAction();
				base.actionID = (ACTION_ID)13;
				PlayMotion((!IsAbleToUseDownTime()) ? 117 : 118, -1f);
				flag2 = true;
			}
			if (shadowSealingStackDebuff.Contains((ACTION_ID)21))
			{
				if (flag2)
				{
					ActLightRingEnd();
				}
				else
				{
					EndAction();
					base.actionID = (ACTION_ID)21;
					PlayMotion(6, (!(stopMotionByDebuffNormalizedTime < 0f)) ? 0f : (-1f));
					if (base._rigidbody != null)
					{
						base._rigidbody.set_velocity(Vector3.get_zero());
					}
					rotateEventKeep = false;
					rotateToTargetFlag = false;
					rotateEventSpeed = 0f;
				}
				flag2 = true;
			}
			if (shadowSealingStackDebuff.Contains(ACTION_ID.PARALYZE))
			{
				if (flag2)
				{
					ActParalyzeEnd();
				}
				else
				{
					EndAction();
					base.actionID = ACTION_ID.PARALYZE;
					PlayMotion(8, -1f);
				}
				flag2 = true;
			}
			if (shadowSealingStackDebuff.Contains(ACTION_ID.FREEZE))
			{
				if (flag2)
				{
					ActFreezeEnd();
				}
				else
				{
					flag = false;
					base.actionID = ACTION_ID.FREEZE;
				}
			}
			if (!object.ReferenceEquals(paralyzeEffectTrans, null))
			{
				EffectManager.ReleaseEffect(paralyzeEffectTrans.get_gameObject(), true, false);
				paralyzeEffectTrans = null;
			}
			if (!object.ReferenceEquals(debuffShadowSealingEffect, null))
			{
				EffectManager.ReleaseEffect(debuffShadowSealingEffect.get_gameObject(), true, false);
				debuffShadowSealingEffect = null;
			}
			InGameSettingsManager.ShadowSealingParam shadowSealingParam = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.shadowSealingParam;
			if (shadowSealingParam.loopSeId != 0)
			{
				SoundManager.StopLoopSE(shadowSealingParam.loopSeId, this);
			}
			if (shadowSealingParam.endSeId != 0)
			{
				SoundManager.PlayOneShotSE(shadowSealingParam.endSeId, base._transform.get_position());
			}
			badStatusMax.shadowSealing *= shadowSealingParam.resistRate;
			badStatusMax.shadowSealingBind *= shadowSealingBindResist;
			if (flag)
			{
				setPause(false);
				m_isStopMotionByDebuff = false;
			}
			_CheckShadowSealingTask();
			int i = 0;
			for (int num = regionWorks.Length; i < num; i++)
			{
				regionWorks[i].shadowSealingData.ownerID = 0;
			}
			if (MonoBehaviourSingleton<UIEnemyStatus>.IsValid())
			{
				MonoBehaviourSingleton<UIEnemyStatus>.I.UpDateStatusIcon();
			}
			shadowSealingStackDebuff.Clear();
		}
	}

	private void UpdateDebuffShadowSealingAction()
	{
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		if (IsDebuffShadowSealing())
		{
			debuffShadowSealingTimer -= Time.get_deltaTime();
			if (debuffShadowSealingTimer <= 0f)
			{
				ActDebuffShadowSealingEnd();
			}
			else
			{
				for (int i = 0; i < shadowSealingStackDebuff.Count; i++)
				{
					bool flag = false;
					switch (shadowSealingStackDebuff[i])
					{
					case ACTION_ID.FREEZE:
						flag = UpdateFreezeAction();
						break;
					case ACTION_ID.PARALYZE:
						flag = UpdateParalyzeAction();
						break;
					case (ACTION_ID)13:
						flag = UpdateDownAction();
						break;
					case (ACTION_ID)21:
						flag = UpdateLightRingAction();
						break;
					}
					if (flag)
					{
						i--;
					}
				}
				float num = 0.1f;
				if (stopMotionByDebuffNormalizedTime >= 0f)
				{
					num = stopMotionByDebuffNormalizedTime;
				}
				AnimatorStateInfo currentAnimatorStateInfo = base.animator.GetCurrentAnimatorStateInfo(0);
				if (!(currentAnimatorStateInfo.get_normalizedTime() < num) && currentAnimatorStateInfo.get_fullPathHash() == Animator.StringToHash("Base Layer.damage") && !m_isStopMotionByDebuff)
				{
					setPause(true);
					m_isStopMotionByDebuff = true;
				}
			}
		}
	}

	public override bool IsDebuffShadowSealing()
	{
		return debuffShadowSealingEffect != null;
	}

	public float GetDebuffShadowSealingTimeRate()
	{
		return debuffShadowSealingTimer / debuffShadowSealingTimerDuration;
	}

	public void CountShadowSealingTarget()
	{
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Expected O, but got Unknown
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Expected O, but got Unknown
		shadowSealingTarget = 0;
		if (isBoss && !object.ReferenceEquals(regionWorks, null) && !targetPoints.IsNullOrEmpty())
		{
			int i = 0;
			for (int num = regionWorks.Length; i < num; i++)
			{
				regionWorks[i].shadowSealingData.isTarget = false;
			}
			List<int> list = new List<int>();
			int j = 0;
			for (int num2 = targetPoints.Length; j < num2; j++)
			{
				TargetPoint targetPoint = targetPoints[j];
				if (targetPoint.regionID >= 0 && targetPoint.regionID < regionWorks.Length)
				{
					EnemyRegionWork enemyRegionWork = regionWorks[targetPoint.regionID];
					if (!targetPoint.isAimEnable || !targetPoint.param.isTargetEnable)
					{
						int k = 0;
						for (int count = enemyRegionWork.bleedWorkList.Count; k < count; k++)
						{
							BleedWork bleedWork = enemyRegionWork.bleedWorkList[k];
							if (!object.ReferenceEquals(bleedWork, null) && !object.ReferenceEquals(bleedWork.bleedEffect, null))
							{
								EffectManager.ReleaseEffect(bleedWork.bleedEffect.get_gameObject(), true, false);
								bleedWork.bleedEffect = null;
							}
						}
						enemyRegionWork.bleedList.Clear();
						enemyRegionWork.bleedWorkList.Clear();
						enemyRegionWork.shadowSealingData.ownerID = 0;
						enemyRegionWork.shadowSealingData.existSec = 0f;
						enemyRegionWork.shadowSealingData.extendRate = 1f;
						if (!object.ReferenceEquals(enemyRegionWork.shadowSealingEffect, null))
						{
							EffectManager.ReleaseEffect(enemyRegionWork.shadowSealingEffect.get_gameObject(), false, true);
							enemyRegionWork.shadowSealingEffect = null;
						}
					}
					else if (!list.Contains(targetPoint.regionID))
					{
						enemyRegionWork.shadowSealingData.isTarget = true;
						list.Add(targetPoint.regionID);
					}
				}
			}
			shadowSealingTarget = list.Count;
			if (_CheckShadowSealingFullStuck())
			{
				ActDebuffShadowSealingStart();
			}
			if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<StageObjectManager>.I.self != null)
			{
				MonoBehaviourSingleton<StageObjectManager>.I.self.ResetShadowSealingUI();
			}
		}
	}

	private bool _CheckShadowSealingFullStuck()
	{
		bool result = false;
		int i = 0;
		for (int num = regionWorks.Length; i < num; i++)
		{
			ShadowSealingData shadowSealingData = regionWorks[i].shadowSealingData;
			if (shadowSealingData.isTarget)
			{
				result = true;
				if (shadowSealingData.ownerID == 0)
				{
					return false;
				}
			}
		}
		return result;
	}

	private void _CheckShadowSealingTask()
	{
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
			if (!object.ReferenceEquals(self, null))
			{
				int num = 0;
				int num2 = regionWorks.Length;
				while (true)
				{
					if (num >= num2)
					{
						return;
					}
					ShadowSealingData shadowSealingData = regionWorks[num].shadowSealingData;
					if (shadowSealingData.isTarget && shadowSealingData.ownerID == self.id)
					{
						break;
					}
					num++;
				}
				self.taskChecker.OnShadowSealing();
				if (MonoBehaviourSingleton<InGameManager>.IsValid())
				{
					MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnShadowSealing();
				}
			}
		}
	}

	private float _GetShadowSealingExtendRate()
	{
		if (object.ReferenceEquals(regionWorks, null))
		{
			return 1f;
		}
		float num = 1f;
		int i = 0;
		for (int num2 = regionWorks.Length; i < num2; i++)
		{
			ShadowSealingData shadowSealingData = regionWorks[i].shadowSealingData;
			if (shadowSealingData.isTarget && shadowSealingData.ownerID != 0 && num < shadowSealingData.extendRate)
			{
				num = shadowSealingData.extendRate;
			}
		}
		return num;
	}

	public int GetShadowSealingStuckNum()
	{
		if (object.ReferenceEquals(regionWorks, null))
		{
			return 0;
		}
		int num = 0;
		int i = 0;
		for (int num2 = regionWorks.Length; i < num2; i++)
		{
			ShadowSealingData shadowSealingData = regionWorks[i].shadowSealingData;
			if (shadowSealingData.isTarget && shadowSealingData.ownerID != 0)
			{
				num++;
			}
		}
		return num;
	}

	public int GetShadowSealingNum()
	{
		return shadowSealingTarget;
	}

	public OpponentMemory.OpponentRecord[] GetHateRankingObjects(bool isIncludeDead)
	{
		EnemyBrain enemyBrain = base.controller.brain as EnemyBrain;
		if (enemyBrain != null)
		{
			return enemyBrain.opponentMem.GetOpponentWithRankingHate(isIncludeDead, 4);
		}
		return null;
	}

	protected bool CheckMadMode(AttackedHitStatusOwner status)
	{
		if (madModeHp == 0)
		{
			return false;
		}
		if (status.isDamageRegionOnly)
		{
			return false;
		}
		if (IsValidBuff(BuffParam.BUFFTYPE.MAD_MODE))
		{
			return false;
		}
		int num = base.hp - status.damage;
		if (num <= madModeHp)
		{
			status.damage -= madModeHp - num;
			return true;
		}
		return false;
	}

	private bool CheckDisableBuffTypeByMadMode(BuffParam.BUFFTYPE targetType)
	{
		if (!IsValidBuff(BuffParam.BUFFTYPE.MAD_MODE))
		{
			return false;
		}
		if (MonoBehaviourSingleton<InGameSettingsManager>.I.madModeParam.onlyResistDebuff.Contains(targetType))
		{
			return false;
		}
		if (MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.ignoreBuffCancellation.Contains(targetType))
		{
			return true;
		}
		return false;
	}

	public void ActMadMode()
	{
		EndAction();
		if (!PlayMotion(123, -1f))
		{
			ActIdle(false, -1f);
		}
		else
		{
			base.actionID = (ACTION_ID)19;
			downTotal = 0f;
			base.badStatusTotal.Reset();
			bool isClearStuckArrow = MonoBehaviourSingleton<InGameSettingsManager>.I.madModeParam.isClearStuckArrow;
			if (isClearStuckArrow)
			{
				ClearBleedDamageAll();
			}
			if (IsDebuffShadowSealing() || isClearStuckArrow)
			{
				ClearShadowSealingAll(true, true);
			}
			bool flag = false;
			List<BuffParam.BUFFTYPE> ignoreBuffCancellation = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.ignoreBuffCancellation;
			int i = 0;
			for (int count = ignoreBuffCancellation.Count; i < count; i++)
			{
				if (OnBuffEnd(ignoreBuffCancellation[i], false, true))
				{
					flag = true;
				}
			}
			if (flag)
			{
				SendBuffSync(BuffParam.BUFFTYPE.NONE);
			}
			if (MonoBehaviourSingleton<UIEnemyAnnounce>.IsValid())
			{
				MonoBehaviourSingleton<UIEnemyAnnounce>.I.RequestAnnounce(enemyTableData.name, STRING_CATEGORY.ENEMY_REACTION, kStrIdx_EnemyReaction_MadMode);
			}
			OnActReaction();
		}
	}

	public void CheckFirstMadMode()
	{
		if ((IsCoopNone() || IsOriginal()) && madModeHpThreshold >= 100 && (int)enemyLevel >= madModeLvThreshold)
		{
			LocalMadModeStart();
		}
	}

	public void LocalMadModeStart()
	{
		BuffParam.BuffData data = new BuffParam.BuffData();
		data.type = BuffParam.BUFFTYPE.MAD_MODE;
		data.time = -1f;
		data.endless = true;
		data.valueType = BuffParam.VALUE_TYPE.CONSTANT;
		data.value = 1;
		SetFromInfo(ref data);
		OnBuffStart(data);
	}

	public override int GetObservedID()
	{
		string s = (id % 500000).ToString() + bulletIndex.ToString();
		int result = -1;
		if (int.TryParse(s, out result))
		{
			bulletIndex++;
			return result;
		}
		return -1;
	}

	public override void OnBreak(int brokenBulletID, bool isSendOnlyOrigin)
	{
		if (!bulletObservableList.IsNullOrEmpty() && !bulletObservableIdList.IsNullOrEmpty() && bulletObservableIdList.Contains(brokenBulletID))
		{
			for (int i = 0; i < bulletObservableList.Count; i++)
			{
				if (bulletObservableList[i].GetObservedID() == brokenBulletID)
				{
					bulletObservableList[i].ForceBreak();
				}
			}
			bulletObservableList.RemoveAll((IBulletObservable o) => o.GetObservedID() == brokenBulletID);
			bulletObservableIdList.RemoveAll((int o) => o == brokenBulletID);
			if (enemySender != null)
			{
				enemySender.OnBulletObservableBroken(brokenBulletID, isSendOnlyOrigin);
			}
		}
	}

	public override void OnBulletDestroy(int observedID)
	{
		bulletObservableList.RemoveAll((IBulletObservable o) => o.GetObservedID() == observedID);
		bulletObservableIdList.RemoveAll((int o) => o == observedID);
	}
}
