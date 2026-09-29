// Decompiled with JetBrains decompiler
// Type: ChatState_HomeTab
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ChatState_HomeTab : ChatState
{
  public override void Enter(MainChat _manager)
  {
    base.Enter(_manager);
    this.EndInitialize();
  }

  public override System.Type GetNextState()
  {
    return Object.op_Equality((Object) this.m_manager, (Object) null) || !this.IsInitialized ? base.GetNextState() : this.m_manager.GetTopState();
  }
}
