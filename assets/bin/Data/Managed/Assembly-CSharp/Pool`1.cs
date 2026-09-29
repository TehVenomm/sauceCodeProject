// Decompiled with JetBrains decompiler
// Type: Pool`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class Pool<ABS_T> where ABS_T : Poolable
{
  private StringKeyTable<Queue<ABS_T>> poolablesOfType = new StringKeyTable<Queue<ABS_T>>();

  private string GetKey<T>() => typeof (T).ToString();

  private string GetKey(System.Type type) => type.ToString();

  public T Alloc<T>() where T : Poolable, ABS_T, new()
  {
    Queue<ABS_T> absTQueue = this.poolablesOfType.Get(this.GetKey<T>());
    if (absTQueue == null)
    {
      absTQueue = new Queue<ABS_T>();
      this.poolablesOfType.Add(this.GetKey<T>(), absTQueue);
    }
    T obj1 = default (T);
    T obj2;
    if (absTQueue.Count > 0)
    {
      obj2 = (T) (object) absTQueue.Dequeue();
    }
    else
    {
      obj2 = new T();
      obj2.OnAwake();
    }
    obj2.OnInit();
    return obj2;
  }

  public void Free(ABS_T poolable)
  {
    Queue<ABS_T> absTQueue = this.poolablesOfType.Get(this.GetKey(poolable.GetType()));
    if (absTQueue == null)
    {
      Debug.LogError((object) ("Pool: not alloc poolable. poolable=" + (object) poolable));
    }
    else
    {
      poolable.OnFinal();
      absTQueue.Enqueue(poolable);
    }
  }

  public void Clear() => this.poolablesOfType.ForEach((Action<Queue<ABS_T>>) (p => p.Clear()));
}
