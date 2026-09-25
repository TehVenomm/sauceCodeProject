// Decompiled with JetBrains decompiler
// Type: GatherItemTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class GatherItemTable : Singleton<GatherItemTable>, IDataTable
{
  private UIntKeyTable<GatherItemTable.GatherItemData> dataTable;

  public void CreateTable(string text)
  {
    this.dataTable = TableUtility.CreateUIntKeyTable<GatherItemTable.GatherItemData>(text, new TableUtility.CallBackUIntKeyReadCSV<GatherItemTable.GatherItemData>(GatherItemTable.GatherItemData.cb), "gatherItemId,listId,name,average,stdDev,enemyPopId,isRare");
    this.dataTable.TrimExcess();
  }

  public GatherItemTable.GatherItemData GetData(uint id)
  {
    return this.dataTable == null ? (GatherItemTable.GatherItemData) null : this.dataTable.Get(id);
  }

  public List<GatherItemTable.GatherItemData> GetAllData()
  {
    if (this.dataTable == null)
      return (List<GatherItemTable.GatherItemData>) null;
    List<GatherItemTable.GatherItemData> list = new List<GatherItemTable.GatherItemData>();
    this.dataTable.ForEach((Action<GatherItemTable.GatherItemData>) (data => list.Add(data)));
    return list;
  }

  public class GatherItemData
  {
    public uint gatherItemId;
    public int listId;
    public string name;
    public int average;
    public int stdDev;
    public int enemyPopId;
    public int isRare;
    public const string NT = "gatherItemId,listId,name,average,stdDev,enemyPopId,isRare";

    public static bool cb(CSVReader csv_reader, GatherItemTable.GatherItemData data, ref uint key)
    {
      data.gatherItemId = key;
      csv_reader.Pop(ref data.listId);
      csv_reader.Pop(ref data.name);
      csv_reader.Pop(ref data.average);
      csv_reader.Pop(ref data.stdDev);
      csv_reader.Pop(ref data.enemyPopId);
      csv_reader.Pop(ref data.isRare);
      return true;
    }
  }
}
