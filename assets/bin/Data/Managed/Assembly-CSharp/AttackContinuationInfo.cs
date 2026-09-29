// Decompiled with JetBrains decompiler
// Type: AttackContinuationInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class AttackContinuationInfo : AttackInfo
{
  [Tooltip("効果タイプ")]
  public AttackContinuationInfo.CONTINUATION_TYPE type;
  [Tooltip("座標固定化設定を無効化するか")]
  public bool disableUpdateFixTrans;
  public AttackContinuationInfo.Inhale inhale = new AttackContinuationInfo.Inhale();
  public AttackContinuationInfo.ByContinuationType infoByContinuationType = new AttackContinuationInfo.ByContinuationType();

  public override AttackInfo GetRateAttackInfo(AttackInfo rate_info, float rate)
  {
    AttackContinuationInfo rateAttackInfo = base.GetRateAttackInfo(rate_info, rate) as AttackContinuationInfo;
    if (rateAttackInfo == this)
      return (AttackInfo) rateAttackInfo;
    if (!(rate_info is AttackContinuationInfo continuationInfo))
      return (AttackInfo) rateAttackInfo;
    rateAttackInfo.type = this.type;
    rateAttackInfo.disableUpdateFixTrans = this.disableUpdateFixTrans;
    if (rateAttackInfo.type != continuationInfo.type)
      return (AttackInfo) rateAttackInfo;
    rateAttackInfo.inhale.speed = AttackInfo.GetRateValue(this.inhale.speed, continuationInfo.inhale.speed, rate);
    return (AttackInfo) rateAttackInfo;
  }

  protected override AttackInfo CreateInfo() => (AttackInfo) new AttackContinuationInfo();

  public override void Copy(ref AttackInfo rInfo)
  {
    base.Copy(ref rInfo);
    if (!(rInfo is AttackContinuationInfo continuationInfo))
      return;
    continuationInfo.type = this.type;
    continuationInfo.disableUpdateFixTrans = this.disableUpdateFixTrans;
    continuationInfo.inhale.speed = this.inhale.speed;
  }

  public enum CONTINUATION_TYPE
  {
    NONE,
    INHALE,
    BARRIER,
    SPEAR_BURST,
  }

  [Serializable]
  public class Inhale
  {
    [Tooltip("吸い込み速度(距離/s)")]
    public float speed;
  }

  [Serializable]
  public class Barrier
  {
  }

  [Serializable]
  public class ByContinuationType
  {
    public AttackContinuationInfo.Barrier barrier = new AttackContinuationInfo.Barrier();
  }
}
