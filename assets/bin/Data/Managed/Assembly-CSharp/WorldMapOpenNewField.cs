using rhyme;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapOpenNewField : GameSection
{
	public enum EVENT_TYPE
	{
		NONE,
		ONLY_CAMERA_MOVE,
		ENCOUNTER_BOSS,
		QUEST_TO_FIELD,
		OPEN_NEW_DUNGEON,
		EXIST_IN_DUNGEON
	}

	public enum AUDIO
	{
		DRAW_LINE = 40000032,
		APEAR_LOCATION
	}

	public class SectionEventData
	{
		private EVENT_TYPE eventType;

		public ENEMY_TYPE enemyType
		{
			get;
			private set;
		}

		public SectionEventData(EVENT_TYPE _eventType, ENEMY_TYPE _enemyType)
		{
			eventType = _eventType;
			enemyType = _enemyType;
		}

		public bool IsOnlyCameraMoveEvent()
		{
			return eventType == EVENT_TYPE.ONLY_CAMERA_MOVE;
		}

		public bool IsEnterDungeon()
		{
			return eventType == EVENT_TYPE.EXIST_IN_DUNGEON;
		}

		public bool IsFindNewDungeon()
		{
			return eventType == EVENT_TYPE.OPEN_NEW_DUNGEON;
		}

		public bool IsEncounterBossEvent()
		{
			return eventType == EVENT_TYPE.ENCOUNTER_BOSS;
		}

		public bool IsQuestToField()
		{
			return eventType == EVENT_TYPE.QUEST_TO_FIELD;
		}
	}

	private SectionEventData eventData;

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

	private Vector3 tutorialTriggerPos = new Vector3(3.7f, 0.5f, 0f);

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "FieldMapTable";
			yield return "RegionTable";
		}
	}

	public override void Initialize()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine("DoInitialize");
	}

	private unsafe IEnumerator DoInitialize()
	{
		eventData = (SectionEventData)GameSection.GetEventData();
		if (MonoBehaviourSingleton<InGameManager>.IsValid())
		{
			portalData = Singleton<FieldMapTable>.I.GetPortalData(MonoBehaviourSingleton<InGameManager>.I.beforePortalID);
		}
		if (MonoBehaviourSingleton<OutGameSettingsManager>.IsValid() && portalData == null)
		{
			portalData = Singleton<FieldMapTable>.I.GetPortalData((uint)MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.linkFieldPortalID);
		}
		if (eventData.IsQuestToField())
		{
			portalData = Singleton<FieldMapTable>.I.GetPortalData(MonoBehaviourSingleton<FieldManager>.I.currentPortalID);
		}
		if (portalData == null)
		{
			base.Initialize();
		}
		else
		{
			FieldMapTable.FieldMapTableData currentMapData = Singleton<FieldMapTable>.I.GetFieldMapData(portalData.srcMapID);
			newMapData = Singleton<FieldMapTable>.I.GetFieldMapData(portalData.dstMapID);
			regionId = newMapData.regionId;
			if (NeedDirectionOpenRegion())
			{
				toRegionRelease = true;
				base.Initialize();
			}
			else
			{
				if (currentMapData != null && newMapData != null && newMapData.regionId != currentMapData.regionId)
				{
					regionId = currentMapData.regionId;
				}
				if (newMapData == null || !IsValidRegion(newMapData))
				{
					newMapData = null;
					base.Initialize();
				}
				else
				{
					LoadingQueue loadQueue = new LoadingQueue(this);
					LoadObject loadedEventUIRoot = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "NewFieldOpenEventUIRoot", false);
					LoadObject loadedLocationSpot = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "LocationSpot", false);
					LoadObject loadedEventCamera = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "NewFieldEventCamera", false);
					LoadObject loadedFilterCamera = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ZoomBlurFilterCamera", false);
					LoadObject loadedPlayerMarker = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "PlayerMarker", false);
					LoadObject loadedRegion = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionMap_" + regionId.ToString("D3"), false);
					LoadObject loadedEffect = loadQueue.LoadEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_map_fire_01", false);
					LoadObject loadedWindEffect = loadQueue.LoadEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_bg_questmap_01", false);
					LoadObject loadedDungeonEff = null;
					if (eventData.IsFindNewDungeon() && newMapData != null)
					{
						uint mapID = newMapData.mapID;
						loadedDungeonEff = loadQueue.LoadEffect(RESOURCE_CATEGORY.EFFECT_DUNGEON, "DEF_" + mapID.ToString("D8"), false);
					}
					LoadObject loadedEncounterBossCutIn = null;
					if (eventData.IsEncounterBossEvent())
					{
						loadedEncounterBossCutIn = loadQueue.Load(RESOURCE_CATEGORY.UI, "InGameFieldQuestWarning", false);
					}
					CacheAudio(loadQueue);
					if (loadQueue.IsLoading())
					{
						yield return (object)loadQueue.Wait();
					}
					if (loadedEncounterBossCutIn != null)
					{
						fieldQuestWarningRoot = ResourceUtility.Realizes(loadedEncounterBossCutIn.loadedObject, -1).get_gameObject();
						UIPanel panel = fieldQuestWarningRoot.GetComponentInChildren<UIPanel>();
						if (panel != null)
						{
							panel.depth = 8000;
						}
						if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
						{
							MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Load(loadQueue);
						}
					}
					if (loadQueue.IsLoading())
					{
						yield return (object)loadQueue.Wait();
					}
					topEffectPrefab = loadedEffect.loadedObject;
					Transform t = ResourceUtility.Realizes(loadedEventUIRoot.loadedObject, base._transform, -1);
					regionMapRoot = ResourceUtility.Realizes(loadedRegion.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform, -1).get_gameObject().GetComponent<RegionMapRoot>();
					if (regionMapRoot != null)
					{
						bool wait = true;
						regionMapRoot.InitPortalStatus(new Action((object)/*Error near IL_0527: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
						while (wait)
						{
							yield return (object)null;
						}
					}
					blurFilter = (ResourceUtility.Instantiate<Object>(loadedFilterCamera.loadedObject) as GameObject).GetComponent<ZoomBlurFilter>();
					blurFilter.get_transform().set_parent(base._transform);
					_camera = ResourceUtility.Realizes(loadedEventCamera.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform, -1).GetComponent<Camera>();
					uiFrontMapSprite = t.FindChild("FrontMap").get_gameObject().GetComponent<UITexture>();
					if (uiFrontMapSprite != null)
					{
						uiFrontMapSprite.alpha = 0f;
					}
					uiMapSprite = t.FindChild("Map").get_gameObject().GetComponent<UITexture>();
					InitMapSprite(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
					if (eventData.IsEncounterBossEvent())
					{
						t.FindChild("TaptoSkip").get_gameObject().SetActive(false);
					}
					bgEventListener = UIEventListener.Get(t.Find("BG").get_gameObject());
					tutorialTrigger = t.FindChild("TUTORIAL_TRIGGER");
					if (tutorialTrigger != null)
					{
						if (!TutorialStep.HasAllTutorialCompleted())
						{
							tutorialTrigger.get_gameObject().SetActive(true);
							UITweenCtrl.Play(tutorialTrigger, true, null, false, 0);
						}
						else
						{
							tutorialTrigger.get_gameObject().SetActive(false);
						}
					}
					spots = new SpotManager(null, loadedLocationSpot.loadedObject as GameObject, _camera);
					spots.spotRootTransform = t;
					playerMarker = ResourceUtility.Realizes(loadedPlayerMarker.loadedObject, -1);
					PlayerMarker playerMarkerCom = playerMarker.GetComponent<PlayerMarker>();
					if (null != playerMarkerCom)
					{
						playerMarkerCom.SetCamera(_camera.get_transform());
					}
					windEffect = ResourceUtility.Realizes(loadedWindEffect.loadedObject, _camera.get_transform(), -1).get_gameObject().GetComponent<rymFX>();
					windEffect.Cameras = (Camera[])new Camera[1]
					{
						_camera
					};
					windEffect.get_gameObject().set_layer(LayerMask.NameToLayer("WorldMap"));
					if (loadedDungeonEff != null)
					{
						dungeonOpenEffect = ResourceUtility.Realizes(loadedDungeonEff.loadedObject, base._transform, -1);
						dungeonOpenEffect.get_gameObject().SetActive(false);
					}
					CreateVisitedLocationSpot();
					if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
					{
						MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += InitMapSprite;
					}
					base.Initialize();
				}
			}
		}
	}

	public override void StartSection()
	{
		if (toRegionRelease)
		{
			MonoBehaviourSingleton<WorldMapManager>.I.transferInfo = new WorldMapManager.TransferInfo((int)regionId, true);
			DispatchEvent("WORLD_MAP", null);
			Exit();
		}
	}

	private bool NeedDirectionOpenRegion()
	{
		MonoBehaviourSingleton<WorldMapManager>.I.openNewFieldId = (int)newMapData.mapID;
		return MonoBehaviourSingleton<WorldMapManager>.I.NeedDirectionOpenRegion((int)regionId);
	}

	private void CreateVisitedLocationSpot()
	{
		OutGameSettingsManager.QuestMap questMap = MonoBehaviourSingleton<OutGameSettingsManager>.I.questMap;
		MonoBehaviourSingleton<FilterManager>.I.StopBlur(questMap.cameraMoveTime, 0f);
		for (int i = 0; i < regionMapRoot.locations.Length; i++)
		{
			RegionMapLocation regionMapLocation = regionMapRoot.locations[i];
			FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)regionMapLocation.mapId);
			if ((fieldMapData != null || regionMapLocation.mapId == 0) && (fieldMapData == null || FieldManager.IsShowPortal(fieldMapData.jumpPortalID)))
			{
				if (fieldMapData != null && !MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(fieldMapData))
				{
					CreateLocationSpot(regionMapLocation, SpotManager.ICON_TYPE.NOT_OPENED, false);
				}
				else if (regionMapLocation.mapId == newMapData.mapID && !eventData.IsOnlyCameraMoveEvent() && !eventData.IsEnterDungeon() && !eventData.IsQuestToField())
				{
					CreateLocationSpot(regionMapLocation, SpotManager.ICON_TYPE.NOT_OPENED, false);
				}
				else
				{
					SpotManager.ICON_TYPE iCON_TYPE = SpotManager.ICON_TYPE.CLEARED;
					if (regionMapLocation.portal.Length > 0)
					{
						for (int j = 0; j < regionMapLocation.portal.Length; j++)
						{
							RegionMapPortal regionMapPortal = regionMapLocation.portal[j];
							if (!regionMapPortal.IsVisited() && regionMapPortal.IsShow())
							{
								iCON_TYPE = SpotManager.ICON_TYPE.NEW;
								break;
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
					}
					CreateLocationSpot(regionMapLocation, iCON_TYPE, false);
				}
			}
		}
	}

	private GameObject CreateLocationSpot(RegionMapLocation location, SpotManager.ICON_TYPE icon = SpotManager.ICON_TYPE.CLEARED, bool isNew = false)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Expected O, but got Unknown
		if (location.mapId == 0)
		{
			return spots.AddSpot(0, MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionTextList().Find((GameSceneTables.TextData textData) => textData.key == "STR_HOME").text, location.get_transform().get_position(), SpotManager.ICON_TYPE.HOME, null, false, false, false, null, null, false, SpotManager.HAPPEN_CONDITION.NONE, 0)._transform.get_gameObject();
		}
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)location.mapId);
		if (fieldMapData == null)
		{
			return null;
		}
		bool canUnlockNewPortal = false;
		if (location.portal.Length > 0 && icon != SpotManager.ICON_TYPE.NOT_OPENED)
		{
			for (int i = 0; i < location.portal.Length; i++)
			{
				string s = location.get_name().Replace("location", string.Empty);
				int.TryParse(s, out int result);
				int[] locationNumbers = GetLocationNumbers(location.portal[i].get_name());
				if (result == locationNumbers[0] && GameSaveData.instance.isNewReleasePortal((uint)location.portal[i].entranceId))
				{
					if (!location.portal[i].IsVisited())
					{
						canUnlockNewPortal = true;
						break;
					}
					GameSaveData.instance.newReleasePortals.Remove((uint)location.portal[i].entranceId);
				}
				if (result == locationNumbers[1] && GameSaveData.instance.isNewReleasePortal((uint)location.portal[i].exitId))
				{
					if (!location.portal[i].IsVisited())
					{
						canUnlockNewPortal = true;
						break;
					}
					GameSaveData.instance.newReleasePortals.Remove((uint)location.portal[i].exitId);
				}
			}
		}
		return spots.AddSpot((int)fieldMapData.mapID, fieldMapData.mapName, location.get_transform().get_position(), icon, null, isNew, canUnlockNewPortal, false, fieldMapData.mapID, location.icon, false, SpotManager.HAPPEN_CONDITION.NONE, 0)._transform.get_gameObject();
	}

	private void InitMapSprite(bool isPortrait)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		if (uiMapSprite != null)
		{
			if (_camera.get_targetTexture() != null)
			{
				RenderTexture.ReleaseTemporary(_camera.get_targetTexture());
				_camera.set_targetTexture(null);
			}
			_camera.set_targetTexture(RenderTexture.GetTemporary(Screen.get_width(), Screen.get_height()));
			uiMapSprite.mainTexture = _camera.get_targetTexture();
			uiMapSprite.width = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth;
			uiMapSprite.height = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight;
		}
		if (uiFrontMapSprite != null && blurFilter != null)
		{
			uiFrontMapSprite.mainTexture = blurFilter.filteredTexture;
			uiFrontMapSprite.width = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth;
			uiFrontMapSprite.height = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight;
		}
	}

	protected override void OnOpen()
	{
		if (toRegionRelease)
		{
			base.OnOpen();
		}
		else
		{
			if (null != bgEventListener)
			{
				UIEventListener uIEventListener = bgEventListener;
				uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener.onClick, new UIEventListener.VoidDelegate(onClick));
			}
			if (portalData == null || newMapData == null)
			{
				RequestEvent("EXIT", null);
			}
			else if (eventData.IsFindNewDungeon())
			{
				OpenNewDungeon();
			}
			else if (eventData.IsOnlyCameraMoveEvent() || eventData.IsEnterDungeon())
			{
				CameraMoveEvent();
			}
			else if (eventData.IsQuestToField())
			{
				QuestToField();
			}
			else
			{
				OpenNewLocation();
			}
			base.OnOpen();
		}
	}

	private void OnQuery_EXIT()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Expected O, but got Unknown
		if (playerMarker != null)
		{
			Object.Destroy(playerMarker.get_gameObject());
			playerMarker = null;
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= InitMapSprite;
		}
		this.StopAllCoroutines();
		if (!calledExit)
		{
			if (null != bgEventListener)
			{
				UIEventListener uIEventListener = bgEventListener;
				uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Remove(uIEventListener.onClick, new UIEventListener.VoidDelegate(onClick));
			}
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("WorldMapOpenNewField", this.get_gameObject(), "INGAME_MAIN", null, null, true);
			calledExit = true;
		}
	}

	public void CameraMoveEvent()
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		RegionMapPortal regionMapPortal;
		bool flag = IsPortalReverseAndGetPortalData((int)portalData.portalID, out regionMapPortal);
		if (regionMapPortal == null)
		{
			RequestEvent("EXIT", null);
		}
		else
		{
			Vector3 position = regionMapPortal.fromLocation.get_transform().get_position();
			playerMarker.SetParent(regionMapPortal.fromLocation.get_transform());
			if (flag)
			{
				position = regionMapPortal.toLocation.get_transform().get_position();
				playerMarker.SetParent(regionMapPortal.toLocation.get_transform());
			}
			playerMarker.set_localPosition(MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerOffset);
			position -= _camera.get_transform().get_forward() * MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraDistance;
			_camera.get_transform().set_position(position);
			this.StartCoroutine(DoExitEvent(regionMapPortal, null, MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.onlyCameraMoveDelay, flag, false));
		}
	}

	private void QuestToField()
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		FieldMapTable.PortalTableData portalTableData = Singleton<FieldMapTable>.I.GetPortalData(MonoBehaviourSingleton<FieldManager>.I.currentPortalID);
		if (portalTableData == null)
		{
			RequestEvent("EXIT", null);
		}
		else
		{
			RegionMapLocation regionMapLocation = regionMapRoot.FindLocation((int)portalTableData.dstMapID);
			if (null == regionMapLocation)
			{
				RequestEvent("EXIT", null);
			}
			else
			{
				Vector3 position = regionMapLocation.get_transform().get_position();
				_camera.get_transform().set_position(position - _camera.get_transform().get_forward() * MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraDistance);
				playerMarker.SetParent(regionMapLocation.get_transform());
				playerMarker.set_localPosition(MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerOffset);
				playerMarker.set_localScale(new Vector3(0f, 0f, 0f));
				this.StartCoroutine(DoQuestToField());
			}
		}
	}

	private IEnumerator DoQuestToField()
	{
		yield return (object)new WaitForSeconds(0.8f);
		TweenScale.Begin(playerMarker.get_gameObject(), MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerScaleTime, Vector3.get_one());
		yield return (object)new WaitForSeconds(MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerScaleTime + 1.5f);
		OnQuery_EXIT();
	}

	private bool IsPortalReverseAndGetPortalData(int portalId, out RegionMapPortal portalData)
	{
		portalData = regionMapRoot.FindEntrancePortal(portalId);
		if (portalData != null)
		{
			return false;
		}
		portalData = regionMapRoot.FindExitPortal(portalId);
		return true;
	}

	private void SetCameraToMiddlePoint(RegionMapPortal portal)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = (portal.fromLocation.get_transform().get_position() + portal.toLocation.get_transform().get_position()) / 2f;
		val -= _camera.get_transform().get_forward() * MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraDistance;
		_camera.get_transform().set_position(val);
	}

	private void SetCameraToLocation(RegionMapLocation location)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = location.get_transform().get_position();
		position -= _camera.get_transform().get_forward() * MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraDistance;
		_camera.get_transform().set_position(position);
	}

	private void SetPlayerMakerToStartPosition(RegionMapPortal portal, bool reverse)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		playerMarker.SetParent(portal.fromLocation.get_transform());
		if (reverse)
		{
			playerMarker.SetParent(portal.toLocation.get_transform());
		}
		playerMarker.set_localPosition(MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerOffset);
	}

	public void OpenNewDungeon()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoOpenNewDungeon());
	}

	private unsafe IEnumerator DoOpenNewDungeon()
	{
		yield return (object)null;
		RegionMapPortal portal;
		bool reverse = IsPortalReverseAndGetPortalData((int)portalData.portalID, out portal);
		if (portal == null)
		{
			RequestEvent("EXIT", null);
		}
		else
		{
			regionMapRoot.animator.Play(portal.get_gameObject().get_name());
			SetCameraToMiddlePoint(portal);
			SetPlayerMakerToStartPosition(portal, reverse);
			SoundManager.PlayOneShotUISE(40000032);
			GameObject effect = ResourceUtility.Instantiate<Object>(topEffectPrefab) as GameObject;
			rymFX rym = effect.GetComponent<rymFX>();
			rym.Cameras = (Camera[])new Camera[1]
			{
				_camera
			};
			rym.ViewShift = 0f;
			portal.Open(effect.get_transform(), regionMapRoot.animator, false, 1f, new Action((object)/*Error near IL_015b: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	public unsafe void OpenNewLocation()
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Expected O, but got Unknown
		//IL_0145: Expected O, but got Unknown
		RegionMapPortal portal;
		bool reverse = IsPortalReverseAndGetPortalData((int)portalData.portalID, out portal);
		if (portal == null)
		{
			RequestEvent("EXIT", null);
		}
		else
		{
			string text = portal.get_gameObject().get_name();
			if (reverse)
			{
				text += "_R";
			}
			regionMapRoot.animator.Play(text);
			SetCameraToMiddlePoint(portal);
			SetPlayerMakerToStartPosition(portal, reverse);
			GameObject effect = ResourceUtility.Instantiate<Object>(topEffectPrefab) as GameObject;
			rymFX rym = effect.GetComponent<rymFX>();
			rym.Cameras = (Camera[])new Camera[1]
			{
				_camera
			};
			rym.ViewShift = 0f;
			float endTime = 1f;
			if (eventData.IsEncounterBossEvent())
			{
				endTime = 0.4f;
			}
			SoundManager.PlayOneShotUISE(40000032);
			_003COpenNewLocation_003Ec__AnonStorey4A1 _003COpenNewLocation_003Ec__AnonStorey4A;
			portal.Open(effect.get_transform(), regionMapRoot.animator, reverse, endTime, new Action((object)_003COpenNewLocation_003Ec__AnonStorey4A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	private IEnumerator DoExitEncounterBossEvent()
	{
		yield return (object)new WaitForSeconds(MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.encounterBossCutInTime);
		OnQuery_EXIT();
	}

	private IEnumerator DoExitEvent(RegionMapPortal portal, rymFX effect, float delay = 0f, bool reverse = false, bool findDungeon = false)
	{
		if (effect != null)
		{
			EffectManager.ReleaseEffect(effect.get_gameObject(), true, false);
			effect = null;
		}
		yield return (object)new WaitForSeconds(delay);
		LoadObject loadObj = null;
		if (findDungeon)
		{
			LoadingQueue loadQueue = new LoadingQueue(this);
			FieldMapTable.FieldMapTableData mapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)portal.toLocation.mapId);
			loadObj = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionMap_" + mapData.childRegionId.ToString("D3"), false);
			if (null != dungeonOpenEffect)
			{
				EffectCtrl eff = dungeonOpenEffect.GetComponent<EffectCtrl>();
				eff.Reset();
				for (int i = 0; i < eff.particles.Length; i++)
				{
					ParticleSystem particle = eff.particles[i];
					if (!(null == particle))
					{
						Renderer renderer = particle.GetComponent<Renderer>();
						if (!(null == renderer))
						{
							renderer.set_sortingOrder(2);
						}
					}
				}
				dungeonOpenEffect.get_gameObject().SetActive(true);
				AudioClip clip_effect = eff.attachedAudioClip;
				if (clip_effect != null)
				{
					int SE_CONFIG_AREA_LOCATION = eff.attachedAudioSettingID;
					SoundManager.PlayOneShotUISE(clip_effect, SE_CONFIG_AREA_LOCATION);
				}
				yield return (object)new WaitForSeconds(eff.waitTime);
			}
			if (loadQueue.IsLoading())
			{
				yield return (object)loadQueue.Wait();
			}
		}
		TweenScale.Begin(playerMarker.get_gameObject(), MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerScaleTime, Vector3.get_zero());
		yield return (object)new WaitForSeconds(MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerScaleTime);
		float timer = 0f;
		Vector3 target2 = portal.toLocation.get_transform().get_position();
		playerMarker.SetParent(portal.toLocation.get_transform());
		if (reverse)
		{
			target2 = portal.fromLocation.get_transform().get_position();
			playerMarker.SetParent(portal.fromLocation.get_transform());
		}
		playerMarker.set_localPosition(MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerOffset);
		target2 -= _camera.get_transform().get_forward() * MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraDistance;
		Vector3 startPos = _camera.get_transform().get_position();
		TweenScale.Begin(playerMarker.get_gameObject(), MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerScaleTime, Vector3.get_one());
		while (timer <= MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraMoveTime)
		{
			timer += Time.get_deltaTime();
			_camera.get_transform().set_position(Vector3.Lerp(startPos, target2, timer / MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraMoveTime));
			yield return (object)null;
		}
		_camera.get_transform().set_position(target2);
		yield return (object)new WaitForSeconds(MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventRemainTime);
		if (findDungeon)
		{
			yield return (object)this.StartCoroutine(DoFindNewDungeonEvent(portal, loadObj));
		}
		OnQuery_EXIT();
	}

	private unsafe IEnumerator DoFindNewDungeonEvent(RegionMapPortal portal, LoadObject newRegion)
	{
		if (blurFilter != null)
		{
			bool wait3 = true;
			blurFilter.CacheRenderTarget(new Action((object)/*Error near IL_005d: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), true);
			while (wait3)
			{
				yield return (object)null;
			}
			uiFrontMapSprite.alpha = 1f;
			spots.ClearAllSpot();
			Object.Destroy(regionMapRoot.get_gameObject());
			RegionMapLocation newLocation = null;
			if (newRegion != null)
			{
				regionMapRoot = ResourceUtility.Realizes(newRegion.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform, -1).get_gameObject().GetComponent<RegionMapRoot>();
				if (regionMapRoot != null)
				{
					wait3 = true;
					regionMapRoot.InitPortalStatus(new Action((object)/*Error near IL_0136: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
					while (wait3)
					{
						yield return (object)null;
					}
					CreateVisitedLocationSpot();
					newLocation = regionMapRoot.FindLocation(portal.toLocation.mapId);
					if (newLocation != null)
					{
						SetCameraToLocation(newLocation);
						playerMarker.SetParent(newLocation.get_transform());
						playerMarker.set_localPosition(MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerOffset);
					}
				}
			}
			wait3 = true;
			float blurTime = 0.25f;
			Vector2 blurCenter = new Vector2(0.5f, 0.5f);
			blurFilter.StartBlurFilter(0.01f, 0.25f, blurTime, blurCenter, new Action((object)/*Error near IL_023f: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			uiMapSprite.alpha = 0f;
			TweenAlpha.Begin(uiMapSprite.get_gameObject(), blurTime, 1f);
			TweenAlpha.Begin(uiFrontMapSprite.get_gameObject(), blurTime, 0f);
			while (wait3)
			{
				yield return (object)null;
			}
			yield return (object)new WaitForSeconds(1f);
			if (regionMapRoot != null && newLocation != null)
			{
				GameObject obj = CreateLocationSpot(newLocation, SpotManager.ICON_TYPE.NEW, true);
				if (obj != null)
				{
					obj.get_transform().set_localScale(new Vector3(0.1f, 0.1f, 0.1f));
					TweenScale.Begin(obj, 0.3f, Vector3.get_one());
					SoundManager.PlayOneShotUISE(40000033);
				}
				yield return (object)new WaitForSeconds(0.5f);
				playerMarker.get_gameObject().SetActive(true);
				playerMarker.set_localScale(new Vector3(0f, 0f, 0f));
				TweenScale.Begin(playerMarker.get_gameObject(), MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerScaleTime, Vector3.get_one());
			}
			yield return (object)new WaitForSeconds(1.5f);
		}
	}

	private IEnumerator DoAfterWaitForSecond(float time, Action func)
	{
		yield return (object)new WaitForSeconds(time);
		if (func != null)
		{
			func.Invoke();
		}
	}

	public override void Exit()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		if (windEffect != null)
		{
			EffectManager.ReleaseEffect(windEffect.get_gameObject(), true, false);
		}
		if (spots != null)
		{
			spots.ClearAllSpot();
		}
		if (regionMapRoot != null)
		{
			Object.Destroy(regionMapRoot.get_gameObject());
		}
		if (_camera != null)
		{
			Object.Destroy(_camera.get_gameObject());
		}
		if (null != dungeonOpenEffect)
		{
			Object.Destroy(dungeonOpenEffect);
		}
		base.Exit();
	}

	private void LateUpdate()
	{
		UpdateTutorialTrigger();
		if (spots != null)
		{
			spots.Update();
		}
		if (isUpdateRenderTexture)
		{
			InitMapSprite(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
			isUpdateRenderTexture = false;
		}
	}

	private void UpdateTutorialTrigger()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		if (!(tutorialTrigger == null))
		{
			Vector3 val = _camera.WorldToScreenPoint(tutorialTriggerPos);
			val = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(val);
			val.z = 0f;
			tutorialTrigger.set_position(val);
		}
	}

	private void OnApplicationPause(bool paused)
	{
		isUpdateRenderTexture = !paused;
	}

	private void onClick(GameObject g)
	{
		if (!eventData.IsEncounterBossEvent())
		{
			OnQuery_EXIT();
		}
	}

	public static bool IsValidRegionFromMapId(uint mapId)
	{
		if (!Singleton<FieldMapTable>.IsValid())
		{
			return false;
		}
		return IsValidRegion(Singleton<FieldMapTable>.I.GetFieldMapData(mapId));
	}

	public static bool IsValidRegion(FieldMapTable.FieldMapTableData mapData)
	{
		if (mapData == null)
		{
			return false;
		}
		if (!Singleton<RegionTable>.IsValid())
		{
			return false;
		}
		RegionTable.Data[] data = Singleton<RegionTable>.I.GetData();
		if (data == null || data.Length == 0)
		{
			return false;
		}
		RegionTable.Data data2 = Array.Find(data, (RegionTable.Data o) => o.regionId == mapData.regionId);
		return null != data2;
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
}
