// Decompiled with JetBrains decompiler
// Type: LimitedEquipItemExceedTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
public class LimitedEquipItemExceedTable : Singleton<LimitedEquipItemExceedTable>, IDataTable
{
  private UIntKeyTable<LimitedEquipItemExceedTable.LimitedEquipItemExceedData> limitedEquipItemExceedTable;

  public void CreateTable(string csv_text)
  {
    this.limitedEquipItemExceedTable = TableUtility.CreateUIntKeyTable<LimitedEquipItemExceedTable.LimitedEquipItemExceedData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<LimitedEquipItemExceedTable.LimitedEquipItemExceedData>(LimitedEquipItemExceedTable.LimitedEquipItemExceedData.cb), "id,rarity,equipmentType,getType,eventId,equipmentId,exceedItemId,exceedNum1,exceedNum2,exceedNum3,exceedNum4");
    this.limitedEquipItemExceedTable.TrimExcess();
  }

  public LimitedEquipItemExceedTable.LimitedEquipItemExceedData[] GetLimitedEquipItemExceedData(
    EquipItemTable.EquipItemData itemData)
  {
    if (this.limitedEquipItemExceedTable == null)
      return (LimitedEquipItemExceedTable.LimitedEquipItemExceedData[]) null;
    LimitedEquipItemExceedTable.LimitedEquipItemExceedData[] validItemData = this.GetValidItemData();
    List<LimitedEquipItemExceedTable.LimitedEquipItemExceedData> source = new List<LimitedEquipItemExceedTable.LimitedEquipItemExceedData>();
    for (int index = 0; index < validItemData.Length; ++index)
    {
      if ((long) itemData.id == (long) validItemData[index].equipmentId)
      {
        source.Add(validItemData[index]);
      }
      else
      {
        if (itemData.rarity == validItemData[index].rarity && itemData.type == validItemData[index].equipmentType && itemData.getType == validItemData[index].getType)
        {
          if (validItemData[index].getType == GET_TYPE.EVENT)
          {
            if (itemData.eventId == validItemData[index].eventId)
            {
              source.Add(validItemData[index]);
              continue;
            }
          }
          else
          {
            source.Add(validItemData[index]);
            continue;
          }
        }
        if (validItemData[index].equipmentType == EQUIPMENT_TYPE.NONE && validItemData[index].getType == GET_TYPE.NONE)
        {
          if (itemData.rarity == validItemData[index].rarity)
            source.Add(validItemData[index]);
        }
        else if (validItemData[index].equipmentType == EQUIPMENT_TYPE.NONE)
        {
          if (itemData.rarity == validItemData[index].rarity && itemData.getType == validItemData[index].getType)
            source.Add(validItemData[index]);
        }
        else if (validItemData[index].getType == GET_TYPE.NONE && itemData.rarity == validItemData[index].rarity && itemData.type == validItemData[index].equipmentType)
          source.Add(validItemData[index]);
      }
    }
    source.Sort((Comparison<LimitedEquipItemExceedTable.LimitedEquipItemExceedData>) ((a, b) => (int) a.id - (int) b.id));
    return source.Distinct<LimitedEquipItemExceedTable.LimitedEquipItemExceedData>().ToArray<LimitedEquipItemExceedTable.LimitedEquipItemExceedData>();
  }

  private LimitedEquipItemExceedTable.LimitedEquipItemExceedData[] GetValidItemData()
  {
    DateTime now = TimeManager.GetNow();
    DateTime dateDefault = new DateTime();
    List<LimitedEquipItemExceedTable.LimitedEquipItemExceedData> validData = new List<LimitedEquipItemExceedTable.LimitedEquipItemExceedData>();
    this.limitedEquipItemExceedTable.ForEach((Action<LimitedEquipItemExceedTable.LimitedEquipItemExceedData>) (o =>
    {
      ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(o.exceed.itemId);
      if (itemData == null || !(itemData.startDate <= now))
        return;
      if (itemData.endDate.CompareTo(dateDefault) == 0 || itemData.endDate > now)
      {
        validData.Add(o);
      }
      else
      {
        if (MonoBehaviourSingleton<InventoryManager>.I.GetHaveingItemNum(itemData.id) <= 0)
          return;
        validData.Add(o);
      }
    }));
    return validData.ToArray();
  }

  public bool IsLimitedLapis(uint itemId)
  {
    bool isLimitedLapis = false;
    this.limitedEquipItemExceedTable.ForEach((Action<LimitedEquipItemExceedTable.LimitedEquipItemExceedData>) (o =>
    {
      if ((int) o.exceed.itemId != (int) itemId)
        return;
      isLimitedLapis = true;
    }));
    return isLimitedLapis;
  }

  public class LimitedEquipItemExceedData
  {
    public uint id;
    public RARITY_TYPE rarity;
    public EQUIPMENT_TYPE equipmentType;
    public GET_TYPE getType;
    public int eventId;
    public int equipmentId;
    public LimitedEquipItemExceedTable.LimitedEquipItemExceedData.ExceedNeedItem exceed;
    public const string NT = "id,rarity,equipmentType,getType,eventId,equipmentId,exceedItemId,exceedNum1,exceedNum2,exceedNum3,exceedNum4";

    public static bool cb(
      CSVReader csv_reader,
      LimitedEquipItemExceedTable.LimitedEquipItemExceedData data,
      ref uint key)
    {
      data.id = key;
      csv_reader.PopEnum<RARITY_TYPE>(ref data.rarity, RARITY_TYPE.D);
      csv_reader.PopEnum<EQUIPMENT_TYPE>(ref data.equipmentType, EQUIPMENT_TYPE.NONE);
      csv_reader.PopEnum<GET_TYPE>(ref data.getType, GET_TYPE.NONE);
      csv_reader.Pop(ref data.eventId);
      csv_reader.Pop(ref data.equipmentId);
      LimitedEquipItemExceedTable.LimitedEquipItemExceedData.ExceedNeedItem exceedNeedItem = new LimitedEquipItemExceedTable.LimitedEquipItemExceedData.ExceedNeedItem();
      uint num = 0;
      csv_reader.Pop(ref num);
      exceedNeedItem.itemId = num;
      exceedNeedItem.num = new uint[4];
      for (int index = 0; index < exceedNeedItem.num.Length; ++index)
        csv_reader.Pop(ref exceedNeedItem.num[index]);
      data.exceed = exceedNeedItem;
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
