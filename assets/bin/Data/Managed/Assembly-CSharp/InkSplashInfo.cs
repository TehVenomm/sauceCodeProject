// Decompiled with JetBrains decompiler
// Type: InkSplashInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class InkSplashInfo
{
  [Tooltip("効果時間(sec)")]
  public float duration;
  [Tooltip("フリックによる減少時間(sec)")]
  public float reduceTimeByFlick;
  [Tooltip("フリックアイコンの座標")]
  public Vector3 flickIconPos = new Vector3(0.0f, 0.0f, 1f);

  public void Copy(InkSplashInfo src)
  {
    this.duration = src.duration;
    this.reduceTimeByFlick = src.reduceTimeByFlick;
    this.flickIconPos = src.flickIconPos;
  }
}
