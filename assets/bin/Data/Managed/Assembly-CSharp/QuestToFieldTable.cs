// Decompiled with JetBrains decompiler
// Type: QuestToFieldTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class QuestToFieldTable : Singleton<QuestToFieldTable>, IDataTable
{
  private DoubleUIntKeyTable<QuestToFieldTable.QuestToFieldData> questToItemTable;

  public void CreateTable(string csv_text)
  {
    this.questToItemTable = TableUtility.CreateDoubleUIntKeyTable<QuestToFieldTable.QuestToFieldData>(csv_text, new TableUtility.CallBackDoubleUIntKeyReadCSV<QuestToFieldTable.QuestToFieldData>(QuestToFieldTable.QuestToFieldData.cb), "questId,mapId,eventId", new TableUtility.CallBackDoubleUIntSecondKey(QuestToFieldTable.QuestToFieldData.CBSecondKey));
    this.questToItemTable.TrimExcess();
  }

  public void AddTable(string csv_text)
  {
    TableUtility.AddDoubleUIntKeyTable<QuestToFieldTable.QuestToFieldData>(this.questToItemTable, csv_text, new TableUtility.CallBackDoubleUIntKeyReadCSV<QuestToFieldTable.QuestToFieldData>(QuestToFieldTable.QuestToFieldData.cb), "questId,mapId,eventId", new TableUtility.CallBackDoubleUIntSecondKey(QuestToFieldTable.QuestToFieldData.CBSecondKey));
  }

  public void InitDependencyData()
  {
    if (!Singleton<FieldMapTable>.IsValid())
      return;
    this.questToItemTable.ForEach((Action<UIntKeyTable<QuestToFieldTable.QuestToFieldData>>) (x => x.ForEach((Action<QuestToFieldTable.QuestToFieldData>) (data =>
    {
      FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(data.mapId);
      if (fieldMapData == null)
        return;
      data.grade = fieldMapData.grade;
    }))));
  }

  public bool IsValidHappenQuest(uint questId)
  {
    if (this.questToItemTable == null)
      return false;
    UIntKeyTable<QuestToFieldTable.QuestToFieldData> uintKeyTable = this.questToItemTable.Get(questId);
    if (uintKeyTable == null)
      return false;
    Version applicationVersion = NetworkNative.getNativeVersionFromName();
    int field_count = 0;
    uintKeyTable.ForEach((Action<QuestToFieldTable.QuestToFieldData>) (data =>
    {
      if (data.mapId == 0U || data.eventId > 0U && !MonoBehaviourSingleton<QuestManager>.I.IsEventPlayableWith((int) data.eventId, applicationVersion))
        return;
      FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(data.mapId);
      if (fieldMapData == null || fieldMapData.IsEventData && !MonoBehaviourSingleton<QuestManager>.I.IsEventPlayableWith(fieldMapData.eventId, applicationVersion))
        return;
      ++field_count;
    }));
    return field_count > 0;
  }

  public FieldMapTable.FieldMapTableData[] GetFieldMapTableFromQuestIdWithClosedField(uint questId)
  {
    return this.GetFieldMapTableFromQuestId(questId, true);
  }

  public FieldMapTable.FieldMapTableData[] GetFieldMapTableFromQuestId(
    uint questId,
    bool hasIncludelockedField = false)
  {
    if (this.questToItemTable == null)
      return (FieldMapTable.FieldMapTableData[]) null;
    UIntKeyTable<QuestToFieldTable.QuestToFieldData> uintKeyTable = this.questToItemTable.Get(questId);
    if (uintKeyTable == null)
      return (FieldMapTable.FieldMapTableData[]) null;
    List<FieldMapTable.FieldMapTableData> list = new List<FieldMapTable.FieldMapTableData>();
    uintKeyTable.ForEach((Action<QuestToFieldTable.QuestToFieldData>) (data =>
    {
      if (data.mapId == 0U)
        return;
      FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(data.mapId);
      if (fieldMapData == null || !fieldMapData.IsEventData && !hasIncludelockedField && !Singleton<ItemToFieldTable>.I.IsOpenMap(fieldMapData))
        return;
      list.Add(fieldMapData);
    }));
    return list.Count <= 0 ? (FieldMapTable.FieldMapTableData[]) null : list.ToArray();
  }

  public List<uint> GetQuestIdList(uint mapId)
  {
    List<uint> list = new List<uint>();
    this.questToItemTable.ForEach((Action<UIntKeyTable<QuestToFieldTable.QuestToFieldData>>) (table => table.ForEach((Action<QuestToFieldTable.QuestToFieldData>) (data =>
    {
      if ((int) data.mapId != (int) mapId)
        return;
      list.Add(data.questId);
    }))));
    return list;
  }

  public Dictionary<uint, uint> GetQuestIdEventIdDic(uint mapId)
  {
    Dictionary<uint, uint> dic = new Dictionary<uint, uint>();
    this.questToItemTable.ForEach((Action<UIntKeyTable<QuestToFieldTable.QuestToFieldData>>) (table => table.ForEach((Action<QuestToFieldTable.QuestToFieldData>) (data =>
    {
      if ((int) data.mapId != (int) mapId)
        return;
      dic[data.questId] = data.eventId;
    }))));
    return dic;
  }

  public class QuestToFieldData
  {
    public uint questId;
    public uint mapId;
    public uint eventId;
    public int grade;
    public const string NT = "questId,mapId,eventId";

    public static bool cb(
      CSVReader csv_reader,
      QuestToFieldTable.QuestToFieldData data,
      ref uint key1,
      ref uint key2)
    {
      data.questId = key1;
      csv_reader.Pop(ref data.mapId);
      csv_reader.Pop(ref data.eventId);
      return true;
    }

    public static string CBSecondKey(CSVReader csv, int table_data_num)
    {
      return table_data_num.ToString();
    }
  }
}
