// Decompiled with JetBrains decompiler
// Type: AttackedHitStatusFix
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class AttackedHitStatusFix
{
  public AttackedHitStatus origin { get; protected set; }

  public AttackedHitStatusFix() => this.origin = new AttackedHitStatus();

  public AttackedHitStatusFix(AttackedHitStatus status) => this.origin = status;

  public AttackHitInfo attackInfo => this.origin.attackInfo;

  public int fromObjectID => this.origin.fromObjectID;

  public StageObject fromObject => this.origin.fromObject;

  public StageObject.OBJECT_TYPE fromType => this.origin.fromType;

  public Vector3 hitPos => this.origin.hitPos;

  public int fromClientID => this.origin.fromClientID;

  public SkillInfo.SkillParam skillParam => this.origin.skillParam;

  public int regionID => this.origin.regionID;

  public bool isDamageRegionOnly => this.origin.isDamageRegionOnly;

  public Enemy.WEAK_STATE weakState => this.origin.weakState;

  public bool IsSpAttackHit => this.origin.isSpAttackHit;

  public int damage => this.origin.damage;

  public AtkAttribute damageDetails => this.origin.damageDetails;

  public float downAddBase => this.origin.downAddBase;

  public float downAddWeak => this.origin.downAddWeak;

  public bool isForceDown => this.origin.isForceDown;

  public float concussionAdd => this.origin.concussionAdd;

  public bool isArrowBleed => this.origin.isArrowBleed;

  public int arrowBleedDamage => this.origin.arrowBleedDamage;

  public int arrowBurstDamage => this.origin.arrowBurstDamage;

  public bool isShadowSealing => this.origin.isShadowSealing;

  public bool isArrowBomb => this.origin.isArrowBomb;

  public Vector3 hostPos => this.origin.hostPos;

  public float hostDir => this.origin.hostDir;

  public int afterHP => this.origin.afterHP;

  public int afterRegionHP => this.origin.afterRegionHP;

  public int afterHealHp => this.origin.afterHealHp;

  public bool breakRegion => this.origin.breakRegion;

  public int reactionType => this.origin.reactionType;

  public Vector3 blowForce => this.origin.blowForce;

  public float downTotal => this.origin.downTotal;

  public float concussionTotal => this.origin.concussionTotal;

  public BadStatus badStatusTotal => this.origin.badStatusTotal;

  public float damageHpRate => this.origin.damageHpRate;

  public bool arrowBleedSkipFirst => this.origin.arrowBleedSkipFirst;

  public int afterBarrierHp => this.origin.barrierHp;

  public int afterShieldHp => this.origin.shieldHp;

  public int afterGrabHp => this.origin.grabHp;

  public int shieldDamage => this.origin.shieldDamage;

  public EnemyAegisController.SyncParam aegisParam => this.origin.aegisParam;

  public int deadReviveCount => this.origin.deadReviveCount;
}
