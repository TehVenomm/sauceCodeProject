// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.ColorCorrectionRamp
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Adjustments/Color Correction (Ramp)")]
public class ColorCorrectionRamp : ImageEffectBase
{
  public Texture textureRamp;

  private void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    this.material.SetTexture("_RampTex", this.textureRamp);
    Graphics.Blit((Texture) source, destination, this.material);
  }
}
