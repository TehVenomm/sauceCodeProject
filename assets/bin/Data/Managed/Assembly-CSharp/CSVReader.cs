// Decompiled with JetBrains decompiler
// Type: CSVReader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Text;
using UnityEngine;

#nullable disable
public class CSVReader
{
  private int pos;
  private string str;
  private int len;
  private StringBuilder builder;
  private StringBuilder baseBuilder = new StringBuilder(256 /*0x0100*/, 1024 /*0x0400*/);
  private int[] namedIndex;
  private StringBuilder[] namedStr;
  private int namedPopIndex;
  private string nameTable;

  public CSVReader()
  {
  }

  public CSVReader(string text, string name_table, bool decrypt = false)
  {
    this.Initialize(text, name_table: name_table, decrypt: decrypt);
  }

  public void Initialize(string csv_text, bool single_line = false, string name_table = null, bool decrypt = false)
  {
    if (decrypt)
      csv_text = Cipher.DecryptRJ128("Auto_XlS_To_CSV.", "yCNBH$$rCNGvC+#f", csv_text);
    this.pos = single_line ? 0 : -1;
    this.str = csv_text;
    this.len = this.str.Length;
    this.namedIndex = (int[]) null;
    this.namedStr = (StringBuilder[]) null;
    this.namedPopIndex = 0;
    this.nameTable = name_table;
    if (!single_line && csv_text.StartsWith("output"))
    {
      this.pos = 0;
      name_table = (string) null;
    }
    this.builder = this.baseBuilder;
    int start_pos = 0;
    if (this.str[0] == '#')
      start_pos = 1;
    this.SetupNameTable(this.nameTable, start_pos);
  }

  private void SetupNameTable(string name_table, int start_pos)
  {
    if (string.IsNullOrEmpty(name_table))
      return;
    string empty = string.Empty;
    this.namedStr = (StringBuilder[]) null;
    this.pos = start_pos;
    int length1 = 0;
    while (this.NextValue())
      ++length1;
    string[] strArray = name_table.Split(',');
    int[] numArray = new int[length1];
    int index1 = 0;
    for (int length2 = numArray.Length; index1 < length2; ++index1)
      numArray[index1] = -1;
    this.pos = start_pos;
    int index2 = 0;
    int index3 = -1;
    for (int length3 = strArray.Length; index2 < length3; ++index2)
    {
      bool flag = false;
      while ((bool) this.Pop(ref empty))
      {
        ++index3;
        if (empty == strArray[index2])
        {
          flag = true;
          numArray[index3] = index2;
          break;
        }
      }
      if (!flag)
      {
        index3 = -1;
        this.pos = start_pos;
        while ((bool) this.Pop(ref empty))
        {
          ++index3;
          if (empty == strArray[index2])
          {
            flag = true;
            numArray[index3] = index2;
            break;
          }
        }
      }
      int num = flag ? 1 : 0;
    }
    this.namedIndex = numArray;
    this.namedStr = new StringBuilder[strArray.Length];
    int index4 = 0;
    for (int length4 = this.namedStr.Length; index4 < length4; ++index4)
      this.namedStr[index4] = new StringBuilder(64 /*0x40*/);
    this.namedPopIndex = this.namedStr.Length;
  }

  public bool NextLine()
  {
    if (this.len <= 0 || this.pos >= this.len)
      return false;
    if (this.pos == -1)
    {
      this.pos = 0;
    }
    else
    {
      do
        ;
      while (this._NextValue());
      switch (this.str[this.pos])
      {
        case '\n':
          ++this.pos;
          if (this.pos < this.len && this.str[this.pos] == '\r')
          {
            ++this.pos;
            break;
          }
          break;
        case '\r':
          ++this.pos;
          if (this.pos < this.len && this.str[this.pos] == '\n')
          {
            ++this.pos;
            break;
          }
          break;
        default:
          return false;
      }
      if (this.pos >= this.len)
        return false;
    }
    if (this.str[this.pos] == '#')
    {
      this.SetupNameTable(this.nameTable, this.pos + 1);
      return this.NextLine();
    }
    if (this.namedIndex != null)
    {
      int index1 = 0;
      for (int length = this.namedStr.Length; index1 < length; ++index1)
        this.namedStr[index1].Length = 0;
      int index2 = 0;
      for (int length = this.namedIndex.Length; index2 != length; ++index2)
      {
        this.builder = this.namedIndex[index2] == -1 ? this.baseBuilder : this.namedStr[this.namedIndex[index2]];
        if (!this._NextValue())
          break;
      }
      this.builder = (StringBuilder) null;
      this.namedPopIndex = 0;
    }
    return true;
  }

