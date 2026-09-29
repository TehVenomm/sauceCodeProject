// Decompiled with JetBrains decompiler
// Type: UIntKeyTable`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class UIntKeyTable<T> : UIntKeyTableBase
{
  public UIntKeyTable() => this.useHashDivision = true;

  public UIntKeyTable(bool useHashDivision) => this.useHashDivision = useHashDivision;

  public bool Add(uint key, T value) => this._Add(key, (object) value);

  public void AddRange(UIntKeyTable<T> table) => this._AddRange((UIntKeyTableBase) table);

  public T Get(uint key) => (T) this._Get(key);

  public void ForEach(Action<T> action)
  {
    if (this.lists == null)
      return;
    int index = 0;
    for (int length = this.lists.Length; index < length; ++index)
      this.lists[index]?.ForEach((Action<UIntKeyTableBase.Item>) (o => action((T) o.value)));
  }

  public void ForEachAsc(Action<T> _action)
  {
    if (_action == null || this.lists == null || this.lists.Length < 1)
      return;
    List<UIntKeyTableBase.Item> objList = new List<UIntKeyTableBase.Item>();
    int index1 = 0;
    for (int length = this.lists.Length; index1 < length; ++index1)
    {
      if (this.lists[index1] != null)
        objList.AddRange((IEnumerable<UIntKeyTableBase.Item>) this.lists[index1]);
    }
    objList.Sort((Comparison<UIntKeyTableBase.Item>) ((a, b) =>
    {
      if (a.key < b.key)
        return -1;
      return (int) a.key != (int) b.key ? 1 : 0;
    }));
    int index2 = 0;
    for (int count = objList.Count; index2 < count; ++index2)
      _action((T) objList[index2].value);
  }

  public void ForEachDesc(Action<T> _action)
  {
    if (_action == null || this.lists == null || this.lists.Length < 1)
      return;
    List<UIntKeyTableBase.Item> objList = new List<UIntKeyTableBase.Item>();
    int index1 = 0;
    for (int length = this.lists.Length; index1 < length; ++index1)
    {
      if (this.lists[index1] != null)
        objList.AddRange((IEnumerable<UIntKeyTableBase.Item>) this.lists[index1]);
    }
    objList.Sort((Comparison<UIntKeyTableBase.Item>) ((a, b) =>
    {
      if (a.key < b.key)
        return 1;
      return (int) a.key != (int) b.key ? -1 : 0;
    }));
    int index2 = 0;
    for (int count = objList.Count; index2 < count; ++index2)
      _action((T) objList[index2].value);
  }

  public void ForEachKey(Action<uint> action)
  {
    if (this.lists == null)
      return;
    int index = 0;
    for (int length = this.lists.Length; index < length; ++index)
      this.lists[index]?.ForEach((Action<UIntKeyTableBase.Item>) (o => action(o.key)));
  }

  public void ForEachKeyValue(Action<uint, T> action)
  {
    if (this.lists == null)
      return;
    int index = 0;
    for (int length = this.lists.Length; index < length; ++index)
      this.lists[index]?.ForEach((Action<UIntKeyTableBase.Item>) (o => action(o.key, (T) o.value)));
  }

  public T Find(Predicate<T> match)
  {
    if (this.lists == null)
      return default (T);
    int index = 0;
    for (int length = this.lists.Length; index < length; ++index)
    {
      List<UIntKeyTableBase.Item> list = this.lists[index];
      if (list != null)
      {
        UIntKeyTableBase.Item obj = list.Find((Predicate<UIntKeyTableBase.Item>) (o => match((T) o.value)));
        if (obj != null)
          return (T) obj.value;
      }
    }
    return default (T);
  }

  public int RemoveAll(Predicate<T> match)
  {
    if (this.lists == null)
      return 0;
    int num = 0;
    int index = 0;
    for (int length = this.lists.Length; index < length; ++index)
    {
      List<UIntKeyTableBase.Item> list = this.lists[index];
      if (list != null)
        num += list.RemoveAll((Predicate<UIntKeyTableBase.Item>) (o => match((T) o.value)));
    }
    return num;
  }

  public virtual int GetCount()
  {
    if (this.lists == null)
      return 0;
    int count = 0;
    int index = 0;
    for (int length = this.lists.Length; index < length; ++index)
    {
      List<UIntKeyTableBase.Item> list = this.lists[index];
      if (list != null)
        count += list.Count;
    }
    return count;
  }

  protected virtual bool ReadCSV(CSVReader csv, T data, ref uint key) => false;

  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    UIntKeyTable<T> rhs = obj as UIntKeyTable<T>;
    if (rhs == null || this.GetCount() != rhs.GetCount())
      return false;
    bool isEqual = true;
    this.ForEachKeyValue((Action<uint, T>) ((key, value1) =>
    {
      T obj1 = rhs.Get(key);
      isEqual = isEqual && value1.Equals((object) obj1);
    }));
    return isEqual;
  }

  public override int GetHashCode() => base.GetHashCode();
}
