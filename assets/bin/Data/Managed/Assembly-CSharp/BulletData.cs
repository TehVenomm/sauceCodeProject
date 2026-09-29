// Decompiled with JetBrains decompiler
// Type: BulletData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[Serializable]
public class BulletData : ScriptableObject
{
  public BulletData.BULLET_TYPE type;
  public BulletData.BulletBase data = new BulletData.BulletBase();
  public BulletData.BulletFall dataFall;
  public BulletData.BulletHoming dataHoming;
  public BulletData.BulletCurve dataCurve;
  public BulletData.BulletLaser dataLaser;
  public BulletData.BulletFunnel dataFunnel;
  public BulletData.BulletMine dataMine;
  public BulletData.BulletTracking dataTracking;
  public BulletData.BulletUndead dataUndead;
  public BulletData.BulletIceFloor dataIceFloor;
  public BulletData.BulletHighExplosive dataHighExplosive;
  public BulletData.BulletDig dataDig;
  public BulletData.BulletActionMine dataActionMine;
  public BulletData.BulletPresent dataPresent;
  public BulletData.BulletCannonball dataCannonball;
  public BulletData.BulletObstacle dataObstacle;
  public BulletData.BulletZone dataZone;
  public BulletData.BulletDecoy dataDecoy;
  public BulletData.BulletBreakable dataBreakable;
  public BulletData.BulletToEndurance dataToEndurance;
  public BulletData.BulletObstacleCylinder dataObstacleCylinder;
  public BulletData.BulletSnatch dataSnatch;
  public BulletData.BulletPairSwordsSoul dataPairSwordsSoul;
  public BulletData.BulletHealingHoming dataHealingHomingBullet;
  public BulletData.BulletArrowSoul dataArrowSoul;
  public BulletData.BulletEnemyPresent dataEnemyPresent;
  public BulletData.BulletBarrier dataBarrier;
  public BulletData.BulletRotateBit dataRotateBit;
  public BulletData.BulletResurrectionHoming dataResurrectionHomingBullet;
  public BulletData.BulletSearch dataSearch;
  public BulletData.BulletFollow dataFollow;
  public BulletData.BulletTurretBit dataTurretBit;
  public BulletData.BulletOracleSpearSp dataOracleSpearSp;
  public BulletData.BulletDecoyTurretBit dataDecoyTurretBit;

  public bool IsDecoy()
  {
    switch (this.type)
    {
      case BulletData.BULLET_TYPE.DECOY:
      case BulletData.BULLET_TYPE.DECOY_TURRET_BIT:
        return true;
      default:
        return false;
    }
  }

