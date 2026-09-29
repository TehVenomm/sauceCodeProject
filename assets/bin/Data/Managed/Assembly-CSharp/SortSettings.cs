// Decompiled with JetBrains decompiler
// Type: SortSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

#nullable disable
public class SortSettings
{
  private static SortSettings[] memSettings;
  public SortSettings.SETTINGS_TYPE settingsType;
  public SortBase.DIALOG_TYPE dialogType;
  public SortBase.SORT_REQUIREMENT requirement = SortBase.SORT_REQUIREMENT.ELEMENT;
  public int type = 511 /*0x01FF*/;
  public int rarity = (int) sbyte.MaxValue;
  public bool orderTypeAsc;
  public int equipFilter = 63 /*0x3F*/;
  public int element = (int) sbyte.MaxValue;
  private SortSettings.SortEquipSetInfo equipSetInfo;
  public SortBase.TYPE TYPE_ALL;
  public Comparison<SortCompareData> indivComparison;

  public static SortSettings CreateMemSortSettings(
    SortBase.DIALOG_TYPE dialog_type,
    SortSettings.SETTINGS_TYPE memory_type)
  {
    if (memory_type >= SortSettings.SETTINGS_TYPE.MAX)
      return (SortSettings) null;
    if (SortSettings.memSettings == null)
      SortSettings.memSettings = new SortSettings[14];
    int index = (int) memory_type;
    SortSettings memSetting = SortSettings.memSettings[index];
    bool flag = memSetting != null;
    if (!flag)
    {
      SortSettings.memSettings[index] = new SortSettings();
      if (SortSettings.GetMemorySortData(memory_type, ref SortSettings.memSettings[index]))
        flag = true;
      memSetting = SortSettings.memSettings[index];
    }
    memSetting.dialogType = dialog_type;
    switch (dialog_type)
    {
      case SortBase.DIALOG_TYPE.WEAPON:
      case SortBase.DIALOG_TYPE.ARMOR:
      case SortBase.DIALOG_TYPE.STORAGE_EQUIP:
      case SortBase.DIALOG_TYPE.TYPE_FILTERABLE_WEAPON:
      case SortBase.DIALOG_TYPE.TYPE_FILTERABLE_ARMOR:
        memSetting.TYPE_ALL = SortBase.TYPE.EQUIP_ALL;
        SortBase.SORT_REQUIREMENT sortRequirement1 = dialog_type == SortBase.DIALOG_TYPE.ARMOR || dialog_type == SortBase.DIALOG_TYPE.TYPE_FILTERABLE_ARMOR ? SortBase.SORT_REQUIREMENT.REQUIREMENT_ARMORS_BIT : SortBase.SORT_REQUIREMENT.REQUIREMENT_WEAPON_BIT;
        if (!flag)
        {
          memSetting.requirement = SortBase.SORT_REQUIREMENT.ELEMENT;
          break;
        }
        if ((memSetting.requirement & sortRequirement1) == (SortBase.SORT_REQUIREMENT) 0)
        {
          memSetting.requirement = memSetting.requirement != SortBase.SORT_REQUIREMENT.ATK ? (memSetting.requirement != SortBase.SORT_REQUIREMENT.DEF ? (memSetting.requirement != SortBase.SORT_REQUIREMENT.ELEM_ATK ? (memSetting.requirement != SortBase.SORT_REQUIREMENT.ELEM_DEF ? SortBase.SORT_REQUIREMENT.ELEMENT : SortBase.SORT_REQUIREMENT.ELEM_ATK) : SortBase.SORT_REQUIREMENT.ELEM_DEF) : SortBase.SORT_REQUIREMENT.ATK) : SortBase.SORT_REQUIREMENT.DEF;
          break;
        }
        break;
      case SortBase.DIALOG_TYPE.SKILL:
      case SortBase.DIALOG_TYPE.STORAGE_SKILL:
        memSetting.TYPE_ALL = SortBase.TYPE.SKILL_ALL;
        if (!flag)
        {
          memSetting.requirement = SortBase.SORT_REQUIREMENT.SKILL_TYPE;
          break;
        }
        break;
      case SortBase.DIALOG_TYPE.USE_ITEM:
      case SortBase.DIALOG_TYPE.MATERIAL:
        memSetting.TYPE_ALL = SortBase.TYPE.WEAPON_ALL;
        if (!flag)
        {
          memSetting.requirement = SortBase.SORT_REQUIREMENT.NUM;
          break;
        }
        break;
      case SortBase.DIALOG_TYPE.SMITH_CREATE_WEAPON:
      case SortBase.DIALOG_TYPE.SMITH_CREATE_ARMOR:
      case SortBase.DIALOG_TYPE.SMITH_CREATE_PICKUP_WEAPON:
      case SortBase.DIALOG_TYPE.SMITH_CREATE_PICKUP_ARMOR:
        memSetting.TYPE_ALL = SortBase.TYPE.NONE;
        SortBase.SORT_REQUIREMENT sortRequirement2;
        switch (dialog_type - 7)
        {
          case SortBase.DIALOG_TYPE.ARMOR:
            sortRequirement2 = SortBase.SORT_REQUIREMENT.REQUIREMENT_CREATE_ARMORS_BIT;
            break;
          case SortBase.DIALOG_TYPE.SKILL:
            sortRequirement2 = SortBase.SORT_REQUIREMENT.REQUIREMENT_CREATE_PICKUP_WEAPON_BIT;
            break;
          case SortBase.DIALOG_TYPE.STORAGE_EQUIP:
            sortRequirement2 = SortBase.SORT_REQUIREMENT.REQUIREMENT_CREATE_PICKUP_ARMORS_BIT;
            break;
          default:
            sortRequirement2 = SortBase.SORT_REQUIREMENT.REQUIREMENT_CREATE_WEAPON_BIT;
            break;
        }
        if (!flag)
        {
          memSetting.requirement = SortBase.SORT_REQUIREMENT.ELEMENT;
          break;
        }
        if ((memSetting.requirement & sortRequirement2) == (SortBase.SORT_REQUIREMENT) 0)
        {
          memSetting.requirement = memSetting.requirement != SortBase.SORT_REQUIREMENT.ATK ? (memSetting.requirement != SortBase.SORT_REQUIREMENT.DEF ? SortBase.SORT_REQUIREMENT.ELEMENT : SortBase.SORT_REQUIREMENT.ATK) : SortBase.SORT_REQUIREMENT.DEF;
          break;
        }
        break;
      case SortBase.DIALOG_TYPE.QUEST:
        memSetting.TYPE_ALL = SortBase.TYPE.ENEMY_ALL;
        if (!flag)
        {
          memSetting.requirement = SortBase.SORT_REQUIREMENT.RARITY;
          break;
        }
        break;
      default:
        memSetting.requirement = SortBase.SORT_REQUIREMENT.RARITY;
        memSetting.TYPE_ALL = SortBase.TYPE.NONE;
        break;
    }
    if (!flag && memSetting.TYPE_ALL != SortBase.TYPE.NONE)
      memSetting.type = (int) memSetting.TYPE_ALL;
    return memSetting.Clone();
  }

