public class Coop_Model_PlayerStoneCount : Coop_Model_ObjectBase
{
	public float remaind_time;

	public bool stop;

	public bool requested;

	public Coop_Model_PlayerStoneCount()
	{
		base.packetType = PACKET_TYPE.PLAYER_STONE_COUNT;
	}

	public override bool IsForceHandleBefore(StageObject owner)
	{
		return true;
	}
}
