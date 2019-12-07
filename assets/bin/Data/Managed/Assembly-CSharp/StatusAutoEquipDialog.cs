using System.Collections.Generic;
using UnityEngine;

public class StatusAutoEquipDialog : GameSection
{
	protected enum UI
	{
		BTN_NONE_WEAPON,
		BTN_ONE_HAND,
		BTN_TWO_HAND,
		BTN_SPEAR,
		BTN_PAIR,
		BTN_ARROW,
		BTN_NONE_ELEMENT,
		BTN_FIRE,
		BTN_WATER,
		BTN_THUNDER,
		BTN_SOIL,
		BTN_LIGHT,
		BTN_DARK,
		BTN_CURRENT,
		BTN_POTENTIAL
	}

	private enum ELEMENT_CONDITION
	{
		EQUAL,
		EFFECTIVE_DEF,
		DISADVANTAGEOUS
	}

	private enum WEAPON_TYPE
	{
		NONE,
		ONE_HAND,
		TWO_HAND,
		SPEAR,
		PAIR,
		ARROW
	}

	private enum ELEMENT
	{
		NONE,
		FIRE,
		WATER,
		THUNDER,
		SOIL,
		LIGHT,
		DARK
	}

	private new enum STATE
	{
		CURRENT,
		POTENTIAL
	}

	private bool isMismatchElem;

	private StatusEquip.LocalEquipSetData selectEquipSetData;

	private int weaponIndex;

	private int elementIndex;

	private bool isCurrent;

	private StatusEquip.ChangeEquipData[] newEquipments;

	private List<ulong> selectedWeaponIds;

	private EquipItemInfo[] weaponItemInfoList;

	private UI[] weapons = new UI[6]
	{
		UI.BTN_NONE_WEAPON,
		UI.BTN_ONE_HAND,
		UI.BTN_TWO_HAND,
		UI.BTN_SPEAR,
		UI.BTN_PAIR,
		UI.BTN_ARROW
	};

	private UI[] elements = new UI[7]
	{
		UI.BTN_NONE_ELEMENT,
		UI.BTN_FIRE,
		UI.BTN_WATER,
		UI.BTN_THUNDER,
		UI.BTN_SOIL,
		UI.BTN_LIGHT,
		UI.BTN_DARK
	};

	private UI[] states = new UI[2]
	{
		UI.BTN_CURRENT,
		UI.BTN_POTENTIAL
	};

	public override void Initialize()
	{
		selectEquipSetData = (GameSection.GetEventData() as StatusEquip.LocalEquipSetData);
		base.Initialize();
	}

