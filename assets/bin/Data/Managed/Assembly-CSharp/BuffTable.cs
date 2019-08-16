using System.Runtime.CompilerServices;

public class BuffTable : Singleton<BuffTable>, IDataTable
{
	public class BuffData
	{
		public uint id;

		public uint growID;

		public BuffParam.BUFFTYPE type;

		public BuffParam.VALUE_TYPE valueType;

		public int value;

		public float duration;

		public float interval;

		public const string NT = "id,growId,type,valueType,value,duration,interval";

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

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<BuffData> _003C_003Ef__mg_0024cache0;

	public void CreateTable(string text)
	{
		dataTable = TableUtility.CreateUIntKeyTable<BuffData>(text, BuffData.cb, "id,growId,type,valueType,value,duration,interval");
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
