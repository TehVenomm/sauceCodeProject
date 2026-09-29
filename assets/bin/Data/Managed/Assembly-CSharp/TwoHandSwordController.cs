// Decompiled with JetBrains decompiler
// Type: TwoHandSwordController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class TwoHandSwordController : IObserver, IWeaponController
{
  public const int TWOHANDSWORD_AVOID_ATTACKID = 20;
  public const float THS_HEAT_GAUGE_MAX = 999f;
  public const float THS_HEAT_GAUGE_UNIT = 333f;
  public const int TWOHANDSWORD_NORMAL_SP_ATTACK_ID = 97;
  public const int TWOHANDSWORD_RUSHATTACK_ID = 98;
  public const int TWOHANDSWORD_HEAT_COMBO1_ID = 89;
  public const int TWOHANDSWORD_HEAT_COMBO2_ID = 88;
  private const int INVALID_CTRL_INDEX = -1;
  public const int MAX_BURST_BULLET_COUNT = 6;
  private Player m_owner;
  private InGameSettingsManager.Player.TwoHandSwordActionInfo m_actionInfo;
  private IWeaponController[] m_thsCtrls = new IWeaponController[5];
  private TwoHandSwordBurstController m_thsNormalCtrl;
  private TwoHandSwordBurstController m_thsHeatCtrl;
  private TwoHandSwordSoulController m_thsSoulCtrl;
  private TwoHandSwordBurstController m_thsBurstCtrl;
  private TwoHandSwordOracleController m_thsOracleCtrl;

  public TwoHandSwordOracleController oracleCtrl => this.m_thsOracleCtrl;

  public TwoHandSwordController()
  {
    this.m_owner = (Player) null;
    this.m_actionInfo = (InGameSettingsManager.Player.TwoHandSwordActionInfo) null;
    this.m_thsSoulCtrl = new TwoHandSwordSoulController();
    this.m_thsCtrls[2] = (IWeaponController) this.m_thsSoulCtrl;
    this.m_thsBurstCtrl = new TwoHandSwordBurstController();
    this.m_thsCtrls[3] = (IWeaponController) this.m_thsBurstCtrl;
    this.m_thsOracleCtrl = new TwoHandSwordOracleController();
    this.m_thsCtrls[4] = (IWeaponController) this.m_thsOracleCtrl;
  }

  public TwoHandSwordController(TwoHandSwordController.InitParam _param) => this.InitAppend(_param);

  public void Init(Player _player)
  {
    this.m_owner = _player;
    for (int index = 0; index < this.m_thsCtrls.Length; ++index)
    {
      if (this.m_thsCtrls[index] != null)
        this.m_thsCtrls[index].Init(_player);
    }
    if (!Object.op_Inequality((Object) this.m_owner, (Object) null) || this.m_owner.playerParameter == null)
      return;
    this.m_actionInfo = this.m_owner.playerParameter.twoHandSwordActionInfo;
  }

  public void InitAppend(TwoHandSwordController.InitParam _param)
  {
    this.Init(_param.Owner);
    this.m_thsBurstCtrl.Init(_param.BurstInitParam);
  }

  public void OnHit()
  {
  }

  public void OnLoadComplete()
  {
  }

  public void Update()
  {
    int validControllerIndex = this.GetValidControllerIndex();
    if (validControllerIndex < 0)
      return;
    this.m_thsCtrls[validControllerIndex].Update();
  }

  public void OnEndAction()
  {
    int validControllerIndex = this.GetValidControllerIndex();
    if (validControllerIndex < 0)
      return;
    this.m_thsCtrls[validControllerIndex].OnEndAction();
  }

  public void OnActDead()
  {
  }

  public void OnActReaction()
  {
  }

  public void OnActAvoid()
  {
    int validControllerIndex = this.GetValidControllerIndex();
    if (validControllerIndex < 0)
      return;
    this.m_thsCtrls[validControllerIndex].OnActAvoid();
  }

  public void OnActSkillAction()
  {
  }

  public void OnRelease()
  {
    int validControllerIndex = this.GetValidControllerIndex();
    if (validControllerIndex < 0)
      return;
    this.m_thsCtrls[validControllerIndex].OnRelease();
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

  private int GetValidControllerIndex()
  {
    if (Object.op_Equality((Object) this.m_owner, (Object) null) || this.m_owner.attackMode != Player.ATTACK_MODE.TWO_HAND_SWORD)
      return -1;
    int spAttackType = (int) this.m_owner.spAttackType;
    return this.m_thsCtrls == null || spAttackType < 0 || this.m_thsCtrls.Length <= spAttackType || this.m_thsCtrls[spAttackType] == null ? -1 : spAttackType;
  }

  public bool GetNormalAttackId(
    SP_ATTACK_TYPE _spAtktype,
    EXTRA_ATTACK_TYPE _exAtkType,
    ref int _attackId,
    ref string _motionLayerName)
  {
    if (Object.op_Equality((Object) this.m_owner, (Object) null) || this.m_actionInfo == null)
      return false;
    switch (_spAtktype)
    {
      case SP_ATTACK_TYPE.SOUL:
        _attackId = _exAtkType != EXTRA_ATTACK_TYPE.LONG ? 10 : this.m_owner.playerParameter.twoHandSwordActionInfo.Soul_LongAttackId;
        break;
      case SP_ATTACK_TYPE.BURST:
        _attackId = this.m_actionInfo.burstTHSInfo.BaseAtkId;
        _motionLayerName = this.m_owner.GetMotionLayerName(Player.ATTACK_MODE.TWO_HAND_SWORD, _spAtktype, _attackId);
        break;
      case SP_ATTACK_TYPE.ORACLE:
        _attackId = 60;
        _motionLayerName = this.m_owner.GetMotionLayerName(Player.ATTACK_MODE.TWO_HAND_SWORD, _spAtktype, _attackId);
        break;
    }
    return true;
  }

  public bool GetSpActionInfo(
    SP_ATTACK_TYPE _spAtkType,
    EXTRA_ATTACK_TYPE _exType,
    ref int _attackId,
    ref string _motionLayerName)
  {
    if (Object.op_Equality((Object) this.m_owner, (Object) null))
      return false;
    switch (_spAtkType)
    {
      case SP_ATTACK_TYPE.NONE:
        _attackId = 97;
        break;
      case SP_ATTACK_TYPE.HEAT:
        _attackId = 98;
        break;
      case SP_ATTACK_TYPE.SOUL:
        _attackId = _exType != EXTRA_ATTACK_TYPE.LONG ? 96 /*0x60*/ : this.m_owner.playerParameter.twoHandSwordActionInfo.Soul_LongSpAttackId;
        break;
      case SP_ATTACK_TYPE.BURST:
        if (this.m_actionInfo != null)
        {
          _attackId = this.m_actionInfo.burstTHSInfo.ReadyForShotID;
          _motionLayerName = this.m_owner.GetMotionLayerName(Player.ATTACK_MODE.TWO_HAND_SWORD, _spAtkType, _attackId);
          break;
        }
        break;
      case SP_ATTACK_TYPE.ORACLE:
        _attackId = 63 /*0x3F*/;
        _motionLayerName = this.m_owner.GetMotionLayerName(Player.ATTACK_MODE.TWO_HAND_SWORD, _spAtkType, _attackId);
        break;
    }
    return true;
  }

  public bool GetSpGaugeIncreaseValue(
    AttackHitInfo.ATTACK_TYPE _atkType,
    int _attackId,
    float _attackRate,
    SoulEnergyController _soulEnergyCtrl,
    Vector3 _hitPosition,
    float _chargeRate,
    ref float _gaugeMax,
    ref float _increaseValue)
  {
    if (Object.op_Equality((Object) this.m_owner, (Object) null))
      return false;
    SP_ATTACK_TYPE spAttackType = this.m_owner.spAttackType;
    switch (spAttackType)
    {
      case SP_ATTACK_TYPE.HEAT:
      case SP_ATTACK_TYPE.SOUL:
        if (this.m_actionInfo == null)
          return false;
        if (this.m_owner.spAttackType == SP_ATTACK_TYPE.HEAT)
        {
          if (_atkType == AttackHitInfo.ATTACK_TYPE.THS_HEAT_COMBO)
            return false;
          _gaugeMax = 999f;
          _increaseValue = this.m_actionInfo.heatGaugeIncreaseBase * _attackRate;
          return true;
        }
        if (spAttackType != SP_ATTACK_TYPE.SOUL)
          return true;
        if (_soulEnergyCtrl == null)
          return false;
        SoulEnergy soulEnergy;
        if (this.IsSoulComboFinishAttackId(_attackId))
        {
          soulEnergy = _soulEnergyCtrl.Get(this.m_actionInfo.soulComboGaugeIncreaseValue);
        }
        else
        {
          if (!this.IsSoulSpAttackId(_attackId))
            return false;
          soulEnergy = _soulEnergyCtrl.Get(this.GetIaiGaugeIncreaseValue(_chargeRate));
        }
        if (soulEnergy != null)
        {
          if (MonoBehaviourSingleton<UIPlayerStatus>.IsValid())
            MonoBehaviourSingleton<UIPlayerStatus>.I.DirectionSoulGauge(soulEnergy, _hitPosition);
          if (MonoBehaviourSingleton<UIEnduranceStatus>.IsValid())
            MonoBehaviourSingleton<UIEnduranceStatus>.I.DirectionSoulGauge(soulEnergy, _hitPosition);
        }
        return false;
      default:
        return false;
    }
  }

  public bool GetActAttackComboParam(ref int _attackId, ref string _motionLayerName)
  {
    return this.m_owner.spAttackType != SP_ATTACK_TYPE.BURST || this.m_thsBurstCtrl.GetComboAttackParam(ref _attackId, ref _motionLayerName);
  }

  public float GetAtkRate(Enemy _enemy, Player _player)
  {
    return Object.op_Equality((Object) _enemy, (Object) null) || Object.op_Equality((Object) _player, (Object) null) || this.m_owner.attackMode != Player.ATTACK_MODE.TWO_HAND_SWORD || _player.spAttackType != SP_ATTACK_TYPE.BURST ? 1f : this.m_thsBurstCtrl.GetAtkRate(_enemy, _player);
  }

  public bool IsTwoHandSwordSpAttackContinueTimeOut(float _timer)
  {
    return this.m_actionInfo == null || (double) _timer > (double) this.m_actionInfo.timeSpAttackContinueInput;
  }

  public float GetWalkSpeedUp(SP_ATTACK_TYPE _spAtkType)
  {
    return _spAtkType == SP_ATTACK_TYPE.SOUL ? this.m_actionInfo.soulWalkSpeed : 0.0f;
  }

  public bool IsEnableChangeActionByLongTap()
  {
    switch (this.m_owner.spAttackType)
    {
      case SP_ATTACK_TYPE.BURST:
        return this.m_thsBurstCtrl != null && this.m_thsBurstCtrl.IsEnableChangeActionByLongTap();
      case SP_ATTACK_TYPE.ORACLE:
        return this.m_thsOracleCtrl != null && this.m_thsOracleCtrl.IsEnableChangeActionByLongTap();
      default:
        return false;
    }
  }

  public float TwoHandSwordBoostAttackSpeed
  {
    get => this.m_thsSoulCtrl != null ? this.m_thsSoulCtrl.TwoHandSwordBoostAttackSpeed : 0.0f;
  }

  public void SetTwoHandSwordBoostAttackSpeed(float _speed)
  {
    if (this.m_thsSoulCtrl == null)
      return;
    this.m_thsSoulCtrl.SetTwoHandSwordBoostAttackSpeed(_speed);
  }

  public void SetSoulChargeRelease()
  {
    if (this.m_thsSoulCtrl == null)
      return;
    this.m_thsSoulCtrl.SetChargeRelease();
  }

  public bool GetIaiNormalDamageUp(ref float value, int attackID, float chargeRate)
  {
    return this.m_thsSoulCtrl != null && this.m_thsSoulCtrl.GetIaiNormalDamageUp(ref value, attackID, chargeRate);
  }

  public float GetIaiGaugeIncreaseValue(float chargeRate)
  {
    return this.m_thsSoulCtrl == null ? 0.0f : this.m_thsSoulCtrl.GetIaiGaugeIncreaseValue(chargeRate);
  }

  public bool IsActiveIai() => this.m_thsSoulCtrl != null && this.m_thsSoulCtrl.IsActiveIai();

  public bool IsSoulComboFinishAttackId(int attackId)
  {
    return this.m_thsSoulCtrl != null && this.m_thsSoulCtrl.IsComboFinishAttackId(attackId);
  }

  public bool IsSoulSpAttackId(int attackId)
  {
    return this.m_thsSoulCtrl != null && this.m_thsSoulCtrl.IsSpAttackId(attackId);
  }

  public int[] GetAllCurrentRestBulletCount
  {
    get
    {
      return this.m_thsBurstCtrl != null ? this.m_thsBurstCtrl.GetAllCurrentRestBulletCount : (int[]) null;
    }
  }

  public int FirstReloadActionAtkID
  {
    get => this.m_thsBurstCtrl != null ? this.m_thsBurstCtrl.FirstReloadActionAtkID : -1;
  }

  public int CurrentRestBulletCount
  {
    get => this.m_thsBurstCtrl != null ? this.m_thsBurstCtrl.CurrentRestBulletCount : 0;
  }

  public int CurrentMaxBulletCount
  {
    get => this.m_thsBurstCtrl != null ? this.m_thsBurstCtrl.CurrentMaxBulletCount : 0;
  }

  public bool IsReadyForShoot => this.m_thsBurstCtrl != null && this.m_thsBurstCtrl.IsReadyForShoot;

  public void SetReadyForShoot(bool _isReady)
  {
    if (this.m_thsBurstCtrl == null)
      return;
    this.m_thsBurstCtrl.SetReadyForShoot(_isReady);
  }

  public bool IsShootingNow => this.m_thsBurstCtrl != null && this.m_thsBurstCtrl.IsShootingNow;

  public void SetStartShooting(bool _isReady)
  {
    if (this.m_thsBurstCtrl == null)
      return;
    this.m_thsBurstCtrl.SetStartShooting(_isReady);
  }

  public bool IsReloadingNow => this.m_thsBurstCtrl != null && this.m_thsBurstCtrl.IsReloadingNow;

  public void SetStartReloading(bool _isReadyForReload)
  {
    if (this.m_thsBurstCtrl == null)
      return;
    this.m_thsBurstCtrl.SetStartReloading(_isReadyForReload);
  }

  public bool IsHit3rdComboAttack
  {
    get => this.m_thsBurstCtrl != null && this.m_thsBurstCtrl.IsHit3rdComboAttack;
  }

  public void Set3rdComboAttackHitFlag(bool _isHit)
  {
    if (this.m_thsBurstCtrl == null)
      return;
    this.m_thsBurstCtrl.Set3rdComboAttackHitFlag(_isHit);
  }

  public bool IsHitAvoidAttack
  {
    get => this.m_thsBurstCtrl != null && this.m_thsBurstCtrl.IsHitAvoidAttack;
  }

  public void SetIsHitAvoidAttack(bool _isHit)
  {
    if (this.m_thsBurstCtrl == null)
      return;
    this.m_thsBurstCtrl.SetIsHitAvoidAttack(_isHit);
  }

  public bool IsEnableChangeReloadMotionSpeed
  {
    get => this.m_thsBurstCtrl != null && this.m_thsBurstCtrl.IsEnableChangeReloadMotionSpeed;
  }

  public void SetChangeReloadMotionSpeedFlag(bool _isEnable)
  {
    if (this.m_thsBurstCtrl == null)
      return;
    this.m_thsBurstCtrl.SetChangeReloadMotionSpeedFlag(_isEnable);
  }

  public bool IsEnableTransitionFromAvoidAtk
  {
    get => this.m_thsBurstCtrl != null && this.m_thsBurstCtrl.IsEnableTransitionFromAvoidAtk;
  }

  public void SetEnableTransitionFromAvoidAtkFlag(bool _isEnable)
  {
    if (this.m_thsBurstCtrl == null)
      return;
    this.m_thsBurstCtrl.SetEnableTransitionFromAvoidAtkFlag(_isEnable);
  }

  public bool GetBurstShotNormalDamageUpRate(AttackHitInfo.ATTACK_TYPE _type, ref float _value)
  {
    return this.m_thsBurstCtrl != null && this.m_thsBurstCtrl.GetBurstShotNormalDamageUpRate(_type, ref _value);
  }

  public bool GetBurstShotElementDamageUpRate(AttackHitInfo.ATTACK_TYPE _type, ref float _value)
  {
    return this.m_thsBurstCtrl != null && this.m_thsBurstCtrl.GetBurstShotElementDamageUpRate(_type, ref _value);
  }

  public bool IsRequiredReloadAction()
  {
    return this.m_thsBurstCtrl != null && this.m_thsBurstCtrl.IsRequiredReloadAction();
  }

  public bool IsEnableReloadAction()
  {
    return this.m_thsBurstCtrl != null && this.m_thsBurstCtrl.IsEnableReloadAction();
  }

  public bool IsEnableShootAction()
  {
    return this.m_thsBurstCtrl != null && this.m_thsBurstCtrl.IsEnableShootAction();
  }

  public bool DoShootAction() => this.m_thsBurstCtrl != null && this.m_thsBurstCtrl.DoShootAction();

  public bool DoReloadAction()
  {
    return this.m_thsBurstCtrl != null && this.m_thsBurstCtrl.DoReloadAction();
  }

  public bool TryFirstReloadAction()
  {
    return this.m_thsBurstCtrl != null && this.m_thsBurstCtrl.TryFirstReloadAction();
  }

  public bool IsChangebleReloadAction()
  {
    return this.m_thsBurstCtrl != null && this.m_thsBurstCtrl.IsChangebleReloadAction();
  }

  public bool CheckEnableNextReloadAction()
  {
    return this.m_thsBurstCtrl != null && this.m_thsBurstCtrl.CheckEnableNextReloadAction();
  }

  public bool DoFullBurst() => this.m_thsBurstCtrl != null && this.m_thsBurstCtrl.DoFullBurst();

  public class InitParam
  {
    public Player Owner;
    public TwoHandSwordBurstController.InitParam BurstInitParam;
  }
}