  public static void DeleteMemSortSetting()
  {
    if (SortSettings.memSettings == null)
      return;
    int index = 0;
    for (int length = SortSettings.memSettings.Length; index < length; ++index)
      SortSettings.memSettings[index] = (SortSettings) null;
    SortSettings.memSettings = (SortSettings[]) null;
  }

  private static int GetSortBitSettingsType(SortSettings.SETTINGS_TYPE settings_type)
  {
    return (int) settings_type;
  }

  private static int GetSortBitDialogType(SortBase.DIALOG_TYPE dialog_type) => (int) dialog_type;

  private static int GetSortBitRequirement(SortBase.SORT_REQUIREMENT requirement)
  {
    return (int) requirement;
  }

  private static int GetSortBitType(int type) => type;

  private static int GetSortBitRarity(int rarity) => rarity;

  private static int GetSortBitOrderTypeAsc(bool order_type_asc) => !order_type_asc ? 0 : 1;

  private static int GetSortBitEquipFilter(int equipfilter) => equipfilter;

  public static string GetSortBit(SortSettings sort_settings)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("{0},", (object) SortSettings.GetSortBitSettingsType(sort_settings.settingsType));
    stringBuilder.AppendFormat("{0},", (object) SortSettings.GetSortBitDialogType(sort_settings.dialogType));
    stringBuilder.AppendFormat("{0},", (object) SortSettings.GetSortBitRequirement(sort_settings.requirement));
    stringBuilder.AppendFormat("{0},", (object) SortSettings.GetSortBitType(sort_settings.type));
    stringBuilder.AppendFormat("{0},", (object) SortSettings.GetSortBitRarity(sort_settings.rarity));
    stringBuilder.AppendFormat("{0},", (object) SortSettings.GetSortBitOrderTypeAsc(sort_settings.orderTypeAsc));
    stringBuilder.AppendFormat("{0},", (object) SortSettings.GetSortBitEquipFilter(sort_settings.equipFilter));
    stringBuilder.AppendFormat("{0}", (object) SortSettings.GetSortBitEquipFilter(sort_settings.element));
    return stringBuilder.ToString();
  }

  public static bool GetMemorySortData(
    SortSettings.SETTINGS_TYPE settings_type,
    ref SortSettings ret)
  {
    string sortBit = GameSaveData.instance.GetSortBit(settings_type);
    int num = !string.IsNullOrEmpty(sortBit) ? 1 : 0;
    if (num != 0)
    {
      ret.settingsType = SortSettings.GetSettingsTypeBySortBit(sortBit);
      ret.dialogType = SortSettings.GetDialogTypeBySortBit(sortBit);
      ret.requirement = SortSettings.GetRequirementBySortBit(sortBit);
      ret.type = (int) SortSettings.GetTypeBySortBit(sortBit);
      ret.rarity = (int) SortSettings.GetRarityBySortBit(sortBit);
      ret.orderTypeAsc = SortSettings.GetOrderTypeAscBySortBit(sortBit);
      ret.equipFilter = (int) SortSettings.GetEquipFilterBySortBit(sortBit);
      ret.element = (int) SortSettings.GetElementBySortBit(sortBit);
      return num != 0;
    }
    ret.settingsType = settings_type;
    return num != 0;
  }

  public static void DeleteMemorySortData(SortSettings.SETTINGS_TYPE settings_type)
  {
    GameSaveData.instance.DeleteSortBit(settings_type);
    GameSaveData.Save();
  }

  public static SortSettings.SETTINGS_TYPE GetSettingsTypeBySortBit(string bit)
  {
    return (SortSettings.SETTINGS_TYPE) SortSettings.GetBitRange(bit, SortSettings.DATA_INDEX.SETTINGS_TYPE);
  }

  private static SortBase.DIALOG_TYPE GetDialogTypeBySortBit(string bit)
  {
    return (SortBase.DIALOG_TYPE) SortSettings.GetBitRange(bit, SortSettings.DATA_INDEX.DIALOG_TYPE);
  }

  private static SortBase.SORT_REQUIREMENT GetRequirementBySortBit(string bit)
  {
    return (SortBase.SORT_REQUIREMENT) SortSettings.GetBitRange(bit, SortSettings.DATA_INDEX.REQUIREMENT);
  }

  private static SortBase.TYPE GetTypeBySortBit(string bit)
  {
    return (SortBase.TYPE) SortSettings.GetBitRange(bit, SortSettings.DATA_INDEX.TYPE);
  }

  private static SortBase.RARITY GetRarityBySortBit(string bit)
  {
    return (SortBase.RARITY) SortSettings.GetBitRange(bit, SortSettings.DATA_INDEX.RARITY);
  }

  private static bool GetOrderTypeAscBySortBit(string bit)
  {
    return SortSettings.GetBitRange(bit, SortSettings.DATA_INDEX.ORDER_TYPE_ASC) == 1;
  }

  private static SortBase.EQUIP_FILTER GetEquipFilterBySortBit(string bit)
  {
    return (SortBase.EQUIP_FILTER) SortSettings.GetBitRange(bit, SortSettings.DATA_INDEX.EQUIP_FILTER);
  }

  private static SortBase.ELEMENT GetElementBySortBit(string bit)
  {
    return (SortBase.ELEMENT) SortSettings.GetBitRange(bit, SortSettings.DATA_INDEX.ELEMENT);
  }

  private static int GetBitRange(string bit, SortSettings.DATA_INDEX data_index)
  {
    string[] strArray = bit.Split(',');
    int result = 0;
    if (data_index < (SortSettings.DATA_INDEX) strArray.Length)
      int.TryParse(strArray[(int) data_index], out result);
    else
      result = int.MaxValue;
    return result;
  }

  public void ResetType(bool isEnable = true) => this.type = isEnable ? (int) this.TYPE_ALL : 0;

  public SortSettings Clone()
  {
    return new SortSettings()
    {
      dialogType = this.dialogType,
      rarity = this.rarity,
      type = this.type,
      requirement = this.requirement,
      orderTypeAsc = this.orderTypeAsc,
      settingsType = this.settingsType,
      equipSetInfo = this.equipSetInfo,
      indivComparison = this.indivComparison,
      equipFilter = this.equipFilter,
      element = this.element,
      TYPE_ALL = this.TYPE_ALL
    };
  }

  public SORT_DATA[] CreateSortAry<T, SORT_DATA>(T[] target_ary) where SORT_DATA : SortCompareData, new()
  {
    if (target_ary == null)
      return (SORT_DATA[]) null;
    SORT_DATA[] sortDataAry = SortCompareData.CreateSortDataAry<T, SORT_DATA>(target_ary, this, this.equipSetInfo);
    this.Sort<SORT_DATA>(sortDataAry);
    return sortDataAry;
  }

  public bool Sort<SORT_DATA>(SORT_DATA[] sort_target_ary) where SORT_DATA : SortCompareData, new()
  {
    SortSettings.memSettings[(int) this.settingsType] = this;
    SortCompareData.InitSortDataAry<SORT_DATA>(sort_target_ary, this, this.equipSetInfo);
    this.DataSort<SORT_DATA>(sort_target_ary);
    return true;
  }

  private void DataSort<SORT_DATA>(SORT_DATA[] ary) where SORT_DATA : SortCompareData
  {
    if (ary.Length <= 1)
      return;
    Comparison<SortCompareData> comparison = this.indivComparison ?? new SortComparison(this.orderTypeAsc).comparison;
    Array.Sort<SortCompareData>((SortCompareData[]) ary, comparison);
  }

  public string GetSortLabel()
  {
    return StringTable.Get(STRING_CATEGORY.SORT, (uint) (int) Mathf.Log((float) this.requirement, 2f));
  }

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
    MAX,
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
    ELEMENT,
  }

  public class SortEquipSetInfo
  {
    public int equipSetNo;
    public bool isLocal;
    public int exclusionSlotIndex;
    public List<ulong> exclusionUniqID;

    public SortEquipSetInfo(
      int _set_no,
      bool _is_local,
      int _exclusion_slot,
      List<ulong> _exclusion_uniq_id)
    {
      this.equipSetNo = _set_no;
      this.isLocal = _is_local;
      this.exclusionSlotIndex = _exclusion_slot;
      this.exclusionUniqID = _exclusion_uniq_id;
    }
  }
}
