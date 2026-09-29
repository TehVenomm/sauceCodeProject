// Decompiled with JetBrains decompiler
// Type: BulletControllerBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class BulletControllerBase : MonoBehaviour
{
  protected BulletObject bulletObject;
  protected StageObject fromObject;
  protected StageObject targetObject;

  public Transform _transform { get; protected set; }

  public Vector3 _position { get; protected set; }

  public Rigidbody _rigidbody { get; protected set; }

  public Collider _collider { get; protected set; }

  protected float speed { get; private set; }

  protected float initialVelocity { get; private set; }

  public float timeCount { get; protected set; }

  public SkillInfo.SkillParam bulletSkillInfoParam { get; protected set; }

  public void RegisterBulletObject(BulletObject b) => this.bulletObject = b;

  public virtual void RegisterFromObject(StageObject obj) => this.fromObject = obj;

  public virtual void RegisterTargetObject(StageObject obj) => this.targetObject = obj;

  protected virtual void Awake()
  {
    this.timeCount = 0.0f;
    this.bulletSkillInfoParam = (SkillInfo.SkillParam) null;
    this._transform = ((Component) this).transform;
    this._position = ((Component) this).transform.position;
    this._rigidbody = ((Component) this).GetComponent<Rigidbody>();
    this._collider = ((Component) this).GetComponent<Collider>();
  }

  public virtual void Initialize(
    BulletData bullet,
    SkillInfo.SkillParam skillParam,
    Vector3 pos,
    Quaternion rot)
  {
    this.speed = bullet.data.speed;
    this.initialVelocity = bullet.data.speed;
    this.bulletSkillInfoParam = skillParam;
    Vector3 forward = Vector3.forward;
    Vector3 vector3 = Vector3.op_Multiply(Quaternion.op_Multiply(rot, forward), this.speed);
    this._transform.position = pos;
    this._transform.LookAt(Vector3.op_Addition(pos, vector3));
    this._rigidbody.velocity = vector3;
  }

  public virtual void PostInitialize()
  {
  }

  public virtual void DestroyBulletObject()
  {
  }

  public virtual void Update() => this.timeCount += Time.deltaTime;

  public virtual void FixedUpdate()
  {
  }

  public virtual void OnHit(Collider collider)
  {
  }

  public virtual void OnHitStay(Collider collider)
  {
  }

  public virtual void OnLandHit()
  {
  }

  public virtual void OnShot()
  {
  }

  public virtual bool IsHit(Collider collider) => true;

  public virtual bool IsBreak(Collider collider) => false;

  protected void SetVelocity(float v) => this.speed = v;
}
