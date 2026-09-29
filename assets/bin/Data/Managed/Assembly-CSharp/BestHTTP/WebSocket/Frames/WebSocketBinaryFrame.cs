// Decompiled with JetBrains decompiler
// Type: BestHTTP.WebSocket.Frames.WebSocketBinaryFrame
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;

#nullable disable
namespace BestHTTP.WebSocket.Frames;

public class WebSocketBinaryFrame : IWebSocketFrameWriter
{
  public virtual WebSocketFrameTypes Type => WebSocketFrameTypes.Binary;

  public bool IsFinal { get; protected set; }

  protected byte[] Data { get; set; }

  protected ulong Pos { get; set; }

  protected ulong Length { get; set; }

  public WebSocketBinaryFrame(byte[] data)
    : this(data, 0UL, data != null ? (ulong) data.Length : 0UL, true)
  {
  }

  public WebSocketBinaryFrame(byte[] data, bool isFinal)
    : this(data, 0UL, data != null ? (ulong) data.Length : 0UL, isFinal)
  {
  }

  public WebSocketBinaryFrame(byte[] data, ulong pos, ulong length, bool isFinal)
  {
    this.Data = data;
    this.Pos = pos;
    this.Length = length;
    this.IsFinal = isFinal;
  }

  public virtual byte[] Get()
  {
    if (this.Data == null)
      this.Data = new byte[0];
    using (MemoryStream memoryStream = new MemoryStream((int) this.Length + 9))
    {
      byte num = this.IsFinal ? (byte) 128 /*0x80*/ : (byte) 0;
      memoryStream.WriteByte((byte) ((WebSocketFrameTypes) num | this.Type));
      if (this.Length < 126UL)
        memoryStream.WriteByte((byte) (128U /*0x80*/ | (uint) (byte) this.Length));
      else if (this.Length < (ulong) ushort.MaxValue)
      {
        memoryStream.WriteByte((byte) 254);
        byte[] bytes = BitConverter.GetBytes((ushort) this.Length);
        if (BitConverter.IsLittleEndian)
          Array.Reverse((Array) bytes, 0, bytes.Length);
        memoryStream.Write(bytes, 0, bytes.Length);
      }
      else
      {
        memoryStream.WriteByte(byte.MaxValue);
        byte[] bytes = BitConverter.GetBytes(this.Length);
        if (BitConverter.IsLittleEndian)
          Array.Reverse((Array) bytes, 0, bytes.Length);
        memoryStream.Write(bytes, 0, bytes.Length);
      }
      byte[] bytes1 = BitConverter.GetBytes(this.GetHashCode());
      memoryStream.Write(bytes1, 0, bytes1.Length);
      for (ulong pos = this.Pos; pos < this.Pos + this.Length; ++pos)
        memoryStream.WriteByte((byte) ((uint) this.Data[pos] ^ (uint) bytes1[pos % 4UL]));
      return memoryStream.ToArray();
    }
  }
}
