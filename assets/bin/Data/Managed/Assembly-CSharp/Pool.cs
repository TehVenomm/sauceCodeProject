using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool<ABS_T> where ABS_T : Poolable
{
	private StringKeyTable<Queue<ABS_T>> poolablesOfType = new StringKeyTable<Queue<ABS_T>>();

	private string GetKey<T>()
	{
		return typeof(T).ToString();
	}

	private string GetKey(Type type)
	{
		return type.ToString();
	}

	public T Alloc<T>() where T : Poolable, ABS_T, new()
	{
		Queue<ABS_T> queue = this.poolablesOfType.Get(GetKey<T>());
		if (queue == null)
		{
			queue = new Queue<ABS_T>();
			this.poolablesOfType.Add(GetKey<T>(), queue);
		}
		T result = (T)null;
		if (queue.Count > 0)
		{
			result = (T)queue.Dequeue();
		}
		else
		{
			result = new T();
			result.OnAwake();
		}
		result.OnInit();
		return result;
	}

	public void Free(ABS_T poolable)
	{
		Queue<ABS_T> queue = poolablesOfType.Get(GetKey(poolable.GetType()));
		if (queue == null)
		{
			Debug.LogError("Pool: not alloc poolable. poolable=" + poolable);
		}
		else
		{
			poolable.OnFinal();
			queue.Enqueue(poolable);
		}
	}

	public void Clear()
	{
		poolablesOfType.ForEach(delegate(Queue<ABS_T> p)
		{
			p.Clear();
		});
	}
}
