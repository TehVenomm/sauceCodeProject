using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMap : GameSection
{
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
		OBJ_FRAME
	}

	private enum TWEEN_ANIMATION
	{
		OPENING,
		ENDING
	}

	private class ValidRegionInfo
	{
		public RegionTable.Data data;

		public LoadObject icon;

		public LoadObject releaseIcon;

		public REGION_STATUS status;

		public ValidRegionInfo(RegionTable.Data _data, LoadObject _icon, LoadObject _releaseIcon, REGION_STATUS _status)
		{
			data = _data;
			icon = _icon;
			releaseIcon = _releaseIcon;
			status = _status;
		}
	}

	private enum REGION_STATUS
	{
		OPEN,
		CLOSE
	}

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

	private string beforeSectionName = string.Empty;

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

	private ValidRegionInfo[] validRegionInfo;

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

	public WorldMapCameraController worldMapCamera
	{
		get;
		private set;
	}

	public ZoomBlurFilter blurFilter
	{
		get
		{
			return _blurFilter;
		}
		private set
		{
			_blurFilter = value;
		}
	}

	public override void Initialize()
	{
		isInGame = (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene");
		this.StartCoroutine("DoInitialize");
	}

	private IEnumerator DoInitialize()
	{
		bool is_recv_delivery = false;
		MonoBehaviourSingleton<DeliveryManager>.I.SendEventNormalList(delegate
		{
			is_recv_delivery = true;
		});
		while (!is_recv_delivery)
		{
			yield return null;
		}
		LoadingQueue loadQueue = new LoadingQueue(this);
		LoadObject loadedWorldMap = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "WorldMap");
		LoadObject loadedRegionSpotRoot = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionSpotRoot");
		LoadObject loadedRegionSpot = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionSpot");
		LoadObject loadedFilterCamera = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ZoomBlurFilterCamera");
		LoadObject loadedPlayerMarker = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "PlayerMarker");
		uint[] openedRegionIds = MonoBehaviourSingleton<WorldMapManager>.I.GetOpenRegionIdListInWorldMap();
		uint[] validRegionIds = MonoBehaviourSingleton<WorldMapManager>.I.GetValidRegionIdListInWorldMap();
		if (MonoBehaviourSingleton<WorldMapManager>.I.releaseRegionIdfromBoard > 0)
		{
			releaseRegionId = MonoBehaviourSingleton<WorldMapManager>.I.releaseRegionIdfromBoard;
		}
		if (releaseRegionId < 0)
		{
			uint[] array = openedRegionIds;
			foreach (uint regionId in array)
			{
				if (!MonoBehaviourSingleton<WorldMapManager>.I.IsShowedOpenRegion((int)regionId))
				{
					releaseRegionId = (int)regionId;
					break;
				}
			}
		}
		if (releaseRegionId < 0 && MonoBehaviourSingleton<WorldMapManager>.I.transferInfo != null)
		{
			releaseRegionId = MonoBehaviourSingleton<WorldMapManager>.I.transferInfo.nextRegionId;
		}
		if (openedRegionIds.Length == 0)
		{
			openedRegionIds = new uint[1];
		}
		if (validRegionIds.Length == 0)
		{
			validRegionIds = new uint[1];
		}
		LoadObject[] regionAreaLOs = new LoadObject[validRegionIds.Length];
		string newRegionIcon = ResourceName.GetRegionIcon(0);
		string passedRegionIcon = ResourceName.GetRegionIcon(1);
		string closeRegionIcon = ResourceName.GetRegionIcon(2);
		validRegionInfo = new ValidRegionInfo[validRegionIds.Length];
		for (int j = 0; j < validRegionIds.Length; j++)
		{
			RegionTable.Data data = Singleton<RegionTable>.I.GetData(validRegionIds[j]);
			if (data.hasParentRegion())
			{
				continue;
			}
			string resource_name = passedRegionIcon;
			REGION_STATUS status = REGION_STATUS.OPEN;
			int num = Array.IndexOf(openedRegionIds, validRegionIds[j]);
			if (num < 0)
			{
				resource_name = closeRegionIcon;
				status = REGION_STATUS.CLOSE;
			}
			else
			{
				EventNormalListData eventNormalListData = MonoBehaviourSingleton<DeliveryManager>.I.GetEventNormalListData((int)data.regionId);
				if (eventNormalListData != null && eventNormalListData.numerator < eventNormalListData.denominator)
				{
					resource_name = newRegionIcon;
				}
			}
			LoadObject icon = loadQueue.Load(RESOURCE_CATEGORY.REGION_ICON, resource_name);
			LoadObject releaseIcon = loadQueue.Load(RESOURCE_CATEGORY.REGION_ICON, newRegionIcon);
			validRegionInfo[j] = new ValidRegionInfo(data, icon, releaseIcon, status);
			if (j != 0)
			{
				regionAreaLOs[j] = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "WorldMapPart" + validRegionIds[j].ToString("D3"));
			}
		}
		if (loadQueue.IsLoading())
		{
			yield return loadQueue.Wait();
		}
		worldMapUIRoot = ResourceUtility.Realizes(loadedWorldMap.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform).get_gameObject();
		worldMapCamera = worldMapUIRoot.get_transform().Find("Camera").GetComponent<WorldMapCameraController>();
		GameObject map3 = worldMapUIRoot.get_transform().Find("Map").get_gameObject();
		Transform map2 = worldMapUIRoot.get_transform().Find("Map2");
		spots = new SpotManager(loadedRegionSpotRoot.loadedObject as GameObject, loadedRegionSpot.loadedObject as GameObject, worldMapCamera._camera);
		spots.CreateSpotRoot();
		spots.SetRoot(base._transform);
		tweenAnimations = spots.spotRootTransform.GetComponentsInChildren<UITweenCtrl>();
		blurFilter = (ResourceUtility.Instantiate<Object>(loadedFilterCamera.loadedObject) as GameObject).GetComponent<ZoomBlurFilter>();
		UIPanel spotPanel = spots.spotRootTransform.GetComponent<UIPanel>();
		if (spotPanel != null)
		{
			spotPanel.depth = base.baseDepth + 1;
		}
		SetSelectorDepth(spots.spotRootTransform, spotPanel.depth);
		currentRegionID = 0;
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
		if (fieldMapData != null)
		{
			currentRegionID = (int)fieldMapData.regionId;
		}
		RegionTable.Data regionData = (releaseRegionId <= 0) ? Singleton<RegionTable>.I.GetData((uint)currentRegionID) : Singleton<RegionTable>.I.GetData((uint)releaseRegionId);
		if (regionData != null)
		{
			currentWorldIndex = Mathf.Max(0, regionData.worldId - 1);
			currentDifficulty = regionData.difficulty;
		}
		else
		{
			currentWorldIndex = 0;
			currentDifficulty = REGION_DIFFICULTY_TYPE.NORMAL;
		}
		GameObject worldMap2Object = null;
		Transform worldSelect = FindCtrl(spots.spotRootTransform, UI.OBJ_WORLD_SELECT);
		if (map2 == null)
		{
			worldSelect.get_gameObject().SetActive(false);
		}
		else
		{
			worldMap2Object = map2.get_gameObject();
			map2.get_gameObject().SetActive(false);
			LoadObject world3 = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, ResourceName.GetChapterImageName(1));
			LoadObject world2 = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, ResourceName.GetChapterImageName(2));
			if (loadQueue.IsLoading())
			{
				yield return loadQueue.Wait();
			}
			Transform parent = spots.spotRootTransform;
			chapterContentList = new List<Transform>();
			chapterContentList.Add(FindCtrl(parent, UI.SPR_CONTENT1));
			chapterContentList.Add(FindCtrl(parent, UI.SPR_CONTENT2));
			chapterContentList.Add(FindCtrl(parent, UI.SPR_CONTENT3));
			chapterContentList.Add(FindCtrl(parent, UI.SPR_CONTENT4));
			currentCenterIndex = 0;
			SetupChapterContentTexture(parent, world3.loadedObject as Texture2D, world2.loadedObject as Texture2D);
			center = FindCtrl(parent, UI.OBJ_WRAP_CENTER).GetComponent<UICenterOnChild>();
			center.onCenter = DragChapter;
			chapterScrollView = FindCtrl(parent, UI.SCR_SELECTOR).GetComponent<UIScrollView>();
			UIWidget w = worldSelect.GetComponent<UIWidget>();
			SyncWorldMapSelectAntors(w);
		}
		worldMaps = (GameObject[])new GameObject[2]
		{
			map3.get_gameObject(),
			worldMap2Object
		};
		playerMarker = ResourceUtility.Realizes(loadedPlayerMarker.loadedObject, base._transform);
		playerMarker.get_gameObject().SetActive(false);
		regionAreas = (Transform[])new Transform[regionAreaLOs.Length];
		for (int k = 0; k < regionAreaLOs.Length; k++)
		{
			if (!(worldMaps[validRegionInfo[k].data.worldId - 1] == null))
			{
				Transform transform = worldMaps[validRegionInfo[k].data.worldId - 1].get_transform();
				LoadObject loadObject = regionAreaLOs[k];
				if (loadObject != null && null != loadObject.loadedObject)
				{
					regionAreas[k] = ResourceUtility.Realizes(loadObject.loadedObject, transform);
					regionAreas[k].get_gameObject().SetActive(false);
				}
			}
		}
		for (int l = 0; l < worldMaps.Length; l++)
		{
			worldMaps[l].SetActive(l == currentWorldIndex);
		}
		isInWorldMap = FieldManager.IsInWorldMap(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
		bool existAreas = MonoBehaviourSingleton<WorldMapManager>.I.IsExistedWorld2();
		SetActive(spots.spotRootTransform, UI.OBJ_WORLD_SELECT, existAreas);
		if (existAreas)
		{
			SetupChapterUI();
		}
		if (isInGame)
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += InitMapSprite;
		}
		base.Initialize();
	}

	private void SyncWorldMapSelectAntors(UIWidget w)
	{
		if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.HasSafeArea)
		{
			DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
			w.leftAnchor.absolute = specialDeviceInfo.WorldMapWorldSelectAnchor.left;
			w.rightAnchor.absolute = specialDeviceInfo.WorldMapWorldSelectAnchor.right;
			w.bottomAnchor.absolute = specialDeviceInfo.WorldMapWorldSelectAnchor.bottom;
			w.topAnchor.absolute = specialDeviceInfo.WorldMapWorldSelectAnchor.top;
			w.UpdateAnchors();
		}
	}

	private void SetSelectorDepth(Transform parent, int baseDepth)
	{
		UIPanel component = FindCtrl(parent, UI.OBJ_SELECTOR).GetComponent<UIPanel>();
		if (component != null)
		{
			component.depth = baseDepth + 1;
		}
		UIPanel component2 = FindCtrl(parent, UI.SCR_SELECTOR).GetComponent<UIPanel>();
		if (component2 != null)
		{
			component2.depth = baseDepth + 2;
		}
		UIPanel component3 = FindCtrl(parent, UI.OBJ_ARROW).GetComponent<UIPanel>();
		if (component3 != null)
		{
			component3.depth = baseDepth + 3;
		}
		UIPanel component4 = FindCtrl(parent, UI.OBJ_FRAME).GetComponent<UIPanel>();
		if (component4 != null)
		{
			component4.depth = baseDepth + 4;
		}
	}

	private void DragChapter(GameObject go)
	{
		for (int i = 0; i < 4; i++)
		{
			if (chapterContentList[i].get_name() == go.get_name())
			{
				currentCenterIndex = i;
				break;
			}
		}
		if (currentWorldIndex != contentWorldIndex[currentCenterIndex])
		{
			currentWorldIndex = contentWorldIndex[currentCenterIndex];
			ChangeActiveWorld();
		}
	}

	private void SetupChapterContentTexture(Transform parent, Texture2D world1, Texture2D world2)
	{
		if (currentWorldIndex == 0)
		{
			SetTexture(parent, UI.TEX_CON1, world1);
			SetTexture(parent, UI.TEX_CON2, world2);
			SetTexture(parent, UI.TEX_CON3, world1);
			SetTexture(parent, UI.TEX_CON4, world2);
			contentWorldIndex = new int[4]
			{
				0,
				1,
				0,
				1
			};
		}
		else
		{
			SetTexture(parent, UI.TEX_CON1, world2);
			SetTexture(parent, UI.TEX_CON2, world1);
			SetTexture(parent, UI.TEX_CON3, world2);
			SetTexture(parent, UI.TEX_CON4, world1);
			contentWorldIndex = new int[4]
			{
				1,
				0,
				1,
				0
			};
		}
	}

	private void SetupChapterUI()
	{
		Transform spotRootTransform = spots.spotRootTransform;
		SetActive(spotRootTransform, UI.SPR_INACTIVE_ARROW_L, is_visible: false);
		SetActive(spotRootTransform, UI.OBJ_ACTIVE_ARROW_L, is_visible: true);
		SetActive(spotRootTransform, UI.SPR_INACTIVE_ARROW_R, is_visible: false);
		SetActive(spotRootTransform, UI.OBJ_ACTIVE_ARROW_R, is_visible: true);
	}

	public void InitRegionInfo()
	{
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		if (spots == null)
		{
			return;
		}
		Transform spotRootTransform = spots.spotRootTransform;
		if (uiMapSprite == null)
		{
			uiMapSprite = spotRootTransform.Find("Map").get_gameObject().GetComponent<UITexture>();
		}
		if (mapTween == null)
		{
			mapTween = spotRootTransform.Find("Map").get_gameObject().GetComponent<TweenAlpha>();
		}
		InitMapSprite(isPortrait: false);
		if (currentWorldIndex >= 0)
		{
			worldMaps[currentWorldIndex].SetActive(true);
		}
		for (int i = 0; i < validRegionInfo.Length; i++)
		{
			RegionTable.Data data = validRegionInfo[i].data;
			if (data == null || currentWorldIndex != data.worldId - 1 || currentDifficulty != data.difficulty)
			{
				continue;
			}
			int mapNo = Singleton<RegionTable>.I.GetMapNo((int)data.regionId);
			string event_name = "OPEN_REGION";
			string regionName = data.regionName;
			if (validRegionInfo[i].status == REGION_STATUS.CLOSE)
			{
				if (validRegionInfo[i].data.difficulty == REGION_DIFFICULTY_TYPE.NORMAL)
				{
					event_name = "RELEASE_REGION";
					if (isInGame)
					{
						event_name = "INGAME_RELEASE_REGION";
					}
				}
				else if (validRegionInfo[i].data.difficulty == REGION_DIFFICULTY_TYPE.HARD)
				{
					event_name = ((!isInGame) ? "HARD_NOT_OPEN" : "INGAME_HARD_NOT_OPEN");
				}
			}
			SpotManager.Spot spot = spots.AddSpot((int)data.regionId, regionName, data.iconPos, SpotManager.ICON_TYPE.CLEARED, event_name, isNew: false, canUnlockNewPortal: false, viewEnemyPopBallon: false, (int)data.regionId, null, isExistDelivery: false, SpotManager.HAPPEN_CONDITION.NONE, mapNo);
			spot.SetIconSprite("SPR_ICON", validRegionInfo[i].icon.loadedObject as Texture2D, (int)data.iconSize.x, (int)data.iconSize.y);
			if (currentRegionID == data.regionId && isInWorldMap)
			{
				playerMarker.get_gameObject().SetActive(true);
				playerMarker.SetParent(worldMaps[currentWorldIndex].get_transform());
				PlayerMarker component = playerMarker.GetComponent<PlayerMarker>();
				component.SetWorldMode(enable: true);
				component.SetCamera(worldMapCamera._camera.get_transform());
				playerMarker.set_localPosition(data.markerPos);
			}
			if (releaseRegionId == (int)data.regionId)
			{
				spot._transform.get_gameObject().SetActive(false);
				if (isInWorldMap)
				{
					playerMarker.get_gameObject().SetActive(false);
				}
			}
		}
	}

	private void InitMapSprite(bool isPortrait)
	{
		if (uiMapSprite != null)
		{
			if (null == worldMapCamera._camera.get_targetTexture())
			{
				worldMapCamera.Restore();
			}
			uiMapSprite.mainTexture = worldMapCamera._camera.get_targetTexture();
			uiMapSprite.width = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth;
			uiMapSprite.height = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight;
		}
	}

	protected override void OnOpen()
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		if (currentWorldIndex >= 0)
		{
			worldMaps[currentWorldIndex].SetActive(true);
		}
		RegionTable.Data[] data = Singleton<RegionTable>.I.GetData();
		if (!isInWorldMap)
		{
			worldMapCamera.targetPos = data[0].iconPos;
		}
		else if (0 <= currentRegionID && data.Length > currentRegionID)
		{
			worldMapCamera.targetPos = data[currentRegionID].iconPos;
		}
		Transform val = spots.spotRootTransform.Find("CLOSE_BTN/OBJ_CLOSE_BTN_ROOT");
		UIWidget widget = null;
		if (val != null)
		{
			widget = val.GetComponent<UIWidget>();
			if (widget != null)
			{
				widget.alpha = 0f;
				widget.get_transform().set_localScale(Vector3.get_zero());
			}
		}
		Transform val2 = FindCtrl(spots.spotRootTransform, UI.OBJ_WORLD_SELECT);
		UIWidget selectWidget = null;
		if (val2 != null)
		{
			selectWidget = val2.GetComponent<UIWidget>();
			if (selectWidget != null)
			{
				selectWidget.alpha = 0f;
			}
			selectWidget.get_gameObject().SetActive(false);
		}
		FadeInMap(delegate
		{
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			selectWidget.get_gameObject().SetActive(true);
			InitRegionInfo();
			if (widget != null)
			{
				widget.alpha = 0f;
				widget.get_transform().set_localScale(Vector3.get_zero());
			}
			if (selectWidget != null)
			{
				selectWidget.alpha = 0f;
			}
			PlayTween(TWEEN_ANIMATION.OPENING);
			UpdateAreas();
			UpdateDifficultyButton();
			RegionTable.Data data2 = Singleton<RegionTable>.I.GetData((uint)releaseRegionId);
			if (!MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && GameSceneEvent.request == null && releaseRegionId > 0)
			{
				bool useReleaseRegion = false;
				if (MonoBehaviourSingleton<WorldMapManager>.I.releaseRegionIdfromBoard > 0)
				{
					useReleaseRegion = true;
					MonoBehaviourSingleton<WorldMapManager>.I.releaseRegionIdfromBoard = 0;
				}
				this.StartCoroutine(PlayOpenRegionMap(useReleaseRegion));
			}
		});
		base.collectUI = base._transform;
		isChangingMap = false;
		base.OnOpen();
	}

	protected override void OnCloseStart()
	{
		base.collectUI = null;
	}

	public override void Exit()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= InitMapSprite;
		}
		MonoBehaviourSingleton<FilterManager>.I.StopBlur();
		spots.ClearAllSpot();
		base.Exit();
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

	protected override void OnDestroy()
	{
		if (blurFilter != null)
		{
			Object.Destroy(blurFilter.get_gameObject());
		}
		if (worldMapUIRoot != null)
		{
			Object.Destroy(worldMapUIRoot);
		}
		GameObject[] array = worldMaps;
		foreach (GameObject val in array)
		{
			if (val != null)
			{
				Object.Destroy(val);
			}
		}
		base.OnDestroy();
	}

	public void OnQuery_OPEN_REGION()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		UpdateDifficultyButton(forceOff: true);
		int id = (int)GameSection.GetEventData();
		blurCenter = new Vector2(0.5f, 0.5f);
		SpotManager.Spot spot = spots.FindSpot(id);
		if (spot != null)
		{
			blurCenter = spot.GetScreenPos();
		}
		PlayTween(TWEEN_ANIMATION.ENDING);
		GameSection.StayEvent();
		blurFilter.CacheRenderTarget(delegate
		{
			playerMarker.get_gameObject().SetActive(false);
			playerMarker.SetParent(base._transform);
			spots.ClearAllSpot();
			GameSection.ResumeEvent(is_resume: true);
		});
	}

	public void OnQuery_OPEN_REGION_CHANGE()
	{
		UpdateDifficultyButton(forceOff: true);
		this.StopAllCoroutines();
		spots.ClearAllSpot();
		DisableWorldMapObject();
	}

	public void OnQuery_DIRECT_REGION()
	{
		UpdateDifficultyButton(forceOff: true);
		this.StopAllCoroutines();
		GameSection.SetEventData(-1);
		spots.ClearAllSpot();
	}

	public void OnQuery_DIRECT_EVENT()
	{
		UpdateDifficultyButton(forceOff: true);
		this.StopAllCoroutines();
		int eventMapRegionID = (int)GameSection.GetEventData();
		MonoBehaviourSingleton<WorldMapManager>.I.eventMapRegionID = eventMapRegionID;
		GameSection.SetEventData(-4);
		spots.ClearAllSpot();
	}

	public void OnQuery_DIRECT_REGION_QUEST()
	{
		UpdateDifficultyButton(forceOff: true);
		this.StopAllCoroutines();
		GameSection.SetEventData(-2);
		spots.ClearAllSpot();
	}

	public void OnQuery_DIRECT_REGION_TUTORIAL()
	{
		UpdateDifficultyButton(forceOff: true);
		this.StopAllCoroutines();
		GameSection.SetEventData(-3);
		spots.ClearAllSpot();
	}

	private void OnQuery_SECTION_BACK()
	{
		GameSection.StayEvent();
		PlayTween(TWEEN_ANIMATION.ENDING, delegate
		{
			if (worldMapUIRoot != null)
			{
				Object.Destroy(worldMapUIRoot);
			}
			GameObject[] array = worldMaps;
			foreach (GameObject val in array)
			{
				if (val != null)
				{
					val.SetActive(false);
				}
			}
			GameSection.ResumeEvent(is_resume: true);
		});
	}

	public void FadeInMap(Action onComplete)
	{
		if (currentWorldIndex >= 0 && worldMaps[currentWorldIndex] != null)
		{
			worldMaps[currentWorldIndex].SetActive(true);
		}
		if (uiMapSprite != null)
		{
			uiMapSprite.get_gameObject().SetActive(true);
		}
		this.StartCoroutine(DoFadeMap(0f, 1f, 0.4f, delegate
		{
			if (onComplete != null)
			{
				onComplete();
			}
		}));
	}

	public void DisableWorldMapObject()
	{
		GameObject[] array = worldMaps;
		foreach (GameObject val in array)
		{
			if (val != null)
			{
				val.SetActive(false);
			}
		}
		if (uiMapSprite != null)
		{
			uiMapSprite.get_gameObject().SetActive(false);
		}
	}

	private IEnumerator DoFadeMap(float from, float to, float time, Action onComplete)
	{
		if (currentWorldIndex < 0 || worldMaps[currentWorldIndex] == null)
		{
			yield break;
		}
		Renderer r = worldMaps[currentWorldIndex].GetComponentInChildren<Renderer>();
		if (r == null)
		{
			yield break;
		}
		Renderer[] areaRenderers = (Renderer[])new Renderer[regionAreas.Length];
		for (int i = 0; i < areaRenderers.Length; i++)
		{
			if (null != regionAreas[i])
			{
				Renderer component = regionAreas[i].GetComponent<Renderer>();
				if (null != component)
				{
					component.get_material().SetFloat("_Alpha", 0f);
				}
				areaRenderers[i] = component;
			}
		}
		float timer2;
		for (timer2 = 0f; timer2 < time; timer2 += Time.get_deltaTime())
		{
			if (null == r)
			{
				yield break;
			}
			float alpha = Mathf.Lerp(from, to, timer2 / time);
			r.get_material().SetFloat("_Alpha", alpha);
			yield return null;
		}
		r.get_material().SetFloat("_Alpha", to);
		timer2 = 0f;
		for (float alphaTime = 0.15f; timer2 <= alphaTime; timer2 += Time.get_deltaTime())
		{
			float alpha2 = Mathf.Lerp(0f, 1.2f, timer2 / alphaTime);
			for (int j = 0; j < areaRenderers.Length; j++)
			{
				if (null != areaRenderers[j])
				{
					areaRenderers[j].get_material().SetFloat("_Alpha", alpha2);
				}
			}
			yield return null;
		}
		onComplete?.Invoke();
	}

	private void LateUpdate()
	{
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

	private bool IsExistedHard()
	{
		uint[] openRegionIdListInWorldMap = MonoBehaviourSingleton<WorldMapManager>.I.GetOpenRegionIdListInWorldMap();
		for (int i = 0; i < openRegionIdListInWorldMap.Length; i++)
		{
			RegionTable.Data data = Singleton<RegionTable>.I.GetData(openRegionIdListInWorldMap[i]);
			if (data != null && data.worldId == currentWorldIndex + 1 && data.HasGroup())
			{
				return true;
			}
		}
		return false;
	}

	public void EnterRegionMapEvent(Action onCompleteFilter)
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		if (blurFilter == null)
		{
			if (onCompleteFilter != null)
			{
				onCompleteFilter();
			}
		}
		else
		{
			DisableWorldMapObject();
			blurFilter.StartBlurFilter(0f, 0.25f, 0.25f, blurCenter, delegate
			{
				//IL_0016: Unknown result type (might be due to invalid IL or missing references)
				blurFilter.SetBlurPram(0f, blurCenter);
				onCompleteFilter();
				if (regionAreas != null)
				{
					for (int i = 0; i < regionAreas.Length; i++)
					{
						if (null != regionAreas[i])
						{
							regionAreas[i].get_gameObject().SetActive(false);
						}
					}
				}
			});
		}
	}

	private void OnQuery_NEXT_WORLD()
	{
		if (!isChangingMap)
		{
			UpdateDifficultyButton(forceOff: true);
			if (currentCenterIndex + 1 >= 4)
			{
				currentCenterIndex = 0;
			}
			else
			{
				currentCenterIndex++;
			}
			center.CenterOn(chapterContentList[currentCenterIndex]);
		}
	}

	private void OnQuery_PREV_WORLD()
	{
		if (!isChangingMap)
		{
			UpdateDifficultyButton(forceOff: true);
			if (currentCenterIndex <= 0)
			{
				currentCenterIndex = 3;
			}
			else
			{
				currentCenterIndex--;
			}
			center.CenterOn(chapterContentList[currentCenterIndex]);
		}
	}

	private void ChangeActiveWorld()
	{
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		isChangingMap = true;
		spots.ClearAllSpot();
		for (int i = 0; i < worldMaps.Length; i++)
		{
			worldMaps[i].SetActive(i == currentWorldIndex);
		}
		if (currentDifficulty == REGION_DIFFICULTY_TYPE.HARD && !IsExistedHard())
		{
			currentDifficulty = REGION_DIFFICULTY_TYPE.NORMAL;
		}
		RegionTable.Data data = Singleton<RegionTable>.I.GetData((uint)currentRegionID);
		if (data != null)
		{
			if (data.worldId == currentWorldIndex + 1)
			{
				worldMapCamera.targetPos = data.iconPos;
			}
			else if (currentWorldIndex == 0)
			{
				RegionTable.Data data2 = Singleton<RegionTable>.I.GetData(0u);
				worldMapCamera.targetPos = data2.iconPos;
			}
			else if (currentWorldIndex == 1)
			{
				RegionTable.Data data3 = Singleton<RegionTable>.I.GetData(9u);
				worldMapCamera.targetPos = data3.iconPos;
			}
		}
		ChangeActiveArea();
	}

	private IEnumerator ChangeFadeMap()
	{
		mapTween.set_enabled(true);
		mapTween.ResetToBeginning();
		mapTween.PlayForward();
		yield return (object)new WaitForSeconds(mapTween.duration / 2f);
		for (int i = 0; i < worldMaps.Length; i++)
		{
			worldMaps[i].SetActive(i == currentWorldIndex);
		}
		Animator animator = worldMaps[currentWorldIndex].GetComponent<Animator>();
		int hash = Animator.StringToHash("Take 001");
		animator.Play(hash, 0, 1f);
	}

	private void ChangeActiveArea()
	{
		UpdateAreas();
		this.StartCoroutine(DoFadeMap(0f, 1f, 0.4f, delegate
		{
			InitRegionInfo();
			isChangingMap = false;
			UpdateDifficultyButton();
		}));
	}

	private void OnApplicationPause(bool paused)
	{
		isUpdateRenderTexture = !paused;
	}

	private void OnCloseDialog_WorldMapSelectDifficultyDialog()
	{
		object eventData = GameSection.GetEventData();
		if (eventData != null)
		{
			REGION_DIFFICULTY_TYPE rEGION_DIFFICULTY_TYPE = (REGION_DIFFICULTY_TYPE)eventData;
			if (rEGION_DIFFICULTY_TYPE != currentDifficulty)
			{
				currentDifficulty = rEGION_DIFFICULTY_TYPE;
				UpdateDifficultyButton();
				spots.ClearAllSpot();
				InitRegionInfo();
				UpdateAreas();
			}
		}
	}

	private void OnCloseDialog_InGameSelectDifficultyDialog()
	{
		object eventData = GameSection.GetEventData();
		if (eventData != null)
		{
			REGION_DIFFICULTY_TYPE rEGION_DIFFICULTY_TYPE = (REGION_DIFFICULTY_TYPE)eventData;
			if (rEGION_DIFFICULTY_TYPE != currentDifficulty)
			{
				currentDifficulty = rEGION_DIFFICULTY_TYPE;
				UpdateDifficultyButton();
				spots.ClearAllSpot();
				InitRegionInfo();
				UpdateAreas();
			}
		}
	}

	private void UpdateAreas()
	{
		if (regionAreas == null)
		{
			return;
		}
		for (int i = 0; i < regionAreas.Length; i++)
		{
			if (!(null == regionAreas[i]))
			{
				if (validRegionInfo[i].status == REGION_STATUS.OPEN && currentWorldIndex == validRegionInfo[i].data.worldId - 1 && currentDifficulty == validRegionInfo[i].data.difficulty && validRegionInfo[i].data.regionId != (uint)releaseRegionId)
				{
					regionAreas[i].get_gameObject().SetActive(true);
				}
				else
				{
					regionAreas[i].get_gameObject().SetActive(false);
				}
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
		bool flag = IsExistedHard();
		SetActive(spotRootTransform, UI.OBJ_SELECT_DIFFICULTY, flag);
		if (flag)
		{
			SetActive(spotRootTransform, UI.BTN_CURRENT_DIFFICULTY_NORMAL, currentDifficulty == REGION_DIFFICULTY_TYPE.NORMAL);
			SetActive(spotRootTransform, UI.BTN_CURRENT_DIFFICULTY_HARD, currentDifficulty == REGION_DIFFICULTY_TYPE.HARD);
			UIWidget component = FindCtrl(spotRootTransform, UI.OBJ_SELECT_DIFFICULTY).GetComponent<UIWidget>();
			if (!(component == null))
			{
				this.StartCoroutine(FadeWidget(component, 0, 1, 0.3f));
			}
		}
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

	private void OnQuery_SELECT_DIFFICULTY()
	{
		GameSection.SetEventData(new object[2]
		{
			false,
			true
		});
		if (isInGame)
		{
			GameSection.ChangeEvent("INGAME_SELECT_DIFFICULTY");
		}
	}

	private void OnQuery_RELEASE_REGION()
	{
		releaseRegionId = (int)GameSection.GetEventData();
		uint[] openRegionIdListInWorldMap = MonoBehaviourSingleton<WorldMapManager>.I.GetOpenRegionIdListInWorldMap();
		GameSection.StayEvent();
		if (openRegionIdListInWorldMap.Length <= 1)
		{
			GameSection.ChangeStayEvent("INVALID_RELEASE");
			releaseRegionId = -1;
			GameSection.ResumeEvent(is_resume: true);
		}
		else
		{
			MonoBehaviourSingleton<WorldMapManager>.I.SendRegionCrystalNum(releaseRegionId, delegate(bool isSuccess, string campainText)
			{
				GameSection.ResumeEvent(isSuccess, new object[2]
				{
					MonoBehaviourSingleton<WorldMapManager>.I.releaseCrystalNum.ToString(),
					campainText
				});
			});
		}
	}

	private void OnQuery_WorldMapReleaseRegionDialog_YES()
	{
		if (releaseRegionId >= 0)
		{
			RegionTable.Data data = Singleton<RegionTable>.I.GetData((uint)releaseRegionId);
			GameSection.StayEvent();
			MonoBehaviourSingleton<WorldMapManager>.I.SendRegionOpen(releaseRegionId, delegate(bool isSuccess)
			{
				GameSection.ResumeEvent(isSuccess);
				if (isSuccess)
				{
					ValidRegionInfo validRegionInfo = this.validRegionInfo[releaseRegionId];
					validRegionInfo.status = REGION_STATUS.OPEN;
					validRegionInfo.icon = validRegionInfo.releaseIcon;
					this.StartCoroutine(PlayOpenRegionMap(useReleaseRegion: true));
				}
			});
		}
	}

	private IEnumerator PlayOpenRegionMap(bool useReleaseRegion)
	{
		if (releaseRegionId < 0)
		{
			yield break;
		}
		worldMapCamera.isInteractive = false;
		playingReleaseRegion = true;
		GameSaveData.instance.AddShowedOpenRegionId(releaseRegionId);
		Transform closeBtn = Utility.Find(spots.spotRootTransform, "CLOSE_BTN");
		if (null != closeBtn)
		{
			closeBtn.get_gameObject().SetActive(false);
		}
		Transform worldSelector = FindCtrl(spots.spotRootTransform, UI.OBJ_WORLD_SELECT);
		bool existWorldSelect = worldSelector.get_gameObject().get_activeSelf();
		SetActive(worldSelector, is_visible: false);
		UpdateDifficultyButton(forceOff: true);
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.CAMERA_ACTION, is_disable: true);
		toRegionId = releaseRegionId;
		yield return this.StartCoroutine(InitializeOpenRegion());
		Vector3 to = new Vector3(0f, 0f, 0f);
		RegionTable.Data toData = Singleton<RegionTable>.I.GetData((uint)toRegionId);
		if (toData != null)
		{
			to = toData.iconPos;
		}
		yield return (object)new WaitForSeconds(0.5f);
		Vector3Interpolator ip = new Vector3Interpolator();
		Vector3 zoomDownTo = to + new Vector3(0f, 0f, -3f);
		ip.Set(1f, worldMapCamera.targetPos, zoomDownTo);
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			worldMapCamera.targetPos = ip.Get();
			yield return null;
		}
		Transform toRegion = regionAreas[toRegionId];
		toRegion.get_gameObject().SetActive(true);
		Renderer toRegionRenderer = toRegion.GetComponent<Renderer>();
		toRegionRenderer.get_material().SetFloat("_Alpha", 0f);
		Renderer topRenderer = glowRegionTop.GetComponent<Renderer>();
		topRenderer.get_material().SetFloat("_Alpha", 0f);
		topRenderer.get_material().SetFloat("_AddColor", 1f);
		topRenderer.get_material().SetFloat("_BlendRate", 1f);
		topRenderer.set_sortingOrder(2);
		glowRegionTop.get_gameObject().SetActive(true);
		yield return (object)new WaitForSeconds(1f);
		mapGlowEffectA.get_gameObject().SetActive(true);
		Renderer mapGlowEffectARenderer = mapGlowEffectA.GetComponent<Renderer>();
		mapGlowEffectARenderer.set_sortingOrder(1);
		SpotManager.Spot toSpot = spots.GetSpot(toRegionId);
		toSpot._transform.get_gameObject().SetActive(false);
		RegionTable.Data toRegionData = Singleton<RegionTable>.I.GetData((uint)toRegionId);
		toSpot.ReleaseRegion(toRegionData.regionName, validRegionInfo[toRegionId].releaseIcon.loadedObject as Texture2D, "OPEN_REGION");
		ip.Set(1f, zoomDownTo, to);
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			worldMapCamera.targetPos = ip.Get();
			yield return null;
		}
		FloatInterpolator fip = new FloatInterpolator();
		fip.Set(2f, 0f, 1.5f, null, 0f);
		fip.Play();
		SoundManager.PlayOneShotUISE(SE_ID_SMOKE);
		while (fip.IsPlaying())
		{
			fip.Update();
			topRenderer.get_material().SetFloat("_Alpha", fip.Get());
			yield return null;
		}
		toRegionRenderer.get_material().SetFloat("_Alpha", 1f);
		mapGlowEffectParticleA.Stop();
		mapGlowEffectParticleA.get_gameObject().SetActive(false);
		mapGlowEffectB.get_gameObject().SetActive(true);
		yield return null;
		fip.Set(0.2f, 1f, 0f, null, 0f);
		fip.Play();
		while (fip.IsPlaying())
		{
			fip.Update();
			topRenderer.get_material().SetFloat("_Alpha", fip.Get());
			yield return null;
		}
		yield return null;
		toSpot._transform.get_gameObject().SetActive(true);
		TweenScale tweenScale = toSpot._transform.GetComponent<TweenScale>();
		tweenScale.PlayForward();
		yield return (object)new WaitForSeconds(1f);
		mapGlowEffectParticleB.Stop();
		mapGlowEffectParticleB.get_gameObject().SetActive(false);
		bool isTweenEnd = false;
		telop.get_gameObject().SetActive(true);
		UITweenCtrl tweenCtrl = telop.GetComponent<UITweenCtrl>();
		tweenCtrl.Reset();
		tweenCtrl.Play(forward: true, delegate
		{
			isTweenEnd = true;
		});
		SoundManager.PlayOneShotUISE(SE_ID_LOGO);
		while (!isTweenEnd)
		{
			yield return null;
		}
		mapGlowEffectA.get_gameObject().SetActive(false);
		mapGlowEffectB.get_gameObject().SetActive(false);
		yield return (object)new WaitForSeconds(0.6f);
		telop.get_gameObject().SetActive(false);
		UpdateDifficultyButton();
		if (null != closeBtn)
		{
			closeBtn.get_gameObject().SetActive(true);
		}
		SetActive(worldSelector, existWorldSelect);
		playingReleaseRegion = false;
		worldMapCamera.isInteractive = true;
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.CAMERA_ACTION, is_disable: false);
		if (useReleaseRegion)
		{
			DispatchEvent("SUMMARY_CONFIRM");
			yield break;
		}
		releaseRegionId = -1;
		WorldMapManager.TransferInfo transferInfo = MonoBehaviourSingleton<WorldMapManager>.I.transferInfo;
		if (transferInfo != null)
		{
			if (!transferInfo.nextInGame)
			{
				DispatchEvent("OPEN_REGION", transferInfo.nextRegionId);
				MonoBehaviourSingleton<WorldMapManager>.I.transferInfo = null;
			}
			else
			{
				DispatchEvent("INGAME_MAIN");
				MonoBehaviourSingleton<WorldMapManager>.I.transferInfo = null;
			}
		}
	}

	private void DelayExecute(float delayTime, Action func)
	{
		this.StartCoroutine(DoDelayExecute(delayTime, func));
	}

	private IEnumerator DoDelayExecute(float delayTime, Action func)
	{
		yield return (object)new WaitForSeconds(delayTime);
		func?.Invoke();
	}

	private IEnumerator InitializeOpenRegion()
	{
		if (!regionOpenInitialized)
		{
			LoadingQueue loadQueue = new LoadingQueue(this);
			LoadObject loadedMapGlowEffectA = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "MapGlowEffectA");
			LoadObject loadedMapGlowEffectB = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "MapGlowEffectB");
			LoadObject loadedTelop = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "TelopOpenRegion");
			loadQueue.CacheSE(SE_ID_LOGO);
			loadQueue.CacheSE(SE_ID_SMOKE);
			LoadObject loadedMaterial = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "WorldMapPartGlow" + toRegionId.ToString("D3"));
			if (loadQueue.IsLoading())
			{
				yield return loadQueue.Wait();
			}
			mapGlowEffectA = ResourceUtility.Realizes(loadedMapGlowEffectA.loadedObject, base._transform);
			mapGlowEffectA.get_gameObject().SetActive(false);
			mapGlowEffectParticleA = mapGlowEffectA.GetComponent<ParticleSystem>();
			mapGlowEffectB = ResourceUtility.Realizes(loadedMapGlowEffectB.loadedObject, base._transform);
			mapGlowEffectB.get_gameObject().SetActive(false);
			mapGlowEffectParticleB = mapGlowEffectB.GetComponent<ParticleSystem>();
			if (loadedMaterial != null)
			{
				glowMaterial = (loadedMaterial.loadedObject as Material);
			}
			if (telop == null)
			{
				telop = ResourceUtility.Realizes(loadedTelop.loadedObject, spots.spotRootTransform);
			}
			regionOpenInitialized = true;
		}
		Transform targetArea = regionAreas[toRegionId];
		targetArea.get_gameObject().SetActive(false);
		mapGlowEffectA.SetParent(targetArea);
		mapGlowEffectA.set_localPosition(new Vector3(0f, 0f, 0f));
		mapGlowEffectB.SetParent(targetArea);
		mapGlowEffectB.set_localPosition(new Vector3(0f, 0f, 0f));
		ShapeModule module = mapGlowEffectParticleB.get_shape();
		MeshFilter meshFilter = targetArea.GetComponent<MeshFilter>();
		module.set_mesh(meshFilter.get_sharedMesh());
		glowRegionTop = ResourceUtility.Realizes(targetArea.get_gameObject(), base._transform);
		glowRegionTop.get_gameObject().SetActive(false);
		glowRegionTop.set_localPosition(glowRegionTop.get_localPosition() + new Vector3(0f, 0f, 0.001f));
		glowRegionTop.set_localScale(new Vector3(1.1f, 1.1f, 1.1f));
		glowRegionTop.GetComponent<Renderer>().set_material(glowMaterial);
	}

	private void OnQuery_WorldMapReleaseRegionDialog_NO()
	{
		releaseRegionId = -1;
	}

	private void OnQuery_WorldMapSummaryConfirmDialog_YES()
	{
		GameSection.SetEventData(releaseRegionId);
		releaseRegionId = -1;
	}

	private void OnQuery_WorldMapSummaryConfirmDialog_NO()
	{
		releaseRegionId = -1;
	}

	private void Update()
	{
		if (worldMapCamera == null)
		{
			return;
		}
		if (chapterScrollView != null)
		{
			if (chapterScrollView.isPressing)
			{
				worldMapCamera.isInteractive = false;
				beforePressed = chapterScrollView.isPressing;
				return;
			}
			if (beforePressed != chapterScrollView.isPressing)
			{
				worldMapCamera.isInteractive = true;
				beforePressed = chapterScrollView.isPressing;
			}
		}
		if (playingReleaseRegion)
		{
			worldMapCamera.isInteractive = false;
			return;
		}
		string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
		if (currentSectionName != beforeSectionName)
		{
			bool isInteractive = currentSectionName == "WorldMap" || currentSectionName == "RegionMap";
			worldMapCamera.isInteractive = isInteractive;
			beforeSectionName = currentSectionName;
		}
	}
}
