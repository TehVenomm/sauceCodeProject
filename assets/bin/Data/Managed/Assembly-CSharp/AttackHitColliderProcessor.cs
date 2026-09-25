// Decompiled with JetBrains decompiler
// Type: AttackHitColliderProcessor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AttackHitColliderProcessor : AttackColliderProcessor
{
  private bool isAlreadyCheckedTask;
  private bool isAlreadyCheckedDeliveryBattleInfo;
  protected List<AttackHitColliderProcessor.HitResult> stackList = new List<AttackHitColliderProcessor.HitResult>();

  protected AttackHitInfo attackHitInfo => this.attackInfo as AttackHitInfo;

  public override void OnDestroy()
  {
    this.CheckStackList();
    base.OnDestroy();
  }

  public override bool IsBusy() => this.stackList.Count > 0;

  public override void OnFixedUpdate()
  {
    base.OnFixedUpdate();
    this.CheckStackList();
  }

  private void CheckStackList()
  {
    if (this.stackList.Count <= 0)
      return;
    if (Object.op_Equality((Object) this.fromObject, (Object) null))
    {
      this.stackList.Clear();
    }
    else
    {
      List<AttackHitColliderProcessor.HitResult> range = this.stackList.GetRange(0, this.stackList.Count);
      this.stackList.Clear();
      if (this.colliderInterface != null)
        this.colliderInterface.SortHitStackList(range);
      int index = 0;
      for (int count = range.Count; index < count; ++index)
      {
        AttackHitColliderProcessor.HitResult hitResult = range[index];
        AttackHitColliderProcessor.HitParam hit_param = hitResult.target.SelectHitCollider(this, hitResult.hitParams);
        if (this.colliderInterface == null || this.colliderInterface.CheckHitAttack(this.attackHitInfo, hit_param.toCollider, hitResult.target))
        {
          if (this.fromObject.CheckHitAttack(this.attackHitInfo, hit_param.toCollider, hitResult.target))
          {
            this.fromObject.OnHitAttack(this.attackHitInfo, hit_param);
            if (this.colliderInterface != null)
            {
              this.colliderInterface.OnHitAttack(this.attackHitInfo, hit_param);
              if (!this.colliderInterface.IsEnable())
                break;
            }
          }
          else
          {
            Self fromObject = this.fromObject as Self;
            if (Object.op_Inequality((Object) fromObject, (Object) null))
              fromObject.CancelHit();
          }
        }
      }
    }
  }

  public override void OnTriggerStay(Collider to_collider)
  {
    base.OnTriggerStay(to_collider);
    if (!this.m_isValidTriggerStay || (double) this.attackHitInfo.hitIntervalTime <= 0.0)
      return;
    if (this.m_isValidMultiHitInterval)
    {
      this.HitProc(to_collider);
    }
    else
    {
      this.m_hitInterval -= Time.deltaTime;
      if ((double) this.m_hitInterval > 0.0)
        return;
      this.m_hitInterval = this.attackHitInfo.hitIntervalTime;
      this.HitProc(to_collider);
    }
  }

  public override void OnTriggerEnter(Collider to_collider)
  {
    base.OnTriggerEnter(to_collider);
    this.HitProc(to_collider);
    this.m_hitInterval = this.attackHitInfo.hitIntervalTime;
  }

  private void HitProc(Collider to_collider)
  {
    if (this.colliderInterface != null && !this.colliderInterface.IsEnable() || Object.op_Equality((Object) this.fromCollider, (Object) null) || Object.op_Equality((Object) this.fromObject, (Object) null) || !this.fromCollider.enabled || Object.op_Equality((Object) ((Component) to_collider).gameObject, (Object) ((Component) this.fromCollider).gameObject) || to_collider.isTrigger && Object.op_Equality((Object) ((Component) to_collider).gameObject.GetComponent<BarrierBulletObject>(), (Object) null))
      return;
    StageObject to_object = ((Component) to_collider).gameObject.GetComponentInParent<StageObject>();
    if (Object.op_Equality((Object) to_object, (Object) null) || Object.op_Equality((Object) to_object, (Object) this.fromObject) || to_object.ignoreHitAttackColliders.IndexOf(to_collider) >= 0)
      return;
    if (this.m_isValidTriggerStay && this.m_isValidMultiHitInterval)
    {
      if (to_object.IsIgnoreByHitInterval(this.fromCollider))
        return;
      to_object.SetHitIntervalStatus(this.fromCollider, this.attackHitInfo.hitIntervalTime);
    }
    if (this.colliderInterface != null && !this.colliderInterface.CheckHitAttack(this.attackHitInfo, to_collider, to_object))
      return;
    if (!this.fromObject.CheckHitAttack(this.attackHitInfo, to_collider, to_object))
    {
      to_object.OnAvoidHit(this.fromObject, this.attackHitInfo);
    }
    else
    {
      Vector3 crossCheckPoint = this.colliderInterface.GetCrossCheckPoint(this.fromCollider);
      Vector3 vector3_1 = Utility.ClosestPointOnCollider(to_collider, crossCheckPoint);
      Vector3 vector3_2 = Vector3.op_Subtraction(vector3_1, crossCheckPoint);
      if (Vector3.op_Equality(vector3_2, Vector3.zero))
      {
        Bounds bounds = to_collider.bounds;
        vector3_2 = Vector3.op_Subtraction(((Bounds) ref bounds).center, crossCheckPoint);
      }
      Quaternion quaternion = Quaternion.LookRotation(vector3_2);
      AttackHitColliderProcessor.HitResult hitResult = (AttackHitColliderProcessor.HitResult) null;
      this.stackList.ForEach((Action<AttackHitColliderProcessor.HitResult>) (o =>
      {
        if (!Object.op_Equality((Object) o.target, (Object) to_object))
          return;
        hitResult = o;
      }));
      if (hitResult == null)
      {
        hitResult = new AttackHitColliderProcessor.HitResult();
        hitResult.target = to_object;
        this.stackList.Add(hitResult);
      }
      float num = 0.0f;
      if (this.colliderInterface != null)
        num = this.colliderInterface.GetTime();
      AttackHitColliderProcessor.HitParam hitParam = new AttackHitColliderProcessor.HitParam();
      hitParam.processor = this;
      hitParam.fromObject = this.fromObject;
      hitParam.toObject = to_object;
      hitParam.fromCollider = this.fromCollider;
      hitParam.toCollider = to_collider;
      hitParam.point = vector3_1;
      if (this.m_damageDistanceData != null)
      {
        Vector3 point = crossCheckPoint;
        BulletObject colliderInterface = this.colliderInterface as BulletObject;
        if (Object.op_Inequality((Object) colliderInterface, (Object) null))
          point = colliderInterface.startColliderPos;
        Vector3 vector3_3 = Utility.ClosestPointOnColliderFix(to_collider, point);
        if (Vector3.op_Equality(point, vector3_3))
        {
          hitParam.distanceXZ = 0.0f;
        }
        else
        {
          Vector2 vector2Xz1 = vector3_3.ToVector2XZ();
          Vector2 vector2Xz2 = this.fromObject._position.ToVector2XZ();
          hitParam.distanceXZ = Vector2.Distance(vector2Xz1, vector2Xz2);
        }
      }
      hitParam.exHitPos = ((Component) this.fromCollider).gameObject.transform.position;
      hitParam.rot = quaternion;
      hitParam.time = num;
      hitParam.targetPointList = this.targetPointList;
      hitParam.crossCheckPoint = crossCheckPoint;
      hitParam.attackMode = this.m_attackMode;
      hitParam.damageDistanceData = this.m_damageDistanceData;
      hitResult.hitParams.Add(hitParam);
      Self fromObject = this.fromObject as Self;
      if (Object.op_Inequality((Object) fromObject, (Object) null) && !this.isAlreadyCheckedTask)
      {
        BattleCheckerBase.JudgementParam judgementParam = BattleCheckerBase.JudgementParam.Create(this.attackInfo, fromObject);
        fromObject.taskChecker.OnAttackHit(this.attackInfo.name, judgementParam);
        this.isAlreadyCheckedTask = true;
      }
      if (this.fromObject is Player && MonoBehaviourSingleton<InGameManager>.IsValid() && !this.isAlreadyCheckedDeliveryBattleInfo)
        MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.AddTotalAttackCount();
      if (!(this.fromObject is Self) || !MonoBehaviourSingleton<InGameManager>.IsValid() || this.isAlreadyCheckedDeliveryBattleInfo)
        return;
      MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.AddAttackCount();
      this.isAlreadyCheckedDeliveryBattleInfo = true;
    }
  }

  public class HitParam
  {
    public AttackHitColliderProcessor processor;
    public StageObject fromObject;
    public StageObject toObject;
    public Collider fromCollider;
    public Collider toCollider;
    public Vector3 point;
    public float distanceXZ;
    public Quaternion rot;
    public Vector3 crossCheckPoint;
    public float time;
    public List<TargetPoint> targetPointList;
    public int regionID = -1;
    public bool isHitAim;
    public bool isSpAttackHit;
    public Player.ATTACK_MODE attackMode;
    public DamageDistanceTable.DamageDistanceData damageDistanceData;
    public Vector3 exHitPos = Vector3.zero;
  }

  public class HitResult
  {
    public StageObject target;
    public List<AttackHitColliderProcessor.HitParam> hitParams = new List<AttackHitColliderProcessor.HitParam>();
  }
}
