using UnityEngine;

public abstract class SkillSelectBase : ItemDetailSkill
{
	protected new enum UI
	{
		OBJ_DETAIL_ROOT,
		TEX_MODEL,
		TEX_INNER_MODEL,
		LBL_NAME,
		LBL_LV_NOW,
		LBL_LV_MAX,
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		LBL_SELL,
		LBL_DESCRIPTION,
		OBJ_FAVORITE_ROOT,
		TWN_FAVORITE,
		TWN_UNFAVORITE,
		OBJ_SUB_STATUS,
		SPR_SKILL_TYPE_ICON,
		SPR_SKILL_TYPE_ICON_BG,
		SPR_SKILL_TYPE_ICON_RARITY,
		STR_TITLE_ITEM_INFO,
		STR_TITLE_DESCRIPTION,
		STR_TITLE_STATUS,
		STR_TITLE_SELL,
		PRG_EXP_BAR,
		OBJ_NEXT_EXP_ROOT,
		BTN_DECISION,
		STR_DECISION_R,
		BTN_SKILL_DECISION,
		STR_SKILL_DECISION,
		STR_SKILL_DECISION_R,
		OBJ_SKILL_INFO_ROOT,
		LBL_EQUIP_ITEM_NAME,
		SCR_INVENTORY,
		GRD_INVENTORY,
		GRD_INVENTORY_SMALL,
		LBL_SORT,
		BTN_BACK,
		TGL_CHANGE_INVENTORY,
		TGL_ICON_ASC,
		BTN_CHANGE_INVENTORY,
		OBJ_EMPTY_SKILL_ROOT,
		TEX_EMPTY_SKILL,
		SPR_EMPTY_SKILL,
		LBL_EMPTY_SKILL_TYPE,
		OBJ_CAPTION_3,
		LBL_CAPTION
	}

	protected SkillItemInfo equipSkillItem;

	protected EquipItemInfo equipItem;

	protected ItemStorageTop.SkillItemInventory inventory;

	protected int selectIndex;

	protected SkillItemInfo selectSkillItem;

	protected bool updateInventory;

	protected int inventoryUIIndex;

	protected UI inventoryUI;

	protected UI[] switchInventoryAry = new UI[1]
	{
		UI.GRD_INVENTORY
	};

