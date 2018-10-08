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
		if (stackList.Count > 0)
		{
			if (base.fromObject == null)
			{
				stackList.Clear();
			}
			else
			{
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
					if (base.colliderInterface == null || base.colliderInterface.CheckHitAttack(attackHitInfo, hitParam.toCollider, hitResult.target))
					{
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
			}
		}
	}

	public override void OnTriggerStay(Collider to_collider)
	{
		base.OnTriggerStay(to_collider);
		if (m_isValidTriggerStay && !(attackHitInfo.hitIntervalTime <= 0f))
		{
			m_hitInterval -= Time.get_deltaTime();
			if (!(m_hitInterval > 0f))
			{
				m_hitInterval = attackHitInfo.hitIntervalTime;
				HitProc(to_collider);
			}
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
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		if ((base.colliderInterface == null || base.colliderInterface.IsEnable()) && !(base.fromCollider == null) && !(base.fromObject == null) && base.fromCollider.get_enabled() && !to_collider.get_isTrigger() && !(to_collider.get_gameObject() == base.fromCollider.get_gameObject()))
		{
			StageObject to_object = to_collider.get_gameObject().GetComponentInParent<StageObject>();
			if (!(to_object == null) && !(to_object == base.fromObject) && to_object.ignoreHitAttackColliders.IndexOf(to_collider) < 0 && (base.colliderInterface == null || base.colliderInterface.CheckHitAttack(attackHitInfo, to_collider, to_object)) && base.fromObject.CheckHitAttack(attackHitInfo, to_collider, to_object))
			{
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
					self.taskChecker.OnAttackHit(base.attackInfo.name, judgementParam, 0);
					isAlreadyCheckedTask = true;
				}
				if (base.fromObject is Player && MonoBehaviourSingleton<InGameManager>.IsValid() && !isAlreadyCheckedDeliveryBattleInfo)
				{
					MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.AddTotalAttackCount(1);
					isAlreadyCheckedDeliveryBattleInfo = true;
				}
			}
		}
	}
}
