// Decompiled with JetBrains decompiler
// Type: TableUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
public class TableUtility
{
  private static char[] charSeparators = new char[3]
  {
    ',',
    '/',
    '_'
  };

  public static List<T> CreateListTable<T>(
    string text,
    TableUtility.CallBackListReadCSV<T> cb,
    string name_table,
    bool ignore_top_empty = true)
    where T : new()
  {
    List<T> listTable = new List<T>();
    CSVReader csv = new CSVReader(text, name_table);
    while (csv.NextLine())
    {
      if (!ignore_top_empty || !csv.IsEmpty())
      {
        T data = new T();
        if (cb(csv, data))
        {
          listTable.Add(data);
        }
        else
        {
          listTable.Clear();
          return (List<T>) null;
        }
      }
    }
    return listTable;
  }

  public static UIntKeyTable<T> CreateUIntKeyTable<T>(
    string text,
    TableUtility.CallBackUIntKeyReadCSV<T> cb,
    string name_table,
    TableUtility.Progress progress = null)
    where T : new()
  {
    UIntKeyTable<T> table = new UIntKeyTable<T>();
    if (!TableUtility.AddUIntKeyTable<T>(table, text, cb, name_table, progress))
      table.Clear();
    return table;
  }

  public static bool AddUIntKeyTable<T>(
    UIntKeyTable<T> table,
    string text,
    TableUtility.CallBackUIntKeyReadCSV<T> cb,
    string name_table,
    TableUtility.Progress progress = null)
    where T : new()
  {
    CSVReader csv = new CSVReader(text, name_table);
    float num1 = 1f;
    if (progress != null)
      num1 = progress.value;
    float num2 = 1f - num1;
    float length = (float) text.Length;
    while (csv.NextLine())
    {
      string empty = string.Empty;
      if ((bool) csv.Pop(ref empty))
      {
        if (empty.Length > 0)
        {
          uint key = uint.Parse(empty);
          T data = new T();
          if (!cb(csv, data, ref key))
            return false;
          table.Add(key, data);
        }
        if (progress != null)
          progress.value = num1 + num2 * (float) csv.GetPosition() / length;
      }
    }
    return true;
  }

  public static bool AddTripleUIntKeyTable<T>(
    TripleUIntKeyTable<T> table1,
    string text,
    TableUtility.CallBackTripleUIntKeyReadCSV<T> cb,
    string name_table)
    where T : new()
  {
    CSVReader csv = new CSVReader(text, name_table);
    while (csv.NextLine())
    {
      string empty1 = string.Empty;
      csv.Pop(ref empty1);
      if (!string.IsNullOrEmpty(empty1))
      {
        uint key1 = uint.Parse(empty1);
        string empty2 = string.Empty;
        csv.Pop(ref empty2);
        if (!string.IsNullOrEmpty(empty2))
        {
          uint key2 = uint.Parse(empty2);
          string empty3 = string.Empty;
          csv.Pop(ref empty3);
          if (!string.IsNullOrEmpty(empty3))
          {
            uint key3 = uint.Parse(empty3);
            T data = new T();
            if (!cb(csv, data, ref key1, ref key2, ref key3))
              return false;
            UIntKeyTable<UIntKeyTable<T>> uintKeyTable1 = table1.Get(key1);
            if (uintKeyTable1 == null)
            {
              uintKeyTable1 = new UIntKeyTable<UIntKeyTable<T>>(false);
              table1.Add(key1, uintKeyTable1);
            }
            UIntKeyTable<T> uintKeyTable2 = uintKeyTable1.Get(key2);
            if (uintKeyTable2 == null)
            {
              uintKeyTable2 = new UIntKeyTable<T>(false);
              uintKeyTable1.Add(key2, uintKeyTable2);
            }
            uintKeyTable2.Add(key3, data);
          }
        }
      }
    }
    return true;
  }

