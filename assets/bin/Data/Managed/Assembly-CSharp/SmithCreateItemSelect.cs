using System;
using UnityEngine;

public class SmithCreateItemSelect : SmithEquipSelectBase
{
	private SmithCreateItemInfo[] pickupWeapon;

	private SmithCreateItemInfo[] pickupArmor;

	private SortBase.TYPE[] transInventoryType = new SortBase.TYPE[11]
	{
		SortBase.TYPE.ONE_HAND_SWORD,
		SortBase.TYPE.TWO_HAND_SWORD,
		SortBase.TYPE.SPEAR,
		SortBase.TYPE.PAIR_SWORDS,
		SortBase.TYPE.ARROW,
		SortBase.TYPE.ARMOR,
		SortBase.TYPE.HELM,
		SortBase.TYPE.ARM,
		SortBase.TYPE.LEG,
		SortBase.TYPE.WEAPON_ALL,
		SortBase.TYPE.ARMOR_ALL
	};

	private EQUIPMENT_TYPE[] transInventoryTypeForEquipment = new EQUIPMENT_TYPE[11]
	{
		EQUIPMENT_TYPE.ONE_HAND_SWORD,
		EQUIPMENT_TYPE.TWO_HAND_SWORD,
		EQUIPMENT_TYPE.SPEAR,
		EQUIPMENT_TYPE.PAIR_SWORDS,
		EQUIPMENT_TYPE.ARROW,
		EQUIPMENT_TYPE.ARMOR,
		EQUIPMENT_TYPE.HELM,
		EQUIPMENT_TYPE.ARM,
		EQUIPMENT_TYPE.LEG,
		EQUIPMENT_TYPE.ONE_HAND_SWORD,
		EQUIPMENT_TYPE.ARMOR
	};

	protected override string prefabSuffix => "Create";

	protected override string GetSelectTypeText()
	{
		return base.sectionData.GetText("TYPE_CREATE");
	}

	protected override void OnClose()
	{
		RemoveCreateNewIcon(selectTypeIndex);
		base.OnClose();
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return base.GetUpdateUINotifyFlags() | NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | NOTIFY_FLAG.UPDATE_USER_STATUS | NOTIFY_FLAG.UPDATE_SMITH_BADGE;
	}

	public override void Initialize()
	{
		EQUIPMENT_TYPE eQUIPMENT_TYPE = (EQUIPMENT_TYPE)GameSection.GetEventData();
		SmithManager.SmithCreateData smithCreateData = MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<SmithManager.SmithCreateData>();
		smithCreateData.selectCreateEquipItemType = TranslateInventoryType(UIBehaviour.GetEquipmentTypeIndex(eQUIPMENT_TYPE));
		smithType = SmithType.GENERATE;
		GameSection.SetEventData(eQUIPMENT_TYPE);
		base.Initialize();
		pickupWeapon = Singleton<CreatePickupItemTable>.I.GetPickupItemAry(SortBase.TYPE.WEAPON_ALL);
		pickupArmor = Singleton<CreatePickupItemTable>.I.GetPickupItemAry(SortBase.TYPE.ARMOR_ALL);
		SetActive((Enum)UI.BTN_WEAPON_PICKUP, pickupWeapon.Length > 0);
		SetActive((Enum)UI.BTN_ARMOR_PICKUP, pickupArmor.Length > 0);
		selectTypeIndex = (int)Mathf.Log((float)smithCreateData.selectCreateEquipItemType, 2f);
		string caption = (!MonoBehaviourSingleton<InventoryManager>.I.IsWeaponInventoryType(base.selectInventoryType)) ? base.sectionData.GetText("CAPTION_DEFENCE") : base.sectionData.GetText("CAPTION_WEAPON");
		InitializeCaption(caption);
	}

