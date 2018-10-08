using System.Collections.Generic;
using UnityEngine;

public class AttackColliderProcessor : StageObjectManager.IDetachedNotify
{
	protected float m_hitInterval = 1f;

	protected Player.ATTACK_MODE m_attackMode;

	protected DamageDistanceTable.DamageDistanceData m_damageDistanceData;

	protected bool m_isValidTriggerStay;

	protected bool m_isValidMultiHitInterval;

	public AttackInfo attackInfo
	{
		get;
		protected set;
	}

	public StageObject fromObject
	{
		get;
		protected set;
	}

	public Collider fromCollider
	{
		get;
		protected set;
	}

	public IAttackCollider colliderInterface
	{
		get;
		protected set;
	}

	public uint checkIndex
	{
		get;
		protected set;
	}

	public List<TargetPoint> targetPointList
	{
		get;
		protected set;
	}

	public void ValidTriggerStay()
	{
		m_isValidTriggerStay = true;
	}

	public void ValidMultiHitInterval()
	{
		m_isValidMultiHitInterval = true;
	}

	public virtual void SetFromInfo(AttackInfo _attack_info, StageObject _object, Collider _collider, IAttackCollider _collider_interface)
	{
		attackInfo = _attack_info;
		fromObject = _object;
		fromCollider = _collider;
		colliderInterface = _collider_interface;
		targetPointList = null;
		Player player = fromObject as Player;
		if ((Object)player != (Object)null && player.targetingPointList != null)
		{
			targetPointList = player.targetingPointList.GetRange(0, player.targetingPointList.Count);
		}
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			MonoBehaviourSingleton<StageObjectManager>.I.AddNotifyInterface(this);
		}
	}

	public virtual void OnDestroy()
	{
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			MonoBehaviourSingleton<StageObjectManager>.I.RemoveNotifyInterface(this);
		}
		if (MonoBehaviourSingleton<AttackColliderManager>.IsValid())
		{
			MonoBehaviourSingleton<AttackColliderManager>.I.RemoveProcessor(this);
		}
	}

	public virtual void OnDetachedObject(StageObject stage_object)
	{
		if ((Object)fromObject == (Object)stage_object)
		{
			fromObject = null;
		}
	}

	public virtual bool IsBusy()
	{
		return false;
	}

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

	public void SetAttackMode(Player.ATTACK_MODE attackMode)
	{
		m_attackMode = attackMode;
	}

	public void SetDamageDistanceData(DamageDistanceTable.DamageDistanceData damageDistanceData)
	{
		m_damageDistanceData = damageDistanceData;
	}
}
