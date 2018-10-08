public class ItemDetailSellItem : ItemDetailSellBase
{
	public override void UpdateUI()
	{
		if (data is ItemSortData || data is AbilityItemSortData)
		{
			base.UpdateUI();
		}
	}

	protected void OnQuery_SALE()
	{
		int sliderNum = GetSliderNum();
		int num = data.GetSalePrice() * sliderNum;
		object[] array = new object[5]
		{
			data,
			sliderNum,
			num,
			ItemDetailEquip.CURRENT_SECTION.ITEM_STORAGE,
			null
		};
		ItemSortData itemSortData = data as ItemSortData;
		if (itemSortData != null && itemSortData.itemData.tableData.type == ITEM_TYPE.LITHOGRAPH)
		{
			GameSection.ChangeEvent("SELL_LITHOGRAPH", array);
		}
		else
		{
			GameSection.SetEventData(array);
		}
	}
}
