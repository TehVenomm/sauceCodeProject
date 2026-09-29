// Decompiled with JetBrains decompiler
// Type: StateType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class StateType
{
  private static State[] s_states = new State[14]
  {
    null,
    (State) new State_Active(),
    (State) new State_NonActive(),
    (State) new State_Search(),
    (State) new State_Select(),
    (State) new State_Action(),
    (State) new State_Explore(),
    (State) new State_Rotate(),
    (State) new State_Move(),
    (State) new State_Stop(),
    (State) new State_Attack(),
    (State) new State_BattleStart(),
    (State) new State_KillTarget(),
    (State) new State_RaiseAlly()
  };

  public static State GetState(STATE_TYPE type) => StateType.s_states[(int) type];
}
