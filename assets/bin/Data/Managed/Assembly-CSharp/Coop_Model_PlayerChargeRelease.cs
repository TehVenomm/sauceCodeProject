using UnityEngine;

public class Coop_Model_PlayerChargeRelease : Coop_Model_ObjectSyncPositionBase
{
	public float lerp_dir;

	public float charge_rate;

	public Vector3 act_pos = Vector3.zero;

	public bool act_pos_f;

	public bool isExRushCharge;

	public Coop_Model_PlayerChargeRelease()
	{
		base.packetType = PACKET_TYPE.PLAYER_CHARGE_RELEASE;
	}

	public override bool IsHandleable(StageObject owner)
	{
		Player player = owner as Player;
		if ((player.actionID == Character.ACTION_ID.ATTACK || player.actionID == (Character.ACTION_ID)21) && !player.enableInputCharge)
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
