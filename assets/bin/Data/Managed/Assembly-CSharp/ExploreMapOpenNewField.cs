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

	private Transform[] playerMarkers_ = (Transform[])new Transform[4];

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
		this.StartCoroutine("DoInitialize");
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
			fieldQuestWarningRoot = ResourceUtility.Realizes(loadedEncounterBossCutIn.loadedObject).get_gameObject();
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
		Transform map = map_.Find("Map");
		map.get_gameObject().SetActive(true);
		mapRoot_ = map_.GetComponent<ExploreMapRoot>();
		ExploreMapLocation[] locations = mapRoot_.locations;
		for (int i = 0; i < locations.Length; i++)
		{
			Transform val = locations[i].get_transform().Find("ExploreSpotActive");
			Transform val2 = locations[i].get_transform().Find("ExploreSpotInactive");
			Transform val3 = locations[i].get_transform().Find("ExploreSpotSonar");
			val.get_gameObject().SetActive(true);
			val2.get_gameObject().SetActive(false);
			List<FieldMapTable.FieldGimmickPointTableData> fieldGimmickPointListByMapID = Singleton<FieldMapTable>.I.GetFieldGimmickPointListByMapID((uint)locations[i].mapId);
			if (fieldGimmickPointListByMapID == null || !(val3 != null))
			{
				continue;
			}
			for (int j = 0; j < fieldGimmickPointListByMapID.Count; j++)
			{
				if (fieldGimmickPointListByMapID[j].gimmickType == FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.SONAR)
				{
					val.get_gameObject().SetActive(false);
					val2.get_gameObject().SetActive(false);
					val3.get_gameObject().SetActive(true);
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
		ExploreMapFrame frame = mapFrame_.GetComponent<ExploreMapFrame>();
		frame.SetMap(mapRoot_);
		RegionTable.Data regionData = Singleton<RegionTable>.I.GetData(regionId);
		if (regionData != null)
		{
			frame.SetCaption(regionData.regionName);
		}
		for (int k = 0; k < playerMarkers_.Length; k++)
		{
			playerMarkers_[k] = ResourceUtility.Realizes(loadedPlayerMarker.loadedObject, mapFrame_);
			ExplorePlayerMarker component2 = playerMarkers_[k].GetComponent<ExplorePlayerMarker>();
			if (null != component2)
			{
				component2.SetIndex(k);
			}
			component2.get_gameObject().SetActive(false);
		}
		selfMarker_ = playerMarkers_[0];
		Transform bg = mapFrame_.Find("BG");
		bg.get_gameObject().SetActive(true);
		bgEventListener = UIEventListener.Get(bg.get_gameObject());
		mapFrame_.Find("TaptoSkip").get_gameObject().SetActive(!toBoss_);
		mapFrame_.Find("CaptionRoot/Close").get_gameObject().SetActive(false);
		mapRoot_.SetMarkers(playerMarkers_, isMiniMap: false);
		ExploreStatus.TraceInfo[] traceHistory = MonoBehaviourSingleton<QuestManager>.I.GetBossTraceHistory();
		if (traceHistory != null && traceHistory.Length > 0)
		{
			Transform val4 = ResourceUtility.Realizes(loadedFootprint.loadedObject, map_);
			ExploreStatus.TraceInfo traceInfo = traceHistory[traceHistory.Length - 1];
			Vector3 positionOnMap = mapRoot_.GetPositionOnMap(traceInfo.mapId);
			val4.set_localPosition(new Vector3(positionOnMap.x + MarkerOffsetX, positionOnMap.y + MarkerOffsetY, positionOnMap.z));
			val4.get_gameObject().SetActive(true);
			if (traceHistory.Length > 1)
			{
				Transform val5 = ResourceUtility.Realizes(loadedFootprint.loadedObject, map_);
				ExploreStatus.TraceInfo traceInfo2 = traceHistory[traceHistory.Length - 2];
				Vector3 positionOnMap2 = mapRoot_.GetPositionOnMap(traceInfo2.mapId);
				val5.set_localPosition(new Vector3(positionOnMap2.x + MarkerOffsetX, positionOnMap2.y + MarkerOffsetY, positionOnMap2.z));
				val5.get_gameObject().SetActive(true);
			}
		}
		redCircle = ResourceUtility.Realizes(loadedCircle.loadedObject, map_);
		redCircle.set_localScale(new Vector3(0.6f, 0.6f, 0.6f));
		battleIcon = ResourceUtility.Realizes(loadedBattleIcon.loadedObject, map_);
		if (mapRoot_.showBattleMarker)
		{
			int exploreBossAppearMapId = MonoBehaviourSingleton<QuestManager>.I.GetExploreBossAppearMapId();
			Vector3 positionOnMap3 = mapRoot_.GetPositionOnMap(exploreBossAppearMapId);
			redCircle.set_localPosition(positionOnMap3);
			battleIcon.set_localPosition(new Vector3(positionOnMap3.x + MarkerOffsetX, positionOnMap3.y + MarkerOffsetY, positionOnMap3.z));
			TweenAlpha component3 = redCircle.GetComponent<TweenAlpha>();
			if (null != component3)
			{
				component3.from = component3.to;
			}
			redCircle.get_gameObject().SetActive(true);
			battleIcon.get_gameObject().SetActive(true);
		}
		else
		{
			redCircle.get_gameObject().SetActive(false);
			battleIcon.get_gameObject().SetActive(false);
		}
		base.Initialize();
	}

	private void OnQuery_EXIT()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() && !calledExit)
		{
			this.StopAllCoroutines();
			if (null != bgEventListener)
			{
				UIEventListener uIEventListener = bgEventListener;
				uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Remove(uIEventListener.onClick, new UIEventListener.VoidDelegate(onClick));
			}
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("ExploreMapOpenNewField", this.get_gameObject(), "INGAME_MAIN");
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
		this.StartCoroutine(DoExitEvent());
	}

	private IEnumerator DoExitEvent()
	{
		selfMarker_.get_gameObject().SetActive(true);
		Vector3 offset = new Vector3(0f, 0f, 0f);
		Transform portalNode = mapRoot_.FindPortalNode(from_.mapId, to_.mapId);
		if (null == portalNode)
		{
			ExploreMapLocation exploreMapLocation = mapRoot_.FindLocation(from_.mapId);
			ExploreMapLocation exploreMapLocation2 = mapRoot_.FindLocation(to_.mapId);
			if (null != exploreMapLocation && null != exploreMapLocation2)
			{
				Vector3 val = (exploreMapLocation.get_transform().get_localPosition() + exploreMapLocation2.get_transform().get_localPosition()) * 0.5f;
				val -= exploreMapLocation.get_transform().get_localPosition();
				offset = val;
				portalNode = exploreMapLocation.get_transform();
			}
		}
		if (null == portalNode)
		{
			yield return (object)new WaitForSeconds(0.1f);
			DispatchEvent("EXIT");
			yield break;
		}
		if (null != from_)
		{
			if (fromBoss_ && null != portalNode)
			{
				Utility.Attach(portalNode, selfMarker_);
				selfMarker_.GetComponent<ExplorePlayerMarker>().SetIndex(0);
				selfMarker_.set_localPosition(selfMarker_.get_localPosition() + offset);
			}
			else
			{
				Utility.Attach(from_.get_transform(), selfMarker_);
				selfMarker_.GetComponent<ExplorePlayerMarker>().SetIndex(0);
			}
			yield return (object)new WaitForSeconds(0.3f);
			TweenScale.Begin(selfMarker_.get_gameObject(), 0.3f, Vector3.get_zero());
			yield return (object)new WaitForSeconds(0.3f);
		}
		else
		{
			selfMarker_.set_localScale(Vector3.get_zero());
			yield return (object)new WaitForSeconds(0.3f);
		}
		yield return (object)new WaitForSeconds(0.3f);
		Utility.Attach(to_.get_transform(), selfMarker_);
		selfMarker_.GetComponent<ExplorePlayerMarker>().SetIndex(0);
		TweenScale.Begin(selfMarker_.get_gameObject(), 0.3f, Vector3.get_one());
		yield return (object)new WaitForSeconds(0.5f);
		if (toBoss_)
		{
			if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
			{
				MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Play(ENEMY_TYPE.NONE, MonoBehaviourSingleton<PartyManager>.I.partyData.quest.explore.isRare);
				MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.FadeOut(3f, 0.3f, delegate
				{
					if (fieldQuestWarningRoot != null)
					{
						Object.Destroy(fieldQuestWarningRoot);
					}
				});
			}
			this.StartCoroutine(DoExitEncounterBossEvent());
		}
		else
		{
			DispatchEvent("EXIT");
		}
	}

	private IEnumerator DoExitEncounterBossEvent()
	{
		yield return (object)new WaitForSeconds(3f);
		DispatchEvent("EXIT");
	}
}
