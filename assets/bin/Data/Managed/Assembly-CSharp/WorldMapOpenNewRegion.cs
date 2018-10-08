using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapOpenNewRegion : GameSection
{
	public enum EVENT_TYPE
	{
		NONE,
		ONLY_CAMERA_MOVE
	}

	public class SectionEventData
	{
		private EVENT_TYPE eventType;

		public SectionEventData(EVENT_TYPE _eventType)
		{
			eventType = _eventType;
		}

		public bool IsOnlyCameraMoveEvent()
		{
			return eventType == EVENT_TYPE.ONLY_CAMERA_MOVE;
		}
	}

	private struct OpendRegionInfo
	{
		public RegionTable.Data data;

		public LoadObject icon;

		public OpendRegionInfo(RegionTable.Data _data, LoadObject _icon)
		{
			data = _data;
			icon = _icon;
		}
	}

	private SectionEventData eventData;

	private OpendRegionInfo[] openedRegionInfo;

	private GameObject worldMapUIRoot;

	private Transform worldMapObject;

	private Transform playerMarker;

	private UITexture uiMapSprite;

	private SpotManager spots;

	private Transform[] regionAreas;

	private Transform glowRegionTop;

	private Transform mapGlowEffectA;

	private Transform mapGlowEffectB;

	private ParticleSystem mapGlowEffectParticleA;

	private ParticleSystem mapGlowEffectParticleB;

	private uint fromRegionID;

	private uint toRegionID;

	private Transform telop;

	private Transform targetRegionIcon;

	private Material glowMaterial;

	private static readonly int SE_ID_SMOKE = 40000034;

	private static readonly int SE_ID_LOGO = 40000160;

	private bool calledExit;

	private bool isUpdateRenderTexture;

	private UIEventListener bgEventListener;

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

	public override void Initialize()
	{
		StartCoroutine("DoInitialize");
	}

	private IEnumerator DoInitialize()
	{
		eventData = (SectionEventData)GameSection.GetEventData();
		FieldMapTable.PortalTableData portal = Singleton<FieldMapTable>.I.GetPortalData(MonoBehaviourSingleton<FieldManager>.I.currentPortalID);
		FieldMapTable.FieldMapTableData mapA = Singleton<FieldMapTable>.I.GetFieldMapData(portal.srcMapID);
		FieldMapTable.FieldMapTableData mapB = Singleton<FieldMapTable>.I.GetFieldMapData(portal.dstMapID);
		fromRegionID = mapA.regionId;
		toRegionID = mapB.regionId;
		LoadingQueue loadQueue = new LoadingQueue(this);
		LoadObject loadedWorldMap = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "WorldMap", false);
		LoadObject loadedRegionSpotRoot = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionSpotRoot", false);
		LoadObject loadedRegionSpot = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionSpot", false);
		LoadObject loadedPlayerMarker = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "PlayerMarker", false);
		LoadObject loadedMapGlowEffectA = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "MapGlowEffectA", false);
		LoadObject loadedMapGlowEffectB = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "MapGlowEffectB", false);
		LoadObject loadedTelop = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "TelopOpenRegion", false);
		loadQueue.CacheSE(SE_ID_LOGO, null);
		loadQueue.CacheSE(SE_ID_SMOKE, null);
		uint[] openedRegionids = MonoBehaviourSingleton<WorldMapManager>.I.GetOpenRegionIdListInWorldMap();
		if (openedRegionids.Length == 0)
		{
			openedRegionids = new uint[1];
		}
		LoadObject[] regionAreaLOs = new LoadObject[openedRegionids.Length];
		string newRegionIcon = ResourceName.GetRegionIcon(0);
		string passedRegionIcon = ResourceName.GetRegionIcon(1);
		int lastIndex = openedRegionids.Length - 1;
		openedRegionInfo = new OpendRegionInfo[openedRegionids.Length];
		for (int j = 0; j < openedRegionids.Length; j++)
		{
			RegionTable.Data data = Singleton<RegionTable>.I.GetData(openedRegionids[j]);
			if (!data.hasParentRegion())
			{
				string iconName = passedRegionIcon;
				if (lastIndex == j)
				{
					iconName = newRegionIcon;
				}
				LoadObject loadedObj = loadQueue.Load(RESOURCE_CATEGORY.REGION_ICON, iconName, false);
				openedRegionInfo[j] = new OpendRegionInfo(data, loadedObj);
				if (j != 0)
				{
					regionAreaLOs[j] = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "WorldMapPart" + openedRegionids[j].ToString("D3"), false);
				}
			}
		}
		LoadObject loadedMaterial = null;
		if (!eventData.IsOnlyCameraMoveEvent())
		{
			loadedMaterial = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "WorldMapPartGlow" + toRegionID.ToString("D3"), false);
		}
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		worldMapUIRoot = ResourceUtility.Realizes(loadedWorldMap.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform, -1).gameObject;
		worldMapCamera = worldMapUIRoot.transform.Find("Camera").GetComponent<WorldMapCameraController>();
		worldMapCamera.isInteractive = false;
		worldMapObject = worldMapUIRoot.transform.FindChild("Map");
		spots = new SpotManager(loadedRegionSpotRoot.loadedObject as GameObject, loadedRegionSpot.loadedObject as GameObject, worldMapCamera._camera);
		spots.CreateSpotRoot();
		GameObject bg = spots.spotRootTransform.Find("BG").gameObject;
		bg.gameObject.SetActive(true);
		bgEventListener = UIEventListener.Get(bg);
		spots.spotRootTransform.Find("TaptoSkip").gameObject.SetActive(true);
		mapGlowEffectA = ResourceUtility.Realizes(loadedMapGlowEffectA.loadedObject, worldMapObject, -1);
		mapGlowEffectA.gameObject.SetActive(false);
		mapGlowEffectParticleA = mapGlowEffectA.GetComponent<ParticleSystem>();
		mapGlowEffectB = ResourceUtility.Realizes(loadedMapGlowEffectB.loadedObject, worldMapObject, -1);
		mapGlowEffectB.gameObject.SetActive(false);
		mapGlowEffectParticleB = mapGlowEffectB.GetComponent<ParticleSystem>();
		playerMarker = ResourceUtility.Realizes(loadedPlayerMarker.loadedObject, base._transform, -1);
		playerMarker.gameObject.SetActive(false);
		if (loadedMaterial != null)
		{
			glowMaterial = (loadedMaterial.loadedObject as Material);
		}
		regionAreas = new Transform[regionAreaLOs.Length];
		for (int i = 0; i < regionAreaLOs.Length; i++)
		{
			LoadObject areaLO = regionAreaLOs[i];
			if (areaLO != null && (UnityEngine.Object)null != areaLO.loadedObject)
			{
				Transform regionArea = ResourceUtility.Realizes(areaLO.loadedObject, worldMapObject, -1);
				if (i == toRegionID)
				{
					if (eventData.IsOnlyCameraMoveEvent())
					{
						regionArea.gameObject.SetActive(true);
					}
					else
					{
						regionArea.gameObject.SetActive(false);
					}
					mapGlowEffectA.SetParent(regionArea);
					mapGlowEffectA.localPosition = new Vector3(0f, 0f, 0f);
					mapGlowEffectB.SetParent(regionArea);
					mapGlowEffectB.localPosition = new Vector3(0f, 0f, 0f);
					ParticleSystem.ShapeModule module = mapGlowEffectParticleB.shape;
					MeshFilter meshFilter = regionArea.GetComponent<MeshFilter>();
					module.mesh = meshFilter.sharedMesh;
					glowRegionTop = ResourceUtility.Realizes(areaLO.loadedObject, worldMapObject, -1);
					glowRegionTop.gameObject.SetActive(false);
					glowRegionTop.localPosition += new Vector3(0f, 0f, 0.001f);
					glowRegionTop.localScale = new Vector3(1.1f, 1.1f, 1.1f);
					glowRegionTop.GetComponent<Renderer>().material = glowMaterial;
				}
				else
				{
					regionArea.gameObject.SetActive(true);
				}
				regionAreas[i] = regionArea;
			}
		}
		telop = ResourceUtility.Realizes(loadedTelop.loadedObject, spots.spotRootTransform, -1);
		Transform closeBtn = Utility.Find(spots.spotRootTransform, "CLOSE_BTN");
		if ((UnityEngine.Object)null != (UnityEngine.Object)closeBtn)
		{
			closeBtn.gameObject.SetActive(false);
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += InitMapSprite;
		}
		base.Initialize();
	}

	public void InitRegionInfo()
	{
		if (spots != null)
		{
			Transform transform = spots.SetRoot(base._transform);
			if ((UnityEngine.Object)uiMapSprite == (UnityEngine.Object)null)
			{
				uiMapSprite = transform.FindChild("Map").gameObject.GetComponent<UITexture>();
			}
			InitMapSprite(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
			worldMapObject.gameObject.SetActive(true);
			for (int i = 0; i < openedRegionInfo.Length; i++)
			{
				RegionTable.Data data = openedRegionInfo[i].data;
				if (data != null)
				{
					SpotManager.Spot spot = spots.AddSpot((int)data.regionId, data.regionName, data.iconPos, SpotManager.ICON_TYPE.CLEARED, "OPEN_REGION", false, false, false, null, null, false, SpotManager.HAPPEN_CONDITION.NONE, 0);
					spot.SetIconSprite("SPR_ICON", openedRegionInfo[i].icon.loadedObject as Texture2D, (int)data.iconSize.x, (int)data.iconSize.y);
					if (fromRegionID == data.regionId)
					{
						playerMarker.gameObject.SetActive(true);
						playerMarker.SetParent(worldMapObject.transform);
						PlayerMarker component = playerMarker.GetComponent<PlayerMarker>();
						component.SetWorldMode(true);
						component.SetCamera(worldMapCamera._camera.transform);
						playerMarker.localPosition = data.markerPos;
					}
					if (toRegionID == data.regionId)
					{
						targetRegionIcon = spot._transform;
						if (!eventData.IsOnlyCameraMoveEvent())
						{
							spot._transform.gameObject.SetActive(false);
						}
						else
						{
							spot._transform.gameObject.SetActive(true);
						}
					}
				}
			}
		}
	}

	private void InitMapSprite(bool isPortrait)
	{
		if ((UnityEngine.Object)uiMapSprite != (UnityEngine.Object)null)
		{
			if ((UnityEngine.Object)null == (UnityEngine.Object)worldMapCamera._camera.targetTexture)
			{
				worldMapCamera.Restore();
			}
			uiMapSprite.mainTexture = worldMapCamera._camera.targetTexture;
			uiMapSprite.width = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth;
			uiMapSprite.height = MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight;
		}
		if (isPortrait)
		{
			if ((UnityEngine.Object)null != (UnityEngine.Object)telop)
			{
				telop.localPosition = new Vector3(0f, 0f, 0f);
			}
			if ((UnityEngine.Object)null != (UnityEngine.Object)mapGlowEffectA)
			{
				ParticleSystemRenderer component = mapGlowEffectA.GetComponent<ParticleSystemRenderer>();
				component.minParticleSize = 1f;
				component.maxParticleSize = 1f;
			}
			if ((UnityEngine.Object)null != (UnityEngine.Object)mapGlowEffectB)
			{
				ParticleSystemRenderer component2 = mapGlowEffectB.GetComponent<ParticleSystemRenderer>();
				component2.minParticleSize = 1f;
				component2.maxParticleSize = 1f;
			}
		}
		else
		{
			if ((UnityEngine.Object)null != (UnityEngine.Object)telop)
			{
				telop.localPosition = new Vector3(0f, -90f, 0f);
			}
			if ((UnityEngine.Object)null != (UnityEngine.Object)mapGlowEffectA)
			{
				ParticleSystemRenderer component3 = mapGlowEffectA.GetComponent<ParticleSystemRenderer>();
				component3.minParticleSize = 0.5f;
				component3.maxParticleSize = 0.5f;
			}
			if ((UnityEngine.Object)null != (UnityEngine.Object)mapGlowEffectB)
			{
				ParticleSystemRenderer component4 = mapGlowEffectB.GetComponent<ParticleSystemRenderer>();
				component4.minParticleSize = 0.5f;
				component4.maxParticleSize = 0.5f;
			}
		}
	}

	protected override void OnOpen()
	{
		UIEventListener uIEventListener = bgEventListener;
		uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener.onClick, new UIEventListener.VoidDelegate(onClick));
		worldMapObject.gameObject.SetActive(true);
		Vector3 from = new Vector3(0f, 0f, 0f);
		Vector3 to = new Vector3(0f, 0f, 0f);
		RegionTable.Data[] data = Singleton<RegionTable>.I.GetData();
		if (0 <= fromRegionID && data.Length > fromRegionID)
		{
			from = data[fromRegionID].iconPos;
			worldMapCamera.targetPos = from;
		}
		if (0 <= toRegionID && data.Length > toRegionID)
		{
			to = data[toRegionID].iconPos;
		}
		FadeInMap(delegate
		{
			InitRegionInfo();
			if (eventData.IsOnlyCameraMoveEvent())
			{
				MoveCamera(from, to);
			}
			else
			{
				GlowRegion(from, to);
			}
		});
		base.collectUI = base._transform;
		base.OnOpen();
	}

	private void OnQuery_EXIT()
	{
		if (!calledExit)
		{
			UIEventListener uIEventListener = bgEventListener;
			uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Remove(uIEventListener.onClick, new UIEventListener.VoidDelegate(onClick));
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("WorldMapOpenNewRegion", base.gameObject, "INGAME_MAIN", null, null, true);
			calledExit = true;
		}
		StopAllCoroutines();
	}

	public override void Exit()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= InitMapSprite;
		}
		if (spots != null)
		{
			spots.ClearAllSpot();
		}
		base.Exit();
	}

	protected override void OnDestroy()
	{
		if ((UnityEngine.Object)worldMapUIRoot != (UnityEngine.Object)null)
		{
			UnityEngine.Object.Destroy(worldMapUIRoot);
		}
		if ((UnityEngine.Object)worldMapObject != (UnityEngine.Object)null)
		{
			UnityEngine.Object.Destroy(worldMapObject.gameObject);
		}
		base.OnDestroy();
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

	public void FadeInMap(Action onComplete)
	{
		if ((UnityEngine.Object)worldMapObject != (UnityEngine.Object)null)
		{
			worldMapObject.gameObject.SetActive(true);
		}
		if ((UnityEngine.Object)uiMapSprite != (UnityEngine.Object)null)
		{
			uiMapSprite.gameObject.SetActive(true);
		}
		StartCoroutine(DoFadeMap(0f, 1f, 0.4f, delegate
		{
			if (onComplete != null)
			{
				onComplete();
			}
		}));
	}

	private IEnumerator DoFadeMap(float from, float to, float time, Action onComplete)
	{
		if (!((UnityEngine.Object)worldMapObject == (UnityEngine.Object)null))
		{
			Renderer r = worldMapObject.gameObject.GetComponentInChildren<Renderer>();
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

	private void GlowRegion(Vector3 from, Vector3 to)
	{
		worldMapCamera.targetPos = from;
		StartCoroutine(DoGlowRegion(from, to));
	}

	private IEnumerator DoGlowRegion(Vector3 from, Vector3 to)
	{
		yield return (object)new WaitForSeconds(0.5f);
		Vector3Interpolator ip = new Vector3Interpolator();
		Vector3 zoomDownTo = to + new Vector3(0f, 0f, -3f);
		ip.Set(1f, from, zoomDownTo, null, default(Vector3), null);
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			worldMapCamera.targetPos = ip.Get();
			yield return (object)null;
		}
		Transform toRegion = regionAreas[toRegionID];
		toRegion.gameObject.SetActive(true);
		Renderer toRegionRenderer = toRegion.GetComponent<Renderer>();
		toRegionRenderer.material.SetFloat("_Alpha", 0f);
		Renderer topRenderer = glowRegionTop.GetComponent<Renderer>();
		topRenderer.material.SetFloat("_Alpha", 0f);
		topRenderer.material.SetFloat("_AddColor", 1f);
		topRenderer.material.SetFloat("_BlendRate", 1f);
		topRenderer.sortingOrder = 2;
		glowRegionTop.gameObject.SetActive(true);
		DelayExecute(1f, delegate
		{
			((_003CDoGlowRegion_003Ec__Iterator199)/*Error near IL_0211: stateMachine*/)._003C_003Ef__this.mapGlowEffectA.gameObject.SetActive(true);
			Renderer component = ((_003CDoGlowRegion_003Ec__Iterator199)/*Error near IL_0211: stateMachine*/)._003C_003Ef__this.mapGlowEffectA.GetComponent<Renderer>();
			component.sortingOrder = 1;
		});
		yield return (object)new WaitForSeconds(1f);
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
			topRenderer.material.SetFloat("_Alpha", fip.Get());
			yield return (object)null;
		}
		toRegionRenderer.material.SetFloat("_Alpha", 1f);
		mapGlowEffectParticleA.Stop();
		mapGlowEffectB.gameObject.SetActive(true);
		yield return (object)new WaitForSeconds(0f);
		fip.Set(0.2f, 1f, 0f, null, 0f, null);
		fip.Play();
		while (fip.IsPlaying())
		{
			fip.Update();
			topRenderer.material.SetFloat("_Alpha", fip.Get());
			yield return (object)null;
		}
		yield return (object)new WaitForSeconds(0f);
		targetRegionIcon.gameObject.SetActive(true);
		TweenScale tweenScale = targetRegionIcon.GetComponent<TweenScale>();
		tweenScale.PlayForward();
		yield return (object)new WaitForSeconds(1f);
		mapGlowEffectParticleB.Stop();
		bool isTweenEnd = false;
		UITweenCtrl tweenCtrl = telop.GetComponent<UITweenCtrl>();
		tweenCtrl.Reset();
		tweenCtrl.Play(true, delegate
		{
			((_003CDoGlowRegion_003Ec__Iterator199)/*Error near IL_04df: stateMachine*/)._003CisTweenEnd_003E__7 = true;
		});
		SoundManager.PlayOneShotUISE(SE_ID_LOGO);
		while (!isTweenEnd)
		{
			yield return (object)null;
		}
		yield return (object)new WaitForSeconds(0f);
		Vector3 scaleBegin = playerMarker.localScale;
		Vector3 scaleEnd = new Vector3(0f, 0f, 0f);
		ip.Set(0.5f, scaleBegin, scaleEnd, null, default(Vector3), null);
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			playerMarker.localScale = ip.Get();
			yield return (object)null;
		}
		RegionTable.Data targetData = openedRegionInfo[toRegionID].data;
		if (targetData != null)
		{
			playerMarker.localPosition = targetData.markerPos;
		}
		yield return (object)new WaitForSeconds(0.1f);
		ip.Set(0.5f, scaleEnd, scaleBegin, null, default(Vector3), null);
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			playerMarker.localScale = ip.Get();
			yield return (object)null;
		}
		yield return (object)new WaitForSeconds(0.4f);
		OnQuery_EXIT();
	}

	private void MoveCamera(Vector3 from, Vector3 to)
	{
		StartCoroutine(DoMoveCamera(from, to));
	}

	private IEnumerator DoMoveCamera(Vector3 from, Vector3 to)
	{
		Vector3Interpolator ip = new Vector3Interpolator();
		yield return (object)new WaitForSeconds(0.5f);
		Vector3 scaleBegin = playerMarker.localScale;
		Vector3 scaleEnd = new Vector3(0f, 0f, 0f);
		ip.Set(0.5f, scaleBegin, scaleEnd, null, default(Vector3), null);
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			playerMarker.localScale = ip.Get();
			yield return (object)null;
		}
		yield return (object)new WaitForSeconds(0f);
		ip.Set(0.7f, from, to, null, default(Vector3), null);
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			worldMapCamera.targetPos = ip.Get();
			yield return (object)null;
		}
		RegionTable.Data targetData = openedRegionInfo[toRegionID].data;
		if (targetData != null)
		{
			playerMarker.localPosition = targetData.markerPos;
		}
		yield return (object)new WaitForSeconds(0.1f);
		ip.Set(0.5f, scaleEnd, scaleBegin, null, default(Vector3), null);
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			playerMarker.localScale = ip.Get();
			yield return (object)null;
		}
		yield return (object)new WaitForSeconds(0.4f);
		OnQuery_EXIT();
	}

	private void DelayExecute(float delayTime, Action func)
	{
		StartCoroutine(DoDelayExecute(delayTime, func));
	}

	private IEnumerator DoDelayExecute(float delayTime, Action func)
	{
		yield return (object)new WaitForSeconds(delayTime);
		func?.Invoke();
	}

	private void OnApplicationPause(bool paused)
	{
		isUpdateRenderTexture = !paused;
	}

	private void onClick(GameObject g)
	{
		OnQuery_EXIT();
	}
}
