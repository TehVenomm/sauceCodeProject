public class AbilityAtkShadowSealing : AbilityAtkWeapon
{
	public override void init(Player _player, string target, int val)
	{
		base.init(_player, "ARROW", val);
	}

	public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
	{
		if (!player.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.HEAT))
		{
			return null;
		}
		Enemy enemy = chara as Enemy;
		if ((object)enemy == null)
		{
			return null;
		}
		if (!enemy.IsDebuffShadowSealing())
		{
			return null;
		}
		return base.GetDamageRate(chara, status);
	}
}
