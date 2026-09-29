// Decompiled with JetBrains decompiler
// Type: FlashCompatibleConvert
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class FlashCompatibleConvert : MonoBehaviour
{
  public static int ToInt32(string s)
  {
    switch (s)
    {
      case null:
        throw new Exception("FlashCompatibleConvert.ToInt32 was passed a null string as argument");
      case "":
        throw new Exception("FlashCompatibleConvert.ToInt32 was passed an empty string as argument");
      default:
        bool flag = s[0] == '-';
        int num1 = flag ? 1 : 0;
        double num2 = 0.0;
        for (int index = num1; index < s.Length; ++index)
        {
          int int32 = FlashCompatibleConvert.CharToInt32(s[index]);
          if (int32 == -1)
            throw new Exception("FlashCompatibleConvert.ToInt32 was passed a wrong argument: " + s);
          num2 += (double) int32 * Math.Pow(10.0, (double) (s.Length - index - 1));
        }
        return (int) (num2 * (flag ? -1.0 : 1.0));
    }
  }

  private static int CharToInt32(char s)
  {
    switch (s)
    {
      case ',':
        return -2;
      case '.':
        return -2;
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
      case 'E':
        return -3;
      case 'e':
        return -3;
      default:
        return -1;
    }
  }

  public static bool ToBoolean(string s)
  {
    switch (s)
    {
      case null:
        throw new Exception("FlashCompatibleConvert.ToBoolean was passed a null string as argument");
      case "":
        throw new Exception("FlashCompatibleConvert.ToBoolean was passed an empty string as argument");
      default:
        if (s.ToLower() == "true")
          return true;
        if (s.ToLower() == "false")
          return false;
        throw new Exception("FlashCompatibleConvert.ToBoolean was passed a wrong argument: " + s);
    }
  }

  public static double ToDouble(string s)
  {
    switch (s)
    {
      case null:
        throw new Exception("FlashCompatibleConvert.ToDouble was passed a null string as argument");
      case "":
        throw new Exception("FlashCompatibleConvert.ToDouble was passed an empty string as argument");
      default:
        int num1 = -1;
        for (int index = 0; index < s.Length; ++index)
        {
          if (FlashCompatibleConvert.CharToInt32(s[index]) == -2)
          {
            num1 = index;
            break;
          }
        }
        int num2 = s.Length;
        int num3 = -1;
        for (int index = 0; index < s.Length; ++index)
        {
          if (FlashCompatibleConvert.CharToInt32(s[index]) == -3)
          {
            num3 = index;
            num2 = index;
            break;
          }
        }
        bool flag = s[0] == '-';
        int num4 = flag ? 1 : 0;
        int num5 = s.Length;
        if (num1 != -1)
          num5 = num1;
        double num6 = 0.0;
        for (int index = num4; index < num2; ++index)
        {
          if (index != num1)
          {
            int int32 = FlashCompatibleConvert.CharToInt32(s[index]);
            if (int32 == -1)
              throw new Exception("FlashCompatibleConvert.ToDouble was passed a wrong argument: " + s);
            if (num1 != -1 && index > num1)
              num6 += (double) int32 * Math.Pow(0.1, (double) (index - num1));
            else
              num6 += (double) int32 * Math.Pow(10.0, (double) (num5 - index - 1));
          }
        }
        if (num3 != -1)
        {
          double y = FlashCompatibleConvert.ToDouble(s.Substring(num3 + 1));
          num6 *= Math.Pow(10.0, y);
        }
        return num6 * (flag ? -1.0 : 1.0);
    }
  }

  public static bool IsDigit(char c)
  {
    return c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9';
  }
}
