// Decompiled with JetBrains decompiler
// Type: ItemDetailPointShopItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;

#nullable disable
public class ItemDetailPointShopItem : PointShopItemList
{
  public UITexture havePointIcon;
  public UILabel havePointNum;

  public void SetUpItemDetailItem(
    PointShopItem item,
    PointShop shop,
    uint pointId,
    bool isChangable)
  {
    this.SetUpText(item, isChangable);
    this.SetUpPointIcon(this.pointIcon, pointId);
    this.SetUpPointIcon(this.havePointIcon, pointId);
    this.havePointNum.text = string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2U), (object) shop.userPoint);
  }
}
