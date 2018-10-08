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
		string text = $"{0:D32}";
		string arg = text.Substring(RoomId.Length) + RoomId;
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
		int num = int.Parse(s);
		if (num == 1)
		{
			Chat_Model_JoinClanRoom chat_Model_JoinClanRoom2 = new Chat_Model_JoinClanRoom();
			chat_Model_JoinClanRoom2.m_packetType = CHAT_PACKET_TYPE.CLAN_JOIN_ROOM;
			chat_Model_JoinClanRoom2.payload = str.Substring(Chat_Model_Base.PAYLOAD_ORIGIN_INDEX);
			chat_Model_JoinClanRoom2.RoomId = str.Substring(40, 32);
			chat_Model_JoinClanRoom2.Owner = int.Parse(s);
			chat_Model_JoinClanRoom = chat_Model_JoinClanRoom2;
			string errorType = str.Substring(80);
			chat_Model_JoinClanRoom.SetErrorType(errorType);
		}
		else
		{
			Chat_Model_JoinClanRoom chat_Model_JoinClanRoom2 = new Chat_Model_JoinClanRoom();
			chat_Model_JoinClanRoom2.m_packetType = CHAT_PACKET_TYPE.CLAN_JOIN_ROOM;
			chat_Model_JoinClanRoom2.payload = str.Substring(Chat_Model_Base.PAYLOAD_ORIGIN_INDEX);
			chat_Model_JoinClanRoom2.RoomId = str.Substring(40, 32);
			chat_Model_JoinClanRoom2.UserId = str.Substring(80, 32);
			chat_Model_JoinClanRoom2.Owner = int.Parse(s);
			chat_Model_JoinClanRoom = chat_Model_JoinClanRoom2;
			chat_Model_JoinClanRoom.SetErrorType("00000000");
		}
		return chat_Model_JoinClanRoom;
	}

	public static Chat_Model_JoinClanRoom Create(string roomId, string nickName)
	{
		Chat_Model_JoinClanRoom chat_Model_JoinClanRoom = new Chat_Model_JoinClanRoom();
		chat_Model_JoinClanRoom.RoomId = roomId;
		chat_Model_JoinClanRoom.NickName = nickName;
		Chat_Model_JoinClanRoom chat_Model_JoinClanRoom2 = chat_Model_JoinClanRoom;
		chat_Model_JoinClanRoom2.payload = chat_Model_JoinClanRoom2.Serialize();
		return chat_Model_JoinClanRoom2;
	}
}
