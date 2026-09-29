// Decompiled with JetBrains decompiler
// Type: AbilityItemSortData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class AbilityItemSortData : SortCompareData
{
  public AbilityItemInfo itemData;

  public override object GetItemData() => (object) this.itemData;

  public override void SetItem(object item) => this.itemData = (AbilityItemInfo) item;

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
        this.sortingData = (long) this.itemData.GetItemTableData().rarity;
        break;
      case SortBase.SORT_REQUIREMENT.LV:
        this.sortingData = 0L;
        break;
      case SortBase.SORT_REQUIREMENT.ATK:
        this.sortingData = 0L;
        break;
      case SortBase.SORT_REQUIREMENT.DEF:
        this.sortingData = 0L;
        break;
      case SortBase.SORT_REQUIREMENT.SALE:
        this.sortingData = (long) this.itemData.GetItemTableData().price;
        break;
      case SortBase.SORT_REQUIREMENT.SOCKET:
        this.sortingData = 0L;
        break;
      case SortBase.SORT_REQUIREMENT.PRICE:
        this.sortingData = 0L;
        break;
      case SortBase.SORT_REQUIREMENT.ELEMENT:
        this.sortingData = 6L - (long) this.GetIconElement();
        break;
      default:
        this.sortingData = (long) this.itemData.uniqueID;
        break;
    }
  }

  public override bool IsFavorite() => false;

  public override int GetItemType() => (int) this.itemData.GetItemTableData().type;

  public override int GetNum() => 1;

  public override ulong GetUniqID() => this.itemData.uniqueID;

  public override uint GetTableID() => this.itemData.tableID;

  public override string GetName() => this.itemData.GetName();

  public override int GetIconID() => this.itemData.GetItemTableData().iconID;

  public override ITEM_ICON_TYPE GetIconType() => ITEM_ICON_TYPE.ABILITY_ITEM;

  public override ELEMENT_TYPE GetIconElement()
  {
    return (ELEMENT_TYPE) this.itemData.GetItemTableData().element;
  }

  public override string GetDetail() => this.itemData.GetDescription();

  public override RARITY_TYPE GetRarity() => this.itemData.GetItemTableData().rarity;

  public override int GetSalePrice() => this.itemData.GetItemTableData().price;

  public override bool CanSale() => !this.itemData.GetItemTableData().cantSale;

  public override REWARD_TYPE GetMaterialType() => REWARD_TYPE.ABILITY_ITEM;

  public override bool IsLithograph()
  {
    return this.itemData != null && this.itemData.GetItemTableData() != null && this.itemData.GetItemTableData().type == ITEM_TYPE.LITHOGRAPH;
  }

  public override uint GetMainorSortWeight()
  {
    return (uint) (0 + ((int) this.ElementTypeToMinorSortValue(this.GetIconElement()) << 27) + ((int) this.GetRarity() << 21));
  }
}
