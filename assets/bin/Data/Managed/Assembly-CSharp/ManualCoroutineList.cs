// Decompiled with JetBrains decompiler
// Type: ManualCoroutineList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class ManualCoroutineList
{
  public List<ManualCoroutine> list = new List<ManualCoroutine>();
  public List<int> stack = new List<int>();

  public void Add(ManualCoroutine mc) => this.list.Add(mc);

  public void Remove(int id)
  {
    while (true)
    {
      int index = this.list.FindIndex((Predicate<ManualCoroutine>) (o => o.id == id));
      if (index != -1)
      {
        this.list[index].Clear();
        this.list.RemoveAt(index);
      }
      else
        break;
    }
  }

  public void SetActive(int id, bool is_active = true)
  {
    this.list.ForEach((Action<ManualCoroutine>) (o =>
    {
      if (o.id != id)
        return;
      o.active = is_active;
    }));
  }

  public void SetActiveToggle(int id, bool is_active = true)
  {
    bool inv_is_active = !is_active;
    this.list.ForEach((Action<ManualCoroutine>) (o =>
    {
      if (o.id == id)
        o.active = is_active;
      else
        o.active = inv_is_active;
    }));
  }

  public void Push(int id)
  {
    this.stack.Add(id);
    this.SetActiveToggle(id);
  }

  public void Pop()
  {
    if (this.stack.Count > 0)
      this.stack.RemoveAt(this.stack.Count - 1);
    if (this.stack.Count <= 0)
      return;
    this.SetActiveToggle(this.stack[this.stack.Count - 1]);
  }

  public int Peek() => this.stack.Count > 0 ? this.stack[this.stack.Count - 1] : -1;

  public int GetActiveCount(int id)
  {
    int count = 0;
    this.list.ForEach((Action<ManualCoroutine>) (o =>
    {
      if (o.id != id || !o.active || !o.isEnabled)
        return;
      ++count;
    }));
    return count;
  }
}
