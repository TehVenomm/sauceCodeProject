using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public class GachaSearchEnemyTable : Singleton<GachaSearchEnemyTable>, IDataTable
{
	public class GachaSearchEnemyData
	{
		public int id;

		public string name;

		public int bRare;

		public int aRare;

		public int sRare;

		public int ssRare;

		public int sssRare;

		public int fire;

		public int water;

		public int thunder;

		public int soil;

		public int light;

		public int dark;

		public int none;

		public int sortPriority;

		public DateTime startAt;

		public const string NT = "id,name,bRare,aRare,sRare,ssRare,sssRare,fire,water,thunder,soil,light,dark,none,sortPriority,startAt";

		public static bool cb(CSVReader csv_reader, GachaSearchEnemyData data, ref uint key)
		{
			data.id = (int)key;
			csv_reader.Pop(ref data.name);
			csv_reader.Pop(ref data.bRare);
			csv_reader.Pop(ref data.aRare);
			csv_reader.Pop(ref data.sRare);
			csv_reader.Pop(ref data.ssRare);
			csv_reader.Pop(ref data.sssRare);
			csv_reader.Pop(ref data.fire);
			csv_reader.Pop(ref data.water);
			csv_reader.Pop(ref data.thunder);
			csv_reader.Pop(ref data.soil);
			csv_reader.Pop(ref data.light);
			csv_reader.Pop(ref data.dark);
			csv_reader.Pop(ref data.none);
			csv_reader.Pop(ref data.sortPriority);
			string value = string.Empty;
			csv_reader.Pop(ref value);
			if (!string.IsNullOrEmpty(value))
			{
				DateTime.TryParse(value, out data.startAt);
			}
			return true;
		}
	}

	private UIntKeyTable<GachaSearchEnemyData> gachaSearchEnemyDataTable;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<GachaSearchEnemyData> _003C_003Ef__mg_0024cache0;

	public void CreateTable(string csv_text)
	{
		gachaSearchEnemyDataTable = TableUtility.CreateUIntKeyTable<GachaSearchEnemyData>(csv_text, GachaSearchEnemyData.cb, "id,name,bRare,aRare,sRare,ssRare,sssRare,fire,water,thunder,soil,light,dark,none,sortPriority,startAt");
		gachaSearchEnemyDataTable.TrimExcess();
	}

	public GachaSearchEnemyData[] GetSortedGachaSearchEnemyData()
	{
		DateTime now = TimeManager.GetNow();
		List<GachaSearchEnemyData> sortData = new List<GachaSearchEnemyData>();
		gachaSearchEnemyDataTable.ForEach(delegate(GachaSearchEnemyData o)
		{
			if (o.startAt <= now)
			{
				sortData.Add(o);
			}
		});
		sortData.Sort((GachaSearchEnemyData a, GachaSearchEnemyData b) => b.sortPriority - a.sortPriority);
		return sortData.ToArray();
	}

	public List<string> GetGachaSearchEnemyNames(GachaSearchEnemyData[] data)
	{
		List<string> list = new List<string>();
		for (int i = 0; i < data.Length; i++)
		{
			list.Add(data[i].name);
		}
		return list;
	}

	public int GetEnemySpeciesId(string name)
	{
		int id = 0;
		gachaSearchEnemyDataTable.ForEach(delegate(GachaSearchEnemyData o)
		{
			if (id == 0 && o.name == name)
			{
				id = o.id;
			}
		});
		return id;
	}

	public GachaSearchEnemyData[] GetEnemyDataOnRairtyFlag(GachaSearchEnemyData[] data, int rarityBit)
	{
		List<GachaSearchEnemyData> list = new List<GachaSearchEnemyData>();
		if ((rarityBit & 4) != 0)
		{
			for (int i = 0; i < data.Length; i++)
			{
				if (data[i].bRare == 1)
				{
					list.Add(data[i]);
				}
			}
		}
		if ((rarityBit & 8) != 0)
		{
			for (int j = 0; j < data.Length; j++)
			{
				if (data[j].aRare == 1)
				{
					list.Add(data[j]);
				}
			}
		}
		if ((rarityBit & 0x10) != 0)
		{
			for (int k = 0; k < data.Length; k++)
			{
				if (data[k].sRare == 1)
				{
					list.Add(data[k]);
				}
			}
		}
		if ((rarityBit & 0x20) != 0)
		{
			for (int l = 0; l < data.Length; l++)
			{
				if (data[l].ssRare == 1)
				{
					list.Add(data[l]);
				}
			}
		}
		list.Sort((GachaSearchEnemyData a, GachaSearchEnemyData b) => b.sortPriority - a.sortPriority);
		return list.Distinct().ToArray();
	}

	public GachaSearchEnemyData[] GetEnemyDataOnElementFlag(GachaSearchEnemyData[] data, int elementBit)
	{
		List<GachaSearchEnemyData> list = new List<GachaSearchEnemyData>();
		if ((elementBit & 1) != 0)
		{
			for (int i = 0; i < data.Length; i++)
			{
				if (data[i].fire == 1)
				{
					list.Add(data[i]);
				}
			}
		}
		if ((elementBit & 2) != 0)
		{
			for (int j = 0; j < data.Length; j++)
			{
				if (data[j].water == 1)
				{
					list.Add(data[j]);
				}
			}
		}
		if ((elementBit & 4) != 0)
		{
			for (int k = 0; k < data.Length; k++)
			{
				if (data[k].thunder == 1)
				{
					list.Add(data[k]);
				}
			}
		}
		if ((elementBit & 8) != 0)
		{
			for (int l = 0; l < data.Length; l++)
			{
				if (data[l].soil == 1)
				{
					list.Add(data[l]);
				}
			}
		}
		if ((elementBit & 0x10) != 0)
		{
			for (int m = 0; m < data.Length; m++)
			{
				if (data[m].light == 1)
				{
					list.Add(data[m]);
				}
			}
		}
		if ((elementBit & 0x20) != 0)
		{
			for (int n = 0; n < data.Length; n++)
			{
				if (data[n].dark == 1)
				{
					list.Add(data[n]);
				}
			}
		}
		list.Sort((GachaSearchEnemyData a, GachaSearchEnemyData b) => b.sortPriority - a.sortPriority);
		return list.Distinct().ToArray();
	}
}
