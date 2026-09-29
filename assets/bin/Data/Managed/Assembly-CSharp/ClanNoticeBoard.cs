// Decompiled with JetBrains decompiler
// Type: ClanNoticeBoard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Globalization;
using UnityEngine;

#nullable disable
public class ClanNoticeBoard : GameSection
{
  public override void UpdateUI()
  {
    this.SetActive((Enum) ClanNoticeBoard.UI.BTN_EDIT, MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsLeader() || MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsSubLeader());
    if (!MonoBehaviourSingleton<ClanManager>.IsValid() || MonoBehaviourSingleton<ClanManager>.I.noticeBoardData == null || MonoBehaviourSingleton<ClanManager>.I.noticeBoardData.contributorUserName == "")
    {
      this.SetActive((Enum) ClanNoticeBoard.UI.LBL_UPDATE_AT, false);
      this.SetActive((Enum) ClanNoticeBoard.UI.LBL_MESSAGE, false);
      this.SetActive((Enum) ClanNoticeBoard.UI.LBL_EDITED_BY, false);
      this.SetActive((Enum) ClanNoticeBoard.UI.LBL_UPDATE_AT, false);
      this.SetActive((Enum) ClanNoticeBoard.UI.SPR_STATUS, false);
      this.SetActive((Enum) ClanNoticeBoard.UI.LBL_NO_MESSAGE, true);
      this.SetFontStyle((Enum) ClanNoticeBoard.UI.LBL_NO_MESSAGE, (FontStyle) 2);
    }
    else
    {
      this.SetActive((Enum) ClanNoticeBoard.UI.LBL_UPDATE_AT, true);
      this.SetActive((Enum) ClanNoticeBoard.UI.LBL_MESSAGE, true);
      this.SetActive((Enum) ClanNoticeBoard.UI.LBL_EDITED_BY, true);
      this.SetActive((Enum) ClanNoticeBoard.UI.LBL_UPDATE_AT, true);
      this.SetStatusSprite(MonoBehaviourSingleton<ClanManager>.I.noticeBoardData.contributorUserClan);
      this.SetLabelText((Enum) ClanNoticeBoard.UI.LBL_UPDATE_AT, MonoBehaviourSingleton<ClanManager>.I.noticeBoardData.updatedAt.ConvToDateTime().ToString("yyyy/MM/dd HH:mm", (IFormatProvider) new CultureInfo("ja-JP")));
      this.SetLabelText((Enum) ClanNoticeBoard.UI.LBL_EDITED_BY, MonoBehaviourSingleton<ClanManager>.I.noticeBoardData.contributorUserName);
      bool is_visible = MonoBehaviourSingleton<ClanManager>.I.noticeBoardData.body == null || MonoBehaviourSingleton<ClanManager>.I.noticeBoardData.body == "";
      this.SetActive((Enum) ClanNoticeBoard.UI.LBL_NO_MESSAGE, is_visible);
      this.SetActive((Enum) ClanNoticeBoard.UI.LBL_MESSAGE, !is_visible);
      if (is_visible)
        this.SetFontStyle((Enum) ClanNoticeBoard.UI.LBL_NO_MESSAGE, (FontStyle) 2);
      else
        this.SetLabelText((Enum) ClanNoticeBoard.UI.LBL_MESSAGE, MonoBehaviourSingleton<ClanManager>.I.noticeBoardData.body);
    }
  }

  private void SetStatusSprite(UserClanData userClan)
  {
    if (userClan.IsLeader())
    {
      this.SetActive((Enum) ClanNoticeBoard.UI.SPR_STATUS, true);
      this.SetSprite((Enum) ClanNoticeBoard.UI.SPR_STATUS, "Clan_HeadmasterIcon");
    }
    else if (userClan.IsSubLeader())
    {
      this.SetActive((Enum) ClanNoticeBoard.UI.SPR_STATUS, true);
      this.SetSprite((Enum) ClanNoticeBoard.UI.SPR_STATUS, "Clan_DeputyHeadmasterIcon");
    }
    else
      this.SetActive((Enum) ClanNoticeBoard.UI.SPR_STATUS, false);
  }

  private void OnQuery_EDIT()
  {
    DateTime dateTime = new DateTime();
    if (MonoBehaviourSingleton<ClanManager>.IsValid() && MonoBehaviourSingleton<ClanManager>.I.noticeBoardData != null && MonoBehaviourSingleton<ClanManager>.I.noticeBoardData.updatedAt != null)
      dateTime = MonoBehaviourSingleton<ClanManager>.I.noticeBoardData.updatedAt.ConvToDateTime();
    TimeSpan timeSpan = DateTime.Now - dateTime;
    if (!(MonoBehaviourSingleton<ClanManager>.I.noticeBoardData.contributorUserId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id.ToString()) || timeSpan.TotalSeconds >= (double) MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.CLAN_NOTICE_BOARD_SEND_INTERVAL_SECONDS)
      return;
    GameSection.ChangeEvent("TIME_ERROR");
  }

  protected enum UI
  {
    LBL_UPDATE_AT,
    LBL_MESSAGE,
    LBL_EDITED_BY,
    SPR_STATUS,
    LBL_NO_MESSAGE,
    BTN_EDIT,
  }
}
