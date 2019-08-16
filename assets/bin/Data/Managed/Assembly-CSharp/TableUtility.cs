using System;
using System.Collections.Generic;
using System.IO;

public class TableUtility
{
	public delegate bool CallBackListReadCSV<T>(CSVReader csv, T data);

	public delegate bool CallBackUIntKeyReadCSV<T>(CSVReader csv, T data, ref uint key);

	public delegate bool CallBackTripleUIntKeyReadCSV<T>(CSVReader csv, T data, ref uint key1, ref uint key2, ref uint key3);

	public delegate bool CallBackDoubleUIntKeyReadCSV<T>(CSVReader csv, T data, ref uint key1, ref uint key2);

	public delegate bool CallBackStringKeyReadCSV<T>(CSVReader csv, T data, ref string key);

	public delegate string CallBackDoubleUIntSecondKey(CSVReader csv, int table_data_num);

	public delegate uint CallBackDoubleUIntParseKey(string key);

	public class Progress
	{
		public float value;
	}

	private static char[] charSeparators = new char[3]
	{
		',',
		'/',
		'_'
	};

	public static List<T> CreateListTable<T>(string text, CallBackListReadCSV<T> cb, string name_table, bool ignore_top_empty = true) where T : new()
	{
		List<T> list = new List<T>();
		CSVReader cSVReader = new CSVReader(text, name_table);
		while (cSVReader.NextLine())
		{
			if (!ignore_top_empty || !cSVReader.IsEmpty())
			{
				T val = new T();
				if (!cb(cSVReader, val))
				{
					list.Clear();
					return null;
				}
				list.Add(val);
			}
		}
		return list;
	}

	public static UIntKeyTable<T> CreateUIntKeyTable<T>(string text, CallBackUIntKeyReadCSV<T> cb, string name_table, Progress progress = null) where T : new()
	{
		UIntKeyTable<T> uIntKeyTable = new UIntKeyTable<T>();
		if (!AddUIntKeyTable(uIntKeyTable, text, cb, name_table, progress))
		{
			uIntKeyTable.Clear();
		}
		return uIntKeyTable;
	}

	public static bool AddUIntKeyTable<T>(UIntKeyTable<T> table, string text, CallBackUIntKeyReadCSV<T> cb, string name_table, Progress progress = null) where T : new()
	{
		CSVReader cSVReader = new CSVReader(text, name_table);
		float num = 1f;
		if (progress != null)
		{
			num = progress.value;
		}
		float num2 = 1f - num;
		float num3 = text.Length;
		while (cSVReader.NextLine())
		{
			string value = string.Empty;
			if (!(bool)cSVReader.Pop(ref value))
			{
				continue;
			}
			if (value.Length > 0)
			{
				uint key = uint.Parse(value);
				T val = new T();
				if (!cb(cSVReader, val, ref key))
				{
					return false;
				}
				bool flag = !table.Add(key, val);
			}
			if (progress != null)
			{
				progress.value = num + num2 * (float)cSVReader.GetPosition() / num3;
			}
		}
		return true;
	}

	public static bool AddTripleUIntKeyTable<T>(TripleUIntKeyTable<T> table1, string text, CallBackTripleUIntKeyReadCSV<T> cb, string name_table) where T : new()
	{
		CSVReader cSVReader = new CSVReader(text, name_table);
		while (cSVReader.NextLine())
		{
			string value = string.Empty;
			cSVReader.Pop(ref value);
			if (string.IsNullOrEmpty(value))
			{
				continue;
			}
			uint key = uint.Parse(value);
			string value2 = string.Empty;
			cSVReader.Pop(ref value2);
			if (string.IsNullOrEmpty(value2))
			{
				continue;
			}
			uint key2 = uint.Parse(value2);
			string value3 = string.Empty;
			cSVReader.Pop(ref value3);
			if (!string.IsNullOrEmpty(value3))
			{
				uint key3 = uint.Parse(value3);
				T val = new T();
				if (!cb(cSVReader, val, ref key, ref key2, ref key3))
				{
					return false;
				}
				UIntKeyTable<UIntKeyTable<T>> uIntKeyTable = table1.Get(key);
				if (uIntKeyTable == null)
				{
					uIntKeyTable = new UIntKeyTable<UIntKeyTable<T>>(useHashDivision: false);
					table1.Add(key, uIntKeyTable);
				}
				UIntKeyTable<T> uIntKeyTable2 = uIntKeyTable.Get(key2);
				if (uIntKeyTable2 == null)
				{
					uIntKeyTable2 = new UIntKeyTable<T>(useHashDivision: false);
					uIntKeyTable.Add(key2, uIntKeyTable2);
				}
				uIntKeyTable2.Add(key3, val);
			}
		}
		return true;
	}

