public class Chat_Model_LeaveClanRoom : Chat_Model_Base
{
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

	public int Owner
	{
		get;
		protected set;
	}

	public string Result
	{
		get;
		protected set;
	}

	public string UserId
	{
		get;
		protected set;
	}

	public Chat_Model_LeaveClanRoom()
	{
		m_packetType = CHAT_PACKET_TYPE.CLAN_LEAVE_ROOM;
	}

	public override string Serialize()
	{
		string arg = $"{0:D32}".Substring(RoomId.Length) + RoomId;
		return $"{arg}{NickName}";
	}

	public override string ToString()
	{
		return Serialize();
	}

	public static Chat_Model_Base Parse(string str)
	{
		Chat_Model_LeaveClanRoom chat_Model_LeaveClanRoom = null;
		string s = str.Substring(72, 8);
		if (int.Parse(s) == 1)
		{
			chat_Model_LeaveClanRoom = new Chat_Model_LeaveClanRoom
			{
				m_packetType = CHAT_PACKET_TYPE.CLAN_LEAVE_ROOM,
				payload = str.Substring(Chat_Model_Base.PAYLOAD_ORIGIN_INDEX),
				RoomId = str.Substring(40, 32),
				Owner = int.Parse(s)
			};
			string errorType = str.Substring(80);
			chat_Model_LeaveClanRoom.SetErrorType(errorType);
		}
		else
		{
			chat_Model_LeaveClanRoom = new Chat_Model_LeaveClanRoom
			{
				m_packetType = CHAT_PACKET_TYPE.CLAN_LEAVE_ROOM,
				payload = str.Substring(Chat_Model_Base.PAYLOAD_ORIGIN_INDEX),
				RoomId = str.Substring(40, 32),
				UserId = str.Substring(80, 32),
				Owner = int.Parse(s)
			};
			chat_Model_LeaveClanRoom.SetErrorType("00000000");
		}
		return chat_Model_LeaveClanRoom;
	}

	public static Chat_Model_LeaveClanRoom Create(string roomId, string nickName)
	{
		Chat_Model_LeaveClanRoom obj = new Chat_Model_LeaveClanRoom
		{
			RoomId = roomId,
			NickName = nickName
		};
		obj.payload = obj.Serialize();
		return obj;
	}
}
