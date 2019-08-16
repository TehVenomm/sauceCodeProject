using System;
using UnityEngine;

public abstract class EquipSelectBase : SmithEquipBase
{
	public enum UI
	{
		LBL_NAME,
		LBL_LV_NOW,
		LBL_LV_MAX,
		LBL_ATK,
		LBL_DEF,
		LBL_ELEM,
		SPR_ELEM,
		LBL_SELL,
		TEX_MODEL,
		SCR_INVENTORY,
		GRD_INVENTORY,
		GRD_INVENTORY_SMALL,
		LBL_SORT,
		BTN_SORT,
		BTN_BACK,
		TGL_CHANGE_INVENTORY,
		TGL_ICON_ASC,
		OBJ_SKILL_BUTTON_ROOT,
		OBJ_DETAIL_ROOT,
		BTN_SELL,
		BTN_GROW,
		OBJ_FAVORITE_ROOT,
		SPR_IS_EVOLVE,
		OBJ_ATK_ROOT,
		OBJ_DEF_ROOT,
		OBJ_ELEM_ROOT,
		OBJ_SELL_ROOT,
		SPR_SELECT_WEAPON,
		SPR_SELECT_DEF,
		SPR_TYPE_ICON,
		SPR_TYPE_ICON_BG,
		SPR_TYPE_ICON_RARITY,
		LBL_SELECT_TYPE,
		OBJ_STATUS_ROOT,
		LBL_STATUS_ATK,
		LBL_STATUS_DEF,
		LBL_STATUS_HP,
		LBL_STATUS_ADD_ATK,
		LBL_STATUS_ADD_DEF,
		LBL_STATUS_ADD_HP,
		STR_TITLE_ITEM_INFO,
		STR_TITLE_STATUS,
		STR_TITLE_SKILL_SLOT,
		STR_TITLE_ABILITY,
		STR_TITLE_MONEY,
		STR_TITLE_MATERIAL,
		STR_TITLE_ELEMENT,
		TBL_ABILITY,
		STR_NON_ABILITY,
		OBJ_ABILITY,
		LBL_ABILITY,
		LBL_ABILITY_NUM,
		OBJ_FIXEDABILITY,
		LBL_FIXEDABILITY,
		LBL_FIXEDABILITY_NUM,
		OBJ_ABILITY_ITEM,
		LBL_ABILITY_ITEM,
		OBJ_WEAPON_WINDOW,
		OBJ_DEFENSE_WINDOW,
		SCR_INVENTORY_DEF,
		GRD_INVENTORY_DEF,
		GRD_INVENTORY_SMALL_DEF,
		BTN_WEAPON_1,
		BTN_WEAPON_2,
		BTN_WEAPON_3,
		BTN_WEAPON_4,
		BTN_WEAPON_5,
		OBJ_CAPTION_3,
		LBL_CAPTION,
		OBJ_ROOT,
		LBL_NO_ITEM
	}

	protected SortCompareData[] localInventoryEquipData;

	protected SortSettings sortSettings;

	protected int selectInventoryIndex;

	protected UI InventoryUI;

	protected UI[] switchInventoryAry = new UI[2]
	{
		UI.GRD_INVENTORY,
		UI.GRD_INVENTORY
	};

	protected int inventoryUIIndex;

	protected UI[] weaponInventoryAry = new UI[2]
	{
		UI.GRD_INVENTORY,
		UI.GRD_INVENTORY
	};

	protected UI[] defenseInventoryAry = new UI[2]
	{
		UI.GRD_INVENTORY_DEF,
		UI.GRD_INVENTORY_DEF
	};

	protected bool IsShowMainStatus => inventoryUIIndex == 0;

	protected virtual EquipItemInfo EquipItem
	{
		get
		{
			return GetEquipData();
		}
		set
		{
		}
	}

