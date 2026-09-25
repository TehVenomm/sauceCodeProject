// Decompiled with JetBrains decompiler
// Type: GachaSearchEnemyTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
public class GachaSearchEnemyTable : Singleton<GachaSearchEnemyTable>, IDataTable
{
  private UIntKeyTable<GachaSearchEnemyTable.GachaSearchEnemyData> gachaSearchEnemyDataTable;

  public void CreateTable(string csv_text)
  {
    this.gachaSearchEnemyDataTable = TableUtility.CreateUIntKeyTable<GachaSearchEnemyTable.GachaSearchEnemyData>(csv_text, new TableUtility.CallBackUIntKeyReadCSV<GachaSearchEnemyTable.GachaSearchEnemyData>(GachaSearchEnemyTable.GachaSearchEnemyData.cb), "id,name,bRare,aRare,sRare,ssRare,sssRare,fire,water,thunder,soil,light,dark,none,sortPriority,startAt");
    this.gachaSearchEnemyDataTable.TrimExcess();
  }

  public GachaSearchEnemyTable.GachaSearchEnemyData[] GetSortedGachaSearchEnemyData()
  {
    DateTime now = TimeManager.GetNow();
    List<GachaSearchEnemyTable.GachaSearchEnemyData> sortData = new List<GachaSearchEnemyTable.GachaSearchEnemyData>();
    this.gachaSearchEnemyDataTable.ForEach((Action<GachaSearchEnemyTable.GachaSearchEnemyData>) (o =>
    {
      if (!(o.startAt <= now))
        return;
      sortData.Add(o);
    }));
    sortData.Sort((Comparison<GachaSearchEnemyTable.GachaSearchEnemyData>) ((a, b) => b.sortPriority - a.sortPriority));
    return sortData.ToArray();
  }

  public List<string> GetGachaSearchEnemyNames(GachaSearchEnemyTable.GachaSearchEnemyData[] data)
  {
    List<string> searchEnemyNames = new List<string>();
    for (int index = 0; index < data.Length; ++index)
      searchEnemyNames.Add(data[index].name);
    return searchEnemyNames;
  }

  public int GetEnemySpeciesId(string name)
  {
    int id = 0;
    this.gachaSearchEnemyDataTable.ForEach((Action<GachaSearchEnemyTable.GachaSearchEnemyData>) (o =>
    {
      if (id != 0 || !(o.name == name))
        return;
      id = o.id;
    }));
    return id;
  }

  public GachaSearchEnemyTable.GachaSearchEnemyData[] GetEnemyDataOnRairtyFlag(
    GachaSearchEnemyTable.GachaSearchEnemyData[] data,
    int rarityBit)
  {
    List<GachaSearchEnemyTable.GachaSearchEnemyData> source = new List<GachaSearchEnemyTable.GachaSearchEnemyData>();
    if ((rarityBit & 4) != 0)
    {
      for (int index = 0; index < data.Length; ++index)
      {
        if (data[index].bRare == 1)
          source.Add(data[index]);
      }
    }
    if ((rarityBit & 8) != 0)
    {
      for (int index = 0; index < data.Length; ++index)
      {
        if (data[index].aRare == 1)
          source.Add(data[index]);
      }
    }
    if ((rarityBit & 16 /*0x10*/) != 0)
    {
      for (int index = 0; index < data.Length; ++index)
      {
        if (data[index].sRare == 1)
          source.Add(data[index]);
      }
    }
    if ((rarityBit & 32 /*0x20*/) != 0)
    {
      for (int index = 0; index < data.Length; ++index)
      {
        if (data[index].ssRare == 1)
          source.Add(data[index]);
      }
    }
    source.Sort((Comparison<GachaSearchEnemyTable.GachaSearchEnemyData>) ((a, b) => b.sortPriority - a.sortPriority));
    return source.Distinct<GachaSearchEnemyTable.GachaSearchEnemyData>().ToArray<GachaSearchEnemyTable.GachaSearchEnemyData>();
  }

  public GachaSearchEnemyTable.GachaSearchEnemyData[] GetEnemyDataOnElementFlag(
    GachaSearchEnemyTable.GachaSearchEnemyData[] data,
    int elementBit)
  {
    List<GachaSearchEnemyTable.GachaSearchEnemyData> source = new List<GachaSearchEnemyTable.GachaSearchEnemyData>();
    if ((elementBit & 1) != 0)
    {
      for (int index = 0; index < data.Length; ++index)
      {
        if (data[index].fire == 1)
          source.Add(data[index]);
      }
    }
    if ((elementBit & 2) != 0)
    {
      for (int index = 0; index < data.Length; ++index)
      {
        if (data[index].water == 1)
          source.Add(data[index]);
      }
    }
    if ((elementBit & 4) != 0)
    {
      for (int index = 0; index < data.Length; ++index)
      {
        if (data[index].thunder == 1)
          source.Add(data[index]);
      }
    }
    if ((elementBit & 8) != 0)
    {
      for (int index = 0; index < data.Length; ++index)
      {
        if (data[index].soil == 1)
          source.Add(data[index]);
      }
    }
    if ((elementBit & 16 /*0x10*/) != 0)
    {
      for (int index = 0; index < data.Length; ++index)
      {
        if (data[index].light == 1)
          source.Add(data[index]);
      }
    }
    if ((elementBit & 32 /*0x20*/) != 0)
    {
      for (int index = 0; index < data.Length; ++index)
      {
        if (data[index].dark == 1)
          source.Add(data[index]);
      }
    }
    source.Sort((Comparison<GachaSearchEnemyTable.GachaSearchEnemyData>) ((a, b) => b.sortPriority - a.sortPriority));
    return source.Distinct<GachaSearchEnemyTable.GachaSearchEnemyData>().ToArray<GachaSearchEnemyTable.GachaSearchEnemyData>();
  }

  public class GachaSearchEnemyData
  {
    public int id;
    public string name;
    public int bRare;
    public int aRare;
    public int sRare;
    public int ssRare;
    public int sssRare;
    public int fire;
    public int water;
    public int thunder;
    public int soil;
    public int light;
    public int dark;
    public int none;
    public int sortPriority;
    public DateTime startAt;
    public const string NT = "id,name,bRare,aRare,sRare,ssRare,sssRare,fire,water,thunder,soil,light,dark,none,sortPriority,startAt";

    public static bool cb(
      CSVReader csv_reader,
      GachaSearchEnemyTable.GachaSearchEnemyData data,
      ref uint key)
    {
      data.id = (int) key;
      csv_reader.Pop(ref data.name);
      csv_reader.Pop(ref data.bRare);
      csv_reader.Pop(ref data.aRare);
      csv_reader.Pop(ref data.sRare);
      csv_reader.Pop(ref data.ssRare);
      csv_reader.Pop(ref data.sssRare);
      csv_reader.Pop(ref data.fire);
      csv_reader.Pop(ref data.water);
      csv_reader.Pop(ref data.thunder);
      csv_reader.Pop(ref data.soil);
      csv_reader.Pop(ref data.light);
      csv_reader.Pop(ref data.dark);
      csv_reader.Pop(ref data.none);
      csv_reader.Pop(ref data.sortPriority);
      string empty = string.Empty;
      csv_reader.Pop(ref empty);
      if (!string.IsNullOrEmpty(empty))
        DateTime.TryParse(empty, out data.startAt);
      return true;
    }
  }
}
