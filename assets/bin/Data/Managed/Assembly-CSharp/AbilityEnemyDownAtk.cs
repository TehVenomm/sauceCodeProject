public class AbilityEnemyDownAtk : AbilityAtkBase
{
	public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
	{
		if (status.weakState != Enemy.WEAK_STATE.DOWN)
		{
			return null;
		}
		return base.GetDamageRate(chara, status);
	}
}
