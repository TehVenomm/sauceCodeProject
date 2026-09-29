// Decompiled with JetBrains decompiler
// Type: FieldBuffTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public class FieldBuffTable : Singleton<FieldBuffTable>, IDataTable
{
  private UIntKeyTable<FieldBuffTable.FieldBuffData> dataTable;

  public void CreateTable(string text)
  {
    this.dataTable = TableUtility.CreateUIntKeyTable<FieldBuffTable.FieldBuffData>(text, new TableUtility.CallBackUIntKeyReadCSV<FieldBuffTable.FieldBuffData>(FieldBuffTable.FieldBuffData.cb), "id,name,buffTableIds");
    this.dataTable.TrimExcess();
  }

  public FieldBuffTable.FieldBuffData GetData(uint id)
  {
    return this.dataTable == null ? (FieldBuffTable.FieldBuffData) null : this.dataTable.Get(id);
  }

  public class FieldBuffData
  {
    public uint id;
    public string name;
    public List<uint> buffTableIds = new List<uint>();
    public const string NT = "id,name,buffTableIds";

    public static bool cb(CSVReader csv_reader, FieldBuffTable.FieldBuffData data, ref uint key)
    {
      data.id = key;
      csv_reader.Pop(ref data.name);
      string str = "";
      csv_reader.Pop(ref str);
      string[] strArray = str.Split(':');
      data.buffTableIds.Clear();
      int index = 0;
      for (int length = strArray.Length; index < length; ++index)
      {
        uint result = 0;
        if (uint.TryParse(strArray[index], out result))
          data.buffTableIds.Add(result);
      }
      return true;
    }
  }
}
