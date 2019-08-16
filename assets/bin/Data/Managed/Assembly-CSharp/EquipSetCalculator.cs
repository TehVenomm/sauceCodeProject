using Network;
using System.Collections.Generic;

public class EquipSetCalculator
{
	private enum eDirtyState
	{
		Dirty,
		CalcFactor,
		CalcFinal
	}

	private EquipValue[] equipValues = new EquipValue[7];

	private StatusFactor cachedFactor = new StatusFactor();

	private SimpleStatus cachedFinal = new SimpleStatus();

	private int cachedWeaponIndex = -1;

	private int cachedHp;

	private int cachedAtk;

	private int cachedDef;

	private eDirtyState dirtyFlag;

	private int setWeaponIndex;

	private void _Reset()
	{
		for (int i = 0; i < 7; i++)
		{
			equipValues[i] = null;
		}
		cachedFinal.Reset();
		cachedFactor.Reset();
		cachedWeaponIndex = -1;
		cachedHp = 0;
		cachedAtk = 0;
		cachedDef = 0;
		dirtyFlag = eDirtyState.Dirty;
		setWeaponIndex = 0;
	}

	public void SetEquipSet(EquipSetInfo equipSet, int setNo, bool isUnique = false)
	{
		if (!object.ReferenceEquals(equipSet, null))
		{
			List<CharaInfo.EquipItem> list = null;
			list = ((!isUnique) ? equipSet.ConvertSelfEquipSetItemList(setNo, isAddNull: true) : equipSet.ConvertSelfUniqueEquipSetItemList(setNo, isAddNull: true));
			SetEquipSet(list, isAddNull: true);
		}
	}

	public void SetEquipSet(List<CharaInfo.EquipItem> equips, bool isAddNull = false)
	{
		if (!object.ReferenceEquals(equips, null))
		{
			_Reset();
			for (int i = 0; i < equips.Count; i++)
			{
				SetEquipItem(equips[i], (!isAddNull) ? (-1) : i);
			}
		}
	}

	public void SetEquipItem(CharaInfo.EquipItem item, int index)
	{
		if (object.ReferenceEquals(item, null))
		{
			if (index != -1)
			{
				equipValues[index] = null;
				dirtyFlag = eDirtyState.Dirty;
			}
			return;
		}
		EquipItemTable.EquipItemData equipItemData = Singleton<EquipItemTable>.I.GetEquipItemData((uint)item.eId);
		if (object.ReferenceEquals(equipItemData, null))
		{
			return;
		}
		if (index == -1)
		{
			switch (equipItemData.type)
			{
			default:
				return;
			case EQUIPMENT_TYPE.ONE_HAND_SWORD:
			case EQUIPMENT_TYPE.TWO_HAND_SWORD:
			case EQUIPMENT_TYPE.SPEAR:
			case EQUIPMENT_TYPE.PAIR_SWORDS:
			case EQUIPMENT_TYPE.ARROW:
				index = setWeaponIndex;
				setWeaponIndex++;
				break;
			case EQUIPMENT_TYPE.ARMOR:
				index = 3;
				break;
			case EQUIPMENT_TYPE.HELM:
				index = 4;
				break;
			case EQUIPMENT_TYPE.ARM:
				index = 5;
				break;
			case EQUIPMENT_TYPE.LEG:
				index = 6;
				break;
			}
		}
		EquipValue equipValue = new EquipValue();
		equipValue.Parse(item, equipItemData);
		equipValues[index] = equipValue;
		dirtyFlag = eDirtyState.Dirty;
	}

	public void SwapWeapon(int swapIndex, int nowIndex)
	{
		EquipValue equipValue = equipValues[nowIndex];
		equipValues[nowIndex] = equipValues[swapIndex];
		equipValues[swapIndex] = equipValue;
		dirtyFlag = eDirtyState.Dirty;
	}

