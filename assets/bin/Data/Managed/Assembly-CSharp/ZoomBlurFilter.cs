// Decompiled with JetBrains decompiler
// Type: ZoomBlurFilter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class ZoomBlurFilter : MonoBehaviour
{
  [SerializeField]
  private Material blurMaterial;
  [SerializeField]
  private Vector2 center;
  [SerializeField]
  private float blurPower;
  [SerializeField]
  private RenderTexture _cachedTexture;
  [SerializeField]
  private RenderTexture _filteredTexture;
  private bool chacheTarget;
  private System.Action onCompleteChecheTarget;
  private RenderTargetCacher cacher;
  private bool requestBlitFilterTexture;

  public RenderTexture filteredTexture
  {
    get => this._filteredTexture;
    private set => this.filteredTexture = value;
  }

  public void SetBlurPram(float _power, Vector2 _center)
  {
    this.blurPower = _power;
    this.center = _center;
  }

  public void CacheRenderTarget(System.Action onComplete, bool reqWithFilter = false)
  {
    this.chacheTarget = true;
    this.onCompleteChecheTarget = onComplete;
    GameObject gameObject = ((Component) MonoBehaviourSingleton<UIManager>.I.uiCamera).gameObject;
    this.cacher = gameObject.GetComponent<RenderTargetCacher>();
    if (Object.op_Equality((Object) null, (Object) this.cacher))
      this.cacher = gameObject.AddComponent<RenderTargetCacher>();
    this.requestBlitFilterTexture = reqWithFilter;
  }

  private void Awake()
  {
    this.blurMaterial = new Material(ResourceUtility.FindShader("mobile/Custom/ImageEffect/RadialBlurFilter"));
    this.Restore();
  }

  private void OnDestroy()
  {
    if (Object.op_Inequality((Object) this._filteredTexture, (Object) null))
    {
      RenderTexture.ReleaseTemporary(this._filteredTexture);
      this._filteredTexture = (RenderTexture) null;
    }
    if (Object.op_Inequality((Object) this.blurMaterial, (Object) null))
    {
      Object.Destroy((Object) this.blurMaterial);
      this.blurMaterial = (Material) null;
    }
    if (!Object.op_Inequality((Object) null, (Object) this.cacher))
      return;
    Object.Destroy((Object) this.cacher);
    this.cacher = (RenderTargetCacher) null;
  }

  private void OnRenderImage(RenderTexture src, RenderTexture dst)
  {
    if (this.chacheTarget && Object.op_Inequality((Object) null, (Object) this.cacher))
    {
      Graphics.Blit((Texture) this.cacher.GetTexture(), this._cachedTexture);
      Graphics.Blit((Texture) src, dst);
      this.chacheTarget = false;
      Object.Destroy((Object) this.cacher);
      this.cacher = (RenderTargetCacher) null;
      if (this.requestBlitFilterTexture)
      {
        this.requestBlitFilterTexture = false;
        Graphics.Blit((Texture) this._cachedTexture, this.filteredTexture);
      }
      if (this.onCompleteChecheTarget == null)
        return;
      this.onCompleteChecheTarget();
      this.onCompleteChecheTarget = (System.Action) null;
    }
    else if (Object.op_Equality((Object) this.blurMaterial, (Object) null) || (double) this.blurPower <= 0.0099999997764825821)
    {
      Graphics.Blit((Texture) src, dst);
    }
    else
    {
      this.blurMaterial.SetVector("_Origin", new Vector4(this.center.x, this.center.y, 0.0f, 0.0f));
      this.blurMaterial.SetFloat("_Power", this.blurPower);
      this._filteredTexture.DiscardContents(true, true);
      if (Object.op_Inequality((Object) this._cachedTexture, (Object) null))
        Graphics.Blit((Texture) this._cachedTexture, this._filteredTexture, this.blurMaterial);
      else
        Graphics.Blit((Texture) src, this._filteredTexture, this.blurMaterial);
      Graphics.Blit((Texture) src, dst);
    }
  }

  public void Restore()
  {
    RenderTextureFormat renderTextureFormat = (RenderTextureFormat) 4;
    this._filteredTexture = RenderTexture.GetTemporary(Screen.width, Screen.height, 0, renderTextureFormat);
    this._cachedTexture = RenderTexture.GetTemporary(Screen.width, Screen.height, 0, renderTextureFormat);
  }

  public void StartBlurFilter(
    float powerStart,
    float powerEnd,
    float duration,
    Vector2 blurCenter,
    System.Action onComplete)
  {
    this.StartCoroutine(this.BlurFilterImpl(powerStart, powerEnd, duration, blurCenter, onComplete));
  }

  private IEnumerator BlurFilterImpl(
    float powerStart,
    float powerEnd,
    float duration,
    Vector2 blurCenter,
    System.Action onComplete)
  {
    float timer = 0.0f;
    while ((double) timer < (double) duration)
    {
      timer += Time.deltaTime;
      this.SetBlurPram(Mathf.Lerp(powerStart, powerEnd, timer / duration), blurCenter);
      yield return (object) null;
    }
    if (onComplete != null)
      onComplete();
  }
}
