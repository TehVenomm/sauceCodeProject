public class AbilityAtkTypeNormal : AbilityAtkWeapon
{
	public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
	{
		if (status.attackInfo.isSkillReference)
		{
			return null;
		}
		if (!InGameUtility.IsDamageUpAtkTypeNormal(player, status.attackInfo))
		{
			return null;
		}
		return base.GetDamageRate(chara, status);
	}
}
