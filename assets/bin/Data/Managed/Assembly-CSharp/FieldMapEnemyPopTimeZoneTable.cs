// Decompiled with JetBrains decompiler
// Type: FieldMapEnemyPopTimeZoneTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class FieldMapEnemyPopTimeZoneTable : Singleton<FieldMapEnemyPopTimeZoneTable>, IDataTable
{
  private UIntKeyTable<FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData> timeZoneDataTable;

  public void CreateTable(string csv_text)
  {
    this.timeZoneDataTable = TableUtility.CreateUIntKeyTable<FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData>(FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData.cb), "id,startTime,endTime,enemyId,mapId,existStrId,goneStrId");
  }

  public void CreateTable(string csv_text, TableUtility.Progress progress)
  {
    this.timeZoneDataTable = TableUtility.CreateUIntKeyTable<FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData>(FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData.cb), "id,startTime,endTime,enemyId,mapId,existStrId,goneStrId", progress);
    this.timeZoneDataTable.TrimExcess();
  }

  public void AddTable(string csv_text)
  {
    TableUtility.AddUIntKeyTable<FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData>(this.timeZoneDataTable, csv_text, new TableUtility.CallBackUIntKeyReadCSV<FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData>(FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData.cb), "id,startTime,endTime,enemyId,mapId,existStrId,goneStrId");
  }

  public List<FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData> GetEnemyTimeZoneDataList(
    int mapId)
  {
    if (this.timeZoneDataTable == null)
      return (List<FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData>) null;
    List<FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData> list = new List<FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData>();
    this.timeZoneDataTable.ForEach((Action<FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData>) (data =>
    {
      if (data.mapId != mapId)
        return;
      list.Add(data);
    }));
    return list;
  }

  public bool TryGetEnableLastEndTime(
    int mapId,
    out FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData resultTimeZone,
    out ENEMY_POP_TYPE resultType)
  {
    resultTimeZone = (FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData) null;
    resultType = ENEMY_POP_TYPE.RARE_SPECIES;
    if (!MonoBehaviourSingleton<FieldManager>.IsValid())
      return false;
    List<FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData> timeZoneDataList = this.GetEnemyTimeZoneDataList(mapId);
    if (timeZoneDataList == null || timeZoneDataList.Count <= 0)
      return false;
    List<FieldMapTable.EnemyPopTableData> rareOrBossEnemyList = Singleton<FieldMapTable>.I.GetRareOrBossEnemyList(mapId);
    if (rareOrBossEnemyList == null || rareOrBossEnemyList.Count <= 0)
      return false;
    bool enableLastEndTime = false;
    DateTime minValue = DateTime.MinValue;
    DateTime createdAt;
    if (!MonoBehaviourSingleton<FieldManager>.I.fieldData.field.TryGetCreatedAt(out createdAt))
      return false;
    DateTime now = TimeManager.GetNow();
    int index = 0;
    for (int count = timeZoneDataList.Count; index < count; ++index)
    {
      FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData enemyPopTimeZoneData = timeZoneDataList[index];
      DateTime result1;
      DateTime result2;
      if (enemyPopTimeZoneData.TryGetStartTime(out result1) && enemyPopTimeZoneData.TryGetEndTime(out result2))
      {
        result1 = TimeManager.CombineDateAndTime(createdAt, result1);
        result2 = TimeManager.CombineDateAndTime(createdAt, result2);
        if (createdAt >= result1 && now <= result2)
        {
          FieldMapTable.EnemyPopTableData enemyPopData = this.FindEnemyPopData(rareOrBossEnemyList, mapId, enemyPopTimeZoneData.enemyId);
          if (enemyPopData != null && (result2 > minValue || this.IsPreferredType(resultType, enemyPopData)))
          {
            enableLastEndTime = true;
            resultType = enemyPopData.enemyPopType;
            resultTimeZone = enemyPopTimeZoneData;
          }
        }
      }
    }
    return enableLastEndTime;
  }

  private bool IsPreferredType(ENEMY_POP_TYPE now, FieldMapTable.EnemyPopTableData popData)
  {
    return now == ENEMY_POP_TYPE.RARE_SPECIES && popData.enemyPopType == ENEMY_POP_TYPE.FIELD_BOSS;
  }

  private FieldMapTable.EnemyPopTableData FindEnemyPopData(
    List<FieldMapTable.EnemyPopTableData> specialEnemyList,
    int mapId,
    int enemyId)
  {
    int index = 0;
    for (int count = specialEnemyList.Count; index < count; ++index)
    {
      FieldMapTable.EnemyPopTableData specialEnemy = specialEnemyList[index];
      if ((long) specialEnemy.mapID == (long) mapId && (long) specialEnemy.enemyID == (long) enemyId)
        return specialEnemy;
    }
    return (FieldMapTable.EnemyPopTableData) null;
  }

  public class FieldMapEnemyPopTimeZoneData
  {
    public uint id;
    public string startTime;
    public string endTime;
    public int enemyId;
    public int mapId;
    public uint existStrId;
    public uint goneStrId;
    public const string NT = "id,startTime,endTime,enemyId,mapId,existStrId,goneStrId";

    public static bool cb(
      CSVReader csv_reader,
      FieldMapEnemyPopTimeZoneTable.FieldMapEnemyPopTimeZoneData data,
      ref uint key)
    {
      data.id = key;
      csv_reader.Pop(ref data.startTime);
      csv_reader.Pop(ref data.endTime);
      csv_reader.Pop(ref data.enemyId);
      csv_reader.Pop(ref data.mapId);
      csv_reader.Pop(ref data.existStrId);
      csv_reader.Pop(ref data.goneStrId);
      return true;
    }

    public bool TryGetStartTime(out DateTime result)
    {
      return DateTime.TryParse(this.startTime, out result);
    }

    public bool TryGetEndTime(out DateTime result) => DateTime.TryParse(this.endTime, out result);
  }
}
