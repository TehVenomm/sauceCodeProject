// Decompiled with JetBrains decompiler
// Type: FriendMessageList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FriendMessageList : FollowListBase
{
  private List<FriendMessageUserListModel.MessageUserInfo> recvdata;

  public override void Initialize()
  {
    this.isInitializeSend = true;
    this.m_isVisibleDefaultInfo = false;
    int followerListSortType = GameSaveData.instance.MutualFollowerListSortType;
    if (0 <= followerListSortType && followerListSortType < 5)
      this.m_currentSortType = (USER_SORT_TYPE) followerListSortType;
    this.titleType = FollowListBase.TITLE_TYPE.MESSAGE;
    base.Initialize();
  }

  public override void StartSection()
  {
  }

  public override void UpdateUI() => this.ListUI();

  public override void ListUI()
  {
    this.SetLabelText((Enum) FriendMessageList.UI.STR_TITLE, this.sectionData.GetText("STR_TITLE"));
    this.SetLabelText((Enum) FriendMessageList.UI.STR_TITLE_REFLECT, this.sectionData.GetText("STR_TITLE"));
    this.SetLabelText((Enum) FriendMessageList.UI.LBL_SORT, StringTable.Get(STRING_CATEGORY.USER_SORT, (uint) this.m_currentSortType));
    FriendMessageUserListModel.MessageUserInfo[] array = this.recvdata.ToArray();
    if (array == null || array.Length == 0)
    {
      this.SetActive((Enum) FriendMessageList.UI.STR_NON_LIST, true);
      this.SetActive((Enum) FriendMessageList.UI.GRD_LIST, false);
      this.SetActive((Enum) FriendMessageList.UI.OBJ_ACTIVE_ROOT, false);
      this.SetActive((Enum) FriendMessageList.UI.OBJ_INACTIVE_ROOT, true);
      this.SetLabelText((Enum) FriendMessageList.UI.LBL_NOW, "0");
      this.SetLabelText((Enum) FriendMessageList.UI.LBL_MAX, "0");
    }
    else
    {
      this.SetPageNumText((Enum) FriendMessageList.UI.LBL_NOW, this.nowPage + 1);
      this.SetPageNumText((Enum) FriendMessageList.UI.LBL_MAX, this.pageNumMax);
      this.SetActive((Enum) FriendMessageList.UI.STR_NON_LIST, false);
      this.SetActive((Enum) FriendMessageList.UI.GRD_LIST, true);
      this.SetActive((Enum) FriendMessageList.UI.OBJ_ACTIVE_ROOT, this.pageNumMax != 1);
      this.SetActive((Enum) FriendMessageList.UI.OBJ_INACTIVE_ROOT, this.pageNumMax == 1);
      this.UpdateDynamicList();
    }
  }

  protected override void UpdateDynamicList()
  {
    FriendMessageUserListModel.MessageUserInfo[] array = this.recvdata.ToArray();
    int pageItemLength = this.GetPageItemLength(this.nowPage);
    int num = this.nowPage * 10;
    if (pageItemLength < 1 || array.Length < 1 || array.Length < num + pageItemLength)
      return;
    FriendMessageUserListModel.MessageUserInfo[] info = new FriendMessageUserListModel.MessageUserInfo[pageItemLength];
    for (int index = 0; index < pageItemLength; ++index)
      info[index] = array[num + index];
    if (GameDefine.ACTIVE_DEGREE)
      this.ScrollGrid.cellHeight = (float) GameDefine.DEGREE_FRIEND_LIST_HEIGHT;
    this.CleanItemList();
    this.SetDynamicList((Enum) FriendMessageList.UI.GRD_LIST, "FollowListBaseItem", pageItemLength, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      FriendMessageUserListModel.MessageUserInfo data = info[i];
      string clanId = data.userClanData != null ? data.userClanData.cId : "0";
      this.SetFollowStatus(t, data.userId, true, true, clanId);
      this.SetCharaInfo((FriendCharaInfo) data, i, t, is_recycle, data.userId == 0);
      this.SetBadge(t, data.noReadNum, (SpriteAlignment) 3, -10, -6);
    }));
  }

  private int GetPageItemLength(int currentPage)
  {
    return currentPage + 1 < this.pageNumMax || this.recvdata.Count % 10 <= 0 ? 10 : this.recvdata.Count % 10;
  }

  protected override void SendGetList(int page, Action<bool> callback = null)
  {
    MonoBehaviourSingleton<FriendManager>.I.SendGetMessageUserList(page, (Action<bool, FriendMessageUserListModel.Param>) ((is_success, recv_data) =>
    {
      if (is_success)
      {
        this.recvdata = recv_data.messageUser;
        this.nowPage = page;
        this.pageNumMax = Mathf.CeilToInt((float) this.recvdata.Count / 10f);
        this.Sort<FriendMessageUserListModel.MessageUserInfo>(this.recvdata);
      }
      if (callback == null)
        return;
      callback(is_success);
    }));
  }

  public override void OnQuery_FOLLOW_INFO()
  {
    int eventData = (int) GameSection.GetEventData();
    List<FriendMessageUserListModel.MessageUserInfo> recvdata = this.recvdata;
    int index = eventData + this.nowPage * 10;
    if (index < 0 || recvdata.IsNullOrEmpty<FriendMessageUserListModel.MessageUserInfo>() || recvdata.Count <= index)
      return;
    FriendMessageUserListModel.MessageUserInfo event_data = this.recvdata[index];
    MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = eventData + 4;
    GameSection.SetEventData((object) event_data);
  }

  public void OnQuery_DIRECT_VIEW_MESSAGE()
  {
    int eventData = (int) GameSection.GetEventData();
    this.recvdata.ToArray();
    this.GetPageItemLength(this.nowPage);
    int num = this.nowPage * 10;
    if (this.recvdata == null || num + eventData < 0 || this.recvdata.Count <= num + eventData)
      return;
    FriendMessageUserListModel.MessageUserInfo messageUserInfo = this.recvdata[num + eventData];
    GameSection.StayEvent();
    MonoBehaviourSingleton<FriendManager>.I.SendGetMessageDetailList(messageUserInfo.userId, 0, (Action<bool>) (is_success => GameSection.ResumeEvent(is_success)));
    MonoBehaviourSingleton<FriendManager>.I.SetNoReadMessageNum(MonoBehaviourSingleton<FriendManager>.I.noReadMessageNum - messageUserInfo.noReadNum);
    messageUserInfo.noReadNum = 0;
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_FRIEND_PARAM) != (GameSection.NOTIFY_FLAG) 0)
      this.isInitializeSendReopen = true;
    if ((flags & GameSection.NOTIFY_FLAG.UPDATE_FRIEND_LIST) != (GameSection.NOTIFY_FLAG) 0)
      this.SetDirtyTable();
    base.OnNotify(flags);
  }

  protected override GameSection.NOTIFY_FLAG GetUpdateUINotifyFlags()
  {
    return GameSection.NOTIFY_FLAG.UPDATE_FRIEND_LIST;
  }

  protected override void OnQuery_PAGE_PREV()
  {
    this.nowPage = (this.nowPage - 1 + this.pageNumMax) % this.pageNumMax;
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(this.GetUpdateUINotifyFlags());
  }

  protected override void OnQuery_PAGE_NEXT()
  {
    this.nowPage = (this.nowPage + 1) % this.pageNumMax;
    MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(this.GetUpdateUINotifyFlags());
  }

  protected override bool IsHideSwitchInfoButton()
  {
    if (this.recvdata != null)
    {
      FriendMessageUserListModel.MessageUserInfo[] array = this.recvdata.ToArray();
      return array == null || array.Length == 0;
    }
    return this.recvdata == null;
  }

  protected override void OnQuery_JOIN_FRIEND()
  {
    int eventData = (int) GameSection.GetEventData();
    if (this.recvdata == null || this.recvdata.Count <= eventData || eventData < 0)
      return;
    FriendCharaInfo.JoinInfo joinStatus = this.recvdata[eventData].joinStatus;
    if (joinStatus == null)
      return;
    GameSection.StayEvent();
    switch (joinStatus.joinType)
    {
      case 2:
        this.JoinLounge(joinStatus);
        break;
      case 3:
        if (!MonoBehaviourSingleton<PartyManager>.IsValid())
          break;
        MonoBehaviourSingleton<PartyManager>.I.SendApply(joinStatus.conditionParam, (Action<bool, Error>) ((isSucceed, error) =>
        {
          if (isSucceed)
            MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("QuestAccept", "QuestAcceptRoom");
          GameSection.ResumeEvent(true);
        }), joinStatus.targetParam);
        break;
      case 4:
        int _toUserId = int.Parse(joinStatus.conditionParam);
        this.JoinField(joinStatus.targetParam, _toUserId, (Action<bool, bool, bool>) ((is_matching, is_connect, is_regist) =>
        {
          if (!is_matching)
            GameSection.StopEvent();
          else if (!is_connect)
          {
            GameSection.StopEvent();
          }
          else
          {
            GameSection.ResumeEvent(is_regist);
            if (!is_regist)
              return;
            MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("InGame");
          }
        }));
        break;
      default:
        GameSection.ResumeEvent(true);
        break;
    }
  }

  protected override void OnQuery_SORT()
  {
    this.UpdateSortType();
    GameSaveData.instance.SetMutualFollowerListSortType((int) this.m_currentSortType);
    this.Sort<FriendMessageUserListModel.MessageUserInfo>(this.recvdata);
    this.RefreshUI();
  }

  protected override void UpdateSortType()
  {
    ++this.m_currentSortType;
    if (this.m_currentSortType < USER_SORT_TYPE.MAX)
      return;
    this.m_currentSortType = USER_SORT_TYPE.NAME;
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
    LBL_SORT,
  }
}
