// Decompiled with JetBrains decompiler
// Type: BestHTTP.WebSocket.Frames.WebSocketContinuationFrame
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace BestHTTP.WebSocket.Frames;

public sealed class WebSocketContinuationFrame : WebSocketBinaryFrame
{
  public override WebSocketFrameTypes Type => WebSocketFrameTypes.Continuation;

  public WebSocketContinuationFrame(byte[] data, bool isFinal)
    : base(data, 0UL, (ulong) data.Length, isFinal)
  {
  }

  public WebSocketContinuationFrame(byte[] data, ulong pos, ulong length, bool isFinal)
    : base(data, pos, length, isFinal)
  {
  }
}
