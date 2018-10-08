using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BulletData : ScriptableObject
{
	public enum BULLET_TYPE
	{
		NORMAL,
		FALL,
		HOMING,
		CURVE,
		LASER,
		FUNNEL,
		MINE,
		TRACKING,
		UNDEAD,
		ICE_FLOOR,
		HIGH_EXPLOSIVE,
		DIG,
		ACTION_MINE,
		PRESENT,
		CANNONBALL,
		OBSTACLE,
		ZONE,
		DECOY,
		BREAKABLE,
		OBSTACLE_CYLINDER,
		SNATCH,
		PAIR_SWORDS_SOUL,
		PAIR_SWORDS_LASER,
		HEALING_HOMING,
		ARROW_SOUL,
		ENEMY_PRESENT,
		CRASH_BIT,
		BARRIER
	}

	public enum AXIS
	{
		NONE = -1,
		X,
		Y,
		Z
	}

	[Serializable]
	public class BulletBase
	{
		public string effectName;

		public bool useWeaponElementEffect;

		public float speed;

		public float radius;

		public Vector3 dispOffset = Vector3.zero;

		public Vector3 dispRotation = Vector3.zero;

		public Vector3 hitOffset = Vector3.zero;

		public float appearTime;

		public Vector3 timeStartScale = Vector3.one;

		public Vector3 timeEndScale = Vector3.one;

		public bool isCharacterHitDelete = true;

		public bool isObjectHitDelete = true;

		public bool isLandHit;

		public string landHiteffectName;

		public BulletData endBullet;

		public bool isBulletTakeoverTarget;

		public bool isEmitGround;

		public float capsuleHeight;

		public AXIS capsuleAxis = AXIS.NONE;

		public BulletBase GetRateBullet(BulletBase rate_info, float rate)
		{
			if (rate_info == null)
			{
				return this;
			}
			BulletBase bulletBase = new BulletBase();
			bulletBase.effectName = ((!(rate < 1f)) ? rate_info.effectName : effectName);
			bulletBase.useWeaponElementEffect = ((!(rate < 1f)) ? rate_info.useWeaponElementEffect : useWeaponElementEffect);
			bulletBase.speed = AttackInfo.GetRateValue(speed, rate_info.speed, rate);
			bulletBase.radius = AttackInfo.GetRateValue(radius, rate_info.radius, rate);
			bulletBase.dispOffset = AttackInfo.GetRateValue(dispOffset, rate_info.dispOffset, rate);
			bulletBase.dispRotation = AttackInfo.GetRateValue(dispRotation, rate_info.dispRotation, rate);
			bulletBase.hitOffset = AttackInfo.GetRateValue(hitOffset, rate_info.hitOffset, rate);
			bulletBase.appearTime = AttackInfo.GetRateValue(appearTime, rate_info.appearTime, rate);
			bulletBase.timeStartScale = AttackInfo.GetRateValue(timeStartScale, rate_info.timeStartScale, rate);
			bulletBase.timeEndScale = AttackInfo.GetRateValue(timeEndScale, rate_info.timeEndScale, rate);
			bulletBase.isCharacterHitDelete = AttackInfo.GetRateValue(isCharacterHitDelete, rate_info.isCharacterHitDelete, rate);
			bulletBase.isObjectHitDelete = AttackInfo.GetRateValue(isObjectHitDelete, rate_info.isObjectHitDelete, rate);
			bulletBase.isLandHit = AttackInfo.GetRateValue(isLandHit, rate_info.isLandHit, rate);
			bulletBase.landHiteffectName = ((!(rate < 1f)) ? rate_info.landHiteffectName : landHiteffectName);
			bulletBase.isBulletTakeoverTarget = AttackInfo.GetRateValue(isBulletTakeoverTarget, rate_info.isBulletTakeoverTarget, rate);
			bulletBase.isEmitGround = AttackInfo.GetRateValue(isEmitGround, rate_info.isEmitGround, rate);
			bulletBase.capsuleHeight = AttackInfo.GetRateValue(capsuleHeight, rate_info.capsuleHeight, rate);
			bulletBase.capsuleAxis = ((!(rate < 1f)) ? rate_info.capsuleAxis : capsuleAxis);
			return bulletBase;
		}

		public string GetEffectName(Player player = null)
		{
			if (!useWeaponElementEffect || (UnityEngine.Object)player == (UnityEngine.Object)null)
			{
				return effectName;
			}
			int currentWeaponElement = player.GetCurrentWeaponElement();
			if (currentWeaponElement >= 6)
			{
				return string.Empty;
			}
			return effectName + currentWeaponElement.ToString();
		}
	}

	[Serializable]
	public class BulletFall
	{
		public float gravityStartTime = -1f;

		public float gravityRate = 1f;

		public BulletFall GetRateBullet(BulletFall rate_info, float rate)
		{
			if (rate_info == null)
			{
				return this;
			}
			BulletFall bulletFall = new BulletFall();
			if (gravityStartTime < 0f || rate_info.gravityStartTime < 0f)
			{
				bulletFall.gravityStartTime = ((!(rate < 1f)) ? rate_info.gravityStartTime : gravityStartTime);
			}
			else
			{
				bulletFall.gravityStartTime = AttackInfo.GetRateValue(gravityStartTime, rate_info.gravityStartTime, rate);
			}
			bulletFall.gravityRate = AttackInfo.GetRateValue(gravityRate, rate_info.gravityRate, rate);
			return bulletFall;
		}
	}

	[Serializable]
	public class BulletCurve
	{
		public float curveAngle;

		public AnimationCurve curveAnim = AnimationCurve.Linear(0f, 0f, 1f, 0f);

		public AnimationCurve timeAnim = AnimationCurve.Linear(0f, 1f, 1f, 1f);

		public Vector3 curveAxis = new Vector3(0f, 1f, 0f);

		public float loopTime = 1f;

		public BulletCurve GetRateBullet(BulletCurve rate_info, float rate)
		{
			if (rate_info == null)
			{
				return this;
			}
			BulletCurve bulletCurve = new BulletCurve();
			bulletCurve.curveAngle = AttackInfo.GetRateValue(curveAngle, rate_info.curveAngle, rate);
			bulletCurve.curveAnim = ((!(rate < 1f)) ? rate_info.curveAnim : curveAnim);
			bulletCurve.curveAxis = AttackInfo.GetRateValue(curveAxis, rate_info.curveAxis, rate);
			bulletCurve.loopTime = AttackInfo.GetRateValue(loopTime, rate_info.loopTime, rate);
			bulletCurve.timeAnim = ((!(rate < 1f)) ? rate_info.timeAnim : timeAnim);
			return bulletCurve;
		}
	}

	[Serializable]
	public class BulletHoming
	{
		public float limitAngel;

		public float limitChangeStartTime;

		public float limitChangeAngel;

		public bool hightLock;

		public bool isTakeOverTarget;

		public float acceleration;

		public BulletHoming GetRateBullet(BulletHoming rate_info, float rate)
		{
			if (rate_info == null)
			{
				return this;
			}
			BulletHoming bulletHoming = new BulletHoming();
			bulletHoming.limitAngel = AttackInfo.GetRateValue(limitAngel, rate_info.limitAngel, rate);
			bulletHoming.limitChangeStartTime = AttackInfo.GetRateValue(limitChangeStartTime, rate_info.limitChangeStartTime, rate);
			bulletHoming.limitChangeAngel = AttackInfo.GetRateValue(limitChangeAngel, rate_info.limitChangeAngel, rate);
			bulletHoming.hightLock = AttackInfo.GetRateValue(hightLock, rate_info.hightLock, rate);
			bulletHoming.isTakeOverTarget = AttackInfo.GetRateValue(isTakeOverTarget, rate_info.isTakeOverTarget, rate);
			bulletHoming.acceleration = AttackInfo.GetRateValue(acceleration, rate_info.acceleration, rate);
			return bulletHoming;
		}
	}

	[Serializable]
	public class BulletLaser
	{
		public Vector3 offsetPosition = Vector3.zero;

		public float capsuleHeight = 1f;

		public float initAngleSpeed;

		public float addAngleSpeed;

		public float limitAngleSpeed;

		public bool isLinkPositionOnly;

		public BulletLaser GetRateBullet(BulletLaser srcInfo, float rate)
		{
			if (srcInfo == null)
			{
				return this;
			}
			BulletLaser bulletLaser = new BulletLaser();
			bulletLaser.offsetPosition = AttackInfo.GetRateValue(offsetPosition, srcInfo.offsetPosition, rate);
			bulletLaser.capsuleHeight = AttackInfo.GetRateValue(capsuleHeight, srcInfo.capsuleHeight, rate);
			bulletLaser.initAngleSpeed = AttackInfo.GetRateValue(initAngleSpeed, srcInfo.initAngleSpeed, rate);
			bulletLaser.addAngleSpeed = AttackInfo.GetRateValue(addAngleSpeed, srcInfo.addAngleSpeed, rate);
			bulletLaser.limitAngleSpeed = AttackInfo.GetRateValue(limitAngleSpeed, srcInfo.limitAngleSpeed, rate);
			bulletLaser.isLinkPositionOnly = AttackInfo.GetRateValue(isLinkPositionOnly, srcInfo.isLinkPositionOnly, rate);
			return bulletLaser;
		}
	}

	[Serializable]
	public class BulletFunnel
	{
		public Vector3 offsetPosition = Vector3.zero;

		public float floatingHeight;

		public float attackInterval;

		public float attackRange;

		public float rotateAngle;

		public float lookAtAngle;

		public BulletData bitBullet;

		public float searchRange;

		public BulletFunnel GetRateBullet(BulletFunnel srcInfo, float rate)
		{
			if (srcInfo == null)
			{
				return this;
			}
			BulletFunnel bulletFunnel = new BulletFunnel();
			bulletFunnel.offsetPosition = AttackInfo.GetRateValue(offsetPosition, srcInfo.offsetPosition, rate);
			bulletFunnel.floatingHeight = AttackInfo.GetRateValue(floatingHeight, srcInfo.floatingHeight, rate);
			bulletFunnel.attackInterval = AttackInfo.GetRateValue(attackInterval, srcInfo.attackInterval, rate);
			bulletFunnel.attackRange = AttackInfo.GetRateValue(attackRange, srcInfo.attackRange, rate);
			bulletFunnel.rotateAngle = AttackInfo.GetRateValue(rotateAngle, srcInfo.rotateAngle, rate);
			bulletFunnel.lookAtAngle = AttackInfo.GetRateValue(lookAtAngle, srcInfo.lookAtAngle, rate);
			bulletFunnel.bitBullet = bitBullet;
			bulletFunnel.searchRange = AttackInfo.GetRateValue(searchRange, srcInfo.searchRange, rate);
			return bulletFunnel;
		}
	}

	[Serializable]
	public class BulletMine
	{
		public float floatingHeight;

		public float floatingRate;

		public float slowDownRate;

		public float unbrakableTime;

		public BulletData explodeBullet;

		public bool isIgnoreHitEnemyMove;

		public bool isIgnoreHitEnemyAttack;

		public BulletMine GetRateBullet(BulletMine srcInfo, float rate)
		{
			if (srcInfo == null)
			{
				return this;
			}
			BulletMine bulletMine = new BulletMine();
			bulletMine.floatingHeight = AttackInfo.GetRateValue(floatingHeight, srcInfo.floatingHeight, rate);
			bulletMine.floatingRate = AttackInfo.GetRateValue(floatingRate, srcInfo.floatingRate, rate);
			bulletMine.slowDownRate = AttackInfo.GetRateValue(slowDownRate, srcInfo.slowDownRate, rate);
			bulletMine.explodeBullet = explodeBullet;
			bulletMine.isIgnoreHitEnemyMove = AttackInfo.GetRateValue(isIgnoreHitEnemyMove, srcInfo.isIgnoreHitEnemyMove, rate);
			bulletMine.isIgnoreHitEnemyAttack = AttackInfo.GetRateValue(isIgnoreHitEnemyAttack, srcInfo.isIgnoreHitEnemyAttack, rate);
			return bulletMine;
		}
	}

	[Serializable]
	public class BulletTracking
	{
		public float moveThreshold;

		public float attackInterval;

		public float emitInterval;

		public int emissionNum;

		public bool isPerfectTrack;

		public BulletData emissionBullet;

		public BulletTracking GetRateBullet(BulletTracking srcInfo, float rate)
		{
			if (srcInfo == null)
			{
				return this;
			}
			BulletTracking bulletTracking = new BulletTracking();
			bulletTracking.moveThreshold = AttackInfo.GetRateValue(moveThreshold, srcInfo.moveThreshold, rate);
			bulletTracking.attackInterval = AttackInfo.GetRateValue(attackInterval, srcInfo.attackInterval, rate);
			bulletTracking.emitInterval = AttackInfo.GetRateValue(emitInterval, srcInfo.emitInterval, rate);
			bulletTracking.emissionNum = AttackInfo.GetRateValue(emissionNum, srcInfo.emissionNum, rate);
			bulletTracking.isPerfectTrack = isPerfectTrack;
			bulletTracking.emissionBullet = emissionBullet;
			return bulletTracking;
		}
	}

	[Serializable]
	public class BulletUndead
	{
		public float floatingHeight;

		public float floatingCoef;

		public float attackInterval;

		public float attackRange;

		public float lookAtAngle;

		public BulletData closeBullet;

		public BulletUndead GetRateBullet(BulletUndead srcInfo, float rate)
		{
			if (srcInfo == null)
			{
				return this;
			}
			BulletUndead bulletUndead = new BulletUndead();
			bulletUndead.floatingHeight = AttackInfo.GetRateValue(floatingHeight, srcInfo.floatingHeight, rate);
			bulletUndead.floatingCoef = AttackInfo.GetRateValue(floatingCoef, srcInfo.floatingCoef, rate);
			bulletUndead.attackInterval = AttackInfo.GetRateValue(attackInterval, srcInfo.attackInterval, rate);
			bulletUndead.attackRange = AttackInfo.GetRateValue(attackRange, srcInfo.attackRange, rate);
			bulletUndead.lookAtAngle = AttackInfo.GetRateValue(lookAtAngle, srcInfo.lookAtAngle, rate);
			bulletUndead.closeBullet = closeBullet;
			return bulletUndead;
		}
	}

	[Serializable]
	public class BulletIceFloor
	{
		public enum TARGETING_TYPE
		{
			NONE,
			NODE_OFFSET,
			ALL_PLAYERS,
			RANDOM_CHOICE
		}

		public float duration;
	}

	[Serializable]
	public class BulletHighExplosive
	{
		public enum TARGETING_TYPE
		{
			ALL_PLAYERS,
			ALL_PLAYERS_EXCEPT_ME
		}

		public TARGETING_TYPE targetingType;
	}

	[Serializable]
	public class BulletDig
	{
		public float floatingHeight;

		public float attackDelay;

		public float attackRange;

		public float lookAtAngle;

		public BulletData flyOutBullet;

		public BulletDig GetRateBullet(BulletDig srcInfo, float rate)
		{
			if (srcInfo == null)
			{
				return this;
			}
			BulletDig bulletDig = new BulletDig();
			bulletDig.floatingHeight = AttackInfo.GetRateValue(floatingHeight, srcInfo.floatingHeight, rate);
			bulletDig.attackDelay = AttackInfo.GetRateValue(attackDelay, srcInfo.attackDelay, rate);
			bulletDig.attackRange = AttackInfo.GetRateValue(attackRange, srcInfo.attackRange, rate);
			bulletDig.lookAtAngle = AttackInfo.GetRateValue(lookAtAngle, srcInfo.lookAtAngle, rate);
			bulletDig.flyOutBullet = flyOutBullet;
			return bulletDig;
		}
	}

	[Serializable]
	public class BulletActionMine
	{
		public enum ACTION_TYPE
		{
			NONE,
			REFLECT,
			INTERMITTENT
		}

		public string appearEffectName = string.Empty;

		public float unbrakableTime;

		public BulletData explodeBullet;

		public bool isIgnoreHitEnemyMove;

		public bool isIgnoreHitEnemyAttack;

		public ACTION_TYPE actionType;

		public BulletData actionBullet;

		public string actionEffectName1 = string.Empty;

		public string actionEffectName2 = string.Empty;

		public Vector3 actionEffectOffset = Vector3.zero;

		public float actionCoolTime;

		public float targettingPlayerRate = 0.25f;

		public float settingRadius;

		public int settingNum;

		public float centerConcentration = 0.5f;

		public float settingHeight;

		public float settingNearLimit;

		public BulletActionMine GetRateBullet(BulletActionMine srcInfo, float rate)
		{
			if (srcInfo == null)
			{
				return this;
			}
			BulletActionMine bulletActionMine = new BulletActionMine();
			bulletActionMine.appearEffectName = appearEffectName;
			bulletActionMine.unbrakableTime = unbrakableTime;
			bulletActionMine.explodeBullet = explodeBullet;
			bulletActionMine.actionType = actionType;
			bulletActionMine.actionBullet = actionBullet;
			bulletActionMine.isIgnoreHitEnemyMove = AttackInfo.GetRateValue(isIgnoreHitEnemyMove, srcInfo.isIgnoreHitEnemyMove, rate);
			bulletActionMine.isIgnoreHitEnemyAttack = AttackInfo.GetRateValue(isIgnoreHitEnemyAttack, srcInfo.isIgnoreHitEnemyAttack, rate);
			bulletActionMine.actionEffectName1 = actionEffectName1;
			bulletActionMine.actionEffectName2 = actionEffectName2;
			bulletActionMine.actionEffectOffset = AttackInfo.GetRateValue(actionEffectOffset, srcInfo.actionEffectOffset, rate);
			bulletActionMine.actionCoolTime = actionCoolTime;
			bulletActionMine.targettingPlayerRate = targettingPlayerRate;
			bulletActionMine.settingRadius = AttackInfo.GetRateValue(settingRadius, srcInfo.settingRadius, rate);
			bulletActionMine.settingNum = settingNum;
			bulletActionMine.centerConcentration = centerConcentration;
			bulletActionMine.settingHeight = AttackInfo.GetRateValue(settingHeight, srcInfo.settingHeight, rate);
			bulletActionMine.settingNearLimit = AttackInfo.GetRateValue(settingNearLimit, srcInfo.settingNearLimit, rate);
			return bulletActionMine;
		}
	}

	[Serializable]
	public class BulletPresent
	{
		public enum TYPE
		{
			NONE,
			HEAL,
			BOMB
		}

		public enum LIFE_SPAN_TYPE
		{
			TIME,
			ENDLESS
		}

		public TYPE type;

		public LIFE_SPAN_TYPE lifeSpanType;

		public List<int> buffIds = new List<int>();
	}

	[Serializable]
	public class BulletEnemyPresent
	{
		public bool isHitEnemyMove;

		public bool isHitEnemyAttack;

		public int value;

		public CALCULATE_TYPE valueType = CALCULATE_TYPE.CONSTANT;

		public List<int> buffIds = new List<int>();
	}

	[Serializable]
	public class BulletCannonball
	{
		public float gravityStartTime = -1f;

		public float gravityRate = 1f;

		public BulletCannonball GetRateBullet(BulletCannonball rate_info, float rate)
		{
			return this;
		}
	}

	[Serializable]
	public class BulletObstacle
	{
		public float colliderStartTime;

		public bool isHitBreak = true;

		public BulletObstacle GetRateBullet(BulletObstacle rate_info, float rate)
		{
			return this;
		}
	}

	[Serializable]
	public class BulletZone
	{
		public enum TYPE
		{
			NONE,
			HEAL
		}

		public TYPE type;

		public float intervalTime;

		public HEAL_TYPE healType;

		public BuffParam.BUFFTYPE buffType = BuffParam.BUFFTYPE.NONE;

		public bool isCarry;
	}

	[Serializable]
	public class BulletDecoy
	{
		[Serializable]
		public class HateInfo
		{
			public Hate.TYPE type;

			public int value;
		}

		public float dontHitSec;

		public HateInfo[] addDecoyHate;

		public HateInfo[] addOwnerHate;

		public float hateDecreaseRate = 1f;

		public int explodeHitNum = 1;

		public float hateInterval;
	}

	[Serializable]
	public class BulletBreakable : BulletHoming
	{
		public enum MOVE_TYPE
		{
			NORMAL,
			HOMING
		}

		public MOVE_TYPE moveType;

		public int breakCount = 1;

		public bool isTakeOverHitCount;

		public bool isIgnoreHitAttackable;

		public bool isIgnoreHitEnemyBody;

		public bool isIgnoreHitEnemyMove;

		public bool isIgnoreHitEnemyAttack;

		public bool isIgnoreHitPlayerBody;

		public bool isIgnoreHitPlayerAttack;

		public bool isIgnoreHitWallAndObject;

		public int toEnduranceDamege;
	}

	[Serializable]
	public class BulletToEndurance : BulletHoming
	{
		public int toEnduranceDamage;
	}

	[Serializable]
	public class BulletObstacleCylinder
	{
		public int colliderNum = 8;

		public float radius = 1f;

		public Vector3 size = Vector3.one;

		public Vector3 center = Vector3.zero;
	}

	[Serializable]
	public class BulletSnatch
	{
		public float maxDistance;
	}

	[Serializable]
	public class BulletPairSwordsSoul
	{
	}

	[Serializable]
	public class BulletHealingHoming : BulletHoming
	{
		public bool isIgnoreColliderExceptTarget = true;

		public int defaultGenerateLayer = 14;

		public List<int> buffIds = new List<int>();

		public BulletHealingHoming()
		{
			isIgnoreColliderExceptTarget = true;
			defaultGenerateLayer = 14;
			buffIds.Clear();
		}

		public BulletHealingHoming(BulletHealingHoming _bulletHH)
		{
			limitAngel = _bulletHH.limitAngel;
			limitChangeStartTime = _bulletHH.limitChangeStartTime;
			limitChangeAngel = _bulletHH.limitChangeAngel;
			hightLock = _bulletHH.hightLock;
			isTakeOverTarget = _bulletHH.isTakeOverTarget;
			acceleration = _bulletHH.acceleration;
			isIgnoreColliderExceptTarget = _bulletHH.isIgnoreColliderExceptTarget;
			defaultGenerateLayer = _bulletHH.defaultGenerateLayer;
			buffIds = _bulletHH.buffIds;
		}

		public BulletHealingHoming CreateParamMergedInstance(BulletHealingHoming _target, float _ratio)
		{
			if (_target == null)
			{
				return this;
			}
			BulletHealingHoming bulletHealingHoming = new BulletHealingHoming();
			bulletHealingHoming.limitAngel = AttackInfo.GetRateValue(limitAngel, _target.limitAngel, _ratio);
			bulletHealingHoming.limitChangeStartTime = AttackInfo.GetRateValue(limitChangeStartTime, _target.limitChangeStartTime, _ratio);
			bulletHealingHoming.limitChangeAngel = AttackInfo.GetRateValue(limitChangeAngel, _target.limitChangeAngel, _ratio);
			bulletHealingHoming.hightLock = AttackInfo.GetRateValue(hightLock, _target.hightLock, _ratio);
			bulletHealingHoming.isTakeOverTarget = AttackInfo.GetRateValue(isTakeOverTarget, _target.isTakeOverTarget, _ratio);
			bulletHealingHoming.acceleration = AttackInfo.GetRateValue(acceleration, _target.acceleration, _ratio);
			bulletHealingHoming.isIgnoreColliderExceptTarget = AttackInfo.GetRateValue(isIgnoreColliderExceptTarget, _target.isIgnoreColliderExceptTarget, _ratio);
			bulletHealingHoming.defaultGenerateLayer = ((!(_ratio <= 0.5f)) ? _target.defaultGenerateLayer : defaultGenerateLayer);
			int i = 0;
			for (int count = buffIds.Count; i < count; i++)
			{
				bulletHealingHoming.buffIds.Add(AttackInfo.GetRateValue(buffIds[i], _target.buffIds[i], _ratio));
			}
			return bulletHealingHoming;
		}
	}

	[Serializable]
	public class BulletArrowSoul
	{
		public float accel;

		public float accelStartTime;

		public float maxSpeed;

		public float angularVelocity;

		public float angularStartTime;

		public float ignoreAngle;
	}

	[Serializable]
	public class BulletBarrier
	{
		public int baseHp;

		public int baseDef;

		public string effectNameInBarrier = string.Empty;
	}

	public BULLET_TYPE type;

	public BulletBase data = new BulletBase();

	public BulletFall dataFall;

	public BulletHoming dataHoming;

	public BulletCurve dataCurve;

	public BulletLaser dataLaser;

	public BulletFunnel dataFunnel;

	public BulletMine dataMine;

	public BulletTracking dataTracking;

	public BulletUndead dataUndead;

	public BulletIceFloor dataIceFloor;

	public BulletHighExplosive dataHighExplosive;

	public BulletDig dataDig;

	public BulletActionMine dataActionMine;

	public BulletPresent dataPresent;

	public BulletCannonball dataCannonball;

	public BulletObstacle dataObstacle;

	public BulletZone dataZone;

	public BulletDecoy dataDecoy;

	public BulletBreakable dataBreakable;

	public BulletToEndurance dataToEndurance;

	public BulletObstacleCylinder dataObstacleCylinder;

	public BulletSnatch dataSnatch;

	public BulletPairSwordsSoul dataPairSwordsSoul;

	public BulletHealingHoming dataHealingHomingBullet;

	public BulletArrowSoul dataArrowSoul;

	public BulletEnemyPresent dataEnemyPresent;

	public BulletBarrier dataBarrier;

	public BulletData GetRateBulletData(BulletData rate_info, float rate)
	{
		if ((UnityEngine.Object)rate_info == (UnityEngine.Object)null)
		{
			return this;
		}
		BulletData bulletData = ResourceUtility.Instantiate(this);
		bulletData.type = ((!(rate < 1f)) ? rate_info.type : type);
		bulletData.data = data.GetRateBullet(rate_info.data, rate);
		switch (bulletData.type)
		{
		case BULLET_TYPE.FALL:
			bulletData.dataFall = dataFall.GetRateBullet(rate_info.dataFall, rate);
			break;
		case BULLET_TYPE.HOMING:
			bulletData.dataHoming = dataHoming.GetRateBullet(rate_info.dataHoming, rate);
			break;
		case BULLET_TYPE.CURVE:
			bulletData.dataCurve = dataCurve.GetRateBullet(rate_info.dataCurve, rate);
			break;
		case BULLET_TYPE.LASER:
			bulletData.dataLaser = dataLaser.GetRateBullet(rate_info.dataLaser, rate);
			break;
		case BULLET_TYPE.FUNNEL:
			bulletData.dataFunnel = dataFunnel.GetRateBullet(rate_info.dataFunnel, rate);
			break;
		case BULLET_TYPE.MINE:
			bulletData.dataMine = dataMine.GetRateBullet(rate_info.dataMine, rate);
			break;
		case BULLET_TYPE.TRACKING:
			bulletData.dataTracking = dataTracking.GetRateBullet(rate_info.dataTracking, rate);
			break;
		case BULLET_TYPE.UNDEAD:
			bulletData.dataUndead = dataUndead.GetRateBullet(rate_info.dataUndead, rate);
			break;
		case BULLET_TYPE.ICE_FLOOR:
			bulletData.dataIceFloor = new BulletIceFloor();
			bulletData.dataIceFloor.duration = dataIceFloor.duration;
			break;
		case BULLET_TYPE.HIGH_EXPLOSIVE:
			bulletData.dataHighExplosive = new BulletHighExplosive();
			bulletData.dataHighExplosive.targetingType = dataHighExplosive.targetingType;
			break;
		case BULLET_TYPE.DIG:
			bulletData.dataDig = dataDig.GetRateBullet(rate_info.dataDig, rate);
			break;
		case BULLET_TYPE.ACTION_MINE:
			bulletData.dataActionMine = dataActionMine.GetRateBullet(rate_info.dataActionMine, rate);
			break;
		case BULLET_TYPE.PRESENT:
			bulletData.dataPresent = dataPresent;
			break;
		case BULLET_TYPE.CANNONBALL:
			bulletData.dataCannonball = new BulletCannonball();
			break;
		case BULLET_TYPE.OBSTACLE:
			bulletData.dataObstacle = new BulletObstacle();
			break;
		case BULLET_TYPE.ZONE:
			bulletData.dataZone = new BulletZone();
			break;
		case BULLET_TYPE.DECOY:
			bulletData.dataDecoy = new BulletDecoy();
			break;
		case BULLET_TYPE.BREAKABLE:
			bulletData.dataBreakable = new BulletBreakable();
			break;
		case BULLET_TYPE.OBSTACLE_CYLINDER:
			bulletData.dataObstacleCylinder = new BulletObstacleCylinder();
			break;
		case BULLET_TYPE.SNATCH:
			bulletData.dataSnatch = new BulletSnatch();
			break;
		case BULLET_TYPE.PAIR_SWORDS_SOUL:
			bulletData.dataPairSwordsSoul = new BulletPairSwordsSoul();
			break;
		case BULLET_TYPE.HEALING_HOMING:
			bulletData.dataHealingHomingBullet = dataHealingHomingBullet.CreateParamMergedInstance(rate_info.dataHealingHomingBullet, rate);
			break;
		case BULLET_TYPE.ARROW_SOUL:
			bulletData.dataArrowSoul = new BulletArrowSoul();
			break;
		case BULLET_TYPE.ENEMY_PRESENT:
			bulletData.dataEnemyPresent = dataEnemyPresent;
			break;
		case BULLET_TYPE.BARRIER:
			bulletData.dataBarrier = new BulletBarrier();
			break;
		}
		return bulletData;
	}
}
