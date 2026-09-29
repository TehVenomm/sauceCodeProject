// Decompiled with JetBrains decompiler
// Type: XorFloat
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Threading;

#nullable disable
public class XorFloat
{
  private byte[] key;
  private byte[] rawValue = new byte[4];
  private byte[] tmpValue = new byte[4];
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

  public XorFloat()
    : this(0.0f)
  {
  }

  public XorFloat(float value)
  {
    this.GenerateKey();
    this.XorAndSet(BitConverter.GetBytes(value), this.rawValue);
  }

  private void GenerateKey()
  {
    byte[] buffer = new byte[4];
    Interlocked.Increment(ref XorFloat.cnt);
    Random rnd = XorFloat.s_rnds[XorFloat.cnt % 5];
    lock (rnd)
      rnd.NextBytes(buffer);
    this.key = buffer;
  }

  public float value
  {
    get => BitConverter.ToSingle(this.XorAndSet(this.rawValue, this.tmpValue), 0);
    set => this.rawValue = this.XorAndSet(BitConverter.GetBytes(value), this.rawValue);
  }

  private byte[] XorAndSet(byte[] buf, byte[] outBuf)
  {
    for (int index = 0; index < 4; ++index)
      outBuf[index] = (byte) ((uint) buf[index] ^ (uint) this.key[index]);
    return outBuf;
  }

  public static implicit operator float(XorFloat xor) => xor == null ? 0.0f : xor.value;

  public static implicit operator XorFloat(float val) => new XorFloat(val);

  public override string ToString() => this.value.ToString();

  public string ToString(string format) => this.value.ToString(format);

  public string ToString(IFormatProvider provider) => this.value.ToString(provider);

  public string ToString(string format, IFormatProvider provider)
  {
    return this.value.ToString(format, provider);
  }

  public static XorFloat operator ++(XorFloat value) => (XorFloat) ((float) value + 1f);
}
