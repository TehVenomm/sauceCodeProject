// Decompiled with JetBrains decompiler
// Type: FriendArenaRankingBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FriendArenaRankingBase : FollowListBase
{
  protected Network.EventData eventData;
  protected List<ArenaRankingData> rankingDataList = new List<ArenaRankingData>();
  protected static readonly FriendArenaRankingBase.UI[] RankSprite = new FriendArenaRankingBase.UI[3]
  {
    FriendArenaRankingBase.UI.SPR_1,
    FriendArenaRankingBase.UI.SPR_2,
    FriendArenaRankingBase.UI.SPR_3
  };
  protected bool isOwn;
  protected int myRank;
  protected bool isTotalTime;
  private UIScrollView scrollView;
  private UIPanel scrollPanel;

  public override void Initialize()
  {
    object[] eventData = (object[]) GameSection.GetEventData();
    this.eventData = eventData[0] as Network.EventData;
    this.myRank = (int) eventData[1];
    this.nowPage = 0;
    this.FollowListBaseInitialize();
  }

  protected void FollowListBaseInitialize() => base.Initialize();

  public override void UpdateUI()
  {
    this.UpdateOwnButton();
    this.ListUI();
  }

  protected void DragToOwn()
  {
    if (!this.isOwn)
      return;
    if (Object.op_Equality((Object) this.scrollView, (Object) null))
    {
      this.scrollView = ((Component) this.GetCtrl((Enum) FriendArenaRankingBase.UI.SCR_LIST)).GetComponent<UIScrollView>();
      this.scrollPanel = ((Component) this.GetCtrl((Enum) FriendArenaRankingBase.UI.SCR_LIST)).GetComponent<UIPanel>();
    }
    int num1 = -1;
    int index = 0;
    for (int count = this.rankingDataList.Count; index < count; ++index)
    {
      if (this.rankingDataList[index].friendCharaInfo.userId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
      {
        num1 = index;
        break;
      }
    }
    if (num1 <= -1)
    {
      this.DispatchEvent("OUT_OF_RANK");
    }
    else
    {
      if (this.rankingDataList.Count <= 3 || num1 <= 3)
        return;
      int count = this.rankingDataList.Count;
      float num2 = (float) num1 / (float) count;
      if ((double) num2 >= 0.60000002384185791)
        num2 = (float) (num1 + 2) / (float) count;
      this.scrollView.SetDragAmount(num2, num2, true);
      ((Component) this.scrollView).transform.localPosition = Vector2.op_Implicit(Vector2.op_UnaryNegation(this.scrollPanel.clipOffset));
    }
  }

  protected override void SetListItem(int i, Transform t, bool is_recycle, FriendCharaInfo data)
  {
    base.SetListItem(i, t, is_recycle, data);
    this.SetRankiItem(i, t);
  }

  protected virtual void SetRankiItem(int i, Transform t)
  {
    ArenaRankingData rankingData = this.rankingDataList[i];
    this.SetActive(t, (Enum) FriendArenaRankingBase.UI.OBJ_COMMENT, false);
    this.SetActive(t, (Enum) FriendArenaRankingBase.UI.OBJ_STATUS, false);
    this.SetTime(t, rankingData.clearMilliSec);
    this.SetRank(t, rankingData.rank);
  }

  private void SetTime(Transform t, int milliSec)
  {
    string stringByMilliSec = QuestUtility.CreateTimeStringByMilliSec(milliSec);
    bool is_visible = QuestUtility.IsDefaultArenaTime(milliSec) && !this.isTotalTime;
    this.SetActive(t, (Enum) FriendArenaRankingBase.UI.LBL_TIME, !is_visible);
    this.SetActive(t, (Enum) FriendArenaRankingBase.UI.LBL_TIME_DEFAULT, is_visible);
    if (is_visible)
      this.SetLabelText(t, (Enum) FriendArenaRankingBase.UI.LBL_TIME_DEFAULT, stringByMilliSec);
    else
      this.SetLabelText(t, (Enum) FriendArenaRankingBase.UI.LBL_TIME, stringByMilliSec);
  }

  protected virtual void SetRank(Transform t, int rank)
  {
    int length = FriendArenaRankingBase.RankSprite.Length;
    for (int index = 0; index < length; ++index)
      this.SetActive(t, (Enum) FriendArenaRankingBase.RankSprite[index], index + 1 == rank);
    if (rank <= length)
    {
      this.SetActive(t, (Enum) FriendArenaRankingBase.UI.LBL_RANK, false);
    }
    else
    {
      this.SetActive(t, (Enum) FriendArenaRankingBase.UI.LBL_RANK, true);
      this.SetLabelText(t, (Enum) FriendArenaRankingBase.UI.LBL_RANK, string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 5U), (object) rank.ToString()));
    }
  }

  protected List<FriendCharaInfo> CreateFriendCharaInfoList(List<ArenaRankingData> rankingDataList)
  {
    List<FriendCharaInfo> friendCharaInfoList = new List<FriendCharaInfo>();
    int index = 0;
    for (int count = rankingDataList.Count; index < count; ++index)
      friendCharaInfoList.Add(rankingDataList[index].friendCharaInfo);
    return friendCharaInfoList;
  }

  protected virtual void OnQuery_OWN() => this.isOwn = !this.isOwn;

  public override void OnQuery_FOLLOW_INFO()
  {
    int eventData = (int) GameSection.GetEventData();
    FriendCharaInfo recv = this.recvList[eventData];
    MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = eventData + 4;
    GameSection.SetEventData((object) new object[2]
    {
      (object) recv,
      (object) this.eventData
    });
  }

  protected void Refresh()
  {
    this.SetDirty((Enum) FriendArenaRankingBase.UI.GRD_LIST);
    this.RefreshUI();
  }

  protected virtual void UpdateOwnButton()
  {
    this.SetActive((Enum) FriendArenaRankingBase.UI.BTN_OWN, this.IsRankingJoined());
    this.SetActive((Enum) FriendArenaRankingBase.UI.OBJ_OWN_ON, this.isOwn);
  }

  protected virtual bool IsRankingJoined()
  {
    return MonoBehaviourSingleton<UserInfoManager>.I.isJoinedArenaRanking;
  }

  private void OnQuery_FOLLOWER()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.eventData,
      (object) this.myRank
    });
  }

  private void OnQuery_WORLD()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.eventData,
      (object) this.myRank
    });
  }

  private void OnQuery_LAST()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.eventData,
      (object) this.myRank
    });
  }

  private void OnQuery_LEGEND()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) this.eventData,
      (object) this.myRank
    });
  }

  protected override string GetListItemName => "FriendArenaRankingListItem";

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
  }
}
