public class TutorialGearSetTable : Singleton<TutorialGearSetTable>, IDataTable
{
	public class ItemData
	{
		public uint id;

		public string name;

		public string difficulty;

		public uint weaponId;

		public uint helmId;

		public uint armId;

		public uint legId;

		public uint armorId;

		public uint skillItemId;

		public const string NT = "setId,setName,difficulty,weaponId,helmId,armId,legId,armorId,skillId";

		public static bool cb(CSVReader csv_reader, ItemData data, ref uint key)
		{
			data.id = key;
			csv_reader.Pop(ref data.name);
			csv_reader.Pop(ref data.difficulty);
			csv_reader.Pop(ref data.weaponId);
			csv_reader.Pop(ref data.helmId);
			csv_reader.Pop(ref data.armId);
			csv_reader.Pop(ref data.legId);
			csv_reader.Pop(ref data.armorId);
			csv_reader.Pop(ref data.skillItemId);
			return true;
		}
	}

	private UIntKeyTable<ItemData> itemTable;

	public UIntKeyTable<ItemData> ItemTable => itemTable;

	public void CreateTable(string csv_text)
	{
		itemTable = TableUtility.CreateUIntKeyTable<ItemData>(csv_text, ItemData.cb, "setId,setName,difficulty,weaponId,helmId,armId,legId,armorId,skillId");
		itemTable.TrimExcess();
	}

	public void AddTable(string csv_text)
	{
		TableUtility.AddUIntKeyTable(itemTable, csv_text, ItemData.cb, "setId,setName,difficulty,weaponId,helmId,armId,legId,armorId,skillId");
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
			itemData.name = Log.NON_DATA_NAME;
		}
		return itemData;
	}

	public bool IsExistItemData(uint id)
	{
		if (itemTable == null)
		{
			return false;
		}
		if (itemTable.Get(id) == null)
		{
			return false;
		}
		return true;
	}
}
