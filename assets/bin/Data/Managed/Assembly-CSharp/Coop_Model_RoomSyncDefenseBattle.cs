public class Coop_Model_RoomSyncDefenseBattle : Coop_Model_Base
{
	public float endurance;

	public Coop_Model_RoomSyncDefenseBattle()
	{
		base.packetType = PACKET_TYPE.ROOM_SYNC_DEFENSE_BATTLE;
	}
}
