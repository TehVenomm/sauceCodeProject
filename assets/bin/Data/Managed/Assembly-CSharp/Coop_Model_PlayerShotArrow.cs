using UnityEngine;

public class Coop_Model_PlayerShotArrow : Coop_Model_ObjectSyncPositionBase
{
	public Vector3 shot_pos = Vector3.zero;

	public Quaternion shot_rot;

	public string attack_name;

	public float attack_rate;

	public int shot_count;

	public bool is_sit_shot;

	public bool is_aim_end = true;

	public Coop_Model_PlayerShotArrow()
	{
		base.packetType = PACKET_TYPE.PLAYER_SHOT_ARROW;
	}

	public override bool IsHandleable(StageObject owner)
	{
		Player player = owner as Player;
		if (player.actionID == Character.ACTION_ID.ATTACK && player.shotArrowCount < shot_count)
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
