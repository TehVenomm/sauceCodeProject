// Decompiled with JetBrains decompiler
// Type: SymbolTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class SymbolTable : Singleton<SymbolTable>, IDataTable
{
  private UIntKeyTable<SymbolTable.SymbolData> symbolTable;

  public int[] markIDs { get; private set; }

  public int[] frameIDs { get; private set; }

  public int[] patternIDs { get; private set; }

  public int[] defaultHasIDs { get; private set; }

  public Color[] markColors { get; private set; }

  public Color[] frameColors { get; private set; }

  public Color[] patternColors { get; private set; }

  public int[] markSortIDs { get; private set; }

  public int[] frameSortIDs { get; private set; }

  public int[] patternSortIDs { get; private set; }

  public void CreateTable(string csv_table)
  {
    this.symbolTable = TableUtility.CreateUIntKeyTable<SymbolTable.SymbolData>(csv_table, new TableUtility.CallBackUIntKeyReadCSV<SymbolTable.SymbolData>(SymbolTable.SymbolData.cb), "dataIndex,rawId,type,name,defaultHasFlg,R,G,B,R2,G2,B2,R3,G3,B3,displayOrder");
    this.symbolTable.TrimExcess();
    this.ConvertTable();
  }

  public void AddTable(string csv_table)
  {
    TableUtility.AddUIntKeyTable<SymbolTable.SymbolData>(this.symbolTable, csv_table, new TableUtility.CallBackUIntKeyReadCSV<SymbolTable.SymbolData>(SymbolTable.SymbolData.cb), "dataIndex,rawId,type,name,defaultHasFlg,R,G,B,R2,G2,B2,R3,G3,B3,displayOrder");
  }

  public void ConvertTable()
  {
    List<int> intList1 = new List<int>();
    List<SymbolTable.DisplayOrderData> data1 = new List<SymbolTable.DisplayOrderData>();
    List<int> intList2 = new List<int>();
    List<SymbolTable.DisplayOrderData> data2 = new List<SymbolTable.DisplayOrderData>();
    List<int> intList3 = new List<int>();
    List<SymbolTable.DisplayOrderData> data3 = new List<SymbolTable.DisplayOrderData>();
    List<int> intList4 = new List<int>();
    List<Color> colorList1 = new List<Color>();
    List<Color> colorList2 = new List<Color>();
    List<Color> colorList3 = new List<Color>();
    for (int index = 0; index < this.GetCount(); ++index)
    {
      SymbolTable.SymbolData data4 = this.GetData(index);
      if (data4.defaultHasFlg > 0)
      {
        SymbolTable.DisplayOrderData displayOrderData = new SymbolTable.DisplayOrderData();
        displayOrderData.num = data4.rawId;
        displayOrderData.order = (int) data4.id;
        displayOrderData.sort = data4.displayOrder;
        switch (data4.type)
        {
          case 1:
            intList1.Add(data4.rawId);
            data1.Add(displayOrderData);
            break;
          case 2:
            intList2.Add(data4.rawId);
            data2.Add(displayOrderData);
            break;
          case 3:
            intList3.Add(data4.rawId);
            data3.Add(displayOrderData);
            break;
        }
      }
      if (data4.hasMarkColor)
        colorList1.Add(Color32.op_Implicit(data4.markColor));
      if (data4.hasFrameColor)
        colorList2.Add(Color32.op_Implicit(data4.frameColor));
      if (data4.hasPatternColor)
        colorList3.Add(Color32.op_Implicit(data4.patternColor));
    }
    this.markIDs = intList1.ToArray();
    this.frameIDs = intList2.ToArray();
    this.patternIDs = intList3.ToArray();
    this.markSortIDs = this.SymbolSort(data1);
    this.frameSortIDs = this.SymbolSort(data2);
    this.patternSortIDs = this.SymbolSort(data3);
    this.defaultHasIDs = intList4.ToArray();
    this.markColors = colorList1.ToArray();
    this.frameColors = colorList2.ToArray();
    this.patternColors = colorList3.ToArray();
  }

  public SymbolTable.SymbolData GetData(int index)
  {
    return this.symbolTable == null || this.symbolTable.GetCount() <= index ? (SymbolTable.SymbolData) null : this.symbolTable.Get((uint) index);
  }

  public SymbolTable.SymbolData GetData(uint index)
  {
    return this.symbolTable == null || (long) this.symbolTable.GetCount() <= (long) index ? (SymbolTable.SymbolData) null : this.symbolTable.Get(index);
  }

  public int GetCount() => this.symbolTable == null ? -1 : this.symbolTable.GetCount();

  public Color GetColor(SymbolTable.SymbolType type, int index)
  {
    Color[] colors = this.GetColors(type);
    return colors != null && colors.Length > index ? colors[index] : Color.white;
  }

  public int GetSymbolID(SymbolTable.SymbolType type, int index)
  {
    int[] symolIds = this.GetSymolIDs(type);
    return symolIds != null && symolIds.Length > index ? symolIds[index] : 0;
  }

  public int GetSymbolIndex(SymbolTable.SymbolType type, int id)
  {
    int[] symolIds = this.GetSymolIDs(type);
    for (int symbolIndex = 0; symbolIndex < symolIds.Length; ++symbolIndex)
    {
      if (symolIds[symbolIndex] == id)
        return symbolIndex;
    }
    return 0;
  }

  public int[] GetSymolIDs(SymbolTable.SymbolType type)
  {
    switch (type)
    {
      case SymbolTable.SymbolType.MARK:
        return this.markIDs;
      case SymbolTable.SymbolType.FRAME:
      case SymbolTable.SymbolType.FRAME_OUTLINE:
        return this.frameIDs;
      case SymbolTable.SymbolType.PATTERN:
        return this.patternIDs;
      default:
        return (int[]) null;
    }
  }

  public int[] GetSortSymbolIDs(SymbolTable.SymbolType type)
  {
    switch (type)
    {
      case SymbolTable.SymbolType.MARK:
        return this.markSortIDs;
      case SymbolTable.SymbolType.FRAME:
      case SymbolTable.SymbolType.FRAME_OUTLINE:
        return this.frameSortIDs;
      case SymbolTable.SymbolType.PATTERN:
        return this.patternSortIDs;
      default:
        return (int[]) null;
    }
  }

  public int[] SymbolSort(List<SymbolTable.DisplayOrderData> data)
  {
    return ((IEnumerable<SymbolTable.DisplayOrderData>) data.GroupBy<SymbolTable.DisplayOrderData, int>((Func<SymbolTable.DisplayOrderData, int>) (s => s.sort)).OrderBy<IGrouping<int, SymbolTable.DisplayOrderData>, int>((Func<IGrouping<int, SymbolTable.DisplayOrderData>, int>) (g => g.Key)).SelectMany<IGrouping<int, SymbolTable.DisplayOrderData>, SymbolTable.DisplayOrderData>((Func<IGrouping<int, SymbolTable.DisplayOrderData>, IEnumerable<SymbolTable.DisplayOrderData>>) (g => (IEnumerable<SymbolTable.DisplayOrderData>) g.OrderBy<SymbolTable.DisplayOrderData, int>((Func<SymbolTable.DisplayOrderData, int>) (s => s.order)))).ToArray<SymbolTable.DisplayOrderData>()).Select<SymbolTable.DisplayOrderData, int>((Func<SymbolTable.DisplayOrderData, int>) (s => s.num)).ToArray<int>();
  }

  public Color[] GetColors(SymbolTable.SymbolType type)
  {
    switch (type)
    {
      case SymbolTable.SymbolType.MARK:
        return this.markColors;
      case SymbolTable.SymbolType.FRAME:
      case SymbolTable.SymbolType.FRAME_OUTLINE:
        return this.frameColors;
      case SymbolTable.SymbolType.PATTERN:
        return this.patternColors;
      default:
        return (Color[]) null;
    }
  }

  public enum SymbolType
  {
    MARK = 1,
    FRAME = 2,
    PATTERN = 3,
    FRAME_OUTLINE = 4,
  }

  public class SymbolData
  {
    public uint id;
    public int rawId;
    public int type;
    public string name;
    public int defaultHasFlg;
    public bool hasMarkColor;
    public Color32 markColor;
    public bool hasFrameColor;
    public Color32 frameColor;
    public bool hasPatternColor;
    public Color32 patternColor;
    public int displayOrder;
    public const string NT = "dataIndex,rawId,type,name,defaultHasFlg,R,G,B,R2,G2,B2,R3,G3,B3,displayOrder";

    public static bool cb(CSVReader csv_reader, SymbolTable.SymbolData data, ref uint key)
    {
      csv_reader.Pop(ref data.rawId);
      csv_reader.Pop(ref data.type);
      csv_reader.Pop(ref data.name);
      csv_reader.Pop(ref data.defaultHasFlg);
      data.hasMarkColor = (bool) csv_reader.PopColor24(ref data.markColor);
      data.hasFrameColor = (bool) csv_reader.PopColor24(ref data.frameColor);
      data.hasPatternColor = (bool) csv_reader.PopColor24(ref data.patternColor);
      csv_reader.Pop(ref data.displayOrder);
      return true;
    }
  }

  public class DisplayOrderData
  {
    public int num;
    public int sort;
    public int order;
  }
}
