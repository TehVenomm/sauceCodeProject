// Decompiled with JetBrains decompiler
// Type: CoopWebSocketSingleton`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class CoopWebSocketSingleton<U> : MonoBehaviourSingleton<U> where U : CoopWebSocketSingleton<U>
{
  public const int SERVER_ID = -1000;
  public const int BROADCAST_ID = -2000;
  public const string SERVER_TOKEN = "";
  public const string BROADCAST_TOKEN = " ";
  public const string PROTOCOL_VERSION = "10";
  private CoopPacketSerializer serializer;
  private BestHTTP.WebSocket.WebSocket sock;
  private bool isConnect;
  public CoopWebSocketSingleton<U>.CONNECTION_STATUS CurrentConnectionStatus;
  public Action<Exception> ErrorOccurred;
  public Action<ushort, string> CloseOccurred;
  public Action<ushort, string> PrepareCloseOccurred;
  private Queue<PacketStream> temporaryQueue = new Queue<PacketStream>();
  public Action<CoopPacket> ReceivePacketAction;
  [SerializeField]
  private string _relayServer;
  [SerializeField]
  private int _fromId;
  [SerializeField]
  private int _ackPrefix;
  [SerializeField]
  private int _packetSendCount;
  [SerializeField]
  private float _packetSendTime;
  private const int SEQUENCE_MAX = 10000000;
  private const float HEARTBEAT_TIMEOUT = 10f;
  private const float HEARTBEAT_INTERVAL = 3f;
  private DateTime lastPacketReceivedTime;
  public System.Action HeartbeatDisconnected;
  private const int MAX_RESEND_COUNT = 3;
  [SerializeField]
  private float resendInterval = 5f;
  private UIntKeyTable<CoopWebSocketSingleton<U>.ResendPacket> resendPackets = new UIntKeyTable<CoopWebSocketSingleton<U>.ResendPacket>();

  public string relayServer
  {
    get => this._relayServer;
    private set => this._relayServer = value;
  }

  public int fromId
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

  public float packetSendTime
  {
    get => this._packetSendTime;
    private set => this._packetSendTime = value;
  }

  public int sequence { get; private set; }

  public static bool IsValidConnected()
  {
    return MonoBehaviourSingleton<U>.IsValid() && MonoBehaviourSingleton<U>.I.IsConnected();
  }

  public static bool IsValidOpen()
  {
    return MonoBehaviourSingleton<U>.IsValid() && MonoBehaviourSingleton<U>.I.IsOpen();
  }

  public CoopWebSocketSingleton()
  {
    this.serializer = CoopWebSocketSingleton<U>.CreatePacketSerializer();
  }

  protected override void Awake() => base.Awake();

  protected override void OnDestroySingleton() => this.Close();

  private void Update()
  {
    while (this.IsConnected() && this.temporaryQueue.Count > 0)
      this.ReceivePacket(this.temporaryQueue.Dequeue());
  }

  public void LogDebug(string message, params object[] args)
  {
  }

  public bool IsConnected() => this.isConnect;

  public bool IsOpen() => this.sock != null && this.sock.IsOpen;

  public void Connect() => this.Connect(this.relayServer, this.fromId, this.ackPrefix);

  public void Connect(string path, int from_id, int ack_prefix)
  {
    this.temporaryQueue.Clear();
    this.resendPackets.Clear();
    this.relayServer = path;
    this.fromId = from_id;
    this.ackPrefix = ack_prefix;
    this.NativeConnect(this.relayServer);
  }

  private void NativeConnect(string relayServer)
  {
    this.sock = new BestHTTP.WebSocket.WebSocket(new Uri(relayServer));
    this.CurrentConnectionStatus = CoopWebSocketSingleton<U>.CONNECTION_STATUS.OPENING;
    this.ErrorOccurred = (Action<Exception>) null;
    this.CloseOccurred = (Action<ushort, string>) null;
    this.PrepareCloseOccurred = (Action<ushort, string>) null;
    this.HeartbeatDisconnected = (System.Action) null;
    this.sock.OnOpen += (Action<BestHTTP.WebSocket.WebSocket>) (ws =>
    {
      this.LogDebug("OnOpen {0}", (object) ws.InternalRequest.Uri);
      this.ClearLastPacketReceivedTime();
      this.isConnect = true;
      this.CurrentConnectionStatus = CoopWebSocketSingleton<U>.CONNECTION_STATUS.CONNECTED;
      this.packetSendTime = Time.time;
      this.StartCoroutine("Heartbeat");
      this.ReceivePacketAction += new Action<CoopPacket>(this.CheckAndSendAck);
      this.StartCoroutine("ResendMonitor");
      this.ReceivePacketAction += new Action<CoopPacket>(this.RemoveResendPacket);
    });
    this.sock.OnBinary += (Action<BestHTTP.WebSocket.WebSocket, byte[]>) ((ws, binary) =>
    {
      this.ClearLastPacketReceivedTime();
      this.temporaryQueue.Enqueue(new PacketStream((object) binary));
    });
    this.sock.OnMessage += (Action<BestHTTP.WebSocket.WebSocket, string>) ((ws, message) =>
    {
      this.ClearLastPacketReceivedTime();
      this.temporaryQueue.Enqueue(new PacketStream((object) message));
    });
    this.sock.OnClosed += (Action<BestHTTP.WebSocket.WebSocket, ushort, string>) ((ws, code, message) =>
    {
      this.OnPrepareClose(code, message);
      this.temporaryQueue.Clear();
      this.RemoveAllResendPackets();
      this.OnCloseOccurred(code, message);
      this.LogDebug("OnClosed Code {0}", (object) code);
      this.LogDebug("OnClosed Message {0}", (object) message);
    });
    this.sock.OnError += (Action<BestHTTP.WebSocket.WebSocket, Exception>) ((ws, ex) =>
    {
      this.CurrentConnectionStatus = CoopWebSocketSingleton<U>.CONNECTION_STATUS.ERROR;
      this.OnErrorOccurred(ex);
      this.LogDebug("OnError Message {0}", (object) ex.Message);
      this.LogDebug("OnError StackTrace {0}", (object) ex.StackTrace);
    });
    this.sock.OnPong += (Action<BestHTTP.WebSocket.WebSocket, byte[]>) ((ws, data) => this.ClearLastPacketReceivedTime());
    this.sock.StartPingThread = true;
    this.sock.PingFrequency = 3000;
    this.sock.Open();
  }

  public void Close(ushort code = 1000, string msg = "Bye!")
  {
    if (!this.IsOpen())
      return;
    this.OnPrepareClose(code, msg);
    this.sock.Close(code, msg);
  }

  private void OnPrepareClose(ushort code, string msg)
  {
    if (!this.isConnect)
      return;
    if (this.PrepareCloseOccurred != null)
      this.PrepareCloseOccurred(code, msg);
    this.ReceivePacketAction = (Action<CoopPacket>) (packet => { });
    this.isConnect = false;
    this.CurrentConnectionStatus = CoopWebSocketSingleton<U>.CONNECTION_STATUS.CLOSED;
  }

  private void OnCloseOccurred(ushort code, string message)
  {
    if (this.CloseOccurred == null)
      return;
    this.CloseOccurred(code, message);
  }

  private void OnErrorOccurred(Exception ex)
  {
    if (this.ErrorOccurred == null)
      return;
    this.ErrorOccurred(ex);
  }

  public int Send<T>(T model, int to_id, bool promise = true) where T : Coop_Model_Base
  {
    return this.Send((Coop_Model_Base) model, typeof (T), to_id, promise);
  }

  public int Send(
    Coop_Model_Base model,
    System.Type type,
    int to_id,
    bool promise = true,
    Func<Coop_Model_ACK, bool> onReceiveAck = null,
    Func<Coop_Model_Base, bool> onPreResend = null)
  {
    model.ct = (int) Coop_Model_Base.GetClientType();
    model.u = this.packetSendCount++;
    int sequence_no = 0;
    if (promise)
    {
      ++this.sequence;
      if (this.sequence >= 10000000)
        this.sequence = 0;
      sequence_no = this.ackPrefix * 10000000 + this.sequence;
    }
    CoopPacket packet = CoopPacket.Create(model, this.fromId, to_id, promise, sequence_no);
    PacketStream stream = this.serializer.Serialize(packet);
    if (model.c != 1000 && model.c != 3)
      this.LogDebug("Send packet: {0} (stream: {1})", (object) packet, (object) stream);
    this.NativeSend(stream);
    if (promise)
      this.RegistResendPacket(packet, onReceiveAck, onPreResend);
    return sequence_no;
  }

  private void NativeSend(PacketStream stream)
  {
    if (stream.IsBuffer())
      this.sock.Send(stream.ToBuffer());
    else if (stream.IsString())
      this.sock.Send(stream.ToString());
    this.packetSendTime = Time.time;
  }

  private void ReceivePacket(PacketStream stream)
  {
    if (stream == null || stream.Length <= 0)
      return;
    CoopPacket coopPacket = this.serializer.Deserialize<Coop_Model_Base>(stream);
    if (coopPacket == null)
      return;
    if (coopPacket.model == null || coopPacket.header == null)
    {
      Debug.LogWarning((object) "Packet model or header is null");
    }
    else
    {
      if (coopPacket.packetType != PACKET_TYPE.HEARTBEAT && coopPacket.packetType != PACKET_TYPE.ACK)
        this.LogDebug("Receive packet: {0} (stream: {1})", (object) coopPacket, (object) stream);
      this.ReceivePacketAction(coopPacket);
    }
  }

  public void ClearLastPacketReceivedTime() => this.lastPacketReceivedTime = DateTime.Now;

  private IEnumerator Heartbeat()
  {
    while (this.IsConnected())
    {
      if (10.0 <= (DateTime.Now - this.lastPacketReceivedTime).TotalSeconds)
      {
        this.OnHeartbeatDisconnected();
        this.ClearLastPacketReceivedTime();
      }
      yield return (object) null;
    }
  }

  private void OnHeartbeatDisconnected()
  {
    this.LogDebug("OnHeartbeatDisconnected.");
    if (this.HeartbeatDisconnected == null)
      return;
    this.HeartbeatDisconnected();
  }

  private void CheckAndSendAck(CoopPacket packet)
  {
    if (packet == null || packet.packetType == PACKET_TYPE.ACK || !packet.promise)
      return;
    this.SendAck(packet);
  }

  private void SendAck(CoopPacket p)
  {
    this.Send<Coop_Model_ACK>(new Coop_Model_ACK()
    {
      ack = p.sequenceNo,
      positive = true
    }, -1000, false);
  }

  private void RegistResendPacket(
    CoopPacket packet,
    Func<Coop_Model_ACK, bool> onReceiveAck,
    Func<Coop_Model_Base, bool> onPreResend = null)
  {
    packet.model.r = true;
    this.resendPackets.Add((uint) packet.sequenceNo, new CoopWebSocketSingleton<U>.ResendPacket()
    {
      resendCount = 0,
      lastSendTime = Time.time,
      onReceiveAck = onReceiveAck,
      onPreResend = onPreResend,
      packet = packet
    });
  }

  private void RemoveResendPacket(CoopPacket packet)
  {
    if (packet.packetType != PACKET_TYPE.ACK && packet.packetType != PACKET_TYPE.REGISTER_ACK && packet.packetType != PACKET_TYPE.PARTY_REGISTER_ACK && packet.packetType != PACKET_TYPE.LOUNGE_REGISTER_ACK || !(packet.model is Coop_Model_ACK model))
      return;
    CoopWebSocketSingleton<U>.ResendPacket resendPacket = this.resendPackets.Get((uint) model.ack);
    if (resendPacket == null)
      return;
    bool flag = model.positive;
    if (resendPacket.onReceiveAck != null)
      flag = resendPacket.onReceiveAck(model);
    if (!flag)
      return;
    this.LogDebug("Remove a packet from the resending queue: packet={0}, ack={1}", (object) resendPacket.packet, (object) model.ack);
    this.resendPackets.Remove((uint) model.ack);
  }

  private void RemoveAllResendPackets()
  {
    this.resendPackets.ForEach((Action<CoopWebSocketSingleton<U>.ResendPacket>) (resend =>
    {
      if (resend.onReceiveAck == null)
        return;
      int num = resend.onReceiveAck((Coop_Model_ACK) null) ? 1 : 0;
    }));
    this.resendPackets.Clear();
  }

  public void RemoveResendPackets(int to_client_id)
  {
    List<uint> delete_keys = new List<uint>();
    this.resendPackets.ForEach((Action<CoopWebSocketSingleton<U>.ResendPacket>) (resend =>
    {
      if (resend.packet.toClientId != to_client_id)
        return;
      if (resend.onReceiveAck != null)
      {
        int num = resend.onReceiveAck((Coop_Model_ACK) null) ? 1 : 0;
      }
      delete_keys.Add((uint) resend.packet.sequenceNo);
      this.LogDebug("Remove resend packet: packet={0}", (object) resend.packet);
    }));
    delete_keys.ForEach((Action<uint>) (key => this.resendPackets.Remove(key)));
  }

  private IEnumerator ResendMonitor()
  {
    try
    {
      while (this.IsConnected())
      {
        float now = Time.time;
        List<uint> delete_keys = new List<uint>();
        this.resendPackets.ForEach((Action<CoopWebSocketSingleton<U>.ResendPacket>) (resend =>
        {
          // ISSUE: reference to a compiler-generated field
          if ((double) now - (double) resend.lastSendTime <= (double) this.\u003C\u003E4__this.resendInterval)
            return;
          // ISSUE: reference to a compiler-generated field
          this.\u003C\u003E4__this.LogDebug("Resend packet: {0}", (object) resend.packet);
          if (resend.onPreResend != null)
          {
            bool flag = true;
            try
            {
              flag = resend.onPreResend(resend.packet.model);
            }
            catch (Exception ex)
            {
              resend.onPreResend = (Func<Coop_Model_Base, bool>) null;
              Log.Warning(LOG.WEBSOCK, $"Excetpion resend.onPreResend:{(object) resend.packet}/{ex.Message}");
            }
            if (!flag)
            {
              delete_keys.Add((uint) resend.packet.sequenceNo);
              // ISSUE: reference to a compiler-generated field
              this.\u003C\u003E4__this.LogDebug("Delete resend packet: sequence={0}", (object) resend.packet.sequenceNo);
              return;
            }
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          this.\u003C\u003E4__this.NativeSend(this.\u003C\u003E4__this.serializer.Serialize(resend.packet));
          resend.lastSendTime = now;
          ++resend.resendCount;
        }));
        delete_keys.ForEach((Action<uint>) (key => this.resendPackets.Remove(key)));
        yield return (object) new WaitForSeconds(this.resendInterval);
      }
    }
    finally
    {
      this.LogDebug("End the coroutine resending packets.");
    }
  }

  public bool IsCompleteSend(int sequence) => this.resendPackets.Get((uint) sequence) == null;

  public bool IsCompleteSendAll() => this.resendPackets.GetCount() <= 0;

  public void LoggingResendPackets(string log)
  {
    this.resendPackets.ForEach((Action<CoopWebSocketSingleton<U>.ResendPacket>) (resend => Log.Warning(LOG.WEBSOCK, "{0} resend packet: {1}", (object) log, (object) resend.packet)));
  }

  public static CoopPacketSerializer CreatePacketSerializer()
  {
    return (CoopPacketSerializer) new CoopPacketMsgpackUnitySerializer();
  }

  public enum CONNECTION_STATUS
  {
    NONE,
    CONNECTED,
    OPENING,
    CLOSED,
    ERROR,
  }

  private class ResendPacket
  {
    public int resendCount;
    public float lastSendTime;
    public Func<Coop_Model_ACK, bool> onReceiveAck;
    public Func<Coop_Model_Base, bool> onPreResend;
    public CoopPacket packet;
  }
}
