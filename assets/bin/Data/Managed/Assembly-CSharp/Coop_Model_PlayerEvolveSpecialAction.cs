public class Coop_Model_PlayerEvolveSpecialAction : Coop_Model_ObjectSyncPositionBase
{
	public Coop_Model_PlayerEvolveSpecialAction()
	{
		base.packetType = PACKET_TYPE.PLAYER_EVOLVE_SPECIAL_ACTION;
	}

	public override bool IsHandleable(StageObject owner)
	{
		Player player = owner as Player;
		if (object.ReferenceEquals(player, null))
		{
			return true;
		}
		if (!player.IsChangeableAction((Character.ACTION_ID)37))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
