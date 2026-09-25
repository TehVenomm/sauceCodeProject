// Decompiled with JetBrains decompiler
// Type: ItemDetailSellItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class ItemDetailSellItem : ItemDetailSellBase
{
  public override void UpdateUI()
  {
    if (!(this.data is ItemSortData) && !(this.data is AbilityItemSortData))
      return;
    base.UpdateUI();
  }

  protected void OnQuery_SALE()
  {
    int sliderNum = this.GetSliderNum();
    int num = this.data.GetSalePrice() * sliderNum;
    object[] event_data = new object[5]
    {
      (object) this.data,
      (object) sliderNum,
      (object) num,
      (object) ItemDetailEquip.CURRENT_SECTION.ITEM_STORAGE,
      null
    };
    if (this.data is ItemSortData data && data.itemData.tableData.type == ITEM_TYPE.LITHOGRAPH)
      GameSection.ChangeEvent("SELL_LITHOGRAPH", (object) event_data);
    else
      GameSection.SetEventData((object) event_data);
  }

  private void OnQuery_SALE_TP()
  {
    if (!TradingPostManager.IsTradingEnable())
      TradingPostManager.ShowUnavailableDialog();
    else
      MonoBehaviourSingleton<TradingPostManager>.I.SetTradingPostSellItemData(this.data.GetTableID(), this.data.GetUniqID(), this.data.GetNum());
  }
}
