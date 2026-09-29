// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.DepthOfFieldDeprecated
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[RequireComponent(typeof (Camera))]
[AddComponentMenu("Image Effects/Camera/Depth of Field (deprecated)")]
public class DepthOfFieldDeprecated : PostEffectsBase
{
  private static int SMOOTH_DOWNSAMPLE_PASS = 6;
  private static float BOKEH_EXTRA_BLUR = 2f;
  public DepthOfFieldDeprecated.Dof34QualitySetting quality = DepthOfFieldDeprecated.Dof34QualitySetting.OnlyBackground;
  public DepthOfFieldDeprecated.DofResolution resolution = DepthOfFieldDeprecated.DofResolution.Low;
  public bool simpleTweakMode = true;
  public float focalPoint = 1f;
  public float smoothness = 0.5f;
  public float focalZDistance;
  public float focalZStartCurve = 1f;
  public float focalZEndCurve = 1f;
  private float focalStartCurve = 2f;
  private float focalEndCurve = 2f;
  private float focalDistance01 = 0.1f;
  public Transform objectFocus;
  public float focalSize;
  public DepthOfFieldDeprecated.DofBlurriness bluriness = DepthOfFieldDeprecated.DofBlurriness.High;
  public float maxBlurSpread = 1.75f;
  public float foregroundBlurExtrude = 1.15f;
  public Shader dofBlurShader;
  private Material dofBlurMaterial;
  public Shader dofShader;
  private Material dofMaterial;
  public bool visualize;
  public DepthOfFieldDeprecated.BokehDestination bokehDestination = DepthOfFieldDeprecated.BokehDestination.Background;
  private float widthOverHeight = 1.25f;
  private float oneOverBaseSize = 1f / 512f;
  public bool bokeh;
  public bool bokehSupport = true;
  public Shader bokehShader;
  public Texture2D bokehTexture;
  public float bokehScale = 2.4f;
  public float bokehIntensity = 0.15f;
  public float bokehThresholdContrast = 0.1f;
  public float bokehThresholdLuminance = 0.55f;
  public int bokehDownsample = 1;
  private Material bokehMaterial;
  private Camera _camera;
  private RenderTexture foregroundTexture;
  private RenderTexture mediumRezWorkTexture;
  private RenderTexture finalDefocus;
  private RenderTexture lowRezWorkTexture;
  private RenderTexture bokehSource;
  private RenderTexture bokehSource2;

  private void CreateMaterials()
  {
    this.dofBlurMaterial = this.CheckShaderAndCreateMaterial(this.dofBlurShader, this.dofBlurMaterial);
    this.dofMaterial = this.CheckShaderAndCreateMaterial(this.dofShader, this.dofMaterial);
    this.bokehSupport = this.bokehShader.isSupported;
    if (!this.bokeh || !this.bokehSupport || !Object.op_Implicit((Object) this.bokehShader))
      return;
    this.bokehMaterial = this.CheckShaderAndCreateMaterial(this.bokehShader, this.bokehMaterial);
  }

  public override bool CheckResources()
  {
    this.CheckSupport(true);
    this.dofBlurMaterial = this.CheckShaderAndCreateMaterial(this.dofBlurShader, this.dofBlurMaterial);
    this.dofMaterial = this.CheckShaderAndCreateMaterial(this.dofShader, this.dofMaterial);
    this.bokehSupport = this.bokehShader.isSupported;
    if (this.bokeh && this.bokehSupport && Object.op_Implicit((Object) this.bokehShader))
      this.bokehMaterial = this.CheckShaderAndCreateMaterial(this.bokehShader, this.bokehMaterial);
    if (!this.isSupported)
      this.ReportAutoDisable();
    return this.isSupported;
  }

  private void OnDisable() => Quads.Cleanup();

  private void OnEnable()
  {
    this._camera = ((Component) this).GetComponent<Camera>();
    Camera camera = this._camera;
    camera.depthTextureMode = (DepthTextureMode) (camera.depthTextureMode | 1);
  }

  private float FocalDistance01(float worldDist)
  {
    return this._camera.WorldToViewportPoint(Vector3.op_Addition(Vector3.op_Multiply(worldDist - this._camera.nearClipPlane, ((Component) this._camera).transform.forward), ((Component) this._camera).transform.position)).z / (this._camera.farClipPlane - this._camera.nearClipPlane);
  }

