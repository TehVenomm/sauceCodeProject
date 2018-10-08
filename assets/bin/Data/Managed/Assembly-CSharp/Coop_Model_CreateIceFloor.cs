using System.Collections.Generic;
using UnityEngine;

public class Coop_Model_CreateIceFloor : Coop_Model_ObjectBase
{
	public string atkName;

	public List<Vector3> posList;

	public List<Quaternion> rotList;

	public Coop_Model_CreateIceFloor()
	{
		base.packetType = PACKET_TYPE.CREATE_ICE_FLOOR;
	}
}
