using Network;
using UnityEngine;

public class QuestRequestItemSeriesArena : QuestRequestItem
{
	private enum UI
	{
		TEX_NPC,
		LBL_DELIVERY_COMMENT,
		OBJ_REQUEST_OK,
		OBJ_REQUEST_COMPLETED,
		LBL_HAVE,
		LBL_NEED,
		LBL_NEED_ITEM_NAME,
		LBL_LIMIT,
		SPR_TYPE_NORMAL,
		SPR_TYPE_EVENT,
		SPR_TYPE_STORY,
		SPR_TYPE_HARD,
		SPR_TYPE_SUB_EVENT,
		SPR_TYPE_EVENT_TEXT,
		SPR_TYPE_DAILY_TEXT,
		SPR_TYPE_WEEKLY_TEXT,
		SPR_DROP_DIFFICULTY_RARE,
		SPR_DROP_DIFFICULTY_SUPER_RARE,
		SPR_FRAME,
		OBJ_ICON_ROOT_1,
		OBJ_ICON_ROOT_2,
		GRD_ICON_ROOT,
		SPR_TYPE_TEXT_STORY,
		SPR_TYPE_TEXT_STORY_HARD,
		SPR_TYPE_TEXT_MODE_SECRET,
		SPR_TYPE_TEXT_MODE_HARD,
		LBL_BEST_TIME,
		SPR_BEST_TIME,
		SPR_MAIN_REWARD,
		OBJ_NEED,
		SPR_TYPE_DIFFICULTY,
		TEX_RANK_ICON,
		SPR_MISSION_CROWN_ON,
		SPR_MISSION_CROWN_OFF
	}

	public override void Setup(Transform t, DeliveryTable.DeliveryData info)
	{
		SetActive(t, UI.SPR_TYPE_DIFFICULTY, info.difficulty >= DIFFICULTY_MODE.HARD);
		SetBestTimeOrStamp(t, info.GetQuestData().questID);
		base.Setup(t, info);
	}

	protected override void SetFrame(Transform t, DeliveryTable.DeliveryData info)
	{
	}

	protected override void SetIcon(Transform t, DeliveryTable.DeliveryData info)
	{
		ResourceLoad.LoadWithSetUITexture(FindCtrl(t, UI.TEX_NPC).GetComponent<UITexture>(), RESOURCE_CATEGORY.SERIES_ARENA_RANK_ICON, ResourceName.GetSeriesArenaRankIconName(info.GetQuestData().rarity));
	}

	private void SetBestTimeOrStamp(Transform t, uint questId)
	{
		ClearStatusQuest clearStatusQuestData = MonoBehaviourSingleton<QuestManager>.I.GetClearStatusQuestData(questId);
		if (clearStatusQuestData != null && clearStatusQuestData.clearTime > 0)
		{
			_ = clearStatusQuestData.clearTime;
			bool flag = MonoBehaviourSingleton<QuestManager>.I.CheckMissionAllClear(questId);
			SetActive(t, UI.SPR_MISSION_CROWN_OFF, !flag);
			SetActive(t, UI.SPR_MISSION_CROWN_ON, flag);
			SetActive(t, UI.SPR_BEST_TIME, is_visible: true);
			SetActive(t, UI.LBL_BEST_TIME, is_visible: true);
			string text = QuestUtility.CreateTimeStringByMilliSecSeriesArena(clearStatusQuestData.clearTime);
			SetLabelText(t, UI.LBL_BEST_TIME, text);
		}
		else
		{
			SetActive(t, UI.SPR_BEST_TIME, is_visible: false);
			SetActive(t, UI.LBL_BEST_TIME, is_visible: false);
		}
	}
}
