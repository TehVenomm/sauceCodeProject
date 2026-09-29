// Decompiled with JetBrains decompiler
// Type: TwoHandSwordBurstController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class TwoHandSwordBurstController : IWeaponController
{
  public const int MAX_BURST_BULLET_COUNT = 6;
  private Player m_owner;
  private InGameSettingsManager.Player.BurstTwoHandSwordActionInfo m_burstActionInfo;
  private int[] m_currentRestBulletCount;
  private int[] m_currentMaxBulletCount;
  private bool m_isReadyForShoot;
  private bool m_isShootingNow;
  private bool m_isReloadingNow;
  private bool m_isHit3rdComboAttack;
  private bool m_isHitAvoidAttack;
  private bool m_isEnableChangeReloadMotionSpeed;
  private bool m_isEnableTransitionFromAvoidAtk;

  public int FirstReloadActionAtkID
  {
    get => this.m_burstActionInfo != null ? this.m_burstActionInfo.FirstReloadActionAttackID : 0;
  }

  public int[] GetAllCurrentRestBulletCount => this.m_currentRestBulletCount;

  public int CurrentRestBulletCount
  {
    get
    {
      return Object.op_Equality((Object) this.m_owner, (Object) null) || this.m_currentRestBulletCount == null || this.m_owner.weaponIndex < 0 || this.m_owner.weaponIndex >= this.m_currentRestBulletCount.Length || this.m_currentRestBulletCount.Length < 0 ? 0 : this.m_currentRestBulletCount[this.m_owner.weaponIndex];
    }
  }

  public void SetCurrentRestBulletCount(int value)
  {
    if (value < 0 || Object.op_Equality((Object) this.m_owner, (Object) null) || this.m_currentRestBulletCount == null || this.m_owner.weaponIndex < 0 || this.m_owner.weaponIndex >= this.m_currentRestBulletCount.Length || this.m_currentRestBulletCount.Length < 0)
      return;
    this.m_currentRestBulletCount[this.m_owner.weaponIndex] = value;
  }

  public int CurrentMaxBulletCount
  {
    get
    {
      return Object.op_Equality((Object) this.m_owner, (Object) null) || this.m_currentMaxBulletCount == null || this.m_owner.weaponIndex < 0 || this.m_owner.weaponIndex >= this.m_currentMaxBulletCount.Length || this.m_currentMaxBulletCount.Length < 0 ? 0 : this.m_currentMaxBulletCount[this.m_owner.weaponIndex];
    }
  }

  public bool IsReadyForShoot => this.m_isReadyForShoot;

  public void SetReadyForShoot(bool _isReady) => this.m_isReadyForShoot = _isReady;

  public bool IsShootingNow => this.m_isShootingNow;

  public void SetStartShooting(bool _isReady) => this.m_isShootingNow = _isReady;

  public bool IsReloadingNow => this.m_isReloadingNow;

  public void SetStartReloading(bool _isReadyForReload)
  {
    this.m_isReloadingNow = _isReadyForReload;
  }

  public bool IsHit3rdComboAttack => this.m_isHit3rdComboAttack;

  public void Set3rdComboAttackHitFlag(bool _isHit) => this.m_isHit3rdComboAttack = _isHit;

  public bool IsHitAvoidAttack => this.m_isHitAvoidAttack;

  public void SetIsHitAvoidAttack(bool _isHit) => this.m_isHitAvoidAttack = _isHit;

  public bool IsEnableChangeReloadMotionSpeed => this.m_isEnableChangeReloadMotionSpeed;

  public void SetChangeReloadMotionSpeedFlag(bool _isEnable)
  {
    this.m_isEnableChangeReloadMotionSpeed = _isEnable;
  }

  public bool IsEnableTransitionFromAvoidAtk => this.m_isEnableTransitionFromAvoidAtk;

  public void SetEnableTransitionFromAvoidAtkFlag(bool _isEnable)
  {
    this.m_isEnableTransitionFromAvoidAtk = _isEnable;
  }

  public TwoHandSwordBurstController()
  {
    this.m_owner = (Player) null;
    this.m_burstActionInfo = (InGameSettingsManager.Player.BurstTwoHandSwordActionInfo) null;
    this.m_currentRestBulletCount = new int[3];
    this.m_currentMaxBulletCount = new int[3];
    for (int index = 0; index < 3; ++index)
    {
      this.m_currentRestBulletCount[index] = 0;
      this.m_currentMaxBulletCount[index] = 0;
    }
  }

  public void Init(Player _player) => this.m_owner = _player;

  public void Init(TwoHandSwordBurstController.InitParam _param)
  {
    if (Object.op_Inequality((Object) _param.Owner, (Object) null))
      this.m_owner = _param.Owner;
    this.m_burstActionInfo = _param.ActionInfo.burstTHSInfo;
    for (int index = 0; index < 3; ++index)
    {
      this.m_currentRestBulletCount[index] = this.IsSetCurrentBulletCountAsFull(_param, index) ? _param.MaxBulletCount : _param.CurrentRestBullets[index];
      this.m_currentMaxBulletCount[index] = _param.MaxBulletCount;
    }
  }

  private bool IsSetCurrentBulletCountAsFull(
    TwoHandSwordBurstController.InitParam _param,
    int index)
  {
    return _param.CurrentRestBullets == null || index < 0 || _param.CurrentRestBullets.Length <= index || _param.IsNeedFullBullet && this.m_currentMaxBulletCount[index] < _param.MaxBulletCount;
  }

  public void OnLoadComplete()
  {
  }

  public void Update()
  {
  }

  public void OnEndAction()
  {
    this.SetReadyForShoot(false);
    this.SetStartReloading(false);
    this.SetStartShooting(false);
    this.Set3rdComboAttackHitFlag(false);
    this.SetIsHitAvoidAttack(false);
    this.SetChangeReloadMotionSpeedFlag(false);
  }

  public void OnActDead()
  {
  }

  public void OnActReaction()
  {
  }

  public void OnActAvoid()
  {
  }

  public void OnActSkillAction()
  {
  }

  public void OnRelease()
  {
    int num = -1;
    string _motionLayerName = "";
    if (this.IsReadyForShoot)
    {
      this.SetReadyForShoot(false);
      if (this.IsRequiredReloadAction())
      {
        num = this.m_burstActionInfo.FirstReloadActionAttackID;
        _motionLayerName = this.m_owner.GetMotionLayerName(this.m_owner.attackMode, this.m_owner.spAttackType, num);
      }
      else if (this.IsEnableShootAction())
      {
        num = this.m_burstActionInfo.FirstShotAttackID;
        _motionLayerName = this.m_owner.GetMotionLayerName(this.m_owner.attackMode, this.m_owner.spAttackType, num);
        this.SetStartShooting(true);
      }
    }
    if (this.IsReloadingNow && this.IsEnableReloadAction())
    {
      num = this.m_burstActionInfo.NextReloadActionAttackID;
      _motionLayerName = this.m_owner.GetMotionLayerName(this.m_owner.attackMode, this.m_owner.spAttackType, num);
    }
    if (num == -1)
      return;
    this.m_owner.ActAttack(num, true, true, _motionLayerName, "");
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

  public bool GetActAttackComboParam(ref int _attackId, ref string _motionLayerName) => true;

  public bool GetBurstShotNormalDamageUpRate(AttackHitInfo.ATTACK_TYPE _type, ref float _value)
  {
    if (this.m_burstActionInfo == null)
      return false;
    if (_type == AttackHitInfo.ATTACK_TYPE.BURST_THS_SINGLE_SHOT)
    {
      _value = this.m_burstActionInfo.SingleShotBaseDmgRate;
      return true;
    }
    if (_type != AttackHitInfo.ATTACK_TYPE.BURST_THS_FULL_BURST)
      return false;
    _value = this.m_burstActionInfo.FullBurstBaseDmgRate;
    return true;
  }

  public bool GetBurstShotElementDamageUpRate(AttackHitInfo.ATTACK_TYPE _type, ref float _value)
  {
    if (this.m_burstActionInfo == null)
      return false;
    if (_type == AttackHitInfo.ATTACK_TYPE.BURST_THS_SINGLE_SHOT)
    {
      _value = this.m_burstActionInfo.SingleShotElementDmgRate;
      return true;
    }
    if (_type != AttackHitInfo.ATTACK_TYPE.BURST_THS_FULL_BURST)
      return false;
    _value = this.m_burstActionInfo.FullBurstElementDmgRate;
    return true;
  }

  public bool GetComboAttackParam(ref int _attackId, ref string _motionLayerName)
  {
    if (_attackId == this.m_burstActionInfo.NextShotAttackID)
    {
      if (!this.IsEnableShootAction())
        return false;
      _attackId = this.m_burstActionInfo.NextShotAttackID;
      _motionLayerName = this.m_owner.GetMotionLayerName(this.m_owner.attackMode, this.m_owner.spAttackType, _attackId);
    }
    return true;
  }

  public bool IsEnableChangeActionByLongTap()
  {
    if (Object.op_Equality((Object) this.m_owner, (Object) null))
      return false;
    if (this.IsEnableComboShotFromAvoidAttack())
    {
      int firstShotAttackId = this.m_burstActionInfo.FirstShotAttackID;
      string motionLayerName = this.m_owner.GetMotionLayerName(this.m_owner.attackMode, this.m_owner.spAttackType, firstShotAttackId);
      this.m_owner.ActAttack(firstShotAttackId, true, false, motionLayerName, "");
      return true;
    }
    if (!this.IsEnableShootFullBurst())
      return false;
    int fullBurstAttackId = this.m_burstActionInfo.FullBurstAttackID;
    string motionLayerName1 = this.m_owner.GetMotionLayerName(this.m_owner.attackMode, this.m_owner.spAttackType, fullBurstAttackId);
    this.m_owner.ActAttack(fullBurstAttackId, true, false, motionLayerName1, "");
    return true;
  }

  private bool IsEnableComboShotFromAvoidAttack()
  {
    return this.IsHitAvoidAttack && this.IsEnableTransitionFromAvoidAtk && this.IsEnableShootAction();
  }

  private bool IsEnableShootFullBurst() => this.IsHit3rdComboAttack && this.IsEnableShootAction();

  public bool IsRequiredReloadAction()
  {
    return !Object.op_Equality((Object) this.m_owner, (Object) null) && this.m_owner.spAttackType == SP_ATTACK_TYPE.BURST && this.CurrentRestBulletCount <= 0;
  }

  public bool IsEnableReloadAction()
  {
    return !Object.op_Equality((Object) this.m_owner, (Object) null) && this.m_owner.spAttackType == SP_ATTACK_TYPE.BURST && this.CurrentMaxBulletCount > 0 && this.CurrentRestBulletCount < this.CurrentMaxBulletCount;
  }

  public bool IsEnableShootAction()
  {
    return !Object.op_Equality((Object) this.m_owner, (Object) null) && this.m_owner.spAttackType == SP_ATTACK_TYPE.BURST && this.CurrentMaxBulletCount > 0 && this.CurrentRestBulletCount > 0;
  }

  public bool DoShootAction() => this.IsEnableShootAction() && this.ConsumeBullet();

  private bool ConsumeBullet()
  {
    if (this.CurrentRestBulletCount < 1)
      return false;
    this.SetCurrentRestBulletCount(this.CurrentRestBulletCount - 1);
    return true;
  }

  public bool DoReloadAction()
  {
    return this.IsEnableReloadAction() && this.IsReloadingNow && this.ReloadBullet();
  }

  public bool TryFirstReloadAction()
  {
    if (Object.op_Equality((Object) this.m_owner, (Object) null))
      return false;
    SelfController controller = this.m_owner.controller as SelfController;
    if (Object.op_Equality((Object) controller, (Object) null))
      return false;
    controller.OnReserveBurstReloadMotion();
    return true;
  }

  public bool IsChangebleReloadAction()
  {
    return !this.m_owner.isDead && !this.m_owner.IsChangingWeapon && (this.m_owner.IsPlayingMotion(2) || this.m_owner.actionID == (Character.ACTION_ID) 26) && this.IsEnableReloadAction() && !this.IsReloadingNow;
  }

  public bool CheckEnableNextReloadAction()
  {
    if (this.IsEnableReloadAction())
    {
      int reloadActionAttackId = this.m_burstActionInfo.NextReloadActionAttackID;
      string motionLayerName = this.m_owner.GetMotionLayerName(this.m_owner.attackMode, this.m_owner.spAttackType, reloadActionAttackId);
      this.m_owner.ActAttack(reloadActionAttackId, true, true, motionLayerName, "");
      return true;
    }
    this.SetStartReloading(false);
    return false;
  }

  private bool ReloadBullet()
  {
    if (this.CurrentRestBulletCount >= this.CurrentMaxBulletCount)
      return false;
    this.SetCurrentRestBulletCount(this.CurrentRestBulletCount + 1);
    return true;
  }

  public bool DoFullBurst()
  {
    if (this.CurrentRestBulletCount < 1)
      return false;
    this.SetCurrentRestBulletCount(0);
    return true;
  }

  public float GetAtkRate(Enemy _enemy, Player _player)
  {
    Vector3 vector3 = Vector3.op_Subtraction(((Component) _enemy).transform.position, ((Component) _player).transform.position);
    return this.m_burstActionInfo.GetDistanceAttenuationRatio(((Vector3) ref vector3).magnitude);
  }

  public class InitParam
  {
    public InGameSettingsManager.Player.TwoHandSwordActionInfo ActionInfo;
    public Player Owner;
    public int MaxBulletCount = 6;
    public int[] CurrentRestBullets;
    public bool IsNeedFullBullet = true;
  }
}
