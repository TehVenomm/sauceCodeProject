// Decompiled with JetBrains decompiler
// Type: EffectManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using rhyme;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class EffectManager : MonoBehaviourSingleton<EffectManager>
{
  private List<EffectManager.OneShotInfo> infoList = new List<EffectManager.OneShotInfo>();
  private List<EffectManager.OneShotInfo> infoSecondList = new List<EffectManager.OneShotInfo>();
  public bool enableStock;
  public int maxStockCount = 64 /*0x40*/;
  private Transform stockParent;

  public static void ClearPoolObjects()
  {
    if (!MonoBehaviourSingleton<EffectManager>.IsValid())
      return;
    MonoBehaviourSingleton<EffectManager>.I.ClearStocks();
  }

  public static void Startup() => EeLSettings.Startup();

  private void Start() => this.ClearStocks();

  private void OnEnable()
  {
    Trail.onQueryDestroy += new Func<Trail, bool>(this.OnTrailQueryDestroy);
  }

  protected override void OnDisable()
  {
    int index1 = 0;
    for (int count = this.infoList.Count; index1 < count; ++index1)
    {
      EffectManager.OneShotInfo info = this.infoList[index1];
      rymTPool<EffectManager.OneShotInfo>.Release(ref info);
    }
    this.infoList.Clear();
    int index2 = 0;
    for (int count = this.infoSecondList.Count; index2 < count; ++index2)
    {
      EffectManager.OneShotInfo infoSecond = this.infoSecondList[index2];
      rymTPool<EffectManager.OneShotInfo>.Release(ref infoSecond);
    }
    this.infoSecondList.Clear();
    base.OnDisable();
    Trail.onQueryDestroy -= new Func<Trail, bool>(this.OnTrailQueryDestroy);
  }

  private bool OnTrailQueryDestroy(Trail trail)
  {
    return !this.StockOrDestroy(((Component) trail).gameObject, false);
  }

  private void LateUpdate()
  {
    if (this.infoList.Count > 0)
    {
      EffectManager.OneShotInfo info = this.infoList[0];
      EffectManager._OneShot(info.name, info.pos, info.rot, info.scale, info.onCreateCallBack);
      rymTPool<EffectManager.OneShotInfo>.Release(ref info);
      this.infoList.RemoveAt(0);
    }
    else
    {
      int count1 = this.infoSecondList.Count;
      if (count1 <= 0)
        return;
      float time = Time.time;
      int count2 = 0;
      for (int index = 0; index < count1; ++index)
      {
        EffectManager.OneShotInfo infoSecond = this.infoSecondList[index];
        if ((double) time - (double) infoSecond.time > 0.10000000149011612)
        {
          ++count2;
        }
        else
        {
          EffectManager._OneShot(infoSecond.name, infoSecond.pos, infoSecond.rot, infoSecond.scale, infoSecond.onCreateCallBack);
          ++count2;
          break;
        }
      }
      for (int index = 0; index < count2; ++index)
      {
        EffectManager.OneShotInfo infoSecond = this.infoSecondList[index];
        rymTPool<EffectManager.OneShotInfo>.Release(ref infoSecond);
      }
      this.infoSecondList.RemoveRange(0, count2);
    }
  }

  public bool StockOrDestroy(GameObject go, bool no_stock_to_destroy)
  {
    if (Object.op_Equality((Object) go, (Object) null))
      return false;
    if (this.enableStock)
    {
      EffectStock component = go.GetComponent<EffectStock>();
      if (Object.op_Inequality((Object) component, (Object) null) && !component.IsLoop())
      {
        component.Stock();
        go.transform.SetParent(this.stockParent, false);
        if (this.stockParent.childCount >= this.maxStockCount)
          Object.DestroyImmediate((Object) ((Component) this.stockParent.GetChild(0)).gameObject);
        return true;
      }
    }
    if (no_stock_to_destroy)
      Object.Destroy((Object) go);
    return false;
  }

  public void ClearStocks()
  {
    if (Object.op_Inequality((Object) this.stockParent, (Object) null))
      Object.DestroyImmediate((Object) ((Component) this.stockParent).gameObject);
    this.stockParent = Utility.CreateGameObject("Stocks", this._transform);
    ((Component) this.stockParent).gameObject.SetActive(false);
  }

  public static Transform GetEffect(string effect_name, Transform parent = null)
  {
    return EffectManager.GetEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effect_name, parent);
  }

  public static bool ExistEffect(string effect_name)
  {
    if (string.IsNullOrEmpty(effect_name) || !MonoBehaviourSingleton<EffectManager>.IsValid() || !MonoBehaviourSingleton<ResourceManager>.IsValid())
      return false;
    effect_name = ResourceName.AddAttributID(effect_name);
    return MonoBehaviourSingleton<ResourceManager>.I.IsCached(RESOURCE_CATEGORY.EFFECT_ACTION, effect_name);
  }

  public static Transform GetCameraLinkEffect(string effect_name, bool y0, Transform parent = null)
  {
    Transform effect = EffectManager.GetEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effect_name, parent);
    if (Object.op_Equality((Object) effect, (Object) null))
      return (Transform) null;
    CameraPosLink cameraPosLink = ((Component) effect).gameObject.AddComponent<CameraPosLink>();
    if (Object.op_Inequality((Object) cameraPosLink, (Object) null))
    {
      cameraPosLink.y0 = y0;
      EffectInfoComponent component = ((Component) effect).gameObject.GetComponent<EffectInfoComponent>();
      if (Object.op_Inequality((Object) component, (Object) null))
        cameraPosLink.cameraOffsetZ = component.CameraPosLinkOffsetZ;
    }
    return effect;
  }

  public static Transform GetUIEffect(string effect_name)
  {
    return EffectManager.GetUIEffect(effect_name, (Transform) null);
  }

  public static Transform GetUIEffect(
    string effect_name,
    UIWidget widget,
    float z = -0.001f,
    int add_render_queue = 0)
  {
    return EffectManager.GetUIEffect(effect_name, ((Component) widget).transform, z, add_render_queue);
  }

  public static Transform GetUIEffect(
    string effect_name,
    Transform parent,
    float z = -0.001f,
    int add_render_queue = 0,
    UIWidget ref_render_queue = null)
  {
    if (Object.op_Equality((Object) parent, (Object) null))
      parent = MonoBehaviourSingleton<GameSceneManager>.I.GetLastSectionExcludeCommonDialog()._transform;
    Transform effect = EffectManager.GetEffect(RESOURCE_CATEGORY.EFFECT_UI, effect_name, parent, 5);
    if (Object.op_Inequality((Object) effect, (Object) null) && add_render_queue != -1)
      EffectManager.SetUIEffectDepth(effect, parent, z, add_render_queue, ref_render_queue);
    return effect;
  }

  public static void SetUIEffectDepth(
    Transform effect,
    Transform parent,
    float z = -0.001f,
    int add_render_queue = 0,
    UIWidget ref_render_queue = null)
  {
    effect.localPosition = Vector3.op_Addition(effect.localPosition, new Vector3(0.0f, 0.0f, z));
    if (Object.op_Equality((Object) ref_render_queue, (Object) null))
      ref_render_queue = ((Component) parent).GetComponentInChildren<UIWidget>();
    rymFX fx = ((Component) effect).GetComponent<rymFX>();
    if (Object.op_Inequality((Object) fx, (Object) null))
    {
      fx.Cameras = MonoBehaviourSingleton<UIManager>.I.cameras;
      if (Object.op_Inequality((Object) ref_render_queue, (Object) null))
        ref_render_queue.onRender += (UIDrawCall.OnRenderCallback) (mate =>
        {
          if (!Object.op_Inequality((Object) fx, (Object) null))
            return;
          fx.SetRenderQueue(mate.renderQueue + add_render_queue);
        });
      else
        fx.ChangeRenderQueue = 3000 + add_render_queue;
    }
    else
    {
      if (!Object.op_Inequality((Object) ((Component) effect).GetComponent<EffectCtrl>(), (Object) null))
        return;
      Renderer[] renderers = ((Component) effect).GetComponentsInChildren<Renderer>();
      if (renderers.Length == 0)
        return;
      ref_render_queue.onRender += (UIDrawCall.OnRenderCallback) (mate =>
      {
        int num = mate.renderQueue + add_render_queue;
        int index1 = 0;
        for (int length1 = renderers.Length; index1 < length1; ++index1)
        {
          Renderer renderer = renderers[index1];
          if (Object.op_Inequality((Object) renderer, (Object) null))
          {
            Material[] materials = renderer.materials;
            int index2 = 0;
            for (int length2 = materials.Length; index2 < length2; ++index2)
            {
              Material material = materials[index2];
              if (Object.op_Inequality((Object) material, (Object) null))
                material.renderQueue = num;
            }
          }
        }
      });
    }
  }

  private static Transform GetEffect(
    RESOURCE_CATEGORY category,
    string effect_name,
    Transform parent = null,
    int layer = -1,
    bool enable_stock = false)
  {
    if (string.IsNullOrEmpty(effect_name))
      return (Transform) null;
    if (MonoBehaviourSingleton<EffectManager>.IsValid())
    {
      EffectManager i = MonoBehaviourSingleton<EffectManager>.I;
      effect_name = ResourceName.AddAttributID(effect_name);
      if (MonoBehaviourSingleton<ResourceManager>.IsValid())
      {
        if (Object.op_Equality((Object) parent, (Object) null))
          parent = i._transform;
        Transform effect1 = (Transform) null;
        bool flag = i.enableStock && enable_stock;
        if (flag)
        {
          Transform effect2 = i.stockParent.Find(effect_name);
          if (Object.op_Inequality((Object) effect2, (Object) null))
          {
            ((Component) effect2).GetComponent<EffectStock>().Recycle(parent, layer);
            return effect2;
          }
        }
        GameObject inactive_inctance = (GameObject) InstantiateManager.FindStock(category, effect_name);
        if (Object.op_Inequality((Object) inactive_inctance, (Object) null))
        {
          effect1 = InstantiateManager.Realizes(ref inactive_inctance, parent, layer);
          inactive_inctance = ((Component) effect1).gameObject;
        }
        else
        {
          GameObject gameObject = !ResourceManager.enableLoadDirect ? (GameObject) MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedObject(category, effect_name) : (GameObject) MonoBehaviourSingleton<ResourceManager>.I.LoadDirect(category, effect_name);
          if (Object.op_Inequality((Object) gameObject, (Object) null))
          {
            effect1 = ResourceUtility.Realizes((Object) gameObject, parent, layer);
            inactive_inctance = ((Component) effect1).gameObject;
          }
        }
        if (Object.op_Inequality((Object) inactive_inctance, (Object) null))
        {
          if (flag)
            inactive_inctance.AddComponent<EffectStock>();
          return effect1;
        }
      }
    }
    return (Transform) null;
  }

  public void AddOneShotInfo(EffectManager.OneShotInfo info, bool is_priority)
  {
    if (is_priority)
      this.infoList.Add(info);
    else
      this.infoSecondList.Add(info);
  }

  public static void OneShot(string effect_name, Vector3 pos, Quaternion rot, bool is_priority = false)
  {
    EffectManager.OneShot(effect_name, pos, rot, Vector3.one, is_priority);
  }

  public static void OneShot(
    string effect_name,
    Vector3 pos,
    Quaternion rot,
    Vector3 scale,
    bool is_priority = false,
    Action<Transform> callback = null)
  {
    bool flag = false;
    if (MonoBehaviourSingleton<InGameManager>.I.graphicOptionType >= 2)
      flag = true;
    if (flag)
    {
      EffectManager._OneShot(effect_name, pos, rot, scale, callback);
    }
    else
    {
      Vector3 viewportPoint = MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToViewportPoint(pos);
      if ((double) viewportPoint.x < -0.5 || (double) viewportPoint.x > 1.5 || (double) viewportPoint.y < -0.5 || (double) viewportPoint.y > 1.5 || (double) viewportPoint.z < 0.0)
        return;
      if (MonoBehaviourSingleton<EffectManager>.IsValid())
      {
        EffectManager.OneShotInfo info = rymTPool<EffectManager.OneShotInfo>.Get();
        info.name = effect_name;
        info.pos = pos;
        info.rot = rot;
        info.scale = scale;
        info.time = Time.time;
        info.onCreateCallBack = callback;
        MonoBehaviourSingleton<EffectManager>.I.AddOneShotInfo(info, is_priority);
      }
      else
        EffectManager._OneShot(effect_name, pos, rot, scale, callback);
    }
  }

  public static void _OneShot(
    string effect_name,
    Vector3 pos,
    Quaternion rot,
    Vector3 scale,
    Action<Transform> callback = null)
  {
    Transform effect = EffectManager.GetEffect(RESOURCE_CATEGORY.EFFECT_ACTION, effect_name, enable_stock: true);
    if (Object.op_Equality((Object) effect, (Object) null))
      return;
    effect.position = pos;
    effect.rotation = rot;
    effect.localScale = Vector3.Scale(effect.localScale, scale);
    if (callback == null)
      return;
    callback(effect);
  }

  public void DeleteManagerChildrenEffects()
  {
    this.infoList.Clear();
    ((Component) this).gameObject.GetComponentsInChildren<rymFX>(Temporary.fxList);
    int index1 = 0;
    for (int count = Temporary.fxList.Count; index1 < count; ++index1)
      Object.Destroy((Object) ((Component) Temporary.fxList[index1]).gameObject);
    Temporary.fxList.Clear();
    ((Component) this).gameObject.GetComponentsInChildren<EffectCtrl>(Temporary.effectCtrlList);
    int index2 = 0;
    for (int count = Temporary.effectCtrlList.Count; index2 < count; ++index2)
      Object.Destroy((Object) ((Component) Temporary.effectCtrlList[index2]).gameObject);
    Temporary.effectCtrlList.Clear();
  }

  public static void ReleaseEffect(
    GameObject effect_object,
    bool isPlayEndAnimation = true,
    bool immediate = false)
  {
    if (Object.op_Equality((Object) effect_object, (Object) null))
      return;
    if (!MonoBehaviourSingleton<EffectManager>.IsValid())
    {
      Object.Destroy((Object) effect_object);
    }
    else
    {
      EffectManager i = MonoBehaviourSingleton<EffectManager>.I;
      EffectInfoComponent component1 = effect_object.GetComponent<EffectInfoComponent>();
      if (Object.op_Inequality((Object) component1, (Object) null) && component1.destroyLoopEnd)
      {
        component1.SetLoopAudioObject((AudioObject) null);
        rymFX component2 = effect_object.GetComponent<rymFX>();
        EffectCtrl effectCtrl = (EffectCtrl) null;
        if (Object.op_Equality((Object) component2, (Object) null))
          effectCtrl = effect_object.GetComponent<EffectCtrl>();
        if (Object.op_Equality((Object) effectCtrl, (Object) null) && effect_object.transform.childCount > 0)
        {
          effect_object.GetComponentsInChildren<Renderer>(Temporary.rendererList);
          int index = 0;
          for (int count = Temporary.rendererList.Count; index < count; ++index)
            Temporary.rendererList[index].enabled = false;
          Temporary.rendererList.Clear();
        }
        effect_object.GetComponents<Trail>(Temporary.trailList);
        bool flag = false;
        if (Object.op_Inequality((Object) component2, (Object) null) && ((Behaviour) component2).enabled)
        {
          component2.AutoDelete = true;
          component2.LoopEnd = true;
          flag = true;
        }
        else if (Object.op_Inequality((Object) effectCtrl, (Object) null) && ((Behaviour) effectCtrl).enabled)
        {
          effectCtrl.EndLoop(isPlayEndAnimation);
          flag = true;
        }
        if (flag && !immediate)
        {
          int index = 0;
          for (int count = Temporary.trailList.Count; index < count; ++index)
            Temporary.trailList[index].StartDeleteFade();
          Temporary.trailList.Clear();
        }
        else
        {
          i.StockOrDestroy(effect_object, true);
          int index = 0;
          for (int count = Temporary.trailList.Count; index < count; ++index)
            Temporary.trailList[index].SetAutoDelete();
          Temporary.trailList.Clear();
        }
      }
      else
        i.StockOrDestroy(effect_object, true);
    }
  }

  public static void ReleaseEffect(ref Transform t)
  {
    if (!Object.op_Inequality((Object) t, (Object) null))
      return;
    EffectManager.ReleaseEffect(((Component) t).gameObject);
    t = (Transform) null;
  }

  private class Pool_OneShotInfo : rymTPool<EffectManager.OneShotInfo>
  {
  }

  public class OneShotInfo
  {
    public string name;
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public float time;
    public Action<Transform> onCreateCallBack;
  }
}
