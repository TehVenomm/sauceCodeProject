public class StatusAvatarEquipSelect : StatusEquip
{
	private EQUIPMENT_TYPE changeTargetType;

	private EquipItemInfo equippingItem;

	private EquipItemInfo selectItem;

	protected override EquipItemInfo EquipItem
	{
		get
		{
			return selectItem;
		}
		set
		{
			selectItem = value;
		}
	}

	protected override EquipItemInfo GetCompareItemData()
	{
		return equippingItem;
	}

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		changeTargetType = (EQUIPMENT_TYPE)array[0];
		equippingItem = (array[1] as EquipItemInfo);
		base.selectEquipSetData = (array[2] as LocalEquipSetData);
		EquipItem = equippingItem;
		if (equippingItem == null)
		{
			selectInventoryIndex = -1;
		}
		else
		{
			selectInventoryIndex = GetSelectItemIndex();
		}
		GameSection.SetEventData(base.selectEquipSetData);
		MonoBehaviourSingleton<StatusManager>.I.SetEquippingItem(equippingItem);
		base.Initialize();
	}

	protected override void InitSort()
	{
		sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.ARMOR, SortSettings.SETTINGS_TYPE.EQUIP_ITEM);
	}

	protected override void InitLocalInventory()
	{
		SortCompareData[] array = localInventoryEquipData = CreateLocalnventory();
		sortSettings.Sort(localInventoryEquipData as EquipItemSortData[]);
	}

	protected override void SelectingInventoryFirst()
	{
		selectInventoryIndex = -1;
	}

	protected override bool IsNotEquip(bool is_not_equip_any_slot, bool is_equip_now_slot)
	{
		return !is_equip_now_slot;
	}

	public override void UpdateUI()
	{
		SetActive(UI.OBJ_STATUS_ROOT, is_visible: false);
		base.UpdateUI();
	}

	protected override void EquipParam()
	{
	}

	protected override void EquipImg()
	{
		if (EquipItem != null)
		{
			SetRenderEquipModel(UI.TEX_MODEL, EquipItem.tableID);
		}
		else
		{
			ClearRenderModel(UI.TEX_MODEL);
		}
	}

	protected override void OnQuery_TRY_ON()
	{
		base.OnQuery_TRY_ON();
		if (EquipItem != null && !MonoBehaviourSingleton<GameSceneManager>.I.CheckEquipItemAndOpenUpdateAppDialog(EquipItem.tableData, base.OnCancelSelect))
		{
			GameSection.StopEvent();
		}
		else
		{
			EquipImg();
		}
	}

	protected override bool IsCreateRemoveButton()
	{
		return true;
	}

	protected override bool IsAlreadyEquipItem(EquipItemInfo item)
	{
		return false;
	}

	private EquipItemSortData[] CreateLocalnventory()
	{
		EquipItemInfo[] array = null;
		switch (changeTargetType)
		{
		default:
			array = MonoBehaviourSingleton<InventoryManager>.I.GetVisualArmorInventory().ToArray();
			break;
		case EQUIPMENT_TYPE.HELM:
			array = MonoBehaviourSingleton<InventoryManager>.I.GetVisualHelmInventory().ToArray();
			break;
		case EQUIPMENT_TYPE.ARM:
			array = MonoBehaviourSingleton<InventoryManager>.I.GetVisualArmInventory().ToArray();
			break;
		case EQUIPMENT_TYPE.LEG:
			array = MonoBehaviourSingleton<InventoryManager>.I.GetVisualLegInventory().ToArray();
			break;
		}
		return sortSettings.CreateSortAry<EquipItemInfo, EquipItemSortData>(array);
	}

	protected override void OnQueryDetail()
	{
		int num = (int)GameSection.GetEventData();
		detailItem = null;
		if (num >= 0 && localInventoryEquipData != null)
		{
			detailItem = (localInventoryEquipData[num].GetItemData() as EquipItemInfo);
		}
		if (detailItem == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.SetEventData(new object[3]
			{
				ItemDetailEquip.CURRENT_SECTION.STATUS_AVATAR,
				detailItem,
				base.selectEquipSetData.setNo
			});
		}
	}

	protected override string GetSelectTypeText()
	{
		string result = string.Empty;
		switch (changeTargetType)
		{
		case EQUIPMENT_TYPE.ARMOR:
			result = base.sectionData.GetText("SELECT_ARMOR");
			break;
		case EQUIPMENT_TYPE.HELM:
			result = base.sectionData.GetText("SELECT_HELM");
			break;
		case EQUIPMENT_TYPE.ARM:
			result = base.sectionData.GetText("SELECT_ARM");
			break;
		case EQUIPMENT_TYPE.LEG:
			result = base.sectionData.GetText("SELECT_LEG");
			break;
		}
		return result;
	}
}
