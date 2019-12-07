using System.Collections.Generic;

public class InventoryList<T, RECV_DATA> where T : ItemInfoBase<RECV_DATA>, new()
{
	protected LinkedList<T> list
	{
		get;
		private set;
	}

	public LinkedListNode<T> GetFirstNode()
	{
		return list.First;
	}

	public LinkedListNode<T> GetLastNode()
	{
		return list.Last;
	}

	public int GetCount()
	{
		return list.Count;
	}

	public T Find(ulong uniq_id)
	{
		for (LinkedListNode<T> linkedListNode = list.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			if (linkedListNode.Value.uniqueID == uniq_id)
			{
				return linkedListNode.Value;
			}
		}
		return null;
	}

	public uint GetTableID(string str_uniq_id)
	{
		if (string.IsNullOrEmpty(str_uniq_id))
		{
			return 0u;
		}
		ulong num = ulong.Parse(str_uniq_id);
		for (LinkedListNode<T> linkedListNode = list.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			if (linkedListNode.Value.uniqueID == num)
			{
				return linkedListNode.Value.tableID;
			}
		}
		return 0u;
	}

	public List<ulong> GetUniqueIDList(uint table_id)
	{
		List<ulong> list = new List<ulong>();
		LinkedListNode<T> first = this.list.First;
		while (first != null)
		{
			if (first.Value.tableID == table_id)
			{
				list.Add(first.Value.uniqueID);
			}
		}
		if (list.Count > 0)
		{
			return list;
		}
		return null;
	}

	public InventoryList()
	{
		list = new LinkedList<T>();
	}

	public T Add(RECV_DATA item)
	{
		if (item == null)
		{
			return null;
		}
		T val = new T();
		val.SetValue(item);
		list.AddLast(val);
		return val;
	}

	public T Set(string uniq_id, RECV_DATA item)
	{
		if (item == null)
		{
			return null;
		}
		ulong uniq_id2 = ulong.Parse(uniq_id);
		T val = Overwrite(uniq_id2, item);
		if (val != null)
		{
			return val;
		}
		val = new T();
		val.SetValue(item);
		list.AddLast(val);
		return val;
	}

	public void AddRange(List<RECV_DATA> item_list)
	{
		if (item_list != null && item_list.Count != 0)
		{
			item_list.ForEach(delegate(RECV_DATA item)
			{
				T val = new T();
				val.SetValue(item);
				list.AddLast(val);
			});
		}
	}

	public T Overwrite(string uniq_id, RECV_DATA item)
	{
		return Overwrite(ulong.Parse(uniq_id), item);
	}

	public T Overwrite(ulong uniq_id, RECV_DATA item)
	{
		for (LinkedListNode<T> linkedListNode = list.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			if (linkedListNode.Value.uniqueID == uniq_id)
			{
				linkedListNode.Value = new T();
				linkedListNode.Value.SetValue(item);
				return linkedListNode.Value;
			}
		}
		return null;
	}

	public void Delete(ulong unique_id)
	{
		LinkedListNode<T> linkedListNode = list.First;
		while (true)
		{
			if (linkedListNode != null)
			{
				if (linkedListNode.Value.uniqueID == unique_id)
				{
					break;
				}
				linkedListNode = linkedListNode.Next;
				continue;
			}
			return;
		}
		list.Remove(linkedListNode);
	}

	public List<T> GetAll()
	{
		List<T> list = new List<T>(GetCount());
		for (LinkedListNode<T> linkedListNode = GetFirstNode(); linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			list.Add(linkedListNode.Value);
		}
		return list;
	}

	public static InventoryList<T, RECV_DATA> CreateList(List<RECV_DATA> recv_list)
	{
		InventoryList<T, RECV_DATA> list = new InventoryList<T, RECV_DATA>();
		recv_list.ForEach(delegate(RECV_DATA o)
		{
			list.Add(o);
		});
		return list;
	}
}
