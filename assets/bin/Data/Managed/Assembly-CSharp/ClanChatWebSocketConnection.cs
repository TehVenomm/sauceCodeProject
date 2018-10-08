using Network;
using System;
using System.Collections;
using UnityEngine;

public class ClanChatWebSocketConnection : IClanChatConnection
{
	private static readonly string STAMP_SYMBOL_BEGIN = "[STMP]";

	private bool established;

	private bool joined;

	private string uri;

	private string roomId;

	private string userName;

	private Coroutine m_ConnectProcess;

	private bool autoReconnect;

	private float RECONNECT_WAIT_SEC = 1f;

	private int RECONNECT_RETRY_LIMIT = 1;

	private bool reconnecting;

	private bool isConnectProcessing;

	private float CONNECTION_TRY_TIMEOUT = 15f;

	public bool isEstablished => established;

	public bool isReadyToChat => joined;

	public bool isConnecting => isConnectProcessing;

	public ClanChatWebSocket chatWebSocket
	{
		get;
		private set;
	}

	public event ClanChatRoom.OnJoin onJoin;

	public event ClanChatRoom.OnLeave onLeave;

	public event ClanChatRoom.OnReceiveText onReceiveText;

	public event ClanChatRoom.OnReceiveStamp onReceiveStamp;

	public event ClanChatRoom.OnReceiveText onReceivePrivateText;

	public event ClanChatRoom.OnReceiveStamp onReceivePrivateStamp;

	public event ClanChatRoom.OnReceiveNotification onReceiveNotification;

	public event ClanChatRoom.OnDisconnect onDisconnect;

	public event ClanChatRoom.OnReceiveUpdateStatus onReceiveUpdateStatus;

	public ClanChatWebSocketConnection()
		: this()
	{
	}

