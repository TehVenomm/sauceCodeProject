// Decompiled with JetBrains decompiler
// Type: WarpingEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class WarpingEffect : MonoBehaviour
{
  [SerializeField]
  protected UISprite sprite;
  [SerializeField]
  protected RenderTargetCacher cacher;
  [SerializeField]
  protected Material atlasMaterial;

  private void Awake()
  {
    if (!Object.op_Inequality((Object) this.atlasMaterial, (Object) null) || !Object.op_Inequality((Object) this.sprite, (Object) null) || !Object.op_Inequality((Object) this.sprite.atlas, (Object) null) || !Object.op_Inequality((Object) this.sprite.atlas.spriteMaterial, (Object) null) || !Object.op_Inequality((Object) this.atlasMaterial, (Object) this.sprite.atlas.spriteMaterial))
      return;
    this.atlasMaterial = this.sprite.atlas.spriteMaterial;
  }

  protected void OnUpdateTexture(RenderTexture rt)
  {
    this.atlasMaterial.SetTexture("_SrcTex", (Texture) rt);
    foreach (UIDrawCall active in UIDrawCall.activeList)
    {
      if (Object.op_Equality((Object) active.baseMaterial, (Object) this.atlasMaterial))
        active.dynamicMaterial.SetTexture("_SrcTex", (Texture) rt);
    }
  }

  protected virtual void OnDestroy()
  {
    if (!Object.op_Inequality((Object) this.cacher, (Object) null))
      return;
    this.cacher.onUpdateTexture -= new Action<RenderTexture>(this.OnUpdateTexture);
  }
}
