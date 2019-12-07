using System;
using System.Collections.Generic;

public class QuestToFieldTable : Singleton<QuestToFieldTable>, IDataTable
{
	public class QuestToFieldData
	{
		public uint questId;

		public uint mapId;

		public uint eventId;

		public int grade;

		public const string NT = "questId,mapId,eventId";

		public static bool cb(CSVReader csv_reader, QuestToFieldData data, ref uint key1, ref uint key2)
		{
			data.questId = key1;
			csv_reader.Pop(ref data.mapId);
			csv_reader.Pop(ref data.eventId);
			return true;
		}

		public static string CBSecondKey(CSVReader csv, int table_data_num)
		{
			return table_data_num.ToString();
		}
	}

	private DoubleUIntKeyTable<QuestToFieldData> questToItemTable;

	public void CreateTable(string csv_text)
	{
		questToItemTable = TableUtility.CreateDoubleUIntKeyTable<QuestToFieldData>(csv_text, QuestToFieldData.cb, "questId,mapId,eventId", QuestToFieldData.CBSecondKey);
		questToItemTable.TrimExcess();
	}

	public void AddTable(string csv_text)
	{
		TableUtility.AddDoubleUIntKeyTable(questToItemTable, csv_text, QuestToFieldData.cb, "questId,mapId,eventId", QuestToFieldData.CBSecondKey);
	}

	public void InitDependencyData()
	{
		if (Singleton<FieldMapTable>.IsValid())
		{
			questToItemTable.ForEach(delegate(UIntKeyTable<QuestToFieldData> x)
			{
				x.ForEach(delegate(QuestToFieldData data)
				{
					FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(data.mapId);
					if (fieldMapData != null)
					{
						data.grade = fieldMapData.grade;
					}
				});
			});
		}
	}

	public bool IsValidHappenQuest(uint questId)
	{
		if (questToItemTable == null)
		{
			return false;
		}
		UIntKeyTable<QuestToFieldData> uIntKeyTable = questToItemTable.Get(questId);
		if (uIntKeyTable == null)
		{
			return false;
		}
		Version applicationVersion = NetworkNative.getNativeVersionFromName();
		int field_count = 0;
		uIntKeyTable.ForEach(delegate(QuestToFieldData data)
		{
			if (data.mapId != 0 && (data.eventId == 0 || MonoBehaviourSingleton<QuestManager>.I.IsEventPlayableWith((int)data.eventId, applicationVersion)))
			{
				FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(data.mapId);
				if (fieldMapData != null && (!fieldMapData.IsEventData || MonoBehaviourSingleton<QuestManager>.I.IsEventPlayableWith(fieldMapData.eventId, applicationVersion)))
				{
					field_count++;
				}
			}
		});
		return field_count > 0;
	}

	public FieldMapTable.FieldMapTableData[] GetFieldMapTableFromQuestIdWithClosedField(uint questId)
	{
		return GetFieldMapTableFromQuestId(questId, hasIncludelockedField: true);
	}

	public FieldMapTable.FieldMapTableData[] GetFieldMapTableFromQuestId(uint questId, bool hasIncludelockedField = false)
	{
		if (questToItemTable == null)
		{
			return null;
		}
		UIntKeyTable<QuestToFieldData> uIntKeyTable = questToItemTable.Get(questId);
		if (uIntKeyTable == null)
		{
			return null;
		}
		List<FieldMapTable.FieldMapTableData> list = new List<FieldMapTable.FieldMapTableData>();
		uIntKeyTable.ForEach(delegate(QuestToFieldData data)
		{
			if (data.mapId != 0)
			{
				FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(data.mapId);
				if (fieldMapData != null && (fieldMapData.IsEventData || hasIncludelockedField || Singleton<ItemToFieldTable>.I.IsOpenMap(fieldMapData)))
				{
					list.Add(fieldMapData);
				}
			}
		});
		if (list.Count <= 0)
		{
			return null;
		}
		return list.ToArray();
	}

	public List<uint> GetQuestIdList(uint mapId)
	{
		List<uint> list = new List<uint>();
		questToItemTable.ForEach(delegate(UIntKeyTable<QuestToFieldData> table)
		{
			table.ForEach(delegate(QuestToFieldData data)
			{
				if (data.mapId == mapId)
				{
					list.Add(data.questId);
				}
			});
		});
		return list;
	}

	public Dictionary<uint, uint> GetQuestIdEventIdDic(uint mapId)
	{
		Dictionary<uint, uint> dic = new Dictionary<uint, uint>();
		questToItemTable.ForEach(delegate(UIntKeyTable<QuestToFieldData> table)
		{
			table.ForEach(delegate(QuestToFieldData data)
			{
				if (data.mapId == mapId)
				{
					dic[data.questId] = data.eventId;
				}
			});
		});
		return dic;
	}
}