	public override void Initialize()
	{
		type = EquipDialogType.SELECT;
		inventoryUIIndex = 0;
		SetDirty(UI.GRD_INVENTORY);
		SetDirty(UI.GRD_INVENTORY_SMALL);
		SetDirty(UI.GRD_INVENTORY_DEF);
		SetDirty(UI.GRD_INVENTORY_SMALL_DEF);
		InitSort();
		InitLocalInventory();
		base.Initialize();
	}

	protected virtual void InitSort()
	{
	}

	protected virtual void InitLocalInventory()
	{
	}

	protected virtual void Update()
	{
		ObserveItemList();
	}

	public override void UpdateUI()
	{
		object obj = ((object)EquipItem) ?? ((object)GetEquipTableData());
		if (obj == null)
		{
			SelectingInventoryFirst();
		}
		else
		{
			selectInventoryIndex = GetSelectItemIndex();
		}
		SetLabelText((Enum)UI.LBL_SELECT_TYPE, GetSelectTypeText());
		SetToggle((Enum)UI.TGL_ICON_ASC, sortSettings.orderTypeAsc);
		SetFontStyle((Enum)UI.STR_TITLE_ITEM_INFO, 2);
		SetFontStyle((Enum)UI.STR_TITLE_STATUS, 2);
		SetFontStyle((Enum)UI.STR_TITLE_SKILL_SLOT, 2);
		SetFontStyle((Enum)UI.STR_TITLE_ABILITY, 2);
		SetFontStyle((Enum)UI.STR_TITLE_MONEY, 2);
		SetFontStyle((Enum)UI.STR_TITLE_MATERIAL, 2);
		SetFontStyle((Enum)UI.STR_TITLE_ELEMENT, 2);
		base.UpdateUI();
	}

	protected virtual string GetSelectTypeText()
	{
		return string.Empty;
	}

	protected virtual EquipItemInfo GetCompareItemData()
	{
		return null;
	}

	private void SetLabelEquipItemParam(EquipItemInfo item, EquipItemInfo comp_item = null)
	{
		int atk = item.atk;
		int def = item.def;
		int elemAtk = item.elemAtk;
		int elemDef = item.elemDef;
		if (comp_item != null)
		{
			atk = comp_item.atk;
			def = comp_item.def;
			elemAtk = comp_item.elemAtk;
			elemDef = comp_item.elemDef;
		}
		bool flag = item.tableData.IsWeapon();
		SetActive((Enum)UI.OBJ_ATK_ROOT, flag);
		SetActive((Enum)UI.OBJ_DEF_ROOT, !flag);
		SetLabelText((Enum)UI.LBL_LV_NOW, item.level.ToString());
		SetLabelText((Enum)UI.LBL_LV_MAX, item.tableData.maxLv.ToString());
		if (flag)
		{
			SetActive((Enum)UI.OBJ_ELEM_ROOT, item.elemAtk > 0);
			SetElementSprite((Enum)UI.SPR_ELEM, item.GetElemAtkType());
			SetLabelCompareParam((Enum)UI.LBL_ATK, item.atk, atk, -1);
			SetLabelCompareParam((Enum)UI.LBL_ELEM, item.elemAtk, elemAtk, -1);
		}
		else
		{
			SetActive((Enum)UI.OBJ_ELEM_ROOT, item.elemDef > 0);
			SetDefElementSprite((Enum)UI.SPR_ELEM, item.GetElemDefType());
			SetLabelCompareParam((Enum)UI.LBL_DEF, item.def, def, -1);
			SetLabelCompareParam((Enum)UI.LBL_ELEM, item.elemDef, elemDef, -1);
		}
	}

