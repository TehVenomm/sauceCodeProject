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
		this.StartCoroutine("DoInitialize");
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
		LoadObject loadedWorldMap = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "WorldMap");
		LoadObject loadedRegionSpotRoot = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionSpotRoot");
		LoadObject loadedRegionSpot = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "RegionSpot");
		LoadObject loadedPlayerMarker = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "PlayerMarker");
		LoadObject loadedMapGlowEffectA = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "MapGlowEffectA");
		LoadObject loadedMapGlowEffectB = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "MapGlowEffectB");
		LoadObject loadedTelop = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "TelopOpenRegion");
		loadQueue.CacheSE(SE_ID_LOGO);
		loadQueue.CacheSE(SE_ID_SMOKE);
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
		for (int i = 0; i < openedRegionids.Length; i++)
		{
			RegionTable.Data data = Singleton<RegionTable>.I.GetData(openedRegionids[i]);
			if (!data.hasParentRegion())
			{
				string resource_name = passedRegionIcon;
				if (lastIndex == i)
				{
					resource_name = newRegionIcon;
				}
				LoadObject icon = loadQueue.Load(RESOURCE_CATEGORY.REGION_ICON, resource_name);
				openedRegionInfo[i] = new OpendRegionInfo(data, icon);
				if (i != 0)
				{
					regionAreaLOs[i] = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "WorldMapPart" + openedRegionids[i].ToString("D3"));
				}
			}
		}
		LoadObject loadedMaterial = null;
		if (!eventData.IsOnlyCameraMoveEvent())
		{
			loadedMaterial = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "WorldMapPartGlow" + toRegionID.ToString("D3"));
		}
		if (loadQueue.IsLoading())
		{
			yield return loadQueue.Wait();
		}
		worldMapUIRoot = ResourceUtility.Realizes(loadedWorldMap.loadedObject, MonoBehaviourSingleton<AppMain>.I._transform).get_gameObject();
		worldMapCamera = worldMapUIRoot.get_transform().Find("Camera").GetComponent<WorldMapCameraController>();
		worldMapCamera.isInteractive = false;
		worldMapObject = worldMapUIRoot.get_transform().Find("Map");
		spots = new SpotManager(loadedRegionSpotRoot.loadedObject as GameObject, loadedRegionSpot.loadedObject as GameObject, worldMapCamera._camera);
		spots.CreateSpotRoot();
		GameObject bg = spots.spotRootTransform.Find("BG").get_gameObject();
		bg.get_gameObject().SetActive(true);
		bgEventListener = UIEventListener.Get(bg);
		spots.spotRootTransform.Find("TaptoSkip").get_gameObject().SetActive(true);
		mapGlowEffectA = ResourceUtility.Realizes(loadedMapGlowEffectA.loadedObject, worldMapObject);
		mapGlowEffectA.get_gameObject().SetActive(false);
		mapGlowEffectParticleA = mapGlowEffectA.GetComponent<ParticleSystem>();
		mapGlowEffectB = ResourceUtility.Realizes(loadedMapGlowEffectB.loadedObject, worldMapObject);
		mapGlowEffectB.get_gameObject().SetActive(false);
		mapGlowEffectParticleB = mapGlowEffectB.GetComponent<ParticleSystem>();
		playerMarker = ResourceUtility.Realizes(loadedPlayerMarker.loadedObject, base._transform);
		playerMarker.get_gameObject().SetActive(false);
		if (loadedMaterial != null)
		{
			glowMaterial = (loadedMaterial.loadedObject as Material);
		}
		regionAreas = (Transform[])new Transform[regionAreaLOs.Length];
		for (int j = 0; j < regionAreaLOs.Length; j++)
		{
			LoadObject loadObject = regionAreaLOs[j];
			if (loadObject == null || !(null != loadObject.loadedObject))
			{
				continue;
			}
			Transform val = ResourceUtility.Realizes(loadObject.loadedObject, worldMapObject);
			if (j == toRegionID)
			{
				if (eventData.IsOnlyCameraMoveEvent())
				{
					val.get_gameObject().SetActive(true);
				}
				else
				{
					val.get_gameObject().SetActive(false);
				}
				mapGlowEffectA.SetParent(val);
				mapGlowEffectA.set_localPosition(new Vector3(0f, 0f, 0f));
				mapGlowEffectB.SetParent(val);
				mapGlowEffectB.set_localPosition(new Vector3(0f, 0f, 0f));
				ShapeModule shape = mapGlowEffectParticleB.get_shape();
				MeshFilter component = val.GetComponent<MeshFilter>();
				shape.set_mesh(component.get_sharedMesh());
				glowRegionTop = ResourceUtility.Realizes(loadObject.loadedObject, worldMapObject);
				glowRegionTop.get_gameObject().SetActive(false);
				glowRegionTop.set_localPosition(glowRegionTop.get_localPosition() + new Vector3(0f, 0f, 0.001f));
				glowRegionTop.set_localScale(new Vector3(1.1f, 1.1f, 1.1f));
				glowRegionTop.GetComponent<Renderer>().set_material(glowMaterial);
			}
			else
			{
				val.get_gameObject().SetActive(true);
			}
			regionAreas[j] = val;
		}
		telop = ResourceUtility.Realizes(loadedTelop.loadedObject, spots.spotRootTransform);
		Transform closeBtn = Utility.Find(spots.spotRootTransform, "CLOSE_BTN");
		if (null != closeBtn)
		{
			closeBtn.get_gameObject().SetActive(false);
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene")
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += InitMapSprite;
		}
		base.Initialize();
	}

	public void InitRegionInfo()
	{
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		if (spots == null)
		{
			return;
		}
		Transform val = spots.SetRoot(base._transform);
		if (uiMapSprite == null)
		{
			uiMapSprite = val.Find("Map").get_gameObject().GetComponent<UITexture>();
		}
		InitMapSprite(MonoBehaviourSingleton<ScreenOrientationManager>.I.isPortrait);
		worldMapObject.get_gameObject().SetActive(true);
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
				playerMarker.get_gameObject().SetActive(true);
				playerMarker.SetParent(worldMapObject.get_transform());
				PlayerMarker component = playerMarker.GetComponent<PlayerMarker>();
				component.SetWorldMode(enable: true);
				component.SetCamera(worldMapCamera._camera.get_transform());
				playerMarker.set_localPosition(data.markerPos);
			}
			if (toRegionID == data.regionId)
			{
				targetRegionIcon = spot._transform;
				if (!eventData.IsOnlyCameraMoveEvent())
				{
					spot._transform.get_gameObject().SetActive(false);
				}
				else
				{
					spot._transform.get_gameObject().SetActive(true);
				}
			}
		}
	}

	private void InitMapSprite(bool isPortrait)
	{
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
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
		if (isPortrait)
		{
			if (null != telop)
			{
				telop.set_localPosition(new Vector3(0f, 0f, 0f));
			}
			if (null != mapGlowEffectA)
			{
				ParticleSystemRenderer component = mapGlowEffectA.GetComponent<ParticleSystemRenderer>();
				component.set_minParticleSize(1f);
				component.set_maxParticleSize(1f);
			}
			if (null != mapGlowEffectB)
			{
				ParticleSystemRenderer component2 = mapGlowEffectB.GetComponent<ParticleSystemRenderer>();
				component2.set_minParticleSize(1f);
				component2.set_maxParticleSize(1f);
			}
		}
		else
		{
			if (null != telop)
			{
				telop.set_localPosition(new Vector3(0f, -90f, 0f));
			}
			if (null != mapGlowEffectA)
			{
				ParticleSystemRenderer component3 = mapGlowEffectA.GetComponent<ParticleSystemRenderer>();
				component3.set_minParticleSize(0.5f);
				component3.set_maxParticleSize(0.5f);
			}
			if (null != mapGlowEffectB)
			{
				ParticleSystemRenderer component4 = mapGlowEffectB.GetComponent<ParticleSystemRenderer>();
				component4.set_minParticleSize(0.5f);
				component4.set_maxParticleSize(0.5f);
			}
		}
	}

	protected override void OnOpen()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		UIEventListener uIEventListener = bgEventListener;
		uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener.onClick, new UIEventListener.VoidDelegate(onClick));
		worldMapObject.get_gameObject().SetActive(true);
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
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
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
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("WorldMapOpenNewRegion", this.get_gameObject(), "INGAME_MAIN");
			calledExit = true;
		}
		this.StopAllCoroutines();
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
			Object.Destroy(worldMapUIRoot);
		}
		if (worldMapObject != null)
		{
			Object.Destroy(worldMapObject.get_gameObject());
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
			worldMapObject.get_gameObject().SetActive(true);
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

	private IEnumerator DoFadeMap(float from, float to, float time, Action onComplete)
	{
		if (worldMapObject == null)
		{
			yield break;
		}
		Renderer r = worldMapObject.get_gameObject().GetComponentInChildren<Renderer>();
		if (!(r == null))
		{
			for (float timer = 0f; timer < time; timer += Time.get_deltaTime())
			{
				float alpha = Mathf.Lerp(from, to, timer / time);
				r.get_material().SetFloat("_Alpha", alpha);
				yield return null;
			}
			r.get_material().SetFloat("_Alpha", to);
			onComplete?.Invoke();
		}
	}

	private void GlowRegion(Vector3 from, Vector3 to)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		worldMapCamera.targetPos = from;
		this.StartCoroutine(DoGlowRegion(from, to));
	}

	private IEnumerator DoGlowRegion(Vector3 from, Vector3 to)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		yield return (object)new WaitForSeconds(0.5f);
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
		Transform toRegion = regionAreas[toRegionID];
		toRegion.get_gameObject().SetActive(true);
		Renderer toRegionRenderer = toRegion.GetComponent<Renderer>();
		toRegionRenderer.get_material().SetFloat("_Alpha", 0f);
		Renderer topRenderer = glowRegionTop.GetComponent<Renderer>();
		topRenderer.get_material().SetFloat("_Alpha", 0f);
		topRenderer.get_material().SetFloat("_AddColor", 1f);
		topRenderer.get_material().SetFloat("_BlendRate", 1f);
		topRenderer.set_sortingOrder(2);
		glowRegionTop.get_gameObject().SetActive(true);
		DelayExecute(1f, delegate
		{
			mapGlowEffectA.get_gameObject().SetActive(true);
			Renderer component = mapGlowEffectA.GetComponent<Renderer>();
			component.set_sortingOrder(1);
		});
		yield return (object)new WaitForSeconds(1f);
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
		mapGlowEffectB.get_gameObject().SetActive(true);
		yield return (object)new WaitForSeconds(0f);
		fip.Set(0.2f, 1f, 0f, null, 0f);
		fip.Play();
		while (fip.IsPlaying())
		{
			fip.Update();
			topRenderer.get_material().SetFloat("_Alpha", fip.Get());
			yield return null;
		}
		yield return (object)new WaitForSeconds(0f);
		targetRegionIcon.get_gameObject().SetActive(true);
		TweenScale tweenScale = targetRegionIcon.GetComponent<TweenScale>();
		tweenScale.PlayForward();
		yield return (object)new WaitForSeconds(1f);
		mapGlowEffectParticleB.Stop();
		bool isTweenEnd = false;
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
		yield return (object)new WaitForSeconds(0f);
		Vector3 scaleBegin = playerMarker.get_localScale();
		Vector3 scaleEnd = new Vector3(0f, 0f, 0f);
		ip.Set(0.5f, scaleBegin, scaleEnd);
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			playerMarker.set_localScale(ip.Get());
			yield return null;
		}
		RegionTable.Data targetData = openedRegionInfo[toRegionID].data;
		if (targetData != null)
		{
			playerMarker.set_localPosition(targetData.markerPos);
		}
		yield return (object)new WaitForSeconds(0.1f);
		ip.Set(0.5f, scaleEnd, scaleBegin);
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			playerMarker.set_localScale(ip.Get());
			yield return null;
		}
		yield return (object)new WaitForSeconds(0.4f);
		OnQuery_EXIT();
	}

	private void MoveCamera(Vector3 from, Vector3 to)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoMoveCamera(from, to));
	}

	private IEnumerator DoMoveCamera(Vector3 from, Vector3 to)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		Vector3Interpolator ip = new Vector3Interpolator();
		yield return (object)new WaitForSeconds(0.5f);
		Vector3 scaleBegin = playerMarker.get_localScale();
		Vector3 scaleEnd = new Vector3(0f, 0f, 0f);
		ip.Set(0.5f, scaleBegin, scaleEnd);
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			playerMarker.set_localScale(ip.Get());
			yield return null;
		}
		yield return (object)new WaitForSeconds(0f);
		ip.Set(0.7f, from, to);
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			worldMapCamera.targetPos = ip.Get();
			yield return null;
		}
		RegionTable.Data targetData = openedRegionInfo[toRegionID].data;
		if (targetData != null)
		{
			playerMarker.set_localPosition(targetData.markerPos);
		}
		yield return (object)new WaitForSeconds(0.1f);
		ip.Set(0.5f, scaleEnd, scaleBegin);
		ip.Play();
		while (ip.IsPlaying())
		{
			ip.Update();
			playerMarker.set_localScale(ip.Get());
			yield return null;
		}
		yield return (object)new WaitForSeconds(0.4f);
		OnQuery_EXIT();
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

	private void OnApplicationPause(bool paused)
	{
		isUpdateRenderTexture = !paused;
	}

	private void onClick(GameObject g)
	{
		OnQuery_EXIT();
	}
}
