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
		default:
		{
			sortingData = 0L;
			CreatePickupItemTable.CreatePickupItemData pickupCreateItem = Singleton<CreatePickupItemTable>.I.GetPickupCreateItem(createData.smithCreateTableData.id);
			if (pickupCreateItem != null)
			{
				sortingData = pickupCreateItem.id;
			}
			break;
		}
		case SortBase.SORT_REQUIREMENT.GET:
			sortingData = 0L;
			break;
		case SortBase.SORT_REQUIREMENT.RARITY:
			sortingData = (long)createData.equipTableData.rarity;
			break;
		case SortBase.SORT_REQUIREMENT.LV:
			sortingData = 1L;
			break;
		case SortBase.SORT_REQUIREMENT.ATK:
			sortingData = (int)createData.equipTableData.baseAtk + createData.equipTableData.baseElemAtk;
			break;
		case SortBase.SORT_REQUIREMENT.DEF:
			sortingData = (int)createData.equipTableData.baseDef + createData.equipTableData.baseElemDef;
			break;
		case SortBase.SORT_REQUIREMENT.SALE:
			sortingData = createData.equipTableData.sale;
			break;
		case SortBase.SORT_REQUIREMENT.SOCKET:
			sortingData = createData.equipTableData.maxSlot;
			break;
		case SortBase.SORT_REQUIREMENT.PRICE:
			sortingData = (int)createData.smithCreateTableData.needMoney;
			break;
		case SortBase.SORT_REQUIREMENT.NUM:
			sortingData = 1L;
			break;
		case SortBase.SORT_REQUIREMENT.ELEMENT:
			sortingData = 6L - (long)GetIconElement();
			break;
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
		return (ELEMENT_TYPE)((!createData.equipTableData.IsWeapon()) ? createData.equipTableData.GetElemDefTypePriorityToTable(null) : createData.equipTableData.GetElemAtkTypePriorityToTable(null));
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
		uint num = 0u;
		uint num2 = EquipmentTypeToMinorSortValue(createData.equipTableData.type);
		num += num2 << 26;
		uint num3 = ElementTypeToMinorSortValue(GetIconElement());
		num += num3 << 23;
		uint rarity = (uint)GetRarity();
		num += rarity << 20;
		uint spAttackType = (uint)createData.equipTableData.spAttackType;
		num += spAttackType << 15;
		uint typeToMinorSortValue = GetTypeToMinorSortValue(GetGetType());
		return num + (typeToMinorSortValue << 7);
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
		int num = 0;
		num = MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterialAndPayAndObtained(createData);
		return num & 3;
	}

	public override int getEquipFilterCreatable()
	{
		int num = 0;
		num = MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterialAndPayAndObtained(createData);
		return num & 0xC;
	}

	public override int getEquipFilterObtained()
	{
		int num = 0;
		num = MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterialAndPayAndObtained(createData);
		return num & 0x30;
	}

	public override bool getEquipFilterPayAndCreatable(int filter)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		num = MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterialAndPayAndObtained(createData);
		num2 = (num & 3);
		num3 = (num & 0xC);
		num4 = (num & 0x30);
		if ((filter & num2) == 0 || (filter & num3) == 0 || (filter & num4) == 0)
		{
			return true;
		}
		return false;
	}
}
