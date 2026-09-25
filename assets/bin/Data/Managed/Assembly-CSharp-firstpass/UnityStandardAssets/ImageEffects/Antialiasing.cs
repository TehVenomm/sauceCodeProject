// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.Antialiasing
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[RequireComponent(typeof (Camera))]
[AddComponentMenu("Image Effects/Other/Antialiasing")]
public class Antialiasing : PostEffectsBase
{
  public AAMode mode = AAMode.FXAA3Console;
  public bool showGeneratedNormals;
  public float offsetScale = 0.2f;
  public float blurRadius = 18f;
  public float edgeThresholdMin = 0.05f;
  public float edgeThreshold = 0.2f;
  public float edgeSharpness = 4f;
  public bool dlaaSharp;
  public Shader ssaaShader;
  private Material ssaa;
  public Shader dlaaShader;
  private Material dlaa;
  public Shader nfaaShader;
  private Material nfaa;
  public Shader shaderFXAAPreset2;
  private Material materialFXAAPreset2;
  public Shader shaderFXAAPreset3;
  private Material materialFXAAPreset3;
  public Shader shaderFXAAII;
  private Material materialFXAAII;
  public Shader shaderFXAAIII;
  private Material materialFXAAIII;

  public Material CurrentAAMaterial()
  {
    Material material;
    switch (this.mode)
    {
      case AAMode.FXAA2:
        material = this.materialFXAAII;
        break;
      case AAMode.FXAA3Console:
        material = this.materialFXAAIII;
        break;
      case AAMode.FXAA1PresetA:
        material = this.materialFXAAPreset2;
        break;
      case AAMode.FXAA1PresetB:
        material = this.materialFXAAPreset3;
        break;
      case AAMode.NFAA:
        material = this.nfaa;
        break;
      case AAMode.SSAA:
        material = this.ssaa;
        break;
      case AAMode.DLAA:
        material = this.dlaa;
        break;
      default:
        material = (Material) null;
        break;
    }
    return material;
  }

  public override bool CheckResources()
  {
    this.CheckSupport(false);
    this.materialFXAAPreset2 = this.CreateMaterial(this.shaderFXAAPreset2, this.materialFXAAPreset2);
    this.materialFXAAPreset3 = this.CreateMaterial(this.shaderFXAAPreset3, this.materialFXAAPreset3);
    this.materialFXAAII = this.CreateMaterial(this.shaderFXAAII, this.materialFXAAII);
    this.materialFXAAIII = this.CreateMaterial(this.shaderFXAAIII, this.materialFXAAIII);
    this.nfaa = this.CreateMaterial(this.nfaaShader, this.nfaa);
    this.ssaa = this.CreateMaterial(this.ssaaShader, this.ssaa);
    this.dlaa = this.CreateMaterial(this.dlaaShader, this.dlaa);
    if (!this.ssaaShader.isSupported)
    {
      this.NotSupported();
      this.ReportAutoDisable();
    }
    return this.isSupported;
  }

  public void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    if (!this.CheckResources())
      Graphics.Blit((Texture) source, destination);
    else if (this.mode == AAMode.FXAA3Console && Object.op_Inequality((Object) this.materialFXAAIII, (Object) null))
    {
      this.materialFXAAIII.SetFloat("_EdgeThresholdMin", this.edgeThresholdMin);
      this.materialFXAAIII.SetFloat("_EdgeThreshold", this.edgeThreshold);
      this.materialFXAAIII.SetFloat("_EdgeSharpness", this.edgeSharpness);
      Graphics.Blit((Texture) source, destination, this.materialFXAAIII);
    }
    else if (this.mode == AAMode.FXAA1PresetB && Object.op_Inequality((Object) this.materialFXAAPreset3, (Object) null))
      Graphics.Blit((Texture) source, destination, this.materialFXAAPreset3);
    else if (this.mode == AAMode.FXAA1PresetA && Object.op_Inequality((Object) this.materialFXAAPreset2, (Object) null))
    {
      ((Texture) source).anisoLevel = 4;
      Graphics.Blit((Texture) source, destination, this.materialFXAAPreset2);
      ((Texture) source).anisoLevel = 0;
    }
    else if (this.mode == AAMode.FXAA2 && Object.op_Inequality((Object) this.materialFXAAII, (Object) null))
      Graphics.Blit((Texture) source, destination, this.materialFXAAII);
    else if (this.mode == AAMode.SSAA && Object.op_Inequality((Object) this.ssaa, (Object) null))
      Graphics.Blit((Texture) source, destination, this.ssaa);
    else if (this.mode == AAMode.DLAA && Object.op_Inequality((Object) this.dlaa, (Object) null))
    {
      ((Texture) source).anisoLevel = 0;
      RenderTexture temporary = RenderTexture.GetTemporary(((Texture) source).width, ((Texture) source).height);
      Graphics.Blit((Texture) source, temporary, this.dlaa, 0);
      Graphics.Blit((Texture) temporary, destination, this.dlaa, this.dlaaSharp ? 2 : 1);
      RenderTexture.ReleaseTemporary(temporary);
    }
    else if (this.mode == AAMode.NFAA && Object.op_Inequality((Object) this.nfaa, (Object) null))
    {
      ((Texture) source).anisoLevel = 0;
      this.nfaa.SetFloat("_OffsetScale", this.offsetScale);
      this.nfaa.SetFloat("_BlurRadius", this.blurRadius);
      Graphics.Blit((Texture) source, destination, this.nfaa, this.showGeneratedNormals ? 1 : 0);
    }
    else
      Graphics.Blit((Texture) source, destination);
  }
}
