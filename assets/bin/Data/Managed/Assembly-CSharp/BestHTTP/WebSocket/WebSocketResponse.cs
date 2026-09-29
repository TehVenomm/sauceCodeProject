// Decompiled with JetBrains decompiler
// Type: BestHTTP.WebSocket.WebSocketResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using BestHTTP.WebSocket.Frames;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

#nullable disable
namespace BestHTTP.WebSocket;

public class WebSocketResponse : HTTPResponse
{
  private const int PingThreadFrequency = 100;
  public Action<WebSocketResponse, string> OnText;
  public Action<WebSocketResponse, byte[]> OnBinary;
  public Action<WebSocketResponse, byte[]> OnPong;
  public Action<WebSocketResponse, WebSocketFrameReader> OnIncompleteFrame;
  public Action<WebSocketResponse, ushort, string> OnClosed;
  private List<WebSocketFrameReader> IncompleteFrames = new List<WebSocketFrameReader>();
  private List<WebSocketFrameReader> CompletedFrames = new List<WebSocketFrameReader>();
  private WebSocketFrameReader CloseFrame;
  private Thread ReceiverThread;
  private Thread PingThread;
  private object FrameLock = new object();
  private object SendLock = new object();
  private bool closeSent;
  private bool closed;
  private int ClosedCount;
  private int MinClosedCount;

  public bool IsClosed => this.ClosedCount >= this.MinClosedCount;

  public int PingFrequnecy { get; private set; }

  internal WebSocketResponse(
    HTTPRequest request,
    Stream stream,
    bool isStreamed,
    bool isFromCache)
    : base(request, stream, isStreamed, isFromCache)
  {
    this.closed = false;
    this.ClosedCount = 0;
    this.MinClosedCount = 1;
  }

  internal override bool Receive(int forceReadRawContentLength = -1)
  {
    int num = base.Receive(forceReadRawContentLength) ? 1 : 0;
    if (num == 0)
      return num != 0;
    if (!this.IsUpgraded)
      return num != 0;
    this.ReceiverThread = new Thread(new ThreadStart(this.ReceiveThreadFunc));
    this.ReceiverThread.Name = "WebSocket Receiver Thread";
    this.ReceiverThread.IsBackground = true;
    this.ReceiverThread.Start();
    return num != 0;
  }

  public void Send(string message)
  {
    if (message == null)
      throw new ArgumentNullException("message must not be null!");
    this.Send((IWebSocketFrameWriter) new WebSocketTextFrame(message));
  }

  public void Send(byte[] data)
  {
    if (data == null)
      throw new ArgumentNullException("data must not be null!");
    ulong num = 32758;
    if ((long) data.Length > (long) num)
    {
      lock (this.SendLock)
      {
        this.Send((IWebSocketFrameWriter) new WebSocketBinaryFrame(data, 0UL, num, false));
        ulong length;
        for (ulong pos = num; pos < (ulong) data.Length; pos += length)
        {
          length = Math.Min(num, (ulong) data.Length - pos);
          this.Send((IWebSocketFrameWriter) new WebSocketContinuationFrame(data, pos, length, pos + length >= (ulong) data.Length));
        }
      }
    }
    else
      this.Send((IWebSocketFrameWriter) new WebSocketBinaryFrame(data));
  }

  public void Send(IWebSocketFrameWriter frame)
  {
    if (frame == null)
      throw new ArgumentNullException("frame is null!");
    if (this.closed)
      return;
    byte[] buffer = frame.Get();
    lock (this.SendLock)
      this.Stream.Write(buffer, 0, buffer.Length);
    if (frame.Type != WebSocketFrameTypes.ConnectionClose)
      return;
    this.closeSent = true;
  }

  public void Close() => this.Close((ushort) 1000, "Bye!");

  public void Close(ushort code, string msg)
  {
    if (this.closed)
      return;
    this.Send((IWebSocketFrameWriter) new WebSocketClose(code, msg));
  }

  public void StartPinging(int frequency)
  {
    this.PingFrequnecy = frequency >= 100 ? frequency : throw new ArgumentException("frequency must be at least 100 millisec!");
    this.MinClosedCount = 2;
    this.PingThread = new Thread(new ThreadStart(this.PingThreadFunc));
    this.PingThread.Name = "WebSocket Ping Thread";
    this.PingThread.IsBackground = true;
    this.PingThread.Start();
  }

