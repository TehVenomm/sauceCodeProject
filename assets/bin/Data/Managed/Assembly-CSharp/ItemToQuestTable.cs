// Decompiled with JetBrains decompiler
// Type: ItemToQuestTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

#nullable disable
public class ItemToQuestTable : Singleton<ItemToQuestTable>, IDataTable
{
  private DoubleUIntKeyTable<ItemToQuestTable.ItemToQuestData> itemToQuestTable;
  private const int CHOICE_NUM = 2;

  public static DoubleUIntKeyTable<ItemToQuestTable.ItemToQuestData> CreateTableCSV(string csv_text)
  {
    return TableUtility.CreateDoubleUIntKeyTable<ItemToQuestTable.ItemToQuestData>(csv_text, new TableUtility.CallBackDoubleUIntKeyReadCSV<ItemToQuestTable.ItemToQuestData>(ItemToQuestTable.ItemToQuestData.cb), "itemId,questId", new TableUtility.CallBackDoubleUIntSecondKey(ItemToQuestTable.ItemToQuestData.CBSecondKey));
  }

  public void CreateTable(string csv_text)
  {
    this.itemToQuestTable = ItemToQuestTable.CreateTableCSV(csv_text);
  }

  public void CreateTable(string csv_text, TableUtility.Progress progress)
  {
    this.itemToQuestTable = TableUtility.CreateDoubleUIntKeyTable<ItemToQuestTable.ItemToQuestData>(csv_text, new TableUtility.CallBackDoubleUIntKeyReadCSV<ItemToQuestTable.ItemToQuestData>(ItemToQuestTable.ItemToQuestData.cb), "itemId,questId", new TableUtility.CallBackDoubleUIntSecondKey(ItemToQuestTable.ItemToQuestData.CBSecondKey), progress: progress);
  }

  public void AddTable(string csv_text)
  {
    TableUtility.AddDoubleUIntKeyTable<ItemToQuestTable.ItemToQuestData>(this.itemToQuestTable, csv_text, new TableUtility.CallBackDoubleUIntKeyReadCSV<ItemToQuestTable.ItemToQuestData>(ItemToQuestTable.ItemToQuestData.cb), "itemId,questId", new TableUtility.CallBackDoubleUIntSecondKey(ItemToQuestTable.ItemToQuestData.CBSecondKey));
  }

  public static DoubleUIntKeyTable<ItemToQuestTable.ItemToQuestData> CreateTableBinary(byte[] bytes)
  {
    DoubleUIntKeyTable<ItemToQuestTable.ItemToQuestData> tableBinary = new DoubleUIntKeyTable<ItemToQuestTable.ItemToQuestData>();
    BinaryTableReader reader = new BinaryTableReader(bytes);
    while (reader.MoveNext())
    {
      uint key1 = reader.ReadUInt32();
      uint key2 = 0;
      UIntKeyTable<ItemToQuestTable.ItemToQuestData> uintKeyTable = tableBinary.Get(key1);
      if (uintKeyTable != null)
        key2 = (uint) uintKeyTable.GetCount();
      ItemToQuestTable.ItemToQuestData itemToQuestData = new ItemToQuestTable.ItemToQuestData();
      itemToQuestData.LoadFromBinary(reader, ref key1, ref key2);
      tableBinary.Add(key1, key2, itemToQuestData);
    }
    return tableBinary;
  }

  public void CreateTable(byte[] bytes)
  {
    this.itemToQuestTable = ItemToQuestTable.CreateTableBinary(bytes);
  }

  public void AddTableFromAPI(uint itemId, List<int> questIds)
  {
    if (this.itemToQuestTable == null)
      this.itemToQuestTable = new DoubleUIntKeyTable<ItemToQuestTable.ItemToQuestData>();
    this.itemToQuestTable.Get(itemId)?.Clear();
    for (int index = 0; index < questIds.Count; ++index)
    {
      ItemToQuestTable.ItemToQuestData itemToQuestData = new ItemToQuestTable.ItemToQuestData();
      itemToQuestData.LoadFromAPI(itemId, (uint) questIds[index], (uint) index);
      this.itemToQuestTable.Add(itemId, (uint) index, itemToQuestData);
    }
    this.InitDependencyData();
  }

