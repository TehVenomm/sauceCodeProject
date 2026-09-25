// Decompiled with JetBrains decompiler
// Type: EscapeString
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Text;

#nullable disable
public class EscapeString
{
  private static string[] conversions = new string[133]
  {
    "%20",
    "%22",
    "%27",
    "%3C",
    "%3E",
    "%23",
    "%25",
    "%7B",
    "%7D",
    "%7C",
    "%5E",
    "%7E",
    "%5B",
    "%5D",
    "%24",
    "%26",
    "%2B",
    "%2C",
    "%2F",
    "%3A",
    "%3B",
    "%3D",
    "%3F",
    "%40",
    "%5F",
    "%0A",
    "%2E",
    "%09",
    "%C2%A1",
    "%C2%A2",
    "%C2%A3",
    "%C2%A4",
    "%C2%A5",
    "%C2%A6",
    "%C2%A7",
    "%C2%A8",
    "%C2%A9",
    "%C2%AA",
    "%C2%AB",
    "%C2%AC",
    "%C2%AD",
    "%C2%AE",
    "%C2%AF",
    "%C2%B0",
    "%C2%B1",
    "%C2%B2",
    "%C2%B3",
    "%C2%B4",
    "%C2%B5",
    "%C2%B6",
    "%C2%B7",
    "%C2%B8",
    "%C2%B9",
    "%C2%BA",
    "%C2%BB",
    "%C2%BC",
    "%C2%BD",
    "%C2%BE",
    "%C2%BF",
    "%C3%80",
    "%C3%81",
    "%C3%82",
    "%C3%83",
    "%C3%84",
    "%C3%85",
    "%C3%86",
    "%C3%87",
    "%C3%88",
    "%C3%89",
    "%C3%8A",
    "%C3%8B",
    "%C3%8C",
    "%C3%8D",
    "%C3%8E",
    "%C3%8F",
    "%C3%90",
    "%C3%91",
    "%C3%92",
    "%C3%93",
    "%C3%94",
    "%C3%95",
    "%C3%96",
    "%C3%97",
    "%C3%98",
    "%C3%99",
    "%C3%9A",
    "%C3%9B",
    "%C3%9C",
    "%C3%9D",
    "%C3%9E",
    "%C3%9F",
    "%C3%A0",
    "%C3%A1",
    "%C3%A2",
    "%C3%A3",
    "%C3%A4",
    "%C3%A5",
    "%C3%A6",
    "%C3%A7",
    "%C3%A8",
    "%C3%A9",
    "%C3%AA",
    "%C3%AB",
    "%C3%AC",
    "%C3%AD",
    "%C3%AE",
    "%C3%AF",
    "%C3%B0",
    "%C3%B1",
    "%C3%B2",
    "%C3%B3",
    "%C3%B4",
    "%C3%B5",
    "%C3%B6",
    "%C3%B7",
    "%C3%B8",
    "%C3%B9",
    "%C3%BA",
    "%C3%BB",
    "%C3%BC",
    "%C3%BD",
    "%C3%BE",
    "%C3%BF",
    "%2D",
    "%2A",
    "%E2%82%AC",
    "%60",
    "%21",
    "%28",
    "%29",
    "%5C",
    "%E2%80%99",
    "%E2%80%A6"
  };
  private static char[] symbols = new char[133]
  {
    ' ',
    '"',
    '\'',
    '<',
    '>',
    '#',
    '%',
    '{',
    '}',
    '|',
    '^',
    '~',
    '[',
    ']',
    '$',
    '&',
    '+',
    ',',
    '/',
    ':',
    ';',
    '=',
    '?',
    '@',
    '_',
    '\n',
    '.',
    ' ',
    '¡',
    '¢',
    '£',
    '¤',
    '¥',
    '¦',
    '§',
    '¨',
    '©',
    'ª',
    '«',
    '¬',
    '\u00AD',
    '®',
    '¯',
    '°',
    '±',
    '\u00B2',
    '\u00B3',
    '´',
    'µ',
    '¶',
    '·',
    '¸',
    '\u00B9',
    'º',
    '»',
    '\u00BC',
    '\u00BD',
    '\u00BE',
    '¿',
    'À',
    'Á',
    'Â',
    'Ã',
    'Ä',
    'Å',
    'Æ',
    'Ç',
    'È',
    'É',
    'Ê',
    'Ë',
    'Ì',
    'Í',
    'Î',
    'Ï',
    'Ð',
    'Ñ',
    'Ò',
    'Ó',
    'Ô',
    'Õ',
    'Ö',
    '×',
    'Ø',
    'Ù',
    'Ú',
    'Û',
    'Ü',
    'Ý',
    'Þ',
    'ß',
    'à',
    'á',
    'â',
    'ã',
    'ä',
    'å',
    'æ',
    'ç',
    'è',
    'é',
    'ê',
    'ë',
    'ì',
    'í',
    'î',
    'ï',
    'ð',
    'ñ',
    'ò',
    'ó',
    'ô',
    'õ',
    'ö',
    '÷',
    'ø',
    'ù',
    'ú',
    'û',
    'ü',
    'ý',
    'þ',
    'ÿ',
    '-',
    '*',
    '€',
    '`',
    '!',
    '(',
    ')',
    '\\',
    '\'',
    '…'
  };

