// Decompiled with JetBrains decompiler
// Type: ChatStateMachine`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class ChatStateMachine<T> where T : ChatState
{
  private MainChat m_Manager;
  private T m_CurrentState;
  private ChatStateMachine<T>.OnChangeStateType m_OnChangeStateType = (ChatStateMachine<T>.OnChangeStateType) ((_param1, _param2) => { });
  private System.Type m_PrevStateType;

  public System.Type CurrentStateType
  {
    get => (object) this.m_CurrentState == null ? (System.Type) null : this.m_CurrentState.GetType();
  }

  public System.Type PrevStateType => this.m_PrevStateType;

  public T CurrentState => this.m_CurrentState;

  public void Initialize(MainChat manager) => this.m_Manager = manager;

  public void AddListener(
    ChatStateMachine<T>.OnChangeStateType onChangeStateType = null)
  {
    this.m_OnChangeStateType += onChangeStateType;
  }

  public void RemoveListener(
    ChatStateMachine<T>.OnChangeStateType onChangeStateType = null)
  {
    this.m_OnChangeStateType -= onChangeStateType;
  }

  public void Start(System.Type stateType)
  {
    this.m_CurrentState = this.CreateState(stateType);
    this.m_CurrentState.Enter(this.m_Manager);
  }

  public bool IsRun() => (object) this.m_CurrentState != null;

  public void Update(float deltaTime)
  {
    if (!this.IsRun())
      return;
    this.m_CurrentState.Update(deltaTime);
    System.Type nextState = this.m_CurrentState.GetNextState();
    if (!(this.m_CurrentState.GetType() != nextState))
      return;
    this.m_PrevStateType = this.m_CurrentState.GetType();
    this.m_CurrentState.Exit();
    if (nextState == (System.Type) null)
    {
      this.m_CurrentState = default (T);
    }
    else
    {
      this.m_CurrentState = this.CreateState(nextState);
      this.m_CurrentState.Enter(this.m_Manager);
      if (this.m_OnChangeStateType == null)
        return;
      this.m_OnChangeStateType(nextState, this.m_PrevStateType);
    }
  }

  private T CreateState(System.Type type) => Activator.CreateInstance(type) as T;

  public delegate void OnChangeStateType(System.Type currentType, System.Type prevType) where T : ChatState;
}
