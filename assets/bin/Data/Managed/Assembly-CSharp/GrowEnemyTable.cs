// Decompiled with JetBrains decompiler
// Type: GrowEnemyTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class GrowEnemyTable : Singleton<GrowEnemyTable>, IDataTable
{
  private DoubleUIntKeyTable<GrowEnemyTable.GrowEnemyData> growTableData;
  private GrowEnemyTable.GrowEnemyData _defaultData = new GrowEnemyTable.GrowEnemyData();

  public void CreateTable(string csv_text)
  {
    this.growTableData = TableUtility.CreateDoubleUIntKeyTable<GrowEnemyTable.GrowEnemyData>(csv_text, new TableUtility.CallBackDoubleUIntKeyReadCSV<GrowEnemyTable.GrowEnemyData>(GrowEnemyTable.GrowEnemyData.cb), "growId,lv,hp,atk", (TableUtility.CallBackDoubleUIntSecondKey) null);
    this.growTableData.TrimExcess();
  }

  public void AddTable(string csv_text)
  {
    TableUtility.AddDoubleUIntKeyTable<GrowEnemyTable.GrowEnemyData>(this.growTableData, csv_text, new TableUtility.CallBackDoubleUIntKeyReadCSV<GrowEnemyTable.GrowEnemyData>(GrowEnemyTable.GrowEnemyData.cb), "growId,lv,hp,atk", (TableUtility.CallBackDoubleUIntSecondKey) null);
  }

  public GrowEnemyTable.GrowEnemyData GetGrowEnemyData(uint growId, int level)
  {
    if (this.growTableData == null)
    {
      Log.Warning("GetGrowEnemyData: growTableDate is null!");
      return this._defaultData;
    }
    UIntKeyTable<GrowEnemyTable.GrowEnemyData> uintKeyTable = this.growTableData.Get(growId);
    if (uintKeyTable == null)
    {
      Log.Warning("GetGrowEnemyData: growId {0} is not found!", (object) growId);
      return this._defaultData;
    }
    GrowEnemyTable.GrowEnemyData growEnemyData = uintKeyTable.Get((uint) level);
    if (growEnemyData != null)
      return growEnemyData;
    GrowEnemyTable.GrowEnemyData prev = (GrowEnemyTable.GrowEnemyData) null;
    GrowEnemyTable.GrowEnemyData next = (GrowEnemyTable.GrowEnemyData) null;
    uintKeyTable.ForEach((Action<GrowEnemyTable.GrowEnemyData>) (grow =>
    {
      if ((int) grow.level < level && (prev == null || (int) grow.level > (int) prev.level))
        prev = grow;
      if ((int) grow.level <= level || next != null && (int) grow.level >= (int) next.level)
        return;
      next = grow;
    }));
    if (next == null || prev == null)
    {
      if (next != null)
        return next;
      if (prev != null)
        return prev;
      Log.Warning("GetGrowEnemyData: growId {0}, Lv {1} Lerp error", (object) growId, (object) level);
      return this._defaultData;
    }
    float num = (float) (level - (int) prev.level) / (float) ((int) next.level - (int) prev.level);
    return new GrowEnemyTable.GrowEnemyData()
    {
      growId = growId,
      level = (XorInt) level,
      hp = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) prev.hp, (float) (int) next.hp, num)),
      atk = (XorInt) Mathf.FloorToInt(Mathf.Lerp((float) (int) prev.atk, (float) (int) next.atk, num))
    };
  }

  public class GrowEnemyData
  {
    public uint growId;
    public XorInt level = (XorInt) 1;
    public XorInt hp = (XorInt) 100;
    public XorInt atk = (XorInt) 100;
    public const string NT = "growId,lv,hp,atk";

    public float hpRate => (float) (int) this.hp * 0.01f;

    public float atkRate => (float) (int) this.atk * 0.01f;

    public static bool cb(
      CSVReader csv_reader,
      GrowEnemyTable.GrowEnemyData data,
      ref uint key1,
      ref uint key2)
    {
      data.growId = key1;
      data.level = (XorInt) (int) key2;
      float num1 = 0.0f;
      csv_reader.Pop(ref num1);
      data.hp = (XorInt) Mathf.RoundToInt(num1);
      float num2 = 0.0f;
      csv_reader.Pop(ref num2);
      data.atk = (XorInt) Mathf.RoundToInt(num2);
      return true;
    }
  }
}
