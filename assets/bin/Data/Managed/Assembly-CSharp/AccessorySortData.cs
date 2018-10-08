public class AccessorySortData : SortCompareData
{
	public AccessoryInfo itemData;

	public override object GetItemData()
	{
		return itemData;
	}

	public override void SetItem(object item)
	{
		itemData = (AccessoryInfo)item;
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
		case SortBase.SORT_REQUIREMENT.PRICE:
			sortingData = itemData.tableData.price;
			break;
		}
	}

	public override bool IsFavorite()
	{
		return itemData.isFavorite;
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
		return itemData.tableData.name;
	}

	public override int GetIconID()
	{
		return (int)itemData.tableID;
	}

	public override ITEM_ICON_TYPE GetIconType()
	{
		return ITEM_ICON_TYPE.ACCESSORY;
	}

	public override string GetDetail()
	{
		return itemData.tableData.descript;
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
		return !itemData.tableData.cantSell;
	}

	public override REWARD_TYPE GetMaterialType()
	{
		return REWARD_TYPE.ACCESSORY;
	}

	public override uint GetMainorSortWeight()
	{
		return (uint)itemData.tableData.orderValue;
	}
}
