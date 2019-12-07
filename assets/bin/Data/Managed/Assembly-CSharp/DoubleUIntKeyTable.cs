using System;
using System.Collections.Generic;

public class DoubleUIntKeyTable<T> : UIntKeyTable<UIntKeyTable<T>>
{
	public T Get(uint key1, uint key2)
	{
		UIntKeyTable<T> uIntKeyTable = Get(key1);
		if (uIntKeyTable == null)
		{
			return default(T);
		}
		return uIntKeyTable.Get(key2);
	}

	public void Add(uint key1, uint key2, T value)
	{
		UIntKeyTable<T> uIntKeyTable = Get(key1);
		if (uIntKeyTable == null)
		{
			uIntKeyTable = new UIntKeyTable<T>(useHashDivision: false);
			Add(key1, uIntKeyTable);
		}
		uIntKeyTable.Add(key2, value);
	}

	public void ForEachDoubleKeyValue(Action<uint, uint, T> a)
	{
		ForEachKeyValue(delegate(uint key1, UIntKeyTable<T> table2)
		{
			table2.ForEachKeyValue(delegate(uint key2, T data)
			{
				a(key1, key2, data);
			});
		});
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		DoubleUIntKeyTable<T> rhs = obj as DoubleUIntKeyTable<T>;
		if (rhs == null)
		{
			return false;
		}
		if (GetCount() != rhs.GetCount())
		{
			return false;
		}
		bool isEqual = true;
		ForEachDoubleKeyValue(delegate(uint key1, uint key2, T value1)
		{
			T val = rhs.Get(key1, key2);
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
					(item.value as UIntKeyTableBase)?.TrimExcess();
				});
				list.TrimExcess();
			}
		}
	}
}
