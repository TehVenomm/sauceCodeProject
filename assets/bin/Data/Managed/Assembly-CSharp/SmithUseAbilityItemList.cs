using System;
using System.Collections.Generic;
using UnityEngine;

public class SmithUseAbilityItemList : GameSection
{
	public enum UI
	{
		OBJ_CAPTION_3,
		LBL_CAPTION,
		SCR_INVENTORY,
		GRD_INVENTORY,
		GRD_INVENTORY_SMALL,
		TGL_CHANGE_INVENTORY,
		BTN_SELL_MODE_END,
		BTN_SELL,
		STR_HAVE_NUM,
		LBL_NOW_HAVE_NUM,
		LBL_MAX_HAVE_NUM,
		OBJ_BTN_SELL_MODE,
		OBJ_SELL_MODE_ROOT,
		LBL_TOTAL,
		LBL_MAX_SELECT_NUM,
		LBL_SELECT_NUM,
		LBL_SORT,
		TGL_ICON_ASC,
		BTN_CHANGE
	}

	public enum SHOW_INVENTORY_MODE
	{
		MAIN_STATUS
	}

	private EquipItemInfo equipItemInfo;

	private bool isSellMode;

	private List<AbilityItemSortData> sellItemData = new List<AbilityItemSortData>();

	private SHOW_INVENTORY_MODE currentShowInventoryMode;

