using Network;
using System;
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
			colorScr.get_gameObject().SetActive(true);
			colorGrd.get_gameObject().SetActive(true);
			symbolGrd.get_gameObject().SetActive(true);
			symbolScr.get_gameObject().SetActive(true);
			pageTab.Select();
		}

		public void UnSelect()
		{
			colorScr.get_gameObject().SetActive(false);
			colorGrd.get_gameObject().SetActive(false);
			symbolGrd.get_gameObject().SetActive(false);
			symbolScr.get_gameObject().SetActive(false);
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
		this.StartCoroutine(DoInitialize());
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
		Transform symbolItem = CreateResources(GetCtrl(UI.OBJ_SYMBOL), symbol_mark_item);
		symbolMark = symbolItem.GetComponent<SymbolMarkCtrl>();
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
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		Transform ctrl2 = GetCtrl(ctrl);
		if (ctrl2 == null)
		{
			return;
		}
		int i = 0;
		for (int childCount = ctrl2.get_childCount(); i < childCount; i++)
		{
			Color val = colors[i];
			Transform child = ctrl2.GetChild(i);
			SetColor(ctrl2.GetChild(i), val);
			UIButton component = base.GetComponent<UIButton>(child);
			if (component != null)
			{
				component.hover = val;
				component.pressed = val;
				component.disabledColor = val;
			}
		}
	}

	private ClanSymbolData createSymbolData()
	{
		ClanSymbolData clanSymbolData = new ClanSymbolData();
		clanSymbolData.m = markPage.symbolId;
		clanSymbolData.mo = markPage.colorId;
		clanSymbolData.f = framePage.symbolId;
		clanSymbolData.fo = framePage.colorId;
		clanSymbolData.p = patternPage.symbolId;
		clanSymbolData.po = patternPage.colorId;
		return clanSymbolData;
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
			string key = string.Empty;
			string key2 = string.Empty;
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
			SetText((Enum)UI.LBL_LIST_MESSAGE, key);
			SetText((Enum)UI.LBL_COLOR_MESSAGE, key2);
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
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		GameObject obj = item.loadedObject as GameObject;
		Transform val = ResourceUtility.Realizes(obj, 5);
		val.set_parent(parent);
		val.set_localScale(Vector3.get_one());
		val.set_localPosition(Vector3.get_zero());
		return val;
	}

	private void CreateColorList(PageInfo page)
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		Color[] colors = Singleton<SymbolTable>.I.GetColors(page.symbolType);
		int num = colors.Length;
		for (int i = 0; i < num; i++)
		{
			Transform val = CreateResources(page.colorGrd.get_transform(), colorListItem);
			val.set_name(i.ToString());
			CharaMakeColorListItem component = val.GetComponent<CharaMakeColorListItem>();
			component.Init(colors[i], i, page.colorScr);
			SetEvent(component.uiEventSender, "SYMBOL_COLOR", i);
		}
		UIUtility.SetGridItemsDraggableWidget(page.colorScr, page.colorGrd, num);
		page.colorGrd.Reposition();
		page.colorScr.ResetPosition();
		if (num <= 8)
		{
			page.colorScr.set_enabled(false);
		}
	}

	private void CreateSymbolList(PageInfo page)
	{
		int[] sortSymbolIDs = Singleton<SymbolTable>.I.GetSortSymbolIDs(page.symbolType);
		int num = sortSymbolIDs.Length;
		for (int i = 0; i < num; i++)
		{
			Transform val = CreateResources(page.symbolGrd.get_transform(), symbolListItem);
			val.set_name(i.ToString());
			SymbolMakeListItem component = val.GetComponent<SymbolMakeListItem>();
			component.Init(sortSymbolIDs[i], page.symbolType);
			component.onButton = onClickSymbol;
			if (page.symbolType == SymbolTable.SymbolType.FRAME)
			{
				Transform val2 = CreateResources(val, symbolListItem);
				SymbolMakeListItem component2 = val2.GetComponent<SymbolMakeListItem>();
				component2.Init(sortSymbolIDs[i], SymbolTable.SymbolType.FRAME_OUTLINE);
				component2.SetButtonActive(isActive: false);
			}
		}
		UIUtility.SetGridItemsDraggableWidget(page.symbolScr, page.symbolGrd, num);
		page.symbolGrd.Reposition();
		page.symbolScr.ResetPosition();
		if (num <= 8)
		{
			page.symbolScr.set_enabled(false);
		}
	}

	private void CreatePage()
	{
		ClanSymbolData sym = MonoBehaviourSingleton<UserInfoManager>.I.userClan.sym;
		markPage = new PageInfo();
		markPage.pageTab = base.GetComponent<ClanSymbolTabController>((Enum)UI.OBJ_MARK);
		markPage.colorScr = base.GetComponent<UIScrollView>((Enum)UI.SCR_MARK_COLOR_LIST);
		markPage.colorGrd = base.GetComponent<UIGrid>((Enum)UI.GRD_MARK_COLOR_LIST);
		markPage.symbolScr = base.GetComponent<UIScrollView>((Enum)UI.SCR_MARK_LIST);
		markPage.symbolGrd = base.GetComponent<UIGrid>((Enum)UI.GRD_MARK_LIST);
		framePage = new PageInfo();
		framePage.pageTab = base.GetComponent<ClanSymbolTabController>((Enum)UI.OBJ_FRAME);
		framePage.colorScr = base.GetComponent<UIScrollView>((Enum)UI.SCR_FRAME_COLOR_LIST);
		framePage.colorGrd = base.GetComponent<UIGrid>((Enum)UI.GRD_FRAME_COLOR_LIST);
		framePage.symbolScr = base.GetComponent<UIScrollView>((Enum)UI.SCR_FRAME_LIST);
		framePage.symbolGrd = base.GetComponent<UIGrid>((Enum)UI.GRD_FRAME_LIST);
		patternPage = new PageInfo();
		patternPage.pageTab = base.GetComponent<ClanSymbolTabController>((Enum)UI.OBJ_PATTERN);
		patternPage.colorScr = base.GetComponent<UIScrollView>((Enum)UI.SCR_PATTERN_COLOR_LIST);
		patternPage.colorGrd = base.GetComponent<UIGrid>((Enum)UI.GRD_PATTERN_COLOR_LIST);
		patternPage.symbolScr = base.GetComponent<UIScrollView>((Enum)UI.SCR_PATTERN_LIST);
		patternPage.symbolGrd = base.GetComponent<UIGrid>((Enum)UI.GRD_PATTERN_LIST);
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
		Transform transform = selectPage.colorGrd.get_transform();
		CharaMakeColorListItem[] componentsInChildren = transform.GetComponentsInChildren<CharaMakeColorListItem>();
		CharaMakeColorListItem[] array = componentsInChildren;
		foreach (CharaMakeColorListItem charaMakeColorListItem in array)
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
