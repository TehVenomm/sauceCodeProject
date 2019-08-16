using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class WaveMatchDropTable : Singleton<WaveMatchDropTable>, IDataTable
{
	public class WaveMatchDropData
	{
		public uint id;

		public string model;

		public WAVEMATCH_ITEM_TYPE type;

		public CALCULATE_TYPE calcType;

		public int value;

		public List<uint> buffTableIds = new List<uint>();

		public string getEffect = string.Empty;

		public int getSE;

		public const string NT = "id,model,type,calcType,value,buffTableIds,getEffect,getSE";

		public static bool cb(CSVReader csv_reader, WaveMatchDropData data, ref uint key)
		{
			data.id = key;
			csv_reader.Pop(ref data.model);
			csv_reader.Pop(ref data.type);
			csv_reader.Pop(ref data.calcType);
			csv_reader.Pop(ref data.value);
			string empty = string.Empty;
			csv_reader.Pop(ref empty);
			string[] array = empty.Split(':');
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
			csv_reader.Pop(ref data.getEffect);
			csv_reader.Pop(ref data.getSE);
			return true;
		}
	}

	private UIntKeyTable<WaveMatchDropData> dataTable;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<WaveMatchDropData> _003C_003Ef__mg_0024cache0;

	public void CreateTable(string text)
	{
		dataTable = TableUtility.CreateUIntKeyTable<WaveMatchDropData>(text, WaveMatchDropData.cb, "id,model,type,calcType,value,buffTableIds,getEffect,getSE");
		dataTable.TrimExcess();
	}

	public WaveMatchDropData GetData(uint id)
	{
		if (dataTable == null)
		{
			return null;
		}
		return dataTable.Get(id);
	}

	public List<WaveMatchDropData> GetAllData()
	{
		if (dataTable == null)
		{
			return null;
		}
		List<WaveMatchDropData> list = new List<WaveMatchDropData>();
		dataTable.ForEach(delegate(WaveMatchDropData data)
		{
			list.Add(data);
		});
		return list;
	}
}
