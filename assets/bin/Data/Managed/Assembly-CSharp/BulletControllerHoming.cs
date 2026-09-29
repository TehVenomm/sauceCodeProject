// Decompiled with JetBrains decompiler
// Type: BulletControllerHoming
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class BulletControllerHoming : BulletControllerBase
{
  private const float targetHightOffset = 1f;
  protected float homingLimit;
  protected float homingChangeStart;
  protected float homingChange;
  protected bool hightLock;
  protected float acceleration;

  public override void Update()
  {
    base.Update();
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
    Vector3 targetPos = this.GetTargetPos();
    float num3 = Mathf.Abs(Vector3.Angle(this._transform.forward, targetPos));
    if ((double) num3 == 0.0)
      return;
    float num4 = num2 / num3;
    if ((double) num4 > 1.0)
      num4 = 1f;
    this._transform.rotation = Quaternion.Lerp(this._transform.rotation, Quaternion.LookRotation(targetPos), num4);
    Vector3 vector3 = Vector3.op_Multiply(Quaternion.op_Multiply(this._transform.rotation, Vector3.forward), this.speed);
    if (this.hightLock)
      vector3.y = 0.0f;
    this._rigidbody.velocity = vector3;
  }

  public override void Initialize(
    BulletData bullet,
    SkillInfo.SkillParam _skillInfoParam,
    Vector3 pos,
    Quaternion rot)
  {
    base.Initialize(bullet, _skillInfoParam, pos, rot);
    this.InitParam(bullet.dataHoming);
  }

  protected void InitParam(BulletData.BulletHoming _data)
  {
    this.homingLimit = _data.limitAngel;
    this.homingChangeStart = _data.limitChangeStartTime;
    this.homingChange = _data.limitChangeAngel;
    this.hightLock = _data.hightLock;
    this.acceleration = _data.acceleration;
  }

  protected virtual Vector3 GetTargetPos()
  {
    Vector3 position1 = this._transform.position;
    Vector3 position2 = this.targetObject._transform.position;
    if (this.hightLock)
    {
      position2.y = 0.0f;
      position1.y = 0.0f;
    }
    else
      ++position2.y;
    return Vector3.op_Subtraction(position2, position1);
  }
}
