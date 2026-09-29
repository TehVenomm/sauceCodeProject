// Decompiled with JetBrains decompiler
// Type: SortCompareData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class SortCompareData
{
  public long sortingData;
  public const long SHIFT_BASE = 1;
  public const int PRIORITY_SHIFT_VALUE = 61;
  public const int SUPER_PRIORITY_SHIFT_VALUE = 62;
  public const int REQUIREMENT_SHIFT_VALUE = 31 /*0x1F*/;
  protected REWARD_CATEGORY m_category;

  public virtual object GetItemData() => (object) null;

  public virtual void SetItem(object item)
  {
  }

  public long GetValue()
  {
    return (this.sortingData - (long) this.GetMainorSortWeight()) % 2305843009213693952L /*0x2000000000000000*/ >> 31 /*0x1F*/;
  }

  public bool IsPriority(bool isAsc)
  {
    return this.sortingData >= 2305843009213693952L /*0x2000000000000000*/ != isAsc;
  }

  protected int EquipmentTypeToSortBaseType(EQUIPMENT_TYPE type)
  {
    SortBase.TYPE sortBaseType;
    switch (type)
    {
      case EQUIPMENT_TYPE.TWO_HAND_SWORD:
        sortBaseType = SortBase.TYPE.TWO_HAND_SWORD;
        break;
      case EQUIPMENT_TYPE.SPEAR:
        sortBaseType = SortBase.TYPE.SPEAR;
        break;
      case EQUIPMENT_TYPE.PAIR_SWORDS:
        sortBaseType = SortBase.TYPE.PAIR_SWORDS;
        break;
      case EQUIPMENT_TYPE.ARROW:
        sortBaseType = SortBase.TYPE.ARROW;
        break;
      case EQUIPMENT_TYPE.ARMOR:
      case EQUIPMENT_TYPE.VISUAL_ARMOR:
        sortBaseType = SortBase.TYPE.ARMOR;
        break;
      case EQUIPMENT_TYPE.HELM:
      case EQUIPMENT_TYPE.VISUAL_HELM:
        sortBaseType = SortBase.TYPE.HELM;
        break;
      case EQUIPMENT_TYPE.ARM:
      case EQUIPMENT_TYPE.VISUAL_ARM:
        sortBaseType = SortBase.TYPE.ARM;
        break;
      case EQUIPMENT_TYPE.LEG:
      case EQUIPMENT_TYPE.VISUAL_LEG:
        sortBaseType = SortBase.TYPE.LEG;
        break;
      default:
        sortBaseType = SortBase.TYPE.ONE_HAND_SWORD;
        break;
    }
    return (int) sortBaseType;
  }

  protected uint EquipmentTypeToMinorSortValue(EQUIPMENT_TYPE type)
  {
    switch (type)
    {
      case EQUIPMENT_TYPE.ONE_HAND_SWORD:
        return 9;
      case EQUIPMENT_TYPE.TWO_HAND_SWORD:
        return 8;
      case EQUIPMENT_TYPE.SPEAR:
        return 7;
      case EQUIPMENT_TYPE.PAIR_SWORDS:
        return 6;
      case EQUIPMENT_TYPE.ARROW:
        return 5;
      case EQUIPMENT_TYPE.ARMOR:
      case EQUIPMENT_TYPE.VISUAL_ARMOR:
        return 3;
      case EQUIPMENT_TYPE.HELM:
      case EQUIPMENT_TYPE.VISUAL_HELM:
        return 4;
      case EQUIPMENT_TYPE.ARM:
      case EQUIPMENT_TYPE.VISUAL_ARM:
        return 2;
      case EQUIPMENT_TYPE.LEG:
      case EQUIPMENT_TYPE.VISUAL_LEG:
        return 1;
      default:
        return 0;
    }
  }

  protected uint ElementTypeToMinorSortValue(ELEMENT_TYPE type) => (uint) (6 - type);

  protected uint GetTypeToMinorSortValue(GET_TYPE type)
  {
    switch (type)
    {
      case GET_TYPE.PAY:
        return 1;
      default:
        return 0;
    }
  }

  public virtual void SetupSortingData(
    SortBase.SORT_REQUIREMENT requirement,
    EquipItemStatus status = null)
  {
  }

  public void Filtering(SortSettings settings)
  {
    bool flag = false;
    if (!flag && (1 << (int) (this.GetRarity() & (RARITY_TYPE) 31 /*0x1F*/) & settings.rarity) == 0)
      flag = true;
    if (!flag && settings.dialogType != SortBase.DIALOG_TYPE.USE_ITEM)
    {
      if (settings.dialogType == SortBase.DIALOG_TYPE.MATERIAL)
      {
        int num;
        switch (this.GetItemType())
        {
          case 6:
            num = 16 /*0x10*/;
            break;
          case 7:
            num = 4;
            break;
          case 14:
            num = 1;
            break;
          case 15:
            num = 8;
            break;
          default:
            num = 2;
            break;
        }
        if ((settings.type & num) == 0)
          flag = true;
        int iconElement = (int) this.GetIconElement();
        if ((1 << (int) (this.GetIconElement() & (ELEMENT_TYPE) 31 /*0x1F*/) & settings.element) == 0)
          flag = true;
      }
      else if (settings.dialogType == SortBase.DIALOG_TYPE.SMITH_CREATE_WEAPON || settings.dialogType == SortBase.DIALOG_TYPE.SMITH_CREATE_ARMOR)
      {
        if ((settings.type & this.GetItemType()) == 0)
          flag = true;
        if (this.getEquipFilterPayAndCreatable(settings.equipFilter))
          flag = true;
        int iconElement = (int) this.GetIconElement();
        if ((1 << (int) (this.GetIconElement() & (ELEMENT_TYPE) 31 /*0x1F*/) & settings.element) == 0)
          flag = true;
      }
      else if (settings.dialogType == SortBase.DIALOG_TYPE.TYPE_FILTERABLE_WEAPON || settings.dialogType == SortBase.DIALOG_TYPE.TYPE_FILTERABLE_ARMOR)
      {
        if ((settings.type & this.GetItemType()) == 0)
          flag = true;
        if ((settings.equipFilter & this.getEquipFilterPay()) == 0)
          flag = true;
        int iconElement = (int) this.GetIconElement();
        if ((1 << (int) (this.GetIconElement() & (ELEMENT_TYPE) 31 /*0x1F*/) & settings.element) == 0)
          flag = true;
      }
      else if (settings.dialogType == SortBase.DIALOG_TYPE.ABILITY_ITEM)
      {
        if ((settings.type & this.GetItemType()) == 0)
          flag = true;
        if ((1 << (int) (this.GetIconElement() & (ELEMENT_TYPE) 31 /*0x1F*/) & settings.element) == 0)
          flag = true;
      }
      else if (settings.dialogType == SortBase.DIALOG_TYPE.SKILL || settings.dialogType == SortBase.DIALOG_TYPE.STORAGE_SKILL)
      {
        if ((settings.type & this.GetItemType()) == 0)
          flag = true;
        if (this.GetIconElementSub() == ELEMENT_TYPE.MAX)
        {
          if ((1 << (int) (this.GetIconElement() & (ELEMENT_TYPE) 31 /*0x1F*/) & settings.element) == 0)
            flag = true;
        }
        else
        {
          int num1 = 1 << (int) (this.GetIconElement() & (ELEMENT_TYPE) 31 /*0x1F*/);
          int num2 = 1 << (int) (this.GetIconElementSub() & (ELEMENT_TYPE) 31 /*0x1F*/);
          int element = settings.element;
          if ((num1 & element) == 0 && (num2 & settings.element) == 0)
            flag = true;
        }
      }
      else
      {
        if ((settings.type & this.GetItemType()) == 0)
          flag = true;
        if ((1 << (int) (this.GetIconElement() & (ELEMENT_TYPE) 31 /*0x1F*/) & settings.element) == 0)
          flag = true;
      }
    }
    if (settings.dialogType == SortBase.DIALOG_TYPE.ACCESSORY)
      flag = false;
    this.sortingData <<= 31 /*0x1F*/;
    if (settings.orderTypeAsc == flag)
      this.sortingData |= 2305843009213693952L /*0x2000000000000000*/;
    this.sortingData += (long) this.GetMainorSortWeight();
    if (!this.IsAbsFirst())
      return;
    if (settings.orderTypeAsc)
      this.sortingData = 0L;
    else
      this.sortingData |= 4611686018427387904L /*0x4000000000000000*/;
  }

  public static SORT_DATA[] CreateSortDataAry<ITEM_DATA, SORT_DATA>(
    ITEM_DATA[] data,
    SortSettings sort_settings,
    SortSettings.SortEquipSetInfo sort_equip_set_info = null)
    where SORT_DATA : SortCompareData, new()
  {
    EquipItemStatus _base = (EquipItemStatus) null;
    if (sort_equip_set_info != null)
      _base = MonoBehaviourSingleton<StatusManager>.I.GetEquipSetAllSkillParam(sort_equip_set_info.equipSetNo, (sort_equip_set_info.isLocal ? 1 : 0) != 0, sort_equip_set_info.exclusionSlotIndex);
    SORT_DATA[] equipItemSortAry = new SORT_DATA[data.Length];
    for (int i = 0; i < data.Length; i++)
    {
      equipItemSortAry[i] = new SORT_DATA();
      equipItemSortAry[i].SetItem((object) data[i]);
      EquipItemStatus status = new EquipItemStatus(_base);
      if (_base != null)
      {
        bool exclusion = false;
        sort_equip_set_info?.exclusionUniqID.ForEach((Action<ulong>) (uniq_id =>
        {
          if (exclusion || (long) equipItemSortAry[i].GetUniqID() != (long) uniq_id)
            return;
          exclusion = true;
        }));
        if (!exclusion)
          status.Add(equipItemSortAry[i].GetItemStatus());
      }
      equipItemSortAry[i].SetupSortingData(sort_settings.requirement, status);
      equipItemSortAry[i].Filtering(sort_settings);
    }
    return equipItemSortAry;
  }

  public static void InitSortDataAry<SORT_DATA>(
    SORT_DATA[] sort_data,
    SortSettings sort_settings,
    SortSettings.SortEquipSetInfo sort_equip_set_info = null)
    where SORT_DATA : SortCompareData, new()
  {
    EquipItemStatus _base = (EquipItemStatus) null;
    if (sort_equip_set_info != null)
      _base = MonoBehaviourSingleton<StatusManager>.I.GetEquipSetAllSkillParam(sort_equip_set_info.equipSetNo, (sort_equip_set_info.isLocal ? 1 : 0) != 0, sort_equip_set_info.exclusionSlotIndex);
    for (int index = 0; index < sort_data.Length; ++index)
    {
      EquipItemStatus status = new EquipItemStatus(_base);
      SORT_DATA sortData = sort_data[index];
      if (_base != null)
      {
        bool exclusion = false;
        sort_equip_set_info?.exclusionUniqID.ForEach((Action<ulong>) (uniq_id =>
        {
          if (exclusion || (long) sortData.GetUniqID() != (long) uniq_id)
            return;
          exclusion = true;
        }));
        if (!exclusion)
          status.Add(sort_data[index].GetItemStatus());
      }
      ((SORT_DATA) sortData).SetupSortingData(sort_settings.requirement, status);
      ((SORT_DATA) sortData).Filtering(sort_settings);
    }
  }

  public virtual bool IsAbsFirst() => false;

  public virtual bool IsFavorite() => false;

  public virtual int GetItemType() => 0;

  public virtual RARITY_TYPE GetRarity() => RARITY_TYPE.D;

  public virtual ulong GetUniqID() => 0;

  public virtual uint GetTableID() => 0;

  public virtual string GetName() => string.Empty;

  public virtual int GetIconID() => 0;

  public virtual ITEM_ICON_TYPE GetIconType() => ITEM_ICON_TYPE.NONE;

  public virtual ELEMENT_TYPE GetIconElement() => ELEMENT_TYPE.MAX;

  public virtual ELEMENT_TYPE GetIconElementSub() => ELEMENT_TYPE.MAX;

  public virtual EQUIPMENT_TYPE? GetIconMagiEnableType() => new EQUIPMENT_TYPE?();

  public virtual string GetDetail() => string.Empty;

  public virtual int GetNum() => 0;

  public virtual int GetSalePrice() => 0;

  public virtual bool CanSale() => false;

  public virtual bool IsEquipping() => false;

  public virtual int GetLevel() => 1;

  public virtual ItemStatus GetItemStatus() => (ItemStatus) null;

  public virtual REWARD_TYPE GetMaterialType() => REWARD_TYPE.NONE;

  public virtual bool IsEquipSomewhere() => false;

  public virtual bool IsLithograph() => false;

  public virtual int getEquipFilterPay() => 0;

  public virtual int getEquipFilterCreatable() => 0;

  public virtual int getEquipFilterObtained() => 0;

  public virtual bool getEquipFilterPayAndCreatable(int filter) => false;

  public virtual bool IsExceeded() => false;

  public virtual GET_TYPE GetGetType() => GET_TYPE.PAY;

  public virtual uint GetMainorSortWeight() => 0;

  public virtual bool IsUniqueEquipping() => false;

  public virtual bool IsHomeEquipping() => false;

  public REWARD_CATEGORY GetCategory() => this.m_category;

  public void SetCategory(REWARD_CATEGORY category) => this.m_category = category;

  public int GetSortValueQuestResult()
  {
    return (int) (0 + (this.IsLithograph() ? 100000 : 0) + (this.GetIconType() == ITEM_ICON_TYPE.ABILITY_ITEM ? 10000 : 0) + (int) (4 - this.m_category) * 100 + this.GetRarity());
  }
}
