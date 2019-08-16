using UnityEngine;

public class LoadAndInstantiateObject : LoadObject
{
	private GameObject instantiatedObject;

	public LoadAndInstantiateObject(MonoBehaviour mono_behaviour, RESOURCE_CATEGORY category, string resource_name)
	{
		Load(mono_behaviour, category, resource_name, cache_package: false);
		if (MonoBehaviourSingleton<InstantiateManager>.IsValid() && !base.isLoading && loadedObject != null)
		{
			base.isLoading = true;
			InstantiateManager.Request(resLoad, loadedObject, OnInstantiate, is_inactivate_instantiated_object: true);
		}
	}

	protected override void OnLoadComplate(ResourceManager.LoadRequest request, ResourceObject[] objs)
	{
		if (!MonoBehaviourSingleton<InstantiateManager>.IsValid())
		{
			base.OnLoadComplate(request, objs);
		}
		else if (objs != null && objs.Length == 1 && objs[0] != null)
		{
			loadedObject = objs[0].obj;
			loadedObjects = objs;
			resLoad.SetReference(loadedObjects);
			InstantiateManager.Request(resLoad, objs[0].obj, OnInstantiate, is_inactivate_instantiated_object: true);
		}
		else
		{
			base.isLoading = false;
			Log.Warning("LoadAndInstantiateObject : not support.");
		}
	}

	private void OnInstantiate(InstantiateManager.InstantiateData data)
	{
		base.isLoading = false;
		instantiatedObject = (data.instantiatedObject as GameObject);
	}

	public override Transform Realizes(Transform parent = null, int layer = -1)
	{
		if (instantiatedObject == null)
		{
			return base.Realizes(parent, layer);
		}
		return InstantiateManager.Realizes(ref instantiatedObject, parent, layer);
	}

	public override GameObject PopInstantiatedGameObject()
	{
		GameObject result = instantiatedObject;
		instantiatedObject = null;
		return result;
	}

	public override bool HasInstantiatedGameObject()
	{
		return instantiatedObject != null;
	}
}
