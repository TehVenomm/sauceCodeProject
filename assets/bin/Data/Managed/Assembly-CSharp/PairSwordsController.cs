// Decompiled with JetBrains decompiler
// Type: PairSwordsController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class PairSwordsController : IObserver, IWeaponController
{
  private static readonly Vector3 OFFSET_LASER_WAIT_EFFECT = new Vector3(0.0f, 1f, 1f);
  private static readonly int HASH_EFFECT_ON_WEAPON_FULL = Animator.StringToHash("Base Layer.LOOP1");
  private static readonly int HASH_EFFECT_ON_WEAPON_DEFAULT = Animator.StringToHash("Base Layer.LOOP2");
  private bool isCtrlActive;
  private PairSwordsController.CHARGE_STATE chargeState;
  private InGameSettingsManager.Player.PairSwordsActionInfo pairSwordsInfo;
  private Player owner;
  private float timerForSpActionGaugeDecreaseAfterHit;
  private bool isExecLaserEnd;
  private bool isEventShotLaserExec;
  private bool isSetGaugePercentForLaser;
  private float gaugePercentForLaser;
  private int comboLvBySync;
  private List<AnimEventShot> bulletLaserList = new List<AnimEventShot>(3);
  private List<Transform> effectTransOnWeaponList = new List<Transform>(2);
  private List<Animator> effectAnimatorOnWeaponList = new List<Animator>(2);
  private Transform effectTransStartShotLaser;
  private bool hasJustAvoid;
  public Transform oracleSpEffect;
  public Transform oracleRushLoopEffect;
  public Transform oracleRushEffect;
  private const int kBurstNormalAttackId = 30;
  private const int kBurstAerialComboAttackId = 32 /*0x20*/;
  private const int kBurstCombineAttackId = 33;
  private const int kBurstCombineId = 34;
  private const int kBurstAerialSpecialId = 35;
  private const int kBurstGroundSpecialStartId = 36;
  private const int kBurstGroundSpecialEndId = 39;
  protected bool isCombineMode;
  protected Quaternion combineEffectRotation = Quaternion.identity;
  protected Vector3 combineEffectScale = new Vector3(1.5f, 1.5f, 1.5f);
  public const int kOracleNormalAttackId = 40;
  public const int kOracleNormal2AttackId = 41;
  public const int kOracleRushStartAttackId = 42;
  public const int kOracleRushAttackId = 43;
  public const int kOracleSpAttackId = 44;
  public const int kOracleAvoidAttackId = 45;

  private void SetChargeState(PairSwordsController.CHARGE_STATE chargeState)
  {
    this.chargeState = chargeState;
  }

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

  private float spActionGaugeMax
  {
    get
    {
      return Object.op_Equality((Object) this.owner, (Object) null) ? 0.0f : this.owner.spActionGaugeMax[this.owner.weaponIndex];
    }
    set
    {
      if (Object.op_Equality((Object) this.owner, (Object) null))
        return;
      this.owner.spActionGaugeMax[this.owner.weaponIndex] = value;
    }
  }

  private void ResetTimerForSpActionGaugeDecreaseAfterHit()
  {
    this.timerForSpActionGaugeDecreaseAfterHit = 0.0f;
  }

  public void SetEventShotLaserExec() => this.isEventShotLaserExec = true;

  public void SetGaugePercentForLaser()
  {
    if (this.isSetGaugePercentForLaser)
      return;
    this.gaugePercentForLaser = this.GetGaugeChargedPercent();
    this.isSetGaugePercentForLaser = true;
  }

  public void AddBulletLaser(AnimEventShot bullet) => this.bulletLaserList.Add(bullet);

  public void GetEffectTransStartShotLaser()
  {
    if (!MonoBehaviourSingleton<EffectManager>.IsValid() || !Object.op_Equality((Object) this.effectTransStartShotLaser, (Object) null))
      return;
    this.effectTransStartShotLaser = EffectManager.GetEffect(this.pairSwordsInfo.Soul_EffectForWaitingLaser, this.owner._transform);
    this.effectTransStartShotLaser.localPosition = PairSwordsController.OFFSET_LASER_WAIT_EFFECT;
  }

  public void Init(Player player)
  {
    this.owner = player;
    if (!MonoBehaviourSingleton<InGameSettingsManager>.IsValid())
      return;
    this.pairSwordsInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.pairSwordsActionInfo;
  }

  public void OnLoadComplete()
  {
    if (!this.owner.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL))
    {
      this.isCtrlActive = false;
    }
    else
    {
      this.isCtrlActive = true;
      this.effectTransOnWeaponList.Clear();
      this.effectAnimatorOnWeaponList.Clear();
      string name = this.pairSwordsInfo.Soul_EffectsForWeapon[this.owner.GetCurrentWeaponElement()];
      Transform transform1 = Utility.Find(this.owner.FindNode("R_Wep"), name);
      Transform transform2 = Utility.Find(this.owner.FindNode("L_Wep"), name);
      this.effectTransOnWeaponList.Add(transform1);
      this.effectTransOnWeaponList.Add(transform2);
      this.effectAnimatorOnWeaponList.Add(((Component) transform1).GetComponent<Animator>());
      this.effectAnimatorOnWeaponList.Add(((Component) transform2).GetComponent<Animator>());
    }
  }

  public void Update()
  {
    switch (this.chargeState)
    {
      case PairSwordsController.CHARGE_STATE.LOOP:
        if (!this.owner.IsCoopNone() && !this.owner.IsOriginal() && this.isExecLaserEnd)
        {
          this.SetChargeState(PairSwordsController.CHARGE_STATE.END);
          break;
        }
        break;
      case PairSwordsController.CHARGE_STATE.LASER_SHOT:
        if (this.isEventShotLaserExec)
        {
          this.SetChargeState(PairSwordsController.CHARGE_STATE.LASER_LOOP);
          break;
        }
        break;
      case PairSwordsController.CHARGE_STATE.LASER_LOOP:
        if ((double) this.spActionGauge <= 0.0)
        {
          this.owner.SetNextTrigger();
          this.SetChargeState(PairSwordsController.CHARGE_STATE.END);
          break;
        }
        break;
      case PairSwordsController.CHARGE_STATE.END:
        this.OnLaserEnd();
        this.isExecLaserEnd = false;
        this.SetChargeState(PairSwordsController.CHARGE_STATE.NONE);
        break;
    }
    this.timerForSpActionGaugeDecreaseAfterHit += Time.deltaTime;
    this.UpdateSpActionGauge();
    this.UpdateEffectOnWeapon();
  }

  private void UpdateSpActionGauge()
  {
    if (Object.op_Equality((Object) this.owner, (Object) null) || !this.owner.CheckAttackMode(Player.ATTACK_MODE.PAIR_SWORDS))
      return;
    switch (this.owner.spAttackType)
    {
      case SP_ATTACK_TYPE.HEAT:
        if (!this.owner.isBoostMode)
          return;
        this.owner.spActionGauge[this.owner.weaponIndex] -= this.pairSwordsInfo.boostGaugeDecreasePerSecond * (1f + this.owner.GetSpGaugeDecreasingRate()) * Time.deltaTime;
        break;
      case SP_ATTACK_TYPE.SOUL:
        if ((this.chargeState == PairSwordsController.CHARGE_STATE.LASER_SHOT || this.chargeState == PairSwordsController.CHARGE_STATE.LASER_LOOP) && this.isEventShotLaserExec)
        {
          this.spActionGauge -= this.pairSwordsInfo.Soul_GaugeDecreaseShootingLaserPerSecond * Time.deltaTime;
          break;
        }
        if ((double) this.timerForSpActionGaugeDecreaseAfterHit >= (double) this.pairSwordsInfo.Soul_TimeForGaugeDecreaseAfterHit && !this.owner.IsStone() && (!this.IsComboLvMax() || (double) this.timerForSpActionGaugeDecreaseAfterHit >= (double) this.pairSwordsInfo.Soul_TimeForGaugeDecreaseAfterHitOnComboLvMax))
        {
          if (this.chargeState == PairSwordsController.CHARGE_STATE.LOOP || this.chargeState == PairSwordsController.CHARGE_STATE.LASER_SHOT && !this.isEventShotLaserExec)
          {
            this.spActionGauge -= this.pairSwordsInfo.Soul_GaugeDecreaseWaitingLaserPerSecond * Time.deltaTime;
            break;
          }
          this.spActionGauge -= this.pairSwordsInfo.Soul_GaugeDecreasePerSecond * Time.deltaTime;
          break;
        }
        break;
      case SP_ATTACK_TYPE.BURST:
        if (!this.owner.isBoostMode)
          return;
        this.owner.spActionGauge[this.owner.weaponIndex] -= this.pairSwordsInfo.Burst_BoostGaugeDecreasePerSecond * (1f + this.owner.GetSpGaugeDecreasingRate()) * Time.deltaTime;
        break;
      case SP_ATTACK_TYPE.ORACLE:
        if (!this.owner.isDead && !this.owner.enabledRushAvoid && this.owner.actionID != (Character.ACTION_ID) 49)
        {
          if (this.owner.enableInputCharge)
          {
            this.spActionGauge -= this.pairSwordsInfo.Oracle_SpGaugeDecreasePerSecond * (1f + this.owner.GetSpGaugeDecreasingRate()) * Time.deltaTime;
            if ((double) this.spActionGauge <= 0.0)
            {
              this.owner.SetChargeRelease(1f);
              break;
            }
            break;
          }
          this.spActionGauge += this.pairSwordsInfo.Oracle_SpGaugeIncreasePerSecond * (1f + this.owner.GetSpGaugeDecreasingRate()) * Time.deltaTime;
          break;
        }
        break;
    }
    this.spActionGauge = Mathf.Max(0.0f, Mathf.Min(this.spActionGaugeMax, this.spActionGauge));
  }

  private void UpdateEffectOnWeapon()
  {
    if (this.effectAnimatorOnWeaponList.IsNullOrEmpty<Animator>())
      return;
    for (int index = 0; index < this.effectAnimatorOnWeaponList.Count; ++index)
    {
      if (!Object.op_Equality((Object) this.effectAnimatorOnWeaponList[index], (Object) null))
      {
        AnimatorStateInfo animatorStateInfo = this.effectAnimatorOnWeaponList[index].GetCurrentAnimatorStateInfo(0);
        int fullPathHash = ((AnimatorStateInfo) ref animatorStateInfo).fullPathHash;
        if (this.IsComboLvMax())
        {
          if (fullPathHash != PairSwordsController.HASH_EFFECT_ON_WEAPON_FULL)
            this.effectAnimatorOnWeaponList[index].Play(PairSwordsController.HASH_EFFECT_ON_WEAPON_FULL);
        }
        else
        {
          if (fullPathHash != PairSwordsController.HASH_EFFECT_ON_WEAPON_DEFAULT)
            this.effectAnimatorOnWeaponList[index].Play(PairSwordsController.HASH_EFFECT_ON_WEAPON_DEFAULT);
          Vector3 localScale = this.effectTransOnWeaponList[index].localScale;
          localScale.z = this.GetGaugeRate();
          this.effectTransOnWeaponList[index].localScale = localScale;
        }
      }
    }
  }

  public void OnStartCharge() => this.SetChargeState(PairSwordsController.CHARGE_STATE.LOOP);

  public void OnLaserEnd(bool isPacket = false)
  {
    if (MonoBehaviourSingleton<EffectManager>.IsValid() && Object.op_Inequality((Object) this.effectTransStartShotLaser, (Object) null))
    {
      EffectManager.ReleaseEffect(((Component) this.effectTransStartShotLaser).gameObject);
      this.effectTransStartShotLaser = (Transform) null;
    }
    this.ClearLaserBullet();
    if (this.pairSwordsInfo.Soul_SeIds.Length >= 2)
      SoundManager.StopLoopSE(this.pairSwordsInfo.Soul_SeIds[1], (DisableNotifyMonoBehaviour) this.owner);
    if (this.pairSwordsInfo.Soul_SeIds.Length >= 3 && this.pairSwordsInfo.Soul_SeIds[2] > 0)
      SoundManager.PlayOneShotSE(this.pairSwordsInfo.Soul_SeIds[2], this.owner._position);
    this.isEventShotLaserExec = false;
    this.isSetGaugePercentForLaser = false;
    this.gaugePercentForLaser = 0.0f;
    this.owner.EndWaitingPacket(StageObject.WAITING_PACKET.PLAYER_PAIR_SWORDS_LASER_END);
    if (Object.op_Inequality((Object) this.owner.playerSender, (Object) null))
      this.owner.playerSender.OnPairSwordsLaserEnd();
    if (!isPacket)
      return;
    this.owner.SetNextTrigger();
    this.isExecLaserEnd = true;
  }

  private void ClearLaserBullet()
  {
    if (this.bulletLaserList.IsNullOrEmpty<AnimEventShot>())
      return;
    for (int index = 0; index < this.bulletLaserList.Count; ++index)
      this.bulletLaserList[index].OnDestroy();
    this.bulletLaserList.Clear();
  }

  public void DecreaseSoulGaugeByDamage()
  {
    if (!this.owner.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL) || this.owner.IsInBarrier() || this.owner.IsStone())
      return;
    this.spActionGauge -= this.pairSwordsInfo.Soul_GaugeDecreaseByDamage;
    if ((double) this.spActionGauge >= 0.0)
      return;
    this.spActionGauge = 0.0f;
  }

  private float GetGaugeChargedPercent()
  {
    return (double) this.spActionGaugeMax <= 0.0 ? 0.0f : Mathf.Clamp01(this.spActionGauge / this.spActionGaugeMax) * 100f;
  }

  private float GetGaugeRate()
  {
    return (double) this.spActionGaugeMax <= 0.0 ? 0.0f : Mathf.Clamp01(this.spActionGauge / this.spActionGaugeMax);
  }

  public int GetComboLv()
  {
    float gaugeChargedPercent = this.GetGaugeChargedPercent();
    int comboLv = 1;
    for (int index = 0; index < this.pairSwordsInfo.Soul_GaugePercentForComboLv.Length; ++index)
    {
      if ((double) gaugeChargedPercent >= (double) this.pairSwordsInfo.Soul_GaugePercentForComboLv[index])
        comboLv = 1 + index;
    }
    if (this.comboLvBySync > 0)
    {
      comboLv = this.comboLvBySync;
      this.comboLvBySync = 0;
    }
    return comboLv;
  }

  public void SetComboLv(int lv) => this.comboLvBySync = lv;

  public float GetAttackSpeedUpRate()
  {
    switch (this.owner.spAttackType)
    {
      case SP_ATTACK_TYPE.HEAT:
        return this.GetHeatAttackSpeedUpRate();
      case SP_ATTACK_TYPE.SOUL:
        return this.GetSoulAttackSpeedUpRate();
      case SP_ATTACK_TYPE.BURST:
        return this.GetBurstAttackSpeedUpRate();
      default:
        return 0.0f;
    }
  }

  private float GetHeatAttackSpeedUpRate()
  {
    return !this.owner.isBoostMode || this.owner.attackID == 98 ? 0.0f : this.pairSwordsInfo.boostAttackAndMoveSpeedUpRate;
  }

  private float GetSoulAttackSpeedUpRate()
  {
    if (!this.owner.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL) || this.pairSwordsInfo.Soul_GaugePercentForComboLv.Length < this.pairSwordsInfo.Soul_NumOfComboLv || this.pairSwordsInfo.Soul_AttackSpeedUpRatesByComboLv.Length < this.pairSwordsInfo.Soul_NumOfComboLv)
      return 0.0f;
    float gaugeChargedPercent = this.GetGaugeChargedPercent();
    float attackSpeedUpRate = this.pairSwordsInfo.Soul_AttackSpeedUpRatesByComboLv[0];
    for (int index = 0; index < this.pairSwordsInfo.Soul_GaugePercentForComboLv.Length; ++index)
    {
      if ((double) gaugeChargedPercent >= (double) this.pairSwordsInfo.Soul_GaugePercentForComboLv[index])
        attackSpeedUpRate = this.pairSwordsInfo.Soul_AttackSpeedUpRatesByComboLv[index];
    }
    return attackSpeedUpRate;
  }

  private float GetBurstAttackSpeedUpRate()
  {
    return !this.owner.isBoostMode || !this.IsBurstGroundSpecialAttack() ? 0.0f : this.pairSwordsInfo.Burst_BoostAttackSpeedUpRate;
  }

  public bool GetElementDamageUpRate(ref float rate)
  {
    bool elementDamageUpRate = false;
    rate = 1f;
    if (this.owner.attackMode != Player.ATTACK_MODE.PAIR_SWORDS || this.owner.spAttackType != SP_ATTACK_TYPE.BURST)
      return elementDamageUpRate;
    if (this.isCombineMode)
    {
      rate += this.pairSwordsInfo.Burst_CombineElementDamageUpRate;
      elementDamageUpRate = true;
    }
    if (this.owner.isBoostMode && this.IsBurstGroundSpecialAttack())
    {
      rate += this.pairSwordsInfo.Burst_BoostGroundSpElementDamageUpRate;
      elementDamageUpRate = true;
    }
    return elementDamageUpRate;
  }

  public float GetAtkRate()
  {
    if (!this.owner.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.SOUL) || this.pairSwordsInfo.Soul_NumOfComboLv <= 0 || this.pairSwordsInfo.Soul_GaugePercentForComboLv.Length < this.pairSwordsInfo.Soul_NumOfComboLv)
      return 1f;
    float num = this.GetGaugeChargedPercent();
    if (this.owner.attackID == this.pairSwordsInfo.Soul_SpLaserShotAttackId)
    {
      if (this.pairSwordsInfo.Soul_AtkRatesForLaserByComboLv.Length < this.pairSwordsInfo.Soul_NumOfComboLv)
        return 1f;
      if (this.isSetGaugePercentForLaser)
      {
        num = this.gaugePercentForLaser;
      }
      else
      {
        this.gaugePercentForLaser = num;
        this.isSetGaugePercentForLaser = true;
      }
      float atkRate = this.pairSwordsInfo.Soul_AtkRatesForLaserByComboLv[0];
      for (int index = 0; index < this.pairSwordsInfo.Soul_GaugePercentForComboLv.Length; ++index)
      {
        if ((double) num >= (double) this.pairSwordsInfo.Soul_GaugePercentForComboLv[index])
          atkRate = this.pairSwordsInfo.Soul_AtkRatesForLaserByComboLv[index];
      }
      return atkRate;
    }
    if (this.pairSwordsInfo.Soul_AtkRatesForBulletByComboLv.Length < this.pairSwordsInfo.Soul_NumOfComboLv)
      return 1f;
    float atkRate1 = this.pairSwordsInfo.Soul_AtkRatesForBulletByComboLv[0];
    for (int index = 0; index < this.pairSwordsInfo.Soul_GaugePercentForComboLv.Length; ++index)
    {
      if ((double) num >= (double) this.pairSwordsInfo.Soul_GaugePercentForComboLv[index])
        atkRate1 = this.pairSwordsInfo.Soul_AtkRatesForBulletByComboLv[index];
    }
    return atkRate1;
  }

  private bool IsAbleToShotSoulLaser()
  {
    return this.chargeState == PairSwordsController.CHARGE_STATE.LOOP;
  }

  public bool IsAbleToAlterSpAction()
  {
    return !this.owner.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.HEAT) || !this.owner.isBoostMode && this.owner.IsSpActionGaugeHalfCharged();
  }

  public bool IsComboLvMax() => this.GetComboLv() == this.pairSwordsInfo.Soul_NumOfComboLv;

  public bool CheckContinueBoostMode()
  {
    switch (this.owner.spAttackType)
    {
      case SP_ATTACK_TYPE.HEAT:
        if ((double) this.spActionGauge > 0.0)
          return true;
        break;
      case SP_ATTACK_TYPE.SOUL:
        if (!this.owner.IsCoopNone() && !this.owner.IsOriginal() || this.IsComboLvMax())
          return true;
        break;
      case SP_ATTACK_TYPE.BURST:
        if (!this.owner.IsCoopNone() && !this.owner.IsOriginal() || (double) this.spActionGauge > 0.0)
          return true;
        break;
    }
    return false;
  }

  public void OnHit() => this.ResetTimerForSpActionGaugeDecreaseAfterHit();

  public void OnEndAction()
  {
    this.hasJustAvoid = false;
    this.EndOracleSp();
    if (Object.op_Inequality((Object) this.oracleRushLoopEffect, (Object) null))
    {
      if (this.owner is Self)
        EffectManager.ReleaseEffect(((Component) this.oracleRushLoopEffect).gameObject);
      else
        EffectManager.ReleaseEffect(((Component) this.oracleRushLoopEffect).gameObject, false, true);
      this.oracleRushLoopEffect = (Transform) null;
    }
    if (!Object.op_Inequality((Object) this.oracleRushEffect, (Object) null))
      return;
    if (this.owner is Self)
      EffectManager.ReleaseEffect(((Component) this.oracleRushEffect).gameObject);
    else
      EffectManager.ReleaseEffect(((Component) this.oracleRushEffect).gameObject, false, true);
    this.oracleRushEffect = (Transform) null;
  }

  public void OnActDead() => this.OnReaction();

  public void OnActReaction()
  {
  }

  public void OnActAvoid() => this.OnAvoid();

  public void OnActSkillAction()
  {
  }

  public void OnRelease()
  {
    if (!this.isCtrlActive || this.owner.isDead || !this.IsAbleToShotSoulLaser() && (this.owner.attackID != this.pairSwordsInfo.Soul_SpLaserWaitAttackId || this.chargeState != PairSwordsController.CHARGE_STATE.NONE))
      return;
    this.owner.ActAttack(this.pairSwordsInfo.Soul_SpLaserShotAttackId, true, true, "", "");
    this.SetChargeState(PairSwordsController.CHARGE_STATE.LASER_SHOT);
    if (!Object.op_Inequality((Object) this.owner.playerSender, (Object) null))
      return;
    this.owner.playerSender.OnSyncSpActionGauge();
  }

  public void OnAvoid()
  {
    if (this.chargeState == PairSwordsController.CHARGE_STATE.NONE)
      return;
    this.SetChargeState(PairSwordsController.CHARGE_STATE.END);
  }

  public void OnReaction()
  {
    if (this.chargeState == PairSwordsController.CHARGE_STATE.NONE)
      return;
    this.SetChargeState(PairSwordsController.CHARGE_STATE.END);
  }

  public void OnActAttack(int id)
  {
  }

  public void OnBuffStart(BuffParam.BuffData data)
  {
  }

  public void OnBuffEnd(BuffParam.BUFFTYPE type)
  {
  }

  public void OnChangeWeapon()
  {
  }

  public void OnAttackedHitFix(AttackedHitStatusFix status)
  {
  }

  public float GetWalkSpeedUp(SP_ATTACK_TYPE _spAtkType)
  {
    switch (_spAtkType)
    {
      case SP_ATTACK_TYPE.HEAT:
        if (this.owner.isBoostMode)
          return this.pairSwordsInfo.boostAttackAndMoveSpeedUpRate;
        break;
      case SP_ATTACK_TYPE.BURST:
        return this.pairSwordsInfo.Burst_MoveSpeedUpRate;
    }
    return 0.0f;
  }

  public bool IsUpdateAerialCollider()
  {
    return this.pairSwordsInfo != null && this.pairSwordsInfo.Burst_IsUpdateAerialCollider;
  }

  public void ResetCombineMode() => this.isCombineMode = false;

  public bool IsCombineMode() => this.isCombineMode;

  public bool IsBurstGroundSpecialAttack()
  {
    return this.owner.attackID >= 36 && this.owner.attackID <= 39;
  }

  public int GetBurstAttackId() => !this.isCombineMode ? 30 : 33;

  public bool IsOverrideHitEffect(ref EnemyHitTypeTable.TypeData _type, ref Vector3 _scale)
  {
    if (!this.owner.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.BURST) || !this.isCombineMode || ((IList<string>) this.pairSwordsInfo.Burst_CombineHitEffect).IsNullOrEmpty<string>())
      return false;
    _type = new EnemyHitTypeTable.TypeData();
    for (int index = 0; index < this.pairSwordsInfo.Burst_CombineHitEffect.Length; ++index)
      _type.elementEffectNames[index] = this.pairSwordsInfo.Burst_CombineHitEffect[index];
    _scale = this.pairSwordsInfo.Burst_CombineHitEffectScale;
    return true;
  }

  public bool ActBurstSpecialAction(ref bool effectFlag)
  {
    if (this.owner.attackID == 32 /*0x20*/)
    {
      this.owner.ActAttack(35, true, true, "", "");
      effectFlag = false;
      return true;
    }
    if (!this.isCombineMode)
    {
      this.owner.ActAttack(34, true, false, "", "");
      effectFlag = true;
      return true;
    }
    if (this.owner.actionID == Character.ACTION_ID.ATTACK && this.owner.attackID != 21 && this.owner.attackID != 34 && this.owner.attackID != 35)
      return false;
    this.owner.ActAttack(36, true, false, "", this.owner.actionID != Character.ACTION_ID.ATTACK ? "attack_36_start" : "");
    effectFlag = false;
    return true;
  }

  public void CombineBurst(bool isCombine)
  {
    if (this.isCombineMode == isCombine || !this.owner.CheckAttackModeAndSpType(Player.ATTACK_MODE.PAIR_SWORDS, SP_ATTACK_TYPE.BURST))
      return;
    this.isCombineMode = isCombine;
    if (this.isCombineMode)
    {
      this.owner.loader.CombineBurstPairSword(true, this.pairSwordsInfo.Burst_CombinePosition, Quaternion.Euler(this.pairSwordsInfo.Burst_CombineEuler));
      this.owner.CheckBurstPairSwordBoost();
    }
    else
      this.owner.loader.CombineBurstPairSword(false, Vector3.zero, Quaternion.identity);
    SoundManager.PlayOneShotSE(10000042, this.owner._position);
    if (Object.op_Inequality((Object) this.owner.loader, (Object) null) && Object.op_Inequality((Object) this.owner.loader.wepR, (Object) null))
      EffectManager.OneShot("ef_btl_wsk3_twinsword_01_00", this.owner.loader.wepR.position, this.combineEffectRotation, this.combineEffectScale);
    if (!Object.op_Inequality((Object) this.owner.playerSender, (Object) null))
      return;
    this.owner.playerSender.OnSyncCombine(this.isCombineMode);
  }

  public void ActAerialAvoid(Vector3 inputVec)
  {
    Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
    Vector3 right = cameraTransform.right;
    Vector3 forward = cameraTransform.forward;
    forward.y = 0.0f;
    ((Vector3) ref forward).Normalize();
    this.owner._transform.LookAt(Vector3.op_Addition(this.owner._transform.position, Vector3.op_Addition(Vector3.op_Multiply(right, inputVec.x), Vector3.op_Multiply(forward, inputVec.y))));
  }

  public bool IsOracleAttackId(int id) => 40 <= id && id <= 45;

  public void EventShotOracleRush(AnimEventData.EventData data)
  {
    this.spActionGauge -= this.pairSwordsInfo.Oracle_SpGaugeRushDecrease * (1f + this.owner.GetSpGaugeDecreasingRate());
    if (this.owner is Self)
    {
      if (Object.op_Inequality((Object) this.oracleRushLoopEffect, (Object) null))
      {
        EffectManager.ReleaseEffect(((Component) this.oracleRushLoopEffect).gameObject);
        this.oracleRushLoopEffect = (Transform) null;
      }
      this.oracleRushLoopEffect = EffectManager.GetEffect("ef_btl_wsk4_twinsword_03", this.owner._transform);
      AttackInfo attackInfo;
      if ((double) this.spActionGauge < 0.0)
      {
        this.spActionGauge = 0.0f;
        attackInfo = this.owner.FindAttackInfo(data.stringArgs[0]);
      }
      else
        attackInfo = this.owner.FindAttackInfo(data.stringArgs[2]);
      if (attackInfo == null)
        return;
      this.oracleRushEffect = AnimEventShot.Create((StageObject) this.owner, data, attackInfo, Vector3.zero).bulletEffect;
      if (!Object.op_Inequality((Object) this.oracleRushEffect, (Object) null))
        return;
      ((Component) this.oracleRushEffect).transform.localRotation = Quaternion.Euler(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]);
    }
    else
    {
      string effect_name = (double) this.spActionGauge >= 0.0 ? $"ef_btl_wsk4_twinsword_01_{this.owner.GetCurrentWeaponElement():D2}" : "ef_btl_wsk4_twinsword_01";
      if (Object.op_Inequality((Object) this.oracleRushEffect, (Object) null))
      {
        EffectManager.ReleaseEffect(((Component) this.oracleRushEffect).gameObject, false, true);
        this.oracleRushEffect = (Transform) null;
      }
      this.oracleRushEffect = EffectManager.GetEffect(effect_name);
      Vector3 vector3_1;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3_1).\u002Ector(data.floatArgs[0], data.floatArgs[1], data.floatArgs[2]);
      Transform oracleRushEffect = this.oracleRushEffect;
      Matrix4x4 localToWorldMatrix = this.owner._transform.localToWorldMatrix;
      Vector3 vector3_2 = ((Matrix4x4) ref localToWorldMatrix).MultiplyPoint3x4(vector3_1);
      oracleRushEffect.position = vector3_2;
      this.oracleRushEffect.rotation = Quaternion.op_Multiply(this.owner._rotation, Quaternion.Euler(data.floatArgs[3], data.floatArgs[4], data.floatArgs[5]));
    }
  }

  public void JustAvoid(bool force = false)
  {
    if (!(!this.hasJustAvoid | force))
      return;
    EffectManager.OneShot("ef_btl_wsk4_twinsword_04", this.owner._position, this.owner._rotation);
    this.spActionGauge = Mathf.Max(0.0f, Mathf.Min(this.spActionGaugeMax, this.spActionGauge + (float) ((double) this.spActionGaugeMax * (double) this.pairSwordsInfo.Oracle_JustAvoidGaugeIncreaseRate * (1.0 + (double) this.owner.buffParam.GetGaugeIncreaseRate(SP_ATTACK_TYPE.ORACLE)))));
    this.hasJustAvoid = true;
    Enemy actionTarget = this.owner.actionTarget as Enemy;
    if (Object.op_Inequality((Object) actionTarget, (Object) null))
    {
      this.owner.SetHitStop(this.pairSwordsInfo.Oracle_JustAvoidMotionStopTime);
      actionTarget.SetHitStop(this.pairSwordsInfo.Oracle_JustAvoidMotionStopTime);
    }
    if (!this.owner.IsCoopNone() && !this.owner.IsOriginal())
      return;
    Self owner = this.owner as Self;
    if (!Object.op_Inequality((Object) owner, (Object) null) || !MonoBehaviourSingleton<InGameManager>.IsValid())
      return;
    owner.taskChecker.OnOraclePairSwords();
    MonoBehaviourSingleton<InGameManager>.I.deliveryBattleChecker.OnOraclePairSwords();
  }

  public void StartOracleSp(AnimEventData.EventData data)
  {
    this.owner.enabledOraclePairSwordsSP = true;
    this.owner.actionMoveRotateMaxSpeedRate = data.floatArgs[0];
    if ((double) this.owner.moveRotateMaxSpeed == 0.0)
      this.owner.actionMoveRotateMaxSpeedRate = 1f;
    if (!Object.op_Equality((Object) this.oracleSpEffect, (Object) null))
      return;
    this.oracleSpEffect = EffectManager.GetEffect($"ef_btl_wsk4_twinsword_05_{this.owner.GetCurrentWeaponElement():D2}", this.owner._transform);
  }

  public void EndOracleSp()
  {
    this.owner.enabledOraclePairSwordsSP = false;
    this.owner.actionMoveRotateMaxSpeedRate = 1f;
    if (!Object.op_Inequality((Object) this.oracleSpEffect, (Object) null))
      return;
    if (this.owner is Self)
      EffectManager.ReleaseEffect(((Component) this.oracleSpEffect).gameObject);
    else
      EffectManager.ReleaseEffect(((Component) this.oracleSpEffect).gameObject, false, true);
    this.oracleSpEffect = (Transform) null;
  }

  public enum CHARGE_STATE
  {
    NONE,
    LOOP,
    LASER_SHOT,
    LASER_LOOP,
    END,
  }
}
