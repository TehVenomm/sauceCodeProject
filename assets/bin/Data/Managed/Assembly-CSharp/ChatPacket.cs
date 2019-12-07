public class ChatPacket
{
	public ChatPacketHeader header
	{
		get;
		set;
	}

	public Chat_Model_Base model
	{
		get;
		set;
	}

	public CHAT_PACKET_TYPE packetType => (CHAT_PACKET_TYPE)header.cmd;

	public string fromClientId => header.fromId;

	public T GetModel<T>() where T : Chat_Model_Base
	{
		return model as T;
	}

	public override string ToString()
	{
		return $"{header}{model}";
	}

	public virtual PacketStream Serialize()
	{
		return new PacketStream(ToString());
	}

	public static ChatPacket Deserialize(PacketStream stream)
	{
		if (stream.IsString())
		{
			return Parse(stream.ToString());
		}
		return null;
	}

	public static ChatPacket Parse(string str)
	{
		ChatPacket chatPacket = new ChatPacket();
		chatPacket.header = ChatPacketHeader.Parse(str);
		chatPacket.model = ParseModel(str, chatPacket.packetType);
		return chatPacket;
	}

	public static Chat_Model_Base ParseModel(string str, CHAT_PACKET_TYPE type)
	{
		switch (type)
		{
		case CHAT_PACKET_TYPE.JOIN_ROOM:
			return Chat_Model_JoinRoom.Parse(str);
		case CHAT_PACKET_TYPE.BROADCAST_ROOM:
			return Chat_Model_BroadcastMessage_Response.Parse(str);
		case CHAT_PACKET_TYPE.PARTY_INVITE:
			return Chat_Model_PartyInvite.Parse(str);
		case CHAT_PACKET_TYPE.CLAN_JOIN_ROOM:
			return Chat_Model_JoinClanRoom.Parse(str);
		case CHAT_PACKET_TYPE.CLAN_LEAVE_ROOM:
			return Chat_Model_LeaveClanRoom.Parse(str);
		case CHAT_PACKET_TYPE.CLAN_SENDTO:
			return Chat_Model_SendToClanMessage_Response.Parse(str);
		case CHAT_PACKET_TYPE.CLAN_BROADCAST_ROOM:
			return Chat_Model_BroadcastClanMessage_Response.Parse(str);
		case CHAT_PACKET_TYPE.CLAN_BROADCAST_STATUS:
			return Chat_Model_BroadcastClanStatus_Response.Parse(str);
		case CHAT_PACKET_TYPE.RALLY_INVITE:
			return Chat_Model_RallyInvite.Parse(str);
		case CHAT_PACKET_TYPE.DARK_MARKET_UPDATE:
			return Chat_Model_UpdateDarkMarket.Parse(str);
		case CHAT_PACKET_TYPE.DARK_MARKET_RESET:
			return Chat_Model_ResetDarkMarket.Parse(str);
		case CHAT_PACKET_TYPE.JACKPOT_WIN_UPDATE:
			return Chat_Model_JackpotWin.Parse(str);
		case CHAT_PACKET_TYPE.TRADING_POST_SOLD:
			return TradingPostSoldModel.Parse(str);
		default:
			return new Chat_Model_Base();
		}
	}
}
