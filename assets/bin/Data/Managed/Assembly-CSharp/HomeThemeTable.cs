using System;

public class HomeThemeTable : Singleton<HomeThemeTable>, IDataTable
{
	public class HomeThemeData
	{
		public const string NT = "id,name,sceneName,npc0MdlID,npc1MdlID,npc2MdlID,npc6MdlID,bgmID,startAt,endAt";

		public int id;

		public string name;

		public string sceneName;

		public int npc0MdlID = -1;

		public int npc1MdlID = -1;

		public int npc2MdlID = -1;

		public int npc6MdlID = -1;

		public int bgmId = -1;

		public DateTime startAt;

		public DateTime endAt;

		public static bool cb(CSVReader csv_reader, HomeThemeData data, ref uint key)
		{
			data.id = (int)key;
			csv_reader.Pop(ref data.name);
			csv_reader.Pop(ref data.sceneName);
			csv_reader.Pop(ref data.npc0MdlID);
			csv_reader.Pop(ref data.npc1MdlID);
			csv_reader.Pop(ref data.npc2MdlID);
			csv_reader.Pop(ref data.npc6MdlID);
			csv_reader.Pop(ref data.bgmId);
			string value = string.Empty;
			csv_reader.Pop(ref value);
			if (!string.IsNullOrEmpty(value))
			{
				DateTime.TryParse(value, out data.startAt);
			}
			string value2 = string.Empty;
			csv_reader.Pop(ref value2);
			if (!string.IsNullOrEmpty(value2))
			{
				DateTime.TryParse(value2, out data.endAt);
			}
			return true;
		}
	}

	private UIntKeyTable<HomeThemeData> homeThemeDataTable;

	private string currentHomeTheme;

	public string CurrentHomeTheme => currentHomeTheme;

	public void CreateTable(string csv_text)
	{
		homeThemeDataTable = TableUtility.CreateUIntKeyTable<HomeThemeData>(csv_text, HomeThemeData.cb, "id,name,sceneName,npc0MdlID,npc1MdlID,npc2MdlID,npc6MdlID,bgmID,startAt,endAt", null);
		homeThemeDataTable.TrimExcess();
	}

	public HomeThemeData GetHomeThemeData(DateTime dateTime)
	{
		HomeThemeData data = null;
		homeThemeDataTable.ForEach(delegate(HomeThemeData o)
		{
			if (data == null && o.startAt <= dateTime && o.endAt > dateTime)
			{
				data = o;
			}
		});
		if (data == null)
		{
			homeThemeDataTable.ForEach(delegate(HomeThemeData o)
			{
				if (o.name == "NORMAL")
				{
					data = o;
				}
			});
		}
		return data;
	}

	public HomeThemeData GetHomeThemeData(string themeName)
	{
		HomeThemeData data = null;
		homeThemeDataTable.ForEach(delegate(HomeThemeData o)
		{
			if (data == null && o.name == themeName)
			{
				data = o;
			}
		});
		return data;
	}

	public int GetNpcModelID(HomeThemeData data, int npcId)
	{
		switch (npcId)
		{
		case 0:
			return data.npc0MdlID;
		case 1:
			return data.npc1MdlID;
		case 2:
			return data.npc2MdlID;
		case 6:
			return data.npc6MdlID;
		default:
			return -1;
		}
	}

	public void SetCurrentHomeThemeName(string name)
	{
		currentHomeTheme = name;
	}
}
