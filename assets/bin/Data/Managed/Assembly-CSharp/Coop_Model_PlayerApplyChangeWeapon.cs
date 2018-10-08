using Network;

public class Coop_Model_PlayerApplyChangeWeapon : Coop_Model_ObjectBase
{
	public CharaInfo.EquipItem item;

	public int index;

	public Coop_Model_PlayerApplyChangeWeapon()
	{
		base.packetType = PACKET_TYPE.PLAYER_APPLY_CHANGE_WEAPON;
	}

	public override bool IsHandleable(StageObject owner)
	{
		Player player = owner as Player;
		if (player.actionID == (Character.ACTION_ID)26 && !player.IsValidWaitingPacket(StageObject.WAITING_PACKET.PLAYER_APPLY_CHANGE_WEAPON))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
