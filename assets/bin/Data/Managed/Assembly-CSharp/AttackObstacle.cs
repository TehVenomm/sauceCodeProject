// Decompiled with JetBrains decompiler
// Type: AttackObstacle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[RequireComponent(typeof (Rigidbody))]
public class AttackObstacle : StageObject
{
  private Collider[] _colliderList;
  private AnimEventShot _animEventShot;
  private float breakEnableTime;
  private bool breakEnebleFlag;
  private float keika;
  private bool isBreak;
  private bool isHitBreakEnable = true;

  protected override void Awake() => base.Awake();

  public void Initialize(AnimEventShot aminEventShot, float time)
  {
    this._animEventShot = aminEventShot;
    Utility.SetLayerWithChildren(((Component) this).transform, 18);
    this._rigidbody = ((Component) this).GetComponent<Rigidbody>();
    this._rigidbody.useGravity = false;
    this._rigidbody.isKinematic = true;
    this._colliderList = ((Component) this).GetComponentsInChildren<Collider>();
    if (this._colliderList.Length == 0)
    {
      CapsuleCollider capsuleCollider = ((Component) this).gameObject.AddComponent<CapsuleCollider>();
      capsuleCollider.center = new Vector3(0.0f, 0.0f, 0.0f);
      capsuleCollider.direction = 2;
      this._colliderList = new Collider[1];
      this._colliderList[0] = (Collider) capsuleCollider;
    }
    this.breakEnableTime = time;
    this.keika = 0.0f;
    this.breakEnebleFlag = (double) this.breakEnableTime <= 0.0;
    int index = 0;
    for (int length = this._colliderList.Length; index < length; ++index)
    {
      this._colliderList[index].isTrigger = false;
      if (!this.breakEnebleFlag)
        this._colliderList[index].enabled = false;
    }
    this.isBreak = false;
    this.isHitBreakEnable = this._animEventShot.bulletData.dataObstacle.isHitBreak;
  }

  protected override void Update()
  {
    base.Update();
    if (this.breakEnebleFlag)
      return;
    this.keika += Time.deltaTime;
    if ((double) this.keika <= (double) this.breakEnableTime)
      return;
    this.breakEnebleFlag = true;
    int index = 0;
    for (int length = this._colliderList.Length; index < length; ++index)
    {
      if (Object.op_Inequality((Object) this._colliderList[index], (Object) null))
        this._colliderList[index].enabled = true;
    }
  }

  protected override bool IsValidAttackedHit(StageObject from_object)
  {
    return from_object is Enemy && base.IsValidAttackedHit(from_object);
  }

  protected override void OnAttackedHitLocal(AttackedHitStatusLocal status)
  {
    base.OnAttackedHitLocal(status);
    status.damage = (int) status.attackInfo.atk.normal;
    status.damage += (int) status.attackInfo.atk.fire;
    status.damage += (int) status.attackInfo.atk.water;
    status.damage += (int) status.attackInfo.atk.thunder;
    status.damage += (int) status.attackInfo.atk.soil;
    status.damage += (int) status.attackInfo.atk.light;
    status.damage += (int) status.attackInfo.atk.dark;
  }

  public override void OnAttackedHitFix(AttackedHitStatusFix status)
  {
    base.OnAttackedHitFix(status);
    if (status.damage <= 0 || status.fromType != StageObject.OBJECT_TYPE.ENEMY || !this.isHitBreakEnable || !this.breakEnebleFlag || this.isBreak)
      return;
    this.isBreak = true;
    int index = 0;
    for (int length = this._colliderList.Length; index < length; ++index)
    {
      if (Object.op_Inequality((Object) this._colliderList[index], (Object) null))
        this._colliderList[index].enabled = false;
    }
    this._animEventShot.OnDestroy();
  }
}
