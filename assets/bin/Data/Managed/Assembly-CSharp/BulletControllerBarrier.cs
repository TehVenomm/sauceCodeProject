// Decompiled with JetBrains decompiler
// Type: BulletControllerBarrier
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class BulletControllerBarrier : BulletControllerBase
{
  private int ignoreLayerMask;

  public override void Initialize(
    BulletData bullet,
    SkillInfo.SkillParam _skillInfoParam,
    Vector3 pos,
    Quaternion rot)
  {
    base.Initialize(bullet, _skillInfoParam, pos, rot);
    this.ignoreLayerMask |= 3072 /*0x0C00*/;
    this.ignoreLayerMask |= 20480 /*0x5000*/;
    this.ignoreLayerMask |= 393728 /*0x060200*/;
  }

  public override bool IsHit(Collider collider)
  {
    return (1 << ((Component) collider).gameObject.layer & this.ignoreLayerMask) <= 0 && !Object.op_Inequality((Object) ((Component) collider).gameObject.GetComponent<AnimEventCollider.AtkColliderHiter>(), (Object) null) && !Object.op_Inequality((Object) ((Component) collider).gameObject.GetComponent<DangerRader>(), (Object) null);
  }
}
