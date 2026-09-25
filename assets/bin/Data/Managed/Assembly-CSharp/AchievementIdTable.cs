// Decompiled with JetBrains decompiler
// Type: AchievementIdTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class AchievementIdTable : Singleton<AchievementIdTable>, IDataTable
{
  private UIntKeyTable<AchievementIdTable.AchievementIdData> achievementIdDataTable;
  private const string NT = "taskId,goalNum,androidKey";

  public void CreateTable(string csv_text)
  {
    this.achievementIdDataTable = new UIntKeyTable<AchievementIdTable.AchievementIdData>();
    CSVReader csvReader = new CSVReader(csv_text, "taskId,goalNum,androidKey", true);
    uint key = 1;
    while (csvReader.NextLine())
    {
      AchievementIdTable.AchievementIdData achievementIdData = new AchievementIdTable.AchievementIdData();
      csvReader.Pop(ref achievementIdData.taskId);
      csvReader.Pop(ref achievementIdData.goalNum);
      csvReader.Pop(ref achievementIdData.key);
      this.achievementIdDataTable.Add(key, achievementIdData);
      ++key;
    }
    this.achievementIdDataTable.TrimExcess();
  }

  public AchievementIdTable.AchievementIdData Get(uint id)
  {
    if (this.achievementIdDataTable == null)
      return (AchievementIdTable.AchievementIdData) null;
    AchievementIdTable.AchievementIdData achievementIdData = this.achievementIdDataTable.Get(id);
    if (achievementIdData != null)
      return achievementIdData;
    Log.TableError((object) this, id);
    return (AchievementIdTable.AchievementIdData) null;
  }

  public AchievementIdTable.AchievementIdData GetByTask(int taskId)
  {
    return this.achievementIdDataTable == null ? (AchievementIdTable.AchievementIdData) null : this.achievementIdDataTable.Find((Predicate<AchievementIdTable.AchievementIdData>) (x => x.taskId == taskId));
  }

  public void ForEach(Action<AchievementIdTable.AchievementIdData> cb)
  {
    this.achievementIdDataTable.ForEach(cb);
  }

  public void CreateTable(TextAsset csv = null)
  {
    if (Object.op_Equality((Object) csv, (Object) null))
      csv = Resources.Load<TextAsset>("Internal/internal__TABLE__AchievementIdTable");
    this.CreateTable(csv.text);
  }

  public class AchievementIdData
  {
    public int taskId;
    public int goalNum;
    public string key;

    public AchievementIdData()
    {
      this.taskId = 0;
      this.goalNum = 0;
      this.key = string.Empty;
    }
  }
}
