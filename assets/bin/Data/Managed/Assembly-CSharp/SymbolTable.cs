using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SymbolTable : Singleton<SymbolTable>, IDataTable
{
	public enum SymbolType
	{
		MARK = 1,
		FRAME,
		PATTERN,
		FRAME_OUTLINE
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

		public static bool cb(CSVReader csv_reader, SymbolData data, ref uint key)
		{
			csv_reader.Pop(ref data.rawId);
			csv_reader.Pop(ref data.type);
			csv_reader.Pop(ref data.name);
			csv_reader.Pop(ref data.defaultHasFlg);
			data.hasMarkColor = csv_reader.PopColor24(ref data.markColor);
			data.hasFrameColor = csv_reader.PopColor24(ref data.frameColor);
			data.hasPatternColor = csv_reader.PopColor24(ref data.patternColor);
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

	private UIntKeyTable<SymbolData> symbolTable;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<SymbolData> _003C_003Ef__mg_0024cache0;

	[CompilerGenerated]
	private static TableUtility.CallBackUIntKeyReadCSV<SymbolData> _003C_003Ef__mg_0024cache1;

	public int[] markIDs
	{
		get;
		private set;
	}

	public int[] frameIDs
	{
		get;
		private set;
	}

	public int[] patternIDs
	{
		get;
		private set;
	}

	public int[] defaultHasIDs
	{
		get;
		private set;
	}

	public Color[] markColors
	{
		get;
		private set;
	}

	public Color[] frameColors
	{
		get;
		private set;
	}

	public Color[] patternColors
	{
		get;
		private set;
	}

	public int[] markSortIDs
	{
		get;
		private set;
	}

	public int[] frameSortIDs
	{
		get;
		private set;
	}

	public int[] patternSortIDs
	{
		get;
		private set;
	}

	public void CreateTable(string csv_table)
	{
		symbolTable = TableUtility.CreateUIntKeyTable<SymbolData>(csv_table, SymbolData.cb, "dataIndex,rawId,type,name,defaultHasFlg,R,G,B,R2,G2,B2,R3,G3,B3,displayOrder");
		symbolTable.TrimExcess();
		ConvertTable();
	}

	public void AddTable(string csv_table)
	{
		TableUtility.AddUIntKeyTable(symbolTable, csv_table, SymbolData.cb, "dataIndex,rawId,type,name,defaultHasFlg,R,G,B,R2,G2,B2,R3,G3,B3,displayOrder");
	}

	public void ConvertTable()
	{
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		List<int> list = new List<int>();
		List<DisplayOrderData> list2 = new List<DisplayOrderData>();
		List<int> list3 = new List<int>();
		List<DisplayOrderData> list4 = new List<DisplayOrderData>();
		List<int> list5 = new List<int>();
		List<DisplayOrderData> list6 = new List<DisplayOrderData>();
		List<int> list7 = new List<int>();
		List<Color> list8 = new List<Color>();
		List<Color> list9 = new List<Color>();
		List<Color> list10 = new List<Color>();
		for (int i = 0; i < GetCount(); i++)
		{
			SymbolData data = GetData(i);
			if (data.defaultHasFlg > 0)
			{
				DisplayOrderData displayOrderData = new DisplayOrderData();
				displayOrderData.num = data.rawId;
				displayOrderData.order = (int)data.id;
				displayOrderData.sort = data.displayOrder;
				switch (data.type)
				{
				case 1:
					list.Add(data.rawId);
					list2.Add(displayOrderData);
					break;
				case 2:
					list3.Add(data.rawId);
					list4.Add(displayOrderData);
					break;
				case 3:
					list5.Add(data.rawId);
					list6.Add(displayOrderData);
					break;
				}
			}
			if (data.hasMarkColor)
			{
				list8.Add(Color32.op_Implicit(data.markColor));
			}
			if (data.hasFrameColor)
			{
				list9.Add(Color32.op_Implicit(data.frameColor));
			}
			if (data.hasPatternColor)
			{
				list10.Add(Color32.op_Implicit(data.patternColor));
			}
		}
		markIDs = list.ToArray();
		frameIDs = list3.ToArray();
		patternIDs = list5.ToArray();
		markSortIDs = SymbolSort(list2);
		frameSortIDs = SymbolSort(list4);
		patternSortIDs = SymbolSort(list6);
		defaultHasIDs = list7.ToArray();
		markColors = list8.ToArray();
		frameColors = list9.ToArray();
		patternColors = list10.ToArray();
	}

	public SymbolData GetData(int index)
	{
		if (symbolTable == null || symbolTable.GetCount() <= index)
		{
			return null;
		}
		return symbolTable.Get((uint)index);
	}

	public SymbolData GetData(uint index)
	{
		if (symbolTable == null || symbolTable.GetCount() <= index)
		{
			return null;
		}
		return symbolTable.Get(index);
	}

	public int GetCount()
	{
		if (symbolTable == null)
		{
			return -1;
		}
		return symbolTable.GetCount();
	}

	public Color GetColor(SymbolType type, int index)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Color[] colors = GetColors(type);
		if (colors != null && colors.Length > index)
		{
			return colors[index];
		}
		return Color.get_white();
	}

	public int GetSymbolID(SymbolType type, int index)
	{
		int[] symolIDs = GetSymolIDs(type);
		if (symolIDs != null && symolIDs.Length > index)
		{
			return symolIDs[index];
		}
		return 0;
	}

	public int GetSymbolIndex(SymbolType type, int id)
	{
		int[] symolIDs = GetSymolIDs(type);
		for (int i = 0; i < symolIDs.Length; i++)
		{
			if (symolIDs[i] == id)
			{
				return i;
			}
		}
		return 0;
	}

	public int[] GetSymolIDs(SymbolType type)
	{
		switch (type)
		{
		case SymbolType.MARK:
			return markIDs;
		case SymbolType.FRAME:
		case SymbolType.FRAME_OUTLINE:
			return frameIDs;
		case SymbolType.PATTERN:
			return patternIDs;
		default:
			return null;
		}
	}

	public int[] GetSortSymbolIDs(SymbolType type)
	{
		switch (type)
		{
		case SymbolType.MARK:
			return markSortIDs;
		case SymbolType.FRAME:
		case SymbolType.FRAME_OUTLINE:
			return frameSortIDs;
		case SymbolType.PATTERN:
			return patternSortIDs;
		default:
			return null;
		}
	}

	public int[] SymbolSort(List<DisplayOrderData> data)
	{
		DisplayOrderData[] source = (from s in data
		group s by s.sort into g
		orderby g.Key
		select g).SelectMany((IGrouping<int, DisplayOrderData> g) => from s in g
		orderby s.order
		select s).ToArray();
		return (from s in source
		select s.num).ToArray();
	}

	public Color[] GetColors(SymbolType type)
	{
		switch (type)
		{
		case SymbolType.MARK:
			return markColors;
		case SymbolType.FRAME:
		case SymbolType.FRAME_OUTLINE:
			return frameColors;
		case SymbolType.PATTERN:
			return patternColors;
		default:
			return null;
		}
	}
}
