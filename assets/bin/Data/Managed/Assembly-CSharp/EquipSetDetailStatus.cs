using System;
using UnityEngine;

public class EquipSetDetailStatus : EquipSetDetailStatusAndAbilityTable
{
	protected new enum UI
	{
		GRD_ABILITY,
		SCR_ABILITY,
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		LBL_ATK_ELEM_FIRE,
		LBL_ATK_ELEM_WATER,
		LBL_ATK_ELEM_THUNDER,
		LBL_ATK_ELEM_EARTH,
		LBL_ATK_ELEM_LIGHT,
		LBL_ATK_ELEM_DARK,
		LBL_ATK_ELEM_NONE,
		LBL_DEF_ELEM_FIRE,
		LBL_DEF_ELEM_WATER,
		LBL_DEF_ELEM_THUNDER,
		LBL_DEF_ELEM_EARTH,
		LBL_DEF_ELEM_LIGHT,
		LBL_DEF_ELEM_DARK,
		LBL_DEF_ELEM_NONE,
		TGL_STATUS_WINDOW_INDEX0,
		TGL_STATUS_WINDOW_INDEX1,
		TGL_STATUS_WINDOW_INDEX2,
		TGL_WINDOW_ICON_INDEX0,
		TGL_WINDOW_ICON_INDEX1,
		TGL_WINDOW_ICON_INDEX2,
		TGL_BUTTON_INDEX0,
		TGL_BUTTON_INDEX1,
		TGL_BUTTON_INDEX2,
		OBJ_EQUIP_BTN_ROOT_ACTIVE,
		OBJ_EQUIP_BTN_ROOT_INACTIVE,
		SPR_BG0,
		SPR_BG1,
		SPR_BG2,
		SPR_BG3,
		SPR_BG4,
		SPR_BG5,
		SPR_BG6,
		SPR_BG7,
		SPR_BG8,
		SPR_BG9,
		OBJ_DETAIL_ROOT,
		OBJ_ABILITY_ITEM_ROOT,
		OBJ_ABILITY_ITEM_ITEM_ROOT,
		BTN_ABILITY,
		LBL_ABILITY_NAME,
		LBL_AP_0,
		LBL_AP_1,
		LBL_AP_2,
		LBL_AP_3,
		LBL_AP_4,
		LBL_AP_5,
		LBL_AP_6,
		LBL_AP_TOTAL,
		SPR_NAME_TAG,
		SPR_NAME_TAG_OFF,
		TGL_BG,
		TGL_NAME_TAG,
		ICON_WEAPON,
		LBL_NAME,
		LBL_LV_NOW,
		LBL_LV_MAX,
		SPR_TYPE_ICON,
		SPR_TYPE_ICON_BG,
		SPR_TYPE_ICON_RARITY,
		OBJ_CAPTION_3,
		LBL_CAPTION
	}

	public override void Initialize()
	{
		InitializeCaption();
		base.Initialize();
	}

	public override void UpdateUI()
	{
		UpdateUIStatus();
		base.UpdateUI();
		UpdateWeaponIcon();
	}

	protected override void OnQuery_INDEX_L()
	{
		base.OnQuery_INDEX_L();
		UpdateWeaponIcon();
	}

	protected override void OnQuery_INDEX_R()
	{
		base.OnQuery_INDEX_R();
		UpdateWeaponIcon();
	}

	private void UpdateWeaponIcon()
	{
		int sex = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex;
		EquipItemInfo equipItemInfo = equipSet.item[selectEquipIndex];
		ItemIcon itemIcon = ItemIcon.CreateEquipItemIconByEquipItemInfo(equipItemInfo, sex, GetCtrl(UI.ICON_WEAPON), null, -1, "DETAIL", 0, false, -1, false, null, false, false);
		if (equipItemInfo != null && equipItemInfo.tableID != 0)
		{
			itemIcon.SetEquipExt(equipItemInfo, base.GetComponent<UILabel>((Enum)UI.LBL_LV_NOW));
		}
		SetLabelText((Enum)UI.LBL_NAME, equipItemInfo.tableData.name);
		SetLabelText((Enum)UI.LBL_LV_NOW, equipItemInfo.level.ToString());
		SetLabelText((Enum)UI.LBL_LV_MAX, equipItemInfo.tableData.maxLv.ToString());
		Transform ctrl = GetCtrl(UI.SPR_TYPE_ICON_BG);
		Transform t_icon = FindCtrl(ctrl, UI.SPR_TYPE_ICON);
		Transform val = FindCtrl(ctrl, UI.SPR_TYPE_ICON_RARITY);
		SetEquipmentTypeIcon(t_icon, ctrl, val, equipItemInfo.tableData);
		SetActive(val, false);
		SetEvent(GetCtrl(UI.ICON_WEAPON), "DETAIL", selectEquipIndex);
	}

	private void OnQuery_DETAIL()
	{
		EquipItemInfo equipItemInfo = equipSet.item[selectEquipIndex];
		GameSection.SetEventData(new object[2]
		{
			ItemDetailEquip.CURRENT_SECTION.EQUIP_SET_DETAIL_STATUS,
			equipItemInfo
		});
	}

	protected virtual void OnQuery_TO_ABILITY()
	{
		GameSection.SetEventData(currentEventData);
	}

	private void InitializeCaption()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Transform ctrl = GetCtrl(UI.OBJ_CAPTION_3);
		string text = base.sectionData.GetText("CAPTION");
		SetLabelText(ctrl, UI.LBL_CAPTION, text);
		UITweenCtrl component = ctrl.get_gameObject().GetComponent<UITweenCtrl>();
		if (component != null)
		{
			component.Reset();
			int i = 0;
			for (int num = component.tweens.Length; i < num; i++)
			{
				component.tweens[i].ResetToBeginning();
			}
			component.Play(true, null);
		}
	}
}
