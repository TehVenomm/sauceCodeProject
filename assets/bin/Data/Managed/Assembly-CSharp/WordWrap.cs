// Decompiled with JetBrains decompiler
// Type: WordWrap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public static class WordWrap
{
  private static readonly string SuppressionHead = "!%),.:;?]}¢°’”‰′″℃、。々〉》」』】〕ぁぃぅぇぉっゃゅょゎ゛゜ゝゞァィゥェォッャュョヮヵヶ・ーヽヾ！％），．：；？］｝｡｣､･ｧｨｩｪｫｬｭｮｯｰﾞﾟ￠！？";
  private static readonly string SuppressionTail = "$([\\{£¥‘“〈《「『【〔＄（［｛｢￡￥";

  public static string Convert(UILabel label, string orgText)
  {
    string text = orgText;
    string final = "";
    if (!label.Wrap(text, out final))
      final = label.text;
    if (final.Equals(text))
      return text;
    string str1 = text.Replace("\r\n", "\n");
    bool flag = true;
    while (flag)
    {
      flag = false;
      string str2 = "";
      string[] strArray = str1.Split('\n');
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (0 < index)
          str2 += "\n";
        string orgText1 = strArray[index];
        string str3 = WordWrap.ConvertWrap(label, orgText1);
        str2 += str3;
      }
      if (!str2.Equals(str1))
      {
        flag = true;
        str1 = str2;
      }
    }
    return str1;
  }

  private static string ConvertWrap(UILabel label, string orgText)
  {
    string final = "";
    if (!label.Wrap(orgText, out final))
      final = orgText;
    if (orgText.Equals(final))
      return final;
    string str1 = "";
    string[] strArray = final.Replace("\n", "\n ").Split('\n');
    for (int index1 = 0; index1 < strArray.Length; ++index1)
    {
      string str2 = strArray[index1];
      if (str2.Length != 0)
      {
        string str3 = str2[0].ToString();
        if (str1.Length > 0 && WordWrap.SuppressionHead.IndexOf(str3) >= 0)
        {
          str1 = str1.Insert(str1.Length - 1, "\n");
          for (int index2 = index1; index2 < strArray.Length; ++index2)
            str1 += strArray[index2];
          break;
        }
        string str4 = str2[str2.Length - 1].ToString();
        if (WordWrap.SuppressionTail.IndexOf(str4) >= 0)
        {
          string str5 = str2.Insert(str2.Length - 1, "\n");
          str1 += str5;
          for (int index3 = index1 + 1; index3 < strArray.Length; ++index3)
            str1 += strArray[index3];
          break;
        }
        str1 += str2;
      }
    }
    return str1;
  }
}
