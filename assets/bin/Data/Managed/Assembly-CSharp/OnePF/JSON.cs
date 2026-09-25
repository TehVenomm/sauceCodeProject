// Decompiled with JetBrains decompiler
// Type: OnePF.JSON
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

#nullable disable
namespace OnePF;

public class JSON
{
  public Dictionary<string, object> fields = new Dictionary<string, object>();

  public JSON()
  {
  }

  public JSON(string jsonString) => this.serialized = jsonString;

  public object this[string fieldName]
  {
    get => this.fields.ContainsKey(fieldName) ? this.fields[fieldName] : (object) null;
    set
    {
      if (this.fields.ContainsKey(fieldName))
        this.fields[fieldName] = value;
      else
        this.fields.Add(fieldName, value);
    }
  }

  public string ToString(string fieldName)
  {
    return this.fields.ContainsKey(fieldName) ? Convert.ToString(this.fields[fieldName]) : "";
  }

  public int ToInt(string fieldName)
  {
    return this.fields.ContainsKey(fieldName) ? Convert.ToInt32(this.fields[fieldName]) : 0;
  }

  public long ToLong(string fieldName)
  {
    return this.fields.ContainsKey(fieldName) ? Convert.ToInt64(this.fields[fieldName]) : 0L;
  }

  public float ToFloat(string fieldName)
  {
    return this.fields.ContainsKey(fieldName) ? Convert.ToSingle(this.fields[fieldName]) : 0.0f;
  }

  public bool ToBoolean(string fieldName)
  {
    return this.fields.ContainsKey(fieldName) && Convert.ToBoolean(this.fields[fieldName]);
  }

  public string serialized
  {
    get => JSON._JSON.Serialize(this);
    set
    {
      JSON json = JSON._JSON.Deserialize(value);
      if (json == null)
        return;
      this.fields = json.fields;
    }
  }

  public JSON ToJSON(string fieldName)
  {
    if (!this.fields.ContainsKey(fieldName))
      this.fields.Add(fieldName, (object) new JSON());
    return (JSON) this[fieldName];
  }

  public static implicit operator Vector2(JSON value)
  {
    return Vector2.op_Implicit(new Vector3(Convert.ToSingle(value["x"]), Convert.ToSingle(value["y"])));
  }

  public static explicit operator JSON(Vector2 value)
  {
    return new JSON()
    {
      ["x"] = (object) value.x,
      ["y"] = (object) value.y
    };
  }

  public static implicit operator Vector3(JSON value)
  {
    return new Vector3(Convert.ToSingle(value["x"]), Convert.ToSingle(value["y"]), Convert.ToSingle(value["z"]));
  }

  public static explicit operator JSON(Vector3 value)
  {
    return new JSON()
    {
      ["x"] = (object) value.x,
      ["y"] = (object) value.y,
      ["z"] = (object) value.z
    };
  }

  public static implicit operator Quaternion(JSON value)
  {
    return new Quaternion(Convert.ToSingle(value["x"]), Convert.ToSingle(value["y"]), Convert.ToSingle(value["z"]), Convert.ToSingle(value["w"]));
  }

  public static explicit operator JSON(Quaternion value)
  {
    return new JSON()
    {
      ["x"] = (object) value.x,
      ["y"] = (object) value.y,
      ["z"] = (object) value.z,
      ["w"] = (object) value.w
    };
  }

  public static implicit operator Color(JSON value)
  {
    return new Color(Convert.ToSingle(value["r"]), Convert.ToSingle(value["g"]), Convert.ToSingle(value["b"]), Convert.ToSingle(value["a"]));
  }

  public static explicit operator JSON(Color value)
  {
    return new JSON()
    {
      ["r"] = (object) value.r,
      ["g"] = (object) value.g,
      ["b"] = (object) value.b,
      ["a"] = (object) value.a
    };
  }

  public static implicit operator Color32(JSON value)
  {
    return new Color32(Convert.ToByte(value["r"]), Convert.ToByte(value["g"]), Convert.ToByte(value["b"]), Convert.ToByte(value["a"]));
  }

  public static explicit operator JSON(Color32 value)
  {
    return new JSON()
    {
      ["r"] = (object) value.r,
      ["g"] = (object) value.g,
      ["b"] = (object) value.b,
      ["a"] = (object) value.a
    };
  }

  public static implicit operator Rect(JSON value)
  {
    return new Rect((float) Convert.ToByte(value["left"]), (float) Convert.ToByte(value["top"]), (float) Convert.ToByte(value["width"]), (float) Convert.ToByte(value["height"]));
  }

