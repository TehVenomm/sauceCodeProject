using System;
using UnityEngine;

[Serializable]
public class AttackHitInfo : AttackInfo
{
	public enum ATTACK_TYPE
	{
		NORMAL,
		SOUNDWAVE,
		TWO_HAND_SWORD_SP,
		HEAL_ATTACK,
		SPEAR_SP,
		CANNON_BALL,
		BOMBROCK,
		JUMP,
		FROM_AVOID,
		COUNTER,
		COUNTER2,
		REVENGE,
		CANNON_BALL_DIRECT,
		THS_HEAT_COMBO,
		SNATCH,
		BURST_THS_COMBO03,
		BURST_THS_SINGLE_SHOT,
		BURST_THS_FULL_BURST,
		GIMMICK_GENERATED,
		COUNTER_BURST,
		JUSTGUARD_ATTACK,
		BURST_SPEAR_COMBO3,
		SHIELD_REFLECT,
		BOOST_BOMB_ARROW,
		BOMB,
		BOOST_ARROW_RAIN,
		ARROW_RAIN,
		THS_ORACLE_HORIZONTAL,
		THS_ORACLE_CHARGE,
		THS_ORACLE_CHARGE_MAX,
		BOOST_BOMB,
		OHS_ORACLE_SP,
		SPEAR_ORACLE_SP,
		SPEAR_ORACLE_SP_CHARGED,
		PAIR_SWORDS_ORACLE_RUSH,
		PAIR_SWORDS_ORACLE_RUSH_BOOST,
		PAIR_SWORDS_ORACLE_SP
	}

	[Serializable]
	public class ToPlayer
	{
		public enum REACTION_TYPE
		{
			NONE,
			DAMAGE,
			BLOW,
			STUNNED_BLOW,
			STUMBLE,
			___,
			FALL_BLOW,
			SHAKE,
			CHARM_BLOW
		}

		[Tooltip("リアクションタイプ")]
		public REACTION_TYPE reactionType = REACTION_TYPE.DAMAGE;

		[Tooltip("リアクション時間（秒")]
		public float reactionLoopTime;

		[Tooltip("吹き飛ばし力")]
		public float reactionBlowForce;

		[Tooltip("吹き飛ばし角度")]
		public float reactionBlowAngle;

		[Tooltip("Buff解除")]
		public bool isBuffCancellation;

		[Tooltip("パリィ・カウンタ\u30fc不可")]
		public bool disableCounter;

		[Tooltip("ガ\u30fcド不可")]
		public bool disableGuard;

		public void Copy(ToPlayer src)
		{
			reactionType = src.reactionType;
			reactionLoopTime = src.reactionLoopTime;
			reactionBlowForce = src.reactionBlowForce;
			reactionBlowAngle = src.reactionBlowAngle;
			isBuffCancellation = src.isBuffCancellation;
			disableCounter = src.disableCounter;
			disableGuard = src.disableGuard;
		}
	}

	[Serializable]
	public class ToEnemy
	{
		public enum REACTION_TYPE
		{
			NONE,
			DAMAGE,
			DOWN,
			BIND
		}

		[Serializable]
		public class DamageToRegionInfo
		{
			[Tooltip("部位へのダメ\u30fcジアップするか")]
			public bool isDamageUp;

			[Tooltip("部位へのダメ\u30fcジアップ倍率（N%アップ）")]
			public int damageUpPercent;

			[Tooltip("竜装へのダメ\u30fcジアップ倍率")]
			public float dragonArmorDamageRate = 1f;

			[Tooltip("黒霧(バリア)へのダメ\u30fcジアップ倍率")]
			public float barrierDamageRate = 1f;

			[Tooltip("弱点属性防御力無視")]
			public bool ignoreWeakElementDefence;
		}

		[Serializable]
		public class ReactionInfo
		{
			[Tooltip("リアクション時間（秒）")]
			public float reactionLoopTime;
		}

		[Tooltip("リアクションタイプ")]
		public REACTION_TYPE reactionType;

