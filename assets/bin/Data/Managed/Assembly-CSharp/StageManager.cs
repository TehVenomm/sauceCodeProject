// Decompiled with JetBrains decompiler
// Type: StageManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using App.Scripts.GoGame.Optimization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#nullable disable
public class StageManager : MonoBehaviourSingleton<StageManager>
{
  private bool isLoadingStage;
  private bool isLoadingBackgoundImage;
  private Transform terrainTransform;
  private float terrainDataSizeInvX;
  private float terrainDataSizeInvZ;
  private bool isLoadingStageObject;
  private bool isLoadingSkyObject;
  private Coroutine loadEffectCoroutine;
  private Transform currentStageContainer;
  private List<Vector3> insideChipList = new List<Vector3>();

  public bool isLoading => this.isLoadingStage || this.isLoadingBackgoundImage;

  public Terrain terrain { get; private set; }

  public string currentStageName { get; private set; }

  public StageTable.StageData currentStageData { get; private set; }

  public Transform stageObject { get; private set; }

  public Transform skyObject { get; private set; }

  public Transform rootEffect { get; private set; }

  public Transform cameraLinkEffect { get; private set; }

  public Transform cameraLinkEffectY0 { get; private set; }

  public int backgroundImageID { get; private set; }

  public Transform backgroundImage { get; private set; }

  public bool isValidInside { get; private set; }

  public SceneSettingsManager.InsideColliderData insideColliderData { get; private set; }

