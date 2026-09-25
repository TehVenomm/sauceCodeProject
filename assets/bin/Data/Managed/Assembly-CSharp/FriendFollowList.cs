// Decompiled with JetBrains decompiler
// Type: FriendFollowList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class FriendFollowList : FollowListBase
{
  public override void Initialize()
  {
    int followListSortType = GameSaveData.instance.FollowListSortType;
    if (0 <= followListSortType && followListSortType < 5)
      this.m_currentSortType = (USER_SORT_TYPE) followListSortType;
    this.titleType = FollowListBase.TITLE_TYPE.FOLLOW;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetActive((Enum) FollowListBase.UI.OBJ_FOLLOW_NUMBER_ROOT, true);
    this.SetLabelText((Enum) FollowListBase.UI.LBL_FOLLOW_NUMBER_NOW, MonoBehaviourSingleton<FriendManager>.I.followNum.ToString());
    this.SetLabelText((Enum) FollowListBase.UI.LBL_FOLLOW_NUMBER_MAX, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxFollow.ToString());
    this.ListUI();
  }

  protected override void UpdateDynamicList()
  {
    FriendCharaInfo[] currentUserArray = this.GetCurrentUserArray();
    int pageItemLength = this.GetPageItemLength(this.nowPage);
    int num = this.nowPage * 10;
    if (pageItemLength < 1 || currentUserArray.Length < 1 || currentUserArray.Length < num + pageItemLength)
      return;
    FriendCharaInfo[] info = new FriendCharaInfo[pageItemLength];
    for (int index = 0; index < pageItemLength; ++index)
      info[index] = currentUserArray[num + index];
    if (GameDefine.ACTIVE_DEGREE)
      this.ScrollGrid.cellHeight = (float) GameDefine.DEGREE_FRIEND_LIST_HEIGHT;
    this.CleanItemList();
    this.SetDynamicList((Enum) FollowListBase.UI.GRD_LIST, this.GetListItemName, pageItemLength, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) => this.SetListItem(i, t, is_recycle, info[i])));
  }

  private int GetPageItemLength(int currentPage)
  {
    return currentPage + 1 < this.pageNumMax || this.recvList.Count % 10 <= 0 ? 10 : this.recvList.Count % 10;
  }

  protected override void SendGetList(int page, Action<bool> callback)
  {
    MonoBehaviourSingleton<FriendManager>.I.SendGetFollowList(page, (Action<bool, FriendFollowListModel.Param>) ((is_success, recv_data) =>
    {
      if (is_success)
      {
        this.recvList = recv_data.follow;
        this.nowPage = page;
        this.pageNumMax = Mathf.CeilToInt((float) this.recvList.Count / 10f);
        this.Sort<FriendCharaInfo>(this.recvList);
      }
      if (callback == null)
        return;
      callback(is_success);
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

  public void OnQuery_MUTUAL_FOLLOW_MESSAGE()
  {
    if (!MonoBehaviourSingleton<FriendManager>.IsValid())
      GameSection.StopEvent();
    else if (MonoBehaviourSingleton<FriendManager>.I.mutualFollowResult == null)
      GameSection.StopEvent();
    else
      GameSection.SetEventData((object) new object[1]
      {
        (object) MonoBehaviourSingleton<FriendManager>.I.mutualFollowResult.targetUserName
      });
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

  protected override void OnQuery_SORT()
  {
    if (this.recvList.Count > 0)
    {
      base.OnQuery_SORT();
      GameSaveData.instance.SetFollowListSortType((int) this.m_currentSortType);
    }
    else
      MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, "You have no Following Hunters", StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 100U)), (Action<string>) (ret => { }));
  }
}
