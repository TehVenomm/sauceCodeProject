// Decompiled with JetBrains decompiler
// Type: ResourceManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using MsgPack;
using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

#nullable disable
public class ResourceManager : MonoBehaviourSingleton<ResourceManager>
{
  public static bool isDownloadAssets = false;
  public static bool enableLoadDirect = true;
  private Dictionary<string, string[]> m_cacheABDependencies = new Dictionary<string, string[]>();
  private static object PROGRESS_COMPLATE = (object) 1f;
  public static bool enableCache = true;
  public static bool downloadOnly = false;
  public static bool internalMode = false;
  public static bool autoRetry = false;
  public Func<bool, Error, int> onDownloadErrorQuery;
  public Func<bool> onAsyncLoadQuery;
  public List<ResourceManager.LoadRequest> loadRequests = new List<ResourceManager.LoadRequest>(64 /*0x40*/);
  public ResourceCache cache = new ResourceCache();
  private int _manifestVersion = -1;
  private int _assetIndex = 7;
  private int _tableIndex = 1;
  private string baseURL = string.Empty;
  public System.Action onAddRequest;
  public System.Action onRemoveRequest;
  private BetterList<ResourceManager.LoadRequest> downloadList = new BetterList<ResourceManager.LoadRequest>();
  private static int MAX_DL_COUNT = 3;
  private static float WWW_TIME_OUT = 90f;
  private bool isDownloadError;
  public int stayCount;
  public const int DEFAULT_LOADING_ASSET_COUNT_LIMIT = 4;

  public bool isLoadingManifest { get; private set; }

  public string downloadURL { get; private set; }

  public string downloadEventUrl { get; private set; }

  public AssetBundleManifest manifest { get; private set; }

  public bool isLoadingSizeInfoManifest { get; private set; }

  public string downloadSizeInfoURL { get; private set; }

  public AssetBundleManifest sizeInfoManifest { get; private set; }

  public AssetBundleManifest event_manifest { get; private set; }

  public bool streamingAssetsMode { get; private set; }

  public int manifestVersion
  {
    get => this._manifestVersion;
    set
    {
      if (this._manifestVersion == value)
        return;
      this._manifestVersion = value;
      CrashlyticsReporter.SetManifestVersion(value);
    }
  }

  public int assetIndex
  {
    get => this._assetIndex;
    set
    {
      if (this._assetIndex == value)
        return;
      this._assetIndex = value;
      CrashlyticsReporter.SetAssetIndex(value);
    }
  }

  public int tableIndex
  {
    get => this._tableIndex;
    set
    {
      if (this._tableIndex == value)
        return;
      this._tableIndex = value;
    }
  }

  private static string cacheDir => Path.Combine(Application.temporaryCachePath, "assets");

  private static string cachingDir
  {
    get
    {
      Cache currentCacheForWriting = Caching.currentCacheForWriting;
      return ((Cache) ref currentCacheForWriting).path;
    }
  }

  public bool isAllStay => this.loadRequests.Count == this.stayCount;

  public int loadingAssetCountFromAssetBundle { get; private set; }

  public int loadingAssetCountLimit { get; set; }

  protected override void Awake()
  {
    base.Awake();
    ((Component) this).gameObject.AddComponent<GoGameResourceManager>();
    Object.DontDestroyOnLoad((Object) this);
    this.onAsyncLoadQuery = new Func<bool>(this.OnAsyncLoadQueryDefault);
    this.loadingAssetCountLimit = 4;
  }

  private bool OnAsyncLoadQueryDefault() => false;

  private void Update() => this.cache.Update();

  public string GetPlatformName() => "and";

  private string GetRelativePath() => Application.streamingAssetsPath;

  public void SetURL(string url)
  {
    ResourceManager.isDownloadAssets = true;
    this.baseURL = url;
    this.UpdateDownloadURL();
  }

  private void UpdateDownloadURL()
  {
    int assetIndex = this.assetIndex;
    this.downloadURL = $"{this.baseURL}assets/{assetIndex}/{this.GetPlatformName()}/";
    this.downloadSizeInfoURL = $"{this.baseURL}assets/{assetIndex}/{this.GetPlatformName()}_info/";
    this.downloadEventUrl = $"{this.baseURL}assets/event/{this.GetPlatformName()}/";
  }

  public void LoadSizeInfoManifest() => this.StartCoroutine(this.DoLoadSizeInfoManifest());

  private IEnumerator DoLoadSizeInfoManifest()
  {
    while (this.isLoadingSizeInfoManifest || this.isLoading)
      yield return (object) null;
    this.isLoadingSizeInfoManifest = true;
    this.sizeInfoManifest = (AssetBundleManifest) null;
    if (ResourceManager.isDownloadAssets)
    {
      this.UpdateDownloadURL();
      string url = $"{this.downloadSizeInfoURL}{this.GetPlatformName()}_info";
      int retry_count = 0;
      Error error_code = Error.None;
      do
      {
        error_code = Error.None;
        UnityWebRequest _www = UnityWebRequestAssetBundle.GetAssetBundle(url);
        yield return (object) _www.SendWebRequest();
        string error = _www.error;
        AssetBundle content = DownloadHandlerAssetBundle.GetContent(_www);
        if (string.IsNullOrEmpty(error))
        {
          if (Object.op_Inequality((Object) content, (Object) null))
          {
            this.sizeInfoManifest = content.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            content.Unload(false);
          }
          else
          {
            error_code = Error.AssetLoadFailed;
            Log.Error(LOG.RESOURCE, _www.downloadHandler.text);
          }
        }
        else
          error_code = !error.Contains("404") ? Error.AssetLoadFailed : Error.AssetNotFound;
        _www.Dispose();
        _www = (UnityWebRequest) null;
        if (error_code != Error.None)
        {
          ++retry_count;
          if (retry_count >= 3)
          {
            Log.Error(LOG.RESOURCE, error);
            int query_result = 0;
            if (this.onDownloadErrorQuery != null)
            {
              while (this.onDownloadErrorQuery != null)
              {
                query_result = this.onDownloadErrorQuery(true, error_code);
                if (query_result != 0)
                  yield return (object) null;
                else
                  break;
              }
              while (query_result == 0 && this.onDownloadErrorQuery != null)
              {
                query_result = this.onDownloadErrorQuery(false, error_code);
                yield return (object) null;
              }
              if (query_result == -1)
                yield break;
            }
            retry_count = 0;
          }
          yield return (object) new WaitForSeconds(1f);
        }
        _www = (UnityWebRequest) null;
      }
      while (error_code != Error.None);
      url = (string) null;
    }
    this.isLoadingSizeInfoManifest = false;
  }

  public void LoadManifest() => this.StartCoroutine(this.DoLoadManifest());

  private IEnumerator DoLoadManifest()
  {
    while (this.isLoadingManifest || this.isLoading)
      yield return (object) null;
    this.isLoadingManifest = true;
    this.manifest = (AssetBundleManifest) null;
    if (ResourceManager.isDownloadAssets)
    {
      this.UpdateDownloadURL();
      string url = $"{this.downloadURL}{this.GetPlatformName()}_v{this.manifestVersion}";
      int retry_count = 0;
      Error error_code = Error.None;
      do
      {
        error_code = Error.None;
        UnityWebRequest _www = UnityWebRequestAssetBundle.GetAssetBundle(url);
        yield return (object) _www.SendWebRequest();
        string error = _www.error;
        AssetBundle content = DownloadHandlerAssetBundle.GetContent(_www);
        if (string.IsNullOrEmpty(error))
        {
          if (Object.op_Inequality((Object) content, (Object) null))
          {
            this.manifest = content.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            content.Unload(false);
          }
          else
          {
            error_code = Error.AssetLoadFailed;
            Log.Error(LOG.RESOURCE, _www.downloadHandler.text);
          }
        }
        else
          error_code = !error.Contains("404") ? Error.AssetLoadFailed : Error.AssetNotFound;
        _www.Dispose();
        _www = (UnityWebRequest) null;
        if (error_code != Error.None)
        {
          ++retry_count;
          if (retry_count >= 3)
          {
            Log.Error(LOG.RESOURCE, error);
            int query_result = 0;
            if (this.onDownloadErrorQuery != null)
            {
              while (this.onDownloadErrorQuery != null)
              {
                query_result = this.onDownloadErrorQuery(true, error_code);
                if (query_result != 0)
                  yield return (object) null;
                else
                  break;
              }
              while (query_result == 0 && this.onDownloadErrorQuery != null)
              {
                query_result = this.onDownloadErrorQuery(false, error_code);
                yield return (object) null;
              }
              if (query_result == -1)
                yield break;
            }
            retry_count = 0;
          }
          yield return (object) new WaitForSeconds(1f);
        }
        _www = (UnityWebRequest) null;
      }
      while (error_code != Error.None);
      url = (string) null;
    }
    yield return (object) this.StartCoroutine(this.DoLoadEventManifest());
  }

  private IEnumerator DoLoadEventManifest()
  {
    this.event_manifest = (AssetBundleManifest) null;
    if (ResourceManager.isDownloadAssets)
    {
      string url = $"{this.downloadEventUrl}{this.GetPlatformName()}_v{this.manifestVersion}";
      int retry_count = 0;
      Error error_code = Error.None;
      do
      {
        error_code = Error.None;
        UnityWebRequest _www = UnityWebRequestAssetBundle.GetAssetBundle(url);
        yield return (object) _www.SendWebRequest();
        string error = _www.error;
        AssetBundle content = DownloadHandlerAssetBundle.GetContent(_www);
        if (string.IsNullOrEmpty(error))
        {
          if (Object.op_Inequality((Object) content, (Object) null))
          {
            this.event_manifest = content.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            content.Unload(false);
          }
          else
          {
            error_code = Error.AssetLoadFailed;
            Log.Error(LOG.RESOURCE, _www.downloadHandler.text);
          }
        }
        else
          error_code = !error.Contains("404") ? Error.AssetLoadFailed : Error.AssetNotFound;
        _www.Dispose();
        _www = (UnityWebRequest) null;
        if (error_code != Error.None)
        {
          ++retry_count;
          if (retry_count >= 3)
          {
            Log.Error(LOG.RESOURCE, error);
            int query_result = 0;
            if (this.onDownloadErrorQuery != null)
            {
              while (this.onDownloadErrorQuery != null)
              {
                query_result = this.onDownloadErrorQuery(true, error_code);
                if (query_result != 0)
                  yield return (object) null;
                else
                  break;
              }
              while (query_result == 0 && this.onDownloadErrorQuery != null)
              {
                query_result = this.onDownloadErrorQuery(false, error_code);
                yield return (object) null;
              }
              if (query_result == -1)
                yield break;
            }
            retry_count = 0;
          }
          yield return (object) new WaitForSeconds(1f);
        }
        _www = (UnityWebRequest) null;
      }
      while (error_code != Error.None);
      url = (string) null;
    }
    this.isLoadingManifest = false;
  }

  public void Reset()
  {
    if (this.isLoading)
      Debug.LogError((object) "isLoading == true");
    this.CancelAll();
    this.loadRequests.Clear();
    this.StopAllCoroutines();
    this.cache.ClearObjectCaches(true);
    this.cache.ClearPackageCaches();
    this.cache.ClearSystemPackageCaches();
    this.cache.ClearSENameDictionary();
    this.cache.ReleaseAllDelayUnloadAssetBundles();
    this.cache.ClearShaderCaches();
    this.downloadList.Clear();
    this.m_cacheABDependencies.Clear();
    this.isDownloadError = false;
  }

