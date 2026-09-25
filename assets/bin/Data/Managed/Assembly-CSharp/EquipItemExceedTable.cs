// Decompiled with JetBrains decompiler
// Type: EquipItemExceedTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
public class EquipItemExceedTable : Singleton<EquipItemExceedTable>, IDataTable
{
  private DoubleUIntKeyTable<EquipItemExceedTable.EquipItemExceedData> tableData;

  public void CreateTable(string csv_text)
  {
    this.tableData = TableUtility.CreateDoubleUIntKeyTable<EquipItemExceedTable.EquipItemExceedData>(csv_text, new TableUtility.CallBackDoubleUIntKeyReadCSV<EquipItemExceedTable.EquipItemExceedData>(EquipItemExceedTable.EquipItemExceedData.cb), "rarity,type,eventId,exchangeItemId,exchangeMoney,exceedItemId0,exceedNum0_1,exceedNum0_2,exceedNum0_3,exceedNum0_4,exceedItemId1,exceedNum1_1,exceedNum1_2,exceedNum1_3,exceedNum1_4,exceedItemId2,exceedNum2_1,exceedNum2_2,exceedNum2_3,exceedNum2_4,exceedItemId3,exceedNum3_1,exceedNum3_2,exceedNum3_3,exceedNum3_4,exceedItemId4,exceedNum4_1,exceedNum4_2,exceedNum4_3,exceedNum4_4", new TableUtility.CallBackDoubleUIntSecondKey(EquipItemExceedTable.EquipItemExceedData.cb_second_key), new TableUtility.CallBackDoubleUIntParseKey(EquipItemExceedTable.EquipItemExceedData.cb_parse_first_key));
    this.tableData.TrimExcess();
  }

  public void AddTable(string csv_text)
  {
    TableUtility.AddDoubleUIntKeyTable<EquipItemExceedTable.EquipItemExceedData>(this.tableData, csv_text, new TableUtility.CallBackDoubleUIntKeyReadCSV<EquipItemExceedTable.EquipItemExceedData>(EquipItemExceedTable.EquipItemExceedData.cb), "rarity,type,eventId,exchangeItemId,exchangeMoney,exceedItemId0,exceedNum0_1,exceedNum0_2,exceedNum0_3,exceedNum0_4,exceedItemId1,exceedNum1_1,exceedNum1_2,exceedNum1_3,exceedNum1_4,exceedItemId2,exceedNum2_1,exceedNum2_2,exceedNum2_3,exceedNum2_4,exceedItemId3,exceedNum3_1,exceedNum3_2,exceedNum3_3,exceedNum3_4,exceedItemId4,exceedNum4_1,exceedNum4_2,exceedNum4_3,exceedNum4_4", new TableUtility.CallBackDoubleUIntSecondKey(EquipItemExceedTable.EquipItemExceedData.cb_second_key), new TableUtility.CallBackDoubleUIntParseKey(EquipItemExceedTable.EquipItemExceedData.cb_parse_first_key));
  }

  public EquipItemExceedTable.EquipItemExceedData GetEquipItemExceedData(
    RARITY_TYPE rarity,
    GET_TYPE getType,
    int eventId = 0)
  {
    if (this.tableData == null)
      return (EquipItemExceedTable.EquipItemExceedData) null;
    if (getType != GET_TYPE.EVENT)
      eventId = 0;
    uint key = (uint) rarity;
    UIntKeyTable<EquipItemExceedTable.EquipItemExceedData> uintKeyTable = this.tableData.Get(key);
    if (uintKeyTable == null)
    {
      Log.Error("EquipItemExceedTable is NULL :: rarity = {0}( {1} )", (object) rarity, (object) key);
      return (EquipItemExceedTable.EquipItemExceedData) null;
    }
    EquipItemExceedTable.EquipItemExceedData equipItemExceedData = uintKeyTable.Find((Predicate<EquipItemExceedTable.EquipItemExceedData>) (data => data.getType == getType && data.eventId == eventId));
    if (equipItemExceedData == null)
    {
      if (getType == GET_TYPE.EVENT)
        equipItemExceedData = uintKeyTable.Find((Predicate<EquipItemExceedTable.EquipItemExceedData>) (data => data.getType == GET_TYPE.FREE && data.eventId == 0));
      else
        Log.Warning("EquipItemExceedTable is NULL :: getType = {0}, eventId = {1}", (object) getType, (object) eventId);
    }
    return equipItemExceedData;
  }

