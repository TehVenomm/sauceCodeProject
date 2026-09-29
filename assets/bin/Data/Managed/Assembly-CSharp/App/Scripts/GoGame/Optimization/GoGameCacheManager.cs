// Decompiled with JetBrains decompiler
// Type: App.Scripts.GoGame.Optimization.GoGameCacheManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
namespace App.Scripts.GoGame.Optimization;

public class GoGameCacheManager : MonoBehaviourSingleton<GoGameCacheManager>
{
  private Transform CacheContainer;
  private GoGameCacheManager.PlayerModelData _selfPlayerModelData;
  private GoGameCacheManager.PlayerModelTransform _selfPlayerModelTransform;
  protected Self _self;
  private readonly Dictionary<RESOURCE_CATEGORY, Dictionary<string, Object>> _selfPlayerResourceCache = new Dictionary<RESOURCE_CATEGORY, Dictionary<string, Object>>();
  private static Dictionary<string, Transform> objCaches = new Dictionary<string, Transform>();
  private static Dictionary<string, SceneParameter> lightMapCaches = new Dictionary<string, SceneParameter>();
  private static Texture2D texture2D = (Texture2D) null;

  protected override void Awake()
  {
    base.Awake();
    if (Object.op_Inequality((Object) this.CacheContainer, (Object) null))
      Object.DestroyImmediate((Object) ((Component) this.CacheContainer).gameObject);
    this.CacheContainer = new GameObject("CacheContainer").transform;
    this.CacheContainer.parent = this._transform;
  }

  protected override void OnDestroySingleton()
  {
    base.OnDestroySingleton();
    this.Delete();
  }

  public void Delete()
  {
    foreach (KeyValuePair<string, Transform> objCach in GoGameCacheManager.objCaches)
    {
      if (Object.op_Inequality((Object) objCach.Value, (Object) null))
        Object.DestroyImmediate((Object) ((Component) objCach.Value).gameObject);
    }
    GoGameCacheManager.objCaches.Clear();
  }

  public void CacheSelfPlayerResource(
    RESOURCE_CATEGORY resourceCategory,
    string resourceName,
    Object loadedObject)
  {
    Dictionary<string, Object> dictionary;
    if (!this._selfPlayerResourceCache.ContainsKey(resourceCategory))
    {
      dictionary = new Dictionary<string, Object>();
      this._selfPlayerResourceCache.Add(resourceCategory, dictionary);
    }
    else
      dictionary = this._selfPlayerResourceCache[resourceCategory];
    if (!dictionary.ContainsKey(resourceName))
    {
      dictionary.Add(resourceName, loadedObject);
    }
    else
    {
      Object @object = dictionary[resourceName];
      dictionary[resourceName] = loadedObject;
      Object.Destroy(@object);
    }
  }

  public bool IsSelfPlayerResourceCached(RESOURCE_CATEGORY resourceCategory, string resourceName)
  {
    return resourceName != null && this._selfPlayerResourceCache.ContainsKey(resourceCategory) && this._selfPlayerResourceCache[resourceCategory].ContainsKey(resourceName);
  }

  public bool IsSelfPlayerResourceCached(
    RESOURCE_CATEGORY resourceCategory,
    string packageName,
    string[] resourceNames)
  {
    return packageName != null ? ((IEnumerable<string>) resourceNames).Aggregate<string, bool>(true, (Func<bool, string, bool>) ((current, resourceName) => ((current ? 1 : 0) | (!this._selfPlayerResourceCache.ContainsKey(resourceCategory) ? 0 : (this._selfPlayerResourceCache[resourceCategory].ContainsKey($"{packageName}/{resourceName}") ? 1 : 0))) != 0)) : ((IEnumerable<string>) resourceNames).Aggregate<string, bool>(true, (Func<bool, string, bool>) ((current, resourceName) => ((current ? 1 : 0) | (!this._selfPlayerResourceCache.ContainsKey(resourceCategory) ? 0 : (this._selfPlayerResourceCache[resourceCategory].ContainsKey(resourceName) ? 1 : 0))) != 0));
  }

  public Object GetSelfPlayerResourceCache(RESOURCE_CATEGORY resourceCategory, string resourceName)
  {
    return !this.IsSelfPlayerResourceCached(resourceCategory, resourceName) ? (Object) null : this._selfPlayerResourceCache[resourceCategory][resourceName];
  }

