// Decompiled with JetBrains decompiler
// Type: State_Explore
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class State_Explore : State
{
  public override void Enter(StateMachine fsm, Brain brain)
  {
    brain.moveCtrl.SetRootPosition(brain.owner.appearPos);
    this.Explore(fsm, brain);
  }

  public override void Process(StateMachine fsm, Brain brain)
  {
    if (fsm.subFsm.currentType == STATE_TYPE.NONE)
      this.Explore(fsm, brain);
    if (fsm.subFsm.currentType != STATE_TYPE.STOP || !brain.moveCtrl.isStopTimeOver)
      return;
    this.Explore(fsm, brain);
  }

  public override void Exit(StateMachine fsm, Brain brain)
  {
    fsm.subFsm.ChangeState(STATE_TYPE.NONE);
  }

  public override void HandleEvent(StateMachine fsm, Brain brain, BRAIN_EVENT ev, object param = null)
  {
    if (ev != BRAIN_EVENT.END_ENEMY_ACTION)
      return;
    fsm.subFsm.ChangeState(STATE_TYPE.NONE);
  }

  private void Explore(StateMachine fsm, Brain brain)
  {
    float num1 = brain.param.moveParam.moveMaxLength * 0.5f;
    int num2 = Utility.Random(100);
    if (num2 < 15)
    {
      Vector3 rootPosition = brain.moveCtrl.rootPosition;
      rootPosition.x += Utility.SymmetryRandom(num1);
      rootPosition.z += Utility.SymmetryRandom(num1);
      brain.moveCtrl.SetTargetPos(rootPosition);
      fsm.subFsm.ChangeState(STATE_TYPE.ROTATE);
    }
    else if (num2 >= 15 && num2 < 45)
    {
      Vector3 rootPosition = brain.moveCtrl.rootPosition;
      rootPosition.x += Utility.SymmetryRandom(num1);
      rootPosition.z += Utility.SymmetryRandom(num1);
      brain.moveCtrl.SetTargetPos(rootPosition);
      brain.moveCtrl.ChangeStopRange(1f);
      fsm.subFsm.ChangeState(STATE_TYPE.MOVE);
    }
    else
    {
      brain.moveCtrl.SetStopTime(Utility.Random(3f));
      fsm.subFsm.ChangeState(STATE_TYPE.STOP);
    }
  }
}