  public void Load(
    bool isEventAsset,
    object master,
    RESOURCE_CATEGORY category,
    string resource_name,
    ResourceManager.LoadComplateDelegate complate_func,
    ResourceManager.LoadErrorDelegate error_func,
    bool cache_package = false,
    object userData = null)
  {
    this.Load(new ResourceManager.LoadRequest()
    {
      master = master,
      category = category,
      packageName = resource_name,
      resourceNames = new string[1]{ resource_name },
      onComplate = complate_func,
      onError = error_func,
      cachePackage = cache_package,
      userData = userData,
      eventAsset = isEventAsset
    });
  }

  public void Load(
    object master,
    RESOURCE_CATEGORY category,
    string resource_name,
    ResourceManager.LoadComplateDelegate complate_func,
    ResourceManager.LoadErrorDelegate error_func,
    bool cache_package = false,
    object userData = null)
  {
    this.Load(new ResourceManager.LoadRequest()
    {
      master = master,
      category = category,
      packageName = resource_name,
      resourceNames = new string[1]{ resource_name },
      onComplate = complate_func,
      onError = error_func,
      cachePackage = cache_package,
      userData = userData
    });
  }

  public void Load(
    bool isEventAsset,
    object master,
    RESOURCE_CATEGORY category,
    string package_name,
    string[] resource_names,
    ResourceManager.LoadComplateDelegate complate_func,
    ResourceManager.LoadErrorDelegate error_func,
    bool cache_package = false,
    object userData = null)
  {
    this.Load(new ResourceManager.LoadRequest()
    {
      master = master,
      category = category,
      packageName = package_name,
      resourceNames = resource_names,
      onComplate = complate_func,
      onError = error_func,
      cachePackage = cache_package,
      userData = userData,
      eventAsset = isEventAsset
    });
  }

  public void Load(
    object master,
    RESOURCE_CATEGORY category,
    string package_name,
    string[] resource_names,
    ResourceManager.LoadComplateDelegate complate_func,
    ResourceManager.LoadErrorDelegate error_func,
    bool cache_package = false,
    object userData = null)
  {
    this.Load(new ResourceManager.LoadRequest()
    {
      master = master,
      category = category,
      packageName = package_name,
      resourceNames = resource_names,
      onComplate = complate_func,
      onError = error_func,
      cachePackage = cache_package,
      userData = userData
    });
  }

  private bool Load(ResourceManager.LoadRequest request)
  {
    request.enableCache = ResourceManager.enableCache;
    request.downloadOnly = ResourceManager.downloadOnly;
    request.internalMode = ResourceManager.internalMode;
    if (request.category == RESOURCE_CATEGORY.UI)
      request.internalMode = false;
    List<ResourceManager.LoadRequest> loadRequestList1 = (List<ResourceManager.LoadRequest>) null;
    AssetBundleManifest assetBundleManifest = this.manifest;
    if (request.category == RESOURCE_CATEGORY.ASSETBUNDLEINFO)
    {
      request.internalMode = false;
      request.downloadOnly = false;
      assetBundleManifest = this.sizeInfoManifest;
    }
    if ((!request.internalMode || request.downloadOnly) && ResourceManager.isDownloadAssets && Object.op_Inequality((Object) assetBundleManifest, (Object) null))
    {
      request.Setup();
      if (request.category == RESOURCE_CATEGORY.ASSETBUNDLEINFO)
        request.hash = this.sizeInfoManifest.GetAssetBundleHash(request.packageName);
      else if (request.category != RESOURCE_CATEGORY.UI)
      {
        if (request.eventAsset)
        {
          Debug.Log((object) ("Check hash: " + request.packageName));
          request.hash = this.event_manifest.GetAssetBundleHash(MonoBehaviourSingleton<GoGameResourceManager>.I.GetFullBundleName(request.packageName));
        }
        else
          request.hash = this.manifest.GetAssetBundleHash(MonoBehaviourSingleton<GoGameResourceManager>.I.GetFullBundleName(request.packageName));
        if (!request.IsValid())
          return false;
        loadRequestList1 = this.GetManifestDependencyRequests(request);
      }
      else
      {
        request.internalMode = true;
        request.uiDep = this.GetUIDependency(request);
        if (request.uiDep != null && request.uiDep.atlasPaths != null && !ResourceManager.internalMode)
          loadRequestList1 = this.GetUIDependencyRequests(request, request.uiDep);
        request.downloadOnly = false;
      }
      if (loadRequestList1 != null)
      {
        List<ResourceManager.LoadRequest> loadRequestList2 = loadRequestList1;
        for (int index1 = 0; index1 < loadRequestList2.Count; ++index1)
        {
          bool flag = false;
          for (int index2 = 0; index2 < this.loadRequests.Count; ++index2)
          {
            if (this.loadRequests[index2] == loadRequestList2[index1])
            {
              flag = true;
              break;
            }
          }
          if (!flag)
            this.AddRequest(loadRequestList2[index1]);
        }
      }
    }
    else if (request.category == RESOURCE_CATEGORY.UI)
      request.uiDep = this.GetUIDependency(request);
    ResourceManager.LoadRequest loadRequest1 = (ResourceManager.LoadRequest) null;
    int index3 = 0;
    for (int count = this.loadRequests.Count; index3 < count; ++index3)
    {
      ResourceManager.LoadRequest loadRequest2 = this.loadRequests[index3];
      if ((loadRequest2.master != null || loadRequest2.sameRequests != null) && !(loadRequest2.packageName != request.packageName))
      {
        if (loadRequest2.category != request.category)
        {
          loadRequest1 = loadRequest2;
        }
        else
        {
          if (loadRequest2.resourceNames != null && request.resourceNames != null)
          {
            if (loadRequest2.resourceNames.Length != request.resourceNames.Length)
            {
              loadRequest1 = loadRequest2;
              continue;
            }
            int index4 = 0;
            int length = loadRequest2.resourceNames.Length;
            while (index4 < length && !(loadRequest2.resourceNames[index4] != request.resourceNames[index4]))
              ++index4;
            if (index4 != length)
            {
              loadRequest1 = loadRequest2;
              continue;
            }
          }
          if (loadRequest2.sameRequests == null)
            loadRequest2.sameRequests = new List<ResourceManager.LoadRequest>();
          loadRequest2.sameRequests.Add(request);
          return true;
        }
      }
    }
    if (loadRequest1 != null)
    {
      if (loadRequestList1 == null)
        loadRequestList1 = new List<ResourceManager.LoadRequest>();
      loadRequest1.cachePackage = true;
      loadRequestList1.Add(loadRequest1);
    }
    request.dependencyRequests = loadRequestList1;
    this.AddRequest(request);
    return true;
  }

  public void LoadAssetBundle(
    bool isEventAsset,
    object master,
    RESOURCE_CATEGORY category,
    string resource_name,
    ResourceManager.LoadComplateDelegate complate_func,
    ResourceManager.LoadErrorDelegate error_func,
    bool cache_package = false,
    bool unload_Asset = false,
    object userData = null)
  {
    this.LoadAssetBundle(new ResourceManager.LoadRequest()
    {
      master = master,
      category = category,
      packageName = resource_name,
      resourceNames = new string[1]{ resource_name },
      onComplate = complate_func,
      onError = error_func,
      cachePackage = cache_package,
      userData = userData,
      eventAsset = isEventAsset,
      unloadAsset = unload_Asset
    });
  }

  public void LoadAssetBundle(
    object master,
    RESOURCE_CATEGORY category,
    string resource_name,
    ResourceManager.LoadComplateDelegate complate_func,
    ResourceManager.LoadErrorDelegate error_func,
    bool cache_package = false,
    bool unload_Asset = false,
    object userData = null)
  {
    this.LoadAssetBundle(new ResourceManager.LoadRequest()
    {
      master = master,
      category = category,
      packageName = resource_name,
      resourceNames = new string[1]{ resource_name },
      onComplate = complate_func,
      onError = error_func,
      cachePackage = cache_package,
      userData = userData,
      unloadAsset = unload_Asset
    });
  }

  public void LoadAssetBundle(
    bool isEventAsset,
    object master,
    RESOURCE_CATEGORY category,
    string package_name,
    string[] resource_names,
    ResourceManager.LoadComplateDelegate complate_func,
    ResourceManager.LoadErrorDelegate error_func,
    bool cache_package = false,
    bool unload_Asset = false,
    object userData = null)
  {
    this.LoadAssetBundle(new ResourceManager.LoadRequest()
    {
      master = master,
      category = category,
      packageName = package_name,
      resourceNames = resource_names,
      onComplate = complate_func,
      onError = error_func,
      cachePackage = cache_package,
      userData = userData,
      eventAsset = isEventAsset,
      unloadAsset = unload_Asset
    });
  }

  public void LoadAssetBundle(
    object master,
    RESOURCE_CATEGORY category,
    string package_name,
    string[] resource_names,
    ResourceManager.LoadComplateDelegate complate_func,
    ResourceManager.LoadErrorDelegate error_func,
    bool cache_package = false,
    object userData = null)
  {
    this.LoadAssetBundle(new ResourceManager.LoadRequest()
    {
      master = master,
      category = category,
      packageName = package_name,
      resourceNames = resource_names,
      onComplate = complate_func,
      onError = error_func,
      cachePackage = cache_package,
      userData = userData
    });
  }

  private void LoadAssetBundle(ResourceManager.LoadRequest request)
  {
    request.enableCache = ResourceManager.enableCache;
    request.downloadOnly = ResourceManager.downloadOnly;
    request.internalMode = ResourceManager.internalMode;
    if (request.category == RESOURCE_CATEGORY.UI)
      request.internalMode = false;
    List<ResourceManager.LoadRequest> loadRequestList1 = (List<ResourceManager.LoadRequest>) null;
    AssetBundleManifest assetBundleManifest = this.manifest;
    if (request.category == RESOURCE_CATEGORY.ASSETBUNDLEINFO)
    {
      request.internalMode = false;
      request.downloadOnly = false;
      assetBundleManifest = this.sizeInfoManifest;
    }
    if ((!request.internalMode || request.downloadOnly) && Object.op_Inequality((Object) assetBundleManifest, (Object) null))
    {
      request.Setup();
      if (request.category == RESOURCE_CATEGORY.ASSETBUNDLEINFO)
        request.hash = this.sizeInfoManifest.GetAssetBundleHash(request.packageName);
      else if (request.category != RESOURCE_CATEGORY.UI)
      {
        request.hash = !request.eventAsset ? this.manifest.GetAssetBundleHash(MonoBehaviourSingleton<GoGameResourceManager>.I.GetFullBundleName(request.packageName)) : this.event_manifest.GetAssetBundleHash(MonoBehaviourSingleton<GoGameResourceManager>.I.GetFullBundleName(request.packageName));
        loadRequestList1 = this.GetManifestDependencyRequests(request);
      }
      else
      {
        request.internalMode = true;
        request.uiDep = this.GetUIDependency(request);
        if (request.uiDep != null && request.uiDep.atlasPaths != null && !ResourceManager.internalMode)
          loadRequestList1 = this.GetUIDependencyRequests(request, request.uiDep);
        request.downloadOnly = false;
      }
      if (loadRequestList1 != null)
      {
        List<ResourceManager.LoadRequest> loadRequestList2 = loadRequestList1;
        for (int index1 = 0; index1 < loadRequestList2.Count; ++index1)
        {
          bool flag = false;
          for (int index2 = 0; index2 < this.loadRequests.Count; ++index2)
          {
            if (this.loadRequests[index2] == loadRequestList2[index1])
            {
              flag = true;
              break;
            }
          }
          if (!flag)
          {
            if (((Hash128) ref loadRequestList2[index1].hash).isValid)
              this.AddAssetBundleRequest(loadRequestList2[index1]);
            else
              this.AddRequest(loadRequestList2[index1]);
          }
        }
      }
    }
    else if (request.category == RESOURCE_CATEGORY.UI)
      request.uiDep = this.GetUIDependency(request);
    ResourceManager.LoadRequest loadRequest1 = (ResourceManager.LoadRequest) null;
    int index3 = 0;
    for (int count = this.loadRequests.Count; index3 < count; ++index3)
    {
      ResourceManager.LoadRequest loadRequest2 = this.loadRequests[index3];
      if ((loadRequest2.master != null || loadRequest2.sameRequests != null) && !(loadRequest2.packageName != request.packageName))
      {
        if (loadRequest2.category != request.category)
        {
          loadRequest1 = loadRequest2;
        }
        else
        {
          if (loadRequest2.resourceNames != null && request.resourceNames != null)
          {
            if (loadRequest2.resourceNames.Length != request.resourceNames.Length)
            {
              loadRequest1 = loadRequest2;
              continue;
            }
            int index4 = 0;
            int length = loadRequest2.resourceNames.Length;
            while (index4 < length && !(loadRequest2.resourceNames[index4] != request.resourceNames[index4]))
              ++index4;
            if (index4 != length)
            {
              loadRequest1 = loadRequest2;
              continue;
            }
          }
          if (loadRequest2.sameRequests == null)
            loadRequest2.sameRequests = new List<ResourceManager.LoadRequest>();
          loadRequest2.sameRequests.Add(request);
          if (request.category != RESOURCE_CATEGORY.UI)
            return;
          Debug.Log((object) ("Same Request: " + request.packageName));
          return;
        }
      }
    }
    if (loadRequest1 != null)
    {
      if (loadRequestList1 == null)
        loadRequestList1 = new List<ResourceManager.LoadRequest>();
      loadRequest1.cachePackage = true;
      loadRequestList1.Add(loadRequest1);
    }
    request.dependencyRequests = loadRequestList1;
    if (((Hash128) ref request.hash).isValid)
      this.AddAssetBundleRequest(request);
    else
      this.AddRequest(request);
  }

