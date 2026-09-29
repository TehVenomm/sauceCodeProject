// Decompiled with JetBrains decompiler
// Type: StringTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class StringTable : Singleton<StringTable>, IDataTable
{
  public static readonly string STRING_DATA_TABLE = nameof (StringTable);
  public const string ERROR_TEXT = "ERR::STRING_NOT_FOUND";
  public const string NT = "category,id,strJP";

  public StringKeyTable<UIntKeyTable<string>> stringKeyTable { get; private set; }

  public static string Get(STRING_CATEGORY category, uint id)
  {
    if (!Singleton<StringTable>.IsValid())
      return "ERR::STRING_NOT_FOUND";
    UIntKeyTable<string> uintKeyTable = Singleton<StringTable>.I.stringKeyTable.Get(category.ToString());
    return uintKeyTable == null ? "ERR::STRING_NOT_FOUND" : uintKeyTable.Get(id) ?? "ERR::STRING_NOT_FOUND";
  }

  public static string[] GetAllInCategory(STRING_CATEGORY category)
  {
    List<string> texts = new List<string>();
    if (!Singleton<StringTable>.IsValid())
      return texts.ToArray();
    UIntKeyTable<string> uintKeyTable = Singleton<StringTable>.I.stringKeyTable.Get(category.ToString());
    if (uintKeyTable == null)
      return texts.ToArray();
    uintKeyTable.ForEach((Action<string>) (text =>
    {
      if (string.IsNullOrEmpty(text))
        texts.Add("ERR::STRING_NOT_FOUND");
      else
        texts.Add(text);
    }));
    return texts.ToArray();
  }

  public static int[] GetAllKeyInCategory(STRING_CATEGORY category)
  {
    List<int> keys = new List<int>();
    if (!Singleton<StringTable>.IsValid())
      return keys.ToArray();
    UIntKeyTable<string> uintKeyTable = Singleton<StringTable>.I.stringKeyTable.Get(category.ToString());
    if (uintKeyTable == null)
      return keys.ToArray();
    uintKeyTable.ForEachKey((Action<uint>) (key => keys.Add((int) key)));
    return keys.ToArray();
  }

  public static Dictionary<int, string> GetCategoryMap(STRING_CATEGORY category)
  {
    Dictionary<int, string> categoryMap = new Dictionary<int, string>();
    if (!Singleton<StringTable>.IsValid())
      return categoryMap;
    UIntKeyTable<string> uintKeyTable = Singleton<StringTable>.I.stringKeyTable.Get(category.ToString());
    if (uintKeyTable == null)
      return categoryMap;
    string[] values = StringTable.GetAllInCategory(category);
    int i = 0;
    uintKeyTable.ForEachKey((Action<uint>) (key =>
    {
      categoryMap.Add((int) key, values[i]);
      ++i;
    }));
    return categoryMap;
  }

  public static string GetErrorCodeText(uint id)
  {
    if (!Singleton<StringTable>.IsValid())
      return "ERR::STRING_NOT_FOUND";
    UIntKeyTable<string> uintKeyTable = Singleton<StringTable>.I.stringKeyTable.Get("ERROR_DIALOG");
    return uintKeyTable == null ? StringTable.GetErrorCodeText(0U) : uintKeyTable.Get(id) ?? "ERR::STRING_NOT_FOUND";
  }

  public static string GetErrorMessage(uint id)
  {
    return StringTable.Format(STRING_CATEGORY.COMMON_DIALOG, 1000U, (object) (StringTable.GetErrorCodeText(id) ?? StringTable.GetErrorCodeText(0U)), (object) id);
  }

  public static string Format(STRING_CATEGORY category, uint id, params object[] args)
  {
    if (!Singleton<StringTable>.IsValid())
      return "ERR::STRING_NOT_FOUND";
    UIntKeyTable<string> uintKeyTable = Singleton<StringTable>.I.stringKeyTable.Get(category.ToString());
    if (uintKeyTable == null)
      return "ERR::STRING_NOT_FOUND";
    string format = uintKeyTable.Get(id);
    if (format == null)
      return "ERR::STRING_NOT_FOUND";
    return args == null ? format : string.Format(format, args);
  }

  public void CreateTable(TextAsset csv = null)
  {
    bool encrypted = false;
    if (Object.op_Equality((Object) csv, (Object) null))
    {
      csv = Resources.Load<TextAsset>("Internal/internal__TABLE__StringTable");
      encrypted = true;
    }
    this.CreateTable(csv.text, encrypted);
  }

  public void CreateTable(string csv_text) => this.CreateTable(csv_text, false);

  public void CreateTable(string csv_text, bool encrypted)
  {
    this.stringKeyTable = new StringKeyTable<UIntKeyTable<string>>();
    CSVReader csvReader = new CSVReader(csv_text, "category,id,strJP", encrypted);
    UIntKeyTable<string> uintKeyTable = (UIntKeyTable<string>) null;
    while (csvReader.NextLine())
    {
      string empty1 = string.Empty;
      csvReader.Pop(ref empty1);
      if (empty1.Length > 0)
      {
        uintKeyTable = this.stringKeyTable.Get(empty1);
        if (uintKeyTable == null)
        {
          uintKeyTable = new UIntKeyTable<string>();
          this.stringKeyTable.Add(empty1, uintKeyTable);
        }
      }
      if (uintKeyTable != null)
      {
        uint key = 0;
        bool flag = (bool) csvReader.Pop(ref key);
        string empty2 = string.Empty;
        int num = (bool) csvReader.Pop(ref empty2) ? 1 : 0;
        if (flag && empty2.Length != 0)
          uintKeyTable.Add(key, empty2);
      }
    }
    this.stringKeyTable.TrimExcess();
  }
}
