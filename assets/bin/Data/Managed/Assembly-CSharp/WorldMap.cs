// Decompiled with JetBrains decompiler
// Type: WorldMap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class WorldMap : GameSection
{
  private GameObject worldMapUIRoot;
  private GameObject[] worldMaps;
  private Transform playerMarker;
  private UITexture uiMapSprite;
  private SpotManager spots;
  private Transform[] regionAreas;
  private int currentRegionID;
  private bool isInWorldMap = true;
  private bool isUpdateRenderTexture;
  private bool isChangingMap;
  private int currentWorldIndex;
  private REGION_DIFFICULTY_TYPE currentDifficulty;
  private TweenAlpha mapTween;
  private int releaseRegionId = -1;
  private bool isInGame;
  private string beforeSectionName = "";
  private bool playingReleaseRegion;
  private int toRegionId;
  private UIEventListener bgEventListener;
  private static readonly int SE_ID_SMOKE = 40000034;
  private static readonly int SE_ID_LOGO = 40000160;
  private Transform mapGlowEffectA;
  private Transform mapGlowEffectB;
  private ParticleSystem mapGlowEffectParticleA;
  private ParticleSystem mapGlowEffectParticleB;
  private Material glowMaterial;
  private Transform glowRegionTop;
  private Transform telop;
  private bool regionOpenInitialized;
  private const int CHAPTER_CONTENT_NUMBER = 4;
  private int currentCenterIndex;
  private List<Transform> chapterContentList;
  private int[] contentWorldIndex;
  private UICenterOnChild center;
  private UIScrollView chapterScrollView;
  private bool beforePressed;
  private UITweenCtrl[] tweenAnimations;
  private WorldMap.ValidRegionInfo[] validRegionInfo;
  private ZoomBlurFilter _blurFilter;
  private Vector2 blurCenter;

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "FieldMapTable";
      yield return "RegionTable";
    }
  }

  public WorldMapCameraController worldMapCamera { get; private set; }

  public ZoomBlurFilter blurFilter
  {
    get => this._blurFilter;
    private set => this._blurFilter = value;
  }

  public override void Initialize()
  {
    this.isInGame = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene";
    this.StartCoroutine("DoInitialize");
  }

  private IEnumerator DoInitialize()
  {
    bool is_recv_delivery = false;
    MonoBehaviourSingleton<DeliveryManager>.I.SendEventNormalList((Action<bool>) (is_success => is_recv_delivery = true));
    while (!is_recv_delivery)
      yield return (object) null;
    LoadingQueue loadQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject loadedWorldMap = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, nameof (WorldMap));
    LoadObject loadedRegionSpotRoot = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionSpotRoot");
    LoadObject loadedRegionSpot = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionSpot");
    LoadObject loadedFilterCamera = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ZoomBlurFilterCamera");
    LoadObject loadedPlayerMarker = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "PlayerMarker");
    uint[] array = MonoBehaviourSingleton<WorldMapManager>.I.GetOpenRegionIdListInWorldMap();
    uint[] numArray = MonoBehaviourSingleton<WorldMapManager>.I.GetValidRegionIdListInWorldMap();
    if (MonoBehaviourSingleton<WorldMapManager>.I.releaseRegionIdfromBoard > 0)
      this.releaseRegionId = MonoBehaviourSingleton<WorldMapManager>.I.releaseRegionIdfromBoard;
    if (this.releaseRegionId < 0)
    {
      foreach (uint regionId in array)
      {
        if (!MonoBehaviourSingleton<WorldMapManager>.I.IsShowedOpenRegion((int) regionId))
        {
          this.releaseRegionId = (int) regionId;
          break;
        }
      }
    }
    if (this.releaseRegionId < 0 && MonoBehaviourSingleton<WorldMapManager>.I.transferInfo != null)
      this.releaseRegionId = MonoBehaviourSingleton<WorldMapManager>.I.transferInfo.nextRegionId;
    if (array.Length == 0)
      array = new uint[1];
    if (numArray.Length == 0)
      numArray = new uint[1];
    LoadObject[] regionAreaLOs = new LoadObject[numArray.Length];
    string regionIcon1 = ResourceName.GetRegionIcon(0);
    string regionIcon2 = ResourceName.GetRegionIcon(1);
    string regionIcon3 = ResourceName.GetRegionIcon(2);
    this.validRegionInfo = new WorldMap.ValidRegionInfo[numArray.Length];
    for (int index = 0; index < numArray.Length; ++index)
    {
      RegionTable.Data data = Singleton<RegionTable>.I.GetData(numArray[index]);
      if (!data.hasParentRegion())
      {
        string resource_name = regionIcon2;
        WorldMap.REGION_STATUS _status = WorldMap.REGION_STATUS.OPEN;
        if (Array.IndexOf<uint>(array, numArray[index]) < 0)
        {
          resource_name = regionIcon3;
          _status = WorldMap.REGION_STATUS.CLOSE;
        }
        else
        {
          EventNormalListData eventNormalListData = MonoBehaviourSingleton<DeliveryManager>.I.GetEventNormalListData((int) data.regionId);
          if (eventNormalListData != null && eventNormalListData.numerator < eventNormalListData.denominator)
            resource_name = regionIcon1;
        }
        LoadObject _icon = loadQueue.Load(RESOURCE_CATEGORY.REGION_ICON, resource_name);
        LoadObject _releaseIcon = loadQueue.Load(RESOURCE_CATEGORY.REGION_ICON, regionIcon1);
        this.validRegionInfo[index] = new WorldMap.ValidRegionInfo(data, _icon, _releaseIcon, _status);
        if (index != 0)
          regionAreaLOs[index] = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "WorldMapPart" + numArray[index].ToString("D3"));
      }
    }
    if (loadQueue.IsLoading())
      yield return (object) loadQueue.Wait();
    this.worldMapUIRoot = ((Component) ResourceUtility.Realizes(loadedWorldMap.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform)).gameObject;
    this.worldMapCamera = ((Component) this.worldMapUIRoot.transform.Find("Camera")).GetComponent<WorldMapCameraController>();
    GameObject map1 = ((Component) this.worldMapUIRoot.transform.Find("Map")).gameObject;
    Transform transform1 = this.worldMapUIRoot.transform.Find("Map2");
    this.spots = new SpotManager(loadedRegionSpotRoot.loadedObject as GameObject, loadedRegionSpot.loadedObject as GameObject, this.worldMapCamera._camera);
    this.spots.CreateSpotRoot();
    this.spots.SetRoot(this._transform);
    this.tweenAnimations = ((Component) this.spots.spotRootTransform).GetComponentsInChildren<UITweenCtrl>();
    this.blurFilter = (ResourceUtility.Instantiate<Object>(loadedFilterCamera.loadedObject) as GameObject).GetComponent<ZoomBlurFilter>();
    UIPanel component = ((Component) this.spots.spotRootTransform).GetComponent<UIPanel>();
    if (Object.op_Inequality((Object) component, (Object) null))
      component.depth = this.baseDepth + 1;
    this.SetSelectorDepth(this.spots.spotRootTransform, component.depth);
    this.currentRegionID = 0;
    FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
    if (fieldMapData != null)
      this.currentRegionID = (int) fieldMapData.regionId;
    RegionTable.Data data1 = this.releaseRegionId <= 0 ? Singleton<RegionTable>.I.GetData((uint) this.currentRegionID) : Singleton<RegionTable>.I.GetData((uint) this.releaseRegionId);
    if (data1 != null)
    {
      this.currentWorldIndex = Mathf.Max(0, data1.worldId - 1);
      this.currentDifficulty = data1.difficulty;
    }
    else
    {
      this.currentWorldIndex = 0;
      this.currentDifficulty = REGION_DIFFICULTY_TYPE.NORMAL;
    }
    GameObject worldMap2Object = (GameObject) null;
    Transform worldSelect = this.FindCtrl(this.spots.spotRootTransform, (Enum) WorldMap.UI.OBJ_WORLD_SELECT);
    if (Object.op_Equality((Object) transform1, (Object) null))
    {
      ((Component) worldSelect).gameObject.SetActive(false);
    }
    else
    {
      worldMap2Object = ((Component) transform1).gameObject;
      ((Component) transform1).gameObject.SetActive(false);
      LoadObject world1 = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, ResourceName.GetChapterImageName(1));
      LoadObject world2 = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, ResourceName.GetChapterImageName(2));
      if (loadQueue.IsLoading())
        yield return (object) loadQueue.Wait();
      Transform spotRootTransform = this.spots.spotRootTransform;
      this.chapterContentList = new List<Transform>();
      this.chapterContentList.Add(this.FindCtrl(spotRootTransform, (Enum) WorldMap.UI.SPR_CONTENT1));
      this.chapterContentList.Add(this.FindCtrl(spotRootTransform, (Enum) WorldMap.UI.SPR_CONTENT2));
      this.chapterContentList.Add(this.FindCtrl(spotRootTransform, (Enum) WorldMap.UI.SPR_CONTENT3));
      this.chapterContentList.Add(this.FindCtrl(spotRootTransform, (Enum) WorldMap.UI.SPR_CONTENT4));
      this.currentCenterIndex = 0;
      this.SetupChapterContentTexture(spotRootTransform, world1.loadedObject as Texture2D, world2.loadedObject as Texture2D);
      this.center = ((Component) this.FindCtrl(spotRootTransform, (Enum) WorldMap.UI.OBJ_WRAP_CENTER)).GetComponent<UICenterOnChild>();
      this.center.onCenter = new UICenterOnChild.OnCenterCallback(this.DragChapter);
      this.chapterScrollView = ((Component) this.FindCtrl(spotRootTransform, (Enum) WorldMap.UI.SCR_SELECTOR)).GetComponent<UIScrollView>();
      this.SyncWorldMapSelectAntors(((Component) worldSelect).GetComponent<UIWidget>());
      world1 = (LoadObject) null;
      world2 = (LoadObject) null;
    }
    this.worldMaps = new GameObject[2]
    {
      map1.gameObject,
      worldMap2Object
    };
    this.playerMarker = ResourceUtility.Realizes(loadedPlayerMarker.loadedObject, this._transform);
    ((Component) this.playerMarker).gameObject.SetActive(false);
    this.regionAreas = new Transform[regionAreaLOs.Length];
    for (int index = 0; index < regionAreaLOs.Length; ++index)
    {
      if (!Object.op_Equality((Object) this.worldMaps[this.validRegionInfo[index].data.worldId - 1], (Object) null))
      {
        Transform transform2 = this.worldMaps[this.validRegionInfo[index].data.worldId - 1].transform;
        LoadObject loadObject = regionAreaLOs[index];
        if (loadObject != null && Object.op_Inequality((Object) null, loadObject.loadedObject))
        {
          this.regionAreas[index] = ResourceUtility.Realizes(loadObject.loadedObject, transform2);
          ((Component) this.regionAreas[index]).gameObject.SetActive(false);
        }
      }
    }
    for (int index = 0; index < this.worldMaps.Length; ++index)
      this.worldMaps[index].SetActive(index == this.currentWorldIndex);
    this.isInWorldMap = FieldManager.IsInWorldMap(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
    bool is_visible = MonoBehaviourSingleton<WorldMapManager>.I.IsExistedWorld2();
    this.SetActive(this.spots.spotRootTransform, (Enum) WorldMap.UI.OBJ_WORLD_SELECT, is_visible);
    if (is_visible)
      this.SetupChapterUI();
    if (this.isInGame)
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.InitMapSprite);
    base.Initialize();
  }

  private void SyncWorldMapSelectAntors(UIWidget w)
  {
    if (!SpecialDeviceManager.HasSpecialDeviceInfo || !SpecialDeviceManager.SpecialDeviceInfo.HasSafeArea)
      return;
    DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
    w.leftAnchor.absolute = specialDeviceInfo.WorldMapWorldSelectAnchor.left;
    w.rightAnchor.absolute = specialDeviceInfo.WorldMapWorldSelectAnchor.right;
    w.bottomAnchor.absolute = specialDeviceInfo.WorldMapWorldSelectAnchor.bottom;
    w.topAnchor.absolute = specialDeviceInfo.WorldMapWorldSelectAnchor.top;
    w.UpdateAnchors();
  }

  private void SetSelectorDepth(Transform parent, int baseDepth)
  {
    UIPanel component1 = ((Component) this.FindCtrl(parent, (Enum) WorldMap.UI.OBJ_SELECTOR)).GetComponent<UIPanel>();
    if (Object.op_Inequality((Object) component1, (Object) null))
      component1.depth = baseDepth + 1;
    UIPanel component2 = ((Component) this.FindCtrl(parent, (Enum) WorldMap.UI.SCR_SELECTOR)).GetComponent<UIPanel>();
    if (Object.op_Inequality((Object) component2, (Object) null))
      component2.depth = baseDepth + 2;
    UIPanel component3 = ((Component) this.FindCtrl(parent, (Enum) WorldMap.UI.OBJ_ARROW)).GetComponent<UIPanel>();
    if (Object.op_Inequality((Object) component3, (Object) null))
      component3.depth = baseDepth + 3;
    UIPanel component4 = ((Component) this.FindCtrl(parent, (Enum) WorldMap.UI.OBJ_FRAME)).GetComponent<UIPanel>();
    if (!Object.op_Inequality((Object) component4, (Object) null))
      return;
    component4.depth = baseDepth + 4;
  }

  private void DragChapter(GameObject go)
  {
    for (int index = 0; index < 4; ++index)
    {
      if (((Object) this.chapterContentList[index]).name == ((Object) go).name)
      {
        this.currentCenterIndex = index;
        break;
      }
    }
    if (this.currentWorldIndex == this.contentWorldIndex[this.currentCenterIndex])
      return;
    this.currentWorldIndex = this.contentWorldIndex[this.currentCenterIndex];
    this.ChangeActiveWorld();
  }

  private void SetupChapterContentTexture(Transform parent, Texture2D world1, Texture2D world2)
  {
    if (this.currentWorldIndex == 0)
    {
      this.SetTexture(parent, (Enum) WorldMap.UI.TEX_CON1, (Texture) world1);
      this.SetTexture(parent, (Enum) WorldMap.UI.TEX_CON2, (Texture) world2);
      this.SetTexture(parent, (Enum) WorldMap.UI.TEX_CON3, (Texture) world1);
      this.SetTexture(parent, (Enum) WorldMap.UI.TEX_CON4, (Texture) world2);
      this.contentWorldIndex = new int[4]{ 0, 1, 0, 1 };
    }
    else
    {
      this.SetTexture(parent, (Enum) WorldMap.UI.TEX_CON1, (Texture) world2);
      this.SetTexture(parent, (Enum) WorldMap.UI.TEX_CON2, (Texture) world1);
      this.SetTexture(parent, (Enum) WorldMap.UI.TEX_CON3, (Texture) world2);
      this.SetTexture(parent, (Enum) WorldMap.UI.TEX_CON4, (Texture) world1);
      this.contentWorldIndex = new int[4]{ 1, 0, 1, 0 };
    }
  }

  private void SetupChapterUI()
  {
    Transform spotRootTransform = this.spots.spotRootTransform;
    this.SetActive(spotRootTransform, (Enum) WorldMap.UI.SPR_INACTIVE_ARROW_L, false);
    this.SetActive(spotRootTransform, (Enum) WorldMap.UI.OBJ_ACTIVE_ARROW_L, true);
    this.SetActive(spotRootTransform, (Enum) WorldMap.UI.SPR_INACTIVE_ARROW_R, false);
    this.SetActive(spotRootTransform, (Enum) WorldMap.UI.OBJ_ACTIVE_ARROW_R, true);
  }

  public void InitRegionInfo()
  {
    if (this.spots == null)
      return;
    Transform spotRootTransform = this.spots.spotRootTransform;
    if (Object.op_Equality((Object) this.uiMapSprite, (Object) null))
      this.uiMapSprite = ((Component) spotRootTransform.Find("Map")).gameObject.GetComponent<UITexture>();
    if (Object.op_Equality((Object) this.mapTween, (Object) null))
      this.mapTween = ((Component) spotRootTransform.Find("Map")).gameObject.GetComponent<TweenAlpha>();
    this.InitMapSprite(false);
    if (this.currentWorldIndex >= 0)
      this.worldMaps[this.currentWorldIndex].SetActive(true);
    for (int index = 0; index < this.validRegionInfo.Length; ++index)
    {
      RegionTable.Data data = this.validRegionInfo[index].data;
      if (data != null && this.currentWorldIndex == data.worldId - 1 && this.currentDifficulty == data.difficulty)
      {
        int mapNo = Singleton<RegionTable>.I.GetMapNo((int) data.regionId);
        string event_name = "OPEN_REGION";
        string regionName = data.regionName;
        if (this.validRegionInfo[index].status == WorldMap.REGION_STATUS.CLOSE)
        {
          if (this.validRegionInfo[index].data.difficulty == REGION_DIFFICULTY_TYPE.NORMAL)
          {
            event_name = "RELEASE_REGION";
            if (this.isInGame)
              event_name = "INGAME_RELEASE_REGION";
          }
          else if (this.validRegionInfo[index].data.difficulty == REGION_DIFFICULTY_TYPE.HARD)
            event_name = !this.isInGame ? "HARD_NOT_OPEN" : "INGAME_HARD_NOT_OPEN";
        }
        SpotManager.Spot spot = this.spots.AddSpot((int) data.regionId, regionName, data.iconPos, SpotManager.ICON_TYPE.CLEARED, event_name, _event: (object) (int) data.regionId, mapNo: mapNo);
        spot.SetIconSprite("SPR_ICON", this.validRegionInfo[index].icon.loadedObject as Texture2D, (int) data.iconSize.x, (int) data.iconSize.y);
        if ((long) this.currentRegionID == (long) data.regionId && this.isInWorldMap)
        {
          ((Component) this.playerMarker).gameObject.SetActive(true);
          this.playerMarker.SetParent(this.worldMaps[this.currentWorldIndex].transform);
          PlayerMarker component = ((Component) this.playerMarker).GetComponent<PlayerMarker>();
          component.SetWorldMode(true);
          component.SetCamera(((Component) this.worldMapCamera._camera).transform);
          this.playerMarker.localPosition = data.markerPos;
        }
        if (this.releaseRegionId == (int) data.regionId)
        {
          ((Component) spot._transform).gameObject.SetActive(false);
          if (this.isInWorldMap)
            ((Component) this.playerMarker).gameObject.SetActive(false);
        }
      }
    }
  }

  private void InitMapSprite(bool isPortrait)
  {
    if (!Object.op_Inequality((Object) this.uiMapSprite, (Object) null))
      return;
    if (Object.op_Equality((Object) null, (Object) this.worldMapCamera._camera.targetTexture))
      this.worldMapCamera.Restore();
    this.uiMapSprite.mainTexture = (Texture) this.worldMapCamera._camera.targetTexture;
    this.uiMapSprite.width = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth;
    this.uiMapSprite.height = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight;
  }

  protected override void OnOpen()
  {
    if (this.currentWorldIndex >= 0)
      this.worldMaps[this.currentWorldIndex].SetActive(true);
    if ((double) ((Component) this).transform.localScale.y > 1.0)
      ((Component) this).transform.localScale = Vector3.one;
    RegionTable.Data[] data = Singleton<RegionTable>.I.GetData();
    if (!this.isInWorldMap)
      this.worldMapCamera.targetPos = data[0].iconPos;
    else if (0 <= this.currentRegionID && data.Length > this.currentRegionID)
      this.worldMapCamera.targetPos = data[this.currentRegionID].iconPos;
    Transform transform = this.spots.spotRootTransform.Find("CLOSE_BTN/OBJ_CLOSE_BTN_ROOT");
    UIWidget widget = (UIWidget) null;
    if (Object.op_Inequality((Object) transform, (Object) null))
    {
      widget = ((Component) transform).GetComponent<UIWidget>();
      if (Object.op_Inequality((Object) widget, (Object) null))
      {
        widget.alpha = 0.0f;
        ((Component) widget).transform.localScale = Vector3.zero;
      }
    }
    Transform ctrl = this.FindCtrl(this.spots.spotRootTransform, (Enum) WorldMap.UI.OBJ_WORLD_SELECT);
    UIWidget selectWidget = (UIWidget) null;
    if (Object.op_Inequality((Object) ctrl, (Object) null))
    {
      selectWidget = ((Component) ctrl).GetComponent<UIWidget>();
      if (Object.op_Inequality((Object) selectWidget, (Object) null))
        selectWidget.alpha = 0.0f;
      ((Component) selectWidget).gameObject.SetActive(false);
    }
    this.FadeInMap((System.Action) (() =>
    {
      ((Component) selectWidget).gameObject.SetActive(true);
      this.InitRegionInfo();
      if (Object.op_Inequality((Object) widget, (Object) null))
      {
        widget.alpha = 0.0f;
        ((Component) widget).transform.localScale = Vector3.zero;
      }
      if (Object.op_Inequality((Object) selectWidget, (Object) null))
        selectWidget.alpha = 0.0f;
      this.PlayTween(WorldMap.TWEEN_ANIMATION.OPENING);
      this.UpdateAreas();
      this.UpdateDifficultyButton();
      Singleton<RegionTable>.I.GetData((uint) this.releaseRegionId);
      if (MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() || GameSceneEvent.request != null || this.releaseRegionId <= 0)
        return;
      bool useReleaseRegion = false;
      if (MonoBehaviourSingleton<WorldMapManager>.I.releaseRegionIdfromBoard > 0)
      {
        useReleaseRegion = true;
        MonoBehaviourSingleton<WorldMapManager>.I.releaseRegionIdfromBoard = 0;
      }
      this.StartCoroutine(this.PlayOpenRegionMap(useReleaseRegion));
    }));
    this.collectUI = this._transform;
    this.isChangingMap = false;
    base.OnOpen();
  }

  protected override void OnCloseStart() => this.collectUI = (Transform) null;

  public override void Exit()
  {
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= new ScreenOrientationManager.OnScreenRotateDelegate(this.InitMapSprite);
    MonoBehaviourSingleton<FilterManager>.I.StopBlur();
    this.spots.ClearAllSpot();
    base.Exit();
  }

  private void PlayTween(WorldMap.TWEEN_ANIMATION type, EventDelegate.Callback onComplete = null)
  {
    UITweenCtrl uiTweenCtrl = Array.Find<UITweenCtrl>(this.tweenAnimations, (Predicate<UITweenCtrl>) (t => (WorldMap.TWEEN_ANIMATION) t.id == type));
    if (!Object.op_Inequality((Object) uiTweenCtrl, (Object) null))
      return;
    uiTweenCtrl.Reset();
    uiTweenCtrl.Play(onFinished: onComplete);
  }

  protected override void OnDestroy()
  {
    if (Object.op_Inequality((Object) this.blurFilter, (Object) null))
      Object.Destroy((Object) ((Component) this.blurFilter).gameObject);
    if (Object.op_Inequality((Object) this.worldMapUIRoot, (Object) null))
      Object.Destroy((Object) this.worldMapUIRoot);
    foreach (GameObject worldMap in this.worldMaps)
    {
      if (Object.op_Inequality((Object) worldMap, (Object) null))
        Object.Destroy((Object) worldMap);
    }
    base.OnDestroy();
  }

  public void OnQuery_OPEN_REGION()
  {
    this.UpdateDifficultyButton(true);
    int eventData = (int) GameSection.GetEventData();
    this.blurCenter = new Vector2(0.5f, 0.5f);
    SpotManager.Spot spot = this.spots.FindSpot(eventData);
    if (spot != null)
      this.blurCenter = spot.GetScreenPos();
    this.PlayTween(WorldMap.TWEEN_ANIMATION.ENDING);
    GameSection.StayEvent();
    this.blurFilter.CacheRenderTarget((System.Action) (() =>
    {
      ((Component) this.playerMarker).gameObject.SetActive(false);
      this.playerMarker.SetParent(this._transform);
      this.spots.ClearAllSpot();
      GameSection.ResumeEvent(true);
    }));
  }

  public void OnQuery_OPEN_REGION_CHANGE()
  {
    this.UpdateDifficultyButton(true);
    this.StopAllCoroutines();
    this.spots.ClearAllSpot();
    this.DisableWorldMapObject();
  }

  public void OnQuery_DIRECT_REGION()
  {
    this.UpdateDifficultyButton(true);
    this.StopAllCoroutines();
    GameSection.SetEventData((object) -1);
    this.spots.ClearAllSpot();
  }

  public void OnQuery_DIRECT_EVENT()
  {
    this.UpdateDifficultyButton(true);
    this.StopAllCoroutines();
    MonoBehaviourSingleton<WorldMapManager>.I.eventMapRegionID = (int) GameSection.GetEventData();
    GameSection.SetEventData((object) -4);
    this.spots.ClearAllSpot();
  }

  public void OnQuery_DIRECT_REGION_QUEST()
  {
    this.UpdateDifficultyButton(true);
    this.StopAllCoroutines();
    GameSection.SetEventData((object) -2);
    this.spots.ClearAllSpot();
  }

  public void OnQuery_DIRECT_REGION_TUTORIAL()
  {
    this.UpdateDifficultyButton(true);
    this.StopAllCoroutines();
    GameSection.SetEventData((object) -3);
    this.spots.ClearAllSpot();
  }

  private void OnQuery_SECTION_BACK()
  {
    GameSection.StayEvent();
    this.PlayTween(WorldMap.TWEEN_ANIMATION.ENDING, (EventDelegate.Callback) (() =>
    {
      if (Object.op_Inequality((Object) this.worldMapUIRoot, (Object) null))
        Object.Destroy((Object) this.worldMapUIRoot);
      foreach (GameObject worldMap in this.worldMaps)
      {
        if (Object.op_Inequality((Object) worldMap, (Object) null))
          worldMap.SetActive(false);
      }
      GameSection.ResumeEvent(true);
    }));
  }

  public void FadeInMap(System.Action onComplete)
  {
    if (this.currentWorldIndex >= 0 && Object.op_Inequality((Object) this.worldMaps[this.currentWorldIndex], (Object) null))
      this.worldMaps[this.currentWorldIndex].SetActive(true);
    if (Object.op_Inequality((Object) this.uiMapSprite, (Object) null))
      ((Component) this.uiMapSprite).gameObject.SetActive(true);
    this.StartCoroutine(this.DoFadeMap(0.0f, 1f, 0.4f, (System.Action) (() =>
    {
      if (onComplete == null)
        return;
      onComplete();
    })));
  }

  public void DisableWorldMapObject()
  {
    foreach (GameObject worldMap in this.worldMaps)
    {
      if (Object.op_Inequality((Object) worldMap, (Object) null))
        worldMap.SetActive(false);
    }
    if (!Object.op_Inequality((Object) this.uiMapSprite, (Object) null))
      return;
    ((Component) this.uiMapSprite).gameObject.SetActive(false);
  }

  private IEnumerator DoFadeMap(float from, float to, float time, System.Action onComplete)
  {
    if (this.currentWorldIndex >= 0 && !Object.op_Equality((Object) this.worldMaps[this.currentWorldIndex], (Object) null))
    {
      Renderer r = this.worldMaps[this.currentWorldIndex].GetComponentInChildren<Renderer>();
      if (!Object.op_Equality((Object) r, (Object) null))
      {
        Renderer[] areaRenderers = new Renderer[this.regionAreas.Length];
        for (int index = 0; index < areaRenderers.Length; ++index)
        {
          if (Object.op_Inequality((Object) null, (Object) this.regionAreas[index]))
          {
            Renderer component = ((Component) this.regionAreas[index]).GetComponent<Renderer>();
            if (Object.op_Inequality((Object) null, (Object) component))
              component.material.SetFloat("_Alpha", 0.0f);
            areaRenderers[index] = component;
          }
        }
        float timer;
        for (timer = 0.0f; (double) timer < (double) time; timer += Time.deltaTime)
        {
          if (Object.op_Equality((Object) null, (Object) r))
            yield break;
          r.material.SetFloat("_Alpha", Mathf.Lerp(from, to, timer / time));
          yield return (object) null;
        }
        r.material.SetFloat("_Alpha", to);
        timer = 0.0f;
        for (float alphaTime = 0.15f; (double) timer <= (double) alphaTime; timer += Time.deltaTime)
        {
          float num = Mathf.Lerp(0.0f, 1.2f, timer / alphaTime);
          for (int index = 0; index < areaRenderers.Length; ++index)
          {
            if (Object.op_Inequality((Object) null, (Object) areaRenderers[index]))
              areaRenderers[index].material.SetFloat("_Alpha", num);
          }
          yield return (object) null;
        }
        if (onComplete != null)
          onComplete();
      }
    }
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

  private bool IsExistedHard()
  {
    foreach (uint regionIdListInWorld in MonoBehaviourSingleton<WorldMapManager>.I.GetOpenRegionIdListInWorldMap())
    {
      RegionTable.Data data = Singleton<RegionTable>.I.GetData(regionIdListInWorld);
      if (data != null && data.worldId == this.currentWorldIndex + 1 && data.HasGroup())
        return true;
    }
    return false;
  }

  public void EnterRegionMapEvent(System.Action onCompleteFilter)
  {
    if (Object.op_Equality((Object) this.blurFilter, (Object) null))
    {
      if (onCompleteFilter == null)
        return;
      onCompleteFilter();
    }
    else
    {
      this.DisableWorldMapObject();
      this.blurFilter.StartBlurFilter(0.0f, 0.25f, 0.25f, this.blurCenter, (System.Action) (() =>
      {
        this.blurFilter.SetBlurPram(0.0f, this.blurCenter);
        onCompleteFilter();
        if (this.regionAreas == null)
          return;
        for (int index = 0; index < this.regionAreas.Length; ++index)
        {
          if (Object.op_Inequality((Object) null, (Object) this.regionAreas[index]))
            ((Component) this.regionAreas[index]).gameObject.SetActive(false);
        }
      }));
    }
  }

  private void OnQuery_NEXT_WORLD()
  {
    if (this.isChangingMap)
      return;
    this.UpdateDifficultyButton(true);
    if (this.currentCenterIndex + 1 >= 4)
      this.currentCenterIndex = 0;
    else
      ++this.currentCenterIndex;
    this.center.CenterOn(this.chapterContentList[this.currentCenterIndex]);
  }

  private void OnQuery_PREV_WORLD()
  {
    if (this.isChangingMap)
      return;
    this.UpdateDifficultyButton(true);
    if (this.currentCenterIndex <= 0)
      this.currentCenterIndex = 3;
    else
      --this.currentCenterIndex;
    this.center.CenterOn(this.chapterContentList[this.currentCenterIndex]);
  }

  private void ChangeActiveWorld()
  {
    this.isChangingMap = true;
    this.spots.ClearAllSpot();
    for (int index = 0; index < this.worldMaps.Length; ++index)
      this.worldMaps[index].SetActive(index == this.currentWorldIndex);
    if (this.currentDifficulty == REGION_DIFFICULTY_TYPE.HARD && !this.IsExistedHard())
      this.currentDifficulty = REGION_DIFFICULTY_TYPE.NORMAL;
    RegionTable.Data data1 = Singleton<RegionTable>.I.GetData((uint) this.currentRegionID);
    if (data1 != null)
    {
      if (data1.worldId == this.currentWorldIndex + 1)
        this.worldMapCamera.targetPos = data1.iconPos;
      else if (this.currentWorldIndex == 0)
        this.worldMapCamera.targetPos = Singleton<RegionTable>.I.GetData(0U).iconPos;
      else if (this.currentWorldIndex == 1)
      {
        RegionTable.Data data2 = Singleton<RegionTable>.I.GetData(9U);
        if (data2 != null)
          this.worldMapCamera.targetPos = data2.iconPos;
      }
    }
    this.ChangeActiveArea();
  }

  private IEnumerator ChangeFadeMap()
  {
    ((Behaviour) this.mapTween).enabled = true;
    this.mapTween.ResetToBeginning();
    this.mapTween.PlayForward();
    yield return (object) new WaitForSeconds(this.mapTween.duration / 2f);
    for (int index = 0; index < this.worldMaps.Length; ++index)
      this.worldMaps[index].SetActive(index == this.currentWorldIndex);
    this.worldMaps[this.currentWorldIndex].GetComponent<Animator>().Play(Animator.StringToHash("Take 001"), 0, 1f);
  }

  private void ChangeActiveArea()
  {
    this.UpdateAreas();
    this.StartCoroutine(this.DoFadeMap(0.0f, 1f, 0.4f, (System.Action) (() =>
    {
      this.InitRegionInfo();
      this.isChangingMap = false;
      this.UpdateDifficultyButton();
    })));
  }

  private void OnApplicationPause(bool paused) => this.isUpdateRenderTexture = !paused;

  private void OnCloseDialog_WorldMapSelectDifficultyDialog()
  {
    object eventData = GameSection.GetEventData();
    if (eventData == null)
      return;
    REGION_DIFFICULTY_TYPE regionDifficultyType = (REGION_DIFFICULTY_TYPE) eventData;
    if (regionDifficultyType == this.currentDifficulty)
      return;
    this.currentDifficulty = regionDifficultyType;
    this.UpdateDifficultyButton();
    this.spots.ClearAllSpot();
    this.InitRegionInfo();
    this.UpdateAreas();
  }

  private void OnCloseDialog_InGameSelectDifficultyDialog()
  {
    object eventData = GameSection.GetEventData();
    if (eventData == null)
      return;
    REGION_DIFFICULTY_TYPE regionDifficultyType = (REGION_DIFFICULTY_TYPE) eventData;
    if (regionDifficultyType == this.currentDifficulty)
      return;
    this.currentDifficulty = regionDifficultyType;
    this.UpdateDifficultyButton();
    this.spots.ClearAllSpot();
    this.InitRegionInfo();
    this.UpdateAreas();
  }

  private void UpdateAreas()
  {
    if (this.regionAreas == null)
      return;
    for (int index = 0; index < this.regionAreas.Length; ++index)
    {
      if (!Object.op_Equality((Object) null, (Object) this.regionAreas[index]))
      {
        if (this.validRegionInfo[index].status == WorldMap.REGION_STATUS.OPEN && this.currentWorldIndex == this.validRegionInfo[index].data.worldId - 1 && this.currentDifficulty == this.validRegionInfo[index].data.difficulty && (int) this.validRegionInfo[index].data.regionId != this.releaseRegionId)
          ((Component) this.regionAreas[index]).gameObject.SetActive(true);
        else
          ((Component) this.regionAreas[index]).gameObject.SetActive(false);
      }
    }
  }

  private void UpdateDifficultyButton(bool forceOff = false)
  {
    Transform spotRootTransform = this.spots.spotRootTransform;
    if (forceOff)
    {
      this.SetActive(spotRootTransform, (Enum) WorldMap.UI.OBJ_SELECT_DIFFICULTY, false);
    }
    else
    {
      bool is_visible = this.IsExistedHard();
      this.SetActive(spotRootTransform, (Enum) WorldMap.UI.OBJ_SELECT_DIFFICULTY, is_visible);
      if (!is_visible)
        return;
      this.SetActive(spotRootTransform, (Enum) WorldMap.UI.BTN_CURRENT_DIFFICULTY_NORMAL, this.currentDifficulty == REGION_DIFFICULTY_TYPE.NORMAL);
      this.SetActive(spotRootTransform, (Enum) WorldMap.UI.BTN_CURRENT_DIFFICULTY_HARD, this.currentDifficulty == REGION_DIFFICULTY_TYPE.HARD);
      UIWidget component = ((Component) this.FindCtrl(spotRootTransform, (Enum) WorldMap.UI.OBJ_SELECT_DIFFICULTY)).GetComponent<UIWidget>();
      if (Object.op_Equality((Object) component, (Object) null))
        return;
      this.StartCoroutine(this.FadeWidget(component, 0, 1, 0.3f));
    }
  }

  private IEnumerator FadeWidget(UIWidget target, int start, int end, float duration)
  {
    float time = 0.0f;
    while ((double) time < (double) duration)
    {
      time += Time.deltaTime;
      target.alpha = Mathf.Lerp((float) start, (float) end, time / duration);
      yield return (object) null;
    }
  }

  private void OnQuery_SELECT_DIFFICULTY()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) false,
      (object) true
    });
    if (!this.isInGame)
      return;
    GameSection.ChangeEvent("INGAME_SELECT_DIFFICULTY");
  }

  private void OnQuery_RELEASE_REGION()
  {
    this.releaseRegionId = (int) GameSection.GetEventData();
    uint[] idListInWorldMap = MonoBehaviourSingleton<WorldMapManager>.I.GetOpenRegionIdListInWorldMap();
    GameSection.StayEvent();
    if (idListInWorldMap.Length <= 1)
    {
      GameSection.ChangeStayEvent("INVALID_RELEASE");
      this.releaseRegionId = -1;
      GameSection.ResumeEvent(true);
    }
    else
      MonoBehaviourSingleton<WorldMapManager>.I.SendRegionCrystalNum(this.releaseRegionId, (Action<bool, string>) ((isSuccess, campainText) => GameSection.ResumeEvent((isSuccess ? 1 : 0) != 0, (object) new object[2]
      {
        (object) MonoBehaviourSingleton<WorldMapManager>.I.releaseCrystalNum.ToString(),
        (object) campainText
      })));
  }

  private void OnQuery_WorldMapReleaseRegionDialog_YES()
  {
    if (this.releaseRegionId < 0)
      return;
    Singleton<RegionTable>.I.GetData((uint) this.releaseRegionId);
    GameSection.StayEvent();
    MonoBehaviourSingleton<WorldMapManager>.I.SendRegionOpen(this.releaseRegionId, (Action<bool>) (isSuccess =>
    {
      GameSection.ResumeEvent(isSuccess);
      if (!isSuccess)
        return;
      WorldMap.ValidRegionInfo validRegionInfo = this.validRegionInfo[this.releaseRegionId];
      validRegionInfo.status = WorldMap.REGION_STATUS.OPEN;
      validRegionInfo.icon = validRegionInfo.releaseIcon;
      this.StartCoroutine(this.PlayOpenRegionMap(true));
    }));
  }

  private IEnumerator PlayOpenRegionMap(bool useReleaseRegion)
  {
    if (this.releaseRegionId >= 0)
    {
      this.worldMapCamera.isInteractive = false;
      this.playingReleaseRegion = true;
      GameSaveData.instance.AddShowedOpenRegionId(this.releaseRegionId);
      Transform closeBtn = Utility.Find(this.spots.spotRootTransform, "CLOSE_BTN");
      if (Object.op_Inequality((Object) null, (Object) closeBtn))
        ((Component) closeBtn).gameObject.SetActive(false);
      Transform worldSelector = this.FindCtrl(this.spots.spotRootTransform, (Enum) WorldMap.UI.OBJ_WORLD_SELECT);
      bool existWorldSelect = ((Component) worldSelector).gameObject.activeSelf;
      this.SetActive(worldSelector, false);
      this.UpdateDifficultyButton(true);
      MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.CAMERA_ACTION, true);
      this.toRegionId = this.releaseRegionId;
      yield return (object) this.StartCoroutine(this.InitializeOpenRegion());
      Vector3 to = new Vector3(0.0f, 0.0f, 0.0f);
      RegionTable.Data data = Singleton<RegionTable>.I.GetData((uint) this.toRegionId);
      if (data != null)
        to = data.iconPos;
      yield return (object) new WaitForSeconds(0.5f);
      Vector3Interpolator ip = new Vector3Interpolator();
      Vector3 zoomDownTo = Vector3.op_Addition(to, new Vector3(0.0f, 0.0f, -3f));
      ip.Set(1f, this.worldMapCamera.targetPos, zoomDownTo, (AnimationCurve) null, new Vector3(), (AnimationCurve) null);
      ip.Play();
      while (ip.IsPlaying())
      {
        ip.Update();
        this.worldMapCamera.targetPos = ip.Get();
        yield return (object) null;
      }
      Transform regionArea = this.regionAreas[this.toRegionId];
      ((Component) regionArea).gameObject.SetActive(true);
      Renderer toRegionRenderer = ((Component) regionArea).GetComponent<Renderer>();
      toRegionRenderer.material.SetFloat("_Alpha", 0.0f);
      Renderer topRenderer = ((Component) this.glowRegionTop).GetComponent<Renderer>();
      topRenderer.material.SetFloat("_Alpha", 0.0f);
      topRenderer.material.SetFloat("_AddColor", 1f);
      topRenderer.material.SetFloat("_BlendRate", 1f);
      topRenderer.sortingOrder = 2;
      ((Component) this.glowRegionTop).gameObject.SetActive(true);
      yield return (object) new WaitForSeconds(1f);
      ((Component) this.mapGlowEffectA).gameObject.SetActive(true);
      ((Component) this.mapGlowEffectA).GetComponent<Renderer>().sortingOrder = 1;
      SpotManager.Spot toSpot = this.spots.GetSpot(this.toRegionId);
      ((Component) toSpot._transform).gameObject.SetActive(false);
      toSpot.ReleaseRegion(Singleton<RegionTable>.I.GetData((uint) this.toRegionId).regionName, this.validRegionInfo[this.toRegionId].releaseIcon.loadedObject as Texture2D, "OPEN_REGION");
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
      SoundManager.PlayOneShotUISE(WorldMap.SE_ID_SMOKE);
      while (fip.IsPlaying())
      {
        double num = (double) fip.Update();
        topRenderer.material.SetFloat("_Alpha", fip.Get());
        yield return (object) null;
      }
      toRegionRenderer.material.SetFloat("_Alpha", 1f);
      this.mapGlowEffectParticleA.Stop();
      ((Component) this.mapGlowEffectParticleA).gameObject.SetActive(false);
      ((Component) this.mapGlowEffectB).gameObject.SetActive(true);
      yield return (object) null;
      fip.Set(0.2f, 1f, 0.0f, (AnimationCurve) null, 0.0f, (AnimationCurve) null);
      fip.Play();
      while (fip.IsPlaying())
      {
        double num = (double) fip.Update();
        topRenderer.material.SetFloat("_Alpha", fip.Get());
        yield return (object) null;
      }
      yield return (object) null;
      ((Component) toSpot._transform).gameObject.SetActive(true);
      ((Component) toSpot._transform).GetComponent<TweenScale>().PlayForward();
      yield return (object) new WaitForSeconds(1f);
      this.mapGlowEffectParticleB.Stop();
      ((Component) this.mapGlowEffectParticleB).gameObject.SetActive(false);
      bool isTweenEnd = false;
      ((Component) this.telop).gameObject.SetActive(true);
      UITweenCtrl component = ((Component) this.telop).GetComponent<UITweenCtrl>();
      component.Reset();
      component.Play(onFinished: (EventDelegate.Callback) (() => isTweenEnd = true));
      SoundManager.PlayOneShotUISE(WorldMap.SE_ID_LOGO);
      while (!isTweenEnd)
        yield return (object) null;
      ((Component) this.mapGlowEffectA).gameObject.SetActive(false);
      ((Component) this.mapGlowEffectB).gameObject.SetActive(false);
      yield return (object) new WaitForSeconds(0.6f);
      ((Component) this.telop).gameObject.SetActive(false);
      this.UpdateDifficultyButton();
      if (Object.op_Inequality((Object) null, (Object) closeBtn))
        ((Component) closeBtn).gameObject.SetActive(true);
      this.SetActive(worldSelector, existWorldSelect);
      this.playingReleaseRegion = false;
      this.worldMapCamera.isInteractive = true;
      MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.CAMERA_ACTION, false);
      if (useReleaseRegion)
      {
        this.DispatchEvent("SUMMARY_CONFIRM");
      }
      else
      {
        this.releaseRegionId = -1;
        WorldMapManager.TransferInfo transferInfo = MonoBehaviourSingleton<WorldMapManager>.I.transferInfo;
        if (transferInfo != null)
        {
          if (!transferInfo.nextInGame)
          {
            this.DispatchEvent("OPEN_REGION", (object) transferInfo.nextRegionId);
            MonoBehaviourSingleton<WorldMapManager>.I.transferInfo = (WorldMapManager.TransferInfo) null;
          }
          else
          {
            this.DispatchEvent("INGAME_MAIN");
            MonoBehaviourSingleton<WorldMapManager>.I.transferInfo = (WorldMapManager.TransferInfo) null;
          }
        }
      }
    }
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

  private IEnumerator InitializeOpenRegion()
  {
    if (!this.regionOpenInitialized)
    {
      LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
      LoadObject loadedMapGlowEffectA = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "MapGlowEffectA");
      LoadObject loadedMapGlowEffectB = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "MapGlowEffectB");
      LoadObject loadedTelop = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "TelopOpenRegion");
      loadingQueue.CacheSE(WorldMap.SE_ID_LOGO);
      loadingQueue.CacheSE(WorldMap.SE_ID_SMOKE);
      LoadObject loadedMaterial = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "WorldMapPartGlow" + this.toRegionId.ToString("D3"));
      if (loadingQueue.IsLoading())
        yield return (object) loadingQueue.Wait();
      this.mapGlowEffectA = ResourceUtility.Realizes(loadedMapGlowEffectA.loadedObject, this._transform);
      ((Component) this.mapGlowEffectA).gameObject.SetActive(false);
      this.mapGlowEffectParticleA = ((Component) this.mapGlowEffectA).GetComponent<ParticleSystem>();
      this.mapGlowEffectB = ResourceUtility.Realizes(loadedMapGlowEffectB.loadedObject, this._transform);
      ((Component) this.mapGlowEffectB).gameObject.SetActive(false);
      this.mapGlowEffectParticleB = ((Component) this.mapGlowEffectB).GetComponent<ParticleSystem>();
      if (loadedMaterial != null)
        this.glowMaterial = loadedMaterial.loadedObject as Material;
      if (Object.op_Equality((Object) this.telop, (Object) null))
        this.telop = ResourceUtility.Realizes(loadedTelop.loadedObject, this.spots.spotRootTransform);
      this.regionOpenInitialized = true;
      loadedMapGlowEffectA = (LoadObject) null;
      loadedMapGlowEffectB = (LoadObject) null;
      loadedTelop = (LoadObject) null;
      loadedMaterial = (LoadObject) null;
    }
    Transform regionArea = this.regionAreas[this.toRegionId];
    ((Component) regionArea).gameObject.SetActive(false);
    this.mapGlowEffectA.SetParent(regionArea);
    this.mapGlowEffectA.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    this.mapGlowEffectB.SetParent(regionArea);
    this.mapGlowEffectB.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    ParticleSystem.ShapeModule shape = this.mapGlowEffectParticleB.shape;
    MeshFilter component = ((Component) regionArea).GetComponent<MeshFilter>();
    ((ParticleSystem.ShapeModule) ref shape).mesh = component.sharedMesh;
    this.glowRegionTop = ResourceUtility.Realizes((Object) ((Component) regionArea).gameObject, this._transform);
    ((Component) this.glowRegionTop).gameObject.SetActive(false);
    this.glowRegionTop.localPosition = Vector3.op_Addition(this.glowRegionTop.localPosition, new Vector3(0.0f, 0.0f, 1f / 1000f));
    this.glowRegionTop.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    ((Component) this.glowRegionTop).GetComponent<Renderer>().material = this.glowMaterial;
  }

  private void OnQuery_WorldMapReleaseRegionDialog_NO() => this.releaseRegionId = -1;

  private void OnQuery_WorldMapSummaryConfirmDialog_YES()
  {
    GameSection.SetEventData((object) this.releaseRegionId);
    this.releaseRegionId = -1;
  }

  private void OnQuery_WorldMapSummaryConfirmDialog_NO() => this.releaseRegionId = -1;

  private void Update()
  {
    if (Object.op_Equality((Object) this.worldMapCamera, (Object) null))
      return;
    if (Object.op_Inequality((Object) this.chapterScrollView, (Object) null))
    {
      if (this.chapterScrollView.isPressing)
      {
        this.worldMapCamera.isInteractive = false;
        this.beforePressed = this.chapterScrollView.isPressing;
        return;
      }
      if (this.beforePressed != this.chapterScrollView.isPressing)
      {
        this.worldMapCamera.isInteractive = true;
        this.beforePressed = this.chapterScrollView.isPressing;
      }
    }
    if (this.playingReleaseRegion)
    {
      this.worldMapCamera.isInteractive = false;
    }
    else
    {
      string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
      if (!(currentSectionName != this.beforeSectionName))
        return;
      this.worldMapCamera.isInteractive = currentSectionName == nameof (WorldMap) || currentSectionName == "RegionMap";
      this.beforeSectionName = currentSectionName;
    }
  }

  protected enum UI
  {
    OBJ_WORLD_SELECT,
    OBJ_SELECT_DIFFICULTY,
    BTN_CURRENT_DIFFICULTY_NORMAL,
    BTN_CURRENT_DIFFICULTY_HARD,
    OBJ_WORLDS,
    SPR_WORLD_FRAME,
    OBJ_ACTIVE_ARROW_R,
    OBJ_ACTIVE_ARROW_L,
    SPR_INACTIVE_ARROW_R,
    SPR_INACTIVE_ARROW_L,
    OBJ_WRAP_CENTER,
    SPR_CONTENT1,
    SPR_CONTENT2,
    SPR_CONTENT3,
    SPR_CONTENT4,
    TEX_CON1,
    TEX_CON2,
    TEX_CON3,
    TEX_CON4,
    SCR_SELECTOR,
    OBJ_SELECTOR,
    OBJ_ARROW,
    OBJ_FRAME,
  }

  private enum TWEEN_ANIMATION
  {
    OPENING,
    ENDING,
  }

  private class ValidRegionInfo
  {
    public RegionTable.Data data;
    public LoadObject icon;
    public LoadObject releaseIcon;
    public WorldMap.REGION_STATUS status;

    public ValidRegionInfo(
      RegionTable.Data _data,
      LoadObject _icon,
      LoadObject _releaseIcon,
      WorldMap.REGION_STATUS _status)
    {
      this.data = _data;
      this.icon = _icon;
      this.releaseIcon = _releaseIcon;
      this.status = _status;
    }
  }

  private enum REGION_STATUS
  {
    OPEN,
    CLOSE,
  }
}
