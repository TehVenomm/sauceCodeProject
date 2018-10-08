using UnityEngine;

public class ItemDetailQuest : QuestSelect
{
	protected new enum UI
	{
		OBJ_FRAME,
		TEX_ENEMY,
		SPR_LOAD_ROTATE_CIRCLE,
		OBJ_LOADING,
		OBJ_QUEST_NORMAL_ROOT,
		LBL_QUEST_TYPE,
		LBL_QUEST_NAME,
		LBL_QUEST_NUM,
		LBL_LIMIT_TIME,
		OBJ_TOP_CROWN_ROOT,
		OBJ_TOP_CROWN_1,
		OBJ_TOP_CROWN_2,
		OBJ_TOP_CROWN_3,
		STR_MISSION_EMPTY,
		SPR_CROWN_1,
		SPR_CROWN_2,
		SPR_CROWN_3,
		OBJ_MISSION_INFO_ROOT,
		OBJ_MISSION_INFO,
		OBJ_MISSION_INFO_1,
		OBJ_MISSION_INFO_2,
		OBJ_MISSION_INFO_3,
		LBL_MISSION_INFO_1,
		LBL_MISSION_INFO_2,
		LBL_MISSION_INFO_3,
		SPR_MISSION_INFO_CROWN_1,
		SPR_MISSION_INFO_CROWN_2,
		SPR_MISSION_INFO_CROWN_3,
		STR_MISSION,
		TWN_CHANGE_BTN,
		OBJ_CHANGE_INFO_TREASURE_ROOT,
		OBJ_CHANGE_INFO_MISSION_ROOT,
		OBJ_CHANGE_INFO_SELL_ROOT,
		OBJ_TREASURE,
		STR_TREASURE,
		GRD_REWARD_QUEST,
		OBJ_SELL_ITEM,
		STR_SELL,
		GRD_REWARD_SELL,
		OBJ_ENEMY,
		SPR_MONSTER_ICON,
		SPR_MONSTER_ICON_GRADE_FRAME,
		SPR_ELEMENT_ROOT,
		SPR_ELEMENT,
		SPR_WEAK_ELEMENT,
		STR_NON_ELEMENT,
		STR_NON_WEAK_ELEMENT,
		SPR_ELEMENT_ROOT_2,
		SPR_ELEMENT_2,
		SPR_WEAK_ELEMENT_2,
		STR_NON_ELEMENT_2,
		STR_NON_WEAK_ELEMENT_2,
		BTN_PARTY,
		BTN_BACK,
		OBJ_PARTY_OPT,
		TWN_DIFFICULT_STAR,
		OBJ_DIFFICULT_STAR_1,
		OBJ_DIFFICULT_STAR_2,
		OBJ_DIFFICULT_STAR_3,
		OBJ_DIFFICULT_STAR_4,
		OBJ_DIFFICULT_STAR_5,
		OBJ_DIFFICULT_STAR_6,
		OBJ_DIFFICULT_STAR_7,
		OBJ_DIFFICULT_STAR_8,
		OBJ_DIFFICULT_STAR_9,
		OBJ_DIFFICULT_STAR_10,
		OBJ_ICON,
		OBJ_ICON_NEW,
		OBJ_ICON_CLEARED,
		OBJ_ICON_COMPLETE,
		SPR_ICON_NEW,
		SPR_ICON_CLEARED,
		SPR_ICON_COMPLETE,
		OBJ_BACK_BTN_ROOT,
		OBJ_PARTY_BTN_ROOT,
		BTN_NEXT,
		OBJ_NEXT_OPT,
		OBJ_NEXT_BTN_ROOT,
		BTN_SELL,
		STR_BTN_SELL,
		STR_BTN_SELL_D,
		BTN_BATTLE,
		OBJ_REWARD_ICON_ROOT,
		OBJ_MATERIAL_ICON_ROOT,
		LBL_ENEMY_LEVEL,
		OBJ_LEVEL_R,
		OBJ_LEVEL_L,
		OBJ_LEVEL_INACTIVE_R,
		OBJ_LEVEL_INACTIVE_L
	}

