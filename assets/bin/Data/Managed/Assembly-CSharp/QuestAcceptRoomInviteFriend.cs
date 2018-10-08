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
		LBL_SORT,
		LBL_SELECT_ALL
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
		SetToggle((Enum)UI.TGL_OK, selectedUserIdList.Count > 0);
		SetLabelText((Enum)UI.LBL_SELECT_ALL, (!IsContainsAllUserInPage()) ? "全選択" : "選択解除");
	}

	protected unsafe void UpdateListUI()
	{
		SetLabelText((Enum)UI.STR_TITLE, base.sectionData.GetText("STR_TITLE"));
		SetLabelText((Enum)UI.STR_TITLE_REFLECT, base.sectionData.GetText("STR_TITLE"));
		SetLabelText((Enum)UI.LBL_SORT, StringTable.Get(STRING_CATEGORY.USER_SORT, (uint)m_currentSortType));
		if (inviteUsers == null || inviteUsers.Length == 0)
		{
			SetActive((Enum)UI.STR_NON_LIST, true);
			SetActive((Enum)UI.GRD_LIST, false);
			SetLabelText((Enum)UI.LBL_NOW, string.Format("{0}/{1}", "0", "0"));
			SetActive((Enum)UI.OBJ_ACTIVE_ROOT, pageNumMax > 1);
			SetActive((Enum)UI.OBJ_INACTIVE_ROOT, pageNumMax == 1);
		}
		else
		{
			SetLabelText((Enum)UI.LBL_NOW, $"{nowPage + 1}/{pageNumMax}");
			SetActive((Enum)UI.OBJ_ACTIVE_ROOT, pageNumMax > 1);
			SetActive((Enum)UI.OBJ_INACTIVE_ROOT, pageNumMax == 1);
			SetActive((Enum)UI.STR_NON_LIST, false);
			SetActive((Enum)UI.GRD_LIST, true);
			int currentPageItemLength = GetCurrentPageItemLength();
			PartyInviteCharaInfo[] currentList = new PartyInviteCharaInfo[currentPageItemLength];
			for (int i = 0; i < currentPageItemLength; i++)
			{
				currentList[i] = inviteUsers[nowPage * 10 + i];
			}
			_003CUpdateListUI_003Ec__AnonStorey32C _003CUpdateListUI_003Ec__AnonStorey32C;
			SetDynamicList((Enum)UI.GRD_LIST, "QuestInviteeSelectListItem", currentPageItemLength, false, null, null, new Action<int, Transform, bool>((object)_003CUpdateListUI_003Ec__AnonStorey32C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	protected int GetCurrentPageItemLength()
	{
		return (nowPage + 1 >= pageNumMax) ? Mathf.Clamp(inviteUsers.Length - nowPage * 10, 0, 10) : 10;
	}

	protected void SortArray()
	{
		if (inviteUsers != null && inviteUsers.Length != 0)
		{
			switch (m_currentSortType)
			{
			case USER_SORT_TYPE.NAME:
				Array.Sort(inviteUsers, base.UserCompareByName<PartyInviteCharaInfo>);
				break;
			case USER_SORT_TYPE.LEVEL:
				Array.Sort(inviteUsers, base.UserCompareByLevel<PartyInviteCharaInfo>);
				break;
			case USER_SORT_TYPE.LOGIN:
				Array.Sort(inviteUsers, base.UserCompareByLoginTime<PartyInviteCharaInfo>);
				break;
			case USER_SORT_TYPE.PLAY_COUNT:
				Array.Sort(inviteUsers, base.UserCompareByPlayCount<PartyInviteCharaInfo>);
				break;
			case USER_SORT_TYPE.REGISTER:
				Array.Sort(inviteUsers, base.UserCompareByResistered<PartyInviteCharaInfo>);
				break;
			}
		}
	}

	protected virtual void SetupListItem(PartyInviteCharaInfo info, int i, Transform t, bool is_recycle)
	{
		SetCharaInfo(info, i, t, is_recycle, false);
		SetActive(t, UI.OBJ_DISABLE_USER_MASK, !info.canEntry || info.invite || selectedUserIdList.Contains(info.userId));
		SetActive(t, UI.OBJ_INVITED, info.invite);
		SetButtonEnabled(t, !info.invite && info.canEntry);
		SetActive(t, UI.OBJ_SELECTED, selectedUserIdList.Contains(info.userId));
		SetActive(t, UI.OBJ_NOT_PROGRESSED, !info.canEntry);
		SetActive(t, UI.OBJ_ROOMCONDITION, false);
	}

	protected unsafe override void SendGetList(int page, Action<bool> callback)
	{
		_003CSendGetList_003Ec__AnonStorey32D _003CSendGetList_003Ec__AnonStorey32D;
		MonoBehaviourSingleton<PartyManager>.I.SendInviteList(new Action<bool, PartyInviteCharaInfo[]>((object)_003CSendGetList_003Ec__AnonStorey32D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	public override void OnQuery_FOLLOW_INFO()
	{
		int num = (int)GameSection.GetEventData();
		int num2 = num + nowPage * 10;
		if (num2 >= 0 && num2 < inviteUsers.Length)
		{
			PartyInviteCharaInfo partyInviteCharaInfo = inviteUsers[num2];
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
	}

	protected override void OnQuery_SORT()
	{
		UpdateSortType();
		GameSaveData.instance.SetMutualFollowerInviteListSortType((int)m_currentSortType);
		SortArray();
		RefreshUI();
	}

	private unsafe void OnQuery_OK()
	{
		GameSection.StayEvent();
		PartyManager i = MonoBehaviourSingleton<PartyManager>.I;
		int[] userIds = selectedUserIdList.ToArray();
		if (_003C_003Ef__am_0024cache2 == null)
		{
			_003C_003Ef__am_0024cache2 = new Action<bool, int[]>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		i.SendInvite(userIds, _003C_003Ef__am_0024cache2);
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
