using System;
using System.Collections.Generic;
using System.IO;

public class TableUtility
{
	public class Progress
	{
		public float value;
	}

	public delegate bool CallBackListReadCSV<T>(CSVReader csv, T data);

	public delegate bool CallBackUIntKeyReadCSV<T>(CSVReader csv, T data, ref uint key);

	public delegate bool CallBackDoubleUIntKeyReadCSV<T>(CSVReader csv, T data, ref uint key1, ref uint key2);

	public delegate bool CallBackStringKeyReadCSV<T>(CSVReader csv, T data, ref string key);

	public delegate string CallBackDoubleUIntSecondKey(CSVReader csv, int table_data_num);

	public delegate uint CallBackDoubleUIntParseKey(string key);

	private static char[] charSeparators = new char[3]
	{
		',',
		'/',
		'_'
	};

	public static List<T> CreateListTable<T>(string text, CallBackListReadCSV<T> cb, string name_table, bool ignore_top_empty = true) where T : new()
	{
		List<T> list = new List<T>();
		CSVReader cSVReader = new CSVReader(text, name_table, false);
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
		CSVReader cSVReader = new CSVReader(text, name_table, false);
		float num = 1f;
		if (progress != null)
		{
			num = progress.value;
		}
		float num2 = 1f - num;
		float num3 = (float)text.Length;
		while (cSVReader.NextLine())
		{
			string value = string.Empty;
			if ((bool)cSVReader.Pop(ref value))
			{
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
		}
		return true;
	}

	public static bool AddDoubleUIntKeyTable<T>(DoubleUIntKeyTable<T> table1, string text, CallBackDoubleUIntKeyReadCSV<T> cb, string name_table, CallBackDoubleUIntSecondKey cb_second_key, CallBackDoubleUIntParseKey cb_parse_first_key = null, CallBackDoubleUIntParseKey cb_parse_second_key = null) where T : new()
	{
		CSVReader cSVReader = new CSVReader(text, name_table, false);
		int num = 0;
		while (cSVReader.NextLine())
		{
			string value = string.Empty;
			string value2 = string.Empty;
			uint key = 0u;
			cSVReader.Pop(ref value);
			if (!string.IsNullOrEmpty(value))
			{
				key = (cb_parse_first_key?.Invoke(value) ?? uint.Parse(value));
				if (cb_second_key != null)
				{
					num = (((UIntKeyTable<UIntKeyTable<T>>)table1).Get(key)?.GetCount() ?? 0);
					value2 = cb_second_key(cSVReader, num);
				}
				else
				{
					cSVReader.Pop(ref value2);
				}
				if (!string.IsNullOrEmpty(value2))
				{
					uint key2 = 0u;
					key2 = (cb_parse_second_key?.Invoke(value2) ?? uint.Parse(value2));
					T val = new T();
					if (!cb(cSVReader, val, ref key, ref key2))
					{
						return false;
					}
					UIntKeyTable<T> uIntKeyTable = ((UIntKeyTable<UIntKeyTable<T>>)table1).Get(key);
					if (uIntKeyTable == null)
					{
						uIntKeyTable = new UIntKeyTable<T>(false);
						((UIntKeyTable<UIntKeyTable<T>>)table1).Add(key, uIntKeyTable);
					}
					uIntKeyTable.Add(key2, val);
				}
			}
		}
		return true;
	}

	public static StringKeyTable<T> CreateStringKeyTable<T>(string text, CallBackStringKeyReadCSV<T> cb, string name_table) where T : new()
	{
		StringKeyTable<T> stringKeyTable = new StringKeyTable<T>();
		CSVReader cSVReader = new CSVReader(text, name_table, false);
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

	public static DoubleUIntKeyTable<T> CreateDoubleUIntKeyTable<T>(string text, CallBackDoubleUIntKeyReadCSV<T> cb, string name_table, CallBackDoubleUIntSecondKey cb_second_key, CallBackDoubleUIntParseKey cb_parse_first_key = null, CallBackDoubleUIntParseKey cb_parse_second_key = null, Progress progress = null) where T : new()
	{
		DoubleUIntKeyTable<T> doubleUIntKeyTable = new DoubleUIntKeyTable<T>();
		int num = 0;
		CSVReader cSVReader = new CSVReader(text, name_table, false);
		float num2 = 1f;
		if (progress != null)
		{
			num2 = progress.value;
		}
		float num3 = 1f - num2;
		float num4 = (float)text.Length;
		while (cSVReader.NextLine())
		{
			string value = string.Empty;
			string value2 = string.Empty;
			uint key = 0u;
			cSVReader.Pop(ref value);
			if (!string.IsNullOrEmpty(value))
			{
				key = (cb_parse_first_key?.Invoke(value) ?? uint.Parse(value));
				if (cb_second_key != null)
				{
					num = (((UIntKeyTable<UIntKeyTable<T>>)doubleUIntKeyTable).Get(key)?.GetCount() ?? 0);
					value2 = cb_second_key(cSVReader, num);
				}
				else
				{
					cSVReader.Pop(ref value2);
				}
				if (!string.IsNullOrEmpty(value2))
				{
					uint key2 = 0u;
					key2 = (cb_parse_second_key?.Invoke(value2) ?? uint.Parse(value2));
					T val = new T();
					if (!cb(cSVReader, val, ref key, ref key2))
					{
						doubleUIntKeyTable.Clear();
						return null;
					}
					UIntKeyTable<T> uIntKeyTable = ((UIntKeyTable<UIntKeyTable<T>>)doubleUIntKeyTable).Get(key);
					if (uIntKeyTable == null)
					{
						uIntKeyTable = new UIntKeyTable<T>(false);
						((UIntKeyTable<UIntKeyTable<T>>)doubleUIntKeyTable).Add(key, uIntKeyTable);
					}
					uIntKeyTable.Add(key2, val);
					if (progress != null)
					{
						progress.value = num2 + num3 * (float)cSVReader.GetPosition() / num4;
					}
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
		CSVReader cSVReader = new CSVReader(text, name_table, false);
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
			uint key = binaryTableReader.ReadUInt32(0u);
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
			uint key = binaryTableReader.ReadUInt32(0u);
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
			uint key = binaryTableReader.ReadUInt32(0u);
			uint key2 = binaryTableReader.ReadUInt32(0u);
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
			uint key = binaryTableReader.ReadUInt32(0u);
			uint key2 = binaryTableReader.ReadUInt32(0u);
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
