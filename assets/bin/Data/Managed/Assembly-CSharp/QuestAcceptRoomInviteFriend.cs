// Decompiled with JetBrains decompiler
// Type: QuestAcceptRoomInviteFriend
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class QuestAcceptRoomInviteFriend : FollowListBase
{
  protected const int LIST_ITEM_COUNT_PER_PAGE = 10;
  protected PartyInviteCharaInfo[] inviteUsers;
  protected List<int> selectedUserIdList = new List<int>();

  public override void Initialize()
  {
    int inviteListSortType = GameSaveData.instance.MutualFollowerInviteListSortType;
    if (0 <= inviteListSortType && inviteListSortType < 5)
      this.m_currentSortType = (USER_SORT_TYPE) inviteListSortType;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.UpdateListUI();
    this.SetToggle((Enum) QuestAcceptRoomInviteFriend.UI.TGL_OK, this.selectedUserIdList.Count > 0);
    this.SetLabelText((Enum) QuestAcceptRoomInviteFriend.UI.LBL_SELECT_ALL, this.IsContainsAllUserInPage() ? StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 40U) : StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 41U));
    this.SetActive((Enum) QuestAcceptRoomInviteFriend.UI.BTN_CHECK_PAGE_ALL_ITEM, this.inviteUsers != null && this.inviteUsers.Length != 0);
  }

  protected void UpdateListUI()
  {
    this.SetLabelText((Enum) QuestAcceptRoomInviteFriend.UI.STR_TITLE, this.sectionData.GetText("STR_TITLE"));
    this.SetLabelText((Enum) QuestAcceptRoomInviteFriend.UI.STR_TITLE_REFLECT, this.sectionData.GetText("STR_TITLE"));
    this.SetLabelText((Enum) QuestAcceptRoomInviteFriend.UI.LBL_SORT, StringTable.Get(STRING_CATEGORY.USER_SORT, (uint) this.m_currentSortType));
    if (this.inviteUsers == null || this.inviteUsers.Length == 0)
    {
      this.SetActive((Enum) QuestAcceptRoomInviteFriend.UI.STR_NON_LIST, true);
      this.SetActive((Enum) QuestAcceptRoomInviteFriend.UI.GRD_LIST, false);
      this.SetLabelText((Enum) QuestAcceptRoomInviteFriend.UI.LBL_NOW, $"{"0"}/{"0"}");
      this.SetActive((Enum) QuestAcceptRoomInviteFriend.UI.OBJ_ACTIVE_ROOT, this.pageNumMax > 1);
      this.SetActive((Enum) QuestAcceptRoomInviteFriend.UI.OBJ_INACTIVE_ROOT, this.pageNumMax == 1);
    }
    else
    {
      this.SetLabelText((Enum) QuestAcceptRoomInviteFriend.UI.LBL_NOW, $"{this.nowPage + 1}/{this.pageNumMax}");
      this.SetActive((Enum) QuestAcceptRoomInviteFriend.UI.OBJ_ACTIVE_ROOT, this.pageNumMax > 1);
      this.SetActive((Enum) QuestAcceptRoomInviteFriend.UI.OBJ_INACTIVE_ROOT, this.pageNumMax == 1);
      this.SetActive((Enum) QuestAcceptRoomInviteFriend.UI.STR_NON_LIST, false);
      this.SetActive((Enum) QuestAcceptRoomInviteFriend.UI.GRD_LIST, true);
      int currentPageItemLength = this.GetCurrentPageItemLength();
      PartyInviteCharaInfo[] currentList = new PartyInviteCharaInfo[currentPageItemLength];
      for (int index = 0; index < currentPageItemLength; ++index)
        currentList[index] = this.inviteUsers[this.nowPage * 10 + index];
      this.SetDynamicList((Enum) QuestAcceptRoomInviteFriend.UI.GRD_LIST, "QuestInviteeSelectListItem", currentPageItemLength, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        PartyInviteCharaInfo info = currentList[i];
        this.SetupListItem(info, i, t, is_recycle);
        string clanId = info.userClanData != null ? info.userClanData.cId : "0";
        this.SetFollowStatus(t, info.userId, info.following, info.follower, clanId);
      }));
    }
  }

  protected int GetCurrentPageItemLength()
  {
    return this.nowPage + 1 >= this.pageNumMax ? Mathf.Clamp(this.inviteUsers.Length - this.nowPage * 10, 0, 10) : 10;
  }

  protected void SortArray()
  {
    if (this.inviteUsers == null || this.inviteUsers.Length == 0)
      return;
    switch (this.m_currentSortType)
    {
      case USER_SORT_TYPE.NAME:
        Array.Sort<PartyInviteCharaInfo>(this.inviteUsers, new Comparison<PartyInviteCharaInfo>(((FollowListBase) this).UserCompareByName<PartyInviteCharaInfo>));
        break;
      case USER_SORT_TYPE.LEVEL:
        Array.Sort<PartyInviteCharaInfo>(this.inviteUsers, new Comparison<PartyInviteCharaInfo>(((FollowListBase) this).UserCompareByLevel<PartyInviteCharaInfo>));
        break;
      case USER_SORT_TYPE.LOGIN:
        Array.Sort<PartyInviteCharaInfo>(this.inviteUsers, new Comparison<PartyInviteCharaInfo>(((FollowListBase) this).UserCompareByLoginTime<PartyInviteCharaInfo>));
        break;
      case USER_SORT_TYPE.PLAY_COUNT:
        Array.Sort<PartyInviteCharaInfo>(this.inviteUsers, new Comparison<PartyInviteCharaInfo>(((FollowListBase) this).UserCompareByPlayCount<PartyInviteCharaInfo>));
        break;
      case USER_SORT_TYPE.REGISTER:
        Array.Sort<PartyInviteCharaInfo>(this.inviteUsers, new Comparison<PartyInviteCharaInfo>(((FollowListBase) this).UserCompareByResistered<PartyInviteCharaInfo>));
        break;
    }
  }

  protected virtual void SetupListItem(
    PartyInviteCharaInfo info,
    int i,
    Transform t,
    bool is_recycle)
  {
    this.SetCharaInfo((FriendCharaInfo) info, i, t, is_recycle, false);
    this.SetActive(t, (Enum) QuestAcceptRoomInviteFriend.UI.OBJ_DISABLE_USER_MASK, !info.canEntry || info.invite || this.selectedUserIdList.Contains(info.userId));
    this.SetActive(t, (Enum) QuestAcceptRoomInviteFriend.UI.OBJ_INVITED, info.invite);
    this.SetButtonEnabled(t, !info.invite && info.canEntry);
    this.SetActive(t, (Enum) QuestAcceptRoomInviteFriend.UI.OBJ_SELECTED, this.selectedUserIdList.Contains(info.userId));
    this.SetActive(t, (Enum) QuestAcceptRoomInviteFriend.UI.OBJ_NOT_PROGRESSED, !info.canEntry);
    this.SetActive(t, (Enum) QuestAcceptRoomInviteFriend.UI.OBJ_ROOMCONDITION, false);
  }

  protected override void SendGetList(int page, Action<bool> callback)
  {
    MonoBehaviourSingleton<PartyManager>.I.SendInviteList((Action<bool, PartyInviteCharaInfo[]>) ((is_success, recv_data) =>
    {
      if (is_success)
      {
        this.nowPage = 0;
        this.pageNumMax = recv_data != null ? Mathf.CeilToInt((float) recv_data.Length / 10f) : 1;
        this.inviteUsers = recv_data;
        this.SortArray();
      }
      if (callback == null)
        return;
      callback(is_success);
    }));
  }

  public override void OnQuery_FOLLOW_INFO()
  {
    int index = (int) GameSection.GetEventData() + this.nowPage * 10;
    if (index < 0 || index >= this.inviteUsers.Length)
      return;
    PartyInviteCharaInfo inviteUser = this.inviteUsers[index];
    if (!inviteUser.canEntry)
      return;
    if (this.selectedUserIdList.Contains(inviteUser.userId))
      this.selectedUserIdList.Remove(inviteUser.userId);
    else
      this.selectedUserIdList.Add(inviteUser.userId);
    this.RefreshUI();
  }

  protected override void OnQuery_SORT()
  {
    this.UpdateSortType();
    GameSaveData.instance.SetMutualFollowerInviteListSortType((int) this.m_currentSortType);
    this.SortArray();
    this.RefreshUI();
  }

  private void OnQuery_OK()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<PartyManager>.I.SendInvite(this.selectedUserIdList.ToArray(), (Action<bool, int[]>) ((is_success, invited_users) => GameSection.ResumeEvent(is_success)));
  }

  protected new void OnQuery_PAGE_PREV()
  {
    this.nowPage = (this.nowPage - 1 + this.pageNumMax) % this.pageNumMax;
    this.RefreshUI();
  }

  protected new void OnQuery_PAGE_NEXT()
  {
    this.nowPage = (this.nowPage + 1) % this.pageNumMax;
    this.RefreshUI();
  }

  protected bool IsContainsAllUserInPage()
  {
    int currentPageItemLength = this.GetCurrentPageItemLength();
    int num = this.nowPage * 10;
    bool flag = false;
    for (int index = 0; index < currentPageItemLength; ++index)
    {
      PartyInviteCharaInfo inviteUser = this.inviteUsers[num + index];
      if (this.IsEnableInvite(inviteUser) && !this.selectedUserIdList.Contains(inviteUser.userId))
      {
        flag = true;
        break;
      }
    }
    return !flag;
  }

  protected virtual bool IsEnableInvite(PartyInviteCharaInfo _info)
  {
    return _info != null && _info.canEntry && !_info.invite;
  }

  protected void OnQuery_CHECK_PAGE_ITEM()
  {
    int currentPageItemLength = this.GetCurrentPageItemLength();
    int num = this.nowPage * 10;
    if (!this.IsContainsAllUserInPage())
    {
      for (int index = 0; index < currentPageItemLength; ++index)
      {
        PartyInviteCharaInfo inviteUser = this.inviteUsers[num + index];
        if (this.IsEnableInvite(inviteUser) && !this.selectedUserIdList.Contains(inviteUser.userId))
          this.selectedUserIdList.Add(inviteUser.userId);
      }
    }
    else
    {
      for (int index = 0; index < currentPageItemLength; ++index)
      {
        PartyInviteCharaInfo inviteUser = this.inviteUsers[num + index];
        if (this.IsEnableInvite(inviteUser) && this.selectedUserIdList.Contains(inviteUser.userId))
          this.selectedUserIdList.Remove(inviteUser.userId);
      }
    }
    this.RefreshUI();
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
    TGL_OK,
    BTN_OK,
    BTN_ALL,
    OBJ_SELECTED,
    OBJ_INVITED,
    OBJ_ROOMCONDITION,
    OBJ_NOT_PROGRESSED,
    LBL_SORT,
    LBL_SELECT_ALL,
    BTN_CHECK_PAGE_ALL_ITEM,
  }
}
