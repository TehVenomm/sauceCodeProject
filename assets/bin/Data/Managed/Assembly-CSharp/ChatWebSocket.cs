// Decompiled with JetBrains decompiler
// Type: ChatWebSocket
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ChatWebSocket : MonoBehaviourSingleton<ChatWebSocket>
{
  public const int SERVER_ID = -1000;
  public const int BROADCAST_ID = -2000;
  public const int PROTCOL_VER = 0;
  public const string SERVER_TOKEN = "###########";
  public const string BROADCAST_TOKEN = "@@@@@@@@@@@";
  public const string PROTOCOL_VERSION = "00";
  private BestHTTP.WebSocket.WebSocket sock;
  private bool isConnect;
  public ChatWebSocket.CONNECTION_STATUS CurrentConnectionStatus;
  public Action<ChatPacket> ReceivePacketAction;
  private Queue<PacketStream> temporaryQueue = new Queue<PacketStream>();
  [SerializeField]
  private string _relayServer;
  private string _fromId;
  [SerializeField]
  private int _ackPrefix;
  [SerializeField]
  private int _packetSendCount;
  private const int SEQUENCE_MAX = 10000000;
  public const float HEARTBEAT_TIMEOUT = 10f;
  public const float HEARTBEAT_INTERVAL = 3f;
  private DateTime lastPacketReceivedTime;

  public event EventHandler ErrorOccurred;

  public string relayServer
  {
    get => this._relayServer;
    private set => this._relayServer = value;
  }

  public string fromId
  {
    get => this._fromId;
    private set => this._fromId = value;
  }

  public int ackPrefix
  {
    get => this._ackPrefix;
    private set => this._ackPrefix = value;
  }

  public int packetSendCount
  {
    get => this._packetSendCount;
    private set => this._packetSendCount = value;
  }

  public int sequence { get; private set; }

  public event System.Action OnClosed;

  public event Action<double> OnPong;

  public void Setup()
  {
  }

  public void Connect() => this.Connect(this.relayServer, this.fromId, this.ackPrefix);

  public void Connect(string path, string from_id, int ack_prefix)
  {
    this.temporaryQueue.Clear();
    this.relayServer = path;
    this.fromId = from_id;
    this.ackPrefix = ack_prefix;
    this.NativeConnect(this.relayServer);
  }

  private void NativeConnect(string relayServer)
  {
    this.sock = new BestHTTP.WebSocket.WebSocket(new Uri(relayServer));
    this.CurrentConnectionStatus = ChatWebSocket.CONNECTION_STATUS.OPENING;
    this.sock.OnOpen += (Action<BestHTTP.WebSocket.WebSocket>) (ws =>
    {
      this.LogDebug("OnOpen {0}", (object) ws.InternalRequest.Uri);
      this.ClearLastPacketReceivedTime();
      this.isConnect = true;
      this.CurrentConnectionStatus = ChatWebSocket.CONNECTION_STATUS.CONNECTED;
    });
    this.sock.OnBinary += (Action<BestHTTP.WebSocket.WebSocket, byte[]>) ((ws, binary) =>
    {
      this.LogDebug("OnBinary {0}", (object) binary.Length);
      this.ClearLastPacketReceivedTime();
      this.temporaryQueue.Enqueue(new PacketStream((object) binary));
    });
    this.sock.OnMessage += (Action<BestHTTP.WebSocket.WebSocket, string>) ((ws, message) =>
    {
      this.LogDebug("OnMessage {0}", (object) message);
      this.ClearLastPacketReceivedTime();
      this.temporaryQueue.Enqueue(new PacketStream((object) message));
    });
    this.sock.OnClosed += (Action<BestHTTP.WebSocket.WebSocket, ushort, string>) ((ws, code, message) =>
    {
      this.OnPrepareClose();
      this.temporaryQueue.Clear();
      if (this.OnClosed != null)
        this.OnClosed();
      this.LogDebug("OnClosed Code {0}", (object) code);
      this.LogDebug("OnClosed Message {0}", (object) message);
    });
    this.sock.OnError += (Action<BestHTTP.WebSocket.WebSocket, Exception>) ((ws, ex) =>
    {
      this.CurrentConnectionStatus = ChatWebSocket.CONNECTION_STATUS.ERROR;
      this.OnErrorOccurred(EventArgs.Empty, ex);
      this.LogDebug("OnError Message {0}", (object) ex.Message);
      this.LogDebug("OnError StackTrace {0}", (object) ex.StackTrace);
    });
    this.sock.OnPong += (Action<BestHTTP.WebSocket.WebSocket, byte[]>) ((ws, data) =>
    {
      if (this.OnPong != null)
        this.OnPong((DateTime.Now - this.lastPacketReceivedTime).TotalMilliseconds - 3000.0);
      this.ClearLastPacketReceivedTime();
    });
    this.sock.StartPingThread = true;
    this.sock.PingFrequency = 3000;
    this.sock.Open();
  }

  public void Close(ushort code = 1000, string msg = "Bye!")
  {
    this.OnPrepareClose();
    this.sock.Close(code, msg);
  }

  private void OnPrepareClose()
  {
    this.ReceivePacketAction = (Action<ChatPacket>) null;
    this.isConnect = false;
    this.CurrentConnectionStatus = ChatWebSocket.CONNECTION_STATUS.CLOSED;
  }

  protected virtual void OnErrorOccurred(EventArgs e, Exception ex)
  {
    if (this.ErrorOccurred == null)
      return;
    this.ErrorOccurred((object) this, e);
  }

  public int Send<T>(T model, int to_id, bool promise = true) where T : Chat_Model_Base
  {
    return this.Send((Chat_Model_Base) model, typeof (T), to_id, promise);
  }

  public int Send(
    Chat_Model_Base model,
    System.Type type,
    int to_id,
    bool promise = true,
    Func<Coop_Model_ACK, bool> onReceiveAck = null,
    Func<Coop_Model_Base, bool> onPreResend = null)
  {
    PacketStream stream = new ChatPacket()
    {
      header = new ChatPacketHeader(0, model.commandId, this.fromId),
      model = model
    }.Serialize();
    int commandId = model.commandId;
    this.NativeSend(stream);
    return 0;
  }

  private void NativeSend(PacketStream stream)
  {
    if (stream.IsBuffer())
    {
      this.sock.Send(stream.ToBuffer());
    }
    else
    {
      if (!stream.IsString())
        return;
      this.sock.Send(stream.ToString());
    }
  }

  private void ReceivePacket(PacketStream stream)
  {
    if (stream == null || stream.Length <= 0)
      return;
    ChatPacket chatPacket = ChatPacket.Deserialize(stream);
    if (chatPacket == null || this.ReceivePacketAction == null)
      return;
    this.ReceivePacketAction(chatPacket);
  }

  public void ClearLastPacketReceivedTime() => this.lastPacketReceivedTime = DateTime.Now;

  public bool IsConnected() => this.isConnect;

  public bool IsOpen() => this.sock != null && this.sock.IsOpen;

  protected new void Awake()
  {
  }

  private void Update()
  {
    while (this.IsConnected() && this.temporaryQueue.Count > 0)
      this.ReceivePacket(this.temporaryQueue.Dequeue());
  }

  public void LogDebug(string message, params object[] args)
  {
  }

  public enum CONNECTION_STATUS
  {
    NONE,
    CONNECTED,
    OPENING,
    CLOSED,
    ERROR,
  }
}