  private UIDependency GetUIDependency(ResourceManager.LoadRequest request)
  {
    TextAsset textAsset = Resources.Load("InternalUI/Deps/" + Path.GetFileNameWithoutExtension(request.packageName).ToLower()) as TextAsset;
    return Object.op_Inequality((Object) textAsset, (Object) null) ? new ObjectPacker().Unpack<UIDependency>(textAsset.bytes) : (UIDependency) null;
  }

  private List<ResourceManager.LoadRequest> GetManifestDependencyRequests(
    ResourceManager.LoadRequest request)
  {
    string fullBundleName = MonoBehaviourSingleton<GoGameResourceManager>.I.GetFullBundleName(request.packageName);
    string[] strArray;
    if (this.m_cacheABDependencies.ContainsKey(fullBundleName))
    {
      strArray = this.m_cacheABDependencies[fullBundleName];
    }
    else
    {
      strArray = this.manifest.GetAllDependencies(fullBundleName);
      this.m_cacheABDependencies.Add(fullBundleName, strArray);
    }
    List<ResourceManager.LoadRequest> dependencyRequests = (List<ResourceManager.LoadRequest>) null;
    int index1 = 0;
    for (int length = strArray.Length; index1 < length; ++index1)
    {
      string nameWithoutVariant = MonoBehaviourSingleton<GoGameResourceManager>.I.GetBundleNameWithoutVariant(strArray[index1]);
      if (!(nameWithoutVariant == "shader" + GoGameResourceManager.GetDefaultAssetBundleExtension()) && !(nameWithoutVariant == "ui_font" + GoGameResourceManager.GetDefaultAssetBundleExtension()) && this.cache.GetCachedPackage(nameWithoutVariant) == null)
      {
        if (dependencyRequests == null)
          dependencyRequests = new List<ResourceManager.LoadRequest>();
        ResourceManager.LoadRequest loadRequest = (ResourceManager.LoadRequest) null;
        int index2 = 0;
        for (int count = this.loadRequests.Count; index2 < count; ++index2)
        {
          if (this.loadRequests[index2].packageName == nameWithoutVariant)
          {
            loadRequest = this.loadRequests[index2];
            break;
          }
        }
        if (loadRequest == null)
        {
          loadRequest = new ResourceManager.LoadRequest();
          loadRequest.master = request.master;
          loadRequest.category = RESOURCE_CATEGORY.MAX;
          loadRequest.packageName = strArray[index1];
          loadRequest.resourceNames = (string[]) null;
          loadRequest.onComplate = (ResourceManager.LoadComplateDelegate) null;
          loadRequest.onError = (ResourceManager.LoadErrorDelegate) null;
          loadRequest.cachePackage = true;
          loadRequest.userData = (object) null;
          loadRequest.enableCache = true;
          loadRequest.hash = this.manifest.GetAssetBundleHash(MonoBehaviourSingleton<GoGameResourceManager>.I.GetFullBundleName(nameWithoutVariant));
          loadRequest.downloadOnly = ResourceManager.downloadOnly;
        }
        else
          loadRequest.cachePackage = true;
        dependencyRequests.Add(loadRequest);
      }
    }
    return dependencyRequests;
  }

  private List<ResourceManager.LoadRequest> GetUIDependencyRequests(
    ResourceManager.LoadRequest request,
    UIDependency dep)
  {
    List<ResourceManager.LoadRequest> dependencyRequests = (List<ResourceManager.LoadRequest>) null;
    for (int index1 = 0; index1 < dep.atlasPaths.Length; ++index1)
    {
      string atlasPath = dep.atlasPaths[index1];
      string assetBundleName = RESOURCE_CATEGORY.UI_ATLAS.ToAssetBundleName(UIDependency.GetAtlasName(atlasPath));
      string resourceName = UIDependency.GetAtlasName(atlasPath) + "_Bundle";
      PackageObject cachedPackage = this.cache.GetCachedPackage(assetBundleName);
      if (cachedPackage != null)
      {
        this.LinkAtlas(cachedPackage, atlasPath, resourceName);
      }
      else
      {
        ResourceManager.LoadRequest loadRequest = (ResourceManager.LoadRequest) null;
        int index2 = 0;
        for (int count = this.loadRequests.Count; index2 < count; ++index2)
        {
          if (this.loadRequests[index2].packageName == assetBundleName)
          {
            loadRequest = this.loadRequests[index2];
            break;
          }
        }
        if (loadRequest == null)
        {
          loadRequest = new ResourceManager.LoadRequest();
          loadRequest.master = request.master;
          loadRequest.category = RESOURCE_CATEGORY.UI_ATLAS;
          loadRequest.packageName = assetBundleName;
          loadRequest.resourceNames = new string[1]
          {
            resourceName
          };
          loadRequest.onComplate = (ResourceManager.LoadComplateDelegate) null;
          if (!request.downloadOnly)
            loadRequest.onAtlasComplete = (ResourceManager.LoadComplateDelegate) ((req, objs) =>
            {
              if (objs == null || objs.Length < 1)
                return;
              this.LinkAtlas(objs[0].package, atlasPath, resourceName);
            });
          loadRequest.onError = (ResourceManager.LoadErrorDelegate) null;
          loadRequest.cachePackage = true;
          loadRequest.userData = (object) null;
          loadRequest.enableCache = true;
          loadRequest.hash = this.manifest.GetAssetBundleHash(MonoBehaviourSingleton<GoGameResourceManager>.I.GetFullBundleName(assetBundleName));
          loadRequest.downloadOnly = ResourceManager.downloadOnly;
        }
        if (dependencyRequests == null)
          dependencyRequests = new List<ResourceManager.LoadRequest>();
        dependencyRequests.Add(loadRequest);
      }
    }
    return dependencyRequests;
  }

  private void LinkAtlas(PackageObject package, string atlasPath, string resourceName)
  {
    UIAtlas component1 = (Resources.Load(atlasPath) as GameObject).GetComponent<UIAtlas>();
    package.hostAtlas = component1;
    AssetBundle assetBundle = package.obj as AssetBundle;
    if (!Object.op_Inequality((Object) assetBundle, (Object) null))
      return;
    UIAtlas component2 = assetBundle.LoadAsset<GameObject>(resourceName).GetComponent<UIAtlas>();
    component1.replacement = component2;
  }

  private void AddRequest(ResourceManager.LoadRequest request)
  {
    this.loadRequests.Add(request);
    if (this.onAddRequest != null)
      this.onAddRequest();
    this.StartCoroutine(this.DoLoad(request));
  }

  private bool CheckPackageDownloading(ResourceManager.LoadRequest request)
  {
    if (request == null || string.IsNullOrEmpty(request.packageName))
      return false;
    int i = 0;
    for (int size = this.downloadList.size; i < size; ++i)
    {
      if (!string.IsNullOrEmpty(this.downloadList[i].packageName) && this.downloadList[i].packageName == request.packageName)
        return true;
    }
    return false;
  }