	public static bool AddDoubleUIntKeyTable<T>(DoubleUIntKeyTable<T> table1, string text, CallBackDoubleUIntKeyReadCSV<T> cb, string name_table, CallBackDoubleUIntSecondKey cb_second_key, CallBackDoubleUIntParseKey cb_parse_first_key = null, CallBackDoubleUIntParseKey cb_parse_second_key = null) where T : new()
	{
		CSVReader cSVReader = new CSVReader(text, name_table);
		int num = 0;
		while (cSVReader.NextLine())
		{
			string value = string.Empty;
			string value2 = string.Empty;
			uint num2 = 0u;
			cSVReader.Pop(ref value);
			if (string.IsNullOrEmpty(value))
			{
				continue;
			}
			num2 = (cb_parse_first_key?.Invoke(value) ?? uint.Parse(value));
			if (cb_second_key != null)
			{
				num = (table1.Get(num2)?.GetCount() ?? 0);
				value2 = cb_second_key(cSVReader, num);
			}
			else
			{
				cSVReader.Pop(ref value2);
			}
			if (!string.IsNullOrEmpty(value2))
			{
				uint num3 = 0u;
				num3 = (cb_parse_second_key?.Invoke(value2) ?? uint.Parse(value2));
				T val = new T();
				if (!cb(cSVReader, val, ref num2, ref num3))
				{
					return false;
				}
				UIntKeyTable<T> uIntKeyTable = table1.Get(num2);
				if (uIntKeyTable == null)
				{
					uIntKeyTable = new UIntKeyTable<T>(useHashDivision: false);
					table1.Add(num2, uIntKeyTable);
				}
				uIntKeyTable.Add(num3, val);
			}
		}
		return true;
	}

	public static StringKeyTable<T> CreateStringKeyTable<T>(string text, CallBackStringKeyReadCSV<T> cb, string name_table) where T : new()
	{
		StringKeyTable<T> stringKeyTable = new StringKeyTable<T>();
		CSVReader cSVReader = new CSVReader(text, name_table);
		while (cSVReader.NextLine())
		{
			string value = string.Empty;
			if ((bool)cSVReader.Pop(ref value) && value.Length > 0)
			{
				T val = new T();
				if (!cb(cSVReader, val, ref value))
				{
					stringKeyTable.Clear();
					return null;
				}
				stringKeyTable.Add(value, val);
			}
		}
		return stringKeyTable;
	}

