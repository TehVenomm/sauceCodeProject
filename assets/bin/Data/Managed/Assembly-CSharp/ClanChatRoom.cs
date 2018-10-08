using Network;
using System;

public class ClanChatRoom
{
	public delegate void OnJoin(CHAT_ERROR_TYPE errorType, string userId);

	public delegate void OnLeave(CHAT_ERROR_TYPE errorType, string userId);

	public delegate void OnReceiveText(ClanChatLogMessageData clanChatMsgData);

	public delegate void OnReceiveStamp(ClanChatLogMessageData clanChatMsgData);

	public delegate void OnReceiveNotification(string message);

	public delegate void OnDisconnect();

	public delegate void OnReceiveUpdateStatus(ClanUpdateStatusData statusData);

	private ChatSendLimitter sendLimitter;

	public string FromId
	{
		get;
		set;
	}

	public string MyName
	{
		get;
		private set;
	}

	public string RoomId
	{
		get;
		private set;
	}

	public int RoomNo
	{
		get;
		private set;
	}

	public bool HasConnect => connection != null && connection.isEstablished;

	public bool IsConnecting => connection != null && connection.isConnecting;

	public IClanChatConnection connection
	{
		get;
		private set;
	}

	public event OnJoin onJoin;

	public event OnLeave onLeave;

	public event OnReceiveText onReceiveText;

	public event OnReceiveStamp onReceiveStamp;

	public event OnReceiveText onReceivePrivateText;

	public event OnReceiveStamp onReceivePrivateStamp;

	public event OnReceiveNotification onReceiveNotification;

	public event OnReceiveUpdateStatus onReceiveUpdateStatus;

	public event OnDisconnect onDisconnect;

	public ClanChatRoom()
	{
		GlobalSettingsManager.ChatParam chatParam = MonoBehaviourSingleton<GlobalSettingsManager>.I.chatParam;
		sendLimitter = new ChatSendLimitter(chatParam.limitCount, chatParam.limitDuration);
	}

	public bool CanSendMessage()
	{
		return !sendLimitter.IsLimit();
	}

	public void SetConnection(IClanChatConnection connection)
	{
		if (this.connection != null)
		{
			if (this.connection.isEstablished)
			{
				this.connection.Disconnect(null);
			}
			connection.onJoin -= _OnJoin;
			connection.onLeave -= _OnLeave;
			connection.onReceiveText -= _OnReceiveText;
			connection.onReceiveStamp -= _OnReceiveStamp;
			connection.onReceivePrivateText -= _OnReceivePrivateText;
			connection.onReceivePrivateStamp -= _OnReceivePrivateStamp;
			connection.onReceiveNotification -= _OnReceiveNotification;
			connection.onReceiveUpdateStatus -= _OnReceiveUpdateStatus;
		}
		this.connection = connection;
		connection.onJoin += _OnJoin;
		connection.onLeave += _OnLeave;
		connection.onReceiveText += _OnReceiveText;
		connection.onReceiveStamp += _OnReceiveStamp;
		connection.onReceivePrivateText += _OnReceivePrivateText;
		connection.onReceivePrivateStamp += _OnReceivePrivateStamp;
		connection.onReceiveNotification += _OnReceiveNotification;
		connection.onDisconnect += _OnDisconnect;
		connection.onReceiveUpdateStatus += _OnReceiveUpdateStatus;
	}

	public void JoinRoom(int roomNo)
	{
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			connection.Join(roomNo, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name);
		}
	}

	public void LeaveRoom(int roomNo)
	{
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			connection.Leave(roomNo, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name);
		}
	}

	public bool SendMessage(string message)
	{
		if (sendLimitter.IsLimit())
		{
			return false;
		}
		sendLimitter.Touch();
		connection.SendText(message);
		return !sendLimitter.IsLimit();
	}

	public bool SendStamp(int stampId)
	{
		if (sendLimitter.IsLimit())
		{
			return false;
		}
		sendLimitter.Touch();
		connection.SendStamp(stampId);
		return !sendLimitter.IsLimit();
	}

	public bool SendPrivateMessage(string target_id, string message)
	{
		if (sendLimitter.IsLimit())
		{
			return false;
		}
		sendLimitter.Touch();
		connection.SendPrivateText(target_id, message);
		return !sendLimitter.IsLimit();
	}

	public bool SendPrivateStamp(string target_id, int stampId)
	{
		if (sendLimitter.IsLimit())
		{
			return false;
		}
		sendLimitter.Touch();
		connection.SendPrivateStamp(target_id, stampId);
		return !sendLimitter.IsLimit();
	}

	public void Disconnect(Action onFinished = null)
	{
		connection.Disconnect(onFinished);
	}

	private void _OnJoin(CHAT_ERROR_TYPE errorType, string userId)
	{
		if (this.onJoin != null)
		{
			this.onJoin(errorType, userId);
		}
	}

	private void _OnLeave(CHAT_ERROR_TYPE errorType, string userId)
	{
		if (this.onLeave != null)
		{
			this.onLeave(errorType, userId);
		}
	}

	private void _OnReceiveText(ClanChatLogMessageData clanChatMsgData)
	{
		if (this.onReceiveText != null)
		{
			this.onReceiveText(clanChatMsgData);
		}
	}

	private void _OnReceiveStamp(ClanChatLogMessageData clanChatMsgData)
	{
		if (this.onReceiveStamp != null)
		{
			this.onReceiveStamp(clanChatMsgData);
		}
	}

	private void _OnReceivePrivateText(ClanChatLogMessageData clanChatMsgData)
	{
		if (this.onReceivePrivateText != null)
		{
			this.onReceivePrivateText(clanChatMsgData);
		}
	}

	private void _OnReceivePrivateStamp(ClanChatLogMessageData clanChatMsgData)
	{
		if (this.onReceivePrivateStamp != null)
		{
			this.onReceivePrivateStamp(clanChatMsgData);
		}
	}

	private void _OnReceiveNotification(string message)
	{
		if (this.onReceiveNotification != null)
		{
			this.onReceiveNotification(message);
		}
	}

	private void _OnReceiveUpdateStatus(ClanUpdateStatusData clanUpdateStatusData)
	{
		if (this.onReceiveUpdateStatus != null)
		{
			this.onReceiveUpdateStatus(clanUpdateStatusData);
		}
	}

	private void _OnDisconnect()
	{
		if (this.onDisconnect != null)
		{
			this.onDisconnect();
		}
	}

	public override string ToString()
	{
		return connection.ToString();
	}
}
