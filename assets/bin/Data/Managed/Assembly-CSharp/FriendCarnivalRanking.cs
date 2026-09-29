// Decompiled with JetBrains decompiler
// Type: FriendCarnivalRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FriendCarnivalRanking : FollowListBase
{
  private FriendCarnivalRanking.RANKING_TAB currentTab;
  private List<CarnivalFriendCharaInfo> borderRankInfo;
  private List<CarnivalFriendCharaInfo> topRankInfo;
  private List<CarnivalFriendCharaInfo> friendRankInfo;
  private QuestCarnivalPointModel.Param currentCarnivalData;
  private List<CarnivalFriendCharaInfo> borderInfo;
  private List<CarnivalFriendCharaInfo> topPlayerInfo;
  private List<CarnivalFriendCharaInfo> followerRankInfo;
  private List<CarnivalFriendCharaInfo> currentRankData;
  private static readonly FriendCarnivalRanking.UI[] RankUI = new FriendCarnivalRanking.UI[3]
  {
    FriendCarnivalRanking.UI.SPR_1,
    FriendCarnivalRanking.UI.SPR_2,
    FriendCarnivalRanking.UI.SPR_3
  };
  private const string BorderItemName = "FriendCarnivalBorderListItem";
  public static readonly FriendCarnivalRanking.BorderIcon[] BorderIcons = new FriendCarnivalRanking.BorderIcon[10]
  {
    new FriendCarnivalRanking.BorderIcon("RankBorderIcon_OutSideTxt", "RankBorderIcon_FFrame", (string) null),
    new FriendCarnivalRanking.BorderIcon("RankBorderIcon_FTxt", "RankBorderIcon_FFrame", (string) null),
    new FriendCarnivalRanking.BorderIcon("RankBorderIcon_ETxt", "RankBorderIcon_FFrame", (string) null),
    new FriendCarnivalRanking.BorderIcon("RankBorderIcon_DTxt", "RankBorderIcon_BFrame", (string) null),
    new FriendCarnivalRanking.BorderIcon("RankBorderIcon_CTxt", "RankBorderIcon_BFrame", (string) null),
    new FriendCarnivalRanking.BorderIcon("RankBorderIcon_BTxt", "RankBorderIcon_BFrame", (string) null),
    new FriendCarnivalRanking.BorderIcon("RankBorderIcon_ATxt", "RankBorderIcon_AFrame", "RankBorderIcon_IItxt"),
    new FriendCarnivalRanking.BorderIcon("RankBorderIcon_ATxt", "RankBorderIcon_AFrame", "RankBorderIcon_Itxt"),
    new FriendCarnivalRanking.BorderIcon("RankBorderIcon_STxt", "RankBorderIcon_SFrame", (string) null),
    new FriendCarnivalRanking.BorderIcon("RankBorderIcon_SSTxt", "RankBorderIcon_SSFrame", (string) null)
  };
  private static readonly FriendCarnivalRanking.UI[] PlayerBorderUI = new FriendCarnivalRanking.UI[10]
  {
    FriendCarnivalRanking.UI.SPR_BORDER_NONE,
    FriendCarnivalRanking.UI.SPR_BORDER_F,
    FriendCarnivalRanking.UI.SPR_BORDER_E,
    FriendCarnivalRanking.UI.SPR_BORDER_D,
    FriendCarnivalRanking.UI.SPR_BORDER_C,
    FriendCarnivalRanking.UI.SPR_BORDER_B,
    FriendCarnivalRanking.UI.SPR_BORDER_A2,
    FriendCarnivalRanking.UI.SPR_BORDER_A1,
    FriendCarnivalRanking.UI.SPR_BORDER_S,
    FriendCarnivalRanking.UI.SPR_BORDER_SS
  };

  public override void Initialize()
  {
    this.nowPage = 0;
    this.StartCoroutine(this.DoInitialize());
  }

  public override void UpdateUI()
  {
    if (this.currentTab != FriendCarnivalRanking.RANKING_TAB.BORDER)
    {
      this.ListUI();
      if (this.currentRankData.IsNullOrEmpty<CarnivalFriendCharaInfo>())
        this.UpdateDynamicList();
      this.ResetScroll(false);
    }
    else
      this.UpdateBorderList();
  }

  protected override FriendCharaInfo[] GetCurrentUserArray()
  {
    if (this.currentRankData == null)
      return (FriendCharaInfo[]) null;
    List<FriendCharaInfo> friendCharaInfoList = new List<FriendCharaInfo>();
    for (int index = 0; index < this.currentRankData.Count; ++index)
      friendCharaInfoList.Add((FriendCharaInfo) this.currentRankData[index]);
    this.recvList = friendCharaInfoList;
    return base.GetCurrentUserArray();
  }

  protected override List<FriendCharaInfo> GetCurrentUserList()
  {
    List<FriendCharaInfo> friendCharaInfoList = new List<FriendCharaInfo>();
    for (int index = 0; index < this.currentRankData.Count; ++index)
      friendCharaInfoList.Add((FriendCharaInfo) this.currentRankData[index]);
    this.recvList = friendCharaInfoList;
    return this.recvList;
  }

  protected override void SetListItem(int i, Transform t, bool is_recycle, FriendCharaInfo data)
  {
    base.SetListItem(i, t, is_recycle, data);
    this.SetRankItem(i, t);
  }

  protected override string GetListItemName => "FriendCarnivalRankingListItem";

  private IEnumerator DoInitialize()
  {
    yield return (object) this.GetCurrentCarnivalStatus();
    yield return (object) this.GetBorderInfo();
    this.SetSelfInfo();
    this.SetActive((Enum) FriendCarnivalRanking.UI.BTN_BORDER, false);
    this.SetActive((Enum) FriendCarnivalRanking.UI.BTN_TOP_PLAYER, true);
    this.SetActive((Enum) FriendCarnivalRanking.UI.BTN_FOLLOWER, true);
    this.SetActive((Enum) FriendCarnivalRanking.UI.SPR_INACTIVE_BORDER, true);
    this.SetActive((Enum) FriendCarnivalRanking.UI.SPR_INACTIVE_TOP_PLAYER, false);
    this.SetActive((Enum) FriendCarnivalRanking.UI.SPR_INACTIVE_FOLLOWER, false);
    this.currentTab = FriendCarnivalRanking.RANKING_TAB.NONE;
    this.ChangeTab(FriendCarnivalRanking.RANKING_TAB.BORDER);
    this.SetLabelText((Enum) FriendCarnivalRanking.UI.LBL_EVENT_NAME, this.currentCarnivalData.eventName);
    this.isInitializeSend = false;
    base.Initialize();
  }

  private void SetSelfInfo()
  {
    Network.UserInfo userInfo = MonoBehaviourSingleton<UserInfoManager>.I.userInfo;
    UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
    this.SetLabelText((Enum) FriendCarnivalRanking.UI.LBL_MY_NAME, userInfo.name);
    this.SetLabelText((Enum) FriendCarnivalRanking.UI.LBL_MY_LEVEL, userStatus.level.ToString());
    int length1 = FriendCarnivalRanking.RankUI.Length;
    int rank = this.currentCarnivalData.rank;
    for (int index = 0; index < length1; ++index)
      this.SetActive((Enum) FriendCarnivalRanking.RankUI[index], index + 1 == rank);
    this.SetActive((Enum) FriendCarnivalRanking.UI.LBL_MY_RANK, rank > length1);
    this.SetLabelText((Enum) FriendCarnivalRanking.UI.LBL_MY_RANK, "Rank " + rank.ToString());
    this.SetLabelText((Enum) FriendCarnivalRanking.UI.LBL_MY_POINT, this.currentCarnivalData.point.ToString("N0") + " pt");
    int length2 = FriendCarnivalRanking.PlayerBorderUI.Length;
    int border = this.currentCarnivalData.border;
    for (int index = 0; index < length2; ++index)
      this.SetActive((Enum) FriendCarnivalRanking.PlayerBorderUI[index], index == border);
    this.ForceSetRenderPlayerModel((Enum) FriendCarnivalRanking.UI.TEX_MY_MODEL, PlayerLoadInfo.FromUserStatus(false, true), 99, new Vector3(0.0f, -1.536f, 1.87f), new Vector3(0.0f, 154f, 0.0f), true);
  }

  private void ChangeTab(FriendCarnivalRanking.RANKING_TAB target)
  {
    if (target == this.currentTab)
      return;
    bool flag = target == FriendCarnivalRanking.RANKING_TAB.BORDER;
    this.SetActive((Enum) FriendCarnivalRanking.UI.OBJ_FULL_LIST, !flag);
    this.SetActive((Enum) FriendCarnivalRanking.UI.OBJ_BORDER_LIST, flag);
    this.ResetScroll(flag);
    this.ChangeRankingTabButtonEnabled(target);
    this.ChangeTitle(target);
    this.ChangeRankData(target);
    this.SetActive((Enum) FriendCarnivalRanking.UI.SPR_HEADER_BORDER, target == FriendCarnivalRanking.RANKING_TAB.BORDER);
    this.SetActive((Enum) FriendCarnivalRanking.UI.SPR_HEADER_TOP_PLAYER, target == FriendCarnivalRanking.RANKING_TAB.TOP_PLAYER);
    this.SetActive((Enum) FriendCarnivalRanking.UI.SPR_HEADER_FOLLOWER, target == FriendCarnivalRanking.RANKING_TAB.FOLLOWER);
    this.currentTab = target;
  }

  private void ChangeRankData(FriendCarnivalRanking.RANKING_TAB target)
  {
    if (target == FriendCarnivalRanking.RANKING_TAB.BORDER)
      this.currentRankData = this.borderInfo;
    else if (target == FriendCarnivalRanking.RANKING_TAB.TOP_PLAYER)
      this.currentRankData = this.topPlayerInfo;
    else
      this.currentRankData = this.followerRankInfo;
  }

  private void ChangeRankingTabButtonEnabled(FriendCarnivalRanking.RANKING_TAB target)
  {
    if (this.currentTab == FriendCarnivalRanking.RANKING_TAB.BORDER)
    {
      this.SetActive((Enum) FriendCarnivalRanking.UI.BTN_BORDER, true);
      this.SetActive((Enum) FriendCarnivalRanking.UI.SPR_INACTIVE_BORDER, false);
    }
    else if (this.currentTab == FriendCarnivalRanking.RANKING_TAB.TOP_PLAYER)
    {
      this.SetActive((Enum) FriendCarnivalRanking.UI.BTN_TOP_PLAYER, true);
      this.SetActive((Enum) FriendCarnivalRanking.UI.SPR_INACTIVE_TOP_PLAYER, false);
    }
    else if (this.currentTab == FriendCarnivalRanking.RANKING_TAB.FOLLOWER)
    {
      this.SetActive((Enum) FriendCarnivalRanking.UI.BTN_FOLLOWER, true);
      this.SetActive((Enum) FriendCarnivalRanking.UI.SPR_INACTIVE_FOLLOWER, false);
    }
    switch (target)
    {
      case FriendCarnivalRanking.RANKING_TAB.BORDER:
        this.SetActive((Enum) FriendCarnivalRanking.UI.BTN_BORDER, false);
        this.SetActive((Enum) FriendCarnivalRanking.UI.SPR_INACTIVE_BORDER, true);
        break;
      case FriendCarnivalRanking.RANKING_TAB.TOP_PLAYER:
        this.SetActive((Enum) FriendCarnivalRanking.UI.BTN_TOP_PLAYER, false);
        this.SetActive((Enum) FriendCarnivalRanking.UI.SPR_INACTIVE_TOP_PLAYER, true);
        break;
      case FriendCarnivalRanking.RANKING_TAB.FOLLOWER:
        this.SetActive((Enum) FriendCarnivalRanking.UI.BTN_FOLLOWER, false);
        this.SetActive((Enum) FriendCarnivalRanking.UI.SPR_INACTIVE_FOLLOWER, true);
        break;
    }
  }

  private void ChangeTitle(FriendCarnivalRanking.RANKING_TAB target)
  {
    this.SetActive((Enum) FriendCarnivalRanking.UI.SPR_TITLE_BORDER, false);
    this.SetActive((Enum) FriendCarnivalRanking.UI.SPR_TITLE_TOP_PLAYER, false);
    this.SetActive((Enum) FriendCarnivalRanking.UI.SPR_TITLE_FOLLOWER, false);
    if (target == FriendCarnivalRanking.RANKING_TAB.BORDER)
      this.SetActive((Enum) FriendCarnivalRanking.UI.SPR_TITLE_BORDER, true);
    else if (target == FriendCarnivalRanking.RANKING_TAB.TOP_PLAYER)
      this.SetActive((Enum) FriendCarnivalRanking.UI.SPR_TITLE_TOP_PLAYER, true);
    else
      this.SetActive((Enum) FriendCarnivalRanking.UI.SPR_TITLE_FOLLOWER, true);
  }

  private void ResetScroll(bool isBorder)
  {
    UIScrollView uiScrollView = !isBorder ? ((Component) this.GetCtrl((Enum) FriendCarnivalRanking.UI.SCR_LIST)).GetComponent<UIScrollView>() : ((Component) this.GetCtrl((Enum) FriendCarnivalRanking.UI.SCR_BORDER_LIST)).GetComponent<UIScrollView>();
    if (!Object.op_Inequality((Object) uiScrollView, (Object) null))
      return;
    ((Behaviour) uiScrollView).enabled = true;
    uiScrollView.ResetPosition();
  }

  private void SetRankItem(int i, Transform t)
  {
    this.SetActive(t, (Enum) FriendCarnivalRanking.UI.OBJ_COMMENT, false);
    this.SetActive(t, (Enum) FriendCarnivalRanking.UI.OBJ_STATUS, false);
    CarnivalFriendCharaInfo carnivalFriendCharaInfo = this.currentRankData[i];
    this.SetPoint(t, carnivalFriendCharaInfo.point);
    this.SetRank(t, carnivalFriendCharaInfo.rank);
    this.SetBorder(t, carnivalFriendCharaInfo.border);
  }

  private void SetBorder(Transform t, int border)
  {
    int length = FriendCarnivalRanking.PlayerBorderUI.Length;
    for (int index = 0; index < length; ++index)
      this.SetActive(t, (Enum) FriendCarnivalRanking.PlayerBorderUI[index], index == border);
  }

  private void SetPoint(Transform t, int point)
  {
    this.SetLabelText(t, (Enum) FriendCarnivalRanking.UI.LBL_POINT, point.ToString("N0") + " pt");
  }

  private void SetRank(Transform t, int rank)
  {
    int length = FriendCarnivalRanking.RankUI.Length;
    for (int index = 0; index < length; ++index)
      this.SetActive(t, (Enum) FriendCarnivalRanking.RankUI[index], index + 1 == rank);
    if (rank <= length)
    {
      this.SetActive(t, (Enum) FriendCarnivalRanking.UI.LBL_RANK, false);
    }
    else
    {
      this.SetActive(t, (Enum) FriendCarnivalRanking.UI.LBL_RANK, true);
      this.SetLabelText(t, (Enum) FriendCarnivalRanking.UI.LBL_RANK, "Rank " + rank.ToString());
    }
  }

  private void UpdateBorderList()
  {
    ((Component) this.GetCtrl((Enum) FriendCarnivalRanking.UI.OBJ_MY_DEGREE_ROOT)).GetComponent<DegreePlate>().Initialize(MonoBehaviourSingleton<UserInfoManager>.I.selectedDegreeIds, false, (Action<DegreePlate>) null);
    this.SetDynamicList((Enum) FriendCarnivalRanking.UI.GRD_BORDER_LIST, "FriendCarnivalBorderListItem", this.currentRankData.Count, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) => this.SetBorderItem(i, t, this.currentRankData[i])));
    this.SetActive((Enum) FriendCarnivalRanking.UI.STR_BORDER_NON_LIST, this.currentRankData.Count <= 0);
    this.ResetScroll(true);
  }

  private void SetBorderItem(int i, Transform t, CarnivalFriendCharaInfo info)
  {
    this.SetBorderIcon(t, FriendCarnivalRanking.BorderIcons[info.border]);
    this.SetLabelText(t, (Enum) FriendCarnivalRanking.UI.LBL_BORDER_NAME, info.name);
    this.SetLabelText(t, (Enum) FriendCarnivalRanking.UI.LBL_BORDER_POINT, info.point.ToString("N0") + " pt");
    this.SetLabelText(t, (Enum) FriendCarnivalRanking.UI.LBL_BORDER_LEVEL, info.level.ToString());
    this.SetLabelText(t, (Enum) FriendCarnivalRanking.UI.LBL_BORDER_RANK, "Rank " + info.rank.ToString());
  }

  private void SetBorderIcon(Transform t, FriendCarnivalRanking.BorderIcon borderIcon)
  {
    if (borderIcon.hasGrade)
    {
      this.SetActive(t, (Enum) FriendCarnivalRanking.UI.SPR_BORDER_ICON, false);
      this.SetActive(t, (Enum) FriendCarnivalRanking.UI.SPR_BORDER_ICON_GRADE, true);
      this.SetSprite(t, (Enum) FriendCarnivalRanking.UI.SPR_BORDER_ICON_GRADE, borderIcon.borderIcon);
      this.SetSprite(t, (Enum) FriendCarnivalRanking.UI.SPR_BORDER_FRAME, borderIcon.borderFrame);
      this.SetSprite(t, (Enum) FriendCarnivalRanking.UI.SPR_BORDER_GRADE, borderIcon.borderGradeIcon);
    }
    else
    {
      this.SetActive(t, (Enum) FriendCarnivalRanking.UI.SPR_BORDER_ICON, true);
      this.SetActive(t, (Enum) FriendCarnivalRanking.UI.SPR_BORDER_ICON_GRADE, false);
      this.SetSprite(t, (Enum) FriendCarnivalRanking.UI.SPR_BORDER_ICON, borderIcon.borderIcon);
      this.SetSprite(t, (Enum) FriendCarnivalRanking.UI.SPR_BORDER_FRAME, borderIcon.borderFrame);
    }
  }

  private void OnQuery_BORDER() => this.ChangeTab(FriendCarnivalRanking.RANKING_TAB.BORDER);

  private void OnQuery_TOP_PLAYER()
  {
    if (this.topPlayerInfo == null)
    {
      GameSection.StayEvent();
      this.StartCoroutine(this.GetTopPlayerInfo((System.Action) (() =>
      {
        this.ChangeTab(FriendCarnivalRanking.RANKING_TAB.TOP_PLAYER);
        GameSection.ResumeEvent(true);
        this.RefreshUI();
      })));
    }
    else
    {
      this.ChangeTab(FriendCarnivalRanking.RANKING_TAB.TOP_PLAYER);
      this.RefreshUI();
    }
  }

  private void OnQuery_FOLLOWER()
  {
    if (this.followerRankInfo == null)
    {
      GameSection.StayEvent();
      this.StartCoroutine(this.GetFollowerInfo((System.Action) (() =>
      {
        this.ChangeTab(FriendCarnivalRanking.RANKING_TAB.FOLLOWER);
        GameSection.ResumeEvent(true);
        this.RefreshUI();
      })));
    }
    else
    {
      this.ChangeTab(FriendCarnivalRanking.RANKING_TAB.FOLLOWER);
      this.RefreshUI();
    }
  }

  private IEnumerator GetCurrentCarnivalStatus()
  {
    bool isRequest = true;
    Protocol.Send<QuestCarnivalPointModel.RequestSendForm, QuestCarnivalPointModel>(QuestCarnivalPointModel.URL, new QuestCarnivalPointModel.RequestSendForm()
    {
      eid = MonoBehaviourSingleton<QuestManager>.I.carnivalEventId
    }, (Action<QuestCarnivalPointModel>) (result =>
    {
      isRequest = false;
      this.currentCarnivalData = result.result;
    }));
    while (isRequest)
      yield return (object) null;
  }

  private IEnumerator GetBorderInfo()
  {
    bool isRequest = true;
    Protocol.Send<CarnivalBorderRankingModel>(CarnivalBorderRankingModel.URL, (WWWForm) null, (Action<CarnivalBorderRankingModel>) (result =>
    {
      isRequest = false;
      this.borderInfo = result.result;
    }));
    while (isRequest)
      yield return (object) null;
  }

  private IEnumerator GetTopPlayerInfo(System.Action callBack)
  {
    bool isRequest = true;
    Protocol.Send<CarnivalTopRankingModel.RequestSendForm, CarnivalTopRankingModel>(CarnivalTopRankingModel.URL, new CarnivalTopRankingModel.RequestSendForm()
    {
      num = 100
    }, (Action<CarnivalTopRankingModel>) (result =>
    {
      isRequest = false;
      this.topPlayerInfo = result.result;
    }));
    while (isRequest)
      yield return (object) null;
    callBack();
  }

  private IEnumerator GetFollowerInfo(System.Action callBack)
  {
    bool isRequest = true;
    Protocol.Send<CarnivalFriendRankingModel>(CarnivalFriendRankingModel.URL, (WWWForm) null, (Action<CarnivalFriendRankingModel>) (result =>
    {
      isRequest = false;
      this.followerRankInfo = result.result;
    }));
    while (isRequest)
      yield return (object) null;
    callBack();
  }

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
    SPR_BORDER_ICON_GRADE,
  }

  private enum RANKING_TAB
  {
    NONE,
    BORDER,
    TOP_PLAYER,
    FOLLOWER,
  }

  public struct BorderIcon(string borderIcon, string borderFrame, string borderGradeIcon)
  {
    public string borderIcon { private set; get; } = borderIcon;

    public string borderFrame { private set; get; } = borderFrame;

    public string borderGradeIcon { private set; get; } = borderGradeIcon;

    public bool hasGrade => !string.IsNullOrEmpty(this.borderGradeIcon);
  }
}
