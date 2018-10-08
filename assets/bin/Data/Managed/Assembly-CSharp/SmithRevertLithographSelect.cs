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
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		SetActive(GetCtrl(uiTypeTab[weaponPickupIndex]).get_parent(), false);
		SetActive(GetCtrl(uiTypeTab[armorPickupIndex]).get_parent(), false);
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

	protected unsafe override void LocalInventory()
	{
		SetupEnableInventoryUI();
		if (localInventoryEquipData != null)
		{
			SetLabelText((Enum)UI.LBL_SORT, sortSettings.GetSortLabel());
			m_generatedIconList.Clear();
			UpdateNewIconInfo();
			SetDynamicList((Enum)InventoryUI, (string)null, localInventoryEquipData.Length, false, new Func<int, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), null, new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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
		}
		else
		{
			int num = (int)GameSection.GetEventData();
			EquipItemSortData equipItemSortData = localInventoryEquipData[num] as EquipItemSortData;
			if (!equipItemSortData.CanSale())
			{
				if (equipItemSortData.IsFavorite())
				{
					string event_data = StringTable.Get(STRING_CATEGORY.SMITH, 14u);
					GameSection.ChangeEvent("NOT_REVERT_FAVORITE", event_data);
				}
				else
				{
					string event_data2 = StringTable.Get(STRING_CATEGORY.SMITH, 15u);
					GameSection.ChangeEvent("NOT_REVERT_EQUIPPING", event_data2);
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
