using UnityEngine;

public class ItemDetailAccessory : SkillInfoBase
{
	protected enum UI
	{
		OBJ_DETAIL_ROOT,
		TEX_MODEL,
		LBL_NAME,
		LBL_DESCRIPTION,
		SPR_SKILL_TYPE_ICON,
		SPR_SKILL_TYPE_ICON_BG,
		SPR_SKILL_TYPE_ICON_RARITY
	}

	protected ItemDetailEquip.CURRENT_SECTION callSection;

	protected AccessoryInfo info;

	protected Transform detailBase;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		callSection = (ItemDetailEquip.CURRENT_SECTION)array[0];
		AccessorySortData accessorySortData = array[1] as AccessorySortData;
		if (accessorySortData != null)
		{
			info = accessorySortData.itemData;
			GameSaveData.instance.RemoveNewIconAndSave(ITEM_ICON_TYPE.ACCESSORY, accessorySortData.GetUniqID());
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		detailBase = SetPrefab(GetCtrl(UI.OBJ_DETAIL_ROOT), "ItemDetailAccessoryBase");
		if (!(detailBase == null))
		{
			SetLabelText(detailBase, UI.LBL_NAME, info.tableData.name);
			SetLabelText(detailBase, UI.LBL_DESCRIPTION, info.tableData.descriptPart);
			SetAccessoryRarityIcon(FindCtrl(detailBase, UI.SPR_SKILL_TYPE_ICON_BG), FindCtrl(detailBase, UI.SPR_SKILL_TYPE_ICON_RARITY), info.tableData);
			SetRenderAccessoryModel(UI.TEX_MODEL, info.tableData.accessoryId, info.tableData.detailScale);
		}
	}
}
