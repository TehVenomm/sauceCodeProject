using UnityEngine;

public static class ResourceUtility
{
	public static Shader FindShader(string shader_name)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		Shader val = Shader.Find(shader_name);
		if (val == null && MonoBehaviourSingleton<ResourceManager>.IsValid())
		{
			val = MonoBehaviourSingleton<ResourceManager>.I.cache.shaderCaches.Get(shader_name);
		}
		return val;
	}

	public static T Instantiate<T>(T obj) where T : Object
	{
		Object val = (object)Object.Instantiate<T>(obj);
		return val as T;
	}

	public static Transform Realizes(Object obj, int layer = -1)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		GameObject val = obj as GameObject;
		if (val == null)
		{
			return null;
		}
		GameObject val2 = ResourceUtility.Instantiate<GameObject>(val);
		string name = val2.get_name();
		name = ResourceName.Normalize(name);
		name = name.Replace("(Clone)", string.Empty);
		val2.set_name(name);
		Transform val3 = val2.get_transform();
		if (layer != -1)
		{
			Utility.SetLayerWithChildren(val3, layer);
		}
		return val3;
	}

	public static Transform Realizes(Object obj, Transform parent, int layer = -1)
	{
		if (obj == null)
		{
			Log.Error("ResourceUtility.Realizes : obj == null");
			return null;
		}
		Transform val = Realizes(obj, layer);
		if (val == null)
		{
			return null;
		}
		Utility.Attach(parent, val);
		return val;
	}
}