	public static TripleUIntKeyTable<T> CreateTripleUIntKeyTable<T>(string text, CallBackTripleUIntKeyReadCSV<T> cb, string name_table) where T : new()
	{
		TripleUIntKeyTable<T> tripleUIntKeyTable = new TripleUIntKeyTable<T>();
		CSVReader cSVReader = new CSVReader(text, name_table);
		while (cSVReader.NextLine())
		{
			string value = string.Empty;
			cSVReader.Pop(ref value);
			if (string.IsNullOrEmpty(value))
			{
				continue;
			}
			uint key = uint.Parse(value);
			string value2 = string.Empty;
			cSVReader.Pop(ref value2);
			if (string.IsNullOrEmpty(value2))
			{
				continue;
			}
			uint key2 = uint.Parse(value2);
			string value3 = string.Empty;
			cSVReader.Pop(ref value3);
			if (!string.IsNullOrEmpty(value3))
			{
				uint key3 = uint.Parse(value3);
				T val = new T();
				if (!cb(cSVReader, val, ref key, ref key2, ref key3))
				{
					tripleUIntKeyTable.Clear();
					return null;
				}
				UIntKeyTable<UIntKeyTable<T>> uIntKeyTable = tripleUIntKeyTable.Get(key);
				if (uIntKeyTable == null)
				{
					uIntKeyTable = new UIntKeyTable<UIntKeyTable<T>>(useHashDivision: false);
					tripleUIntKeyTable.Add(key, uIntKeyTable);
				}
				UIntKeyTable<T> uIntKeyTable2 = uIntKeyTable.Get(key2);
				if (uIntKeyTable2 == null)
				{
					uIntKeyTable2 = new UIntKeyTable<T>(useHashDivision: false);
					uIntKeyTable.Add(key2, uIntKeyTable2);
				}
				uIntKeyTable2.Add(key3, val);
			}
		}
		return tripleUIntKeyTable;
	}

	public static DoubleUIntKeyTable<T> CreateDoubleUIntKeyTable<T>(string text, CallBackDoubleUIntKeyReadCSV<T> cb, string name_table, CallBackDoubleUIntSecondKey cb_second_key, CallBackDoubleUIntParseKey cb_parse_first_key = null, CallBackDoubleUIntParseKey cb_parse_second_key = null, Progress progress = null) where T : new()
	{
		DoubleUIntKeyTable<T> doubleUIntKeyTable = new DoubleUIntKeyTable<T>();
		int num = 0;
		CSVReader cSVReader = new CSVReader(text, name_table);
		float num2 = 1f;
		if (progress != null)
		{
			num2 = progress.value;
		}
		float num3 = 1f - num2;
		float num4 = text.Length;
		while (cSVReader.NextLine())
		{
			string value = string.Empty;
			string value2 = string.Empty;
			uint num5 = 0u;
			cSVReader.Pop(ref value);
			if (string.IsNullOrEmpty(value))
			{
				continue;
			}
			num5 = (cb_parse_first_key?.Invoke(value) ?? uint.Parse(value));
			if (cb_second_key != null)
			{
				num = (doubleUIntKeyTable.Get(num5)?.GetCount() ?? 0);
				value2 = cb_second_key(cSVReader, num);
			}
			else
			{
				cSVReader.Pop(ref value2);
			}
			if (!string.IsNullOrEmpty(value2))
			{
				uint num6 = 0u;
				num6 = (cb_parse_second_key?.Invoke(value2) ?? uint.Parse(value2));
				T val = new T();
				if (!cb(cSVReader, val, ref num5, ref num6))
				{
					doubleUIntKeyTable.Clear();
					return null;
				}
				UIntKeyTable<T> uIntKeyTable = doubleUIntKeyTable.Get(num5);
				if (uIntKeyTable == null)
				{
					uIntKeyTable = new UIntKeyTable<T>(useHashDivision: false);
					doubleUIntKeyTable.Add(num5, uIntKeyTable);
				}
				uIntKeyTable.Add(num6, val);
				if (progress != null)
				{
					progress.value = num2 + num3 * (float)cSVReader.GetPosition() / num4;
				}
			}
		}
		return doubleUIntKeyTable;
	}

	public static UIntKeyTable<List<T>> CreateUIntKeyListTable<T>(string text, CallBackUIntKeyReadCSV<T> cb, string name_table) where T : new()
	{
		UIntKeyTable<List<T>> uIntKeyTable = new UIntKeyTable<List<T>>();
		if (!AddUIntKeyListTable(uIntKeyTable, text, cb, name_table))
		{
			uIntKeyTable.Clear();
		}
		return uIntKeyTable;
	}

