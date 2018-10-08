using Network;
using System;

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

	protected unsafe override void SendGetList(int page, Action<bool> callback)
	{
		_003CSendGetList_003Ec__AnonStorey32D _003CSendGetList_003Ec__AnonStorey32D;
		MonoBehaviourSingleton<GuildManager>.I.SendInviteList(new Action<bool, GuildInviteCharaInfo[]>((object)_003CSendGetList_003Ec__AnonStorey32D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return base.GetUpdateUINotifyFlags() | NOTIFY_FLAG.RECEIVE_COOP_ROOM_UPDATE;
	}

	private unsafe void OnQuery_OK()
	{
		GameSection.StayEvent();
		GuildManager i = MonoBehaviourSingleton<GuildManager>.I;
		int[] userIds = selectedUserIdList.ToArray();
		if (_003C_003Ef__am_0024cache0 == null)
		{
			_003C_003Ef__am_0024cache0 = new Action<bool, int[]>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		i.SendInvite(userIds, _003C_003Ef__am_0024cache0);
	}
}
