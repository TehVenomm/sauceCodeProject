public class Coop_Model_PlayerGetRareDrop : Coop_Model_ObjectBase
{
	public int type;

	public int item_id;

	public Coop_Model_PlayerGetRareDrop()
	{
		base.packetType = PACKET_TYPE.PLAYER_GET_RAREDROP;
	}
}
