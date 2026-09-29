// Decompiled with JetBrains decompiler
// Type: Vector4Interpolator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class Vector4Interpolator : InterpolatorBase<Vector4>
{
  protected override void Calc(float t, float r)
  {
    Vector4 vector4 = Vector4.op_Addition(Vector4.op_Multiply(Vector4.op_Subtraction(this.endValue, this.beginValue), r), this.beginValue);
    if (this.addCurve != null && this.addCurve.length > 0)
      vector4 = Vector4.op_Addition(Vector4.op_Multiply(this.addValue, this.addCurve.Evaluate(t)), vector4);
    this.nowValue = vector4;
  }
}
