// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.ContrastEnhance
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[RequireComponent(typeof (Camera))]
[AddComponentMenu("Image Effects/Color Adjustments/Contrast Enhance (Unsharp Mask)")]
public class ContrastEnhance : PostEffectsBase
{
  [Range(0.0f, 1f)]
  public float intensity = 0.5f;
  [Range(0.0f, 0.999f)]
  public float threshold;
  private Material separableBlurMaterial;
  private Material contrastCompositeMaterial;
  [Range(0.0f, 1f)]
  public float blurSpread = 1f;
  public Shader separableBlurShader;
  public Shader contrastCompositeShader;

  public override bool CheckResources()
  {
    this.CheckSupport(false);
    this.contrastCompositeMaterial = this.CheckShaderAndCreateMaterial(this.contrastCompositeShader, this.contrastCompositeMaterial);
    this.separableBlurMaterial = this.CheckShaderAndCreateMaterial(this.separableBlurShader, this.separableBlurMaterial);
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
      int width = ((Texture) source).width;
      int height = ((Texture) source).height;
      RenderTexture temporary1 = RenderTexture.GetTemporary(width / 2, height / 2, 0);
      Graphics.Blit((Texture) source, temporary1);
      RenderTexture temporary2 = RenderTexture.GetTemporary(width / 4, height / 4, 0);
      Graphics.Blit((Texture) temporary1, temporary2);
      RenderTexture.ReleaseTemporary(temporary1);
      this.separableBlurMaterial.SetVector("offsets", new Vector4(0.0f, this.blurSpread * 1f / (float) ((Texture) temporary2).height, 0.0f, 0.0f));
      RenderTexture temporary3 = RenderTexture.GetTemporary(width / 4, height / 4, 0);
      Graphics.Blit((Texture) temporary2, temporary3, this.separableBlurMaterial);
      RenderTexture.ReleaseTemporary(temporary2);
      this.separableBlurMaterial.SetVector("offsets", new Vector4(this.blurSpread * 1f / (float) ((Texture) temporary2).width, 0.0f, 0.0f, 0.0f));
      RenderTexture temporary4 = RenderTexture.GetTemporary(width / 4, height / 4, 0);
      Graphics.Blit((Texture) temporary3, temporary4, this.separableBlurMaterial);
      RenderTexture.ReleaseTemporary(temporary3);
      this.contrastCompositeMaterial.SetTexture("_MainTexBlurred", (Texture) temporary4);
      this.contrastCompositeMaterial.SetFloat("intensity", this.intensity);
      this.contrastCompositeMaterial.SetFloat("threshold", this.threshold);
      Graphics.Blit((Texture) source, destination, this.contrastCompositeMaterial);
      RenderTexture.ReleaseTemporary(temporary4);
    }
  }
}
