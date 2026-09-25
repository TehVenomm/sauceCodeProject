// Decompiled with JetBrains decompiler
// Type: ClanScoutList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class ClanScoutList : GameSection
{
  private int scoutRejectClanId;
  private int nowPage = 1;
  private int pageMax = 1;
  private const int SHOW_NUM = 10;
  private ClanData[] clans;

  public override void UpdateUI()
  {
    if (!ClanMatchingManager.IsScoutValidNotEmptyList())
    {
      this.SetActive((Enum) ClanScoutList.UI.GRD_CLAN, false);
      this.SetActive((Enum) ClanScoutList.UI.STR_NON_LIST, true);
      this.SetActive((Enum) ClanScoutList.UI.OBJ_ACTIVE_ROOT, false);
      this.SetActive((Enum) ClanScoutList.UI.OBJ_INACTIVE_ROOT, true);
      this.SetLabelText((Enum) ClanScoutList.UI.LBL_MAX, "0");
      this.SetLabelText((Enum) ClanScoutList.UI.LBL_NOW, "0");
    }
    else
    {
      this.clans = MonoBehaviourSingleton<ClanMatchingManager>.I.scoutClans.ToArray();
      Array.Sort<ClanData>(this.clans, (Comparison<ClanData>) ((a, b) =>
      {
        if (a.expiredAt > b.expiredAt)
          return -1;
        return a.expiredAt < b.expiredAt ? 1 : 0;
      }));
      this.SetActive((Enum) ClanScoutList.UI.GRD_CLAN, true);
      this.SetActive((Enum) ClanScoutList.UI.STR_NON_LIST, false);
      this.pageMax = 1 + (this.clans.Length - 1) / 10;
      bool is_visible = this.pageMax > 1;
      this.SetActive((Enum) ClanScoutList.UI.OBJ_ACTIVE_ROOT, is_visible);
      this.SetActive((Enum) ClanScoutList.UI.OBJ_INACTIVE_ROOT, !is_visible);
      this.SetLabelText((Enum) ClanScoutList.UI.LBL_MAX, this.pageMax.ToString());
      this.SetLabelText((Enum) ClanScoutList.UI.LBL_NOW, this.nowPage.ToString());
      int sourceIndex = 10 * (this.nowPage - 1);
      if (this.clans.Length <= sourceIndex)
      {
        sourceIndex = 0;
        this.nowPage = 1;
      }
      int length = this.nowPage == this.pageMax ? this.clans.Length - sourceIndex : 10;
      ClanData[] showList = new ClanData[length];
      Array.Copy((Array) this.clans, sourceIndex, (Array) showList, 0, length);
      this.SetGrid((Enum) ClanScoutList.UI.GRD_CLAN, "ClanScoutListItem", showList.Length, true, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
      {
        this.SetEvent(t, "SELECT_SCOUT", i);
        this.SetListItemData(showList[i], t);
        this.SetEvent(this.FindCtrl(t, (Enum) ClanScoutList.UI.BTN_DELETE), "DELETE_SCOUT", i);
      }));
      base.UpdateUI();
    }
  }

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

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
    this.SetDirty((Enum) ClanScoutList.UI.GRD_CLAN);
    this.RefreshUI();
  }

  private void SendRequest(System.Action onFinish, Action<bool> cb)
  {
    MonoBehaviourSingleton<ClanMatchingManager>.I.SendClanInviteList((Action<bool>) (isSuccess =>
    {
      onFinish();
      if (cb == null)
        return;
      cb(isSuccess);
    }));
  }

  private void SendInviteReject(System.Action cb)
  {
    MonoBehaviourSingleton<ClanMatchingManager>.I.SendClanRejectInvite(this.scoutRejectClanId, (Action<bool>) (isSuccess =>
    {
      if (isSuccess)
        GameSection.ChangeStayEvent("COMPLETE");
      if (cb == null)
        return;
      cb();
    }));
  }

  private void SetListItemData(ClanData clan, Transform t)
  {
    ServerConstDefine constDefine = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine;
    this.SetLabelText(t, (Enum) ClanScoutList.UI.LBL_CLAN_NAME, clan.name);
    this.SetLabelText(t, (Enum) ClanScoutList.UI.LBL_CLAN_LV, clan.lv.ToString());
    this.SetLabelText(t, (Enum) ClanScoutList.UI.LBL_HOST_NAME, clan.mName);
    this.SetLabelText(t, (Enum) ClanScoutList.UI.LBL_CLAN_COMMENT, clan.cmt);
    this.SetLabelText(t, (Enum) ClanScoutList.UI.LBL_CLAN_LABEL, StringTable.Get(STRING_CATEGORY.CLAN_LABEL, (uint) clan.lbl));
    this.SetLabelText(t, (Enum) ClanScoutList.UI.LBL_CLAN_MEMBER_NUM, clan.num.ToString());
    this.SetLabelText(t, (Enum) ClanScoutList.UI.LBL_CLAN_MAX_MEMBER_NUM, constDefine.CLAN_MAX_MEMBER_NUM.ToString());
    this.SetLabelText(t, (Enum) ClanScoutList.UI.LBL_LIMIT_TIME, MonoBehaviourSingleton<ClanMatchingManager>.I.ConvertDateIntToString("", clan.expiredAt));
    bool is_visible = GameSaveData.instance.isNewClanScout(clan.cId, clan.expiredAt);
    this.SetActive(t, (Enum) ClanScoutList.UI.OBJ_NEW, is_visible);
    if (Object.op_Equality((Object) this.FindCtrl(t, (Enum) ClanScoutList.UI.OBJ_SYMBOL_MARK), (Object) null))
      this.StartCoroutine(this.CreateSymbolMark(clan, t));
    else
      this.GetComponent<SymbolMarkCtrl>(t, (Enum) ClanScoutList.UI.OBJ_SYMBOL_MARK).LoadSymbol(clan.sym);
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

  private void OnQuery_RELOAD()
  {
    GameSection.StayEvent();
    this.StartCoroutine(this.Reload((Action<bool>) (b => GameSection.ResumeEvent(b))));
  }

  private void OnQuery_SELECT_SCOUT()
  {
    GameSection.SetEventData((object) this.clans[10 * (this.nowPage - 1) + (int) GameSection.GetEventData()].cId);
  }

  private void OnQuery_DELETE_SCOUT()
  {
    int index = 10 * (this.nowPage - 1) + (int) GameSection.GetEventData();
    this.scoutRejectClanId = int.Parse(this.clans[index].cId);
    GameSection.SetEventData((object) new string[1]
    {
      this.clans[index].name
    });
  }

  private void OnQuery_ClanScoutDeleteDialog_YES()
  {
    GameSection.StayEvent();
    this.SendInviteReject((System.Action) (() => this.StartCoroutine(this.Reload((Action<bool>) (b => GameSection.ResumeEvent(b))))));
  }

  private void OnCloseDialog_ClanSearchSettings() => this.RefreshUI();

  private IEnumerator CreateSymbolMark(ClanData clan, Transform t)
  {
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject symbolMarkLoadObj = loadingQueue.Load(RESOURCE_CATEGORY.UI, "ClanSymbolMark");
    yield return (object) loadingQueue.Wait();
    Transform transform = ResourceUtility.Realizes((Object) (symbolMarkLoadObj.loadedObject as GameObject), 5);
    transform.parent = this.FindCtrl(t, (Enum) ClanScoutList.UI.OBJ_SYMBOL);
    transform.localScale = Vector3.one;
    transform.localPosition = Vector3.zero;
    ((Object) transform).name = "OBJ_SYMBOL_MARK";
    SymbolMarkCtrl component = ((Component) transform).GetComponent<SymbolMarkCtrl>();
    component.Initilize();
    component.LoadSymbol(clan.sym);
    component.SetSize(90);
  }

  protected enum UI
  {
    GRD_CLAN,
    STR_NON_LIST,
    STR_SORT,
    LBL_CLAN_LV,
    LBL_CLAN_NAME,
    LBL_HOST_NAME,
    LBL_CLAN_LABEL,
    LBL_CLAN_COMMENT,
    LBL_CLAN_MAX_MEMBER_NUM,
    LBL_CLAN_MEMBER_NUM,
    LBL_LIMIT_TIME,
    BTN_DELETE,
    OBJ_SYMBOL,
    TEX_STAMP,
    LBL_NOW,
    LBL_MAX,
    OBJ_ACTIVE_ROOT,
    OBJ_INACTIVE_ROOT,
    OBJ_NEW,
    OBJ_SYMBOL_MARK,
  }
}
