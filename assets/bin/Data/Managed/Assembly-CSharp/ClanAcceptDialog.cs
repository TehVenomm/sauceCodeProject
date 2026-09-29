// Decompiled with JetBrains decompiler
// Type: ClanAcceptDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ClanAcceptDialog : GameSection
{
  private const int ITEM_COUNT_PER_PAGE = 10;
  private List<FriendCharaInfo> originalList;
  private List<FriendCharaInfo> pageList = new List<FriendCharaInfo>();
  private int nowPage;
  private int pageNumMax = 1;

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  protected IEnumerator DoInitialize()
  {
    bool isReceived = false;
    MonoBehaviourSingleton<ClanMatchingManager>.I.SendRequestList((Action<bool, List<FriendCharaInfo>>) ((b, l) =>
    {
      this.originalList = l;
      this.nowPage = 0;
      if (this.originalList != null)
      {
        this.pageNumMax = Mathf.CeilToInt((float) this.originalList.Count / 10f);
        MonoBehaviourSingleton<UserInfoManager>.I.SetClanRequestNum(this.originalList.Count);
      }
      else
        this.pageNumMax = 1;
      isReceived = true;
    }));
    while (!isReceived)
      yield return (object) null;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetupSrollBarCollider();
    this.SetupPaging();
    int pageList = this.CreatePageList();
    this.SetActive((Enum) ClanAcceptDialog.UI.STR_NON_LIST, pageList == 0);
    this.SetDynamicList((Enum) ClanAcceptDialog.UI.GRD_LIST, "ClanAcceptListItem", pageList, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      this.SetupItem(t, i);
      this.SetActive(t, true);
    }));
  }

  private void SetupSrollBarCollider()
  {
    Transform ctrl = this.GetCtrl((Enum) ClanAcceptDialog.UI.OBJ_SCROLL_BAR);
    if (Object.op_Equality((Object) ctrl, (Object) null))
      return;
    UIWidget component1 = ((Component) ctrl).GetComponent<UIWidget>();
    if (Object.op_Equality((Object) component1, (Object) null))
      return;
    BoxCollider component2 = ((Component) ctrl).GetComponent<BoxCollider>();
    if (Object.op_Equality((Object) component2, (Object) null))
      return;
    component2.size = Vector2.op_Implicit(new Vector2(component2.size.x, component1.localSize.y));
  }

  private void SetupPaging()
  {
    this.SetPageNumText((Enum) ClanAcceptDialog.UI.LBL_NOW, this.nowPage + 1);
    this.SetPageNumText((Enum) ClanAcceptDialog.UI.LBL_MAX, this.pageNumMax);
    this.SetActive((Enum) ClanAcceptDialog.UI.OBJ_ACTIVE_ROOT, this.pageNumMax != 1);
    this.SetActive((Enum) ClanAcceptDialog.UI.OBJ_INACTIVE_ROOT, this.pageNumMax == 1);
  }

  private int CreatePageList()
  {
    this.pageList.Clear();
    if (this.originalList == null)
      return 0;
    int num = this.nowPage * 10;
    for (int index1 = 0; index1 < 10; ++index1)
    {
      int index2 = num + index1;
      if (index2 < this.originalList.Count)
        this.pageList.Add(this.originalList[index2]);
      else
        break;
    }
    return this.pageList.Count;
  }

  private void SetupItem(Transform t, int index)
  {
    ClanAcceptListItem clanAcceptListItem = ((Component) t).GetComponent<ClanAcceptListItem>();
    if (Object.op_Equality((Object) clanAcceptListItem, (Object) null))
      clanAcceptListItem = ((Component) t).gameObject.AddComponent<ClanAcceptListItem>();
    clanAcceptListItem.InitUI();
    clanAcceptListItem.Setup(t, index, this.pageList[index]);
  }

  private void OnQuery_ACCEPT()
  {
    int index = (int) GameSection.GetEventData();
    if (index >= this.pageList.Count)
      return;
    int userId = this.pageList[index].userId;
    GameSection.StayEvent();
    MonoBehaviourSingleton<ClanMatchingManager>.I.SendAcceptRequest(userId, (Action<bool>) (b =>
    {
      if (b)
        this.ExecRequestSuccess(index);
      else
        GameSection.ResumeEvent(false);
    }));
  }

  private void OnQuery_REJECT()
  {
    int index = (int) GameSection.GetEventData();
    if (index >= this.pageList.Count)
      return;
    int userId = this.pageList[index].userId;
    GameSection.StayEvent();
    MonoBehaviourSingleton<ClanMatchingManager>.I.SendRejectRequest(userId, (Action<bool>) (b =>
    {
      if (b)
        this.ExecRequestSuccess(index);
      else
        GameSection.ResumeEvent(false);
    }));
  }

  private void ExecRequestSuccess(int index)
  {
    string name = this.pageList[index].name;
    this.originalList.RemoveAt(index + this.nowPage * 10);
    int num = Mathf.CeilToInt((float) this.originalList.Count / 10f);
    if (num > 0 && num != this.pageNumMax)
    {
      this.pageNumMax = num;
      if (this.nowPage >= this.pageNumMax)
        --this.nowPage;
    }
    MonoBehaviourSingleton<UserInfoManager>.I.DecreaseClanRequestNum();
    this.RefreshUI();
    GameSection.ResumeEvent(true, (object) new string[1]
    {
      name
    });
  }

  private void OnQuery_PAGE_PREV()
  {
    this.nowPage = (this.nowPage - 1 + this.pageNumMax) % this.pageNumMax;
    this.SetDirty((Enum) ClanAcceptDialog.UI.GRD_LIST);
    this.RefreshUI();
  }

  private void OnQuery_PAGE_NEXT()
  {
    this.nowPage = (this.nowPage + 1) % this.pageNumMax;
    this.SetDirty((Enum) ClanAcceptDialog.UI.GRD_LIST);
    this.RefreshUI();
  }

  private void OnQuery_DETAIL()
  {
    int eventData = (int) GameSection.GetEventData();
    FriendCharaInfo page = this.pageList[eventData];
    MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = eventData + 4;
    GameSection.SetEventData((object) page);
  }

  protected enum UI
  {
    STR_NON_LIST,
    GRD_LIST,
    OBJ_SCROLL_BAR,
    LBL_NOW,
    LBL_MAX,
    OBJ_ACTIVE_ROOT,
    OBJ_INACTIVE_ROOT,
  }
}
