// Decompiled with JetBrains decompiler
// Type: DangerRader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class DangerRader : MonoBehaviour
{
  public const float RADER_SPAN = 0.2f;
  private SpanTimer triggerSpan = new SpanTimer(0.2f);
  private const int RECORD_MAX = 20;
  public LinkedList<DangerRader.ColliderRecord> records = new LinkedList<DangerRader.ColliderRecord>();
  private float nearMoveTime;
  private float nearWillBulletHitTime;
  private float nearWillDashHitTime;

  public Brain brain { get; private set; }

  public Rigidbody _rigidbody { get; private set; }

  public Collider _collider { get; private set; }

  public static DangerRader Create(Brain brain, float radius)
  {
    DangerRader componentInChildren = ((Component) brain).gameObject.GetComponentInChildren<DangerRader>();
    if (Object.op_Inequality((Object) componentInChildren, (Object) null))
      return componentInChildren;
    int layer = ((Component) brain.owner).gameObject.layer;
    DangerRader objectAndComponent = (DangerRader) Utility.CreateGameObjectAndComponent(nameof (DangerRader), ((Component) brain).transform, layer);
    if (Object.op_Equality((Object) objectAndComponent, (Object) null))
      return (DangerRader) null;
    objectAndComponent.SetRadius(radius);
    return objectAndComponent;
  }

  public void SetRadius(float radius)
  {
    SphereCollider collider = this._collider as SphereCollider;
    if (!Object.op_Inequality((Object) collider, (Object) null))
      return;
    collider.radius = radius;
  }

  private void Awake()
  {
    this._rigidbody = ((Component) this).GetComponent<Rigidbody>();
    this._collider = ((Component) this).GetComponent<Collider>();
    if (Object.op_Equality((Object) this._collider, (Object) null))
    {
      SphereCollider sphereCollider = ((Component) this).gameObject.AddComponent<SphereCollider>();
      sphereCollider.center = new Vector3(0.0f, 0.0f, 0.0f);
      this._collider = (Collider) sphereCollider;
    }
    if (!Object.op_Inequality((Object) this._collider, (Object) null))
      return;
    this._collider.isTrigger = true;
    if (Object.op_Equality((Object) this._rigidbody, (Object) null))
      this._rigidbody = ((Component) this).gameObject.AddComponent<Rigidbody>();
    this._rigidbody.isKinematic = true;
  }

  private void Start() => this.brain = ((Component) this).gameObject.GetComponentInParent<Brain>();

  private void Update()
  {
    if (!this.triggerSpan.IsReady())
      return;
    this._collider.enabled = false;
    this._collider.enabled = true;
  }

  private void OnTriggerEnter(Collider collider)
  {
    if (Object.op_Equality((Object) this.brain, (Object) null) || Object.op_Equality((Object) this._collider, (Object) null) || !this._collider.enabled || !collider.isTrigger || Object.op_Equality((Object) ((Component) collider).gameObject, (Object) ((Component) this).gameObject))
      return;
    BulletObject component = ((Component) collider).gameObject.GetComponent<BulletObject>();
    StageObject stageObject = !Object.op_Inequality((Object) component, (Object) null) ? ((Component) collider).gameObject.GetComponentInParent<StageObject>() : component.stageObject;
    if (Object.op_Equality((Object) stageObject, (Object) null) || Object.op_Equality((Object) stageObject, (Object) this.brain.owner))
      return;
    if (this.RecordCollider(collider, stageObject, component).isMove)
      this.brain.HandleEvent(BRAIN_EVENT.BULLET_CATCH, (object) component);
    else
      this.brain.HandleEvent(BRAIN_EVENT.COLLIDER_CATCH, (object) stageObject);
  }

  public DangerRader.ColliderRecord firstRecord
  {
    get => this.records.Count > 0 ? this.records.First.Value : (DangerRader.ColliderRecord) null;
  }

  private DangerRader.ColliderRecord RecordCollider(
    Collider collider,
    StageObject obj,
    BulletObject bullet)
  {
    DangerRader.ColliderRecord colliderRecord;
    if (this.records.Count >= 20)
    {
      colliderRecord = this.records.Last.Value;
      this.records.RemoveLast();
    }
    else
      colliderRecord = new DangerRader.ColliderRecord();
    colliderRecord.time = Time.time;
    colliderRecord.pos = ((Component) collider).transform.position;
    colliderRecord.forward = ((Component) collider).transform.forward;
    colliderRecord.angle = AIUtility.GetAngle360OfTargetPos(this.brain.owner, colliderRecord.pos);
    colliderRecord.radius = 1f;
    switch (collider)
    {
      case SphereCollider _:
        colliderRecord.radius = (collider as SphereCollider).radius;
        break;
      case CapsuleCollider _:
        CapsuleCollider capsuleCollider = collider as CapsuleCollider;
        colliderRecord.radius = Mathf.Max(capsuleCollider.radius, capsuleCollider.height);
        break;
    }
    colliderRecord.isDash = false;
    Enemy enemy = obj as Enemy;
    if (Object.op_Inequality((Object) enemy, (Object) null))
      colliderRecord.isDash = enemy.enableDash;
    colliderRecord.isBullet = Object.op_Inequality((Object) bullet, (Object) null);
    colliderRecord.isWillHit = false;
    if (colliderRecord.isMove)
    {
      this.nearMoveTime = colliderRecord.time;
      int opponentMask = AIUtility.GetOpponentMask(obj);
      colliderRecord.isWillHit = AIUtility.IsHitObjectFromMoveObject(((Component) collider).transform, ((Component) this.brain.owner).transform, colliderRecord.radius, opponentMask);
      if (colliderRecord.isWillHit)
      {
        if (colliderRecord.isDash)
          this.nearWillDashHitTime = colliderRecord.time;
        if (colliderRecord.isBullet)
          this.nearWillBulletHitTime = colliderRecord.time;
      }
    }
    this.records.AddFirst(colliderRecord);
    return colliderRecord;
  }

  public PLACE GetSafetyPlace()
  {
    return this.firstRecord == null ? PLACE.BACK : AIUtility.GetPlaceOfAngle360(this.firstRecord.angle).Reverse();
  }

  public PLACE GetSafetySide()
  {
    return this.firstRecord == null ? PLACE.LEFT : AIUtility.GetSideOfAngle360(this.firstRecord.angle).Reverse();
  }

  public bool AskDanger(float pass = 0.2f)
  {
    float num = Time.time - pass;
    return this.firstRecord != null && (double) this.firstRecord.time >= (double) num;
  }

  public bool AskDangerMove(float pass = 0.2f)
  {
    return (double) this.nearMoveTime >= (double) (Time.time - pass);
  }

  public bool AskWillHit(float pass = 0.2f)
  {
    return this.AskWillDashHit(pass) || this.AskWillBulletHit(pass);
  }

  public bool AskWillBulletHit(float pass = 0.2f)
  {
    return (double) this.nearWillBulletHitTime >= (double) (Time.time - pass);
  }

  public bool AskWillDashHit(float pass = 0.2f)
  {
    return (double) this.nearWillDashHitTime >= (double) (Time.time - pass);
  }

  public bool AskDangerPosition(Vector3 pos, float pass = 0.2f)
  {
    if (this.records.Count <= 0)
      return false;
    float num = Time.time - pass;
    foreach (DangerRader.ColliderRecord record in this.records)
    {
      if ((double) record.time >= (double) num)
      {
        if ((double) AIUtility.GetLengthWithBetweenPosition(record.pos, pos) < (double) record.radius)
          return true;
      }
      else
        break;
    }
    return false;
  }

  public class ColliderRecord
  {
    public float time;
    public Vector3 pos;
    public float angle;
    public Vector3 forward;
    public float radius;
    public bool isDash;
    public bool isBullet;
    public bool isWillHit;

    public bool isMove => this.isDash || this.isBullet;
  }
}
