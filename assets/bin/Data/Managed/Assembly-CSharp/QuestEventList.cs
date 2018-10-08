using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestEventList : GameSection
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
		LBL_SUB_TITLE,
		BTN_EVENT,
		OBJ_FRAME,
		SPR_BG_FRAME,
		SPR_CLEARED,
		SPR_NEW
	}

	private List<Network.EventData> eventList;

	private Dictionary<int, LoadObject> bannerTable;

	private Dictionary<int, bool> exploreClearedMap;

	protected bool isInGame;

	private Coroutine checkUpdate;

	private static readonly float UPDATE_INTERVAL_SEC = 30f;

	public override void Initialize()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine("DoInitialize");
	}

	private IEnumerator DoInitialize()
	{
		bool is_recv_delivery = false;
		MonoBehaviourSingleton<QuestManager>.I.SendGetEventList(delegate
		{
			((_003CDoInitialize_003Ec__IteratorBB)/*Error near IL_0035: stateMachine*/)._003Cis_recv_delivery_003E__0 = true;
		});
		while (!is_recv_delivery)
		{
			yield return (object)null;
		}
		if (isInGame)
		{
			eventList = new List<Network.EventData>(MonoBehaviourSingleton<QuestManager>.I.eventList.Count);
			List<Network.EventData> allEventList = new List<Network.EventData>(MonoBehaviourSingleton<QuestManager>.I.eventList);
			for (int j = 0; j < allEventList.Count; j++)
			{
				if (allEventList[j].eventType != 4 && allEventList[j].eventType != 12 && allEventList[j].eventType != 15)
				{
					eventList.Add(allEventList[j]);
				}
			}
		}
		else
		{
			eventList = new List<Network.EventData>(MonoBehaviourSingleton<QuestManager>.I.eventList);
		}
		RemoveEndedEvents();
		LoadingQueue loadingQueue = new LoadingQueue(this);
		bannerTable = new Dictionary<int, LoadObject>(eventList.Count);
		exploreClearedMap = new Dictionary<int, bool>();
		for (int i = 0; i < eventList.Count; i++)
		{
			Network.EventData e = eventList[i];
			if (!bannerTable.ContainsKey(e.bannerId))
			{
				string bannerImg = ResourceName.GetEventBanner(e.bannerId);
				LoadObject obj = loadingQueue.Load(RESOURCE_CATEGORY.EVENT_ICON, bannerImg, false);
				bannerTable.Add(e.bannerId, obj);
				if (e.eventType == 15 && (int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < 50)
				{
					LoadDisableArenaBanner(loadingQueue);
				}
				if (e.eventType == 4)
				{
					yield return (object)this.StartCoroutine(GetExplorePoint(e.eventId));
				}
			}
		}
		if (loadingQueue.IsLoading())
		{
			yield return (object)loadingQueue.Wait();
		}
		base.Initialize();
	}

	private void LoadDisableArenaBanner(LoadingQueue loadingQueue)
	{
		string eventBanner = ResourceName.GetEventBanner(10012201);
		LoadObject value = loadingQueue.Load(RESOURCE_CATEGORY.EVENT_ICON, eventBanner, false);
		bannerTable.Add(10012201, value);
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
		UpdateNPC();
		UpdateEventList();
		UpdateCheck();
	}

	protected void UpdateCheck()
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Expected O, but got Unknown
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		if (checkUpdate != null)
		{
			this.StopCoroutine(checkUpdate);
		}
		List<Network.EventData> list = eventList.FindAll((Network.EventData x) => x.HasEndDate());
		if (list == null || list.Count == 0)
		{
			float updateTime = 86400f;
			checkUpdate = this.StartCoroutine(DoCheck(updateTime));
		}
		else
		{
			int num = list.Min((Network.EventData data) => data.GetRest());
			float updateTime = (!((float)num < UPDATE_INTERVAL_SEC)) ? ((float)num) : UPDATE_INTERVAL_SEC;
			checkUpdate = this.StartCoroutine(DoCheck(updateTime));
		}
	}

	private IEnumerator DoCheck(float updateTime)
	{
		yield return (object)new WaitForSeconds(updateTime);
		RefreshUI();
	}

	protected void UpdateNPC()
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		string empty = string.Empty;
		NPCMessageTable.Section section = Singleton<NPCMessageTable>.I.GetSection(base.sectionData.sectionName + "_TEXT");
		if (section != null)
		{
			NPCMessageTable.Message message = section.GetNPCMessage();
			if (message != null)
			{
				empty = message.message;
				SetRenderNPCModel((Enum)UI.TEX_NPCMODEL, message.npc, message.pos, message.rot, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.questCenterNPCFOV, (Action<NPCLoader>)delegate(NPCLoader loader)
				{
					loader.GetAnimator().Play(message.animationStateName);
				});
				SetLabelText((Enum)UI.LBL_NPC_MESSAGE, empty);
			}
		}
	}

	protected void UpdateEventList()
	{
		RemoveEndedEvents();
		if (eventList == null || eventList.Count == 0)
		{
			SetActive((Enum)UI.STR_EVENT_NON_LIST, true);
		}
		else
		{
			SetActive((Enum)UI.STR_EVENT_NON_LIST, false);
			SetDynamicList((Enum)UI.GRD_EVENT_QUEST, "QuestEventListSelectItem", eventList.Count, false, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycle)
			{
				Network.EventData eventData = eventList[i];
				Texture2D val = null;
				if (bannerTable.TryGetValue(eventData.bannerId, out LoadObject value))
				{
					val = (value.loadedObject as Texture2D);
					if (val != null)
					{
						Transform t2 = FindCtrl(t, UI.TEX_EVENT_BANNER);
						SetActive(t2, true);
						SetTexture(t2, val);
						SetActive(t, UI.LBL_NO_BANNER, false);
					}
					else
					{
						SetActive(t, UI.TEX_EVENT_BANNER, false);
						SetActive(t, UI.LBL_NO_BANNER, true);
						string name = eventData.name;
						SetLabelText(t, UI.LBL_NO_BANNER, name);
					}
				}
				if (!string.IsNullOrEmpty(eventData.endDate.date))
				{
					Transform t3 = FindCtrl(t, UI.LBL_LEFT);
					SetActive(t3, true);
					SetLabelText(t3, StringTable.Get(STRING_CATEGORY.TIME, 4u));
					t3 = FindCtrl(t, UI.LBL_LEFT_TIME);
					SetActive(t3, true);
					SetLabelText(t3, UIUtility.TimeFormatWithUnit(eventData.GetRest()));
				}
				else
				{
					SetActive(t, UI.LBL_LEFT, false);
					SetActive(t, UI.LBL_LEFT_TIME, false);
				}
				SetActive(t, UI.LBL_SUB_TITLE, false);
				if (eventData.eventType == 4)
				{
					SetEvent(t, "SELECT_EXPLORE", eventData);
				}
				else if (eventData.eventType == 12)
				{
					SetEvent(t, "SELECT_RUSH", eventData);
				}
				else if (eventData.eventType == 15)
				{
					SetArenaItem(t, eventData);
				}
				else
				{
					SetEvent(t, "SELECT", eventData);
				}
				Version nativeVersionFromName = NetworkNative.getNativeVersionFromName();
				bool flag = eventData.IsPlayableWith(nativeVersionFromName);
				bool flag2 = IsClearedEvent(eventData) && flag;
				bool is_visible = !flag2 && !eventData.readPrologueStory;
				SetActive(t, UI.SPR_NEW, is_visible);
				SetActive(t, UI.SPR_CLEARED, flag2);
				SetBadge(FindCtrl(t, UI.TEX_EVENT_BANNER), MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableEventDeliveryNum(eventData.eventId), 1, 16, -3, false);
			});
		}
	}

	private void SetArenaItem(Transform t, Network.EventData ev)
	{
		if ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level < 50)
		{
			SetEvent(t, "SELECT_DISABLE_ARENA", ev);
			if (bannerTable.TryGetValue(10012201, out LoadObject value))
			{
				Texture2D val = value.loadedObject as Texture2D;
				if (val != null)
				{
					Transform t2 = FindCtrl(t, UI.TEX_EVENT_BANNER);
					SetActive(t2, true);
					SetTexture(t2, val);
					SetActive(t, UI.LBL_NO_BANNER, false);
				}
				else
				{
					SetActive(t, UI.TEX_EVENT_BANNER, false);
					SetActive(t, UI.LBL_NO_BANNER, true);
					string name = ev.name;
					SetLabelText(t, UI.LBL_NO_BANNER, name);
				}
			}
		}
		else
		{
			SetEvent(t, "SELECT_ARENA", ev);
			SetLabelText(t, UI.LBL_SUB_TITLE, ev.name);
			SetActive(t, UI.LBL_SUB_TITLE, true);
		}
	}

	private IEnumerator GetExplorePoint(int eventId)
	{
		bool isRequest = true;
		Protocol.Send<QuestExplorePointModel.RequestSendForm, QuestExplorePointModel>(post_data: new QuestExplorePointModel.RequestSendForm
		{
			eid = eventId
		}, url: QuestExplorePointModel.URL, call_back: (Action<QuestExplorePointModel>)delegate(QuestExplorePointModel result)
		{
			((_003CGetExplorePoint_003Ec__IteratorBD)/*Error near IL_004f: stateMachine*/)._003CisRequest_003E__0 = false;
			bool value = false;
			if (result.result.reward == null || result.result.reward.point <= 0)
			{
				value = true;
			}
			((_003CGetExplorePoint_003Ec__IteratorBD)/*Error near IL_004f: stateMachine*/)._003C_003Ef__this.exploreClearedMap.Add(((_003CGetExplorePoint_003Ec__IteratorBD)/*Error near IL_004f: stateMachine*/).eventId, value);
		}, get_param: string.Empty);
		while (isRequest)
		{
			yield return (object)null;
		}
	}

	private bool IsClearedEvent(Network.EventData eventData)
	{
		if (eventData.eventType == 4)
		{
			bool value = false;
			exploreClearedMap.TryGetValue(eventData.eventId, out value);
			if (value)
			{
				return MonoBehaviourSingleton<DeliveryManager>.I.IsAllClearedEvent(eventData.eventId);
			}
			return false;
		}
		return MonoBehaviourSingleton<DeliveryManager>.I.IsAllClearedEvent(eventData.eventId);
	}

	private void OnApplicationPause(bool pause)
	{
		if (!pause)
		{
			RefreshUI();
		}
	}

	private void OnQuery_SELECT()
	{
		Network.EventData ev = GameSection.GetEventData() as Network.EventData;
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
			else if (!ev.readPrologueStory)
			{
				GameSection.StayEvent();
				MonoBehaviourSingleton<QuestManager>.I.SendQuestReadEventStory(ev.eventId, delegate(bool success, Error error)
				{
					if (success)
					{
						if (ev.prologueStoryId > 0)
						{
							GameSceneTables.EventData eventData = base.sectionData.GetEventData("STORY");
							if (eventData != null)
							{
								string name = (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge()) ? "MAIN_MENU_HOME" : "MAIN_MENU_LOUNGE";
								EventData[] array = new EventData[3]
								{
									new EventData(name, null),
									new EventData("TO_EVENT", null),
									new EventData("SELECT", ev.eventId)
								};
								GameSection.ChangeStayEvent("STORY", new object[4]
								{
									ev.prologueStoryId,
									string.Empty,
									string.Empty,
									array
								});
							}
						}
						ev.readPrologueStory = true;
					}
					GameSection.ResumeEvent(true, null);
				});
			}
		}
	}

	private void OnQuery_SELECT_EXPLORE()
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
				else if (!ev.readPrologueStory)
				{
					GameSection.StayEvent();
					MonoBehaviourSingleton<QuestManager>.I.SendQuestReadEventStory(ev.eventId, delegate(bool success, Error error)
					{
						if (success)
						{
							if (ev.prologueStoryId > 0)
							{
								GameSceneTables.EventData eventData = base.sectionData.GetEventData("STORY");
								if (eventData != null)
								{
									string name = (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge()) ? "MAIN_MENU_HOME" : "MAIN_MENU_LOUNGE";
									EventData[] array = new EventData[3]
									{
										new EventData(name, null),
										new EventData("TO_EVENT", null),
										new EventData("SELECT_EXPLORE", ev.eventId)
									};
									GameSection.ChangeStayEvent("STORY", new object[4]
									{
										ev.prologueStoryId,
										string.Empty,
										string.Empty,
										array
									});
								}
							}
							ev.readPrologueStory = true;
						}
						GameSection.ResumeEvent(true, null);
					});
				}
			}
		}
	}

	private void OnQuery_SELECT_RUSH()
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
				else if (!ev.readPrologueStory)
				{
					GameSection.StayEvent();
					MonoBehaviourSingleton<QuestManager>.I.SendQuestReadEventStory(ev.eventId, delegate(bool success, Error error)
					{
						if (success)
						{
							if (ev.prologueStoryId > 0)
							{
								GameSceneTables.EventData eventData = base.sectionData.GetEventData("STORY");
								if (eventData != null)
								{
									string name = (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge()) ? "MAIN_MENU_HOME" : "MAIN_MENU_LOUNGE";
									EventData[] array = new EventData[3]
									{
										new EventData(name, null),
										new EventData("TO_EVENT", null),
										new EventData("SELECT_RUSH", ev.eventId)
									};
									GameSection.ChangeStayEvent("STORY", new object[4]
									{
										ev.prologueStoryId,
										string.Empty,
										string.Empty,
										array
									});
								}
							}
							ev.readPrologueStory = true;
						}
						GameSection.ResumeEvent(true, null);
					});
				}
			}
		}
	}

	private void OnQuery_SELECT_ARENA()
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
				else if (!ev.readPrologueStory)
				{
					GameSection.StayEvent();
					MonoBehaviourSingleton<QuestManager>.I.SendQuestReadEventStory(ev.eventId, delegate(bool success, Error error)
					{
						if (success)
						{
							if (ev.prologueStoryId > 0)
							{
								GameSceneTables.EventData eventData = base.sectionData.GetEventData("STORY");
								if (eventData != null)
								{
									string name = (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge()) ? "MAIN_MENU_HOME" : "MAIN_MENU_LOUNGE";
									EventData[] array = new EventData[3]
									{
										new EventData(name, null),
										new EventData("TO_EVENT", null),
										new EventData("SELECT_ARENA", ev)
									};
									GameSection.ChangeStayEvent("STORY", new object[4]
									{
										ev.prologueStoryId,
										string.Empty,
										string.Empty,
										array
									});
								}
							}
							ev.readPrologueStory = true;
						}
						GameSection.ResumeEvent(true, null);
					});
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
		if ((event_name == "SELECT" || event_name == "SELECT_EXPLORE" || event_name == "SELECT_RUSH") && event_data is int)
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
