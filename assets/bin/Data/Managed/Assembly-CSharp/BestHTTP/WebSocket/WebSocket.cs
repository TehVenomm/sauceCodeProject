// Decompiled with JetBrains decompiler
// Type: BestHTTP.WebSocket.WebSocket
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using BestHTTP.WebSocket.Frames;
using System;

#nullable disable
namespace BestHTTP.WebSocket;

public sealed class WebSocket
{
  public Action<BestHTTP.WebSocket.WebSocket> OnOpen;
  public Action<BestHTTP.WebSocket.WebSocket, string> OnMessage;
  public Action<BestHTTP.WebSocket.WebSocket, byte[]> OnBinary;
  public Action<BestHTTP.WebSocket.WebSocket, byte[]> OnPong;
  public Action<BestHTTP.WebSocket.WebSocket, ushort, string> OnClosed;
  public Action<BestHTTP.WebSocket.WebSocket, Exception> OnError;
  public Action<BestHTTP.WebSocket.WebSocket, WebSocketFrameReader> OnIncompleteFrame;
  private bool requestSent;
  private WebSocketResponse webSocket;

  public HTTPRequest InternalRequest { get; private set; }

  public bool IsOpen => this.webSocket != null && !this.webSocket.IsClosed;

  public bool StartPingThread { get; set; }

  public int PingFrequency { get; set; }

  public WebSocket(Uri uri)
    : this(uri, string.Empty, string.Empty)
  {
  }

  public WebSocket(Uri uri, string protocol)
    : this(uri, protocol, string.Empty)
  {
  }

  public WebSocket(Uri uri, string origin, string protocol = "")
  {
    this.PingFrequency = 1000;
    if (uri.Port == -1)
      uri = new Uri($"{uri.Scheme}://{uri.Host}:{(uri.Scheme.Equals("wss", StringComparison.OrdinalIgnoreCase) ? "443" : "80")}{uri.PathAndQuery}");
    this.InternalRequest = new HTTPRequest(uri, (Action<HTTPRequest, HTTPResponse>) ((req, resp) =>
    {
      if (resp != null && req.Exception == null || this.OnError == null)
        return;
      this.OnError(this, req.Exception);
    }));
    this.InternalRequest.SetHeader("Host", $"{uri.Host}:{(object) uri.Port}");
    this.InternalRequest.SetHeader("Upgrade", "websocket");
    this.InternalRequest.SetHeader("Connection", "keep-alive, Upgrade");
    this.InternalRequest.SetHeader("Sec-WebSocket-Key", this.GetSecKey(new object[4]
    {
      (object) this,
      (object) this.InternalRequest,
      (object) uri,
      new object()
    }));
    if (!string.IsNullOrEmpty(origin))
      this.InternalRequest.SetHeader("Origin", origin);
    this.InternalRequest.SetHeader("Sec-WebSocket-Version", "13");
    if (!string.IsNullOrEmpty(protocol))
      this.InternalRequest.SetHeader("Sec-WebSocket-Protocol", protocol);
    this.InternalRequest.SetHeader("Cache-Control", "no-cache");
    this.InternalRequest.SetHeader("Pragma", "no-cache");
    this.InternalRequest.OnUpgraded = (Action<HTTPRequest, HTTPResponse>) ((req, resp) =>
    {
      this.webSocket = resp as WebSocketResponse;
      if (this.webSocket == null)
      {
        if (this.OnError == null)
          return;
        this.OnError(this, req.Exception);
      }
      else
      {
        if (this.OnOpen != null)
          this.OnOpen(this);
        this.webSocket.OnText = (Action<WebSocketResponse, string>) ((ws, msg) =>
        {
          if (this.OnMessage == null)
            return;
          this.OnMessage(this, msg);
        });
        this.webSocket.OnBinary = (Action<WebSocketResponse, byte[]>) ((ws, bin) =>
        {
          if (this.OnBinary == null)
            return;
          this.OnBinary(this, bin);
        });
        this.webSocket.OnClosed = (Action<WebSocketResponse, ushort, string>) ((ws, code, msg) =>
        {
          if (this.OnClosed == null)
            return;
          this.OnClosed(this, code, msg);
        });
        if (this.OnPong != null)
          this.webSocket.OnPong = (Action<WebSocketResponse, byte[]>) ((ws, bin) =>
          {
            if (this.OnPong == null)
              return;
            this.OnPong(this, bin);
          });
        if (this.OnIncompleteFrame != null)
          this.webSocket.OnIncompleteFrame = (Action<WebSocketResponse, WebSocketFrameReader>) ((ws, frame) =>
          {
            if (this.OnIncompleteFrame == null)
              return;
            this.OnIncompleteFrame(this, frame);
          });
        if (!this.StartPingThread)
          return;
        this.webSocket.StartPinging(Math.Max(this.PingFrequency, 100));
      }
    });
  }

  public void Open()
  {
    if (this.requestSent || this.InternalRequest == null)
      return;
    this.InternalRequest.Send();
    this.requestSent = true;
  }

  public void Send(string message)
  {
    if (!this.IsOpen)
      return;
    this.webSocket.Send(message);
  }

  public void Send(byte[] buffer)
  {
    if (!this.IsOpen)
      return;
    this.webSocket.Send(buffer);
  }

  public void Send(IWebSocketFrameWriter frame)
  {
    if (!this.IsOpen)
      return;
    this.webSocket.Send(frame);
  }

  public void Close()
  {
    if (!this.IsOpen)
      return;
    this.webSocket.Close();
  }

  public void Close(ushort code, string message)
  {
    if (!this.IsOpen)
      return;
    this.webSocket.Close(code, message);
  }

  private string GetSecKey(object[] from)
  {
    byte[] inArray = new byte[16 /*0x10*/];
    int num = 0;
    for (int index1 = 0; index1 < from.Length; ++index1)
    {
      byte[] bytes = BitConverter.GetBytes(from[index1].GetHashCode());
      for (int index2 = 0; index2 < bytes.Length && num < inArray.Length; ++index2)
        inArray[num++] = bytes[index2];
    }
    return Convert.ToBase64String(inArray);
  }
}
