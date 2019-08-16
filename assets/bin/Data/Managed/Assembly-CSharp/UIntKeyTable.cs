using System;
using System.Collections.Generic;

public class UIntKeyTable<T> : UIntKeyTableBase
{
	public UIntKeyTable()
	{
		useHashDivision = true;
	}

	public UIntKeyTable(bool useHashDivision)
	{
		base.useHashDivision = useHashDivision;
	}

	public bool Add(uint key, T value)
	{
		return _Add(key, value);
	}

	public void AddRange(UIntKeyTable<T> table)
	{
		_AddRange(table);
	}

	public T Get(uint key)
	{
		return (T)_Get(key);
	}

	public void ForEach(Action<T> action)
	{
		if (lists != null)
		{
			int i = 0;
			for (int num = lists.Length; i < num; i++)
			{
				lists[i]?.ForEach(delegate(Item o)
				{
					action((T)o.value);
				});
			}
		}
	}

	public void ForEachAsc(Action<T> _action)
	{
		if (_action == null || lists == null || lists.Length < 1)
		{
			return;
		}
		List<Item> list = new List<Item>();
		int i = 0;
		for (int num = lists.Length; i < num; i++)
		{
			if (lists[i] != null)
			{
				list.AddRange(lists[i]);
			}
		}
		list.Sort((Item a, Item b) => (a.key < b.key) ? (-1) : ((a.key != b.key) ? 1 : 0));
		int j = 0;
		for (int count = list.Count; j < count; j++)
		{
			_action((T)list[j].value);
		}
	}

	public void ForEachDesc(Action<T> _action)
	{
		if (_action == null || lists == null || lists.Length < 1)
		{
			return;
		}
		List<Item> list = new List<Item>();
		int i = 0;
		for (int num = lists.Length; i < num; i++)
		{
			if (lists[i] != null)
			{
				list.AddRange(lists[i]);
			}
		}
		list.Sort((Item a, Item b) => (a.key < b.key) ? 1 : ((a.key != b.key) ? (-1) : 0));
		int j = 0;
		for (int count = list.Count; j < count; j++)
		{
			_action((T)list[j].value);
		}
	}

	public void ForEachKey(Action<uint> action)
	{
		if (lists != null)
		{
			int i = 0;
			for (int num = lists.Length; i < num; i++)
			{
				lists[i]?.ForEach(delegate(Item o)
				{
					action(o.key);
				});
			}
		}
	}

	public void ForEachKeyValue(Action<uint, T> action)
	{
		if (lists != null)
		{
			int i = 0;
			for (int num = lists.Length; i < num; i++)
			{
				lists[i]?.ForEach(delegate(Item o)
				{
					action(o.key, (T)o.value);
				});
			}
		}
	}

	public T Find(Predicate<T> match)
	{
		if (lists == null)
		{
			return default(T);
		}
		int i = 0;
		for (int num = lists.Length; i < num; i++)
		{
			List<Item> list = lists[i];
			if (list != null)
			{
				Item item = list.Find((Item o) => match((T)o.value));
				if (item != null)
				{
					return (T)item.value;
				}
			}
		}
		return default(T);
	}

	public int RemoveAll(Predicate<T> match)
	{
		if (lists == null)
		{
			return 0;
		}
		int num = 0;
		int i = 0;
		for (int num2 = lists.Length; i < num2; i++)
		{
			List<Item> list = lists[i];
			if (list != null)
			{
				num += list.RemoveAll((Item o) => match((T)o.value));
			}
		}
		return num;
	}

	public virtual int GetCount()
	{
		if (lists == null)
		{
			return 0;
		}
		int num = 0;
		int i = 0;
		for (int num2 = lists.Length; i < num2; i++)
		{
			List<Item> list = lists[i];
			if (list != null)
			{
				num += list.Count;
			}
		}
		return num;
	}

	protected virtual bool ReadCSV(CSVReader csv, T data, ref uint key)
	{
		return false;
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		UIntKeyTable<T> rhs = obj as UIntKeyTable<T>;
		if (rhs == null)
		{
			return false;
		}
		if (GetCount() != rhs.GetCount())
		{
			return false;
		}
		bool isEqual = true;
		ForEachKeyValue(delegate(uint key, T value1)
		{
			T val = rhs.Get(key);
			isEqual = (isEqual && value1.Equals(val));
		});
		return isEqual;
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}
}
