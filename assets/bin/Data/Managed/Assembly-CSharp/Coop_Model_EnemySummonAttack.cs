using UnityEngine;

public class Coop_Model_EnemySummonAttack : Coop_Model_Base
{
	public int enemyId;

	public int attackId;

	public Vector3 summonPos;

	public Vector3 summonRot;

	public int targetId;

	public Coop_Model_EnemySummonAttack()
	{
		base.packetType = PACKET_TYPE.ENEMY_SUMMON_ATTACK;
	}
}
