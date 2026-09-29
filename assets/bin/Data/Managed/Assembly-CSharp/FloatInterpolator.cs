// Decompiled with JetBrains decompiler
// Type: FloatInterpolator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class FloatInterpolator : InterpolatorBase<float>
{
  protected override void Calc(float t, float r)
  {
    float num = (this.endValue - this.beginValue) * r + this.beginValue;
    if (this.addCurve != null && this.addCurve.length > 0)
      num = this.addValue * this.addCurve.Evaluate(t) + num;
    this.nowValue = num;
  }
}
