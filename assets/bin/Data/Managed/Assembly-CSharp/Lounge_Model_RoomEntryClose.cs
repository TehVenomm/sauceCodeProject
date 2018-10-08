public class Lounge_Model_RoomEntryClose : Coop_Model_Base
{
	public int reason;

	public Lounge_Model_RoomEntryClose()
	{
		base.packetType = PACKET_TYPE.LOUNGE_ROOM_ENTRY_CLOSE;
	}

	public override string ToString()
	{
		return base.ToString() + ",reason=" + reason;
	}
}