	protected override void EquipParam()
	{
		EquipItemInfo item = EquipItem;
		EquipItemTable.EquipItemData equipItemData = (item == null) ? null : item.tableData;
		if (item == null || equipItemData == null)
		{
			return;
		}
		SetLabelText((Enum)UI.LBL_NAME, equipItemData.name);
		SetLabelEquipItemParam(item, GetCompareItemData());
		SetActive((Enum)UI.SPR_IS_EVOLVE, item.tableData.IsEvolve());
		SetSkillIconButton(UI.OBJ_SKILL_BUTTON_ROOT, "SkillIconButton", equipItemData, GetSkillSlotData(item));
		SetEquipmentTypeIcon((Enum)UI.SPR_TYPE_ICON, (Enum)UI.SPR_TYPE_ICON_BG, (Enum)UI.SPR_TYPE_ICON_RARITY, item.tableData);
		SetLabelText((Enum)UI.LBL_SELL, item.sellPrice.ToString());
		if (item.ability != null && item.ability.Length > 0)
		{
			bool empty_ability = true;
			SetTable(UI.TBL_ABILITY, "ItemDetailEquipAbilityItem", item.ability.Length, reset: false, delegate(int i, Transform t, bool is_recycle)
			{
				EquipItemAbility equipItemAbility = item.ability[i];
				if (equipItemAbility.id == 0)
				{
					SetActive(t, is_visible: false);
				}
				else
				{
					empty_ability = false;
					SetActive(t, is_visible: true);
					if (item.IsFixedAbility(i))
					{
						SetActive(t, UI.OBJ_ABILITY, is_visible: false);
						SetActive(t, UI.OBJ_FIXEDABILITY, is_visible: true);
						SetLabelText(t, UI.LBL_FIXEDABILITY, equipItemAbility.GetName());
						SetLabelText(t, UI.LBL_FIXEDABILITY_NUM, equipItemAbility.GetAP());
					}
					else
					{
						SetLabelText(t, UI.LBL_ABILITY, equipItemAbility.GetName());
						SetLabelText(t, UI.LBL_ABILITY_NUM, equipItemAbility.GetAP());
					}
					SetEvent(t, "ABILITY", i);
				}
			});
			if (empty_ability)
			{
				SetActive((Enum)UI.STR_NON_ABILITY, is_visible: true);
			}
			else
			{
				SetActive((Enum)UI.STR_NON_ABILITY, is_visible: false);
			}
		}
		else
		{
			SetActive((Enum)UI.STR_NON_ABILITY, is_visible: true);
		}
	}

