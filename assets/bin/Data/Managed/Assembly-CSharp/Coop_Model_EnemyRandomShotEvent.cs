using System.Collections.Generic;
using UnityEngine;

public class Coop_Model_EnemyRandomShotEvent : Coop_Model_ObjectBase
{
	public List<Vector3> points = new List<Vector3>();

	public Coop_Model_EnemyRandomShotEvent()
	{
		base.packetType = PACKET_TYPE.ENEMY_RANDOMSHOT_EVENT;
	}
}
