using System.Collections.Generic;
using UnityEngine;

public class AttackHitColliderProcessor : AttackColliderProcessor
{
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

		public Vector3 exHitPos = Vector3.get_zero();
	}

	public class HitResult
	{
		public StageObject target;

		public List<HitParam> hitParams = new List<HitParam>();
	}

	private bool isAlreadyCheckedTask;

	private bool isAlreadyCheckedDeliveryBattleInfo;

	protected List<HitResult> stackList = new List<HitResult>();

	protected AttackHitInfo attackHitInfo => base.attackInfo as AttackHitInfo;

	public override void OnDestroy()
	{
		CheckStackList();
		base.OnDestroy();
	}

	public override bool IsBusy()
	{
		return stackList.Count > 0;
	}

	public override void OnFixedUpdate()
	{
		base.OnFixedUpdate();
		CheckStackList();
	}

	private void CheckStackList()
	{
		if (stackList.Count <= 0)
		{
			return;
		}
		if (base.fromObject == null)
		{
			stackList.Clear();
			return;
		}
		List<HitResult> range = stackList.GetRange(0, stackList.Count);
		stackList.Clear();
		if (base.colliderInterface != null)
		{
			base.colliderInterface.SortHitStackList(range);
		}
		int i = 0;
		for (int count = range.Count; i < count; i++)
		{
			HitResult hitResult = range[i];
			HitParam hitParam = hitResult.target.SelectHitCollider(this, hitResult.hitParams);
			if (base.colliderInterface != null && !base.colliderInterface.CheckHitAttack(attackHitInfo, hitParam.toCollider, hitResult.target))
			{
				continue;
			}
			if (base.fromObject.CheckHitAttack(attackHitInfo, hitParam.toCollider, hitResult.target))
			{
				base.fromObject.OnHitAttack(attackHitInfo, hitParam);
				if (base.colliderInterface != null)
				{
					base.colliderInterface.OnHitAttack(attackHitInfo, hitParam);
					if (!base.colliderInterface.IsEnable())
					{
						break;
					}
				}
			}
			else
			{
				Self self = base.fromObject as Self;
				if (self != null)
				{
					self.CancelHit();
				}
			}
		}
	}

	public override void OnTriggerStay(Collider to_collider)
	{
		base.OnTriggerStay(to_collider);
		if (!m_isValidTriggerStay || attackHitInfo.hitIntervalTime <= 0f)
		{
			return;
		}
		if (m_isValidMultiHitInterval)
		{
			HitProc(to_collider);
			return;
		}
		m_hitInterval -= Time.get_deltaTime();
		if (!(m_hitInterval > 0f))
		{
			m_hitInterval = attackHitInfo.hitIntervalTime;
			HitProc(to_collider);
		}
	}

	public override void OnTriggerEnter(Collider to_collider)
	{
		base.OnTriggerEnter(to_collider);
		HitProc(to_collider);
		m_hitInterval = attackHitInfo.hitIntervalTime;
	}

	private void HitProc(Collider to_collider)
	{
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		if ((base.colliderInterface != null && !base.colliderInterface.IsEnable()) || base.fromCollider == null || base.fromObject == null || !base.fromCollider.get_enabled() || to_collider.get_gameObject() == base.fromCollider.get_gameObject())
		{
			return;
		}
		if (to_collider.get_isTrigger())
		{
			BarrierBulletObject component = to_collider.get_gameObject().GetComponent<BarrierBulletObject>();
			if (component == null)
			{
				return;
			}
		}
		StageObject to_object = to_collider.get_gameObject().GetComponentInParent<StageObject>();
		if (to_object == null || to_object == base.fromObject || to_object.ignoreHitAttackColliders.IndexOf(to_collider) >= 0)
		{
			return;
		}
		if (m_isValidTriggerStay && m_isValidMultiHitInterval)
		{
			if (to_object.IsIgnoreByHitInterval(base.fromCollider))
			{
				return;
			}
			to_object.SetHitIntervalStatus(base.fromCollider, attackHitInfo.hitIntervalTime);
		}
		if (base.colliderInterface != null && !base.colliderInterface.CheckHitAttack(attackHitInfo, to_collider, to_object))
		{
			return;
		}
		if (!base.fromObject.CheckHitAttack(attackHitInfo, to_collider, to_object))
		{
			to_object.OnAvoidHit(base.fromObject, attackHitInfo);
			return;
		}
		Vector3 crossCheckPoint = base.colliderInterface.GetCrossCheckPoint(base.fromCollider);
		Vector3 val = Utility.ClosestPointOnCollider(to_collider, crossCheckPoint);
		Vector3 val2 = val - crossCheckPoint;
		if (val2 == Vector3.get_zero())
		{
			Bounds bounds = to_collider.get_bounds();
			val2 = bounds.get_center() - crossCheckPoint;
		}
		Quaternion rot = Quaternion.LookRotation(val2);
		HitResult hitResult = null;
		stackList.ForEach(delegate(HitResult o)
		{
			if (o.target == to_object)
			{
				hitResult = o;
			}
		});
		if (hitResult == null)
		{
			hitResult = new HitResult();
			hitResult.target = to_object;
			stackList.Add(hitResult);
		}
		float time = 0f;
		if (base.colliderInterface != null)
		{
			time = base.colliderInterface.GetTime();
		}
		HitParam hitParam = new HitParam();
		hitParam.processor = this;
		hitParam.fromObject = base.fromObject;
		hitParam.toObject = to_object;
		hitParam.fromCollider = base.fromCollider;
		hitParam.toCollider = to_collider;
		hitParam.point = val;
		if (m_damageDistanceData != null)
		{
			Vector3 val3 = crossCheckPoint;
			BulletObject bulletObject = base.colliderInterface as BulletObject;
			if (bulletObject != null)
			{
				val3 = bulletObject.startColliderPos;
			}
			Vector3 val4 = Utility.ClosestPointOnColliderFix(to_collider, val3);
			if (val3 == val4)
			{
				hitParam.distanceXZ = 0f;
			}
			else
			{
				Vector2 val5 = val4.ToVector2XZ();
				Vector2 val6 = base.fromObject._position.ToVector2XZ();
				hitParam.distanceXZ = Vector2.Distance(val5, val6);
			}
		}
		hitParam.exHitPos = base.fromCollider.get_gameObject().get_transform().get_position();
		hitParam.rot = rot;
		hitParam.time = time;
		hitParam.targetPointList = base.targetPointList;
		hitParam.crossCheckPoint = crossCheckPoint;
		hitParam.attackMode = m_attackMode;
		hitParam.damageDistanceData = m_damageDistanceData;
		hitResult.hitParams.Add(hitParam);
		Self self = base.fromObject as Self;
		if (self != null && !isAlreadyCheckedTask)
		{
			BattleCheckerBase.JudgementParam judgementParam = BattleCheckerBase.JudgementParam.Create(base.attackInfo, self);
			self.taskChecker.OnAttackHit(base.attackInfo.name, judgementParam);
			isAlreadyCheckedTask = true;
		}
		if (base.fromObject is Player && MonoBehaviourSingleton<InGameManager>.IsValid() && !isAlreadyCheckedDeliveryBattleInfo)
		{
			MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.AddTotalAttackCount();
		}
		if (base.fromObject is Self && MonoBehaviourSingleton<InGameManager>.IsValid() && !isAlreadyCheckedDeliveryBattleInfo)
		{
			MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.AddAttackCount();
			isAlreadyCheckedDeliveryBattleInfo = true;
		}
	}
}
