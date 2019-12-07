using System.Collections.Generic;

public class GatherItemTable : Singleton<GatherItemTable>, IDataTable
{
	public class GatherItemData
	{
		public uint gatherItemId;

		public int listId;

		public string name;

		public int average;

		public int stdDev;

		public int enemyPopId;

		public int isRare;

		public const string NT = "gatherItemId,listId,name,average,stdDev,enemyPopId,isRare";

		public static bool cb(CSVReader csv_reader, GatherItemData data, ref uint key)
		{
			data.gatherItemId = key;
			csv_reader.Pop(ref data.listId);
			csv_reader.Pop(ref data.name);
			csv_reader.Pop(ref data.average);
			csv_reader.Pop(ref data.stdDev);
			csv_reader.Pop(ref data.enemyPopId);
			csv_reader.Pop(ref data.isRare);
			return true;
		}
	}

	private UIntKeyTable<GatherItemData> dataTable;

	public void CreateTable(string text)
	{
		dataTable = TableUtility.CreateUIntKeyTable<GatherItemData>(text, GatherItemData.cb, "gatherItemId,listId,name,average,stdDev,enemyPopId,isRare");
		dataTable.TrimExcess();
	}

	public GatherItemData GetData(uint id)
	{
		if (dataTable == null)
		{
			return null;
		}
		return dataTable.Get(id);
	}

	public List<GatherItemData> GetAllData()
	{
		if (dataTable == null)
		{
			return null;
		}
		List<GatherItemData> list = new List<GatherItemData>();
		dataTable.ForEach(delegate(GatherItemData data)
		{
			list.Add(data);
		});
		return list;
	}
}
