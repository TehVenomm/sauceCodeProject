using System;

public class ChatRoom
{
	public delegate void OnJoin(CHAT_ERROR_TYPE errorType);

	public delegate void OnJoinClan(CHAT_ERROR_TYPE errorType, bool owner, string userId);

	public delegate void OnReceiveText(int userId, string userName, string message);

	public delegate void OnReceiveStamp(int userId, string userName, int stampId);

	public delegate void OnReceiveNotification(string message);

	public delegate void OnDisconnect();

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

	public IChatConnection connection
	{
		get;
		private set;
	}

	public event OnJoin onJoin;

	public event OnJoinClan onJoinClan;

	public event OnReceiveText onReceiveText;

	public event OnReceiveStamp onReceiveStamp;

	public event OnReceiveText onReceivePrivateText;

	public event OnReceiveStamp onReceivePrivateStamp;

	public event OnReceiveNotification onReceiveNotification;

	public event OnDisconnect onDisconnect;

	public ChatRoom()
	{
		GlobalSettingsManager.ChatParam chatParam = MonoBehaviourSingleton<GlobalSettingsManager>.I.chatParam;
		sendLimitter = new ChatSendLimitter(chatParam.limitCount, chatParam.limitDuration);
	}

	public bool CanSendMessage()
	{
		return !sendLimitter.IsLimit();
	}

	public void SetConnection(IChatConnection connection)
	{
		if (this.connection != null)
		{
			if (this.connection.isEstablished)
			{
				this.connection.Disconnect(null);
			}
			connection.onReceiveText -= _OnReceiveText;
			connection.onReceiveStamp -= _OnReceiveStamp;
			connection.onReceiveNotification -= _OnReceiveNotification;
		}
		this.connection = connection;
		connection.onJoin += _OnJoin;
		connection.onReceiveText += _OnReceiveText;
		connection.onReceiveStamp += _OnReceiveStamp;
		connection.onReceiveNotification += _OnReceiveNotification;
		connection.onDisconnect += _OnDisconnect;
	}

	public void JoinRoom(int roomNo)
	{
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			connection.Join(roomNo, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name);
		}
	}

	public void JoinClanRoom(int roomNo)
	{
		if (MonoBehaviourSingleton<UserInfoManager>.IsValid())
		{
			connection.Join(roomNo, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name);
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

	public void Disconnect(Action onFinished = null)
	{
		connection.Disconnect(onFinished);
	}

	private void _OnJoin(CHAT_ERROR_TYPE errorType)
	{
		if (this.onJoin != null)
		{
			this.onJoin(errorType);
		}
	}

	private void _OnJoinClan(CHAT_ERROR_TYPE errorType, bool isOwner, string userId)
	{
		if (this.onJoinClan != null)
		{
			this.onJoinClan(errorType, isOwner, userId);
		}
	}

	private void _OnReceiveText(int userId, string userName, string message)
	{
		if (this.onReceiveText != null)
		{
			this.onReceiveText(userId, userName, message);
		}
	}

	private void _OnReceiveStamp(int userId, string userName, int stampId)
	{
		if (this.onReceiveStamp != null)
		{
			this.onReceiveStamp(userId, userName, stampId);
		}
	}

	private void _OnReceivePrivateText(int userId, string userName, string message)
	{
		if (this.onReceivePrivateText != null)
		{
			this.onReceivePrivateText(userId, userName, message);
		}
	}

	private void _OnReceivePrivateStamp(int userId, string userName, int stampId)
	{
		if (this.onReceivePrivateStamp != null)
		{
			this.onReceivePrivateStamp(userId, userName, stampId);
		}
	}

	private void _OnReceiveNotification(string message)
	{
		if (this.onReceiveNotification != null)
		{
			this.onReceiveNotification(message);
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
