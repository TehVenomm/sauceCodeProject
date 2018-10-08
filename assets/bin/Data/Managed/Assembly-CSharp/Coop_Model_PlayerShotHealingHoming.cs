using UnityEngine;

public class Coop_Model_PlayerShotHealingHoming : Coop_Model_ObjectBase
{
	public string atkInfoName;

	public string launchNodeName;

	public Vector3 offsetPos;

	public Vector3 offsetRot;

	public int[] targetPlayerIDs;

	public int targetNum;

	public Coop_Model_PlayerShotHealingHoming()
	{
		base.packetType = PACKET_TYPE.PLAYER_SHOT_HEALING_HOMING;
	}
}
