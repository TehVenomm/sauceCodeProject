public class Coop_Model_PlayerPrayerEnd : Coop_Model_ObjectBase
{
	public int sid;

	public int reason;

	public Coop_Model_PlayerPrayerEnd()
	{
		base.packetType = PACKET_TYPE.PLAYER_PRAYER_END;
	}

	public override bool IsForceHandleBefore(StageObject owner)
	{
		return true;
	}
}
