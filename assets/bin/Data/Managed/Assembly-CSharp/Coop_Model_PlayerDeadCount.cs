public class Coop_Model_PlayerDeadCount : Coop_Model_ObjectBase
{
	public float remaind_time;

	public bool stop;

	public bool requested;

	public Coop_Model_PlayerDeadCount()
	{
		base.packetType = PACKET_TYPE.PLAYER_DEAD_COUNT;
	}

	public override bool IsForceHandleBefore(StageObject owner)
	{
		return true;
	}
}
