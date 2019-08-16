using UnityEngine;

public class ResourceLink : MonoBehaviour
{
	public Object[] objects;

	public ResourceLink()
		: this()
	{
	}

	public T Get<T>(string name) where T : Object
	{
		if (objects != null && !string.IsNullOrEmpty(name))
		{
			int i = 0;
			for (int num = objects.Length; i < num; i++)
			{
				Object val = objects[i];
				if (val != null && val is T && val.get_name() == name)
				{
					return val as T;
				}
			}
		}
		return (T)(object)null;
	}

	public T GetFirstObject<T>(string filter) where T : Object
	{
		if (objects == null)
		{
			return (T)(object)null;
		}
		for (int i = 0; i < objects.Length; i++)
		{
			Object val = objects[i];
			if (val != null && (string.IsNullOrEmpty(filter) || val.get_name().Contains(filter)) && val is T)
			{
				return val as T;
			}
		}
		return (T)(object)null;
	}
}
