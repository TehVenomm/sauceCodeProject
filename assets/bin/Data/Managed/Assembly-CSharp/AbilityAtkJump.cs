public class AbilityAtkJump : AbilityAtkWeapon
{
	public override void init(Player _player, string target, int val)
	{
		base.init(_player, "SPEAR", val);
	}

	public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
	{
		if (status.attackInfo.attackType != AttackHitInfo.ATTACK_TYPE.JUMP)
		{
			return null;
		}
		return base.GetDamageRate(chara, status);
	}
}
