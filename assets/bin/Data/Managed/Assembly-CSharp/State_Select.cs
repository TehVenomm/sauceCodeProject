// Decompiled with JetBrains decompiler
// Type: State_Select
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class State_Select : State
{
  public override void Enter(StateMachine fsm, Brain brain)
  {
  }

  public override void Process(StateMachine fsm, Brain brain)
  {
    EnemyBrain enemyBrain = brain as EnemyBrain;
    if (Object.op_Equality((Object) enemyBrain, (Object) null) || enemyBrain.opponentMem == null)
      return;
    enemyBrain.opponentMem.Update();
    enemyBrain.actionCtrl.SelectAction();
    if (enemyBrain.actionCtrl.canUseCount <= 0)
      fsm.processSpan.SetTempSpan(1f);
    else if (enemyBrain.actionCtrl.totalWeight <= 0)
      fsm.ChangeState(STATE_TYPE.SEARCH);
    else
      fsm.ChangeState(STATE_TYPE.ACTION);
  }

  public override void Exit(StateMachine fsm, Brain brain)
  {
  }
}
