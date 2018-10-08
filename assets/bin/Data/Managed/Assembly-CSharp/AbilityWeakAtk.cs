public class AbilityWeakAtk : AbilityAtkBase
{
	public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
	{
		if (!Enemy.IsWeakStateDisplaySign(status.weakState))
		{
			return null;
		}
		return base.GetDamageRate(chara, status);
	}
}
