// Decompiled with JetBrains decompiler
// Type: RegionMap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class RegionMap : GameSection
{
  private UITweenCtrl[] tweenAnimations;
  private SpotManager spots;
  private RegionMapRoot regionMapRoot;
  private UITexture uiMapSprite;
  private UITexture uiParentMapSprite;
  private WorldMap parent;
  private Transform playerMarker;
  private rymFX windEffect;
  private Vector3 redCircleOrgPos;
  private Transform redCircle;
  private Transform portalGuideTxt;
  private int regionId;
  private bool directOpen;
  private bool isTutorial;
  private bool isEventMap;
  private bool displayQuestTargetMode;
  private int questTargetMapID;
  private int[] questTargetPortalIDs;
  private List<uint> enemyPopBallonMapIds;
  public const int NON_EVENT_OPEN_EVENT = -1;
  public const int QUEST_TARGET_DISPLAY_EVENT = -2;
  public const int TUTORIAL_EVENT = -3;
  public const int FROM_EVENT_QUEST_EVENT = -4;
  private bool isInGame;
  private bool isUpdateRenderTexture;
  private bool IsCalledExit;
  private Transform rootTransform;
  private HashSet<uint> DeliveryTargetEnemyIds = new HashSet<uint>();
  private HashSet<uint> DeliveryTargetMapIds = new HashSet<uint>();
  private bool isShowDescription = true;
  private UIPanel rootPanel;
  private int defaultDepth;
  private GameObject announceTap;
  private UIWidget closeButton;
  private bool isOpened;
  private bool isToDescription;
  private Dictionary<SpotManager.Spot, uint> spotMapIdDic = new Dictionary<SpotManager.Spot, uint>();
  private bool isOpenedHard;

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "FieldMapTable";
      yield return "RegionTable";
      yield return "QuestToFieldtable";
    }
  }

  public override string overrideBackKeyEvent => "CLOSE";

  public override void Initialize()
  {
    this.isInGame = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene";
    this.StartCoroutine("DoInitialize");
  }

  private IEnumerator DoInitialize()
  {
    int eventData = (int) GameSection.GetEventData();
    FieldMapTable.FieldMapTableData fieldMapTableData = (FieldMapTable.FieldMapTableData) null;
    switch (eventData)
    {
      case -4:
        this.regionId = MonoBehaviourSingleton<WorldMapManager>.I.eventMapRegionID;
        break;
      case -3:
      case -2:
        this.directOpen = true;
        this.isShowDescription = false;
        this.isTutorial = -3 == eventData;
        this.displayQuestTargetMode = MonoBehaviourSingleton<WorldMapManager>.I.isDisplayQuestTargetMode();
        if (this.displayQuestTargetMode)
          MonoBehaviourSingleton<WorldMapManager>.I.PopDisplayQuestTarget(out this.questTargetMapID, out this.questTargetPortalIDs);
        if (WorldMapManager.IsValidPortalIDs(this.questTargetPortalIDs))
        {
          FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData((uint) this.questTargetPortalIDs[0]);
          if (portalData != null)
            fieldMapTableData = Singleton<FieldMapTable>.I.GetFieldMapData(portalData.srcMapID);
        }
        else
          fieldMapTableData = Singleton<FieldMapTable>.I.GetFieldMapData((uint) this.questTargetMapID);
        this.isEventMap = FieldManager.IsEventMap(fieldMapTableData.mapID);
        break;
      case -1:
        this.directOpen = true;
        fieldMapTableData = Singleton<FieldMapTable>.I.GetFieldMapData(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
        break;
      default:
        this.regionId = eventData;
        break;
    }
    if (fieldMapTableData != null)
      this.regionId = (int) fieldMapTableData.regionId;
    this.isEventMap = FieldManager.IsEventRegion((uint) this.regionId);
    if (MonoBehaviourSingleton<WorldMapManager>.I.NeedDirectionOpenRegion(this.regionId))
    {
      base.Initialize();
    }
    else
    {
      LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
      LoadObject loadedLocationSpotRoot = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "LocationSpotRoot");
      LoadObject loadedLocationSpot = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "LocationSpot");
      LoadObject loadedPlayerMarker = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "PlayerMarker");
      LoadObject loadObj = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionMap_" + this.regionId.ToString("D3"));
      LoadObject loadedEffect = loadingQueue.LoadEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_bg_questmap_01");
      if (loadingQueue.IsLoading())
        yield return (object) loadingQueue.Wait();
      this.parent = MonoBehaviourSingleton<GameSceneManager>.I.FindSection("WorldMap") as WorldMap;
      Camera camera = this.parent.worldMapCamera._camera;
      this.spots = new SpotManager(loadedLocationSpotRoot.loadedObject as GameObject, loadedLocationSpot.loadedObject as GameObject, camera);
      this.spots.CreateSpotRoot();
      this.redCircle = this.spots.spotRootTransform.Find("RedCircle");
      this.portalGuideTxt = this.spots.spotRootTransform.Find("PortalGuideTxt");
      UIPanel component1 = ((Component) this.spots.spotRootTransform).GetComponent<UIPanel>();
      if (Object.op_Inequality((Object) component1, (Object) null))
        component1.depth = this.baseDepth + 1;
      this.regionMapRoot = ((Component) ResourceUtility.Realizes(loadObj.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform)).gameObject.GetComponent<RegionMapRoot>();
      if (Object.op_Inequality((Object) this.regionMapRoot, (Object) null))
      {
        bool wait = true;
        this.regionMapRoot.InitPortalStatus((System.Action) (() => wait = false));
        while (wait)
          yield return (object) null;
      }
      this.rootTransform = this.spots.SetRoot(this._transform);
      ((Component) this.rootTransform).gameObject.SetActive(true);
      this.rootPanel = ((Component) this.rootTransform).GetComponent<UIPanel>();
      if (Object.op_Inequality((Object) this.rootPanel, (Object) null))
        this.defaultDepth = this.rootPanel.depth;
      this.uiMapSprite = ((Component) this.rootTransform.Find("Map")).gameObject.GetComponent<UITexture>();
      this.uiParentMapSprite = ((Component) this.rootTransform.Find("ParentMap")).gameObject.GetComponent<UITexture>();
      this.InitMapSprite(false);
      if (!this.directOpen)
        this.StartShowingTween();
      this.windEffect = ((Component) ResourceUtility.Realizes(loadedEffect.loadedObject, ((Component) this.parent.worldMapCamera).transform)).gameObject.GetComponent<rymFX>();
      this.windEffect.Cameras = new Camera[1]
      {
        this.parent.worldMapCamera._camera
      };
      ((Component) this.windEffect).gameObject.layer = LayerMask.NameToLayer("WorldMap");
      ((Component) this.spots.spotRootTransform).GetComponent<UIPanel>().RebuildAllDrawCalls();
      this.tweenAnimations = ((Component) this.spots.spotRootTransform).GetComponentsInChildren<UITweenCtrl>();
      this.playerMarker = ResourceUtility.Realizes(loadedPlayerMarker.loadedObject);
      PlayerMarker component2 = ((Component) this.playerMarker).GetComponent<PlayerMarker>();
      if (Object.op_Inequality((Object) null, (Object) component2))
        component2.SetCamera(((Component) this.parent.worldMapCamera._camera).transform);
      this.SetFirstCameraPos();
      this.collectUI = this._transform;
      Transform transform1 = this._transform.Find("LocationSpotRoot/CLOSE_BTN");
      if (Object.op_Inequality((Object) transform1, (Object) null))
        this.closeButton = ((Component) transform1).GetComponent<UIWidget>();
      Transform transform2 = this._transform.Find("LocationSpotRoot/AnnounceTap");
      if (Object.op_Inequality((Object) transform2, (Object) null))
      {
        this.announceTap = ((Component) transform2).gameObject;
        this.SyncBorderTitleAnctors();
      }
      if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
        MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.InitMapSprite);
      this.UpdateDifficultyButton();
      this.InitDeliveryTargetIdLists();
      base.Initialize();
    }
  }

  private void SyncBorderTitleAnctors()
  {
    if (!SpecialDeviceManager.HasSpecialDeviceInfo || !SpecialDeviceManager.SpecialDeviceInfo.HasSafeArea)
      return;
    UIWidget componentInChildren = this.announceTap.GetComponentInChildren<UIWidget>();
    DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
    if (Object.op_Equality((Object) componentInChildren, (Object) null))
      return;
    componentInChildren.leftAnchor.absolute = specialDeviceInfo.RegionMapBorderTitleAnchor.left;
    componentInChildren.rightAnchor.absolute = specialDeviceInfo.RegionMapBorderTitleAnchor.right;
    componentInChildren.bottomAnchor.absolute = specialDeviceInfo.RegionMapBorderTitleAnchor.bottom;
    componentInChildren.topAnchor.absolute = specialDeviceInfo.RegionMapBorderTitleAnchor.top;
    componentInChildren.UpdateAnchors();
  }

  private void UpdateDifficultyButton(bool forceOff = false)
  {
    Transform spotRootTransform = this.spots.spotRootTransform;
    if (forceOff)
    {
      this.SetActive(spotRootTransform, (Enum) RegionMap.UI.OBJ_SELECT_DIFFICULTY, false);
    }
    else
    {
      RegionTable.Data data1 = Singleton<RegionTable>.I.GetData((uint) this.regionId);
      bool is_visible = this.IsExistedHard();
      if (is_visible && data1.difficulty != REGION_DIFFICULTY_TYPE.HARD)
      {
        RegionTable.Data data2 = Singleton<RegionTable>.I.GetData(data1.groupId, REGION_DIFFICULTY_TYPE.HARD);
        if (data2 != null)
        {
          this.isOpenedHard = MonoBehaviourSingleton<WorldMapManager>.I.IsOpenRegion(data2.regionId);
          is_visible = this.isOpenedHard;
        }
      }
      else
        this.isOpenedHard = data1.difficulty == REGION_DIFFICULTY_TYPE.HARD;
      this.SetActive(spotRootTransform, (Enum) RegionMap.UI.OBJ_SELECT_DIFFICULTY, is_visible);
      if (!is_visible)
        return;
      this.SetActive(spotRootTransform, (Enum) RegionMap.UI.BTN_CURRENT_DIFFICULTY_NORMAL, data1.difficulty == REGION_DIFFICULTY_TYPE.NORMAL);
      this.SetActive(spotRootTransform, (Enum) RegionMap.UI.BTN_CURRENT_DIFFICULTY_HARD, data1.difficulty == REGION_DIFFICULTY_TYPE.HARD);
      UIWidget component = ((Component) this.FindCtrl(spotRootTransform, (Enum) RegionMap.UI.OBJ_SELECT_DIFFICULTY)).GetComponent<UIWidget>();
      if (Object.op_Equality((Object) component, (Object) null))
        return;
      this.StartCoroutine(this.FadeWidget(component, 0, 1, 0.3f));
    }
  }

  private bool IsExistedHard()
  {
    RegionTable.Data data = Singleton<RegionTable>.I.GetData((uint) this.regionId);
    return data != null && (data.difficulty == REGION_DIFFICULTY_TYPE.HARD || data.HasGroup());
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

  private void StartShowingTween()
  {
    if (Object.op_Inequality((Object) this.uiMapSprite, (Object) null))
    {
      this.uiMapSprite.alpha = 0.0f;
      TweenAlpha.Begin(((Component) this.uiMapSprite).gameObject, 0.3f, 1f);
      ((Component) this.uiMapSprite).transform.localScale = Vector3.op_Multiply(Vector3.one, 0.8f);
      TweenScale.Begin(((Component) this.uiMapSprite).gameObject, 0.3f, Vector3.one);
    }
    if (!Object.op_Inequality((Object) this.uiParentMapSprite, (Object) null))
      return;
    this.uiParentMapSprite.alpha = 1f;
    TweenScale.Begin(((Component) this.uiParentMapSprite).gameObject, 0.3f, Vector3.op_Multiply(Vector3.one, 1.2f));
    TweenAlpha.Begin(((Component) this.uiParentMapSprite).gameObject, 0.3f, 0.0f);
  }

  private void InitMapSprite(bool isPortrait)
  {
    if (Object.op_Inequality((Object) this.uiMapSprite, (Object) null))
    {
      if (Object.op_Equality((Object) null, (Object) this.parent.worldMapCamera._camera.targetTexture))
        this.parent.worldMapCamera.Restore();
      this.uiMapSprite.mainTexture = (Texture) this.parent.worldMapCamera._camera.targetTexture;
      this.uiMapSprite.width = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth;
      this.uiMapSprite.height = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight;
    }
    if (!Object.op_Inequality((Object) this.uiParentMapSprite, (Object) null))
      return;
    if (Object.op_Equality((Object) null, (Object) this.parent.blurFilter.filteredTexture))
      this.parent.blurFilter.Restore();
    this.uiParentMapSprite.alpha = 0.0f;
    this.uiParentMapSprite.mainTexture = (Texture) this.parent.blurFilter.filteredTexture;
    this.uiParentMapSprite.width = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth;
    this.uiParentMapSprite.height = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight;
  }

  private void CreateVisitedLocationSpotIfNeed()
  {
    if (this.spots.Count >= 1)
      return;
    MonoBehaviourSingleton<FilterManager>.I.StopBlur(MonoBehaviourSingleton<OutGameSettingsManager>.I.questMap.cameraMoveTime);
    this.spotMapIdDic = new Dictionary<SpotManager.Spot, uint>();
    if (Object.op_Equality((Object) this.regionMapRoot, (Object) null))
      return;
    for (int index1 = 0; index1 < this.regionMapRoot.locations.Length; ++index1)
    {
      RegionMapLocation location = this.regionMapRoot.locations[index1];
      FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint) location.mapId);
      if (fieldMapData != null || location.mapId == 0)
      {
        if (fieldMapData != null)
        {
          if (!FieldManager.IsShowPortal(fieldMapData.jumpPortalID))
          {
            this.CreateLocationSpot(location, SpotManager.ICON_TYPE.INVISIBLE);
            continue;
          }
          if (!MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(fieldMapData))
          {
            this.CreateLocationSpot(location, SpotManager.ICON_TYPE.NOT_OPENED);
            continue;
          }
        }
        SpotManager.ICON_TYPE iconStatus = SpotManager.ICON_TYPE.CLEARED;
        bool attach_new_release_portal = false;
        bool isExistDelivery = false;
        SpotManager.HAPPEN_CONDITION happen = SpotManager.HAPPEN_CONDITION.NONE;
        if (location.portal.Length != 0)
        {
          for (int index2 = 0; index2 < location.portal.Length; ++index2)
          {
            RegionMapPortal regionMapPortal = location.portal[index2];
            if (!regionMapPortal.IsVisited() && regionMapPortal.IsShow())
              iconStatus = SpotManager.ICON_TYPE.NEW;
            if (!attach_new_release_portal)
            {
              int[] locationNumbers = this.GetLocationNumbers(((Object) regionMapPortal).name);
              if (index1 == locationNumbers[0] && GameSaveData.instance.isNewReleasePortal((uint) regionMapPortal.entranceId))
              {
                if (regionMapPortal.IsVisited())
                  GameSaveData.instance.newReleasePortals.Remove((uint) regionMapPortal.entranceId);
                else
                  attach_new_release_portal = true;
              }
              if (index1 == locationNumbers[1] && GameSaveData.instance.isNewReleasePortal((uint) regionMapPortal.exitId))
              {
                if (regionMapPortal.IsVisited())
                  GameSaveData.instance.newReleasePortals.Remove((uint) regionMapPortal.exitId);
                else
                  attach_new_release_portal = true;
              }
            }
          }
        }
        if (fieldMapData != null)
        {
          if (FieldManager.IsToHardPortal(fieldMapData.jumpPortalID))
          {
            iconStatus = SpotManager.ICON_TYPE.HARD;
            if (iconStatus == SpotManager.ICON_TYPE.NEW)
              iconStatus = SpotManager.ICON_TYPE.HARD_NEW;
          }
          if (fieldMapData.hasChildRegion && (long) fieldMapData.childRegionId != (long) this.regionId)
            iconStatus = SpotManager.ICON_TYPE.CHILD_REGION;
          if (this.isShowDescription)
          {
            isExistDelivery = this.IsExistDeliveryTarget(fieldMapData.mapID);
            happen = this.GetHappenCondition(fieldMapData.mapID);
          }
        }
        this.CreateLocationSpot(location, iconStatus, attach_new_release_portal, isExistDelivery, happen);
      }
    }
    for (int index = 0; index < this.regionMapRoot.portals.Length; ++index)
    {
      RegionMapPortal portal = this.regionMapRoot.portals[index];
      if (portal.IsVisited())
        portal.Open();
    }
  }

  private bool IsExistDeliveryTarget(uint mapId)
  {
    if (this.DeliveryTargetMapIds.Contains(mapId))
      return true;
    List<FieldMapTable.EnemyPopTableData> enemyPopList = Singleton<FieldMapTable>.I.GetEnemyPopList(mapId);
    int index = 0;
    for (int count = enemyPopList.Count; index < count; ++index)
    {
      if (this.DeliveryTargetEnemyIds.Contains(enemyPopList[index].enemyID))
        return true;
    }
    return false;
  }

  private SpotManager.HAPPEN_CONDITION GetHappenCondition(uint mapId)
  {
    Dictionary<uint, uint> questIdEventIdDic = Singleton<QuestToFieldTable>.I.GetQuestIdEventIdDic(mapId);
    if (questIdEventIdDic == null)
      return SpotManager.HAPPEN_CONDITION.NONE;
    bool flag = false;
    foreach (KeyValuePair<uint, uint> keyValuePair in questIdEventIdDic)
    {
      uint key = keyValuePair.Key;
      QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(key);
      if (questData != null && questData.IsMissionExist() && (keyValuePair.Value < 1U || MonoBehaviourSingleton<QuestManager>.I.IsEventPlayableWith((int) keyValuePair.Value, NetworkNative.getNativeVersionFromName())))
      {
        flag = true;
        ClearStatusQuest clearStatusQuestData = MonoBehaviourSingleton<QuestManager>.I.GetClearStatusQuestData(key);
        if (clearStatusQuestData == null)
          return SpotManager.HAPPEN_CONDITION.NOT_CLEAR;
        int index = 0;
        for (int count = clearStatusQuestData.missionStatus.Count; index < count; ++index)
        {
          if (clearStatusQuestData.missionStatus[index] <= 2)
            return SpotManager.HAPPEN_CONDITION.NOT_CLEAR;
        }
      }
    }
    return !flag ? SpotManager.HAPPEN_CONDITION.NONE : SpotManager.HAPPEN_CONDITION.ALL_CLEAR;
  }

  private void CreateLocationSpot(
    RegionMapLocation location,
    SpotManager.ICON_TYPE iconStatus = SpotManager.ICON_TYPE.CLEARED,
    bool attach_new_release_portal = false,
    bool isExistDelivery = false,
    SpotManager.HAPPEN_CONDITION happen = SpotManager.HAPPEN_CONDITION.NONE)
  {
    if (location.mapId == 0)
    {
      this.spots.AddSpot(0, MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionTextList().Find((Predicate<GameSceneTables.TextData>) (textData => textData.key == "STR_HOME")).text, ((Component) location).transform.position, SpotManager.ICON_TYPE.HOME, "HOME", _event: (object) 0);
    }
    else
    {
      string event_name = "SELECT";
      if (iconStatus == SpotManager.ICON_TYPE.NOT_OPENED)
        event_name = string.Empty;
      if (iconStatus == SpotManager.ICON_TYPE.CHILD_REGION)
        event_name = "SELECT_CHILD";
      FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint) location.mapId);
      if (fieldMapData == null)
        return;
      bool viewEnemyPopBallon = this.enemyPopBallonMapIds != null && this.enemyPopBallonMapIds.Contains(fieldMapData.mapID);
      RegionMap.SpotEventData _event = new RegionMap.SpotEventData();
      _event.mapId = fieldMapData.mapID;
      _event.childRegionId = fieldMapData.childRegionId;
      SpotManager.Spot key = this.spots.AddSpot((int) _event.mapId, fieldMapData.mapName, ((Component) location).transform.position, iconStatus, event_name, canUnlockNewPortal: attach_new_release_portal, viewEnemyPopBallon: viewEnemyPopBallon, _event: (object) _event, dungeon_icon: location.icon, isExistDelivery: isExistDelivery, happenQuestCondition: happen);
      if (SpotManager.ICON_TYPE.INVISIBLE == iconStatus)
        ((Component) key._transform).gameObject.SetActive(false);
      if (iconStatus == SpotManager.ICON_TYPE.NOT_OPENED || iconStatus == SpotManager.ICON_TYPE.INVISIBLE)
        return;
      this.spotMapIdDic[key] = fieldMapData.mapID;
    }
  }

  private void SetFirstCameraPos()
  {
    FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
    ((Component) this.playerMarker).gameObject.SetActive(true);
    if (fieldMapData == null)
    {
      this.parent.worldMapCamera.targetPos = ((Component) this.regionMapRoot.locations[0]).transform.position;
      if (this.regionId == 0)
      {
        this.playerMarker.SetParent(((Component) this.regionMapRoot.locations[0]).transform);
      }
      else
      {
        ((Component) this.playerMarker).gameObject.SetActive(false);
        this.playerMarker.SetParent(((Component) this).transform);
      }
    }
    else if (!FieldManager.HasWorldMap(MonoBehaviourSingleton<FieldManager>.I.currentMapID) || (int) fieldMapData.regionId != this.regionId)
    {
      this.parent.worldMapCamera.targetPos = ((Component) this.regionMapRoot.locations[0]).transform.position;
      ((Component) this.playerMarker).gameObject.SetActive(false);
      this.playerMarker.SetParent(((Component) this).transform);
    }
    else
    {
      RegionMapLocation location = this.regionMapRoot.FindLocation((int) fieldMapData.mapID);
      if (Object.op_Inequality((Object) location, (Object) null))
      {
        this.parent.worldMapCamera.targetPos = ((Component) location).transform.position;
        this.playerMarker.SetParent(((Component) location).transform);
      }
    }
    this.playerMarker.localPosition = MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerOffset;
  }

  private void PlayTween(RegionMap.TWEEN_ANIMATION type, EventDelegate.Callback onComplete = null)
  {
    UITweenCtrl uiTweenCtrl = Array.Find<UITweenCtrl>(this.tweenAnimations, (Predicate<UITweenCtrl>) (t => (RegionMap.TWEEN_ANIMATION) t.id == type));
    if (!Object.op_Inequality((Object) uiTweenCtrl, (Object) null))
      return;
    uiTweenCtrl.Reset();
    uiTweenCtrl.Play(onFinished: onComplete);
  }

  protected override void OnOpen()
  {
    if (MonoBehaviourSingleton<WorldMapManager>.I.NeedDirectionOpenRegion(this.regionId))
    {
      this.isOpened = true;
      base.OnOpen();
    }
    else
    {
      if (Object.op_Inequality((Object) this.parent, (Object) null) && !this.isOpened)
      {
        if (!this.directOpen)
        {
          this.parent.EnterRegionMapEvent((System.Action) (() => this.CreateVisitedLocationSpotIfNeed()));
        }
        else
        {
          this.parent.DisableWorldMapObject();
          this.CreateVisitedLocationSpotIfNeed();
          if (this.displayQuestTargetMode)
          {
            this.parent.worldMapCamera.isInteractive = false;
            if (this.isTutorial)
              this.DisplayQuestTargetTutorial();
            else
              this.DisplayQuestTarget();
          }
        }
      }
      this.SetButtonsAlpha();
      this.PlayTween(RegionMap.TWEEN_ANIMATION.OPENING);
      this.isOpened = true;
      base.OnOpen();
    }
  }

  public override void StartSection()
  {
    if (!MonoBehaviourSingleton<WorldMapManager>.I.NeedDirectionOpenRegion(this.regionId))
      return;
    MonoBehaviourSingleton<WorldMapManager>.I.transferInfo = new WorldMapManager.TransferInfo(this.regionId, false);
    this.DispatchEvent("WORLDMAP");
  }

  private void UpdateDeliveryTargetMarkers()
  {
    if (this.spotMapIdDic == null)
      return;
    this.InitDeliveryTargetIdLists();
    List<SpotManager.Spot> allSpots = this.spots.GetAllSpots();
    if (allSpots == null)
      return;
    int index = 0;
    for (int count = allSpots.Count; index < count; ++index)
    {
      SpotManager.Spot key = allSpots[index];
      uint mapId = 0;
      if (this.spotMapIdDic.TryGetValue(key, out mapId))
        key.UpdateDeliveryTargetMarker(this.IsExistDeliveryTarget(mapId));
    }
  }

  private void SetButtonsAlpha()
  {
    RegionTable.Data data = Singleton<RegionTable>.I.GetData((uint) this.regionId);
    if (this.isEventMap)
    {
      this.HideButtons();
    }
    else
    {
      if (data == null)
        return;
      Transform transform1 = ((Component) this).transform.Find("LocationSpotRoot/BACK_REGION_BTN");
      if (Object.op_Inequality((Object) transform1, (Object) null))
      {
        UIWidget component = ((Component) transform1).GetComponent<UIWidget>();
        if (Object.op_Inequality((Object) component, (Object) null))
          component.alpha = data.hasParentRegion() ? 1f : 0.0f;
      }
      Transform transform2 = ((Component) this).transform.Find("LocationSpotRoot/BACK_BTN");
      if (!Object.op_Inequality((Object) transform2, (Object) null))
        return;
      UIWidget component1 = ((Component) transform2).GetComponent<UIWidget>();
      if (!Object.op_Inequality((Object) component1, (Object) null))
        return;
      component1.alpha = data.hasParentRegion() ? 0.0f : 1f;
    }
  }

  private void HideButtons()
  {
    Transform[] transformArray = new Transform[2]
    {
      this._transform.Find("LocationSpotRoot/BACK_REGION_BTN"),
      this._transform.Find("LocationSpotRoot/BACK_BTN")
    };
    foreach (Transform transform in transformArray)
    {
      if (!Object.op_Equality((Object) null, (Object) transform))
      {
        UIWidget component = ((Component) transform).GetComponent<UIWidget>();
        if (Object.op_Inequality((Object) null, (Object) component))
          component.alpha = 0.0f;
      }
    }
  }

  protected override void OnCloseStart()
  {
    this.StopAllCoroutines();
    this.collectUI = (Transform) null;
    if (!this.isToDescription && Object.op_Inequality((Object) this.parent, (Object) null))
      this.parent.worldMapCamera.isInteractive = true;
    MonoBehaviourSingleton<WorldMapManager>.I.ignoreTutorial = false;
    this.isToDescription = false;
    base.OnCloseStart();
  }

  public override void Exit()
  {
    if (Object.op_Inequality((Object) this.windEffect, (Object) null))
      EffectManager.ReleaseEffect(((Component) this.windEffect).gameObject);
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= new ScreenOrientationManager.OnScreenRotateDelegate(this.InitMapSprite);
    if (this.spots != null)
      this.spots.ClearAllSpot();
    this.FadeOutMap();
    MonoBehaviourSingleton<FilterManager>.I.StopBlur();
    if (Object.op_Inequality((Object) this.parent, (Object) null))
      this.parent.worldMapCamera.isInteractive = true;
    MonoBehaviourSingleton<WorldMapManager>.I.ignoreTutorial = false;
    base.Exit();
  }

  private void OnQuery_SELECT()
  {
    if (this.isShowDescription)
    {
      this.HideInTheBack();
      this.isToDescription = true;
      GameSection.ChangeEvent("TO_DESCRIPTION");
    }
    else
      this.MoveIngameMapOrHome();
  }

  private void HideInTheBack()
  {
    if (Object.op_Inequality((Object) this.rootPanel, (Object) null))
      this.rootPanel.depth = 4000;
    this.HideButtons();
    if (Object.op_Inequality((Object) this.closeButton, (Object) null))
      this.closeButton.alpha = 0.0f;
    if (Object.op_Inequality((Object) this.announceTap, (Object) null))
      this.announceTap.SetActive(false);
    this.parent.worldMapCamera.isInteractive = false;
  }

  private void OnCloseDialog()
  {
    this.PutOutInFront();
    this.collectUI = this._transform;
    this.parent.worldMapCamera.isInteractive = true;
    this.UpdateDeliveryTargetMarkers();
  }

  private void PutOutInFront()
  {
    if (Object.op_Inequality((Object) this.rootPanel, (Object) null))
      this.rootPanel.depth = this.defaultDepth;
    this.SetButtonsAlpha();
    if (Object.op_Inequality((Object) this.closeButton, (Object) null))
      this.closeButton.alpha = 1f;
    if (Object.op_Inequality((Object) this.announceTap, (Object) null))
      this.announceTap.SetActive(true);
    ((Component) this.parent.worldMapCamera).gameObject.SetActive(true);
  }

  private void OnQuery_TO_FIELD_OR_HOME()
  {
    this.HideInTheBack();
    this.MoveIngameMapOrHome();
  }

  private void MoveIngameMapOrHome()
  {
    if (MonoBehaviourSingleton<FilterManager>.I.IsEnabledBlur())
    {
      GameSection.StopEvent();
    }
    else
    {
      uint mapId = ((RegionMap.SpotEventData) GameSection.GetEventData()).mapId;
      if ((int) mapId == (int) MonoBehaviourSingleton<FieldManager>.I.currentMapID)
      {
        GameSection.StopEvent();
        if (UIInGameFieldMenu.IsValid())
          UIInGameFieldMenu.I.OnClickPopMenu();
        Transform transform = Utility.Find(this._transform, "CLOSE_BACK");
        if (!Object.op_Inequality((Object) null, (Object) transform))
          return;
        UIButton component = ((Component) transform).GetComponent<UIButton>();
        if (Object.op_Inequality((Object) null, (Object) component))
          component.onClick.ForEach((Action<EventDelegate>) (o => o.Execute()));
        this.DispatchEvent("CLOSE");
      }
      else if (mapId == 0U)
      {
        if (!MonoBehaviourSingleton<InGameProgress>.IsValid())
          return;
        MonoBehaviourSingleton<InGameProgress>.I.FieldToHome();
      }
      else
      {
        FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(mapId);
        if (fieldMapData == null || fieldMapData.jumpPortalID == 0U)
          Log.Error("RegionMap.OnQuery_SELECT() jumpPortalID is not found.");
        else if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckPortalAndOpenUpdateAppDialog(fieldMapData.jumpPortalID, false))
        {
          GameSection.StopEvent();
        }
        else
        {
          if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "InGameScene")
          {
            this.spots.ClearAllSpot();
            ((Component) this.spots.spotRootTransform).gameObject.SetActive(false);
            ((Component) MonoBehaviourSingleton<UIManager>.I.system.GetCtrl((Enum) UIManager.SYSTEM.DIALOG_BLOCKER)).gameObject.SetActive(false);
            GameSection.StayEvent();
            CoopApp.EnterField(fieldMapData.jumpPortalID, 0U, (Action<bool, bool, bool>) ((is_matching, is_connect, is_regist) =>
            {
              if (!is_connect)
              {
                GameSection.ChangeStayEvent("COOP_SERVER_INVALID");
                GameSection.ResumeEvent(true);
                MonoBehaviourSingleton<AppMain>.I.onDelayCall += (System.Action) (() => this.DispatchEvent("CLOSE"));
              }
              else
              {
                GameSection.ResumeEvent(is_regist);
                if (!is_regist)
                  return;
                MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("InGame");
              }
            }));
          }
          else if (MonoBehaviourSingleton<InGameProgress>.IsValid() && (int) MonoBehaviourSingleton<FieldManager>.I.currentMapID != (int) mapId)
          {
            MonoBehaviourSingleton<InGameProgress>.I.PortalNext(fieldMapData.jumpPortalID);
            OutGameSettingsManager.QuestMap questMap = MonoBehaviourSingleton<OutGameSettingsManager>.I.questMap;
            MonoBehaviourSingleton<FilterManager>.I.StartBlur(questMap.cameraBlurTime, questMap.cameraBlurStrength, questMap.cameraMoveTime);
            this.spots.ClearAllSpot();
            MonoBehaviourSingleton<FieldManager>.I.useFastTravel = true;
          }
          this.IsCalledExit = true;
        }
      }
    }
  }

  private void OnQuery_SELECT_CHILD()
  {
    RegionMap.SpotEventData eventData = GameSection.GetEventData() as RegionMap.SpotEventData;
    GameSection.StayEvent();
    if (eventData == null)
    {
      GameSection.ResumeEvent(false);
    }
    else
    {
      this.regionId = (int) eventData.childRegionId;
      this.StartCoroutine(this.DoChangeRegion(true));
    }
  }

  private void OnQuery_BACK_REGION()
  {
    RegionTable.Data data = Singleton<RegionTable>.I.GetData((uint) this.regionId);
    if (data == null)
      return;
    if (Object.op_Inequality((Object) this.redCircle, (Object) null))
      ((Component) this.redCircle).gameObject.SetActive(false);
    this.displayQuestTargetMode = false;
    this.regionId = (int) data.parentRegionId;
    this.StartCoroutine(this.DoChangeRegion(false));
  }

  private IEnumerator DoChangeRegion(bool withBlur)
  {
    bool wait = false;
    if (withBlur)
    {
      wait = true;
      this.parent.blurFilter.CacheRenderTarget((System.Action) (() =>
      {
        wait = false;
        this.uiParentMapSprite.alpha = 1f;
        this.spots.ClearAllSpot();
      }), true);
    }
    else
      this.spots.ClearAllSpot();
    LoadingQueue loadingQueue = new LoadingQueue((MonoBehaviour) this);
    LoadObject loadObj = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionMap_" + this.regionId.ToString("D3"));
    if (loadingQueue.IsLoading())
      yield return (object) loadingQueue.Wait();
    while (wait)
      yield return (object) null;
    Object.Destroy((Object) ((Component) this.regionMapRoot).gameObject);
    this.regionMapRoot = ((Component) ResourceUtility.Realizes(loadObj.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform)).gameObject.GetComponent<RegionMapRoot>();
    if (Object.op_Inequality((Object) this.regionMapRoot, (Object) null))
    {
      wait = true;
      this.regionMapRoot.InitPortalStatus((System.Action) (() => wait = false));
      while (wait)
        yield return (object) null;
    }
    if (withBlur)
    {
      wait = true;
      float duration = 0.25f;
      Vector2 blurCenter;
      // ISSUE: explicit constructor call
      ((Vector2) ref blurCenter).\u002Ector(0.5f, 0.5f);
      this.parent.blurFilter.StartBlurFilter(0.01f, 0.25f, duration, blurCenter, (System.Action) (() => wait = false));
      this.uiMapSprite.alpha = 0.0f;
      TweenAlpha.Begin(((Component) this.uiMapSprite).gameObject, duration, 1f);
      TweenAlpha.Begin(((Component) this.uiParentMapSprite).gameObject, duration, 0.0f);
    }
    this.SetFirstCameraPos();
    this.isOpened = false;
    this.OnOpen();
    GameSection.ResumeEvent(true);
    yield return (object) null;
  }

  private void OnQuery_HOME()
  {
    if (MonoBehaviourSingleton<FilterManager>.I.IsEnabledBlur())
      GameSection.StopEvent();
    else if ((long) (int) GameSection.GetEventData() == (long) MonoBehaviourSingleton<FieldManager>.I.currentMapID)
    {
      GameSection.StopEvent();
    }
    else
    {
      OutGameSettingsManager.QuestMap questMap = MonoBehaviourSingleton<OutGameSettingsManager>.I.questMap;
      MonoBehaviourSingleton<FilterManager>.I.StartBlur(questMap.cameraBlurTime, questMap.cameraBlurStrength, questMap.cameraMoveTime);
      this.spots.ClearAllSpot();
      if (MonoBehaviourSingleton<InGameProgress>.IsValid())
        MonoBehaviourSingleton<InGameProgress>.I.FieldToHome();
      this.IsCalledExit = true;
    }
  }

  private void OnQuery_SECTION_BACK()
  {
    if (this.IsCalledExit)
      return;
    GameSection.StayEvent();
    this.DoExitUIEvent((System.Action) (() => GameSection.ResumeEvent(true)));
    this.parent.worldMapCamera.isInteractive = true;
    MonoBehaviourSingleton<WorldMapManager>.I.ignoreTutorial = false;
  }

  private void DoExitUIEvent(System.Action onComplete)
  {
    this.PlayTween(RegionMap.TWEEN_ANIMATION.ENDING, (EventDelegate.Callback) (() =>
    {
      if (onComplete == null)
        return;
      onComplete();
    }));
    this.spots.ClearAllSpot();
    this.FadeOutMap();
  }

  private void OnQuery_VIEW_POP_ENEMY_MAP()
  {
    this.enemyPopBallonMapIds = GameSection.GetEventData() as List<uint>;
  }

  private void LateUpdate()
  {
    if (this.spots != null)
      this.spots.Update();
    if (Object.op_Inequality((Object) null, (Object) this.redCircle) && ((Component) this.redCircle).gameObject.activeSelf)
    {
      Camera camera = this.parent.worldMapCamera._camera;
      if (Object.op_Inequality((Object) null, (Object) camera))
      {
        Vector3 worldPoint = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(camera.WorldToScreenPoint(this.redCircleOrgPos));
        worldPoint.z = 0.0f;
        this.redCircle.position = worldPoint;
      }
    }
    if (!this.isUpdateRenderTexture)
      return;
    this.InitMapSprite(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
    this.isUpdateRenderTexture = false;
  }

  public void FadeOutMap()
  {
    this.StartCoroutine(this.DoFadeMap(1f, 0.0f, 0.3f, (System.Action) (() =>
    {
      if (!Object.op_Inequality((Object) this.regionMapRoot, (Object) null))
        return;
      Object.Destroy((Object) ((Component) this.regionMapRoot).gameObject);
      this.regionMapRoot = (RegionMapRoot) null;
    })));
  }

  public void FadeInMap()
  {
    this.StartCoroutine(this.DoFadeMap(0.0f, 1f, 0.5f, (System.Action) (() => this.CreateVisitedLocationSpotIfNeed())));
  }

  private IEnumerator DoFadeMap(float from, float to, float time, System.Action onComplete)
  {
    if (!Object.op_Equality((Object) this.regionMapRoot, (Object) null))
    {
      Renderer r = ((Component) this.regionMapRoot).GetComponentInChildren<Renderer>();
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

  protected override void OnDestroy()
  {
    if (Object.op_Inequality((Object) this.regionMapRoot, (Object) null))
    {
      Object.Destroy((Object) ((Component) this.regionMapRoot).gameObject);
      this.regionMapRoot = (RegionMapRoot) null;
    }
    base.OnDestroy();
  }

  private void DisplayQuestTarget()
  {
    FieldMapTable.PortalTableData portalData = this.GetPortalData();
    FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint) this.questTargetMapID);
    bool isStepOver = this.IsStepOver(portalData);
    int num = this.IsSameRegionPlayer() ? 1 : 0;
    Vector3 targetPosition = this.GetTargetPosition(fieldMapData, portalData, isStepOver);
    Vector3 from = targetPosition;
    if (num != 0)
    {
      RegionMapLocation playerLocation = this.GetPlayerLocation();
      if (Object.op_Inequality((Object) null, (Object) playerLocation))
        from = ((Component) playerLocation).transform.position;
    }
    this.SetRedCirclePosition(fieldMapData, portalData, isStepOver);
    this.parent.worldMapCamera.targetPos = from;
    this.StartCoroutine(this.DoDisplayQuestTarget(from, targetPosition));
  }

  private IEnumerator DoDisplayQuestTarget(Vector3 from, Vector3 to)
  {
    yield return (object) new WaitForSeconds(0.8f);
    Vector3Interpolator ip = new Vector3Interpolator();
    if (0.10000000149011612 < (double) Vector3.Distance(from, to))
      ip.Set(1f, from, to, (AnimationCurve) null, new Vector3(), (AnimationCurve) null);
    else
      ip.Set(0.0f, from, to, (AnimationCurve) null, new Vector3(), (AnimationCurve) null);
    ip.Play();
    while (ip.IsPlaying())
    {
      ip.Update();
      this.parent.worldMapCamera.targetPos = ip.Get();
      yield return (object) null;
    }
    if (Object.op_Inequality((Object) null, (Object) this.redCircle))
    {
      yield return (object) new WaitForSeconds(0.5f);
      ((Component) this.redCircle).gameObject.SetActive(true);
      TweenAlpha tweenAlpha = ((Component) this.redCircle).GetComponent<TweenAlpha>();
      if (Object.op_Inequality((Object) null, (Object) tweenAlpha))
      {
        while (((Behaviour) tweenAlpha).isActiveAndEnabled)
          yield return (object) null;
      }
      tweenAlpha = (TweenAlpha) null;
    }
    this.parent.worldMapCamera.isInteractive = true;
    MonoBehaviourSingleton<WorldMapManager>.I.ignoreTutorial = false;
  }

  private void DisplayQuestTargetTutorial()
  {
    FieldMapTable.PortalTableData portalData = this.GetPortalData();
    FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint) this.questTargetMapID);
    bool isStepOver = this.IsStepOver(portalData);
    Vector3 targetPosition = this.GetTargetPosition(fieldMapData, portalData, isStepOver);
    Vector3 from = targetPosition;
    RegionMapLocation playerLocation = this.GetPlayerLocation();
    if (Object.op_Inequality((Object) null, (Object) playerLocation))
      from = ((Component) playerLocation).transform.position;
    Vector3 neighbor = Vector3.zero;
    int neighborMapId = this.GetNeighborMapID();
    RegionMapLocation location = this.regionMapRoot.FindLocation(neighborMapId);
    if (Object.op_Inequality((Object) null, (Object) location))
      neighbor = ((Component) location).transform.position;
    this.SetRedCirclePosition(fieldMapData, portalData, isStepOver);
    Transform icon = (Transform) null;
    Transform button = (Transform) null;
    SpotManager.Spot spot = this.spots.FindSpot(neighborMapId);
    if (Object.op_Inequality((Object) null, (Object) this.portalGuideTxt) && spot != null)
    {
      this.portalGuideTxt.SetParent(spot._transform);
      this.portalGuideTxt.localPosition = new Vector3(0.0f, -60f, 0.0f);
      icon = spot.type != SpotManager.ICON_TYPE.NEW ? spot._transform.Find("SPR_ICON_CLEARED") : spot._transform.Find("SPR_ICON_NEW");
      button = spot._transform.Find("SPR_BUTTON");
    }
    this.parent.worldMapCamera.targetPos = from;
    this.StartCoroutine(this.DoDisplayQuestTargetTutorial(from, targetPosition, neighbor, icon, button));
  }

  private IEnumerator DoDisplayQuestTargetTutorial(
    Vector3 from,
    Vector3 to,
    Vector3 neighbor,
    Transform icon,
    Transform button)
  {
    Vector3Interpolator ip = new Vector3Interpolator();
    if (0.10000000149011612 < (double) Vector3.Distance(from, to))
      ip.Set(1f, from, to, (AnimationCurve) null, new Vector3(), (AnimationCurve) null);
    else
      ip.Set(0.0f, from, to, (AnimationCurve) null, new Vector3(), (AnimationCurve) null);
    yield return (object) new WaitForSeconds(0.8f);
    ip.Play();
    while (ip.IsPlaying())
    {
      ip.Update();
      this.parent.worldMapCamera.targetPos = ip.Get();
      yield return (object) null;
    }
    TweenAlpha tweenAlpha;
    if (Object.op_Inequality((Object) null, (Object) this.redCircle))
    {
      yield return (object) new WaitForSeconds(0.5f);
      ((Component) this.redCircle).gameObject.SetActive(true);
      tweenAlpha = ((Component) this.redCircle).GetComponent<TweenAlpha>();
      if (Object.op_Inequality((Object) null, (Object) tweenAlpha))
      {
        while (((Behaviour) tweenAlpha).isActiveAndEnabled)
          yield return (object) null;
      }
      tweenAlpha = (TweenAlpha) null;
    }
    ip.Set(0.8f, to, neighbor, (AnimationCurve) null, new Vector3(), (AnimationCurve) null);
    yield return (object) new WaitForSeconds(0.5f);
    ip.Play();
    while (ip.IsPlaying())
    {
      ip.Update();
      this.parent.worldMapCamera.targetPos = ip.Get();
      yield return (object) null;
    }
    if (Object.op_Inequality((Object) null, (Object) this.portalGuideTxt))
    {
      yield return (object) new WaitForSeconds(0.5f);
      ((Component) this.portalGuideTxt).gameObject.SetActive(true);
      tweenAlpha = ((Component) this.portalGuideTxt).GetComponent<TweenAlpha>();
      if (Object.op_Inequality((Object) null, (Object) tweenAlpha))
      {
        while (((Behaviour) tweenAlpha).isActiveAndEnabled)
          yield return (object) null;
      }
      tweenAlpha = (TweenAlpha) null;
    }
    if (Object.op_Inequality((Object) null, (Object) icon))
    {
      yield return (object) new WaitForSeconds(0.5f);
      UITweenCtrl component = ((Component) icon).GetComponent<UITweenCtrl>();
      if (Object.op_Inequality((Object) null, (Object) component))
        component.Play();
    }
    Transform transform = TutorialMessage.AttachCursor(button);
    if (Object.op_Inequality((Object) null, (Object) transform))
    {
      Vector3 localPosition = transform.localPosition;
      transform.localPosition = Vector3.op_Addition(localPosition, new Vector3(0.0f, -10f, 0.0f));
    }
    this.parent.worldMapCamera.isInteractive = true;
    MonoBehaviourSingleton<WorldMapManager>.I.ignoreTutorial = false;
  }

  private FieldMapTable.PortalTableData[] GetPortalArray(int mapID)
  {
    List<FieldMapTable.PortalTableData> portalList = new List<FieldMapTable.PortalTableData>();
    FieldMapTable.FieldMapTableData[] fieldMapDataInRegion = Singleton<FieldMapTable>.I.GetFieldMapDataInRegion((uint) this.regionId);
    for (int index = 0; index < fieldMapDataInRegion.Length; ++index)
    {
      if (fieldMapDataInRegion[index].jumpPortalID != 0U)
        Singleton<FieldMapTable>.I.GetPortalListByMapID(fieldMapDataInRegion[index].mapID)?.ForEach((Action<FieldMapTable.PortalTableData>) (o =>
        {
          if ((long) o.dstMapID != (long) mapID)
            return;
          portalList.Add(o);
        }));
    }
    return portalList.ToArray();
  }

  private bool IsSameRegionPlayer()
  {
    uint currentMapId = MonoBehaviourSingleton<FieldManager>.I.currentMapID;
    if (currentMapId == 0U)
      return this.regionId == 0;
    FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(currentMapId);
    return fieldMapData != null && (long) fieldMapData.regionId == (long) this.regionId;
  }

  private FieldMapTable.PortalTableData GetPortalData()
  {
    if (this.questTargetPortalIDs == null)
      return (FieldMapTable.PortalTableData) null;
    int length = this.questTargetPortalIDs.Length;
    if (0 >= length)
      return (FieldMapTable.PortalTableData) null;
    FieldMapTable.PortalTableData portalData1 = (FieldMapTable.PortalTableData) null;
    for (int index = 0; index < length; ++index)
    {
      FieldMapTable.PortalTableData portalData2 = Singleton<FieldMapTable>.I.GetPortalData((uint) this.questTargetPortalIDs[index]);
      if (portalData2 != null && (MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledMap((int) portalData2.srcMapID) || MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledMap((int) portalData2.dstMapID)))
      {
        portalData1 = portalData2;
        break;
      }
    }
    if (portalData1 != null)
      return portalData1;
    for (int index = 0; index < length; ++index)
    {
      portalData1 = Singleton<FieldMapTable>.I.GetPortalData((uint) this.questTargetPortalIDs[index]);
      if (portalData1 != null)
        break;
    }
    return portalData1;
  }

  private bool IsStepOver(FieldMapTable.PortalTableData portal)
  {
    if (portal == null)
      return false;
    FieldMapTable.FieldMapTableData fieldMapData1 = Singleton<FieldMapTable>.I.GetFieldMapData(portal.srcMapID);
    FieldMapTable.FieldMapTableData fieldMapData2 = Singleton<FieldMapTable>.I.GetFieldMapData(portal.dstMapID);
    return fieldMapData1 != null && fieldMapData2 != null && (int) fieldMapData1.regionId != (int) fieldMapData2.regionId;
  }

  private Vector3 GetTargetPosition(
    FieldMapTable.FieldMapTableData map,
    FieldMapTable.PortalTableData portal,
    bool isStepOver)
  {
    if (map == null && portal == null)
      return Vector3.zero;
    Vector3 targetPosition = Vector3.zero;
    if (portal == null)
    {
      RegionMapLocation location = this.regionMapRoot.FindLocation((int) map.mapID);
      if (Object.op_Inequality((Object) null, (Object) location))
        targetPosition = ((Component) location).transform.position;
    }
    else if ((double) portal.mapX < 10000000000.0)
    {
      RegionMapLocation location = this.regionMapRoot.FindLocation((int) portal.srcMapID);
      if (Object.op_Inequality((Object) null, (Object) location))
      {
        Vector3 vector3;
        // ISSUE: explicit constructor call
        ((Vector3) ref vector3).\u002Ector(portal.mapX, portal.mapY, 0.0f);
        targetPosition = Vector3.op_Addition(((Component) location).transform.position, vector3);
      }
    }
    else if (!isStepOver)
    {
      RegionMapLocation location1 = this.regionMapRoot.FindLocation((int) portal.srcMapID);
      RegionMapLocation location2 = this.regionMapRoot.FindLocation((int) portal.dstMapID);
      if (Object.op_Inequality((Object) null, (Object) location1) && Object.op_Inequality((Object) null, (Object) location2))
        targetPosition = Vector3.Lerp(((Component) location1).transform.position, ((Component) location2).transform.position, 0.5f);
    }
    else
    {
      RegionMapLocation location = this.regionMapRoot.FindLocation((int) portal.srcMapID);
      if (Object.op_Inequality((Object) null, (Object) location))
      {
        Vector3 vector3_1;
        // ISSUE: explicit constructor call
        ((Vector3) ref vector3_1).\u002Ector(0.0f, -1f, 0.0f);
        Vector3 vector3_2 = Quaternion.op_Multiply(Quaternion.Euler(0.0f, 0.0f, -portal.dstDir), vector3_1);
        targetPosition = Vector3.op_Addition(((Component) location).transform.position, Vector3.op_Multiply(vector3_2, 2f));
      }
    }
    return targetPosition;
  }

  private RegionMapLocation GetPlayerLocation()
  {
    return this.regionMapRoot.FindLocation((int) MonoBehaviourSingleton<FieldManager>.I.currentMapID);
  }

  private int GetNeighborMapID()
  {
    int neighborMapId = 0;
    if (this.questTargetPortalIDs == null || this.questTargetPortalIDs.Length == 0)
    {
      FieldMapTable.PortalTableData[] portalArray = this.GetPortalArray(this.questTargetMapID);
      if (portalArray == null || portalArray.Length == 0)
        return 0;
      for (int index = 0; index < portalArray.Length; ++index)
      {
        if (MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledMap((int) portalArray[index].srcMapID))
        {
          neighborMapId = (int) portalArray[index].srcMapID;
          break;
        }
      }
    }
    else
    {
      for (int index = 0; index < this.questTargetPortalIDs.Length; ++index)
      {
        FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData((uint) this.questTargetPortalIDs[index]);
        if (portalData != null)
        {
          if (MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledMap((int) portalData.srcMapID))
          {
            neighborMapId = (int) portalData.srcMapID;
            break;
          }
          if (MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledMap((int) portalData.dstMapID))
          {
            neighborMapId = (int) portalData.dstMapID;
            break;
          }
        }
      }
    }
    return neighborMapId;
  }

  private void SetRedCirclePosition(
    FieldMapTable.FieldMapTableData map,
    FieldMapTable.PortalTableData portal,
    bool isStepOver)
  {
    if (map == null && portal == null)
      return;
    if (portal == null)
    {
      SpotManager.Spot spot = this.spots.FindSpot((int) map.mapID);
      if (spot == null)
        return;
      this.redCircleOrgPos = spot.originalPos;
      Transform transform = spot._transform.Find("LBL_NAME");
      if (!Object.op_Inequality((Object) null, (Object) transform))
        return;
      ((Component) ((Component) transform).transform).gameObject.SetActive(true);
    }
    else if ((double) portal.mapX < 10000000000.0)
    {
      SpotManager.Spot spot = this.spots.FindSpot((int) portal.srcMapID);
      if (spot == null)
        return;
      Vector3 vector3;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3).\u002Ector(portal.mapX, portal.mapY, 0.0f);
      this.redCircleOrgPos = Vector3.op_Addition(spot.originalPos, vector3);
    }
    else if (!isStepOver)
    {
      SpotManager.Spot spot1 = this.spots.FindSpot((int) portal.srcMapID);
      SpotManager.Spot spot2 = this.spots.FindSpot((int) portal.dstMapID);
      if (spot1 == null || spot2 == null)
        return;
      this.redCircleOrgPos = Vector3.Lerp(spot1.originalPos, spot2.originalPos, 0.5f);
    }
    else
    {
      SpotManager.Spot spot = this.spots.FindSpot((int) portal.srcMapID);
      if (spot == null)
        return;
      Vector3 vector3_1;
      // ISSUE: explicit constructor call
      ((Vector3) ref vector3_1).\u002Ector(0.0f, -1f, 0.0f);
      Vector3 vector3_2 = Quaternion.op_Multiply(Quaternion.Euler(0.0f, 0.0f, -portal.dstDir), vector3_1);
      this.redCircleOrgPos = Vector3.op_Addition(spot.originalPos, Vector3.op_Multiply(vector3_2, 2f));
    }
  }

  private int[] GetLocationNumbers(string portalName)
  {
    string[] strArray = portalName.Replace("portal", "").Split('_');
    return new int[2]
    {
      int.Parse(strArray[0]),
      int.Parse(strArray[1])
    };
  }

  private void OnApplicationPause(bool paused) => this.isUpdateRenderTexture = !paused;

  private void InitDeliveryTargetIdLists()
  {
    Delivery[] deliveryList = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList(false);
    int index1 = 0;
    for (int length = deliveryList.Length; index1 < length; ++index1)
    {
      int dId = deliveryList[index1].dId;
      if (!MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery(dId))
      {
        DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint) dId);
        List<uint> mapIdList = deliveryTableData.GetMapIdList();
        if (mapIdList != null)
        {
          int index2 = 0;
          for (int count = mapIdList.Count; index2 < count; ++index2)
            this.DeliveryTargetMapIds.Add(mapIdList[index2]);
        }
        else
        {
          List<uint> enemyIdList = deliveryTableData.GetEnemyIdList();
          if (enemyIdList != null)
          {
            int index3 = 0;
            for (int count = enemyIdList.Count; index3 < count; ++index3)
              this.DeliveryTargetEnemyIds.Add(enemyIdList[index3]);
          }
        }
      }
    }
  }

  private void OnQuery_SELECT_DIFFICULTY()
  {
    GameSection.SetEventData((object) new object[2]
    {
      (object) true,
      (object) this.isOpenedHard
    });
    if (!this.isInGame)
      return;
    GameSection.ChangeEvent("INGAME_SELECT_DIFFICULTY");
  }

  private void OnCloseDialog_WorldMapSelectDifficultyDialog()
  {
    object eventData = GameSection.GetEventData();
    if (eventData == null)
      return;
    REGION_DIFFICULTY_TYPE type = (REGION_DIFFICULTY_TYPE) eventData;
    RegionTable.Data data1 = Singleton<RegionTable>.I.GetData((uint) this.regionId);
    if (type == data1.difficulty)
      return;
    RegionTable.Data data2 = Singleton<RegionTable>.I.GetData(data1.groupId, type);
    if (data2 == null)
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
    {
      new EventData("WORLDMAP"),
      new EventData("OPEN_REGION_CHANGE", (object) (int) data2.regionId)
    });
  }

  private void OnCloseDialog_InGameSelectDifficultyDialog()
  {
    object eventData = GameSection.GetEventData();
    if (eventData == null)
      return;
    REGION_DIFFICULTY_TYPE type = (REGION_DIFFICULTY_TYPE) eventData;
    RegionTable.Data data1 = Singleton<RegionTable>.I.GetData((uint) this.regionId);
    if (type == data1.difficulty)
      return;
    RegionTable.Data data2 = Singleton<RegionTable>.I.GetData(data1.groupId, type);
    if (data2 == null)
      return;
    MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(new EventData[2]
    {
      new EventData("WORLDMAP"),
      new EventData("OPEN_REGION_CHANGE", (object) (int) data2.regionId)
    });
  }

  protected enum UI
  {
    OBJ_SELECT_DIFFICULTY,
    BTN_CURRENT_DIFFICULTY_NORMAL,
    BTN_CURRENT_DIFFICULTY_HARD,
    SOR_BORDER_TITLE,
  }

  private enum TWEEN_ANIMATION
  {
    OPENING,
    ENDING,
  }

  public class SpotEventData
  {
    public uint mapId;
    public uint childRegionId;
  }
}
