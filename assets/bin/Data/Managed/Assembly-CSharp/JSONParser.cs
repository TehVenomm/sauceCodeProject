using System.Collections.Generic;
using UnityEngine;

public class JSONParser
{
	private FlashCompatibleTextReader reader;

	public JSONNode Parse(FlashCompatibleTextReader reader)
	{
		this.reader = reader;
		if (reader.Peek() == -1)
		{
			return null;
		}
		return ReadObject();
	}

	private JSONNode ReadObject()
	{
		JSONNode jSONNode = new JSONNode();
		SkipWhitespace();
		if (reader.Peek() != 123)
		{
			Debug.LogError("malformed json: no starting '{'");
			return null;
		}
		reader.Read();
		while (reader.Peek() != 125)
		{
			if (reader.Peek() == 44)
			{
				reader.Read();
			}
			if (reader.Peek() == 125)
			{
				break;
			}
			SkipWhitespace();
			string fieldName = ReadFieldName().Trim();
			SkipWhitespace();
			reader.Read();
			IJSONFieldValue val = ReadValue();
			jSONNode.AddField(fieldName, val);
			SkipWhitespace();
		}
		reader.Read();
		return jSONNode;
	}

	private IJSONFieldValue ReadValue()
	{
		SkipWhitespace();
		char c = (char)reader.Peek();
		if (c == '"' || c == '\'')
		{
			return new JSONStringFieldValue(ReadString());
		}
		if (!FlashCompatibleConvert.IsDigit(c))
		{
			switch (c)
			{
			case '-':
				break;
			case '[':
				return new JSONListFieldValue(ReadList());
			case '{':
				return new JSONObjectFieldValue(ReadObject());
			case 'f':
			case 't':
				return new JSONBooleanFieldValue(ReadBoolean());
			case 'n':
				ReadNull();
				return new JSONNullFieldValue();
			default:
				return null;
			}
		}
		return new JSONNumberFieldValue(ReadNumber());
	}

	private void SkipWhitespace()
	{
		while (true)
		{
			int num = reader.Peek();
			if (num == -1 || !char.IsWhiteSpace((char)num))
			{
				break;
			}
			reader.Read();
		}
	}

	private string ReadFieldName()
	{
		SkipWhitespace();
		string text = string.Empty;
		char c = (char)reader.Peek();
		bool flag = c == '\'' || c == '"';
		SkipWhitespace();
		while (true)
		{
			switch (c)
			{
			case '}':
				if (text == string.Empty)
				{
					return string.Empty;
				}
				Debug.LogError("malformed json: read '}' before reading ':'");
				return null;
			case ':':
				if (flag && (text.EndsWith("'") || text.EndsWith("\"")))
				{
					text = text.Substring(1, text.Length - 2);
				}
				return text;
			}
			text += ((char)(ushort)reader.Read()).ToString();
			SkipWhitespace();
			c = (char)reader.Peek();
		}
	}

	private double ReadNumber()
	{
		string text = string.Empty;
		while (true)
		{
			int num = reader.Peek();
			if (num == -1 || num == 44 || num == 125 || num == 93 || char.IsWhiteSpace((char)num))
			{
				break;
			}
			text += ((char)(ushort)reader.Read()).ToString();
		}
		return FlashCompatibleConvert.ToDouble(text);
	}

	private bool ReadBoolean()
	{
		char c = (char)reader.Peek();
		bool flag = c == 't';
		for (int i = 0; i < 4; i++)
		{
			reader.Read();
		}
		if (!flag)
		{
			reader.Read();
		}
		return flag;
	}

	private void ReadNull()
	{
		for (int i = 0; i < 4; i++)
		{
			reader.Read();
		}
	}

	private string ReadString()
	{
		string text = string.Empty;
		bool flag = (ushort)reader.Peek() == 39;
		reader.Read();
		while (true)
		{
			char c = (char)reader.Peek();
			if ((!flag && c == '"') || (flag && c == '\''))
			{
				break;
			}
			text += ((char)(ushort)reader.Read()).ToString();
		}
		reader.Read();
		return text;
	}

	private List<IJSONFieldValue> ReadList()
	{
		List<IJSONFieldValue> list = new List<IJSONFieldValue>();
		reader.Read();
		while (true)
		{
			switch ((ushort)reader.Peek())
			{
			case 93:
				reader.Read();
				return list;
			case 44:
				reader.Read();
				SkipWhitespace();
				break;
			default:
			{
				IJSONFieldValue item = ReadValue();
				list.Add(item);
				SkipWhitespace();
				break;
			}
			}
		}
	}
}
