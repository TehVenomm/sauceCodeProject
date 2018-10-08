using UnityEngine;

public class ItemIconMaterial : ItemIcon
{
	public UILabel lblHave;

	public UILabel lblNeed;

	public UISprite baseBG;

	public static ItemIcon CreateMaterialIcon(ITEM_ICON_TYPE icon_type, ItemTable.ItemData item_table, Transform parent = null, int have_num = -1, int need_num = -1, string event_name = null, int event_data = 0, bool is_new = false)
	{
		ItemIconMaterial itemIconMaterial = ItemIcon.CreateIcon<ItemIconMaterial>(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.itemIconMaterialPrefab, icon_type, item_table.iconID, item_table.rarity, parent, ELEMENT_TYPE.MAX, null, -1, event_name, event_data, is_new, -1, false, null, false, item_table.enemyIconID, item_table.enemyIconID2, false, QUEST_ICON_SIZE_TYPE.DEFAULT, GET_TYPE.PAY, ELEMENT_TYPE.MAX);
		itemIconMaterial.SetMaterialNum(have_num, need_num);
		itemIconMaterial.SetVisibleBG(true);
		return itemIconMaterial;
	}

	public void SetMaterialNum(int have_num, int need_num)
	{
		UIBehaviour.SetMaterialNumText(lblHave.transform, lblNeed.transform, have_num, need_num);
	}

	public void SetVisibleBG(bool is_visible)
	{
		baseBG.enabled = is_visible;
	}
}
