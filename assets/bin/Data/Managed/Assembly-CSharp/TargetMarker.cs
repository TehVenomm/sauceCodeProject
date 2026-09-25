// Decompiled with JetBrains decompiler
// Type: TargetMarker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class TargetMarker
{
  public const string CANNON_MARKER_CRITICAL = "ef_btl_target_cannon_02";
  public const string CANNON_MARKER_GRAB = "ef_btl_target_cannon_03";
  protected Transform transform;
  protected Transform effectTransform;
  private TargetMarker.EFFECT_TYPE effectType;
  protected Transform multiLockTransform;
  private MultiLockMarker multiLock;

  public TargetPoint point { get; protected set; }

  public TargetMarker(Transform transform)
  {
    this.point = (TargetPoint) null;
    this.SetParentTransform(transform);
  }

  public void SetParentTransform(Transform _transform)
  {
    this.transform = _transform;
    if (Object.op_Inequality((Object) this.effectTransform, (Object) null))
      Utility.Attach(this.transform, this.effectTransform);
    if (!Object.op_Inequality((Object) this.multiLockTransform, (Object) null))
      return;
    Utility.Attach(this.transform, this.multiLockTransform);
  }

  public void UnableMarker()
  {
    this.effectType = TargetMarker.EFFECT_TYPE.NONE;
    if (Object.op_Inequality((Object) this.effectTransform, (Object) null))
      EffectManager.ReleaseEffect(((Component) this.effectTransform).gameObject);
    if (Object.op_Inequality((Object) this.multiLockTransform, (Object) null))
      EffectManager.ReleaseEffect(((Component) this.multiLockTransform).gameObject);
    this.effectTransform = (Transform) null;
    this.multiLockTransform = (Transform) null;
    this.multiLock = (MultiLockMarker) null;
  }

  public bool UpdateMarker(TargetMarker.UpdateParam param)
  {
    if (param == null)
      return false;
    bool flag1 = false;
    TargetMarker.EFFECT_TYPE index1 = TargetMarker.EFFECT_TYPE.NONE;
    switch (param.weakState)
    {
      case Enemy.WEAK_STATE.WEAK:
        index1 = TargetMarker.EFFECT_TYPE.WEAK;
        break;
      case Enemy.WEAK_STATE.DOWN:
        index1 = TargetMarker.EFFECT_TYPE.DOWN;
        break;
      case Enemy.WEAK_STATE.WEAK_SP_ATTACK:
      case Enemy.WEAK_STATE.WEAK_SP_DOWN_MAX:
      case Enemy.WEAK_STATE.WEAK_ELEMENT_SP_ATTACK:
        index1 = !param.isAimMode || !param.targeting || param.weakSubParam == 5 ? (param.weakState != Enemy.WEAK_STATE.WEAK_ELEMENT_SP_ATTACK ? (TargetMarker.EFFECT_TYPE) (7 + param.weakSubParam - 1) : TargetMarker.EFFECT_TYPE.WEAK_ELEMENT_SP_ATTACK) : TargetMarker.EFFECT_TYPE.NORMAL;
        break;
      case Enemy.WEAK_STATE.WEAK_ELEMENT_ATTACK:
        index1 = TargetMarker.EFFECT_TYPE.WEAK_ELEMENT_ATTACK;
        break;
      case Enemy.WEAK_STATE.WEAK_ELEMENT_SKILL_ATTACK:
        index1 = TargetMarker.EFFECT_TYPE.WEAK_ELEMENT_SKILL_ATTACK;
        break;
      case Enemy.WEAK_STATE.WEAK_SKILL_ATTACK:
        index1 = TargetMarker.EFFECT_TYPE.WEAK_SKILL_ATTACK;
        break;
      case Enemy.WEAK_STATE.WEAK_HEAL_ATTACK:
        index1 = TargetMarker.EFFECT_TYPE.WEAK_HEAL_ATTACK;
        break;
      case Enemy.WEAK_STATE.WEAK_CANNON:
        index1 = TargetMarker.EFFECT_TYPE.WEAK_CANNON;
        break;
      default:
        if (param.targeting)
        {
          index1 = TargetMarker.EFFECT_TYPE.NORMAL;
          break;
        }
        break;
    }
    if (param.isAimMode && (index1 == TargetMarker.EFFECT_TYPE.NONE || index1 == TargetMarker.EFFECT_TYPE.NORMAL))
    {
      switch (param.spAttackType)
      {
        case SP_ATTACK_TYPE.HEAT:
          index1 = !param.isAimChargeMax ? TargetMarker.EFFECT_TYPE.AIM_HEAT : TargetMarker.EFFECT_TYPE.AIM_HEAT_CHARGE_MAX;
          break;
        case SP_ATTACK_TYPE.SOUL:
          break;
        case SP_ATTACK_TYPE.BURST:
          index1 = !param.isAimChargeMax ? TargetMarker.EFFECT_TYPE.AIM_BURST : TargetMarker.EFFECT_TYPE.AIM_BURST_CHARGE_MAX;
          break;
        default:
          index1 = !param.isAimChargeMax ? TargetMarker.EFFECT_TYPE.AIM : TargetMarker.EFFECT_TYPE.AIM_CHARGE_MAX;
          break;
      }
    }
    bool flag2 = false;
    if (param.isAimArrow && param.spAttackType == SP_ATTACK_TYPE.SOUL)
    {
      switch (index1)
      {
        case TargetMarker.EFFECT_TYPE.WEAK:
        case TargetMarker.EFFECT_TYPE.WEAK_SP_ARROW:
          flag2 = true;
          break;
        case TargetMarker.EFFECT_TYPE.WEAK_ELEMENT_SP_ATTACK:
          if (param.weakSubParam == 5)
            goto case TargetMarker.EFFECT_TYPE.WEAK;
          goto default;
        default:
          index1 = TargetMarker.EFFECT_TYPE.NONE;
          goto case TargetMarker.EFFECT_TYPE.WEAK;
      }
    }
    if (this.effectType != index1 || Object.op_Inequality((Object) this.point, (Object) param.targetPoint) || !param.targetPoint.param.isShowRange)
    {
      if (Object.op_Inequality((Object) this.effectTransform, (Object) null))
        EffectManager.ReleaseEffect(((Component) this.effectTransform).gameObject);
      this.effectTransform = (Transform) null;
    }
    if (Object.op_Equality((Object) this.effectTransform, (Object) null) && index1 != TargetMarker.EFFECT_TYPE.NONE && param.targetPoint.param.isShowRange)
    {
      string str = MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarkerSettings.effectNames[(int) index1];
      switch (index1)
      {
        case TargetMarker.EFFECT_TYPE.WEAK_ELEMENT_ATTACK:
        case TargetMarker.EFFECT_TYPE.WEAK_ELEMENT_SKILL_ATTACK:
          str = param.validElementType >= 0 ? str + param.validElementType.ToString() : string.Empty;
          break;
        case TargetMarker.EFFECT_TYPE.WEAK_ELEMENT_SP_ATTACK:
          str = string.Format(str, (object) (param.weakSubParam - 1), (object) param.validElementType);
          break;
      }
      if (!string.IsNullOrEmpty(str))
        this.effectTransform = EffectManager.GetEffect(str, this.transform);
      flag1 = true;
    }
    if (param.playSign && param.targetPoint.param.isShowRange)
    {
      TargetMarker.EFFECT_TYPE index2 = TargetMarker.EFFECT_TYPE.NONE;
      switch (param.weakState)
      {
        case Enemy.WEAK_STATE.WEAK:
          index2 = TargetMarker.EFFECT_TYPE.WEAK_SIGN;
          break;
        case Enemy.WEAK_STATE.DOWN:
          index2 = TargetMarker.EFFECT_TYPE.DOWN_SIGN;
          break;
        case Enemy.WEAK_STATE.WEAK_SP_ATTACK:
        case Enemy.WEAK_STATE.WEAK_SP_DOWN_MAX:
          index2 = (TargetMarker.EFFECT_TYPE) (12 + param.weakSubParam - 1);
          break;
      }
      if (index2 != TargetMarker.EFFECT_TYPE.NONE)
      {
        string effectName = MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarkerSettings.effectNames[(int) index2];
        if (!string.IsNullOrEmpty(effectName))
        {
          Transform effect = EffectManager.GetEffect(effectName, this.transform);
          if (Object.op_Inequality((Object) effect, (Object) null))
            effect.Set(param.targetPoint.param.markerPos, param.targetPoint.param.markerRot);
          flag1 = true;
        }
      }
    }
    if (flag2 && param.targetPoint.param.isShowRange)
    {
      if (!param.isMultiLockMax && Object.op_Equality((Object) this.multiLockTransform, (Object) null))
      {
        string effectName = MonoBehaviourSingleton<InGameSettingsManager>.I.targetMarkerSettings.effectNames[24];
        if (!effectName.IsNullOrWhiteSpace())
        {
          this.multiLockTransform = EffectManager.GetEffect(effectName, this.transform);
          this.multiLock = ((Component) this.multiLockTransform).GetComponentInChildren<MultiLockMarker>();
          if (Object.op_Inequality((Object) this.multiLock, (Object) null))
            this.multiLock.Init();
          flag1 = true;
        }
      }
    }
    else
    {
      if (Object.op_Inequality((Object) this.multiLockTransform, (Object) null))
        EffectManager.ReleaseEffect(((Component) this.multiLockTransform).gameObject);
      this.multiLockTransform = (Transform) null;
      this.multiLock = (MultiLockMarker) null;
    }
    this.effectType = index1;
    this.point = param.targetPoint;
    if (Object.op_Inequality((Object) this.effectTransform, (Object) null))
    {
      this.effectTransform.Set(this.point.param.markerPos, this.point.param.markerRot);
      this.effectTransform.localScale = Vector3.op_Multiply(Vector3.one, param.markerScale);
    }
    if (Object.op_Inequality((Object) this.multiLockTransform, (Object) null))
    {
      this.multiLockTransform.Set(this.point.param.markerPos, this.point.param.markerRot);
      this.multiLockTransform.localScale = Vector3.op_Multiply(Vector3.one, param.markerScale);
    }
    return flag1;
  }

  public void UpdateByTargetPoint(TargetPoint targetPoint, string effectName)
  {
    if (Object.op_Equality((Object) this.effectTransform, (Object) null))
      this.effectTransform = EffectManager.GetEffect(effectName, this.transform);
    Transform cameraTransform = MonoBehaviourSingleton<InGameCameraManager>.I.cameraTransform;
    Vector3 targetPoint1 = targetPoint.GetTargetPoint();
    Vector3 vector3 = Vector3.op_Subtraction(cameraTransform.position, targetPoint1);
    this.effectTransform.Set(Vector3.op_Addition(Vector3.op_Multiply(((Vector3) ref vector3).normalized, targetPoint.scaledMarkerZShift), targetPoint1), cameraTransform.rotation);
  }

  public MultiLockMarker GetMultiLock() => this.multiLock;

  public void HideMultiLock()
  {
    if (Object.op_Equality((Object) this.multiLock, (Object) null))
      return;
    this.multiLock.Hide();
  }

  public void ResetMultiLock()
  {
    if (Object.op_Equality((Object) this.multiLock, (Object) null))
      return;
    this.multiLock.Reset();
  }

  public void EndMultiLockBoost(bool isHide)
  {
    if (Object.op_Equality((Object) this.multiLock, (Object) null))
      return;
    this.multiLock.EndBoost(isHide);
  }

  public List<int> GetMultiLockOrder()
  {
    return Object.op_Equality((Object) this.multiLock, (Object) null) ? (List<int>) null : this.multiLock.lockOrder;
  }

  public int GetMultiLockNum()
  {
    return Object.op_Equality((Object) this.multiLock, (Object) null) ? 0 : this.multiLock.lockOrder.Count;
  }

  public enum EFFECT_TYPE
  {
    NONE = -1, // 0xFFFFFFFF
    NORMAL = 0,
    WEAK = 1,
    DOWN = 2,
    WEAK_SIGN = 3,
    DOWN_SIGN = 4,
    AIM = 5,
    AIM_CHARGE_MAX = 6,
    WEAK_SP_ONE_HAND_SWORD = 7,
    WEAK_SP_TWO_HAND_SWORD = 8,
    WEAK_SP_SPEAR = 9,
    WEAK_SP_PAIR_SWORDS = 10, // 0x0000000A
    WEAK_SP_ARROW = 11, // 0x0000000B
    WEAK_SP_ONE_HAND_SWORD_SIGN = 12, // 0x0000000C
    WEAK_SP_TWO_HAND_SWORD_SIGN = 13, // 0x0000000D
    WEAK_SP_SPEAR_SIGN = 14, // 0x0000000E
    WEAK_SP_PAIR_SWORDS_SIGN = 15, // 0x0000000F
    WEAK_SP_ARROW_SIGN = 16, // 0x00000010
    WEAK_ELEMENT_ATTACK = 17, // 0x00000011
    WEAK_ELEMENT_SKILL_ATTACK = 18, // 0x00000012
    WEAK_SKILL_ATTACK = 19, // 0x00000013
    WEAK_HEAL_ATTACK = 20, // 0x00000014
    AIM_HEAT = 21, // 0x00000015
    AIM_HEAT_CHARGE_MAX = 22, // 0x00000016
    WEAK_CANNON = 23, // 0x00000017
    AIM_SOUL = 24, // 0x00000018
    WEAK_ELEMENT_SP_ATTACK = 25, // 0x00000019
    AIM_BURST = 26, // 0x0000001A
    AIM_BURST_CHARGE_MAX = 27, // 0x0000001B
  }

  public class UpdateParam
  {
    public TargetPoint targetPoint;
    public bool targeting;
    public bool isLock;
    public Enemy.WEAK_STATE weakState;
    public int weakSubParam = -1;
    public bool playSign;
    public SP_ATTACK_TYPE spAttackType;
    public bool isAimArrow;
    public bool isAimMode;
    public bool isAimChargeMax;
    public float markerScale = 1f;
    public int validElementType = -1;
    public bool isMultiLockMax;
  }
}
