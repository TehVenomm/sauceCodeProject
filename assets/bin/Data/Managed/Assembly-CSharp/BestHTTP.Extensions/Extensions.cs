using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BestHTTP.Extensions
{
	public static class Extensions
	{
		public static string AsciiToString(this byte[] bytes)
		{
			StringBuilder stringBuilder = new StringBuilder(bytes.Length);
			foreach (byte b in bytes)
			{
				stringBuilder.Append((char)((b > 127) ? 63 : ((int)b)));
			}
			return stringBuilder.ToString();
		}

		public static byte[] GetASCIIBytes(this string str)
		{
			byte[] array = new byte[str.Length];
			for (int i = 0; i < str.Length; i++)
			{
				char c = str[i];
				array[i] = (byte)((c >= '\u0080') ? '?' : c);
			}
			return array;
		}

		public static void WriteLine(this FileStream fs)
		{
			fs.Write(HTTPRequest.EOL, 0, 2);
		}

		public static void WriteLine(this FileStream fs, string line)
		{
			byte[] aSCIIBytes = line.GetASCIIBytes();
			fs.Write(aSCIIBytes, 0, aSCIIBytes.Length);
			fs.WriteLine();
		}

		public static void WriteLine(this FileStream fs, string format, params object[] values)
		{
			byte[] aSCIIBytes = string.Format(format, values).GetASCIIBytes();
			fs.Write(aSCIIBytes, 0, aSCIIBytes.Length);
			fs.WriteLine();
		}

		public static string[] FindOption(this string str, string option)
		{
			string[] array = str.ToLower().Split(new char[1]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			option = option.ToLower();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Contains(option))
				{
					return array[i].Split(new char[1]
					{
						'='
					}, StringSplitOptions.RemoveEmptyEntries);
				}
			}
			return null;
		}

		public static int ToInt32(this string str, int defaultValue = 0)
		{
			if (str != null)
			{
				try
				{
					return int.Parse(str);
					IL_0014:
					int result;
					return result;
				}
				catch
				{
					return defaultValue;
					IL_0021:
					int result;
					return result;
				}
			}
			return defaultValue;
		}

		public static long ToInt64(this string str, long defaultValue = 0L)
		{
			if (str != null)
			{
				try
				{
					return long.Parse(str);
					IL_0014:
					long result;
					return result;
				}
				catch
				{
					return defaultValue;
					IL_0021:
					long result;
					return result;
				}
			}
			return defaultValue;
		}

		public static DateTime ToDateTime(this string str, DateTime defaultValue = default(DateTime))
		{
			if (str != null)
			{
				try
				{
					DateTime.TryParse(str, out defaultValue);
					return defaultValue.ToUniversalTime();
					IL_001e:
					DateTime result;
					return result;
				}
				catch
				{
					return defaultValue;
					IL_002b:
					DateTime result;
					return result;
				}
			}
			return defaultValue;
		}

		public static string ToStrOrEmpty(this string str)
		{
			if (str == null)
			{
				return string.Empty;
			}
			return str;
		}

		public static string CalculateMD5Hash(this string input)
		{
			return input.GetASCIIBytes().CalculateMD5Hash();
		}

		public static string CalculateMD5Hash(this byte[] input)
		{
			byte[] array = MD5.Create().ComputeHash(input);
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				byte b = array2[i];
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		internal unsafe static string Read(this string str, ref int pos, char block, bool needResult = true)
		{
			_003CRead_003Ec__AnonStorey7C7 _003CRead_003Ec__AnonStorey7C;
			return str.Read(ref pos, new Func<char, bool>((object)_003CRead_003Ec__AnonStorey7C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), needResult);
		}

		internal static string Read(this string str, ref int pos, Func<char, bool> block, bool needResult = true)
		{
			if (pos >= str.Length)
			{
				return string.Empty;
			}
			str.SkipWhiteSpace(ref pos);
			int num = pos;
			while (pos < str.Length && block.Invoke(str[pos]))
			{
				pos++;
			}
			string result = (!needResult) ? null : str.Substring(num, pos - num);
			pos++;
			return result;
		}

		internal static string ReadQuotedText(this string str, ref int pos)
		{
			string empty = string.Empty;
			if (str == null)
			{
				return empty;
			}
			if (str[pos] == '"')
			{
				str.Read(ref pos, '"', false);
				empty = str.Read(ref pos, '"', true);
				str.Read(ref pos, ',', false);
			}
			else
			{
				empty = str.Read(ref pos, ',', true);
			}
			return empty;
		}

		internal static void SkipWhiteSpace(this string str, ref int pos)
		{
			if (pos < str.Length)
			{
				while (pos < str.Length && char.IsWhiteSpace(str[pos]))
				{
					pos++;
				}
			}
		}

		internal static string TrimAndLower(this string str)
		{
			if (str == null)
			{
				return null;
			}
			char[] array = new char[str.Length];
			int num = 0;
			foreach (char c in str)
			{
				if (!char.IsWhiteSpace(c) && !char.IsControl(c))
				{
					array[num++] = char.ToLowerInvariant(c);
				}
			}
			return new string(array, 0, num);
		}

		internal unsafe static List<KeyValuePair> ParseOptionalHeader(this string str)
		{
			List<KeyValuePair> list = new List<KeyValuePair>();
			if (str == null)
			{
				return list;
			}
			int pos = 0;
			while (pos < str.Length)
			{
				if (_003C_003Ef__am_0024cache0 == null)
				{
					_003C_003Ef__am_0024cache0 = new Func<char, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				string key = str.Read(ref pos, _003C_003Ef__am_0024cache0, true).TrimAndLower();
				KeyValuePair keyValuePair = new KeyValuePair(key);
				if (str[pos - 1] == '=')
				{
					keyValuePair.Value = str.ReadQuotedText(ref pos);
				}
				list.Add(keyValuePair);
			}
			return list;
		}

		internal unsafe static List<KeyValuePair> ParseQualityParams(this string str)
		{
			List<KeyValuePair> list = new List<KeyValuePair>();
			if (str == null)
			{
				return list;
			}
			int pos = 0;
			while (pos < str.Length)
			{
				if (_003C_003Ef__am_0024cache1 == null)
				{
					_003C_003Ef__am_0024cache1 = new Func<char, bool>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				}
				string key = str.Read(ref pos, _003C_003Ef__am_0024cache1, true).TrimAndLower();
				KeyValuePair keyValuePair = new KeyValuePair(key);
				if (str[pos - 1] == ';')
				{
					str.Read(ref pos, '=', false);
					keyValuePair.Value = str.Read(ref pos, ',', true);
				}
				list.Add(keyValuePair);
			}
			return list;
		}
	}
}
