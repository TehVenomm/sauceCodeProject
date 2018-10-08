using UnityEngine;

public class ResourceLink : MonoBehaviour
{
	public Object[] objects;

	public T Get<T>(string name) where T : Object
	{
		if (objects != null && !string.IsNullOrEmpty(name))
		{
			int i = 0;
			for (int num = objects.Length; i < num; i++)
			{
				Object @object = objects[i];
				if (@object != (Object)null && @object is T && @object.name == name)
				{
					return @object as T;
				}
			}
		}
		return (T)null;
	}

	public T GetFirstObject<T>(string filter) where T : Object
	{
		if (objects == null)
		{
			return (T)null;
		}
		for (int i = 0; i < objects.Length; i++)
		{
			Object @object = objects[i];
			if (@object != (Object)null && (string.IsNullOrEmpty(filter) || @object.name.Contains(filter)) && @object is T)
			{
				return @object as T;
			}
		}
		return (T)null;
	}
}
