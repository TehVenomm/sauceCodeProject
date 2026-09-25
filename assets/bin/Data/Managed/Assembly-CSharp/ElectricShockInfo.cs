// Decompiled with JetBrains decompiler
// Type: ElectricShockInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class ElectricShockInfo
{
  [Tooltip("効果時間(sec)")]
  public float duration;
  [Tooltip("インターバル時間(sec)")]
  public float damageInterval;
  [Tooltip("効果倍率(%)")]
  public int atkRate = 1;

  public void Copy(ElectricShockInfo src)
  {
    this.duration = src.duration;
    this.damageInterval = src.damageInterval;
    this.atkRate = src.atkRate;
  }
}
