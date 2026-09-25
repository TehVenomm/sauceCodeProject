// Decompiled with JetBrains decompiler
// Type: PriorityQueue`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class PriorityQueue<T> where T : IComparable<T>
{
  private List<T> data;

  public PriorityQueue() => this.data = new List<T>();

  public void Clear() => this.data.Clear();

  public void Enqueue(T item)
  {
    this.data.Add(item);
    int index1;
    for (int index2 = this.data.Count - 1; index2 > 0; index2 = index1)
    {
      index1 = (index2 - 1) / 2;
      if (this.data[index2].CompareTo(this.data[index1]) >= 0)
        break;
      T obj = this.data[index2];
      this.data[index2] = this.data[index1];
      this.data[index1] = obj;
    }
  }

  public T Dequeue()
  {
    int index1 = this.data.Count - 1;
    T obj1 = this.data[0];
    this.data[0] = this.data[index1];
    this.data.RemoveAt(index1);
    int num = index1 - 1;
    int index2 = 0;
    while (true)
    {
      int index3 = index2 * 2 + 1;
      if (index3 <= num)
      {
        int index4 = index3 + 1;
        T obj2;
        if (index4 <= num)
        {
          obj2 = this.data[index4];
          if (obj2.CompareTo(this.data[index3]) < 0)
            index3 = index4;
        }
        obj2 = this.data[index2];
        if (obj2.CompareTo(this.data[index3]) > 0)
        {
          T obj3 = this.data[index2];
          this.data[index2] = this.data[index3];
          this.data[index3] = obj3;
          index2 = index3;
        }
        else
          break;
      }
      else
        break;
    }
    return obj1;
  }

  public T Peek() => this.data[0];

  public int Count() => this.data.Count;

  public bool IsConsistent()
  {
    if (this.data.Count == 0)
      return true;
    int num = this.data.Count - 1;
    for (int index1 = 0; index1 < this.data.Count; ++index1)
    {
      int index2 = 2 * index1 + 1;
      int index3 = 2 * index1 + 2;
      T obj;
      if (index2 <= num)
      {
        obj = this.data[index1];
        if (obj.CompareTo(this.data[index2]) > 0)
          return false;
      }
      if (index3 <= num)
      {
        obj = this.data[index1];
        if (obj.CompareTo(this.data[index3]) > 0)
          return false;
      }
    }
    return true;
  }

  public void Remove(T item)
  {
    if (!this.data.Contains(item))
      return;
    this.data.Remove(item);
  }

  public T Find(string id)
  {
    return this.data == null ? default (T) : this.data.Find((Predicate<T>) (x => x.ToString() == id));
  }
}