	protected bool IsShowMainStatus => inventoryUIIndex == 0;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		if (array != null && array.Length > 2)
		{
			equipSkillItem = (array[1] as SkillItemInfo);
			equipItem = (array[2] as EquipItemInfo);
		}
		GameSection.SetEventData(new object[2]
		{
			(ItemDetailEquip.CURRENT_SECTION)(int)array[0],
			null
		});
		base.Initialize();
	}

	protected override SkillItemInfo GetCompareItem()
	{
		return equipSkillItem;
	}

	public override void UpdateUI()
	{
		SetFontStyle(UI.STR_TITLE_ITEM_INFO, FontStyle.Italic);
		SetFontStyle(UI.STR_TITLE_DESCRIPTION, FontStyle.Italic);
		SetFontStyle(UI.STR_TITLE_STATUS, FontStyle.Italic);
		SetFontStyle(UI.STR_TITLE_SELL, FontStyle.Italic);
		SetActive(UI.BTN_DECISION, true);
		SetActive(UI.BTN_SKILL_DECISION, false);
		SetLabelText(UI.STR_DECISION_R, base.sectionData.GetText("STR_DECISION"));
		SetActive(UI.BTN_CHANGE_INVENTORY, false);
		if (inventory == null || updateInventory)
		{
			selectIndex = GetInventoryFirstIndex();
			inventory = CreateInventory();
			if (inventory.datas.Length > 0)
			{
				if (selectSkillItem == null)
				{
					if (equipSkillItem != null)
					{
						selectIndex = GetSelectItemIndex(equipSkillItem);
					}
					if (selectIndex >= 0)
					{
						selectSkillItem = (inventory.datas[selectIndex].GetItemData() as SkillItemInfo);
					}
					else
					{
						selectSkillItem = null;
					}
				}
				else
				{
					selectIndex = GetSelectItemIndex(selectSkillItem);
				}
			}
			updateInventory = false;
		}
		SetInventoryIsEmptyParam();
		SetLabelText(UI.LBL_SORT, inventory.sortSettings.GetSortLabel());
		SetToggle(UI.TGL_ICON_ASC, inventory.sortSettings.orderTypeAsc);
		UpdateInventoryUI();
		UpdateParam();
	}

	protected virtual void SetInventoryIsEmptyParam()
	{
	}

	protected virtual void UpdateParam()
	{
		itemData = selectSkillItem;
		base.UpdateUI();
		UpdateAnchors();
	}

	protected override void SetupDetailBase()
	{
		_SetupSkillInfoRoot();
	}

	protected void _SetupDetailBase()
	{
		SetActive(UI.OBJ_SKILL_INFO_ROOT, false);
		base.SetupDetailBase();
	}

	protected void _SetupSkillInfoRoot()
	{
		SetActive(UI.OBJ_SKILL_INFO_ROOT, true);
		detailBase = GetCtrl(UI.OBJ_SKILL_INFO_ROOT);
	}

	protected virtual int GetInventoryFirstIndex()
	{
		return 0;
	}

	protected virtual ItemStorageTop.SkillItemInventory CreateInventory()
	{
		return new ItemStorageTop.SkillItemInventory(SortSettings.SETTINGS_TYPE.SKILL_ITEM, SKILL_SLOT_TYPE.NONE, false);
	}

	protected virtual void UpdateInventoryUI()
	{
		SetupEnableInventoryUI();
		m_generatedIconList.Clear();
		UpdateNewIconInfo();
		SetDynamicList(inventoryUI, null, inventory.datas.Length, false, delegate(int i)
		{
			SortCompareData sortCompareData2 = inventory.datas[i];
			if (sortCompareData2 == null || !sortCompareData2.IsPriority(inventory.sortSettings.orderTypeAsc))
			{
				return false;
			}
			return true;
		}, null, delegate(int i, Transform t, bool is_recycle)
		{
			SortCompareData sortCompareData = inventory.datas[i];
			if (sortCompareData != null && sortCompareData.IsPriority(inventory.sortSettings.orderTypeAsc))
			{
				ITEM_ICON_TYPE iconType = sortCompareData.GetIconType();
				bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iconType, sortCompareData.GetUniqID());
				ItemIcon itemIcon = CreateItemIconDetail(iconType, sortCompareData.GetIconID(), sortCompareData.GetRarity(), sortCompareData as SkillItemSortData, IsShowMainStatus, t, "SELECT", i, is_new, 100, selectIndex == i, sortCompareData.IsEquipping(), sortCompareData.IsExceeded(), false);
				itemIcon.SetItemID(sortCompareData.GetTableID());
				itemIcon.SetButtonColor(inventory.datas[i].IsPriority(inventory.sortSettings.orderTypeAsc), true);
				SetLongTouch(itemIcon.transform, "DETAIL", i);
				if ((Object)itemIcon != (Object)null && sortCompareData != null)
				{
					itemIcon.SetInitData(sortCompareData);
				}
				if (!m_generatedIconList.Contains(itemIcon))
				{
					m_generatedIconList.Add(itemIcon);
				}
			}
		});
	}

	protected void OnQuery_SORT()
	{
		if (inventory == null || inventory.datas.Length == 0)
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.SetEventData(inventory.sortSettings.Clone());
		}
	}

	protected virtual void OnCloseSort()
	{
		SortSettings sortSettings = (SortSettings)GameSection.GetEventData();
		if (sortSettings != null)
		{
			SortCompareData sortCompareData = null;
			if (selectIndex >= 0)
			{
				sortCompareData = inventory.datas[selectIndex];
			}
			if (inventory.Sort(sortSettings))
			{
				if (sortCompareData != null)
				{
					selectIndex = GetSelectItemIndex(sortCompareData.GetItemData() as SkillItemInfo);
				}
				SetDirty(UI.GRD_INVENTORY);
				RefreshUI();
			}
		}
	}

	protected virtual void OnQuery_SELECT()
	{
		int num = (int)GameSection.GetEventData();
		if (num >= 0)
		{
			selectIndex = num;
			selectSkillItem = (inventory.datas[selectIndex].GetItemData() as SkillItemInfo);
		}
		else
		{
			selectIndex = -1;
			selectSkillItem = null;
		}
		UpdateParam();
	}

	private void OnQuery_DETAIL()
	{
		int index = (int)GameSection.GetEventData();
		GameSection.SetEventData(CreateDetailEventData(index));
	}

	protected void OnQuery_DECISION()
	{
		OnDecision();
	}

	protected virtual void OnDecision()
	{
	}

	protected virtual object[] CreateDetailEventData(int index)
	{
		return new object[2]
		{
			ItemDetailEquip.CURRENT_SECTION.STATUS_EQUIP,
			inventory.datas[index].GetItemData()
		};
	}

	protected int GetSelectItemIndex(SkillItemInfo select_item)
	{
		if (select_item != null)
		{
			int i = 0;
			for (int num = inventory.datas.Length; i < num; i++)
			{
				if (inventory.datas[i].GetUniqID() == select_item.uniqueID)
				{
					return i;
				}
			}
		}
		return -1;
	}

	protected void SetupEnableInventoryUI()
	{
		int i = 0;
		for (int num = switchInventoryAry.Length; i < num; i++)
		{
			SetActive(switchInventoryAry[i], false);
		}
		SetActive(switchInventoryAry[inventoryUIIndex], true);
		inventoryUI = switchInventoryAry[inventoryUIIndex];
		SetToggle(UI.TGL_CHANGE_INVENTORY, inventoryUI == UI.GRD_INVENTORY);
	}

	protected virtual void OnQuery_CHANGE_INVENTORY()
	{
		inventoryUIIndex = ((inventoryUIIndex + 1 < switchInventoryAry.Length) ? (inventoryUIIndex + 1) : 0);
		SetDirty(UI.GRD_INVENTORY);
		SetDirty(UI.GRD_INVENTORY_SMALL);
		RefreshUI();
	}

	protected ItemIcon CreateRemoveIcon(Transform parent = null, string event_name = null, int event_data = 0, int toggle_group = -1, bool is_select = false, string name = null)
	{
		if (inventoryUI == UI.GRD_INVENTORY)
		{
			return ItemIconDetail.CreateRemoveButton(parent, event_name, event_data, toggle_group, is_select, name);
		}
		return ItemIconDetailSmall.CreateSmallRemoveButton(parent, event_name, event_data, toggle_group, is_select, name);
	}

	protected ItemIcon CreateItemIconDetail(ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity, SkillItemSortData item_data, bool is_show_main_status, Transform parent = null, string event_name = null, int event_data = 0, bool is_new = false, int toggle_group = -1, bool is_select = false, bool is_equipping = false, bool isValidExceed = false, bool isShowEnableExceed = false)
	{
		if (inventoryUI == UI.GRD_INVENTORY)
		{
			return ItemIconDetail.CreateSkillDetailIcon(icon_type, icon_id, rarity, item_data, is_show_main_status, parent, event_name, event_data, is_new, toggle_group, is_select, is_equipping, isValidExceed, isShowEnableExceed);
		}
		return ItemIconDetailSmall.CreateSmallSkillDetailIcon(icon_type, icon_id, rarity, item_data, parent, event_name, event_data, is_new, toggle_group, is_select, is_equipping, isValidExceed, isShowEnableExceed);
	}
}
