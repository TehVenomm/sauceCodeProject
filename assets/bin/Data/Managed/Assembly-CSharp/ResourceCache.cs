using System.Collections.Generic;
using UnityEngine;

public class ResourceCache
{
	public const int MAX_DELETE_PACKAGE = 32;

	public StringKeyTable<ResourceObject>[] objectCaches = new StringKeyTable<ResourceObject>[86];

	public HashSet<string> IgnoreCategorySpecifiedReleaseList = new HashSet<string>();

	public HashSet<string> PreloadedInGameResouces = new HashSet<string>();

	public List<ResourceObject> systemCaches = new List<ResourceObject>();

	public StringKeyTable<PackageObject> packageCaches = new StringKeyTable<PackageObject>();

	public StringKeyTable<PackageObject> systemPackageCaches = new StringKeyTable<PackageObject>();

	public StringKeyTable<Shader> shaderCaches = new StringKeyTable<Shader>();

	public BetterList<PackageObject> deletePackageObjects = new BetterList<PackageObject>();

	private bool requestDeletePackageObjects;

	public BetterList<DelayUnloadAssetBundle> delayUnloadAssetBundles = new BetterList<DelayUnloadAssetBundle>();

	public Dictionary<int, string> m_dicSENames = new Dictionary<int, string>(100);

	private int requestUnloadUnusedAssetsFrame;

	public static bool CanUseCustomUnloder()
	{
		return FieldManager.IsValidInGame() && !MonoBehaviourSingleton<TransitionManager>.I.isChanging && !MonoBehaviourSingleton<TransitionManager>.I.isTransing;
	}

	public void RequestUnloadUnusedAssets()
	{
		requestUnloadUnusedAssetsFrame = Time.frameCount;
	}

