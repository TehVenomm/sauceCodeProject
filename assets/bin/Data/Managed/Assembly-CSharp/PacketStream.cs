// Decompiled with JetBrains decompiler
// Type: PacketStream
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Text;

#nullable disable
public class PacketStream
{
  private object stream;
  private PacketStream.TYPE type;

  public PacketStream(object stream)
  {
    this.stream = stream;
    System.Type type = stream.GetType();
    if (type == typeof (byte[]))
    {
      this.type = PacketStream.TYPE.BUFFER;
    }
    else
    {
      if (!(type == typeof (string)))
        return;
      this.type = PacketStream.TYPE.STRING;
    }
  }

  public int Length
  {
    get
    {
      switch (this.type)
      {
        case PacketStream.TYPE.BUFFER:
          return ((byte[]) this.stream).Length;
        case PacketStream.TYPE.STRING:
          return ((string) this.stream).Length;
        default:
          return 0;
      }
    }
  }

  public bool IsBuffer() => this.type == PacketStream.TYPE.BUFFER;

  public bool IsString() => this.type == PacketStream.TYPE.STRING;

  public byte[] ToBuffer()
  {
    switch (this.type)
    {
      case PacketStream.TYPE.BUFFER:
        return (byte[]) this.stream;
      case PacketStream.TYPE.STRING:
        return Encoding.ASCII.GetBytes((string) this.stream);
      default:
        return new byte[0];
    }
  }

  public override string ToString()
  {
    switch (this.type)
    {
      case PacketStream.TYPE.BUFFER:
        return BitConverter.ToString((byte[]) this.stream);
      case PacketStream.TYPE.STRING:
        return (string) this.stream;
      default:
        return string.Empty;
    }
  }

  private enum TYPE
  {
    NONE,
    BUFFER,
    STRING,
  }
}
