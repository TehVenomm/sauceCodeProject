// Decompiled with JetBrains decompiler
// Type: Coop_Model_ObjectAttackedHitFix
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Coop_Model_ObjectAttackedHitFix : Coop_Model_ObjectBase
{
  public string attackInfoName;
  public float attackInfoRate;
  public int fromObjectID;
  public int fromType;
  public Vector3 hitPos = Vector3.zero;
  public int fromClientID;
  public int skillIndex = -1;
  public int regionID = -1;
  public int weakState;
  public int damage;
  public float downAddBase;
  public float downAddWeak;
  public bool isForceDown;
  public float concussionAdd;
  public bool isArrowBleed;
  public int arrowBleedDamage;
  public int arrowBurstDamage;
  public Vector3 hostPos = Vector3.zero;
  public float hostDir;
  public int afterHP;
  public int afterRegionHP;
  public int afterHealHp;
  public bool breakRegion;
  public int reactionType;
  public Vector3 blowForce = Vector3.zero;
  public float downTotal;
  public float concussionTotal;
  public BadStatus badStatusTotal = new BadStatus();
  public float damageHpRate;
  public bool arrowBleedSkipFirst;
  public bool isSpAttackHit;
  public int afterRegionBarrierHp;
  public AtkAttribute damageDetails = new AtkAttribute();
  public int afterShieldHp;
  public int afterGrabHp;
  public bool isShadowSealing;
  public bool isArrowBomb;
  public EnemyAegisController.SyncParam aegisParam = new EnemyAegisController.SyncParam();
  public int deadReviveCount;

  public Coop_Model_ObjectAttackedHitFix() => this.packetType = PACKET_TYPE.OBJECT_ATTACKED_HIT_FIX;

  public override bool IsPromiseOverAgainCheck() => true;

  public override Vector3 GetObjectPosition() => this.hostPos;

  public override bool IsHaveObjectPosition() => this.reactionType != 0;

  public override bool IsForceHandleBefore(StageObject owner)
  {
    return this.reactionType != 0 || base.IsForceHandleBefore(owner);
  }

  public void SetAttackedHitStatus(AttackedHitStatusFix status)
  {
    this.attackInfoName = status.attackInfo.name;
    this.attackInfoRate = status.attackInfo.rateInfoRate;
    this.fromObjectID = status.fromObjectID;
    this.fromType = (int) status.fromType;
    this.hitPos = status.hitPos;
    this.fromClientID = status.fromClientID;
    if (status.skillParam != null)
      this.skillIndex = status.skillParam.skillIndex;
    this.regionID = status.regionID;
    this.weakState = (int) status.weakState;
    this.damage = status.damage;
    this.downAddBase = status.downAddBase;
    this.downAddWeak = status.downAddWeak;
    this.isForceDown = status.isForceDown;
    this.concussionAdd = status.concussionAdd;
    this.isArrowBleed = status.isArrowBleed;
    this.arrowBleedDamage = status.arrowBleedDamage;
    this.arrowBurstDamage = status.arrowBurstDamage;
    this.hostPos = status.hostPos;
    this.hostDir = status.hostDir;
    this.afterHP = status.afterHP;
    this.afterRegionHP = status.afterRegionHP;
    this.afterHealHp = status.afterHealHp;
    this.breakRegion = status.breakRegion;
    this.reactionType = status.reactionType;
    this.blowForce = status.blowForce;
    this.downTotal = status.downTotal;
    this.concussionTotal = status.concussionTotal;
    this.badStatusTotal.Copy(status.badStatusTotal);
    this.damageHpRate = status.damageHpRate;
    this.arrowBleedSkipFirst = status.arrowBleedSkipFirst;
    this.isSpAttackHit = status.IsSpAttackHit;
    this.afterRegionBarrierHp = status.afterBarrierHp;
    this.damageDetails = status.damageDetails;
    this.afterShieldHp = status.afterShieldHp;
    this.afterGrabHp = status.afterGrabHp;
    this.isShadowSealing = status.isShadowSealing;
    this.aegisParam.Copy(status.aegisParam);
    this.deadReviveCount = status.deadReviveCount;
    this.isArrowBomb = status.isArrowBomb;
  }

  public void CopyAttackedHitStatus(out AttackedHitStatusFix status)
  {
    AttackedHitStatus status1 = new AttackedHitStatus();
    status1.fromObjectID = this.fromObjectID;
    status1.fromObject = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(this.fromObjectID);
    if (Object.op_Inequality((Object) status1.fromObject, (Object) null) && !status1.fromObject.isLoading)
      status1.attackInfo = status1.fromObject.FindAttackInfoExternal(this.attackInfoName, true, this.attackInfoRate) as AttackHitInfo;
    if (status1.attackInfo == null)
      status1.attackInfo = new AttackHitInfo();
    status1.fromType = (StageObject.OBJECT_TYPE) this.fromType;
    status1.hitPos = this.hitPos;
    status1.fromClientID = this.fromClientID;
    if (Object.op_Inequality((Object) status1.fromObject, (Object) null))
      status1.skillParam = status1.fromObject.GetSkillParam(this.skillIndex);
    status1.regionID = this.regionID;
    status1.weakState = (Enemy.WEAK_STATE) this.weakState;
    status1.damage = this.damage;
    status1.downAddBase = this.downAddBase;
    status1.downAddWeak = this.downAddWeak;
    status1.isForceDown = this.isForceDown;
    status1.concussionAdd = this.concussionAdd;
    status1.isArrowBleed = this.isArrowBleed;
    status1.arrowBleedDamage = this.arrowBleedDamage;
    status1.arrowBurstDamage = this.arrowBurstDamage;
    status1.hostPos = this.hostPos;
    status1.hostDir = this.hostDir;
    status1.afterHP = this.afterHP;
    status1.afterRegionHP = this.afterRegionHP;
    status1.afterHealHp = this.afterHealHp;
    status1.breakRegion = this.breakRegion;
    status1.reactionType = this.reactionType;
    status1.blowForce = this.blowForce;
    status1.downTotal = this.downTotal;
    status1.concussionTotal = this.concussionTotal;
    status1.badStatusTotal.Copy(this.badStatusTotal);
    status1.damageHpRate = this.damageHpRate;
    status1.arrowBleedSkipFirst = this.arrowBleedSkipFirst;
    status1.isSpAttackHit = this.isSpAttackHit;
    status1.barrierHp = this.afterRegionBarrierHp;
    status1.damageDetails = this.damageDetails;
    status1.shieldHp = this.afterShieldHp;
    status1.grabHp = this.afterGrabHp;
    status1.isShadowSealing = this.isShadowSealing;
    status1.isArrowBomb = this.isArrowBomb;
    status1.aegisParam.Copy(this.aegisParam);
    status1.deadReviveCount = this.deadReviveCount;
    status = new AttackedHitStatusFix(status1);
  }
}
