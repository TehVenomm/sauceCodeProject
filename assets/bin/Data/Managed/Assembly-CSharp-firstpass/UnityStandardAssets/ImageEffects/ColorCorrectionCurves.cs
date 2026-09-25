// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.ColorCorrectionCurves
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Adjustments/Color Correction (Curves, Saturation)")]
public class ColorCorrectionCurves : PostEffectsBase
{
  public AnimationCurve redChannel = new AnimationCurve(new Keyframe[2]
  {
    new Keyframe(0.0f, 0.0f),
    new Keyframe(1f, 1f)
  });
  public AnimationCurve greenChannel = new AnimationCurve(new Keyframe[2]
  {
    new Keyframe(0.0f, 0.0f),
    new Keyframe(1f, 1f)
  });
  public AnimationCurve blueChannel = new AnimationCurve(new Keyframe[2]
  {
    new Keyframe(0.0f, 0.0f),
    new Keyframe(1f, 1f)
  });
  public bool useDepthCorrection;
  public AnimationCurve zCurve = new AnimationCurve(new Keyframe[2]
  {
    new Keyframe(0.0f, 0.0f),
    new Keyframe(1f, 1f)
  });
  public AnimationCurve depthRedChannel = new AnimationCurve(new Keyframe[2]
  {
    new Keyframe(0.0f, 0.0f),
    new Keyframe(1f, 1f)
  });
  public AnimationCurve depthGreenChannel = new AnimationCurve(new Keyframe[2]
  {
    new Keyframe(0.0f, 0.0f),
    new Keyframe(1f, 1f)
  });
  public AnimationCurve depthBlueChannel = new AnimationCurve(new Keyframe[2]
  {
    new Keyframe(0.0f, 0.0f),
    new Keyframe(1f, 1f)
  });
  private Material ccMaterial;
  private Material ccDepthMaterial;
  private Material selectiveCcMaterial;
  private Texture2D rgbChannelTex;
  private Texture2D rgbDepthChannelTex;
  private Texture2D zCurveTex;
  public float saturation = 1f;
  public bool selectiveCc;
  public Color selectiveFromColor = Color.white;
  public Color selectiveToColor = Color.white;
  public ColorCorrectionCurves.ColorCorrectionMode mode;
  public bool updateTextures = true;
  public Shader colorCorrectionCurvesShader;
  public Shader simpleColorCorrectionCurvesShader;
  public Shader colorCorrectionSelectiveShader;
  private bool updateTexturesOnStartup = true;

  private new void Start()
  {
    base.Start();
    this.updateTexturesOnStartup = true;
  }

  private void Awake()
  {
  }

  public override bool CheckResources()
  {
    this.CheckSupport(this.mode == ColorCorrectionCurves.ColorCorrectionMode.Advanced);
    this.ccMaterial = this.CheckShaderAndCreateMaterial(this.simpleColorCorrectionCurvesShader, this.ccMaterial);
    this.ccDepthMaterial = this.CheckShaderAndCreateMaterial(this.colorCorrectionCurvesShader, this.ccDepthMaterial);
    this.selectiveCcMaterial = this.CheckShaderAndCreateMaterial(this.colorCorrectionSelectiveShader, this.selectiveCcMaterial);
    if (!Object.op_Implicit((Object) this.rgbChannelTex))
      this.rgbChannelTex = new Texture2D(256 /*0x0100*/, 4, (TextureFormat) 5, false, true);
    if (!Object.op_Implicit((Object) this.rgbDepthChannelTex))
      this.rgbDepthChannelTex = new Texture2D(256 /*0x0100*/, 4, (TextureFormat) 5, false, true);
    if (!Object.op_Implicit((Object) this.zCurveTex))
      this.zCurveTex = new Texture2D(256 /*0x0100*/, 1, (TextureFormat) 5, false, true);
    ((Object) this.rgbChannelTex).hideFlags = (HideFlags) 52;
    ((Object) this.rgbDepthChannelTex).hideFlags = (HideFlags) 52;
    ((Object) this.zCurveTex).hideFlags = (HideFlags) 52;
    ((Texture) this.rgbChannelTex).wrapMode = (TextureWrapMode) 1;
    ((Texture) this.rgbDepthChannelTex).wrapMode = (TextureWrapMode) 1;
    ((Texture) this.zCurveTex).wrapMode = (TextureWrapMode) 1;
    if (!this.isSupported)
      this.ReportAutoDisable();
    return this.isSupported;
  }

