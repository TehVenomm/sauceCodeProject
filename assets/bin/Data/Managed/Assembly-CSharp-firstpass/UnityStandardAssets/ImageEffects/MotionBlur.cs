// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.ImageEffects.MotionBlur
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using UnityEngine;

#nullable disable
namespace UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Blur/Motion Blur (Color Accumulation)")]
[RequireComponent(typeof (Camera))]
public class MotionBlur : ImageEffectBase
{
  [Range(0.0f, 0.92f)]
  public float blurAmount = 0.8f;
  public bool extraBlur;
  private RenderTexture accumTexture;

  protected override void Start()
  {
    if (!SystemInfo.supportsRenderTextures)
      ((Behaviour) this).enabled = false;
    else
      base.Start();
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    Object.DestroyImmediate((Object) this.accumTexture);
  }

  private void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    if (Object.op_Equality((Object) this.accumTexture, (Object) null) || ((Texture) this.accumTexture).width != ((Texture) source).width || ((Texture) this.accumTexture).height != ((Texture) source).height)
    {
      Object.DestroyImmediate((Object) this.accumTexture);
      this.accumTexture = new RenderTexture(((Texture) source).width, ((Texture) source).height, 0);
      ((Object) this.accumTexture).hideFlags = (HideFlags) 61;
      Graphics.Blit((Texture) source, this.accumTexture);
    }
    if (this.extraBlur)
    {
      RenderTexture temporary = RenderTexture.GetTemporary(((Texture) source).width / 4, ((Texture) source).height / 4, 0);
      this.accumTexture.MarkRestoreExpected();
      Graphics.Blit((Texture) this.accumTexture, temporary);
      Graphics.Blit((Texture) temporary, this.accumTexture);
      RenderTexture.ReleaseTemporary(temporary);
    }
    this.blurAmount = Mathf.Clamp(this.blurAmount, 0.0f, 0.92f);
    this.material.SetTexture("_MainTex", (Texture) this.accumTexture);
    this.material.SetFloat("_AccumOrig", 1f - this.blurAmount);
    this.accumTexture.MarkRestoreExpected();
    Graphics.Blit((Texture) source, this.accumTexture, this.material);
    Graphics.Blit((Texture) this.accumTexture, destination);
  }
}
