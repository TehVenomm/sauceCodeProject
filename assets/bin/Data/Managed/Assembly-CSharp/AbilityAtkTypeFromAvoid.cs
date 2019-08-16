public class AbilityAtkTypeFromAvoid : AbilityAtkWeapon
{
	public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
	{
		if (status.attackInfo.attackType != AttackHitInfo.ATTACK_TYPE.FROM_AVOID)
		{
			return null;
		}
		return base.GetDamageRate(chara, status);
	}
}
