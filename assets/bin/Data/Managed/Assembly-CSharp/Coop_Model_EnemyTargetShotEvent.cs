using System.Collections.Generic;

public class Coop_Model_EnemyTargetShotEvent : Coop_Model_ObjectBase
{
	public List<Enemy.RandomShotInfo.TargetInfo> targets = new List<Enemy.RandomShotInfo.TargetInfo>();

	public Coop_Model_EnemyTargetShotEvent()
	{
		base.packetType = PACKET_TYPE.ENEMY_TARGRTSHOT_EVENT;
	}
}