  private IEnumerator DoLoad(ResourceManager.LoadRequest request)
  {
    if (MonoBehaviourSingleton<AppMain>.I.isExecutingClearMemory)
    {
      ++this.stayCount;
      while (MonoBehaviourSingleton<AppMain>.I.isExecutingClearMemory)
      {
        if (!request.IsValid())
        {
          --this.stayCount;
          this.BreakRequest(ref request);
          yield break;
        }
        yield return (object) null;
      }
      --this.stayCount;
    }
    int j;
    int m;
    if (request.dependencyRequests != null)
    {
      j = 0;
      m = request.dependencyRequests.Count;
      while (request.IsValid())
      {
        bool flag = false;
        ResourceManager.LoadRequest dependencyRequest = request.dependencyRequests[j];
        int index = 0;
        for (int count = this.loadRequests.Count; index < count; ++index)
        {
          if (this.loadRequests[index] == dependencyRequest)
          {
            flag = true;
            break;
          }
        }
        if (flag)
        {
          yield return (object) null;
        }
        else
        {
          ++j;
          if (j == m)
            goto label_19;
        }
      }
      this.BreakRequest(ref request);
      yield break;
    }
label_19:
    BetterList<PackageObject> dependency_packages = this.cache.GetDependencyPackages(request.dependencyRequests);
    if (!request.internalMode)
    {
      while (this.isDownloadError && !request.internalMode)
      {
        yield return (object) null;
        if (!request.IsValid())
        {
          this.BreakRequest(ref request);
          yield break;
        }
      }
    }
    PackageObject load_package = (PackageObject) null;
    AssetBundle asset_bundle = (AssetBundle) null;
    ResourceObject[] load_objects = (ResourceObject[]) null;
    bool use_package_cache = false;
    bool use_package_cache_delay = false;
    bool[] use_object_caches = (bool[]) null;
    int load_num = request.resourceNames != null ? request.resourceNames.Length : 0;
    int loaded_num = 0;
    bool package_only = load_num == 0;
    if (request.downloadOnly)
      package_only = true;
    if (load_num > 0)
    {
      load_objects = new ResourceObject[load_num];
      use_object_caches = new bool[load_num];
    }
    StringKeyTable<ResourceObject> object_cache_category = request.category != RESOURCE_CATEGORY.MAX ? this.cache.objectCaches[(int) request.category] : (StringKeyTable<ResourceObject>) null;
    load_package = this.cache.GetCachedPackage(request.packageName);
    if (load_package != null)
    {
      asset_bundle = load_package.obj as AssetBundle;
      use_package_cache = true;
    }
    if (Object.op_Equality((Object) asset_bundle, (Object) null))
      load_package = (PackageObject) null;
    if (loaded_num < load_num && this.cache.objectCaches != null && this.cache.systemCaches != null && object_cache_category != null)
    {
      int index1 = 0;
      for (int index2 = load_num; index1 < index2; ++index1)
      {
        if (load_objects[index1] == null)
        {
          string res_name = request.resourceNames[index1];
          load_objects[index1] = object_cache_category.Get(res_name);
          if (load_objects[index1] == null)
            load_objects[index1] = this.cache.systemCaches.Find((Predicate<ResourceObject>) (o => o.obj.name == res_name));
          if (load_objects[index1] != null)
          {
            use_object_caches[index1] = true;
            ++loaded_num;
          }
        }
      }
    }
    AssetBundleRequest asset_bundle_load_asset;
    if (loaded_num < load_num && !request.downloadOnly && load_package != null && Object.op_Inequality((Object) asset_bundle, (Object) null))
    {
      m = 0;
      for (j = load_num; m < j && (request.master != null || request.sameRequests != null) && !Object.op_Equality((Object) asset_bundle, (Object) null); ++m)
      {
        if (load_objects[m] == null)
        {
          if (this.onAsyncLoadQuery())
          {
            while (this.IsWaitAssetBundleLoadAsync())
              yield return (object) null;
            this.loadingAssetCountFromAssetBundle++;
            asset_bundle_load_asset = asset_bundle.LoadAssetAsync(request.resourceNames[m]);
            while (!((AsyncOperation) asset_bundle_load_asset).isDone)
              yield return (object) null;
            load_objects[m] = ResourceObject.Get(request.category, request.resourceNames[m], asset_bundle_load_asset.asset);
            this.loadingAssetCountFromAssetBundle--;
            asset_bundle_load_asset = (AssetBundleRequest) null;
          }
          else
            load_objects[m] = ResourceObject.Get(request.category, request.resourceNames[m], asset_bundle.LoadAsset(request.resourceNames[m]));
          if (load_objects[m] != null)
            ++loaded_num;
        }
      }
    }
    if (load_package == null && (loaded_num < load_num || package_only))
    {
      bool flag = request.category != RESOURCE_CATEGORY.ASSETBUNDLEINFO ? Object.op_Inequality((Object) this.manifest, (Object) null) : Object.op_Inequality((Object) this.sizeInfoManifest, (Object) null);
      string filename;
      if (request.category == RESOURCE_CATEGORY.MAX || ResourceManager.isDownloadAssets & flag && (!request.internalMode || request.downloadOnly))
      {
        while (this.downloadList.size >= ResourceManager.MAX_DL_COUNT || this.CheckPackageDownloading(request))
        {
          yield return (object) null;
          if (!request.IsValid())
          {
            this.BreakRequest(ref request);
            yield break;
          }
        }
        this.downloadList.Add(request);
        j = 0;
        bool is_retry = false;
        do
        {
          is_retry = false;
          if (!request.IsValid())
          {
            this.BreakRequest(ref request);
            yield break;
          }
          if (!request.downloadOnly)
          {
            load_package = this.cache.GetCachedPackage(request.packageName);
            if (load_package != null)
            {
              asset_bundle = load_package.obj as AssetBundle;
              use_package_cache = true;
            }
          }
          if (load_package != null || Object.op_Equality((Object) asset_bundle, (Object) null))
          {
            while (this.isDownloadError && !request.internalMode)
            {
              yield return (object) null;
              if (!request.IsValid())
              {
                this.BreakRequest(ref request);
                yield break;
              }
            }
            RESOURCE_CATEGORY? category = MonoBehaviourSingleton<GoGameResourceManager>.I.GetCategory(request.packageName);
            string url;
            if (request.eventAsset)
            {
              url = $"{this.downloadEventUrl}{request.packageName}?v={request.hash.ToString()}";
            }
            else
            {
              string variantName = MonoBehaviourSingleton<GoGameResourceManager>.I.GetVariantName(new RESOURCE_CATEGORY?(!category.HasValue ? RESOURCE_CATEGORY.MAX : category.Value));
              if (request.packageName.Contains("-sd"))
                url = $"{this.downloadURL}{request.packageName}?v={request.hash.ToString()}";
              else
                url = $"{this.downloadURL}{request.packageName}{variantName}?v={request.hash.ToString()}";
            }
            if (request.category == RESOURCE_CATEGORY.ASSETBUNDLEINFO)
              url = $"{this.downloadSizeInfoURL}{request.packageName}?v={request.hash.ToString()}";
            filename = Path.GetFileName(request.packageName);
            string str = url;
            if (request.IsValid())
            {
              int num = ResourceManager.IsVersionCached(filename, request.hash) ? 1 : 0;
              UnityWebRequest www = (UnityWebRequest) null;
              Error error_code = Error.None;
              AssetBundle loaded_assetBundle = (AssetBundle) null;
              AssetBundleCreateRequest loadMem;
              if (num == 0)
              {
                www = UnityWebRequest.Get(str);
                www.SendWebRequest();
                request.progressObject = (object) www;
                float timeOut = ResourceManager.WWW_TIME_OUT;
                float currentProgress = www.downloadProgress;
                while (!www.isDone && (double) timeOut > 0.0)
                {
                  yield return (object) null;
                  if ((double) currentProgress == (double) www.downloadProgress)
                  {
                    timeOut -= Time.deltaTime;
                  }
                  else
                  {
                    currentProgress = www.downloadProgress;
                    timeOut = ResourceManager.WWW_TIME_OUT;
                  }
                  if (!request.IsValid())
                  {
                    www.Dispose();
                    this.BreakRequest(ref request);
                    yield break;
                  }
                }
                if (!www.isDone)
                  error_code = Error.AssetLoadFailed;
                else if (www.error == null)
                {
                  if (!request.downloadOnly)
                  {
                    CrashlyticsReporter.SetLoadingBundle(url);
                    loaded_assetBundle = this.cache.PopDelayUnloadAssetBundle(request.packageName);
                    if (Object.op_Equality((Object) loaded_assetBundle, (Object) null))
                    {
                      load_package = this.cache.GetCachedPackage(request.packageName);
                      if (load_package != null)
                      {
                        loaded_assetBundle = load_package.obj as AssetBundle;
                        asset_bundle = loaded_assetBundle;
                        use_package_cache = true;
                        use_package_cache_delay = true;
                      }
                    }
                    if (Object.op_Equality((Object) loaded_assetBundle, (Object) null))
                    {
                      loadMem = AssetBundle.LoadFromMemoryAsync(www.downloadHandler.data);
                      yield return (object) loadMem;
                      loaded_assetBundle = loadMem.assetBundle;
                      loadMem = (AssetBundleCreateRequest) null;
                    }
                    CrashlyticsReporter.SetLoadingBundle("");
                  }
                  byte[] data = www.downloadHandler.data;
                  www.Dispose();
                  error_code = this.SaveAssetBundle(filename, request.hash, data);
                }
                else
                  error_code = !www.error.Contains("404") ? Error.AssetLoadFailed : Error.AssetNotFound;
              }
              else if (!request.downloadOnly)
              {
                string path = ResourceManager.GetCachePath(filename, request.hash);
                CrashlyticsReporter.SetLoadingBundle(url);
                loaded_assetBundle = this.cache.PopDelayUnloadAssetBundle(request.packageName);
                if (Object.op_Equality((Object) loaded_assetBundle, (Object) null))
                {
                  load_package = this.cache.GetCachedPackage(request.packageName);
                  if (load_package != null)
                  {
                    loaded_assetBundle = load_package.obj as AssetBundle;
                    asset_bundle = loaded_assetBundle;
                    use_package_cache = true;
                    use_package_cache_delay = true;
                  }
                }
                if (Object.op_Equality((Object) loaded_assetBundle, (Object) null))
                {
                  if (this.onAsyncLoadQuery())
                  {
                    loadMem = AssetBundle.LoadFromFileAsync(path);
                    yield return (object) loadMem;
                    loaded_assetBundle = loadMem.assetBundle;
                    loadMem = (AssetBundleCreateRequest) null;
                  }
                  else
                    loaded_assetBundle = AssetBundle.LoadFromFile(path);
                }
                CrashlyticsReporter.SetLoadingBundle("");
                if (Object.op_Equality((Object) loaded_assetBundle, (Object) null))
                {
                  Log.Warning(LOG.RESOURCE, "cached file load failed: {0}", (object) filename);
                  error_code = Error.AssetLoadFailed;
                  if (File.Exists(path))
                    File.Delete(path);
                }
                if (this.onAsyncLoadQuery())
                  yield return (object) null;
                path = (string) null;
              }
              if (request.progressObject != null)
                request.progressObject = ResourceManager.PROGRESS_COMPLATE;
              while (this.isDownloadError && !request.internalMode)
              {
                yield return (object) null;
                if (!request.IsValid())
                {
                  this.cache.AddDelayUnloadAssetBundle(request.packageName, ref loaded_assetBundle);
                  www.Dispose();
                  this.BreakRequest(ref request);
                  yield break;
                }
              }
              if (error_code == Error.None)
              {
                if (!request.downloadOnly && (request.cachePackage || !package_only || request.category == RESOURCE_CATEGORY.MAX))
                {
                  load_package = PackageObject.Get(request.packageName, (object) loaded_assetBundle);
                  asset_bundle = load_package.obj as AssetBundle;
                }
                www?.Dispose();
                if (!use_package_cache_delay)
                  use_package_cache = false;
              }
              else
              {
                load_package = this.cache.GetCachedPackage(request.packageName);
                if (load_package != null)
                {
                  asset_bundle = load_package.obj as AssetBundle;
                  use_package_cache = true;
                }
                if (load_package != null || Object.op_Equality((Object) asset_bundle, (Object) null))
                {
                  ++j;
                  if (ResourceManager.autoRetry)
                    j = 0;
                  if (j < 3)
                  {
                    yield return (object) new WaitForSeconds(1f);
                    is_retry = true;
                  }
                  else
                  {
                    if (error_code == Error.AssetLoadFailed)
                      Log.Error(LOG.RESOURCE, "{0}:{1}", (object) error_code.ToString(), (object) url);
                    else
                      Log.Error("{0}:{1}:{2}:{3}", (object) error_code.ToString(), (object) request.category, (object) request.packageName, (object) url);
                    if (this.isDownloadError)
                    {
                      while (this.isDownloadError)
                      {
                        if (!request.IsValid())
                        {
                          this.BreakRequest(ref request);
                          yield break;
                        }
                      }
                      j = 0;
                      is_retry = true;
                    }
                    else
                    {
                      this.isDownloadError = true;
                      m = 0;
                      if (request.IsValid() && this.onDownloadErrorQuery != null)
                      {
                        while (this.onDownloadErrorQuery != null)
                        {
                          m = this.onDownloadErrorQuery(true, error_code);
                          if (m != 0)
                            yield return (object) null;
                          else
                            break;
                        }
                        while (m == 0 && this.onDownloadErrorQuery != null)
                        {
                          m = this.onDownloadErrorQuery(false, error_code);
                          yield return (object) null;
                        }
                        if (m == -1)
                        {
                          this.cache.AddDelayUnloadAssetBundle(request.packageName, ref loaded_assetBundle);
                          this.BreakRequest(ref request);
                          yield break;
                        }
                      }
                      this.isDownloadError = false;
                      if (m != 1)
                      {
                        if (request.onError != null)
                          request.onError(request, ResourceManager.ERROR_CODE.WWW_ERROR);
                      }
                      else
                      {
                        j = 0;
                        is_retry = true;
                      }
                    }
                  }
                }
                www?.Dispose();
              }
              www = (UnityWebRequest) null;
              loaded_assetBundle = (AssetBundle) null;
            }
            url = (string) null;
            filename = (string) null;
          }
        }
        while (is_retry);
        if (Object.op_Inequality((Object) asset_bundle, (Object) null))
        {
          m = 0;
          for (int n = load_num; m < n && (request.master != null || request.sameRequests != null) && !Object.op_Equality((Object) asset_bundle, (Object) null); ++m)
          {
            if (load_objects[m] == null)
            {
              if (this.onAsyncLoadQuery())
              {
                while (this.IsWaitAssetBundleLoadAsync())
                  yield return (object) null;
                this.loadingAssetCountFromAssetBundle++;
                asset_bundle_load_asset = asset_bundle.LoadAssetAsync(request.resourceNames[m]);
                asset_bundle.GetAllAssetNames();
                while (!((AsyncOperation) asset_bundle_load_asset).isDone)
                  yield return (object) null;
                load_objects[m] = ResourceObject.Get(request.category, request.resourceNames[m], asset_bundle_load_asset.asset);
                this.loadingAssetCountFromAssetBundle--;
                asset_bundle_load_asset = (AssetBundleRequest) null;
              }
              else
                load_objects[m] = ResourceObject.Get(request.category, request.resourceNames[m], asset_bundle.LoadAsset(request.resourceNames[m]));
              if (load_objects[m] != null)
                ++loaded_num;
            }
          }
        }
        if (Object.op_Inequality((Object) asset_bundle, (Object) null) && (request.downloadOnly || !use_package_cache && !request.cachePackage))
        {
          if (!use_package_cache && !use_package_cache_delay)
            this.cache.AddDelayUnloadAssetBundle(request.packageName, ref asset_bundle);
          load_package = (PackageObject) null;
          this.cache.ReleasePackageObjects(dependency_packages);
          dependency_packages = (BetterList<PackageObject>) null;
        }
        this.downloadList.Remove(request);
      }
      else
      {
        switch (ResourceDefine.types[(int) request.category])
        {
          case ResourceManager.CATEGORY_TYPE.SINGLE:
          case ResourceManager.CATEGORY_TYPE.PACK:
          case ResourceManager.CATEGORY_TYPE.HASH256:
            filename = string.Empty;
            break;
          default:
            filename = request.packageName + "/";
            break;
        }
        int num1;
        if (request.category == RESOURCE_CATEGORY.PLAYER_HIGH_RESO_TEX)
        {
          string str = request.packageName.Substring(0, 3);
          string s = request.packageName.Substring(3, 2);
          if (!str.Equals("WEP"))
          {
            num1 = int.Parse(s) / 10 * 10;
            s = num1.ToString("00");
          }
          filename = $"{str}/{str}{s}/{filename}";
        }
        for (int i = 0; i < load_num; num1 = ++i)
        {
          if (load_objects[i] == null)
          {
            ResourceRequest req;
            if (!request.internalMode && request.category != RESOURCE_CATEGORY.UI)
            {
              int num2 = ResourceManager.enableLoadDirect ? 1 : 0;
              ResourceManager.enableLoadDirect = true;
              Coroutine coroutine = this.StartCoroutine(this.LoadDirect(request, filename + request.resourceNames[i], (Action<Object>) (o =>
              {
                if (!Object.op_Inequality(o, (Object) null))
                  return;
                load_objects[i] = ResourceObject.Get(request.category, request.resourceNames[i], o);
              })));
              ResourceManager.enableLoadDirect = num2 != 0;
              yield return (object) coroutine;
            }
            else if (request.category == RESOURCE_CATEGORY.UI)
            {
              string path = request.uiDep.path;
              if (this.onAsyncLoadQuery() || request.downloadOnly)
              {
                req = Resources.LoadAsync(path);
                while (!((AsyncOperation) req).isDone)
                  yield return (object) null;
                load_objects[i] = ResourceObject.Get(request.category, request.resourceNames[i], req.asset);
                req = (ResourceRequest) null;
              }
              else
                load_objects[i] = ResourceObject.Get(request.category, request.resourceNames[i], Resources.Load(path));
            }
            else
            {
              string str = $"Internal/internal__{request.category.ToString()}__{request.resourceNames[i]}";
              if (this.onAsyncLoadQuery() || request.downloadOnly)
              {
                req = Resources.LoadAsync(str);
                while (!((AsyncOperation) req).isDone)
                  yield return (object) null;
                load_objects[i] = ResourceObject.Get(request.category, request.resourceNames[i], req.asset);
                req = (ResourceRequest) null;
              }
              else
                load_objects[i] = ResourceObject.Get(request.category, request.resourceNames[i], Resources.Load(str));
            }
            if (request.master != null || request.sameRequests != null)
            {
              if (load_objects[i] != null)
                num1 = ++loaded_num;
            }
            else
              break;
          }
        }
        if (!use_package_cache && request.cachePackage)
          load_package = PackageObject.Get(request.packageName, (object) "dummy");
        filename = (string) null;
      }
    }
    int num3 = 0;
    if (request.master != null)
      ++num3;
    if (request.sameRequests != null)
    {
      int index = 0;
      for (int count = request.sameRequests.Count; index < count; ++index)
      {
        if (request.sameRequests[index].master != null)
          ++num3;
      }
    }
    bool flag1 = Object.op_Inequality((Object) asset_bundle, (Object) null);
    if (num3 > 0)
    {
      if (this.cache.objectCaches != null && request.enableCache && load_num > 0)
      {
        if (object_cache_category == null)
        {
          object_cache_category = this.cache.objectCaches[(int) request.category];
          if (object_cache_category == null)
          {
            object_cache_category = new StringKeyTable<ResourceObject>();
            this.cache.objectCaches[(int) request.category] = object_cache_category;
          }
        }
        for (int index = 0; index < load_num; ++index)
        {
          if (!use_object_caches[index] && load_objects[index] != null)
            object_cache_category.Add(request.resourceNames[index], load_objects[index]);
        }
      }
      if (!request.downloadOnly && request.cachePackage && !string.IsNullOrEmpty(request.packageName) && !use_package_cache && load_package != null && this.cache.GetCachedPackage(request.packageName) == null)
      {
        this.cache.packageCaches.Add(MonoBehaviourSingleton<GoGameResourceManager>.I.GetBundleNameWithoutVariant(request.packageName), load_package);
        flag1 = false;
      }
      if (load_package != null && load_objects != null)
      {
        int index = 0;
        for (int length = load_objects.Length; index < length; ++index)
        {
          if (load_objects[index] != null)
          {
            load_package.refCount += num3;
            load_objects[index].package = load_package;
          }
        }
        if (dependency_packages != null)
        {
          int i = 0;
          for (int size = dependency_packages.size; i < size; ++i)
          {
            PackageObject packageObject = dependency_packages[i];
            if (packageObject != null)
            {
              packageObject.refCount += num3;
              load_package.linkPackages.Add(packageObject);
            }
          }
        }
      }
    }
    if (flag1)
      this.cache.AddDelayUnloadAssetBundle(request.packageName, ref asset_bundle);
    if (request.downloadOnly)
      loaded_num = load_num;
    if (request.master != null || request.sameRequests != null)
    {
      if (loaded_num == load_num)
      {
        if (request.onAtlasComplete != null && request.category == RESOURCE_CATEGORY.UI_ATLAS)
          request.onAtlasComplete(request, load_objects);
        if (request.onComplate != null)
          request.onComplate(request, load_objects);
        if (request.sameRequests != null)
        {
          int index = 0;
          for (int count = request.sameRequests.Count; index < count; ++index)
          {
            ResourceManager.LoadRequest sameRequest = request.sameRequests[index];
            if (sameRequest.onAtlasComplete != null && sameRequest.category == RESOURCE_CATEGORY.UI_ATLAS)
              sameRequest.onAtlasComplete(sameRequest, load_objects);
            if (sameRequest.master != null && sameRequest.onComplate != null)
              sameRequest.onComplate(sameRequest, load_objects);
          }
        }
      }
      else
      {
        if (request.onError != null)
          request.onError(request, ResourceManager.ERROR_CODE.NOT_FOUND);
        if (request.sameRequests != null)
        {
          int index = 0;
          for (int count = request.sameRequests.Count; index < count; ++index)
          {
            ResourceManager.LoadRequest sameRequest = request.sameRequests[index];
            if (sameRequest.master != null && sameRequest.onError != null)
              sameRequest.onError(sameRequest, ResourceManager.ERROR_CODE.NOT_FOUND);
          }
        }
      }
    }
    this.RemoveRequest(ref request);
  }

