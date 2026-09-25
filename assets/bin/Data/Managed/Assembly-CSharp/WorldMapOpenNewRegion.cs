// Decompiled with JetBrains decompiler
// Type: WorldMapOpenNewRegion
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class WorldMapOpenNewRegion : GameSection
{
  private WorldMapOpenNewRegion.SectionEventData eventData;
  private WorldMapOpenNewRegion.OpendRegionInfo[] openedRegionInfo;
  private GameObject worldMapUIRoot;
  private Transform worldMapObject;
  private Transform playerMarker;
  private UITexture uiMapSprite;
  private SpotManager spots;
  private Transform[] regionAreas;
  private Transform glowRegionTop;
  private Transform mapGlowEffectA;
  private Transform mapGlowEffectB;
  private ParticleSystem mapGlowEffectParticleA;
  private ParticleSystem mapGlowEffectParticleB;
  private uint fromRegionID;
  private uint toRegionID;
  private Transform telop;
  private Transform targetRegionIcon;
  private Material glowMaterial;
  private static readonly int SE_ID_SMOKE = 40000034;
  private static readonly int SE_ID_LOGO = 40000160;
  private bool calledExit;
  private bool isUpdateRenderTexture;
  private UIEventListener bgEventListener;

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "FieldMapTable";
      yield return "RegionTable";
    }
  }

  public WorldMapCameraController worldMapCamera { get; private set; }

  public override void Initialize() => this.StartCoroutine("DoInitialize");

  private IEnumerator DoInitialize()
  {
    this.eventData = (WorldMapOpenNewRegion.SectionEventData) GameSection.GetEventData();
    FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData(MonoBehaviourSingleton<FieldManager>.I.currentPortalID);
    FieldMapTable.FieldMapTableData fieldMapData1 = Singleton<FieldMapTable>.I.GetFieldMapData(portalData.srcMapID);
    FieldMapTable.FieldMapTableData fieldMapData2 = Singleton<FieldMapTable>.I.GetFieldMapData(portalData.dstMapID);
    this.fromRegionID = fieldMapData1.regionId;
    this.toRegionID = fieldMapData2.regionId;
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject loadedWorldMap = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "WorldMap");
    LoadObject loadedRegionSpotRoot = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionSpotRoot");
    LoadObject loadedRegionSpot = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionSpot");
    LoadObject loadedPlayerMarker = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "PlayerMarker");
    LoadObject loadedMapGlowEffectA = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "MapGlowEffectA");
    LoadObject loadedMapGlowEffectB = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "MapGlowEffectB");
    LoadObject loadedTelop = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "TelopOpenRegion");
    loadingQueue.CacheSE(WorldMapOpenNewRegion.SE_ID_LOGO);
    loadingQueue.CacheSE(WorldMapOpenNewRegion.SE_ID_SMOKE);
    uint[] numArray = MonoBehaviourSingleton<WorldMapManager>.I.GetOpenRegionIdListInWorldMap();
    if (numArray.Length == 0)
      numArray = new uint[1];
    LoadObject[] regionAreaLOs = new LoadObject[numArray.Length];
    string regionIcon1 = ResourceName.GetRegionIcon(0);
    string regionIcon2 = ResourceName.GetRegionIcon(1);
    int num = numArray.Length - 1;
    this.openedRegionInfo = new WorldMapOpenNewRegion.OpendRegionInfo[numArray.Length];
    for (int index = 0; index < numArray.Length; ++index)
    {
      RegionTable.Data data = Singleton<RegionTable>.I.GetData(numArray[index]);
      if (!data.hasParentRegion())
      {
        string resource_name = regionIcon2;
        if (num == index)
          resource_name = regionIcon1;
        LoadObject _icon = loadingQueue.Load(RESOURCE_CATEGORY.REGION_ICON, resource_name);
        this.openedRegionInfo[index] = new WorldMapOpenNewRegion.OpendRegionInfo(data, _icon);
        if (index != 0)
          regionAreaLOs[index] = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "WorldMapPart" + numArray[index].ToString("D3"));
      }
    }
    LoadObject loadedMaterial = (LoadObject) null;
    if (!this.eventData.IsOnlyCameraMoveEvent())
      loadedMaterial = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "WorldMapPartGlow" + this.toRegionID.ToString("D3"));
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    this.worldMapUIRoot = ((Component) ResourceUtility.Realizes(loadedWorldMap.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform)).gameObject;
    this.worldMapCamera = ((Component) this.worldMapUIRoot.transform.Find("Camera")).GetComponent<WorldMapCameraController>();
    this.worldMapCamera.isInteractive = false;
    this.worldMapObject = this.worldMapUIRoot.transform.Find("Map");
    this.spots = new SpotManager(loadedRegionSpotRoot.loadedObject as GameObject, loadedRegionSpot.loadedObject as GameObject, this.worldMapCamera._camera);
    this.spots.CreateSpotRoot();
    GameObject gameObject = ((Component) this.spots.spotRootTransform.Find("BG")).gameObject;
    gameObject.gameObject.SetActive(true);
    this.bgEventListener = UIEventListener.Get(gameObject);
    ((Component) this.spots.spotRootTransform.Find("TaptoSkip")).gameObject.SetActive(true);
    this.mapGlowEffectA = ResourceUtility.Realizes(loadedMapGlowEffectA.loadedObject, this.worldMapObject);
    ((Component) this.mapGlowEffectA).gameObject.SetActive(false);
    this.mapGlowEffectParticleA = ((Component) this.mapGlowEffectA).GetComponent<ParticleSystem>();
    this.mapGlowEffectB = ResourceUtility.Realizes(loadedMapGlowEffectB.loadedObject, this.worldMapObject);
    ((Component) this.mapGlowEffectB).gameObject.SetActive(false);
    this.mapGlowEffectParticleB = ((Component) this.mapGlowEffectB).GetComponent<ParticleSystem>();
    this.playerMarker = ResourceUtility.Realizes(loadedPlayerMarker.loadedObject, this._transform);
    ((Component) this.playerMarker).gameObject.SetActive(false);
    if (loadedMaterial != null)
      this.glowMaterial = loadedMaterial.loadedObject as Material;
    this.regionAreas = new Transform[regionAreaLOs.Length];
    for (int index = 0; index < regionAreaLOs.Length; ++index)
    {
      LoadObject loadObject = regionAreaLOs[index];
      if (loadObject != null && Object.op_Inequality((Object) null, loadObject.loadedObject))
      {
        Transform transform = ResourceUtility.Realizes(loadObject.loadedObject, this.worldMapObject);
        if ((long) index == (long) this.toRegionID)
        {
          if (this.eventData.IsOnlyCameraMoveEvent())
            ((Component) transform).gameObject.SetActive(true);
          else
            ((Component) transform).gameObject.SetActive(false);
          this.mapGlowEffectA.SetParent(transform);
          this.mapGlowEffectA.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
          this.mapGlowEffectB.SetParent(transform);
          this.mapGlowEffectB.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
          ParticleSystem.ShapeModule shape = this.mapGlowEffectParticleB.shape;
          MeshFilter component = ((Component) transform).GetComponent<MeshFilter>();
          ((ParticleSystem.ShapeModule) ref shape).mesh = component.sharedMesh;
          this.glowRegionTop = ResourceUtility.Realizes(loadObject.loadedObject, this.worldMapObject);
          ((Component) this.glowRegionTop).gameObject.SetActive(false);
          this.glowRegionTop.localPosition = Vector3.op_Addition(this.glowRegionTop.localPosition, new Vector3(0.0f, 0.0f, 1f / 1000f));
          this.glowRegionTop.localScale = new Vector3(1.1f, 1.1f, 1.1f);
          ((Component) this.glowRegionTop).GetComponent<Renderer>().material = this.glowMaterial;
        }
        else
          ((Component) transform).gameObject.SetActive(true);
        this.regionAreas[index] = transform;
      }
    }
    this.telop = ResourceUtility.Realizes(loadedTelop.loadedObject, this.spots.spotRootTransform);
    Transform transform1 = Utility.Find(this.spots.spotRootTransform, "CLOSE_BTN");
    if (Object.op_Inequality((Object) null, (Object) transform1))
      ((Component) transform1).gameObject.SetActive(false);
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.InitMapSprite);
    base.Initialize();
  }

  public void InitRegionInfo()
  {
    if (this.spots == null)
      return;
    Transform transform = this.spots.SetRoot(this._transform);
    if (Object.op_Equality((Object) this.uiMapSprite, (Object) null))
      this.uiMapSprite = ((Component) transform.Find("Map")).gameObject.GetComponent<UITexture>();
    this.InitMapSprite(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
    ((Component) this.worldMapObject).gameObject.SetActive(true);
    for (int index = 0; index < this.openedRegionInfo.Length; ++index)
    {
      RegionTable.Data data = this.openedRegionInfo[index].data;
      if (data != null)
      {
        SpotManager.Spot spot = this.spots.AddSpot((int) data.regionId, data.regionName, data.iconPos, SpotManager.ICON_TYPE.CLEARED, "OPEN_REGION");
        spot.SetIconSprite("SPR_ICON", this.openedRegionInfo[index].icon.loadedObject as Texture2D, (int) data.iconSize.x, (int) data.iconSize.y);
        if ((int) this.fromRegionID == (int) data.regionId)
        {
          ((Component) this.playerMarker).gameObject.SetActive(true);
          this.playerMarker.SetParent(((Component) this.worldMapObject).transform);
          PlayerMarker component = ((Component) this.playerMarker).GetComponent<PlayerMarker>();
          component.SetWorldMode(true);
          component.SetCamera(((Component) this.worldMapCamera._camera).transform);
          this.playerMarker.localPosition = data.markerPos;
        }
        if ((int) this.toRegionID == (int) data.regionId)
        {
          this.targetRegionIcon = spot._transform;
          if (!this.eventData.IsOnlyCameraMoveEvent())
            ((Component) spot._transform).gameObject.SetActive(false);
          else
            ((Component) spot._transform).gameObject.SetActive(true);
        }
      }
    }
  }

  private void InitMapSprite(bool isPortrait)
  {
    if (Object.op_Inequality((Object) this.uiMapSprite, (Object) null))
    {
      if (Object.op_Equality((Object) null, (Object) this.worldMapCamera._camera.targetTexture))
        this.worldMapCamera.Restore();
      this.uiMapSprite.mainTexture = (Texture) this.worldMapCamera._camera.targetTexture;
      this.uiMapSprite.width = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth;
      this.uiMapSprite.height = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight;
    }
    if (isPortrait)
    {
      if (Object.op_Inequality((Object) null, (Object) this.telop))
        this.telop.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
      if (Object.op_Inequality((Object) null, (Object) this.mapGlowEffectA))
      {
        ParticleSystemRenderer component = ((Component) this.mapGlowEffectA).GetComponent<ParticleSystemRenderer>();
        component.minParticleSize = 1f;
        component.maxParticleSize = 1f;
      }
      if (!Object.op_Inequality((Object) null, (Object) this.mapGlowEffectB))
        return;
      ParticleSystemRenderer component1 = ((Component) this.mapGlowEffectB).GetComponent<ParticleSystemRenderer>();
      component1.minParticleSize = 1f;
      component1.maxParticleSize = 1f;
    }
    else
    {
      if (Object.op_Inequality((Object) null, (Object) this.telop))
        this.telop.localPosition = new Vector3(0.0f, -90f, 0.0f);
      if (Object.op_Inequality((Object) null, (Object) this.mapGlowEffectA))
      {
        ParticleSystemRenderer component = ((Component) this.mapGlowEffectA).GetComponent<ParticleSystemRenderer>();
        component.minParticleSize = 0.5f;
        component.maxParticleSize = 0.5f;
      }
      if (!Object.op_Inequality((Object) null, (Object) this.mapGlowEffectB))
        return;
      ParticleSystemRenderer component2 = ((Component) this.mapGlowEffectB).GetComponent<ParticleSystemRenderer>();
      component2.minParticleSize = 0.5f;
      component2.maxParticleSize = 0.5f;
    }
  }

  protected override void OnOpen()
  {
    this.bgEventListener.onClick += new UIEventListener.VoidDelegate(this.onClick);
    ((Component) this.worldMapObject).gameObject.SetActive(true);
    Vector3 from = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 to = new Vector3(0.0f, 0.0f, 0.0f);
    RegionTable.Data[] data = Singleton<RegionTable>.I.GetData();
    if (0U <= this.fromRegionID && (long) data.Length > (long) this.fromRegionID)
    {
      from = data[(int) this.fromRegionID].iconPos;
      this.worldMapCamera.targetPos = from;
    }
    if (0U <= this.toRegionID && (long) data.Length > (long) this.toRegionID)
      to = data[(int) this.toRegionID].iconPos;
    this.FadeInMap((System.Action) (() =>
    {
      this.InitRegionInfo();
      if (this.eventData.IsOnlyCameraMoveEvent())
        this.MoveCamera(from, to);
      else
        this.GlowRegion(from, to);
    }));
    this.collectUI = this._transform;
    base.OnOpen();
  }

  private void OnQuery_EXIT()
  {
    if (!this.calledExit)
    {
      this.bgEventListener.onClick -= new UIEventListener.VoidDelegate(this.onClick);
      MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(nameof (WorldMapOpenNewRegion), ((Component) this).gameObject, "INGAME_MAIN");
      this.calledExit = true;
    }
    this.StopAllCoroutines();
  }

  public override void Exit()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= new ScreenOrientationManager.OnScreenRotateDelegate(this.InitMapSprite);
    if (this.spots != null)
      this.spots.ClearAllSpot();
    base.Exit();
  }

  protected override void OnDestroy()
  {
    if (Object.op_Inequality((Object) this.worldMapUIRoot, (Object) null))
      Object.Destroy((Object) this.worldMapUIRoot);
    if (Object.op_Inequality((Object) this.worldMapObject, (Object) null))
      Object.Destroy((Object) ((Component) this.worldMapObject).gameObject);
    base.OnDestroy();
  }

  private void LateUpdate()
  {
    if (this.spots != null)
      this.spots.Update();
    if (!this.isUpdateRenderTexture)
      return;
    this.InitMapSprite(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
    this.isUpdateRenderTexture = false;
  }

  public void FadeInMap(System.Action onComplete)
  {
    if (Object.op_Inequality((Object) this.worldMapObject, (Object) null))
      ((Component) this.worldMapObject).gameObject.SetActive(true);
    if (Object.op_Inequality((Object) this.uiMapSprite, (Object) null))
      ((Component) this.uiMapSprite).gameObject.SetActive(true);
    this.StartCoroutine(this.DoFadeMap(0.0f, 1f, 0.4f, (System.Action) (() =>
    {
      if (onComplete == null)
        return;
      onComplete();
    })));
  }

  private IEnumerator DoFadeMap(float from, float to, float time, System.Action onComplete)
  {
    if (!Object.op_Equality((Object) this.worldMapObject, (Object) null))
    {
      Renderer r = ((Component) this.worldMapObject).gameObject.GetComponentInChildren<Renderer>();
      if (!Object.op_Equality((Object) r, (Object) null))
      {
        for (float timer = 0.0f; (double) timer < (double) time; timer += Time.deltaTime)
        {
          r.material.SetFloat("_Alpha", Mathf.Lerp(from, to, timer / time));
          yield return (object) null;
        }
        r.material.SetFloat("_Alpha", to);
        if (onComplete != null)
          onComplete();
      }
    }
  }

  private void GlowRegion(Vector3 from, Vector3 to)
  {
    this.worldMapCamera.targetPos = from;
    this.StartCoroutine(this.DoGlowRegion(from, to));
  }

  private IEnumerator DoGlowRegion(Vector3 from, Vector3 to)
  {
    yield return (object) new WaitForSeconds(0.5f);
    Vector3Interpolator ip = new Vector3Interpolator();
    Vector3 zoomDownTo = Vector3.op_Addition(to, new Vector3(0.0f, 0.0f, -3f));
    ip.Set(1f, from, zoomDownTo, (AnimationCurve) null, new Vector3(), (AnimationCurve) null);
    ip.Play();
    while (ip.IsPlaying())
    {
      ip.Update();
      this.worldMapCamera.targetPos = ip.Get();
      yield return (object) null;
    }
    Transform regionArea = this.regionAreas[(int) this.toRegionID];
    ((Component) regionArea).gameObject.SetActive(true);
    Renderer toRegionRenderer = ((Component) regionArea).GetComponent<Renderer>();
    toRegionRenderer.material.SetFloat("_Alpha", 0.0f);
    Renderer topRenderer = ((Component) this.glowRegionTop).GetComponent<Renderer>();
    topRenderer.material.SetFloat("_Alpha", 0.0f);
    topRenderer.material.SetFloat("_AddColor", 1f);
    topRenderer.material.SetFloat("_BlendRate", 1f);
    topRenderer.sortingOrder = 2;
    ((Component) this.glowRegionTop).gameObject.SetActive(true);
    this.DelayExecute(1f, (System.Action) (() =>
    {
      ((Component) this.mapGlowEffectA).gameObject.SetActive(true);
      ((Component) this.mapGlowEffectA).GetComponent<Renderer>().sortingOrder = 1;
    }));
    yield return (object) new WaitForSeconds(1f);
    ip.Set(1f, zoomDownTo, to, (AnimationCurve) null, new Vector3(), (AnimationCurve) null);
    ip.Play();
    while (ip.IsPlaying())
    {
      ip.Update();
      this.worldMapCamera.targetPos = ip.Get();
      yield return (object) null;
    }
    FloatInterpolator fip = new FloatInterpolator();
    fip.Set(2f, 0.0f, 1.5f, (AnimationCurve) null, 0.0f, (AnimationCurve) null);
    fip.Play();
    SoundManager.PlayOneShotUISE(WorldMapOpenNewRegion.SE_ID_SMOKE);
    while (fip.IsPlaying())
    {
      double num = (double) fip.Update();
      topRenderer.material.SetFloat("_Alpha", fip.Get());
      yield return (object) null;
    }
    toRegionRenderer.material.SetFloat("_Alpha", 1f);
    this.mapGlowEffectParticleA.Stop();
    ((Component) this.mapGlowEffectB).gameObject.SetActive(true);
    yield return (object) new WaitForSeconds(0.0f);
    fip.Set(0.2f, 1f, 0.0f, (AnimationCurve) null, 0.0f, (AnimationCurve) null);
    fip.Play();
    while (fip.IsPlaying())
    {
      double num = (double) fip.Update();
      topRenderer.material.SetFloat("_Alpha", fip.Get());
      yield return (object) null;
    }
    yield return (object) new WaitForSeconds(0.0f);
    ((Component) this.targetRegionIcon).gameObject.SetActive(true);
    ((Component) this.targetRegionIcon).GetComponent<TweenScale>().PlayForward();
    yield return (object) new WaitForSeconds(1f);
    this.mapGlowEffectParticleB.Stop();
    bool isTweenEnd = false;
    UITweenCtrl component = ((Component) this.telop).GetComponent<UITweenCtrl>();
    component.Reset();
    component.Play(onFinished: (EventDelegate.Callback) (() => isTweenEnd = true));
    SoundManager.PlayOneShotUISE(WorldMapOpenNewRegion.SE_ID_LOGO);
    while (!isTweenEnd)
      yield return (object) null;
    yield return (object) new WaitForSeconds(0.0f);
    Vector3 scaleBegin = this.playerMarker.localScale;
    Vector3 scaleEnd = new Vector3(0.0f, 0.0f, 0.0f);
    ip.Set(0.5f, scaleBegin, scaleEnd, (AnimationCurve) null, new Vector3(), (AnimationCurve) null);
    ip.Play();
    while (ip.IsPlaying())
    {
      ip.Update();
      this.playerMarker.localScale = ip.Get();
      yield return (object) null;
    }
    RegionTable.Data data = this.openedRegionInfo[(int) this.toRegionID].data;
    if (data != null)
      this.playerMarker.localPosition = data.markerPos;
    yield return (object) new WaitForSeconds(0.1f);
    ip.Set(0.5f, scaleEnd, scaleBegin, (AnimationCurve) null, new Vector3(), (AnimationCurve) null);
    ip.Play();
    while (ip.IsPlaying())
    {
      ip.Update();
      this.playerMarker.localScale = ip.Get();
      yield return (object) null;
    }
    yield return (object) new WaitForSeconds(0.4f);
    this.OnQuery_EXIT();
  }

  private void MoveCamera(Vector3 from, Vector3 to)
  {
    this.StartCoroutine(this.DoMoveCamera(from, to));
  }

  private IEnumerator DoMoveCamera(Vector3 from, Vector3 to)
  {
    Vector3Interpolator ip = new Vector3Interpolator();
    yield return (object) new WaitForSeconds(0.5f);
    Vector3 scaleBegin = this.playerMarker.localScale;
    Vector3 scaleEnd = new Vector3(0.0f, 0.0f, 0.0f);
    ip.Set(0.5f, scaleBegin, scaleEnd, (AnimationCurve) null, new Vector3(), (AnimationCurve) null);
    ip.Play();
    while (ip.IsPlaying())
    {
      ip.Update();
      this.playerMarker.localScale = ip.Get();
      yield return (object) null;
    }
    yield return (object) new WaitForSeconds(0.0f);
    ip.Set(0.7f, from, to, (AnimationCurve) null, new Vector3(), (AnimationCurve) null);
    ip.Play();
    while (ip.IsPlaying())
    {
      ip.Update();
      this.worldMapCamera.targetPos = ip.Get();
      yield return (object) null;
    }
    RegionTable.Data data = this.openedRegionInfo[(int) this.toRegionID].data;
    if (data != null)
      this.playerMarker.localPosition = data.markerPos;
    yield return (object) new WaitForSeconds(0.1f);
    ip.Set(0.5f, scaleEnd, scaleBegin, (AnimationCurve) null, new Vector3(), (AnimationCurve) null);
    ip.Play();
    while (ip.IsPlaying())
    {
      ip.Update();
      this.playerMarker.localScale = ip.Get();
      yield return (object) null;
    }
    yield return (object) new WaitForSeconds(0.4f);
    this.OnQuery_EXIT();
  }

  private void DelayExecute(float delayTime, System.Action func)
  {
    this.StartCoroutine(this.DoDelayExecute(delayTime, func));
  }

  private IEnumerator DoDelayExecute(float delayTime, System.Action func)
  {
    yield return (object) new WaitForSeconds(delayTime);
    if (func != null)
      func();
  }

  private void OnApplicationPause(bool paused) => this.isUpdateRenderTexture = !paused;

  private void onClick(GameObject g) => this.OnQuery_EXIT();

  public enum EVENT_TYPE
  {
    NONE,
    ONLY_CAMERA_MOVE,
  }

  public class SectionEventData
  {
    private WorldMapOpenNewRegion.EVENT_TYPE eventType;

    public SectionEventData(WorldMapOpenNewRegion.EVENT_TYPE _eventType)
    {
      this.eventType = _eventType;
    }

    public bool IsOnlyCameraMoveEvent()
    {
      return this.eventType == WorldMapOpenNewRegion.EVENT_TYPE.ONLY_CAMERA_MOVE;
    }
  }

  private struct OpendRegionInfo(RegionTable.Data _data, LoadObject _icon)
  {
    public RegionTable.Data data = _data;
    public LoadObject icon = _icon;
  }
}
