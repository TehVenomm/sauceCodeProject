// Decompiled with JetBrains decompiler
// Type: SlimeColorAnim
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class SlimeColorAnim : SlimeAnimBase<Color>
{
  public override Color UpdateAnim()
  {
    if (!this.isPlaying)
      return new Color(1f, 1f, 1f, 0.0f);
    float num1 = this.nowTime / this.playTime;
    float num2 = this.animCurve.Evaluate(num1);
    if (this.isBlend && (double) this.nowTime <= (double) this.blendEndTime)
    {
      float num3 = this.blendCurve.Evaluate(num1);
      num2 = this.blendParam.a + (num2 - this.blendParam.a) * num3;
    }
    return new Color(1f, 1f, 1f, num2);
  }
}
