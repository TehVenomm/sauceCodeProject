using Network;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemToQuestTable : Singleton<ItemToQuestTable>, IDataTable
{
	public class ItemToQuestData
	{
		public const string NT = "itemId,questId";

		public uint itemId;

		public uint questId;

		public int grade;

		private uint key2;

		public static bool cb(CSVReader csv_reader, ItemToQuestData data, ref uint key1, ref uint key2)
		{
			data.itemId = key1;
			data.key2 = key2;
			csv_reader.Pop(ref data.questId);
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

		public void LoadFromBinary(BinaryTableReader reader, ref uint key1, ref uint key2)
		{
			itemId = key1;
			this.key2 = key2;
			questId = reader.ReadUInt32(0u);
		}

		public void LoadFromAPI(uint iId, uint qId, uint k2)
		{
			itemId = iId;
			questId = qId;
			key2 = k2;
		}

		public void DumpBinary(BinaryWriter writer)
		{
			writer.Write(questId);
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			ItemToQuestData itemToQuestData = obj as ItemToQuestData;
			if (itemToQuestData == null)
			{
				return false;
			}
			return itemId == itemToQuestData.itemId && questId == itemToQuestData.questId && grade == itemToQuestData.grade && key2 == itemToQuestData.key2;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}

	public class RecommendQuestData
	{
		public QuestTable.QuestTableData[] recommendData;

		public bool isNeedUnknownQuest;

		public RecommendQuestData(QuestTable.QuestTableData[] table, bool need_unknown)
		{
			recommendData = table;
			isNeedUnknownQuest = need_unknown;
		}
	}

	public enum QUEST_AVAILABLE_CHOICES
	{
		AVAILABLE = 0,
		NOT_FOUND_LOCATION_DATA = -1,
		TOO_BIG_GRADE = -2,
		NOT_CLEAR_FOR_VISIBLE_REQUIRE_QUEST = -3,
		NOT_AVILABLE_MANAGER = -4,
		NOT_FOUND_AREA_DATA = -5,
		NOT_SELECT_TYPE = -6
	}

	private const int CHOICE_NUM = 2;

	private DoubleUIntKeyTable<ItemToQuestData> itemToQuestTable;

	public static DoubleUIntKeyTable<ItemToQuestData> CreateTableCSV(string csv_text)
	{
		return TableUtility.CreateDoubleUIntKeyTable<ItemToQuestData>(csv_text, ItemToQuestData.cb, "itemId,questId", ItemToQuestData.CBSecondKey, null, null, null);
	}

	public void CreateTable(string csv_text)
	{
		itemToQuestTable = CreateTableCSV(csv_text);
	}

	public void CreateTable(string csv_text, TableUtility.Progress progress)
	{
		itemToQuestTable = TableUtility.CreateDoubleUIntKeyTable<ItemToQuestData>(csv_text, ItemToQuestData.cb, "itemId,questId", ItemToQuestData.CBSecondKey, null, null, progress);
	}

	public void AddTable(string csv_text)
	{
		TableUtility.AddDoubleUIntKeyTable(itemToQuestTable, csv_text, ItemToQuestData.cb, "itemId,questId", ItemToQuestData.CBSecondKey, null, null);
	}

	public static DoubleUIntKeyTable<ItemToQuestData> CreateTableBinary(byte[] bytes)
	{
		DoubleUIntKeyTable<ItemToQuestData> doubleUIntKeyTable = new DoubleUIntKeyTable<ItemToQuestData>();
		BinaryTableReader binaryTableReader = new BinaryTableReader(bytes);
		while (binaryTableReader.MoveNext())
		{
			uint key = binaryTableReader.ReadUInt32(0u);
			uint key2 = 0u;
			UIntKeyTable<ItemToQuestData> uIntKeyTable = doubleUIntKeyTable.Get(key);
			if (uIntKeyTable != null)
			{
				key2 = (uint)uIntKeyTable.GetCount();
			}
			ItemToQuestData itemToQuestData = new ItemToQuestData();
			itemToQuestData.LoadFromBinary(binaryTableReader, ref key, ref key2);
			doubleUIntKeyTable.Add(key, key2, itemToQuestData);
		}
		return doubleUIntKeyTable;
	}

	public void CreateTable(byte[] bytes)
	{
		itemToQuestTable = CreateTableBinary(bytes);
	}

	public void AddTableFromAPI(uint itemId, List<int> questIds)
	{
		if (itemToQuestTable == null)
		{
			itemToQuestTable = new DoubleUIntKeyTable<ItemToQuestData>();
		}
		itemToQuestTable.Get(itemId)?.Clear();
		for (int i = 0; i < questIds.Count; i++)
		{
			ItemToQuestData itemToQuestData = new ItemToQuestData();
			itemToQuestData.LoadFromAPI(itemId, (uint)questIds[i], (uint)i);
			itemToQuestTable.Add(itemId, (uint)i, itemToQuestData);
		}
		InitDependencyData();
	}

	public void InitDependencyData()
	{
		if (Singleton<QuestTable>.IsValid())
		{
			itemToQuestTable.ForEach(delegate(UIntKeyTable<ItemToQuestData> x)
			{
				x.ForEach(delegate(ItemToQuestData data)
				{
					QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(data.questId);
					if (questData != null)
					{
						data.grade = questData.grade;
					}
				});
			});
		}
	}

	public QuestTable.QuestTableData[] GetQuestTableFromItemID(uint item_id)
	{
		return _GetQuestTableFromItemID(item_id, null);
	}

	public uint[] GetCandidateEnemies(uint item_id, int trim_count = -1)
	{
		List<uint> list = new List<uint>();
		List<string> list2 = new List<string>();
		QuestTable.QuestTableData[] happenQuestTableFromItemID = Singleton<ItemToQuestTable>.I.GetHappenQuestTableFromItemID(item_id);
		if (happenQuestTableFromItemID != null && happenQuestTableFromItemID.Length > 0)
		{
			QuestTable.QuestTableData[] array = happenQuestTableFromItemID;
			foreach (QuestTable.QuestTableData questTableData in array)
			{
				uint mainEnemyID = (uint)questTableData.GetMainEnemyID();
				EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData(mainEnemyID);
				if (enemyData != null && !list2.Contains(enemyData.name))
				{
					list.Add(mainEnemyID);
					list2.Add(enemyData.name);
				}
			}
		}
		if (trim_count > 0)
		{
			return list.GetRange(0, Mathf.Min(list.Count, trim_count)).ToArray();
		}
		return list.ToArray();
	}

	public QuestTable.QuestTableData[] GetDistinctQuestFromItemID(uint item_id, QUEST_TYPE? quest_type = default(QUEST_TYPE?))
	{
		return _GetDistinctQuestFromItemID(item_id, quest_type);
	}

	private QuestTable.QuestTableData[] _GetDistinctQuestFromItemID(uint item_id, QUEST_TYPE? quest_type = default(QUEST_TYPE?))
	{
		QuestTable.QuestTableData[] array = _GetQuestTableFromItemID(item_id, quest_type);
		if (array == null)
		{
			return null;
		}
		List<QuestTable.QuestTableData> list = new List<QuestTable.QuestTableData>();
		List<string> list2 = new List<string>();
		QuestTable.QuestTableData[] array2 = array;
		foreach (QuestTable.QuestTableData questTableData in array2)
		{
			string enemyName = Singleton<EnemyTable>.I.GetEnemyName((uint)questTableData.GetMainEnemyID());
			if (!string.IsNullOrEmpty(enemyName) && !list2.Contains(enemyName))
			{
				list2.Add(enemyName);
				list.Add(questTableData);
			}
		}
		return list.ToArray();
	}

	public QuestTable.QuestTableData[] GetDistinctHappenQuestFromItemID(uint item_id)
	{
		return _GetDistinctQuestFromItemID(item_id, QUEST_TYPE.HAPPEN);
	}

	public QuestTable.QuestTableData[] GetHappenQuestTableFromItemID(uint item_id)
	{
		return _GetQuestTableFromItemID(item_id, QUEST_TYPE.HAPPEN);
	}

	private QuestTable.QuestTableData[] _GetQuestTableFromItemID(uint item_id, QUEST_TYPE? quest_type = default(QUEST_TYPE?))
	{
		if (itemToQuestTable == null)
		{
			return null;
		}
		UIntKeyTable<ItemToQuestData> uIntKeyTable = itemToQuestTable.Get(item_id);
		if (uIntKeyTable == null)
		{
			return null;
		}
		List<QuestTable.QuestTableData> list = new List<QuestTable.QuestTableData>();
		uIntKeyTable.ForEach(delegate(ItemToQuestData data)
		{
			QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(data.questId);
			if (questData != null && (!quest_type.HasValue || (quest_type.Value == questData.questType && (quest_type.Value != QUEST_TYPE.HAPPEN || (Singleton<QuestToFieldTable>.IsValid() && Singleton<QuestToFieldTable>.I.IsValidHappenQuest(data.questId))))))
			{
				list.Add(questData);
			}
		});
		if (list.Count == 0)
		{
			return null;
		}
		return list.ToArray();
	}

	public RecommendQuestData GetRecommendQuest(uint item_id)
	{
		QuestTable.QuestTableData[] array = new QuestTable.QuestTableData[2];
		for (int i = 0; i < 2; i++)
		{
			array[i] = null;
		}
		RecommendQuestData recommendQuestData = new RecommendQuestData(array, false);
		QuestTable.QuestTableData[] questTableFromItemID = GetQuestTableFromItemID(item_id);
		if (questTableFromItemID == null)
		{
			return recommendQuestData;
		}
		bool find_unknown_quest = false;
		QuestTable.QuestTableData[] A = new QuestTable.QuestTableData[2];
		QuestTable.QuestTableData[] B = new QuestTable.QuestTableData[2];
		for (int j = 0; j < 2; j++)
		{
			A[j] = null;
			B[j] = null;
		}
		Array.ForEach(questTableFromItemID, delegate(QuestTable.QuestTableData data)
		{
			bool find_unknown_quest2 = false;
			switch (data.questType)
			{
			default:
				return;
			case QUEST_TYPE.NORMAL:
			case QUEST_TYPE.EVENT:
				UpdateRecommendQuestPriority(A, data, false, out find_unknown_quest2);
				break;
			case QUEST_TYPE.ORDER:
				UpdateRecommendQuestPriority(B, data, true, out find_unknown_quest2);
				break;
			}
			if (find_unknown_quest2)
			{
				find_unknown_quest = true;
			}
		});
		bool flag = A != null && A.Length > 0 && A[0] != null;
		bool flag2 = flag && A != null && A.Length > 1 && A[1] != null;
		bool flag3 = B != null && B.Length > 0 && B[0] != null;
		bool flag4 = flag3 && B != null && B.Length > 1 && B[1] != null;
		int num = 0;
		if (flag)
		{
			recommendQuestData.recommendData[num++] = A[0];
		}
		if (flag3)
		{
			recommendQuestData.recommendData[num++] = B[0];
		}
		if (flag2 && num < 2)
		{
			recommendQuestData.recommendData[num++] = A[1];
		}
		if (flag4 && num < 2)
		{
			recommendQuestData.recommendData[num++] = B[1];
		}
		if (!flag && !flag2 && find_unknown_quest)
		{
			recommendQuestData.isNeedUnknownQuest = true;
		}
		return recommendQuestData;
	}

	private void UpdateRecommendQuestPriority(QuestTable.QuestTableData[] quest_table, QuestTable.QuestTableData _table, bool is_order, out bool find_unknown_quest)
	{
		find_unknown_quest = false;
		if (!is_order && !IsOpenedQuest(_table))
		{
			find_unknown_quest = true;
		}
		else
		{
			if (is_order)
			{
				QuestItemInfo questItem = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem(_table.questID);
				if (questItem == null)
				{
					return;
				}
				int questNum = QuestTable.GetQuestNum(questItem);
				if (questNum <= 0)
				{
					return;
				}
			}
			for (int i = 0; i < 2; i++)
			{
				if (quest_table[i] == null)
				{
					quest_table[i] = _table;
					if (i != 0)
					{
						SortRecommendQuest(quest_table, _table, is_order);
					}
					return;
				}
			}
			int num = 1;
			if (IsSelectPriorityQuestInfo(quest_table[num], _table))
			{
				SortRecommendQuest(quest_table, _table, is_order);
			}
		}
	}

	private bool IsSelectPriorityQuestInfo(QuestTable.QuestTableData now_info, QuestTable.QuestTableData check_data)
	{
		if (now_info == null)
		{
			return true;
		}
		bool result = false;
		if (GetTypePriority(now_info.questType) == GetTypePriority(check_data.questType) && now_info.grade <= check_data.grade)
		{
			if (now_info.grade < check_data.grade || ((now_info.questID > check_data.questID) & (now_info.grade == check_data.grade)))
			{
				result = true;
			}
		}
		else if (GetTypePriority(now_info.questType) < GetTypePriority(check_data.questType) && now_info.grade <= check_data.grade)
		{
			result = true;
		}
		else if (GetTypePriority(now_info.questType) > GetTypePriority(check_data.questType) && now_info.grade < check_data.grade)
		{
			result = true;
		}
		return result;
	}

	private int GetTypePriority(QUEST_TYPE now_type)
	{
		int result = 0;
		switch (now_type)
		{
		case QUEST_TYPE.NORMAL:
			result = 1;
			break;
		case QUEST_TYPE.EVENT:
			result = 2;
			break;
		}
		return result;
	}

	private void SortRecommendQuest(QuestTable.QuestTableData[] quest_table, QuestTable.QuestTableData _table, bool is_order)
	{
		for (int num = quest_table.Length; num > 0; num--)
		{
			QuestTable.QuestTableData questTableData = null;
			QuestTable.QuestTableData questTableData2 = null;
			if (num == quest_table.Length)
			{
				questTableData = quest_table[num - 1];
				questTableData2 = _table;
			}
			else
			{
				questTableData = quest_table[num - 1];
				questTableData2 = quest_table[num];
			}
			if (questTableData != null && questTableData2 != null)
			{
				bool flag = false;
				if (is_order)
				{
					if (questTableData.rarity == questTableData2.rarity)
					{
						if (questTableData.questID > questTableData2.questID)
						{
							flag = true;
						}
					}
					else if (questTableData.rarity > questTableData2.rarity)
					{
						flag = true;
					}
				}
				else if (IsSelectPriorityQuestInfo(questTableData, questTableData2))
				{
					flag = true;
				}
				if (!flag)
				{
					break;
				}
				QuestTable.QuestTableData questTableData3 = questTableData;
				if (num == quest_table.Length)
				{
					quest_table[num - 1] = questTableData2;
				}
				else
				{
					quest_table[num - 1] = quest_table[num];
					quest_table[num] = questTableData3;
				}
			}
		}
	}

	public QUEST_AVAILABLE_CHOICES IsAvailableQuest(QuestTable.QuestTableData quest_table)
	{
		_IsOpenedQuest(quest_table, out QUEST_AVAILABLE_CHOICES available_choices);
		return available_choices;
	}

	private bool IsOpenedQuest(QuestTable.QuestTableData quest_table)
	{
		QUEST_AVAILABLE_CHOICES available_choices;
		return _IsOpenedQuest(quest_table, out available_choices);
	}

	private bool _IsOpenedQuest(QuestTable.QuestTableData quest_table, out QUEST_AVAILABLE_CHOICES available_choices)
	{
		if (quest_table.questType == QUEST_TYPE.NORMAL && quest_table.grade > MonoBehaviourSingleton<UserInfoManager>.I.userStatus.questGrade)
		{
			available_choices = QUEST_AVAILABLE_CHOICES.TOO_BIG_GRADE;
			return false;
		}
		if (quest_table.questType == QUEST_TYPE.HAPPEN)
		{
			ClearStatusQuest clearStatusQuest = MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.Find((ClearStatusQuest data) => data.questId == quest_table.questID);
			if (clearStatusQuest == null || clearStatusQuest.questStatus < 3)
			{
				available_choices = QUEST_AVAILABLE_CHOICES.NOT_SELECT_TYPE;
				return false;
			}
		}
		if (quest_table.questType == QUEST_TYPE.GATE)
		{
			available_choices = QUEST_AVAILABLE_CHOICES.NOT_SELECT_TYPE;
			return false;
		}
		if (quest_table.appearQuestId != 0)
		{
			ClearStatusQuest clearStatusQuest2 = MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.Find((ClearStatusQuest data) => data.questId == quest_table.appearQuestId);
			if (clearStatusQuest2 == null || clearStatusQuest2.questStatus < 3)
			{
				available_choices = QUEST_AVAILABLE_CHOICES.NOT_CLEAR_FOR_VISIBLE_REQUIRE_QUEST;
				return false;
			}
		}
		available_choices = QUEST_AVAILABLE_CHOICES.AVAILABLE;
		return true;
	}
}
