public class Coop_Model_RoomChatStamp : Coop_Model_Base
{
	public int userId;

	public int stampId;

	public Coop_Model_RoomChatStamp()
	{
		base.packetType = PACKET_TYPE.ROOM_CHAT_STAMP;
	}

	public override string ToString()
	{
		return base.ToString() + $"userId={userId} stamId={stampId}";
	}
}
