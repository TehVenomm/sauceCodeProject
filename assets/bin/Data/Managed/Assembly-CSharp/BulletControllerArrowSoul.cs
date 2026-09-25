// Decompiled with JetBrains decompiler
// Type: BulletControllerArrowSoul
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class BulletControllerArrowSoul : BulletControllerBase
{
  protected float accel;
  protected float accelStartTime;
  protected float maxSpeed;
  protected float angularVelocity;
  protected float angularStartTime;
  protected float ignoreAngle;
  protected bool isEndAccel;
  protected bool isLookTarget;
  protected TargetPoint target;
  protected Vector3 direction = Vector3.forward;
  protected float speed0;
  protected float speed1;
  protected bool isPuppet;
  protected Vector3 puppetTargetPos;

  public void SetTarget(TargetPoint point) => this.target = point;

  public TargetPoint GetTarget() => this.target;

  public void SetPuppetTargetPos(Vector3 pos)
  {
    this.puppetTargetPos = pos;
    this.isPuppet = true;
  }

  public override void Initialize(
    BulletData bullet,
    SkillInfo.SkillParam skillParam,
    Vector3 pos,
    Quaternion rot)
  {
    this.timeCount = 0.0f;
    this.isEndAccel = false;
    this.isLookTarget = false;
    this.bulletSkillInfoParam = skillParam;
    this.speed0 = bullet.data.speed;
    this.speed1 = bullet.data.speed;
    this.accel = bullet.dataArrowSoul.accel;
    this.accelStartTime = bullet.dataArrowSoul.accelStartTime;
    this.maxSpeed = bullet.dataArrowSoul.maxSpeed;
    this.angularVelocity = bullet.dataArrowSoul.angularVelocity;
    this.angularStartTime = bullet.dataArrowSoul.angularStartTime;
    this.ignoreAngle = bullet.dataArrowSoul.ignoreAngle;
    this._transform.position = pos;
    Vector3 soulShotDir = MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulShotDirs[Random.Range(0, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulShotDirs.Length - 1)];
    this.direction = Quaternion.op_Multiply(rot, soulShotDir);
    Transform transform = this._transform;
    transform.position = Vector3.op_Subtraction(transform.position, Vector3.op_Multiply(this.direction, MonoBehaviourSingleton<InGameSettingsManager>.I.player.arrowActionInfo.soulShotDirVec));
    this._transform.rotation = rot;
    this._rigidbody.velocity = Vector3.op_Multiply(this.direction, this.speed0);
  }

  public override void Update()
  {
    if (Object.op_Equality((Object) this.target, (Object) null) && !this.isPuppet)
      return;
    this.timeCount += Time.deltaTime;
    bool flag = this._CalcSpeed();
    this._CalcAngle();
    if (this.isLookTarget)
    {
      Vector3 vector3 = Vector3.op_Subtraction(this.isPuppet ? this.puppetTargetPos : this.target.GetTargetPoint(), this._transform.position);
      float num1 = Mathf.Abs(Vector3.Angle(this._transform.forward, vector3));
      if ((double) num1 > (double) this.ignoreAngle)
      {
        float num2 = this.angularVelocity * Time.deltaTime / num1;
        if ((double) num2 > 1.0)
          num2 = 1f;
        this._transform.rotation = Quaternion.Lerp(this._transform.rotation, Quaternion.LookRotation(vector3), num2);
        this.direction = Quaternion.op_Multiply(this._transform.rotation, Vector3.forward);
        flag = true;
      }
    }
    if (!flag)
      return;
    this._rigidbody.velocity = Vector3.op_Multiply(this.direction, this.speed1);
  }

  private bool _CalcSpeed()
  {
    if (this.isEndAccel || (double) this.timeCount < (double) this.accelStartTime)
      return false;
    this.speed1 = this.speed0 + this.accel * (this.timeCount - this.accelStartTime);
    if ((double) this.speed1 > (double) this.maxSpeed)
    {
      this.speed1 = this.maxSpeed;
      this.isEndAccel = true;
    }
    return true;
  }

  private void _CalcAngle()
  {
    if (this.isLookTarget || (double) this.timeCount < (double) this.angularStartTime)
      return;
    this.isLookTarget = true;
  }
}
