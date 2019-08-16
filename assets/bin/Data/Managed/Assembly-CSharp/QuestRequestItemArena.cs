using UnityEngine;

public class QuestRequestItemArena : QuestRequestItem
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
		OBJ_LEVEL_LIMIT,
		LBL_LEVEL_LIMIT,
		SPR_TYPE_TEXT_STORY,
		SPR_TYPE_TEXT_STORY_HARD,
		SPR_TYPE_TEXT_MODE_SECRET,
		SPR_TYPE_TEXT_MODE_HARD,
		LBL_BEST_TIME,
		SPR_BEST_TIME,
		SPR_MAIN_REWARD,
		OBJ_NEED,
		SPR_TYPE_DIFFICULTY
	}

	private ArenaTable.ArenaData arenaData;

	public void SetupComplete(Transform t, DeliveryTable.DeliveryData info, ArenaUserRecordModel.Param record)
	{
		SetActive(t, UI.SPR_TYPE_DIFFICULTY, info.difficulty >= DIFFICULTY_MODE.HARD);
		InitArenaData(info);
		base.Setup(t, info);
		SetBestTimeOrStamp(t, record);
	}

	public override void Setup(Transform t, DeliveryTable.DeliveryData info)
	{
		SetActive(t, UI.SPR_TYPE_DIFFICULTY, info.difficulty >= DIFFICULTY_MODE.HARD);
		SetActive(t, UI.SPR_BEST_TIME, is_visible: false);
		SetActive(t, UI.LBL_BEST_TIME, is_visible: false);
		InitArenaData(info);
		base.Setup(t, info);
	}

	protected override void SetFrame(Transform t, DeliveryTable.DeliveryData info)
	{
	}

	protected override void SetDeliveryName(Transform t, DeliveryTable.DeliveryData info)
	{
		string arenaTitle = QuestUtility.GetArenaTitle(arenaData.group, info.name);
		SetLabelText(t, UI.LBL_DELIVERY_COMMENT, arenaTitle);
	}

	protected override void SetIcon(Transform t, DeliveryTable.DeliveryData info)
	{
		UITexture component = FindCtrl(t, UI.TEX_NPC).GetComponent<UITexture>();
		ResourceLoad.LoadWithSetUITexture(component, RESOURCE_CATEGORY.ARENA_RANK_ICON, ResourceName.GetArenaRankIconName(arenaData.rank));
	}

	private void InitArenaData(DeliveryTable.DeliveryData info)
	{
		arenaData = info.GetArenaData();
	}

	private void SetBestTimeOrStamp(Transform t, ArenaUserRecordModel.Param record)
	{
		if (arenaData.rank == ARENA_RANK.S)
		{
			string text = (record == null) ? QuestUtility.CreateTimeStringByMilliSec(QuestUtility.GetDefaultArenaTime()) : QuestUtility.CreateTimeStringByMilliSec(record.clearMilliSecList[(int)arenaData.group]);
			SetActive(t, UI.SPR_BEST_TIME, is_visible: true);
			SetActive(t, UI.LBL_BEST_TIME, is_visible: true);
			SetLabelText(t, UI.LBL_BEST_TIME, text);
			SetActive(t, UI.GRD_ICON_ROOT, is_visible: false);
			SetActive(t, UI.SPR_MAIN_REWARD, is_visible: false);
			SetActive(t, UI.OBJ_NEED, is_visible: false);
		}
		else
		{
			SetActive(t, UI.SPR_BEST_TIME, is_visible: false);
			SetActive(t, UI.LBL_BEST_TIME, is_visible: false);
			SetActive(t, UI.OBJ_REQUEST_COMPLETED, is_visible: true);
		}
	}
}
