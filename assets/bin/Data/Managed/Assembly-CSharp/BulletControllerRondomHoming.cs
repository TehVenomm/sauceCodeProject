// Decompiled with JetBrains decompiler
// Type: BulletControllerRondomHoming
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class BulletControllerRondomHoming : BulletControllerHoming
{
  private int targetIndex = -1;
  private bool targetNull;
  private EnemyRegionWork targetRegionWork;
  private Enemy.RegionInfo targetRegionInfo;

  protected override Vector3 GetTargetPos()
  {
    Vector3 position = this._transform.position;
    Enemy targetObject = this.targetObject as Enemy;
    if (this.targetNull || Object.op_Equality((Object) targetObject, (Object) null) || targetObject.targetPoints == null || targetObject.targetPoints.Length == 0)
      return base.GetTargetPos();
    Vector3 targetPos1 = Vector3.zero;
    Vector3 targetPos2;
    if (!this.TryGetTargetPoint(targetObject, out targetPos2))
    {
      this.targetNull = true;
      targetPos2 = base.GetTargetPos();
    }
    else
      targetPos1 = Vector3.op_Subtraction(targetPos2, position);
    return targetPos1;
  }

  private bool TryGetTargetPoint(Enemy enemy, out Vector3 targetPos)
  {
    targetPos = Vector3.zero;
    TargetPoint[] targetPoints = enemy.targetPoints;
    if (this.targetIndex != -1)
    {
      targetPos = targetPoints[this.targetIndex].GetTargetPoint();
      if ((double) targetPos.y > 0.0)
      {
        if (this.IsTargetablePoint(targetPoints[this.targetIndex], this.targetRegionInfo, this.targetRegionWork) && Object.op_Inequality((Object) targetPoints[this.targetIndex], (Object) null))
          return true;
        this.targetIndex = -1;
        this.targetNull = true;
        return false;
      }
      this.targetIndex = -1;
      this.targetNull = true;
      return false;
    }
    List<int> source = new List<int>();
    for (int index = 0; index < targetPoints.Length; ++index)
    {
      if ((double) targetPoints[index].GetTargetPoint().y > 0.0)
        source.Add(index);
    }
    if (source.Count == 0)
      return false;
    List<int> list = source.OrderBy<int, Guid>((Func<int, Guid>) (i => Guid.NewGuid())).ToList<int>();
    TargetPoint targetPoint1 = (TargetPoint) null;
    int index1 = 0;
    EnemyRegionWork[] regionWorks = enemy.regionWorks;
    if (regionWorks != null || regionWorks.Length != 0)
    {
      for (int index2 = 0; index2 < list.Count; ++index2)
      {
        index1 = list[index2];
        TargetPoint targetPoint2 = targetPoints[index1];
        if (Object.op_Inequality((Object) targetPoint2, (Object) null) && targetPoint2.regionID != -1)
        {
          for (int index3 = 0; index3 < regionWorks.Length; ++index3)
          {
            EnemyRegionWork regionWork = regionWorks[index3];
            Enemy.RegionInfo regionInfo = regionWorks[index3].regionInfo;
            if (regionWork.regionId == targetPoint2.regionID)
            {
              if (this.IsTargetablePoint(targetPoints[index1], regionInfo, regionWork))
              {
                this.targetRegionWork = regionWork;
                this.targetRegionInfo = regionInfo;
                targetPoint1 = targetPoint2;
                break;
              }
              break;
            }
          }
          if (Object.op_Inequality((Object) targetPoint1, (Object) null))
            break;
        }
      }
    }
    if (Object.op_Equality((Object) targetPoint1, (Object) null))
      return false;
    targetPos = targetPoint1.GetTargetPoint();
    this.targetIndex = index1;
    return true;
  }

  private bool IsTargetablePoint(
    TargetPoint targetPoint,
    Enemy.RegionInfo regionInfo,
    EnemyRegionWork regionWork)
  {
    if (!((Component) targetPoint).gameObject.activeInHierarchy)
      return false;
    return regionInfo.maxHP <= 0 || regionInfo.breakAfterHit || regionWork.hp.value > 0;
  }
}
