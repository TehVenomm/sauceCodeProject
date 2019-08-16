using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingPostItemDetail : GameSection
{
	private enum UI
	{
		BTN_SELL,
		BTN_QUATITY,
		BTN_PRICE,
		SPR_PRICE_ARROW,
		BTN_REFRESH,
		PNL_MATERIAL_INFO,
		LBL_CRYSTAL_NUM,
		OBJ_DETAIL_BASE,
		LBL_SELL_ITEM,
		LBL_QTY_NORMAL,
		LBL_QTY_SELECT,
		LBL_PRICE_NORMAL,
		LBL_PRICE_SELECT,
		LBL_REFRESH,
		LBL_SELL,
		STR_SELL,
		SPR_NEED,
		STR_HAVE,
		LBL_HAVE_NUM,
		LBL_NAME,
		OBJ_ICON_ROOT,
		STR_WINDOW_TITLE,
		STR_NOT_HOWTO,
		SCR_HOWTO,
		GRD_HOWTO,
		LBL_TOTAL_TEXT,
		LBL_TOTAL_NUM,
		LBL_START_TEXT,
		LBL_COST,
		OBJ_ICON,
		OBJ_NOT_LICENSED,
		LBL_NOTICE,
		LBL_LOGIN,
		LBL_REQ_LOGIN,
		LBL_REQ_LICENSE
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

	private SORT_TYPE _sortType;

	private TradingPostInfo info;

	private List<TradingPostDetail> details = new List<TradingPostDetail>();

	private TradingPostDetail currentDetail;

	private bool close;

	private int currentPage = 1;

	private bool endPage;

	private UIScrollView scrollview;

	private bool refresh;

	private bool next;

	private ItemSortData itemData;

	public override void Initialize()
	{
		scrollview = base.GetComponent<UIScrollView>((Enum)UI.SCR_HOWTO);
		UIScrollView uIScrollView = scrollview;
		uIScrollView.onReachBottom = (UIScrollView.OnDragNotification)Delegate.Combine(uIScrollView.onReachBottom, new UIScrollView.OnDragNotification(OnScrollViewReachBottom));
		this.StartCoroutine(DoInitialize());
	}

	protected override void OnDestroy()
	{
		if (scrollview != null)
		{
			UIScrollView uIScrollView = scrollview;
			uIScrollView.onStoppedMoving = (UIScrollView.OnDragNotification)Delegate.Remove(uIScrollView.onStoppedMoving, new UIScrollView.OnDragNotification(OnScrollViewReachBottom));
		}
		base.OnDestroy();
	}

	private IEnumerator DoInitialize()
	{
		info = MonoBehaviourSingleton<TradingPostManager>.I.Viewinfo;
		ItemInfo itemInfo = ItemInfo.CreateItemInfo(info.itemId);
		itemData = new ItemSortData();
		itemData.SetItem(itemInfo);
		bool wait = true;
		MonoBehaviourSingleton<TradingPostManager>.I.SendRequestItemDetail(info.itemId, currentPage, delegate(bool success, List<TradingPostDetail> list)
		{
			wait = false;
			details.AddRange(list);
		});
		while (wait)
		{
			yield return null;
		}
		base.Initialize();
	}

	public override void UpdateUI()
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
		int itemNum = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableID == itemData.GetTableID());
		bool flag2 = details.Count == 0;
		SetLabelText(UI.LBL_CRYSTAL_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal);
		Transform ctrl = GetCtrl(UI.OBJ_DETAIL_BASE);
		SetItemIcon(FindCtrl(ctrl, UI.OBJ_ICON_ROOT), itemData);
		SetActive(ctrl, UI.SPR_NEED, is_visible: false);
		SetActive(ctrl, UI.STR_SELL, itemData.CanSale());
		SetActive(ctrl, UI.OBJ_NOT_LICENSED, TradingPostManager.IsPurchasedLicense());
		SetActive(ctrl, UI.STR_NOT_HOWTO, flag2);
		SetLabelText(ctrl, UI.LBL_NAME, itemData.GetName());
		SetLabelText(ctrl, UI.STR_WINDOW_TITLE, string.Format(base.sectionData.GetText("STR_TITLE_TEXT"), details.Count));
		SetLabelText(ctrl, UI.STR_HAVE, base.sectionData.GetText("STR_OWN"));
		SetLabelText(ctrl, UI.LBL_HAVE_NUM, itemNum);
		SetLabelText(ctrl, UI.STR_SELL, base.sectionData.GetText("STR_VALUE"));
		SetLabelText(ctrl, UI.LBL_SELL, itemData.GetSalePrice());
		if (flag2)
		{
			SetLabelText(ctrl, UI.STR_NOT_HOWTO, base.sectionData.GetText("STR_EMPTY"));
		}
		if (_sortType != 0)
		{
			if (_sortType == SORT_TYPE.QUANTITY)
			{
				details.Sort((TradingPostDetail e1, TradingPostDetail e2) => e1.quantity.CompareTo(e2.quantity));
			}
			else if (_sortType == SORT_TYPE.PRICE_UP)
			{
				details.Sort((TradingPostDetail e1, TradingPostDetail e2) => e1.price.CompareTo(e2.price));
			}
			else
			{
				details.Sort((TradingPostDetail e1, TradingPostDetail e2) => e2.price.CompareTo(e1.price));
			}
			SetSortButtonUIStatus();
		}
		SetGrid(ctrl, UI.GRD_HOWTO, "TradingPostListBaseItem", details.Count, reset: false, delegate(int i, Transform t, bool b)
		{
			InitItemList(details[i], t, i);
		});
	}

	private void OnScrollViewReachBottom()
	{
		if (TradingPostManager.IsFulfillRequirement() && TradingPostManager.IsFinishTradingPostTutorial())
		{
			Next();
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

	private void AddList(List<TradingPostDetail> list)
	{
		AddItemList(GetCtrl(UI.GRD_HOWTO), "TradingPostListBaseItem", list.Count, null, null, delegate(int i, Transform t, bool b)
		{
			InitItemList(list[i], t, details.Count + i);
		});
	}

	private void InitItemList(TradingPostDetail detail, Transform t, int i)
	{
		SetLabelText(t, UI.LBL_TOTAL_TEXT, base.sectionData.GetText("STR_QUATITY"));
		SetLabelText(t, UI.LBL_START_TEXT, base.sectionData.GetText("STR_PRICE"));
		SetLabelText(t, UI.LBL_NAME, detail.from);
		SetLabelText(t, UI.LBL_TOTAL_NUM, detail.quantity);
		SetLabelText(t, UI.LBL_COST, detail.price);
		SetItemIcon(FindCtrl(t, UI.OBJ_ICON), itemData);
		SetEvent(t, "BUY", i);
	}

	private void Update()
	{
		if (close && MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() && !MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
		{
			close = false;
			GameSection.BackSection();
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(NOTIFY_FLAG.UPDATE_TRADING_POST);
		}
		else
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
		SetButtonSprite((Enum)ui, sprite_name, with_press: true);
	}

	private void SetSortButtonUIStatus()
	{
		BoxCollider component = GetCtrl(UI.BTN_QUATITY).get_gameObject().GetComponent<BoxCollider>();
		switch (_sortType)
		{
		case SORT_TYPE.QUANTITY:
			SetActive((Enum)UI.LBL_PRICE_NORMAL, is_visible: true);
			SetActive((Enum)UI.LBL_PRICE_SELECT, is_visible: false);
			SetBtnActive(UI.BTN_QUATITY, active: true);
			SetBtnActive(UI.BTN_PRICE, active: false);
			component.set_enabled(false);
			SetPriceArrowStatus(isVisible: false, isUp: false);
			break;
		case SORT_TYPE.PRICE_DOWN:
			SetActive((Enum)UI.LBL_PRICE_NORMAL, is_visible: false);
			SetActive((Enum)UI.LBL_PRICE_SELECT, is_visible: true);
			SetBtnActive(UI.BTN_QUATITY, active: false);
			SetBtnActive(UI.BTN_PRICE, active: true);
			component.set_enabled(true);
			SetPriceArrowStatus(isVisible: true, isUp: false);
			break;
		case SORT_TYPE.PRICE_UP:
			SetActive((Enum)UI.LBL_PRICE_NORMAL, is_visible: false);
			SetActive((Enum)UI.LBL_PRICE_SELECT, is_visible: true);
			SetBtnActive(UI.BTN_QUATITY, active: false);
			SetBtnActive(UI.BTN_PRICE, active: true);
			component.set_enabled(true);
			SetPriceArrowStatus(isVisible: true, isUp: true);
			break;
		default:
			SetActive((Enum)UI.LBL_PRICE_NORMAL, is_visible: true);
			SetActive((Enum)UI.LBL_PRICE_SELECT, is_visible: false);
			SetBtnActive(UI.BTN_QUATITY, active: false);
			SetBtnActive(UI.BTN_PRICE, active: false);
			component.set_enabled(true);
			SetPriceArrowStatus(isVisible: false, isUp: false);
			break;
		}
	}

	private void SetPriceArrowStatus(bool isVisible, bool isUp)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
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
		if (!MonoBehaviourSingleton<InventoryManager>.I.IsHaveingItem((uint)info.itemId))
		{
			GameSection.ChangeEvent("NO_ITEM");
		}
		else
		{
			MonoBehaviourSingleton<TradingPostManager>.I.SetTradingPostSellItemData((uint)info.itemId, 0uL, MonoBehaviourSingleton<InventoryManager>.I.GetHaveingItemNum((uint)info.itemId));
		}
	}

	private void OnQuery_REFRESH()
	{
		currentPage = 1;
		endPage = false;
		details.Clear();
		ResetSort();
		GameSection.StayEvent();
		MonoBehaviourSingleton<TradingPostManager>.I.SendRequestItemDetail((int)itemData.GetTableID(), currentPage, delegate(bool b, List<TradingPostDetail> list)
		{
			if (list.Count == 0)
			{
				endPage = true;
				GameSection.ResumeEvent(is_resume: false);
			}
			else
			{
				details.AddRange(list);
				RefreshUI();
				GameSection.ResumeEvent(is_resume: false);
			}
		});
	}

	private void OnQuery_NEXT()
	{
		currentPage++;
		GameSection.StayEvent();
		MonoBehaviourSingleton<TradingPostManager>.I.SendRequestItemDetail((int)itemData.GetTableID(), currentPage, delegate(bool b, List<TradingPostDetail> list)
		{
			if (list.Count == 0)
			{
				endPage = true;
				GameSection.ResumeEvent(is_resume: false);
			}
			else
			{
				details.AddRange(list);
				RefreshUI();
				GameSection.ResumeEvent(is_resume: false);
			}
		});
	}

	private void OnQuery_BUY()
	{
		if (!TradingPostManager.IsFulfillRequirement())
		{
			GameSection.ChangeEvent("LICENSE_REQUIRE");
			return;
		}
		int index = (int)GameSection.GetEventData();
		currentDetail = details[index];
		GameSection.SetEventData(currentDetail);
	}

	private void OnQuery_PURCHASE()
	{
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.crystal < currentDetail.price)
		{
			GameSection.ChangeEvent("LACK");
			return;
		}
		GameSection.StayEvent();
		MonoBehaviourSingleton<TradingPostManager>.I.SendRequestBuyItem(currentDetail.transactionId, delegate(bool isSuccess, Error ret)
		{
			if (isSuccess)
			{
				details.Remove(currentDetail);
				currentDetail = null;
				RefreshUI();
				GameSection.ChangeStayEvent("SUCCESS");
			}
			GameSection.ResumeEvent(isSuccess);
		});
	}

	private void OnCloseDialog_CrystalShopMessage()
	{
		RefreshUI();
	}

	private void OnCloseDialog_CrystalShopTradingPostLicense()
	{
		close = true;
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
			if (uniqID != 0)
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
			itemIcon = ItemIcon.Create(iTEM_ICON_TYPE, icon_id, rarity, holder, element, magi_enable_icon_type, num, "DROP", event_data, is_new, -1, is_select: false, null, is_equipping: false, enemy_icon_id);
		}
		itemIcon.SetRewardBG(is_visible: false);
		SetMaterialInfo(itemIcon.transform, data.GetMaterialType(), data.GetTableID(), GetCtrl(UI.PNL_MATERIAL_INFO));
	}
}
