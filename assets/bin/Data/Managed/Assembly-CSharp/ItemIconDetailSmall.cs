using UnityEngine;

public class ItemIconDetailSmall : ItemIcon
{
	private const string STR_X = "×";

	public UISprite spriteValueType;

	public UISprite spRegistedAchievement;

	public UISprite spGrowMax;

	public UISprite spriteValidEvolve;

	public UISprite spriteGrayOut;

	public UISprite spriteEquipIndex;

	public UISprite spriteSelectNumber;

	public UISprite spriteEnableExceed;

	public UISprite[] spriteBgSmall;

	public static ItemIcon CreateSmallRemoveButton(Transform parent = null, string event_name = null, int event_data = 0, int toggle_group = -1, bool is_select = false, string name = null)
	{
		ItemIconDetailSmall itemIconDetailSmall = ItemIcon.CreateIcon<ItemIconDetailSmall>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailSmallPrefab, ITEM_ICON_TYPE.NONE, ItemIcon.GetRemoveButtonIconID(), null, parent, ELEMENT_TYPE.MAX, null, -1, event_name, event_data, false, toggle_group, is_select, name, false, 0, 0, false, QUEST_ICON_SIZE_TYPE.DEFAULT, GET_TYPE.PAY);
		itemIconDetailSmall.EquipTypeIconInit(null);
		itemIconDetailSmall.SetEquipIndexSprite(-1);
		itemIconDetailSmall.SetIconStatusSprite(ItemIconDetail.ICON_STATUS.NONE);
		itemIconDetailSmall.SetupSelectNumberSprite(-1);
		itemIconDetailSmall.SetFavoriteIcon(false);
		return itemIconDetailSmall;
	}

	public static ItemIcon CreateSmallMaterialIcon(ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity, Transform parent = null, int num = -1, string name = null, string event_name = null, int event_data = 0, int toggle_group = -1, bool is_select = false, bool is_new = false, int enemy_icon_id = 0, int enemy_icon_id2 = 0, ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE)
	{
		string icon_under_text = "×" + num.ToString();
		ItemIconDetailSmall itemIconDetailSmall = ItemIcon.CreateIcon<ItemIconDetailSmall>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailSmallPrefab, icon_type, icon_id, rarity, parent, ELEMENT_TYPE.MAX, null, -1, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, false, enemy_icon_id, enemy_icon_id2, false, QUEST_ICON_SIZE_TYPE.DEFAULT, GET_TYPE.PAY);
		itemIconDetailSmall.EquipTypeIconInit(null);
		itemIconDetailSmall.SetEquipIndexSprite(-1);
		itemIconDetailSmall.SetIconStatusSprite(icon_status);
		itemIconDetailSmall.SetupSelectNumberSprite(-1);
		itemIconDetailSmall.SetFavoriteIcon(false);
		return itemIconDetailSmall;
	}

	public static ItemIcon CreateSmallEquipDetailIcon(EquipItemSortData item_data, Transform parent = null, string event_name = null, int event_data = 0, ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE, bool is_new = false, int toggle_group = -1, bool is_select = false, int equip_index = -1)
	{
		ItemIcon itemIcon = _CreateSmallEquipDetailIcon(item_data, parent, event_name, event_data, icon_status, is_new, toggle_group, (!is_select) ? (-1) : 0, equip_index);
		itemIcon.SetFavoriteIcon(item_data.IsFavorite());
		return itemIcon;
	}

	public static ItemIcon CreateSmallEquipSelectDetailIcon(EquipItemSortData item_data, Transform parent = null, string event_name = null, int event_data = 0, ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE, bool is_new = false, int toggle_group = -1, int select_number = -1, int equip_index = -1)
	{
		ItemIcon itemIcon = _CreateSmallEquipDetailIcon(item_data, parent, event_name, event_data, icon_status, is_new, toggle_group, select_number, equip_index);
		itemIcon.SetFavoriteIcon(item_data.IsFavorite());
		return itemIcon;
	}

	private static ItemIcon _CreateSmallEquipDetailIcon(EquipItemSortData item_data, Transform parent = null, string event_name = null, int event_data = 0, ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE, bool is_new = false, int toggle_group = -1, int select_number = -1, int equip_index = -1)
	{
		int sex = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex;
		bool is_equipping = equip_index == 0;
		string icon_under_text = (!item_data.equipData.tableData.IsWeapon()) ? item_data.equipData.def.ToString() : item_data.equipData.atk.ToString();
		EquipItemInfo equipItemInfo = item_data.GetItemData() as EquipItemInfo;
		ItemIconDetailSmall itemIconDetailSmall = CreateEquipItemIconDetailSmall(equipItemInfo, sex, parent, null, -1, event_name, event_data, is_new, toggle_group, select_number > -1, icon_under_text, is_equipping, false);
		itemIconDetailSmall.EquipTypeIconInit(equipItemInfo.tableData);
		itemIconDetailSmall.SetEquipIndexSprite(equip_index - 1);
		itemIconDetailSmall.SetIconStatusSprite(icon_status);
		itemIconDetailSmall.SetupSelectNumberSprite(select_number);
		itemIconDetailSmall.SetFavoriteIcon(equipItemInfo.isFavorite);
		itemIconDetailSmall.SetEquipExt(item_data.equipData);
		return itemIconDetailSmall;
	}

	private static ItemIconDetailSmall CreateEquipItemIconDetailSmall(EquipItemInfo eauipItemInfo, int sex, Transform parent, EQUIPMENT_TYPE? magi_enable_icon_type = default(EQUIPMENT_TYPE?), int num = -1, string event_name = null, int event_data = 0, bool is_new = false, int toggle_group = -1, bool is_select = false, string icon_under_text = null, bool is_equipping = false, bool disable_rarity_text = false)
	{
		return ItemIcon.CreateEquipIconByEquipItemInfo<ItemIconDetailSmall>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailSmallPrefab, eauipItemInfo, sex, parent, magi_enable_icon_type, num, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, disable_rarity_text);
	}

	public static ItemIcon CreateSmithCreateEquipDetailIcon(ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity, SmithCreateSortData item_data, Transform parent = null, string event_name = null, int event_data = 0, ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE, bool is_new = false, int toggle_group = -1, bool is_select = false, bool is_equipping = false, GET_TYPE getType = GET_TYPE.PAY)
	{
		SmithCreateItemInfo smithCreateItemInfo = item_data.GetItemData() as SmithCreateItemInfo;
		string icon_under_text = (!smithCreateItemInfo.equipTableData.IsWeapon()) ? smithCreateItemInfo.equipTableData.baseDef.ToString() : smithCreateItemInfo.equipTableData.baseAtk.ToString();
		ItemIconDetailSmall itemIconDetailSmall = ItemIcon.CreateIcon<ItemIconDetailSmall>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailSmallPrefab, icon_type, icon_id, rarity, parent, smithCreateItemInfo.equipTableData.GetTargetElementPriorityToTable(), null, -1, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, 0, 0, false, QUEST_ICON_SIZE_TYPE.DEFAULT, getType);
		itemIconDetailSmall.EquipTypeIconInit(smithCreateItemInfo.equipTableData);
		itemIconDetailSmall.SetEquipIndexSprite(-1);
		itemIconDetailSmall.SetIconStatusSprite(icon_status);
		itemIconDetailSmall.SetupSelectNumberSprite(-1);
		itemIconDetailSmall.SetFavoriteIcon(false);
		return itemIconDetailSmall;
	}

	public static ItemIcon CreateSmallSkillDetailIcon(ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity, SkillItemSortData item_data, Transform parent = null, string event_name = null, int event_data = 0, bool is_new = false, int toggle_group = -1, bool is_select = false, bool is_equipping = false, bool isValidExceed = false, bool isShowEnableExceed = false)
	{
		ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE;
		if (isShowEnableExceed)
		{
			icon_status = ItemIconDetail.ICON_STATUS.VALID_EXCEED_0;
		}
		else if (isValidExceed)
		{
			icon_status = ItemIconDetail.ICON_STATUS.VALID_EXCEED;
		}
		ItemIcon itemIcon = _CreateSmallSkillDetailIcon(icon_type, icon_id, rarity, item_data, parent, event_name, event_data, is_new, toggle_group, (!is_select) ? (-1) : 0, is_equipping, icon_status);
		itemIcon.SetFavoriteIcon(item_data.IsFavorite());
		return itemIcon;
	}

	public static ItemIcon CreateSmallSkillSelectDetailIcon(ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity, SkillItemSortData item_data, Transform parent = null, string event_name = null, int event_data = 0, bool is_new = false, int toggle_group = -1, int select_number = -1, bool is_equipping = false, ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE)
	{
		ItemIcon itemIcon = _CreateSmallSkillDetailIcon(icon_type, icon_id, rarity, item_data, parent, event_name, event_data, is_new, toggle_group, select_number, is_equipping, icon_status);
		itemIcon.SetFavoriteIcon(item_data.IsFavorite());
		return itemIcon;
	}

	private static ItemIcon _CreateSmallSkillDetailIcon(ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity, SkillItemSortData item_data, Transform parent = null, string event_name = null, int event_data = 0, bool is_new = false, int toggle_group = -1, int select_number = -1, bool is_equipping = false, ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		string format = (!item_data.skillData.IsExceeded()) ? "Lv. {0}/{1}" : ("Lv. {0}/" + UIUtility.GetColorText("{1}", ExceedSkillItemTable.color));
		string icon_under_text = string.Format(format, item_data.GetLevel(), item_data.skillData.GetMaxLevel());
		ItemIconDetailSmall itemIconDetailSmall = ItemIcon.CreateIcon<ItemIconDetailSmall>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailSmallPrefab, icon_type, icon_id, rarity, parent, item_data.GetIconElement(), item_data.skillData.tableData.GetEnableEquipType(), -1, event_name, event_data, is_new, toggle_group, select_number > -1, icon_under_text, is_equipping, 0, 0, false, QUEST_ICON_SIZE_TYPE.DEFAULT, GET_TYPE.PAY);
		itemIconDetailSmall.EquipTypeIconInit(null);
		itemIconDetailSmall.SetEquipIndexSprite(-1);
		itemIconDetailSmall.SetIconStatusSprite(icon_status);
		itemIconDetailSmall.SetupSelectNumberSprite(select_number);
		itemIconDetailSmall.SetFavoriteIcon(item_data.IsFavorite());
		return itemIconDetailSmall;
	}

	public static ItemIcon CreateSmallQuestItemIcon(ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity, Transform parent = null, ELEMENT_TYPE element_type = ELEMENT_TYPE.MAX, int num = -1, string name = null, string event_name = null, int event_data = 0, int toggle_group = -1, bool is_select = false, bool is_new = false)
	{
		string icon_under_text = "×" + num.ToString();
		ItemIconDetailSmall itemIconDetailSmall = ItemIcon.CreateIcon<ItemIconDetailSmall>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailSmallPrefab, icon_type, icon_id, rarity, parent, element_type, null, -1, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, false, 0, 0, false, QUEST_ICON_SIZE_TYPE.DEFAULT, GET_TYPE.PAY);
		itemIconDetailSmall.EquipTypeIconInit(null);
		itemIconDetailSmall.SetEquipIndexSprite(-1);
		itemIconDetailSmall.SetIconStatusSprite(ItemIconDetail.ICON_STATUS.NONE);
		itemIconDetailSmall.SetupSelectNumberSprite(-1);
		itemIconDetailSmall.SetFavoriteIcon(false);
		return itemIconDetailSmall;
	}

	public static ItemIcon CreateSmallListItemIcon(ITEM_ICON_TYPE iconType, EquipItemSortData sortData, Transform parent, bool isNew, int no, GET_TYPE getType)
	{
		EquipItemTable.EquipItemData tableData = sortData.equipData.tableData;
		EquipItemInfo equipItemInfo = sortData.GetItemData() as EquipItemInfo;
		ItemIconDetailSmall itemIconDetailSmall = ItemIcon.CreateIcon<ItemIconDetailSmall>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailSmallPrefab, iconType, tableData.GetIconID(), tableData.rarity, parent, equipItemInfo.GetTargetElementPriorityToTable(), null, -1, string.Empty, 0, isNew, -1, false, "No." + no.ToString("D4"), false, 0, 0, false, QUEST_ICON_SIZE_TYPE.DEFAULT, getType);
		itemIconDetailSmall.EquipTypeIconInit(tableData);
		itemIconDetailSmall.SetEquipIndexSprite(-1);
		itemIconDetailSmall.EquipTypeIconInit(null);
		itemIconDetailSmall.SetIconStatusSprite(ItemIconDetail.ICON_STATUS.NONE);
		itemIconDetailSmall.SetupSelectNumberSprite(-1);
		itemIconDetailSmall.SetFavoriteIcon(false);
		return itemIconDetailSmall;
	}

	public void EquipTypeIconInit(EquipItemTable.EquipItemData equip_table = null)
	{
		if (equip_table == null)
		{
			spriteValueType.set_enabled(false);
		}
		else
		{
			spriteValueType.set_enabled(true);
			spriteValueType.spriteName = ((!equip_table.IsWeapon()) ? ItemIcon.SPR_TYPE_DEF : ItemIcon.SPR_TYPE_ATK);
		}
	}

	private void SetIconStatusSprite(ItemIconDetail.ICON_STATUS icon_status = ItemIconDetail.ICON_STATUS.NONE)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		SetRegistedIcon(false);
		spriteValidEvolve.get_gameObject().SetActive(icon_status == ItemIconDetail.ICON_STATUS.VALID_EVOLVE);
		spGrowMax.set_enabled(icon_status == ItemIconDetail.ICON_STATUS.GROW_MAX);
		spriteGrayOut.set_enabled(icon_status == ItemIconDetail.ICON_STATUS.GRAYOUT || icon_status == ItemIconDetail.ICON_STATUS.NOT_ENOUGH_MATERIAL);
		spriteEnableExceed.get_gameObject().SetActive(icon_status == ItemIconDetail.ICON_STATUS.VALID_EXCEED_0);
		bool flag = icon_status == ItemIconDetail.ICON_STATUS.VALID_EXCEED || icon_status == ItemIconDetail.ICON_STATUS.VALID_EXCEED_0;
		spriteBgSmall[0].get_gameObject().SetActive(!flag);
		spriteBgSmall[1].get_gameObject().SetActive(flag);
	}

	public void SetEquipIndexSprite(int index)
	{
		if (index < 0 || ItemIconDetailEquipSetupper.SPR_EQUIP_INDEX.Length <= index)
		{
			spriteEquipIndex.spriteName = string.Empty;
		}
		else
		{
			spriteEquipIndex.spriteName = ItemIconDetailEquipSetupper.SPR_EQUIP_INDEX[index];
		}
	}

	public void SetupSelectNumberSprite(int select_number = -1)
	{
		if (!(spriteSelectNumber == null))
		{
			int num = select_number - 1;
			if (num >= 0 && num < ItemIconDetailSetuperBase.SPR_SKILL_MATERIAL_NUMBER.Length)
			{
				spriteSelectNumber.set_enabled(true);
				spriteSelectNumber.spriteName = ItemIconDetailSetuperBase.SPR_SKILL_MATERIAL_NUMBER[num];
			}
			else
			{
				spriteSelectNumber.set_enabled(false);
			}
		}
	}

	public void SetRegistedIcon(bool is_visible)
	{
		spRegistedAchievement.set_enabled(is_visible);
	}

	public override void SetGrayout(bool isActive)
	{
		if (spriteGrayOut != null)
		{
			spriteGrayOut.set_enabled(isActive);
		}
	}
}
