public class AbilityAtkRevenge : AbilityAtkWeapon
{
	public override void init(Player _player, string target, int val)
	{
		base.init(_player, "ONE_HAND_SWORD", val);
	}

	public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
	{
		if (status.attackInfo.attackType != AttackHitInfo.ATTACK_TYPE.REVENGE)
		{
			return null;
		}
		return base.GetDamageRate(chara, status);
	}
}
