using System;

public class ChatOfflineConnection : IChatConnection
{
	public bool isEstablished => false;

	public event ChatRoom.OnJoin onJoin;

	public event ChatRoom.OnReceiveText onReceiveText;

	public event ChatRoom.OnReceiveStamp onReceiveStamp;

	public event ChatRoom.OnReceiveNotification onReceiveNotification;

	public event ChatRoom.OnDisconnect onDisconnect;

	public void Connect()
	{
	}

	public void Disconnect(Action onFinished)
	{
		if (onFinished != null)
		{
			onFinished.Invoke();
		}
	}

	public void Join(int roomNo, string userName)
	{
		if (this.onJoin != null)
		{
			this.onJoin(CHAT_ERROR_TYPE.NO_ERROR);
		}
	}

	public void SendText(string message)
	{
	}

	public void SendStamp(int stampId)
	{
	}
}
