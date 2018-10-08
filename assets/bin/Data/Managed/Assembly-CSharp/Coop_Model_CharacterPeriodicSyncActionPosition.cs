public class Coop_Model_CharacterPeriodicSyncActionPosition : Coop_Model_ObjectBase
{
	public Character.PeriodicSyncActionPositionInfo info;

	public Coop_Model_CharacterPeriodicSyncActionPosition()
	{
		base.packetType = PACKET_TYPE.CHARACTER_PERIODIC_SYNC_ACTION_POSITION;
	}
}
