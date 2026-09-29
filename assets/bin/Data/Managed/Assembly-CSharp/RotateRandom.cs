// Decompiled with JetBrains decompiler
// Type: RotateRandom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class RotateRandom : MonoBehaviour
{
  public float Xrandom_low;
  public float Xrandom_high = 360f;
  public float Yrandom_low;
  public float Yrandom_high = 360f;
  public float Zrandom_low;
  public float Zrandom_high = 360f;

  private void OnEnable()
  {
    ((Component) this).transform.rotation = Quaternion.Euler(Random.Range(this.Xrandom_low, this.Xrandom_high), Random.Range(this.Yrandom_low, this.Yrandom_high), Random.Range(this.Zrandom_low, this.Zrandom_high));
  }
}
