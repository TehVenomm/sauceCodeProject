public class Coop_Model_PlayerWarp : Coop_Model_ObjectSyncPositionBase
{
	public Coop_Model_PlayerWarp()
	{
		base.packetType = PACKET_TYPE.PLAYER_WARP;
	}

	public override bool IsHandleable(StageObject owner)
	{
		if (!(owner as Character).IsChangeableAction((Character.ACTION_ID)36))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