  private void AddAssetBundleRequest(ResourceManager.LoadRequest request)
  {
    this.loadRequests.Add(request);
    if (this.onAddRequest != null)
      this.onAddRequest();
    this.StartCoroutine(this.DoLoadAssetBundle(request));
  }

  private IEnumerator DoLoadAssetBundle(ResourceManager.LoadRequest request)
  {
    if (MonoBehaviourSingleton<AppMain>.I.isExecutingClearMemory)
    {
      ++this.stayCount;
      while (MonoBehaviourSingleton<AppMain>.I.isExecutingClearMemory)
      {
        if (!request.IsValid())
        {
          --this.stayCount;
          this.BreakRequest(ref request);
          yield break;
        }
        yield return (object) null;
      }
      --this.stayCount;
    }
    int j;
    int m;
    if (request.dependencyRequests != null)
    {
      j = 0;
      m = request.dependencyRequests.Count;
      while (request.IsValid())
      {
        bool flag = false;
        ResourceManager.LoadRequest dependencyRequest = request.dependencyRequests[j];
        int index = 0;
        for (int count = this.loadRequests.Count; index < count; ++index)
        {
          if (this.loadRequests[index] == dependencyRequest)
          {
            flag = true;
            break;
          }
        }
        if (flag)
        {
          yield return (object) null;
        }
        else
        {
          ++j;
          if (j == m)
            goto label_19;
        }
      }
      this.BreakRequest(ref request);
      yield break;
    }
label_19:
    BetterList<PackageObject> dependency_packages = this.cache.GetDependencyPackages(request.dependencyRequests);
    if (!request.internalMode)
    {
      while (this.isDownloadError)
      {
        yield return (object) null;
        if (!request.IsValid())
        {
          this.BreakRequest(ref request);
          yield break;
        }
      }
    }
    PackageObject load_package = (PackageObject) null;
    AssetBundle asset_bundle = (AssetBundle) null;
    ResourceObject[] load_objects = (ResourceObject[]) null;
    bool use_package_cache = false;
    bool use_package_cache_delay = false;
    bool[] use_object_caches = (bool[]) null;
    int load_num = request.resourceNames != null ? request.resourceNames.Length : 0;
    int loaded_num = 0;
    bool package_only = load_num == 0;
    if (request.downloadOnly)
      package_only = true;
    if (load_num > 0)
    {
      load_objects = new ResourceObject[load_num];
      use_object_caches = new bool[load_num];
    }
    StringKeyTable<ResourceObject> object_cache_category = request.category != RESOURCE_CATEGORY.MAX ? this.cache.objectCaches[(int) request.category] : (StringKeyTable<ResourceObject>) null;
    load_package = this.cache.GetCachedPackage(request.packageName);
    if (load_package != null)
    {
      asset_bundle = load_package.obj as AssetBundle;
      use_package_cache = true;
    }
    if (Object.op_Equality((Object) asset_bundle, (Object) null))
      load_package = (PackageObject) null;
    if (loaded_num < load_num && this.cache.objectCaches != null && this.cache.systemCaches != null && object_cache_category != null)
    {
      int index1 = 0;
      for (int index2 = load_num; index1 < index2; ++index1)
      {
        if (load_objects[index1] == null)
        {
          string res_name = request.resourceNames[index1];
          load_objects[index1] = object_cache_category.Get(res_name);
          if (load_objects[index1] == null)
            load_objects[index1] = this.cache.systemCaches.Find((Predicate<ResourceObject>) (o => o.obj.name == res_name));
          if (load_objects[index1] != null)
          {
            use_object_caches[index1] = true;
            ++loaded_num;
          }
        }
      }
    }
    AssetBundleRequest asset_bundle_load_asset;
    if (loaded_num < load_num && !request.downloadOnly && load_package != null && Object.op_Inequality((Object) asset_bundle, (Object) null))
    {
      m = 0;
      for (j = load_num; m < j && (request.master != null || request.sameRequests != null); ++m)
      {
        if (load_objects[m] == null)
        {
          if (this.onAsyncLoadQuery())
          {
            while (this.IsWaitAssetBundleLoadAsync())
              yield return (object) null;
            this.loadingAssetCountFromAssetBundle++;
            asset_bundle_load_asset = asset_bundle.LoadAssetAsync(request.resourceNames[m]);
            while (!((AsyncOperation) asset_bundle_load_asset).isDone)
              yield return (object) null;
            load_objects[m] = ResourceObject.Get(request.category, request.resourceNames[m], asset_bundle_load_asset.asset);
            this.loadingAssetCountFromAssetBundle--;
            asset_bundle_load_asset = (AssetBundleRequest) null;
          }
          else
            load_objects[m] = ResourceObject.Get(request.category, request.resourceNames[m], asset_bundle.LoadAsset(request.resourceNames[m]));
          if (load_objects[m] != null)
            ++loaded_num;
        }
      }
    }
    if (load_package == null && (loaded_num < load_num || package_only))
    {
      bool flag1 = request.category != RESOURCE_CATEGORY.ASSETBUNDLEINFO ? Object.op_Inequality((Object) this.manifest, (Object) null) : Object.op_Inequality((Object) this.sizeInfoManifest, (Object) null);
      string url;
      if (request.category == RESOURCE_CATEGORY.MAX || ResourceManager.isDownloadAssets & flag1 && (!request.internalMode || request.downloadOnly))
      {
        while (this.downloadList.size >= ResourceManager.MAX_DL_COUNT || this.CheckPackageDownloading(request))
        {
          yield return (object) null;
          if (!request.IsValid())
          {
            this.BreakRequest(ref request);
            yield break;
          }
        }
        this.downloadList.Add(request);
        j = 0;
        bool is_retry = false;
        bool flag2;
        do
        {
          is_retry = false;
          if (!request.IsValid())
          {
            this.BreakRequest(ref request);
            yield break;
          }
          if (!request.downloadOnly)
          {
            load_package = this.cache.GetCachedPackage(request.packageName);
            if (load_package != null)
            {
              asset_bundle = load_package.obj as AssetBundle;
              use_package_cache = true;
            }
          }
          if (load_package != null || Object.op_Equality((Object) asset_bundle, (Object) null))
          {
            while (this.isDownloadError && !request.internalMode)
            {
              yield return (object) null;
              if (!request.IsValid())
              {
                this.BreakRequest(ref request);
                yield break;
              }
            }
            Hash128 hash = request.hash;
            RESOURCE_CATEGORY? category = MonoBehaviourSingleton<GoGameResourceManager>.I.GetCategory(request.packageName);
            if (request.eventAsset)
            {
              url = $"{this.downloadEventUrl}{request.packageName}?v={request.hash.ToString()}";
            }
            else
            {
              string variantName = MonoBehaviourSingleton<GoGameResourceManager>.I.GetVariantName(new RESOURCE_CATEGORY?(!category.HasValue ? RESOURCE_CATEGORY.MAX : category.Value));
              if (request.packageName.Contains("-sd"))
                url = $"{this.downloadURL}{request.packageName}?v={request.hash.ToString()}";
              else
                url = $"{this.downloadURL}{request.packageName}{variantName}?v={request.hash.ToString()}";
            }
            if (request.category == RESOURCE_CATEGORY.ASSETBUNDLEINFO)
              url = $"{this.downloadSizeInfoURL}{request.packageName}?v={request.hash.ToString()}";
            Path.GetFileName(request.packageName);
            string str = url;
            if (request.IsValid())
            {
              int num = Caching.IsVersionCached(str, hash) ? 1 : 0;
              Error error_code = Error.None;
              AssetBundle loaded_assetBundle = (AssetBundle) null;
              UnityWebRequest www = (UnityWebRequest) null;
              UnityWebRequest unityWebRequest = www = UnityWebRequestAssetBundle.GetAssetBundle(str, hash, 0U);
              try
              {
                if (num == 0)
                {
                  www.timeout = (int) ResourceManager.WWW_TIME_OUT;
                  www.SendWebRequest();
                  request.progressObject = (object) www;
                  double downloadProgress1 = (double) www.downloadProgress;
                  while (!www.isDone)
                  {
                    yield return (object) null;
                    double downloadProgress2 = (double) www.downloadProgress;
                    if (!request.IsValid())
                    {
                      www.Dispose();
                      this.BreakRequest(ref request);
                      flag2 = false;
                      goto label_157;
                    }
                  }
                  if (!www.isDone || www.isNetworkError || www.isHttpError)
                    error_code = Error.AssetLoadFailed;
                  else if (www.error == null)
                  {
                    if (!request.downloadOnly)
                    {
                      CrashlyticsReporter.SetLoadingBundle(url);
                      loaded_assetBundle = DownloadHandlerAssetBundle.GetContent(www);
                      if (Object.op_Equality((Object) loaded_assetBundle, (Object) null))
                      {
                        load_package = this.cache.GetCachedPackage(request.packageName);
                        if (load_package != null)
                        {
                          loaded_assetBundle = load_package.obj as AssetBundle;
                          asset_bundle = loaded_assetBundle;
                          use_package_cache = true;
                          use_package_cache_delay = true;
                        }
                      }
                      CrashlyticsReporter.SetLoadingBundle("");
                    }
                    www.Dispose();
                  }
                  else
                    error_code = !www.error.Contains("404") ? Error.AssetLoadFailed : Error.AssetNotFound;
                }
                else if (!request.downloadOnly)
                {
                  if (Object.op_Equality((Object) loaded_assetBundle, (Object) null))
                  {
                    load_package = this.cache.GetCachedPackage(request.packageName);
                    if (load_package != null)
                    {
                      loaded_assetBundle = load_package.obj as AssetBundle;
                      asset_bundle = loaded_assetBundle;
                      use_package_cache = true;
                      use_package_cache_delay = true;
                    }
                  }
                  if (Object.op_Equality((Object) loaded_assetBundle, (Object) null))
                    loaded_assetBundle = this.GetLoadedAssetBundle(request.packageName);
                  if (Object.op_Equality((Object) loaded_assetBundle, (Object) null))
                  {
                    yield return (object) www.SendWebRequest();
                    if (this.onAsyncLoadQuery())
                      yield return (object) null;
                    if (!www.isDone || www.isNetworkError || www.isHttpError)
                      error_code = Error.AssetLoadFailed;
                    else if (www.error == null)
                    {
                      if (!request.downloadOnly)
                      {
                        CrashlyticsReporter.SetLoadingBundle(url);
                        loaded_assetBundle = DownloadHandlerAssetBundle.GetContent(www);
                        if (Object.op_Equality((Object) loaded_assetBundle, (Object) null))
                        {
                          load_package = this.cache.GetCachedPackage(request.packageName);
                          if (load_package != null)
                          {
                            loaded_assetBundle = load_package.obj as AssetBundle;
                            asset_bundle = loaded_assetBundle;
                            use_package_cache = true;
                            use_package_cache_delay = true;
                          }
                        }
                        CrashlyticsReporter.SetLoadingBundle("");
                      }
                      www.Dispose();
                    }
                    else
                      error_code = !www.error.Contains("404") ? Error.AssetLoadFailed : Error.AssetNotFound;
                  }
                }
                if (request.progressObject != null)
                  request.progressObject = ResourceManager.PROGRESS_COMPLATE;
                while (this.isDownloadError && !request.internalMode)
                {
                  yield return (object) null;
                  if (!request.IsValid())
                  {
                    this.cache.AddDelayUnloadAssetBundle(request.packageName, ref loaded_assetBundle);
                    www.Dispose();
                    this.BreakRequest(ref request);
                    flag2 = false;
                    goto label_157;
                  }
                }
                if (error_code == Error.None)
                {
                  if (!request.downloadOnly && (request.cachePackage || !package_only || request.category == RESOURCE_CATEGORY.MAX))
                  {
                    load_package = PackageObject.Get(request.packageName, (object) loaded_assetBundle);
                    asset_bundle = load_package.obj as AssetBundle;
                  }
                  www?.Dispose();
                  if (!use_package_cache_delay)
                    use_package_cache = false;
                }
                else
                {
                  load_package = this.cache.GetCachedPackage(request.packageName);
                  if (load_package != null)
                  {
                    asset_bundle = load_package.obj as AssetBundle;
                    use_package_cache = true;
                  }
                  if (load_package != null || Object.op_Equality((Object) asset_bundle, (Object) null))
                  {
                    ++j;
                    if (ResourceManager.autoRetry)
                      j = 0;
                    if (j < 3)
                    {
                      yield return (object) new WaitForSeconds(1f);
                      is_retry = true;
                    }
                    else
                    {
                      if (error_code == Error.AssetLoadFailed)
                        Log.Error(LOG.RESOURCE, "{0}:{1}", (object) error_code.ToString(), (object) url);
                      else
                        Log.Error("{0}:{1}:{2}:{3}", (object) error_code.ToString(), (object) request.category, (object) request.packageName, (object) url);
                      if (this.isDownloadError)
                      {
                        while (this.isDownloadError)
                        {
                          if (!request.IsValid())
                          {
                            this.BreakRequest(ref request);
                            flag2 = false;
                            goto label_157;
                          }
                        }
                        j = 0;
                        is_retry = true;
                      }
                      else
                      {
                        this.isDownloadError = true;
                        m = 0;
                        if (request.IsValid() && this.onDownloadErrorQuery != null)
                        {
                          while (this.onDownloadErrorQuery != null)
                          {
                            m = this.onDownloadErrorQuery(true, error_code);
                            if (m != 0)
                              yield return (object) null;
                            else
                              break;
                          }
                          while (m == 0 && this.onDownloadErrorQuery != null)
                          {
                            m = this.onDownloadErrorQuery(false, error_code);
                            yield return (object) null;
                          }
                          if (m == -1)
                          {
                            this.cache.AddDelayUnloadAssetBundle(request.packageName, ref loaded_assetBundle);
                            this.BreakRequest(ref request);
                            flag2 = false;
                            goto label_157;
                          }
                        }
                        this.isDownloadError = false;
                        if (m != 1)
                        {
                          if (request.onError != null)
                            request.onError(request, ResourceManager.ERROR_CODE.WWW_ERROR);
                        }
                        else
                        {
                          j = 0;
                          is_retry = true;
                        }
                      }
                    }
                  }
                  www?.Dispose();
                }
                goto label_58;
label_157:
                goto label_247;
              }
              finally
              {
                ((IDisposable) unityWebRequest)?.Dispose();
              }
label_58:
              unityWebRequest = (UnityWebRequest) null;
              loaded_assetBundle = (AssetBundle) null;
              www = (UnityWebRequest) null;
            }
            url = (string) null;
          }
        }
        while (is_retry);
        goto label_160;
label_247:
        return flag2;
label_160:
        if (Object.op_Inequality((Object) asset_bundle, (Object) null))
        {
          m = 0;
          for (int n = load_num; m < n && (request.master != null || request.sameRequests != null) && !Object.op_Equality((Object) asset_bundle, (Object) null); ++m)
          {
            if (load_objects[m] == null)
            {
              if (this.onAsyncLoadQuery())
              {
                while (this.IsWaitAssetBundleLoadAsync())
                  yield return (object) null;
                this.loadingAssetCountFromAssetBundle++;
                asset_bundle_load_asset = asset_bundle.LoadAssetAsync(request.resourceNames[m]);
                while (!((AsyncOperation) asset_bundle_load_asset).isDone)
                  yield return (object) null;
                load_objects[m] = ResourceObject.Get(request.category, request.resourceNames[m], asset_bundle_load_asset.asset);
                this.loadingAssetCountFromAssetBundle--;
                asset_bundle_load_asset = (AssetBundleRequest) null;
              }
              else
                load_objects[m] = ResourceObject.Get(request.category, request.resourceNames[m], asset_bundle.LoadAsset(request.resourceNames[m]));
              if (load_objects[m] != null)
                ++loaded_num;
            }
          }
        }
        if (Object.op_Inequality((Object) asset_bundle, (Object) null) && (request.downloadOnly || !use_package_cache && !request.cachePackage))
        {
          if (!use_package_cache && !use_package_cache_delay)
            this.cache.AddDelayUnloadAssetBundle(request.packageName, ref asset_bundle);
          load_package = (PackageObject) null;
          this.cache.ReleasePackageObjects(dependency_packages);
          dependency_packages = (BetterList<PackageObject>) null;
        }
        this.downloadList.Remove(request);
      }
      else
      {
        switch (ResourceDefine.types[(int) request.category])
        {
          case ResourceManager.CATEGORY_TYPE.SINGLE:
          case ResourceManager.CATEGORY_TYPE.PACK:
          case ResourceManager.CATEGORY_TYPE.HASH256:
            url = string.Empty;
            break;
          default:
            url = request.packageName + "/";
            break;
        }
        int num1;
        if (request.category == RESOURCE_CATEGORY.PLAYER_HIGH_RESO_TEX)
        {
          string str = request.packageName.Substring(0, 3);
          string s = request.packageName.Substring(3, 2);
          if (!str.Equals("WEP"))
          {
            num1 = int.Parse(s) / 10 * 10;
            s = num1.ToString("00");
          }
          url = $"{str}/{str}{s}/{url}";
        }
        for (int i = 0; i < load_num; num1 = ++i)
        {
          if (load_objects[i] == null)
          {
            ResourceRequest req;
            if (!request.internalMode && request.category != RESOURCE_CATEGORY.UI)
            {
              int num2 = ResourceManager.enableLoadDirect ? 1 : 0;
              ResourceManager.enableLoadDirect = true;
              Coroutine coroutine = this.StartCoroutine(this.LoadDirect(request, url + request.resourceNames[i], (Action<Object>) (o =>
              {
                if (!Object.op_Inequality(o, (Object) null))
                  return;
                load_objects[i] = ResourceObject.Get(request.category, request.resourceNames[i], o);
              })));
              ResourceManager.enableLoadDirect = num2 != 0;
              yield return (object) coroutine;
            }
            else if (request.category == RESOURCE_CATEGORY.UI)
            {
              string path = request.uiDep.path;
              if (this.onAsyncLoadQuery() || request.downloadOnly)
              {
                req = Resources.LoadAsync(path);
                while (!((AsyncOperation) req).isDone)
                  yield return (object) null;
                load_objects[i] = ResourceObject.Get(request.category, request.resourceNames[i], req.asset);
                req = (ResourceRequest) null;
              }
              else
                load_objects[i] = ResourceObject.Get(request.category, request.resourceNames[i], Resources.Load(path));
            }
            else
            {
              string str = $"Internal/internal__{request.category.ToString()}__{request.resourceNames[i]}";
              if (this.onAsyncLoadQuery() || request.downloadOnly)
              {
                req = Resources.LoadAsync(str);
                while (!((AsyncOperation) req).isDone)
                  yield return (object) null;
                load_objects[i] = ResourceObject.Get(request.category, request.resourceNames[i], req.asset);
                req = (ResourceRequest) null;
              }
              else
                load_objects[i] = ResourceObject.Get(request.category, request.resourceNames[i], Resources.Load(str));
            }
            if (request.master != null || request.sameRequests != null)
            {
              if (load_objects[i] != null)
                num1 = ++loaded_num;
            }
            else
              break;
          }
        }
        if (!use_package_cache && request.cachePackage)
          load_package = PackageObject.Get(request.packageName, (object) "dummy");
        url = (string) null;
      }
    }
    int num3 = 0;
    if (request.master != null)
      ++num3;
    if (request.sameRequests != null)
    {
      int index = 0;
      for (int count = request.sameRequests.Count; index < count; ++index)
      {
        if (request.sameRequests[index].master != null)
          ++num3;
      }
    }
    if (num3 > 0)
    {
      if (this.cache.objectCaches != null && request.enableCache && load_num > 0)
      {
        if (object_cache_category == null)
        {
          object_cache_category = this.cache.objectCaches[(int) request.category];
          if (object_cache_category == null)
          {
            object_cache_category = new StringKeyTable<ResourceObject>();
            this.cache.objectCaches[(int) request.category] = object_cache_category;
          }
        }
        for (int index = 0; index < load_num; ++index)
        {
          if (!use_object_caches[index] && load_objects[index] != null)
            object_cache_category.Add(request.resourceNames[index], load_objects[index]);
        }
      }
      if (!request.downloadOnly && request.cachePackage && !string.IsNullOrEmpty(request.packageName) && !use_package_cache && load_package != null && this.cache.GetCachedPackage(request.packageName) == null)
        this.cache.packageCaches.Add(MonoBehaviourSingleton<GoGameResourceManager>.I.GetBundleNameWithoutVariant(request.packageName), load_package);
      if (load_package != null && load_objects != null)
      {
        int index = 0;
        for (int length = load_objects.Length; index < length; ++index)
        {
          if (load_objects[index] != null)
          {
            load_package.refCount += num3;
            load_objects[index].package = load_package;
          }
        }
        if (dependency_packages != null)
        {
          int i = 0;
          for (int size = dependency_packages.size; i < size; ++i)
          {
            PackageObject packageObject = dependency_packages[i];
            if (packageObject != null)
            {
              packageObject.refCount += num3;
              load_package.linkPackages.Add(packageObject);
            }
          }
        }
      }
    }
    if (request.downloadOnly)
      loaded_num = load_num;
    if ((request.master != null || request.sameRequests != null) && loaded_num == load_num)
    {
      if (request.onAtlasComplete != null && request.category == RESOURCE_CATEGORY.UI_ATLAS)
        request.onAtlasComplete(request, load_objects);
      if (request.onComplate != null)
        request.onComplate(request, load_objects);
      if (request.sameRequests != null)
      {
        int index = 0;
        for (int count = request.sameRequests.Count; index < count; ++index)
        {
          ResourceManager.LoadRequest sameRequest = request.sameRequests[index];
          if (sameRequest.onAtlasComplete != null && sameRequest.category == RESOURCE_CATEGORY.UI_ATLAS)
            sameRequest.onAtlasComplete(sameRequest, load_objects);
          if (sameRequest.master != null && sameRequest.onComplate != null)
            sameRequest.onComplate(sameRequest, load_objects);
        }
      }
    }
    if (request.unloadAsset && Object.op_Inequality((Object) asset_bundle, (Object) null))
      asset_bundle.Unload(false);
    this.RemoveRequest(ref request);
  }

