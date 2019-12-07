using System.Collections.Generic;
using UnityEngine;

public class SmithAbilityChangeSelect : SmithEquipSelectBase
{
	protected override string prefabSuffix => "Ability";

	public override void Initialize()
	{
		smithType = SmithType.ABILITY_CHANGE;
		switchInventoryAry = new EquipSelectBase.UI[2]
		{
			EquipSelectBase.UI.GRD_INVENTORY,
			EquipSelectBase.UI.GRD_INVENTORY
		};
		GameSection.SetEventData(EQUIPMENT_TYPE.ONE_HAND_SWORD);
		base.Initialize();
		inventoryUIIndex = 1;
		GameSection.SetEventData(null);
		string text = base.sectionData.GetText("CAPTION");
		InitializeCaption(text);
	}

	public override void InitializeReopen()
	{
		InitLocalInventory();
		base.InitializeReopen();
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_USER_STATUS | NOTIFY_FLAG.UPDATE_EQUIP_GROW | NOTIFY_FLAG.UPDATE_EQUIP_EVOLVE | NOTIFY_FLAG.UPDATE_EQUIP_FAVORITE | NOTIFY_FLAG.UPDATE_SKILL_CHANGE | NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | NOTIFY_FLAG.UPDATE_EQUIP_INVENTORY | NOTIFY_FLAG.UPDATE_SKILL_INVENTORY | NOTIFY_FLAG.UPDATE_EQUIP_ABILITY;
	}

	public override void UpdateUI()
	{
		SetActive(GetCtrl(uiTypeTab[weaponPickupIndex]).parent, is_visible: false);
		SetActive(GetCtrl(uiTypeTab[armorPickupIndex]).parent, is_visible: false);
		base.UpdateUI();
	}

	protected override void InitSort()
	{
		SortBase.DIALOG_TYPE dialog_type = (!MonoBehaviourSingleton<InventoryManager>.I.IsWeaponInventoryType(base.selectInventoryType)) ? SortBase.DIALOG_TYPE.ARMOR : SortBase.DIALOG_TYPE.WEAPON;
		sortSettings = SortSettings.CreateMemSortSettings(dialog_type, SortSettings.SETTINGS_TYPE.EQUIP_ITEM);
	}

