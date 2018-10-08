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
				if ((UnityEngine.Object)null == loadedExploreMap.loadedObject)
				{
					base.Initialize();
				}
				else
				{
					if (loadedEncounterBossCutIn != null)
					{
						fieldQuestWarningRoot = ResourceUtility.Realizes(loadedEncounterBossCutIn.loadedObject, -1).gameObject;
						UIPanel panel2 = fieldQuestWarningRoot.GetComponentInChildren<UIPanel>();
						if ((UnityEngine.Object)panel2 != (UnityEngine.Object)null)
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
					map.gameObject.SetActive(true);
					mapRoot_ = map_.GetComponent<ExploreMapRoot>();
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
					from_ = mapRoot_.FindLocation((int)portalData_.srcMapID);
					to_ = mapRoot_.FindLocation((int)portalData_.dstMapID);
					if ((UnityEngine.Object)null == (UnityEngine.Object)to_)
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
							if ((UnityEngine.Object)null != (UnityEngine.Object)marker)
							{
								marker.SetIndex(j);
							}
							marker.gameObject.SetActive(false);
						}
						selfMarker_ = playerMarkers_[0];
						Transform bg = mapFrame_.FindChild("BG");
						bg.gameObject.SetActive(true);
						bgEventListener = UIEventListener.Get(bg.gameObject);
						mapFrame_.FindChild("TaptoSkip").gameObject.SetActive(!toBoss_);
						mapFrame_.FindChild("CaptionRoot/Close").gameObject.SetActive(false);
						mapRoot_.SetMarkers(playerMarkers_, false);
						ExploreStatus.TraceInfo[] traceHistory = MonoBehaviourSingleton<QuestManager>.I.GetBossTraceHistory();
						if (traceHistory != null && traceHistory.Length > 0)
						{
							Transform lastFootprint = ResourceUtility.Realizes(loadedFootprint.loadedObject, map_, -1);
							ExploreStatus.TraceInfo lastInfo = traceHistory[traceHistory.Length - 1];
							Vector3 lastPos = mapRoot_.GetPositionOnMap(lastInfo.mapId);
							lastFootprint.localPosition = new Vector3(lastPos.x + MarkerOffsetX, lastPos.y + MarkerOffsetY, lastPos.z);
							lastFootprint.gameObject.SetActive(true);
							if (traceHistory.Length > 1)
							{
								Transform lastSecondFootprint = ResourceUtility.Realizes(loadedFootprint.loadedObject, map_, -1);
								ExploreStatus.TraceInfo lastSecondInfo = traceHistory[traceHistory.Length - 2];
								Vector3 lastSecondPos = mapRoot_.GetPositionOnMap(lastSecondInfo.mapId);
								lastSecondFootprint.localPosition = new Vector3(lastSecondPos.x + MarkerOffsetX, lastSecondPos.y + MarkerOffsetY, lastSecondPos.z);
								lastSecondFootprint.gameObject.SetActive(true);
							}
						}
						redCircle = ResourceUtility.Realizes(loadedCircle.loadedObject, map_, -1);
						redCircle.localScale = new Vector3(0.6f, 0.6f, 0.6f);
						battleIcon = ResourceUtility.Realizes(loadedBattleIcon.loadedObject, map_, -1);
						if (mapRoot_.showBattleMarker)
						{
							int bossMapId = MonoBehaviourSingleton<QuestManager>.I.GetExploreBossAppearMapId();
							Vector3 pos = mapRoot_.GetPositionOnMap(bossMapId);
							redCircle.localPosition = pos;
							battleIcon.localPosition = new Vector3(pos.x + MarkerOffsetX, pos.y + MarkerOffsetY, pos.z);
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
						base.Initialize();
					}
				}
			}
		}
	}

	private void OnQuery_EXIT()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.IsEventExecutionPossible() && !calledExit)
		{
			StopAllCoroutines();
			if ((UnityEngine.Object)null != (UnityEngine.Object)bgEventListener)
			{
				UIEventListener uIEventListener = bgEventListener;
				uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Remove(uIEventListener.onClick, new UIEventListener.VoidDelegate(onClick));
			}
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("ExploreMapOpenNewField", base.gameObject, "INGAME_MAIN", null, null, true);
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
		if ((UnityEngine.Object)null == (UnityEngine.Object)bgEventListener)
		{
			DispatchEvent("EXIT", null);
		}
		else
		{
			UIEventListener uIEventListener = bgEventListener;
			uIEventListener.onClick = (UIEventListener.VoidDelegate)Delegate.Combine(uIEventListener.onClick, new UIEventListener.VoidDelegate(onClick));
			StartCoroutine(DoExitEvent());
		}
	}

	private IEnumerator DoExitEvent()
	{
		selfMarker_.gameObject.SetActive(true);
		Vector3 offset = new Vector3(0f, 0f, 0f);
		Transform portalNode = mapRoot_.FindPortalNode(from_.mapId, to_.mapId);
		if ((UnityEngine.Object)null == (UnityEngine.Object)portalNode)
		{
			ExploreMapLocation loc3 = mapRoot_.FindLocation(from_.mapId);
			ExploreMapLocation loc2 = mapRoot_.FindLocation(to_.mapId);
			if ((UnityEngine.Object)null != (UnityEngine.Object)loc3 && (UnityEngine.Object)null != (UnityEngine.Object)loc2)
			{
				Vector3 pos2 = (loc3.transform.localPosition + loc2.transform.localPosition) * 0.5f;
				pos2 -= loc3.transform.localPosition;
				offset = pos2;
				portalNode = loc3.transform;
			}
		}
		if ((UnityEngine.Object)null == (UnityEngine.Object)portalNode)
		{
			yield return (object)new WaitForSeconds(0.1f);
			DispatchEvent("EXIT", null);
		}
		else
		{
			if ((UnityEngine.Object)null != (UnityEngine.Object)from_)
			{
				if (fromBoss_ && (UnityEngine.Object)null != (UnityEngine.Object)portalNode)
				{
					Utility.Attach(portalNode, selfMarker_);
					selfMarker_.GetComponent<ExplorePlayerMarker>().SetIndex(0);
					selfMarker_.localPosition += offset;
				}
				else
				{
					Utility.Attach(from_.transform, selfMarker_);
					selfMarker_.GetComponent<ExplorePlayerMarker>().SetIndex(0);
				}
				yield return (object)new WaitForSeconds(0.3f);
				TweenScale.Begin(selfMarker_.gameObject, 0.3f, Vector3.zero);
				yield return (object)new WaitForSeconds(0.3f);
			}
			else
			{
				selfMarker_.localScale = Vector3.zero;
				yield return (object)new WaitForSeconds(0.3f);
			}
			yield return (object)new WaitForSeconds(0.3f);
			Utility.Attach(to_.transform, selfMarker_);
			selfMarker_.GetComponent<ExplorePlayerMarker>().SetIndex(0);
			TweenScale.Begin(selfMarker_.gameObject, 0.3f, Vector3.one);
			yield return (object)new WaitForSeconds(0.5f);
			if (toBoss_)
			{
				if (MonoBehaviourSingleton<UIInGameFieldQuestWarning>.IsValid())
				{
					MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.Play(ENEMY_TYPE.NONE, MonoBehaviourSingleton<PartyManager>.I.partyData.quest.explore.isRare, false);
					MonoBehaviourSingleton<UIInGameFieldQuestWarning>.I.FadeOut(3f, 0.3f, delegate
					{
						if ((UnityEngine.Object)((_003CDoExitEvent_003Ec__Iterator17B)/*Error near IL_0403: stateMachine*/)._003C_003Ef__this.fieldQuestWarningRoot != (UnityEngine.Object)null)
						{
							UnityEngine.Object.Destroy(((_003CDoExitEvent_003Ec__Iterator17B)/*Error near IL_0403: stateMachine*/)._003C_003Ef__this.fieldQuestWarningRoot);
						}
					});
				}
				StartCoroutine(DoExitEncounterBossEvent());
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
