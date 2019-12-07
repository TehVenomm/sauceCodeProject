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

	private UnityEngine.Object topEffectPrefab;

	private UnityEngine.Object remainEffectPrefab;

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
		StartCoroutine("DoInitialize");
	}

	private IEnumerator DoInitialize()
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
			yield break;
		}
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(portalData.srcMapID);
		newMapData = Singleton<FieldMapTable>.I.GetFieldMapData(portalData.dstMapID);
		regionId = newMapData.regionId;
		if (NeedDirectionOpenRegion())
		{
			toRegionRelease = true;
			base.Initialize();
			yield break;
		}
		if (fieldMapData != null && newMapData != null && newMapData.regionId != fieldMapData.regionId)
		{
			regionId = fieldMapData.regionId;
		}
		if (newMapData == null || !IsValidRegion(newMapData))
		{
			newMapData = null;
			base.Initialize();
			yield break;
		}
		LoadingQueue loadQueue = new LoadingQueue(this);
		LoadObject loadedEventUIRoot = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "NewFieldOpenEventUIRoot");
		LoadObject loadedLocationSpot = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "LocationSpot");
		LoadObject loadedEventCamera = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "NewFieldEventCamera");
		LoadObject loadedFilterCamera = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ZoomBlurFilterCamera");
		LoadObject loadedPlayerMarker = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "PlayerMarker");
		LoadObject loadedRegion = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionMap_" + regionId.ToString("D3"));
		LoadObject loadedEffect = loadQueue.LoadEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_map_fire_01");
		LoadObject loadedWindEffect = loadQueue.LoadEffect(RESOURCE_CATEGORY.EFFECT_ACTION, "ef_btl_bg_questmap_01");
		LoadObject loadedDungeonEff = null;
		if (eventData.IsFindNewDungeon() && newMapData != null)
		{
			uint mapID = newMapData.mapID;
			loadedDungeonEff = loadQueue.LoadEffect(RESOURCE_CATEGORY.EFFECT_DUNGEON, "DEF_" + mapID.ToString("D8"));
		}
		LoadObject loadedEncounterBossCutIn = null;
		if (eventData.IsEncounterBossEvent())
		{
			loadedEncounterBossCutIn = loadQueue.Load(RESOURCE_CATEGORY.UI, "InGameFieldQuestWarning");
		}
		CacheAudio(loadQueue);
		if (loadQueue.IsLoading())
		{
			yield return loadQueue.Wait();
		}
		if (loadedEncounterBossCutIn != null)
		{
			fieldQuestWarningRoot = ResourceUtility.Realizes(loadedEncounterBossCutIn.loadedObject).gameObject;
			UIPanel componentInChildren = fieldQuestWarningRoot.GetComponentInChildren<UIPanel>();
			if (componentInChildren != null)
			{
				componentInChildren.depth = 8000;
			}
			if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
			{
				MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Load(loadQueue);
			}
		}
		if (loadQueue.IsLoading())
		{
			yield return loadQueue.Wait();
		}
		topEffectPrefab = loadedEffect.loadedObject;
		Transform t = ResourceUtility.Realizes(loadedEventUIRoot.loadedObject, base._transform);
		regionMapRoot = ResourceUtility.Realizes(loadedRegion.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform).gameObject.GetComponent<RegionMapRoot>();
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
		blurFilter = (ResourceUtility.Instantiate(loadedFilterCamera.loadedObject) as GameObject).GetComponent<ZoomBlurFilter>();
		blurFilter.transform.parent = base._transform;
		_camera = ResourceUtility.Realizes(loadedEventCamera.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform).GetComponent<Camera>();
		uiFrontMapSprite = t.Find("FrontMap").gameObject.GetComponent<UITexture>();
		if (uiFrontMapSprite != null)
		{
			uiFrontMapSprite.alpha = 0f;
		}
		uiMapSprite = t.Find("Map").gameObject.GetComponent<UITexture>();
		InitMapSprite(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		if (eventData.IsEncounterBossEvent())
		{
			t.Find("TaptoSkip").gameObject.SetActive(value: false);
		}
		bgEventListener = UIEventListener.Get(t.Find("BG").gameObject);
		tutorialTrigger = t.Find("TUTORIAL_TRIGGER");
		if (tutorialTrigger != null)
		{
			if (!TutorialStep.HasAllTutorialCompleted())
			{
				tutorialTrigger.gameObject.SetActive(value: true);
				UITweenCtrl.Play(tutorialTrigger, forward: true, null, is_input_block: false);
			}
			else
			{
				tutorialTrigger.gameObject.SetActive(value: false);
			}
		}
		spots = new SpotManager(null, loadedLocationSpot.loadedObject as GameObject, _camera);
		spots.spotRootTransform = t;
		playerMarker = ResourceUtility.Realizes(loadedPlayerMarker.loadedObject);
		PlayerMarker component = playerMarker.GetComponent<PlayerMarker>();
		if (null != component)
		{
			component.SetCamera(_camera.transform);
		}
		windEffect = ResourceUtility.Realizes(loadedWindEffect.loadedObject, _camera.transform).gameObject.GetComponent<rymFX>();
		windEffect.Cameras = new Camera[1]
		{
			_camera
		};
		windEffect.gameObject.layer = LayerMask.NameToLayer("WorldMap");
		if (loadedDungeonEff != null)
		{
			dungeonOpenEffect = ResourceUtility.Realizes(loadedDungeonEff.loadedObject, base._transform);
			dungeonOpenEffect.gameObject.SetActive(value: false);
		}
		CreateVisitedLocationSpot();
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += InitMapSprite;
		}
		base.Initialize();
	}

	public override void StartSection()
	{
		if (toRegionRelease)
		{
			MonoBehaviourSingleton<WorldMapManager>.I.transferInfo = new WorldMapManager.TransferInfo((int)regionId, directInGame: true);
			DispatchEvent("WORLD_MAP");
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
		MonoBehaviourSingleton<FilterManager>.I.StopBlur(questMap.cameraMoveTime);
		for (int i = 0; i < regionMapRoot.locations.Length; i++)
		{
			RegionMapLocation regionMapLocation = regionMapRoot.locations[i];
			FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)regionMapLocation.mapId);
			if ((fieldMapData == null && regionMapLocation.mapId != 0) || (fieldMapData != null && !FieldManager.IsShowPortal(fieldMapData.jumpPortalID)))
			{
				continue;
			}
			if (fieldMapData != null && !MonoBehaviourSingleton<FieldManager>.I.CanJumpToMap(fieldMapData))
			{
				CreateLocationSpot(regionMapLocation, SpotManager.ICON_TYPE.NOT_OPENED);
				continue;
			}
			if (regionMapLocation.mapId == newMapData.mapID && !eventData.IsOnlyCameraMoveEvent() && !eventData.IsEnterDungeon() && !eventData.IsQuestToField())
			{
				CreateLocationSpot(regionMapLocation, SpotManager.ICON_TYPE.NOT_OPENED);
				continue;
			}
			SpotManager.ICON_TYPE iCON_TYPE = SpotManager.ICON_TYPE.CLEARED;
			if (regionMapLocation.portal.Length != 0)
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
			CreateLocationSpot(regionMapLocation, iCON_TYPE);
		}
	}

	private GameObject CreateLocationSpot(RegionMapLocation location, SpotManager.ICON_TYPE icon = SpotManager.ICON_TYPE.CLEARED, bool isNew = false)
	{
		if (location.mapId == 0)
		{
			return spots.AddSpot(0, MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionTextList().Find((GameSceneTables.TextData textData) => textData.key == "STR_HOME").text, location.transform.position, SpotManager.ICON_TYPE.HOME, null)._transform.gameObject;
		}
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)location.mapId);
		if (fieldMapData == null)
		{
			return null;
		}
		bool canUnlockNewPortal = false;
		if (location.portal.Length != 0 && icon != SpotManager.ICON_TYPE.NOT_OPENED)
		{
			for (int i = 0; i < location.portal.Length; i++)
			{
				int.TryParse(location.name.Replace("location", ""), out int result);
				int[] locationNumbers = GetLocationNumbers(location.portal[i].name);
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
		return spots.AddSpot((int)fieldMapData.mapID, fieldMapData.mapName, location.transform.position, icon, null, isNew, canUnlockNewPortal, viewEnemyPopBallon: false, fieldMapData.mapID, location.icon)._transform.gameObject;
	}

	private void InitMapSprite(bool isPortrait)
	{
		if (uiMapSprite != null)
		{
			if (_camera.targetTexture != null)
			{
				RenderTexture.ReleaseTemporary(_camera.targetTexture);
				_camera.targetTexture = null;
			}
			_camera.targetTexture = RenderTexture.GetTemporary(Screen.width, Screen.height);
			uiMapSprite.mainTexture = _camera.targetTexture;
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
			return;
		}
		if (null != bgEventListener)
		{
			UIEventListener uIEventListener = bgEventListener;
			uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener.onClick, new UIEventListener.VoidDelegate(onClick));
		}
		if (portalData == null || newMapData == null)
		{
			RequestEvent("EXIT");
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
		Transform transform = Utility.FindChild(base.gameObject.transform, "BG");
		if (transform != null)
		{
			UITexture component = transform.GetComponent<UITexture>();
			if (component != null)
			{
				component.width = 4000;
				component.height = 4000;
				component.gameObject.SetActive(value: true);
			}
		}
		base.OnOpen();
	}

	private void OnQuery_EXIT()
	{
		if (playerMarker != null)
		{
			UnityEngine.Object.Destroy(playerMarker.gameObject);
			playerMarker = null;
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= InitMapSprite;
		}
		StopAllCoroutines();
		if (!calledExit)
		{
			if (null != bgEventListener)
			{
				UIEventListener uIEventListener = bgEventListener;
				uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Remove(uIEventListener.onClick, new UIEventListener.VoidDelegate(onClick));
			}
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("WorldMapOpenNewField", base.gameObject, "INGAME_MAIN");
			calledExit = true;
		}
	}

	public void CameraMoveEvent()
	{
		RegionMapPortal regionMapPortal;
		bool flag = IsPortalReverseAndGetPortalData((int)portalData.portalID, out regionMapPortal);
		if (regionMapPortal == null)
		{
			RequestEvent("EXIT");
			return;
		}
		Vector3 position = regionMapPortal.fromLocation.transform.position;
		playerMarker.SetParent(regionMapPortal.fromLocation.transform);
		if (flag)
		{
			position = regionMapPortal.toLocation.transform.position;
			playerMarker.SetParent(regionMapPortal.toLocation.transform);
		}
		playerMarker.localPosition = MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerOffset;
		position -= _camera.transform.forward * MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraDistance;
		_camera.transform.position = position;
		StartCoroutine(DoExitEvent(regionMapPortal, null, MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.onlyCameraMoveDelay, flag));
	}

	private void QuestToField()
	{
		FieldMapTable.PortalTableData portalTableData = Singleton<FieldMapTable>.I.GetPortalData(MonoBehaviourSingleton<FieldManager>.I.currentPortalID);
		if (portalTableData == null)
		{
			RequestEvent("EXIT");
			return;
		}
		RegionMapLocation regionMapLocation = regionMapRoot.FindLocation((int)portalTableData.dstMapID);
		if (null == regionMapLocation)
		{
			RequestEvent("EXIT");
			return;
		}
		Vector3 position = regionMapLocation.transform.position;
		_camera.transform.position = position - _camera.transform.forward * MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraDistance;
		playerMarker.SetParent(regionMapLocation.transform);
		playerMarker.localPosition = MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerOffset;
		playerMarker.localScale = new Vector3(0f, 0f, 0f);
		StartCoroutine(DoQuestToField());
	}

	private IEnumerator DoQuestToField()
	{
		yield return new WaitForSeconds(0.8f);
		TweenScale.Begin(playerMarker.gameObject, MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerScaleTime, Vector3.one);
		yield return new WaitForSeconds(MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerScaleTime + 1.5f);
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
		Vector3 position = (portal.fromLocation.transform.position + portal.toLocation.transform.position) / 2f;
		position -= _camera.transform.forward * MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraDistance;
		_camera.transform.position = position;
	}

	private void SetCameraToLocation(RegionMapLocation location)
	{
		Vector3 position = location.transform.position;
		position -= _camera.transform.forward * MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraDistance;
		_camera.transform.position = position;
	}

	private void SetPlayerMakerToStartPosition(RegionMapPortal portal, bool reverse)
	{
		playerMarker.SetParent(portal.fromLocation.transform);
		if (reverse)
		{
			playerMarker.SetParent(portal.toLocation.transform);
		}
		playerMarker.localPosition = MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerOffset;
	}

	public void OpenNewDungeon()
	{
		StartCoroutine(DoOpenNewDungeon());
	}

	private IEnumerator DoOpenNewDungeon()
	{
		yield return null;
		RegionMapPortal portal;
		bool reverse = IsPortalReverseAndGetPortalData((int)portalData.portalID, out portal);
		if (portal == null)
		{
			RequestEvent("EXIT");
			yield break;
		}
		regionMapRoot.animator.Play(portal.gameObject.name);
		SetCameraToMiddlePoint(portal);
		SetPlayerMakerToStartPosition(portal, reverse);
		SoundManager.PlayOneShotUISE(40000032);
		GameObject gameObject = ResourceUtility.Instantiate(topEffectPrefab) as GameObject;
		rymFX rym = gameObject.GetComponent<rymFX>();
		rym.Cameras = new Camera[1]
		{
			_camera
		};
		rym.ViewShift = 0f;
		portal.Open(gameObject.transform, regionMapRoot.animator, reverse: false, 1f, delegate
		{
			if (!calledExit)
			{
				GameObject gameObject2 = CreateLocationSpot(portal.toLocation, SpotManager.ICON_TYPE.CHILD_REGION, isNew: true);
				if (gameObject2 != null)
				{
					gameObject2.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
					TweenScale.Begin(gameObject2, 0.3f, Vector3.one);
				}
				StartCoroutine(DoExitEvent(portal, rym, 0f, reverse, findDungeon: true));
			}
		});
	}

	public void OpenNewLocation()
	{
		RegionMapPortal portal;
		bool reverse = IsPortalReverseAndGetPortalData((int)portalData.portalID, out portal);
		if (portal == null)
		{
			RequestEvent("EXIT");
			return;
		}
		string text = portal.gameObject.name;
		if (reverse)
		{
			text += "_R";
		}
		regionMapRoot.animator.Play(text);
		SetCameraToMiddlePoint(portal);
		SetPlayerMakerToStartPosition(portal, reverse);
		GameObject effect = ResourceUtility.Instantiate(topEffectPrefab) as GameObject;
		rymFX rym = effect.GetComponent<rymFX>();
		rym.Cameras = new Camera[1]
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
		portal.Open(effect.transform, regionMapRoot.animator, reverse, endTime, delegate
		{
			if (!calledExit)
			{
				RegionMapLocation location = portal.toLocation;
				if (eventData.IsEncounterBossEvent())
				{
					if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
					{
						MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Play(eventData.enemyType);
						MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.FadeOut(MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.encounterBossCutInTime + 2f, 0.3f, delegate
						{
							if (fieldQuestWarningRoot != null)
							{
								UnityEngine.Object.Destroy(fieldQuestWarningRoot);
							}
						});
					}
					if (effect != null)
					{
						EffectManager.ReleaseEffect(effect);
					}
					StartCoroutine(DoExitEncounterBossEvent());
				}
				else
				{
					if (reverse)
					{
						location = portal.fromLocation;
					}
					GameObject gameObject = CreateLocationSpot(location, SpotManager.ICON_TYPE.NEW, isNew: true);
					if (gameObject != null)
					{
						gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
						TweenScale.Begin(gameObject, 0.3f, Vector3.one);
						SoundManager.PlayOneShotUISE(40000033);
					}
					StartCoroutine(DoExitEvent(portal, rym, 0f, reverse));
				}
			}
		});
	}

	private IEnumerator DoExitEncounterBossEvent()
	{
		yield return new WaitForSeconds(MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.encounterBossCutInTime);
		OnQuery_EXIT();
	}

	private IEnumerator DoExitEvent(RegionMapPortal portal, rymFX effect, float delay = 0f, bool reverse = false, bool findDungeon = false)
	{
		if (effect != null)
		{
			EffectManager.ReleaseEffect(effect.gameObject);
			effect = null;
		}
		yield return new WaitForSeconds(delay);
		LoadObject loadObj = null;
		if (findDungeon)
		{
			LoadingQueue loadQueue = new LoadingQueue(this);
			FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)portal.toLocation.mapId);
			loadObj = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionMap_" + fieldMapData.childRegionId.ToString("D3"));
			if (null != dungeonOpenEffect)
			{
				EffectCtrl component = dungeonOpenEffect.GetComponent<EffectCtrl>();
				component.Reset();
				for (int i = 0; i < component.particles.Length; i++)
				{
					ParticleSystem particleSystem = component.particles[i];
					if (!(null == particleSystem))
					{
						Renderer component2 = particleSystem.GetComponent<Renderer>();
						if (!(null == component2))
						{
							component2.sortingOrder = 2;
						}
					}
				}
				dungeonOpenEffect.gameObject.SetActive(value: true);
				AudioClip attachedAudioClip = component.attachedAudioClip;
				if (attachedAudioClip != null)
				{
					int attachedAudioSettingID = component.attachedAudioSettingID;
					SoundManager.PlayOneShotUISE(attachedAudioClip, attachedAudioSettingID);
				}
				yield return new WaitForSeconds(component.waitTime);
			}
			if (loadQueue.IsLoading())
			{
				yield return loadQueue.Wait();
			}
		}
		TweenScale.Begin(playerMarker.gameObject, MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerScaleTime, Vector3.zero);
		yield return new WaitForSeconds(MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerScaleTime);
		float timer = 0f;
		Vector3 target = portal.toLocation.transform.position;
		playerMarker.SetParent(portal.toLocation.transform);
		if (reverse)
		{
			target = portal.fromLocation.transform.position;
			playerMarker.SetParent(portal.fromLocation.transform);
		}
		playerMarker.localPosition = MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerOffset;
		target -= _camera.transform.forward * MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraDistance;
		Vector3 startPos = _camera.transform.position;
		TweenScale.Begin(playerMarker.gameObject, MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerScaleTime, Vector3.one);
		while (timer <= MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraMoveTime)
		{
			timer += Time.deltaTime;
			_camera.transform.position = Vector3.Lerp(startPos, target, timer / MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraMoveTime);
			yield return null;
		}
		_camera.transform.position = target;
		yield return new WaitForSeconds(MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventRemainTime);
		if (findDungeon)
		{
			yield return StartCoroutine(DoFindNewDungeonEvent(portal, loadObj));
		}
		OnQuery_EXIT();
	}

	private IEnumerator DoFindNewDungeonEvent(RegionMapPortal portal, LoadObject newRegion)
	{
		if (!(blurFilter != null))
		{
			yield break;
		}
		bool wait = true;
		blurFilter.CacheRenderTarget(delegate
		{
			playerMarker.gameObject.SetActive(value: false);
			playerMarker.SetParent(base._transform);
			wait = false;
		}, reqWithFilter: true);
		while (wait)
		{
			yield return null;
		}
		uiFrontMapSprite.alpha = 1f;
		spots.ClearAllSpot();
		UnityEngine.Object.Destroy(regionMapRoot.gameObject);
		RegionMapLocation newLocation = null;
		if (newRegion != null)
		{
			regionMapRoot = ResourceUtility.Realizes(newRegion.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform).gameObject.GetComponent<RegionMapRoot>();
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
				CreateVisitedLocationSpot();
				newLocation = regionMapRoot.FindLocation(portal.toLocation.mapId);
				if (newLocation != null)
				{
					SetCameraToLocation(newLocation);
					playerMarker.SetParent(newLocation.transform);
					playerMarker.localPosition = MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerOffset;
				}
			}
		}
		wait = true;
		float duration = 0.25f;
		Vector2 blurCenter = new Vector2(0.5f, 0.5f);
		blurFilter.StartBlurFilter(0.01f, 0.25f, duration, blurCenter, delegate
		{
			wait = false;
		});
		uiMapSprite.alpha = 0f;
		TweenAlpha.Begin(uiMapSprite.gameObject, duration, 1f);
		TweenAlpha.Begin(uiFrontMapSprite.gameObject, duration, 0f);
		while (wait)
		{
			yield return null;
		}
		yield return new WaitForSeconds(1f);
		if (regionMapRoot != null && newLocation != null)
		{
			GameObject gameObject = CreateLocationSpot(newLocation, SpotManager.ICON_TYPE.NEW, isNew: true);
			if (gameObject != null)
			{
				gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
				TweenScale.Begin(gameObject, 0.3f, Vector3.one);
				SoundManager.PlayOneShotUISE(40000033);
			}
			yield return new WaitForSeconds(0.5f);
			playerMarker.gameObject.SetActive(value: true);
			playerMarker.localScale = new Vector3(0f, 0f, 0f);
			TweenScale.Begin(playerMarker.gameObject, MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerScaleTime, Vector3.one);
		}
		yield return new WaitForSeconds(1.5f);
	}

	private IEnumerator DoAfterWaitForSecond(float time, Action func)
	{
		yield return new WaitForSeconds(time);
		func?.Invoke();
	}

	public override void Exit()
	{
		if (windEffect != null)
		{
			EffectManager.ReleaseEffect(windEffect.gameObject);
		}
		if (spots != null)
		{
			spots.ClearAllSpot();
		}
		if (regionMapRoot != null)
		{
			UnityEngine.Object.Destroy(regionMapRoot.gameObject);
		}
		if (_camera != null)
		{
			UnityEngine.Object.Destroy(_camera.gameObject);
		}
		if (null != dungeonOpenEffect)
		{
			UnityEngine.Object.Destroy(dungeonOpenEffect);
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
		if (!(tutorialTrigger == null))
		{
			Vector3 position = _camera.WorldToScreenPoint(tutorialTriggerPos);
			position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(position);
			position.z = 0f;
			tutorialTrigger.position = position;
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
		return data2 != null;
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
}
