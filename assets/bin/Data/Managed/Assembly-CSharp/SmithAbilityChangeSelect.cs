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
		SetActive(GetCtrl(uiTypeTab[weaponPickupIndex]).parent, false);
		SetActive(GetCtrl(uiTypeTab[armorPickupIndex]).parent, false);
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
		localInventoryEquipData = sortSettings.CreateSortAry<EquipItemInfo, EquipItemSortData>(list.ToArray());
	}

	protected override void LocalInventory()
	{
		SetupEnableInventoryUI();
		if (localInventoryEquipData != null)
		{
			SetLabelText(UI.LBL_SORT, sortSettings.GetSortLabel());
			m_generatedIconList.Clear();
			UpdateNewIconInfo();
			SetDynamicList(InventoryUI, null, localInventoryEquipData.Length, false, delegate(int i)
			{
				SortCompareData sortCompareData = localInventoryEquipData[i];
				if (sortCompareData == null || !sortCompareData.IsPriority(sortSettings.orderTypeAsc))
				{
					return false;
				}
				return true;
			}, null, delegate(int i, Transform t, bool is_recycle)
			{
				uint tableID = localInventoryEquipData[i].GetTableID();
				if (tableID == 0)
				{
					SetActive(t, false);
				}
				else
				{
					SetActive(t, true);
					EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(tableID);
					EquipItemSortData equipItemSortData = localInventoryEquipData[i] as EquipItemSortData;
					EquipItemInfo equip = equipItemSortData.GetItemData() as EquipItemInfo;
					ITEM_ICON_TYPE iconType = equipItemSortData.GetIconType();
					bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iconType, equipItemSortData.GetUniqID());
					SkillSlotUIData[] skillSlotData = GetSkillSlotData(equip);
					ItemIcon itemIcon = CreateEquipAbilityIconDetail(getType: equipItemSortData.GetGetType(), icon_type: iconType, icon_id: equipItemData.GetIconID(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex), rarity: equipItemData.rarity, item_data: equipItemSortData, skill_slot_data: skillSlotData, is_show_main_status: base.IsShowMainStatus, parent: t, event_name: "TRY_ON", event_data: i, icon_status: equipItemSortData.GetIconStatus(), is_new: is_new, toggle_group: -1, is_select: false, equip_index: -1);
					itemIcon.SetItemID(equipItemSortData.GetTableID());
					itemIcon.SetButtonColor(localInventoryEquipData[i].IsPriority(sortSettings.orderTypeAsc), true);
					SetLongTouch(itemIcon.transform, "DETAIL", i);
					if ((Object)itemIcon != (Object)null && equipItemSortData != null)
					{
						itemIcon.SetInitData(equipItemSortData);
					}
					if ((Object)itemIcon != (Object)null && !m_generatedIconList.Contains(itemIcon))
					{
						m_generatedIconList.Add(itemIcon);
					}
				}
			});
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
		if (localInventoryEquipData != null && localInventoryEquipData.Length != 0)
		{
			selectInventoryIndex = (int)GameSection.GetEventData();
			SortCompareData sortCompareData = localInventoryEquipData[selectInventoryIndex];
			if (sortCompareData != null)
			{
				ulong uniqID = sortCompareData.GetUniqID();
				if (uniqID != 0L)
				{
					SmithManager.SmithGrowData smithGrowData = MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<SmithManager.SmithGrowData>();
					smithGrowData.selectEquipData = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(uniqID);
					GameSection.ChangeEvent("SELECT_ITEM", null);
					OnQuery_SELECT_ITEM();
				}
			}
		}
	}

	protected override void OnQuery_SELECT_ITEM()
	{
		if (localInventoryEquipData == null || localInventoryEquipData.Length == 0)
		{
			GameSection.StopEvent();
		}
		else
		{
			SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
			if (smithData == null)
			{
				GameSection.StopEvent();
			}
		}
	}

	protected override void OnQueryDetail()
	{
		int num = (int)GameSection.GetEventData();
		SortCompareData sortCompareData = localInventoryEquipData[num];
		if (sortCompareData == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			ulong uniqID = sortCompareData.GetUniqID();
			if (uniqID != 0L)
			{
				SmithManager.SmithGrowData smithGrowData = MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<SmithManager.SmithGrowData>();
				smithGrowData.selectEquipData = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(uniqID);
			}
			base.OnQueryDetail();
		}
	}
}
