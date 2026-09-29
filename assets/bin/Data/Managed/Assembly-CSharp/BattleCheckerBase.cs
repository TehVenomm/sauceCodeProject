// Decompiled with JetBrains decompiler
// Type: BattleCheckerBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
[Serializable]
public abstract class BattleCheckerBase
{
  private const int ERROR_SPECIAL_ACTION_ID = -1;
  private int specialActionId = -1;
  private string[] specialAttackNames = new string[4]
  {
    "PLC00_attack_",
    "PLC01_attack_",
    "PLC02_attack_",
    "PLC04_attack_"
  };
  private readonly string BOW_CHARGE_ATTACK = "PLC05_attack_00";
  private readonly string TWO_HAND_SWORD_CHARGE_ATTACK_HEAT = "PLC01_attack_88";
  private readonly string ONE_HAND_SWORD_HEAT_COUNTER = "PLC00_attack_98";
  private readonly string ONE_HAND_SWORD_REVENGE_BURST = "PLC00_attack_92";
  private readonly string ONE_HAND_SWORD_BURST_COUNTER = "PLC00_attack_93";

  public bool isEnableAttackCount { set; get; }

  public void OnAttackHit(
    string attackName,
    BattleCheckerBase.JudgementParam judgementParam,
    int damage = 0)
  {
    bool flag = (double) judgementParam.chargeRate >= 0.99000000953674316;
    if (attackName == this.BOW_CHARGE_ATTACK & flag)
    {
      this.OnArrowChargeAttack(damage);
    }
    else
    {
      if (!this.isEnableAttackCount)
        return;
      if (attackName == this.TWO_HAND_SWORD_CHARGE_ATTACK_HEAT)
        this.OnHeatTwoHandSword(damage);
      else if (attackName == this.ONE_HAND_SWORD_HEAT_COUNTER)
        this.OnCounter(damage);
      else if (attackName == this.ONE_HAND_SWORD_REVENGE_BURST)
        this.OnRevengeBurst(damage);
      else if (attackName == this.ONE_HAND_SWORD_BURST_COUNTER)
      {
        this.OnBurstOneHandSword(damage);
      }
      else
      {
        if (this.specialActionId == -1)
        {
          if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
            return;
          InGameSettingsManager.Player player = MonoBehaviourSingleton<InGameSettingsManager>.I.player;
          this.specialActionId = player.specialActionInfo.spAttackID;
          int rushLoopAttackId = player.spearActionInfo.rushLoopAttackID;
          for (int index = 0; index < this.specialAttackNames.Length; ++index)
            this.specialAttackNames[index] = !(this.specialAttackNames[index] == this.specialAttackNames[2]) ? this.specialAttackNames[index] + this.specialActionId.ToString() : this.specialAttackNames[index] + rushLoopAttackId.ToString();
        }
        for (int index = 0; index < this.specialAttackNames.Length; ++index)
        {
          if (attackName.Contains(this.specialAttackNames[index]))
          {
            switch (index)
            {
              case 0:
                this.OnCounter(damage);
                return;
              case 1:
                if (flag)
                  this.OnTwoHandSwordChargeAttack(damage);
                if ((double) judgementParam.chargeExpandRate <= 0.0)
                  return;
                this.OnTwoHandSwordExChargeAttack(damage);
                return;
              case 2:
                this.OnSpearSpecialAttack(damage);
                if ((double) judgementParam.exRushChargeRate <= 0.0)
                  return;
                this.OnSpearExChargeAttack(damage);
                return;
              case 3:
                this.OnPairSwordsCombo(damage);
                return;
              default:
                return;
            }
          }
        }
      }
    }
  }

  protected abstract void OnCounter(int damage);

  protected abstract void OnSpearSpecialAttack(int damage);

  protected abstract void OnSpearExChargeAttack(int damage);

  protected abstract void OnPairSwordsCombo(int damage);

  protected abstract void OnTwoHandSwordChargeAttack(int damage);

  protected abstract void OnTwoHandSwordExChargeAttack(int damage);

  protected abstract void OnArrowChargeAttack(int damage);

  protected abstract void OnHeatTwoHandSword(int damage);

  protected abstract void OnRevengeBurst(int damage);

  protected abstract void OnBurstOneHandSword(int damage);

  public void OnWeakAttack(Enemy.WEAK_STATE weakState, int damage = 0)
  {
    this.OnNormalWeakHit(damage);
    if (!Enemy.IsWeakStateSpAttack(weakState))
      return;
    this.OnWeaponWeakHit(damage);
  }

  protected abstract void OnNormalWeakHit(int damage);

  protected abstract void OnWeaponWeakHit(int damage);

  private enum SPECIAL_ATTACK_WEAPON_TYPE
  {
    ONE_HAND_SWORD,
    TWO_HAND_SWORD,
    LANCE,
    PAIR_SWORDS,
    MAX_NUM,
  }

  public class JudgementParam
  {
    public float exRushChargeRate;
    public float chargeExpandRate;
    public float chargeRate;

    private JudgementParam(float chargeRate, float exRushChargeRate, float chargeExpandRate)
    {
      this.chargeRate = chargeRate;
      this.exRushChargeRate = exRushChargeRate;
      this.chargeExpandRate = chargeExpandRate;
    }

    public static BattleCheckerBase.JudgementParam Create(AttackInfo attackInfo, Self self)
    {
      return new BattleCheckerBase.JudgementParam(attackInfo.rateInfoRate, self.GetExRushChargeRate(), self.GetChargeExpandingRate());
    }
  }
}
