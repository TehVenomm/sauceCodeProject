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
		BTN_CURRENT_DIFFICULTY_HARD
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

	public const int NON_EVENT_OPEN_EVENT = -1;

	public const int QUEST_TARGET_DISPLAY_EVENT = -2;

	public const int TUTORIAL_EVENT = -3;

	public const int FROM_EVENT_QUEST_EVENT = -4;

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
				FieldMapTable.PortalTableData portal = Singleton<FieldMapTable>.I.GetPortalData((uint)questTargetPortalIDs[0]);
				if (portal != null)
				{
					mapData = Singleton<FieldMapTable>.I.GetFieldMapData(portal.srcMapID);
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
		}
		else
		{
			LoadingQueue loadQueue = new LoadingQueue(this);
			LoadObject loadedLocationSpotRoot = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "LocationSpotRoot", false);
			LoadObject loadedLocationSpot = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "LocationSpot", false);
			LoadObject loadedPlayerMarker = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "PlayerMarker", false);
			LoadObject loadObj = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionMap_" + regionId.ToString("D3"), false);
			LoadObject loadedEffect = loadQueue.LoadEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_bg_questmap_01", false);
			if (loadQueue.IsLoading())
			{
				yield return (object)loadQueue.Wait();
			}
			parent = (MonoBehaviourSingleton<GameSceneManager>.I.FindSection("WorldMap") as WorldMap);
			spots = new SpotManager(_targetCamera: parent.worldMapCamera._camera, _spotRootPrefab: loadedLocationSpotRoot.loadedObject as GameObject, _spotPrefab: loadedLocationSpot.loadedObject as GameObject);
			spots.CreateSpotRoot();
			redCircle = spots.spotRootTransform.Find("RedCircle");
			portalGuideTxt = spots.spotRootTransform.Find("PortalGuideTxt");
			UIPanel spotPanel = spots.spotRootTransform.GetComponent<UIPanel>();
			if ((UnityEngine.Object)spotPanel != (UnityEngine.Object)null)
			{
				spotPanel.depth = base.baseDepth + 1;
			}
			regionMapRoot = ResourceUtility.Realizes(loadObj.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform, -1).gameObject.GetComponent<RegionMapRoot>();
			if ((UnityEngine.Object)regionMapRoot != (UnityEngine.Object)null)
			{
				bool wait = true;
				regionMapRoot.InitPortalStatus(delegate
				{
					((_003CDoInitialize_003Ec__Iterator17E)/*Error near IL_0470: stateMachine*/)._003Cwait_003E__11 = false;
				});
				while (wait)
				{
					yield return (object)null;
				}
			}
			rootTransform = spots.SetRoot(base._transform);
			rootTransform.gameObject.SetActive(true);
			rootPanel = rootTransform.GetComponent<UIPanel>();
			if ((UnityEngine.Object)rootPanel != (UnityEngine.Object)null)
			{
				defaultDepth = rootPanel.depth;
			}
			uiMapSprite = rootTransform.FindChild("Map").gameObject.GetComponent<UITexture>();
			uiParentMapSprite = rootTransform.FindChild("ParentMap").gameObject.GetComponent<UITexture>();
			InitMapSprite(false);
			if (!directOpen)
			{
				StartShowingTween();
			}
			windEffect = ResourceUtility.Realizes(loadedEffect.loadedObject, parent.worldMapCamera.transform, -1).gameObject.GetComponent<rymFX>();
			windEffect.Cameras = new Camera[1]
			{
				parent.worldMapCamera._camera
			};
			windEffect.gameObject.layer = LayerMask.NameToLayer("WorldMap");
			spots.spotRootTransform.GetComponent<UIPanel>().RebuildAllDrawCalls();
			tweenAnimations = spots.spotRootTransform.GetComponentsInChildren<UITweenCtrl>();
			playerMarker = ResourceUtility.Realizes(loadedPlayerMarker.loadedObject, -1);
			PlayerMarker playerMarkerCom = playerMarker.GetComponent<PlayerMarker>();
			if ((UnityEngine.Object)null != (UnityEngine.Object)playerMarkerCom)
			{
				playerMarkerCom.SetCamera(parent.worldMapCamera._camera.transform);
			}
			SetFirstCameraPos();
			base.collectUI = base._transform;
			Transform closeButtonTransform = base._transform.FindChild("LocationSpotRoot/CLOSE_BTN");
			if ((UnityEngine.Object)closeButtonTransform != (UnityEngine.Object)null)
			{
				closeButton = closeButtonTransform.GetComponent<UIWidget>();
			}
			Transform announceTapTransform = base._transform.FindChild("LocationSpotRoot/AnnounceTap");
			if ((UnityEngine.Object)announceTapTransform != (UnityEngine.Object)null)
			{
				announceTap = announceTapTransform.gameObject;
			}
			if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
			{
				MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += InitMapSprite;
			}
			UpdateDifficultyButton(false);
			InitDeliveryTargetIdLists();
			base.Initialize();
		}
	}

	private void UpdateDifficultyButton(bool forceOff = false)
	{
		Transform transform = null;
		Transform spotRootTransform = spots.spotRootTransform;
		if (forceOff)
		{
			SetActive(spotRootTransform, UI.OBJ_SELECT_DIFFICULTY, false);
		}
		else
		{
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
				if (!((UnityEngine.Object)component == (UnityEngine.Object)null))
				{
					StartCoroutine(FadeWidget(component, 0, 1, 0.3f));
				}
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
			float prog = time / duration;
			float a = target.alpha = Mathf.Lerp((float)start, (float)end, prog);
			yield return (object)null;
		}
	}

	private void StartShowingTween()
	{
		if ((UnityEngine.Object)uiMapSprite != (UnityEngine.Object)null)
		{
			uiMapSprite.alpha = 0f;
			TweenAlpha.Begin(uiMapSprite.gameObject, 0.3f, 1f);
			uiMapSprite.transform.localScale = Vector3.one * 0.8f;
			TweenScale.Begin(uiMapSprite.gameObject, 0.3f, Vector3.one);
		}
		if ((UnityEngine.Object)uiParentMapSprite != (UnityEngine.Object)null)
		{
			uiParentMapSprite.alpha = 1f;
			TweenScale.Begin(uiParentMapSprite.gameObject, 0.3f, Vector3.one * 1.2f);
			TweenAlpha.Begin(uiParentMapSprite.gameObject, 0.3f, 0f);
		}
	}

	private void InitMapSprite(bool isPortrait)
	{
		if ((UnityEngine.Object)uiMapSprite != (UnityEngine.Object)null)
		{
			if ((UnityEngine.Object)null == (UnityEngine.Object)parent.worldMapCamera._camera.targetTexture)
			{
				parent.worldMapCamera.Restore();
			}
			uiMapSprite.mainTexture = parent.worldMapCamera._camera.targetTexture;
			uiMapSprite.width = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth;
			uiMapSprite.height = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight;
		}
		if ((UnityEngine.Object)uiParentMapSprite != (UnityEngine.Object)null)
		{
			if ((UnityEngine.Object)null == (UnityEngine.Object)parent.blurFilter.filteredTexture)
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
		if (spots.Count < 1)
		{
			OutGameSettingsManager.QuestMap questMap = MonoBehaviourSingleton<OutGameSettingsManager>.I.questMap;
			MonoBehaviourSingleton<FilterManager>.I.StopBlur(questMap.cameraMoveTime, 0f);
			spotMapIdDic = new Dictionary<SpotManager.Spot, uint>();
			if (!((UnityEngine.Object)regionMapRoot == (UnityEngine.Object)null))
			{
				for (int i = 0; i < regionMapRoot.locations.Length; i++)
				{
					RegionMapLocation regionMapLocation = regionMapRoot.locations[i];
					FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)regionMapLocation.mapId);
					if (fieldMapData != null || regionMapLocation.mapId == 0)
					{
						if (fieldMapData != null)
						{
							if (!FieldManager.IsShowPortal(fieldMapData.jumpPortalID))
							{
								CreateLocationSpot(regionMapLocation, SpotManager.ICON_TYPE.INVISIBLE, false, false, SpotManager.HAPPEN_CONDITION.NONE);
								continue;
							}
							if (!MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(fieldMapData))
							{
								CreateLocationSpot(regionMapLocation, SpotManager.ICON_TYPE.NOT_OPENED, false, false, SpotManager.HAPPEN_CONDITION.NONE);
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
								if (!flag)
								{
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
		if (location.mapId == 0)
		{
			spots.AddSpot(0, MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionTextList().Find((GameSceneTables.TextData textData) => textData.key == "STR_HOME").text, location.transform.position, SpotManager.ICON_TYPE.HOME, "HOME", false, false, false, 0, null, false, SpotManager.HAPPEN_CONDITION.NONE, 0);
		}
		else
		{
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
				SpotManager.Spot spot = spots.AddSpot((int)spotEventData.mapId, fieldMapData.mapName, location.transform.position, iconStatus, event_name, false, attach_new_release_portal, viewEnemyPopBallon, spotEventData, location.icon, isExistDelivery, happen, 0);
				if (iconStatus == SpotManager.ICON_TYPE.INVISIBLE)
				{
					spot._transform.gameObject.SetActive(false);
				}
				if (iconStatus != SpotManager.ICON_TYPE.NOT_OPENED && iconStatus != SpotManager.ICON_TYPE.INVISIBLE)
				{
					spotMapIdDic[spot] = fieldMapData.mapID;
				}
			}
		}
	}

	private void SetFirstCameraPos()
	{
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
		playerMarker.gameObject.SetActive(true);
		if (fieldMapData == null)
		{
			parent.worldMapCamera.targetPos = regionMapRoot.locations[0].transform.position;
			if (regionId == 0)
			{
				playerMarker.SetParent(regionMapRoot.locations[0].transform);
			}
			else
			{
				playerMarker.gameObject.SetActive(false);
				playerMarker.SetParent(base.transform);
			}
		}
		else if (!FieldManager.HasWorldMap(MonoBehaviourSingleton<FieldManager>.I.currentMapID) || fieldMapData.regionId != (uint)regionId)
		{
			parent.worldMapCamera.targetPos = regionMapRoot.locations[0].transform.position;
			playerMarker.gameObject.SetActive(false);
			playerMarker.SetParent(base.transform);
		}
		else
		{
			RegionMapLocation regionMapLocation = regionMapRoot.FindLocation((int)fieldMapData.mapID);
			if ((UnityEngine.Object)regionMapLocation != (UnityEngine.Object)null)
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
		if ((UnityEngine.Object)uITweenCtrl != (UnityEngine.Object)null)
		{
			uITweenCtrl.Reset();
			uITweenCtrl.Play(true, onComplete);
		}
	}

	protected override void OnOpen()
	{
		if (MonoBehaviourSingleton<WorldMapManager>.I.NeedDirectionOpenRegion(regionId))
		{
			isOpened = true;
			base.OnOpen();
		}
		else
		{
			if ((UnityEngine.Object)parent != (UnityEngine.Object)null && !isOpened)
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
			PlayTween(TWEEN_ANIMATION.OPENING, null);
			isOpened = true;
			base.OnOpen();
		}
	}

	public override void StartSection()
	{
		if (MonoBehaviourSingleton<WorldMapManager>.I.NeedDirectionOpenRegion(regionId))
		{
			MonoBehaviourSingleton<WorldMapManager>.I.transferInfo = new WorldMapManager.TransferInfo(regionId, false);
			DispatchEvent("WORLDMAP", null);
		}
	}

	private void UpdateDeliveryTargetMarkers()
	{
		if (spotMapIdDic != null)
		{
			InitDeliveryTargetIdLists();
			List<SpotManager.Spot> allSpots = spots.GetAllSpots();
			if (allSpots != null)
			{
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
		}
	}

	private void SetButtonsAlpha()
	{
		RegionTable.Data data = Singleton<RegionTable>.I.GetData((uint)regionId);
		if (isEventMap)
		{
			HideButtons();
		}
		else if (data != null)
		{
			Transform transform = base.transform.FindChild("LocationSpotRoot/BACK_REGION_BTN");
			if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
			{
				UIWidget component = transform.GetComponent<UIWidget>();
				if ((UnityEngine.Object)component != (UnityEngine.Object)null)
				{
					component.alpha = ((!data.hasParentRegion()) ? 0f : 1f);
				}
			}
			transform = base.transform.FindChild("LocationSpotRoot/BACK_BTN");
			if ((UnityEngine.Object)transform != (UnityEngine.Object)null)
			{
				UIWidget component2 = transform.GetComponent<UIWidget>();
				if ((UnityEngine.Object)component2 != (UnityEngine.Object)null)
				{
					component2.alpha = ((!data.hasParentRegion()) ? 1f : 0f);
				}
			}
		}
	}

	private void HideButtons()
	{
		Transform[] array = new Transform[2]
		{
			base._transform.FindChild("LocationSpotRoot/BACK_REGION_BTN"),
			base._transform.FindChild("LocationSpotRoot/BACK_BTN")
		};
		foreach (Transform transform in array)
		{
			if (!((UnityEngine.Object)null == (UnityEngine.Object)transform))
			{
				UIWidget component = transform.GetComponent<UIWidget>();
				if ((UnityEngine.Object)null != (UnityEngine.Object)component)
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
		if (!isToDescription && (UnityEngine.Object)parent != (UnityEngine.Object)null)
		{
			parent.worldMapCamera.isInteractive = true;
		}
		MonoBehaviourSingleton<WorldMapManager>.I.ignoreTutorial = false;
		isToDescription = false;
		base.OnCloseStart();
	}

	public override void Exit()
	{
		if ((UnityEngine.Object)windEffect != (UnityEngine.Object)null)
		{
			EffectManager.ReleaseEffect(windEffect.gameObject, true, false);
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
		if ((UnityEngine.Object)parent != (UnityEngine.Object)null)
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
			GameSection.ChangeEvent("TO_DESCRIPTION", null);
		}
		else
		{
			MoveIngameMapOrHome();
		}
	}

	private void HideInTheBack()
	{
		if ((UnityEngine.Object)rootPanel != (UnityEngine.Object)null)
		{
			rootPanel.depth = 4000;
		}
		HideButtons();
		if ((UnityEngine.Object)closeButton != (UnityEngine.Object)null)
		{
			closeButton.alpha = 0f;
		}
		if ((UnityEngine.Object)announceTap != (UnityEngine.Object)null)
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
		if ((UnityEngine.Object)rootPanel != (UnityEngine.Object)null)
		{
			rootPanel.depth = defaultDepth;
		}
		SetButtonsAlpha();
		if ((UnityEngine.Object)closeButton != (UnityEngine.Object)null)
		{
			closeButton.alpha = 1f;
		}
		if ((UnityEngine.Object)announceTap != (UnityEngine.Object)null)
		{
			announceTap.SetActive(true);
		}
		parent.worldMapCamera.gameObject.SetActive(true);
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
		}
		else
		{
			SpotEventData spotEventData = (SpotEventData)GameSection.GetEventData();
			uint mapId = spotEventData.mapId;
			if (mapId == MonoBehaviourSingleton<FieldManager>.I.currentMapID)
			{
				GameSection.StopEvent();
				if (UIInGameFieldMenu.IsValid())
				{
					UIInGameFieldMenu.I.OnClickPopMenu();
				}
				Transform transform = Utility.Find(base._transform, "CLOSE_BACK");
				if ((UnityEngine.Object)null != (UnityEngine.Object)transform)
				{
					UIButton component = transform.GetComponent<UIButton>();
					if ((UnityEngine.Object)null != (UnityEngine.Object)component)
					{
						component.onClick.ForEach(delegate(EventDelegate o)
						{
							o.Execute();
						});
					}
					DispatchEvent("CLOSE", null);
				}
			}
			else if (mapId == 0)
			{
				if (MonoBehaviourSingleton<InGameProgress>.IsValid())
				{
					MonoBehaviourSingleton<InGameProgress>.I.FieldToHome();
				}
			}
			else
			{
				FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(mapId);
				if (fieldMapData == null || fieldMapData.jumpPortalID == 0)
				{
					Log.Error("RegionMap.OnQuery_SELECT() jumpPortalID is not found.");
				}
				else if (!MonoBehaviourSingleton<GameSceneManager>.I.CheckPortalAndOpenUpdateAppDialog(fieldMapData.jumpPortalID, false, true))
				{
					GameSection.StopEvent();
				}
				else
				{
					if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "InGameScene")
					{
						spots.ClearAllSpot();
						spots.spotRootTransform.gameObject.SetActive(false);
						MonoBehaviourSingleton<UIManager>.I.system.GetCtrl(UIManager.SYSTEM.DIALOG_BLOCKER).gameObject.SetActive(false);
						GameSection.StayEvent();
						CoopApp.EnterField(fieldMapData.jumpPortalID, 0u, delegate(bool is_matching, bool is_connect, bool is_regist)
						{
							if (!is_connect)
							{
								GameSection.ChangeStayEvent("COOP_SERVER_INVALID", null);
								GameSection.ResumeEvent(true, null);
								AppMain i = MonoBehaviourSingleton<AppMain>.I;
								i.onDelayCall = (Action)Delegate.Combine(i.onDelayCall, (Action)delegate
								{
									DispatchEvent("CLOSE", null);
								});
							}
							else
							{
								GameSection.ResumeEvent(is_regist, null);
								if (is_regist)
								{
									MonoBehaviourSingleton<GameSceneManager>.I.ChangeScene("InGame", null, UITransition.TYPE.CLOSE, UITransition.TYPE.OPEN, false);
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
			}
		}
	}

	private void OnQuery_SELECT_CHILD()
	{
		SpotEventData spotEventData = GameSection.GetEventData() as SpotEventData;
		GameSection.StayEvent();
		if (spotEventData == null)
		{
			GameSection.ResumeEvent(false, null);
		}
		else
		{
			regionId = (int)spotEventData.childRegionId;
			StartCoroutine(DoChangeRegion(true));
		}
	}

	private void OnQuery_BACK_REGION()
	{
		RegionTable.Data data = Singleton<RegionTable>.I.GetData((uint)regionId);
		if (data != null)
		{
			if ((UnityEngine.Object)redCircle != (UnityEngine.Object)null)
			{
				redCircle.gameObject.SetActive(false);
			}
			displayQuestTargetMode = false;
			regionId = (int)data.parentRegionId;
			StartCoroutine(DoChangeRegion(false));
		}
	}

	private IEnumerator DoChangeRegion(bool withBlur)
	{
		bool wait2 = false;
		if (withBlur)
		{
			wait2 = true;
			parent.blurFilter.CacheRenderTarget(delegate
			{
				((_003CDoChangeRegion_003Ec__Iterator180)/*Error near IL_0056: stateMachine*/)._003Cwait_003E__0 = false;
				((_003CDoChangeRegion_003Ec__Iterator180)/*Error near IL_0056: stateMachine*/)._003C_003Ef__this.uiParentMapSprite.alpha = 1f;
				((_003CDoChangeRegion_003Ec__Iterator180)/*Error near IL_0056: stateMachine*/)._003C_003Ef__this.spots.ClearAllSpot();
			}, true);
		}
		else
		{
			spots.ClearAllSpot();
		}
		LoadingQueue loadQueue = new LoadingQueue(this);
		LoadObject loadObj = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionMap_" + regionId.ToString("D3"), false);
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		while (wait2)
		{
			yield return (object)null;
		}
		UnityEngine.Object.Destroy(regionMapRoot.gameObject);
		regionMapRoot = ResourceUtility.Realizes(loadObj.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform, -1).gameObject.GetComponent<RegionMapRoot>();
		if ((UnityEngine.Object)regionMapRoot != (UnityEngine.Object)null)
		{
			wait2 = true;
			regionMapRoot.InitPortalStatus(delegate
			{
				((_003CDoChangeRegion_003Ec__Iterator180)/*Error near IL_017e: stateMachine*/)._003Cwait_003E__0 = false;
			});
			while (wait2)
			{
				yield return (object)null;
			}
		}
		if (withBlur)
		{
			float blurTime = 0.25f;
			Vector2 blurCenter = new Vector2(0.5f, 0.5f);
			parent.blurFilter.StartBlurFilter(0.01f, 0.25f, blurTime, blurCenter, delegate
			{
				((_003CDoChangeRegion_003Ec__Iterator180)/*Error near IL_020a: stateMachine*/)._003Cwait_003E__0 = false;
			});
			uiMapSprite.alpha = 0f;
			TweenAlpha.Begin(uiMapSprite.gameObject, blurTime, 1f);
			TweenAlpha.Begin(uiParentMapSprite.gameObject, blurTime, 0f);
		}
		SetFirstCameraPos();
		isOpened = false;
		OnOpen();
		GameSection.ResumeEvent(true, null);
		yield return (object)null;
	}

	private void OnQuery_HOME()
	{
		if (MonoBehaviourSingleton<FilterManager>.I.IsEnabledBlur())
		{
			GameSection.StopEvent();
		}
		else
		{
			int num = (int)GameSection.GetEventData();
			if (num == MonoBehaviourSingleton<FieldManager>.I.currentMapID)
			{
				GameSection.StopEvent();
			}
			else
			{
				OutGameSettingsManager.QuestMap questMap = MonoBehaviourSingleton<OutGameSettingsManager>.I.questMap;
				MonoBehaviourSingleton<FilterManager>.I.StartBlur(questMap.cameraBlurTime, questMap.cameraBlurStrength, questMap.cameraMoveTime);
				spots.ClearAllSpot();
				if (MonoBehaviourSingleton<InGameProgress>.IsValid())
				{
					MonoBehaviourSingleton<InGameProgress>.I.FieldToHome();
				}
				IsCalledExit = true;
			}
		}
	}

	private void OnQuery_SECTION_BACK()
	{
		if (!IsCalledExit)
		{
			GameSection.StayEvent();
			DoExitUIEvent(delegate
			{
				GameSection.ResumeEvent(true, null);
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
		if ((UnityEngine.Object)null != (UnityEngine.Object)redCircle && redCircle.gameObject.activeSelf)
		{
			Camera camera = parent.worldMapCamera._camera;
			if ((UnityEngine.Object)null != (UnityEngine.Object)camera)
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
			if ((UnityEngine.Object)regionMapRoot != (UnityEngine.Object)null)
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
		if (!((UnityEngine.Object)regionMapRoot == (UnityEngine.Object)null))
		{
			Renderer r = regionMapRoot.GetComponentInChildren<Renderer>();
			if (!((UnityEngine.Object)r == (UnityEngine.Object)null))
			{
				for (float timer = 0f; timer < time; timer += Time.deltaTime)
				{
					float alpha = Mathf.Lerp(from, to, timer / time);
					r.material.SetFloat("_Alpha", alpha);
					yield return (object)null;
				}
				r.material.SetFloat("_Alpha", to);
				onComplete?.Invoke();
			}
		}
	}

	protected override void OnDestroy()
	{
		if ((UnityEngine.Object)regionMapRoot != (UnityEngine.Object)null)
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
		bool flag = IsSameRegionPlayer();
		Vector3 targetPosition = GetTargetPosition(fieldMapData, portalData, isStepOver);
		Vector3 vector = targetPosition;
		if (flag)
		{
			RegionMapLocation playerLocation = GetPlayerLocation();
			if ((UnityEngine.Object)null != (UnityEngine.Object)playerLocation)
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
		yield return (object)new WaitForSeconds(0.8f);
		Vector3Interpolator ip = new Vector3Interpolator();
		if (0.1f < Vector3.Distance(from, to))
		{
			ip.Set(1f, from, to, null, default(Vector3), null);
		}
		else
		{
			ip.Set(0f, from, to, null, default(Vector3), null);
		}
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			parent.worldMapCamera.targetPos = ip.Get();
			yield return (object)null;
		}
		if ((UnityEngine.Object)null != (UnityEngine.Object)redCircle)
		{
			yield return (object)new WaitForSeconds(0.5f);
			redCircle.gameObject.SetActive(true);
			TweenAlpha tweenAlpha = redCircle.GetComponent<TweenAlpha>();
			if ((UnityEngine.Object)null != (UnityEngine.Object)tweenAlpha)
			{
				while (tweenAlpha.isActiveAndEnabled)
				{
					yield return (object)null;
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
		if ((UnityEngine.Object)null != (UnityEngine.Object)playerLocation)
		{
			vector = playerLocation.transform.position;
		}
		Vector3 neighbor = Vector3.zero;
		int neighborMapID = GetNeighborMapID();
		RegionMapLocation regionMapLocation = regionMapRoot.FindLocation(neighborMapID);
		if ((UnityEngine.Object)null != (UnityEngine.Object)regionMapLocation)
		{
			neighbor = regionMapLocation.transform.position;
		}
		SetRedCirclePosition(fieldMapData, portalData, isStepOver);
		Transform icon = null;
		Transform button = null;
		SpotManager.Spot spot = spots.FindSpot(neighborMapID);
		if ((UnityEngine.Object)null != (UnityEngine.Object)portalGuideTxt && spot != null)
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
			ip.Set(1f, from, to, null, default(Vector3), null);
		}
		else
		{
			ip.Set(0f, from, to, null, default(Vector3), null);
		}
		yield return (object)new WaitForSeconds(0.8f);
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			parent.worldMapCamera.targetPos = ip.Get();
			yield return (object)null;
		}
		if ((UnityEngine.Object)null != (UnityEngine.Object)redCircle)
		{
			yield return (object)new WaitForSeconds(0.5f);
			redCircle.gameObject.SetActive(true);
			TweenAlpha tweenAlpha2 = redCircle.GetComponent<TweenAlpha>();
			if ((UnityEngine.Object)null != (UnityEngine.Object)tweenAlpha2)
			{
				while (tweenAlpha2.isActiveAndEnabled)
				{
					yield return (object)null;
				}
			}
		}
		ip.Set(0.8f, to, neighbor, null, default(Vector3), null);
		yield return (object)new WaitForSeconds(0.5f);
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			parent.worldMapCamera.targetPos = ip.Get();
			yield return (object)null;
		}
		if ((UnityEngine.Object)null != (UnityEngine.Object)portalGuideTxt)
		{
			yield return (object)new WaitForSeconds(0.5f);
			portalGuideTxt.gameObject.SetActive(true);
			TweenAlpha tweenAlpha = portalGuideTxt.GetComponent<TweenAlpha>();
			if ((UnityEngine.Object)null != (UnityEngine.Object)tweenAlpha)
			{
				while (tweenAlpha.isActiveAndEnabled)
				{
					yield return (object)null;
				}
			}
		}
		if ((UnityEngine.Object)null != (UnityEngine.Object)icon)
		{
			yield return (object)new WaitForSeconds(0.5f);
			UITweenCtrl tweenCtrl = icon.GetComponent<UITweenCtrl>();
			if ((UnityEngine.Object)null != (UnityEngine.Object)tweenCtrl)
			{
				tweenCtrl.Play(true, null);
			}
		}
		Transform cursor = TutorialMessage.AttachCursor(button, null);
		if ((UnityEngine.Object)null != (UnityEngine.Object)cursor)
		{
			Vector3 cursorPos = cursor.localPosition;
			cursor.localPosition = cursorPos + new Vector3(0f, -10f, 0f);
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
				Singleton<FieldMapTable>.I.GetPortalListByMapID(fieldMapDataInRegion[i].mapID, false)?.ForEach(delegate(FieldMapTable.PortalTableData o)
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
		if (map == null && portal == null)
		{
			return Vector3.zero;
		}
		Vector3 result = Vector3.zero;
		if (portal == null)
		{
			RegionMapLocation regionMapLocation = regionMapRoot.FindLocation((int)map.mapID);
			if ((UnityEngine.Object)null != (UnityEngine.Object)regionMapLocation)
			{
				result = regionMapLocation.transform.position;
			}
		}
		else if (portal.mapX < 1E+10f)
		{
			RegionMapLocation regionMapLocation2 = regionMapRoot.FindLocation((int)portal.srcMapID);
			if ((UnityEngine.Object)null != (UnityEngine.Object)regionMapLocation2)
			{
				Vector3 b = new Vector3(portal.mapX, portal.mapY, 0f);
				result = regionMapLocation2.transform.position + b;
			}
		}
		else if (!isStepOver)
		{
			RegionMapLocation regionMapLocation3 = regionMapRoot.FindLocation((int)portal.srcMapID);
			RegionMapLocation regionMapLocation4 = regionMapRoot.FindLocation((int)portal.dstMapID);
			if ((UnityEngine.Object)null != (UnityEngine.Object)regionMapLocation3 && (UnityEngine.Object)null != (UnityEngine.Object)regionMapLocation4)
			{
				result = Vector3.Lerp(regionMapLocation3.transform.position, regionMapLocation4.transform.position, 0.5f);
			}
		}
		else
		{
			RegionMapLocation regionMapLocation5 = regionMapRoot.FindLocation((int)portal.srcMapID);
			if ((UnityEngine.Object)null != (UnityEngine.Object)regionMapLocation5)
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
		if (map != null || portal != null)
		{
			if (portal == null)
			{
				SpotManager.Spot spot = spots.FindSpot((int)map.mapID);
				if (spot != null)
				{
					redCircleOrgPos = spot.originalPos;
					Transform transform = spot._transform.Find("LBL_NAME");
					if ((UnityEngine.Object)null != (UnityEngine.Object)transform)
					{
						transform.transform.gameObject.SetActive(true);
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
		Delivery[] deliveryList = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList(false);
		int i = 0;
		for (int num = deliveryList.Length; i < num; i++)
		{
			int dId = deliveryList[i].dId;
			if (!MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery(dId))
			{
				DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)dId);
				List<uint> mapIdList = deliveryTableData.GetMapIdList();
				if (mapIdList != null)
				{
					int j = 0;
					for (int count = mapIdList.Count; j < count; j++)
					{
						DeliveryTargetMapIds.Add(mapIdList[j]);
					}
				}
				else
				{
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
			GameSection.ChangeEvent("INGAME_SELECT_DIFFICULTY", null);
		}
	}

	private void OnCloseDialog_WorldMapSelectDifficultyDialog()
	{
		object eventData = GameSection.GetEventData();
		if (eventData != null)
		{
			REGION_DIFFICULTY_TYPE rEGION_DIFFICULTY_TYPE = (REGION_DIFFICULTY_TYPE)(int)eventData;
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

	private void OnCloseDialog_InGameSelectDifficultyDialog()
	{
		object eventData = GameSection.GetEventData();
		if (eventData != null)
		{
			REGION_DIFFICULTY_TYPE rEGION_DIFFICULTY_TYPE = (REGION_DIFFICULTY_TYPE)(int)eventData;
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
}
