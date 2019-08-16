public class Coop_Model_PlayerTeleportAvoid : Coop_Model_ObjectSyncPositionBase
{
	public Coop_Model_PlayerTeleportAvoid()
	{
		base.packetType = PACKET_TYPE.PLAYER_TELEPORT_AVOID;
	}

	public override bool IsHandleable(StageObject owner)
	{
		Character character = owner as Character;
		if (!character.IsChangeableAction((Character.ACTION_ID)46))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
