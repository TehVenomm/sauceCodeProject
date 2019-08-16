using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletControllerRondomHoming : BulletControllerHoming
{
	private int targetIndex = -1;

	private bool targetNull;

	private EnemyRegionWork targetRegionWork;

	private Enemy.RegionInfo targetRegionInfo;

	protected override Vector3 GetTargetPos()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = base._transform.get_position();
		Enemy enemy = targetObject as Enemy;
		if (targetNull || enemy == null || enemy.targetPoints == null || enemy.targetPoints.Length == 0)
		{
			return base.GetTargetPos();
		}
		Vector3 result = Vector3.get_zero();
		if (!TryGetTargetPoint(enemy, out Vector3 targetPos))
		{
			targetNull = true;
			targetPos = base.GetTargetPos();
		}
		else
		{
			result = targetPos - position;
		}
		return result;
	}

	private bool TryGetTargetPoint(Enemy enemy, out Vector3 targetPos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		targetPos = Vector3.get_zero();
		TargetPoint[] targetPoints = enemy.targetPoints;
		if (targetIndex != -1)
		{
			targetPos = targetPoints[targetIndex].GetTargetPoint();
			if (targetPos.y > 0f)
			{
				if (IsTargetablePoint(targetPoints[targetIndex], targetRegionInfo, targetRegionWork) && targetPoints[targetIndex] != null)
				{
					return true;
				}
				targetIndex = -1;
				targetNull = true;
				return false;
			}
			targetIndex = -1;
			targetNull = true;
			return false;
		}
		List<int> list = new List<int>();
		for (int j = 0; j < targetPoints.Length; j++)
		{
			Vector3 targetPoint = targetPoints[j].GetTargetPoint();
			if (targetPoint.y > 0f)
			{
				list.Add(j);
			}
		}
		if (list.Count == 0)
		{
			return false;
		}
		list = (from i in list
		orderby Guid.NewGuid()
		select i).ToList();
		TargetPoint targetPoint2 = null;
		int num = 0;
		EnemyRegionWork[] regionWorks = enemy.regionWorks;
		if (regionWorks != null || regionWorks.Length > 0)
		{
			for (int k = 0; k < list.Count; k++)
			{
				num = list[k];
				TargetPoint targetPoint3 = targetPoints[num];
				if (!(targetPoint3 != null) || targetPoint3.regionID == -1)
				{
					continue;
				}
				for (int l = 0; l < regionWorks.Length; l++)
				{
					EnemyRegionWork enemyRegionWork = regionWorks[l];
					Enemy.RegionInfo regionInfo = regionWorks[l].regionInfo;
					if (enemyRegionWork.regionId == targetPoint3.regionID)
					{
						if (IsTargetablePoint(targetPoints[num], regionInfo, enemyRegionWork))
						{
							targetRegionWork = enemyRegionWork;
							targetRegionInfo = regionInfo;
							targetPoint2 = targetPoint3;
						}
						break;
					}
				}
				if (targetPoint2 != null)
				{
					break;
				}
			}
		}
		if (targetPoint2 == null)
		{
			return false;
		}
		targetPos = targetPoint2.GetTargetPoint();
		targetIndex = num;
		return true;
	}

	private bool IsTargetablePoint(TargetPoint targetPoint, Enemy.RegionInfo regionInfo, EnemyRegionWork regionWork)
	{
		return targetPoint.get_gameObject().get_activeInHierarchy() && (regionInfo.maxHP <= 0 || regionInfo.breakAfterHit || regionWork.hp.value > 0);
	}
}
