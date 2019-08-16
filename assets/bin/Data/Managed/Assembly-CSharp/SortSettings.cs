using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SortSettings
{
	public enum SETTINGS_TYPE
	{
		EQUIP_ITEM,
		SKILL_ITEM,
		MATERIAL,
		USE_ITEM,
		CREATE_EQUIP_ITEM,
		GROW_BASE_SKILL_ITEM,
		GROW_SKILL_ITEM,
		STORAGE_EQUIP,
		STORAGE_SKILL,
		ORDER_QUEST,
		EXCEED_SKILL_ITEM,
		STORAGE_ABILITY_ITEM,
		ELEMENT,
		STORAGE_ACCESSORY,
		MAX
	}

	private enum DATA_INDEX
	{
		SETTINGS_TYPE,
		DIALOG_TYPE,
		REQUIREMENT,
		TYPE,
		RARITY,
		ORDER_TYPE_ASC,
		EQUIP_FILTER,
		ELEMENT
	}

	public class SortEquipSetInfo
	{
		public int equipSetNo;

		public bool isLocal;

		public int exclusionSlotIndex;

		public List<ulong> exclusionUniqID;

		public SortEquipSetInfo(int _set_no, bool _is_local, int _exclusion_slot, List<ulong> _exclusion_uniq_id)
		{
			equipSetNo = _set_no;
			isLocal = _is_local;
			exclusionSlotIndex = _exclusion_slot;
			exclusionUniqID = _exclusion_uniq_id;
		}
	}

	private static SortSettings[] memSettings;

	public SETTINGS_TYPE settingsType;

	public SortBase.DIALOG_TYPE dialogType;

	public SortBase.SORT_REQUIREMENT requirement = SortBase.SORT_REQUIREMENT.ELEMENT;

	public int type = 511;

	public int rarity = 127;

	public bool orderTypeAsc;

	public int equipFilter = 63;

	public int element = 127;

	private SortEquipSetInfo equipSetInfo;

	public SortBase.TYPE TYPE_ALL;

	public Comparison<SortCompareData> indivComparison;

	public static SortSettings CreateMemSortSettings(SortBase.DIALOG_TYPE dialog_type, SETTINGS_TYPE memory_type)
	{
		if (memory_type >= SETTINGS_TYPE.MAX)
		{
			return null;
		}
		if (memSettings == null)
		{
			memSettings = new SortSettings[14];
		}
		SortSettings sortSettings = memSettings[(int)memory_type];
		bool flag = sortSettings != null;
		if (!flag)
		{
			memSettings[(int)memory_type] = new SortSettings();
			if (GetMemorySortData(memory_type, ref memSettings[(int)memory_type]))
			{
				flag = true;
			}
			sortSettings = memSettings[(int)memory_type];
		}
		sortSettings.dialogType = dialog_type;
		switch (dialog_type)
		{
		case SortBase.DIALOG_TYPE.WEAPON:
		case SortBase.DIALOG_TYPE.ARMOR:
		case SortBase.DIALOG_TYPE.STORAGE_EQUIP:
		case SortBase.DIALOG_TYPE.TYPE_FILTERABLE_WEAPON:
		case SortBase.DIALOG_TYPE.TYPE_FILTERABLE_ARMOR:
		{
			sortSettings.TYPE_ALL = SortBase.TYPE.EQUIP_ALL;
			SortBase.SORT_REQUIREMENT sORT_REQUIREMENT = (dialog_type != SortBase.DIALOG_TYPE.ARMOR && dialog_type != SortBase.DIALOG_TYPE.TYPE_FILTERABLE_ARMOR) ? SortBase.SORT_REQUIREMENT.REQUIREMENT_WEAPON_BIT : SortBase.SORT_REQUIREMENT.REQUIREMENT_ARMORS_BIT;
			if (!flag)
			{
				sortSettings.requirement = SortBase.SORT_REQUIREMENT.ELEMENT;
			}
			else if ((sortSettings.requirement & sORT_REQUIREMENT) == (SortBase.SORT_REQUIREMENT)0)
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
			break;
		}
		case SortBase.DIALOG_TYPE.SMITH_CREATE_WEAPON:
		case SortBase.DIALOG_TYPE.SMITH_CREATE_ARMOR:
		case SortBase.DIALOG_TYPE.SMITH_CREATE_PICKUP_WEAPON:
		case SortBase.DIALOG_TYPE.SMITH_CREATE_PICKUP_ARMOR:
		{
			sortSettings.TYPE_ALL = SortBase.TYPE.NONE;
			SortBase.SORT_REQUIREMENT sORT_REQUIREMENT2;
			switch (dialog_type)
			{
			default:
				sORT_REQUIREMENT2 = SortBase.SORT_REQUIREMENT.REQUIREMENT_CREATE_WEAPON_BIT;
				break;
			case SortBase.DIALOG_TYPE.SMITH_CREATE_ARMOR:
				sORT_REQUIREMENT2 = SortBase.SORT_REQUIREMENT.REQUIREMENT_CREATE_ARMORS_BIT;
				break;
			case SortBase.DIALOG_TYPE.SMITH_CREATE_PICKUP_WEAPON:
				sORT_REQUIREMENT2 = SortBase.SORT_REQUIREMENT.REQUIREMENT_CREATE_PICKUP_WEAPON_BIT;
				break;
			case SortBase.DIALOG_TYPE.SMITH_CREATE_PICKUP_ARMOR:
				sORT_REQUIREMENT2 = SortBase.SORT_REQUIREMENT.REQUIREMENT_CREATE_PICKUP_ARMORS_BIT;
				break;
			}
			if (!flag)
			{
				sortSettings.requirement = SortBase.SORT_REQUIREMENT.ELEMENT;
			}
			else if ((sortSettings.requirement & sORT_REQUIREMENT2) == (SortBase.SORT_REQUIREMENT)0)
			{
				if (sortSettings.requirement == SortBase.SORT_REQUIREMENT.ATK)
				{
					sortSettings.requirement = SortBase.SORT_REQUIREMENT.DEF;
				}
				else if (sortSettings.requirement == SortBase.SORT_REQUIREMENT.DEF)
				{
					sortSettings.requirement = SortBase.SORT_REQUIREMENT.ATK;
				}
				else
				{
					sortSettings.requirement = SortBase.SORT_REQUIREMENT.ELEMENT;
				}
			}
			break;
		}
		case SortBase.DIALOG_TYPE.SKILL:
		case SortBase.DIALOG_TYPE.STORAGE_SKILL:
			sortSettings.TYPE_ALL = SortBase.TYPE.SKILL_ALL;
			if (!flag)
			{
				sortSettings.requirement = SortBase.SORT_REQUIREMENT.SKILL_TYPE;
			}
			break;
		case SortBase.DIALOG_TYPE.USE_ITEM:
		case SortBase.DIALOG_TYPE.MATERIAL:
			sortSettings.TYPE_ALL = SortBase.TYPE.WEAPON_ALL;
			if (!flag)
			{
				sortSettings.requirement = SortBase.SORT_REQUIREMENT.NUM;
			}
			break;
		case SortBase.DIALOG_TYPE.QUEST:
			sortSettings.TYPE_ALL = SortBase.TYPE.ENEMY_ALL;
			if (!flag)
			{
				sortSettings.requirement = SortBase.SORT_REQUIREMENT.RARITY;
			}
			break;
		default:
			sortSettings.requirement = SortBase.SORT_REQUIREMENT.RARITY;
			sortSettings.TYPE_ALL = SortBase.TYPE.NONE;
			break;
		}
		if (!flag && sortSettings.TYPE_ALL != 0)
		{
			sortSettings.type = (int)sortSettings.TYPE_ALL;
		}
		return sortSettings.Clone();
	}

	public static void DeleteMemSortSetting()
	{
		if (memSettings != null)
		{
			int i = 0;
			for (int num = memSettings.Length; i < num; i++)
			{
				memSettings[i] = null;
			}
			memSettings = null;
		}
	}

	private static int GetSortBitSettingsType(SETTINGS_TYPE settings_type)
	{
		return (int)settings_type;
	}

	private static int GetSortBitDialogType(SortBase.DIALOG_TYPE dialog_type)
	{
		return (int)dialog_type;
	}

	private static int GetSortBitRequirement(SortBase.SORT_REQUIREMENT requirement)
	{
		return (int)requirement;
	}

	private static int GetSortBitType(int type)
	{
		return type;
	}

	private static int GetSortBitRarity(int rarity)
	{
		return rarity;
	}

	private static int GetSortBitOrderTypeAsc(bool order_type_asc)
	{
		return order_type_asc ? 1 : 0;
	}

	private static int GetSortBitEquipFilter(int equipfilter)
	{
		return equipfilter;
	}

	public static string GetSortBit(SortSettings sort_settings)
	{
		StringBuilder stringBuilder = new StringBuilder();
		int num = 0;
		num = GetSortBitSettingsType(sort_settings.settingsType);
		stringBuilder.AppendFormat("{0},", num);
		num = GetSortBitDialogType(sort_settings.dialogType);
		stringBuilder.AppendFormat("{0},", num);
		num = GetSortBitRequirement(sort_settings.requirement);
		stringBuilder.AppendFormat("{0},", num);
		num = GetSortBitType(sort_settings.type);
		stringBuilder.AppendFormat("{0},", num);
		num = GetSortBitRarity(sort_settings.rarity);
		stringBuilder.AppendFormat("{0},", num);
		num = GetSortBitOrderTypeAsc(sort_settings.orderTypeAsc);
		stringBuilder.AppendFormat("{0},", num);
		num = GetSortBitEquipFilter(sort_settings.equipFilter);
		stringBuilder.AppendFormat("{0},", num);
		num = GetSortBitEquipFilter(sort_settings.element);
		stringBuilder.AppendFormat("{0}", num);
		return stringBuilder.ToString();
	}

	public static bool GetMemorySortData(SETTINGS_TYPE settings_type, ref SortSettings ret)
	{
		string sortBit = GameSaveData.instance.GetSortBit(settings_type);
		bool flag = !string.IsNullOrEmpty(sortBit);
		if (flag)
		{
			ret.settingsType = GetSettingsTypeBySortBit(sortBit);
			ret.dialogType = GetDialogTypeBySortBit(sortBit);
			ret.requirement = GetRequirementBySortBit(sortBit);
			ret.type = (int)GetTypeBySortBit(sortBit);
			ret.rarity = (int)GetRarityBySortBit(sortBit);
			ret.orderTypeAsc = GetOrderTypeAscBySortBit(sortBit);
			ret.equipFilter = (int)GetEquipFilterBySortBit(sortBit);
			ret.element = (int)GetElementBySortBit(sortBit);
		}
		else
		{
			ret.settingsType = settings_type;
		}
		return flag;
	}

	public static void DeleteMemorySortData(SETTINGS_TYPE settings_type)
	{
		GameSaveData.instance.DeleteSortBit(settings_type);
		GameSaveData.Save();
	}

	public static SETTINGS_TYPE GetSettingsTypeBySortBit(string bit)
	{
		return (SETTINGS_TYPE)GetBitRange(bit, DATA_INDEX.SETTINGS_TYPE);
	}

	private static SortBase.DIALOG_TYPE GetDialogTypeBySortBit(string bit)
	{
		return (SortBase.DIALOG_TYPE)GetBitRange(bit, DATA_INDEX.DIALOG_TYPE);
	}

	private static SortBase.SORT_REQUIREMENT GetRequirementBySortBit(string bit)
	{
		return (SortBase.SORT_REQUIREMENT)GetBitRange(bit, DATA_INDEX.REQUIREMENT);
	}

	private static SortBase.TYPE GetTypeBySortBit(string bit)
	{
		return (SortBase.TYPE)GetBitRange(bit, DATA_INDEX.TYPE);
	}

	private static SortBase.RARITY GetRarityBySortBit(string bit)
	{
		return (SortBase.RARITY)GetBitRange(bit, DATA_INDEX.RARITY);
	}

	private static bool GetOrderTypeAscBySortBit(string bit)
	{
		return GetBitRange(bit, DATA_INDEX.ORDER_TYPE_ASC) == 1;
	}

	private static SortBase.EQUIP_FILTER GetEquipFilterBySortBit(string bit)
	{
		return (SortBase.EQUIP_FILTER)GetBitRange(bit, DATA_INDEX.EQUIP_FILTER);
	}

	private static SortBase.ELEMENT GetElementBySortBit(string bit)
	{
		return (SortBase.ELEMENT)GetBitRange(bit, DATA_INDEX.ELEMENT);
	}

	private static int GetBitRange(string bit, DATA_INDEX data_index)
	{
		string[] array = bit.Split(',');
		int result = 0;
		if ((int)data_index < array.Length)
		{
			int.TryParse(array[(int)data_index], out result);
		}
		else
		{
			result = int.MaxValue;
		}
		return result;
	}

	public void ResetType(bool isEnable = true)
	{
		type = (int)(isEnable ? TYPE_ALL : SortBase.TYPE.NONE);
	}

	public SortSettings Clone()
	{
		SortSettings sortSettings = new SortSettings();
		sortSettings.dialogType = dialogType;
		sortSettings.rarity = rarity;
		sortSettings.type = type;
		sortSettings.requirement = requirement;
		sortSettings.orderTypeAsc = orderTypeAsc;
		sortSettings.settingsType = settingsType;
		sortSettings.equipSetInfo = equipSetInfo;
		sortSettings.indivComparison = indivComparison;
		sortSettings.equipFilter = equipFilter;
		sortSettings.element = element;
		sortSettings.TYPE_ALL = TYPE_ALL;
		return sortSettings;
	}

	public SORT_DATA[] CreateSortAry<T, SORT_DATA>(T[] target_ary) where SORT_DATA : SortCompareData, new()
	{
		if (target_ary == null)
		{
			return null;
		}
		SORT_DATA[] array = SortCompareData.CreateSortDataAry<T, SORT_DATA>(target_ary, this, equipSetInfo);
		Sort(array);
		return array;
	}

	public bool Sort<SORT_DATA>(SORT_DATA[] sort_target_ary) where SORT_DATA : SortCompareData, new()
	{
		memSettings[(int)settingsType] = this;
		SortCompareData.InitSortDataAry(sort_target_ary, this, equipSetInfo);
		DataSort(sort_target_ary);
		return true;
	}

	private void DataSort<SORT_DATA>(SORT_DATA[] ary) where SORT_DATA : SortCompareData
	{
		if (ary.Length > 1)
		{
			Comparison<SortCompareData> comparison = indivComparison ?? new SortComparison(orderTypeAsc).comparison;
			Array.Sort(ary, comparison);
		}
	}

	public string GetSortLabel()
	{
		int id = (int)Mathf.Log((float)requirement, 2f);
		return StringTable.Get(STRING_CATEGORY.SORT, (uint)id);
	}
}
