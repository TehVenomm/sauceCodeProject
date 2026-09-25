// Decompiled with JetBrains decompiler
// Type: ChatStampAll
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ChatStampAll : MonoBehaviour
{
  public UIGrid grid;
  public UIScrollView scroll;
  public UIButton closeButton;
  public UITweenCtrl tweenCtrl;
  private GameObject mChatStampPrefab;
  private List<StampTable.Data> currentUnlockStamps;
  private List<Transform> createIcons;
  private bool initailized;

  public void Open()
  {
    ((Component) this).gameObject.SetActive(true);
    this.tweenCtrl.Play();
    this.StartCoroutine(this.DoOpen());
  }

  private IEnumerator DoOpen()
  {
    if (!this.initailized)
    {
      LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
      LoadObject lo_chat_stamp_listitem = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ChatStampListItem");
      if (loadingQueue.IsLoading())
        yield return (object) loadingQueue.Wait();
      this.mChatStampPrefab = lo_chat_stamp_listitem.loadedObject as GameObject;
      this.closeButton.onClick.Add(new EventDelegate(new EventDelegate.Callback(this.OnClose)));
      this.createIcons = new List<Transform>();
      this.initailized = true;
      lo_chat_stamp_listitem = (LoadObject) null;
    }
    this.currentUnlockStamps = Singleton<StampTable>.I.GetUnlockStamps(MonoBehaviourSingleton<UserInfoManager>.I);
    this.CreateStampList();
    this.InitStampList();
    UIUtility.SetGridItemsDraggableWidget(this.scroll, this.grid, this.createIcons.Count);
  }

  public void Close()
  {
    this.tweenCtrl.Play(false, (EventDelegate.Callback) (() => ((Component) this).gameObject.SetActive(false)));
  }

  private void CreateStampList()
  {
    for (int count = this.createIcons.Count; count < MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds.Count + this.currentUnlockStamps.Count; ++count)
    {
      Transform stampItem = this.CreateStampItem(((Component) this.grid).transform);
      ((Object) stampItem).name = count.ToString();
      this.createIcons.Add(stampItem);
    }
    this.grid.Reposition();
    this.scroll.ResetPosition();
  }

  private void InitStampList()
  {
    for (int index1 = 0; index1 < MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds.Count + this.currentUnlockStamps.Count; ++index1)
    {
      Transform createIcon = this.createIcons[index1];
      if (MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds.Count > index1)
      {
        int index = index1;
        this.InitStampItem(MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds[index], createIcon, (System.Action) (() => this.OnClickIcon(MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds[index])));
      }
      else
      {
        int index = index1 - MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds.Count;
        this.InitStampItem((int) this.currentUnlockStamps[index].id, createIcon, (System.Action) (() => this.OnClickIcon((int) this.currentUnlockStamps[index].id)));
      }
    }
  }

  private Transform CreateStampItem(Transform parent)
  {
    Transform stampItem = ResourceUtility.Realizes((Object) this.mChatStampPrefab, 5);
    stampItem.parent = parent;
    stampItem.localScale = Vector3.one;
    return stampItem;
  }

  private void InitStampItem(int stampId, Transform iTransform, System.Action onClick)
  {
    ChatStampListItem component = ((Component) iTransform).GetComponent<ChatStampListItem>();
    component.Init(stampId);
    component.onButton = onClick;
  }

  private void OnClickIcon(int stampId)
  {
    MonoBehaviourSingleton<UIManager>.I.mainChat.SendStampAsMine(stampId);
    this.Close();
  }

  private void OnClose()
  {
    SoundManager.PlaySystemSE(SoundID.UISE.CANCEL);
    this.Close();
  }
}
