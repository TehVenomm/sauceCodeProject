public class AbilityAtkSp : AbilityAtkWeapon
{
	public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
	{
		if (player.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.BURST) && player.spearCtrl.IsSpecialActionHit())
		{
			return base.GetDamageRate(chara, status);
		}
		if (player.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.ORACLE) && status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.OHS_ORACLE_SP)
		{
			return base.GetDamageRate(chara, status);
		}
		if (player.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE) && (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.SPEAR_ORACLE_SP || status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.SPEAR_ORACLE_SP_CHARGED))
		{
			return base.GetDamageRate(chara, status);
		}
		if ((!player.isActSpecialAction && !player.isActOneHandSwordCounter && !player.isActTwoHandSwordHeatCombo && !player.isActPairSwordsSoulLaser && !player.snatchCtrl.IsFlickedAttack(player.attackID) && !player.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.BURST)) || player.actionID != Character.ACTION_ID.ATTACK)
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
