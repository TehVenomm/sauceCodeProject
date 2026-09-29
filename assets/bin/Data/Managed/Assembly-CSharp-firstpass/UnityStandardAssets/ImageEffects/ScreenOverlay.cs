// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.ScreenOverlay
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[RequireComponent(typeof (Camera))]
[AddComponentMenu("Image Effects/Other/Screen Overlay")]
public class ScreenOverlay : PostEffectsBase
{
  public ScreenOverlay.OverlayBlendMode blendMode = ScreenOverlay.OverlayBlendMode.Overlay;
  public float intensity = 1f;
  public Texture2D texture;
  public Shader overlayShader;
  private Material overlayMaterial;

  public override bool CheckResources()
  {
    this.CheckSupport(false);
    this.overlayMaterial = this.CheckShaderAndCreateMaterial(this.overlayShader, this.overlayMaterial);
    if (!this.isSupported)
      this.ReportAutoDisable();
    return this.isSupported;
  }

  private void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    if (!this.CheckResources())
    {
      Graphics.Blit((Texture) source, destination);
    }
    else
    {
      Vector4 vector4;
      // ISSUE: explicit constructor call
      ((Vector4) ref vector4).\u002Ector(1f, 0.0f, 0.0f, 1f);
      this.overlayMaterial.SetVector("_UV_Transform", vector4);
      this.overlayMaterial.SetFloat("_Intensity", this.intensity);
      this.overlayMaterial.SetTexture("_Overlay", (Texture) this.texture);
      Graphics.Blit((Texture) source, destination, this.overlayMaterial, (int) this.blendMode);
    }
  }

  public enum OverlayBlendMode
  {
    Additive,
    ScreenBlend,
    Multiply,
    Overlay,
    AlphaBlend,
  }
}
