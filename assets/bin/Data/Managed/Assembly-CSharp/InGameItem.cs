using System;
using UnityEngine;

public class InGameItem : GameSection
{
	private enum UI
	{
		BG,
		SCR_INVENTORY,
		GRD_INVENTORY,
		GRD_INVENTORY_SMALL,
		OBJ_BACK,
		OBJ_CAPTION_3,
		TGL_CHANGE_INVENTORY,
		BTN_ITEM_SHOP
	}

	public class InGameUseItemInventory : ItemStorageTop.UseItemInventory
	{
		protected override bool IsUseItemType(ITEM_TYPE item_type)
		{
			return item_type == ITEM_TYPE.USE_ITEM;
		}

		public override ItemIcon CreateIcon(object[] data)
		{
			SortCompareData sortCompareData = data[0] as SortCompareData;
			Transform parent = data[1] as Transform;
			int event_data = (int)data[2];
			bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(ITEM_ICON_TYPE.ITEM, sortCompareData.GetUniqID());
			ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(sortCompareData.GetTableID());
			ItemStorageTop.SHOW_INVENTORY_MODE sHOW_INVENTORY_MODE = (ItemStorageTop.SHOW_INVENTORY_MODE)(int)data[3];
			if (sHOW_INVENTORY_MODE != ItemStorageTop.SHOW_INVENTORY_MODE.SMALL)
			{
				return ItemIconDetail.CreateMaterialIcon(sortCompareData.GetIconType(), sortCompareData.GetIconID(), sortCompareData.GetRarity(), itemData, sHOW_INVENTORY_MODE == ItemStorageTop.SHOW_INVENTORY_MODE.MAIN_STATUS, parent, sortCompareData.GetNum(), sortCompareData.GetName(), "SELECT", event_data, -1, false, is_new);
			}
			return ItemIconDetailSmall.CreateSmallMaterialIcon(sortCompareData.GetIconType(), sortCompareData.GetIconID(), sortCompareData.GetRarity(), parent, sortCompareData.GetNum(), sortCompareData.GetName(), "SELECT", event_data, -1, false, is_new, 0, 0, ItemIconDetail.ICON_STATUS.NONE);
		}
	}

	private bool isInActiveRotate;

	private ItemStorageTop.SHOW_INVENTORY_MODE showInventoryMode;

	private InGameUseItemInventory inventory;

	public override void Initialize()
	{
		base.Initialize();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
			isInActiveRotate = true;
		}
		PlayTween((Enum)UI.OBJ_CAPTION_3, true, (EventDelegate.Callback)null, false, 0);
	}

	public override void Exit()
	{
		base.Exit();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= OnScreenRotate;
		}
	}

	public override void UpdateUI()
	{
		UpdateAnchors();
		base.UpdateUI();
		SetToggle((Enum)UI.TGL_CHANGE_INVENTORY, showInventoryMode != ItemStorageTop.SHOW_INVENTORY_MODE.SMALL);
		inventory = new InGameUseItemInventory();
		SetDynamicList((Enum)SelectListTarget(showInventoryMode), (string)null, inventory.datas.Length, false, (Func<int, bool>)delegate(int i)
		{
			SortCompareData sortCompareData2 = inventory.datas[i];
			if (sortCompareData2 == null || !sortCompareData2.IsPriority(inventory.sortSettings.orderTypeAsc))
			{
				return false;
			}
			return true;
		}, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycre)
		{
			SortCompareData sortCompareData = inventory.datas[i];
			ItemIcon itemIcon = inventory.CreateIcon(new object[4]
			{
				sortCompareData,
				t,
				i,
				showInventoryMode
			});
			if (itemIcon != null)
			{
				itemIcon.toggleSelectFrame.onChange.Clear();
				itemIcon.toggleSelectFrame.onChange.Add(new EventDelegate(this, "IconToggleChange"));
				SetEvent(itemIcon.transform, "DETAIL", i);
				SetLongTouch(itemIcon.transform, "DETAIL", i);
			}
		});
		UIPanel component = GetCtrl(UI.SCR_INVENTORY).GetComponent<UIPanel>();
		component.Refresh();
		if (isInActiveRotate && MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			Reposition(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		}
		isInActiveRotate = false;
	}

	private void Reposition(bool isPortrait)
	{
		UIScreenRotationHandler[] components = GetCtrl(UI.BG).GetComponents<UIScreenRotationHandler>();
		for (int i = 0; i < components.Length; i++)
		{
			components[i].InvokeRotate();
		}
		GetCtrl(UI.OBJ_BACK).GetComponent<UIScreenRotationHandler>().InvokeRotate();
		GetCtrl(UI.BTN_ITEM_SHOP).GetComponent<UIScreenRotationHandler>().InvokeRotate();
		GetCtrl(UI.BG).GetComponent<UIRect>().UpdateAnchors();
		UpdateAnchors();
		UIScrollView component = GetCtrl(UI.SCR_INVENTORY).GetComponent<UIScrollView>();
		component.ResetPosition();
		AppMain i2 = MonoBehaviourSingleton<AppMain>.I;
		i2.onDelayCall = (Action)Delegate.Combine(i2.onDelayCall, (Action)delegate
		{
			RefreshUI();
		});
	}

	private void OnScreenRotate(bool isPortrait)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		if (base.transferUI != null)
		{
			isInActiveRotate = !base.transferUI.get_gameObject().get_activeInHierarchy();
		}
		else
		{
			isInActiveRotate = !base.collectUI.get_gameObject().get_activeInHierarchy();
		}
		if (!isInActiveRotate)
		{
			Reposition(isPortrait);
		}
	}

	private UI SelectListTarget(ItemStorageTop.SHOW_INVENTORY_MODE show_detail_icon)
	{
		if (show_detail_icon != ItemStorageTop.SHOW_INVENTORY_MODE.SMALL)
		{
			SetActive((Enum)UI.GRD_INVENTORY, true);
			SetActive((Enum)UI.GRD_INVENTORY_SMALL, false);
			return UI.GRD_INVENTORY;
		}
		SetActive((Enum)UI.GRD_INVENTORY, false);
		SetActive((Enum)UI.GRD_INVENTORY_SMALL, true);
		return UI.GRD_INVENTORY_SMALL;
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.UPDATE_ITEM_INVENTORY) != (NOTIFY_FLAG)0L)
		{
			SetDirty(SelectListTarget(showInventoryMode));
			RefreshUI();
		}
		base.OnNotify(flags);
	}

	private void OnQuery_CHANGE_INVENTORY()
	{
		showInventoryMode = ((showInventoryMode != ItemStorageTop.SHOW_INVENTORY_MODE.SMALL) ? (showInventoryMode + 1) : ItemStorageTop.SHOW_INVENTORY_MODE.MAIN_STATUS);
		SetDirty(UI.GRD_INVENTORY);
		SetDirty(UI.GRD_INVENTORY_SMALL);
		RefreshUI();
	}

	private void OnQuery_DETAIL()
	{
		int num = (int)GameSection.GetEventData();
		GameSection.ChangeEvent("USE_ITEM_SELECT", inventory.datas[num]);
	}

	private void OnQuery_ITEM_SHOP()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<ShopManager>.I.SendGetShop(delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null);
		});
	}
}