  private AssetBundle GetLoadedAssetBundle(string name)
  {
    foreach (AssetBundle loadedAssetBundle in AssetBundle.GetAllLoadedAssetBundles())
    {
      if (((Object) loadedAssetBundle).name == name)
        return loadedAssetBundle;
    }
    return (AssetBundle) null;
  }

  private void BreakRequest(ref ResourceManager.LoadRequest request)
  {
    this.RemoveRequest(ref request);
  }

  private void RemoveRequest(ref ResourceManager.LoadRequest request)
  {
    this.downloadList.Remove(request);
    request.Cancel();
    if (request.sameRequests != null)
    {
      int index = 0;
      for (int count = request.sameRequests.Count; index < count; ++index)
        request.sameRequests[index].Cancel();
    }
    this.loadRequests.Remove(request);
    request = (ResourceManager.LoadRequest) null;
    if (this.onRemoveRequest == null)
      return;
    this.onRemoveRequest();
  }

  private bool IsWaitAssetBundleLoadAsync()
  {
    return this.loadingAssetCountFromAssetBundle >= this.loadingAssetCountLimit;
  }

  public void Cancel(object master)
  {
    int index1 = 0;
    for (int count1 = this.loadRequests.Count; index1 < count1; ++index1)
    {
      ResourceManager.LoadRequest loadRequest = this.loadRequests[index1];
      if (loadRequest.sameRequests != null)
      {
        int index2 = 0;
        for (int count2 = loadRequest.sameRequests.Count; index2 < count2; ++index2)
        {
          if (loadRequest.sameRequests[index2].master == master)
            loadRequest.sameRequests[index2].Cancel();
        }
      }
      if (loadRequest.master == master)
        loadRequest.Cancel();
    }
  }

