public class AbilityItemSortData : SortCompareData
{
	public AbilityItemInfo itemData;

	public override object GetItemData()
	{
		return itemData;
	}

	public override void SetItem(object item)
	{
		itemData = (AbilityItemInfo)item;
	}

	public override void SetupSortingData(SortBase.SORT_REQUIREMENT requirement, EquipItemStatus status = null)
	{
		switch (requirement)
		{
		default:
			sortingData = (long)itemData.uniqueID;
			break;
		case SortBase.SORT_REQUIREMENT.RARITY:
			sortingData = (long)itemData.GetItemTableData().rarity;
			break;
		case SortBase.SORT_REQUIREMENT.LV:
			sortingData = 0L;
			break;
		case SortBase.SORT_REQUIREMENT.ATK:
			sortingData = 0L;
			break;
		case SortBase.SORT_REQUIREMENT.DEF:
			sortingData = 0L;
			break;
		case SortBase.SORT_REQUIREMENT.SALE:
			sortingData = itemData.GetItemTableData().price;
			break;
		case SortBase.SORT_REQUIREMENT.SOCKET:
			sortingData = 0L;
			break;
		case SortBase.SORT_REQUIREMENT.PRICE:
			sortingData = 0L;
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

	public override int GetItemType()
	{
		return (int)itemData.GetItemTableData().type;
	}

	public override int GetNum()
	{
		return 1;
	}

	public override ulong GetUniqID()
	{
		return itemData.uniqueID;
	}

	public override uint GetTableID()
	{
		return itemData.tableID;
	}

	public override string GetName()
	{
		return itemData.GetName();
	}

	public override int GetIconID()
	{
		return itemData.GetItemTableData().iconID;
	}

	public override ITEM_ICON_TYPE GetIconType()
	{
		return ITEM_ICON_TYPE.ABILITY_ITEM;
	}

	public override ELEMENT_TYPE GetIconElement()
	{
		return (ELEMENT_TYPE)itemData.GetItemTableData().element;
	}

	public override string GetDetail()
	{
		return itemData.GetDescription();
	}

	public override RARITY_TYPE GetRarity()
	{
		return itemData.GetItemTableData().rarity;
	}

	public override int GetSalePrice()
	{
		return itemData.GetItemTableData().price;
	}

	public override bool CanSale()
	{
		return !itemData.GetItemTableData().cantSale;
	}

	public override REWARD_TYPE GetMaterialType()
	{
		return REWARD_TYPE.ABILITY_ITEM;
	}

	public override bool IsLithograph()
	{
		if (itemData == null || itemData.GetItemTableData() == null)
		{
			return false;
		}
		return itemData.GetItemTableData().type == ITEM_TYPE.LITHOGRAPH;
	}

	public override uint GetMainorSortWeight()
	{
		uint num = 0u;
		uint num2 = ElementTypeToMinorSortValue(GetIconElement());
		num += num2 << 27;
		uint rarity = (uint)GetRarity();
		return num + (rarity << 21);
	}
}
