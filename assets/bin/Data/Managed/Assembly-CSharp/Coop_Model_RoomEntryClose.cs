public class Coop_Model_RoomEntryClose : Coop_Model_Base
{
	public int reason;

	public Coop_Model_RoomEntryClose()
	{
		base.packetType = PACKET_TYPE.ROOM_ENTRY_CLOSE;
	}

	public override string ToString()
	{
		return base.ToString() + ",reason=" + reason;
	}
}