  public bool NextValue()
  {
    if (this.namedStr == null)
      return this._NextValue();
    if (this.namedPopIndex < this.namedStr.Length)
    {
      this.builder = this.namedStr[this.namedPopIndex++];
      return true;
    }
    this.builder = this.baseBuilder;
    this.builder.Length = 0;
    return false;
  }

  private bool _NextValue()
  {
    int pos = this.pos;
    int len = this.len;
    if (pos >= len)
      return false;
    string str = this.str;
    char ch1 = str[pos];
    switch (ch1)
    {
      case '\n':
      case '\r':
        return false;
      default:
        this.builder.Length = 0;
        if (ch1 == '"')
        {
          int index = pos + 1;
          while (index < len)
          {
            char ch2 = str[index++];
            if (ch2 == '"')
            {
              if (index < len && str[index] == '"')
              {
                ++index;
                this.builder.Append('"');
              }
              else
              {
                if (index < len && str[index] == ',')
                  ++index;
                this.pos = index;
                return true;
              }
            }
            else
              this.builder.Append(ch2);
          }
        }
        else
        {
          while (pos < len)
          {
            char ch3 = str[pos];
            switch (ch3)
            {
              case '\n':
              case '\r':
                this.pos = pos;
                return true;
              case ',':
                this.pos = pos + 1;
                return true;
              default:
                this.builder.Append(ch3);
                ++pos;
                if (pos >= len)
                {
                  this.pos = pos;
                  return true;
                }
                continue;
            }
          }
        }
        return false;
    }
  }

  public bool IsEmpty()
  {
    int pos = this.pos;
    int namedPopIndex = this.namedPopIndex;
    bool flag = true;
    if (this.NextValue() && this.builder.Length > 0)
      flag = false;
    this.pos = pos;
    this.namedPopIndex = namedPopIndex;
    return flag;
  }

  public CSVReader.PopResult Pop(ref int value)
  {
    if (!this.NextValue())
      return CSVReader.PopResult.EMPTY;
    if (this.builder.Length == 0)
      return CSVReader.PopResult.EMPTY;
    try
    {
      value = int.Parse(this.builder.ToString());
    }
    catch
    {
      return CSVReader.PopResult.PARSE_ERROR;
    }
    return CSVReader.PopResult.SUCCESS;
  }

  public CSVReader.PopResult Pop(ref XorInt value)
  {
    int num = 0;
    CSVReader.PopResult popResult = this.Pop(ref num);
    value = (XorInt) num;
    return popResult;
  }

  public CSVReader.PopResult Pop(ref uint value)
  {
    if (!this.NextValue())
      return CSVReader.PopResult.EMPTY;
    if (this.builder.Length == 0)
      return CSVReader.PopResult.EMPTY;
    try
    {
      value = uint.Parse(this.builder.ToString());
    }
    catch
    {
      return CSVReader.PopResult.PARSE_ERROR;
    }
    return CSVReader.PopResult.SUCCESS;
  }

  public CSVReader.PopResult Pop(ref XorUInt value)
  {
    uint num = 0;
    CSVReader.PopResult popResult = this.Pop(ref num);
    value = (XorUInt) num;
    return popResult;
  }

  public CSVReader.PopResult Pop(ref short value)
  {
    if (!this.NextValue())
      return CSVReader.PopResult.EMPTY;
    if (this.builder.Length == 0)
      return CSVReader.PopResult.EMPTY;
    try
    {
      value = short.Parse(this.builder.ToString());
    }
    catch
    {
      return CSVReader.PopResult.PARSE_ERROR;
    }
    return CSVReader.PopResult.SUCCESS;
  }

