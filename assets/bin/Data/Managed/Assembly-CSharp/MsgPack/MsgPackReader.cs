// Decompiled with JetBrains decompiler
// Type: MsgPack.MsgPackReader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;
using System.Text;

#nullable disable
namespace MsgPack;

public class MsgPackReader
{
  private Stream _strm;
  private byte[] _tmp0 = new byte[8];
  private byte[] _tmp1 = new byte[8];
  private Encoding _encoding = Encoding.UTF8;
  private byte[] _buf = new byte[64 /*0x40*/];

  public MsgPackReader(Stream strm) => this._strm = strm;

  public TypePrefixes Type { get; private set; }

  public bool ValueBoolean { get; private set; }

  public uint Length { get; private set; }

  public uint ValueUnsigned { get; private set; }

  public ulong ValueUnsigned64 { get; private set; }

  public int ValueSigned { get; private set; }

  public long ValueSigned64 { get; private set; }

  public float ValueFloat { get; private set; }

  public double ValueDouble { get; private set; }

  public sbyte ExtType { get; private set; }

  public bool IsSigned()
  {
    return this.Type == TypePrefixes.NegativeFixNum || this.Type == TypePrefixes.PositiveFixNum || this.Type == TypePrefixes.Int8 || this.Type == TypePrefixes.Int16 || this.Type == TypePrefixes.Int32;
  }

  public bool IsBoolean() => this.Type == TypePrefixes.True || this.Type == TypePrefixes.False;

  public bool IsSigned64() => this.Type == TypePrefixes.Int64;

  public bool IsUnsigned()
  {
    return this.Type == TypePrefixes.PositiveFixNum || this.Type == TypePrefixes.UInt8 || this.Type == TypePrefixes.UInt16 || this.Type == TypePrefixes.UInt32;
  }

  public bool IsUnsigned64() => this.Type == TypePrefixes.UInt64;

  public bool IsRaw()
  {
    return this.Type == TypePrefixes.FixRaw || this.Type == TypePrefixes.Raw8 || this.Type == TypePrefixes.Raw16 || this.Type == TypePrefixes.Raw32;
  }

  public bool IsBinary()
  {
    TypePrefixes type = this.Type;
    switch (type)
    {
      case TypePrefixes.Bin8:
      case TypePrefixes.Bin16:
        return true;
      default:
        return type == TypePrefixes.Bin32;
    }
  }

  public bool IsArray()
  {
    return this.Type == TypePrefixes.FixArray || this.Type == TypePrefixes.Array16 || this.Type == TypePrefixes.Array32;
  }

  public bool IsMap()
  {
    return this.Type == TypePrefixes.FixMap || this.Type == TypePrefixes.Map16 || this.Type == TypePrefixes.Map32;
  }

