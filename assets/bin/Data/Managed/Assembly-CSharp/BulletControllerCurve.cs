// Decompiled with JetBrains decompiler
// Type: BulletControllerCurve
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class BulletControllerCurve : BulletControllerBase
{
  protected FloatInterpolator curve;
  protected FloatInterpolator curveTime;
  protected Vector3 curveAxis = Vector3.zero;
  protected Quaternion baseRotation = Quaternion.identity;

  public override void Update()
  {
    base.Update();
    this.curve.Update(Time.deltaTime * this.curveTime.Update());
    this._transform.rotation = Quaternion.op_Multiply(this.baseRotation, Quaternion.AngleAxis(this.curve.Get(), this.curveAxis));
    this._rigidbody.velocity = Vector3.op_Multiply(Quaternion.op_Multiply(this._transform.rotation, Vector3.forward), this.speed);
  }

  public override void Initialize(
    BulletData bullet,
    SkillInfo.SkillParam _skillInfoParam,
    Vector3 pos,
    Quaternion rot)
  {
    base.Initialize(bullet, _skillInfoParam, pos, rot);
    this.baseRotation = this._transform.rotation;
    this.curve = new FloatInterpolator();
    this.curve.loopType = Interpolator.LOOP.REPETE;
    this.curve.Set(bullet.dataCurve.loopTime, 0.0f, bullet.dataCurve.curveAngle, bullet.dataCurve.curveAnim, 0.0f, (AnimationCurve) null);
    this.curve.Play();
    this.curveTime = new FloatInterpolator();
    this.curveTime.Set(bullet.data.appearTime, 1f, 0.0f, bullet.dataCurve.timeAnim, 0.0f, (AnimationCurve) null);
    this.curveTime.Play();
    this.curveAxis = bullet.dataCurve.curveAxis;
  }
}
