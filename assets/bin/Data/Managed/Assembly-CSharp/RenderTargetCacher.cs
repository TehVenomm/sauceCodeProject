// Decompiled with JetBrains decompiler
// Type: RenderTargetCacher
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[RequireComponent(typeof (Camera))]
public class RenderTargetCacher : MonoBehaviour
{
  [SerializeField]
  private RenderTexture renderTexture;
  public bool cacheAfter;
  private Camera cam;
  public Action<RenderTexture> onUpdateTexture;

  public Action<RenderTexture, RenderTexture> postEffectProc { get; set; }

  public RenderTexture GetTexture()
  {
    if (Object.op_Equality((Object) this.renderTexture, (Object) null))
      this.CreateTexture();
    return this.renderTexture;
  }

  private void Start()
  {
    this.cam = ((Component) this).GetComponent<Camera>();
    this.CreateTexture();
  }

  private void CreateTexture()
  {
    if (Object.op_Inequality((Object) this.renderTexture, (Object) null))
      return;
    this.renderTexture = RenderTexture.GetTemporary(Screen.width, Screen.height);
    if (this.onUpdateTexture == null)
      return;
    this.onUpdateTexture(this.renderTexture);
  }

  private void OnDestroy()
  {
    if (!Object.op_Inequality((Object) this.renderTexture, (Object) null))
      return;
    RenderTexture.ReleaseTemporary(this.renderTexture);
    this.renderTexture = (RenderTexture) null;
  }

  private void OnPreRender()
  {
    if (Object.op_Equality((Object) this.cam, (Object) null))
      return;
    if (this.cam.clearFlags == 2)
      GL.Clear(true, true, Color.black);
    if (Object.op_Equality((Object) this.renderTexture, (Object) null))
      return;
    if (((Texture) this.renderTexture).width != Screen.width || ((Texture) this.renderTexture).height != Screen.height)
    {
      this.renderTexture.Release();
      this.renderTexture = (RenderTexture) null;
      this.CreateTexture();
    }
    if (!Object.op_Inequality((Object) this.renderTexture, (Object) null))
      return;
    this.renderTexture.DiscardContents();
  }

  private void OnRenderImage(RenderTexture src, RenderTexture dest)
  {
    if (!this.cacheAfter)
      Graphics.Blit((Texture) src, this.renderTexture);
    if (this.postEffectProc != null)
      this.postEffectProc(src, dest);
    else
      Graphics.Blit((Texture) src, dest);
    if (!this.cacheAfter)
      return;
    Graphics.Blit((Texture) dest, this.renderTexture);
  }
}
