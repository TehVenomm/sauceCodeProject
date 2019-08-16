using System;

public class AbilityAtkEnemyType : AbilityAtkBase
{
	private ENEMY_TYPE type;

	public override void init(Player _player, string target, int val)
	{
		base.init(_player, target, val);
		type = ENEMY_TYPE.NONE;
		if (Enum.IsDefined(typeof(ENEMY_TYPE), target))
		{
			type = (ENEMY_TYPE)Enum.Parse(typeof(ENEMY_TYPE), target);
		}
	}

	public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
	{
		Enemy enemy = chara as Enemy;
		if (enemy == null)
		{
			return null;
		}
		if (type != enemy.GetEnemyType())
		{
			return null;
		}
		return attr;
	}
}
