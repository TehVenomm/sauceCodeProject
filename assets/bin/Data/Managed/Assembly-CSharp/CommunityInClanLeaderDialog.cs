using System;
using UnityEngine;

public class CommunityInClanLeaderDialog : CommunityDialogBase
{
	private enum UI
	{
		SPR_FRAME,
		BTN_CLAN_ON,
		BTN_CLAN_OFF,
		BTN_LOUNGE,
		BTN_LOUNGE_OFF,
		BTN_EXIT,
		SPR_NEW_ICON,
		BTN_CLAN_SETTING,
		BTN_CLAN_APPLIED
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		if (MonoBehaviourSingleton<UserInfoManager>.I.userClan.IsLeader())
		{
			SetActive((Enum)UI.BTN_CLAN_SETTING, is_visible: true);
		}
		else
		{
			SetActive((Enum)UI.BTN_CLAN_SETTING, is_visible: false);
		}
		UpdateBadge();
	}

	private void UpdateBadge()
	{
		Transform ctrl = GetCtrl(UI.BTN_CLAN_APPLIED);
		if (!(ctrl == null) && ctrl.get_gameObject().get_activeSelf())
		{
			int num = MonoBehaviourSingleton<UserInfoManager>.I.clanRequestNum;
			if (num < 0)
			{
				num = 0;
			}
			SetBadge(ctrl, num, 3, -8, -8);
		}
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.UPDATE_EQUIP_EVOLVE) != (NOTIFY_FLAG)0L)
		{
			UpdateBadge();
		}
	}

	private void OnQuery_CLAN_DETAIL()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "ClanScene")
		{
			GameSection.ChangeEvent("CLAN_REGISTERD");
		}
		else
		{
			GameSection.SetEventData("0");
		}
	}
}
