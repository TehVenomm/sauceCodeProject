// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.ScreenSpaceAmbientObscurance
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[RequireComponent(typeof (Camera))]
[AddComponentMenu("Image Effects/Rendering/Screen Space Ambient Obscurance")]
internal class ScreenSpaceAmbientObscurance : PostEffectsBase
{
  [Range(0.0f, 3f)]
  public float intensity = 0.5f;
  [Range(0.1f, 3f)]
  public float radius = 0.2f;
  [Range(0.0f, 3f)]
  public int blurIterations = 1;
  [Range(0.0f, 5f)]
  public float blurFilterDistance = 1.25f;
  [Range(0.0f, 1f)]
  public int downsample;
  public Texture2D rand;
  public Shader aoShader;
  private Material aoMaterial;

  public override bool CheckResources()
  {
    this.CheckSupport(true);
    this.aoMaterial = this.CheckShaderAndCreateMaterial(this.aoShader, this.aoMaterial);
    if (!this.isSupported)
      this.ReportAutoDisable();
    return this.isSupported;
  }

  private void OnDisable()
  {
    if (Object.op_Implicit((Object) this.aoMaterial))
      Object.DestroyImmediate((Object) this.aoMaterial);
    this.aoMaterial = (Material) null;
  }

  [ImageEffectOpaque]
  private void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    if (!this.CheckResources())
    {
      Graphics.Blit((Texture) source, destination);
    }
    else
    {
      Matrix4x4 projectionMatrix = ((Component) this).GetComponent<Camera>().projectionMatrix;
      Matrix4x4 inverse = ((Matrix4x4) ref projectionMatrix).inverse;
      Vector4 vector4;
      // ISSUE: explicit constructor call
      ((Vector4) ref vector4).\u002Ector((float) (-2.0 / ((double) Screen.width * (double) ((Matrix4x4) ref projectionMatrix)[0])), (float) (-2.0 / ((double) Screen.height * (double) ((Matrix4x4) ref projectionMatrix)[5])), (1f - ((Matrix4x4) ref projectionMatrix)[2]) / ((Matrix4x4) ref projectionMatrix)[0], (1f + ((Matrix4x4) ref projectionMatrix)[6]) / ((Matrix4x4) ref projectionMatrix)[5]);
      this.aoMaterial.SetVector("_ProjInfo", vector4);
      this.aoMaterial.SetMatrix("_ProjectionInv", inverse);
      this.aoMaterial.SetTexture("_Rand", (Texture) this.rand);
      this.aoMaterial.SetFloat("_Radius", this.radius);
      this.aoMaterial.SetFloat("_Radius2", this.radius * this.radius);
      this.aoMaterial.SetFloat("_Intensity", this.intensity);
      this.aoMaterial.SetFloat("_BlurFilterDistance", this.blurFilterDistance);
      int width = ((Texture) source).width;
      int height = ((Texture) source).height;
      RenderTexture renderTexture = RenderTexture.GetTemporary(width >> this.downsample, height >> this.downsample);
      Graphics.Blit((Texture) source, renderTexture, this.aoMaterial, 0);
      if (this.downsample > 0)
      {
        RenderTexture temporary = RenderTexture.GetTemporary(width, height);
        Graphics.Blit((Texture) renderTexture, temporary, this.aoMaterial, 4);
        RenderTexture.ReleaseTemporary(renderTexture);
        renderTexture = temporary;
      }
      for (int index = 0; index < this.blurIterations; ++index)
      {
        this.aoMaterial.SetVector("_Axis", Vector4.op_Implicit(new Vector2(1f, 0.0f)));
        RenderTexture temporary = RenderTexture.GetTemporary(width, height);
        Graphics.Blit((Texture) renderTexture, temporary, this.aoMaterial, 1);
        RenderTexture.ReleaseTemporary(renderTexture);
        this.aoMaterial.SetVector("_Axis", Vector4.op_Implicit(new Vector2(0.0f, 1f)));
        renderTexture = RenderTexture.GetTemporary(width, height);
        Graphics.Blit((Texture) temporary, renderTexture, this.aoMaterial, 1);
        RenderTexture.ReleaseTemporary(temporary);
      }
      this.aoMaterial.SetTexture("_AOTex", (Texture) renderTexture);
      Graphics.Blit((Texture) source, destination, this.aoMaterial, 2);
      RenderTexture.ReleaseTemporary(renderTexture);
    }
  }
}
