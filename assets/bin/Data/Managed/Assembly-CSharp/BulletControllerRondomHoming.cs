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
		Vector3 position = base._transform.position;
		Enemy enemy = targetObject as Enemy;
		if (targetNull || enemy == null || enemy.targetPoints == null || enemy.targetPoints.Length == 0)
		{
			return base.GetTargetPos();
		}
		Vector3 result = Vector3.zero;
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
		targetPos = Vector3.zero;
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
			if (targetPoints[j].GetTargetPoint().y > 0f)
			{
				list.Add(j);
			}
		}
		if (list.Count == 0)
		{
			return false;
		}
		list = list.OrderBy((int i) => Guid.NewGuid()).ToList();
		TargetPoint targetPoint = null;
		int num = 0;
		EnemyRegionWork[] regionWorks = enemy.regionWorks;
		if (regionWorks != null || regionWorks.Length != 0)
		{
			for (int k = 0; k < list.Count; k++)
			{
				num = list[k];
				TargetPoint targetPoint2 = targetPoints[num];
				if (!(targetPoint2 != null) || targetPoint2.regionID == -1)
				{
					continue;
				}
				for (int l = 0; l < regionWorks.Length; l++)
				{
					EnemyRegionWork enemyRegionWork = regionWorks[l];
					Enemy.RegionInfo regionInfo = regionWorks[l].regionInfo;
					if (enemyRegionWork.regionId == targetPoint2.regionID)
					{
						if (IsTargetablePoint(targetPoints[num], regionInfo, enemyRegionWork))
						{
							targetRegionWork = enemyRegionWork;
							targetRegionInfo = regionInfo;
							targetPoint = targetPoint2;
						}
						break;
					}
				}
				if (targetPoint != null)
				{
					break;
				}
			}
		}
		if (targetPoint == null)
		{
			return false;
		}
		targetPos = targetPoint.GetTargetPoint();
		targetIndex = num;
		return true;
	}

	private bool IsTargetablePoint(TargetPoint targetPoint, Enemy.RegionInfo regionInfo, EnemyRegionWork regionWork)
	{
		if (targetPoint.gameObject.activeInHierarchy)
		{
			if (regionInfo.maxHP > 0 && !regionInfo.breakAfterHit)
			{
				return regionWork.hp.value > 0;
			}
			return true;
		}
		return false;
	}
}
