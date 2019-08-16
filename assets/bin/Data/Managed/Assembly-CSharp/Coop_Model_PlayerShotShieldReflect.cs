using UnityEngine;

public class Coop_Model_PlayerShotShieldReflect : Coop_Model_ObjectBase
{
	public string atkInfoName;

	public int damage;

	public int targetId;

	public Vector3 offsetPos;

	public Vector3 offsetRot;

	public Coop_Model_PlayerShotShieldReflect()
	{
		base.packetType = PACKET_TYPE.PLAYER_SHOT_SHIELD_REFLECT;
	}
}