	protected override void InitLocalInventory()
	{
		SmithManager.SmithCreateData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>();
		SortBase.TYPE selectCreateEquipItemType = smithData.selectCreateEquipItemType;
		switch (selectCreateEquipItemType)
		{
		case SortBase.TYPE.WEAPON_ALL:
			localInventoryEquipData = SortCompareData.CreateSortDataAry<SmithCreateItemInfo, SmithCreateSortData>(pickupWeapon, sortSettings);
			break;
		case SortBase.TYPE.ARMOR_ALL:
			localInventoryEquipData = SortCompareData.CreateSortDataAry<SmithCreateItemInfo, SmithCreateSortData>(pickupArmor, sortSettings);
			break;
		default:
			localInventoryEquipData = sortSettings.CreateSortAry<SmithCreateItemInfo, SmithCreateSortData>(Singleton<CreateEquipItemTable>.I.GetCreateEquipItemDataAry(SortBaseTypeToEquipmentType(selectCreateEquipItemType)));
			break;
		}
		SelectingInventoryFirst();
	}

	public override void UpdateUI()
	{
		SmithManager.SmithBadgeData smithBadgeData = MonoBehaviourSingleton<SmithManager>.I.smithBadgeData;
		SetBadge((Enum)UI.BTN_HELM, smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.HELM), 6, 0, 0, is_scale_normalize: true);
		SetBadge((Enum)UI.BTN_ARMOR, smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.ARMOR), 6, 0, 0, is_scale_normalize: true);
		SetBadge((Enum)UI.BTN_ARM, smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.ARM), 6, 0, 0, is_scale_normalize: true);
		SetBadge((Enum)UI.BTN_LEG, smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.LEG), 6, 0, 0, is_scale_normalize: true);
		SetBadge((Enum)UI.BTN_WEAPON_1, smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.ONE_HAND_SWORD), 6, 0, 0, is_scale_normalize: true);
		SetBadge((Enum)UI.BTN_WEAPON_2, smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.TWO_HAND_SWORD), 6, 0, 0, is_scale_normalize: true);
		SetBadge((Enum)UI.BTN_WEAPON_3, smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.SPEAR), 6, 0, 0, is_scale_normalize: true);
		SetBadge((Enum)UI.BTN_WEAPON_4, smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.PAIR_SWORDS), 6, 0, 0, is_scale_normalize: true);
		SetBadge((Enum)UI.BTN_WEAPON_5, smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.ARROW), 6, 0, 0, is_scale_normalize: true);
		SetBadge((Enum)UI.BTN_WEAPON_PICKUP, smithBadgeData.GetPickupBadgeNum(is_weapon: true), 6, 0, 0, is_scale_normalize: true);
		SetBadge((Enum)UI.BTN_ARMOR_PICKUP, smithBadgeData.GetPickupBadgeNum(is_weapon: false), 6, 0, 0, is_scale_normalize: true);
		SetActive(GetCtrl(uiTypeTab[weaponPickupIndex]).get_parent(), pickupWeapon != null && pickupWeapon.Length > 0);
		SetActive(GetCtrl(uiTypeTab[armorPickupIndex]).get_parent(), pickupArmor != null && pickupArmor.Length > 0);
		base.GetComponent<UIGrid>((Enum)UI.GRD_WEAPON).Reposition();
		base.GetComponent<UIGrid>((Enum)UI.GRD_ARMOR).Reposition();
		base.UpdateUI();
		if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM))
		{
			SetBadge((Enum)UI.BTN_WEAPON_3, smithBadgeData.GetBadgeNum(EQUIPMENT_TYPE.SPEAR) - 1, 6, 0, 0, is_scale_normalize: true);
			if (MonoBehaviourSingleton<UIManager>.I.tutorialMessage != null)
			{
				MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName(), "CreateItem");
			}
		}
	}

	protected override void SetupInventoryTypeToggole()
	{
		SmithManager.SmithCreateData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>();
		SortBase.TYPE selectCreateEquipItemType = smithData.selectCreateEquipItemType;
		bool flag = false;
		if (selectCreateEquipItemType < SortBase.TYPE.ARMOR || selectCreateEquipItemType == SortBase.TYPE.WEAPON_ALL)
		{
			flag = true;
		}
		SetActive((Enum)UI.OBJ_ATK_ROOT, flag);
		SetActive((Enum)UI.OBJ_DEF_ROOT, !flag);
		SetToggleButton((Enum)UI.TGL_BUTTON_ROOT, flag, (Action<bool>)delegate(bool is_active)
		{
			SmithManager.SmithCreateData smithData2 = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>();
			smithData2.selectCreateEquipItemType = (is_active ? SortBase.TYPE.ONE_HAND_SWORD : SortBase.TYPE.HELM);
			int num = (!is_active) ? 1 : 0;
			ResetTween((Enum)tabAnimTarget[num], 0);
			PlayTween((Enum)tabAnimTarget[num], forward: true, (EventDelegate.Callback)null, is_input_block: false, 0);
			SetActive((Enum)UI.OBJ_ATK_ROOT, is_active);
			SetActive((Enum)UI.OBJ_DEF_ROOT, !is_active);
			selectTypeIndex = (int)Mathf.Log((float)smithData2.selectCreateEquipItemType, 2f);
			SetDirty(InventoryUI);
			InitSort();
			InitLocalInventory();
			LocalInventory();
			UpdateTabButton();
			if (!TutorialStep.HasAllTutorialCompleted() && smithData2.selectCreateEquipItemType == SortBase.TYPE.HELM)
			{
				MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun(MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName(), "SelectArmor");
			}
		});
	}

	protected override void LocalInventory()
	{
		SetupEnableInventoryUI();
		SetLabelText((Enum)UI.LBL_SORT, sortSettings.GetSortLabel());
		if (localInventoryEquipData != null)
		{
			SortBase.TYPE tYPE = TranslateInventoryType(selectTypeIndex);
			bool _is_pickup = tYPE == SortBase.TYPE.WEAPON_ALL || tYPE == SortBase.TYPE.ARMOR_ALL;
			m_generatedIconList.Clear();
			UpdateNewIconInfo();
			bool initItem = false;
			SetDynamicList((Enum)InventoryUI, string.Empty, localInventoryEquipData.Length, reset: false, (Func<int, bool>)delegate(int check_index)
			{
				SmithCreateItemInfo smithCreateItemInfo = localInventoryEquipData[check_index].GetItemData() as SmithCreateItemInfo;
				if (smithCreateItemInfo == null)
				{
					return false;
				}
				if (!MonoBehaviourSingleton<InventoryManager>.I.IsHaveingKeyMaterial(smithCreateItemInfo.smithCreateTableData.needKeyOrder, smithCreateItemInfo.smithCreateTableData.needMaterial))
				{
					return false;
				}
				if ((int)smithCreateItemInfo.smithCreateTableData.researchLv > MonoBehaviourSingleton<UserInfoManager>.I.userStatus.researchLv)
				{
					return false;
				}
				uint tableID2 = localInventoryEquipData[check_index].GetTableID();
				if (tableID2 == 20250105 && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady && !MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM))
				{
					return false;
				}
				if (tableID2 == 0)
				{
					return false;
				}
				SortCompareData sortCompareData = localInventoryEquipData[check_index];
				if (sortCompareData == null || !sortCompareData.IsPriority(sortSettings.orderTypeAsc))
				{
					return false;
				}
				return true;
			}, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycle)
			{
				initItem = true;
				SmithCreateItemInfo create_info = localInventoryEquipData[i].GetItemData() as SmithCreateItemInfo;
				uint tableID = localInventoryEquipData[i].GetTableID();
				if (tableID != 20250105 || !MonoBehaviourSingleton<UserInfoManager>.I.userStatus.IsTutorialBitReady || MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(TUTORIAL_MENU_BIT.FORGE_ITEM))
				{
					if (tableID == 0)
					{
						SetActive(t, is_visible: false);
					}
					else
					{
						SetActive(t, is_visible: true);
						SmithCreateSortData smithCreateSortData = localInventoryEquipData[i] as SmithCreateSortData;
						if (smithCreateSortData != null && smithCreateSortData.IsPriority(sortSettings.orderTypeAsc))
						{
							EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData(tableID);
							ITEM_ICON_TYPE iconType = localInventoryEquipData[i].GetIconType();
							SkillSlotUIData[] skillSlotData = GetSkillSlotData(equipItemData, 0);
							bool flag = false;
							flag = MonoBehaviourSingleton<SmithManager>.I.NeedSmithBadge(create_info, _is_pickup);
							SmithCreateItemSelect smithCreateItemSelect = this;
							ITEM_ICON_TYPE icon_type = iconType;
							int iconID = equipItemData.GetIconID(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex);
							RARITY_TYPE? rarity = equipItemData.rarity;
							SmithCreateSortData item_data = smithCreateSortData;
							SkillSlotUIData[] skill_slot_data = skillSlotData;
							bool isShowMainStatus = base.IsShowMainStatus;
							string event_name = "SELECT_ITEM";
							ItemIconDetail.ICON_STATUS iconStatus = smithCreateSortData.GetIconStatus();
							bool is_new = flag;
							GET_TYPE getType = smithCreateSortData.GetGetType();
							ItemIcon itemIcon = smithCreateItemSelect.CreateSmithCreateItemIconDetail(icon_type, iconID, rarity, item_data, skill_slot_data, isShowMainStatus, t, event_name, i, iconStatus, is_new, -1, is_select: false, getType);
							itemIcon.SetItemID(smithCreateSortData.GetTableID());
							itemIcon.SetButtonColor(localInventoryEquipData[i].IsPriority(sortSettings.orderTypeAsc), is_instant: true);
							SetLongTouch(itemIcon.transform, "DETAIL", i);
							if (itemIcon != null && smithCreateSortData != null)
							{
								itemIcon.SetInitData(smithCreateSortData);
							}
							if (itemIcon != null && !m_generatedIconList.Contains(itemIcon))
							{
								m_generatedIconList.Add(itemIcon);
							}
						}
					}
				}
			});
			SetActive(GetCtrl(UI.OBJ_ROOT), UI.LBL_NO_ITEM, !initItem);
			SetLabelText(GetCtrl(UI.OBJ_ROOT), UI.LBL_NO_ITEM, StringTable.Get(STRING_CATEGORY.COMMON, 19799u));
		}
		else
		{
			SetActive(GetCtrl(UI.OBJ_ROOT), UI.LBL_NO_ITEM, is_visible: true);
			SetLabelText(GetCtrl(UI.OBJ_ROOT), UI.LBL_NO_ITEM, StringTable.Get(STRING_CATEGORY.COMMON, 19799u));
		}
	}

	protected override void InitSort()
	{
		SmithManager.SmithCreateData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>();
		if (smithData.selectCreateEquipItemType < SortBase.TYPE.ARMOR || smithData.selectCreateEquipItemType == SortBase.TYPE.WEAPON_ALL)
		{
			if (smithData.selectCreateEquipItemType == SortBase.TYPE.WEAPON_ALL)
			{
				sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.SMITH_CREATE_PICKUP_WEAPON, SortSettings.SETTINGS_TYPE.CREATE_EQUIP_ITEM);
			}
			else
			{
				sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.SMITH_CREATE_WEAPON, SortSettings.SETTINGS_TYPE.CREATE_EQUIP_ITEM);
			}
		}
		else if (smithData.selectCreateEquipItemType == SortBase.TYPE.ARMOR_ALL)
		{
			sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.SMITH_CREATE_PICKUP_ARMOR, SortSettings.SETTINGS_TYPE.CREATE_EQUIP_ITEM);
		}
		else
		{
			sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.SMITH_CREATE_ARMOR, SortSettings.SETTINGS_TYPE.CREATE_EQUIP_ITEM);
		}
	}

	protected override bool sorting()
	{
		InitLocalInventory();
		return true;
	}

	protected override void SelectingInventoryFirst()
	{
		if (localInventoryEquipData != null && localInventoryEquipData.Length != 0 && localInventoryEquipData[0] != null)
		{
			SmithCreateItemInfo smithCreateItemInfo = localInventoryEquipData[0].GetItemData() as SmithCreateItemInfo;
			if (smithCreateItemInfo != null)
			{
				selectInventoryIndex = 0;
				SmithManager.SmithCreateData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>();
				smithData.generateTableData = smithCreateItemInfo.equipTableData;
				smithData.createEquipItemTable = smithCreateItemInfo.smithCreateTableData;
			}
		}
	}

	protected override int GetSelectItemIndex()
	{
		if (localInventoryEquipData == null || localInventoryEquipData.Length == 0 || localInventoryEquipData[0] == null)
		{
			return -1;
		}
		SmithManager.SmithCreateData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>();
		int i = 0;
		for (int num = localInventoryEquipData.Length; i < num; i++)
		{
			SmithCreateItemInfo smithCreateItemInfo = localInventoryEquipData[i].GetItemData() as SmithCreateItemInfo;
			if (smithCreateItemInfo != null && smithCreateItemInfo.equipTableData.id == smithData.generateTableData.id && smithCreateItemInfo.smithCreateTableData.id == smithData.createEquipItemTable.id)
			{
				return i;
			}
		}
		return -1;
	}

	protected void OnCloseDialog_SmithCreateItemSort()
	{
		OnCloseSortDialog();
	}

	protected override void OnQuery_TRY_ON()
	{
		selectInventoryIndex = (int)GameSection.GetEventData();
		base.OnQuery_TRY_ON();
	}

	private void TryOn()
	{
		if (localInventoryEquipData != null && localInventoryEquipData.Length != 0)
		{
			selectInventoryIndex = (int)GameSection.GetEventData();
			SmithCreateSortData smithCreateSortData = localInventoryEquipData[selectInventoryIndex] as SmithCreateSortData;
			SmithManager.SmithCreateData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>();
			if (smithCreateSortData != null && smithData != null)
			{
				smithData.createEquipItemTable = smithCreateSortData.createData.smithCreateTableData;
				smithData.generateTableData = smithCreateSortData.createData.equipTableData;
			}
		}
	}

	protected override void OnQuery_SELECT_ITEM()
	{
		TryOn();
		SmithManager.SmithCreateData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>();
		if (smithData.createEquipItemTable == null || smithData.generateTableData == null)
		{
			GameSection.StopEvent();
		}
	}

	protected override void OnQueryDetail()
	{
		TryOn();
		GameSection.SetEventData(new object[2]
		{
			ItemDetailEquip.CURRENT_SECTION.SMITH_CREATE,
			GetEquipTableData()
		});
	}

	protected override void OnQuery_SKILL_ICON_BUTTON()
	{
		GameSection.StopEvent();
	}

	private void OnQuery_ABILITY()
	{
		int num = (int)GameSection.GetEventData();
		int num2 = num >> 16;
		int num3 = num % 65536;
		SmithCreateItemInfo smithCreateItemInfo = localInventoryEquipData[num2].GetItemData() as SmithCreateItemInfo;
		EquipItemAbility equipItemAbility = null;
		if (smithCreateItemInfo != null)
		{
			equipItemAbility = new EquipItemAbility((uint)smithCreateItemInfo.equipTableData.fixedAbility[num3].id, 0);
		}
		if (equipItemAbility == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.SetEventData(equipItemAbility);
		}
	}

	protected override void OnQuery_TYPE_TAB()
	{
		int selectTypeIndex = base.selectTypeIndex;
		base.selectTypeIndex = (int)GameSection.GetEventData();
		RemoveCreateNewIcon(selectTypeIndex);
		SmithManager.SmithCreateData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>();
		SortBase.TYPE tYPE = smithData.selectCreateEquipItemType = TranslateInventoryType(base.selectTypeIndex);
		if ((selectTypeIndex >= 9 && base.selectTypeIndex < 9) || (selectTypeIndex < 9 && base.selectTypeIndex >= 9))
		{
			InitSort();
		}
		SetDirty(InventoryUI);
		InitLocalInventory();
		RefreshUI();
	}

	private SortBase.TYPE TranslateInventoryType(int index)
	{
		return transInventoryType[index];
	}

	private EQUIPMENT_TYPE TranslateInventoryTypeForEquipment(int index)
	{
		return transInventoryTypeForEquipment[index];
	}

	private void RemoveCreateNewIcon(int tab_index)
	{
		EQUIPMENT_TYPE type = TranslateInventoryTypeForEquipment(tab_index);
		int badgeNum = MonoBehaviourSingleton<SmithManager>.I.smithBadgeData.GetBadgeNum(type);
		if (badgeNum > 0)
		{
			SortBase.TYPE tYPE = TranslateInventoryType(tab_index);
			bool is_pickup = tYPE == SortBase.TYPE.WEAPON_ALL || tYPE == SortBase.TYPE.ARMOR_ALL;
			MonoBehaviourSingleton<SmithManager>.I.RemoveSmithBadge(type, is_pickup);
			MonoBehaviourSingleton<SmithManager>.I.CreateBadgeData(is_force: true);
			MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(NOTIFY_FLAG.UPDATE_SMITH_BADGE);
		}
	}
}
