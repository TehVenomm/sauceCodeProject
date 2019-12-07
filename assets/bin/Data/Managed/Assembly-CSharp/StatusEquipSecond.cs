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

	protected override SortCompareData[] CreateSortAry()
	{
		return sortSettings.CreateSortAry<EquipItemInfo, EquipItemSortWithPayCheckData>(MonoBehaviourSingleton<SmithManager>.I.localInventoryEquipData as EquipItemInfo[]);
	}

	private void SetupTargetInventory()
	{
		string text;
		if (MonoBehaviourSingleton<InventoryManager>.I.IsWeaponInventoryType(MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType))
		{
			switchInventoryAry = weaponInventoryAry;
			SetActive(UI.OBJ_WEAPON_WINDOW, is_visible: true);
			SetActive(UI.OBJ_DEFENSE_WINDOW, is_visible: false);
			SetToggle(tgl[(int)(MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType - 1)], value: true);
			text = base.sectionData.GetText("CAPTION_WEAPON");
		}
		else
		{
			switchInventoryAry = defenseInventoryAry;
			SetActive(UI.OBJ_WEAPON_WINDOW, is_visible: false);
			SetActive(UI.OBJ_DEFENSE_WINDOW, is_visible: true);
			text = base.sectionData.GetText("CAPTION_DEFENCE");
		}
		InitializeCaption(text);
	}

	private void OnQuery_TAB_1()
	{
		int num = 0;
		SetToggle(tgl[num], value: true);
		LimitedInventory(num);
	}

	private void OnQuery_TAB_2()
	{
		int num = 1;
		SetToggle(tgl[num], value: true);
		LimitedInventory(num);
	}

	private void OnQuery_TAB_3()
	{
		int num = 2;
		SetToggle(tgl[num], value: true);
		LimitedInventory(num);
	}

	private void OnQuery_TAB_4()
	{
		int num = 3;
		SetToggle(tgl[num], value: true);
		LimitedInventory(num);
	}

	private void OnQuery_TAB_5()
	{
		int num = 4;
		SetToggle(tgl[num], value: true);
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

	protected override SortBase.DIALOG_TYPE GetDialogType(bool isWeapon)
	{
		if (!isWeapon)
		{
			return SortBase.DIALOG_TYPE.TYPE_FILTERABLE_ARMOR;
		}
		return SortBase.DIALOG_TYPE.TYPE_FILTERABLE_WEAPON;
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
			GameSaveData.instance.SetPushTrackEquipTutorial(canPush: false);
		}
		base.OnQuery_TRY_ON();
		GameSection.ChangeEvent("SELECT_ITEM");
		OnQuery_SELECT_ITEM();
	}

	private void OnQuery_SECTION_BACK()
	{
		if (backEventData != null)
		{
			GameSection.SetEventData(backEventData);
			Close();
		}
	}

	private void InitializeCaption(string caption)
	{
		Transform ctrl = GetCtrl(UI.OBJ_CAPTION_3);
		SetLabelText(ctrl, UI.LBL_CAPTION, caption);
		UITweenCtrl component = ctrl.gameObject.GetComponent<UITweenCtrl>();
		if (component != null)
		{
			component.Reset();
			int i = 0;
			for (int num = component.tweens.Length; i < num; i++)
			{
				component.tweens[i].ResetToBeginning();
			}
			component.Play();
		}
	}
}
