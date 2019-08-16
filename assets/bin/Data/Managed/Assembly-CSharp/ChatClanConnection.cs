using System;

public class ChatClanConnection : IChatConnection
{
	private bool established;

	public bool isEstablished => established;

	public event ChatRoom.OnJoin onJoin;

	public event ChatRoom.OnReceiveText onReceiveText;

	public event ChatRoom.OnReceiveStamp onReceiveStamp;

	public event ChatRoom.OnReceiveNotification onReceiveNotification;

	public event ChatRoom.OnAfterSendUserMessage onAfterSendUserMessage;

	public event ChatRoom.OnDisconnect onDisconnect;

	public void Connect()
	{
		established = true;
	}

	public void Disconnect(Action onFinished = null)
	{
		established = false;
		onFinished?.Invoke();
	}

	public void Join(int roomNo, string userName)
	{
		established = true;
		if (this.onJoin != null)
		{
			this.onJoin(CHAT_ERROR_TYPE.NO_ERROR);
		}
	}

	public void SendText(string message)
	{
		if (MonoBehaviourSingleton<ClanMatchingManager>.IsValid())
		{
			MonoBehaviourSingleton<ClanMatchingManager>.I.ChatMessage(message);
		}
	}

	public void SendStamp(int stampId)
	{
		if (MonoBehaviourSingleton<ClanMatchingManager>.IsValid())
		{
			MonoBehaviourSingleton<ClanMatchingManager>.I.ChatStamp(stampId);
		}
	}

	public void OnReceiveMessage(int userId, string userName, string message, string chatItemId = "")
	{
		if (this.onReceiveText != null)
		{
			this.onReceiveText(userId, userName, message, chatItemId);
		}
	}

	public void OnReceiveStamp(int userId, string userName, int stampId, string chatItemId = "")
	{
		if (this.onReceiveStamp != null)
		{
			this.onReceiveStamp(userId, userName, stampId, chatItemId);
		}
	}

	public void OnReceiveNotification(string message, string chatItemId = "")
	{
		if (this.onReceiveNotification != null)
		{
			this.onReceiveNotification(message, chatItemId);
		}
	}

	public void OnReceiveMessageOld(int userId, string userName, string message, string chatItemId = "")
	{
		if (this.onReceiveText != null)
		{
			this.onReceiveText(userId, userName, message, chatItemId, isOldMessage: true);
		}
	}

	public void OnReceiveStampOld(int userId, string userName, int stampId, string chatItemId = "")
	{
		if (this.onReceiveStamp != null)
		{
			this.onReceiveStamp(userId, userName, stampId, chatItemId, isOldMessage: true);
		}
	}

	public void OnReceiveNotificationOld(string message, string chatItemId = "")
	{
		if (this.onReceiveNotification != null)
		{
			this.onReceiveNotification(message, chatItemId, isOldMessage: true);
		}
	}

	public void OnAfterSendUserMessage()
	{
		if (this.onAfterSendUserMessage != null)
		{
			this.onAfterSendUserMessage();
		}
	}
}
