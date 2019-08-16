using System.Collections.Generic;

public abstract class UIntKeyTableBase
{
	public class Item
	{
		public uint key;

		public object value;

		public Item(uint _key, object _value)
		{
			key = _key;
			value = _value;
		}
	}

	protected List<Item>[] lists;

	protected bool useHashDivision = true;

	public UIntKeyTableBase()
	{
		useHashDivision = true;
	}

	public UIntKeyTableBase(bool useHashDivision)
	{
		this.useHashDivision = useHashDivision;
	}

	private List<Item> GetList(uint key)
	{
		if (lists == null)
		{
			return null;
		}
		return lists[GetHash(key)];
	}

	private Item GetItem(List<Item> list, uint key)
	{
		if (list == null)
		{
			return null;
		}
		List<Item>.Enumerator enumerator = list.GetEnumerator();
		while (enumerator.MoveNext())
		{
			if (enumerator.Current.key == key)
			{
				return enumerator.Current;
			}
		}
		return null;
	}

	protected bool _Add(uint key, object value)
	{
		if (lists == null)
		{
			if (useHashDivision)
			{
				lists = new List<Item>[256];
			}
			else
			{
				lists = new List<Item>[1];
			}
		}
		uint hash = GetHash(key);
		List<Item> list = lists[hash];
		if (list == null)
		{
			list = new List<Item>();
			lists[hash] = list;
		}
		list.Add(new Item(key, value));
		return true;
	}

	protected void _AddRange(UIntKeyTableBase table)
	{
		if (table.lists == null)
		{
			return;
		}
		if (lists == null)
		{
			if (useHashDivision)
			{
				lists = new List<Item>[256];
			}
			else
			{
				lists = new List<Item>[1];
			}
		}
		int i = 0;
		for (int num = table.lists.Length; i < num; i++)
		{
			List<Item> list = table.lists[i];
			if (list != null)
			{
				if (lists[i] == null)
				{
					lists[i] = new List<Item>(list);
				}
				else
				{
					lists[i].AddRange(list);
				}
			}
		}
	}

	protected object _Get(uint key)
	{
		List<Item> list = GetList(key);
		if (list == null)
		{
			return null;
		}
		return GetItem(list, key)?.value;
	}

	public void Remove(uint key)
	{
		List<Item> list = GetList(key);
		if (list != null)
		{
			Item item = GetItem(list, key);
			if (item != null)
			{
				list.Remove(item);
			}
		}
	}

	public virtual void Clear()
	{
		lists = null;
	}

	public uint GetHash(uint key)
	{
		if (useHashDivision)
		{
			return ((key & 0xFF) + ((key & 0xFF00) >> 8) + ((key & 0xFF0000) >> 16) + ((uint)((int)key & -16777216) >> 24)) & 0xFF;
		}
		return 0u;
	}

	public virtual void TrimExcess()
	{
		if (lists != null)
		{
			for (int i = 0; i < lists.Length; i++)
			{
				lists[i]?.TrimExcess();
			}
		}
	}
}
