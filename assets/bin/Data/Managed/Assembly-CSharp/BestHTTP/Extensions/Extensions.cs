// Decompiled with JetBrains decompiler
// Type: BestHTTP.Extensions.Extensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

#nullable disable
namespace BestHTTP.Extensions;

public static class Extensions
{
  public static string AsciiToString(this byte[] bytes)
  {
    StringBuilder stringBuilder = new StringBuilder(bytes.Length);
    foreach (byte num in bytes)
      stringBuilder.Append(num <= (byte) 127 /*0x7F*/ ? (char) num : '?');
    return stringBuilder.ToString();
  }

  public static byte[] GetASCIIBytes(this string str)
  {
    byte[] asciiBytes = new byte[str.Length];
    for (int index = 0; index < str.Length; ++index)
    {
      char ch = str[index];
      asciiBytes[index] = ch < '\u0080' ? (byte) ch : (byte) 63 /*0x3F*/;
    }
    return asciiBytes;
  }

  public static void WriteLine(this FileStream fs) => fs.Write(HTTPRequest.EOL, 0, 2);

  public static void WriteLine(this FileStream fs, string line)
  {
    byte[] asciiBytes = line.GetASCIIBytes();
    fs.Write(asciiBytes, 0, asciiBytes.Length);
    fs.WriteLine();
  }

  public static void WriteLine(this FileStream fs, string format, params object[] values)
  {
    byte[] asciiBytes = string.Format(format, values).GetASCIIBytes();
    fs.Write(asciiBytes, 0, asciiBytes.Length);
    fs.WriteLine();
  }

  public static string[] FindOption(this string str, string option)
  {
    string[] strArray = str.ToLower().Split(new char[1]
    {
      ','
    }, StringSplitOptions.RemoveEmptyEntries);
    option = option.ToLower();
    for (int index = 0; index < strArray.Length; ++index)
    {
      if (strArray[index].Contains(option))
        return strArray[index].Split(new char[1]{ '=' }, StringSplitOptions.RemoveEmptyEntries);
    }
    return (string[]) null;
  }

  public static int ToInt32(this string str, int defaultValue = 0)
  {
    if (str == null)
      return defaultValue;
    try
    {
      return int.Parse(str);
    }
    catch
    {
      return defaultValue;
    }
  }

  public static long ToInt64(this string str, long defaultValue = 0)
  {
    if (str == null)
      return defaultValue;
    try
    {
      return long.Parse(str);
    }
    catch
    {
      return defaultValue;
    }
  }

  public static DateTime ToDateTime(this string str, DateTime defaultValue = default (DateTime))
  {
    if (str == null)
      return defaultValue;
    try
    {
      DateTime.TryParse(str, out defaultValue);
      return defaultValue.ToUniversalTime();
    }
    catch
    {
      return defaultValue;
    }
  }

  public static string ToStrOrEmpty(this string str) => str == null ? string.Empty : str;

  public static string CalculateMD5Hash(this string input)
  {
    return input.GetASCIIBytes().CalculateMD5Hash();
  }

  public static string CalculateMD5Hash(this byte[] input)
  {
    byte[] hash = MD5.Create().ComputeHash(input);
    StringBuilder stringBuilder = new StringBuilder();
    foreach (byte num in hash)
      stringBuilder.Append(num.ToString("x2"));
    return stringBuilder.ToString();
  }

  internal static string Read(this string str, ref int pos, char block, bool needResult = true)
  {
    return str.Read(ref pos, (Func<char, bool>) (ch => (int) ch != (int) block), needResult);
  }

  internal static string Read(
    this string str,
    ref int pos,
    Func<char, bool> block,
    bool needResult = true)
  {
    if (pos >= str.Length)
      return string.Empty;
    str.SkipWhiteSpace(ref pos);
    int startIndex = pos;
    while (pos < str.Length && block(str[pos]))
      ++pos;
    string str1 = needResult ? str.Substring(startIndex, pos - startIndex) : (string) null;
    ++pos;
    return str1;
  }

  internal static string ReadQuotedText(this string str, ref int pos)
  {
    string empty = string.Empty;
    if (str == null)
      return empty;
    string str1;
    if (str[pos] == '"')
    {
      str.Read(ref pos, '"', false);
      str1 = str.Read(ref pos, '"');
      str.Read(ref pos, ',', false);
    }
    else
      str1 = str.Read(ref pos, ',');
    return str1;
  }

  internal static void SkipWhiteSpace(this string str, ref int pos)
  {
    if (pos >= str.Length)
      return;
    while (pos < str.Length && char.IsWhiteSpace(str[pos]))
      ++pos;
  }

  internal static string TrimAndLower(this string str)
  {
    if (str == null)
      return (string) null;
    char[] chArray = new char[str.Length];
    int length = 0;
    for (int index = 0; index < str.Length; ++index)
    {
      char c = str[index];
      if (!char.IsWhiteSpace(c) && !char.IsControl(c))
        chArray[length++] = char.ToLowerInvariant(c);
    }
    return new string(chArray, 0, length);
  }

  internal static List<KeyValuePair> ParseOptionalHeader(this string str)
  {
    List<KeyValuePair> optionalHeader = new List<KeyValuePair>();
    if (str == null)
      return optionalHeader;
    int pos = 0;
    while (pos < str.Length)
    {
      KeyValuePair keyValuePair = new KeyValuePair(str.Read(ref pos, (Func<char, bool>) (ch => ch != '=' && ch != ',')).TrimAndLower());
      if (str[pos - 1] == '=')
        keyValuePair.Value = str.ReadQuotedText(ref pos);
      optionalHeader.Add(keyValuePair);
    }
    return optionalHeader;
  }

  internal static List<KeyValuePair> ParseQualityParams(this string str)
  {
    List<KeyValuePair> qualityParams = new List<KeyValuePair>();
    if (str == null)
      return qualityParams;
    int pos = 0;
    while (pos < str.Length)
    {
      KeyValuePair keyValuePair = new KeyValuePair(str.Read(ref pos, (Func<char, bool>) (ch => ch != ',' && ch != ';')).TrimAndLower());
      if (str[pos - 1] == ';')
      {
        str.Read(ref pos, '=', false);
        keyValuePair.Value = str.Read(ref pos, ',');
      }
      qualityParams.Add(keyValuePair);
    }
    return qualityParams;
  }
}
