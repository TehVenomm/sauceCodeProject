// Decompiled with JetBrains decompiler
// Type: StringKeyTable`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class StringKeyTable<T> : StringKeyTableBase
{
  public void Add(string key, T value) => this._Add(key, (object) value);

  public T Get(string key) => (T) this._Get(key);

  public List<StringKeyTableBase.Item>[] GetList() => this.lists;

  public void ForEach(Action<T> action)
  {
    if (this.lists == null)
      return;
    int index = 0;
    for (int length = this.lists.Length; index < length; ++index)
      this.lists[index]?.ForEach((Action<StringKeyTableBase.Item>) (o => action((T) o.value)));
  }

  public void ForEachKeys(Action<string> action)
  {
    if (this.lists == null)
      return;
    int index = 0;
    for (int length = this.lists.Length; index < length; ++index)
      this.lists[index]?.ForEach((Action<StringKeyTableBase.Item>) (o => action(o.key)));
  }

  public void ForEachKeyAndValue(Action<string, T> action)
  {
    if (this.lists == null)
      return;
    int index = 0;
    for (int length = this.lists.Length; index < length; ++index)
      this.lists[index]?.ForEach((Action<StringKeyTableBase.Item>) (o => action(o.key, (T) o.value)));
  }

  protected virtual bool ReadCSV(CSVReader csv, T data, ref string key) => false;
}
