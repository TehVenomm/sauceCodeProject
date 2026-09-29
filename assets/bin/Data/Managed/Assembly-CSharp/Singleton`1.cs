// Decompiled with JetBrains decompiler
// Type: Singleton`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class Singleton<T> : SingletonBase where T : new()
{
  public static T I;

  public static void Create()
  {
    if ((object) Singleton<T>.I != null)
      return;
    Singleton<T>.I = new T();
    SingletonBase.AddInstance((object) Singleton<T>.I);
  }

  public static bool IsValid() => (object) Singleton<T>.I != null;

  public override void Remove()
  {
    SingletonBase.instanceList.Remove((object) this);
    Singleton<T>.I = default (T);
  }

  public void DoAction(MonoBehaviour target, LoadingQueue load_queue, System.Action action)
  {
    target.StartCoroutine(this.DoActionAsync(load_queue, action));
  }

  private IEnumerator DoActionAsync(LoadingQueue load_queue, System.Action action)
  {
    while (load_queue.IsLoading())
      yield return (object) null;
    if (action != null)
      action();
  }
}
