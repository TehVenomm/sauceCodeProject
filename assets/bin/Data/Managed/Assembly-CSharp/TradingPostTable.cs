public class TradingPostTable : Singleton<TradingPostTable>, IDataTable
{
	public class ItemData
	{
		public const string NT = "itemId,itemType,itemName,maxQuantity,cantSell,startDate,endDate";

		public uint itemId;

		public string itemType;

		public string itemName;

		public uint maxQuantity;

		public bool cantSell;

		public string startDate;

		public string endDate;

		public static bool cb(CSVReader csv_reader, ItemData data, ref uint key)
		{
			data.itemId = key;
			csv_reader.Pop(ref data.itemType);
			csv_reader.Pop(ref data.itemName);
			csv_reader.Pop(ref data.maxQuantity);
			csv_reader.Pop(ref data.cantSell);
			csv_reader.Pop(ref data.startDate);
			csv_reader.Pop(ref data.endDate);
			return true;
		}
	}

	private UIntKeyTable<ItemData> itemTable;

	public UIntKeyTable<ItemData> ItemTable => itemTable;

	public void CreateTable(string csv_text)
	{
		itemTable = TableUtility.CreateUIntKeyTable<ItemData>(csv_text, ItemData.cb, "itemId,itemType,itemName,maxQuantity,cantSell,startDate,endDate", null);
		itemTable.TrimExcess();
	}

	public void AddTable(string csv_text)
	{
		TableUtility.AddUIntKeyTable(itemTable, csv_text, ItemData.cb, "itemId,itemType,itemName,maxQuantity,cantSell,startDate,endDate", null);
	}

	public void CreateTableFromInternal(string encrypted_csv_text)
	{
		string csv_text = DataTableManager.Decrypt(encrypted_csv_text);
		CreateTable(csv_text);
	}

	public ItemData GetItemData(uint id)
	{
		if (itemTable == null)
		{
			return null;
		}
		ItemData itemData = itemTable.Get(id);
		if (itemData == null)
		{
			Log.TableError(this, id);
			itemData = new ItemData();
		}
		return itemData;
	}

	public bool IsExistItemData(uint id)
	{
		if (itemTable == null)
		{
			return false;
		}
		ItemData itemData = itemTable.Get(id);
		if (itemData == null)
		{
			return false;
		}
		return true;
	}
}