		[Tooltip("ヒットストップ時間（プレイヤ\u30fc）")]
		public float hitStopTime;

		[Tooltip("ヒットストップ時間（モンスタ\u30fc）")]
		public float enemyHitStopTime;

		[Tooltip("ヒットタイプ名(EnemyHitTypeTable)")]
		public string hitTypeName;

		[Tooltip("武器固有攻撃")]
		public bool isSpecialAttack;

		[Tooltip("WEAKに当たったときに怯む")]
		public bool isWeakHitReaction;

		[Tooltip("脳震盪蓄積値(WEAKヒット時)")]
		public float concussion;

		[Tooltip("部位へのダメ\u30fcジアップ情報")]
		public DamageToRegionInfo damageToRegionInfo = new DamageToRegionInfo();

		[Tooltip("リアクションの情報")]
		public ReactionInfo reactionInfo = new ReactionInfo();

		[Tooltip("イ\u30fcジスへのダメ\u30fcジ上昇")]
		public float aegisDamageRate = 1f;

		public void Copy(ToEnemy src)
		{
			reactionType = src.reactionType;
			hitStopTime = src.hitStopTime;
			hitTypeName = src.hitTypeName;
			isSpecialAttack = src.isSpecialAttack;
			damageToRegionInfo = src.damageToRegionInfo;
			reactionInfo = src.reactionInfo;
			aegisDamageRate = src.aegisDamageRate;
		}
	}

	[Tooltip("攻撃力倍率")]
	public float atkRate = 1f;

	[Tooltip("攻撃力")]
	public AtkAttribute atk = new AtkAttribute();

	[Tooltip("自身の攻撃力を無視する")]
	public bool isAtkIgnore;

	[Tooltip("ダウン値")]
	public float down;

	[Tooltip("ダウン中にヒットしてもダウン値を増やすか")]
	public bool isForceDown;

	[Tooltip("状態異常値")]
	public BadStatus badStatus = new BadStatus();

	[Tooltip("モ\u30fcションストップ時間")]
	public float motionStopTime;

	[Tooltip("カメラ揺れ大きさ")]
	public float shakeCameraPercent;

	[Tooltip("カメラ揺れ周期（0で共通設定")]
	public float shakeCycleTime;

	[Tooltip("多重ヒット無効フラグ")]
	public bool enableIdentityCheck = true;

	[Tooltip("ヒットの中に居続けても連続してダメ\u30fcジを受けるか")]
	public bool isValidTriggerStay;

	[Tooltip("ヒット間隔（秒。多重ヒット有効時")]
	public float hitIntervalTime = 1f;

	[Tooltip("最大ヒット数（0で無制限。多重ヒット有効時")]
	public int hitCountMax;

	[Tooltip("ヒットエフェクト名")]
	public string hitEffectName;

	[Tooltip("共通SEを再生するか")]
	public bool playCommonHitSe;

	[Tooltip("ヒットSE ID")]
	public int hitSEID;

	[Tooltip("共通Effectを再生するか")]
	public bool playCommonHitEffect;

	[Tooltip("継続して残るエフェクト名(属性差分がある場合はエンジニアに相談してください")]
	public string remainEffectName;

	[Tooltip("HP吸収率 Playerは与えたダメ\u30fcジの割合,敵は最大HPの割合")]
	public float absorptance;

	[Tooltip("即死攻撃かどうか")]
	public bool isImmediateDeath;

	[Tooltip("掴み状態にダメ\u30fcジを与えるかどうか")]
	public bool canAttackGrabbedPlayer;

	[Tooltip("外からのダメ\u30fcジのオフセット用")]
	public int damageNumAddGroup;

	[Tooltip("この攻撃ではゲ\u30fcジ増えない")]
	public bool dontIncreaseGauge;

	[Tooltip("この攻撃で破壊可能オブジェクトを壊せるか")]
	public bool isBreakObject;

	[Tooltip("攻撃タイプ（特殊な攻撃指定用")]
	public ATTACK_TYPE attackType;