  public bool Read()
  {
    byte[] tmp0 = this._tmp0;
    int num1 = this._strm.ReadByte();
    if (num1 < 0)
      return false;
    this.Type = num1 < 0 || num1 > (int) sbyte.MaxValue ? (num1 < 224 /*0xE0*/ || num1 > (int) byte.MaxValue ? (num1 < 160 /*0xA0*/ || num1 > 191 ? (num1 < 144 /*0x90*/ || num1 > 159 ? (num1 < 128 /*0x80*/ || num1 > 143 ? (212 > num1 || num1 > 216 ? (TypePrefixes) num1 : TypePrefixes.FixExt) : TypePrefixes.FixMap) : TypePrefixes.FixArray) : TypePrefixes.FixRaw) : TypePrefixes.NegativeFixNum) : TypePrefixes.PositiveFixNum;
    TypePrefixes type = this.Type;
    if ((uint) type <= 128U /*0x80*/)
    {
      if (type != TypePrefixes.PositiveFixNum)
      {
        if (type != TypePrefixes.FixMap)
          goto label_53;
      }
      else
      {
        this.ValueSigned = num1 & (int) sbyte.MaxValue;
        this.ValueUnsigned = (uint) this.ValueSigned;
        goto label_54;
      }
    }
    else
    {
      switch (type)
      {
        case TypePrefixes.FixArray:
          break;
        case TypePrefixes.FixRaw:
          this.Length = (uint) (num1 & 31 /*0x1F*/);
          goto label_54;
        case TypePrefixes.Nil:
          goto label_54;
        case TypePrefixes.False:
          this.ValueBoolean = false;
          goto label_54;
        case TypePrefixes.True:
          this.ValueBoolean = true;
          goto label_54;
        case TypePrefixes.Bin8:
        case TypePrefixes.Ext8:
        case TypePrefixes.Raw8:
          this.Length = this._strm.Read(tmp0, 0, 1) == 1 ? (uint) tmp0[0] : throw new FormatException();
          goto label_54;
        case TypePrefixes.Bin16:
        case TypePrefixes.Ext16:
        case TypePrefixes.Raw16:
        case TypePrefixes.Array16:
        case TypePrefixes.Map16:
          if (this._strm.Read(tmp0, 0, 2) != 2)
            throw new FormatException();
          this.Length = (uint) tmp0[0] << 8 | (uint) tmp0[1];
          goto label_54;
        case TypePrefixes.Bin32:
        case TypePrefixes.Ext32:
        case TypePrefixes.Raw32:
        case TypePrefixes.Array32:
        case TypePrefixes.Map32:
          if (this._strm.Read(tmp0, 0, 4) != 4)
            throw new FormatException();
          this.Length = (uint) ((int) tmp0[0] << 24 | (int) tmp0[1] << 16 /*0x10*/ | (int) tmp0[2] << 8) | (uint) tmp0[3];
          goto label_54;
        case TypePrefixes.Float:
          double num2 = (double) this.ReadSingle();
          goto label_54;
        case TypePrefixes.Double:
          this.ReadDouble();
          goto label_54;
        case TypePrefixes.UInt8:
          int num3 = this._strm.ReadByte();
          this.ValueUnsigned = num3 >= 0 ? (uint) num3 : throw new FormatException();
          goto label_54;
        case TypePrefixes.UInt16:
          if (this._strm.Read(tmp0, 0, 2) != 2)
            throw new FormatException();
          this.ValueUnsigned = (uint) tmp0[0] << 8 | (uint) tmp0[1];
          goto label_54;
        case TypePrefixes.UInt32:
          if (this._strm.Read(tmp0, 0, 4) != 4)
            throw new FormatException();
          this.ValueUnsigned = (uint) ((int) tmp0[0] << 24 | (int) tmp0[1] << 16 /*0x10*/ | (int) tmp0[2] << 8) | (uint) tmp0[3];
          goto label_54;
        case TypePrefixes.UInt64:
          if (this._strm.Read(tmp0, 0, 8) != 8)
            throw new FormatException();
          this.ValueUnsigned64 = (ulong) ((long) tmp0[0] << 56 | (long) tmp0[1] << 48 /*0x30*/ | (long) tmp0[2] << 40 | (long) tmp0[3] << 32 /*0x20*/ | (long) tmp0[4] << 24 | (long) tmp0[5] << 16 /*0x10*/ | (long) tmp0[6] << 8) | (ulong) tmp0[7];
          goto label_54;
        case TypePrefixes.Int8:
          int num4 = this._strm.ReadByte();
          this.ValueSigned = num4 >= 0 ? (int) (sbyte) num4 : throw new FormatException();
          goto label_54;
        case TypePrefixes.Int16:
          if (this._strm.Read(tmp0, 0, 2) != 2)
            throw new FormatException();
          this.ValueSigned = (int) (short) ((int) tmp0[0] << 8 | (int) tmp0[1]);
          goto label_54;
        case TypePrefixes.Int32:
          if (this._strm.Read(tmp0, 0, 4) != 4)
            throw new FormatException();
          this.ValueSigned = (int) tmp0[0] << 24 | (int) tmp0[1] << 16 /*0x10*/ | (int) tmp0[2] << 8 | (int) tmp0[3];
          goto label_54;
        case TypePrefixes.Int64:
          if (this._strm.Read(tmp0, 0, 8) != 8)
            throw new FormatException();
          this.ValueSigned64 = (long) tmp0[0] << 56 | (long) tmp0[1] << 48 /*0x30*/ | (long) tmp0[2] << 40 | (long) tmp0[3] << 32 /*0x20*/ | (long) tmp0[4] << 24 | (long) tmp0[5] << 16 /*0x10*/ | (long) tmp0[6] << 8 | (long) tmp0[7];
          goto label_54;
        case TypePrefixes.FixExt:
          switch (num1)
          {
            case 212:
              this.Length = 1U;
              goto label_54;
            case 213:
              this.Length = 2U;
              goto label_54;
            case 214:
              this.Length = 4U;
              goto label_54;
            case 215:
              this.Length = 8U;
              goto label_54;
            case 216:
              this.Length = 16U /*0x10*/;
              goto label_54;
            default:
              goto label_54;
          }
        case TypePrefixes.NegativeFixNum:
          this.ValueSigned = (num1 & 31 /*0x1F*/) - 32 /*0x20*/;
          goto label_54;
        default:
          goto label_53;
      }
    }
    this.Length = (uint) (num1 & 15);
    goto label_54;
label_53:
    throw new FormatException();
label_54:
    return true;
  }