  public EquipItemExceedTable.EquipItemExceedData GetEquipItemExceedDataIncludeLimited(
    EquipItemTable.EquipItemData itemData)
  {
    if (itemData == null)
      return (EquipItemExceedTable.EquipItemExceedData) null;
    EquipItemExceedTable.EquipItemExceedData equipItemExceedData1 = this.GetEquipItemExceedData(itemData.rarity, itemData.getType, itemData.eventId);
    LimitedEquipItemExceedTable.LimitedEquipItemExceedData[] equipItemExceedData2 = Singleton<LimitedEquipItemExceedTable>.I.GetLimitedEquipItemExceedData(itemData);
    List<EquipItemExceedTable.EquipItemExceedData.ExceedNeedItem> exceedNeedItemList = new List<EquipItemExceedTable.EquipItemExceedData.ExceedNeedItem>();
    for (int index1 = 0; index1 < equipItemExceedData2.Length; ++index1)
    {
      EquipItemExceedTable.EquipItemExceedData.ExceedNeedItem exceedNeedItem = new EquipItemExceedTable.EquipItemExceedData.ExceedNeedItem();
      exceedNeedItem.itemId = equipItemExceedData2[index1].exceed.itemId;
      exceedNeedItem.num = new uint[4];
      if (exceedNeedItem.num.Length == equipItemExceedData2[index1].exceed.num.Length)
      {
        for (int index2 = 0; index2 < exceedNeedItem.num.Length; ++index2)
          exceedNeedItem.num[index2] = equipItemExceedData2[index1].exceed.num[index2];
        exceedNeedItemList.Add(exceedNeedItem);
      }
    }
    EquipItemExceedTable.EquipItemExceedData dataIncludeLimited = equipItemExceedData1.Clone();
    dataIncludeLimited.exceed = ((IEnumerable<EquipItemExceedTable.EquipItemExceedData.ExceedNeedItem>) dataIncludeLimited.exceed).Concat<EquipItemExceedTable.EquipItemExceedData.ExceedNeedItem>((IEnumerable<EquipItemExceedTable.EquipItemExceedData.ExceedNeedItem>) exceedNeedItemList.ToArray()).ToArray<EquipItemExceedTable.EquipItemExceedData.ExceedNeedItem>();
    return dataIncludeLimited;
  }

  public bool IsFreeLapis(RARITY_TYPE rarity, uint lapis_item_id, int eventId = 0)
  {
    if (this.tableData == null)
      return true;
    UIntKeyTable<EquipItemExceedTable.EquipItemExceedData> uintKeyTable = this.tableData.Get((uint) rarity);
    if (uintKeyTable == null)
      return true;
    EquipItemExceedTable.EquipItemExceedData equipItemExceedData = uintKeyTable.Find((Predicate<EquipItemExceedTable.EquipItemExceedData>) (data => (int) data.exchangeItemId == (int) lapis_item_id && data.eventId == eventId));
    return equipItemExceedData == null || equipItemExceedData.getType != GET_TYPE.PAY;
  }

  public class EquipItemExceedData
  {
    public RARITY_TYPE rarity;
    public GET_TYPE getType;
    public int eventId;
    public uint exchangeItemId;
    public uint exchangeMoney;
    public EquipItemExceedTable.EquipItemExceedData.ExceedNeedItem[] exceed;
    public const string NT = "rarity,type,eventId,exchangeItemId,exchangeMoney,exceedItemId0,exceedNum0_1,exceedNum0_2,exceedNum0_3,exceedNum0_4,exceedItemId1,exceedNum1_1,exceedNum1_2,exceedNum1_3,exceedNum1_4,exceedItemId2,exceedNum2_1,exceedNum2_2,exceedNum2_3,exceedNum2_4,exceedItemId3,exceedNum3_1,exceedNum3_2,exceedNum3_3,exceedNum3_4,exceedItemId4,exceedNum4_1,exceedNum4_2,exceedNum4_3,exceedNum4_4";

    public EquipItemExceedTable.EquipItemExceedData Clone()
    {
      return new EquipItemExceedTable.EquipItemExceedData()
      {
        rarity = this.rarity,
        getType = this.getType,
        eventId = this.eventId,
        exchangeItemId = this.exchangeItemId,
        exchangeMoney = this.exchangeMoney,
        exceed = this.exceed
      };
    }

    public static uint cb_parse_first_key(string key_str)
    {
      return (uint) (RARITY_TYPE) Enum.Parse(typeof (RARITY_TYPE), key_str);
    }

    public static string cb_second_key(CSVReader csv, int table_data_num)
    {
      return table_data_num.ToString();
    }

    public static bool cb(
      CSVReader csv_reader,
      EquipItemExceedTable.EquipItemExceedData data,
      ref uint key1,
      ref uint key2)
    {
      data.rarity = (RARITY_TYPE) key1;
      csv_reader.Pop<GET_TYPE>(ref data.getType);
      csv_reader.Pop(ref data.eventId);
      csv_reader.Pop(ref data.exchangeItemId);
      csv_reader.Pop(ref data.exchangeMoney);
      List<EquipItemExceedTable.EquipItemExceedData.ExceedNeedItem> exceedNeedItemList = new List<EquipItemExceedTable.EquipItemExceedData.ExceedNeedItem>();
      for (int index1 = 0; index1 < 5; ++index1)
      {
        uint num1 = 0;
        csv_reader.Pop(ref num1);
        if (num1 != 0U)
        {
          EquipItemExceedTable.EquipItemExceedData.ExceedNeedItem item = new EquipItemExceedTable.EquipItemExceedData.ExceedNeedItem();
          item.itemId = num1;
          item.num = new uint[4];
          for (int index2 = 0; index2 < 4; ++index2)
            csv_reader.Pop(ref item.num[index2]);
          if (exceedNeedItemList.Find((Predicate<EquipItemExceedTable.EquipItemExceedData.ExceedNeedItem>) (_data => (int) _data.itemId == (int) item.itemId)) == null)
            exceedNeedItemList.Add(item);
        }
        else
        {
          for (int index3 = 0; index3 < 4; ++index3)
          {
            uint num2 = 0;
            csv_reader.Pop(ref num2);
          }
        }
      }
      data.exceed = exceedNeedItemList.ToArray();
      return true;
    }

    public class ExceedNeedItem
    {
      public uint itemId;
      public uint[] num;

      public uint getNeedNum(int exceedCnt)
      {
        return exceedCnt <= 0 || exceedCnt > this.num.Length ? 0U : this.num[exceedCnt - 1];
      }
    }
  }
}
