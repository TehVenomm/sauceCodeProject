// Decompiled with JetBrains decompiler
// Type: FriendArenaRankingLast
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class FriendArenaRankingLast : FriendArenaRankingBase
{
  private List<FriendCharaInfo>[] cacheCharaList = new List<FriendCharaInfo>[2];
  private List<ArenaRankingData>[] cacheRankingList = new List<ArenaRankingData>[2];
  protected Network.EventData lastEventData;

  public override void Initialize()
  {
    this.isTotalTime = true;
    base.Initialize();
  }

  private void SetArenaName(Network.EventData nameEventData)
  {
    if (nameEventData == null)
    {
      this.SetLabelText((Enum) FriendArenaRankingLast.UI.LBL_ARENA_NAME, "");
      this.SetLabelText((Enum) FriendArenaRankingLast.UI.LBL_END_DATE, "");
    }
    else
    {
      this.SetLabelText((Enum) FriendArenaRankingLast.UI.LBL_ARENA_NAME, nameEventData.name);
      this.SetLabelText((Enum) FriendArenaRankingLast.UI.LBL_END_DATE, QuestUtility.GetEndDateString(nameEventData));
    }
  }

  private void CacheLists(List<FriendCharaInfo> charaList, List<ArenaRankingData> rankingDataList)
  {
    int index = this.isOwn ? 1 : 0;
    this.cacheCharaList[index] = charaList;
    this.cacheRankingList[index] = rankingDataList;
  }

  private bool IsExistCache() => this.cacheCharaList[this.isOwn ? 1 : 0] != null;

  private List<FriendCharaInfo> GetCacheCharaList() => this.cacheCharaList[this.isOwn ? 1 : 0];

  private List<ArenaRankingData> GetCacheRankingDataList()
  {
    return this.cacheRankingList[this.isOwn ? 1 : 0];
  }

  protected override void OnQuery_OWN()
  {
    base.OnQuery_OWN();
    this.RefreshListAndUI();
  }

  private void RefreshListAndUI()
  {
    if (this.IsExistCache())
    {
      this.recvList = this.ChangeData(this.GetCacheCharaList());
      this.rankingDataList = this.GetCacheRankingDataList();
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

  protected override void UpdateOwnButton() => base.UpdateOwnButton();

  protected override bool IsRankingJoined() => this.myRank >= 1;

  protected override void SendGetList(int nowPage, Action<bool> callback)
  {
    MonoBehaviourSingleton<FriendManager>.I.SendGetLastRanking(-1, this.isOwn ? 1 : 0, (Action<bool, ArenaLastRankingModel.Param>) ((is_success, recv_data) =>
    {
      if (is_success)
      {
        this.recvList = this.ChangeData(this.CreateFriendCharaInfoList(recv_data.rankingDataList));
        this.rankingDataList = recv_data.rankingDataList;
        this.lastEventData = recv_data.eventData;
        this.CacheLists(this.recvList, this.rankingDataList);
        this.SetArenaName(this.lastEventData);
        this.myRank = recv_data.myRank;
      }
      callback(is_success);
    }));
  }

  public override void OnQuery_FOLLOW_INFO()
  {
    int eventData = (int) GameSection.GetEventData();
    FriendCharaInfo recv = this.recvList[eventData];
    MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = eventData + 4;
    GameSection.SetEventData((object) new object[2]
    {
      (object) recv,
      (object) this.lastEventData
    });
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
    LBL_TIME_DEFAULT,
    LBL_END_DATE,
  }
}
