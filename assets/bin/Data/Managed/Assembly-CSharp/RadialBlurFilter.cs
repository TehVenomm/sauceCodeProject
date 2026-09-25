// Decompiled with JetBrains decompiler
// Type: RadialBlurFilter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class RadialBlurFilter : FilterBase
{
  [SerializeField]
  private Material _blurMaterial;
  [SerializeField]
  private float _strength;
  private PostEffector postEffector;

  public Material blurMaterial => this._blurMaterial;

  public float strength
  {
    get => this._strength;
    set
    {
      this._strength = value;
      if (!Object.op_Inequality((Object) this._blurMaterial, (Object) null))
        return;
      this._blurMaterial.SetFloat("_Power", this._strength);
    }
  }

  public void SetCenter(Vector2 screenPos)
  {
    if (!Object.op_Inequality((Object) this._blurMaterial, (Object) null))
      return;
    this._blurMaterial.SetVector("_Origin", Vector4.op_Implicit(screenPos));
  }

  private void Awake()
  {
    this._blurMaterial = new Material(ResourceUtility.FindShader("mobile/Custom/ImageEffect/RadialBlurFilter"));
  }

  public override void StartFilter()
  {
    this.postEffector = ((Component) this).gameObject.AddComponent<PostEffector>();
    this.postEffector.SetFilter((FilterBase) this);
  }

  public override void StopFilter()
  {
    if (!Object.op_Inequality((Object) this.postEffector, (Object) null))
      return;
    Object.Destroy((Object) this.postEffector);
    this.postEffector = (PostEffector) null;
  }

  public override void PostEffectProc(RenderTexture src, RenderTexture dest)
  {
    if (Object.op_Inequality((Object) this._blurMaterial, (Object) null))
      Graphics.Blit((Texture) src, dest, this._blurMaterial);
    else
      Graphics.Blit((Texture) src, dest);
  }
}