  private IEnumerator LoadStage(string id, string load_scene_name, StageTable.StageData data)
  {
    this.isLoadingStageObject = true;
    if (GoGameCacheManager.HasCacheObj(id))
      load_scene_name += "_lightmap";
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    EffectObject.wait = true;
    if (ResourceManager.internalMode)
      load_scene_name = $"internal__STAGE_SCENE__{load_scene_name}";
    else if (ResourceManager.isDownloadAssets)
    {
      loadingQueue.Load(RESOURCE_CATEGORY.STAGE_SCENE, load_scene_name, (string[]) null, true);
      yield return (object) loadingQueue.Wait();
    }
    AsyncOperation ao = SceneManager.LoadSceneAsync(load_scene_name);
    while (!ao.isDone)
      yield return (object) null;
    if (GoGameCacheManager.HasCacheObj(id))
    {
      this.currentStageContainer = GoGameCacheManager.RetrieveObj(id, this._transform);
      SceneSettingsManager componentInChildren = ((Component) this.currentStageContainer).GetComponentInChildren<SceneSettingsManager>();
      if (Object.op_Inequality((Object) componentInChildren, (Object) null))
      {
        componentInChildren.Self();
        componentInChildren.InitializeScene();
        this.stageObject = ((Component) componentInChildren).transform;
      }
    }
    else
    {
      this.currentStageContainer = new GameObject($"{"cache"}_{load_scene_name}").transform;
      this.currentStageContainer.parent = this._transform;
      Scene activeScene = SceneManager.GetActiveScene();
      foreach (GameObject rootGameObject in ((Scene) ref activeScene).GetRootGameObjects())
        rootGameObject.transform.parent = this.currentStageContainer;
      if (MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
        this.stageObject = ((Component) MonoBehaviourSingleton<SceneSettingsManager>.I).transform;
    }
    if (!MonoBehaviourSingleton<GlobalSettingsManager>.I.stageCache.Contains(data.scene))
    {
      PackageObject packageObject = MonoBehaviourSingleton<ResourceManager>.I.cache.PopCachedPackage(RESOURCE_CATEGORY.STAGE_SCENE.ToAssetBundleName(load_scene_name));
      AssetBundle assetBundle = packageObject != null ? packageObject.obj as AssetBundle : (AssetBundle) null;
      if (Object.op_Inequality((Object) assetBundle, (Object) null))
        assetBundle.Unload(false);
    }
    bool flag = id.StartsWith("FI");
    if (Object.op_Inequality((Object) this.stageObject, (Object) null) & flag && (!MonoBehaviourSingleton<SceneSettingsManager>.IsValid() || !MonoBehaviourSingleton<SceneSettingsManager>.I.forceFogON))
      StageManager.ChangeLightShader(this._transform);
    if (MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
    {
      MonoBehaviourSingleton<SceneSettingsManager>.I.attributeID = data.attributeID;
      SceneParameter component = ((Component) MonoBehaviourSingleton<SceneSettingsManager>.I).GetComponent<SceneParameter>();
      if (Object.op_Inequality((Object) component, (Object) null))
        component.Apply();
      if (flag && !MonoBehaviourSingleton<SceneSettingsManager>.I.forceFogON)
      {
        ShaderGlobal.fogColor = MonoBehaviourSingleton<SceneSettingsManager>.I.fogColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        ShaderGlobal.fogNear = MonoBehaviourSingleton<SceneSettingsManager>.I.linearFogStart = 0.0f;
        ShaderGlobal.fogFar = MonoBehaviourSingleton<SceneSettingsManager>.I.linearFogEnd = float.MaxValue;
        ShaderGlobal.fogNearLimit = MonoBehaviourSingleton<SceneSettingsManager>.I.limitFogStart = 0.0f;
        ShaderGlobal.fogFarLimit = MonoBehaviourSingleton<SceneSettingsManager>.I.limitFogEnd = 1f;
      }
      if (MonoBehaviourSingleton<SceneSettingsManager>.I.saveInsideCollider && MonoBehaviourSingleton<SceneSettingsManager>.I.insideColliderData != null && (MonoBehaviourSingleton<SceneSettingsManager>.I.insideColliderData.minX != 0 || MonoBehaviourSingleton<SceneSettingsManager>.I.insideColliderData.maxX != 0 || MonoBehaviourSingleton<SceneSettingsManager>.I.insideColliderData.minZ != 0 || MonoBehaviourSingleton<SceneSettingsManager>.I.insideColliderData.maxZ != 0))
        this.isValidInside = true;
      if (this.isValidInside)
        this.insideColliderData = MonoBehaviourSingleton<SceneSettingsManager>.I.insideColliderData;
    }
    this.isLoadingStageObject = false;
  }

  private IEnumerator LoadSky(StageTable.StageData data)
  {
    this.isLoadingSkyObject = true;
    if (GoGameCacheManager.HasCacheObj(data.sky))
    {
      this.skyObject = GoGameCacheManager.RetrieveObj(data.sky, this._transform);
    }
    else
    {
      ResourceManager.enableCache = false;
      LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
      LoadObject lo_sky = (LoadObject) null;
      if (!string.IsNullOrEmpty(data.sky))
        lo_sky = load_queue.Load(RESOURCE_CATEGORY.STAGE_SKY, data.sky);
      ResourceManager.enableCache = true;
      while (load_queue.IsLoading())
        yield return (object) null;
      if (lo_sky != null)
        this.skyObject = ResourceUtility.Realizes(lo_sky.loadedObject, this._transform);
      load_queue = (LoadingQueue) null;
      lo_sky = (LoadObject) null;
    }
    this.isLoadingSkyObject = false;
  }

  private IEnumerator LoadEffect(StageTable.StageData data)
  {
    LoadingQueue load_queue = new LoadingQueue((MonoBehaviour) this);
    bool wait_load_root_effect = false;
    if (GoGameCacheManager.HasCacheObj(data.rootEffect))
    {
      this.rootEffect = GoGameCacheManager.RetrieveObj(data.rootEffect, this._transform);
    }
    else
    {
      wait_load_root_effect = true;
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, data.rootEffect);
    }
    bool wait_load_cameraLinkEffect = false;
    if (GoGameCacheManager.HasCacheObj(data.cameraLinkEffect))
    {
      this.cameraLinkEffect = GoGameCacheManager.RetrieveObj(data.cameraLinkEffect, this._transform);
    }
    else
    {
      wait_load_cameraLinkEffect = true;
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, data.cameraLinkEffect);
    }
    bool wait_load_cameraLinkEffectY0 = false;
    if (GoGameCacheManager.HasCacheObj(data.cameraLinkEffectY0))
    {
      this.cameraLinkEffectY0 = GoGameCacheManager.RetrieveObj(data.cameraLinkEffectY0, this._transform);
    }
    else
    {
      wait_load_cameraLinkEffectY0 = true;
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, data.cameraLinkEffectY0);
    }
    for (int index = 0; index < 8; ++index)
      load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_ACTION, data.useEffects[index]);
    while (load_queue.IsLoading())
      yield return (object) null;
    EffectObject.wait = false;
    if (wait_load_cameraLinkEffect)
      this.cameraLinkEffect = EffectManager.GetCameraLinkEffect(data.cameraLinkEffect, false, this._transform);
    if (wait_load_cameraLinkEffectY0)
      this.cameraLinkEffectY0 = EffectManager.GetCameraLinkEffect(data.cameraLinkEffectY0, true, this._transform);
    if (wait_load_root_effect)
      this.rootEffect = EffectManager.GetEffect(data.rootEffect, this._transform);
    while (this.isLoadingStageObject)
      yield return (object) null;
    if (MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
    {
      WeatherController weatherController = MonoBehaviourSingleton<SceneSettingsManager>.I.weatherController;
      if (Object.op_Inequality((Object) this.cameraLinkEffect, (Object) null))
        ((Component) this.cameraLinkEffect).gameObject.SetActive(!weatherController.cameraLinkEffectEnable);
      if (Object.op_Inequality((Object) this.cameraLinkEffectY0, (Object) null))
        ((Component) this.cameraLinkEffectY0).gameObject.SetActive(!weatherController.cameraLinkEffectY0Enable);
      if (MonoBehaviourSingleton<FieldManager>.IsValid() && MonoBehaviourSingleton<FieldManager>.I.fieldData != null)
        MonoBehaviourSingleton<SceneSettingsManager>.I.ActivateObjectsByProgress(MonoBehaviourSingleton<FieldManager>.I.fieldData.mapFlag);
    }
  }

  public bool LoadStage(string id)
  {
    if (this.isLoadingStage)
    {
      Log.Error(LOG.GAMESCENE, "can't load stage.");
      return false;
    }
    if (this.currentStageName == id)
      return false;
    this.StartCoroutine(this.LoadStageCoroutine(id));
    return true;
  }

  private IEnumerator LoadStageCoroutine(string id)
  {
    double realtimeSinceStartup1 = (double) Time.realtimeSinceStartup;
    this.isLoadingStage = true;
    this.insideColliderData = (SceneSettingsManager.InsideColliderData) null;
    this.isValidInside = false;
    this.UnloadStage();
    double realtimeSinceStartup2 = (double) Time.realtimeSinceStartup;
    Input.gyro.enabled = false;
    this.currentStageName = id;
    StageTable.StageData data = (StageTable.StageData) null;
    if (!string.IsNullOrEmpty(id))
    {
      double realtimeSinceStartup3 = (double) Time.realtimeSinceStartup;
      if (!Singleton<StageTable>.IsValid())
        yield break;
      data = Singleton<StageTable>.I.GetData(id);
      if (data == null)
        yield break;
      double realtimeSinceStartup4 = (double) Time.realtimeSinceStartup;
      this.StartCoroutine(this.LoadStage(id, data.scene, data));
      this.StartCoroutine(this.LoadSky(data));
      this.loadEffectCoroutine = this.StartCoroutine(this.LoadEffect(data));
      while (this.isLoadingStageObject || this.isLoadingSkyObject)
        yield return (object) null;
      double realtimeSinceStartup5 = (double) Time.realtimeSinceStartup;
    }
    else if (MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
      ShaderGlobal.lightProbe = true;
    ShaderGlobal.lightProbe = Object.op_Inequality((Object) LightmapSettings.lightProbes, (Object) null);
    this.currentStageData = data;
    this.isLoadingStage = false;
  }

  public void SetWeatherEffect(string effectName)
  {
    if (Object.op_Inequality((Object) this.cameraLinkEffect, (Object) null))
    {
      Object.Destroy((Object) ((Component) this.cameraLinkEffect).gameObject);
      this.cameraLinkEffect = (Transform) null;
    }
    this.cameraLinkEffect = EffectManager.GetCameraLinkEffect(effectName, false, this._transform);
    if (!MonoBehaviourSingleton<SceneSettingsManager>.IsValid())
      return;
    WeatherController weatherController = MonoBehaviourSingleton<SceneSettingsManager>.I.weatherController;
    if (!Object.op_Inequality((Object) this.cameraLinkEffect, (Object) null))
      return;
    weatherController.cameraLinkEffectEnable = true;
    ((Component) this.cameraLinkEffect).gameObject.SetActive(!weatherController.cameraLinkEffectEnable);
  }

  public void LoadBackgoundImage(int image_id)
  {
    if (this.isLoadingBackgoundImage)
    {
      Log.Error(LOG.GAMESCENE, "can't load stage.");
    }
    else
    {
      if (this.backgroundImageID == image_id)
        return;
      this.StartCoroutine(this.DoLoadBackgoundImage(image_id));
    }
  }

  private IEnumerator DoLoadBackgoundImage(int image_id)
  {
    this.isLoadingBackgoundImage = true;
    this.UnloadStage();
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    ResourceManager.enableCache = false;
    LoadObject lo_bg = loadingQueue.Load(RESOURCE_CATEGORY.STAGE_IMAGE, MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName().Contains("TutorialWeaponSelectTop") ? "BackgroundTutImage" : "BackgroundImage");
    LoadObject lo_tex = loadingQueue.Load(RESOURCE_CATEGORY.STAGE_IMAGE, ResourceName.GetBackgroundImage(image_id));
    ResourceManager.enableCache = true;
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    Transform transform = ResourceUtility.Realizes(lo_bg.loadedObject, this._transform, 0);
    ((Renderer) ((Component) transform).gameObject.GetComponent<MeshRenderer>()).material.mainTexture = lo_tex.loadedObject as Texture;
    ((Component) transform).gameObject.AddComponent<FixedViewQuad>();
    this.backgroundImageID = image_id;
    this.backgroundImage = transform;
    this.isLoadingBackgoundImage = false;
  }

  public void UnloadStage()
  {
    if (MonoBehaviourSingleton<EffectManager>.IsValid())
    {
      MonoBehaviourSingleton<EffectManager>.I.DeleteManagerChildrenEffects();
      MonoBehaviourSingleton<EffectManager>.I.ClearStocks();
    }
    if (AppMain.needClearMemory && MonoBehaviourSingleton<InstantiateManager>.IsValid())
      MonoBehaviourSingleton<InstantiateManager>.I.ClearStocks();
    if (this.loadEffectCoroutine != null)
    {
      this.StopCoroutine(this.loadEffectCoroutine);
      this.loadEffectCoroutine = (Coroutine) null;
    }
    if (Object.op_Inequality((Object) this.currentStageContainer, (Object) null))
    {
      int num = Object.op_Equality((Object) this.stageObject, (Object) null) ? 1 : 0;
      if (GoGameCacheManager.ShouldCacheStage(this.currentStageName))
      {
        GoGameCacheManager.CacheObj(this.currentStageName, this.currentStageContainer);
        if (Object.op_Inequality((Object) this.stageObject, (Object) null))
          ((Component) this.stageObject).GetComponent<SceneSettingsManager>().Remove();
      }
      else
        Object.DestroyImmediate((Object) ((Component) this.currentStageContainer).gameObject);
      this.currentStageContainer = (Transform) null;
      this.stageObject = (Transform) null;
      if (num == 0)
      {
        SceneManager.LoadScene("Empty");
        ShaderGlobal.Initialize();
        MonoBehaviourSingleton<GlobalSettingsManager>.I.ResetLightRot();
        MonoBehaviourSingleton<GlobalSettingsManager>.I.ResetAmbientColor();
        Input.gyro.enabled = true;
      }
    }
    if (Object.op_Inequality((Object) this.skyObject, (Object) null))
    {
      if (GoGameCacheManager.ShouldCacheSky(this.currentStageData.sky))
        GoGameCacheManager.CacheObj(this.currentStageData.sky, this.skyObject);
      else
        Object.Destroy((Object) ((Component) this.skyObject).gameObject);
      this.skyObject = (Transform) null;
    }
    if (Object.op_Inequality((Object) this.rootEffect, (Object) null))
    {
      if (GoGameCacheManager.ShouldCacheEffect(this.currentStageData.rootEffect))
        GoGameCacheManager.CacheObj(this.currentStageData.rootEffect, this.rootEffect);
      else
        Object.Destroy((Object) ((Component) this.rootEffect).gameObject);
      this.rootEffect = (Transform) null;
    }
    if (Object.op_Inequality((Object) this.cameraLinkEffect, (Object) null))
    {
      if (GoGameCacheManager.ShouldCacheEffect(this.currentStageData.cameraLinkEffect))
        GoGameCacheManager.CacheObj(this.currentStageData.cameraLinkEffect, this.cameraLinkEffect);
      else
        Object.Destroy((Object) ((Component) this.cameraLinkEffect).gameObject);
      this.cameraLinkEffect = (Transform) null;
    }
    if (Object.op_Inequality((Object) this.cameraLinkEffectY0, (Object) null))
    {
      if (GoGameCacheManager.ShouldCacheEffect(this.currentStageData.cameraLinkEffectY0))
        GoGameCacheManager.CacheObj(this.currentStageData.cameraLinkEffectY0, this.cameraLinkEffectY0);
      else
        Object.Destroy((Object) ((Component) this.cameraLinkEffectY0).gameObject);
      this.cameraLinkEffectY0 = (Transform) null;
    }
    this.currentStageName = (string) null;
    this.currentStageData = (StageTable.StageData) null;
    this.backgroundImageID = 0;
    if (!Object.op_Inequality((Object) this.backgroundImage, (Object) null))
      return;
    Object.Destroy((Object) ((Component) this.backgroundImage).gameObject);
    this.backgroundImage = (Transform) null;
  }

  public static float GetHeight(Vector3 pos)
  {
    if (!MonoBehaviourSingleton<StageManager>.IsValid())
      return 0.0f;
    StageManager i = MonoBehaviourSingleton<StageManager>.I;
    Terrain terrain = i.terrain;
    if (Object.op_Equality((Object) terrain, (Object) null))
      return 0.0f;
    Vector3 position = i.terrainTransform.position;
    return terrain.terrainData.GetInterpolatedHeight((pos.x - position.x) * i.terrainDataSizeInvX, (pos.z - position.z) * i.terrainDataSizeInvZ);
  }

  public static Vector3 FitHeight(Vector3 pos)
  {
    pos.y = StageManager.GetHeight(pos);
    return pos;
  }

  public static void ChangeLightShader(Transform root)
  {
    UIntKeyTable<Material> uintKeyTable = new UIntKeyTable<Material>();
    List<Renderer> rendererList = new List<Renderer>();
    ((Component) root).GetComponentsInChildren<Renderer>(true, rendererList);
    int index1 = 0;
    for (int count = rendererList.Count; index1 < count; ++index1)
    {
      Renderer renderer = rendererList[index1];
      Material[] sharedMaterials = renderer.sharedMaterials;
      int index2 = 0;
      for (int length = sharedMaterials.Length; index2 < length; ++index2)
      {
        Material material1 = sharedMaterials[index2];
        if (Object.op_Inequality((Object) material1, (Object) null) && Object.op_Inequality((Object) material1.shader, (Object) null))
        {
          Material material2 = uintKeyTable.Get((uint) ((Object) material1).GetInstanceID());
          if (Object.op_Inequality((Object) material2, (Object) null))
          {
            sharedMaterials[index2] = material2;
          }
          else
          {
            string name = ((Object) material1.shader).name;
            if (!name.EndsWith("__l"))
            {
              Shader shader = ResourceUtility.FindShader(name + "__l");
              if (Object.op_Inequality((Object) shader, (Object) null))
              {
                Material material3 = new Material(material1);
                material3.shader = shader;
                sharedMaterials[index2] = material3;
                uintKeyTable.Add((uint) ((Object) material1).GetInstanceID(), material3);
                continue;
              }
            }
            uintKeyTable.Add((uint) ((Object) material1).GetInstanceID(), material1);
          }
        }
      }
      renderer.sharedMaterials = sharedMaterials;
    }
    uintKeyTable.Clear();
    rendererList.Clear();
  }

  public bool CheckInsideFlags(int index_x, int index_z)
  {
    if (!this.isValidInside)
      return false;
    int num1 = this.insideColliderData.maxZ - this.insideColliderData.minZ + 1;
    int num2 = index_x * num1 + index_z;
    int index = num2 / 32 /*0x20*/;
    return index < this.insideColliderData.insideFlags.Count && (this.insideColliderData.insideFlags[index] & 1 << num2 % 32 /*0x20*/) != 0;
  }

  public bool CheckPosInside(Vector3 check_pos)
  {
    if (!this.isValidInside)
      return false;
    float num1 = (float) this.insideColliderData.minX * this.insideColliderData.chipSize;
    float num2 = (float) this.insideColliderData.maxX * this.insideColliderData.chipSize;
    if ((double) check_pos.x < (double) num1 || (double) check_pos.x >= (double) num2)
      return false;
    float num3 = (float) this.insideColliderData.minZ * this.insideColliderData.chipSize;
    float num4 = (float) this.insideColliderData.maxZ * this.insideColliderData.chipSize;
    return (double) check_pos.z >= (double) num3 && (double) check_pos.z < (double) num4 && this.CheckInsideFlags((int) (((double) check_pos.x - (double) num1) / (double) this.insideColliderData.chipSize), (int) (((double) check_pos.z - (double) num3) / (double) this.insideColliderData.chipSize));
  }

  public Vector3 ClampInside(Vector3 pos)
  {
    return this.insideColliderData == null ? pos : new Vector3(Mathf.Clamp(pos.x, (float) this.insideColliderData.minX, (float) this.insideColliderData.maxX), pos.y, Mathf.Clamp(pos.z, (float) this.insideColliderData.minZ, (float) this.insideColliderData.maxZ));
  }

  public Vector3 GetRandomPosByInsideInfo(Vector3 center, float max_radius, float min_radius = 0.0f)
  {
    bool valid = false;
    return this.GetRandomPosByInsideInfo(center, max_radius, min_radius, ref valid);
  }

  public Vector3 GetRandomPosByInsideInfo(
    Vector3 center,
    float max_radius,
    float min_radius,
    ref bool valid)
  {
    valid = false;
    if (!this.isValidInside || (double) max_radius < 0.0 || (double) min_radius < 0.0 || (double) min_radius > (double) max_radius)
      return center;
    float num1 = (float) ((double) this.insideColliderData.chipSize * 0.5 * 1.4199999570846558);
    if ((double) max_radius - (double) min_radius < (double) num1 && (double) (max_radius - num1) < 0.0)
      ;
    this.insideChipList.Clear();
    int num2 = Mathf.CeilToInt(max_radius * 2f / this.insideColliderData.chipSize) + 1;
    int num3 = Mathf.FloorToInt(center.x - max_radius);
    int num4 = Mathf.FloorToInt(center.z - max_radius);
    for (int index1 = 0; index1 < num2; ++index1)
    {
      for (int index2 = 0; index2 < num2; ++index2)
      {
        Vector3 zero = Vector3.zero;
        zero.x = (float) ((double) (num3 + index1) * (double) this.insideColliderData.chipSize + (double) this.insideColliderData.chipSize * 0.5);
        zero.z = (float) ((double) (num4 + index2) * (double) this.insideColliderData.chipSize + (double) this.insideColliderData.chipSize * 0.5);
        Vector3 vector3_1 = Vector3.op_Subtraction(zero, center);
        if ((double) ((Vector3) ref vector3_1).sqrMagnitude < (double) max_radius * (double) max_radius)
        {
          Vector3 vector3_2 = Vector3.op_Subtraction(zero, center);
          if ((double) ((Vector3) ref vector3_2).sqrMagnitude > (double) min_radius * (double) min_radius && this.CheckPosInside(zero))
            this.insideChipList.Add(zero);
        }
      }
    }
    if (this.insideChipList.Count <= 0)
      return center;
    Vector3 insideChip = this.insideChipList[(int) ((double) this.insideChipList.Count * (double) Random.value)];
    insideChip.x += this.insideColliderData.chipSize * (Random.value - 0.5f);
    insideChip.z += this.insideColliderData.chipSize * (Random.value - 0.5f);
    valid = true;
    return insideChip;
  }
}
