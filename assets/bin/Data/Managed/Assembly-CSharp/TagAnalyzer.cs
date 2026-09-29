// Decompiled with JetBrains decompiler
// Type: TagAnalyzer
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Text;

#nullable disable
internal class TagAnalyzer
{
  private int line;
  private int column;

  public string findTag { get; private set; }

  public string findTagText { get; private set; }

  protected virtual bool IsValidTag(string tag, int line, int column)
  {
    if (tag == "M" || tag == "F")
      return true;
    Log.Warning(LOG.OUTGAME, $"Invalid tag : {tag} :: line {(object) (line + 1)} : column {(object) column}");
    return false;
  }

  public bool IsFindTag()
  {
    return !string.IsNullOrEmpty(this.findTag) && !string.IsNullOrEmpty(this.findTagText);
  }

  public int Analyze(string text, int _line, int _column)
  {
    this.findTag = string.Empty;
    this.findTagText = string.Empty;
    this.line = _line;
    this.column = _column;
    int num1 = (int) text[this.column];
    int length = text.Length;
    int num2 = this.column;
    if (num1 == 60 && !this.IsFindTag())
    {
      char minValue1 = char.MinValue;
      int num3 = this.column + 1;
      StringBuilder stringBuilder1 = new StringBuilder();
      while (minValue1 != '>' && num3 < length)
      {
        minValue1 = text[num3++];
        if (minValue1 != '>')
          stringBuilder1.Append(minValue1);
      }
      if (this.IsValidTag(stringBuilder1.ToString(), this.line, this.column))
      {
        this.findTag = stringBuilder1.ToString();
        StringBuilder stringBuilder2 = new StringBuilder();
        StringBuilder stringBuilder3 = new StringBuilder();
        bool flag = false;
        while (!flag && num3 < length)
        {
          char ch = text[num3++];
          if (ch == '<')
          {
            int num4 = num3;
            if (num4 < length)
            {
              string str = text;
              int index = num4;
              int num5 = index + 1;
              if (str[index] == '/')
              {
                char minValue2 = char.MinValue;
                while (minValue2 != '>' && num5 < length)
                {
                  minValue2 = text[num5++];
                  if (minValue2 != '>')
                    stringBuilder3.Append(minValue2);
                }
                if (stringBuilder3.ToString() == this.findTag)
                {
                  flag = true;
                  num3 = num5;
                }
                else
                  Log.Error(LOG.OUTGAME, $"not match END_TAG : start = {this.findTag} : end = {stringBuilder3.ToString()} :: line {(object) (this.line + 1)} : column {(object) this.column}");
              }
            }
          }
          else
            stringBuilder2.Append(ch);
        }
        if (flag)
        {
          this.findTagText = stringBuilder2.ToString();
          num2 = num3;
        }
        else if (num3 >= length)
        {
          Log.Error(LOG.OUTGAME, $"did not end Tag Analyze till the end of line : tag = {this.findTag} : text = {this.findTagText} :: line {(object) (this.line + 1)}");
          this.findTag = string.Empty;
          this.findTagText = string.Empty;
        }
      }
    }
    return num2;
  }
}
