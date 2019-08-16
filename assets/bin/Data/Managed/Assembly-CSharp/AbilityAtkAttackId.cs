public class AbilityAtkAttackId : AbilityAtkBase
{
	public int attackId = -1;

	public override void init(Player _player, string target, int val)
	{
		base.init(_player, target, val);
		if (int.TryParse(target, out int result))
		{
			attackId = result;
		}
		else
		{
			attackId = -1;
		}
	}

	public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
	{
		if (player.attackID != attackId)
		{
			return null;
		}
		return base.GetDamageRate(chara, status);
	}
}