  public static bool AddDoubleUIntKeyTable<T>(
    DoubleUIntKeyTable<T> table1,
    string text,
    TableUtility.CallBackDoubleUIntKeyReadCSV<T> cb,
    string name_table,
    TableUtility.CallBackDoubleUIntSecondKey cb_second_key,
    TableUtility.CallBackDoubleUIntParseKey cb_parse_first_key = null,
    TableUtility.CallBackDoubleUIntParseKey cb_parse_second_key = null)
    where T : new()
  {
    CSVReader csv = new CSVReader(text, name_table);
    while (csv.NextLine())
    {
      string empty = string.Empty;
      string str = string.Empty;
      csv.Pop(ref empty);
      if (!string.IsNullOrEmpty(empty))
      {
        uint key1 = cb_parse_first_key != null ? cb_parse_first_key(empty) : uint.Parse(empty);
        if (cb_second_key != null)
        {
          UIntKeyTable<T> uintKeyTable = table1.Get(key1);
          int count = uintKeyTable != null ? uintKeyTable.GetCount() : 0;
          str = cb_second_key(csv, count);
        }
        else
          csv.Pop(ref str);
        if (!string.IsNullOrEmpty(str))
        {
          uint key2 = 0;
          key2 = cb_parse_second_key != null ? cb_parse_second_key(str) : uint.Parse(str);
          T data = new T();
          if (!cb(csv, data, ref key1, ref key2))
            return false;
          UIntKeyTable<T> uintKeyTable = table1.Get(key1);
          if (uintKeyTable == null)
          {
            uintKeyTable = new UIntKeyTable<T>(false);
            table1.Add(key1, uintKeyTable);
          }
          uintKeyTable.Add(key2, data);
        }
      }
    }
    return true;
  }

  public static StringKeyTable<T> CreateStringKeyTable<T>(
    string text,
    TableUtility.CallBackStringKeyReadCSV<T> cb,
    string name_table)
    where T : new()
  {
    StringKeyTable<T> stringKeyTable = new StringKeyTable<T>();
    CSVReader csv = new CSVReader(text, name_table);
    while (csv.NextLine())
    {
      string empty = string.Empty;
      if ((bool) csv.Pop(ref empty) && empty.Length > 0)
      {
        T data = new T();
        if (cb(csv, data, ref empty))
        {
          stringKeyTable.Add(empty, data);
        }
        else
        {
          stringKeyTable.Clear();
          return (StringKeyTable<T>) null;
        }
      }
    }
    return stringKeyTable;
  }

  public static TripleUIntKeyTable<T> CreateTripleUIntKeyTable<T>(
    string text,
    TableUtility.CallBackTripleUIntKeyReadCSV<T> cb,
    string name_table)
    where T : new()
  {
    TripleUIntKeyTable<T> tripleUintKeyTable = new TripleUIntKeyTable<T>();
    CSVReader csv = new CSVReader(text, name_table);
    while (csv.NextLine())
    {
      string empty1 = string.Empty;
      csv.Pop(ref empty1);
      if (!string.IsNullOrEmpty(empty1))
      {
        uint key1 = uint.Parse(empty1);
        string empty2 = string.Empty;
        csv.Pop(ref empty2);
        if (!string.IsNullOrEmpty(empty2))
        {
          uint key2 = uint.Parse(empty2);
          string empty3 = string.Empty;
          csv.Pop(ref empty3);
          if (!string.IsNullOrEmpty(empty3))
          {
            uint key3 = uint.Parse(empty3);
            T data = new T();
            if (cb(csv, data, ref key1, ref key2, ref key3))
            {
              UIntKeyTable<UIntKeyTable<T>> uintKeyTable1 = tripleUintKeyTable.Get(key1);
              if (uintKeyTable1 == null)
              {
                uintKeyTable1 = new UIntKeyTable<UIntKeyTable<T>>(false);
                tripleUintKeyTable.Add(key1, uintKeyTable1);
              }
              UIntKeyTable<T> uintKeyTable2 = uintKeyTable1.Get(key2);
              if (uintKeyTable2 == null)
              {
                uintKeyTable2 = new UIntKeyTable<T>(false);
                uintKeyTable1.Add(key2, uintKeyTable2);
              }
              uintKeyTable2.Add(key3, data);
            }
            else
            {
              tripleUintKeyTable.Clear();
              return (TripleUIntKeyTable<T>) null;
            }
          }
        }
      }
    }
    return tripleUintKeyTable;
  }

