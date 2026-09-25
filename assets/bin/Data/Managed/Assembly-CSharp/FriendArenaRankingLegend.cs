// Decompiled with JetBrains decompiler
// Type: FriendArenaRankingLegend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FriendArenaRankingLegend : FriendArenaRankingBase
{
  private List<Network.EventData> eventDataList = new List<Network.EventData>();

  protected override void SetListItem(int i, Transform t, bool is_recycle, FriendCharaInfo data)
  {
    base.SetListItem(i, t, is_recycle, data);
    this.SetArenaName(t, i);
  }

  public override void UpdateUI()
  {
    base.UpdateUI();
    this.SetActive((Enum) FriendArenaRankingLegend.UI.BTN_OWN, false);
    this.SetActive((Enum) FriendArenaRankingLegend.UI.OBJ_OWN_ON, false);
  }

  protected override void SetRankiItem(int i, Transform t)
  {
    base.SetRankiItem(i, t);
    this.SetActive(t, (Enum) FriendArenaRankingLegend.UI.OBJ_STATUS, true);
    this.SetActive(t, (Enum) FriendArenaRankingLegend.UI.OBJ_COMMENT, true);
  }

  protected override void SetRank(Transform t, int rank)
  {
    int length = FriendArenaRankingBase.RankSprite.Length;
    for (int index = 0; index < length; ++index)
      this.SetActive(t, (Enum) FriendArenaRankingBase.RankSprite[index], false);
    this.SetActive(t, (Enum) FriendArenaRankingLegend.UI.LBL_RANK, false);
  }

  protected override void UpdateDynamicList()
  {
    FriendCharaInfo[] info = (FriendCharaInfo[]) null;
    int item_num = 0;
    if (this.recvList != null && this.recvList.Count > 0)
    {
      info = this.recvList.ToArray();
      if (info != null)
        item_num = info.Length;
    }
    this.SetDynamicList((Enum) FriendArenaRankingLegend.UI.GRD_LIST, this.GetListItemName, item_num, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) => this.SetListItem(i, t, is_recycle, info[i])));
  }

  private void SetArenaName(Transform t, int index)
  {
    this.SetLabelText(t, (Enum) FriendArenaRankingLegend.UI.LBL_ITEM_ARENA_NAME, this.eventDataList[index].name);
  }

  protected override void SendGetList(int page, Action<bool> callback)
  {
    MonoBehaviourSingleton<FriendManager>.I.SendGetLegendRanking((Action<bool, List<ArenaLegendRankingModel.Param>>) ((is_success, recv_data) =>
    {
      if (is_success)
      {
        this.UpdateLists(recv_data);
        this.recvList = this.ChangeData(this.CreateFriendCharaInfoList(this.rankingDataList));
        this.nowPage = page;
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
      (object) this.eventDataList[eventData]
    });
  }

  private void UpdateLists(List<ArenaLegendRankingModel.Param> result)
  {
    this.rankingDataList = new List<ArenaRankingData>();
    this.eventDataList = new List<Network.EventData>();
    List<FriendInfo> friendInfoList = new List<FriendInfo>();
    int index = 0;
    for (int count = result.Count; index < count; ++index)
    {
      this.rankingDataList.Add(result[index].rankingData);
      this.eventDataList.Add(result[index].eventData);
    }
  }

  protected override string GetListItemName => "FriendArenaRankingLegendListItem";

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
    LBL_ITEM_ARENA_NAME,
  }
}
