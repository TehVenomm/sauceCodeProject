// Decompiled with JetBrains decompiler
// Type: FieldGimmickCannonSpecial
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public class FieldGimmickCannonSpecial : FieldGimmickCannonBase
{
  private static readonly string NAME_ATTACKINFO = "cannonball_special";
  private static readonly string NAME_NODE_SHOT_EFFECT = "Effect";
  private static readonly Vector3 OFFSET_BEAM_CHARGE_EFFECT = new Vector3(0.0f, 0.0f, 11f);
  public static readonly string NAME_EFFECT_CHARGE = "ef_btl_magibullet_cannon_01_01";
  private Transform m_launchTrans;
  private Transform m_effectChargeTrans;
  private float m_delayChangeCamera;
  private float m_durationChangeCamera;
  private int m_seIdShot;
  private int m_seIdCharge;
  private int m_seIdChargeMax;
  private int m_seIdOnBoard;
  private bool isPlayedChargeMaxSE;

  public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
  {
    base.Initialize(pointData);
    this.m_launchTrans = this.modelTrans.Find(FieldGimmickCannonSpecial.NAME_NODE_SHOT_EFFECT);
    this.m_delayChangeCamera = MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.delayChangeCameraForSpecial;
    this.m_seIdShot = MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.seIdForSpecial;
    this.m_seIdCharge = MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.seIdForSpecialCharge;
    this.m_seIdChargeMax = MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.seIdForSpecialChargeMax;
    this.m_seIdOnBoard = MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.seIdForSpecialOnBoard;
  }

  public override void OnBoard(Player player)
  {
    base.OnBoard(player);
    if (this.m_seIdOnBoard > 0)
      SoundManager.PlayOneShotSE(this.m_seIdOnBoard, this._transform.position);
    this.m_owner.SetCannonChargeMax(MonoBehaviourSingleton<InGameSettingsManager>.I.cannonParam.chargeTimeMaxForSpecial);
  }

  public override void OnLeave()
  {
    base.OnLeave();
    this.ReleaseCharge();
  }

  public override void Shot()
  {
    if (!this.IsReadyForShot() || this.m_owner is Self && !this.m_owner.IsCannonFullCharged())
      return;
    AttackInfo attackHitInfo = this.GetAttackHitInfo();
    if (attackHitInfo == null)
      return;
    new GameObject("AttackCannonBeam").AddComponent<AttackCannonBeam>().Initialize(new AttackCannonBeam.InitParamCannonBeam()
    {
      attacker = (StageObject) this.m_owner,
      atkInfo = attackHitInfo,
      launchTrans = this.m_launchTrans
    });
    if (Object.op_Inequality((Object) attackHitInfo.bulletData, (Object) null) && attackHitInfo.bulletData.data != null)
      this.m_durationChangeCamera = attackHitInfo.bulletData.data.appearTime;
    if (this.m_seIdShot > 0)
      SoundManager.PlayOneShotSE(this.m_seIdShot, this.m_launchTrans.position);
    this.StartCoroutine(this.DelayCameraChange());
    this.StartCoolTime();
    this.SetState(FieldGimmickCannonBase.STATE.COOLTIME);
  }

  private IEnumerator DelayCameraChange()
  {
    if (this.m_owner is Self)
    {
      yield return (object) new WaitForSeconds(this.m_delayChangeCamera);
      if (!Object.op_Equality((Object) this.m_owner, (Object) null) && MonoBehaviourSingleton<InGameCameraManager>.IsValid())
      {
        MonoBehaviourSingleton<InGameCameraManager>.I.SetCameraMode(InGameCameraManager.CAMERA_MODE.CANNON_BEAM);
        yield return (object) new WaitForSeconds(this.m_durationChangeCamera);
        if (!Object.op_Equality((Object) this.m_owner, (Object) null))
          MonoBehaviourSingleton<InGameCameraManager>.I.SetCameraMode(InGameCameraManager.CAMERA_MODE.CANNON_BEAM_CHARGE);
      }
    }
  }

  protected override AttackInfo GetAttackHitInfo()
  {
    return Object.op_Equality((Object) this.m_owner, (Object) null) ? (AttackInfo) null : this.m_owner.GetAttackInfos().Find<AttackInfo>((Predicate<AttackInfo>) (info => info.name == FieldGimmickCannonSpecial.NAME_ATTACKINFO)) ?? (AttackInfo) null;
  }

  protected override void UpdateStateStandBy() => this.SetState(FieldGimmickCannonBase.STATE.READY);

  protected override void UpdateStateReady()
  {
    if (Object.op_Inequality((Object) this.m_owner, (Object) null) && this.m_seIdChargeMax > 0 && this.m_owner.IsCannonFullCharged() && !this.isPlayedChargeMaxSE)
    {
      SoundManager.PlayOneShotSE(this.m_seIdChargeMax, this._transform.position);
      this.isPlayedChargeMaxSE = true;
    }
    if (!this.IsRemainCoolTime())
      return;
    this.SetState(FieldGimmickCannonBase.STATE.COOLTIME);
  }

  public override void ApplyCannonVector(Vector3 cannonVec)
  {
  }

  public void StartCharge()
  {
    if (MonoBehaviourSingleton<EffectManager>.IsValid())
    {
      this.m_effectChargeTrans = EffectManager.GetEffect(FieldGimmickCannonSpecial.NAME_EFFECT_CHARGE, this.m_launchTrans);
      Transform effectChargeTrans = this.m_effectChargeTrans;
      effectChargeTrans.position = Vector3.op_Addition(effectChargeTrans.position, FieldGimmickCannonSpecial.OFFSET_BEAM_CHARGE_EFFECT);
    }
    if (this.m_seIdCharge <= 0)
      return;
    SoundManager.PlayOneShotSE(this.m_seIdCharge, this._transform.position);
  }

  public void ReleaseCharge()
  {
    if (MonoBehaviourSingleton<EffectManager>.IsValid() && Object.op_Inequality((Object) this.m_effectChargeTrans, (Object) null))
      EffectManager.ReleaseEffect(((Component) this.m_effectChargeTrans).gameObject);
    if (Object.op_Inequality((Object) this.m_owner, (Object) null))
      this.m_owner.ClearCannonChargeRate();
    this.isPlayedChargeMaxSE = false;
  }
}
