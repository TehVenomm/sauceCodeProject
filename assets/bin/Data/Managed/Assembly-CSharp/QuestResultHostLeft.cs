// Decompiled with JetBrains decompiler
// Type: QuestResultHostLeft
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class QuestResultHostLeft : GameSection
{
  private void OnQuery_LEAVE_HUNT()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<ChatManager>.I.SwitchRoomChatConnectionToCoopConnection();
    MonoBehaviourSingleton<CoopApp>.I.LeaveWithParty((Action<bool>) (tf => GameSection.ResumeEvent(true)), true);
    if (!MonoBehaviourSingleton<UIManager>.IsValid() || !Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null))
      return;
    MonoBehaviourSingleton<UIManager>.I.mainChat.HideOpenButton();
    MonoBehaviourSingleton<UIManager>.I.mainChat.HideAll();
  }
}