  public void CancelAll()
  {
    int index1 = 0;
    for (int count1 = this.loadRequests.Count; index1 < count1; ++index1)
    {
      ResourceManager.LoadRequest loadRequest = this.loadRequests[index1];
      loadRequest.Cancel();
      if (loadRequest.sameRequests != null)
      {
        int index2 = 0;
        for (int count2 = loadRequest.sameRequests.Count; index2 < count2; ++index2)
          loadRequest.sameRequests[index2].Cancel();
      }
    }
  }

  public bool isLoading => this.loadRequests.Count > 0;

  protected override void OnDetachServant(DisableNotifyMonoBehaviour servant)
  {
    this.Cancel((object) servant);
  }

  public bool ExistRequest(RESOURCE_CATEGORY category, string resource_name)
  {
    int index = 0;
    for (int count = this.loadRequests.Count; index < count; ++index)
    {
      ResourceManager.LoadRequest loadRequest = this.loadRequests[index];
      if (loadRequest.category == category && loadRequest.packageName == resource_name)
        return true;
    }
    return false;
  }

  public bool ExistRequest(
    RESOURCE_CATEGORY category,
    string package_name,
    string[] resource_names)
  {
    int index1 = 0;
    for (int count = this.loadRequests.Count; index1 < count; ++index1)
    {
      ResourceManager.LoadRequest loadRequest = this.loadRequests[index1];
      if (loadRequest.category == category && loadRequest.packageName == package_name)
      {
        if (loadRequest.resourceNames == null && resource_names == null)
          return true;
        if (loadRequest.resourceNames.Length == resource_names.Length)
        {
          int index2 = 0;
          int length = loadRequest.resourceNames.Length;
          while (index2 < length && !(loadRequest.resourceNames[index2] != resource_names[index2]))
            ++index2;
          if (index2 == length)
            return true;
        }
      }
    }
    return false;
  }

  public bool IsCached(RESOURCE_CATEGORY category, string packageName)
  {
    if (Object.op_Inequality((Object) this.manifest, (Object) null))
    {
      Hash128 assetBundleHash = this.manifest.GetAssetBundleHash(MonoBehaviourSingleton<GoGameResourceManager>.I.GetFullBundleName(category.ToAssetBundleName(packageName)));
      if (((Hash128) ref assetBundleHash).isValid)
        return Caching.IsVersionCached(this.GetAssetBundleURL(category, packageName, assetBundleHash), assetBundleHash);
    }
    return this.IsCached(category.ToAssetBundleName(packageName));
  }

  private string GetAssetBundleURL(RESOURCE_CATEGORY category, string packageName, Hash128 hash)
  {
    string lower = packageName.ToLower();
    string variantName = MonoBehaviourSingleton<GoGameResourceManager>.I.GetVariantName(new RESOURCE_CATEGORY?(category));
    string assetBundleUrl;
    if (lower.Contains("-sd"))
      assetBundleUrl = $"{this.downloadURL}{lower}?v={hash.ToString()}";
    else
      assetBundleUrl = $"{this.downloadURL}{lower}{variantName}?v={hash.ToString()}";
    if (category == RESOURCE_CATEGORY.ASSETBUNDLEINFO)
      assetBundleUrl = $"{this.downloadSizeInfoURL}{packageName}?v={hash.ToString()}";
    return assetBundleUrl;
  }

