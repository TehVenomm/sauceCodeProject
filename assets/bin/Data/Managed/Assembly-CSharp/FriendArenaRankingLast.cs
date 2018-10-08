using Network;
using System;
using System.Collections.Generic;

public class FriendArenaRankingLast : FriendArenaRankingBase
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
		OBJ_OWN_ON,
		LBL_TIME_DEFAULT,
		LBL_END_DATE
	}

	private List<FriendCharaInfo>[] cacheCharaList = new List<FriendCharaInfo>[2];

	private List<ArenaRankingData>[] cacheRankingList = new List<ArenaRankingData>[2];

	protected Network.EventData lastEventData;

	public override void Initialize()
	{
		isTotalTime = true;
		base.Initialize();
	}

	private void SetArenaName(Network.EventData nameEventData)
	{
		if (nameEventData == null)
		{
			SetLabelText((Enum)UI.LBL_ARENA_NAME, string.Empty);
			SetLabelText((Enum)UI.LBL_END_DATE, string.Empty);
		}
		else
		{
			SetLabelText((Enum)UI.LBL_ARENA_NAME, nameEventData.name);
			string endDateString = QuestUtility.GetEndDateString(nameEventData);
			SetLabelText((Enum)UI.LBL_END_DATE, endDateString);
		}
	}

	private void CacheLists(List<FriendCharaInfo> charaList, List<ArenaRankingData> rankingDataList)
	{
		int num = isOwn ? 1 : 0;
		cacheCharaList[num] = charaList;
		cacheRankingList[num] = rankingDataList;
	}

	private bool IsExistCache()
	{
		int num = isOwn ? 1 : 0;
		return cacheCharaList[num] != null;
	}

	private List<FriendCharaInfo> GetCacheCharaList()
	{
		int num = isOwn ? 1 : 0;
		return cacheCharaList[num];
	}

	private List<ArenaRankingData> GetCacheRankingDataList()
	{
		int num = isOwn ? 1 : 0;
		return cacheRankingList[num];
	}

	protected override void OnQuery_OWN()
	{
		base.OnQuery_OWN();
		RefreshListAndUI();
	}

	private void RefreshListAndUI()
	{
		if (IsExistCache())
		{
			recvList = ChangeData(GetCacheCharaList());
			rankingDataList = GetCacheRankingDataList();
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

	protected override void UpdateOwnButton()
	{
		base.UpdateOwnButton();
	}

	protected override bool IsRankingJoined()
	{
		return myRank >= 1;
	}

	protected unsafe override void SendGetList(int nowPage, Action<bool> callback)
	{
		int isContaionSelf = isOwn ? 1 : 0;
		_003CSendGetList_003Ec__AnonStorey309 _003CSendGetList_003Ec__AnonStorey;
		MonoBehaviourSingleton<FriendManager>.I.SendGetLastRanking(-1, isContaionSelf, new Action<bool, ArenaLastRankingModel.Param>((object)_003CSendGetList_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	public override void OnQuery_FOLLOW_INFO()
	{
		int num = (int)GameSection.GetEventData();
		FriendCharaInfo friendCharaInfo = recvList[num];
		MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = num + 4;
		GameSection.SetEventData(new object[2]
		{
			friendCharaInfo,
			lastEventData
		});
	}
}
