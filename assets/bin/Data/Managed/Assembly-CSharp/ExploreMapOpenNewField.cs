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

	private const float WARNING_TIME = 3f;

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
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine("DoInitialize");
	}

	private IEnumerator DoInitialize()
	{
		EventData eventData = GameSection.GetEventData() as EventData;
		if (eventData == null)
		{
			base.Initialize();
		}
		else
		{
			uint regionId = eventData.regionId;
			fromBoss_ = eventData.fromBoss;
			toBoss_ = eventData.toBoss;
			portalData_ = Singleton<FieldMapTable>.I.GetPortalData(eventData.portalId);
			if (portalData_ == null)
			{
				base.Initialize();
			}
			else
			{
				LoadingQueue loadQueue = new LoadingQueue(this);
				LoadObject loadedExploreMapFrame = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreMapFrame", false);
				LoadObject loadedExploreMap = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreMap_" + regionId.ToString("D3"), false);
				LoadObject loadedPlayerMarker = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExplorePlayerMarker", false);
				LoadObject loadedCircle = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreCircle", false);
				LoadObject loadedBattleIcon = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreBattleMarker", false);
				LoadObject loadedFootprint = loadQueue.Load(RESOURCE_CATEGORY.WORLDMAP, "ExploreTraceMarker", false);
				LoadObject loadedEncounterBossCutIn = null;
				if (toBoss_)
				{
					loadedEncounterBossCutIn = loadQueue.Load(RESOURCE_CATEGORY.UI, "InGameFieldQuestWarning", false);
				}
				if (loadQueue.IsLoading())
				{
					yield return (object)loadQueue.Wait();
				}
				if (null == loadedExploreMap.loadedObject)
				{
					base.Initialize();
				}
				else
				{
					if (loadedEncounterBossCutIn != null)
					{
						fieldQuestWarningRoot = ResourceUtility.Realizes(loadedEncounterBossCutIn.loadedObject, -1).get_gameObject();
						UIPanel panel2 = fieldQuestWarningRoot.GetComponentInChildren<UIPanel>();
						if (panel2 != null)
						{
							panel2.depth = 12000;
						}
						if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
						{
							MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Load(loadQueue);
						}
					}
					mapFrame_ = ResourceUtility.Realizes(loadedExploreMapFrame.loadedObject, base._transform, -1);
					if (toBoss_)
					{
						UIPanel panel = mapFrame_.GetComponent<UIPanel>();
						panel.renderQueue = UIPanel.RenderQueue.StartAt;
						panel.startingRenderQueue = 2900;
					}
					map_ = ResourceUtility.Realizes(loadedExploreMap.loadedObject, mapFrame_, -1);
					Transform map = map_.FindChild("Map");
					map.get_gameObject().SetActive(true);
					mapRoot_ = map_.GetComponent<ExploreMapRoot>();
					ExploreMapLocation[] locations = mapRoot_.locations;
					for (int k = 0; k < locations.Length; k++)
					{
						Transform active = locations[k].get_transform().FindChild("ExploreSpotActive");
						Transform inactive = locations[k].get_transform().FindChild("ExploreSpotInactive");
						Transform sonar = locations[k].get_transform().FindChild("ExploreSpotSonar");
						active.get_gameObject().SetActive(true);
						inactive.get_gameObject().SetActive(false);
						List<FieldMapTable.FieldGimmickPointTableData> gimmicks = Singleton<FieldMapTable>.I.GetFieldGimmickPointListByMapID((uint)locations[k].mapId);
						if (gimmicks != null && sonar != null)
						{
							for (int i = 0; i < gimmicks.Count; i++)
							{
								if (gimmicks[i].gimmickType == FieldMapTable.FieldGimmickPointTableData.GIMMICK_TYPE.SONAR)
								{
									active.get_gameObject().SetActive(false);
									inactive.get_gameObject().SetActive(false);
									sonar.get_gameObject().SetActive(true);
								}
							}
						}
					}
					mapRoot_.UpdatePortals(false);
					from_ = mapRoot_.FindLocation((int)portalData_.srcMapID);
					to_ = mapRoot_.FindLocation((int)portalData_.dstMapID);
					if (null == to_)
					{
						base.Initialize();
					}
					else
					{
						ExploreMapFrame frame = mapFrame_.GetComponent<ExploreMapFrame>();
						frame.SetMap(mapRoot_);
						RegionTable.Data regionData = Singleton<RegionTable>.I.GetData(regionId);
						if (regionData != null)
						{
							frame.SetCaption(regionData.regionName);
						}
						for (int j = 0; j < playerMarkers_.Length; j++)
						{
							playerMarkers_[j] = ResourceUtility.Realizes(loadedPlayerMarker.loadedObject, mapFrame_, -1);
							ExplorePlayerMarker marker = playerMarkers_[j].GetComponent<ExplorePlayerMarker>();
							if (null != marker)
							{
								marker.SetIndex(j);
							}
							marker.get_gameObject().SetActive(false);
						}
						selfMarker_ = playerMarkers_[0];
						Transform bg = mapFrame_.FindChild("BG");
						bg.get_gameObject().SetActive(true);
						bgEventListener = UIEventListener.Get(bg.get_gameObject());
						mapFrame_.FindChild("TaptoSkip").get_gameObject().SetActive(!toBoss_);
						mapFrame_.FindChild("CaptionRoot/Close").get_gameObject().SetActive(false);
						mapRoot_.SetMarkers(playerMarkers_, false);
						ExploreStatus.TraceInfo[] traceHistory = MonoBehaviourSingleton<QuestManager>.I.GetBossTraceHistory();
						if (traceHistory != null && traceHistory.Length > 0)
						{
							Transform lastFootprint = ResourceUtility.Realizes(loadedFootprint.loadedObject, map_, -1);
							ExploreStatus.TraceInfo lastInfo = traceHistory[traceHistory.Length - 1];
							Vector3 lastPos = mapRoot_.GetPositionOnMap(lastInfo.mapId);
							lastFootprint.set_localPosition(new Vector3(lastPos.x + MarkerOffsetX, lastPos.y + MarkerOffsetY, lastPos.z));
							lastFootprint.get_gameObject().SetActive(true);
							if (traceHistory.Length > 1)
							{
								Transform lastSecondFootprint = ResourceUtility.Realizes(loadedFootprint.loadedObject, map_, -1);
								ExploreStatus.TraceInfo lastSecondInfo = traceHistory[traceHistory.Length - 2];
								Vector3 lastSecondPos = mapRoot_.GetPositionOnMap(lastSecondInfo.mapId);
								lastSecondFootprint.set_localPosition(new Vector3(lastSecondPos.x + MarkerOffsetX, lastSecondPos.y + MarkerOffsetY, lastSecondPos.z));
								lastSecondFootprint.get_gameObject().SetActive(true);
							}
						}
						redCircle = ResourceUtility.Realizes(loadedCircle.loadedObject, map_, -1);
						redCircle.set_localScale(new Vector3(0.6f, 0.6f, 0.6f));
						battleIcon = ResourceUtility.Realizes(loadedBattleIcon.loadedObject, map_, -1);
						if (mapRoot_.showBattleMarker)
						{
							int bossMapId = MonoBehaviourSingleton<QuestManager>.I.GetExploreBossAppearMapId();
							Vector3 pos = mapRoot_.GetPositionOnMap(bossMapId);
							redCircle.set_localPosition(pos);
							battleIcon.set_localPosition(new Vector3(pos.x + MarkerOffsetX, pos.y + MarkerOffsetY, pos.z));
							TweenAlpha tweenAlpha = redCircle.GetComponent<TweenAlpha>();
							if (null != tweenAlpha)
							{
								tweenAlpha.from = tweenAlpha.to;
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
				}
			}
		}
	}

	private void OnQuery_EXIT()
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() && !calledExit)
		{
			this.StopAllCoroutines();
			if (null != bgEventListener)
			{
				UIEventListener uIEventListener = bgEventListener;
				uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Remove(uIEventListener.onClick, new UIEventListener.VoidDelegate(onClick));
			}
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("ExploreMapOpenNewField", this.get_gameObject(), "INGAME_MAIN", null, null, true);
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
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		if (null == bgEventListener)
		{
			DispatchEvent("EXIT", null);
		}
		else
		{
			UIEventListener uIEventListener = bgEventListener;
			uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener.onClick, new UIEventListener.VoidDelegate(onClick));
			this.StartCoroutine(DoExitEvent());
		}
	}

	private unsafe IEnumerator DoExitEvent()
	{
		selfMarker_.get_gameObject().SetActive(true);
		Vector3 offset = new Vector3(0f, 0f, 0f);
		Transform portalNode = mapRoot_.FindPortalNode(from_.mapId, to_.mapId);
		if (null == portalNode)
		{
			ExploreMapLocation loc3 = mapRoot_.FindLocation(from_.mapId);
			ExploreMapLocation loc2 = mapRoot_.FindLocation(to_.mapId);
			if (null != loc3 && null != loc2)
			{
				Vector3 pos2 = (loc3.get_transform().get_localPosition() + loc2.get_transform().get_localPosition()) * 0.5f;
				pos2 -= loc3.get_transform().get_localPosition();
				offset = pos2;
				portalNode = loc3.get_transform();
			}
		}
		if (null == portalNode)
		{
			yield return (object)new WaitForSeconds(0.1f);
			DispatchEvent("EXIT", null);
		}
		else
		{
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
					MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Play(ENEMY_TYPE.NONE, MonoBehaviourSingleton<PartyManager>.I.partyData.quest.explore.isRare, false);
					MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.FadeOut(3f, 0.3f, new Action((object)/*Error near IL_0403: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				}
				this.StartCoroutine(DoExitEncounterBossEvent());
			}
			else
			{
				DispatchEvent("EXIT", null);
			}
		}
	}

	private IEnumerator DoExitEncounterBossEvent()
	{
		yield return (object)new WaitForSeconds(3f);
		DispatchEvent("EXIT", null);
	}
}
