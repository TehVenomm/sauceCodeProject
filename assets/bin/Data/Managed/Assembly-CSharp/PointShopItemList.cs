// Decompiled with JetBrains decompiler
// Type: PointShopItemList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using UnityEngine;

#nullable disable
public class PointShopItemList : MonoBehaviour
{
  public UILabel tradeNum;
  public UILabel remainingTime;
  public UILabel pointNum;
  public UILabel itemName;
  public UITexture pointIcon;
  public Transform itemIconRoot;

  public void SetUp(PointShopItem item, uint pointId, bool isChangable)
  {
    this.SetUpText(item, isChangable);
    this.SetUpPointIcon(this.pointIcon, pointId);
    this.SetUpItemIcon(item);
  }

  protected void SetUpText(PointShopItem item, bool isChangable)
  {
    this.itemName.text = item.name;
    if (item.hasLimit)
    {
      switch (item.limitPeriodType)
      {
        case POINT_SHOP_ITEM_LIMIT_TYPE.DAILY:
          this.tradeNum.text = string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 3U), (object) (item.limit - item.buyCount));
          break;
        case POINT_SHOP_ITEM_LIMIT_TYPE.WEEKLY:
          this.tradeNum.text = string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 4U), (object) (item.limit - item.buyCount));
          break;
        case POINT_SHOP_ITEM_LIMIT_TYPE.MONTHLY:
          this.tradeNum.text = string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 5U), (object) (item.limit - item.buyCount));
          break;
        default:
          this.tradeNum.text = string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 1U), (object) (item.limit - item.buyCount));
          break;
      }
    }
    else
      this.tradeNum.text = StringTable.Get(STRING_CATEGORY.POINT_SHOP, 0U);
    this.pointNum.text = string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 2U), (object) item.needPoint);
    this.pointNum.color = isChangable ? Color.white : Color.red;
    this.remainingTime.text = item.expire;
  }

  protected void SetUpPointIcon(UITexture targetUITexture, uint pointId)
  {
    ResourceLoad.LoadPointIconImageTexture(targetUITexture, pointId);
  }

  protected void SetUpItemIcon(PointShopItem item)
  {
    ItemIcon.CreateRewardItemIcon((REWARD_TYPE) item.type, (uint) item.itemId, this.itemIconRoot).SetEnableCollider(false);
  }
}
