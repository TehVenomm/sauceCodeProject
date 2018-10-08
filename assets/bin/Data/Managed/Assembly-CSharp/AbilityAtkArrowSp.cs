public class AbilityAtkArrowSp : AbilityAtkWeapon
{
	public override void init(Player _player, string target, int val)
	{
		base.init(_player, "ARROW", val);
	}

	public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
	{
		AttackHitInfo.ToEnemy toEnemy = status.attackInfo.toEnemy;
		if (toEnemy == null || !toEnemy.isSpecialAttack)
		{
			return null;
		}
		return base.GetDamageRate(chara, status);
	}
}
