public class Coop_Model_PlayerStopCounter : Coop_Model_ObjectBase
{
	public bool stop;

	public Coop_Model_PlayerStopCounter()
	{
		base.packetType = PACKET_TYPE.PLAYER_STOP_COUNTER;
	}

	public override bool IsForceHandleBefore(StageObject owner)
	{
		return true;
	}
}
