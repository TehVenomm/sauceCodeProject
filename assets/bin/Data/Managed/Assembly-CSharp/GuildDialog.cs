// Decompiled with JetBrains decompiler
// Type: GuildDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;

#nullable disable
public class GuildDialog : GameSection
{
  public void OnQuery_INVITED_LOUNGE()
  {
    string inviteValue = MonoBehaviourSingleton<LoungeMatchingManager>.I.InviteValue;
    if (string.IsNullOrEmpty(inviteValue))
      return;
    string[] strArray = inviteValue.Split('_');
    GameSection.StayEvent();
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SendApply(strArray[0], (Action<bool, Error>) ((is_success, ret_code) => GameSection.ResumeEvent(is_success)));
  }

  public void OnQuery_FRIEND_INVITED_LOUNGE()
  {
    string eventData = (string) GameSection.GetEventData();
    GameSection.StayEvent();
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SendEntry(eventData, (Action<bool>) (isSuccess => GameSection.ResumeEvent(isSuccess)));
  }

  private void OnQuery_HOW_TO() => GameSection.SetEventData((object) WebViewManager.lounge);

  private void OnQuery_CREATE()
  {
    if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal >= 15)
      return;
    GameSection.ChangeEvent("DONT_HAVE_GEM");
  }

  private void OnQuery_HINT() => GameSection.SetEventData((object) WebViewManager.GuildHint);
}
