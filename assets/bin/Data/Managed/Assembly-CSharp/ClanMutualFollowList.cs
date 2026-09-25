// Decompiled with JetBrains decompiler
// Type: ClanMutualFollowList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ClanMutualFollowList : FollowListBase
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
    this.SetLabelText((Enum) ClanMutualFollowList.UI.STR_TITLE, this.sectionData.GetText("STR_TITLE"));
    this.SetLabelText((Enum) ClanMutualFollowList.UI.STR_TITLE_REFLECT, this.sectionData.GetText("STR_TITLE"));
    this.SetLabelText((Enum) ClanMutualFollowList.UI.LBL_SORT, StringTable.Get(STRING_CATEGORY.USER_SORT, (uint) this.m_currentSortType));
    FriendMessageUserListModel.MessageUserInfo[] array = this.recvdata.ToArray();
    if (array == null || array.Length == 0)
    {
      this.SetActive((Enum) ClanMutualFollowList.UI.STR_NON_LIST, true);
      this.SetActive((Enum) ClanMutualFollowList.UI.GRD_LIST, false);
      this.SetActive((Enum) ClanMutualFollowList.UI.OBJ_ACTIVE_ROOT, false);
      this.SetActive((Enum) ClanMutualFollowList.UI.OBJ_INACTIVE_ROOT, true);
      this.SetLabelText((Enum) ClanMutualFollowList.UI.LBL_NOW, "0");
      this.SetLabelText((Enum) ClanMutualFollowList.UI.LBL_MAX, "0");
    }
    else
    {
      this.SetPageNumText((Enum) ClanMutualFollowList.UI.LBL_NOW, this.nowPage + 1);
      this.SetPageNumText((Enum) ClanMutualFollowList.UI.LBL_MAX, this.pageNumMax);
      this.SetActive((Enum) ClanMutualFollowList.UI.STR_NON_LIST, false);
      this.SetActive((Enum) ClanMutualFollowList.UI.GRD_LIST, true);
      this.SetActive((Enum) ClanMutualFollowList.UI.OBJ_ACTIVE_ROOT, this.pageNumMax != 1);
      this.SetActive((Enum) ClanMutualFollowList.UI.OBJ_INACTIVE_ROOT, this.pageNumMax == 1);
      this.UpdateDynamicList();
    }
  }

  private FriendMessageUserListModel.MessageUserInfo[] GetMessageUserInfo()
  {
    FriendMessageUserListModel.MessageUserInfo[] array = this.recvdata.ToArray();
    int pageItemLength = this.GetPageItemLength(this.nowPage);
    int num = this.nowPage * 10;
    if (pageItemLength < 1 || array.Length < 1 || array.Length < num + pageItemLength)
      return (FriendMessageUserListModel.MessageUserInfo[]) null;
    FriendMessageUserListModel.MessageUserInfo[] messageUserInfo = new FriendMessageUserListModel.MessageUserInfo[pageItemLength];
    for (int index = 0; index < pageItemLength; ++index)
      messageUserInfo[index] = array[num + index];
    return messageUserInfo;
  }

  protected override void UpdateDynamicList()
  {
    int pageItemLength = this.GetPageItemLength(this.nowPage);
    FriendMessageUserListModel.MessageUserInfo[] info = this.GetMessageUserInfo();
    if (GameDefine.ACTIVE_DEGREE)
      this.ScrollGrid.cellHeight = (float) GameDefine.DEGREE_FRIEND_LIST_HEIGHT;
    this.CleanItemList();
    this.SetDynamicList((Enum) ClanMutualFollowList.UI.GRD_LIST, "FriendInClanListItem", pageItemLength, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      FriendMessageUserListModel.MessageUserInfo data = info[i];
      string clanId = data.userClanData != null ? data.userClanData.cId : "0";
      this.SetFollowStatus(t, data.userId, true, true, clanId);
      this.SetCharaInfo((FriendCharaInfo) data, i, t, is_recycle, data.userId == 0);
      this.SetBadge(t, data.noReadNum, (SpriteAlignment) 3, -10, -6);
    }));
  }

  protected override void SetCharaInfo(
    FriendCharaInfo data,
    int i,
    Transform t,
    bool is_recycle,
    bool isGM)
  {
    base.SetCharaInfo(data, i, t, is_recycle, isGM);
    this.SetUserClanInfo(t, i, data.userClanData);
    if (data.userClanData.IsRegistered())
      this.SetEvent(t, "DETAIL", i);
    else
      this.SetEvent(t, "NONE", i);
  }

  protected override void SetJoinInfo(
    Transform t,
    int _itemIndex,
    FriendCharaInfo.JoinInfo _joinStatus,
    string _lastLoginText)
  {
  }

  protected void SetUserClanInfo(Transform t, int _itemIndex, UserClanData userClanData)
  {
    if (userClanData.IsRegistered())
    {
      this.SetActive(t, (Enum) ClanMutualFollowList.UI.IN_CLAN_TEXT_ROOT, true);
      this.SetActive(t, (Enum) ClanMutualFollowList.UI.NOT_IN_CLAN_TEXT_ROOT, false);
      this.SetStatusSprite(t, userClanData);
      this.SetLabelText(t, (Enum) ClanMutualFollowList.UI.LBL_CLAN_NAME, userClanData.name);
    }
    else
    {
      this.SetActive(t, (Enum) ClanMutualFollowList.UI.IN_CLAN_TEXT_ROOT, false);
      this.SetActive(t, (Enum) ClanMutualFollowList.UI.NOT_IN_CLAN_TEXT_ROOT, true);
    }
  }

  private void SetStatusSprite(Transform root, UserClanData userClan)
  {
    if (userClan.IsLeader())
    {
      this.SetActive(root, (Enum) ClanMutualFollowList.UI.SPR_CLAN_STATUS, true);
      this.SetSprite(root, (Enum) ClanMutualFollowList.UI.SPR_CLAN_STATUS, "Clan_HeadmasterIcon");
    }
    else if (userClan.IsSubLeader())
    {
      this.SetActive(root, (Enum) ClanMutualFollowList.UI.SPR_CLAN_STATUS, true);
      this.SetSprite(root, (Enum) ClanMutualFollowList.UI.SPR_CLAN_STATUS, "Clan_DeputyHeadmasterIcon");
    }
    else
      this.SetActive(root, (Enum) ClanMutualFollowList.UI.SPR_CLAN_STATUS, false);
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

  public void OnQuery_DETAIL()
  {
    int eventData = (int) GameSection.GetEventData();
    FriendMessageUserListModel.MessageUserInfo[] messageUserInfo = this.GetMessageUserInfo();
    if (eventData < 0 || ((IList<FriendMessageUserListModel.MessageUserInfo>) messageUserInfo).IsNullOrEmpty<FriendMessageUserListModel.MessageUserInfo>() || messageUserInfo.Length <= eventData)
      GameSection.StopEvent();
    else
      GameSection.SetEventData((object) messageUserInfo[eventData].userClanData.cId);
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

  protected override bool IsHideSwitchInfoButton() => false;

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
    IN_CLAN_TEXT_ROOT,
    NOT_IN_CLAN_TEXT_ROOT,
    SPR_CLAN_STATUS,
    LBL_CLAN_NAME,
  }
}
