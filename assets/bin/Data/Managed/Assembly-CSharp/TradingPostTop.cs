using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TradingPostTop : GameSection
{
	private enum UI
	{
		BTN_STORAGE,
		BTN_SELL,
		BTN_QUATITY,
		BTN_PRICE,
		SPR_PRICE_ARROW,
		BTN_REFRESH,
		LBL_SELL_ITEM,
		LBL_QTY_NORMAL,
		LBL_QTY_SELECT,
		LBL_PRICE_NORMAL,
		LBL_PRICE_SELECT,
		LBL_REFRESH,
		TGL_TAB0,
		TGL_TAB1,
		TGL_TAB2,
		TGL_TAB3,
		TGL_TAB4,
		SCR_POST,
		GRD_POST,
		GRD_POST_SMALL,
		PNL_MATERIAL_INFO,
		LBL_NAME,
		LBL_TOTAL_NUM,
		LBL_COST,
		OBJ_ICON,
		OBJ_NOT_LICENSED,
		LBL_NOTICE,
		LBL_LOGIN,
		LBL_REQ_LOGIN,
		LBL_REQ_LICENSE,
		OBJ_TUTORIAL,
		OBJ_TUTORIAL_1,
		OBJ_TUTORIAL_2,
		OBJ_TUTORIAL_3,
		LBL_MESSAGE
	}

	private enum VIEW_TYPE
	{
		ALL,
		MATERIAL,
		ITEM,
		MAGI,
		LAPIS
	}

	private enum SORT_TYPE
	{
		NONE,
		QUANTITY,
		PRICE_UP,
		PRICE_DOWN
	}

	private const string BTN_NORMAL_SPRITE = "ItemBoxExtentBtn_half";

	private const string BTN_ACTIVE_SPRITE = "ItemBoxExtentBtn2_half";

	private VIEW_TYPE _viewType;

	private SORT_TYPE _sortType;

	private List<TradingPostInfo> allInfos = new List<TradingPostInfo>();

	private List<TradingPostInfo> currentInfos;

	private Action[] tutorialArr;

	private int currentTutorialIndex;

	private int currentPage = 1;

	private bool endPage;

	private UIScrollView scrollview;

	private bool refresh;

	private bool next;

	public override void Initialize()
	{
		if (!TradingPostManager.IsFinishTradingPostTutorial())
		{
			tutorialArr = new Action[3];
			tutorialArr[0] = tutorialStep1;
			tutorialArr[1] = tutorialStep2;
			tutorialArr[2] = tutorialStep3;
		}
		scrollview = GetComponent<UIScrollView>(UI.SCR_POST);
		UIScrollView uIScrollView = scrollview;
		uIScrollView.onReachBottom = (UIScrollView.OnDragNotification)Delegate.Combine(uIScrollView.onReachBottom, new UIScrollView.OnDragNotification(OnScrollViewReachBottom));
		StartCoroutine(DoInitialize());
	}

	protected override void OnDestroy()
	{
		if (scrollview != null)
		{
			UIScrollView uIScrollView = scrollview;
			uIScrollView.onReachBottom = (UIScrollView.OnDragNotification)Delegate.Remove(uIScrollView.onReachBottom, new UIScrollView.OnDragNotification(OnScrollViewReachBottom));
		}
		base.OnDestroy();
	}

	private void OnScrollViewReachBottom()
	{
		if (TradingPostManager.IsFulfillRequirement() && TradingPostManager.IsFinishTradingPostTutorial())
		{
			Next();
		}
	}

	private IEnumerator DoInitialize()
	{
		bool wait = true;
		MonoBehaviourSingleton<TradingPostManager>.I.SendRequestInfo(currentPage, delegate(bool b, List<TradingPostInfo> list)
		{
			wait = false;
			allInfos.AddRange(list);
		});
		while (wait)
		{
			yield return null;
		}
		base.Initialize();
		if (MonoBehaviourSingleton<TradingPostManager>.I.tradingPostSoldNum > 0)
		{
			RequestEvent("HISTORY");
		}
	}

	private void Refresh()
	{
		refresh = true;
	}

	private void Next()
	{
		next = true;
	}

	private void Update()
	{
		if ((!refresh && !next) || !MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() || MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
		{
			return;
		}
		if (refresh)
		{
			refresh = false;
			DispatchEvent("REFRESH");
		}
		else if (next)
		{
			next = false;
			if (!endPage)
			{
				DispatchEvent("NEXT");
			}
		}
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		switch (flags)
		{
		case NOTIFY_FLAG.UPDATE_TRADING_POST:
			UpdateUI();
			StartSection();
			break;
		case NOTIFY_FLAG.UPDATE_TRADING_POST_ITEM_DETAIL:
			Refresh();
			break;
		case NOTIFY_FLAG.UPDATE_TRADING_POST_SOLD:
			UpdateUI();
			break;
		}
	}

	public override void UpdateUI()
	{
		SetLabelText(UI.LBL_SELL_ITEM, base.sectionData.GetText("STR_SELL_ITEM"));
		SetLabelText(UI.LBL_QTY_NORMAL, base.sectionData.GetText("TEXT_BTN_QTY"));
		SetLabelText(UI.LBL_QTY_SELECT, base.sectionData.GetText("TEXT_BTN_QTY"));
		SetLabelText(UI.LBL_PRICE_NORMAL, base.sectionData.GetText("TEXT_BTN_PRICE"));
		SetLabelText(UI.LBL_PRICE_SELECT, base.sectionData.GetText("TEXT_BTN_PRICE"));
		SetLabelText(UI.LBL_REFRESH, base.sectionData.GetText("TEXT_BTN_REFRESH"));
		bool flag = TradingPostManager.IsPurchasedLicense() || TradingPostManager.IsLoginRequireFinish();
		SetActive(UI.OBJ_NOT_LICENSED, !flag);
		if (!flag)
		{
			SetLabelText(UI.LBL_LOGIN, string.Format(base.sectionData.GetText("STR_LOGIN"), MonoBehaviourSingleton<TradingPostManager>.I.tradingDay, MonoBehaviourSingleton<TradingPostManager>.I.tradingConditionDay));
			SetLabelText(UI.LBL_NOTICE, string.Format(base.sectionData.GetText("STR_NOTICE"), MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name));
			SetLabelText(UI.LBL_REQ_LOGIN, base.sectionData.GetText("STR_REQ_LOGIN"));
			SetLabelText(UI.LBL_REQ_LICENSE, base.sectionData.GetText("STR_REQ_LICENSE"));
		}
		List<ITEM_TYPE> itemTypes;
		switch (_viewType)
		{
		case VIEW_TYPE.MATERIAL:
			itemTypes = MonoBehaviourSingleton<GlobalSettingsManager>.I.itemMaterialType;
			break;
		case VIEW_TYPE.ITEM:
			itemTypes = MonoBehaviourSingleton<GlobalSettingsManager>.I.itemItemType;
			break;
		case VIEW_TYPE.MAGI:
			itemTypes = MonoBehaviourSingleton<GlobalSettingsManager>.I.itemMagiType;
			break;
		case VIEW_TYPE.LAPIS:
			itemTypes = MonoBehaviourSingleton<GlobalSettingsManager>.I.itemLapisType;
			break;
		default:
			itemTypes = Enum.GetValues(typeof(ITEM_TYPE)).Cast<ITEM_TYPE>().ToList();
			break;
		}
		currentInfos = allInfos.Where(delegate(TradingPostInfo info)
		{
			ITEM_TYPE type = ItemInfo.CreateItemInfo(info.itemId).GetType();
			return itemTypes.Contains(type);
		}).ToList();
		if (_sortType != 0)
		{
			if (_sortType == SORT_TYPE.QUANTITY)
			{
				currentInfos.Sort((TradingPostInfo e1, TradingPostInfo e2) => e1.totalQuantity.CompareTo(e2.totalQuantity));
			}
			else if (_sortType == SORT_TYPE.PRICE_UP)
			{
				currentInfos.Sort((TradingPostInfo e1, TradingPostInfo e2) => e1.unitPrice.CompareTo(e2.unitPrice));
			}
			else
			{
				currentInfos.Sort((TradingPostInfo e1, TradingPostInfo e2) => e2.unitPrice.CompareTo(e1.unitPrice));
			}
			SetSortButtonUIStatus();
		}
		SetGrid(UI.GRD_POST, "TradingPostListBaseItem", currentInfos.Count, reset: true, delegate(int i, Transform t, bool b)
		{
			InitItemList(currentInfos[i], t, i);
		});
		SetBadge(UI.BTN_STORAGE, MonoBehaviourSingleton<TradingPostManager>.I.tradingPostSoldNum, SpriteAlignment.TopLeft, 8, -8);
	}

	public override void StartSection()
	{
		if (TradingPostManager.IsFulfillRequirement() && !TradingPostManager.IsFinishTradingPostTutorial())
		{
			startTutorial();
		}
		else if (MonoBehaviourSingleton<TradingPostManager>.I.tradingPostFindItemId != 0)
		{
			DispatchEvent("FIND_ITEM");
		}
	}

	private void AddList(List<TradingPostInfo> list)
	{
		AddItemList(GetCtrl(UI.GRD_POST), "TradingPostListBaseItem", list.Count, null, null, delegate(int i, Transform t, bool b)
		{
			InitItemList(list[i], t, i);
		});
	}

	private void InitItemList(TradingPostInfo info, Transform t, int i)
	{
		ItemInfo item = ItemInfo.CreateItemInfo(info.itemId);
		ItemSortData itemSortData = new ItemSortData();
		itemSortData.SetItem(item);
		SetLabelText(t, UI.LBL_NAME, itemSortData.GetName());
		SetLabelText(t, UI.LBL_TOTAL_NUM, info.totalQuantity);
		SetLabelText(t, UI.LBL_COST, info.unitPrice);
		SetItemIcon(FindCtrl(t, UI.OBJ_ICON), itemSortData);
		SetEvent(t, "DETAIL", i);
	}

	private void OnQuery_TAB_0()
	{
		if (_viewType != 0)
		{
			_viewType = VIEW_TYPE.ALL;
			RefreshUI();
		}
	}

	private void ResetSort()
	{
		_sortType = SORT_TYPE.NONE;
		SetSortButtonUIStatus();
	}

	private void SetBtnActive(UI ui, bool active)
	{
		string sprite_name = active ? "ItemBoxExtentBtn2_half" : "ItemBoxExtentBtn_half";
		SetSprite(ui, sprite_name);
		SetButtonSprite(ui, sprite_name, with_press: true);
	}

	private void SetSortButtonUIStatus()
	{
		BoxCollider component = GetCtrl(UI.BTN_QUATITY).gameObject.GetComponent<BoxCollider>();
		switch (_sortType)
		{
		case SORT_TYPE.QUANTITY:
			SetActive(UI.LBL_PRICE_NORMAL, is_visible: true);
			SetActive(UI.LBL_PRICE_SELECT, is_visible: false);
			SetBtnActive(UI.BTN_QUATITY, active: true);
			SetBtnActive(UI.BTN_PRICE, active: false);
			component.enabled = false;
			SetPriceArrowStatus(isVisible: false, isUp: false);
			break;
		case SORT_TYPE.PRICE_DOWN:
			SetActive(UI.LBL_PRICE_NORMAL, is_visible: false);
			SetActive(UI.LBL_PRICE_SELECT, is_visible: true);
			SetBtnActive(UI.BTN_QUATITY, active: false);
			SetBtnActive(UI.BTN_PRICE, active: true);
			component.enabled = true;
			SetPriceArrowStatus(isVisible: true, isUp: false);
			break;
		case SORT_TYPE.PRICE_UP:
			SetActive(UI.LBL_PRICE_NORMAL, is_visible: false);
			SetActive(UI.LBL_PRICE_SELECT, is_visible: true);
			SetBtnActive(UI.BTN_QUATITY, active: false);
			SetBtnActive(UI.BTN_PRICE, active: true);
			component.enabled = true;
			SetPriceArrowStatus(isVisible: true, isUp: true);
			break;
		default:
			SetActive(UI.LBL_PRICE_NORMAL, is_visible: true);
			SetActive(UI.LBL_PRICE_SELECT, is_visible: false);
			SetBtnActive(UI.BTN_QUATITY, active: false);
			SetBtnActive(UI.BTN_PRICE, active: false);
			component.enabled = true;
			SetPriceArrowStatus(isVisible: false, isUp: false);
			break;
		}
	}

	private void SetPriceArrowStatus(bool isVisible, bool isUp)
	{
		SetActive(UI.SPR_PRICE_ARROW, isVisible);
		Vector3 one = Vector3.one;
		if (isUp)
		{
			one.y = -1f;
		}
		else
		{
			one.y = 1f;
		}
		GetCtrl(UI.SPR_PRICE_ARROW).transform.localScale = one;
	}

	private void OnQuery_TAB_1()
	{
		if (_viewType != VIEW_TYPE.MATERIAL)
		{
			_viewType = VIEW_TYPE.MATERIAL;
			RefreshUI();
		}
	}

	private void OnQuery_TAB_2()
	{
		if (_viewType != VIEW_TYPE.ITEM)
		{
			_viewType = VIEW_TYPE.ITEM;
			RefreshUI();
		}
	}

	private void OnQuery_TAB_3()
	{
		if (_viewType != VIEW_TYPE.MAGI)
		{
			_viewType = VIEW_TYPE.MAGI;
			RefreshUI();
		}
	}

	private void OnQuery_TAB_4()
	{
		if (_viewType != VIEW_TYPE.LAPIS)
		{
			_viewType = VIEW_TYPE.LAPIS;
			RefreshUI();
		}
	}

	private void OnQuery_REFRESH()
	{
		currentPage = 1;
		endPage = false;
		allInfos.Clear();
		ResetSort();
		GameSection.StayEvent();
		MonoBehaviourSingleton<TradingPostManager>.I.SendRequestInfo(currentPage, delegate(bool b, List<TradingPostInfo> list)
		{
			if (list.Count == 0)
			{
				endPage = true;
			}
			else
			{
				allInfos.AddRange(list);
			}
			RefreshUI();
			GameSection.ResumeEvent(is_resume: false);
		});
	}

	private void OnQuery_NEXT()
	{
		currentPage++;
		GameSection.StayEvent();
		MonoBehaviourSingleton<TradingPostManager>.I.SendRequestInfo(currentPage, delegate(bool b, List<TradingPostInfo> list)
		{
			if (list.Count == 0)
			{
				endPage = true;
			}
			else
			{
				allInfos.AddRange(list);
			}
			RefreshUI();
			GameSection.ResumeEvent(is_resume: false);
		});
	}

	private void OnQuery_QUATITY()
	{
		_sortType = SORT_TYPE.QUANTITY;
		RefreshUI();
	}

	private void OnQuery_PRICE()
	{
		switch (_sortType)
		{
		case SORT_TYPE.NONE:
		case SORT_TYPE.QUANTITY:
		case SORT_TYPE.PRICE_UP:
			_sortType = SORT_TYPE.PRICE_DOWN;
			break;
		case SORT_TYPE.PRICE_DOWN:
			_sortType = SORT_TYPE.PRICE_UP;
			break;
		default:
			_sortType = SORT_TYPE.PRICE_DOWN;
			break;
		}
		RefreshUI();
	}

	private void OnQuery_SELL()
	{
		RequestEvent("TO_STORAGE");
	}

	private void OnQuery_DETAIL()
	{
		int index = (int)GameSection.GetEventData();
		MonoBehaviourSingleton<TradingPostManager>.I.Viewinfo = currentInfos[index];
	}

	private void OnQuery_HELP()
	{
		GameSection.SetEventData(WebViewManager.TradingPost);
	}

	private void OnQuery_HISTORY()
	{
		GameSection.SetEventData(1);
	}

	private void OnQuery_FIND_ITEM()
	{
		if (!TradingPostManager.IsFinishTradingPostTutorial())
		{
			GameSection.StopEvent();
			return;
		}
		GameSection.StayEvent();
		int tradingPostFindItemId = MonoBehaviourSingleton<TradingPostManager>.I.tradingPostFindItemId;
		MonoBehaviourSingleton<TradingPostManager>.I.RemoveTradingPostFindData();
		MonoBehaviourSingleton<TradingPostManager>.I.SendRequestFindItem(tradingPostFindItemId, delegate(bool b, TradingPostInfo info)
		{
			if (!b || info == null)
			{
				GameSection.ChangeStayEvent("MISSING");
			}
			else
			{
				GameSection.ChangeStayEvent("DETAIL");
				MonoBehaviourSingleton<TradingPostManager>.I.Viewinfo = info;
			}
			GameSection.ResumeEvent(is_resume: true);
		});
	}

	private void OnCloseDialog_CrystalShopTradingPostLicense()
	{
		RefreshUI();
		StartSection();
	}

	private void OnCloseDialog_TradingPostActiveHistory()
	{
		Refresh();
		UpdateUI();
	}

	private void OnCloseDialog_TradingPostInventoryDialog()
	{
		if (MonoBehaviourSingleton<TradingPostManager>.I.isRefreshTradingPost)
		{
			MonoBehaviourSingleton<TradingPostManager>.I.isRefreshTradingPost = false;
			Refresh();
		}
	}

	private void startTutorial()
	{
		currentTutorialIndex = 0;
		StartCoroutine(DoTutorial());
	}

	private IEnumerator DoTutorial()
	{
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.MOMENT, is_disable: true);
		yield return new WaitForSeconds(1f);
		nextStep();
		yield return new WaitForSeconds(2f);
		while (HasStepTutorial())
		{
			if (Input.GetMouseButtonDown(0))
			{
				nextStep();
				yield return new WaitForSeconds(2f);
			}
			else
			{
				yield return null;
			}
		}
		while (!Input.GetMouseButtonDown(0))
		{
			yield return null;
		}
		endTutorial();
	}

	private bool HasStepTutorial()
	{
		if (tutorialArr != null)
		{
			return currentTutorialIndex < tutorialArr.Length;
		}
		return false;
	}

	private void nextStep()
	{
		tutorialArr[currentTutorialIndex]();
		currentTutorialIndex++;
	}

	private void tutorialStep1()
	{
		Transform ctrl = GetCtrl(UI.OBJ_TUTORIAL_1);
		SetLabelText(ctrl, UI.LBL_MESSAGE, string.Format(base.sectionData.GetText("STR_TUTORIAL_1"), MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name));
		SetActive(UI.OBJ_TUTORIAL_1, is_visible: true);
		SetActive(UI.OBJ_TUTORIAL_2, is_visible: false);
		SetActive(UI.OBJ_TUTORIAL_3, is_visible: false);
	}

	private void tutorialStep2()
	{
		Transform ctrl = GetCtrl(UI.OBJ_TUTORIAL_2);
		SetLabelText(ctrl, UI.LBL_MESSAGE, base.sectionData.GetText("STR_TUTORIAL_2"));
		SetActive(UI.OBJ_TUTORIAL_1, is_visible: false);
		SetActive(UI.OBJ_TUTORIAL_2, is_visible: true);
		SetActive(UI.OBJ_TUTORIAL_3, is_visible: false);
	}

	private void tutorialStep3()
	{
		Transform ctrl = GetCtrl(UI.OBJ_TUTORIAL_3);
		SetLabelText(ctrl, UI.LBL_MESSAGE, base.sectionData.GetText("STR_TUTORIAL_3"));
		SetActive(UI.OBJ_TUTORIAL_1, is_visible: false);
		SetActive(UI.OBJ_TUTORIAL_2, is_visible: false);
		SetActive(UI.OBJ_TUTORIAL_3, is_visible: true);
	}

	private void endTutorial()
	{
		GameSaveData.instance.isFinishTradingPostTutorial = true;
		SetActive(UI.OBJ_TUTORIAL, is_visible: false);
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.MOMENT, is_disable: false);
	}

	private void SetItemIcon(Transform holder, ItemSortData data, int event_data = 0)
	{
		ITEM_ICON_TYPE iTEM_ICON_TYPE = ITEM_ICON_TYPE.NONE;
		RARITY_TYPE? rarity = null;
		ELEMENT_TYPE element = ELEMENT_TYPE.MAX;
		EQUIPMENT_TYPE? magi_enable_icon_type = null;
		int icon_id = -1;
		int num = -1;
		if (data != null)
		{
			iTEM_ICON_TYPE = data.GetIconType();
			icon_id = data.GetIconID();
			rarity = data.GetRarity();
			element = data.GetIconElement();
			magi_enable_icon_type = data.GetIconMagiEnableType();
		}
		bool is_new = false;
		switch (iTEM_ICON_TYPE)
		{
		case ITEM_ICON_TYPE.ITEM:
		case ITEM_ICON_TYPE.QUEST_ITEM:
			if (data.GetUniqID() != 0L)
			{
				is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iTEM_ICON_TYPE, data.GetUniqID());
			}
			break;
		default:
			is_new = true;
			break;
		case ITEM_ICON_TYPE.NONE:
			break;
		}
		int enemy_icon_id = 0;
		if (iTEM_ICON_TYPE == ITEM_ICON_TYPE.ITEM)
		{
			enemy_icon_id = Singleton<ItemTable>.I.GetItemData(data.GetTableID()).enemyIconID;
		}
		ItemIcon itemIcon = null;
		itemIcon = ((data.GetIconType() != ITEM_ICON_TYPE.QUEST_ITEM) ? ItemIcon.Create(iTEM_ICON_TYPE, icon_id, rarity, holder, element, magi_enable_icon_type, num, "DROP", event_data, is_new, -1, is_select: false, null, is_equipping: false, enemy_icon_id) : ItemIcon.Create(new ItemIcon.ItemIconCreateParam
		{
			icon_type = data.GetIconType(),
			icon_id = data.GetIconID(),
			rarity = data.GetRarity(),
			parent = holder,
			element = data.GetIconElement(),
			magi_enable_equip_type = data.GetIconMagiEnableType(),
			num = data.GetNum(),
			enemy_icon_id = enemy_icon_id,
			questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST
		}));
		itemIcon.SetRewardBG(is_visible: false);
		SetMaterialInfo(itemIcon.transform, data.GetMaterialType(), data.GetTableID(), GetCtrl(UI.PNL_MATERIAL_INFO));
	}
}
