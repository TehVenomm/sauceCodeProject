public class Coop_Model_PlayerGrabbedEnd : Coop_Model_ObjectSyncPositionBase
{
	public float angle;

	public float power;

	public Coop_Model_PlayerGrabbedEnd()
	{
		base.packetType = PACKET_TYPE.PLAYER_GRABBED_END;
	}

	public override bool IsHandleable(StageObject owner)
	{
		return base.IsHandleable(owner);
	}
}
