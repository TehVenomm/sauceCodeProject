using System;
using System.Text;
using UnityEngine;

public class CSVReader
{
	public class PopResult
	{
		public enum Result
		{
			SUCCESS,
			EMPTY,
			DONT_DEFINE_ERROR,
			UNKNOWN,
			PARSE_ERROR
		}

		private Result _result;

		public static readonly PopResult EMPTY = new PopResult(Result.EMPTY);

		public static readonly PopResult DONT_DEFINE = new PopResult(Result.DONT_DEFINE_ERROR);

		public static readonly PopResult UNKNOWN = new PopResult(Result.UNKNOWN);

		public static readonly PopResult SUCCESS = new PopResult(Result.SUCCESS);

		public static readonly PopResult PARSE_ERROR = new PopResult(Result.PARSE_ERROR);

		public PopResult(Result result)
		{
			_result = result;
		}

		public static bool IsParseSucceeded(PopResult _result)
		{
			return (Result)_result != Result.DONT_DEFINE_ERROR && (Result)_result != Result.UNKNOWN;
		}

		public static bool operator ==(PopResult a, PopResult b)
		{
			if (object.ReferenceEquals(a, b))
			{
				return true;
			}
			if ((object)a == null || (object)b == null)
			{
				return false;
			}
			return a._result == b._result;
		}

		public static bool operator !=(PopResult a, PopResult b)
		{
			return !(a == b);
		}

		public static implicit operator bool(PopResult result)
		{
			return result._result == Result.SUCCESS;
		}

		public static implicit operator Result(PopResult result)
		{
			return result._result;
		}

		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			if (GetType() != o.GetType())
			{
				return false;
			}
			return _result == ((PopResult)o)._result;
		}

