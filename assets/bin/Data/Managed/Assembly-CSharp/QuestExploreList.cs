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
		StartCoroutine("DoInitialize");
	}

	private IEnumerator DoInitialize()
	{
		bool is_recv_delivery = false;
		MonoBehaviourSingleton<QuestManager>.I.SendGetExploreList(delegate
		{
			is_recv_delivery = true;
		});
		while (!is_recv_delivery)
		{
			yield return null;
		}
		List<Network.EventData> list = new List<Network.EventData>(MonoBehaviourSingleton<QuestManager>.I.eventList);
		eventList = new List<Network.EventData>();
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].eventType == 4)
			{
				eventList.Add(list[i]);
			}
		}
		for (int j = 0; j < list.Count; j++)
		{
			if (list[j].eventType == 12)
			{
				eventList.Add(list[j]);
			}
		}
		RemoveEndedEvents();
		LoadingQueue loadingQueue = new LoadingQueue(this);
		bannerTable = new Dictionary<int, LoadObject>(eventList.Count);
		for (int k = 0; k < eventList.Count; k++)
		{
			Network.EventData eventData = eventList[k];
			if (!bannerTable.ContainsKey(eventData.bannerId))
			{
				string eventBanner = ResourceName.GetEventBanner(eventData.bannerId);
				LoadObject value = loadingQueue.Load(isEventAsset: true, RESOURCE_CATEGORY.EVENT_ICON, eventBanner);
				bannerTable.Add(eventData.bannerId, value);
			}
		}
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
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

	protected void UpdateEventList()
	{
		RemoveEndedEvents();
		if (eventList == null || eventList.Count == 0)
		{
			SetActive(UI.STR_EVENT_NON_LIST, is_visible: true);
			return;
		}
		SetActive(UI.STR_EVENT_NON_LIST, is_visible: false);
		SetDynamicList(UI.GRD_EVENT_QUEST, "QuestEventListSelectItem", eventList.Count, reset: false, null, null, delegate(int i, Transform t, bool is_recycle)
		{
			Network.EventData eventData = eventList[i];
			Texture2D texture2D = null;
			if (bannerTable.TryGetValue(eventData.bannerId, out LoadObject value))
			{
				texture2D = (value.loadedObject as Texture2D);
				if (texture2D != null)
				{
					Transform t2 = FindCtrl(t, UI.TEX_EVENT_BANNER);
					SetActive(t2, is_visible: true);
					SetTexture(t2, texture2D);
					SetActive(t, UI.LBL_NO_BANNER, is_visible: false);
				}
				else
				{
					SetActive(t, UI.TEX_EVENT_BANNER, is_visible: false);
					SetActive(t, UI.LBL_NO_BANNER, is_visible: true);
					string name = eventData.name;
					SetLabelText(t, UI.LBL_NO_BANNER, name);
				}
			}
			if (!string.IsNullOrEmpty(eventData.endDate.date))
			{
				Transform t3 = FindCtrl(t, UI.LBL_LEFT);
				SetActive(t3, is_visible: true);
				SetLabelText(t3, StringTable.Get(STRING_CATEGORY.TIME, 4u));
				t3 = FindCtrl(t, UI.LBL_LEFT_TIME);
				SetActive(t3, is_visible: true);
				SetLabelText(t3, UIUtility.TimeFormatWithUnit(eventData.GetRest()));
			}
			else
			{
				SetActive(t, UI.LBL_LEFT, is_visible: false);
				SetActive(t, UI.LBL_LEFT_TIME, is_visible: false);
			}
			SetEvent(t, "SELECT_EXPLORE", eventData);
			Version nativeVersionFromName = NetworkNative.getNativeVersionFromName();
			bool flag = eventData.IsPlayableWith(nativeVersionFromName);
			bool flag2 = IsClearedEvent(eventData) && flag;
			bool is_visible = !flag2 && !eventData.readPrologueStory;
			SetActive(t, UI.SPR_NEW, is_visible);
			SetActive(t, UI.SPR_CLEARED, flag2);
			SetBadge(FindCtrl(t, UI.TEX_EVENT_BANNER), MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableEventDeliveryNum(eventData.eventId), SpriteAlignment.TopLeft, 16, -3);
		});
	}

	private bool IsClearedEvent(Network.EventData eventData)
	{
		return MonoBehaviourSingleton<DeliveryManager>.I.IsAllClearedEvent(eventData.eventId);
	}

	private void Update()
	{
		nextUpdate -= Time.deltaTime;
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

	private void OnQuery_SELECT_EXPLORE()
	{
		Network.EventData ev = GameSection.GetEventData() as Network.EventData;
		if (ev == null)
		{
			return;
		}
		if (ev.HasEndDate() && ev.GetRest() < 0)
		{
			GameSection.ChangeEvent("SELECT_ENDED");
			return;
		}
		Version nativeVersionFromName = NetworkNative.getNativeVersionFromName();
		if (!ev.IsPlayableWith(nativeVersionFromName))
		{
			string event_data = string.Format(base.sectionData.GetText("REQUIRE_HIGHER_VERSION"), ev.minVersion);
			GameSection.ChangeEvent("SELECT_VERSION", event_data);
			return;
		}
		if (!ev.readPrologueStory)
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<QuestManager>.I.SendQuestReadEventStory(ev.eventId, delegate(bool success, Error error)
			{
				if (success)
				{
					if (ev.prologueStoryId > 0 && base.sectionData.GetEventData("STORY") != null)
					{
						string goingHomeEvent = GameSection.GetGoingHomeEvent();
						EventData[] array = new EventData[3]
						{
							new EventData(goingHomeEvent, null),
							new EventData("EXPLORE", null),
							new EventData("SELECT_EXPLORE", ev.eventId)
						};
						GameSection.ChangeStayEvent("STORY", new object[4]
						{
							ev.prologueStoryId,
							"",
							"",
							array
						});
					}
					ev.readPrologueStory = true;
				}
				if (ev.eventType == 12)
				{
					GameSection.ChangeStayEvent("SELECT_RUSH", ev);
				}
				GameSection.ResumeEvent(is_resume: true);
			});
		}
		if (ev.eventType == 12)
		{
			GameSection.ChangeEvent("SELECT_RUSH", ev);
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
