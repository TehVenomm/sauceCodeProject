using System;

public class QuestAcceptArenaRoomUserEquipDetail : ItemDetailEquipDialog
{
	protected new enum UI
	{
		OBJ_DETAIL_ROOT,
		TEX_MODEL,
		OBJ_FRAME_BG,
		STR_LV,
		LBL_NAME,
		LBL_LV_NOW,
		LBL_LV_MAX,
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		LBL_ELEM,
		LBL_ELEM_DEF,
		SPR_ELEM,
		SPR_ELEM_DEF,
		LBL_SELL,
		OBJ_SKILL_BUTTON_ROOT,
		SPR_IS_EVOLVE,
		OBJ_FAVORITE_ROOT,
		TWN_FAVORITE,
		TWN_UNFAVORITE,
		OBJ_ATK_ROOT,
		OBJ_DEF_ROOT,
		OBJ_ELEM_ROOT,
		STR_ONLY_VISUAL,
		SPR_TYPE_ICON,
		SPR_TYPE_ICON_BG,
		SPR_TYPE_ICON_RARITY,
		STR_TITLE_ITEM_INFO,
		STR_TITLE_STATUS,
		STR_TITLE_SKILL_SLOT,
		STR_TITLE_ABILITY,
		STR_TITLE_SELL,
		STR_TITLE_ATK,
		STR_TITLE_DEF,
		STR_TITLE_HP,
		STR_TITLE_ELEM_ATK,
		STR_TITLE_ELEM_DEF,
		TBL_ABILITY,
		STR_NON_ABILITY,
		OBJ_ABILITY,
		LBL_ABILITY,
		LBL_ABILITY_NUM,
		OBJ_FIXEDABILITY,
		LBL_FIXEDABILITY,
		LBL_FIXEDABILITY_NUM,
		OBJ_ABILITY_ITEM,
		LBL_ABILITY_ITEM,
		BTN_SELL,
		BTN_EXCEED,
		SPR_COUNT_0_ON,
		SPR_COUNT_1_ON,
		SPR_COUNT_2_ON,
		SPR_COUNT_3_ON,
		BTN_CHANGE,
		BTN_CREATE,
		BTN_GROW,
		BTN_ABILITY,
		BTN_GROW_OFF,
		BTN_ABILITY_OFF,
		STR_BTN_CHANGE,
		STR_BTN_CREATE,
		STR_BTN_GROW,
		STR_BTN_ABILITY,
		STR_BTN_CHANGE_D,
		STR_BTN_CREATE_D,
		STR_BTN_GROW_D,
		STR_BTN_ABILITY_D,
		OBJ_NEED_UPDATE_ABILITY,
		LBL_NEED_UPDATE_ABILITY,
		BTN_GRAPH,
		SPR_SP_ATTACK_TYPE,
		OBJ_EVOLVE_SELECT,
		LBL_EVOLVE_NORMAL,
		SPR_EVOLVE_ELEM,
		LBL_EVOLVE_ATTRIBUTE,
		OBJ_ARROW_BTN_ROOT
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		SetActive((Enum)UI.BTN_CHANGE, is_visible: true);
	}

	protected override void OnQuery_SKILL_ICON_BUTTON()
	{
		object detailItemData = base.detailItemData;
		GameSection.SetEventData(new object[3]
		{
			CURRENT_SECTION.STATUS_TOP,
			detailItemData,
			sex
		});
	}
}
