using UnityEngine;

public class Coop_Model_PlayerRushAvoid : Coop_Model_ObjectSyncPositionBase
{
	public Vector3 inputVec;

	public Coop_Model_PlayerRushAvoid()
	{
		base.packetType = PACKET_TYPE.PLAYER_RUSH_AVOID;
	}

	public override bool IsHandleable(StageObject owner)
	{
		if (!(owner as Character).IsChangeableAction((Character.ACTION_ID)49))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
