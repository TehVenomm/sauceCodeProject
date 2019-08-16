using Network;
using System;
using UnityEngine;

public class GuildInviteFriend : QuestAcceptRoomInviteFriend
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
		SPR_FOLLOW,
		SPR_FOLLOWER,
		SPR_BLACKLIST_ICON,
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
		TGL_OK,
		BTN_OK,
		BTN_ALL,
		OBJ_SELECTED,
		OBJ_INVITED,
		OBJ_ROOMCONDITION,
		OBJ_NOT_PROGRESSED,
		LBL_SORT
	}

	protected override void SendGetList(int page, Action<bool> callback)
	{
		MonoBehaviourSingleton<GuildManager>.I.SendInviteList(delegate(bool is_success, GuildInviteCharaInfo[] recv_data)
		{
			if (is_success)
			{
				nowPage = page;
				pageNumMax = ((recv_data != null && recv_data.Length > 0) ? Mathf.CeilToInt((float)recv_data.Length / 10f) : 0);
				inviteUsers = recv_data;
				Sort(GetCurrentUserList());
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
		MonoBehaviourSingleton<GuildManager>.I.SendInvite(selectedUserIdList.ToArray(), delegate(bool is_success, int[] invited_users)
		{
			GameSection.ResumeEvent(is_success);
		});
	}
}
