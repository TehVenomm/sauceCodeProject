using UnityEngine;

public class Coop_Model_PlayerSpecialAction : Coop_Model_ObjectSyncPositionBase
{
	public Vector3 act_pos = Vector3.zero;

	public bool act_pos_f;

	public bool start_effect;

	public bool isSuccess;

	public Coop_Model_PlayerSpecialAction()
	{
		base.packetType = PACKET_TYPE.PLAYER_SPECIAL_ACTION;
	}

	public override bool IsHandleable(StageObject owner)
	{
		Character character = owner as Character;
		if (!character.IsChangeableAction((Character.ACTION_ID)32))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
