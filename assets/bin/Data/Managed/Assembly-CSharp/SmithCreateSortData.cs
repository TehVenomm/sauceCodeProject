public class SmithCreateSortData : SortCompareData
{
	public SmithCreateItemInfo createData;

	public override object GetItemData()
	{
		return createData;
	}

	public override void SetItem(object item)
	{
		createData = (SmithCreateItemInfo)item;
	}

	public override void SetupSortingData(SortBase.SORT_REQUIREMENT requirement, EquipItemStatus status = null)
	{
		switch (requirement)
		{
		case SortBase.SORT_REQUIREMENT.GET:
			sortingData = 0L;
			return;
		case SortBase.SORT_REQUIREMENT.RARITY:
			sortingData = (long)createData.equipTableData.rarity;
			return;
		case SortBase.SORT_REQUIREMENT.LV:
			sortingData = 1L;
			return;
		case SortBase.SORT_REQUIREMENT.ATK:
			sortingData = (int)createData.equipTableData.baseAtk + createData.equipTableData.baseElemAtk;
			return;
		case SortBase.SORT_REQUIREMENT.DEF:
			sortingData = (int)createData.equipTableData.baseDef + createData.equipTableData.baseElemDef;
			return;
		case SortBase.SORT_REQUIREMENT.SALE:
			sortingData = createData.equipTableData.sale;
			return;
		case SortBase.SORT_REQUIREMENT.SOCKET:
			sortingData = createData.equipTableData.maxSlot;
			return;
		case SortBase.SORT_REQUIREMENT.PRICE:
			sortingData = (int)createData.smithCreateTableData.needMoney;
			return;
		case SortBase.SORT_REQUIREMENT.NUM:
			sortingData = 1L;
			return;
		case SortBase.SORT_REQUIREMENT.ELEMENT:
			sortingData = 6L - (long)GetIconElement();
			return;
		}
		sortingData = 0L;
		CreatePickupItemTable.CreatePickupItemData pickupCreateItem = Singleton<CreatePickupItemTable>.I.GetPickupCreateItem(createData.smithCreateTableData.id);
		if (pickupCreateItem != null)
		{
			sortingData = pickupCreateItem.id;
		}
	}

	public override bool IsFavorite()
	{
		return false;
	}

	public override RARITY_TYPE GetRarity()
	{
		return createData.equipTableData.rarity;
	}

	public override ELEMENT_TYPE GetIconElement()
	{
		if (!createData.equipTableData.IsWeapon())
		{
			return (ELEMENT_TYPE)createData.equipTableData.GetElemDefTypePriorityToTable();
		}
		return (ELEMENT_TYPE)createData.equipTableData.GetElemAtkTypePriorityToTable();
	}

	public override int GetItemType()
	{
		return EquipmentTypeToSortBaseType(createData.equipTableData.type);
	}

	public override uint GetTableID()
	{
		return createData.equipTableData.id;
	}

	public override string GetName()
	{
		return createData.equipTableData.name;
	}

	public override ItemStatus GetItemStatus()
	{
		return createData.equipTableData.GetDefaultSkillBuffParam();
	}

	public override ITEM_ICON_TYPE GetIconType()
	{
		return ItemIcon.GetItemIconType(createData.equipTableData.type);
	}

	public override REWARD_TYPE GetMaterialType()
	{
		return REWARD_TYPE.EQUIP_ITEM;
	}

	public override GET_TYPE GetGetType()
	{
		return createData.equipTableData.getType;
	}

	public override uint GetMainorSortWeight()
	{
		uint num = EquipmentTypeToMinorSortValue(createData.equipTableData.type);
		uint num2 = 0 + (num << 26);
		uint num3 = ElementTypeToMinorSortValue(GetIconElement());
		uint num4 = num2 + (num3 << 23);
		uint rarity = (uint)GetRarity();
		uint num5 = num4 + (rarity << 20);
		uint spAttackType = (uint)createData.equipTableData.spAttackType;
		uint num6 = num5 + (spAttackType << 15);
		uint typeToMinorSortValue = GetTypeToMinorSortValue(GetGetType());
		return num6 + (typeToMinorSortValue << 7);
	}

	public ItemIconDetail.ICON_STATUS GetIconStatus()
	{
		if (!MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterial(createData.smithCreateTableData.needMaterial))
		{
			return ItemIconDetail.ICON_STATUS.NOT_ENOUGH_MATERIAL;
		}
		if ((int)createData.smithCreateTableData.needMoney > MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money)
		{
			return ItemIconDetail.ICON_STATUS.NOT_ENOUGH_MATERIAL;
		}
		return ItemIconDetail.ICON_STATUS.NONE;
	}

	public override int getEquipFilterPay()
	{
		return MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterialAndPayAndObtained(createData) & 3;
	}

	public override int getEquipFilterCreatable()
	{
		return MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterialAndPayAndObtained(createData) & 0xC;
	}

	public override int getEquipFilterObtained()
	{
		return MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterialAndPayAndObtained(createData) & 0x30;
	}

	public override bool getEquipFilterPayAndCreatable(int filter)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterialAndPayAndObtained(createData);
		num = (num4 & 3);
		num2 = (num4 & 0xC);
		num3 = (num4 & 0x30);
		if ((filter & num) == 0 || (filter & num2) == 0 || (filter & num3) == 0)
		{
			return true;
		}
		return false;
	}
}
