// Decompiled with JetBrains decompiler
// Type: AbilityAtkBurstSpearSpinMaxDamageUp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class AbilityAtkBurstSpearSpinMaxDamageUp : AbilityAtkWeapon
{
  public override void init(Player _player, string target, int val)
  {
    base.init(_player, "SPEAR", val);
  }

  public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
  {
    if (!this.player.spearCtrl.IsBurstSpin())
      return (AtkAttribute) null;
    float rate = 0.0f;
    if (!this.player.spearCtrl.GetSpinRate(ref rate))
      return (AtkAttribute) null;
    return (double) rate < 1.0 ? (AtkAttribute) null : base.GetDamageRate(chara, status);
  }
}
