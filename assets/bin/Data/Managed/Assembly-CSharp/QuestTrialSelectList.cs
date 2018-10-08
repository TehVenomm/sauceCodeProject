using System.Collections.Generic;
using UnityEngine;

public class QuestTrialSelectList : QuestEventSelectList
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
		SPR_CLEARD_BLACK,
		SPR_FRAME
	}

	protected override bool showMap => false;

	protected override void UpdateTable()
	{
		int num = 0;
		int count = stories.Count;
		if (count > 0)
		{
			num++;
		}
		List<ShowDeliveryData> list = new List<ShowDeliveryData>();
		if (deliveryInfo != null)
		{
			for (int j = 0; j < deliveryInfo.Length; j++)
			{
				ShowDeliveryData item = new ShowDeliveryData(j, false, deliveryInfo[j]);
				list.Add(item);
			}
		}
		if (clearedDeliveries != null)
		{
			for (int k = 0; k < clearedDeliveries.Count; k++)
			{
				ShowDeliveryData item2 = new ShowDeliveryData(k, true, clearedDeliveries[k]);
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
			SetActive(UI.STR_DELIVERY_NON_LIST, true);
			SetActive(UI.GRD_DELIVERY_QUEST, false);
			SetActive(UI.TBL_DELIVERY_QUEST, false);
		}
		else
		{
			SetActive(UI.STR_DELIVERY_NON_LIST, false);
			SetActive(UI.GRD_DELIVERY_QUEST, false);
			SetActive(UI.TBL_DELIVERY_QUEST, true);
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
			if ((bool)ctrl)
			{
				int l = 0;
				for (int childCount = ctrl.childCount; l < childCount; l++)
				{
					Transform child = ctrl.GetChild(0);
					child.parent = null;
					Object.Destroy(child.gameObject);
				}
			}
			bool isRenewalFlag = MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.isTheaterRenewal;
			SetTable(UI.TBL_DELIVERY_QUEST, string.Empty, num2, isResetUI, delegate(int i, Transform parent)
			{
				Transform transform = null;
				if (i >= storyStartIndex)
				{
					if (!HasChapterStory() || i == storyStartIndex || !isRenewalFlag)
					{
						return Realizes("QuestEventStoryItem", parent, true);
					}
					return null;
				}
				if (i < borderIndex)
				{
					if (i < questStartIndex)
					{
						if (!((Object)null != (Object)mapItem))
						{
							return Realizes("QuestEventBorderItem", parent, true);
						}
						return ResourceUtility.Realizes(mapItem.gameObject, parent, -1);
					}
					return Realizes("QuestRequestItemTrial", parent, true);
				}
				return Realizes("QuestEventBorderItem", parent, true);
			}, delegate(int i, Transform t, bool is_recycle)
			{
				if (!((Object)t == (Object)null))
				{
					SetActive(t, true);
					if (i >= storyStartIndex)
					{
						int storyIndex = i - storyStartIndex;
						InitStory(storyIndex, t);
					}
					else if (i < borderIndex || i >= storyStartIndex)
					{
						if (i >= questStartIndex && i < borderIndex)
						{
							int num3 = i - questStartIndex;
							InitDelivery(showList[num3], t);
							ChangeDeliveryFrameSprite(t);
						}
						else if (i < questStartIndex)
						{
							InitMap(t);
						}
					}
				}
			});
			UIScrollView component = GetComponent<UIScrollView>(UI.SCR_DELIVERY_QUEST);
			component.enabled = true;
			RepositionTable();
		}
	}

	protected override void UpdateCompletedDeliveryUI(Transform parent)
	{
		SetActive(parent, UI.SPR_CLEARD_BLACK, false);
	}

	protected override void ChangeDeliveryFrameSprite(Transform parent)
	{
		SetSprite(parent, UI.SPR_FRAME, "RequestPlate_Trial");
	}

	protected override void OnQuery_JUMP_TO_STORY_PAGE()
	{
		EventData[] array = new EventData[2]
		{
			new EventData("SELECT_TRIAL", null),
			new EventData("SELECT_CHAPTER_FROM_OUTER", null)
		};
		GameSection.SetEventData(new object[2]
		{
			GetChapterId(),
			array
		});
	}

	private void OnQuery_HOW_TO()
	{
		GameSection.SetEventData(WebViewManager.Trial);
	}

	protected override void OnQuery_SELECT_STORY()
	{
		int index = (int)GameSection.GetEventData();
		Story story = stories[index];
		EventData[] array = new EventData[3]
		{
			new EventData("MAIN_MENU_HOME", null),
			new EventData("TO_EVENT", null),
			new EventData("SELECT_TRIAL", eventData)
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
