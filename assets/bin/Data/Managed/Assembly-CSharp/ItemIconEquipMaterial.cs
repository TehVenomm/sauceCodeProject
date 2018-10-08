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
		ItemIconEquipMaterial itemIconEquipMaterial = ItemIcon.CreateIcon<ItemIconEquipMaterial>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconEquipMaterialPrefab, icon_type, equip_table.GetIconID(), equip_table.rarity, parent, ELEMENT_TYPE.MAX, null, -1, event_name, event_data, is_new, -1, false, null, false, 0, 0, false, QUEST_ICON_SIZE_TYPE.DEFAULT, getType, ELEMENT_TYPE.MAX);
		itemIconEquipMaterial.EquipTypeIconInit(equip_table);
		itemIconEquipMaterial.SetMaterialNum(have_num);
		itemIconEquipMaterial.SetVisibleBG(true);
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
		}
		else
		{
			spriteValueType.set_enabled(true);
			spriteValueType.spriteName = ((!equip_table.IsWeapon()) ? ItemIcon.SPR_TYPE_DEF : ItemIcon.SPR_TYPE_ATK);
		}
	}

	public void SelectUniqueID(ulong id)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		selectedUniqueID = id;
		if (selectedUniqueID == 0L)
		{
			strUnselect.get_gameObject().SetActive(true);
			lblHave.get_transform().get_parent().get_gameObject()
				.SetActive(true);
			lblLv.get_transform().get_parent().get_gameObject()
				.SetActive(false);
			spriteValueType.get_gameObject().SetActive(false);
			lblText.text = "0";
			lblLv.text = "0";
		}
		else
		{
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
}
