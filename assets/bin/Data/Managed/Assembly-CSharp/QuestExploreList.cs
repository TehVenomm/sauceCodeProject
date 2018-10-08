using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestExploreList : GameSection
{
	protected enum UI
	{
		SCR_EVENT_QUEST,
		GRD_EVENT_QUEST,
		TEX_NPCMODEL,
		LBL_NPC_MESSAGE,
		OBJ_NPC,
		TEX_EVENT_BANNER,
		LBL_NO_BANNER,
		LBL_LEFT,
		LBL_LEFT_TIME,
		STR_EVENT_NON_LIST,
		BTN_EVENT,
		OBJ_FRAME,
		SPR_BG_FRAME,
		SPR_CLEARED,
		SPR_NEW
	}

	private List<Network.EventData> eventList;

	private Dictionary<int, LoadObject> bannerTable;

	private static readonly float UPDATE_INTERVAL_SEC = 30f;

	private float nextUpdate = UPDATE_INTERVAL_SEC;

	public override string overrideBackKeyEvent => "CLOSE";

	public override void Initialize()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine("DoInitialize");
	}

	private IEnumerator DoInitialize()
	{
		bool is_recv_delivery = false;
		MonoBehaviourSingleton<QuestManager>.I.SendGetExploreList(delegate
		{
			((_003CDoInitialize_003Ec__Iterator11F)/*Error near IL_0031: stateMachine*/)._003Cis_recv_delivery_003E__0 = true;
		});
		while (!is_recv_delivery)
		{
			yield return (object)null;
		}
		List<Network.EventData> allEventList = new List<Network.EventData>(MonoBehaviourSingleton<QuestManager>.I.eventList);
		eventList = new List<Network.EventData>();
		for (int k = 0; k < allEventList.Count; k++)
		{
			if (allEventList[k].eventType == 4)
			{
				eventList.Add(allEventList[k]);
			}
		}
		for (int j = 0; j < allEventList.Count; j++)
		{
			if (allEventList[j].eventType == 12)
			{
				eventList.Add(allEventList[j]);
			}
		}
		RemoveEndedEvents();
		LoadingQueue loadingQueue = new LoadingQueue(this);
		bannerTable = new Dictionary<int, LoadObject>(eventList.Count);
		for (int i = 0; i < eventList.Count; i++)
		{
			Network.EventData e = eventList[i];
			if (!bannerTable.ContainsKey(e.bannerId))
			{
				string bannerImg = ResourceName.GetEventBanner(e.bannerId);
				LoadObject obj = loadingQueue.Load(true, RESOURCE_CATEGORY.EVENT_ICON, bannerImg, false);
				bannerTable.Add(e.bannerId, obj);
			}
		}
		if (loadingQueue.IsLoading())
		{
			yield return (object)loadingQueue.Wait();
		}
		base.Initialize();
	}

	private void RemoveEndedEvents()
	{
		if (eventList != null)
		{
			eventList.RemoveAll((Network.EventData e) => e.HasEndDate() && e.GetRest() < 0);
		}
	}

	public override void UpdateUI()
	{
		UpdateEventList();
	}

	protected unsafe void UpdateEventList()
	{
		RemoveEndedEvents();
		if (eventList == null || eventList.Count == 0)
		{
			SetActive((Enum)UI.STR_EVENT_NON_LIST, true);
		}
		else
		{
			SetActive((Enum)UI.STR_EVENT_NON_LIST, false);
			SetDynamicList((Enum)UI.GRD_EVENT_QUEST, "QuestEventListSelectItem", eventList.Count, false, null, null, new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	private bool IsClearedEvent(Network.EventData eventData)
	{
		return MonoBehaviourSingleton<DeliveryManager>.I.IsAllClearedEvent(eventData.eventId);
	}

	private void Update()
	{
		nextUpdate -= Time.get_deltaTime();
		if (nextUpdate < 0f)
		{
			RefreshUI();
			nextUpdate = UPDATE_INTERVAL_SEC;
		}
	}

	private void OnApplicationPause(bool pause)
	{
		if (!pause)
		{
			RefreshUI();
			nextUpdate = UPDATE_INTERVAL_SEC;
		}
	}

	private unsafe void OnQuery_SELECT_EXPLORE()
	{
		Network.EventData ev = GameSection.GetEventData() as Network.EventData;
		if (ev != null)
		{
			if (ev.HasEndDate() && ev.GetRest() < 0)
			{
				GameSection.ChangeEvent("SELECT_ENDED", null);
			}
			else
			{
				Version nativeVersionFromName = NetworkNative.getNativeVersionFromName();
				if (!ev.IsPlayableWith(nativeVersionFromName))
				{
					string event_data = string.Format(base.sectionData.GetText("REQUIRE_HIGHER_VERSION"), ev.minVersion);
					GameSection.ChangeEvent("SELECT_VERSION", event_data);
				}
				else
				{
					if (!ev.readPrologueStory)
					{
						GameSection.StayEvent();
						_003COnQuery_SELECT_EXPLORE_003Ec__AnonStorey419 _003COnQuery_SELECT_EXPLORE_003Ec__AnonStorey;
						MonoBehaviourSingleton<QuestManager>.I.SendQuestReadEventStory(ev.eventId, new Action<bool, Error>((object)_003COnQuery_SELECT_EXPLORE_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
					}
					if (ev.eventType == 12)
					{
						GameSection.ChangeEvent("SELECT_RUSH", ev);
					}
				}
			}
		}
	}

	private void OnCloseDialog_QuestEventEndedDialog()
	{
		RefreshUI();
	}

	public override EventData CheckAutoEvent(string event_name, object event_data)
	{
		if (event_name == "SELECT_EXPLORE" && event_data is int)
		{
			if (eventList != null)
			{
				int event_id = (int)event_data;
				Network.EventData eventData = eventList.Find((Network.EventData e) => e.eventId == event_id);
				if (eventData != null)
				{
					return new EventData(event_name, eventData);
				}
			}
			return new EventData("NONE", null);
		}
		return base.CheckAutoEvent(event_name, event_data);
	}
}
