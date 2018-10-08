using System.Collections.Generic;
using UnityEngine;

public class Coop_Model_EnemyShot : Coop_Model_ObjectBase
{
	public string atkName;

	public List<Vector3> posList;

	public List<Quaternion> rotList;

	public Coop_Model_EnemyShot()
	{
		base.packetType = PACKET_TYPE.ENEMY_SHOT;
	}
}
