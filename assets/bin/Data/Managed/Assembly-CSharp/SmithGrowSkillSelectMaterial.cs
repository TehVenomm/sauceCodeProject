using System;
using System.Collections.Generic;
using UnityEngine;

public class SmithGrowSkillSelectMaterial : GameSection
{
	protected enum UI
	{
		STR_NON_MATERIAL,
		LBL_EQUIP_ITEM_NAME,
		SCR_INVENTORY,
		GRD_INVENTORY,
		GRD_INVENTORY_SMALL,
		TGL_CHANGE_INVENTORY,
		BTN_CHANGE_INVENTORY,
		LBL_SORT,
		TGL_ICON_ASC,
		LBL_SELECT_NUM,
		STR_TITLE_MATERIAL,
		STR_TITLE_MONEY,
		OBJ_GOLD,
		LBL_GOLD,
		LBL_LV_NOW,
		LBL_LV_MAX,
		OBJ_LV_EX,
		LBL_LV_EX,
		OBJ_NEXT_EXP_ROOT,
		PRG_EXP_BAR,
		STR_EXCEED_CAUTION
	}

	public int MATERIAL_SELECT_MAX = 10;

	protected List<ItemIcon> m_generatedIconList = new List<ItemIcon>();

	protected List<SortCompareData> m_newIconUpdateTargetList = new List<SortCompareData>();

	private SkillItemInfo skillItem;

	protected ItemStorageTop.SkillItemInventory inventory;

	private List<SkillItemInfo> materialSkillItem;

	protected UI inventoryUI;

	protected UI[] switchInventoryAry = new UI[2]
	{
		UI.GRD_INVENTORY,
		UI.GRD_INVENTORY_SMALL
	};

	protected int inventoryUIIndex;

	private Color goldColor = Color.white;

	private bool isSelectMax;

	private bool isExceed;

	private bool isSortTypeReset;

	private Comparison<SortCompareData> m_defaultComparison;

