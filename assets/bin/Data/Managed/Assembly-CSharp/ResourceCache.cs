// Decompiled with JetBrains decompiler
// Type: ResourceCache
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class ResourceCache
{
  public const int MAX_DELETE_PACKAGE = 32 /*0x20*/;
  public StringKeyTable<ResourceObject>[] objectCaches = new StringKeyTable<ResourceObject>[91];
  public HashSet<string> IgnoreCategorySpecifiedReleaseList = new HashSet<string>();
  public HashSet<string> PreloadedInGameResouces = new HashSet<string>();
  public List<ResourceObject> systemCaches = new List<ResourceObject>();
  public StringKeyTable<PackageObject> packageCaches = new StringKeyTable<PackageObject>();
  public StringKeyTable<PackageObject> systemPackageCaches = new StringKeyTable<PackageObject>();
  public StringKeyTable<Shader> shaderCaches = new StringKeyTable<Shader>();
  private List<string> shaderNames = new List<string>();
  public BetterList<PackageObject> deletePackageObjects = new BetterList<PackageObject>();
  private bool requestDeletePackageObjects;
  public BetterList<DelayUnloadAssetBundle> delayUnloadAssetBundles = new BetterList<DelayUnloadAssetBundle>();
  public Dictionary<int, string> m_dicSENames = new Dictionary<int, string>(100);
  private int requestUnloadUnusedAssetsFrame;

  public static bool CanUseCustomUnloder()
  {
    return FieldManager.IsValidInGame() && !MonoBehaviourSingleton<TransitionManager>.I.isChanging && !MonoBehaviourSingleton<TransitionManager>.I.isTransing;
  }

  public void RequestUnloadUnusedAssets() => this.requestUnloadUnusedAssetsFrame = Time.frameCount;

  public void ClearObjectCaches(bool clearPreloaded)
  {
    if (MonoBehaviourSingleton<InGameManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<InGameManager>.I.selfCacheObject, (Object) null))
      MonoBehaviourSingleton<InGameManager>.I.DestroySelfCache();
    if (this.IgnoreCategorySpecifiedReleaseList != null)
      this.IgnoreCategorySpecifiedReleaseList.Clear();
    if (clearPreloaded && this.PreloadedInGameResouces != null)
      this.PreloadedInGameResouces.Clear();
    if (this.objectCaches == null)
      return;
    if (clearPreloaded)
    {
      int index = 0;
      for (int length = this.objectCaches.Length; index < length; ++index)
      {
        if (this.objectCaches[index] != null)
        {
          this.objectCaches[index].ForEach((Action<ResourceObject>) (o => ResourceObject.Release(ref o)));
          this.objectCaches[index].Clear();
        }
      }
    }
    else
    {
      List<string> releasedObjNames = new List<string>();
      int index1 = 0;
      for (int length = this.objectCaches.Length; index1 < length; ++index1)
      {
        releasedObjNames.Clear();
        if (this.objectCaches[index1] != null)
        {
          this.objectCaches[index1].ForEach((Action<ResourceObject>) (o =>
          {
            if (this.PreloadedInGameResouces.Contains(o.name))
              return;
            releasedObjNames.Add(o.name);
            ResourceObject.Release(ref o);
          }));
          int index2 = 0;
          for (int count = releasedObjNames.Count; index2 < count; ++index2)
            this.objectCaches[index1].Remove(releasedObjNames[index2]);
        }
      }
    }
  }

  public void ClearObjectCaches(RESOURCE_CATEGORY[] categories)
  {
    if (this.objectCaches == null || categories == null)
      return;
    List<string> releasedObjNames = new List<string>();
    int index1 = 0;
    for (int length = categories.Length; index1 < length; ++index1)
    {
      int category = (int) categories[index1];
      releasedObjNames.Clear();
      if (this.objectCaches[category] != null)
      {
        this.objectCaches[category].ForEach((Action<ResourceObject>) (o =>
        {
          if (this.IgnoreCategorySpecifiedReleaseList.Contains(o.name))
            return;
          releasedObjNames.Add(o.name);
          ResourceObject.Release(ref o);
        }));
        int index2 = 0;
        for (int count = releasedObjNames.Count; index2 < count; ++index2)
          this.objectCaches[category].Remove(releasedObjNames[index2]);
      }
    }
  }

  public void ClearPackageCaches()
  {
    this.packageCaches.ForEach((Action<PackageObject>) (o => PackageObject.Release(ref o)));
    this.DeletePackageObjects();
    this.packageCaches.Clear();
  }

  public IEnumerator DoClearPackageCaches(System.Action callback = null)
  {
    if (this.packageCaches != null)
    {
      List<StringKeyTableBase.Item>[] lists = this.packageCaches.GetList();
      if (lists != null)
      {
        int i = 0;
        for (int n = lists.Length; i < n; ++i)
        {
          lists[i]?.ForEach((Action<StringKeyTableBase.Item>) (o =>
          {
            PackageObject packageObject = o.value as PackageObject;
            PackageObject.Release(ref packageObject);
          }));
          yield return (object) null;
        }
      }
      lists = (List<StringKeyTableBase.Item>[]) null;
    }
    yield return (object) this.DoDeletePackageObjects();
    this.packageCaches.Clear();
    callback();
  }

  public IEnumerator DoClearObjectCaches(bool clearPreloaded)
  {
    if (MonoBehaviourSingleton<InGameManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<InGameManager>.I.selfCacheObject, (Object) null))
      MonoBehaviourSingleton<InGameManager>.I.DestroySelfCache();
    if (this.IgnoreCategorySpecifiedReleaseList != null)
      this.IgnoreCategorySpecifiedReleaseList.Clear();
    if (clearPreloaded && this.PreloadedInGameResouces != null)
      this.PreloadedInGameResouces.Clear();
    int i;
    int n;
    if (this.objectCaches != null)
    {
      if (clearPreloaded)
      {
        i = 0;
        for (n = this.objectCaches.Length; i < n; ++i)
        {
          if (this.objectCaches[i] != null)
          {
            this.objectCaches[i].ForEach((Action<ResourceObject>) (o => ResourceObject.Release(ref o)));
            this.objectCaches[i].Clear();
            yield return (object) null;
          }
        }
      }
      else
      {
        List<string> releasedObjNames = new List<string>();
        n = 0;
        for (i = this.objectCaches.Length; n < i; ++n)
        {
          releasedObjNames.Clear();
          if (this.objectCaches[n] != null)
          {
            this.objectCaches[n].ForEach((Action<ResourceObject>) (o =>
            {
              if (this.PreloadedInGameResouces.Contains(o.name))
                return;
              releasedObjNames.Add(o.name);
              ResourceObject.Release(ref o);
            }));
            int index = 0;
            for (int count = releasedObjNames.Count; index < count; ++index)
              this.objectCaches[n].Remove(releasedObjNames[index]);
          }
          yield return (object) null;
        }
      }
    }
  }

  public void ClearSystemPackageCaches()
  {
    this.systemPackageCaches.ForEach((Action<PackageObject>) (o => PackageObject.Release(ref o)));
    this.systemPackageCaches.Clear();
  }

  public void AddIgnoreCategorySpecifiedReleaseList(List<string> objNames)
  {
    int index = 0;
    for (int count = objNames.Count; index < count; ++index)
    {
      if (!this.IgnoreCategorySpecifiedReleaseList.Contains(objNames[index]))
        this.IgnoreCategorySpecifiedReleaseList.Add(objNames[index]);
    }
  }

  public void AddPreloadResources(string objName)
  {
    if (this.PreloadedInGameResouces.Contains(objName))
      return;
    this.PreloadedInGameResouces.Add(objName);
  }

  public void AddSystemCaches(List<LoadObject> los)
  {
    los.ForEach((Action<LoadObject>) (lo =>
    {
      int index = 0;
      for (int length = lo.loadedObjects.Length; index < length; ++index)
        this.systemCaches.Add(ResourceObject.Get(RESOURCE_CATEGORY.MAX, lo.loadedObjects[index].obj.name, lo.loadedObjects[index].obj));
    }));
  }

  public void RemoveSystemCaches(List<LoadObject> los)
  {
    los?.ForEach((Action<LoadObject>) (lo =>
    {
      int i = 0;
      for (int length = lo.loadedObjects.Length; i < length; ++i)
      {
        int index = this.systemCaches.FindIndex((Predicate<ResourceObject>) (o => o.obj.name == lo.loadedObjects[i].obj.name));
        if (index >= 0)
          this.systemCaches.RemoveAt(index);
      }
    }));
  }

  public void MarkSystemPackage(string cahced_package_name)
  {
    PackageObject packageObject = this.packageCaches.Get(cahced_package_name);
    if (packageObject == null)
      return;
    this.packageCaches.Remove(cahced_package_name);
    if (this.systemPackageCaches.Get(cahced_package_name) != null)
      this.systemPackageCaches.Remove(cahced_package_name);
    this.systemPackageCaches.Add(cahced_package_name, packageObject);
  }

  public void CacheShadersFromPackage(string cahced_package_name)
  {
    PackageObject cachedPackage = this.GetCachedPackage(cahced_package_name);
    Debug.Log((object) "CACHE SHADER");
    if (cachedPackage == null)
      return;
    AssetBundle assetBundle = cachedPackage.obj as AssetBundle;
    if (!Object.op_Inequality((Object) assetBundle, (Object) null))
      return;
    Shader[] shaderArray = assetBundle.LoadAllAssets<Shader>();
    ShaderVariantCollection variantCollection = new ShaderVariantCollection();
    int index = 0;
    for (int length = shaderArray.Length; index < length; ++index)
    {
      if (!shaderArray[index].isSupported)
        Log.Error("no support shader : " + ((Object) shaderArray[index]).name);
      string name = ((Object) shaderArray[index]).name;
      this.shaderCaches.Add(name, shaderArray[index]);
      this.shaderNames.Add(name);
      if (name.StartsWith("EeL") || name.Contains("effect"))
        variantCollection.Add(new ShaderVariantCollection.ShaderVariant()
        {
          shader = shaderArray[index]
        });
    }
    if (variantCollection.isWarmedUp)
      return;
    variantCollection.WarmUp();
  }

  public PackageObject PopCachedPackage(string cahced_package_name)
  {
    PackageObject packageObject = this.packageCaches.Get(cahced_package_name);
    if (packageObject == null)
      return packageObject;
    this.packageCaches.Remove(cahced_package_name);
    return packageObject;
  }

  public PackageObject GetCachedPackage(string package_name)
  {
    PackageObject cachedPackage = this.packageCaches.Get(package_name) ?? this.systemPackageCaches.Get(package_name);
    if (cachedPackage == null)
    {
      int num = 0;
      for (int size = this.deletePackageObjects.size; num < size; ++num)
      {
        if (this.deletePackageObjects[num].name == package_name)
        {
          cachedPackage = this.deletePackageObjects[num];
          this.deletePackageObjects.RemoveAt(num);
          this.packageCaches.Add(MonoBehaviourSingleton<GoGameResourceManager>.I.GetBundleNameWithoutVariant(package_name), cachedPackage);
          break;
        }
      }
    }
    return cachedPackage;
  }

  public bool IsCached(string package_name) => this.GetCachedPackage(package_name) != null;

  public ResourceObject GetCachedResourceObject(RESOURCE_CATEGORY category, string resource_name)
  {
    if (this.objectCaches == null)
      return (ResourceObject) null;
    StringKeyTable<ResourceObject> objectCach = this.objectCaches[(int) category];
    return objectCach == null ? (ResourceObject) null : objectCach.Get(resource_name) ?? this.systemCaches.Find((Predicate<ResourceObject>) (o => o.obj.name == resource_name));
  }

  public ResourceObject[] GetCachedResourceObjects(
    RESOURCE_CATEGORY category,
    string[] resource_names)
  {
    if (this.objectCaches == null)
      return (ResourceObject[]) null;
    StringKeyTable<ResourceObject> objectCach = this.objectCaches[(int) category];
    if (objectCach == null)
      return (ResourceObject[]) null;
    ResourceObject[] cachedResourceObjects = new ResourceObject[resource_names.Length];
    int index = 0;
    for (int length = resource_names.Length; index < length; ++index)
    {
      string res_name = resource_names[index];
      cachedResourceObjects[index] = objectCach.Get(res_name);
      if (cachedResourceObjects[index] == null)
        cachedResourceObjects[index] = this.systemCaches.Find((Predicate<ResourceObject>) (o => o.obj.name == res_name));
    }
    return cachedResourceObjects;
  }

  public ResourceObject GetCachedResourceObject(
    RESOURCE_CATEGORY category,
    string package_name,
    string resource_name,
    bool inc_ref_count)
  {
    ResourceObject cachedResourceObject = this.GetCachedResourceObject(category, resource_name);
    if (cachedResourceObject == null && this.objectCaches != null)
    {
      PackageObject cachedPackage = this.GetCachedPackage(package_name);
      if (cachedPackage != null)
      {
        if (!ResourceManager.enableLoadDirect)
        {
          if (ResourceManager.isDownloadAssets)
          {
            if (cachedPackage.obj is AssetBundle)
              cachedResourceObject = ResourceObject.Get(category, resource_name, (cachedPackage.obj as AssetBundle).LoadAsset(resource_name));
          }
          else
          {
            int num = ResourceManager.enableLoadDirect ? 1 : 0;
            ResourceManager.enableLoadDirect = true;
            cachedResourceObject = ResourceObject.Get(category, resource_name, ResourceManager.LoadDirect(category, package_name, resource_name));
            ResourceManager.enableLoadDirect = num != 0;
          }
        }
        else
          cachedResourceObject = ResourceObject.Get(category, resource_name, ResourceManager.LoadDirect(category, package_name, resource_name));
      }
    }
    if (cachedResourceObject == null)
      cachedResourceObject = this.systemCaches.Find((Predicate<ResourceObject>) (o => o.obj.name == resource_name));
    return cachedResourceObject;
  }

  public Object GetCachedObject(RESOURCE_CATEGORY category, string resource_name)
  {
    return this.GetCachedResourceObject(category, resource_name)?.obj;
  }

  public Object GetCachedObject(
    RESOURCE_CATEGORY category,
    string package_name,
    string resource_name)
  {
    return this.GetCachedResourceObject(category, package_name, resource_name, false)?.obj;
  }

  public void ReleaseResourceObjects(ResourceObject[] resobjs)
  {
    if (resobjs == null)
      return;
    int index = 0;
    for (int length = resobjs.Length; index < length; ++index)
    {
      ResourceObject resobj = resobjs[index];
      if (resobj != null)
      {
        --resobj.refCount;
        if (resobj.refCount == 0)
        {
          resobj.obj = (Object) null;
          if (this.objectCaches[(int) resobj.category] != null && this.objectCaches[(int) resobj.category].Get(resobj.name) != null)
            this.objectCaches[(int) resobj.category].Remove(resobj.name);
          PackageObject package = resobj.package;
          if (package != null)
          {
            this.ReleasePackageObjects(package.linkPackages);
            this.ReleasePackageObject(ref package);
          }
          ResourceObject.Release(ref resobj);
          this.RequestUnloadUnusedAssets();
        }
      }
    }
  }

  public BetterList<PackageObject> GetDependencyPackages(List<ResourceManager.LoadRequest> requests)
  {
    if (requests == null || requests.Count == 0)
      return (BetterList<PackageObject>) null;
    BetterList<PackageObject> dependencyPackages = new BetterList<PackageObject>();
    int index = 0;
    for (int count = requests.Count; index < count; ++index)
    {
      PackageObject cachedPackage = this.GetCachedPackage(requests[index].packageName);
      if (cachedPackage != null)
      {
        ++cachedPackage.refCount;
        dependencyPackages.Add(cachedPackage);
      }
    }
    return dependencyPackages;
  }

  public void ReleasePackageObject(ref PackageObject pakobj)
  {
    if (pakobj == null)
      return;
    --pakobj.refCount;
    if (pakobj.refCount == 0)
    {
      this.packageCaches.Remove(pakobj.name);
      this.deletePackageObjects.Add(pakobj);
    }
    pakobj = (PackageObject) null;
  }

  public void ReleasePackageObjects(BetterList<PackageObject> objs)
  {
    if (objs == null)
      return;
    int i = 0;
    for (int size = objs.size; i < size; ++i)
    {
      PackageObject pakobj = objs[i];
      this.ReleasePackageObject(ref pakobj);
    }
    objs.Clear();
  }

  private void DeletePackageObjects()
  {
    if (MonoBehaviourSingleton<ResourceManager>.I.isLoading || InstantiateManager.isBusy)
    {
      this.requestDeletePackageObjects = true;
    }
    else
    {
      int i = 0;
      for (int size = this.deletePackageObjects.size; i < size; ++i)
      {
        PackageObject deletePackageObject = this.deletePackageObjects[i];
        if (deletePackageObject != null)
          PackageObject.Release(ref deletePackageObject);
      }
      this.deletePackageObjects.Clear();
      this.RequestUnloadUnusedAssets();
    }
  }

  private IEnumerator DoDeletePackageObjects()
  {
    if (MonoBehaviourSingleton<ResourceManager>.I.isLoading || InstantiateManager.isBusy)
    {
      this.requestDeletePackageObjects = true;
    }
    else
    {
      int i = 0;
      for (int n = this.deletePackageObjects.size; i < n; ++i)
      {
        PackageObject deletePackageObject = this.deletePackageObjects[i];
        if (deletePackageObject != null)
          PackageObject.Release(ref deletePackageObject);
        yield return (object) null;
      }
      this.deletePackageObjects.Clear();
      this.RequestUnloadUnusedAssets();
    }
  }

  public IEnumerator CheckShaderCacheOnMemory(MonoBehaviour mono)
  {
    List<string> delShader = new List<string>();
    for (int index = 0; index < this.shaderNames.Count; ++index)
    {
      if (Object.op_Equality((Object) this.shaderCaches.Get(this.shaderNames[index]), (Object) null))
        delShader.Add(this.shaderNames[index]);
    }
    if (delShader.Count > 10)
    {
      this.ClearShaderCaches();
      yield return (object) this.ReloadAllShader(mono);
    }
    else if (delShader.Count > 0)
    {
      for (int i = 0; i < delShader.Count; ++i)
        yield return (object) this.ReLoadShader(mono, delShader[i]);
    }
  }

  private IEnumerator ReLoadShader(MonoBehaviour mono, string shader_name)
  {
    LoadingQueue loadingQueue = new LoadingQueue(mono);
    LoadObject load_obj = loadingQueue.Load(RESOURCE_CATEGORY.SHADER, shader_name, (string[]) null, true);
    yield return (object) loadingQueue.Wait();
    Shader loadedObject = load_obj.loadedObject as Shader;
    this.shaderCaches.Remove(shader_name);
    this.shaderCaches.Add(shader_name, loadedObject);
  }

  private IEnumerator ReloadAllShader(MonoBehaviour mono)
  {
    LoadingQueue loadingQueue = new LoadingQueue(mono);
    ResourceManager.internalMode = false;
    loadingQueue.Load(RESOURCE_CATEGORY.SHADER, (string) null, (string[]) null, true);
    ResourceManager.internalMode = true;
    yield return (object) loadingQueue.Wait();
    MonoBehaviourSingleton<ResourceManager>.I.cache.MarkSystemPackage(RESOURCE_CATEGORY.SHADER.ToAssetBundleName());
    MonoBehaviourSingleton<ResourceManager>.I.cache.CacheShadersFromPackage(RESOURCE_CATEGORY.SHADER.ToAssetBundleName());
  }

  public int CheckShaderExistOnMemory()
  {
    int num = 0;
    if (this.shaderNames.Count == 0)
      return -1;
    for (int index = 0; index < this.shaderNames.Count; ++index)
    {
      if (Object.op_Equality((Object) this.shaderCaches.Get(this.shaderNames[index]), (Object) null))
        ++num;
    }
    return num;
  }

  public void ClearShaderCaches()
  {
    this.shaderCaches.Clear();
    this.shaderNames.Clear();
  }

  public void AddDelayUnloadAssetBundle(string name, ref AssetBundle asset_bundle)
  {
    if (Object.op_Equality((Object) asset_bundle, (Object) null))
      return;
    this.delayUnloadAssetBundles.Add(DelayUnloadAssetBundle.Get(name, asset_bundle));
    asset_bundle = (AssetBundle) null;
  }

  public AssetBundle PopDelayUnloadAssetBundle(string name)
  {
    int num = 0;
    for (int size = this.delayUnloadAssetBundles.size; num < size; ++num)
    {
      DelayUnloadAssetBundle unloadAssetBundle = this.delayUnloadAssetBundles[num];
      if (unloadAssetBundle.name == name)
      {
        this.delayUnloadAssetBundles.RemoveAt(num);
        AssetBundle assetBundle = unloadAssetBundle.assetBundle;
        unloadAssetBundle.assetBundle = (AssetBundle) null;
        DelayUnloadAssetBundle.Release(ref unloadAssetBundle);
        return assetBundle;
      }
    }
    return (AssetBundle) null;
  }

  public void ReleaseAllDelayUnloadAssetBundles()
  {
    int i = 0;
    for (int size = this.delayUnloadAssetBundles.size; i < size; ++i)
    {
      DelayUnloadAssetBundle unloadAssetBundle = this.delayUnloadAssetBundles[i];
      if (this.GetCachedPackage(unloadAssetBundle.name) != null)
        unloadAssetBundle.assetBundle = (AssetBundle) null;
      DelayUnloadAssetBundle.Release(ref unloadAssetBundle);
    }
    this.delayUnloadAssetBundles.Clear();
  }

  public void Update()
  {
    if (MonoBehaviourSingleton<ResourceManager>.I.loadingAssetCountFromAssetBundle == 0 && this.delayUnloadAssetBundles.size > 0)
      this.ReleaseAllDelayUnloadAssetBundles();
    if (MonoBehaviourSingleton<ResourceManager>.I.isLoading || InstantiateManager.isBusy)
      return;
    if (this.deletePackageObjects.size >= 32 /*0x20*/ || this.requestDeletePackageObjects)
    {
      this.DeletePackageObjects();
      this.requestDeletePackageObjects = false;
    }
    if (this.requestUnloadUnusedAssetsFrame == 0)
      return;
    int frameCount = Time.frameCount;
    if (frameCount - this.requestUnloadUnusedAssetsFrame < 1 && frameCount >= this.requestUnloadUnusedAssetsFrame)
      return;
    this.DeletePackageObjects();
    this.requestUnloadUnusedAssetsFrame = 0;
    if (frameCount - MonoBehaviourSingleton<AppMain>.I.frameExecutedUnloadUnusedAssets <= Application.targetFrameRate || ResourceCache.CanUseCustomUnloder())
      return;
    MonoBehaviourSingleton<AppMain>.I.UnloadUnusedAssets(false);
  }

  public string GetSEName(int se_id)
  {
    if (this.m_dicSENames == null)
      return ResourceName.CreateSEName(se_id);
    if (this.m_dicSENames.ContainsKey(se_id))
      return this.m_dicSENames[se_id];
    string seName = ResourceName.CreateSEName(se_id);
    this.m_dicSENames[se_id] = seName;
    return seName;
  }

  public void ClearSENameDictionary()
  {
    if (this.m_dicSENames == null)
      return;
    this.m_dicSENames.Clear();
  }
}
