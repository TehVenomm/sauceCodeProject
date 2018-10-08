using System;

public class Chat_Model_BroadcastMessage_Request : Chat_Model_Base
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

	public Chat_Model_BroadcastMessage_Request()
	{
		m_packetType = CHAT_PACKET_TYPE.BROADCAST_ROOM;
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

	public static Chat_Model_BroadcastMessage_Request Create(string roomId, string message)
	{
		string timeStampClien = DateTime.UtcNow.ToString("yyyyMMddhhmmssff");
		Chat_Model_BroadcastMessage_Request chat_Model_BroadcastMessage_Request = new Chat_Model_BroadcastMessage_Request();
		chat_Model_BroadcastMessage_Request.RoomId = roomId;
		chat_Model_BroadcastMessage_Request.TimeStampClien = timeStampClien;
		chat_Model_BroadcastMessage_Request.Message = message;
		Chat_Model_BroadcastMessage_Request chat_Model_BroadcastMessage_Request2 = chat_Model_BroadcastMessage_Request;
		chat_Model_BroadcastMessage_Request2.payload = chat_Model_BroadcastMessage_Request2.Serialize();
		return chat_Model_BroadcastMessage_Request2;
	}
}
