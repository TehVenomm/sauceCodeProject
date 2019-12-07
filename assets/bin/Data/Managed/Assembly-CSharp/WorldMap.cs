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

	private string beforeSectionName = "";

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
		StartCoroutine("DoInitialize");
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
		uint[] array = MonoBehaviourSingleton<WorldMapManager>.I.GetOpenRegionIdListInWorldMap();
		uint[] array2 = MonoBehaviourSingleton<WorldMapManager>.I.GetValidRegionIdListInWorldMap();
		if (MonoBehaviourSingleton<WorldMapManager>.I.releaseRegionIdfromBoard > 0)
		{
			releaseRegionId = MonoBehaviourSingleton<WorldMapManager>.I.releaseRegionIdfromBoard;
		}
		if (releaseRegionId < 0)
		{
			uint[] array3 = array;
			foreach (uint regionId in array3)
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
		if (array.Length == 0)
		{
			array = new uint[1];
		}
		if (array2.Length == 0)
		{
			array2 = new uint[1];
		}
		LoadObject[] regionAreaLOs = new LoadObject[array2.Length];
		string regionIcon = ResourceName.GetRegionIcon(0);
		string regionIcon2 = ResourceName.GetRegionIcon(1);
		string regionIcon3 = ResourceName.GetRegionIcon(2);
		validRegionInfo = new ValidRegionInfo[array2.Length];
		for (int j = 0; j < array2.Length; j++)
		{
			RegionTable.Data data = Singleton<RegionTable>.I.GetData(array2[j]);
			if (data.hasParentRegion())
			{
				continue;
			}
			string resource_name = regionIcon2;
			REGION_STATUS status = REGION_STATUS.OPEN;
			if (Array.IndexOf(array, array2[j]) < 0)
			{
				resource_name = regionIcon3;
				status = REGION_STATUS.CLOSE;
			}
			else
			{
				EventNormalListData eventNormalListData = MonoBehaviourSingleton<DeliveryManager>.I.GetEventNormalListData((int)data.regionId);
				if (eventNormalListData != null && eventNormalListData.numerator < eventNormalListData.denominator)
				{
					resource_name = regionIcon;
				}
			}
			LoadObject icon = loadQueue.Load(RESOURCE_CATEGORY.REGION_ICON, resource_name);
			LoadObject releaseIcon = loadQueue.Load(RESOURCE_CATEGORY.REGION_ICON, regionIcon);
			validRegionInfo[j] = new ValidRegionInfo(data, icon, releaseIcon, status);
			if (j != 0)
			{
				regionAreaLOs[j] = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "WorldMapPart" + array2[j].ToString("D3"));
			}
		}
		if (loadQueue.IsLoading())
		{
			yield return loadQueue.Wait();
		}
		worldMapUIRoot = ResourceUtility.Realizes(loadedWorldMap.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform).gameObject;
		worldMapCamera = worldMapUIRoot.transform.Find("Camera").GetComponent<WorldMapCameraController>();
		GameObject map = worldMapUIRoot.transform.Find("Map").gameObject;
		Transform transform = worldMapUIRoot.transform.Find("Map2");
		spots = new SpotManager(loadedRegionSpotRoot.loadedObject as GameObject, loadedRegionSpot.loadedObject as GameObject, worldMapCamera._camera);
		spots.CreateSpotRoot();
		spots.SetRoot(base._transform);
		tweenAnimations = spots.spotRootTransform.GetComponentsInChildren<UITweenCtrl>();
		blurFilter = (ResourceUtility.Instantiate(loadedFilterCamera.loadedObject) as GameObject).GetComponent<ZoomBlurFilter>();
		UIPanel component = spots.spotRootTransform.GetComponent<UIPanel>();
		if (component != null)
		{
			component.depth = base.baseDepth + 1;
		}
		SetSelectorDepth(spots.spotRootTransform, component.depth);
		currentRegionID = 0;
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
		if (fieldMapData != null)
		{
			currentRegionID = (int)fieldMapData.regionId;
		}
		RegionTable.Data data2 = (releaseRegionId <= 0) ? Singleton<RegionTable>.I.GetData((uint)currentRegionID) : Singleton<RegionTable>.I.GetData((uint)releaseRegionId);
		if (data2 != null)
		{
			currentWorldIndex = Mathf.Max(0, data2.worldId - 1);
			currentDifficulty = data2.difficulty;
		}
		else
		{
			currentWorldIndex = 0;
			currentDifficulty = REGION_DIFFICULTY_TYPE.NORMAL;
		}
		GameObject worldMap2Object = null;
		Transform worldSelect = FindCtrl(spots.spotRootTransform, UI.OBJ_WORLD_SELECT);
		if (transform == null)
		{
			worldSelect.gameObject.SetActive(value: false);
		}
		else
		{
			worldMap2Object = transform.gameObject;
			transform.gameObject.SetActive(value: false);
			LoadObject world3 = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, ResourceName.GetChapterImageName(1));
			LoadObject world2 = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, ResourceName.GetChapterImageName(2));
			if (loadQueue.IsLoading())
			{
				yield return loadQueue.Wait();
			}
			Transform spotRootTransform = spots.spotRootTransform;
			chapterContentList = new List<Transform>();
			chapterContentList.Add(FindCtrl(spotRootTransform, UI.SPR_CONTENT1));
			chapterContentList.Add(FindCtrl(spotRootTransform, UI.SPR_CONTENT2));
			chapterContentList.Add(FindCtrl(spotRootTransform, UI.SPR_CONTENT3));
			chapterContentList.Add(FindCtrl(spotRootTransform, UI.SPR_CONTENT4));
			currentCenterIndex = 0;
			SetupChapterContentTexture(spotRootTransform, world3.loadedObject as Texture2D, world2.loadedObject as Texture2D);
			center = FindCtrl(spotRootTransform, UI.OBJ_WRAP_CENTER).GetComponent<UICenterOnChild>();
			center.onCenter = DragChapter;
			chapterScrollView = FindCtrl(spotRootTransform, UI.SCR_SELECTOR).GetComponent<UIScrollView>();
			UIWidget component2 = worldSelect.GetComponent<UIWidget>();
			SyncWorldMapSelectAntors(component2);
		}
		worldMaps = new GameObject[2]
		{
			map.gameObject,
			worldMap2Object
		};
		playerMarker = ResourceUtility.Realizes(loadedPlayerMarker.loadedObject, base._transform);
		playerMarker.gameObject.SetActive(value: false);
		regionAreas = new Transform[regionAreaLOs.Length];
		for (int k = 0; k < regionAreaLOs.Length; k++)
		{
			if (!(worldMaps[validRegionInfo[k].data.worldId - 1] == null))
			{
				Transform transform2 = worldMaps[validRegionInfo[k].data.worldId - 1].transform;
				LoadObject loadObject = regionAreaLOs[k];
				if (loadObject != null && null != loadObject.loadedObject)
				{
					regionAreas[k] = ResourceUtility.Realizes(loadObject.loadedObject, transform2);
					regionAreas[k].gameObject.SetActive(value: false);
				}
			}
		}
		for (int l = 0; l < worldMaps.Length; l++)
		{
			worldMaps[l].SetActive(l == currentWorldIndex);
		}
		isInWorldMap = FieldManager.IsInWorldMap(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
		bool flag = MonoBehaviourSingleton<WorldMapManager>.I.IsExistedWorld2();
		SetActive(spots.spotRootTransform, UI.OBJ_WORLD_SELECT, flag);
		if (flag)
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
			if (chapterContentList[i].name == go.name)
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
		if (spots == null)
		{
			return;
		}
		Transform spotRootTransform = spots.spotRootTransform;
		if (uiMapSprite == null)
		{
			uiMapSprite = spotRootTransform.Find("Map").gameObject.GetComponent<UITexture>();
		}
		if (mapTween == null)
		{
			mapTween = spotRootTransform.Find("Map").gameObject.GetComponent<TweenAlpha>();
		}
		InitMapSprite(isPortrait: false);
		if (currentWorldIndex >= 0)
		{
			worldMaps[currentWorldIndex].SetActive(value: true);
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
				playerMarker.gameObject.SetActive(value: true);
				playerMarker.SetParent(worldMaps[currentWorldIndex].transform);
				PlayerMarker component = playerMarker.GetComponent<PlayerMarker>();
				component.SetWorldMode(enable: true);
				component.SetCamera(worldMapCamera._camera.transform);
				playerMarker.localPosition = data.markerPos;
			}
			if (releaseRegionId == (int)data.regionId)
			{
				spot._transform.gameObject.SetActive(value: false);
				if (isInWorldMap)
				{
					playerMarker.gameObject.SetActive(value: false);
				}
			}
		}
	}

	private void InitMapSprite(bool isPortrait)
	{
		if (uiMapSprite != null)
		{
			if (null == worldMapCamera._camera.targetTexture)
			{
				worldMapCamera.Restore();
			}
			uiMapSprite.mainTexture = worldMapCamera._camera.targetTexture;
			uiMapSprite.width = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth;
			uiMapSprite.height = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight;
		}
	}

	protected override void OnOpen()
	{
		if (currentWorldIndex >= 0)
		{
			worldMaps[currentWorldIndex].SetActive(value: true);
		}
		if (base.transform.localScale.y > 1f)
		{
			base.transform.localScale = Vector3.one;
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
		Transform transform = spots.spotRootTransform.Find("CLOSE_BTN/OBJ_CLOSE_BTN_ROOT");
		UIWidget widget = null;
		if (transform != null)
		{
			widget = transform.GetComponent<UIWidget>();
			if (widget != null)
			{
				widget.alpha = 0f;
				widget.transform.localScale = Vector3.zero;
			}
		}
		Transform transform2 = FindCtrl(spots.spotRootTransform, UI.OBJ_WORLD_SELECT);
		UIWidget selectWidget = null;
		if (transform2 != null)
		{
			selectWidget = transform2.GetComponent<UIWidget>();
			if (selectWidget != null)
			{
				selectWidget.alpha = 0f;
			}
			selectWidget.gameObject.SetActive(value: false);
		}
		FadeInMap(delegate
		{
			selectWidget.gameObject.SetActive(value: true);
			InitRegionInfo();
			if (widget != null)
			{
				widget.alpha = 0f;
				widget.transform.localScale = Vector3.zero;
			}
			if (selectWidget != null)
			{
				selectWidget.alpha = 0f;
			}
			PlayTween(TWEEN_ANIMATION.OPENING);
			UpdateAreas();
			UpdateDifficultyButton();
			Singleton<RegionTable>.I.GetData((uint)releaseRegionId);
			if (!MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent() && GameSceneEvent.request == null && releaseRegionId > 0)
			{
				bool useReleaseRegion = false;
				if (MonoBehaviourSingleton<WorldMapManager>.I.releaseRegionIdfromBoard > 0)
				{
					useReleaseRegion = true;
					MonoBehaviourSingleton<WorldMapManager>.I.releaseRegionIdfromBoard = 0;
				}
				StartCoroutine(PlayOpenRegionMap(useReleaseRegion));
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
			UnityEngine.Object.Destroy(blurFilter.gameObject);
		}
		if (worldMapUIRoot != null)
		{
			UnityEngine.Object.Destroy(worldMapUIRoot);
		}
		GameObject[] array = worldMaps;
		foreach (GameObject gameObject in array)
		{
			if (gameObject != null)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
		base.OnDestroy();
	}

	public void OnQuery_OPEN_REGION()
	{
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
			playerMarker.gameObject.SetActive(value: false);
			playerMarker.SetParent(base._transform);
			spots.ClearAllSpot();
			GameSection.ResumeEvent(is_resume: true);
		});
	}

	public void OnQuery_OPEN_REGION_CHANGE()
	{
		UpdateDifficultyButton(forceOff: true);
		StopAllCoroutines();
		spots.ClearAllSpot();
		DisableWorldMapObject();
	}

	public void OnQuery_DIRECT_REGION()
	{
		UpdateDifficultyButton(forceOff: true);
		StopAllCoroutines();
		GameSection.SetEventData(-1);
		spots.ClearAllSpot();
	}

	public void OnQuery_DIRECT_EVENT()
	{
		UpdateDifficultyButton(forceOff: true);
		StopAllCoroutines();
		int eventMapRegionID = (int)GameSection.GetEventData();
		MonoBehaviourSingleton<WorldMapManager>.I.eventMapRegionID = eventMapRegionID;
		GameSection.SetEventData(-4);
		spots.ClearAllSpot();
	}

	public void OnQuery_DIRECT_REGION_QUEST()
	{
		UpdateDifficultyButton(forceOff: true);
		StopAllCoroutines();
		GameSection.SetEventData(-2);
		spots.ClearAllSpot();
	}

	public void OnQuery_DIRECT_REGION_TUTORIAL()
	{
		UpdateDifficultyButton(forceOff: true);
		StopAllCoroutines();
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
				UnityEngine.Object.Destroy(worldMapUIRoot);
			}
			GameObject[] array = worldMaps;
			foreach (GameObject gameObject in array)
			{
				if (gameObject != null)
				{
					gameObject.SetActive(value: false);
				}
			}
			GameSection.ResumeEvent(is_resume: true);
		});
	}

	public void FadeInMap(Action onComplete)
	{
		if (currentWorldIndex >= 0 && worldMaps[currentWorldIndex] != null)
		{
			worldMaps[currentWorldIndex].SetActive(value: true);
		}
		if (uiMapSprite != null)
		{
			uiMapSprite.gameObject.SetActive(value: true);
		}
		StartCoroutine(DoFadeMap(0f, 1f, 0.4f, delegate
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
		foreach (GameObject gameObject in array)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(value: false);
			}
		}
		if (uiMapSprite != null)
		{
			uiMapSprite.gameObject.SetActive(value: false);
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
		Renderer[] areaRenderers = new Renderer[regionAreas.Length];
		for (int i = 0; i < areaRenderers.Length; i++)
		{
			if (null != regionAreas[i])
			{
				Renderer component = regionAreas[i].GetComponent<Renderer>();
				if (null != component)
				{
					component.material.SetFloat("_Alpha", 0f);
				}
				areaRenderers[i] = component;
			}
		}
		float timer2;
		for (timer2 = 0f; timer2 < time; timer2 += Time.deltaTime)
		{
			if (null == r)
			{
				yield break;
			}
			float value = Mathf.Lerp(from, to, timer2 / time);
			r.material.SetFloat("_Alpha", value);
			yield return null;
		}
		r.material.SetFloat("_Alpha", to);
		timer2 = 0f;
		for (float alphaTime = 0.15f; timer2 <= alphaTime; timer2 += Time.deltaTime)
		{
			float value2 = Mathf.Lerp(0f, 1.2f, timer2 / alphaTime);
			for (int j = 0; j < areaRenderers.Length; j++)
			{
				if (null != areaRenderers[j])
				{
					areaRenderers[j].material.SetFloat("_Alpha", value2);
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
				blurFilter.SetBlurPram(0f, blurCenter);
				onCompleteFilter();
				if (regionAreas != null)
				{
					for (int i = 0; i < regionAreas.Length; i++)
					{
						if (null != regionAreas[i])
						{
							regionAreas[i].gameObject.SetActive(value: false);
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
		mapTween.enabled = true;
		mapTween.ResetToBeginning();
		mapTween.PlayForward();
		yield return new WaitForSeconds(mapTween.duration / 2f);
		for (int i = 0; i < worldMaps.Length; i++)
		{
			worldMaps[i].SetActive(i == currentWorldIndex);
		}
		Animator component = worldMaps[currentWorldIndex].GetComponent<Animator>();
		int stateNameHash = Animator.StringToHash("Take 001");
		component.Play(stateNameHash, 0, 1f);
	}

	private void ChangeActiveArea()
	{
		UpdateAreas();
		StartCoroutine(DoFadeMap(0f, 1f, 0.4f, delegate
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
					regionAreas[i].gameObject.SetActive(value: true);
				}
				else
				{
					regionAreas[i].gameObject.SetActive(value: false);
				}
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
		bool flag = IsExistedHard();
		SetActive(spotRootTransform, UI.OBJ_SELECT_DIFFICULTY, flag);
		if (flag)
		{
			SetActive(spotRootTransform, UI.BTN_CURRENT_DIFFICULTY_NORMAL, currentDifficulty == REGION_DIFFICULTY_TYPE.NORMAL);
			SetActive(spotRootTransform, UI.BTN_CURRENT_DIFFICULTY_HARD, currentDifficulty == REGION_DIFFICULTY_TYPE.HARD);
			UIWidget component = FindCtrl(spotRootTransform, UI.OBJ_SELECT_DIFFICULTY).GetComponent<UIWidget>();
			if (!(component == null))
			{
				StartCoroutine(FadeWidget(component, 0, 1, 0.3f));
			}
		}
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
			Singleton<RegionTable>.I.GetData((uint)releaseRegionId);
			GameSection.StayEvent();
			MonoBehaviourSingleton<WorldMapManager>.I.SendRegionOpen(releaseRegionId, delegate(bool isSuccess)
			{
				GameSection.ResumeEvent(isSuccess);
				if (isSuccess)
				{
					ValidRegionInfo obj = validRegionInfo[releaseRegionId];
					obj.status = REGION_STATUS.OPEN;
					obj.icon = obj.releaseIcon;
					StartCoroutine(PlayOpenRegionMap(useReleaseRegion: true));
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
			closeBtn.gameObject.SetActive(value: false);
		}
		Transform worldSelector = FindCtrl(spots.spotRootTransform, UI.OBJ_WORLD_SELECT);
		bool existWorldSelect = worldSelector.gameObject.activeSelf;
		SetActive(worldSelector, is_visible: false);
		UpdateDifficultyButton(forceOff: true);
		MonoBehaviourSingleton<UIManager>.I.SetDisable(UIManager.DISABLE_FACTOR.CAMERA_ACTION, is_disable: true);
		toRegionId = releaseRegionId;
		yield return StartCoroutine(InitializeOpenRegion());
		Vector3 to = new Vector3(0f, 0f, 0f);
		RegionTable.Data data = Singleton<RegionTable>.I.GetData((uint)toRegionId);
		if (data != null)
		{
			to = data.iconPos;
		}
		yield return new WaitForSeconds(0.5f);
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
		Transform transform = regionAreas[toRegionId];
		transform.gameObject.SetActive(value: true);
		Renderer toRegionRenderer = transform.GetComponent<Renderer>();
		toRegionRenderer.material.SetFloat("_Alpha", 0f);
		Renderer topRenderer = glowRegionTop.GetComponent<Renderer>();
		topRenderer.material.SetFloat("_Alpha", 0f);
		topRenderer.material.SetFloat("_AddColor", 1f);
		topRenderer.material.SetFloat("_BlendRate", 1f);
		topRenderer.sortingOrder = 2;
		glowRegionTop.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(1f);
		mapGlowEffectA.gameObject.SetActive(value: true);
		mapGlowEffectA.GetComponent<Renderer>().sortingOrder = 1;
		SpotManager.Spot toSpot = spots.GetSpot(toRegionId);
		toSpot._transform.gameObject.SetActive(value: false);
		RegionTable.Data data2 = Singleton<RegionTable>.I.GetData((uint)toRegionId);
		toSpot.ReleaseRegion(data2.regionName, validRegionInfo[toRegionId].releaseIcon.loadedObject as Texture2D, "OPEN_REGION");
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
			topRenderer.material.SetFloat("_Alpha", fip.Get());
			yield return null;
		}
		toRegionRenderer.material.SetFloat("_Alpha", 1f);
		mapGlowEffectParticleA.Stop();
		mapGlowEffectParticleA.gameObject.SetActive(value: false);
		mapGlowEffectB.gameObject.SetActive(value: true);
		yield return null;
		fip.Set(0.2f, 1f, 0f, null, 0f);
		fip.Play();
		while (fip.IsPlaying())
		{
			fip.Update();
			topRenderer.material.SetFloat("_Alpha", fip.Get());
			yield return null;
		}
		yield return null;
		toSpot._transform.gameObject.SetActive(value: true);
		toSpot._transform.GetComponent<TweenScale>().PlayForward();
		yield return new WaitForSeconds(1f);
		mapGlowEffectParticleB.Stop();
		mapGlowEffectParticleB.gameObject.SetActive(value: false);
		bool isTweenEnd = false;
		telop.gameObject.SetActive(value: true);
		UITweenCtrl component = telop.GetComponent<UITweenCtrl>();
		component.Reset();
		component.Play(forward: true, delegate
		{
			isTweenEnd = true;
		});
		SoundManager.PlayOneShotUISE(SE_ID_LOGO);
		while (!isTweenEnd)
		{
			yield return null;
		}
		mapGlowEffectA.gameObject.SetActive(value: false);
		mapGlowEffectB.gameObject.SetActive(value: false);
		yield return new WaitForSeconds(0.6f);
		telop.gameObject.SetActive(value: false);
		UpdateDifficultyButton();
		if (null != closeBtn)
		{
			closeBtn.gameObject.SetActive(value: true);
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
		StartCoroutine(DoDelayExecute(delayTime, func));
	}

	private IEnumerator DoDelayExecute(float delayTime, Action func)
	{
		yield return new WaitForSeconds(delayTime);
		func?.Invoke();
	}

	private IEnumerator InitializeOpenRegion()
	{
		if (!regionOpenInitialized)
		{
			LoadingQueue loadingQueue = new LoadingQueue(this);
			LoadObject loadedMapGlowEffectA = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "MapGlowEffectA");
			LoadObject loadedMapGlowEffectB = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "MapGlowEffectB");
			LoadObject loadedTelop = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "TelopOpenRegion");
			loadingQueue.CacheSE(SE_ID_LOGO);
			loadingQueue.CacheSE(SE_ID_SMOKE);
			LoadObject loadedMaterial = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "WorldMapPartGlow" + toRegionId.ToString("D3"));
			if (loadingQueue.IsLoading())
			{
				yield return loadingQueue.Wait();
			}
			mapGlowEffectA = ResourceUtility.Realizes(loadedMapGlowEffectA.loadedObject, base._transform);
			mapGlowEffectA.gameObject.SetActive(value: false);
			mapGlowEffectParticleA = mapGlowEffectA.GetComponent<ParticleSystem>();
			mapGlowEffectB = ResourceUtility.Realizes(loadedMapGlowEffectB.loadedObject, base._transform);
			mapGlowEffectB.gameObject.SetActive(value: false);
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
		Transform transform = regionAreas[toRegionId];
		transform.gameObject.SetActive(value: false);
		mapGlowEffectA.SetParent(transform);
		mapGlowEffectA.localPosition = new Vector3(0f, 0f, 0f);
		mapGlowEffectB.SetParent(transform);
		mapGlowEffectB.localPosition = new Vector3(0f, 0f, 0f);
		ParticleSystem.ShapeModule shape = mapGlowEffectParticleB.shape;
		MeshFilter component = transform.GetComponent<MeshFilter>();
		shape.mesh = component.sharedMesh;
		glowRegionTop = ResourceUtility.Realizes(transform.gameObject, base._transform);
		glowRegionTop.gameObject.SetActive(value: false);
		glowRegionTop.localPosition += new Vector3(0f, 0f, 0.001f);
		glowRegionTop.localScale = new Vector3(1.1f, 1.1f, 1.1f);
		glowRegionTop.GetComponent<Renderer>().material = glowMaterial;
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
