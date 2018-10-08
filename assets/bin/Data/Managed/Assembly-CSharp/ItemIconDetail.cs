using UnityEngine;

public class ItemIconDetail : ItemIcon
{
	public enum ICON_STATUS
	{
		NONE,
		NOT_ENOUGH_MATERIAL,
		VALID_EVOLVE,
		GRAYOUT,
		GROW_MAX,
		VALID_EXCEED_0,
		VALID_EXCEED
	}

	public ItemIconDetailEquipSetupper setupperEquip;

	public ItemIconDetailEquipAbilitySetupper setupperEquipAbility;

	public ItemIconDetailSkillSetupper setupperSkill;

	public ItemIconDetailMaterialSetupper setupperMaterial;

	public ItemIconDetailQuestItemSetupper setupperQuestItem;

	public ItemIconDetailAccessorySetupper setupperAccessory;

	public ItemIconDetailRemoveBtnSetupper setupperRemoveBtn;

	public UISprite spriteGrayout;

	public static ItemIcon CreateRemoveButton(Transform parent = null, string event_name = null, int event_data = 0, int toggle_group = -1, bool is_select = false, string name = null)
	{
		ItemIconDetail itemIconDetail = ItemIcon.CreateIcon<ItemIconDetail>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailPrefab, ITEM_ICON_TYPE.NONE, ItemIcon.GetRemoveButtonIconID(), null, parent, ELEMENT_TYPE.MAX, null, -1, event_name, event_data, false, toggle_group, is_select, null, false, 0, 0, false, QUEST_ICON_SIZE_TYPE.DEFAULT, GET_TYPE.PAY, ELEMENT_TYPE.MAX);
		itemIconDetail.setupperRemoveBtn.Set(new object[1]
		{
			name
		});
		itemIconDetail.SetFavoriteIcon(false);
		return itemIconDetail;
	}

	public static ItemIcon CreateMaterialIcon(ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity, ItemTable.ItemData item_table, bool is_show_main_status, Transform parent = null, int num = -1, string name = null, string event_name = null, int event_data = 0, int toggle_group = -1, bool is_select = false, bool is_new = false)
	{
		ItemIconDetail itemIconDetail = ItemIcon.CreateIcon<ItemIconDetail>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailPrefab, icon_type, icon_id, rarity, parent, ELEMENT_TYPE.MAX, null, -1, event_name, event_data, is_new, toggle_group, is_select, string.Empty, false, item_table.enemyIconID, item_table.enemyIconID2, false, QUEST_ICON_SIZE_TYPE.DEFAULT, GET_TYPE.PAY, ELEMENT_TYPE.MAX);
		itemIconDetail.setupperMaterial.Set(new object[3]
		{
			item_table,
			num,
			is_show_main_status
		});
		return itemIconDetail;
	}

	public static ItemIcon CreateEquipDetailIcon(EquipItemSortData item_data, SkillSlotUIData[] skill_slot_data, bool is_show_main_status, Transform parent = null, string event_name = null, int event_data = 0, ICON_STATUS icon_status = ICON_STATUS.NONE, bool is_new = false, int toggle_group = -1, bool is_select = false, int equipping_sp_index = -1)
	{
		ItemIcon itemIcon = _CreateEquipDetailIcon(item_data, skill_slot_data, is_show_main_status, parent, event_name, event_data, icon_status, is_new, toggle_group, (!is_select) ? (-1) : 0, equipping_sp_index);
		itemIcon.SetFavoriteIcon(item_data.IsFavorite());
		return itemIcon;
	}

	public static ItemIcon CreateEquipDetailSelectNumberIcon(EquipItemSortData item_data, SkillSlotUIData[] skill_slot_data, bool is_show_main_status, Transform parent = null, string event_name = null, int event_data = 0, ICON_STATUS icon_status = ICON_STATUS.NONE, bool is_new = false, int toggle_group = -1, int select_number = -1, int equipping_sp_index = -1, GET_TYPE getType = GET_TYPE.PAY)
	{
		ItemIcon itemIcon = _CreateEquipDetailIcon(item_data, skill_slot_data, is_show_main_status, parent, event_name, event_data, icon_status, is_new, toggle_group, select_number, equipping_sp_index);
		itemIcon.SetFavoriteIcon(item_data.IsFavorite());
		return itemIcon;
	}

	private static ItemIcon _CreateEquipDetailIcon(EquipItemSortData item_data, SkillSlotUIData[] skill_slot_data, bool is_show_main_status, Transform parent = null, string event_name = null, int event_data = 0, ICON_STATUS icon_status = ICON_STATUS.NONE, bool is_new = false, int toggle_group = -1, int select_number = -1, int equipping_sp_index = -1)
	{
		int sex = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex;
		bool is_equipping = equipping_sp_index == 0;
		EquipItemInfo equipItemInfo = item_data.GetItemData() as EquipItemInfo;
		ItemIconDetail itemIconDetail = CreateEquipItemIconDetail(equipItemInfo, sex, parent, null, -1, event_name, event_data, is_new, toggle_group, select_number > -1, string.Empty, is_equipping, false);
		itemIconDetail.setupperEquip.Set(new object[6]
		{
			item_data.GetItemData() as EquipItemInfo,
			skill_slot_data,
			is_show_main_status,
			icon_status,
			equipping_sp_index,
			select_number
		});
		itemIconDetail.SetFavoriteIcon(item_data.IsFavorite());
		if ((bool)itemIconDetail.setupperEquip.lvRoot)
		{
			UILabel[] componentsInChildren = itemIconDetail.setupperEquip.lvRoot.GetComponentsInChildren<UILabel>();
			itemIconDetail.SetEquipExt(item_data.equipData, componentsInChildren);
		}
		return itemIconDetail;
	}

	public static ItemIcon CreateSmithCreateEquipDetailIcon(ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity, SmithCreateSortData item_data, SkillSlotUIData[] skill_slot_data, bool is_show_main_status, Transform parent = null, string event_name = null, int event_data = 0, ICON_STATUS icon_status = ICON_STATUS.NONE, bool is_new = false, int toggle_group = -1, bool is_select = false, bool is_equipping = false, GET_TYPE getType = GET_TYPE.PAY)
	{
		SmithCreateItemInfo smithCreateItemInfo = item_data.GetItemData() as SmithCreateItemInfo;
		ItemIconDetail itemIconDetail = ItemIcon.CreateIcon<ItemIconDetail>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailPrefab, icon_type, icon_id, rarity, parent, smithCreateItemInfo.equipTableData.GetTargetElementPriorityToTable(), null, -1, event_name, event_data, is_new, toggle_group, is_select, string.Empty, is_equipping, 0, 0, false, QUEST_ICON_SIZE_TYPE.DEFAULT, getType, ELEMENT_TYPE.MAX);
		EquipItemTable.EquipItemData equipTableData = item_data.createData.equipTableData;
		itemIconDetail.setupperEquip.Set(new object[6]
		{
			equipTableData,
			skill_slot_data,
			is_show_main_status,
			icon_status,
			-1,
			-1
		});
		return itemIconDetail;
	}

	public static ItemIcon CreateEquipAbilityIcon(ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity, EquipItemSortData item_data, SkillSlotUIData[] skill_slot_data, bool is_show_main_status, Transform parent = null, string event_name = null, int event_data = 0, ICON_STATUS icon_status = ICON_STATUS.NONE, bool is_new = false, int toggle_group = -1, bool is_select = false, int equipping_sp_index = -1, GET_TYPE getType = GET_TYPE.PAY)
	{
		bool is_equipping = equipping_sp_index == 0;
		EquipItemInfo equipItemInfo = item_data.GetItemData() as EquipItemInfo;
		ItemIconDetail itemIconDetail = ItemIcon.CreateIcon<ItemIconDetail>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailPrefab, icon_type, icon_id, rarity, parent, equipItemInfo.GetTargetElementPriorityToTable(), null, -1, event_name, event_data, is_new, toggle_group, false, string.Empty, is_equipping, 0, 0, false, QUEST_ICON_SIZE_TYPE.DEFAULT, getType, ELEMENT_TYPE.MAX);
		itemIconDetail.setupperEquipAbility.Set(new object[5]
		{
			item_data.GetItemData() as EquipItemInfo,
			skill_slot_data,
			is_show_main_status,
			icon_status,
			equipping_sp_index
		});
		itemIconDetail.SetFavoriteIcon(item_data.IsFavorite());
		if ((bool)itemIconDetail.setupperEquip.lvRoot)
		{
			UILabel[] componentsInChildren = itemIconDetail.setupperEquipAbility.lvRoot.GetComponentsInChildren<UILabel>();
			itemIconDetail.SetEquipExt(item_data.equipData, componentsInChildren);
		}
		return itemIconDetail;
	}

	public static ItemIcon CreateEquipRevertLithographIcon(ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity, EquipItemSortData item_data, SkillSlotUIData[] skill_slot_data, bool is_show_main_status, Transform parent = null, string event_name = null, int event_data = 0, ICON_STATUS icon_status = ICON_STATUS.NONE, bool is_new = false, int toggle_group = -1, bool is_select = false, int equipping_sp_index = -1, GET_TYPE getType = GET_TYPE.PAY)
	{
		bool is_equipping = equipping_sp_index == 0;
		EquipItemInfo equipItemInfo = item_data.GetItemData() as EquipItemInfo;
		ItemIconDetail itemIconDetail = ItemIcon.CreateIcon<ItemIconDetail>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailPrefab, icon_type, icon_id, rarity, parent, equipItemInfo.GetTargetElementPriorityToTable(), null, -1, event_name, event_data, is_new, toggle_group, false, string.Empty, is_equipping, 0, 0, false, QUEST_ICON_SIZE_TYPE.DEFAULT, getType, ELEMENT_TYPE.MAX);
		itemIconDetail.setupperEquipAbility.Set(new object[5]
		{
			item_data.GetItemData() as EquipItemInfo,
			skill_slot_data,
			is_show_main_status,
			icon_status,
			equipping_sp_index
		});
		itemIconDetail.SetFavoriteIcon(item_data.IsFavorite());
		if ((bool)itemIconDetail.setupperEquip.lvRoot)
		{
			UILabel[] componentsInChildren = itemIconDetail.setupperEquipAbility.lvRoot.GetComponentsInChildren<UILabel>();
			itemIconDetail.SetEquipExt(item_data.equipData, componentsInChildren);
		}
		return itemIconDetail;
	}

	public static ItemIcon CreateSkillDetailIcon(ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity, SkillItemSortData item_data, bool is_show_main_status, Transform parent = null, string event_name = null, int event_data = 0, bool is_new = false, int toggle_group = -1, bool is_select = false, bool is_equipping = false, bool isValidExceed = false, bool isShowEnableExceed = false)
	{
		ICON_STATUS icon_status = ICON_STATUS.NONE;
		if (isShowEnableExceed)
		{
			icon_status = ICON_STATUS.VALID_EXCEED_0;
		}
		else if (isValidExceed)
		{
			icon_status = ICON_STATUS.VALID_EXCEED;
		}
		ItemIcon itemIcon = _CreateSkillDetailIcon(icon_type, icon_id, rarity, item_data, is_show_main_status, parent, event_name, event_data, icon_status, is_new, toggle_group, (!is_select) ? (-1) : 0, is_equipping);
		itemIcon.SetFavoriteIcon(item_data.IsFavorite());
		return itemIcon;
	}

	public static ItemIcon CreateSkillDetailSelectNumberIcon(ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity, SkillItemSortData item_data, bool is_show_main_status, Transform parent = null, string event_name = null, int event_data = 0, bool is_new = false, int toggle_group = -1, int select_number = -1, bool is_equipping = false, ICON_STATUS icon_status = ICON_STATUS.NONE)
	{
		ItemIcon itemIcon = _CreateSkillDetailIcon(icon_type, icon_id, rarity, item_data, is_show_main_status, parent, event_name, event_data, icon_status, is_new, toggle_group, select_number, is_equipping);
		itemIcon.SetFavoriteIcon(item_data.IsFavorite());
		return itemIcon;
	}

	private static ItemIcon _CreateSkillDetailIcon(ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity, SkillItemSortData item_data, bool is_show_main_status, Transform parent = null, string event_name = null, int event_data = 0, ICON_STATUS icon_status = ICON_STATUS.NONE, bool is_new = false, int toggle_group = -1, int select_number = -1, bool is_equipping = false)
	{
		ItemIconDetail itemIconDetail = ItemIcon.CreateIcon<ItemIconDetail>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailPrefab, icon_type, icon_id, rarity, parent, item_data.GetIconElement(), item_data.skillData.tableData.GetEnableEquipType(), -1, event_name, event_data, is_new, toggle_group, select_number > -1, string.Empty, is_equipping, 0, 0, false, QUEST_ICON_SIZE_TYPE.DEFAULT, GET_TYPE.PAY, item_data.GetIconElementSub());
		itemIconDetail.setupperSkill.Set(new object[4]
		{
			item_data,
			is_show_main_status,
			select_number,
			icon_status
		});
		itemIconDetail.SetFavoriteIcon(item_data.IsFavorite());
		return itemIconDetail;
	}

	public static ItemIcon CreateQuestItemIcon(ITEM_ICON_TYPE icon_type, int icon_id, RARITY_TYPE? rarity, QuestSortData quest_item, bool is_show_main_status, Transform parent = null, int num = -1, string name = null, string event_name = null, int event_data = 0, int toggle_group = -1, bool is_select = false, bool is_new = false)
	{
		ItemIconDetail itemIconDetail = ItemIcon.CreateIcon<ItemIconDetail>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailPrefab, icon_type, icon_id, rarity, parent, quest_item.GetEnemyElement(), null, -1, event_name, event_data, is_new, toggle_group, is_select, string.Empty, false, 0, 0, false, QUEST_ICON_SIZE_TYPE.DEFAULT, GET_TYPE.PAY, ELEMENT_TYPE.MAX);
		itemIconDetail.setupperQuestItem.Set(new object[2]
		{
			quest_item,
			is_show_main_status
		});
		return itemIconDetail;
	}

	public static ItemIconDetail CreateEquipItemIconDetail(EquipItemInfo equipItemInfo, int sex, Transform parent, EQUIPMENT_TYPE? magi_enable_icon_type = default(EQUIPMENT_TYPE?), int num = -1, string event_name = null, int event_data = 0, bool is_new = false, int toggle_group = -1, bool is_select = false, string icon_under_text = null, bool is_equipping = false, bool disable_rarity_text = false)
	{
		return ItemIcon.CreateEquipIconByEquipItemInfo<ItemIconDetail>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailPrefab, equipItemInfo, sex, parent, magi_enable_icon_type, num, event_name, event_data, is_new, toggle_group, is_select, icon_under_text, is_equipping, disable_rarity_text);
	}

	public static ItemIcon CreateAccessoryIcon(AccessoryTable.AccessoryData data, Transform _parent, string _eventName, int _eventData, bool _isNew, bool _isEquipping)
	{
		ItemIconDetail itemIconDetail = ItemIcon.CreateAccessoryIcon(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconDetailPrefab, (int)data.accessoryId, data.rarity, _parent, _eventName, _eventData, _isNew, _isEquipping, data.getType);
		itemIconDetail.setupperAccessory.Set(new object[1]
		{
			data
		});
		return itemIconDetail;
	}

	public override void SetGrayout(bool isActive)
	{
		if ((Object)spriteGrayout != (Object)null)
		{
			spriteGrayout.enabled = isActive;
		}
	}
}
