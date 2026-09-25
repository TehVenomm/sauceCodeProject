// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.SepiaTone
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Adjustments/Sepia Tone")]
public class SepiaTone : ImageEffectBase
{
  private void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    Graphics.Blit((Texture) source, destination, this.material);
  }
}
