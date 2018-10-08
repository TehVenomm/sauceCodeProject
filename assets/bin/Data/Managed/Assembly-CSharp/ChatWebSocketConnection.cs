using System;
using System.Collections;
using UnityEngine;

public class ChatWebSocketConnection : MonoBehaviour, IChatConnection
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

	public ChatWebSocket chatWebSocket
	{
		get;
		private set;
	}

	public event ChatRoom.OnJoin onJoin;

	public event ChatRoom.OnReceiveText onReceiveText;

	public event ChatRoom.OnReceiveStamp onReceiveStamp;

	public event ChatRoom.OnReceiveNotification onReceiveNotification;

	public event ChatRoom.OnDisconnect onDisconnect;

	public void Setup(string host, int port, string path, bool autoReconnect = true)
	{
		UriBuilder uriBuilder = new UriBuilder("ws", host, port, path);
		uri = uriBuilder.Uri.ToString();
		this.autoReconnect = autoReconnect;
		chatWebSocket = (Utility.CreateGameObjectAndComponent("ChatWebSocket", base.transform, -1) as ChatWebSocket);
	}

	private void OnWebSocketClosed()
	{
		chatWebSocket.OnClosed -= OnWebSocketClosed;
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
		reconnecting = true;
		StartCoroutine(TryReconnect(count));
	}

	private IEnumerator TryReconnect(int count)
	{
		for (float time = RECONNECT_WAIT_SEC * (float)(RECONNECT_RETRY_LIMIT - count + 1); time > 0f; time -= Time.deltaTime)
		{
			yield return (object)null;
		}
		TryConnect(delegate(bool success)
		{
			((_003CTryReconnect_003Ec__Iterator20C)/*Error near IL_0088: stateMachine*/)._003C_003Ef__this.reconnecting = false;
			if (!success)
			{
				if (((_003CTryReconnect_003Ec__Iterator20C)/*Error near IL_0088: stateMachine*/).count > 0)
				{
					((_003CTryReconnect_003Ec__Iterator20C)/*Error near IL_0088: stateMachine*/)._003C_003Ef__this.Reconnect(((_003CTryReconnect_003Ec__Iterator20C)/*Error near IL_0088: stateMachine*/).count - 1);
				}
			}
			else
			{
				((_003CTryReconnect_003Ec__Iterator20C)/*Error near IL_0088: stateMachine*/)._003C_003Ef__this.Join(((_003CTryReconnect_003Ec__Iterator20C)/*Error near IL_0088: stateMachine*/)._003C_003Ef__this.roomId, ((_003CTryReconnect_003Ec__Iterator20C)/*Error near IL_0088: stateMachine*/)._003C_003Ef__this.userName);
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
		if (isEstablished)
		{
			onFinished?.Invoke(true);
		}
		else if (isConnectProcessing)
		{
			StartCoroutine(WaitConnectProcess(onFinished));
		}
		else
		{
			chatWebSocket.ReceivePacketAction = OnReceivePacket;
			m_ConnectProcess = StartCoroutine(ConnectProcess(onFinished));
		}
	}

	public void Disconnect(Action onFinished = null)
	{
		if (isEstablished)
		{
			chatWebSocket.OnClosed -= OnWebSocketClosed;
			chatWebSocket.Send(Chat_Model_LeaveRoom_Request.Create(roomId), 0, true);
			chatWebSocket.Close(1000, "Bye!");
			established = false;
			joined = false;
			if ((onFinished != null || this.onDisconnect != null) && !AppMain.isApplicationQuit)
			{
				StartCoroutine(WaitClose(onFinished));
			}
		}
		else
		{
			StopConnectProcess();
			onFinished?.Invoke();
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
		onFinished?.Invoke();
		if (this.onDisconnect != null)
		{
			this.onDisconnect();
		}
	}

	public void Join(int roomNo, string userName)
	{
		roomId = $"room{roomNo}";
		Join(roomId, userName);
	}

	public void JoinClan(int roomNo, string userName)
	{
		roomId = $"room{roomNo}";
		JoinClan(roomId, userName);
	}

	private void Join(string roomId, string userName)
	{
		this.roomId = roomId;
		this.userName = userName.Replace(":", "：");
		TryConnect(delegate(bool result)
		{
			if (result)
			{
				chatWebSocket.Send(Chat_Model_JoinRoom.Create(this.roomId, this.userName), 0, true);
			}
			else
			{
				Log.Error("チャット入室に失敗しました");
			}
		});
	}

	private void JoinClan(string roomId, string userName)
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
				Log.Error("チャット入室に失敗しました");
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
		chatWebSocket.Send(Chat_Model_BroadcastMessage_Request.Create(roomId, message), 0, true);
	}

	private void OnReceivePacket(ChatPacket packet)
	{
		if (packet != null)
		{
			switch (packet.model.packetType)
			{
			case CHAT_PACKET_TYPE.JOIN_ROOM:
				OnJoin(packet);
				break;
			case CHAT_PACKET_TYPE.BROADCAST_ROOM:
			{
				Chat_Model_BroadcastMessage_Response chat_Model_BroadcastMessage_Response = packet.model as Chat_Model_BroadcastMessage_Response;
				if (chat_Model_BroadcastMessage_Response != null)
				{
					OnReceiveMessage(chat_Model_BroadcastMessage_Response);
				}
				else
				{
					Log.Error("Failed parse: Chat_Model_BroadCastMessage_Response");
				}
				break;
			}
			case CHAT_PACKET_TYPE.PARTY_INVITE:
			{
				Chat_Model_PartyInvite chat_Model_PartyInvite = packet.model as Chat_Model_PartyInvite;
				if (chat_Model_PartyInvite != null)
				{
					OnReceivePartyInvite(chat_Model_PartyInvite);
				}
				else
				{
					Log.Error("Failed parse: Chat_Model_BroadCastMessage_Response");
				}
				break;
			}
			case CHAT_PACKET_TYPE.RALLY_INVITE:
			{
				Chat_Model_RallyInvite chat_Model_RallyInvite = packet.model as Chat_Model_RallyInvite;
				if (chat_Model_RallyInvite != null)
				{
					OnReceiveRallyInvite(chat_Model_RallyInvite);
				}
				else
				{
					Log.Error("Failed parse: Chat_Model_RallyInvite");
				}
				break;
			}
			case CHAT_PACKET_TYPE.DARK_MARKET_RESET:
			{
				Chat_Model_ResetDarkMarket chat_Model_ResetDarkMarket = packet.model as Chat_Model_ResetDarkMarket;
				if (chat_Model_ResetDarkMarket != null)
				{
					OnReceiveResetDarkMarket(chat_Model_ResetDarkMarket);
				}
				else
				{
					Log.Error("Failed parse: Chat_Model_ResetDarkMarket");
				}
				break;
			}
			case CHAT_PACKET_TYPE.DARK_MARKET_UPDATE:
			{
				Chat_Model_UpdateDarkMarket chat_Model_UpdateDarkMarket = packet.model as Chat_Model_UpdateDarkMarket;
				if (chat_Model_UpdateDarkMarket != null)
				{
					OnReceiveUpdateDarkMarket(chat_Model_UpdateDarkMarket);
				}
				else
				{
					Log.Error("Failed parse: Chat_Model_UpdateDarkMarket");
				}
				break;
			}
			}
		}
	}

	private void OnJoin(ChatPacket packet)
	{
		if (!joined)
		{
			Chat_Model_JoinRoom chat_Model_JoinRoom = packet.model as Chat_Model_JoinRoom;
			if (chat_Model_JoinRoom != null && chat_Model_JoinRoom.errorType == CHAT_ERROR_TYPE.NO_ERROR)
			{
				long result = 0L;
				long.TryParse(packet.header.fromId, out result);
				long result2 = 0L;
				long.TryParse(chatWebSocket.fromId, out result2);
				if (result == result2)
				{
					joined = true;
					if (this.onJoin != null)
					{
						this.onJoin(chat_Model_JoinRoom.errorType);
					}
				}
			}
		}
	}

	private void OnReceiveMessage(Chat_Model_BroadcastMessage_Response packet)
	{
		int result = 0;
		int.TryParse(packet.SenderId, out result);
		if (packet.Message.Contains(STAMP_SYMBOL_BEGIN))
		{
			string s = packet.Message.Substring(STAMP_SYMBOL_BEGIN.Length, 8);
			int result2 = -1;
			int.TryParse(s, out result2);
			if (this.onReceiveStamp != null)
			{
				this.onReceiveStamp(result, packet.SenderName, result2);
			}
		}
		else if (this.onReceiveText != null)
		{
			this.onReceiveText(result, packet.SenderName, packet.Message);
		}
	}

	private void OnReceivePartyInvite(Chat_Model_PartyInvite packet)
	{
		int result = 0;
		int.TryParse(packet.flag, out result);
		if (result > 0)
		{
			if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
			{
				MonoBehaviourSingleton<UserInfoManager>.I.SetPartyInviteChat(true);
			}
		}
		else if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			MonoBehaviourSingleton<UserInfoManager>.I.SetPartyInviteChat(false);
		}
	}

	private void OnReceiveRallyInvite(Chat_Model_RallyInvite packet)
	{
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			MonoBehaviourSingleton<UserInfoManager>.I.SetRallyInviteChat(true);
		}
	}

	private void OnReceiveResetDarkMarket(Chat_Model_ResetDarkMarket packet)
	{
		GameSaveData.instance.resetMarketTime = $"{packet.endDate.Substring(0, 4)}-{packet.endDate.Substring(4, 2)}-{packet.endDate.Substring(6, 2)} {packet.endDate.Substring(8, 2)}:{packet.endDate.Substring(10, 2)}:{packet.endDate.Substring(12, 2)}";
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.RESET_DARK_MARKET);
	}

	private void OnReceiveUpdateDarkMarket(Chat_Model_UpdateDarkMarket packet)
	{
		int result = 0;
		int.TryParse(packet.itemMarketId, out result);
		int result2 = 0;
		int.TryParse(packet.soldNum, out result2);
		if (result != 0 && result2 != 0)
		{
			MonoBehaviourSingleton<ShopManager>.I.UpdateDarkMarketUsedCount(result, result2);
		}
		MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_DARK_MARKET);
	}

	private IEnumerator ConnectProcess(Action<bool> onFinished)
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
			while (!chatWebSocket.IsConnected() && chatWebSocket.CurrentConnectionStatus != ChatWebSocket.CONNECTION_STATUS.ERROR && waitTimeRest > 0f)
			{
				waitTimeRest -= Time.deltaTime;
				yield return (object)null;
			}
			m_ConnectProcess = null;
			established = chatWebSocket.IsConnected();
			if (established)
			{
				chatWebSocket.OnClosed += OnWebSocketClosed;
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
			StopCoroutine(m_ConnectProcess);
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
		Disconnect(null);
		if ((bool)chatWebSocket)
		{
			UnityEngine.Object.Destroy(chatWebSocket.gameObject);
		}
	}

	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			if ((UnityEngine.Object)chatWebSocket != (UnityEngine.Object)null)
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
