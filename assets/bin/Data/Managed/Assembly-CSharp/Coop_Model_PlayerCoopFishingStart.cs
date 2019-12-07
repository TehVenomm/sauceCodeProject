using UnityEngine;

public class Coop_Model_PlayerCoopFishingStart : Coop_Model_ObjectSyncPositionBase
{
	public Vector3 actPos;

	public bool actPosFlag;

	public int gimmickId;

	public Coop_Model_PlayerCoopFishingStart()
	{
		base.packetType = PACKET_TYPE.PLAYER_COOP_FISHING_START;
	}

	public override bool IsHandleable(StageObject owner)
	{
		if (!(owner as Character).IsChangeableAction((Character.ACTION_ID)41))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