  public static DoubleUIntKeyTable<T> CreateDoubleUIntKeyTable<T>(
    string text,
    TableUtility.CallBackDoubleUIntKeyReadCSV<T> cb,
    string name_table,
    TableUtility.CallBackDoubleUIntSecondKey cb_second_key,
    TableUtility.CallBackDoubleUIntParseKey cb_parse_first_key = null,
    TableUtility.CallBackDoubleUIntParseKey cb_parse_second_key = null,
    TableUtility.Progress progress = null)
    where T : new()
  {
    DoubleUIntKeyTable<T> doubleUintKeyTable = new DoubleUIntKeyTable<T>();
    CSVReader csv = new CSVReader(text, name_table);
    float num1 = 1f;
    if (progress != null)
      num1 = progress.value;
    float num2 = 1f - num1;
    float length = (float) text.Length;
    while (csv.NextLine())
    {
      string empty = string.Empty;
      string str = string.Empty;
      csv.Pop(ref empty);
      if (!string.IsNullOrEmpty(empty))
      {
        uint key1 = cb_parse_first_key != null ? cb_parse_first_key(empty) : uint.Parse(empty);
        if (cb_second_key != null)
        {
          UIntKeyTable<T> uintKeyTable = doubleUintKeyTable.Get(key1);
          int count = uintKeyTable != null ? uintKeyTable.GetCount() : 0;
          str = cb_second_key(csv, count);
        }
        else
          csv.Pop(ref str);
        if (!string.IsNullOrEmpty(str))
        {
          uint key2 = cb_parse_second_key != null ? cb_parse_second_key(str) : uint.Parse(str);
          T data = new T();
          if (cb(csv, data, ref key1, ref key2))
          {
            UIntKeyTable<T> uintKeyTable = doubleUintKeyTable.Get(key1);
            if (uintKeyTable == null)
            {
              uintKeyTable = new UIntKeyTable<T>(false);
              doubleUintKeyTable.Add(key1, uintKeyTable);
            }
            uintKeyTable.Add(key2, data);
            if (progress != null)
              progress.value = num1 + num2 * (float) csv.GetPosition() / length;
          }
          else
          {
            doubleUintKeyTable.Clear();
            return (DoubleUIntKeyTable<T>) null;
          }
        }
      }
    }
    return doubleUintKeyTable;
  }

  public static UIntKeyTable<List<T>> CreateUIntKeyListTable<T>(
    string text,
    TableUtility.CallBackUIntKeyReadCSV<T> cb,
    string name_table)
    where T : new()
  {
    UIntKeyTable<List<T>> table = new UIntKeyTable<List<T>>();
    if (!TableUtility.AddUIntKeyListTable<T>(table, text, cb, name_table))
      table.Clear();
    return table;
  }

  public static bool AddUIntKeyListTable<T>(
    UIntKeyTable<List<T>> table,
    string text,
    TableUtility.CallBackUIntKeyReadCSV<T> cb,
    string name_table)
    where T : new()
  {
    CSVReader csv = new CSVReader(text, name_table);
    while (csv.NextLine())
    {
      string empty = string.Empty;
      if ((bool) csv.Pop(ref empty) && empty.Length > 0)
      {
        uint key = uint.Parse(empty);
        T data = new T();
        if (!cb(csv, data, ref key))
          return false;
        List<T> objList = table.Get(key);
        if (objList == null)
        {
          objList = new List<T>();
          table.Add(key, objList);
        }
        objList.Add(data);
      }
    }
    return true;
  }

