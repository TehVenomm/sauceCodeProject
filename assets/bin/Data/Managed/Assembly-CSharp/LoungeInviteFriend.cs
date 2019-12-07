using Network;
using System;
using UnityEngine;

public class LoungeInviteFriend : QuestAcceptRoomInviteFriend
{
	private new enum UI
	{
		SPR_TITLE_FOLLOW_LIST,
		SPR_TITLE_FOLLOWER_LIST,
		SPR_TITLE_MESSAGE,
		SPR_TITLE_BLACKLIST,
		OBJ_FOLLOW_NUMBER_ROOT,
		LBL_FOLLOW_NUMBER_NOW,
		LBL_FOLLOW_NUMBER_MAX,
		OBJ_DISABLE_USER_MASK,
		LBL_NAME,
		GRD_LIST,
		TEX_MODEL,
		STR_NON_LIST,
		GRD_FOLLOW_ARROW,
		OBJ_FOLLOW,
		SPR_FOLLOW,
		SPR_FOLLOWER,
		SPR_BLACKLIST_ICON,
		SPR_SAME_CLAN_ICON,
		OBJ_COMMENT,
		LBL_COMMENT,
		LBL_LAST_LOGIN,
		LBL_LAST_LOGIN_TIME,
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		LBL_LEVEL,
		LBL_NOW,
		LBL_MAX,
		OBJ_ACTIVE_ROOT,
		OBJ_INACTIVE_ROOT,
		BTN_PAGE_PREV,
		BTN_PAGE_NEXT,
		STR_TITLE,
		STR_TITLE_REFLECT,
		LBL_SELECT_ALL,
		TGL_OK,
		BTN_OK,
		BTN_ALL,
		OBJ_SELECTED,
		OBJ_INVITED,
		OBJ_ROOMCONDITION,
		OBJ_NOT_PROGRESSED,
		LBL_SORT,
		SPR_ICON_FIRST_MET,
		OBJ_CANT_LOUNGE,
		OBJ_IN_LOUNGE
	}

	protected override void SendGetList(int page, Action<bool> callback)
	{
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInviteList(delegate(bool is_success, LoungeInviteCharaInfo[] recv_data)
		{
			if (is_success)
			{
				nowPage = 0;
				pageNumMax = ((recv_data == null) ? 1 : Mathf.CeilToInt((float)recv_data.Length / 10f));
				PartyInviteCharaInfo[] array = inviteUsers = recv_data;
				SortArray();
			}
			if (callback != null)
			{
				callback(is_success);
			}
		});
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return base.GetUpdateUINotifyFlags() | NOTIFY_FLAG.RECEIVE_COOP_ROOM_UPDATE;
	}

	private void OnQuery_OK()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SendInvite(selectedUserIdList.ToArray(), delegate(bool is_success, int[] invited_users)
		{
			GameSection.ResumeEvent(is_success);
		});
	}

	protected override void SetupListItem(PartyInviteCharaInfo info, int i, Transform t, bool is_recycle)
	{
		base.SetupListItem(info, i, t, is_recycle);
		bool flag = (int)info.level < 15;
		SetActive(t, UI.OBJ_CANT_LOUNGE, flag);
		bool flag2 = IsUserInSameLounge(info);
		SetActive(t, UI.OBJ_IN_LOUNGE, flag2);
		if (flag | flag2)
		{
			SetActive(t, UI.OBJ_INVITED, is_visible: false);
			SetActive(t, UI.OBJ_NOT_PROGRESSED, is_visible: false);
			SetActive(t, UI.OBJ_ROOMCONDITION, is_visible: false);
		}
		SetButtonEnabled(t, !info.invite && info.canEntry && !flag2 && !flag);
		if (LoungeMatchingManager.IsValidInLounge())
		{
			SetActive(t, UI.SPR_ICON_FIRST_MET, MonoBehaviourSingleton<LoungeMatchingManager>.I.CheckFirstMet(info.userId));
		}
	}

	protected override bool IsEnableInvite(PartyInviteCharaInfo _info)
	{
		if (!base.IsEnableInvite(_info))
		{
			return false;
		}
		if ((int)_info.level < 15)
		{
			return false;
		}
		if (IsUserInSameLounge(_info))
		{
			return false;
		}
		return true;
	}

	private bool IsUserInSameLounge(PartyInviteCharaInfo _info)
	{
		if (_info == null)
		{
			return false;
		}
		LoungeModel.Lounge loungeData = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData;
		if (loungeData == null)
		{
			return false;
		}
		for (int i = 0; i < loungeData.slotInfos.Count; i++)
		{
			if (loungeData.slotInfos[i].userInfo != null && loungeData.slotInfos[i].userInfo.userId == _info.userId)
			{
				return true;
			}
		}
		return false;
	}
}
