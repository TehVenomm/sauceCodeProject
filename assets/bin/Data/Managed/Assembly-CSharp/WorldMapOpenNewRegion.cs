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
		FieldMapTable.PortalTableData portalData = Singleton<FieldMapTable>.I.GetPortalData(MonoBehaviourSingleton<FieldManager>.I.currentPortalID);
		FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(portalData.srcMapID);
		FieldMapTable.FieldMapTableData fieldMapData2 = Singleton<FieldMapTable>.I.GetFieldMapData(portalData.dstMapID);
		fromRegionID = fieldMapData.regionId;
		toRegionID = fieldMapData2.regionId;
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject loadedWorldMap = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "WorldMap");
		LoadObject loadedRegionSpotRoot = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionSpotRoot");
		LoadObject loadedRegionSpot = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionSpot");
		LoadObject loadedPlayerMarker = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "PlayerMarker");
		LoadObject loadedMapGlowEffectA = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "MapGlowEffectA");
		LoadObject loadedMapGlowEffectB = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "MapGlowEffectB");
		LoadObject loadedTelop = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "TelopOpenRegion");
		loadingQueue.CacheSE(SE_ID_LOGO);
		loadingQueue.CacheSE(SE_ID_SMOKE);
		uint[] array = MonoBehaviourSingleton<WorldMapManager>.I.GetOpenRegionIdListInWorldMap();
		if (array.Length == 0)
		{
			array = new uint[1];
		}
		LoadObject[] regionAreaLOs = new LoadObject[array.Length];
		string regionIcon = ResourceName.GetRegionIcon(0);
		string regionIcon2 = ResourceName.GetRegionIcon(1);
		int num = array.Length - 1;
		openedRegionInfo = new OpendRegionInfo[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			RegionTable.Data data = Singleton<RegionTable>.I.GetData(array[i]);
			if (!data.hasParentRegion())
			{
				string resource_name = regionIcon2;
				if (num == i)
				{
					resource_name = regionIcon;
				}
				LoadObject icon = loadingQueue.Load(RESOURCE_CATEGORY.REGION_ICON, resource_name);
				openedRegionInfo[i] = new OpendRegionInfo(data, icon);
				if (i != 0)
				{
					regionAreaLOs[i] = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "WorldMapPart" + array[i].ToString("D3"));
				}
			}
		}
		LoadObject loadedMaterial = null;
		if (!eventData.IsOnlyCameraMoveEvent())
		{
			loadedMaterial = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "WorldMapPartGlow" + toRegionID.ToString("D3"));
		}
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		worldMapUIRoot = ResourceUtility.Realizes(loadedWorldMap.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform).gameObject;
		worldMapCamera = worldMapUIRoot.transform.Find("Camera").GetComponent<WorldMapCameraController>();
		worldMapCamera.isInteractive = false;
		worldMapObject = worldMapUIRoot.transform.Find("Map");
		spots = new SpotManager(loadedRegionSpotRoot.loadedObject as GameObject, loadedRegionSpot.loadedObject as GameObject, worldMapCamera._camera);
		spots.CreateSpotRoot();
		GameObject gameObject = spots.spotRootTransform.Find("BG").gameObject;
		gameObject.gameObject.SetActive(value: true);
		bgEventListener = UIEventListener.Get(gameObject);
		spots.spotRootTransform.Find("TaptoSkip").gameObject.SetActive(value: true);
		mapGlowEffectA = ResourceUtility.Realizes(loadedMapGlowEffectA.loadedObject, worldMapObject);
		mapGlowEffectA.gameObject.SetActive(value: false);
		mapGlowEffectParticleA = mapGlowEffectA.GetComponent<ParticleSystem>();
		mapGlowEffectB = ResourceUtility.Realizes(loadedMapGlowEffectB.loadedObject, worldMapObject);
		mapGlowEffectB.gameObject.SetActive(value: false);
		mapGlowEffectParticleB = mapGlowEffectB.GetComponent<ParticleSystem>();
		playerMarker = ResourceUtility.Realizes(loadedPlayerMarker.loadedObject, base._transform);
		playerMarker.gameObject.SetActive(value: false);
		if (loadedMaterial != null)
		{
			glowMaterial = (loadedMaterial.loadedObject as Material);
		}
		regionAreas = new Transform[regionAreaLOs.Length];
		for (int j = 0; j < regionAreaLOs.Length; j++)
		{
			LoadObject loadObject = regionAreaLOs[j];
			if (loadObject == null || !(null != loadObject.loadedObject))
			{
				continue;
			}
			Transform transform = ResourceUtility.Realizes(loadObject.loadedObject, worldMapObject);
			if (j == toRegionID)
			{
				if (eventData.IsOnlyCameraMoveEvent())
				{
					transform.gameObject.SetActive(value: true);
				}
				else
				{
					transform.gameObject.SetActive(value: false);
				}
				mapGlowEffectA.SetParent(transform);
				mapGlowEffectA.localPosition = new Vector3(0f, 0f, 0f);
				mapGlowEffectB.SetParent(transform);
				mapGlowEffectB.localPosition = new Vector3(0f, 0f, 0f);
				ParticleSystem.ShapeModule shape = mapGlowEffectParticleB.shape;
				MeshFilter component = transform.GetComponent<MeshFilter>();
				shape.mesh = component.sharedMesh;
				glowRegionTop = ResourceUtility.Realizes(loadObject.loadedObject, worldMapObject);
				glowRegionTop.gameObject.SetActive(value: false);
				glowRegionTop.localPosition += new Vector3(0f, 0f, 0.001f);
				glowRegionTop.localScale = new Vector3(1.1f, 1.1f, 1.1f);
				glowRegionTop.GetComponent<Renderer>().material = glowMaterial;
			}
			else
			{
				transform.gameObject.SetActive(value: true);
			}
			regionAreas[j] = transform;
		}
		telop = ResourceUtility.Realizes(loadedTelop.loadedObject, spots.spotRootTransform);
		Transform transform2 = Utility.Find(spots.spotRootTransform, "CLOSE_BTN");
		if (null != transform2)
		{
			transform2.gameObject.SetActive(value: false);
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += InitMapSprite;
		}
		base.Initialize();
	}

	public void InitRegionInfo()
	{
		if (spots == null)
		{
			return;
		}
		Transform transform = spots.SetRoot(base._transform);
		if (uiMapSprite == null)
		{
			uiMapSprite = transform.Find("Map").gameObject.GetComponent<UITexture>();
		}
		InitMapSprite(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		worldMapObject.gameObject.SetActive(value: true);
		for (int i = 0; i < openedRegionInfo.Length; i++)
		{
			RegionTable.Data data = openedRegionInfo[i].data;
			if (data == null)
			{
				continue;
			}
			SpotManager.Spot spot = spots.AddSpot((int)data.regionId, data.regionName, data.iconPos, SpotManager.ICON_TYPE.CLEARED, "OPEN_REGION");
			spot.SetIconSprite("SPR_ICON", openedRegionInfo[i].icon.loadedObject as Texture2D, (int)data.iconSize.x, (int)data.iconSize.y);
			if (fromRegionID == data.regionId)
			{
				playerMarker.gameObject.SetActive(value: true);
				playerMarker.SetParent(worldMapObject.transform);
				PlayerMarker component = playerMarker.GetComponent<PlayerMarker>();
				component.SetWorldMode(enable: true);
				component.SetCamera(worldMapCamera._camera.transform);
				playerMarker.localPosition = data.markerPos;
			}
			if (toRegionID == data.regionId)
			{
				targetRegionIcon = spot._transform;
				if (!eventData.IsOnlyCameraMoveEvent())
				{
					spot._transform.gameObject.SetActive(value: false);
				}
				else
				{
					spot._transform.gameObject.SetActive(value: true);
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
		if (isPortrait)
		{
			if (null != telop)
			{
				telop.localPosition = new Vector3(0f, 0f, 0f);
			}
			if (null != mapGlowEffectA)
			{
				ParticleSystemRenderer component = mapGlowEffectA.GetComponent<ParticleSystemRenderer>();
				component.minParticleSize = 1f;
				component.maxParticleSize = 1f;
			}
			if (null != mapGlowEffectB)
			{
				ParticleSystemRenderer component2 = mapGlowEffectB.GetComponent<ParticleSystemRenderer>();
				component2.minParticleSize = 1f;
				component2.maxParticleSize = 1f;
			}
		}
		else
		{
			if (null != telop)
			{
				telop.localPosition = new Vector3(0f, -90f, 0f);
			}
			if (null != mapGlowEffectA)
			{
				ParticleSystemRenderer component3 = mapGlowEffectA.GetComponent<ParticleSystemRenderer>();
				component3.minParticleSize = 0.5f;
				component3.maxParticleSize = 0.5f;
			}
			if (null != mapGlowEffectB)
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
		worldMapObject.gameObject.SetActive(value: true);
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
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("WorldMapOpenNewRegion", base.gameObject, "INGAME_MAIN");
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
		if (worldMapUIRoot != null)
		{
			UnityEngine.Object.Destroy(worldMapUIRoot);
		}
		if (worldMapObject != null)
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
		if (worldMapObject != null)
		{
			worldMapObject.gameObject.SetActive(value: true);
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

	private IEnumerator DoFadeMap(float from, float to, float time, Action onComplete)
	{
		if (worldMapObject == null)
		{
			yield break;
		}
		Renderer r = worldMapObject.gameObject.GetComponentInChildren<Renderer>();
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

	private void GlowRegion(Vector3 from, Vector3 to)
	{
		worldMapCamera.targetPos = from;
		StartCoroutine(DoGlowRegion(from, to));
	}

	private IEnumerator DoGlowRegion(Vector3 from, Vector3 to)
	{
		yield return new WaitForSeconds(0.5f);
		Vector3Interpolator ip = new Vector3Interpolator();
		Vector3 zoomDownTo = to + new Vector3(0f, 0f, -3f);
		ip.Set(1f, from, zoomDownTo);
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			worldMapCamera.targetPos = ip.Get();
			yield return null;
		}
		Transform transform = regionAreas[toRegionID];
		transform.gameObject.SetActive(value: true);
		Renderer toRegionRenderer = transform.GetComponent<Renderer>();
		toRegionRenderer.material.SetFloat("_Alpha", 0f);
		Renderer topRenderer = glowRegionTop.GetComponent<Renderer>();
		topRenderer.material.SetFloat("_Alpha", 0f);
		topRenderer.material.SetFloat("_AddColor", 1f);
		topRenderer.material.SetFloat("_BlendRate", 1f);
		topRenderer.sortingOrder = 2;
		glowRegionTop.gameObject.SetActive(value: true);
		DelayExecute(1f, delegate
		{
			mapGlowEffectA.gameObject.SetActive(value: true);
			mapGlowEffectA.GetComponent<Renderer>().sortingOrder = 1;
		});
		yield return new WaitForSeconds(1f);
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
		mapGlowEffectB.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(0f);
		fip.Set(0.2f, 1f, 0f, null, 0f);
		fip.Play();
		while (fip.IsPlaying())
		{
			fip.Update();
			topRenderer.material.SetFloat("_Alpha", fip.Get());
			yield return null;
		}
		yield return new WaitForSeconds(0f);
		targetRegionIcon.gameObject.SetActive(value: true);
		targetRegionIcon.GetComponent<TweenScale>().PlayForward();
		yield return new WaitForSeconds(1f);
		mapGlowEffectParticleB.Stop();
		bool isTweenEnd = false;
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
		yield return new WaitForSeconds(0f);
		Vector3 scaleBegin = playerMarker.localScale;
		Vector3 scaleEnd = new Vector3(0f, 0f, 0f);
		ip.Set(0.5f, scaleBegin, scaleEnd);
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			playerMarker.localScale = ip.Get();
			yield return null;
		}
		RegionTable.Data data = openedRegionInfo[toRegionID].data;
		if (data != null)
		{
			playerMarker.localPosition = data.markerPos;
		}
		yield return new WaitForSeconds(0.1f);
		ip.Set(0.5f, scaleEnd, scaleBegin);
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			playerMarker.localScale = ip.Get();
			yield return null;
		}
		yield return new WaitForSeconds(0.4f);
		OnQuery_EXIT();
	}

	private void MoveCamera(Vector3 from, Vector3 to)
	{
		StartCoroutine(DoMoveCamera(from, to));
	}

	private IEnumerator DoMoveCamera(Vector3 from, Vector3 to)
	{
		Vector3Interpolator ip = new Vector3Interpolator();
		yield return new WaitForSeconds(0.5f);
		Vector3 scaleBegin = playerMarker.localScale;
		Vector3 scaleEnd = new Vector3(0f, 0f, 0f);
		ip.Set(0.5f, scaleBegin, scaleEnd);
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			playerMarker.localScale = ip.Get();
			yield return null;
		}
		yield return new WaitForSeconds(0f);
		ip.Set(0.7f, from, to);
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			worldMapCamera.targetPos = ip.Get();
			yield return null;
		}
		RegionTable.Data data = openedRegionInfo[toRegionID].data;
		if (data != null)
		{
			playerMarker.localPosition = data.markerPos;
		}
		yield return new WaitForSeconds(0.1f);
		ip.Set(0.5f, scaleEnd, scaleBegin);
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			playerMarker.localScale = ip.Get();
			yield return null;
		}
		yield return new WaitForSeconds(0.4f);
		OnQuery_EXIT();
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

	private void OnApplicationPause(bool paused)
	{
		isUpdateRenderTexture = !paused;
	}

	private void onClick(GameObject g)
	{
		OnQuery_EXIT();
	}
}
