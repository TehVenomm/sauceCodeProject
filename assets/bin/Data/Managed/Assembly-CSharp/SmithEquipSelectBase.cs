using System;
using UnityEngine;

public abstract class SmithEquipSelectBase : EquipSelectBase
{
	protected new enum UI
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
		LBL_ABILITY,
		LBL_ABILITY_NUM,
		BTN_HELM,
		BTN_ARMOR,
		BTN_ARM,
		BTN_LEG,
		BTN_ARMOR_PICKUP,
		BTN_WEAPON_1,
		BTN_WEAPON_2,
		BTN_WEAPON_3,
		BTN_WEAPON_4,
		BTN_WEAPON_5,
		BTN_WEAPON_PICKUP,
		TGL_BUTTON_ROOT,
		GRD_WEAPON,
		GRD_ARMOR,
		OBJ_CAPTION_3,
		LBL_CAPTION,
		OBJ_ROOT
	}

	protected UI[] uiTypeTab = new UI[11]
	{
		UI.BTN_WEAPON_1,
		UI.BTN_WEAPON_2,
		UI.BTN_WEAPON_3,
		UI.BTN_WEAPON_4,
		UI.BTN_WEAPON_5,
		UI.BTN_ARMOR,
		UI.BTN_HELM,
		UI.BTN_ARM,
		UI.BTN_LEG,
		UI.BTN_WEAPON_PICKUP,
		UI.BTN_ARMOR_PICKUP
	};

	private InventoryManager.INVENTORY_TYPE[] inventoryType = new InventoryManager.INVENTORY_TYPE[11]
	{
		InventoryManager.INVENTORY_TYPE.ONE_HAND_SWORD,
		InventoryManager.INVENTORY_TYPE.TWO_HAND_SWORD,
		InventoryManager.INVENTORY_TYPE.SPEAR,
		InventoryManager.INVENTORY_TYPE.PAIR_SWORDS,
		InventoryManager.INVENTORY_TYPE.ARROW,
		InventoryManager.INVENTORY_TYPE.ARMOR,
		InventoryManager.INVENTORY_TYPE.HELM,
		InventoryManager.INVENTORY_TYPE.ARM,
		InventoryManager.INVENTORY_TYPE.LEG,
		InventoryManager.INVENTORY_TYPE.ALL_WEAPON,
		InventoryManager.INVENTORY_TYPE.ALL_ARMOR
	};

	protected int selectTypeIndex;

	protected int weaponPickupIndex;

	protected int armorPickupIndex;

	protected UI[] tabAnimTarget = new UI[2]
	{
		UI.SPR_SELECT_WEAPON,
		UI.SPR_SELECT_DEF
	};

	protected InventoryManager.INVENTORY_TYPE selectInventoryType => inventoryType[selectTypeIndex];

	protected abstract string prefabSuffix
	{
		get;
	}

	public override void Initialize()
	{
		selectTypeIndex = UIBehaviour.GetEquipmentTypeIndex((EQUIPMENT_TYPE)(int)GameSection.GetEventData());
		weaponPickupIndex = Array.FindIndex(uiTypeTab, (UI ui) => ui == UI.BTN_WEAPON_PICKUP);
		armorPickupIndex = Array.FindIndex(uiTypeTab, (UI ui) => ui == UI.BTN_ARMOR_PICKUP);
		SetPrefab((Enum)UI.OBJ_ROOT, "SmithEquipSelectBase_" + prefabSuffix);
		base.Initialize();
	}

	public override void UpdateUI()
	{
		UpdateTabButton();
		SetupInventoryTypeToggole();
		base.UpdateUI();
	}

	protected virtual void SetupInventoryTypeToggole()
	{
		bool flag = selectTypeIndex < UIBehaviour.GetEquipmentTypeIndex(EQUIPMENT_TYPE.ARMOR) || selectTypeIndex == weaponPickupIndex;
		SetActive((Enum)UI.OBJ_ATK_ROOT, flag);
		SetActive((Enum)UI.OBJ_DEF_ROOT, !flag);
		SetToggleButton((Enum)UI.TGL_BUTTON_ROOT, flag, (Action<bool>)delegate(bool is_active)
		{
			EQUIPMENT_TYPE type = (!is_active) ? EQUIPMENT_TYPE.HELM : EQUIPMENT_TYPE.ONE_HAND_SWORD;
			int num = (!is_active) ? 1 : 0;
			ResetTween((Enum)tabAnimTarget[num], 0);
			PlayTween((Enum)tabAnimTarget[num], true, (EventDelegate.Callback)null, false, 0);
			SetActive((Enum)UI.OBJ_ATK_ROOT, is_active);
			SetActive((Enum)UI.OBJ_DEF_ROOT, !is_active);
			selectTypeIndex = UIBehaviour.GetEquipmentTypeIndex(type);
			sortSettings.dialogType = ((!is_active) ? SortBase.DIALOG_TYPE.ARMOR : SortBase.DIALOG_TYPE.WEAPON);
			SortBase.SORT_REQUIREMENT sORT_REQUIREMENT = (sortSettings.dialogType != SortBase.DIALOG_TYPE.ARMOR) ? SortBase.SORT_REQUIREMENT.REQUIREMENT_WEAPON_BIT : SortBase.SORT_REQUIREMENT.REQUIREMENT_ARMORS_BIT;
			if ((sortSettings.requirement & sORT_REQUIREMENT) == (SortBase.SORT_REQUIREMENT)0)
			{
				if (sortSettings.requirement == SortBase.SORT_REQUIREMENT.ATK)
				{
					sortSettings.requirement = SortBase.SORT_REQUIREMENT.DEF;
				}
				else if (sortSettings.requirement == SortBase.SORT_REQUIREMENT.DEF)
				{
					sortSettings.requirement = SortBase.SORT_REQUIREMENT.ATK;
				}
				else if (sortSettings.requirement == SortBase.SORT_REQUIREMENT.ELEM_ATK)
				{
					sortSettings.requirement = SortBase.SORT_REQUIREMENT.ELEM_DEF;
				}
				else if (sortSettings.requirement == SortBase.SORT_REQUIREMENT.ELEM_DEF)
				{
					sortSettings.requirement = SortBase.SORT_REQUIREMENT.ELEM_ATK;
				}
				else
				{
					sortSettings.requirement = SortBase.SORT_REQUIREMENT.ELEMENT;
				}
			}
			SetDirty(InventoryUI);
			InitLocalInventory();
			LocalInventory();
			UpdateTabButton();
		});
	}

	protected override void EquipParam()
	{
	}

	protected virtual void OnQuery_TYPE_TAB()
	{
		selectTypeIndex = (int)GameSection.GetEventData();
		InitLocalInventory();
		SetDirty(InventoryUI);
		RefreshUI();
	}

	protected void UpdateTabButton()
	{
		int i = 0;
		for (int num = uiTypeTab.Length; i < num; i++)
		{
			SetEvent((Enum)uiTypeTab[i], "TYPE_TAB", i);
		}
		SetToggle((Enum)uiTypeTab[selectTypeIndex], true);
	}

	protected virtual void OnQuery_SECTION_BACK()
	{
		MonoBehaviourSingleton<SmithManager>.I.DisableSmithBlur(false);
	}

	protected void InitializeCaption(string caption)
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
