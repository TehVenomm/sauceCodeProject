// Decompiled with JetBrains decompiler
// Type: BestHTTP.WebSocket.Frames.WebSocketClose
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.IO;
using System.Text;

#nullable disable
namespace BestHTTP.WebSocket.Frames;

public sealed class WebSocketClose : WebSocketBinaryFrame
{
  public override WebSocketFrameTypes Type => WebSocketFrameTypes.ConnectionClose;

  public WebSocketClose()
    : base((byte[]) null)
  {
  }

  public WebSocketClose(ushort code, string message)
    : base(WebSocketClose.GetData(code, message))
  {
  }

  private static byte[] GetData(ushort code, string message)
  {
    using (MemoryStream memoryStream = new MemoryStream(2 + Encoding.UTF8.GetByteCount(message)))
    {
      byte[] bytes1 = BitConverter.GetBytes(code);
      if (BitConverter.IsLittleEndian)
        Array.Reverse((Array) bytes1, 0, bytes1.Length);
      memoryStream.Write(bytes1, 0, bytes1.Length);
      byte[] bytes2 = Encoding.UTF8.GetBytes(message);
      memoryStream.Write(bytes2, 0, bytes2.Length);
      return memoryStream.ToArray();
    }
  }
}
