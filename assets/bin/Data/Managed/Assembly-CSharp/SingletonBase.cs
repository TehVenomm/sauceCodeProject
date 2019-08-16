using System.Collections.Generic;

public abstract class SingletonBase
{
	public static List<object> instanceList;

	public static void AddInstance(object obj)
	{
		if (instanceList == null)
		{
			instanceList = new List<object>();
		}
		instanceList.Add(obj);
	}

	public static void RemoveAllInstance()
	{
		if (instanceList == null)
		{
			return;
		}
		while (instanceList.Count > 0)
		{
			if (instanceList[0] is SingletonBase)
			{
				(instanceList[0] as SingletonBase).Remove();
			}
			else
			{
				instanceList.RemoveAt(0);
			}
		}
		instanceList = null;
	}

	public virtual void Remove()
	{
	}
}
