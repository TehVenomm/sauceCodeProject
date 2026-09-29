// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.BloomOptimized
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[RequireComponent(typeof (Camera))]
[AddComponentMenu("Image Effects/Bloom and Glow/Bloom (Optimized)")]
public class BloomOptimized : PostEffectsBase
{
  [Range(0.0f, 1.5f)]
  public float threshold = 0.25f;
  [Range(0.0f, 2.5f)]
  public float intensity = 0.75f;
  [Range(0.25f, 5.5f)]
  public float blurSize = 1f;
  private BloomOptimized.Resolution resolution;
  [Range(1f, 4f)]
  public int blurIterations = 1;
  public BloomOptimized.BlurType blurType;
  public Shader fastBloomShader;
  private Material fastBloomMaterial;

  public override bool CheckResources()
  {
    this.CheckSupport(false);
    this.fastBloomMaterial = this.CheckShaderAndCreateMaterial(this.fastBloomShader, this.fastBloomMaterial);
    if (!this.isSupported)
      this.ReportAutoDisable();
    return this.isSupported;
  }

  private void OnDisable()
  {
    if (!Object.op_Implicit((Object) this.fastBloomMaterial))
      return;
    Object.DestroyImmediate((Object) this.fastBloomMaterial);
  }

  private void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    if (!this.CheckResources())
    {
      Graphics.Blit((Texture) source, destination);
    }
    else
    {
      int num1 = this.resolution == BloomOptimized.Resolution.Low ? 4 : 2;
      float num2 = this.resolution == BloomOptimized.Resolution.Low ? 0.5f : 1f;
      this.fastBloomMaterial.SetVector("_Parameter", new Vector4(this.blurSize * num2, 0.0f, this.threshold, this.intensity));
      ((Texture) source).filterMode = (FilterMode) 1;
      int num3 = ((Texture) source).width / num1;
      int num4 = ((Texture) source).height / num1;
      RenderTexture renderTexture1 = RenderTexture.GetTemporary(num3, num4, 0, source.format);
      ((Texture) renderTexture1).filterMode = (FilterMode) 1;
      Graphics.Blit((Texture) source, renderTexture1, this.fastBloomMaterial, 1);
      int num5 = this.blurType == BloomOptimized.BlurType.Standard ? 0 : 2;
      for (int index = 0; index < this.blurIterations; ++index)
      {
        this.fastBloomMaterial.SetVector("_Parameter", new Vector4((float) ((double) this.blurSize * (double) num2 + (double) index * 1.0), 0.0f, this.threshold, this.intensity));
        RenderTexture temporary1 = RenderTexture.GetTemporary(num3, num4, 0, source.format);
        ((Texture) temporary1).filterMode = (FilterMode) 1;
        Graphics.Blit((Texture) renderTexture1, temporary1, this.fastBloomMaterial, 2 + num5);
        RenderTexture.ReleaseTemporary(renderTexture1);
        RenderTexture renderTexture2 = temporary1;
        RenderTexture temporary2 = RenderTexture.GetTemporary(num3, num4, 0, source.format);
        ((Texture) temporary2).filterMode = (FilterMode) 1;
        Graphics.Blit((Texture) renderTexture2, temporary2, this.fastBloomMaterial, 3 + num5);
        RenderTexture.ReleaseTemporary(renderTexture2);
        renderTexture1 = temporary2;
      }
      this.fastBloomMaterial.SetTexture("_Bloom", (Texture) renderTexture1);
      Graphics.Blit((Texture) source, destination, this.fastBloomMaterial, 0);
      RenderTexture.ReleaseTemporary(renderTexture1);
    }
  }

  public enum Resolution
  {
    Low,
    High,
  }

  public enum BlurType
  {
    Standard,
    Sgx,
  }
}
