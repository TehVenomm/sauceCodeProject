using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FriendArenaRankingBase : FollowListBase
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
		OBJ_OWN_ON,
		LBL_TIME_DEFAULT
	}

	protected Network.EventData eventData;

	protected List<ArenaRankingData> rankingDataList = new List<ArenaRankingData>();

	protected static readonly UI[] RankSprite = new UI[3]
	{
		UI.SPR_1,
		UI.SPR_2,
		UI.SPR_3
	};

	protected bool isOwn;

	protected int myRank;

	protected bool isTotalTime;

	private UIScrollView scrollView;

	private UIPanel scrollPanel;

	protected override string GetListItemName => "FriendArenaRankingListItem";

	public override void Initialize()
	{
		object[] array = (object[])GameSection.GetEventData();
		eventData = (array[0] as Network.EventData);
		myRank = (int)array[1];
		nowPage = 0;
		FollowListBaseInitialize();
	}

	protected void FollowListBaseInitialize()
	{
		base.Initialize();
	}

	public override void UpdateUI()
	{
		UpdateOwnButton();
		ListUI();
	}

	protected void DragToOwn()
	{
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		if (isOwn)
		{
			if (scrollView == null)
			{
				scrollView = GetCtrl(UI.SCR_LIST).GetComponent<UIScrollView>();
				scrollPanel = GetCtrl(UI.SCR_LIST).GetComponent<UIPanel>();
			}
			int num = -1;
			int i = 0;
			for (int count = rankingDataList.Count; i < count; i++)
			{
				if (rankingDataList[i].friendCharaInfo.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
				{
					num = i;
					break;
				}
			}
			if (num <= -1)
			{
				DispatchEvent("OUT_OF_RANK", null);
			}
			else if (rankingDataList.Count > 3 && num > 3)
			{
				int count2 = rankingDataList.Count;
				float num2 = (float)num / (float)count2;
				if (num2 >= 0.6f)
				{
					num += 2;
					num2 = (float)num / (float)count2;
				}
				scrollView.SetDragAmount(num2, num2, true);
				Vector2 clipOffset = scrollPanel.clipOffset;
				scrollView.get_transform().set_localPosition(Vector2.op_Implicit(-clipOffset));
			}
		}
	}

	protected override void SetListItem(int i, Transform t, bool is_recycle, FriendCharaInfo data)
	{
		base.SetListItem(i, t, is_recycle, data);
		SetRankiItem(i, t);
	}

	protected virtual void SetRankiItem(int i, Transform t)
	{
		ArenaRankingData arenaRankingData = rankingDataList[i];
		SetActive(t, UI.OBJ_COMMENT, false);
		SetActive(t, UI.OBJ_STATUS, false);
		SetTime(t, arenaRankingData.clearMilliSec);
		SetRank(t, arenaRankingData.rank);
	}

	private void SetTime(Transform t, int milliSec)
	{
		string text = QuestUtility.CreateTimeStringByMilliSec(milliSec);
		bool flag = QuestUtility.IsDefaultArenaTime(milliSec) && !isTotalTime;
		SetActive(t, UI.LBL_TIME, !flag);
		SetActive(t, UI.LBL_TIME_DEFAULT, flag);
		if (flag)
		{
			SetLabelText(t, UI.LBL_TIME_DEFAULT, text);
		}
		else
		{
			SetLabelText(t, UI.LBL_TIME, text);
		}
	}

	protected virtual void SetRank(Transform t, int rank)
	{
		int num = RankSprite.Length;
		for (int i = 0; i < num; i++)
		{
			SetActive(t, RankSprite[i], i + 1 == rank);
		}
		if (rank <= num)
		{
			SetActive(t, UI.LBL_RANK, false);
		}
		else
		{
			SetActive(t, UI.LBL_RANK, true);
			SetLabelText(t, UI.LBL_RANK, string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 5u), rank.ToString()));
		}
	}

	protected List<FriendCharaInfo> CreateFriendCharaInfoList(List<ArenaRankingData> rankingDataList)
	{
		List<FriendCharaInfo> list = new List<FriendCharaInfo>();
		int i = 0;
		for (int count = rankingDataList.Count; i < count; i++)
		{
			list.Add(rankingDataList[i].friendCharaInfo);
		}
		return list;
	}

	protected virtual void OnQuery_OWN()
	{
		isOwn = !isOwn;
	}

	public override void OnQuery_FOLLOW_INFO()
	{
		int num = (int)GameSection.GetEventData();
		FriendCharaInfo friendCharaInfo = recvList[num];
		MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = num + 4;
		GameSection.SetEventData(new object[2]
		{
			friendCharaInfo,
			eventData
		});
	}

	protected void Refresh()
	{
		SetDirty(UI.GRD_LIST);
		RefreshUI();
	}

	protected virtual void UpdateOwnButton()
	{
		SetActive((Enum)UI.BTN_OWN, IsRankingJoined());
		SetActive((Enum)UI.OBJ_OWN_ON, isOwn);
	}

	protected virtual bool IsRankingJoined()
	{
		return MonoBehaviourSingleton<UserInfoManager>.I.isJoinedArenaRanking;
	}

	private void OnQuery_FOLLOWER()
	{
		GameSection.SetEventData(new object[2]
		{
			eventData,
			myRank
		});
	}

	private void OnQuery_WORLD()
	{
		GameSection.SetEventData(new object[2]
		{
			eventData,
			myRank
		});
	}

	private void OnQuery_LAST()
	{
		GameSection.SetEventData(new object[2]
		{
			eventData,
			myRank
		});
	}

	private void OnQuery_LEGEND()
	{
		GameSection.SetEventData(new object[2]
		{
			eventData,
			myRank
		});
	}
}
