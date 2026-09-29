// Decompiled with JetBrains decompiler
// Type: BlurAndTurbulanceFilter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class BlurAndTurbulanceFilter : MonoBehaviour
{
  [SerializeField]
  private Material blurMaterial;
  [SerializeField]
  private Material turbulanceMaterial;
  [SerializeField]
  private Vector2 center;
  [SerializeField]
  private Vector2 scroll;
  [SerializeField]
  private float blurPower;
  [SerializeField]
  private float turbulancePower;
  [SerializeField]
  private float scale = 1f;
  [SerializeField]
  private float brightness;

  public void SetBlurPram(float _power, Vector2 _center)
  {
    this.blurPower = _power;
    this.center = _center;
  }

  public void SetTurbulanceParam(float _power, float _scale, float _brightness)
  {
    this.turbulancePower = _power;
    this.scale = _scale;
    this.brightness = _brightness;
  }

  private void Awake()
  {
    this.blurMaterial = new Material(ResourceUtility.FindShader("mobile/Custom/ImageEffect/RadialBlurFilter"));
  }
}
