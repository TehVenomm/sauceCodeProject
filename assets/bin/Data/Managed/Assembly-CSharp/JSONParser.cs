// Decompiled with JetBrains decompiler
// Type: JSONParser
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class JSONParser
{
  private FlashCompatibleTextReader reader;

  public JSONNode Parse(FlashCompatibleTextReader reader)
  {
    this.reader = reader;
    return reader.Peek() == -1 ? (JSONNode) null : this.ReadObject();
  }

  private JSONNode ReadObject()
  {
    JSONNode jsonNode = new JSONNode();
    this.SkipWhitespace();
    if (this.reader.Peek() != 123)
    {
      Debug.LogError((object) "malformed json: no starting '{'");
      return (JSONNode) null;
    }
    this.reader.Read();
    while (this.reader.Peek() != 125)
    {
      if (this.reader.Peek() == 44)
        this.reader.Read();
      if (this.reader.Peek() != 125)
      {
        this.SkipWhitespace();
        string fieldName = this.ReadFieldName().Trim();
        this.SkipWhitespace();
        this.reader.Read();
        IJSONFieldValue val = this.ReadValue();
        jsonNode.AddField(fieldName, val);
        this.SkipWhitespace();
      }
      else
        break;
    }
    this.reader.Read();
    return jsonNode;
  }

  private IJSONFieldValue ReadValue()
  {
    this.SkipWhitespace();
    char c = (char) this.reader.Peek();
    switch (c)
    {
      case '"':
      case '\'':
        return (IJSONFieldValue) new JSONStringFieldValue(this.ReadString());
      default:
        if (!FlashCompatibleConvert.IsDigit(c))
        {
          switch (c)
          {
            case '-':
              break;
            case '[':
              return (IJSONFieldValue) new JSONListFieldValue(this.ReadList());
            case 'f':
            case 't':
              return (IJSONFieldValue) new JSONBooleanFieldValue(this.ReadBoolean());
            case 'n':
              this.ReadNull();
              return (IJSONFieldValue) new JSONNullFieldValue();
            case '{':
              return (IJSONFieldValue) new JSONObjectFieldValue(this.ReadObject());
            default:
              return (IJSONFieldValue) null;
          }
        }
        return (IJSONFieldValue) new JSONNumberFieldValue(this.ReadNumber());
    }
  }

  private void SkipWhitespace()
  {
    while (true)
    {
      int c = this.reader.Peek();
      if (c != -1 && char.IsWhiteSpace((char) c))
        this.reader.Read();
      else
        break;
    }
  }

  private string ReadFieldName()
  {
    this.SkipWhitespace();
    string str = "";
    char ch = (char) this.reader.Peek();
    bool flag = ch == '\'' || ch == '"';
    this.SkipWhitespace();
    while (true)
    {
      switch (ch)
      {
        case ':':
          goto label_6;
        case '}':
          goto label_1;
        default:
          str += ((char) this.reader.Read()).ToString();
          this.SkipWhitespace();
          ch = (char) this.reader.Peek();
          continue;
      }
    }
label_1:
    if (str == "")
      return "";
    Debug.LogError((object) "malformed json: read '}' before reading ':'");
    return (string) null;
label_6:
    if (flag && (str.EndsWith("'") || str.EndsWith("\"")))
      str = str.Substring(1, str.Length - 2);
    return str;
  }

  private double ReadNumber()
  {
    string s = "";
    while (true)
    {
      int c = this.reader.Peek();
      switch (c)
      {
        case -1:
        case 44:
        case 93:
        case 125:
          goto label_3;
        default:
          if (!char.IsWhiteSpace((char) c))
          {
            s += ((char) this.reader.Read()).ToString();
            continue;
          }
          goto label_3;
      }
    }
label_3:
    return FlashCompatibleConvert.ToDouble(s);
  }

  private bool ReadBoolean()
  {
    bool flag = (ushort) this.reader.Peek() == (ushort) 116;
    for (int index = 0; index < 4; ++index)
      this.reader.Read();
    if (!flag)
      this.reader.Read();
    return flag;
  }

  private void ReadNull()
  {
    for (int index = 0; index < 4; ++index)
      this.reader.Read();
  }

  private string ReadString()
  {
    string str = "";
    bool flag = (ushort) this.reader.Peek() == (ushort) 39;
    this.reader.Read();
    while (true)
    {
      char ch = (char) this.reader.Peek();
      if ((flag || ch != '"') && (!flag || ch != '\''))
        str += ((char) this.reader.Read()).ToString();
      else
        break;
    }
    this.reader.Read();
    return str;
  }

  private List<IJSONFieldValue> ReadList()
  {
    List<IJSONFieldValue> jsonFieldValueList = new List<IJSONFieldValue>();
    this.reader.Read();
    while (true)
    {
      switch ((char) this.reader.Peek())
      {
        case ',':
          this.reader.Read();
          this.SkipWhitespace();
          continue;
        case ']':
          goto label_2;
        default:
          IJSONFieldValue jsonFieldValue = this.ReadValue();
          jsonFieldValueList.Add(jsonFieldValue);
          this.SkipWhitespace();
          continue;
      }
    }
label_2:
    this.reader.Read();
    return jsonFieldValueList;
  }
}
