// Decompiled with JetBrains decompiler
// Type: FriendFollowerList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FriendFollowerList : FollowListBase
{
  private int m_currentFollowerCount;
  private int m_maxFollowerCount = 1000;
  private int m_chunkSize = 100;
  protected bool IsConnect;
  private FriendCharaInfo[][] m_cachedUserDataList = new FriendCharaInfo[5][];
  private int m_nextPage;

  public override void Initialize()
  {
    this.m_currentFollowerCount = MonoBehaviourSingleton<FriendManager>.I.followerNum;
    this.m_maxFollowerCount = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.FRIEND_MAX_FOLLOWER;
    for (int index = 0; index < this.m_cachedUserDataList.Length; ++index)
      this.m_cachedUserDataList[index] = new FriendCharaInfo[this.m_maxFollowerCount];
    int followerListSortType = GameSaveData.instance.FollowerListSortType;
    if (0 <= followerListSortType && followerListSortType < 5)
      this.m_currentSortType = (USER_SORT_TYPE) followerListSortType;
    this.titleType = FollowListBase.TITLE_TYPE.FOLLOWER;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetActive((Enum) FollowListBase.UI.OBJ_FOLLOW_NUMBER_ROOT, true);
    this.SetLabelText((Enum) FollowListBase.UI.LBL_FOLLOW_NUMBER_NOW, this.m_currentFollowerCount.ToString());
    this.SetLabelText((Enum) FollowListBase.UI.LBL_FOLLOW_NUMBER_MAX, this.m_maxFollowerCount.ToString());
    this.ListUI();
  }

  protected override void UpdateDynamicList()
  {
    FriendCharaInfo[] currentUserArray = this.GetCurrentUserArray();
    if (((IList<FriendCharaInfo>) currentUserArray).IsNullOrEmpty<FriendCharaInfo>())
      return;
    int currentPageItemLength = this.GetCurrentPageItemLength();
    FriendCharaInfo[] currentList = new FriendCharaInfo[currentPageItemLength];
    for (int index = 0; index < currentPageItemLength; ++index)
      currentList[index] = currentUserArray[this.nowPage * 10 + index];
    if (GameDefine.ACTIVE_DEGREE)
      ((Component) this.GetCtrl((Enum) FollowListBase.UI.GRD_LIST)).GetComponent<UIGrid>().cellHeight = (float) GameDefine.DEGREE_FRIEND_LIST_HEIGHT;
    this.CleanItemList();
    this.SetDynamicList((Enum) FollowListBase.UI.GRD_LIST, this.GetListItemName, currentPageItemLength, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) => this.SetListItem(i, t, is_recycle, currentList[i])));
  }

  private int GetCurrentPageItemLength() => this.GetPageItemLength(this.nowPage);

  private int GetPageItemLength(int currentPage)
  {
    return currentPage + 1 != this.pageNumMax || this.m_currentFollowerCount % 10 <= 0 ? 10 : this.m_currentFollowerCount % 10;
  }

  private int GetChunkIndex(int pageIndex)
  {
    return this.m_chunkSize < 1 || pageIndex < 0 ? 0 : Mathf.FloorToInt((float) (pageIndex * 10 / this.m_chunkSize));
  }

  protected override void SendGetList(int page, Action<bool> callback)
  {
    if (this.IsConnect)
      return;
    int chunkIndex = this.GetChunkIndex(page);
    this.IsConnect = true;
    MonoBehaviourSingleton<FriendManager>.I.SendGetFollowerList(chunkIndex, (int) this.m_currentSortType, (Action<bool, FriendFollowerListModel.Param>) ((is_success, recv_data) =>
    {
      if (is_success)
      {
        this.m_chunkSize = recv_data.chunkSize;
        this.m_currentFollowerCount = recv_data.totalFollowers > 0 ? recv_data.totalFollowers : recv_data.follow.Count;
        int num = chunkIndex * this.m_chunkSize;
        int index = 0;
        for (int count = recv_data.follow.Count; index < count; ++index)
        {
          FriendCharaInfo[] currentUserArray = this.GetCurrentUserArray();
          if (!((IList<FriendCharaInfo>) currentUserArray).IsNullOrEmpty<FriendCharaInfo>())
            currentUserArray[num + index] = recv_data.follow[index];
          else
            break;
        }
        if (this.m_nextPage != 0)
          this.nowPage = this.m_nextPage;
        this.pageNumMax = Mathf.CeilToInt((float) this.m_currentFollowerCount / 10f);
      }
      if (callback != null)
        callback(is_success);
      this.m_nextPage = 0;
      this.IsConnect = false;
    }));
  }

  protected override void PostSendGetListByReopen(int page)
  {
    this.SetDirtyTable();
    base.PostSendGetListByReopen(page);
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

  public override void OnQuery_FOLLOW_INFO()
  {
    int eventData = (int) GameSection.GetEventData();
    FriendCharaInfo[] currentUserArray = this.GetCurrentUserArray();
    int index = eventData + this.nowPage * 10;
    if (index < 0 || ((IList<FriendCharaInfo>) currentUserArray).IsNullOrEmpty<FriendCharaInfo>() || currentUserArray.Length <= index)
      return;
    FriendCharaInfo event_data = currentUserArray[index];
    MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = eventData + 4;
    GameSection.SetEventData((object) event_data);
  }

  protected override FriendCharaInfo[] GetCurrentUserArray()
  {
    return this.m_currentFollowerCount <= 0 ? (FriendCharaInfo[]) null : this.m_cachedUserDataList[(int) this.m_currentSortType];
  }

  private bool HasNullOrEmptyData(int _nextPage)
  {
    FriendCharaInfo[] currentUserArray = this.GetCurrentUserArray();
    if (currentUserArray == null)
      return true;
    int num = _nextPage * 10;
    int pageItemLength = this.GetPageItemLength(_nextPage);
    for (int index = 0; index < pageItemLength; ++index)
    {
      if (currentUserArray[num + index] == null)
        return true;
    }
    return false;
  }

  protected override void OnQuery_PAGE_PREV()
  {
    if (this.IsConnect)
      return;
    int _nextPage = (this.nowPage - 1 + this.pageNumMax) % this.pageNumMax;
    if (!this.HasNullOrEmptyData(_nextPage))
    {
      this.nowPage = _nextPage;
      MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(this.GetUpdateUINotifyFlags());
    }
    else
    {
      this.m_nextPage = _nextPage;
      base.OnQuery_PAGE_PREV();
    }
  }

  protected override void OnQuery_PAGE_NEXT()
  {
    if (this.IsConnect)
      return;
    int _nextPage = (this.nowPage + 1) % this.pageNumMax;
    if (!this.HasNullOrEmptyData(_nextPage))
    {
      this.nowPage = _nextPage;
      MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(this.GetUpdateUINotifyFlags());
    }
    else
    {
      this.m_nextPage = _nextPage;
      base.OnQuery_PAGE_NEXT();
    }
  }

  protected override void OnQuery_SORT()
  {
    if (this.m_currentFollowerCount > 0)
    {
      if (this.IsConnect)
        return;
      this.UpdateSortType();
      GameSaveData.instance.SetFollowerListSortType((int) this.m_currentSortType);
      if (!this.HasNullOrEmptyData(this.nowPage))
      {
        MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(this.GetUpdateUINotifyFlags());
      }
      else
      {
        GameSection.StayEvent();
        this.SendGetList(this.nowPage, (Action<bool>) (ret => GameSection.ResumeEvent(ret)));
      }
    }
    else
      MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, "You have no Follower Hunters", StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 100U)), (Action<string>) (ret => { }));
  }
}