	private void OnQuery_OK()
	{
		weaponIndex = GetToggleIndex(weapons);
		elementIndex = GetToggleIndex(elements);
		isCurrent = (GetToggleIndex(states) == 0);
		MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = ChangeWeaponTypeToInventoryType(weaponIndex);
		weaponItemInfoList = MonoBehaviourSingleton<InventoryManager>.I.GetEquipInventoryClone();
		if (weaponItemInfoList == null || weaponItemInfoList.Length == 0)
		{
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, StringTable.Get(STRING_CATEGORY.AUTO_EQUIP, 0u)), delegate
			{
			});
			GameSection.StopEvent();
			return;
		}
		newEquipments = new StatusEquip.ChangeEquipData[7];
		selectedWeaponIds = new List<ulong>(3);
		for (int i = 0; i < newEquipments.Length; i++)
		{
			newEquipments[i] = new StatusEquip.ChangeEquipData(selectEquipSetData.setNo, i, null);
			switch (i)
			{
			case 0:
				SelectWeapon0();
				break;
			case 1:
				SelectWeapon1();
				break;
			case 2:
				SelectWeapon2();
				break;
			case 4:
				SelectHelm();
				break;
			case 3:
				SelectArmor();
				break;
			case 5:
				SelectArm();
				break;
			case 6:
				SelectLeg();
				break;
			}
		}
		GameSection.SetEventData(newEquipments);
		if (isMismatchElem)
		{
			GameSection.ChangeEvent("ELMENT_WARNING");
		}
		else
		{
			GameSection.ChangeEvent("COMPLETE");
		}
	}

	private void OnQuery_StatusAutoEquipWarningElementDialog_OK()
	{
		GameSection.SetEventData(newEquipments);
	}

	private void OnQuery_QuestAcceptArenaRoomAutoEquipWarningElementDialog_OK()
	{
		GameSection.SetEventData(newEquipments);
	}

	private void SelectWeapon0()
	{
		newEquipments[0].item = GetWeaponMaxAtk(validElement: true);
		if (elementIndex != 0 && ChangeElementToMasterDefineElement(elementIndex, ELEMENT_CONDITION.EQUAL) != (ELEMENT_TYPE)newEquipments[0].item.GetElemAtkType())
		{
			isMismatchElem = true;
		}
	}

	private void SelectWeapon1()
	{
		newEquipments[1].item = GetWeaponMaxAtk(validElement: true);
	}

	private void SelectWeapon2()
	{
		newEquipments[2].item = GetWeaponMaxAtk(validElement: true);
	}

	private void SelectHelm()
	{
		MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = InventoryManager.INVENTORY_TYPE.HELM;
		EquipItemInfo[] equipInventoryClone = MonoBehaviourSingleton<InventoryManager>.I.GetEquipInventoryClone();
		newEquipments[4].item = GetEquipMaxDef(equipInventoryClone, validElement: true, validAbilityWeaponType: true, checkOnlyFixAbility: true);
	}

	private void SelectArmor()
	{
		MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = InventoryManager.INVENTORY_TYPE.ARMOR;
		EquipItemInfo[] equipInventoryClone = MonoBehaviourSingleton<InventoryManager>.I.GetEquipInventoryClone();
		newEquipments[3].item = GetEquipMaxDef(equipInventoryClone, validElement: true, validAbilityWeaponType: true, checkOnlyFixAbility: true);
	}

	private void SelectArm()
	{
		MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = InventoryManager.INVENTORY_TYPE.ARM;
		EquipItemInfo[] equipInventoryClone = MonoBehaviourSingleton<InventoryManager>.I.GetEquipInventoryClone();
		newEquipments[5].item = GetEquipMaxDef(equipInventoryClone, validElement: true, validAbilityWeaponType: true, checkOnlyFixAbility: true);
	}

	private void SelectLeg()
	{
		MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = InventoryManager.INVENTORY_TYPE.LEG;
		EquipItemInfo[] equipInventoryClone = MonoBehaviourSingleton<InventoryManager>.I.GetEquipInventoryClone();
		newEquipments[6].item = GetEquipMaxDef(equipInventoryClone, validElement: true, validAbilityWeaponType: true, checkOnlyFixAbility: true);
	}

	private EquipItemInfo GetWeaponMaxAtk(bool validElement, ELEMENT_CONDITION condition = ELEMENT_CONDITION.EQUAL)
	{
		EquipItemInfo equipItemInfo = null;
		int num = -1;
		for (int i = 0; i < weaponItemInfoList.Length; i++)
		{
			if (validElement && elementIndex != 0)
			{
				ELEMENT_TYPE eLEMENT_TYPE = ChangeElementToMasterDefineElement(elementIndex, condition);
				int elemAtkType = weaponItemInfoList[i].GetElemAtkType();
				if ((eLEMENT_TYPE != (ELEMENT_TYPE)elemAtkType && condition == ELEMENT_CONDITION.EQUAL) || ((eLEMENT_TYPE == (ELEMENT_TYPE)elemAtkType || 6 == elemAtkType) && condition == ELEMENT_CONDITION.DISADVANTAGEOUS))
				{
					continue;
				}
			}
			int num2 = 0;
			if (isCurrent)
			{
				num2 = weaponItemInfoList[i].atk + weaponItemInfoList[i].elemAtk;
			}
			else
			{
				EquipItemTable.EquipItemData tableData = weaponItemInfoList[i].tableData;
				GrowEquipItemTable.GrowEquipItemData growEquipItemData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemData(tableData.growID, (uint)tableData.maxLv);
				if (growEquipItemData != null)
				{
					EquipItemExceedParamTable.EquipItemExceedParamAll equipItemExceedParamAll = tableData.GetExceedParam((uint)weaponItemInfoList[i].exceed);
					if (equipItemExceedParamAll == null)
					{
						equipItemExceedParamAll = new EquipItemExceedParamTable.EquipItemExceedParamAll();
					}
					int num3 = growEquipItemData.GetGrowParamAtk(tableData.baseAtk) + (int)equipItemExceedParamAll.atk;
					int[] growParamElemAtk = growEquipItemData.GetGrowParamElemAtk(tableData.atkElement);
					int j = 0;
					for (int num4 = growParamElemAtk.Length; j < num4; j++)
					{
						growParamElemAtk[j] += equipItemExceedParamAll.atkElement[j];
					}
					int num5 = Mathf.Max(growParamElemAtk);
					num2 = num3 + num5;
				}
			}
			if (num2 != 0)
			{
				int num6 = (int)weaponItemInfoList[i].tableData.type;
				if (num6 >= 4)
				{
					num6--;
				}
				if (num6 < MonoBehaviourSingleton<GlobalSettingsManager>.I.playerWeaponAttackRate.Length)
				{
					num2 = (int)((float)num2 / MonoBehaviourSingleton<GlobalSettingsManager>.I.playerWeaponAttackRate[num6]);
				}
			}
			if (num2 > num && !selectedWeaponIds.Contains(weaponItemInfoList[i].uniqueID))
			{
				equipItemInfo = weaponItemInfoList[i];
				num = num2;
			}
		}
		if (equipItemInfo != null)
		{
			selectedWeaponIds.Add(equipItemInfo.uniqueID);
		}
		if (equipItemInfo == null && validElement && condition == ELEMENT_CONDITION.EQUAL)
		{
			equipItemInfo = GetWeaponMaxAtk(validElement: true, ELEMENT_CONDITION.DISADVANTAGEOUS);
		}
		else if (equipItemInfo == null && validElement && condition == ELEMENT_CONDITION.DISADVANTAGEOUS)
		{
			equipItemInfo = GetWeaponMaxAtk(validElement: false);
		}
		return equipItemInfo;
	}

	private EquipItemInfo GetEquipMaxDef(EquipItemInfo[] items, bool validElement, bool validAbilityWeaponType, bool checkOnlyFixAbility)
	{
		EquipItemInfo equipItemInfo = null;
		int num = -1;
		int num2 = -1;
		for (int i = 0; i < items.Length; i++)
		{
			if ((validAbilityWeaponType && weaponIndex != 0 && !HasFixAbilityWithWeaponType(items[i], weaponIndex, checkOnlyFixAbility)) || (validElement && elementIndex != 0 && ChangeElementToMasterDefineElement(elementIndex, ELEMENT_CONDITION.EFFECTIVE_DEF) != (ELEMENT_TYPE)items[i].GetElemDefType()))
			{
				continue;
			}
			int num3 = 0;
			int num4 = 0;
			if (isCurrent)
			{
				num3 = items[i].def + items[i].elemDef;
				num4 = items[i].hp;
			}
			else
			{
				EquipItemTable.EquipItemData tableData = items[i].tableData;
				GrowEquipItemTable.GrowEquipItemData growEquipItemData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemData(tableData.growID, (uint)tableData.maxLv);
				if (growEquipItemData != null)
				{
					EquipItemExceedParamTable.EquipItemExceedParamAll equipItemExceedParamAll = tableData.GetExceedParam(4u);
					if (equipItemExceedParamAll == null)
					{
						equipItemExceedParamAll = new EquipItemExceedParamTable.EquipItemExceedParamAll();
					}
					int num5 = growEquipItemData.GetGrowParamDef(tableData.baseDef) + (int)equipItemExceedParamAll.def;
					int[] growParamElemDef = growEquipItemData.GetGrowParamElemDef(tableData.defElement);
					int j = 0;
					for (int num6 = growParamElemDef.Length; j < num6; j++)
					{
						growParamElemDef[j] += equipItemExceedParamAll.defElement[j];
					}
					int num7 = Mathf.Max(growParamElemDef);
					num3 = num5 + num7;
					num4 = growEquipItemData.GetGrowParamHp(tableData.baseHp) + (int)equipItemExceedParamAll.hp;
				}
			}
			if (num3 > num || (num3 == num && num4 > num2))
			{
				equipItemInfo = items[i];
				num = num3;
				num2 = num4;
			}
		}
		if ((equipItemInfo == null && validElement) & validAbilityWeaponType & checkOnlyFixAbility)
		{
			equipItemInfo = GetEquipMaxDef(items, validElement: true, validAbilityWeaponType: true, checkOnlyFixAbility: false);
		}
		else if (((equipItemInfo == null && validElement) & validAbilityWeaponType) && !checkOnlyFixAbility)
		{
			equipItemInfo = GetEquipMaxDef(items, validElement: false, validAbilityWeaponType: true, checkOnlyFixAbility: true);
		}
		else if ((equipItemInfo == null && !validElement) & validAbilityWeaponType & checkOnlyFixAbility)
		{
			equipItemInfo = GetEquipMaxDef(items, validElement: false, validAbilityWeaponType: true, checkOnlyFixAbility: false);
		}
		else if (((equipItemInfo == null && !validElement) & validAbilityWeaponType) && !checkOnlyFixAbility)
		{
			equipItemInfo = GetEquipMaxDef(items, validElement: true, validAbilityWeaponType: false, checkOnlyFixAbility: false);
		}
		else if (equipItemInfo == null)
		{
			equipItemInfo = GetEquipMaxDef(items, validElement: false, validAbilityWeaponType: false, checkOnlyFixAbility: false);
		}
		return equipItemInfo;
	}

	private int GetToggleIndex(UI[] uiArray)
	{
		int result = 0;
		for (int i = 0; i < uiArray.Length; i++)
		{
			UIToggle component = GetCtrl(uiArray[i]).GetComponent<UIToggle>();
			if (!(component == null) && component.value)
			{
				result = i;
				break;
			}
		}
		return result;
	}

	private InventoryManager.INVENTORY_TYPE ChangeWeaponTypeToInventoryType(int weaponIndex)
	{
		InventoryManager.INVENTORY_TYPE result = InventoryManager.INVENTORY_TYPE.ALL_WEAPON;
		switch (weaponIndex)
		{
		case 1:
			result = InventoryManager.INVENTORY_TYPE.ONE_HAND_SWORD;
			break;
		case 2:
			result = InventoryManager.INVENTORY_TYPE.TWO_HAND_SWORD;
			break;
		case 3:
			result = InventoryManager.INVENTORY_TYPE.SPEAR;
			break;
		case 4:
			result = InventoryManager.INVENTORY_TYPE.PAIR_SWORDS;
			break;
		case 5:
			result = InventoryManager.INVENTORY_TYPE.ARROW;
			break;
		}
		return result;
	}

	private ELEMENT_TYPE ChangeElementToMasterDefineElement(int elementIndex, ELEMENT_CONDITION condition)
	{
		ELEMENT_TYPE result = ELEMENT_TYPE.MAX;
		switch (elementIndex)
		{
		case 1:
			switch (condition)
			{
			case ELEMENT_CONDITION.EQUAL:
				result = ELEMENT_TYPE.FIRE;
				break;
			case ELEMENT_CONDITION.EFFECTIVE_DEF:
				result = ELEMENT_TYPE.SOIL;
				break;
			default:
				result = ELEMENT_TYPE.THUNDER;
				break;
			}
			break;
		case 2:
			switch (condition)
			{
			case ELEMENT_CONDITION.EQUAL:
				result = ELEMENT_TYPE.WATER;
				break;
			case ELEMENT_CONDITION.EFFECTIVE_DEF:
				result = ELEMENT_TYPE.FIRE;
				break;
			default:
				result = ELEMENT_TYPE.SOIL;
				break;
			}
			break;
		case 3:
			switch (condition)
			{
			case ELEMENT_CONDITION.EQUAL:
				result = ELEMENT_TYPE.THUNDER;
				break;
			case ELEMENT_CONDITION.EFFECTIVE_DEF:
				result = ELEMENT_TYPE.WATER;
				break;
			default:
				result = ELEMENT_TYPE.FIRE;
				break;
			}
			break;
		case 4:
			switch (condition)
			{
			case ELEMENT_CONDITION.EQUAL:
				result = ELEMENT_TYPE.SOIL;
				break;
			case ELEMENT_CONDITION.EFFECTIVE_DEF:
				result = ELEMENT_TYPE.THUNDER;
				break;
			default:
				result = ELEMENT_TYPE.WATER;
				break;
			}
			break;
		case 5:
			switch (condition)
			{
			case ELEMENT_CONDITION.EQUAL:
				result = ELEMENT_TYPE.LIGHT;
				break;
			case ELEMENT_CONDITION.EFFECTIVE_DEF:
				result = ELEMENT_TYPE.DARK;
				break;
			default:
				result = ELEMENT_TYPE.MAX;
				break;
			}
			break;
		case 6:
			switch (condition)
			{
			case ELEMENT_CONDITION.EQUAL:
				result = ELEMENT_TYPE.DARK;
				break;
			case ELEMENT_CONDITION.EFFECTIVE_DEF:
				result = ELEMENT_TYPE.LIGHT;
				break;
			default:
				result = ELEMENT_TYPE.MAX;
				break;
			}
			break;
		}
		return result;
	}

	private bool HasFixAbilityWithWeaponType(EquipItemInfo item, int wepIndex, bool checkOnlyFixAbility)
	{
		if (item.ability == null || item.ability.Length == 0)
		{
			return false;
		}
		for (int i = 0; i < item.ability.Length; i++)
		{
			if (checkOnlyFixAbility && !item.IsFixedAbility(i))
			{
				continue;
			}
			AbilityDataTable.AbilityData abilityData = Singleton<AbilityDataTable>.I.GetAbilityData(item.ability[i].id, item.ability[i].ap);
			if (abilityData == null)
			{
				continue;
			}
			ENABLE_EQUIP_TYPE enableEquipType = abilityData.enableEquipType;
			if (enableEquipType != 0)
			{
				if (enableEquipType == ENABLE_EQUIP_TYPE.ONE_HAND_SWORD && wepIndex == 1)
				{
					return true;
				}
				if (enableEquipType == ENABLE_EQUIP_TYPE.TWO_HAND_SWORD && wepIndex == 2)
				{
					return true;
				}
				if (enableEquipType == ENABLE_EQUIP_TYPE.SPEAR && wepIndex == 3)
				{
					return true;
				}
				if (enableEquipType == ENABLE_EQUIP_TYPE.PAIR_SWORDS && wepIndex == 4)
				{
					return true;
				}
				if (enableEquipType == ENABLE_EQUIP_TYPE.ARROW && wepIndex == 5)
				{
					return true;
				}
			}
			AbilityDataTable.AbilityData.AbilityInfo[] info = abilityData.info;
			for (int j = 0; j < info.Length; j++)
			{
				if (!string.IsNullOrEmpty(info[j].target))
				{
					string target = info[j].target;
					if (target == "ONE_HAND_SWORD" && wepIndex == 1)
					{
						return true;
					}
					if (target == "TWO_HAND_SWORD" && wepIndex == 2)
					{
						return true;
					}
					if (target == "SPEAR" && wepIndex == 3)
					{
						return true;
					}
					if (target == "PAIR_SWORDS" && wepIndex == 4)
					{
						return true;
					}
					if (target == "ARROW" && wepIndex == 5)
					{
						return true;
					}
				}
			}
		}
		return false;
	}
}
