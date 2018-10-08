using System.Collections.Generic;
using UnityEngine;

public class ArenaTable : Singleton<ArenaTable>, IDataTable
{
	public class ArenaData
	{
		public const string NT = "id,groupId,rank,limit_0,limit_1,limit_2,condition_0,condition_1,condition_2,timeLimit,level,questId_0,questId_1,questId_2,questId_3,questId_4";

		public int id;

		public ARENA_GROUP group;

		public ARENA_RANK rank;

		public ARENA_LIMIT[] limits;

		public ARENA_CONDITION[] conditions;

		public int timeLimit;

		public int level;

		public int[] questIds;

		public static bool cb(CSVReader csv_reader, ArenaData data, ref uint key)
		{
			data.id = (int)key;
			csv_reader.PopEnum(ref data.group, ARENA_GROUP.E);
			csv_reader.PopEnum(ref data.rank, ARENA_RANK.C);
			data.limits = new ARENA_LIMIT[3];
			for (int i = 0; i < 3; i++)
			{
				csv_reader.PopEnum(ref data.limits[i], ARENA_LIMIT.NONE);
			}
			data.conditions = new ARENA_CONDITION[3];
			for (int j = 0; j < 3; j++)
			{
				csv_reader.PopEnum(ref data.conditions[j], ARENA_CONDITION.NONE);
			}
			csv_reader.Pop(ref data.timeLimit);
			csv_reader.Pop(ref data.level);
			data.questIds = new int[5];
			for (int k = 0; k < 5; k++)
			{
				csv_reader.Pop(ref data.questIds[k]);
			}
			return true;
		}

		public List<QuestTable.QuestTableData> GetQuestDataArray()
		{
			List<QuestTable.QuestTableData> list = new List<QuestTable.QuestTableData>(5);
			int i = 0;
			for (int num = 5; i < num; i++)
			{
				int num2 = questIds[i];
				if (num2 > 0)
				{
					QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData((uint)num2);
					if (questData == null)
					{
						Debug.LogError((object)("ArenaTableに存在しないクエストId: " + questIds[i] + "が設定されています"));
					}
					else
					{
						list.Add(questData);
					}
				}
			}
			return list;
		}
	}

	public const int WAVE_NUM = 5;

	public const int LIMIT_NUM = 3;

	public const int CONDITION_NUM = 3;

	private UIntKeyTable<ArenaData> arenaDataTable;

	public void CreateTable(string csv_text)
	{
		arenaDataTable = TableUtility.CreateUIntKeyTable<ArenaData>(csv_text, ArenaData.cb, "id,groupId,rank,limit_0,limit_1,limit_2,condition_0,condition_1,condition_2,timeLimit,level,questId_0,questId_1,questId_2,questId_3,questId_4", null);
		arenaDataTable.TrimExcess();
	}

	public ArenaData GetArenaData(int arenaId)
	{
		return arenaDataTable.Get((uint)arenaId);
	}
}
