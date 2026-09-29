// Decompiled with JetBrains decompiler
// Type: FortuneWheelMotionBlur
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityStandardAssets.ImageEffects;

#nullable disable
[RequireComponent(typeof (Camera))]
public class FortuneWheelMotionBlur : ImageEffectBase
{
  [Range(0.0f, 0.92f)]
  public float blurAmount = 0.8f;
  public GameObject result;
  private Camera _camera;
  private UITexture _resultTexture;
  private UIRoot _root;
  private RenderTexture _srcRT;
  private RenderTexture _dstRT;

  protected override void Start()
  {
    this._camera = ((Component) this).gameObject.GetComponent<Camera>();
    this._resultTexture = this.result.GetComponent<UITexture>();
    this._root = GameObject.Find("UI_Root").gameObject.GetComponent<UIRoot>();
    base.Start();
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    Object.DestroyImmediate((Object) this._dstRT);
    this._dstRT = (RenderTexture) null;
    Object.DestroyImmediate((Object) this._srcRT);
    this._srcRT = (RenderTexture) null;
  }

  private void OnDestroy()
  {
    Object.DestroyImmediate((Object) this._dstRT);
    this._dstRT = (RenderTexture) null;
    Object.DestroyImmediate((Object) this._srcRT);
    this._srcRT = (RenderTexture) null;
  }

  private void OnPreCull()
  {
    RenderTexture.active = this._camera.targetTexture;
    GL.Begin(4);
    GL.Clear(true, true, Color.clear);
    GL.End();
    RenderTexture.active = (RenderTexture) null;
    this.SetLayerRecursive(((Component) ((Component) this).gameObject.transform.parent).gameObject, 4);
    this._resultTexture.width = this._root.manualWidth;
    this._resultTexture.height = this._root.manualHeight;
    if (Object.op_Equality((Object) this._srcRT, (Object) null) || ((Texture) this._srcRT).width != this._camera.pixelWidth || ((Texture) this._srcRT).height != this._camera.pixelHeight)
    {
      this._srcRT = new RenderTexture(this._camera.pixelWidth, this._camera.pixelHeight, 24);
      ((Object) this._srcRT).hideFlags = (HideFlags) 61;
      this._srcRT.Create();
    }
    this._camera.targetTexture = this._srcRT;
    if (Object.op_Equality((Object) this._dstRT, (Object) null) || ((Texture) this._dstRT).width != ((Texture) this._srcRT).width || ((Texture) this._dstRT).height != ((Texture) this._srcRT).height)
    {
      this._dstRT = new RenderTexture(this._camera.pixelWidth, this._camera.pixelHeight, 24);
      ((Object) this._dstRT).hideFlags = (HideFlags) 61;
      this._dstRT.Create();
      Graphics.Blit((Texture) this._srcRT, this._dstRT);
    }
    this._resultTexture.mainTexture = (Texture) this._dstRT;
  }

  private void OnPostRender()
  {
    this.blurAmount = Mathf.Clamp(this.blurAmount, 0.0f, 0.92f);
    this.material.SetTexture("_MainTex", (Texture) this._dstRT);
    this.material.SetFloat("_AccumOrig", 1f - this.blurAmount);
    this._dstRT.MarkRestoreExpected();
    Graphics.Blit((Texture) this._srcRT, this._dstRT, this.material);
  }

  private void SetLayerRecursive(GameObject obj, int layer)
  {
    if (Object.op_Equality((Object) obj, (Object) null))
      return;
    obj.layer = layer;
    foreach (Component component in obj.transform)
      this.SetLayerRecursive(component.gameObject, layer);
  }
}
