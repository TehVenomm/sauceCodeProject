using BestHTTP.WebSocket;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopWebSocketSingleton<U> : MonoBehaviourSingleton<U> where U : CoopWebSocketSingleton<U>
{
	public enum CONNECTION_STATUS
	{
		NONE,
		CONNECTED,
		OPENING,
		CLOSED,
		ERROR
	}

	private class ResendPacket
	{
		public int resendCount;

		public float lastSendTime;

		public Func<Coop_Model_ACK, bool> onReceiveAck;

		public Func<Coop_Model_Base, bool> onPreResend;

		public CoopPacket packet;
	}

	public const int SERVER_ID = -1000;

	public const int BROADCAST_ID = -2000;

	public const string SERVER_TOKEN = "";

	public const string BROADCAST_TOKEN = " ";

	public const string PROTOCOL_VERSION = "10";

	private CoopPacketSerializer serializer;

	private WebSocket sock;

	private bool isConnect;

	public CONNECTION_STATUS CurrentConnectionStatus;

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

	public Action HeartbeatDisconnected;

	private const int MAX_RESEND_COUNT = 3;

	[SerializeField]
	private float resendInterval = 5f;

	private UIntKeyTable<ResendPacket> resendPackets = new UIntKeyTable<ResendPacket>();

	public string relayServer
	{
		get
		{
			return _relayServer;
		}
		private set
		{
			_relayServer = value;
		}
	}

	public int fromId
	{
		get
		{
			return _fromId;
		}
		private set
		{
			_fromId = value;
		}
	}

	public int ackPrefix
	{
		get
		{
			return _ackPrefix;
		}
		private set
		{
			_ackPrefix = value;
		}
	}

	public int packetSendCount
	{
		get
		{
			return _packetSendCount;
		}
		private set
		{
			_packetSendCount = value;
		}
	}

	public float packetSendTime
	{
		get
		{
			return _packetSendTime;
		}
		private set
		{
			_packetSendTime = value;
		}
	}

	public int sequence
	{
		get;
		private set;
	}

	public CoopWebSocketSingleton()
	{
		serializer = CreatePacketSerializer();
	}

	public static bool IsValidConnected()
	{
		int result;
		if (MonoBehaviourSingleton<U>.IsValid())
		{
			U i = MonoBehaviourSingleton<U>.I;
			result = (i.IsConnected() ? 1 : 0);
		}
		else
		{
			result = 0;
		}
		return (byte)result != 0;
	}

	public static bool IsValidOpen()
	{
		int result;
		if (MonoBehaviourSingleton<U>.IsValid())
		{
			U i = MonoBehaviourSingleton<U>.I;
			result = (i.IsOpen() ? 1 : 0);
		}
		else
		{
			result = 0;
		}
		return (byte)result != 0;
	}

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void OnDestroySingleton()
	{
		Close(1000);
	}

	private void Update()
	{
		while (IsConnected() && temporaryQueue.Count > 0)
		{
			ReceivePacket(temporaryQueue.Dequeue());
		}
	}

	public void LogDebug(string message, params object[] args)
	{
	}

	public bool IsConnected()
	{
		return isConnect;
	}

	public bool IsOpen()
	{
		return sock != null && sock.IsOpen;
	}

	public void Connect()
	{
		Connect(relayServer, fromId, ackPrefix);
	}

	public void Connect(string path, int from_id, int ack_prefix)
	{
		temporaryQueue.Clear();
		resendPackets.Clear();
		relayServer = path;
		fromId = from_id;
		ackPrefix = ack_prefix;
		NativeConnect(relayServer);
	}

	private void NativeConnect(string relayServer)
	{
		sock = new WebSocket(new Uri(relayServer));
		CurrentConnectionStatus = CONNECTION_STATUS.OPENING;
		ErrorOccurred = null;
		CloseOccurred = null;
		PrepareCloseOccurred = null;
		HeartbeatDisconnected = null;
		WebSocket webSocket = sock;
		webSocket.OnOpen = (Action<WebSocket>)Delegate.Combine(webSocket.OnOpen, (Action<WebSocket>)delegate(WebSocket ws)
		{
			LogDebug("OnOpen {0}", ws.InternalRequest.Uri);
			ClearLastPacketReceivedTime();
			isConnect = true;
			CurrentConnectionStatus = CONNECTION_STATUS.CONNECTED;
			packetSendTime = Time.get_time();
			this.StartCoroutine("Heartbeat");
			ReceivePacketAction = (Action<CoopPacket>)Delegate.Combine(ReceivePacketAction, new Action<CoopPacket>(CheckAndSendAck));
			this.StartCoroutine("ResendMonitor");
			ReceivePacketAction = (Action<CoopPacket>)Delegate.Combine(ReceivePacketAction, new Action<CoopPacket>(RemoveResendPacket));
		});
		WebSocket webSocket2 = sock;
		webSocket2.OnBinary = (Action<WebSocket, byte[]>)Delegate.Combine(webSocket2.OnBinary, (Action<WebSocket, byte[]>)delegate(WebSocket ws, byte[] binary)
		{
			ClearLastPacketReceivedTime();
			temporaryQueue.Enqueue(new PacketStream(binary));
		});
		WebSocket webSocket3 = sock;
		webSocket3.OnMessage = (Action<WebSocket, string>)Delegate.Combine(webSocket3.OnMessage, (Action<WebSocket, string>)delegate(WebSocket ws, string message)
		{
			ClearLastPacketReceivedTime();
			temporaryQueue.Enqueue(new PacketStream(message));
		});
		WebSocket webSocket4 = sock;
		webSocket4.OnClosed = (Action<WebSocket, ushort, string>)Delegate.Combine(webSocket4.OnClosed, (Action<WebSocket, ushort, string>)delegate(WebSocket ws, ushort code, string message)
		{
			OnPrepareClose(code, message);
			temporaryQueue.Clear();
			RemoveAllResendPackets();
			OnCloseOccurred(code, message);
			LogDebug("OnClosed Code {0}", code);
			LogDebug("OnClosed Message {0}", message);
		});
		WebSocket webSocket5 = sock;
		webSocket5.OnError = (Action<WebSocket, Exception>)Delegate.Combine(webSocket5.OnError, (Action<WebSocket, Exception>)delegate(WebSocket ws, Exception ex)
		{
			CurrentConnectionStatus = CONNECTION_STATUS.ERROR;
			OnErrorOccurred(ex);
			LogDebug("OnError Message {0}", ex.Message);
			LogDebug("OnError StackTrace {0}", ex.StackTrace);
		});
		WebSocket webSocket6 = sock;
		webSocket6.OnPong = (Action<WebSocket, byte[]>)Delegate.Combine(webSocket6.OnPong, (Action<WebSocket, byte[]>)delegate
		{
			ClearLastPacketReceivedTime();
		});
		sock.StartPingThread = true;
		sock.PingFrequency = 3000;
		sock.Open();
	}

	public void Close(ushort code = 1000, string msg = "Bye!")
	{
		if (IsOpen())
		{
			OnPrepareClose(code, msg);
			sock.Close(code, msg);
		}
	}

	private void OnPrepareClose(ushort code, string msg)
	{
		if (isConnect)
		{
			if (PrepareCloseOccurred != null)
			{
				PrepareCloseOccurred(code, msg);
			}
			ReceivePacketAction = delegate
			{
			};
			isConnect = false;
			CurrentConnectionStatus = CONNECTION_STATUS.CLOSED;
		}
	}

	private void OnCloseOccurred(ushort code, string message)
	{
		if (CloseOccurred != null)
		{
			CloseOccurred(code, message);
		}
	}

	private void OnErrorOccurred(Exception ex)
	{
		if (ErrorOccurred != null)
		{
			ErrorOccurred(ex);
		}
	}

	public int Send<T>(T model, int to_id, bool promise = true) where T : Coop_Model_Base
	{
		return Send(model, typeof(T), to_id, promise);
	}

	public int Send(Coop_Model_Base model, Type type, int to_id, bool promise = true, Func<Coop_Model_ACK, bool> onReceiveAck = null, Func<Coop_Model_Base, bool> onPreResend = null)
	{
		model.ct = (int)Coop_Model_Base.GetClientType();
		model.u = packetSendCount++;
		int num = 0;
		if (promise)
		{
			sequence++;
			if (sequence >= 10000000)
			{
				sequence = 0;
			}
			num = ackPrefix * 10000000 + sequence;
		}
		CoopPacket coopPacket = CoopPacket.Create(model, fromId, to_id, promise, num);
		PacketStream packetStream = serializer.Serialize(coopPacket);
		if (model.c != 1000 && model.c != 3)
		{
			LogDebug("Send packet: {0} (stream: {1})", coopPacket, packetStream);
		}
		NativeSend(packetStream);
		if (promise)
		{
			RegistResendPacket(coopPacket, onReceiveAck, onPreResend);
		}
		return num;
	}

	private void NativeSend(PacketStream stream)
	{
		if (stream.IsBuffer())
		{
			sock.Send(stream.ToBuffer());
		}
		else if (stream.IsString())
		{
			sock.Send(stream.ToString());
		}
		packetSendTime = Time.get_time();
	}

	private void ReceivePacket(PacketStream stream)
	{
		if (stream == null || stream.Length <= 0)
		{
			return;
		}
		CoopPacket coopPacket = serializer.Deserialize<Coop_Model_Base>(stream);
		if (coopPacket == null)
		{
			return;
		}
		if (coopPacket.model == null || coopPacket.header == null)
		{
			Debug.LogWarning((object)"Packet model or header is null");
			return;
		}
		if (coopPacket.packetType != PACKET_TYPE.HEARTBEAT && coopPacket.packetType != PACKET_TYPE.ACK)
		{
			LogDebug("Receive packet: {0} (stream: {1})", coopPacket, stream);
		}
		ReceivePacketAction(coopPacket);
	}

	public void ClearLastPacketReceivedTime()
	{
		lastPacketReceivedTime = DateTime.Now;
	}

	private IEnumerator Heartbeat()
	{
		while (IsConnected())
		{
			double diffSeconds = (DateTime.Now - lastPacketReceivedTime).TotalSeconds;
			if (10.0 <= diffSeconds)
			{
				OnHeartbeatDisconnected();
				ClearLastPacketReceivedTime();
			}
			yield return null;
		}
	}

	private void OnHeartbeatDisconnected()
	{
		LogDebug("OnHeartbeatDisconnected.");
		if (HeartbeatDisconnected != null)
		{
			HeartbeatDisconnected();
		}
	}

	private void CheckAndSendAck(CoopPacket packet)
	{
		if (packet != null && packet.packetType != PACKET_TYPE.ACK && packet.promise)
		{
			SendAck(packet);
		}
	}

	private void SendAck(CoopPacket p)
	{
		Coop_Model_ACK coop_Model_ACK = new Coop_Model_ACK();
		coop_Model_ACK.ack = p.sequenceNo;
		coop_Model_ACK.positive = true;
		Send(coop_Model_ACK, -1000, promise: false);
	}

	private void RegistResendPacket(CoopPacket packet, Func<Coop_Model_ACK, bool> onReceiveAck, Func<Coop_Model_Base, bool> onPreResend = null)
	{
		packet.model.r = true;
		ResendPacket resendPacket = new ResendPacket();
		resendPacket.resendCount = 0;
		resendPacket.lastSendTime = Time.get_time();
		resendPacket.onReceiveAck = onReceiveAck;
		resendPacket.onPreResend = onPreResend;
		resendPacket.packet = packet;
		resendPackets.Add((uint)packet.sequenceNo, resendPacket);
	}

	private void RemoveResendPacket(CoopPacket packet)
	{
		if (packet.packetType != PACKET_TYPE.ACK && packet.packetType != PACKET_TYPE.REGISTER_ACK && packet.packetType != PACKET_TYPE.PARTY_REGISTER_ACK && packet.packetType != PACKET_TYPE.LOUNGE_REGISTER_ACK)
		{
			return;
		}
		Coop_Model_ACK coop_Model_ACK = packet.model as Coop_Model_ACK;
		if (coop_Model_ACK == null)
		{
			return;
		}
		ResendPacket resendPacket = resendPackets.Get((uint)coop_Model_ACK.ack);
		if (resendPacket != null)
		{
			bool flag = coop_Model_ACK.positive;
			if (resendPacket.onReceiveAck != null)
			{
				flag = resendPacket.onReceiveAck(coop_Model_ACK);
			}
			if (flag)
			{
				LogDebug("Remove a packet from the resending queue: packet={0}, ack={1}", resendPacket.packet, coop_Model_ACK.ack);
				resendPackets.Remove((uint)coop_Model_ACK.ack);
			}
		}
	}

	private void RemoveAllResendPackets()
	{
		resendPackets.ForEach(delegate(ResendPacket resend)
		{
			if (resend.onReceiveAck != null)
			{
				resend.onReceiveAck(null);
			}
		});
		resendPackets.Clear();
	}

	public void RemoveResendPackets(int to_client_id)
	{
		List<uint> delete_keys = new List<uint>();
		resendPackets.ForEach(delegate(ResendPacket resend)
		{
			if (resend.packet.toClientId == to_client_id)
			{
				if (resend.onReceiveAck != null)
				{
					resend.onReceiveAck(null);
				}
				delete_keys.Add((uint)resend.packet.sequenceNo);
				LogDebug("Remove resend packet: packet={0}", resend.packet);
			}
		});
		delete_keys.ForEach(delegate(uint key)
		{
			resendPackets.Remove(key);
		});
	}

	private IEnumerator ResendMonitor()
	{
		try
		{
			while (IsConnected())
			{
				float now = Time.get_time();
				List<uint> delete_keys = new List<uint>();
				resendPackets.ForEach(delegate(ResendPacket resend)
				{
					if (now - resend.lastSendTime > resendInterval)
					{
						LogDebug("Resend packet: {0}", resend.packet);
						if (resend.onPreResend != null)
						{
							bool flag = true;
							try
							{
								flag = resend.onPreResend(resend.packet.model);
							}
							catch (Exception ex)
							{
								resend.onPreResend = null;
								Log.Warning(LOG.WEBSOCK, "Excetpion resend.onPreResend:" + resend.packet + "/" + ex.Message);
							}
							if (!flag)
							{
								delete_keys.Add((uint)resend.packet.sequenceNo);
								LogDebug("Delete resend packet: sequence={0}", resend.packet.sequenceNo);
								return;
							}
						}
						PacketStream stream = serializer.Serialize(resend.packet);
						NativeSend(stream);
						resend.lastSendTime = now;
						resend.resendCount++;
					}
				});
				delete_keys.ForEach(delegate(uint key)
				{
					resendPackets.Remove(key);
				});
				yield return (object)new WaitForSeconds(resendInterval);
			}
		}
		finally
		{
			base._003C_003E__Finally0();
		}
	}

	public bool IsCompleteSend(int sequence)
	{
		return resendPackets.Get((uint)sequence) == null;
	}

	public bool IsCompleteSendAll()
	{
		return resendPackets.GetCount() <= 0;
	}

	public void LoggingResendPackets(string log)
	{
		resendPackets.ForEach(delegate(ResendPacket resend)
		{
			Log.Warning(LOG.WEBSOCK, "{0} resend packet: {1}", log, resend.packet);
		});
	}

	public static CoopPacketSerializer CreatePacketSerializer()
	{
		return new CoopPacketMsgpackUnitySerializer();
	}
}
