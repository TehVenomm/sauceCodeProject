using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemToFieldTable : Singleton<ItemToFieldTable>, IDataTable
{
	public class ItemToFieldData
	{
		public uint itemId;

		public uint fieldId;

		public uint[] enemyId = new uint[5];

		public uint[] pointId = new uint[5];

		public int grade;

		private uint key2;

		public const string NT = "itemId,fieldId,enemyId_0,enemyId_1,enemyId_2,enemyId_3,enemyId_4,pointId_0,pointId_1,pointId_2,pointId_3,pointId_4";

		public static bool cb(CSVReader csv_reader, ItemToFieldData data, ref uint key1, ref uint key2)
		{
			data.itemId = key1;
			data.key2 = key2;
			csv_reader.Pop(ref data.fieldId);
			for (int i = 0; i < 5; i++)
			{
				csv_reader.Pop(ref data.enemyId[i]);
				if (data.enemyId[i] != 0 && Singleton<EnemyFieldDropItemTable>.IsValid())
				{
					Singleton<EnemyFieldDropItemTable>.I.Add(data.enemyId[i], data.itemId, data.fieldId, new List<int>
					{
						0
					});
				}
			}
			for (int j = 0; j < 5; j++)
			{
				csv_reader.Pop(ref data.pointId[j]);
			}
			return true;
		}

		public static string CBSecondKey(CSVReader csv, int table_data_num)
		{
			return table_data_num.ToString();
		}

		public uint GetSecondKey()
		{
			return key2;
		}
	}

	public class CandidateField
	{
		public uint enemyId;

		public FieldMapTable.FieldMapTableData mapData;
	}

	public enum FIELD_AVAILABLE_CHOICES
	{
		AVAILABLE = 0,
		TOO_BIG_GRADE = -1,
		NOT_AVILABLE_MANAGER = -2,
		NOT_TRAVELED = -3
	}

	public class ItemDetailToFieldData
	{
		public FieldMapTable.FieldMapTableData mapData;
	}

	public class ItemDetailToFieldEnemyData : ItemDetailToFieldData
	{
		public uint[] enemyID;

		public ItemDetailToFieldEnemyData(FieldMapTable.FieldMapTableData map, uint[] enemy)
		{
			mapData = map;
			enemyID = enemy;
		}
	}

	public class ItemDetailToFieldPointData : ItemDetailToFieldData
	{
		public uint pointID;

		public FieldMapTable.GatherPointTableData pointTable;

		public FieldMapTable.GatherPointViewTableData pointViewTable;

		public ItemDetailToFieldPointData(FieldMapTable.FieldMapTableData map, uint point, FieldMapTable.GatherPointTableData point_table, FieldMapTable.GatherPointViewTableData point_view_table)
		{
			mapData = map;
			pointID = point;
			pointTable = point_table;
			pointViewTable = point_view_table;
		}
	}

	public class RecommendFieldData
	{
		public ItemDetailToFieldData[] dropFieldData;

		public bool isNeedUnknownField;

		public RecommendFieldData(ItemDetailToFieldData[] _data, bool find_unknown)
		{
			dropFieldData = _data;
			isNeedUnknownField = find_unknown;
		}
	}

	public const int ENEMY_NUM_MAX = 5;

	public const int GATHER_POINT_NUM_MAX = 5;

	private DoubleUIntKeyTable<ItemToFieldData> itemToFieldTable;

	public void CreateTable(string csv_text)
	{
		itemToFieldTable = TableUtility.CreateDoubleUIntKeyTable<ItemToFieldData>(csv_text, ItemToFieldData.cb, "itemId,fieldId,enemyId_0,enemyId_1,enemyId_2,enemyId_3,enemyId_4,pointId_0,pointId_1,pointId_2,pointId_3,pointId_4", ItemToFieldData.CBSecondKey);
		itemToFieldTable.TrimExcess();
	}

	public void AddTable(string csv_text)
	{
		TableUtility.AddDoubleUIntKeyTable(itemToFieldTable, csv_text, ItemToFieldData.cb, "itemId,fieldId,enemyId_0,enemyId_1,enemyId_2,enemyId_3,enemyId_4,pointId_0,pointId_1,pointId_2,pointId_3,pointId_4", ItemToFieldData.CBSecondKey);
	}

	public void InitDependencyData()
	{
		if (Singleton<FieldMapTable>.IsValid())
		{
			itemToFieldTable.ForEach(delegate(UIntKeyTable<ItemToFieldData> x)
			{
				x.ForEach(delegate(ItemToFieldData data)
				{
					FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(data.fieldId);
					if (fieldMapData != null)
					{
						data.grade = fieldMapData.grade;
					}
				});
			});
		}
	}

	public ItemDetailToFieldData[] GetFieldTableFromItemID(uint item_id, out bool find_unknown_field, bool isExcludeNotPlayable = false)
	{
		find_unknown_field = false;
		if (itemToFieldTable == null)
		{
			return null;
		}
		UIntKeyTable<ItemToFieldData> uIntKeyTable = itemToFieldTable.Get(item_id);
		if (uIntKeyTable == null)
		{
			return null;
		}
		bool temp_find_unknown = false;
		List<ItemDetailToFieldData> list = new List<ItemDetailToFieldData>();
		List<uint> icon_id_list = new List<uint>();
		uIntKeyTable.ForEach(delegate(ItemToFieldData data)
		{
			if (data.enemyId != null && data.enemyId.Length != 0)
			{
				FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(data.fieldId);
				if (fieldMapData != null)
				{
					if (IsOpenMap(fieldMapData))
					{
						if (data.enemyId[0] != 0)
						{
							list.Add(new ItemDetailToFieldEnemyData(fieldMapData, data.enemyId));
						}
						int i = 0;
						for (int num = data.pointId.Length; i < num; i++)
						{
							if (data.pointId[i] != 0)
							{
								FieldMapTable.GatherPointTableData gatherPointData = Singleton<FieldMapTable>.I.GetGatherPointData(data.pointId[i]);
								if (gatherPointData != null)
								{
									FieldMapTable.GatherPointViewTableData gatherPointViewData = Singleton<FieldMapTable>.I.GetGatherPointViewData(gatherPointData.viewID);
									if (gatherPointViewData != null && gatherPointViewData.iconID != 0 && icon_id_list.IndexOf(gatherPointViewData.iconID) < 0 && (!isExcludeNotPlayable || IsPlayableField(fieldMapData)))
									{
										icon_id_list.Add(gatherPointViewData.iconID);
										list.Add(new ItemDetailToFieldPointData(fieldMapData, data.pointId[i], gatherPointData, gatherPointViewData));
									}
								}
							}
						}
					}
					else
					{
						temp_find_unknown = true;
					}
				}
			}
		});
		find_unknown_field = temp_find_unknown;
		if (list.Count == 0)
		{
			return null;
		}
		return list.ToArray();
	}

	public CandidateField[] GetCandidateField(uint item_id, int trim_count = -1, bool isExcludeNotPlayable = false)
	{
		List<CandidateField> ret = new List<CandidateField>();
		List<string> enemy_names = new List<string>();
		UIntKeyTable<ItemToFieldData> uIntKeyTable = itemToFieldTable.Get(item_id);
		if (uIntKeyTable == null)
		{
			return null;
		}
		uIntKeyTable.ForEach(delegate(ItemToFieldData data)
		{
			FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(data.fieldId);
			if (data.enemyId != null)
			{
				uint[] enemyId = data.enemyId;
				foreach (uint num in enemyId)
				{
					EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData(num);
					if (enemyData != null && (!isExcludeNotPlayable || IsPlayableField(fieldMapData)) && !enemy_names.Contains(enemyData.name))
					{
						ret.Add(new CandidateField
						{
							enemyId = num,
							mapData = fieldMapData
						});
						enemy_names.Add(enemyData.name);
					}
				}
			}
		});
		return ret.ToArray();
	}

	private bool IsPlayableField(FieldMapTable.FieldMapTableData field_table)
	{
		if (!field_table.IsEventData)
		{
			return true;
		}
		if (MonoBehaviourSingleton<QuestManager>.I.IsPlayableVersionEvent(field_table.eventId))
		{
			return true;
		}
		return false;
	}

	public uint[] GetCandidateEnemies(uint item_id, int trim_count = -1)
	{
		List<uint> enemy_ids = new List<uint>();
		List<string> enemy_names = new List<string>();
		UIntKeyTable<ItemToFieldData> uIntKeyTable = itemToFieldTable.Get(item_id);
		if (uIntKeyTable == null)
		{
			return null;
		}
		uIntKeyTable.ForEach(delegate(ItemToFieldData data)
		{
			if (data.enemyId != null)
			{
				uint[] enemyId = data.enemyId;
				foreach (uint num in enemyId)
				{
					EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData(num);
					if (enemyData != null && !enemy_names.Contains(enemyData.name))
					{
						enemy_ids.Add(num);
						enemy_names.Add(enemyData.name);
					}
				}
			}
		});
		if (trim_count > 0)
		{
			return enemy_ids.GetRange(0, Mathf.Min(enemy_ids.Count, trim_count)).ToArray();
		}
		return enemy_ids.ToArray();
	}

	public RecommendFieldData GetRecommendField(uint item_id, int max_num, bool isExcludeNotPlayable = false)
	{
		RecommendFieldData recommendFieldData = new RecommendFieldData(null, find_unknown: false);
		bool find_unknown_field = false;
		ItemDetailToFieldData[] array = GetFieldTableFromItemID(item_id, out find_unknown_field, isExcludeNotPlayable);
		if (array == null || array.Length == 0 || max_num <= 0)
		{
			QuestTable.QuestTableData[] array2 = null;
			if (Singleton<ItemToQuestTable>.IsValid())
			{
				array2 = Singleton<ItemToQuestTable>.I.GetHappenQuestTableFromItemID(item_id);
			}
			if (array2 != null && array2.Length != 0)
			{
				recommendFieldData.isNeedUnknownField = true;
			}
			else
			{
				recommendFieldData.isNeedUnknownField = find_unknown_field;
			}
			return recommendFieldData;
		}
		if (array.Length <= max_num)
		{
			recommendFieldData.dropFieldData = array;
			return recommendFieldData;
		}
		Array.Sort(array, delegate(ItemDetailToFieldData l, ItemDetailToFieldData r)
		{
			int num = r.mapData.grade - l.mapData.grade;
			if (num == 0)
			{
				num = (int)(r.mapData.mapID - l.mapData.mapID);
			}
			return num;
		});
		Array.Resize(ref array, max_num);
		recommendFieldData.dropFieldData = array;
		return recommendFieldData;
	}

	public FIELD_AVAILABLE_CHOICES IsAvailableMap(FieldMapTable.FieldMapTableData field_table)
	{
		_IsOpenMap(field_table, out FIELD_AVAILABLE_CHOICES available_choices);
		return available_choices;
	}

	public bool IsOpenMap(FieldMapTable.FieldMapTableData field_table)
	{
		FIELD_AVAILABLE_CHOICES available_choices;
		return _IsOpenMap(field_table, out available_choices);
	}

	private bool _IsOpenMap(FieldMapTable.FieldMapTableData field_table, out FIELD_AVAILABLE_CHOICES available_choices)
	{
		if (field_table.IsEventData)
		{
			List<Network.EventData> list = new List<Network.EventData>(MonoBehaviourSingleton<QuestManager>.I.eventList);
			list.RemoveAll((Network.EventData e) => e.HasEndDate() && e.GetRest() < 0);
			list.RemoveAll((Network.EventData e) => !e.enableEvent);
			if (list.Find((Network.EventData e) => e.eventId == field_table.eventId) != null)
			{
				available_choices = FIELD_AVAILABLE_CHOICES.AVAILABLE;
				return true;
			}
			available_choices = FIELD_AVAILABLE_CHOICES.NOT_TRAVELED;
			return false;
		}
		if (field_table.grade > MonoBehaviourSingleton<UserInfoManager>.I.userStatus.fieldGrade)
		{
			available_choices = FIELD_AVAILABLE_CHOICES.TOO_BIG_GRADE;
			return false;
		}
		if (!MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledMap((int)field_table.mapID))
		{
			available_choices = FIELD_AVAILABLE_CHOICES.NOT_TRAVELED;
			return false;
		}
		available_choices = FIELD_AVAILABLE_CHOICES.AVAILABLE;
		return true;
	}
}
