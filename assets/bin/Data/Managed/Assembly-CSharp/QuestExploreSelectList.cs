using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestExploreSelectList : QuestEventSelectList
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
		SPR_TYPE_DIFFICULTY,
		BTN_INFO_NEW,
		OBJ_CURRENT_STATUS
	}

	private class ExploreSort : IComparer<DeliveryTable.DeliveryData>
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
			if (x.type != y.type)
			{
				if (x.type == DELIVERY_TYPE.EVENT)
				{
					return -1;
				}
				return 1;
			}
			if (x.type == DELIVERY_TYPE.SUB_EVENT && y.type == DELIVERY_TYPE.SUB_EVENT)
			{
				bool flag3 = MonoBehaviourSingleton<DeliveryManager>.I.IsClearDelivery(x.id);
				bool flag4 = MonoBehaviourSingleton<DeliveryManager>.I.IsClearDelivery(y.id);
				if (flag3 != flag4)
				{
					if (flag3)
					{
						return 1;
					}
					return -1;
				}
			}
			return (int)(x.id - y.id);
		}
	}

	private QuestExplorePointModel.Param currentData;

	private const string EXPLORE_FRAME_SPRITE = "RequestPlate_Explore";

	private List<DeliveryTable.DeliveryData> deliveryList = new List<DeliveryTable.DeliveryData>();

	private List<DeliveryTable.DeliveryData> allDeliveryList = new List<DeliveryTable.DeliveryData>();

	protected override bool showMap => false;

	protected override IEnumerator DoInitialize()
	{
		SetLabelText((Enum)UI.LBL_HOST_LIMIT, eventData.hostCountLimit.ToString());
		string remainTime = QuestSpecialSelect.GetRemainTimeText(GetRemainTime());
		SetLabelText((Enum)UI.LBL_HOST_RESET_TIME, StringTable.Format(STRING_CATEGORY.EXPLORE, 2u, remainTime));
		yield return this.StartCoroutine(GetCurrentStatus());
		yield return this.StartCoroutine(base.DoInitialize());
	}

	public override void UpdateUI()
	{
		SetLabelText((Enum)UI.LBL_LOCATION_NAME, eventData.name);
		SetLabelText((Enum)UI.LBL_LOCATION_NAME_EFFECT, eventData.name);
		if (eventData.eventId == 99001001)
		{
			SetActive((Enum)UI.BTN_INFO, is_visible: false);
			SetActive(GetCtrl(UI.OBJ_IMAGE), UI.BTN_INFO_NEW, is_visible: true);
			SetActive((Enum)UI.OBJ_CURRENT_STATUS, is_visible: false);
		}
		else
		{
			SetActive((Enum)UI.BTN_INFO, !string.IsNullOrEmpty(eventData.linkName));
			SetActive(GetCtrl(UI.OBJ_IMAGE), UI.BTN_INFO_NEW, is_visible: false);
			SetActive((Enum)UI.OBJ_CURRENT_STATUS, is_visible: true);
		}
		title.Update();
		titleEffect.Update();
		stories.Clear();
		if (eventData.prologueStoryId > 0)
		{
			stories.Add(new Story(eventData.prologueStoryId, eventData.prologueTitle));
		}
		clearedDeliveries = CreateClearedDliveryList();
		UpdateList();
		UpdateAnchors();
		isResetUI = false;
	}

	protected override void UpdateTable()
	{
		SetLabelText((Enum)UI.LBL_CURRENT_POINT, StringTable.Format(STRING_CATEGORY.EXPLORE, 0u, currentData.point));
		SetLabelText((Enum)UI.LBL_POINT_TITLE, StringTable.Get(STRING_CATEGORY.EXPLORE, 1u));
		if (currentData.reward != null && currentData.reward.reward.Count > 0)
		{
			SetActive((Enum)UI.OBJ_NEXT_REWARD_ROOT, is_visible: true);
			QuestExplorePointModel.Param.Reward reward = currentData.reward.reward[0];
			ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon((REWARD_TYPE)reward.type, (uint)reward.itemId, GetCtrl(UI.OBJ_NEXT_REWARD_ICON_POS), reward.num);
			itemIcon.GetComponent<BoxCollider>().set_enabled(false);
			string rewardName = Utility.GetRewardName((REWARD_TYPE)reward.type, (uint)reward.itemId);
			rewardName = Utility.TrimText(rewardName, GetCtrl(UI.LBL_NEXT_REWARD_NAME).GetComponent<UILabel>());
			SetLabelText((Enum)UI.LBL_NEXT_POINT, StringTable.Format(STRING_CATEGORY.EXPLORE, 0u, currentData.reward.point));
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
		SortDeliveryList();
		int questStartIndex = 0;
		questStartIndex++;
		int borderIndex = questStartIndex + deliveryInfo.Length + clearedDeliveries.Count;
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
			Transform val = null;
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
				int index2 = i - questStartIndex;
				if (allDeliveryList[index2].type == DELIVERY_TYPE.SUB_EVENT)
				{
					return Realizes("QuestRequestItem", parent);
				}
				return Realizes("QuestRequestItemExplore", parent);
			}
			if (i == 0)
			{
				return Realizes("QuestExploreRequestItemToSearch", parent);
			}
			return Realizes("QuestEventBorderItem", parent);
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
						int index = i - questStartIndex;
						DeliveryTable.DeliveryData item = allDeliveryList[index];
						if (clearedDeliveries.Contains(item))
						{
							InitCompletedDelivery(clearedDeliveries.IndexOf(item), t);
						}
						else
						{
							InitNormalDelivery(deliveryList.IndexOf(item), t);
						}
					}
					else if (i == 0)
					{
						InitGoToSearchButton(t);
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
		SetEvent(t, "SELECT_RUSH_STORY", index);
		SetLabelText(t, UI.LBL_STORY_TITLE, stories[index].title);
	}

	protected override void InitNormalDelivery(int index, Transform t)
	{
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryInfo[index].dId);
		SetupDeliveryListItem(t, deliveryTableData);
		SetDifficultySprite(t, deliveryTableData);
		if (deliveryTableData.type == DELIVERY_TYPE.SUB_EVENT)
		{
			SetEvent(t, "SELECT_DELIVERY", index);
			return;
		}
		SetEvent(t, "SELECT_EXPLORE", index);
		SetSprite(t, UI.SPR_FRAME, "RequestPlate_Explore");
	}

	protected override void InitCompletedDelivery(int completedIndex, Transform t)
	{
		DeliveryTable.DeliveryData deliveryData = clearedDeliveries[completedIndex];
		if (deliveryData.type == DELIVERY_TYPE.SUB_EVENT)
		{
			base.InitCompletedDelivery(completedIndex, t);
			return;
		}
		SetEvent(t, "SELECT_COMPLETED_EXPLORE", completedIndex);
		SetupDeliveryListItem(t, deliveryData);
		SetDifficultySprite(t, deliveryData);
		SetActive(t, UI.OBJ_REQUEST_COMPLETED, is_visible: true);
		SetCompletedHaveCount(t, deliveryData);
		SetSprite(t, UI.SPR_FRAME, "RequestPlate_Explore");
	}

	private void OnQuery_SELECT_EXPLORE()
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
						GameSection.ChangeStayEvent("EXPLORE_REWARD", new object[2]
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
			int num2 = (from x in MonoBehaviourSingleton<InventoryManager>.I.abilityItemInventory.GetAll()
			where x.equipUniqueId == 0
			select x).Count();
			if (num2 >= MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxAbilityItem)
			{
				GameSection.ChangeEvent("LIMIT_ABILITY_ITEM");
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
	}

	private void OnQuery_SELECT_COMPLETED_EXPLORE()
	{
		int num = (from x in MonoBehaviourSingleton<InventoryManager>.I.abilityItemInventory.GetAll()
		where x.equipUniqueId == 0
		select x).Count();
		if (num >= MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxAbilityItem)
		{
			GameSection.ChangeEvent("LIMIT_ABILITY_ITEM");
			return;
		}
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

	private void OnQuery_SELECT_EXPLORE_STORY()
	{
		int index = (int)GameSection.GetEventData();
		Story story = stories[index];
		string goingHomeEvent = GameSection.GetGoingHomeEvent();
		EventData[] array = new EventData[3]
		{
			new EventData(goingHomeEvent, null),
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

	public TimeSpan GetRemainTime()
	{
		DateTime now = TimeManager.GetNow();
		TimeSpan value = TimeSpan.Parse(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.EXPLORE_HOST_LIMIT_RESET_TIME_1);
		DateTime dateTime = now.Date.Add(value);
		if (dateTime > now)
		{
			return dateTime - now;
		}
		value = TimeSpan.Parse(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.EXPLORE_HOST_LIMIT_RESET_TIME_2);
		dateTime = now.Date.Add(value);
		if (dateTime > now)
		{
			return dateTime - now;
		}
		value = TimeSpan.Parse(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.EXPLORE_HOST_LIMIT_RESET_TIME_3);
		dateTime = now.Date.Add(value);
		if (dateTime > now)
		{
			return dateTime - now;
		}
		value = TimeSpan.Parse(MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.EXPLORE_HOST_LIMIT_RESET_TIME_1);
		dateTime = now.Date.Add(value).Add(TimeSpan.FromDays(1.0));
		if (dateTime > now)
		{
			return dateTime - now;
		}
		Log.Error("ホスト回復時間がおかしいようです");
		return TimeSpan.FromDays(1.0);
	}

	private IEnumerator GetCurrentStatus()
	{
		bool isRequest = true;
		Protocol.Send<QuestExplorePointModel.RequestSendForm, QuestExplorePointModel>(postData: new QuestExplorePointModel.RequestSendForm
		{
			eid = eventData.eventId
		}, url: QuestExplorePointModel.URL, callBack: (Action<QuestExplorePointModel>)delegate(QuestExplorePointModel result)
		{
			isRequest = false;
			currentData = result.result;
		}, getParam: string.Empty);
		while (isRequest)
		{
			yield return null;
		}
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
		GameSection.SetEventData(WebViewManager.Explore);
	}

	private void SetDifficultySprite(Transform t, DeliveryTable.DeliveryData dd)
	{
		SetActive(t, UI.SPR_TYPE_DIFFICULTY, (dd != null && dd.difficulty >= DIFFICULTY_MODE.HARD) ? true : false);
	}

	private void SortDeliveryList()
	{
		deliveryList.Clear();
		allDeliveryList.Clear();
		int i = 0;
		for (int num = deliveryInfo.Length; i < num; i++)
		{
			DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryInfo[i].dId);
			if (deliveryTableData != null)
			{
				deliveryList.Add(deliveryTableData);
			}
		}
		allDeliveryList.AddRange(deliveryList);
		allDeliveryList.AddRange(clearedDeliveries);
		allDeliveryList.Sort(new ExploreSort());
	}
}