		public override int GetHashCode()
		{
			return _result.GetHashCode();
		}
	}

	private int pos;

	private string str;

	private int len;

	private StringBuilder builder;

	private StringBuilder baseBuilder = new StringBuilder(256, 1024);

	private int[] namedIndex;

	private StringBuilder[] namedStr;

	private int namedPopIndex;

	private string nameTable;

	public CSVReader()
	{
	}

	public CSVReader(string text, string name_table, bool decrypt = false)
	{
		Initialize(text, single_line: false, name_table, decrypt);
	}

	public void Initialize(string csv_text, bool single_line = false, string name_table = null, bool decrypt = false)
	{
		if (decrypt)
		{
			csv_text = Cipher.DecryptRJ128("Auto_XlS_To_CSV.", "yCNBH$$rCNGvC+#f", csv_text);
		}
		pos = ((!single_line) ? (-1) : 0);
		str = csv_text;
		len = str.Length;
		namedIndex = null;
		namedStr = null;
		namedPopIndex = 0;
		nameTable = name_table;
		if (!single_line && csv_text.StartsWith("output"))
		{
			pos = 0;
			name_table = null;
		}
		builder = baseBuilder;
		int start_pos = 0;
		if (str[0] == '#')
		{
			start_pos = 1;
		}
		SetupNameTable(nameTable, start_pos);
	}

	private void SetupNameTable(string name_table, int start_pos)
	{
		if (string.IsNullOrEmpty(name_table))
		{
			return;
		}
		string value = string.Empty;
		namedStr = null;
		pos = start_pos;
		int num = 0;
		while (NextValue())
		{
			num++;
		}
		string[] array = name_table.Split(',');
		int[] array2 = new int[num];
		int i = 0;
		for (int num2 = array2.Length; i < num2; i++)
		{
			array2[i] = -1;
		}
		pos = start_pos;
		int j = 0;
		int num3 = -1;
		for (int num4 = array.Length; j < num4; j++)
		{
			bool flag = false;
			while ((bool)Pop(ref value))
			{
				num3++;
				if (value == array[j])
				{
					flag = true;
					array2[num3] = j;
					break;
				}
			}
			if (!flag)
			{
				num3 = -1;
				pos = start_pos;
				while ((bool)Pop(ref value))
				{
					num3++;
					if (value == array[j])
					{
						flag = true;
						array2[num3] = j;
						break;
					}
				}
			}
			if (!flag)
			{
			}
		}
		namedIndex = array2;
		namedStr = new StringBuilder[array.Length];
		int k = 0;
		for (int num5 = namedStr.Length; k < num5; k++)
		{
			namedStr[k] = new StringBuilder(64);
		}
		namedPopIndex = namedStr.Length;
	}

	public bool NextLine()
	{
		if (len <= 0)
		{
			return false;
		}
		if (pos >= len)
		{
			return false;
		}
		if (pos == -1)
		{
			pos = 0;
		}
		else
		{
			while (_NextValue())
			{
			}
			switch (str[pos])
			{
			case '\n':
				pos++;
				if (pos < len && str[pos] == '\r')
				{
					pos++;
				}
				break;
			case '\r':
				pos++;
				if (pos < len && str[pos] == '\n')
				{
					pos++;
				}
				break;
			default:
				return false;
			}
			if (pos >= len)
			{
				return false;
			}
		}
		if (str[pos] == '#')
		{
			SetupNameTable(nameTable, pos + 1);
			return NextLine();
		}
		if (namedIndex != null)
		{
			int i = 0;
			for (int num = namedStr.Length; i < num; i++)
			{
				namedStr[i].Length = 0;
			}
			int j = 0;
			for (int num2 = namedIndex.Length; j != num2; j++)
			{
				if (namedIndex[j] != -1)
				{
					builder = namedStr[namedIndex[j]];
				}
				else
				{
					builder = baseBuilder;
				}
				if (!_NextValue())
				{
					break;
				}
			}
			builder = null;
			namedPopIndex = 0;
		}
		return true;
	}

	public bool NextValue()
	{
		if (namedStr != null)
		{
			if (namedPopIndex < namedStr.Length)
			{
				builder = namedStr[namedPopIndex++];
				return true;
			}
			builder = baseBuilder;
			builder.Length = 0;
			return false;
		}
		return _NextValue();
	}

	private bool _NextValue()
	{
		int num = pos;
		int num2 = len;
		if (num >= num2)
		{
			return false;
		}
		string text = str;
		char c = text[num];
		if (c == '\n' || c == '\r')
		{
			return false;
		}
		builder.Length = 0;
		if (c == '"')
		{
			num++;
			while (num < num2)
			{
				c = text[num++];
				if (c == '"')
				{
					if (num >= num2 || text[num] != '"')
					{
						if (num < num2 && text[num] == ',')
						{
							num++;
						}
						pos = num;
						return true;
					}
					num++;
					builder.Append('"');
				}
				else
				{
					builder.Append(c);
				}
			}
		}
		else
		{
			while (num < num2)
			{
				c = text[num];
				switch (c)
				{
				case ',':
					pos = num + 1;
					return true;
				case '\n':
				case '\r':
					pos = num;
					return true;
				}
				builder.Append(c);
				num++;
				if (num >= num2)
				{
					pos = num;
					return true;
				}
			}
		}
		return false;
	}

	public bool IsEmpty()
	{
		int num = pos;
		int num2 = namedPopIndex;
		bool result = true;
		if (NextValue() && builder.Length > 0)
		{
			result = false;
		}
		pos = num;
		namedPopIndex = num2;
		return result;
	}

	public PopResult Pop(ref int value)
	{
		if (!NextValue())
		{
			return PopResult.EMPTY;
		}
		if (builder.Length == 0)
		{
			return PopResult.EMPTY;
		}
		try
		{
			value = int.Parse(builder.ToString());
		}
		catch
		{
			return PopResult.PARSE_ERROR;
		}
		return PopResult.SUCCESS;
	}

	public PopResult Pop(ref XorInt value)
	{
		int value2 = 0;
		PopResult result = Pop(ref value2);
		value = value2;
		return result;
	}

	public PopResult Pop(ref uint value)
	{
		if (!NextValue())
		{
			return PopResult.EMPTY;
		}
		if (builder.Length == 0)
		{
			return PopResult.EMPTY;
		}
		try
		{
			value = uint.Parse(builder.ToString());
		}
		catch
		{
			return PopResult.PARSE_ERROR;
		}
		return PopResult.SUCCESS;
	}

	public PopResult Pop(ref XorUInt value)
	{
		uint value2 = 0u;
		PopResult result = Pop(ref value2);
		value = value2;
		return result;
	}

	public PopResult Pop(ref short value)
	{
		if (!NextValue())
		{
			return PopResult.EMPTY;
		}
		if (builder.Length == 0)
		{
			return PopResult.EMPTY;
		}
		try
		{
			value = short.Parse(builder.ToString());
		}
		catch
		{
			return PopResult.PARSE_ERROR;
		}
		return PopResult.SUCCESS;
	}

	public PopResult Pop(ref ushort value)
	{
		if (!NextValue())
		{
			return PopResult.EMPTY;
		}
		if (builder.Length == 0)
		{
			return PopResult.EMPTY;
		}
		try
		{
			value = ushort.Parse(builder.ToString());
		}
		catch
		{
			return PopResult.PARSE_ERROR;
		}
		return PopResult.SUCCESS;
	}

	public PopResult Pop(ref char value)
	{
		if (!NextValue())
		{
			return PopResult.EMPTY;
		}
		if (builder.Length == 0)
		{
			return PopResult.EMPTY;
		}
		try
		{
			value = char.Parse(builder.ToString());
		}
		catch
		{
			return PopResult.PARSE_ERROR;
		}
		return PopResult.SUCCESS;
	}

	public PopResult Pop(ref byte value)
	{
		if (!NextValue())
		{
			return PopResult.EMPTY;
		}
		if (builder.Length == 0)
		{
			return PopResult.EMPTY;
		}
		try
		{
			value = byte.Parse(builder.ToString());
		}
		catch
		{
			return PopResult.PARSE_ERROR;
		}
		return PopResult.SUCCESS;
	}

	public PopResult Pop(ref float value)
	{
		if (!NextValue())
		{
			return PopResult.EMPTY;
		}
		if (builder.Length == 0)
		{
			return PopResult.EMPTY;
		}
		try
		{
			value = float.Parse(builder.ToString());
		}
		catch
		{
			return PopResult.PARSE_ERROR;
		}
		return PopResult.SUCCESS;
	}

	public PopResult Pop(ref XorFloat value)
	{
		float value2 = 0f;
		PopResult result = Pop(ref value2);
		value = value2;
		return result;
	}

	public PopResult Pop(ref string value)
	{
		if (!NextValue())
		{
			return PopResult.EMPTY;
		}
		if (builder.Length == 0)
		{
			value = string.Empty;
			return PopResult.EMPTY;
		}
		value = builder.ToString();
		return PopResult.SUCCESS;
	}

	public PopResult Pop(ref bool value)
	{
		if (!NextValue())
		{
			return PopResult.EMPTY;
		}
		if (builder.Length == 0)
		{
			return PopResult.EMPTY;
		}
		string a = builder.ToString();
		if (a != "0" && a != "false")
		{
			value = true;
		}
		else
		{
			value = false;
		}
		return PopResult.SUCCESS;
	}

	public PopResult Pop(ref Vector2 value)
	{
		PopResult a = Pop(ref value.x);
		PopResult a2 = Pop(ref value.y);
		if (a == PopResult.SUCCESS && a2 == PopResult.SUCCESS)
		{
			return PopResult.SUCCESS;
		}
		if (a == PopResult.EMPTY && a2 == PopResult.EMPTY)
		{
			return PopResult.EMPTY;
		}
		return PopResult.UNKNOWN;
	}

	public PopResult Pop(ref Vector3 value)
	{
		PopResult a = Pop(ref value.x);
		PopResult a2 = Pop(ref value.y);
		PopResult a3 = Pop(ref value.z);
		if (a == PopResult.SUCCESS && a2 == PopResult.SUCCESS && a3 == PopResult.SUCCESS)
		{
			return PopResult.SUCCESS;
		}
		if (a == PopResult.EMPTY && a2 == PopResult.EMPTY && a3 == PopResult.EMPTY)
		{
			return PopResult.EMPTY;
		}
		return PopResult.UNKNOWN;
	}

	public PopResult Pop(ref Vector4 value)
	{
		PopResult a = Pop(ref value.x);
		PopResult a2 = Pop(ref value.y);
		PopResult a3 = Pop(ref value.z);
		PopResult a4 = Pop(ref value.w);
		if (a == PopResult.SUCCESS && a2 == PopResult.SUCCESS && a3 == PopResult.SUCCESS && a4 == PopResult.SUCCESS)
		{
			return PopResult.SUCCESS;
		}
		if (a == PopResult.EMPTY && a2 == PopResult.EMPTY && a3 == PopResult.EMPTY && a4 == PopResult.EMPTY)
		{
			return PopResult.EMPTY;
		}
		return PopResult.UNKNOWN;
	}

	public PopResult Pop<T>(ref T value)
	{
		if (!NextValue())
		{
			return PopResult.EMPTY;
		}
		if (builder.Length == 0)
		{
			return PopResult.EMPTY;
		}
		try
		{
			if (!Enum.IsDefined(typeof(T), builder.ToString()))
			{
				return PopResult.DONT_DEFINE;
			}
			value = (T)Enum.Parse(typeof(T), builder.ToString());
		}
		catch
		{
			return PopResult.DONT_DEFINE;
		}
		return PopResult.SUCCESS;
	}

	public PopResult PopEnum<T>(ref T value, T defValue)
	{
		if (!NextValue())
		{
			value = defValue;
			return PopResult.EMPTY;
		}
		if (builder.Length == 0)
		{
			value = defValue;
			return PopResult.EMPTY;
		}
		try
		{
			if (!Enum.IsDefined(typeof(T), builder.ToString()))
			{
				value = defValue;
				return PopResult.DONT_DEFINE;
			}
			value = (T)Enum.Parse(typeof(T), builder.ToString());
		}
		catch
		{
			value = defValue;
			return PopResult.UNKNOWN;
		}
		return PopResult.SUCCESS;
	}

	public PopResult PopColor(ref Vector3 value)
	{
		int value2 = 255;
		int value3 = 255;
		int value4 = 255;
		PopResult a = Pop(ref value2);
		PopResult a2 = Pop(ref value3);
		PopResult a3 = Pop(ref value4);
		if (a == PopResult.SUCCESS && a2 == PopResult.SUCCESS && a3 == PopResult.SUCCESS)
		{
			value.x = (float)value2 / 255f;
			value.y = (float)value3 / 255f;
			value.z = (float)value4 / 255f;
			return PopResult.SUCCESS;
		}
		if (a == PopResult.EMPTY && a2 == PopResult.EMPTY && a3 == PopResult.EMPTY)
		{
			return PopResult.EMPTY;
		}
		return PopResult.UNKNOWN;
	}

	public PopResult PopColor24(ref Color32 value)
	{
		value._002Ector(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		PopResult a = Pop(ref value.r);
		PopResult a2 = Pop(ref value.g);
		PopResult a3 = Pop(ref value.b);
		if (a == PopResult.SUCCESS && a2 == PopResult.SUCCESS && a3 == PopResult.SUCCESS)
		{
			return PopResult.SUCCESS;
		}
		if (a == PopResult.EMPTY && a2 == PopResult.EMPTY && a3 == PopResult.EMPTY)
		{
			return PopResult.EMPTY;
		}
		return PopResult.UNKNOWN;
	}

	public PopResult PopColor24(ref int value)
	{
		PopResult result = PopResult.SUCCESS;
		int value2 = 255;
		int value3 = 255;
		int value4 = 255;
		if (!(bool)Pop(ref value2))
		{
			result = PopResult.UNKNOWN;
		}
		if (!(bool)Pop(ref value3))
		{
			result = PopResult.UNKNOWN;
		}
		if (!(bool)Pop(ref value4))
		{
			result = PopResult.UNKNOWN;
		}
		value = (((value2 & 0xFF) << 24) | ((value3 & 0xFF) << 16) | ((value4 & 0xFF) << 8) | 0xFF);
		return result;
	}

	public int GetPosition()
	{
		return pos;
	}
}
