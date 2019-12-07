using System;

public class Chat_Model_SendToClanMessage_Request : Chat_Model_Base
{
	public string UserId
	{
		get;
		protected set;
	}

	public string NickName
	{
		get;
		protected set;
	}

	public string RoomId
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

	public Chat_Model_SendToClanMessage_Request()
	{
		m_packetType = CHAT_PACKET_TYPE.CLAN_SENDTO;
	}

	public override string Serialize()
	{
		string text = $"{int.Parse(UserId):D32}";
		string text2 = $"{int.Parse(RoomId):D32}";
		return $"{text}{text2}{TimeStampClien}{$"{NickName}:{Message}"}";
	}

	public override string ToString()
	{
		return Serialize();
	}

	public static Chat_Model_SendToClanMessage_Request Create(string user_id, string user_name, string room_id, string message)
	{
		string timeStampClien = DateTime.UtcNow.ToString("yyyyMMddhhmmssff");
		Chat_Model_SendToClanMessage_Request obj = new Chat_Model_SendToClanMessage_Request
		{
			UserId = user_id,
			NickName = user_name,
			RoomId = room_id,
			TimeStampClien = timeStampClien,
			Message = message
		};
		obj.payload = obj.Serialize();
		return obj;
	}
}
