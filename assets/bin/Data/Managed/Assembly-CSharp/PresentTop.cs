using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PresentTop : GameSection
{
	private enum UI
	{
		GRD_LIST,
		LBL_NOW,
		LBL_MAX,
		BTN_PAGE_PREV,
		BTN_PAGE_NEXT,
		BTN_ALL,
		BTN_ALL_DISABLE,
		STR_ALL_DISABLE,
		OBJ_ACTIVE_ROOT,
		OBJ_INACTIVE_ROOT,
		STR_TITLE,
		STR_TITLE_REFLECT,
		STR_NON_LIST,
		LBL_NAME,
		LBL_COMMENT,
		LBL_DESC,
		LBL_TIME,
		LBL_EXPIRE,
		BTN_SELECT,
		OBJ_ICON_ROOT
	}

	private object[] selectEventData;

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetLabelText((Enum)UI.STR_TITLE, base.sectionData.GetText("STR_TITLE"));
		SetLabelText((Enum)UI.STR_TITLE_REFLECT, base.sectionData.GetText("STR_TITLE"));
		int count = MonoBehaviourSingleton<PresentManager>.I.presentData.presents.Count;
		bool flag = count > 0;
		SetActive((Enum)UI.BTN_ALL, flag);
		SetActive((Enum)UI.BTN_ALL_DISABLE, !flag);
		SetLabelText((Enum)UI.STR_ALL_DISABLE, base.sectionData.GetText("STR_ALL"));
		SetActive((Enum)UI.STR_NON_LIST, !flag);
		SetGrid(UI.GRD_LIST, "PresentListItem", count, false, delegate(int i, Transform t, bool b)
		{
			Present present = MonoBehaviourSingleton<PresentManager>.I.presentData.presents[i];
			SetLabelText(t, UI.LBL_NAME, present.name);
			SetLabelText(t, UI.LBL_COMMENT, present.comment);
			SetLabelText(t, UI.LBL_DESC, present.desc);
			string text = (!string.IsNullOrEmpty(present.expire)) ? present.expire : base.sectionData.GetText("NON_EXPIRE");
			SetLabelText(t, UI.LBL_EXPIRE, text);
			SetLabelText(t, UI.LBL_TIME, present.timeInfo);
			SetEvent(t, UI.BTN_SELECT, "SELECT", i);
			ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon((REWARD_TYPE)present.type, (uint)present.itemId, FindCtrl(t, UI.OBJ_ICON_ROOT), -1, null, 0, false, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT);
			if (itemIcon != null)
			{
				itemIcon.SetEnableCollider(false);
			}
		});
		int num = 1;
		int page_num = MonoBehaviourSingleton<PresentManager>.I.page + num;
		int num2 = Mathf.Max(MonoBehaviourSingleton<PresentManager>.I.pageMax, num);
		SetPageNumText((Enum)UI.LBL_NOW, page_num);
		SetPageNumText((Enum)UI.LBL_MAX, num2);
		SetActive((Enum)UI.OBJ_ACTIVE_ROOT, num != num2);
		SetActive((Enum)UI.OBJ_INACTIVE_ROOT, num == num2);
	}

	public override void StartSection()
	{
	}

	private void MovePage(int page, bool is_on_query_event = true)
	{
		if (page < 0)
		{
			page = MonoBehaviourSingleton<PresentManager>.I.pageMax - 1;
		}
		else if (page >= MonoBehaviourSingleton<PresentManager>.I.pageMax)
		{
			page = 0;
		}
		if (is_on_query_event)
		{
			GameSection.StayEvent();
		}
		MonoBehaviourSingleton<PresentManager>.I.SendGetPresent(page, delegate(bool is_success)
		{
			if (is_on_query_event)
			{
				GameSection.ResumeEvent(is_success, null);
			}
		});
	}

	private void OnQuery_PAGE_PREV()
	{
		MovePage(MonoBehaviourSingleton<PresentManager>.I.page - 1, true);
	}

	private void OnQuery_PAGE_NEXT()
	{
		MovePage(MonoBehaviourSingleton<PresentManager>.I.page + 1, true);
	}

	private void OnQuery_SELECT()
	{
		Present present = MonoBehaviourSingleton<PresentManager>.I.presentData.presents[(int)GameSection.GetEventData()];
		selectEventData = new object[3]
		{
			(int)GameSection.GetEventData(),
			present.name,
			1
		};
		GameSection.SetEventData(selectEventData);
		List<string> list = new List<string>();
		list.Add(present.uniqId);
		SendReceivePresent(list);
	}

	private void OnQuery_ALL()
	{
		selectEventData = new object[3]
		{
			-1,
			string.Empty,
			MonoBehaviourSingleton<PresentManager>.I.presentData.presents.Count
		};
		GameSection.SetEventData(selectEventData);
	}

	private void OnQuery_PresentAllConfirm_YES()
	{
		GameSection.SetEventData(selectEventData);
		List<string> uniqIds = new List<string>();
		MonoBehaviourSingleton<PresentManager>.I.presentData.presents.ForEach(delegate(Present o)
		{
			uniqIds.Add(o.uniqId);
		});
		SendReceivePresent(uniqIds);
	}

	private void SendReceivePresent(List<string> ids)
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<PresentManager>.I.SendReceivePresent(ids, delegate(bool is_success, Error network_err, int num)
		{
			bool is_resume = is_success;
			if (is_success)
			{
				selectEventData[2] = num;
				SoundManager.PlaySystemSE(SoundID.UISE.GET_PRIZE, 1f);
			}
			else
			{
				is_resume = true;
				switch (network_err)
				{
				default:
					is_resume = false;
					break;
				case Error.WRN_PRESENT_OVER_MONEY:
					GameSection.ChangeStayEvent("WRN_PRESENT_OVER_MONEY", null);
					break;
				case Error.WRN_PRESENT_OVER_ITEM:
					GameSection.ChangeStayEvent("WRN_PRESENT_OVER_ITEM", null);
					break;
				case Error.WRN_PRESENT_OVER_EQUIP_ITEM:
					GameSection.ChangeStayEvent("WRN_PRESENT_OVER_EQUIP_ITEM", null);
					break;
				case Error.WRN_PRESENT_OVER_SKILL_ITEM:
					GameSection.ChangeStayEvent("WRN_PRESENT_OVER_SKILL_ITEM", null);
					break;
				case Error.WRN_PRESENT_OVER_QUEST_ITEM:
					GameSection.ChangeStayEvent("WRN_PRESENT_OVER_QUEST_ITEM", null);
					break;
				case Error.WRN_PRESENT_OVER_EQUIP_AND_SKILL:
					GameSection.ChangeStayEvent("WRN_PRESENT_OVER_EQUIP_AND_SKILL", null);
					break;
				case Error.WRN_PRESENT_OVER_ETC:
					GameSection.ChangeStayEvent("WRN_PRESENT_OVER_ETC", null);
					break;
				}
			}
			GameSection.ResumeEvent(is_resume, null);
		});
	}

	private void OnQuery_PresentOneMessage_OK()
	{
	}

	private void OnQuery_PresentAllMessage_OK()
	{
	}

	public void OnQuery_PresentRecvOverEquipItem_GO_ITEM_STORAGE()
	{
		GO_ITEM_STORAGE(ItemStorageTop.TAB_MODE.EQUIP);
	}

	public void OnQuery_PresentRecvOverSkillItem_GO_ITEM_STORAGE()
	{
		GO_ITEM_STORAGE(ItemStorageTop.TAB_MODE.SKILL);
	}

	public void OnQuery_PresentRecvOverEquipAndSkill_GO_ITEM_STORAGE()
	{
		GO_ITEM_STORAGE(ItemStorageTop.TAB_MODE.SKILL);
	}

	private void GO_ITEM_STORAGE(ItemStorageTop.TAB_MODE tab)
	{
		string name = "TAB_" + (int)tab;
		EventData[] autoEvents = new EventData[5]
		{
			new EventData("SECTION_BACK", null),
			new EventData("SECTION_BACK", null),
			new EventData("MAIN_MENU_STUDIO", null),
			new EventData("TO_STORAGE", null),
			new EventData(name, null)
		};
		GameSection.StopEvent();
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
	}

	public void OnQuery_PresentRecvOverEquipItem_EXPAND_STORAGE()
	{
		EXPAND_STORAGE();
	}

	public void OnQuery_PresentRecvOverSkillItem_EXPAND_STORAGE()
	{
		EXPAND_STORAGE();
	}

	public void OnQuery_PresentRecvOverEquipAndSkill_EXPAND_STORAGE()
	{
		EXPAND_STORAGE();
	}

	private void EXPAND_STORAGE()
	{
		DispatchEvent("EXPAND_STORAGE", null);
	}

	private void OnQuery_CAUTION()
	{
		GameSection.SetEventData(WebViewManager.Present);
	}

	public override void OnNotify(NOTIFY_FLAG notify_flags)
	{
		if ((notify_flags & NOTIFY_FLAG.UPDATE_PRESENT_NUM) != (NOTIFY_FLAG)0L && (notify_flags & NOTIFY_FLAG.UPDATE_PRESENT_LIST) == (NOTIFY_FLAG)0L)
		{
			int num = (MonoBehaviourSingleton<PresentManager>.I.presentNum > 0) ? ((MonoBehaviourSingleton<PresentManager>.I.presentNum - 1) / MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.LIST_NUM_PER_PAGE) : 0;
			int page = Mathf.Min(num, MonoBehaviourSingleton<PresentManager>.I.page);
			SetDirty(UI.GRD_LIST);
			MovePage(page, false);
		}
		else if ((notify_flags & NOTIFY_FLAG.UPDATE_PRESENT_LIST) != (NOTIFY_FLAG)0L)
		{
			SetDirty(UI.GRD_LIST);
		}
		base.OnNotify(notify_flags);
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_PRESENT_LIST;
	}
}
