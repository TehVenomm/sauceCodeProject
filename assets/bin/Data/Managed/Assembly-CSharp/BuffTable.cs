public class BuffTable : Singleton<BuffTable>, IDataTable
{
	public class BuffData
	{
		public const string NT = "id,growId,type,valueType,value,duration,interval";

		public uint id;

		public uint growID;

		public BuffParam.BUFFTYPE type;

		public BuffParam.VALUE_TYPE valueType;

		public int value;

		public float duration;

		public float interval;

		public static bool cb(CSVReader csv_reader, BuffData data, ref uint key)
		{
			data.id = key;
			csv_reader.Pop(ref data.growID);
			csv_reader.Pop(ref data.type);
			csv_reader.Pop(ref data.valueType);
			csv_reader.Pop(ref data.value);
			csv_reader.Pop(ref data.duration);
			csv_reader.Pop(ref data.interval);
			return true;
		}
	}

	private UIntKeyTable<BuffData> dataTable;

	public void CreateTable(string text)
	{
		dataTable = TableUtility.CreateUIntKeyTable<BuffData>(text, BuffData.cb, "id,growId,type,valueType,value,duration,interval", null);
		dataTable.TrimExcess();
	}

	public BuffData GetData(uint id)
	{
		if (dataTable == null)
		{
			return null;
		}
		return dataTable.Get(id);
	}
}
