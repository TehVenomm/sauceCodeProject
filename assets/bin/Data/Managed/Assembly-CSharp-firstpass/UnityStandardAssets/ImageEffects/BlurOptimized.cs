// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.BlurOptimized
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[RequireComponent(typeof (Camera))]
[AddComponentMenu("Image Effects/Blur/Blur (Optimized)")]
public class BlurOptimized : PostEffectsBase
{
  [Range(0.0f, 2f)]
  public int downsample = 1;
  [Range(0.0f, 10f)]
  public float blurSize = 3f;
  [Range(1f, 4f)]
  public int blurIterations = 2;
  public BlurOptimized.BlurType blurType;
  public Shader blurShader;
  private Material blurMaterial;

  public override bool CheckResources()
  {
    this.CheckSupport(false);
    this.blurMaterial = this.CheckShaderAndCreateMaterial(this.blurShader, this.blurMaterial);
    if (!this.isSupported)
      this.ReportAutoDisable();
    return this.isSupported;
  }

  public void OnDisable()
  {
    if (!Object.op_Implicit((Object) this.blurMaterial))
      return;
    Object.DestroyImmediate((Object) this.blurMaterial);
  }

  public void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    if (!this.CheckResources())
    {
      Graphics.Blit((Texture) source, destination);
    }
    else
    {
      float num1 = (float) (1.0 / (1.0 * (double) (1 << this.downsample)));
      this.blurMaterial.SetVector("_Parameter", new Vector4(this.blurSize * num1, -this.blurSize * num1, 0.0f, 0.0f));
      ((Texture) source).filterMode = (FilterMode) 1;
      int num2 = ((Texture) source).width >> this.downsample;
      int num3 = ((Texture) source).height >> this.downsample;
      RenderTexture renderTexture1 = RenderTexture.GetTemporary(num2, num3, 0, source.format);
      ((Texture) renderTexture1).filterMode = (FilterMode) 1;
      Graphics.Blit((Texture) source, renderTexture1, this.blurMaterial, 0);
      int num4 = this.blurType == BlurOptimized.BlurType.StandardGauss ? 0 : 2;
      for (int index = 0; index < this.blurIterations; ++index)
      {
        float num5 = (float) index * 1f;
        this.blurMaterial.SetVector("_Parameter", new Vector4(this.blurSize * num1 + num5, -this.blurSize * num1 - num5, 0.0f, 0.0f));
        RenderTexture temporary1 = RenderTexture.GetTemporary(num2, num3, 0, source.format);
        ((Texture) temporary1).filterMode = (FilterMode) 1;
        Graphics.Blit((Texture) renderTexture1, temporary1, this.blurMaterial, 1 + num4);
        RenderTexture.ReleaseTemporary(renderTexture1);
        RenderTexture renderTexture2 = temporary1;
        RenderTexture temporary2 = RenderTexture.GetTemporary(num2, num3, 0, source.format);
        ((Texture) temporary2).filterMode = (FilterMode) 1;
        Graphics.Blit((Texture) renderTexture2, temporary2, this.blurMaterial, 2 + num4);
        RenderTexture.ReleaseTemporary(renderTexture2);
        renderTexture1 = temporary2;
      }
      Graphics.Blit((Texture) renderTexture1, destination);
      RenderTexture.ReleaseTemporary(renderTexture1);
    }
  }

  public enum BlurType
  {
    StandardGauss,
    SgxGauss,
  }
}