	private Transform questPrefab;

	private QuestSortData data;

	private Color backupAmbientLight = RenderSettings.ambientLight;

	private bool isGachaResult;

	private bool isInProgressMultiResultGacha;

	public override void Initialize()
	{
		data = (GameSection.GetEventData() as QuestSortData);
		QuestItemInfo questItemInfo = data.GetItemData() as QuestItemInfo;
		GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.QUEST_ITEM, data.GetUniqID());
		GameSection.SetEventData(questItemInfo.infoData);
		isGachaResult = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName().Contains("Gacha");
		if (isGachaResult)
		{
			isInProgressMultiResultGacha = (MonoBehaviourSingleton<GachaManager>.IsValid() && MonoBehaviourSingleton<GachaManager>.I.IsMultiResult() && MonoBehaviourSingleton<GachaManager>.I.IsExistNextGachaResult());
			backupAmbientLight = RenderSettings.ambientLight;
			RenderSettings.ambientLight = Utility.MakeColorByInt(201, 210, 226, 255);
		}
		base.Initialize();
	}

	protected override void OnClose()
	{
		if (isGachaResult)
		{
			RenderSettings.ambientLight = backupAmbientLight;
		}
		base.OnClose();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		SetActive(UI.BTN_NEXT, false);
		SetActive(UI.OBJ_NEXT_OPT, false);
		SetActive(UI.BTN_PARTY, false);
		SetActive(UI.OBJ_PARTY_OPT, false);
		SetActive(UI.BTN_BACK, false);
		questPrefab = SetPrefab(base.collectUI, "ItemDetailQuest", true);
		SetActive(questPrefab, UI.BTN_SELL, !isGachaResult);
		SetActive(questPrefab, UI.BTN_BATTLE, isGachaResult && !isInProgressMultiResultGacha);
		SetActive(UI.OBJ_PARTY_BTN_ROOT, false);
		SetLabelText(questPrefab, UI.STR_BTN_SELL, base.sectionData.GetText("TEXT_EXCHANGE"));
		SetLabelText(questPrefab, UI.STR_BTN_SELL_D, base.sectionData.GetText("TEXT_EXCHANGE"));
		SetActive(questPrefab, UI.OBJ_ICON, !isGachaResult);
	}

	protected override void SetClearStatus(CLEAR_STATUS clear_status)
	{
		if (!isGachaResult)
		{
			base.SetClearStatus(clear_status);
		}
	}

	private void OnQuery_SELL()
	{
		if (!data.CanSale())
		{
			GameSection.ChangeEvent("NOT_SELL", null);
		}
		else
		{
			GameSection.SetEventData(data);
		}
	}

	private void OnQuery_BATTLE()
	{
		if (data == null)
		{
			GameSection.StopEvent();
		}
	}

	protected void OnQuery_ItemDetailJumpQuestConfirm_YES()
	{
		string name = (!MonoBehaviourSingleton<LoungeMatchingManager>.I.IsInLounge()) ? "MAIN_MENU_HOME" : "MAIN_MENU_LOUNGE";
		EventData[] autoEvents = new EventData[4]
		{
			new EventData(name, null),
			new EventData("GACHA_QUEST_COUNTER", null),
			new EventData("TO_GACHA_QUEST_COUNTER", null),
			new EventData("SELECT_ORDER", data.GetTableID())
		};
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY) != (NOTIFY_FLAG)0L)
		{
			QuestItemInfo questItem = MonoBehaviourSingleton<InventoryManager>.I.GetQuestItem(data.GetTableID());
			if (questItem != null && questItem.infoData != null && questItem.infoData.questData.num > 0)
			{
				data = new QuestSortData();
				data.SetItem(questItem);
			}
		}
		base.OnNotify(flags);
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY;
	}
}
