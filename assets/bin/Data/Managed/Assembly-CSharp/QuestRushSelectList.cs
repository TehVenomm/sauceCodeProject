using Network;
using System;
using System.Collections;
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
		SPR_FRAME,
		SPR_TYPE_DIFFICULTY,
		LBL_POINT_TITLE,
		LBL_CURRENT_POINT,
		OBJ_NEXT_REWARD_ROOT,
		LBL_NEXT_REWARD_NAME,
		LBL_NEXT_POINT,
		GRD_NEXT_ICON,
		OBJ_NEXT_REWARD_ICON_POS1,
		OBJ_NEXT_REWARD_ICON_POS2,
		OBJ_CURRENT_STATUS,
		SPR_NORMAL_INFO,
		SPR_CARNIVAL_INFO
	}

	private const string RUSH_FRAME_SPRITE = "RequestPlate_Rush";

	private QuestRushPointModel.Param currentData;

	protected override bool showMap => false;

	protected override IEnumerator DoInitialize()
	{
		bool isCarnival = MonoBehaviourSingleton<DeliveryManager>.I.IsCarnivalEvent(eventData.eventId);
		if (!isCarnival)
		{
			yield return (object)this.StartCoroutine(GetCurrentStatus());
		}
		SetActive((Enum)UI.OBJ_CURRENT_STATUS, !isCarnival);
		SetActive((Enum)UI.OBJ_NEXT_REWARD_ROOT, !isCarnival);
		SetActive((Enum)UI.SPR_NORMAL_INFO, !isCarnival);
		SetActive((Enum)UI.SPR_CARNIVAL_INFO, isCarnival);
		yield return (object)this.StartCoroutine(base.DoInitialize());
	}

	protected unsafe override void UpdateTable()
	{
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Expected O, but got Unknown
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		if (currentData != null)
		{
			SetLabelText((Enum)UI.LBL_CURRENT_POINT, StringTable.Format(STRING_CATEGORY.RUSH, 0u, currentData.point));
		}
		SetLabelText((Enum)UI.LBL_POINT_TITLE, StringTable.Get(STRING_CATEGORY.RUSH, 1u));
		if (currentData != null && currentData.reward != null && currentData.reward.reward.Count > 0)
		{
			SetActive((Enum)UI.OBJ_NEXT_REWARD_ROOT, true);
			SetLabelText((Enum)UI.LBL_NEXT_POINT, StringTable.Format(STRING_CATEGORY.RUSH, 0u, currentData.reward.point));
			int num = Mathf.Min(2, currentData.reward.reward.Count);
			string text = string.Empty;
			for (int i = 0; i < num; i++)
			{
				QuestRushPointModel.Param.Reward reward = currentData.reward.reward[i];
				Transform ctrl;
				if (i == 0)
				{
					ctrl = GetCtrl(UI.OBJ_NEXT_REWARD_ICON_POS1);
					text = Utility.GetRewardName((REWARD_TYPE)reward.type, (uint)reward.itemId);
				}
				else
				{
					ctrl = GetCtrl(UI.OBJ_NEXT_REWARD_ICON_POS2);
					text = text + "\n" + Utility.GetRewardName((REWARD_TYPE)reward.type, (uint)reward.itemId);
				}
				ItemIcon.CreateRewardItemIcon((REWARD_TYPE)reward.type, (uint)reward.itemId, ctrl, reward.num, null, 0, false, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT);
			}
			if (currentData.reward.reward.Count == 1)
			{
				SetActive((Enum)UI.OBJ_NEXT_REWARD_ICON_POS2, false);
			}
			SetLabelText((Enum)UI.LBL_NEXT_REWARD_NAME, text);
			GetCtrl(UI.GRD_NEXT_ICON).GetComponent<UIGrid>().Reposition();
		}
		else
		{
			SetActive((Enum)UI.OBJ_NEXT_REWARD_ROOT, false);
		}
		int num2 = 0;
		int count = stories.Count;
		if (count > 0)
		{
			num2++;
		}
		int num3 = deliveryInfo.Length + clearedDeliveries.Count;
		num3++;
		if (showStory)
		{
			num3 += num2 + stories.Count;
		}
		if (deliveryInfo == null || num3 == 0)
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
			Transform ctrl2 = GetCtrl(UI.TBL_DELIVERY_QUEST);
			if (Object.op_Implicit(ctrl2))
			{
				int j = 0;
				for (int childCount = ctrl2.get_childCount(); j < childCount; j++)
				{
					Transform val = ctrl2.GetChild(0);
					val.set_parent(null);
					Object.Destroy(val.get_gameObject());
				}
			}
			bool isRenewalFlag = MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.isTheaterRenewal;
			_003CUpdateTable_003Ec__AnonStorey41F _003CUpdateTable_003Ec__AnonStorey41F;
			SetTable(UI.TBL_DELIVERY_QUEST, string.Empty, num3, false, new Func<int, Transform, Transform>((object)_003CUpdateTable_003Ec__AnonStorey41F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), new Action<int, Transform, bool>((object)_003CUpdateTable_003Ec__AnonStorey41F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			UIScrollView component = base.GetComponent<UIScrollView>((Enum)UI.SCR_DELIVERY_QUEST);
			component.set_enabled(true);
			RepositionTable();
		}
	}

	protected unsafe override void UpdateGrid()
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
			SetDynamicList((Enum)UI.GRD_DELIVERY_QUEST, "QuestRequestItemRush", num, false, null, null, new Action<int, Transform, bool>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
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
		SetEvent(t, "SELECT_RUSH", index);
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryInfo[index].dId);
		SetupDeliveryListItem(t, deliveryTableData);
		SetDifficultySprite(t, deliveryTableData);
	}

	protected override void InitCompletedDelivery(int completedIndex, Transform t)
	{
		int num = clearedDeliveries.Count - 1 - completedIndex;
		DeliveryTable.DeliveryData deliveryData = clearedDeliveries[num];
		SetEvent(t, "SELECT_COMPLETED_RUSH", num);
		SetupDeliveryListItem(t, deliveryData);
		SetActive(t, UI.OBJ_REQUEST_COMPLETED, true);
		SetDifficultySprite(t, deliveryData);
		SetCompletedHaveCount(t, deliveryData);
	}

	private unsafe void OnQuery_SELECT_RUSH()
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
			_003COnQuery_SELECT_RUSH_003Ec__AnonStorey420 _003COnQuery_SELECT_RUSH_003Ec__AnonStorey;
			MonoBehaviourSingleton<DeliveryManager>.I.SendDeliveryComplete(deliveryInfo[num].uId, enable_clear_event, new Action<bool, DeliveryRewardList>((object)_003COnQuery_SELECT_RUSH_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
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

	private void SetDifficultySprite(Transform t, DeliveryTable.DeliveryData dd)
	{
		SetActive(t, UI.SPR_TYPE_DIFFICULTY, dd.difficulty >= DIFFICULTY_MODE.HARD);
	}

	private IEnumerator GetCurrentStatus()
	{
		bool isRequest = true;
		Protocol.Send<QuestRushPointModel.RequestSendForm, QuestRushPointModel>(postData: new QuestRushPointModel.RequestSendForm
		{
			eid = eventData.eventId
		}, url: QuestRushPointModel.URL, callBack: (Action<QuestRushPointModel>)delegate(QuestRushPointModel result)
		{
			((_003CGetCurrentStatus_003Ec__Iterator129)/*Error near IL_0059: stateMachine*/)._003CisRequest_003E__0 = false;
			((_003CGetCurrentStatus_003Ec__Iterator129)/*Error near IL_0059: stateMachine*/)._003C_003Ef__this.currentData = result.result;
		}, getParam: string.Empty);
		while (isRequest)
		{
			yield return (object)null;
		}
	}

	protected override void OnQuery_INFO()
	{
		string arg = base.eventData.linkName + "_REWARD";
		string eventData = string.Format(WebViewManager.NewsWithLinkParamFormat, arg);
		GameSection.SetEventData(eventData);
	}

	private void OnQuery_HOW_TO()
	{
		GameSection.SetEventData(WebViewManager.Rush);
	}
}
