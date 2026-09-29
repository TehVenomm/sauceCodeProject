// Decompiled with JetBrains decompiler
// Type: ChatState_PersonalMsgView
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class ChatState_PersonalMsgView : ChatState
{
  private static readonly string WINDOW_PREFAB_PATH = "InternalUI/UI_Chat/Chat_FriendMessage";
  private FriendMessageUIController m_friendMsg;
  private bool isInitializing;

  public FriendMessageUIController M_friendMsg => this.m_friendMsg;

  public override void Enter(MainChat _manager)
  {
    base.Enter(_manager);
    if (Object.op_Equality((Object) this.m_manager, (Object) null))
      return;
    this.m_manager.ExecCoroutine(this.InitializeCoroutine());
  }

  private IEnumerator InitializeCoroutine()
  {
    if (this.IsInitialized || this.isInitializing)
    {
      this.m_manager.PopState();
      this.EndInitialize();
    }
    else
    {
      this.isInitializing = true;
      this.m_friendMsg = ((Component) ResourceUtility.Realizes(Resources.Load(ChatState_PersonalMsgView.WINDOW_PREFAB_PATH), ((Component) this.m_manager).transform, 5)).GetComponent<FriendMessageUIController>();
      if (Object.op_Equality((Object) this.m_friendMsg, (Object) null))
      {
        this.m_manager.PopState();
        this.EndInitialize();
      }
      else
      {
        this.m_friendMsg.Initialize(this.m_manager);
        yield return (object) null;
        this.isInitializing = false;
        this.EndInitialize();
      }
    }
  }

  public override System.Type GetNextState()
  {
    return Object.op_Equality((Object) this.m_manager, (Object) null) || !this.IsInitialized ? base.GetNextState() : this.m_manager.GetTopState();
  }

  public override void Exit()
  {
    Object.Destroy((Object) ((Component) this.m_friendMsg).gameObject);
    this.m_friendMsg = (FriendMessageUIController) null;
  }
}
