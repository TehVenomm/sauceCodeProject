// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.CreaseShading
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[RequireComponent(typeof (Camera))]
[AddComponentMenu("Image Effects/Edge Detection/Crease Shading")]
public class CreaseShading : PostEffectsBase
{
  public float intensity = 0.5f;
  public int softness = 1;
  public float spread = 1f;
  public Shader blurShader;
  private Material blurMaterial;
  public Shader depthFetchShader;
  private Material depthFetchMaterial;
  public Shader creaseApplyShader;
  private Material creaseApplyMaterial;

  public override bool CheckResources()
  {
    this.CheckSupport(true);
    this.blurMaterial = this.CheckShaderAndCreateMaterial(this.blurShader, this.blurMaterial);
    this.depthFetchMaterial = this.CheckShaderAndCreateMaterial(this.depthFetchShader, this.depthFetchMaterial);
    this.creaseApplyMaterial = this.CheckShaderAndCreateMaterial(this.creaseApplyShader, this.creaseApplyMaterial);
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
      float num1 = (float) (1.0 * (double) width / (1.0 * (double) height));
      float num2 = 1f / 512f;
      RenderTexture temporary1 = RenderTexture.GetTemporary(width, height, 0);
      RenderTexture renderTexture1 = RenderTexture.GetTemporary(width / 2, height / 2, 0);
      Graphics.Blit((Texture) source, temporary1, this.depthFetchMaterial);
      Graphics.Blit((Texture) temporary1, renderTexture1);
      for (int index = 0; index < this.softness; ++index)
      {
        RenderTexture temporary2 = RenderTexture.GetTemporary(width / 2, height / 2, 0);
        this.blurMaterial.SetVector("offsets", new Vector4(0.0f, this.spread * num2, 0.0f, 0.0f));
        Graphics.Blit((Texture) renderTexture1, temporary2, this.blurMaterial);
        RenderTexture.ReleaseTemporary(renderTexture1);
        RenderTexture renderTexture2 = temporary2;
        RenderTexture temporary3 = RenderTexture.GetTemporary(width / 2, height / 2, 0);
        this.blurMaterial.SetVector("offsets", new Vector4(this.spread * num2 / num1, 0.0f, 0.0f, 0.0f));
        Graphics.Blit((Texture) renderTexture2, temporary3, this.blurMaterial);
        RenderTexture.ReleaseTemporary(renderTexture2);
        renderTexture1 = temporary3;
      }
      this.creaseApplyMaterial.SetTexture("_HrDepthTex", (Texture) temporary1);
      this.creaseApplyMaterial.SetTexture("_LrDepthTex", (Texture) renderTexture1);
      this.creaseApplyMaterial.SetFloat("intensity", this.intensity);
      Graphics.Blit((Texture) source, destination, this.creaseApplyMaterial);
      RenderTexture.ReleaseTemporary(temporary1);
      RenderTexture.ReleaseTemporary(renderTexture1);
    }
  }
}
