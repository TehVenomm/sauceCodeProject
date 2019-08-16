using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class QuestSeriesArenaEventList : QuestEventSelectList
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
		SPR_FRAME,
		TEX_EVENT_BANNER,
		LBL_RIGHT_VALUE,
		SPR_ICON_EXISTPRESENT,
		SPR_ICON_NEW,
		SPR_ICON_EVENTREWARD,
		TEX_ICON,
		SPR_MISSION_CROWN_ON,
		SPR_MISSION_CROWN_OFF,
		LBL_STORY_TITLE
	}

	private const int SERIES_ARENA_BGM = 134;

	private static int m_lastBGMId;

	private Dictionary<int, LoadObject> bannerTable;

	protected List<EventListData> seriesArenaEventList = new List<EventListData>();

	protected EventListData seriesArenaTopData;

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
		if (eventData == null)
		{
			bool is_recv_delivery = false;
			MonoBehaviourSingleton<QuestManager>.I.SendGetEventList(delegate
			{
				is_recv_delivery = true;
			});
			while (!is_recv_delivery)
			{
				yield return null;
			}
			is_recv_delivery = false;
			MonoBehaviourSingleton<DeliveryManager>.I.SendEventList(delegate
			{
				is_recv_delivery = true;
			});
			while (!is_recv_delivery)
			{
				yield return null;
			}
			EventListData data = MonoBehaviourSingleton<DeliveryManager>.I.FindSeriesArenaTopData();
			eventData = ChoiceEventData(data);
			GameSection.SetEventData(data);
		}
		if (MonoBehaviourSingleton<SoundManager>.IsValid() && MonoBehaviourSingleton<SoundManager>.I.playingBGMID != 3)
		{
			m_lastBGMId = MonoBehaviourSingleton<SoundManager>.I.playingBGMID;
			SoundManager.RequestBGM(134);
		}
		seriesArenaTopData = MonoBehaviourSingleton<DeliveryManager>.I.FindSeriesArenaTopData();
		yield return this.StartCoroutine(LoadSeriesArenaTopBanner());
		GetDeliveryList();
		EndInitialize();
	}

	protected IEnumerator LoadSeriesArenaTopBanner()
	{
		string resourceName = ResourceName.GetEventBG(seriesArenaTopData.bannerId);
		Hash128 hash = default(Hash128);
		if (MonoBehaviourSingleton<ResourceManager>.I.manifest != null)
		{
			hash = MonoBehaviourSingleton<ResourceManager>.I.manifest.GetAssetBundleHash(RESOURCE_CATEGORY.EVENT_BG.ToAssetBundleName(resourceName));
		}
		Utility.CreateGameObjectAndComponent("TheaterModeTable", this.get_gameObject().get_transform());
		while (MonoBehaviourSingleton<TheaterModeTable>.I.isLoading)
		{
			yield return null;
		}
		if (MonoBehaviourSingleton<ResourceManager>.I.manifest == null || hash.get_isValid())
		{
			LoadingQueue load_queue = new LoadingQueue(this);
			LoadObject lo_bg = load_queue.Load(RESOURCE_CATEGORY.EVENT_BG, resourceName);
			if (load_queue.IsLoading())
			{
				yield return load_queue.Wait();
			}
			SetTexture(texture: lo_bg.loadedObject as Texture2D, texture_enum: UI.TEX_EVENT_BG);
		}
	}

	protected IEnumerator LoadBanner(Transform t, int bannerId)
	{
		LoadingQueue loadingQueue = new LoadingQueue(this);
		LoadObject obj = loadingQueue.Load(RESOURCE_CATEGORY.EVENT_ICON, ResourceName.GetEventBanner(bannerId));
		if (loadingQueue.IsLoading())
		{
			yield return loadingQueue.Wait();
		}
		Texture2D bannerTex = obj.loadedObject as Texture2D;
		if (bannerTex != null)
		{
			Transform t2 = FindCtrl(t, UI.TEX_EVENT_BANNER);
			SetTexture(t2, bannerTex);
			SetActive(t2, is_visible: true);
		}
	}

	protected void UpdateNoArenaTable()
	{
		int item_num = 1;
		SetTable(UI.TBL_DELIVERY_QUEST, string.Empty, item_num, reset: false, delegate(int i, Transform parent)
		{
			Transform result = null;
			if (i == 0)
			{
				result = Realizes("QuestArenaRequestItemToRanking", parent);
			}
			return result;
		}, delegate(int i, Transform t, bool is_recycle)
		{
			SetActive(t, is_visible: true);
			if (i == 0)
			{
				InitGoToRankingButton(t);
			}
		});
		UIScrollView component = base.GetComponent<UIScrollView>((Enum)UI.SCR_DELIVERY_QUEST);
		component.set_enabled(false);
		RepositionTable();
	}

	public override void UpdateUI()
	{
		CreateSeriesArenaList();
		if (eventData != null)
		{
			if (seriesArenaEventList.Count == 0 && eventData.enableRanking)
			{
				UpdateNoArenaTable();
			}
			else
			{
				base.UpdateUI();
			}
		}
	}

	public override void StartSection()
	{
		base.StartSection();
		if (eventData != null)
		{
			if (seriesArenaEventList.Count == 0 && eventData.enableRanking)
			{
				UpdateNoArenaTable();
			}
			else if (!IsPlayableVersion())
			{
				string event_data = string.Format(base.sectionData.GetText("REQUIRE_HIGHER_VERSION"), seriesArenaTopData.minVersion);
				RequestEvent("SELECT_VERSION", event_data);
			}
			else if (GameSaveData.instance.IsTutorialSeriesArena())
			{
				RequestEvent("HOW_TO");
			}
		}
	}

	private bool IsPlayableVersion()
	{
		Version nativeVersionFromName = NetworkNative.getNativeVersionFromName();
		return seriesArenaTopData.IsPlayableWith(nativeVersionFromName);
	}

	protected override void UpdateTable()
	{
		int num = 0;
		int count = stories.Count;
		if (count > 0)
		{
			num++;
		}
		int num2 = seriesArenaEventList.Count;
		if (eventData.enableRanking)
		{
			num2++;
		}
		if (showStory)
		{
			num2 += num + stories.Count;
		}
		int questStartIndex = 0;
		if (eventData.enableRanking)
		{
			questStartIndex++;
		}
		int borderIndex = seriesArenaEventList.Count + questStartIndex;
		int storyStartIndex = borderIndex;
		if (stories.Count > 0)
		{
			storyStartIndex++;
		}
		Transform ctrl = GetCtrl(UI.TBL_DELIVERY_QUEST);
		if (Object.op_Implicit(ctrl))
		{
			int j = 0;
			for (int childCount = ctrl.get_childCount(); j < childCount; j++)
			{
				Transform child = ctrl.GetChild(0);
				child.set_parent(null);
				Object.Destroy(child.get_gameObject());
			}
		}
		bool isRenewalFlag = MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.isTheaterRenewal;
		SetTable(UI.TBL_DELIVERY_QUEST, string.Empty, num2, reset: false, delegate(int i, Transform parent)
		{
			Transform result = null;
			if (i >= storyStartIndex)
			{
				if (!HasChapterStory() || i == storyStartIndex || !isRenewalFlag)
				{
					return Realizes("QuestEventStoryItem", parent);
				}
				return null;
			}
			if (i >= borderIndex)
			{
				result = Realizes("QuestEventBorderItem", parent);
			}
			else if (i >= questStartIndex)
			{
				result = Realizes("QuestEventListItemSeriesArena", parent);
			}
			else if (i == 0)
			{
				result = Realizes("QuestArenaRequestItemToRanking", parent);
			}
			return result;
		}, delegate(int i, Transform t, bool is_recycle)
		{
			if (!(t == null))
			{
				SetActive(t, is_visible: true);
				if (i >= storyStartIndex)
				{
					int storyIndex = i - storyStartIndex;
					InitStory(storyIndex, t);
				}
				else if (i < borderIndex)
				{
					if (i >= questStartIndex)
					{
						InitNormalDelivery(i - questStartIndex, t);
					}
					else if (i == 0)
					{
						InitGoToRankingButton(t);
					}
				}
			}
		});
		UIScrollView component = base.GetComponent<UIScrollView>((Enum)UI.SCR_DELIVERY_QUEST);
		component.set_enabled(true);
		RepositionTable();
	}

	protected override void InitStory(int index, Transform t)
	{
		bool flag = MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.isTheaterRenewal;
		if (HasChapterStory() && flag)
		{
			base.InitStory(index, t);
			return;
		}
		SetEvent(t, "SELECT_SERIES_ARENA_STORY", index);
		SetLabelText(t, UI.LBL_STORY_TITLE, stories[index].title);
	}

	private void OnQuery_SELECT_SERIES_ARENA_STORY()
	{
		int index = (int)GameSection.GetEventData();
		Story story = stories[index];
		string goingHomeEvent = GameSection.GetGoingHomeEvent();
		EventData[] array = new EventData[3]
		{
			new EventData(goingHomeEvent, null),
			new EventData("EVENT_COUNTER"),
			new EventData("SELECT_SERIES_ARENA")
		};
		GameSection.SetEventData(new object[4]
		{
			story.id,
			string.Empty,
			string.Empty,
			array
		});
	}

	protected void CreateSeriesArenaList()
	{
		seriesArenaEventList = MonoBehaviourSingleton<DeliveryManager>.I.FindSeriesArenaDataList();
	}

	protected override void InitNormalDelivery(int index, Transform t)
	{
		EventListData eventListData = seriesArenaEventList[index];
		SetEvent(t, "SELECT_SERIES_ARENA", eventListData.eventId);
		SetUpSeriesArenaListItem(t, eventListData);
	}

	private void OnQuery_SELECT_SERIES_ARENA()
	{
		int currentSeriesArenaId = (int)GameSection.GetEventData();
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentSeriesArenaId(currentSeriesArenaId);
	}

	protected void InitGoToRankingButton(Transform t)
	{
		SetEventName(t, "RANKING");
	}

	private void SetUpSeriesArenaListItem(Transform t, EventListData info)
	{
		int bannerId = info.bannerId;
		this.StartCoroutine(LoadBanner(t, bannerId));
		SetActive(t, UI.SPR_ICON_NEW, info.leftBadgeEnum == BADGE_1_CATEGORY.NEW_EVENT);
		SetActive(t, UI.SPR_ICON_EVENTREWARD, info.rightBadgeEnum == BADGE_2_CATEGORY.EVENT_REWARD);
		bool flag = MonoBehaviourSingleton<QuestManager>.I.CheckEventMissionAllClear(info.eventId);
		SetActive(t, UI.SPR_MISSION_CROWN_OFF, !flag);
		SetActive(t, UI.SPR_MISSION_CROWN_ON, flag);
		StringBuilder stringBuilder = new StringBuilder();
		if (info.rightBadgeEnum == BADGE_2_CATEGORY.DELIVERY_NUM)
		{
			stringBuilder.Append("[000000]");
			stringBuilder.Append(info.rightValue);
		}
		else if (info.leftBadgeEnum == BADGE_1_CATEGORY.CLEARED)
		{
			stringBuilder.Append("[000000]");
			stringBuilder.Append("全依頼クリア");
		}
		QuestTable.QuestTableData eventQuestData = Singleton<QuestTable>.I.GetEventQuestData(info.eventId);
		RARITY_TYPE rarity = eventQuestData.rarity;
		UITexture component = FindCtrl(t, UI.TEX_ICON).GetComponent<UITexture>();
		ResourceLoad.LoadWithSetUITexture(component, RESOURCE_CATEGORY.SERIES_ARENA_RANK_ICON, ResourceName.GetSeriesArenaRankIconName(rarity));
		SetLabelText(t, UI.LBL_RIGHT_VALUE, stringBuilder.ToString());
		SetSupportEncoding(t, UI.LBL_RIGHT_VALUE, isEnable: true);
		SetActive(t, UI.LBL_RIGHT_VALUE, is_visible: true);
		SetActive(t, UI.SPR_ICON_EXISTPRESENT, MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableEventDeliveryNum(info.eventId) > 0);
	}

	protected void OnQuery_CLOSE()
	{
		if (m_lastBGMId > 0)
		{
			SoundManager.RequestBGM(m_lastBGMId);
		}
	}

	private void OnQuery_SECTION_BACK()
	{
		if (m_lastBGMId > 0)
		{
			SoundManager.RequestBGM(m_lastBGMId);
		}
	}

	private void OnQuery_HOW_TO()
	{
		GameSection.SetEventData(WebViewManager.SeriesArena);
	}

	protected virtual void OnQuery_TO_UNIQUE_STATUS()
	{
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentSeriesArenaId(0);
	}

	private Network.EventData ChoiceEventData(EventListData eld)
	{
		if (eld == null)
		{
			return eld;
		}
		if (!MonoBehaviourSingleton<QuestManager>.IsValid())
		{
			return eld;
		}
		Network.EventData eventData = MonoBehaviourSingleton<QuestManager>.I._GetEventData(eld.eventId);
		if (eventData == null)
		{
			return eld;
		}
		return eventData;
	}
}