  public void UpdateParameters()
  {
    this.CheckResources();
    if (this.redChannel == null || this.greenChannel == null || this.blueChannel == null)
      return;
    for (float num1 = 0.0f; (double) num1 <= 1.0; num1 += 0.003921569f)
    {
      float num2 = Mathf.Clamp(this.redChannel.Evaluate(num1), 0.0f, 1f);
      float num3 = Mathf.Clamp(this.greenChannel.Evaluate(num1), 0.0f, 1f);
      float num4 = Mathf.Clamp(this.blueChannel.Evaluate(num1), 0.0f, 1f);
      this.rgbChannelTex.SetPixel((int) Mathf.Floor(num1 * (float) byte.MaxValue), 0, new Color(num2, num2, num2));
      this.rgbChannelTex.SetPixel((int) Mathf.Floor(num1 * (float) byte.MaxValue), 1, new Color(num3, num3, num3));
      this.rgbChannelTex.SetPixel((int) Mathf.Floor(num1 * (float) byte.MaxValue), 2, new Color(num4, num4, num4));
      float num5 = Mathf.Clamp(this.zCurve.Evaluate(num1), 0.0f, 1f);
      this.zCurveTex.SetPixel((int) Mathf.Floor(num1 * (float) byte.MaxValue), 0, new Color(num5, num5, num5));
      float num6 = Mathf.Clamp(this.depthRedChannel.Evaluate(num1), 0.0f, 1f);
      float num7 = Mathf.Clamp(this.depthGreenChannel.Evaluate(num1), 0.0f, 1f);
      float num8 = Mathf.Clamp(this.depthBlueChannel.Evaluate(num1), 0.0f, 1f);
      this.rgbDepthChannelTex.SetPixel((int) Mathf.Floor(num1 * (float) byte.MaxValue), 0, new Color(num6, num6, num6));
      this.rgbDepthChannelTex.SetPixel((int) Mathf.Floor(num1 * (float) byte.MaxValue), 1, new Color(num7, num7, num7));
      this.rgbDepthChannelTex.SetPixel((int) Mathf.Floor(num1 * (float) byte.MaxValue), 2, new Color(num8, num8, num8));
    }
    this.rgbChannelTex.Apply();
    this.rgbDepthChannelTex.Apply();
    this.zCurveTex.Apply();
  }

  private void UpdateTextures() => this.UpdateParameters();

  private void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    if (!this.CheckResources())
    {
      Graphics.Blit((Texture) source, destination);
    }
    else
    {
      if (this.updateTexturesOnStartup)
      {
        this.UpdateParameters();
        this.updateTexturesOnStartup = false;
      }
      if (this.useDepthCorrection)
      {
        Camera component = ((Component) this).GetComponent<Camera>();
        component.depthTextureMode = (DepthTextureMode) (component.depthTextureMode | 1);
      }
      RenderTexture renderTexture = destination;
      if (this.selectiveCc)
        renderTexture = RenderTexture.GetTemporary(((Texture) source).width, ((Texture) source).height);
      if (this.useDepthCorrection)
      {
        this.ccDepthMaterial.SetTexture("_RgbTex", (Texture) this.rgbChannelTex);
        this.ccDepthMaterial.SetTexture("_ZCurve", (Texture) this.zCurveTex);
        this.ccDepthMaterial.SetTexture("_RgbDepthTex", (Texture) this.rgbDepthChannelTex);
        this.ccDepthMaterial.SetFloat("_Saturation", this.saturation);
        Graphics.Blit((Texture) source, renderTexture, this.ccDepthMaterial);
      }
      else
      {
        this.ccMaterial.SetTexture("_RgbTex", (Texture) this.rgbChannelTex);
        this.ccMaterial.SetFloat("_Saturation", this.saturation);
        Graphics.Blit((Texture) source, renderTexture, this.ccMaterial);
      }
      if (!this.selectiveCc)
        return;
      this.selectiveCcMaterial.SetColor("selColor", this.selectiveFromColor);
      this.selectiveCcMaterial.SetColor("targetColor", this.selectiveToColor);
      Graphics.Blit((Texture) renderTexture, destination, this.selectiveCcMaterial);
      RenderTexture.ReleaseTemporary(renderTexture);
    }
  }

  public enum ColorCorrectionMode
  {
    Simple,
    Advanced,
  }
}
