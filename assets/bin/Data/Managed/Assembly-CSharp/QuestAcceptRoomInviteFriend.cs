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
		SPR_ICON_FIRST_MET,
		OBJ_CANT_LOUNGE,
		OBJ_IN_LOUNGE
	}

	protected enum SORT_TYPE
	{
		NAME,
		LEVEL,
		LOGIN
	}

	private SORT_TYPE currentSortType;

	protected PartyInviteCharaInfo[] inviteUsers;

	protected List<int> selectedUserIdList = new List<int>();

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void UpdateUI()
	{
		UpdateListUI();
		SetToggle((Enum)UI.TGL_OK, selectedUserIdList.Count > 0);
	}

	public void UpdateListUI()
	{
		SetLabelText((Enum)UI.STR_TITLE, base.sectionData.GetText("STR_TITLE"));
		SetLabelText((Enum)UI.STR_TITLE_REFLECT, base.sectionData.GetText("STR_TITLE"));
		SetText((Enum)UI.LBL_SORT, "STR_SORT_" + currentSortType.ToString());
		if (inviteUsers == null || inviteUsers.Length == 0)
		{
			SetActive((Enum)UI.STR_NON_LIST, true);
			SetActive((Enum)UI.GRD_LIST, false);
			SetButtonEnabled((Enum)UI.BTN_PAGE_PREV, false);
			SetButtonEnabled((Enum)UI.BTN_PAGE_NEXT, false);
			SetLabelText((Enum)UI.LBL_NOW, "0");
			SetLabelText((Enum)UI.LBL_MAX, "0");
		}
		else
		{
			SetLabelText((Enum)UI.LBL_NOW, (nowPage + 1).ToString());
			SetActive((Enum)UI.STR_NON_LIST, false);
			SetActive((Enum)UI.GRD_LIST, true);
			SetButtonEnabled((Enum)UI.BTN_PAGE_PREV, nowPage > 0);
			SetButtonEnabled((Enum)UI.BTN_PAGE_NEXT, nowPage + 1 < pageNumMax);
			SetDynamicList((Enum)UI.GRD_LIST, "QuestInviteeSelectListItem", inviteUsers.Length, false, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycle)
			{
				PartyInviteCharaInfo partyInviteCharaInfo = inviteUsers[i];
				SetupListItem(partyInviteCharaInfo, i, t, is_recycle);
				SetFollowStatus(t, partyInviteCharaInfo.userId, partyInviteCharaInfo.following, partyInviteCharaInfo.follower);
				if (LoungeMatchingManager.IsValidInLounge())
				{
					SetActive(t, UI.SPR_ICON_FIRST_MET, MonoBehaviourSingleton<LoungeMatchingManager>.I.CheckFirstMet(partyInviteCharaInfo.userId));
				}
			});
		}
	}

	protected void Sort()
	{
		if (inviteUsers != null && inviteUsers.Length != 0)
		{
			switch (currentSortType)
			{
			case SORT_TYPE.NAME:
				Array.Sort(inviteUsers, (PartyInviteCharaInfo a, PartyInviteCharaInfo b) => string.Compare(a.name, b.name));
				break;
			case SORT_TYPE.LEVEL:
				Array.Sort(inviteUsers, (PartyInviteCharaInfo a, PartyInviteCharaInfo b) => (int)b.level - (int)a.level);
				break;
			case SORT_TYPE.LOGIN:
				Array.Sort(inviteUsers, (PartyInviteCharaInfo a, PartyInviteCharaInfo b) => b.lastLoginTm - a.lastLoginTm);
				break;
			}
		}
	}

	private void SetupListItem(PartyInviteCharaInfo info, int i, Transform t, bool is_recycle)
	{
		SetCharaInfo(info, i, t, is_recycle, false);
		SetActive(t, UI.OBJ_DISABLE_USER_MASK, !info.canEntry || info.invite || selectedUserIdList.Contains(info.userId));
		SetActive(t, UI.OBJ_INVITED, info.invite);
		SetButtonEnabled(t, !info.invite && info.canEntry);
		SetActive(t, UI.OBJ_SELECTED, selectedUserIdList.Contains(info.userId));
		SetActive(t, UI.OBJ_NOT_PROGRESSED, !info.canEntry);
		SetActive(t, UI.OBJ_ROOMCONDITION, false);
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "LoungeInviteFriend")
		{
			bool flag = (int)info.level < 15;
			SetActive(t, UI.OBJ_CANT_LOUNGE, flag);
			bool flag2 = false;
			LoungeModel.Lounge loungeData = MonoBehaviourSingleton<LoungeMatchingManager>.I.loungeData;
			for (int j = 0; j < loungeData.slotInfos.Count; j++)
			{
				if (loungeData.slotInfos[j].userInfo != null && loungeData.slotInfos[j].userInfo.userId == info.userId)
				{
					flag2 = true;
					break;
				}
			}
			SetActive(t, UI.OBJ_IN_LOUNGE, flag2);
			if (flag || flag2)
			{
				SetActive(t, UI.OBJ_INVITED, false);
				SetActive(t, UI.OBJ_NOT_PROGRESSED, false);
				SetActive(t, UI.OBJ_ROOMCONDITION, false);
			}
			SetButtonEnabled(t, !info.invite && info.canEntry && !flag2 && !flag);
		}
	}

	protected override void SendGetList(int page, Action<bool> callback)
	{
		MonoBehaviourSingleton<PartyManager>.I.SendInviteList(delegate(bool is_success, PartyInviteCharaInfo[] recv_data)
		{
			if (is_success)
			{
				nowPage = 1;
				pageNumMax = 1;
				inviteUsers = recv_data;
				Sort();
			}
			if (callback != null)
			{
				callback(is_success);
			}
		});
	}

	public override void OnQuery_FOLLOW_INFO()
	{
		int num = (int)GameSection.GetEventData();
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

	private void OnQuery_SORT()
	{
		currentSortType++;
		if ((int)currentSortType >= Enum.GetValues(typeof(SORT_TYPE)).Length)
		{
			currentSortType = SORT_TYPE.NAME;
		}
		Sort();
		RefreshUI();
	}

	private void OnQuery_OK()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<PartyManager>.I.SendInvite(selectedUserIdList.ToArray(), delegate(bool is_success, int[] invited_users)
		{
			GameSection.ResumeEvent(is_success, null);
		});
	}
}
