public class ItemSortData : SortCompareData
{
	public ItemInfo itemData;

	public override object GetItemData()
	{
		return itemData;
	}

	public override void SetItem(object item)
	{
		itemData = (ItemInfo)item;
	}

	public override void SetupSortingData(SortBase.SORT_REQUIREMENT requirement, EquipItemStatus status = null)
	{
		switch (requirement)
		{
		default:
			sortingData = (long)itemData.uniqueID;
			break;
		case SortBase.SORT_REQUIREMENT.RARITY:
			sortingData = (long)itemData.tableData.rarity;
			break;
		case SortBase.SORT_REQUIREMENT.LV:
			sortingData = 1L;
			break;
		case SortBase.SORT_REQUIREMENT.ATK:
			sortingData = 0L;
			break;
		case SortBase.SORT_REQUIREMENT.DEF:
			sortingData = 0L;
			break;
		case SortBase.SORT_REQUIREMENT.SALE:
			sortingData = itemData.tableData.price;
			break;
		case SortBase.SORT_REQUIREMENT.SOCKET:
			sortingData = 0L;
			break;
		case SortBase.SORT_REQUIREMENT.PRICE:
			sortingData = 0L;
			break;
		case SortBase.SORT_REQUIREMENT.NUM:
			sortingData = itemData.num;
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
		return (int)itemData.tableData.type;
	}

	public override int GetNum()
	{
		return itemData.num;
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
		return itemData.tableData.name;
	}

	public override int GetIconID()
	{
		return itemData.tableData.iconID;
	}

	public override ITEM_ICON_TYPE GetIconType()
	{
		return ItemIcon.GetItemIconType(itemData.tableData.type);
	}

	public override ELEMENT_TYPE GetIconElement()
	{
		return (ELEMENT_TYPE)itemData.tableData.element;
	}

	public override string GetDetail()
	{
		return itemData.tableData.text;
	}

	public override RARITY_TYPE GetRarity()
	{
		return itemData.tableData.rarity;
	}

	public override int GetSalePrice()
	{
		return itemData.tableData.price;
	}

	public override bool CanSale()
	{
		return !itemData.tableData.cantSale;
	}

	public override REWARD_TYPE GetMaterialType()
	{
		return REWARD_TYPE.ITEM;
	}

	public override bool IsLithograph()
	{
		if (itemData == null || itemData.tableData == null)
		{
			return false;
		}
		return itemData.tableData.type == ITEM_TYPE.LITHOGRAPH;
	}

	public override uint GetMainorSortWeight()
	{
		uint num = 0u;
		if (itemData.tableData.type == ITEM_TYPE.ABILITY_ITEM)
		{
			uint num2 = ElementTypeToMinorSortValue(GetIconElement());
			num += num2 << 6;
			uint rarity = (uint)GetRarity();
			return num + rarity;
		}
		return GetTableID();
	}
}
