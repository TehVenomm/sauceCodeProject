// Decompiled with JetBrains decompiler
// Type: ColorInterpolator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class ColorInterpolator : InterpolatorBase<Color>
{
  protected override void Calc(float t, float r)
  {
    Color color = Color.op_Addition(Color.op_Multiply(Color.op_Subtraction(this.endValue, this.beginValue), r), this.beginValue);
    if (this.addCurve != null && this.addCurve.length > 0)
      color = Color.op_Addition(Color.op_Multiply(this.addValue, this.addCurve.Evaluate(t)), color);
    if ((double) color.r < 0.0)
      color.r = 0.0f;
    else if ((double) color.r > 1.0)
      color.r = 1f;
    if ((double) color.g < 0.0)
      color.g = 0.0f;
    else if ((double) color.g > 1.0)
      color.g = 1f;
    if ((double) color.b < 0.0)
      color.b = 0.0f;
    else if ((double) color.b > 1.0)
      color.b = 1f;
    if ((double) color.a < 0.0)
      color.a = 0.0f;
    else if ((double) color.a > 1.0)
      color.a = 1f;
    this.nowValue = color;
  }
}
