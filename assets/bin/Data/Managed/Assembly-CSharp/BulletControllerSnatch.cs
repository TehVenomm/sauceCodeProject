// Decompiled with JetBrains decompiler
// Type: BulletControllerSnatch
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class BulletControllerSnatch : BulletControllerBase
{
  private Player owner;
  private BulletControllerSnatch.STATE state;
  private Vector3 startPos = Vector3.zero;
  private float maxDistance;

  public override void Initialize(
    BulletData bullet,
    SkillInfo.SkillParam _skillInfoParam,
    Vector3 pos,
    Quaternion rot)
  {
    base.Initialize(bullet, _skillInfoParam, pos, rot);
    this.maxDistance = bullet.dataSnatch.maxDistance;
    this.startPos = pos;
    this.SetState(BulletControllerSnatch.STATE.FORWARD);
  }

  public override void Update()
  {
    this.timeCount += Time.deltaTime;
    switch (this.state)
    {
      case BulletControllerSnatch.STATE.FORWARD:
        this.SetVelocity(Mathf.Max(0.0f, this.initialVelocity - this.initialVelocity * this.timeCount * this.timeCount));
        Vector3 vector3 = Vector3.op_Subtraction(this._transform.position, this.startPos);
        if ((double) ((Vector3) ref vector3).magnitude > (double) this.maxDistance)
        {
          this.SetVelocity(0.0f);
          this.SetState(BulletControllerSnatch.STATE.MISS);
          break;
        }
        break;
      case BulletControllerSnatch.STATE.MISS:
        if (Object.op_Inequality((Object) this._collider, (Object) null))
          this._collider.enabled = false;
        this.owner.snatchCtrl.OnReach();
        this.SetState(BulletControllerSnatch.STATE.DESTROY);
        break;
      case BulletControllerSnatch.STATE.SNATCH:
        if (Object.op_Inequality((Object) this._collider, (Object) null))
          this._collider.enabled = false;
        this.SetVelocity(0.0f);
        this.SetState(BulletControllerSnatch.STATE.DESTROY);
        break;
      case BulletControllerSnatch.STATE.DESTROY:
        this.bulletObject.OnDestroy();
        this.SetState(BulletControllerSnatch.STATE.NONE);
        break;
    }
    this._rigidbody.velocity = Vector3.op_Multiply(Quaternion.op_Multiply(this._transform.rotation, Vector3.forward), this.speed);
  }

  public override bool IsHit(Collider collider)
  {
    int layer = ((Component) collider).gameObject.layer;
    if (layer == 31 /*0x1F*/ || layer == 8 && Object.op_Inequality((Object) ((Component) collider).gameObject.GetComponent<DangerRader>(), (Object) null))
      return false;
    if (layer == 11 || layer == 10)
    {
      EnemyColliderSettings component = ((Component) collider).gameObject.GetComponent<EnemyColliderSettings>();
      if (Object.op_Inequality((Object) component, (Object) null) && ((Object) component.targetCollider).GetInstanceID() == ((Object) collider).GetInstanceID())
        return false;
    }
    return true;
  }

  public override void OnHit(Collider collider)
  {
    switch (((Component) collider).gameObject.layer)
    {
      case 10:
      case 11:
        if (Object.op_Inequality((Object) ((Component) collider).gameObject.GetComponent<AttackRestraintObject>(), (Object) null))
        {
          this.SetState(BulletControllerSnatch.STATE.MISS);
          break;
        }
        this.SetState(BulletControllerSnatch.STATE.SNATCH);
        break;
      default:
        this.SetState(BulletControllerSnatch.STATE.MISS);
        break;
    }
  }

  public override void OnHitStay(Collider collider) => this.OnHit(collider);

  public override void RegisterFromObject(StageObject obj)
  {
    this.owner = obj as Player;
    if (!Object.op_Inequality((Object) this.owner, (Object) null))
      return;
    this.owner.snatchCtrl.SetSnatchBulletTrans(this._transform);
  }

  private void SetState(BulletControllerSnatch.STATE state) => this.state = state;

  private enum STATE
  {
    NONE,
    FORWARD,
    MISS,
    SNATCH,
    DESTROY,
  }
}
