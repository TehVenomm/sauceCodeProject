// Decompiled with JetBrains decompiler
// Type: OneHandSwordController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class OneHandSwordController : IWeaponController
{
  private static readonly int ORACLE_ATTACK_ID_START = 23;
  private static readonly int ORACLE_ATTACK_ID_END = 30;
  private InGameSettingsManager.Player.OneHandSwordActionInfo ohsInfo;
  private Player owner;
  private bool isCtrlActive;
  private List<Transform> oracleDragonEffects = new List<Transform>();
  private List<Animator> oracleDragonEffectAnimators = new List<Animator>();
  private Transform oracleProtection;

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

  private SnatchController snatchCtrl => this.owner.snatchCtrl;

  public void Init(Player player)
  {
    if (Object.op_Equality((Object) player, (Object) null))
      return;
    this.owner = player;
    this.ohsInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.ohsActionInfo;
  }

  public void OnLoadComplete()
  {
    if (!this.owner.CheckAttackMode(Player.ATTACK_MODE.ONE_HAND_SWORD))
      this.isCtrlActive = false;
    else
      this.isCtrlActive = true;
  }

  public void Update() => this.UpdateSpActionGauge();

  public void OnEndAction()
  {
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
    if (!this.owner.enableCounterAttack)
      return;
    switch (this.owner.spAttackType)
    {
      case SP_ATTACK_TYPE.NONE:
        this.owner.ActAttack(this.owner.playerParameter.specialActionInfo.spAttackID, true, true, "", "");
        this.IncrementCounterAttackCount();
        this.owner.isActOneHandSwordCounter = true;
        break;
      case SP_ATTACK_TYPE.HEAT:
        this.owner.ActAttack(this.ohsInfo.heatOHSInfo.counterAttackId, true, true, "", "");
        this.IncrementCounterAttackCount();
        this.owner.isActOneHandSwordCounter = true;
        break;
    }
  }

  public void OnActAttack(int id)
  {
    if (this.owner.spAttackType != SP_ATTACK_TYPE.ORACLE)
      return;
    string str = "attack_" + (object) id;
    int hash = Animator.StringToHash(str);
    for (int index = 0; index < this.oracleDragonEffectAnimators.Count; ++index)
    {
      Animator dragonEffectAnimator = this.oracleDragonEffectAnimators[index];
      if (dragonEffectAnimator.HasState(0, hash))
        dragonEffectAnimator.Play(str);
    }
  }

  public void OnBuffStart(BuffParam.BuffData data)
  {
    if (data.type != BuffParam.BUFFTYPE.ORACLE_OHS_PROTECTION || !Object.op_Equality((Object) this.oracleProtection, (Object) null))
      return;
    this.oracleProtection = EffectManager.GetEffect("ef_btl_wsk4_sword_dragon_veil", this.owner._transform);
  }

  public void OnBuffEnd(BuffParam.BUFFTYPE type)
  {
    if (type != BuffParam.BUFFTYPE.ORACLE_OHS_PROTECTION || !Object.op_Inequality((Object) this.oracleProtection, (Object) null))
      return;
    EffectManager.ReleaseEffect(((Component) this.oracleProtection).gameObject);
    this.oracleProtection = (Transform) null;
  }

  public void OnReloadEffect(BuffParam.BUFFTYPE type)
  {
    if (type != BuffParam.BUFFTYPE.ORACLE_OHS_PROTECTION)
      return;
    if (Object.op_Inequality((Object) this.oracleProtection, (Object) null))
      EffectManager.ReleaseEffect(((Component) this.oracleProtection).gameObject);
    EffectManager.OneShot("ef_btl_wsk4_sword_dragon_veil_re", this.owner._transform.position, this.owner._transform.rotation, Vector3.one, callback: (Action<Transform>) (effect =>
    {
      effect.parent = this.owner._transform;
      effect.localPosition = Vector3.zero;
      effect.localRotation = Quaternion.identity;
      effect.localScale = Vector3.one;
    }));
    this.oracleProtection = EffectManager.GetEffect("ef_btl_wsk4_sword_dragon_veil", this.owner._transform);
  }

  public void OnChangeWeapon()
  {
    if (!this.owner.IsValidBuff(BuffParam.BUFFTYPE.ORACLE_OHS_PROTECTION))
      return;
    this.owner.OnBuffEnd(BuffParam.BUFFTYPE.ORACLE_OHS_PROTECTION, true, true);
  }

  public void OnAttackedHitFix(AttackedHitStatusFix status)
  {
  }

  private void UpdateSpActionGauge()
  {
    if (!this.isCtrlActive || Object.op_Equality((Object) this.owner, (Object) null))
      return;
    float num1;
    switch (this.owner.spAttackType)
    {
      case SP_ATTACK_TYPE.SOUL:
        if (!this.owner.isBoostMode)
          return;
        num1 = this.snatchCtrl.state != SnatchController.STATE.SNATCH ? this.ohsInfo.Soul_BoostGaugeDecreasePerSecond * Time.deltaTime : this.ohsInfo.Soul_BoostSnatchGaugeDecreasePerSecond * Time.deltaTime;
        break;
      case SP_ATTACK_TYPE.BURST:
        if (!this.owner.isBoostMode)
          return;
        num1 = 1000f / this.ohsInfo.burstOHSInfo.GaugeTime * Time.deltaTime;
        break;
      case SP_ATTACK_TYPE.ORACLE:
        if (!this.owner.isBoostMode)
          return;
        num1 = 1000f / this.ohsInfo.oracleOHSInfo.spGaugeDecreasingValue * Time.deltaTime;
        break;
      default:
        return;
    }
    float num2 = 1f + this.owner.GetSpGaugeDecreasingRate();
    this.spActionGauge -= num1 * num2;
    if ((double) this.spActionGauge > 0.0)
      return;
    this.spActionGauge = 0.0f;
  }

  private void IncrementCounterAttackCount()
  {
    if (!this.owner.IsCoopNone() && !this.owner.IsOriginal() || !this.owner.buffParam.IncrementCounterConditionAbility())
      return;
    EffectManager.GetEffect("ef_btl_ab_charge_01", this.owner._transform);
  }

  public bool GetNormalAttackId(
    SP_ATTACK_TYPE _spAtkType,
    EXTRA_ATTACK_TYPE _exAtkType,
    ref int _attackId,
    ref string _motionLayerName)
  {
    if (Object.op_Equality((Object) this.owner, (Object) null) || this.ohsInfo == null)
      return false;
    switch (_spAtkType)
    {
      case SP_ATTACK_TYPE.SOUL:
        _attackId = this.ohsInfo.soulOHSInfo.BaseAtkId;
        break;
      case SP_ATTACK_TYPE.BURST:
        _attackId = this.ohsInfo.burstOHSInfo.BaseAtkId;
        _motionLayerName = this.owner.GetMotionLayerName(Player.ATTACK_MODE.ONE_HAND_SWORD, _spAtkType, _attackId);
        break;
      case SP_ATTACK_TYPE.ORACLE:
        _attackId = this.ohsInfo.oracleOHSInfo.comboAttackId;
        _motionLayerName = this.owner.GetMotionLayerName(Player.ATTACK_MODE.ONE_HAND_SWORD, _spAtkType, _attackId);
        break;
    }
    return true;
  }

  public bool CheckActAttackCombo(int attackId)
  {
    if (!this.owner.CheckSpAttackType(SP_ATTACK_TYPE.HEAT) || !this.owner.isBoostMode || attackId != this.ohsInfo.heatOHSInfo.revengeStrikeAttackId)
      return false;
    this.spActionGauge = 0.0f;
    return true;
  }

  public static bool IsOracleAttackId(int id)
  {
    return id >= OneHandSwordController.ORACLE_ATTACK_ID_START && id <= OneHandSwordController.ORACLE_ATTACK_ID_END;
  }

  public void OnStartOracleBoost()
  {
    BuffParam.BuffData buffData = new BuffParam.BuffData();
    buffData.type = BuffParam.BUFFTYPE.ORACLE_OHS_PROTECTION;
    int doubleProbability = this.owner.buffParam.GetOracleOhsProtectionDoubleProbability();
    int num = Random.Range(0, 100);
    buffData.value = num < doubleProbability ? 2 : 1;
    buffData.time = -1f;
    this.owner.OnBuffStart(buffData);
    InGameSettingsManager.Player.OracleOneHandSwordActionInfo.DragonEffect[] dragonEffects = this.ohsInfo.oracleOHSInfo.dragonEffects;
    for (int index = 0; index < dragonEffects.Length; ++index)
    {
      Transform node = this.owner.FindNode(dragonEffects[index].link);
      if (!Object.op_Equality((Object) node, (Object) null))
      {
        Transform effect = EffectManager.GetEffect(dragonEffects[index].GetEffectName(this.owner.GetNowWeaponElement()), node);
        if (!Object.op_Equality((Object) effect, (Object) null))
        {
          this.oracleDragonEffects.Add(effect);
          Animator component = ((Component) effect).GetComponent<Animator>();
          if (Object.op_Inequality((Object) component, (Object) null))
            this.oracleDragonEffectAnimators.Add(component);
        }
      }
    }
  }

  public void OnEndOracleBoost()
  {
    for (int index = 0; index < this.oracleDragonEffects.Count; ++index)
      EffectManager.ReleaseEffect(((Component) this.oracleDragonEffects[index]).gameObject);
    this.oracleDragonEffects.Clear();
    this.oracleDragonEffectAnimators.Clear();
  }
}
