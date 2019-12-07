using System.Linq;

public class QuestRoomSettings : GameSection
{
	private enum UI
	{
		LBL_LEVEL,
		STR_NON_CONDITION_TOTAL,
		OBJ_OPTION,
		OBJ_LOCK,
		OBJ_LOCK_LOUNGE
	}

	private object[] eventData;

	private QUEST_TYPE questType;

	protected PartyManager.PartySetting setting;

	public override void Initialize()
	{
		eventData = (GameSection.GetEventData() as object[]);
		questType = (QUEST_TYPE)eventData[0];
		if (MonoBehaviourSingleton<PartyManager>.I.partySetting != null)
		{
			PartyManager.PartySetting partySetting = MonoBehaviourSingleton<PartyManager>.I.partySetting;
			setting = new PartyManager.PartySetting(partySetting.isLock, partySetting.level, partySetting.total);
		}
		else
		{
			setting = new PartyManager.PartySetting(is_lock: true, 0, 0);
		}
		MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(null);
		if (MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge())
		{
			SetActive(UI.OBJ_LOCK_LOUNGE, is_visible: true);
			SetActive(UI.OBJ_LOCK, is_visible: false);
		}
		else
		{
			SetActive(UI.OBJ_LOCK_LOUNGE, is_visible: false);
			SetActive(UI.OBJ_LOCK, is_visible: true);
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		UpdateRoomSettingsText();
		UpdateSelectFrame();
	}

	private void UpdateRoomSettingsText()
	{
		if (setting.level > 0)
		{
			SetActive(UI.STR_NON_CONDITION_TOTAL, is_visible: false);
			SetActive(UI.LBL_LEVEL, is_visible: true);
			SetLabelText(UI.LBL_LEVEL, setting.level.ToString());
		}
		else
		{
			SetActive(UI.LBL_LEVEL, is_visible: false);
			SetActive(UI.STR_NON_CONDITION_TOTAL, is_visible: true);
		}
	}

	private void UpdateSelectFrame()
	{
		QuestRoomSettingsOption component = GetComponent<QuestRoomSettingsOption>(UI.OBJ_OPTION);
		if (component != null)
		{
			component.SetShowOption(is_enable: true);
		}
	}

	private void OnQuery_ROOM()
	{
		ToRoom();
	}

	private void ToRoom()
	{
		GameSection.SetEventData(eventData);
		if (setting.isLock)
		{
			setting.level = 0;
			setting.total = 0;
		}
		GameSection.StayEvent();
		if (!MonoBehaviourSingleton<PartyManager>.I.IsInParty())
		{
			int currentQuestID = (int)MonoBehaviourSingleton<QuestManager>.I.currentQuestID;
			QuestTable.QuestTableData questData = Singleton<QuestTable>.I.GetQuestData(MonoBehaviourSingleton<QuestManager>.I.currentQuestID);
			bool flag = MonoBehaviourSingleton<QuestManager>.I.GetExploreEventIds().Contains(questData.eventId);
			setting.ex = (flag ? 1 : 0);
			MonoBehaviourSingleton<PartyManager>.I.SendCreate(currentQuestID, setting, delegate(bool is_success)
			{
				if (is_success)
				{
					MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(setting);
				}
				GameSection.ResumeEvent(is_success);
			});
		}
		else
		{
			MonoBehaviourSingleton<PartyManager>.I.SendEdit(setting, delegate(bool is_success)
			{
				if (is_success)
				{
					MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(setting);
				}
				GameSection.ResumeEvent(is_success);
			});
		}
	}

	private void OnQuery_CoopServerInvalidConfirm_YES()
	{
		GameSection.StayEvent();
		CoopApp.EnterQuestOffline(delegate(bool is_m, bool is_c, bool is_r, bool is_s)
		{
			GameSection.ResumeEvent(is_s);
		});
	}

	private void OnQuery_CLOSE()
	{
		if (MonoBehaviourSingleton<PartyManager>.I.partySetting != null)
		{
			GameSection.ChangeEvent("ROOM");
		}
		else if (questType == QUEST_TYPE.ORDER)
		{
			GameSection.ChangeEvent("TO_ORDER");
		}
		else
		{
			GameSection.ChangeEvent("TO_SELECT");
		}
	}

	private void OnQuery_LOCK()
	{
		setting.isLock = true;
		UpdateSelectFrame();
		ToRoom();
	}

	private void OnQuery_LOCK_LOUNGE()
	{
		setting.isLock = true;
		UpdateSelectFrame();
		ToRoom();
	}

	private void OnQuery_UNLOCK()
	{
		setting.isLock = false;
		UpdateSelectFrame();
		ToRoom();
	}

	private void OnQuery_LEVEL()
	{
		MonoBehaviourSingleton<PartyManager>.I.SetPartySetting(setting);
		GameSection.SetEventData(eventData);
	}
}
