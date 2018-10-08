using System.Collections.Generic;

public abstract class StringKeyTableBase
{
	public class Item
	{
		public int hash;

		public string key;

		public object value;

		public Item(string _key, object _value)
		{
			hash = GetHashB(_key);
			key = _key;
			value = _value;
		}
	}

	protected List<Item>[] lists;

	protected List<Item> GetList(string key)
	{
		if (lists == null)
		{
			return null;
		}
		return lists[GetHashA(key)];
	}

	protected Item GetItem(List<Item> list, string key)
	{
		if (list == null)
		{
			return null;
		}
		int hashB = GetHashB(key);
		List<Item>.Enumerator enumerator = list.GetEnumerator();
		while (enumerator.MoveNext())
		{
			if (enumerator.Current.hash == hashB && enumerator.Current.key == key)
			{
				return enumerator.Current;
			}
		}
		return null;
	}

	protected void _Add(string key, object value)
	{
		if (!string.IsNullOrEmpty(key))
		{
			if (lists == null)
			{
				lists = new List<Item>[256];
			}
			int hashA = GetHashA(key);
			List<Item> list = lists[hashA];
			if (list == null)
			{
				list = new List<Item>();
				lists[hashA] = list;
			}
			list.Add(new Item(key, value));
		}
	}

	protected object _Get(string key)
	{
		if (string.IsNullOrEmpty(key))
		{
			return null;
		}
		List<Item> list = GetList(key);
		if (list == null)
		{
			return null;
		}
		return GetItem(list, key)?.value;
	}

	public void Remove(string key)
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

	public void Clear()
	{
		lists = null;
	}

	public static int GetHashA(string key)
	{
		int num = 0;
		int i = 0;
		for (int length = key.Length; i < length; i++)
		{
			num = ((num << 1) | ((num >> 31) & 1));
			num += key[i];
		}
		return num & 0xFF;
	}

	public static int GetHashB(string key)
	{
		int num = 0;
		int i = 0;
		for (int length = key.Length; i < length; i++)
		{
			int num2 = i & 0x1F;
			num = ((num << num2) | ((num >> 31 - num2) + key[i]));
		}
		return num;
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
