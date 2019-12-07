using System.Collections.Generic;
using UnityEngine;

public class StringTable : Singleton<StringTable>, IDataTable
{
	public static readonly string STRING_DATA_TABLE = "StringTable";

	public const string ERROR_TEXT = "ERR::STRING_NOT_FOUND";

	public const string NT = "category,id,strJP";

	public StringKeyTable<UIntKeyTable<string>> stringKeyTable
	{
		get;
		private set;
	}

	public static string Get(STRING_CATEGORY category, uint id)
	{
		if (!Singleton<StringTable>.IsValid())
		{
			return "ERR::STRING_NOT_FOUND";
		}
		UIntKeyTable<string> uIntKeyTable = Singleton<StringTable>.I.stringKeyTable.Get(category.ToString());
		if (uIntKeyTable == null)
		{
			return "ERR::STRING_NOT_FOUND";
		}
		string text = uIntKeyTable.Get(id);
		if (text != null)
		{
			return text;
		}
		return "ERR::STRING_NOT_FOUND";
	}

	public static string[] GetAllInCategory(STRING_CATEGORY category)
	{
		List<string> texts = new List<string>();
		if (!Singleton<StringTable>.IsValid())
		{
			return texts.ToArray();
		}
		UIntKeyTable<string> uIntKeyTable = Singleton<StringTable>.I.stringKeyTable.Get(category.ToString());
		if (uIntKeyTable == null)
		{
			return texts.ToArray();
		}
		uIntKeyTable.ForEach(delegate(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				texts.Add("ERR::STRING_NOT_FOUND");
			}
			else
			{
				texts.Add(text);
			}
		});
		return texts.ToArray();
	}

	public static int[] GetAllKeyInCategory(STRING_CATEGORY category)
	{
		List<int> keys = new List<int>();
		if (!Singleton<StringTable>.IsValid())
		{
			return keys.ToArray();
		}
		UIntKeyTable<string> uIntKeyTable = Singleton<StringTable>.I.stringKeyTable.Get(category.ToString());
		if (uIntKeyTable == null)
		{
			return keys.ToArray();
		}
		uIntKeyTable.ForEachKey(delegate(uint key)
		{
			keys.Add((int)key);
		});
		return keys.ToArray();
	}

	public static Dictionary<int, string> GetCategoryMap(STRING_CATEGORY category)
	{
		Dictionary<int, string> categoryMap = new Dictionary<int, string>();
		if (!Singleton<StringTable>.IsValid())
		{
			return categoryMap;
		}
		UIntKeyTable<string> uIntKeyTable = Singleton<StringTable>.I.stringKeyTable.Get(category.ToString());
		if (uIntKeyTable == null)
		{
			return categoryMap;
		}
		string[] values = GetAllInCategory(category);
		int i = 0;
		uIntKeyTable.ForEachKey(delegate(uint key)
		{
			categoryMap.Add((int)key, values[i]);
			i++;
		});
		return categoryMap;
	}

	public static string GetErrorCodeText(uint id)
	{
		if (!Singleton<StringTable>.IsValid())
		{
			return "ERR::STRING_NOT_FOUND";
		}
		UIntKeyTable<string> uIntKeyTable = Singleton<StringTable>.I.stringKeyTable.Get("ERROR_DIALOG");
		if (uIntKeyTable == null)
		{
			return GetErrorCodeText(0u);
		}
		string text = uIntKeyTable.Get(id);
		if (text != null)
		{
			return text;
		}
		return "ERR::STRING_NOT_FOUND";
	}

	public static string GetErrorMessage(uint id)
	{
		string errorCodeText = GetErrorCodeText(id);
		if (errorCodeText == null)
		{
			errorCodeText = GetErrorCodeText(0u);
		}
		return Format(STRING_CATEGORY.COMMON_DIALOG, 1000u, errorCodeText, id);
	}

	public static string Format(STRING_CATEGORY category, uint id, params object[] args)
	{
		if (!Singleton<StringTable>.IsValid())
		{
			return "ERR::STRING_NOT_FOUND";
		}
		UIntKeyTable<string> uIntKeyTable = Singleton<StringTable>.I.stringKeyTable.Get(category.ToString());
		if (uIntKeyTable == null)
		{
			return "ERR::STRING_NOT_FOUND";
		}
		string text = uIntKeyTable.Get(id);
		if (text == null)
		{
			return "ERR::STRING_NOT_FOUND";
		}
		if (args == null)
		{
			return text;
		}
		return string.Format(text, args);
	}

	public void CreateTable(TextAsset csv = null)
	{
		bool encrypted = false;
		if (csv == null)
		{
			csv = Resources.Load<TextAsset>("Internal/internal__TABLE__StringTable");
			encrypted = true;
		}
		CreateTable(csv.text, encrypted);
	}

	public void CreateTable(string csv_text)
	{
		CreateTable(csv_text, encrypted: false);
	}

	public void CreateTable(string csv_text, bool encrypted)
	{
		stringKeyTable = new StringKeyTable<UIntKeyTable<string>>();
		CSVReader cSVReader = new CSVReader(csv_text, "category,id,strJP", encrypted);
		UIntKeyTable<string> uIntKeyTable = null;
		while (cSVReader.NextLine())
		{
			string value = string.Empty;
			cSVReader.Pop(ref value);
			if (value.Length > 0)
			{
				uIntKeyTable = stringKeyTable.Get(value);
				if (uIntKeyTable == null)
				{
					uIntKeyTable = new UIntKeyTable<string>();
					stringKeyTable.Add(value, uIntKeyTable);
				}
			}
			if (uIntKeyTable != null)
			{
				uint value2 = 0u;
				bool flag = cSVReader.Pop(ref value2);
				string value3 = string.Empty;
				_ = (bool)cSVReader.Pop(ref value3);
				if (flag && value3.Length != 0)
				{
					uIntKeyTable.Add(value2, value3);
				}
			}
		}
		stringKeyTable.TrimExcess();
	}
}
