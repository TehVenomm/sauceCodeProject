// Decompiled with JetBrains decompiler
// Type: SpearController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class SpearController : IWeaponController
{
  private static readonly int defaultMaxStock = 8;
  private InGameSettingsManager.Player.SpearActionInfo spearInfo;
  private Player owner;
  private bool isCtrlActive;
  private bool isSoulSacrificedHp;
  private bool isSoulHealedHp;
  private bool isSoulNarrowEscaped;
  private bool isBladeEffectContinue;
  private float chargeRate;
  private Transform bladeEffectTrans;
  private Character.HealData healData;
  private Transform wepNode;
  private Transform spinNode;
  private Transform spinEffectTrans;
  private Transform spinGroundEffectTrans;
  private float spinTimer;
  private bool enableSpin;
  private bool isHitAttack;
  private bool isBarrierBulletDelete;
  private string spinEffectName = string.Empty;
  private string throwGroundEffectName = string.Empty;
  private string spinThrowGroundEffectName = string.Empty;
  private EffectCtrl spinEffectCtrl;
  private int EFFECT_STATE_NAME_HASH_LOOP_1;
  private int EFFECT_STATE_NAME_HASH_LOOP_2;
  private bool isPlayingSpinSE;
  private bool isPlayingSpinMaxSpeedSE;
  private BulletObject oracleSpBullet;
  private BulletControllerOracleSpearSp oracleSpController;
  private Transform oracleGuardEffect;
  private int consumedStockCounts;
  private bool freeToUseStock;
  public int[] stockedCounts = new int[3];

  public bool FullStocked => this.StockedCount >= this.MaxStockCount;

  public int MaxStockCount => SpearController.defaultMaxStock;

  public bool OracleSpCharged
  {
    get
    {
      return Object.op_Inequality((Object) this.oracleSpController, (Object) null) && this.oracleSpController.Charged;
    }
  }

  public bool InOracleSpLoop
  {
    get => Object.op_Inequality((Object) this.oracleSpController, (Object) null);
  }

  public float StockedRate
  {
    get
    {
      float stockedRate = 0.0f;
      if (this.MaxStockCount > 0)
        stockedRate = this.consumedStockCounts <= 0 ? (float) this.StockedCount / (float) this.MaxStockCount : (float) this.consumedStockCounts / (float) this.MaxStockCount;
      return stockedRate;
    }
  }

  public int StockedCount
  {
    get => this.stockedCounts[this.owner.weaponIndex];
    set => this.stockedCounts[this.owner.weaponIndex] = value;
  }

  public bool Guarding { get; set; }

  private float spActionGauge
  {
    get
    {
      return Object.op_Equality((Object) this.owner, (Object) null) ? 0.0f : this.owner.spActionGauge[this.owner.weaponIndex];
    }
    set
    {
      if (Object.op_Equality((Object) this.owner, (Object) null))
        return;
      this.owner.spActionGauge[this.owner.weaponIndex] = value;
    }
  }

  public void Init(Player player)
  {
    if (Object.op_Equality((Object) player, (Object) null) || !MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      return;
    this.owner = player;
    this.spearInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo;
    this.healData = new Character.HealData(0, HEAL_TYPE.NONE, HEAL_EFFECT_TYPE.BASIS, new List<int>()
    {
      80 /*0x50*/
    });
    this.EFFECT_STATE_NAME_HASH_LOOP_1 = Animator.StringToHash(GameDefine.EFFECT_STATE_NAME_LOOP_1);
    this.EFFECT_STATE_NAME_HASH_LOOP_2 = Animator.StringToHash(GameDefine.EFFECT_STATE_NAME_LOOP_2);
    for (int index = 0; index < 3; ++index)
      this.stockedCounts[index] = -1;
  }

  public bool IsGuard()
  {
    if (!Object.op_Inequality((Object) this.owner, (Object) null))
      return this.Guarding;
    return this.Guarding && !this.owner.disableGuard;
  }

  public void OnLoadComplete()
  {
    if (!this.owner.CheckAttackMode(Player.ATTACK_MODE.SPEAR))
    {
      this.isCtrlActive = false;
    }
    else
    {
      this.isCtrlActive = true;
      this.wepNode = this.owner.FindNode(GameDefine.PLAYER_WEAPON_PARENT_NODE_RIGHT);
      this.spinEffectName = string.Empty;
      this.throwGroundEffectName = string.Empty;
      this.spinThrowGroundEffectName = string.Empty;
      switch (this.owner.spAttackType)
      {
        case SP_ATTACK_TYPE.BURST:
          this.spinNode = this.owner.FindNode("weaponR");
          int nowWeaponElement = (int) this.owner.GetNowWeaponElement();
          if (nowWeaponElement < this.spearInfo.burstSpearInfo.spinEffectNames.Length)
            this.spinEffectName = this.spearInfo.burstSpearInfo.spinEffectNames[nowWeaponElement];
          if (nowWeaponElement < this.spearInfo.burstSpearInfo.spinThrowGroundEffectNames.Length)
            this.spinThrowGroundEffectName = this.spearInfo.burstSpearInfo.spinThrowGroundEffectNames[nowWeaponElement];
          this.throwGroundEffectName = this.spearInfo.burstSpearInfo.throwGroundEffectName;
          break;
        case SP_ATTACK_TYPE.ORACLE:
          if (this.StockedCount >= 0)
            break;
          this.StockedCount = this.owner.buffParam.passive.stockAddInit;
          break;
      }
    }
  }

  public void OnActDead()
  {
    if (!this.isCtrlActive)
      return;
    this.isSoulNarrowEscaped = false;
  }

  public void OnActReaction() => this.OnWeaponActionEnd();

  public void OnEndAction()
  {
    if (!this.isCtrlActive)
      return;
    this.isSoulSacrificedHp = false;
    this.isSoulHealedHp = false;
    if (!this.isBladeEffectContinue)
    {
      this.ClearBladeEffect();
      this.ClearStoredChargeRate();
    }
    this.isBladeEffectContinue = false;
    this.isHitAttack = false;
    this.GuardOff();
    this.DestroyOracleSp();
  }

  public void OnActAvoid() => this.OnWeaponActionEnd();

  public void OnActSkillAction() => this.OnWeaponActionEnd();

  public void OnRelease()
  {
  }

  public void OnActAttack(int id)
  {
  }

  public void OnBuffStart(BuffParam.BuffData data)
  {
  }

  public void OnBuffEnd(BuffParam.BUFFTYPE type)
  {
    if (type != BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS)
      return;
    this.consumedStockCounts = 0;
  }

  public void OnChangeWeapon()
  {
    this.OnWeaponActionEnd();
    if (!this.owner.IsValidBuff(BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS))
      return;
    this.owner.OnBuffEnd(BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS, true, true);
  }

  public void OnAttackedHitFix(AttackedHitStatusFix status)
  {
    if (!this.owner.IsCoopNone() && !this.owner.IsOriginal() || !this.IsGuard())
      return;
    if (this.owner._CheckJustGuardSec())
      this.GuardJust();
    else
      this.GuardOn(true);
  }

  public void Update()
  {
    if (!this.isCtrlActive)
      return;
    this.UpdateSpActionGauge();
    this.UpdateBurstSpin();
    this.UpdateOracleSp();
  }

  private void UpdateSpActionGauge()
  {
    if (!this.isCtrlActive || Object.op_Equality((Object) this.owner, (Object) null) || !this.owner.CheckAttackMode(Player.ATTACK_MODE.SPEAR))
      return;
    switch (this.owner.spAttackType)
    {
      case SP_ATTACK_TYPE.SOUL:
        if (!this.owner.isBoostMode)
          break;
        this.spActionGauge -= (!this.owner.enableInputCharge ? this.spearInfo.Soul_BoostModeGaugeDecreasePerSecond * Time.deltaTime : this.spearInfo.Soul_BoostModeGaugeDecreasePerSecondOnSpActionCharging * Time.deltaTime) * (1f + this.owner.GetSpGaugeDecreasingRate());
        if ((double) this.spActionGauge > 0.0)
          break;
        this.spActionGauge = 0.0f;
        break;
      case SP_ATTACK_TYPE.BURST:
        if (this.owner.isActSpecialAction)
        {
          this.spActionGauge -= this.spearInfo.burstSpearInfo.gaugeDecreaseOnSpAttackPerSecond * Time.deltaTime * (1f + this.owner.GetSpGaugeDecreasingRate());
          if ((double) this.spActionGauge > 0.0)
            break;
          this.spActionGauge = 0.0f;
          this.owner.SetNextTrigger();
          break;
        }
        if (this.enableSpin)
        {
          this.spActionGauge -= this.spearInfo.burstSpearInfo.gaugeDecreaseOnSpinPerSecond * Time.deltaTime * (1f + this.owner.GetSpGaugeDecreasingRate());
          if ((double) this.spActionGauge > 0.0)
            break;
          this.spActionGauge = 0.0f;
          this.OnWeaponActionEnd();
          break;
        }
        this.spActionGauge += this.spearInfo.burstSpearInfo.gaugeIncreasePerSecond * Time.deltaTime * (1f + this.owner.buffParam.GetGaugeIncreaseRate(SP_ATTACK_TYPE.BURST));
        if ((double) this.spActionGauge < (double) this.owner.CurrentWeaponSpActionGaugeMax)
          break;
        this.spActionGauge = this.owner.CurrentWeaponSpActionGaugeMax;
        break;
      case SP_ATTACK_TYPE.ORACLE:
        this.UpdateOracleStock();
        break;
    }
  }

  private void UpdateBurstSpin()
  {
    if (Object.op_Equality((Object) this.spinNode, (Object) null) || !this.enableSpin)
      return;
    float spinRate = this.GetSpinRate();
    this.spinNode.Rotate(0.0f, 0.0f, Mathf.Lerp(this.spearInfo.burstSpearInfo.spinSpeedMin, this.spearInfo.burstSpearInfo.spinSpeedMax, spinRate));
    this.spinTimer += Time.deltaTime;
    if ((double) spinRate < 1.0)
      return;
    if (!this.spinEffectCtrl.IsCurrentState(this.EFFECT_STATE_NAME_HASH_LOOP_2))
      this.spinEffectCtrl.Play(this.EFFECT_STATE_NAME_HASH_LOOP_2);
    if (this.spearInfo.burstSpearInfo.spinSeId > 0 && this.isPlayingSpinSE)
    {
      SoundManager.StopLoopSE(this.spearInfo.burstSpearInfo.spinSeId, (DisableNotifyMonoBehaviour) this.owner);
      this.isPlayingSpinSE = false;
    }
    if (this.spearInfo.burstSpearInfo.spinMaxSpeedSeId <= 0 || this.isPlayingSpinMaxSpeedSE)
      return;
    SoundManager.PlayLoopSE(this.spearInfo.burstSpearInfo.spinMaxSpeedSeId, (DisableNotifyMonoBehaviour) this.owner);
    this.isPlayingSpinMaxSpeedSE = true;
  }

  public void SacrificeHPBySoulAttackHit()
  {
    if (!this.isCtrlActive || !this.owner.CheckSpAttackType(SP_ATTACK_TYPE.SOUL) || this.owner.isBoostMode || this.isSoulSacrificedHp || !this.owner.IsCoopNone() && !this.owner.IsOriginal())
      return;
    int sacrificedHP = Mathf.FloorToInt((float) (this.owner.hpMax * this.GetSoulSpearSacrificeHpPercentByAttackID()) * 0.01f);
    if (sacrificedHP <= 0)
      return;
    this.SacrificedHp(sacrificedHP);
  }

  public void SacrificedHp(int sacrificedHP, bool isPacket = false)
  {
    int num = this.owner.hp - sacrificedHP;
    if (num <= 0 && (this.owner.attackID != this.spearInfo.Soul_AttackId || !this.isSoulNarrowEscaped))
    {
      num = 1;
      this.isSoulNarrowEscaped = true;
    }
    this.owner.hp = num;
    this.isSoulSacrificedHp = true;
    if (isPacket)
      return;
    if (MonoBehaviourSingleton<UIDamageManager>.IsValid() && this.owner is Self)
      MonoBehaviourSingleton<UIDamageManager>.I.CreatePlayerDamage((Character) this.owner, sacrificedHP, UIPlayerDamageNum.DAMAGE_COLOR.DAMAGE);
    if (Object.op_Inequality((Object) this.owner.playerSender, (Object) null))
      this.owner.playerSender.OnSacrificedHp(sacrificedHP);
    if (this.owner.hp > 0)
      return;
    if (this.owner.buffParam.IsNarrowEscape())
    {
      this.owner.buffParam.UseNarrowEscape();
      this.owner.hp = 1;
    }
    else
    {
      this.owner.healHp = 0;
      this.owner.ActDead(true, false);
    }
  }

  private int GetSoulSpearSacrificeHpPercentByAttackID()
  {
    int[] attackIdsForSacrifice = this.spearInfo.Soul_AttackIdsForSacrifice;
    int[] sacrificeHpPercents = this.spearInfo.Soul_SacrificeHPPercents;
    if (((IList<int>) attackIdsForSacrifice).IsNullOrEmpty<int>() || ((IList<int>) sacrificeHpPercents).IsNullOrEmpty<int>() || attackIdsForSacrifice.Length != sacrificeHpPercents.Length)
      return 0;
    int index = Array.IndexOf<int>(attackIdsForSacrifice, this.owner.attackID);
    return index < 0 ? 0 : sacrificeHpPercents[index];
  }

  public void HealHPBySoulSpAttackHit(SP_ATTACK_TYPE type)
  {
    if (!this.isCtrlActive || !this.owner.CheckSpAttackType(SP_ATTACK_TYPE.SOUL) || type != SP_ATTACK_TYPE.SOUL || this.isSoulHealedHp || !this.owner.IsCoopNone() && !this.owner.IsOriginal())
      return;
    int percentByChargeRate = this.GetSoulSpearHealHpPercentByChargeRate();
    if (percentByChargeRate <= 0)
      return;
    this.isSoulHealedHp = true;
    this.healData.healHp = percentByChargeRate;
    this.owner.OnHealReceive(this.healData);
  }

  public void StoreChargeRate(float chargeRate) => this.chargeRate = chargeRate;

  private void ClearStoredChargeRate() => this.chargeRate = 0.0f;

  private int GetSoulSpearHealHpPercentByChargeRate()
  {
    if (((IList<int>) this.spearInfo.Soul_HealHPPercents).IsNullOrEmpty<int>() || this.spearInfo.Soul_HealHPPercents.Length < 3)
      return 0;
    return (double) this.chargeRate >= 1.0 ? Mathf.FloorToInt((float) (this.owner.hpMax * this.spearInfo.Soul_HealHPPercents[2]) * 0.01f) : Mathf.FloorToInt((float) (this.owner.hpMax * (this.spearInfo.Soul_HealHPPercents[0] + Mathf.FloorToInt((float) (this.spearInfo.Soul_HealHPPercents[1] - this.spearInfo.Soul_HealHPPercents[0]) * this.chargeRate))) * 0.01f);
  }

  public bool GetBoostDamageUpRate(ref float value)
  {
    value = 1f;
    if (!this.isCtrlActive || !MonoBehaviourSingleton<InGameSettingsManager>.IsValid() || this.owner.actionID != Character.ACTION_ID.ATTACK)
      return false;
    switch (this.owner.spAttackType)
    {
      case SP_ATTACK_TYPE.SOUL:
        if (!this.owner.isBoostMode)
          return false;
        value = this.spearInfo.Soul_BoostElementDamageRate;
        break;
      case SP_ATTACK_TYPE.BURST:
        if (!this.enableSpin)
          return false;
        value = (double) this.GetSpinRate() < 1.0 ? this.spearInfo.burstSpearInfo.spinElementDamageRate : this.spearInfo.burstSpearInfo.spinElementDamageRateMax;
        break;
      default:
        return false;
    }
    return true;
  }

  public bool GetSpinRate(ref float rate)
  {
    if (!this.isCtrlActive || !this.owner.CheckSpAttackType(SP_ATTACK_TYPE.BURST))
      return false;
    float num = this.spearInfo.burstSpearInfo.spinTimeToMaxSpeed - this.spearInfo.burstSpearInfo.spinTimeToMaxSpeed * this.owner.buffParam.GetBurstSpearSpinTimeRate();
    rate = (double) num > 0.0 ? Mathf.Clamp01(this.spinTimer / num) : 1f;
    return true;
  }

  private float GetSpinRate()
  {
    float num = this.spearInfo.burstSpearInfo.spinTimeToMaxSpeed - this.spearInfo.burstSpearInfo.spinTimeToMaxSpeed * this.owner.buffParam.GetBurstSpearSpinTimeRate();
    return (double) num <= 0.0 ? 1f : Mathf.Clamp01(this.spinTimer / num);
  }

  public bool IsSoulBoostMode()
  {
    return this.isCtrlActive && this.owner.CheckSpAttackType(SP_ATTACK_TYPE.SOUL) && this.owner.isBoostMode;
  }

  public bool IsBurstSpin()
  {
    return this.isCtrlActive && this.owner.CheckSpAttackType(SP_ATTACK_TYPE.BURST) && this.enableSpin;
  }

  public bool IsSpecialActionHit() => this.IsBurstSpin();

  public void ExecBladeEffect()
  {
    if (!MonoBehaviourSingleton<EffectManager>.IsValid())
      return;
    this.bladeEffectTrans = EffectManager.GetEffect("ef_btl_wsk2_spear_02_02", this.wepNode);
  }

  public void ContinueBladeEffect() => this.isBladeEffectContinue = true;

  public void MakeInvincible() => this.owner.hitOffFlag |= StageObject.HIT_OFF_FLAG.INVICIBLE;

  public void EnableHitFlag()
  {
    if (this.isHitAttack || this.owner.attackID == this.spearInfo.burstSpearInfo.hitComboAttackId)
      return;
    this.isHitAttack = true;
  }

  public bool IsInputedSpAttackContinue()
  {
    return this.isCtrlActive && this.owner.inputComboFlag && this.owner.inputComboID == this.spearInfo.Soul_SpAttackContinueId;
  }

  public bool IsEnableBurstCombo()
  {
    return this.isCtrlActive && this.owner.CheckSpAttackType(SP_ATTACK_TYPE.BURST) && this.isHitAttack;
  }

  private void ClearBladeEffect()
  {
    if (!MonoBehaviourSingleton<EffectManager>.IsValid() || Object.op_Equality((Object) this.bladeEffectTrans, (Object) null))
      return;
    EffectManager.ReleaseEffect(((Component) this.bladeEffectTrans).gameObject);
    this.bladeEffectTrans = (Transform) null;
  }

  private void ClearSpinEffect()
  {
    if (Object.op_Equality((Object) this.spinEffectTrans, (Object) null))
      return;
    EffectManager.ReleaseEffect(((Component) this.spinEffectTrans).gameObject);
    this.spinEffectTrans = (Transform) null;
  }

  private void ClearSpinGroundEffect()
  {
    if (Object.op_Equality((Object) this.spinGroundEffectTrans, (Object) null))
      return;
    EffectManager.ReleaseEffect(ref this.spinGroundEffectTrans);
  }

  private void ClearSpinSE()
  {
    if (this.spearInfo.burstSpearInfo.spinSeId > 0)
      SoundManager.StopLoopSE(this.spearInfo.burstSpearInfo.spinSeId, (DisableNotifyMonoBehaviour) this.owner);
    if (this.spearInfo.burstSpearInfo.spinMaxSpeedSeId > 0)
      SoundManager.StopLoopSE(this.spearInfo.burstSpearInfo.spinMaxSpeedSeId, (DisableNotifyMonoBehaviour) this.owner);
    this.isPlayingSpinSE = false;
    this.isPlayingSpinMaxSpeedSE = false;
  }

  public bool GetNormalAttackId(
    SP_ATTACK_TYPE _spAtkType,
    EXTRA_ATTACK_TYPE _exAtkType,
    ref int _attackId,
    ref string _motionLayerName)
  {
    if (Object.op_Equality((Object) this.owner, (Object) null) || this.spearInfo == null)
      return false;
    switch (_spAtkType)
    {
      case SP_ATTACK_TYPE.SOUL:
        _attackId = this.spearInfo.Soul_AttackId;
        break;
      case SP_ATTACK_TYPE.BURST:
        _attackId = this.spearInfo.burstSpearInfo.baseAtkId;
        _motionLayerName = this.owner.GetMotionLayerName(Player.ATTACK_MODE.SPEAR, _spAtkType, _attackId);
        break;
      case SP_ATTACK_TYPE.ORACLE:
        _attackId = this.spearInfo.oracle.comboAttackId;
        _motionLayerName = this.owner.GetMotionLayerName(Player.ATTACK_MODE.SPEAR, _spAtkType, _attackId);
        break;
    }
    return true;
  }

  public bool GetSpActionInfo(
    SP_ATTACK_TYPE _spAtkType,
    EXTRA_ATTACK_TYPE _exAtkType,
    ref int _attackId,
    ref string _motionLayerName)
  {
    if (Object.op_Equality((Object) this.owner, (Object) null) || this.spearInfo == null)
      return false;
    switch (_spAtkType)
    {
      case SP_ATTACK_TYPE.NONE:
        _attackId = this.spearInfo.rushLoopAttackID;
        break;
      case SP_ATTACK_TYPE.HEAT:
        _attackId = this.spearInfo.Heat_SpAttackId;
        break;
      case SP_ATTACK_TYPE.SOUL:
        _attackId = this.spearInfo.Soul_SpAttackId;
        break;
      case SP_ATTACK_TYPE.BURST:
        _attackId = this.spearInfo.burstSpearInfo.spAtkId;
        _motionLayerName = this.owner.GetMotionLayerName(Player.ATTACK_MODE.SPEAR, _spAtkType, _attackId);
        break;
      case SP_ATTACK_TYPE.ORACLE:
        _attackId = this.spearInfo.oracle.guardAttackId;
        _motionLayerName = this.owner.GetMotionLayerName(Player.ATTACK_MODE.SPEAR, _spAtkType, _attackId);
        break;
    }
    return true;
  }

  public float GetWalkSpeedUp(SP_ATTACK_TYPE _spAtkType)
  {
    switch (_spAtkType)
    {
      case SP_ATTACK_TYPE.NONE:
        return 0.0f;
      case SP_ATTACK_TYPE.HEAT:
        return this.spearInfo.heatWalkSpeed;
      case SP_ATTACK_TYPE.SOUL:
        return this.spearInfo.Soul_WalkSpeedUpRate;
      case SP_ATTACK_TYPE.BURST:
        return 0.0f;
      default:
        return 0.0f;
    }
  }

  public float GetAvoidUp(SP_ATTACK_TYPE _spAtkType)
  {
    return _spAtkType != SP_ATTACK_TYPE.HEAT && _spAtkType == SP_ATTACK_TYPE.BURST ? this.spearInfo.burstSpearInfo.avoidSpeedUpRate : 0.0f;
  }

  public bool TryOverrideHitEffect(ref EnemyHitTypeTable.TypeData retTypeData, ref Vector3 scale)
  {
    if (!this.isCtrlActive || !this.owner.CheckSpAttackType(SP_ATTACK_TYPE.BURST) || ((IList<string>) this.spearInfo.burstSpearInfo.spinElementHitEffectNames).IsNullOrEmpty<string>() || !this.IsBurstSpin())
      return false;
    retTypeData = new EnemyHitTypeTable.TypeData();
    int index = 0;
    for (int length = this.spearInfo.burstSpearInfo.spinElementHitEffectNames.Length; index < length; ++index)
      retTypeData.elementEffectNames[index] = this.spearInfo.burstSpearInfo.spinElementHitEffectNames[index];
    scale = this.spearInfo.burstSpearInfo.spinElementHitEffectScale;
    return true;
  }

  public void ActAttackBurstCombo()
  {
    if (!this.isCtrlActive || !this.owner.CheckSpAttackType(SP_ATTACK_TYPE.BURST))
      return;
    int hitComboAttackId = this.spearInfo.burstSpearInfo.hitComboAttackId;
    string motionLayerName = this.owner.GetMotionLayerName(Player.ATTACK_MODE.SPEAR, SP_ATTACK_TYPE.BURST, hitComboAttackId);
    this.owner.ActAttack(hitComboAttackId, true, false, motionLayerName, "");
  }

  public void OnWeaponActionStart()
  {
    if (!this.isCtrlActive || !this.owner.CheckSpAttackType(SP_ATTACK_TYPE.BURST))
      return;
    this.enableSpin = true;
    if (!Object.op_Equality((Object) this.spinEffectTrans, (Object) null))
      return;
    this.spinEffectTrans = EffectManager.GetEffect(this.spinEffectName, this.wepNode);
    if (Object.op_Inequality((Object) this.spinEffectTrans, (Object) null))
    {
      this.spinEffectCtrl = ((Component) this.spinEffectTrans).GetComponent<EffectCtrl>();
      if (Object.op_Inequality((Object) this.spinEffectCtrl, (Object) null))
        this.spinEffectCtrl.Play(this.EFFECT_STATE_NAME_HASH_LOOP_1);
    }
    if (this.spearInfo.burstSpearInfo.spinSeId <= 0 || this.isPlayingSpinSE)
      return;
    SoundManager.PlayLoopSE(this.spearInfo.burstSpearInfo.spinSeId, (DisableNotifyMonoBehaviour) this.owner);
    this.isPlayingSpinSE = true;
  }

  public void OnWeaponActionEnd()
  {
    if (!this.isCtrlActive)
      return;
    if (Object.op_Inequality((Object) this.spinNode, (Object) null))
      this.spinNode.localRotation = InGameUtility.QUATERNION_IDENTITY;
    this.enableSpin = false;
    this.spinTimer = 0.0f;
    this.ClearSpinEffect();
    this.ClearSpinSE();
  }

  public bool CheckConditionTrigger()
  {
    return this.isCtrlActive && this.owner.CheckSpAttackType(SP_ATTACK_TYPE.BURST) && this.enableSpin && (!this.owner.IsOriginal() && !this.owner.IsCoopNone() || this.owner.attackHitCount > 0);
  }

  public bool CheckConditionTrigger2()
  {
    return this.isCtrlActive && this.owner.CheckSpAttackType(SP_ATTACK_TYPE.BURST) && !this.enableSpin;
  }

  public void OnFixPositionWeaponR_ON(Vector3 pos)
  {
    if (!this.isCtrlActive || !this.owner.CheckSpAttackType(SP_ATTACK_TYPE.BURST))
      return;
    Vector3 pos1;
    // ISSUE: explicit constructor call
    ((Vector3) ref pos1).\u002Ector(pos.x, 0.0f, pos.z);
    EffectManager.OneShot(this.throwGroundEffectName, pos1, Quaternion.identity);
    if (!this.enableSpin)
      return;
    this.spinGroundEffectTrans = EffectManager.GetEffect(this.spinThrowGroundEffectName);
    this.spinGroundEffectTrans.position = pos1;
  }

  public void OnFixPositionWeaponR_OFF() => this.ClearSpinGroundEffect();

  public bool IsBarrierBulletDelete() => this.isBarrierBulletDelete;

  public void EnableBarrierBulletDelete() => this.isBarrierBulletDelete = true;

  public void DisableBarrierBulletDelete() => this.isBarrierBulletDelete = false;

  public static bool IsOracleAttackId(int attackId)
  {
    InGameSettingsManager.Player.SpearActionInfo.Oracle oracle = MonoBehaviourSingleton<InGameSettingsManager>.I.player.spearActionInfo.oracle;
    return attackId >= oracle.comboAttackId && attackId <= oracle.reservedAttackId;
  }

  public bool CanConsumeOracleStock(int count = 0)
  {
    return this.StockedCount > 0 && this.StockedCount >= count;
  }

  public void ConsumeOracleStock(int count = 0)
  {
    if (!this.CanConsumeOracleStock(count) || this.LotFreeToUseStock())
      return;
    if (count > 0)
      this.stockedCounts[this.owner.weaponIndex] = Mathf.Max(0, this.stockedCounts[this.owner.weaponIndex] - count);
    else
      this.stockedCounts[this.owner.weaponIndex] = 0;
  }

  private bool LotFreeToUseStock()
  {
    float stockProbability = this.owner.buffParam.passive.freeStockProbability;
    return (double) stockProbability > 0.0 && (double) Random.Range(0.0f, 1f) <= (double) stockProbability;
  }

  public void EventShotOracleSp(AnimEventData.EventData data)
  {
    if (Object.op_Inequality((Object) this.oracleSpBullet, (Object) null))
      this.DestroyOracleSp();
    AttackInfo attackInfo = this.owner.FindAttackInfo(data.stringArgs[0]);
    if (attackInfo == null)
      return;
    this.oracleSpBullet = (BulletObject) AnimEventShot.Create((StageObject) this.owner, data, attackInfo, Vector3.zero);
    if (!Object.op_Inequality((Object) this.oracleSpBullet, (Object) null))
      return;
    this.oracleSpController = ((Component) this.oracleSpBullet).GetComponent<BulletControllerOracleSpearSp>();
  }

  public void DestroyOracleSp()
  {
    if (!Object.op_Inequality((Object) this.oracleSpBullet, (Object) null))
      return;
    this.oracleSpBullet.OnDestroy();
    this.oracleSpBullet = (BulletObject) null;
    this.oracleSpController = (BulletControllerOracleSpearSp) null;
  }

  public void UpdateOracleSp()
  {
    if (!Object.op_Inequality((Object) this.oracleSpController, (Object) null) || (double) this.owner.GetChargingRate() < 1.0 || this.oracleSpController.Charged || !this.owner.IsValidBuff(BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS) && !this.CanConsumeOracleStock(1))
      return;
    if (!this.owner.IsValidBuff(BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS))
      this.ConsumeOracleStock(1);
    this.oracleSpController.UpdateChargedEffect();
  }

  public void GuardOn(bool isPlayEnd = false)
  {
    if (!this.Guarding)
    {
      this.Guarding = true;
      this.owner._StartGuard();
    }
    if (Object.op_Inequality((Object) this.oracleGuardEffect, (Object) null))
    {
      EffectManager.ReleaseEffect(((Component) this.oracleGuardEffect).gameObject, isPlayEnd, !isPlayEnd);
      this.oracleGuardEffect = (Transform) null;
    }
    this.oracleGuardEffect = EffectManager.GetEffect("ef_btl_wsk4_spear_guard", this.owner._transform);
  }

  public void GuardOff()
  {
    this.Guarding = false;
    this.owner._EndGuard();
    if (!Object.op_Inequality((Object) this.oracleGuardEffect, (Object) null))
      return;
    Animator component = ((Component) this.oracleGuardEffect).GetComponent<Animator>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.Play("END_cancel");
  }

  public void GuardJust()
  {
    if (!Object.op_Inequality((Object) this.oracleGuardEffect, (Object) null))
      return;
    Animator component = ((Component) this.oracleGuardEffect).GetComponent<Animator>();
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.Play("END_just");
    SoundManager.PlayOneShotSE(10000042, this.owner._position);
  }

  public void StartOracleGutsMode()
  {
    if (this.owner.IsValidBuff(BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS) || !this.CanConsumeOracleStock())
      return;
    this.consumedStockCounts = this.StockedCount;
    this.ConsumeOracleStock();
    this.owner.FinishBoostMode();
    this.owner.DoHealType(HEAL_TYPE.ALL_BADSTATUS);
    SoundManager.PlayOneShotSE(this.spearInfo.oracle.gutsSE, this.owner._position);
    BuffParam.BuffData buffData = new BuffParam.BuffData();
    buffData.type = BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS;
    buffData.time = this.spearInfo.oracle.gutsBaseTime;
    buffData.time += this.spearInfo.oracle.gutsTimePerStock * (float) this.consumedStockCounts;
    buffData.time *= 1f + this.owner.buffParam.passive.gutsTimeRateUp;
    buffData.value = 999;
    this.owner.OnBuffStart(buffData);
  }

  public float GetOracleElementDamageRate(AttackHitInfo info)
  {
    float num = 1f;
    float elementDamageRate;
    if (info.attackType == AttackHitInfo.ATTACK_TYPE.SPEAR_ORACLE_SP)
      elementDamageRate = !this.OracleSpCharged ? num * this.spearInfo.oracle.spElementDamageRate : num * this.spearInfo.oracle.chargedSpElementDamageRate;
    else if (info.attackType == AttackHitInfo.ATTACK_TYPE.SPEAR_ORACLE_SP_CHARGED)
    {
      elementDamageRate = num * this.spearInfo.oracle.chargedSpElementDamageRate;
    }
    else
    {
      elementDamageRate = Mathf.Lerp(num, this.spearInfo.oracle.elementDamageRateFullStocked, this.StockedRate);
      if (this.owner.IsValidBuff(BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS))
        elementDamageRate *= this.spearInfo.oracle.elementDamageRateWhileGuts;
    }
    return elementDamageRate;
  }

  public float GetOracleAttackSpeedRate()
  {
    float oracleAttackSpeedRate = Mathf.Lerp(1f, this.spearInfo.oracle.attackSpeedRateFullStocked, this.StockedRate);
    if (this.owner.IsValidBuff(BuffParam.BUFFTYPE.ORACLE_SPEAR_GUTS))
      oracleAttackSpeedRate *= this.spearInfo.oracle.attackSpeedRateWhileGuts;
    return oracleAttackSpeedRate;
  }

  public float GetOracleSpChargeTimeRate(float rate)
  {
    return Mathf.Lerp(rate, rate * this.spearInfo.oracle.spChargeTimeRateFullStocked, this.StockedRate);
  }

  private void UpdateOracleStock()
  {
    if (!this.owner.IsSpActionGaugeFullCharged() || this.stockedCounts[this.owner.weaponIndex] >= this.MaxStockCount)
      return;
    this.OnUpdateOracleStock();
  }

  public void OnUpdateOracleStock()
  {
    ++this.stockedCounts[this.owner.weaponIndex];
    this.spActionGauge = 0.0f;
    SoundManager.PlayOneShotUISE(40000359);
    Transform effect = EffectManager.GetEffect("ef_btl_wsk4_spear_stock", this.owner._transform);
    if (Object.op_Inequality((Object) effect, (Object) null))
      EffectManager.ReleaseEffect(((Component) effect).gameObject);
    if (this.owner.IsCoopNone() || this.owner.IsOriginal())
    {
      Self owner = this.owner as Self;
      if (Object.op_Inequality((Object) owner, (Object) null) && MonoBehaviourSingleton<InGameManager>.IsValid())
      {
        owner.taskChecker.OnOracleSpear();
        MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnOracleSpear();
      }
    }
    if (!Object.op_Inequality((Object) this.owner.playerSender, (Object) null))
      return;
    this.owner.playerSender.OnUpdateOracleSpearStock();
  }
}
