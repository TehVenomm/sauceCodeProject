// Decompiled with JetBrains decompiler
// Type: BestHTTP.WebSocket.Frames.WebSocketFrameReader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace BestHTTP.WebSocket.Frames;

public sealed class WebSocketFrameReader
{
  public bool IsFinal { get; private set; }

  public WebSocketFrameTypes Type { get; private set; }

  public bool HasMask { get; private set; }

  public ulong Length { get; private set; }

  public byte[] Mask { get; private set; }

  public byte[] Data { get; private set; }

  internal void Read(Stream stream)
  {
    byte num1 = (byte) stream.ReadByte();
    this.IsFinal = ((uint) num1 & 128U /*0x80*/) > 0U;
    this.Type = (WebSocketFrameTypes) ((uint) num1 & 15U);
    byte num2 = (byte) stream.ReadByte();
    this.HasMask = ((uint) num2 & 128U /*0x80*/) > 0U;
    this.Length = (ulong) ((int) num2 & (int) sbyte.MaxValue);
    if (this.Length == 126UL)
    {
      byte[] buffer = new byte[2];
      stream.Read(buffer, 0, 2);
      if (BitConverter.IsLittleEndian)
        Array.Reverse((Array) buffer, 0, buffer.Length);
      this.Length = (ulong) BitConverter.ToUInt16(buffer, 0);
    }
    else if (this.Length == (ulong) sbyte.MaxValue)
    {
      byte[] buffer = new byte[8];
      stream.Read(buffer, 0, 8);
      if (BitConverter.IsLittleEndian)
        Array.Reverse((Array) buffer, 0, buffer.Length);
      this.Length = BitConverter.ToUInt64(buffer, 0);
    }
    if (this.HasMask)
    {
      this.Mask = new byte[4];
      stream.Read(this.Mask, 0, 4);
    }
    this.Data = new byte[this.Length];
    for (ulong index = 0; index < this.Length; ++index)
    {
      this.Data[index] = (byte) stream.ReadByte();
      if (this.HasMask)
        this.Data[index] = (byte) ((uint) this.Data[index] ^ (uint) this.Mask[index % 4UL]);
    }
  }

  internal void Assemble(List<WebSocketFrameReader> fragments)
  {
    fragments.Add(this);
    ulong length = 0;
    for (int index = 0; index < fragments.Count; ++index)
      length += fragments[index].Length;
    byte[] destinationArray = new byte[length];
    ulong destinationIndex = 0;
    for (int index = 0; index < fragments.Count; ++index)
    {
      Array.Copy((Array) fragments[index].Data, 0, (Array) destinationArray, (int) destinationIndex, (int) fragments[index].Length);
      destinationIndex += fragments[index].Length;
    }
    this.Type = fragments[0].Type;
    this.Length = length;
    this.Data = destinationArray;
  }
}