  public static explicit operator JSON(Rect value)
  {
    return new JSON()
    {
      ["left"] = (object) ((Rect) ref value).xMin,
      ["top"] = (object) ((Rect) ref value).yMax,
      ["width"] = (object) ((Rect) ref value).width,
      ["height"] = (object) ((Rect) ref value).height
    };
  }

  public T[] ToArray<T>(string fieldName)
  {
    if (!this.fields.ContainsKey(fieldName) || !(this.fields[fieldName] is IEnumerable))
      return new T[0];
    List<T> objList = new List<T>();
    foreach (object obj in this.fields[fieldName] as IEnumerable)
    {
      switch (objList)
      {
        case List<string> _:
          (objList as List<string>).Add(Convert.ToString(obj));
          continue;
        case List<int> _:
          (objList as List<int>).Add(Convert.ToInt32(obj));
          continue;
        case List<float> _:
          (objList as List<float>).Add(Convert.ToSingle(obj));
          continue;
        case List<bool> _:
          (objList as List<bool>).Add(Convert.ToBoolean(obj));
          continue;
        case List<Vector2> _:
          (objList as List<Vector2>).Add((Vector2) (JSON) obj);
          continue;
        case List<Vector3> _:
          (objList as List<Vector3>).Add((Vector3) (JSON) obj);
          continue;
        case List<Rect> _:
          (objList as List<Rect>).Add((Rect) (JSON) obj);
          continue;
        case List<Color> _:
          (objList as List<Color>).Add((Color) (JSON) obj);
          continue;
        case List<Color32> _:
          (objList as List<Color32>).Add((Color32) (JSON) obj);
          continue;
        case List<Quaternion> _:
          (objList as List<Quaternion>).Add((Quaternion) (JSON) obj);
          continue;
        case List<JSON> _:
          (objList as List<JSON>).Add((JSON) obj);
          continue;
        default:
          continue;
      }
    }
    return objList.ToArray();
  }

  private sealed class _JSON
  {
    public static JSON Deserialize(string json)
    {
      return json == null ? (JSON) null : JSON._JSON.Parser.Parse(json);
    }

    public static string Serialize(JSON obj) => JSON._JSON.Serializer.Serialize(obj);

    private sealed class Parser : IDisposable
    {
      private const string WHITE_SPACE = " \t\n\r";
      private const string WORD_BREAK = " \t\n\r{}[],:\"";
      private StringReader json;

      private Parser(string jsonString) => this.json = new StringReader(jsonString);

      public static JSON Parse(string jsonString)
      {
        using (JSON._JSON.Parser parser = new JSON._JSON.Parser(jsonString))
          return parser.ParseValue() as JSON;
      }

      public void Dispose()
      {
        this.json.Dispose();
        this.json = (StringReader) null;
      }

