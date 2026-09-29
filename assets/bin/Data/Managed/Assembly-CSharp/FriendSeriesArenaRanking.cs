// Decompiled with JetBrains decompiler
// Type: FriendSeriesArenaRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FriendSeriesArenaRanking : FollowListBase
{
  private FriendSeriesArenaRanking.RANKING_TAB currentTab;
  private QuestCarnivalPointModel.Param currentCarnivalData;
  private List<CarnivalFriendCharaInfo> topPlayerInfo;
  private List<CarnivalFriendCharaInfo> followerRankInfo;
  private List<CarnivalFriendCharaInfo> borderInfo;
  private List<CarnivalFriendCharaInfo> currentRankData;
  private static readonly FriendSeriesArenaRanking.UI[] RankUI = new FriendSeriesArenaRanking.UI[3]
  {
    FriendSeriesArenaRanking.UI.SPR_1,
    FriendSeriesArenaRanking.UI.SPR_2,
    FriendSeriesArenaRanking.UI.SPR_3
  };
  private EventListData eventData;
  private const string BorderItemName = "FriendSeriesArenaBorderListItem";

  public override void Initialize()
  {
    this.nowPage = 0;
    this.eventData = MonoBehaviourSingleton<DeliveryManager>.I.FindSeriesArenaTopData();
    this.StartCoroutine(this.DoInitialize());
  }

  public override void UpdateUI()
  {
    if (this.currentTab != FriendSeriesArenaRanking.RANKING_TAB.BORDER)
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

  protected override string GetListItemName => "FriendSeriesArenaRankingListItem";

  private IEnumerator DoInitialize()
  {
    yield return (object) this.GetCurrentCarnivalStatus();
    yield return (object) this.GetBorderInfo();
    this.SetSelfInfo();
    this.SetActive((Enum) FriendSeriesArenaRanking.UI.BTN_BORDER, false);
    this.SetActive((Enum) FriendSeriesArenaRanking.UI.BTN_TOP_PLAYER, true);
    this.SetActive((Enum) FriendSeriesArenaRanking.UI.BTN_FOLLOWER, true);
    this.SetActive((Enum) FriendSeriesArenaRanking.UI.SPR_INACTIVE_BORDER, true);
    this.SetActive((Enum) FriendSeriesArenaRanking.UI.SPR_INACTIVE_TOP_PLAYER, false);
    this.SetActive((Enum) FriendSeriesArenaRanking.UI.SPR_INACTIVE_FOLLOWER, false);
    this.currentTab = FriendSeriesArenaRanking.RANKING_TAB.NONE;
    this.ChangeTab(FriendSeriesArenaRanking.RANKING_TAB.BORDER);
    this.SetLabelText((Enum) FriendSeriesArenaRanking.UI.LBL_EVENT_NAME, this.currentCarnivalData.eventName);
    this.isInitializeSend = false;
    base.Initialize();
  }

  private void SetSelfInfo()
  {
    Network.UserInfo userInfo = MonoBehaviourSingleton<UserInfoManager>.I.userInfo;
    UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
    Transform ctrl = this.GetCtrl((Enum) FriendSeriesArenaRanking.UI.ROOT_MYSCORE);
    this.SetLabelText(ctrl, (Enum) FriendSeriesArenaRanking.UI.LBL_NAME, userInfo.name);
    this.SetLabelText(ctrl, (Enum) FriendSeriesArenaRanking.UI.LBL_LEVEL, userStatus.level.ToString());
    this.SetClearTime(ctrl, this.currentCarnivalData.clearTime);
    this.SetRank(ctrl, this.currentCarnivalData.rank);
    this.SetMonsteLv(ctrl, this.currentCarnivalData.maxLevel);
    this.ForceSetRenderPlayerModel(ctrl, (Enum) FriendSeriesArenaRanking.UI.TEX_MODEL, PlayerLoadInfo.FromUserStatus(false, true), 99, new Vector3(0.0f, -1.536f, 1.87f), new Vector3(0.0f, 154f, 0.0f), true);
  }

  private void ChangeTab(FriendSeriesArenaRanking.RANKING_TAB target)
  {
    if (target == this.currentTab)
      return;
    bool flag = target == FriendSeriesArenaRanking.RANKING_TAB.BORDER;
    this.SetActive((Enum) FriendSeriesArenaRanking.UI.OBJ_FULL_LIST, !flag);
    this.SetActive((Enum) FriendSeriesArenaRanking.UI.OBJ_BORDER_LIST, flag);
    this.ResetScroll(flag);
    this.ChangeRankingTabButtonEnabled(target);
    this.ChangeTitle(target);
    this.ChangeRankData(target);
    this.SetActive((Enum) FriendSeriesArenaRanking.UI.SPR_HEADER_BORDER, target == FriendSeriesArenaRanking.RANKING_TAB.BORDER);
    this.SetActive((Enum) FriendSeriesArenaRanking.UI.SPR_HEADER_TOP_PLAYER, target == FriendSeriesArenaRanking.RANKING_TAB.TOP_PLAYER);
    this.SetActive((Enum) FriendSeriesArenaRanking.UI.SPR_HEADER_FOLLOWER, target == FriendSeriesArenaRanking.RANKING_TAB.FOLLOWER);
    this.currentTab = target;
  }

  private void ChangeRankData(FriendSeriesArenaRanking.RANKING_TAB target)
  {
    if (target == FriendSeriesArenaRanking.RANKING_TAB.BORDER)
      this.currentRankData = this.borderInfo;
    else if (target == FriendSeriesArenaRanking.RANKING_TAB.TOP_PLAYER)
      this.currentRankData = this.topPlayerInfo;
    else
      this.currentRankData = this.followerRankInfo;
  }

  private void ChangeRankingTabButtonEnabled(FriendSeriesArenaRanking.RANKING_TAB target)
  {
    if (this.currentTab == FriendSeriesArenaRanking.RANKING_TAB.BORDER)
    {
      this.SetActive((Enum) FriendSeriesArenaRanking.UI.BTN_BORDER, true);
      this.SetActive((Enum) FriendSeriesArenaRanking.UI.SPR_INACTIVE_BORDER, false);
    }
    else if (this.currentTab == FriendSeriesArenaRanking.RANKING_TAB.TOP_PLAYER)
    {
      this.SetActive((Enum) FriendSeriesArenaRanking.UI.BTN_TOP_PLAYER, true);
      this.SetActive((Enum) FriendSeriesArenaRanking.UI.SPR_INACTIVE_TOP_PLAYER, false);
    }
    else if (this.currentTab == FriendSeriesArenaRanking.RANKING_TAB.FOLLOWER)
    {
      this.SetActive((Enum) FriendSeriesArenaRanking.UI.BTN_FOLLOWER, true);
      this.SetActive((Enum) FriendSeriesArenaRanking.UI.SPR_INACTIVE_FOLLOWER, false);
    }
    switch (target)
    {
      case FriendSeriesArenaRanking.RANKING_TAB.BORDER:
        this.SetActive((Enum) FriendSeriesArenaRanking.UI.BTN_BORDER, false);
        this.SetActive((Enum) FriendSeriesArenaRanking.UI.SPR_INACTIVE_BORDER, true);
        break;
      case FriendSeriesArenaRanking.RANKING_TAB.TOP_PLAYER:
        this.SetActive((Enum) FriendSeriesArenaRanking.UI.BTN_TOP_PLAYER, false);
        this.SetActive((Enum) FriendSeriesArenaRanking.UI.SPR_INACTIVE_TOP_PLAYER, true);
        break;
      case FriendSeriesArenaRanking.RANKING_TAB.FOLLOWER:
        this.SetActive((Enum) FriendSeriesArenaRanking.UI.BTN_FOLLOWER, false);
        this.SetActive((Enum) FriendSeriesArenaRanking.UI.SPR_INACTIVE_FOLLOWER, true);
        break;
    }
  }

  private void ChangeTitle(FriendSeriesArenaRanking.RANKING_TAB target)
  {
    this.SetActive((Enum) FriendSeriesArenaRanking.UI.SPR_TITLE_BORDER, false);
    this.SetActive((Enum) FriendSeriesArenaRanking.UI.SPR_TITLE_TOP_PLAYER, false);
    this.SetActive((Enum) FriendSeriesArenaRanking.UI.SPR_TITLE_FOLLOWER, false);
    if (target == FriendSeriesArenaRanking.RANKING_TAB.BORDER)
      this.SetActive((Enum) FriendSeriesArenaRanking.UI.SPR_TITLE_BORDER, true);
    else if (target == FriendSeriesArenaRanking.RANKING_TAB.TOP_PLAYER)
      this.SetActive((Enum) FriendSeriesArenaRanking.UI.SPR_TITLE_TOP_PLAYER, true);
    else
      this.SetActive((Enum) FriendSeriesArenaRanking.UI.SPR_TITLE_FOLLOWER, true);
  }

  private void ResetScroll(bool isBorder)
  {
    UIScrollView uiScrollView = !isBorder ? ((Component) this.GetCtrl((Enum) FriendSeriesArenaRanking.UI.SCR_LIST)).GetComponent<UIScrollView>() : ((Component) this.GetCtrl((Enum) FriendSeriesArenaRanking.UI.SCR_BORDER_LIST)).GetComponent<UIScrollView>();
    if (!Object.op_Inequality((Object) uiScrollView, (Object) null))
      return;
    ((Behaviour) uiScrollView).enabled = true;
    uiScrollView.ResetPosition();
  }

  private void SetRankItem(int i, Transform t)
  {
    this.SetActive(t, (Enum) FriendSeriesArenaRanking.UI.OBJ_COMMENT, false);
    CarnivalFriendCharaInfo carnivalFriendCharaInfo = this.currentRankData[i];
    this.SetClearTime(t, carnivalFriendCharaInfo.clearTime);
    this.SetRank(t, carnivalFriendCharaInfo.rank);
    this.SetMonsteLv(t, carnivalFriendCharaInfo.maxLevel);
  }

  private void SetMonsteLv(Transform t, int monster_lv)
  {
    this.SetLabelText(t, (Enum) FriendSeriesArenaRanking.UI.LBL_MONSTER_LV, monster_lv.ToString("N0"));
  }

  private void SetClearTime(Transform t, int clearTime)
  {
    string milliSecSeriesArena = QuestUtility.CreateTimeStringByMilliSecSeriesArena(clearTime);
    this.SetLabelText(t, (Enum) FriendSeriesArenaRanking.UI.LBL_CLEAR_TIME, milliSecSeriesArena);
  }

  private void SetRank(Transform t, int rank)
  {
    int length = FriendSeriesArenaRanking.RankUI.Length;
    for (int index = 0; index < length; ++index)
      this.SetActive(t, (Enum) FriendSeriesArenaRanking.RankUI[index], index + 1 == rank);
    if (rank <= length)
    {
      this.SetActive(t, (Enum) FriendSeriesArenaRanking.UI.LBL_RANK, false);
    }
    else
    {
      this.SetActive(t, (Enum) FriendSeriesArenaRanking.UI.LBL_RANK, true);
      this.SetLabelText(t, (Enum) FriendSeriesArenaRanking.UI.LBL_RANK, "Rank " + rank.ToString());
    }
  }

  private void UpdateBorderList()
  {
    ((Component) this.GetCtrl((Enum) FriendSeriesArenaRanking.UI.OBJ_DEGREE_FRAME_ROOT)).GetComponent<DegreePlate>().Initialize(MonoBehaviourSingleton<UserInfoManager>.I.selectedDegreeIds, false, (Action<DegreePlate>) null);
    this.SetDynamicList((Enum) FriendSeriesArenaRanking.UI.GRD_BORDER_LIST, "FriendSeriesArenaBorderListItem", this.currentRankData.Count, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) => this.SetBorderItem(i, t, this.currentRankData[i])));
    this.SetActive((Enum) FriendSeriesArenaRanking.UI.STR_BORDER_NON_LIST, this.currentRankData.Count <= 0);
    this.ResetScroll(true);
  }

  private void SetBorderItem(int i, Transform t, CarnivalFriendCharaInfo info)
  {
    this.SetLabelText(t, (Enum) FriendSeriesArenaRanking.UI.LBL_NAME, info.name);
    this.SetClearTime(t, info.clearTime);
    this.SetRank(t, info.rank);
    this.SetMonsteLv(t, info.maxLevel);
    this.SetLabelText(t, (Enum) FriendSeriesArenaRanking.UI.LBL_LEVEL, info.level.ToString());
  }

  private void OnQuery_BORDER() => this.ChangeTab(FriendSeriesArenaRanking.RANKING_TAB.BORDER);

  private void OnQuery_TOP_PLAYER()
  {
    if (this.topPlayerInfo == null)
    {
      GameSection.StayEvent();
      this.StartCoroutine(this.GetTopPlayerInfo((System.Action) (() =>
      {
        this.ChangeTab(FriendSeriesArenaRanking.RANKING_TAB.TOP_PLAYER);
        GameSection.ResumeEvent(true);
        this.RefreshUI();
      })));
    }
    else
    {
      this.ChangeTab(FriendSeriesArenaRanking.RANKING_TAB.TOP_PLAYER);
      this.RefreshUI();
    }
  }

  protected virtual void OnQuery_INFO_REWARD()
  {
    GameSection.SetEventData((object) string.Format(WebViewManager.NewsWithLinkParamFormat, (object) this.eventData.linkName));
  }

  private void OnQuery_FOLLOWER()
  {
    if (this.followerRankInfo == null)
    {
      GameSection.StayEvent();
      this.StartCoroutine(this.GetFollowerInfo((System.Action) (() =>
      {
        this.ChangeTab(FriendSeriesArenaRanking.RANKING_TAB.FOLLOWER);
        GameSection.ResumeEvent(true);
        this.RefreshUI();
      })));
    }
    else
    {
      this.ChangeTab(FriendSeriesArenaRanking.RANKING_TAB.FOLLOWER);
      this.RefreshUI();
    }
  }

  private IEnumerator GetCurrentCarnivalStatus()
  {
    bool isRequest = true;
    Protocol.Send<QuestCarnivalPointModel.RequestSendForm, QuestCarnivalPointModel>(QuestCarnivalPointModel.URL, new QuestCarnivalPointModel.RequestSendForm()
    {
      eid = MonoBehaviourSingleton<DeliveryManager>.I.FindSeriesArenaTopData().eventId
    }, (Action<QuestCarnivalPointModel>) (result =>
    {
      isRequest = false;
      this.currentCarnivalData = result.result;
    }));
    while (isRequest)
      yield return (object) null;
  }

  private IEnumerator GetTopPlayerInfo(System.Action callBack)
  {
    bool isRequest = true;
    Protocol.Send<CarnivalTopRankingModel.RequestSendForm, CarnivalTopRankingModel>(CarnivalTopRankingModel.URL, new CarnivalTopRankingModel.RequestSendForm()
    {
      num = 100,
      sa = 1
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
    Protocol.Send<CarnivalFriendRankingModel.RequestSendForm, CarnivalFriendRankingModel>(CarnivalFriendRankingModel.URL, new CarnivalFriendRankingModel.RequestSendForm()
    {
      sa = 1
    }, (Action<CarnivalFriendRankingModel>) (result =>
    {
      isRequest = false;
      this.followerRankInfo = result.result;
    }));
    while (isRequest)
      yield return (object) null;
    callBack();
  }

  private IEnumerator GetBorderInfo()
  {
    bool isRequest = true;
    Protocol.Send<CarnivalBorderRankingModel.RequestSendForm, CarnivalBorderRankingModel>(CarnivalBorderRankingModel.URL, new CarnivalBorderRankingModel.RequestSendForm()
    {
      sa = 1
    }, (Action<CarnivalBorderRankingModel>) (result =>
    {
      isRequest = false;
      this.borderInfo = result.result;
    }));
    while (isRequest)
      yield return (object) null;
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
    ROOT_MYSCORE,
  }

  private enum RANKING_TAB
  {
    NONE,
    BORDER,
    TOP_PLAYER,
    FOLLOWER,
  }
}
