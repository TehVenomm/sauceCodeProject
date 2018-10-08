using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FriendArenaRanking : FriendArenaRankingBase
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
		OBJ_STATUS,
		LBL_TIME,
		LBL_ARENA_NAME,
		SPR_1,
		SPR_2,
		SPR_3,
		LBL_RANK,
		SCR_LIST,
		BTN_OWN,
		OBJ_OWN_OFF,
		LBL_TIME_DEFAULT,
		BTN_TAB_A,
		BTN_TAB_B,
		BTN_TAB_C,
		BTN_TAB_D,
		BTN_TAB_E,
		BTN_TAB_TOTAL,
		LBL_END_DATE
	}

	private static readonly UI[] GroupTabList = new UI[6]
	{
		UI.BTN_TAB_A,
		UI.BTN_TAB_B,
		UI.BTN_TAB_C,
		UI.BTN_TAB_D,
		UI.BTN_TAB_E,
		UI.BTN_TAB_TOTAL
	};

	private UIButton[] buttonList = new UIButton[GroupTabList.Length];

	protected List<FriendCharaInfo>[] charaListList = new List<FriendCharaInfo>[GroupTabList.Length];

	protected List<ArenaRankingData>[] rankingDataListList = new List<ArenaRankingData>[GroupTabList.Length];

	private List<FriendCharaInfo>[] charaListListOwn = new List<FriendCharaInfo>[GroupTabList.Length];

	private List<ArenaRankingData>[] rankingDataListListOwn = new List<ArenaRankingData>[GroupTabList.Length];

	protected int selectedTab = GroupTabList.Length - 1;

	public override void Initialize()
	{
		object[] array = (object[])GameSection.GetEventData();
		eventData = (array[0] as Network.EventData);
		myRank = (int)array[1];
		if (eventData == null)
		{
			InitializeBase();
		}
		else
		{
			nowPage = GroupTabList.Length - 1;
			isTotalTime = true;
			FollowListBaseInitialize();
		}
	}

	public override void UpdateUI()
	{
		UpdateOwnButton();
		SetTab();
		UpdateTitle();
		if (eventData != null)
		{
			ListUI();
		}
	}

	protected virtual void UpdateTitle()
	{
		if (eventData == null)
		{
			SetLabelText(UI.LBL_ARENA_NAME, string.Empty);
			SetLabelText(UI.LBL_END_DATE, string.Empty);
		}
		else
		{
			SetLabelText(UI.LBL_ARENA_NAME, eventData.name);
			string endDateString = QuestUtility.GetEndDateString(eventData);
			SetLabelText(UI.LBL_END_DATE, endDateString);
		}
	}

	private void SetTab()
	{
		int i = 0;
		for (int num = GroupTabList.Length; i < num; i++)
		{
			Transform ctrl = GetCtrl(GroupTabList[i]);
			buttonList[i] = ctrl.GetComponent<UIButton>();
			SetEvent(ctrl, "TAB", i);
		}
	}

	protected override void SendGetList(int nowPage, Action<bool> callback)
	{
		int group = selectedTab;
		if (selectedTab >= GroupTabList.Length - 1)
		{
			group = -1;
		}
		SendGetRanking(group, callback);
	}

	protected virtual void SendGetRanking(int group, Action<bool> callback)
	{
		SendGetNormalRanking(group, callback);
	}

	private void SendGetNormalRanking(int sendGroup, Action<bool> callback)
	{
		int isContaionSelf = isOwn ? 1 : 0;
		MonoBehaviourSingleton<FriendManager>.I.SendGetArenaRanking(sendGroup, isContaionSelf, delegate(bool is_success, List<ArenaRankingData> recv_data)
		{
			if (is_success)
			{
				recvList = ChangeData(CreateFriendCharaInfoList(recv_data));
				base.rankingDataList = recv_data;
				CacheLists(recvList, recv_data);
				List<ArenaRankingData> rankingDataList = base.rankingDataList;
			}
			callback(is_success);
		});
	}

	private void OnQuery_TAB()
	{
		isOwn = false;
		if (eventData != null)
		{
			SetButtonActive(selectedTab, true);
			int num = (int)GameSection.GetEventData();
			SetButtonActive(num, false);
			selectedTab = num;
			isTotalTime = (num == GroupTabList.Length - 1);
			RefreshListAndUI(num);
		}
	}

	protected override void OnQuery_OWN()
	{
		base.OnQuery_OWN();
		RefreshListAndUI(selectedTab);
	}

	private void RefreshListAndUI(int group)
	{
		if (eventData != null)
		{
			if (IsExistCache(group))
			{
				recvList = ChangeData(GetCacheCharaList(group));
				rankingDataList = GetCacheRankingDataList(group);
				Refresh();
				DragToOwn();
			}
			else
			{
				GameSection.StayEvent();
				SendGetList(nowPage, delegate(bool b)
				{
					GameSection.ResumeEvent(b, null);
					Refresh();
					DragToOwn();
				});
			}
		}
	}

	protected virtual void CacheLists(List<FriendCharaInfo> charaList, List<ArenaRankingData> rankingDataList)
	{
		if (isOwn)
		{
			charaListListOwn[selectedTab] = charaList;
			rankingDataListListOwn[selectedTab] = rankingDataList;
		}
		else
		{
			charaListList[selectedTab] = charaList;
			rankingDataListList[selectedTab] = rankingDataList;
		}
	}

	protected virtual bool IsExistCache(int tabNum)
	{
		if (isOwn)
		{
			return charaListListOwn[tabNum] != null;
		}
		return charaListList[tabNum] != null;
	}

	protected virtual List<FriendCharaInfo> GetCacheCharaList(int tabNum)
	{
		if (isOwn)
		{
			return charaListListOwn[tabNum];
		}
		return charaListList[tabNum];
	}

	protected virtual List<ArenaRankingData> GetCacheRankingDataList(int tabNum)
	{
		if (isOwn)
		{
			return rankingDataListListOwn[tabNum];
		}
		return rankingDataListList[tabNum];
	}

	private void SetButtonActive(int index, bool isActive)
	{
		buttonList[index].isEnabled = isActive;
	}
}
