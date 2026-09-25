// Decompiled with JetBrains decompiler
// Type: TutorialGearSetTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class TutorialGearSetTable : Singleton<TutorialGearSetTable>, IDataTable
{
  private UIntKeyTable<TutorialGearSetTable.ItemData> itemTable;

  public UIntKeyTable<TutorialGearSetTable.ItemData> ItemTable => this.itemTable;

  public void CreateTable(string csv_text)
  {
    this.itemTable = TableUtility.CreateUIntKeyTable<TutorialGearSetTable.ItemData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<TutorialGearSetTable.ItemData>(TutorialGearSetTable.ItemData.cb), "setId,setName,difficulty,weaponId,helmId,armId,legId,armorId,skillId");
    this.itemTable.TrimExcess();
  }

  public void AddTable(string csv_text)
  {
    TableUtility.AddUIntKeyTable<TutorialGearSetTable.ItemData>(this.itemTable, csv_text, new TableUtility.CallBackUIntKeyReadCSV<TutorialGearSetTable.ItemData>(TutorialGearSetTable.ItemData.cb), "setId,setName,difficulty,weaponId,helmId,armId,legId,armorId,skillId");
  }

  public void CreateTableFromInternal(string encrypted_csv_text)
  {
    this.CreateTable(DataTableManager.Decrypt(encrypted_csv_text));
  }

  public TutorialGearSetTable.ItemData GetItemData(uint id)
  {
    if (this.itemTable == null)
      return (TutorialGearSetTable.ItemData) null;
    TutorialGearSetTable.ItemData itemData = this.itemTable.Get(id);
    if (itemData == null)
    {
      Log.TableError((object) this, id);
      itemData = new TutorialGearSetTable.ItemData();
      itemData.name = Log.NON_DATA_NAME;
    }
    return itemData;
  }

  public bool IsExistItemData(uint id) => this.itemTable != null && this.itemTable.Get(id) != null;

  public class ItemData
  {
    public uint id;
    public string name;
    public string difficulty;
    public uint weaponId;
    public uint helmId;
    public uint armId;
    public uint legId;
    public uint armorId;
    public uint skillItemId;
    public const string NT = "setId,setName,difficulty,weaponId,helmId,armId,legId,armorId,skillId";

    public static bool cb(CSVReader csv_reader, TutorialGearSetTable.ItemData data, ref uint key)
    {
      data.id = key;
      csv_reader.Pop(ref data.name);
      csv_reader.Pop(ref data.difficulty);
      csv_reader.Pop(ref data.weaponId);
      csv_reader.Pop(ref data.helmId);
      csv_reader.Pop(ref data.armId);
      csv_reader.Pop(ref data.legId);
      csv_reader.Pop(ref data.armorId);
      csv_reader.Pop(ref data.skillItemId);
      return true;
    }
  }
}
