// Decompiled with JetBrains decompiler
// Type: WeaponController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class WeaponController
{
  private Brain brain;
  private int _changeIndex = -1;

  public WeaponController(Brain brain) => this.brain = brain;

  public WeaponController.ATTACK_TYPE attackType { get; private set; }

  private void TypeOn(WeaponController.ATTACK_TYPE type) => this.attackType |= type;

  private void TypeOff(WeaponController.ATTACK_TYPE type) => this.attackType &= ~type;

  private bool TypeIsOn(WeaponController.ATTACK_TYPE type) => (this.attackType & type) == type;

  public void AttackOn() => this.TypeOn(WeaponController.ATTACK_TYPE.ATTACK);

  public void ComboOn() => this.TypeOn(WeaponController.ATTACK_TYPE.COMBO);

  public void SpecialOn() => this.TypeOn(WeaponController.ATTACK_TYPE.SPECIAL);

  public void GuardOn() => this.TypeOn(WeaponController.ATTACK_TYPE.GUARD);

  public void AimOn() => this.TypeOn(WeaponController.ATTACK_TYPE.AIM);

  public void AvoidAttackOn() => this.TypeOn(WeaponController.ATTACK_TYPE.AIM);

  public void AttackOff() => this.TypeOff(WeaponController.ATTACK_TYPE.ATTACK);

  public void ComboOff() => this.TypeOff(WeaponController.ATTACK_TYPE.COMBO);

  public void SpecialOff() => this.TypeOff(WeaponController.ATTACK_TYPE.SPECIAL);

  public void GuardOff() => this.TypeOff(WeaponController.ATTACK_TYPE.GUARD);

  public void AimOff() => this.TypeOff(WeaponController.ATTACK_TYPE.AIM);

  public void AvoidAttackOff() => this.TypeOff(WeaponController.ATTACK_TYPE.AIM);

  public bool IsAttack() => this.TypeIsOn(WeaponController.ATTACK_TYPE.ATTACK);

  public bool IsCombo() => this.TypeIsOn(WeaponController.ATTACK_TYPE.COMBO);

  public bool IsSpecial() => this.TypeIsOn(WeaponController.ATTACK_TYPE.SPECIAL);

  public bool IsGuard() => this.TypeIsOn(WeaponController.ATTACK_TYPE.GUARD);

  public bool IsAim() => this.TypeIsOn(WeaponController.ATTACK_TYPE.AIM);

  public bool IsAvoidAttack() => this.TypeIsOn(WeaponController.ATTACK_TYPE.AIM);

  public int beforeAttackId { get; private set; }

  public void SetBeforeAttackId(int id) => this.beforeAttackId = id;

  public float chargeRate { get; private set; }

  public void SetChargeRate(float rate) => this.chargeRate = rate;

  public bool isFullCharge => (double) this.chargeRate >= 1.0;

  public bool IsGuardAttack()
  {
    bool flag = false;
    if (this.brain.owner is Player)
      flag = (this.brain.owner as Player).CheckAttackMode(Player.ATTACK_MODE.ONE_HAND_SWORD);
    return flag;
  }

  public float GetAttackReach()
  {
    float attackReach = 3f;
    if (this.brain.owner is Player)
      attackReach = (this.brain.owner as Player).attackReach;
    return attackReach;
  }

  public float GetSpecialReach()
  {
    float specialReach = 0.0f;
    if (this.brain.owner is Player)
      specialReach = (this.brain.owner as Player).specialReach;
    return specialReach;
  }

  public float GetAvoidAttackReach()
  {
    float avoidAttackReach = 0.0f;
    if (this.brain.owner is Player)
      avoidAttackReach = (this.brain.owner as Player).avoidAttackReach;
    return avoidAttackReach;
  }

  public int changeIndex => this._changeIndex;

  public void SetChangeIndex(int index) => this._changeIndex = index;

  public void ResetChangeIndex() => this._changeIndex = -1;

  [Flags]
  public enum ATTACK_TYPE
  {
    NONE = 0,
    ATTACK = 1,
    COMBO = 2,
    SPECIAL = 4,
    GUARD = 8,
    AIM = 16, // 0x00000010
    AVOID_ATTACK = AIM, // 0x00000010
  }
}
