// Decompiled with JetBrains decompiler
// Type: StringKeyTableBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
public abstract class StringKeyTableBase
{
  protected List<StringKeyTableBase.Item>[] lists;

  protected List<StringKeyTableBase.Item> GetList(string key)
  {
    return this.lists == null ? (List<StringKeyTableBase.Item>) null : this.lists[StringKeyTableBase.GetHashA(key)];
  }

  protected StringKeyTableBase.Item GetItem(List<StringKeyTableBase.Item> list, string key)
  {
    if (list == null)
      return (StringKeyTableBase.Item) null;
    int hashB = StringKeyTableBase.GetHashB(key);
    List<StringKeyTableBase.Item>.Enumerator enumerator = list.GetEnumerator();
    while (enumerator.MoveNext())
    {
      if (enumerator.Current.hash == hashB && enumerator.Current.key == key)
        return enumerator.Current;
    }
    return (StringKeyTableBase.Item) null;
  }

  protected void _Add(string key, object value)
  {
    if (string.IsNullOrEmpty(key))
      return;
    if (this.lists == null)
      this.lists = new List<StringKeyTableBase.Item>[256 /*0x0100*/];
    int hashA = StringKeyTableBase.GetHashA(key);
    List<StringKeyTableBase.Item> objList = this.lists[hashA];
    if (objList == null)
    {
      objList = new List<StringKeyTableBase.Item>();
      this.lists[hashA] = objList;
    }
    objList.Add(new StringKeyTableBase.Item(key, value));
  }

  protected object _Get(string key)
  {
    if (string.IsNullOrEmpty(key))
      return (object) null;
    List<StringKeyTableBase.Item> list = this.GetList(key);
    if (list == null)
      return (object) null;
    return this.GetItem(list, key)?.value;
  }

  public void Remove(string key)
  {
    List<StringKeyTableBase.Item> list = this.GetList(key);
    if (list == null)
      return;
    StringKeyTableBase.Item obj = this.GetItem(list, key);
    if (obj == null)
      return;
    list.Remove(obj);
  }

  public void Clear() => this.lists = (List<StringKeyTableBase.Item>[]) null;

  public static int GetHashA(string key)
  {
    int num = 0;
    int index = 0;
    for (int length = key.Length; index < length; ++index)
      num = (num << 1 | num >> 31 /*0x1F*/ & 1) + (int) key[index];
    return num & (int) byte.MaxValue;
  }

  public static int GetHashB(string key)
  {
    int hashB = 0;
    int index = 0;
    for (int length = key.Length; index < length; ++index)
    {
      int num = index & 31 /*0x1F*/;
      hashB = hashB << num | (hashB >> 31 /*0x1F*/ - num) + (int) key[index];
    }
    return hashB;
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
    public int hash;
    public string key;
    public object value;

    public Item(string _key, object _value)
    {
      this.hash = StringKeyTableBase.GetHashB(_key);
      this.key = _key;
      this.value = _value;
    }
  }
}
