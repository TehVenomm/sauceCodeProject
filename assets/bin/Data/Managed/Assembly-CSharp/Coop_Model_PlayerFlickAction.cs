using UnityEngine;

public class Coop_Model_PlayerFlickAction : Coop_Model_ObjectSyncPositionBase
{
	public Vector3 inputVec;

	public Coop_Model_PlayerFlickAction()
	{
		base.packetType = PACKET_TYPE.PLAYER_FLICK_ACTION;
	}

	public override bool IsHandleable(StageObject owner)
	{
		Character character = owner as Character;
		if (!character.IsChangeableAction((Character.ACTION_ID)42))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
