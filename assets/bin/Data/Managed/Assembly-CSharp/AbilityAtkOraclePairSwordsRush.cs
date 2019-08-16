public class AbilityAtkOraclePairSwordsRush : AbilityAtkWeapon
{
	public override void init(Player _player, string target, int val)
	{
		base.init(_player, "PAIR_SWORDS", val);
	}

	public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
	{
		if (!player.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.ORACLE))
		{
			return null;
		}
		if (status.attackInfo.attackType != AttackHitInfo.ATTACK_TYPE.PAIR_SWORDS_ORACLE_RUSH && status.attackInfo.attackType != AttackHitInfo.ATTACK_TYPE.PAIR_SWORDS_ORACLE_RUSH_BOOST)
		{
			return null;
		}
		return base.GetDamageRate(chara, status);
	}
}
