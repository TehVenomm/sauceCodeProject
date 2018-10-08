public class QuestResultDropItemDetail : GameSection
{
	private enum UI
	{
		LBL_ITEM_NAME,
		OBJ_ITEM,
		LBL_TEXT,
		OBJ_EQUIP,
		LBL_ATK,
		LBL_DEF,
		LBL_SKILL_A,
		LBL_SKILL_B,
		LBL_SKILL_C
	}

	protected SortCompareData iconData;

	public override void Initialize()
	{
		iconData = (SortCompareData)GameSection.GetEventData();
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		if (iconData != null)
		{
			if (iconData is ItemSortData)
			{
				SetActive(UI.OBJ_EQUIP, false);
				SetActive(UI.OBJ_ITEM, true);
				ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(iconData.GetTableID());
				SetLabelText(UI.LBL_ITEM_NAME, iconData.GetName());
				SetLabelText(UI.LBL_TEXT, itemData.text);
			}
			else if (iconData is EquipItemSortData)
			{
				SetActive(UI.OBJ_EQUIP, true);
				SetActive(UI.OBJ_ITEM, false);
				EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(iconData.GetTableID());
				SetLabelText(UI.LBL_ITEM_NAME, equipItemData.name);
				SetLabelText(UI.LBL_ATK, equipItemData.baseAtk.ToString());
				SetLabelText(UI.LBL_DEF, equipItemData.baseDef.ToString());
				int fixedSkillLength = equipItemData.fixedSkillLength;
				UI[] array = new UI[3]
				{
					UI.LBL_SKILL_A,
					UI.LBL_SKILL_B,
					UI.LBL_SKILL_C
				};
				for (int i = 0; i < 3; i++)
				{
					if (i < fixedSkillLength)
					{
						SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(equipItemData.GetSkillSlot(0)[i].skill_id);
						if (skillItemData != null)
						{
							SetActive(array[i], true);
							SetLabelText(array[i], skillItemData.name);
						}
						else
						{
							SetActive(array[i], false);
						}
					}
					else
					{
						SetActive(array[i], false);
					}
				}
			}
			else
			{
				SetActive(UI.OBJ_EQUIP, true);
				SetActive(UI.OBJ_ITEM, false);
				SkillItemTable.SkillItemData skillItemData2 = Singleton<SkillItemTable>.I.GetSkillItemData(iconData.GetTableID());
				SetLabelText(UI.LBL_ITEM_NAME, skillItemData2.name);
				SetLabelText(UI.LBL_ATK, skillItemData2.baseAtk.ToString());
				SetLabelText(UI.LBL_DEF, skillItemData2.baseDef.ToString());
				SetLabelText(UI.LBL_SKILL_A, string.Empty);
				SetLabelText(UI.LBL_SKILL_B, string.Empty);
				SetLabelText(UI.LBL_SKILL_C, string.Empty);
			}
		}
	}
}
