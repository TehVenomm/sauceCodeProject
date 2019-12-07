using System;
using System.Collections.Generic;

public class FieldMapEnemyPopTimeZoneTable : Singleton<FieldMapEnemyPopTimeZoneTable>, IDataTable
{
	public class FieldMapEnemyPopTimeZoneData
	{
		public uint id;

		public string startTime;

		public string endTime;

		public int enemyId;

		public int mapId;

		public uint existStrId;

		public uint goneStrId;

		public const string NT = "id,startTime,endTime,enemyId,mapId,existStrId,goneStrId";

		public static bool cb(CSVReader csv_reader, FieldMapEnemyPopTimeZoneData data, ref uint key)
		{
			data.id = key;
			csv_reader.Pop(ref data.startTime);
			csv_reader.Pop(ref data.endTime);
			csv_reader.Pop(ref data.enemyId);
			csv_reader.Pop(ref data.mapId);
			csv_reader.Pop(ref data.existStrId);
			csv_reader.Pop(ref data.goneStrId);
			return true;
		}

		public bool TryGetStartTime(out DateTime result)
		{
			return DateTime.TryParse(startTime, out result);
		}

		public bool TryGetEndTime(out DateTime result)
		{
			return DateTime.TryParse(endTime, out result);
		}
	}

	private UIntKeyTable<FieldMapEnemyPopTimeZoneData> timeZoneDataTable;

	public void CreateTable(string csv_text)
	{
		timeZoneDataTable = TableUtility.CreateUIntKeyTable<FieldMapEnemyPopTimeZoneData>(csv_text, FieldMapEnemyPopTimeZoneData.cb, "id,startTime,endTime,enemyId,mapId,existStrId,goneStrId");
	}

	public void CreateTable(string csv_text, TableUtility.Progress progress)
	{
		timeZoneDataTable = TableUtility.CreateUIntKeyTable<FieldMapEnemyPopTimeZoneData>(csv_text, FieldMapEnemyPopTimeZoneData.cb, "id,startTime,endTime,enemyId,mapId,existStrId,goneStrId", progress);
		timeZoneDataTable.TrimExcess();
	}

	public void AddTable(string csv_text)
	{
		TableUtility.AddUIntKeyTable(timeZoneDataTable, csv_text, FieldMapEnemyPopTimeZoneData.cb, "id,startTime,endTime,enemyId,mapId,existStrId,goneStrId");
	}

	public List<FieldMapEnemyPopTimeZoneData> GetEnemyTimeZoneDataList(int mapId)
	{
		if (timeZoneDataTable == null)
		{
			return null;
		}
		List<FieldMapEnemyPopTimeZoneData> list = new List<FieldMapEnemyPopTimeZoneData>();
		timeZoneDataTable.ForEach(delegate(FieldMapEnemyPopTimeZoneData data)
		{
			if (data.mapId == mapId)
			{
				list.Add(data);
			}
		});
		return list;
	}

	public bool TryGetEnableLastEndTime(int mapId, out FieldMapEnemyPopTimeZoneData resultTimeZone, out ENEMY_POP_TYPE resultType)
	{
		resultTimeZone = null;
		resultType = ENEMY_POP_TYPE.RARE_SPECIES;
		if (!MonoBehaviourSingleton<FieldManager>.IsValid())
		{
			return false;
		}
		List<FieldMapEnemyPopTimeZoneData> enemyTimeZoneDataList = GetEnemyTimeZoneDataList(mapId);
		if (enemyTimeZoneDataList == null || enemyTimeZoneDataList.Count <= 0)
		{
			return false;
		}
		List<FieldMapTable.EnemyPopTableData> rareOrBossEnemyList = Singleton<FieldMapTable>.I.GetRareOrBossEnemyList(mapId);
		if (rareOrBossEnemyList == null || rareOrBossEnemyList.Count <= 0)
		{
			return false;
		}
		bool result = false;
		DateTime minValue = DateTime.MinValue;
		if (!MonoBehaviourSingleton<FieldManager>.I.fieldData.field.TryGetCreatedAt(out DateTime createdAt))
		{
			return false;
		}
		DateTime now = TimeManager.GetNow();
		int i = 0;
		for (int count = enemyTimeZoneDataList.Count; i < count; i++)
		{
			FieldMapEnemyPopTimeZoneData fieldMapEnemyPopTimeZoneData = enemyTimeZoneDataList[i];
			if (!fieldMapEnemyPopTimeZoneData.TryGetStartTime(out DateTime result2) || !fieldMapEnemyPopTimeZoneData.TryGetEndTime(out DateTime result3))
			{
				continue;
			}
			result2 = TimeManager.CombineDateAndTime(createdAt, result2);
			result3 = TimeManager.CombineDateAndTime(createdAt, result3);
			if (createdAt >= result2 && now <= result3)
			{
				FieldMapTable.EnemyPopTableData enemyPopTableData = FindEnemyPopData(rareOrBossEnemyList, mapId, fieldMapEnemyPopTimeZoneData.enemyId);
				if (enemyPopTableData != null && (result3 > minValue || IsPreferredType(resultType, enemyPopTableData)))
				{
					result = true;
					resultType = enemyPopTableData.enemyPopType;
					resultTimeZone = fieldMapEnemyPopTimeZoneData;
				}
			}
		}
		return result;
	}

	private bool IsPreferredType(ENEMY_POP_TYPE now, FieldMapTable.EnemyPopTableData popData)
	{
		if (now == ENEMY_POP_TYPE.RARE_SPECIES && popData.enemyPopType == ENEMY_POP_TYPE.FIELD_BOSS)
		{
			return true;
		}
		return false;
	}

	private FieldMapTable.EnemyPopTableData FindEnemyPopData(List<FieldMapTable.EnemyPopTableData> specialEnemyList, int mapId, int enemyId)
	{
		int i = 0;
		for (int count = specialEnemyList.Count; i < count; i++)
		{
			FieldMapTable.EnemyPopTableData enemyPopTableData = specialEnemyList[i];
			if (enemyPopTableData.mapID == mapId && enemyPopTableData.enemyID == enemyId)
			{
				return enemyPopTableData;
			}
		}
		return null;
	}
}
