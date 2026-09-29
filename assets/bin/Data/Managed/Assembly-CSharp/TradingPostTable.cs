// Decompiled with JetBrains decompiler
// Type: TradingPostTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class TradingPostTable : Singleton<TradingPostTable>, IDataTable
{
  private UIntKeyTable<TradingPostTable.ItemData> itemTable;

  public UIntKeyTable<TradingPostTable.ItemData> ItemTable => this.itemTable;

  public void CreateTable(string csv_text)
  {
    this.itemTable = TableUtility.CreateUIntKeyTable<TradingPostTable.ItemData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<TradingPostTable.ItemData>(TradingPostTable.ItemData.cb), "itemId,itemType,itemName,maxQuantity,cantSell,startDate,endDate");
    this.itemTable.TrimExcess();
  }

  public void AddTable(string csv_text)
  {
    TableUtility.AddUIntKeyTable<TradingPostTable.ItemData>(this.itemTable, csv_text, new TableUtility.CallBackUIntKeyReadCSV<TradingPostTable.ItemData>(TradingPostTable.ItemData.cb), "itemId,itemType,itemName,maxQuantity,cantSell,startDate,endDate");
  }

  public void CreateTableFromInternal(string encrypted_csv_text)
  {
    this.CreateTable(DataTableManager.Decrypt(encrypted_csv_text));
  }

  public TradingPostTable.ItemData GetItemData(uint id)
  {
    if (this.itemTable == null)
      return (TradingPostTable.ItemData) null;
    TradingPostTable.ItemData itemData = this.itemTable.Get(id);
    if (itemData == null)
    {
      Log.TableError((object) this, id);
      itemData = new TradingPostTable.ItemData();
    }
    return itemData;
  }

  public bool IsExistItemData(uint id) => this.itemTable != null && this.itemTable.Get(id) != null;

  public class ItemData
  {
    public uint itemId;
    public string itemType;
    public string itemName;
    public uint maxQuantity;
    public bool cantSell;
    public string startDate;
    public string endDate;
    public const string NT = "itemId,itemType,itemName,maxQuantity,cantSell,startDate,endDate";

    public static bool cb(CSVReader csv_reader, TradingPostTable.ItemData data, ref uint key)
    {
      data.itemId = key;
      csv_reader.Pop(ref data.itemType);
      csv_reader.Pop(ref data.itemName);
      csv_reader.Pop(ref data.maxQuantity);
      csv_reader.Pop(ref data.cantSell);
      csv_reader.Pop(ref data.startDate);
      csv_reader.Pop(ref data.endDate);
      return true;
    }
  }
}
