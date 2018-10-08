public class Coop_Model_RoomNotifyEncounterBoss : Coop_Model_Base
{
	public int mid;

	public int pid;

	public Coop_Model_RoomNotifyEncounterBoss()
	{
		base.packetType = PACKET_TYPE.ROOM_NOTIFY_ENCOUNTER_BOSS;
	}
}
