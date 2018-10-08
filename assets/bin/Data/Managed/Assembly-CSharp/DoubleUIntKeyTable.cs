using System;
using System.Collections.Generic;

public class DoubleUIntKeyTable<T> : UIntKeyTable<UIntKeyTable<T>>
{
	public T Get(uint key1, uint key2)
	{
		UIntKeyTable<T> uIntKeyTable = Get(key1);
		return (uIntKeyTable == null) ? default(T) : uIntKeyTable.Get(key2);
	}

	public void Add(uint key1, uint key2, T value)
	{
		UIntKeyTable<T> uIntKeyTable = Get(key1);
		if (uIntKeyTable == null)
		{
			uIntKeyTable = new UIntKeyTable<T>(false);
			Add(key1, uIntKeyTable);
		}
		uIntKeyTable.Add(key2, value);
	}

	public unsafe void ForEachDoubleKeyValue(Action<uint, uint, T> a)
	{
		_003CForEachDoubleKeyValue_003Ec__AnonStorey52B _003CForEachDoubleKeyValue_003Ec__AnonStorey52B;
		ForEachKeyValue(new Action<uint, UIntKeyTable<uint>>((object)_003CForEachDoubleKeyValue_003Ec__AnonStorey52B, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	public unsafe override bool Equals(object obj)
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
		_003CEquals_003Ec__AnonStorey52D _003CEquals_003Ec__AnonStorey52D;
		ForEachDoubleKeyValue(new Action<uint, uint, uint>((object)_003CEquals_003Ec__AnonStorey52D, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		return isEqual;
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public override void TrimExcess()
	{
		if (lists != null)
		{
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
}
