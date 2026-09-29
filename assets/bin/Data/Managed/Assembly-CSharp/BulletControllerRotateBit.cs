// Decompiled with JetBrains decompiler
// Type: BulletControllerRotateBit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class BulletControllerRotateBit : BulletControllerBase
{
  private Vector3 centralPoint;
  private float rotateAngle_Deg;
  private float rotateRadius;
  private Vector3 rotateAxis;
  private int rotateSign;
  private float waitTime;
  private float speedUpTime;
  private bool isLinearSpeedUp;
  private BulletData bulletData;

  public override void Initialize(
    BulletData bullet,
    SkillInfo.SkillParam skillParam,
    Vector3 pos,
    Quaternion rot)
  {
    base.Initialize(bullet, skillParam, pos, rot);
    this.bulletData = bullet;
    this.rotateAngle_Deg = bullet.dataRotateBit.rotateAngle_Deg;
    this.rotateRadius = bullet.dataRotateBit.rotateRadius;
    this.rotateAxis = bullet.dataRotateBit.rotateAxis;
    this.rotateSign = bullet.dataRotateBit.rotateSign <= 0 ? 1 : -1;
    this.waitTime = bullet.dataRotateBit.waitTime;
    this.isLinearSpeedUp = bullet.dataRotateBit.isLinearAngleSpeedUp;
    this.speedUpTime = bullet.dataRotateBit.speedUpTime;
  }

  public override void RegisterTargetObject(StageObject obj)
  {
    base.RegisterTargetObject(obj);
    if (this.bulletData.dataRotateBit.isSetCentralPosition)
      this.centralPoint = this.bulletData.dataRotateBit.centralPosition;
    else if (Object.op_Equality((Object) this.targetObject, (Object) null))
    {
      if (Object.op_Inequality((Object) this.fromObject, (Object) null))
        this.centralPoint = Vector3.op_Addition(this.fromObject._position, Quaternion.op_Multiply(this.fromObject._rotation, new Vector3(0.0f, 0.0f, 2f)));
      else
        this.centralPoint = Vector3.zero;
    }
    else
      this.centralPoint = this.targetObject._position;
  }

  public override void Update()
  {
    this.timeCount += Time.deltaTime;
    if ((double) this.waitTime >= (double) this.timeCount)
      return;
    this.UpdateRotateAroundCentralPosition();
  }

  private void UpdateRotateAroundCentralPosition()
  {
    Vector3 position = this._transform.position;
    Vector3 vector3_1 = Vector3.op_Subtraction(position, this.centralPoint);
    ((Vector3) ref vector3_1).Normalize();
    Vector3 vector3_2 = Vector3.op_Multiply(vector3_1, this.rotateRadius);
    Vector3 vector3_3 = Vector3.op_Subtraction(Vector3.op_Addition(this.centralPoint, Quaternion.op_Multiply(Quaternion.AngleAxis((float) this.rotateSign * (this.isLinearSpeedUp ? Mathf.Lerp(0.0f, this.rotateAngle_Deg, Mathf.Clamp01((this.timeCount - this.waitTime) / this.speedUpTime)) : this.rotateAngle_Deg) * Time.deltaTime, this.rotateAxis), vector3_2)), position);
    this._transform.position = Vector3.op_Addition(position, vector3_3);
  }
}
