// Decompiled with JetBrains decompiler
// Type: ChatMessageUserUIController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ChatMessageUserUIController
{
  private static readonly string MESSAGE_USER_ITEM_PREFAB_PATH = "InternalUI/UI_Friend/FollowListBaseItem";
  private GameObject m_userItemPrefab;
  private bool m_isConnecting;
  private UIWidget m_rootWidget;
  private Transform m_itemListParent;
  private int m_visibleItemCount;
  private List<FriendMessageUserListModel.MessageUserInfo> m_apiResponce;
  private List<HomeMutualFollowerListItem> m_currentItemList = new List<HomeMutualFollowerListItem>();
  private Queue<HomeMutualFollowerListItem> m_itemPool = new Queue<HomeMutualFollowerListItem>();
  private System.Action m_OnClickItem;

  public bool IsConnecting => this.m_isConnecting;

  private void SetStartConnecting() => this.m_isConnecting = true;

  private void SetEndConnecting() => this.m_isConnecting = false;

  public ChatMessageUserUIController()
  {
  }

  public ChatMessageUserUIController(ChatMessageUserUIController.InitParam _param)
  {
    if (_param == null)
      return;
    this.m_rootWidget = _param.RootWidget;
    this.m_itemListParent = _param.ItemListParent;
    this.m_visibleItemCount = _param.ItemVisibleCount;
    this.m_OnClickItem = _param.OnClickItem;
  }

  public IEnumerator LoadInternalResources(MonoBehaviour _coroutineExecutor)
  {
    if (!Object.op_Inequality((Object) this.m_userItemPrefab, (Object) null))
    {
      LoadingQueue loadingQueue = new LoadingQueue(_coroutineExecutor);
      LoadObject loadObject_ListItem = loadingQueue.Load(RESOURCE_CATEGORY.UI, "FollowListBaseItem");
      if (loadingQueue.IsLoading())
        yield return (object) loadingQueue.Wait();
      this.m_userItemPrefab = loadObject_ListItem.loadedObject as GameObject;
    }
  }

  public IEnumerator SendRequestMessagingPersonList(MonoBehaviour _coroutineExecutor)
  {
    if (MonoBehaviourSingleton<FriendManager>.IsValid())
    {
      if (Object.op_Equality((Object) this.m_userItemPrefab, (Object) null))
        yield return (object) this.LoadInternalResources(_coroutineExecutor);
      this.SetStartConnecting();
      MonoBehaviourSingleton<FriendManager>.I.SendGetUserListMessagedOnce(true, (Action<bool, FriendMessagedMutualFollowerListModel.Param>) ((is_success, recv_data) =>
      {
        if (is_success)
        {
          this.m_apiResponce = recv_data.messageFollowList;
          this.GenerateMessageUserList(recv_data.messageFollowList);
        }
        this.SetEndConnecting();
      }));
      while (this.IsConnecting)
        yield return (object) null;
    }
  }

  public void HideAll()
  {
  }

  public void ShowAll()
  {
  }

  protected void GenerateMessageUserList(
    List<FriendMessageUserListModel.MessageUserInfo> recv_data)
  {
    this.SetRootAlpha(0.0f);
    this.RecycleItem();
    int len = recv_data != null ? recv_data.Count : 0;
    this.m_currentItemList = this.GetItemObjects(len, this.m_itemListParent);
    int loadCompleteCount = 0;
    for (int index = 0; index < len; ++index)
    {
      HomeMutualFollowerListItem currentItem = this.m_currentItemList[index];
      ((Component) currentItem).transform.localPosition = Vector3.op_Multiply(Vector3.down, (float) index * 130f);
      ((Component) currentItem).transform.localScale = Vector3.op_Multiply(Vector3.one, 0.98f);
      currentItem.Initialize(new HomeMutualFollowerListItem.InitParam()
      {
        CharacterInfo = (FriendCharaInfo) recv_data[index],
        Index = index,
        IsFollower = recv_data[index].follower,
        IsFollowing = recv_data[index].following,
        clanId = recv_data[index].userClanData != null ? recv_data[index].userClanData.cId : "",
        NoReadMsgNum = recv_data[index].noReadNum,
        IsPermittedMessage = recv_data[index].isPermitted,
        IsUseRenderTextureCharaModel = !FieldManager.IsValidInField() && !FieldManager.IsValidInGame() && !FieldManager.IsValidInTutorial(),
        OnClickItem = new Action<int>(this.OnClickItem),
        OnCompleteLoading = (System.Action) (() =>
        {
          ++loadCompleteCount;
          if (loadCompleteCount < Mathf.Min(len, this.m_visibleItemCount))
            return;
          this.SetRootAlpha(1f);
        })
      });
    }
  }

  private void RecycleItem()
  {
    int index = 0;
    for (int count = this.m_currentItemList.Count; index < count; ++index)
    {
      this.m_currentItemList[index].HideAll();
      this.m_itemPool.Enqueue(this.m_currentItemList[index]);
    }
    this.m_currentItemList.Clear();
  }

  private List<HomeMutualFollowerListItem> GetItemObjects(int _requestCount, Transform _parentObj)
  {
    List<HomeMutualFollowerListItem> itemObjects = new List<HomeMutualFollowerListItem>();
    if (Object.op_Equality((Object) _parentObj, (Object) null))
      return itemObjects;
    if (_requestCount < this.m_itemPool.Count)
    {
      for (int index = 0; index < _requestCount; ++index)
        itemObjects.Add(this.m_itemPool.Dequeue());
      return itemObjects;
    }
    while (this.m_itemPool.Count > 0)
      itemObjects.Add(this.m_itemPool.Dequeue());
    if (Object.op_Equality((Object) this.m_userItemPrefab, (Object) null))
      return itemObjects;
    int num = _requestCount - itemObjects.Count;
    for (int index = 0; index < num; ++index)
    {
      Transform transform = ResourceUtility.Realizes((Object) this.m_userItemPrefab, _parentObj, 5);
      if (!Object.op_Equality((Object) transform, (Object) null))
      {
        HomeMutualFollowerListItem component = ((Component) transform).GetComponent<HomeMutualFollowerListItem>();
        if (!Object.op_Equality((Object) component, (Object) null))
          itemObjects.Add(component);
      }
    }
    return itemObjects;
  }

  protected void OnClickItem(int _itemIndex)
  {
    if (this.m_apiResponce == null || _itemIndex < 0 || this.m_apiResponce.Count <= _itemIndex)
      return;
    FriendMessageUserListModel.MessageUserInfo messageUserInfo = this.m_apiResponce[_itemIndex];
    if (messageUserInfo == null)
      return;
    if (!messageUserInfo.isPermitted)
    {
      MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, StringTable.GetErrorMessage(13022U)), (Action<string>) (ret => { }), true, 13022);
    }
    else
    {
      if (!MonoBehaviourSingleton<FriendManager>.IsValid())
        return;
      MonoBehaviourSingleton<FriendManager>.I.SendGetMessageDetailList(messageUserInfo.userId, 0, true, (Action<bool>) (flag =>
      {
        if (this.m_OnClickItem == null)
          return;
        this.m_OnClickItem();
      }));
    }
  }

  private void SetRootAlpha(float _value)
  {
    if (Object.op_Equality((Object) this.m_rootWidget, (Object) null))
      return;
    this.m_rootWidget.alpha = Mathf.Clamp01(_value);
  }

  public void ClearList()
  {
    int index = 0;
    for (int count = this.m_currentItemList.Count; index < count; ++index)
    {
      this.m_currentItemList[index].CleanRenderTexture();
      Object.Destroy((Object) ((Component) this.m_currentItemList[index]).gameObject);
      this.m_currentItemList[index] = (HomeMutualFollowerListItem) null;
    }
    this.m_currentItemList.Clear();
  }

  public class InitParam
  {
    public UIWidget RootWidget;
    public Transform ItemListParent;
    public int ItemVisibleCount;
    public System.Action OnClickItem;
  }
}
