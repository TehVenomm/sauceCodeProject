using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FriendArenaRankingLegend : FriendArenaRankingBase
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
		LBL_ITEM_ARENA_NAME
	}

	private List<Network.EventData> eventDataList = new List<Network.EventData>();

	protected override string GetListItemName => "FriendArenaRankingLegendListItem";

	protected override void SetListItem(int i, Transform t, bool is_recycle, FriendCharaInfo data)
	{
		base.SetListItem(i, t, is_recycle, data);
		SetArenaName(t, i);
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		SetActive((Enum)UI.BTN_OWN, false);
		SetActive((Enum)UI.OBJ_OWN_ON, false);
	}

	protected override void SetRankiItem(int i, Transform t)
	{
		base.SetRankiItem(i, t);
		SetActive(t, UI.OBJ_STATUS, true);
		SetActive(t, UI.OBJ_COMMENT, true);
	}

	protected override void SetRank(Transform t, int rank)
	{
		int num = FriendArenaRankingBase.RankSprite.Length;
		for (int i = 0; i < num; i++)
		{
			SetActive(t, FriendArenaRankingBase.RankSprite[i], false);
		}
		SetActive(t, UI.LBL_RANK, false);
	}

	protected unsafe override void UpdateDynamicList()
	{
		FriendCharaInfo[] info = null;
		int item_num = 0;
		if (recvList != null && recvList.Count > 0)
		{
			info = recvList.ToArray();
			if (info != null)
			{
				item_num = info.Length;
			}
		}
		_003CUpdateDynamicList_003Ec__AnonStorey319 _003CUpdateDynamicList_003Ec__AnonStorey;
		SetDynamicList((Enum)UI.GRD_LIST, GetListItemName, item_num, false, null, null, new Action<int, Transform, bool>((object)_003CUpdateDynamicList_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private void SetArenaName(Transform t, int index)
	{
		SetLabelText(t, UI.LBL_ITEM_ARENA_NAME, eventDataList[index].name);
	}

	protected unsafe override void SendGetList(int page, Action<bool> callback)
	{
		_003CSendGetList_003Ec__AnonStorey31A _003CSendGetList_003Ec__AnonStorey31A;
		MonoBehaviourSingleton<FriendManager>.I.SendGetLegendRanking(new Action<bool, List<ArenaLegendRankingModel.Param>>((object)_003CSendGetList_003Ec__AnonStorey31A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	public override void OnQuery_FOLLOW_INFO()
	{
		int num = (int)GameSection.GetEventData();
		FriendCharaInfo friendCharaInfo = recvList[num];
		MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = num + 4;
		GameSection.SetEventData(new object[2]
		{
			friendCharaInfo,
			eventDataList[num]
		});
	}

	private void UpdateLists(List<ArenaLegendRankingModel.Param> result)
	{
		rankingDataList = new List<ArenaRankingData>();
		eventDataList = new List<Network.EventData>();
		List<FriendInfo> list = new List<FriendInfo>();
		int i = 0;
		for (int count = result.Count; i < count; i++)
		{
			rankingDataList.Add(result[i].rankingData);
			eventDataList.Add(result[i].eventData);
		}
	}
}
