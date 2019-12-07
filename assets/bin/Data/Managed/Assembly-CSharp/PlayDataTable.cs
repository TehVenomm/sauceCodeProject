using Network;
using System;
using System.Collections.Generic;

public class PlayDataTable : Singleton<PlayDataTable>, IDataTable
{
	public class PlayData
	{
		public int id;

		public int type;

		public int subType;

		public string name;

		public string format;

		public int orderNo;

		public DateTime startAt;

		public int count;

		public const string NT = "id,type,subType,name,format,orderNo,startAt";

		public static bool cb(CSVReader csv_reader, PlayData data, ref uint key)
		{
			data.id = (int)key;
			csv_reader.Pop(ref data.type);
			csv_reader.Pop(ref data.subType);
			csv_reader.Pop(ref data.name);
			csv_reader.Pop(ref data.format);
			csv_reader.Pop(ref data.orderNo);
			string value = string.Empty;
			csv_reader.Pop(ref value);
			if (!string.IsNullOrEmpty(value))
			{
				DateTime.TryParse(value, out data.startAt);
			}
			return true;
		}
	}

	private UIntKeyTable<PlayData> playDataTable;

	public void CreateTable(string csv_text)
	{
		playDataTable = TableUtility.CreateUIntKeyTable<PlayData>(csv_text, PlayData.cb, "id,type,subType,name,format,orderNo,startAt");
		playDataTable.TrimExcess();
	}

	public PlayData[] GetSortedPlayData(AchievementCounter[] dataList)
	{
		DateTime now = TimeManager.GetNow();
		List<PlayData> sortData = new List<PlayData>();
		playDataTable.ForEach(delegate(PlayData o)
		{
			if (o.startAt <= now)
			{
				AchievementCounter achievementCounter = dataList.Find((AchievementCounter x) => x.type == o.type && x.subType == o.subType);
				string s = (achievementCounter != null) ? achievementCounter.count : "0";
				o.count = int.Parse(s);
				sortData.Add(o);
			}
		});
		sortData.Sort((PlayData a, PlayData b) => a.orderNo - b.orderNo);
		return sortData.ToArray();
	}
}