	public static bool AddUIntKeyListTable<T>(UIntKeyTable<List<T>> table, string text, CallBackUIntKeyReadCSV<T> cb, string name_table) where T : new()
	{
		CSVReader cSVReader = new CSVReader(text, name_table);
		while (cSVReader.NextLine())
		{
			string value = string.Empty;
			if ((bool)cSVReader.Pop(ref value) && value.Length > 0)
			{
				uint key = uint.Parse(value);
				T val = new T();
				if (!cb(cSVReader, val, ref key))
				{
					return false;
				}
				List<T> list = table.Get(key);
				if (list == null)
				{
					list = new List<T>();
					table.Add(key, list);
				}
				list.Add(val);
			}
		}
		return true;
	}

	public static UIntKeyTable<T> CreateUIntKeyTableFromBinary<T>(byte[] bytes) where T : IUIntKeyBinaryTableData, new()
	{
		UIntKeyTable<T> uIntKeyTable = new UIntKeyTable<T>();
		BinaryTableReader binaryTableReader = new BinaryTableReader(bytes);
		while (binaryTableReader.MoveNext())
		{
			uint key = binaryTableReader.ReadUInt32();
			T value = new T();
			value.LoadFromBinary(binaryTableReader, ref key);
			uIntKeyTable.Add(key, value);
		}
		return uIntKeyTable;
	}

	public static UIntKeyTable<T> CreateUIntKeyTableFromBinary<T>(MemoryStream stream) where T : IUIntKeyBinaryTableData, new()
	{
		UIntKeyTable<T> uIntKeyTable = new UIntKeyTable<T>();
		BinaryTableReader binaryTableReader = new BinaryTableReader(stream);
		while (binaryTableReader.MoveNext())
		{
			uint key = binaryTableReader.ReadUInt32();
			T value = new T();
			value.LoadFromBinary(binaryTableReader, ref key);
			uIntKeyTable.Add(key, value);
		}
		return uIntKeyTable;
	}

	public static DoubleUIntKeyTable<T> CreateDoubleUIntKeyTableFromBinary<T>(byte[] bytes) where T : IDoubleUIntKeyBinaryTableData, new()
	{
		DoubleUIntKeyTable<T> doubleUIntKeyTable = new DoubleUIntKeyTable<T>();
		BinaryTableReader binaryTableReader = new BinaryTableReader(bytes);
		while (binaryTableReader.MoveNext())
		{
			uint key = binaryTableReader.ReadUInt32();
			uint key2 = binaryTableReader.ReadUInt32();
			T value = new T();
			value.LoadFromBinary(binaryTableReader, ref key, ref key2);
			doubleUIntKeyTable.Add(key, key2, value);
		}
		return doubleUIntKeyTable;
	}

	public static DoubleUIntKeyTable<T> CreateDoubleUIntKeyTableFromBinary<T>(MemoryStream stream) where T : IDoubleUIntKeyBinaryTableData, new()
	{
		DoubleUIntKeyTable<T> doubleUIntKeyTable = new DoubleUIntKeyTable<T>();
		BinaryTableReader binaryTableReader = new BinaryTableReader(stream);
		while (binaryTableReader.MoveNext())
		{
			uint key = binaryTableReader.ReadUInt32();
			uint key2 = binaryTableReader.ReadUInt32();
			T value = new T();
			value.LoadFromBinary(binaryTableReader, ref key, ref key2);
			doubleUIntKeyTable.Add(key, key2, value);
		}
		return doubleUIntKeyTable;
	}

	public static int[] ParseStringToIntArray(string buff)
	{
		int[] array = null;
		if (!string.IsNullOrEmpty(buff))
		{
			string[] array2 = buff.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
			if (array2 != null && array2.Length > 0)
			{
				array = new int[array2.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					int result = 0;
					int.TryParse(array2[i], out result);
					array[i] = result;
				}
			}
		}
		return array;
	}
}
