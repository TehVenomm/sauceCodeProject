// Decompiled with JetBrains decompiler
// Type: BestHTTP.WebSocket.Frames.WebSocketPing
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Text;

#nullable disable
namespace BestHTTP.WebSocket.Frames;

public sealed class WebSocketPing(string msg) : WebSocketBinaryFrame(Encoding.UTF8.GetBytes(msg))
{
  public override WebSocketFrameTypes Type => WebSocketFrameTypes.Ping;
}
