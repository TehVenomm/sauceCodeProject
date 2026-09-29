// Decompiled with JetBrains decompiler
// Type: ClanSearchList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class ClanSearchList : GameSection
{
  private ClanData[] clans;
  private ClanSearchList.SortType sortType;
  private const int SHOW_NUM = 10;
  private int nowPage = 1;
  private int pageMax = 1;

  public override void UpdateUI()
  {
    if (!ClanMatchingManager.IsValidNotEmptyList())
    {
      this.SetActive((Enum) ClanSearchList.UI.GRD_CLAN, false);
      this.SetActive((Enum) ClanSearchList.UI.STR_NON_LIST, true);
      this.SetActive((Enum) ClanSearchList.UI.OBJ_ACTIVE_ROOT, false);
      this.SetActive((Enum) ClanSearchList.UI.OBJ_INACTIVE_ROOT, true);
      this.SetLabelText((Enum) ClanSearchList.UI.LBL_MAX, "0");
      this.SetLabelText((Enum) ClanSearchList.UI.LBL_NOW, "0");
    }
    else
    {
      this.clans = MonoBehaviourSingleton<ClanMatchingManager>.I.clans.ToArray();
      this.SetActive((Enum) ClanSearchList.UI.GRD_CLAN, true);
      this.SetActive((Enum) ClanSearchList.UI.STR_NON_LIST, false);
      ClanData[] showList = this.clans;
      Array.Sort<ClanData>(showList, (Comparison<ClanData>) ((a, b) =>
      {
        switch (this.sortType)
        {
          case ClanSearchList.SortType.CreateOld:
            if (a.createdAt < b.createdAt)
              return -1;
            return a.createdAt > b.createdAt ? 1 : 0;
          case ClanSearchList.SortType.CreateNew:
            if (a.createdAt < b.createdAt)
              return 1;
            return a.createdAt > b.createdAt ? -1 : 0;
          case ClanSearchList.SortType.MemberMany:
            if (a.num < b.num)
              return 1;
            return a.num > b.num ? -1 : 0;
          case ClanSearchList.SortType.MemberFew:
            if (a.num < b.num)
              return -1;
            return a.num > b.num ? 1 : 0;
          default:
            return 0;
        }
      }));
      this.pageMax = 1 + (showList.Length - 1) / 10;
      bool is_visible = this.pageMax > 1;
      this.SetActive((Enum) ClanSearchList.UI.OBJ_ACTIVE_ROOT, is_visible);
      this.SetActive((Enum) ClanSearchList.UI.OBJ_INACTIVE_ROOT, !is_visible);
      this.SetLabelText((Enum) ClanSearchList.UI.LBL_MAX, this.pageMax.ToString());
      this.SetLabelText((Enum) ClanSearchList.UI.LBL_NOW, this.nowPage.ToString());
      int sourceIndex = 10 * (this.nowPage - 1);
      if (showList.Length <= sourceIndex)
      {
        sourceIndex = 0;
        this.nowPage = 1;
      }
      int length = this.nowPage == this.pageMax ? showList.Length - sourceIndex : 10;
      ClanData[] destinationArray = new ClanData[length];
      Array.Copy((Array) showList, sourceIndex, (Array) destinationArray, 0, length);
      showList = destinationArray;
      this.SetGrid((Enum) ClanSearchList.UI.GRD_CLAN, "ClanSearchListItem", showList.Length, true, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        this.SetEvent(t, "SELECT", i);
        this.SetListItemData(showList[i], t);
      }));
      this.SetLabelText((Enum) ClanSearchList.UI.STR_SORT, this.GetSortString(this.sortType));
      base.UpdateUI();
    }
  }

  public override void Initialize()
  {
    MonoBehaviourSingleton<ClanMatchingManager>.I.ResetSearchRequest();
    this.StartCoroutine(this.DoInitialize());
  }

  private IEnumerator DoInitialize()
  {
    yield return (object) this.StartCoroutine(this.Reload());
    base.Initialize();
  }

  private IEnumerator Reload(Action<bool> cb = null)
  {
    bool is_recv = false;
    this.SendRequest((System.Action) (() => is_recv = true), cb);
    while (!is_recv)
      yield return (object) null;
    this.nowPage = 1;
    this.SetDirty((Enum) ClanSearchList.UI.GRD_CLAN);
    this.RefreshUI();
  }

  private void SendRequest(System.Action onFinish, Action<bool> cb)
  {
    MonoBehaviourSingleton<ClanMatchingManager>.I.RequestSearch((Action<bool, Error>) ((isSuccess, error) =>
    {
      onFinish();
      if (cb == null)
        return;
      cb(isSuccess);
    }), false);
  }

  private void SetListItemData(ClanData clan, Transform t)
  {
    ServerConstDefine constDefine = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine;
    this.SetLabelText(t, (Enum) ClanSearchList.UI.LBL_CLAN_NAME, clan.name);
    this.SetLabelText(t, (Enum) ClanSearchList.UI.LBL_CLAN_LV, clan.lv.ToString());
    this.SetLabelText(t, (Enum) ClanSearchList.UI.LBL_HOST_NAME, clan.mName);
    this.SetLabelText(t, (Enum) ClanSearchList.UI.LBL_CLAN_MEMBER_NUM, clan.num.ToString());
    this.SetLabelText(t, (Enum) ClanSearchList.UI.LBL_CLAN_MAX_MEMBER_NUM, constDefine.CLAN_MAX_MEMBER_NUM.ToString());
    this.SetLabelText(t, (Enum) ClanSearchList.UI.LBL_CLAN_COMMENT, clan.cmt);
    this.SetLabelText(t, (Enum) ClanSearchList.UI.LBL_CLAN_LABEL, StringTable.Get(STRING_CATEGORY.CLAN_LABEL, (uint) clan.lbl));
    if (Object.op_Equality((Object) this.FindCtrl(t, (Enum) ClanSearchList.UI.OBJ_SYMBOL_MARK), (Object) null))
      this.StartCoroutine(this.CreateSymbolMark(clan, t));
    else
      this.GetComponent<SymbolMarkCtrl>(t, (Enum) ClanSearchList.UI.OBJ_SYMBOL_MARK).LoadSymbol(clan.sym);
  }

  private IEnumerator CreateSymbolMark(ClanData clan, Transform t)
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject symbolMarkLoadObj = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ClanSymbolMark");
    yield return (object) loadingQueue.Wait();
    Transform transform = ResourceUtility.Realizes((Object) (symbolMarkLoadObj.loadedObject as GameObject), 5);
    transform.parent = this.FindCtrl(t, (Enum) ClanSearchList.UI.OBJ_SYMBOL);
    transform.localScale = Vector3.one;
    transform.localPosition = Vector3.zero;
    ((Object) transform).name = "OBJ_SYMBOL_MARK";
    SymbolMarkCtrl component = ((Component) transform).GetComponent<SymbolMarkCtrl>();
    component.Initilize();
    component.LoadSymbol(clan.sym);
  }

  private void OnQuery_RELOAD()
  {
    GameSection.StayEvent();
    this.StartCoroutine(this.Reload((Action<bool>) (b => GameSection.ResumeEvent(b))));
  }

  private void OnQuery_SELECT()
  {
    GameSection.SetEventData((object) this.clans[10 * (this.nowPage - 1) + (int) GameSection.GetEventData()].cId);
  }

  private void OnQuery_SORT()
  {
    int num = (int) (this.sortType + 1);
    if (num >= Enum.GetNames(typeof (ClanSearchList.SortType)).Length)
      num = 0;
    this.sortType = (ClanSearchList.SortType) num;
    this.nowPage = 1;
    this.RefreshUI();
  }

  private void OnCloseDialog_ClanSearchSettings()
  {
    this.nowPage = 1;
    this.RefreshUI();
  }

  private void OnQuery_PAGE_PREV()
  {
    this.nowPage = this.nowPage > 1 ? this.nowPage - 1 : this.pageMax;
    this.RefreshUI();
  }

  private void OnQuery_PAGE_NEXT()
  {
    this.nowPage = this.nowPage < this.pageMax ? this.nowPage + 1 : 1;
    this.RefreshUI();
  }

  private string GetSortString(ClanSearchList.SortType type)
  {
    switch (type)
    {
      case ClanSearchList.SortType.CreateOld:
        return "Oldest";
      case ClanSearchList.SortType.CreateNew:
        return "Newest";
      case ClanSearchList.SortType.MemberMany:
        return "Most Members";
      case ClanSearchList.SortType.MemberFew:
        return "Fewest Members";
      default:
        return string.Empty;
    }
  }

  protected enum UI
  {
    GRD_CLAN,
    STR_NON_LIST,
    STR_SORT,
    STR_CLAN_LV,
    LBL_CLAN_LV,
    LBL_CLAN_NAME,
    LBL_HOST_NAME,
    LBL_CLAN_LABEL,
    LBL_CLAN_COMMENT,
    LBL_CLAN_MAX_MEMBER_NUM,
    LBL_CLAN_MEMBER_NUM,
    OBJ_SYMBOL,
    OBJ_SYMBOL_MARK,
    TEX_STAMP,
    LBL_NOW,
    LBL_MAX,
    OBJ_ACTIVE_ROOT,
    OBJ_INACTIVE_ROOT,
  }

  public enum SortType
  {
    CreateOld,
    CreateNew,
    MemberMany,
    MemberFew,
  }
}
