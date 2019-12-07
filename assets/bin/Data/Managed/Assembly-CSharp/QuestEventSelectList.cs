using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEventSelectList : QuestSpecialSelect
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
		OBJ_ACTIVE_ROOT,
		OBJ_INACTIVE_ROOT,
		LBL_MAX,
		LBL_NOW,
		BTN_INGAME_INFO,
		BTN_FISHING_RECORD,
		SPR_CLEARD_BLACK
	}

	protected class Story
	{
		public int id;

		public string title;

		public Story(int id, string title)
		{
			this.id = id;
			this.title = title;
		}
	}

	public class ShowDeliveryData
	{
		public int index = -1;

		public bool isCompleted;

		public DeliveryTable.DeliveryData data;

		public ShowDeliveryData(int i, bool isComp, DeliveryTable.DeliveryData d)
		{
			index = i;
			isCompleted = isComp;
			data = d;
		}

		public ShowDeliveryData(int i, bool isComp, Delivery d)
		{
			index = i;
			isCompleted = isComp;
			data = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)d.dId);
		}
	}

	protected Network.EventData eventData;

	protected List<DeliveryTable.DeliveryData> clearedDeliveries;

	protected List<Story> stories = new List<Story>();

	protected LabelWidthLimitter title;

	protected LabelWidthLimitter titleEffect;

	protected Transform mapItem;

	protected bool isResetUI;

	protected int nowPage = 1;

	protected int pageMax = 1;

	protected virtual bool showStory => true;

	protected virtual bool showMap => true;

	public override void Initialize()
	{
		eventData = (GameSection.GetEventData() as Network.EventData);
		SkipTween(UI.SPR_DELIVERY_BTN_SELECTED);
		SetActive(UI.OBJ_DELIVERY_ROOT, is_visible: true);
		int width = GetWidth(UI.WGT_LOCATION_NAME_LIMIT);
		title = new LabelWidthLimitter(GetCtrl(UI.LBL_LOCATION_NAME).GetComponent<UILabel>(), width, fixAnchorForEffect: false);
		titleEffect = new LabelWidthLimitter(GetCtrl(UI.LBL_LOCATION_NAME_EFFECT).GetComponent<UILabel>(), width, fixAnchorForEffect: true);
		base.Initialize();
	}

	protected override IEnumerator DoInitialize()
	{
		string eventBG = ResourceName.GetEventBG(eventData.bannerId);
		Hash128 hash = default(Hash128);
		if (MonoBehaviourSingleton<ResourceManager>.I.event_manifest != null)
		{
			hash = MonoBehaviourSingleton<ResourceManager>.I.event_manifest.GetAssetBundleHash(RESOURCE_CATEGORY.EVENT_BG.ToAssetBundleName(eventBG));
		}
		if (MonoBehaviourSingleton<ResourceManager>.I.event_manifest == null || hash.isValid)
		{
			LoadingQueue loadingQueue = new LoadingQueue(this);
			LoadObject lo_bg = loadingQueue.Load(isEventAsset: true, RESOURCE_CATEGORY.EVENT_BG, eventBG);
			LoadObject lo_item = null;
			if (ShouldShowEventMapButton())
			{
				lo_item = loadingQueue.Load(RESOURCE_CATEGORY.QUEST_ITEM, "QEM_10000000");
			}
			if (loadingQueue.IsLoading())
			{
				yield return loadingQueue.Wait();
			}
			Texture2D texture = lo_bg.loadedObject as Texture2D;
			SetTexture(UI.TEX_EVENT_BG, texture);
			if (lo_item != null && null != lo_item.loadedObject)
			{
				GameObject gameObject = lo_item.loadedObject as GameObject;
				mapItem = gameObject.transform;
			}
		}
		GetDeliveryList();
		EndInitialize();
	}

	public override void Exit()
	{
		base.Exit();
	}

	protected override void OnOpen()
	{
		StartCoroutine(InitScroll());
		base.OnOpen();
	}

	protected override void RemoveRecommend()
	{
	}

	private IEnumerator InitScroll()
	{
		UIScrollView scroll = GetComponent<UIScrollView>(UI.SCR_DELIVERY_QUEST);
		while (base.state != STATE.OPEN)
		{
			yield return null;
		}
		scroll.enabled = scroll.shouldMoveVertically;
		RepositionTable();
	}

	protected virtual bool ShouldShowEventMapButton()
	{
		if (!showMap)
		{
			return false;
		}
		RegionTable.Data data = Array.Find(Singleton<RegionTable>.I.GetData(), (RegionTable.Data o) => o.eventId == eventData.eventId);
		return data != null;
	}

	protected override bool IsVisibleDelivery(Delivery delivery, DeliveryTable.DeliveryData tableData)
	{
		if (eventData == null)
		{
			return false;
		}
		if (tableData.IsEvent() && tableData.eventID == eventData.eventId)
		{
			return true;
		}
		return false;
	}

	protected void RepositionTable()
	{
		UITable component = GetComponent<UITable>(UI.TBL_DELIVERY_QUEST);
		if ((bool)component)
		{
			component.Reposition();
			List<Transform> childList = component.GetChildList();
			int i = 0;
			for (int count = childList.Count; i < count; i++)
			{
				Vector3 localPosition = childList[i].localPosition;
				localPosition.x = 0f;
				childList[i].localPosition = localPosition;
			}
		}
	}

	protected bool HasChapterStory()
	{
		return GetChapterId() > 0;
	}

	protected int GetChapterId()
	{
		if (!MonoBehaviourSingleton<TheaterModeTable>.IsValid())
		{
			return -1;
		}
		if (stories == null || stories.Count < 1)
		{
			return -1;
		}
		int chapterId = -1;
		MonoBehaviourSingleton<TheaterModeTable>.I.AllTheaterData(delegate(TheaterModeTable.TheaterModeData data)
		{
			int i = 0;
			for (int count = stories.Count; i < count; i++)
			{
				if (data.script_id == stories[i].id)
				{
					chapterId = data.chapter_id;
				}
			}
		});
		return chapterId;
	}

	public override void UpdateUI()
	{
		SetLabelText(UI.LBL_LOCATION_NAME, eventData.name);
		SetLabelText(UI.LBL_LOCATION_NAME_EFFECT, eventData.name);
		SetActive(UI.BTN_INFO, !string.IsNullOrEmpty(eventData.linkName));
		title.Update();
		titleEffect.Update();
		stories.Clear();
		if (eventData.prologueStoryId > 0)
		{
			stories.Add(new Story(eventData.prologueStoryId, eventData.prologueTitle));
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() != "InGameScene")
		{
			SetActive(UI.BTN_FISHING_RECORD, eventData.subButtonType == 1);
		}
		clearedDeliveries = CreateClearedDliveryList();
		UpdateList();
		UpdateAnchors();
		isResetUI = false;
	}

	protected virtual List<DeliveryTable.DeliveryData> CreateClearedDliveryList()
	{
		List<DeliveryTable.DeliveryData> list = new List<DeliveryTable.DeliveryData>();
		List<ClearStatusDelivery> clearStatusDelivery = MonoBehaviourSingleton<DeliveryManager>.I.clearStatusDelivery;
		int i = 0;
		for (int count = clearStatusDelivery.Count; i < count; i++)
		{
			ClearStatusDelivery d = clearStatusDelivery[i];
			if (d.deliveryStatus != 3)
			{
				continue;
			}
			DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)d.deliveryId);
			if (deliveryTableData.eventID != eventData.eventId || Array.Exists(deliveryInfo, (Delivery x) => x.dId == d.deliveryId))
			{
				continue;
			}
			list.Add(deliveryTableData);
			if (deliveryTableData.clearEventID != 0)
			{
				string value = deliveryTableData.clearEventTitle;
				if (string.IsNullOrEmpty(value))
				{
					value = deliveryTableData.name;
				}
				stories.Add(new Story((int)deliveryTableData.clearEventID, value));
			}
		}
		return list;
	}

	protected void UpdateList()
	{
		if ((bool)GetCtrl(UI.TBL_DELIVERY_QUEST))
		{
			UpdateTable();
		}
		else
		{
			UpdateGrid();
		}
	}

	protected virtual void UpdateTable()
	{
		int num = 0;
		if (stories.Count > 0)
		{
			num++;
		}
		List<ShowDeliveryData> list = new List<ShowDeliveryData>();
		if (deliveryInfo != null)
		{
			for (int j = 0; j < deliveryInfo.Length; j++)
			{
				ShowDeliveryData item = new ShowDeliveryData(j, isComp: false, deliveryInfo[j]);
				list.Add(item);
			}
		}
		if (clearedDeliveries != null)
		{
			for (int k = 0; k < clearedDeliveries.Count; k++)
			{
				ShowDeliveryData item2 = new ShowDeliveryData(k, isComp: true, clearedDeliveries[k]);
				list.Add(item2);
			}
		}
		pageMax = 1 + (list.Count - 1) / 10;
		bool flag = pageMax > 1;
		SetActive(UI.OBJ_ACTIVE_ROOT, flag);
		SetActive(UI.OBJ_INACTIVE_ROOT, !flag);
		SetLabelText(UI.LBL_MAX, pageMax.ToString());
		SetLabelText(UI.LBL_NOW, nowPage.ToString());
		ShowDeliveryData[] showList = GetPagingList(list.ToArray(), 10, nowPage);
		int num2 = showList.Length;
		if (showStory)
		{
			num2 += num + stories.Count;
		}
		if (num2 == 0)
		{
			SetActive(UI.STR_DELIVERY_NON_LIST, is_visible: true);
			SetActive(UI.GRD_DELIVERY_QUEST, is_visible: false);
			SetActive(UI.TBL_DELIVERY_QUEST, is_visible: false);
			return;
		}
		SetActive(UI.STR_DELIVERY_NON_LIST, is_visible: false);
		SetActive(UI.GRD_DELIVERY_QUEST, is_visible: false);
		SetActive(UI.TBL_DELIVERY_QUEST, is_visible: true);
		bool flag2 = false;
		if (ShouldShowEventMapButton())
		{
			flag2 = true;
			num2++;
		}
		int questStartIndex = 0;
		if (flag2)
		{
			int num3 = ++questStartIndex;
		}
		int borderIndex = questStartIndex + showList.Length;
		int storyStartIndex = borderIndex;
		if (stories.Count > 0)
		{
			storyStartIndex++;
		}
		Transform ctrl = GetCtrl(UI.TBL_DELIVERY_QUEST);
		if ((bool)ctrl)
		{
			int l = 0;
			for (int childCount = ctrl.childCount; l < childCount; l++)
			{
				Transform child = ctrl.GetChild(0);
				child.parent = null;
				UnityEngine.Object.Destroy(child.gameObject);
			}
		}
		bool isRenewalFlag = MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.isTheaterRenewal;
		SetTable(UI.TBL_DELIVERY_QUEST, "", num2, isResetUI, delegate(int i, Transform parent)
		{
			Transform transform = null;
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
				return Realizes("QuestEventBorderItem", parent);
			}
			if (i >= questStartIndex)
			{
				return Realizes("QuestRequestItem", parent);
			}
			return (null != mapItem) ? ResourceUtility.Realizes(mapItem.gameObject, parent) : Realizes("QuestEventBorderItem", parent);
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
				else if (i < borderIndex || i >= storyStartIndex)
				{
					if (i >= questStartIndex && i < borderIndex)
					{
						int num4 = i - questStartIndex;
						InitDelivery(showList[num4], t);
						ChangeDeliveryFrameSprite(t);
					}
					else if (i < questStartIndex)
					{
						InitMap(t);
					}
				}
			}
		});
		GetComponent<UIScrollView>(UI.SCR_DELIVERY_QUEST).enabled = true;
		RepositionTable();
	}

	protected virtual void ChangeDeliveryFrameSprite(Transform parent)
	{
	}

	protected virtual void UpdateGrid()
	{
		List<ShowDeliveryData> list = new List<ShowDeliveryData>();
		if (deliveryInfo != null)
		{
			for (int j = 0; j < deliveryInfo.Length; j++)
			{
				ShowDeliveryData item = new ShowDeliveryData(j, isComp: false, deliveryInfo[j]);
				list.Add(item);
			}
		}
		if (clearedDeliveries != null)
		{
			for (int k = 0; k < clearedDeliveries.Count; k++)
			{
				ShowDeliveryData item2 = new ShowDeliveryData(k, isComp: true, clearedDeliveries[k]);
				list.Add(item2);
			}
		}
		pageMax = 1 + (list.Count - 1) / 10;
		bool flag = pageMax > 1;
		SetActive(UI.OBJ_ACTIVE_ROOT, flag);
		SetActive(UI.OBJ_INACTIVE_ROOT, !flag);
		SetLabelText(UI.LBL_MAX, pageMax.ToString());
		SetLabelText(UI.LBL_NOW, nowPage.ToString());
		ShowDeliveryData[] showList = GetPagingList(list.ToArray(), 10, nowPage);
		if (list.Count == 0)
		{
			SetActive(UI.STR_DELIVERY_NON_LIST, is_visible: true);
			SetActive(UI.GRD_DELIVERY_QUEST, is_visible: false);
		}
		else
		{
			SetActive(UI.STR_DELIVERY_NON_LIST, is_visible: false);
			SetActive(UI.GRD_DELIVERY_QUEST, is_visible: true);
			SetDynamicList(UI.GRD_DELIVERY_QUEST, "QuestRequestItem", showList.Length, isResetUI, null, null, delegate(int i, Transform t, bool is_recycle)
			{
				SetActive(t, is_visible: true);
				if (showList[i].data.subType == DELIVERY_SUB_TYPE.ASSIGNED_EQUIPMENT)
				{
					SetEvent(t, "COMPLETED_ASSIGNED_EQUIPMENT", showList[i].index);
				}
				else if (!showList[i].isCompleted)
				{
					SetEvent(t, "SELECT_DELIVERY", showList[i].index);
				}
				else
				{
					SetEvent(t, "SELECT_COMPLETED_DELIVERY", showList[i].index);
				}
				SetupDeliveryListItem(t, showList[i].data);
				if (showList[i].isCompleted)
				{
					SetActive(t, UI.OBJ_REQUEST_COMPLETED, is_visible: true);
				}
			});
		}
	}

	protected void InitDelivery(ShowDeliveryData showData, Transform t)
	{
		if (showData.isCompleted)
		{
			bool flag = false;
			if (showData.data != null)
			{
				QuestTable.QuestTableData questData = showData.data.GetQuestData();
				if (questData != null && questData.questType == QUEST_TYPE.HAPPEN)
				{
					flag = true;
				}
			}
			if (flag)
			{
				SetEvent(t, "SELECT_COMPLETED_DELIVERY_HAPPEN", showData.index);
			}
			else if (showData.data.subType == DELIVERY_SUB_TYPE.ASSIGNED_EQUIPMENT)
			{
				SetEvent(t, "COMPLETED_ASSIGNED_EQUIPMENT", showData.index);
			}
			else
			{
				SetEvent(t, "SELECT_COMPLETED_DELIVERY", showData.index);
			}
			UpdateCompletedDeliveryUI(t);
			SetupDeliveryListItem(t, showData.data);
			SetActive(t, UI.OBJ_REQUEST_COMPLETED, is_visible: true);
			SetCompletedHaveCount(t, showData.data);
		}
		else
		{
			if (showData.data != null && showData.data.subType == DELIVERY_SUB_TYPE.READ_STORY)
			{
				SetEvent(t, "READ_STORY", showData.index);
			}
			else if (showData.data != null && showData.data.subType == DELIVERY_SUB_TYPE.ASSIGNED_EQUIPMENT)
			{
				SetEvent(t, "ASSIGNED_EQUIPMENT", showData.index);
			}
			else
			{
				SetEvent(t, "SELECT_DELIVERY", showData.index);
			}
			SetupDeliveryListItem(t, showData.data);
		}
	}

	protected virtual void UpdateCompletedDeliveryUI(Transform parent)
	{
	}

	private void OnQuery_ASSIGNED_EQUIPMENT()
	{
		int num = (int)GameSection.GetEventData();
		int dId = deliveryInfo[num].dId;
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)dId);
		bool flag = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery(dId);
		if (flag)
		{
			GameSection.StayEvent();
			SendDeliveryComplete(num, dId, flag, is_happen_quest: false);
		}
		else
		{
			GameSection.SetEventData(deliveryTableData);
		}
	}

	private void OnQuery_COMPLETED_ASSIGNED_EQUIPMENT()
	{
		int index = (int)GameSection.GetEventData();
		GameSection.SetEventData(clearedDeliveries[index]);
	}

	protected virtual void InitNormalDelivery(int index, Transform t)
	{
		SetEvent(t, "SELECT_DELIVERY", index);
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryInfo[index].dId);
		SetupDeliveryListItem(t, deliveryTableData);
	}

	protected virtual void InitCompletedDelivery(int completedIndex, Transform t)
	{
		DeliveryTable.DeliveryData deliveryData = clearedDeliveries[completedIndex];
		bool flag = false;
		if (deliveryData != null)
		{
			QuestTable.QuestTableData questData = deliveryData.GetQuestData();
			if (questData != null && questData.questType == QUEST_TYPE.HAPPEN)
			{
				flag = true;
			}
		}
		if (flag)
		{
			SetEvent(t, "SELECT_COMPLETED_DELIVERY_HAPPEN", completedIndex);
		}
		else
		{
			SetEvent(t, "SELECT_COMPLETED_DELIVERY", completedIndex);
		}
		SetupDeliveryListItem(t, deliveryData);
		SetActive(t, UI.OBJ_REQUEST_COMPLETED, is_visible: true);
		SetCompletedHaveCount(t, deliveryData);
	}

	protected virtual void InitStory(int storyIndex, Transform t)
	{
		bool flag = MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.isTheaterRenewal;
		if (HasChapterStory() && flag)
		{
			int event_data = (eventData != null) ? eventData.eventId : 0;
			SetEvent(t, "JUMP_TO_STORY_PAGE", event_data);
			SetLabelText(t, UI.LBL_STORY_TITLE, "View All Stories");
		}
		else
		{
			SetEvent(t, "SELECT_STORY", storyIndex);
			SetLabelText(t, UI.LBL_STORY_TITLE, stories[storyIndex].title);
		}
	}

	protected virtual void InitMap(Transform t)
	{
		SetEvent(t, "WORLDMAP", eventData.eventId);
		SetLabelText(t, UI.LBL_STORY_TITLE, "マップ");
	}

	protected virtual void OnQuery_INFO()
	{
		GameSection.SetEventData(string.Format(WebViewManager.NewsWithLinkParamFormat, eventData.linkName));
	}

	private void OnQuery_SELECT_COMPLETED_DELIVERY()
	{
		SetCompletedDeliveryEventData();
	}

	private void OnQuery_SELECT_COMPLETED_DELIVERY_HAPPEN()
	{
		SetCompletedDeliveryEventData();
	}

	private void SetCompletedDeliveryEventData()
	{
		int index = (int)GameSection.GetEventData();
		int id = (int)clearedDeliveries[index].id;
		DeliveryRewardList deliveryRewardList = new DeliveryRewardList();
		GameSection.SetEventData(new object[3]
		{
			id,
			deliveryRewardList,
			true
		});
	}

	protected virtual void OnQuery_JUMP_TO_STORY_PAGE()
	{
		EventData[] array = new EventData[2]
		{
			new EventData("SELECT_EVENT", null),
			new EventData("SELECT_CHAPTER_FROM_OUTER", null)
		};
		GameSection.SetEventData(new object[2]
		{
			GetChapterId(),
			array
		});
	}

	protected virtual void OnQuery_SELECT_STORY()
	{
		int index = (int)GameSection.GetEventData();
		Story story = stories[index];
		string goingHomeEvent = GameSection.GetGoingHomeEvent();
		EventData[] array;
		if (eventData == null || !MonoBehaviourSingleton<DeliveryManager>.IsValid() || MonoBehaviourSingleton<DeliveryManager>.I.eventListData.IsNullOrEmpty() || MonoBehaviourSingleton<DeliveryManager>.I.GetEventListData(eventData.eventId) == null)
		{
			array = new EventData[3]
			{
				new EventData(goingHomeEvent, null),
				new EventData("TO_EVENT", null),
				new EventData("SELECT", eventData)
			};
		}
		else
		{
			EventListData eventListData = MonoBehaviourSingleton<DeliveryManager>.I.GetEventListData(eventData.eventId);
			array = new EventData[4]
			{
				new EventData(goingHomeEvent, null),
				new EventData("TO_EVENT", null),
				(eventListData.placeEnum == EVENT_DISPLAY_PLACE.PRESENT) ? new EventData("TAB_PRESENT", null) : new EventData("TAB_EVENT", null),
				new EventData("SELECT", eventData)
			};
		}
		GameSection.SetEventData(new object[4]
		{
			story.id,
			"",
			"",
			array
		});
	}

	protected virtual void OnQuery_WORLDMAP()
	{
		int eventId = (int)GameSection.GetEventData();
		RegionTable.Data data = Array.Find(Singleton<RegionTable>.I.GetData(), (RegionTable.Data o) => o.eventId == eventId);
		if (data != null)
		{
			MonoBehaviourSingleton<WorldMapManager>.I.ignoreTutorial = true;
			RequestEvent("DIRECT_EVENT", (int)data.regionId);
		}
	}

	private void OnQuery_PAGE_PREV()
	{
		isResetUI = true;
		nowPage = ((nowPage > 1) ? (nowPage - 1) : pageMax);
		RefreshUI();
	}

	private void OnQuery_PAGE_NEXT()
	{
		isResetUI = true;
		nowPage = ((nowPage >= pageMax) ? 1 : (nowPage + 1));
		RefreshUI();
	}

	private void OnQuery_FISHING_RECORD()
	{
		GameSection.SetEventData(eventData.eventId);
	}
}