	public StatusFactor GetStatusFactor(int weaponIndex)
	{
		if (weaponIndex < 0 || weaponIndex >= 3)
		{
			return null;
		}
		if (weaponIndex == cachedWeaponIndex && dirtyFlag >= eDirtyState.CalcFactor)
		{
			return cachedFactor;
		}
		cachedFactor.Reset();
		EQUIPMENT_TYPE weaponType = EQUIPMENT_TYPE.NONE;
		SP_ATTACK_TYPE spAttackType = SP_ATTACK_TYPE.NONE;
		for (int i = 0; i < 7; i++)
		{
			if (object.ReferenceEquals(equipValues[i], null))
			{
				continue;
			}
			EquipValue equipValue = equipValues[i];
			if (i < 3)
			{
				if (i != weaponIndex)
				{
					continue;
				}
				weaponType = equipValue.type;
				spAttackType = equipValue.spAttackType;
			}
			cachedFactor.baseStatus.hp += equipValue.baseStatus.hp;
			cachedFactor.constHp += equipValue.constHp;
			for (int j = 0; j < 7; j++)
			{
				cachedFactor.baseStatus.attacks[j] += equipValue.baseStatus.attacks[j];
				cachedFactor.constAtks[j] += equipValue.constAtks[j];
			}
			cachedFactor.baseStatus.defences[0] += equipValue.baseStatus.defences[0];
			cachedFactor.constDefs[0] += equipValue.constDefs[0];
			for (int k = 0; k < 6; k++)
			{
				cachedFactor.baseStatus.tolerances[k] += equipValue.baseStatus.tolerances[k];
				cachedFactor.constTols[k] += equipValue.constTols[k];
			}
			_GetEnableSkillSupport(weaponType, spAttackType, equipValue);
		}
		for (int l = 0; l < 6; l++)
		{
			cachedFactor.baseStatus.defences[l + 1] += cachedFactor.baseStatus.defences[0];
		}
		cachedFactor.CheckMinusRate();
		cachedWeaponIndex = weaponIndex;
		dirtyFlag = eDirtyState.CalcFactor;
		return cachedFactor;
	}

	public SimpleStatus GetFinalStatus(int weaponIndex, UserStatus status)
	{
		return GetFinalStatus(weaponIndex, status.hp, status.atk, status.def);
	}

	public SimpleStatus GetFinalStatus(int weaponIndex, int userHp, int userAtk, int userDef)
	{
		if (weaponIndex == cachedWeaponIndex && userHp == cachedHp && userAtk == cachedAtk && userDef == cachedDef && dirtyFlag == eDirtyState.CalcFinal)
		{
			return cachedFinal;
		}
		GetStatusFactor(weaponIndex);
		cachedFinal.Reset();
		cachedFinal.hp = (int)((float)(userHp + cachedFactor.baseStatus.hp) * cachedFactor.hpRate + (float)cachedFactor.constHp);
		if (cachedFinal.hp < 1)
		{
			cachedFinal.hp = 1;
		}
		for (int i = 0; i < 7; i++)
		{
			if (i == 0)
			{
				cachedFinal.attacks[i] = (int)((float)(userAtk + cachedFactor.baseStatus.attacks[i]) * cachedFactor.atkRate[i] + (float)cachedFactor.constAtks[i]);
			}
			else
			{
				cachedFinal.attacks[i] = (int)((float)cachedFactor.baseStatus.attacks[i] * cachedFactor.atkRate[i] + (float)cachedFactor.constAtks[i]);
			}
			if (cachedFinal.attacks[i] < 0)
			{
				cachedFinal.attacks[i] = 0;
			}
			cachedFinal.defences[i] = (int)((float)(userDef + cachedFactor.baseStatus.defences[i]) * cachedFactor.defRate[i] + (float)cachedFactor.constDefs[i]);
			if (cachedFinal.defences[i] < 0)
			{
				cachedFinal.defences[i] = 0;
			}
		}
		for (int j = 0; j < 6; j++)
		{
			cachedFinal.tolerances[j] = (int)((float)cachedFactor.baseStatus.tolerances[j] * cachedFactor.tolRate[j] + (float)cachedFactor.constTols[j]);
		}
		cachedHp = userHp;
		cachedAtk = userAtk;
		cachedDef = userDef;
		dirtyFlag = eDirtyState.CalcFinal;
		return cachedFinal;
	}

