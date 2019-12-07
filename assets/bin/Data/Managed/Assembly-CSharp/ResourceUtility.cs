using UnityEngine;

public static class ResourceUtility
{
	public static Shader FindShader(string shader_name)
	{
		Shader shader = Shader.Find(shader_name);
		if (shader == null && MonoBehaviourSingleton<ResourceManager>.IsValid())
		{
			shader = MonoBehaviourSingleton<ResourceManager>.I.cache.shaderCaches.Get(shader_name);
		}
		return shader;
	}

	public static T Instantiate<T>(T obj) where T : Object
	{
		return Object.Instantiate(obj) as T;
	}

	public static Transform Realizes(Object obj, int layer = -1)
	{
		GameObject gameObject = obj as GameObject;
		if (gameObject == null)
		{
			return null;
		}
		GameObject gameObject2 = Instantiate(gameObject);
		string name = gameObject2.name;
		name = ResourceName.Normalize(name);
		name = (gameObject2.name = name.Replace("(Clone)", ""));
		Transform transform = gameObject2.transform;
		if (layer != -1)
		{
			Utility.SetLayerWithChildren(transform, layer);
		}
		return transform;
	}

	public static Transform Realizes(Object obj, Transform parent, int layer = -1)
	{
		if (obj == null)
		{
			Log.Error("ResourceUtility.Realizes : obj == null");
			return null;
		}
		Transform transform = Realizes(obj, layer);
		if (transform == null)
		{
			return null;
		}
		Utility.Attach(parent, transform);
		return transform;
	}
}
