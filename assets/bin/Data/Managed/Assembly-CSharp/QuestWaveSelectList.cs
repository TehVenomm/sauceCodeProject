using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestWaveSelectList : QuestEventSelectList
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
		LBL_HOST_LIMIT,
		LBL_HOST_RESET_TIME,
		LBL_POINT_TITLE,
		LBL_CURRENT_POINT,
		OBJ_NEXT_REWARD_ROOT,
		LBL_NEXT_REWARD_NAME,
		LBL_NEXT_POINT,
		OBJ_NEXT_REWARD_ICON_POS,
		LBL_WAVE_TITLE,
		SPR_TYPE_DIFFICULTY,
		OBJ_CURRENT_STATUS,
		SPR_NORMAL_INFO,
		SPR_CARNIVAL_INFO
	}

	private const string FRAME_SPRITE = "RequestPlateBase";

	private QuestPointRewardModel.Param currentData;

	protected override bool showMap => false;

	protected override IEnumerator DoInitialize()
	{
		bool isCarnival = MonoBehaviourSingleton<DeliveryManager>.I.IsCarnivalEvent(eventData.eventId);
		if (!isCarnival)
		{
			yield return this.StartCoroutine(GetCurrentStatus());
		}
		SetActive((Enum)UI.OBJ_CURRENT_STATUS, !isCarnival);
		SetActive((Enum)UI.OBJ_NEXT_REWARD_ROOT, !isCarnival);
		SetActive((Enum)UI.SPR_NORMAL_INFO, !isCarnival);
		SetActive((Enum)UI.SPR_CARNIVAL_INFO, isCarnival);
		SetActive((Enum)UI.LBL_WAVE_TITLE, !isCarnival);
		yield return this.StartCoroutine(base.DoInitialize());
	}

	private IEnumerator GetCurrentStatus()
	{
		bool isRequest = true;
		Protocol.Send<QuestPointRewardModel.RequestSendForm, QuestPointRewardModel>(postData: new QuestPointRewardModel.RequestSendForm
		{
			eid = eventData.eventId
		}, url: QuestPointRewardModel.URL, callBack: (Action<QuestPointRewardModel>)delegate(QuestPointRewardModel result)
		{
			isRequest = false;
			currentData = result.result;
		}, getParam: string.Empty);
		while (isRequest)
		{
			yield return null;
		}
	}

	protected override void UpdateTable()
	{
		SetLabelText((Enum)UI.LBL_WAVE_TITLE, eventData.name);
		if (currentData != null)
		{
			SetLabelText((Enum)UI.LBL_CURRENT_POINT, StringTable.Format(STRING_CATEGORY.WAVE_MATCH, 0u, currentData.point));
		}
		SetLabelText((Enum)UI.LBL_POINT_TITLE, StringTable.Get(STRING_CATEGORY.WAVE_MATCH, 1u));
		if (currentData != null && currentData.reward != null && currentData.reward.reward.Count > 0)
		{
			SetActive((Enum)UI.OBJ_NEXT_REWARD_ROOT, is_visible: true);
			QuestPointRewardModel.Param.Reward reward = currentData.reward.reward[0];
			ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon((REWARD_TYPE)reward.type, (uint)reward.itemId, GetCtrl(UI.OBJ_NEXT_REWARD_ICON_POS), reward.num);
			string rewardName = Utility.GetRewardName((REWARD_TYPE)reward.type, (uint)reward.itemId);
			SetLabelText((Enum)UI.LBL_NEXT_POINT, StringTable.Format(STRING_CATEGORY.WAVE_MATCH, 0u, currentData.reward.point));
			SetLabelText((Enum)UI.LBL_NEXT_REWARD_NAME, rewardName);
		}
		else
		{
			SetActive((Enum)UI.OBJ_NEXT_REWARD_ROOT, is_visible: false);
		}
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
			SetActive((Enum)UI.STR_DELIVERY_NON_LIST, is_visible: true);
			SetActive((Enum)UI.GRD_DELIVERY_QUEST, is_visible: false);
			SetActive((Enum)UI.TBL_DELIVERY_QUEST, is_visible: false);
			return;
		}
		SetActive((Enum)UI.STR_DELIVERY_NON_LIST, is_visible: false);
		SetActive((Enum)UI.GRD_DELIVERY_QUEST, is_visible: false);
		SetActive((Enum)UI.TBL_DELIVERY_QUEST, is_visible: true);
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
				result = Realizes("QuestRequestItemWave", parent);
			}
			else if (i == 0)
			{
				result = Realizes("QuestWaveRequestItemToSearch", parent);
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
					SetSprite(t, UI.SPR_FRAME, "RequestPlateBase");
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
		SetEvent(t, "SELECT_WAVE_STORY", index);
		SetLabelText(t, UI.LBL_STORY_TITLE, stories[index].title);
	}

	private void OnQuery_SELECT_WAVE_STORY()
	{
		int index = (int)GameSection.GetEventData();
		Story story = stories[index];
		string goingHomeEvent = GameSection.GetGoingHomeEvent();
		EventData[] array = new EventData[3]
		{
			new EventData(goingHomeEvent, null),
			new EventData("TO_EVENT", null),
			new EventData("SELECT_WAVE", eventData)
		};
		GameSection.SetEventData(new object[4]
		{
			story.id,
			string.Empty,
			string.Empty,
			array
		});
	}

	protected override void InitNormalDelivery(int index, Transform t)
	{
		SetEvent(t, "SELECT_WAVE", index);
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryInfo[index].dId);
		SetupDeliveryListItem(t, deliveryTableData);
		SetDifficultySprite(t, deliveryTableData);
	}

	private void OnQuery_SELECT_WAVE()
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
					List<FieldMapTable.PortalTableData> deliveryRelationPortalData = Singleton<FieldMapTable>.I.GetDeliveryRelationPortalData((uint)delivery_id);
					for (int i = 0; i < deliveryRelationPortalData.Count; i++)
					{
						GameSaveData.instance.newReleasePortals.Add(deliveryRelationPortalData[i].portalID);
					}
					if (is_tutorial)
					{
						TutorialStep.isSendFirstRewardComplete = true;
					}
					if (!enable_clear_event)
					{
						MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
						GameSection.ChangeStayEvent("WAVE_REWARD", new object[2]
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
				GameSection.ResumeEvent(is_success);
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

	protected override void InitCompletedDelivery(int completedIndex, Transform t)
	{
		DeliveryTable.DeliveryData deliveryData = clearedDeliveries[completedIndex];
		SetEvent(t, "SELECT_COMPLETED_WAVE", completedIndex);
		SetupDeliveryListItem(t, deliveryData);
		SetDifficultySprite(t, deliveryData);
		SetActive(t, UI.OBJ_REQUEST_COMPLETED, is_visible: true);
		SetCompletedHaveCount(t, deliveryData);
	}

	private void OnQuery_SELECT_COMPLETED_WAVE()
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

	private void InitGoToSearchButton(Transform t)
	{
		SetEvent(t, "TO_SEARCH", null);
	}

	private void OnQuery_TO_SEARCH()
	{
		GameSection.SetEventData(eventData.eventId);
	}

	private void OnQuery_HOW_TO()
	{
		GameSection.SetEventData(WebViewManager.Wave);
	}

	private void SetDifficultySprite(Transform t, DeliveryTable.DeliveryData dd)
	{
		SetActive(t, UI.SPR_TYPE_DIFFICULTY, dd.difficulty >= DIFFICULTY_MODE.HARD);
	}
}