	private void _GetEnableSkillSupport(EQUIPMENT_TYPE weaponType, SP_ATTACK_TYPE spAttackType, EquipValue equip)
	{
		int i = 0;
		for (int count = equip.skillSupport.Count; i < count; i++)
		{
			EquipValue.SkillSupport skillSupport = equip.skillSupport[i];
			if (!Utility.IsEnableEquip(weaponType, skillSupport.targetEquip) || !Utility.IsEnableSpAttackType(skillSupport.targetSpAttackType, spAttackType))
			{
				continue;
			}
			switch (skillSupport.type)
			{
			case BuffParam.BUFFTYPE.HP_UP:
				cachedFactor.constHp += skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.HP_DOWN:
				cachedFactor.constHp -= skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.HPUP_RATE:
				cachedFactor.hpRate += (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.HPDOWN_RATE:
				cachedFactor.hpRate -= (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.ATTACK_NORMAL:
				cachedFactor.constAtks[0] += skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.ATTACK_FIRE:
				cachedFactor.constAtks[1] += skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.ATTACK_WATER:
				cachedFactor.constAtks[2] += skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.ATTACK_THUNDER:
				cachedFactor.constAtks[3] += skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.ATTACK_SOIL:
				cachedFactor.constAtks[4] += skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.ATTACK_LIGHT:
				cachedFactor.constAtks[5] += skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.ATTACK_DARK:
				cachedFactor.constAtks[6] += skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.ATTACK_ALLELEMENT:
				for (int num7 = 0; num7 < 6; num7++)
				{
					cachedFactor.constAtks[num7 + 1] += skillSupport.value;
				}
				break;
			case BuffParam.BUFFTYPE.ATTACK_DOWN_NORMAL:
				cachedFactor.constAtks[0] -= skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.ATTACK_DOWN_FIRE:
				cachedFactor.constAtks[1] -= skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.ATTACK_DOWN_WATER:
				cachedFactor.constAtks[2] -= skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.ATTACK_DOWN_THUNDER:
				cachedFactor.constAtks[3] -= skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.ATTACK_DOWN_SOIL:
				cachedFactor.constAtks[4] -= skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.ATTACK_DOWN_LIGHT:
				cachedFactor.constAtks[5] -= skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.ATTACK_DOWN_DARK:
				cachedFactor.constAtks[6] -= skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.ATTACK_DOWN_ALLELEMENT:
				for (int num6 = 0; num6 < 6; num6++)
				{
					cachedFactor.constAtks[num6 + 1] -= skillSupport.value;
				}
				break;
			case BuffParam.BUFFTYPE.ATKUP_RATE_NORMAL:
				cachedFactor.atkRate[0] += (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.ATKUP_RATE_FIRE:
				cachedFactor.atkRate[1] += (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.ATKUP_RATE_WATER:
				cachedFactor.atkRate[2] += (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.ATKUP_RATE_THUNDER:
				cachedFactor.atkRate[3] += (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.ATKUP_RATE_SOIL:
				cachedFactor.atkRate[4] += (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.ATKUP_RATE_LIGHT:
				cachedFactor.atkRate[5] += (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.ATKUP_RATE_DARK:
				cachedFactor.atkRate[6] += (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.ATKUP_RATE_ALL:
				cachedFactor.atkRate[0] += (float)skillSupport.value * 0.01f;
				goto case BuffParam.BUFFTYPE.ATKUP_RATE_ALLELEMENT;
			case BuffParam.BUFFTYPE.ATKUP_RATE_ALLELEMENT:
				for (int num5 = 0; num5 < 6; num5++)
				{
					cachedFactor.atkRate[num5 + 1] += (float)skillSupport.value * 0.01f;
				}
				break;
			case BuffParam.BUFFTYPE.ATKDOWN_RATE_NORMAL:
				cachedFactor.atkRate[0] -= (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.ATKDOWN_RATE_FIRE:
				cachedFactor.atkRate[1] -= (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.ATKDOWN_RATE_WATER:
				cachedFactor.atkRate[2] -= (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.ATKDOWN_RATE_THUNDER:
				cachedFactor.atkRate[3] -= (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.ATKDOWN_RATE_SOIL:
				cachedFactor.atkRate[4] -= (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.ATKDOWN_RATE_LIGHT:
				cachedFactor.atkRate[5] -= (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.ATKDOWN_RATE_DARK:
				cachedFactor.atkRate[6] -= (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.ATKDOWN_RATE_ALLELEMENT:
				for (int num4 = 0; num4 < 6; num4++)
				{
					cachedFactor.atkRate[num4 + 1] -= (float)skillSupport.value * 0.01f;
				}
				break;
			case BuffParam.BUFFTYPE.DEFENCE_NORMAL:
				cachedFactor.constDefs[0] += skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.DEFENCE_FIRE:
				cachedFactor.constDefs[1] += skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.DEFENCE_WATER:
				cachedFactor.constDefs[2] += skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.DEFENCE_THUNDER:
				cachedFactor.constDefs[3] += skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.DEFENCE_SOIL:
				cachedFactor.constDefs[4] += skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.DEFENCE_LIGHT:
				cachedFactor.constDefs[5] += skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.DEFENCE_DARK:
				cachedFactor.constDefs[6] += skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.DEFENCE_ALLELEMENT:
				for (int num3 = 0; num3 < 6; num3++)
				{
					cachedFactor.constDefs[num3 + 1] += skillSupport.value;
				}
				break;
			case BuffParam.BUFFTYPE.DEFENCE_DOWN_NORMAL:
				cachedFactor.constDefs[0] -= skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.DEFENCE_DOWN_FIRE:
				cachedFactor.constDefs[1] -= skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.DEFENCE_DOWN_WATER:
				cachedFactor.constDefs[2] -= skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.DEFENCE_DOWN_THUNDER:
				cachedFactor.constDefs[3] -= skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.DEFENCE_DOWN_SOIL:
				cachedFactor.constDefs[4] -= skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.DEFENCE_DOWN_LIGHT:
				cachedFactor.constDefs[5] -= skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.DEFENCE_DOWN_DARK:
				cachedFactor.constDefs[6] -= skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.DEFENCE_DOWN_ALLELEMENT:
				for (int num2 = 0; num2 < 6; num2++)
				{
					cachedFactor.constDefs[num2 + 1] -= skillSupport.value;
				}
				break;
			case BuffParam.BUFFTYPE.DEFUP_RATE_NORMAL:
				cachedFactor.defRate[0] += (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.DEFUP_RATE_FIRE:
				cachedFactor.defRate[1] += (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.DEFUP_RATE_WATER:
				cachedFactor.defRate[2] += (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.DEFUP_RATE_THUNDER:
				cachedFactor.defRate[3] += (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.DEFUP_RATE_SOIL:
				cachedFactor.defRate[4] += (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.DEFUP_RATE_LIGHT:
				cachedFactor.defRate[5] += (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.DEFUP_RATE_DARK:
				cachedFactor.defRate[6] += (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.DEFUP_RATE_ALLELEMENT:
				for (int num = 0; num < 6; num++)
				{
					cachedFactor.defRate[num + 1] += (float)skillSupport.value * 0.01f;
				}
				break;
			case BuffParam.BUFFTYPE.DEFDOWN_RATE_NORMAL:
				cachedFactor.defRate[0] -= (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.DEFDOWN_RATE_FIRE:
				cachedFactor.defRate[1] -= (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.DEFDOWN_RATE_WATER:
				cachedFactor.defRate[2] -= (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.DEFDOWN_RATE_THUNDER:
				cachedFactor.defRate[3] -= (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.DEFDOWN_RATE_SOIL:
				cachedFactor.defRate[4] -= (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.DEFDOWN_RATE_LIGHT:
				cachedFactor.defRate[5] -= (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.DEFDOWN_RATE_DARK:
				cachedFactor.defRate[6] -= (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.DEFDOWN_RATE_ALLELEMENT:
				for (int n = 0; n < 6; n++)
				{
					cachedFactor.defRate[n + 1] -= (float)skillSupport.value * 0.01f;
				}
				break;
			case BuffParam.BUFFTYPE.TOLERANCE_FIRE:
				cachedFactor.constTols[0] += skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.TOLERANCE_WATER:
				cachedFactor.constTols[1] += skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.TOLERANCE_THUNDER:
				cachedFactor.constTols[2] += skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.TOLERANCE_SOIL:
				cachedFactor.constTols[3] += skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.TOLERANCE_LIGHT:
				cachedFactor.constTols[4] += skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.TOLERANCE_DARK:
				cachedFactor.constTols[5] += skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.TOLERANCE_ALLELEMENT:
				for (int m = 0; m < 6; m++)
				{
					cachedFactor.constTols[m] += skillSupport.value;
				}
				break;
			case BuffParam.BUFFTYPE.TOLERANCE_DOWN_FIRE:
				cachedFactor.constTols[0] -= skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.TOLERANCE_DOWN_WATER:
				cachedFactor.constTols[1] -= skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.TOLERANCE_DOWN_THUNDER:
				cachedFactor.constTols[2] -= skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.TOLERANCE_DOWN_SOIL:
				cachedFactor.constTols[3] -= skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.TOLERANCE_DOWN_LIGHT:
				cachedFactor.constTols[4] -= skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.TOLERANCE_DOWN_DARK:
				cachedFactor.constTols[5] -= skillSupport.value;
				break;
			case BuffParam.BUFFTYPE.TOLERANCE_DOWN_ALLELEMENT:
				for (int l = 0; l < 6; l++)
				{
					cachedFactor.constTols[l] -= skillSupport.value;
				}
				break;
			case BuffParam.BUFFTYPE.TOLUP_RATE_FIRE:
				cachedFactor.tolRate[0] += (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.TOLUP_RATE_WATER:
				cachedFactor.tolRate[1] += (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.TOLUP_RATE_THUNDER:
				cachedFactor.tolRate[2] += (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.TOLUP_RATE_SOIL:
				cachedFactor.tolRate[3] += (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.TOLUP_RATE_LIGHT:
				cachedFactor.tolRate[4] += (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.TOLUP_RATE_DARK:
				cachedFactor.tolRate[5] += (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.TOLUP_RATE_ALLELEMENT:
				for (int k = 0; k < 6; k++)
				{
					cachedFactor.tolRate[k] += (float)skillSupport.value * 0.01f;
				}
				break;
			case BuffParam.BUFFTYPE.TOLDOWN_RATE_FIRE:
				cachedFactor.tolRate[0] -= (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.TOLDOWN_RATE_WATER:
				cachedFactor.tolRate[1] -= (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.TOLDOWN_RATE_THUNDER:
				cachedFactor.tolRate[2] -= (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.TOLDOWN_RATE_SOIL:
				cachedFactor.tolRate[3] -= (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.TOLDOWN_RATE_LIGHT:
				cachedFactor.tolRate[4] -= (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.TOLDOWN_RATE_DARK:
				cachedFactor.tolRate[5] -= (float)skillSupport.value * 0.01f;
				break;
			case BuffParam.BUFFTYPE.TOLDOWN_RATE_ALLELEMENT:
				for (int j = 0; j < 6; j++)
				{
					cachedFactor.tolRate[j] -= (float)skillSupport.value * 0.01f;
				}
				break;
			}
		}
	}
}