	[Tooltip("属性 (攻撃力で指定できない場合に使用)")]
	public ELEMENT_TYPE elementType = ELEMENT_TYPE.MAX;

	[Tooltip("武器SPアクションタイプ")]
	public SP_ATTACK_TYPE spAttackType;

	[Tooltip("ダメ\u30fcジアップ処理用ラベル設定（複数設定可）")]
	public string[] damageUpLabels;

	public ToPlayer toPlayer = new ToPlayer();

	public ToEnemy toEnemy = new ToEnemy();

	public GrabInfo grabInfo = new GrabInfo();

	public RestraintInfo restraintInfo = new RestraintInfo();

	public ElectricShockInfo electricShockInfo = new ElectricShockInfo();

	public InkSplashInfo inkSplashInfo = new InkSplashInfo();

	public ElectricShockInfo soilShockInfo = new ElectricShockInfo();

	public uint[] buffIDs;

	[Tooltip("バリアに対する攻撃力")]
	public int toShieldAtk;

	[Tooltip("バリアに対するクリティカル時のレ\u30fcト")]
	public float toShieldCriticalRate;

	[Tooltip("バリアに対する属性のクリティカル時のレ\u30fcト")]
	public float toShieldElementCriticalRate;

	[Tooltip("ヒット時に回復する割合")]
	public float hitHealRate;

	public bool IsReferenceAtkValue => !isAtkIgnore;