	protected override void InitLocalInventory()
	{
		MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = base.selectInventoryType;
		MonoBehaviourSingleton<SmithManager>.I.CreateLocalInventory();
		selectInventoryIndex = -1;
		EquipItemInfo[] array = MonoBehaviourSingleton<SmithManager>.I.localInventoryEquipData as EquipItemInfo[];
		List<EquipItemInfo> list = new List<EquipItemInfo>(array.Length);
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			EquipItemInfo equipItemInfo = array[i];
			if (equipItemInfo.GetValidLotAbility() > 0 || equipItemInfo.tableData.IsShadow())
			{
				list.Add(equipItemInfo);
			}
		}
		SortCompareData[] array2 = localInventoryEquipData = sortSettings.CreateSortAry<EquipItemInfo, EquipItemSortData>(list.ToArray());
	}

	protected override void LocalInventory()
	{
		SetupEnableInventoryUI();
		if (localInventoryEquipData != null)
		{
			SetLabelText(UI.LBL_SORT, sortSettings.GetSortLabel());
			m_generatedIconList.Clear();
			UpdateNewIconInfo();
			bool initItem = false;
			SetDynamicList(InventoryUI, null, localInventoryEquipData.Length, reset: false, delegate(int i)
			{
				SortCompareData sortCompareData = localInventoryEquipData[i];
				return (sortCompareData != null && sortCompareData.IsPriority(sortSettings.orderTypeAsc)) ? true : false;
			}, null, delegate(int i, Transform t, bool is_recycle)
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
					ItemIcon itemIcon = CreateEquipAbilityIconDetail(iconType, equipItemData.GetIconID(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex), equipItemData.rarity, equipItemSortData, skillSlotData, base.IsShowMainStatus, t, "TRY_ON", i, equipItemSortData.GetIconStatus(), is_new, -1, is_select: false, -1, equipItemSortData.GetGetType());
					itemIcon.SetItemID(equipItemSortData.GetTableID());
					itemIcon.SetButtonColor(localInventoryEquipData[i].IsPriority(sortSettings.orderTypeAsc), is_instant: true);
					SetLongTouch(itemIcon.transform, "DETAIL", i);
					if (itemIcon != null && equipItemSortData != null)
					{
						itemIcon.SetInitData(equipItemSortData);
					}
					if (itemIcon != null && !m_generatedIconList.Contains(itemIcon))
					{
						m_generatedIconList.Add(itemIcon);
					}
				}
			});
			SetActive(GetCtrl(UI.OBJ_ROOT), UI.LBL_NO_ITEM, !initItem);
			SetLabelText(GetCtrl(UI.OBJ_ROOT), UI.LBL_NO_ITEM, StringTable.Get(STRING_CATEGORY.COMMON, 19799u));
		}
		else
		{
			SetActive(GetCtrl(UI.OBJ_ROOT), UI.LBL_NO_ITEM, is_visible: true);
			SetLabelText(GetCtrl(UI.OBJ_ROOT), UI.LBL_NO_ITEM, StringTable.Get(STRING_CATEGORY.COMMON, 19799u));
		}
	}

	protected ItemIcon CreateEquipAbilityIconDetail(ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity, EquipItemSortData item_data, SkillSlotUIData[] skill_slot_data, bool is_show_main_status, Transform parent = null, string event_name = null, int event_data = 0, ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE, bool is_new = false, int toggle_group = -1, bool is_select = false, int equip_index = -1, GET_TYPE getType = GET_TYPE.PAY)
	{
		return ItemIconDetail.CreateEquipAbilityIcon(icon_type, icon_id, rarity, item_data, skill_slot_data, is_show_main_status, parent, event_name, event_data, icon_status, is_new, toggle_group, is_select, equip_index, getType);
	}

	protected override bool sorting()
	{
		InitLocalInventory();
		return true;
	}

	protected override void SelectingInventoryFirst()
	{
		if (localInventoryEquipData != null && localInventoryEquipData.Length != 0 && localInventoryEquipData[0] != null)
		{
			SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
			if (smithData != null)
			{
				smithData.selectEquipData = (localInventoryEquipData[0].GetItemData() as EquipItemInfo);
				selectInventoryIndex = 0;
			}
		}
	}

	protected override int GetSelectItemIndex()
	{
		if (localInventoryEquipData == null || localInventoryEquipData.Length == 0)
		{
			return -1;
		}
		EquipItemInfo equipData = GetEquipData();
		if (equipData == null)
		{
			return -2;
		}
		int i = 0;
		for (int num = localInventoryEquipData.Length; i < num; i++)
		{
			if (equipData.uniqueID == localInventoryEquipData[i].GetUniqID())
			{
				return i;
			}
		}
		return -1;
	}

	protected void OnCloseDialog_SmithEquipChangeSort()
	{
		OnCloseSortDialog();
	}

	protected override void OnQuery_TRY_ON()
	{
		if (localInventoryEquipData == null || localInventoryEquipData.Length == 0)
		{
			return;
		}
		selectInventoryIndex = (int)GameSection.GetEventData();
		SortCompareData sortCompareData = localInventoryEquipData[selectInventoryIndex];
		if (sortCompareData != null)
		{
			ulong uniqID = sortCompareData.GetUniqID();
			if (uniqID != 0L)
			{
				MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<SmithManager.SmithGrowData>().selectEquipData = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(uniqID);
				GameSection.ChangeEvent("SELECT_ITEM");
				OnQuery_SELECT_ITEM();
			}
		}
	}

	protected override void OnQuery_SELECT_ITEM()
	{
		if (localInventoryEquipData == null || localInventoryEquipData.Length == 0)
		{
			GameSection.StopEvent();
		}
		else if (MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>() == null)
		{
			GameSection.StopEvent();
		}
	}

	protected override void OnQueryDetail()
	{
		int num = (int)GameSection.GetEventData();
		SortCompareData sortCompareData = localInventoryEquipData[num];
		if (sortCompareData == null)
		{
			GameSection.StopEvent();
			return;
		}
		ulong uniqID = sortCompareData.GetUniqID();
		if (uniqID != 0L)
		{
			MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<SmithManager.SmithGrowData>().selectEquipData = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(uniqID);
		}
		base.OnQueryDetail();
	}
}
