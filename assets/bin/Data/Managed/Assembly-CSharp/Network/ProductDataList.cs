// Decompiled with JetBrains decompiler
// Type: Network.ProductDataList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Network;

[Serializable]
public class ProductDataList
{
  public string checkSum;
  public List<SkuAdsData> skuPopups = new List<SkuAdsData>();
  public bool hasPurchaseBundle;
  public List<string> promotionList = new List<string>();
  public List<ProductData> shopList = new List<ProductData>();

  public List<ProductData> GetGemList()
  {
    return this.shopList.Where<ProductData>((Func<ProductData, bool>) (o => !o.isSpecial)).ToList<ProductData>();
  }

  public List<ProductData> GetBundleList()
  {
    return this.shopList.Where<ProductData>((Func<ProductData, bool>) (o => Singleton<ProductDataTable>.I.HasPack(o.productId))).ToList<ProductData>();
  }

  public bool HasPurchasedBundle()
  {
    List<ProductData> list = this.shopList.Where<ProductData>((Func<ProductData, bool>) (o => Singleton<ProductDataTable>.I.HasPack(o.productId))).ToList<ProductData>();
    return list != null && list.Count < Singleton<ProductDataTable>.I.TotalPack();
  }
}
