// Decompiled with JetBrains decompiler
// Type: FriendArenaRanking
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FriendArenaRanking : FriendArenaRankingBase
{
  private static readonly FriendArenaRanking.UI[] GroupTabList = new FriendArenaRanking.UI[6]
  {
    FriendArenaRanking.UI.BTN_TAB_A,
    FriendArenaRanking.UI.BTN_TAB_B,
    FriendArenaRanking.UI.BTN_TAB_C,
    FriendArenaRanking.UI.BTN_TAB_D,
    FriendArenaRanking.UI.BTN_TAB_E,
    FriendArenaRanking.UI.BTN_TAB_TOTAL
  };
  private UIButton[] buttonList = new UIButton[FriendArenaRanking.GroupTabList.Length];
  protected List<FriendCharaInfo>[] charaListList = new List<FriendCharaInfo>[FriendArenaRanking.GroupTabList.Length];
  protected List<ArenaRankingData>[] rankingDataListList = new List<ArenaRankingData>[FriendArenaRanking.GroupTabList.Length];
  private List<FriendCharaInfo>[] charaListListOwn = new List<FriendCharaInfo>[FriendArenaRanking.GroupTabList.Length];
  private List<ArenaRankingData>[] rankingDataListListOwn = new List<ArenaRankingData>[FriendArenaRanking.GroupTabList.Length];
  protected int selectedTab = FriendArenaRanking.GroupTabList.Length - 1;

  public override void Initialize()
  {
    object[] eventData = (object[]) GameSection.GetEventData();
    this.eventData = eventData[0] as Network.EventData;
    this.myRank = (int) eventData[1];
    if (this.eventData == null)
    {
      this.InitializeBase();
    }
    else
    {
      this.nowPage = FriendArenaRanking.GroupTabList.Length - 1;
      this.isTotalTime = true;
      this.FollowListBaseInitialize();
    }
  }

  public override void UpdateUI()
  {
    this.UpdateOwnButton();
    this.SetTab();
    this.UpdateTitle();
    if (this.eventData == null)
      return;
    this.ListUI();
  }

  protected virtual void UpdateTitle()
  {
    if (this.eventData == null)
    {
      this.SetLabelText((Enum) FriendArenaRanking.UI.LBL_ARENA_NAME, "");
      this.SetLabelText((Enum) FriendArenaRanking.UI.LBL_END_DATE, "");
    }
    else
    {
      this.SetLabelText((Enum) FriendArenaRanking.UI.LBL_ARENA_NAME, this.eventData.name);
      this.SetLabelText((Enum) FriendArenaRanking.UI.LBL_END_DATE, QuestUtility.GetEndDateString(this.eventData));
    }
  }

  private void SetTab()
  {
    int event_data = 0;
    for (int length = FriendArenaRanking.GroupTabList.Length; event_data < length; ++event_data)
    {
      Transform ctrl = this.GetCtrl((Enum) FriendArenaRanking.GroupTabList[event_data]);
      this.buttonList[event_data] = ((Component) ctrl).GetComponent<UIButton>();
      this.SetEvent(ctrl, "TAB", event_data);
    }
  }

  protected override void SendGetList(int nowPage, Action<bool> callback)
  {
    int group = this.selectedTab;
    if (this.selectedTab >= FriendArenaRanking.GroupTabList.Length - 1)
      group = -1;
    this.SendGetRanking(group, callback);
  }

  protected virtual void SendGetRanking(int group, Action<bool> callback)
  {
    this.SendGetNormalRanking(group, callback);
  }

  private void SendGetNormalRanking(int sendGroup, Action<bool> callback)
  {
    int isContaionSelf = this.isOwn ? 1 : 0;
    MonoBehaviourSingleton<FriendManager>.I.SendGetArenaRanking(sendGroup, isContaionSelf, (Action<bool, List<ArenaRankingData>>) ((is_success, recv_data) =>
    {
      if (is_success)
      {
        this.recvList = this.ChangeData(this.CreateFriendCharaInfoList(recv_data));
        this.rankingDataList = recv_data;
        this.CacheLists(this.recvList, recv_data);
        List<ArenaRankingData> rankingDataList = this.rankingDataList;
      }
      callback(is_success);
    }));
  }

  private void OnQuery_TAB()
  {
    this.isOwn = false;
    if (this.eventData == null)
      return;
    this.SetButtonActive(this.selectedTab, true);
    int eventData = (int) GameSection.GetEventData();
    this.SetButtonActive(eventData, false);
    this.selectedTab = eventData;
    this.isTotalTime = eventData == FriendArenaRanking.GroupTabList.Length - 1;
    this.RefreshListAndUI(eventData);
  }

  protected override void OnQuery_OWN()
  {
    base.OnQuery_OWN();
    this.RefreshListAndUI(this.selectedTab);
  }

  private void RefreshListAndUI(int group)
  {
    if (this.eventData == null)
      return;
    if (this.IsExistCache(group))
    {
      this.recvList = this.ChangeData(this.GetCacheCharaList(group));
      this.rankingDataList = this.GetCacheRankingDataList(group);
      this.Refresh();
      this.DragToOwn();
    }
    else
    {
      GameSection.StayEvent();
      this.SendGetList(this.nowPage, (Action<bool>) (b =>
      {
        GameSection.ResumeEvent(b);
        this.Refresh();
        this.DragToOwn();
      }));
    }
  }

  protected virtual void CacheLists(
    List<FriendCharaInfo> charaList,
    List<ArenaRankingData> rankingDataList)
  {
    if (this.isOwn)
    {
      this.charaListListOwn[this.selectedTab] = charaList;
      this.rankingDataListListOwn[this.selectedTab] = rankingDataList;
    }
    else
    {
      this.charaListList[this.selectedTab] = charaList;
      this.rankingDataListList[this.selectedTab] = rankingDataList;
    }
  }

  protected virtual bool IsExistCache(int tabNum)
  {
    return this.isOwn ? this.charaListListOwn[tabNum] != null : this.charaListList[tabNum] != null;
  }

  protected virtual List<FriendCharaInfo> GetCacheCharaList(int tabNum)
  {
    return this.isOwn ? this.charaListListOwn[tabNum] : this.charaListList[tabNum];
  }

  protected virtual List<ArenaRankingData> GetCacheRankingDataList(int tabNum)
  {
    return this.isOwn ? this.rankingDataListListOwn[tabNum] : this.rankingDataListList[tabNum];
  }

  private void SetButtonActive(int index, bool isActive)
  {
    this.buttonList[index].isEnabled = isActive;
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
    OBJ_OWN_OFF,
    LBL_TIME_DEFAULT,
    BTN_TAB_A,
    BTN_TAB_B,
    BTN_TAB_C,
    BTN_TAB_D,
    BTN_TAB_E,
    BTN_TAB_TOTAL,
    LBL_END_DATE,
  }
}
