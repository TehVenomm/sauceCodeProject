// Decompiled with JetBrains decompiler
// Type: AttackedHitStatusOwner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class AttackedHitStatusOwner
{
  public AttackedHitStatus origin { get; protected set; }

  public AttackedHitStatusOwner() => this.origin = new AttackedHitStatus();

  public AttackedHitStatusOwner(AttackedHitStatus status) => this.origin = status;

  public AttackHitInfo attackInfo => this.origin.attackInfo;

  public int fromObjectID => this.origin.fromObjectID;

  public StageObject fromObject => this.origin.fromObject;

  public StageObject.OBJECT_TYPE fromType => this.origin.fromType;

  public Vector3 fromPos => this.origin.fromPos;

  public Vector3 hitPos => this.origin.hitPos;

  public int fromClientID => this.origin.fromClientID;

  public SkillInfo.SkillParam skillParam => this.origin.skillParam;

  public bool validDamage => this.origin.validDamage;

  public BadStatus badStatusAdd => this.origin.badStatusAdd;

  public int regionID => this.origin.regionID;

  public bool isDamageRegionOnly => this.origin.isDamageRegionOnly;

  public Enemy.WEAK_STATE weakState => this.origin.weakState;

  public bool IsSpAttackHit => this.origin.isSpAttackHit;

  public int damage
  {
    get => this.origin.damage;
    set => this.origin.damage = value;
  }

  public AtkAttribute damageDetails
  {
    get => this.origin.damageDetails;
    set => this.origin.damageDetails = value;
  }

  public float downAddBase
  {
    get => this.origin.downAddBase;
    set => this.origin.downAddBase = value;
  }

  public float downAddWeak
  {
    get => this.origin.downAddWeak;
    set => this.origin.downAddWeak = value;
  }

  public bool isForceDown
  {
    get => this.origin.isForceDown;
    set => this.origin.isForceDown = value;
  }

  public float concussionAdd
  {
    get => this.origin.concussionAdd;
    set => this.origin.concussionAdd = value;
  }

  public bool isArrowBleed
  {
    get => this.origin.isArrowBleed;
    set => this.origin.isArrowBleed = value;
  }

  public int arrowBleedDamage => this.origin.arrowBleedDamage;

  public int arrowBurstDamage => this.origin.arrowBurstDamage;

  public bool isShadowSealing
  {
    get => this.origin.isShadowSealing;
    set => this.origin.isShadowSealing = value;
  }

  public bool isArrowBomb
  {
    get => this.origin.isArrowBomb;
    set => this.origin.isArrowBomb = value;
  }

  public Vector3 hostPos
  {
    get => this.origin.hostPos;
    set => this.origin.hostPos = value;
  }

  public float hostDir
  {
    get => this.origin.hostDir;
    set => this.origin.hostDir = value;
  }

  public int afterHP
  {
    get => this.origin.afterHP;
    set => this.origin.afterHP = value;
  }

  public int afterRegionHP
  {
    get => this.origin.afterRegionHP;
    set => this.origin.afterRegionHP = value;
  }

  public int afterHealHp
  {
    get => this.origin.afterHealHp;
    set => this.origin.afterHealHp = value;
  }

  public bool breakRegion
  {
    get => this.origin.breakRegion;
    set => this.origin.breakRegion = value;
  }

  public int reactionType
  {
    get => this.origin.reactionType;
    set => this.origin.reactionType = value;
  }

  public Vector3 blowForce
  {
    get => this.origin.blowForce;
    set => this.origin.blowForce = value;
  }

  public float downTotal
  {
    get => this.origin.downTotal;
    set => this.origin.downTotal = value;
  }

  public float concussionTotal
  {
    get => this.origin.concussionTotal;
    set => this.origin.concussionTotal = value;
  }

  public BadStatus badStatusTotal
  {
    get => this.origin.badStatusTotal;
    set => this.origin.badStatusTotal = value;
  }

  public float damageHpRate
  {
    get => this.origin.damageHpRate;
    set => this.origin.damageHpRate = value;
  }

  public bool arrowBleedSkipFirst
  {
    get => this.origin.arrowBleedSkipFirst;
    set => this.origin.arrowBleedSkipFirst = value;
  }

  public int afterBarrierHp
  {
    get => this.origin.barrierHp;
    set => this.origin.barrierHp = value;
  }

  public int afterShieldHp
  {
    get => this.origin.shieldHp;
    set => this.origin.shieldHp = value;
  }

  public int afterGrabHp
  {
    get => this.origin.grabHp;
    set => this.origin.grabHp = value;
  }

  public int shieldDamage
  {
    get => this.origin.shieldDamage;
    set => this.origin.shieldDamage = value;
  }

  public EnemyAegisController.SyncParam aegisParam
  {
    get => this.origin.aegisParam;
    set => this.origin.aegisParam = value;
  }

  public int deadReviveCount
  {
    get => this.origin.deadReviveCount;
    set => this.origin.deadReviveCount = value;
  }
}
