public class AbilityAtkSp : AbilityAtkWeapon
{
	public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
	{
		if ((!player.isActSpecialAction && !player.isActOneHandSwordCounter && !player.isActTwoHandSwordHeatCombo && !player.isActPairSwordsSoulLaser) || player.actionID != Character.ACTION_ID.ATTACK)
		{
			return null;
		}
		AttackHitInfo.ToEnemy toEnemy = status.attackInfo.toEnemy;
		if (toEnemy == null || !toEnemy.isSpecialAttack)
		{
			return null;
		}
		if (player.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.HEAT))
		{
			return null;
		}
		return base.GetDamageRate(chara, status);
	}
}
