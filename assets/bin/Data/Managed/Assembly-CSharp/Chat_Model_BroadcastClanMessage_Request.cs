using System;

public class Chat_Model_BroadcastClanMessage_Request : Chat_Model_Base
{
	public string RoomId
	{
		get;
		protected set;
	}

	public string NickName
	{
		get;
		protected set;
	}

	public int RoomNumber
	{
		get;
		protected set;
	}

	public string TimeStampClien
	{
		get;
		protected set;
	}

	public string Message
	{
		get;
		protected set;
	}

	public Chat_Model_BroadcastClanMessage_Request()
	{
		m_packetType = CHAT_PACKET_TYPE.CLAN_BROADCAST_ROOM;
	}

	public override string Serialize()
	{
		string text = $"{0:D32}";
		string arg = text.Substring(RoomId.Length) + RoomId;
		return $"{arg}{TimeStampClien}{Message}";
	}

	public override string ToString()
	{
		return Serialize();
	}

	public static Chat_Model_BroadcastClanMessage_Request Create(string roomId, string name, string message)
	{
		string timeStampClien = DateTime.UtcNow.ToString("yyyyMMddhhmmssff");
		Chat_Model_BroadcastClanMessage_Request chat_Model_BroadcastClanMessage_Request = new Chat_Model_BroadcastClanMessage_Request();
		chat_Model_BroadcastClanMessage_Request.RoomId = roomId;
		chat_Model_BroadcastClanMessage_Request.TimeStampClien = timeStampClien;
		chat_Model_BroadcastClanMessage_Request.Message = message;
		chat_Model_BroadcastClanMessage_Request.NickName = name;
		Chat_Model_BroadcastClanMessage_Request chat_Model_BroadcastClanMessage_Request2 = chat_Model_BroadcastClanMessage_Request;
		chat_Model_BroadcastClanMessage_Request2.payload = chat_Model_BroadcastClanMessage_Request2.Serialize();
		return chat_Model_BroadcastClanMessage_Request2;
	}
}
