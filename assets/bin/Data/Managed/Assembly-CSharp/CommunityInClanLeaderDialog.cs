// Decompiled with JetBrains decompiler
// Type: CommunityInClanLeaderDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class CommunityInClanLeaderDialog : CommunityDialogBase
{
  public override void UpdateUI()
  {
    base.UpdateUI();
    if (MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsLeader())
      this.SetActive((Enum) CommunityInClanLeaderDialog.UI.BTN_CLAN_SETTING, true);
    else
      this.SetActive((Enum) CommunityInClanLeaderDialog.UI.BTN_CLAN_SETTING, false);
    this.UpdateBadge();
  }

  private void UpdateBadge()
  {
    Transform ctrl = this.GetCtrl((Enum) CommunityInClanLeaderDialog.UI.BTN_CLAN_APPLIED);
    if (Object.op_Equality((Object) ctrl, (Object) null) || !((Component) ctrl).gameObject.activeSelf)
      return;
    int num = MonoBehaviourSingleton<UserInfoManager>.I.clanRequestNum;
    if (num < 0)
      num = 0;
    this.SetBadge(ctrl, num, (SpriteAlignment) 3, -8, -8);
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_EQUIP_EVOLVE) == (GameSection.NOTIFY_FLAG) 0)
      return;
    this.UpdateBadge();
  }

  private void OnQuery_CLAN_DETAIL()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "ClanScene")
      GameSection.ChangeEvent("CLAN_REGISTERD");
    else
      GameSection.SetEventData((object) "0");
  }

  private new enum UI
  {
    SPR_FRAME,
    BTN_CLAN_ON,
    BTN_CLAN_OFF,
    BTN_LOUNGE,
    BTN_LOUNGE_OFF,
    BTN_EXIT,
    SPR_NEW_ICON,
    BTN_CLAN_SETTING,
    BTN_CLAN_APPLIED,
  }
}