  public void InitDependencyData()
  {
    if (!Singleton<QuestTable>.IsValid())
      return;
    this.itemToQuestTable.ForEach((Action<UIntKeyTable<ItemToQuestTable.ItemToQuestData>>) (x => x.ForEach((Action<ItemToQuestTable.ItemToQuestData>) (data =>
    {
      QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(data.questId);
      if (questData == null)
        return;
      data.grade = questData.grade;
    }))));
  }

  public QuestTable.QuestTableData[] GetQuestTableFromItemID(uint item_id)
  {
    return this._GetQuestTableFromItemID(item_id);
  }

  public uint[] GetCandidateEnemies(uint item_id, int trim_count = -1)
  {
    List<uint> uintList = new List<uint>();
    List<string> stringList = new List<string>();
    QuestTable.QuestTableData[] questTableFromItemId = Singleton<ItemToQuestTable>.I.GetHappenQuestTableFromItemID(item_id);
    if (questTableFromItemId != null && questTableFromItemId.Length != 0)
    {
      foreach (QuestTable.QuestTableData questTableData in questTableFromItemId)
      {
        uint mainEnemyId = (uint) questTableData.GetMainEnemyID();
        EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData(mainEnemyId);
        if (enemyData != null && !stringList.Contains(enemyData.name))
        {
          uintList.Add(mainEnemyId);
          stringList.Add(enemyData.name);
        }
      }
    }
    return trim_count > 0 ? uintList.GetRange(0, Mathf.Min(uintList.Count, trim_count)).ToArray() : uintList.ToArray();
  }

  public QuestTable.QuestTableData[] GetDistinctQuestFromItemID(
    uint item_id,
    QUEST_TYPE? quest_type = null)
  {
    return this._GetDistinctQuestFromItemID(item_id, quest_type);
  }

  private QuestTable.QuestTableData[] _GetDistinctQuestFromItemID(
    uint item_id,
    QUEST_TYPE? quest_type = null)
  {
    QuestTable.QuestTableData[] questTableFromItemId = this._GetQuestTableFromItemID(item_id, quest_type);
    if (questTableFromItemId == null)
      return (QuestTable.QuestTableData[]) null;
    List<QuestTable.QuestTableData> questTableDataList = new List<QuestTable.QuestTableData>();
    List<string> stringList = new List<string>();
    foreach (QuestTable.QuestTableData questTableData in questTableFromItemId)
    {
      string enemyName = Singleton<EnemyTable>.I.GetEnemyName((uint) questTableData.GetMainEnemyID());
      if (!string.IsNullOrEmpty(enemyName) && !stringList.Contains(enemyName))
      {
        stringList.Add(enemyName);
        questTableDataList.Add(questTableData);
      }
    }
    return questTableDataList.ToArray();
  }

  public QuestTable.QuestTableData[] GetDistinctHappenQuestFromItemID(uint item_id)
  {
    return this._GetDistinctQuestFromItemID(item_id, new QUEST_TYPE?(QUEST_TYPE.HAPPEN));
  }

  public QuestTable.QuestTableData[] GetHappenQuestTableFromItemID(uint item_id)
  {
    return this._GetQuestTableFromItemID(item_id, new QUEST_TYPE?(QUEST_TYPE.HAPPEN));
  }

  private QuestTable.QuestTableData[] _GetQuestTableFromItemID(uint item_id, QUEST_TYPE? quest_type = null)
  {
    if (this.itemToQuestTable == null)
      return (QuestTable.QuestTableData[]) null;
    UIntKeyTable<ItemToQuestTable.ItemToQuestData> uintKeyTable = this.itemToQuestTable.Get(item_id);
    if (uintKeyTable == null)
      return (QuestTable.QuestTableData[]) null;
    List<QuestTable.QuestTableData> list = new List<QuestTable.QuestTableData>();
    uintKeyTable.ForEach((Action<ItemToQuestTable.ItemToQuestData>) (data =>
    {
      QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(data.questId);
      if (questData == null || quest_type.HasValue && (quest_type.Value != questData.questType || quest_type.Value == QUEST_TYPE.HAPPEN && (!Singleton<QuestToFieldTable>.IsValid() || !Singleton<QuestToFieldTable>.I.IsValidHappenQuest(data.questId))))
        return;
      list.Add(questData);
    }));
    return list.Count == 0 ? (QuestTable.QuestTableData[]) null : list.ToArray();
  }

