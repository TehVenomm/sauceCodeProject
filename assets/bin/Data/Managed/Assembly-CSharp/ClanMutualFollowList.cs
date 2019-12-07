using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ClanMutualFollowList : FollowListBase
{
	protected new enum UI
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
		OBJ_DEGREE_FRAME_ROOT,
		SPR_ICON_FIRST_MET,
		OBJ_SWITCH_INFO,
		DEFAULT_STATUS_ROOT,
		JOIN_STATUS_ROOT,
		ONLINE_TEXT_ROOT,
		ONLINE_TEXT,
		DETAIL_TEXT,
		JOIN_BUTTON_ROOT,
		BTN_JOIN_BUTTON,
		LBL_BUTTON_TEXT,
		BTN_SORT,
		LBL_SORT,
		IN_CLAN_TEXT_ROOT,
		NOT_IN_CLAN_TEXT_ROOT,
		SPR_CLAN_STATUS,
		LBL_CLAN_NAME
	}

	private List<FriendMessageUserListModel.MessageUserInfo> recvdata;

	public override void Initialize()
	{
		isInitializeSend = true;
		m_isVisibleDefaultInfo = false;
		int mutualFollowerListSortType = GameSaveData.instance.MutualFollowerListSortType;
		if (0 <= mutualFollowerListSortType && mutualFollowerListSortType < 5)
		{
			m_currentSortType = (USER_SORT_TYPE)mutualFollowerListSortType;
		}
		titleType = TITLE_TYPE.MESSAGE;
		base.Initialize();
	}

	public override void StartSection()
	{
	}

	public override void UpdateUI()
	{
		ListUI();
	}

	public override void ListUI()
	{
		SetLabelText(UI.STR_TITLE, base.sectionData.GetText("STR_TITLE"));
		SetLabelText(UI.STR_TITLE_REFLECT, base.sectionData.GetText("STR_TITLE"));
		SetLabelText(UI.LBL_SORT, StringTable.Get(STRING_CATEGORY.USER_SORT, (uint)m_currentSortType));
		FriendMessageUserListModel.MessageUserInfo[] array = recvdata.ToArray();
		if (array == null || array.Length == 0)
		{
			SetActive(UI.STR_NON_LIST, is_visible: true);
			SetActive(UI.GRD_LIST, is_visible: false);
			SetActive(UI.OBJ_ACTIVE_ROOT, is_visible: false);
			SetActive(UI.OBJ_INACTIVE_ROOT, is_visible: true);
			SetLabelText(UI.LBL_NOW, "0");
			SetLabelText(UI.LBL_MAX, "0");
		}
		else
		{
			SetPageNumText(UI.LBL_NOW, nowPage + 1);
			SetPageNumText(UI.LBL_MAX, pageNumMax);
			SetActive(UI.STR_NON_LIST, is_visible: false);
			SetActive(UI.GRD_LIST, is_visible: true);
			SetActive(UI.OBJ_ACTIVE_ROOT, pageNumMax != 1);
			SetActive(UI.OBJ_INACTIVE_ROOT, pageNumMax == 1);
			UpdateDynamicList();
		}
	}

	private FriendMessageUserListModel.MessageUserInfo[] GetMessageUserInfo()
	{
		FriendMessageUserListModel.MessageUserInfo[] array = recvdata.ToArray();
		int pageItemLength = GetPageItemLength(nowPage);
		int num = nowPage * 10;
		if (pageItemLength < 1 || array.Length < 1 || array.Length < num + pageItemLength)
		{
			return null;
		}
		FriendMessageUserListModel.MessageUserInfo[] array2 = new FriendMessageUserListModel.MessageUserInfo[pageItemLength];
		for (int i = 0; i < pageItemLength; i++)
		{
			array2[i] = array[num + i];
		}
		return array2;
	}

	protected override void UpdateDynamicList()
	{
		int pageItemLength = GetPageItemLength(nowPage);
		FriendMessageUserListModel.MessageUserInfo[] info = GetMessageUserInfo();
		if (GameDefine.ACTIVE_DEGREE)
		{
			base.ScrollGrid.cellHeight = GameDefine.DEGREE_FRIEND_LIST_HEIGHT;
		}
		CleanItemList();
		SetDynamicList(UI.GRD_LIST, "FriendInClanListItem", pageItemLength, reset: false, null, null, delegate(int i, Transform t, bool is_recycle)
		{
			FriendMessageUserListModel.MessageUserInfo messageUserInfo = info[i];
			string clanId = (messageUserInfo.userClanData != null) ? messageUserInfo.userClanData.cId : "0";
			SetFollowStatus(t, messageUserInfo.userId, following: true, follower: true, clanId);
			SetCharaInfo(messageUserInfo, i, t, is_recycle, messageUserInfo.userId == 0);
			SetBadge(t, messageUserInfo.noReadNum, SpriteAlignment.TopRight, -10, -6);
		});
	}

	protected override void SetCharaInfo(FriendCharaInfo data, int i, Transform t, bool is_recycle, bool isGM)
	{
		base.SetCharaInfo(data, i, t, is_recycle, isGM);
		SetUserClanInfo(t, i, data.userClanData);
		if (data.userClanData.IsRegistered())
		{
			SetEvent(t, "DETAIL", i);
		}
		else
		{
			SetEvent(t, "NONE", i);
		}
	}

	protected override void SetJoinInfo(Transform t, int _itemIndex, FriendCharaInfo.JoinInfo _joinStatus, string _lastLoginText)
	{
	}

	protected void SetUserClanInfo(Transform t, int _itemIndex, UserClanData userClanData)
	{
		if (userClanData.IsRegistered())
		{
			SetActive(t, UI.IN_CLAN_TEXT_ROOT, is_visible: true);
			SetActive(t, UI.NOT_IN_CLAN_TEXT_ROOT, is_visible: false);
			SetStatusSprite(t, userClanData);
			SetLabelText(t, UI.LBL_CLAN_NAME, userClanData.name);
		}
		else
		{
			SetActive(t, UI.IN_CLAN_TEXT_ROOT, is_visible: false);
			SetActive(t, UI.NOT_IN_CLAN_TEXT_ROOT, is_visible: true);
		}
	}

	private void SetStatusSprite(Transform root, UserClanData userClan)
	{
		if (userClan.IsLeader())
		{
			SetActive(root, UI.SPR_CLAN_STATUS, is_visible: true);
			SetSprite(root, UI.SPR_CLAN_STATUS, "Clan_HeadmasterIcon");
		}
		else if (userClan.IsSubLeader())
		{
			SetActive(root, UI.SPR_CLAN_STATUS, is_visible: true);
			SetSprite(root, UI.SPR_CLAN_STATUS, "Clan_DeputyHeadmasterIcon");
		}
		else
		{
			SetActive(root, UI.SPR_CLAN_STATUS, is_visible: false);
		}
	}

	private int GetPageItemLength(int currentPage)
	{
		if (currentPage + 1 < pageNumMax || recvdata.Count % 10 <= 0)
		{
			return 10;
		}
		return recvdata.Count % 10;
	}

	protected override void SendGetList(int page, Action<bool> callback = null)
	{
		MonoBehaviourSingleton<FriendManager>.I.SendGetMessageUserList(page, delegate(bool is_success, FriendMessageUserListModel.Param recv_data)
		{
			if (is_success)
			{
				recvdata = recv_data.messageUser;
				nowPage = page;
				pageNumMax = Mathf.CeilToInt((float)recvdata.Count / 10f);
				Sort(recvdata);
			}
			if (callback != null)
			{
				callback(is_success);
			}
		});
	}

	public void OnQuery_DETAIL()
	{
		int num = (int)GameSection.GetEventData();
		FriendMessageUserListModel.MessageUserInfo[] messageUserInfo = GetMessageUserInfo();
		if (num < 0 || messageUserInfo.IsNullOrEmpty() || messageUserInfo.Length <= num)
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.SetEventData(messageUserInfo[num].userClanData.cId);
		}
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.UPDATE_FRIEND_PARAM) != (NOTIFY_FLAG)0L)
		{
			isInitializeSendReopen = true;
		}
		if ((flags & NOTIFY_FLAG.UPDATE_FRIEND_LIST) != (NOTIFY_FLAG)0L)
		{
			SetDirtyTable();
		}
		base.OnNotify(flags);
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_FRIEND_LIST;
	}

	protected override void OnQuery_PAGE_PREV()
	{
		int num = nowPage = (nowPage - 1 + pageNumMax) % pageNumMax;
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GetUpdateUINotifyFlags());
	}

	protected override void OnQuery_PAGE_NEXT()
	{
		int num = nowPage = (nowPage + 1) % pageNumMax;
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GetUpdateUINotifyFlags());
	}

	protected override bool IsHideSwitchInfoButton()
	{
		return false;
	}

	protected override void OnQuery_JOIN_FRIEND()
	{
		int num = (int)GameSection.GetEventData();
		if (recvdata == null || recvdata.Count <= num || num < 0)
		{
			return;
		}
		FriendCharaInfo.JoinInfo joinStatus = recvdata[num].joinStatus;
		if (joinStatus == null)
		{
			return;
		}
		GameSection.StayEvent();
		switch (joinStatus.joinType)
		{
		case 3:
			if (MonoBehaviourSingleton<PartyManager>.IsValid())
			{
				MonoBehaviourSingleton<PartyManager>.I.SendApply(joinStatus.conditionParam, delegate(bool isSucceed, Error error)
				{
					if (isSucceed)
					{
						MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("QuestAccept", "QuestAcceptRoom");
					}
					GameSection.ResumeEvent(is_resume: true);
				}, joinStatus.targetParam);
			}
			break;
		case 2:
			JoinLounge(joinStatus);
			break;
		case 4:
		{
			int toUserId = int.Parse(joinStatus.conditionParam);
			JoinField(joinStatus.targetParam, toUserId, delegate(bool is_matching, bool is_connect, bool is_regist)
			{
				if (!is_matching)
				{
					GameSection.StopEvent();
				}
				else if (!is_connect)
				{
					GameSection.StopEvent();
				}
				else
				{
					GameSection.ResumeEvent(is_regist);
					if (is_regist)
					{
						MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("InGame");
					}
				}
			});
			break;
		}
		default:
			GameSection.ResumeEvent(is_resume: true);
			break;
		}
	}

	protected override void OnQuery_SORT()
	{
		UpdateSortType();
		GameSaveData.instance.SetMutualFollowerListSortType((int)m_currentSortType);
		Sort(recvdata);
		RefreshUI();
	}

	protected override void UpdateSortType()
	{
		m_currentSortType++;
		if (m_currentSortType >= USER_SORT_TYPE.MAX)
		{
			m_currentSortType = USER_SORT_TYPE.NAME;
		}
	}
}
