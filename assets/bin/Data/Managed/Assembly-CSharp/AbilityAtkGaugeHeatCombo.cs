public class AbilityAtkGaugeHeatCombo : AbilityAtkWeapon
{
	public override void init(Player _player, string target, int val)
	{
		base.init(_player, "TWO_HAND_SWORD", val);
	}

	public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
	{
		if (!player.CheckAttackModeAndSpType(Player.ATTACK_MODE.TWO_HAND_SWORD, SP_ATTACK_TYPE.HEAT))
		{
			return null;
		}
		if (status.attackInfo.attackType != AttackHitInfo.ATTACK_TYPE.THS_HEAT_COMBO)
		{
			return null;
		}
		if (player.useGaugeLevel <= 0)
		{
			return null;
		}
		return base.GetDamageRate(chara, status);
	}
}
