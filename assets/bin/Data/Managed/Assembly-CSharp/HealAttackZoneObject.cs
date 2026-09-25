// Decompiled with JetBrains decompiler
// Type: HealAttackZoneObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class HealAttackZoneObject : HealAttackObject
{
  private Dictionary<int, HealAttackZoneObject.TargetInfo> targetCollection = new Dictionary<int, HealAttackZoneObject.TargetInfo>();
  private float intervalTime;

  protected override bool isDuplicateAttackInfo => true;

  protected override string GetAttackInfoName() => "sk_heal_atk_zone";

  public void Setup(Player owner, Transform trans, BulletData bullet, SkillInfo.SkillParam skill)
  {
    this.Initialize((StageObject) owner, trans, (float) skill.supportValue[0], bullet.data.radius);
    this.intervalTime = bullet.dataZone.intervalTime;
    if (this.m_attackInfo is AttackHitInfo attackInfo)
      attackInfo.atkRate /= (float) Mathf.FloorToInt(skill.supportTime[0] / this.intervalTime);
    this.targetCollection.Clear();
  }

  protected override void Update() => this.m_timeCount += Time.deltaTime;

  protected override void OnTriggerEnter(Collider collider)
  {
    Enemy validEnemy = this._GetValidEnemy(collider);
    if (validEnemy == null)
      return;
    if (this.targetCollection.ContainsKey(validEnemy.id))
    {
      this.targetCollection[validEnemy.id].Enter(((Object) collider).name);
    }
    else
    {
      HealAttackZoneObject.TargetInfo targetInfo = new HealAttackZoneObject.TargetInfo();
      targetInfo.Enter(((Object) collider).name);
      this.targetCollection.Add(validEnemy.id, targetInfo);
    }
  }

  protected override void OnTriggerStay(Collider collider)
  {
    Enemy validEnemy = this._GetValidEnemy(collider);
    if (validEnemy == null)
      return;
    if (!this.targetCollection.ContainsKey(validEnemy.id))
      this.targetCollection.Add(validEnemy.id, new HealAttackZoneObject.TargetInfo());
    this.targetCollection[validEnemy.id].Stay(((Object) collider).name);
    if ((double) this.targetCollection[validEnemy.id].sec < (double) this.intervalTime)
      return;
    this.targetCollection[validEnemy.id].sec -= this.intervalTime;
    this.m_attackHitChecker.ClearTarget(this.m_attackInfo, (StageObject) validEnemy);
    this.m_colliderProcessor.OnTriggerEnter(collider);
  }

  protected override void OnTriggerExit(Collider collider)
  {
    Enemy validEnemy = this._GetValidEnemy(collider);
    if (validEnemy == null || !this.targetCollection.ContainsKey(validEnemy.id))
      return;
    this.targetCollection[validEnemy.id].Exit(((Object) collider).name);
  }

  private Enemy _GetValidEnemy(Collider collider)
  {
    Enemy componentInParent = ((Component) collider).gameObject.GetComponentInParent<Enemy>();
    if (componentInParent == null)
      return (Enemy) null;
    return (double) componentInParent.healDamageRate <= 0.0 ? (Enemy) null : componentInParent;
  }

  private class TargetInfo
  {
    private Dictionary<string, bool> collInfo = new Dictionary<string, bool>();
    public float sec;
    public float lastTime;

    public void Enter(string name)
    {
      if (this.collInfo.ContainsKey(name))
        this.collInfo[name] = true;
      else
        this.collInfo.Add(name, true);
    }

    public void Stay(string name)
    {
      if (this.collInfo.ContainsKey(name))
        this.collInfo[name] = true;
      else
        this.collInfo.Add(name, true);
      if ((double) this.lastTime == (double) Time.time)
        return;
      this.sec += Time.deltaTime;
      this.lastTime = Time.time;
    }

    public void Exit(string name)
    {
      if (this.collInfo.ContainsKey(name))
        this.collInfo[name] = false;
      if (this._CheckValid())
        return;
      this.sec = 0.0f;
      this.lastTime = 0.0f;
    }

    private bool _CheckValid()
    {
      foreach (bool flag in this.collInfo.Values)
      {
        if (flag)
          return true;
      }
      return false;
    }
  }
}
