// Decompiled with JetBrains decompiler
// Type: ChatStampFavoriteEdit
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class ChatStampFavoriteEdit : MonoBehaviour
{
  public UIGrid favoriteGrid;
  public UIGrid unlockGrid;
  public UIScrollView unlockScroll;
  public UIButton yesButton;
  public UIButton noButton;
  public UITweenCtrl tweenCtrl;
  public GameObject topBlocker;
  public GameObject bottomBlocker;
  public GameObject selectedIconRoot;
  private GameObject mChatStampPrefab;
  private int selectFavoriteStampIndex;
  private int[] currentFavorite;
  private List<StampTable.Data> currentUnlockStamps;
  private List<Transform> favoriteIcons;
  private List<Transform> unlockIcons;
  private ChatStampListItem selectedIcon;
  private bool initialized;

  private IEnumerator DoOpen()
  {
    if (!this.initialized)
    {
      LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
      LoadObject lo_chat_stamp_listitem = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ChatStampListItemFavorite");
      if (loadingQueue.IsLoading())
        yield return (object) loadingQueue.Wait();
      this.mChatStampPrefab = lo_chat_stamp_listitem.loadedObject as GameObject;
      this.yesButton.onClick.Add(new EventDelegate(new EventDelegate.Callback(this.OnYes)));
      this.noButton.onClick.Add(new EventDelegate(new EventDelegate.Callback(this.OnNo)));
      this.initialized = true;
      this.favoriteIcons = new List<Transform>();
      this.unlockIcons = new List<Transform>();
      lo_chat_stamp_listitem = (LoadObject) null;
    }
    this.selectedIcon = ((Component) this.CreateStampItem(this.selectedIconRoot.transform)).GetComponent<ChatStampListItem>();
    this.selectedIcon.SetActiveComponents(false);
    this.CreateFavoriteStampList();
    this.InitFavoriteStampList();
    this.CreateUnlockStampList();
    this.InitUnlockStampList();
  }

  public void Open()
  {
    ((Component) this).gameObject.SetActive(true);
    this.tweenCtrl.Play();
    this.currentFavorite = MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds.ToArray();
    this.currentUnlockStamps = Singleton<StampTable>.I.GetUnlockStamps(MonoBehaviourSingleton<UserInfoManager>.I);
    this.SetBlocker(true);
    this.StartCoroutine(this.DoOpen());
  }

  public void Close(bool update)
  {
    this.tweenCtrl.Play(false, (EventDelegate.Callback) (() => ((Component) this).gameObject.SetActive(false)));
    if (update)
    {
      MonoBehaviourSingleton<UserInfoManager>.I.SetFavoriteStamp(((IEnumerable<int>) this.currentFavorite).ToList<int>());
      MonoBehaviourSingleton<UIManager>.I.mainChat.UpdateStampList();
    }
    else
    {
      for (int index = 0; index < this.currentFavorite.Length; ++index)
      {
        if (this.currentFavorite[index] != MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds[index])
          ((Component) this.favoriteIcons[index]).GetComponent<ChatStampListItem>().Init(MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds[index]);
      }
    }
  }

  private void CreateFavoriteStampList()
  {
    for (int count = this.favoriteIcons.Count; count < MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds.Count; ++count)
      this.favoriteIcons.Add(this.CreateStampItem(((Component) this.favoriteGrid).transform));
    this.favoriteGrid.Reposition();
  }

  private void CreateUnlockStampList()
  {
    for (int count = this.unlockIcons.Count; count < this.currentUnlockStamps.Count; ++count)
      this.unlockIcons.Add(this.CreateStampItem(((Component) this.unlockGrid).transform));
    this.unlockGrid.Reposition();
    UIUtility.SetGridItemsDraggableWidget(this.unlockScroll, this.unlockGrid, this.currentUnlockStamps.Count);
  }

  private void InitFavoriteStampList()
  {
    for (int index1 = 0; index1 < MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds.Count; ++index1)
    {
      int index = index1;
      Transform favoriteIcon = this.favoriteIcons[index1];
      this.InitStampItem(MonoBehaviourSingleton<UserInfoManager>.I.favoriteStampIds[index1], favoriteIcon, (System.Action) (() => this.OnClickFavoriteStamp(index)));
    }
  }

  private void InitUnlockStampList()
  {
    for (int index = 0; index < this.currentUnlockStamps.Count; ++index)
    {
      int j = (int) this.currentUnlockStamps[index].id;
      Transform unlockIcon = this.unlockIcons[index];
      this.InitStampItem((int) this.currentUnlockStamps[index].id, unlockIcon, (System.Action) (() => this.OnClickUnlockStamp(j)));
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

  private void OnClickFavoriteStamp(int index)
  {
    this.selectFavoriteStampIndex = index;
    this.SetBlocker(false);
    this.SetSelectedFavoriteIcon(this.favoriteIcons[index], this.currentFavorite[index]);
  }

  private void OnClickUnlockStamp(int stampId)
  {
    int index = Array.IndexOf<int>(this.currentFavorite, stampId);
    if (index >= 0)
    {
      if (index != this.selectFavoriteStampIndex)
      {
        int _stampId = this.currentFavorite[this.selectFavoriteStampIndex];
        this.currentFavorite[this.selectFavoriteStampIndex] = stampId;
        ChatStampListItem component1 = ((Component) this.favoriteIcons[this.selectFavoriteStampIndex]).GetComponent<ChatStampListItem>();
        component1.Init(stampId);
        ((Component) component1).GetComponent<UITweenCtrl>().Reset();
        ((Component) component1).GetComponent<UITweenCtrl>().Play();
        this.currentFavorite[index] = _stampId;
        ChatStampListItem component2 = ((Component) this.favoriteIcons[index]).GetComponent<ChatStampListItem>();
        component2.Init(_stampId);
        ((Component) component2).GetComponent<UITweenCtrl>().Reset();
        ((Component) component2).GetComponent<UITweenCtrl>().Play();
      }
    }
    else
    {
      this.currentFavorite[this.selectFavoriteStampIndex] = stampId;
      ChatStampListItem component = ((Component) this.favoriteIcons[this.selectFavoriteStampIndex]).GetComponent<ChatStampListItem>();
      component.Init(stampId);
      ((Component) component).GetComponent<UITweenCtrl>().Reset();
      ((Component) component).GetComponent<UITweenCtrl>().Play();
    }
    this.SetBlocker(true);
  }

  private void SetBlocker(bool isTopActive)
  {
    this.topBlocker.SetActive(!isTopActive);
    this.bottomBlocker.SetActive(isTopActive);
    if (!Object.op_Inequality((Object) this.selectedIcon, (Object) null))
      return;
    this.selectedIcon.SetActiveComponents(!isTopActive);
  }

  private void SetSelectedFavoriteIcon(Transform selectIcon, int stampId)
  {
    ((Component) this.selectedIcon).transform.localPosition = selectIcon.localPosition;
    this.selectedIcon.Init(stampId);
  }

  private void OnYes()
  {
    SoundManager.PlaySystemSE(SoundID.UISE.OK);
    this.Close(true);
  }

  private void OnNo()
  {
    SoundManager.PlaySystemSE(SoundID.UISE.CANCEL);
    this.Close(false);
  }
}
