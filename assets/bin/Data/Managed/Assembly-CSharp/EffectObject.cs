// Decompiled with JetBrains decompiler
// Type: EffectObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class EffectObject : MonoBehaviour
{
  public static bool wait = true;
  public string effectName;

  private IEnumerator Start()
  {
    while (EffectObject.wait)
      yield return (object) null;
    EffectManager.GetEffect(this.effectName, ((Component) this).transform);
    yield return (object) null;
    Object.DestroyImmediate((Object) this);
  }
}
