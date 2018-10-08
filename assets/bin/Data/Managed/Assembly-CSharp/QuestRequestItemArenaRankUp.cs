using UnityEngine;

public class QuestRequestItemArenaRankUp : QuestRequestItem
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
		SPR_BEST_TIME
	}

	private ArenaTable.ArenaData arenaData;

	public override void Setup(Transform t, DeliveryTable.DeliveryData info)
	{
		SetActive(t, UI.SPR_BEST_TIME, false);
		SetActive(t, UI.LBL_BEST_TIME, false);
		InitArenaData(info);
		base.Setup(t, info);
	}

	protected override void SetFrame(Transform t, DeliveryTable.DeliveryData info)
	{
	}

	protected override void SetIcon(Transform t, DeliveryTable.DeliveryData info)
	{
		UITexture component = FindCtrl(t, UI.TEX_NPC).GetComponent<UITexture>();
		ResourceLoad.LoadWithSetUITexture(component, RESOURCE_CATEGORY.ARENA_RANK_ICON, ResourceName.GetArenaRankIconName(arenaData.rank));
	}

	public void InitArenaData(DeliveryTable.DeliveryData info)
	{
		arenaData = info.GetArenaData();
	}
}
