// Decompiled with JetBrains decompiler
// Type: ShakeInterpolator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class ShakeInterpolator : InterpolatorBase<Vector3>
{
  private Vector3Interpolator shakeAnimPos = new Vector3Interpolator();

  protected override void Calc(float t, float r)
  {
    if (!this.shakeAnimPos.IsPlaying())
    {
      this.shakeAnimPos.Set(0.1f, this.beginValue, Vector3.op_Addition(this.beginValue, new Vector3(Random.Range(-1f, 1f) * this.addValue.x, Random.Range(-1f, 1f) * this.addValue.y, 0.0f)), (AnimationCurve) null, new Vector3(), (AnimationCurve) null);
      this.shakeAnimPos.Play();
    }
    this.nowValue = this.shakeAnimPos.Update();
  }
}
