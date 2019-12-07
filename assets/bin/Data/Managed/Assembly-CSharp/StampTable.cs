using System;
using System.Collections.Generic;

public class StampTable : Singleton<StampTable>, IDataTable
{
	[Serializable]
	public class Data
	{
		public const string INDEX_NAMES = "id,desc,seId,type";

		public uint id;

		public string desc;

		public STAMP_TYPE type;

		public int seId;

		public bool hasSE;

		public static bool InsertRow(CSVReader CSVReader, Data Data, ref uint key)
		{
			Data.id = key;
			CSVReader.Pop(ref Data.desc);
			CSVReader.Pop(ref Data.seId);
			CSVReader.PopEnum(ref Data.type, STAMP_TYPE.NONE);
			Data.hasSE = (Data.seId > 0);
			return true;
		}
	}

	public UIntKeyTable<Data> table;

	public void CreateTable(string csv_text)
	{
		table = TableUtility.CreateUIntKeyTable<Data>(csv_text, Data.InsertRow, "id,desc,seId,type");
		table.TrimExcess();
	}

	public Data GetData(uint id)
	{
		if (table == null)
		{
			return null;
		}
		return table.Get(id);
	}

	public List<Data> GetUnlockStamps(UserInfoManager userInfo)
	{
		List<Data> data = new List<Data>();
		table.ForEach(delegate(Data stampData)
		{
			if (stampData.type == STAMP_TYPE.COMMON || userInfo.unlockStampIds.Contains((int)stampData.id))
			{
				data.Add(stampData);
			}
		});
		return data;
	}
}
