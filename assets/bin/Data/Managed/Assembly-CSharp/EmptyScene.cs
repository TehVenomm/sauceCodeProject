// Decompiled with JetBrains decompiler
// Type: EmptyScene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class EmptyScene : MonoBehaviour
{
  public static bool IsClearCache { get; set; }

  private IEnumerator Start()
  {
    Resources.UnloadUnusedAssets();
    yield return (object) new WaitForEndOfFrame();
    yield return (object) new WaitForEndOfFrame();
    yield return (object) new WaitForEndOfFrame();
    if (EmptyScene.IsClearCache)
    {
      EmptyScene.IsClearCache = false;
      yield return (object) this.StartCoroutine(ResourceManager.ClearCache());
      yield return (object) new WaitForEndOfFrame();
      yield return (object) new WaitForEndOfFrame();
      yield return (object) new WaitForEndOfFrame();
    }
  }
}
