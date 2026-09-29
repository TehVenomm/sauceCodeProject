// Decompiled with JetBrains decompiler
// Type: RegionTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class RegionTable : Singleton<RegionTable>, IDataTable
{
  private UIntKeyTable<RegionTable.Data> table;

  public void CreateTable(string csv_text)
  {
    this.table = TableUtility.CreateUIntKeyTable<RegionTable.Data>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<RegionTable.Data>(RegionTable.Data.cb), "regionId,name,iconId,x,y,z,w,h,mx,my,parentRegionId,eventId,difficulty,worldId,nextRegionId,startAt,groupId");
  }

  public RegionTable.Data GetData(uint id)
  {
    return !Singleton<RegionTable>.IsValid() ? (RegionTable.Data) null : this.table.Get(id);
  }

  public RegionTable.Data[] GetData()
  {
    if (!Singleton<RegionTable>.IsValid())
      return (RegionTable.Data[]) null;
    RegionTable.Data[] ret = new RegionTable.Data[this.table.GetCount()];
    int index = 0;
    this.table.ForEach((Action<RegionTable.Data>) (data => ret[index++] = data));
    return ret;
  }

  public RegionTable.Data GetData(int groupId, REGION_DIFFICULTY_TYPE type)
  {
    if (!Singleton<RegionTable>.IsValid())
      return (RegionTable.Data) null;
    RegionTable.Data[] data1 = this.GetData();
    List<RegionTable.Data> dataList = new List<RegionTable.Data>();
    foreach (RegionTable.Data data2 in data1)
    {
      if (data2.groupId == groupId && data2.difficulty == type)
        return data2;
    }
    return (RegionTable.Data) null;
  }

  public RegionTable.Data[] GetGroupData(int groupId)
  {
    if (!Singleton<RegionTable>.IsValid())
      return (RegionTable.Data[]) null;
    RegionTable.Data[] data1 = this.GetData();
    List<RegionTable.Data> dataList = new List<RegionTable.Data>();
    foreach (RegionTable.Data data2 in data1)
    {
      if (data2.groupId == groupId)
        dataList.Add(data2);
    }
    return dataList.ToArray();
  }

  public int GetMapNo(int regionId)
  {
    if (!Singleton<RegionTable>.IsValid())
      return 0;
    RegionTable.Data data1 = this.GetData((uint) regionId);
    if (data1 == null || data1.regionId >= 100U)
      return 0;
    int num1 = regionId;
    if (data1.difficulty != REGION_DIFFICULTY_TYPE.NORMAL)
    {
      RegionTable.Data data2 = this.GetData((int) data1.regionId, REGION_DIFFICULTY_TYPE.NORMAL);
      if (data2 == null)
        return 0;
      num1 = (int) data2.regionId;
    }
    List<int> intList = new List<int>();
    foreach (RegionTable.Data data3 in this.GetData())
    {
      if (data3.regionId < 100U && data3.difficulty == REGION_DIFFICULTY_TYPE.NORMAL)
        intList.Add((int) data3.regionId);
    }
    if (!intList.Contains(num1))
      return 0;
    int num2 = intList.IndexOf(regionId);
    if (data1.worldId == 2)
      num2 -= 9;
    return num2 + 1;
  }

  public class Data
  {
    public uint regionId;
    public string regionName;
    public int iconID;
    public Vector3 iconPos = Vector3.zero;
    public Vector2 iconSize = Vector2.zero;
    public Vector3 markerPos = Vector2.op_Implicit(Vector2.zero);
    public uint parentRegionId = uint.MaxValue;
    public int eventId;
    public int worldId;
    public REGION_DIFFICULTY_TYPE difficulty;
    public int nextRegionId;
    public DateTime startAt;
    public int groupId;
    public const uint NON_PARENT_ID = 4294967295 /*0xFFFFFFFF*/;
    public const string NT = "regionId,name,iconId,x,y,z,w,h,mx,my,parentRegionId,eventId,difficulty,worldId,nextRegionId,startAt,groupId";
    public static int EVENT_START_TIME_LEN = 3;
    private const int TIME_DATA_LEN = 2;

    public bool hasParentRegion() => this.parentRegionId != uint.MaxValue;

    public static bool cb(CSVReader csvReader, RegionTable.Data data, ref uint key)
    {
      data.regionId = key;
      csvReader.Pop(ref data.regionName);
      csvReader.Pop(ref data.iconID);
      csvReader.Pop(ref data.iconPos);
      csvReader.Pop(ref data.iconSize);
      Vector2 vector2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2).\u002Ector(0.0f, 0.0f);
      csvReader.Pop(ref vector2);
      data.markerPos = new Vector3(vector2.x, vector2.y, 0.0f);
      csvReader.Pop(ref data.parentRegionId);
      csvReader.Pop(ref data.eventId);
      csvReader.PopEnum<REGION_DIFFICULTY_TYPE>(ref data.difficulty, REGION_DIFFICULTY_TYPE.NORMAL);
      csvReader.Pop(ref data.worldId);
      csvReader.Pop(ref data.nextRegionId);
      string empty = string.Empty;
      csvReader.Pop(ref empty);
      if (!string.IsNullOrEmpty(empty))
        DateTime.TryParse(empty, out data.startAt);
      csvReader.Pop(ref data.groupId);
      return true;
    }

    public bool HasGroup() => this.groupId > 0;

    public bool HasStartAt() => this.startAt.CompareTo(new DateTime()) != 0;
  }
}
