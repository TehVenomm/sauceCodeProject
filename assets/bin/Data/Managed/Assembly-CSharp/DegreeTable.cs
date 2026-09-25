// Decompiled with JetBrains decompiler
// Type: DegreeTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class DegreeTable : Singleton<DegreeTable>, IDataTable
{
  public const int INFO_MAX = 3;
  private UIntKeyTable<DegreeTable.DegreeData> dataTable;

  public void CreateTable(string csv_text)
  {
    this.dataTable = TableUtility.CreateUIntKeyTable<DegreeTable.DegreeData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<DegreeTable.DegreeData>(DegreeTable.DegreeData.cb), "id,name,type,requirementType,requirementText,lockNameId,lockTextId");
    this.dataTable.TrimExcess();
  }

  public void AddTable(string csv_text)
  {
    TableUtility.AddUIntKeyTable<DegreeTable.DegreeData>(this.dataTable, csv_text, new TableUtility.CallBackUIntKeyReadCSV<DegreeTable.DegreeData>(DegreeTable.DegreeData.cb), "id,name,type,requirementType,requirementText,lockNameId,lockTextId");
  }

  public DegreeTable.DegreeData GetData(uint id)
  {
    if (this.dataTable == null)
      return (DegreeTable.DegreeData) null;
    DegreeTable.DegreeData data = this.dataTable.Get(id);
    if (data == null)
    {
      Log.TableError((object) this, id);
      data = new DegreeTable.DegreeData();
      data.name = Log.NON_DATA_NAME;
    }
    return data;
  }

  public List<DegreeTable.DegreeData> GetAll()
  {
    List<DegreeTable.DegreeData> allData = new List<DegreeTable.DegreeData>();
    this.dataTable.ForEach((Action<DegreeTable.DegreeData>) (x =>
    {
      if (x == null)
        return;
      allData.Add(x);
    }));
    return allData;
  }

  public class DegreeData
  {
    public uint id;
    public string name;
    public DEGREE_TYPE type;
    public DEGREE_REQUIREMENT_TYPE requirementType;
    public string requirementText;
    public uint lockNameId;
    public uint lockTextId;
    public const string NT = "id,name,type,requirementType,requirementText,lockNameId,lockTextId";

    public static bool cb(CSVReader csv_reader, DegreeTable.DegreeData data, ref uint key)
    {
      data.id = key;
      csv_reader.Pop(ref data.name);
      csv_reader.PopEnum<DEGREE_TYPE>(ref data.type, DEGREE_TYPE.NONE);
      csv_reader.PopEnum<DEGREE_REQUIREMENT_TYPE>(ref data.requirementType, DEGREE_REQUIREMENT_TYPE.COMMON);
      csv_reader.Pop(ref data.requirementText);
      csv_reader.Pop(ref data.lockNameId);
      csv_reader.Pop(ref data.lockTextId);
      return true;
    }

    public bool IsUnlcok(List<int> userUnlockList)
    {
      if (this.requirementType == DEGREE_REQUIREMENT_TYPE.COMMON)
        return true;
      return userUnlockList != null && userUnlockList.Contains((int) this.id);
    }

    public bool IsSecretText(List<int> userUnlockList)
    {
      if (this.lockTextId == 0U || this.IsUnlcok(userUnlockList))
        return false;
      return userUnlockList == null || !userUnlockList.Contains((int) this.lockTextId);
    }

    public bool IsSecretName(List<int> userUnlockList)
    {
      if (this.lockNameId == 0U || this.IsUnlcok(userUnlockList))
        return false;
      return userUnlockList == null || !userUnlockList.Contains((int) this.lockNameId);
    }
  }
}
