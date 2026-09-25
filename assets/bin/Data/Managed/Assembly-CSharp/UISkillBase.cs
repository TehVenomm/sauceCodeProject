// Decompiled with JetBrains decompiler
// Type: UISkillBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class UISkillBase : WarpingEffect
{
  private void Start()
  {
    this.cacher = ((Component) MonoBehaviourSingleton<AppMain>.I.mainCamera).GetComponent<RenderTargetCacher>();
    this.cacher.onUpdateTexture += new Action<RenderTexture>(((WarpingEffect) this).OnUpdateTexture);
    try
    {
      this.atlasMaterial.SetTexture("_SrcTex", (Texture) this.cacher.GetTexture());
    }
    catch (UnassignedReferenceException ex)
    {
      Debug.Log((object) ("UISkillBase Error:" + (object) ex));
    }
  }
}