	public override AttackInfo GetRateAttackInfo(AttackInfo rate_info, float rate)
	{
		AttackHitInfo attackHitInfo = base.GetRateAttackInfo(rate_info, rate) as AttackHitInfo;
		if (attackHitInfo == this)
		{
			return attackHitInfo;
		}
		AttackHitInfo attackHitInfo2 = rate_info as AttackHitInfo;
		if (attackHitInfo2 == null)
		{
			return attackHitInfo;
		}
		attackHitInfo.atkRate = AttackInfo.GetRateValue(atkRate, attackHitInfo2.atkRate, rate);
		attackHitInfo.atk.normal = AttackInfo.GetRateValue(atk.normal, attackHitInfo2.atk.normal, rate);
		attackHitInfo.atk.fire = AttackInfo.GetRateValue(atk.fire, attackHitInfo2.atk.fire, rate);
		attackHitInfo.atk.water = AttackInfo.GetRateValue(atk.water, attackHitInfo2.atk.water, rate);
		attackHitInfo.atk.thunder = AttackInfo.GetRateValue(atk.thunder, attackHitInfo2.atk.thunder, rate);
		attackHitInfo.atk.soil = AttackInfo.GetRateValue(atk.soil, attackHitInfo2.atk.soil, rate);
		attackHitInfo.atk.light = AttackInfo.GetRateValue(atk.light, attackHitInfo2.atk.light, rate);
		attackHitInfo.atk.dark = AttackInfo.GetRateValue(atk.dark, attackHitInfo2.atk.dark, rate);
		attackHitInfo.isAtkIgnore = isAtkIgnore;
		attackHitInfo.down = AttackInfo.GetRateValue(down, attackHitInfo2.down, rate);
		attackHitInfo.isForceDown = AttackInfo.GetRateValue(isForceDown, attackHitInfo2.isForceDown, rate);
		attackHitInfo.badStatus.paralyze = AttackInfo.GetRateValue(badStatus.paralyze, attackHitInfo2.badStatus.paralyze, rate);
		attackHitInfo.badStatus.poison = AttackInfo.GetRateValue(badStatus.poison, attackHitInfo2.badStatus.poison, rate);
		attackHitInfo.badStatus.burning = AttackInfo.GetRateValue(badStatus.burning, attackHitInfo2.badStatus.burning, rate);
		attackHitInfo.badStatus.speedDown = AttackInfo.GetRateValue(badStatus.speedDown, attackHitInfo2.badStatus.speedDown, rate);
		attackHitInfo.badStatus.attackSpeedDown = AttackInfo.GetRateValue(badStatus.attackSpeedDown, attackHitInfo2.badStatus.attackSpeedDown, rate);
		attackHitInfo.badStatus.lightRing = AttackInfo.GetRateValue(badStatus.lightRing, attackHitInfo2.badStatus.lightRing, rate);
		attackHitInfo.badStatus.erosion = AttackInfo.GetRateValue(badStatus.erosion, attackHitInfo2.badStatus.erosion, rate);
		attackHitInfo.badStatus.stone = AttackInfo.GetRateValue(badStatus.stone, attackHitInfo2.badStatus.stone, rate);
		attackHitInfo.shakeCameraPercent = AttackInfo.GetRateValue(shakeCameraPercent, attackHitInfo2.shakeCameraPercent, rate);
		attackHitInfo.shakeCycleTime = AttackInfo.GetRateValue(shakeCycleTime, attackHitInfo2.shakeCycleTime, rate);
		attackHitInfo.enableIdentityCheck = enableIdentityCheck;
		attackHitInfo.hitEffectName = ((!(rate < 1f)) ? attackHitInfo2.hitEffectName : hitEffectName);
		attackHitInfo.playCommonHitSe = ((!(rate < 1f)) ? attackHitInfo2.playCommonHitSe : playCommonHitSe);
		attackHitInfo.hitSEID = ((!(rate < 1f)) ? attackHitInfo2.hitSEID : hitSEID);
		attackHitInfo.playCommonHitEffect = ((!(rate < 1f)) ? attackHitInfo2.playCommonHitEffect : playCommonHitEffect);
		attackHitInfo.remainEffectName = ((!(rate < 1f)) ? attackHitInfo2.remainEffectName : remainEffectName);
		attackHitInfo.damageNumAddGroup = damageNumAddGroup;
		attackHitInfo.dontIncreaseGauge = dontIncreaseGauge;
		attackHitInfo.isBreakObject = isBreakObject;
		attackHitInfo.attackType = ((!(rate < 1f)) ? attackHitInfo2.attackType : attackType);
		attackHitInfo.spAttackType = ((!(rate < 1f)) ? attackHitInfo2.spAttackType : spAttackType);
		attackHitInfo.hitIntervalTime = AttackInfo.GetRateValue(hitIntervalTime, attackHitInfo2.hitIntervalTime, rate);
		attackHitInfo.enableIdentityCheck = ((!(rate <= 0f)) ? attackHitInfo2.enableIdentityCheck : enableIdentityCheck);
		attackHitInfo.isValidTriggerStay = ((!(rate <= 0f)) ? attackHitInfo2.isValidTriggerStay : isValidTriggerStay);
		attackHitInfo.toPlayer.reactionType = ((!(rate < 1f)) ? attackHitInfo2.toPlayer.reactionType : toPlayer.reactionType);
		attackHitInfo.toPlayer.reactionLoopTime = AttackInfo.GetRateValue(toPlayer.reactionLoopTime, attackHitInfo2.toPlayer.reactionLoopTime, rate);
		attackHitInfo.toPlayer.reactionBlowForce = AttackInfo.GetRateValue(toPlayer.reactionBlowForce, attackHitInfo2.toPlayer.reactionBlowForce, rate);
		attackHitInfo.toPlayer.reactionBlowAngle = AttackInfo.GetRateValue(toPlayer.reactionBlowAngle, attackHitInfo2.toPlayer.reactionBlowAngle, rate);
		attackHitInfo.toEnemy.reactionType = ((!(rate < 1f)) ? attackHitInfo2.toEnemy.reactionType : toEnemy.reactionType);
		attackHitInfo.toEnemy.hitStopTime = AttackInfo.GetRateValue(toEnemy.hitStopTime, attackHitInfo2.toEnemy.hitStopTime, rate);
		attackHitInfo.toEnemy.enemyHitStopTime = AttackInfo.GetRateValue(toEnemy.enemyHitStopTime, attackHitInfo2.toEnemy.enemyHitStopTime, rate);
		attackHitInfo.toEnemy.hitTypeName = ((!(rate < 1f)) ? attackHitInfo2.toEnemy.hitTypeName : toEnemy.hitTypeName);
		attackHitInfo.toEnemy.isSpecialAttack = ((!(rate < 1f)) ? attackHitInfo2.toEnemy.isSpecialAttack : toEnemy.isSpecialAttack);
		attackHitInfo.toEnemy.concussion = AttackInfo.GetRateValue(toEnemy.concussion, attackHitInfo2.toEnemy.concussion, rate);
		string[] array = (!(rate < 1f)) ? attackHitInfo2.damageUpLabels : damageUpLabels;
		if (array != null)
		{
			int num = array.Length;
			attackHitInfo.damageUpLabels = new string[num];
			if (num > 0)
			{
				array.CopyTo(attackHitInfo.damageUpLabels, 0);
			}
		}
		return attackHitInfo;
	}

