using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestArenaSelectList : QuestEventSelectList
{
	protected new enum UI
	{
		TEX_EVENT_BG,
		BTN_INFO,
		TGL_BUTTON_ROOT,
		SPR_DELIVERY_BTN_SELECTED,
		OBJ_DELIVERY_ROOT,
		TEX_NPCMODEL,
		LBL_NPC_MESSAGE,
		GRD_DELIVERY_QUEST,
		TBL_DELIVERY_QUEST,
		STR_DELIVERY_NON_LIST,
		OBJ_REQUEST_COMPLETED,
		LBL_LOCATION_NAME,
		LBL_LOCATION_NAME_EFFECT,
		WGT_LOCATION_NAME_LIMIT,
		SCR_DELIVERY_QUEST,
		OBJ_IMAGE,
		BTN_EVENT,
		OBJ_FRAME,
		SPR_BG_FRAME,
		LBL_STORY_TITLE,
		SPR_FRAME,
		LBL_SUB_TITLE
	}

	public class ArenaSort : IComparer<DeliveryTable.DeliveryData>
	{
		public int Compare(DeliveryTable.DeliveryData x, DeliveryTable.DeliveryData y)
		{
			bool flag = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int)x.id);
			bool flag2 = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int)y.id);
			if (flag != flag2)
			{
				if (flag)
				{
					return -1;
				}
				return 1;
			}
			return y.displayOrder - x.displayOrder;
		}
	}

	private const string ARENA_FRAME_SPRITE = "RequestPlate_Arena";

	private int m_lastBGMId;

	private List<Delivery> visibleDeliveryList = new List<Delivery>();

	private List<DeliveryTable.DeliveryData> notClearDevliveries = new List<DeliveryTable.DeliveryData>();

	private List<uint> timeAttackDeliveryIds = new List<uint>();

	private List<ArenaTable.ArenaData> arenaDataList = new List<ArenaTable.ArenaData>();

	private ArenaUserRecordModel.Param record;

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "DeliveryRewardTable";
			yield return "FieldMapTable";
			yield return "ArenaTable";
		}
	}

	protected override bool showMap => false;

	public override void Initialize()
	{
		base.Initialize();
	}

	protected override IEnumerator DoInitialize()
	{
		if (GameSection.GetEventData() == null)
		{
			bool is_recv_delivery = false;
			MonoBehaviourSingleton<QuestManager>.I.SendGetEventList(delegate
			{
				((_003CDoInitialize_003Ec__Iterator115)/*Error near IL_003b: stateMachine*/)._003Cis_recv_delivery_003E__0 = true;
			});
			while (!is_recv_delivery)
			{
				yield return (object)null;
			}
			GameSection.SetEventData(eventData = MonoBehaviourSingleton<QuestManager>.I.FindArenaDataFromList());
			MonoBehaviourSingleton<DeliveryManager>.I.DeleteCleardDeliveryId();
		}
		if (eventData == null)
		{
			this.StartCoroutine(LoadDisableBanner());
		}
		else
		{
			if (MonoBehaviourSingleton<SoundManager>.IsValid())
			{
				m_lastBGMId = MonoBehaviourSingleton<SoundManager>.I.requestBGMID;
				SoundManager.RequestBGM(3, true);
			}
			if (MonoBehaviourSingleton<UserInfoManager>.I.isJoinedArenaRanking)
			{
				yield return (object)this.StartCoroutine(SendGetMyRcord());
			}
			this.StartCoroutine(base.DoInitialize());
		}
	}

	private IEnumerator LoadDisableBanner()
	{
		string resourceName = ResourceName.GetEventBG(10012200);
		Hash128 hash = default(Hash128);
		if (MonoBehaviourSingleton<ResourceManager>.I.event_manifest != null)
		{
			hash = MonoBehaviourSingleton<ResourceManager>.I.event_manifest.GetAssetBundleHash(RESOURCE_CATEGORY.EVENT_BG.ToAssetBundleName(resourceName));
		}
		if (MonoBehaviourSingleton<ResourceManager>.I.event_manifest == null || hash.get_isValid())
		{
			LoadingQueue load_queue = new LoadingQueue(this);
			LoadObject lo_bg = load_queue.Load(true, RESOURCE_CATEGORY.EVENT_BG, resourceName, false);
			if (load_queue.IsLoading())
			{
				yield return (object)load_queue.Wait();
			}
			SetTexture(texture: lo_bg.loadedObject as Texture2D, texture_enum: UI.TEX_EVENT_BG);
		}
		EndInitialize();
	}

	public override void UpdateUI()
	{
		if (eventData == null)
		{
			SetActive((Enum)UI.BTN_INFO, false);
			SetActive((Enum)UI.LBL_SUB_TITLE, false);
			UpdateTitle();
			UpdateNoArenaTable();
		}
		else
		{
			CreateArenaList();
			CreateVisibleDeliveryList();
			base.UpdateUI();
			UpdateSubTitle();
			UpdateTitle();
		}
	}

	public override void StartSection()
	{
		base.StartSection();
		if (eventData != null)
		{
			if (!IsPlayableVersion())
			{
				string event_data = string.Format(base.sectionData.GetText("REQUIRE_HIGHER_VERSION"), eventData.minVersion);
				RequestEvent("SELECT_VERSION", event_data);
			}
			else if (!eventData.readPrologueStory && eventData.prologueStoryId > 0)
			{
				StartAutoPrologue();
			}
		}
	}

	private bool IsPlayableVersion()
	{
		if (eventData == null)
		{
			return true;
		}
		Version nativeVersionFromName = NetworkNative.getNativeVersionFromName();
		return eventData.IsPlayableWith(nativeVersionFromName);
	}

	private void StartAutoPrologue()
	{
		string name = (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge()) ? "MAIN_MENU_HOME" : "MAIN_MENU_LOUNGE";
		EventData[] array = new EventData[2]
		{
			new EventData(name, null),
			new EventData("ARENA_LIST", eventData)
		};
		EventData[] autoEvents = new EventData[1]
		{
			new EventData("AUTO_PROLOGUE", new object[4]
			{
				eventData.prologueStoryId,
				string.Empty,
				string.Empty,
				array
			})
		};
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
	}

	private unsafe void OnQuery_AUTO_PROLOGUE()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<QuestManager>.I.SendQuestReadEventStory(eventData.eventId, new Action<bool, Error>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	private unsafe IEnumerator SendGetMyRcord()
	{
		bool isFinishGetRecord = false;
		MonoBehaviourSingleton<QuestManager>.I.SendGetArenaUserRecord(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id, eventData.eventId, new Action<bool, ArenaUserRecordModel.Param>((object)/*Error near IL_004c: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		while (!isFinishGetRecord)
		{
			yield return (object)null;
		}
	}

	private void UpdateSubTitle()
	{
		SetActive((Enum)UI.LBL_SUB_TITLE, false);
		SetLabelText((Enum)UI.LBL_SUB_TITLE, eventData.name);
	}

	private void UpdateTitle()
	{
		string text = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 27u);
		SetLabelText((Enum)UI.LBL_LOCATION_NAME, text);
		SetLabelText((Enum)UI.LBL_LOCATION_NAME_EFFECT, text);
	}

	protected unsafe override void UpdateTable()
	{
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Expected O, but got Unknown
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		int count = stories.Count;
		if (count > 0)
		{
			num++;
		}
		_SorteliveryList();
		int num2 = notClearDevliveries.Count + clearedDeliveries.Count;
		num2++;
		if (showStory)
		{
			num2 += num + stories.Count;
		}
		if (notClearDevliveries == null || num2 == 0)
		{
			SetActive((Enum)UI.STR_DELIVERY_NON_LIST, true);
			SetActive((Enum)UI.GRD_DELIVERY_QUEST, false);
			SetActive((Enum)UI.TBL_DELIVERY_QUEST, false);
		}
		else
		{
			SetActive((Enum)UI.STR_DELIVERY_NON_LIST, false);
			SetActive((Enum)UI.GRD_DELIVERY_QUEST, false);
			SetActive((Enum)UI.TBL_DELIVERY_QUEST, true);
			int questStartIndex = 0;
			questStartIndex++;
			int completedStartIndex = notClearDevliveries.Count + questStartIndex;
			int borderIndex = completedStartIndex + clearedDeliveries.Count;
			int storyStartIndex = borderIndex;
			if (stories.Count > 0)
			{
				storyStartIndex++;
			}
			Transform ctrl = GetCtrl(UI.TBL_DELIVERY_QUEST);
			if (Object.op_Implicit(ctrl))
			{
				int i = 0;
				for (int childCount = ctrl.get_childCount(); i < childCount; i++)
				{
					Transform val = ctrl.GetChild(0);
					val.set_parent(null);
					Object.Destroy(val.get_gameObject());
				}
			}
			bool isRenewalFlag = MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.isTheaterRenewal;
			_003CUpdateTable_003Ec__AnonStorey3F9 _003CUpdateTable_003Ec__AnonStorey3F;
			SetTable(UI.TBL_DELIVERY_QUEST, string.Empty, num2, false, new Func<int, Transform, Transform>((object)_003CUpdateTable_003Ec__AnonStorey3F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), new Action<int, Transform, bool>((object)_003CUpdateTable_003Ec__AnonStorey3F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			UIScrollView component = base.GetComponent<UIScrollView>((Enum)UI.SCR_DELIVERY_QUEST);
			component.set_enabled(true);
			RepositionTable();
		}
	}

	protected unsafe void UpdateNoArenaTable()
	{
		int item_num = 1;
		SetTable(UI.TBL_DELIVERY_QUEST, string.Empty, item_num, false, new Func<int, Transform, Transform>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		UIScrollView component = base.GetComponent<UIScrollView>((Enum)UI.SCR_DELIVERY_QUEST);
		component.set_enabled(false);
		RepositionTable();
	}

	private void CreateArenaList()
	{
		arenaDataList.Clear();
		for (int i = 0; i < deliveryInfo.Length; i++)
		{
			DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryInfo[i].dId);
			ArenaTable.ArenaData arenaData = deliveryTableData.GetArenaData();
			if (arenaData == null)
			{
				Debug.LogError((object)(this.get_name() + " " + deliveryTableData.name + " : arenaDataが見つかりません"));
			}
			else
			{
				arenaDataList.Add(arenaData);
			}
		}
	}

	private void CreateVisibleDeliveryList()
	{
		visibleDeliveryList.Clear();
		int i = 0;
		for (int num = deliveryInfo.Length; i < num; i++)
		{
			Delivery item = deliveryInfo[i];
			visibleDeliveryList.Add(item);
		}
	}

	protected override List<DeliveryTable.DeliveryData> CreateClearedDliveryList()
	{
		return CreateClearedDliveryList(ARENA_RANK.S);
	}

	private List<DeliveryTable.DeliveryData> CreateClearedDliveryList(ARENA_RANK borderRank)
	{
		List<DeliveryTable.DeliveryData> list = new List<DeliveryTable.DeliveryData>();
		List<ClearStatusDelivery> clearStatusDelivery = MonoBehaviourSingleton<DeliveryManager>.I.clearStatusDelivery;
		int i = 0;
		for (int count = clearStatusDelivery.Count; i < count; i++)
		{
			ClearStatusDelivery d = clearStatusDelivery[i];
			if (d.deliveryStatus == 3)
			{
				DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)d.deliveryId);
				if (deliveryTableData.eventID == eventData.eventId && !Array.Exists(deliveryInfo, (Delivery x) => x.dId == d.deliveryId))
				{
					ArenaTable.ArenaData arenaData = deliveryTableData.GetArenaData();
					if (arenaData != null && arenaData.rank >= borderRank && deliveryTableData.GetConditionType(0u) != DELIVERY_CONDITION_TYPE.COMPLETE_DELIVERY_ID)
					{
						list.Add(deliveryTableData);
						if (deliveryTableData.clearEventID != 0)
						{
							string text = deliveryTableData.clearEventTitle;
							if (string.IsNullOrEmpty(text))
							{
								text = deliveryTableData.name;
							}
							stories.Add(new Story((int)deliveryTableData.clearEventID, text));
						}
					}
				}
			}
		}
		return list;
	}

	protected override void InitStory(int index, Transform t)
	{
		bool flag = MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.isTheaterRenewal;
		if (HasChapterStory() && flag)
		{
			base.InitStory(index, t);
		}
		else
		{
			SetEvent(t, "SELECT_RUSH_STORY", index);
			SetLabelText(t, UI.LBL_STORY_TITLE, stories[index].title);
		}
	}

	protected override void InitNormalDelivery(int index, Transform t)
	{
		DeliveryTable.DeliveryData deliveryData = notClearDevliveries[index];
		if (timeAttackDeliveryIds.Contains(deliveryData.id))
		{
			SetEvent(t, "SELECT_TIMEATTACK_RUSH", index);
			SetUpCompletedArenaListItem(t, deliveryData);
			SetCompletedHaveCount(t, deliveryData);
		}
		else
		{
			SetEvent(t, "SELECT_RUSH", index);
			if (deliveryData.GetConditionType(0u) == DELIVERY_CONDITION_TYPE.COMPLETE_DELIVERY_ID)
			{
				SetUpArenaListItemRankUp(t, deliveryData);
			}
			else
			{
				SetUpArenaListItem(t, deliveryData);
			}
		}
	}

	private void SetUpArenaListItem(Transform t, DeliveryTable.DeliveryData info)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		QuestRequestItemArena questRequestItemArena = t.GetComponent<QuestRequestItemArena>();
		if (questRequestItemArena == null)
		{
			questRequestItemArena = t.get_gameObject().AddComponent<QuestRequestItemArena>();
		}
		questRequestItemArena.InitUI();
		questRequestItemArena.Setup(t, info);
	}

	private void SetUpArenaListItemRankUp(Transform t, DeliveryTable.DeliveryData info)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		QuestRequestItemArenaRankUp questRequestItemArenaRankUp = t.GetComponent<QuestRequestItemArenaRankUp>();
		if (questRequestItemArenaRankUp == null)
		{
			questRequestItemArenaRankUp = t.get_gameObject().AddComponent<QuestRequestItemArenaRankUp>();
		}
		questRequestItemArenaRankUp.InitUI();
		questRequestItemArenaRankUp.Setup(t, info);
	}

	private void SetUpCompletedArenaListItem(Transform t, DeliveryTable.DeliveryData info)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		QuestRequestItemArena questRequestItemArena = t.GetComponent<QuestRequestItemArena>();
		if (questRequestItemArena == null)
		{
			questRequestItemArena = t.get_gameObject().AddComponent<QuestRequestItemArena>();
		}
		questRequestItemArena.InitUI();
		questRequestItemArena.SetupComplete(t, info, record);
	}

	protected override void InitCompletedDelivery(int completedIndex, Transform t)
	{
		DeliveryTable.DeliveryData deliveryData = clearedDeliveries[completedIndex];
		SetEvent(t, "SELECT_COMPLETED_RUSH", completedIndex);
		if (deliveryData.GetConditionType(0u) == DELIVERY_CONDITION_TYPE.COMPLETE_DELIVERY_ID)
		{
			SetUpArenaListItemRankUp(t, deliveryData);
			SetActive(t, UI.OBJ_REQUEST_COMPLETED, true);
		}
		else
		{
			SetUpCompletedArenaListItem(t, deliveryData);
		}
		SetCompletedHaveCount(t, deliveryData);
	}

	private unsafe void OnQuery_SELECT_RUSH()
	{
		int index = (int)GameSection.GetEventData();
		DeliveryTable.DeliveryData dd = notClearDevliveries[index];
		Delivery notClearDelivery = GetNotClearDelivery(dd.id);
		if (MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int)dd.id))
		{
			changeToDeliveryClearEvent = true;
			bool is_tutorial = !TutorialStep.HasFirstDeliveryCompleted();
			bool enable_clear_event = dd.clearEventID != 0;
			GameSection.StayEvent();
			MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
			_003COnQuery_SELECT_RUSH_003Ec__AnonStorey3FB _003COnQuery_SELECT_RUSH_003Ec__AnonStorey3FB;
			MonoBehaviourSingleton<DeliveryManager>.I.SendDeliveryComplete(notClearDelivery.uId, enable_clear_event, new Action<bool, DeliveryRewardList>((object)_003COnQuery_SELECT_RUSH_003Ec__AnonStorey3FB, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
		else if (dd.GetConditionType(0u) == DELIVERY_CONDITION_TYPE.COMPLETE_DELIVERY_ID)
		{
			GameSection.SetEventData(new object[2]
			{
				(int)dd.id,
				null
			});
		}
		else
		{
			ArenaTable.ArenaData arenaData = dd.GetArenaData();
			MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID((uint)arenaData.questIds[0], true);
			MonoBehaviourSingleton<QuestManager>.I.SetCurrentArenaId(arenaData.id);
			GameSection.ChangeEvent("TO_ROOM", dd);
		}
	}

	private void InitGoToRankingButton(Transform t)
	{
		SetEvent(t, "RANKING", eventData);
	}

	private void OnQuery_RANKING()
	{
		Network.EventData eventData = GameSection.GetEventData() as Network.EventData;
		if (eventData == null)
		{
			EventData[] autoEvents = new EventData[2]
			{
				new EventData("RANK", null),
				new EventData("LAST", new object[2]
				{
					"null",
					-1
				})
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
		}
	}

	private void OnQuery_SELECT_COMPLETED_RUSH()
	{
		int index = (int)GameSection.GetEventData();
		DeliveryTable.DeliveryData deliveryData = clearedDeliveries[index];
		if (deliveryData.GetConditionType(0u) == DELIVERY_CONDITION_TYPE.COMPLETE_DELIVERY_ID)
		{
			int id = (int)deliveryData.id;
			DeliveryRewardList deliveryRewardList = new DeliveryRewardList();
			GameSection.SetEventData(new object[3]
			{
				id,
				deliveryRewardList,
				true
			});
		}
		else
		{
			ArenaTable.ArenaData arenaData = deliveryData.GetArenaData();
			MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID((uint)arenaData.questIds[0], true);
			MonoBehaviourSingleton<QuestManager>.I.SetCurrentArenaId(arenaData.id);
			GameSection.ChangeEvent("TO_ROOM", deliveryData);
		}
	}

	private void OnQuery_SELECT_TIMEATTACK_RUSH()
	{
		int index = (int)GameSection.GetEventData();
		DeliveryTable.DeliveryData deliveryData = notClearDevliveries[index];
		ArenaTable.ArenaData arenaData = deliveryData.GetArenaData();
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID((uint)arenaData.questIds[0], true);
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentArenaId(arenaData.id);
		GameSection.ChangeEvent("TO_ROOM", deliveryData);
	}

	private void OnQuery_SELECT_RUSH_STORY()
	{
		int index = (int)GameSection.GetEventData();
		Story story = stories[index];
		string name = (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge()) ? "MAIN_MENU_HOME" : "MAIN_MENU_LOUNGE";
		EventData[] array = new EventData[2]
		{
			new EventData(name, null),
			new EventData("ARENA_LIST", eventData)
		};
		GameSection.SetEventData(new object[4]
		{
			story.id,
			string.Empty,
			string.Empty,
			array
		});
	}

	private void OnQuery_CLOSE()
	{
		if (m_lastBGMId > 0)
		{
			SoundManager.RequestBGM(m_lastBGMId, true);
		}
	}

	private void OnQuery_SECTION_BACK()
	{
		if (m_lastBGMId > 0)
		{
			SoundManager.RequestBGM(m_lastBGMId, true);
		}
	}

	private void _SorteliveryList()
	{
		notClearDevliveries.Clear();
		timeAttackDeliveryIds.Clear();
		for (int i = 0; i < visibleDeliveryList.Count; i++)
		{
			DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)visibleDeliveryList[i].dId);
			if (deliveryTableData != null)
			{
				notClearDevliveries.Add(deliveryTableData);
			}
		}
		for (int j = 0; j < clearedDeliveries.Count; j++)
		{
			DeliveryTable.DeliveryData deliveryData = clearedDeliveries[j];
			if (deliveryData != null)
			{
				ArenaTable.ArenaData arenaData = deliveryData.GetArenaData();
				if (arenaData != null && arenaData.rank == ARENA_RANK.S)
				{
					notClearDevliveries.Add(deliveryData);
					clearedDeliveries.Remove(deliveryData);
					timeAttackDeliveryIds.Add(deliveryData.id);
					j--;
				}
			}
		}
		notClearDevliveries.Sort(new ArenaSort());
	}

	private Delivery GetNotClearDelivery(uint deliveryId)
	{
		return visibleDeliveryList.Find((Delivery d) => d.dId == deliveryId);
	}

	private void OnQuery_HOW_TO()
	{
		GameSection.SetEventData(WebViewManager.Arena);
	}
}
