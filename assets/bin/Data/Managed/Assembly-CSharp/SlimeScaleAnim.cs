// Decompiled with JetBrains decompiler
// Type: SlimeScaleAnim
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class SlimeScaleAnim : SlimeAnimBase<Vector3>
{
  public override Vector3 UpdateAnim()
  {
    float num1 = this.nowTime / this.playTime;
    float num2 = this.animCurve.Evaluate(num1);
    if (this.isBlend && (double) this.nowTime <= (double) this.blendEndTime)
    {
      float num3 = this.blendCurve.Evaluate(num1);
      num2 = this.blendParam.x + (num2 - this.blendParam.x) * num3;
    }
    return new Vector3(num2, num2, num2);
  }
}
