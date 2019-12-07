public class Coop_Model_PlayerAvoid : Coop_Model_ObjectSyncPositionBase
{
	public Coop_Model_PlayerAvoid()
	{
		base.packetType = PACKET_TYPE.PLAYER_AVOID;
	}

	public override bool IsHandleable(StageObject owner)
	{
		if (!(owner as Character).IsChangeableAction(Character.ACTION_ID.MAX))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
