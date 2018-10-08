using Network;
using System;
using System.Collections.Generic;

public class FriendArenaRankingFriend : FriendArenaRanking
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
		BTN_TAB_A,
		BTN_TAB_B,
		BTN_TAB_C,
		BTN_TAB_D,
		BTN_TAB_E,
		BTN_TAB_TOTAL,
		LBL_END_DATE
	}

	protected override void UpdateOwnButton()
	{
		base.UpdateOwnButton();
		if (eventData != null && (recvList == null || recvList.Count <= 4))
		{
			SetActive((Enum)UI.BTN_OWN, false);
			SetActive((Enum)UI.OBJ_OWN_OFF, false);
		}
	}

	protected override void SendGetRanking(int group, Action<bool> callback)
	{
		SendGetFriendRanking(group, callback);
	}

	private void SendGetFriendRanking(int sendGroup, Action<bool> callback)
	{
		MonoBehaviourSingleton<FriendManager>.I.SendGetFriendRanking(sendGroup, 0, delegate(bool is_success, List<ArenaRankingData> recv_data)
		{
			if (is_success)
			{
				recvList = ChangeData(CreateFriendCharaInfoList(recv_data));
				CacheLists(recvList, recv_data);
				rankingDataList = recv_data;
			}
			callback(is_success);
		});
	}

	protected override void CacheLists(List<FriendCharaInfo> charaList, List<ArenaRankingData> rankingDataList)
	{
		charaListList[selectedTab] = charaList;
		rankingDataListList[selectedTab] = rankingDataList;
	}

	protected override bool IsExistCache(int tabNum)
	{
		return charaListList[tabNum] != null;
	}

	protected override List<FriendCharaInfo> GetCacheCharaList(int tabNum)
	{
		return charaListList[tabNum];
	}

	protected override List<ArenaRankingData> GetCacheRankingDataList(int tabNum)
	{
		return rankingDataListList[tabNum];
	}
}
