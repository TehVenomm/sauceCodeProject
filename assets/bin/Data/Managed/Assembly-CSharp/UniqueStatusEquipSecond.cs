using System;
using UnityEngine;

public class UniqueStatusEquipSecond : StatusEquip
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

	private int swapSetNo;

	private int swapSlotNo;

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
			SetActive((Enum)UI.OBJ_WEAPON_WINDOW, is_visible: true);
			SetActive((Enum)UI.OBJ_DEFENSE_WINDOW, is_visible: false);
			SetToggle((Enum)tgl[(int)(MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType - 1)], value: true);
			text = base.sectionData.GetText("CAPTION_WEAPON");
		}
		else
		{
			switchInventoryAry = defenseInventoryAry;
			SetActive((Enum)UI.OBJ_WEAPON_WINDOW, is_visible: false);
			SetActive((Enum)UI.OBJ_DEFENSE_WINDOW, is_visible: true);
			text = base.sectionData.GetText("CAPTION_DEFENCE");
		}
		InitializeCaption(text);
	}

	private void OnQuery_TAB_1()
	{
		int num = 0;
		SetToggle((Enum)tgl[num], value: true);
		LimitedInventory(num);
	}

	private void OnQuery_TAB_2()
	{
		int num = 1;
		SetToggle((Enum)tgl[num], value: true);
		LimitedInventory(num);
	}

	private void OnQuery_TAB_3()
	{
		int num = 2;
		SetToggle((Enum)tgl[num], value: true);
		LimitedInventory(num);
	}

	private void OnQuery_TAB_4()
	{
		int num = 3;
		SetToggle((Enum)tgl[num], value: true);
		LimitedInventory(num);
	}

	private void OnQuery_TAB_5()
	{
		int num = 4;
		SetToggle((Enum)tgl[num], value: true);
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
		return (!isWeapon) ? SortBase.DIALOG_TYPE.TYPE_FILTERABLE_ARMOR : SortBase.DIALOG_TYPE.TYPE_FILTERABLE_WEAPON;
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
		base.OnQuery_TRY_ON();
		OnQuery_SELECT_ITEM();
	}

	protected void OnQuery_UniqueStatusRemoveEquipConfirm_YES()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<StatusManager>.I.RemoveOrderNo(base.selectEquipSetData.setNo, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
			GameSection.SetEventData(new ChangeEquipData(base.selectEquipSetData.setNo, base.selectEquipSetData.index, null));
		});
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
		UITweenCtrl component = ctrl.get_gameObject().GetComponent<UITweenCtrl>();
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

	protected override int GetEquipIndex(EquipItemInfo select_item)
	{
		EquipSetInfo[] localEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet();
		if (select_item == null)
		{
			return -1;
		}
		for (int i = 0; i < localEquipSet.Length; i++)
		{
			EquipSetInfo equipSetInfo = localEquipSet[i];
			for (int j = 0; j < equipSetInfo.item.Length; j++)
			{
				if (equipSetInfo.item[j] != null && equipSetInfo.item[j].uniqueID != 0 && select_item.uniqueID == equipSetInfo.item[j].uniqueID)
				{
					swapSetNo = i;
					swapSlotNo = j;
					return j;
				}
			}
		}
		return -1;
	}

	protected override bool IsAlreadyEquipItem(EquipItemInfo item)
	{
		return GetEquipIndex(item) >= 0 && (base.selectEquipSetData.index != swapSlotNo || base.selectEquipSetData.setNo != swapSetNo);
	}

	protected void OnQuery_UniqueStatusSwapEquipConfirm_YES()
	{
		int num = base.selectEquipSetData.EquippingIndexOf(EquipItem);
		int index = base.selectEquipSetData.index;
		EquipItemInfo equipItemInfo = base.selectEquipSetData.equipSetInfo.item[index];
		base.selectEquipSetData.equipSetInfo.item[index] = base.selectEquipSetData.equipSetInfo.item[num];
		base.selectEquipSetData.equipSetInfo.item[num] = equipItemInfo;
		MonoBehaviourSingleton<StatusManager>.I.SwapUniqueWeapon(num, index);
	}

	protected void OnQuery_UniqueStatusClosetSwapEquipConfirm_YES()
	{
		int index = base.selectEquipSetData.index;
		EquipSetInfo[] localEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet();
		base.selectEquipSetData.equipSetInfo.item[index] = localEquipSet[swapSetNo].item[swapSlotNo];
		localEquipSet[swapSetNo].item[swapSlotNo] = null;
		MonoBehaviourSingleton<StatusManager>.I.ReplaceUniqueEquipSet(base.selectEquipSetData.equipSetInfo, base.selectEquipSetData.setNo);
		MonoBehaviourSingleton<StatusManager>.I.ReplaceUniqueEquipSet(localEquipSet[swapSetNo], swapSetNo);
	}

	protected void OnQuery_UniqueStatusOrderSwapEquipConfirm_YES()
	{
		int index = base.selectEquipSetData.index;
		EquipSetInfo[] localEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet();
		base.selectEquipSetData.equipSetInfo.item[index] = localEquipSet[swapSetNo].item[swapSlotNo];
		localEquipSet[swapSetNo].item[swapSlotNo] = null;
		MonoBehaviourSingleton<StatusManager>.I.ReplaceUniqueEquipSet(base.selectEquipSetData.equipSetInfo, base.selectEquipSetData.setNo);
		MonoBehaviourSingleton<StatusManager>.I.ReplaceUniqueEquipSet(localEquipSet[swapSetNo], swapSetNo);
		GameSection.StayEvent();
		MonoBehaviourSingleton<StatusManager>.I.RemoveOrderNo(swapSetNo, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
		});
	}

	protected override void ChangeSwapEquipConfirm(int slotNo, string equipName)
	{
		EquipSetInfo[] localEquipSet = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet();
		if (swapSetNo != base.selectEquipSetData.setNo)
		{
			if (localEquipSet[swapSetNo].order > 0)
			{
				GameSection.ChangeEvent("SWAP_CONFIRM_EQUIP_ORDER", new object[1]
				{
					equipName
				});
			}
			else
			{
				GameSection.ChangeEvent("SWAP_CONFIRM_OTHER_CLOSET", new object[1]
				{
					equipName
				});
			}
		}
		else
		{
			base.ChangeSwapEquipConfirm(slotNo, equipName);
		}
	}

	protected override bool IsRemoveEquipSloat(int equip_slot_index)
	{
		return true;
	}

	protected override ItemIcon CreateRemoveIcon(Transform parent, string event_name, int event_data = 0, int toggle_group = -1, bool is_select = false, string name = null)
	{
		if (base.selectEquipSetData.equipSetInfo.order > 0)
		{
			event_name = "EQUIP_ORDER_REMOVE";
		}
		return base.CreateRemoveIcon(parent, event_name, event_data, toggle_group, is_select, name);
	}

	protected override bool IsCreateRemoveButton()
	{
		return true;
	}
}
