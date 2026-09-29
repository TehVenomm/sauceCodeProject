// Decompiled with JetBrains decompiler
// Type: BuffParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

#nullable disable
public class BuffParam
{
  private const float REGENERATE_INTERVAL = 2f;
  public const float ENEMY_POISON_INTERVAL = 5f;
  public const float PLAYER_POISON_INTERVAL = 2f;
  public const float ENEMY_POISON_TIME = 20f;
  public const float PLAYER_POISON_BASE_VALUE = 0.02f;
  public const float ENEMY_POISON_BASE_VALUE = 0.015f;
  public const float PLAYER_DEADLY_POISON_BASE_VALUE = 0.1f;
  public const float PLAYER_BURNING_INTERVAL = 1f;
  public const float PLAYER_BURNING_BASE_VALUE = 0.03f;
  public const float ENEMY_ACID_INTERVAL = 5f;
  public const float PLAYER_ACID_INTERVAL = 2f;
  public const float PLAYER_ACID_BASE_VALUE = 0.02f;
  public const float ENEMY_ACID_BASE_VALUE = 0.015f;
  public const float DEBUFF_DEFAULT_ACID_TIME = 20f;
  public const int PLAYER_MOVE_SPEED_DOWN_VALUE = 50;
  private const float BURNING_AVOIDCOUNT = 3f;
  private const float AVOID_UP_RATE_MIN = 0.2f;
  private const float MOVE_SPEED_UP_RATE_MIN = 0.2f;
  private const float ABSORB_UP_RATE_MIN = 0.0f;
  private const float DEBUFF_DEFAULT_SLIDE_TIME = 20f;
  private const float DEBUFF_DEFAULT_SILENCE_TIME = 20f;
  private const float DEBUFF_DEFAULT_CANTHEALHP_TIME = 20f;
  private const float DEBUFF_DEFAULT_BLIND_TIME = 10f;
  private const float DEBUFF_DEFAULT_STONE_TIME = 30f;
  public const int PLAYER_DEFAULT_ATTACK_SPEED_DOWN_VALUE = 50;
  private const float DEBUFF_DEFAULT_ATTACK_SPEED_DOWN_TIME = 10f;
  private static readonly BuffParam.BUFFTYPE[] INVINCIBLE_BUFFTYPES = new BuffParam.BUFFTYPE[9]
  {
    BuffParam.BUFFTYPE.INVINCIBLE_NORMAL,
    BuffParam.BUFFTYPE.INVINCIBLE_FIRE,
    BuffParam.BUFFTYPE.INVINCIBLE_WATER,
    BuffParam.BUFFTYPE.INVINCIBLE_THUNDER,
    BuffParam.BUFFTYPE.INVINCIBLE_SOIL,
    BuffParam.BUFFTYPE.INVINCIBLE_LIGHT,
    BuffParam.BUFFTYPE.INVINCIBLE_DARK,
    BuffParam.BUFFTYPE.INVINCIBLE_ALL_ELEMENT,
    BuffParam.BUFFTYPE.INVINCIBLE_ALL
  };
  public static readonly BuffParam.BUFFTYPE[] INVINCIBLECOUNT_BUFFTYPES = new BuffParam.BUFFTYPE[4]
  {
    BuffParam.BUFFTYPE.ORACLE_OHS_PROTECTION,
    BuffParam.BUFFTYPE.SUBSTITUTE,
    BuffParam.BUFFTYPE.INVINCIBLECOUNT,
    BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS
  };
  public BuffParam.PassiveBuff passive = new BuffParam.PassiveBuff();
  protected BuffParam.BuffData[] data;
  protected Character chara;
  protected Player player;
  protected int[] counterAttackCountList = new int[7];
  protected Dictionary<BuffParam.BUFFTYPE, BuffParam.BuffData> fieldData = new Dictionary<BuffParam.BUFFTYPE, BuffParam.BuffData>(221);
  public EvolveController ownerEvolveCtrl;
  private Animator invincibleCountAnimator;
  private Animator invincibleBadStatusAnimator;
  private Animator invincibleBuffCancellationAnimator;
  private bool shouldSync;
  public List<BuffParam.EffectInfo> loopEffect = new List<BuffParam.EffectInfo>();
  private uint oldAbilityId;
  private List<int> executedStackOrder = new List<int>();

  public SubstituteController substituteCtrl { get; protected set; }

  public BuffParam(Character _chara)
  {
    this.data = new BuffParam.BuffData[221];
    for (int index = 0; index < 221; ++index)
    {
      this.data[index] = new BuffParam.BuffData();
      this.data[index].type = (BuffParam.BUFFTYPE) index;
    }
    this.fieldData.Clear();
    this.chara = _chara;
    this.player = _chara as Player;
    this.passive.Reset();
    this.substituteCtrl = new SubstituteController();
    this.substituteCtrl.Initialize(this.player);
  }

  public void Update()
  {
    bool flag1 = this.chara.IsCoopNone() || this.chara.IsOriginal();
    bool flag2 = this.chara.isProgressStop();
    bool flag3 = false;
    for (int type = 0; type < 221; ++type)
    {
      if (this.data[type].enable)
      {
        if (this.data[type].endless.HasValue && !this.data[type].endless.Value)
          this.data[type].time -= Time.deltaTime;
        if (flag1 && !flag2)
        {
          if ((double) this.data[type].interval > 0.0)
          {
            this.data[type].progress += Time.deltaTime;
            if ((double) this.data[type].interval < (double) this.data[type].progress)
            {
              this.chara.OnBuffRoutine(this.data[type]);
              this.data[type].progress -= this.data[type].interval;
            }
          }
          if (this.data[type].type == BuffParam.BUFFTYPE.DAMAGE_MOTION_STOP)
            this.chara.OnBuffRoutine(this.data[type]);
          bool flag4 = false;
          if (this.data[type].endless.HasValue && this.data[type].endless.Value)
          {
            switch (this.data[type].type)
            {
              case BuffParam.BUFFTYPE.INVINCIBLECOUNT:
              case BuffParam.BUFFTYPE.MAD_MODE:
              case BuffParam.BUFFTYPE.AUTO_REVIVE:
              case BuffParam.BUFFTYPE.INVINCIBLE_BADSTATUS:
              case BuffParam.BUFFTYPE.SUBSTITUTE:
              case BuffParam.BUFFTYPE.STONE:
              case BuffParam.BUFFTYPE.INVINCIBLE_BUFF_CANCELLATION:
              case BuffParam.BUFFTYPE.ORACLE_OHS_PROTECTION:
              case BuffParam.BUFFTYPE.AUTO_REVIVE_SKILL_CHARGE:
                break;
              case BuffParam.BUFFTYPE.SHIELD:
              case BuffParam.BUFFTYPE.SHIELD_SUPER_ARMOR:
              case BuffParam.BUFFTYPE.SHIELD_REFLECT:
              case BuffParam.BUFFTYPE.SHIELD_REFLECT_DAMAGE_UP:
              case BuffParam.BUFFTYPE.SHIELD_INVINCIBLE_BADSTATUS:
                if (Object.op_Inequality((Object) this.player, (Object) null) && (int) this.player.ShieldHp <= 0)
                {
                  flag4 = true;
                  break;
                }
                break;
              default:
                Log.Error("'{0}' is endless, but not defined end condition.", (object) this.data[type].type);
                break;
            }
          }
          else if ((double) this.data[type].time <= 0.0)
            flag4 = true;
          if (flag4 && this.chara.OnBuffEnd((BuffParam.BUFFTYPE) type, false))
            flag3 = true;
        }
      }
    }
    if (flag3)
      this.chara.SendBuffSync();
    this.substituteCtrl.Update();
  }

  public void OnAvoid()
  {
    BuffParam.BuffData buffData = this.data[27];
    if (buffData.value <= 0)
      return;
    ++buffData.avoidCount;
    if (!this.chara.IsCoopNone() && !this.chara.IsOriginal() || (double) buffData.avoidCount < 3.0)
      return;
    this.chara.OnBuffEnd(BuffParam.BUFFTYPE.BURNING, true);
  }

  public void OnBleeding() => this.chara.OnBuffRoutine(this.data[201]);

  public void ReduceInkSplashTime(float dt)
  {
    BuffParam.BuffData buffData = this.data[35];
    if (buffData.value <= 0)
      return;
    buffData.time -= dt;
  }

  public void ResetInterval(BuffParam.BUFFTYPE type)
  {
    this.data[(int) type].interval = 0.0f;
    this.data[(int) type].progress = 0.0f;
  }

  public void DecreaseInvincibleCount()
  {
    BuffParam.BUFFTYPE type = BuffParam.BUFFTYPE.NONE;
    for (int index = 0; index < BuffParam.INVINCIBLECOUNT_BUFFTYPES.Length; ++index)
    {
      BuffParam.BuffData buffData = this.data[(int) BuffParam.INVINCIBLECOUNT_BUFFTYPES[index]];
      if (buffData.value > 0)
      {
        type = buffData.type;
        break;
      }
    }
    if (type == BuffParam.BUFFTYPE.NONE)
      return;
    BuffParam.BuffData buffData1 = this.data[(int) type];
    if (!this.chara.IsCoopNone() && !this.chara.IsOriginal() || (double) buffData1.interval > 0.0)
      return;
    --buffData1.value;
    if (buffData1.value <= 0)
    {
      this.chara.OnBuffEnd(type, true);
      if (type == BuffParam.BUFFTYPE.INVINCIBLECOUNT)
        this.chara.isUseInvincibleBuff = true;
    }
    else
    {
      if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
        buffData1.interval = MonoBehaviourSingleton<InGameSettingsManager>.I.buff.invincibleInterval;
      if (type == BuffParam.BUFFTYPE.ORACLE_OHS_PROTECTION && Object.op_Inequality((Object) this.player, (Object) null))
        this.player.ohsCtrl.OnReloadEffect(type);
    }
    if (type != BuffParam.BUFFTYPE.INVINCIBLECOUNT)
    {
      if (type != BuffParam.BUFFTYPE.SUBSTITUTE)
        return;
      this.substituteCtrl.Sub();
    }
    else
    {
      if (!Object.op_Inequality((Object) this.invincibleCountAnimator, (Object) null))
        return;
      this.invincibleCountAnimator.SetTrigger("Hit");
    }
  }

  public void DecreaseInvincibleBadStatus()
  {
    BuffParam.BuffData buffData = this.data[182];
    if (buffData.value <= 0 || !this.chara.IsCoopNone() && !this.chara.IsOriginal() || (double) buffData.interval > 0.0)
      return;
    --buffData.value;
    if (buffData.value <= 0)
    {
      this.chara.OnBuffEnd(BuffParam.BUFFTYPE.INVINCIBLE_BADSTATUS, true);
      this.chara.isUseInvincibleBadStatusBuff = true;
    }
    else if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      buffData.interval = MonoBehaviourSingleton<InGameSettingsManager>.I.buff.invincibleInterval;
    if (!Object.op_Inequality((Object) this.invincibleBadStatusAnimator, (Object) null))
      return;
    this.invincibleBadStatusAnimator.SetTrigger("Hit");
  }

