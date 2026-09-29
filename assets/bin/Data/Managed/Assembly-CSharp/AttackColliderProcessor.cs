// Decompiled with JetBrains decompiler
// Type: AttackColliderProcessor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AttackColliderProcessor : StageObjectManager.IDetachedNotify
{
  protected float m_hitInterval = 1f;
  protected Player.ATTACK_MODE m_attackMode;
  protected DamageDistanceTable.DamageDistanceData m_damageDistanceData;
  protected bool m_isValidTriggerStay;
  protected bool m_isValidMultiHitInterval;

  public AttackInfo attackInfo { get; protected set; }

  public StageObject fromObject { get; protected set; }

  public Collider fromCollider { get; protected set; }

  public IAttackCollider colliderInterface { get; protected set; }

  public uint checkIndex { get; protected set; }

  public void ValidTriggerStay() => this.m_isValidTriggerStay = true;

  public void ValidMultiHitInterval() => this.m_isValidMultiHitInterval = true;

  public List<TargetPoint> targetPointList { get; protected set; }

  public virtual void SetFromInfo(
    AttackInfo _attack_info,
    StageObject _object,
    Collider _collider,
    IAttackCollider _collider_interface)
  {
    this.attackInfo = _attack_info;
    this.fromObject = _object;
    this.fromCollider = _collider;
    this.colliderInterface = _collider_interface;
    this.targetPointList = (List<TargetPoint>) null;
    Player fromObject = this.fromObject as Player;
    AttackHitInfo attackHitInfo = _attack_info as AttackHitInfo;
    if (Object.op_Inequality((Object) fromObject, (Object) null) && attackHitInfo != null)
      this.targetPointList = attackHitInfo.attackType != AttackHitInfo.ATTACK_TYPE.ARROW_RAIN || (double) this.attackInfo.rateInfoRate < 1.0 ? fromObject.targetingPointList.GetRange(0, fromObject.targetingPointList.Count) : fromObject.arrowRainTargetPointList.GetRange(0, fromObject.arrowRainTargetPointList.Count);
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
      return;
    MonoBehaviourSingleton<StageObjectManager>.I.AddNotifyInterface((StageObjectManager.IDetachedNotify) this);
  }

  public virtual void OnDestroy()
  {
    if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
      MonoBehaviourSingleton<StageObjectManager>.I.RemoveNotifyInterface((StageObjectManager.IDetachedNotify) this);
    if (!MonoBehaviourSingleton<AttackColliderManager>.IsValid())
      return;
    MonoBehaviourSingleton<AttackColliderManager>.I.RemoveProcessor(this);
  }

  public virtual void OnDetachedObject(StageObject stage_object)
  {
    if (!Object.op_Equality((Object) this.fromObject, (Object) stage_object))
      return;
    this.fromObject = (StageObject) null;
  }

  public virtual bool IsBusy() => false;

  public virtual void OnFixedUpdate()
  {
  }

  public virtual void OnTriggerEnter(Collider to_collider)
  {
  }

  public virtual void OnTriggerStay(Collider to_collider)
  {
  }

  public virtual void OnTriggerExit(Collider to_collider)
  {
  }

  public void SetAttackMode(Player.ATTACK_MODE attackMode) => this.m_attackMode = attackMode;

  public void SetDamageDistanceData(
    DamageDistanceTable.DamageDistanceData damageDistanceData)
  {
    this.m_damageDistanceData = damageDistanceData;
  }
}
