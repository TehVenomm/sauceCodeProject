using System.Collections.Generic;
using UnityEngine;

public static class SaveData
{
	public enum Key
	{
		Account,
		DevelopHost,
		Game,
		ServerAccount
	}

	private static Dictionary<string, string> m_SaveDataString = new Dictionary<string, string>();

	private static void SetString(Key key, string val)
	{
		m_SaveDataString[key.ToString()] = val;
	}

	private static string GetString(Key key, string defaultValue = "")
	{
		if (!m_SaveDataString.ContainsKey(key.ToString()))
		{
			return m_SaveDataString[key.ToString()] = CryptoPrefs.GetString(key.ToString(), defaultValue);
		}
		return m_SaveDataString[key.ToString()];
	}

	public static void SetData<T>(Key key, T data)
	{
		string val = JsonUtility.ToJson(data);
		SetString(key, val);
	}

	public static T GetData<T>(Key key) where T : new()
	{
		try
		{
			if (!HasKey(key))
			{
				return new T();
			}
			return JsonUtility.FromJson<T>(GetString(key));
		}
		catch
		{
			return new T();
		}
	}

	public static bool HasKey(Key key)
	{
		if (!m_SaveDataString.ContainsKey(key.ToString()) && !CryptoPrefs.HasKey(key.ToString()))
		{
			return PlayerPrefs.HasKey(key.ToString());
		}
		return true;
	}

	public static void DeleteKey(Key key)
	{
		m_SaveDataString.Remove(key.ToString());
		CryptoPrefs.DeleteKey(key.ToString());
		PlayerPrefs.DeleteKey(key.ToString());
	}

	public static void DeleteAll()
	{
		m_SaveDataString.Clear();
		CryptoPrefs.DeleteAll();
	}

	public static void Save()
	{
		foreach (KeyValuePair<string, string> item in m_SaveDataString)
		{
			CryptoPrefs.SetString(item.Key, item.Value);
		}
		CryptoPrefs.Save();
	}
}