	public void Setup(string host, int port, string path, bool autoReconnect = true)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		UriBuilder uriBuilder = new UriBuilder("ws", host, port, path);
		uri = uriBuilder.Uri.ToString();
		this.autoReconnect = autoReconnect;
		chatWebSocket = (Utility.CreateGameObjectAndComponent("ClanChatWebSocket", this.get_transform(), -1) as ClanChatWebSocket);
	}

	private unsafe void OnWebSocketClosed()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		chatWebSocket.OnClosed -= new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
		established = false;
		joined = false;
		if (autoReconnect && !reconnecting)
		{
			Reconnect(RECONNECT_RETRY_LIMIT);
		}
		if (this.onDisconnect != null)
		{
			this.onDisconnect();
		}
	}

	private void Reconnect(int count)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		reconnecting = true;
		this.StartCoroutine(TryReconnect(count));
	}

	private IEnumerator TryReconnect(int count)
	{
		for (float time = RECONNECT_WAIT_SEC * (float)(RECONNECT_RETRY_LIMIT - count + 1); time > 0f; time -= Time.get_deltaTime())
		{
			yield return (object)null;
		}
		TryConnect(delegate(bool success)
		{
			((_003CTryReconnect_003Ec__Iterator217)/*Error near IL_0088: stateMachine*/)._003C_003Ef__this.reconnecting = false;
			if (!success)
			{
				if (((_003CTryReconnect_003Ec__Iterator217)/*Error near IL_0088: stateMachine*/).count > 0)
				{
					((_003CTryReconnect_003Ec__Iterator217)/*Error near IL_0088: stateMachine*/)._003C_003Ef__this.Reconnect(((_003CTryReconnect_003Ec__Iterator217)/*Error near IL_0088: stateMachine*/).count - 1);
				}
			}
			else
			{
				((_003CTryReconnect_003Ec__Iterator217)/*Error near IL_0088: stateMachine*/)._003C_003Ef__this.Join(((_003CTryReconnect_003Ec__Iterator217)/*Error near IL_0088: stateMachine*/)._003C_003Ef__this.roomId, ((_003CTryReconnect_003Ec__Iterator217)/*Error near IL_0088: stateMachine*/)._003C_003Ef__this.userName);
			}
		});
	}

	public void Connect()
	{
		TryConnect(delegate
		{
		});
	}

	private void TryConnect(Action<bool> onFinished)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		if (isEstablished)
		{
			onFinished?.Invoke(true);
		}
		else if (isConnectProcessing)
		{
			this.StartCoroutine(WaitConnectProcess(onFinished));
		}
		else
		{
			chatWebSocket.ReceivePacketAction = OnReceivePacket;
			m_ConnectProcess = this.StartCoroutine(ConnectProcess(onFinished));
		}
	}

	public unsafe void Disconnect(Action onFinished = null)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		if (isEstablished)
		{
			chatWebSocket.OnClosed -= new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			chatWebSocket.Send(Chat_Model_LeaveRoom_Request.Create(roomId), 0, true);
			chatWebSocket.Close(1000, "Bye!");
			established = false;
			joined = false;
			if ((onFinished != null || this.onDisconnect != null) && !AppMain.isApplicationQuit)
			{
				this.StartCoroutine(WaitClose(onFinished));
			}
		}
		else
		{
			StopConnectProcess();
			if (onFinished != null)
			{
				onFinished.Invoke();
			}
			if (this.onDisconnect != null)
			{
				this.onDisconnect();
			}
		}
	}

	private IEnumerator WaitClose(Action onFinished = null)
	{
		while (chatWebSocket.IsOpen())
		{
			yield return (object)null;
		}
		if (onFinished != null)
		{
			onFinished.Invoke();
		}
		if (this.onDisconnect != null)
		{
			this.onDisconnect();
		}
	}

	public void Join(int roomNo, string userName)
	{
		roomId = roomNo.ToString();
		Join(roomId, userName);
	}

	private void Join(string roomId, string userName)
	{
		this.roomId = roomId;
		this.userName = userName.Replace(":", "：");
		TryConnect(delegate(bool result)
		{
			if (result)
			{
				chatWebSocket.Send(Chat_Model_JoinClanRoom.Create(this.roomId, this.userName), 0, true);
			}
			else
			{
				Log.Error("I failed to enter chat");
			}
		});
	}

	public void Leave(int roomNo, string userName)
	{
		roomId = roomNo.ToString();
		Leave(roomId, userName);
	}

	private void Leave(string roomId, string userName)
	{
		this.roomId = roomId;
		this.userName = userName.Replace(":", "：");
		TryConnect(delegate(bool result)
		{
			if (result)
			{
				chatWebSocket.Send(Chat_Model_LeaveClanRoom.Create(this.roomId, this.userName), 0, true);
			}
			else
			{
				Log.Error("I failed to leave chat");
			}
		});
	}

	public void SendText(string message)
	{
		if (isEstablished)
		{
			string text = message.Replace(":", "：");
			int num = PickStampId(text);
			if (num <= 0 || MonoBehaviourSingleton<UIManager>.I.mainChat.CanIPostTheStamp(num))
			{
				Send(text);
			}
		}
	}

	public void SendStamp(int stampId)
	{
		if (isEstablished)
		{
			string message = $"{STAMP_SYMBOL_BEGIN}{stampId:D8}";
			Send(message);
		}
	}

	private void Send(string message)
	{
		chatWebSocket.Send(Chat_Model_BroadcastClanMessage_Request.Create(roomId, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name, message), 0, true);
	}

	public void SendPrivateText(string target_id, string message)
	{
		if (isEstablished)
		{
			string text = message.Replace(":", "：");
			int num = PickStampId(text);
			if (num <= 0 || MonoBehaviourSingleton<UIManager>.I.mainChat.CanIPostTheStamp(num))
			{
				SendPrivate(target_id, text);
			}
		}
	}

	public void SendPrivateStamp(string target_id, int stampId)
	{
		if (isEstablished)
		{
			string message = $"{STAMP_SYMBOL_BEGIN}{stampId:D8}";
			SendPrivate(target_id, message);
		}
	}

	private void SendPrivate(string target_id, string message)
	{
		chatWebSocket.Send(Chat_Model_SendToClanMessage_Request.Create(target_id, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name, roomId, message), 0, true);
	}

	private void OnReceivePacket(ChatPacket packet)
	{
		if (packet != null)
		{
			switch (packet.model.packetType)
			{
			case CHAT_PACKET_TYPE.CLAN_JOIN_ROOM:
				OnJoin(packet);
				break;
			case CHAT_PACKET_TYPE.CLAN_LEAVE_ROOM:
				OnLeave(packet);
				break;
			case CHAT_PACKET_TYPE.CLAN_BROADCAST_ROOM:
				OnReceiveMessage(packet);
				break;
			case CHAT_PACKET_TYPE.CLAN_SENDTO:
				OnReceivePrivateMessage(packet);
				break;
			case CHAT_PACKET_TYPE.CLAN_BROADCAST_STATUS:
				OnReceiveUpdateStatus(packet);
				break;
			}
		}
	}

	private void OnJoin(ChatPacket packet)
	{
		Chat_Model_JoinClanRoom chat_Model_JoinClanRoom = packet.model as Chat_Model_JoinClanRoom;
		if (chat_Model_JoinClanRoom != null && chat_Model_JoinClanRoom.errorType == CHAT_ERROR_TYPE.NO_ERROR)
		{
			long result = 0L;
			long.TryParse(packet.header.fromId, out result);
			long result2 = 0L;
			long.TryParse(chatWebSocket.fromId, out result2);
			if (result == result2)
			{
				if (string.IsNullOrEmpty(chat_Model_JoinClanRoom.UserId))
				{
					joined = true;
				}
				if (this.onJoin != null)
				{
					this.onJoin(chat_Model_JoinClanRoom.errorType, chat_Model_JoinClanRoom.UserId);
				}
			}
		}
	}

	private void OnLeave(ChatPacket packet)
	{
		Chat_Model_LeaveClanRoom chat_Model_LeaveClanRoom = packet.model as Chat_Model_LeaveClanRoom;
		if (chat_Model_LeaveClanRoom != null && chat_Model_LeaveClanRoom.errorType == CHAT_ERROR_TYPE.NO_ERROR)
		{
			long result = 0L;
			long.TryParse(packet.header.fromId, out result);
			long result2 = 0L;
			long.TryParse(chatWebSocket.fromId, out result2);
			if (result == result2)
			{
				if (chat_Model_LeaveClanRoom.Owner == 1)
				{
					joined = false;
				}
				if (this.onLeave != null)
				{
					this.onLeave(chat_Model_LeaveClanRoom.errorType, chat_Model_LeaveClanRoom.UserId);
				}
			}
		}
	}

	private void OnReceiveUpdateStatus(ChatPacket packet)
	{
		Chat_Model_BroadcastClanStatus_Response chat_Model_BroadcastClanStatus_Response = packet.model as Chat_Model_BroadcastClanStatus_Response;
		int result = 0;
		int result2 = 0;
		int result3 = 0;
		int result4 = 0;
		int result5 = 0;
		int.TryParse(chat_Model_BroadcastClanStatus_Response.Type, out result);
		int.TryParse(chat_Model_BroadcastClanStatus_Response.RoomId, out result2);
		int.TryParse(chat_Model_BroadcastClanStatus_Response.Result, out result3);
		int.TryParse(chat_Model_BroadcastClanStatus_Response.Status, out result4);
		int.TryParse(chat_Model_BroadcastClanStatus_Response.Id, out result5);
		ClanUpdateStatusData clanUpdateStatusData = new ClanUpdateStatusData();
		clanUpdateStatusData.id = result5;
		clanUpdateStatusData.type = result;
		clanUpdateStatusData.roomId = result2;
		clanUpdateStatusData.result = result3;
		clanUpdateStatusData.status = result4;
		switch (result)
		{
		case 2:
			if (this.onReceiveUpdateStatus != null)
			{
				this.onReceiveUpdateStatus(clanUpdateStatusData);
			}
			break;
		case 3:
			if (this.onReceiveUpdateStatus != null)
			{
				this.onReceiveUpdateStatus(clanUpdateStatusData);
			}
			break;
		}
	}

	private void OnReceiveMessage(ChatPacket packet)
	{
		Chat_Model_BroadcastClanMessage_Response chat_Model_BroadcastClanMessage_Response = packet.model as Chat_Model_BroadcastClanMessage_Response;
		int result = 0;
		int result2 = 0;
		int.TryParse(chat_Model_BroadcastClanMessage_Response.SenderId, out result);
		int.TryParse(chat_Model_BroadcastClanMessage_Response.Id, out result2);
		if (result == 0)
		{
			if (this.onReceiveNotification != null)
			{
				this.onReceiveNotification(chat_Model_BroadcastClanMessage_Response.Message);
			}
		}
		else if (chat_Model_BroadcastClanMessage_Response.Message.Contains(STAMP_SYMBOL_BEGIN))
		{
			string s = chat_Model_BroadcastClanMessage_Response.Message.Substring(STAMP_SYMBOL_BEGIN.Length, 8);
			int result3 = -1;
			int.TryParse(s, out result3);
			if (this.onReceiveStamp != null)
			{
				ClanChatLogMessageData clanChatLogMessageData = new ClanChatLogMessageData();
				clanChatLogMessageData.uuid = chat_Model_BroadcastClanMessage_Response.Uuid;
				clanChatLogMessageData.id = result2;
				clanChatLogMessageData.fromUserId = result;
				clanChatLogMessageData.senderName = chat_Model_BroadcastClanMessage_Response.SenderName;
				clanChatLogMessageData.stampId = result3;
				this.onReceiveStamp(clanChatLogMessageData);
			}
		}
		else if (this.onReceiveText != null)
		{
			ClanChatLogMessageData clanChatLogMessageData2 = new ClanChatLogMessageData();
			clanChatLogMessageData2.uuid = chat_Model_BroadcastClanMessage_Response.Uuid;
			clanChatLogMessageData2.id = result2;
			clanChatLogMessageData2.fromUserId = result;
			clanChatLogMessageData2.senderName = chat_Model_BroadcastClanMessage_Response.SenderName;
			clanChatLogMessageData2.message = chat_Model_BroadcastClanMessage_Response.Message;
			this.onReceiveText(clanChatLogMessageData2);
		}
	}

	private void OnReceivePrivateMessage(ChatPacket packet)
	{
		Chat_Model_SendToClanMessage_Response chat_Model_SendToClanMessage_Response = packet.model as Chat_Model_SendToClanMessage_Response;
		int result = 0;
		int result2 = 0;
		int result3 = 0;
		int.TryParse(chat_Model_SendToClanMessage_Response.SenderId, out result);
		int.TryParse(chat_Model_SendToClanMessage_Response.ReceiveId, out result2);
		int.TryParse(chat_Model_SendToClanMessage_Response.Id, out result3);
		if (chat_Model_SendToClanMessage_Response.Message.Contains(STAMP_SYMBOL_BEGIN))
		{
			string s = chat_Model_SendToClanMessage_Response.Message.Substring(STAMP_SYMBOL_BEGIN.Length, 8);
			int result4 = -1;
			int.TryParse(s, out result4);
			if (this.onReceivePrivateStamp != null)
			{
				ClanChatLogMessageData clanChatLogMessageData = new ClanChatLogMessageData();
				clanChatLogMessageData.uuid = chat_Model_SendToClanMessage_Response.Uuid;
				clanChatLogMessageData.id = result3;
				clanChatLogMessageData.fromUserId = result;
				clanChatLogMessageData.toUserId = result2;
				clanChatLogMessageData.senderName = chat_Model_SendToClanMessage_Response.SenderName;
				clanChatLogMessageData.stampId = result4;
				this.onReceivePrivateStamp(clanChatLogMessageData);
			}
		}
		else if (this.onReceivePrivateText != null)
		{
			ClanChatLogMessageData clanChatLogMessageData2 = new ClanChatLogMessageData();
			clanChatLogMessageData2.uuid = chat_Model_SendToClanMessage_Response.Uuid;
			clanChatLogMessageData2.id = result3;
			clanChatLogMessageData2.fromUserId = result;
			clanChatLogMessageData2.toUserId = result2;
			clanChatLogMessageData2.senderName = chat_Model_SendToClanMessage_Response.SenderName;
			clanChatLogMessageData2.message = chat_Model_SendToClanMessage_Response.Message;
			this.onReceivePrivateText(clanChatLogMessageData2);
		}
	}

	private unsafe IEnumerator ConnectProcess(Action<bool> onFinished)
	{
		if (!MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			onFinished(false);
		}
		else
		{
			isConnectProcessing = true;
			string fromId = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id.ToString();
			chatWebSocket.Connect(uri, fromId, 0);
			float waitTimeRest = CONNECTION_TRY_TIMEOUT;
			while (!chatWebSocket.IsConnected() && chatWebSocket.CurrentConnectionStatus != ClanChatWebSocket.CONNECTION_STATUS.ERROR && waitTimeRest > 0f)
			{
				waitTimeRest -= Time.get_deltaTime();
				yield return (object)null;
			}
			m_ConnectProcess = null;
			established = chatWebSocket.IsConnected();
			if (established)
			{
				chatWebSocket.OnClosed += new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			onFinished?.Invoke(isEstablished);
			isConnectProcessing = false;
		}
	}

	private IEnumerator WaitConnectProcess(Action<bool> onFinished)
	{
		while (isConnectProcessing)
		{
			yield return (object)null;
		}
		onFinished?.Invoke(isEstablished);
	}

	private void StopConnectProcess()
	{
		if (m_ConnectProcess != null)
		{
			this.StopCoroutine(m_ConnectProcess);
		}
		m_ConnectProcess = null;
		isConnectProcessing = false;
	}

	private static int PickStampId(string msg)
	{
		int result = -1;
		if (msg.Contains(STAMP_SYMBOL_BEGIN))
		{
			string s = msg.Substring(STAMP_SYMBOL_BEGIN.Length, 8);
			int.TryParse(s, out result);
		}
		return result;
	}

	private void OnDestroy()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		Disconnect(null);
		if (Object.op_Implicit(chatWebSocket))
		{
			Object.Destroy(chatWebSocket.get_gameObject());
		}
	}

	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			if (chatWebSocket != null)
			{
				Disconnect(null);
			}
		}
		else
		{
			Reconnect(RECONNECT_RETRY_LIMIT);
		}
	}
}
