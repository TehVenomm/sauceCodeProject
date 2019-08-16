using Network;
using System;
using UnityEngine;

public class SmithUseAbilityItemDetail : GameSection
{
	private enum UI
	{
		LBL_NAME,
		LBL_LV_NOW,
		LBL_LV_MAX,
		SPR_TYPE_ICON,
		SPR_TYPE_ICON_BG,
		SPR_TYPE_ICON_RARITY,
		OBJ_BEFORE_ITEM_ROOT,
		OBJ_AFTER_ITEM_ROOT,
		LBL_ABILITY_ITEM_NAME,
		LBL_ABILITY_ITEM_DESC
	}

	private enum BACK_TO
	{
		STATUS_TOP,
		STATUS_TOP_EQUIPDETAIL
	}

	private EquipItemInfo equipItemInfo;

	private AbilityItemSortData abilityItemInfo;

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_USER_STATUS;
	}

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		if (array[0] is EquipItemInfo)
		{
			equipItemInfo = (array[0] as EquipItemInfo);
		}
		if (array[1] is AbilityItemSortData)
		{
			abilityItemInfo = (array[1] as AbilityItemSortData);
		}
		base.Initialize();
	}

	public override void InitializeReopen()
	{
		equipItemInfo = (GameSection.GetEventData() as EquipItemInfo);
		base.InitializeReopen();
	}

	public override void UpdateUI()
	{
		SetLabelText((Enum)UI.LBL_NAME, equipItemInfo.tableData.name);
		SetLabelText((Enum)UI.LBL_LV_NOW, equipItemInfo.level.ToString());
		SetLabelText((Enum)UI.LBL_LV_MAX, equipItemInfo.tableData.maxLv.ToString());
		SetEquipmentTypeIcon((Enum)UI.SPR_TYPE_ICON, (Enum)UI.SPR_TYPE_ICON_BG, (Enum)UI.SPR_TYPE_ICON_RARITY, equipItemInfo.tableData);
		Transform ctrl = GetCtrl(UI.OBJ_BEFORE_ITEM_ROOT);
		Transform ctrl2 = GetCtrl(UI.OBJ_AFTER_ITEM_ROOT);
		AbilityItemInfo abilityItem = equipItemInfo.GetAbilityItem();
		if (abilityItem == null)
		{
			SetLabelText(ctrl, UI.LBL_ABILITY_ITEM_NAME, string.Empty);
			SetLabelText(ctrl, UI.LBL_ABILITY_ITEM_DESC, StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 28u));
		}
		else
		{
			SetLabelText(ctrl, UI.LBL_ABILITY_ITEM_NAME, abilityItem.GetName());
			SetLabelText(ctrl, UI.LBL_ABILITY_ITEM_DESC, abilityItem.GetDescription());
		}
		SetLabelText(ctrl2, UI.LBL_ABILITY_ITEM_NAME, abilityItemInfo.GetName());
		SetLabelText(ctrl2, UI.LBL_ABILITY_ITEM_DESC, abilityItemInfo.itemData.GetDescription());
	}

	private void OnQuery_START()
	{
		GameSection.SetEventData(new object[2]
		{
			abilityItemInfo.GetName(),
			equipItemInfo.tableData.name
		});
	}

	private void OnQuery_SmithConfirmAbilityItem_YES()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<SmithManager>.I.SendUseAbilityItem(equipItemInfo.uniqueID, abilityItemInfo.GetUniqID(), delegate(Error error, EquipItemInfo itemInfo)
		{
			GameSection.ResumeEvent(error == Error.None);
		});
	}

	private void OnQuery_SEND()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<SmithManager>.I.SendUseAbilityItem(equipItemInfo.uniqueID, abilityItemInfo.GetUniqID(), delegate(Error error, EquipItemInfo itemInfo)
		{
			GameSection.ResumeEvent(error != Error.None);
		});
	}
}
