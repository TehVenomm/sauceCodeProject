public class QuestAcceptEquipSecond : StatusEquipSecond
{
	public new enum UI
	{
		LBL_NAME,
		LBL_LV_NOW,
		LBL_LV_MAX,
		LBL_ATK,
		LBL_DEF,
		LBL_ELEM,
		SPR_ELEM,
		LBL_SELL,
		TEX_MODEL,
		SCR_INVENTORY,
		GRD_INVENTORY,
		GRD_INVENTORY_SMALL,
		LBL_SORT,
		BTN_SORT,
		BTN_BACK,
		TGL_CHANGE_INVENTORY,
		TGL_ICON_ASC,
		OBJ_SKILL_BUTTON_ROOT,
		OBJ_DETAIL_ROOT,
		BTN_SELL,
		BTN_GROW,
		OBJ_FAVORITE_ROOT,
		SPR_IS_EVOLVE,
		OBJ_ATK_ROOT,
		OBJ_DEF_ROOT,
		OBJ_ELEM_ROOT,
		OBJ_SELL_ROOT,
		SPR_SELECT_WEAPON,
		SPR_SELECT_DEF,
		SPR_TYPE_ICON,
		SPR_TYPE_ICON_BG,
		SPR_TYPE_ICON_RARITY,
		LBL_SELECT_TYPE,
		OBJ_STATUS_ROOT,
		LBL_STATUS_ATK,
		LBL_STATUS_DEF,
		LBL_STATUS_HP,
		LBL_STATUS_ADD_ATK,
		LBL_STATUS_ADD_DEF,
		LBL_STATUS_ADD_HP,
		STR_TITLE_ITEM_INFO,
		STR_TITLE_STATUS,
		STR_TITLE_SKILL_SLOT,
		STR_TITLE_ABILITY,
		STR_TITLE_MONEY,
		STR_TITLE_MATERIAL,
		STR_TITLE_ELEMENT,
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
		OBJ_WEAPON_WINDOW,
		OBJ_DEFENSE_WINDOW,
		SCR_INVENTORY_DEF,
		GRD_INVENTORY_DEF,
		GRD_INVENTORY_SMALL_DEF,
		BTN_WEAPON_1,
		BTN_WEAPON_2,
		BTN_WEAPON_3,
		BTN_WEAPON_4,
		BTN_WEAPON_5,
		OBJ_CAPTION_3,
		LBL_CAPTION
	}

	protected override void OnQuery_SELECT_ITEM()
	{
		if (OnSelectItemAndChekIsGoStatus())
		{
			GameSection.ChangeEvent("USER_EQUIP", null);
		}
	}

	protected override void ChangeSelectItem(EquipItemInfo select_item, EquipItemInfo old_item)
	{
		GameSection.SetEventData(new ChangeEquipData(base.selectEquipSetData.setNo, base.selectEquipSetData.index, select_item));
		if (old_item == null || select_item == null)
		{
			GameSection.ChangeEvent("USER_EQUIP", null);
		}
		else
		{
			bool flag = false;
			for (int i = 0; i < old_item.GetMaxSlot(); i++)
			{
				if (old_item.GetSkillItem(i, MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo()) != null)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				migrationOldItem = old_item;
				migrationSelectItem = select_item;
				object eventData = GameSection.GetEventData();
				GameSection.ChangeEvent("MIGRATION_SKILL_CONFIRM", null);
			}
			else
			{
				GameSection.ChangeEvent("USER_EQUIP", null);
			}
		}
	}

	private void OnQuery_QuestAcceptMigrationSkillConfirm_YES()
	{
		base.OnQuery_StatusMigrationSkillConfirm_YES();
	}

	private void OnQuery_QuestAcceptMigrationSkillConfirm_NO()
	{
		base.OnQuery_StatusMigrationSkillConfirm_NO();
	}

	private void OnQuery_QuestAcceptSwapEquipConfirm_YES()
	{
		OnQuery_StatusSwapEquipConfirm_YES();
	}
}
