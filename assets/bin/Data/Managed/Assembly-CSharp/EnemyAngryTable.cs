// Decompiled with JetBrains decompiler
// Type: EnemyAngryTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class EnemyAngryTable : Singleton<EnemyAngryTable>, IDataTable
{
  private UIntKeyTable<EnemyAngryTable.Data> dataTable;

  public void CreateTable(TextAsset textasset) => this.CreateTable(textasset.text);

  public void CreateTable(string text)
  {
    this.dataTable = TableUtility.CreateUIntKeyTable<EnemyAngryTable.Data>(text, new TableUtility.CallBackUIntKeyReadCSV<EnemyAngryTable.Data>(EnemyAngryTable.Data.cb), "id,condition,value1,value2,value3,value4");
    this.dataTable.TrimExcess();
  }

  public void AddTable(TextAsset textasset)
  {
    TableUtility.AddUIntKeyTable<EnemyAngryTable.Data>(this.dataTable, textasset.text, new TableUtility.CallBackUIntKeyReadCSV<EnemyAngryTable.Data>(EnemyAngryTable.Data.cb), "id,condition,value1,value2,value3,value4");
  }

  public EnemyAngryTable.Data GetData(uint id)
  {
    return this.dataTable == null ? (EnemyAngryTable.Data) null : this.dataTable.Get(id);
  }

  public class Data
  {
    public uint id;
    public ANGRY_CONDITION condition;
    public int value1;
    public int value2;
    public int value3;
    public int value4;
    public uint actionID;
    public const string NT = "id,condition,value1,value2,value3,value4";

    public static bool cb(CSVReader csv_reader, EnemyAngryTable.Data data, ref uint key)
    {
      data.id = key;
      int num = 0;
      csv_reader.Pop(ref num);
      data.condition = (ANGRY_CONDITION) num;
      csv_reader.Pop(ref data.value1);
      csv_reader.Pop(ref data.value2);
      csv_reader.Pop(ref data.value3);
      csv_reader.Pop(ref data.value4);
      return true;
    }
  }
}
