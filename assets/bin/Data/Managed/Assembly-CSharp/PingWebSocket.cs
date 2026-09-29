// Decompiled with JetBrains decompiler
// Type: PingWebSocket
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class PingWebSocket : MonoBehaviour
{
  private const float HEARTBEAT_TIMEOUT = 5f;
  private const float HEARTBEAT_INTERVAL = 1f;
  private BestHTTP.WebSocket.WebSocket sock;
  private DateTime lastPacketReceivedTime;
  private bool isConnect;

  public event System.Action OnOpen;

  public event System.Action OnClosed;

  public event System.Action OnError;

  public event Action<double> OnPong;

  public void Connect(string relayServer)
  {
    this.sock = new BestHTTP.WebSocket.WebSocket(new Uri(relayServer));
    this.sock.OnOpen += (Action<BestHTTP.WebSocket.WebSocket>) (ws =>
    {
      this.lastPacketReceivedTime = DateTime.Now;
      this.isConnect = true;
      if (this.OnOpen == null)
        return;
      this.OnOpen();
    });
    this.sock.OnClosed += (Action<BestHTTP.WebSocket.WebSocket, ushort, string>) ((ws, code, message) =>
    {
      this.isConnect = false;
      if (this.OnClosed == null)
        return;
      this.OnClosed();
    });
    this.sock.OnError += (Action<BestHTTP.WebSocket.WebSocket, Exception>) ((ws, ex) =>
    {
      if (this.OnError == null)
        return;
      this.OnError();
    });
    this.sock.OnPong += (Action<BestHTTP.WebSocket.WebSocket, byte[]>) ((ws, data) =>
    {
      if (this.OnPong != null)
        this.OnPong((DateTime.Now - this.lastPacketReceivedTime).TotalMilliseconds - 1000.0);
      this.lastPacketReceivedTime = DateTime.Now;
    });
    this.sock.StartPingThread = true;
    this.sock.PingFrequency = 1000;
    this.sock.Open();
  }

  public void Close(ushort code = 1000, string msg = "Bye!")
  {
    if (this.sock == null)
      return;
    this.sock.Close(code, msg);
  }

  public bool IsConnected() => this.isConnect;
}
