// Decompiled with JetBrains decompiler
// Type: AbilityAtkWeapon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class AbilityAtkWeapon : AbilityAtkBase
{
  private Player.ATTACK_MODE mode;

  public override void init(Player _player, string target, int val)
  {
    base.init(_player, target, val);
    this.mode = (Player.ATTACK_MODE) Enum.Parse(typeof (Player.ATTACK_MODE), target);
  }

  public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
  {
    Player.ATTACK_MODE attackMode = this.player.attackMode;
    if (status.attackMode != Player.ATTACK_MODE.NONE)
      attackMode = status.attackMode;
    return this.mode == Player.ATTACK_MODE.NONE || this.mode == attackMode ? this.attr : (AtkAttribute) null;
  }
}
