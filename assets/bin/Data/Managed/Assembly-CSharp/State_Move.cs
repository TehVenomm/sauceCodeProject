// Decompiled with JetBrains decompiler
// Type: State_Move
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class State_Move : State
{
  public override void Enter(StateMachine fsm, Brain brain) => brain.moveCtrl.SeekOn();

  public override void Process(StateMachine fsm, Brain brain)
  {
  }

  public override void Exit(StateMachine fsm, Brain brain)
  {
    brain.moveCtrl.SeekOff();
    brain.moveCtrl.ResetStopRange();
  }

  public override void HandleEvent(StateMachine fsm, Brain brain, BRAIN_EVENT ev, object param = null)
  {
    if (ev != BRAIN_EVENT.END_ENEMY_ACTION)
      return;
    fsm.ChangeState(STATE_TYPE.ATTACK);
  }
}
