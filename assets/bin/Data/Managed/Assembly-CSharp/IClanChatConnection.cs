using System;

public interface IClanChatConnection
{
	bool isEstablished
	{
		get;
	}

	bool isConnecting
	{
		get;
	}

	event ClanChatRoom.OnJoin onJoin;

	event ClanChatRoom.OnLeave onLeave;

	event ClanChatRoom.OnReceiveText onReceiveText;

	event ClanChatRoom.OnReceiveStamp onReceiveStamp;

	event ClanChatRoom.OnReceiveText onReceivePrivateText;

	event ClanChatRoom.OnReceiveUpdateStatus onReceiveUpdateStatus;

	event ClanChatRoom.OnReceiveStamp onReceivePrivateStamp;

	event ClanChatRoom.OnReceiveNotification onReceiveNotification;

	event ClanChatRoom.OnDisconnect onDisconnect;

	void Connect();

	void Disconnect(Action onFinished = null);

	void Join(int roomNo, string userName);

	void Leave(int roomNo, string userName);

	void SendText(string message);

	void SendStamp(int stampId);

	void SendPrivateText(string targetId, string message);

	void SendPrivateStamp(string targetId, int stampId);
}
