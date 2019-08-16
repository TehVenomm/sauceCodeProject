using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class FriendCarnivalRanking : FollowListBase
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
		LBL_POINT,
		BTN_BORDER,
		BTN_TOP_PLAYER,
		BTN_FOLLOWER,
		SPR_INACTIVE_BORDER,
		SPR_INACTIVE_TOP_PLAYER,
		SPR_INACTIVE_FOLLOWER,
		SPR_TITLE_BORDER,
		SPR_TITLE_TOP_PLAYER,
		SPR_TITLE_FOLLOWER,
		OBJ_FULL_LIST,
		OBJ_BORDER_LIST,
		SCR_BORDER_LIST,
		GRD_BORDER_LIST,
		LBL_MY_NAME,
		LBL_MY_LEVEL,
		TEX_MY_MODEL,
		LBL_MY_RANK,
		LBL_MY_POINT,
		SPR_MY_BORDER,
		OBJ_MY_DEGREE_ROOT,
		LBL_BORDER_POINT,
		LBL_BORDER_LEVEL,
		LBL_BORDER_NAME,
		SPR_BORDER_ICON,
		LBL_BORDER_RANK,
		SPR_BORDER_RANK,
		STR_BORDER_NON_LIST,
		SPR_BORDER_NONE,
		SPR_BORDER_F,
		SPR_BORDER_E,
		SPR_BORDER_D,
		SPR_BORDER_C,
		SPR_BORDER_B,
		SPR_BORDER_A,
		SPR_BORDER_S,
		SPR_BORDER_SS,
		SPR_HEADER_BORDER,
		SPR_HEADER_TOP_PLAYER,
		SPR_HEADER_FOLLOWER,
		LBL_EVENT_NAME,
		SPR_BORDER_A1,
		SPR_BORDER_A2,
		SPR_BORDER_FRAME,
		SPR_BORDER_GRADE,
		SPR_BORDER_ICON_GRADE
	}

	private enum RANKING_TAB
	{
		NONE,
		BORDER,
		TOP_PLAYER,
		FOLLOWER
	}

	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct BorderIcon
	{
		public string borderIcon
		{
			get;
			private set;
		}

		public string borderFrame
		{
			get;
			private set;
		}

		public string borderGradeIcon
		{
			get;
			private set;
		}

		public bool hasGrade => !string.IsNullOrEmpty(borderGradeIcon);

		public BorderIcon(string borderIcon, string borderFrame, string borderGradeIcon)
		{
			this.borderIcon = borderIcon;
			this.borderFrame = borderFrame;
			this.borderGradeIcon = borderGradeIcon;
		}
	}

	private RANKING_TAB currentTab;

	private List<CarnivalFriendCharaInfo> borderRankInfo;

	private List<CarnivalFriendCharaInfo> topRankInfo;

	private List<CarnivalFriendCharaInfo> friendRankInfo;

	private QuestCarnivalPointModel.Param currentCarnivalData;

	private List<CarnivalFriendCharaInfo> borderInfo;

	private List<CarnivalFriendCharaInfo> topPlayerInfo;

	private List<CarnivalFriendCharaInfo> followerRankInfo;

	private List<CarnivalFriendCharaInfo> currentRankData;

	private static readonly UI[] RankUI = new UI[3]
	{
		UI.SPR_1,
		UI.SPR_2,
		UI.SPR_3
	};

	private const string BorderItemName = "FriendCarnivalBorderListItem";

	public static readonly BorderIcon[] BorderIcons = new BorderIcon[10]
	{
		new BorderIcon("RankBorderIcon_OutSideTxt", "RankBorderIcon_FFrame", null),
		new BorderIcon("RankBorderIcon_FTxt", "RankBorderIcon_FFrame", null),
		new BorderIcon("RankBorderIcon_ETxt", "RankBorderIcon_FFrame", null),
		new BorderIcon("RankBorderIcon_DTxt", "RankBorderIcon_BFrame", null),
		new BorderIcon("RankBorderIcon_CTxt", "RankBorderIcon_BFrame", null),
		new BorderIcon("RankBorderIcon_BTxt", "RankBorderIcon_BFrame", null),
		new BorderIcon("RankBorderIcon_ATxt", "RankBorderIcon_AFrame", "RankBorderIcon_IItxt"),
		new BorderIcon("RankBorderIcon_ATxt", "RankBorderIcon_AFrame", "RankBorderIcon_Itxt"),
		new BorderIcon("RankBorderIcon_STxt", "RankBorderIcon_SFrame", null),
		new BorderIcon("RankBorderIcon_SSTxt", "RankBorderIcon_SSFrame", null)
	};

	private static readonly UI[] PlayerBorderUI = new UI[10]
	{
		UI.SPR_BORDER_NONE,
		UI.SPR_BORDER_F,
		UI.SPR_BORDER_E,
		UI.SPR_BORDER_D,
		UI.SPR_BORDER_C,
		UI.SPR_BORDER_B,
		UI.SPR_BORDER_A2,
		UI.SPR_BORDER_A1,
		UI.SPR_BORDER_S,
		UI.SPR_BORDER_SS
	};

	protected override string GetListItemName => "FriendCarnivalRankingListItem";

	public override void Initialize()
	{
		nowPage = 0;
		this.StartCoroutine(DoInitialize());
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
		SetActive((Enum)UI.BTN_BORDER, is_visible: false);
		SetActive((Enum)UI.BTN_TOP_PLAYER, is_visible: true);
		SetActive((Enum)UI.BTN_FOLLOWER, is_visible: true);
		SetActive((Enum)UI.SPR_INACTIVE_BORDER, is_visible: true);
		SetActive((Enum)UI.SPR_INACTIVE_TOP_PLAYER, is_visible: false);
		SetActive((Enum)UI.SPR_INACTIVE_FOLLOWER, is_visible: false);
		currentTab = RANKING_TAB.NONE;
		ChangeTab(RANKING_TAB.BORDER);
		SetLabelText((Enum)UI.LBL_EVENT_NAME, currentCarnivalData.eventName);
		isInitializeSend = false;
		base.Initialize();
	}

	private void SetSelfInfo()
	{
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		UserInfo userInfo = MonoBehaviourSingleton<UserInfoManager>.I.userInfo;
		UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		SetLabelText((Enum)UI.LBL_MY_NAME, userInfo.name);
		SetLabelText((Enum)UI.LBL_MY_LEVEL, userStatus.level.ToString());
		int num = RankUI.Length;
		int rank = currentCarnivalData.rank;
		for (int i = 0; i < num; i++)
		{
			SetActive((Enum)RankUI[i], i + 1 == rank);
		}
		SetActive((Enum)UI.LBL_MY_RANK, rank > num);
		SetLabelText((Enum)UI.LBL_MY_RANK, "Rank " + rank.ToString());
		int point = currentCarnivalData.point;
		SetLabelText((Enum)UI.LBL_MY_POINT, point.ToString("N0") + " pt");
		int num2 = PlayerBorderUI.Length;
		int border = currentCarnivalData.border;
		for (int j = 0; j < num2; j++)
		{
			SetActive((Enum)PlayerBorderUI[j], j == border);
		}
		ForceSetRenderPlayerModel((Enum)UI.TEX_MY_MODEL, PlayerLoadInfo.FromUserStatus(need_weapon: false, is_priority_visual_equip: true), 99, new Vector3(0f, -1.536f, 1.87f), new Vector3(0f, 154f, 0f), is_priority_visual_equip: true, (Action<PlayerLoader>)null);
	}

	private void ChangeTab(RANKING_TAB target)
	{
		if (target != currentTab)
		{
			bool flag = target == RANKING_TAB.BORDER;
			SetActive((Enum)UI.OBJ_FULL_LIST, !flag);
			SetActive((Enum)UI.OBJ_BORDER_LIST, flag);
			ResetScroll(flag);
			ChangeRankingTabButtonEnabled(target);
			ChangeTitle(target);
			ChangeRankData(target);
			SetActive((Enum)UI.SPR_HEADER_BORDER, target == RANKING_TAB.BORDER);
			SetActive((Enum)UI.SPR_HEADER_TOP_PLAYER, target == RANKING_TAB.TOP_PLAYER);
			SetActive((Enum)UI.SPR_HEADER_FOLLOWER, target == RANKING_TAB.FOLLOWER);
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
			SetActive((Enum)UI.BTN_BORDER, is_visible: true);
			SetActive((Enum)UI.SPR_INACTIVE_BORDER, is_visible: false);
		}
		else if (currentTab == RANKING_TAB.TOP_PLAYER)
		{
			SetActive((Enum)UI.BTN_TOP_PLAYER, is_visible: true);
			SetActive((Enum)UI.SPR_INACTIVE_TOP_PLAYER, is_visible: false);
		}
		else if (currentTab == RANKING_TAB.FOLLOWER)
		{
			SetActive((Enum)UI.BTN_FOLLOWER, is_visible: true);
			SetActive((Enum)UI.SPR_INACTIVE_FOLLOWER, is_visible: false);
		}
		switch (target)
		{
		case RANKING_TAB.BORDER:
			SetActive((Enum)UI.BTN_BORDER, is_visible: false);
			SetActive((Enum)UI.SPR_INACTIVE_BORDER, is_visible: true);
			break;
		case RANKING_TAB.TOP_PLAYER:
			SetActive((Enum)UI.BTN_TOP_PLAYER, is_visible: false);
			SetActive((Enum)UI.SPR_INACTIVE_TOP_PLAYER, is_visible: true);
			break;
		case RANKING_TAB.FOLLOWER:
			SetActive((Enum)UI.BTN_FOLLOWER, is_visible: false);
			SetActive((Enum)UI.SPR_INACTIVE_FOLLOWER, is_visible: true);
			break;
		}
	}

	private void ChangeTitle(RANKING_TAB target)
	{
		SetActive((Enum)UI.SPR_TITLE_BORDER, is_visible: false);
		SetActive((Enum)UI.SPR_TITLE_TOP_PLAYER, is_visible: false);
		SetActive((Enum)UI.SPR_TITLE_FOLLOWER, is_visible: false);
		switch (target)
		{
		case RANKING_TAB.BORDER:
			SetActive((Enum)UI.SPR_TITLE_BORDER, is_visible: true);
			break;
		case RANKING_TAB.TOP_PLAYER:
			SetActive((Enum)UI.SPR_TITLE_TOP_PLAYER, is_visible: true);
			break;
		default:
			SetActive((Enum)UI.SPR_TITLE_FOLLOWER, is_visible: true);
			break;
		}
	}

	private void ResetScroll(bool isBorder)
	{
		UIScrollView uIScrollView = null;
		uIScrollView = ((!isBorder) ? GetCtrl(UI.SCR_LIST).GetComponent<UIScrollView>() : GetCtrl(UI.SCR_BORDER_LIST).GetComponent<UIScrollView>());
		if (uIScrollView != null)
		{
			uIScrollView.set_enabled(true);
			uIScrollView.ResetPosition();
		}
	}

	private void SetRankItem(int i, Transform t)
	{
		SetActive(t, UI.OBJ_COMMENT, is_visible: false);
		SetActive(t, UI.OBJ_STATUS, is_visible: false);
		CarnivalFriendCharaInfo carnivalFriendCharaInfo = currentRankData[i];
		SetPoint(t, carnivalFriendCharaInfo.point);
		SetRank(t, carnivalFriendCharaInfo.rank);
		SetBorder(t, carnivalFriendCharaInfo.border);
	}

	private void SetBorder(Transform t, int border)
	{
		int num = PlayerBorderUI.Length;
		for (int i = 0; i < num; i++)
		{
			SetActive(t, PlayerBorderUI[i], i == border);
		}
	}

	private void SetPoint(Transform t, int point)
	{
		SetLabelText(t, UI.LBL_POINT, point.ToString("N0") + " pt");
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
		DegreePlate component = GetCtrl(UI.OBJ_MY_DEGREE_ROOT).GetComponent<DegreePlate>();
		component.Initialize(MonoBehaviourSingleton<UserInfoManager>.I.selectedDegreeIds, isButton: false, null);
		SetDynamicList((Enum)UI.GRD_BORDER_LIST, "FriendCarnivalBorderListItem", currentRankData.Count, reset: false, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycle)
		{
			SetBorderItem(i, t, currentRankData[i]);
		});
		SetActive((Enum)UI.STR_BORDER_NON_LIST, currentRankData.Count <= 0);
		ResetScroll(isBorder: true);
	}

	private void SetBorderItem(int i, Transform t, CarnivalFriendCharaInfo info)
	{
		SetBorderIcon(t, BorderIcons[info.border]);
		SetLabelText(t, UI.LBL_BORDER_NAME, info.name);
		SetLabelText(t, UI.LBL_BORDER_POINT, info.point.ToString("N0") + " pt");
		SetLabelText(t, UI.LBL_BORDER_LEVEL, info.level.ToString());
		SetLabelText(t, UI.LBL_BORDER_RANK, "Rank " + info.rank.ToString());
	}

	private void SetBorderIcon(Transform t, BorderIcon borderIcon)
	{
		if (borderIcon.hasGrade)
		{
			SetActive(t, UI.SPR_BORDER_ICON, is_visible: false);
			SetActive(t, UI.SPR_BORDER_ICON_GRADE, is_visible: true);
			SetSprite(t, UI.SPR_BORDER_ICON_GRADE, borderIcon.borderIcon);
			SetSprite(t, UI.SPR_BORDER_FRAME, borderIcon.borderFrame);
			SetSprite(t, UI.SPR_BORDER_GRADE, borderIcon.borderGradeIcon);
		}
		else
		{
			SetActive(t, UI.SPR_BORDER_ICON, is_visible: true);
			SetActive(t, UI.SPR_BORDER_ICON_GRADE, is_visible: false);
			SetSprite(t, UI.SPR_BORDER_ICON, borderIcon.borderIcon);
			SetSprite(t, UI.SPR_BORDER_FRAME, borderIcon.borderFrame);
		}
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
			this.StartCoroutine(GetTopPlayerInfo(delegate
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

	private void OnQuery_FOLLOWER()
	{
		if (followerRankInfo == null)
		{
			GameSection.StayEvent();
			this.StartCoroutine(GetFollowerInfo(delegate
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
		Protocol.Send<QuestCarnivalPointModel.RequestSendForm, QuestCarnivalPointModel>(postData: new QuestCarnivalPointModel.RequestSendForm
		{
			eid = MonoBehaviourSingleton<QuestManager>.I.carnivalEventId
		}, url: QuestCarnivalPointModel.URL, callBack: (Action<QuestCarnivalPointModel>)delegate(QuestCarnivalPointModel result)
		{
			isRequest = false;
			currentCarnivalData = result.result;
		}, getParam: string.Empty);
		while (isRequest)
		{
			yield return null;
		}
	}

	private IEnumerator GetBorderInfo()
	{
		bool isRequest = true;
		Protocol.Send(CarnivalBorderRankingModel.URL, null, delegate(CarnivalBorderRankingModel result)
		{
			isRequest = false;
			borderInfo = result.result;
		}, string.Empty);
		while (isRequest)
		{
			yield return null;
		}
	}

	private IEnumerator GetTopPlayerInfo(Action callBack)
	{
		bool isRequest = true;
		Protocol.Send<CarnivalTopRankingModel.RequestSendForm, CarnivalTopRankingModel>(postData: new CarnivalTopRankingModel.RequestSendForm
		{
			num = 100
		}, url: CarnivalTopRankingModel.URL, callBack: (Action<CarnivalTopRankingModel>)delegate(CarnivalTopRankingModel result)
		{
			isRequest = false;
			topPlayerInfo = result.result;
		}, getParam: string.Empty);
		while (isRequest)
		{
			yield return null;
		}
		callBack();
	}

	private IEnumerator GetFollowerInfo(Action callBack)
	{
		bool isRequest = true;
		Protocol.Send(CarnivalFriendRankingModel.URL, null, delegate(CarnivalFriendRankingModel result)
		{
			isRequest = false;
			followerRankInfo = result.result;
		}, string.Empty);
		while (isRequest)
		{
			yield return null;
		}
		callBack();
	}
}
