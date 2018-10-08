using System;
using System.Collections.Generic;
using UnityEngine;

public class RegionTable : Singleton<RegionTable>, IDataTable
{
	public class Data
	{
		public const uint NON_PARENT_ID = uint.MaxValue;

		public const string NT = "regionId,name,iconId,x,y,z,w,h,mx,my,parentRegionId,eventId,difficulty,worldId,nextRegionId,startAt,groupId";

		private const int TIME_DATA_LEN = 2;

		public uint regionId;

		public string regionName;

		public int iconID;

		public Vector3 iconPos = Vector3.zero;

		public Vector2 iconSize = Vector2.zero;

		public Vector3 markerPos = Vector2.zero;

		public uint parentRegionId = uint.MaxValue;

		public int eventId;

		public int worldId;

		public REGION_DIFFICULTY_TYPE difficulty;

		public int nextRegionId;

		public DateTime startAt;

		public int groupId;

		public static int EVENT_START_TIME_LEN = 3;

		public bool hasParentRegion()
		{
			return parentRegionId != uint.MaxValue;
		}

		public static bool cb(CSVReader csvReader, Data data, ref uint key)
		{
			data.regionId = key;
			csvReader.Pop(ref data.regionName);
			csvReader.Pop(ref data.iconID);
			csvReader.Pop(ref data.iconPos);
			csvReader.Pop(ref data.iconSize);
			Vector2 value = new Vector2(0f, 0f);
			csvReader.Pop(ref value);
			data.markerPos = new Vector3(value.x, value.y, 0f);
			csvReader.Pop(ref data.parentRegionId);
			csvReader.Pop(ref data.eventId);
			csvReader.PopEnum(ref data.difficulty, REGION_DIFFICULTY_TYPE.NORMAL);
			csvReader.Pop(ref data.worldId);
			csvReader.Pop(ref data.nextRegionId);
			string value2 = string.Empty;
			csvReader.Pop(ref value2);
			if (!string.IsNullOrEmpty(value2))
			{
				DateTime.TryParse(value2, out data.startAt);
			}
			csvReader.Pop(ref data.groupId);
			return true;
		}

		public bool HasGroup()
		{
			return groupId > 0;
		}

		public bool HasStartAt()
		{
			if (startAt.CompareTo(default(DateTime)) == 0)
			{
				return false;
			}
			return true;
		}
	}

	private UIntKeyTable<Data> table;

	public void CreateTable(string csv_text)
	{
		table = TableUtility.CreateUIntKeyTable<Data>(csv_text, Data.cb, "regionId,name,iconId,x,y,z,w,h,mx,my,parentRegionId,eventId,difficulty,worldId,nextRegionId,startAt,groupId", null);
	}

	public Data GetData(uint id)
	{
		if (!Singleton<RegionTable>.IsValid())
		{
			return null;
		}
		return table.Get(id);
	}

	public Data[] GetData()
	{
		if (!Singleton<RegionTable>.IsValid())
		{
			return null;
		}
		int count = table.GetCount();
		Data[] ret = new Data[count];
		int index = 0;
		table.ForEach(delegate(Data data)
		{
			ret[++index] = data;
		});
		return ret;
	}

	public Data GetData(int groupId, REGION_DIFFICULTY_TYPE type)
	{
		if (!Singleton<RegionTable>.IsValid())
		{
			return null;
		}
		Data[] data = GetData();
		List<Data> list = new List<Data>();
		Data[] array = data;
		foreach (Data data2 in array)
		{
			if (data2.groupId == groupId && data2.difficulty == type)
			{
				return data2;
			}
		}
		return null;
	}

	public Data[] GetGroupData(int groupId)
	{
		if (!Singleton<RegionTable>.IsValid())
		{
			return null;
		}
		Data[] data = GetData();
		List<Data> list = new List<Data>();
		Data[] array = data;
		foreach (Data data2 in array)
		{
			if (data2.groupId == groupId)
			{
				list.Add(data2);
			}
		}
		return list.ToArray();
	}

	public int GetMapNo(int regionId)
	{
		if (!Singleton<RegionTable>.IsValid())
		{
			return 0;
		}
		Data data = GetData((uint)regionId);
		if (data == null)
		{
			return 0;
		}
		if (data.regionId >= 100)
		{
			return 0;
		}
		int item = regionId;
		if (data.difficulty != 0)
		{
			Data data2 = GetData((int)data.regionId, REGION_DIFFICULTY_TYPE.NORMAL);
			if (data2 == null)
			{
				return 0;
			}
			item = (int)data2.regionId;
		}
		List<int> list = new List<int>();
		Data[] data3 = GetData();
		Data[] array = data3;
		foreach (Data data4 in array)
		{
			if (data4.regionId < 100 && data4.difficulty == REGION_DIFFICULTY_TYPE.NORMAL)
			{
				list.Add((int)data4.regionId);
			}
		}
		if (list.Contains(item))
		{
			int num = list.IndexOf(regionId);
			if (data.worldId == 2)
			{
				num -= 9;
			}
			return num + 1;
		}
		return 0;
	}
}
