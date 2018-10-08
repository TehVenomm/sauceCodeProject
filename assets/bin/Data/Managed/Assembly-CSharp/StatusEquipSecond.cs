using System;
using UnityEngine;

public class StatusEquipSecond : StatusEquip
{
	public new enum UI
	{
		LBL_NAME,
		LBL_LV_NOW,
		LBL_LV_MAX,
		LBL_ATK,
		LBL_DEF,
		LBL_ELEM,
		SPR_ELEM,
		LBL_SELL,
		TEX_MODEL,
		SCR_INVENTORY,
		GRD_INVENTORY,
		GRD_INVENTORY_SMALL,
		LBL_SORT,
		BTN_SORT,
		BTN_BACK,
		TGL_CHANGE_INVENTORY,
		TGL_ICON_ASC,
		OBJ_SKILL_BUTTON_ROOT,
		OBJ_DETAIL_ROOT,
		BTN_SELL,
		BTN_GROW,
		OBJ_FAVORITE_ROOT,
		SPR_IS_EVOLVE,
		OBJ_ATK_ROOT,
		OBJ_DEF_ROOT,
		OBJ_ELEM_ROOT,
		OBJ_SELL_ROOT,
		SPR_SELECT_WEAPON,
		SPR_SELECT_DEF,
		SPR_TYPE_ICON,
		SPR_TYPE_ICON_BG,
		SPR_TYPE_ICON_RARITY,
		LBL_SELECT_TYPE,
		OBJ_STATUS_ROOT,
		LBL_STATUS_ATK,
		LBL_STATUS_DEF,
		LBL_STATUS_HP,
		LBL_STATUS_ADD_ATK,
		LBL_STATUS_ADD_DEF,
		LBL_STATUS_ADD_HP,
		STR_TITLE_ITEM_INFO,
		STR_TITLE_STATUS,
		STR_TITLE_SKILL_SLOT,
		STR_TITLE_ABILITY,
		STR_TITLE_MONEY,
		STR_TITLE_MATERIAL,
		STR_TITLE_ELEMENT,
		TBL_ABILITY,
		STR_NON_ABILITY,
		OBJ_ABILITY,
		LBL_ABILITY,
		LBL_ABILITY_NUM,
		OBJ_FIXEDABILITY,
		LBL_FIXEDABILITY,
		LBL_FIXEDABILITY_NUM,
		OBJ_ABILITY_ITEM,
		LBL_ABILITY_ITEM,
		OBJ_WEAPON_WINDOW,
		OBJ_DEFENSE_WINDOW,
		SCR_INVENTORY_DEF,
		GRD_INVENTORY_DEF,
		GRD_INVENTORY_SMALL_DEF,
		BTN_WEAPON_1,
		BTN_WEAPON_2,
		BTN_WEAPON_3,
		BTN_WEAPON_4,
		BTN_WEAPON_5,
		OBJ_CAPTION_3,
		LBL_CAPTION
	}

	private UI[] tgl = new UI[5]
	{
		UI.BTN_WEAPON_1,
		UI.BTN_WEAPON_2,
		UI.BTN_WEAPON_3,
		UI.BTN_WEAPON_4,
		UI.BTN_WEAPON_5
	};

	protected object[] backEventData;

	public override void Initialize()
	{
		object eventData = GameSection.GetEventData();
		if (eventData is ItemDetailEquip.DetailEquipEventData)
		{
			backEventData = (eventData as ItemDetailEquip.DetailEquipEventData).currentEventData;
			GameSection.SetEventData((eventData as ItemDetailEquip.DetailEquipEventData).localEquipSetData);
		}
		SetupTargetInventory();
		base.Initialize();
	}

	private void SetupTargetInventory()
	{
		string text;
		if (MonoBehaviourSingleton<InventoryManager>.I.IsWeaponInventoryType(MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType))
		{
			switchInventoryAry = weaponInventoryAry;
			SetActive((Enum)UI.OBJ_WEAPON_WINDOW, true);
			SetActive((Enum)UI.OBJ_DEFENSE_WINDOW, false);
			SetToggle((Enum)tgl[(int)(MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType - 1)], true);
			text = base.sectionData.GetText("CAPTION_WEAPON");
		}
		else
		{
			switchInventoryAry = defenseInventoryAry;
			SetActive((Enum)UI.OBJ_WEAPON_WINDOW, false);
			SetActive((Enum)UI.OBJ_DEFENSE_WINDOW, true);
			text = base.sectionData.GetText("CAPTION_DEFENCE");
		}
		InitializeCaption(text);
	}

	private void OnQuery_TAB_1()
	{
		int num = 0;
		SetToggle((Enum)tgl[num], true);
		LimitedInventory(num);
	}

	private void OnQuery_TAB_2()
	{
		int num = 1;
		SetToggle((Enum)tgl[num], true);
		LimitedInventory(num);
	}

	private void OnQuery_TAB_3()
	{
		int num = 2;
		SetToggle((Enum)tgl[num], true);
		LimitedInventory(num);
	}

	private void OnQuery_TAB_4()
	{
		int num = 3;
		SetToggle((Enum)tgl[num], true);
		LimitedInventory(num);
	}

	private void OnQuery_TAB_5()
	{
		int num = 4;
		SetToggle((Enum)tgl[num], true);
		LimitedInventory(num);
	}

	private void LimitedInventory(int index)
	{
		MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = (InventoryManager.INVENTORY_TYPE)(index + 1);
		InitLocalInventory();
		SetDirty(InventoryUI);
		RefreshUI();
	}

	protected override void InitSort()
	{
		base.InitSort();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
	}

	protected override void _OnOpenStatusStage()
	{
	}

	protected override void _OnCloseStatusStage()
	{
	}

	protected override void EquipParam()
	{
	}

	protected override void OnQuery_TRY_ON()
	{
		if (GameSaveData.instance.canPushTrackEquipTutorial)
		{
			GameSaveData.instance.SetPushTrackEquipTutorial(false);
			MonoBehaviourSingleton<GoWrapManager>.I.trackTutorialStep(TRACK_TUTORIAL_STEP_BIT.tutorial_weapon_equip, "Tutorial");
		}
		base.OnQuery_TRY_ON();
		GameSection.ChangeEvent("SELECT_ITEM", null);
		OnQuery_SELECT_ITEM();
	}

	private void OnQuery_SECTION_BACK()
	{
		if (backEventData != null)
		{
			GameSection.SetEventData(backEventData);
			Close(UITransition.TYPE.CLOSE);
		}
	}

	private void InitializeCaption(string caption)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		Transform ctrl = GetCtrl(UI.OBJ_CAPTION_3);
		SetLabelText(ctrl, UI.LBL_CAPTION, caption);
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
