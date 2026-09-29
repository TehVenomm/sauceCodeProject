// Decompiled with JetBrains decompiler
// Type: QuestUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
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
  private static readonly ARENA_LIMIT[] SpAttackTypeLimit = new ARENA_LIMIT[5]
  {
    ARENA_LIMIT.ACTION_TYPE_NORMAL,
    ARENA_LIMIT.ACTION_TYPE_HEAT,
    ARENA_LIMIT.ACTION_TYPE_SOUL,
    ARENA_LIMIT.ACTION_TYPE_BURST,
    ARENA_LIMIT.ACTION_TYPE_ORACLE
  };
  private static readonly ARENA_LIMIT[] WeaponNumTypeLimit = new ARENA_LIMIT[2]
  {
    ARENA_LIMIT.EQUIP_ONLY_ONE_WEAPON,
    ARENA_LIMIT.EQUIP_TWO_WEAPONS
  };

  public static bool JudgeLimit(ArenaTable.ArenaData arenaData, EquipSetInfo equipSet)
  {
    return QuestUtility.JudgeLimit(arenaData, QuestUtility.CreateEquipItemData(equipSet));
  }

  public static bool JudgeLimit(ArenaTable.ArenaData arenaData, List<CharaInfo.EquipItem> equipSet)
  {
    return QuestUtility.JudgeLimit(arenaData, QuestUtility.CreateEquipItemData(equipSet));
  }

  public static bool JudgeLimit(
    ArenaTable.ArenaData arenaData,
    EquipItemTable.EquipItemData[] equipSet)
  {
    List<EQUIPMENT_TYPE> allowTypes1 = new List<EQUIPMENT_TYPE>();
    List<SP_ATTACK_TYPE> allowTypes2 = new List<SP_ATTACK_TYPE>();
    int limitNum = 3;
    int index = 0;
    for (int length = arenaData.limits.Length; index < length; ++index)
    {
      ARENA_LIMIT limit = arenaData.limits[index];
      if (limit != ARENA_LIMIT.NONE)
      {
        if (((IEnumerable<ARENA_LIMIT>) QuestUtility.WeaponTypeLimit).Contains<ARENA_LIMIT>(limit))
          allowTypes1.Add(QuestUtility.GetEquipmentType(limit));
        else if (((IEnumerable<ARENA_LIMIT>) QuestUtility.SpAttackTypeLimit).Contains<ARENA_LIMIT>(limit))
          allowTypes2.Add(QuestUtility.GetSPAttackType(limit));
        else if (((IEnumerable<ARENA_LIMIT>) QuestUtility.WeaponNumTypeLimit).Contains<ARENA_LIMIT>(limit))
          limitNum = QuestUtility.GetEquipmentNum(limit);
      }
    }
    bool flag = true;
    if (allowTypes1.Count >= 1)
      flag &= QuestUtility.JudgeLimitWeapon(allowTypes1, equipSet);
    if (allowTypes2.Count >= 1)
      flag &= QuestUtility.JudgeLimitWeaponAttackType(allowTypes2, equipSet);
    if (limitNum <= 2)
      flag &= QuestUtility.JudgeLimitWeaponNum(limitNum, equipSet);
    return flag;
  }

  public static string CreateTimeStringByMilliSec(int milliSecond)
  {
    int num1 = (int) ((double) milliSecond * (1.0 / 1000.0));
    int num2 = num1 / 60;
    int num3 = num1 % 60;
    milliSecond %= 1000;
    return $"{num2:d2}:{num3:d2}.{milliSecond:d3}";
  }

  public static string CreateTimeStringByMilliSecSeriesArena(int milliSecond)
  {
    string stringByMilliSec = QuestUtility.CreateTimeStringByMilliSec(milliSecond);
    return stringByMilliSec.Remove(stringByMilliSec.Length - 1);
  }

  public static bool IsDefaultArenaTime(int milliSecond)
  {
    return milliSecond == QuestUtility.GetDefaultArenaTime();
  }

  public static int GetDefaultArenaTime() => 3599999;

  public static int ToSecByMilliSec(int milliSecond)
  {
    return (int) ((double) milliSecond * (1.0 / 1000.0));
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
      case ARENA_LIMIT.ACTION_TYPE_ORACLE:
        return SP_ATTACK_TYPE.ORACLE;
      default:
        return SP_ATTACK_TYPE.NONE;
    }
  }

  private static int GetEquipmentNum(ARENA_LIMIT limitType)
  {
    if (limitType == ARENA_LIMIT.EQUIP_ONLY_ONE_WEAPON)
      return 1;
    return limitType == ARENA_LIMIT.EQUIP_TWO_WEAPONS ? 2 : 3;
  }

  private static bool JudgeLimitWeapon(
    List<EQUIPMENT_TYPE> allowTypes,
    EquipItemTable.EquipItemData[] equips)
  {
    int index1 = 0;
    for (int count = allowTypes.Count; index1 < count; ++index1)
    {
      if (!Singleton<EquipItemTable>.I.IsWeapon(allowTypes[index1]))
        Debug.LogError((object) "武器のチェックに武器以外のタイプが渡されています(無視されます)");
    }
    int index2 = 0;
    for (int length = equips.Length; index2 < length; ++index2)
    {
      if (equips[index2] != null)
      {
        EQUIPMENT_TYPE type = equips[index2].type;
        if (Singleton<EquipItemTable>.I.IsWeapon(type) && !allowTypes.Contains(type))
          return false;
      }
    }
    return true;
  }

  private static bool JudgeLimitWeaponAttackType(
    List<SP_ATTACK_TYPE> allowTypes,
    EquipItemTable.EquipItemData[] equips)
  {
    int index = 0;
    for (int length = equips.Length; index < length; ++index)
    {
      if (equips[index] != null)
      {
        EquipItemTable.EquipItemData equip = equips[index];
        if (Singleton<EquipItemTable>.I.IsWeapon(equip.type) && !allowTypes.Contains(equip.spAttackType))
          return false;
      }
    }
    return true;
  }

  private static bool JudgeLimitWeaponNum(int limitNum, EquipItemTable.EquipItemData[] equips)
  {
    int num = 0;
    for (int index = 0; index < 3; ++index)
    {
      if (equips[index] != null)
        ++num;
    }
    return num <= limitNum;
  }

  public static string GetLimitText(ArenaTable.ArenaData arenaData)
  {
    string self = "";
    int index = 0;
    for (int length = arenaData.limits.Length; index < length; ++index)
    {
      uint limit = (uint) arenaData.limits[index];
      if (limit > 0U)
        self = $"{self}{StringTable.Get(STRING_CATEGORY.ARENA_LIMIT, limit)}\n";
    }
    if (self.IsNullOrWhiteSpace())
      self = StringTable.Get(STRING_CATEGORY.ARENA_LIMIT, 0U);
    return self.TrimEnd();
  }

  public static string GetConditionText(ArenaTable.ArenaData arenaData)
  {
    string self = "";
    int index = 0;
    for (int length = arenaData.conditions.Length; index < length; ++index)
    {
      uint condition = (uint) arenaData.conditions[index];
      if (condition > 0U)
        self = $"{self}{StringTable.Get(STRING_CATEGORY.ARENA_CONDITION, condition)}\n";
    }
    if (self.IsNullOrWhiteSpace())
      self = StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 18U);
    return self.TrimEnd();
  }

  public static string GetEndDateString(Network.EventData eventData)
  {
    return "~" + eventData.endDate.date.Substring(5, 11).Replace("-", "/");
  }

  public static string GetArenaTitle(ARENA_GROUP group, string subTitle)
  {
    return $"{StringTable.Format(STRING_CATEGORY.ARENA, 0U, (object) group)} {subTitle}";
  }

  public static string GetArenaTitle(ARENA_GROUP group, ARENA_RANK rank)
  {
    string str = StringTable.Format(STRING_CATEGORY.ARENA, 0U, (object) group);
    StringTable.Format(STRING_CATEGORY.ARENA, 1U, (object) rank);
    return $"{str} {(object) rank}";
  }

  private static EquipItemTable.EquipItemData[] CreateEquipItemData(List<CharaInfo.EquipItem> equips)
  {
    EquipItemTable.EquipItemData[] equipItemData1 = new EquipItemTable.EquipItemData[equips.Count];
    int index = 0;
    for (int length = equipItemData1.Length; index < length; ++index)
    {
      EquipItemTable.EquipItemData equipItemData2 = Singleton<EquipItemTable>.I.GetEquipItemData((uint) equips[index].eId);
      equipItemData1[index] = equipItemData2;
    }
    return equipItemData1;
  }

  private static EquipItemTable.EquipItemData[] CreateEquipItemData(EquipSetInfo equips)
  {
    EquipItemTable.EquipItemData[] equipItemData = new EquipItemTable.EquipItemData[equips.item.Length];
    int index = 0;
    for (int length = equipItemData.Length; index < length; ++index)
    {
      if (equips.item[index] != null)
      {
        EquipItemTable.EquipItemData tableData = equips.item[index].tableData;
        equipItemData[index] = tableData;
      }
    }
    return equipItemData;
  }
}
