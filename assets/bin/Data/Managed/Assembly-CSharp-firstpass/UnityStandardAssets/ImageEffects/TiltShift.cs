// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.TiltShift
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[RequireComponent(typeof (Camera))]
[AddComponentMenu("Image Effects/Camera/Tilt Shift (Lens Blur)")]
internal class TiltShift : PostEffectsBase
{
  public TiltShift.TiltShiftMode mode;
  public TiltShift.TiltShiftQuality quality = TiltShift.TiltShiftQuality.Normal;
  [Range(0.0f, 15f)]
  public float blurArea = 1f;
  [Range(0.0f, 25f)]
  public float maxBlurSize = 5f;
  [Range(0.0f, 1f)]
  public int downsample;
  public Shader tiltShiftShader;
  private Material tiltShiftMaterial;

  public override bool CheckResources()
  {
    this.CheckSupport(true);
    this.tiltShiftMaterial = this.CheckShaderAndCreateMaterial(this.tiltShiftShader, this.tiltShiftMaterial);
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
      this.tiltShiftMaterial.SetFloat("_BlurSize", (double) this.maxBlurSize < 0.0 ? 0.0f : this.maxBlurSize);
      this.tiltShiftMaterial.SetFloat("_BlurArea", this.blurArea);
      ((Texture) source).filterMode = (FilterMode) 1;
      RenderTexture renderTexture = destination;
      if ((double) this.downsample > 0.0)
      {
        renderTexture = RenderTexture.GetTemporary(((Texture) source).width >> this.downsample, ((Texture) source).height >> this.downsample, 0, source.format);
        ((Texture) renderTexture).filterMode = (FilterMode) 1;
      }
      int num = (int) this.quality * 2;
      Graphics.Blit((Texture) source, renderTexture, this.tiltShiftMaterial, this.mode == TiltShift.TiltShiftMode.TiltShiftMode ? num : num + 1);
      if (this.downsample > 0)
      {
        this.tiltShiftMaterial.SetTexture("_Blurred", (Texture) renderTexture);
        Graphics.Blit((Texture) source, destination, this.tiltShiftMaterial, 6);
      }
      if (!Object.op_Inequality((Object) renderTexture, (Object) destination))
        return;
      RenderTexture.ReleaseTemporary(renderTexture);
    }
  }

  public enum TiltShiftMode
  {
    TiltShiftMode,
    IrisMode,
  }

  public enum TiltShiftQuality
  {
    Preview,
    Normal,
    High,
  }
}
