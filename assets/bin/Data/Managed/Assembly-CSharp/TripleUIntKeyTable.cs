using System;
using System.Collections.Generic;

public class TripleUIntKeyTable<T> : UIntKeyTable<UIntKeyTable<UIntKeyTable<T>>>
{
	public T Get(uint key1, uint key2, uint key3)
	{
		UIntKeyTable<UIntKeyTable<T>> uIntKeyTable = Get(key1);
		if (uIntKeyTable != null)
		{
			UIntKeyTable<T> uIntKeyTable2 = uIntKeyTable.Get(key2);
			if (uIntKeyTable2 != null)
			{
				return uIntKeyTable2.Get(key3);
			}
		}
		return default(T);
	}

	public void Add(uint key1, uint key2, uint key3, T value)
	{
		UIntKeyTable<UIntKeyTable<T>> uIntKeyTable = Get(key1);
		if (uIntKeyTable == null)
		{
			uIntKeyTable = new UIntKeyTable<UIntKeyTable<T>>(useHashDivision: false);
			Add(key1, uIntKeyTable);
		}
		UIntKeyTable<T> uIntKeyTable2 = uIntKeyTable.Get(key2);
		if (uIntKeyTable2 == null)
		{
			uIntKeyTable2 = new UIntKeyTable<T>(useHashDivision: false);
			uIntKeyTable.Add(key2, uIntKeyTable2);
		}
		uIntKeyTable2.Add(key3, value);
	}

	public void ForEachTripleKeyValue(Action<uint, uint, uint, T> a)
	{
		ForEachKeyValue(delegate(uint key1, UIntKeyTable<UIntKeyTable<T>> table2)
		{
			table2.ForEachKeyValue(delegate(uint key2, UIntKeyTable<T> table3)
			{
				table3.ForEachKeyValue(delegate(uint key3, T data)
				{
					a(key1, key2, key3, data);
				});
			});
		});
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		TripleUIntKeyTable<T> rhs = obj as TripleUIntKeyTable<T>;
		if (rhs == null)
		{
			return false;
		}
		if (GetCount() != rhs.GetCount())
		{
			return false;
		}
		bool isEqual = true;
		ForEachTripleKeyValue(delegate(uint key1, uint key2, uint key3, T value1)
		{
			T val = rhs.Get(key1, key2, key3);
			isEqual = (isEqual && value1.Equals(val));
		});
		return isEqual;
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public override void TrimExcess()
	{
		if (lists == null)
		{
			return;
		}
		for (int i = 0; i < lists.Length; i++)
		{
			List<Item> list = lists[i];
			if (list != null)
			{
				list.ForEach(delegate(Item item)
				{
					List<Item> list2 = item.value as List<Item>;
					if (list2 != null)
					{
						list2.ForEach(delegate(Item item2)
						{
							(item2.value as UIntKeyTableBase)?.TrimExcess();
						});
						list2.TrimExcess();
					}
				});
				list.TrimExcess();
			}
		}
	}
}
