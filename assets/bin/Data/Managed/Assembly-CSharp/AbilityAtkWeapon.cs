using System;

public class AbilityAtkWeapon : AbilityAtkBase
{
	private Player.ATTACK_MODE mode;

	public override void init(Player _player, string target, int val)
	{
		base.init(_player, target, val);
		mode = (Player.ATTACK_MODE)Enum.Parse(typeof(Player.ATTACK_MODE), target);
	}

	public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
	{
		Player.ATTACK_MODE attackMode = player.attackMode;
		if (status.attackMode != 0)
		{
			attackMode = status.attackMode;
		}
		if (mode == Player.ATTACK_MODE.NONE || mode == attackMode)
		{
			return attr;
		}
		return null;
	}
}