  public void CacheSelfPlayerModel()
  {
    if (Object.op_Inequality((Object) this._self, (Object) null))
    {
      this._self.ClearStoreEffect();
      this._self.ClearEvent();
    }
    if (this._selfPlayerModelData == null)
      return;
    if (!string.IsNullOrEmpty(this._selfPlayerModelData.wepnName))
    {
      this.RemoveUnusedObjects(this._selfPlayerModelTransform.wepR, this._selfPlayerModelTransform.wepRChild);
      this.RemoveUnusedObjects(this._selfPlayerModelTransform.wepL, this._selfPlayerModelTransform.wepLChild);
      GoGameCacheManager.CacheObj(this._selfPlayerModelData.wepnName + "R", this._selfPlayerModelTransform.wepR);
      GoGameCacheManager.CacheObj(this._selfPlayerModelData.wepnName + "L", this._selfPlayerModelTransform.wepL);
    }
    if (!string.IsNullOrEmpty(this._selfPlayerModelData.faceName))
    {
      this.RemoveUnusedObjects(this._selfPlayerModelTransform.face, this._selfPlayerModelTransform.faceChild);
      GoGameCacheManager.CacheObj(this._selfPlayerModelData.faceName, this._selfPlayerModelTransform.face);
    }
    if (!string.IsNullOrEmpty(this._selfPlayerModelData.hairName))
    {
      this.RemoveUnusedObjects(this._selfPlayerModelTransform.hair, this._selfPlayerModelTransform.hairChild);
      GoGameCacheManager.CacheObj(this._selfPlayerModelData.hairName, this._selfPlayerModelTransform.hair);
    }
    if (!string.IsNullOrEmpty(this._selfPlayerModelData.bodyName))
    {
      this.RemoveUnusedObjects(this._selfPlayerModelTransform.body, this._selfPlayerModelTransform.bodyChild);
      GoGameCacheManager.CacheObj(this._selfPlayerModelData.bodyName, this._selfPlayerModelTransform.body);
    }
    if (!string.IsNullOrEmpty(this._selfPlayerModelData.headName))
    {
      this.RemoveUnusedObjects(this._selfPlayerModelTransform.head, this._selfPlayerModelTransform.headChild);
      GoGameCacheManager.CacheObj(this._selfPlayerModelData.headName, this._selfPlayerModelTransform.head);
    }
    if (!string.IsNullOrEmpty(this._selfPlayerModelData.armName))
    {
      this.RemoveUnusedObjects(this._selfPlayerModelTransform.arm, this._selfPlayerModelTransform.armChild);
      GoGameCacheManager.CacheObj(this._selfPlayerModelData.armName, this._selfPlayerModelTransform.arm);
    }
    if (string.IsNullOrEmpty(this._selfPlayerModelData.legName))
      return;
    this.RemoveUnusedObjects(this._selfPlayerModelTransform.leg, this._selfPlayerModelTransform.legChild);
    GoGameCacheManager.CacheObj(this._selfPlayerModelData.legName, this._selfPlayerModelTransform.leg);
  }

