// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.Bloom
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[RequireComponent(typeof (Camera))]
[AddComponentMenu("Image Effects/Bloom and Glow/Bloom")]
public class Bloom : PostEffectsBase
{
  public Bloom.TweakMode tweakMode;
  public Bloom.BloomScreenBlendMode screenBlendMode = Bloom.BloomScreenBlendMode.Add;
  public Bloom.HDRBloomMode hdr;
  private bool doHdr;
  public float sepBlurSpread = 2.5f;
  public Bloom.BloomQuality quality = Bloom.BloomQuality.High;
  public float bloomIntensity = 0.5f;
  public float bloomThreshold = 0.5f;
  public Color bloomThresholdColor = Color.white;
  public int bloomBlurIterations = 2;
  public int hollywoodFlareBlurIterations = 2;
  public float flareRotation;
  public Bloom.LensFlareStyle lensflareMode = Bloom.LensFlareStyle.Anamorphic;
  public float hollyStretchWidth = 2.5f;
  public float lensflareIntensity;
  public float lensflareThreshold = 0.3f;
  public float lensFlareSaturation = 0.75f;
  public Color flareColorA = new Color(0.4f, 0.4f, 0.8f, 0.75f);
  public Color flareColorB = new Color(0.4f, 0.8f, 0.8f, 0.75f);
  public Color flareColorC = new Color(0.8f, 0.4f, 0.8f, 0.75f);
  public Color flareColorD = new Color(0.8f, 0.4f, 0.0f, 0.75f);
  public Texture2D lensFlareVignetteMask;
  public Shader lensFlareShader;
  private Material lensFlareMaterial;
  public Shader screenBlendShader;
  private Material screenBlend;
  public Shader blurAndFlaresShader;
  private Material blurAndFlaresMaterial;
  public Shader brightPassFilterShader;
  private Material brightPassFilterMaterial;

  public override bool CheckResources()
  {
    this.CheckSupport(false);
    this.screenBlend = this.CheckShaderAndCreateMaterial(this.screenBlendShader, this.screenBlend);
    this.lensFlareMaterial = this.CheckShaderAndCreateMaterial(this.lensFlareShader, this.lensFlareMaterial);
    this.blurAndFlaresMaterial = this.CheckShaderAndCreateMaterial(this.blurAndFlaresShader, this.blurAndFlaresMaterial);
    this.brightPassFilterMaterial = this.CheckShaderAndCreateMaterial(this.brightPassFilterShader, this.brightPassFilterMaterial);
    if (!this.isSupported)
      this.ReportAutoDisable();
    return this.isSupported;
  }

