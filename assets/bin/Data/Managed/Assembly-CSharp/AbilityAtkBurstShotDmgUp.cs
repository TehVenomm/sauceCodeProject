public class AbilityAtkBurstShotDmgUp : AbilityAtkWeapon
{
	public override void init(Player _player, string target, int val)
	{
		base.init(_player, "TWO_HAND_SWORD", val);
	}

	public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
	{
		if (!player.IsBurstTwoHandSword())
		{
			return null;
		}
		if (status.attackInfo.attackType != AttackHitInfo.ATTACK_TYPE.BURST_THS_SINGLE_SHOT && status.attackInfo.attackType != AttackHitInfo.ATTACK_TYPE.BURST_THS_FULL_BURST)
		{
			return null;
		}
		return base.GetDamageRate(chara, status);
	}
}
