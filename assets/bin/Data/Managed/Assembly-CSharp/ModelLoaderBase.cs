// Decompiled with JetBrains decompiler
// Type: ModelLoaderBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public abstract class ModelLoaderBase : MonoBehaviour
{
  public abstract bool IsLoading();

  public abstract Animator GetAnimator();

  public abstract void SetEnabled(bool is_enable);

  public abstract Transform GetHead();

  public static void SetEnabled(Renderer[] renderers, bool is_enable)
  {
    if (renderers == null)
      return;
    int index = 0;
    for (int length = renderers.Length; index < length; ++index)
      renderers[index].enabled = is_enable;
  }
}
