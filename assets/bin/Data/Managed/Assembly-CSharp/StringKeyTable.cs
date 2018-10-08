using System;

public class StringKeyTable<T> : StringKeyTableBase
{
	public void Add(string key, T value)
	{
		_Add(key, value);
	}

	public T Get(string key)
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

	public void ForEachKeys(Action<string> action)
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

	public void ForEachKeyAndValue(Action<string, T> action)
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

	protected virtual bool ReadCSV(CSVReader csv, T data, ref string key)
	{
		return false;
	}
}
