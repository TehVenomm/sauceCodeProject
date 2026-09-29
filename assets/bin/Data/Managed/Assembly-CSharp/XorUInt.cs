// Decompiled with JetBrains decompiler
// Type: XorUInt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Threading;

#nullable disable
public class XorUInt
{
  private uint key;
  private const int gens = 5;
  private static Random[] s_rnds = new Random[5]
  {
    new Random(),
    new Random(),
    new Random(),
    new Random(),
    new Random()
  };
  private static int cnt = 0;

  public XorUInt()
    : this(0U)
  {
  }

  public XorUInt(uint value)
  {
    this.GenerateKey();
    this.rawValue = this.Xor(value);
  }

  private void GenerateKey()
  {
    Interlocked.Increment(ref XorUInt.cnt);
    Random rnd = XorUInt.s_rnds[XorUInt.cnt % 5];
    lock (rnd)
      this.key = (uint) rnd.Next();
  }

  public uint rawValue { get; private set; }

  public uint value
  {
    get => this.Xor(this.rawValue);
    set => this.rawValue = this.Xor(value);
  }

  private uint Xor(uint x) => x ^ this.key;

  public static implicit operator uint(XorUInt xor) => xor == null ? 0U : xor.value;

  public static explicit operator int(XorUInt xor) => xor == null ? 0 : (int) xor.value;

  public static implicit operator XorUInt(uint val) => new XorUInt(val);

  public override string ToString() => this.value.ToString();

  public string ToString(string format) => this.value.ToString(format);

  public string ToString(IFormatProvider provider) => this.value.ToString(provider);

  public string ToString(string format, IFormatProvider provider)
  {
    return this.value.ToString(format, provider);
  }
}