  public void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    if (!this.CheckResources())
    {
      Graphics.Blit((Texture) source, destination);
    }
    else
    {
      this.doHdr = false;
      this.doHdr = this.hdr != Bloom.HDRBloomMode.Auto ? this.hdr == Bloom.HDRBloomMode.On : source.format == 2 && ((Component) this).GetComponent<Camera>().allowHDR;
      this.doHdr = this.doHdr && this.supportHDRTextures;
      Bloom.BloomScreenBlendMode bloomScreenBlendMode = this.screenBlendMode;
      if (this.doHdr)
        bloomScreenBlendMode = Bloom.BloomScreenBlendMode.Add;
      RenderTextureFormat renderTextureFormat = this.doHdr ? (RenderTextureFormat) 2 : (RenderTextureFormat) 7;
      int num1 = ((Texture) source).width / 2;
      int num2 = ((Texture) source).height / 2;
      int num3 = ((Texture) source).width / 4;
      int num4 = ((Texture) source).height / 4;
      float num5 = (float) (1.0 * (double) ((Texture) source).width / (1.0 * (double) ((Texture) source).height));
      float num6 = 1f / 512f;
      RenderTexture temporary1 = RenderTexture.GetTemporary(num3, num4, 0, renderTextureFormat);
      RenderTexture temporary2 = RenderTexture.GetTemporary(num1, num2, 0, renderTextureFormat);
      if (this.quality > Bloom.BloomQuality.Cheap)
      {
        Graphics.Blit((Texture) source, temporary2, this.screenBlend, 2);
        RenderTexture temporary3 = RenderTexture.GetTemporary(num3, num4, 0, renderTextureFormat);
        Graphics.Blit((Texture) temporary2, temporary3, this.screenBlend, 2);
        Graphics.Blit((Texture) temporary3, temporary1, this.screenBlend, 6);
        RenderTexture.ReleaseTemporary(temporary3);
      }
      else
      {
        Graphics.Blit((Texture) source, temporary2);
        Graphics.Blit((Texture) temporary2, temporary1, this.screenBlend, 6);
      }
      RenderTexture.ReleaseTemporary(temporary2);
      RenderTexture renderTexture1 = RenderTexture.GetTemporary(num3, num4, 0, renderTextureFormat);
      this.BrightFilter(Color.op_Multiply(this.bloomThreshold, this.bloomThresholdColor), temporary1, renderTexture1);
      if (this.bloomBlurIterations < 1)
        this.bloomBlurIterations = 1;
      else if (this.bloomBlurIterations > 10)
        this.bloomBlurIterations = 10;
      for (int index = 0; index < this.bloomBlurIterations; ++index)
      {
        float num7 = (float) (1.0 + (double) index * 0.25) * this.sepBlurSpread;
        RenderTexture temporary4 = RenderTexture.GetTemporary(num3, num4, 0, renderTextureFormat);
        this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(0.0f, num7 * num6, 0.0f, 0.0f));
        Graphics.Blit((Texture) renderTexture1, temporary4, this.blurAndFlaresMaterial, 4);
        RenderTexture.ReleaseTemporary(renderTexture1);
        RenderTexture renderTexture2 = temporary4;
        RenderTexture temporary5 = RenderTexture.GetTemporary(num3, num4, 0, renderTextureFormat);
        this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num7 / num5 * num6, 0.0f, 0.0f, 0.0f));
        Graphics.Blit((Texture) renderTexture2, temporary5, this.blurAndFlaresMaterial, 4);
        RenderTexture.ReleaseTemporary(renderTexture2);
        renderTexture1 = temporary5;
        if (this.quality > Bloom.BloomQuality.Cheap)
        {
          if (index == 0)
          {
            Graphics.SetRenderTarget(temporary1);
            GL.Clear(false, true, Color.black);
            Graphics.Blit((Texture) renderTexture1, temporary1);
          }
          else
          {
            temporary1.MarkRestoreExpected();
            Graphics.Blit((Texture) renderTexture1, temporary1, this.screenBlend, 10);
          }
        }
      }
      if (this.quality > Bloom.BloomQuality.Cheap)
      {
        Graphics.SetRenderTarget(renderTexture1);
        GL.Clear(false, true, Color.black);
        Graphics.Blit((Texture) temporary1, renderTexture1, this.screenBlend, 6);
      }
      if ((double) this.lensflareIntensity > (double) Mathf.Epsilon)
      {
        RenderTexture temporary6 = RenderTexture.GetTemporary(num3, num4, 0, renderTextureFormat);
        if (this.lensflareMode == Bloom.LensFlareStyle.Ghosting)
        {
          this.BrightFilter(this.lensflareThreshold, renderTexture1, temporary6);
          if (this.quality > Bloom.BloomQuality.Cheap)
          {
            this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(0.0f, (float) (1.5 / (1.0 * (double) ((Texture) temporary1).height)), 0.0f, 0.0f));
            Graphics.SetRenderTarget(temporary1);
            GL.Clear(false, true, Color.black);
            Graphics.Blit((Texture) temporary6, temporary1, this.blurAndFlaresMaterial, 4);
            this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4((float) (1.5 / (1.0 * (double) ((Texture) temporary1).width)), 0.0f, 0.0f, 0.0f));
            Graphics.SetRenderTarget(temporary6);
            GL.Clear(false, true, Color.black);
            Graphics.Blit((Texture) temporary1, temporary6, this.blurAndFlaresMaterial, 4);
          }
          this.Vignette(0.975f, temporary6, temporary6);
          this.BlendFlares(temporary6, renderTexture1);
        }
        else
        {
          float num8 = 1f * Mathf.Cos(this.flareRotation);
          float num9 = 1f * Mathf.Sin(this.flareRotation);
          float num10 = this.hollyStretchWidth * 1f / num5 * num6;
          this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num8, num9, 0.0f, 0.0f));
          this.blurAndFlaresMaterial.SetVector("_Threshhold", new Vector4(this.lensflareThreshold, 1f, 0.0f, 0.0f));
          this.blurAndFlaresMaterial.SetVector("_TintColor", Vector4.op_Multiply(Vector4.op_Multiply(new Vector4(this.flareColorA.r, this.flareColorA.g, this.flareColorA.b, this.flareColorA.a), this.flareColorA.a), this.lensflareIntensity));
          this.blurAndFlaresMaterial.SetFloat("_Saturation", this.lensFlareSaturation);
          temporary1.DiscardContents();
          Graphics.Blit((Texture) temporary6, temporary1, this.blurAndFlaresMaterial, 2);
          temporary6.DiscardContents();
          Graphics.Blit((Texture) temporary1, temporary6, this.blurAndFlaresMaterial, 3);
          this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num8 * num10, num9 * num10, 0.0f, 0.0f));
          this.blurAndFlaresMaterial.SetFloat("_StretchWidth", this.hollyStretchWidth);
          temporary1.DiscardContents();
          Graphics.Blit((Texture) temporary6, temporary1, this.blurAndFlaresMaterial, 1);
          this.blurAndFlaresMaterial.SetFloat("_StretchWidth", this.hollyStretchWidth * 2f);
          temporary6.DiscardContents();
          Graphics.Blit((Texture) temporary1, temporary6, this.blurAndFlaresMaterial, 1);
          this.blurAndFlaresMaterial.SetFloat("_StretchWidth", this.hollyStretchWidth * 4f);
          temporary1.DiscardContents();
          Graphics.Blit((Texture) temporary6, temporary1, this.blurAndFlaresMaterial, 1);
          for (int index = 0; index < this.hollywoodFlareBlurIterations; ++index)
          {
            float num11 = this.hollyStretchWidth * 2f / num5 * num6;
            this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num11 * num8, num11 * num9, 0.0f, 0.0f));
            temporary6.DiscardContents();
            Graphics.Blit((Texture) temporary1, temporary6, this.blurAndFlaresMaterial, 4);
            this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num11 * num8, num11 * num9, 0.0f, 0.0f));
            temporary1.DiscardContents();
            Graphics.Blit((Texture) temporary6, temporary1, this.blurAndFlaresMaterial, 4);
          }
          if (this.lensflareMode == Bloom.LensFlareStyle.Anamorphic)
          {
            this.AddTo(1f, temporary1, renderTexture1);
          }
          else
          {
            this.Vignette(1f, temporary1, temporary6);
            this.BlendFlares(temporary6, temporary1);
            this.AddTo(1f, temporary1, renderTexture1);
          }
        }
        RenderTexture.ReleaseTemporary(temporary6);
      }
      int num12 = (int) bloomScreenBlendMode;
      this.screenBlend.SetFloat("_Intensity", this.bloomIntensity);
      this.screenBlend.SetTexture("_ColorBuffer", (Texture) source);
      if (this.quality > Bloom.BloomQuality.Cheap)
      {
        RenderTexture temporary7 = RenderTexture.GetTemporary(num1, num2, 0, renderTextureFormat);
        Graphics.Blit((Texture) renderTexture1, temporary7);
        Graphics.Blit((Texture) temporary7, destination, this.screenBlend, num12);
        RenderTexture.ReleaseTemporary(temporary7);
      }
      else
        Graphics.Blit((Texture) renderTexture1, destination, this.screenBlend, num12);
      RenderTexture.ReleaseTemporary(temporary1);
      RenderTexture.ReleaseTemporary(renderTexture1);
    }
  }

  private void AddTo(float intensity_, RenderTexture from, RenderTexture to)
  {
    this.screenBlend.SetFloat("_Intensity", intensity_);
    to.MarkRestoreExpected();
    Graphics.Blit((Texture) from, to, this.screenBlend, 9);
  }

  private void BlendFlares(RenderTexture from, RenderTexture to)
  {
    this.lensFlareMaterial.SetVector("colorA", Vector4.op_Multiply(new Vector4(this.flareColorA.r, this.flareColorA.g, this.flareColorA.b, this.flareColorA.a), this.lensflareIntensity));
    this.lensFlareMaterial.SetVector("colorB", Vector4.op_Multiply(new Vector4(this.flareColorB.r, this.flareColorB.g, this.flareColorB.b, this.flareColorB.a), this.lensflareIntensity));
    this.lensFlareMaterial.SetVector("colorC", Vector4.op_Multiply(new Vector4(this.flareColorC.r, this.flareColorC.g, this.flareColorC.b, this.flareColorC.a), this.lensflareIntensity));
    this.lensFlareMaterial.SetVector("colorD", Vector4.op_Multiply(new Vector4(this.flareColorD.r, this.flareColorD.g, this.flareColorD.b, this.flareColorD.a), this.lensflareIntensity));
    to.MarkRestoreExpected();
    Graphics.Blit((Texture) from, to, this.lensFlareMaterial);
  }

  private void BrightFilter(float thresh, RenderTexture from, RenderTexture to)
  {
    this.brightPassFilterMaterial.SetVector("_Threshhold", new Vector4(thresh, thresh, thresh, thresh));
    Graphics.Blit((Texture) from, to, this.brightPassFilterMaterial, 0);
  }

  private void BrightFilter(Color threshColor, RenderTexture from, RenderTexture to)
  {
    this.brightPassFilterMaterial.SetVector("_Threshhold", Color.op_Implicit(threshColor));
    Graphics.Blit((Texture) from, to, this.brightPassFilterMaterial, 1);
  }

  private void Vignette(float amount, RenderTexture from, RenderTexture to)
  {
    if (Object.op_Implicit((Object) this.lensFlareVignetteMask))
    {
      this.screenBlend.SetTexture("_ColorBuffer", (Texture) this.lensFlareVignetteMask);
      to.MarkRestoreExpected();
      Graphics.Blit(Object.op_Equality((Object) from, (Object) to) ? (Texture) null : (Texture) from, to, this.screenBlend, Object.op_Equality((Object) from, (Object) to) ? 7 : 3);
    }
    else
    {
      if (!Object.op_Inequality((Object) from, (Object) to))
        return;
      Graphics.SetRenderTarget(to);
      GL.Clear(false, true, Color.black);
      Graphics.Blit((Texture) from, to);
    }
  }

  public enum LensFlareStyle
  {
    Ghosting,
    Anamorphic,
    Combined,
  }

  public enum TweakMode
  {
    Basic,
    Complex,
  }

  public enum HDRBloomMode
  {
    Auto,
    On,
    Off,
  }

  public enum BloomScreenBlendMode
  {
    Screen,
    Add,
  }

  public enum BloomQuality
  {
    Cheap,
    High,
  }
}
