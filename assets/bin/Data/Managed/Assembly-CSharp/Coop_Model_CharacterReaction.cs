using UnityEngine;

public class Coop_Model_CharacterReaction : Coop_Model_ObjectSyncPositionBase
{
	public int reactionType;

	public Vector3 blowForce = Vector3.get_zero();

	public float loopTime;

	public int targetId;

	public Coop_Model_CharacterReaction()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		base.packetType = PACKET_TYPE.CHARACTER_REACTION;
	}

	public override bool IsForceHandleBefore(StageObject owner)
	{
		return true;
	}
}
