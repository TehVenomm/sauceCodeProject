using System;
using UnityEngine;

public class SmithRevertLithographSelect : SmithEquipSelectBase
{
	protected override string prefabSuffix => "Lithograph";

	public override void Initialize()
	{
		smithType = SmithType.REVERT_LITHOGRAPH;
		switchInventoryAry = new EquipSelectBase.UI[2]
		{
			EquipSelectBase.UI.GRD_INVENTORY,
			EquipSelectBase.UI.GRD_INVENTORY
		};
		GameSection.SetEventData(EQUIPMENT_TYPE.ONE_HAND_SWORD);
		base.Initialize();
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
		SetActive(GetCtrl(uiTypeTab[weaponPickupIndex]).get_parent(), is_visible: false);
		SetActive(GetCtrl(uiTypeTab[armorPickupIndex]).get_parent(), is_visible: false);
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
		localInventoryEquipData = sortSettings.CreateSortAry<EquipItemInfo, EquipItemSortData>(MonoBehaviourSingleton<SmithManager>.I.localInventoryEquipData as EquipItemInfo[]);
	}

	protected override void LocalInventory()
	{
		SetupEnableInventoryUI();
		if (localInventoryEquipData != null)
		{
			SetLabelText((Enum)UI.LBL_SORT, sortSettings.GetSortLabel());
			m_generatedIconList.Clear();
			UpdateNewIconInfo();
			SetDynamicList((Enum)InventoryUI, (string)null, localInventoryEquipData.Length, reset: false, (Func<int, bool>)delegate(int i)
			{
				SortCompareData sortCompareData = localInventoryEquipData[i];
				if (sortCompareData == null || !sortCompareData.IsPriority(sortSettings.orderTypeAsc))
				{
					return false;
				}
				uint tableID = localInventoryEquipData[i].GetTableID();
				EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(tableID);
				if (!equipItemData.IsRevertable())
				{
					return false;
				}
				return true;
			}, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycle)
			{
				if (localInventoryEquipData[i].GetTableID() == 0)
				{
					SetActive(t, is_visible: false);
				}
				else
				{
					SetActive(t, is_visible: true);
					EquipItemSortData equipItemSortData = localInventoryEquipData[i] as EquipItemSortData;
					EquipItemInfo equip = equipItemSortData.GetItemData() as EquipItemInfo;
					ITEM_ICON_TYPE iconType = equipItemSortData.GetIconType();
					bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(iconType, equipItemSortData.GetUniqID());
					SkillSlotUIData[] skillSlotData = GetSkillSlotData(equip);
					ItemIcon itemIcon = CreateItemIconDetail(equipItemSortData, skillSlotData, base.IsShowMainStatus, t, "SELECT_ITEM", i, ItemIconDetail.ICON_STATUS.NONE, is_new);
					itemIcon.SetItemID(equipItemSortData.GetTableID());
					itemIcon.SetButtonColor(localInventoryEquipData[i].IsPriority(sortSettings.orderTypeAsc), is_instant: true);
					itemIcon.SetGrayout(IsRequiredIconGrayOut(equipItemSortData));
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
		}
	}

	protected override bool sorting()
	{
		InitLocalInventory();
		return true;
	}

	protected virtual bool IsRequiredIconGrayOut(SortCompareData _data)
	{
		if (_data.IsFavorite())
		{
			return true;
		}
		if (_data.IsEquipping())
		{
			return true;
		}
		return false;
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

	protected override void OnQuery_SELECT_ITEM()
	{
		if (localInventoryEquipData == null || localInventoryEquipData.Length == 0)
		{
			GameSection.StopEvent();
			return;
		}
		int num = (int)GameSection.GetEventData();
		EquipItemSortData equipItemSortData = localInventoryEquipData[num] as EquipItemSortData;
		if (!equipItemSortData.CanSale())
		{
			if (equipItemSortData.IsFavorite())
			{
				string event_data = StringTable.Get(STRING_CATEGORY.SMITH, 14u);
				GameSection.ChangeEvent("NOT_REVERT_FAVORITE", event_data);
			}
			else if (equipItemSortData.IsHomeEquipping())
			{
				GameSection.ChangeEvent("NOT_REVERT_EQUIPPING");
			}
			else
			{
				GameSection.ChangeEvent("NOT_REVERT_UNIQUE_EQUIPPING");
			}
		}
		else
		{
			uint tableID = equipItemSortData.GetTableID();
			EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(tableID);
			if (!equipItemData.IsRevertable())
			{
				GameSection.StopEvent();
			}
			else
			{
				GameSection.SetEventData(equipItemSortData);
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
			return;
		}
		ulong uniqID = sortCompareData.GetUniqID();
		if (uniqID != 0)
		{
			SmithManager.SmithGrowData smithGrowData = MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<SmithManager.SmithGrowData>();
			smithGrowData.selectEquipData = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(uniqID);
		}
		base.OnQueryDetail();
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.UPDATE_ITEM_INVENTORY) != (NOTIFY_FLAG)0L || (flags & NOTIFY_FLAG.UPDATE_EQUIP_INVENTORY) != (NOTIFY_FLAG)0L)
		{
			SetDirty(InventoryUI);
			InitLocalInventory();
		}
		base.OnNotify(flags);
	}
}