  public void DecreaseInvincibleBuffCancellation()
  {
    if (this.IsValidBuff(BuffParam.BUFFTYPE.INVINCIBLE_BUFF_CANCELLATION_EXPAND) || !this.chara.IsCoopNone() && !this.chara.IsOriginal())
      return;
    BuffParam.BuffData buffData = this.data[206];
    if (buffData.value <= 0)
      return;
    --buffData.value;
    if (buffData.value <= 0)
      this.chara.OnBuffEnd(BuffParam.BUFFTYPE.INVINCIBLE_BUFF_CANCELLATION, true);
    if (Object.op_Inequality((Object) this.invincibleBuffCancellationAnimator, (Object) null))
      this.invincibleBuffCancellationAnimator.SetTrigger("Hit");
    if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid() || (double) MonoBehaviourSingleton<InGameSettingsManager>.I.buff.invincibleBuffCancellationExpandTime <= 0.0)
      return;
    this.chara.OnBuffStart(new BuffParam.BuffData()
    {
      type = BuffParam.BUFFTYPE.INVINCIBLE_BUFF_CANCELLATION_EXPAND,
      time = MonoBehaviourSingleton<InGameSettingsManager>.I.buff.invincibleBuffCancellationExpandTime,
      value = 1
    });
  }

  public int GetValue(BuffParam.BUFFTYPE type, bool addFieldBuff = true)
  {
    if (type == BuffParam.BUFFTYPE.NONE || type >= BuffParam.BUFFTYPE.MAX)
      return 0;
    int num = this.data[(int) type].value;
    if (addFieldBuff)
      num += this.GetFieldBuffValue(type);
    if (this.ownerEvolveCtrl != null)
      num += this.ownerEvolveCtrl.GetExecBuffValue(type);
    return num;
  }

  public bool IsValidBuffOrFieldBuff(BuffParam.BUFFTYPE type, bool isFieldBuff)
  {
    return !isFieldBuff ? this.IsValidBuff(type) : this.IsValidFieldBuff(type);
  }

  public bool IsValidBuff(BuffParam.BUFFTYPE type) => this.GetValue(type, false) > 0;

  public bool IsValidFieldBuff(BuffParam.BUFFTYPE type) => this.GetFieldBuffValue(type) > 0;

  public bool IsEnableBuff(BuffParam.BUFFTYPE type)
  {
    return type != BuffParam.BUFFTYPE.NONE && type < BuffParam.BUFFTYPE.MAX && this.data[(int) type].enable;
  }

  public bool IsValidAutoReviveSkillChargeBuff()
  {
    return this.IsValidBuff(BuffParam.BUFFTYPE.AUTO_REVIVE_SKILL_CHARGE) && this.GetValue(BuffParam.BUFFTYPE.AUTO_REVIVE_SKILL_CHARGE) > 0;
  }

  public float GetMoveSpeed()
  {
    float moveSpeed = (float) (1.0 + (double) this.passive.moveSpeedUp + (double) (this.GetValue(BuffParam.BUFFTYPE.MOVE_SPEED_UP) - this.GetValue(BuffParam.BUFFTYPE.MOVE_SPEED_DOWN)) * 0.0099999997764825821);
    if (this.player != null && this.player.isBoostMode)
      moveSpeed += this.passive.boostMoveSpeedUp + (float) this.GetValue(BuffParam.BUFFTYPE.BOOST_MOVE_SPEED_UP) * 0.01f;
    if ((double) moveSpeed < 0.20000000298023224)
      moveSpeed = 0.2f;
    return moveSpeed;
  }

  public float GetAtkSpeed()
  {
    float atkSpeed = (float) (1.0 + (double) this.passive.attackSpeedUp + (double) (this.GetValue(BuffParam.BUFFTYPE.ATTACK_SPEED_UP) - this.GetValue(BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN)) * 0.0099999997764825821);
    if (this.player != null && this.player.isBoostMode)
      atkSpeed += this.passive.boostAttackSpeedUp + (float) this.GetValue(BuffParam.BUFFTYPE.BOOST_ATTACK_SPEED_UP) * 0.01f;
    return atkSpeed;
  }

  public float GetHealSpeedUp()
  {
    return (float) (1.0 + (double) this.passive.hpHealSpeedUp + (double) this.GetValue(BuffParam.BUFFTYPE.HP_HEAL_SPEEDUP) * 0.0099999997764825821);
  }

  public float GetGuardUp() => 1f - this.passive.guardUp;

  public float GetSkillAbsorbUp(ELEMENT_TYPE eType = ELEMENT_TYPE.MAX)
  {
    float skillAbsorbUp = (float) (1.0 + (double) this.passive.skillAbsorbUp + (double) this.GetValue(BuffParam.BUFFTYPE.SKILL_ABSORBUP) * 0.0099999997764825821);
    switch (eType)
    {
      case ELEMENT_TYPE.FIRE:
        skillAbsorbUp = skillAbsorbUp + this.passive.skillAbsorbUp_OnlyAttackAndElement.fire + (float) this.GetValue(BuffParam.BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_FIRE) * 0.01f;
        break;
      case ELEMENT_TYPE.WATER:
        skillAbsorbUp = skillAbsorbUp + this.passive.skillAbsorbUp_OnlyAttackAndElement.water + (float) this.GetValue(BuffParam.BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_WATER) * 0.01f;
        break;
      case ELEMENT_TYPE.THUNDER:
        skillAbsorbUp = skillAbsorbUp + this.passive.skillAbsorbUp_OnlyAttackAndElement.thunder + (float) this.GetValue(BuffParam.BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_THUNDER) * 0.01f;
        break;
      case ELEMENT_TYPE.SOIL:
        skillAbsorbUp = skillAbsorbUp + this.passive.skillAbsorbUp_OnlyAttackAndElement.soil + (float) this.GetValue(BuffParam.BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_SOIL) * 0.01f;
        break;
      case ELEMENT_TYPE.LIGHT:
        skillAbsorbUp = skillAbsorbUp + this.passive.skillAbsorbUp_OnlyAttackAndElement.light + (float) this.GetValue(BuffParam.BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_LIGHT) * 0.01f;
        break;
      case ELEMENT_TYPE.DARK:
        skillAbsorbUp = skillAbsorbUp + this.passive.skillAbsorbUp_OnlyAttackAndElement.dark + (float) this.GetValue(BuffParam.BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_DARK) * 0.01f;
        break;
    }
    if ((double) skillAbsorbUp < 0.0)
      skillAbsorbUp = 0.0f;
    return skillAbsorbUp;
  }

  public float GetSkillAbsorbUpByElementList(ELEMENT_TYPE[] eTypes)
  {
    float absorbUpByElementList = (float) (1.0 + (double) this.passive.skillAbsorbUp + (double) this.GetValue(BuffParam.BUFFTYPE.SKILL_ABSORBUP) * 0.0099999997764825821);
    if (eTypes != null && eTypes.Length != 0)
    {
      for (int index = 0; index < eTypes.Length; ++index)
      {
        switch (eTypes[index])
        {
          case ELEMENT_TYPE.FIRE:
            absorbUpByElementList = absorbUpByElementList + this.passive.skillAbsorbUp_OnlyAttackAndElement.fire + (float) this.GetValue(BuffParam.BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_FIRE) * 0.01f;
            break;
          case ELEMENT_TYPE.WATER:
            absorbUpByElementList = absorbUpByElementList + this.passive.skillAbsorbUp_OnlyAttackAndElement.water + (float) this.GetValue(BuffParam.BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_WATER) * 0.01f;
            break;
          case ELEMENT_TYPE.THUNDER:
            absorbUpByElementList = absorbUpByElementList + this.passive.skillAbsorbUp_OnlyAttackAndElement.thunder + (float) this.GetValue(BuffParam.BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_THUNDER) * 0.01f;
            break;
          case ELEMENT_TYPE.SOIL:
            absorbUpByElementList = absorbUpByElementList + this.passive.skillAbsorbUp_OnlyAttackAndElement.soil + (float) this.GetValue(BuffParam.BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_SOIL) * 0.01f;
            break;
          case ELEMENT_TYPE.LIGHT:
            absorbUpByElementList = absorbUpByElementList + this.passive.skillAbsorbUp_OnlyAttackAndElement.light + (float) this.GetValue(BuffParam.BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_LIGHT) * 0.01f;
            break;
          case ELEMENT_TYPE.DARK:
            absorbUpByElementList = absorbUpByElementList + this.passive.skillAbsorbUp_OnlyAttackAndElement.dark + (float) this.GetValue(BuffParam.BUFFTYPE.SKILL_ABSORBUP_ONLY_ATK_DARK) * 0.01f;
            break;
        }
      }
    }
    if ((double) absorbUpByElementList < 0.0)
      absorbUpByElementList = 0.0f;
    return absorbUpByElementList;
  }

  public float GetSkillHealSpeedUp()
  {
    return (float) (1.0 + (double) this.passive.skillHealSpeedUp + (double) this.GetValue(BuffParam.BUFFTYPE.SKILL_HEAL_SPEEDUP) * 0.0099999997764825821);
  }

  public float GetAvoidUp()
  {
    float avoidUp = 1f + this.passive.avoidUp;
    if (this.GetValue(BuffParam.BUFFTYPE.MOVE_SPEED_DOWN) > 0)
      avoidUp -= (float) this.GetValue(BuffParam.BUFFTYPE.MOVE_SPEED_DOWN) * 0.01f;
    if (this.player != null && this.player.isBoostMode)
      avoidUp += this.passive.boostAvoidUp + (float) this.GetValue(BuffParam.BUFFTYPE.BOOST_AVOID_UP) * 0.01f;
    if ((double) avoidUp < 0.20000000298023224)
      avoidUp = 0.2f;
    return avoidUp;
  }

  public float GetTeleportUp()
  {
    float teleportUp = 1f + this.passive.teleportUp;
    if (this.GetValue(BuffParam.BUFFTYPE.MOVE_SPEED_DOWN) > 0)
      teleportUp -= (float) this.GetValue(BuffParam.BUFFTYPE.MOVE_SPEED_DOWN) * 0.01f;
    if ((double) teleportUp < 0.20000000298023224)
      teleportUp = 0.2f;
    return teleportUp;
  }

  public float GetHealUp()
  {
    float healUp = 1f + this.passive.healUP;
    if ((double) healUp < 0.0)
      healUp = 0.0f;
    return healUp;
  }

  public float GetHealHpRate() => this.passive.healUP;

  public float GetHealUpDependsWeaponRate() => this.passive.healUpDependsWeapon;

  public float GetParalyzeTime()
  {
    return (float) (7.0 * (double) this.passive.tolerance[0] * 0.0099999997764825821);
  }

  public float GetPoisonTime()
  {
    return (float) (20.0 * (double) this.passive.tolerance[1] * 0.0099999997764825821);
  }

  public float GetBurningTime()
  {
    return (float) (5.0 * (double) this.passive.tolerance[2] * 0.0099999997764825821);
  }

  public float GetBleedTime()
  {
    return (float) ((double) MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.bleedingParam.duration * (double) this.passive.tolerance[15] * 0.0099999997764825821);
  }

  public float GetSpeedDownTime()
  {
    return (float) (10.0 * (double) this.passive.tolerance[3] * 0.0099999997764825821);
  }

  public float GetAttackSpeedDownTime()
  {
    float num = 10f;
    if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      num = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.attackSpeedDownParam.duration;
    return (float) ((double) num * (double) this.passive.tolerance[11] * 0.0099999997764825821);
  }

  public float GetDeadlyPoisonTime()
  {
    float num = 20f;
    if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      num = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.deadlyPosion.duration;
    return (float) ((double) num * (double) this.passive.tolerance[1] * 0.0099999997764825821);
  }

  public float GetSlideTime()
  {
    float num = 20f;
    if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      num = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.slideParam.duration;
    return (float) ((double) num * (double) this.passive.tolerance[9] * 0.0099999997764825821);
  }

  public float GetSilenceTime()
  {
    float num = 20f;
    if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      num = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.silenceParam.duration;
    return (float) ((double) num * (double) this.passive.tolerance[10] * 0.0099999997764825821);
  }

  public float GetCantHealHpTime()
  {
    float num = 20f;
    if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      num = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.cantHealHpParam.duration;
    return (float) ((double) num * (double) this.passive.tolerance[12] * 0.0099999997764825821);
  }

  public float GetBlindTime()
  {
    float num = 10f;
    if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      num = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.blindParam.duration;
    return (float) ((double) num * (double) this.passive.tolerance[13] * 0.0099999997764825821);
  }

  public float GetStoneTime()
  {
    float num = 30f;
    if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      num = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.stoneParam.duration;
    return (float) ((double) num * (double) this.passive.tolerance[14] * 0.0099999997764825821);
  }

  public float GetAcidTime()
  {
    float num = 20f;
    if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      num = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.acidParam.duration;
    return (float) ((double) num * (double) this.passive.tolerance[16 /*0x10*/] * 0.0099999997764825821);
  }

  public float GetCorruptionTime()
  {
    float num = 20f;
    if (MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      num = MonoBehaviourSingleton<InGameSettingsManager>.I.debuff.corruptionParam.duration;
    return (float) ((double) num * (double) this.passive.tolerance[18] * 0.0099999997764825821);
  }

  public float GetBleedUp()
  {
    float bleedUp = 1f + this.passive.bleedUp;
    if ((double) bleedUp < 0.0)
      bleedUp = 0.0f;
    return bleedUp;
  }

  public float GetChargeSwordsTimeRate() => this.passive.chargeSwordsTimeRate;

  public float GetChargeArrowTimeRate() => this.passive.chargeArrowTimeRate;

  public float GetChargeHeatArrowTimeRate() => this.passive.chargeHeatArrowTimeRate;

  public float GetChargePairSwordsTimeRate() => this.passive.chargePairSwordsTimeRate;

  public float GetSpearRushDistanceRate() => this.passive.spearRushDistanceRate;

  public float GetSpearRushSpeedRate() => this.passive.spearRushSpeedRate;

  public float GetDragonArmorDamageRate() => this.passive.dragonArmorDamageRate;

  public float GetChargeSpearTimeRate() => this.passive.chargeSpearTimeRate;

  public float GetSkillTimeRate() => this.passive.skillTimeRate;

  public float GetArrowRainNumRate() => 1f + this.passive.arrowRainNumRate;

  public float GetBurstSpearSpinTimeRate() => this.passive.burstSpearSpinTimeRate;

  public float GetOracleThsHorizontalSpeedRate() => 1f + this.passive.oracleThsHorizontalSpeedUp;

  public float GetOracleSpinSmashChargeTimeRate() => this.passive.oracleThsSpinSmashTimeRate;

  public float GetOracleDiveSmashChargeTimeRate() => this.passive.oracleThsDiveSmashTimeRate;

  public float GetOracleWheelSmashChargeTimeRate() => this.passive.oracleThsWheelSmashTimeRate;

  public int GetOracleOhsProtectionDoubleProbability()
  {
    return this.passive.oracleOhsProtectionDoubleProbability;
  }

  public float GetStumbleTime(float time)
  {
    return (float) ((double) time * (double) this.passive.tolerance[4] * 0.0099999997764825821);
  }

  public float GetShakeTime(float time)
  {
    return (float) ((double) time * (double) this.passive.tolerance[5] * 0.0099999997764825821);
  }

  public float GetCharmTime(float time)
  {
    return (float) ((double) time * (double) this.passive.tolerance[17] * 0.0099999997764825821);
  }

  public bool IsHalfReaction(BuffParam.TOLERANCETYPE type)
  {
    return this.passive.tolerance[(int) type] < 100 && !this.IsInvalidReaction(type);
  }

  public bool IsInvalidReaction(BuffParam.TOLERANCETYPE type)
  {
    return this.passive.tolerance[(int) type] <= 0;
  }

  public float GetDamageDownRate()
  {
    float damageDownRate = (float) (1.0 - ((double) this.passive.damageDown + (double) this.GetValue(BuffParam.BUFFTYPE.DAMAGE_DOWN) * 0.0099999997764825821));
    if (this.player != null && this.player.isBoostMode)
      damageDownRate -= this.passive.boostDamageDown + (float) this.GetValue(BuffParam.BUFFTYPE.BOOST_DAMAGE_DOWN) * 0.01f;
    float num = (float) (1.0 - (MonoBehaviourSingleton<InGameSettingsManager>.IsValid() ? (double) MonoBehaviourSingleton<InGameSettingsManager>.I.player.maxDamageDownRate : 0.60000002384185791));
    if ((double) damageDownRate < (double) num)
      damageDownRate = num;
    return damageDownRate;
  }

  public float GetGaugeIncreaseRate(SP_ATTACK_TYPE type)
  {
    float gaugeIncreaseRate = this.passive.gaugeIncreaseRate;
    switch (type)
    {
      case SP_ATTACK_TYPE.HEAT:
        gaugeIncreaseRate += this.passive.heatGaugeIncreaseRate;
        break;
      case SP_ATTACK_TYPE.SOUL:
        gaugeIncreaseRate += this.passive.soulGaugeIncreaseRate;
        break;
      case SP_ATTACK_TYPE.BURST:
        gaugeIncreaseRate += this.passive.burstGaugeIncreaseRate;
        break;
      case SP_ATTACK_TYPE.ORACLE:
        gaugeIncreaseRate += this.passive.oracleGaugeIncreaseRate;
        break;
    }
    if ((double) gaugeIncreaseRate < -1.0)
      gaugeIncreaseRate = -1f;
    return gaugeIncreaseRate;
  }

  public float GetGaugeDecreaseRate()
  {
    float gaugeDecreaseRate = this.passive.gaugeDecreaseRate;
    if ((double) gaugeDecreaseRate < -1.0)
      gaugeDecreaseRate = -1f;
    return gaugeDecreaseRate;
  }

  public float GetSubGaugeIncreaseRate()
  {
    float gaugeIncreaseRate = this.passive.subGaugeIncreaseRate;
    if ((double) gaugeIncreaseRate < -1.0)
      gaugeIncreaseRate = -1f;
    return gaugeIncreaseRate;
  }

  public float GetHeatGaugeIncreaseRate() => this.passive.heatGaugeIncreaseRate;

  public float GetSoulGaugeIncreaseRate() => this.passive.soulGaugeIncreaseRate;

  public float GetSoulChargeTimeRate() => this.passive.soulChargeTimeRate;

  public float GetShadowSealingExtend() => 1f + this.passive.shadowSealingExtend;

  public float GetShadowSealingExtendArrow() => 1f + this.passive.shadowSealingExtendArrow;

  public float GetConcussionExtend() => 1f + this.passive.concussionExtend;

  public float GetDistanceRateFromAvoid() => 1f + this.passive.distanceRateFromAvoid;

  public float GetDistanceRateIai()
  {
    return (float) (1.0 + (double) this.passive.distanceRateIai + (double) this.GetValue(BuffParam.BUFFTYPE.DISTANCE_UP_IAI) * 0.0099999997764825821);
  }

  public float GetLockOnTimeRate() => 1f + this.passive.lockOnTimeRate;

  public bool IsNarrowEscape() => this.passive.narrowEscape > 0 && this.passive.enableNarrowEscape;

  public void UseNarrowEscape() => --this.passive.narrowEscape;

  public float GetDamageUpRate(Player player, AttackedHitStatusLocal status)
  {
    int num1 = this.GetValue(BuffParam.BUFFTYPE.DAMAGE_UP);
    int num2 = this.GetValue(BuffParam.BUFFTYPE.BOOST_DAMAGE_UP);
    if (num2 > 0 && Object.op_Inequality((Object) player, (Object) null) && player.isBoostMode)
      num1 += num2;
    if (status.attackInfo.isSkillReference)
      return (float) num1 * 0.01f;
    int num3 = this.GetValue(BuffParam.BUFFTYPE.DAMAGE_UP_NORMAL);
    if (num3 > 0 && InGameUtility.IsDamageUpAtkTypeNormal(player, status.attackInfo))
      num1 += num3;
    int num4 = this.GetValue(BuffParam.BUFFTYPE.DAMAGE_UP_FROM_AVOID);
    if (num4 > 0 && status.attackInfo.attackType == AttackHitInfo.ATTACK_TYPE.FROM_AVOID)
      num1 += num4;
    return (float) num1 * 0.01f;
  }

  public AtkAttribute GetAbilityDamageRate(Character chara, AttackedHitStatusLocal status)
  {
    AtkAttribute attr = new AtkAttribute();
    attr.Set(1f);
    this.passive.abilityAtkList.ForEach((Action<AbilityAtkBase>) (data =>
    {
      AtkAttribute damageRate = data.GetDamageRate(chara, status);
      if (damageRate == null)
        return;
      attr.Add(damageRate);
    }));
    return attr;
  }

  public AtkAttribute GetBuffAtkRate()
  {
    AtkAttribute buffAtkRate = new AtkAttribute();
    buffAtkRate.normal = (float) this.GetValue(BuffParam.BUFFTYPE.ATKUP_RATE_NORMAL) * 0.01f;
    buffAtkRate.fire = (float) this.GetValue(BuffParam.BUFFTYPE.ATKUP_RATE_FIRE) * 0.01f;
    buffAtkRate.water = (float) this.GetValue(BuffParam.BUFFTYPE.ATKUP_RATE_WATER) * 0.01f;
    buffAtkRate.thunder = (float) this.GetValue(BuffParam.BUFFTYPE.ATKUP_RATE_THUNDER) * 0.01f;
    buffAtkRate.soil = (float) this.GetValue(BuffParam.BUFFTYPE.ATKUP_RATE_SOIL) * 0.01f;
    buffAtkRate.light = (float) this.GetValue(BuffParam.BUFFTYPE.ATKUP_RATE_LIGHT) * 0.01f;
    buffAtkRate.dark = (float) this.GetValue(BuffParam.BUFFTYPE.ATKUP_RATE_DARK) * 0.01f;
    buffAtkRate.AddElementOnly((float) this.GetValue(BuffParam.BUFFTYPE.ATKUP_RATE_ALLELEMENT) * 0.01f);
    buffAtkRate.AddAll((float) this.GetValue(BuffParam.BUFFTYPE.ATKUP_RATE_ALL) * 0.01f);
    return buffAtkRate;
  }

  public AtkAttribute GetBuffDefenceRate()
  {
    AtkAttribute buffDefenceRate = new AtkAttribute();
    buffDefenceRate.Set(0.0f);
    buffDefenceRate.normal = (float) this.GetValue(BuffParam.BUFFTYPE.DEFUP_RATE_NORMAL) * 0.01f;
    buffDefenceRate.fire = (float) this.GetValue(BuffParam.BUFFTYPE.DEFUP_RATE_FIRE) * 0.01f;
    buffDefenceRate.water = (float) this.GetValue(BuffParam.BUFFTYPE.DEFUP_RATE_WATER) * 0.01f;
    buffDefenceRate.thunder = (float) this.GetValue(BuffParam.BUFFTYPE.DEFUP_RATE_THUNDER) * 0.01f;
    buffDefenceRate.soil = (float) this.GetValue(BuffParam.BUFFTYPE.DEFUP_RATE_SOIL) * 0.01f;
    buffDefenceRate.light = (float) this.GetValue(BuffParam.BUFFTYPE.DEFUP_RATE_LIGHT) * 0.01f;
    buffDefenceRate.dark = (float) this.GetValue(BuffParam.BUFFTYPE.DEFUP_RATE_DARK) * 0.01f;
    buffDefenceRate.AddElementOnly((float) this.GetValue(BuffParam.BUFFTYPE.DEFUP_RATE_ALLELEMENT) * 0.01f);
    return buffDefenceRate;
  }

  public AtkAttribute GetBuffToleranceRate()
  {
    AtkAttribute buffToleranceRate = new AtkAttribute();
    buffToleranceRate.Set(0.0f);
    buffToleranceRate.fire = (float) this.GetValue(BuffParam.BUFFTYPE.TOLUP_RATE_FIRE) * 0.01f;
    buffToleranceRate.water = (float) this.GetValue(BuffParam.BUFFTYPE.TOLUP_RATE_WATER) * 0.01f;
    buffToleranceRate.thunder = (float) this.GetValue(BuffParam.BUFFTYPE.TOLUP_RATE_THUNDER) * 0.01f;
    buffToleranceRate.soil = (float) this.GetValue(BuffParam.BUFFTYPE.TOLUP_RATE_SOIL) * 0.01f;
    buffToleranceRate.light = (float) this.GetValue(BuffParam.BUFFTYPE.TOLUP_RATE_LIGHT) * 0.01f;
    buffToleranceRate.dark = (float) this.GetValue(BuffParam.BUFFTYPE.TOLUP_RATE_DARK) * 0.01f;
    buffToleranceRate.AddElementOnly((float) this.GetValue(BuffParam.BUFFTYPE.TOLUP_RATE_ALLELEMENT) * 0.01f);
    return buffToleranceRate;
  }

  public AtkAttribute GetBuffAtkConstant()
  {
    AtkAttribute buffAtkConstant = new AtkAttribute();
    buffAtkConstant.Set(0.0f);
    buffAtkConstant.normal = (float) this.GetValue(BuffParam.BUFFTYPE.ATTACK_NORMAL);
    buffAtkConstant.fire = (float) this.GetValue(BuffParam.BUFFTYPE.ATTACK_FIRE);
    buffAtkConstant.water = (float) this.GetValue(BuffParam.BUFFTYPE.ATTACK_WATER);
    buffAtkConstant.thunder = (float) this.GetValue(BuffParam.BUFFTYPE.ATTACK_THUNDER);
    buffAtkConstant.soil = (float) this.GetValue(BuffParam.BUFFTYPE.ATTACK_SOIL);
    buffAtkConstant.light = (float) this.GetValue(BuffParam.BUFFTYPE.ATTACK_LIGHT);
    buffAtkConstant.dark = (float) this.GetValue(BuffParam.BUFFTYPE.ATTACK_DARK);
    return buffAtkConstant;
  }

  public float GetPassiveHpRate() => this.passive.hpUpRate - this.passive.hpDownRate;

  public AtkAttribute GetPassiveAtkRate()
  {
    AtkAttribute passiveAtkRate = new AtkAttribute();
    passiveAtkRate.Set(0.0f);
    passiveAtkRate.Add(this.passive.atkUpRate);
    passiveAtkRate.Sub(this.passive.atkDownRate);
    return passiveAtkRate;
  }

  public AtkAttribute GetPassiveAtkUpConstant()
  {
    AtkAttribute passiveAtkUpConstant = new AtkAttribute();
    passiveAtkUpConstant.Set(0.0f);
    passiveAtkUpConstant.normal = (float) this.passive.atkList[0];
    passiveAtkUpConstant.fire = (float) this.passive.atkList[1];
    passiveAtkUpConstant.water = (float) this.passive.atkList[2];
    passiveAtkUpConstant.thunder = (float) this.passive.atkList[3];
    passiveAtkUpConstant.soil = (float) this.passive.atkList[4];
    passiveAtkUpConstant.light = (float) this.passive.atkList[5];
    passiveAtkUpConstant.dark = (float) this.passive.atkList[6];
    return passiveAtkUpConstant;
  }

  public AtkAttribute GetEquipAtkUpConstant(List<int> baseStateAtkList)
  {
    AtkAttribute equipAtkUpConstant = new AtkAttribute();
    equipAtkUpConstant.Set(0.0f);
    equipAtkUpConstant.normal = (float) baseStateAtkList[0];
    equipAtkUpConstant.fire = (float) baseStateAtkList[1];
    equipAtkUpConstant.water = (float) baseStateAtkList[2];
    equipAtkUpConstant.thunder = (float) baseStateAtkList[3];
    equipAtkUpConstant.soil = (float) baseStateAtkList[4];
    equipAtkUpConstant.light = (float) baseStateAtkList[5];
    equipAtkUpConstant.dark = (float) baseStateAtkList[6];
    return equipAtkUpConstant;
  }

  public float GetBadStatusUp(BuffParam.BAD_STATUS_UP type)
  {
    return type == BuffParam.BAD_STATUS_UP.MAX ? 0.0f : this.passive.badStatusUp[(int) type];
  }

  public float GetBadStatusRateUp(BuffParam.BAD_STATUS_UP type)
  {
    return type == BuffParam.BAD_STATUS_UP.MAX ? 1f : 1f + this.passive.badStatusRateUp[(int) type];
  }

  public float GetPoisonDamageDownRate()
  {
    return this.passive.poisonDamageDownRate + (float) this.GetValue(BuffParam.BUFFTYPE.POISON_DAMAGE_DOWN) * 0.01f;
  }

  public float GetBurnDamageDownRate()
  {
    return this.passive.burnDamageDownRate + (float) this.GetValue(BuffParam.BUFFTYPE.BURN_DAMAGE_DOWN) * 0.01f;
  }

  private int GetPoisonGuardWeight()
  {
    return this.GetValue(BuffParam.BUFFTYPE.POISON_GUARD) + this.passive.poisonGuardWeight;
  }

  private int GetBurnGuardWeight()
  {
    return this.GetValue(BuffParam.BUFFTYPE.BURN_GUARD) + this.passive.burnGuardWeight;
  }

  private int GetParalyzeGuardWeight()
  {
    return this.GetValue(BuffParam.BUFFTYPE.PARALYZE_GUARD) + this.passive.paralyzeGuardWeight;
  }

  private int GetSilenceGuardWeight()
  {
    return this.GetValue(BuffParam.BUFFTYPE.SILENCE_GUARD) + this.passive.silenceGuardWeight;
  }

  private bool LotteryBadStatusGuard(BuffParam.BUFFTYPE targetType)
  {
    int num = 0;
    switch (targetType)
    {
      case BuffParam.BUFFTYPE.ATTACK_PARALYZE:
        num = this.GetParalyzeGuardWeight();
        break;
      case BuffParam.BUFFTYPE.POISON:
        num = this.GetPoisonGuardWeight();
        break;
      case BuffParam.BUFFTYPE.BURNING:
        num = this.GetBurnGuardWeight();
        break;
      case BuffParam.BUFFTYPE.DEADLY_POISON:
        num = this.GetPoisonGuardWeight();
        break;
      case BuffParam.BUFFTYPE.SILENCE:
        num = this.GetSilenceGuardWeight();
        break;
    }
    return num > 0 && Random.Range(0, 100) <= num;
  }

  public void ApplyBadStatusGuard(ref BadStatus targetBadStatus)
  {
    if ((double) targetBadStatus.paralyze > 0.0 && (this.LotteryBadStatusGuard(BuffParam.BUFFTYPE.ATTACK_PARALYZE) || (double) this.GetParalyzeTime() <= 0.0))
      targetBadStatus.paralyze = 0.0f;
    if ((double) targetBadStatus.poison > 0.0 && (this.LotteryBadStatusGuard(BuffParam.BUFFTYPE.POISON) || (double) this.GetPoisonTime() <= 0.0))
      targetBadStatus.poison = 0.0f;
    if ((double) targetBadStatus.deadlyPoison > 0.0 && (this.LotteryBadStatusGuard(BuffParam.BUFFTYPE.DEADLY_POISON) || (double) this.GetDeadlyPoisonTime() <= 0.0))
      targetBadStatus.deadlyPoison = 0.0f;
    if ((double) targetBadStatus.burning > 0.0 && (this.LotteryBadStatusGuard(BuffParam.BUFFTYPE.BURNING) || (double) this.GetBurningTime() <= 0.0))
      targetBadStatus.burning = 0.0f;
    if ((double) targetBadStatus.silence > 0.0 && (this.LotteryBadStatusGuard(BuffParam.BUFFTYPE.SILENCE) || (double) this.GetSilenceTime() <= 0.0))
      targetBadStatus.silence = 0.0f;
    if ((double) targetBadStatus.cantHealHp > 0.0 && (this.LotteryBadStatusGuard(BuffParam.BUFFTYPE.CANT_HEAL_HP) || (double) this.GetCantHealHpTime() <= 0.0))
      targetBadStatus.cantHealHp = 0.0f;
    if ((double) targetBadStatus.blind > 0.0 && (this.LotteryBadStatusGuard(BuffParam.BUFFTYPE.BLIND) || (double) this.GetBlindTime() <= 0.0))
      targetBadStatus.blind = 0.0f;
    if ((double) targetBadStatus.speedDown > 0.0 && (double) this.GetSpeedDownTime() <= 0.0)
      targetBadStatus.speedDown = 0.0f;
    if ((double) targetBadStatus.attackSpeedDown > 0.0 && (double) this.GetAttackSpeedDownTime() <= 0.0)
      targetBadStatus.attackSpeedDown = 0.0f;
    if ((double) targetBadStatus.slide <= 0.0 || (double) this.GetSlideTime() > 0.0)
      return;
    targetBadStatus.slide = 0.0f;
  }

  public float GetJustGuardExtendRate() => 1f + this.passive.justGuardExtendRate;

  public List<BuffParam.BuffData> GetAbsorbBuffDataList()
  {
    List<BuffParam.BuffData> absorbBuffDataList = new List<BuffParam.BuffData>();
    for (int index = 144 /*0x90*/; index <= 151; ++index)
    {
      if (this.data[index].IsAbsorbType() && this.IsValidBuff(this.data[index].type))
        absorbBuffDataList.Add(this.data[index]);
    }
    return absorbBuffDataList;
  }

  public List<BuffParam.BuffData> GetInvincibleBuffDataList()
  {
    List<BuffParam.BuffData> invincibleBuffDataList = new List<BuffParam.BuffData>();
    int index = 0;
    for (int length = BuffParam.INVINCIBLE_BUFFTYPES.Length; index < length; ++index)
    {
      BuffParam.BuffData buffData = this.data[(int) BuffParam.INVINCIBLE_BUFFTYPES[index]];
      if (buffData.IsInvinsibleType() && this.IsValidBuff(buffData.type))
        invincibleBuffDataList.Add(buffData);
    }
    return invincibleBuffDataList;
  }

  public List<BuffParam.BuffData> GetHitAbsorbBuffDataList()
  {
    List<BuffParam.BuffData> absorbBuffDataList = new List<BuffParam.BuffData>();
    for (int index = 152; index <= 159; ++index)
    {
      if (BuffParam.IsHitAbsorbType(this.data[index].type) && this.IsValidBuff(this.data[index].type))
        absorbBuffDataList.Add(this.data[index]);
    }
    return absorbBuffDataList;
  }

  public void SetValue(BuffParam.BUFFTYPE type, int value)
  {
    if (type == BuffParam.BUFFTYPE.NONE || type >= BuffParam.BUFFTYPE.MAX)
      return;
    this.data[(int) type].value = value;
  }

  private bool IsCheckCurrentBuffValue(BuffParam.BUFFTYPE targetType)
  {
    return targetType != BuffParam.BUFFTYPE.BREAK_GHOST_FORM && targetType != BuffParam.BUFFTYPE.SUPER_ARMOR && targetType != BuffParam.BUFFTYPE.SHIELD_SUPER_ARMOR && targetType != BuffParam.BUFFTYPE.SHIELD_INVINCIBLE_BADSTATUS;
  }

  public bool BuffStart(BuffParam.BuffData buffData)
  {
    BuffParam.BUFFTYPE type = buffData.type;
    if (type == BuffParam.BUFFTYPE.NONE || type >= BuffParam.BUFFTYPE.MAX || (type == BuffParam.BUFFTYPE.SHIELD || type == BuffParam.BUFFTYPE.SHIELD_SUPER_ARMOR || type == BuffParam.BUFFTYPE.SHIELD_REFLECT || type == BuffParam.BUFFTYPE.SHIELD_REFLECT_DAMAGE_UP || type == BuffParam.BUFFTYPE.SHIELD_INVINCIBLE_BADSTATUS) && this.player.isDead || (type == BuffParam.BUFFTYPE.AUTO_REVIVE || type == BuffParam.BUFFTYPE.AUTO_REVIVE_SKILL_CHARGE) && (this.player == null || !this.player.IsAbleToAutoReviveBuff()))
      return false;
    if (type == BuffParam.BUFFTYPE.INVINCIBLECOUNT)
    {
      if (this.player == null || !this.player.IsAbleToInvincibleBuff())
        return false;
      this.ResetInterval(type);
    }
    if (type == BuffParam.BUFFTYPE.INVINCIBLE_BADSTATUS)
    {
      if (this.player == null || !this.player.IsAbleToInvincibleBadStatusBuff())
        return false;
      this.ResetInterval(type);
    }
    if (this.IsCheckCurrentBuffValue(type) && this.GetValue(type, false) > buffData.value)
      return false;
    BuffParam.BuffData data = this.data[(int) type];
    if (data.enable)
    {
      if ((data.type == BuffParam.BUFFTYPE.SHIELD || data.type == BuffParam.BUFFTYPE.SHIELD_SUPER_ARMOR) && this.player.IsValidShield())
      {
        if (buffData.isPlayLoopEffect)
          this.PlayBuffLoopEffect(data);
        this.shouldSync = true;
        return true;
      }
      if (buffData.isCallBuffEnd)
        this.chara.OnBuffEnd(type, false, false);
    }
    data.value = buffData.value;
    data.time = buffData.time;
    data.endless = !buffData.endless.HasValue ? new bool?((double) buffData.time < 0.0) : buffData.endless;
    data.enable = true;
    data.conditionIndex = buffData.conditionIndex;
    data.interval = buffData.interval;
    data.damage = buffData.damage;
    data.valueType = buffData.valueType;
    data.fromObjectID = buffData.fromObjectID;
    data.skillId = buffData.skillId;
    switch (data.type)
    {
      case BuffParam.BUFFTYPE.REGENERATE:
      case BuffParam.BUFFTYPE.REGENERATE_PROPORTION:
        if ((double) data.interval == 0.0)
        {
          data.interval = 2f;
          break;
        }
        break;
      case BuffParam.BUFFTYPE.BREAK_GHOST_FORM:
        data.value = 1;
        break;
      case BuffParam.BUFFTYPE.SHIELD:
        if (Object.op_Inequality((Object) this.player, (Object) null))
        {
          int num = Mathf.Max(0, (int) ((double) this.player.hpMax * ((double) buffData.value / 100.0)));
          this.player.ShieldHp = (XorInt) num;
          this.player.ShieldHpMax = (XorInt) num;
          data.fromEquipIndex = buffData.fromEquipIndex;
          data.fromSkillIndex = buffData.fromSkillIndex;
          break;
        }
        break;
      case BuffParam.BUFFTYPE.SUBSTITUTE:
        this.ResetInterval(type);
        this.substituteCtrl.Create(data.value);
        break;
    }
    if (buffData.isPlayLoopEffect)
      this.PlayBuffLoopEffect(data);
    this.shouldSync = true;
    return true;
  }

  public bool BuffEnd(BuffParam.BUFFTYPE type, bool isPlayEndEffect = true)
  {
    if (type == BuffParam.BUFFTYPE.NONE || type >= BuffParam.BUFFTYPE.MAX)
      return false;
    this.shouldSync = true;
    BuffParam.BuffData data = this.data[(int) type];
    if (this.passive != null && this.passive.conditionsAbilityList != null)
    {
      int conditionIndex = data.conditionIndex;
      if (conditionIndex >= 0 && conditionIndex < this.passive.conditionsAbilityList.Count)
      {
        this.passive.conditionsAbilityList[conditionIndex].counterAttackNum = 0;
        this.passive.conditionsAbilityList[conditionIndex].cleaveComboNum = 0;
      }
    }
    int num = data.EndBuff() ? 1 : 0;
    if (num != 0)
      this.EndBuffEffect(data, isPlayEndEffect);
    if (type != BuffParam.BUFFTYPE.SUBSTITUTE)
      return num != 0;
    this.substituteCtrl.End();
    return num != 0;
  }

  public void AllBuffEnd(bool sync)
  {
    for (int type = 0; type < 221; ++type)
      this.chara.OnBuffEnd((BuffParam.BUFFTYPE) type, false);
    if (!sync)
      return;
    this.chara.SendBuffSync();
  }

  public void PlayBuffLoopEffectAll()
  {
    for (int index = 0; index < 221; ++index)
    {
      if (this.data[index].enable)
        this.PlayBuffLoopEffect(this.data[index]);
    }
  }

  public void OnDetachServant(DisableNotifyMonoBehaviour servant)
  {
    int index = 0;
    for (int count = this.loopEffect.Count; index < count; ++index)
    {
      if (Object.op_Equality((Object) this.loopEffect[index].effect, (Object) ((Component) servant).gameObject))
      {
        if (Object.op_Inequality((Object) this.loopEffect[index].effect, (Object) null))
          EffectManager.ReleaseEffect(this.loopEffect[index].effect);
        this.loopEffect.RemoveAt(index);
        break;
      }
    }
  }

  public void PlayBuffLoopEffect(BuffParam.BuffData data)
  {
    this.PlayBuffEffect(data, "LOOP", true);
  }

  private void PlayBuffEffect(BuffParam.BuffData data, string type_key, bool loop)
  {
    if (data == null || Object.op_Equality((Object) this.chara.effectPlayProcessor, (Object) null))
      return;
    List<EffectPlayProcessor.EffectSetting> effectSettingList = (List<EffectPlayProcessor.EffectSetting>) null;
    string str1 = (string) null;
    string setting_name = $"BUFF_{type_key}_{data.type.ToString()}";
    if (Object.op_Inequality((Object) this.player, (Object) null) && Object.op_Inequality((Object) this.player.loader, (Object) null) && this.player.loader.loadInfo != null)
    {
      str1 = "_PLC" + (this.player.loader.loadInfo.weaponModelID / 1000).ToString("D2");
      effectSettingList = this.chara.effectPlayProcessor.GetSettings(setting_name + str1);
    }
    if (effectSettingList == null)
      effectSettingList = this.chara.effectPlayProcessor.GetSettings(setting_name);
    if (effectSettingList == null)
    {
      string str2 = "BUFF";
      switch (data.type)
      {
        case BuffParam.BUFFTYPE.MOVE_SPEED_DOWN:
        case BuffParam.BUFFTYPE.POISON:
        case BuffParam.BUFFTYPE.BURNING:
        case BuffParam.BUFFTYPE.DEADLY_POISON:
        case BuffParam.BUFFTYPE.ELECTRIC_SHOCK:
        case BuffParam.BUFFTYPE.INK_SPLASH:
        case BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN:
        case BuffParam.BUFFTYPE.CANT_HEAL_HP:
        case BuffParam.BUFFTYPE.BLIND:
        case BuffParam.BUFFTYPE.EROSION:
        case BuffParam.BUFFTYPE.STONE:
        case BuffParam.BUFFTYPE.SOIL_SHOCK:
        case BuffParam.BUFFTYPE.BLEEDING:
        case BuffParam.BUFFTYPE.ACID:
        case BuffParam.BUFFTYPE.CORRUPTION:
        case BuffParam.BUFFTYPE.STIGMATA:
        case BuffParam.BUFFTYPE.CYCLONIC_THUNDERSTORM:
          str2 = "DEBUFF";
          break;
        case BuffParam.BUFFTYPE.GHOST_FORM:
        case BuffParam.BUFFTYPE.SILENCE:
        case BuffParam.BUFFTYPE.SUBSTITUTE:
        case BuffParam.BUFFTYPE.INVINCIBLE_BUFF_CANCELLATION_EXPAND:
        case BuffParam.BUFFTYPE.ORACLE_OHS_PROTECTION:
        case BuffParam.BUFFTYPE.DAMAGE_MOTION_STOP:
          str2 = "";
          break;
        case BuffParam.BUFFTYPE.MAD_MODE:
          str2 = "MADMODE";
          break;
      }
      setting_name = $"BUFF_{type_key}_{str2}_DEFAULT";
      if (Object.op_Inequality((Object) this.player, (Object) null))
        effectSettingList = this.chara.effectPlayProcessor.GetSettings(setting_name + str1);
    }
    if (effectSettingList == null)
      effectSettingList = this.chara.effectPlayProcessor.GetSettings(setting_name);
    if (effectSettingList == null)
      return;
    int index1 = 0;
    for (int count1 = effectSettingList.Count; index1 < count1; ++index1)
    {
      EffectPlayProcessor.EffectSetting setting1 = effectSettingList[index1];
      if (data.skillId > 0U)
      {
        SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(data.skillId);
        if (skillItemData != null)
        {
          for (int index2 = 0; index2 < skillItemData.supportType.Length; ++index2)
          {
            if (skillItemData.supportType[index2] == data.type && !string.IsNullOrEmpty(skillItemData.supportEffectName[index2]))
            {
              EffectPlayProcessor.EffectSetting effectSetting = setting1.Clone();
              effectSetting.effectName = skillItemData.supportEffectName[index2];
              setting1 = effectSetting;
            }
          }
        }
      }
      if (loop)
      {
        bool flag = false;
        int index3 = 0;
        for (int count2 = this.loopEffect.Count; index3 < count2; ++index3)
        {
          EffectPlayProcessor.EffectSetting setting2 = this.loopEffect[index3].setting;
          if (setting2 != null && setting2.effectName == setting1.effectName && setting2.nodeName == setting1.nodeName)
          {
            if (!this.loopEffect[index3].linkData.Contains(data))
              this.loopEffect[index3].linkData.Add(data);
            flag = true;
            break;
          }
        }
        if (flag)
          continue;
      }
      Transform transform = this.chara.effectPlayProcessor.PlayEffect(setting1);
      if (Object.op_Inequality((Object) transform, (Object) null) & loop)
      {
        ((Component) transform).gameObject.AddComponent<DisableNotifyMonoBehaviour>().SetNotifyMaster((DisableNotifyMonoBehaviour) this.chara);
        this.loopEffect.Add(new BuffParam.EffectInfo()
        {
          effect = ((Component) transform).gameObject,
          setting = setting1,
          linkData = {
            data
          }
        });
        if (data.type == BuffParam.BUFFTYPE.SLIDE)
          ((Component) transform).gameObject.AddComponent<BuffSlideEffectController>().Initialize(this.player);
        else if (data.type == BuffParam.BUFFTYPE.INVINCIBLECOUNT)
          this.invincibleCountAnimator = ((Component) transform).GetComponent<Animator>();
        else if (data.type == BuffParam.BUFFTYPE.INVINCIBLE_BADSTATUS)
          this.invincibleBadStatusAnimator = ((Component) transform).GetComponent<Animator>();
        else if (data.type == BuffParam.BUFFTYPE.INVINCIBLE_BUFF_CANCELLATION)
          this.invincibleBuffCancellationAnimator = ((Component) transform).GetComponent<Animator>();
      }
    }
  }

  private void EndBuffEffect(BuffParam.BuffData data, bool isPlayEndEffect)
  {
    int index = 0;
    while (index < this.loopEffect.Count)
    {
      if (this.loopEffect[index].linkData.Contains(data))
        this.loopEffect[index].linkData.Remove(data);
      if (this.loopEffect[index].linkData.Count <= 0)
      {
        GameObject effect = this.loopEffect[index].effect;
        this.loopEffect.RemoveAt(index);
        if (Object.op_Inequality((Object) effect, (Object) null))
        {
          if (data.type == BuffParam.BUFFTYPE.SLIDE)
            ((Behaviour) effect.GetComponent<BuffSlideEffectController>()).enabled = false;
          else if (data.type == BuffParam.BUFFTYPE.INVINCIBLECOUNT)
            this.invincibleCountAnimator = (Animator) null;
          else if (data.type == BuffParam.BUFFTYPE.INVINCIBLE_BADSTATUS)
            this.invincibleBadStatusAnimator = (Animator) null;
          else if (data.type == BuffParam.BUFFTYPE.INVINCIBLE_BUFF_CANCELLATION)
            this.invincibleBuffCancellationAnimator = (Animator) null;
          EffectManager.ReleaseEffect(effect, isPlayEndEffect);
        }
      }
      else
        ++index;
    }
  }

  public BuffParam.BuffSyncParam CreateSyncParamIfNeeded()
  {
    if (!this.shouldSync)
      return (BuffParam.BuffSyncParam) null;
    this.shouldSync = false;
    return this.CreateSyncParam();
  }

  public BuffParam.BuffSyncParam CreateSyncParam(BuffParam.BUFFTYPE nowBuffType = BuffParam.BUFFTYPE.NONE)
  {
    BuffParam.BuffSyncParam syncParam = new BuffParam.BuffSyncParam();
    for (int index = 0; index < 221; ++index)
    {
      if (this.data[index].enable && !this.IsIgnoreSyncType(this.data[index].type))
      {
        BuffParam.BuffSyncData buffSyncData = new BuffParam.BuffSyncData();
        buffSyncData.type = (int) this.data[index].type;
        buffSyncData.time = this.data[index].time;
        buffSyncData.value = this.data[index].value;
        buffSyncData.valueType = (int) this.data[index].valueType;
        buffSyncData.conditionIndex = this.data[index].conditionIndex;
        buffSyncData.fromObjectID = this.data[index].fromObjectID;
        buffSyncData.fromEquipIndex = this.data[index].fromEquipIndex;
        buffSyncData.fromSkillIndex = this.data[index].fromSkillIndex;
        string empty = string.Empty;
        if (this.data[index].endless.HasValue)
          empty = this.data[index].endless.ToString();
        buffSyncData.endless = empty;
        buffSyncData.skillId = (int) this.data[index].skillId;
        buffSyncData.isOwnerEnemyBuffStart = (BuffParam.BUFFTYPE) index == nowBuffType;
        syncParam.buffDatas.Add(buffSyncData);
      }
    }
    if (Object.op_Inequality((Object) this.player, (Object) null))
      syncParam.shieldHp = (int) this.player.ShieldHp;
    return syncParam;
  }

  private bool IsIgnoreSyncType(BuffParam.BUFFTYPE type) => type == BuffParam.BUFFTYPE.INK_SPLASH;

  public void SetSyncParam(BuffParam.BuffSyncParam sync_param, bool isPlayLoopEffect = true)
  {
    BuffParam.BuffSyncData[] buffSyncDataArray = new BuffParam.BuffSyncData[221];
    int index = 0;
    for (int count = sync_param.buffDatas.Count; index < count; ++index)
    {
      int type = sync_param.buffDatas[index].type;
      if (type >= 0 && type < 221)
        buffSyncDataArray[type] = sync_param.buffDatas[index];
    }
    for (int type = 0; type < 221; ++type)
    {
      if (this.data[type].enable)
      {
        if (buffSyncDataArray[type] != null)
        {
          this.data[type].SetBuffData(buffSyncDataArray[type]);
          this.data[type].isCallBuffEnd = false;
          this.chara.OnBuffStart(this.data[type]);
          this.data[type].isCallBuffEnd = true;
        }
        else
          this.chara.OnBuffEnd((BuffParam.BUFFTYPE) type, false);
      }
      else if (buffSyncDataArray[type] != null)
      {
        BuffParam.BuffData buffData = new BuffParam.BuffData();
        buffData.type = (BuffParam.BUFFTYPE) type;
        buffData.isPlayLoopEffect = isPlayLoopEffect;
        buffData.SetBuffData(buffSyncDataArray[type]);
        this.chara.OnBuffStart(buffData);
      }
    }
    if (!Object.op_Inequality((Object) this.player, (Object) null))
      return;
    this.player.ShieldHp = (XorInt) sync_param.shieldHp;
  }

  public void SetSyncParamForExplorePlayerStatus(BuffParam.BuffSyncParam sync_param)
  {
    BuffParam.BuffSyncData[] buffSyncDataArray = new BuffParam.BuffSyncData[221];
    int index1 = 0;
    for (int count = sync_param.buffDatas.Count; index1 < count; ++index1)
    {
      int type = sync_param.buffDatas[index1].type;
      if (type >= 0 && type < 221)
        buffSyncDataArray[type] = sync_param.buffDatas[index1];
    }
    for (int index2 = 0; index2 < 221; ++index2)
    {
      BuffParam.BuffData buffData = this.data[index2];
      if (buffData.enable)
      {
        if (buffSyncDataArray[index2] != null)
        {
          this.data[index2].SetBuffData(buffSyncDataArray[index2]);
        }
        else
        {
          buffData.enable = false;
          buffData.time = 0.0f;
          buffData.value = 0;
          buffData.interval = 0.0f;
          buffData.progress = 0.0f;
          buffData.avoidCount = 0;
          buffData.endless = new bool?(false);
          buffData.skillId = 0U;
        }
      }
      else if (buffSyncDataArray[index2] != null)
      {
        buffData.enable = true;
        buffData.SetBuffData(buffSyncDataArray[index2]);
      }
    }
    if (!Object.op_Inequality((Object) this.player, (Object) null))
      return;
    this.player.ShieldHp = (XorInt) sync_param.shieldHp;
  }

  private bool CheckAbilityByEventID(uint abilityID, int eventID)
  {
    if (Singleton<AbilityTable>.IsValid())
    {
      AbilityTable.Ability ability = Singleton<AbilityTable>.I.GetAbility(abilityID);
      if (ability != null && ability.unlockEventId > 0)
        return ability.unlockEventId == eventID;
    }
    return true;
  }

  private bool CheckAbilityInfoByEventID(
    AbilityDataTable.AbilityData.AbilityInfo abilityInfo,
    int eventID)
  {
    return abilityInfo == null || abilityInfo.unlockEventId <= 0 || abilityInfo.unlockEventId == eventID;
  }

  private bool RazerActiveFullArmorSet(uint abilityId)
  {
    CharaInfo charaInfo = this.player.createInfo.charaInfo;
    List<int> intList1 = new List<int>()
    {
      80000020,
      80000030,
      80000040,
      80000050
    };
    List<int> intList2 = new List<int>()
    {
      80000021,
      80000031,
      80000041,
      80000051
    };
    int num = 0;
    if (charaInfo.sex == 0)
    {
      foreach (CharaInfo.EquipItem equip in charaInfo.equipSet)
      {
        if (intList1.IndexOf(equip.eId) > -1)
          ++num;
      }
    }
    else
    {
      foreach (CharaInfo.EquipItem equip in charaInfo.equipSet)
      {
        if (intList2.IndexOf(equip.eId) > -1)
          ++num;
      }
    }
    return num == 4;
  }

  private bool RazerActiveWeapon(uint abilityId)
  {
    return new List<int>()
    {
      60020200,
      60020201,
      60020202,
      60030200,
      60030201,
      60030202
    }.IndexOf(this.player.weaponData.eId) > -1;
  }

  private bool RazerS2ActiveWeapon(uint abilityId)
  {
    return new List<int>()
    {
      99200101,
      99200102,
      99200103,
      99200104
    }.IndexOf(this.player.weaponData.eId) > -1;
  }

  private BuffParam.ConditionsAbility CreateConditionsAbility(
    AbilityDataTable.AbilityData data,
    EQUIPMENT_TYPE equipType,
    SP_ATTACK_TYPE spAttackType,
    int eventId)
  {
    return this.CreateConditionsAbility(data.info, equipType, spAttackType, eventId);
  }

  private BuffParam.ConditionsAbility CreateConditionsAbility(
    AbilityDataTable.AbilityData.AbilityInfo[] dataInfo,
    EQUIPMENT_TYPE equipType,
    SP_ATTACK_TYPE spAttackType,
    int eventId)
  {
    BuffParam.ConditionsAbility conditionsAbility = (BuffParam.ConditionsAbility) null;
    for (int index1 = 0; index1 < dataInfo.Length; ++index1)
    {
      AbilityDataTable.AbilityData.AbilityInfo abilityInfo = dataInfo[index1];
      if (this.CheckAbilityInfoByEventID(abilityInfo, eventId))
      {
        bool flag1 = false;
        int enablesCount1 = abilityInfo.getEnablesCount();
        for (int index2 = 0; index2 < enablesCount1; ++index2)
        {
          if (abilityInfo.enables[index2].type != ABILITY_ENABLE_TYPE.NONE)
          {
            flag1 = true;
            break;
          }
        }
        if (flag1)
        {
          if (conditionsAbility == null)
            conditionsAbility = new BuffParam.ConditionsAbility();
          conditionsAbility.conditionInfos[index1] = new BuffParam.ConditionsAbility.ConditionsAbilityInfo();
          conditionsAbility.conditionInfos[index1].abilityInfo = abilityInfo;
          bool flag2 = true;
          bool rEnable = true;
          int enablesCount2 = abilityInfo.getEnablesCount();
          for (int index3 = 0; index3 < enablesCount2; ++index3)
          {
            flag2 &= Utility.IsEnableEquip(equipType, abilityInfo.enables[index3].type);
            if (Utility.CheckEnableSpAttackType(ref rEnable, spAttackType, abilityInfo.enables[index3].SpAtkEnableTypeBit) && !rEnable)
              break;
          }
          conditionsAbility.conditionInfos[index1].isValid = flag2 & rEnable;
        }
      }
    }
    if (conditionsAbility == null)
      return (BuffParam.ConditionsAbility) null;
    for (int index = 0; index < dataInfo.Length; ++index)
    {
      BuffParam.ConditionsAbility.ConditionsAbilityInfo conditionInfo = conditionsAbility.conditionInfos[index];
      if (conditionInfo != null)
      {
        if (conditionInfo.abilityInfo.type == ABILITY_TYPE.NARROW_ESCAPE)
          this.AddAbilityParam(conditionInfo.abilityInfo);
        AbilityAtkBase abilityAtkBase = (AbilityAtkBase) null;
        if (conditionInfo.abilityInfo.enables[0].type == ABILITY_ENABLE_TYPE.RAZER_FULLSET_ACTIVE)
        {
          if (conditionInfo.abilityInfo.type == ABILITY_TYPE.TOLERANCE_UP && this.RazerActiveFullArmorSet(this.oldAbilityId))
            abilityAtkBase = this.GetAbilityAtkParam(conditionInfo.abilityInfo);
        }
        else if (conditionInfo.abilityInfo.enables[0].type == ABILITY_ENABLE_TYPE.RAZER_WEAPON_ACTIVE || conditionInfo.abilityInfo.target == "RAZER_WEAPON")
        {
          if ((conditionInfo.abilityInfo.type == ABILITY_TYPE.DAMAGE_UP_ELEMENT || conditionInfo.abilityInfo.type == ABILITY_TYPE.GAUGE_INCREASE_UP) && this.RazerActiveWeapon(this.oldAbilityId))
            abilityAtkBase = this.GetAbilityAtkParam(conditionInfo.abilityInfo);
        }
        else if (conditionInfo.abilityInfo.enables[0].type == ABILITY_ENABLE_TYPE.RAZER_S2_WEAPON_ACTIVE)
        {
          if (this.RazerS2ActiveWeapon(this.oldAbilityId) && (conditionInfo.abilityInfo.type == ABILITY_TYPE.DAMAGE_UP_ELEMENT || conditionInfo.abilityInfo.type == ABILITY_TYPE.ATTACK_SPEED_UP || conditionInfo.abilityInfo.type == ABILITY_TYPE.DAMAGE_DOWN))
            abilityAtkBase = this.GetAbilityAtkParam(conditionInfo.abilityInfo);
        }
        else
          abilityAtkBase = this.GetAbilityAtkParam(conditionInfo.abilityInfo);
        if (abilityAtkBase != null)
          conditionInfo.atkBase = abilityAtkBase;
      }
    }
    return conditionsAbility;
  }

  public void AddAbility(
    uint ability_id,
    int AP,
    EQUIPMENT_TYPE equipType,
    SP_ATTACK_TYPE spAttackType,
    int eventID)
  {
    this.oldAbilityId = ability_id;
    AbilityDataTable.AbilityData abilityData = Singleton<AbilityDataTable>.I.GetAbilityData(ability_id, AP);
    if (abilityData == null || !Utility.IsEnableEquip(equipType, abilityData.enableEquipType) || !this.CheckAbilityByEventID(ability_id, eventID))
      return;
    switch (ability_id)
    {
      case 80000200:
        if (!this.RazerActiveFullArmorSet(ability_id) && !this.RazerActiveWeapon(ability_id))
          return;
        break;
      case 99201003:
        if (!this.RazerS2ActiveWeapon(ability_id))
          return;
        break;
    }
    BuffParam.ConditionsAbility conditionsAbility = this.CreateConditionsAbility(abilityData, equipType, spAttackType, eventID);
    if (conditionsAbility != null)
      this.passive.conditionsAbilityList.Add(conditionsAbility);
    for (int index = 0; index < 3; ++index)
    {
      if (this.CheckAbilityInfoByEventID(abilityData.info[index], eventID) && (conditionsAbility == null || conditionsAbility.conditionInfos[index] == null))
      {
        AbilityAtkBase abilityAtkParam = this.GetAbilityAtkParam(abilityData.info[index]);
        if (abilityAtkParam != null)
          this.passive.abilityAtkList.Add(abilityAtkParam);
        else
          this.AddAbilityParam(abilityData.info[index]);
      }
    }
  }

  public void AddAbilityItemParam(
    AbilityDataTable.AbilityData.AbilityInfo[] info,
    EQUIPMENT_TYPE equipType,
    SP_ATTACK_TYPE spAttackType,
    int eventID)
  {
    BuffParam.ConditionsAbility conditionsAbility = this.CreateConditionsAbility(info, equipType, spAttackType, eventID);
    if (conditionsAbility != null)
      this.passive.conditionsAbilityList.Add(conditionsAbility);
    for (int index = 0; index < info.Length; ++index)
    {
      if (this.CheckAbilityInfoByEventID(info[index], eventID) && (conditionsAbility == null || conditionsAbility.conditionInfos[index] == null))
      {
        AbilityAtkBase abilityAtkParam = this.GetAbilityAtkParam(info[index]);
        if (abilityAtkParam != null)
          this.passive.abilityAtkList.Add(abilityAtkParam);
        else
          this.AddAbilityParam(info[index]);
      }
    }
  }

  private void AddAbilityParam(AbilityDataTable.AbilityData.AbilityInfo info, bool isRemove = false)
  {
    int num = 1;
    if (isRemove)
      num *= -1;
    switch (info.type)
    {
      case ABILITY_TYPE.DAMAGE_UP_SP_ACTION:
        if (!(info.target == "ARROW"))
          break;
        this.passive.bleedUp += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.GUARD_UP:
        this.passive.guardUp += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.CHARGE_SPEED_UP:
        if (!Enum.IsDefined(typeof (EQUIPMENT_TYPE), (object) info.target))
          break;
        switch ((EQUIPMENT_TYPE) Enum.Parse(typeof (EQUIPMENT_TYPE), info.target))
        {
          case EQUIPMENT_TYPE.TWO_HAND_SWORD:
            this.passive.chargeSwordsTimeRate += (float) (int) info.value * 0.01f * (float) num;
            return;
          case EQUIPMENT_TYPE.SPEAR:
            this.passive.chargeSpearTimeRate += (float) (int) info.value * 0.01f * (float) num;
            return;
          case EQUIPMENT_TYPE.TWO_HAND_SWORD | EQUIPMENT_TYPE.SPEAR:
            return;
          case EQUIPMENT_TYPE.PAIR_SWORDS:
            this.passive.chargePairSwordsTimeRate += (float) (int) info.value * 0.01f * (float) num;
            return;
          case EQUIPMENT_TYPE.ARROW:
            this.passive.chargeArrowTimeRate += (float) (int) info.value * 0.01f * (float) num;
            return;
          default:
            return;
        }
      case ABILITY_TYPE.BAD_STATUS_UP:
        if (!Enum.IsDefined(typeof (BuffParam.BAD_STATUS_UP), (object) info.target))
          break;
        this.passive.badStatusUp[(int) Enum.Parse(typeof (BuffParam.BAD_STATUS_UP), info.target)] += (float) (int) info.value * (float) num;
        break;
      case ABILITY_TYPE.TOLERANCE_UP:
        if (!Enum.IsDefined(typeof (BuffParam.TOLERANCETYPE), (object) info.target))
          break;
        this.passive.tolerance[(int) Enum.Parse(typeof (BuffParam.TOLERANCETYPE), info.target)] -= (int) info.value * num;
        break;
      case ABILITY_TYPE.HEAL_UP:
        this.passive.healUP += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.HEAL_SPEEDUP:
        this.passive.hpHealSpeedUp += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.AVOID_UP:
        this.passive.avoidUp += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.MOVE_SPEED_UP:
        this.passive.moveSpeedUp += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.SKILL_ABSORB_UP:
        this.passive.skillAbsorbUp += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.SKILL_SPEED_UP:
        this.passive.skillTimeRate += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.DAMAGE_DOWN:
        this.passive.damageDown += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.ATTACK_SPEED_UP:
        this.passive.attackSpeedUp += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.NARROW_ESCAPE:
        if (this.passive.firstInitialized)
          break;
        this.passive.narrowEscape += (int) info.value * num;
        break;
      case ABILITY_TYPE.BREAK_GHOST_FORM:
        if (isRemove)
        {
          --this.passive.breakGhostFormCounter;
          if (this.passive.breakGhostFormCounter >= 0)
            break;
          this.passive.breakGhostFormCounter = 0;
          break;
        }
        ++this.passive.breakGhostFormCounter;
        break;
      case ABILITY_TYPE.SPEAR_RUSH_DISTANCE_UP:
        this.passive.spearRushDistanceRate += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.DAMAGE_DOWN_SP_ACTION:
        if (!(info.target == "ARROW"))
          break;
        this.passive.bleedUp -= (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.CHARGE_SPEED_DOWN:
        if (!Enum.IsDefined(typeof (EQUIPMENT_TYPE), (object) info.target))
          break;
        switch ((EQUIPMENT_TYPE) Enum.Parse(typeof (EQUIPMENT_TYPE), info.target))
        {
          case EQUIPMENT_TYPE.TWO_HAND_SWORD:
            this.passive.chargeSwordsTimeRate -= (float) (int) info.value * 0.01f * (float) num;
            return;
          case EQUIPMENT_TYPE.SPEAR:
            this.passive.chargeSpearTimeRate -= (float) (int) info.value * 0.01f * (float) num;
            return;
          case EQUIPMENT_TYPE.TWO_HAND_SWORD | EQUIPMENT_TYPE.SPEAR:
            return;
          case EQUIPMENT_TYPE.PAIR_SWORDS:
            this.passive.chargePairSwordsTimeRate -= (float) (int) info.value * 0.01f * (float) num;
            return;
          case EQUIPMENT_TYPE.ARROW:
            this.passive.chargeArrowTimeRate -= (float) (int) info.value * 0.01f * (float) num;
            return;
          default:
            return;
        }
      case ABILITY_TYPE.AVOID_DOWN:
        this.passive.avoidUp -= (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.MOVE_SPEED_DOWN:
        this.passive.moveSpeedUp -= (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.SKILL_ABSORB_DOWN:
        this.passive.skillAbsorbUp -= (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.SKILL_SPEED_DOWN:
        this.passive.skillTimeRate -= (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.JUSTGUARD_EXTEND_RATE:
        this.passive.justGuardExtendRate += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.CHARGE_HEAT_ARROW_SPEED_UP:
        this.passive.chargeHeatArrowTimeRate += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.CHARGE_HEAT_ARROW_SPEED_DOWN:
        this.passive.chargeHeatArrowTimeRate -= (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.SKILL_ABSORBUP_ONLY_ATK_FIRE:
        this.passive.skillAbsorbUp_OnlyAttackAndElement.fire += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.SKILL_ABSORBUP_ONLY_ATK_WATER:
        this.passive.skillAbsorbUp_OnlyAttackAndElement.water += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.SKILL_ABSORBUP_ONLY_ATK_THUNDER:
        this.passive.skillAbsorbUp_OnlyAttackAndElement.thunder += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.SKILL_ABSORBUP_ONLY_ATK_SOIL:
        this.passive.skillAbsorbUp_OnlyAttackAndElement.soil += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.SKILL_ABSORBUP_ONLY_ATK_LIGHT:
        this.passive.skillAbsorbUp_OnlyAttackAndElement.light += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.SKILL_ABSORBUP_ONLY_ATK_DARK:
        this.passive.skillAbsorbUp_OnlyAttackAndElement.dark += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.SOUL_GAUGE_INCREASE_UP:
        this.passive.soulGaugeIncreaseRate += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.SOUL_GAUGE_INCREASE_DOWN:
        this.passive.soulGaugeIncreaseRate -= (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.SOUL_CHARGE_SPEED_UP:
        if (!Enum.IsDefined(typeof (EQUIPMENT_TYPE), (object) info.target))
          break;
        this.passive.soulChargeTimeRate += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.GAUGE_INCREASE_UP:
        this.passive.gaugeIncreaseRate += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.GAUGE_INCREASE_DOWN:
        this.passive.gaugeIncreaseRate -= (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.BOOST_MOVE_SPEED_UP:
        this.passive.boostMoveSpeedUp += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.BOOST_AVOID_UP:
        this.passive.boostAvoidUp += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.BOOST_DAMAGE_DOWN:
        this.passive.boostDamageDown += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.SHADOWSEALING_EXTEND:
        this.passive.shadowSealingExtend += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.SHADOWSEALING_EXTEND_ARROW:
        this.passive.shadowSealingExtendArrow += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.RESIST_FIELD_DEBUFF:
        this.SetPassiveFieldBuffResist(info.target, (float) (int) info.value * 0.01f * (float) num);
        this.SetPassiveFieldBuffResistByType(info.target, (float) (int) info.value * 0.01f * (float) num);
        break;
      case ABILITY_TYPE.RECEIVED_DAMAGE_UP:
        this.passive.damageDown -= (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.HEAL_DOWN:
        this.passive.healUP -= (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.BADSTATUS_TOLERANCE_DOWN:
        if (!Enum.IsDefined(typeof (BuffParam.TOLERANCETYPE), (object) info.target))
          break;
        this.passive.tolerance[(int) Enum.Parse(typeof (BuffParam.TOLERANCETYPE), info.target)] += (int) info.value * num;
        break;
      case ABILITY_TYPE.DISTANCE_UP_FROM_AVOID:
        this.passive.distanceRateFromAvoid += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.GAUGE_DECREASE_UP:
        this.passive.gaugeDecreaseRate += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.GAUGE_DECREASE_DOWN:
        this.passive.gaugeDecreaseRate -= (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.SUBGAUGE_INCREASE_UP:
        this.passive.subGaugeIncreaseRate += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.SUBGAUGE_INCREASE_DOWN:
        this.passive.subGaugeIncreaseRate -= (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.LOCKON_TIME_UP:
        this.passive.lockOnTimeRate += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.LOCKON_TIME_DOWN:
        this.passive.lockOnTimeRate -= (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.HEAL_UP_DEPENDS_WEAPON:
        if (Object.op_Equality((Object) this.player, (Object) null) || !Enum.IsDefined(typeof (EQUIPMENT_TYPE), (object) info.target))
          break;
        EQUIPMENT_TYPE equipmentType = Player.ConvertAttackModeToEquipmentType(this.player.attackMode);
        if (equipmentType != (EQUIPMENT_TYPE) Enum.Parse(typeof (EQUIPMENT_TYPE), info.target))
          break;
        bool flag = true;
        bool rEnable = true;
        int enablesCount = info.getEnablesCount();
        for (int index = 0; index < enablesCount; ++index)
        {
          if (!Enum.IsDefined(typeof (ABILITY_ENABLE_TYPE), (object) info.enables[index].type))
            return;
          if (Utility.CheckEnableSpAttackType(ref rEnable, this.player.spAttackType, info.enables[index].type))
          {
            if (!rEnable)
              break;
          }
          else
            flag = Utility.IsEnableEquip(equipmentType, info.enables[index].type);
        }
        if (!(flag & rEnable))
          break;
        this.passive.healUpDependsWeapon += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.BURST_RELOAD_SPEED_UP:
        this.passive.burstReloadActionSpeed += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.BURST_ADDITIONAL_BULLET:
        this.passive.additionalMaxBulletCnt += (int) info.value * num;
        break;
      case ABILITY_TYPE.DRAGONARMOR_DAMAGE_UP:
        this.passive.dragonArmorDamageRate += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.BAD_STATUS_RATE_UP:
        if (!Enum.IsDefined(typeof (BuffParam.BAD_STATUS_UP), (object) info.target))
          break;
        this.passive.badStatusRateUp[(int) Enum.Parse(typeof (BuffParam.BAD_STATUS_UP), info.target)] += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.BOOST_ATTACK_SPEED_UP:
        this.passive.boostAttackSpeedUp += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.DISTANCE_UP_IAI:
        this.passive.distanceRateIai += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.BAD_STATUS_DOWN_RATE_UP:
        this.passive.badStatusRateUp[2] += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.SOUL_ARROW_LOCKON_UP:
        this.passive.addSoulArrowLockCount += (int) info.value * num;
        break;
      case ABILITY_TYPE.ARROW_RAIN_TOTAL_HIT_RATE:
        this.passive.arrowRainNumRate += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.BAD_STATUS_CONCUSSION_RATE_UP:
        this.passive.badStatusRateUp[4] += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.BURST_SPEAR_SPINTIME_RATE:
        this.passive.burstSpearSpinTimeRate += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.BURST_SPEAR_RUSH_SPEED_UP:
        this.passive.spearRushSpeedRate += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.ORACLE_THS_HORIZONTAL_SPEED_UP:
        this.passive.oracleThsHorizontalSpeedUp += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.CONCUSSION_EXTEND:
        this.passive.concussionExtend += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.ORACLE_THS_SPIN_SMASH_CHARGE_SPEED_UP:
        this.passive.oracleThsSpinSmashTimeRate += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.ORACLE_THS_DIVE_SMASH_CHARGE_SPEED_UP:
        this.passive.oracleThsDiveSmashTimeRate += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.ORACLE_THS_WHEEL_SMASH_CHARGE_SPEED_UP:
        this.passive.oracleThsWheelSmashTimeRate += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.TELEPORT_UP:
        this.passive.teleportUp += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.STOCK_ADD_INIT:
        this.passive.stockAddInit += (int) info.value * num;
        break;
      case ABILITY_TYPE.GUTS_TIME_RATE_UP:
        this.passive.gutsTimeRateUp += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.FREE_STOCK_PROBABILITY:
        this.passive.freeStockProbability += (float) (int) info.value * 0.01f * (float) num;
        break;
      case ABILITY_TYPE.ORACLE_OHS_PROTECTION_DOUBLE_PROBABILITY:
        this.passive.oracleOhsProtectionDoubleProbability += (int) info.value * num;
        break;
    }
  }

  private AbilityAtkBase GetAbilityAtkParam(AbilityDataTable.AbilityData.AbilityInfo info)
  {
    AbilityAtkBase abilityAtkParam = (AbilityAtkBase) null;
    bool flag = false;
    switch (info.type)
    {
      case ABILITY_TYPE.DAMAGE_UP_WEAPON:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkWeapon();
        break;
      case ABILITY_TYPE.DAMAGE_UP_ELEMENT:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkElement();
        break;
      case ABILITY_TYPE.DAMAGE_UP_ENEMY_TYPE:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkEnemyType();
        break;
      case ABILITY_TYPE.DAMAGE_UP_ENEMY_NAME:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkEnemyName();
        break;
      case ABILITY_TYPE.DAMAGE_UP_SP_ACTION:
        if (info.target != "ARROW")
        {
          abilityAtkParam = (AbilityAtkBase) new AbilityAtkSp();
          break;
        }
        break;
      case ABILITY_TYPE.DAMAGE_UP_WEAK:
        abilityAtkParam = (AbilityAtkBase) new AbilityWeakAtk();
        break;
      case ABILITY_TYPE.DAMAGE_UP_ENEMY_DOWN:
        abilityAtkParam = (AbilityAtkBase) new AbilityEnemyDownAtk();
        break;
      case ABILITY_TYPE.LUNATIC_TEAR:
        abilityAtkParam = (AbilityAtkBase) new AbilityLunaticTear();
        break;
      case ABILITY_TYPE.BUFF_WING:
        abilityAtkParam = (AbilityAtkBase) new AbilityBuffWing();
        break;
      case ABILITY_TYPE.DAMAGE_DOWN_WEAPON:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkWeapon();
        flag = true;
        break;
      case ABILITY_TYPE.DAMAGE_DOWN_ELEMENT:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkElement();
        flag = true;
        break;
      case ABILITY_TYPE.DAMAGE_DOWN_ENEMY_TYPE:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkEnemyType();
        flag = true;
        break;
      case ABILITY_TYPE.DAMAGE_DOWN_ENEMY_NAME:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkEnemyName();
        flag = true;
        break;
      case ABILITY_TYPE.DAMAGE_DOWN_SP_ACTION:
        if (info.target != "ARROW")
        {
          abilityAtkParam = (AbilityAtkBase) new AbilityAtkSp();
          flag = true;
          break;
        }
        break;
      case ABILITY_TYPE.DAMAGE_UP_FROM_AVOID:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkTypeFromAvoid();
        break;
      case ABILITY_TYPE.COUNTER_DAMAGE_UP:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkCounter();
        break;
      case ABILITY_TYPE.REVENGE_DAMAGE_UP:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkRevenge();
        break;
      case ABILITY_TYPE.BOOST_DAMAGE_UP:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkBoost();
        break;
      case ABILITY_TYPE.JUMP_DAMAGE_UP:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkJump();
        break;
      case ABILITY_TYPE.SHADOWSEALING_DAMAGE_UP:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkShadowSealing();
        break;
      case ABILITY_TYPE.GAUGE_HEAT_COMBO_DAMAGE_UP:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkGaugeHeatCombo();
        break;
      case ABILITY_TYPE.DAMAGE_UP_ARROW_SP:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkArrowSp();
        break;
      case ABILITY_TYPE.BURST_SHOT_DAMAGE_UP:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkBurstShotDmgUp();
        break;
      case ABILITY_TYPE.BURST_FULLBURST_DAMAGE_UP:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkFullBurstDmgUp();
        break;
      case ABILITY_TYPE.DAMAGE_UP_NORMAL:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkNormal();
        break;
      case ABILITY_TYPE.DAMAGE_UP_ATK_NORMAL:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkTypeNormal();
        break;
      case ABILITY_TYPE.DAMAGE_UP_ATTACK_ID:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkAttackId();
        break;
      case ABILITY_TYPE.DAMAGE_UP_ENEMY_CONCUSSION:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkConcussion();
        break;
      case ABILITY_TYPE.BURST_SPEAR_MAXSPIN_ATK_UP:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkBurstSpearSpinMaxDamageUp();
        break;
      case ABILITY_TYPE.BURST_ARROW_BOMB_DAMAGE_UP:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkBurstArrowBombDamageUp();
        break;
      case ABILITY_TYPE.DAMAGE_UP_LABEL:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkDamageUpLabel();
        break;
      case ABILITY_TYPE.ORACLE_PAIR_SWORDS_RUSH_DAMAGE_UP:
        abilityAtkParam = (AbilityAtkBase) new AbilityAtkOraclePairSwordsRush();
        break;
    }
    if (abilityAtkParam != null)
    {
      if (flag)
        abilityAtkParam.init(this.player, info.target, -(int) info.value);
      else
        abilityAtkParam.init(this.player, info.target, (int) info.value);
    }
    return abilityAtkParam;
  }

  public void UpdateConditionsAbility()
  {
    float hpRate = (float) this.chara.hp / (float) this.chara.hpMax;
    for (int index1 = 0; index1 < this.passive.conditionsAbilityList.Count; ++index1)
    {
      BuffParam.ConditionsAbility conditionsAbility = this.passive.conditionsAbilityList[index1];
      for (int index2 = 0; index2 < 3; ++index2)
      {
        if (conditionsAbility.conditionInfos[index2] != null && conditionsAbility.conditionInfos[index2].isValid)
        {
          bool flag = this.IsMatchConditions(conditionsAbility.conditionInfos[index2].abilityInfo, hpRate, conditionsAbility.counterAttackNum, conditionsAbility.cleaveComboNum);
          if (flag == conditionsAbility.conditionInfos[index2].isMatch)
          {
            if (conditionsAbility.conditionInfos[index2].currentStack < conditionsAbility.conditionInfos[index2].GetMaxStack() & flag && !conditionsAbility.conditionInfos[index2].isStacked)
            {
              this.AddConditionsAbility(conditionsAbility.conditionInfos[index2], index1);
              conditionsAbility.conditionInfos[index2].isStacked = true;
              ++conditionsAbility.conditionInfos[index2].currentStack;
            }
          }
          else
          {
            if (!flag)
            {
              this.RemoveConditionsAbility(conditionsAbility.conditionInfos[index2]);
            }
            else
            {
              this.AddConditionsAbility(conditionsAbility.conditionInfos[index2], index1);
              conditionsAbility.conditionInfos[index2].isStacked = true;
              ++conditionsAbility.conditionInfos[index2].currentStack;
            }
            conditionsAbility.conditionInfos[index2].isMatch = flag;
          }
        }
      }
    }
  }

  public void ResetStack()
  {
    for (int index1 = 0; index1 < this.passive.conditionsAbilityList.Count; ++index1)
    {
      BuffParam.ConditionsAbility conditionsAbility = this.passive.conditionsAbilityList[index1];
      for (int index2 = 0; index2 < 3; ++index2)
      {
        if (conditionsAbility.conditionInfos != null && conditionsAbility.conditionInfos.Length != 0 && conditionsAbility.conditionInfos[index2] != null)
          conditionsAbility.conditionInfos[index2].isStacked = false;
      }
    }
  }

  private bool IsMatchConditions(
    AbilityDataTable.AbilityData.AbilityInfo info,
    float hpRate,
    int nowCounterAttackNum,
    int nowCleaveCombo)
  {
    int enablesCount = info.getEnablesCount();
    for (int index = 0; index < enablesCount; ++index)
    {
      switch (info.enables[index].type)
      {
        case ABILITY_ENABLE_TYPE.IF_HP_LOW:
          if ((double) hpRate > (double) info.enables[index].values[0] * 0.0099999997764825821)
            return false;
          break;
        case ABILITY_ENABLE_TYPE.IF_HP_HIGH:
          if ((double) hpRate < (double) info.enables[index].values[0] * 0.0099999997764825821)
            return false;
          break;
        case ABILITY_ENABLE_TYPE.IF_COUNTER_ATTACK:
          int num1 = info.enables[index].values[0];
          if (nowCounterAttackNum < num1)
            return false;
          break;
        case ABILITY_ENABLE_TYPE.COMBINE_ON:
          return this.player.pairSwordsCtrl.IsCombineMode();
        case ABILITY_ENABLE_TYPE.COMBINE_OFF:
          return !this.player.pairSwordsCtrl.IsCombineMode();
        case ABILITY_ENABLE_TYPE.AERIAL:
          return this.player.isAerial;
        case ABILITY_ENABLE_TYPE.IN_SPEAR_BARRIER:
          return this.player.IsInSpearBurstBarrier();
        case ABILITY_ENABLE_TYPE.GUTS:
          return this.player.IsValidBuff(BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS);
        case ABILITY_ENABLE_TYPE.FULL_STOCKED:
          return this.player.spearCtrl.FullStocked;
        case ABILITY_ENABLE_TYPE.IN_ORACLE_SPEAR_SP_LOOP:
          return this.player.spearCtrl.InOracleSpLoop;
        case ABILITY_ENABLE_TYPE.GUARDING:
          return this.player._IsGuard() || this.player.spearCtrl.IsGuard();
        case ABILITY_ENABLE_TYPE.ATTACKING:
          return this.player.actionID == Character.ACTION_ID.ATTACK;
        case ABILITY_ENABLE_TYPE.BOOST:
          return this.player.isBoostMode;
        case ABILITY_ENABLE_TYPE.IN_ORACLE_PAIR_SWORDS_SP_LOOP:
          return this.player.enabledOraclePairSwordsSP;
        case ABILITY_ENABLE_TYPE.IN_ORACLE_PAIR_SWORDS_RUSH_LOOP:
          return this.player.attackID == 43 || this.player.attackID == 42;
        case ABILITY_ENABLE_TYPE.FINISH_A_CLEAVE_COMBO:
          int num2 = info.enables[index].values[0];
          if (nowCleaveCombo <= 0)
            return false;
          break;
      }
    }
    return true;
  }

  private void AddConditionsAbility(
    BuffParam.ConditionsAbility.ConditionsAbilityInfo info,
    int conditionIndex)
  {
    if (info.abilityInfo.type == ABILITY_TYPE.NARROW_ESCAPE)
      this.passive.enableNarrowEscape = true;
    else if (info.abilityInfo.enables[0].type == ABILITY_ENABLE_TYPE.RAZER_FULLSET_ACTIVE)
    {
      if (info.abilityInfo.type == ABILITY_TYPE.TOLERANCE_UP && this.RazerActiveFullArmorSet(this.oldAbilityId))
        this.AddAbilityParam(info.abilityInfo);
    }
    else if (info.abilityInfo.enables[0].type == ABILITY_ENABLE_TYPE.RAZER_WEAPON_ACTIVE || info.abilityInfo.target == "RAZER_WEAPON")
    {
      if ((info.abilityInfo.type == ABILITY_TYPE.DAMAGE_UP_ELEMENT || info.abilityInfo.type == ABILITY_TYPE.GAUGE_INCREASE_UP) && this.RazerActiveWeapon(this.oldAbilityId))
        this.AddAbilityParam(info.abilityInfo);
    }
    else if (info.abilityInfo.enables[0].type == ABILITY_ENABLE_TYPE.RAZER_S2_WEAPON_ACTIVE)
    {
      if (this.RazerS2ActiveWeapon(this.oldAbilityId) && (info.abilityInfo.type == ABILITY_TYPE.DAMAGE_UP_ELEMENT || info.abilityInfo.type == ABILITY_TYPE.ATTACK_SPEED_UP || info.abilityInfo.type == ABILITY_TYPE.DAMAGE_DOWN))
        this.AddAbilityParam(info.abilityInfo);
    }
    else
      this.AddAbilityParam(info.abilityInfo);
    AbilityLunaticTear atkBase1 = info.atkBase as AbilityLunaticTear;
    AbilityBuffWing atkBase2 = info.atkBase as AbilityBuffWing;
    if (atkBase1 != null)
    {
      if (!this.player.isInitialized || this.IsValidBuff(BuffParam.BUFFTYPE.LUNATIC_TEAR))
        return;
      this.chara.OnBuffStart(new BuffParam.BuffData()
      {
        type = BuffParam.BUFFTYPE.LUNATIC_TEAR,
        time = atkBase1.validTime,
        value = 1,
        conditionIndex = conditionIndex
      });
    }
    else if (atkBase2 != null)
    {
      if (!this.player.isInitialized || this.IsValidBuff(BuffParam.BUFFTYPE.WING))
        return;
      this.chara.OnBuffStart(new BuffParam.BuffData()
      {
        type = BuffParam.BUFFTYPE.WING,
        time = atkBase2.validTime,
        value = 1,
        conditionIndex = conditionIndex
      });
    }
    else
      this.passive.abilityAtkList.Add(info.atkBase);
  }

  private void RemoveConditionsAbility(
    BuffParam.ConditionsAbility.ConditionsAbilityInfo abilityInfo)
  {
    if (abilityInfo.abilityInfo.type == ABILITY_TYPE.NARROW_ESCAPE)
      this.passive.enableNarrowEscape = false;
    else
      this.AddAbilityParam(abilityInfo.abilityInfo, true);
    this.passive.abilityAtkList.Remove(abilityInfo.atkBase);
  }

  public bool IncrementCounterConditionAbility()
  {
    bool flag = false;
    foreach (BuffParam.ConditionsAbility conditionsAbility in this.passive.conditionsAbilityList)
    {
      for (int index1 = 0; index1 < 3; ++index1)
      {
        if (conditionsAbility.conditionInfos[index1] != null)
        {
          int enablesCount = conditionsAbility.conditionInfos[index1].abilityInfo.getEnablesCount();
          for (int index2 = 0; index2 < enablesCount; ++index2)
          {
            if (conditionsAbility.conditionInfos[index1].abilityInfo.enables[index2].type == ABILITY_ENABLE_TYPE.IF_COUNTER_ATTACK)
            {
              int num = conditionsAbility.conditionInfos[index1].abilityInfo.enables[index2].values[0];
              if (conditionsAbility.counterAttackNum < num)
              {
                ++conditionsAbility.counterAttackNum;
                flag = true;
              }
            }
          }
        }
      }
    }
    return flag;
  }

  public bool IncrementCleaveComboConditionAbility()
  {
    bool flag = false;
    foreach (BuffParam.ConditionsAbility conditionsAbility in this.passive.conditionsAbilityList)
    {
      for (int index1 = 0; index1 < 3; ++index1)
      {
        if (conditionsAbility.conditionInfos[index1] != null)
        {
          int enablesCount = conditionsAbility.conditionInfos[index1].abilityInfo.getEnablesCount();
          for (int index2 = 0; index2 < enablesCount; ++index2)
          {
            if (conditionsAbility.conditionInfos[index1].abilityInfo.enables[index2].type == ABILITY_ENABLE_TYPE.FINISH_A_CLEAVE_COMBO)
            {
              int num = conditionsAbility.conditionInfos[index1].abilityInfo.enables[index2].values[0];
              if (conditionsAbility.cleaveComboNum < num)
              {
                ++conditionsAbility.cleaveComboNum;
                flag = true;
              }
            }
          }
        }
      }
    }
    return flag;
  }

  public bool IsValidBuffByAbility(BuffParam.BUFFTYPE targetType)
  {
    bool flag = false;
    if (targetType == BuffParam.BUFFTYPE.BREAK_GHOST_FORM)
      flag = this.passive.breakGhostFormCounter > 0;
    return flag;
  }

  public GameObject GetLoopEffect(BuffParam.BuffData data)
  {
    GameObject loopEffect = (GameObject) null;
    for (int index = 0; index < this.loopEffect.Count; ++index)
    {
      if (this.loopEffect[index].linkData.Contains(data))
      {
        loopEffect = this.loopEffect[index].effect;
        break;
      }
    }
    return loopEffect;
  }

  public static bool IsTypeShowDamageOnEnemy(BuffParam.BUFFTYPE type)
  {
    bool flag = false;
    if (type == BuffParam.BUFFTYPE.POISON || type == BuffParam.BUFFTYPE.ELECTRIC_SHOCK || type == BuffParam.BUFFTYPE.BURNING || type == BuffParam.BUFFTYPE.EROSION || type == BuffParam.BUFFTYPE.SOIL_SHOCK || type == BuffParam.BUFFTYPE.ACID || type == BuffParam.BUFFTYPE.CORRUPTION || type == BuffParam.BUFFTYPE.STIGMATA || type == BuffParam.BUFFTYPE.CYCLONIC_THUNDERSTORM)
      flag = true;
    return flag;
  }

  public static bool IsTypeValueBasedOnHP(BuffParam.BUFFTYPE type)
  {
    bool flag = false;
    if (type == BuffParam.BUFFTYPE.POISON || type == BuffParam.BUFFTYPE.DEADLY_POISON || type == BuffParam.BUFFTYPE.BURNING || type == BuffParam.BUFFTYPE.EROSION || type == BuffParam.BUFFTYPE.ACID || type == BuffParam.BUFFTYPE.CORRUPTION || type == BuffParam.BUFFTYPE.STIGMATA || type == BuffParam.BUFFTYPE.CYCLONIC_THUNDERSTORM)
      flag = true;
    return flag;
  }

  public static bool IsHitAbsorbType(BuffParam.BUFFTYPE buffType)
  {
    switch (buffType)
    {
      case BuffParam.BUFFTYPE.HIT_ABSORB_NORMAL:
      case BuffParam.BUFFTYPE.HIT_ABSORB_FIRE:
      case BuffParam.BUFFTYPE.HIT_ABSORB_WATER:
      case BuffParam.BUFFTYPE.HIT_ABSORB_THUNDER:
      case BuffParam.BUFFTYPE.HIT_ABSORB_SOIL:
      case BuffParam.BUFFTYPE.HIT_ABSORB_LIGHT:
      case BuffParam.BUFFTYPE.HIT_ABSORB_DARK:
      case BuffParam.BUFFTYPE.HIT_ABSORB_ALL:
        return true;
      default:
        return false;
    }
  }

  public bool IsValidShieldBuff(int skillIndex)
  {
    BuffParam.BuffData buffData = this.data[60];
    return !Object.op_Equality((Object) this.player, (Object) null) && buffData.enable && buffData.fromObjectID == this.player.createInfo.charaInfo.userId && buffData.fromEquipIndex == this.player.weaponIndex && buffData.fromSkillIndex == skillIndex;
  }

  public bool IsValidInvincibleBuff()
  {
    bool flag = false;
    int index = 0;
    for (int length = BuffParam.INVINCIBLE_BUFFTYPES.Length; index < length; ++index)
    {
      if (this.IsValidBuff(this.data[(int) BuffParam.INVINCIBLE_BUFFTYPES[index]].type))
      {
        flag = true;
        break;
      }
    }
    return flag;
  }

  public int GetShieldFromSkillIndex()
  {
    return Object.op_Equality((Object) this.player, (Object) null) ? -1 : this.data[60].fromSkillIndex;
  }

  public string GetFromText()
  {
    BuffParam.BuffData buffData = this.data[60];
    if (Object.op_Equality((Object) this.player, (Object) null))
      return "null";
    if (!buffData.enable)
      return "false";
    return $"{(object) buffData.fromObjectID},{(object) buffData.fromEquipIndex},{(object) buffData.fromSkillIndex}";
  }

  public void ClearFieldBuff() => this.fieldData.Clear();

  public void StartFieldBuff(uint fieldBuffId, BuffTable.BuffData tableData)
  {
    if (this.fieldData.ContainsKey(tableData.type))
      return;
    BuffParam.BuffData buffData = new BuffParam.BuffData();
    buffData.type = tableData.type;
    buffData.valueType = tableData.valueType;
    buffData.value = tableData.value;
    buffData.interval = tableData.interval;
    buffData.time = -1f;
    buffData.endless = new bool?(true);
    buffData.enable = true;
    buffData.fromFieldBuffID = fieldBuffId;
    this.fieldData.Add(buffData.type, buffData);
  }

  public int GetFieldBuffValue(BuffParam.BUFFTYPE type)
  {
    if (!this.fieldData.ContainsKey(type))
      return 0;
    BuffParam.BuffData buffData = this.fieldData[type];
    float num = 1f - this.GetPassiveFieldBuffResist(buffData.fromFieldBuffID);
    if ((double) num < 0.0)
      num = 0.0f;
    return (int) ((double) buffData.value * (double) num);
  }

  private void SetPassiveFieldBuffResist(string idStr, float value)
  {
    uint result = 0;
    if (!uint.TryParse(idStr, out result))
      return;
    if (this.passive.fieldBuffResist.ContainsKey(result))
      this.passive.fieldBuffResist[result] += value;
    else
      this.passive.fieldBuffResist.Add(result, value);
  }

  private void SetPassiveFieldBuffResistByType(string idStr, float value)
  {
    uint result = 0;
    if (!uint.TryParse(idStr, out result) || !Singleton<FieldBuffTable>.IsValid())
      return;
    FieldBuffTable.FieldBuffData data1 = Singleton<FieldBuffTable>.I.GetData(result);
    if (data1 == null || data1.buffTableIds.IsNullOrEmpty<uint>() || !Singleton<BuffTable>.IsValid())
      return;
    int index = 0;
    for (int count = data1.buffTableIds.Count; index < count; ++index)
    {
      BuffTable.BuffData data2 = Singleton<BuffTable>.I.GetData(data1.buffTableIds[index]);
      if (data2 != null)
      {
        if (this.passive.fieldBuffResistByType.ContainsKey(data2.type))
          this.passive.fieldBuffResistByType[data2.type] += value;
        else
          this.passive.fieldBuffResistByType.Add(data2.type, value);
      }
    }
  }

  public float GetRatePassiveFieldBuffResist(BuffParam.BUFFTYPE type)
  {
    return !this.fieldData.ContainsKey(type) ? 0.0f : this.GetPassiveFieldBuffResist(this.fieldData[type].fromFieldBuffID);
  }

  private float GetPassiveFieldBuffResist(uint id)
  {
    return this.passive.fieldBuffResist.ContainsKey(id) ? this.passive.fieldBuffResist[id] : 0.0f;
  }

  public float GetPassiveFieldBuffResist(BuffParam.BUFFTYPE type)
  {
    return this.passive.fieldBuffResistByType.ContainsKey(type) ? this.passive.fieldBuffResistByType[type] : 0.0f;
  }

  public bool IsValidInvincibleCountBuff()
  {
    for (int index = 0; index < BuffParam.INVINCIBLECOUNT_BUFFTYPES.Length; ++index)
    {
      if (this.IsValidBuff(BuffParam.INVINCIBLECOUNT_BUFFTYPES[index]))
        return true;
    }
    return false;
  }

  public enum BUFFTYPE
  {
    NONE = -1, // 0xFFFFFFFF
    ATTACK_NORMAL = 0,
    ATTACK_FIRE = 1,
    ATTACK_WATER = 2,
    ATTACK_THUNDER = 3,
    ATTACK_SOIL = 4,
    ATTACK_LIGHT = 5,
    ATTACK_DARK = 6,
    ATTACK_ALLELEMENT = 7,
    ATTACK_PARALYZE = 8,
    ATTACK_POISON = 9,
    DEFENCE_NORMAL = 10, // 0x0000000A
    DEFENCE_FIRE = 11, // 0x0000000B
    DEFENCE_WATER = 12, // 0x0000000C
    DEFENCE_THUNDER = 13, // 0x0000000D
    DEFENCE_SOIL = 14, // 0x0000000E
    DEFENCE_LIGHT = 15, // 0x0000000F
    DEFENCE_DARK = 16, // 0x00000010
    DEFENCE_ALLELEMENT = 17, // 0x00000011
    MOVE_SPEED_UP = 18, // 0x00000012
    MOVE_SPEED_DOWN = 19, // 0x00000013
    ATTACK_SPEED_UP = 20, // 0x00000014
    INVINCIBLECOUNT = 21, // 0x00000015
    REGENERATE = 22, // 0x00000016
    HP_HEAL_SPEEDUP = 23, // 0x00000017
    SKILL_ABSORBUP = 24, // 0x00000018
    SKILL_HEAL_SPEEDUP = 25, // 0x00000019
    POISON = 26, // 0x0000001A
    BURNING = 27, // 0x0000001B
    DEADLY_POISON = 28, // 0x0000001C
    DAMAGE_DOWN = 29, // 0x0000001D
    LUNATIC_TEAR = 30, // 0x0000001E
    WING = 31, // 0x0000001F
    GHOST_FORM = 32, // 0x00000020
    BREAK_GHOST_FORM = 33, // 0x00000021
    ELECTRIC_SHOCK = 34, // 0x00000022
    INK_SPLASH = 35, // 0x00000023
    ATKUP_RATE_NORMAL = 36, // 0x00000024
    ATKUP_RATE_FIRE = 37, // 0x00000025
    ATKUP_RATE_WATER = 38, // 0x00000026
    ATKUP_RATE_THUNDER = 39, // 0x00000027
    ATKUP_RATE_SOIL = 40, // 0x00000028
    ATKUP_RATE_LIGHT = 41, // 0x00000029
    ATKUP_RATE_DARK = 42, // 0x0000002A
    ATKUP_RATE_ALLELEMENT = 43, // 0x0000002B
    DEFUP_RATE_NORMAL = 44, // 0x0000002C
    DEFUP_RATE_FIRE = 45, // 0x0000002D
    DEFUP_RATE_WATER = 46, // 0x0000002E
    DEFUP_RATE_THUNDER = 47, // 0x0000002F
    DEFUP_RATE_SOIL = 48, // 0x00000030
    DEFUP_RATE_LIGHT = 49, // 0x00000031
    DEFUP_RATE_DARK = 50, // 0x00000032
    DEFUP_RATE_ALLELEMENT = 51, // 0x00000033
    DAMAGE_UP = 52, // 0x00000034
    POISON_DAMAGE_DOWN = 53, // 0x00000035
    POISON_GUARD = 54, // 0x00000036
    BURN_DAMAGE_DOWN = 55, // 0x00000037
    BURN_GUARD = 56, // 0x00000038
    SUPER_ARMOR = 57, // 0x00000039
    DEFDOWN_RATE_NORMAL = 58, // 0x0000003A
    DEFDOWN_RATE_ALLELEMENT = 59, // 0x0000003B
    SHIELD = 60, // 0x0000003C
    SHIELD_SUPER_ARMOR = 61, // 0x0000003D
    SKILL_CHARGE = 62, // 0x0000003E
    SKILL_CHARGE_RATE = 63, // 0x0000003F
    HP_UP = 64, // 0x00000040
    HP_DOWN = 65, // 0x00000041
    HPUP_RATE = 66, // 0x00000042
    HPDOWN_RATE = 67, // 0x00000043
    ATTACK_DOWN_NORMAL = 68, // 0x00000044
    ATTACK_DOWN_FIRE = 69, // 0x00000045
    ATTACK_DOWN_WATER = 70, // 0x00000046
    ATTACK_DOWN_THUNDER = 71, // 0x00000047
    ATTACK_DOWN_SOIL = 72, // 0x00000048
    ATTACK_DOWN_LIGHT = 73, // 0x00000049
    ATTACK_DOWN_DARK = 74, // 0x0000004A
    ATTACK_DOWN_ALLELEMENT = 75, // 0x0000004B
    ATKDOWN_RATE_NORMAL = 76, // 0x0000004C
    ATKDOWN_RATE_FIRE = 77, // 0x0000004D
    ATKDOWN_RATE_WATER = 78, // 0x0000004E
    ATKDOWN_RATE_THUNDER = 79, // 0x0000004F
    ATKDOWN_RATE_SOIL = 80, // 0x00000050
    ATKDOWN_RATE_LIGHT = 81, // 0x00000051
    ATKDOWN_RATE_DARK = 82, // 0x00000052
    ATKDOWN_RATE_ALLELEMENT = 83, // 0x00000053
    DEFENCE_DOWN_NORMAL = 84, // 0x00000054
    DEFENCE_DOWN_FIRE = 85, // 0x00000055
    DEFENCE_DOWN_WATER = 86, // 0x00000056
    DEFENCE_DOWN_THUNDER = 87, // 0x00000057
    DEFENCE_DOWN_SOIL = 88, // 0x00000058
    DEFENCE_DOWN_LIGHT = 89, // 0x00000059
    DEFENCE_DOWN_DARK = 90, // 0x0000005A
    DEFENCE_DOWN_ALLELEMENT = 91, // 0x0000005B
    DEFDOWN_RATE_FIRE = 92, // 0x0000005C
    DEFDOWN_RATE_WATER = 93, // 0x0000005D
    DEFDOWN_RATE_THUNDER = 94, // 0x0000005E
    DEFDOWN_RATE_SOIL = 95, // 0x0000005F
    DEFDOWN_RATE_LIGHT = 96, // 0x00000060
    DEFDOWN_RATE_DARK = 97, // 0x00000061
    TOLERANCE_FIRE = 98, // 0x00000062
    TOLERANCE_WATER = 99, // 0x00000063
    TOLERANCE_THUNDER = 100, // 0x00000064
    TOLERANCE_SOIL = 101, // 0x00000065
    TOLERANCE_LIGHT = 102, // 0x00000066
    TOLERANCE_DARK = 103, // 0x00000067
    TOLERANCE_ALLELEMENT = 104, // 0x00000068
    TOLERANCE_DOWN_FIRE = 105, // 0x00000069
    TOLERANCE_DOWN_WATER = 106, // 0x0000006A
    TOLERANCE_DOWN_THUNDER = 107, // 0x0000006B
    TOLERANCE_DOWN_SOIL = 108, // 0x0000006C
    TOLERANCE_DOWN_LIGHT = 109, // 0x0000006D
    TOLERANCE_DOWN_DARK = 110, // 0x0000006E
    TOLERANCE_DOWN_ALLELEMENT = 111, // 0x0000006F
    TOLUP_RATE_FIRE = 112, // 0x00000070
    TOLUP_RATE_WATER = 113, // 0x00000071
    TOLUP_RATE_THUNDER = 114, // 0x00000072
    TOLUP_RATE_SOIL = 115, // 0x00000073
    TOLUP_RATE_LIGHT = 116, // 0x00000074
    TOLUP_RATE_DARK = 117, // 0x00000075
    TOLUP_RATE_ALLELEMENT = 118, // 0x00000076
    TOLDOWN_RATE_FIRE = 119, // 0x00000077
    TOLDOWN_RATE_WATER = 120, // 0x00000078
    TOLDOWN_RATE_THUNDER = 121, // 0x00000079
    TOLDOWN_RATE_SOIL = 122, // 0x0000007A
    TOLDOWN_RATE_LIGHT = 123, // 0x0000007B
    TOLDOWN_RATE_DARK = 124, // 0x0000007C
    TOLDOWN_RATE_ALLELEMENT = 125, // 0x0000007D
    SLIDE = 126, // 0x0000007E
    JUSTGUARD_EXTEND_RATE = 127, // 0x0000007F
    PARALYZE_GUARD = 128, // 0x00000080
    SILENCE = 129, // 0x00000081
    SKILL_CHARGE_FIRE = 130, // 0x00000082
    SKILL_CHARGE_WATER = 131, // 0x00000083
    SKILL_CHARGE_THUNDER = 132, // 0x00000084
    SKILL_CHARGE_SOIL = 133, // 0x00000085
    SKILL_CHARGE_LIGHT = 134, // 0x00000086
    SKILL_CHARGE_DARK = 135, // 0x00000087
    REGENERATE_PROPORTION = 136, // 0x00000088
    SKILL_ABSORBUP_ONLY_ATK_FIRE = 137, // 0x00000089
    SKILL_ABSORBUP_ONLY_ATK_WATER = 138, // 0x0000008A
    SKILL_ABSORBUP_ONLY_ATK_THUNDER = 139, // 0x0000008B
    SKILL_ABSORBUP_ONLY_ATK_SOIL = 140, // 0x0000008C
    SKILL_ABSORBUP_ONLY_ATK_LIGHT = 141, // 0x0000008D
    SKILL_ABSORBUP_ONLY_ATK_DARK = 142, // 0x0000008E
    SILENCE_GUARD = 143, // 0x0000008F
    ABSORB_NORMAL = 144, // 0x00000090
    ABSORB_FIRE = 145, // 0x00000091
    ABSORB_WATER = 146, // 0x00000092
    ABSORB_THUNDER = 147, // 0x00000093
    ABSORB_SOIL = 148, // 0x00000094
    ABSORB_LIGHT = 149, // 0x00000095
    ABSORB_DARK = 150, // 0x00000096
    ABSORB_ALL_ELEMENT = 151, // 0x00000097
    HIT_ABSORB_NORMAL = 152, // 0x00000098
    HIT_ABSORB_FIRE = 153, // 0x00000099
    HIT_ABSORB_WATER = 154, // 0x0000009A
    HIT_ABSORB_THUNDER = 155, // 0x0000009B
    HIT_ABSORB_SOIL = 156, // 0x0000009C
    HIT_ABSORB_LIGHT = 157, // 0x0000009D
    HIT_ABSORB_DARK = 158, // 0x0000009E
    HIT_ABSORB_ALL = 159, // 0x0000009F
    MAD_MODE = 160, // 0x000000A0
    ATTACK_SPEED_DOWN = 161, // 0x000000A1
    AUTO_REVIVE = 162, // 0x000000A2
    WARP_BY_AVOID = 163, // 0x000000A3
    INVINCIBLE_NORMAL = 164, // 0x000000A4
    INVINCIBLE_FIRE = 165, // 0x000000A5
    INVINCIBLE_WATER = 166, // 0x000000A6
    INVINCIBLE_THUNDER = 167, // 0x000000A7
    INVINCIBLE_SOIL = 168, // 0x000000A8
    INVINCIBLE_ALL = 169, // 0x000000A9
    DAMAGE_UP_NORMAL = 170, // 0x000000AA
    DAMAGE_UP_FROM_AVOID = 171, // 0x000000AB
    HEAT_GAUGE_INCREASE_UP = 172, // 0x000000AC
    HEAT_GAUGE_INCREASE_DOWN = 173, // 0x000000AD
    SOUL_GAUGE_INCREASE_UP = 174, // 0x000000AE
    SOUL_GAUGE_INCREASE_DOWN = 175, // 0x000000AF
    SKILL_CHARGE_WHEN_DAMAGED = 176, // 0x000000B0
    INVINCIBLE_LIGHT = 177, // 0x000000B1
    INVINCIBLE_DARK = 178, // 0x000000B2
    CANT_HEAL_HP = 179, // 0x000000B3
    BLIND = 180, // 0x000000B4
    ATTACK_FREEZE = 181, // 0x000000B5
    INVINCIBLE_BADSTATUS = 182, // 0x000000B6
    SLIDE_ICE = 183, // 0x000000B7
    BAD_STATUS_DOWN_RATE_UP = 184, // 0x000000B8
    LIGHT_RING = 185, // 0x000000B9
    DISTANCE_UP_IAI = 186, // 0x000000BA
    BOOST_DAMAGE_UP = 187, // 0x000000BB
    BOOST_DAMAGE_DOWN = 188, // 0x000000BC
    BOOST_ATTACK_SPEED_UP = 189, // 0x000000BD
    BOOST_MOVE_SPEED_UP = 190, // 0x000000BE
    BOOST_AVOID_UP = 191, // 0x000000BF
    HEAL_UP = 192, // 0x000000C0
    SKILL_CHARGE_ABOVE = 193, // 0x000000C1
    SKILL_CHARGE_UNDER = 194, // 0x000000C2
    SUBSTITUTE = 195, // 0x000000C3
    EROSION = 196, // 0x000000C4
    STONE = 197, // 0x000000C5
    SHIELD_REFLECT = 198, // 0x000000C6
    SHIELD_REFLECT_DAMAGE_UP = 199, // 0x000000C7
    SOIL_SHOCK = 200, // 0x000000C8
    BLEEDING = 201, // 0x000000C9
    BAD_STATUS_CONCUSSION_RATE_UP = 202, // 0x000000CA
    ATKUP_RATE_ALL = 203, // 0x000000CB
    INVINCIBLE_ALL_ELEMENT = 204, // 0x000000CC
    ACID = 205, // 0x000000CD
    INVINCIBLE_BUFF_CANCELLATION = 206, // 0x000000CE
    INVINCIBLE_BUFF_CANCELLATION_EXPAND = 207, // 0x000000CF
    BURST_GAUGE_INCREASE_UP = 208, // 0x000000D0
    BURST_GAUGE_INCREASE_DOWN = 209, // 0x000000D1
    ORACLE_GAUGE_INCREASE_UP = 210, // 0x000000D2
    ORACLE_GAUGE_INCREASE_DOWN = 211, // 0x000000D3
    ORACLE_OHS_PROTECTION = 212, // 0x000000D4
    DAMAGE_MOTION_STOP = 213, // 0x000000D5
    TO_ENEMY_DEBUFF_VALUE_UP = 214, // 0x000000D6
    CORRUPTION = 215, // 0x000000D7
    ORACLE_SPEAR_GUTS = 216, // 0x000000D8
    STIGMATA = 217, // 0x000000D9
    SHIELD_INVINCIBLE_BADSTATUS = 218, // 0x000000DA
    CYCLONIC_THUNDERSTORM = 219, // 0x000000DB
    AUTO_REVIVE_SKILL_CHARGE = 220, // 0x000000DC
    MAX = 221, // 0x000000DD
    HIT_PARALYZE = 222, // 0x000000DE
    HIT_POISON = 223, // 0x000000DF
  }

  public enum TOLERANCETYPE
  {
    PARALYZE,
    POISON,
    BURNING,
    SPEED_DOWN,
    STUMBLE,
    SHAKE,
    SOUNDWAVE,
    SKILL,
    FREEZE,
    SLIDE,
    SILENCE,
    ATTACK_SPEED_DOWN,
    CANT_HEAL_HP,
    BLIND,
    STONE,
    BLEEDING,
    ACID,
    CHARM,
    CORRUPTION,
    MAX,
  }

  public enum BAD_STATUS_UP
  {
    PARALYZE,
    POISON,
    DOWN,
    FREEZE,
    CONCUSSION,
    MAX,
  }

  public enum VALUE_TYPE
  {
    NONE,
    RATE,
    CONSTANT,
  }

  public class ConditionsAbility
  {
    public BuffParam.ConditionsAbility.ConditionsAbilityInfo[] conditionInfos = new BuffParam.ConditionsAbility.ConditionsAbilityInfo[3];
    public int counterAttackNum;
    public int cleaveComboNum;

    public class ConditionsAbilityInfo
    {
      public AbilityDataTable.AbilityData.AbilityInfo abilityInfo = new AbilityDataTable.AbilityData.AbilityInfo();
      public AbilityAtkBase atkBase = new AbilityAtkBase();
      public bool isValid = true;
      public bool isMatch;
      public int currentStack;
      public bool isStacked;
      public List<int> executedStackOrder = new List<int>();

      public int GetMaxStack()
      {
        if (this.abilityInfo.enables != null && this.abilityInfo.enables.Count > 0)
        {
          for (int index = 0; index < this.abilityInfo.enables.Count; ++index)
          {
            if (this.abilityInfo.enables[index].type == ABILITY_ENABLE_TYPE.FINISH_A_CLEAVE_COMBO)
              return 3;
          }
        }
        return 1;
      }
    }
  }

  public class PassiveBuff
  {
    public int hp;
    public List<int> atkList = new List<int>();
    public List<int> defList = new List<int>();
    public List<int> tolList = new List<int>();
    [FormerlySerializedAs("atkAllElement")]
    private XorFloat _atkAllElement = (XorFloat) 0.0f;
    [FormerlySerializedAs("moveSpeedUp")]
    private XorFloat _moveSpeedUp = (XorFloat) 0.0f;
    [FormerlySerializedAs("boostMoveSpeedUp")]
    private XorFloat _boostMoveSpeedUp = (XorFloat) 0.0f;
    [FormerlySerializedAs("attackSpeedUp")]
    private XorFloat _attackSpeedUp = (XorFloat) 0.0f;
    [FormerlySerializedAs("boostAttackSpeedUp")]
    private XorFloat _boostAttackSpeedUp = (XorFloat) 0.0f;
    [FormerlySerializedAs("hpHealSpeedUp")]
    private XorFloat _hpHealSpeedUp = (XorFloat) 0.0f;
    [FormerlySerializedAs("guardUp")]
    private XorFloat _guardUp = (XorFloat) 0.0f;
    [FormerlySerializedAs("skillAbsorbUp")]
    private XorFloat _skillAbsorbUp = (XorFloat) 0.0f;
    [FormerlySerializedAs("skillHealSpeedUp")]
    private XorFloat _skillHealSpeedUp = (XorFloat) 0.0f;
    [FormerlySerializedAs("avoidUp")]
    private XorFloat _avoidUp = (XorFloat) 0.0f;
    [FormerlySerializedAs("boostAvoidUp")]
    private XorFloat _boostAvoidUp = (XorFloat) 0.0f;
    [FormerlySerializedAs("healUp")]
    private XorFloat _healUP = (XorFloat) 0.0f;
    private XorFloat _healUpDependsWeapon = (XorFloat) 0.0f;
    [FormerlySerializedAs("bleedUp")]
    private XorFloat _bleedUp = (XorFloat) 0.0f;
    [FormerlySerializedAs("chargeSwordsTimeRate")]
    private XorFloat _chargeSwordsTimeRate = (XorFloat) 0.0f;
    [FormerlySerializedAs("chargeArrowTimeRate")]
    private XorFloat _chargeArrowTimeRate = (XorFloat) 0.0f;
    [FormerlySerializedAs("chargeHeatArrowTimeRate")]
    private XorFloat _chargeHeatArrowTimeRate = (XorFloat) 0.0f;
    [FormerlySerializedAs("chargePairSwordsTimeRate")]
    private XorFloat _chargePairSwordsTimeRate = (XorFloat) 0.0f;
    [FormerlySerializedAs("spearRushDistanceRate")]
    private XorFloat _spearRushDistanceRate = (XorFloat) 0.0f;
    [FormerlySerializedAs("chargeSpearTimeRate")]
    private XorFloat _chargeSpearTimeRate = (XorFloat) 0.0f;
    [FormerlySerializedAs("skillTimeRate")]
    private XorFloat _skillTimeRate = (XorFloat) 0.0f;
    [FormerlySerializedAs("damageDown")]
    private XorFloat _damageDown = (XorFloat) 0.0f;
    [FormerlySerializedAs("boostDamageDown")]
    private XorFloat _boostDamageDown = (XorFloat) 0.0f;
    [FormerlySerializedAs("narrowEscape")]
    private XorInt _narrowEscape = (XorInt) 0;
    public bool enableNarrowEscape;
    [FormerlySerializedAs("_poisonDamageDownRate")]
    private XorFloat _poisonDamageDownRate = (XorFloat) 0.0f;
    [FormerlySerializedAs("_poisonGuardWeight")]
    private XorInt _poisonGuardWeight = (XorInt) 0;
    [FormerlySerializedAs("burnDamageDownRate")]
    private XorFloat _burnDamageDownRate = (XorFloat) 0.0f;
    [FormerlySerializedAs("burnGuardWeight")]
    private XorInt _burnGuardWeight = (XorInt) 0;
    [FormerlySerializedAs("paralyzeGuardWeight")]
    private XorInt _paralyzeGuardWeight = (XorInt) 0;
    [FormerlySerializedAs("silenceGuardWeight")]
    private XorInt _silenceGuardWeight = (XorInt) 0;
    [FormerlySerializedAs("justGuardExtendRate")]
    private XorFloat _justGuardExtendRate = (XorFloat) 0.0f;
    [FormerlySerializedAs("gaugeIncreaseRate")]
    private XorFloat _gaugeIncreaseRate = (XorFloat) 0.0f;
    [FormerlySerializedAs("gaugeDecreaseRate")]
    private XorFloat _gaugeDecreaseRate = (XorFloat) 0.0f;
    [FormerlySerializedAs("subGaugeIncreaseRate")]
    private XorFloat _subGaugeIncreaseRate = (XorFloat) 0.0f;
    [FormerlySerializedAs("heatGaugeIncreaseRate")]
    private XorFloat _heatGaugeIncreaseRate = (XorFloat) 0.0f;
    [FormerlySerializedAs("soulGaugeIncreaseRate")]
    private XorFloat _soulGaugeIncreaseRate = (XorFloat) 0.0f;
    [FormerlySerializedAs("burstGaugeIncreaseRate")]
    private XorFloat _burstGaugeIncreaseRate = (XorFloat) 0.0f;
    [FormerlySerializedAs("oracleGaugeIncreaseRate")]
    private XorFloat _oracleGaugeIncreaseRate = (XorFloat) 0.0f;
    [FormerlySerializedAs("soulChargeTimeRate")]
    private XorFloat _soulChargeTimeRate = (XorFloat) 0.0f;
    [FormerlySerializedAs("shadowSealingExtend")]
    private XorFloat _shadowSealingExtend = (XorFloat) 0.0f;
    [FormerlySerializedAs("shadowSealingExtendArrow")]
    private XorFloat _shadowSealingExtendArrow = (XorFloat) 0.0f;
    [FormerlySerializedAs("distanceRateFromAvoid")]
    private XorFloat _distanceRateFromAvoid = (XorFloat) 0.0f;
    [FormerlySerializedAs("distanceRateIai")]
    private XorFloat _distanceRateIai = (XorFloat) 0.0f;
    [FormerlySerializedAs("lockOnTimeRate")]
    private XorFloat _lockOnTimeRate = (XorFloat) 0.0f;
    [FormerlySerializedAs("ReloadSpeed")]
    private XorFloat _burstReloadActionSpeed = (XorFloat) 0.0f;
    [FormerlySerializedAs("AdditionalMaxBulletCount")]
    private XorInt _additionalMaxBulletCnt = (XorInt) 0;
    [FormerlySerializedAs("dragonArmorDamageRate")]
    private XorFloat _dragonArmorDamageRate = (XorFloat) 0.0f;
    [FormerlySerializedAs("addSoulArrowLockCount")]
    private XorInt _addSoulArrowLockCount = (XorInt) 0;
    private XorFloat _arrowRainNumRate = (XorFloat) 0.0f;
    private XorFloat _burstSpearSpinTimeRate = (XorFloat) 0.0f;
    private XorFloat _spearRushSpeedRate = (XorFloat) 0.0f;
    private XorFloat _oracleThsHorizontalSpeedUp = (XorFloat) 0.0f;
    private XorFloat _oracleSpinSmashTimeRate = (XorFloat) 0.0f;
    private XorFloat _oracleDiveSmashTimeRate = (XorFloat) 0.0f;
    private XorFloat _oracleWheelSmashTimeRate = (XorFloat) 0.0f;
    private XorInt _oracleOhsProtectionDoubleProbability = (XorInt) 0;
    private XorFloat _concussionExtend = (XorFloat) 0.0f;
    private XorFloat _teleportUp = (XorFloat) 0.0f;
    private XorInt _stockAddInit = (XorInt) 0;
    private XorFloat _gutsTimeRateUp = (XorFloat) 0.0f;
    private XorFloat _freeStockProbability = (XorFloat) 0.0f;
    public List<AbilityAtkBase> abilityAtkList = new List<AbilityAtkBase>(64 /*0x40*/);
    public int[] tolerance = new int[19];
    public float[] badStatusUp = new float[5];
    public float[] badStatusRateUp = new float[5];
    public float hpUpRate;
    public float hpDownRate;
    public AtkAttribute atkUpRate = new AtkAttribute();
    public AtkAttribute atkDownRate = new AtkAttribute();
    public AtkAttribute defUpRate = new AtkAttribute();
    public AtkAttribute defDownRate = new AtkAttribute();
    public AtkAttribute tolUpRate = new AtkAttribute();
    public AtkAttribute tolDownRate = new AtkAttribute();
    public AtkAttribute skillAbsorbUp_OnlyAttackAndElement = new AtkAttribute();
    public bool firstInitialized;
    public List<BuffParam.ConditionsAbility> conditionsAbilityList = new List<BuffParam.ConditionsAbility>();
    public int breakGhostFormCounter;
    public Dictionary<uint, float> fieldBuffResist = new Dictionary<uint, float>(221);
    public Dictionary<BuffParam.BUFFTYPE, float> fieldBuffResistByType = new Dictionary<BuffParam.BUFFTYPE, float>(221);

    public float atkAllElement
    {
      get => (float) this._atkAllElement;
      set => this._atkAllElement = (XorFloat) value;
    }

    public float moveSpeedUp
    {
      get => (float) this._moveSpeedUp;
      set => this._moveSpeedUp = (XorFloat) value;
    }

    public float boostMoveSpeedUp
    {
      get => (float) this._boostMoveSpeedUp;
      set => this._boostMoveSpeedUp = (XorFloat) value;
    }

    public float attackSpeedUp
    {
      get => (float) this._attackSpeedUp;
      set => this._attackSpeedUp = (XorFloat) value;
    }

    public float boostAttackSpeedUp
    {
      get => (float) this._boostAttackSpeedUp;
      set => this._boostAttackSpeedUp = (XorFloat) value;
    }

    public float hpHealSpeedUp
    {
      get => (float) this._hpHealSpeedUp;
      set => this._hpHealSpeedUp = (XorFloat) value;
    }

    public float guardUp
    {
      get => (float) this._guardUp;
      set => this._guardUp = (XorFloat) value;
    }

    public float skillAbsorbUp
    {
      get => (float) this._skillAbsorbUp;
      set => this._skillAbsorbUp = (XorFloat) value;
    }

    public float skillHealSpeedUp
    {
      get => (float) this._skillHealSpeedUp;
      set => this._skillHealSpeedUp = (XorFloat) value;
    }

    public float avoidUp
    {
      get => (float) this._avoidUp;
      set => this._avoidUp = (XorFloat) value;
    }

    public float boostAvoidUp
    {
      get => (float) this._boostAvoidUp;
      set => this._boostAvoidUp = (XorFloat) value;
    }

    public float healUP
    {
      get => (float) this._healUP;
      set => this._healUP = (XorFloat) value;
    }

    public float healUpDependsWeapon
    {
      get => (float) this._healUpDependsWeapon;
      set => this._healUpDependsWeapon = (XorFloat) value;
    }

    public float bleedUp
    {
      get => (float) this._bleedUp;
      set => this._bleedUp = (XorFloat) value;
    }

    public float chargeSwordsTimeRate
    {
      get => (float) this._chargeSwordsTimeRate;
      set => this._chargeSwordsTimeRate = (XorFloat) value;
    }

    public float chargeArrowTimeRate
    {
      get => (float) this._chargeArrowTimeRate;
      set => this._chargeArrowTimeRate = (XorFloat) value;
    }

    public float chargeHeatArrowTimeRate
    {
      get => (float) this._chargeHeatArrowTimeRate;
      set => this._chargeHeatArrowTimeRate = (XorFloat) value;
    }

    public float chargePairSwordsTimeRate
    {
      get => (float) this._chargePairSwordsTimeRate;
      set => this._chargePairSwordsTimeRate = (XorFloat) value;
    }

    public float spearRushDistanceRate
    {
      get => (float) this._spearRushDistanceRate;
      set => this._spearRushDistanceRate = (XorFloat) value;
    }

    public float chargeSpearTimeRate
    {
      get => (float) this._chargeSpearTimeRate;
      set => this._chargeSpearTimeRate = (XorFloat) value;
    }

    public float skillTimeRate
    {
      get => (float) this._skillTimeRate;
      set => this._skillTimeRate = (XorFloat) value;
    }

    public float damageDown
    {
      get => (float) this._damageDown;
      set => this._damageDown = (XorFloat) value;
    }

    public float boostDamageDown
    {
      get => (float) this._boostDamageDown;
      set => this._boostDamageDown = (XorFloat) value;
    }

    public int narrowEscape
    {
      get => (int) this._narrowEscape;
      set => this._narrowEscape = (XorInt) value;
    }

    public float poisonDamageDownRate
    {
      get => (float) this._poisonDamageDownRate;
      set => this._poisonDamageDownRate = (XorFloat) value;
    }

    public int poisonGuardWeight
    {
      get => (int) this._poisonGuardWeight;
      set => this._poisonGuardWeight = (XorInt) value;
    }

    public float burnDamageDownRate
    {
      get => (float) this._burnDamageDownRate;
      set => this._burnDamageDownRate = (XorFloat) value;
    }

    public int burnGuardWeight
    {
      get => (int) this._burnGuardWeight;
      set => this._burnGuardWeight = (XorInt) value;
    }

    public int paralyzeGuardWeight
    {
      get => (int) this._paralyzeGuardWeight;
      set => this._paralyzeGuardWeight = (XorInt) value;
    }

    public int silenceGuardWeight
    {
      get => (int) this._silenceGuardWeight;
      set => this._silenceGuardWeight = (XorInt) value;
    }

    public float justGuardExtendRate
    {
      get => (float) this._justGuardExtendRate;
      set => this._justGuardExtendRate = (XorFloat) value;
    }

    public float gaugeIncreaseRate
    {
      get => (float) this._gaugeIncreaseRate;
      set => this._gaugeIncreaseRate = (XorFloat) value;
    }

    public float gaugeDecreaseRate
    {
      get => (float) this._gaugeDecreaseRate;
      set => this._gaugeDecreaseRate = (XorFloat) value;
    }

    public float subGaugeIncreaseRate
    {
      get => (float) this._subGaugeIncreaseRate;
      set => this._subGaugeIncreaseRate = (XorFloat) value;
    }

    public float heatGaugeIncreaseRate
    {
      get => (float) this._heatGaugeIncreaseRate;
      set => this._heatGaugeIncreaseRate = (XorFloat) value;
    }

    public float soulGaugeIncreaseRate
    {
      get => (float) this._soulGaugeIncreaseRate;
      set => this._soulGaugeIncreaseRate = (XorFloat) value;
    }

    public float burstGaugeIncreaseRate
    {
      get => (float) this._burstGaugeIncreaseRate;
      set => this._burstGaugeIncreaseRate = (XorFloat) value;
    }

    public float oracleGaugeIncreaseRate
    {
      get => (float) this._oracleGaugeIncreaseRate;
      set => this._oracleGaugeIncreaseRate = (XorFloat) value;
    }

    public float soulChargeTimeRate
    {
      get => (float) this._soulChargeTimeRate;
      set => this._soulChargeTimeRate = (XorFloat) value;
    }

    public float shadowSealingExtend
    {
      get => (float) this._shadowSealingExtend;
      set => this._shadowSealingExtend = (XorFloat) value;
    }

    public float shadowSealingExtendArrow
    {
      get => (float) this._shadowSealingExtendArrow;
      set => this._shadowSealingExtendArrow = (XorFloat) value;
    }

    public float distanceRateFromAvoid
    {
      get => (float) this._distanceRateFromAvoid;
      set => this._distanceRateFromAvoid = (XorFloat) value;
    }

    public float distanceRateIai
    {
      get => (float) this._distanceRateIai;
      set => this._distanceRateIai = (XorFloat) value;
    }

    public float lockOnTimeRate
    {
      get => (float) this._lockOnTimeRate;
      set => this._lockOnTimeRate = (XorFloat) value;
    }

    public float burstReloadActionSpeed
    {
      get => (float) this._burstReloadActionSpeed;
      set => this._burstReloadActionSpeed = (XorFloat) value;
    }

    public int additionalMaxBulletCnt
    {
      get => (int) this._additionalMaxBulletCnt;
      set => this._additionalMaxBulletCnt = (XorInt) value;
    }

    public float dragonArmorDamageRate
    {
      get => (float) this._dragonArmorDamageRate;
      set => this._dragonArmorDamageRate = (XorFloat) value;
    }

    public int addSoulArrowLockCount
    {
      get => (int) this._addSoulArrowLockCount;
      set => this._addSoulArrowLockCount = (XorInt) value;
    }

    public float arrowRainNumRate
    {
      get => (float) this._arrowRainNumRate;
      set => this._arrowRainNumRate = (XorFloat) value;
    }

    public float burstSpearSpinTimeRate
    {
      get => (float) this._burstSpearSpinTimeRate;
      set => this._burstSpearSpinTimeRate = (XorFloat) value;
    }

    public float spearRushSpeedRate
    {
      get => (float) this._spearRushSpeedRate;
      set => this._spearRushSpeedRate = (XorFloat) value;
    }

    public float oracleThsHorizontalSpeedUp
    {
      get => (float) this._oracleThsHorizontalSpeedUp;
      set => this._oracleThsHorizontalSpeedUp = (XorFloat) value;
    }

    public float oracleThsSpinSmashTimeRate
    {
      get => (float) this._oracleSpinSmashTimeRate;
      set => this._oracleSpinSmashTimeRate = (XorFloat) value;
    }

    public float oracleThsDiveSmashTimeRate
    {
      get => (float) this._oracleDiveSmashTimeRate;
      set => this._oracleDiveSmashTimeRate = (XorFloat) value;
    }

    public float oracleThsWheelSmashTimeRate
    {
      get => (float) this._oracleWheelSmashTimeRate;
      set => this._oracleWheelSmashTimeRate = (XorFloat) value;
    }

    public int oracleOhsProtectionDoubleProbability
    {
      get => (int) this._oracleOhsProtectionDoubleProbability;
      set => this._oracleOhsProtectionDoubleProbability = (XorInt) value;
    }

    public float concussionExtend
    {
      get => (float) this._concussionExtend;
      set => this._concussionExtend = (XorFloat) value;
    }

    public float teleportUp
    {
      get => (float) this._teleportUp;
      set => this._teleportUp = (XorFloat) value;
    }

    public int stockAddInit
    {
      get => (int) this._stockAddInit;
      set => this._stockAddInit = (XorInt) value;
    }

    public float gutsTimeRateUp
    {
      get => (float) this._gutsTimeRateUp;
      set => this._gutsTimeRateUp = (XorFloat) value;
    }

    public float freeStockProbability
    {
      get => (float) this._freeStockProbability;
      set => this._freeStockProbability = (XorFloat) value;
    }

    public void Reset()
    {
      this.hp = 0;
      this.atkList.Clear();
      this.defList.Clear();
      for (int index = 0; index < 7; ++index)
      {
        this.atkList.Add(0);
        this.defList.Add(0);
      }
      this.tolList.Clear();
      for (int index = 0; index < 6; ++index)
        this.tolList.Add(0);
      this.hpUpRate = 0.0f;
      this.hpDownRate = 0.0f;
      this.atkUpRate.Set(0.0f);
      this.atkDownRate.Set(0.0f);
      this.defUpRate.Set(0.0f);
      this.defDownRate.Set(0.0f);
      this.tolUpRate.Set(0.0f);
      this.tolDownRate.Set(0.0f);
      this.skillAbsorbUp_OnlyAttackAndElement.Set(0.0f);
      this.moveSpeedUp = 0.0f;
      this.boostMoveSpeedUp = 0.0f;
      this.attackSpeedUp = 0.0f;
      this.boostAttackSpeedUp = 0.0f;
      this.hpHealSpeedUp = 0.0f;
      this.guardUp = 0.0f;
      this.skillAbsorbUp = 0.0f;
      this.skillHealSpeedUp = 0.0f;
      this.avoidUp = 0.0f;
      this.boostAvoidUp = 0.0f;
      this.healUP = 0.0f;
      this.healUpDependsWeapon = 0.0f;
      this.chargeSwordsTimeRate = 0.0f;
      this.chargeArrowTimeRate = 0.0f;
      this.chargeHeatArrowTimeRate = 0.0f;
      this.chargePairSwordsTimeRate = 0.0f;
      this.spearRushDistanceRate = 0.0f;
      this.chargeSpearTimeRate = 0.0f;
      this.skillTimeRate = 0.0f;
      this.bleedUp = 0.0f;
      this.damageDown = 0.0f;
      this.boostDamageDown = 0.0f;
      this.atkAllElement = 0.0f;
      this.gaugeIncreaseRate = 0.0f;
      this.gaugeDecreaseRate = 0.0f;
      this.subGaugeIncreaseRate = 0.0f;
      this.heatGaugeIncreaseRate = 0.0f;
      this.soulGaugeIncreaseRate = 0.0f;
      this.burstGaugeIncreaseRate = 0.0f;
      this.oracleGaugeIncreaseRate = 0.0f;
      this.soulChargeTimeRate = 0.0f;
      this.shadowSealingExtend = 0.0f;
      this.shadowSealingExtendArrow = 0.0f;
      this.distanceRateFromAvoid = 0.0f;
      this.distanceRateIai = 0.0f;
      this.lockOnTimeRate = 0.0f;
      this.dragonArmorDamageRate = 0.0f;
      this.burstSpearSpinTimeRate = 0.0f;
      this.spearRushSpeedRate = 0.0f;
      this.oracleThsHorizontalSpeedUp = 0.0f;
      this.concussionExtend = 0.0f;
      this.teleportUp = 0.0f;
      this.oracleThsSpinSmashTimeRate = 0.0f;
      this.oracleThsDiveSmashTimeRate = 0.0f;
      this.oracleThsWheelSmashTimeRate = 0.0f;
      this.stockAddInit = 0;
      this.gutsTimeRateUp = 0.0f;
      this.freeStockProbability = 0.0f;
      this.oracleOhsProtectionDoubleProbability = 0;
      this.burstReloadActionSpeed = 0.0f;
      this.additionalMaxBulletCnt = 0;
      this.addSoulArrowLockCount = 0;
      this.poisonDamageDownRate = 0.0f;
      this.poisonGuardWeight = 0;
      this.burnDamageDownRate = 0.0f;
      this.burnGuardWeight = 0;
      this.justGuardExtendRate = 0.0f;
      this.paralyzeGuardWeight = 0;
      this.silenceGuardWeight = 0;
      if (!this.firstInitialized)
        this.narrowEscape = 0;
      this.enableNarrowEscape = false;
      this.abilityAtkList.Clear();
      for (int index = 0; index < 5; ++index)
        this.badStatusUp[index] = 0.0f;
      for (int index = 0; index < 5; ++index)
        this.badStatusRateUp[index] = 0.0f;
      for (int index = 0; index < 19; ++index)
        this.tolerance[index] = 100;
      this.breakGhostFormCounter = 0;
      this.fieldBuffResist.Clear();
      this.fieldBuffResistByType.Clear();
      this.conditionsAbilityList.Clear();
    }
  }

  public class BuffData
  {
    public int conditionIndex = -1;
    public BuffParam.BUFFTYPE type;
    public bool enable;
    [FormerlySerializedAs("time")]
    private XorFloat _time = (XorFloat) 0.0f;
    [FormerlySerializedAs("value")]
    private XorInt _value = (XorInt) 0;
    [FormerlySerializedAs("interval")]
    private XorFloat _interval = (XorFloat) 0.0f;
    [FormerlySerializedAs("progress")]
    private XorFloat _progress = (XorFloat) 0.0f;
    public int avoidCount;
    public int damage;
    public BuffParam.VALUE_TYPE valueType;
    public int fromObjectID;
    public int fromEquipIndex = -1;
    public int fromSkillIndex = -1;
    public uint fromFieldBuffID;
    public bool sync = true;
    public bool isPlayLoopEffect = true;
    public bool? endless;
    public bool isCallBuffEnd = true;
    public uint skillId;
    public bool isOwnerEnemyBuffStart;

    public float time
    {
      get => (float) this._time;
      set => this._time = (XorFloat) value;
    }

    public int value
    {
      get => (int) this._value;
      set => this._value = (XorInt) value;
    }

    public float interval
    {
      get => (float) this._interval;
      set => this._interval = (XorFloat) value;
    }

    public float progress
    {
      get => (float) this._progress;
      set => this._progress = (XorFloat) value;
    }

    public bool EndBuff()
    {
      if (!this.enable)
        return false;
      this.enable = false;
      this.time = 0.0f;
      this.value = 0;
      this.interval = 0.0f;
      this.progress = 0.0f;
      this.avoidCount = 0;
      this.damage = 0;
      this.valueType = BuffParam.VALUE_TYPE.NONE;
      this.fromObjectID = 0;
      this.fromEquipIndex = -1;
      this.fromSkillIndex = -1;
      this.fromFieldBuffID = 0U;
      this.conditionIndex = -1;
      this.sync = true;
      this.endless = new bool?();
      this.skillId = 0U;
      this.isOwnerEnemyBuffStart = false;
      return true;
    }

    public void SetBuffData(BuffParam.BuffSyncData syncData)
    {
      this.time = syncData.time;
      this.value = syncData.value;
      this.valueType = (BuffParam.VALUE_TYPE) syncData.valueType;
      this.sync = false;
      this.conditionIndex = syncData.conditionIndex;
      this.fromObjectID = syncData.fromObjectID;
      this.fromEquipIndex = syncData.fromEquipIndex;
      this.fromSkillIndex = syncData.fromSkillIndex;
      if (string.IsNullOrEmpty(syncData.endless))
      {
        this.endless = new bool?();
      }
      else
      {
        bool result = false;
        bool.TryParse(syncData.endless, out result);
        this.endless = new bool?(result);
      }
      this.skillId = (uint) syncData.skillId;
      this.isOwnerEnemyBuffStart = syncData.isOwnerEnemyBuffStart;
    }

    public bool isSkillChargeType()
    {
      switch (this.type)
      {
        case BuffParam.BUFFTYPE.SKILL_CHARGE:
        case BuffParam.BUFFTYPE.SKILL_CHARGE_RATE:
        case BuffParam.BUFFTYPE.SKILL_CHARGE_FIRE:
        case BuffParam.BUFFTYPE.SKILL_CHARGE_WATER:
        case BuffParam.BUFFTYPE.SKILL_CHARGE_THUNDER:
        case BuffParam.BUFFTYPE.SKILL_CHARGE_SOIL:
        case BuffParam.BUFFTYPE.SKILL_CHARGE_LIGHT:
        case BuffParam.BUFFTYPE.SKILL_CHARGE_DARK:
          return true;
        default:
          return false;
      }
    }

    public bool isSkillChargeMoveType()
    {
      switch (this.type)
      {
        case BuffParam.BUFFTYPE.SKILL_CHARGE_ABOVE:
        case BuffParam.BUFFTYPE.SKILL_CHARGE_UNDER:
          return true;
        default:
          return false;
      }
    }

    public bool IsAbsorbType()
    {
      switch (this.type)
      {
        case BuffParam.BUFFTYPE.ABSORB_NORMAL:
        case BuffParam.BUFFTYPE.ABSORB_FIRE:
        case BuffParam.BUFFTYPE.ABSORB_WATER:
        case BuffParam.BUFFTYPE.ABSORB_THUNDER:
        case BuffParam.BUFFTYPE.ABSORB_SOIL:
        case BuffParam.BUFFTYPE.ABSORB_LIGHT:
        case BuffParam.BUFFTYPE.ABSORB_DARK:
        case BuffParam.BUFFTYPE.ABSORB_ALL_ELEMENT:
          return true;
        default:
          return false;
      }
    }

    public bool IsInvinsibleType()
    {
      switch (this.type)
      {
        case BuffParam.BUFFTYPE.INVINCIBLE_NORMAL:
        case BuffParam.BUFFTYPE.INVINCIBLE_FIRE:
        case BuffParam.BUFFTYPE.INVINCIBLE_WATER:
        case BuffParam.BUFFTYPE.INVINCIBLE_THUNDER:
        case BuffParam.BUFFTYPE.INVINCIBLE_SOIL:
        case BuffParam.BUFFTYPE.INVINCIBLE_ALL:
        case BuffParam.BUFFTYPE.INVINCIBLE_LIGHT:
        case BuffParam.BUFFTYPE.INVINCIBLE_DARK:
        case BuffParam.BUFFTYPE.INVINCIBLE_ALL_ELEMENT:
          return true;
        default:
          return false;
      }
    }
  }

  public class EffectInfo
  {
    public GameObject effect;
    public EffectPlayProcessor.EffectSetting setting;
    public List<BuffParam.BuffData> linkData = new List<BuffParam.BuffData>();
  }

  [Serializable]
  public class BuffSyncData
  {
    public int type;
    public float time;
    public int value;
    public int valueType;
    public int conditionIndex;
    public int fromObjectID;
    public int fromEquipIndex = -1;
    public int fromSkillIndex = -1;
    public string endless = string.Empty;
    public int skillId;
    public bool isOwnerEnemyBuffStart;

    public override string ToString()
    {
      string str = $"{$"{$"{$"{$"{$"{$"{$"{$"{"" + (object) this.type},{(object) this.time}"},{(object) this.value}"},{(object) this.valueType}"},{(object) this.conditionIndex}"},{(object) this.fromObjectID}"},{(object) this.fromEquipIndex}"},{(object) this.fromSkillIndex}"},{this.endless}"},{(object) this.skillId}";
      return base.ToString() + str;
    }
  }

  [Serializable]
  public class BuffSyncParam
  {
    public List<BuffParam.BuffSyncData> buffDatas = new List<BuffParam.BuffSyncData>();
    public int shieldHp;

    public override string ToString()
    {
      string str = "";
      if (this.buffDatas != null)
      {
        str += "b[";
        this.buffDatas.ForEach((Action<BuffParam.BuffSyncData>) (b => str = $"{str}({(object) b}),"));
        str += "],";
      }
      str = $"{str}shield={(object) this.shieldHp},";
      return base.ToString() + str;
    }
  }
}
