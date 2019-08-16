using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class FieldBuffTable : Singleton<FieldBuffTable>, IDataTable
{
	public class FieldBuffData
	{
		public uint id;

		public string name;

		public List<uint> buffTableIds = new List<uint>();

		public const string NT = "id,name,buffTableIds";

		public static bool cb(CSVReader csv_reader, FieldBuffData data, ref uint key)
		{
			data.id = key;
			csv_reader.Pop(ref data.name);
			string value = string.Empty;
			csv_reader.Pop(ref value);
			string[] array = value.Split(':');
			data.buffTableIds.Clear();
			int i = 0;
			for (int num = array.Length; i < num; i++)
			{
				uint result = 0u;
				if (uint.TryParse(array[i], out result))
				{
					data.buffTableIds.Add(result);
				}
			}
			return true;
		}
	}

	private UIntKeyTable<FieldBuffData> dataTable;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<FieldBuffData> _003C_003Ef__mg_0024cache0;

	public void CreateTable(string text)
	{
		dataTable = TableUtility.CreateUIntKeyTable<FieldBuffData>(text, FieldBuffData.cb, "id,name,buffTableIds");
		dataTable.TrimExcess();
	}

	public FieldBuffData GetData(uint id)
	{
		if (dataTable == null)
		{
			return null;
		}
		return dataTable.Get(id);
	}
}
