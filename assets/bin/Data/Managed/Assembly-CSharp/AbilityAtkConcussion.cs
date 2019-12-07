public class AbilityAtkConcussion : AbilityAtkWeapon
{
	public override void init(Player _player, string target, int val)
	{
		base.init(_player, "TWO_HAND_SWORD", val);
	}

	public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
	{
		if (!player.IsOracleTwoHandSword())
		{
			return null;
		}
		Enemy enemy = chara as Enemy;
		if ((object)enemy == null)
		{
			return null;
		}
		if (!enemy.IsConcussion())
		{
			return null;
		}
		return base.GetDamageRate(chara, status);
	}
}
