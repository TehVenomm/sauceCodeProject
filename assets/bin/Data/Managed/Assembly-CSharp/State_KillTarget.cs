// Decompiled with JetBrains decompiler
// Type: State_KillTarget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class State_KillTarget : State
{
  public override void Enter(StateMachine fsm, Brain brain)
  {
    if (brain.think == null)
      return;
    brain.think.KillTargetOn();
  }

  public override void Process(StateMachine fsm, Brain brain)
  {
  }

  public override void Exit(StateMachine fsm, Brain brain)
  {
    if (brain.think == null)
      return;
    brain.think.KillTargetOff();
  }
}
