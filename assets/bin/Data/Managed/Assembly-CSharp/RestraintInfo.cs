// Decompiled with JetBrains decompiler
// Type: RestraintInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class RestraintInfo
{
  [Tooltip("有効かどうか")]
  public bool enable;
  [Tooltip("拘束時間(sec)")]
  public float duration;
  [Tooltip("継続ダメージ間隔(sec)")]
  public float damageInterval;
  [Tooltip("継続ダメージ値(0-100指定)")]
  public int damageRate;
  [Tooltip("フリックによる減少時間(sec)")]
  public float reduceTimeByFlick;
  [Tooltip("衝突半径")]
  public float radius;
  [Tooltip("エフェクト名")]
  public string effectName = string.Empty;
  [Tooltip("モーションを停止させるか？")]
  public bool isStopMotion;
  [Tooltip("仲間の攻撃で解除禁止か?")]
  public bool isDisableRemoveByPlayerAttack;

  public void Copy(RestraintInfo src)
  {
    this.enable = src.enable;
    this.duration = src.duration;
    this.damageInterval = src.damageInterval;
    this.damageRate = src.damageRate;
    this.reduceTimeByFlick = src.reduceTimeByFlick;
    this.radius = src.radius;
    this.effectName = src.effectName;
    this.isStopMotion = src.isStopMotion;
    this.isDisableRemoveByPlayerAttack = src.isDisableRemoveByPlayerAttack;
  }
}
