using UnityEngine;

public class AbilityChangeAbilityList : UIBehaviour
{
	private enum UI
	{
		STR_ABILITY,
		LBL_NAME,
		LBL_LV_NOW,
		LBL_LV_MAX,
		SPR_TYPE_ICON,
		SPR_TYPE_ICON_BG,
		SPR_TYPE_ICON_RARITY,
		SPR_ABILITY_1,
		SPR_ABILITY_2,
		SPR_ABILITY_3,
		SPR_ABILITY_4,
		SPR_ABILITY_5,
		SPR_ABILITY_6,
		LBL_NAME_1,
		LBL_NAME_2,
		LBL_NAME_3,
		LBL_NAME_4,
		LBL_NAME_5,
		LBL_NAME_6,
		LBL_POINT_1,
		LBL_POINT_2,
		LBL_POINT_3,
		LBL_POINT_4,
		LBL_POINT_5,
		LBL_POINT_6,
		LBL_DESC_1,
		LBL_DESC_2,
		LBL_DESC_3,
		LBL_DESC_4,
		LBL_DESC_5,
		LBL_DESC_6,
		OBJ_ABILITY_ITEM_ITEM_ROOT,
		LBL_NO_ABILITY_ITEM,
		LBL_ABILITY_ITEM_NAME,
		LBL_ABILITY_ITEM_DESC
	}

	private EquipItemInfo equipItemInfo;

	private AbilityItemInfo abilityItemInfo;

	public bool EnableAbilityChange
	{
		get;
		private set;
	}

	public void SetParameter(EquipItemInfo equipItemInfo)
	{
		this.equipItemInfo = equipItemInfo;
		abilityItemInfo = equipItemInfo.GetAbilityItem();
		EnableAbilityChange = true;
		if (!base.uiFirstUpdate)
		{
			RefreshUI();
		}
	}

	public override void UpdateUI()
	{
		if (equipItemInfo != null)
		{
			SetFontStyle(UI.STR_ABILITY, FontStyle.Italic);
			SetFontStyle(UI.LBL_NAME_1, FontStyle.BoldAndItalic);
			SetFontStyle(UI.LBL_NAME_2, FontStyle.BoldAndItalic);
			SetFontStyle(UI.LBL_NAME_3, FontStyle.BoldAndItalic);
			SetLabelText(UI.LBL_NAME, equipItemInfo.tableData.name);
			SetLabelText(UI.LBL_LV_NOW, equipItemInfo.level.ToString());
			SetLabelText(UI.LBL_LV_MAX, equipItemInfo.tableData.maxLv.ToString());
			SetEquipmentTypeIcon(UI.SPR_TYPE_ICON, UI.SPR_TYPE_ICON_BG, UI.SPR_TYPE_ICON_RARITY, equipItemInfo.tableData);
			EquipItemAbility[] validAbility = equipItemInfo.GetValidAbility();
			bool enableAbilityChange = false;
			int num = 0;
			for (num = 0; num < validAbility.Length; num++)
			{
				if (equipItemInfo.IsFixedAbility(num))
				{
					SetAbilityActive(num, false);
					SetAbilityActive(num + 3, true);
					SetAbilityData(num + 3, validAbility[num]);
				}
				else
				{
					enableAbilityChange = true;
					SetAbilityActive(num, true);
					SetAbilityData(num, validAbility[num]);
				}
			}
			EnableAbilityChange = enableAbilityChange;
			for (; num < 3; num++)
			{
				SetAbilityActive(num, false);
			}
			bool flag = abilityItemInfo != null;
			bool flag2 = !equipItemInfo.tableData.IsEquipableAbilityItem() || !flag;
			SetActive(UI.OBJ_ABILITY_ITEM_ITEM_ROOT, flag);
			SetActive(UI.LBL_NO_ABILITY_ITEM, flag2);
			if (flag2)
			{
				if (!equipItemInfo.tableData.IsEquipableAbilityItem())
				{
					SetLabelText(UI.LBL_NO_ABILITY_ITEM, StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 23u));
				}
				else
				{
					SetLabelText(UI.LBL_NO_ABILITY_ITEM, StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 24u));
				}
			}
			else
			{
				SetAbilityItemData();
			}
		}
	}

	private void SetAbilityActive(int index, bool active)
	{
		UI[] array = new UI[6]
		{
			UI.SPR_ABILITY_1,
			UI.SPR_ABILITY_2,
			UI.SPR_ABILITY_3,
			UI.SPR_ABILITY_4,
			UI.SPR_ABILITY_5,
			UI.SPR_ABILITY_6
		};
		if (0 <= index && 5 >= index)
		{
			SetActive(array[index], active);
		}
	}

	private void SetAbilityData(int index, EquipItemAbility ability)
	{
		UI uI = UI.LBL_NAME_1;
		UI uI2 = UI.LBL_POINT_1;
		UI uI3 = UI.LBL_DESC_1;
		UI[] array = new UI[6]
		{
			UI.LBL_NAME_1,
			UI.LBL_NAME_2,
			UI.LBL_NAME_3,
			UI.LBL_NAME_4,
			UI.LBL_NAME_5,
			UI.LBL_NAME_6
		};
		UI[] array2 = new UI[6]
		{
			UI.LBL_POINT_1,
			UI.LBL_POINT_2,
			UI.LBL_POINT_3,
			UI.LBL_POINT_4,
			UI.LBL_POINT_5,
			UI.LBL_POINT_6
		};
		UI[] array3 = new UI[6]
		{
			UI.LBL_DESC_1,
			UI.LBL_DESC_2,
			UI.LBL_DESC_3,
			UI.LBL_DESC_4,
			UI.LBL_DESC_5,
			UI.LBL_DESC_6
		};
		if (0 <= index && 5 >= index)
		{
			uI = array[index];
			uI2 = array2[index];
			uI3 = array3[index];
		}
		AbilityDataTable.AbilityData abilityData = Singleton<AbilityDataTable>.I.GetAbilityData(ability.id, ability.ap);
		if (abilityData == null)
		{
			abilityData = Singleton<AbilityDataTable>.I.GetMinimumAbilityData(ability.id);
		}
		SetLabelText(uI, ability.GetName());
		SetLabelText(uI2, ability.GetAP());
		SetLabelText(uI3, abilityData.description);
	}

	private void SetAbilityItemData()
	{
		if (abilityItemInfo != null)
		{
			SetLabelText(UI.LBL_ABILITY_ITEM_NAME, abilityItemInfo.GetName());
			SetLabelText(UI.LBL_ABILITY_ITEM_DESC, abilityItemInfo.GetDescription());
		}
	}
}
