// Decompiled with JetBrains decompiler
// Type: MenuTop
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class MenuTop : GameSection
{
  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_PRESENT_NUM | GameSection.NOTIFY_FLAG.UPDATE_USER_INFO | GameSection.NOTIFY_FLAG.UPDATE_USER_STATUS;
  }

  public override void UpdateUI()
  {
    this.SetBadge((Enum) MenuTop.UI.INFO, GameSaveData.instance.IsShowNewsNotification() ? 1 : 0, (SpriteAlignment) 3, -4, -4);
    this.SetBadge((Enum) MenuTop.UI.PRESENT_BTN, MonoBehaviourSingleton<PresentManager>.I.presentNum, (SpriteAlignment) 3, -4, -4);
    this.SetBadge((Enum) MenuTop.UI.FRIEND, MonoBehaviourSingleton<FriendManager>.I.noReadMessageNum, (SpriteAlignment) 3, -4, -4);
    this.SetBadge((Enum) MenuTop.UI.SUPPORT, MonoBehaviourSingleton<HelpshiftManager>.I.countMess, (SpriteAlignment) 3, -4, -4);
  }

  public void OnQuery_FRIEND()
  {
    if (!(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "FriendScene"))
      return;
    GameSection.ChangeEvent("[BACK]");
  }

  public void OnQuery_PROFILE()
  {
    if (!(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "ProfileScene"))
      return;
    GameSection.ChangeEvent("[BACK]");
  }

  private void OnQuery_PRESENT()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<PresentManager>.I.SendGetPresent(0, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  private void OnQuery_MUTUAL_FOLLOW()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<FriendManager>.I.SendGetFollowLink((Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
  }

  private void OnQuery_GOWRAP_INFO()
  {
    GameSaveData instance = GameSaveData.instance;
    DateTime dateTime = DateTime.UtcNow;
    dateTime = dateTime.AddSeconds(-10800.0);
    int day = dateTime.Day;
    instance.dayShowNewsNotification = day;
    this.UpdateUI();
    MonoBehaviourSingleton<GoWrapManager>.I.ShowMenu();
  }

  private void OnQuery_MenuTitleConfirm_YES()
  {
    ServerAccountSaveData.instance.RemoveAccount(NetworkManager.APP_HOST);
    AccountManager.ResetAccount();
    MonoBehaviourSingleton<HelpshiftManager>.I.Logout();
    MonoBehaviourSingleton<UIManager>.I.loading.ShowTutorialBg(true);
    MonoBehaviourSingleton<AppMain>.I.Reset(true, false);
    this.RefreshUI();
  }

  private void OnQuery_SUPPORT()
  {
    if (!MonoBehaviourSingleton<HelpshiftManager>.IsValid())
      return;
    MonoBehaviourSingleton<HelpshiftManager>.I.Show(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.code, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id.ToString(), MonoBehaviourSingleton<UserInfoManager>.I.Vip_Status == 1, GameSaveData.instance.currentServer.name, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.advancedUserMail);
  }

  protected override void OnOpen()
  {
    base.OnOpen();
    if (!MonoBehaviourSingleton<HelpshiftManager>.IsValid())
      return;
    MonoBehaviourSingleton<HelpshiftManager>.I.RegisterDelegate(new System.Action(this.UpdateHelpshiftNumber));
  }

  protected override void OnDestroy()
  {
    if (MonoBehaviourSingleton<HelpshiftManager>.IsValid())
      MonoBehaviourSingleton<HelpshiftManager>.I.UnRegisterDelegate(new System.Action(this.UpdateHelpshiftNumber));
    base.OnDestroy();
  }

  private void OnQuery_CHANGE_SERVER()
  {
    if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isAdvancedUser | MonoBehaviourSingleton<UserInfoManager>.I.userInfo.isAdvancedUserGoogle)
      MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Menu", "ConfigServer");
    else
      MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("Menu", "ConfigConfirmSwitchServer");
  }

  private void UpdateHelpshiftNumber()
  {
    this.UpdateUI();
    MonoBehaviourSingleton<UIManager>.I.mainStatus.UpdateUI();
  }

  private enum UI
  {
    PRESENT_BTN,
    PRESENT_LBL,
    INFO,
    TITLE,
    FRIEND,
    HELP,
    CONFIG,
    STORAGE,
    SUPPORT,
    SPR_BADGE,
    DOWNLOAD_ENEMY,
  }
}
