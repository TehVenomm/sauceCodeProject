// Decompiled with JetBrains decompiler
// Type: TwoHandSwordOracleController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class TwoHandSwordOracleController : IWeaponController
{
  public const int TWOHANDSWORD_ORACLE_BASE_ATTACK_ID = 60;
  public const int TWOHANDSWORD_ORACLE_BASE_ATTACK_COMBO_02_ID = 61;
  public const int TWOHANDSWORD_ORACLE_BASE_ATTACK_COMBO_03_ID = 62;
  public const int TWOHANDSWORD_ORACLE_SP_ATTACK_ID = 63 /*0x3F*/;
  public const int TWOHANDSWORD_ORACLE_AVOID_ATTACK_ID = 64 /*0x40*/;
  private Player owner;
  private InGameSettingsManager.Player.OracleTwoHandSwordActionInfo oracleActionInfo;
  private AnimEventData.EventData playingVernierEffectEventData;
  private bool isHorizontalAttack;
  private bool isHorizontalMax;
  private bool isHorizontalFollow;
  private int countHorizontal;
  private bool isHitHorizontalAttack;
  private bool isTapHorizontalAttack;

  public bool IsHorizontalAttack => this.isHorizontalAttack;

  public TwoHandSwordOracleController() => this.owner = (Player) null;

  public void Init(Player _player)
  {
    this.owner = _player;
    this.oracleActionInfo = MonoBehaviourSingleton<InGameSettingsManager>.I.player.twoHandSwordActionInfo.oracleTHSInfo;
  }

  public void OnLoadComplete()
  {
  }

  public void Update()
  {
  }

  public void OnEndAction()
  {
    this.EndVernierEffect();
    this.EndHorizontal();
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

  public static bool IsOracleLayerAttackId(int attackId)
  {
    return 60 <= attackId && attackId <= 64 /*0x40*/;
  }

  public bool IsEnableChangeActionByLongTap()
  {
    if (Object.op_Equality((Object) this.owner, (Object) null))
      return false;
    Self owner = this.owner as Self;
    if (Object.op_Inequality((Object) owner, (Object) null))
    {
      if (owner.controllerInputCombo && owner.actionID == Character.ACTION_ID.ATTACK && owner.attackID == 61)
        return owner.InputAttackCombo();
      if (owner.IsActionFromAvoid() && owner.enableSpAttackContinue)
      {
        int num = 64 /*0x40*/;
        string motionLayerName = this.owner.GetMotionLayerName(this.owner.attackMode, this.owner.spAttackType, num);
        this.owner.ActAttack(num, true, false, motionLayerName, "");
        return true;
      }
    }
    return false;
  }

  public void StartVernierEffect(bool isMax)
  {
    string str = (string) null;
    if (isMax)
    {
      int currentWeaponElement = this.owner.GetCurrentWeaponElement();
      if (currentWeaponElement < this.oracleActionInfo.maxVernierEffectNames.Length)
        str = this.oracleActionInfo.maxVernierEffectNames[currentWeaponElement];
    }
    else
      str = this.oracleActionInfo.normalVernierEffectName;
    if (this.playingVernierEffectEventData != null)
    {
      if (!(this.playingVernierEffectEventData.stringArgs[0] != str))
        return;
      this.EndVernierEffect();
    }
    if (str == null)
      return;
    AnimEventData.EventData data = new AnimEventData.EventData();
    data.id = AnimEventFormat.ID.EFFECT;
    data.stringArgs = new string[2]
    {
      str,
      GameDefine.PLAYER_WEAPON_PARENT_NODE_RIGHT
    };
    data.intArgs = new int[3]
    {
      0,
      0,
      this.oracleActionInfo.isPlayOtherPlayerVernierEffect ? 1 : 0
    };
    AnimEventData.EventData eventData = data;
    float[] numArray = new float[7];
    numArray[0] = 1f;
    eventData.floatArgs = numArray;
    this.owner.OnAnimEvent(data);
    this.playingVernierEffectEventData = data;
    this.owner.OnAnimEvent(new AnimEventData.EventData()
    {
      id = AnimEventFormat.ID.SE_ONESHOT,
      stringArgs = new string[1]{ "" },
      intArgs = new int[1]
      {
        isMax ? this.oracleActionInfo.maxVernierSeId : this.oracleActionInfo.normalVernierSeId
      }
    });
  }

  public void EndVernierEffect()
  {
    if (this.playingVernierEffectEventData == null)
      return;
    this.owner.OnAnimEvent(new AnimEventData.EventData()
    {
      id = AnimEventFormat.ID.EFFECT_DELETE,
      stringArgs = this.playingVernierEffectEventData.stringArgs
    });
    this.playingVernierEffectEventData = (AnimEventData.EventData) null;
  }

  public void StartHorizontal(bool isMax)
  {
    this.isHorizontalAttack = true;
    this.isHorizontalMax = isMax;
    this.isHorizontalFollow = false;
    this.countHorizontal = 0;
    this.isHitHorizontalAttack = false;
    this.isTapHorizontalAttack = false;
    this.owner.UpdateAnimatorSpeed();
  }

  public void HitHorizontal() => this.isHitHorizontalAttack = true;

  public void CheckNextHorizontal()
  {
    if (!this.isHorizontalAttack)
      return;
    this.isTapHorizontalAttack = true;
  }

  public bool NextHorizontal()
  {
    bool flag = this.isHorizontalAttack;
    bool isFinish = false;
    Self owner = this.owner as Self;
    if (flag && Object.op_Inequality((Object) owner, (Object) null) && (!this.isTapHorizontalAttack || this.oracleActionInfo.needHitHorizontal && !this.isHitHorizontalAttack))
      flag = false;
    if (flag)
    {
      if (this.EnableHorizontalNext())
      {
        ++this.countHorizontal;
        this.isHitHorizontalAttack = false;
        this.isTapHorizontalAttack = false;
        this.owner.UpdateAnimatorSpeed();
      }
      else if (this.owner.IsCoopNone() || this.owner.IsOriginal())
      {
        flag = false;
        isFinish = true;
      }
    }
    if (isFinish || !flag)
      this.SetHorizontalNextMotion(isFinish);
    return flag && Object.op_Inequality((Object) owner, (Object) null);
  }

  private bool EnableHorizontalNext()
  {
    if (this.isHorizontalMax)
    {
      if (this.oracleActionInfo.horizontalMaxSpinInfoList == null || this.countHorizontal >= this.oracleActionInfo.horizontalMaxSpinInfoList.Length)
        return false;
    }
    else if (this.oracleActionInfo.horizontalSpinInfoList == null || this.countHorizontal >= this.oracleActionInfo.horizontalSpinInfoList.Length)
      return false;
    return true;
  }

  public void SetHorizontalNextMotion(bool isFinish)
  {
    if (!this.isHorizontalAttack || this.isHorizontalFollow)
      return;
    this.isHorizontalFollow = true;
    this.owner.SetNextTrigger(isFinish ? 1 : 0);
    this.owner.UpdateAnimatorSpeed();
    if (!Object.op_Inequality((Object) this.owner.playerSender, (Object) null))
      return;
    this.owner.playerSender.OnSetHorizontalNextMotion(isFinish);
  }

  public void EndHorizontal()
  {
    this.isHorizontalAttack = false;
    this.isHitHorizontalAttack = false;
  }

  public float GetHorizontalSpeed()
  {
    if (this.isHorizontalAttack && !this.isHorizontalFollow)
    {
      if (this.countHorizontal <= 0)
        return this.isHorizontalMax ? this.oracleActionInfo.horizontalMaxFirstSpeed * this.owner.buffParam.GetOracleThsHorizontalSpeedRate() : 1f * this.owner.buffParam.GetOracleThsHorizontalSpeedRate();
      int index = this.countHorizontal - 1;
      InGameSettingsManager.Player.OracleTwoHandSwordActionInfo.HorizontalSpinInfo[] horizontalSpinInfoArray = this.isHorizontalMax ? this.oracleActionInfo.horizontalMaxSpinInfoList : this.oracleActionInfo.horizontalSpinInfoList;
      if (index < horizontalSpinInfoArray.Length)
        return horizontalSpinInfoArray[index].spinSpeedRate * this.owner.buffParam.GetOracleThsHorizontalSpeedRate();
    }
    return 1f;
  }

  public bool GetHorizontalDamageUpRate(string attackInfoName, ref float value)
  {
    value = 1f;
    if (!this.isHorizontalAttack || this.oracleActionInfo.horizontalDamageUpAttackInfoNames == null || !Array.Exists<string>(this.oracleActionInfo.horizontalDamageUpAttackInfoNames, (Predicate<string>) (s => s == attackInfoName)) || this.countHorizontal <= 0)
      return false;
    int index = this.countHorizontal - 1;
    InGameSettingsManager.Player.OracleTwoHandSwordActionInfo.HorizontalSpinInfo[] horizontalSpinInfoArray = this.isHorizontalMax ? this.oracleActionInfo.horizontalMaxSpinInfoList : this.oracleActionInfo.horizontalSpinInfoList;
    if (index >= horizontalSpinInfoArray.Length)
      return false;
    value = horizontalSpinInfoArray[index].damageRate;
    return true;
  }

  public bool GetChargeNormalDamageUpRate(AttackHitInfo.ATTACK_TYPE attackType, ref float value)
  {
    value = 1f;
    if (attackType != AttackHitInfo.ATTACK_TYPE.THS_ORACLE_CHARGE)
    {
      if (attackType != AttackHitInfo.ATTACK_TYPE.THS_ORACLE_CHARGE_MAX)
        return false;
      value = this.oracleActionInfo.maxChargeBaseDmgRate;
      return true;
    }
    value = this.oracleActionInfo.chargeBaseDmgRate;
    return true;
  }

  public bool GetChargeElementDamageUpRate(AttackHitInfo.ATTACK_TYPE attackType, ref float value)
  {
    value = 1f;
    if (attackType != AttackHitInfo.ATTACK_TYPE.THS_ORACLE_CHARGE)
    {
      if (attackType != AttackHitInfo.ATTACK_TYPE.THS_ORACLE_CHARGE_MAX)
        return false;
      value = this.oracleActionInfo.maxChargeElementDmgRate;
      return true;
    }
    value = this.oracleActionInfo.chargeElementDmgRate;
    return true;
  }

  public bool GetConcussionEnemyElementDamageUpRate(Enemy enemy, ref float value)
  {
    value = 1f;
    if (!enemy.IsConcussion())
      return false;
    value = this.oracleActionInfo.concussionEnemyElementDmgRate;
    return true;
  }
}
