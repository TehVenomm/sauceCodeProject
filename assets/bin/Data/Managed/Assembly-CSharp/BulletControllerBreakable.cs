// Decompiled with JetBrains decompiler
// Type: BulletControllerBreakable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class BulletControllerBreakable : BulletControllerBase
{
  private const float OFFSET_TARGET_HEIGHT = 1f;
  private BulletData.BulletBreakable.MOVE_TYPE moveType;
  private List<BulletControllerBreakable.HitTimerInfo> hitTimerInfoList = new List<BulletControllerBreakable.HitTimerInfo>();
  private int breakCount;
  private int hitCounter;
  private int ignoreLayerMask;
  private int ignoreHitCountLayerMask;
  private float homingLimit;
  private float homingChangeStart;
  private float homingChange;
  private bool hightLock;
  private float acceleration;
  private int damageToEndurance;
  private BulletData emissionBulletOnBroken;
  private string emissionBulletAttackInfoName;

  private bool enableEmissionBulletOnBroken
  {
    get => Object.op_Inequality((Object) this.emissionBulletOnBroken, (Object) null);
  }

  public override void Initialize(
    BulletData bullet,
    SkillInfo.SkillParam _skillInfoParam,
    Vector3 pos,
    Quaternion rot)
  {
    base.Initialize(bullet, _skillInfoParam, pos, rot);
    if (bullet.dataBreakable == null)
      return;
    this.moveType = bullet.dataBreakable.moveType;
    this.hitCounter = 0;
    this.breakCount = bullet.dataBreakable.breakCount;
    this.emissionBulletOnBroken = bullet.dataBreakable.emissionBulletOnBroken;
    this.emissionBulletAttackInfoName = bullet.dataBreakable.emissionBulletAttackInfoName;
    if (bullet.dataBreakable.isIgnoreHitEnemyAttack)
      this.ignoreLayerMask |= 40960 /*0xA000*/;
    if (bullet.dataBreakable.isIgnoreHitEnemyBody)
      this.ignoreLayerMask |= 2048 /*0x0800*/;
    if (bullet.dataBreakable.isIgnoreHitEnemyMove)
      this.ignoreLayerMask |= 1024 /*0x0400*/;
    if (bullet.dataBreakable.isIgnoreHitPlayerBody)
      this.ignoreLayerMask |= 256 /*0x0100*/;
    if (bullet.dataBreakable.isIgnoreHitPlayerAttack)
      this.ignoreLayerMask |= 20480 /*0x5000*/;
    if (bullet.dataBreakable.isIgnoreHitWallAndObject)
      this.ignoreLayerMask |= 393728 /*0x060200*/;
    this.ignoreHitCountLayerMask = this.ignoreLayerMask;
    if (bullet.dataBreakable.isIgnoreHitCountPlayerBody)
      this.ignoreHitCountLayerMask |= 256 /*0x0100*/;
    this.homingLimit = bullet.dataToEndurance.limitAngel;
    this.homingChangeStart = bullet.dataToEndurance.limitChangeStartTime;
    this.homingChange = bullet.dataToEndurance.limitChangeAngel;
    this.hightLock = bullet.dataToEndurance.hightLock;
    this.acceleration = bullet.dataToEndurance.acceleration;
    this.damageToEndurance = bullet.dataToEndurance.toEnduranceDamage;
  }

  public override void Update()
  {
    if (this.moveType == BulletData.BulletBreakable.MOVE_TYPE.NORMAL)
      return;
    this.timeCount += Time.deltaTime;
    int index = 0;
    for (int count = this.hitTimerInfoList.Count; index < count; ++index)
      this.hitTimerInfoList[index].timer -= Time.deltaTime;
    this.hitTimerInfoList.RemoveAll((Predicate<BulletControllerBreakable.HitTimerInfo>) (item => (double) item.timer <= 0.0));
    if (Object.op_Equality((Object) this.targetObject, (Object) null))
      return;
    this.SetVelocity(this.initialVelocity + this.acceleration * this.timeCount);
    float num1 = this.homingLimit;
    if ((double) this.timeCount > (double) this.homingChangeStart)
    {
      num1 -= this.homingChange * (this.timeCount - this.homingChangeStart);
      if ((double) num1 < 0.0)
        num1 = 0.0f;
    }
    float num2 = num1 * Time.deltaTime;
    Vector3 position1 = this._transform.position;
    Vector3 position2 = this.targetObject._transform.position;
    if (this.hightLock)
    {
      position2.y = 0.0f;
      position1.y = 0.0f;
    }
    else
      ++position2.y;
    Vector3 vector3_1 = Vector3.op_Subtraction(position2, position1);
    float num3 = Mathf.Abs(Vector3.Angle(this._transform.forward, vector3_1));
    if ((double) num3 == 0.0)
      return;
    float num4 = num2 / num3;
    if ((double) num4 > 1.0)
      num4 = 1f;
    this._transform.rotation = Quaternion.Lerp(this._transform.rotation, Quaternion.LookRotation(vector3_1), num4);
    Vector3 vector3_2 = Vector3.op_Multiply(Quaternion.op_Multiply(this._transform.rotation, Vector3.forward), this.speed);
    if (this.hightLock)
      vector3_2.y = 0.0f;
    this._rigidbody.velocity = vector3_2;
  }

  public override void OnShot() => ((Component) this).gameObject.layer = 31 /*0x1F*/;

  public override bool IsHit(Collider collider)
  {
    if ((1 << ((Component) collider).gameObject.layer & this.ignoreLayerMask) > 0 || Object.op_Inequality((Object) ((Component) collider).gameObject.GetComponent<DangerRader>(), (Object) null))
      return false;
    AnimEventCollider.AtkColliderHiter atkHiter = ((Component) collider).gameObject.GetComponent<AnimEventCollider.AtkColliderHiter>();
    return !Object.op_Inequality((Object) atkHiter, (Object) null) || !this.hitTimerInfoList.Exists((Predicate<BulletControllerBreakable.HitTimerInfo>) (item => item.name == atkHiter.attackInfo.name));
  }

  public override void OnHit(Collider collider)
  {
    if ((1 << ((Component) collider).gameObject.layer & this.ignoreHitCountLayerMask) > 0)
      return;
    ++this.hitCounter;
    AnimEventCollider.AtkColliderHiter atkHiter = ((Component) collider).gameObject.GetComponent<AnimEventCollider.AtkColliderHiter>();
    if (Object.op_Inequality((Object) atkHiter, (Object) null) && !this.hitTimerInfoList.Exists((Predicate<BulletControllerBreakable.HitTimerInfo>) (item => item.name == atkHiter.attackInfo.name)) && atkHiter.attackInfo is AttackHitInfo attackInfo)
      this.hitTimerInfoList.Add(new BulletControllerBreakable.HitTimerInfo(attackInfo.name, attackInfo.hitIntervalTime));
    if (!this.enableEmissionBulletOnBroken || ((Component) collider).gameObject.layer != 12 && ((Component) collider).gameObject.layer != 14 || !this.IsBreak(collider))
      return;
    this.CreateEmissionBulletOnBroken();
  }

  private void CreateEmissionBulletOnBroken()
  {
    AttackInfo atkInfo = this.fromObject.FindAttackInfo(this.emissionBulletAttackInfoName) ?? this.bulletObject.GetAttackInfo();
    if (atkInfo == null)
      return;
    BulletData emissionBulletOnBroken = this.emissionBulletOnBroken;
    if (Object.op_Equality((Object) emissionBulletOnBroken, (Object) null) || Object.op_Equality((Object) this.fromObject, (Object) null))
      return;
    AnimEventShot.CreateByExternalBulletData(emissionBulletOnBroken, this.fromObject, atkInfo, this._transform.position, this._transform.rotation);
  }

  public override bool IsBreak(Collider collider)
  {
    return this.breakCount <= 0 || this.hitCounter >= this.breakCount;
  }

  public int GetHitCount() => this.hitCounter;

  public void SetHitCount(int count) => this.hitCounter = count;

  public override void OnLandHit()
  {
    if (!MonoBehaviourSingleton<QuestManager>.IsValid() || !MonoBehaviourSingleton<QuestManager>.I.IsDefenseBattle() || !MonoBehaviourSingleton<InGameProgress>.IsValid() || (double) MonoBehaviourSingleton<InGameProgress>.I.defenseBattleEndurance <= 0.0)
      return;
    MonoBehaviourSingleton<InGameProgress>.I.DamageToEndurance(this.damageToEndurance);
    if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
      return;
    MonoBehaviourSingleton<InGameCameraManager>.I.SetShakeCamera(this._transform.position, 1f, 0.2f);
  }

  private class HitTimerInfo
  {
    public string name;
    public float timer;

    public HitTimerInfo(string name, float timer)
    {
      this.name = name;
      this.timer = timer;
    }
  }
}
