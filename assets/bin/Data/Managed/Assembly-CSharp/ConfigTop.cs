// Decompiled with JetBrains decompiler
// Type: ConfigTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class ConfigTop : GameSection
{
  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_USER_INFO;
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    bool is_enabled1 = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isStopperSet != 0;
    bool is_enabled2 = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isParentPassSet != 0;
    this.SetToggle((Enum) ConfigTop.UI.TGL_STOPPER_ON, is_enabled1);
    this.SetToggle((Enum) ConfigTop.UI.TGL_STOPPER_OFF, !is_enabled1);
    this.SetToggle((Enum) ConfigTop.UI.TGL_PP_ON, is_enabled2);
    this.SetToggle((Enum) ConfigTop.UI.TGL_PP_OFF, !is_enabled2);
    this.SetButtonEnabled((Enum) ConfigTop.UI.BTN_STOPPER_ON, !is_enabled1);
    this.SetButtonEnabled((Enum) ConfigTop.UI.BTN_STOPPER_OFF, is_enabled1);
    this.SetButtonEnabled((Enum) ConfigTop.UI.BTN_PP_ON, !is_enabled2);
    this.SetButtonEnabled((Enum) ConfigTop.UI.BTN_PP_OFF, is_enabled2);
  }

  private void OnQuery_STOPPER_ON() => this.SendStopper(true);

  private void OnQuery_STOPPER_OFF() => this.SendStopper(false);

  private void SendStopper(bool enable)
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<UserInfoManager>.I.SendStopper(enable, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  private void OnQuery_NAME()
  {
    DateTime now = TimeManager.GetNow();
    DateTime result;
    if (DateTime.TryParse(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.editNameAt.date, out result))
    {
      if (result.CompareTo(now) <= 0)
        return;
      GameSection.ChangeEvent("NOT_CHANGE_NAME", (object) new string[1]
      {
        MonoBehaviourSingleton<UserInfoManager>.I.userInfo.editNameAt.date
      });
    }
    else
      GameSection.StopEvent();
  }

  private void OnQuery_ConfigClearCacheConfirm_YES()
  {
    MenuReset.needClearCache = true;
    MenuReset.needPredownload = true;
  }

  private enum UI
  {
    BTN_STOPPER_ON,
    BTN_STOPPER_OFF,
    BTN_PP_ON,
    BTN_PP_OFF,
    SPR_STOPPER_ON,
    SPR_STOPPER_OFF,
    SPR_PP_ON,
    SPR_PP_OFF,
    TGL_STOPPER_ON,
    TGL_STOPPER_OFF,
    TGL_PP_ON,
    TGL_PP_OFF,
  }
}
