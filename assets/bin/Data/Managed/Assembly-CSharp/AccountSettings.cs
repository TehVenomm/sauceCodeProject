// Decompiled with JetBrains decompiler
// Type: AccountSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class AccountSettings : GameSection
{
  public override void Initialize() => base.Initialize();

  public override void UpdateUI()
  {
    if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isAdvancedUser | MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isAdvancedUserGoogle)
    {
      this.SetActive((Enum) AccountSettings.UI.BTN_MAIL, false);
      this.SetActive((Enum) AccountSettings.UI.BTN_LINK_ACCOUNT, false);
      this.SetActive((Enum) AccountSettings.UI.LBL_REGISTED_TEXT, true);
      this.SetLabelText((Enum) AccountSettings.UI.LBL_REGISTED, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.advancedUserMail);
      this.SetLabelText((Enum) AccountSettings.UI.LBL_REGISTED_TEXT, this.sectionData.GetText(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isAdvancedUser ? "REGISTED_MAIL" : "REGISTED_GOOGLE"));
      this.SetActive((Enum) AccountSettings.UI.BTN_MAIL_CHANGE_PASSWORD, true);
    }
    else
    {
      this.SetActive((Enum) AccountSettings.UI.BTN_MAIL, true);
      this.SetActive((Enum) AccountSettings.UI.BTN_LINK_ACCOUNT, true);
      this.SetActive((Enum) AccountSettings.UI.LBL_REGISTED_TEXT, false);
      this.GetComponent<UIGrid>((Enum) AccountSettings.UI.GRD_BTN).Reposition();
    }
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    base.OnNotify(flags);
    if ((GameSection.NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY & flags) == (GameSection.NOTIFY_FLAG) 0)
      return;
    this.RefreshUI();
  }

  private enum UI
  {
    BTN_MAIL,
    BTN_LINK_ACCOUNT,
    LBL_REGISTED_TEXT,
    STR_REGISTED,
    LBL_REGISTED,
    GRD_BTN,
    BTN_MAIL_CHANGE_PASSWORD,
  }
}
