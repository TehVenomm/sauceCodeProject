public class Coop_Model_PlayerPrayerStart : Coop_Model_ObjectBase
{
	public int sid;

	public Coop_Model_PlayerPrayerStart()
	{
		base.packetType = PACKET_TYPE.PLAYER_PRAYER_START;
	}

	public override bool IsForceHandleBefore(StageObject owner)
	{
		return true;
	}
}
