using System;

public class ChatRoom
{
	public delegate void OnJoin(CHAT_ERROR_TYPE errorType);

	public delegate void OnJoinClan(CHAT_ERROR_TYPE errorType, bool owner, string userId);

	public delegate void OnReceiveText(int userId, string userName, string message, string chatItemId, bool isOldMessage = false);

	public delegate void OnReceiveStamp(int userId, string userName, int stampId, string chatItemId, bool isOldMessage = false);

	public delegate void OnReceiveNotification(string message, string chatItemId, bool isOldMessage = false);

	public delegate void OnAfterSendUserMessage();

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

	public event OnReceiveNotification onReceiveNotification;

	public event OnAfterSendUserMessage onAfterSendUserMessage;

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
				this.connection.Disconnect();
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
		connection.onAfterSendUserMessage += _OnAfterSendUserMessage;
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

	private void _OnReceiveText(int userId, string userName, string message, string chatItemId, bool isOldMessage = false)
	{
		if (this.onReceiveText != null)
		{
			this.onReceiveText(userId, userName, message, chatItemId, isOldMessage);
		}
	}

	private void _OnReceiveStamp(int userId, string userName, int stampId, string chatItemId, bool isOldMessage = false)
	{
		if (this.onReceiveStamp != null)
		{
			this.onReceiveStamp(userId, userName, stampId, chatItemId, isOldMessage);
		}
	}

	private void _OnReceiveNotification(string message, string chatItemId, bool isOldMessage = false)
	{
		if (this.onReceiveNotification != null)
		{
			this.onReceiveNotification(message, chatItemId, isOldMessage);
		}
	}

	private void _OnAfterSendUserMessage()
	{
		if (this.onAfterSendUserMessage != null)
		{
			this.onAfterSendUserMessage();
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