  private void RemoveUnusedObjects(Transform obj, Transform[] group)
  {
    if (Object.op_Equality((Object) obj, (Object) null) || group == null)
      return;
    Transform[] componentsInChildren = ((Component) obj).GetComponentsInChildren<Transform>();
    List<Transform> transformList = new List<Transform>();
    for (int index1 = 0; index1 < componentsInChildren.Length; ++index1)
    {
      bool flag = false;
      if (!Object.op_Equality((Object) componentsInChildren[index1], (Object) null))
      {
        for (int index2 = 0; index2 < group.Length; ++index2)
        {
          if (!Object.op_Equality((Object) group[index2], (Object) null) && Object.op_Equality((Object) componentsInChildren[index1], (Object) group[index2]))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          transformList.Add(componentsInChildren[index1]);
      }
    }
    for (int index = transformList.Count - 1; index >= 0; --index)
    {
      Transform transform = transformList[index];
      transformList.Remove(transform);
      if (Object.op_Inequality((Object) transform, (Object) null))
        Object.Destroy((Object) ((Component) transform).gameObject);
    }
  }

  private void SafeDestroy(Object obj)
  {
    if (!Object.op_Inequality(obj, (Object) null))
      return;
    Object.Destroy(obj);
  }

  private void SafeDestroy(Transform transform)
  {
    if (!Object.op_Inequality((Object) transform, (Object) null))
      return;
    Object.Destroy((Object) ((Component) transform).gameObject);
  }

  public void ClearCacheSelfPlayerModelNotUse(GoGameCacheManager.PlayerModelData newData)
  {
    if (this._selfPlayerModelData == null)
      return;
    if (!string.IsNullOrEmpty(this._selfPlayerModelData.wepnName) && this._selfPlayerModelData.wepnName != newData.wepnName)
    {
      this.SafeDestroy(GoGameCacheManager.RetrieveObj(this._selfPlayerModelData.wepnName + "R"));
      this.SafeDestroy(GoGameCacheManager.RetrieveObj(this._selfPlayerModelData.wepnName + "L"));
    }
    if (!string.IsNullOrEmpty(this._selfPlayerModelData.faceName) && this._selfPlayerModelData.faceName != newData.faceName)
      this.SafeDestroy(GoGameCacheManager.RetrieveObj(this._selfPlayerModelData.faceName));
    if (!string.IsNullOrEmpty(this._selfPlayerModelData.hairName) && this._selfPlayerModelData.hairName != newData.hairName)
      this.SafeDestroy(GoGameCacheManager.RetrieveObj(this._selfPlayerModelData.hairName));
    if (!string.IsNullOrEmpty(this._selfPlayerModelData.bodyName) && this._selfPlayerModelData.bodyName != newData.bodyName)
      this.SafeDestroy(GoGameCacheManager.RetrieveObj(this._selfPlayerModelData.bodyName));
    if (!string.IsNullOrEmpty(this._selfPlayerModelData.headName) && this._selfPlayerModelData.headName != newData.headName)
      this.SafeDestroy(GoGameCacheManager.RetrieveObj(this._selfPlayerModelData.headName));
    if (!string.IsNullOrEmpty(this._selfPlayerModelData.armName) && this._selfPlayerModelData.armName != newData.armName)
      this.SafeDestroy(GoGameCacheManager.RetrieveObj(this._selfPlayerModelData.armName));
    if (string.IsNullOrEmpty(this._selfPlayerModelData.legName) || !(this._selfPlayerModelData.legName != newData.legName))
      return;
    this.SafeDestroy(GoGameCacheManager.RetrieveObj(this._selfPlayerModelData.legName));
  }

  public void SaveCacheSelfPlayerModel(Self self)
  {
    this._selfPlayerModelData = new GoGameCacheManager.PlayerModelData()
    {
      faceName = self.loader.faceCacheName,
      hairName = self.loader.hairCacheName,
      bodyName = self.loader.bodyCacheName,
      headName = self.loader.headCacheName,
      armName = self.loader.armCacheName,
      legName = self.loader.legCacheName,
      wepnName = self.loader.weaponCacheName
    };
    this._selfPlayerModelTransform = new GoGameCacheManager.PlayerModelTransform()
    {
      wepR = self.loader.wepR,
      wepL = self.loader.wepL,
      face = self.loader.face,
      hair = self.loader.hair,
      body = self.loader.body,
      head = self.loader.head,
      arm = self.loader.arm,
      leg = self.loader.leg
    };
    if (Object.op_Inequality((Object) this._selfPlayerModelTransform.wepR, (Object) null))
      this._selfPlayerModelTransform.wepRChild = ((Component) this._selfPlayerModelTransform.wepR).GetComponentsInChildren<Transform>();
    if (Object.op_Inequality((Object) this._selfPlayerModelTransform.wepL, (Object) null))
      this._selfPlayerModelTransform.wepLChild = ((Component) this._selfPlayerModelTransform.wepL).GetComponentsInChildren<Transform>();
    if (Object.op_Inequality((Object) this._selfPlayerModelTransform.face, (Object) null))
      this._selfPlayerModelTransform.faceChild = ((Component) this._selfPlayerModelTransform.face).GetComponentsInChildren<Transform>();
    if (Object.op_Inequality((Object) this._selfPlayerModelTransform.hair, (Object) null))
      this._selfPlayerModelTransform.headChild = ((Component) this._selfPlayerModelTransform.hair).GetComponentsInChildren<Transform>();
    if (Object.op_Inequality((Object) this._selfPlayerModelTransform.body, (Object) null))
      this._selfPlayerModelTransform.bodyChild = ((Component) this._selfPlayerModelTransform.body).GetComponentsInChildren<Transform>();
    if (Object.op_Inequality((Object) this._selfPlayerModelTransform.head, (Object) null))
      this._selfPlayerModelTransform.headChild = ((Component) this._selfPlayerModelTransform.head).GetComponentsInChildren<Transform>();
    if (Object.op_Inequality((Object) this._selfPlayerModelTransform.arm, (Object) null))
      this._selfPlayerModelTransform.armChild = ((Component) this._selfPlayerModelTransform.arm).GetComponentsInChildren<Transform>();
    if (Object.op_Inequality((Object) this._selfPlayerModelTransform.leg, (Object) null))
      this._selfPlayerModelTransform.legChild = ((Component) this._selfPlayerModelTransform.leg).GetComponentsInChildren<Transform>();
    this._self = self;
  }

  public static bool ShouldCacheStage(string id)
  {
    return MonoBehaviourSingleton<GlobalSettingsManager>.IsValid() && MonoBehaviourSingleton<GlobalSettingsManager>.I.stageCache.Contains(id);
  }

  public static bool ShouldCacheSky(string id)
  {
    return MonoBehaviourSingleton<GlobalSettingsManager>.IsValid() && MonoBehaviourSingleton<GlobalSettingsManager>.I.skyboxCaches.Contains(id);
  }

  public static bool ShouldCacheEffect(string id)
  {
    return MonoBehaviourSingleton<GlobalSettingsManager>.IsValid() && MonoBehaviourSingleton<GlobalSettingsManager>.I.stageEffectCaches.Contains(id);
  }

  public static bool HasCacheObj(string id)
  {
    return !string.IsNullOrEmpty(id) && GoGameCacheManager.objCaches.ContainsKey(id) && Object.op_Inequality((Object) GoGameCacheManager.objCaches[id], (Object) null);
  }

  public static void CacheObj(string id, Transform obj)
  {
    if (Object.op_Equality((Object) obj, (Object) null))
      return;
    ((Component) obj).gameObject.SetActive(false);
    obj.parent = MonoBehaviourSingleton<GoGameCacheManager>.I.CacheContainer;
    if (GoGameCacheManager.objCaches.ContainsKey(id))
      GoGameCacheManager.objCaches[id] = obj;
    else
      GoGameCacheManager.objCaches.Add(id, obj);
  }

  public static bool CacheEffectIfNeed(string id, Transform obj)
  {
    if (!GoGameCacheManager.ShouldCacheEffect(id))
      return false;
    GoGameCacheManager.CacheObj(id, obj);
    return true;
  }

  public static Transform RetrieveObj(string id, Transform parent = null)
  {
    if (!GoGameCacheManager.objCaches.ContainsKey(id))
      return (Transform) null;
    Transform objCach = GoGameCacheManager.objCaches[id];
    ((Component) objCach).gameObject.SetActive(true);
    if (Object.op_Inequality((Object) parent, (Object) null))
      objCach.parent = parent;
    GoGameCacheManager.objCaches.Remove(id);
    return objCach;
  }

  public static void CacheLightMap(string id, Transform obj)
  {
    SceneParameter sceneParameter = ((Component) obj).GetComponent<SceneParameter>();
    if (Object.op_Equality((Object) sceneParameter, (Object) null))
      sceneParameter = ((Component) obj).gameObject.AddComponent<SceneParameter>();
    LightmapData[] lightmaps = LightmapSettings.lightmaps;
    if (lightmaps != null && lightmaps.Length != 0)
    {
      int length = lightmaps.Length;
      Texture2D[] texture2DArray1 = new Texture2D[length];
      Texture2D[] texture2DArray2 = new Texture2D[length];
      for (int index = 0; index < length; ++index)
      {
        texture2DArray1[index] = lightmaps[index].lightmapColor;
        texture2DArray2[index] = lightmaps[index].lightmapDir;
      }
      sceneParameter.lightmapsFar = texture2DArray1;
      sceneParameter.lightmapsNear = texture2DArray2;
      sceneParameter.lightmapMode = LightmapSettings.lightmapsMode;
    }
    sceneParameter.lightProbes = LightmapSettings.lightProbes;
    if (GoGameCacheManager.lightMapCaches.ContainsKey(id))
      GoGameCacheManager.lightMapCaches[id] = sceneParameter;
    else
      GoGameCacheManager.lightMapCaches.Add(id, sceneParameter);
  }

  public static SceneParameter RetrieveLightMap(string id)
  {
    return GoGameCacheManager.lightMapCaches.ContainsKey(id) ? GoGameCacheManager.lightMapCaches[id] : (SceneParameter) null;
  }

  public class PlayerModelData
  {
    public string faceName;
    public string hairName;
    public string bodyName;
    public string headName;
    public string armName;
    public string legName;
    public string wepnName;
    public HashSet<string> accUIDs;
  }

  public class PlayerModelTransform
  {
    public Transform wepR;
    public Transform wepL;
    public Transform face;
    public Transform hair;
    public Transform body;
    public Transform head;
    public Transform arm;
    public Transform leg;
    public Transform[] wepRChild;
    public Transform[] wepLChild;
    public Transform[] faceChild;
    public Transform[] hairChild;
    public Transform[] bodyChild;
    public Transform[] headChild;
    public Transform[] armChild;
    public Transform[] legChild;
  }
}