  public bool IsCached(string dependency)
  {
    return !Object.op_Equality((Object) this.manifest, (Object) null) && ResourceManager.IsVersionCached(Path.GetFileName(dependency), this.manifest.GetAssetBundleHash(MonoBehaviourSingleton<GoGameResourceManager>.I.GetFullBundleName(dependency)));
  }

  public bool IsCachedDependencies(RESOURCE_CATEGORY category, string packageName)
  {
    foreach (string dependency in this.GetALLDependenciesFileName(category.ToAssetBundleName(packageName)))
    {
      if (!this.IsCached(dependency))
        return false;
    }
    return true;
  }

  public bool IsCachedWithDependencies(RESOURCE_CATEGORY category, string packageName)
  {
    return this.IsCached(category, packageName) && this.IsCachedDependencies(category, packageName);
  }

  public string[] GetALLDependenciesFileName(string assetName)
  {
    HashSet<string> stringSet = new HashSet<string>();
    if (Object.op_Inequality((Object) this.manifest, (Object) null))
    {
      foreach (string allDependency in this.manifest.GetAllDependencies(MonoBehaviourSingleton<GoGameResourceManager>.I.GetFullBundleName(assetName)))
        stringSet.UnionWith((IEnumerable<string>) this.GetALLDependenciesFileName(MonoBehaviourSingleton<GoGameResourceManager>.I.GetBundleNameWithoutVariant(allDependency)));
    }
    string[] array = new string[stringSet.Count];
    stringSet.CopyTo(array);
    return array;
  }

  public static bool IsVersionCached(string filename, Hash128 hash)
  {
    return File.Exists(ResourceManager.GetCachePath(filename, hash));
  }

  public static string GetCachePath(string filename, Hash128 hash)
  {
    return Path.Combine(ResourceManager.GetCacheDir(filename), $"{filename}.{hash.ToString()}");
  }

  public static string GetCacheDir(string filename)
  {
    return Path.Combine(ResourceManager.cacheDir, ((byte) (filename.GetHashCode() % 256 /*0x0100*/)).ToString("X2"));
  }

  private Error SaveAssetBundle(string filename, Hash128 hash, byte[] bytes)
  {
    if (bytes.Length == 0)
      return Error.AssetLoadFailed;
    Error error = Error.None;
    string cacheDir = ResourceManager.GetCacheDir(filename);
    if (Directory.Exists(cacheDir))
    {
      foreach (string file in Directory.GetFiles(cacheDir, filename + "*"))
        File.Delete(file);
    }
    else
      Directory.CreateDirectory(cacheDir);
    string cachePath = ResourceManager.GetCachePath(filename, hash);
    try
    {
      File.WriteAllBytes(cachePath, bytes);
    }
    catch
    {
      error = Error.AssetSaveFailed;
    }
    return error;
  }

  public static IEnumerator ClearCache()
  {
    string[] dirs = Directory.GetDirectories(ResourceManager.cacheDir);
    int i;
    for (i = 0; i < dirs.Length; ++i)
    {
      string path = dirs[i];
      try
      {
        Directory.Delete(path, true);
      }
      catch
      {
      }
      if (i % 2 == 0)
        yield return (object) null;
    }
    if (Directory.Exists(ResourceManager.cachingDir))
    {
      string[] cdirs = Directory.GetDirectories(ResourceManager.cachingDir);
      for (i = 0; i < cdirs.Length; ++i)
      {
        string path = cdirs[i];
        try
        {
          Directory.Delete(path, true);
        }
        catch
        {
        }
        if (i % 2 == 0)
          yield return (object) null;
      }
      cdirs = (string[]) null;
    }
  }

  public Object LoadDirect(RESOURCE_CATEGORY category, string resource_name)
  {
    if (!ResourceManager.enableLoadDirect)
    {
      Log.Error(LOG.RESOURCE, $"can not LoadDirect. {category.ToString()} : {resource_name}");
      return (Object) null;
    }
    Object @object = (Object) null;
    string[] subPath = ResourceDefine.subPaths[(int) category];
    int index = 0;
    for (int length = subPath.Length; index < length; ++index)
    {
      @object = ExternalResources.Load<Object>(subPath[index] + resource_name);
      if (Object.op_Inequality(@object, (Object) null))
        break;
    }
    return @object;
  }

  public IEnumerator LoadDirect(
    ResourceManager.LoadRequest request,
    string resource_name,
    Action<Object> onComplete)
  {
    RESOURCE_CATEGORY category = request.category;
    Object load_object = (Object) null;
    if (!ResourceManager.enableLoadDirect)
    {
      Log.Error(LOG.RESOURCE, $"can not LoadDirect. {category.ToString()} : {resource_name}");
      onComplete(load_object);
    }
    else
    {
      string[] paths = ResourceDefine.subPaths[(int) category];
      int i = 0;
      for (int n = paths.Length; i < n; ++i)
      {
        string path = paths[i] + resource_name;
        if (path.Contains("StreamingAssets"))
        {
          path = Application.streamingAssetsPath + path.Replace("StreamingAssets", "").ToLower();
          UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(path);
          www.SendWebRequest();
          request.progressObject = (object) www;
          while (!www.isDone)
            yield return (object) null;
          request.progressObject = ResourceManager.PROGRESS_COMPLATE;
          load_object = (Object) DownloadHandlerAssetBundle.GetContent(www);
          www.Dispose();
          www = (UnityWebRequest) null;
        }
        else if (this.onAsyncLoadQuery() || request.downloadOnly)
        {
          yield return (object) ExternalResources.LoadAsync<Object>(path, (Action<ResourceRequest>) (progress => request.progressObject = (object) progress), (Action<Object>) (asset =>
          {
            request.progressObject = ResourceManager.PROGRESS_COMPLATE;
            load_object = asset;
          }));
        }
        else
        {
          yield return (object) null;
          load_object = ExternalResources.Load<Object>(path);
        }
        if (!Object.op_Inequality(load_object, (Object) null))
          path = (string) null;
        else
          break;
      }
      onComplete(load_object);
    }
  }

  public static Object LoadDirect(
    RESOURCE_CATEGORY category,
    string package_name,
    string resource_name)
  {
    if (!ResourceManager.enableLoadDirect)
    {
      Log.Error(LOG.RESOURCE, $"can not LoadDirect. {category.ToString()} : {package_name} : {resource_name}");
      return (Object) null;
    }
    Object @object = (Object) null;
    string[] subPath = ResourceDefine.subPaths[(int) category];
    int index = 0;
    for (int length = subPath.Length; index < length; ++index)
    {
      @object = ExternalResources.Load<Object>($"{subPath[index]}{package_name}/{resource_name}");
      if (Object.op_Inequality(@object, (Object) null))
        break;
    }
    return @object;
  }

  public Object __LoadDirect(RESOURCE_CATEGORY category, string resource_name)
  {
    bool enableLoadDirect = ResourceManager.enableLoadDirect;
    ResourceManager.enableLoadDirect = true;
    Object @object = this.LoadDirect(category, resource_name);
    ResourceManager.enableLoadDirect = enableLoadDirect;
    return @object;
  }

  public Object __LoadDirect(RESOURCE_CATEGORY category, string package_name, string resource_name)
  {
    bool enableLoadDirect = ResourceManager.enableLoadDirect;
    ResourceManager.enableLoadDirect = true;
    Object @object = ResourceManager.LoadDirect(category, package_name, resource_name);
    ResourceManager.enableLoadDirect = enableLoadDirect;
    return @object;
  }

  public static IEnumerator UnleaseAllLoadedAssetBundle()
  {
    foreach (AssetBundle loadedAssetBundle in AssetBundle.GetAllLoadedAssetBundles())
    {
      loadedAssetBundle.Unload(false);
      yield return (object) null;
    }
  }

  public enum CATEGORY_TYPE
  {
    SINGLE,
    FOLDER,
    PACK,
    HASH256,
  }

  public class LoadRequest
  {
    public object master;
    public RESOURCE_CATEGORY category;
    public string packageName;
    public string[] resourceNames;
    public object userData;
    public bool cachePackage;
    public bool enableCache;
    public bool downloadOnly;
    public bool internalMode;
    public bool eventAsset;
    public bool unloadAsset;
    public object progressObject;
    public ResourceManager.LoadComplateDelegate onComplate;
    public ResourceManager.LoadErrorDelegate onError;
    public ResourceManager.LoadComplateDelegate onAtlasComplete;
    public List<ResourceManager.LoadRequest> sameRequests;
    public List<ResourceManager.LoadRequest> dependencyRequests;
    public Hash128 hash;
    public UIDependency uiDep;

    public void Clear()
    {
      this.master = (object) null;
      this.category = RESOURCE_CATEGORY.MAX;
      this.packageName = (string) null;
      this.resourceNames = (string[]) null;
      this.onComplate = (ResourceManager.LoadComplateDelegate) null;
      this.onError = (ResourceManager.LoadErrorDelegate) null;
      this.userData = (object) null;
      this.cachePackage = false;
      this.enableCache = true;
      this.downloadOnly = false;
      this.internalMode = false;
      this.progressObject = (object) null;
      this.sameRequests = (List<ResourceManager.LoadRequest>) null;
      this.dependencyRequests = (List<ResourceManager.LoadRequest>) null;
      this.uiDep = (UIDependency) null;
    }

    public void Cancel()
    {
      this.master = (object) null;
      this.onComplate = (ResourceManager.LoadComplateDelegate) null;
      this.onError = (ResourceManager.LoadErrorDelegate) null;
      this.progressObject = (object) null;
      if (this.sameRequests != null)
        return;
      this.userData = (object) null;
      this.enableCache = false;
      this.cachePackage = false;
    }

    public void Setup()
    {
      if (this.category == RESOURCE_CATEGORY.ASSETBUNDLEINFO)
      {
        if (this.packageName == "EnemyPredownloadTable")
          this.packageName = "enemypredownloadtable.dat";
        else
          this.packageName = "assetbundleinfo.dat";
      }
      else if (this.category != RESOURCE_CATEGORY.MAX)
        this.packageName = this.category.ToAssetBundleName(this.packageName);
      else
        this.packageName = this.packageName.ToLower();
    }

    public bool IsValid()
    {
      if (this.master != null)
        return true;
      if (this.sameRequests != null)
      {
        int index = 0;
        for (int count = this.sameRequests.Count; index < count; ++index)
        {
          if (this.sameRequests[index].master != null)
            return true;
        }
      }
      return false;
    }

    public int GetValidCount()
    {
      int validCount = 0;
      if (this.master != null)
        ++validCount;
      if (this.sameRequests != null)
      {
        int index = 0;
        for (int count = this.sameRequests.Count; index < count; ++index)
        {
          if (this.sameRequests[index].master != null)
            ++validCount;
        }
      }
      return validCount;
    }

    public float GetProgress()
    {
      if (this.progressObject is ResourceRequest)
        return ((AsyncOperation) (this.progressObject as ResourceRequest)).progress;
      if (this.progressObject is UnityWebRequest || this.progressObject is UnityWebRequestAssetBundle)
        return (this.progressObject as UnityWebRequest).downloadProgress;
      return this.progressObject == ResourceManager.PROGRESS_COMPLATE ? 1f : 0.0f;
    }
  }

  public enum ERROR_CODE
  {
    NOT_FOUND,
    WWW_ERROR,
  }

  public delegate void LoadComplateDelegate(
    ResourceManager.LoadRequest request,
    ResourceObject[] objs);

  public delegate void LoadErrorDelegate(
    ResourceManager.LoadRequest request,
    ResourceManager.ERROR_CODE error_node);
}
