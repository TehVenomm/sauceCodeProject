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
		int regionId = (int)GameSection.GetEventData();
		regionData = Singleton<RegionTable>.I.GetData((uint)regionId);
		bool needSummary = regionId != 0;
		SetActive((Enum)UI.BTN_SUMMARY, needSummary);
		LoadingQueue loadQueue = new LoadingQueue(this);
		string bannerName = ResourceName.GetAreaBG(regionId);
		LoadObject bannerObj = loadQueue.Load(RESOURCE_CATEGORY.AREA_BACKGROUND, bannerName, false);
		if (loadQueue.IsLoading())
		{
			yield return (object)loadQueue.Wait();
		}
		Texture2D bannerTex = bannerObj.loadedObject as Texture2D;
		if (bannerTex != null)
		{
			SetTexture((Enum)UI.TEX_AREA_BG, bannerTex);
		}
		if (ShouldShowEventMapButton())
		{
			LoadObject item = loadQueue.Load(RESOURCE_CATEGORY.QUEST_ITEM, "QEM_10000001", false);
			if (loadQueue.IsLoading())
			{
				yield return (object)loadQueue.Wait();
			}
			SetTexture(texture: item.loadedObject as Texture2D, texture_enum: UI.TEX_EVENT_BG);
			if (item != null && null != item.loadedObject)
			{
				GameObject mapItemObj = item.loadedObject as GameObject;
				mapItem = mapItemObj.get_transform();
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
		SetLabelText((Enum)UI.LBL_LOCATION_NAME, regionData.regionName);
		SetLabelText((Enum)UI.LBL_LOCATION_NAME_EFFECT, regionData.regionName);
	}

	protected unsafe override void UpdateTable()
	{
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Expected O, but got Unknown
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		int count = stories.Count;
		if (count > 0)
		{
			num++;
		}
		List<ShowDeliveryData> list = new List<ShowDeliveryData>();
		if (deliveryInfo != null)
		{
			for (int i = 0; i < deliveryInfo.Length; i++)
			{
				ShowDeliveryData item = new ShowDeliveryData(i, false, deliveryInfo[i]);
				list.Add(item);
			}
		}
		if (clearedDeliveries != null)
		{
			for (int j = 0; j < clearedDeliveries.Count; j++)
			{
				ShowDeliveryData item2 = new ShowDeliveryData(j, true, clearedDeliveries[j]);
				list.Add(item2);
			}
		}
		pageMax = 1 + (list.Count - 1) / 10;
		bool flag = pageMax > 1;
		SetActive((Enum)UI.OBJ_ACTIVE_ROOT, flag);
		SetActive((Enum)UI.OBJ_INACTIVE_ROOT, !flag);
		SetLabelText((Enum)UI.LBL_MAX, pageMax.ToString());
		SetLabelText((Enum)UI.LBL_NOW, nowPage.ToString());
		ShowDeliveryData[] showList = GetPagingList(list.ToArray(), 10, nowPage);
		int num2 = showList.Length;
		if (showStory)
		{
			num2 += num + stories.Count;
		}
		SetActive((Enum)UI.TBL_DELIVERY_QUEST, true);
		bool flag2 = false;
		if (ShouldShowEventMapButton())
		{
			flag2 = true;
			num2++;
		}
		int questStartIndex = 0;
		if (flag2)
		{
			questStartIndex++;
		}
		int borderIndex = questStartIndex + showList.Length;
		int storyStartIndex = borderIndex;
		if (stories.Count > 0)
		{
			storyStartIndex++;
		}
		Transform ctrl = GetCtrl(UI.TBL_DELIVERY_QUEST);
		int k = 0;
		for (int childCount = ctrl.get_childCount(); k < childCount; k++)
		{
			Transform val = ctrl.GetChild(0);
			val.set_parent(null);
			Object.Destroy(val.get_gameObject());
		}
		bool isRenewalFlag = MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.isTheaterRenewal;
		_003CUpdateTable_003Ec__AnonStorey3A9 _003CUpdateTable_003Ec__AnonStorey3A;
		SetTable(UI.TBL_DELIVERY_QUEST, string.Empty, num2, isResetUI, new Func<int, Transform, Transform>((object)_003CUpdateTable_003Ec__AnonStorey3A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), new Action<int, Transform, bool>((object)_003CUpdateTable_003Ec__AnonStorey3A, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		UIScrollView component = base.GetComponent<UIScrollView>((Enum)UI.SCR_DELIVERY_QUEST);
		component.set_enabled(true);
		RepositionTable();
	}

	protected override void InitMap(Transform t)
	{
		SetEvent(t, "WORLDMAP", regionData.regionId);
		SetLabelText(t, UI.LBL_STORY_TITLE, "マップ");
	}

	protected override void GetDeliveryList()
	{
		deliveryInfo = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList(true);
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
				NPCTable.NPCData nPCData = Singleton<NPCTable>.I.GetNPCData((int)deliveryTableData.npcID);
				if (nPCData == null)
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

	private new List<DeliveryTable.DeliveryData> CreateClearedDliveryList()
	{
		List<DeliveryTable.DeliveryData> list = new List<DeliveryTable.DeliveryData>();
		List<ClearStatusDelivery> clearStatusDelivery = MonoBehaviourSingleton<DeliveryManager>.I.clearStatusDelivery;
		int groupId = regionData.groupId;
		uint regionId = regionData.regionId;
		int i = 0;
		for (int count = clearStatusDelivery.Count; i < count; i++)
		{
			ClearStatusDelivery d = clearStatusDelivery[i];
			if (d.deliveryStatus == 3)
			{
				DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)d.deliveryId);
				if ((deliveryTableData.type == DELIVERY_TYPE.STORY || deliveryTableData.type == DELIVERY_TYPE.ONCE) && (deliveryTableData.regionId == regionId || (groupId > 0 && deliveryTableData.regionId == groupId)) && !Array.Exists(deliveryInfo, (Delivery x) => x.dId == d.deliveryId))
				{
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
		EventData[] array = new EventData[3]
		{
			new EventData("MAIN_MENU_HOME", null),
			new EventData("TO_QUEST", null),
			new EventData("SELECT_AREA", (int)regionData.regionId)
		};
		GameSection.SetEventData(new object[4]
		{
			story.id,
			string.Empty,
			string.Empty,
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
