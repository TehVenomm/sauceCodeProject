// Decompiled with JetBrains decompiler
// Type: QuaternionInterpolator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class QuaternionInterpolator : InterpolatorBase<Quaternion>
{
  protected override void Calc(float t, float r)
  {
    if (this.addCurve != null && this.addCurve.length > 0)
      r *= this.addCurve.Evaluate(t);
    this.nowValue = Quaternion.Slerp(this.beginValue, this.endValue, r);
  }
}
