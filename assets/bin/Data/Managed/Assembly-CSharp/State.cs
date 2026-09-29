// Decompiled with JetBrains decompiler
// Type: State
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public abstract class State
{
  public abstract void Enter(StateMachine fsm, Brain brain);

  public abstract void Process(StateMachine fsm, Brain brain);

  public abstract void Exit(StateMachine fsm, Brain brain);

  public virtual void HandleEvent(StateMachine fsm, Brain brain, BRAIN_EVENT ev, object param = null)
  {
  }
}