  public CSVReader.PopResult Pop(ref ushort value)
  {
    if (!this.NextValue())
      return CSVReader.PopResult.EMPTY;
    if (this.builder.Length == 0)
      return CSVReader.PopResult.EMPTY;
    try
    {
      value = ushort.Parse(this.builder.ToString());
    }
    catch
    {
      return CSVReader.PopResult.PARSE_ERROR;
    }
    return CSVReader.PopResult.SUCCESS;
  }

  public CSVReader.PopResult Pop(ref char value)
  {
    if (!this.NextValue())
      return CSVReader.PopResult.EMPTY;
    if (this.builder.Length == 0)
      return CSVReader.PopResult.EMPTY;
    try
    {
      value = char.Parse(this.builder.ToString());
    }
    catch
    {
      return CSVReader.PopResult.PARSE_ERROR;
    }
    return CSVReader.PopResult.SUCCESS;
  }

  public CSVReader.PopResult Pop(ref byte value)
  {
    if (!this.NextValue())
      return CSVReader.PopResult.EMPTY;
    if (this.builder.Length == 0)
      return CSVReader.PopResult.EMPTY;
    try
    {
      value = byte.Parse(this.builder.ToString());
    }
    catch
    {
      return CSVReader.PopResult.PARSE_ERROR;
    }
    return CSVReader.PopResult.SUCCESS;
  }

  public CSVReader.PopResult Pop(ref float value)
  {
    if (!this.NextValue())
      return CSVReader.PopResult.EMPTY;
    if (this.builder.Length == 0)
      return CSVReader.PopResult.EMPTY;
    try
    {
      value = float.Parse(this.builder.ToString());
    }
    catch
    {
      return CSVReader.PopResult.PARSE_ERROR;
    }
    return CSVReader.PopResult.SUCCESS;
  }

  public CSVReader.PopResult Pop(ref XorFloat value)
  {
    float num = 0.0f;
    CSVReader.PopResult popResult = this.Pop(ref num);
    value = (XorFloat) num;
    return popResult;
  }

  public CSVReader.PopResult Pop(ref string value)
  {
    if (!this.NextValue())
      return CSVReader.PopResult.EMPTY;
    if (this.builder.Length == 0)
    {
      value = string.Empty;
      return CSVReader.PopResult.EMPTY;
    }
    value = this.builder.ToString();
    return CSVReader.PopResult.SUCCESS;
  }

  public CSVReader.PopResult Pop(ref bool value)
  {
    if (!this.NextValue() || this.builder.Length == 0)
      return CSVReader.PopResult.EMPTY;
    string str = this.builder.ToString();
    value = str != "0" && str != "false";
    return CSVReader.PopResult.SUCCESS;
  }

  public CSVReader.PopResult Pop(ref Vector2 value)
  {
    CSVReader.PopResult popResult1 = this.Pop(ref value.x);
    CSVReader.PopResult popResult2 = this.Pop(ref value.y);
    if (popResult1 == CSVReader.PopResult.SUCCESS && popResult2 == CSVReader.PopResult.SUCCESS)
      return CSVReader.PopResult.SUCCESS;
    return popResult1 == CSVReader.PopResult.EMPTY && popResult2 == CSVReader.PopResult.EMPTY ? CSVReader.PopResult.EMPTY : CSVReader.PopResult.UNKNOWN;
  }

  public CSVReader.PopResult Pop(ref Vector3 value)
  {
    CSVReader.PopResult popResult1 = this.Pop(ref value.x);
    CSVReader.PopResult popResult2 = this.Pop(ref value.y);
    CSVReader.PopResult popResult3 = this.Pop(ref value.z);
    if (popResult1 == CSVReader.PopResult.SUCCESS && popResult2 == CSVReader.PopResult.SUCCESS && popResult3 == CSVReader.PopResult.SUCCESS)
      return CSVReader.PopResult.SUCCESS;
    return popResult1 == CSVReader.PopResult.EMPTY && popResult2 == CSVReader.PopResult.EMPTY && popResult3 == CSVReader.PopResult.EMPTY ? CSVReader.PopResult.EMPTY : CSVReader.PopResult.UNKNOWN;
  }