  private void ReceiveThreadFunc()
  {
    while (!this.closed)
    {
      try
      {
        WebSocketFrameReader ping = new WebSocketFrameReader();
        ping.Read(this.Stream);
        if (ping.HasMask)
          this.Close((ushort) 1002, "Protocol Error: masked frame received from server!");
        else if (!ping.IsFinal)
        {
          if (this.OnIncompleteFrame == null)
          {
            this.IncompleteFrames.Add(ping);
          }
          else
          {
            lock (this.FrameLock)
              this.CompletedFrames.Add(ping);
          }
        }
        else
        {
          switch (ping.Type)
          {
            case WebSocketFrameTypes.Continuation:
              if (this.OnIncompleteFrame == null)
              {
                ping.Assemble(this.IncompleteFrames);
                this.IncompleteFrames.Clear();
                goto case WebSocketFrameTypes.Text;
              }
              lock (this.FrameLock)
              {
                this.CompletedFrames.Add(ping);
                continue;
              }
            case WebSocketFrameTypes.Text:
            case WebSocketFrameTypes.Binary:
              if (this.OnText != null)
              {
                lock (this.FrameLock)
                {
                  this.CompletedFrames.Add(ping);
                  continue;
                }
              }
              continue;
            case WebSocketFrameTypes.ConnectionClose:
              this.CloseFrame = ping;
              if (!this.closeSent)
                this.Send((IWebSocketFrameWriter) new WebSocketClose());
              this.closed = this.closeSent;
              continue;
            case WebSocketFrameTypes.Ping:
              if (!this.closeSent)
              {
                if (!this.closed)
                {
                  this.Send((IWebSocketFrameWriter) new WebSocketPong(ping));
                  continue;
                }
                continue;
              }
              continue;
            case WebSocketFrameTypes.Pong:
              if (this.OnPong != null)
              {
                lock (this.FrameLock)
                {
                  this.CompletedFrames.Add(ping);
                  continue;
                }
              }
              continue;
            default:
              continue;
          }
        }
      }
      catch (ThreadAbortException ex)
      {
        this.IncompleteFrames.Clear();
        this.closed = true;
      }
      catch (Exception ex)
      {
        this.baseRequest.Exception = ex;
        this.closed = true;
      }
    }
    Interlocked.Increment(ref this.ClosedCount);
    if (this.PingThread == null || !this.PingThread.Join(1000))
      return;
    this.PingThread.Abort();
  }

  private void PingThreadFunc()
  {
    int num = 0;
    while (!this.closed)
    {
      try
      {
        Thread.Sleep(100);
        num += 100;
        if (num >= this.PingFrequnecy)
        {
          this.Send((IWebSocketFrameWriter) new WebSocketPing(string.Empty));
          num = 0;
        }
      }
      catch (ThreadAbortException ex)
      {
        this.closed = true;
      }
      catch (Exception ex)
      {
        this.baseRequest.Exception = ex;
        this.closed = true;
      }
    }
    Interlocked.Increment(ref this.ClosedCount);
    if (!this.ReceiverThread.Join(1000))
      return;
    this.ReceiverThread.Abort();
  }

  internal void HandleEvents()
  {
    lock (this.FrameLock)
    {
      for (int index = 0; index < this.CompletedFrames.Count; ++index)
      {
        WebSocketFrameReader completedFrame = this.CompletedFrames[index];
        try
        {
          switch (completedFrame.Type)
          {
            case WebSocketFrameTypes.Continuation:
              if (this.OnIncompleteFrame != null)
              {
                this.OnIncompleteFrame(this, completedFrame);
                continue;
              }
              continue;
            case WebSocketFrameTypes.Text:
              if (completedFrame.IsFinal)
              {
                if (this.OnText != null)
                {
                  this.OnText(this, Encoding.UTF8.GetString(completedFrame.Data, 0, completedFrame.Data.Length));
                  continue;
                }
                continue;
              }
              goto case WebSocketFrameTypes.Continuation;
            case WebSocketFrameTypes.Binary:
              if (completedFrame.IsFinal)
              {
                if (this.OnBinary != null)
                {
                  this.OnBinary(this, completedFrame.Data);
                  continue;
                }
                continue;
              }
              goto case WebSocketFrameTypes.Continuation;
            case WebSocketFrameTypes.Pong:
              if (this.OnPong != null)
              {
                this.OnPong(this, completedFrame.Data);
                continue;
              }
              continue;
            default:
              continue;
          }
        }
        catch
        {
        }
      }
      this.CompletedFrames.Clear();
    }
    if (!this.IsClosed)
      return;
    if (this.OnClosed == null)
      return;
    try
    {
      ushort num = 0;
      string empty = string.Empty;
      if (this.CloseFrame != null && this.CloseFrame.Data != null && this.CloseFrame.Data.Length >= 2)
      {
        if (BitConverter.IsLittleEndian)
          Array.Reverse((Array) this.CloseFrame.Data, 0, 2);
        num = BitConverter.ToUInt16(this.CloseFrame.Data, 0);
        if (this.CloseFrame.Data.Length > 2)
          empty = Encoding.UTF8.GetString(this.CloseFrame.Data, 2, this.CloseFrame.Data.Length - 2);
      }
      this.OnClosed(this, num, empty);
    }
    catch
    {
    }
  }
}
