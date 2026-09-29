// Decompiled with JetBrains decompiler
// Type: DoubleUIntKeyTable`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class DoubleUIntKeyTable<T> : UIntKeyTable<UIntKeyTable<T>>
{
  public T Get(uint key1, uint key2)
  {
    UIntKeyTable<T> uintKeyTable = this.Get(key1);
    return uintKeyTable == null ? default (T) : uintKeyTable.Get(key2);
  }

  public void Add(uint key1, uint key2, T value)
  {
    UIntKeyTable<T> uintKeyTable = this.Get(key1);
    if (uintKeyTable == null)
    {
      uintKeyTable = new UIntKeyTable<T>(false);
      this.Add(key1, uintKeyTable);
    }
    uintKeyTable.Add(key2, value);
  }

  public void ForEachDoubleKeyValue(Action<uint, uint, T> a)
  {
    this.ForEachKeyValue((Action<uint, UIntKeyTable<T>>) ((key1, table2) => table2.ForEachKeyValue((Action<uint, T>) ((key2, data) => a(key1, key2, data)))));
  }

  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    DoubleUIntKeyTable<T> rhs = obj as DoubleUIntKeyTable<T>;
    if (rhs == null || this.GetCount() != rhs.GetCount())
      return false;
    bool isEqual = true;
    this.ForEachDoubleKeyValue((Action<uint, uint, T>) ((key1, key2, value1) =>
    {
      T obj1 = rhs.Get(key1, key2);
      isEqual = isEqual && value1.Equals((object) obj1);
    }));
    return isEqual;
  }

  public override int GetHashCode() => base.GetHashCode();

  public override void TrimExcess()
  {
    if (this.lists == null)
      return;
    for (int index = 0; index < this.lists.Length; ++index)
    {
      List<UIntKeyTableBase.Item> list = this.lists[index];
      if (list != null)
      {
        list.ForEach((Action<UIntKeyTableBase.Item>) (item =>
        {
          if (!(item.value is UIntKeyTableBase uintKeyTableBase2))
            return;
          uintKeyTableBase2.TrimExcess();
        }));
        list.TrimExcess();
      }
    }
  }
}
