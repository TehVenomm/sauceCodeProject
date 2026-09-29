// Decompiled with JetBrains decompiler
// Type: BuffTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class BuffTable : Singleton<BuffTable>, IDataTable
{
  private UIntKeyTable<BuffTable.BuffData> dataTable;

  public void CreateTable(string text)
  {
    this.dataTable = TableUtility.CreateUIntKeyTable<BuffTable.BuffData>(text, new TableUtility.CallBackUIntKeyReadCSV<BuffTable.BuffData>(BuffTable.BuffData.cb), "id,growId,type,valueType,value,duration,interval");
    this.dataTable.TrimExcess();
  }

  public BuffTable.BuffData GetData(uint id)
  {
    return this.dataTable == null ? (BuffTable.BuffData) null : this.dataTable.Get(id);
  }

  public class BuffData
  {
    public uint id;
    public uint growID;
    public BuffParam.BUFFTYPE type;
    public BuffParam.VALUE_TYPE valueType;
    public int value;
    public float duration;
    public float interval;
    public const string NT = "id,growId,type,valueType,value,duration,interval";

    public static bool cb(CSVReader csv_reader, BuffTable.BuffData data, ref uint key)
    {
      data.id = key;
      csv_reader.Pop(ref data.growID);
      csv_reader.Pop<BuffParam.BUFFTYPE>(ref data.type);
      csv_reader.Pop<BuffParam.VALUE_TYPE>(ref data.valueType);
      csv_reader.Pop(ref data.value);
      csv_reader.Pop(ref data.duration);
      csv_reader.Pop(ref data.interval);
      return true;
    }
  }
}