	protected override void LocalInventory()
	{
		SetupEnableInventoryUI();
		if (localInventoryEquipData != null)
		{
			SetLabelText((Enum)UI.LBL_SORT, sortSettings.GetSortLabel());
			m_generatedIconList.Clear();
			UpdateNewIconInfo();
			bool initItem = false;
			SetDynamicList((Enum)InventoryUI, (string)null, localInventoryEquipData.Length, reset: false, (Func<int, bool>)delegate(int i)
			{
				SortCompareData sortCompareData = localInventoryEquipData[i];
				if (sortCompareData == null || !sortCompareData.IsPriority(sortSettings.orderTypeAsc))
				{
					return false;
				}
				return true;
			}, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycle)
			{
				initItem = true;
				uint tableID = localInventoryEquipData[i].GetTableID();
				if (tableID == 0)
				{
					SetActive(t, is_visible: false);
				}
				else
				{
					SetActive(t, is_visible: true);
					EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(tableID);
					EquipItemSortData equipItemSortData = localInventoryEquipData[i] as EquipItemSortData;
					EquipItemInfo equip = equipItemSortData.GetItemData() as EquipItemInfo;
					ITEM_ICON_TYPE iconType = equipItemSortData.GetIconType();
					bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iconType, equipItemSortData.GetUniqID());
					SkillSlotUIData[] skillSlotData = GetSkillSlotData(equip);
					ItemIcon itemIcon = CreateItemIconDetail(equipItemSortData, skillSlotData, IsShowMainStatus, t, "TRY_ON", i, equipItemSortData.GetIconStatus(), is_new);
					itemIcon.SetItemID(equipItemSortData.GetTableID());
					itemIcon.SetButtonColor(localInventoryEquipData[i].IsPriority(sortSettings.orderTypeAsc), is_instant: true);
					SetLongTouch(itemIcon.transform, "DETAIL", i);
					itemIcon.SetInitData(equipItemSortData);
					if (!m_generatedIconList.Contains(itemIcon))
					{
						m_generatedIconList.Add(itemIcon);
					}
				}
			});
			SetActive(base._transform, UI.LBL_NO_ITEM, !initItem);
			SetLabelText(base._transform, UI.LBL_NO_ITEM, StringTable.Get(STRING_CATEGORY.COMMON, 19799u));
		}
		else
		{
			SetActive(base._transform, UI.LBL_NO_ITEM, is_visible: true);
			SetLabelText(base._transform, UI.LBL_NO_ITEM, StringTable.Get(STRING_CATEGORY.COMMON, 19799u));
		}
	}

	protected void OnQuery_SORT()
	{
		GameSection.SetEventData(sortSettings.Clone());
	}

	protected void OnCloseSortDialog()
	{
		SortSettings sortSettings = (SortSettings)GameSection.GetEventData();
		if (sortSettings != null)
		{
			this.sortSettings = sortSettings;
			if (localInventoryEquipData != null && sorting())
			{
				selectInventoryIndex = GetSelectItemIndex();
				SetDirty(UI.GRD_INVENTORY);
				SetDirty(UI.GRD_INVENTORY_SMALL);
				SetDirty(UI.GRD_INVENTORY_DEF);
				SetDirty(UI.GRD_INVENTORY_SMALL_DEF);
				RefreshUI();
			}
		}
	}

	protected virtual void OnQuery_TRY_ON()
	{
		EquipParam();
	}

	protected virtual void OnQuery_SELECT_ITEM()
	{
	}

	protected void OnQuery_DETAIL()
	{
		OnQueryDetail();
	}

	protected virtual void OnQuery_SKILL_ICON_BUTTON()
	{
		ItemDetailEquip.CURRENT_SECTION cURRENT_SECTION;
		object obj;
		if (smithType == SmithType.GROW || smithType == SmithType.EVOLVE || smithType == SmithType.ABILITY_CHANGE || smithType == SmithType.REVERT_LITHOGRAPH)
		{
			cURRENT_SECTION = ItemDetailEquip.CURRENT_SECTION.SMITH_GROW;
			obj = EquipItem;
		}
		else
		{
			cURRENT_SECTION = ItemDetailEquip.CURRENT_SECTION.SMITH_CREATE;
			obj = GetEquipTableData();
		}
		GameSection.SetEventData(new object[2]
		{
			cURRENT_SECTION,
			obj
		});
	}

	protected virtual void OnQueryDetail()
	{
		GameSection.SetEventData(new object[2]
		{
			ItemDetailEquip.CURRENT_SECTION.SMITH_GROW,
			EquipItem
		});
	}

	private void OnQuery_ABILITY()
	{
		int num = (int)GameSection.GetEventData();
		EquipItemAbility equipItemAbility = null;
		if (smithType == SmithType.GROW || smithType == SmithType.EVOLVE || smithType == SmithType.ABILITY_CHANGE || smithType == SmithType.REVERT_LITHOGRAPH)
		{
			EquipItemInfo equipItem = EquipItem;
			equipItemAbility = new EquipItemAbility(equipItem.ability[num].id, 0);
		}
		else
		{
			EquipItemTable.EquipItemData equipTableData = GetEquipTableData();
			equipItemAbility = new EquipItemAbility((uint)equipTableData.fixedAbility[num].id, 0);
		}
		if (equipItemAbility == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.SetEventData(equipItemAbility);
		}
	}

	protected virtual void SelectingInventoryFirst()
	{
		selectInventoryIndex = 0;
	}

	protected virtual bool sorting()
	{
		return false;
	}

	protected virtual int GetSelectItemIndex()
	{
		return -1;
	}

	protected void SetupEnableInventoryUI()
	{
		int i = 0;
		for (int num = switchInventoryAry.Length; i < num; i++)
		{
			SetActive((Enum)switchInventoryAry[i], is_visible: false);
		}
		SetActive((Enum)switchInventoryAry[inventoryUIIndex], is_visible: true);
		InventoryUI = switchInventoryAry[inventoryUIIndex];
		SetToggle((Enum)UI.TGL_CHANGE_INVENTORY, InventoryUI == UI.GRD_INVENTORY || InventoryUI == UI.GRD_INVENTORY_DEF);
	}

	protected virtual void OnQuery_CHANGE_INVENTORY()
	{
		inventoryUIIndex = ((inventoryUIIndex + 1 < switchInventoryAry.Length) ? (inventoryUIIndex + 1) : 0);
		SetDirty(UI.GRD_INVENTORY);
		SetDirty(UI.GRD_INVENTORY_SMALL);
		SetDirty(UI.GRD_INVENTORY_DEF);
		SetDirty(UI.GRD_INVENTORY_SMALL_DEF);
		RefreshUI();
	}

	protected virtual ItemIcon CreateRemoveIcon(Transform parent = null, string event_name = null, int event_data = 0, int toggle_group = -1, bool is_select = false, string name = null)
	{
		if (InventoryUI == UI.GRD_INVENTORY || InventoryUI == UI.GRD_INVENTORY_DEF)
		{
			return ItemIconDetail.CreateRemoveButton(parent, event_name, event_data, toggle_group, is_select, name);
		}
		return ItemIconDetailSmall.CreateSmallRemoveButton(parent, event_name, event_data, toggle_group, is_select, name);
	}

	protected ItemIcon CreateItemIconDetail(EquipItemSortData item_data, SkillSlotUIData[] skill_slot_data, bool is_show_main_status, Transform parent = null, string event_name = null, int event_data = 0, ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE, bool is_new = false, int toggle_group = -1, bool is_select = false, int equip_index = -1)
	{
		if (InventoryUI == UI.GRD_INVENTORY || InventoryUI == UI.GRD_INVENTORY_DEF)
		{
			return ItemIconDetail.CreateEquipDetailIcon(item_data, skill_slot_data, is_show_main_status, parent, event_name, event_data, icon_status, is_new, toggle_group, is_select, equip_index);
		}
		return ItemIconDetailSmall.CreateSmallEquipDetailIcon(item_data, parent, event_name, event_data, icon_status, is_new, toggle_group, is_select, equip_index);
	}

	protected ItemIcon CreateSmithCreateItemIconDetail(ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity, SmithCreateSortData item_data, SkillSlotUIData[] skill_slot_data, bool is_show_main_status, Transform parent = null, string event_name = null, int event_data = 0, ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE, bool is_new = false, int toggle_group = -1, bool is_select = false, GET_TYPE getType = GET_TYPE.PAY)
	{
		bool registedIcon = MonoBehaviourSingleton<AchievementManager>.I.CheckEquipItemCollection(item_data.createData.equipTableData);
		if (InventoryUI == UI.GRD_INVENTORY)
		{
			ItemIcon itemIcon = ItemIconDetail.CreateSmithCreateEquipDetailIcon(icon_type, icon_id, rarity, item_data, skill_slot_data, is_show_main_status, parent, event_name, event_data, icon_status, is_new, toggle_group, is_select, is_equipping: false, getType);
			ItemIconDetail itemIconDetail = itemIcon as ItemIconDetail;
			if (itemIconDetail != null)
			{
				itemIconDetail.setupperEquip.SetRegistedIcon(registedIcon);
			}
			return itemIcon;
		}
		ItemIcon itemIcon2 = ItemIconDetailSmall.CreateSmithCreateEquipDetailIcon(icon_type, icon_id, rarity, item_data, parent, event_name, event_data, icon_status, is_new, toggle_group, is_select, is_equipping: false, getType);
		ItemIconDetailSmall itemIconDetailSmall = itemIcon2 as ItemIconDetailSmall;
		if (itemIconDetailSmall != null)
		{
			itemIconDetailSmall.SetRegistedIcon(registedIcon);
		}
		return itemIcon2;
	}
}
