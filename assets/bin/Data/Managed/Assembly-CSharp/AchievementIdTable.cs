using System;
using UnityEngine;

public class AchievementIdTable : Singleton<AchievementIdTable>, IDataTable
{
	public class AchievementIdData
	{
		public int taskId;

		public int goalNum;

		public string key;

		public AchievementIdData()
		{
			taskId = 0;
			goalNum = 0;
			key = string.Empty;
		}
	}

	private const string NT = "taskId,goalNum,androidKey";

	private UIntKeyTable<AchievementIdData> achievementIdDataTable;

	public void CreateTable(string csv_text)
	{
		achievementIdDataTable = new UIntKeyTable<AchievementIdData>();
		CSVReader cSVReader = new CSVReader(csv_text, "taskId,goalNum,androidKey", true);
		uint num = 1u;
		while (cSVReader.NextLine())
		{
			AchievementIdData achievementIdData = new AchievementIdData();
			cSVReader.Pop(ref achievementIdData.taskId);
			cSVReader.Pop(ref achievementIdData.goalNum);
			cSVReader.Pop(ref achievementIdData.key);
			achievementIdDataTable.Add(num, achievementIdData);
			num++;
		}
		achievementIdDataTable.TrimExcess();
	}

	public AchievementIdData Get(uint id)
	{
		if (achievementIdDataTable == null)
		{
			return null;
		}
		AchievementIdData achievementIdData = achievementIdDataTable.Get(id);
		if (achievementIdData == null)
		{
			Log.TableError(this, id);
			return null;
		}
		return achievementIdData;
	}

	public AchievementIdData GetByTask(int taskId)
	{
		if (achievementIdDataTable == null)
		{
			return null;
		}
		return achievementIdDataTable.Find((AchievementIdData x) => x.taskId == taskId);
	}

	public void ForEach(Action<AchievementIdData> cb)
	{
		achievementIdDataTable.ForEach(cb);
	}

	public void CreateTable(TextAsset csv = null)
	{
		bool flag = false;
		if (csv == null)
		{
			csv = Resources.Load<TextAsset>("Internal/internal__TABLE__AchievementIdTable");
		}
		CreateTable(csv.get_text());
	}
}
