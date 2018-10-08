using Network;

public class ItemDetailPointShopItem : PointShopItemList
{
	public UITexture havePointIcon;

	public UILabel havePointNum;

	public void SetUpItemDetailItem(PointShopItem item, PointShop shop, uint pointId, bool isChangable)
	{
		SetUpText(item, isChangable);
		SetUpPointIcon(pointIcon, pointId);
		SetUpPointIcon(havePointIcon, pointId);
		havePointNum.text = string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2u), shop.userPoint);
	}
}