	private ItemStorageTop.AbilityItemInventory inventory;

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return base.GetUpdateUINotifyFlags() | NOTIFY_FLAG.UPDATE_ABILITY_ITEM_INVENTORY;
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.UPDATE_ABILITY_ITEM_INVENTORY) != (NOTIFY_FLAG)0L)
		{
			inventory = null;
			SetDirty(GetCurrentInventoryRoot());
		}
		base.OnNotify(flags);
	}

	public override void Initialize()
	{
		equipItemInfo = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().selectEquipData;
		currentShowInventoryMode = SHOW_INVENTORY_MODE.MAIN_STATUS;
		InitializeCaption();
		base.Initialize();
	}

	public unsafe override void UpdateUI()
	{
		if (inventory == null)
		{
			inventory = new ItemStorageTop.AbilityItemInventory();
		}
		SetActive((Enum)UI.OBJ_BTN_SELL_MODE, !isSellMode);
		SetActive((Enum)UI.OBJ_SELL_MODE_ROOT, isSellMode);
		SetLabelText((Enum)UI.LBL_MAX_HAVE_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxAbilityItem.ToString());
		SetLabelText((Enum)UI.LBL_NOW_HAVE_NUM, inventory.datas.Length.ToString());
		SetActive((Enum)UI.GRD_INVENTORY, false);
		SetActive((Enum)UI.GRD_INVENTORY_SMALL, false);
		SetLabelText((Enum)UI.LBL_SORT, inventory.sortSettings.GetSortLabel());
		SetToggle((Enum)UI.TGL_ICON_ASC, inventory.sortSettings.orderTypeAsc);
		if (isSellMode)
		{
			SetLabelText((Enum)UI.LBL_MAX_SELECT_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX.ToString());
			SetLabelText((Enum)UI.LBL_SELECT_NUM, sellItemData.Count.ToString());
			int num = 0;
			foreach (AbilityItemSortData sellItemDatum in sellItemData)
			{
				num += sellItemDatum.itemData.GetItemTableData().price;
			}
			SetLabelText((Enum)UI.LBL_TOTAL, num.ToString());
		}
		UI currentInventoryRoot = GetCurrentInventoryRoot();
		SetActive((Enum)currentInventoryRoot, true);
		SetDynamicList((Enum)currentInventoryRoot, (string)null, inventory.datas.Length, false, new Func<int, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), null, new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		SetActive((Enum)UI.BTN_CHANGE, false);
		UpdateAnchors();
		base.UpdateUI();
	}

	public override void Exit()
	{
		MonoBehaviourSingleton<InventoryManager>.I.DoRemoveNewFragsAbilityItem();
		base.Exit();
	}

	private ItemIcon CreateIcon(AbilityItemSortData item_data, Transform parent, int index)
	{
		bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(ITEM_ICON_TYPE.ABILITY_ITEM, item_data.GetUniqID());
		MonoBehaviourSingleton<InventoryManager>.I.AddShowFragsAbilityItem(item_data.GetUniqID());
		ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(item_data.GetTableID());
		return ItemIconDetail.CreateMaterialIcon(item_data.GetIconType(), item_data.GetIconID(), item_data.GetRarity(), itemData, true, parent, item_data.GetNum(), item_data.GetName(), "SELECT_ITEM", index, -1, false, is_new);
	}

	private void OnQuery_CHANGE_INVENTORY()
	{
		currentShowInventoryMode = SHOW_INVENTORY_MODE.MAIN_STATUS;
		SetToggle((Enum)UI.TGL_CHANGE_INVENTORY, true);
		RefreshUI();
	}

	private void OnQuery_SELECT_ITEM()
	{
		AbilityItemSortData abilityItemSortData = GameSection.GetEventData() as AbilityItemSortData;
		if (abilityItemSortData == null)
		{
			GameSection.StopEvent();
		}
		else if (isSellMode)
		{
			ChangeSellModeSelectItem(abilityItemSortData);
		}
		else
		{
			GameSection.SetEventData(new object[2]
			{
				equipItemInfo,
				abilityItemSortData
			});
		}
	}

	private void OnQuery_SELL_MODE()
	{
		isSellMode = !isSellMode;
		RefreshUI();
	}

	private void OnQuery_SELL_MODE_END()
	{
		sellItemData.Clear();
		isSellMode = !isSellMode;
		RefreshUI();
	}

	protected void OnQuery_SELL()
	{
		if (sellItemData.Count == 0)
		{
			GameSection.ChangeEvent("SELL_NOT_SELECT", null);
		}
		else
		{
			GameSection.SetEventData(sellItemData);
		}
	}

	private void OnQuery_LESS_RARITY()
	{
		AbilityItemSortData abilityItemSortData = GameSection.GetEventData() as AbilityItemSortData;
		if (abilityItemSortData == null)
		{
			GameSection.StopEvent();
		}
		else if (isSellMode)
		{
			ChangeSellModeSelectItem(abilityItemSortData);
		}
	}

	private void OnQuery_SECTION_BACK()
	{
		GameSection.SetEventData(equipItemInfo);
	}

	private void OnQuery_SORT()
	{
		GameSection.SetEventData(inventory.sortSettings.Clone());
	}

	private void InitializeCaption()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Transform ctrl = GetCtrl(UI.OBJ_CAPTION_3);
		SetLabelText(ctrl, UI.LBL_CAPTION, StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 26u));
		UITweenCtrl component = ctrl.get_gameObject().GetComponent<UITweenCtrl>();
		if (component != null)
		{
			component.Reset();
			int i = 0;
			for (int num = component.tweens.Length; i < num; i++)
			{
				component.tweens[i].ResetToBeginning();
			}
			component.Play(true, null);
		}
	}

	private UI GetCurrentInventoryRoot()
	{
		return (currentShowInventoryMode != 0) ? UI.GRD_INVENTORY_SMALL : UI.GRD_INVENTORY;
	}

	private void ChangeSellModeSelectItem(AbilityItemSortData abilityItem)
	{
		if (sellItemData.Contains(abilityItem))
		{
			sellItemData.Remove(abilityItem);
			RefreshSelectSell();
			GameSection.StopEvent();
		}
		else if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX > sellItemData.Count)
		{
			sellItemData.Add(abilityItem);
			RefreshSelectSell();
			GameSection.StopEvent();
		}
		else
		{
			GameSection.ChangeEvent("OVER_SELL_ITEM", null);
		}
	}

	private void RefreshSelectSell()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		ItemIcon[] componentsInChildren = GetCtrl(GetCurrentInventoryRoot()).GetComponentsInChildren<ItemIcon>();
		foreach (ItemIcon itemIcon in componentsInChildren)
		{
			ulong uniqueId = itemIcon.GetUniqID;
			int num = sellItemData.FindIndex((AbilityItemSortData x) => x.GetUniqID() == uniqueId);
			itemIcon.selectFrame.get_gameObject().SetActive(num >= 0);
			if (itemIcon is ItemIconDetail)
			{
				(itemIcon as ItemIconDetail).setupperEquip.SetupSelectNumberSprite(num + 1);
			}
			else
			{
				(itemIcon as ItemIconDetailSmall).SetupSelectNumberSprite(num + 1);
			}
		}
		SetSellInfoView();
	}

	private void SetSellInfoView()
	{
		SetLabelText((Enum)UI.LBL_MAX_SELECT_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX.ToString());
		SetLabelText((Enum)UI.LBL_SELECT_NUM, sellItemData.Count.ToString());
		int num = 0;
		foreach (AbilityItemSortData sellItemDatum in sellItemData)
		{
			num += sellItemDatum.itemData.GetItemTableData().price;
		}
		SetLabelText((Enum)UI.LBL_TOTAL, num.ToString());
	}

	private void OnCloseDialog_AbilityItemSellIncludeRareConfirm()
	{
		OnCloseDialog_AbilityItemSellConfirm();
	}

	private void OnCloseDialog_AbilityItemSellConfirm()
	{
		inventory = null;
		isSellMode = false;
		sellItemData.Clear();
		SetDirty(GetCurrentInventoryRoot());
		RefreshUI();
	}

	private void OnCloseDialog_ItemStorageAbilityItemSort()
	{
		CloseSort();
	}

	protected void CloseSort()
	{
		if (GameSection.GetEventData() is SortSettings && inventory.Sort(GameSection.GetEventData() as SortSettings))
		{
			SetDirty(UI.GRD_INVENTORY);
			SetDirty(UI.GRD_INVENTORY_SMALL);
			RefreshUI();
		}
	}
}
