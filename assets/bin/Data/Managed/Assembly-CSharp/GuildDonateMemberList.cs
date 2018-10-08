using Network;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuildDonateMemberList : GuildMemberList
{
	protected new enum UI
	{
		OBJ_GUILD_NUMBER_ROOT,
		LBL_GUILD_NUMBER_NOW,
		LBL_GUILD_NUMBER_MAX,
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
		LBL_NAME,
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		LBL_LEVEL,
		OBJ_DEGREE_FRAME_ROOT,
		SPR_ICON_FIRST_MET,
		OBJ_OFFLINE_MASK,
		OBJ_SELECTED,
		OBJ_REQUEST_PENDING
	}

	private DonateInfo _info;

	private List<int> _invitedList;

	public override string ListItemEvent => "SELECT";

	public override void Initialize()
	{
		_info = (GameSection.GetEventData() as DonateInfo);
		base.Initialize();
	}

	protected override void GetListItem(Action<bool, object> callback)
	{
		base.GetListItem(delegate(bool success, object obj)
		{
			MonoBehaviourSingleton<GuildManager>.I.SendDonateInviteList(_info.id, delegate(bool donate_success, GuildDonate.GuildDonateInviteListModel ret)
			{
				allMember = new List<FriendCharaInfo>(ret.result.list);
				allMember.Remove(members.FirstOrDefault((FriendCharaInfo o) => o.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id));
				_invitedList = (from user in ret.result.donate_list
				where user.isInvited
				select user into x
				select x.userId).ToList();
				callback(success, null);
			});
		});
	}

	protected override void SetListItem(int i, Transform t, string event_name, FriendCharaInfo member)
	{
		SetActive(t, UI.OBJ_OFFLINE_MASK, false);
		bool flag = _invitedList.Contains(member.userId);
		SetActive(t, UI.OBJ_SELECTED, flag);
		SetButtonEnabled(t, !flag);
		if (!flag)
		{
			SetEvent(t, ListItemEvent, new object[2]
			{
				i,
				member
			});
		}
	}

	private void OnQuery_SELECT()
	{
		object[] array = GameSection.GetEventData() as object[];
		int index = (int)array[0];
		FriendCharaInfo member = array[1] as FriendCharaInfo;
		GameSection.StayEvent();
		MonoBehaviourSingleton<GuildManager>.I.SendDonateInvite(_info.id, member.userId, delegate(bool success)
		{
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Expected O, but got Unknown
			if (success)
			{
				Transform ctrl = GetCtrl(UI.GRD_LIST);
				Transform val = ctrl.GetChild(index).GetChild(0);
				SetActive(val, UI.OBJ_SELECTED, true);
				SetButtonEnabled(val, false);
				_invitedList.Remove(member.userId);
			}
			GameSection.ResumeEvent(false, null);
		});
	}

	private void OnQuery_INVITE()
	{
	}
}
