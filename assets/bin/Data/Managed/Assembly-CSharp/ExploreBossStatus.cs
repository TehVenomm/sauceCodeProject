// Decompiled with JetBrains decompiler
// Type: ExploreBossStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class ExploreBossStatus
{
  public EnemyRegionWork[] regionWorks;
  public uint[] execAngryIds;

  public XorInt coopEnemyId { get; private set; }

  public XorInt hpMax { get; private set; }

  public XorInt hp { get; private set; }

  public XorInt barrierHp { get; private set; }

  public XorInt downCount { get; private set; }

  public float concussionTotal { get; private set; }

  public float concussionMax { get; private set; }

  public float concussionExtend { get; private set; }

  public bool isDead { get; private set; }

  public XorInt shieldHp { get; private set; }

  public uint nowAngryId { get; private set; }

  public bool isMadMode { get; private set; }

  public int deadReviveCount { get; private set; }

  public int recoveredHP { get; private set; }

  public void UpdateStatus(Enemy enemy)
  {
    this.coopEnemyId = (XorInt) enemy.id;
    this.hpMax = (XorInt) enemy.hpMax;
    this.hp = (XorInt) enemy.hp;
    this.barrierHp = enemy.BarrierHp;
    this.downCount = (XorInt) enemy.downCount;
    this.concussionTotal = enemy.concussionTotal;
    this.concussionMax = enemy.concussionMax;
    this.concussionExtend = enemy.concussionExtend;
    this.CopyRegionWorks(enemy.regionWorks);
    this.isDead = enemy.isDead;
    this.shieldHp = enemy.ShieldHp;
    this.nowAngryId = enemy.NowAngryID;
    if (enemy.ExecAngryIDList.Count > 0)
      this.execAngryIds = enemy.ExecAngryIDList.ToArray();
    this.isMadMode = enemy.IsValidBuff(BuffParam.BUFFTYPE.MAD_MODE);
    this.deadReviveCount = enemy.deadReviveCount;
    if (!MonoBehaviourSingleton<InGameRecorder>.IsValid())
      return;
    this.recoveredHP = MonoBehaviourSingleton<InGameRecorder>.I.GetEnemyRecoveredHpById(enemy.id);
  }

  public void UpdateStatus(Coop_Model_RoomSyncExploreBoss boss)
  {
    this.coopEnemyId = (XorInt) boss.ceId;
    this.hpMax = (XorInt) boss.hpm;
    this.hp = (XorInt) boss.hp;
    this.barrierHp = (XorInt) boss.bhp;
    this.downCount = (XorInt) boss.downCount;
    this.concussionTotal = boss.concussionTotal;
    this.concussionMax = boss.concussionMax;
    this.concussionExtend = boss.concussionExtend;
    this.shieldHp = (XorInt) boss.shp;
    this.regionWorks = new EnemyRegionWork[boss.rs.Length];
    int index = 0;
    for (int length = boss.rs.Length; index < length; ++index)
    {
      EnemyRegionWork region = new EnemyRegionWork();
      boss.rs[index].ApplyTo(region);
      this.regionWorks[index] = region;
    }
    this.nowAngryId = boss.angid;
    this.execAngryIds = boss.eangids;
    this.isMadMode = boss.isMM;
    this.deadReviveCount = boss.deadReviveCount;
    this.recoveredHP = boss.recoveredHP;
  }

  public void UpdateStatus(Coop_Model_RoomExploreBossDead model)
  {
    this.hp = (XorInt) 0;
    this.isDead = true;
    this.downCount = (XorInt) model.downCount;
    this.concussionTotal = model.concussionTotal;
    this.concussionMax = model.concussionMax;
    this.concussionExtend = model.concussionExtend;
    if (this.regionWorks == null)
    {
      int num = 0;
      foreach (int breakId in model.breakIds)
      {
        if (num < breakId)
          num = breakId;
      }
      this.regionWorks = new EnemyRegionWork[num + 1];
      for (int index = 0; index <= num; ++index)
      {
        EnemyRegionWork enemyRegionWork = new EnemyRegionWork();
        this.regionWorks[index] = enemyRegionWork;
      }
    }
    if (model.breakIds != null)
    {
      foreach (int breakId in model.breakIds)
      {
        if (this.regionWorks.Length > breakId)
        {
          EnemyRegionWork regionWork = this.regionWorks[breakId];
          if (regionWork != null)
          {
            regionWork.hp = (XorInt) 0;
            regionWork.isBroke = true;
          }
        }
      }
    }
    if ((int) this.hpMax > 0)
      return;
    foreach (Coop_Model_RoomExploreBossDead.TotalDamage dmg in model.dmgs)
      this.hpMax = (XorInt) ((int) this.hpMax + dmg.dmg);
  }

  private void CopyRegionWorks(EnemyRegionWork[] regionWorks)
  {
    this.regionWorks = new EnemyRegionWork[regionWorks.Length];
    for (int index = 0; index < regionWorks.Length; ++index)
    {
      EnemyRegionWork enemyRegionWork = new EnemyRegionWork();
      enemyRegionWork.CopyFrom(regionWorks[index]);
      this.regionWorks[index] = enemyRegionWork;
    }
  }

  public List<int> GetBreakIds()
  {
    List<int> breakIds = new List<int>();
    breakIds.Add(0);
    if (this.regionWorks == null)
      return breakIds;
    for (int index = 1; index < this.regionWorks.Length; ++index)
    {
      if (this.regionWorks[index].isBroke)
        breakIds.Add(index);
    }
    return breakIds;
  }
}