  public sbyte ReadExtType()
  {
    int num = this._strm.ReadByte();
    this.ExtType = num >= 0 ? (sbyte) num : throw new FormatException();
    return this.ExtType;
  }

  public int ReadValueRaw(byte[] buf, int offset, int count) => this._strm.Read(buf, offset, count);

  public string ReadRawString() => this.ReadRawString(this._buf);

  public string ReadRawString(byte[] buf)
  {
    if ((long) this.Length < (long) buf.Length)
    {
      if ((long) this.ReadValueRaw(buf, 0, (int) this.Length) != (long) this.Length)
        throw new FormatException();
      return this._encoding.GetString(buf, 0, (int) this.Length);
    }
    byte[] numArray = new byte[(int) this.Length];
    return this.ReadValueRaw(numArray, 0, numArray.Length) == numArray.Length ? this._encoding.GetString(numArray) : throw new FormatException();
  }

  public float ReadSingle()
  {
    byte[] tmp0 = this._tmp0;
    byte[] tmp1 = this._tmp1;
    this._strm.Read(tmp0, 0, 4);
    if (BitConverter.IsLittleEndian)
    {
      tmp1[0] = tmp0[3];
      tmp1[1] = tmp0[2];
      tmp1[2] = tmp0[1];
      tmp1[3] = tmp0[0];
      this.ValueFloat = BitConverter.ToSingle(tmp1, 0);
    }
    else
      this.ValueFloat = BitConverter.ToSingle(tmp0, 0);
    return this.ValueFloat;
  }

  public double ReadDouble()
  {
    byte[] tmp0 = this._tmp0;
    byte[] tmp1 = this._tmp1;
    this._strm.Read(tmp0, 0, 8);
    if (BitConverter.IsLittleEndian)
    {
      tmp1[0] = tmp0[7];
      tmp1[1] = tmp0[6];
      tmp1[2] = tmp0[5];
      tmp1[3] = tmp0[4];
      tmp1[4] = tmp0[3];
      tmp1[5] = tmp0[2];
      tmp1[6] = tmp0[1];
      tmp1[7] = tmp0[0];
      this.ValueDouble = BitConverter.ToDouble(tmp1, 0);
    }
    else
      this.ValueDouble = BitConverter.ToDouble(tmp0, 0);
    return this.ValueDouble;
  }
}
