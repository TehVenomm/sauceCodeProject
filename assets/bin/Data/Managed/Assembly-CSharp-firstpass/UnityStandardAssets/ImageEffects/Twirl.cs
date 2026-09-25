// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.Twirl
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Displacement/Twirl")]
public class Twirl : ImageEffectBase
{
  public Vector2 radius = new Vector2(0.3f, 0.3f);
  [Range(0.0f, 360f)]
  public float angle = 50f;
  public Vector2 center = new Vector2(0.5f, 0.5f);

  private void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    UnityStandardAssets.ImageEffects.ImageEffects.RenderDistortion(this.material, source, destination, this.angle, this.center, this.radius);
  }
}
