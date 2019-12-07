using Network;
using System;
using System.Globalization;
using UnityEngine;

public class ClanNoticeBoard : GameSection
{
	protected enum UI
	{
		LBL_UPDATE_AT,
		LBL_MESSAGE,
		LBL_EDITED_BY,
		SPR_STATUS,
		LBL_NO_MESSAGE,
		BTN_EDIT
	}

	public override void UpdateUI()
	{
		bool is_visible = MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsLeader() || MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsSubLeader();
		SetActive(UI.BTN_EDIT, is_visible);
		if (!MonoBehaviourSingleton<ClanManager>.IsValid() || MonoBehaviourSingleton<ClanManager>.I.noticeBoardData == null || MonoBehaviourSingleton<ClanManager>.I.noticeBoardData.contributorUserName == "")
		{
			SetActive(UI.LBL_UPDATE_AT, is_visible: false);
			SetActive(UI.LBL_MESSAGE, is_visible: false);
			SetActive(UI.LBL_EDITED_BY, is_visible: false);
			SetActive(UI.LBL_UPDATE_AT, is_visible: false);
			SetActive(UI.SPR_STATUS, is_visible: false);
			SetActive(UI.LBL_NO_MESSAGE, is_visible: true);
			SetFontStyle(UI.LBL_NO_MESSAGE, FontStyle.Italic);
			return;
		}
		SetActive(UI.LBL_UPDATE_AT, is_visible: true);
		SetActive(UI.LBL_MESSAGE, is_visible: true);
		SetActive(UI.LBL_EDITED_BY, is_visible: true);
		SetActive(UI.LBL_UPDATE_AT, is_visible: true);
		SetStatusSprite(MonoBehaviourSingleton<ClanManager>.I.noticeBoardData.contributorUserClan);
		SetLabelText(UI.LBL_UPDATE_AT, MonoBehaviourSingleton<ClanManager>.I.noticeBoardData.updatedAt.ConvToDateTime().ToString("yyyy/MM/dd HH:mm", new CultureInfo("ja-JP")));
		SetLabelText(UI.LBL_EDITED_BY, MonoBehaviourSingleton<ClanManager>.I.noticeBoardData.contributorUserName);
		bool flag = MonoBehaviourSingleton<ClanManager>.I.noticeBoardData.body == null || MonoBehaviourSingleton<ClanManager>.I.noticeBoardData.body == "";
		SetActive(UI.LBL_NO_MESSAGE, flag);
		SetActive(UI.LBL_MESSAGE, !flag);
		if (flag)
		{
			SetFontStyle(UI.LBL_NO_MESSAGE, FontStyle.Italic);
		}
		else
		{
			SetLabelText(UI.LBL_MESSAGE, MonoBehaviourSingleton<ClanManager>.I.noticeBoardData.body);
		}
	}

	private void SetStatusSprite(UserClanData userClan)
	{
		if (userClan.IsLeader())
		{
			SetActive(UI.SPR_STATUS, is_visible: true);
			SetSprite(UI.SPR_STATUS, "Clan_HeadmasterIcon");
		}
		else if (userClan.IsSubLeader())
		{
			SetActive(UI.SPR_STATUS, is_visible: true);
			SetSprite(UI.SPR_STATUS, "Clan_DeputyHeadmasterIcon");
		}
		else
		{
			SetActive(UI.SPR_STATUS, is_visible: false);
		}
	}

	private void OnQuery_EDIT()
	{
		DateTime d = default(DateTime);
		if (MonoBehaviourSingleton<ClanManager>.IsValid() && MonoBehaviourSingleton<ClanManager>.I.noticeBoardData != null && MonoBehaviourSingleton<ClanManager>.I.noticeBoardData.updatedAt != null)
		{
			d = MonoBehaviourSingleton<ClanManager>.I.noticeBoardData.updatedAt.ConvToDateTime();
		}
		TimeSpan timeSpan = DateTime.Now - d;
		if (MonoBehaviourSingleton<ClanManager>.I.noticeBoardData.contributorUserId != MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id.ToString() && timeSpan.TotalSeconds < (double)MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.CLAN_NOTICE_BOARD_SEND_INTERVAL_SECONDS)
		{
			GameSection.ChangeEvent("TIME_ERROR");
		}
	}
}
