using System.Collections.Generic;
using UnityEngine;

public class TutorialReadData
{
	public class SaveData
	{
		public bool read_all;

		public List<int> read_ids = new List<int>();
	}

	private const string SAVE_KEY = "TutorialProgress";

	private SaveData m_SaveData;

	public bool IsCompleteTutorial
	{
		get;
		private set;
	}

	public SaveData Data => m_SaveData;

	public void SetReadId(int id, bool hasRead)
	{
		if (m_SaveData != null && m_SaveData.read_ids != null)
		{
			if (hasRead)
			{
				if (!m_SaveData.read_ids.Contains(id))
				{
					m_SaveData.read_ids.Add(id);
				}
			}
			else if (m_SaveData.read_ids.Contains(id))
			{
				m_SaveData.read_ids.Remove(id);
			}
			UpdateReadAllFlag();
		}
	}

	public bool HasRead(int id)
	{
		if (m_SaveData == null || m_SaveData.read_ids == null)
		{
			return false;
		}
		return m_SaveData.read_ids.Contains(id);
	}

	public int LastRead()
	{
		if (m_SaveData == null || m_SaveData.read_ids == null)
		{
			return -1;
		}
		if (m_SaveData.read_ids.Count == 0)
		{
			return -1;
		}
		return m_SaveData.read_ids[m_SaveData.read_ids.Count - 1];
	}

	public bool HasReadAll()
	{
		if (m_SaveData == null)
		{
			return false;
		}
		return m_SaveData.read_all;
	}

	public void UpdateReadAllFlag()
	{
		if (Singleton<TutorialMessageTable>.IsValid() && m_SaveData != null && m_SaveData.read_ids != null)
		{
			bool flag = true;
			int[] tutorialIds = Singleton<TutorialMessageTable>.I.GetTutorialIds();
			int[] array = tutorialIds;
			foreach (int item in array)
			{
				if (!m_SaveData.read_ids.Contains(item))
				{
					flag = false;
					break;
				}
			}
			SaveData saveData = m_SaveData;
			bool read_all = IsCompleteTutorial = flag;
			saveData.read_all = read_all;
			if (flag)
			{
				int j = 0;
				for (int num = tutorialIds.Length; j < num; j++)
				{
				}
			}
		}
	}

	public static void SaveAsEmptyData()
	{
		string value = JSONSerializer.Serialize(new SaveData());
		PlayerPrefs.SetString("TutorialProgress", value);
	}

	public void Save()
	{
		string value = JSONSerializer.Serialize(m_SaveData);
		PlayerPrefs.SetString("TutorialProgress", value);
	}

	public static bool HasSave()
	{
		return PlayerPrefs.HasKey("TutorialProgress");
	}

	public static void DeleteSave()
	{
		PlayerPrefs.DeleteKey("TutorialProgress");
	}

	public void LoadSaveData()
	{
		SaveData saveData = null;
		if (HasSave())
		{
			string @string = PlayerPrefs.GetString("TutorialProgress");
			saveData = JSONSerializer.Deserialize<SaveData>(@string);
			if (saveData == null)
			{
				Log.Error("JSONSerializer.Deserialize<TutorialReadData.SaveData> {0}", @string);
				return;
			}
		}
		else
		{
			saveData = new SaveData();
		}
		m_SaveData = saveData;
	}

	public static TutorialReadData CreateAndLoad()
	{
		SaveData saveData = null;
		if (HasSave())
		{
			string @string = PlayerPrefs.GetString("TutorialProgress");
			saveData = JSONSerializer.Deserialize<SaveData>(@string);
			if (saveData == null)
			{
				Log.Error("JSONSerializer.Deserialize<TutorialReadData.SaveData> {0}", @string);
				return null;
			}
		}
		else
		{
			saveData = new SaveData();
		}
		TutorialReadData tutorialReadData = new TutorialReadData();
		tutorialReadData.LoadSaveData();
		tutorialReadData.UpdateReadAllFlag();
		return tutorialReadData;
	}
}
