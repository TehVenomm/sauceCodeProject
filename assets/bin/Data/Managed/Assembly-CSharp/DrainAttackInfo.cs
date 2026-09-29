// Decompiled with JetBrains decompiler
// Type: DrainAttackInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class DrainAttackInfo
{
  [Tooltip("ドレイン攻撃ID")]
  public int id;
  [Tooltip("継続ダメージ間隔(sec)")]
  public float damageInterval;
  [Tooltip("継続ダメージ値(0-100指定)")]
  public float damageRate;
  [Tooltip("ドレイン攻撃によるHP回復間隔(sec)")]
  public float recoverInterval;
  [Tooltip("ドレイン攻撃によるHP回復値(0-100指定)")]
  public float recoverRate;

  public void Copy(DrainAttackInfo src)
  {
    this.id = src.id;
    this.damageInterval = src.damageInterval;
    this.damageRate = src.damageRate;
    this.recoverInterval = src.recoverInterval;
    this.recoverRate = src.recoverRate;
  }
}
