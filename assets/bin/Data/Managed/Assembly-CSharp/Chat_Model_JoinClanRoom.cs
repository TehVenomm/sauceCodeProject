public class Chat_Model_JoinClanRoom : Chat_Model_Base
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

	public Chat_Model_JoinClanRoom()
	{
		m_packetType = CHAT_PACKET_TYPE.CLAN_JOIN_ROOM;
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
		Chat_Model_JoinClanRoom chat_Model_JoinClanRoom = null;
		string s = str.Substring(72, 8);
		if (int.Parse(s) == 1)
		{
			chat_Model_JoinClanRoom = new Chat_Model_JoinClanRoom
			{
				m_packetType = CHAT_PACKET_TYPE.CLAN_JOIN_ROOM,
				payload = str.Substring(Chat_Model_Base.PAYLOAD_ORIGIN_INDEX),
				RoomId = str.Substring(40, 32),
				Owner = int.Parse(s)
			};
			string errorType = str.Substring(80);
			chat_Model_JoinClanRoom.SetErrorType(errorType);
		}
		else
		{
			chat_Model_JoinClanRoom = new Chat_Model_JoinClanRoom
			{
				m_packetType = CHAT_PACKET_TYPE.CLAN_JOIN_ROOM,
				payload = str.Substring(Chat_Model_Base.PAYLOAD_ORIGIN_INDEX),
				RoomId = str.Substring(40, 32),
				UserId = str.Substring(80, 32),
				Owner = int.Parse(s)
			};
			chat_Model_JoinClanRoom.SetErrorType("00000000");
		}
		return chat_Model_JoinClanRoom;
	}

	public static Chat_Model_JoinClanRoom Create(string roomId, string nickName)
	{
		Chat_Model_JoinClanRoom obj = new Chat_Model_JoinClanRoom
		{
			RoomId = roomId,
			NickName = nickName
		};
		obj.payload = obj.Serialize();
		return obj;
	}
}
