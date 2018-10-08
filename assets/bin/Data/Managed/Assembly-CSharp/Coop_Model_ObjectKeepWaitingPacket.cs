public class Coop_Model_ObjectKeepWaitingPacket : Coop_Model_ObjectBase
{
	public int type;

	public Coop_Model_ObjectKeepWaitingPacket()
	{
		base.packetType = PACKET_TYPE.OBJECT_KEEP_WAITING_PACKET;
	}
}
