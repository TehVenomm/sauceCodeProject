// Decompiled with JetBrains decompiler
// Type: BulletControllerFall
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class BulletControllerFall : BulletControllerBase
{
  protected float gravityStartTime;
  protected float gravityRate;

  public override void FixedUpdate()
  {
    base.FixedUpdate();
    if ((double) this.gravityStartTime < 0.0 || (double) this.timeCount < (double) this.gravityStartTime)
      return;
    this._rigidbody.AddForce(Vector3.op_Multiply(Physics.gravity, this.gravityRate), (ForceMode) 5);
    if (!Vector3.op_Inequality(this._rigidbody.velocity, Vector3.zero))
      return;
    this._transform.forward = this._rigidbody.velocity;
  }

  public override void Initialize(
    BulletData bullet,
    SkillInfo.SkillParam _skillInfoParam,
    Vector3 pos,
    Quaternion rot)
  {
    base.Initialize(bullet, _skillInfoParam, pos, rot);
    this.gravityStartTime = bullet.dataFall.gravityStartTime;
    this.gravityRate = bullet.dataFall.gravityRate;
  }
}
