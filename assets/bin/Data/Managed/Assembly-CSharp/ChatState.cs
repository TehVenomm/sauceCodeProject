// Decompiled with JetBrains decompiler
// Type: ChatState
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public abstract class ChatState
{
  protected MainChat m_manager;
  private bool m_isInitialized;

  protected bool IsInitialized => this.m_isInitialized;

  protected void BeginInitialize() => this.m_isInitialized = false;

  protected void EndInitialize() => this.m_isInitialized = true;

  public virtual void Enter(MainChat _manager) => this.m_manager = _manager;

  public virtual void Update(float _deltaTime)
  {
  }

  public virtual System.Type GetNextState() => this.GetType();

  public virtual void Exit()
  {
  }

  public virtual void OnDragAtTop(string chatItemId, float dragpower)
  {
  }

  public virtual void OnDragAtBottom(string chatItemId, float dragpower)
  {
  }

  public virtual void OnShowMessageOnDisplay(string chatItemId)
  {
  }

  public virtual void OnTapHeaderTab(MainChat.CHAT_TYPE chatType)
  {
  }
}
