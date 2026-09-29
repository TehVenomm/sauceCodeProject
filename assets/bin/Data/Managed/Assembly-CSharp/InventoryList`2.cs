// Decompiled with JetBrains decompiler
// Type: InventoryList`2
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class InventoryList<T, RECV_DATA> where T : ItemInfoBase<RECV_DATA>, new()
{
  protected LinkedList<T> list { private set; get; }

  public LinkedListNode<T> GetFirstNode() => this.list.First;

  public LinkedListNode<T> GetLastNode() => this.list.Last;

  public int GetCount() => this.list.Count;

  public T Find(ulong uniq_id)
  {
    for (LinkedListNode<T> linkedListNode = this.list.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
    {
      if ((long) linkedListNode.Value.uniqueID == (long) uniq_id)
        return linkedListNode.Value;
    }
    return default (T);
  }

  public uint GetTableID(string str_uniq_id)
  {
    if (string.IsNullOrEmpty(str_uniq_id))
      return 0;
    ulong num = ulong.Parse(str_uniq_id);
    for (LinkedListNode<T> linkedListNode = this.list.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
    {
      if ((long) linkedListNode.Value.uniqueID == (long) num)
        return linkedListNode.Value.tableID;
    }
    return 0;
  }

  public List<ulong> GetUniqueIDList(uint table_id)
  {
    List<ulong> ulongList = new List<ulong>();
    LinkedListNode<T> first = this.list.First;
    while (first != null)
    {
      if ((int) first.Value.tableID == (int) table_id)
        ulongList.Add(first.Value.uniqueID);
    }
    return ulongList.Count > 0 ? ulongList : (List<ulong>) null;
  }

  public InventoryList() => this.list = new LinkedList<T>();

  public T Add(RECV_DATA item)
  {
    if ((object) item == null)
      return default (T);
    T obj = new T();
    obj.SetValue(item);
    this.list.AddLast(obj);
    return obj;
  }

  public T Set(string uniq_id, RECV_DATA item)
  {
    if ((object) item == null)
      return default (T);
    T obj1 = this.Overwrite(ulong.Parse(uniq_id), item);
    if ((object) obj1 != null)
      return obj1;
    T obj2 = new T();
    obj2.SetValue(item);
    this.list.AddLast(obj2);
    return obj2;
  }

  public void AddRange(List<RECV_DATA> item_list)
  {
    if (item_list == null || item_list.Count == 0)
      return;
    item_list.ForEach((Action<RECV_DATA>) (item =>
    {
      T obj = new T();
      obj.SetValue(item);
      this.list.AddLast(obj);
    }));
  }

  public T Overwrite(string uniq_id, RECV_DATA item) => this.Overwrite(ulong.Parse(uniq_id), item);

  public T Overwrite(ulong uniq_id, RECV_DATA item)
  {
    for (LinkedListNode<T> linkedListNode = this.list.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
    {
      if ((long) linkedListNode.Value.uniqueID == (long) uniq_id)
      {
        linkedListNode.Value = new T();
        linkedListNode.Value.SetValue(item);
        return linkedListNode.Value;
      }
    }
    return default (T);
  }

  public void Delete(ulong unique_id)
  {
    for (LinkedListNode<T> node = this.list.First; node != null; node = node.Next)
    {
      if ((long) node.Value.uniqueID == (long) unique_id)
      {
        this.list.Remove(node);
        break;
      }
    }
  }

  public List<T> GetAll()
  {
    List<T> all = new List<T>(this.GetCount());
    for (LinkedListNode<T> linkedListNode = this.GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
      all.Add(linkedListNode.Value);
    return all;
  }

  public static InventoryList<T, RECV_DATA> CreateList(List<RECV_DATA> recv_list)
  {
    InventoryList<T, RECV_DATA> list = new InventoryList<T, RECV_DATA>();
    recv_list.ForEach((Action<RECV_DATA>) (o => list.Add(o)));
    return list;
  }
}
