// Decompiled with JetBrains decompiler
// Type: BulletControllerEnemyPresent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class BulletControllerEnemyPresent : BulletControllerBase
{
  private const string OBJECT_NAME = "EnemyPresentBullet";
  private BulletData bulletData;
  private SphereCollider cachedCollider;
  private int ignoreLayerMask;
  private bool isPicked;

  public override void Initialize(
    BulletData bullet,
    SkillInfo.SkillParam skillParam,
    Vector3 pos,
    Quaternion rot)
  {
    base.Initialize(bullet, skillParam, pos, rot);
    this.bulletData = bullet;
    ((Object) ((Component) this).gameObject).name = "EnemyPresentBullet";
    ((Component) this).gameObject.layer = 31 /*0x1F*/;
    if (!bullet.dataEnemyPresent.isHitEnemyAttack)
      this.ignoreLayerMask |= 8192 /*0x2000*/;
    if (!bullet.dataEnemyPresent.isHitEnemyMove)
      this.ignoreLayerMask |= 1024 /*0x0400*/;
    this.ignoreLayerMask |= 32768 /*0x8000*/;
    this.ignoreLayerMask |= 20480 /*0x5000*/;
    if (!bullet.data.isObjectHitDelete)
      this.ignoreLayerMask |= 2490880;
    this.cachedCollider = ((Component) this).gameObject.AddComponent<SphereCollider>();
    this.cachedCollider.radius = bullet.data.radius;
    this.cachedCollider.center = bullet.data.hitOffset;
    ((Collider) this.cachedCollider).isTrigger = true;
    ((Collider) this.cachedCollider).enabled = true;
    this.isPicked = false;
  }

  public override bool IsHit(Collider collider)
  {
    if (this.isPicked)
      return false;
    int layer = ((Component) collider).gameObject.layer;
    return (1 << layer & this.ignoreLayerMask) <= 0 && (layer != 8 || !Object.op_Inequality((Object) ((Component) collider).gameObject.GetComponent<DangerRader>(), (Object) null)) && !Object.op_Equality((Object) ((Component) collider).gameObject.GetComponent<Self>(), (Object) null);
  }

  public override void OnHit(Collider collider)
  {
    this.isPicked = true;
    if (Object.op_Inequality((Object) this.cachedCollider, (Object) null))
      ((Collider) this.cachedCollider).enabled = false;
    Self component = ((Component) collider).gameObject.GetComponent<Self>();
    if (Object.op_Inequality((Object) component, (Object) null))
    {
      this._ExecHeal(component);
      this._ExecBuff(component);
    }
    if (!Object.op_Inequality((Object) this.bulletObject, (Object) null))
      return;
    this.bulletObject.NotifyBroken(false);
  }

  private void _ExecHeal(Self self)
  {
    Character.HealData healData = new Character.HealData(this.bulletData.dataEnemyPresent.value, HEAL_TYPE.NONE, HEAL_EFFECT_TYPE.BASIS, new List<int>()
    {
      10
    });
    if (this.bulletData.dataEnemyPresent.valueType == CALCULATE_TYPE.RATE)
    {
      healData.healHp = (int) ((double) this.bulletData.dataEnemyPresent.value * 0.0099999997764825821 * (double) self.hpMax);
      healData.applyAbilityTypeList.Clear();
    }
    self.OnHealReceive(healData);
  }

  private void _ExecBuff(Self self)
  {
    if (this.bulletData.dataEnemyPresent.buffIds == null || this.bulletData.dataEnemyPresent.buffIds.Count <= 0)
      return;
    int index = 0;
    for (int count = this.bulletData.dataEnemyPresent.buffIds.Count; index < count; ++index)
      self.StartBuffByBuffTableId(this.bulletData.dataEnemyPresent.buffIds[index], (SkillInfo.SkillParam) null);
  }
}
