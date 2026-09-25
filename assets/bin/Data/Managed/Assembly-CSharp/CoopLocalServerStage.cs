// Decompiled with JetBrains decompiler
// Type: CoopLocalServerStage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class CoopLocalServerStage
{
  private int nowEnemyId;
  private List<CoopLocalServerEnemyPop> enemyPops;
  private SpanTimer popSpanTimer = new SpanTimer(1f);

  public CoopLocalServerSocket socket { get; private set; }

  public CoopLocalServerStage(CoopLocalServerSocket socket) => this.socket = socket;

  public void Init(
    uint map_id,
    List<CoopOfflineManager.EnemyPopParam> enemy_pop_params,
    int now_enemy_id)
  {
    this.nowEnemyId = now_enemy_id;
    this.InitEnemyPop(enemy_pop_params);
  }

  public void Update()
  {
    if (this.enemyPops == null || !this.popSpanTimer.IsReady())
      return;
    this.enemyPops.ForEach((Action<CoopLocalServerEnemyPop>) (epop => epop.Update()));
  }

  public int GenerateEnemyUniqId()
  {
    ++this.nowEnemyId;
    if (this.nowEnemyId > 999999)
      this.nowEnemyId = 500000;
    return this.nowEnemyId;
  }

  private void InitEnemyPop(
    List<CoopOfflineManager.EnemyPopParam> enemy_pop_params)
  {
    if (QuestManager.IsValidInGameWaveMatch() || QuestManager.IsValidInGameSeries() || QuestManager.IsValidInGameSeriesArena())
      return;
    this.enemyPops = new List<CoopLocalServerEnemyPop>();
    int num = 0;
    for (int count1 = enemy_pop_params.Count; num < count1; ++num)
    {
      FieldMapTable.EnemyPopTableData data = enemy_pop_params[num].data;
      if (data != null && enemy_pop_params[num].data.enemyPopType == ENEMY_POP_TYPE.NONE)
      {
        int count2 = enemy_pop_params[num].count;
        CoopLocalServerEnemyPop localServerEnemyPop = new CoopLocalServerEnemyPop();
        localServerEnemyPop.Init(this, num, data, count2);
        this.enemyPops.Add(localServerEnemyPop);
      }
    }
  }

  public void StartEnemyPop()
  {
    if (this.enemyPops == null)
      return;
    this.enemyPops.ForEach((Action<CoopLocalServerEnemyPop>) (epop => epop.Start()));
  }

  public void OutEnemy(int sid)
  {
    if (this.enemyPops == null)
      return;
    bool is_exterm = true;
    this.enemyPops.ForEach((Action<CoopLocalServerEnemyPop>) (epop =>
    {
      epop.Out(sid);
      if (epop.IsExtermination())
        return;
      is_exterm = false;
    }));
    if (!is_exterm)
      return;
    this.socket.SendEnemyExtermination();
  }
}
