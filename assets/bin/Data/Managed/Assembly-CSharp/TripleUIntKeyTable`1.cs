// Decompiled with JetBrains decompiler
// Type: TripleUIntKeyTable`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class TripleUIntKeyTable<T> : UIntKeyTable<UIntKeyTable<UIntKeyTable<T>>>
{
  public T Get(uint key1, uint key2, uint key3)
  {
    UIntKeyTable<UIntKeyTable<T>> uintKeyTable1 = this.Get(key1);
    if (uintKeyTable1 != null)
    {
      UIntKeyTable<T> uintKeyTable2 = uintKeyTable1.Get(key2);
      if (uintKeyTable2 != null)
        return uintKeyTable2.Get(key3);
    }
    return default (T);
  }

  public void Add(uint key1, uint key2, uint key3, T value)
  {
    UIntKeyTable<UIntKeyTable<T>> uintKeyTable1 = this.Get(key1);
    if (uintKeyTable1 == null)
    {
      uintKeyTable1 = new UIntKeyTable<UIntKeyTable<T>>(false);
      this.Add(key1, uintKeyTable1);
    }
    UIntKeyTable<T> uintKeyTable2 = uintKeyTable1.Get(key2);
    if (uintKeyTable2 == null)
    {
      uintKeyTable2 = new UIntKeyTable<T>(false);
      uintKeyTable1.Add(key2, uintKeyTable2);
    }
    uintKeyTable2.Add(key3, value);
  }

  public void ForEachTripleKeyValue(Action<uint, uint, uint, T> a)
  {
    this.ForEachKeyValue((Action<uint, UIntKeyTable<UIntKeyTable<T>>>) ((key1, table2) => table2.ForEachKeyValue((Action<uint, UIntKeyTable<T>>) ((key2, table3) => table3.ForEachKeyValue((Action<uint, T>) ((key3, data) => a(key1, key2, key3, data)))))));
  }

  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    TripleUIntKeyTable<T> rhs = obj as TripleUIntKeyTable<T>;
    if (rhs == null || this.GetCount() != rhs.GetCount())
      return false;
    bool isEqual = true;
    this.ForEachTripleKeyValue((Action<uint, uint, uint, T>) ((key1, key2, key3, value1) =>
    {
      T obj1 = rhs.Get(key1, key2, key3);
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
          if (!(item.value is List<UIntKeyTableBase.Item> objList2))
            return;
          objList2.ForEach((Action<UIntKeyTableBase.Item>) (item2 =>
          {
            if (!(item2.value is UIntKeyTableBase uintKeyTableBase2))
              return;
            uintKeyTableBase2.TrimExcess();
          }));
          objList2.TrimExcess();
        }));
        list.TrimExcess();
      }
    }
  }
}
