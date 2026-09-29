// Decompiled with JetBrains decompiler
// Type: ItemTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class ItemTable : Singleton<ItemTable>, IDataTable
{
  private UIntKeyTable<ItemTable.ItemData> itemTable;

  public void CreateTable(string csv_text)
  {
    this.itemTable = TableUtility.CreateUIntKeyTable<ItemTable.ItemData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<ItemTable.ItemData>(ItemTable.ItemData.cb), "itemId,itemType,getType,eventId,name,text,enemyIconID,enemyIconID2,rarity,iconID,price,cantSell,element,effectType_0,effectType_1,effectType_2,effectTime,startDate,endDate");
    this.itemTable.TrimExcess();
  }

  public void AddTable(string csv_text)
  {
    TableUtility.AddUIntKeyTable<ItemTable.ItemData>(this.itemTable, csv_text, new TableUtility.CallBackUIntKeyReadCSV<ItemTable.ItemData>(ItemTable.ItemData.cb), "itemId,itemType,getType,eventId,name,text,enemyIconID,enemyIconID2,rarity,iconID,price,cantSell,element,effectType_0,effectType_1,effectType_2,effectTime,startDate,endDate");
  }

  public ItemTable.ItemData GetItemData(uint id)
  {
    if (this.itemTable == null)
      return (ItemTable.ItemData) null;
    ItemTable.ItemData itemData = this.itemTable.Get(id);
    if (itemData == null)
    {
      Log.TableError((object) this, id);
      itemData = new ItemTable.ItemData();
      itemData.name = Log.NON_DATA_NAME;
    }
    return itemData;
  }

  public bool IsExistItemData(uint id) => this.itemTable != null && this.itemTable.Get(id) != null;

  public static int ChangeItemIdToSkillItemIdIfNeed(int id)
  {
    switch (id)
    {
      case 1001000:
        return 401900001;
      case 1001001:
        return 401900002;
      case 1001002:
        return 401900003;
      case 1001003:
        return 401900004;
      default:
        return id;
    }
  }

  public List<ItemTable.ItemData> GetItemTypeItemData(ITEM_TYPE type)
  {
    List<ItemTable.ItemData> returnData = new List<ItemTable.ItemData>();
    this.itemTable.ForEach((Action<ItemTable.ItemData>) (data =>
    {
      if (data.type != type)
        return;
      returnData.Add(data);
    }));
    return returnData;
  }

  public class ItemData
  {
    public uint id;
    public ITEM_TYPE type;
    public GET_TYPE getType;
    public int eventId;
    public string name;
    public string text;
    public int enemyIconID;
    public int enemyIconID2;
    public RARITY_TYPE rarity;
    public int iconID;
    public int price;
    public bool cantSale;
    public int element;
    public USE_ITEM_EFFECT_TYPE[] useEffectTypes;
    public int effectTime;
    public DateTime startDate;
    public DateTime endDate;
    public const string NT = "itemId,itemType,getType,eventId,name,text,enemyIconID,enemyIconID2,rarity,iconID,price,cantSell,element,effectType_0,effectType_1,effectType_2,effectTime,startDate,endDate";

    public static bool cb(CSVReader csv_reader, ItemTable.ItemData data, ref uint key)
    {
      data.id = key;
      csv_reader.Pop<ITEM_TYPE>(ref data.type);
      csv_reader.Pop<GET_TYPE>(ref data.getType);
      csv_reader.Pop(ref data.eventId);
      csv_reader.Pop(ref data.name);
      csv_reader.Pop(ref data.text);
      csv_reader.Pop(ref data.enemyIconID);
      csv_reader.Pop(ref data.enemyIconID2);
      csv_reader.Pop<RARITY_TYPE>(ref data.rarity);
      csv_reader.Pop(ref data.iconID);
      csv_reader.Pop(ref data.price);
      csv_reader.Pop(ref data.cantSale);
      csv_reader.Pop(ref data.element);
      List<USE_ITEM_EFFECT_TYPE> useItemEffectTypeList = new List<USE_ITEM_EFFECT_TYPE>();
      for (int index = 0; index < 3; ++index)
      {
        USE_ITEM_EFFECT_TYPE useItemEffectType = USE_ITEM_EFFECT_TYPE.NONE;
        csv_reader.Pop<USE_ITEM_EFFECT_TYPE>(ref useItemEffectType);
        if (useItemEffectType != USE_ITEM_EFFECT_TYPE.NONE)
          useItemEffectTypeList.Add(useItemEffectType);
      }
      data.useEffectTypes = useItemEffectTypeList.ToArray();
      csv_reader.Pop(ref data.effectTime);
      string empty1 = string.Empty;
      csv_reader.Pop(ref empty1);
      if (!string.IsNullOrEmpty(empty1))
        DateTime.TryParse(empty1, out data.startDate);
      string empty2 = string.Empty;
      csv_reader.Pop(ref empty2);
      if (!string.IsNullOrEmpty(empty2))
        DateTime.TryParse(empty2, out data.endDate);
      return true;
    }
  }
}
