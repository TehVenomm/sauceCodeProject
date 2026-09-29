// Decompiled with JetBrains decompiler
// Type: JsonEscape
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Text;

#nullable disable
public static class JsonEscape
{
  public static string escape(string s)
  {
    if (s == null || s.Length == 0)
      return "";
    int length = s.Length;
    StringBuilder stringBuilder = new StringBuilder(length + 4);
    for (int index = 0; index < length; ++index)
    {
      char ch = s[index];
      switch (ch)
      {
        case '\b':
          stringBuilder.Append("\\b");
          break;
        case '\t':
          stringBuilder.Append("\\t");
          break;
        case '\n':
          stringBuilder.Append("\\n");
          break;
        case '\f':
          stringBuilder.Append("\\f");
          break;
        case '\r':
          stringBuilder.Append("\\r");
          break;
        case '"':
        case '\\':
          stringBuilder.Append('\\');
          stringBuilder.Append(ch);
          break;
        case '/':
          stringBuilder.Append('\\');
          stringBuilder.Append(ch);
          break;
        default:
          if (ch > '\u007F')
          {
            string str = "\\u" + ((int) ch).ToString("x4");
            stringBuilder.Append(str);
            break;
          }
          stringBuilder.Append(ch);
          break;
      }
    }
    return stringBuilder.ToString();
  }

  public static string removeSpecialChars(string s)
  {
    if (s == null || s.Length == 0)
      return "";
    int length = s.Length;
    StringBuilder stringBuilder = new StringBuilder(length);
    for (int index = 0; index < length; ++index)
    {
      char ch = s[index];
      switch (ch)
      {
        case '\b':
        case '\t':
        case '\n':
        case '\f':
        case '\r':
          stringBuilder.Append(" ");
          continue;
        case '"':
        case '\'':
        case '\\':
          continue;
        default:
          stringBuilder.Append(ch);
          continue;
      }
    }
    return stringBuilder.ToString();
  }
}
