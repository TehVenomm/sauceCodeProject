using System;

public interface IChatConnection
{
	bool isEstablished
	{
		get;
	}

	event ChatRoom.OnJoin onJoin;

	event ChatRoom.OnReceiveText onReceiveText;

	event ChatRoom.OnReceiveStamp onReceiveStamp;

	event ChatRoom.OnReceiveNotification onReceiveNotification;

	event ChatRoom.OnDisconnect onDisconnect;

	void Connect();

	void Disconnect(Action onFinished = null);

	void Join(int roomNo, string userName);

	void SendText(string message);

	void SendStamp(int stampId);
}
