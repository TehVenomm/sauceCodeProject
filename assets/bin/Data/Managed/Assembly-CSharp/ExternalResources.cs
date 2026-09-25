// Decompiled with JetBrains decompiler
// Type: ExternalResources
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using UnityEngine;

#nullable disable
public static class ExternalResources
{
  public static T Load<T>(string path) where T : Object
  {
    T obj = Resources.Load<T>(path);
    return Object.op_Inequality((Object) obj, (Object) null) ? obj : default (T);
  }

  public static IEnumerator LoadAsync<T>(
    string path,
    Action<ResourceRequest> progress,
    Action<T> complete)
    where T : Object
  {
    ResourceRequest request = Resources.LoadAsync<T>(path);
    if (!((AsyncOperation) request).isDone)
    {
      progress(request);
      yield return (object) null;
    }
    if (Object.op_Inequality(request.asset, (Object) null))
      complete((T) request.asset);
  }
}
