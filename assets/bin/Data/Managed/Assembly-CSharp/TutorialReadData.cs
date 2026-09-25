// Decompiled with JetBrains decompiler
// Type: TutorialReadData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class TutorialReadData
{
  private TutorialReadData.SaveData m_SaveData;
  private const string SAVE_KEY = "TutorialProgress";

  public bool IsCompleteTutorial { get; private set; }

  public TutorialReadData.SaveData Data => this.m_SaveData;

  public void SetReadId(int id, bool hasRead)
  {
    if (this.m_SaveData == null || this.m_SaveData.read_ids == null)
      return;
    if (hasRead)
    {
      if (!this.m_SaveData.read_ids.Contains(id))
        this.m_SaveData.read_ids.Add(id);
    }
    else if (this.m_SaveData.read_ids.Contains(id))
      this.m_SaveData.read_ids.Remove(id);
    this.UpdateReadAllFlag();
  }

  public bool HasRead(int id)
  {
    return this.m_SaveData != null && this.m_SaveData.read_ids != null && this.m_SaveData.read_ids.Contains(id);
  }

  public int LastRead()
  {
    return this.m_SaveData == null || this.m_SaveData.read_ids == null || this.m_SaveData.read_ids.Count == 0 ? -1 : this.m_SaveData.read_ids[this.m_SaveData.read_ids.Count - 1];
  }

  public bool HasReadAll() => this.m_SaveData != null && this.m_SaveData.read_all;

  public void UpdateReadAllFlag()
  {
    if (!Singleton<TutorialMessageTable>.IsValid() || this.m_SaveData == null || this.m_SaveData.read_ids == null)
      return;
    bool flag = true;
    int[] tutorialIds = Singleton<TutorialMessageTable>.I.GetTutorialIds();
    foreach (int num in tutorialIds)
    {
      if (!this.m_SaveData.read_ids.Contains(num))
      {
        flag = false;
        break;
      }
    }
    this.m_SaveData.read_all = this.IsCompleteTutorial = flag;
    if (!flag)
      return;
    int num1 = 0;
    int length = tutorialIds.Length;
    while (num1 < length)
      ++num1;
  }

  public static void SaveAsEmptyData()
  {
    PlayerPrefs.SetString("TutorialProgress", JSONSerializer.Serialize<TutorialReadData.SaveData>(new TutorialReadData.SaveData()));
  }

  public void Save()
  {
    PlayerPrefs.SetString("TutorialProgress", JSONSerializer.Serialize<TutorialReadData.SaveData>(this.m_SaveData));
  }

  public static bool HasSave() => PlayerPrefs.HasKey("TutorialProgress");

  public static void DeleteSave() => PlayerPrefs.DeleteKey("TutorialProgress");

  public void LoadSaveData()
  {
    TutorialReadData.SaveData saveData;
    if (TutorialReadData.HasSave())
    {
      string message = PlayerPrefs.GetString("TutorialProgress");
      saveData = JSONSerializer.Deserialize<TutorialReadData.SaveData>(message);
      if (saveData == null)
      {
        Log.Error("JSONSerializer.Deserialize<TutorialReadData.SaveData> {0}", (object) message);
        return;
      }
    }
    else
      saveData = new TutorialReadData.SaveData();
    this.m_SaveData = saveData;
  }

  public static TutorialReadData CreateAndLoad()
  {
    if (TutorialReadData.HasSave())
    {
      string message = PlayerPrefs.GetString("TutorialProgress");
      if (JSONSerializer.Deserialize<TutorialReadData.SaveData>(message) == null)
      {
        Log.Error("JSONSerializer.Deserialize<TutorialReadData.SaveData> {0}", (object) message);
        return (TutorialReadData) null;
      }
    }
    else
    {
      TutorialReadData.SaveData saveData = new TutorialReadData.SaveData();
    }
    TutorialReadData andLoad = new TutorialReadData();
    andLoad.LoadSaveData();
    andLoad.UpdateReadAllFlag();
    return andLoad;
  }

  public class SaveData
  {
    public bool read_all;
    public List<int> read_ids = new List<int>();
  }
}
