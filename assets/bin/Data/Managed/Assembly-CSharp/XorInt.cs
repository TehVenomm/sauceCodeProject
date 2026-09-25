// Decompiled with JetBrains decompiler
// Type: XorInt
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Threading;

#nullable disable
public class XorInt
{
  private int key;
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

  public XorInt()
    : this(0)
  {
  }

  public XorInt(int value)
  {
    this.GenerateKey();
    this.rawValue = this.Xor(value);
  }

  private void GenerateKey()
  {
    Interlocked.Increment(ref XorInt.cnt);
    Random rnd = XorInt.s_rnds[XorInt.cnt % 5];
    lock (rnd)
      this.key = rnd.Next();
  }

  public int rawValue { get; private set; }

  public int value
  {
    get => this.Xor(this.rawValue);
    set => this.rawValue = this.Xor(value);
  }

  private int Xor(int x) => x ^ this.key;

  public static implicit operator int(XorInt xor) => xor == null ? 0 : xor.value;

  public static implicit operator XorInt(int val) => new XorInt(val);

  public override string ToString() => this.value.ToString();

  public string ToString(string format) => this.value.ToString(format);

  public string ToString(IFormatProvider provider) => this.value.ToString(provider);

  public string ToString(string format, IFormatProvider provider)
  {
    return this.value.ToString(format, provider);
  }
}
