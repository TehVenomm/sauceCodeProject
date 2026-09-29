// Decompiled with JetBrains decompiler
// Type: HealAttackObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class HealAttackObject : MonoBehaviour, IAttackCollider
{
  private Player m_player;
  private StageObject m_attacker;
  private CapsuleCollider m_capsule;
  private Rigidbody m_rigidBody;
  protected AttackInfo m_attackInfo;
  protected AttackColliderProcessor m_colliderProcessor;
  protected AttackHitChecker m_attackHitChecker;
  protected float m_timeCount;

  protected virtual bool isDuplicateAttackInfo => false;

  protected virtual string GetAttackInfoName() => "sk_heal_atk";

  public void Initialize(
    StageObject attacker,
    Transform parent,
    SkillInfo.SkillParam skillParam,
    Vector3 pos,
    Vector3 rot,
    float height,
    int attackLayer)
  {
    this._Initialize(attacker, parent, pos, rot, height, attackLayer, (float) skillParam.healHp, (float) skillParam.tableData.skillRange);
  }

  public void Initialize(StageObject attacker, Transform parent, float healAtk, float radius)
  {
    this._Initialize(attacker, parent, Vector3.zero, Vector3.zero, 0.0f, 12, healAtk, radius);
  }

  private void _Initialize(
    StageObject attacker,
    Transform parent,
    Vector3 pos,
    Vector3 rot,
    float height,
    int attackLayer,
    float healAtk,
    float radius)
  {
    this.m_player = attacker as Player;
    ((Component) this).gameObject.layer = attackLayer;
    AttackHitInfo attackInfo = this.m_player.FindAttackInfo(this.GetAttackInfoName(), isDuplicate: this.isDuplicateAttackInfo) as AttackHitInfo;
    attackInfo.atk.normal = healAtk;
    this.m_attacker = attacker;
    this.m_attackInfo = (AttackInfo) attackInfo;
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

  public void Destroy()
  {
    if (Object.op_Inequality((Object) this.m_rigidBody, (Object) null))
      this.m_rigidBody.Sleep();
    Object.Destroy((Object) ((Component) this).gameObject);
  }

  protected void Awake()
  {
    this.m_capsule = ((Component) this).gameObject.AddComponent<CapsuleCollider>();
    this.m_rigidBody = ((Component) this).gameObject.AddComponent<Rigidbody>();
    this.m_rigidBody.useGravity = false;
  }

  protected virtual void Update()
  {
    this.m_timeCount += Time.deltaTime;
    if (this.m_player.isActSkillAction)
      return;
    this.Destroy();
  }

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

  public bool CheckHitAttack(AttackHitInfo info, Collider to_collider, StageObject to_object)
  {
    return (this.m_attackHitChecker == null || this.m_attackHitChecker.CheckHitAttack(info, to_collider, to_object)) && (info.attackType != AttackHitInfo.ATTACK_TYPE.HEAL_ATTACK || !(to_object is Enemy) || (double) (to_object as Enemy).healDamageRate > 0.0);
  }

  public void OnHitAttack(AttackHitInfo info, AttackHitColliderProcessor.HitParam hit_param)
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
