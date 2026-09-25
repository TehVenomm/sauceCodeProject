// Decompiled with JetBrains decompiler
// Type: AccessorySortData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class AccessorySortData : SortCompareData
{
  public AccessoryInfo itemData;

  public override object GetItemData() => (object) this.itemData;

  public override void SetItem(object item) => this.itemData = (AccessoryInfo) item;

  public override void SetupSortingData(
    SortBase.SORT_REQUIREMENT requirement,
    EquipItemStatus status = null)
  {
    if (requirement != SortBase.SORT_REQUIREMENT.GET)
    {
      if (requirement != SortBase.SORT_REQUIREMENT.RARITY)
      {
        if (requirement == SortBase.SORT_REQUIREMENT.PRICE)
        {
          this.sortingData = (long) this.itemData.tableData.price;
          return;
        }
      }
      else
      {
        this.sortingData = (long) this.itemData.tableData.rarity;
        return;
      }
    }
    this.sortingData = (long) this.itemData.uniqueID;
  }

  public override bool IsFavorite() => this.itemData.isFavorite;

  public override int GetNum() => 1;

  public override ulong GetUniqID() => this.itemData.uniqueID;

  public override uint GetTableID() => this.itemData.tableID;

  public override string GetName() => this.itemData.tableData.name;

  public override int GetIconID() => (int) this.itemData.tableID;

  public override ITEM_ICON_TYPE GetIconType() => ITEM_ICON_TYPE.ACCESSORY;

  public override string GetDetail() => this.itemData.tableData.descript;

  public override RARITY_TYPE GetRarity() => this.itemData.tableData.rarity;

  public override int GetSalePrice() => this.itemData.tableData.price;

  public override bool CanSale() => !this.itemData.tableData.cantSell;

  public override REWARD_TYPE GetMaterialType() => REWARD_TYPE.ACCESSORY;

  public override uint GetMainorSortWeight() => (uint) this.itemData.tableData.orderValue;
}
