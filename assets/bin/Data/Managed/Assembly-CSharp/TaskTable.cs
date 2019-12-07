using System;

public class TaskTable : Singleton<TaskTable>, IDataTable
{
	public class TaskData
	{
		public int id;

		public string title;

		public string detail;

		public int orderNo;

		public string openRequirements;

		public string startTime;

		public string endTime;

		public int goalNum;

		public string conditionVal1;

		public string conditionVal2;

		public string conditionVal3;

		public string conditionVal4;

		public string conditionVal5;

		public REWARD_TYPE rewardType;

		public int itemId;

		public int rewardNum;

		public int rewartVal1;

		public int rewartVal2;

		public string minVersion;

		public const string NT = "taskId,title,detail,orderNo,openRequirements,startTime,endTime,goalNum,conditionVal1,conditionVal2,conditionVal3,conditionVal4,conditionVal5,rewardType,itemId,rewardNum,rewardVal1,rewardVal2,minVersion";

		public static bool cb(CSVReader csv_reader, TaskData data, ref uint key)
		{
			data.id = (int)key;
			csv_reader.Pop(ref data.title);
			csv_reader.Pop(ref data.detail);
			csv_reader.Pop(ref data.orderNo);
			csv_reader.Pop(ref data.openRequirements);
			csv_reader.Pop(ref data.startTime);
			csv_reader.Pop(ref data.endTime);
			csv_reader.Pop(ref data.goalNum);
			csv_reader.Pop(ref data.conditionVal1);
			csv_reader.Pop(ref data.conditionVal2);
			csv_reader.Pop(ref data.conditionVal3);
			csv_reader.Pop(ref data.conditionVal4);
			csv_reader.Pop(ref data.conditionVal5);
			string value = string.Empty;
			csv_reader.Pop(ref value);
			data.rewardType = (REWARD_TYPE)Enum.Parse(typeof(REWARD_TYPE), value);
			csv_reader.Pop(ref data.itemId);
			csv_reader.Pop(ref data.rewardNum);
			csv_reader.Pop(ref data.rewartVal1);
			csv_reader.Pop(ref data.rewartVal2);
			csv_reader.Pop(ref data.minVersion);
			return true;
		}

		public string GetRewardString()
		{
			if (!Singleton<ItemTable>.IsValid())
			{
				return string.Empty;
			}
			return MonoBehaviourSingleton<AchievementManager>.I.GetRewardName(rewardType, (uint)itemId, (uint)rewardNum, (uint)rewartVal1);
		}
	}

	private UIntKeyTable<TaskData> taskDataTable;

	public void CreateTable(string csv_text)
	{
		taskDataTable = TableUtility.CreateUIntKeyTable<TaskData>(csv_text, TaskData.cb, "taskId,title,detail,orderNo,openRequirements,startTime,endTime,goalNum,conditionVal1,conditionVal2,conditionVal3,conditionVal4,conditionVal5,rewardType,itemId,rewardNum,rewardVal1,rewardVal2,minVersion");
		taskDataTable.TrimExcess();
	}

	public TaskData Get(uint id)
	{
		if (taskDataTable == null)
		{
			return null;
		}
		TaskData taskData = taskDataTable.Get(id);
		if (taskData == null)
		{
			Log.TableError(this, id);
			return null;
		}
		return taskData;
	}
}
