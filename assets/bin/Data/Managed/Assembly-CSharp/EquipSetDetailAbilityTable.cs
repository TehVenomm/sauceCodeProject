using System.Collections.Generic;
using UnityEngine;

public class EquipSetDetailAbilityTable : EquipSetDetailStatusAndAbilityTable
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

	private AbilityDetailPopUp abilityDetailPopUp;

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "AbilityTable";
			yield return "AbilityDataTable";
			yield return "AbilityItemLotTable";
			foreach (string item in base.requireDataTable)
			{
				yield return item;
			}
		}
	}

	public override void UpdateUI()
	{
		UpdateAbilityTable();
		base.UpdateUI();
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		base.OnNotify(flags);
		if ((flags & NOTIFY_FLAG.PRETREAT_SCENE) != (NOTIFY_FLAG)0L)
		{
			UIGrid component = GetComponent<UIGrid>(UI.GRD_ABILITY);
			List<Transform> childList = component.GetChildList();
			NoEventReleaseTouchAndReleases(childList);
			OnQuery_RELEASE_ABILITY();
		}
	}

	protected override void SetAbilityItemEvent(Transform t, int index)
	{
		SetTouchAndRelease(t.GetComponentInChildren<UIButton>().transform, "ABILITY_DATA", "RELEASE_ABILITY", index);
	}

	protected override void SetAbilityItemItemEvent(Transform t, int index)
	{
		SetTouchAndRelease(t.GetComponentInChildren<UIButton>().transform, "ABILITY_ITEM_DATA", "RELEASE_ABILITY", index);
	}

	protected override void OnQuery_ABILITY_DATA()
	{
		int num = (int)GameSection.GetEventData();
		EquipItemAbility ability = abilityCollection[num].ability;
		Transform child = GetComponent<UIGrid>(UI.GRD_ABILITY).GetChild(num);
		if ((Object)abilityDetailPopUp == (Object)null)
		{
			abilityDetailPopUp = CreateAndGetAbilityDetail(UI.OBJ_DETAIL_ROOT);
		}
		abilityDetailPopUp.ShowAbilityDetail(child);
		abilityDetailPopUp.SetAbilityDetailText(ability);
		GameSection.StopEvent();
	}

	protected override void OnQuery_ABILITY_ITEM_DATA()
	{
		int num = (int)GameSection.GetEventData();
		int index = num - abilityCollection.Length;
		AbilityItemInfo abilityItemInfo = abilityItems[index];
		Transform child = GetComponent<UIGrid>(UI.GRD_ABILITY).GetChild(num);
		if ((Object)abilityDetailPopUp == (Object)null)
		{
			abilityDetailPopUp = CreateAndGetAbilityDetail(UI.OBJ_DETAIL_ROOT);
		}
		abilityDetailPopUp.ShowAbilityDetail(child);
		abilityDetailPopUp.SetAbilityDetailText(abilityItemInfo.GetName(), string.Empty, abilityItemInfo.GetDescription());
		GameSection.StopEvent();
	}

	protected void OnQuery_RELEASE_ABILITY()
	{
		if (!((Object)abilityDetailPopUp == (Object)null))
		{
			abilityDetailPopUp.Hide();
			GameSection.StopEvent();
		}
	}

	protected override void PreCacheAbilityDetail(string name, string ap, string desc)
	{
		if ((Object)abilityDetailPopUp == (Object)null)
		{
			abilityDetailPopUp = CreateAndGetAbilityDetail(UI.OBJ_DETAIL_ROOT);
		}
		abilityDetailPopUp.PreCacheAbilityDetail(name, ap, desc);
	}
}
