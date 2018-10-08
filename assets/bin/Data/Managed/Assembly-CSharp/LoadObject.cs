using System.Collections;
using UnityEngine;

public class LoadObject
{
	public Object loadedObject;

	public ResourceObject[] loadedObjects;

	protected ResourceLoad resLoad;

	public bool isLoading
	{
		get;
		protected set;
	}

	public LoadObject()
	{
	}

	public LoadObject(MonoBehaviour mono_behaviour, RESOURCE_CATEGORY category, string resource_name, bool cache_package = false)
	{
		Load(mono_behaviour, category, resource_name, cache_package);
	}

	public LoadObject(MonoBehaviour mono_behaviour, RESOURCE_CATEGORY category, string package_name, string[] resource_names, bool cache_package = false)
	{
		Load(mono_behaviour, category, package_name, resource_names, cache_package);
	}

	public void Load(MonoBehaviour mono_behaviour, RESOURCE_CATEGORY category, string resource_name, bool cache_package)
	{
		isLoading = false;
		if (!string.IsNullOrEmpty(resource_name))
		{
			if ((Object)resLoad == (Object)null)
			{
				resLoad = ResourceLoad.GetResourceLoad(mono_behaviour, false);
			}
			ResourceObject cachedResourceObject = MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedResourceObject(category, resource_name);
			if (cachedResourceObject != null)
			{
				loadedObject = cachedResourceObject.obj;
				resLoad.SetReference(cachedResourceObject);
			}
			if (loadedObject == (Object)null)
			{
				isLoading = true;
				MonoBehaviourSingleton<ResourceManager>.I.Load(resLoad, category, resource_name, OnLoadComplate, OnLoadError, cache_package, null);
			}
		}
	}

	public void Load(MonoBehaviour mono_behaviour, RESOURCE_CATEGORY category, string package_name, string[] resource_names, bool cache_package = false)
	{
		isLoading = false;
		if (resource_names != null && resource_names.Length >= 1)
		{
			loadedObjects = MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedResourceObjects(category, resource_names);
			if (loadedObjects != null)
			{
				if (loadedObjects[0] != null)
				{
					loadedObject = loadedObjects[0].obj;
				}
				int i = 0;
				for (int num = loadedObjects.Length; i < num; i++)
				{
					if (loadedObjects[i] == null)
					{
						loadedObjects = null;
						break;
					}
				}
			}
		}
		if ((Object)resLoad == (Object)null)
		{
			resLoad = ResourceLoad.GetResourceLoad(mono_behaviour, false);
		}
		if (loadedObjects == null)
		{
			isLoading = true;
			MonoBehaviourSingleton<ResourceManager>.I.Load(resLoad, category, package_name, resource_names, OnLoadComplate, OnLoadError, cache_package, null);
		}
		else
		{
			resLoad.SetReference(loadedObjects);
		}
	}

	protected virtual void OnLoadComplate(ResourceManager.LoadRequest request, ResourceObject[] objs)
	{
		if (objs != null && objs.Length >= 1 && objs[0] != null)
		{
			loadedObject = objs[0].obj;
		}
		loadedObjects = objs;
		isLoading = false;
		resLoad.SetReference(loadedObjects);
		if (IsStock(request))
		{
			InstantiateManager.RequestStock(request.category, loadedObject, request.resourceNames[0], true);
		}
	}

	private bool IsStock(ResourceManager.LoadRequest request)
	{
		if (request.category == RESOURCE_CATEGORY.EFFECT_UI)
		{
			return true;
		}
		if (request.category == RESOURCE_CATEGORY.EFFECT_ACTION && request.resourceNames != null && !request.resourceNames[0].Contains("_bg_"))
		{
			return true;
		}
		return false;
	}

	private void OnLoadError(ResourceManager.LoadRequest request, ResourceManager.ERROR_CODE error_node)
	{
		isLoading = false;
	}

	private IEnumerator DoWait()
	{
		while (isLoading)
		{
			yield return (object)null;
		}
	}

	public Coroutine Wait(MonoBehaviour mono_behaviour)
	{
		return mono_behaviour.StartCoroutine(DoWait());
	}

	public virtual Transform Realizes(Transform parent = null, int layer = -1)
	{
		if ((Object)resLoad == (Object)null || loadedObject == (Object)null)
		{
			Log.Error("it is not loaded.");
			return null;
		}
		if (!(loadedObject is GameObject))
		{
			Log.Error("it is not GameObject.");
			return null;
		}
		return ResourceUtility.Realizes(loadedObject, parent, layer);
	}

	public virtual GameObject PopInstantiatedGameObject()
	{
		return null;
	}

	public virtual bool HasInstantiatedGameObject()
	{
		return false;
	}

	public void ReleaseAllResources()
	{
		if (MonoBehaviourSingleton<ResourceManager>.IsValid() && MonoBehaviourSingleton<ResourceManager>.I.cache != null)
		{
			if (loadedObjects != null)
			{
				MonoBehaviourSingleton<ResourceManager>.I.cache.ReleaseResourceObjects(loadedObjects);
				loadedObjects = null;
			}
			if ((Object)resLoad != (Object)null)
			{
				resLoad.ReleaseAllResources();
				resLoad = null;
			}
			loadedObject = null;
		}
	}
}
