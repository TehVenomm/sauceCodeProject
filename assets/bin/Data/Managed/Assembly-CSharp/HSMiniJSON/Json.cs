// Decompiled with JetBrains decompiler
// Type: HSMiniJSON.Json
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

#nullable disable
namespace HSMiniJSON;

public static class Json
{
  public static object Deserialize(string json)
  {
    return json == null ? (object) null : Json.Parser.Parse(json);
  }

  public static string Serialize(object obj) => Json.Serializer.Serialize(obj);

  private sealed class Parser : IDisposable
  {
    private const string WORD_BREAK = "{}[],:\"";
    private StringReader json;

    public static bool IsWordBreak(char c) => char.IsWhiteSpace(c) || "{}[],:\"".IndexOf(c) != -1;

    private Parser(string jsonString) => this.json = new StringReader(jsonString);

    public static object Parse(string jsonString)
    {
      using (Json.Parser parser = new Json.Parser(jsonString))
        return parser.ParseValue();
    }

    public void Dispose()
    {
      this.json.Dispose();
      this.json = (StringReader) null;
    }

    private Dictionary<string, object> ParseObject()
    {
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      this.json.Read();
      while (true)
      {
        Json.Parser.TOKEN nextToken;
        do
        {
          nextToken = this.NextToken;
          if (nextToken != Json.Parser.TOKEN.NONE)
          {
            if (nextToken == Json.Parser.TOKEN.CURLY_CLOSE)
              goto label_5;
          }
          else
            goto label_4;
        }
        while (nextToken == Json.Parser.TOKEN.COMMA);
        string key = this.ParseString();
        if (key != null)
        {
          if (this.NextToken == Json.Parser.TOKEN.COLON)
          {
            this.json.Read();
            dictionary[key] = this.ParseValue();
          }
          else
            goto label_9;
        }
        else
          goto label_7;
      }
label_4:
      return (Dictionary<string, object>) null;
label_5:
      return dictionary;
label_7:
      return (Dictionary<string, object>) null;
label_9:
      return (Dictionary<string, object>) null;
    }

    private List<object> ParseArray()
    {
      List<object> array = new List<object>();
      this.json.Read();
      bool flag = true;
      while (flag)
      {
        Json.Parser.TOKEN nextToken = this.NextToken;
        switch (nextToken)
        {
          case Json.Parser.TOKEN.NONE:
            return (List<object>) null;
          case Json.Parser.TOKEN.SQUARED_CLOSE:
            flag = false;
            continue;
          case Json.Parser.TOKEN.COMMA:
            continue;
          default:
            object byToken = this.ParseByToken(nextToken);
            array.Add(byToken);
            continue;
        }
      }
      return array;
    }

    private object ParseValue() => this.ParseByToken(this.NextToken);

    private object ParseByToken(Json.Parser.TOKEN token)
    {
      switch (token)
      {
        case Json.Parser.TOKEN.CURLY_OPEN:
          return (object) this.ParseObject();
        case Json.Parser.TOKEN.SQUARED_OPEN:
          return (object) this.ParseArray();
        case Json.Parser.TOKEN.STRING:
          return (object) this.ParseString();
        case Json.Parser.TOKEN.NUMBER:
          return this.ParseNumber();
        case Json.Parser.TOKEN.TRUE:
          return (object) true;
        case Json.Parser.TOKEN.FALSE:
          return (object) false;
        case Json.Parser.TOKEN.NULL:
          return (object) null;
        default:
          return (object) null;
      }
    }

    private string ParseString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      this.json.Read();
      bool flag = true;
      while (flag)
      {
        if (this.json.Peek() == -1)
          break;
        char nextChar1 = this.NextChar;
        switch (nextChar1)
        {
          case '"':
            flag = false;
            continue;
          case '\\':
            if (this.json.Peek() == -1)
            {
              flag = false;
              continue;
            }
            char nextChar2 = this.NextChar;
            switch (nextChar2)
            {
              case '"':
              case '/':
              case '\\':
                stringBuilder.Append(nextChar2);
                continue;
              case 'b':
                stringBuilder.Append('\b');
                continue;
              case 'f':
                stringBuilder.Append('\f');
                continue;
              case 'n':
                stringBuilder.Append('\n');
                continue;
              case 'r':
                stringBuilder.Append('\r');
                continue;
              case 't':
                stringBuilder.Append('\t');
                continue;
              case 'u':
                char[] chArray = new char[4];
                for (int index = 0; index < 4; ++index)
                  chArray[index] = this.NextChar;
                stringBuilder.Append((char) Convert.ToInt32(new string(chArray), 16 /*0x10*/));
                continue;
              default:
                continue;
            }
          default:
            stringBuilder.Append(nextChar1);
            continue;
        }
      }
      return stringBuilder.ToString();
    }

    private object ParseNumber()
    {
      string nextWord = this.NextWord;
      if (nextWord.IndexOf('.') == -1)
      {
        long result;
        long.TryParse(nextWord, out result);
        return (object) result;
      }
      double result1;
      double.TryParse(nextWord, out result1);
      return (object) result1;
    }

    private void EatWhitespace()
    {
      while (char.IsWhiteSpace(this.PeekChar))
      {
        this.json.Read();
        if (this.json.Peek() == -1)
          break;
      }
    }