  public static UIntKeyTable<T> CreateUIntKeyTableFromBinary<T>(byte[] bytes) where T : IUIntKeyBinaryTableData, new()
  {
    UIntKeyTable<T> keyTableFromBinary = new UIntKeyTable<T>();
    BinaryTableReader reader = new BinaryTableReader(bytes);
    while (reader.MoveNext())
    {
      uint key1 = reader.ReadUInt32();
      T obj = new T();
      obj.LoadFromBinary(reader, ref key1);
      keyTableFromBinary.Add(key1, obj);
    }
    return keyTableFromBinary;
  }

  public static UIntKeyTable<T> CreateUIntKeyTableFromBinary<T>(MemoryStream stream) where T : IUIntKeyBinaryTableData, new()
  {
    UIntKeyTable<T> keyTableFromBinary = new UIntKeyTable<T>();
    BinaryTableReader reader = new BinaryTableReader(stream);
    while (reader.MoveNext())
    {
      uint key1 = reader.ReadUInt32();
      T obj = new T();
      obj.LoadFromBinary(reader, ref key1);
      keyTableFromBinary.Add(key1, obj);
    }
    return keyTableFromBinary;
  }

  public static DoubleUIntKeyTable<T> CreateDoubleUIntKeyTableFromBinary<T>(byte[] bytes) where T : IDoubleUIntKeyBinaryTableData, new()
  {
    DoubleUIntKeyTable<T> keyTableFromBinary = new DoubleUIntKeyTable<T>();
    BinaryTableReader reader = new BinaryTableReader(bytes);
    while (reader.MoveNext())
    {
      uint key1 = reader.ReadUInt32();
      uint key2 = reader.ReadUInt32();
      T obj = new T();
      obj.LoadFromBinary(reader, ref key1, ref key2);
      keyTableFromBinary.Add(key1, key2, obj);
    }
    return keyTableFromBinary;
  }

  public static DoubleUIntKeyTable<T> CreateDoubleUIntKeyTableFromBinary<T>(MemoryStream stream) where T : IDoubleUIntKeyBinaryTableData, new()
  {
    DoubleUIntKeyTable<T> keyTableFromBinary = new DoubleUIntKeyTable<T>();
    BinaryTableReader reader = new BinaryTableReader(stream);
    while (reader.MoveNext())
    {
      uint key1 = reader.ReadUInt32();
      uint key2 = reader.ReadUInt32();
      T obj = new T();
      obj.LoadFromBinary(reader, ref key1, ref key2);
      keyTableFromBinary.Add(key1, key2, obj);
    }
    return keyTableFromBinary;
  }

  public static int[] ParseStringToIntArray(string buff)
  {
    int[] stringToIntArray = (int[]) null;
    if (!string.IsNullOrEmpty(buff))
    {
      string[] strArray = buff.Split(TableUtility.charSeparators, StringSplitOptions.RemoveEmptyEntries);
      if (strArray != null && strArray.Length != 0)
      {
        stringToIntArray = new int[strArray.Length];
        for (int index = 0; index < strArray.Length; ++index)
        {
          int result = 0;
          int.TryParse(strArray[index], out result);
          stringToIntArray[index] = result;
        }
      }
    }
    return stringToIntArray;
  }

  public delegate bool CallBackListReadCSV<T>(CSVReader csv, T data);

  public delegate bool CallBackUIntKeyReadCSV<T>(CSVReader csv, T data, ref uint key);

  public delegate bool CallBackTripleUIntKeyReadCSV<T>(
    CSVReader csv,
    T data,
    ref uint key1,
    ref uint key2,
    ref uint key3);

  public delegate bool CallBackDoubleUIntKeyReadCSV<T>(
    CSVReader csv,
    T data,
    ref uint key1,
    ref uint key2);

  public delegate bool CallBackStringKeyReadCSV<T>(CSVReader csv, T data, ref string key);

  public delegate string CallBackDoubleUIntSecondKey(CSVReader csv, int table_data_num);

  public delegate uint CallBackDoubleUIntParseKey(string key);

  public class Progress
  {
    public float value;
  }
}
