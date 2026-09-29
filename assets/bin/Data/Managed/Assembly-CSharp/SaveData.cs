// Decompiled with JetBrains decompiler
// Type: SaveData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public static class SaveData
{
  private static Dictionary<string, string> m_SaveDataString = new Dictionary<string, string>();

  private static void SetString(SaveData.Key key, string val)
  {
    SaveData.m_SaveDataString[key.ToString()] = val;
  }

  private static string GetString(SaveData.Key key, string defaultValue = "")
  {
    return !SaveData.m_SaveDataString.ContainsKey(key.ToString()) ? (SaveData.m_SaveDataString[key.ToString()] = CryptoPrefs.GetString(key.ToString(), defaultValue)) : SaveData.m_SaveDataString[key.ToString()];
  }

  public static void SetData<T>(SaveData.Key key, T data)
  {
    string json = JsonUtility.ToJson((object) data);
    SaveData.SetString(key, json);
  }

  public static T GetData<T>(SaveData.Key key) where T : new()
  {
    try
    {
      return !SaveData.HasKey(key) ? new T() : JsonUtility.FromJson<T>(SaveData.GetString(key));
    }
    catch
    {
      return new T();
    }
  }

  public static bool HasKey(SaveData.Key key)
  {
    return SaveData.m_SaveDataString.ContainsKey(key.ToString()) || CryptoPrefs.HasKey(key.ToString()) || PlayerPrefs.HasKey(key.ToString());
  }

  public static void DeleteKey(SaveData.Key key)
  {
    SaveData.m_SaveDataString.Remove(key.ToString());
    CryptoPrefs.DeleteKey(key.ToString());
    PlayerPrefs.DeleteKey(key.ToString());
  }

  public static void DeleteAll()
  {
    SaveData.m_SaveDataString.Clear();
    CryptoPrefs.DeleteAll();
  }

  public static void Save()
  {
    foreach (KeyValuePair<string, string> keyValuePair in SaveData.m_SaveDataString)
      CryptoPrefs.SetString(keyValuePair.Key, keyValuePair.Value);
    CryptoPrefs.Save();
  }

  public enum Key
  {
    Account,
    DevelopHost,
    Game,
    ServerAccount,
  }
}
