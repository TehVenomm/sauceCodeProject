using System.Collections.Generic;
using UnityEngine;

public class AttackContinuationColliderProcessor : AttackColliderProcessor
{
	protected List<StageObject> enterList = new List<StageObject>();

	protected AttackContinuationInfo attackContinuationInfo => base.attackInfo as AttackContinuationInfo;

	public override void OnDestroy()
	{
		base.OnDestroy();
		int i = 0;
		for (int count = enterList.Count; i < count; i++)
		{
			enterList[i].OnContinuationExit(attackContinuationInfo, base.fromCollider);
		}
		enterList.Clear();
	}

	public override void OnFixedUpdate()
	{
		base.OnFixedUpdate();
	}

	public override void OnTriggerEnter(Collider to_collider)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		base.OnTriggerEnter(to_collider);
		if (!(base.fromCollider == null) && !(base.fromObject == null) && base.fromCollider.get_enabled() && !(to_collider.get_gameObject() == base.fromCollider.get_gameObject()))
		{
			StageObject componentInParent = to_collider.get_gameObject().GetComponentInParent<StageObject>();
			if (!(componentInParent == null))
			{
				float time = 0f;
				if (base.colliderInterface != null)
				{
					time = base.colliderInterface.GetTime();
				}
				if (componentInParent.OnContinuationEnter(attackContinuationInfo, base.fromObject, base.fromCollider, time))
				{
					enterList.Add(componentInParent);
				}
			}
		}
	}

	public override void OnTriggerExit(Collider to_collider)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		base.OnTriggerExit(to_collider);
		StageObject componentInParent = to_collider.get_gameObject().GetComponentInParent<StageObject>();
		if (!(componentInParent == null))
		{
			int num = 0;
			int count = enterList.Count;
			while (true)
			{
				if (num >= count)
				{
					return;
				}
				if (enterList[num] == componentInParent)
				{
					break;
				}
				num++;
			}
			componentInParent.OnContinuationExit(attackContinuationInfo, base.fromCollider);
			enterList.RemoveAt(num);
		}
	}
}
