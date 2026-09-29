// Decompiled with JetBrains decompiler
// Type: AngleVector3Interpolator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class AngleVector3Interpolator : Vector3Interpolator
{
  protected override void Calc(float t, float r)
  {
    Vector3 vector3;
    vector3.x = Mathf.LerpAngle(this.beginValue.x, this.endValue.x, r);
    vector3.y = Mathf.LerpAngle(this.beginValue.y, this.endValue.y, r);
    vector3.z = Mathf.LerpAngle(this.beginValue.z, this.endValue.z, r);
    if (this.addCurve != null && this.addCurve.length > 0)
      vector3 = Vector3.op_Addition(Vector3.op_Multiply(this.addValue, this.addCurve.Evaluate(t)), vector3);
    this.nowValue = vector3;
  }
}
