using UnityEngine;

public class Coop_Model_CharacterReaction : Coop_Model_ObjectSyncPositionBase
{
	public int reactionType;

	public Vector3 blowForce = Vector3.zero;

	public float loopTime;

	public int targetId;

	public int deadReviveCount;

	public Coop_Model_CharacterReaction()
	{
		base.packetType = PACKET_TYPE.CHARACTER_REACTION;
	}

	public override bool IsForceHandleBefore(StageObject owner)
	{
		return true;
	}
}
