using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreMapOpenNewField : GameSection
{
	public class EventData
	{
		public uint regionId;

		public uint portalId;

		public bool fromBoss;

		public bool toBoss;
	}

	private UIEventListener bgEventListener;

	private bool calledExit;

	private Transform mapFrame_;

	private Transform map_;

	private ExploreMapRoot mapRoot_;

	private Transform[] playerMarkers_ = new Transform[4];

	private Transform selfMarker_;

	private FieldMapTable.PortalTableData portalData_;

	private ExploreMapLocation from_;

	private ExploreMapLocation to_;

	private bool fromBoss_;

	private bool toBoss_;

	private Transform redCircle;

	private Transform battleIcon;

	private GameObject fieldQuestWarningRoot;

	private const float WARNING_TIME = 3f;

	private float MarkerOffsetX = 22f;

	private float MarkerOffsetY = 33f;

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
		EventData eventData = GameSection.GetEventData() as EventData;
		if (eventData == null)
		{
			base.Initialize();
			yield break;
		}
		uint regionId = eventData.regionId;
		fromBoss_ = eventData.fromBoss;
		toBoss_ = eventData.toBoss;
		portalData_ = Singleton<FieldMapTable>.I.GetPortalData(eventData.portalId);
		if (portalData_ == null)
		{
			base.Initialize();
			yield break;
		}
		LoadingQueue loadQueue = new LoadingQueue(this);
		LoadObject loadedExploreMapFrame = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreMapFrame");
		LoadObject loadedExploreMap = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreMap_" + regionId.ToString("D3"));
		LoadObject loadedPlayerMarker = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExplorePlayerMarker");
		LoadObject loadedCircle = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreCircle");
		LoadObject loadedBattleIcon = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreBattleMarker");
		LoadObject loadedFootprint = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreTraceMarker");
		LoadObject loadedEncounterBossCutIn = null;
		if (toBoss_)
		{
			loadedEncounterBossCutIn = loadQueue.Load(RESOURCE_CATEGORY.UI, "InGameFieldQuestWarning");
		}
		if (loadQueue.IsLoading())
		{
			yield return loadQueue.Wait();
		}
		if (null == loadedExploreMap.loadedObject)
		{
			base.Initialize();
			yield break;
		}
		if (loadedEncounterBossCutIn != null)
		{
			fieldQuestWarningRoot = ResourceUtility.Realizes(loadedEncounterBossCutIn.loadedObject).gameObject;
			UIPanel componentInChildren = fieldQuestWarningRoot.GetComponentInChildren<UIPanel>();
			if (componentInChildren != null)
			{
				componentInChildren.depth = 12000;
			}
			if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
			{
				MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Load(loadQueue);
			}
		}
		mapFrame_ = ResourceUtility.Realizes(loadedExploreMapFrame.loadedObject, base._transform);
		if (toBoss_)
		{
			UIPanel component = mapFrame_.GetComponent<UIPanel>();
			component.renderQueue = UIPanel.RenderQueue.StartAt;
			component.startingRenderQueue = 2900;
		}
		map_ = ResourceUtility.Realizes(loadedExploreMap.loadedObject, mapFrame_);
		map_.Find("Map").gameObject.SetActive(value: true);
		mapRoot_ = map_.GetComponent<ExploreMapRoot>();
		ExploreMapLocation[] locations = mapRoot_.locations;
		for (int i = 0; i < locations.Length; i++)
		{
			Transform transform = locations[i].transform.Find("ExploreSpotActive");
			Transform transform2 = locations[i].transform.Find("ExploreSpotInactive");
			Transform transform3 = locations[i].transform.Find("ExploreSpotSonar");
			transform.gameObject.SetActive(value: true);
			transform2.gameObject.SetActive(value: false);
			List<FieldMapTable.FieldGimmickPointTableData> fieldGimmickPointListByMapID = Singleton<FieldMapTable>.I.GetFieldGimmickPointListByMapID((uint)locations[i].mapId);
			if (fieldGimmickPointListByMapID == null || !(transform3 != null))
			{
				continue;
			}
			for (int j = 0; j < fieldGimmickPointListByMapID.Count; j++)
			{
				if (fieldGimmickPointListByMapID[j].gimmickType == FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.SONAR)
				{
					transform.gameObject.SetActive(value: false);
					transform2.gameObject.SetActive(value: false);
					transform3.gameObject.SetActive(value: true);
				}
			}
		}
		mapRoot_.UpdatePortals(isMiniMap: false);
		from_ = mapRoot_.FindLocation((int)portalData_.srcMapID);
		to_ = mapRoot_.FindLocation((int)portalData_.dstMapID);
		if (null == to_)
		{
			base.Initialize();
			yield break;
		}
		ExploreMapFrame component2 = mapFrame_.GetComponent<ExploreMapFrame>();
		component2.SetMap(mapRoot_);
		RegionTable.Data data = Singleton<RegionTable>.I.GetData(regionId);
		if (data != null)
		{
			component2.SetCaption(data.regionName);
		}
		for (int k = 0; k < playerMarkers_.Length; k++)
		{
			playerMarkers_[k] = ResourceUtility.Realizes(loadedPlayerMarker.loadedObject, mapFrame_);
			ExplorePlayerMarker component3 = playerMarkers_[k].GetComponent<ExplorePlayerMarker>();
			if (null != component3)
			{
				component3.SetIndex(k);
			}
			component3.gameObject.SetActive(value: false);
		}
		selfMarker_ = playerMarkers_[0];
		Transform transform4 = mapFrame_.Find("BG");
		transform4.gameObject.SetActive(value: true);
		bgEventListener = UIEventListener.Get(transform4.gameObject);
		mapFrame_.Find("TaptoSkip").gameObject.SetActive(!toBoss_);
		mapFrame_.Find("CaptionRoot/Close").gameObject.SetActive(value: false);
		mapRoot_.SetMarkers(playerMarkers_, isMiniMap: false);
		ExploreStatus.TraceInfo[] bossTraceHistory = MonoBehaviourSingleton<QuestManager>.I.GetBossTraceHistory();
		if (bossTraceHistory != null && bossTraceHistory.Length != 0)
		{
			Transform transform5 = ResourceUtility.Realizes(loadedFootprint.loadedObject, map_);
			ExploreStatus.TraceInfo traceInfo = bossTraceHistory[bossTraceHistory.Length - 1];
			Vector3 positionOnMap = mapRoot_.GetPositionOnMap(traceInfo.mapId);
			transform5.localPosition = new Vector3(positionOnMap.x + MarkerOffsetX, positionOnMap.y + MarkerOffsetY, positionOnMap.z);
			transform5.gameObject.SetActive(value: true);
			if (bossTraceHistory.Length > 1)
			{
				Transform transform6 = ResourceUtility.Realizes(loadedFootprint.loadedObject, map_);
				ExploreStatus.TraceInfo traceInfo2 = bossTraceHistory[bossTraceHistory.Length - 2];
				Vector3 positionOnMap2 = mapRoot_.GetPositionOnMap(traceInfo2.mapId);
				transform6.localPosition = new Vector3(positionOnMap2.x + MarkerOffsetX, positionOnMap2.y + MarkerOffsetY, positionOnMap2.z);
				transform6.gameObject.SetActive(value: true);
			}
		}
		redCircle = ResourceUtility.Realizes(loadedCircle.loadedObject, map_);
		redCircle.localScale = new Vector3(0.6f, 0.6f, 0.6f);
		battleIcon = ResourceUtility.Realizes(loadedBattleIcon.loadedObject, map_);
		if (mapRoot_.showBattleMarker)
		{
			int exploreBossAppearMapId = MonoBehaviourSingleton<QuestManager>.I.GetExploreBossAppearMapId();
			Vector3 positionOnMap3 = mapRoot_.GetPositionOnMap(exploreBossAppearMapId);
			redCircle.localPosition = positionOnMap3;
			battleIcon.localPosition = new Vector3(positionOnMap3.x + MarkerOffsetX, positionOnMap3.y + MarkerOffsetY, positionOnMap3.z);
			TweenAlpha component4 = redCircle.GetComponent<TweenAlpha>();
			if (null != component4)
			{
				component4.from = component4.to;
			}
			redCircle.gameObject.SetActive(value: true);
			battleIcon.gameObject.SetActive(value: true);
		}
		else
		{
			redCircle.gameObject.SetActive(value: false);
			battleIcon.gameObject.SetActive(value: false);
		}
		base.Initialize();
	}

	private void OnQuery_EXIT()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() && !calledExit)
		{
			StopAllCoroutines();
			if (null != bgEventListener)
			{
				UIEventListener uIEventListener = bgEventListener;
				uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Remove(uIEventListener.onClick, new UIEventListener.VoidDelegate(onClick));
			}
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("ExploreMapOpenNewField", base.gameObject, "INGAME_MAIN");
			calledExit = true;
		}
	}

	private void onClick(GameObject g)
	{
		if (!toBoss_)
		{
			OnQuery_EXIT();
		}
	}

	protected override void OnOpen()
	{
		if (null == bgEventListener)
		{
			DispatchEvent("EXIT");
			return;
		}
		UIEventListener uIEventListener = bgEventListener;
		uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener.onClick, new UIEventListener.VoidDelegate(onClick));
		StartCoroutine(DoExitEvent());
	}

	private IEnumerator DoExitEvent()
	{
		selfMarker_.gameObject.SetActive(value: true);
		Vector3 b = new Vector3(0f, 0f, 0f);
		Transform transform = mapRoot_.FindPortalNode(from_.mapId, to_.mapId);
		if (null == transform)
		{
			ExploreMapLocation exploreMapLocation = mapRoot_.FindLocation(from_.mapId);
			ExploreMapLocation exploreMapLocation2 = mapRoot_.FindLocation(to_.mapId);
			if (null != exploreMapLocation && null != exploreMapLocation2)
			{
				b = (exploreMapLocation.transform.localPosition + exploreMapLocation2.transform.localPosition) * 0.5f - exploreMapLocation.transform.localPosition;
				transform = exploreMapLocation.transform;
			}
		}
		if (null == transform)
		{
			yield return new WaitForSeconds(0.1f);
			DispatchEvent("EXIT");
			yield break;
		}
		if (null != from_)
		{
			if (fromBoss_ && null != transform)
			{
				Utility.Attach(transform, selfMarker_);
				selfMarker_.GetComponent<ExplorePlayerMarker>().SetIndex(0);
				selfMarker_.localPosition += b;
			}
			else
			{
				Utility.Attach(from_.transform, selfMarker_);
				selfMarker_.GetComponent<ExplorePlayerMarker>().SetIndex(0);
			}
			yield return new WaitForSeconds(0.3f);
			TweenScale.Begin(selfMarker_.gameObject, 0.3f, Vector3.zero);
			yield return new WaitForSeconds(0.3f);
		}
		else
		{
			selfMarker_.localScale = Vector3.zero;
			yield return new WaitForSeconds(0.3f);
		}
		yield return new WaitForSeconds(0.3f);
		Utility.Attach(to_.transform, selfMarker_);
		selfMarker_.GetComponent<ExplorePlayerMarker>().SetIndex(0);
		TweenScale.Begin(selfMarker_.gameObject, 0.3f, Vector3.one);
		yield return new WaitForSeconds(0.5f);
		if (toBoss_)
		{
			if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
			{
				MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Play(ENEMY_TYPE.NONE, MonoBehaviourSingleton<PartyManager>.I.partyData.quest.explore.isRare);
				MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.FadeOut(3f, 0.3f, delegate
				{
					if (fieldQuestWarningRoot != null)
					{
						UnityEngine.Object.Destroy(fieldQuestWarningRoot);
					}
				});
			}
			StartCoroutine(DoExitEncounterBossEvent());
		}
		else
		{
			DispatchEvent("EXIT");
		}
	}

	private IEnumerator DoExitEncounterBossEvent()
	{
		yield return new WaitForSeconds(3f);
		DispatchEvent("EXIT");
	}
}
