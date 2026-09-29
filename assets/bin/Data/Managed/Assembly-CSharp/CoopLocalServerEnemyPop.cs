// Decompiled with JetBrains decompiler
// Type: CoopLocalServerEnemyPop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class CoopLocalServerEnemyPop
{
  private CoopLocalServerStage stage;
  private int count;
  private List<CoopLocalServerEnemy> enemys;
  private bool isStart;

  public int popIndex { get; private set; }

  public FieldMapTable.EnemyPopTableData data { get; private set; }

  public void Init(
    CoopLocalServerStage stage,
    int idx,
    FieldMapTable.EnemyPopTableData data,
    int count)
  {
    this.stage = stage;
    this.popIndex = idx;
    this.data = data;
    this.count = count;
    this.enemys = new List<CoopLocalServerEnemy>();
    this.isStart = false;
  }

  public bool IsTotalComplete() => this.data.popNumTotal > 0 && this.count >= this.data.popNumTotal;

  public bool IsExtermination() => this.IsTotalComplete() && this.GetPopNum() == 0;

  public int GetPopNum()
  {
    int num = 0;
    this.enemys.ForEach((Action<CoopLocalServerEnemy>) (e =>
    {
      if (!e.IsPop())
        return;
      ++num;
    }));
    return num;
  }

  public void Start()
  {
    if (!MonoBehaviourSingleton<StageObjectManager>.IsValid() || this.isStart)
      return;
    this.isStart = true;
    Action<StageObject> action = (Action<StageObject>) (o =>
    {
      Enemy enemy = o as Enemy;
      if (Object.op_Equality((Object) enemy, (Object) null) || enemy.enemyPopIndex != this.popIndex)
        return;
      CoopLocalServerEnemy localServerEnemy = new CoopLocalServerEnemy(this, 0.0f);
      localServerEnemy.Pop(enemy.id);
      this.enemys.Add(localServerEnemy);
    });
    MonoBehaviourSingleton<StageObjectManager>.I.enemyList.ForEach(action);
    MonoBehaviourSingleton<StageObjectManager>.I.cacheList.ForEach(action);
    for (int count = this.enemys.Count; count < this.data.popNumMax; ++count)
    {
      float pop_time = this.data.GeneratePopTime();
      if (count < this.data.popNumInit)
        pop_time = 0.0f;
      else if (count < this.data.popNumMin)
        pop_time = 0.0f;
      this.enemys.Add(new CoopLocalServerEnemy(this, pop_time));
    }
    this.Update();
  }

  public void Update()
  {
    if (this.IsTotalComplete())
      return;
    this.enemys.ForEach((Action<CoopLocalServerEnemy>) (enemy =>
    {
      if (enemy.IsPop() || this.IsTotalComplete() || !enemy.IsReady())
        return;
      this.Pop(enemy);
    }));
  }

  public void Pop(CoopLocalServerEnemy enemy)
  {
    int enemyUniqId = this.stage.GenerateEnemyUniqId();
    ++this.count;
    enemy.Pop(enemyUniqId);
    this.stage.socket.SendEnemyPop(enemy);
  }

  public void Out(int sid)
  {
    CoopLocalServerEnemy localServerEnemy = this.enemys.Find((Predicate<CoopLocalServerEnemy>) (e => e.sid == sid));
    if (localServerEnemy == null)
      return;
    float num = this.data.GeneratePopTime();
    if (this.GetPopNum() < this.data.popNumMin)
      num = 0.0f;
    localServerEnemy.Out(Time.time + num);
  }
}
