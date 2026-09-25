// Decompiled with JetBrains decompiler
// Type: BestHTTP.WebSocket.Frames.WebSocketFrameTypes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace BestHTTP.WebSocket.Frames;

public enum WebSocketFrameTypes : byte
{
  Continuation = 0,
  Text = 1,
  Binary = 2,
  ConnectionClose = 8,
  Ping = 9,
  Pong = 10, // 0x0A
}
