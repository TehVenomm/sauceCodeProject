// Decompiled with JetBrains decompiler
// Type: Network.PointShopItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Network;

[Serializable]
public class PointShopItem : Present
{
  public int needPoint;
  public int limit = -1;
  public int limitType;
  public int buyCount;
  public string pointShopItemId;

  public bool hasLimit => this.limit != -1;

  public bool isBuyable => !this.hasLimit || this.limit > this.buyCount;

  public POINT_SHOP_ITEM_LIMIT_TYPE limitPeriodType
  {
    get
    {
      return !Enum.IsDefined(typeof (POINT_SHOP_ITEM_LIMIT_TYPE), (object) this.limitType) ? POINT_SHOP_ITEM_LIMIT_TYPE.NONE : (POINT_SHOP_ITEM_LIMIT_TYPE) this.limitType;
    }
  }
}
