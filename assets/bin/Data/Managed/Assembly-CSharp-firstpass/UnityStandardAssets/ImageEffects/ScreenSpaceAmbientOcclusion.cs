// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.ScreenSpaceAmbientOcclusion
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using System;
using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[RequireComponent(typeof (Camera))]
[AddComponentMenu("Image Effects/Rendering/Screen Space Ambient Occlusion")]
public class ScreenSpaceAmbientOcclusion : MonoBehaviour
{
  [Range(0.05f, 1f)]
  public float m_Radius = 0.4f;
  public ScreenSpaceAmbientOcclusion.SSAOSamples m_SampleCount = ScreenSpaceAmbientOcclusion.SSAOSamples.Medium;
  [Range(0.5f, 4f)]
  public float m_OcclusionIntensity = 1.5f;
  [Range(0.0f, 4f)]
  public int m_Blur = 2;
  [Range(1f, 6f)]
  public int m_Downsampling = 2;
  [Range(0.2f, 2f)]
  public float m_OcclusionAttenuation = 1f;
  [Range(1E-05f, 0.5f)]
  public float m_MinZ = 0.01f;
  public Shader m_SSAOShader;
  private Material m_SSAOMaterial;
  public Texture2D m_RandomTexture;
  private bool m_Supported;

  private static Material CreateMaterial(Shader shader)
  {
    if (!Object.op_Implicit((Object) shader))
      return (Material) null;
    Material material = new Material(shader);
    ((Object) material).hideFlags = (HideFlags) 61;
    return material;
  }

  private static void DestroyMaterial(Material mat)
  {
    if (!Object.op_Implicit((Object) mat))
      return;
    Object.DestroyImmediate((Object) mat);
    mat = (Material) null;
  }

  private void OnDisable() => ScreenSpaceAmbientOcclusion.DestroyMaterial(this.m_SSAOMaterial);

  private void Start()
  {
    if (!SystemInfo.supportsImageEffects || !SystemInfo.SupportsRenderTextureFormat((RenderTextureFormat) 1))
    {
      this.m_Supported = false;
      ((Behaviour) this).enabled = false;
    }
    else
    {
      this.CreateMaterials();
      if (!Object.op_Implicit((Object) this.m_SSAOMaterial) || this.m_SSAOMaterial.passCount != 5)
      {
        this.m_Supported = false;
        ((Behaviour) this).enabled = false;
      }
      else
        this.m_Supported = true;
    }
  }

  private void OnEnable()
  {
    Camera component = ((Component) this).GetComponent<Camera>();
    component.depthTextureMode = (DepthTextureMode) (component.depthTextureMode | 2);
  }

  private void CreateMaterials()
  {
    if (Object.op_Implicit((Object) this.m_SSAOMaterial) || !this.m_SSAOShader.isSupported)
      return;
    this.m_SSAOMaterial = ScreenSpaceAmbientOcclusion.CreateMaterial(this.m_SSAOShader);
    this.m_SSAOMaterial.SetTexture("_RandomTexture", (Texture) this.m_RandomTexture);
  }

  [ImageEffectOpaque]
  private void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    if (!this.m_Supported || !this.m_SSAOShader.isSupported)
    {
      ((Behaviour) this).enabled = false;
    }
    else
    {
      this.CreateMaterials();
      this.m_Downsampling = Mathf.Clamp(this.m_Downsampling, 1, 6);
      this.m_Radius = Mathf.Clamp(this.m_Radius, 0.05f, 1f);
      this.m_MinZ = Mathf.Clamp(this.m_MinZ, 1E-05f, 0.5f);
      this.m_OcclusionIntensity = Mathf.Clamp(this.m_OcclusionIntensity, 0.5f, 4f);
      this.m_OcclusionAttenuation = Mathf.Clamp(this.m_OcclusionAttenuation, 0.2f, 2f);
      this.m_Blur = Mathf.Clamp(this.m_Blur, 0, 4);
      RenderTexture renderTexture = RenderTexture.GetTemporary(((Texture) source).width / this.m_Downsampling, ((Texture) source).height / this.m_Downsampling, 0);
      double fieldOfView = (double) ((Component) this).GetComponent<Camera>().fieldOfView;
      float farClipPlane = ((Component) this).GetComponent<Camera>().farClipPlane;
      float num1 = Mathf.Tan((float) (fieldOfView * (Math.PI / 180.0) * 0.5)) * farClipPlane;
      this.m_SSAOMaterial.SetVector("_FarCorner", Vector4.op_Implicit(new Vector3(num1 * ((Component) this).GetComponent<Camera>().aspect, num1, farClipPlane)));
      int num2;
      int num3;
      if (Object.op_Implicit((Object) this.m_RandomTexture))
      {
        num2 = ((Texture) this.m_RandomTexture).width;
        num3 = ((Texture) this.m_RandomTexture).height;
      }
      else
      {
        num2 = 1;
        num3 = 1;
      }
      this.m_SSAOMaterial.SetVector("_NoiseScale", Vector4.op_Implicit(new Vector3((float) ((Texture) renderTexture).width / (float) num2, (float) ((Texture) renderTexture).height / (float) num3, 0.0f)));
      this.m_SSAOMaterial.SetVector("_Params", new Vector4(this.m_Radius, this.m_MinZ, 1f / this.m_OcclusionAttenuation, this.m_OcclusionIntensity));
      int num4 = this.m_Blur > 0 ? 1 : 0;
      Graphics.Blit(num4 != 0 ? (Texture) null : (Texture) source, renderTexture, this.m_SSAOMaterial, (int) this.m_SampleCount);
      if (num4 != 0)
      {
        RenderTexture temporary1 = RenderTexture.GetTemporary(((Texture) source).width, ((Texture) source).height, 0);
        this.m_SSAOMaterial.SetVector("_TexelOffsetScale", new Vector4((float) this.m_Blur / (float) ((Texture) source).width, 0.0f, 0.0f, 0.0f));
        this.m_SSAOMaterial.SetTexture("_SSAO", (Texture) renderTexture);
        Graphics.Blit((Texture) null, temporary1, this.m_SSAOMaterial, 3);
        RenderTexture.ReleaseTemporary(renderTexture);
        RenderTexture temporary2 = RenderTexture.GetTemporary(((Texture) source).width, ((Texture) source).height, 0);
        this.m_SSAOMaterial.SetVector("_TexelOffsetScale", new Vector4(0.0f, (float) this.m_Blur / (float) ((Texture) source).height, 0.0f, 0.0f));
        this.m_SSAOMaterial.SetTexture("_SSAO", (Texture) temporary1);
        Graphics.Blit((Texture) source, temporary2, this.m_SSAOMaterial, 3);
        RenderTexture.ReleaseTemporary(temporary1);
        renderTexture = temporary2;
      }
      this.m_SSAOMaterial.SetTexture("_SSAO", (Texture) renderTexture);
      Graphics.Blit((Texture) source, destination, this.m_SSAOMaterial, 4);
      RenderTexture.ReleaseTemporary(renderTexture);
    }
  }

  public enum SSAOSamples
  {
    Low,
    Medium,
    High,
  }
}
