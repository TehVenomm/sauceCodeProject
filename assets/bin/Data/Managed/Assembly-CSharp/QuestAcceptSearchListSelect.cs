// Decompiled with JetBrains decompiler
// Type: QuestAcceptSearchListSelect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;

#nullable disable
public class QuestAcceptSearchListSelect : QuestSearchListSelect
{
  private void OnCloseDialog_QuestAcceptSearchRoomCondition() => this.CloseSearchRoomCondition();

  public void OnQuery_INVITED_ROOM()
  {
    string inviteValue = MonoBehaviourSingleton<PartyManager>.I.InviteValue;
    if (string.IsNullOrEmpty(inviteValue))
      return;
    string[] strArray = inviteValue.Split('_');
    GameSection.SetEventData((object) new object[1]
    {
      (object) false
    });
    GameSection.StayEvent();
    MonoBehaviourSingleton<PartyManager>.I.SendApply(strArray[0], (Action<bool, Error>) ((is_success, ret_code) =>
    {
      if (is_success && !MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog(MonoBehaviourSingleton<PartyManager>.I.GetQuestId()))
        Protocol.Force((System.Action) (() => MonoBehaviourSingleton<PartyManager>.I.SendLeave((Action<bool>) (b => { }))));
      else
        GameSection.ResumeEvent(is_success);
    }));
  }

  public void OnQuery_JOIN_ROOM()
  {
    string eventData = (string) GameSection.GetEventData();
    if (string.IsNullOrEmpty(eventData))
      GameSection.StopEvent();
    GameSection.SetEventData((object) new object[1]
    {
      (object) false
    });
    GameSection.StayEvent();
    MonoBehaviourSingleton<PartyManager>.I.SendEntry(eventData, true, (Action<bool>) (is_success =>
    {
      if (is_success && !MonoBehaviourSingleton<GameSceneManager>.I.CheckQuestAndOpenUpdateAppDialog(MonoBehaviourSingleton<PartyManager>.I.GetQuestId()))
        Protocol.Force((System.Action) (() => MonoBehaviourSingleton<PartyManager>.I.SendLeave((Action<bool>) (b => { }))));
      else
        GameSection.ResumeEvent(is_success);
    }));
  }
}
