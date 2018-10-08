using System;
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

	protected unsafe override void UpdateTable()
	{
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Expected O, but got Unknown
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
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
		if (num2 == 0)
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
			if (Object.op_Implicit(ctrl))
			{
				int k = 0;
				for (int childCount = ctrl.get_childCount(); k < childCount; k++)
				{
					Transform val = ctrl.GetChild(0);
					val.set_parent(null);
					Object.Destroy(val.get_gameObject());
				}
			}
			bool isRenewalFlag = MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.isTheaterRenewal;
			_003CUpdateTable_003Ec__AnonStorey412 _003CUpdateTable_003Ec__AnonStorey;
			SetTable(UI.TBL_DELIVERY_QUEST, string.Empty, num2, isResetUI, new Func<int, Transform, Transform>((object)_003CUpdateTable_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), new Action<int, Transform, bool>((object)_003CUpdateTable_003Ec__AnonStorey, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			UIScrollView component = base.GetComponent<UIScrollView>((Enum)UI.SCR_DELIVERY_QUEST);
			component.set_enabled(true);
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
