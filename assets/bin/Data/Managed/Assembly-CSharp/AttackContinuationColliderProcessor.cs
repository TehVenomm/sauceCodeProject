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
		base.OnTriggerEnter(to_collider);
		if (!((Object)base.fromCollider == (Object)null) && !((Object)base.fromObject == (Object)null) && base.fromCollider.enabled && !((Object)to_collider.gameObject == (Object)base.fromCollider.gameObject))
		{
			StageObject componentInParent = to_collider.gameObject.GetComponentInParent<StageObject>();
			if (!((Object)componentInParent == (Object)null))
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
		base.OnTriggerExit(to_collider);
		StageObject componentInParent = to_collider.gameObject.GetComponentInParent<StageObject>();
		if (!((Object)componentInParent == (Object)null))
		{
			int num = 0;
			int count = enterList.Count;
			while (true)
			{
				if (num >= count)
				{
					return;
				}
				if ((Object)enterList[num] == (Object)componentInParent)
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
