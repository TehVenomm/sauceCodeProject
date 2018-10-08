public class AbilityAtkEnemyName : AbilityAtkBase
{
	private string name;

	public override void init(Player _player, string target, int val)
	{
		base.init(_player, target, val);
		name = target;
	}

	public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
	{
		Enemy enemy = chara as Enemy;
		if (enemy == null)
		{
			return null;
		}
		if (name != enemy.enemyTableData.name)
		{
			return null;
		}
		return attr;
	}
}
