using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestRushSelectList : QuestEventSelectList
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
		SPR_FRAME
	}

	private const string RUSH_FRAME_SPRITE = "RequestPlate_Rush";

	protected override bool showMap => false;

	protected override void UpdateTable()
	{
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Expected O, but got Unknown
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		int count = stories.Count;
		if (count > 0)
		{
			num++;
		}
		int num2 = deliveryInfo.Length + clearedDeliveries.Count;
		num2++;
		if (showStory)
		{
			num2 += num + stories.Count;
		}
		if (deliveryInfo == null || num2 == 0)
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
			int completedStartIndex = deliveryInfo.Length + questStartIndex;
			int borderIndex = completedStartIndex + clearedDeliveries.Count;
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
					Transform val = ctrl.GetChild(0);
					val.set_parent(null);
					Object.Destroy(val.get_gameObject());
				}
			}
			SetTable(UI.TBL_DELIVERY_QUEST, string.Empty, num2, false, delegate(int i, Transform parent)
			{
				Transform result = null;
				if (i >= storyStartIndex)
				{
					result = Realizes("QuestEventStoryItem", parent, true);
				}
				else if (i >= borderIndex)
				{
					result = Realizes("QuestEventBorderItem", parent, true);
				}
				else if (i >= questStartIndex)
				{
					result = Realizes("QuestRequestItemRush", parent, true);
				}
				else if (i == 0)
				{
					result = Realizes("QuestRushRequestItemToSearch", parent, true);
				}
				return result;
			}, delegate(int i, Transform t, bool is_recycle)
			{
				SetActive(t, true);
				if (i >= storyStartIndex)
				{
					int index = i - storyStartIndex;
					InitStory(index, t);
				}
				else if (i < borderIndex)
				{
					if (i >= completedStartIndex)
					{
						int completedIndex = i - completedStartIndex;
						InitCompletedDelivery(completedIndex, t);
					}
					else if (i >= questStartIndex)
					{
						InitNormalDelivery(i - questStartIndex, t);
					}
					else if (i == 0)
					{
						InitGoToSearchButton(t);
					}
				}
				if (i < storyStartIndex && i != 0)
				{
					SetSprite(t, UI.SPR_FRAME, "RequestPlate_Rush");
				}
			});
			UIScrollView component = base.GetComponent<UIScrollView>((Enum)UI.SCR_DELIVERY_QUEST);
			component.set_enabled(true);
			RepositionTable();
		}
	}

	protected override void UpdateGrid()
	{
		int num = deliveryInfo.Length + clearedDeliveries.Count;
		if (deliveryInfo == null || num == 0)
		{
			SetActive((Enum)UI.STR_DELIVERY_NON_LIST, true);
			SetActive((Enum)UI.GRD_DELIVERY_QUEST, false);
		}
		else
		{
			SetActive((Enum)UI.STR_DELIVERY_NON_LIST, false);
			SetActive((Enum)UI.GRD_DELIVERY_QUEST, true);
			SetDynamicList((Enum)UI.GRD_DELIVERY_QUEST, "QuestRequestItemRush", num, false, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycle)
			{
				SetActive(t, true);
				DeliveryTable.DeliveryData deliveryData = null;
				bool flag = i >= deliveryInfo.Length;
				if (!flag)
				{
					SetEvent(t, "SELECT_RUSH", i);
					deliveryData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryInfo[i].dId);
				}
				else
				{
					SetEvent(t, "SELECT_COMPLETED_RUSH", i - deliveryInfo.Length);
					deliveryData = clearedDeliveries[i - deliveryInfo.Length];
				}
				SetupDeliveryListItem(t, deliveryData);
				SetActive(t, UI.OBJ_REQUEST_COMPLETED, flag);
				SetSprite(t, UI.SPR_FRAME, "RequestPlate_Rush");
			});
		}
	}

	protected override void InitStory(int index, Transform t)
	{
		SetEvent(t, "SELECT_RUSH_STORY", index);
		SetLabelText(t, UI.LBL_STORY_TITLE, stories[index].title);
	}

	protected override void InitNormalDelivery(int index, Transform t)
	{
		SetEvent(t, "SELECT_RUSH", index);
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryInfo[index].dId);
		SetupDeliveryListItem(t, deliveryTableData);
	}

	protected override void InitCompletedDelivery(int completedIndex, Transform t)
	{
		int num = clearedDeliveries.Count - 1 - completedIndex;
		DeliveryTable.DeliveryData info = clearedDeliveries[num];
		SetEvent(t, "SELECT_COMPLETED_RUSH", num);
		SetupDeliveryListItem(t, info);
		SetActive(t, UI.OBJ_REQUEST_COMPLETED, true);
		SetCompletedHaveCount(t, info);
	}

	private void OnQuery_SELECT_RUSH()
	{
		int num = (int)GameSection.GetEventData();
		bool flag = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery(deliveryInfo[num].dId);
		int delivery_id = deliveryInfo[num].dId;
		if (flag)
		{
			DeliveryTable.DeliveryData table = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryInfo[num].dId);
			changeToDeliveryClearEvent = true;
			bool is_tutorial = !TutorialStep.HasFirstDeliveryCompleted();
			bool enable_clear_event = table.clearEventID != 0;
			GameSection.StayEvent();
			MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
			MonoBehaviourSingleton<DeliveryManager>.I.SendDeliveryComplete(deliveryInfo[num].uId, enable_clear_event, delegate(bool is_success, DeliveryRewardList recv_reward)
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
						GameSection.ChangeStayEvent("RUSH_REWARD", new object[2]
						{
							delivery_id,
							recv_reward
						});
					}
					else
					{
						GameSection.ChangeStayEvent("CLEAR_EVENT", new object[3]
						{
							(int)table.clearEventID,
							delivery_id,
							recv_reward
						});
					}
				}
				else
				{
					changeToDeliveryClearEvent = false;
				}
				GameSection.ResumeEvent(is_success, null);
			});
		}
		else
		{
			GameSection.SetEventData(new object[2]
			{
				delivery_id,
				null
			});
		}
	}

	private void InitGoToSearchButton(Transform t)
	{
		SetEvent(t, "TO_SEARCH", null);
	}

	private void OnQuery_TO_SEARCH()
	{
		MonoBehaviourSingleton<PartyManager>.I.SetNowRushQuestIds(CreateQuestIdList());
	}

	private List<int> CreateQuestIdList()
	{
		List<int> list = new List<int>();
		if (clearedDeliveries != null)
		{
			int i = 0;
			for (int count = clearedDeliveries.Count; i < count; i++)
			{
				list.Add((int)clearedDeliveries[i].needs[0].questId);
			}
		}
		if (deliveryInfo != null)
		{
			int j = 0;
			for (int num = deliveryInfo.Length; j < num; j++)
			{
				DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryInfo[j].dId);
				list.Add((int)deliveryTableData.needs[0].questId);
			}
		}
		return list;
	}

	private void OnQuery_SELECT_COMPLETED_RUSH()
	{
		int index = (int)GameSection.GetEventData();
		DeliveryTable.DeliveryData deliveryData = clearedDeliveries[index];
		int id = (int)deliveryData.id;
		DeliveryRewardList deliveryRewardList = new DeliveryRewardList();
		GameSection.SetEventData(new object[3]
		{
			id,
			deliveryRewardList,
			true
		});
	}

	private void OnQuery_SELECT_RUSH_STORY()
	{
		int index = (int)GameSection.GetEventData();
		Story story = stories[index];
		string name = (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge()) ? "MAIN_MENU_HOME" : "MAIN_MENU_LOUNGE";
		EventData[] array = new EventData[3]
		{
			new EventData(name, null),
			new EventData("EXPLORE", null),
			new EventData("SELECT_EXPLORE", eventData)
		};
		GameSection.SetEventData(new object[4]
		{
			story.id,
			string.Empty,
			string.Empty,
			array
		});
	}
}
