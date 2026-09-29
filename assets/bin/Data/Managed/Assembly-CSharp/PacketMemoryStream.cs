// Decompiled with JetBrains decompiler
// Type: PacketMemoryStream
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;

#nullable disable
public class PacketMemoryStream : MemoryStream
{
  public PacketMemoryStream()
  {
  }

  public PacketMemoryStream(byte[] data)
    : base(data)
  {
  }

  private byte[] GetBytes(int val)
  {
    byte[] bytes = BitConverter.GetBytes(val);
    if (BitConverter.IsLittleEndian)
      Array.Reverse((Array) bytes);
    return bytes;
  }

  private int ToInt32(byte[] bytes)
  {
    if (BitConverter.IsLittleEndian)
      Array.Reverse((Array) bytes);
    return BitConverter.ToInt32(bytes, 0);
  }

  public void WriteInt32(int val)
  {
    byte[] bytes = this.GetBytes(val);
    this.Write(bytes, 0, bytes.Length);
  }

  public void WriteBytes(byte[] bytes) => this.Write(bytes, 0, bytes.Length);

  public int ReadInt32()
  {
    byte[] bytes = this.GetBytes(0);
    this.Read(bytes, 0, bytes.Length);
    return this.ToInt32(bytes);
  }

  public byte[] ReadBytes(int len)
  {
    byte[] buffer = new byte[len];
    this.Read(buffer, 0, buffer.Length);
    return buffer;
  }

  public override string ToString()
  {
    return $"{this.Length}/{this.Capacity}({this.Position}): {BitConverter.ToString(this.ToArray())}";
  }
}
