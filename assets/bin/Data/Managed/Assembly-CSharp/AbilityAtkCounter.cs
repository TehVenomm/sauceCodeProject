public class AbilityAtkCounter : AbilityAtkWeapon
{
	public override void init(Player _player, string target, int val)
	{
		base.init(_player, "ONE_HAND_SWORD", val);
	}

	public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
	{
		if (status.attackInfo.attackType != AttackHitInfo.ATTACK_TYPE.COUNTER && status.attackInfo.attackType != AttackHitInfo.ATTACK_TYPE.COUNTER2)
		{
			return null;
		}
		return base.GetDamageRate(chara, status);
	}
}
