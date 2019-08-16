using BestHTTP.WebSocket;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChatWebSocket : MonoBehaviourSingleton<ChatWebSocket>
{
	public enum CONNECTION_STATUS
	{
		NONE,
		CONNECTED,
		OPENING,
		CLOSED,
		ERROR
	}

	public const int SERVER_ID = -1000;

	public const int BROADCAST_ID = -2000;

	public const int PROTCOL_VER = 0;

	public const string SERVER_TOKEN = "###########";

	public const string BROADCAST_TOKEN = "@@@@@@@@@@@";

	public const string PROTOCOL_VERSION = "00";

	private WebSocket sock;

	private bool isConnect;

	public CONNECTION_STATUS CurrentConnectionStatus;

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

	public string fromId
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

	public int sequence
	{
		get;
		private set;
	}

	public event EventHandler ErrorOccurred;

	public event Action OnClosed;

	public event Action<double> OnPong;

	public void Setup()
	{
	}

	public void Connect()
	{
		Connect(relayServer, fromId, ackPrefix);
	}

	public void Connect(string path, string from_id, int ack_prefix)
	{
		temporaryQueue.Clear();
		relayServer = path;
		fromId = from_id;
		ackPrefix = ack_prefix;
		NativeConnect(relayServer);
	}

	private void NativeConnect(string relayServer)
	{
		sock = new WebSocket(new Uri(relayServer));
		CurrentConnectionStatus = CONNECTION_STATUS.OPENING;
		WebSocket webSocket = sock;
		webSocket.OnOpen = (Action<WebSocket>)Delegate.Combine(webSocket.OnOpen, (Action<WebSocket>)delegate(WebSocket ws)
		{
			LogDebug("OnOpen {0}", ws.InternalRequest.Uri);
			ClearLastPacketReceivedTime();
			isConnect = true;
			CurrentConnectionStatus = CONNECTION_STATUS.CONNECTED;
		});
		WebSocket webSocket2 = sock;
		webSocket2.OnBinary = (Action<WebSocket, byte[]>)Delegate.Combine(webSocket2.OnBinary, (Action<WebSocket, byte[]>)delegate(WebSocket ws, byte[] binary)
		{
			LogDebug("OnBinary {0}", binary.Length);
			ClearLastPacketReceivedTime();
			temporaryQueue.Enqueue(new PacketStream(binary));
		});
		WebSocket webSocket3 = sock;
		webSocket3.OnMessage = (Action<WebSocket, string>)Delegate.Combine(webSocket3.OnMessage, (Action<WebSocket, string>)delegate(WebSocket ws, string message)
		{
			LogDebug("OnMessage {0}", message);
			ClearLastPacketReceivedTime();
			temporaryQueue.Enqueue(new PacketStream(message));
		});
		WebSocket webSocket4 = sock;
		webSocket4.OnClosed = (Action<WebSocket, ushort, string>)Delegate.Combine(webSocket4.OnClosed, (Action<WebSocket, ushort, string>)delegate(WebSocket ws, ushort code, string message)
		{
			OnPrepareClose();
			temporaryQueue.Clear();
			if (this.OnClosed != null)
			{
				this.OnClosed();
			}
			LogDebug("OnClosed Code {0}", code);
			LogDebug("OnClosed Message {0}", message);
		});
		WebSocket webSocket5 = sock;
		webSocket5.OnError = (Action<WebSocket, Exception>)Delegate.Combine(webSocket5.OnError, (Action<WebSocket, Exception>)delegate(WebSocket ws, Exception ex)
		{
			CurrentConnectionStatus = CONNECTION_STATUS.ERROR;
			OnErrorOccurred(EventArgs.Empty, ex);
			LogDebug("OnError Message {0}", ex.Message);
			LogDebug("OnError StackTrace {0}", ex.StackTrace);
		});
		WebSocket webSocket6 = sock;
		webSocket6.OnPong = (Action<WebSocket, byte[]>)Delegate.Combine(webSocket6.OnPong, (Action<WebSocket, byte[]>)delegate
		{
			if (this.OnPong != null)
			{
				this.OnPong((DateTime.Now - lastPacketReceivedTime).TotalMilliseconds - 3000.0);
			}
			ClearLastPacketReceivedTime();
		});
		sock.StartPingThread = true;
		sock.PingFrequency = 3000;
		sock.Open();
	}

	public void Close(ushort code = 1000, string msg = "Bye!")
	{
		OnPrepareClose();
		sock.Close(code, msg);
	}

	private void OnPrepareClose()
	{
		ReceivePacketAction = null;
		isConnect = false;
		CurrentConnectionStatus = CONNECTION_STATUS.CLOSED;
	}

	protected virtual void OnErrorOccurred(EventArgs e, Exception ex)
	{
		if (this.ErrorOccurred != null)
		{
			this.ErrorOccurred(this, e);
		}
	}

	public int Send<T>(T model, int to_id, bool promise = true) where T : Chat_Model_Base
	{
		return Send(model, typeof(T), to_id, promise);
	}

	public int Send(Chat_Model_Base model, Type type, int to_id, bool promise = true, Func<Coop_Model_ACK, bool> onReceiveAck = null, Func<Coop_Model_Base, bool> onPreResend = null)
	{
		int result = 0;
		ChatPacket chatPacket = new ChatPacket();
		ChatPacketHeader chatPacketHeader2 = chatPacket.header = new ChatPacketHeader(0, model.commandId, fromId);
		chatPacket.model = model;
		PacketStream stream = chatPacket.Serialize();
		if (model.commandId != 502)
		{
		}
		NativeSend(stream);
		return result;
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
	}

	private void ReceivePacket(PacketStream stream)
	{
		if (stream != null && stream.Length > 0)
		{
			ChatPacket chatPacket = ChatPacket.Deserialize(stream);
			if (chatPacket != null && ReceivePacketAction != null)
			{
				ReceivePacketAction(chatPacket);
			}
		}
	}

	public void ClearLastPacketReceivedTime()
	{
		lastPacketReceivedTime = DateTime.Now;
	}

	public bool IsConnected()
	{
		return isConnect;
	}

	public bool IsOpen()
	{
		return sock != null && sock.IsOpen;
	}

	protected new void Awake()
	{
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
}
