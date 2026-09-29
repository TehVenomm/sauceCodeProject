// Decompiled with JetBrains decompiler
// Type: MetaAI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class MetaAI
{
  private const int kPlayerNum = 8;
  private Player[] needRescuePlayer = new Player[8];

  public void Update() => this.WatchingPlayer();

  private void WatchingPlayer()
  {
    for (int index = 0; index < 8; ++index)
      this.needRescuePlayer[index] = (Player) null;
    int num1 = 0;
    int num2 = 7;
    bool flag1 = false;
    int index1 = 0;
    for (int count = MonoBehaviourSingleton<StageObjectManager>.I.playerList.Count; index1 < count; ++index1)
    {
      if (MonoBehaviourSingleton<StageObjectManager>.I.playerList[index1] is Player player)
      {
        bool flag2 = false;
        if (!player.IsPrayed())
        {
          if (player.isDead && !player.isWaitingResurrectionHoming && (double) player.rescueTime > 0.0)
            flag2 = true;
          if (player.IsStone() && (double) player.stoneRescueTime > 0.0)
            flag2 = true;
        }
        if (flag2)
        {
          if (player.isNpc)
            this.needRescuePlayer[num2--] = player;
          else
            this.needRescuePlayer[num1++] = player;
          flag1 = true;
        }
      }
    }
    if (!flag1)
      return;
    for (int index2 = 0; index2 < 8; ++index2)
    {
      if (this.needRescuePlayer[index2] != null)
        this.OnRescuePlayer(this.needRescuePlayer[index2]);
    }
  }

  private void OnRescuePlayer(Player dead_player)
  {
    NonPlayer nearestAliveNpc = AIUtility.GetNearestAliveNpc((StageObject) dead_player);
    if (Object.op_Equality((Object) nearestAliveNpc, (Object) null) || Object.op_Equality((Object) nearestAliveNpc.controller, (Object) null))
      return;
    Brain brain = nearestAliveNpc.controller.brain;
    if (Object.op_Equality((Object) brain, (Object) null) || brain.think == null)
      return;
    brain.targetCtrl.SetAllyTarget((StageObject) dead_player);
    if (brain.fsm == null)
      return;
    brain.fsm.ChangeState(STATE_TYPE.RAISE_ALLY);
  }
}
