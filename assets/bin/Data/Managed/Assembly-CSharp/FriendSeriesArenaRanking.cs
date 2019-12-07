using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendSeriesArenaRanking : FollowListBase
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
		LBL_TIME,
		LBL_ARENA_NAME,
		SPR_1,
		SPR_2,
		SPR_3,
		LBL_RANK,
		SCR_LIST,
		BTN_OWN,
		OBJ_OWN_ON,
		LBL_CLEAR_TIME,
		LBL_MONSTER_LV,
		BTN_BORDER,
		BTN_TOP_PLAYER,
		BTN_FOLLOWER,
		SPR_INACTIVE_BORDER,
		SPR_INACTIVE_TOP_PLAYER,
		SPR_INACTIVE_FOLLOWER,
		SPR_TITLE_BORDER,
		SPR_TITLE_TOP_PLAYER,
		SPR_TITLE_FOLLOWER,
		LBL_BORDER_POINT,
		LBL_BORDER_LEVEL,
		LBL_BORDER_NAME,
		SPR_BORDER_ICON,
		LBL_BORDER_RANK,
		SPR_BORDER_NONE,
		SPR_BORDER_C,
		SPR_BORDER_B,
		SPR_BORDER_A,
		SPR_BORDER_S,
		SPR_BORDER_SS,
		OBJ_FULL_LIST,
		OBJ_BORDER_LIST,
		SCR_BORDER_LIST,
		GRD_BORDER_LIST,
		STR_BORDER_NON_LIST,
		SPR_HEADER_BORDER,
		SPR_HEADER_TOP_PLAYER,
		SPR_HEADER_FOLLOWER,
		SPR_BORDER_FRAME,
		SPR_BORDER_GRADE,
		SPR_BORDER_ICON_GRADE,
		LBL_EVENT_NAME,
		ROOT_MYSCORE
	}

	private enum RANKING_TAB
	{
		NONE,
		BORDER,
		TOP_PLAYER,
		FOLLOWER
	}

	private RANKING_TAB currentTab;

	private QuestCarnivalPointModel.Param currentCarnivalData;

	private List<CarnivalFriendCharaInfo> topPlayerInfo;

	private List<CarnivalFriendCharaInfo> followerRankInfo;

	private List<CarnivalFriendCharaInfo> borderInfo;

	private List<CarnivalFriendCharaInfo> currentRankData;

	private static readonly UI[] RankUI = new UI[3]
	{
		UI.SPR_1,
		UI.SPR_2,
		UI.SPR_3
	};

	private EventListData eventData;

	private const string BorderItemName = "FriendSeriesArenaBorderListItem";

	protected override string GetListItemName => "FriendSeriesArenaRankingListItem";

	public override void Initialize()
	{
		nowPage = 0;
		eventData = MonoBehaviourSingleton<DeliveryManager>.I.FindSeriesArenaTopData();
		StartCoroutine(DoInitialize());
	}

	public override void UpdateUI()
	{
		if (currentTab != RANKING_TAB.BORDER)
		{
			ListUI();
			if (currentRankData.IsNullOrEmpty())
			{
				UpdateDynamicList();
			}
			ResetScroll(isBorder: false);
		}
		else
		{
			UpdateBorderList();
		}
	}

	protected override FriendCharaInfo[] GetCurrentUserArray()
	{
		if (currentRankData == null)
		{
			return null;
		}
		List<FriendCharaInfo> list = new List<FriendCharaInfo>();
		for (int i = 0; i < currentRankData.Count; i++)
		{
			list.Add(currentRankData[i]);
		}
		recvList = list;
		return base.GetCurrentUserArray();
	}

	protected override List<FriendCharaInfo> GetCurrentUserList()
	{
		List<FriendCharaInfo> list = new List<FriendCharaInfo>();
		for (int i = 0; i < currentRankData.Count; i++)
		{
			list.Add(currentRankData[i]);
		}
		recvList = list;
		return recvList;
	}

	protected override void SetListItem(int i, Transform t, bool is_recycle, FriendCharaInfo data)
	{
		base.SetListItem(i, t, is_recycle, data);
		SetRankItem(i, t);
	}

	private IEnumerator DoInitialize()
	{
		yield return GetCurrentCarnivalStatus();
		yield return GetBorderInfo();
		SetSelfInfo();
		SetActive(UI.BTN_BORDER, is_visible: false);
		SetActive(UI.BTN_TOP_PLAYER, is_visible: true);
		SetActive(UI.BTN_FOLLOWER, is_visible: true);
		SetActive(UI.SPR_INACTIVE_BORDER, is_visible: true);
		SetActive(UI.SPR_INACTIVE_TOP_PLAYER, is_visible: false);
		SetActive(UI.SPR_INACTIVE_FOLLOWER, is_visible: false);
		currentTab = RANKING_TAB.NONE;
		ChangeTab(RANKING_TAB.BORDER);
		SetLabelText(UI.LBL_EVENT_NAME, currentCarnivalData.eventName);
		isInitializeSend = false;
		base.Initialize();
	}

	private void SetSelfInfo()
	{
		UserInfo userInfo = MonoBehaviourSingleton<UserInfoManager>.I.userInfo;
		UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		Transform ctrl = GetCtrl(UI.ROOT_MYSCORE);
		SetLabelText(ctrl, UI.LBL_NAME, userInfo.name);
		SetLabelText(ctrl, UI.LBL_LEVEL, userStatus.level.ToString());
		SetClearTime(ctrl, currentCarnivalData.clearTime);
		SetRank(ctrl, currentCarnivalData.rank);
		SetMonsteLv(ctrl, currentCarnivalData.maxLevel);
		ForceSetRenderPlayerModel(ctrl, UI.TEX_MODEL, PlayerLoadInfo.FromUserStatus(need_weapon: false, is_priority_visual_equip: true), 99, new Vector3(0f, -1.536f, 1.87f), new Vector3(0f, 154f, 0f), is_priority_visual_equip: true);
	}

	private void ChangeTab(RANKING_TAB target)
	{
		if (target != currentTab)
		{
			bool flag = target == RANKING_TAB.BORDER;
			SetActive(UI.OBJ_FULL_LIST, !flag);
			SetActive(UI.OBJ_BORDER_LIST, flag);
			ResetScroll(flag);
			ChangeRankingTabButtonEnabled(target);
			ChangeTitle(target);
			ChangeRankData(target);
			SetActive(UI.SPR_HEADER_BORDER, target == RANKING_TAB.BORDER);
			SetActive(UI.SPR_HEADER_TOP_PLAYER, target == RANKING_TAB.TOP_PLAYER);
			SetActive(UI.SPR_HEADER_FOLLOWER, target == RANKING_TAB.FOLLOWER);
			currentTab = target;
		}
	}

	private void ChangeRankData(RANKING_TAB target)
	{
		switch (target)
		{
		case RANKING_TAB.BORDER:
			currentRankData = borderInfo;
			break;
		case RANKING_TAB.TOP_PLAYER:
			currentRankData = topPlayerInfo;
			break;
		default:
			currentRankData = followerRankInfo;
			break;
		}
	}

	private void ChangeRankingTabButtonEnabled(RANKING_TAB target)
	{
		if (currentTab == RANKING_TAB.BORDER)
		{
			SetActive(UI.BTN_BORDER, is_visible: true);
			SetActive(UI.SPR_INACTIVE_BORDER, is_visible: false);
		}
		else if (currentTab == RANKING_TAB.TOP_PLAYER)
		{
			SetActive(UI.BTN_TOP_PLAYER, is_visible: true);
			SetActive(UI.SPR_INACTIVE_TOP_PLAYER, is_visible: false);
		}
		else if (currentTab == RANKING_TAB.FOLLOWER)
		{
			SetActive(UI.BTN_FOLLOWER, is_visible: true);
			SetActive(UI.SPR_INACTIVE_FOLLOWER, is_visible: false);
		}
		switch (target)
		{
		case RANKING_TAB.BORDER:
			SetActive(UI.BTN_BORDER, is_visible: false);
			SetActive(UI.SPR_INACTIVE_BORDER, is_visible: true);
			break;
		case RANKING_TAB.TOP_PLAYER:
			SetActive(UI.BTN_TOP_PLAYER, is_visible: false);
			SetActive(UI.SPR_INACTIVE_TOP_PLAYER, is_visible: true);
			break;
		case RANKING_TAB.FOLLOWER:
			SetActive(UI.BTN_FOLLOWER, is_visible: false);
			SetActive(UI.SPR_INACTIVE_FOLLOWER, is_visible: true);
			break;
		}
	}

	private void ChangeTitle(RANKING_TAB target)
	{
		SetActive(UI.SPR_TITLE_BORDER, is_visible: false);
		SetActive(UI.SPR_TITLE_TOP_PLAYER, is_visible: false);
		SetActive(UI.SPR_TITLE_FOLLOWER, is_visible: false);
		switch (target)
		{
		case RANKING_TAB.BORDER:
			SetActive(UI.SPR_TITLE_BORDER, is_visible: true);
			break;
		case RANKING_TAB.TOP_PLAYER:
			SetActive(UI.SPR_TITLE_TOP_PLAYER, is_visible: true);
			break;
		default:
			SetActive(UI.SPR_TITLE_FOLLOWER, is_visible: true);
			break;
		}
	}

	private void ResetScroll(bool isBorder)
	{
		UIScrollView uIScrollView = null;
		uIScrollView = ((!isBorder) ? GetCtrl(UI.SCR_LIST).GetComponent<UIScrollView>() : GetCtrl(UI.SCR_BORDER_LIST).GetComponent<UIScrollView>());
		if (uIScrollView != null)
		{
			uIScrollView.enabled = true;
			uIScrollView.ResetPosition();
		}
	}

	private void SetRankItem(int i, Transform t)
	{
		SetActive(t, UI.OBJ_COMMENT, is_visible: false);
		CarnivalFriendCharaInfo carnivalFriendCharaInfo = currentRankData[i];
		SetClearTime(t, carnivalFriendCharaInfo.clearTime);
		SetRank(t, carnivalFriendCharaInfo.rank);
		SetMonsteLv(t, carnivalFriendCharaInfo.maxLevel);
	}

	private void SetMonsteLv(Transform t, int monster_lv)
	{
		SetLabelText(t, UI.LBL_MONSTER_LV, monster_lv.ToString("N0"));
	}

	private void SetClearTime(Transform t, int clearTime)
	{
		string text = QuestUtility.CreateTimeStringByMilliSecSeriesArena(clearTime);
		SetLabelText(t, UI.LBL_CLEAR_TIME, text);
	}

	private void SetRank(Transform t, int rank)
	{
		int num = RankUI.Length;
		for (int i = 0; i < num; i++)
		{
			SetActive(t, RankUI[i], i + 1 == rank);
		}
		if (rank <= num)
		{
			SetActive(t, UI.LBL_RANK, is_visible: false);
			return;
		}
		SetActive(t, UI.LBL_RANK, is_visible: true);
		SetLabelText(t, UI.LBL_RANK, "Rank " + rank.ToString());
	}

	private void UpdateBorderList()
	{
		GetCtrl(UI.OBJ_DEGREE_FRAME_ROOT).GetComponent<DegreePlate>().Initialize(MonoBehaviourSingleton<UserInfoManager>.I.selectedDegreeIds, isButton: false, null);
		SetDynamicList(UI.GRD_BORDER_LIST, "FriendSeriesArenaBorderListItem", currentRankData.Count, reset: false, null, null, delegate(int i, Transform t, bool is_recycle)
		{
			SetBorderItem(i, t, currentRankData[i]);
		});
		SetActive(UI.STR_BORDER_NON_LIST, currentRankData.Count <= 0);
		ResetScroll(isBorder: true);
	}

	private void SetBorderItem(int i, Transform t, CarnivalFriendCharaInfo info)
	{
		SetLabelText(t, UI.LBL_NAME, info.name);
		SetClearTime(t, info.clearTime);
		SetRank(t, info.rank);
		SetMonsteLv(t, info.maxLevel);
		SetLabelText(t, UI.LBL_LEVEL, info.level.ToString());
	}

	private void OnQuery_BORDER()
	{
		ChangeTab(RANKING_TAB.BORDER);
	}

	private void OnQuery_TOP_PLAYER()
	{
		if (topPlayerInfo == null)
		{
			GameSection.StayEvent();
			StartCoroutine(GetTopPlayerInfo(delegate
			{
				ChangeTab(RANKING_TAB.TOP_PLAYER);
				GameSection.ResumeEvent(is_resume: true);
				RefreshUI();
			}));
		}
		else
		{
			ChangeTab(RANKING_TAB.TOP_PLAYER);
			RefreshUI();
		}
	}

	protected virtual void OnQuery_INFO_REWARD()
	{
		GameSection.SetEventData(string.Format(WebViewManager.NewsWithLinkParamFormat, eventData.linkName));
	}

	private void OnQuery_FOLLOWER()
	{
		if (followerRankInfo == null)
		{
			GameSection.StayEvent();
			StartCoroutine(GetFollowerInfo(delegate
			{
				ChangeTab(RANKING_TAB.FOLLOWER);
				GameSection.ResumeEvent(is_resume: true);
				RefreshUI();
			}));
		}
		else
		{
			ChangeTab(RANKING_TAB.FOLLOWER);
			RefreshUI();
		}
	}

	private IEnumerator GetCurrentCarnivalStatus()
	{
		bool isRequest = true;
		QuestCarnivalPointModel.RequestSendForm requestSendForm = new QuestCarnivalPointModel.RequestSendForm();
		requestSendForm.eid = MonoBehaviourSingleton<DeliveryManager>.I.FindSeriesArenaTopData().eventId;
		Protocol.Send(QuestCarnivalPointModel.URL, requestSendForm, delegate(QuestCarnivalPointModel result)
		{
			isRequest = false;
			currentCarnivalData = result.result;
		});
		while (isRequest)
		{
			yield return null;
		}
	}

	private IEnumerator GetTopPlayerInfo(Action callBack)
	{
		bool isRequest = true;
		CarnivalTopRankingModel.RequestSendForm requestSendForm = new CarnivalTopRankingModel.RequestSendForm();
		requestSendForm.num = 100;
		requestSendForm.sa = 1;
		Protocol.Send(CarnivalTopRankingModel.URL, requestSendForm, delegate(CarnivalTopRankingModel result)
		{
			isRequest = false;
			topPlayerInfo = result.result;
		});
		while (isRequest)
		{
			yield return null;
		}
		callBack();
	}

	private IEnumerator GetFollowerInfo(Action callBack)
	{
		bool isRequest = true;
		CarnivalFriendRankingModel.RequestSendForm requestSendForm = new CarnivalFriendRankingModel.RequestSendForm();
		requestSendForm.sa = 1;
		Protocol.Send(CarnivalFriendRankingModel.URL, requestSendForm, delegate(CarnivalFriendRankingModel result)
		{
			isRequest = false;
			followerRankInfo = result.result;
		});
		while (isRequest)
		{
			yield return null;
		}
		callBack();
	}

	private IEnumerator GetBorderInfo()
	{
		bool isRequest = true;
		CarnivalBorderRankingModel.RequestSendForm requestSendForm = new CarnivalBorderRankingModel.RequestSendForm();
		requestSendForm.sa = 1;
		Protocol.Send(CarnivalBorderRankingModel.URL, requestSendForm, delegate(CarnivalBorderRankingModel result)
		{
			isRequest = false;
			borderInfo = result.result;
		});
		while (isRequest)
		{
			yield return null;
		}
	}
}