	protected bool IsShowMainStatus => inventoryUIIndex == 0;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		skillItem = (array[0] as SkillItemInfo);
		SkillItemInfo[] array2 = array[1] as SkillItemInfo[];
		isExceed = (bool)array[2];
		isSortTypeReset = (bool)array[3];
		GameSection.SetEventData(new object[2]
		{
			ItemDetailEquip.CURRENT_SECTION.UI_PARTS,
			skillItem
		});
		SetActive(UI.BTN_CHANGE_INVENTORY, is_visible: false);
		materialSkillItem = new List<SkillItemInfo>();
		if (array2 != null)
		{
			int i = 0;
			for (int num = array2.Length; i < num; i++)
			{
				materialSkillItem.Add(array2[i]);
			}
		}
		if (materialSkillItem.Count == MATERIAL_SELECT_MAX)
		{
			isSelectMax = true;
		}
		UILabel component = GetComponent<UILabel>(UI.LBL_GOLD);
		if (component != null)
		{
			goldColor = component.color;
		}
		MATERIAL_SELECT_MAX = (isExceed ? 10 : 10);
		base.Initialize();
		UIScrollView componentInChildren = GetCtrl(UI.SCR_INVENTORY).GetComponentInChildren<UIScrollView>();
		componentInChildren.onDragFinished = OnReposition;
		componentInChildren.onStoppedMoving = OnReposition;
	}

	protected override void OnClose()
	{
		UpdateNewIconInfo();
		base.OnClose();
	}

	protected virtual void Update()
	{
		ObserveItemList();
	}

	public void OnReposition()
	{
		Transform ctrl = GetCtrl(inventoryUI);
		ItemIcon[] icons = ctrl.GetComponentsInChildren<ItemIcon>();
		materialSkillItem.ForEach(delegate(SkillItemInfo material)
		{
			ItemIcon itemIcon = Array.Find(icons, (ItemIcon _icon) => _icon.GetUniqID == material.uniqueID);
			if (itemIcon != null)
			{
				IconSelect(itemIcon, is_select: true);
			}
		});
	}

	public override void UpdateUI()
	{
		SetFontStyle(UI.STR_TITLE_MATERIAL, FontStyle.Italic);
		SetFontStyle(UI.STR_TITLE_MONEY, FontStyle.Italic);
		SetActive(UI.STR_EXCEED_CAUTION, isExceed);
		SetActive(UI.OBJ_GOLD, !isExceed);
		if (!isExceed)
		{
			UpdateNeedGold();
		}
		UpdateLvExp();
		if (inventory == null)
		{
			InitInventory();
		}
		bool is_visible = inventory == null || inventory.datas == null || inventory.datas.Length <= 1;
		SetActive(UI.STR_NON_MATERIAL, is_visible);
		SetupEnableInventoryUI();
		UpdateInventory();
		UpdateSelectMaterialIcon();
		SetLabelText(UI.LBL_SORT, inventory.sortSettings.GetSortLabel());
		SetToggle(UI.TGL_ICON_ASC, inventory.sortSettings.orderTypeAsc);
	}

	private void UpdateInventory()
	{
		m_generatedIconList.Clear();
		UpdateNewIconInfo();
		int base_item_index = Array.FindIndex(inventory.datas, (SortCompareData data) => data.GetUniqID() == skillItem.uniqueID);
		SetDynamicList(inventoryUI, null, inventory.datas.Length, reset: false, delegate(int i)
		{
			if (i == base_item_index)
			{
				return false;
			}
			SkillItemSortData skillItemSortData = inventory.datas[i] as SkillItemSortData;
			if (skillItemSortData == null || !skillItemSortData.IsPriority(inventory.sortSettings.orderTypeAsc))
			{
				return false;
			}
			return !isExceed || ((skillItemSortData.skillData.tableData.type == skillItem.tableData.type || skillItemSortData.skillData.tableData.type == SKILL_SLOT_TYPE.PASSIVE) ? true : false);
		}, null, delegate(int i, Transform t, bool is_recycle)
		{
			SkillItemSortData item = inventory.datas[i] as SkillItemSortData;
			int num = materialSkillItem.FindIndex((SkillItemInfo material) => material.uniqueID == item.GetUniqID());
			if (num > -1)
			{
				num++;
			}
			ITEM_ICON_TYPE iconType = item.GetIconType();
			bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iconType, item.GetUniqID());
			ItemIcon itemIcon = CreateItemIconDetail(iconType, item.skillData.tableData.iconID, item.skillData.tableData.rarity, item, IsShowMainStatus, t, "MATERIAL", i, is_new, -1, num, item.IsEquipping(), isSelectMax);
			itemIcon.SetUniqID(item.GetUniqID());
			SetLongTouch(itemIcon.transform, "DETAIL", i);
			if (itemIcon != null && item != null)
			{
				itemIcon.SetInitData(item);
			}
			if (!m_generatedIconList.Contains(itemIcon))
			{
				m_generatedIconList.Add(itemIcon);
			}
		});
	}

	private void InitInventory()
	{
		inventory = new ItemStorageTop.SkillItemInventory(isExceed ? SortSettings.SETTINGS_TYPE.EXCEED_SKILL_ITEM : SortSettings.SETTINGS_TYPE.GROW_SKILL_ITEM, SKILL_SLOT_TYPE.NONE, isAddMaterial: true);
		if (isSortTypeReset)
		{
			inventory.sortSettings.ResetType();
		}
		sorting();
	}

	private SkillItemInfo ParamCopy(SkillItemInfo _ref, bool isLevelUp = false, bool isExceedUp = false)
	{
		return SmithGrowSkillSecond.ParamCopy(_ref, isLevelUp, isExceedUp);
	}

	protected bool sorting()
	{
		inventory.sortSettings.indivComparison = CustomCompare;
		m_defaultComparison = new SortComparison(inventory.sortSettings.orderTypeAsc).comparison;
		return inventory.sortSettings.Sort(inventory.datas as SkillItemSortData[]);
	}

	private void OnQuery_SORT()
	{
		GameSection.SetEventData(new object[3]
		{
			skillItem,
			inventory.sortSettings.Clone(),
			isExceed
		});
	}

	private void OnCloseDialog_SmithSkillGrowSort()
	{
		SortSettings sortSettings = (SortSettings)GameSection.GetEventData();
		if (sortSettings != null)
		{
			inventory.sortSettings.indivComparison = CustomCompare;
			m_defaultComparison = new SortComparison(inventory.sortSettings.orderTypeAsc).comparison;
			if (inventory.Sort(sortSettings))
			{
				SetDirty(UI.GRD_INVENTORY);
				SetDirty(UI.GRD_INVENTORY_SMALL);
				RefreshUI();
			}
		}
	}

	private bool IsEnableSelect(SortCompareData item)
	{
		if (item == null)
		{
			return false;
		}
		if (!item.IsFavorite())
		{
			return item.GetUniqID() != skillItem.uniqueID;
		}
		return false;
	}

	private void OnQuery_MATERIAL()
	{
		int num = (int)GameSection.GetEventData();
		SkillItemSortData item = inventory.datas[num] as SkillItemSortData;
		bool flag = materialSkillItem.Find((SkillItemInfo material) => material.uniqueID == item.GetUniqID()) != null;
		SkillItemInfo item2 = item.GetItemData() as SkillItemInfo;
		if (!IsEnableSelect(inventory.datas[num]))
		{
			if (item.IsFavorite())
			{
				GameSection.ChangeEvent("NOT_MATERIAL_FAVORITE");
			}
		}
		else if (flag)
		{
			materialSkillItem.Remove(item2);
		}
		else if (materialSkillItem.Count < MATERIAL_SELECT_MAX)
		{
			materialSkillItem.Add(item2);
		}
		bool num2 = isSelectMax;
		isSelectMax = (materialSkillItem.Count == MATERIAL_SELECT_MAX);
		if (num2 != isSelectMax)
		{
			UpdateInventory();
		}
		UpdateSelectMaterialIcon();
	}

	private void OnQuery_MATERIAL_NUM()
	{
		int num = (int)GameSection.GetEventData();
		SkillItemSortData skillItemSortData = inventory.datas[num] as SkillItemSortData;
		SkillItemInfo skillItemInfo = skillItemSortData.GetItemData() as SkillItemInfo;
		int num2 = 0;
		int i = 0;
		for (int count = materialSkillItem.Count; i < count; i++)
		{
			if (materialSkillItem[i].uniqueID == skillItemInfo.uniqueID)
			{
				num2++;
			}
		}
		GameSection.SetEventData(new object[3]
		{
			skillItemSortData,
			MATERIAL_SELECT_MAX - materialSkillItem.Count,
			num2
		});
	}

	private void OnCloseDialog_SmithGrowSkillSelectMaterialItemNum()
	{
		object[] obj = GameSection.GetEventData() as object[];
		SkillItemInfo skillItemInfo = (obj[0] as SkillItemSortData).GetItemData() as SkillItemInfo;
		int num = (int)obj[1];
		int num2 = 0;
		int i = 0;
		for (int count = materialSkillItem.Count; i < count; i++)
		{
			if (materialSkillItem[i].uniqueID == skillItemInfo.uniqueID)
			{
				num2++;
			}
		}
		int num3 = num - num2;
		if (num3 < 0)
		{
			for (int num4 = materialSkillItem.Count - 1; num4 >= 0; num4--)
			{
				if (materialSkillItem[num4].uniqueID == skillItemInfo.uniqueID)
				{
					materialSkillItem.RemoveAt(num4);
					num3++;
					if (num3 >= 0)
					{
						break;
					}
				}
			}
		}
		else if (num3 > 0)
		{
			for (int j = 0; j < num3; j++)
			{
				materialSkillItem.Add(skillItemInfo);
			}
		}
		bool num5 = isSelectMax;
		isSelectMax = (materialSkillItem.Count == MATERIAL_SELECT_MAX);
		if (num5 != isSelectMax)
		{
			UpdateInventory();
		}
		UpdateSelectMaterialIcon();
	}

	private void ResetSelectMaterialIcon()
	{
		_UpdateSelectMaterialIcon(reset: true);
	}

	private void UpdateSelectMaterialIcon()
	{
		_UpdateSelectMaterialIcon(reset: false);
	}

	private void _UpdateSelectMaterialIcon(bool reset)
	{
		Transform ctrl = GetCtrl(inventoryUI);
		ItemIcon[] icons = ctrl.GetComponentsInChildren<ItemIcon>();
		int i = 0;
		for (int num = icons.Length; i < num; i++)
		{
			if (inventoryUI == UI.GRD_INVENTORY)
			{
				(icons[i] as ItemIconDetail).setupperSkill.SetupSelectNumberSprite(-1);
			}
			else
			{
				(icons[i] as ItemIconDetailSmall).SetupSelectNumberSprite();
			}
			IconSelect(icons[i], is_select: false);
		}
		int index = (!reset) ? 1 : (-1);
		materialSkillItem.ForEach(delegate(SkillItemInfo material)
		{
			ItemIcon itemIcon = Array.Find(icons, (ItemIcon _icon) => _icon.GetUniqID == material.uniqueID);
			if (itemIcon != null)
			{
				if (inventoryUI == UI.GRD_INVENTORY)
				{
					ItemIconDetail itemIconDetail = itemIcon as ItemIconDetail;
					if (itemIconDetail != null)
					{
						if (itemIconDetail.iconType != ITEM_ICON_TYPE.SKILL_GROW)
						{
							itemIconDetail.setupperSkill.SetupSelectNumberSprite(index);
							IconSelect(itemIconDetail, is_select: true);
						}
						else
						{
							itemIconDetail.setupperSkill.SetupSelectNumberSprite(-1);
							IconSelect(itemIconDetail, is_select: true);
						}
					}
				}
				else
				{
					ItemIconDetailSmall itemIconDetailSmall = itemIcon as ItemIconDetailSmall;
					if (itemIconDetailSmall != null)
					{
						if (itemIconDetailSmall.iconType != ITEM_ICON_TYPE.SKILL_GROW)
						{
							itemIconDetailSmall.SetupSelectNumberSprite(index);
							IconSelect(itemIconDetailSmall, is_select: true);
						}
						else
						{
							itemIconDetailSmall.SetupSelectNumberSprite();
							IconSelect(itemIconDetailSmall, is_select: true);
						}
					}
				}
			}
			if (!reset)
			{
				index++;
			}
		});
		if (!isExceed)
		{
			UpdateNeedGold();
		}
		UpdateLvExp();
	}

	private void OnQuery_DECISION()
	{
		GameSection.SetEventData(new object[2]
		{
			skillItem,
			materialSkillItem.ToArray()
		});
	}

	private void OnQuery_CLEAR()
	{
		materialSkillItem.Clear();
		isSelectMax = false;
		SetDirty(UI.GRD_INVENTORY);
		SetDirty(UI.GRD_INVENTORY_SMALL);
		RefreshUI();
	}

	private void OnQuery_DETAIL()
	{
		int num = (int)GameSection.GetEventData();
		SkillItemSortData skillItemSortData = inventory.datas[num] as SkillItemSortData;
		if (skillItemSortData.skillData.tableData.type == SKILL_SLOT_TYPE.GROW)
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.SetEventData(new object[2]
			{
				ItemDetailEquip.CURRENT_SECTION.SMITH_SKILL_GROW,
				skillItemSortData
			});
		}
	}

	protected void OnQuery_CHANGE_INVENTORY()
	{
		inventoryUIIndex = ((inventoryUIIndex + 1 < switchInventoryAry.Length) ? (inventoryUIIndex + 1) : 0);
		SetDirty(UI.GRD_INVENTORY);
		SetDirty(UI.GRD_INVENTORY_SMALL);
		RefreshUI();
	}

	protected void SetupEnableInventoryUI()
	{
		int i = 0;
		for (int num = switchInventoryAry.Length; i < num; i++)
		{
			SetActive(switchInventoryAry[i], is_visible: false);
		}
		SetActive(switchInventoryAry[inventoryUIIndex], is_visible: true);
		inventoryUI = switchInventoryAry[inventoryUIIndex];
		SetToggle(UI.TGL_CHANGE_INVENTORY, inventoryUI == UI.GRD_INVENTORY);
	}

	protected ItemIcon CreateItemIconDetail(ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity, SkillItemSortData item_data, bool is_show_main_status, Transform parent = null, string event_name = null, int event_data = 0, bool is_new = false, int toggle_group = -1, int select_number = -1, bool is_equipping = false, bool is_select_max = false)
	{
		ItemIconDetail.ICON_STATUS iCON_STATUS = ItemIconDetail.ICON_STATUS.NONE;
		if (is_select_max && select_number == -1)
		{
			iCON_STATUS = ItemIconDetail.ICON_STATUS.GRAYOUT;
		}
		if (inventoryUI == UI.GRD_INVENTORY)
		{
			if (icon_type == ITEM_ICON_TYPE.SKILL_GROW)
			{
				ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(item_data.skillData.itemId);
				bool is_select = select_number != -1;
				ItemIcon itemIcon = ItemIconDetail.CreateMaterialIcon(icon_type, icon_id, rarity, itemData, is_show_main_status, parent, item_data.skillData.num, itemData.name, "MATERIAL_NUM", event_data, toggle_group, is_select);
				((ItemIconDetail)itemIcon).setupperSkill.GrayOut(iCON_STATUS);
				return itemIcon;
			}
			bool isSameSkillExceed = isExceed && skillItem.tableData.id == item_data.skillData.tableData.id;
			return ItemIconDetail.CreateSkillDetailSelectNumberIcon(icon_type, icon_id, rarity, item_data, is_show_main_status, parent, event_name, event_data, is_new, toggle_group, select_number, is_equipping, iCON_STATUS, isSameSkillExceed);
		}
		if (icon_type == ITEM_ICON_TYPE.SKILL_GROW)
		{
			ItemTable.ItemData itemData2 = Singleton<ItemTable>.I.GetItemData(item_data.skillData.itemId);
			bool is_select2 = select_number != -1;
			return ItemIconDetailSmall.CreateSmallMaterialIcon(icon_type, icon_id, rarity, parent, item_data.skillData.num, itemData2.name, "MATERIAL_NUM", event_data, toggle_group, is_select2, is_new, 0, 0, iCON_STATUS);
		}
		return ItemIconDetailSmall.CreateSmallSkillSelectDetailIcon(icon_type, icon_id, rarity, item_data, parent, event_name, event_data, is_new, toggle_group, select_number, is_equipping, iCON_STATUS);
	}

	private void UpdateLvExp()
	{
		SkillItemInfo[] array = materialSkillItem.ToArray();
		int num = (array != null) ? array.Length : 0;
		SetLabelText(UI.LBL_SELECT_NUM, (MATERIAL_SELECT_MAX - num).ToString());
		SkillItemInfo skillItemInfo = ParamCopy(skillItem);
		SkillItemInfo skillItemInfo2 = ParamCopy(skillItem);
		if (array != null)
		{
			int i = 0;
			for (int num2 = array.Length; i < num2; i++)
			{
				if (isExceed)
				{
					if (skillItemInfo2.IsMaxExceed())
					{
						continue;
					}
					if (skillItem.tableData.id == array[i].tableData.id)
					{
						skillItemInfo2.exceedExp += array[i].giveSameSkillExceedExp;
					}
					else
					{
						skillItemInfo2.exceedExp += array[i].giveExceedExp;
					}
					while (skillItemInfo2.exceedExpNext <= skillItemInfo2.exceedExp)
					{
						skillItemInfo2 = ParamCopy(skillItemInfo2, isLevelUp: false, isExceedUp: true);
						if (skillItemInfo2.IsMaxExceed())
						{
							skillItemInfo2.exceedExp = skillItemInfo2.expPrev;
							break;
						}
					}
				}
				else
				{
					if (skillItemInfo2.IsLevelMax() || array[i].level > array[i].GetMaxLevel())
					{
						continue;
					}
					skillItemInfo2.exp += array[i].giveExp;
					while (skillItemInfo2.expNext <= skillItemInfo2.exp)
					{
						skillItemInfo2 = ParamCopy(skillItemInfo2, isLevelUp: true);
						if (skillItemInfo2.IsLevelMax())
						{
							skillItemInfo2.exp = skillItemInfo2.expPrev;
							break;
						}
					}
				}
			}
		}
		SetLabelText(UI.LBL_LV_NOW, skillItemInfo2.level.ToString());
		SetLabelText(UI.LBL_LV_MAX, skillItemInfo2.GetMaxLevel().ToString());
		SetActive(UI.OBJ_LV_EX, skillItemInfo2.IsExceeded());
		SetLabelText(UI.LBL_LV_EX, skillItemInfo2.exceedCnt.ToString());
		SkillGrowProgress component = FindCtrl(base.transform, UI.PRG_EXP_BAR).GetComponent<SkillGrowProgress>();
		if (isExceed)
		{
			float fill_amount = (float)(skillItem.exceedExp - skillItem.exceedExpPrev) / (float)(skillItem.exceedExpNext - skillItem.exceedExpPrev);
			SetProgressInt(base.transform, UI.PRG_EXP_BAR, skillItemInfo2.exceedExp, skillItemInfo2.exceedExpPrev, skillItemInfo2.exceedExpNext);
			component.SetExceedMode();
			component.SetBaseGauge(skillItemInfo2.exceedCnt == skillItemInfo.exceedCnt, fill_amount);
		}
		else
		{
			float fill_amount2 = (float)(skillItem.exp - skillItem.expPrev) / (float)(skillItem.expNext - skillItem.expPrev);
			SetProgressInt(base.transform, UI.PRG_EXP_BAR, skillItemInfo2.exp, skillItemInfo2.expPrev, skillItemInfo2.expNext);
			component.SetGrowMode();
			component.SetBaseGauge(skillItemInfo2.level == skillItemInfo.level, fill_amount2);
		}
	}

	private void UpdateNeedGold()
	{
		int num = 0;
		if (materialSkillItem != null && skillItem != null)
		{
			num = (int)(skillItem.growCost * (float)materialSkillItem.Count);
		}
		SetLabelText(UI.LBL_GOLD, num.ToString("N0"));
		if (MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money < num)
		{
			SetColor(UI.LBL_GOLD, Color.red);
		}
		else
		{
			SetColor(UI.LBL_GOLD, goldColor);
		}
	}

	private void IconSelect(ItemIcon icon, bool is_select)
	{
		icon.selectFrame.gameObject.SetActive(is_select);
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & (NOTIFY_FLAG.UPDATE_SKILL_FAVORITE | NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | NOTIFY_FLAG.UPDATE_SKILL_INVENTORY)) != (NOTIFY_FLAG)0L)
		{
			inventory = new ItemStorageTop.SkillItemInventory(isExceed ? SortSettings.SETTINGS_TYPE.EXCEED_SKILL_ITEM : SortSettings.SETTINGS_TYPE.GROW_SKILL_ITEM, SKILL_SLOT_TYPE.NONE, isAddMaterial: true);
			sorting();
			skillItem = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.Find(skillItem.uniqueID);
			List<SkillItemInfo> del_list = new List<SkillItemInfo>();
			materialSkillItem.ForEach(delegate(SkillItemInfo skill)
			{
				SkillItemInfo skillItemInfo = MonoBehaviourSingleton<InventoryManager>.I.skillItemInventory.Find(skill.uniqueID);
				SkillItemInfo skillItemInfo2 = MonoBehaviourSingleton<InventoryManager>.I.skillMaterialInventory.Find(skill.uniqueID);
				if ((skillItemInfo == null && skillItemInfo2 == null) || (skillItemInfo != null && skillItemInfo.isFavorite))
				{
					del_list.Add(skill);
				}
			});
			del_list.ForEach(delegate(SkillItemInfo delitem)
			{
				materialSkillItem.Remove(delitem);
			});
			inventory = null;
			RefreshUI();
		}
		base.OnNotify(flags);
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_USER_STATUS | NOTIFY_FLAG.UPDATE_SKILL_FAVORITE | NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | NOTIFY_FLAG.UPDATE_SKILL_INVENTORY;
	}

	private int CustomCompare(SortCompareData lp, SortCompareData rp)
	{
		SkillItemSortData skillItemSortData = lp as SkillItemSortData;
		SkillItemSortData skillItemSortData2 = rp as SkillItemSortData;
		if (skillItemSortData == null || skillItemSortData2 == null)
		{
			return 0;
		}
		if (skillItemSortData.skillData == null || skillItemSortData.skillData.tableData == null || skillItemSortData2.skillData == null || skillItemSortData2.skillData.tableData == null)
		{
			return 0;
		}
		if (skillItemSortData.skillData.tableData.type == SKILL_SLOT_TYPE.GROW && skillItemSortData2.skillData.tableData.type == SKILL_SLOT_TYPE.GROW)
		{
			return m_defaultComparison(lp, rp);
		}
		if (skillItemSortData.skillData.tableData.type == SKILL_SLOT_TYPE.GROW)
		{
			return -1;
		}
		if (skillItemSortData2.skillData.tableData.type == SKILL_SLOT_TYPE.GROW)
		{
			return 1;
		}
		return m_defaultComparison(lp, rp);
	}

	protected void ObserveItemList()
	{
		if (m_generatedIconList != null && m_generatedIconList.Count >= 1)
		{
			int i = 0;
			for (int count = m_generatedIconList.Count; i < count; i++)
			{
				ItemIcon icon = m_generatedIconList[i];
				ObserveItemListNewIcon(icon);
			}
		}
	}

	protected void ObserveItemListNewIcon(ItemIcon _icon)
	{
		if (!(_icon == null) && _icon.InitData != null && _icon.IsVisbleNewIcon() && !m_newIconUpdateTargetList.Contains(_icon.InitData))
		{
			m_newIconUpdateTargetList.Add(_icon.InitData);
		}
	}

	protected void UpdateNewIconInfo()
	{
		if (m_newIconUpdateTargetList.Count < 1)
		{
			return;
		}
		GameSaveData instance = GameSaveData.instance;
		if (instance != null)
		{
			int i = 0;
			for (int count = m_newIconUpdateTargetList.Count; i < count; i++)
			{
				SortCompareData sortCompareData = m_newIconUpdateTargetList[i];
				instance.RemoveNewIconAndSave(sortCompareData.GetIconType(), sortCompareData.GetUniqID());
			}
			m_newIconUpdateTargetList.Clear();
		}
	}
}
