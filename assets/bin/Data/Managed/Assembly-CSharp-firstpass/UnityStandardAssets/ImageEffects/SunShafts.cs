// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.SunShafts
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[RequireComponent(typeof (Camera))]
[AddComponentMenu("Image Effects/Rendering/Sun Shafts")]
public class SunShafts : PostEffectsBase
{
  public SunShafts.SunShaftsResolution resolution = SunShafts.SunShaftsResolution.Normal;
  public SunShafts.ShaftsScreenBlendMode screenBlendMode;
  public Transform sunTransform;
  public int radialBlurIterations = 2;
  public Color sunColor = Color.white;
  public Color sunThreshold = new Color(0.87f, 0.74f, 0.65f);
  public float sunShaftBlurRadius = 2.5f;
  public float sunShaftIntensity = 1.15f;
  public float maxRadius = 0.75f;
  public bool useDepthTexture = true;
  public Shader sunShaftsShader;
  private Material sunShaftsMaterial;
  public Shader simpleClearShader;
  private Material simpleClearMaterial;

  public override bool CheckResources()
  {
    this.CheckSupport(this.useDepthTexture);
    this.sunShaftsMaterial = this.CheckShaderAndCreateMaterial(this.sunShaftsShader, this.sunShaftsMaterial);
    this.simpleClearMaterial = this.CheckShaderAndCreateMaterial(this.simpleClearShader, this.simpleClearMaterial);
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
      if (this.useDepthTexture)
      {
        Camera component = ((Component) this).GetComponent<Camera>();
        component.depthTextureMode = (DepthTextureMode) (component.depthTextureMode | 1);
      }
      int num1 = 4;
      if (this.resolution == SunShafts.SunShaftsResolution.Normal)
        num1 = 2;
      else if (this.resolution == SunShafts.SunShaftsResolution.High)
        num1 = 1;
      Vector3 vector3 = Vector3.op_Multiply(Vector3.one, 0.5f);
      if (Object.op_Implicit((Object) this.sunTransform))
      {
        vector3 = ((Component) this).GetComponent<Camera>().WorldToViewportPoint(this.sunTransform.position);
      }
      else
      {
        // ISSUE: explicit constructor call
        ((Vector3) ref vector3).\u002Ector(0.5f, 0.5f, 0.0f);
      }
      int num2 = ((Texture) source).width / num1;
      int num3 = ((Texture) source).height / num1;
      RenderTexture temporary1 = RenderTexture.GetTemporary(num2, num3, 0);
      this.sunShaftsMaterial.SetVector("_BlurRadius4", Vector4.op_Multiply(new Vector4(1f, 1f, 0.0f, 0.0f), this.sunShaftBlurRadius));
      this.sunShaftsMaterial.SetVector("_SunPosition", new Vector4(vector3.x, vector3.y, vector3.z, this.maxRadius));
      this.sunShaftsMaterial.SetVector("_SunThreshold", Color.op_Implicit(this.sunThreshold));
      if (!this.useDepthTexture)
      {
        RenderTextureFormat renderTextureFormat = ((Component) this).GetComponent<Camera>().allowHDR ? (RenderTextureFormat) 9 : (RenderTextureFormat) 7;
        RenderTexture temporary2 = RenderTexture.GetTemporary(((Texture) source).width, ((Texture) source).height, 0, renderTextureFormat);
        RenderTexture.active = temporary2;
        GL.ClearWithSkybox(false, ((Component) this).GetComponent<Camera>());
        this.sunShaftsMaterial.SetTexture("_Skybox", (Texture) temporary2);
        Graphics.Blit((Texture) source, temporary1, this.sunShaftsMaterial, 3);
        RenderTexture.ReleaseTemporary(temporary2);
      }
      else
        Graphics.Blit((Texture) source, temporary1, this.sunShaftsMaterial, 2);
      this.DrawBorder(temporary1, this.simpleClearMaterial);
      this.radialBlurIterations = Mathf.Clamp(this.radialBlurIterations, 1, 4);
      float num4 = this.sunShaftBlurRadius * (1f / 768f);
      this.sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(num4, num4, 0.0f, 0.0f));
      this.sunShaftsMaterial.SetVector("_SunPosition", new Vector4(vector3.x, vector3.y, vector3.z, this.maxRadius));
      for (int index = 0; index < this.radialBlurIterations; ++index)
      {
        RenderTexture temporary3 = RenderTexture.GetTemporary(num2, num3, 0);
        Graphics.Blit((Texture) temporary1, temporary3, this.sunShaftsMaterial, 1);
        RenderTexture.ReleaseTemporary(temporary1);
        float num5 = (float) ((double) this.sunShaftBlurRadius * (((double) index * 2.0 + 1.0) * 6.0) / 768.0);
        this.sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(num5, num5, 0.0f, 0.0f));
        temporary1 = RenderTexture.GetTemporary(num2, num3, 0);
        Graphics.Blit((Texture) temporary3, temporary1, this.sunShaftsMaterial, 1);
        RenderTexture.ReleaseTemporary(temporary3);
        float num6 = (float) ((double) this.sunShaftBlurRadius * (((double) index * 2.0 + 2.0) * 6.0) / 768.0);
        this.sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(num6, num6, 0.0f, 0.0f));
      }
      if ((double) vector3.z >= 0.0)
        this.sunShaftsMaterial.SetVector("_SunColor", Vector4.op_Multiply(new Vector4(this.sunColor.r, this.sunColor.g, this.sunColor.b, this.sunColor.a), this.sunShaftIntensity));
      else
        this.sunShaftsMaterial.SetVector("_SunColor", Vector4.zero);
      this.sunShaftsMaterial.SetTexture("_ColorBuffer", (Texture) temporary1);
      Graphics.Blit((Texture) source, destination, this.sunShaftsMaterial, this.screenBlendMode == SunShafts.ShaftsScreenBlendMode.Screen ? 0 : 4);
      RenderTexture.ReleaseTemporary(temporary1);
    }
  }

  public enum SunShaftsResolution
  {
    Low,
    Normal,
    High,
  }

  public enum ShaftsScreenBlendMode
  {
    Screen,
    Add,
  }
}