  private int GetDividerBasedOnQuality()
  {
    int dividerBasedOnQuality = 1;
    if (this.resolution == DepthOfFieldDeprecated.DofResolution.Medium)
      dividerBasedOnQuality = 2;
    else if (this.resolution == DepthOfFieldDeprecated.DofResolution.Low)
      dividerBasedOnQuality = 2;
    return dividerBasedOnQuality;
  }

  private int GetLowResolutionDividerBasedOnQuality(int baseDivider)
  {
    int dividerBasedOnQuality = baseDivider;
    if (this.resolution == DepthOfFieldDeprecated.DofResolution.High)
      dividerBasedOnQuality *= 2;
    if (this.resolution == DepthOfFieldDeprecated.DofResolution.Low)
      dividerBasedOnQuality *= 2;
    return dividerBasedOnQuality;
  }

  private void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    if (!this.CheckResources())
    {
      Graphics.Blit((Texture) source, destination);
    }
    else
    {
      if ((double) this.smoothness < 0.10000000149011612)
        this.smoothness = 0.1f;
      this.bokeh = this.bokeh && this.bokehSupport;
      float num1 = this.bokeh ? DepthOfFieldDeprecated.BOKEH_EXTRA_BLUR : 1f;
      bool flag = this.quality > DepthOfFieldDeprecated.Dof34QualitySetting.OnlyBackground;
      float num2 = this.focalSize / (this._camera.farClipPlane - this._camera.nearClipPlane);
      bool blurForeground;
      if (this.simpleTweakMode)
      {
        this.focalDistance01 = Object.op_Implicit((Object) this.objectFocus) ? this._camera.WorldToViewportPoint(this.objectFocus.position).z / this._camera.farClipPlane : this.FocalDistance01(this.focalPoint);
        this.focalStartCurve = this.focalDistance01 * this.smoothness;
        this.focalEndCurve = this.focalStartCurve;
        blurForeground = flag && (double) this.focalPoint > (double) this._camera.nearClipPlane + (double) Mathf.Epsilon;
      }
      else
      {
        if (Object.op_Implicit((Object) this.objectFocus))
        {
          Vector3 viewportPoint = this._camera.WorldToViewportPoint(this.objectFocus.position);
          viewportPoint.z /= this._camera.farClipPlane;
          this.focalDistance01 = viewportPoint.z;
        }
        else
          this.focalDistance01 = this.FocalDistance01(this.focalZDistance);
        this.focalStartCurve = this.focalZStartCurve;
        this.focalEndCurve = this.focalZEndCurve;
        blurForeground = flag && (double) this.focalPoint > (double) this._camera.nearClipPlane + (double) Mathf.Epsilon;
      }
      this.widthOverHeight = (float) (1.0 * (double) ((Texture) source).width / (1.0 * (double) ((Texture) source).height));
      this.oneOverBaseSize = 1f / 512f;
      this.dofMaterial.SetFloat("_ForegroundBlurExtrude", this.foregroundBlurExtrude);
      this.dofMaterial.SetVector("_CurveParams", new Vector4(this.simpleTweakMode ? 1f / this.focalStartCurve : this.focalStartCurve, this.simpleTweakMode ? 1f / this.focalEndCurve : this.focalEndCurve, num2 * 0.5f, this.focalDistance01));
      this.dofMaterial.SetVector("_InvRenderTargetSize", new Vector4((float) (1.0 / (1.0 * (double) ((Texture) source).width)), (float) (1.0 / (1.0 * (double) ((Texture) source).height)), 0.0f, 0.0f));
      int dividerBasedOnQuality1 = this.GetDividerBasedOnQuality();
      int dividerBasedOnQuality2 = this.GetLowResolutionDividerBasedOnQuality(dividerBasedOnQuality1);
      this.AllocateTextures(blurForeground, source, dividerBasedOnQuality1, dividerBasedOnQuality2);
      Graphics.Blit((Texture) source, source, this.dofMaterial, 3);
      this.Downsample(source, this.mediumRezWorkTexture);
      this.Blur(this.mediumRezWorkTexture, this.mediumRezWorkTexture, DepthOfFieldDeprecated.DofBlurriness.Low, 4, this.maxBlurSpread);
      if (this.bokeh && (DepthOfFieldDeprecated.BokehDestination.Foreground & this.bokehDestination) != (DepthOfFieldDeprecated.BokehDestination) 0)
      {
        this.dofMaterial.SetVector("_Threshhold", new Vector4(this.bokehThresholdContrast, this.bokehThresholdLuminance, 0.95f, 0.0f));
        Graphics.Blit((Texture) this.mediumRezWorkTexture, this.bokehSource2, this.dofMaterial, 11);
        Graphics.Blit((Texture) this.mediumRezWorkTexture, this.lowRezWorkTexture);
        this.Blur(this.lowRezWorkTexture, this.lowRezWorkTexture, this.bluriness, 0, this.maxBlurSpread * num1);
      }
      else
      {
        this.Downsample(this.mediumRezWorkTexture, this.lowRezWorkTexture);
        this.Blur(this.lowRezWorkTexture, this.lowRezWorkTexture, this.bluriness, 0, this.maxBlurSpread);
      }
      this.dofBlurMaterial.SetTexture("_TapLow", (Texture) this.lowRezWorkTexture);
      this.dofBlurMaterial.SetTexture("_TapMedium", (Texture) this.mediumRezWorkTexture);
      Graphics.Blit((Texture) null, this.finalDefocus, this.dofBlurMaterial, 3);
      if (this.bokeh && (DepthOfFieldDeprecated.BokehDestination.Foreground & this.bokehDestination) != (DepthOfFieldDeprecated.BokehDestination) 0)
        this.AddBokeh(this.bokehSource2, this.bokehSource, this.finalDefocus);
      this.dofMaterial.SetTexture("_TapLowBackground", (Texture) this.finalDefocus);
      this.dofMaterial.SetTexture("_TapMedium", (Texture) this.mediumRezWorkTexture);
      Graphics.Blit((Texture) source, blurForeground ? this.foregroundTexture : destination, this.dofMaterial, this.visualize ? 2 : 0);
      if (blurForeground)
      {
        Graphics.Blit((Texture) this.foregroundTexture, source, this.dofMaterial, 5);
        this.Downsample(source, this.mediumRezWorkTexture);
        this.BlurFg(this.mediumRezWorkTexture, this.mediumRezWorkTexture, DepthOfFieldDeprecated.DofBlurriness.Low, 2, this.maxBlurSpread);
        if (this.bokeh && (DepthOfFieldDeprecated.BokehDestination.Foreground & this.bokehDestination) != (DepthOfFieldDeprecated.BokehDestination) 0)
        {
          this.dofMaterial.SetVector("_Threshhold", new Vector4(this.bokehThresholdContrast * 0.5f, this.bokehThresholdLuminance, 0.0f, 0.0f));
          Graphics.Blit((Texture) this.mediumRezWorkTexture, this.bokehSource2, this.dofMaterial, 11);
          Graphics.Blit((Texture) this.mediumRezWorkTexture, this.lowRezWorkTexture);
          this.BlurFg(this.lowRezWorkTexture, this.lowRezWorkTexture, this.bluriness, 1, this.maxBlurSpread * num1);
        }
        else
          this.BlurFg(this.mediumRezWorkTexture, this.lowRezWorkTexture, this.bluriness, 1, this.maxBlurSpread);
        Graphics.Blit((Texture) this.lowRezWorkTexture, this.finalDefocus);
        this.dofMaterial.SetTexture("_TapLowForeground", (Texture) this.finalDefocus);
        Graphics.Blit((Texture) source, destination, this.dofMaterial, this.visualize ? 1 : 4);
        if (this.bokeh && (DepthOfFieldDeprecated.BokehDestination.Foreground & this.bokehDestination) != (DepthOfFieldDeprecated.BokehDestination) 0)
          this.AddBokeh(this.bokehSource2, this.bokehSource, destination);
      }
      this.ReleaseTextures();
    }
  }

  private void Blur(
    RenderTexture from,
    RenderTexture to,
    DepthOfFieldDeprecated.DofBlurriness iterations,
    int blurPass,
    float spread)
  {
    RenderTexture temporary = RenderTexture.GetTemporary(((Texture) to).width, ((Texture) to).height);
    if (iterations > DepthOfFieldDeprecated.DofBlurriness.Low)
    {
      this.BlurHex(from, to, blurPass, spread, temporary);
      if (iterations > DepthOfFieldDeprecated.DofBlurriness.High)
      {
        this.dofBlurMaterial.SetVector("offsets", new Vector4(0.0f, spread * this.oneOverBaseSize, 0.0f, 0.0f));
        Graphics.Blit((Texture) to, temporary, this.dofBlurMaterial, blurPass);
        this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, 0.0f, 0.0f, 0.0f));
        Graphics.Blit((Texture) temporary, to, this.dofBlurMaterial, blurPass);
      }
    }
    else
    {
      this.dofBlurMaterial.SetVector("offsets", new Vector4(0.0f, spread * this.oneOverBaseSize, 0.0f, 0.0f));
      Graphics.Blit((Texture) from, temporary, this.dofBlurMaterial, blurPass);
      this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, 0.0f, 0.0f, 0.0f));
      Graphics.Blit((Texture) temporary, to, this.dofBlurMaterial, blurPass);
    }
    RenderTexture.ReleaseTemporary(temporary);
  }

  private void BlurFg(
    RenderTexture from,
    RenderTexture to,
    DepthOfFieldDeprecated.DofBlurriness iterations,
    int blurPass,
    float spread)
  {
    this.dofBlurMaterial.SetTexture("_TapHigh", (Texture) from);
    RenderTexture temporary = RenderTexture.GetTemporary(((Texture) to).width, ((Texture) to).height);
    if (iterations > DepthOfFieldDeprecated.DofBlurriness.Low)
    {
      this.BlurHex(from, to, blurPass, spread, temporary);
      if (iterations > DepthOfFieldDeprecated.DofBlurriness.High)
      {
        this.dofBlurMaterial.SetVector("offsets", new Vector4(0.0f, spread * this.oneOverBaseSize, 0.0f, 0.0f));
        Graphics.Blit((Texture) to, temporary, this.dofBlurMaterial, blurPass);
        this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, 0.0f, 0.0f, 0.0f));
        Graphics.Blit((Texture) temporary, to, this.dofBlurMaterial, blurPass);
      }
    }
    else
    {
      this.dofBlurMaterial.SetVector("offsets", new Vector4(0.0f, spread * this.oneOverBaseSize, 0.0f, 0.0f));
      Graphics.Blit((Texture) from, temporary, this.dofBlurMaterial, blurPass);
      this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, 0.0f, 0.0f, 0.0f));
      Graphics.Blit((Texture) temporary, to, this.dofBlurMaterial, blurPass);
    }
    RenderTexture.ReleaseTemporary(temporary);
  }

  private void BlurHex(
    RenderTexture from,
    RenderTexture to,
    int blurPass,
    float spread,
    RenderTexture tmp)
  {
    this.dofBlurMaterial.SetVector("offsets", new Vector4(0.0f, spread * this.oneOverBaseSize, 0.0f, 0.0f));
    Graphics.Blit((Texture) from, tmp, this.dofBlurMaterial, blurPass);
    this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, 0.0f, 0.0f, 0.0f));
    Graphics.Blit((Texture) tmp, to, this.dofBlurMaterial, blurPass);
    this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, spread * this.oneOverBaseSize, 0.0f, 0.0f));
    Graphics.Blit((Texture) to, tmp, this.dofBlurMaterial, blurPass);
    this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, -spread * this.oneOverBaseSize, 0.0f, 0.0f));
    Graphics.Blit((Texture) tmp, to, this.dofBlurMaterial, blurPass);
  }

  private void Downsample(RenderTexture from, RenderTexture to)
  {
    this.dofMaterial.SetVector("_InvRenderTargetSize", new Vector4((float) (1.0 / (1.0 * (double) ((Texture) to).width)), (float) (1.0 / (1.0 * (double) ((Texture) to).height)), 0.0f, 0.0f));
    Graphics.Blit((Texture) from, to, this.dofMaterial, DepthOfFieldDeprecated.SMOOTH_DOWNSAMPLE_PASS);
  }

  private void AddBokeh(RenderTexture bokehInfo, RenderTexture tempTex, RenderTexture finalTarget)
  {
    if (!Object.op_Implicit((Object) this.bokehMaterial))
      return;
    Mesh[] meshes = Quads.GetMeshes(((Texture) tempTex).width, ((Texture) tempTex).height);
    RenderTexture.active = tempTex;
    GL.Clear(false, true, new Color(0.0f, 0.0f, 0.0f, 0.0f));
    GL.PushMatrix();
    GL.LoadIdentity();
    ((Texture) bokehInfo).filterMode = (FilterMode) 0;
    float num1 = (float) ((double) ((Texture) bokehInfo).width * 1.0 / ((double) ((Texture) bokehInfo).height * 1.0));
    float num2 = (float) (2.0 / (1.0 * (double) ((Texture) bokehInfo).width)) + this.bokehScale * this.maxBlurSpread * DepthOfFieldDeprecated.BOKEH_EXTRA_BLUR * this.oneOverBaseSize;
    this.bokehMaterial.SetTexture("_Source", (Texture) bokehInfo);
    this.bokehMaterial.SetTexture("_MainTex", (Texture) this.bokehTexture);
    this.bokehMaterial.SetVector("_ArScale", new Vector4(num2, num2 * num1, 0.5f, 0.5f * num1));
    this.bokehMaterial.SetFloat("_Intensity", this.bokehIntensity);
    this.bokehMaterial.SetPass(0);
    foreach (Mesh mesh in meshes)
    {
      if (Object.op_Implicit((Object) mesh))
        Graphics.DrawMeshNow(mesh, Matrix4x4.identity);
    }
    GL.PopMatrix();
    Graphics.Blit((Texture) tempTex, finalTarget, this.dofMaterial, 8);
    ((Texture) bokehInfo).filterMode = (FilterMode) 1;
  }

  private void ReleaseTextures()
  {
    if (Object.op_Implicit((Object) this.foregroundTexture))
      RenderTexture.ReleaseTemporary(this.foregroundTexture);
    if (Object.op_Implicit((Object) this.finalDefocus))
      RenderTexture.ReleaseTemporary(this.finalDefocus);
    if (Object.op_Implicit((Object) this.mediumRezWorkTexture))
      RenderTexture.ReleaseTemporary(this.mediumRezWorkTexture);
    if (Object.op_Implicit((Object) this.lowRezWorkTexture))
      RenderTexture.ReleaseTemporary(this.lowRezWorkTexture);
    if (Object.op_Implicit((Object) this.bokehSource))
      RenderTexture.ReleaseTemporary(this.bokehSource);
    if (!Object.op_Implicit((Object) this.bokehSource2))
      return;
    RenderTexture.ReleaseTemporary(this.bokehSource2);
  }

  private void AllocateTextures(
    bool blurForeground,
    RenderTexture source,
    int divider,
    int lowTexDivider)
  {
    this.foregroundTexture = (RenderTexture) null;
    if (blurForeground)
      this.foregroundTexture = RenderTexture.GetTemporary(((Texture) source).width, ((Texture) source).height, 0);
    this.mediumRezWorkTexture = RenderTexture.GetTemporary(((Texture) source).width / divider, ((Texture) source).height / divider, 0);
    this.finalDefocus = RenderTexture.GetTemporary(((Texture) source).width / divider, ((Texture) source).height / divider, 0);
    this.lowRezWorkTexture = RenderTexture.GetTemporary(((Texture) source).width / lowTexDivider, ((Texture) source).height / lowTexDivider, 0);
    this.bokehSource = (RenderTexture) null;
    this.bokehSource2 = (RenderTexture) null;
    if (this.bokeh)
    {
      this.bokehSource = RenderTexture.GetTemporary(((Texture) source).width / (lowTexDivider * this.bokehDownsample), ((Texture) source).height / (lowTexDivider * this.bokehDownsample), 0, (RenderTextureFormat) 2);
      this.bokehSource2 = RenderTexture.GetTemporary(((Texture) source).width / (lowTexDivider * this.bokehDownsample), ((Texture) source).height / (lowTexDivider * this.bokehDownsample), 0, (RenderTextureFormat) 2);
      ((Texture) this.bokehSource).filterMode = (FilterMode) 1;
      ((Texture) this.bokehSource2).filterMode = (FilterMode) 1;
      RenderTexture.active = this.bokehSource2;
      GL.Clear(false, true, new Color(0.0f, 0.0f, 0.0f, 0.0f));
    }
    ((Texture) source).filterMode = (FilterMode) 1;
    ((Texture) this.finalDefocus).filterMode = (FilterMode) 1;
    ((Texture) this.mediumRezWorkTexture).filterMode = (FilterMode) 1;
    ((Texture) this.lowRezWorkTexture).filterMode = (FilterMode) 1;
    if (!Object.op_Implicit((Object) this.foregroundTexture))
      return;
    ((Texture) this.foregroundTexture).filterMode = (FilterMode) 1;
  }

  public enum Dof34QualitySetting
  {
    OnlyBackground = 1,
    BackgroundAndForeground = 2,
  }

  public enum DofResolution
  {
    High = 2,
    Medium = 3,
    Low = 4,
  }

  public enum DofBlurriness
  {
    Low = 1,
    High = 2,
    VeryHigh = 4,
  }

  public enum BokehDestination
  {
    Background = 1,
    Foreground = 2,
    BackgroundAndForeground = 3,
  }
}
