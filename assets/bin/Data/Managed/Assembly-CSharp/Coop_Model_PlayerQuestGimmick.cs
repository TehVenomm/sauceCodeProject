using UnityEngine;

public class Coop_Model_PlayerQuestGimmick : Coop_Model_ObjectSyncPositionBase
{
	public Vector3 act_pos = Vector3.zero;

	public bool act_pos_f;

	public int gimmickId;

	public Coop_Model_PlayerQuestGimmick()
	{
		base.packetType = PACKET_TYPE.PLAYER_QUEST_GIMMICK;
	}
}
