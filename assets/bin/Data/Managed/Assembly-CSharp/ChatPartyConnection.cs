using System;

public class ChatPartyConnection : IChatConnection
{
	private bool established;

	public bool isEstablished => established;

	public event ChatRoom.OnJoin onJoin;

	public event ChatRoom.OnReceiveText onReceiveText;

	public event ChatRoom.OnReceiveStamp onReceiveStamp;

	public event ChatRoom.OnReceiveNotification onReceiveNotification;

	public event ChatRoom.OnDisconnect onDisconnect;

	public void Connect()
	{
		established = true;
	}

	public void Disconnect(Action onFinished = null)
	{
		if (MonoBehaviourSingleton<PartyNetworkManager>.IsValid())
		{
			MonoBehaviourSingleton<PartyNetworkManager>.I.Close(1000, "Bye!", null);
		}
		established = false;
		onFinished?.Invoke();
	}

	public void Join(int roomNo, string userName)
	{
		if (MonoBehaviourSingleton<PartyManager>.IsValid())
		{
			MonoBehaviourSingleton<PartyManager>.I.ConnectServer(null);
		}
		established = true;
		if (this.onJoin != null)
		{
			this.onJoin(CHAT_ERROR_TYPE.NO_ERROR);
		}
	}

	public void SendText(string message)
	{
		if (MonoBehaviourSingleton<PartyNetworkManager>.IsValid())
		{
			MonoBehaviourSingleton<PartyNetworkManager>.I.ChatMessage(message);
		}
		OnReceiveMessage(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name, message);
	}

	public void SendStamp(int stampId)
	{
		if (MonoBehaviourSingleton<PartyNetworkManager>.IsValid())
		{
			MonoBehaviourSingleton<PartyNetworkManager>.I.ChatStamp(stampId);
		}
		OnReceiveStamp(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.name, stampId);
	}

	public void OnReceiveMessage(int userId, string userName, string message)
	{
		if (isEstablished && this.onReceiveText != null)
		{
			this.onReceiveText(userId, userName, message);
		}
	}

	public void OnReceiveStamp(int userId, string userName, int stampId)
	{
		if (isEstablished && this.onReceiveStamp != null)
		{
			this.onReceiveStamp(userId, userName, stampId);
		}
	}

	public void OnReceiveNotification(string message)
	{
		if (isEstablished && this.onReceiveNotification != null)
		{
			this.onReceiveNotification(message);
		}
	}
}
