using System.Collections.Generic;

public class GameSectionHistory
{
	public class HistoryData
	{
		public string sceneName;

		public string sectionName;

		public GAME_SECTION_TYPE sectionType;
	}

	private List<HistoryData> historyList = new List<HistoryData>();

	public void Push(string scene_name, string section_name, GAME_SECTION_TYPE section_type)
	{
		int num = historyList.FindIndex((HistoryData o) => o.sceneName == scene_name && o.sectionName == section_name);
		if (num != -1)
		{
			int num2 = historyList.Count - 1;
			num++;
			if (num <= num2)
			{
				historyList.RemoveRange(num, num2 - num + 1);
			}
		}
		else
		{
			HistoryData historyData = new HistoryData();
			historyData.sceneName = scene_name;
			historyData.sectionName = section_name;
			historyData.sectionType = section_type;
			historyList.Add(historyData);
		}
	}

	public void PopSection()
	{
		int count = historyList.Count;
		if (count > 0)
		{
			historyList.RemoveAt(count - 1);
		}
	}

	public void RemoveSection(HistoryData removeData)
	{
		int num = historyList.FindIndex((HistoryData x) => x.sceneName == removeData.sceneName && x.sectionName == removeData.sectionName);
		if (num >= 0)
		{
			historyList.RemoveAt(num);
		}
	}

	public void RemoveSection(string removeSectionName)
	{
		int num = historyList.FindIndex((HistoryData x) => x.sectionName == removeSectionName);
		if (num >= 0)
		{
			historyList.RemoveAt(num);
		}
	}

	public void CueScene()
	{
		int num = historyList.FindLastIndex((HistoryData o) => o.sectionType == GAME_SECTION_TYPE.SCENE);
		if (num != -1)
		{
			int num2 = historyList.Count - 1;
			num++;
			if (num <= num2)
			{
				historyList.RemoveRange(num, num2 - num + 1);
			}
		}
	}

	public void CutDialog()
	{
		int num = historyList.FindLastIndex((HistoryData o) => !o.sectionType.IsDialog());
		if (num != -1)
		{
			int num2 = historyList.Count - 1;
			num++;
			if (num <= num2)
			{
				historyList.RemoveRange(num, num2 - num + 1);
			}
		}
	}

	public void CutSingleDialog()
	{
		int num = historyList.FindLastIndex((HistoryData o) => !o.sectionType.IsSingle());
		if (num != -1)
		{
			int num2 = historyList.Count - 1;
			num++;
			if (num <= num2)
			{
				historyList.RemoveRange(num, num2 - num + 1);
			}
		}
	}

	public HistoryData GetLast()
	{
		int count = historyList.Count;
		if (count == 0)
		{
			return null;
		}
		return historyList[count - 1];
	}

	public HistoryData GetLast(int i, bool ignore_common_dialog = false)
	{
		int count = historyList.Count;
		HistoryData historyData = null;
		while (historyData == null && i > 0 && i <= count)
		{
			historyData = historyList[count - i];
			if (ignore_common_dialog && historyData.sectionType == GAME_SECTION_TYPE.COMMON_DIALOG)
			{
				historyData = null;
				i++;
			}
		}
		return historyData;
	}

	public bool Exist(string name)
	{
		int i = 0;
		for (int count = historyList.Count; i < count; i++)
		{
			if (historyList[i].sectionName == name)
			{
				return true;
			}
		}
		return false;
	}

	public void Clear()
	{
		historyList.Clear();
	}

	public List<HistoryData> GetHistoryList()
	{
		return historyList;
	}
}
