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
		StartCoroutine("DoInitialize");
	}

	private IEnumerator DoInitialize()
	{
		int num = (int)GameSection.GetEventData();
		FieldMapTable.FieldMapTableData fieldMapTableData = null;
		switch (num)
		{
		case -4:
			regionId = MonoBehaviourSingleton<WorldMapManager>.I.eventMapRegionID;
			break;
		case -1:
			directOpen = true;
			fieldMapTableData = Singleton<FieldMapTable>.I.GetFieldMapData(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
			break;
		case -3:
		case -2:
			directOpen = true;
			isShowDescription = false;
			isTutorial = (-3 == num);
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
					fieldMapTableData = Singleton<FieldMapTable>.I.GetFieldMapData(portalData.srcMapID);
				}
			}
			else
			{
				fieldMapTableData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)questTargetMapID);
			}
			isEventMap = FieldManager.IsEventMap(fieldMapTableData.mapID);
			break;
		default:
			regionId = num;
			break;
		}
		if (fieldMapTableData != null)
		{
			regionId = (int)fieldMapTableData.regionId;
		}
		isEventMap = FieldManager.IsEventRegion((uint)regionId);
		if (MonoBehaviourSingleton<WorldMapManager>.I.NeedDirectionOpenRegion(regionId))
		{
			base.Initialize();
			yield break;
		}
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject loadedLocationSpotRoot = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "LocationSpotRoot");
		LoadObject loadedLocationSpot = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "LocationSpot");
		LoadObject loadedPlayerMarker = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "PlayerMarker");
		LoadObject loadObj = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionMap_" + regionId.ToString("D3"));
		LoadObject loadedEffect = loadingQueue.LoadEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_bg_questmap_01");
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		parent = (MonoBehaviourSingleton<GameSceneManager>.I.FindSection("WorldMap") as WorldMap);
		Camera camera = parent.worldMapCamera._camera;
		spots = new SpotManager(loadedLocationSpotRoot.loadedObject as GameObject, loadedLocationSpot.loadedObject as GameObject, camera);
		spots.CreateSpotRoot();
		redCircle = spots.spotRootTransform.Find("RedCircle");
		portalGuideTxt = spots.spotRootTransform.Find("PortalGuideTxt");
		UIPanel component = spots.spotRootTransform.GetComponent<UIPanel>();
		if (component != null)
		{
			component.depth = base.baseDepth + 1;
		}
		regionMapRoot = ResourceUtility.Realizes(loadObj.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform).gameObject.GetComponent<RegionMapRoot>();
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
		rootTransform.gameObject.SetActive(value: true);
		rootPanel = rootTransform.GetComponent<UIPanel>();
		if (rootPanel != null)
		{
			defaultDepth = rootPanel.depth;
		}
		uiMapSprite = rootTransform.Find("Map").gameObject.GetComponent<UITexture>();
		uiParentMapSprite = rootTransform.Find("ParentMap").gameObject.GetComponent<UITexture>();
		InitMapSprite(isPortrait: false);
		if (!directOpen)
		{
			StartShowingTween();
		}
		windEffect = ResourceUtility.Realizes(loadedEffect.loadedObject, parent.worldMapCamera.transform).gameObject.GetComponent<rymFX>();
		windEffect.Cameras = new Camera[1]
		{
			parent.worldMapCamera._camera
		};
		windEffect.gameObject.layer = LayerMask.NameToLayer("WorldMap");
		spots.spotRootTransform.GetComponent<UIPanel>().RebuildAllDrawCalls();
		tweenAnimations = spots.spotRootTransform.GetComponentsInChildren<UITweenCtrl>();
		playerMarker = ResourceUtility.Realizes(loadedPlayerMarker.loadedObject);
		PlayerMarker component2 = playerMarker.GetComponent<PlayerMarker>();
		if (null != component2)
		{
			component2.SetCamera(parent.worldMapCamera._camera.transform);
		}
		SetFirstCameraPos();
		base.collectUI = base._transform;
		Transform transform = base._transform.Find("LocationSpotRoot/CLOSE_BTN");
		if (transform != null)
		{
			closeButton = transform.GetComponent<UIWidget>();
		}
		Transform transform2 = base._transform.Find("LocationSpotRoot/AnnounceTap");
		if (transform2 != null)
		{
			announceTap = transform2.gameObject;
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
				StartCoroutine(FadeWidget(component, 0, 1, 0.3f));
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
			time += Time.deltaTime;
			float t = time / duration;
			float num2 = target.alpha = Mathf.Lerp(start, end, t);
			yield return null;
		}
	}

	private void StartShowingTween()
	{
		if (uiMapSprite != null)
		{
			uiMapSprite.alpha = 0f;
			TweenAlpha.Begin(uiMapSprite.gameObject, 0.3f, 1f);
			uiMapSprite.transform.localScale = Vector3.one * 0.8f;
			TweenScale.Begin(uiMapSprite.gameObject, 0.3f, Vector3.one);
		}
		if (uiParentMapSprite != null)
		{
			uiParentMapSprite.alpha = 1f;
			TweenScale.Begin(uiParentMapSprite.gameObject, 0.3f, Vector3.one * 1.2f);
			TweenAlpha.Begin(uiParentMapSprite.gameObject, 0.3f, 0f);
		}
	}

	private void InitMapSprite(bool isPortrait)
	{
		if (uiMapSprite != null)
		{
			if (null == parent.worldMapCamera._camera.targetTexture)
			{
				parent.worldMapCamera.Restore();
			}
			uiMapSprite.mainTexture = parent.worldMapCamera._camera.targetTexture;
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
			if (regionMapLocation.portal.Length != 0)
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
					int[] locationNumbers = GetLocationNumbers(regionMapPortal.name);
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
					if (clearStatusQuestData.missionStatus[i] <= 2)
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
		if (location.mapId == 0)
		{
			spots.AddSpot(0, MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionTextList().Find((GameSceneTables.TextData textData) => textData.key == "STR_HOME").text, location.transform.position, SpotManager.ICON_TYPE.HOME, "HOME", isNew: false, canUnlockNewPortal: false, viewEnemyPopBallon: false, 0);
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
			SpotManager.Spot spot = spots.AddSpot((int)spotEventData.mapId, fieldMapData.mapName, location.transform.position, iconStatus, event_name, isNew: false, attach_new_release_portal, viewEnemyPopBallon, spotEventData, location.icon, isExistDelivery, happen);
			if (SpotManager.ICON_TYPE.INVISIBLE == iconStatus)
			{
				spot._transform.gameObject.SetActive(value: false);
			}
			if (iconStatus != SpotManager.ICON_TYPE.NOT_OPENED && iconStatus != SpotManager.ICON_TYPE.INVISIBLE)
			{
				spotMapIdDic[spot] = fieldMapData.mapID;
			}
		}
	}

	private void SetFirstCameraPos()
	{
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
		playerMarker.gameObject.SetActive(value: true);
		if (fieldMapData == null)
		{
			parent.worldMapCamera.targetPos = regionMapRoot.locations[0].transform.position;
			if (regionId == 0)
			{
				playerMarker.SetParent(regionMapRoot.locations[0].transform);
			}
			else
			{
				playerMarker.gameObject.SetActive(value: false);
				playerMarker.SetParent(base.transform);
			}
		}
		else if (!FieldManager.HasWorldMap(MonoBehaviourSingleton<FieldManager>.I.currentMapID) || fieldMapData.regionId != (uint)regionId)
		{
			parent.worldMapCamera.targetPos = regionMapRoot.locations[0].transform.position;
			playerMarker.gameObject.SetActive(value: false);
			playerMarker.SetParent(base.transform);
		}
		else
		{
			RegionMapLocation regionMapLocation = regionMapRoot.FindLocation((int)fieldMapData.mapID);
			if (regionMapLocation != null)
			{
				parent.worldMapCamera.targetPos = regionMapLocation.transform.position;
				playerMarker.SetParent(regionMapLocation.transform);
			}
		}
		playerMarker.localPosition = MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerOffset;
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
			Transform transform = base.transform.Find("LocationSpotRoot/BACK_REGION_BTN");
			if (transform != null)
			{
				UIWidget component = transform.GetComponent<UIWidget>();
				if (component != null)
				{
					component.alpha = (data.hasParentRegion() ? 1f : 0f);
				}
			}
			transform = base.transform.Find("LocationSpotRoot/BACK_BTN");
			if (transform != null)
			{
				UIWidget component2 = transform.GetComponent<UIWidget>();
				if (component2 != null)
				{
					component2.alpha = (data.hasParentRegion() ? 0f : 1f);
				}
			}
		}
	}

	private void HideButtons()
	{
		Transform[] array = new Transform[2]
		{
			base._transform.Find("LocationSpotRoot/BACK_REGION_BTN"),
			base._transform.Find("LocationSpotRoot/BACK_BTN")
		};
		foreach (Transform transform in array)
		{
			if (!(null == transform))
			{
				UIWidget component = transform.GetComponent<UIWidget>();
				if (null != component)
				{
					component.alpha = 0f;
				}
			}
		}
	}

	protected override void OnCloseStart()
	{
		StopAllCoroutines();
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
			EffectManager.ReleaseEffect(windEffect.gameObject);
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
			announceTap.SetActive(value: false);
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
			announceTap.SetActive(value: true);
		}
		parent.worldMapCamera.gameObject.SetActive(value: true);
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
		uint mapId = ((SpotEventData)GameSection.GetEventData()).mapId;
		if (mapId == MonoBehaviourSingleton<FieldManager>.I.currentMapID)
		{
			GameSection.StopEvent();
			if (UIInGameFieldMenu.IsValid())
			{
				UIInGameFieldMenu.I.OnClickPopMenu();
			}
			Transform transform = Utility.Find(base._transform, "CLOSE_BACK");
			if (null != transform)
			{
				UIButton component = transform.GetComponent<UIButton>();
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
			spots.spotRootTransform.gameObject.SetActive(value: false);
			MonoBehaviourSingleton<UIManager>.I.system.GetCtrl(UIManager.SYSTEM.DIALOG_BLOCKER).gameObject.SetActive(value: false);
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
		StartCoroutine(DoChangeRegion(withBlur: true));
	}

	private void OnQuery_BACK_REGION()
	{
		RegionTable.Data data = Singleton<RegionTable>.I.GetData((uint)regionId);
		if (data != null)
		{
			if (redCircle != null)
			{
				redCircle.gameObject.SetActive(value: false);
			}
			displayQuestTargetMode = false;
			regionId = (int)data.parentRegionId;
			StartCoroutine(DoChangeRegion(withBlur: false));
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
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject loadObj = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionMap_" + regionId.ToString("D3"));
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		while (wait)
		{
			yield return null;
		}
		UnityEngine.Object.Destroy(regionMapRoot.gameObject);
		regionMapRoot = ResourceUtility.Realizes(loadObj.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform).gameObject.GetComponent<RegionMapRoot>();
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
			Vector2 blurCenter = new Vector2(0.5f, 0.5f);
			parent.blurFilter.StartBlurFilter(0.01f, 0.25f, duration, blurCenter, delegate
			{
				wait = false;
			});
			uiMapSprite.alpha = 0f;
			TweenAlpha.Begin(uiMapSprite.gameObject, duration, 1f);
			TweenAlpha.Begin(uiParentMapSprite.gameObject, duration, 0f);
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
		if ((int)GameSection.GetEventData() == MonoBehaviourSingleton<FieldManager>.I.currentMapID)
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
		if (spots != null)
		{
			spots.Update();
		}
		if (null != redCircle && redCircle.gameObject.activeSelf)
		{
			Camera camera = parent.worldMapCamera._camera;
			if (null != camera)
			{
				Vector3 position = camera.WorldToScreenPoint(redCircleOrgPos);
				position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(position);
				position.z = 0f;
				redCircle.position = position;
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
		StartCoroutine(DoFadeMap(1f, 0f, 0.3f, delegate
		{
			if (regionMapRoot != null)
			{
				UnityEngine.Object.Destroy(regionMapRoot.gameObject);
				regionMapRoot = null;
			}
		}));
	}

	public void FadeInMap()
	{
		StartCoroutine(DoFadeMap(0f, 1f, 0.5f, delegate
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
			for (float timer = 0f; timer < time; timer += Time.deltaTime)
			{
				float value = Mathf.Lerp(from, to, timer / time);
				r.material.SetFloat("_Alpha", value);
				yield return null;
			}
			r.material.SetFloat("_Alpha", to);
			onComplete?.Invoke();
		}
	}

	protected override void OnDestroy()
	{
		if (regionMapRoot != null)
		{
			UnityEngine.Object.Destroy(regionMapRoot.gameObject);
			regionMapRoot = null;
		}
		base.OnDestroy();
	}

	private void DisplayQuestTarget()
	{
		FieldMapTable.PortalTableData portalData = GetPortalData();
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)questTargetMapID);
		bool isStepOver = IsStepOver(portalData);
		bool num = IsSameRegionPlayer();
		Vector3 targetPosition = GetTargetPosition(fieldMapData, portalData, isStepOver);
		Vector3 vector = targetPosition;
		if (num)
		{
			RegionMapLocation playerLocation = GetPlayerLocation();
			if (null != playerLocation)
			{
				vector = playerLocation.transform.position;
			}
		}
		SetRedCirclePosition(fieldMapData, portalData, isStepOver);
		parent.worldMapCamera.targetPos = vector;
		StartCoroutine(DoDisplayQuestTarget(vector, targetPosition));
	}

	private IEnumerator DoDisplayQuestTarget(Vector3 from, Vector3 to)
	{
		yield return new WaitForSeconds(0.8f);
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
			yield return new WaitForSeconds(0.5f);
			redCircle.gameObject.SetActive(value: true);
			TweenAlpha tweenAlpha = redCircle.GetComponent<TweenAlpha>();
			if (null != tweenAlpha)
			{
				while (tweenAlpha.isActiveAndEnabled)
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
		FieldMapTable.PortalTableData portalData = GetPortalData();
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)questTargetMapID);
		bool isStepOver = IsStepOver(portalData);
		Vector3 targetPosition = GetTargetPosition(fieldMapData, portalData, isStepOver);
		Vector3 vector = targetPosition;
		RegionMapLocation playerLocation = GetPlayerLocation();
		if (null != playerLocation)
		{
			vector = playerLocation.transform.position;
		}
		Vector3 neighbor = Vector3.zero;
		int neighborMapID = GetNeighborMapID();
		RegionMapLocation regionMapLocation = regionMapRoot.FindLocation(neighborMapID);
		if (null != regionMapLocation)
		{
			neighbor = regionMapLocation.transform.position;
		}
		SetRedCirclePosition(fieldMapData, portalData, isStepOver);
		Transform icon = null;
		Transform button = null;
		SpotManager.Spot spot = spots.FindSpot(neighborMapID);
		if (null != portalGuideTxt && spot != null)
		{
			portalGuideTxt.SetParent(spot._transform);
			portalGuideTxt.localPosition = new Vector3(0f, -60f, 0f);
			icon = ((spot.type != 0) ? spot._transform.Find("SPR_ICON_CLEARED") : spot._transform.Find("SPR_ICON_NEW"));
			button = spot._transform.Find("SPR_BUTTON");
		}
		parent.worldMapCamera.targetPos = vector;
		StartCoroutine(DoDisplayQuestTargetTutorial(vector, targetPosition, neighbor, icon, button));
	}

	private IEnumerator DoDisplayQuestTargetTutorial(Vector3 from, Vector3 to, Vector3 neighbor, Transform icon, Transform button)
	{
		Vector3Interpolator ip = new Vector3Interpolator();
		if (0.1f < Vector3.Distance(from, to))
		{
			ip.Set(1f, from, to);
		}
		else
		{
			ip.Set(0f, from, to);
		}
		yield return new WaitForSeconds(0.8f);
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			parent.worldMapCamera.targetPos = ip.Get();
			yield return null;
		}
		if (null != redCircle)
		{
			yield return new WaitForSeconds(0.5f);
			redCircle.gameObject.SetActive(value: true);
			TweenAlpha tweenAlpha2 = redCircle.GetComponent<TweenAlpha>();
			if (null != tweenAlpha2)
			{
				while (tweenAlpha2.isActiveAndEnabled)
				{
					yield return null;
				}
			}
		}
		ip.Set(0.8f, to, neighbor);
		yield return new WaitForSeconds(0.5f);
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			parent.worldMapCamera.targetPos = ip.Get();
			yield return null;
		}
		if (null != portalGuideTxt)
		{
			yield return new WaitForSeconds(0.5f);
			portalGuideTxt.gameObject.SetActive(value: true);
			TweenAlpha tweenAlpha2 = portalGuideTxt.GetComponent<TweenAlpha>();
			if (null != tweenAlpha2)
			{
				while (tweenAlpha2.isActiveAndEnabled)
				{
					yield return null;
				}
			}
		}
		if (null != icon)
		{
			yield return new WaitForSeconds(0.5f);
			UITweenCtrl component = icon.GetComponent<UITweenCtrl>();
			if (null != component)
			{
				component.Play();
			}
		}
		Transform transform = TutorialMessage.AttachCursor(button);
		if (null != transform)
		{
			Vector3 localPosition = transform.localPosition;
			transform.localPosition = localPosition + new Vector3(0f, -10f, 0f);
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
			return regionId == 0;
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
		if (map == null && portal == null)
		{
			return Vector3.zero;
		}
		Vector3 result = Vector3.zero;
		if (portal == null)
		{
			RegionMapLocation regionMapLocation = regionMapRoot.FindLocation((int)map.mapID);
			if (null != regionMapLocation)
			{
				result = regionMapLocation.transform.position;
			}
		}
		else if (portal.mapX < 1E+10f)
		{
			RegionMapLocation regionMapLocation2 = regionMapRoot.FindLocation((int)portal.srcMapID);
			if (null != regionMapLocation2)
			{
				Vector3 b = new Vector3(portal.mapX, portal.mapY, 0f);
				result = regionMapLocation2.transform.position + b;
			}
		}
		else if (!isStepOver)
		{
			RegionMapLocation regionMapLocation3 = regionMapRoot.FindLocation((int)portal.srcMapID);
			RegionMapLocation regionMapLocation4 = regionMapRoot.FindLocation((int)portal.dstMapID);
			if (null != regionMapLocation3 && null != regionMapLocation4)
			{
				result = Vector3.Lerp(regionMapLocation3.transform.position, regionMapLocation4.transform.position, 0.5f);
			}
		}
		else
		{
			RegionMapLocation regionMapLocation5 = regionMapRoot.FindLocation((int)portal.srcMapID);
			if (null != regionMapLocation5)
			{
				Vector3 point = new Vector3(0f, -1f, 0f);
				point = Quaternion.Euler(0f, 0f, 0f - portal.dstDir) * point;
				result = regionMapLocation5.transform.position;
				result += point * 2f;
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
				Transform transform = spot._transform.Find("LBL_NAME");
				if (null != transform)
				{
					transform.transform.gameObject.SetActive(value: true);
				}
			}
		}
		else if (portal.mapX < 1E+10f)
		{
			SpotManager.Spot spot2 = spots.FindSpot((int)portal.srcMapID);
			if (spot2 != null)
			{
				Vector3 b = new Vector3(portal.mapX, portal.mapY, 0f);
				redCircleOrgPos = spot2.originalPos + b;
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
				Vector3 point = new Vector3(0f, -1f, 0f);
				point = Quaternion.Euler(0f, 0f, 0f - portal.dstDir) * point;
				redCircleOrgPos = spot5.originalPos + point * 2f;
			}
		}
	}

	private int[] GetLocationNumbers(string portalName)
	{
		string[] array = portalName.Replace("portal", "").Split('_');
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
