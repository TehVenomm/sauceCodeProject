// Decompiled with JetBrains decompiler
// Type: AttackColliderObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AttackColliderObject : MonoBehaviour, IAttackCollider
{
  private StageObject m_attacker;
  private CapsuleCollider m_capsule;
  private Rigidbody m_rigidBody;
  private AttackInfo m_attackInfo;
  private AttackColliderProcessor m_colliderProcessor;
  protected AttackHitChecker m_attackHitChecker;
  private float m_timeCount;

  public void Initialize(
    StageObject attacker,
    Transform parent,
    AttackInfo atkInfo,
    Vector3 pos,
    Vector3 rot,
    float radius,
    float height,
    int attackLayer)
  {
    ((Component) this).gameObject.layer = attackLayer;
    this.m_attacker = attacker;
    this.m_attackInfo = atkInfo;
    Transform transform = ((Component) this).transform;
    transform.parent = parent;
    transform.localEulerAngles = rot;
    transform.localPosition = Quaternion.op_Multiply(transform.localRotation, pos);
    transform.localScale = Vector3.one;
    this.m_capsule.direction = 2;
    this.m_capsule.radius = radius;
    this.m_capsule.height = height;
    ((Collider) this.m_capsule).enabled = true;
    this.m_capsule.center = new Vector3(0.0f, 0.0f, height * 0.5f);
    ((Collider) this.m_capsule).isTrigger = true;
    this.m_timeCount = 0.0f;
    if (!MonoBehaviourSingleton<AttackColliderManager>.IsValid())
      return;
    this.m_colliderProcessor = MonoBehaviourSingleton<AttackColliderManager>.I.CreateProcessor(this.m_attackInfo, attacker, (Collider) this.m_capsule, (IAttackCollider) this);
    this.m_attackHitChecker = attacker.ReferenceAttackHitChecker();
  }

  public void Initialize(
    StageObject attacker,
    Transform parent,
    AttackInfo atkInfo,
    Vector3 pos,
    Vector3 rot,
    float radius,
    float height,
    int direction,
    Vector3 center,
    int attackLayer)
  {
    ((Component) this).gameObject.layer = attackLayer;
    this.m_attacker = attacker;
    this.m_attackInfo = atkInfo;
    Transform transform = ((Component) this).transform;
    transform.parent = parent;
    transform.localEulerAngles = rot;
    transform.localPosition = pos;
    transform.localScale = Vector3.one;
    this.m_capsule.direction = direction;
    this.m_capsule.radius = radius;
    this.m_capsule.height = height;
    ((Collider) this.m_capsule).enabled = true;
    this.m_capsule.center = center;
    ((Collider) this.m_capsule).isTrigger = true;
    this.m_timeCount = 0.0f;
    if (!MonoBehaviourSingleton<AttackColliderManager>.IsValid())
      return;
    this.m_colliderProcessor = MonoBehaviourSingleton<AttackColliderManager>.I.CreateProcessor(this.m_attackInfo, attacker, (Collider) this.m_capsule, (IAttackCollider) this);
    this.m_attackHitChecker = attacker.ReferenceAttackHitChecker();
  }

  public void InitializeForExAtkCollider(
    StageObject attacker,
    Transform parent,
    AttackInfo atkInfo,
    Vector3 pos,
    Vector3 rot,
    float radius,
    float height,
    int attackLayer)
  {
    ((Component) this).gameObject.layer = attackLayer;
    this.m_attacker = attacker;
    this.m_attackInfo = atkInfo;
    Transform transform = ((Component) this).transform;
    transform.parent = parent;
    transform.localEulerAngles = rot;
    transform.localPosition = pos;
    transform.localScale = Vector3.one;
    this.m_capsule.direction = 2;
    this.m_capsule.radius = radius;
    this.m_capsule.height = height;
    ((Collider) this.m_capsule).enabled = true;
    this.m_capsule.center = new Vector3(0.0f, 0.0f, height * 0.5f);
    ((Collider) this.m_capsule).isTrigger = true;
    this.m_timeCount = 0.0f;
    if (MonoBehaviourSingleton<AttackColliderManager>.IsValid())
    {
      this.m_colliderProcessor = MonoBehaviourSingleton<AttackColliderManager>.I.CreateProcessor(this.m_attackInfo, attacker, (Collider) this.m_capsule, (IAttackCollider) this);
      this.m_attackHitChecker = attacker.ReferenceAttackHitChecker();
    }
    if (this.m_colliderProcessor == null || this.m_attackInfo == null || !(this.m_attackInfo is AttackHitInfo attackInfo) || !attackInfo.isValidTriggerStay)
      return;
    this.m_colliderProcessor.ValidTriggerStay();
    this.m_colliderProcessor.ValidMultiHitInterval();
  }

  public virtual void Destroy()
  {
    if (Object.op_Inequality((Object) this.m_rigidBody, (Object) null))
      this.m_rigidBody.Sleep();
    if (this.m_attackInfo != null)
    {
      BulletData bulletData = this.m_attackInfo.bulletData;
      if (Object.op_Inequality((Object) bulletData, (Object) null) && !string.IsNullOrEmpty(bulletData.data.landHiteffectName))
      {
        Transform effect = EffectManager.GetEffect(bulletData.data.landHiteffectName);
        if (Object.op_Inequality((Object) effect, (Object) null))
        {
          effect.position = ((Component) this).transform.position;
          effect.rotation = ((Component) this).transform.rotation;
        }
      }
    }
    Object.Destroy((Object) ((Component) this).gameObject);
  }

  protected void Awake()
  {
    this.m_capsule = ((Component) this).gameObject.AddComponent<CapsuleCollider>();
    this.m_rigidBody = ((Component) this).gameObject.GetComponent<Rigidbody>();
    if (Object.op_Equality((Object) this.m_rigidBody, (Object) null))
      this.m_rigidBody = ((Component) this).gameObject.AddComponent<Rigidbody>();
    this.m_rigidBody.useGravity = false;
  }

  private void Update() => this.m_timeCount += Time.deltaTime;

  protected virtual void OnTriggerEnter(Collider collider)
  {
    if (this.m_colliderProcessor == null)
      return;
    this.m_colliderProcessor.OnTriggerEnter(collider);
  }

  protected virtual void OnTriggerStay(Collider collider)
  {
    if (this.m_colliderProcessor == null)
      return;
    this.m_colliderProcessor.OnTriggerStay(collider);
  }

  protected virtual void OnTriggerExit(Collider collider)
  {
    if (this.m_colliderProcessor == null)
      return;
    this.m_colliderProcessor.OnTriggerExit(collider);
  }

  protected void ActivateOwnCollider()
  {
    if (!Object.op_Inequality((Object) this.m_capsule, (Object) null))
      return;
    ((Collider) this.m_capsule).enabled = true;
  }

  protected void DeactivateOwnCollider()
  {
    if (!Object.op_Inequality((Object) this.m_capsule, (Object) null))
      return;
    ((Collider) this.m_capsule).enabled = false;
  }

  public void ValidTriggerStay()
  {
    if (this.m_colliderProcessor == null)
      return;
    this.m_colliderProcessor.ValidTriggerStay();
  }

  public virtual void OnHitTrigger(Collider to_collider, StageObject to_object)
  {
  }

  public virtual float GetTime() => this.m_timeCount;

  public virtual bool IsEnable() => true;

  public virtual void SortHitStackList(
    List<AttackHitColliderProcessor.HitResult> stack_list)
  {
  }

  public virtual Vector3 GetCrossCheckPoint(Collider from_collider)
  {
    Bounds bounds = from_collider.bounds;
    Vector3 crossCheckPoint = ((Bounds) ref bounds).center;
    Character attacker = this.m_attacker as Character;
    if (Object.op_Inequality((Object) attacker, (Object) null) && Object.op_Inequality((Object) attacker.rootNode, (Object) null))
      crossCheckPoint = attacker.rootNode.position;
    return crossCheckPoint;
  }

  public virtual bool CheckHitAttack(
    AttackHitInfo info,
    Collider to_collider,
    StageObject to_object)
  {
    return this.m_attackHitChecker == null || this.m_attackHitChecker.CheckHitAttack(info, to_collider, to_object);
  }

  public virtual void OnHitAttack(AttackHitInfo info, AttackHitColliderProcessor.HitParam hit_param)
  {
    if (this.m_attackHitChecker == null)
      return;
    this.m_attackHitChecker.OnHitAttack(info, hit_param);
  }

  public AttackInfo GetAttackInfo() => this.m_colliderProcessor.attackInfo;

  public StageObject GetFromObject() => this.m_colliderProcessor.fromObject;

  public void DetachRigidbody()
  {
    if (!Object.op_Inequality((Object) this.m_rigidBody, (Object) null))
      return;
    Object.Destroy((Object) this.m_rigidBody);
    this.m_rigidBody = (Rigidbody) null;
  }

  public int UniqueID { get; set; }
}
