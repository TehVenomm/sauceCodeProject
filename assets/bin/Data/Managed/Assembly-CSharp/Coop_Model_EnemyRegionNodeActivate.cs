public class Coop_Model_EnemyRegionNodeActivate : Coop_Model_ObjectBase
{
	public int[] regionIDs;

	public bool isRandom;

	public int randomSelectedID = -1;

	public Coop_Model_EnemyRegionNodeActivate()
	{
		base.packetType = PACKET_TYPE.ENEMY_REGION_NODE_ACTIVATE;
	}
}
