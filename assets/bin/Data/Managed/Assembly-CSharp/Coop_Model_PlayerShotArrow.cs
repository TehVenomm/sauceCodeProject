using UnityEngine;

public class Coop_Model_PlayerShotArrow : Coop_Model_ObjectSyncPositionBase
{
	public Vector3 shot_pos = Vector3.get_zero();

	public Quaternion shot_rot = default(Quaternion);

	public string attack_name;

	public float attack_rate;

	public int shot_count;

	public bool is_sit_shot;

	public bool is_aim_end = true;

	public Coop_Model_PlayerShotArrow()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
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
