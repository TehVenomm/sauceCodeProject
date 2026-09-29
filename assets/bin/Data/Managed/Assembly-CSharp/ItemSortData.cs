// Decompiled with JetBrains decompiler
// Type: ItemSortData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class ItemSortData : SortCompareData
{
  public ItemInfo itemData;

  public override object GetItemData() => (object) this.itemData;

  public override void SetItem(object item) => this.itemData = (ItemInfo) item;

  public override void SetupSortingData(
    SortBase.SORT_REQUIREMENT requirement,
    EquipItemStatus status = null)
  {
    switch (requirement)
    {
      case SortBase.SORT_REQUIREMENT.NUM:
        this.sortingData = (long) this.itemData.num;
        break;
      case SortBase.SORT_REQUIREMENT.RARITY:
        this.sortingData = (long) this.itemData.tableData.rarity;
        break;
      case SortBase.SORT_REQUIREMENT.LV:
        this.sortingData = 1L;
        break;
      case SortBase.SORT_REQUIREMENT.ATK:
        this.sortingData = 0L;
        break;
      case SortBase.SORT_REQUIREMENT.DEF:
        this.sortingData = 0L;
        break;
      case SortBase.SORT_REQUIREMENT.SALE:
        this.sortingData = (long) this.itemData.tableData.price;
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

  public override int GetItemType() => (int) this.itemData.tableData.type;

  public override int GetNum() => this.itemData.num;

  public override ulong GetUniqID() => this.itemData.uniqueID;

  public override uint GetTableID() => this.itemData.tableID;

  public override string GetName() => this.itemData.tableData.name;

  public override int GetIconID() => this.itemData.tableData.iconID;

  public override ITEM_ICON_TYPE GetIconType()
  {
    return ItemIcon.GetItemIconType(this.itemData.tableData.type);
  }

  public override ELEMENT_TYPE GetIconElement() => (ELEMENT_TYPE) this.itemData.tableData.element;

  public override string GetDetail() => this.itemData.tableData.text;

  public override RARITY_TYPE GetRarity() => this.itemData.tableData.rarity;

  public override int GetSalePrice() => this.itemData.tableData.price;

  public override bool CanSale() => !this.itemData.tableData.cantSale;

  public override REWARD_TYPE GetMaterialType() => REWARD_TYPE.ITEM;

  public override bool IsLithograph()
  {
    return this.itemData != null && this.itemData.tableData != null && this.itemData.tableData.type == ITEM_TYPE.LITHOGRAPH;
  }

  public override uint GetMainorSortWeight()
  {
    uint num = 0;
    uint mainorSortWeight;
    if (this.itemData.tableData.type == ITEM_TYPE.ABILITY_ITEM)
    {
      uint minorSortValue = this.ElementTypeToMinorSortValue(this.GetIconElement());
      mainorSortWeight = (uint) ((int) (num + (minorSortValue << 6)) + this.GetRarity());
    }
    else
      mainorSortWeight = this.GetTableID();
    return mainorSortWeight;
  }
}
