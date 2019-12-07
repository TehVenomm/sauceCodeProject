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
		string arg = $"{0:D32}".Substring(RoomId.Length) + RoomId;
		return $"{arg}{TimeStampClien}{Message}";
	}

	public override string ToString()
	{
		return Serialize();
	}

	public static Chat_Model_BroadcastClanMessage_Request Create(string roomId, string name, string message)
	{
		string timeStampClien = DateTime.UtcNow.ToString("yyyyMMddhhmmssff");
		Chat_Model_BroadcastClanMessage_Request obj = new Chat_Model_BroadcastClanMessage_Request
		{
			RoomId = roomId,
			TimeStampClien = timeStampClien,
			Message = message,
			NickName = name
		};
		obj.payload = obj.Serialize();
		return obj;
	}
}
