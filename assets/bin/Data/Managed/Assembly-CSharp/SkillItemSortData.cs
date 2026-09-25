// Decompiled with JetBrains decompiler
// Type: SkillItemSortData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class SkillItemSortData : SortCompareData
{
  public SkillItemInfo skillData;

  public override object GetItemData() => (object) this.skillData;

  public override void SetItem(object item) => this.skillData = (SkillItemInfo) item;

  public override void SetupSortingData(
    SortBase.SORT_REQUIREMENT requirement,
    EquipItemStatus status = null)
  {
    switch (requirement)
    {
      case SortBase.SORT_REQUIREMENT.NUM:
        this.sortingData = 1L;
        break;
      case SortBase.SORT_REQUIREMENT.RARITY:
        this.sortingData = (long) this.skillData.tableData.rarity;
        break;
      case SortBase.SORT_REQUIREMENT.LV:
        this.sortingData = (long) this.skillData.level;
        break;
      case SortBase.SORT_REQUIREMENT.ATK:
        this.sortingData = (long) this.skillData.atk;
        break;
      case SortBase.SORT_REQUIREMENT.DEF:
        this.sortingData = (long) this.skillData.def;
        break;
      case SortBase.SORT_REQUIREMENT.SALE:
        this.sortingData = (long) this.skillData.sellPrice;
        break;
      case SortBase.SORT_REQUIREMENT.PRICE:
        this.sortingData = 0L;
        break;
      case SortBase.SORT_REQUIREMENT.HP:
        this.sortingData = (long) this.skillData.hp;
        break;
      case SortBase.SORT_REQUIREMENT.ELEMENT:
        this.sortingData = 6L - (long) this.GetIconElement();
        break;
      case SortBase.SORT_REQUIREMENT.SKILL_TYPE:
        this.sortingData = 63L /*0x3F*/ - (long) this.skillData.tableData.type;
        break;
      default:
        this.sortingData = (long) this.skillData.uniqueID;
        break;
    }
  }

  public override bool IsFavorite() => this.skillData.isFavorite;

  public override int GetItemType()
  {
    return 1 << (int) (this.skillData.tableData.type - 1 & (SKILL_SLOT_TYPE) 31 /*0x1F*/);
  }

  public override ulong GetUniqID() => this.skillData.uniqueID;

  public override uint GetTableID() => this.skillData.tableID;

  public override string GetName() => this.skillData.tableData.name;

  public override int GetIconID() => this.skillData.tableData.iconID;

  public override ITEM_ICON_TYPE GetIconType()
  {
    return ItemIcon.GetItemIconType(this.skillData.tableData.type);
  }

  public override EQUIPMENT_TYPE? GetIconMagiEnableType()
  {
    return this.skillData.tableData.GetEnableEquipType();
  }

  public override string GetDetail() => this.skillData.GetExplanationText();

  public override int GetNum() => this.skillData.num;

  public override RARITY_TYPE GetRarity() => this.skillData.tableData.rarity;

  public override ELEMENT_TYPE GetIconElement() => this.skillData.tableData.skillAtkType;

  public override ELEMENT_TYPE GetIconElementSub()
  {
    return this.skillData.tableData.GetAttackElementByIndex(1);
  }

  public override int GetSalePrice() => this.skillData.sellPrice;

  public override bool CanSale() => !this.skillData.isFavorite;

  public override bool IsEquipping()
  {
    return this.skillData.isAttached || this.skillData.isUniqueAttached;
  }

  public override int GetLevel() => this.skillData.level;

  public override REWARD_TYPE GetMaterialType() => REWARD_TYPE.SKILL_ITEM;

  public override bool IsEquipSomewhere() => this.IsEquipping();

  public override bool IsExceeded() => this.skillData.IsExceeded();

  public override uint GetMainorSortWeight()
  {
    return (uint) (0 + ((int) (63 /*0x3F*/ - this.skillData.tableData.type) << 22) + ((int) this.ElementTypeToMinorSortValue(this.GetIconElement()) << 19) + ((int) this.GetRarity() << 13) + (this.skillData.level << 6));
  }
}
