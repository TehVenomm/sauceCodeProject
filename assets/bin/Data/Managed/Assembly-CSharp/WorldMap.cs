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

	private const int CHAPTER_CONTENT_NUMBER = 4;

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
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		isInGame = (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene");
		this.StartCoroutine("DoInitialize");
	}

	private IEnumerator DoInitialize()
	{
		LoadingQueue loadQueue = new LoadingQueue(this);
		LoadObject loadedWorldMap = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "WorldMap", false);
		LoadObject loadedRegionSpotRoot = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionSpotRoot", false);
		LoadObject loadedRegionSpot = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionSpot", false);
		LoadObject loadedFilterCamera = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ZoomBlurFilterCamera", false);
		LoadObject loadedPlayerMarker = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "PlayerMarker", false);
		uint[] openedRegionIds = MonoBehaviourSingleton<WorldMapManager>.I.GetOpenRegionIdListInWorldMap();
		uint[] validRegionIds = MonoBehaviourSingleton<WorldMapManager>.I.GetValidRegionIdListInWorldMap();
		if (MonoBehaviourSingleton<WorldMapManager>.I.releaseRegionIdfromBoard > 0)
		{
			releaseRegionId = MonoBehaviourSingleton<WorldMapManager>.I.releaseRegionIdfromBoard;
		}
		if (releaseRegionId < 0)
		{
			uint[] array = openedRegionIds;
			foreach (uint id in array)
			{
				if (!MonoBehaviourSingleton<WorldMapManager>.I.IsShowedOpenRegion((int)id))
				{
					releaseRegionId = (int)id;
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
		for (int k = 0; k < validRegionIds.Length; k++)
		{
			RegionTable.Data data = Singleton<RegionTable>.I.GetData(validRegionIds[k]);
			if (!data.hasParentRegion())
			{
				string iconName = passedRegionIcon;
				REGION_STATUS regionStatus = REGION_STATUS.OPEN;
				int opened = Array.IndexOf(openedRegionIds, validRegionIds[k]);
				if (opened < 0)
				{
					iconName = closeRegionIcon;
					regionStatus = REGION_STATUS.CLOSE;
				}
				else if (!MonoBehaviourSingleton<WorldMapManager>.I.IsAllOpenedMap((int)data.regionId))
				{
					iconName = newRegionIcon;
				}
				LoadObject loadedObj = loadQueue.Load(RESOURCE_CATEGORY.REGION_ICON, iconName, false);
				LoadObject releaseIconObj = loadQueue.Load(RESOURCE_CATEGORY.REGION_ICON, newRegionIcon, false);
				validRegionInfo[k] = new ValidRegionInfo(data, loadedObj, releaseIconObj, regionStatus);
				if (k != 0)
				{
					regionAreaLOs[k] = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "WorldMapPart" + validRegionIds[k].ToString("D3"), false);
				}
			}
		}
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		worldMapUIRoot = ResourceUtility.Realizes(loadedWorldMap.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform, -1).get_gameObject();
		worldMapCamera = worldMapUIRoot.get_transform().Find("Camera").GetComponent<WorldMapCameraController>();
		GameObject map3 = worldMapUIRoot.get_transform().FindChild("Map").get_gameObject();
		Transform map2 = worldMapUIRoot.get_transform().FindChild("Map2");
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
			LoadObject world3 = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, ResourceName.GetChapterImageName(1), false);
			LoadObject world2 = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, ResourceName.GetChapterImageName(2), false);
			if (loadQueue.IsLoading())
			{
				yield return (object)loadQueue.Wait();
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
		}
		worldMaps = (GameObject[])new GameObject[2]
		{
			map3.get_gameObject(),
			worldMap2Object
		};
		playerMarker = ResourceUtility.Realizes(loadedPlayerMarker.loadedObject, base._transform, -1);
		playerMarker.get_gameObject().SetActive(false);
		regionAreas = (Transform[])new Transform[regionAreaLOs.Length];
		for (int j = 0; j < regionAreaLOs.Length; j++)
		{
			if (!(worldMaps[validRegionInfo[j].data.worldId - 1] == null))
			{
				Transform worldMapTransform = worldMaps[validRegionInfo[j].data.worldId - 1].get_transform();
				LoadObject areaLO = regionAreaLOs[j];
				if (areaLO != null && null != areaLO.loadedObject)
				{
					regionAreas[j] = ResourceUtility.Realizes(areaLO.loadedObject, worldMapTransform, -1);
					regionAreas[j].get_gameObject().SetActive(false);
				}
			}
		}
		for (int i = 0; i < worldMaps.Length; i++)
		{
			worldMaps[i].SetActive(i == currentWorldIndex);
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
		SetActive(spotRootTransform, UI.SPR_INACTIVE_ARROW_L, false);
		SetActive(spotRootTransform, UI.OBJ_ACTIVE_ARROW_L, true);
		SetActive(spotRootTransform, UI.SPR_INACTIVE_ARROW_R, false);
		SetActive(spotRootTransform, UI.OBJ_ACTIVE_ARROW_R, true);
	}

	public void InitRegionInfo()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Expected O, but got Unknown
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		if (spots != null)
		{
			Transform spotRootTransform = spots.spotRootTransform;
			if (uiMapSprite == null)
			{
				uiMapSprite = spotRootTransform.FindChild("Map").get_gameObject().GetComponent<UITexture>();
			}
			if (mapTween == null)
			{
				mapTween = spotRootTransform.FindChild("Map").get_gameObject().GetComponent<TweenAlpha>();
			}
			InitMapSprite(false);
			if (currentWorldIndex >= 0)
			{
				worldMaps[currentWorldIndex].SetActive(true);
			}
			for (int i = 0; i < validRegionInfo.Length; i++)
			{
				RegionTable.Data data = validRegionInfo[i].data;
				if (data != null && currentWorldIndex == data.worldId - 1 && currentDifficulty == data.difficulty)
				{
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
					SpotManager.Spot spot = spots.AddSpot((int)data.regionId, regionName, data.iconPos, SpotManager.ICON_TYPE.CLEARED, event_name, false, false, false, (int)data.regionId, null, false, SpotManager.HAPPEN_CONDITION.NONE, mapNo);
					spot.SetIconSprite("SPR_ICON", validRegionInfo[i].icon.loadedObject as Texture2D, (int)data.iconSize.x, (int)data.iconSize.y);
					if (currentRegionID == data.regionId && isInWorldMap)
					{
						playerMarker.get_gameObject().SetActive(true);
						playerMarker.SetParent(worldMaps[currentWorldIndex].get_transform());
						PlayerMarker component = playerMarker.GetComponent<PlayerMarker>();
						component.SetWorldMode(true);
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
		}
	}

	private void InitMapSprite(bool isPortrait)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
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

	protected unsafe override void OnOpen()
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Expected O, but got Unknown
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
		Transform val = spots.spotRootTransform.FindChild("CLOSE_BTN/OBJ_CLOSE_BTN_ROOT");
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
		_003COnOpen_003Ec__AnonStorey4C2 _003COnOpen_003Ec__AnonStorey4C;
		FadeInMap(new Action((object)_003COnOpen_003Ec__AnonStorey4C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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
			uITweenCtrl.Play(true, onComplete);
		}
	}

	protected override void OnDestroy()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
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

	public unsafe void OnQuery_OPEN_REGION()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		UpdateDifficultyButton(true);
		int id = (int)GameSection.GetEventData();
		blurCenter = new Vector2(0.5f, 0.5f);
		SpotManager.Spot spot = spots.FindSpot(id);
		if (spot != null)
		{
			blurCenter = spot.GetScreenPos();
		}
		PlayTween(TWEEN_ANIMATION.ENDING, null);
		GameSection.StayEvent();
		blurFilter.CacheRenderTarget(new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), false);
	}

	public void OnQuery_OPEN_REGION_CHANGE()
	{
		UpdateDifficultyButton(true);
		this.StopAllCoroutines();
		spots.ClearAllSpot();
		DisableWorldMapObject();
	}

	public void OnQuery_DIRECT_REGION()
	{
		UpdateDifficultyButton(true);
		this.StopAllCoroutines();
		GameSection.SetEventData(-1);
		spots.ClearAllSpot();
	}

	public void OnQuery_DIRECT_EVENT()
	{
		UpdateDifficultyButton(true);
		this.StopAllCoroutines();
		int eventMapRegionID = (int)GameSection.GetEventData();
		MonoBehaviourSingleton<WorldMapManager>.I.eventMapRegionID = eventMapRegionID;
		GameSection.SetEventData(-4);
		spots.ClearAllSpot();
	}

	public void OnQuery_DIRECT_REGION_QUEST()
	{
		UpdateDifficultyButton(true);
		this.StopAllCoroutines();
		GameSection.SetEventData(-2);
		spots.ClearAllSpot();
	}

	public void OnQuery_DIRECT_REGION_TUTORIAL()
	{
		UpdateDifficultyButton(true);
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
			GameSection.ResumeEvent(true, null, false);
		});
	}

	public unsafe void FadeInMap(Action onComplete)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		if (currentWorldIndex >= 0 && worldMaps[currentWorldIndex] != null)
		{
			worldMaps[currentWorldIndex].SetActive(true);
		}
		if (uiMapSprite != null)
		{
			uiMapSprite.get_gameObject().SetActive(true);
		}
		_003CFadeInMap_003Ec__AnonStorey4C4 _003CFadeInMap_003Ec__AnonStorey4C;
		this.StartCoroutine(DoFadeMap(0f, 1f, 0.4f, new Action((object)_003CFadeInMap_003Ec__AnonStorey4C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
	}

	public void DisableWorldMapObject()
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
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
		if (currentWorldIndex >= 0 && !(worldMaps[currentWorldIndex] == null))
		{
			Renderer r = worldMaps[currentWorldIndex].GetComponentInChildren<Renderer>();
			if (!(r == null))
			{
				Renderer[] areaRenderers = (Renderer[])new Renderer[regionAreas.Length];
				for (int j = 0; j < areaRenderers.Length; j++)
				{
					if (null != regionAreas[j])
					{
						Renderer ren = regionAreas[j].GetComponent<Renderer>();
						if (null != ren)
						{
							ren.get_material().SetFloat("_Alpha", 0f);
						}
						areaRenderers[j] = ren;
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
					yield return (object)null;
				}
				r.get_material().SetFloat("_Alpha", to);
				timer2 = 0f;
				for (float alphaTime = 0.15f; timer2 <= alphaTime; timer2 += Time.get_deltaTime())
				{
					float alpha2 = Mathf.Lerp(0f, 1.2f, timer2 / alphaTime);
					for (int i = 0; i < areaRenderers.Length; i++)
					{
						if (null != areaRenderers[i])
						{
							areaRenderers[i].get_material().SetFloat("_Alpha", alpha2);
						}
					}
					yield return (object)null;
				}
				if (onComplete != null)
				{
					onComplete.Invoke();
				}
			}
		}
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

	public unsafe void EnterRegionMapEvent(Action onCompleteFilter)
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		if (blurFilter == null)
		{
			if (onCompleteFilter != null)
			{
				onCompleteFilter.Invoke();
			}
		}
		else
		{
			DisableWorldMapObject();
			_003CEnterRegionMapEvent_003Ec__AnonStorey4C5 _003CEnterRegionMapEvent_003Ec__AnonStorey4C;
			blurFilter.StartBlurFilter(0f, 0.25f, 0.25f, blurCenter, new Action((object)_003CEnterRegionMapEvent_003Ec__AnonStorey4C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	private void OnQuery_NEXT_WORLD()
	{
		if (!isChangingMap)
		{
			UpdateDifficultyButton(true);
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
			UpdateDifficultyButton(true);
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

	private unsafe void ChangeActiveArea()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		UpdateAreas();
		this.StartCoroutine(DoFadeMap(0f, 1f, 0.4f, new Action((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/)));
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
			REGION_DIFFICULTY_TYPE rEGION_DIFFICULTY_TYPE = (REGION_DIFFICULTY_TYPE)(int)eventData;
			if (rEGION_DIFFICULTY_TYPE != currentDifficulty)
			{
				currentDifficulty = rEGION_DIFFICULTY_TYPE;
				UpdateDifficultyButton(false);
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
			REGION_DIFFICULTY_TYPE rEGION_DIFFICULTY_TYPE = (REGION_DIFFICULTY_TYPE)(int)eventData;
			if (rEGION_DIFFICULTY_TYPE != currentDifficulty)
			{
				currentDifficulty = rEGION_DIFFICULTY_TYPE;
				UpdateDifficultyButton(false);
				spots.ClearAllSpot();
				InitRegionInfo();
				UpdateAreas();
			}
		}
	}

	private void UpdateAreas()
	{
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		if (regionAreas != null)
		{
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
	}

	private void UpdateDifficultyButton(bool forceOff = false)
	{
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		Transform val = null;
		Transform spotRootTransform = spots.spotRootTransform;
		if (forceOff)
		{
			SetActive(spotRootTransform, UI.OBJ_SELECT_DIFFICULTY, false);
		}
		else
		{
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
	}

	private IEnumerator FadeWidget(UIWidget target, int start, int end, float duration)
	{
		float time = 0f;
		while (time < duration)
		{
			time += Time.get_deltaTime();
			float prog = time / duration;
			float a = target.alpha = Mathf.Lerp((float)start, (float)end, prog);
			yield return (object)null;
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
			GameSection.ChangeEvent("INGAME_SELECT_DIFFICULTY", null);
		}
	}

	private unsafe void OnQuery_RELEASE_REGION()
	{
		releaseRegionId = (int)GameSection.GetEventData();
		uint[] openRegionIdListInWorldMap = MonoBehaviourSingleton<WorldMapManager>.I.GetOpenRegionIdListInWorldMap();
		GameSection.StayEvent();
		if (openRegionIdListInWorldMap.Length <= 1)
		{
			GameSection.ChangeStayEvent("INVALID_RELEASE", null);
			GameSection.ResumeEvent(true, null, false);
			releaseRegionId = -1;
		}
		else
		{
			WorldMapManager i = MonoBehaviourSingleton<WorldMapManager>.I;
			int regionId = releaseRegionId;
			if (_003C_003Ef__am_0024cache28 == null)
			{
				_003C_003Ef__am_0024cache28 = new Action<bool, string>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			i.SendRegionCrystalNum(regionId, _003C_003Ef__am_0024cache28);
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
				//IL_0037: Unknown result type (might be due to invalid IL or missing references)
				GameSection.ResumeEvent(isSuccess, null, false);
				if (isSuccess)
				{
					ValidRegionInfo validRegionInfo = this.validRegionInfo[releaseRegionId];
					validRegionInfo.status = REGION_STATUS.OPEN;
					validRegionInfo.icon = validRegionInfo.releaseIcon;
					this.StartCoroutine(PlayOpenRegionMap(true));
				}
			});
		}
	}

	private IEnumerator PlayOpenRegionMap(bool useReleaseRegion)
	{
		if (releaseRegionId >= 0)
		{
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
			SetActive(worldSelector, false);
			UpdateDifficultyButton(true);
			MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.CAMERA_ACTION, true);
			toRegionId = releaseRegionId;
			yield return (object)this.StartCoroutine(InitializeOpenRegion());
			Vector3 to = new Vector3(0f, 0f, 0f);
			RegionTable.Data toData = Singleton<RegionTable>.I.GetData((uint)toRegionId);
			if (toData != null)
			{
				to = toData.iconPos;
			}
			yield return (object)new WaitForSeconds(0.5f);
			Vector3Interpolator ip = new Vector3Interpolator();
			Vector3 zoomDownTo = to + new Vector3(0f, 0f, -3f);
			ip.Set(1f, worldMapCamera.targetPos, zoomDownTo, null, default(Vector3), null);
			ip.Play();
			while (ip.IsPlaying())
			{
				ip.Update();
				worldMapCamera.targetPos = ip.Get();
				yield return (object)null;
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
			ip.Set(1f, zoomDownTo, to, null, default(Vector3), null);
			ip.Play();
			while (ip.IsPlaying())
			{
				ip.Update();
				worldMapCamera.targetPos = ip.Get();
				yield return (object)null;
			}
			FloatInterpolator fip = new FloatInterpolator();
			fip.Set(2f, 0f, 1.5f, null, 0f, null);
			fip.Play();
			SoundManager.PlayOneShotUISE(SE_ID_SMOKE);
			while (fip.IsPlaying())
			{
				fip.Update();
				topRenderer.get_material().SetFloat("_Alpha", fip.Get());
				yield return (object)null;
			}
			toRegionRenderer.get_material().SetFloat("_Alpha", 1f);
			mapGlowEffectParticleA.Stop();
			mapGlowEffectParticleA.get_gameObject().SetActive(false);
			mapGlowEffectB.get_gameObject().SetActive(true);
			yield return (object)null;
			fip.Set(0.2f, 1f, 0f, null, 0f, null);
			fip.Play();
			while (fip.IsPlaying())
			{
				fip.Update();
				topRenderer.get_material().SetFloat("_Alpha", fip.Get());
				yield return (object)null;
			}
			yield return (object)null;
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
			tweenCtrl.Play(true, delegate
			{
				((_003CPlayOpenRegionMap_003Ec__Iterator1A2)/*Error near IL_0740: stateMachine*/)._003CisTweenEnd_003E__15 = true;
			});
			SoundManager.PlayOneShotUISE(SE_ID_LOGO);
			while (!isTweenEnd)
			{
				yield return (object)null;
			}
			mapGlowEffectA.get_gameObject().SetActive(false);
			mapGlowEffectB.get_gameObject().SetActive(false);
			yield return (object)new WaitForSeconds(0.6f);
			telop.get_gameObject().SetActive(false);
			UpdateDifficultyButton(false);
			if (null != closeBtn)
			{
				closeBtn.get_gameObject().SetActive(true);
			}
			SetActive(worldSelector, existWorldSelect);
			playingReleaseRegion = false;
			worldMapCamera.isInteractive = true;
			MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.CAMERA_ACTION, false);
			if (useReleaseRegion)
			{
				DispatchEvent("SUMMARY_CONFIRM", null);
			}
			else
			{
				releaseRegionId = -1;
				WorldMapManager.TransferInfo info = MonoBehaviourSingleton<WorldMapManager>.I.transferInfo;
				if (info != null)
				{
					if (!info.nextInGame)
					{
						DispatchEvent("OPEN_REGION", info.nextRegionId);
						MonoBehaviourSingleton<WorldMapManager>.I.transferInfo = null;
					}
					else
					{
						DispatchEvent("INGAME_MAIN", null);
						MonoBehaviourSingleton<WorldMapManager>.I.transferInfo = null;
					}
				}
			}
		}
	}

	private void DelayExecute(float delayTime, Action func)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoDelayExecute(delayTime, func));
	}

	private IEnumerator DoDelayExecute(float delayTime, Action func)
	{
		yield return (object)new WaitForSeconds(delayTime);
		if (func != null)
		{
			func.Invoke();
		}
	}

	private IEnumerator InitializeOpenRegion()
	{
		if (!regionOpenInitialized)
		{
			LoadingQueue loadQueue = new LoadingQueue(this);
			LoadObject loadedMapGlowEffectA = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "MapGlowEffectA", false);
			LoadObject loadedMapGlowEffectB = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "MapGlowEffectB", false);
			LoadObject loadedTelop = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "TelopOpenRegion", false);
			loadQueue.CacheSE(SE_ID_LOGO, null);
			loadQueue.CacheSE(SE_ID_SMOKE, null);
			LoadObject loadedMaterial = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "WorldMapPartGlow" + toRegionId.ToString("D3"), false);
			if (loadQueue.IsLoading())
			{
				yield return (object)loadQueue.Wait();
			}
			mapGlowEffectA = ResourceUtility.Realizes(loadedMapGlowEffectA.loadedObject, base._transform, -1);
			mapGlowEffectA.get_gameObject().SetActive(false);
			mapGlowEffectParticleA = mapGlowEffectA.GetComponent<ParticleSystem>();
			mapGlowEffectB = ResourceUtility.Realizes(loadedMapGlowEffectB.loadedObject, base._transform, -1);
			mapGlowEffectB.get_gameObject().SetActive(false);
			mapGlowEffectParticleB = mapGlowEffectB.GetComponent<ParticleSystem>();
			if (loadedMaterial != null)
			{
				glowMaterial = (loadedMaterial.loadedObject as Material);
			}
			if (telop == null)
			{
				telop = ResourceUtility.Realizes(loadedTelop.loadedObject, spots.spotRootTransform, -1);
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
		glowRegionTop = ResourceUtility.Realizes(targetArea.get_gameObject(), base._transform, -1);
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
		if (!(worldMapCamera == null))
		{
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
			}
			else
			{
				string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
				if (currentSectionName != beforeSectionName)
				{
					bool isInteractive = currentSectionName == "WorldMap" || currentSectionName == "RegionMap";
					worldMapCamera.isInteractive = isInteractive;
					beforeSectionName = currentSectionName;
				}
			}
		}
	}
}
