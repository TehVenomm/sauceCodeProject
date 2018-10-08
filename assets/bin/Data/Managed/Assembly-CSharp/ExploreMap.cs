using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreMap : GameSection
{
	public enum OPEN_MAP_TYPE
	{
		NORMAL,
		SONAR
	}

	private enum SONAR_DIR
	{
		NONE,
		UP,
		DOWN,
		LEFT,
		RIGHT
	}

	private enum AUDIO
	{
		SONAR = 40000094,
		MARKER = 40000125
	}

	private const float MarkerOffsetX = 22f;

	private const float MarkerOffsetY = 33f;

	private OPEN_MAP_TYPE openType;

	private Transform[] playerMarkers_ = new Transform[4];

	private ExploreMapRoot mapRoot_;

	private Transform findIcon;

	private Transform battleIcon;

	private Transform sonarDirEffect;

	private Transform redCircle;

	private Transform sonarTexture;

	private bool calledExit;

	private Transform tapToSkip;

	private UIEventListener bgEventListener;

	public override string overrideBackKeyEvent => "CLOSE";

	public override void Initialize()
	{
		object eventData = GameSection.GetEventData();
		if (eventData is OPEN_MAP_TYPE)
		{
			openType = (OPEN_MAP_TYPE)(int)eventData;
		}
		StartCoroutine(DoInitialize());
		if (openType == OPEN_MAP_TYPE.SONAR)
		{
			StartCoroutine(StartSonar());
		}
	}

	private IEnumerator DoInitialize()
	{
		uint regionId = MonoBehaviourSingleton<FieldManager>.I.currentMapData.regionId;
		LoadingQueue loadQueue = new LoadingQueue(this);
		LoadObject loadedExploreMapFrame = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreMapFrame", false);
		LoadObject loadedExploreMap = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreMap_" + regionId.ToString("D3"), false);
		LoadObject loadedPlayerMarker = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExplorePlayerMarker", false);
		LoadObject loadedCircle = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreCircle", false);
		LoadObject loadedBattleIcon = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreBattleMarker", false);
		LoadObject loadedFootprint = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreTraceMarker", false);
		LoadObject loadedFindIcon = null;
		LoadObject loadedDirSonar = null;
		LoadObject loadedSonarTexture = null;
		if (openType == OPEN_MAP_TYPE.SONAR)
		{
			loadedFindIcon = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreExclametionMarker", false);
			loadedDirSonar = loadQueue.Load(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_sonar_02", false);
			loadedSonarTexture = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreSonarTexture", false);
			CacheAudio(loadQueue);
		}
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		Transform exploreMapFrame = ResourceUtility.Realizes(loadedExploreMapFrame.loadedObject, base._transform, -1);
		Transform exploreMap = ResourceUtility.Realizes(loadedExploreMap.loadedObject, exploreMapFrame, -1);
		Transform map = exploreMap.FindChild("Map");
		map.gameObject.SetActive(true);
		mapRoot_ = exploreMap.GetComponent<ExploreMapRoot>();
		ExploreMapLocation[] locations = mapRoot_.locations;
		for (int k = 0; k < locations.Length; k++)
		{
			Transform active = locations[k].transform.FindChild("ExploreSpotActive");
			Transform inactive = locations[k].transform.FindChild("ExploreSpotInactive");
			Transform sonar = locations[k].transform.FindChild("ExploreSpotSonar");
			active.gameObject.SetActive(true);
			inactive.gameObject.SetActive(false);
			List<FieldMapTable.FieldGimmickPointTableData> gimmicks = Singleton<FieldMapTable>.I.GetFieldGimmickPointListByMapID((uint)locations[k].mapId);
			if (gimmicks != null && (UnityEngine.Object)sonar != (UnityEngine.Object)null)
			{
				for (int i = 0; i < gimmicks.Count; i++)
				{
					if (gimmicks[i].gimmickType == FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.SONAR)
					{
						active.gameObject.SetActive(false);
						inactive.gameObject.SetActive(false);
						sonar.gameObject.SetActive(true);
					}
				}
			}
		}
		mapRoot_.UpdatePortals(false);
		ExploreMapFrame frame = exploreMapFrame.GetComponent<ExploreMapFrame>();
		frame.SetMap(mapRoot_);
		RegionTable.Data regionData = Singleton<RegionTable>.I.GetData(regionId);
		if (regionData != null)
		{
			frame.SetCaption(regionData.regionName);
		}
		for (int j = 0; j < playerMarkers_.Length; j++)
		{
			playerMarkers_[j] = ResourceUtility.Realizes(loadedPlayerMarker.loadedObject, exploreMapFrame, -1);
			ExplorePlayerMarker marker = playerMarkers_[j].GetComponent<ExplorePlayerMarker>();
			if ((UnityEngine.Object)null != (UnityEngine.Object)marker)
			{
				marker.SetIndex(j);
			}
			marker.gameObject.SetActive(false);
		}
		mapRoot_.SetMarkers(playerMarkers_, false);
		ExploreStatus.TraceInfo[] traceHistory = MonoBehaviourSingleton<QuestManager>.I.GetBossTraceHistory();
		if (traceHistory != null && traceHistory.Length > 0)
		{
			Transform lastFootprint = ResourceUtility.Realizes(loadedFootprint.loadedObject, exploreMap, -1);
			ExploreStatus.TraceInfo lastInfo = traceHistory[traceHistory.Length - 1];
			Vector3 lastPos = mapRoot_.GetPositionOnMap(lastInfo.mapId);
			lastFootprint.localPosition = new Vector3(lastPos.x + 22f, lastPos.y + 33f, lastPos.z);
			lastFootprint.gameObject.SetActive(true);
			if (traceHistory.Length > 1)
			{
				Transform lastSecondFootprint = ResourceUtility.Realizes(loadedFootprint.loadedObject, exploreMap, -1);
				ExploreStatus.TraceInfo lastSecondInfo = traceHistory[traceHistory.Length - 2];
				Vector3 lastSecondPos = mapRoot_.GetPositionOnMap(lastSecondInfo.mapId);
				lastSecondFootprint.localPosition = new Vector3(lastSecondPos.x + 22f, lastSecondPos.y + 33f, lastSecondPos.z);
				lastSecondFootprint.gameObject.SetActive(true);
			}
		}
		redCircle = ResourceUtility.Realizes(loadedCircle.loadedObject, exploreMap, -1);
		redCircle.localScale = new Vector3(0.6f, 0.6f, 0.6f);
		battleIcon = ResourceUtility.Realizes(loadedBattleIcon.loadedObject, exploreMap, -1);
		if (mapRoot_.showBattleMarker && openType != OPEN_MAP_TYPE.SONAR)
		{
			int bossMapId = MonoBehaviourSingleton<QuestManager>.I.GetExploreBossAppearMapId();
			Vector3 pos = mapRoot_.GetPositionOnMap(bossMapId);
			redCircle.localPosition = pos;
			battleIcon.localPosition = new Vector3(pos.x + 22f, pos.y + 33f, pos.z);
			TweenAlpha tweenAlpha = redCircle.GetComponent<TweenAlpha>();
			if ((UnityEngine.Object)null != (UnityEngine.Object)tweenAlpha)
			{
				tweenAlpha.from = tweenAlpha.to;
			}
			redCircle.gameObject.SetActive(true);
			battleIcon.gameObject.SetActive(true);
		}
		else
		{
			redCircle.gameObject.SetActive(false);
			battleIcon.gameObject.SetActive(false);
		}
		if (openType == OPEN_MAP_TYPE.SONAR)
		{
			tapToSkip = Utility.FindChild(exploreMapFrame, "TaptoSkip");
			Transform backGround = Utility.FindChild(exploreMapFrame, "BG");
			backGround.gameObject.SetActive(true);
			bgEventListener = UIEventListener.Get(backGround.gameObject);
			Transform close = Utility.FindChild(exploreMapFrame, "CaptionRoot/Close");
			if ((UnityEngine.Object)close != (UnityEngine.Object)null)
			{
				close.gameObject.SetActive(false);
			}
			findIcon = ResourceUtility.Realizes(loadedFindIcon.loadedObject, exploreMap, -1);
			findIcon.gameObject.SetActive(false);
			sonarTexture = ResourceUtility.Realizes(loadedSonarTexture.loadedObject, exploreMap, -1);
			UITexture uiTex = sonarTexture.gameObject.GetComponentInChildren<UITexture>();
			UIRenderTexture renderTexture = UIRenderTexture.Get(uiTex, mapRoot_.GetSonarFov(), false, -1);
			renderTexture.modelTransform.localPosition = new Vector3(0f, 0f, 150f);
			sonarDirEffect = ResourceUtility.Realizes(loadedDirSonar.loadedObject, renderTexture.modelTransform, renderTexture.renderLayer);
			sonarDirEffect.localScale = mapRoot_.GetSonarScale();
			sonarDirEffect.gameObject.SetActive(false);
			renderTexture.Enable(0.25f);
			renderTexture.renderCamera.backgroundColor = mapRoot_.sonarBackGroundColor;
			mapRoot_.SetDirectionSonar(sonarDirEffect.gameObject);
		}
		base.Initialize();
	}

	private IEnumerator StartSonar()
	{
		while (!base.isInitialized)
		{
			yield return (object)null;
		}
		int bossMapId = MonoBehaviourSingleton<QuestManager>.I.GetExploreBossAppearMapId();
		Vector3 bossPos = mapRoot_.GetPositionOnMap(bossMapId);
		int currentMapId = (int)MonoBehaviourSingleton<FieldManager>.I.currentMapID;
		Vector3 selfPos = mapRoot_.GetPositionOnMap(currentMapId);
		float distance = Vector3.Distance(bossPos, selfPos);
		float sonarSize = GetSonarSize(currentMapId);
		bool find = distance <= sonarSize;
		SONAR_DIR dir = CalculateSonarDir(bossPos, selfPos);
		yield return (object)new WaitForSeconds(0.7f);
		PlaySonarEffect(dir, selfPos, sonarSize);
		PlayAudio(AUDIO.SONAR);
		yield return (object)new WaitForSeconds(2f);
		if (mapRoot_.showBattleMarker)
		{
			sonarDirEffect.gameObject.SetActive(false);
			MonoBehaviourSingleton<QuestManager>.I.GetExploreBossAppearMapId();
			Vector3 pos = mapRoot_.GetPositionOnMap(bossMapId);
			redCircle.localPosition = pos;
			redCircle.gameObject.SetActive(true);
			TweenAlpha tweenAlpha2 = redCircle.GetComponent<TweenAlpha>();
			if ((UnityEngine.Object)null != (UnityEngine.Object)tweenAlpha2)
			{
				while (tweenAlpha2.isActiveAndEnabled)
				{
					yield return (object)null;
				}
			}
			yield return (object)new WaitForSeconds(0.4f);
			battleIcon.localPosition = new Vector3(pos.x + 22f, pos.y + 33f, pos.z);
			battleIcon.gameObject.SetActive(true);
			PlayAudio(AUDIO.MARKER);
		}
		else if (find && !redCircle.gameObject.activeSelf)
		{
			sonarDirEffect.gameObject.SetActive(false);
			redCircle.localPosition = bossPos;
			redCircle.gameObject.SetActive(true);
			TweenAlpha tweenAlpha = redCircle.GetComponent<TweenAlpha>();
			if ((UnityEngine.Object)null != (UnityEngine.Object)tweenAlpha)
			{
				while (tweenAlpha.isActiveAndEnabled)
				{
					yield return (object)null;
				}
			}
			yield return (object)new WaitForSeconds(0.4f);
			findIcon.localPosition = new Vector3(bossPos.x + 22f, bossPos.y + 33f, bossPos.z);
			findIcon.gameObject.SetActive(true);
			PlayAudio(AUDIO.MARKER);
		}
		if ((UnityEngine.Object)tapToSkip != (UnityEngine.Object)null)
		{
			tapToSkip.gameObject.SetActive(true);
			UIEventListener uIEventListener = bgEventListener;
			uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener.onClick, new UIEventListener.VoidDelegate(onClick));
		}
	}

	private float GetSonarSize(int mapId)
	{
		List<FieldMapTable.FieldGimmickPointTableData> fieldGimmickPointListByMapID = Singleton<FieldMapTable>.I.GetFieldGimmickPointListByMapID((uint)mapId);
		if (fieldGimmickPointListByMapID == null)
		{
			return 0f;
		}
		float result = 0f;
		for (int i = 0; i < fieldGimmickPointListByMapID.Count; i++)
		{
			if (fieldGimmickPointListByMapID[i].gimmickType == FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.SONAR)
			{
				result = fieldGimmickPointListByMapID[i].value1;
			}
		}
		return result;
	}

	private void PlaySonarEffect(SONAR_DIR dir, Vector3 pos, float size)
	{
		sonarTexture.localPosition = pos;
		float num = size / 100f * mapRoot_.GetMapScale() * mapRoot_.GetSonarOffset();
		Vector3 localScale = sonarTexture.localScale;
		sonarTexture.localScale = new Vector3(localScale.x * num, localScale.y * num, localScale.z);
		sonarDirEffect.gameObject.SetActive(true);
		RotateSonarEffect(dir);
	}

	private void RotateSonarEffect(SONAR_DIR dir)
	{
		switch (dir)
		{
		case SONAR_DIR.UP:
			sonarTexture.Rotate(new Vector3(0f, 0f, 0f));
			break;
		case SONAR_DIR.DOWN:
			sonarTexture.Rotate(new Vector3(0f, 0f, 180f));
			break;
		case SONAR_DIR.LEFT:
			sonarTexture.Rotate(new Vector3(0f, 0f, 90f));
			break;
		case SONAR_DIR.RIGHT:
			sonarTexture.Rotate(new Vector3(0f, 0f, -90f));
			break;
		}
	}

	private SONAR_DIR CalculateSonarDir(Vector3 targetPos, Vector3 currentPos)
	{
		float x = targetPos.x - currentPos.x;
		float y = targetPos.y - currentPos.y;
		float num = Mathf.Atan2(y, x);
		float deg = num * 57.29578f;
		return GetSonarDir(deg);
	}

	private SONAR_DIR GetSonarDir(float deg)
	{
		if (deg >= 45f && deg < 135f)
		{
			return SONAR_DIR.UP;
		}
		if (deg >= -135f && deg < -45f)
		{
			return SONAR_DIR.DOWN;
		}
		if (deg >= -45f && deg < 45f)
		{
			return SONAR_DIR.RIGHT;
		}
		if (deg >= 135f && deg < 180f)
		{
			return SONAR_DIR.LEFT;
		}
		if (deg >= -180f && deg < -135f)
		{
			return SONAR_DIR.LEFT;
		}
		return SONAR_DIR.NONE;
	}

	private void PlayAudio(AUDIO type)
	{
		SoundManager.PlayOneShotUISE((int)type);
	}

	private void onClick(GameObject g)
	{
		OnQuery_EXIT();
	}

	private void OnQuery_EXIT()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() && !calledExit)
		{
			if ((UnityEngine.Object)null != (UnityEngine.Object)bgEventListener)
			{
				UIEventListener uIEventListener = bgEventListener;
				uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Remove(uIEventListener.onClick, new UIEventListener.VoidDelegate(onClick));
			}
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("ExploreMap", base.gameObject, "[BACK]", null, null, true);
			calledExit = true;
		}
	}
}
