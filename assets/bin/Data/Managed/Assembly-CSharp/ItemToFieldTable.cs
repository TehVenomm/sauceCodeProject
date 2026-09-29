// Decompiled with JetBrains decompiler
// Type: ItemToFieldTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ItemToFieldTable : Singleton<ItemToFieldTable>, IDataTable
{
  public const int ENEMY_NUM_MAX = 5;
  public const int GATHER_POINT_NUM_MAX = 5;
  private DoubleUIntKeyTable<ItemToFieldTable.ItemToFieldData> itemToFieldTable;

  public void CreateTable(string csv_text)
  {
    this.itemToFieldTable = TableUtility.CreateDoubleUIntKeyTable<ItemToFieldTable.ItemToFieldData>(csv_text, new TableUtility.CallBackDoubleUIntKeyReadCSV<ItemToFieldTable.ItemToFieldData>(ItemToFieldTable.ItemToFieldData.cb), "itemId,fieldId,enemyId_0,enemyId_1,enemyId_2,enemyId_3,enemyId_4,pointId_0,pointId_1,pointId_2,pointId_3,pointId_4", new TableUtility.CallBackDoubleUIntSecondKey(ItemToFieldTable.ItemToFieldData.CBSecondKey));
    this.itemToFieldTable.TrimExcess();
  }

  public void AddTable(string csv_text)
  {
    TableUtility.AddDoubleUIntKeyTable<ItemToFieldTable.ItemToFieldData>(this.itemToFieldTable, csv_text, new TableUtility.CallBackDoubleUIntKeyReadCSV<ItemToFieldTable.ItemToFieldData>(ItemToFieldTable.ItemToFieldData.cb), "itemId,fieldId,enemyId_0,enemyId_1,enemyId_2,enemyId_3,enemyId_4,pointId_0,pointId_1,pointId_2,pointId_3,pointId_4", new TableUtility.CallBackDoubleUIntSecondKey(ItemToFieldTable.ItemToFieldData.CBSecondKey));
  }

  public void InitDependencyData()
  {
    if (!Singleton<FieldMapTable>.IsValid())
      return;
    this.itemToFieldTable.ForEach((Action<UIntKeyTable<ItemToFieldTable.ItemToFieldData>>) (x => x.ForEach((Action<ItemToFieldTable.ItemToFieldData>) (data =>
    {
      FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(data.fieldId);
      if (fieldMapData == null)
        return;
      data.grade = fieldMapData.grade;
    }))));
  }

  public ItemToFieldTable.ItemDetailToFieldData[] GetFieldTableFromItemID(
    uint item_id,
    out bool find_unknown_field,
    bool isExcludeNotPlayable = false)
  {
    find_unknown_field = false;
    if (this.itemToFieldTable == null)
      return (ItemToFieldTable.ItemDetailToFieldData[]) null;
    UIntKeyTable<ItemToFieldTable.ItemToFieldData> uintKeyTable = this.itemToFieldTable.Get(item_id);
    if (uintKeyTable == null)
      return (ItemToFieldTable.ItemDetailToFieldData[]) null;
    bool temp_find_unknown = false;
    List<ItemToFieldTable.ItemDetailToFieldData> list = new List<ItemToFieldTable.ItemDetailToFieldData>();
    List<uint> icon_id_list = new List<uint>();
    uintKeyTable.ForEach((Action<ItemToFieldTable.ItemToFieldData>) (data =>
    {
      if (data.enemyId == null || data.enemyId.Length == 0)
        return;
      FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(data.fieldId);
      if (fieldMapData == null)
        return;
      if (this.IsOpenMap(fieldMapData))
      {
        if (data.enemyId[0] != 0U)
          list.Add((ItemToFieldTable.ItemDetailToFieldData) new ItemToFieldTable.ItemDetailToFieldEnemyData(fieldMapData, data.enemyId));
        int index = 0;
        for (int length = data.pointId.Length; index < length; ++index)
        {
          if (data.pointId[index] > 0U)
          {
            FieldMapTable.GatherPointTableData gatherPointData = Singleton<FieldMapTable>.I.GetGatherPointData(data.pointId[index]);
            if (gatherPointData != null)
            {
              FieldMapTable.GatherPointViewTableData gatherPointViewData = Singleton<FieldMapTable>.I.GetGatherPointViewData(gatherPointData.viewID);
              if (gatherPointViewData != null && gatherPointViewData.iconID > 0U && icon_id_list.IndexOf(gatherPointViewData.iconID) < 0 && (!isExcludeNotPlayable || this.IsPlayableField(fieldMapData)))
              {
                icon_id_list.Add(gatherPointViewData.iconID);
                list.Add((ItemToFieldTable.ItemDetailToFieldData) new ItemToFieldTable.ItemDetailToFieldPointData(fieldMapData, data.pointId[index], gatherPointData, gatherPointViewData));
              }
            }
          }
        }
      }
      else
        temp_find_unknown = true;
    }));
    find_unknown_field = temp_find_unknown;
    return list.Count == 0 ? (ItemToFieldTable.ItemDetailToFieldData[]) null : list.ToArray();
  }

  public ItemToFieldTable.CandidateField[] GetCandidateField(
    uint item_id,
    int trim_count = -1,
    bool isExcludeNotPlayable = false)
  {
    List<ItemToFieldTable.CandidateField> ret = new List<ItemToFieldTable.CandidateField>();
    List<string> enemy_names = new List<string>();
    UIntKeyTable<ItemToFieldTable.ItemToFieldData> uintKeyTable = this.itemToFieldTable.Get(item_id);
    if (uintKeyTable == null)
      return (ItemToFieldTable.CandidateField[]) null;
    uintKeyTable.ForEach((Action<ItemToFieldTable.ItemToFieldData>) (data =>
    {
      FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(data.fieldId);
      if (data.enemyId == null)
        return;
      foreach (uint id in data.enemyId)
      {
        EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData(id);
        if (enemyData != null && (!isExcludeNotPlayable || this.IsPlayableField(fieldMapData)) && !enemy_names.Contains(enemyData.name))
        {
          ret.Add(new ItemToFieldTable.CandidateField()
          {
            enemyId = id,
            mapData = fieldMapData
          });
          enemy_names.Add(enemyData.name);
        }
      }
    }));
    return ret.ToArray();
  }

  private bool IsPlayableField(FieldMapTable.FieldMapTableData field_table)
  {
    return !field_table.IsEventData || MonoBehaviourSingleton<QuestManager>.I.IsPlayableVersionEvent(field_table.eventId);
  }

  public uint[] GetCandidateEnemies(uint item_id, int trim_count = -1)
  {
    List<uint> enemy_ids = new List<uint>();
    List<string> enemy_names = new List<string>();
    UIntKeyTable<ItemToFieldTable.ItemToFieldData> uintKeyTable = this.itemToFieldTable.Get(item_id);
    if (uintKeyTable == null)
      return (uint[]) null;
    uintKeyTable.ForEach((Action<ItemToFieldTable.ItemToFieldData>) (data =>
    {
      if (data.enemyId == null)
        return;
      foreach (uint id in data.enemyId)
      {
        EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData(id);
        if (enemyData != null && !enemy_names.Contains(enemyData.name))
        {
          enemy_ids.Add(id);
          enemy_names.Add(enemyData.name);
        }
      }
    }));
    return trim_count > 0 ? enemy_ids.GetRange(0, Mathf.Min(enemy_ids.Count, trim_count)).ToArray() : enemy_ids.ToArray();
  }

  public ItemToFieldTable.RecommendFieldData GetRecommendField(
    uint item_id,
    int max_num,
    bool isExcludeNotPlayable = false)
  {
    ItemToFieldTable.RecommendFieldData recommendField1 = new ItemToFieldTable.RecommendFieldData((ItemToFieldTable.ItemDetailToFieldData[]) null, false);
    bool find_unknown_field = false;
    ItemToFieldTable.ItemDetailToFieldData[] fieldTableFromItemId = this.GetFieldTableFromItemID(item_id, out find_unknown_field, isExcludeNotPlayable);
    if (fieldTableFromItemId == null || fieldTableFromItemId.Length == 0 || max_num <= 0)
    {
      QuestTable.QuestTableData[] questTableDataArray = (QuestTable.QuestTableData[]) null;
      if (Singleton<ItemToQuestTable>.IsValid())
        questTableDataArray = Singleton<ItemToQuestTable>.I.GetHappenQuestTableFromItemID(item_id);
      recommendField1.isNeedUnknownField = questTableDataArray != null && questTableDataArray.Length != 0 || find_unknown_field;
      return recommendField1;
    }
    if (fieldTableFromItemId.Length <= max_num)
    {
      recommendField1.dropFieldData = fieldTableFromItemId;
      return recommendField1;
    }
    Array.Sort<ItemToFieldTable.ItemDetailToFieldData>(fieldTableFromItemId, (Comparison<ItemToFieldTable.ItemDetailToFieldData>) ((l, r) =>
    {
      int recommendField2 = r.mapData.grade - l.mapData.grade;
      if (recommendField2 == 0)
        recommendField2 = (int) r.mapData.mapID - (int) l.mapData.mapID;
      return recommendField2;
    }));
    Array.Resize<ItemToFieldTable.ItemDetailToFieldData>(ref fieldTableFromItemId, max_num);
    recommendField1.dropFieldData = fieldTableFromItemId;
    return recommendField1;
  }

  public ItemToFieldTable.FIELD_AVAILABLE_CHOICES IsAvailableMap(
    FieldMapTable.FieldMapTableData field_table)
  {
    ItemToFieldTable.FIELD_AVAILABLE_CHOICES available_choices;
    this._IsOpenMap(field_table, out available_choices);
    return available_choices;
  }

  public bool IsOpenMap(FieldMapTable.FieldMapTableData field_table)
  {
    return this._IsOpenMap(field_table, out ItemToFieldTable.FIELD_AVAILABLE_CHOICES _);
  }

  private bool _IsOpenMap(
    FieldMapTable.FieldMapTableData field_table,
    out ItemToFieldTable.FIELD_AVAILABLE_CHOICES available_choices)
  {
    if (field_table.IsEventData)
    {
      List<Network.EventData> eventDataList = new List<Network.EventData>((IEnumerable<Network.EventData>) MonoBehaviourSingleton<QuestManager>.I.eventList);
      eventDataList.RemoveAll((Predicate<Network.EventData>) (e => e.HasEndDate() && e.GetRest() < 0));
      eventDataList.RemoveAll((Predicate<Network.EventData>) (e => !e.enableEvent));
      if (eventDataList.Find((Predicate<Network.EventData>) (e => e.eventId == field_table.eventId)) != null)
      {
        available_choices = ItemToFieldTable.FIELD_AVAILABLE_CHOICES.AVAILABLE;
        return true;
      }
      available_choices = ItemToFieldTable.FIELD_AVAILABLE_CHOICES.NOT_TRAVELED;
      return false;
    }
    if (field_table.grade > MonoBehaviourSingleton<UserInfoManager>.I.userStatus.fieldGrade)
    {
      available_choices = ItemToFieldTable.FIELD_AVAILABLE_CHOICES.TOO_BIG_GRADE;
      return false;
    }
    if (!MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledMap((int) field_table.mapID))
    {
      available_choices = ItemToFieldTable.FIELD_AVAILABLE_CHOICES.NOT_TRAVELED;
      return false;
    }
    available_choices = ItemToFieldTable.FIELD_AVAILABLE_CHOICES.AVAILABLE;
    return true;
  }

  public class ItemToFieldData
  {
    public uint itemId;
    public uint fieldId;
    public uint[] enemyId = new uint[5];
    public uint[] pointId = new uint[5];
    public int grade;
    private uint key2;
    public const string NT = "itemId,fieldId,enemyId_0,enemyId_1,enemyId_2,enemyId_3,enemyId_4,pointId_0,pointId_1,pointId_2,pointId_3,pointId_4";

    public static bool cb(
      CSVReader csv_reader,
      ItemToFieldTable.ItemToFieldData data,
      ref uint key1,
      ref uint key2)
    {
      data.itemId = key1;
      data.key2 = key2;
      csv_reader.Pop(ref data.fieldId);
      for (int index = 0; index < 5; ++index)
      {
        csv_reader.Pop(ref data.enemyId[index]);
        if (data.enemyId[index] != 0U && Singleton<EnemyFieldDropItemTable>.IsValid())
          Singleton<EnemyFieldDropItemTable>.I.Add(data.enemyId[index], data.itemId, data.fieldId, new List<int>()
          {
            0
          });
      }
      for (int index = 0; index < 5; ++index)
        csv_reader.Pop(ref data.pointId[index]);
      return true;
    }

    public static string CBSecondKey(CSVReader csv, int table_data_num)
    {
      return table_data_num.ToString();
    }

    public uint GetSecondKey() => this.key2;
  }

  public class CandidateField
  {
    public uint enemyId;
    public FieldMapTable.FieldMapTableData mapData;
  }

  public enum FIELD_AVAILABLE_CHOICES
  {
    NOT_TRAVELED = -3, // 0xFFFFFFFD
    NOT_AVILABLE_MANAGER = -2, // 0xFFFFFFFE
    TOO_BIG_GRADE = -1, // 0xFFFFFFFF
    AVAILABLE = 0,
  }

  public class ItemDetailToFieldData
  {
    public FieldMapTable.FieldMapTableData mapData;
  }

  public class ItemDetailToFieldEnemyData : ItemToFieldTable.ItemDetailToFieldData
  {
    public uint[] enemyID;

    public ItemDetailToFieldEnemyData(FieldMapTable.FieldMapTableData map, uint[] enemy)
    {
      this.mapData = map;
      this.enemyID = enemy;
    }
  }

  public class ItemDetailToFieldPointData : ItemToFieldTable.ItemDetailToFieldData
  {
    public uint pointID;
    public FieldMapTable.GatherPointTableData pointTable;
    public FieldMapTable.GatherPointViewTableData pointViewTable;

    public ItemDetailToFieldPointData(
      FieldMapTable.FieldMapTableData map,
      uint point,
      FieldMapTable.GatherPointTableData point_table,
      FieldMapTable.GatherPointViewTableData point_view_table)
    {
      this.mapData = map;
      this.pointID = point;
      this.pointTable = point_table;
      this.pointViewTable = point_view_table;
    }
  }

  public class RecommendFieldData
  {
    public ItemToFieldTable.ItemDetailToFieldData[] dropFieldData;
    public bool isNeedUnknownField;

    public RecommendFieldData(ItemToFieldTable.ItemDetailToFieldData[] _data, bool find_unknown)
    {
      this.dropFieldData = _data;
      this.isNeedUnknownField = find_unknown;
    }
  }
}
