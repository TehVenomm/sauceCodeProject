using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestAcceptRoomInviteFriend : FollowListBase
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
		TGL_OK,
		BTN_OK,
		BTN_ALL,
		OBJ_SELECTED,
		OBJ_INVITED,
		OBJ_ROOMCONDITION,
		OBJ_NOT_PROGRESSED,
		LBL_SORT,
		LBL_SELECT_ALL,
		BTN_CHECK_PAGE_ALL_ITEM
	}

	protected const int LIST_ITEM_COUNT_PER_PAGE = 10;

	protected PartyInviteCharaInfo[] inviteUsers;

	protected List<int> selectedUserIdList = new List<int>();

	public override void Initialize()
	{
		int mutualFollowerInviteListSortType = GameSaveData.instance.MutualFollowerInviteListSortType;
		if (0 <= mutualFollowerInviteListSortType && mutualFollowerInviteListSortType < 5)
		{
			m_currentSortType = (USER_SORT_TYPE)mutualFollowerInviteListSortType;
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		UpdateListUI();
		SetToggle(UI.TGL_OK, selectedUserIdList.Count > 0);
		SetLabelText(UI.LBL_SELECT_ALL, IsContainsAllUserInPage() ? StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 40u) : StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 41u));
		SetActive(UI.BTN_CHECK_PAGE_ALL_ITEM, inviteUsers != null && inviteUsers.Length != 0);
	}

	protected void UpdateListUI()
	{
		SetLabelText(UI.STR_TITLE, base.sectionData.GetText("STR_TITLE"));
		SetLabelText(UI.STR_TITLE_REFLECT, base.sectionData.GetText("STR_TITLE"));
		SetLabelText(UI.LBL_SORT, StringTable.Get(STRING_CATEGORY.USER_SORT, (uint)m_currentSortType));
		if (inviteUsers == null || inviteUsers.Length == 0)
		{
			SetActive(UI.STR_NON_LIST, is_visible: true);
			SetActive(UI.GRD_LIST, is_visible: false);
			SetLabelText(UI.LBL_NOW, string.Format("{0}/{1}", "0", "0"));
			SetActive(UI.OBJ_ACTIVE_ROOT, pageNumMax > 1);
			SetActive(UI.OBJ_INACTIVE_ROOT, pageNumMax == 1);
			return;
		}
		SetLabelText(UI.LBL_NOW, $"{nowPage + 1}/{pageNumMax}");
		SetActive(UI.OBJ_ACTIVE_ROOT, pageNumMax > 1);
		SetActive(UI.OBJ_INACTIVE_ROOT, pageNumMax == 1);
		SetActive(UI.STR_NON_LIST, is_visible: false);
		SetActive(UI.GRD_LIST, is_visible: true);
		int currentPageItemLength = GetCurrentPageItemLength();
		PartyInviteCharaInfo[] currentList = new PartyInviteCharaInfo[currentPageItemLength];
		for (int j = 0; j < currentPageItemLength; j++)
		{
			currentList[j] = inviteUsers[nowPage * 10 + j];
		}
		SetDynamicList(UI.GRD_LIST, "QuestInviteeSelectListItem", currentPageItemLength, reset: false, null, null, delegate(int i, Transform t, bool is_recycle)
		{
			PartyInviteCharaInfo partyInviteCharaInfo = currentList[i];
			SetupListItem(partyInviteCharaInfo, i, t, is_recycle);
			string clanId = (partyInviteCharaInfo.userClanData != null) ? partyInviteCharaInfo.userClanData.cId : "0";
			SetFollowStatus(t, partyInviteCharaInfo.userId, partyInviteCharaInfo.following, partyInviteCharaInfo.follower, clanId);
		});
	}

	protected int GetCurrentPageItemLength()
	{
		if (nowPage + 1 >= pageNumMax)
		{
			return Mathf.Clamp(inviteUsers.Length - nowPage * 10, 0, 10);
		}
		return 10;
	}

	protected void SortArray()
	{
		if (inviteUsers != null && inviteUsers.Length != 0)
		{
			switch (m_currentSortType)
			{
			case USER_SORT_TYPE.NAME:
				Array.Sort(inviteUsers, base.UserCompareByName);
				break;
			case USER_SORT_TYPE.LEVEL:
				Array.Sort(inviteUsers, base.UserCompareByLevel);
				break;
			case USER_SORT_TYPE.LOGIN:
				Array.Sort(inviteUsers, base.UserCompareByLoginTime);
				break;
			case USER_SORT_TYPE.PLAY_COUNT:
				Array.Sort(inviteUsers, base.UserCompareByPlayCount);
				break;
			case USER_SORT_TYPE.REGISTER:
				Array.Sort(inviteUsers, base.UserCompareByResistered);
				break;
			}
		}
	}

	protected virtual void SetupListItem(PartyInviteCharaInfo info, int i, Transform t, bool is_recycle)
	{
		SetCharaInfo(info, i, t, is_recycle, isGM: false);
		SetActive(t, UI.OBJ_DISABLE_USER_MASK, !info.canEntry || info.invite || selectedUserIdList.Contains(info.userId));
		SetActive(t, UI.OBJ_INVITED, info.invite);
		SetButtonEnabled(t, !info.invite && info.canEntry);
		SetActive(t, UI.OBJ_SELECTED, selectedUserIdList.Contains(info.userId));
		SetActive(t, UI.OBJ_NOT_PROGRESSED, !info.canEntry);
		SetActive(t, UI.OBJ_ROOMCONDITION, is_visible: false);
	}

	protected override void SendGetList(int page, Action<bool> callback)
	{
		MonoBehaviourSingleton<PartyManager>.I.SendInviteList(delegate(bool is_success, PartyInviteCharaInfo[] recv_data)
		{
			if (is_success)
			{
				nowPage = 0;
				pageNumMax = ((recv_data == null) ? 1 : Mathf.CeilToInt((float)recv_data.Length / 10f));
				inviteUsers = recv_data;
				SortArray();
			}
			if (callback != null)
			{
				callback(is_success);
			}
		});
	}

	public override void OnQuery_FOLLOW_INFO()
	{
		int num = (int)GameSection.GetEventData() + nowPage * 10;
		if (num < 0 || num >= inviteUsers.Length)
		{
			return;
		}
		PartyInviteCharaInfo partyInviteCharaInfo = inviteUsers[num];
		if (partyInviteCharaInfo.canEntry)
		{
			if (selectedUserIdList.Contains(partyInviteCharaInfo.userId))
			{
				selectedUserIdList.Remove(partyInviteCharaInfo.userId);
			}
			else
			{
				selectedUserIdList.Add(partyInviteCharaInfo.userId);
			}
			RefreshUI();
		}
	}

	protected override void OnQuery_SORT()
	{
		UpdateSortType();
		GameSaveData.instance.SetMutualFollowerInviteListSortType((int)m_currentSortType);
		SortArray();
		RefreshUI();
	}

	private void OnQuery_OK()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<PartyManager>.I.SendInvite(selectedUserIdList.ToArray(), delegate(bool is_success, int[] invited_users)
		{
			GameSection.ResumeEvent(is_success);
		});
	}

	protected new void OnQuery_PAGE_PREV()
	{
		nowPage = (nowPage - 1 + pageNumMax) % pageNumMax;
		RefreshUI();
	}

	protected new void OnQuery_PAGE_NEXT()
	{
		nowPage = (nowPage + 1) % pageNumMax;
		RefreshUI();
	}

	protected bool IsContainsAllUserInPage()
	{
		int currentPageItemLength = GetCurrentPageItemLength();
		int num = nowPage * 10;
		bool flag = false;
		for (int i = 0; i < currentPageItemLength; i++)
		{
			PartyInviteCharaInfo partyInviteCharaInfo = inviteUsers[num + i];
			if (IsEnableInvite(partyInviteCharaInfo) && !selectedUserIdList.Contains(partyInviteCharaInfo.userId))
			{
				flag = true;
				break;
			}
		}
		return !flag;
	}

	protected virtual bool IsEnableInvite(PartyInviteCharaInfo _info)
	{
		if (_info == null)
		{
			return false;
		}
		if (!_info.canEntry)
		{
			return false;
		}
		if (_info.invite)
		{
			return false;
		}
		return true;
	}

	protected void OnQuery_CHECK_PAGE_ITEM()
	{
		int currentPageItemLength = GetCurrentPageItemLength();
		int num = nowPage * 10;
		if (!IsContainsAllUserInPage())
		{
			for (int i = 0; i < currentPageItemLength; i++)
			{
				PartyInviteCharaInfo partyInviteCharaInfo = inviteUsers[num + i];
				if (IsEnableInvite(partyInviteCharaInfo) && !selectedUserIdList.Contains(partyInviteCharaInfo.userId))
				{
					selectedUserIdList.Add(partyInviteCharaInfo.userId);
				}
			}
		}
		else
		{
			for (int j = 0; j < currentPageItemLength; j++)
			{
				PartyInviteCharaInfo partyInviteCharaInfo2 = inviteUsers[num + j];
				if (IsEnableInvite(partyInviteCharaInfo2) && selectedUserIdList.Contains(partyInviteCharaInfo2.userId))
				{
					selectedUserIdList.Remove(partyInviteCharaInfo2.userId);
				}
			}
		}
		RefreshUI();
	}
}
