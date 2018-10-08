using UnityEngine;

public static class ResourceUtility
{
	public static Shader FindShader(string shader_name)
	{
		Shader shader = Shader.Find(shader_name);
		if ((Object)shader == (Object)null && MonoBehaviourSingleton<ResourceManager>.IsValid())
		{
			shader = MonoBehaviourSingleton<ResourceManager>.I.cache.shaderCaches.Get(shader_name);
		}
		return shader;
	}

	public static T Instantiate<T>(T obj) where T : Object
	{
		Object @object = Object.Instantiate(obj);
		return @object as T;
	}

	public static Transform Realizes(Object obj, int layer = -1)
	{
		GameObject gameObject = obj as GameObject;
		if ((Object)gameObject == (Object)null)
		{
			return null;
		}
		GameObject gameObject2 = Instantiate(gameObject);
		string name = gameObject2.name;
		name = ResourceName.Normalize(name);
		name = (gameObject2.name = name.Replace("(Clone)", string.Empty));
		Transform transform = gameObject2.transform;
		if (layer != -1)
		{
			Utility.SetLayerWithChildren(transform, layer);
		}
		return transform;
	}

	public static Transform Realizes(Object obj, Transform parent, int layer = -1)
	{
		if (obj == (Object)null)
		{
			Log.Error("ResourceUtility.Realizes : obj == null");
			return null;
		}
		Transform transform = Realizes(obj, layer);
		if ((Object)transform == (Object)null)
		{
			return null;
		}
		Utility.Attach(parent, transform);
		return transform;
	}
}
