// Decompiled with JetBrains decompiler
// Type: SymbolMake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class SymbolMake : GameSection
{
  private const int MAX_SHOW_COLOR_ITEM_COUNT = 10;
  private const int MAX_SHOW_SYMBOL_ITEM_COUNT = 8;
  private SymbolMake.PAGE page;
  private Vector3Interpolator cameraAnim = new Vector3Interpolator();
  private LoadObject colorListItem;
  private LoadObject symbolListItem;
  private SymbolMarkCtrl symbolMark;
  private SymbolMake.PageInfo markPage;
  private SymbolMake.PageInfo framePage;
  private SymbolMake.PageInfo patternPage;
  private SymbolMake.PageInfo selectPage;

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "SymbolTable";
    }
  }

  public override string overrideBackKeyEvent => "PAGE_PREV";

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  private IEnumerator DoInitialize()
  {
    while (MonoBehaviourSingleton<DataTableManager>.I.IsLoading())
      yield return (object) null;
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    this.colorListItem = load_queue.Load(RESOURCE_CATEGORY.UI, "CharaMakeColorListItem");
    yield return (object) load_queue.Wait();
    this.symbolListItem = load_queue.Load(RESOURCE_CATEGORY.UI, "ClanSymbolListItem");
    yield return (object) load_queue.Wait();
    LoadObject symbol_mark_item = load_queue.Load(RESOURCE_CATEGORY.UI, "ClanSymbolMark");
    yield return (object) load_queue.Wait();
    this.symbolMark = ((Component) this.CreateResources(this.GetCtrl((Enum) SymbolMake.UI.OBJ_SYMBOL), symbol_mark_item)).GetComponent<SymbolMarkCtrl>();
    this.symbolMark.Initilize();
    this.CreatePage();
    this.LoadSymbol();
    this.selectPage = (SymbolMake.PageInfo) null;
    this.MovePage(1);
    base.Initialize();
  }

  protected override void OnDestroy() => base.OnDestroy();

  private void SetSpriteColors(SymbolMake.UI ctrl, Color[] colors)
  {
    Transform ctrl1 = this.GetCtrl((Enum) ctrl);
    if (Object.op_Equality((Object) ctrl1, (Object) null))
      return;
    int index = 0;
    for (int childCount = ctrl1.childCount; index < childCount; ++index)
    {
      Color color = colors[index];
      Transform child = ctrl1.GetChild(index);
      this.SetColor(ctrl1.GetChild(index), color);
      UIButton component = this.GetComponent<UIButton>(child);
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        component.hover = color;
        component.pressed = color;
        component.disabledColor = color;
      }
    }
  }

  private ClanSymbolData createSymbolData()
  {
    return new ClanSymbolData()
    {
      m = this.markPage.symbolId,
      mo = this.markPage.colorId,
      f = this.framePage.symbolId,
      fo = this.framePage.colorId,
      p = this.patternPage.symbolId,
      po = this.patternPage.colorId
    };
  }

  private void LoadSymbol() => this.symbolMark.LoadSymbol(this.createSymbolData());

  private void OnQuery_MARK_TAB() => this.MovePage(1);

  private void OnQuery_FRAME_TAB() => this.MovePage(2);

  private void OnQuery_PATTERN_TAB() => this.MovePage(3);

  private void MovePage(int n)
  {
    if (this.page == (SymbolMake.PAGE) n)
      return;
    this.page = (SymbolMake.PAGE) n;
    if (this.selectPage != null)
      this.selectPage.UnSelect();
    string key1 = "";
    string key2 = "";
    switch (this.page)
    {
      case SymbolMake.PAGE.MARK:
        this.selectPage = this.markPage;
        key1 = "MARK_MESSAGE";
        key2 = "MARK_COLOR_MESSAGE";
        break;
      case SymbolMake.PAGE.FRAME:
        this.selectPage = this.framePage;
        key1 = "FRAME_MESSAGE";
        key2 = "FRAME_COLOR_MESSAGE";
        break;
      case SymbolMake.PAGE.PATTERN:
        this.selectPage = this.patternPage;
        key1 = "PATTERN_MESSAGE";
        key2 = "PATTERN_COLOR_MESSAGE";
        break;
    }
    this.selectPage.Select();
    this.SelectSymbolColor();
    this.SetText((Enum) SymbolMake.UI.LBL_LIST_MESSAGE, key1);
    this.SetText((Enum) SymbolMake.UI.LBL_COLOR_MESSAGE, key2);
  }

  private void onClickSymbol(int id)
  {
    this.selectPage.symbolId = id;
    this.LoadSymbol();
  }

  private void OnQuery_SYMBOL_COLOR()
  {
    this.selectPage.colorId = (int) GameSection.GetEventData();
    this.SelectSymbolColor();
    this.LoadSymbol();
  }

  protected void OnQuery_SymbolEditConfirmDialog_YES()
  {
    GameSection.StayEvent();
    MonoBehaviourSingleton<ClanMatchingManager>.I.SendSymbolEditRequest(this.createSymbolData(), (Action<bool>) (isSuccses =>
    {
      GameSection.ResumeEvent(isSuccses);
      if (!isSuccses)
        return;
      MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData.sym = this.createSymbolData();
    }));
  }

  private Transform CreateResources(Transform parent, LoadObject item)
  {
    Transform resources = ResourceUtility.Realizes((Object) (item.loadedObject as GameObject), 5);
    resources.parent = parent;
    resources.localScale = Vector3.one;
    resources.localPosition = Vector3.zero;
    return resources;
  }

  private void CreateColorList(SymbolMake.PageInfo page)
  {
    Color[] colors = Singleton<SymbolTable>.I.GetColors(page.symbolType);
    int length = colors.Length;
    for (int index = 0; index < length; ++index)
    {
      Transform resources = this.CreateResources(((Component) page.colorGrd).transform, this.colorListItem);
      ((Object) resources).name = index.ToString();
      CharaMakeColorListItem component = ((Component) resources).GetComponent<CharaMakeColorListItem>();
      component.Init(colors[index], index, page.colorScr);
      this.SetEvent(component.uiEventSender, "SYMBOL_COLOR", index);
    }
    UIUtility.SetGridItemsDraggableWidget(page.colorScr, page.colorGrd, length);
    page.colorGrd.Reposition();
    page.colorScr.ResetPosition();
    if (length > 8)
      return;
    ((Behaviour) page.colorScr).enabled = false;
  }

  private void CreateSymbolList(SymbolMake.PageInfo page)
  {
    int[] sortSymbolIds = Singleton<SymbolTable>.I.GetSortSymbolIDs(page.symbolType);
    int length = sortSymbolIds.Length;
    for (int index = 0; index < length; ++index)
    {
      Transform resources = this.CreateResources(((Component) page.symbolGrd).transform, this.symbolListItem);
      ((Object) resources).name = index.ToString();
      SymbolMakeListItem component1 = ((Component) resources).GetComponent<SymbolMakeListItem>();
      component1.Init(sortSymbolIds[index], page.symbolType);
      component1.onButton = new Action<int>(this.onClickSymbol);
      if (page.symbolType == SymbolTable.SymbolType.FRAME)
      {
        SymbolMakeListItem component2 = ((Component) this.CreateResources(resources, this.symbolListItem)).GetComponent<SymbolMakeListItem>();
        component2.Init(sortSymbolIds[index], SymbolTable.SymbolType.FRAME_OUTLINE);
        component2.SetButtonActive(false);
      }
    }
    UIUtility.SetGridItemsDraggableWidget(page.symbolScr, page.symbolGrd, length);
    page.symbolGrd.Reposition();
    page.symbolScr.ResetPosition();
    if (length > 8)
      return;
    ((Behaviour) page.symbolScr).enabled = false;
  }

  private void CreatePage()
  {
    ClanSymbolData sym = MonoBehaviourSingleton<UserInfoManager>.I.userClan.sym;
    this.markPage = new SymbolMake.PageInfo();
    this.markPage.pageTab = this.GetComponent<ClanSymbolTabController>((Enum) SymbolMake.UI.OBJ_MARK);
    this.markPage.colorScr = this.GetComponent<UIScrollView>((Enum) SymbolMake.UI.SCR_MARK_COLOR_LIST);
    this.markPage.colorGrd = this.GetComponent<UIGrid>((Enum) SymbolMake.UI.GRD_MARK_COLOR_LIST);
    this.markPage.symbolScr = this.GetComponent<UIScrollView>((Enum) SymbolMake.UI.SCR_MARK_LIST);
    this.markPage.symbolGrd = this.GetComponent<UIGrid>((Enum) SymbolMake.UI.GRD_MARK_LIST);
    this.framePage = new SymbolMake.PageInfo();
    this.framePage.pageTab = this.GetComponent<ClanSymbolTabController>((Enum) SymbolMake.UI.OBJ_FRAME);
    this.framePage.colorScr = this.GetComponent<UIScrollView>((Enum) SymbolMake.UI.SCR_FRAME_COLOR_LIST);
    this.framePage.colorGrd = this.GetComponent<UIGrid>((Enum) SymbolMake.UI.GRD_FRAME_COLOR_LIST);
    this.framePage.symbolScr = this.GetComponent<UIScrollView>((Enum) SymbolMake.UI.SCR_FRAME_LIST);
    this.framePage.symbolGrd = this.GetComponent<UIGrid>((Enum) SymbolMake.UI.GRD_FRAME_LIST);
    this.patternPage = new SymbolMake.PageInfo();
    this.patternPage.pageTab = this.GetComponent<ClanSymbolTabController>((Enum) SymbolMake.UI.OBJ_PATTERN);
    this.patternPage.colorScr = this.GetComponent<UIScrollView>((Enum) SymbolMake.UI.SCR_PATTERN_COLOR_LIST);
    this.patternPage.colorGrd = this.GetComponent<UIGrid>((Enum) SymbolMake.UI.GRD_PATTERN_COLOR_LIST);
    this.patternPage.symbolScr = this.GetComponent<UIScrollView>((Enum) SymbolMake.UI.SCR_PATTERN_LIST);
    this.patternPage.symbolGrd = this.GetComponent<UIGrid>((Enum) SymbolMake.UI.GRD_PATTERN_LIST);
    this.markPage.Initilize(sym.m, sym.mo, SymbolTable.SymbolType.MARK);
    this.framePage.Initilize(sym.f, sym.fo, SymbolTable.SymbolType.FRAME);
    this.patternPage.Initilize(sym.p, sym.po, SymbolTable.SymbolType.PATTERN);
    this.CreateColorList(this.markPage);
    this.CreateColorList(this.framePage);
    this.CreateColorList(this.patternPage);
    this.CreateSymbolList(this.markPage);
    this.CreateSymbolList(this.framePage);
    this.CreateSymbolList(this.patternPage);
  }

  private void SelectSymbolColor()
  {
    int colorId = this.selectPage.colorId;
    foreach (CharaMakeColorListItem componentsInChild in ((Component) ((Component) this.selectPage.colorGrd).transform).GetComponentsInChildren<CharaMakeColorListItem>())
    {
      if (colorId == componentsInChild.id)
        componentsInChild.On();
      else
        componentsInChild.Off();
    }
  }

  private enum UI
  {
    SCR_MARK_COLOR_LIST,
    GRD_MARK_COLOR_LIST,
    SCR_FRAME_COLOR_LIST,
    GRD_FRAME_COLOR_LIST,
    SCR_PATTERN_COLOR_LIST,
    GRD_PATTERN_COLOR_LIST,
    OBJ_SYMBOL_COLORS_ON,
    OBJ_SYMBOL_COLORS_OFF,
    SCR_MARK_LIST,
    GRD_MARK_LIST,
    SCR_FRAME_LIST,
    GRD_FRAME_LIST,
    SCR_PATTERN_LIST,
    GRD_PATTERN_LIST,
    GRD_LIST,
    OBJ_MARK,
    OBJ_FRAME,
    OBJ_PATTERN,
    LBL_LIST_MESSAGE,
    LBL_COLOR_MESSAGE,
    OBJ_SYMBOL,
  }

  private enum PAGE
  {
    NONE,
    MARK,
    FRAME,
    PATTERN,
    MAX,
  }

  private class PageInfo
  {
    public UIScrollView colorScr;
    public UIGrid colorGrd;
    public UIScrollView symbolScr;
    public UIGrid symbolGrd;
    public ClanSymbolTabController pageTab;
    public SymbolTable.SymbolType symbolType;
    public int colorId;
    public int symbolId;

    public void Initilize(int id, int color, SymbolTable.SymbolType type)
    {
      this.pageTab.Initilize();
      this.symbolId = id;
      this.colorId = color;
      this.symbolType = type;
      this.UnSelect();
    }

    public void ResetPosition()
    {
      this.colorGrd.Reposition();
      this.colorScr.contentPivot = UIWidget.Pivot.Top;
      this.colorScr.ResetPosition();
      this.symbolGrd.Reposition();
      this.symbolScr.contentPivot = UIWidget.Pivot.Top;
      this.symbolScr.ResetPosition();
    }

    public void Select()
    {
      ((Component) this.colorScr).gameObject.SetActive(true);
      ((Component) this.colorGrd).gameObject.SetActive(true);
      ((Component) this.symbolGrd).gameObject.SetActive(true);
      ((Component) this.symbolScr).gameObject.SetActive(true);
      this.pageTab.Select();
    }

    public void UnSelect()
    {
      ((Component) this.colorScr).gameObject.SetActive(false);
      ((Component) this.colorGrd).gameObject.SetActive(false);
      ((Component) this.symbolGrd).gameObject.SetActive(false);
      ((Component) this.symbolScr).gameObject.SetActive(false);
      this.pageTab.UnSelect();
    }
  }
}
