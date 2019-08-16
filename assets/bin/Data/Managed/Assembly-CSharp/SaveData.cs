using System.Collections.Generic;
using UnityEngine;

public static class SaveData
{
	public enum Key
	{
		Account,
		DevelopHost,
		Game
	}

	private static Dictionary<string, string> m_SaveDataString = new Dictionary<string, string>();

	private static void SetString(Key key, string val)
	{
		m_SaveDataString[key.ToString()] = val;
	}

	private static string GetString(Key key, string defaultValue = "")
	{
		object result;
		if (m_SaveDataString.ContainsKey(key.ToString()))
		{
			result = m_SaveDataString[key.ToString()];
		}
		else
		{
			string @string = CryptoPrefs.GetString(key.ToString(), defaultValue);
			m_SaveDataString[key.ToString()] = @string;
			result = @string;
		}
		return (string)result;
	}

	public static void SetData<T>(Key key, T data)
	{
		string val = JsonUtility.ToJson((object)data);
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
			string @string = GetString(key, string.Empty);
			return JsonUtility.FromJson<T>(@string);
		}
		catch
		{
			return new T();
		}
	}

	public static bool HasKey(Key key)
	{
		return m_SaveDataString.ContainsKey(key.ToString()) || CryptoPrefs.HasKey(key.ToString()) || PlayerPrefs.HasKey(key.ToString());
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