  public ItemToQuestTable.RecommendQuestData GetRecommendQuest(uint item_id)
  {
    QuestTable.QuestTableData[] table = new QuestTable.QuestTableData[2];
    for (int index = 0; index < 2; ++index)
      table[index] = (QuestTable.QuestTableData) null;
    ItemToQuestTable.RecommendQuestData recommendQuest = new ItemToQuestTable.RecommendQuestData(table, false);
    QuestTable.QuestTableData[] questTableFromItemId = this.GetQuestTableFromItemID(item_id);
    if (questTableFromItemId == null)
      return recommendQuest;
    bool find_unknown_quest = false;
    QuestTable.QuestTableData[] A = new QuestTable.QuestTableData[2];
    QuestTable.QuestTableData[] B = new QuestTable.QuestTableData[2];
    for (int index = 0; index < 2; ++index)
    {
      A[index] = (QuestTable.QuestTableData) null;
      B[index] = (QuestTable.QuestTableData) null;
    }
    Array.ForEach<QuestTable.QuestTableData>(questTableFromItemId, (Action<QuestTable.QuestTableData>) (data =>
    {
      bool find_unknown_quest1 = false;
      switch (data.questType)
      {
        case QUEST_TYPE.NORMAL:
        case QUEST_TYPE.EVENT:
          this.UpdateRecommendQuestPriority(A, data, false, out find_unknown_quest1);
          break;
        case QUEST_TYPE.ORDER:
          this.UpdateRecommendQuestPriority(B, data, true, out find_unknown_quest1);
          break;
        case QUEST_TYPE.SERIES_ARENA:
          DeliveryTable.DeliveryData deliveryData = Singleton<DeliveryTable>.I.GetDeliveryTableDataFromQuestId(data.questID);
          if (MonoBehaviourSingleton<QuestManager>.I.eventList.Where<Network.EventData>((Func<Network.EventData, bool>) (e => e.eventId == deliveryData.eventID)).FirstOrDefault<Network.EventData>() != null)
          {
            this.UpdateRecommendQuestPriority(A, data, false, out find_unknown_quest1);
            break;
          }
          break;
        default:
          return;
      }
      if (!find_unknown_quest1)
        return;
      find_unknown_quest = true;
    }));
    bool flag1 = A != null && A.Length != 0 && A[0] != null;
    bool flag2 = flag1 && A != null && A.Length > 1 && A[1] != null;
    int num1 = B == null || B.Length == 0 ? 0 : (B[0] != null ? 1 : 0);
    bool flag3 = num1 != 0 && B != null && B.Length > 1 && B[1] != null;
    int num2 = 0;
    if (flag1)
      recommendQuest.recommendData[num2++] = A[0];
    if (num1 != 0)
      recommendQuest.recommendData[num2++] = B[0];
    if (flag2 && num2 < 2)
      recommendQuest.recommendData[num2++] = A[1];
    if (flag3 && num2 < 2)
    {
      QuestTable.QuestTableData[] recommendData = recommendQuest.recommendData;
      int index = num2;
      int num3 = index + 1;
      QuestTable.QuestTableData questTableData = B[1];
      recommendData[index] = questTableData;
    }
    if (((flag1 ? 0 : (!flag2 ? 1 : 0)) & (find_unknown_quest ? 1 : 0)) != 0)
      recommendQuest.isNeedUnknownQuest = true;
    return recommendQuest;
  }