    private char PeekChar => Convert.ToChar(this.json.Peek());

    private char NextChar => Convert.ToChar(this.json.Read());

    private string NextWord
    {
      get
      {
        StringBuilder stringBuilder = new StringBuilder();
        while (!Json.Parser.IsWordBreak(this.PeekChar))
        {
          stringBuilder.Append(this.NextChar);
          if (this.json.Peek() == -1)
            break;
        }
        return stringBuilder.ToString();
      }
    }

    private Json.Parser.TOKEN NextToken
    {
      get
      {
        this.EatWhitespace();
        if (this.json.Peek() == -1)
          return Json.Parser.TOKEN.NONE;
        switch (this.PeekChar)
        {
          case '"':
            return Json.Parser.TOKEN.STRING;
          case ',':
            this.json.Read();
            return Json.Parser.TOKEN.COMMA;
          case '-':
          case '0':
          case '1':
          case '2':
          case '3':
          case '4':
          case '5':
          case '6':
          case '7':
          case '8':
          case '9':
            return Json.Parser.TOKEN.NUMBER;
          case ':':
            return Json.Parser.TOKEN.COLON;
          case '[':
            return Json.Parser.TOKEN.SQUARED_OPEN;
          case ']':
            this.json.Read();
            return Json.Parser.TOKEN.SQUARED_CLOSE;
          case '{':
            return Json.Parser.TOKEN.CURLY_OPEN;
          case '}':
            this.json.Read();
            return Json.Parser.TOKEN.CURLY_CLOSE;
          default:
            switch (this.NextWord)
            {
              case "false":
                return Json.Parser.TOKEN.FALSE;
              case "true":
                return Json.Parser.TOKEN.TRUE;
              case "null":
                return Json.Parser.TOKEN.NULL;
              default:
                return Json.Parser.TOKEN.NONE;
            }
        }
      }
    }

    private enum TOKEN
    {
      NONE,
      CURLY_OPEN,
      CURLY_CLOSE,
      SQUARED_OPEN,
      SQUARED_CLOSE,
      COLON,
      COMMA,
      STRING,
      NUMBER,
      TRUE,
      FALSE,
      NULL,
    }
  }

  private sealed class Serializer
  {
    private StringBuilder builder;

    private Serializer() => this.builder = new StringBuilder();

    public static string Serialize(object obj)
    {
      Json.Serializer serializer = new Json.Serializer();
      serializer.SerializeValue(obj);
      return serializer.builder.ToString();
    }

    private void SerializeValue(object value)
    {
      switch (value)
      {
        case null:
          this.builder.Append("null");
          break;
        case string str:
          this.SerializeString(str);
          break;
        case bool flag:
          this.builder.Append(flag ? "true" : "false");
          break;
        case IList anArray:
          this.SerializeArray(anArray);
          break;
        case IDictionary dictionary:
          this.SerializeObject(dictionary);
          break;
        case char c:
          this.SerializeString(new string(c, 1));
          break;
        default:
          this.SerializeOther(value);
          break;
      }
    }

    private void SerializeObject(IDictionary obj)
    {
      bool flag = true;
      this.builder.Append('{');
      foreach (object key in (IEnumerable) obj.Keys)
      {
        if (!flag)
          this.builder.Append(',');
        this.SerializeString(key.ToString());
        this.builder.Append(':');
        this.SerializeValue(obj[key]);
        flag = false;
      }
      this.builder.Append('}');
    }

    private void SerializeArray(IList anArray)
    {
      this.builder.Append('[');
      bool flag = true;
      foreach (object an in (IEnumerable) anArray)
      {
        if (!flag)
          this.builder.Append(',');
        this.SerializeValue(an);
        flag = false;
      }
      this.builder.Append(']');
    }

    private void SerializeString(string str)
    {
      this.builder.Append('"');
      foreach (char ch in str.ToCharArray())
      {
        switch (ch)
        {
          case '\b':
            this.builder.Append("\\b");
            break;
          case '\t':
            this.builder.Append("\\t");
            break;
          case '\n':
            this.builder.Append("\\n");
            break;
          case '\f':
            this.builder.Append("\\f");
            break;
          case '\r':
            this.builder.Append("\\r");
            break;
          case '"':
            this.builder.Append("\\\"");
            break;
          case '\\':
            this.builder.Append("\\\\");
            break;
          default:
            int int32 = Convert.ToInt32(ch);
            if (int32 >= 32 /*0x20*/ && int32 <= 126)
            {
              this.builder.Append(ch);
              break;
            }
            this.builder.Append("\\u");
            this.builder.Append(int32.ToString("x4"));
            break;
        }
      }
      this.builder.Append('"');
    }

    private void SerializeOther(object value)
    {
      switch (value)
      {
        case float num:
          this.builder.Append(num.ToString("R"));
          break;
        case int _:
        case uint _:
        case long _:
        case sbyte _:
        case byte _:
        case short _:
        case ushort _:
        case ulong _:
          this.builder.Append(value);
          break;
        case double _:
        case Decimal _:
          this.builder.Append(Convert.ToDouble(value).ToString("R"));
          break;
        default:
          this.SerializeString(value.ToString());
          break;
      }
    }
  }
}
