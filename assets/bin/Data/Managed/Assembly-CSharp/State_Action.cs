// Decompiled with JetBrains decompiler
// Type: State_Action
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class State_Action : State
{
  public override void Enter(StateMachine fsm, Brain brain)
  {
    EnemyActionController.ActionInfo nowAction = (brain as EnemyBrain).actionCtrl.nowAction;
    if (nowAction.data.isRotate)
      fsm.subFsm.ChangeState(STATE_TYPE.ROTATE);
    else if (nowAction.data.isMove)
      fsm.subFsm.ChangeState(STATE_TYPE.MOVE);
    else
      fsm.subFsm.ChangeState(STATE_TYPE.ATTACK);
  }

  public override void Process(StateMachine fsm, Brain brain)
  {
  }

  public override void Exit(StateMachine fsm, Brain brain)
  {
    fsm.subFsm.ChangeState(STATE_TYPE.NONE);
  }

  public override void HandleEvent(StateMachine fsm, Brain brain, BRAIN_EVENT ev, object param = null)
  {
    if (ev != BRAIN_EVENT.END_ENEMY_ACTION || fsm.subFsm.currentType != STATE_TYPE.ATTACK)
      return;
    fsm.ChangeState(STATE_TYPE.SEARCH);
  }
}
