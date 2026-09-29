// Decompiled with JetBrains decompiler
// Type: AbilityAtkAttackId
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class AbilityAtkAttackId : AbilityAtkBase
{
  public int attackId = -1;

  public override void init(Player _player, string target, int val)
  {
    base.init(_player, target, val);
    int result;
    if (int.TryParse(target, out result))
      this.attackId = result;
    else
      this.attackId = -1;
  }

  public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
  {
    return this.player.attackID != this.attackId ? (AtkAttribute) null : base.GetDamageRate(chara, status);
  }
}
