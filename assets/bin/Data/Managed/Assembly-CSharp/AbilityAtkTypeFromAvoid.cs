// Decompiled with JetBrains decompiler
// Type: AbilityAtkTypeFromAvoid
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class AbilityAtkTypeFromAvoid : AbilityAtkWeapon
{
  public override AtkAttribute GetDamageRate(Character chara, AttackedHitStatusLocal status)
  {
    return status.attackInfo.attackType != AttackHitInfo.ATTACK_TYPE.FROM_AVOID ? (AtkAttribute) null : base.GetDamageRate(chara, status);
  }
}
