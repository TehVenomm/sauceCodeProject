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
		SNATCH
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
			SHAKE
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

		public void Copy(ToPlayer src)
		{
			reactionType = src.reactionType;
			reactionLoopTime = src.reactionLoopTime;
			reactionBlowForce = src.reactionBlowForce;
			reactionBlowAngle = src.reactionBlowAngle;
			isBuffCancellation = src.isBuffCancellation;
		}
	}

	[Serializable]
	public class ToEnemy
	{
		public enum REACTION_TYPE
		{
			NONE,
			DAMAGE,
			DOWN
		}

		[Serializable]
		public class DamageToRegionInfo
		{
			[Tooltip("部位へのダメ\u30fcジアップするか")]
			public bool isDamageUp;

			[Tooltip("部位へのダメ\u30fcジアップ倍率（N%アップ）")]
			public int damageUpPercent;
		}

		[Tooltip("リアクションタイプ")]
		public REACTION_TYPE reactionType;

		[Tooltip("ヒットストップ時間")]
		public float hitStopTime;

		[Tooltip("ヒットタイプ名(EnemyHitTypeTable)")]
		public string hitTypeName;

		[Tooltip("武器固有攻撃")]
		public bool isSpecialAttack;

		[Tooltip("WEAKに当たったときに怯む")]
		public bool isWeakHitReaction;

		[Tooltip("部位へのダメ\u30fcジアップ情報")]
		public DamageToRegionInfo damageToRegionInfo = new DamageToRegionInfo();

		public void Copy(ToEnemy src)
		{
			reactionType = src.reactionType;
			hitStopTime = src.hitStopTime;
			hitTypeName = src.hitTypeName;
			isSpecialAttack = src.isSpecialAttack;
			damageToRegionInfo = src.damageToRegionInfo;
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

	[Tooltip("状態異常値")]
	public BadStatus badStatus = new BadStatus();

	[Tooltip("カメラ揺れ大きさ")]
	public float shakeCameraPercent;

	[Tooltip("カメラ揺れ周期（0で共通設定")]
	public float shakeCycleTime;

	[Tooltip("多重ヒット無効フラグ")]
	public bool enableIdentityCheck = true;

	[Tooltip("ヒット間隔（秒。多重ヒット有効時")]
	public float hitIntervalTime = 1f;

	[Tooltip("ヒットエフェクト名")]
	public string hitEffectName;

	[Tooltip("共通SEを再生するか")]
	public bool playCommonHitSe;

	[Tooltip("ヒットSE ID")]
	public int hitSEID;

	[Tooltip("共通Effectを再生するか")]
	public bool playCommonHitEffect;

	[Tooltip("継続して残るエフェクト名")]
	public string remainEffectName;

	[Tooltip("HP吸収率 Playerは与えたダメ\u30fcジの割合,敵は最大HPの割合")]
	public int absorptance;

	[Tooltip("即死攻撃かどうか")]
	public bool isImmediateDeath;

	[Tooltip("掴み状態にダメ\u30fcジを与えるかどうか")]
	public bool canAttackGrabbedPlayer;

	[Tooltip("外からのダメ\u30fcジのオフセット用")]
	public int damageNumAddGroup;

	[Tooltip("この攻撃ではゲ\u30fcジ増えない")]
	public bool dontIncreaseGauge;

	[Tooltip("攻撃タイプ（特殊な攻撃指定用")]
	public ATTACK_TYPE attackType;

	[Tooltip("属性 (攻撃力で指定できない場合に使用)")]
	public ELEMENT_TYPE elementType = ELEMENT_TYPE.MAX;

	[Tooltip("武器SPアクションタイプ")]
	public SP_ATTACK_TYPE spAttackType;

	public ToPlayer toPlayer = new ToPlayer();

	public ToEnemy toEnemy = new ToEnemy();

	public GrabInfo grabInfo = new GrabInfo();

	public RestraintInfo restraintInfo = new RestraintInfo();

	public ElectricShockInfo electricShockInfo = new ElectricShockInfo();

	public InkSplashInfo inkSplashInfo = new InkSplashInfo();

	public uint[] buffIDs;

	[Tooltip("バリアに対する攻撃力")]
	public int toShieldAtk;

	[Tooltip("バリアに対するクリティカル時のレ\u30fcト")]
	public float toShieldCriticalRate;

	[Tooltip("バリアに対する属性のクリティカル時のレ\u30fcト")]
	public float toShieldElementCriticalRate;

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
		attackHitInfo.badStatus.paralyze = AttackInfo.GetRateValue(badStatus.paralyze, attackHitInfo2.badStatus.paralyze, rate);
		attackHitInfo.badStatus.poison = AttackInfo.GetRateValue(badStatus.poison, attackHitInfo2.badStatus.poison, rate);
		attackHitInfo.badStatus.burning = AttackInfo.GetRateValue(badStatus.burning, attackHitInfo2.badStatus.burning, rate);
		attackHitInfo.badStatus.speedDown = AttackInfo.GetRateValue(badStatus.speedDown, attackHitInfo2.badStatus.speedDown, rate);
		attackHitInfo.badStatus.attackSpeedDown = AttackInfo.GetRateValue(badStatus.attackSpeedDown, attackHitInfo2.badStatus.attackSpeedDown, rate);
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
		attackHitInfo.attackType = ((!(rate < 1f)) ? attackHitInfo2.attackType : attackType);
		attackHitInfo.spAttackType = ((!(rate < 1f)) ? attackHitInfo2.spAttackType : spAttackType);
		attackHitInfo.toPlayer.reactionType = ((!(rate < 1f)) ? attackHitInfo2.toPlayer.reactionType : toPlayer.reactionType);
		attackHitInfo.toPlayer.reactionLoopTime = AttackInfo.GetRateValue(toPlayer.reactionLoopTime, attackHitInfo2.toPlayer.reactionLoopTime, rate);
		attackHitInfo.toPlayer.reactionBlowForce = AttackInfo.GetRateValue(toPlayer.reactionBlowForce, attackHitInfo2.toPlayer.reactionBlowForce, rate);
		attackHitInfo.toPlayer.reactionBlowAngle = AttackInfo.GetRateValue(toPlayer.reactionBlowAngle, attackHitInfo2.toPlayer.reactionBlowAngle, rate);
		attackHitInfo.toEnemy.reactionType = ((!(rate < 1f)) ? attackHitInfo2.toEnemy.reactionType : toEnemy.reactionType);
		attackHitInfo.toEnemy.hitStopTime = AttackInfo.GetRateValue(toEnemy.hitStopTime, attackHitInfo2.toEnemy.hitStopTime, rate);
		attackHitInfo.toEnemy.hitTypeName = ((!(rate < 1f)) ? attackHitInfo2.toEnemy.hitTypeName : toEnemy.hitTypeName);
		attackHitInfo.toEnemy.isSpecialAttack = ((!(rate < 1f)) ? attackHitInfo2.toEnemy.isSpecialAttack : toEnemy.isSpecialAttack);
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
		if (attackHitInfo != null)
		{
			attackHitInfo.atkRate = atkRate;
			attackHitInfo.isAtkIgnore = isAtkIgnore;
			attackHitInfo.down = down;
			attackHitInfo.shakeCameraPercent = shakeCameraPercent;
			attackHitInfo.shakeCycleTime = shakeCycleTime;
			attackHitInfo.enableIdentityCheck = enableIdentityCheck;
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
			attackHitInfo.attackType = attackType;
			attackHitInfo.elementType = elementType;
			attackHitInfo.spAttackType = spAttackType;
			attackHitInfo.toShieldAtk = toShieldAtk;
			attackHitInfo.toShieldCriticalRate = toShieldCriticalRate;
			attackHitInfo.toShieldElementCriticalRate = toShieldElementCriticalRate;
			attackHitInfo.atk.Copy(atk);
			attackHitInfo.badStatus.Copy(badStatus);
			attackHitInfo.toPlayer.Copy(toPlayer);
			attackHitInfo.toEnemy.Copy(toEnemy);
			attackHitInfo.grabInfo.Copy(grabInfo);
			attackHitInfo.restraintInfo.Copy(restraintInfo);
			attackHitInfo.electricShockInfo.Copy(electricShockInfo);
			attackHitInfo.inkSplashInfo.Copy(inkSplashInfo);
			if (buffIDs != null)
			{
				int num = buffIDs.Length;
				attackHitInfo.buffIDs = new uint[buffIDs.Length];
				if (num > 0)
				{
					buffIDs.CopyTo(attackHitInfo.buffIDs, 0);
				}
			}
		}
	}
}
