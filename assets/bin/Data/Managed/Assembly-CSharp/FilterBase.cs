// Decompiled with JetBrains decompiler
// Type: FilterBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class FilterBase : MonoBehaviour
{
  [SerializeField]
  private RenderTargetCacher cacher;

  public virtual void PostEffectProc(RenderTexture src, RenderTexture dest)
  {
    Graphics.Blit((Texture) src, dest);
  }

  public virtual void StartFilter()
  {
  }

  public virtual void StopFilter()
  {
  }
}