      private JSON ParseObject()
      {
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        JSON json = new JSON();
        json.fields = dictionary;
        this.json.Read();
        while (true)
        {
          JSON._JSON.Parser.TOKEN nextToken;
          do
          {
            nextToken = this.NextToken;
            if (nextToken != JSON._JSON.Parser.TOKEN.NONE)
            {
              if (nextToken == JSON._JSON.Parser.TOKEN.CURLY_CLOSE)
                goto label_5;
            }
            else
              goto label_4;
          }
          while (nextToken == JSON._JSON.Parser.TOKEN.COMMA);
          string key = this.ParseString();
          if (key != null)
          {
            if (this.NextToken == JSON._JSON.Parser.TOKEN.COLON)
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
        return (JSON) null;
label_5:
        return json;
label_7:
        return (JSON) null;
label_9:
        return (JSON) null;
      }

      private List<object> ParseArray()
      {
        List<object> array = new List<object>();
        this.json.Read();
        bool flag = true;
        while (flag)
        {
          JSON._JSON.Parser.TOKEN nextToken = this.NextToken;
          switch (nextToken)
          {
            case JSON._JSON.Parser.TOKEN.NONE:
              return (List<object>) null;
            case JSON._JSON.Parser.TOKEN.SQUARED_CLOSE:
              flag = false;
              continue;
            case JSON._JSON.Parser.TOKEN.COMMA:
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

      private object ParseByToken(JSON._JSON.Parser.TOKEN token)
      {
        switch (token)
        {
          case JSON._JSON.Parser.TOKEN.CURLY_OPEN:
            return (object) this.ParseObject();
          case JSON._JSON.Parser.TOKEN.SQUARED_OPEN:
            return (object) this.ParseArray();
          case JSON._JSON.Parser.TOKEN.STRING:
            return (object) this.ParseString();
          case JSON._JSON.Parser.TOKEN.NUMBER:
            return this.ParseNumber();
          case JSON._JSON.Parser.TOKEN.TRUE:
            return (object) true;
          case JSON._JSON.Parser.TOKEN.FALSE:
            return (object) false;
          case JSON._JSON.Parser.TOKEN.NULL:
            return (object) null;
          default:
            return (object) null;
        }
      }

      private string ParseString()
      {
        StringBuilder stringBuilder1 = new StringBuilder();
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
                  stringBuilder1.Append(nextChar2);
                  continue;
                case 'b':
                  stringBuilder1.Append('\b');
                  continue;
                case 'f':
                  stringBuilder1.Append('\f');
                  continue;
                case 'n':
                  stringBuilder1.Append('\n');
                  continue;
                case 'r':
                  stringBuilder1.Append('\r');
                  continue;
                case 't':
                  stringBuilder1.Append('\t');
                  continue;
                case 'u':
                  StringBuilder stringBuilder2 = new StringBuilder();
                  for (int index = 0; index < 4; ++index)
                    stringBuilder2.Append(this.NextChar);
                  stringBuilder1.Append((char) Convert.ToInt32(stringBuilder2.ToString(), 16 /*0x10*/));
                  continue;
                default:
                  continue;
              }
            default:
              stringBuilder1.Append(nextChar1);
              continue;
          }
        }
        return stringBuilder1.ToString();
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
        while (" \t\n\r".IndexOf(this.PeekChar) != -1)
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
          while (" \t\n\r{}[],:\"".IndexOf(this.PeekChar) == -1)
          {
            stringBuilder.Append(this.NextChar);
            if (this.json.Peek() == -1)
              break;
          }
          return stringBuilder.ToString();
        }
      }

      private JSON._JSON.Parser.TOKEN NextToken
      {
        get
        {
          this.EatWhitespace();
          if (this.json.Peek() == -1)
            return JSON._JSON.Parser.TOKEN.NONE;
          switch (this.PeekChar)
          {
            case '"':
              return JSON._JSON.Parser.TOKEN.STRING;
            case ',':
              this.json.Read();
              return JSON._JSON.Parser.TOKEN.COMMA;
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
              return JSON._JSON.Parser.TOKEN.NUMBER;
            case ':':
              return JSON._JSON.Parser.TOKEN.COLON;
            case '[':
              return JSON._JSON.Parser.TOKEN.SQUARED_OPEN;
            case ']':
              this.json.Read();
              return JSON._JSON.Parser.TOKEN.SQUARED_CLOSE;
            case '{':
              return JSON._JSON.Parser.TOKEN.CURLY_OPEN;
            case '}':
              this.json.Read();
              return JSON._JSON.Parser.TOKEN.CURLY_CLOSE;
            default:
              switch (this.NextWord)
              {
                case "false":
                  return JSON._JSON.Parser.TOKEN.FALSE;
                case "true":
                  return JSON._JSON.Parser.TOKEN.TRUE;
                case "null":
                  return JSON._JSON.Parser.TOKEN.NULL;
                default:
                  return JSON._JSON.Parser.TOKEN.NONE;
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

      public static string Serialize(JSON obj)
      {
        JSON._JSON.Serializer serializer = new JSON._JSON.Serializer();
        serializer.SerializeValue((object) obj);
        return serializer.builder.ToString();
      }

      private void SerializeValue(object value)
      {
        switch (value)
        {
          case null:
            this.builder.Append("null");
            break;
          case string _:
            this.SerializeString(value as string);
            break;
          case bool _:
            this.builder.Append(value.ToString().ToLower());
            break;
          case JSON _:
            this.SerializeObject(value as JSON);
            break;
          case IDictionary _:
            this.SerializeDictionary(value as IDictionary);
            break;
          case IList _:
            this.SerializeArray(value as IList);
            break;
          case char _:
            this.SerializeString(value.ToString());
            break;
          default:
            this.SerializeOther(value);
            break;
        }
      }

      private void SerializeObject(JSON obj) => this.SerializeDictionary((IDictionary) obj.fields);

      private void SerializeDictionary(IDictionary obj)
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
              this.builder.Append("\\u" + Convert.ToString(int32, 16 /*0x10*/).PadLeft(4, '0'));
              break;
          }
        }
        this.builder.Append('"');
      }

      private void SerializeOther(object value)
      {
        switch (value)
        {
          case float _:
          case int _:
          case uint _:
          case long _:
          case double _:
          case sbyte _:
          case byte _:
          case short _:
          case ushort _:
          case ulong _:
          case Decimal _:
            this.builder.Append(value.ToString());
            break;
          default:
            this.SerializeString(value.ToString());
            break;
        }
      }
    }
  }
}