  private void UpdateRecommendQuestPriority(
    QuestTable.QuestTableData[] quest_table,
    QuestTable.QuestTableData _table,
    bool is_order,
    out bool find_unknown_quest)
  {
    find_unknown_quest = false;
    if (!is_order && !this.IsOpenedQuest(_table))
    {
      find_unknown_quest = true;
    }
    else
    {
      if (is_order)
      {
        QuestItemInfo questItem = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem(_table.questID);
        if (questItem == null || QuestTable.GetQuestNum(questItem) <= 0)
          return;
      }
      for (int index = 0; index < 2; ++index)
      {
        if (quest_table[index] == null)
        {
          quest_table[index] = _table;
          if (index == 0)
            return;
          this.SortRecommendQuest(quest_table, _table, is_order);
          return;
        }
      }
      int index1 = 1;
      if (!this.IsSelectPriorityQuestInfo(quest_table[index1], _table))
        return;
      this.SortRecommendQuest(quest_table, _table, is_order);
    }
  }

  private bool IsSelectPriorityQuestInfo(
    QuestTable.QuestTableData now_info,
    QuestTable.QuestTableData check_data)
  {
    if (now_info == null)
      return true;
    bool flag = false;
    if (this.GetTypePriority(now_info.questType) == this.GetTypePriority(check_data.questType) && now_info.grade <= check_data.grade)
    {
      if (now_info.grade < check_data.grade || now_info.questID > check_data.questID & now_info.grade == check_data.grade)
        flag = true;
    }
    else if (this.GetTypePriority(now_info.questType) < this.GetTypePriority(check_data.questType) && now_info.grade <= check_data.grade)
      flag = true;
    else if (this.GetTypePriority(now_info.questType) > this.GetTypePriority(check_data.questType) && now_info.grade < check_data.grade)
      flag = true;
    return flag;
  }

  private int GetTypePriority(QUEST_TYPE now_type)
  {
    int typePriority = 0;
    switch (now_type)
    {
      case QUEST_TYPE.NORMAL:
        typePriority = 1;
        break;
      case QUEST_TYPE.EVENT:
        typePriority = 2;
        break;
    }
    return typePriority;
  }

  private void SortRecommendQuest(
    QuestTable.QuestTableData[] quest_table,
    QuestTable.QuestTableData _table,
    bool is_order)
  {
    for (int length = quest_table.Length; length > 0; --length)
    {
      QuestTable.QuestTableData now_info;
      QuestTable.QuestTableData check_data;
      if (length == quest_table.Length)
      {
        now_info = quest_table[length - 1];
        check_data = _table;
      }
      else
      {
        now_info = quest_table[length - 1];
        check_data = quest_table[length];
      }
      if (now_info != null && check_data != null)
      {
        bool flag = false;
        if (is_order)
        {
          if (now_info.rarity == check_data.rarity)
          {
            if (now_info.questID > check_data.questID)
              flag = true;
          }
          else if (now_info.rarity > check_data.rarity)
            flag = true;
        }
        else if (this.IsSelectPriorityQuestInfo(now_info, check_data))
          flag = true;
        if (!flag)
          break;
        QuestTable.QuestTableData questTableData = now_info;
        if (length == quest_table.Length)
        {
          quest_table[length - 1] = check_data;
        }
        else
        {
          quest_table[length - 1] = quest_table[length];
          quest_table[length] = questTableData;
        }
      }
    }
  }

  public ItemToQuestTable.QUEST_AVAILABLE_CHOICES IsAvailableQuest(
    QuestTable.QuestTableData quest_table)
  {
    ItemToQuestTable.QUEST_AVAILABLE_CHOICES available_choices;
    this._IsOpenedQuest(quest_table, out available_choices);
    return available_choices;
  }

  private bool IsOpenedQuest(QuestTable.QuestTableData quest_table)
  {
    return this._IsOpenedQuest(quest_table, out ItemToQuestTable.QUEST_AVAILABLE_CHOICES _);
  }

