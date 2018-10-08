using System;
using UnityEngine;

public class FlashCompatibleConvert : MonoBehaviour
{
	public static int ToInt32(string s)
	{
		if (s == null)
		{
			throw new Exception("FlashCompatibleConvert.ToInt32 was passed a null string as argument");
		}
		if (s.Length == 0)
		{
			throw new Exception("FlashCompatibleConvert.ToInt32 was passed an empty string as argument");
		}
		bool flag = s[0] == '-';
		int num = flag ? 1 : 0;
		double num2 = 0.0;
		for (int i = num; i < s.Length; i++)
		{
			int num3 = CharToInt32(s[i]);
			if (num3 == -1)
			{
				throw new Exception("FlashCompatibleConvert.ToInt32 was passed a wrong argument: " + s);
			}
			num2 += (double)num3 * Math.Pow(10.0, (double)(s.Length - i - 1));
		}
		return (int)(num2 * (double)((!flag) ? 1 : (-1)));
	}

	private static int CharToInt32(char s)
	{
		switch (s)
		{
		case '0':
			return 0;
		case '1':
			return 1;
		case '2':
			return 2;
		case '3':
			return 3;
		case '4':
			return 4;
		case '5':
			return 5;
		case '6':
			return 6;
		case '7':
			return 7;
		case '8':
			return 8;
		case '9':
			return 9;
		case '.':
			return -2;
		case ',':
			return -2;
		case 'e':
			return -3;
		case 'E':
			return -3;
		default:
			return -1;
		}
	}

	public static bool ToBoolean(string s)
	{
		if (s == null)
		{
			throw new Exception("FlashCompatibleConvert.ToBoolean was passed a null string as argument");
		}
		if (s.Length == 0)
		{
			throw new Exception("FlashCompatibleConvert.ToBoolean was passed an empty string as argument");
		}
		if (s.ToLower() == "true")
		{
			return true;
		}
		if (s.ToLower() == "false")
		{
			return false;
		}
		throw new Exception("FlashCompatibleConvert.ToBoolean was passed a wrong argument: " + s);
	}

	public static double ToDouble(string s)
	{
		if (s == null)
		{
			throw new Exception("FlashCompatibleConvert.ToDouble was passed a null string as argument");
		}
		if (s.Length == 0)
		{
			throw new Exception("FlashCompatibleConvert.ToDouble was passed an empty string as argument");
		}
		int num = -1;
		for (int i = 0; i < s.Length; i++)
		{
			if (CharToInt32(s[i]) == -2)
			{
				num = i;
				break;
			}
		}
		int num2 = s.Length;
		int num3 = -1;
		for (int j = 0; j < s.Length; j++)
		{
			if (CharToInt32(s[j]) == -3)
			{
				num3 = j;
				num2 = j;
				break;
			}
		}
		bool flag = s[0] == '-';
		int num4 = flag ? 1 : 0;
		int num5 = s.Length;
		if (num != -1)
		{
			num5 = num;
		}
		double num6 = 0.0;
		for (int k = num4; k < num2; k++)
		{
			if (k != num)
			{
				int num7 = CharToInt32(s[k]);
				if (num7 == -1)
				{
					throw new Exception("FlashCompatibleConvert.ToDouble was passed a wrong argument: " + s);
				}
				num6 = ((num == -1 || k <= num) ? (num6 + (double)num7 * Math.Pow(10.0, (double)(num5 - k - 1))) : (num6 + (double)num7 * Math.Pow(0.1, (double)(k - num))));
			}
		}
		if (num3 != -1)
		{
			double y = ToDouble(s.Substring(num3 + 1));
			num6 *= Math.Pow(10.0, y);
		}
		return num6 * (double)((!flag) ? 1 : (-1));
	}

	public static bool IsDigit(char c)
	{
		return c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9';
	}
}
