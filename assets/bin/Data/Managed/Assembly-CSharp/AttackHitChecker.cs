using System.Collections.Generic;
using UnityEngine;

public class AttackHitChecker
{
	protected class HitRecord
	{
		public StageObject target;

		public float lastHitTime = -1f;

		public int hitCount;
	}

	protected StringKeyTable<List<HitRecord>> attackHitList = new StringKeyTable<List<HitRecord>>();

	public void OnHitAttack(AttackHitInfo info, AttackHitColliderProcessor.HitParam hit_param)
	{
		List<HitRecord> list = attackHitList.Get(info.name);
		if (list == null)
		{
			list = new List<HitRecord>();
			attackHitList.Add(info.name, list);
		}
		HitRecord hitRecord = null;
		int i = 0;
		for (int count = list.Count; i < count; i++)
		{
			if (list[i].target == hit_param.toObject)
			{
				hitRecord = list[i];
				break;
			}
		}
		if (hitRecord == null)
		{
			hitRecord = new HitRecord();
			hitRecord.target = hit_param.toObject;
			list.Add(hitRecord);
		}
		hitRecord.lastHitTime = Time.get_time();
		hitRecord.hitCount++;
	}

	public bool CheckHitAttack(AttackHitInfo info, Collider to_collider, StageObject to_object)
	{
		List<HitRecord> list = attackHitList.Get(info.name);
		if (list != null)
		{
			int i = 0;
			for (int count = list.Count; i < count; i++)
			{
				HitRecord hitRecord = list[i];
				if (hitRecord.target == to_object)
				{
					if (info.enableIdentityCheck)
					{
						return false;
					}
					if (Time.get_time() - hitRecord.lastHitTime <= info.hitIntervalTime)
					{
						return false;
					}
					if (info.hitCountMax > 0 && info.hitCountMax <= hitRecord.hitCount)
					{
						return false;
					}
				}
			}
		}
		return true;
	}

	public void ClearAll()
	{
		attackHitList.Clear();
	}

	public void ClearHitInfo(AttackHitInfo info)
	{
		ClearHitInfo(info.name);
	}

	public void ClearHitInfo(string infoName)
	{
		List<HitRecord> list = attackHitList.Get(infoName);
		if (!object.ReferenceEquals(list, null))
		{
			list.Clear();
		}
	}

	public void ClearTarget(AttackInfo info, StageObject target)
	{
		List<HitRecord> list = attackHitList.Get(info.name);
		if (object.ReferenceEquals(list, null))
		{
			return;
		}
		int num = 0;
		int count = list.Count;
		HitRecord hitRecord;
		while (true)
		{
			if (num < count)
			{
				hitRecord = list[num];
				if (hitRecord.target == target)
				{
					break;
				}
				num++;
				continue;
			}
			return;
		}
		list.Remove(hitRecord);
	}
}
