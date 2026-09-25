// Decompiled with JetBrains decompiler
// Type: WorldMapOpenNewField
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class WorldMapOpenNewField : GameSection
{
  private WorldMapOpenNewField.SectionEventData eventData;
  private FieldMapTable.PortalTableData portalData;
  private FieldMapTable.FieldMapTableData newMapData;
  private RegionMapRoot regionMapRoot;
  private Transform playerMarker;
  private rymFX windEffect;
  private Object topEffectPrefab;
  private Object remainEffectPrefab;
  private Transform dungeonOpenEffect;
  private GameObject fieldQuestWarningRoot;
  private Camera _camera;
  private UITexture uiFrontMapSprite;
  private UITexture uiMapSprite;
  private uint regionId;
  private ZoomBlurFilter blurFilter;
  private SpotManager spots;
  private bool calledExit;
  private bool isUpdateRenderTexture;
  private bool toRegionRelease;
  private UIEventListener bgEventListener;
  private Transform tutorialTrigger;
  private Vector3 tutorialTriggerPos = new Vector3(3.7f, 0.5f, 0.0f);

  public override IEnumerable<string> requireDataTable
  {
    get
    {
      yield return "FieldMapTable";
      yield return "RegionTable";
    }
  }

  public override void Initialize() => this.StartCoroutine("DoInitialize");

  private IEnumerator DoInitialize()
  {
    this.eventData = (WorldMapOpenNewField.SectionEventData) GameSection.GetEventData();
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
      this.portalData = Singleton<FieldMapTable>.I.GetPortalData(MonoBehaviourSingleton<InGameManager>.I.beforePortalID);
    if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid() && this.portalData == null)
      this.portalData = Singleton<FieldMapTable>.I.GetPortalData((uint) MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.linkFieldPortalID);
    if (this.eventData.IsQuestToField())
      this.portalData = Singleton<FieldMapTable>.I.GetPortalData(MonoBehaviourSingleton<FieldManager>.I.currentPortalID);
    if (this.portalData == null)
    {
      base.Initialize();
    }
    else
    {
      FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(this.portalData.srcMapID);
      this.newMapData = Singleton<FieldMapTable>.I.GetFieldMapData(this.portalData.dstMapID);
      this.regionId = this.newMapData.regionId;
      if (this.NeedDirectionOpenRegion())
      {
        this.toRegionRelease = true;
        base.Initialize();
      }
      else
      {
        if (fieldMapData != null && this.newMapData != null && (int) this.newMapData.regionId != (int) fieldMapData.regionId)
          this.regionId = fieldMapData.regionId;
        if (this.newMapData == null || !WorldMapOpenNewField.IsValidRegion(this.newMapData))
        {
          this.newMapData = (FieldMapTable.FieldMapTableData) null;
          base.Initialize();
        }
        else
        {
          LoadingQueue loadQueue = new LoadingQueue((MonoBehaviour) this);
          LoadObject loadedEventUIRoot = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "NewFieldOpenEventUIRoot");
          LoadObject loadedLocationSpot = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "LocationSpot");
          LoadObject loadedEventCamera = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "NewFieldEventCamera");
          LoadObject loadedFilterCamera = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ZoomBlurFilterCamera");
          LoadObject loadedPlayerMarker = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "PlayerMarker");
          LoadObject loadedRegion = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionMap_" + this.regionId.ToString("D3"));
          LoadObject loadedEffect = loadQueue.LoadEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_map_fire_01");
          LoadObject loadedWindEffect = loadQueue.LoadEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_bg_questmap_01");
          LoadObject loadedDungeonEff = (LoadObject) null;
          if (this.eventData.IsFindNewDungeon() && this.newMapData != null)
            loadedDungeonEff = loadQueue.LoadEffect(RESOURCE_CATEGORY.EFFECT_DUNGEON, "DEF_" + this.newMapData.mapID.ToString("D8"));
          LoadObject loadedEncounterBossCutIn = (LoadObject) null;
          if (this.eventData.IsEncounterBossEvent())
            loadedEncounterBossCutIn = loadQueue.Load(RESOURCE_CATEGORY.UI, "InGameFieldQuestWarning");
          this.CacheAudio(loadQueue);
          if (loadQueue.IsLoading())
            yield return (object) loadQueue.Wait();
          if (loadedEncounterBossCutIn != null)
          {
            this.fieldQuestWarningRoot = ((Component) ResourceUtility.Realizes(loadedEncounterBossCutIn.loadedObject)).gameObject;
            UIPanel componentInChildren = this.fieldQuestWarningRoot.GetComponentInChildren<UIPanel>();
            if (Object.op_Inequality((Object) componentInChildren, (Object) null))
              componentInChildren.depth = 8000;
            if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
              MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Load(loadQueue);
          }
          if (loadQueue.IsLoading())
            yield return (object) loadQueue.Wait();
          this.topEffectPrefab = loadedEffect.loadedObject;
          Transform t = ResourceUtility.Realizes(loadedEventUIRoot.loadedObject, this._transform);
          this.regionMapRoot = ((Component) ResourceUtility.Realizes(loadedRegion.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform)).gameObject.GetComponent<RegionMapRoot>();
          if (Object.op_Inequality((Object) this.regionMapRoot, (Object) null))
          {
            bool wait = true;
            this.regionMapRoot.InitPortalStatus((System.Action) (() => wait = false));
            while (wait)
              yield return (object) null;
          }
          this.blurFilter = (ResourceUtility.Instantiate<Object>(loadedFilterCamera.loadedObject) as GameObject).GetComponent<ZoomBlurFilter>();
          ((Component) this.blurFilter).transform.parent = this._transform;
          this._camera = ((Component) ResourceUtility.Realizes(loadedEventCamera.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform)).GetComponent<Camera>();
          this.uiFrontMapSprite = ((Component) t.Find("FrontMap")).gameObject.GetComponent<UITexture>();
          if (Object.op_Inequality((Object) this.uiFrontMapSprite, (Object) null))
            this.uiFrontMapSprite.alpha = 0.0f;
          this.uiMapSprite = ((Component) t.Find("Map")).gameObject.GetComponent<UITexture>();
          this.InitMapSprite(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
          if (this.eventData.IsEncounterBossEvent())
            ((Component) t.Find("TaptoSkip")).gameObject.SetActive(false);
          this.bgEventListener = UIEventListener.Get(((Component) t.Find("BG")).gameObject);
          this.tutorialTrigger = t.Find("TUTORIAL_TRIGGER");
          if (Object.op_Inequality((Object) this.tutorialTrigger, (Object) null))
          {
            if (!TutorialStep.HasAllTutorialCompleted())
            {
              ((Component) this.tutorialTrigger).gameObject.SetActive(true);
              UITweenCtrl.Play(this.tutorialTrigger, is_input_block: false);
            }
            else
              ((Component) this.tutorialTrigger).gameObject.SetActive(false);
          }
          this.spots = new SpotManager((GameObject) null, loadedLocationSpot.loadedObject as GameObject, this._camera);
          this.spots.spotRootTransform = t;
          this.playerMarker = ResourceUtility.Realizes(loadedPlayerMarker.loadedObject);
          PlayerMarker component = ((Component) this.playerMarker).GetComponent<PlayerMarker>();
          if (Object.op_Inequality((Object) null, (Object) component))
            component.SetCamera(((Component) this._camera).transform);
          this.windEffect = ((Component) ResourceUtility.Realizes(loadedWindEffect.loadedObject, ((Component) this._camera).transform)).gameObject.GetComponent<rymFX>();
          this.windEffect.Cameras = new Camera[1]
          {
            this._camera
          };
          ((Component) this.windEffect).gameObject.layer = LayerMask.NameToLayer("WorldMap");
          if (loadedDungeonEff != null)
          {
            this.dungeonOpenEffect = ResourceUtility.Realizes(loadedDungeonEff.loadedObject, this._transform);
            ((Component) this.dungeonOpenEffect).gameObject.SetActive(false);
          }
          this.CreateVisitedLocationSpot();
          if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
            MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += new ScreenOrientationManager.OnScreenRotateDelegate(this.InitMapSprite);
          base.Initialize();
        }
      }
    }
  }

  public override void StartSection()
  {
    if (!this.toRegionRelease)
      return;
    MonoBehaviourSingleton<WorldMapManager>.I.transferInfo = new WorldMapManager.TransferInfo((int) this.regionId, true);
    this.DispatchEvent("WORLD_MAP");
    this.Exit();
  }

  private bool NeedDirectionOpenRegion()
  {
    MonoBehaviourSingleton<WorldMapManager>.I.openNewFieldId = (int) this.newMapData.mapID;
    return MonoBehaviourSingleton<WorldMapManager>.I.NeedDirectionOpenRegion((int) this.regionId);
  }

  private void CreateVisitedLocationSpot()
  {
    MonoBehaviourSingleton<FilterManager>.I.StopBlur(MonoBehaviourSingleton<OutGameSettingsManager>.I.questMap.cameraMoveTime);
    for (int index1 = 0; index1 < this.regionMapRoot.locations.Length; ++index1)
    {
      RegionMapLocation location = this.regionMapRoot.locations[index1];
      FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint) location.mapId);
      if ((fieldMapData != null || location.mapId == 0) && (fieldMapData == null || FieldManager.IsShowPortal(fieldMapData.jumpPortalID)))
      {
        if (fieldMapData != null && !MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(fieldMapData))
          this.CreateLocationSpot(location, SpotManager.ICON_TYPE.NOT_OPENED);
        else if ((long) location.mapId == (long) this.newMapData.mapID && !this.eventData.IsOnlyCameraMoveEvent() && !this.eventData.IsEnterDungeon() && !this.eventData.IsQuestToField())
        {
          this.CreateLocationSpot(location, SpotManager.ICON_TYPE.NOT_OPENED);
        }
        else
        {
          SpotManager.ICON_TYPE icon = SpotManager.ICON_TYPE.CLEARED;
          if (location.portal.Length != 0)
          {
            for (int index2 = 0; index2 < location.portal.Length; ++index2)
            {
              RegionMapPortal regionMapPortal = location.portal[index2];
              if (!regionMapPortal.IsVisited() && regionMapPortal.IsShow())
              {
                icon = SpotManager.ICON_TYPE.NEW;
                break;
              }
            }
          }
          if (fieldMapData != null)
          {
            if (FieldManager.IsToHardPortal(fieldMapData.jumpPortalID))
            {
              icon = SpotManager.ICON_TYPE.HARD;
              if (icon == SpotManager.ICON_TYPE.NEW)
                icon = SpotManager.ICON_TYPE.HARD_NEW;
            }
            if (fieldMapData.hasChildRegion && (int) fieldMapData.childRegionId != (int) this.regionId)
              icon = SpotManager.ICON_TYPE.CHILD_REGION;
          }
          this.CreateLocationSpot(location, icon);
        }
      }
    }
  }

  private GameObject CreateLocationSpot(
    RegionMapLocation location,
    SpotManager.ICON_TYPE icon = SpotManager.ICON_TYPE.CLEARED,
    bool isNew = false)
  {
    if (location.mapId == 0)
      return ((Component) this.spots.AddSpot(0, MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionTextList().Find((Predicate<GameSceneTables.TextData>) (textData => textData.key == "STR_HOME")).text, ((Component) location).transform.position, SpotManager.ICON_TYPE.HOME, (string) null)._transform).gameObject;
    FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint) location.mapId);
    if (fieldMapData == null)
      return (GameObject) null;
    bool canUnlockNewPortal = false;
    if (location.portal.Length != 0 && icon != SpotManager.ICON_TYPE.NOT_OPENED)
    {
      for (int index = 0; index < location.portal.Length; ++index)
      {
        int result;
        int.TryParse(((Object) location).name.Replace(nameof (location), ""), out result);
        int[] locationNumbers = this.GetLocationNumbers(((Object) location.portal[index]).name);
        if (result == locationNumbers[0] && GameSaveData.instance.isNewReleasePortal((uint) location.portal[index].entranceId))
        {
          if (location.portal[index].IsVisited())
          {
            GameSaveData.instance.newReleasePortals.Remove((uint) location.portal[index].entranceId);
          }
          else
          {
            canUnlockNewPortal = true;
            break;
          }
        }
        if (result == locationNumbers[1] && GameSaveData.instance.isNewReleasePortal((uint) location.portal[index].exitId))
        {
          if (location.portal[index].IsVisited())
          {
            GameSaveData.instance.newReleasePortals.Remove((uint) location.portal[index].exitId);
          }
          else
          {
            canUnlockNewPortal = true;
            break;
          }
        }
      }
    }
    return ((Component) this.spots.AddSpot((int) fieldMapData.mapID, fieldMapData.mapName, ((Component) location).transform.position, icon, (string) null, isNew, canUnlockNewPortal, _event: (object) fieldMapData.mapID, dungeon_icon: location.icon)._transform).gameObject;
  }

  private void InitMapSprite(bool isPortrait)
  {
    if (Object.op_Inequality((Object) this.uiMapSprite, (Object) null))
    {
      if (Object.op_Inequality((Object) this._camera.targetTexture, (Object) null))
      {
        RenderTexture.ReleaseTemporary(this._camera.targetTexture);
        this._camera.targetTexture = (RenderTexture) null;
      }
      this._camera.targetTexture = RenderTexture.GetTemporary(Screen.width, Screen.height);
      this.uiMapSprite.mainTexture = (Texture) this._camera.targetTexture;
      this.uiMapSprite.width = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth;
      this.uiMapSprite.height = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight;
    }
    if (!Object.op_Inequality((Object) this.uiFrontMapSprite, (Object) null) || !Object.op_Inequality((Object) this.blurFilter, (Object) null))
      return;
    this.uiFrontMapSprite.mainTexture = (Texture) this.blurFilter.filteredTexture;
    this.uiFrontMapSprite.width = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth;
    this.uiFrontMapSprite.height = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight;
  }

  protected override void OnOpen()
  {
    if (this.toRegionRelease)
    {
      base.OnOpen();
    }
    else
    {
      if (Object.op_Inequality((Object) null, (Object) this.bgEventListener))
        this.bgEventListener.onClick += new UIEventListener.VoidDelegate(this.onClick);
      if (this.portalData == null || this.newMapData == null)
        this.RequestEvent("EXIT");
      else if (this.eventData.IsFindNewDungeon())
        this.OpenNewDungeon();
      else if (this.eventData.IsOnlyCameraMoveEvent() || this.eventData.IsEnterDungeon())
        this.CameraMoveEvent();
      else if (this.eventData.IsQuestToField())
        this.QuestToField();
      else
        this.OpenNewLocation();
      Transform child = Utility.FindChild(((Component) this).gameObject.transform, "BG");
      if (Object.op_Inequality((Object) child, (Object) null))
      {
        UITexture component = ((Component) child).GetComponent<UITexture>();
        if (Object.op_Inequality((Object) component, (Object) null))
        {
          component.width = 4000;
          component.height = 4000;
          ((Component) component).gameObject.SetActive(true);
        }
      }
      base.OnOpen();
    }
  }

  private void OnQuery_EXIT()
  {
    if (Object.op_Inequality((Object) this.playerMarker, (Object) null))
    {
      Object.Destroy((Object) ((Component) this.playerMarker).gameObject);
      this.playerMarker = (Transform) null;
    }
    if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
      MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= new ScreenOrientationManager.OnScreenRotateDelegate(this.InitMapSprite);
    this.StopAllCoroutines();
    if (this.calledExit)
      return;
    if (Object.op_Inequality((Object) null, (Object) this.bgEventListener))
      this.bgEventListener.onClick -= new UIEventListener.VoidDelegate(this.onClick);
    MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent(nameof (WorldMapOpenNewField), ((Component) this).gameObject, "INGAME_MAIN");
    this.calledExit = true;
  }

  public void CameraMoveEvent()
  {
    RegionMapPortal portalData1;
    bool portalData2 = this.IsPortalReverseAndGetPortalData((int) this.portalData.portalID, out portalData1);
    if (Object.op_Equality((Object) portalData1, (Object) null))
    {
      this.RequestEvent("EXIT");
    }
    else
    {
      Vector3 position = ((Component) portalData1.fromLocation).transform.position;
      this.playerMarker.SetParent(((Component) portalData1.fromLocation).transform);
      if (portalData2)
      {
        position = ((Component) portalData1.toLocation).transform.position;
        this.playerMarker.SetParent(((Component) portalData1.toLocation).transform);
      }
      this.playerMarker.localPosition = MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerOffset;
      ((Component) this._camera).transform.position = Vector3.op_Subtraction(position, Vector3.op_Multiply(((Component) this._camera).transform.forward, MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraDistance));
      this.StartCoroutine(this.DoExitEvent(portalData1, (rymFX) null, MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.onlyCameraMoveDelay, portalData2));
    }
  }

  private void QuestToField()
  {
    FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData(MonoBehaviourSingleton<FieldManager>.I.currentPortalID);
    if (portalData == null)
    {
      this.RequestEvent("EXIT");
    }
    else
    {
      RegionMapLocation location = this.regionMapRoot.FindLocation((int) portalData.dstMapID);
      if (Object.op_Equality((Object) null, (Object) location))
      {
        this.RequestEvent("EXIT");
      }
      else
      {
        ((Component) this._camera).transform.position = Vector3.op_Subtraction(((Component) location).transform.position, Vector3.op_Multiply(((Component) this._camera).transform.forward, MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraDistance));
        this.playerMarker.SetParent(((Component) location).transform);
        this.playerMarker.localPosition = MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerOffset;
        this.playerMarker.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        this.StartCoroutine(this.DoQuestToField());
      }
    }
  }

  private IEnumerator DoQuestToField()
  {
    yield return (object) new WaitForSeconds(0.8f);
    TweenScale.Begin(((Component) this.playerMarker).gameObject, MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerScaleTime, Vector3.one);
    yield return (object) new WaitForSeconds(MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerScaleTime + 1.5f);
    this.OnQuery_EXIT();
  }

  private bool IsPortalReverseAndGetPortalData(int portalId, out RegionMapPortal portalData)
  {
    portalData = this.regionMapRoot.FindEntrancePortal(portalId);
    if (Object.op_Inequality((Object) portalData, (Object) null))
      return false;
    portalData = this.regionMapRoot.FindExitPortal(portalId);
    return true;
  }

  private void SetCameraToMiddlePoint(RegionMapPortal portal)
  {
    ((Component) this._camera).transform.position = Vector3.op_Subtraction(Vector3.op_Division(Vector3.op_Addition(((Component) portal.fromLocation).transform.position, ((Component) portal.toLocation).transform.position), 2f), Vector3.op_Multiply(((Component) this._camera).transform.forward, MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraDistance));
  }

  private void SetCameraToLocation(RegionMapLocation location)
  {
    ((Component) this._camera).transform.position = Vector3.op_Subtraction(((Component) location).transform.position, Vector3.op_Multiply(((Component) this._camera).transform.forward, MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraDistance));
  }

  private void SetPlayerMakerToStartPosition(RegionMapPortal portal, bool reverse)
  {
    this.playerMarker.SetParent(((Component) portal.fromLocation).transform);
    if (reverse)
      this.playerMarker.SetParent(((Component) portal.toLocation).transform);
    this.playerMarker.localPosition = MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerOffset;
  }

  public void OpenNewDungeon() => this.StartCoroutine(this.DoOpenNewDungeon());

  private IEnumerator DoOpenNewDungeon()
  {
    yield return (object) null;
    RegionMapPortal portal;
    bool reverse = this.IsPortalReverseAndGetPortalData((int) this.portalData.portalID, out portal);
    if (Object.op_Equality((Object) portal, (Object) null))
    {
      this.RequestEvent("EXIT");
    }
    else
    {
      this.regionMapRoot.animator.Play(((Object) ((Component) portal).gameObject).name);
      this.SetCameraToMiddlePoint(portal);
      this.SetPlayerMakerToStartPosition(portal, reverse);
      SoundManager.PlayOneShotUISE(40000032);
      GameObject gameObject = ResourceUtility.Instantiate<Object>(this.topEffectPrefab) as GameObject;
      rymFX rym = gameObject.GetComponent<rymFX>();
      rym.Cameras = new Camera[1]{ this._camera };
      rym.ViewShift = 0.0f;
      portal.Open(gameObject.transform, this.regionMapRoot.animator, false, 1f, (System.Action) (() =>
      {
        if (this.calledExit)
          return;
        GameObject locationSpot = this.CreateLocationSpot(portal.toLocation, SpotManager.ICON_TYPE.CHILD_REGION, true);
        if (Object.op_Inequality((Object) locationSpot, (Object) null))
        {
          locationSpot.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
          TweenScale.Begin(locationSpot, 0.3f, Vector3.one);
        }
        this.StartCoroutine(this.DoExitEvent(portal, rym, reverse: reverse, findDungeon: true));
      }));
    }
  }

  public void OpenNewLocation()
  {
    RegionMapPortal portal;
    bool reverse = this.IsPortalReverseAndGetPortalData((int) this.portalData.portalID, out portal);
    if (Object.op_Equality((Object) portal, (Object) null))
    {
      this.RequestEvent("EXIT");
    }
    else
    {
      string name = ((Object) ((Component) portal).gameObject).name;
      if (reverse)
        name += "_R";
      this.regionMapRoot.animator.Play(name);
      this.SetCameraToMiddlePoint(portal);
      this.SetPlayerMakerToStartPosition(portal, reverse);
      GameObject effect = ResourceUtility.Instantiate<Object>(this.topEffectPrefab) as GameObject;
      rymFX rym = effect.GetComponent<rymFX>();
      rym.Cameras = new Camera[1]{ this._camera };
      rym.ViewShift = 0.0f;
      float endTime = 1f;
      if (this.eventData.IsEncounterBossEvent())
        endTime = 0.4f;
      SoundManager.PlayOneShotUISE(40000032);
      portal.Open(effect.transform, this.regionMapRoot.animator, reverse, endTime, (System.Action) (() =>
      {
        if (this.calledExit)
          return;
        RegionMapLocation location = portal.toLocation;
        if (this.eventData.IsEncounterBossEvent())
        {
          if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
          {
            MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Play(this.eventData.enemyType);
            MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.FadeOut(MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.encounterBossCutInTime + 2f, 0.3f, (System.Action) (() =>
            {
              if (!Object.op_Inequality((Object) this.fieldQuestWarningRoot, (Object) null))
                return;
              Object.Destroy((Object) this.fieldQuestWarningRoot);
            }));
          }
          if (Object.op_Inequality((Object) effect, (Object) null))
            EffectManager.ReleaseEffect(effect);
          this.StartCoroutine(this.DoExitEncounterBossEvent());
        }
        else
        {
          if (reverse)
            location = portal.fromLocation;
          GameObject locationSpot = this.CreateLocationSpot(location, SpotManager.ICON_TYPE.NEW, true);
          if (Object.op_Inequality((Object) locationSpot, (Object) null))
          {
            locationSpot.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            TweenScale.Begin(locationSpot, 0.3f, Vector3.one);
            SoundManager.PlayOneShotUISE(40000033);
          }
          this.StartCoroutine(this.DoExitEvent(portal, rym, reverse: reverse));
        }
      }));
    }
  }

  private IEnumerator DoExitEncounterBossEvent()
  {
    yield return (object) new WaitForSeconds(MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.encounterBossCutInTime);
    this.OnQuery_EXIT();
  }

  private IEnumerator DoExitEvent(
    RegionMapPortal portal,
    rymFX effect,
    float delay = 0.0f,
    bool reverse = false,
    bool findDungeon = false)
  {
    if (Object.op_Inequality((Object) effect, (Object) null))
    {
      EffectManager.ReleaseEffect(((Component) effect).gameObject);
      effect = (rymFX) null;
    }
    yield return (object) new WaitForSeconds(delay);
    LoadObject loadObj = (LoadObject) null;
    if (findDungeon)
    {
      LoadingQueue loadQueue = new LoadingQueue((MonoBehaviour) this);
      loadObj = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionMap_" + Singleton<FieldMapTable>.I.GetFieldMapData((uint) portal.toLocation.mapId).childRegionId.ToString("D3"));
      if (Object.op_Inequality((Object) null, (Object) this.dungeonOpenEffect))
      {
        EffectCtrl component1 = ((Component) this.dungeonOpenEffect).GetComponent<EffectCtrl>();
        component1.Reset();
        for (int index = 0; index < component1.particles.Length; ++index)
        {
          ParticleSystem particle = component1.particles[index];
          if (!Object.op_Equality((Object) null, (Object) particle))
          {
            Renderer component2 = ((Component) particle).GetComponent<Renderer>();
            if (!Object.op_Equality((Object) null, (Object) component2))
              component2.sortingOrder = 2;
          }
        }
        ((Component) this.dungeonOpenEffect).gameObject.SetActive(true);
        AudioClip attachedAudioClip = component1.attachedAudioClip;
        if (Object.op_Inequality((Object) attachedAudioClip, (Object) null))
        {
          int attachedAudioSettingId = component1.attachedAudioSettingID;
          SoundManager.PlayOneShotUISE(attachedAudioClip, attachedAudioSettingId);
        }
        yield return (object) new WaitForSeconds(component1.waitTime);
      }
      if (loadQueue.IsLoading())
        yield return (object) loadQueue.Wait();
      loadQueue = (LoadingQueue) null;
    }
    TweenScale.Begin(((Component) this.playerMarker).gameObject, MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerScaleTime, Vector3.zero);
    yield return (object) new WaitForSeconds(MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerScaleTime);
    float timer = 0.0f;
    Vector3 target = ((Component) portal.toLocation).transform.position;
    this.playerMarker.SetParent(((Component) portal.toLocation).transform);
    if (reverse)
    {
      target = ((Component) portal.fromLocation).transform.position;
      this.playerMarker.SetParent(((Component) portal.fromLocation).transform);
    }
    this.playerMarker.localPosition = MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerOffset;
    target = Vector3.op_Subtraction(target, Vector3.op_Multiply(((Component) this._camera).transform.forward, MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraDistance));
    Vector3 startPos = ((Component) this._camera).transform.position;
    TweenScale.Begin(((Component) this.playerMarker).gameObject, MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerScaleTime, Vector3.one);
    while ((double) timer <= (double) MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraMoveTime)
    {
      timer += Time.deltaTime;
      ((Component) this._camera).transform.position = Vector3.Lerp(startPos, target, timer / MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraMoveTime);
      yield return (object) null;
    }
    ((Component) this._camera).transform.position = target;
    yield return (object) new WaitForSeconds(MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventRemainTime);
    if (findDungeon)
      yield return (object) this.StartCoroutine(this.DoFindNewDungeonEvent(portal, loadObj));
    this.OnQuery_EXIT();
  }

  private IEnumerator DoFindNewDungeonEvent(RegionMapPortal portal, LoadObject newRegion)
  {
    if (Object.op_Inequality((Object) this.blurFilter, (Object) null))
    {
      bool wait = true;
      this.blurFilter.CacheRenderTarget((System.Action) (() =>
      {
        ((Component) this.playerMarker).gameObject.SetActive(false);
        this.playerMarker.SetParent(this._transform);
        wait = false;
      }), true);
      while (wait)
        yield return (object) null;
      this.uiFrontMapSprite.alpha = 1f;
      this.spots.ClearAllSpot();
      Object.Destroy((Object) ((Component) this.regionMapRoot).gameObject);
      RegionMapLocation newLocation = (RegionMapLocation) null;
      if (newRegion != null)
      {
        this.regionMapRoot = ((Component) ResourceUtility.Realizes(newRegion.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform)).gameObject.GetComponent<RegionMapRoot>();
        if (Object.op_Inequality((Object) this.regionMapRoot, (Object) null))
        {
          wait = true;
          this.regionMapRoot.InitPortalStatus((System.Action) (() => wait = false));
          while (wait)
            yield return (object) null;
          this.CreateVisitedLocationSpot();
          newLocation = this.regionMapRoot.FindLocation(portal.toLocation.mapId);
          if (Object.op_Inequality((Object) newLocation, (Object) null))
          {
            this.SetCameraToLocation(newLocation);
            this.playerMarker.SetParent(((Component) newLocation).transform);
            this.playerMarker.localPosition = MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerOffset;
          }
        }
      }
      wait = true;
      float duration = 0.25f;
      Vector2 blurCenter;
      // ISSUE: explicit constructor call
      ((Vector2) ref blurCenter).\u002Ector(0.5f, 0.5f);
      this.blurFilter.StartBlurFilter(0.01f, 0.25f, duration, blurCenter, (System.Action) (() => wait = false));
      this.uiMapSprite.alpha = 0.0f;
      TweenAlpha.Begin(((Component) this.uiMapSprite).gameObject, duration, 1f);
      TweenAlpha.Begin(((Component) this.uiFrontMapSprite).gameObject, duration, 0.0f);
      while (wait)
        yield return (object) null;
      yield return (object) new WaitForSeconds(1f);
      if (Object.op_Inequality((Object) this.regionMapRoot, (Object) null) && Object.op_Inequality((Object) newLocation, (Object) null))
      {
        GameObject locationSpot = this.CreateLocationSpot(newLocation, SpotManager.ICON_TYPE.NEW, true);
        if (Object.op_Inequality((Object) locationSpot, (Object) null))
        {
          locationSpot.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
          TweenScale.Begin(locationSpot, 0.3f, Vector3.one);
          SoundManager.PlayOneShotUISE(40000033);
        }
        yield return (object) new WaitForSeconds(0.5f);
        ((Component) this.playerMarker).gameObject.SetActive(true);
        this.playerMarker.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        TweenScale.Begin(((Component) this.playerMarker).gameObject, MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerScaleTime, Vector3.one);
      }
      yield return (object) new WaitForSeconds(1.5f);
      newLocation = (RegionMapLocation) null;
    }
  }

  private IEnumerator DoAfterWaitForSecond(float time, System.Action func)
  {
    yield return (object) new WaitForSeconds(time);
    if (func != null)
      func();
  }

  public override void Exit()
  {
    if (Object.op_Inequality((Object) this.windEffect, (Object) null))
      EffectManager.ReleaseEffect(((Component) this.windEffect).gameObject);
    if (this.spots != null)
      this.spots.ClearAllSpot();
    if (Object.op_Inequality((Object) this.regionMapRoot, (Object) null))
      Object.Destroy((Object) ((Component) this.regionMapRoot).gameObject);
    if (Object.op_Inequality((Object) this._camera, (Object) null))
      Object.Destroy((Object) ((Component) this._camera).gameObject);
    if (Object.op_Inequality((Object) null, (Object) this.dungeonOpenEffect))
      Object.Destroy((Object) this.dungeonOpenEffect);
    base.Exit();
  }

  private void LateUpdate()
  {
    this.UpdateTutorialTrigger();
    if (this.spots != null)
      this.spots.Update();
    if (!this.isUpdateRenderTexture)
      return;
    this.InitMapSprite(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
    this.isUpdateRenderTexture = false;
  }

  private void UpdateTutorialTrigger()
  {
    if (Object.op_Equality((Object) this.tutorialTrigger, (Object) null))
      return;
    Vector3 worldPoint = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(this._camera.WorldToScreenPoint(this.tutorialTriggerPos));
    worldPoint.z = 0.0f;
    this.tutorialTrigger.position = worldPoint;
  }

  private void OnApplicationPause(bool paused) => this.isUpdateRenderTexture = !paused;

  private void onClick(GameObject g)
  {
    if (this.eventData.IsEncounterBossEvent())
      return;
    this.OnQuery_EXIT();
  }

  public static bool IsValidRegionFromMapId(uint mapId)
  {
    return Singleton<FieldMapTable>.IsValid() && WorldMapOpenNewField.IsValidRegion(Singleton<FieldMapTable>.I.GetFieldMapData(mapId));
  }

  public static bool IsValidRegion(FieldMapTable.FieldMapTableData mapData)
  {
    if (mapData == null || !Singleton<RegionTable>.IsValid())
      return false;
    RegionTable.Data[] data = Singleton<RegionTable>.I.GetData();
    return data != null && data.Length != 0 && Array.Find<RegionTable.Data>(data, (Predicate<RegionTable.Data>) (o => (int) o.regionId == (int) mapData.regionId)) != null;
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

  public enum EVENT_TYPE
  {
    NONE,
    ONLY_CAMERA_MOVE,
    ENCOUNTER_BOSS,
    QUEST_TO_FIELD,
    OPEN_NEW_DUNGEON,
    EXIST_IN_DUNGEON,
  }

  public enum AUDIO
  {
    DRAW_LINE = 40000032, // 0x02625A20
    APEAR_LOCATION = 40000033, // 0x02625A21
  }

  public class SectionEventData
  {
    private WorldMapOpenNewField.EVENT_TYPE eventType;

    public SectionEventData(WorldMapOpenNewField.EVENT_TYPE _eventType, ENEMY_TYPE _enemyType)
    {
      this.eventType = _eventType;
      this.enemyType = _enemyType;
    }

    public ENEMY_TYPE enemyType { get; private set; }

    public bool IsOnlyCameraMoveEvent()
    {
      return this.eventType == WorldMapOpenNewField.EVENT_TYPE.ONLY_CAMERA_MOVE;
    }

    public bool IsEnterDungeon()
    {
      return this.eventType == WorldMapOpenNewField.EVENT_TYPE.EXIST_IN_DUNGEON;
    }

    public bool IsFindNewDungeon()
    {
      return this.eventType == WorldMapOpenNewField.EVENT_TYPE.OPEN_NEW_DUNGEON;
    }

    public bool IsEncounterBossEvent()
    {
      return this.eventType == WorldMapOpenNewField.EVENT_TYPE.ENCOUNTER_BOSS;
    }

    public bool IsQuestToField()
    {
      return this.eventType == WorldMapOpenNewField.EVENT_TYPE.QUEST_TO_FIELD;
    }
  }
}
