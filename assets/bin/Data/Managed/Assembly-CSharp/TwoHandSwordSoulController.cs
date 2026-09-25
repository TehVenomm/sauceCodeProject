// Decompiled with JetBrains decompiler
// Type: TwoHandSwordSoulController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class TwoHandSwordSoulController : IWeaponController
{
  public const int TWOHANDSWORD_SOUL_BASE_ATTACK_ID = 10;
  public const int TWOHANDSWORD_SOUL_FINISH_ATTACK_ID = 14;
  public const int TWOHANDSWORD_SOUL_SP_ATTACK_ID = 96 /*0x60*/;
  private Player m_owner;
  private InGameSettingsManager.Player.TwoHandSwordActionInfo m_actionInfo;
  private TwoHandSwordSoulController.eIaiState iaiState;
  private float twoHandSwordBoostAttackSpeed;
  private Transform twoHandSwordsBoostLoopEffect;
  private Transform twoHandSwordsChargeMaxEffect;
  private float iaiCounter;

  public TwoHandSwordSoulController.eIaiState IaiState => this.iaiState;

  public void SetIaiState(TwoHandSwordSoulController.eIaiState _state) => this.iaiState = _state;

  public bool IsActiveIai() => this.iaiState != 0;

  public float TwoHandSwordBoostAttackSpeed => this.twoHandSwordBoostAttackSpeed;

  public void SetTwoHandSwordBoostAttackSpeed(float _speed)
  {
    this.twoHandSwordBoostAttackSpeed = _speed;
    if (this.m_actionInfo == null)
      return;
    this.twoHandSwordBoostAttackSpeed = Mathf.Clamp(this.twoHandSwordBoostAttackSpeed, this.m_actionInfo.soulBoostMinAttackSpeed, this.m_actionInfo.soulBoostMaxAttackSpeed);
  }

  public TwoHandSwordSoulController() => this.m_owner = (Player) null;

  public void Init(Player _player)
  {
    this.m_owner = _player;
    this.m_actionInfo = _player.playerParameter.twoHandSwordActionInfo;
  }

  public void Init(TwoHandSwordSoulController.InitParam _param)
  {
  }

  public void OnLoadComplete()
  {
  }

  public void Update()
  {
    switch (this.iaiState)
    {
      case TwoHandSwordSoulController.eIaiState.In:
        this.iaiCounter += Time.deltaTime;
        if ((double) this.iaiCounter < (double) this.m_actionInfo.soulIaiInSec)
          break;
        this.m_owner.SetEnableNodeRenderer("", false);
        this.m_owner.EventMoveEnd();
        this.m_owner.SetEnableEventMove(true);
        this.m_owner.SetEnableAddForce(false);
        this.m_owner.SetEventMoveVelocity(Vector3.op_Multiply(Vector3.op_Multiply(Vector3.forward, this.GetIaiMoveSpeed(this.m_owner.ChargeRate)), this.m_owner.buffParam.GetDistanceRateIai()));
        this.m_owner.SetVelocity(Quaternion.op_Multiply(Quaternion.LookRotation(this.m_owner.GetTransformForward()), this.m_owner.EventMoveVelocity), Character.VELOCITY_TYPE.EVENT_MOVE);
        this.m_owner.SetEventMoveTimeCount(this.m_actionInfo.soulIaiMoveSec);
        this.iaiCounter = 0.0f;
        this.SetIaiState(TwoHandSwordSoulController.eIaiState.Hide);
        break;
      case TwoHandSwordSoulController.eIaiState.Hide:
        this.iaiCounter += Time.deltaTime;
        if ((double) this.iaiCounter < (double) this.m_actionInfo.soulIaiHideSec)
          break;
        this.m_owner.SetEnableNodeRenderer("", true);
        this.m_owner.SetNextTrigger();
        this.iaiCounter = 0.0f;
        this.SetIaiState(TwoHandSwordSoulController.eIaiState.None);
        break;
    }
  }

  public void OnEndAction()
  {
    if (Object.op_Inequality((Object) this.m_owner, (Object) null))
      this.m_owner.ReleaseEffect(ref this.twoHandSwordsChargeMaxEffect);
    this.SetIaiState(TwoHandSwordSoulController.eIaiState.None);
    this.iaiCounter = 0.0f;
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

  public void SetChargeRelease()
  {
    if (Object.op_Equality((Object) this.m_owner, (Object) null))
      return;
    this.m_owner.ReleaseEffect(ref this.twoHandSwordsChargeMaxEffect);
    this.iaiCounter = 0.0f;
    this.SetIaiState(TwoHandSwordSoulController.eIaiState.In);
    this.m_owner.SetNextTrigger();
  }

  public bool GetIaiNormalDamageUp(ref float value, int attackID, float chargeRate)
  {
    value = 1f;
    if (!this.IsSpAttackId(attackID))
      return false;
    value = (double) chargeRate < 1.0 ? this.m_actionInfo.soulIaiNormalDamageUp[0] + (this.m_actionInfo.soulIaiNormalDamageUp[1] - this.m_actionInfo.soulIaiNormalDamageUp[0]) * chargeRate : this.m_actionInfo.soulIaiNormalDamageUp[2];
    return true;
  }

  public float GetIaiGaugeIncreaseValue(float chargeRate)
  {
    return (double) chargeRate >= 1.0 ? this.m_actionInfo.soulIaiGaugeIncreaseValue[2] : this.m_actionInfo.soulIaiGaugeIncreaseValue[0] + (this.m_actionInfo.soulIaiGaugeIncreaseValue[1] - this.m_actionInfo.soulIaiGaugeIncreaseValue[0]) * chargeRate;
  }

  private float GetIaiMoveSpeed(float chargeRate)
  {
    return this.m_actionInfo.soulIaiMoveSpeed[0] + (this.m_actionInfo.soulIaiMoveSpeed[1] - this.m_actionInfo.soulIaiMoveSpeed[0]) * chargeRate;
  }

  public bool IsComboFinishAttackId(int attackId)
  {
    return attackId == 14 || attackId == this.m_actionInfo.Soul_LongAttackFinishId;
  }

  public bool IsSpAttackId(int attackId)
  {
    return attackId == 96 /*0x60*/ || attackId == this.m_actionInfo.Soul_LongSpAttackId;
  }

  public class InitParam
  {
    public Player Owner;
  }

  public enum eIaiState
  {
    None,
    In,
    Hide,
  }
}
