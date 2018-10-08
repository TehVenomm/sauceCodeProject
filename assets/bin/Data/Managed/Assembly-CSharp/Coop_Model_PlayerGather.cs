using UnityEngine;

public class Coop_Model_PlayerGather : Coop_Model_ObjectSyncPositionBase
{
	public Vector3 act_pos = Vector3.get_zero();

	public bool act_pos_f;

	public int point_id;

	public Coop_Model_PlayerGather()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		base.packetType = PACKET_TYPE.PLAYER_GATHER;
	}

	public override bool IsHandleable(StageObject owner)
	{
		Character character = owner as Character;
		if (!character.IsChangeableAction((Character.ACTION_ID)26))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
