using Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolMake : GameSection
{
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
		OBJ_SYMBOL
	}

	private enum PAGE
	{
		NONE,
		MARK,
		FRAME,
		PATTERN,
		MAX
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
			pageTab.Initilize();
			symbolId = id;
			colorId = color;
			symbolType = type;
			UnSelect();
		}

		public void ResetPosition()
		{
			colorGrd.Reposition();
			colorScr.contentPivot = UIWidget.Pivot.Top;
			colorScr.ResetPosition();
			symbolGrd.Reposition();
			symbolScr.contentPivot = UIWidget.Pivot.Top;
			symbolScr.ResetPosition();
		}

		public void Select()
		{
			colorScr.gameObject.SetActive(value: true);
			colorGrd.gameObject.SetActive(value: true);
			symbolGrd.gameObject.SetActive(value: true);
			symbolScr.gameObject.SetActive(value: true);
			pageTab.Select();
		}

		public void UnSelect()
		{
			colorScr.gameObject.SetActive(value: false);
			colorGrd.gameObject.SetActive(value: false);
			symbolGrd.gameObject.SetActive(value: false);
			symbolScr.gameObject.SetActive(value: false);
			pageTab.UnSelect();
		}
	}

	private const int MAX_SHOW_COLOR_ITEM_COUNT = 10;

	private const int MAX_SHOW_SYMBOL_ITEM_COUNT = 8;

	private PAGE page;

	private Vector3Interpolator cameraAnim = new Vector3Interpolator();

	private LoadObject colorListItem;

	private LoadObject symbolListItem;

	private SymbolMarkCtrl symbolMark;

	private PageInfo markPage;

	private PageInfo framePage;

	private PageInfo patternPage;

	private PageInfo selectPage;

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "SymbolTable";
		}
	}

	public override string overrideBackKeyEvent => "PAGE_PREV";

	public override void Initialize()
	{
		StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		while (MonoBehaviourSingleton<DataTableManager>.I.IsLoading())
		{
			yield return null;
		}
		LoadingQueue load_queue = new LoadingQueue(this);
		colorListItem = load_queue.Load(RESOURCE_CATEGORY.UI, "CharaMakeColorListItem");
		yield return load_queue.Wait();
		symbolListItem = load_queue.Load(RESOURCE_CATEGORY.UI, "ClanSymbolListItem");
		yield return load_queue.Wait();
		LoadObject symbol_mark_item = load_queue.Load(RESOURCE_CATEGORY.UI, "ClanSymbolMark");
		yield return load_queue.Wait();
		Transform transform = CreateResources(GetCtrl(UI.OBJ_SYMBOL), symbol_mark_item);
		symbolMark = transform.GetComponent<SymbolMarkCtrl>();
		symbolMark.Initilize();
		CreatePage();
		LoadSymbol();
		selectPage = null;
		MovePage(1);
		base.Initialize();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
	}

	private void SetSpriteColors(UI ctrl, Color[] colors)
	{
		Transform ctrl2 = GetCtrl(ctrl);
		if (ctrl2 == null)
		{
			return;
		}
		int i = 0;
		for (int childCount = ctrl2.childCount; i < childCount; i++)
		{
			Color color = colors[i];
			Transform child = ctrl2.GetChild(i);
			SetColor(ctrl2.GetChild(i), color);
			UIButton component = GetComponent<UIButton>(child);
			if (component != null)
			{
				component.hover = color;
				component.pressed = color;
				component.disabledColor = color;
			}
		}
	}

	private ClanSymbolData createSymbolData()
	{
		return new ClanSymbolData
		{
			m = markPage.symbolId,
			mo = markPage.colorId,
			f = framePage.symbolId,
			fo = framePage.colorId,
			p = patternPage.symbolId,
			po = patternPage.colorId
		};
	}

	private void LoadSymbol()
	{
		symbolMark.LoadSymbol(createSymbolData());
	}

	private void OnQuery_MARK_TAB()
	{
		MovePage(1);
	}

	private void OnQuery_FRAME_TAB()
	{
		MovePage(2);
	}

	private void OnQuery_PATTERN_TAB()
	{
		MovePage(3);
	}

	private void MovePage(int n)
	{
		if (page != (PAGE)n)
		{
			page = (PAGE)n;
			if (selectPage != null)
			{
				selectPage.UnSelect();
			}
			string key = "";
			string key2 = "";
			switch (page)
			{
			case PAGE.MARK:
				selectPage = markPage;
				key = "MARK_MESSAGE";
				key2 = "MARK_COLOR_MESSAGE";
				break;
			case PAGE.FRAME:
				selectPage = framePage;
				key = "FRAME_MESSAGE";
				key2 = "FRAME_COLOR_MESSAGE";
				break;
			case PAGE.PATTERN:
				selectPage = patternPage;
				key = "PATTERN_MESSAGE";
				key2 = "PATTERN_COLOR_MESSAGE";
				break;
			}
			selectPage.Select();
			SelectSymbolColor();
			SetText(UI.LBL_LIST_MESSAGE, key);
			SetText(UI.LBL_COLOR_MESSAGE, key2);
		}
	}

	private void onClickSymbol(int id)
	{
		selectPage.symbolId = id;
		LoadSymbol();
	}

	private void OnQuery_SYMBOL_COLOR()
	{
		int colorId = (int)GameSection.GetEventData();
		selectPage.colorId = colorId;
		SelectSymbolColor();
		LoadSymbol();
	}

	protected void OnQuery_SymbolEditConfirmDialog_YES()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<ClanMatchingManager>.I.SendSymbolEditRequest(createSymbolData(), delegate(bool isSuccses)
		{
			GameSection.ResumeEvent(isSuccses);
			if (isSuccses)
			{
				MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData.sym = createSymbolData();
			}
		});
	}

	private Transform CreateResources(Transform parent, LoadObject item)
	{
		Transform transform = ResourceUtility.Realizes(item.loadedObject as GameObject, 5);
		transform.parent = parent;
		transform.localScale = Vector3.one;
		transform.localPosition = Vector3.zero;
		return transform;
	}

	private void CreateColorList(PageInfo page)
	{
		Color[] colors = Singleton<SymbolTable>.I.GetColors(page.symbolType);
		int num = colors.Length;
		for (int i = 0; i < num; i++)
		{
			Transform transform = CreateResources(page.colorGrd.transform, colorListItem);
			transform.name = i.ToString();
			CharaMakeColorListItem component = transform.GetComponent<CharaMakeColorListItem>();
			component.Init(colors[i], i, page.colorScr);
			SetEvent(component.uiEventSender, "SYMBOL_COLOR", i);
		}
		UIUtility.SetGridItemsDraggableWidget(page.colorScr, page.colorGrd, num);
		page.colorGrd.Reposition();
		page.colorScr.ResetPosition();
		if (num <= 8)
		{
			page.colorScr.enabled = false;
		}
	}

	private void CreateSymbolList(PageInfo page)
	{
		int[] sortSymbolIDs = Singleton<SymbolTable>.I.GetSortSymbolIDs(page.symbolType);
		int num = sortSymbolIDs.Length;
		for (int i = 0; i < num; i++)
		{
			Transform transform = CreateResources(page.symbolGrd.transform, symbolListItem);
			transform.name = i.ToString();
			SymbolMakeListItem component = transform.GetComponent<SymbolMakeListItem>();
			component.Init(sortSymbolIDs[i], page.symbolType);
			component.onButton = onClickSymbol;
			if (page.symbolType == SymbolTable.SymbolType.FRAME)
			{
				SymbolMakeListItem component2 = CreateResources(transform, symbolListItem).GetComponent<SymbolMakeListItem>();
				component2.Init(sortSymbolIDs[i], SymbolTable.SymbolType.FRAME_OUTLINE);
				component2.SetButtonActive(isActive: false);
			}
		}
		UIUtility.SetGridItemsDraggableWidget(page.symbolScr, page.symbolGrd, num);
		page.symbolGrd.Reposition();
		page.symbolScr.ResetPosition();
		if (num <= 8)
		{
			page.symbolScr.enabled = false;
		}
	}

	private void CreatePage()
	{
		ClanSymbolData sym = MonoBehaviourSingleton<UserInfoManager>.I.userClan.sym;
		markPage = new PageInfo();
		markPage.pageTab = GetComponent<ClanSymbolTabController>(UI.OBJ_MARK);
		markPage.colorScr = GetComponent<UIScrollView>(UI.SCR_MARK_COLOR_LIST);
		markPage.colorGrd = GetComponent<UIGrid>(UI.GRD_MARK_COLOR_LIST);
		markPage.symbolScr = GetComponent<UIScrollView>(UI.SCR_MARK_LIST);
		markPage.symbolGrd = GetComponent<UIGrid>(UI.GRD_MARK_LIST);
		framePage = new PageInfo();
		framePage.pageTab = GetComponent<ClanSymbolTabController>(UI.OBJ_FRAME);
		framePage.colorScr = GetComponent<UIScrollView>(UI.SCR_FRAME_COLOR_LIST);
		framePage.colorGrd = GetComponent<UIGrid>(UI.GRD_FRAME_COLOR_LIST);
		framePage.symbolScr = GetComponent<UIScrollView>(UI.SCR_FRAME_LIST);
		framePage.symbolGrd = GetComponent<UIGrid>(UI.GRD_FRAME_LIST);
		patternPage = new PageInfo();
		patternPage.pageTab = GetComponent<ClanSymbolTabController>(UI.OBJ_PATTERN);
		patternPage.colorScr = GetComponent<UIScrollView>(UI.SCR_PATTERN_COLOR_LIST);
		patternPage.colorGrd = GetComponent<UIGrid>(UI.GRD_PATTERN_COLOR_LIST);
		patternPage.symbolScr = GetComponent<UIScrollView>(UI.SCR_PATTERN_LIST);
		patternPage.symbolGrd = GetComponent<UIGrid>(UI.GRD_PATTERN_LIST);
		markPage.Initilize(sym.m, sym.mo, SymbolTable.SymbolType.MARK);
		framePage.Initilize(sym.f, sym.fo, SymbolTable.SymbolType.FRAME);
		patternPage.Initilize(sym.p, sym.po, SymbolTable.SymbolType.PATTERN);
		CreateColorList(markPage);
		CreateColorList(framePage);
		CreateColorList(patternPage);
		CreateSymbolList(markPage);
		CreateSymbolList(framePage);
		CreateSymbolList(patternPage);
	}

	private void SelectSymbolColor()
	{
		int colorId = selectPage.colorId;
		CharaMakeColorListItem[] componentsInChildren = selectPage.colorGrd.transform.GetComponentsInChildren<CharaMakeColorListItem>();
		foreach (CharaMakeColorListItem charaMakeColorListItem in componentsInChildren)
		{
			if (colorId == charaMakeColorListItem.id)
			{
				charaMakeColorListItem.On();
			}
			else
			{
				charaMakeColorListItem.Off();
			}
		}
	}
}
