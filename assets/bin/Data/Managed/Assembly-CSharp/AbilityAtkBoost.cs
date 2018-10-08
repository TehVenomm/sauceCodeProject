public class AbilityAtkBoost : AbilityAtkWeapon
{
	public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
	{
		if (!player.isBoostMode)
		{
			return null;
		}
		return base.GetDamageRate(chara, status);
	}
}