  public CSVReader.PopResult Pop(ref Vector4 value)
  {
    CSVReader.PopResult popResult1 = this.Pop(ref value.x);
    CSVReader.PopResult popResult2 = this.Pop(ref value.y);
    CSVReader.PopResult popResult3 = this.Pop(ref value.z);
    CSVReader.PopResult popResult4 = this.Pop(ref value.w);
    if (popResult1 == CSVReader.PopResult.SUCCESS && popResult2 == CSVReader.PopResult.SUCCESS && popResult3 == CSVReader.PopResult.SUCCESS && popResult4 == CSVReader.PopResult.SUCCESS)
      return CSVReader.PopResult.SUCCESS;
    return popResult1 == CSVReader.PopResult.EMPTY && popResult2 == CSVReader.PopResult.EMPTY && popResult3 == CSVReader.PopResult.EMPTY && popResult4 == CSVReader.PopResult.EMPTY ? CSVReader.PopResult.EMPTY : CSVReader.PopResult.UNKNOWN;
  }

  public CSVReader.PopResult Pop<T>(ref T value)
  {
    if (!this.NextValue())
      return CSVReader.PopResult.EMPTY;
    if (this.builder.Length == 0)
      return CSVReader.PopResult.EMPTY;
    try
    {
      if (!Enum.IsDefined(typeof (T), (object) this.builder.ToString()))
        return CSVReader.PopResult.DONT_DEFINE;
      value = (T) Enum.Parse(typeof (T), this.builder.ToString());
    }
    catch
    {
      return CSVReader.PopResult.DONT_DEFINE;
    }
    return CSVReader.PopResult.SUCCESS;
  }

  public CSVReader.PopResult PopEnum<T>(ref T value, T defValue)
  {
    if (!this.NextValue())
    {
      value = defValue;
      return CSVReader.PopResult.EMPTY;
    }
    if (this.builder.Length == 0)
    {
      value = defValue;
      return CSVReader.PopResult.EMPTY;
    }
    try
    {
      if (!Enum.IsDefined(typeof (T), (object) this.builder.ToString()))
      {
        value = defValue;
        return CSVReader.PopResult.DONT_DEFINE;
      }
      value = (T) Enum.Parse(typeof (T), this.builder.ToString());
    }
    catch
    {
      value = defValue;
      return CSVReader.PopResult.UNKNOWN;
    }
    return CSVReader.PopResult.SUCCESS;
  }

  public CSVReader.PopResult PopColor(ref Vector3 value)
  {
    int maxValue1 = (int) byte.MaxValue;
    int maxValue2 = (int) byte.MaxValue;
    int maxValue3 = (int) byte.MaxValue;
    CSVReader.PopResult popResult1 = this.Pop(ref maxValue1);
    CSVReader.PopResult popResult2 = this.Pop(ref maxValue2);
    CSVReader.PopResult popResult3 = this.Pop(ref maxValue3);
    if (popResult1 == CSVReader.PopResult.SUCCESS && popResult2 == CSVReader.PopResult.SUCCESS && popResult3 == CSVReader.PopResult.SUCCESS)
    {
      value.x = (float) maxValue1 / (float) byte.MaxValue;
      value.y = (float) maxValue2 / (float) byte.MaxValue;
      value.z = (float) maxValue3 / (float) byte.MaxValue;
      return CSVReader.PopResult.SUCCESS;
    }
    return popResult1 == CSVReader.PopResult.EMPTY && popResult2 == CSVReader.PopResult.EMPTY && popResult3 == CSVReader.PopResult.EMPTY ? CSVReader.PopResult.EMPTY : CSVReader.PopResult.UNKNOWN;
  }

