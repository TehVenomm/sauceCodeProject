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

	public unsafe override void Initialize()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		if (!TradingPostManager.IsFinishTradingPostTutorial())
		{
			tutorialArr = (Action[])new Action[3];
			tutorialArr[0] = new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			tutorialArr[1] = new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			tutorialArr[2] = new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		}
		scrollview = base.GetComponent<UIScrollView>((Enum)UI.SCR_POST);
		UIScrollView uIScrollView = scrollview;
		uIScrollView.onReachBottom = (UIScrollView.OnDragNotification)Delegate.Combine(uIScrollView.onReachBottom, new UIScrollView.OnDragNotification(OnScrollViewReachBottom));
		this.StartCoroutine(DoInitialize());
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

	private unsafe IEnumerator DoInitialize()
	{
		bool wait = true;
		MonoBehaviourSingleton<TradingPostManager>.I.SendRequestInfo(currentPage, new Action<bool, List<TradingPostInfo>>((object)/*Error near IL_0038: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		while (wait)
		{
			yield return (object)null;
		}
		base.Initialize();
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
		if ((refresh || next) && MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() && !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
		{
			if (refresh)
			{
				refresh = false;
				DispatchEvent("REFRESH", null);
			}
			else if (next)
			{
				next = false;
				if (!endPage)
				{
					DispatchEvent("NEXT", null);
				}
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
		}
	}

	public unsafe override void UpdateUI()
	{
		SetLabelText((Enum)UI.LBL_SELL_ITEM, base.sectionData.GetText("STR_SELL_ITEM"));
		SetLabelText((Enum)UI.LBL_QTY_NORMAL, base.sectionData.GetText("TEXT_BTN_QTY"));
		SetLabelText((Enum)UI.LBL_QTY_SELECT, base.sectionData.GetText("TEXT_BTN_QTY"));
		SetLabelText((Enum)UI.LBL_PRICE_NORMAL, base.sectionData.GetText("TEXT_BTN_PRICE"));
		SetLabelText((Enum)UI.LBL_PRICE_SELECT, base.sectionData.GetText("TEXT_BTN_PRICE"));
		SetLabelText((Enum)UI.LBL_REFRESH, base.sectionData.GetText("TEXT_BTN_REFRESH"));
		bool flag = TradingPostManager.IsPurchasedLicense() || TradingPostManager.IsLoginRequireFinish();
		SetActive((Enum)UI.OBJ_NOT_LICENSED, !flag);
		if (!flag)
		{
			SetLabelText((Enum)UI.LBL_LOGIN, string.Format(base.sectionData.GetText("STR_LOGIN"), MonoBehaviourSingleton<TradingPostManager>.I.tradingDay, MonoBehaviourSingleton<TradingPostManager>.I.tradingConditionDay));
			SetLabelText((Enum)UI.LBL_NOTICE, string.Format(base.sectionData.GetText("STR_NOTICE"), MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name));
			SetLabelText((Enum)UI.LBL_REQ_LOGIN, base.sectionData.GetText("STR_REQ_LOGIN"));
			SetLabelText((Enum)UI.LBL_REQ_LICENSE, base.sectionData.GetText("STR_REQ_LICENSE"));
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
		_003CUpdateUI_003Ec__AnonStorey4AD _003CUpdateUI_003Ec__AnonStorey4AD;
		currentInfos = allInfos.Where(new Func<TradingPostInfo, bool>((object)_003CUpdateUI_003Ec__AnonStorey4AD, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)).ToList();
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
		SetGrid(UI.GRD_POST, "TradingPostListBaseItem", currentInfos.Count, false, new Action<int, Transform, bool>((object)_003CUpdateUI_003Ec__AnonStorey4AD, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	public override void StartSection()
	{
		if (TradingPostManager.IsFulfillRequirement() && !TradingPostManager.IsFinishTradingPostTutorial())
		{
			startTutorial();
		}
		else if (MonoBehaviourSingleton<TradingPostManager>.I.tradingPostFindItemId != 0)
		{
			DispatchEvent("FIND_ITEM", null);
		}
	}

	private unsafe void AddList(List<TradingPostInfo> list)
	{
		_003CAddList_003Ec__AnonStorey4AE _003CAddList_003Ec__AnonStorey4AE;
		AddItemList(GetCtrl(UI.GRD_POST), "TradingPostListBaseItem", list.Count, null, null, new Action<int, Transform, bool>((object)_003CAddList_003Ec__AnonStorey4AE, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private void InitItemList(TradingPostInfo info, Transform t, int i)
	{
		ItemInfo item = ItemInfo.CreateItemInfo(info.itemId);
		ItemSortData itemSortData = new ItemSortData();
		itemSortData.SetItem(item);
		SetLabelText(t, UI.LBL_NAME, itemSortData.GetName());
		SetLabelText(t, UI.LBL_TOTAL_NUM, info.totalQuantity);
		SetLabelText(t, UI.LBL_COST, info.unitPrice);
		SetItemIcon(FindCtrl(t, UI.OBJ_ICON), itemSortData, 0);
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
		string sprite_name = (!active) ? "ItemBoxExtentBtn_half" : "ItemBoxExtentBtn2_half";
		SetSprite((Enum)ui, sprite_name);
		SetButtonSprite((Enum)ui, sprite_name, true);
	}

	private void SetSortButtonUIStatus()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		BoxCollider component = GetCtrl(UI.BTN_QUATITY).get_gameObject().GetComponent<BoxCollider>();
		switch (_sortType)
		{
		case SORT_TYPE.QUANTITY:
			SetActive((Enum)UI.LBL_PRICE_NORMAL, true);
			SetActive((Enum)UI.LBL_PRICE_SELECT, false);
			SetBtnActive(UI.BTN_QUATITY, true);
			SetBtnActive(UI.BTN_PRICE, false);
			component.set_enabled(false);
			SetPriceArrowStatus(false, false);
			break;
		case SORT_TYPE.PRICE_DOWN:
			SetActive((Enum)UI.LBL_PRICE_NORMAL, false);
			SetActive((Enum)UI.LBL_PRICE_SELECT, true);
			SetBtnActive(UI.BTN_QUATITY, false);
			SetBtnActive(UI.BTN_PRICE, true);
			component.set_enabled(true);
			SetPriceArrowStatus(true, false);
			break;
		case SORT_TYPE.PRICE_UP:
			SetActive((Enum)UI.LBL_PRICE_NORMAL, false);
			SetActive((Enum)UI.LBL_PRICE_SELECT, true);
			SetBtnActive(UI.BTN_QUATITY, false);
			SetBtnActive(UI.BTN_PRICE, true);
			component.set_enabled(true);
			SetPriceArrowStatus(true, true);
			break;
		default:
			SetActive((Enum)UI.LBL_PRICE_NORMAL, true);
			SetActive((Enum)UI.LBL_PRICE_SELECT, false);
			SetBtnActive(UI.BTN_QUATITY, false);
			SetBtnActive(UI.BTN_PRICE, false);
			component.set_enabled(true);
			SetPriceArrowStatus(false, false);
			break;
		}
	}

	private void SetPriceArrowStatus(bool isVisible, bool isUp)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		SetActive((Enum)UI.SPR_PRICE_ARROW, isVisible);
		Vector3 one = Vector3.get_one();
		if (isUp)
		{
			one.y = -1f;
		}
		else
		{
			one.y = 1f;
		}
		GetCtrl(UI.SPR_PRICE_ARROW).get_transform().set_localScale(one);
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

	private unsafe void OnQuery_REFRESH()
	{
		currentPage = 1;
		endPage = false;
		allInfos.Clear();
		ResetSort();
		GameSection.StayEvent();
		MonoBehaviourSingleton<TradingPostManager>.I.SendRequestInfo(currentPage, new Action<bool, List<TradingPostInfo>>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private unsafe void OnQuery_NEXT()
	{
		currentPage++;
		GameSection.StayEvent();
		MonoBehaviourSingleton<TradingPostManager>.I.SendRequestInfo(currentPage, new Action<bool, List<TradingPostInfo>>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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
		RequestEvent("TO_STORAGE", null);
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

	private unsafe void OnQuery_FIND_ITEM()
	{
		if (!TradingPostManager.IsFinishTradingPostTutorial())
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.StayEvent();
			int tradingPostFindItemId = MonoBehaviourSingleton<TradingPostManager>.I.tradingPostFindItemId;
			MonoBehaviourSingleton<TradingPostManager>.I.RemoveTradingPostFindData();
			TradingPostManager i = MonoBehaviourSingleton<TradingPostManager>.I;
			int itemId = tradingPostFindItemId;
			if (_003C_003Ef__am_0024cacheE == null)
			{
				_003C_003Ef__am_0024cacheE = new Action<bool, TradingPostInfo>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			i.SendRequestFindItem(itemId, _003C_003Ef__am_0024cacheE);
		}
	}

	private void OnCloseDialog_CrystalShopTradingPostLicense()
	{
		RefreshUI();
		StartSection();
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
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		currentTutorialIndex = 0;
		this.StartCoroutine(DoTutorial());
	}

	private IEnumerator DoTutorial()
	{
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.MOMENT, true);
		yield return (object)new WaitForSeconds(1f);
		nextStep();
		yield return (object)new WaitForSeconds(2f);
		while (HasStepTutorial())
		{
			if (Input.GetMouseButtonDown(0))
			{
				nextStep();
				yield return (object)new WaitForSeconds(2f);
			}
			else
			{
				yield return (object)null;
			}
		}
		while (!Input.GetMouseButtonDown(0))
		{
			yield return (object)null;
		}
		endTutorial();
	}

	private bool HasStepTutorial()
	{
		return tutorialArr != null && currentTutorialIndex < tutorialArr.Length;
	}

	private void nextStep()
	{
		tutorialArr[currentTutorialIndex].Invoke();
		currentTutorialIndex++;
	}

	private void tutorialStep1()
	{
		Transform ctrl = GetCtrl(UI.OBJ_TUTORIAL_1);
		SetLabelText(ctrl, UI.LBL_MESSAGE, string.Format(base.sectionData.GetText("STR_TUTORIAL_1"), MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name));
		SetActive((Enum)UI.OBJ_TUTORIAL_1, true);
		SetActive((Enum)UI.OBJ_TUTORIAL_2, false);
		SetActive((Enum)UI.OBJ_TUTORIAL_3, false);
	}

	private void tutorialStep2()
	{
		Transform ctrl = GetCtrl(UI.OBJ_TUTORIAL_2);
		SetLabelText(ctrl, UI.LBL_MESSAGE, base.sectionData.GetText("STR_TUTORIAL_2"));
		SetActive((Enum)UI.OBJ_TUTORIAL_1, false);
		SetActive((Enum)UI.OBJ_TUTORIAL_2, true);
		SetActive((Enum)UI.OBJ_TUTORIAL_3, false);
	}

	private void tutorialStep3()
	{
		Transform ctrl = GetCtrl(UI.OBJ_TUTORIAL_3);
		SetLabelText(ctrl, UI.LBL_MESSAGE, base.sectionData.GetText("STR_TUTORIAL_3"));
		SetActive((Enum)UI.OBJ_TUTORIAL_1, false);
		SetActive((Enum)UI.OBJ_TUTORIAL_2, false);
		SetActive((Enum)UI.OBJ_TUTORIAL_3, true);
	}

	private void endTutorial()
	{
		GameSaveData.instance.isFinishTradingPostTutorial = true;
		SetActive((Enum)UI.OBJ_TUTORIAL, false);
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.MOMENT, false);
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
		{
			ulong uniqID = data.GetUniqID();
			if (uniqID != 0L)
			{
				is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iTEM_ICON_TYPE, data.GetUniqID());
			}
			break;
		}
		default:
			is_new = true;
			break;
		case ITEM_ICON_TYPE.NONE:
			break;
		}
		int enemy_icon_id = 0;
		if (iTEM_ICON_TYPE == ITEM_ICON_TYPE.ITEM)
		{
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(data.GetTableID());
			enemy_icon_id = itemData.enemyIconID;
		}
		ItemIcon itemIcon = null;
		if (data.GetIconType() == ITEM_ICON_TYPE.QUEST_ITEM)
		{
			ItemIcon.ItemIconCreateParam itemIconCreateParam = new ItemIcon.ItemIconCreateParam();
			itemIconCreateParam.icon_type = data.GetIconType();
			itemIconCreateParam.icon_id = data.GetIconID();
			itemIconCreateParam.rarity = data.GetRarity();
			itemIconCreateParam.parent = holder;
			itemIconCreateParam.element = data.GetIconElement();
			itemIconCreateParam.magi_enable_equip_type = data.GetIconMagiEnableType();
			itemIconCreateParam.num = data.GetNum();
			itemIconCreateParam.enemy_icon_id = enemy_icon_id;
			itemIconCreateParam.questIconSizeType = ItemIcon.QUEST_ICON_SIZE_TYPE.REWARD_DELIVERY_LIST;
			itemIcon = ItemIcon.Create(itemIconCreateParam);
		}
		else
		{
			itemIcon = ItemIcon.Create(iTEM_ICON_TYPE, icon_id, rarity, holder, element, magi_enable_icon_type, num, "DROP", event_data, is_new, -1, false, null, false, enemy_icon_id, 0, false, GET_TYPE.PAY, ELEMENT_TYPE.MAX);
		}
		itemIcon.SetRewardBG(false);
		SetMaterialInfo(itemIcon.transform, data.GetMaterialType(), data.GetTableID(), GetCtrl(UI.PNL_MATERIAL_INFO));
	}
}
