using Network;
using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionMap : GameSection
{
	protected enum UI
	{
		OBJ_SELECT_DIFFICULTY,
		BTN_CURRENT_DIFFICULTY_NORMAL,
		BTN_CURRENT_DIFFICULTY_HARD,
		SOR_BORDER_TITLE
	}

	private enum TWEEN_ANIMATION
	{
		OPENING,
		ENDING
	}

	public class SpotEventData
	{
		public uint mapId;

		public uint childRegionId;
	}

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
		isInGame = (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene");
		this.StartCoroutine("DoInitialize");
	}

	private IEnumerator DoInitialize()
	{
		int eventId = (int)GameSection.GetEventData();
		FieldMapTable.FieldMapTableData mapData = null;
		switch (eventId)
		{
		case -4:
			regionId = MonoBehaviourSingleton<WorldMapManager>.I.eventMapRegionID;
			break;
		case -1:
			directOpen = true;
			mapData = Singleton<FieldMapTable>.I.GetFieldMapData(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
			break;
		case -3:
		case -2:
			directOpen = true;
			isShowDescription = false;
			isTutorial = (-3 == eventId);
			displayQuestTargetMode = MonoBehaviourSingleton<WorldMapManager>.I.isDisplayQuestTargetMode();
			if (displayQuestTargetMode)
			{
				MonoBehaviourSingleton<WorldMapManager>.I.PopDisplayQuestTarget(out questTargetMapID, out questTargetPortalIDs);
			}
			if (WorldMapManager.IsValidPortalIDs(questTargetPortalIDs))
			{
				FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData((uint)questTargetPortalIDs[0]);
				if (portalData != null)
				{
					mapData = Singleton<FieldMapTable>.I.GetFieldMapData(portalData.srcMapID);
				}
			}
			else
			{
				mapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)questTargetMapID);
			}
			isEventMap = FieldManager.IsEventMap(mapData.mapID);
			break;
		default:
			regionId = eventId;
			break;
		}
		if (mapData != null)
		{
			regionId = (int)mapData.regionId;
		}
		isEventMap = FieldManager.IsEventRegion((uint)regionId);
		if (MonoBehaviourSingleton<WorldMapManager>.I.NeedDirectionOpenRegion(regionId))
		{
			base.Initialize();
			yield break;
		}
		LoadingQueue loadQueue = new LoadingQueue(this);
		LoadObject loadedLocationSpotRoot = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "LocationSpotRoot");
		LoadObject loadedLocationSpot = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "LocationSpot");
		LoadObject loadedPlayerMarker = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "PlayerMarker");
		LoadObject loadObj = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionMap_" + regionId.ToString("D3"));
		LoadObject loadedEffect = loadQueue.LoadEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_bg_questmap_01");
		if (loadQueue.IsLoading())
		{
			yield return loadQueue.Wait();
		}
		parent = (MonoBehaviourSingleton<GameSceneManager>.I.FindSection("WorldMap") as WorldMap);
		spots = new SpotManager(_targetCamera: parent.worldMapCamera._camera, _spotRootPrefab: loadedLocationSpotRoot.loadedObject as GameObject, _spotPrefab: loadedLocationSpot.loadedObject as GameObject);
		spots.CreateSpotRoot();
		redCircle = spots.spotRootTransform.Find("RedCircle");
		portalGuideTxt = spots.spotRootTransform.Find("PortalGuideTxt");
		UIPanel spotPanel = spots.spotRootTransform.GetComponent<UIPanel>();
		if (spotPanel != null)
		{
			spotPanel.depth = base.baseDepth + 1;
		}
		regionMapRoot = ResourceUtility.Realizes(loadObj.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform).get_gameObject().GetComponent<RegionMapRoot>();
		if (regionMapRoot != null)
		{
			bool wait = true;
			regionMapRoot.InitPortalStatus(delegate
			{
				wait = false;
			});
			while (wait)
			{
				yield return null;
			}
		}
		rootTransform = spots.SetRoot(base._transform);
		rootTransform.get_gameObject().SetActive(true);
		rootPanel = rootTransform.GetComponent<UIPanel>();
		if (rootPanel != null)
		{
			defaultDepth = rootPanel.depth;
		}
		uiMapSprite = rootTransform.Find("Map").get_gameObject().GetComponent<UITexture>();
		uiParentMapSprite = rootTransform.Find("ParentMap").get_gameObject().GetComponent<UITexture>();
		InitMapSprite(isPortrait: false);
		if (!directOpen)
		{
			StartShowingTween();
		}
		windEffect = ResourceUtility.Realizes(loadedEffect.loadedObject, parent.worldMapCamera.get_transform()).get_gameObject().GetComponent<rymFX>();
		windEffect.Cameras = (Camera[])new Camera[1]
		{
			parent.worldMapCamera._camera
		};
		windEffect.get_gameObject().set_layer(LayerMask.NameToLayer("WorldMap"));
		spots.spotRootTransform.GetComponent<UIPanel>().RebuildAllDrawCalls();
		tweenAnimations = spots.spotRootTransform.GetComponentsInChildren<UITweenCtrl>();
		playerMarker = ResourceUtility.Realizes(loadedPlayerMarker.loadedObject);
		PlayerMarker playerMarkerCom = playerMarker.GetComponent<PlayerMarker>();
		if (null != playerMarkerCom)
		{
			playerMarkerCom.SetCamera(parent.worldMapCamera._camera.get_transform());
		}
		SetFirstCameraPos();
		base.collectUI = base._transform;
		Transform closeButtonTransform = base._transform.Find("LocationSpotRoot/CLOSE_BTN");
		if (closeButtonTransform != null)
		{
			closeButton = closeButtonTransform.GetComponent<UIWidget>();
		}
		Transform announceTapTransform = base._transform.Find("LocationSpotRoot/AnnounceTap");
		if (announceTapTransform != null)
		{
			announceTap = announceTapTransform.get_gameObject();
			SyncBorderTitleAnctors();
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += InitMapSprite;
		}
		UpdateDifficultyButton();
		InitDeliveryTargetIdLists();
		base.Initialize();
	}

	private void SyncBorderTitleAnctors()
	{
		if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.HasSafeArea)
		{
			UIWidget componentInChildren = announceTap.GetComponentInChildren<UIWidget>();
			DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
			if (!(componentInChildren == null))
			{
				componentInChildren.leftAnchor.absolute = specialDeviceInfo.RegionMapBorderTitleAnchor.left;
				componentInChildren.rightAnchor.absolute = specialDeviceInfo.RegionMapBorderTitleAnchor.right;
				componentInChildren.bottomAnchor.absolute = specialDeviceInfo.RegionMapBorderTitleAnchor.bottom;
				componentInChildren.topAnchor.absolute = specialDeviceInfo.RegionMapBorderTitleAnchor.top;
				componentInChildren.UpdateAnchors();
			}
		}
	}

	private void UpdateDifficultyButton(bool forceOff = false)
	{
		Transform val = null;
		Transform spotRootTransform = spots.spotRootTransform;
		if (forceOff)
		{
			SetActive(spotRootTransform, UI.OBJ_SELECT_DIFFICULTY, is_visible: false);
			return;
		}
		RegionTable.Data data = Singleton<RegionTable>.I.GetData((uint)regionId);
		bool flag = IsExistedHard();
		if (flag && data.difficulty != REGION_DIFFICULTY_TYPE.HARD)
		{
			RegionTable.Data data2 = Singleton<RegionTable>.I.GetData(data.groupId, REGION_DIFFICULTY_TYPE.HARD);
			if (data2 != null)
			{
				isOpenedHard = MonoBehaviourSingleton<WorldMapManager>.I.IsOpenRegion(data2.regionId);
				flag = isOpenedHard;
			}
		}
		else if (data.difficulty == REGION_DIFFICULTY_TYPE.HARD)
		{
			isOpenedHard = true;
		}
		else
		{
			isOpenedHard = false;
		}
		SetActive(spotRootTransform, UI.OBJ_SELECT_DIFFICULTY, flag);
		if (flag)
		{
			SetActive(spotRootTransform, UI.BTN_CURRENT_DIFFICULTY_NORMAL, data.difficulty == REGION_DIFFICULTY_TYPE.NORMAL);
			SetActive(spotRootTransform, UI.BTN_CURRENT_DIFFICULTY_HARD, data.difficulty == REGION_DIFFICULTY_TYPE.HARD);
			UIWidget component = FindCtrl(spotRootTransform, UI.OBJ_SELECT_DIFFICULTY).GetComponent<UIWidget>();
			if (!(component == null))
			{
				this.StartCoroutine(FadeWidget(component, 0, 1, 0.3f));
			}
		}
	}

	private bool IsExistedHard()
	{
		RegionTable.Data data = Singleton<RegionTable>.I.GetData((uint)regionId);
		if (data == null)
		{
			return false;
		}
		if (data.difficulty == REGION_DIFFICULTY_TYPE.HARD)
		{
			return true;
		}
		if (data.HasGroup())
		{
			return true;
		}
		return false;
	}

	private IEnumerator FadeWidget(UIWidget target, int start, int end, float duration)
	{
		float time = 0f;
		while (time < duration)
		{
			time += Time.get_deltaTime();
			float prog = time / duration;
			float a = target.alpha = Mathf.Lerp((float)start, (float)end, prog);
			yield return null;
		}
	}

	private void StartShowingTween()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		if (uiMapSprite != null)
		{
			uiMapSprite.alpha = 0f;
			TweenAlpha.Begin(uiMapSprite.get_gameObject(), 0.3f, 1f);
			uiMapSprite.get_transform().set_localScale(Vector3.get_one() * 0.8f);
			TweenScale.Begin(uiMapSprite.get_gameObject(), 0.3f, Vector3.get_one());
		}
		if (uiParentMapSprite != null)
		{
			uiParentMapSprite.alpha = 1f;
			TweenScale.Begin(uiParentMapSprite.get_gameObject(), 0.3f, Vector3.get_one() * 1.2f);
			TweenAlpha.Begin(uiParentMapSprite.get_gameObject(), 0.3f, 0f);
		}
	}

	private void InitMapSprite(bool isPortrait)
	{
		if (uiMapSprite != null)
		{
			if (null == parent.worldMapCamera._camera.get_targetTexture())
			{
				parent.worldMapCamera.Restore();
			}
			uiMapSprite.mainTexture = parent.worldMapCamera._camera.get_targetTexture();
			uiMapSprite.width = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth;
			uiMapSprite.height = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight;
		}
		if (uiParentMapSprite != null)
		{
			if (null == parent.blurFilter.filteredTexture)
			{
				parent.blurFilter.Restore();
			}
			uiParentMapSprite.alpha = 0f;
			uiParentMapSprite.mainTexture = parent.blurFilter.filteredTexture;
			uiParentMapSprite.width = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth;
			uiParentMapSprite.height = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight;
		}
	}

	private void CreateVisitedLocationSpotIfNeed()
	{
		if (spots.Count >= 1)
		{
			return;
		}
		OutGameSettingsManager.QuestMap questMap = MonoBehaviourSingleton<OutGameSettingsManager>.I.questMap;
		MonoBehaviourSingleton<FilterManager>.I.StopBlur(questMap.cameraMoveTime);
		spotMapIdDic = new Dictionary<SpotManager.Spot, uint>();
		if (regionMapRoot == null)
		{
			return;
		}
		for (int i = 0; i < regionMapRoot.locations.Length; i++)
		{
			RegionMapLocation regionMapLocation = regionMapRoot.locations[i];
			FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)regionMapLocation.mapId);
			if (fieldMapData == null && regionMapLocation.mapId != 0)
			{
				continue;
			}
			if (fieldMapData != null)
			{
				if (!FieldManager.IsShowPortal(fieldMapData.jumpPortalID))
				{
					CreateLocationSpot(regionMapLocation, SpotManager.ICON_TYPE.INVISIBLE);
					continue;
				}
				if (!MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(fieldMapData))
				{
					CreateLocationSpot(regionMapLocation, SpotManager.ICON_TYPE.NOT_OPENED);
					continue;
				}
			}
			SpotManager.ICON_TYPE iCON_TYPE = SpotManager.ICON_TYPE.CLEARED;
			bool flag = false;
			bool isExistDelivery = false;
			SpotManager.HAPPEN_CONDITION happen = SpotManager.HAPPEN_CONDITION.NONE;
			if (regionMapLocation.portal.Length > 0)
			{
				for (int j = 0; j < regionMapLocation.portal.Length; j++)
				{
					RegionMapPortal regionMapPortal = regionMapLocation.portal[j];
					if (!regionMapPortal.IsVisited() && regionMapPortal.IsShow())
					{
						iCON_TYPE = SpotManager.ICON_TYPE.NEW;
					}
					if (flag)
					{
						continue;
					}
					int[] locationNumbers = GetLocationNumbers(regionMapPortal.get_name());
					if (i == locationNumbers[0] && GameSaveData.instance.isNewReleasePortal((uint)regionMapPortal.entranceId))
					{
						if (regionMapPortal.IsVisited())
						{
							GameSaveData.instance.newReleasePortals.Remove((uint)regionMapPortal.entranceId);
						}
						else
						{
							flag = true;
						}
					}
					if (i == locationNumbers[1] && GameSaveData.instance.isNewReleasePortal((uint)regionMapPortal.exitId))
					{
						if (regionMapPortal.IsVisited())
						{
							GameSaveData.instance.newReleasePortals.Remove((uint)regionMapPortal.exitId);
						}
						else
						{
							flag = true;
						}
					}
				}
			}
			if (fieldMapData != null)
			{
				if (FieldManager.IsToHardPortal(fieldMapData.jumpPortalID))
				{
					iCON_TYPE = SpotManager.ICON_TYPE.HARD;
					if (iCON_TYPE == SpotManager.ICON_TYPE.NEW)
					{
						iCON_TYPE = SpotManager.ICON_TYPE.HARD_NEW;
					}
				}
				if (fieldMapData.hasChildRegion && fieldMapData.childRegionId != regionId)
				{
					iCON_TYPE = SpotManager.ICON_TYPE.CHILD_REGION;
				}
				if (isShowDescription)
				{
					isExistDelivery = IsExistDeliveryTarget(fieldMapData.mapID);
					happen = GetHappenCondition(fieldMapData.mapID);
				}
			}
			CreateLocationSpot(regionMapLocation, iCON_TYPE, flag, isExistDelivery, happen);
		}
		for (int k = 0; k < regionMapRoot.portals.Length; k++)
		{
			RegionMapPortal regionMapPortal2 = regionMapRoot.portals[k];
			if (regionMapPortal2.IsVisited())
			{
				regionMapPortal2.Open();
			}
		}
	}

	private bool IsExistDeliveryTarget(uint mapId)
	{
		if (DeliveryTargetMapIds.Contains(mapId))
		{
			return true;
		}
		List<FieldMapTable.EnemyPopTableData> enemyPopList = Singleton<FieldMapTable>.I.GetEnemyPopList(mapId);
		int i = 0;
		for (int count = enemyPopList.Count; i < count; i++)
		{
			if (DeliveryTargetEnemyIds.Contains(enemyPopList[i].enemyID))
			{
				return true;
			}
		}
		return false;
	}

	private SpotManager.HAPPEN_CONDITION GetHappenCondition(uint mapId)
	{
		Dictionary<uint, uint> questIdEventIdDic = Singleton<QuestToFieldTable>.I.GetQuestIdEventIdDic(mapId);
		if (questIdEventIdDic == null)
		{
			return SpotManager.HAPPEN_CONDITION.NONE;
		}
		bool flag = false;
		foreach (KeyValuePair<uint, uint> item in questIdEventIdDic)
		{
			uint key = item.Key;
			QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(key);
			if (questData != null && questData.IsMissionExist() && (item.Value < 1 || MonoBehaviourSingleton<QuestManager>.I.IsEventPlayableWith((int)item.Value, NetworkNative.getNativeVersionFromName())))
			{
				flag = true;
				ClearStatusQuest clearStatusQuestData = MonoBehaviourSingleton<QuestManager>.I.GetClearStatusQuestData(key);
				if (clearStatusQuestData == null)
				{
					return SpotManager.HAPPEN_CONDITION.NOT_CLEAR;
				}
				int i = 0;
				for (int count = clearStatusQuestData.missionStatus.Count; i < count; i++)
				{
					CLEAR_STATUS cLEAR_STATUS = (CLEAR_STATUS)clearStatusQuestData.missionStatus[i];
					if (cLEAR_STATUS <= CLEAR_STATUS.NOT_CLEAR)
					{
						return SpotManager.HAPPEN_CONDITION.NOT_CLEAR;
					}
				}
			}
		}
		if (!flag)
		{
			return SpotManager.HAPPEN_CONDITION.NONE;
		}
		return SpotManager.HAPPEN_CONDITION.ALL_CLEAR;
	}

	private void CreateLocationSpot(RegionMapLocation location, SpotManager.ICON_TYPE iconStatus = SpotManager.ICON_TYPE.CLEARED, bool attach_new_release_portal = false, bool isExistDelivery = false, SpotManager.HAPPEN_CONDITION happen = SpotManager.HAPPEN_CONDITION.NONE)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		if (location.mapId == 0)
		{
			spots.AddSpot(0, MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionTextList().Find((GameSceneTables.TextData textData) => textData.key == "STR_HOME").text, location.get_transform().get_position(), SpotManager.ICON_TYPE.HOME, "HOME", isNew: false, canUnlockNewPortal: false, viewEnemyPopBallon: false, 0);
			return;
		}
		string event_name = "SELECT";
		if (iconStatus == SpotManager.ICON_TYPE.NOT_OPENED)
		{
			event_name = string.Empty;
		}
		if (iconStatus == SpotManager.ICON_TYPE.CHILD_REGION)
		{
			event_name = "SELECT_CHILD";
		}
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)location.mapId);
		if (fieldMapData != null)
		{
			bool viewEnemyPopBallon = enemyPopBallonMapIds != null && enemyPopBallonMapIds.Contains(fieldMapData.mapID);
			SpotEventData spotEventData = new SpotEventData();
			spotEventData.mapId = fieldMapData.mapID;
			spotEventData.childRegionId = fieldMapData.childRegionId;
			SpotManager.Spot spot = spots.AddSpot((int)spotEventData.mapId, fieldMapData.mapName, location.get_transform().get_position(), iconStatus, event_name, isNew: false, attach_new_release_portal, viewEnemyPopBallon, spotEventData, location.icon, isExistDelivery, happen);
			if (iconStatus == SpotManager.ICON_TYPE.INVISIBLE)
			{
				spot._transform.get_gameObject().SetActive(false);
			}
			if (iconStatus != SpotManager.ICON_TYPE.NOT_OPENED && iconStatus != SpotManager.ICON_TYPE.INVISIBLE)
			{
				spotMapIdDic[spot] = fieldMapData.mapID;
			}
		}
	}

	private void SetFirstCameraPos()
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
		playerMarker.get_gameObject().SetActive(true);
		if (fieldMapData == null)
		{
			parent.worldMapCamera.targetPos = regionMapRoot.locations[0].get_transform().get_position();
			if (regionId == 0)
			{
				playerMarker.SetParent(regionMapRoot.locations[0].get_transform());
			}
			else
			{
				playerMarker.get_gameObject().SetActive(false);
				playerMarker.SetParent(this.get_transform());
			}
		}
		else if (!FieldManager.HasWorldMap(MonoBehaviourSingleton<FieldManager>.I.currentMapID) || fieldMapData.regionId != (uint)regionId)
		{
			parent.worldMapCamera.targetPos = regionMapRoot.locations[0].get_transform().get_position();
			playerMarker.get_gameObject().SetActive(false);
			playerMarker.SetParent(this.get_transform());
		}
		else
		{
			RegionMapLocation regionMapLocation = regionMapRoot.FindLocation((int)fieldMapData.mapID);
			if (regionMapLocation != null)
			{
				parent.worldMapCamera.targetPos = regionMapLocation.get_transform().get_position();
				playerMarker.SetParent(regionMapLocation.get_transform());
			}
		}
		playerMarker.set_localPosition(MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerOffset);
	}

	private void PlayTween(TWEEN_ANIMATION type, EventDelegate.Callback onComplete = null)
	{
		UITweenCtrl uITweenCtrl = Array.Find(tweenAnimations, (UITweenCtrl t) => t.id == (int)type);
		if (uITweenCtrl != null)
		{
			uITweenCtrl.Reset();
			uITweenCtrl.Play(forward: true, onComplete);
		}
	}

	protected override void OnOpen()
	{
		if (MonoBehaviourSingleton<WorldMapManager>.I.NeedDirectionOpenRegion(regionId))
		{
			isOpened = true;
			base.OnOpen();
			return;
		}
		if (parent != null && !isOpened)
		{
			if (!directOpen)
			{
				parent.EnterRegionMapEvent(delegate
				{
					CreateVisitedLocationSpotIfNeed();
				});
			}
			else
			{
				parent.DisableWorldMapObject();
				CreateVisitedLocationSpotIfNeed();
				if (displayQuestTargetMode)
				{
					parent.worldMapCamera.isInteractive = false;
					if (isTutorial)
					{
						DisplayQuestTargetTutorial();
					}
					else
					{
						DisplayQuestTarget();
					}
				}
			}
		}
		SetButtonsAlpha();
		PlayTween(TWEEN_ANIMATION.OPENING);
		isOpened = true;
		base.OnOpen();
	}

	public override void StartSection()
	{
		if (MonoBehaviourSingleton<WorldMapManager>.I.NeedDirectionOpenRegion(regionId))
		{
			MonoBehaviourSingleton<WorldMapManager>.I.transferInfo = new WorldMapManager.TransferInfo(regionId, directInGame: false);
			DispatchEvent("WORLDMAP");
		}
	}

	private void UpdateDeliveryTargetMarkers()
	{
		if (spotMapIdDic == null)
		{
			return;
		}
		InitDeliveryTargetIdLists();
		List<SpotManager.Spot> allSpots = spots.GetAllSpots();
		if (allSpots == null)
		{
			return;
		}
		int i = 0;
		for (int count = allSpots.Count; i < count; i++)
		{
			SpotManager.Spot spot = allSpots[i];
			uint value = 0u;
			if (spotMapIdDic.TryGetValue(spot, out value))
			{
				spot.UpdateDeliveryTargetMarker(IsExistDeliveryTarget(value));
			}
		}
	}

	private void SetButtonsAlpha()
	{
		RegionTable.Data data = Singleton<RegionTable>.I.GetData((uint)regionId);
		if (isEventMap)
		{
			HideButtons();
		}
		else
		{
			if (data == null)
			{
				return;
			}
			Transform val = this.get_transform().Find("LocationSpotRoot/BACK_REGION_BTN");
			if (val != null)
			{
				UIWidget component = val.GetComponent<UIWidget>();
				if (component != null)
				{
					component.alpha = ((!data.hasParentRegion()) ? 0f : 1f);
				}
			}
			val = this.get_transform().Find("LocationSpotRoot/BACK_BTN");
			if (val != null)
			{
				UIWidget component2 = val.GetComponent<UIWidget>();
				if (component2 != null)
				{
					component2.alpha = ((!data.hasParentRegion()) ? 1f : 0f);
				}
			}
		}
	}

	private void HideButtons()
	{
		Transform[] array = (Transform[])new Transform[2]
		{
			base._transform.Find("LocationSpotRoot/BACK_REGION_BTN"),
			base._transform.Find("LocationSpotRoot/BACK_BTN")
		};
		foreach (Transform val in array)
		{
			if (!(null == val))
			{
				UIWidget component = val.GetComponent<UIWidget>();
				if (null != component)
				{
					component.alpha = 0f;
				}
			}
		}
	}

	protected override void OnCloseStart()
	{
		this.StopAllCoroutines();
		base.collectUI = null;
		if (!isToDescription && parent != null)
		{
			parent.worldMapCamera.isInteractive = true;
		}
		MonoBehaviourSingleton<WorldMapManager>.I.ignoreTutorial = false;
		isToDescription = false;
		base.OnCloseStart();
	}

	public override void Exit()
	{
		if (windEffect != null)
		{
			EffectManager.ReleaseEffect(windEffect.get_gameObject());
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= InitMapSprite;
		}
		if (spots != null)
		{
			spots.ClearAllSpot();
		}
		FadeOutMap();
		MonoBehaviourSingleton<FilterManager>.I.StopBlur();
		if (parent != null)
		{
			parent.worldMapCamera.isInteractive = true;
		}
		MonoBehaviourSingleton<WorldMapManager>.I.ignoreTutorial = false;
		base.Exit();
	}

	private void OnQuery_SELECT()
	{
		if (isShowDescription)
		{
			HideInTheBack();
			isToDescription = true;
			GameSection.ChangeEvent("TO_DESCRIPTION");
		}
		else
		{
			MoveIngameMapOrHome();
		}
	}

	private void HideInTheBack()
	{
		if (rootPanel != null)
		{
			rootPanel.depth = 4000;
		}
		HideButtons();
		if (closeButton != null)
		{
			closeButton.alpha = 0f;
		}
		if (announceTap != null)
		{
			announceTap.SetActive(false);
		}
		parent.worldMapCamera.isInteractive = false;
	}

	private void OnCloseDialog()
	{
		PutOutInFront();
		base.collectUI = base._transform;
		parent.worldMapCamera.isInteractive = true;
		UpdateDeliveryTargetMarkers();
	}

	private void PutOutInFront()
	{
		if (rootPanel != null)
		{
			rootPanel.depth = defaultDepth;
		}
		SetButtonsAlpha();
		if (closeButton != null)
		{
			closeButton.alpha = 1f;
		}
		if (announceTap != null)
		{
			announceTap.SetActive(true);
		}
		parent.worldMapCamera.get_gameObject().SetActive(true);
	}

	private void OnQuery_TO_FIELD_OR_HOME()
	{
		HideInTheBack();
		MoveIngameMapOrHome();
	}

	private void MoveIngameMapOrHome()
	{
		if (MonoBehaviourSingleton<FilterManager>.I.IsEnabledBlur())
		{
			GameSection.StopEvent();
			return;
		}
		SpotEventData spotEventData = (SpotEventData)GameSection.GetEventData();
		uint mapId = spotEventData.mapId;
		if (mapId == MonoBehaviourSingleton<FieldManager>.I.currentMapID)
		{
			GameSection.StopEvent();
			if (UIInGameFieldMenu.IsValid())
			{
				UIInGameFieldMenu.I.OnClickPopMenu();
			}
			Transform val = Utility.Find(base._transform, "CLOSE_BACK");
			if (null != val)
			{
				UIButton component = val.GetComponent<UIButton>();
				if (null != component)
				{
					component.onClick.ForEach(delegate(EventDelegate o)
					{
						o.Execute();
					});
				}
				DispatchEvent("CLOSE");
			}
			return;
		}
		if (mapId == 0)
		{
			if (MonoBehaviourSingleton<InGameProgress>.IsValid())
			{
				MonoBehaviourSingleton<InGameProgress>.I.FieldToHome();
			}
			return;
		}
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(mapId);
		if (fieldMapData == null || fieldMapData.jumpPortalID == 0)
		{
			Log.Error("RegionMap.OnQuery_SELECT() jumpPortalID is not found.");
			return;
		}
		if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckPortalAndOpenUpdateAppDialog(fieldMapData.jumpPortalID, check_dst_quest: false))
		{
			GameSection.StopEvent();
			return;
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "InGameScene")
		{
			spots.ClearAllSpot();
			spots.spotRootTransform.get_gameObject().SetActive(false);
			MonoBehaviourSingleton<UIManager>.I.system.GetCtrl(UIManager.SYSTEM.DIALOG_BLOCKER).get_gameObject().SetActive(false);
			GameSection.StayEvent();
			CoopApp.EnterField(fieldMapData.jumpPortalID, 0u, delegate(bool is_matching, bool is_connect, bool is_regist)
			{
				if (!is_connect)
				{
					GameSection.ChangeStayEvent("COOP_SERVER_INVALID");
					GameSection.ResumeEvent(is_resume: true);
					AppMain i = MonoBehaviourSingleton<AppMain>.I;
					i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, (Action)delegate
					{
						DispatchEvent("CLOSE");
					});
				}
				else
				{
					GameSection.ResumeEvent(is_regist);
					if (is_regist)
					{
						MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("InGame");
					}
				}
			});
		}
		else if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<FieldManager>.I.currentMapID != mapId)
		{
			MonoBehaviourSingleton<InGameProgress>.I.PortalNext(fieldMapData.jumpPortalID);
			OutGameSettingsManager.QuestMap questMap = MonoBehaviourSingleton<OutGameSettingsManager>.I.questMap;
			MonoBehaviourSingleton<FilterManager>.I.StartBlur(questMap.cameraBlurTime, questMap.cameraBlurStrength, questMap.cameraMoveTime);
			spots.ClearAllSpot();
			MonoBehaviourSingleton<FieldManager>.I.useFastTravel = true;
		}
		IsCalledExit = true;
	}

	private void OnQuery_SELECT_CHILD()
	{
		SpotEventData spotEventData = GameSection.GetEventData() as SpotEventData;
		GameSection.StayEvent();
		if (spotEventData == null)
		{
			GameSection.ResumeEvent(is_resume: false);
			return;
		}
		regionId = (int)spotEventData.childRegionId;
		this.StartCoroutine(DoChangeRegion(withBlur: true));
	}

	private void OnQuery_BACK_REGION()
	{
		RegionTable.Data data = Singleton<RegionTable>.I.GetData((uint)regionId);
		if (data != null)
		{
			if (redCircle != null)
			{
				redCircle.get_gameObject().SetActive(false);
			}
			displayQuestTargetMode = false;
			regionId = (int)data.parentRegionId;
			this.StartCoroutine(DoChangeRegion(withBlur: false));
		}
	}

	private IEnumerator DoChangeRegion(bool withBlur)
	{
		bool wait = false;
		if (withBlur)
		{
			wait = true;
			parent.blurFilter.CacheRenderTarget(delegate
			{
				wait = false;
				uiParentMapSprite.alpha = 1f;
				spots.ClearAllSpot();
			}, reqWithFilter: true);
		}
		else
		{
			spots.ClearAllSpot();
		}
		LoadingQueue loadQueue = new LoadingQueue(this);
		LoadObject loadObj = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionMap_" + regionId.ToString("D3"));
		if (loadQueue.IsLoading())
		{
			yield return loadQueue.Wait();
		}
		while (wait)
		{
			yield return null;
		}
		Object.Destroy(regionMapRoot.get_gameObject());
		regionMapRoot = ResourceUtility.Realizes(loadObj.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform).get_gameObject().GetComponent<RegionMapRoot>();
		if (regionMapRoot != null)
		{
			wait = true;
			regionMapRoot.InitPortalStatus(delegate
			{
				wait = false;
			});
			while (wait)
			{
				yield return null;
			}
		}
		if (withBlur)
		{
			wait = true;
			float duration = 0.25f;
			Vector2 blurCenter = default(Vector2);
			blurCenter._002Ector(0.5f, 0.5f);
			parent.blurFilter.StartBlurFilter(0.01f, 0.25f, duration, blurCenter, delegate
			{
				wait = false;
			});
			uiMapSprite.alpha = 0f;
			TweenAlpha.Begin(uiMapSprite.get_gameObject(), duration, 1f);
			TweenAlpha.Begin(uiParentMapSprite.get_gameObject(), duration, 0f);
		}
		SetFirstCameraPos();
		isOpened = false;
		OnOpen();
		GameSection.ResumeEvent(is_resume: true);
		yield return null;
	}

	private void OnQuery_HOME()
	{
		if (MonoBehaviourSingleton<FilterManager>.I.IsEnabledBlur())
		{
			GameSection.StopEvent();
			return;
		}
		int num = (int)GameSection.GetEventData();
		if (num == MonoBehaviourSingleton<FieldManager>.I.currentMapID)
		{
			GameSection.StopEvent();
			return;
		}
		OutGameSettingsManager.QuestMap questMap = MonoBehaviourSingleton<OutGameSettingsManager>.I.questMap;
		MonoBehaviourSingleton<FilterManager>.I.StartBlur(questMap.cameraBlurTime, questMap.cameraBlurStrength, questMap.cameraMoveTime);
		spots.ClearAllSpot();
		if (MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			MonoBehaviourSingleton<InGameProgress>.I.FieldToHome();
		}
		IsCalledExit = true;
	}

	private void OnQuery_SECTION_BACK()
	{
		if (!IsCalledExit)
		{
			GameSection.StayEvent();
			DoExitUIEvent(delegate
			{
				GameSection.ResumeEvent(is_resume: true);
			});
			parent.worldMapCamera.isInteractive = true;
			MonoBehaviourSingleton<WorldMapManager>.I.ignoreTutorial = false;
		}
	}

	private void DoExitUIEvent(Action onComplete)
	{
		PlayTween(TWEEN_ANIMATION.ENDING, delegate
		{
			if (onComplete != null)
			{
				onComplete();
			}
		});
		spots.ClearAllSpot();
		FadeOutMap();
	}

	private void OnQuery_VIEW_POP_ENEMY_MAP()
	{
		enemyPopBallonMapIds = (GameSection.GetEventData() as List<uint>);
	}

	private void LateUpdate()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		if (spots != null)
		{
			spots.Update();
		}
		if (null != redCircle && redCircle.get_gameObject().get_activeSelf())
		{
			Camera camera = parent.worldMapCamera._camera;
			if (null != camera)
			{
				Vector3 val = camera.WorldToScreenPoint(redCircleOrgPos);
				val = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(val);
				val.z = 0f;
				redCircle.set_position(val);
			}
		}
		if (isUpdateRenderTexture)
		{
			InitMapSprite(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
			isUpdateRenderTexture = false;
		}
	}

	public void FadeOutMap()
	{
		this.StartCoroutine(DoFadeMap(1f, 0f, 0.3f, delegate
		{
			if (regionMapRoot != null)
			{
				Object.Destroy(regionMapRoot.get_gameObject());
				regionMapRoot = null;
			}
		}));
	}

	public void FadeInMap()
	{
		this.StartCoroutine(DoFadeMap(0f, 1f, 0.5f, delegate
		{
			CreateVisitedLocationSpotIfNeed();
		}));
	}

	private IEnumerator DoFadeMap(float from, float to, float time, Action onComplete)
	{
		if (regionMapRoot == null)
		{
			yield break;
		}
		Renderer r = regionMapRoot.GetComponentInChildren<Renderer>();
		if (!(r == null))
		{
			for (float timer = 0f; timer < time; timer += Time.get_deltaTime())
			{
				float alpha = Mathf.Lerp(from, to, timer / time);
				r.get_material().SetFloat("_Alpha", alpha);
				yield return null;
			}
			r.get_material().SetFloat("_Alpha", to);
			onComplete?.Invoke();
		}
	}

	protected override void OnDestroy()
	{
		if (regionMapRoot != null)
		{
			Object.Destroy(regionMapRoot.get_gameObject());
			regionMapRoot = null;
		}
		base.OnDestroy();
	}

	private void DisplayQuestTarget()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		FieldMapTable.PortalTableData portalData = GetPortalData();
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)questTargetMapID);
		bool isStepOver = IsStepOver(portalData);
		bool flag = IsSameRegionPlayer();
		Vector3 targetPosition = GetTargetPosition(fieldMapData, portalData, isStepOver);
		Vector3 val = targetPosition;
		if (flag)
		{
			RegionMapLocation playerLocation = GetPlayerLocation();
			if (null != playerLocation)
			{
				val = playerLocation.get_transform().get_position();
			}
		}
		SetRedCirclePosition(fieldMapData, portalData, isStepOver);
		parent.worldMapCamera.targetPos = val;
		this.StartCoroutine(DoDisplayQuestTarget(val, targetPosition));
	}

	private IEnumerator DoDisplayQuestTarget(Vector3 from, Vector3 to)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		yield return (object)new WaitForSeconds(0.8f);
		Vector3Interpolator ip = new Vector3Interpolator();
		if (0.1f < Vector3.Distance(from, to))
		{
			ip.Set(1f, from, to);
		}
		else
		{
			ip.Set(0f, from, to);
		}
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			parent.worldMapCamera.targetPos = ip.Get();
			yield return null;
		}
		if (null != redCircle)
		{
			yield return (object)new WaitForSeconds(0.5f);
			redCircle.get_gameObject().SetActive(true);
			TweenAlpha tweenAlpha = redCircle.GetComponent<TweenAlpha>();
			if (null != tweenAlpha)
			{
				while (tweenAlpha.get_isActiveAndEnabled())
				{
					yield return null;
				}
			}
		}
		parent.worldMapCamera.isInteractive = true;
		MonoBehaviourSingleton<WorldMapManager>.I.ignoreTutorial = false;
	}

	private void DisplayQuestTargetTutorial()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		FieldMapTable.PortalTableData portalData = GetPortalData();
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)questTargetMapID);
		bool isStepOver = IsStepOver(portalData);
		Vector3 targetPosition = GetTargetPosition(fieldMapData, portalData, isStepOver);
		Vector3 val = targetPosition;
		RegionMapLocation playerLocation = GetPlayerLocation();
		if (null != playerLocation)
		{
			val = playerLocation.get_transform().get_position();
		}
		Vector3 neighbor = Vector3.get_zero();
		int neighborMapID = GetNeighborMapID();
		RegionMapLocation regionMapLocation = regionMapRoot.FindLocation(neighborMapID);
		if (null != regionMapLocation)
		{
			neighbor = regionMapLocation.get_transform().get_position();
		}
		SetRedCirclePosition(fieldMapData, portalData, isStepOver);
		Transform icon = null;
		Transform button = null;
		SpotManager.Spot spot = spots.FindSpot(neighborMapID);
		if (null != portalGuideTxt && spot != null)
		{
			portalGuideTxt.SetParent(spot._transform);
			portalGuideTxt.set_localPosition(new Vector3(0f, -60f, 0f));
			icon = ((spot.type != 0) ? spot._transform.Find("SPR_ICON_CLEARED") : spot._transform.Find("SPR_ICON_NEW"));
			button = spot._transform.Find("SPR_BUTTON");
		}
		parent.worldMapCamera.targetPos = val;
		this.StartCoroutine(DoDisplayQuestTargetTutorial(val, targetPosition, neighbor, icon, button));
	}

	private IEnumerator DoDisplayQuestTargetTutorial(Vector3 from, Vector3 to, Vector3 neighbor, Transform icon, Transform button)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		Vector3Interpolator ip = new Vector3Interpolator();
		if (0.1f < Vector3.Distance(from, to))
		{
			ip.Set(1f, from, to);
		}
		else
		{
			ip.Set(0f, from, to);
		}
		yield return (object)new WaitForSeconds(0.8f);
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			parent.worldMapCamera.targetPos = ip.Get();
			yield return null;
		}
		if (null != redCircle)
		{
			yield return (object)new WaitForSeconds(0.5f);
			redCircle.get_gameObject().SetActive(true);
			TweenAlpha tweenAlpha2 = redCircle.GetComponent<TweenAlpha>();
			if (null != tweenAlpha2)
			{
				while (tweenAlpha2.get_isActiveAndEnabled())
				{
					yield return null;
				}
			}
		}
		ip.Set(0.8f, to, neighbor);
		yield return (object)new WaitForSeconds(0.5f);
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			parent.worldMapCamera.targetPos = ip.Get();
			yield return null;
		}
		if (null != portalGuideTxt)
		{
			yield return (object)new WaitForSeconds(0.5f);
			portalGuideTxt.get_gameObject().SetActive(true);
			TweenAlpha tweenAlpha = portalGuideTxt.GetComponent<TweenAlpha>();
			if (null != tweenAlpha)
			{
				while (tweenAlpha.get_isActiveAndEnabled())
				{
					yield return null;
				}
			}
		}
		if (null != icon)
		{
			yield return (object)new WaitForSeconds(0.5f);
			UITweenCtrl tweenCtrl = icon.GetComponent<UITweenCtrl>();
			if (null != tweenCtrl)
			{
				tweenCtrl.Play();
			}
		}
		Transform cursor = TutorialMessage.AttachCursor(button);
		if (null != cursor)
		{
			Vector3 localPosition = cursor.get_localPosition();
			cursor.set_localPosition(localPosition + new Vector3(0f, -10f, 0f));
		}
		parent.worldMapCamera.isInteractive = true;
		MonoBehaviourSingleton<WorldMapManager>.I.ignoreTutorial = false;
	}

	private FieldMapTable.PortalTableData[] GetPortalArray(int mapID)
	{
		List<FieldMapTable.PortalTableData> portalList = new List<FieldMapTable.PortalTableData>();
		FieldMapTable.FieldMapTableData[] fieldMapDataInRegion = Singleton<FieldMapTable>.I.GetFieldMapDataInRegion((uint)regionId);
		for (int i = 0; i < fieldMapDataInRegion.Length; i++)
		{
			if (fieldMapDataInRegion[i].jumpPortalID != 0)
			{
				Singleton<FieldMapTable>.I.GetPortalListByMapID(fieldMapDataInRegion[i].mapID)?.ForEach(delegate(FieldMapTable.PortalTableData o)
				{
					if (o.dstMapID == mapID)
					{
						portalList.Add(o);
					}
				});
			}
		}
		return portalList.ToArray();
	}

	private bool IsSameRegionPlayer()
	{
		uint currentMapID = MonoBehaviourSingleton<FieldManager>.I.currentMapID;
		if (currentMapID == 0)
		{
			return 0 == regionId;
		}
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(currentMapID);
		if (fieldMapData == null)
		{
			return false;
		}
		return fieldMapData.regionId == regionId;
	}

	private FieldMapTable.PortalTableData GetPortalData()
	{
		if (questTargetPortalIDs == null)
		{
			return null;
		}
		int num = questTargetPortalIDs.Length;
		if (0 >= num)
		{
			return null;
		}
		FieldMapTable.PortalTableData portalTableData = null;
		for (int i = 0; i < num; i++)
		{
			FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData((uint)questTargetPortalIDs[i]);
			if (portalData != null && (MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledMap((int)portalData.srcMapID) || MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledMap((int)portalData.dstMapID)))
			{
				portalTableData = portalData;
				break;
			}
		}
		if (portalTableData != null)
		{
			return portalTableData;
		}
		for (int j = 0; j < num; j++)
		{
			portalTableData = Singleton<FieldMapTable>.I.GetPortalData((uint)questTargetPortalIDs[j]);
			if (portalTableData != null)
			{
				break;
			}
		}
		return portalTableData;
	}

	private bool IsStepOver(FieldMapTable.PortalTableData portal)
	{
		if (portal == null)
		{
			return false;
		}
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(portal.srcMapID);
		FieldMapTable.FieldMapTableData fieldMapData2 = Singleton<FieldMapTable>.I.GetFieldMapData(portal.dstMapID);
		if (fieldMapData == null || fieldMapData2 == null)
		{
			return false;
		}
		return fieldMapData.regionId != fieldMapData2.regionId;
	}

	private Vector3 GetTargetPosition(FieldMapTable.FieldMapTableData map, FieldMapTable.PortalTableData portal, bool isStepOver)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		if (map == null && portal == null)
		{
			return Vector3.get_zero();
		}
		Vector3 result = Vector3.get_zero();
		if (portal == null)
		{
			RegionMapLocation regionMapLocation = regionMapRoot.FindLocation((int)map.mapID);
			if (null != regionMapLocation)
			{
				result = regionMapLocation.get_transform().get_position();
			}
		}
		else if (portal.mapX < 1E+10f)
		{
			RegionMapLocation regionMapLocation2 = regionMapRoot.FindLocation((int)portal.srcMapID);
			if (null != regionMapLocation2)
			{
				Vector3 val = default(Vector3);
				val._002Ector(portal.mapX, portal.mapY, 0f);
				result = regionMapLocation2.get_transform().get_position() + val;
			}
		}
		else if (!isStepOver)
		{
			RegionMapLocation regionMapLocation3 = regionMapRoot.FindLocation((int)portal.srcMapID);
			RegionMapLocation regionMapLocation4 = regionMapRoot.FindLocation((int)portal.dstMapID);
			if (null != regionMapLocation3 && null != regionMapLocation4)
			{
				result = Vector3.Lerp(regionMapLocation3.get_transform().get_position(), regionMapLocation4.get_transform().get_position(), 0.5f);
			}
		}
		else
		{
			RegionMapLocation regionMapLocation5 = regionMapRoot.FindLocation((int)portal.srcMapID);
			if (null != regionMapLocation5)
			{
				Vector3 val2 = default(Vector3);
				val2._002Ector(0f, -1f, 0f);
				val2 = Quaternion.Euler(0f, 0f, 0f - portal.dstDir) * val2;
				result = regionMapLocation5.get_transform().get_position();
				result += val2 * 2f;
			}
		}
		return result;
	}

	private RegionMapLocation GetPlayerLocation()
	{
		return regionMapRoot.FindLocation((int)MonoBehaviourSingleton<FieldManager>.I.currentMapID);
	}

	private int GetNeighborMapID()
	{
		int result = 0;
		if (questTargetPortalIDs == null || questTargetPortalIDs.Length == 0)
		{
			FieldMapTable.PortalTableData[] portalArray = GetPortalArray(questTargetMapID);
			if (portalArray == null || portalArray.Length == 0)
			{
				return 0;
			}
			for (int i = 0; i < portalArray.Length; i++)
			{
				if (MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledMap((int)portalArray[i].srcMapID))
				{
					result = (int)portalArray[i].srcMapID;
					break;
				}
			}
		}
		else
		{
			for (int j = 0; j < questTargetPortalIDs.Length; j++)
			{
				FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData((uint)questTargetPortalIDs[j]);
				if (portalData != null)
				{
					if (MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledMap((int)portalData.srcMapID))
					{
						result = (int)portalData.srcMapID;
						break;
					}
					if (MonoBehaviourSingleton<WorldMapManager>.I.IsTraveledMap((int)portalData.dstMapID))
					{
						result = (int)portalData.dstMapID;
						break;
					}
				}
			}
		}
		return result;
	}

	private void SetRedCirclePosition(FieldMapTable.FieldMapTableData map, FieldMapTable.PortalTableData portal, bool isStepOver)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		if (map == null && portal == null)
		{
			return;
		}
		if (portal == null)
		{
			SpotManager.Spot spot = spots.FindSpot((int)map.mapID);
			if (spot != null)
			{
				redCircleOrgPos = spot.originalPos;
				Transform val = spot._transform.Find("LBL_NAME");
				if (null != val)
				{
					val.get_transform().get_gameObject().SetActive(true);
				}
			}
		}
		else if (portal.mapX < 1E+10f)
		{
			SpotManager.Spot spot2 = spots.FindSpot((int)portal.srcMapID);
			if (spot2 != null)
			{
				Vector3 val2 = default(Vector3);
				val2._002Ector(portal.mapX, portal.mapY, 0f);
				redCircleOrgPos = spot2.originalPos + val2;
			}
		}
		else if (!isStepOver)
		{
			SpotManager.Spot spot3 = spots.FindSpot((int)portal.srcMapID);
			SpotManager.Spot spot4 = spots.FindSpot((int)portal.dstMapID);
			if (spot3 != null && spot4 != null)
			{
				redCircleOrgPos = Vector3.Lerp(spot3.originalPos, spot4.originalPos, 0.5f);
			}
		}
		else
		{
			SpotManager.Spot spot5 = spots.FindSpot((int)portal.srcMapID);
			if (spot5 != null)
			{
				Vector3 val3 = default(Vector3);
				val3._002Ector(0f, -1f, 0f);
				val3 = Quaternion.Euler(0f, 0f, 0f - portal.dstDir) * val3;
				redCircleOrgPos = spot5.originalPos + val3 * 2f;
			}
		}
	}

	private int[] GetLocationNumbers(string portalName)
	{
		string[] array = portalName.Replace("portal", string.Empty).Split('_');
		return new int[2]
		{
			int.Parse(array[0]),
			int.Parse(array[1])
		};
	}

	private void OnApplicationPause(bool paused)
	{
		isUpdateRenderTexture = !paused;
	}

	private void InitDeliveryTargetIdLists()
	{
		Delivery[] deliveryList = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList(do_sort: false);
		int i = 0;
		for (int num = deliveryList.Length; i < num; i++)
		{
			int dId = deliveryList[i].dId;
			if (MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery(dId))
			{
				continue;
			}
			DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)dId);
			List<uint> mapIdList = deliveryTableData.GetMapIdList();
			if (mapIdList != null)
			{
				int j = 0;
				for (int count = mapIdList.Count; j < count; j++)
				{
					DeliveryTargetMapIds.Add(mapIdList[j]);
				}
				continue;
			}
			List<uint> enemyIdList = deliveryTableData.GetEnemyIdList();
			if (enemyIdList != null)
			{
				int k = 0;
				for (int count2 = enemyIdList.Count; k < count2; k++)
				{
					DeliveryTargetEnemyIds.Add(enemyIdList[k]);
				}
			}
		}
	}

	private void OnQuery_SELECT_DIFFICULTY()
	{
		GameSection.SetEventData(new object[2]
		{
			true,
			isOpenedHard
		});
		if (isInGame)
		{
			GameSection.ChangeEvent("INGAME_SELECT_DIFFICULTY");
		}
	}

	private void OnCloseDialog_WorldMapSelectDifficultyDialog()
	{
		object eventData = GameSection.GetEventData();
		if (eventData == null)
		{
			return;
		}
		REGION_DIFFICULTY_TYPE rEGION_DIFFICULTY_TYPE = (REGION_DIFFICULTY_TYPE)eventData;
		RegionTable.Data data = Singleton<RegionTable>.I.GetData((uint)regionId);
		if (rEGION_DIFFICULTY_TYPE != data.difficulty)
		{
			RegionTable.Data data2 = Singleton<RegionTable>.I.GetData(data.groupId, rEGION_DIFFICULTY_TYPE);
			if (data2 != null)
			{
				EventData[] autoEvents = new EventData[2]
				{
					new EventData("WORLDMAP"),
					new EventData("OPEN_REGION_CHANGE", (int)data2.regionId)
				};
				MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
			}
		}
	}

	private void OnCloseDialog_InGameSelectDifficultyDialog()
	{
		object eventData = GameSection.GetEventData();
		if (eventData == null)
		{
			return;
		}
		REGION_DIFFICULTY_TYPE rEGION_DIFFICULTY_TYPE = (REGION_DIFFICULTY_TYPE)eventData;
		RegionTable.Data data = Singleton<RegionTable>.I.GetData((uint)regionId);
		if (rEGION_DIFFICULTY_TYPE != data.difficulty)
		{
			RegionTable.Data data2 = Singleton<RegionTable>.I.GetData(data.groupId, rEGION_DIFFICULTY_TYPE);
			if (data2 != null)
			{
				EventData[] autoEvents = new EventData[2]
				{
					new EventData("WORLDMAP"),
					new EventData("OPEN_REGION_CHANGE", (int)data2.regionId)
				};
				MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
			}
		}
	}
}
