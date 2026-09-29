// Decompiled with JetBrains decompiler
// Type: AttackHitChecker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class AttackHitChecker
{
  protected StringKeyTable<List<AttackHitChecker.HitRecord>> attackHitList = new StringKeyTable<List<AttackHitChecker.HitRecord>>();

  public void OnHitAttack(AttackHitInfo info, AttackHitColliderProcessor.HitParam hit_param)
  {
    List<AttackHitChecker.HitRecord> hitRecordList = this.attackHitList.Get(info.name);
    if (hitRecordList == null)
    {
      hitRecordList = new List<AttackHitChecker.HitRecord>();
      this.attackHitList.Add(info.name, hitRecordList);
    }
    AttackHitChecker.HitRecord hitRecord = (AttackHitChecker.HitRecord) null;
    int index = 0;
    for (int count = hitRecordList.Count; index < count; ++index)
    {
      if (Object.op_Equality((Object) hitRecordList[index].target, (Object) hit_param.toObject))
      {
        hitRecord = hitRecordList[index];
        break;
      }
    }
    if (hitRecord == null)
    {
      hitRecord = new AttackHitChecker.HitRecord();
      hitRecord.target = hit_param.toObject;
      hitRecordList.Add(hitRecord);
    }
    hitRecord.lastHitTime = Time.time;
    ++hitRecord.hitCount;
  }

  public bool CheckHitAttack(AttackHitInfo info, Collider to_collider, StageObject to_object)
  {
    List<AttackHitChecker.HitRecord> hitRecordList = this.attackHitList.Get(info.name);
    if (hitRecordList != null)
    {
      int index = 0;
      for (int count = hitRecordList.Count; index < count; ++index)
      {
        AttackHitChecker.HitRecord hitRecord = hitRecordList[index];
        if (Object.op_Equality((Object) hitRecord.target, (Object) to_object) && (info.enableIdentityCheck || (double) Time.time - (double) hitRecord.lastHitTime <= (double) info.hitIntervalTime || info.hitCountMax > 0 && info.hitCountMax <= hitRecord.hitCount))
          return false;
      }
    }
    return true;
  }

  public void ClearAll() => this.attackHitList.Clear();

  public void ClearHitInfo(AttackHitInfo info) => this.ClearHitInfo(info.name);

  public void ClearHitInfo(string infoName) => this.attackHitList.Get(infoName)?.Clear();

  public void ClearTarget(AttackInfo info, StageObject target)
  {
    List<AttackHitChecker.HitRecord> hitRecordList = this.attackHitList.Get(info.name);
    if (hitRecordList == null)
      return;
    int index = 0;
    for (int count = hitRecordList.Count; index < count; ++index)
    {
      AttackHitChecker.HitRecord hitRecord = hitRecordList[index];
      if (Object.op_Equality((Object) hitRecord.target, (Object) target))
      {
        hitRecordList.Remove(hitRecord);
        break;
      }
    }
  }

  protected class HitRecord
  {
    public StageObject target;
    public float lastHitTime = -1f;
    public int hitCount;
  }
}
