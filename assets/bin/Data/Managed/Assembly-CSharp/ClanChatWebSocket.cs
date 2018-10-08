using BestHTTP.WebSocket;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ClanChatWebSocket : MonoBehaviourSingleton<ClanChatWebSocket>
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

	private const int SEQUENCE_MAX = 10000000;

	public const float HEARTBEAT_TIMEOUT = 10f;

	public const float HEARTBEAT_INTERVAL = 3f;

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

	public event Action OnClosed
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Expected O, but got Unknown
			this.OnClosed = Delegate.Combine((Delegate)this.OnClosed, (Delegate)value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Expected O, but got Unknown
			this.OnClosed = Delegate.Remove((Delegate)this.OnClosed, (Delegate)value);
		}
	}

	public event Action<double> OnPong;

	private void OnApplicationQuit()
	{
		if (MonoBehaviourSingleton<ChatManager>.I.clanChat != null && MonoBehaviourSingleton<ChatManager>.I.clanChat.HasConnect)
		{
			MonoBehaviourSingleton<ChatManager>.I.DestroyClanChat();
		}
	}

	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			if (MonoBehaviourSingleton<ChatManager>.I.clanChat != null && MonoBehaviourSingleton<ChatManager>.I.clanChat.HasConnect)
			{
				MonoBehaviourSingleton<ChatManager>.I.DestroyClanChat();
			}
		}
		else
		{
			MonoBehaviourSingleton<ChatManager>.I.CreateClanChat(MonoBehaviourSingleton<GuildManager>.I.guildInfos.chat, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.clanId, null);
		}
	}

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

	private unsafe void NativeConnect(string relayServer)
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
		webSocket2.OnBinary = Delegate.Combine((Delegate)webSocket2.OnBinary, (Delegate)new Action<WebSocket, byte[]>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		WebSocket webSocket3 = sock;
		webSocket3.OnMessage = Delegate.Combine((Delegate)webSocket3.OnMessage, (Delegate)new Action<WebSocket, string>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		WebSocket webSocket4 = sock;
		webSocket4.OnClosed = Delegate.Combine((Delegate)webSocket4.OnClosed, (Delegate)new Action<WebSocket, ushort, string>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		WebSocket webSocket5 = sock;
		webSocket5.OnError = Delegate.Combine((Delegate)webSocket5.OnError, (Delegate)new Action<WebSocket, Exception>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		WebSocket webSocket6 = sock;
		webSocket6.OnPong = Delegate.Combine((Delegate)webSocket6.OnPong, (Delegate)new Action<WebSocket, byte[]>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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
		return Send(model, typeof(T), to_id, promise, null, null);
	}

	public int Send(Chat_Model_Base model, Type type, int to_id, bool promise = true, Func<Coop_Model_ACK, bool> onReceiveAck = null, Func<Coop_Model_Base, bool> onPreResend = null)
	{
		int result = 0;
		ChatPacket chatPacket = new ChatPacket();
		ChatPacketHeader chatPacketHeader2 = chatPacket.header = new ChatPacketHeader(0, model.commandId, fromId);
		chatPacket.model = model;
		PacketStream stream = chatPacket.Serialize();
		if (model.commandId == 502)
		{
			goto IL_0040;
		}
		goto IL_0040;
		IL_0040:
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