  public CSVReader.PopResult PopColor24(ref Color32 value)
  {
    value = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
    CSVReader.PopResult popResult1 = this.Pop(ref value.r);
    CSVReader.PopResult popResult2 = this.Pop(ref value.g);
    CSVReader.PopResult popResult3 = this.Pop(ref value.b);
    if (popResult1 == CSVReader.PopResult.SUCCESS && popResult2 == CSVReader.PopResult.SUCCESS && popResult3 == CSVReader.PopResult.SUCCESS)
      return CSVReader.PopResult.SUCCESS;
    return popResult1 == CSVReader.PopResult.EMPTY && popResult2 == CSVReader.PopResult.EMPTY && popResult3 == CSVReader.PopResult.EMPTY ? CSVReader.PopResult.EMPTY : CSVReader.PopResult.UNKNOWN;
  }

  public CSVReader.PopResult PopColor24(ref int value)
  {
    CSVReader.PopResult popResult = CSVReader.PopResult.SUCCESS;
    int maxValue1 = (int) byte.MaxValue;
    int maxValue2 = (int) byte.MaxValue;
    int maxValue3 = (int) byte.MaxValue;
    if (!(bool) this.Pop(ref maxValue1))
      popResult = CSVReader.PopResult.UNKNOWN;
    if (!(bool) this.Pop(ref maxValue2))
      popResult = CSVReader.PopResult.UNKNOWN;
    if (!(bool) this.Pop(ref maxValue3))
      popResult = CSVReader.PopResult.UNKNOWN;
    value = (maxValue1 & (int) byte.MaxValue) << 24 | (maxValue2 & (int) byte.MaxValue) << 16 /*0x10*/ | (maxValue3 & (int) byte.MaxValue) << 8 | (int) byte.MaxValue;
    return popResult;
  }

  public int GetPosition() => this.pos;

  public class PopResult
  {
    private CSVReader.PopResult.Result _result;
    public static readonly CSVReader.PopResult EMPTY = new CSVReader.PopResult(CSVReader.PopResult.Result.EMPTY);
    public static readonly CSVReader.PopResult DONT_DEFINE = new CSVReader.PopResult(CSVReader.PopResult.Result.DONT_DEFINE_ERROR);
    public static readonly CSVReader.PopResult UNKNOWN = new CSVReader.PopResult(CSVReader.PopResult.Result.UNKNOWN);
    public static readonly CSVReader.PopResult SUCCESS = new CSVReader.PopResult(CSVReader.PopResult.Result.SUCCESS);
    public static readonly CSVReader.PopResult PARSE_ERROR = new CSVReader.PopResult(CSVReader.PopResult.Result.PARSE_ERROR);

    public static bool IsParseSucceeded(CSVReader.PopResult _result)
    {
      return (CSVReader.PopResult.Result) _result != CSVReader.PopResult.Result.DONT_DEFINE_ERROR && (CSVReader.PopResult.Result) _result != CSVReader.PopResult.Result.UNKNOWN;
    }

    public PopResult(CSVReader.PopResult.Result result) => this._result = result;

    public static bool operator ==(CSVReader.PopResult a, CSVReader.PopResult b)
    {
      if ((object) a == (object) b)
        return true;
      return (object) a != null && (object) b != null && a._result == b._result;
    }

    public static bool operator !=(CSVReader.PopResult a, CSVReader.PopResult b) => !(a == b);

    public static implicit operator bool(CSVReader.PopResult result)
    {
      return result._result == CSVReader.PopResult.Result.SUCCESS;
    }

    public static implicit operator CSVReader.PopResult.Result(CSVReader.PopResult result)
    {
      return result._result;
    }

    public override bool Equals(object o)
    {
      return o != null && !(this.GetType() != o.GetType()) && this._result == ((CSVReader.PopResult) o)._result;
    }

    public override int GetHashCode() => this._result.GetHashCode();

    public enum Result
    {
      SUCCESS,
      EMPTY,
      DONT_DEFINE_ERROR,
      UNKNOWN,
      PARSE_ERROR,
    }
  }
}
