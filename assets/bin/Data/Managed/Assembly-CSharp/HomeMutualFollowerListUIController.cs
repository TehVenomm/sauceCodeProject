// Decompiled with JetBrains decompiler
// Type: HomeMutualFollowerListUIController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class HomeMutualFollowerListUIController : ScrollItemListControllerBase
{
  private static readonly string LIST_ITEM_PREFAB_NAME = "FollowListBaseItem";
  private static readonly string WINDOW_TITLE_TEXT = "Mutual Follower List";
  private HomeVariableMemberListController m_listCtrl;
  private List<FriendMessageUserListModel.MessageUserInfo> m_userDataList;

  public HomeMutualFollowerListUIController()
  {
  }

  public HomeMutualFollowerListUIController(
    HomeMutualFollowerListUIController.InitParam _initParam)
    : base((ScrollItemListControllerBase.InitializeParameter) _initParam)
  {
    this.m_listCtrl = _initParam.ListCtrl;
  }

  protected override IEnumerator RequestNextPageInfo(int _nextPageNum, Action<bool, int> _callback)
  {
    MonoBehaviourSingleton<FriendManager>.I.SendGetMessageUserList(_nextPageNum, true, (Action<bool, FriendMessageUserListModel.Param>) ((is_success, recv_data) =>
    {
      if (is_success)
      {
        this.m_userDataList = recv_data.messageUser;
        this.m_maxPageNum = recv_data.pageNumMax;
        this.m_currentPageNum = this.MaxPageNum < 1 ? 0 : _nextPageNum % this.MaxPageNum;
      }
      if (_callback == null)
        return;
      _callback(is_success, _nextPageNum);
    }));
    yield return (object) null;
  }

  protected override void OnCallbackRequestPageInfo(bool _isSucceeded, int _nextPageNum)
  {
    if (_isSucceeded)
    {
      this.m_listCtrl.UpdateUI();
      this.m_listCtrl.SetCurrentPageNum(this.MaxPageNum < 1 ? 0 : this.CurrentPageNum + 1);
      this.m_listCtrl.SetMaxPageNum(this.MaxPageNum);
    }
    base.OnCallbackRequestPageInfo(_isSucceeded, _nextPageNum);
  }

  public override string GetItemPrefabName()
  {
    return HomeMutualFollowerListUIController.LIST_ITEM_PREFAB_NAME;
  }

  public override void SetListItem(int i, Transform t, bool is_recycle)
  {
    if (this.m_userDataList == null || this.m_userDataList.Count < 1 || this.m_userDataList.Count <= i || i < 0)
    {
      if (this.OnCompleteAllItemLoading == null)
        return;
      this.OnCompleteAllItemLoading(this.ItemLoadCompleteCount);
    }
    else
    {
      FriendMessageUserListModel.MessageUserInfo userData = this.m_userDataList[i];
      HomeMutualFollowerListItem component = ((Component) t).GetComponent<HomeMutualFollowerListItem>();
      if (Object.op_Equality((Object) component, (Object) null))
      {
        if (this.OnCompleteAllItemLoading == null)
          return;
        this.OnCompleteAllItemLoading(this.ItemLoadCompleteCount);
      }
      else
        component.Initialize(new HomeMutualFollowerListItem.InitParam()
        {
          CharacterInfo = (FriendCharaInfo) userData,
          Index = i,
          IsFollower = userData.follower,
          IsFollowing = userData.following,
          clanId = userData.userClanData != null ? userData.userClanData.cId : "",
          NoReadMsgNum = userData.noReadNum,
          IsPermittedMessage = userData.isPermitted,
          OnClickItem = new Action<int>(this.OnClickItem),
          IsUseRenderTextureCharaModel = !FieldManager.IsValidInField() && !FieldManager.IsValidInGame() && !FieldManager.IsValidInTutorial(),
          OnCompleteLoading = (System.Action) (() =>
          {
            this.IncrementLoadCompleteCount();
            if (this.OnCompleteAllItemLoading == null)
              return;
            this.OnCompleteAllItemLoading(this.ItemLoadCompleteCount);
          })
        });
    }
  }

  public override int GetItemListDataCount()
  {
    return this.m_userDataList != null ? this.m_userDataList.Count : 0;
  }

  public override string GetChatTitle() => HomeMutualFollowerListUIController.WINDOW_TITLE_TEXT;

  protected void OnClickItem(int _itemIndex)
  {
    if (this.m_userDataList == null || _itemIndex < 0 || this.m_userDataList.Count <= _itemIndex)
      return;
    if (!this.m_userDataList[_itemIndex].isPermitted)
    {
      MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, StringTable.GetErrorMessage(13022U)), (Action<string>) (ret => { }), true, 13022);
    }
    else
    {
      if (!MonoBehaviourSingleton<FriendManager>.IsValid())
        return;
      MonoBehaviourSingleton<FriendManager>.I.SendGetMessageDetailList(this.m_userDataList[_itemIndex].userId, 0, true, (Action<bool>) (flag =>
      {
        if (!flag || !Object.op_Inequality((Object) this.m_listCtrl, (Object) null))
          return;
        this.m_listCtrl.OnClickItem();
      }));
    }
  }

  public class InitParam : ScrollItemListControllerBase.InitializeParameter
  {
    public HomeVariableMemberListController ListCtrl;
  }
}
