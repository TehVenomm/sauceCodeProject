// Decompiled with JetBrains decompiler
// Type: ChatState_Init
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ChatState_Init : ChatState
{
  public override void Enter(MainChat _manager)
  {
    base.Enter(_manager);
    this.EndInitialize();
    if (!Object.op_Inequality((Object) this.m_manager, (Object) null))
      return;
    this.m_manager.PushNextState(typeof (ChatState_HomeTab));
  }

  public override System.Type GetNextState()
  {
    return !this.IsInitialized || Object.op_Equality((Object) this.m_manager, (Object) null) ? base.GetNextState() : this.m_manager.GetTopState();
  }
}
