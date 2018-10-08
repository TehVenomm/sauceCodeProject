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
						fieldQuestWarningRoot = ResourceUtility.Realizes(loadedEncounterBossCutIn.loadedObject, -1).gameObject;
						UIPanel panel = fieldQuestWarningRoot.GetComponentInChildren<UIPanel>();
						if ((UnityEngine.Object)panel != (UnityEngine.Object)null)
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
					regionMapRoot = ResourceUtility.Realizes(loadedRegion.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform, -1).gameObject.GetComponent<RegionMapRoot>();
					if ((UnityEngine.Object)regionMapRoot != (UnityEngine.Object)null)
					{
						bool wait = true;
						regionMapRoot.InitPortalStatus(delegate
						{
							((_003CDoInitialize_003Ec__Iterator18F)/*Error near IL_0527: stateMachine*/)._003Cwait_003E__15 = false;
						});
						while (wait)
						{
							yield return (object)null;
						}
					}
					blurFilter = (ResourceUtility.Instantiate(loadedFilterCamera.loadedObject) as GameObject).GetComponent<ZoomBlurFilter>();
					blurFilter.transform.parent = base._transform;
					_camera = ResourceUtility.Realizes(loadedEventCamera.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform, -1).GetComponent<Camera>();
					uiFrontMapSprite = t.FindChild("FrontMap").gameObject.GetComponent<UITexture>();
					if ((UnityEngine.Object)uiFrontMapSprite != (UnityEngine.Object)null)
					{
						uiFrontMapSprite.alpha = 0f;
					}
					uiMapSprite = t.FindChild("Map").gameObject.GetComponent<UITexture>();
					InitMapSprite(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
					if (eventData.IsEncounterBossEvent())
					{
						t.FindChild("TaptoSkip").gameObject.SetActive(false);
					}
					bgEventListener = UIEventListener.Get(t.Find("BG").gameObject);
					tutorialTrigger = t.FindChild("TUTORIAL_TRIGGER");
					if ((UnityEngine.Object)tutorialTrigger != (UnityEngine.Object)null)
					{
						if (!TutorialStep.HasAllTutorialCompleted())
						{
							tutorialTrigger.gameObject.SetActive(true);
							UITweenCtrl.Play(tutorialTrigger, true, null, false, 0);
						}
						else
						{
							tutorialTrigger.gameObject.SetActive(false);
						}
					}
					spots = new SpotManager(null, loadedLocationSpot.loadedObject as GameObject, _camera);
					spots.spotRootTransform = t;
					playerMarker = ResourceUtility.Realizes(loadedPlayerMarker.loadedObject, -1);
					PlayerMarker playerMarkerCom = playerMarker.GetComponent<PlayerMarker>();
					if ((UnityEngine.Object)null != (UnityEngine.Object)playerMarkerCom)
					{
						playerMarkerCom.SetCamera(_camera.transform);
					}
					windEffect = ResourceUtility.Realizes(loadedWindEffect.loadedObject, _camera.transform, -1).gameObject.GetComponent<rymFX>();
					windEffect.Cameras = new Camera[1]
					{
						_camera
					};
					windEffect.gameObject.layer = LayerMask.NameToLayer("WorldMap");
					if (loadedDungeonEff != null)
					{
						dungeonOpenEffect = ResourceUtility.Realizes(loadedDungeonEff.loadedObject, base._transform, -1);
						dungeonOpenEffect.gameObject.SetActive(false);
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
		if (location.mapId == 0)
		{
			return spots.AddSpot(0, MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionTextList().Find((GameSceneTables.TextData textData) => textData.key == "STR_HOME").text, location.transform.position, SpotManager.ICON_TYPE.HOME, null, false, false, false, null, null, false, SpotManager.HAPPEN_CONDITION.NONE, 0)._transform.gameObject;
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
				string s = location.name.Replace("location", string.Empty);
				int.TryParse(s, out int result);
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
		return spots.AddSpot((int)fieldMapData.mapID, fieldMapData.mapName, location.transform.position, icon, null, isNew, canUnlockNewPortal, false, fieldMapData.mapID, location.icon, false, SpotManager.HAPPEN_CONDITION.NONE, 0)._transform.gameObject;
	}

	private void InitMapSprite(bool isPortrait)
	{
		if ((UnityEngine.Object)uiMapSprite != (UnityEngine.Object)null)
		{
			if ((UnityEngine.Object)_camera.targetTexture != (UnityEngine.Object)null)
			{
				RenderTexture.ReleaseTemporary(_camera.targetTexture);
				_camera.targetTexture = null;
			}
			_camera.targetTexture = RenderTexture.GetTemporary(Screen.width, Screen.height);
			uiMapSprite.mainTexture = _camera.targetTexture;
			uiMapSprite.width = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth;
			uiMapSprite.height = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight;
		}
		if ((UnityEngine.Object)uiFrontMapSprite != (UnityEngine.Object)null && (UnityEngine.Object)blurFilter != (UnityEngine.Object)null)
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
			if ((UnityEngine.Object)null != (UnityEngine.Object)bgEventListener)
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
		if ((UnityEngine.Object)playerMarker != (UnityEngine.Object)null)
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
			if ((UnityEngine.Object)null != (UnityEngine.Object)bgEventListener)
			{
				UIEventListener uIEventListener = bgEventListener;
				uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Remove(uIEventListener.onClick, new UIEventListener.VoidDelegate(onClick));
			}
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("WorldMapOpenNewField", base.gameObject, "INGAME_MAIN", null, null, true);
			calledExit = true;
		}
	}

	public void CameraMoveEvent()
	{
		RegionMapPortal regionMapPortal;
		bool flag = IsPortalReverseAndGetPortalData((int)portalData.portalID, out regionMapPortal);
		if ((UnityEngine.Object)regionMapPortal == (UnityEngine.Object)null)
		{
			RequestEvent("EXIT", null);
		}
		else
		{
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
			StartCoroutine(DoExitEvent(regionMapPortal, null, MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.onlyCameraMoveDelay, flag, false));
		}
	}

	private void QuestToField()
	{
		FieldMapTable.PortalTableData portalTableData = Singleton<FieldMapTable>.I.GetPortalData(MonoBehaviourSingleton<FieldManager>.I.currentPortalID);
		if (portalTableData == null)
		{
			RequestEvent("EXIT", null);
		}
		else
		{
			RegionMapLocation regionMapLocation = regionMapRoot.FindLocation((int)portalTableData.dstMapID);
			if ((UnityEngine.Object)null == (UnityEngine.Object)regionMapLocation)
			{
				RequestEvent("EXIT", null);
			}
			else
			{
				Vector3 position = regionMapLocation.transform.position;
				_camera.transform.position = position - _camera.transform.forward * MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraDistance;
				playerMarker.SetParent(regionMapLocation.transform);
				playerMarker.localPosition = MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerOffset;
				playerMarker.localScale = new Vector3(0f, 0f, 0f);
				StartCoroutine(DoQuestToField());
			}
		}
	}

	private IEnumerator DoQuestToField()
	{
		yield return (object)new WaitForSeconds(0.8f);
		TweenScale.Begin(playerMarker.gameObject, MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerScaleTime, Vector3.one);
		yield return (object)new WaitForSeconds(MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerScaleTime + 1.5f);
		OnQuery_EXIT();
	}

	private bool IsPortalReverseAndGetPortalData(int portalId, out RegionMapPortal portalData)
	{
		portalData = regionMapRoot.FindEntrancePortal(portalId);
		if ((UnityEngine.Object)portalData != (UnityEngine.Object)null)
		{
			return false;
		}
		portalData = regionMapRoot.FindExitPortal(portalId);
		return true;
	}

	private void SetCameraToMiddlePoint(RegionMapPortal portal)
	{
		Vector3 a = (portal.fromLocation.transform.position + portal.toLocation.transform.position) / 2f;
		a -= _camera.transform.forward * MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraDistance;
		_camera.transform.position = a;
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
		yield return (object)null;
		RegionMapPortal portal;
		bool reverse = IsPortalReverseAndGetPortalData((int)portalData.portalID, out portal);
		if ((UnityEngine.Object)portal == (UnityEngine.Object)null)
		{
			RequestEvent("EXIT", null);
		}
		else
		{
			regionMapRoot.animator.Play(portal.gameObject.name);
			SetCameraToMiddlePoint(portal);
			SetPlayerMakerToStartPosition(portal, reverse);
			SoundManager.PlayOneShotUISE(40000032);
			GameObject effect = ResourceUtility.Instantiate(topEffectPrefab) as GameObject;
			rymFX rym = effect.GetComponent<rymFX>();
			rym.Cameras = new Camera[1]
			{
				_camera
			};
			rym.ViewShift = 0f;
			portal.Open(effect.transform, regionMapRoot.animator, false, 1f, delegate
			{
				if (!((_003CDoOpenNewDungeon_003Ec__Iterator191)/*Error near IL_015b: stateMachine*/)._003C_003Ef__this.calledExit)
				{
					GameObject gameObject = ((_003CDoOpenNewDungeon_003Ec__Iterator191)/*Error near IL_015b: stateMachine*/)._003C_003Ef__this.CreateLocationSpot(((_003CDoOpenNewDungeon_003Ec__Iterator191)/*Error near IL_015b: stateMachine*/)._003Cportal_003E__0.toLocation, SpotManager.ICON_TYPE.CHILD_REGION, true);
					if ((UnityEngine.Object)gameObject != (UnityEngine.Object)null)
					{
						gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
						TweenScale.Begin(gameObject, 0.3f, Vector3.one);
					}
					((_003CDoOpenNewDungeon_003Ec__Iterator191)/*Error near IL_015b: stateMachine*/)._003C_003Ef__this.StartCoroutine(((_003CDoOpenNewDungeon_003Ec__Iterator191)/*Error near IL_015b: stateMachine*/)._003C_003Ef__this.DoExitEvent(((_003CDoOpenNewDungeon_003Ec__Iterator191)/*Error near IL_015b: stateMachine*/)._003Cportal_003E__0, ((_003CDoOpenNewDungeon_003Ec__Iterator191)/*Error near IL_015b: stateMachine*/)._003Crym_003E__3, 0f, ((_003CDoOpenNewDungeon_003Ec__Iterator191)/*Error near IL_015b: stateMachine*/)._003Creverse_003E__1, true));
				}
			});
		}
	}

	public void OpenNewLocation()
	{
		RegionMapPortal portal;
		bool reverse = IsPortalReverseAndGetPortalData((int)portalData.portalID, out portal);
		if ((UnityEngine.Object)portal == (UnityEngine.Object)null)
		{
			RequestEvent("EXIT", null);
		}
		else
		{
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
							MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Play(eventData.enemyType, 0, false);
							MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.FadeOut(MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.encounterBossCutInTime + 2f, 0.3f, delegate
							{
								if ((UnityEngine.Object)fieldQuestWarningRoot != (UnityEngine.Object)null)
								{
									UnityEngine.Object.Destroy(fieldQuestWarningRoot);
								}
							});
						}
						if ((UnityEngine.Object)effect != (UnityEngine.Object)null)
						{
							EffectManager.ReleaseEffect(effect, true, false);
						}
						StartCoroutine(DoExitEncounterBossEvent());
					}
					else
					{
						if (reverse)
						{
							location = portal.fromLocation;
						}
						GameObject gameObject = CreateLocationSpot(location, SpotManager.ICON_TYPE.NEW, true);
						if ((UnityEngine.Object)gameObject != (UnityEngine.Object)null)
						{
							gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
							TweenScale.Begin(gameObject, 0.3f, Vector3.one);
							SoundManager.PlayOneShotUISE(40000033);
						}
						StartCoroutine(DoExitEvent(portal, rym, 0f, reverse, false));
					}
				}
			});
		}
	}

	private IEnumerator DoExitEncounterBossEvent()
	{
		yield return (object)new WaitForSeconds(MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.encounterBossCutInTime);
		OnQuery_EXIT();
	}

	private IEnumerator DoExitEvent(RegionMapPortal portal, rymFX effect, float delay = 0f, bool reverse = false, bool findDungeon = false)
	{
		if ((UnityEngine.Object)effect != (UnityEngine.Object)null)
		{
			EffectManager.ReleaseEffect(effect.gameObject, true, false);
			effect = null;
		}
		yield return (object)new WaitForSeconds(delay);
		LoadObject loadObj = null;
		if (findDungeon)
		{
			LoadingQueue loadQueue = new LoadingQueue(this);
			FieldMapTable.FieldMapTableData mapData = Singleton<FieldMapTable>.I.GetFieldMapData((uint)portal.toLocation.mapId);
			loadObj = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionMap_" + mapData.childRegionId.ToString("D3"), false);
			if ((UnityEngine.Object)null != (UnityEngine.Object)dungeonOpenEffect)
			{
				EffectCtrl eff = dungeonOpenEffect.GetComponent<EffectCtrl>();
				eff.Reset();
				for (int i = 0; i < eff.particles.Length; i++)
				{
					ParticleSystem particle = eff.particles[i];
					if (!((UnityEngine.Object)null == (UnityEngine.Object)particle))
					{
						Renderer renderer = particle.GetComponent<Renderer>();
						if (!((UnityEngine.Object)null == (UnityEngine.Object)renderer))
						{
							renderer.sortingOrder = 2;
						}
					}
				}
				dungeonOpenEffect.gameObject.SetActive(true);
				AudioClip clip_effect = eff.attachedAudioClip;
				if ((UnityEngine.Object)clip_effect != (UnityEngine.Object)null)
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
		TweenScale.Begin(playerMarker.gameObject, MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerScaleTime, Vector3.zero);
		yield return (object)new WaitForSeconds(MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerScaleTime);
		float timer = 0f;
		Vector3 target2 = portal.toLocation.transform.position;
		playerMarker.SetParent(portal.toLocation.transform);
		if (reverse)
		{
			target2 = portal.fromLocation.transform.position;
			playerMarker.SetParent(portal.fromLocation.transform);
		}
		playerMarker.localPosition = MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerOffset;
		target2 -= _camera.transform.forward * MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraDistance;
		Vector3 startPos = _camera.transform.position;
		TweenScale.Begin(playerMarker.gameObject, MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerScaleTime, Vector3.one);
		while (timer <= MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraMoveTime)
		{
			timer += Time.deltaTime;
			_camera.transform.position = Vector3.Lerp(startPos, target2, timer / MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventCameraMoveTime);
			yield return (object)null;
		}
		_camera.transform.position = target2;
		yield return (object)new WaitForSeconds(MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.eventRemainTime);
		if (findDungeon)
		{
			yield return (object)StartCoroutine(DoFindNewDungeonEvent(portal, loadObj));
		}
		OnQuery_EXIT();
	}

	private IEnumerator DoFindNewDungeonEvent(RegionMapPortal portal, LoadObject newRegion)
	{
		if ((UnityEngine.Object)blurFilter != (UnityEngine.Object)null)
		{
			bool wait3 = true;
			blurFilter.CacheRenderTarget(delegate
			{
				((_003CDoFindNewDungeonEvent_003Ec__Iterator194)/*Error near IL_005d: stateMachine*/)._003C_003Ef__this.playerMarker.gameObject.SetActive(false);
				((_003CDoFindNewDungeonEvent_003Ec__Iterator194)/*Error near IL_005d: stateMachine*/)._003C_003Ef__this.playerMarker.SetParent(((_003CDoFindNewDungeonEvent_003Ec__Iterator194)/*Error near IL_005d: stateMachine*/)._003C_003Ef__this._transform);
				((_003CDoFindNewDungeonEvent_003Ec__Iterator194)/*Error near IL_005d: stateMachine*/)._003Cwait_003E__0 = false;
			}, true);
			while (wait3)
			{
				yield return (object)null;
			}
			uiFrontMapSprite.alpha = 1f;
			spots.ClearAllSpot();
			UnityEngine.Object.Destroy(regionMapRoot.gameObject);
			RegionMapLocation newLocation = null;
			if (newRegion != null)
			{
				regionMapRoot = ResourceUtility.Realizes(newRegion.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform, -1).gameObject.GetComponent<RegionMapRoot>();
				if ((UnityEngine.Object)regionMapRoot != (UnityEngine.Object)null)
				{
					wait3 = true;
					regionMapRoot.InitPortalStatus(delegate
					{
						((_003CDoFindNewDungeonEvent_003Ec__Iterator194)/*Error near IL_0136: stateMachine*/)._003Cwait_003E__0 = false;
					});
					while (wait3)
					{
						yield return (object)null;
					}
					CreateVisitedLocationSpot();
					newLocation = regionMapRoot.FindLocation(portal.toLocation.mapId);
					if ((UnityEngine.Object)newLocation != (UnityEngine.Object)null)
					{
						SetCameraToLocation(newLocation);
						playerMarker.SetParent(newLocation.transform);
						playerMarker.localPosition = MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerOffset;
					}
				}
			}
			wait3 = true;
			float blurTime = 0.25f;
			Vector2 blurCenter = new Vector2(0.5f, 0.5f);
			blurFilter.StartBlurFilter(0.01f, 0.25f, blurTime, blurCenter, delegate
			{
				((_003CDoFindNewDungeonEvent_003Ec__Iterator194)/*Error near IL_023f: stateMachine*/)._003Cwait_003E__0 = false;
			});
			uiMapSprite.alpha = 0f;
			TweenAlpha.Begin(uiMapSprite.gameObject, blurTime, 1f);
			TweenAlpha.Begin(uiFrontMapSprite.gameObject, blurTime, 0f);
			while (wait3)
			{
				yield return (object)null;
			}
			yield return (object)new WaitForSeconds(1f);
			if ((UnityEngine.Object)regionMapRoot != (UnityEngine.Object)null && (UnityEngine.Object)newLocation != (UnityEngine.Object)null)
			{
				GameObject obj = CreateLocationSpot(newLocation, SpotManager.ICON_TYPE.NEW, true);
				if ((UnityEngine.Object)obj != (UnityEngine.Object)null)
				{
					obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
					TweenScale.Begin(obj, 0.3f, Vector3.one);
					SoundManager.PlayOneShotUISE(40000033);
				}
				yield return (object)new WaitForSeconds(0.5f);
				playerMarker.gameObject.SetActive(true);
				playerMarker.localScale = new Vector3(0f, 0f, 0f);
				TweenScale.Begin(playerMarker.gameObject, MonoBehaviourSingleton<GlobalSettingsManager>.I.worldMapParam.playerMarkerScaleTime, Vector3.one);
			}
			yield return (object)new WaitForSeconds(1.5f);
		}
	}

	private IEnumerator DoAfterWaitForSecond(float time, Action func)
	{
		yield return (object)new WaitForSeconds(time);
		func?.Invoke();
	}

	public override void Exit()
	{
		if ((UnityEngine.Object)windEffect != (UnityEngine.Object)null)
		{
			EffectManager.ReleaseEffect(windEffect.gameObject, true, false);
		}
		if (spots != null)
		{
			spots.ClearAllSpot();
		}
		if ((UnityEngine.Object)regionMapRoot != (UnityEngine.Object)null)
		{
			UnityEngine.Object.Destroy(regionMapRoot.gameObject);
		}
		if ((UnityEngine.Object)_camera != (UnityEngine.Object)null)
		{
			UnityEngine.Object.Destroy(_camera.gameObject);
		}
		if ((UnityEngine.Object)null != (UnityEngine.Object)dungeonOpenEffect)
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
		if (!((UnityEngine.Object)tutorialTrigger == (UnityEngine.Object)null))
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
