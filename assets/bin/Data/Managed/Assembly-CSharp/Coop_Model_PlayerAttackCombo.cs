public class Coop_Model_PlayerAttackCombo : Coop_Model_CharacterAttack
{
	public Coop_Model_PlayerAttackCombo()
	{
		base.packetType = PACKET_TYPE.PLAYER_ATTACK_COMBO;
	}

	public override bool IsHandleable(StageObject owner)
	{
		Player player = owner as Player;
		if (player.actionID == Character.ACTION_ID.ATTACK && !player.enableComboTrans)
		{
			return false;
		}
		return true;
	}
}