  public static string Escape(string input)
  {
    StringBuilder stringBuilder = new StringBuilder("");
    for (int index1 = 0; index1 < input.Length; ++index1)
    {
      bool flag = false;
      for (int index2 = 0; index2 < EscapeString.conversions.Length; ++index2)
      {
        if ((int) input[index1] == (int) EscapeString.symbols[index2])
        {
          stringBuilder.Append(EscapeString.conversions[index2]);
          flag = true;
          break;
        }
      }
      if (!flag)
        stringBuilder.Append(input[index1]);
    }
    return stringBuilder.ToString();
  }

  public static string Unescape(string input)
  {
    StringBuilder stringBuilder1 = new StringBuilder("");
    char[] charArray = input.ToCharArray();
    int index1 = 0;
    while (index1 < charArray.Length)
    {
      if (charArray[index1] == '%')
      {
        StringBuilder stringBuilder2 = new StringBuilder("");
        StringBuilder stringBuilder3 = stringBuilder2;
        char[] chArray1 = charArray;
        int index2 = index1;
        int num1 = index2 + 1;
        int num2 = (int) chArray1[index2];
        stringBuilder3.Append((char) num2);
        StringBuilder stringBuilder4 = stringBuilder2;
        char[] chArray2 = charArray;
        int index3 = num1;
        int num3 = index3 + 1;
        int num4 = (int) chArray2[index3];
        stringBuilder4.Append((char) num4);
        StringBuilder stringBuilder5 = stringBuilder2;
        char[] chArray3 = charArray;
        int index4 = num3;
        index1 = index4 + 1;
        int num5 = (int) chArray3[index4];
        stringBuilder5.Append((char) num5);
        string str = stringBuilder2.ToString();
        switch (str)
        {
          case "%C2":
          case "%C3":
            StringBuilder stringBuilder6 = stringBuilder2;
            char[] chArray4 = charArray;
            int index5 = index1;
            int num6 = index5 + 1;
            int num7 = (int) chArray4[index5];
            stringBuilder6.Append((char) num7);
            StringBuilder stringBuilder7 = stringBuilder2;
            char[] chArray5 = charArray;
            int index6 = num6;
            int num8 = index6 + 1;
            int num9 = (int) chArray5[index6];
            stringBuilder7.Append((char) num9);
            StringBuilder stringBuilder8 = stringBuilder2;
            char[] chArray6 = charArray;
            int index7 = num8;
            index1 = index7 + 1;
            int num10 = (int) chArray6[index7];
            stringBuilder8.Append((char) num10);
            str = stringBuilder2.ToString();
            break;
          case "%E2":
            StringBuilder stringBuilder9 = stringBuilder2;
            char[] chArray7 = charArray;
            int index8 = index1;
            int num11 = index8 + 1;
            int num12 = (int) chArray7[index8];
            stringBuilder9.Append((char) num12);
            StringBuilder stringBuilder10 = stringBuilder2;
            char[] chArray8 = charArray;
            int index9 = num11;
            int num13 = index9 + 1;
            int num14 = (int) chArray8[index9];
            stringBuilder10.Append((char) num14);
            StringBuilder stringBuilder11 = stringBuilder2;
            char[] chArray9 = charArray;
            int index10 = num13;
            int num15 = index10 + 1;
            int num16 = (int) chArray9[index10];
            stringBuilder11.Append((char) num16);
            StringBuilder stringBuilder12 = stringBuilder2;
            char[] chArray10 = charArray;
            int index11 = num15;
            int num17 = index11 + 1;
            int num18 = (int) chArray10[index11];
            stringBuilder12.Append((char) num18);
            StringBuilder stringBuilder13 = stringBuilder2;
            char[] chArray11 = charArray;
            int index12 = num17;
            int num19 = index12 + 1;
            int num20 = (int) chArray11[index12];
            stringBuilder13.Append((char) num20);
            StringBuilder stringBuilder14 = stringBuilder2;
            char[] chArray12 = charArray;
            int index13 = num19;
            index1 = index13 + 1;
            int num21 = (int) chArray12[index13];
            stringBuilder14.Append((char) num21);
            str = stringBuilder2.ToString();
            break;
        }
        bool flag = false;
        for (int index14 = 0; index14 < EscapeString.conversions.Length; ++index14)
        {
          if (EscapeString.conversions[index14] == str)
          {
            stringBuilder1.Append(EscapeString.symbols[index14]);
            flag = true;
            break;
          }
        }
        if (!flag)
          stringBuilder1.Append(str);
      }
      else
        stringBuilder1.Append(charArray[index1++]);
    }
    return stringBuilder1.ToString();
  }
}
