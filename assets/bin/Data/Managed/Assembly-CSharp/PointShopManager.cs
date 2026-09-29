// Decompiled with JetBrains decompiler
// Type: PointShopManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class PointShopManager
{
  public void SendGetPointShops(Action<bool, List<PointShop>> call_back)
  {
    Protocol.Send<PointShopModel>("ajax/pointshop/list", (WWWForm) null, (Action<PointShopModel>) (ret =>
    {
      bool flag = false;
      if (ret.Error == Error.None)
      {
        this.pointShopList = ret.result;
        flag = true;
      }
      call_back(flag, this.pointShopList);
    }));
  }

  public void SendPointShopBuy(
    PointShopItem pointShopItem,
    PointShop pointShop,
    int num,
    Action<bool> call_back)
  {
    Protocol.Send<PointShopBuyModel.SendForm, PointShopBuyModel>(PointShopBuyModel.URL, new PointShopBuyModel.SendForm()
    {
      uid = pointShopItem.pointShopItemId,
      num = num
    }, (Action<PointShopBuyModel>) (result =>
    {
      bool flag = false;
      if (result != null && result.Error == Error.None)
      {
        pointShopItem.buyCount += num;
        pointShop.userPoint -= pointShopItem.needPoint * num;
        flag = true;
      }
      call_back(flag);
    }));
  }

  public static string GetBoughtMessage(PointShopItem item, int num)
  {
    return item.itemId != 1200000 ? string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 7U), (object) item.name, (object) num) : string.Format(StringTable.Get(STRING_CATEGORY.POINT_SHOP, 8U), (object) item.name, (object) num);
  }

  public List<PointShop> pointShopList { get; private set; }
}
