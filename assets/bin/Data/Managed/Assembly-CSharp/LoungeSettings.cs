// Decompiled with JetBrains decompiler
// Type: LoungeSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class LoungeSettings : GameSection
{
  public override void Initialize()
  {
    this.SetLabelText((Enum) LoungeSettings.UI.LBL_ROOM_ID, MonoBehaviourSingleton<LoungeMatchingManager>.I.GetLoungeNumber());
    bool is_visible = MonoBehaviourSingleton<LoungeMatchingManager>.I.GetOwnerUserId() == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id;
    this.SetActive((Enum) LoungeSettings.UI.BTN_SETTING, is_visible);
    this.SetActive((Enum) LoungeSettings.UI.SPR_SETTING_GRAY, !is_visible);
    base.Initialize();
  }

  private void OnQuery_MEMBER()
  {
    if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData == null)
    {
      GameSection.ChangeEvent("ERROR");
    }
    else
    {
      GameSection.StayEvent();
      MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInfo((Action<bool>) (isSuccess => GameSection.ResumeEvent(isSuccess)));
    }
  }

  private void OnQuery_EXIT()
  {
    if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData == null)
      return;
    GameSection.StayEvent();
    MonoBehaviourSingleton<LoungeMatchingManager>.I.SendLeave((Action<bool>) (isSuccess => GameSection.ResumeEvent(isSuccess)));
  }

  private void OnQuery_INVITE()
  {
    if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData == null)
    {
      GameSection.ChangeEvent("ERROR");
    }
    else
    {
      if (MonoBehaviourSingleton<LoungeMatchingManager>.I.GetMemberCount() < MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData.num + 1)
        return;
      GameSection.ChangeEvent("MAX_MEMBER");
    }
  }

  private void OnQuery_SETTING()
  {
    if (MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData != null)
      return;
    GameSection.ChangeEvent("ERROR");
  }

  private void OnQuery_HOW_TO() => GameSection.SetEventData((object) WebViewManager.lounge);

  private enum UI
  {
    BTN_SETTING,
    SPR_SETTING_GRAY,
    LBL_ROOM_ID,
  }
}
