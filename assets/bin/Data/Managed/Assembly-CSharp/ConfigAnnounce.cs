// Decompiled with JetBrains decompiler
// Type: ConfigAnnounce
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class ConfigAnnounce : GameSection
{
  private int pushEnable;
  private bool autoClose;

  public override string overrideBackKeyEvent => "CLOSE";

  private void Update()
  {
    if (!this.autoClose || MonoBehaviourSingleton<GameSceneManager>.I.isWaiting)
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(nameof (ConfigAnnounce), ((Component) this).gameObject, "CLOSE");
    this.autoClose = false;
  }

  public override void UpdateUI()
  {
    this.updateNoticeFlag();
    this.UpdateUI_Event();
    this.UpdateUI_Friend();
    this.UpdateUI_GuildRequest();
    base.UpdateUI();
  }

  private void UpdateUI_Event()
  {
    bool is_enabled = (this.pushEnable & 1) != 0;
    this.SetToggle((Enum) ConfigAnnounce.UI.TGL_EVENT_ON, is_enabled);
    this.SetToggle((Enum) ConfigAnnounce.UI.TGL_EVENT_OFF, !is_enabled);
    this.SetButtonEnabled((Enum) ConfigAnnounce.UI.BTN_EVENT_ON, !is_enabled);
    this.SetButtonEnabled((Enum) ConfigAnnounce.UI.BTN_EVENT_OFF, is_enabled);
  }

  private void UpdateUI_Friend()
  {
    bool is_enabled = (this.pushEnable & 2) != 0;
    this.SetToggle((Enum) ConfigAnnounce.UI.TGL_FRIEND_ON, is_enabled);
    this.SetToggle((Enum) ConfigAnnounce.UI.TGL_FRIEND_OFF, !is_enabled);
    this.SetButtonEnabled((Enum) ConfigAnnounce.UI.BTN_FRIEND_ON, !is_enabled);
    this.SetButtonEnabled((Enum) ConfigAnnounce.UI.BTN_FRIEND_OFF, is_enabled);
  }

  private void UpdateUI_GuildRequest()
  {
    bool localPushFlag = MonoBehaviourSingleton<GuildRequestManager>.I.GetLocalPushFlag();
    this.SetToggle((Enum) ConfigAnnounce.UI.TGL_GUILD_REQUEST_ON, localPushFlag);
    this.SetToggle((Enum) ConfigAnnounce.UI.TGL_GUILD_REQUEST_OFF, !localPushFlag);
    this.SetButtonEnabled((Enum) ConfigAnnounce.UI.BTN_GUILD_REQUEST_ON, !localPushFlag);
    this.SetButtonEnabled((Enum) ConfigAnnounce.UI.BTN_GUILD_REQUEST_OFF, localPushFlag);
  }

  private void updateNoticeFlag()
  {
    this.pushEnable = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.pushEnable;
  }

  private void OnQuery_EVENT()
  {
    this.pushEnable ^= 1;
    this.UpdateUI_Event();
  }

  private void OnQuery_FRIEND()
  {
    this.pushEnable ^= 2;
    this.UpdateUI_Friend();
  }

  private void OnQuery_GUILD_REQUEST()
  {
    MonoBehaviourSingleton<GuildRequestManager>.I.ToggleLocalPushFlag();
    this.UpdateUI_GuildRequest();
  }

  private void OnQuery_CLOSE()
  {
    if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.pushEnable == this.pushEnable)
      return;
    GameSection.StayEvent();
    MonoBehaviourSingleton<UserInfoManager>.I.SendPushNotificationDeviceEnable(this.pushEnable, (Action<bool>) (is_success =>
    {
      GameSection.ResumeEvent(is_success);
      this.pushEnable = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.pushEnable;
      this.autoClose = true;
    }));
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_USER_INFO;
  }

  private enum UI
  {
    TGL_EVENT_ON,
    TGL_EVENT_OFF,
    TGL_FRIEND_ON,
    TGL_FRIEND_OFF,
    TGL_GUILD_REQUEST_ON,
    TGL_GUILD_REQUEST_OFF,
    BTN_EVENT_ON,
    BTN_EVENT_OFF,
    BTN_FRIEND_ON,
    BTN_FRIEND_OFF,
    BTN_GUILD_REQUEST_ON,
    BTN_GUILD_REQUEST_OFF,
  }
}
