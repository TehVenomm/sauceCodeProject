using Network;
using System;

public class QuestInfoData
{
	public class Mission
	{
		public QuestTable.MissionTableData tableData;

		public CLEAR_STATUS state;

		public Mission(QuestTable.MissionTableData _table, CLEAR_STATUS _state = CLEAR_STATUS.LOCK)
		{
			tableData = _table;
			state = _state;
		}
	}

	public class Quest
	{
		public class Reward
		{
			public int type;

			public int id;

			public int priority;

			public Reward(int _type, int _id, int _priority)
			{
				type = _type;
				id = _id;
				priority = _priority;
			}
		}

		public QuestTable.QuestTableData tableData;

		public int useCrystal;

		public int num;

		public Reward[] reward;

		public Quest(QuestTable.QuestTableData quest_table_data, QuestData quest_list)
		{
			int have_num = (quest_list.order == null) ? 1 : quest_list.order.num;
			Init(quest_table_data, have_num, quest_list.crystalNum, quest_list.reward);
		}

		public Quest(QuestTable.QuestTableData quest_table_data, int have_num, int crystal_num, QuestData.QuestRewardList reward_list)
		{
			Init(quest_table_data, have_num, crystal_num, reward_list);
		}

		private void Init(QuestTable.QuestTableData quest_table_data, int have_num, int crystal_num, QuestData.QuestRewardList reward_list)
		{
			tableData = quest_table_data;
			useCrystal = crystal_num;
			int[] array = reward_list.itemIds.ToArray();
			int[] array2 = reward_list.types.ToArray();
			int[] array3 = reward_list.pri.ToArray();
			int num = array.Length;
			if (num > 0)
			{
				reward = new Reward[num];
				int i = 0;
				for (int num2 = num; i < num2; i++)
				{
					reward[i] = new Reward(array2[i], array[i], array3[i]);
				}
				Array.Sort(reward, (Reward l, Reward r) => l.priority - r.priority);
			}
			else
			{
				reward = null;
			}
			this.num = have_num;
		}
	}

	public Quest questData;

	public Mission[] missionData;

	public bool isExistMission;

	public QuestInfoData(QuestTable.QuestTableData quest_table_data, QuestData quest_list, int[] mission_clear_status)
	{
		questData = new Quest(quest_table_data, quest_list);
		MissionInit(mission_clear_status);
	}

	public QuestInfoData(QuestTable.QuestTableData quest_table_data, QuestData.QuestRewardList reward_list, int have_num, int crystal_num, int[] mission_clear_status)
	{
		questData = new Quest(quest_table_data, have_num, crystal_num, reward_list);
		MissionInit(mission_clear_status);
	}

	public static Mission[] CreateMissionData(QuestTable.QuestTableData quest_table)
	{
		if (quest_table.missionID == null || (quest_table.missionID[0] == 0 && quest_table.missionID[1] == 0 && quest_table.missionID[2] == 0))
		{
			return null;
		}
		Mission[] mission_info = new Mission[3];
		bool is_find = false;
		MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.ForEach(delegate(ClearStatusQuest data)
		{
			if (!is_find && data.questId == quest_table.questID)
			{
				is_find = true;
				int index = 0;
				data.missionStatus.ForEach(delegate
				{
					if (quest_table.missionID[index] != 0)
					{
						mission_info[index] = new Mission(Singleton<QuestTable>.I.GetMissionData(quest_table.missionID[index]), (CLEAR_STATUS)data.missionStatus[index]);
						index++;
					}
				});
			}
		});
		if (!is_find)
		{
			for (int i = 0; i < 3; i++)
			{
				if (quest_table.missionID[i] != 0)
				{
					mission_info[i] = new Mission(Singleton<QuestTable>.I.GetMissionData(quest_table.missionID[i]), CLEAR_STATUS.NOT_CLEAR);
				}
			}
		}
		return mission_info;
	}

	private void MissionInit(int[] mission_clear_status)
	{
		missionData = new Mission[3];
		isExistMission = false;
		for (int i = 0; i < 3; i++)
		{
			if (questData.tableData.missionID[i] == 0)
			{
				missionData[i] = null;
				continue;
			}
			CLEAR_STATUS state = (CLEAR_STATUS)((mission_clear_status == null) ? 1 : mission_clear_status[i]);
			missionData[i] = new Mission(Singleton<QuestTable>.I.GetMissionData(questData.tableData.missionID[i]), state);
			isExistMission = true;
		}
	}

	public bool IsMissionEmpty()
	{
		if (missionData == null)
		{
			return true;
		}
		bool flag = false;
		int i = 0;
		for (int num = missionData.Length; i < num; i++)
		{
			if (missionData[i] != null && missionData[i].tableData != null)
			{
				flag = true;
				break;
			}
		}
		return !flag;
	}
}
