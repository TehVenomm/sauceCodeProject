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
			openType = (OPEN_MAP_TYPE)eventData;
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
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject loadedExploreMapFrame = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreMapFrame");
		LoadObject loadedExploreMap = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreMap_" + regionId.ToString("D3"));
		LoadObject loadedPlayerMarker = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExplorePlayerMarker");
		LoadObject loadedCircle = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreCircle");
		LoadObject loadedBattleIcon = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreBattleMarker");
		LoadObject loadedFootprint = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreTraceMarker");
		LoadObject loadedFindIcon = null;
		LoadObject loadedDirSonar = null;
		LoadObject loadedSonarTexture = null;
		if (openType == OPEN_MAP_TYPE.SONAR)
		{
			loadedFindIcon = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreExclametionMarker");
			loadedDirSonar = loadingQueue.Load(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_sonar_02");
			loadedSonarTexture = loadingQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreSonarTexture");
			CacheAudio(loadingQueue);
		}
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		Transform transform = ResourceUtility.Realizes(loadedExploreMapFrame.loadedObject, base._transform);
		Transform transform2 = ResourceUtility.Realizes(loadedExploreMap.loadedObject, transform);
		transform2.Find("Map").gameObject.SetActive(value: true);
		mapRoot_ = transform2.GetComponent<ExploreMapRoot>();
		ExploreMapLocation[] locations = mapRoot_.locations;
		for (int i = 0; i < locations.Length; i++)
		{
			Transform transform3 = locations[i].transform.Find("ExploreSpotActive");
			Transform transform4 = locations[i].transform.Find("ExploreSpotInactive");
			Transform transform5 = locations[i].transform.Find("ExploreSpotSonar");
			transform3.gameObject.SetActive(value: true);
			transform4.gameObject.SetActive(value: false);
			List<FieldMapTable.FieldGimmickPointTableData> fieldGimmickPointListByMapID = Singleton<FieldMapTable>.I.GetFieldGimmickPointListByMapID((uint)locations[i].mapId);
			if (fieldGimmickPointListByMapID == null || !(transform5 != null))
			{
				continue;
			}
			for (int j = 0; j < fieldGimmickPointListByMapID.Count; j++)
			{
				if (fieldGimmickPointListByMapID[j].gimmickType == FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.SONAR)
				{
					transform3.gameObject.SetActive(value: false);
					transform4.gameObject.SetActive(value: false);
					transform5.gameObject.SetActive(value: true);
				}
			}
		}
		mapRoot_.UpdatePortals(isMiniMap: false);
		ExploreMapFrame component = transform.GetComponent<ExploreMapFrame>();
		component.SetMap(mapRoot_);
		RegionTable.Data data = Singleton<RegionTable>.I.GetData(regionId);
		if (data != null)
		{
			component.SetCaption(data.regionName);
		}
		for (int k = 0; k < playerMarkers_.Length; k++)
		{
			playerMarkers_[k] = ResourceUtility.Realizes(loadedPlayerMarker.loadedObject, transform);
			ExplorePlayerMarker component2 = playerMarkers_[k].GetComponent<ExplorePlayerMarker>();
			if (null != component2)
			{
				component2.SetIndex(k);
			}
			component2.gameObject.SetActive(value: false);
		}
		mapRoot_.SetMarkers(playerMarkers_, isMiniMap: false);
		ExploreStatus.TraceInfo[] bossTraceHistory = MonoBehaviourSingleton<QuestManager>.I.GetBossTraceHistory();
		if (bossTraceHistory != null && bossTraceHistory.Length != 0)
		{
			Transform transform6 = ResourceUtility.Realizes(loadedFootprint.loadedObject, transform2);
			ExploreStatus.TraceInfo traceInfo = bossTraceHistory[bossTraceHistory.Length - 1];
			Vector3 positionOnMap = mapRoot_.GetPositionOnMap(traceInfo.mapId);
			transform6.localPosition = new Vector3(positionOnMap.x + 22f, positionOnMap.y + 33f, positionOnMap.z);
			transform6.gameObject.SetActive(value: true);
			if (bossTraceHistory.Length > 1)
			{
				Transform transform7 = ResourceUtility.Realizes(loadedFootprint.loadedObject, transform2);
				ExploreStatus.TraceInfo traceInfo2 = bossTraceHistory[bossTraceHistory.Length - 2];
				Vector3 positionOnMap2 = mapRoot_.GetPositionOnMap(traceInfo2.mapId);
				transform7.localPosition = new Vector3(positionOnMap2.x + 22f, positionOnMap2.y + 33f, positionOnMap2.z);
				transform7.gameObject.SetActive(value: true);
			}
		}
		redCircle = ResourceUtility.Realizes(loadedCircle.loadedObject, transform2);
		redCircle.localScale = new Vector3(0.6f, 0.6f, 0.6f);
		battleIcon = ResourceUtility.Realizes(loadedBattleIcon.loadedObject, transform2);
		if (mapRoot_.showBattleMarker && openType != OPEN_MAP_TYPE.SONAR)
		{
			int exploreBossAppearMapId = MonoBehaviourSingleton<QuestManager>.I.GetExploreBossAppearMapId();
			Vector3 positionOnMap3 = mapRoot_.GetPositionOnMap(exploreBossAppearMapId);
			redCircle.localPosition = positionOnMap3;
			battleIcon.localPosition = new Vector3(positionOnMap3.x + 22f, positionOnMap3.y + 33f, positionOnMap3.z);
			TweenAlpha component3 = redCircle.GetComponent<TweenAlpha>();
			if (null != component3)
			{
				component3.from = component3.to;
			}
			redCircle.gameObject.SetActive(value: true);
			battleIcon.gameObject.SetActive(value: true);
		}
		else
		{
			redCircle.gameObject.SetActive(value: false);
			battleIcon.gameObject.SetActive(value: false);
		}
		if (openType == OPEN_MAP_TYPE.SONAR)
		{
			tapToSkip = Utility.FindChild(transform, "TaptoSkip");
			Transform transform8 = Utility.FindChild(transform, "BG");
			transform8.gameObject.SetActive(value: true);
			bgEventListener = UIEventListener.Get(transform8.gameObject);
			Transform transform9 = Utility.FindChild(transform, "CaptionRoot/Close");
			if (transform9 != null)
			{
				transform9.gameObject.SetActive(value: false);
			}
			findIcon = ResourceUtility.Realizes(loadedFindIcon.loadedObject, transform2);
			findIcon.gameObject.SetActive(value: false);
			sonarTexture = ResourceUtility.Realizes(loadedSonarTexture.loadedObject, transform2);
			UIRenderTexture uIRenderTexture = UIRenderTexture.Get(sonarTexture.gameObject.GetComponentInChildren<UITexture>(), mapRoot_.GetSonarFov());
			uIRenderTexture.modelTransform.localPosition = new Vector3(0f, 0f, 150f);
			sonarDirEffect = ResourceUtility.Realizes(loadedDirSonar.loadedObject, uIRenderTexture.modelTransform, uIRenderTexture.renderLayer);
			sonarDirEffect.localScale = mapRoot_.GetSonarScale();
			sonarDirEffect.gameObject.SetActive(value: false);
			uIRenderTexture.Enable();
			uIRenderTexture.renderCamera.backgroundColor = mapRoot_.sonarBackGroundColor;
			mapRoot_.SetDirectionSonar(sonarDirEffect.gameObject);
		}
		base.Initialize();
	}

	private IEnumerator StartSonar()
	{
		while (!base.isInitialized)
		{
			yield return null;
		}
		int bossMapId = MonoBehaviourSingleton<QuestManager>.I.GetExploreBossAppearMapId();
		Vector3 bossPos = mapRoot_.GetPositionOnMap(bossMapId);
		int currentMapID = (int)MonoBehaviourSingleton<FieldManager>.I.currentMapID;
		Vector3 selfPos = mapRoot_.GetPositionOnMap(currentMapID);
		float num = Vector3.Distance(bossPos, selfPos);
		float sonarSize = GetSonarSize(currentMapID);
		bool find = num <= sonarSize;
		SONAR_DIR dir = CalculateSonarDir(bossPos, selfPos);
		yield return new WaitForSeconds(0.7f);
		PlaySonarEffect(dir, selfPos, sonarSize);
		PlayAudio(AUDIO.SONAR);
		yield return new WaitForSeconds(2f);
		if (mapRoot_.showBattleMarker)
		{
			sonarDirEffect.gameObject.SetActive(value: false);
			MonoBehaviourSingleton<QuestManager>.I.GetExploreBossAppearMapId();
			Vector3 pos = mapRoot_.GetPositionOnMap(bossMapId);
			redCircle.localPosition = pos;
			redCircle.gameObject.SetActive(value: true);
			TweenAlpha tweenAlpha2 = redCircle.GetComponent<TweenAlpha>();
			if (null != tweenAlpha2)
			{
				while (tweenAlpha2.isActiveAndEnabled)
				{
					yield return null;
				}
			}
			yield return new WaitForSeconds(0.4f);
			battleIcon.localPosition = new Vector3(pos.x + 22f, pos.y + 33f, pos.z);
			battleIcon.gameObject.SetActive(value: true);
			PlayAudio(AUDIO.MARKER);
		}
		else if (find && !redCircle.gameObject.activeSelf)
		{
			sonarDirEffect.gameObject.SetActive(value: false);
			redCircle.localPosition = bossPos;
			redCircle.gameObject.SetActive(value: true);
			TweenAlpha tweenAlpha2 = redCircle.GetComponent<TweenAlpha>();
			if (null != tweenAlpha2)
			{
				while (tweenAlpha2.isActiveAndEnabled)
				{
					yield return null;
				}
			}
			yield return new WaitForSeconds(0.4f);
			findIcon.localPosition = new Vector3(bossPos.x + 22f, bossPos.y + 33f, bossPos.z);
			findIcon.gameObject.SetActive(value: true);
			PlayAudio(AUDIO.MARKER);
		}
		if (tapToSkip != null)
		{
			tapToSkip.gameObject.SetActive(value: true);
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
		sonarDirEffect.gameObject.SetActive(value: true);
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
		float deg = Mathf.Atan2(targetPos.y - currentPos.y, x) * 57.29578f;
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
			if (null != bgEventListener)
			{
				UIEventListener uIEventListener = bgEventListener;
				uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Remove(uIEventListener.onClick, new UIEventListener.VoidDelegate(onClick));
			}
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("ExploreMap", base.gameObject, "[BACK]");
			calledExit = true;
		}
	}
}