	public void ClearObjectCaches(bool clearPreloaded)
	{
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && (Object)MonoBehaviourSingleton<InGameManager>.I.selfCacheObject != (Object)null)
		{
			MonoBehaviourSingleton<InGameManager>.I.DestroySelfCache();
		}
		if (IgnoreCategorySpecifiedReleaseList != null)
		{
			IgnoreCategorySpecifiedReleaseList.Clear();
		}
		if (clearPreloaded && PreloadedInGameResouces != null)
		{
			PreloadedInGameResouces.Clear();
		}
		if (objectCaches != null)
		{
			if (clearPreloaded)
			{
				int i = 0;
				for (int num = objectCaches.Length; i < num; i++)
				{
					if (objectCaches[i] != null)
					{
						objectCaches[i].ForEach(delegate(ResourceObject o)
						{
							ResourceObject.Release(ref o);
						});
						objectCaches[i].Clear();
					}
				}
			}
			else
			{
				List<string> releasedObjNames = new List<string>();
				int j = 0;
				for (int num2 = objectCaches.Length; j < num2; j++)
				{
					releasedObjNames.Clear();
					if (objectCaches[j] != null)
					{
						objectCaches[j].ForEach(delegate(ResourceObject o)
						{
							if (!PreloadedInGameResouces.Contains(o.name))
							{
								releasedObjNames.Add(o.name);
								ResourceObject.Release(ref o);
							}
						});
						int k = 0;
						for (int count = releasedObjNames.Count; k < count; k++)
						{
							objectCaches[j].Remove(releasedObjNames[k]);
						}
					}
				}
			}
		}
	}

	public void ClearObjectCaches(RESOURCE_CATEGORY[] categories)
	{
		if (objectCaches != null && categories != null)
		{
			List<string> releasedObjNames = new List<string>();
			int i = 0;
			for (int num = categories.Length; i < num; i++)
			{
				int num2 = (int)categories[i];
				releasedObjNames.Clear();
				if (objectCaches[num2] != null)
				{
					objectCaches[num2].ForEach(delegate(ResourceObject o)
					{
						if (!IgnoreCategorySpecifiedReleaseList.Contains(o.name))
						{
							releasedObjNames.Add(o.name);
							ResourceObject.Release(ref o);
						}
					});
					int j = 0;
					for (int count = releasedObjNames.Count; j < count; j++)
					{
						objectCaches[num2].Remove(releasedObjNames[j]);
					}
				}
			}
		}
	}

	public void ClearPackageCaches()
	{
		packageCaches.ForEach(delegate(PackageObject o)
		{
			PackageObject.Release(ref o);
		});
		DeletePackageObjects();
		packageCaches.Clear();
	}

	public void ClearSystemPackageCaches()
	{
		systemPackageCaches.ForEach(delegate(PackageObject o)
		{
			PackageObject.Release(ref o);
		});
		systemPackageCaches.Clear();
	}

	public void AddIgnoreCategorySpecifiedReleaseList(List<string> objNames)
	{
		int i = 0;
		for (int count = objNames.Count; i < count; i++)
		{
			if (!IgnoreCategorySpecifiedReleaseList.Contains(objNames[i]))
			{
				IgnoreCategorySpecifiedReleaseList.Add(objNames[i]);
			}
		}
	}

	public void AddPreloadResources(string objName)
	{
		if (!PreloadedInGameResouces.Contains(objName))
		{
			PreloadedInGameResouces.Add(objName);
		}
	}

	public void AddSystemCaches(List<LoadObject> los)
	{
		los.ForEach(delegate(LoadObject lo)
		{
			int i = 0;
			for (int num = lo.loadedObjects.Length; i < num; i++)
			{
				systemCaches.Add(ResourceObject.Get(RESOURCE_CATEGORY.MAX, lo.loadedObjects[i].obj.name, lo.loadedObjects[i].obj));
			}
		});
	}

	public void RemoveSystemCaches(List<LoadObject> los)
	{
		los?.ForEach(delegate(LoadObject lo)
		{
			ResourceCache resourceCache = this;
			int i = 0;
			for (int num = lo.loadedObjects.Length; i < num; i++)
			{
				int num2 = systemCaches.FindIndex((ResourceObject o) => o.obj.name == lo.loadedObjects[i].obj.name);
				if (num2 >= 0)
				{
					systemCaches.RemoveAt(num2);
				}
			}
		});
	}

	public void MarkSystemPackage(string cahced_package_name)
	{
		PackageObject packageObject = packageCaches.Get(cahced_package_name);
		if (packageObject != null)
		{
			packageCaches.Remove(cahced_package_name);
			systemPackageCaches.Add(cahced_package_name, packageObject);
		}
	}

	public void CacheShadersFromPackage(string cahced_package_name)
	{
		PackageObject cachedPackage = GetCachedPackage(cahced_package_name);
		if (cachedPackage != null)
		{
			AssetBundle assetBundle = cachedPackage.obj as AssetBundle;
			if ((Object)assetBundle != (Object)null)
			{
				Shader[] array = assetBundle.LoadAllAssets<Shader>();
				ShaderVariantCollection shaderVariantCollection = new ShaderVariantCollection();
				int i = 0;
				for (int num = array.Length; i < num; i++)
				{
					if (!array[i].isSupported)
					{
						Log.Error("no support shader : " + array[i].name);
					}
					string name = array[i].name;
					shaderCaches.Add(name, array[i]);
					if (name.StartsWith("EeL") || name.Contains("effect"))
					{
						ShaderVariantCollection.ShaderVariant variant = default(ShaderVariantCollection.ShaderVariant);
						variant.shader = array[i];
						shaderVariantCollection.Add(variant);
					}
				}
				if (!shaderVariantCollection.isWarmedUp)
				{
					shaderVariantCollection.WarmUp();
				}
			}
		}
	}

	public PackageObject PopCachedPackage(string cahced_package_name)
	{
		PackageObject packageObject = packageCaches.Get(cahced_package_name);
		if (packageObject != null)
		{
			packageCaches.Remove(cahced_package_name);
		}
		return packageObject;
	}

	public PackageObject GetCachedPackage(string package_name)
	{
		PackageObject packageObject = packageCaches.Get(package_name);
		if (packageObject == null)
		{
			packageObject = systemPackageCaches.Get(package_name);
		}
		if (packageObject == null)
		{
			int i = 0;
			for (int size = deletePackageObjects.size; i < size; i++)
			{
				if (deletePackageObjects[i].name == package_name)
				{
					packageObject = deletePackageObjects[i];
					deletePackageObjects.RemoveAt(i);
					packageCaches.Add(package_name, packageObject);
					break;
				}
			}
		}
		return packageObject;
	}

	public bool IsCached(string package_name)
	{
		PackageObject cachedPackage = GetCachedPackage(package_name);
		return cachedPackage != null;
	}

	public ResourceObject GetCachedResourceObject(RESOURCE_CATEGORY category, string resource_name)
	{
		if (objectCaches == null)
		{
			return null;
		}
		StringKeyTable<ResourceObject> stringKeyTable = objectCaches[(int)category];
		if (stringKeyTable == null)
		{
			return null;
		}
		ResourceObject resourceObject = stringKeyTable.Get(resource_name);
		if (resourceObject == null)
		{
			resourceObject = systemCaches.Find((ResourceObject o) => o.obj.name == resource_name);
		}
		return resourceObject;
	}

	public ResourceObject[] GetCachedResourceObjects(RESOURCE_CATEGORY category, string[] resource_names)
	{
		if (objectCaches == null)
		{
			return null;
		}
		StringKeyTable<ResourceObject> stringKeyTable = objectCaches[(int)category];
		if (stringKeyTable == null)
		{
			return null;
		}
		ResourceObject[] array = new ResourceObject[resource_names.Length];
		int i = 0;
		for (int num = resource_names.Length; i < num; i++)
		{
			string res_name = resource_names[i];
			array[i] = stringKeyTable.Get(res_name);
			if (array[i] == null)
			{
				array[i] = systemCaches.Find((ResourceObject o) => o.obj.name == res_name);
			}
		}
		return array;
	}

	public ResourceObject GetCachedResourceObject(RESOURCE_CATEGORY category, string package_name, string resource_name, bool inc_ref_count)
	{
		ResourceObject resourceObject = GetCachedResourceObject(category, resource_name);
		if (resourceObject == null && objectCaches != null)
		{
			PackageObject cachedPackage = GetCachedPackage(package_name);
			if (cachedPackage != null)
			{
				if (!ResourceManager.enableLoadDirect)
				{
					if (ResourceManager.isDownloadAssets)
					{
						if (cachedPackage.obj is AssetBundle)
						{
							resourceObject = ResourceObject.Get(category, resource_name, (cachedPackage.obj as AssetBundle).LoadAsset(resource_name));
						}
					}
					else
					{
						bool enableLoadDirect = ResourceManager.enableLoadDirect;
						ResourceManager.enableLoadDirect = true;
						resourceObject = ResourceObject.Get(category, resource_name, ResourceManager.LoadDirect(category, package_name, resource_name));
						ResourceManager.enableLoadDirect = enableLoadDirect;
					}
				}
				else
				{
					resourceObject = ResourceObject.Get(category, resource_name, ResourceManager.LoadDirect(category, package_name, resource_name));
				}
			}
		}
		if (resourceObject == null)
		{
			resourceObject = systemCaches.Find((ResourceObject o) => o.obj.name == resource_name);
		}
		return resourceObject;
	}

	public Object GetCachedObject(RESOURCE_CATEGORY category, string resource_name)
	{
		return GetCachedResourceObject(category, resource_name)?.obj;
	}

	public Object GetCachedObject(RESOURCE_CATEGORY category, string package_name, string resource_name)
	{
		return GetCachedResourceObject(category, package_name, resource_name, false)?.obj;
	}

	public void ReleaseResourceObjects(ResourceObject[] resobjs)
	{
		if (resobjs != null)
		{
			int i = 0;
			for (int num = resobjs.Length; i < num; i++)
			{
				ResourceObject resobj = resobjs[i];
				if (resobj != null)
				{
					resobj.refCount--;
					if (resobj.refCount == 0)
					{
						resobj.obj = null;
						if (objectCaches[(int)resobj.category] != null && objectCaches[(int)resobj.category].Get(resobj.name) != null)
						{
							objectCaches[(int)resobj.category].Remove(resobj.name);
						}
						PackageObject pakobj = resobj.package;
						if (pakobj != null)
						{
							ReleasePackageObjects(pakobj.linkPackages);
							ReleasePackageObject(ref pakobj);
						}
						ResourceObject.Release(ref resobj);
						RequestUnloadUnusedAssets();
					}
				}
			}
		}
	}

	public BetterList<PackageObject> GetDependencyPackages(List<ResourceManager.LoadRequest> requests)
	{
		if (requests == null || requests.Count == 0)
		{
			return null;
		}
		BetterList<PackageObject> betterList = new BetterList<PackageObject>();
		int i = 0;
		for (int count = requests.Count; i < count; i++)
		{
			PackageObject cachedPackage = GetCachedPackage(requests[i].packageName);
			if (cachedPackage != null)
			{
				cachedPackage.refCount++;
				betterList.Add(cachedPackage);
			}
		}
		return betterList;
	}

	public void ReleasePackageObject(ref PackageObject pakobj)
	{
		if (pakobj != null)
		{
			pakobj.refCount--;
			if (pakobj.refCount == 0)
			{
				packageCaches.Remove(pakobj.name);
				deletePackageObjects.Add(pakobj);
			}
			pakobj = null;
		}
	}

	public void ReleasePackageObjects(BetterList<PackageObject> objs)
	{
		if (objs != null)
		{
			int i = 0;
			for (int size = objs.size; i < size; i++)
			{
				PackageObject pakobj = objs[i];
				ReleasePackageObject(ref pakobj);
			}
			objs.Clear();
		}
	}

	private void DeletePackageObjects()
	{
		if (MonoBehaviourSingleton<ResourceManager>.I.isLoading || InstantiateManager.isBusy)
		{
			requestDeletePackageObjects = true;
		}
		else
		{
			int i = 0;
			for (int size = deletePackageObjects.size; i < size; i++)
			{
				PackageObject obj = deletePackageObjects[i];
				if (obj != null)
				{
					PackageObject.Release(ref obj);
				}
			}
			deletePackageObjects.Clear();
			RequestUnloadUnusedAssets();
		}
	}

	public void AddDelayUnloadAssetBundle(string name, ref AssetBundle asset_bundle)
	{
		if (!((Object)asset_bundle == (Object)null))
		{
			delayUnloadAssetBundles.Add(DelayUnloadAssetBundle.Get(name, asset_bundle));
			asset_bundle = null;
		}
	}

	public AssetBundle PopDelayUnloadAssetBundle(string name)
	{
		int i = 0;
		for (int size = delayUnloadAssetBundles.size; i < size; i++)
		{
			DelayUnloadAssetBundle obj = delayUnloadAssetBundles[i];
			if (obj.name == name)
			{
				delayUnloadAssetBundles.RemoveAt(i);
				AssetBundle assetBundle = obj.assetBundle;
				obj.assetBundle = null;
				DelayUnloadAssetBundle.Release(ref obj);
				return assetBundle;
			}
		}
		return null;
	}

	public void ReleaseAllDelayUnloadAssetBundles()
	{
		int i = 0;
		for (int size = delayUnloadAssetBundles.size; i < size; i++)
		{
			DelayUnloadAssetBundle obj = delayUnloadAssetBundles[i];
			if (GetCachedPackage(obj.name) != null)
			{
				obj.assetBundle = null;
			}
			DelayUnloadAssetBundle.Release(ref obj);
		}
		delayUnloadAssetBundles.Clear();
	}

	public void Update()
	{
		if (MonoBehaviourSingleton<ResourceManager>.I.loadingAssetCountFromAssetBundle == 0 && delayUnloadAssetBundles.size > 0)
		{
			ReleaseAllDelayUnloadAssetBundles();
		}
		if (!MonoBehaviourSingleton<ResourceManager>.I.isLoading && !InstantiateManager.isBusy)
		{
			if (deletePackageObjects.size >= 32 || requestDeletePackageObjects)
			{
				DeletePackageObjects();
				requestDeletePackageObjects = false;
			}
			if (requestUnloadUnusedAssetsFrame != 0)
			{
				int frameCount = Time.frameCount;
				if (frameCount - requestUnloadUnusedAssetsFrame >= 1 || frameCount < requestUnloadUnusedAssetsFrame)
				{
					DeletePackageObjects();
					requestUnloadUnusedAssetsFrame = 0;
					if (frameCount - MonoBehaviourSingleton<AppMain>.I.frameExecutedUnloadUnusedAssets > Application.targetFrameRate && !CanUseCustomUnloder())
					{
						MonoBehaviourSingleton<AppMain>.I.UnloadUnusedAssets(false);
					}
				}
			}
		}
	}

	public string GetSEName(int se_id)
	{
		if (m_dicSENames == null)
		{
			return ResourceName.CreateSEName(se_id);
		}
		if (m_dicSENames.ContainsKey(se_id))
		{
			return m_dicSENames[se_id];
		}
		string text = ResourceName.CreateSEName(se_id);
		m_dicSENames[se_id] = text;
		return text;
	}

	public void ClearSENameDictionary()
	{
		if (m_dicSENames != null)
		{
			m_dicSENames.Clear();
		}
	}
}