  public BulletData GetRateBulletData(BulletData rate_info, float rate)
  {
    if (Object.op_Equality((Object) rate_info, (Object) null))
      return this;
    BulletData rateBulletData = ResourceUtility.Instantiate<BulletData>(this);
    rateBulletData.type = (double) rate < 1.0 ? this.type : rate_info.type;
    rateBulletData.data = this.data.GetRateBullet(rate_info.data, rate);
    switch (rateBulletData.type)
    {
      case BulletData.BULLET_TYPE.FALL:
        rateBulletData.dataFall = this.dataFall.GetRateBullet(rate_info.dataFall, rate);
        break;
      case BulletData.BULLET_TYPE.HOMING:
        rateBulletData.dataHoming = this.dataHoming.GetRateBullet(rate_info.dataHoming, rate);
        break;
      case BulletData.BULLET_TYPE.CURVE:
        rateBulletData.dataCurve = this.dataCurve.GetRateBullet(rate_info.dataCurve, rate);
        break;
      case BulletData.BULLET_TYPE.LASER:
        rateBulletData.dataLaser = this.dataLaser.GetRateBullet(rate_info.dataLaser, rate);
        break;
      case BulletData.BULLET_TYPE.FUNNEL:
        rateBulletData.dataFunnel = this.dataFunnel.GetRateBullet(rate_info.dataFunnel, rate);
        break;
      case BulletData.BULLET_TYPE.MINE:
        rateBulletData.dataMine = this.dataMine.GetRateBullet(rate_info.dataMine, rate);
        break;
      case BulletData.BULLET_TYPE.TRACKING:
        rateBulletData.dataTracking = this.dataTracking.GetRateBullet(rate_info.dataTracking, rate);
        break;
      case BulletData.BULLET_TYPE.UNDEAD:
        rateBulletData.dataUndead = this.dataUndead.GetRateBullet(rate_info.dataUndead, rate);
        break;
      case BulletData.BULLET_TYPE.ICE_FLOOR:
        rateBulletData.dataIceFloor = new BulletData.BulletIceFloor();
        rateBulletData.dataIceFloor.duration = this.dataIceFloor.duration;
        break;
      case BulletData.BULLET_TYPE.HIGH_EXPLOSIVE:
        rateBulletData.dataHighExplosive = new BulletData.BulletHighExplosive();
        rateBulletData.dataHighExplosive.targetingType = this.dataHighExplosive.targetingType;
        break;
      case BulletData.BULLET_TYPE.DIG:
        rateBulletData.dataDig = this.dataDig.GetRateBullet(rate_info.dataDig, rate);
        break;
      case BulletData.BULLET_TYPE.ACTION_MINE:
        rateBulletData.dataActionMine = this.dataActionMine.GetRateBullet(rate_info.dataActionMine, rate);
        break;
      case BulletData.BULLET_TYPE.PRESENT:
        rateBulletData.dataPresent = this.dataPresent;
        break;
      case BulletData.BULLET_TYPE.CANNONBALL:
        rateBulletData.dataCannonball = new BulletData.BulletCannonball();
        break;
      case BulletData.BULLET_TYPE.OBSTACLE:
        rateBulletData.dataObstacle = new BulletData.BulletObstacle();
        break;
      case BulletData.BULLET_TYPE.ZONE:
        rateBulletData.dataZone = new BulletData.BulletZone();
        break;
      case BulletData.BULLET_TYPE.DECOY:
        rateBulletData.dataDecoy = new BulletData.BulletDecoy();
        break;
      case BulletData.BULLET_TYPE.BREAKABLE:
        rateBulletData.dataBreakable = new BulletData.BulletBreakable();
        break;
      case BulletData.BULLET_TYPE.OBSTACLE_CYLINDER:
        rateBulletData.dataObstacleCylinder = new BulletData.BulletObstacleCylinder();
        break;
      case BulletData.BULLET_TYPE.SNATCH:
        rateBulletData.dataSnatch = new BulletData.BulletSnatch();
        break;
      case BulletData.BULLET_TYPE.PAIR_SWORDS_SOUL:
        rateBulletData.dataPairSwordsSoul = new BulletData.BulletPairSwordsSoul();
        break;
      case BulletData.BULLET_TYPE.HEALING_HOMING:
        rateBulletData.dataHealingHomingBullet = this.dataHealingHomingBullet.CreateParamMergedInstance(rate_info.dataHealingHomingBullet, rate);
        break;
      case BulletData.BULLET_TYPE.ARROW_SOUL:
        rateBulletData.dataArrowSoul = new BulletData.BulletArrowSoul();
        break;
      case BulletData.BULLET_TYPE.ENEMY_PRESENT:
        rateBulletData.dataEnemyPresent = this.dataEnemyPresent;
        break;
      case BulletData.BULLET_TYPE.BARRIER:
        rateBulletData.dataBarrier = new BulletData.BulletBarrier();
        break;
      case BulletData.BULLET_TYPE.ROTATE_BIT:
        rateBulletData.dataRotateBit = new BulletData.BulletRotateBit();
        break;
      case BulletData.BULLET_TYPE.RESURRECTION_HOMING:
        rateBulletData.dataResurrectionHomingBullet = this.dataResurrectionHomingBullet.CreateParamMergedInstance(rate_info.dataResurrectionHomingBullet, rate);
        break;
      case BulletData.BULLET_TYPE.SEARCH:
        rateBulletData.dataSearch = new BulletData.BulletSearch();
        break;
      case BulletData.BULLET_TYPE.DECOY_TURRET_BIT:
        rateBulletData.dataDecoyTurretBit = new BulletData.BulletDecoyTurretBit();
        break;
    }
    return rateBulletData;
  }

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
    BARRIER,
    ROTATE_BIT,
    SPEAR_BARRIER,
    RESURRECTION_HOMING,
    SEARCH,
    TURRET_BIT,
    RANDOM_HOMING,
    ORACLE_SPEAR_SP,
    DECOY_TURRET_BIT,
  }

  public enum AXIS
  {
    NONE = -1, // 0xFFFFFFFF
    X = 0,
    Y = 1,
    Z = 2,
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
    public BulletData.AXIS capsuleAxis = BulletData.AXIS.NONE;

    public BulletData.BulletBase GetRateBullet(BulletData.BulletBase rate_info, float rate)
    {
      if (rate_info == null)
        return this;
      return new BulletData.BulletBase()
      {
        effectName = (double) rate < 1.0 ? this.effectName : rate_info.effectName,
        useWeaponElementEffect = (double) rate < 1.0 ? this.useWeaponElementEffect : rate_info.useWeaponElementEffect,
        speed = AttackInfo.GetRateValue(this.speed, rate_info.speed, rate),
        radius = AttackInfo.GetRateValue(this.radius, rate_info.radius, rate),
        dispOffset = AttackInfo.GetRateValue(this.dispOffset, rate_info.dispOffset, rate),
        dispRotation = AttackInfo.GetRateValue(this.dispRotation, rate_info.dispRotation, rate),
        hitOffset = AttackInfo.GetRateValue(this.hitOffset, rate_info.hitOffset, rate),
        appearTime = AttackInfo.GetRateValue(this.appearTime, rate_info.appearTime, rate),
        timeStartScale = AttackInfo.GetRateValue(this.timeStartScale, rate_info.timeStartScale, rate),
        timeEndScale = AttackInfo.GetRateValue(this.timeEndScale, rate_info.timeEndScale, rate),
        isCharacterHitDelete = AttackInfo.GetRateValue(this.isCharacterHitDelete, rate_info.isCharacterHitDelete, rate),
        isObjectHitDelete = AttackInfo.GetRateValue(this.isObjectHitDelete, rate_info.isObjectHitDelete, rate),
        isLandHit = AttackInfo.GetRateValue(this.isLandHit, rate_info.isLandHit, rate),
        landHiteffectName = (double) rate < 1.0 ? this.landHiteffectName : rate_info.landHiteffectName,
        isBulletTakeoverTarget = AttackInfo.GetRateValue(this.isBulletTakeoverTarget, rate_info.isBulletTakeoverTarget, rate),
        isEmitGround = AttackInfo.GetRateValue(this.isEmitGround, rate_info.isEmitGround, rate),
        capsuleHeight = AttackInfo.GetRateValue(this.capsuleHeight, rate_info.capsuleHeight, rate),
        capsuleAxis = (double) rate < 1.0 ? this.capsuleAxis : rate_info.capsuleAxis
      };
    }

    public string GetEffectName(Player player = null)
    {
      if (!this.useWeaponElementEffect || Object.op_Equality((Object) player, (Object) null))
        return this.effectName;
      int currentWeaponElement = player.GetCurrentWeaponElement();
      return currentWeaponElement >= 6 ? "" : this.effectName + currentWeaponElement.ToString();
    }
  }

  [Serializable]
  public class BulletFall
  {
    public float gravityStartTime = -1f;
    public float gravityRate = 1f;

    public BulletData.BulletFall GetRateBullet(BulletData.BulletFall rate_info, float rate)
    {
      if (rate_info == null)
        return this;
      return new BulletData.BulletFall()
      {
        gravityStartTime = (double) this.gravityStartTime < 0.0 || (double) rate_info.gravityStartTime < 0.0 ? ((double) rate < 1.0 ? this.gravityStartTime : rate_info.gravityStartTime) : AttackInfo.GetRateValue(this.gravityStartTime, rate_info.gravityStartTime, rate),
        gravityRate = AttackInfo.GetRateValue(this.gravityRate, rate_info.gravityRate, rate)
      };
    }
  }

  [Serializable]
  public class BulletCurve
  {
    public float curveAngle;
    public AnimationCurve curveAnim = AnimationCurve.Linear(0.0f, 0.0f, 1f, 0.0f);
    public AnimationCurve timeAnim = AnimationCurve.Linear(0.0f, 1f, 1f, 1f);
    public Vector3 curveAxis = new Vector3(0.0f, 1f, 0.0f);
    public float loopTime = 1f;

    public BulletData.BulletCurve GetRateBullet(BulletData.BulletCurve rate_info, float rate)
    {
      if (rate_info == null)
        return this;
      return new BulletData.BulletCurve()
      {
        curveAngle = AttackInfo.GetRateValue(this.curveAngle, rate_info.curveAngle, rate),
        curveAnim = (double) rate < 1.0 ? this.curveAnim : rate_info.curveAnim,
        curveAxis = AttackInfo.GetRateValue(this.curveAxis, rate_info.curveAxis, rate),
        loopTime = AttackInfo.GetRateValue(this.loopTime, rate_info.loopTime, rate),
        timeAnim = (double) rate < 1.0 ? this.timeAnim : rate_info.timeAnim
      };
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

    public BulletData.BulletHoming GetRateBullet(BulletData.BulletHoming rate_info, float rate)
    {
      if (rate_info == null)
        return this;
      return new BulletData.BulletHoming()
      {
        limitAngel = AttackInfo.GetRateValue(this.limitAngel, rate_info.limitAngel, rate),
        limitChangeStartTime = AttackInfo.GetRateValue(this.limitChangeStartTime, rate_info.limitChangeStartTime, rate),
        limitChangeAngel = AttackInfo.GetRateValue(this.limitChangeAngel, rate_info.limitChangeAngel, rate),
        hightLock = AttackInfo.GetRateValue(this.hightLock, rate_info.hightLock, rate),
        isTakeOverTarget = AttackInfo.GetRateValue(this.isTakeOverTarget, rate_info.isTakeOverTarget, rate),
        acceleration = AttackInfo.GetRateValue(this.acceleration, rate_info.acceleration, rate)
      };
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

    public BulletData.BulletLaser GetRateBullet(BulletData.BulletLaser srcInfo, float rate)
    {
      if (srcInfo == null)
        return this;
      return new BulletData.BulletLaser()
      {
        offsetPosition = AttackInfo.GetRateValue(this.offsetPosition, srcInfo.offsetPosition, rate),
        capsuleHeight = AttackInfo.GetRateValue(this.capsuleHeight, srcInfo.capsuleHeight, rate),
        initAngleSpeed = AttackInfo.GetRateValue(this.initAngleSpeed, srcInfo.initAngleSpeed, rate),
        addAngleSpeed = AttackInfo.GetRateValue(this.addAngleSpeed, srcInfo.addAngleSpeed, rate),
        limitAngleSpeed = AttackInfo.GetRateValue(this.limitAngleSpeed, srcInfo.limitAngleSpeed, rate),
        isLinkPositionOnly = AttackInfo.GetRateValue(this.isLinkPositionOnly, srcInfo.isLinkPositionOnly, rate)
      };
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
    public string finalAtkInfoName;

    public BulletData.BulletFunnel GetRateBullet(BulletData.BulletFunnel srcInfo, float rate)
    {
      if (srcInfo == null)
        return this;
      return new BulletData.BulletFunnel()
      {
        offsetPosition = AttackInfo.GetRateValue(this.offsetPosition, srcInfo.offsetPosition, rate),
        floatingHeight = AttackInfo.GetRateValue(this.floatingHeight, srcInfo.floatingHeight, rate),
        attackInterval = AttackInfo.GetRateValue(this.attackInterval, srcInfo.attackInterval, rate),
        attackRange = AttackInfo.GetRateValue(this.attackRange, srcInfo.attackRange, rate),
        rotateAngle = AttackInfo.GetRateValue(this.rotateAngle, srcInfo.rotateAngle, rate),
        lookAtAngle = AttackInfo.GetRateValue(this.lookAtAngle, srcInfo.lookAtAngle, rate),
        bitBullet = this.bitBullet,
        searchRange = AttackInfo.GetRateValue(this.searchRange, srcInfo.searchRange, rate),
        finalAtkInfoName = this.finalAtkInfoName
      };
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

    public BulletData.BulletMine GetRateBullet(BulletData.BulletMine srcInfo, float rate)
    {
      if (srcInfo == null)
        return this;
      return new BulletData.BulletMine()
      {
        floatingHeight = AttackInfo.GetRateValue(this.floatingHeight, srcInfo.floatingHeight, rate),
        floatingRate = AttackInfo.GetRateValue(this.floatingRate, srcInfo.floatingRate, rate),
        slowDownRate = AttackInfo.GetRateValue(this.slowDownRate, srcInfo.slowDownRate, rate),
        explodeBullet = this.explodeBullet,
        isIgnoreHitEnemyMove = AttackInfo.GetRateValue(this.isIgnoreHitEnemyMove, srcInfo.isIgnoreHitEnemyMove, rate),
        isIgnoreHitEnemyAttack = AttackInfo.GetRateValue(this.isIgnoreHitEnemyAttack, srcInfo.isIgnoreHitEnemyAttack, rate)
      };
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

    public BulletData.BulletTracking GetRateBullet(BulletData.BulletTracking srcInfo, float rate)
    {
      if (srcInfo == null)
        return this;
      return new BulletData.BulletTracking()
      {
        moveThreshold = AttackInfo.GetRateValue(this.moveThreshold, srcInfo.moveThreshold, rate),
        attackInterval = AttackInfo.GetRateValue(this.attackInterval, srcInfo.attackInterval, rate),
        emitInterval = AttackInfo.GetRateValue(this.emitInterval, srcInfo.emitInterval, rate),
        emissionNum = AttackInfo.GetRateValue(this.emissionNum, srcInfo.emissionNum, rate),
        isPerfectTrack = this.isPerfectTrack,
        emissionBullet = this.emissionBullet
      };
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

    public BulletData.BulletUndead GetRateBullet(BulletData.BulletUndead srcInfo, float rate)
    {
      if (srcInfo == null)
        return this;
      return new BulletData.BulletUndead()
      {
        floatingHeight = AttackInfo.GetRateValue(this.floatingHeight, srcInfo.floatingHeight, rate),
        floatingCoef = AttackInfo.GetRateValue(this.floatingCoef, srcInfo.floatingCoef, rate),
        attackInterval = AttackInfo.GetRateValue(this.attackInterval, srcInfo.attackInterval, rate),
        attackRange = AttackInfo.GetRateValue(this.attackRange, srcInfo.attackRange, rate),
        lookAtAngle = AttackInfo.GetRateValue(this.lookAtAngle, srcInfo.lookAtAngle, rate),
        closeBullet = this.closeBullet
      };
    }
  }

  [Serializable]
  public class BulletIceFloor
  {
    public float duration;

    public enum TARGETING_TYPE
    {
      NONE,
      NODE_OFFSET,
      ALL_PLAYERS,
      RANDOM_CHOICE,
    }
  }

  [Serializable]
  public class BulletHighExplosive
  {
    public BulletData.BulletHighExplosive.TARGETING_TYPE targetingType;

    public enum TARGETING_TYPE
    {
      ALL_PLAYERS,
      ALL_PLAYERS_EXCEPT_ME,
    }
  }

  [Serializable]
  public class BulletDig
  {
    public float floatingHeight;
    public float attackDelay;
    public float attackRange;
    public float lookAtAngle;
    public BulletData flyOutBullet;

    public BulletData.BulletDig GetRateBullet(BulletData.BulletDig srcInfo, float rate)
    {
      if (srcInfo == null)
        return this;
      return new BulletData.BulletDig()
      {
        floatingHeight = AttackInfo.GetRateValue(this.floatingHeight, srcInfo.floatingHeight, rate),
        attackDelay = AttackInfo.GetRateValue(this.attackDelay, srcInfo.attackDelay, rate),
        attackRange = AttackInfo.GetRateValue(this.attackRange, srcInfo.attackRange, rate),
        lookAtAngle = AttackInfo.GetRateValue(this.lookAtAngle, srcInfo.lookAtAngle, rate),
        flyOutBullet = this.flyOutBullet
      };
    }
  }

  [Serializable]
  public class BulletActionMine
  {
    public string appearEffectName = "";
    public float unbrakableTime;
    public BulletData explodeBullet;
    public bool isIgnoreHitEnemyMove;
    public bool isIgnoreHitEnemyAttack;
    public BulletData.BulletActionMine.ACTION_TYPE actionType;
    public BulletData actionBullet;
    public string actionEffectName1 = "";
    public string actionEffectName2 = "";
    public Vector3 actionEffectOffset = Vector3.zero;
    public float actionCoolTime;
    public float targettingPlayerRate = 0.25f;
    public float settingRadius;
    public int settingNum;
    public float centerConcentration = 0.5f;
    public float settingHeight;
    public float settingNearLimit;

    public BulletData.BulletActionMine GetRateBullet(
      BulletData.BulletActionMine srcInfo,
      float rate)
    {
      if (srcInfo == null)
        return this;
      return new BulletData.BulletActionMine()
      {
        appearEffectName = this.appearEffectName,
        unbrakableTime = this.unbrakableTime,
        explodeBullet = this.explodeBullet,
        actionType = this.actionType,
        actionBullet = this.actionBullet,
        isIgnoreHitEnemyMove = AttackInfo.GetRateValue(this.isIgnoreHitEnemyMove, srcInfo.isIgnoreHitEnemyMove, rate),
        isIgnoreHitEnemyAttack = AttackInfo.GetRateValue(this.isIgnoreHitEnemyAttack, srcInfo.isIgnoreHitEnemyAttack, rate),
        actionEffectName1 = this.actionEffectName1,
        actionEffectName2 = this.actionEffectName2,
        actionEffectOffset = AttackInfo.GetRateValue(this.actionEffectOffset, srcInfo.actionEffectOffset, rate),
        actionCoolTime = this.actionCoolTime,
        targettingPlayerRate = this.targettingPlayerRate,
        settingRadius = AttackInfo.GetRateValue(this.settingRadius, srcInfo.settingRadius, rate),
        settingNum = this.settingNum,
        centerConcentration = this.centerConcentration,
        settingHeight = AttackInfo.GetRateValue(this.settingHeight, srcInfo.settingHeight, rate),
        settingNearLimit = AttackInfo.GetRateValue(this.settingNearLimit, srcInfo.settingNearLimit, rate)
      };
    }

    public enum ACTION_TYPE
    {
      NONE,
      REFLECT,
      INTERMITTENT,
    }
  }

  [Serializable]
  public class BulletPresent
  {
    public BulletData.BulletPresent.TYPE type;
    public BulletData.BulletPresent.LIFE_SPAN_TYPE lifeSpanType;
    public List<int> buffIds = new List<int>();

    public enum TYPE
    {
      NONE,
      HEAL,
      BOMB,
    }

    public enum LIFE_SPAN_TYPE
    {
      TIME,
      ENDLESS,
    }
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

    public BulletData.BulletCannonball GetRateBullet(
      BulletData.BulletCannonball rate_info,
      float rate)
    {
      return this;
    }
  }

  [Serializable]
  public class BulletObstacle
  {
    public float colliderStartTime;
    public bool isHitBreak = true;

    public BulletData.BulletObstacle GetRateBullet(BulletData.BulletObstacle rate_info, float rate)
    {
      return this;
    }
  }

  [Serializable]
  public class BulletZone
  {
    public BulletData.BulletZone.TYPE type;
    public float intervalTime;
    public HEAL_TYPE healType;
    public BuffParam.BUFFTYPE buffType = BuffParam.BUFFTYPE.NONE;
    public bool isCarry;

    public enum TYPE
    {
      NONE,
      HEAL,
    }
  }

  [Serializable]
  public class BulletDecoy
  {
    public float dontHitSec;
    public BulletData.BulletDecoy.HateInfo[] addDecoyHate;
    public BulletData.BulletDecoy.HateInfo[] addOwnerHate;
    public float hateDecreaseRate = 1f;
    public int explodeHitNum = 1;
    public float hateInterval;

    [Serializable]
    public class HateInfo
    {
      public Hate.TYPE type;
      public int value;
    }
  }

  [Serializable]
  public class BulletBreakable : BulletData.BulletHoming
  {
    public BulletData.BulletBreakable.MOVE_TYPE moveType;
    public int breakCount = 1;
    public bool isTakeOverHitCount;
    public bool isIgnoreHitEnemyBody;
    public bool isIgnoreHitEnemyMove;
    public bool isIgnoreHitEnemyAttack;
    public bool isIgnoreHitPlayerBody;
    public bool isIgnoreHitPlayerAttack;
    public bool isIgnoreHitWallAndObject;
    public bool isIgnoreHitCountPlayerBody = true;
    public BulletData emissionBulletOnBroken;
    public string emissionBulletAttackInfoName;
    public int toEnduranceDamege;

    public enum MOVE_TYPE
    {
      NORMAL,
      HOMING,
    }
  }

  [Serializable]
  public class BulletToEndurance : BulletData.BulletHoming
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
  public class BulletHealingHoming : BulletData.BulletHoming
  {
    public bool isIgnoreColliderExceptTarget = true;
    public int defaultGenerateLayer = 14;
    public List<int> buffIds = new List<int>();

    public BulletHealingHoming()
    {
      this.isIgnoreColliderExceptTarget = true;
      this.defaultGenerateLayer = 14;
      this.buffIds.Clear();
    }

    public BulletHealingHoming(BulletData.BulletHealingHoming _bulletHH)
    {
      this.limitAngel = _bulletHH.limitAngel;
      this.limitChangeStartTime = _bulletHH.limitChangeStartTime;
      this.limitChangeAngel = _bulletHH.limitChangeAngel;
      this.hightLock = _bulletHH.hightLock;
      this.isTakeOverTarget = _bulletHH.isTakeOverTarget;
      this.acceleration = _bulletHH.acceleration;
      this.isIgnoreColliderExceptTarget = _bulletHH.isIgnoreColliderExceptTarget;
      this.defaultGenerateLayer = _bulletHH.defaultGenerateLayer;
      this.buffIds = _bulletHH.buffIds;
    }

    public BulletData.BulletHealingHoming CreateParamMergedInstance(
      BulletData.BulletHealingHoming _target,
      float _ratio)
    {
      if (_target == null)
        return this;
      BulletData.BulletHealingHoming paramMergedInstance = new BulletData.BulletHealingHoming();
      paramMergedInstance.MergeParam(_target, _ratio);
      return paramMergedInstance;
    }

    protected void MergeParam(BulletData.BulletHealingHoming _target, float _ratio)
    {
      this.limitAngel = AttackInfo.GetRateValue(this.limitAngel, _target.limitAngel, _ratio);
      this.limitChangeStartTime = AttackInfo.GetRateValue(this.limitChangeStartTime, _target.limitChangeStartTime, _ratio);
      this.limitChangeAngel = AttackInfo.GetRateValue(this.limitChangeAngel, _target.limitChangeAngel, _ratio);
      this.hightLock = AttackInfo.GetRateValue(this.hightLock, _target.hightLock, _ratio);
      this.isTakeOverTarget = AttackInfo.GetRateValue(this.isTakeOverTarget, _target.isTakeOverTarget, _ratio);
      this.acceleration = AttackInfo.GetRateValue(this.acceleration, _target.acceleration, _ratio);
      this.isIgnoreColliderExceptTarget = AttackInfo.GetRateValue(this.isIgnoreColliderExceptTarget, _target.isIgnoreColliderExceptTarget, _ratio);
      this.defaultGenerateLayer = (double) _ratio <= 0.5 ? this.defaultGenerateLayer : _target.defaultGenerateLayer;
      int index = 0;
      for (int count = this.buffIds.Count; index < count; ++index)
        this.buffIds.Add(AttackInfo.GetRateValue(this.buffIds[index], _target.buffIds[index], _ratio));
    }
  }

  [Serializable]
  public class BulletResurrectionHoming : BulletData.BulletHealingHoming
  {
    public bool isAddBuffActionOnResurrected = true;

    public BulletResurrectionHoming() => this.isAddBuffActionOnResurrected = true;

    public BulletResurrectionHoming(BulletData.BulletResurrectionHoming _bulletRH)
      : base((BulletData.BulletHealingHoming) _bulletRH)
    {
      this.isAddBuffActionOnResurrected = _bulletRH.isAddBuffActionOnResurrected;
    }

    public BulletData.BulletResurrectionHoming CreateParamMergedInstance(
      BulletData.BulletResurrectionHoming _target,
      float _ratio)
    {
      if (_target == null)
        return this;
      BulletData.BulletResurrectionHoming paramMergedInstance = new BulletData.BulletResurrectionHoming();
      paramMergedInstance.MergeParam(_target, _ratio);
      return paramMergedInstance;
    }

    protected void MergeParam(BulletData.BulletResurrectionHoming _target, float _ratio)
    {
      this.MergeParam((BulletData.BulletHealingHoming) _target, _ratio);
      this.isAddBuffActionOnResurrected = AttackInfo.GetRateValue(this.isAddBuffActionOnResurrected, _target.isAddBuffActionOnResurrected, _ratio);
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

  [Serializable]
  public class BulletRotateBit
  {
    public bool isSetCentralPosition;
    public Vector3 centralPosition;
    public float rotateAngle_Deg;
    public float rotateRadius;
    public Vector3 rotateAxis;
    public int rotateSign;
    public float waitTime;
    public bool isLinearAngleSpeedUp;
    public float speedUpTime;
  }

  [Serializable]
  public class BulletSearch
  {
    public float searchStartTime;
    public float searchRangeSqr;
    public float angularVelocity;
  }

  [Serializable]
  public class BulletFollow
  {
    public Vector3 followOffset;
    public float attenuation;
  }

  [Serializable]
  public class BulletTurretBit : BulletData.BulletFollow
  {
    public float firstShotDelay_Sec;
    public float shotInterval_Sec;
    public string normalAtkInfoName;
    public string finalAtkInfoName;
    public float lookAtInterpolate;
    public float searchInterval_Sec;
    public float searchRangeSqr;
    public int uniqueId;
  }

  [Serializable]
  public class BulletOracleSpearSp
  {
    public string chargedEffectName;
    public bool useWeaponElementEffect;
    public int chargedSEId;

    public string GetEffectName(Player player)
    {
      return !this.useWeaponElementEffect || Object.op_Equality((Object) player, (Object) null) ? this.chargedEffectName : this.chargedEffectName + (object) player.GetCurrentWeaponElement();
    }
  }

  [Serializable]
  public class BulletDecoyTurretBit : BulletData.BulletDecoy
  {
    public float firstShotDelay_Sec;
    public float shotInterval_Sec;
    public string normalAtkInfoName = "";
    public string finalAtkInfoName = "";
    public float lookAtInterpolate;
    public float searchInterval_Sec;
    public float searchRangeSqr;
    public string rotateNodeName = "";
    public string chargeEffectName = "";
    public string chargeEffectParentNodename = "";
    public Vector3 chargeEffectDispOffset;
    public Vector3 chargeEffectDispRotation;
  }
}
