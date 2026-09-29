// Decompiled with JetBrains decompiler
// Type: CommunityDialogBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class CommunityDialogBase : GameSection
{
  public override void Initialize()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "HomeScene")
    {
      this.SetActive((Enum) CommunityDialogBase.UI.BTN_EXIT, false);
      UISprite component = this.GetComponent<UISprite>((Enum) CommunityDialogBase.UI.SPR_FRAME);
      component.height -= 90;
      Vector3 localPosition = ((Component) component).transform.localPosition;
      ((Component) component).transform.localPosition = new Vector3(localPosition.x, localPosition.y - 45f, localPosition.x);
    }
    base.Initialize();
  }

  public override void StartSection()
  {
  }

  public override void UpdateUI()
  {
    bool is_visible1 = MonoBehaviourSingleton<UserInfoManager>.I.clanDisplayType != DISPLAY_TYPE.NONE && (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level >= 15;
    this.SetActive((Enum) CommunityDialogBase.UI.BTN_CLAN_ON, is_visible1);
    this.SetActive((Enum) CommunityDialogBase.UI.BTN_CLAN_OFF, !is_visible1);
    if (is_visible1 && MonoBehaviourSingleton<UserInfoManager>.I.clanDisplayType == DISPLAY_TYPE.NEW)
      this.SetActive((Enum) CommunityDialogBase.UI.SPR_NEW_ICON, true);
    else
      this.SetActive((Enum) CommunityDialogBase.UI.SPR_NEW_ICON, false);
    MonoBehaviourSingleton<UserInfoManager>.I.IsRegisteredClan();
    bool is_visible2 = (int) MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level >= 15;
    this.SetActive((Enum) CommunityDialogBase.UI.BTN_LOUNGE, is_visible2);
    this.SetActive((Enum) CommunityDialogBase.UI.BTN_LOUNGE_OFF, !is_visible2);
  }

  private void OnQuery_CLAN()
  {
    if (!MonoBehaviourSingleton<UserInfoManager>.I.IsRegisteredClan())
      return;
    GameSection.ChangeEvent("REGIESTERED_CLAN");
    GameSection.StayEvent();
    MonoBehaviourSingleton<ClanMatchingManager>.I.RequestDetail("0", (Action<ClanDetailModel.Param>) (result => GameSection.ResumeEvent(true)));
  }

  private void OnQuery_CLAN_ERROR()
  {
    if (MonoBehaviourSingleton<UserInfoManager>.I.clanDisplayType == DISPLAY_TYPE.NONE)
      GameSection.SetEventData((object) StringTable.Get(STRING_CATEGORY.CLAN_ERROR, 0U));
    else
      GameSection.SetEventData((object) string.Format(StringTable.Get(STRING_CATEGORY.CLAN_ERROR, 1U), (object) 15.ToString()));
  }

  private void OnQuery_LOUNGE()
  {
    if (!MonoBehaviourSingleton<LoungeManager>.IsValid())
      return;
    GameSection.ChangeEvent("LOUNGE_SETTING");
  }

  private void OnQuery_LOUNGE_ERROR()
  {
    GameSection.SetEventData((object) new string[2]
    {
      15.ToString(),
      "OK"
    });
  }

  private void OnQuery_EXIT()
  {
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[1]
    {
      new EventData("MAIN_MENU_HOME", (object) null)
    });
  }

  private enum UI
  {
    SPR_FRAME,
    BTN_CLAN_ON,
    BTN_CLAN_OFF,
    BTN_LOUNGE,
    BTN_LOUNGE_OFF,
    BTN_EXIT,
    SPR_NEW_ICON,
  }
}
