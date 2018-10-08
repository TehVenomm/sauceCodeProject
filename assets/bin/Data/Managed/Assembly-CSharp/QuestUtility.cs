using Network;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class QuestUtility
{
	private static readonly ARENA_LIMIT[] WeaponTypeLimit = new ARENA_LIMIT[5]
	{
		ARENA_LIMIT.WEAPON_ONE_HAND_SWORD,
		ARENA_LIMIT.WEAPON_TWO_HAND_SWORD,
		ARENA_LIMIT.WEAPON_SPEAR,
		ARENA_LIMIT.WEAPON_PAIR_SWORDS,
		ARENA_LIMIT.WEAPON_ARROW
	};

	private static readonly ARENA_LIMIT[] SpAttackTypeLimit = new ARENA_LIMIT[4]
	{
		ARENA_LIMIT.ACTION_TYPE_NORMAL,
		ARENA_LIMIT.ACTION_TYPE_HEAT,
		ARENA_LIMIT.ACTION_TYPE_SOUL,
		ARENA_LIMIT.ACTION_TYPE_BURST
	};

	private static readonly ARENA_LIMIT[] WeaponNumTypeLimit = new ARENA_LIMIT[2]
	{
		ARENA_LIMIT.EQUIP_ONLY_ONE_WEAPON,
		ARENA_LIMIT.EQUIP_TWO_WEAPONS
	};

	public static bool JudgeLimit(ArenaTable.ArenaData arenaData, EquipSetInfo equipSet)
	{
		return JudgeLimit(arenaData, CreateEquipItemData(equipSet));
	}

	public static bool JudgeLimit(ArenaTable.ArenaData arenaData, List<CharaInfo.EquipItem> equipSet)
	{
		return JudgeLimit(arenaData, CreateEquipItemData(equipSet));
	}

	public static bool JudgeLimit(ArenaTable.ArenaData arenaData, EquipItemTable.EquipItemData[] equipSet)
	{
		List<EQUIPMENT_TYPE> list = new List<EQUIPMENT_TYPE>();
		List<SP_ATTACK_TYPE> list2 = new List<SP_ATTACK_TYPE>();
		int num = 3;
		int i = 0;
		for (int num2 = arenaData.limits.Length; i < num2; i++)
		{
			ARENA_LIMIT aRENA_LIMIT = arenaData.limits[i];
			if (aRENA_LIMIT != 0)
			{
				if (WeaponTypeLimit.Contains(aRENA_LIMIT))
				{
					list.Add(GetEquipmentType(aRENA_LIMIT));
				}
				else if (SpAttackTypeLimit.Contains(aRENA_LIMIT))
				{
					list2.Add(GetSPAttackType(aRENA_LIMIT));
				}
				else if (WeaponNumTypeLimit.Contains(aRENA_LIMIT))
				{
					num = GetEquipmentNum(aRENA_LIMIT);
				}
			}
		}
		bool flag = true;
		if (list.Count >= 1)
		{
			flag &= JudgeLimitWeapon(list, equipSet);
		}
		if (list2.Count >= 1)
		{
			flag &= JudgeLimitWeaponAttackType(list2, equipSet);
		}
		if (num <= 2)
		{
			flag &= JudgeLimitWeaponNum(num, equipSet);
		}
		return flag;
	}

	public static string CreateTimeStringByMilliSec(int milliSecond)
	{
		float num = 0.001f;
		int num2 = (int)((float)milliSecond * 0.001f);
		int num3 = num2 / 60;
		num2 %= 60;
		milliSecond %= 1000;
		return $"{num3:d2}:{num2:d2}.{milliSecond:d3}";
	}

	public static bool IsDefaultArenaTime(int milliSecond)
	{
		if (milliSecond == GetDefaultArenaTime())
		{
			return true;
		}
		return false;
	}

	public static int GetDefaultArenaTime()
	{
		return 3599999;
	}

	public static int ToSecByMilliSec(int milliSecond)
	{
		return (int)((float)milliSecond * 0.001f);
	}

	private static EQUIPMENT_TYPE GetEquipmentType(ARENA_LIMIT limitType)
	{
		switch (limitType)
		{
		case ARENA_LIMIT.WEAPON_ONE_HAND_SWORD:
			return EQUIPMENT_TYPE.ONE_HAND_SWORD;
		case ARENA_LIMIT.WEAPON_TWO_HAND_SWORD:
			return EQUIPMENT_TYPE.TWO_HAND_SWORD;
		case ARENA_LIMIT.WEAPON_SPEAR:
			return EQUIPMENT_TYPE.SPEAR;
		case ARENA_LIMIT.WEAPON_PAIR_SWORDS:
			return EQUIPMENT_TYPE.PAIR_SWORDS;
		case ARENA_LIMIT.WEAPON_ARROW:
			return EQUIPMENT_TYPE.ARROW;
		default:
			return EQUIPMENT_TYPE.NONE;
		}
	}

	private static SP_ATTACK_TYPE GetSPAttackType(ARENA_LIMIT limitType)
	{
		switch (limitType)
		{
		case ARENA_LIMIT.ACTION_TYPE_NORMAL:
			return SP_ATTACK_TYPE.NONE;
		case ARENA_LIMIT.ACTION_TYPE_HEAT:
			return SP_ATTACK_TYPE.HEAT;
		case ARENA_LIMIT.ACTION_TYPE_SOUL:
			return SP_ATTACK_TYPE.SOUL;
		case ARENA_LIMIT.ACTION_TYPE_BURST:
			return SP_ATTACK_TYPE.BURST;
		default:
			return SP_ATTACK_TYPE.NONE;
		}
	}

	private static int GetEquipmentNum(ARENA_LIMIT limitType)
	{
		switch (limitType)
		{
		case ARENA_LIMIT.EQUIP_ONLY_ONE_WEAPON:
			return 1;
		case ARENA_LIMIT.EQUIP_TWO_WEAPONS:
			return 2;
		default:
			return 3;
		}
	}

	private static bool JudgeLimitWeapon(List<EQUIPMENT_TYPE> allowTypes, EquipItemTable.EquipItemData[] equips)
	{
		int i = 0;
		for (int count = allowTypes.Count; i < count; i++)
		{
			if (!Singleton<EquipItemTable>.I.IsWeapon(allowTypes[i]))
			{
				Debug.LogError("武器のチェックに武器以外のタイプが渡されています(無視されます)");
			}
		}
		int j = 0;
		for (int num = equips.Length; j < num; j++)
		{
			if (equips[j] != null)
			{
				EQUIPMENT_TYPE type = equips[j].type;
				if (Singleton<EquipItemTable>.I.IsWeapon(type) && !allowTypes.Contains(type))
				{
					return false;
				}
			}
		}
		return true;
	}

	private static bool JudgeLimitWeaponAttackType(List<SP_ATTACK_TYPE> allowTypes, EquipItemTable.EquipItemData[] equips)
	{
		int i = 0;
		for (int num = equips.Length; i < num; i++)
		{
			if (equips[i] != null)
			{
				EquipItemTable.EquipItemData equipItemData = equips[i];
				if (Singleton<EquipItemTable>.I.IsWeapon(equipItemData.type) && !allowTypes.Contains(equipItemData.spAttackType))
				{
					return false;
				}
			}
		}
		return true;
	}

	private static bool JudgeLimitWeaponNum(int limitNum, EquipItemTable.EquipItemData[] equips)
	{
		int num = 0;
		for (int i = 0; i < 3; i++)
		{
			if (equips[i] != null)
			{
				num++;
			}
		}
		return num <= limitNum;
	}

	public static string GetLimitText(ArenaTable.ArenaData arenaData)
	{
		string text = string.Empty;
		int i = 0;
		for (int num = arenaData.limits.Length; i < num; i++)
		{
			uint num2 = (uint)arenaData.limits[i];
			if (num2 != 0)
			{
				text = text + StringTable.Get(STRING_CATEGORY.ARENA_LIMIT, num2) + "\n";
			}
		}
		if (text.IsNullOrWhiteSpace())
		{
			text = StringTable.Get(STRING_CATEGORY.ARENA_LIMIT, 0u);
		}
		return text.TrimEnd();
	}

	public static string GetConditionText(ArenaTable.ArenaData arenaData)
	{
		string text = string.Empty;
		int i = 0;
		for (int num = arenaData.conditions.Length; i < num; i++)
		{
			uint num2 = (uint)arenaData.conditions[i];
			if (num2 != 0)
			{
				text = text + StringTable.Get(STRING_CATEGORY.ARENA_CONDITION, num2) + "\n";
			}
		}
		if (text.IsNullOrWhiteSpace())
		{
			text = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 18u);
		}
		return text.TrimEnd();
	}

	public static string GetEndDateString(Network.EventData eventData)
	{
		string date = eventData.endDate.date;
		return "~" + date.Substring(5, 11).Replace("-", "/");
	}

	public static string GetArenaTitle(ARENA_GROUP group, string subTitle)
	{
		string str = StringTable.Format(STRING_CATEGORY.ARENA, 0u, group);
		return str + " " + subTitle;
	}

	public static string GetArenaTitle(ARENA_GROUP group, ARENA_RANK rank)
	{
		string arg = StringTable.Format(STRING_CATEGORY.ARENA, 0u, group);
		string text = StringTable.Format(STRING_CATEGORY.ARENA, 1u, rank);
		return arg + " " + rank;
	}

	private static EquipItemTable.EquipItemData[] CreateEquipItemData(List<CharaInfo.EquipItem> equips)
	{
		EquipItemTable.EquipItemData[] array = new EquipItemTable.EquipItemData[equips.Count];
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			EquipItemTable.EquipItemData equipItemData = array[i] = Singleton<EquipItemTable>.I.GetEquipItemData((uint)equips[i].eId);
		}
		return array;
	}

	private static EquipItemTable.EquipItemData[] CreateEquipItemData(EquipSetInfo equips)
	{
		EquipItemTable.EquipItemData[] array = new EquipItemTable.EquipItemData[equips.item.Length];
		int i = 0;
		for (int num = array.Length; i < num; i++)
		{
			if (equips.item[i] != null)
			{
				EquipItemTable.EquipItemData equipItemData = array[i] = equips.item[i].tableData;
			}
		}
		return array;
	}
}