	protected override AttackInfo CreateInfo()
	{
		return new AttackHitInfo();
	}

	public override void Copy(ref AttackInfo rInfo)
	{
		base.Copy(ref rInfo);
		AttackHitInfo attackHitInfo = rInfo as AttackHitInfo;
		if (attackHitInfo == null)
		{
			return;
		}
		attackHitInfo.atkRate = atkRate;
		attackHitInfo.isAtkIgnore = isAtkIgnore;
		attackHitInfo.down = down;
		attackHitInfo.isForceDown = isForceDown;
		attackHitInfo.shakeCameraPercent = shakeCameraPercent;
		attackHitInfo.shakeCycleTime = shakeCycleTime;
		attackHitInfo.enableIdentityCheck = enableIdentityCheck;
		attackHitInfo.isValidTriggerStay = isValidTriggerStay;
		attackHitInfo.hitIntervalTime = hitIntervalTime;
		attackHitInfo.hitEffectName = hitEffectName;
		attackHitInfo.playCommonHitSe = playCommonHitSe;
		attackHitInfo.hitSEID = hitSEID;
		attackHitInfo.playCommonHitEffect = playCommonHitEffect;
		attackHitInfo.remainEffectName = remainEffectName;
		attackHitInfo.absorptance = absorptance;
		attackHitInfo.isImmediateDeath = isImmediateDeath;
		attackHitInfo.canAttackGrabbedPlayer = canAttackGrabbedPlayer;
		attackHitInfo.damageNumAddGroup = damageNumAddGroup;
		attackHitInfo.dontIncreaseGauge = dontIncreaseGauge;
		attackHitInfo.isBreakObject = isBreakObject;
		attackHitInfo.attackType = attackType;
		attackHitInfo.elementType = elementType;
		attackHitInfo.spAttackType = spAttackType;
		attackHitInfo.toShieldAtk = toShieldAtk;
		attackHitInfo.toShieldCriticalRate = toShieldCriticalRate;
		attackHitInfo.toShieldElementCriticalRate = toShieldElementCriticalRate;
		attackHitInfo.hitHealRate = hitHealRate;
		attackHitInfo.atk.Copy(atk);
		attackHitInfo.badStatus.Copy(badStatus);
		attackHitInfo.toPlayer.Copy(toPlayer);
		attackHitInfo.toEnemy.Copy(toEnemy);
		attackHitInfo.grabInfo.Copy(grabInfo);
		attackHitInfo.restraintInfo.Copy(restraintInfo);
		attackHitInfo.electricShockInfo.Copy(electricShockInfo);
		attackHitInfo.soilShockInfo.Copy(soilShockInfo);
		attackHitInfo.inkSplashInfo.Copy(inkSplashInfo);
		if (damageUpLabels != null)
		{
			int num = damageUpLabels.Length;
			attackHitInfo.damageUpLabels = new string[num];
			if (num > 0)
			{
				damageUpLabels.CopyTo(attackHitInfo.damageUpLabels, 0);
			}
		}
		if (buffIDs != null)
		{
			int num2 = buffIDs.Length;
			attackHitInfo.buffIDs = new uint[buffIDs.Length];
			if (num2 > 0)
			{
				buffIDs.CopyTo(attackHitInfo.buffIDs, 0);
			}
		}
	}
}
