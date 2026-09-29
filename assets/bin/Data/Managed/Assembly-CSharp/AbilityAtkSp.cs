// Decompiled with JetBrains decompiler
// Type: AbilityAtkSp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class AbilityAtkSp : AbilityAtkWeapon
{
  public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
  {
    if (this.player.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.BURST) && this.player.spearCtrl.IsSpecialActionHit())
      return base.GetDamageRate(chara, status);
    if (this.player.CheckAttackModeAndSpType(Player.ATTACK_MODE.ONE_HAND_SWORD, SP_ATTACK_TYPE.ORACLE) && status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.OHS_ORACLE_SP)
      return base.GetDamageRate(chara, status);
    if (this.player.CheckAttackModeAndSpType(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.ORACLE) && (status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.SPEAR_ORACLE_SP || status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.SPEAR_ORACLE_SP_CHARGED))
      return base.GetDamageRate(chara, status);
    if (!this.player.isActSpecialAction && !this.player.isActOneHandSwordCounter && !this.player.isActTwoHandSwordHeatCombo && !this.player.isActPairSwordsSoulLaser && !this.player.snatchCtrl.IsFlickedAttack(this.player.attackID) && !this.player.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.BURST) || this.player.actionID != Character.ACTION_ID.ATTACK)
      return (AtkAttribute) null;
    AttackHitInfo.ToEnemy toEnemy = status.attackInfo.toEnemy;
    if (toEnemy == null || !toEnemy.isSpecialAttack)
      return (AtkAttribute) null;
    return this.player.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.HEAT) ? (AtkAttribute) null : base.GetDamageRate(chara, status);
  }
}
