using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSeriesArenaSelectList : QuestSeriesArenaEventList
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
		SPR_FRAME
	}

	public class SeriesArenaSort : IComparer<ShowDeliveryData>
	{
		public int Compare(ShowDeliveryData x, ShowDeliveryData y)
		{
			bool flag = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int)x.data.id);
			bool flag2 = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int)y.data.id);
			if (flag != flag2)
			{
				if (flag)
				{
					return -1;
				}
				return 1;
			}
			return x.data.displayOrder - y.data.displayOrder;
		}
	}

	private const string ARENA_FRAME_SPRITE = "RequestPlate_hero";

	private int eventId;

	private List<ShowDeliveryData> deliveryList = new List<ShowDeliveryData>();

	public override void Initialize()
	{
		eventId = (int)GameSection.GetEventData();
		base.Initialize();
	}

	protected override IEnumerator DoInitialize()
	{
		eventData = MonoBehaviourSingleton<QuestManager>.I._GetEventData(eventId);
		seriesArenaTopData = MonoBehaviourSingleton<DeliveryManager>.I.FindSeriesArenaTopData();
		yield return this.StartCoroutine(LoadSeriesArenaTopBanner());
		GetDeliveryList();
		EndInitialize();
	}

	protected override void OnQuery_TO_UNIQUE_STATUS()
	{
	}

	protected override void UpdateTable()
	{
		deliveryList = new List<ShowDeliveryData>();
		if (deliveryInfo != null)
		{
			for (int j = 0; j < deliveryInfo.Length; j++)
			{
				ShowDeliveryData item = new ShowDeliveryData(j, isComp: false, deliveryInfo[j]);
				deliveryList.Add(item);
			}
		}
		if (clearedDeliveries != null)
		{
			for (int k = 0; k < clearedDeliveries.Count; k++)
			{
				ShowDeliveryData item2 = new ShowDeliveryData(k, isComp: true, clearedDeliveries[k]);
				deliveryList.Add(item2);
			}
		}
		deliveryList.Sort(new SeriesArenaSort());
		int num = 0;
		int count = stories.Count;
		if (count > 0)
		{
			num++;
		}
		int num2 = deliveryList.Count;
		if (seriesArenaTopData.enableRanking)
		{
			num2++;
		}
		int questStartIndex = 0;
		if (seriesArenaTopData.enableRanking)
		{
			questStartIndex++;
		}
		Transform ctrl = GetCtrl(UI.TBL_DELIVERY_QUEST);
		if (Object.op_Implicit(ctrl))
		{
			int l = 0;
			for (int childCount = ctrl.get_childCount(); l < childCount; l++)
			{
				Transform child = ctrl.GetChild(0);
				child.set_parent(null);
				Object.Destroy(child.get_gameObject());
			}
		}
		bool flag = MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.isTheaterRenewal;
		SetTable(UI.TBL_DELIVERY_QUEST, string.Empty, num2, reset: false, delegate(int i, Transform parent)
		{
			Transform result = null;
			if (i >= questStartIndex)
			{
				result = Realizes("QuestRequestItemSeriesArena", parent);
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
				if (i >= questStartIndex)
				{
					InitNormalDelivery(i - questStartIndex, t);
				}
				else if (i == 0)
				{
					InitGoToRankingButton(t);
				}
			}
		});
		UIScrollView component = base.GetComponent<UIScrollView>((Enum)UI.SCR_DELIVERY_QUEST);
		component.set_enabled(true);
		RepositionTable();
	}

	protected override void InitNormalDelivery(int index, Transform t)
	{
		ShowDeliveryData showDeliveryData = deliveryList[index];
		SetEvent(t, "SELECT_SERIES_ARENA", index);
		SetUpSeriesArenaListItem(t, showDeliveryData.data);
		if (showDeliveryData.isCompleted)
		{
			SetActive(t, UI.OBJ_REQUEST_COMPLETED, is_visible: true);
		}
		SetSprite(t, UI.SPR_FRAME, "RequestPlate_hero");
	}

	private void SetUpSeriesArenaListItem(Transform t, DeliveryTable.DeliveryData info)
	{
		QuestRequestItemSeriesArena questRequestItemSeriesArena = t.GetComponent<QuestRequestItemSeriesArena>();
		if (questRequestItemSeriesArena == null)
		{
			questRequestItemSeriesArena = t.get_gameObject().AddComponent<QuestRequestItemSeriesArena>();
		}
		questRequestItemSeriesArena.InitUI();
		questRequestItemSeriesArena.Setup(t, info);
	}

	private void OnQuery_SECTION_BACK()
	{
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentSeriesArenaId(0);
	}

	private void OnQuery_SELECT_SERIES_ARENA()
	{
		int index = (int)GameSection.GetEventData();
		DeliveryTable.DeliveryData data = deliveryList[index].data;
		bool flag = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery((int)data.id);
		Delivery notClearDelivery = GetNotClearDelivery(data.id);
		if (flag)
		{
			changeToDeliveryClearEvent = true;
			bool is_tutorial = !TutorialStep.HasFirstDeliveryCompleted();
			bool enable_clear_event = data.clearEventID != 0;
			GameSection.StayEvent();
			MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
			MonoBehaviourSingleton<DeliveryManager>.I.SendDeliveryComplete(notClearDelivery.uId, enable_clear_event, delegate(bool is_success, DeliveryRewardList recv_reward)
			{
				if (is_success)
				{
					if (is_tutorial)
					{
						TutorialStep.isSendFirstRewardComplete = true;
					}
					if (!enable_clear_event)
					{
						MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
						GameSection.ChangeStayEvent("SERIES_ARENA_REWARD", new object[2]
						{
							(int)data.id,
							recv_reward
						});
					}
					else
					{
						GameSection.ChangeStayEvent("CLEAR_EVENT", new object[3]
						{
							(int)data.clearEventID,
							(int)data.id,
							recv_reward
						});
					}
				}
				else
				{
					changeToDeliveryClearEvent = false;
				}
				MonoBehaviourSingleton<QuestManager>.I.SendGetEventList(delegate
				{
					MonoBehaviourSingleton<DeliveryManager>.I.SendEventList(delegate
					{
						GameSection.ResumeEvent(is_success);
					});
				});
			});
		}
		else
		{
			MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(data.GetQuestData().questID);
			GameSection.ChangeEvent("TO_ROOM", data);
		}
	}

	private Delivery GetNotClearDelivery(uint deliveryId)
	{
		return deliveryInfo.Find((Delivery d) => d.dId == deliveryId);
	}

	protected override void InitCompletedDelivery(int completedIndex, Transform t)
	{
	}
}
