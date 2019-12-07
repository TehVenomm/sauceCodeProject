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
		ItemIconEquipMaterial itemIconEquipMaterial = ItemIcon.CreateIcon<ItemIconEquipMaterial>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconEquipMaterialPrefab, icon_type, equip_table.GetIconID(), equip_table.rarity, parent, ELEMENT_TYPE.MAX, null, -1, event_name, event_data, is_new, -1, is_select: false, null, is_equipping: false, 0, 0, disable_rarity_text: false, QUEST_ICON_SIZE_TYPE.DEFAULT, getType);
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
		baseBG.enabled = is_visible;
	}

	public void EquipTypeIconInit(EquipItemTable.EquipItemData equip_table = null)
	{
		if (equip_table == null)
		{
			spriteValueType.enabled = false;
			return;
		}
		spriteValueType.enabled = true;
		spriteValueType.spriteName = (equip_table.IsWeapon() ? ItemIcon.SPR_TYPE_ATK : ItemIcon.SPR_TYPE_DEF);
	}

	public void SelectUniqueID(ulong id)
	{
		selectedUniqueID = id;
		if (selectedUniqueID == 0L)
		{
			strUnselect.gameObject.SetActive(value: true);
			lblHave.transform.parent.gameObject.SetActive(value: true);
			lblLv.transform.parent.gameObject.SetActive(value: false);
			spriteValueType.gameObject.SetActive(value: false);
			lblText.text = "0";
			lblLv.text = "0";
			return;
		}
		strUnselect.gameObject.SetActive(value: false);
		lblHave.transform.parent.gameObject.SetActive(value: false);
		lblLv.transform.parent.gameObject.SetActive(value: true);
		spriteValueType.gameObject.SetActive(value: true);
		EquipItemInfo equipItemInfo = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(id);
		if (equipItemInfo != null)
		{
			lblText.text = (equipItemInfo.tableData.IsWeapon() ? equipItemInfo.atk.ToString() : equipItemInfo.def.ToString());
			lblLv.text = equipItemInfo.level.ToString();
		}
	}
}
