using Network;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BuffParam
{
	public enum BUFFTYPE
	{
		NONE = -1,
		ATTACK_NORMAL,
		ATTACK_FIRE,
		ATTACK_WATER,
		ATTACK_THUNDER,
		ATTACK_SOIL,
		ATTACK_LIGHT,
		ATTACK_DARK,
		ATTACK_ALLELEMENT,
		ATTACK_PARALYZE,
		ATTACK_POISON,
		DEFENCE_NORMAL,
		DEFENCE_FIRE,
		DEFENCE_WATER,
		DEFENCE_THUNDER,
		DEFENCE_SOIL,
		DEFENCE_LIGHT,
		DEFENCE_DARK,
		DEFENCE_ALLELEMENT,
		MOVE_SPEED_UP,
		MOVE_SPEED_DOWN,
		ATTACK_SPEED_UP,
		INVINCIBLECOUNT,
		REGENERATE,
		HP_HEAL_SPEEDUP,
		SKILL_ABSORBUP,
		SKILL_HEAL_SPEEDUP,
		POISON,
		BURNING,
		DEADLY_POISON,
		DAMAGE_DOWN,
		LUNATIC_TEAR,
		WING,
		GHOST_FORM,
		BREAK_GHOST_FORM,
		ELECTRIC_SHOCK,
		INK_SPLASH,
		ATKUP_RATE_NORMAL,
		ATKUP_RATE_FIRE,
		ATKUP_RATE_WATER,
		ATKUP_RATE_THUNDER,
		ATKUP_RATE_SOIL,
		ATKUP_RATE_LIGHT,
		ATKUP_RATE_DARK,
		ATKUP_RATE_ALLELEMENT,
		DEFUP_RATE_NORMAL,
		DEFUP_RATE_FIRE,
		DEFUP_RATE_WATER,
		DEFUP_RATE_THUNDER,
		DEFUP_RATE_SOIL,
		DEFUP_RATE_LIGHT,
		DEFUP_RATE_DARK,
		DEFUP_RATE_ALLELEMENT,
		DAMAGE_UP,
		POISON_DAMAGE_DOWN,
		POISON_GUARD,
		BURN_DAMAGE_DOWN,
		BURN_GUARD,
		SUPER_ARMOR,
		DEFDOWN_RATE_NORMAL,
		DEFDOWN_RATE_ALLELEMENT,
		SHIELD,
		SHIELD_SUPER_ARMOR,
		SKILL_CHARGE,
		SKILL_CHARGE_RATE,
		HP_UP,
		HP_DOWN,
		HPUP_RATE,
		HPDOWN_RATE,
		ATTACK_DOWN_NORMAL,
		ATTACK_DOWN_FIRE,
		ATTACK_DOWN_WATER,
		ATTACK_DOWN_THUNDER,
		ATTACK_DOWN_SOIL,
		ATTACK_DOWN_LIGHT,
		ATTACK_DOWN_DARK,
		ATTACK_DOWN_ALLELEMENT,
		ATKDOWN_RATE_NORMAL,
		ATKDOWN_RATE_FIRE,
		ATKDOWN_RATE_WATER,
		ATKDOWN_RATE_THUNDER,
		ATKDOWN_RATE_SOIL,
		ATKDOWN_RATE_LIGHT,
		ATKDOWN_RATE_DARK,
		ATKDOWN_RATE_ALLELEMENT,
		DEFENCE_DOWN_NORMAL,
		DEFENCE_DOWN_FIRE,
		DEFENCE_DOWN_WATER,
		DEFENCE_DOWN_THUNDER,
		DEFENCE_DOWN_SOIL,
		DEFENCE_DOWN_LIGHT,
		DEFENCE_DOWN_DARK,
		DEFENCE_DOWN_ALLELEMENT,
		DEFDOWN_RATE_FIRE,
		DEFDOWN_RATE_WATER,
		DEFDOWN_RATE_THUNDER,
		DEFDOWN_RATE_SOIL,
		DEFDOWN_RATE_LIGHT,
		DEFDOWN_RATE_DARK,
		TOLERANCE_FIRE,
		TOLERANCE_WATER,
		TOLERANCE_THUNDER,
		TOLERANCE_SOIL,
		TOLERANCE_LIGHT,
		TOLERANCE_DARK,
		TOLERANCE_ALLELEMENT,
		TOLERANCE_DOWN_FIRE,
		TOLERANCE_DOWN_WATER,
		TOLERANCE_DOWN_THUNDER,
		TOLERANCE_DOWN_SOIL,
		TOLERANCE_DOWN_LIGHT,
		TOLERANCE_DOWN_DARK,
		TOLERANCE_DOWN_ALLELEMENT,
		TOLUP_RATE_FIRE,
		TOLUP_RATE_WATER,
		TOLUP_RATE_THUNDER,
		TOLUP_RATE_SOIL,
		TOLUP_RATE_LIGHT,
		TOLUP_RATE_DARK,
		TOLUP_RATE_ALLELEMENT,
		TOLDOWN_RATE_FIRE,
		TOLDOWN_RATE_WATER,
		TOLDOWN_RATE_THUNDER,
		TOLDOWN_RATE_SOIL,
		TOLDOWN_RATE_LIGHT,
		TOLDOWN_RATE_DARK,
		TOLDOWN_RATE_ALLELEMENT,
		SLIDE,
		JUSTGUARD_EXTEND_RATE,
		PARALYZE_GUARD,
		SILENCE,
		SKILL_CHARGE_FIRE,
		SKILL_CHARGE_WATER,
		SKILL_CHARGE_THUNDER,
		SKILL_CHARGE_SOIL,
		SKILL_CHARGE_LIGHT,
		SKILL_CHARGE_DARK,
		REGENERATE_PROPORTION,
		SKILL_ABSORBUP_ONLY_ATK_FIRE,
		SKILL_ABSORBUP_ONLY_ATK_WATER,
		SKILL_ABSORBUP_ONLY_ATK_THUNDER,
		SKILL_ABSORBUP_ONLY_ATK_SOIL,
		SKILL_ABSORBUP_ONLY_ATK_LIGHT,
		SKILL_ABSORBUP_ONLY_ATK_DARK,
		SILENCE_GUARD,
		ABSORB_NORMAL,
		ABSORB_FIRE,
		ABSORB_WATER,
		ABSORB_THUNDER,
		ABSORB_SOIL,
		ABSORB_LIGHT,
		ABSORB_DARK,
		ABSORB_ALL_ELEMENT,
		HIT_ABSORB_NORMAL,
		HIT_ABSORB_FIRE,
		HIT_ABSORB_WATER,
		HIT_ABSORB_THUNDER,
		HIT_ABSORB_SOIL,
		HIT_ABSORB_LIGHT,
		HIT_ABSORB_DARK,
		HIT_ABSORB_ALL,
		MAD_MODE,
		ATTACK_SPEED_DOWN,
		AUTO_REVIVE,
		WARP_BY_AVOID,
		INVINCIBLE_NORMAL,
		INVINCIBLE_FIRE,
		INVINCIBLE_WATER,
		INVINCIBLE_THUNDER,
		INVINCIBLE_SOIL,
		INVINCIBLE_ALL,
		DAMAGE_UP_NORMAL,
		DAMAGE_UP_FROM_AVOID,
		HEAT_GAUGE_INCREASE_UP,
		HEAT_GAUGE_INCREASE_DOWN,
		SOUL_GAUGE_INCREASE_UP,
		SOUL_GAUGE_INCREASE_DOWN,
		SKILL_CHARGE_WHEN_DAMAGED,
		INVINCIBLE_LIGHT,
		INVINCIBLE_DARK,
		CANT_HEAL_HP,
		BLIND,
		ATTACK_FREEZE,
		INVINCIBLE_BADSTATUS,
		SLIDE_ICE,
		BAD_STATUS_DOWN_RATE_UP,
		LIGHT_RING,
		DISTANCE_UP_IAI,
		BOOST_DAMAGE_UP,
		BOOST_DAMAGE_DOWN,
		BOOST_ATTACK_SPEED_UP,
		BOOST_MOVE_SPEED_UP,
		BOOST_AVOID_UP,
		MAX,
		HIT_PARALYZE,
		HIT_POISON
	}

	public enum TOLERANCETYPE
	{
		PARALYZE,
		POISON,
		BURNING,
		SPEED_DOWN,
		STUMBLE,
		SHAKE,
		SOUNDWAVE,
		SKILL,
		FREEZE,
		SLIDE,
		SILENCE,
		ATTACK_SPEED_DOWN,
		CANT_HEAL_HP,
		BLIND,
		MAX
	}

	public enum BAD_STATUS_UP
	{
		PARALYZE,
		POISON,
		DOWN,
		FREEZE,
		MAX
	}

	public enum VALUE_TYPE
	{
		NONE,
		RATE,
		CONSTANT
	}

	public class ConditionsAbility
	{
		public class ConditionsAbilityInfo
		{
			public AbilityDataTable.AbilityData.AbilityInfo abilityInfo = new AbilityDataTable.AbilityData.AbilityInfo();

			public AbilityAtkBase atkBase = new AbilityAtkBase();

			public bool isValid = true;

			public bool isMatch;

			public int currentStack;

			public bool isStacked;

			public List<int> executedStackOrder = new List<int>();

			public int GetMaxStack()
			{
				if (abilityInfo.enables != null && abilityInfo.enables.Count > 0)
				{
					for (int i = 0; i < abilityInfo.enables.Count; i++)
					{
						if (abilityInfo.enables[i].type == ABILITY_ENABLE_TYPE.FINISH_A_CLEAVE_COMBO)
						{
							return 3;
						}
					}
				}
				return 1;
			}
		}

		public ConditionsAbilityInfo[] conditionInfos = new ConditionsAbilityInfo[3];

		public int counterAttackNum;

		public int cleaveComboNum;
	}

	public class PassiveBuff
	{
		public int hp;

		public List<int> atkList = new List<int>();

		public List<int> defList = new List<int>();

		public List<int> tolList = new List<int>();

		[FormerlySerializedAs("atkAllElement")]
		private XorFloat _atkAllElement = 0f;

		[FormerlySerializedAs("moveSpeedUp")]
		private XorFloat _moveSpeedUp = 0f;

		[FormerlySerializedAs("boostMoveSpeedUp")]
		private XorFloat _boostMoveSpeedUp = 0f;

		[FormerlySerializedAs("attackSpeedUp")]
		private XorFloat _attackSpeedUp = 0f;

		[FormerlySerializedAs("boostAttackSpeedUp")]
		private XorFloat _boostAttackSpeedUp = 0f;

		[FormerlySerializedAs("hpHealSpeedUp")]
		private XorFloat _hpHealSpeedUp = 0f;

		[FormerlySerializedAs("guardUp")]
		private XorFloat _guardUp = 0f;

		[FormerlySerializedAs("skillAbsorbUp")]
		private XorFloat _skillAbsorbUp = 0f;

		[FormerlySerializedAs("skillHealSpeedUp")]
		private XorFloat _skillHealSpeedUp = 0f;

		[FormerlySerializedAs("avoidUp")]
		private XorFloat _avoidUp = 0f;

		[FormerlySerializedAs("boostAvoidUp")]
		private XorFloat _boostAvoidUp = 0f;

		[FormerlySerializedAs("healUp")]
		private XorFloat _healUP = 0f;

		private XorFloat _healUpDependsWeapon = 0f;

		[FormerlySerializedAs("bleedUp")]
		private XorFloat _bleedUp = 0f;

		[FormerlySerializedAs("chargeSwordsTimeRate")]
		private XorFloat _chargeSwordsTimeRate = 0f;

		[FormerlySerializedAs("chargeArrowTimeRate")]
		private XorFloat _chargeArrowTimeRate = 0f;

		[FormerlySerializedAs("chargeHeatArrowTimeRate")]
		private XorFloat _chargeHeatArrowTimeRate = 0f;

		[FormerlySerializedAs("chargePairSwordsTimeRate")]
		private XorFloat _chargePairSwordsTimeRate = 0f;

		[FormerlySerializedAs("spearRushDistanceRate")]
		private XorFloat _spearRushDistanceRate = 0f;

		[FormerlySerializedAs("chargeSpearTimeRate")]
		private XorFloat _chargeSpearTimeRate = 0f;

		[FormerlySerializedAs("skillTimeRate")]
		private XorFloat _skillTimeRate = 0f;

		[FormerlySerializedAs("damageDown")]
		private XorFloat _damageDown = 0f;

		[FormerlySerializedAs("boostDamageDown")]
		private XorFloat _boostDamageDown = 0f;

		[FormerlySerializedAs("narrowEscape")]
		private XorInt _narrowEscape = 0;

		public bool enableNarrowEscape;

		[FormerlySerializedAs("_poisonDamageDownRate")]
		private XorFloat _poisonDamageDownRate = 0f;

		[FormerlySerializedAs("_poisonGuardWeight")]
		private XorInt _poisonGuardWeight = 0;

		[FormerlySerializedAs("burnDamageDownRate")]
		private XorFloat _burnDamageDownRate = 0f;

		[FormerlySerializedAs("burnGuardWeight")]
		private XorInt _burnGuardWeight = 0;

		[FormerlySerializedAs("paralyzeGuardWeight")]
		private XorInt _paralyzeGuardWeight = 0;

		[FormerlySerializedAs("silenceGuardWeight")]
		private XorInt _silenceGuardWeight = 0;

		[FormerlySerializedAs("justGuardExtendRate")]
		private XorFloat _justGuardExtendRate = 0f;

		[FormerlySerializedAs("gaugeIncreaseRate")]
		private XorFloat _gaugeIncreaseRate = 0f;

		[FormerlySerializedAs("gaugeDecreaseRate")]
		private XorFloat _gaugeDecreaseRate = 0f;

		[FormerlySerializedAs("subGaugeIncreaseRate")]
		private XorFloat _subGaugeIncreaseRate = 0f;

		[FormerlySerializedAs("heatGaugeIncreaseRate")]
		private XorFloat _heatGaugeIncreaseRate = 0f;

		[FormerlySerializedAs("soulGaugeIncreaseRate")]
		private XorFloat _soulGaugeIncreaseRate = 0f;

		[FormerlySerializedAs("soulChargeTimeRate")]
		private XorFloat _soulChargeTimeRate = 0f;

		[FormerlySerializedAs("shadowSealingExtend")]
		private XorFloat _shadowSealingExtend = 0f;

		[FormerlySerializedAs("shadowSealingExtendArrow")]
		private XorFloat _shadowSealingExtendArrow = 0f;

		[FormerlySerializedAs("distanceRateFromAvoid")]
		private XorFloat _distanceRateFromAvoid = 0f;

		[FormerlySerializedAs("distanceRateIai")]
		private XorFloat _distanceRateIai = 0f;

		[FormerlySerializedAs("lockOnTimeRate")]
		private XorFloat _lockOnTimeRate = 0f;

		[FormerlySerializedAs("ReloadSpeed")]
		private XorFloat _burstReloadActionSpeed = 0f;

		[FormerlySerializedAs("AdditionalMaxBulletCount")]
		private XorInt _additionalMaxBulletCnt = 0;

		[FormerlySerializedAs("dragonArmorDamageRate")]
		private XorFloat _dragonArmorDamageRate = 0f;

		public List<AbilityAtkBase> abilityAtkList = new List<AbilityAtkBase>(64);

		public int[] tolerance = new int[14];

		public float[] badStatusUp = new float[4];

		public float[] badStatusRateUp = new float[4];

		public float hpUpRate;

		public float hpDownRate;

		public AtkAttribute atkUpRate = new AtkAttribute();

		public AtkAttribute atkDownRate = new AtkAttribute();

		public AtkAttribute defUpRate = new AtkAttribute();

		public AtkAttribute defDownRate = new AtkAttribute();

		public AtkAttribute tolUpRate = new AtkAttribute();

		public AtkAttribute tolDownRate = new AtkAttribute();

		public AtkAttribute skillAbsorbUp_OnlyAttackAndElement = new AtkAttribute();

		public bool firstInitialized;

		public List<ConditionsAbility> conditionsAbilityList = new List<ConditionsAbility>();

		public int breakGhostFormCounter;

		public Dictionary<uint, float> fieldBuffResist = new Dictionary<uint, float>(192);

		public Dictionary<BUFFTYPE, float> fieldBuffResistByType = new Dictionary<BUFFTYPE, float>(192);

		public float atkAllElement
		{
			get
			{
				return _atkAllElement;
			}
			set
			{
				_atkAllElement = value;
			}
		}

		public float moveSpeedUp
		{
			get
			{
				return _moveSpeedUp;
			}
			set
			{
				_moveSpeedUp = value;
			}
		}

		public float boostMoveSpeedUp
		{
			get
			{
				return _boostMoveSpeedUp;
			}
			set
			{
				_boostMoveSpeedUp = value;
			}
		}

		public float attackSpeedUp
		{
			get
			{
				return _attackSpeedUp;
			}
			set
			{
				_attackSpeedUp = value;
			}
		}

		public float boostAttackSpeedUp
		{
			get
			{
				return _boostAttackSpeedUp;
			}
			set
			{
				_boostAttackSpeedUp = value;
			}
		}

		public float hpHealSpeedUp
		{
			get
			{
				return _hpHealSpeedUp;
			}
			set
			{
				_hpHealSpeedUp = value;
			}
		}

		public float guardUp
		{
			get
			{
				return _guardUp;
			}
			set
			{
				_guardUp = value;
			}
		}

		public float skillAbsorbUp
		{
			get
			{
				return _skillAbsorbUp;
			}
			set
			{
				_skillAbsorbUp = value;
			}
		}

		public float skillHealSpeedUp
		{
			get
			{
				return _skillHealSpeedUp;
			}
			set
			{
				_skillHealSpeedUp = value;
			}
		}

		public float avoidUp
		{
			get
			{
				return _avoidUp;
			}
			set
			{
				_avoidUp = value;
			}
		}

		public float boostAvoidUp
		{
			get
			{
				return _boostAvoidUp;
			}
			set
			{
				_boostAvoidUp = value;
			}
		}

		public float healUP
		{
			get
			{
				return _healUP;
			}
			set
			{
				_healUP = value;
			}
		}

		public float healUpDependsWeapon
		{
			get
			{
				return _healUpDependsWeapon;
			}
			set
			{
				_healUpDependsWeapon = value;
			}
		}

		public float bleedUp
		{
			get
			{
				return _bleedUp;
			}
			set
			{
				_bleedUp = value;
			}
		}

		public float chargeSwordsTimeRate
		{
			get
			{
				return _chargeSwordsTimeRate;
			}
			set
			{
				_chargeSwordsTimeRate = value;
			}
		}

		public float chargeArrowTimeRate
		{
			get
			{
				return _chargeArrowTimeRate;
			}
			set
			{
				_chargeArrowTimeRate = value;
			}
		}

		public float chargeHeatArrowTimeRate
		{
			get
			{
				return _chargeHeatArrowTimeRate;
			}
			set
			{
				_chargeHeatArrowTimeRate = value;
			}
		}

		public float chargePairSwordsTimeRate
		{
			get
			{
				return _chargePairSwordsTimeRate;
			}
			set
			{
				_chargePairSwordsTimeRate = value;
			}
		}

		public float spearRushDistanceRate
		{
			get
			{
				return _spearRushDistanceRate;
			}
			set
			{
				_spearRushDistanceRate = value;
			}
		}

		public float chargeSpearTimeRate
		{
			get
			{
				return _chargeSpearTimeRate;
			}
			set
			{
				_chargeSpearTimeRate = value;
			}
		}

		public float skillTimeRate
		{
			get
			{
				return _skillTimeRate;
			}
			set
			{
				_skillTimeRate = value;
			}
		}

		public float damageDown
		{
			get
			{
				return _damageDown;
			}
			set
			{
				_damageDown = value;
			}
		}

		public float boostDamageDown
		{
			get
			{
				return _boostDamageDown;
			}
			set
			{
				_boostDamageDown = value;
			}
		}

		public int narrowEscape
		{
			get
			{
				return _narrowEscape;
			}
			set
			{
				_narrowEscape = value;
			}
		}

		public float poisonDamageDownRate
		{
			get
			{
				return _poisonDamageDownRate;
			}
			set
			{
				_poisonDamageDownRate = value;
			}
		}

		public int poisonGuardWeight
		{
			get
			{
				return _poisonGuardWeight;
			}
			set
			{
				_poisonGuardWeight = value;
			}
		}

		public float burnDamageDownRate
		{
			get
			{
				return _burnDamageDownRate;
			}
			set
			{
				_burnDamageDownRate = value;
			}
		}

		public int burnGuardWeight
		{
			get
			{
				return _burnGuardWeight;
			}
			set
			{
				_burnGuardWeight = value;
			}
		}

		public int paralyzeGuardWeight
		{
			get
			{
				return _paralyzeGuardWeight;
			}
			set
			{
				_paralyzeGuardWeight = value;
			}
		}

		public int silenceGuardWeight
		{
			get
			{
				return _silenceGuardWeight;
			}
			set
			{
				_silenceGuardWeight = value;
			}
		}

		public float justGuardExtendRate
		{
			get
			{
				return _justGuardExtendRate;
			}
			set
			{
				_justGuardExtendRate = value;
			}
		}

		public float gaugeIncreaseRate
		{
			get
			{
				return _gaugeIncreaseRate;
			}
			set
			{
				_gaugeIncreaseRate = value;
			}
		}

		public float gaugeDecreaseRate
		{
			get
			{
				return _gaugeDecreaseRate;
			}
			set
			{
				_gaugeDecreaseRate = value;
			}
		}

		public float subGaugeIncreaseRate
		{
			get
			{
				return _subGaugeIncreaseRate;
			}
			set
			{
				_subGaugeIncreaseRate = value;
			}
		}

		public float heatGaugeIncreaseRate
		{
			get
			{
				return _heatGaugeIncreaseRate;
			}
			set
			{
				_heatGaugeIncreaseRate = value;
			}
		}

		public float soulGaugeIncreaseRate
		{
			get
			{
				return _soulGaugeIncreaseRate;
			}
			set
			{
				_soulGaugeIncreaseRate = value;
			}
		}

		public float soulChargeTimeRate
		{
			get
			{
				return _soulChargeTimeRate;
			}
			set
			{
				_soulChargeTimeRate = value;
			}
		}

		public float shadowSealingExtend
		{
			get
			{
				return _shadowSealingExtend;
			}
			set
			{
				_shadowSealingExtend = value;
			}
		}

		public float shadowSealingExtendArrow
		{
			get
			{
				return _shadowSealingExtendArrow;
			}
			set
			{
				_shadowSealingExtendArrow = value;
			}
		}

		public float distanceRateFromAvoid
		{
			get
			{
				return _distanceRateFromAvoid;
			}
			set
			{
				_distanceRateFromAvoid = value;
			}
		}

		public float distanceRateIai
		{
			get
			{
				return _distanceRateIai;
			}
			set
			{
				_distanceRateIai = value;
			}
		}

		public float lockOnTimeRate
		{
			get
			{
				return _lockOnTimeRate;
			}
			set
			{
				_lockOnTimeRate = value;
			}
		}

		public float burstReloadActionSpeed
		{
			get
			{
				return _burstReloadActionSpeed;
			}
			set
			{
				_burstReloadActionSpeed = value;
			}
		}

		public int additionalMaxBulletCnt
		{
			get
			{
				return _additionalMaxBulletCnt;
			}
			set
			{
				_additionalMaxBulletCnt = value;
			}
		}

		public float dragonArmorDamageRate
		{
			get
			{
				return _dragonArmorDamageRate;
			}
			set
			{
				_dragonArmorDamageRate = value;
			}
		}

		public void Reset()
		{
			hp = 0;
			atkList.Clear();
			defList.Clear();
			for (int i = 0; i < 7; i++)
			{
				atkList.Add(0);
				defList.Add(0);
			}
			tolList.Clear();
			for (int j = 0; j < 6; j++)
			{
				tolList.Add(0);
			}
			hpUpRate = 0f;
			hpDownRate = 0f;
			atkUpRate.Set(0f);
			atkDownRate.Set(0f);
			defUpRate.Set(0f);
			defDownRate.Set(0f);
			tolUpRate.Set(0f);
			tolDownRate.Set(0f);
			skillAbsorbUp_OnlyAttackAndElement.Set(0f);
			moveSpeedUp = 0f;
			boostMoveSpeedUp = 0f;
			attackSpeedUp = 0f;
			boostAttackSpeedUp = 0f;
			hpHealSpeedUp = 0f;
			guardUp = 0f;
			skillAbsorbUp = 0f;
			skillHealSpeedUp = 0f;
			avoidUp = 0f;
			boostAvoidUp = 0f;
			healUP = 0f;
			healUpDependsWeapon = 0f;
			chargeSwordsTimeRate = 0f;
			chargeArrowTimeRate = 0f;
			chargeHeatArrowTimeRate = 0f;
			chargePairSwordsTimeRate = 0f;
			spearRushDistanceRate = 0f;
			chargeSpearTimeRate = 0f;
			skillTimeRate = 0f;
			bleedUp = 0f;
			damageDown = 0f;
			boostDamageDown = 0f;
			atkAllElement = 0f;
			gaugeIncreaseRate = 0f;
			gaugeDecreaseRate = 0f;
			subGaugeIncreaseRate = 0f;
			heatGaugeIncreaseRate = 0f;
			soulGaugeIncreaseRate = 0f;
			soulChargeTimeRate = 0f;
			shadowSealingExtend = 0f;
			shadowSealingExtendArrow = 0f;
			distanceRateFromAvoid = 0f;
			distanceRateIai = 0f;
			lockOnTimeRate = 0f;
			dragonArmorDamageRate = 0f;
			burstReloadActionSpeed = 0f;
			additionalMaxBulletCnt = 0;
			poisonDamageDownRate = 0f;
			poisonGuardWeight = 0;
			burnDamageDownRate = 0f;
			burnGuardWeight = 0;
			justGuardExtendRate = 0f;
			paralyzeGuardWeight = 0;
			silenceGuardWeight = 0;
			if (!firstInitialized)
			{
				narrowEscape = 0;
			}
			enableNarrowEscape = false;
			abilityAtkList.Clear();
			for (int k = 0; k < 4; k++)
			{
				badStatusUp[k] = 0f;
			}
			for (int l = 0; l < 4; l++)
			{
				badStatusRateUp[l] = 0f;
			}
			for (int m = 0; m < 14; m++)
			{
				tolerance[m] = 100;
			}
			breakGhostFormCounter = 0;
			fieldBuffResist.Clear();
			fieldBuffResistByType.Clear();
			conditionsAbilityList.Clear();
		}
	}

	public class BuffData
	{
		public int conditionIndex = -1;

		public BUFFTYPE type;

		public bool enable;

		[FormerlySerializedAs("time")]
		private XorFloat _time = 0f;

		[FormerlySerializedAs("value")]
		private XorInt _value = 0;

		[FormerlySerializedAs("interval")]
		private XorFloat _interval = 0f;

		[FormerlySerializedAs("progress")]
		private XorFloat _progress = 0f;

		public int avoidCount;

		public int damage;

		public VALUE_TYPE valueType;

		public int fromObjectID;

		public int fromEquipIndex = -1;

		public int fromSkillIndex = -1;

		public uint fromFieldBuffID;

		public bool sync = true;

		public bool isPlayLoopEffect = true;

		public bool? endless;

		public bool isCallBuffEnd = true;

		public uint skillId;

		public bool isOwnerEnemyBuffStart;

		public float time
		{
			get
			{
				return _time;
			}
			set
			{
				_time = value;
			}
		}

		public int value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
			}
		}

		public float interval
		{
			get
			{
				return _interval;
			}
			set
			{
				_interval = value;
			}
		}

		public float progress
		{
			get
			{
				return _progress;
			}
			set
			{
				_progress = value;
			}
		}

		public bool EndBuff()
		{
			if (!enable)
			{
				return false;
			}
			enable = false;
			time = 0f;
			value = 0;
			interval = 0f;
			progress = 0f;
			avoidCount = 0;
			damage = 0;
			valueType = VALUE_TYPE.NONE;
			fromObjectID = 0;
			fromEquipIndex = -1;
			fromSkillIndex = -1;
			fromFieldBuffID = 0u;
			conditionIndex = -1;
			sync = true;
			endless = null;
			skillId = 0u;
			isOwnerEnemyBuffStart = false;
			return true;
		}

		public void SetBuffData(BuffSyncData syncData)
		{
			time = syncData.time;
			value = syncData.value;
			valueType = (VALUE_TYPE)syncData.valueType;
			sync = false;
			conditionIndex = syncData.conditionIndex;
			fromObjectID = syncData.fromObjectID;
			fromEquipIndex = syncData.fromEquipIndex;
			fromSkillIndex = syncData.fromSkillIndex;
			if (string.IsNullOrEmpty(syncData.endless))
			{
				endless = null;
			}
			else
			{
				bool result = false;
				bool.TryParse(syncData.endless, out result);
				endless = result;
			}
			skillId = (uint)syncData.skillId;
			isOwnerEnemyBuffStart = syncData.isOwnerEnemyBuffStart;
		}

		public bool isSkillChargeType()
		{
			switch (type)
			{
			case BUFFTYPE.SKILL_CHARGE:
			case BUFFTYPE.SKILL_CHARGE_RATE:
			case BUFFTYPE.SKILL_CHARGE_FIRE:
			case BUFFTYPE.SKILL_CHARGE_WATER:
			case BUFFTYPE.SKILL_CHARGE_THUNDER:
			case BUFFTYPE.SKILL_CHARGE_SOIL:
			case BUFFTYPE.SKILL_CHARGE_LIGHT:
			case BUFFTYPE.SKILL_CHARGE_DARK:
				return true;
			default:
				return false;
			}
		}

		public bool IsAbsorbType()
		{
			switch (type)
			{
			case BUFFTYPE.ABSORB_NORMAL:
			case BUFFTYPE.ABSORB_FIRE:
			case BUFFTYPE.ABSORB_WATER:
			case BUFFTYPE.ABSORB_THUNDER:
			case BUFFTYPE.ABSORB_SOIL:
			case BUFFTYPE.ABSORB_LIGHT:
			case BUFFTYPE.ABSORB_DARK:
			case BUFFTYPE.ABSORB_ALL_ELEMENT:
				return true;
			default:
				return false;
			}
		}

		public bool IsInvinsibleType()
		{
			switch (type)
			{
			case BUFFTYPE.INVINCIBLE_NORMAL:
			case BUFFTYPE.INVINCIBLE_FIRE:
			case BUFFTYPE.INVINCIBLE_WATER:
			case BUFFTYPE.INVINCIBLE_THUNDER:
			case BUFFTYPE.INVINCIBLE_SOIL:
			case BUFFTYPE.INVINCIBLE_ALL:
			case BUFFTYPE.INVINCIBLE_LIGHT:
			case BUFFTYPE.INVINCIBLE_DARK:
				return true;
			default:
				return false;
			}
		}
	}

	public class EffectInfo
	{
		public GameObject effect;

		public EffectPlayProcessor.EffectSetting setting;

		public List<BuffData> linkData = new List<BuffData>();
	}

	[Serializable]
	public class BuffSyncData
	{
		public int type;

		public float time;

		public int value;

		public int valueType;

		public int conditionIndex;

		public int fromObjectID;

		public int fromEquipIndex = -1;

		public int fromSkillIndex = -1;

		public string endless = string.Empty;

		public int skillId;

		public bool isOwnerEnemyBuffStart;

		public override string ToString()
		{
			string empty = string.Empty;
			empty += type;
			empty = empty + "," + time;
			empty = empty + "," + value;
			empty = empty + "," + valueType;
			empty = empty + "," + conditionIndex;
			empty = empty + "," + fromObjectID;
			empty = empty + "," + fromEquipIndex;
			empty = empty + "," + fromSkillIndex;
			empty = empty + "," + endless;
			empty = empty + "," + skillId;
			return base.ToString() + empty;
		}
	}

	[Serializable]
	public class BuffSyncParam
	{
		public List<BuffSyncData> buffDatas = new List<BuffSyncData>();

		public int shieldHp;

		public override string ToString()
		{
			string str = string.Empty;
			if (buffDatas != null)
			{
				str += "b[";
				buffDatas.ForEach(delegate(BuffSyncData b)
				{
					string text2 = str;
					str = text2 + "(" + b + "),";
				});
				str += "],";
			}
			string text = str;
			str = text + "shield=" + shieldHp + ",";
			return base.ToString() + str;
		}
	}

	private const float REGENERATE_INTERVAL = 2f;

	public const float ENEMY_POISON_INTERVAL = 5f;

	public const float PLAYER_POISON_INTERVAL = 2f;

	public const float ENEMY_POISON_TIME = 20f;

	public const float PLAYER_POISON_BASE_VALUE = 0.02f;

	public const float ENEMY_POISON_BASE_VALUE = 0.015f;

	public const float PLAYER_DEADLY_POISON_BASE_VALUE = 0.1f;

	public const float PLAYER_BURNING_INTERVAL = 1f;

	public const float PLAYER_BURNING_BASE_VALUE = 0.03f;

	public const int PLAYER_MOVE_SPEED_DOWN_VALUE = 50;

	private const float BURNING_AVOIDCOUNT = 3f;

	private const float AVOID_UP_RATE_MIN = 0.2f;

	private const float MOVE_SPEED_UP_RATE_MIN = 0.2f;

	private const float ABSORB_UP_RATE_MIN = 0f;

	private const float DEBUFF_DEFAULT_SLIDE_TIME = 20f;

	private const float DEBUFF_DEFAULT_SILENCE_TIME = 20f;

	private const float DEBUFF_DEFAULT_CANTHEALHP_TIME = 20f;

	private const float DEBUFF_DEFAULT_BLIND_TIME = 10f;

	public const int PLAYER_DEFAULT_ATTACK_SPEED_DOWN_VALUE = 50;

	private const float DEBUFF_DEFAULT_ATTACK_SPEED_DOWN_TIME = 10f;

	public PassiveBuff passive = new PassiveBuff();

	protected BuffData[] data;

	protected Character chara;

	protected Player player;

	protected int[] counterAttackCountList = new int[7];

	protected Dictionary<BUFFTYPE, BuffData> fieldData = new Dictionary<BUFFTYPE, BuffData>(192);

	public EvolveController ownerEvolveCtrl;

	private Animator invincibleCountAnimator;

	private Animator invincibleBadStatusAnimator;

	private bool shouldSync;

	public List<EffectInfo> loopEffect = new List<EffectInfo>();

	private uint oldAbilityId;

	private List<int> executedStackOrder = new List<int>();

	public BuffParam(Character _chara)
	{
		data = new BuffData[192];
		for (int i = 0; i < 192; i++)
		{
			data[i] = new BuffData();
			data[i].type = (BUFFTYPE)i;
		}
		fieldData.Clear();
		chara = _chara;
		player = (_chara as Player);
		passive.Reset();
	}

	public void Update()
	{
		bool flag = chara.IsCoopNone() || chara.IsOriginal();
		bool flag2 = chara.isProgressStop();
		bool flag3 = false;
		for (int i = 0; i < 192; i++)
		{
			if (data[i].enable)
			{
				if (data[i].endless.HasValue && !data[i].endless.Value)
				{
					data[i].time -= Time.deltaTime;
				}
				if (flag && !flag2)
				{
					if (data[i].interval > 0f)
					{
						data[i].progress += Time.deltaTime;
						if (data[i].interval < data[i].progress)
						{
							chara.OnBuffRoutine(data[i], false);
							data[i].progress -= data[i].interval;
						}
					}
					bool flag4 = false;
					if (data[i].endless.HasValue && data[i].endless.Value)
					{
						switch (data[i].type)
						{
						case BUFFTYPE.SHIELD:
						case BUFFTYPE.SHIELD_SUPER_ARMOR:
							if ((UnityEngine.Object)player != (UnityEngine.Object)null && (int)player.ShieldHp <= 0)
							{
								flag4 = true;
							}
							break;
						default:
							Log.Error("'{0}' is endless, but not defined end condition.", data[i].type);
							break;
						case BUFFTYPE.INVINCIBLECOUNT:
						case BUFFTYPE.MAD_MODE:
						case BUFFTYPE.AUTO_REVIVE:
						case BUFFTYPE.INVINCIBLE_BADSTATUS:
							break;
						}
					}
					else if (data[i].time <= 0f)
					{
						flag4 = true;
					}
					if (flag4 && chara.OnBuffEnd((BUFFTYPE)i, false, true))
					{
						flag3 = true;
					}
				}
			}
		}
		if (flag3)
		{
			chara.SendBuffSync(BUFFTYPE.NONE);
		}
	}

	public void OnAvoid()
	{
		BuffData buffData = data[27];
		if (buffData.value > 0)
		{
			buffData.avoidCount++;
			if ((chara.IsCoopNone() || chara.IsOriginal()) && (float)buffData.avoidCount >= 3f)
			{
				chara.OnBuffEnd(BUFFTYPE.BURNING, true, true);
			}
		}
	}

	public void ReduceInkSplashTime(float dt)
	{
		BuffData buffData = data[35];
		if (buffData.value > 0)
		{
			buffData.time -= dt;
		}
	}

	public void ResetInterval(BUFFTYPE type)
	{
		data[(int)type].interval = 0f;
		data[(int)type].progress = 0f;
	}

	public void DecreaseInvincibleCount()
	{
		BuffData buffData = data[21];
		if (buffData.value > 0 && (chara.IsCoopNone() || chara.IsOriginal()) && buffData.interval <= 0f)
		{
			buffData.value--;
			if (buffData.value <= 0)
			{
				chara.OnBuffEnd(BUFFTYPE.INVINCIBLECOUNT, true, true);
				chara.isUseInvincibleBuff = true;
			}
			else if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
			{
				buffData.interval = MonoBehaviourSingleton<InGameSettingsManager>.I.buff.invincibleInterval;
			}
			if ((UnityEngine.Object)invincibleCountAnimator != (UnityEngine.Object)null)
			{
				invincibleCountAnimator.SetTrigger("Hit");
			}
		}
	}

	public void DecreaseInvincibleBadStatus()
	{
		BuffData buffData = data[182];
		if (buffData.value > 0 && (chara.IsCoopNone() || chara.IsOriginal()) && buffData.interval <= 0f)
		{
			buffData.value--;
			if (buffData.value <= 0)
			{
				chara.OnBuffEnd(BUFFTYPE.INVINCIBLE_BADSTATUS, true, true);
				chara.isUseInvincibleBadStatusBuff = true;
			}
			else if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
			{
				buffData.interval = MonoBehaviourSingleton<InGameSettingsManager>.I.buff.invincibleInterval;
			}
			if ((UnityEngine.Object)invincibleBadStatusAnimator != (UnityEngine.Object)null)
			{
				invincibleBadStatusAnimator.SetTrigger("Hit");
			}
		}
	}

	public int GetValue(BUFFTYPE type, bool addFieldBuff = true)
	{
		if (type == BUFFTYPE.NONE || type >= BUFFTYPE.MAX)
		{
			return 0;
		}
		int num = data[(int)type].value;
		if (addFieldBuff)
		{
			num += GetFieldBuffValue(type);
		}
		if (!object.ReferenceEquals(ownerEvolveCtrl, null))
		{
			num += ownerEvolveCtrl.GetExecBuffValue(type);
		}
		return num;
	}

	public bool IsValidBuffOrFieldBuff(BUFFTYPE type, bool isFieldBuff)
	{
		return (!isFieldBuff) ? IsValidBuff(type) : IsValidFieldBuff(type);
	}

	public bool IsValidBuff(BUFFTYPE type)
	{
		return GetValue(type, false) > 0;
	}

	public bool IsValidFieldBuff(BUFFTYPE type)
	{
		return GetFieldBuffValue(type) > 0;
	}

	public bool IsEnableBuff(BUFFTYPE type)
	{
		if (type == BUFFTYPE.NONE || type >= BUFFTYPE.MAX)
		{
			return false;
		}
		return data[(int)type].enable;
	}

	public float GetMoveSpeed()
	{
		int num = GetValue(BUFFTYPE.MOVE_SPEED_UP, true) - GetValue(BUFFTYPE.MOVE_SPEED_DOWN, true);
		float num2 = 1f + passive.moveSpeedUp + (float)num * 0.01f;
		if (!object.ReferenceEquals(player, null) && player.isBoostMode)
		{
			num2 += passive.boostMoveSpeedUp + (float)GetValue(BUFFTYPE.BOOST_MOVE_SPEED_UP, true) * 0.01f;
		}
		if (num2 < 0.2f)
		{
			num2 = 0.2f;
		}
		return num2;
	}

	public float GetAtkSpeed()
	{
		int num = GetValue(BUFFTYPE.ATTACK_SPEED_UP, true) - GetValue(BUFFTYPE.ATTACK_SPEED_DOWN, true);
		float num2 = 1f + passive.attackSpeedUp + (float)num * 0.01f;
		if (!object.ReferenceEquals(player, null) && player.isBoostMode)
		{
			num2 += passive.boostAttackSpeedUp + (float)GetValue(BUFFTYPE.BOOST_ATTACK_SPEED_UP, true) * 0.01f;
		}
		return num2;
	}

	public float GetHealSpeedUp()
	{
		return 1f + passive.hpHealSpeedUp + (float)GetValue(BUFFTYPE.HP_HEAL_SPEEDUP, true) * 0.01f;
	}

	public float GetGuardUp()
	{
		return 1f - passive.guardUp;
	}

	public float GetSkillAbsorbUp(ELEMENT_TYPE eType = ELEMENT_TYPE.MAX)
	{
		float num = 1f + passive.skillAbsorbUp + (float)GetValue(BUFFTYPE.SKILL_ABSORBUP, true) * 0.01f;
		switch (eType)
		{
		case ELEMENT_TYPE.FIRE:
			num += passive.skillAbsorbUp_OnlyAttackAndElement.fire;
			num += (float)GetValue(BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_FIRE, true) * 0.01f;
			break;
		case ELEMENT_TYPE.WATER:
			num += passive.skillAbsorbUp_OnlyAttackAndElement.water;
			num += (float)GetValue(BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_WATER, true) * 0.01f;
			break;
		case ELEMENT_TYPE.THUNDER:
			num += passive.skillAbsorbUp_OnlyAttackAndElement.thunder;
			num += (float)GetValue(BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_THUNDER, true) * 0.01f;
			break;
		case ELEMENT_TYPE.SOIL:
			num += passive.skillAbsorbUp_OnlyAttackAndElement.soil;
			num += (float)GetValue(BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_SOIL, true) * 0.01f;
			break;
		case ELEMENT_TYPE.LIGHT:
			num += passive.skillAbsorbUp_OnlyAttackAndElement.light;
			num += (float)GetValue(BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_LIGHT, true) * 0.01f;
			break;
		case ELEMENT_TYPE.DARK:
			num += passive.skillAbsorbUp_OnlyAttackAndElement.dark;
			num += (float)GetValue(BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_DARK, true) * 0.01f;
			break;
		}
		if (num < 0f)
		{
			num = 0f;
		}
		return num;
	}

	public float GetSkillAbsorbUpByElementList(ELEMENT_TYPE[] eTypes)
	{
		float num = 1f + passive.skillAbsorbUp + (float)GetValue(BUFFTYPE.SKILL_ABSORBUP, true) * 0.01f;
		if (eTypes != null && eTypes.Length > 0)
		{
			for (int i = 0; i < eTypes.Length; i++)
			{
				switch (eTypes[i])
				{
				case ELEMENT_TYPE.FIRE:
					num += passive.skillAbsorbUp_OnlyAttackAndElement.fire;
					num += (float)GetValue(BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_FIRE, true) * 0.01f;
					break;
				case ELEMENT_TYPE.WATER:
					num += passive.skillAbsorbUp_OnlyAttackAndElement.water;
					num += (float)GetValue(BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_WATER, true) * 0.01f;
					break;
				case ELEMENT_TYPE.THUNDER:
					num += passive.skillAbsorbUp_OnlyAttackAndElement.thunder;
					num += (float)GetValue(BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_THUNDER, true) * 0.01f;
					break;
				case ELEMENT_TYPE.SOIL:
					num += passive.skillAbsorbUp_OnlyAttackAndElement.soil;
					num += (float)GetValue(BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_SOIL, true) * 0.01f;
					break;
				case ELEMENT_TYPE.LIGHT:
					num += passive.skillAbsorbUp_OnlyAttackAndElement.light;
					num += (float)GetValue(BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_LIGHT, true) * 0.01f;
					break;
				case ELEMENT_TYPE.DARK:
					num += passive.skillAbsorbUp_OnlyAttackAndElement.dark;
					num += (float)GetValue(BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_DARK, true) * 0.01f;
					break;
				}
			}
		}
		if (num < 0f)
		{
			num = 0f;
		}
		return num;
	}

	public float GetSkillHealSpeedUp()
	{
		return 1f + passive.skillHealSpeedUp + (float)GetValue(BUFFTYPE.SKILL_HEAL_SPEEDUP, true) * 0.01f;
	}

	public float GetAvoidUp()
	{
		float num = 1f + passive.avoidUp;
		if (GetValue(BUFFTYPE.MOVE_SPEED_DOWN, true) > 0)
		{
			num -= (float)GetValue(BUFFTYPE.MOVE_SPEED_DOWN, true) * 0.01f;
		}
		if (!object.ReferenceEquals(player, null) && player.isBoostMode)
		{
			num += passive.boostAvoidUp + (float)GetValue(BUFFTYPE.BOOST_AVOID_UP, true) * 0.01f;
		}
		if (num < 0.2f)
		{
			num = 0.2f;
		}
		return num;
	}

	public float GetHealUp()
	{
		float num = 1f + passive.healUP;
		if (num < 0f)
		{
			num = 0f;
		}
		return num;
	}

	public float GetHealHpRate()
	{
		return passive.healUP;
	}

	public float GetHealUpDependsWeaponRate()
	{
		return passive.healUpDependsWeapon;
	}

	public float GetParalyzeTime()
	{
		return 7f * (float)passive.tolerance[0] * 0.01f;
	}

	public float GetPoisonTime()
	{
		return 20f * (float)passive.tolerance[1] * 0.01f;
	}

	public float GetBurningTime()
	{
		return 5f * (float)passive.tolerance[2] * 0.01f;
	}

	public float GetSpeedDownTime()
	{
		return 10f * (float)passive.tolerance[3] * 0.01f;
	}

	public float GetAttackSpeedDownTime()
	{
		float num = 10f;
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			num = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.attackSpeedDownParam.duration;
		}
		return num * (float)passive.tolerance[11] * 0.01f;
	}

	public float GetDeadlyPoisonTime()
	{
		float num = 20f;
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			num = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.deadlyPosion.duration;
		}
		return num * (float)passive.tolerance[1] * 0.01f;
	}

	public float GetSlideTime()
	{
		float num = 20f;
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			num = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.slideParam.duration;
		}
		return num * (float)passive.tolerance[9] * 0.01f;
	}

	public float GetSilenceTime()
	{
		float num = 20f;
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			num = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.silenceParam.duration;
		}
		return num * (float)passive.tolerance[10] * 0.01f;
	}

	public float GetCantHealHpTime()
	{
		float num = 20f;
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			num = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.cantHealHpParam.duration;
		}
		return num * (float)passive.tolerance[12] * 0.01f;
	}

	public float GetBlindTime()
	{
		float num = 10f;
		if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
		{
			num = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.blindParam.duration;
		}
		return num * (float)passive.tolerance[13] * 0.01f;
	}

	public float GetBleedUp()
	{
		float num = 1f + passive.bleedUp;
		if (num < 0f)
		{
			num = 0f;
		}
		return num;
	}

	public float GetChargeSwordsTimeRate()
	{
		return passive.chargeSwordsTimeRate;
	}

	public float GetChargeArrowTimeRate()
	{
		return passive.chargeArrowTimeRate;
	}

	public float GetChargeHeatArrowTimeRate()
	{
		return passive.chargeHeatArrowTimeRate;
	}

	public float GetChargePairSwordsTimeRate()
	{
		return passive.chargePairSwordsTimeRate;
	}

	public float GetSpearRushDistanceRate()
	{
		return passive.spearRushDistanceRate;
	}

	public float GetDragonArmorDamageRate()
	{
		return passive.dragonArmorDamageRate;
	}

	public float GetChargeSpearTimeRate()
	{
		return passive.chargeSpearTimeRate;
	}

	public float GetSkillTimeRate()
	{
		return passive.skillTimeRate;
	}

	public float GetStumbleTime(float time)
	{
		return time * (float)passive.tolerance[4] * 0.01f;
	}

	public float GetShakeTime(float time)
	{
		return time * (float)passive.tolerance[5] * 0.01f;
	}

	public bool IsHalfReaction(TOLERANCETYPE type)
	{
		return passive.tolerance[(int)type] < 100 && !IsInvalidReaction(type);
	}

	public bool IsInvalidReaction(TOLERANCETYPE type)
	{
		return passive.tolerance[(int)type] <= 0;
	}

	public float GetDamageDownRate()
	{
		float num = 1f - (passive.damageDown + (float)GetValue(BUFFTYPE.DAMAGE_DOWN, true) * 0.01f);
		if (!object.ReferenceEquals(player, null) && player.isBoostMode)
		{
			num -= passive.boostDamageDown + (float)GetValue(BUFFTYPE.BOOST_DAMAGE_DOWN, true) * 0.01f;
		}
		float num2 = 1f - ((!MonoBehaviourSingleton<InGameSettingsManager>.IsValid()) ? 0.6f : MonoBehaviourSingleton<InGameSettingsManager>.I.player.maxDamageDownRate);
		if (num < num2)
		{
			num = num2;
		}
		return num;
	}

	public float GetGaugeIncreaseRate(SP_ATTACK_TYPE type)
	{
		float num = passive.gaugeIncreaseRate;
		switch (type)
		{
		case SP_ATTACK_TYPE.HEAT:
			num += passive.heatGaugeIncreaseRate;
			break;
		case SP_ATTACK_TYPE.SOUL:
			num += passive.soulGaugeIncreaseRate;
			break;
		}
		if (num < -1f)
		{
			num = -1f;
		}
		return num;
	}

	public float GetGaugeDecreaseRate()
	{
		float num = passive.gaugeDecreaseRate;
		if (num < -1f)
		{
			num = -1f;
		}
		return num;
	}

	public float GetSubGaugeIncreaseRate()
	{
		float num = passive.subGaugeIncreaseRate;
		if (num < -1f)
		{
			num = -1f;
		}
		return num;
	}

	public float GetHeatGaugeIncreaseRate()
	{
		return passive.heatGaugeIncreaseRate;
	}

	public float GetSoulGaugeIncreaseRate()
	{
		return passive.soulGaugeIncreaseRate;
	}

	public float GetSoulChargeTimeRate()
	{
		return passive.soulChargeTimeRate;
	}

	public float GetShadowSealingExtend()
	{
		return 1f + passive.shadowSealingExtend;
	}

	public float GetShadowSealingExtendArrow()
	{
		return 1f + passive.shadowSealingExtendArrow;
	}

	public float GetDistanceRateFromAvoid()
	{
		return 1f + passive.distanceRateFromAvoid;
	}

	public float GetDistanceRateIai()
	{
		return 1f + passive.distanceRateIai + (float)GetValue(BUFFTYPE.DISTANCE_UP_IAI, true) * 0.01f;
	}

	public float GetLockOnTimeRate()
	{
		return 1f + passive.lockOnTimeRate;
	}

	public bool IsNarrowEscape()
	{
		if (passive.narrowEscape > 0 && passive.enableNarrowEscape)
		{
			return true;
		}
		return false;
	}

	public void UseNarrowEscape()
	{
		passive.narrowEscape--;
	}

	public float GetDamageUpRate(Player player, AttackedHitStatusLocal status)
	{
		int num = GetValue(BUFFTYPE.DAMAGE_UP, true);
		int value = GetValue(BUFFTYPE.BOOST_DAMAGE_UP, true);
		if (value > 0 && (UnityEngine.Object)player != (UnityEngine.Object)null && player.isBoostMode)
		{
			num += value;
		}
		if (status.attackInfo.isSkillReference)
		{
			return (float)num * 0.01f;
		}
		value = GetValue(BUFFTYPE.DAMAGE_UP_NORMAL, true);
		if (value > 0 && InGameUtility.IsDamageUpAtkTypeNormal(player, status.attackInfo))
		{
			num += value;
		}
		value = GetValue(BUFFTYPE.DAMAGE_UP_FROM_AVOID, true);
		if (value > 0 && status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.FROM_AVOID)
		{
			num += value;
		}
		return (float)num * 0.01f;
	}

	public AtkAttribute GetAbilityDamageRate(Character chara, AttackedHitStatusLocal status)
	{
		AtkAttribute attr = new AtkAttribute();
		attr.Set(1f);
		passive.abilityAtkList.ForEach(delegate(AbilityAtkBase data)
		{
			AtkAttribute damageRate = data.GetDamageRate(chara, status);
			if (damageRate != null)
			{
				attr.Add(damageRate);
			}
		});
		return attr;
	}

	public AtkAttribute GetBuffAtkRate()
	{
		AtkAttribute atkAttribute = new AtkAttribute();
		atkAttribute.normal = (float)GetValue(BUFFTYPE.ATKUP_RATE_NORMAL, true) * 0.01f;
		atkAttribute.fire = (float)GetValue(BUFFTYPE.ATKUP_RATE_FIRE, true) * 0.01f;
		atkAttribute.water = (float)GetValue(BUFFTYPE.ATKUP_RATE_WATER, true) * 0.01f;
		atkAttribute.thunder = (float)GetValue(BUFFTYPE.ATKUP_RATE_THUNDER, true) * 0.01f;
		atkAttribute.soil = (float)GetValue(BUFFTYPE.ATKUP_RATE_SOIL, true) * 0.01f;
		atkAttribute.light = (float)GetValue(BUFFTYPE.ATKUP_RATE_LIGHT, true) * 0.01f;
		atkAttribute.dark = (float)GetValue(BUFFTYPE.ATKUP_RATE_DARK, true) * 0.01f;
		atkAttribute.AddElementOnly((float)GetValue(BUFFTYPE.ATKUP_RATE_ALLELEMENT, true) * 0.01f);
		return atkAttribute;
	}

	public AtkAttribute GetBuffDefenceRate()
	{
		AtkAttribute atkAttribute = new AtkAttribute();
		atkAttribute.Set(0f);
		atkAttribute.normal = (float)GetValue(BUFFTYPE.DEFUP_RATE_NORMAL, true) * 0.01f;
		atkAttribute.fire = (float)GetValue(BUFFTYPE.DEFUP_RATE_FIRE, true) * 0.01f;
		atkAttribute.water = (float)GetValue(BUFFTYPE.DEFUP_RATE_WATER, true) * 0.01f;
		atkAttribute.thunder = (float)GetValue(BUFFTYPE.DEFUP_RATE_THUNDER, true) * 0.01f;
		atkAttribute.soil = (float)GetValue(BUFFTYPE.DEFUP_RATE_SOIL, true) * 0.01f;
		atkAttribute.light = (float)GetValue(BUFFTYPE.DEFUP_RATE_LIGHT, true) * 0.01f;
		atkAttribute.dark = (float)GetValue(BUFFTYPE.DEFUP_RATE_DARK, true) * 0.01f;
		atkAttribute.AddElementOnly((float)GetValue(BUFFTYPE.DEFUP_RATE_ALLELEMENT, true) * 0.01f);
		return atkAttribute;
	}

	public AtkAttribute GetBuffToleranceRate()
	{
		AtkAttribute atkAttribute = new AtkAttribute();
		atkAttribute.Set(0f);
		atkAttribute.fire = (float)GetValue(BUFFTYPE.TOLUP_RATE_FIRE, true) * 0.01f;
		atkAttribute.water = (float)GetValue(BUFFTYPE.TOLUP_RATE_WATER, true) * 0.01f;
		atkAttribute.thunder = (float)GetValue(BUFFTYPE.TOLUP_RATE_THUNDER, true) * 0.01f;
		atkAttribute.soil = (float)GetValue(BUFFTYPE.TOLUP_RATE_SOIL, true) * 0.01f;
		atkAttribute.light = (float)GetValue(BUFFTYPE.TOLUP_RATE_LIGHT, true) * 0.01f;
		atkAttribute.dark = (float)GetValue(BUFFTYPE.TOLUP_RATE_DARK, true) * 0.01f;
		atkAttribute.AddElementOnly((float)GetValue(BUFFTYPE.TOLUP_RATE_ALLELEMENT, true) * 0.01f);
		return atkAttribute;
	}

	public AtkAttribute GetBuffAtkConstant()
	{
		AtkAttribute atkAttribute = new AtkAttribute();
		atkAttribute.Set(0f);
		atkAttribute.normal = (float)GetValue(BUFFTYPE.ATTACK_NORMAL, true);
		atkAttribute.fire = (float)GetValue(BUFFTYPE.ATTACK_FIRE, true);
		atkAttribute.water = (float)GetValue(BUFFTYPE.ATTACK_WATER, true);
		atkAttribute.thunder = (float)GetValue(BUFFTYPE.ATTACK_THUNDER, true);
		atkAttribute.soil = (float)GetValue(BUFFTYPE.ATTACK_SOIL, true);
		atkAttribute.light = (float)GetValue(BUFFTYPE.ATTACK_LIGHT, true);
		atkAttribute.dark = (float)GetValue(BUFFTYPE.ATTACK_DARK, true);
		return atkAttribute;
	}

	public float GetPassiveHpRate()
	{
		return passive.hpUpRate - passive.hpDownRate;
	}

	public AtkAttribute GetPassiveAtkRate()
	{
		AtkAttribute atkAttribute = new AtkAttribute();
		atkAttribute.Set(0f);
		atkAttribute.Add(passive.atkUpRate);
		atkAttribute.Sub(passive.atkDownRate);
		return atkAttribute;
	}

	public AtkAttribute GetPassiveAtkUpConstant()
	{
		AtkAttribute atkAttribute = new AtkAttribute();
		atkAttribute.Set(0f);
		atkAttribute.normal = (float)passive.atkList[0];
		atkAttribute.fire = (float)passive.atkList[1];
		atkAttribute.water = (float)passive.atkList[2];
		atkAttribute.thunder = (float)passive.atkList[3];
		atkAttribute.soil = (float)passive.atkList[4];
		atkAttribute.light = (float)passive.atkList[5];
		atkAttribute.dark = (float)passive.atkList[6];
		return atkAttribute;
	}

	public AtkAttribute GetEquipAtkUpConstant(List<int> baseStateAtkList)
	{
		AtkAttribute atkAttribute = new AtkAttribute();
		atkAttribute.Set(0f);
		atkAttribute.normal = (float)baseStateAtkList[0];
		atkAttribute.fire = (float)baseStateAtkList[1];
		atkAttribute.water = (float)baseStateAtkList[2];
		atkAttribute.thunder = (float)baseStateAtkList[3];
		atkAttribute.soil = (float)baseStateAtkList[4];
		atkAttribute.light = (float)baseStateAtkList[5];
		atkAttribute.dark = (float)baseStateAtkList[6];
		return atkAttribute;
	}

	public float GetBadStatusUp(BAD_STATUS_UP type)
	{
		if (type == BAD_STATUS_UP.MAX)
		{
			return 0f;
		}
		return passive.badStatusUp[(int)type];
	}

	public float GetBadStatusRateUp(BAD_STATUS_UP type)
	{
		if (type == BAD_STATUS_UP.MAX)
		{
			return 1f;
		}
		return 1f + passive.badStatusRateUp[(int)type];
	}

	public float GetPoisonDamageDownRate()
	{
		return passive.poisonDamageDownRate + (float)GetValue(BUFFTYPE.POISON_DAMAGE_DOWN, true) * 0.01f;
	}

	public float GetBurnDamageDownRate()
	{
		return passive.burnDamageDownRate + (float)GetValue(BUFFTYPE.BURN_DAMAGE_DOWN, true) * 0.01f;
	}

	private int GetPoisonGuardWeight()
	{
		return GetValue(BUFFTYPE.POISON_GUARD, true) + passive.poisonGuardWeight;
	}

	private int GetBurnGuardWeight()
	{
		return GetValue(BUFFTYPE.BURN_GUARD, true) + passive.burnGuardWeight;
	}

	private int GetParalyzeGuardWeight()
	{
		return GetValue(BUFFTYPE.PARALYZE_GUARD, true) + passive.paralyzeGuardWeight;
	}

	private int GetSilenceGuardWeight()
	{
		return GetValue(BUFFTYPE.SILENCE_GUARD, true) + passive.silenceGuardWeight;
	}

	private bool LotteryBadStatusGuard(BUFFTYPE targetType)
	{
		int num = 0;
		switch (targetType)
		{
		case BUFFTYPE.POISON:
			num = GetPoisonGuardWeight();
			break;
		case BUFFTYPE.DEADLY_POISON:
			num = GetPoisonGuardWeight();
			break;
		case BUFFTYPE.BURNING:
			num = GetBurnGuardWeight();
			break;
		case BUFFTYPE.ATTACK_PARALYZE:
			num = GetParalyzeGuardWeight();
			break;
		case BUFFTYPE.SILENCE:
			num = GetSilenceGuardWeight();
			break;
		}
		if (num > 0)
		{
			int num2 = UnityEngine.Random.Range(0, 100);
			if (num2 <= num)
			{
				return true;
			}
		}
		return false;
	}

	public void ApplyBadStatusGuard(ref BadStatus targetBadStatus)
	{
		if (LotteryBadStatusGuard(BUFFTYPE.ATTACK_PARALYZE))
		{
			targetBadStatus.paralyze = 0f;
		}
		if (LotteryBadStatusGuard(BUFFTYPE.POISON))
		{
			targetBadStatus.poison = 0f;
		}
		if (LotteryBadStatusGuard(BUFFTYPE.DEADLY_POISON))
		{
			targetBadStatus.deadlyPoison = 0f;
		}
		if (LotteryBadStatusGuard(BUFFTYPE.BURNING))
		{
			targetBadStatus.burning = 0f;
		}
		if (LotteryBadStatusGuard(BUFFTYPE.SILENCE))
		{
			targetBadStatus.silence = 0f;
		}
		if (LotteryBadStatusGuard(BUFFTYPE.CANT_HEAL_HP))
		{
			targetBadStatus.cantHealHp = 0f;
		}
		if (LotteryBadStatusGuard(BUFFTYPE.BLIND))
		{
			targetBadStatus.blind = 0f;
		}
	}

	public float GetJustGuardExtendRate()
	{
		return 1f + passive.justGuardExtendRate;
	}

	public List<BuffData> GetAbsorbBuffDataList()
	{
		List<BuffData> list = new List<BuffData>();
		for (int i = 144; i <= 151; i++)
		{
			if (data[i].IsAbsorbType() && IsValidBuff(data[i].type))
			{
				list.Add(data[i]);
			}
		}
		return list;
	}

	public List<BuffData> GetInvincibleBuffDataList()
	{
		List<BuffData> list = new List<BuffData>();
		for (int i = 164; i <= 169; i++)
		{
			if (data[i].IsInvinsibleType() && IsValidBuff(data[i].type))
			{
				list.Add(data[i]);
			}
		}
		return list;
	}

	public List<BuffData> GetHitAbsorbBuffDataList()
	{
		List<BuffData> list = new List<BuffData>();
		for (int i = 152; i <= 159; i++)
		{
			if (IsHitAbsorbType(data[i].type) && IsValidBuff(data[i].type))
			{
				list.Add(data[i]);
			}
		}
		return list;
	}

	public void SetValue(BUFFTYPE type, int value)
	{
		if (type != BUFFTYPE.NONE && type < BUFFTYPE.MAX)
		{
			data[(int)type].value = value;
		}
	}

	private bool IsCheckCurrentBuffValue(BUFFTYPE targetType)
	{
		if (targetType == BUFFTYPE.BREAK_GHOST_FORM || targetType == BUFFTYPE.SUPER_ARMOR || targetType == BUFFTYPE.SHIELD_SUPER_ARMOR)
		{
			return false;
		}
		return true;
	}

	public bool BuffStart(BuffData buffData)
	{
		BUFFTYPE type = buffData.type;
		if (type == BUFFTYPE.NONE || type >= BUFFTYPE.MAX)
		{
			return false;
		}
		if ((type == BUFFTYPE.SHIELD || type == BUFFTYPE.SHIELD_SUPER_ARMOR) && player.isDead)
		{
			return false;
		}
		if (type == BUFFTYPE.AUTO_REVIVE)
		{
			if (object.ReferenceEquals(player, null))
			{
				return false;
			}
			if (!player.IsAbleToAutoReviveBuff())
			{
				return false;
			}
		}
		if (type == BUFFTYPE.INVINCIBLECOUNT)
		{
			if (object.ReferenceEquals(player, null))
			{
				return false;
			}
			if (!player.IsAbleToInvincibleBuff())
			{
				return false;
			}
			ResetInterval(type);
		}
		if (type == BUFFTYPE.INVINCIBLE_BADSTATUS)
		{
			if (object.ReferenceEquals(player, null))
			{
				return false;
			}
			if (!player.IsAbleToInvincibleBadStatusBuff())
			{
				return false;
			}
			ResetInterval(type);
		}
		if (IsCheckCurrentBuffValue(type) && GetValue(type, false) > buffData.value)
		{
			return false;
		}
		BuffData buffData2 = data[(int)type];
		if (buffData2.enable)
		{
			if ((buffData2.type == BUFFTYPE.SHIELD || buffData2.type == BUFFTYPE.SHIELD_SUPER_ARMOR) && player.IsValidShield())
			{
				if (buffData.isPlayLoopEffect)
				{
					PlayBuffLoopEffect(buffData2);
				}
				shouldSync = true;
				return true;
			}
			if (buffData.isCallBuffEnd)
			{
				chara.OnBuffEnd(type, false, false);
			}
		}
		buffData2.value = buffData.value;
		buffData2.time = buffData.time;
		if (buffData.endless.HasValue)
		{
			buffData2.endless = buffData.endless;
		}
		else
		{
			buffData2.endless = (buffData.time < 0f);
		}
		buffData2.enable = true;
		buffData2.conditionIndex = buffData.conditionIndex;
		buffData2.interval = buffData.interval;
		buffData2.damage = buffData.damage;
		buffData2.valueType = buffData.valueType;
		buffData2.fromObjectID = buffData.fromObjectID;
		buffData2.skillId = buffData.skillId;
		switch (buffData2.type)
		{
		case BUFFTYPE.REGENERATE:
		case BUFFTYPE.REGENERATE_PROPORTION:
			if (buffData2.interval == 0f)
			{
				buffData2.interval = 2f;
			}
			break;
		case BUFFTYPE.BREAK_GHOST_FORM:
			buffData2.value = 1;
			break;
		case BUFFTYPE.SHIELD:
			if ((UnityEngine.Object)player != (UnityEngine.Object)null)
			{
				int b = (int)((float)player.hpMax * ((float)buffData.value / 100f));
				b = Mathf.Max(0, b);
				player.ShieldHp = b;
				player.ShieldHpMax = b;
				buffData2.fromEquipIndex = buffData.fromEquipIndex;
				buffData2.fromSkillIndex = buffData.fromSkillIndex;
			}
			break;
		}
		if (buffData.isPlayLoopEffect)
		{
			PlayBuffLoopEffect(buffData2);
		}
		shouldSync = true;
		return true;
	}

	public bool BuffEnd(BUFFTYPE type, bool isPlayEndEffect = true)
	{
		if (type == BUFFTYPE.NONE || type >= BUFFTYPE.MAX)
		{
			return false;
		}
		shouldSync = true;
		BuffData buffData = data[(int)type];
		if (passive != null && passive.conditionsAbilityList != null)
		{
			int conditionIndex = buffData.conditionIndex;
			if (conditionIndex >= 0 && conditionIndex < passive.conditionsAbilityList.Count)
			{
				passive.conditionsAbilityList[conditionIndex].counterAttackNum = 0;
				passive.conditionsAbilityList[conditionIndex].cleaveComboNum = 0;
			}
		}
		bool flag = buffData.EndBuff();
		if (flag)
		{
			EndBuffEffect(buffData, isPlayEndEffect);
		}
		return flag;
	}

	public void AllBuffEnd(bool sync)
	{
		for (int i = 0; i < 192; i++)
		{
			chara.OnBuffEnd((BUFFTYPE)i, false, true);
		}
		if (sync)
		{
			chara.SendBuffSync(BUFFTYPE.NONE);
		}
	}

	public void PlayBuffLoopEffectAll()
	{
		for (int i = 0; i < 192; i++)
		{
			if (data[i].enable)
			{
				PlayBuffLoopEffect(data[i]);
			}
		}
	}

	public void OnDetachServant(DisableNotifyMonoBehaviour servant)
	{
		int num = 0;
		int count = loopEffect.Count;
		while (true)
		{
			if (num >= count)
			{
				return;
			}
			if ((UnityEngine.Object)loopEffect[num].effect == (UnityEngine.Object)servant.gameObject)
			{
				break;
			}
			num++;
		}
		if ((UnityEngine.Object)loopEffect[num].effect != (UnityEngine.Object)null)
		{
			EffectManager.ReleaseEffect(loopEffect[num].effect, true, false);
		}
		loopEffect.RemoveAt(num);
	}

	public void PlayBuffLoopEffect(BuffData data)
	{
		PlayBuffEffect(data, "LOOP", true);
	}

	private void PlayBuffEffect(BuffData data, string type_key, bool loop)
	{
		if (data != null && !((UnityEngine.Object)chara.effectPlayProcessor == (UnityEngine.Object)null))
		{
			List<EffectPlayProcessor.EffectSetting> list = null;
			string text = null;
			string str = null;
			text = "BUFF_" + type_key + "_" + data.type.ToString();
			if ((UnityEngine.Object)player != (UnityEngine.Object)null && (UnityEngine.Object)player.loader != (UnityEngine.Object)null && player.loader.loadInfo != null)
			{
				str = "_PLC" + (player.loader.loadInfo.weaponModelID / 1000).ToString("D2");
				list = chara.effectPlayProcessor.GetSettings(text + str);
			}
			if (list == null)
			{
				list = chara.effectPlayProcessor.GetSettings(text);
			}
			if (list == null)
			{
				string text2 = "BUFF";
				switch (data.type)
				{
				case BUFFTYPE.MOVE_SPEED_DOWN:
				case BUFFTYPE.POISON:
				case BUFFTYPE.BURNING:
				case BUFFTYPE.DEADLY_POISON:
				case BUFFTYPE.ELECTRIC_SHOCK:
				case BUFFTYPE.INK_SPLASH:
				case BUFFTYPE.ATTACK_SPEED_DOWN:
				case BUFFTYPE.CANT_HEAL_HP:
				case BUFFTYPE.BLIND:
					text2 = "DEBUFF";
					break;
				case BUFFTYPE.MAD_MODE:
					text2 = "MADMODE";
					break;
				case BUFFTYPE.GHOST_FORM:
				case BUFFTYPE.SILENCE:
					text2 = string.Empty;
					break;
				}
				text = "BUFF_" + type_key + "_" + text2 + "_DEFAULT";
				if ((UnityEngine.Object)player != (UnityEngine.Object)null)
				{
					list = chara.effectPlayProcessor.GetSettings(text + str);
				}
			}
			if (list == null)
			{
				list = chara.effectPlayProcessor.GetSettings(text);
			}
			if (list != null)
			{
				int i = 0;
				for (int count = list.Count; i < count; i++)
				{
					EffectPlayProcessor.EffectSetting effectSetting = list[i];
					if (data.skillId != 0)
					{
						SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(data.skillId);
						if (skillItemData != null)
						{
							for (int j = 0; j < skillItemData.supportType.Length; j++)
							{
								if (skillItemData.supportType[j] == data.type && !string.IsNullOrEmpty(skillItemData.supportEffectName[j]))
								{
									EffectPlayProcessor.EffectSetting effectSetting2 = effectSetting.Clone();
									effectSetting2.effectName = skillItemData.supportEffectName[j];
									effectSetting = effectSetting2;
								}
							}
						}
					}
					if (loop)
					{
						bool flag = false;
						int k = 0;
						for (int count2 = loopEffect.Count; k < count2; k++)
						{
							EffectPlayProcessor.EffectSetting setting = loopEffect[k].setting;
							if (setting != null && setting.effectName == effectSetting.effectName && setting.nodeName == effectSetting.nodeName)
							{
								if (!loopEffect[k].linkData.Contains(data))
								{
									loopEffect[k].linkData.Add(data);
								}
								flag = true;
								break;
							}
						}
						if (flag)
						{
							continue;
						}
					}
					Transform transform = chara.effectPlayProcessor.PlayEffect(effectSetting, null);
					if ((UnityEngine.Object)transform != (UnityEngine.Object)null && loop)
					{
						DisableNotifyMonoBehaviour disableNotifyMonoBehaviour = transform.gameObject.AddComponent<DisableNotifyMonoBehaviour>();
						disableNotifyMonoBehaviour.SetNotifyMaster(chara);
						EffectInfo effectInfo = new EffectInfo();
						effectInfo.effect = transform.gameObject;
						effectInfo.setting = effectSetting;
						effectInfo.linkData.Add(data);
						loopEffect.Add(effectInfo);
						if (data.type == BUFFTYPE.SLIDE)
						{
							BuffSlideEffectController buffSlideEffectController = transform.gameObject.AddComponent<BuffSlideEffectController>();
							buffSlideEffectController.Initialize(player);
						}
						else if (data.type == BUFFTYPE.INVINCIBLECOUNT)
						{
							invincibleCountAnimator = transform.GetComponent<Animator>();
						}
						else if (data.type == BUFFTYPE.INVINCIBLE_BADSTATUS)
						{
							invincibleBadStatusAnimator = transform.GetComponent<Animator>();
						}
					}
				}
			}
		}
	}

	private void EndBuffEffect(BuffData data, bool isPlayEndEffect)
	{
		int num = 0;
		while (num < loopEffect.Count)
		{
			if (loopEffect[num].linkData.Contains(data))
			{
				loopEffect[num].linkData.Remove(data);
			}
			if (loopEffect[num].linkData.Count <= 0)
			{
				GameObject effect = loopEffect[num].effect;
				loopEffect.RemoveAt(num);
				if ((UnityEngine.Object)effect != (UnityEngine.Object)null)
				{
					if (data.type == BUFFTYPE.SLIDE)
					{
						BuffSlideEffectController component = effect.GetComponent<BuffSlideEffectController>();
						component.enabled = false;
					}
					else if (data.type == BUFFTYPE.INVINCIBLECOUNT)
					{
						invincibleCountAnimator = null;
					}
					else if (data.type == BUFFTYPE.INVINCIBLE_BADSTATUS)
					{
						invincibleBadStatusAnimator = null;
					}
					EffectManager.ReleaseEffect(effect, isPlayEndEffect, false);
				}
			}
			else
			{
				num++;
			}
		}
	}

	public BuffSyncParam CreateSyncParamIfNeeded()
	{
		if (shouldSync)
		{
			shouldSync = false;
			return CreateSyncParam(BUFFTYPE.NONE);
		}
		return null;
	}

	public BuffSyncParam CreateSyncParam(BUFFTYPE nowBuffType = BUFFTYPE.NONE)
	{
		BuffSyncParam buffSyncParam = new BuffSyncParam();
		for (int i = 0; i < 192; i++)
		{
			if (data[i].enable && !IsIgnoreSyncType(data[i].type))
			{
				BuffSyncData buffSyncData = new BuffSyncData();
				buffSyncData.type = (int)data[i].type;
				buffSyncData.time = data[i].time;
				buffSyncData.value = data[i].value;
				buffSyncData.valueType = (int)data[i].valueType;
				buffSyncData.conditionIndex = data[i].conditionIndex;
				buffSyncData.fromObjectID = data[i].fromObjectID;
				buffSyncData.fromEquipIndex = data[i].fromEquipIndex;
				buffSyncData.fromSkillIndex = data[i].fromSkillIndex;
				string endless = string.Empty;
				if (data[i].endless.HasValue)
				{
					endless = data[i].endless.ToString();
				}
				buffSyncData.endless = endless;
				buffSyncData.skillId = (int)data[i].skillId;
				buffSyncData.isOwnerEnemyBuffStart = (i == (int)nowBuffType);
				buffSyncParam.buffDatas.Add(buffSyncData);
			}
		}
		if ((UnityEngine.Object)player != (UnityEngine.Object)null)
		{
			buffSyncParam.shieldHp = player.ShieldHp;
		}
		return buffSyncParam;
	}

	private bool IsIgnoreSyncType(BUFFTYPE type)
	{
		if (type == BUFFTYPE.INK_SPLASH)
		{
			return true;
		}
		return false;
	}

	public void SetSyncParam(BuffSyncParam sync_param, bool isPlayLoopEffect = true)
	{
		BuffSyncData[] array = new BuffSyncData[192];
		int i = 0;
		for (int count = sync_param.buffDatas.Count; i < count; i++)
		{
			int type = sync_param.buffDatas[i].type;
			if (type >= 0 && type < 192)
			{
				array[type] = sync_param.buffDatas[i];
			}
		}
		for (int j = 0; j < 192; j++)
		{
			if (data[j].enable)
			{
				if (array[j] != null)
				{
					data[j].SetBuffData(array[j]);
					data[j].isCallBuffEnd = false;
					chara.OnBuffStart(data[j]);
					data[j].isCallBuffEnd = true;
				}
				else
				{
					chara.OnBuffEnd((BUFFTYPE)j, false, true);
				}
			}
			else if (array[j] != null)
			{
				BuffData buffData = new BuffData();
				buffData.type = (BUFFTYPE)j;
				buffData.isPlayLoopEffect = isPlayLoopEffect;
				buffData.SetBuffData(array[j]);
				chara.OnBuffStart(buffData);
			}
		}
		if ((UnityEngine.Object)player != (UnityEngine.Object)null)
		{
			player.ShieldHp = sync_param.shieldHp;
		}
	}

	public void SetSyncParamForExplorePlayerStatus(BuffSyncParam sync_param)
	{
		BuffSyncData[] array = new BuffSyncData[192];
		int i = 0;
		for (int count = sync_param.buffDatas.Count; i < count; i++)
		{
			int type = sync_param.buffDatas[i].type;
			if (type >= 0 && type < 192)
			{
				array[type] = sync_param.buffDatas[i];
			}
		}
		for (int j = 0; j < 192; j++)
		{
			BuffData buffData = data[j];
			if (buffData.enable)
			{
				if (array[j] != null)
				{
					data[j].SetBuffData(array[j]);
				}
				else
				{
					buffData.enable = false;
					buffData.time = 0f;
					buffData.value = 0;
					buffData.interval = 0f;
					buffData.progress = 0f;
					buffData.avoidCount = 0;
					buffData.endless = false;
					buffData.skillId = 0u;
				}
			}
			else if (array[j] != null)
			{
				buffData.enable = true;
				buffData.SetBuffData(array[j]);
			}
		}
		if ((UnityEngine.Object)player != (UnityEngine.Object)null)
		{
			player.ShieldHp = sync_param.shieldHp;
		}
	}

	private bool CheckAbilityByEventID(uint abilityID, int eventID)
	{
		if (Singleton<AbilityTable>.IsValid())
		{
			AbilityTable.Ability ability = Singleton<AbilityTable>.I.GetAbility(abilityID);
			if (ability != null && ability.unlockEventId > 0)
			{
				return ability.unlockEventId == eventID;
			}
		}
		return true;
	}

	private bool CheckAbilityInfoByEventID(AbilityDataTable.AbilityData.AbilityInfo abilityInfo, int eventID)
	{
		if (abilityInfo != null && abilityInfo.unlockEventId > 0)
		{
			return abilityInfo.unlockEventId == eventID;
		}
		return true;
	}

	private bool RazerActiveFullArmorSet(uint abilityId)
	{
		CharaInfo charaInfo = player.createInfo.charaInfo;
		List<int> list = new List<int>();
		list.Add(80000020);
		list.Add(80000030);
		list.Add(80000040);
		list.Add(80000050);
		List<int> list2 = list;
		list = new List<int>();
		list.Add(80000021);
		list.Add(80000031);
		list.Add(80000041);
		list.Add(80000051);
		List<int> list3 = list;
		int num = 0;
		if (charaInfo.sex == 0)
		{
			foreach (CharaInfo.EquipItem item in charaInfo.equipSet)
			{
				if (list2.IndexOf(item.eId) > -1)
				{
					num++;
				}
			}
		}
		else
		{
			foreach (CharaInfo.EquipItem item2 in charaInfo.equipSet)
			{
				if (list3.IndexOf(item2.eId) > -1)
				{
					num++;
				}
			}
		}
		if (num == 4)
		{
			return true;
		}
		return false;
	}

	private bool RazerActiveWeapon(uint abilityId)
	{
		List<int> list = new List<int>();
		list.Add(60020200);
		list.Add(60020201);
		list.Add(60020202);
		list.Add(60030200);
		list.Add(60030201);
		list.Add(60030202);
		List<int> list2 = list;
		CharaInfo.EquipItem weaponData = player.weaponData;
		if (list2.IndexOf(weaponData.eId) > -1)
		{
			return true;
		}
		return false;
	}

	private ConditionsAbility CreateConditionsAbility(AbilityDataTable.AbilityData data, EQUIPMENT_TYPE equipType, SP_ATTACK_TYPE spAttackType, int eventId)
	{
		return CreateConditionsAbility(data.info, equipType, spAttackType, eventId);
	}

	private ConditionsAbility CreateConditionsAbility(AbilityDataTable.AbilityData.AbilityInfo[] dataInfo, EQUIPMENT_TYPE equipType, SP_ATTACK_TYPE spAttackType, int eventId)
	{
		ConditionsAbility conditionsAbility = null;
		for (int i = 0; i < dataInfo.Length; i++)
		{
			AbilityDataTable.AbilityData.AbilityInfo abilityInfo = dataInfo[i];
			if (CheckAbilityInfoByEventID(abilityInfo, eventId))
			{
				bool flag = false;
				int enablesCount = abilityInfo.getEnablesCount();
				for (int j = 0; j < enablesCount; j++)
				{
					if (abilityInfo.enables[j].type != 0)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					if (conditionsAbility == null)
					{
						conditionsAbility = new ConditionsAbility();
					}
					conditionsAbility.conditionInfos[i] = new ConditionsAbility.ConditionsAbilityInfo();
					conditionsAbility.conditionInfos[i].abilityInfo = abilityInfo;
					bool flag2 = true;
					bool rEnable = true;
					int enablesCount2 = abilityInfo.getEnablesCount();
					for (int k = 0; k < enablesCount2; k++)
					{
						flag2 &= Utility.IsEnableEquip(equipType, abilityInfo.enables[k].type);
						if (Utility.CheckEnableSpAttackType(ref rEnable, spAttackType, abilityInfo.enables[k].SpAtkEnableTypeBit) && !rEnable)
						{
							break;
						}
					}
					conditionsAbility.conditionInfos[i].isValid = (flag2 && rEnable);
				}
			}
		}
		if (conditionsAbility == null)
		{
			return null;
		}
		for (int l = 0; l < dataInfo.Length; l++)
		{
			ConditionsAbility.ConditionsAbilityInfo conditionsAbilityInfo = conditionsAbility.conditionInfos[l];
			if (conditionsAbilityInfo != null)
			{
				if (conditionsAbilityInfo.abilityInfo.type == ABILITY_TYPE.NARROW_ESCAPE)
				{
					AddAbilityParam(conditionsAbilityInfo.abilityInfo, false);
				}
				AbilityAtkBase abilityAtkBase = null;
				if (conditionsAbilityInfo.abilityInfo.enables[0].type == ABILITY_ENABLE_TYPE.RAZER_FULLSET_ACTIVE)
				{
					if (conditionsAbilityInfo.abilityInfo.type == ABILITY_TYPE.TOLERANCE_UP && RazerActiveFullArmorSet(oldAbilityId))
					{
						abilityAtkBase = GetAbilityAtkParam(conditionsAbilityInfo.abilityInfo);
					}
				}
				else if (conditionsAbilityInfo.abilityInfo.enables[0].type == ABILITY_ENABLE_TYPE.RAZER_WEAPON_ACTIVE || conditionsAbilityInfo.abilityInfo.target == "RAZER_WEAPON")
				{
					if ((conditionsAbilityInfo.abilityInfo.type == ABILITY_TYPE.DAMAGE_UP_ELEMENT || conditionsAbilityInfo.abilityInfo.type == ABILITY_TYPE.GAUGE_INCREASE_UP) && RazerActiveWeapon(oldAbilityId))
					{
						abilityAtkBase = GetAbilityAtkParam(conditionsAbilityInfo.abilityInfo);
					}
				}
				else
				{
					abilityAtkBase = GetAbilityAtkParam(conditionsAbilityInfo.abilityInfo);
				}
				if (abilityAtkBase != null)
				{
					conditionsAbilityInfo.atkBase = abilityAtkBase;
				}
			}
		}
		return conditionsAbility;
	}

	public void AddAbility(uint ability_id, int AP, EQUIPMENT_TYPE equipType, SP_ATTACK_TYPE spAttackType, int eventID)
	{
		oldAbilityId = ability_id;
		AbilityDataTable.AbilityData abilityData = Singleton<AbilityDataTable>.I.GetAbilityData(ability_id, AP);
		if (abilityData != null && Utility.IsEnableEquip(equipType, abilityData.enableEquipType) && CheckAbilityByEventID(ability_id, eventID) && (ability_id != 80000200 || RazerActiveFullArmorSet(ability_id) || RazerActiveWeapon(ability_id)))
		{
			ConditionsAbility conditionsAbility = CreateConditionsAbility(abilityData, equipType, spAttackType, eventID);
			if (conditionsAbility != null)
			{
				passive.conditionsAbilityList.Add(conditionsAbility);
			}
			for (int i = 0; i < 3; i++)
			{
				if (CheckAbilityInfoByEventID(abilityData.info[i], eventID) && (conditionsAbility == null || conditionsAbility.conditionInfos[i] == null))
				{
					AbilityAtkBase abilityAtkParam = GetAbilityAtkParam(abilityData.info[i]);
					if (abilityAtkParam != null)
					{
						passive.abilityAtkList.Add(abilityAtkParam);
					}
					else
					{
						AddAbilityParam(abilityData.info[i], false);
					}
				}
			}
		}
	}

	public void AddAbilityItemParam(AbilityDataTable.AbilityData.AbilityInfo[] info, EQUIPMENT_TYPE equipType, SP_ATTACK_TYPE spAttackType, int eventID)
	{
		ConditionsAbility conditionsAbility = CreateConditionsAbility(info, equipType, spAttackType, eventID);
		if (conditionsAbility != null)
		{
			passive.conditionsAbilityList.Add(conditionsAbility);
		}
		for (int i = 0; i < info.Length; i++)
		{
			if (CheckAbilityInfoByEventID(info[i], eventID) && (conditionsAbility == null || conditionsAbility.conditionInfos[i] == null))
			{
				AbilityAtkBase abilityAtkParam = GetAbilityAtkParam(info[i]);
				if (abilityAtkParam != null)
				{
					passive.abilityAtkList.Add(abilityAtkParam);
				}
				else
				{
					AddAbilityParam(info[i], false);
				}
			}
		}
	}

	private void AddAbilityParam(AbilityDataTable.AbilityData.AbilityInfo info, bool isRemove = false)
	{
		int num = 1;
		if (isRemove)
		{
			num *= -1;
		}
		switch (info.type)
		{
		case ABILITY_TYPE.LUCK_UP:
		case ABILITY_TYPE.DAMAGE_UP_WEAK:
		case ABILITY_TYPE.DAMAGE_UP_ENEMY_DOWN:
		case ABILITY_TYPE.EXP_UP:
		case ABILITY_TYPE.MONEY_UP:
		case ABILITY_TYPE.IF_HP_LOW:
		case ABILITY_TYPE.IF_HP_HIGH:
		case ABILITY_TYPE.IF_COUNTER_ATTACK:
		case ABILITY_TYPE.LUNATIC_TEAR:
		case ABILITY_TYPE.BUFF_WING:
		case ABILITY_TYPE.HOUNDGP_POINT_UP:
		case ABILITY_TYPE.DAMAGE_DOWN_WEAPON:
		case ABILITY_TYPE.DAMAGE_DOWN_ELEMENT:
		case ABILITY_TYPE.DAMAGE_DOWN_ENEMY_TYPE:
		case ABILITY_TYPE.DAMAGE_DOWN_ENEMY_NAME:
		case ABILITY_TYPE.DAMAGE_UP_FROM_AVOID:
		case ABILITY_TYPE.COUNTER_DAMAGE_UP:
		case ABILITY_TYPE.REVENGE_DAMAGE_UP:
		case ABILITY_TYPE.BOOST_DAMAGE_UP:
		case ABILITY_TYPE.JUMP_DAMAGE_UP:
		case ABILITY_TYPE.SHADOWSEALING_DAMAGE_UP:
		case ABILITY_TYPE.GAUGE_HEAT_COMBO_DAMAGE_UP:
		case ABILITY_TYPE.DAMAGE_UP_ARROW_SP:
		case ABILITY_TYPE.BURST_SHOT_DAMAGE_UP:
		case ABILITY_TYPE.BURST_FULLBURST_DAMAGE_UP:
		case ABILITY_TYPE.DAMAGE_UP_NORMAL:
		case ABILITY_TYPE.DAMAGE_UP_ATK_NORMAL:
			break;
		case ABILITY_TYPE.DAMAGE_UP_SP_ACTION:
			if (info.target == "ARROW")
			{
				passive.bleedUp += (float)(int)info.value * 0.01f * (float)num;
			}
			break;
		case ABILITY_TYPE.DAMAGE_DOWN_SP_ACTION:
			if (info.target == "ARROW")
			{
				passive.bleedUp -= (float)(int)info.value * 0.01f * (float)num;
			}
			break;
		case ABILITY_TYPE.GUARD_UP:
			passive.guardUp += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.CHARGE_SPEED_UP:
			if (Enum.IsDefined(typeof(EQUIPMENT_TYPE), info.target))
			{
				switch ((int)Enum.Parse(typeof(EQUIPMENT_TYPE), info.target))
				{
				case 3:
					break;
				case 1:
					passive.chargeSwordsTimeRate += (float)(int)info.value * 0.01f * (float)num;
					break;
				case 5:
					passive.chargeArrowTimeRate += (float)(int)info.value * 0.01f * (float)num;
					break;
				case 4:
					passive.chargePairSwordsTimeRate += (float)(int)info.value * 0.01f * (float)num;
					break;
				case 2:
					passive.chargeSpearTimeRate += (float)(int)info.value * 0.01f * (float)num;
					break;
				}
			}
			break;
		case ABILITY_TYPE.CHARGE_SPEED_DOWN:
			if (Enum.IsDefined(typeof(EQUIPMENT_TYPE), info.target))
			{
				switch ((int)Enum.Parse(typeof(EQUIPMENT_TYPE), info.target))
				{
				case 3:
					break;
				case 1:
					passive.chargeSwordsTimeRate -= (float)(int)info.value * 0.01f * (float)num;
					break;
				case 5:
					passive.chargeArrowTimeRate -= (float)(int)info.value * 0.01f * (float)num;
					break;
				case 4:
					passive.chargePairSwordsTimeRate -= (float)(int)info.value * 0.01f * (float)num;
					break;
				case 2:
					passive.chargeSpearTimeRate -= (float)(int)info.value * 0.01f * (float)num;
					break;
				}
			}
			break;
		case ABILITY_TYPE.CHARGE_HEAT_ARROW_SPEED_UP:
			passive.chargeHeatArrowTimeRate += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.CHARGE_HEAT_ARROW_SPEED_DOWN:
			passive.chargeHeatArrowTimeRate -= (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.SOUL_CHARGE_SPEED_UP:
			if (Enum.IsDefined(typeof(EQUIPMENT_TYPE), info.target))
			{
				passive.soulChargeTimeRate += (float)(int)info.value * 0.01f * (float)num;
			}
			break;
		case ABILITY_TYPE.SPEAR_RUSH_DISTANCE_UP:
			passive.spearRushDistanceRate += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.BAD_STATUS_UP:
			if (Enum.IsDefined(typeof(BAD_STATUS_UP), info.target))
			{
				int num4 = (int)Enum.Parse(typeof(BAD_STATUS_UP), info.target);
				passive.badStatusUp[num4] += (float)(int)info.value * (float)num;
			}
			break;
		case ABILITY_TYPE.BAD_STATUS_RATE_UP:
			if (Enum.IsDefined(typeof(BAD_STATUS_UP), info.target))
			{
				int num3 = (int)Enum.Parse(typeof(BAD_STATUS_UP), info.target);
				passive.badStatusRateUp[num3] += (float)(int)info.value * 0.01f * (float)num;
			}
			break;
		case ABILITY_TYPE.TOLERANCE_UP:
			if (Enum.IsDefined(typeof(TOLERANCETYPE), info.target))
			{
				int num2 = (int)Enum.Parse(typeof(TOLERANCETYPE), info.target);
				passive.tolerance[num2] -= (int)info.value * num;
			}
			break;
		case ABILITY_TYPE.BADSTATUS_TOLERANCE_DOWN:
			if (Enum.IsDefined(typeof(TOLERANCETYPE), info.target))
			{
				int num5 = (int)Enum.Parse(typeof(TOLERANCETYPE), info.target);
				passive.tolerance[num5] += (int)info.value * num;
			}
			break;
		case ABILITY_TYPE.HEAL_UP:
			passive.healUP += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.HEAL_DOWN:
			passive.healUP -= (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.HEAL_SPEEDUP:
			passive.hpHealSpeedUp += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.HEAL_UP_DEPENDS_WEAPON:
			if (!((UnityEngine.Object)player == (UnityEngine.Object)null) && Enum.IsDefined(typeof(EQUIPMENT_TYPE), info.target))
			{
				EQUIPMENT_TYPE eQUIPMENT_TYPE = Player.ConvertAttackModeToEquipmentType(player.attackMode);
				if (eQUIPMENT_TYPE == (EQUIPMENT_TYPE)(int)Enum.Parse(typeof(EQUIPMENT_TYPE), info.target))
				{
					bool flag = true;
					bool rEnable = true;
					int enablesCount = info.getEnablesCount();
					for (int i = 0; i < enablesCount; i++)
					{
						if (!Enum.IsDefined(typeof(ABILITY_ENABLE_TYPE), info.enables[i].type))
						{
							return;
						}
						if (Utility.CheckEnableSpAttackType(ref rEnable, player.spAttackType, info.enables[i].type))
						{
							if (!rEnable)
							{
								break;
							}
						}
						else
						{
							flag = Utility.IsEnableEquip(eQUIPMENT_TYPE, info.enables[i].type);
						}
					}
					if (flag && rEnable)
					{
						passive.healUpDependsWeapon += (float)(int)info.value * 0.01f * (float)num;
					}
				}
			}
			break;
		case ABILITY_TYPE.AVOID_UP:
			passive.avoidUp += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.AVOID_DOWN:
			passive.avoidUp -= (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.BOOST_AVOID_UP:
			passive.boostAvoidUp += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.MOVE_SPEED_UP:
			passive.moveSpeedUp += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.MOVE_SPEED_DOWN:
			passive.moveSpeedUp -= (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.BOOST_MOVE_SPEED_UP:
			passive.boostMoveSpeedUp += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.SKILL_ABSORB_UP:
			passive.skillAbsorbUp += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.SKILL_ABSORB_DOWN:
			passive.skillAbsorbUp -= (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.SKILL_ABSORBUP_ONLY_ATK_FIRE:
			passive.skillAbsorbUp_OnlyAttackAndElement.fire += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.SKILL_ABSORBUP_ONLY_ATK_WATER:
			passive.skillAbsorbUp_OnlyAttackAndElement.water += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.SKILL_ABSORBUP_ONLY_ATK_THUNDER:
			passive.skillAbsorbUp_OnlyAttackAndElement.thunder += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.SKILL_ABSORBUP_ONLY_ATK_SOIL:
			passive.skillAbsorbUp_OnlyAttackAndElement.soil += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.SKILL_ABSORBUP_ONLY_ATK_LIGHT:
			passive.skillAbsorbUp_OnlyAttackAndElement.light += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.SKILL_ABSORBUP_ONLY_ATK_DARK:
			passive.skillAbsorbUp_OnlyAttackAndElement.dark += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.SKILL_SPEED_UP:
			passive.skillTimeRate += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.SKILL_SPEED_DOWN:
			passive.skillTimeRate -= (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.DAMAGE_DOWN:
			passive.damageDown += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.RECEIVED_DAMAGE_UP:
			passive.damageDown -= (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.BOOST_DAMAGE_DOWN:
			passive.boostDamageDown += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.ATTACK_SPEED_UP:
			passive.attackSpeedUp += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.BOOST_ATTACK_SPEED_UP:
			passive.boostAttackSpeedUp += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.NARROW_ESCAPE:
			if (!passive.firstInitialized)
			{
				passive.narrowEscape += (int)info.value * num;
			}
			break;
		case ABILITY_TYPE.BREAK_GHOST_FORM:
			if (isRemove)
			{
				passive.breakGhostFormCounter--;
				if (passive.breakGhostFormCounter < 0)
				{
					passive.breakGhostFormCounter = 0;
				}
			}
			else
			{
				passive.breakGhostFormCounter++;
			}
			break;
		case ABILITY_TYPE.JUSTGUARD_EXTEND_RATE:
			passive.justGuardExtendRate += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.GAUGE_INCREASE_UP:
			passive.gaugeIncreaseRate += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.GAUGE_INCREASE_DOWN:
			passive.gaugeIncreaseRate -= (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.SOUL_GAUGE_INCREASE_UP:
			passive.soulGaugeIncreaseRate += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.SOUL_GAUGE_INCREASE_DOWN:
			passive.soulGaugeIncreaseRate -= (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.GAUGE_DECREASE_UP:
			passive.gaugeDecreaseRate += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.GAUGE_DECREASE_DOWN:
			passive.gaugeDecreaseRate -= (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.SUBGAUGE_INCREASE_UP:
			passive.subGaugeIncreaseRate += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.SUBGAUGE_INCREASE_DOWN:
			passive.subGaugeIncreaseRate -= (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.SHADOWSEALING_EXTEND:
			passive.shadowSealingExtend += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.SHADOWSEALING_EXTEND_ARROW:
			passive.shadowSealingExtendArrow += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.LOCKON_TIME_UP:
			passive.lockOnTimeRate += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.LOCKON_TIME_DOWN:
			passive.lockOnTimeRate -= (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.RESIST_FIELD_DEBUFF:
			SetPassiveFieldBuffResist(info.target, (float)(int)info.value * 0.01f * (float)num);
			SetPassiveFieldBuffResistByType(info.target, (float)(int)info.value * 0.01f * (float)num);
			break;
		case ABILITY_TYPE.DISTANCE_UP_FROM_AVOID:
			passive.distanceRateFromAvoid += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.DISTANCE_UP_IAI:
			passive.distanceRateIai += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.BURST_RELOAD_SPEED_UP:
			passive.burstReloadActionSpeed += (float)(int)info.value * 0.01f * (float)num;
			break;
		case ABILITY_TYPE.BURST_ADDITIONAL_BULLET:
			passive.additionalMaxBulletCnt += (int)info.value * num;
			break;
		case ABILITY_TYPE.DRAGONARMOR_DAMAGE_UP:
			passive.dragonArmorDamageRate += (float)(int)info.value * 0.01f * (float)num;
			break;
		}
	}

	private AbilityAtkBase GetAbilityAtkParam(AbilityDataTable.AbilityData.AbilityInfo info)
	{
		AbilityAtkBase abilityAtkBase = null;
		bool flag = false;
		switch (info.type)
		{
		case ABILITY_TYPE.DAMAGE_UP_WEAPON:
			abilityAtkBase = new AbilityAtkWeapon();
			break;
		case ABILITY_TYPE.DAMAGE_DOWN_WEAPON:
			abilityAtkBase = new AbilityAtkWeapon();
			flag = true;
			break;
		case ABILITY_TYPE.DAMAGE_UP_ELEMENT:
			abilityAtkBase = new AbilityAtkElement();
			break;
		case ABILITY_TYPE.DAMAGE_DOWN_ELEMENT:
			abilityAtkBase = new AbilityAtkElement();
			flag = true;
			break;
		case ABILITY_TYPE.DAMAGE_UP_ENEMY_TYPE:
			abilityAtkBase = new AbilityAtkEnemyType();
			break;
		case ABILITY_TYPE.DAMAGE_DOWN_ENEMY_TYPE:
			abilityAtkBase = new AbilityAtkEnemyType();
			flag = true;
			break;
		case ABILITY_TYPE.DAMAGE_UP_ENEMY_NAME:
			abilityAtkBase = new AbilityAtkEnemyName();
			break;
		case ABILITY_TYPE.DAMAGE_DOWN_ENEMY_NAME:
			abilityAtkBase = new AbilityAtkEnemyName();
			flag = true;
			break;
		case ABILITY_TYPE.DAMAGE_UP_SP_ACTION:
			if (info.target != "ARROW")
			{
				abilityAtkBase = new AbilityAtkSp();
			}
			break;
		case ABILITY_TYPE.DAMAGE_DOWN_SP_ACTION:
			if (info.target != "ARROW")
			{
				abilityAtkBase = new AbilityAtkSp();
				flag = true;
			}
			break;
		case ABILITY_TYPE.DAMAGE_UP_WEAK:
			abilityAtkBase = new AbilityWeakAtk();
			break;
		case ABILITY_TYPE.DAMAGE_UP_ENEMY_DOWN:
			abilityAtkBase = new AbilityEnemyDownAtk();
			break;
		case ABILITY_TYPE.LUNATIC_TEAR:
			abilityAtkBase = new AbilityLunaticTear();
			break;
		case ABILITY_TYPE.BUFF_WING:
			abilityAtkBase = new AbilityBuffWing();
			break;
		case ABILITY_TYPE.DAMAGE_UP_FROM_AVOID:
			abilityAtkBase = new AbilityAtkTypeFromAvoid();
			break;
		case ABILITY_TYPE.BOOST_DAMAGE_UP:
			abilityAtkBase = new AbilityAtkBoost();
			break;
		case ABILITY_TYPE.COUNTER_DAMAGE_UP:
			abilityAtkBase = new AbilityAtkCounter();
			break;
		case ABILITY_TYPE.REVENGE_DAMAGE_UP:
			abilityAtkBase = new AbilityAtkRevenge();
			break;
		case ABILITY_TYPE.JUMP_DAMAGE_UP:
			abilityAtkBase = new AbilityAtkJump();
			break;
		case ABILITY_TYPE.SHADOWSEALING_DAMAGE_UP:
			abilityAtkBase = new AbilityAtkShadowSealing();
			break;
		case ABILITY_TYPE.GAUGE_HEAT_COMBO_DAMAGE_UP:
			abilityAtkBase = new AbilityAtkGaugeHeatCombo();
			break;
		case ABILITY_TYPE.DAMAGE_UP_ARROW_SP:
			abilityAtkBase = new AbilityAtkArrowSp();
			break;
		case ABILITY_TYPE.BURST_SHOT_DAMAGE_UP:
			abilityAtkBase = new AbilityAtkBurstShotDmgUp();
			break;
		case ABILITY_TYPE.BURST_FULLBURST_DAMAGE_UP:
			abilityAtkBase = new AbilityAtkFullBurstDmgUp();
			break;
		case ABILITY_TYPE.DAMAGE_UP_NORMAL:
			abilityAtkBase = new AbilityAtkNormal();
			break;
		case ABILITY_TYPE.DAMAGE_UP_ATK_NORMAL:
			abilityAtkBase = new AbilityAtkTypeNormal();
			break;
		}
		if (abilityAtkBase != null)
		{
			if (flag)
			{
				abilityAtkBase.init(player, info.target, -(int)info.value);
			}
			else
			{
				abilityAtkBase.init(player, info.target, info.value);
			}
		}
		return abilityAtkBase;
	}

	public void UpdateConditionsAbility()
	{
		float hpRate = (float)chara.hp / (float)chara.hpMax;
		for (int i = 0; i < passive.conditionsAbilityList.Count; i++)
		{
			ConditionsAbility conditionsAbility = passive.conditionsAbilityList[i];
			for (int j = 0; j < 3; j++)
			{
				if (conditionsAbility.conditionInfos[j] != null && conditionsAbility.conditionInfos[j].isValid)
				{
					bool flag = IsMatchConditions(conditionsAbility.conditionInfos[j].abilityInfo, hpRate, conditionsAbility.counterAttackNum, conditionsAbility.cleaveComboNum);
					if (flag == conditionsAbility.conditionInfos[j].isMatch)
					{
						if (conditionsAbility.conditionInfos[j].currentStack < conditionsAbility.conditionInfos[j].GetMaxStack() && flag && !conditionsAbility.conditionInfos[j].isStacked)
						{
							AddConditionsAbility(conditionsAbility.conditionInfos[j], i);
							conditionsAbility.conditionInfos[j].isStacked = true;
							conditionsAbility.conditionInfos[j].currentStack++;
						}
					}
					else
					{
						if (!flag)
						{
							RemoveConditionsAbility(conditionsAbility.conditionInfos[j]);
						}
						else
						{
							AddConditionsAbility(conditionsAbility.conditionInfos[j], i);
							conditionsAbility.conditionInfos[j].isStacked = true;
							conditionsAbility.conditionInfos[j].currentStack++;
						}
						conditionsAbility.conditionInfos[j].isMatch = flag;
					}
				}
			}
		}
	}

	public void ResetStack()
	{
		for (int i = 0; i < passive.conditionsAbilityList.Count; i++)
		{
			ConditionsAbility conditionsAbility = passive.conditionsAbilityList[i];
			for (int j = 0; j < 3; j++)
			{
				if (conditionsAbility.conditionInfos != null && conditionsAbility.conditionInfos.Length > 0 && conditionsAbility.conditionInfos[j] != null)
				{
					conditionsAbility.conditionInfos[j].isStacked = false;
				}
			}
		}
	}

	private bool IsMatchConditions(AbilityDataTable.AbilityData.AbilityInfo info, float hpRate, int nowCounterAttackNum, int nowCleaveCombo)
	{
		int enablesCount = info.getEnablesCount();
		for (int i = 0; i < enablesCount; i++)
		{
			switch (info.enables[i].type)
			{
			case ABILITY_ENABLE_TYPE.IF_HP_LOW:
				if (!(hpRate <= (float)info.enables[i].values[0] * 0.01f))
				{
					return false;
				}
				break;
			case ABILITY_ENABLE_TYPE.FINISH_A_CLEAVE_COMBO:
			{
				int num2 = info.enables[i].values[0];
				if (nowCleaveCombo <= 0)
				{
					return false;
				}
				break;
			}
			case ABILITY_ENABLE_TYPE.IF_HP_HIGH:
				if (!(hpRate >= (float)info.enables[i].values[0] * 0.01f))
				{
					return false;
				}
				break;
			case ABILITY_ENABLE_TYPE.IF_COUNTER_ATTACK:
			{
				int num = info.enables[i].values[0];
				if (nowCounterAttackNum < num)
				{
					return false;
				}
				break;
			}
			}
		}
		return true;
	}

	private void AddConditionsAbility(ConditionsAbility.ConditionsAbilityInfo info, int conditionIndex)
	{
		if (info.abilityInfo.type == ABILITY_TYPE.NARROW_ESCAPE)
		{
			passive.enableNarrowEscape = true;
		}
		else if (info.abilityInfo.enables[0].type == ABILITY_ENABLE_TYPE.RAZER_FULLSET_ACTIVE)
		{
			if (info.abilityInfo.type == ABILITY_TYPE.TOLERANCE_UP && RazerActiveFullArmorSet(oldAbilityId))
			{
				AddAbilityParam(info.abilityInfo, false);
			}
		}
		else if (info.abilityInfo.enables[0].type == ABILITY_ENABLE_TYPE.RAZER_WEAPON_ACTIVE || info.abilityInfo.target == "RAZER_WEAPON")
		{
			if ((info.abilityInfo.type == ABILITY_TYPE.DAMAGE_UP_ELEMENT || info.abilityInfo.type == ABILITY_TYPE.GAUGE_INCREASE_UP) && RazerActiveWeapon(oldAbilityId))
			{
				AddAbilityParam(info.abilityInfo, false);
			}
		}
		else
		{
			AddAbilityParam(info.abilityInfo, false);
		}
		AbilityLunaticTear abilityLunaticTear = info.atkBase as AbilityLunaticTear;
		AbilityBuffWing abilityBuffWing = info.atkBase as AbilityBuffWing;
		if (abilityLunaticTear != null)
		{
			if (player.isInitialized && !IsValidBuff(BUFFTYPE.LUNATIC_TEAR))
			{
				BuffData buffData = new BuffData();
				buffData.type = BUFFTYPE.LUNATIC_TEAR;
				buffData.time = abilityLunaticTear.validTime;
				buffData.value = 1;
				buffData.conditionIndex = conditionIndex;
				chara.OnBuffStart(buffData);
			}
		}
		else if (abilityBuffWing != null)
		{
			if (player.isInitialized && !IsValidBuff(BUFFTYPE.WING))
			{
				BuffData buffData2 = new BuffData();
				buffData2.type = BUFFTYPE.WING;
				buffData2.time = abilityBuffWing.validTime;
				buffData2.value = 1;
				buffData2.conditionIndex = conditionIndex;
				chara.OnBuffStart(buffData2);
			}
		}
		else
		{
			passive.abilityAtkList.Add(info.atkBase);
		}
	}

	private void RemoveConditionsAbility(ConditionsAbility.ConditionsAbilityInfo abilityInfo)
	{
		if (abilityInfo.abilityInfo.type == ABILITY_TYPE.NARROW_ESCAPE)
		{
			passive.enableNarrowEscape = false;
		}
		else
		{
			AddAbilityParam(abilityInfo.abilityInfo, true);
		}
		passive.abilityAtkList.Remove(abilityInfo.atkBase);
	}

	public bool IncrementCounterConditionAbility()
	{
		bool result = false;
		foreach (ConditionsAbility conditionsAbility in passive.conditionsAbilityList)
		{
			for (int i = 0; i < 3; i++)
			{
				if (conditionsAbility.conditionInfos[i] != null)
				{
					int enablesCount = conditionsAbility.conditionInfos[i].abilityInfo.getEnablesCount();
					for (int j = 0; j < enablesCount; j++)
					{
						if (conditionsAbility.conditionInfos[i].abilityInfo.enables[j].type == ABILITY_ENABLE_TYPE.IF_COUNTER_ATTACK)
						{
							int num = conditionsAbility.conditionInfos[i].abilityInfo.enables[j].values[0];
							if (conditionsAbility.counterAttackNum < num)
							{
								conditionsAbility.counterAttackNum++;
								result = true;
							}
						}
					}
				}
			}
		}
		return result;
	}

	public bool IncrementCleaveComboConditionAbility()
	{
		bool result = false;
		foreach (ConditionsAbility conditionsAbility in passive.conditionsAbilityList)
		{
			for (int i = 0; i < 3; i++)
			{
				if (conditionsAbility.conditionInfos[i] != null)
				{
					int enablesCount = conditionsAbility.conditionInfos[i].abilityInfo.getEnablesCount();
					for (int j = 0; j < enablesCount; j++)
					{
						if (conditionsAbility.conditionInfos[i].abilityInfo.enables[j].type == ABILITY_ENABLE_TYPE.FINISH_A_CLEAVE_COMBO)
						{
							int num = conditionsAbility.conditionInfos[i].abilityInfo.enables[j].values[0];
							if (conditionsAbility.cleaveComboNum < num)
							{
								conditionsAbility.cleaveComboNum++;
								result = true;
							}
						}
					}
				}
			}
		}
		return result;
	}

	public bool IsValidBuffByAbility(BUFFTYPE targetType)
	{
		bool result = false;
		if (targetType == BUFFTYPE.BREAK_GHOST_FORM)
		{
			result = (passive.breakGhostFormCounter > 0);
		}
		return result;
	}

	public GameObject GetLoopEffect(BuffData data)
	{
		GameObject result = null;
		for (int i = 0; i < loopEffect.Count; i++)
		{
			if (loopEffect[i].linkData.Contains(data))
			{
				result = loopEffect[i].effect;
				break;
			}
		}
		return result;
	}

	public static bool IsTypeShowDamageOnEnemy(BUFFTYPE type)
	{
		bool result = false;
		if (type == BUFFTYPE.POISON || type == BUFFTYPE.ELECTRIC_SHOCK || type == BUFFTYPE.BURNING)
		{
			result = true;
		}
		return result;
	}

	public static bool IsTypeValueBasedOnHP(BUFFTYPE type)
	{
		bool result = false;
		if (type == BUFFTYPE.POISON || type == BUFFTYPE.DEADLY_POISON || type == BUFFTYPE.BURNING)
		{
			result = true;
		}
		return result;
	}

	public static bool IsHitAbsorbType(BUFFTYPE buffType)
	{
		switch (buffType)
		{
		case BUFFTYPE.HIT_ABSORB_NORMAL:
		case BUFFTYPE.HIT_ABSORB_FIRE:
		case BUFFTYPE.HIT_ABSORB_WATER:
		case BUFFTYPE.HIT_ABSORB_THUNDER:
		case BUFFTYPE.HIT_ABSORB_SOIL:
		case BUFFTYPE.HIT_ABSORB_LIGHT:
		case BUFFTYPE.HIT_ABSORB_DARK:
		case BUFFTYPE.HIT_ABSORB_ALL:
			return true;
		default:
			return false;
		}
	}

	public bool IsValidShieldBuff(int skillIndex)
	{
		BuffData buffData = data[60];
		if ((UnityEngine.Object)player == (UnityEngine.Object)null)
		{
			return false;
		}
		return buffData.enable && buffData.fromObjectID == player.createInfo.charaInfo.userId && buffData.fromEquipIndex == player.weaponIndex && buffData.fromSkillIndex == skillIndex;
	}

	public bool IsValidInvinsibleBuff()
	{
		bool result = false;
		for (int i = 164; i <= 169; i++)
		{
			if (IsValidBuff((BUFFTYPE)i))
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public int GetShieldFromSkillIndex()
	{
		if ((UnityEngine.Object)player == (UnityEngine.Object)null)
		{
			return -1;
		}
		return data[60].fromSkillIndex;
	}

	public string GetFromText()
	{
		BuffData buffData = data[60];
		if ((UnityEngine.Object)player == (UnityEngine.Object)null)
		{
			return "null";
		}
		if (!buffData.enable)
		{
			return "false";
		}
		return buffData.fromObjectID + "," + buffData.fromEquipIndex + "," + buffData.fromSkillIndex;
	}

	public void ClearFieldBuff()
	{
		fieldData.Clear();
	}

	public void StartFieldBuff(uint fieldBuffId, BuffTable.BuffData tableData)
	{
		if (!fieldData.ContainsKey(tableData.type))
		{
			BuffData buffData = new BuffData();
			buffData.type = tableData.type;
			buffData.valueType = tableData.valueType;
			buffData.value = tableData.value;
			buffData.interval = tableData.interval;
			buffData.time = -1f;
			buffData.endless = true;
			buffData.enable = true;
			buffData.fromFieldBuffID = fieldBuffId;
			fieldData.Add(buffData.type, buffData);
		}
	}

	public int GetFieldBuffValue(BUFFTYPE type)
	{
		if (!fieldData.ContainsKey(type))
		{
			return 0;
		}
		BuffData buffData = fieldData[type];
		float num = 1f - GetPassiveFieldBuffResist(buffData.fromFieldBuffID);
		if (num < 0f)
		{
			num = 0f;
		}
		return (int)((float)buffData.value * num);
	}

	private void SetPassiveFieldBuffResist(string idStr, float value)
	{
		uint result = 0u;
		if (uint.TryParse(idStr, out result))
		{
			if (passive.fieldBuffResist.ContainsKey(result))
			{
				Dictionary<uint, float> fieldBuffResist;
				Dictionary<uint, float> dictionary = fieldBuffResist = passive.fieldBuffResist;
				uint key;
				uint key2 = key = result;
				float num = fieldBuffResist[key];
				dictionary[key2] = num + value;
			}
			else
			{
				passive.fieldBuffResist.Add(result, value);
			}
		}
	}

	private void SetPassiveFieldBuffResistByType(string idStr, float value)
	{
		uint result = 0u;
		if (uint.TryParse(idStr, out result) && Singleton<FieldBuffTable>.IsValid())
		{
			FieldBuffTable.FieldBuffData fieldBuffData = Singleton<FieldBuffTable>.I.GetData(result);
			if (fieldBuffData != null && !fieldBuffData.buffTableIds.IsNullOrEmpty() && Singleton<BuffTable>.IsValid())
			{
				int i = 0;
				for (int count = fieldBuffData.buffTableIds.Count; i < count; i++)
				{
					BuffTable.BuffData buffData = Singleton<BuffTable>.I.GetData(fieldBuffData.buffTableIds[i]);
					if (buffData != null)
					{
						if (passive.fieldBuffResistByType.ContainsKey(buffData.type))
						{
							Dictionary<BUFFTYPE, float> fieldBuffResistByType;
							Dictionary<BUFFTYPE, float> dictionary = fieldBuffResistByType = passive.fieldBuffResistByType;
							BUFFTYPE type;
							BUFFTYPE key = type = buffData.type;
							float num = fieldBuffResistByType[type];
							dictionary[key] = num + value;
						}
						else
						{
							passive.fieldBuffResistByType.Add(buffData.type, value);
						}
					}
				}
			}
		}
	}

	public float GetRatePassiveFieldBuffResist(BUFFTYPE type)
	{
		if (!fieldData.ContainsKey(type))
		{
			return 0f;
		}
		BuffData buffData = fieldData[type];
		return GetPassiveFieldBuffResist(buffData.fromFieldBuffID);
	}

	private float GetPassiveFieldBuffResist(uint id)
	{
		if (passive.fieldBuffResist.ContainsKey(id))
		{
			return passive.fieldBuffResist[id];
		}
		return 0f;
	}

	public float GetPassiveFieldBuffResist(BUFFTYPE type)
	{
		if (passive.fieldBuffResistByType.ContainsKey(type))
		{
			return passive.fieldBuffResistByType[type];
		}
		return 0f;
	}
}
