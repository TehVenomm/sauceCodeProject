// Decompiled with JetBrains decompiler
// Type: MD5Hash
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

#nullable disable
public class MD5Hash : IDataTableRequestHash
{
  public static readonly MD5Hash invalidHash = new MD5Hash();
  private static MD5 md5Calc = MD5.Create();
  private uint u32_0;
  private uint u32_1;
  private uint u32_2;
  private uint u32_3;

  public bool isValid
  {
    get => this.u32_0 != 0U || this.u32_1 != 0U || this.u32_2 != 0U || this.u32_3 > 0U;
  }

  public MD5Hash(uint u32_0, uint u32_1, uint u32_2, uint u32_3)
  {
    this.u32_0 = u32_0;
    this.u32_1 = u32_1;
    this.u32_2 = u32_2;
    this.u32_3 = u32_3;
  }

  public MD5Hash()
  {
  }

  private MD5Hash(byte[] hash)
  {
    this.u32_0 |= (uint) hash[0] << 24;
    this.u32_0 |= (uint) hash[1] << 16 /*0x10*/;
    this.u32_0 |= (uint) hash[2] << 8;
    this.u32_0 |= (uint) hash[3];
    this.u32_1 |= (uint) hash[4] << 24;
    this.u32_1 |= (uint) hash[5] << 16 /*0x10*/;
    this.u32_1 |= (uint) hash[6] << 8;
    this.u32_1 |= (uint) hash[7];
    this.u32_2 |= (uint) hash[8] << 24;
    this.u32_2 |= (uint) hash[9] << 16 /*0x10*/;
    this.u32_2 |= (uint) hash[10] << 8;
    this.u32_2 |= (uint) hash[11];
    this.u32_3 |= (uint) hash[12] << 24;
    this.u32_3 |= (uint) hash[13] << 16 /*0x10*/;
    this.u32_3 |= (uint) hash[14] << 8;
    this.u32_3 |= (uint) hash[15];
  }

  public uint this[int key]
  {
    get
    {
      switch (key)
      {
        case 0:
          return this.u32_0;
        case 1:
          return this.u32_1;
        case 2:
          return this.u32_2;
        case 3:
          return this.u32_3;
        default:
          throw new KeyNotFoundException();
      }
    }
    private set
    {
      switch (key)
      {
        case 0:
          this.u32_0 = value;
          break;
        case 1:
          this.u32_1 = value;
          break;
        case 2:
          this.u32_2 = value;
          break;
        case 3:
          this.u32_3 = value;
          break;
        default:
          throw new KeyNotFoundException();
      }
    }
  }

  public static MD5Hash Parse(string hashString)
  {
    MD5Hash md5Hash = new MD5Hash();
    for (int key = 0; key < 4; ++key)
    {
      for (int index = 0; index < 4; ++index)
      {
        byte num = Convert.ToByte(hashString.Substring(index * 2 + key * 8, 2), 16 /*0x10*/);
        md5Hash[key] |= (uint) num << 24 - index * 8;
      }
    }
    return md5Hash;
  }

  public static MD5Hash Calc(byte[] data)
  {
    MD5Hash.md5Calc.Initialize();
    return new MD5Hash(MD5Hash.md5Calc.ComputeHash(data));
  }

  public static MD5Hash Calc(string s)
  {
    MD5Hash.md5Calc.Initialize();
    byte[] bytes = Encoding.UTF8.GetBytes(s);
    return new MD5Hash(MD5Hash.md5Calc.ComputeHash(bytes));
  }

  public override string ToString()
  {
    return this.u32_0.ToString("x8") + this.u32_1.ToString("x8") + this.u32_2.ToString("x8") + this.u32_3.ToString("x8");
  }

  public override bool Equals(object obj)
  {
    MD5Hash md5Hash = obj as MD5Hash;
    return !(md5Hash == (MD5Hash) null) && this == md5Hash;
  }

  public override int GetHashCode()
  {
    return (int) this.u32_0 ^ (int) this.u32_1 ^ (int) this.u32_2 ^ (int) this.u32_3;
  }

  public uint GetUIntHashCode() => this.u32_0 ^ this.u32_1 ^ this.u32_2 ^ this.u32_3;

  public static bool operator ==(MD5Hash a, MD5Hash b)
  {
    return (int) a.u32_0 == (int) b.u32_0 && (int) a.u32_1 == (int) b.u32_1 && (int) a.u32_2 == (int) b.u32_2 && (int) a.u32_3 == (int) b.u32_3;
  }

  public static bool operator !=(MD5Hash a, MD5Hash b)
  {
    return (int) a.u32_0 != (int) b.u32_0 || (int) a.u32_1 != (int) b.u32_1 || (int) a.u32_2 != (int) b.u32_2 || (int) a.u32_3 == (int) b.u32_3;
  }
}
