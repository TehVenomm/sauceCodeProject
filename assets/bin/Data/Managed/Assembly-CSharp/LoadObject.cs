// Decompiled with JetBrains decompiler
// Type: LoadObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class LoadObject
{
  public Object loadedObject;
  public ResourceObject[] loadedObjects;
  protected ResourceLoad resLoad;

  public bool isLoading { get; protected set; }

  public LoadObject()
  {
  }

  public LoadObject(
    MonoBehaviour mono_behaviour,
    RESOURCE_CATEGORY category,
    string resource_name,
    bool cache_package = false,
    bool unload_bundle = false)
  {
    this.QuickLoad(mono_behaviour, category, resource_name, cache_package, unload_bundle);
  }

  public LoadObject(
    bool isEventAsset,
    MonoBehaviour mono_behaviour,
    RESOURCE_CATEGORY category,
    string resource_name,
    bool cache_package = false)
  {
    this.QuickLoad(isEventAsset, mono_behaviour, category, resource_name, cache_package);
  }

  public LoadObject(
    MonoBehaviour mono_behaviour,
    RESOURCE_CATEGORY category,
    string package_name,
    string[] resource_names,
    bool cache_package = false)
  {
    this.QuickLoad(mono_behaviour, category, package_name, resource_names, cache_package);
  }

  public void Load(
    bool isEventAsset,
    MonoBehaviour mono_behaviour,
    RESOURCE_CATEGORY category,
    string resource_name,
    bool cache_package)
  {
    this.isLoading = false;
    if (string.IsNullOrEmpty(resource_name))
      return;
    if (Object.op_Equality((Object) this.resLoad, (Object) null))
      this.resLoad = ResourceLoad.GetResourceLoad(mono_behaviour);
    ResourceObject cachedResourceObject = MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedResourceObject(category, resource_name);
    if (cachedResourceObject != null)
    {
      this.loadedObject = cachedResourceObject.obj;
      this.resLoad.SetReference(cachedResourceObject);
    }
    if (!Object.op_Equality(this.loadedObject, (Object) null))
      return;
    this.isLoading = true;
    MonoBehaviourSingleton<ResourceManager>.I.Load(isEventAsset, (object) this.resLoad, category, resource_name, new ResourceManager.LoadComplateDelegate(this.OnLoadComplate), new ResourceManager.LoadErrorDelegate(this.OnLoadError), cache_package);
  }

  public void Load(
    MonoBehaviour mono_behaviour,
    RESOURCE_CATEGORY category,
    string resource_name,
    bool cache_package)
  {
    this.isLoading = false;
    if (string.IsNullOrEmpty(resource_name))
      return;
    if (Object.op_Equality((Object) this.resLoad, (Object) null))
      this.resLoad = ResourceLoad.GetResourceLoad(mono_behaviour);
    ResourceObject cachedResourceObject = MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedResourceObject(category, resource_name);
    if (cachedResourceObject != null)
    {
      this.loadedObject = cachedResourceObject.obj;
      this.resLoad.SetReference(cachedResourceObject);
    }
    if (!Object.op_Equality(this.loadedObject, (Object) null))
      return;
    this.isLoading = true;
    MonoBehaviourSingleton<ResourceManager>.I.Load((object) this.resLoad, category, resource_name, new ResourceManager.LoadComplateDelegate(this.OnLoadComplate), new ResourceManager.LoadErrorDelegate(this.OnLoadError), cache_package);
  }

  public void Load(
    MonoBehaviour mono_behaviour,
    RESOURCE_CATEGORY category,
    string package_name,
    string[] resource_names,
    bool cache_package = false)
  {
    this.isLoading = false;
    if (resource_names != null && resource_names.Length >= 1)
    {
      this.loadedObjects = MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedResourceObjects(category, resource_names);
      if (this.loadedObjects != null)
      {
        if (this.loadedObjects[0] != null)
          this.loadedObject = this.loadedObjects[0].obj;
        int index = 0;
        for (int length = this.loadedObjects.Length; index < length; ++index)
        {
          if (this.loadedObjects[index] == null)
          {
            this.loadedObjects = (ResourceObject[]) null;
            break;
          }
        }
      }
    }
    if (Object.op_Equality((Object) this.resLoad, (Object) null))
      this.resLoad = ResourceLoad.GetResourceLoad(mono_behaviour);
    if (this.loadedObjects == null)
    {
      this.isLoading = true;
      MonoBehaviourSingleton<ResourceManager>.I.Load((object) this.resLoad, category, package_name, resource_names, new ResourceManager.LoadComplateDelegate(this.OnLoadComplate), new ResourceManager.LoadErrorDelegate(this.OnLoadError), cache_package);
    }
    else
      this.resLoad.SetReference(this.loadedObjects);
  }

  public void QuickLoad(
    bool isEventAsset,
    MonoBehaviour mono_behaviour,
    RESOURCE_CATEGORY category,
    string resource_name,
    bool cache_package,
    bool unload_asset = false)
  {
    this.isLoading = false;
    if (string.IsNullOrEmpty(resource_name))
      return;
    if (Object.op_Equality((Object) this.resLoad, (Object) null))
      this.resLoad = ResourceLoad.GetResourceLoad(mono_behaviour);
    ResourceObject cachedResourceObject = MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedResourceObject(category, resource_name);
    if (cachedResourceObject != null)
    {
      this.loadedObject = cachedResourceObject.obj;
      this.resLoad.SetReference(cachedResourceObject);
    }
    if (!Object.op_Equality(this.loadedObject, (Object) null))
      return;
    this.isLoading = true;
    MonoBehaviourSingleton<ResourceManager>.I.LoadAssetBundle(isEventAsset, (object) this.resLoad, category, resource_name, new ResourceManager.LoadComplateDelegate(this.OnLoadComplate), new ResourceManager.LoadErrorDelegate(this.OnLoadError), cache_package, unload_asset);
  }

  public void QuickLoad(
    MonoBehaviour mono_behaviour,
    RESOURCE_CATEGORY category,
    string resource_name,
    bool cache_package,
    bool unload_asset = false)
  {
    this.isLoading = false;
    if (string.IsNullOrEmpty(resource_name))
      return;
    if (Object.op_Equality((Object) this.resLoad, (Object) null))
      this.resLoad = ResourceLoad.GetResourceLoad(mono_behaviour);
    ResourceObject cachedResourceObject = MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedResourceObject(category, resource_name);
    if (cachedResourceObject != null)
    {
      this.loadedObject = cachedResourceObject.obj;
      this.resLoad.SetReference(cachedResourceObject);
    }
    if (!Object.op_Equality(this.loadedObject, (Object) null))
      return;
    this.isLoading = true;
    MonoBehaviourSingleton<ResourceManager>.I.LoadAssetBundle((object) this.resLoad, category, resource_name, new ResourceManager.LoadComplateDelegate(this.OnLoadComplate), new ResourceManager.LoadErrorDelegate(this.OnLoadError), cache_package, unload_asset);
  }

  public void QuickLoad(
    MonoBehaviour mono_behaviour,
    RESOURCE_CATEGORY category,
    string package_name,
    string[] resource_names,
    bool cache_package = false,
    bool unload_asset = false)
  {
    this.isLoading = false;
    if (resource_names != null && resource_names.Length >= 1)
    {
      this.loadedObjects = MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedResourceObjects(category, resource_names);
      if (this.loadedObjects != null)
      {
        if (this.loadedObjects[0] != null)
          this.loadedObject = this.loadedObjects[0].obj;
        int index = 0;
        for (int length = this.loadedObjects.Length; index < length; ++index)
        {
          if (this.loadedObjects[index] == null)
          {
            this.loadedObjects = (ResourceObject[]) null;
            break;
          }
        }
      }
    }
    if (Object.op_Equality((Object) this.resLoad, (Object) null))
      this.resLoad = ResourceLoad.GetResourceLoad(mono_behaviour);
    if (this.loadedObjects == null)
    {
      this.isLoading = true;
      MonoBehaviourSingleton<ResourceManager>.I.LoadAssetBundle((object) this.resLoad, category, package_name, resource_names, new ResourceManager.LoadComplateDelegate(this.OnLoadComplate), new ResourceManager.LoadErrorDelegate(this.OnLoadError), cache_package, (object) unload_asset);
    }
    else
      this.resLoad.SetReference(this.loadedObjects);
  }

  protected virtual void OnLoadComplate(ResourceManager.LoadRequest request, ResourceObject[] objs)
  {
    if (objs != null && objs.Length >= 1 && objs[0] != null)
      this.loadedObject = objs[0].obj;
    this.loadedObjects = objs;
    this.isLoading = false;
    this.resLoad.SetReference(this.loadedObjects);
    if (!this.IsStock(request))
      return;
    InstantiateManager.RequestStock(request.category, this.loadedObject, request.resourceNames[0], true);
  }

  private bool IsStock(ResourceManager.LoadRequest request)
  {
    return request.category == RESOURCE_CATEGORY.EFFECT_UI || request.category == RESOURCE_CATEGORY.EFFECT_ACTION && request.resourceNames != null && !request.resourceNames[0].Contains("_bg_");
  }

  private void OnLoadError(
    ResourceManager.LoadRequest request,
    ResourceManager.ERROR_CODE error_node)
  {
    this.isLoading = false;
  }

  private IEnumerator DoWait()
  {
    while (this.isLoading)
      yield return (object) null;
  }

  public Coroutine Wait(MonoBehaviour mono_behaviour)
  {
    return mono_behaviour.StartCoroutine(this.DoWait());
  }

  public virtual Transform Realizes(Transform parent = null, int layer = -1)
  {
    if (Object.op_Equality((Object) this.resLoad, (Object) null) || Object.op_Equality(this.loadedObject, (Object) null))
    {
      Log.Error("it is not loaded.");
      return (Transform) null;
    }
    if (this.loadedObject is GameObject)
      return ResourceUtility.Realizes(this.loadedObject, parent, layer);
    Log.Error("it is not GameObject.");
    return (Transform) null;
  }

  public static Transform RealizesWithGameObject(GameObject gameObj, Transform parent = null, int layer = -1)
  {
    return ResourceUtility.Realizes((Object) gameObj, parent, layer);
  }

  public virtual GameObject PopInstantiatedGameObject() => (GameObject) null;

  public virtual bool HasInstantiatedGameObject() => false;

  public void ReleaseAllResources()
  {
    if (!MonoBehaviourSingleton<ResourceManager>.IsValid() || MonoBehaviourSingleton<ResourceManager>.I.cache == null)
      return;
    if (this.loadedObjects != null)
    {
      MonoBehaviourSingleton<ResourceManager>.I.cache.ReleaseResourceObjects(this.loadedObjects);
      this.loadedObjects = (ResourceObject[]) null;
    }
    if (Object.op_Inequality((Object) this.resLoad, (Object) null))
    {
      this.resLoad.ReleaseAllResources();
      this.resLoad = (ResourceLoad) null;
    }
    this.loadedObject = (Object) null;
  }
}
