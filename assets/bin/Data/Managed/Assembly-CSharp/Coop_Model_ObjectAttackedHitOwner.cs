// Decompiled with JetBrains decompiler
// Type: Coop_Model_ObjectAttackedHitOwner
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class Coop_Model_ObjectAttackedHitOwner : Coop_Model_ObjectBase
{
  public string attackInfoName;
  public float attackInfoRate;
  public int fromObjectID;
  public int fromType;
  public Vector3 fromPos = Vector3.zero;
  public Vector3 hitPos = Vector3.zero;
  public int fromClientID;
  public int skillIndex = -1;
  public int regionID = -1;
  public bool isDamageRegionOnly;
  public int weakState;
  public int damage;
  public bool validDamage;
  public float downAddBase;
  public float downAddWeak;
  public bool isForceDown;
  public float concussionAdd;
  public bool isArrowBleed;
  public int arrowBleedDamage;
  public int arrowBurstDamage;
  public BadStatus badStatusAdd = new BadStatus();
  public bool isSpAttackHit;
  public AtkAttribute damageDetails = new AtkAttribute();
  public bool isShadowSealing;
  public bool isArrowBomb;

  public Coop_Model_ObjectAttackedHitOwner()
  {
    this.packetType = PACKET_TYPE.OBJECT_ATTACKED_HIT_OWNER;
  }

  public void SetAttackedHitStatus(AttackedHitStatusOwner status)
  {
    this.attackInfoName = status.attackInfo.name;
    this.attackInfoRate = status.attackInfo.rateInfoRate;
    this.fromObjectID = status.fromObjectID;
    this.fromType = (int) status.fromType;
    this.fromPos = status.fromPos;
    this.hitPos = status.hitPos;
    this.fromClientID = status.fromClientID;
    if (status.skillParam != null)
      this.skillIndex = status.skillParam.skillIndex;
    this.regionID = status.regionID;
    this.isDamageRegionOnly = status.isDamageRegionOnly;
    this.weakState = (int) status.weakState;
    this.damage = status.damage;
    this.validDamage = status.validDamage;
    this.downAddBase = status.downAddBase;
    this.downAddWeak = status.downAddWeak;
    this.isForceDown = status.isForceDown;
    this.concussionAdd = status.concussionAdd;
    this.isArrowBleed = status.isArrowBleed;
    this.arrowBleedDamage = status.arrowBleedDamage;
    this.arrowBurstDamage = status.arrowBurstDamage;
    this.badStatusAdd.Copy(status.badStatusAdd);
    this.isSpAttackHit = status.IsSpAttackHit;
    this.damageDetails = status.damageDetails;
    this.isShadowSealing = status.isShadowSealing;
    this.isArrowBomb = status.isArrowBomb;
  }

  public void CopyAttackedHitStatus(out AttackedHitStatusOwner status)
  {
    AttackedHitStatus status1 = new AttackedHitStatus();
    status1.fromObjectID = this.fromObjectID;
    status1.fromObject = MonoBehaviourSingleton<StageObjectManager>.I.FindCharacter(this.fromObjectID);
    if (Object.op_Inequality((Object) status1.fromObject, (Object) null))
      status1.attackInfo = status1.fromObject.FindAttackInfoExternal(this.attackInfoName, true, this.attackInfoRate) as AttackHitInfo;
    if (status1.attackInfo == null)
      status1.attackInfo = new AttackHitInfo();
    status1.fromType = (StageObject.OBJECT_TYPE) this.fromType;
    status1.fromPos = this.fromPos;
    status1.hitPos = this.hitPos;
    status1.fromClientID = this.fromClientID;
    if (Object.op_Inequality((Object) status1.fromObject, (Object) null))
      status1.skillParam = status1.fromObject.GetSkillParam(this.skillIndex);
    status1.regionID = this.regionID;
    status1.isDamageRegionOnly = this.isDamageRegionOnly;
    status1.weakState = (Enemy.WEAK_STATE) this.weakState;
    status1.damage = this.damage;
    status1.validDamage = this.validDamage;
    status1.downAddBase = this.downAddBase;
    status1.downAddWeak = this.downAddWeak;
    status1.isForceDown = this.isForceDown;
    status1.concussionAdd = this.concussionAdd;
    status1.isArrowBleed = this.isArrowBleed;
    status1.arrowBleedDamage = this.arrowBleedDamage;
    status1.arrowBurstDamage = this.arrowBurstDamage;
    status1.badStatusAdd.Copy(this.badStatusAdd);
    status1.isSpAttackHit = this.isSpAttackHit;
    status1.damageDetails = this.damageDetails;
    status1.isShadowSealing = this.isShadowSealing;
    status1.isArrowBomb = this.isArrowBomb;
    status = new AttackedHitStatusOwner(status1);
  }
}
