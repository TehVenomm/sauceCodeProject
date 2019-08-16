public class AbilityAtkBurstArrowBombDamageUp : AbilityAtkWeapon
{
	public override void init(Player _player, string target, int val)
	{
		base.init(_player, "ARROW", val);
	}

	public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
	{
		if (!player.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.BURST))
		{
			return null;
		}
		if (status.attackInfo.attackType != AttackHitInfo.ATTACK_TYPE.BOMB && status.attackInfo.attackType != AttackHitInfo.ATTACK_TYPE.BOOST_BOMB)
		{
			return null;
		}
		return base.GetDamageRate(chara, status);
	}
}
