using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestAreaDeliveryList : QuestEventSelectList
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
		TEX_AREA_BG,
		GRD_DELIVERY,
		BTN_SUMMARY
	}

	protected RegionTable.Data regionData;

	protected override IEnumerator DoInitialize()
	{
		int num = (int)GameSection.GetEventData();
		regionData = Singleton<RegionTable>.I.GetData((uint)num);
		bool is_visible = num != 0;
		SetActive(UI.BTN_SUMMARY, is_visible);
		LoadingQueue loadQueue = new LoadingQueue(this);
		string areaBG = ResourceName.GetAreaBG(num);
		LoadObject bannerObj = loadQueue.Load(RESOURCE_CATEGORY.AREA_BACKGROUND, areaBG);
		if (loadQueue.IsLoading())
		{
			yield return loadQueue.Wait();
		}
		Texture2D texture2D = bannerObj.loadedObject as Texture2D;
		if (texture2D != null)
		{
			SetTexture(UI.TEX_AREA_BG, texture2D);
		}
		if (ShouldShowEventMapButton())
		{
			LoadObject item = loadQueue.Load(RESOURCE_CATEGORY.QUEST_ITEM, "QEM_10000001");
			if (loadQueue.IsLoading())
			{
				yield return loadQueue.Wait();
			}
			Texture2D texture = item.loadedObject as Texture2D;
			SetTexture(UI.TEX_EVENT_BG, texture);
			if (item != null && null != item.loadedObject)
			{
				GameObject gameObject = item.loadedObject as GameObject;
				mapItem = gameObject.transform;
			}
		}
		SetAreaName();
		EndInitialize();
	}

	public override void UpdateUI()
	{
		stories.Clear();
		GetDeliveryList();
		clearedDeliveries = CreateClearedDliveryList();
		UpdateTable();
		UpdateAnchors();
		SetAreaName();
		title.Update();
		titleEffect.Update();
		isResetUI = false;
	}

	private void SetAreaName()
	{
		SetLabelText(UI.LBL_LOCATION_NAME, regionData.regionName);
		SetLabelText(UI.LBL_LOCATION_NAME_EFFECT, regionData.regionName);
	}

	protected override void UpdateTable()
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
		int l = 0;
		for (int childCount = ctrl.childCount; l < childCount; l++)
		{
			Transform child = ctrl.GetChild(0);
			child.parent = null;
			UnityEngine.Object.Destroy(child.gameObject);
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
				else if (i < borderIndex)
				{
					if (i >= questStartIndex)
					{
						int num4 = i - questStartIndex;
						InitDelivery(showList[num4], t);
					}
					else
					{
						InitMap(t);
					}
				}
			}
		});
		GetComponent<UIScrollView>(UI.SCR_DELIVERY_QUEST).enabled = true;
		RepositionTable();
	}

	protected override void InitMap(Transform t)
	{
		SetEvent(t, "WORLDMAP", regionData.regionId);
		SetLabelText(t, UI.LBL_STORY_TITLE, "マップ");
	}

	protected override void GetDeliveryList()
	{
		deliveryInfo = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList();
		int groupId = regionData.groupId;
		uint regionId = regionData.regionId;
		List<Delivery> list = new List<Delivery>();
		int i = 0;
		for (int num = deliveryInfo.Length; i < num; i++)
		{
			Delivery delivery = deliveryInfo[i];
			DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)delivery.dId);
			if (deliveryTableData == null)
			{
				Log.Warning("DeliveryTable Not Found : dId " + delivery.dId);
			}
			else if (!deliveryTableData.IsEvent())
			{
				if (Singleton<NPCTable>.I.GetNPCData((int)deliveryTableData.npcID) == null)
				{
					Log.Error("DeliveryTable NPC ID Found  : dId " + delivery.dId + " : npcID " + deliveryTableData.npcID);
				}
				else if (deliveryTableData.regionId == regionId)
				{
					list.Add(delivery);
				}
				else if (groupId > 0 && deliveryTableData.regionId == groupId)
				{
					list.Add(delivery);
				}
			}
		}
		deliveryInfo = list.ToArray();
	}

	protected override List<DeliveryTable.DeliveryData> CreateClearedDliveryList()
	{
		List<DeliveryTable.DeliveryData> list = new List<DeliveryTable.DeliveryData>();
		List<ClearStatusDelivery> clearStatusDelivery = MonoBehaviourSingleton<DeliveryManager>.I.clearStatusDelivery;
		int groupId = regionData.groupId;
		uint regionId = regionData.regionId;
		int i = 0;
		for (int count = clearStatusDelivery.Count; i < count; i++)
		{
			ClearStatusDelivery d = clearStatusDelivery[i];
			if (d.deliveryStatus != 3)
			{
				continue;
			}
			DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)d.deliveryId);
			if ((deliveryTableData.type != DELIVERY_TYPE.STORY && deliveryTableData.type != DELIVERY_TYPE.ONCE) || (deliveryTableData.regionId != regionId && (groupId <= 0 || deliveryTableData.regionId != groupId)) || Array.Exists(deliveryInfo, (Delivery x) => x.dId == d.deliveryId))
			{
				continue;
			}
			list.Add(deliveryTableData);
			if (deliveryTableData.readScriptId != 0)
			{
				string text = deliveryTableData.clearEventTitle;
				if (string.IsNullOrEmpty(text))
				{
					text = deliveryTableData.name;
				}
				stories.Add(new Story((int)deliveryTableData.readScriptId, text));
			}
			else if (deliveryTableData.clearEventID != 0)
			{
				string text2 = deliveryTableData.clearEventTitle;
				if (string.IsNullOrEmpty(text2))
				{
					text2 = deliveryTableData.name;
				}
				stories.Add(new Story((int)deliveryTableData.clearEventID, text2));
			}
		}
		return list;
	}

	protected override bool ShouldShowEventMapButton()
	{
		return showMap;
	}

	protected override void OnQuery_SELECT_STORY()
	{
		int index = (int)GameSection.GetEventData();
		Story story = stories[index];
		string goingHomeEvent = GameSection.GetGoingHomeEvent();
		EventData[] array = new EventData[3]
		{
			new EventData(goingHomeEvent, null),
			new EventData("TO_QUEST", null),
			new EventData("SELECT_AREA", (int)regionData.regionId)
		};
		GameSection.SetEventData(new object[4]
		{
			story.id,
			"",
			"",
			array
		});
	}

	protected override void OnQuery_WORLDMAP()
	{
		if (regionData != null)
		{
			MonoBehaviourSingleton<WorldMapManager>.I.ignoreTutorial = true;
			RequestEvent("OPEN_REGION_CHANGE", (int)regionData.regionId);
		}
	}

	private void OnQuery_SUMMARY()
	{
		GameSection.SetEventData((int)regionData.regionId);
	}

	protected override void OnQuery_JUMP_TO_STORY_PAGE()
	{
		EventData[] array = new EventData[1]
		{
			new EventData("SELECT_CHAPTER_FROM_OUTER", null)
		};
		GameSection.SetEventData(new object[2]
		{
			GetChapterId(),
			array
		});
	}
}
