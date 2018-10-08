using System;

public class ClanChatOfflineConnection : IClanChatConnection
{
	public bool isEstablished => false;

	public bool isConnecting => false;

	public event ClanChatRoom.OnJoin onJoin;

	public event ClanChatRoom.OnLeave onLeave;

	public event ClanChatRoom.OnReceiveText onReceiveText;

	public event ClanChatRoom.OnReceiveStamp onReceiveStamp;

	public event ClanChatRoom.OnReceiveText onReceivePrivateText;

	public event ClanChatRoom.OnReceiveStamp onReceivePrivateStamp;

	public event ClanChatRoom.OnReceiveNotification onReceiveNotification;

	public event ClanChatRoom.OnDisconnect onDisconnect;

	public event ClanChatRoom.OnReceiveUpdateStatus onReceiveUpdateStatus;

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
			this.onJoin(CHAT_ERROR_TYPE.NO_ERROR, null);
		}
	}

	public void Leave(int roomNo, string userName)
	{
		if (this.onLeave != null)
		{
			this.onLeave(CHAT_ERROR_TYPE.NO_ERROR, null);
		}
	}

	public void SendText(string message)
	{
	}

	public void SendStamp(int stampId)
	{
	}

	public void SendPrivateText(string targetId, string message)
	{
	}

	public void SendPrivateStamp(string targetId, int stampId)
	{
	}
}
