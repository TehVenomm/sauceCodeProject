// Decompiled with JetBrains decompiler
// Type: UIntKeyTableBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public abstract class UIntKeyTableBase
{
  protected List<UIntKeyTableBase.Item>[] lists;
  protected bool useHashDivision = true;

  public UIntKeyTableBase() => this.useHashDivision = true;

  public UIntKeyTableBase(bool useHashDivision) => this.useHashDivision = useHashDivision;

  private List<UIntKeyTableBase.Item> GetList(uint key)
  {
    return this.lists == null ? (List<UIntKeyTableBase.Item>) null : this.lists[(int) this.GetHash(key)];
  }

  private UIntKeyTableBase.Item GetItem(List<UIntKeyTableBase.Item> list, uint key)
  {
    if (list == null)
      return (UIntKeyTableBase.Item) null;
    List<UIntKeyTableBase.Item>.Enumerator enumerator = list.GetEnumerator();
    while (enumerator.MoveNext())
    {
      if ((int) enumerator.Current.key == (int) key)
        return enumerator.Current;
    }
    return (UIntKeyTableBase.Item) null;
  }

  protected bool _Add(uint key, object value)
  {
    if (this.lists == null)
      this.lists = !this.useHashDivision ? new List<UIntKeyTableBase.Item>[1] : new List<UIntKeyTableBase.Item>[256 /*0x0100*/];
    uint hash = this.GetHash(key);
    List<UIntKeyTableBase.Item> objList = this.lists[(int) hash];
    if (objList == null)
    {
      objList = new List<UIntKeyTableBase.Item>();
      this.lists[(int) hash] = objList;
    }
    objList.Add(new UIntKeyTableBase.Item(key, value));
    return true;
  }

  protected void _AddRange(UIntKeyTableBase table)
  {
    if (table.lists == null)
      return;
    if (this.lists == null)
      this.lists = !this.useHashDivision ? new List<UIntKeyTableBase.Item>[1] : new List<UIntKeyTableBase.Item>[256 /*0x0100*/];
    int index = 0;
    for (int length = table.lists.Length; index < length; ++index)
    {
      List<UIntKeyTableBase.Item> list = table.lists[index];
      if (list != null)
      {
        if (this.lists[index] == null)
          this.lists[index] = new List<UIntKeyTableBase.Item>((IEnumerable<UIntKeyTableBase.Item>) list);
        else
          this.lists[index].AddRange((IEnumerable<UIntKeyTableBase.Item>) list);
      }
    }
  }

  protected object _Get(uint key)
  {
    List<UIntKeyTableBase.Item> list = this.GetList(key);
    if (list == null)
      return (object) null;
    return this.GetItem(list, key)?.value;
  }

  public void Remove(uint key)
  {
    List<UIntKeyTableBase.Item> list = this.GetList(key);
    if (list == null)
      return;
    UIntKeyTableBase.Item obj = this.GetItem(list, key);
    if (obj == null)
      return;
    list.Remove(obj);
  }

  public virtual void Clear() => this.lists = (List<UIntKeyTableBase.Item>[]) null;

  public uint GetHash(uint key)
  {
    return this.useHashDivision ? (uint) (((int) key & (int) byte.MaxValue) + (int) ((key & 65280U) >> 8) + (int) ((key & 16711680U /*0xFF0000*/) >> 16 /*0x10*/) + (int) ((key & 4278190080U /*0xFF000000*/) >> 24) & (int) byte.MaxValue) : 0U;
  }

  public virtual void TrimExcess()
  {
    if (this.lists == null)
      return;
    for (int index = 0; index < this.lists.Length; ++index)
      this.lists[index]?.TrimExcess();
  }

  public class Item
  {
    public uint key;
    public object value;

    public Item(uint _key, object _value)
    {
      this.key = _key;
      this.value = _value;
    }
  }
}