  private bool _IsOpenedQuest(
    QuestTable.QuestTableData quest_table,
    out ItemToQuestTable.QUEST_AVAILABLE_CHOICES available_choices)
  {
    if (quest_table.questType == QUEST_TYPE.NORMAL && quest_table.grade > MonoBehaviourSingleton<UserInfoManager>.I.userStatus.questGrade)
    {
      available_choices = ItemToQuestTable.QUEST_AVAILABLE_CHOICES.TOO_BIG_GRADE;
      return false;
    }
    if (quest_table.questType == QUEST_TYPE.HAPPEN)
    {
      ClearStatusQuest clearStatusQuest = MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.Find((Predicate<ClearStatusQuest>) (data => (long) data.questId == (long) quest_table.questID));
      if (clearStatusQuest == null || clearStatusQuest.questStatus < 3)
      {
        available_choices = ItemToQuestTable.QUEST_AVAILABLE_CHOICES.NOT_SELECT_TYPE;
        return false;
      }
    }
    if (quest_table.questType == QUEST_TYPE.GATE)
    {
      available_choices = ItemToQuestTable.QUEST_AVAILABLE_CHOICES.NOT_SELECT_TYPE;
      return false;
    }
    if (quest_table.appearQuestId != 0U)
    {
      ClearStatusQuest clearStatusQuest = MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.Find((Predicate<ClearStatusQuest>) (data => (long) data.questId == (long) quest_table.appearQuestId));
      if (clearStatusQuest == null || clearStatusQuest.questStatus < 3)
      {
        available_choices = ItemToQuestTable.QUEST_AVAILABLE_CHOICES.NOT_CLEAR_FOR_VISIBLE_REQUIRE_QUEST;
        return false;
      }
    }
    available_choices = ItemToQuestTable.QUEST_AVAILABLE_CHOICES.AVAILABLE;
    return true;
  }

  public class ItemToQuestData
  {
    public uint itemId;
    public uint questId;
    public int grade;
    private uint key2;
    public const string NT = "itemId,questId";

    public static bool cb(
      CSVReader csv_reader,
      ItemToQuestTable.ItemToQuestData data,
      ref uint key1,
      ref uint key2)
    {
      data.itemId = key1;
      data.key2 = key2;
      csv_reader.Pop(ref data.questId);
      return true;
    }

    public static string CBSecondKey(CSVReader csv, int table_data_num)
    {
      return table_data_num.ToString();
    }

    public uint GetSecondKey() => this.key2;

    public void LoadFromBinary(BinaryTableReader reader, ref uint key1, ref uint key2)
    {
      this.itemId = key1;
      this.key2 = key2;
      this.questId = reader.ReadUInt32();
    }

    public void LoadFromAPI(uint iId, uint qId, uint k2)
    {
      this.itemId = iId;
      this.questId = qId;
      this.key2 = k2;
    }

    public void DumpBinary(BinaryWriter writer) => writer.Write(this.questId);

    public override bool Equals(object obj)
    {
      return obj != null && obj is ItemToQuestTable.ItemToQuestData itemToQuestData && (int) this.itemId == (int) itemToQuestData.itemId && (int) this.questId == (int) itemToQuestData.questId && this.grade == itemToQuestData.grade && (int) this.key2 == (int) itemToQuestData.key2;
    }

    public override int GetHashCode() => base.GetHashCode();
  }

  public class RecommendQuestData
  {
    public QuestTable.QuestTableData[] recommendData;
    public bool isNeedUnknownQuest;

    public RecommendQuestData(QuestTable.QuestTableData[] table, bool need_unknown)
    {
      this.recommendData = table;
      this.isNeedUnknownQuest = need_unknown;
    }
  }

  public enum QUEST_AVAILABLE_CHOICES
  {
    NOT_SELECT_TYPE = -6, // 0xFFFFFFFA
    NOT_FOUND_AREA_DATA = -5, // 0xFFFFFFFB
    NOT_AVILABLE_MANAGER = -4, // 0xFFFFFFFC
    NOT_CLEAR_FOR_VISIBLE_REQUIRE_QUEST = -3, // 0xFFFFFFFD
    TOO_BIG_GRADE = -2, // 0xFFFFFFFE
    NOT_FOUND_LOCATION_DATA = -1, // 0xFFFFFFFF
    AVAILABLE = 0,
  }
}
