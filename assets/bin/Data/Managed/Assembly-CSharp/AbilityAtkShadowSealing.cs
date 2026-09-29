// Decompiled with JetBrains decompiler
// Type: AbilityAtkShadowSealing
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class AbilityAtkShadowSealing : AbilityAtkWeapon
{
  public override void init(Player _player, string target, int val)
  {
    base.init(_player, "ARROW", val);
  }

  public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
  {
    if (!this.player.CheckAttackModeAndSpType(Player.ATTACK_MODE.ARROW, SP_ATTACK_TYPE.HEAT))
      return (AtkAttribute) null;
    if (!(chara is Enemy enemy))
      return (AtkAttribute) null;
    return !enemy.IsDebuffShadowSealing() ? (AtkAttribute) null : base.GetDamageRate(chara, status);
  }
}
