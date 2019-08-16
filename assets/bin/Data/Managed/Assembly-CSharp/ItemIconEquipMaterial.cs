using UnityEngine;

public class ItemIconEquipMaterial : ItemIcon
{
	public UILabel strUnselect;

	public UILabel lblHave;

	public UISprite baseBG;

	public UILabel lblLv;

	public UISprite spriteValueType;

	public UILabel lblText;

	public ulong selectedUniqueID;

	public static ItemIconEquipMaterial CreateEquipMaterialIcon(ITEM_ICON_TYPE icon_type, EquipItemTable.EquipItemData equip_table, Transform parent = null, int have_num = -1, int need_num = -1, string event_name = null, int event_data = 0, bool is_new = false, GET_TYPE getType = GET_TYPE.PAY)
	{
		GameObject itemIconEquipMaterialPrefab = MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconEquipMaterialPrefab;
		int iconID = equip_table.GetIconID();
		RARITY_TYPE? rarity = equip_table.rarity;
		ELEMENT_TYPE element = ELEMENT_TYPE.MAX;
		EQUIPMENT_TYPE? magi_enable_icon_type = null;
		int num = -1;
		bool is_new2 = is_new;
		int toggle_group = -1;
		bool is_select = false;
		string icon_under_text = null;
		bool is_equipping = false;
		ItemIconEquipMaterial itemIconEquipMaterial = ItemIcon.CreateIcon<ItemIconEquipMaterial>(itemIconEquipMaterialPrefab, icon_type, iconID, rarity, parent, element, magi_enable_icon_type, num, event_name, event_data, is_new2, toggle_group, is_select, icon_under_text, is_equipping, 0, 0, disable_rarity_text: false, QUEST_ICON_SIZE_TYPE.DEFAULT, getType);
		itemIconEquipMaterial.EquipTypeIconInit(equip_table);
		itemIconEquipMaterial.SetMaterialNum(have_num);
		itemIconEquipMaterial.SetVisibleBG(is_visible: true);
		itemIconEquipMaterial.SelectUniqueID(0uL);
		return itemIconEquipMaterial;
	}

	public void SetMaterialNum(int have_num)
	{
		lblHave.text = have_num.ToString();
	}

	public void SetVisibleBG(bool is_visible)
	{
		baseBG.set_enabled(is_visible);
	}

	public void EquipTypeIconInit(EquipItemTable.EquipItemData equip_table = null)
	{
		if (equip_table == null)
		{
			spriteValueType.set_enabled(false);
			return;
		}
		spriteValueType.set_enabled(true);
		spriteValueType.spriteName = ((!equip_table.IsWeapon()) ? ItemIcon.SPR_TYPE_DEF : ItemIcon.SPR_TYPE_ATK);
	}

	public void SelectUniqueID(ulong id)
	{
		selectedUniqueID = id;
		if (selectedUniqueID == 0)
		{
			strUnselect.get_gameObject().SetActive(true);
			lblHave.get_transform().get_parent().get_gameObject()
				.SetActive(true);
			lblLv.get_transform().get_parent().get_gameObject()
				.SetActive(false);
			spriteValueType.get_gameObject().SetActive(false);
			lblText.text = "0";
			lblLv.text = "0";
			return;
		}
		strUnselect.get_gameObject().SetActive(false);
		lblHave.get_transform().get_parent().get_gameObject()
			.SetActive(false);
		lblLv.get_transform().get_parent().get_gameObject()
			.SetActive(true);
		spriteValueType.get_gameObject().SetActive(true);
		EquipItemInfo equipItemInfo = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(id);
		if (equipItemInfo != null)
		{
			lblText.text = ((!equipItemInfo.tableData.IsWeapon()) ? equipItemInfo.def.ToString() : equipItemInfo.atk.ToString());
			lblLv.text = equipItemInfo.level.ToString();
		}
	}
}
