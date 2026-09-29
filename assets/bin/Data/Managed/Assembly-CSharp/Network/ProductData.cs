// Decompiled with JetBrains decompiler
// Type: Network.ProductData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

#nullable disable
namespace Network;

public class ProductData
{
  public string productId;
  public double price;
  public double priceIncludeTax;
  public int crystalNum;
  public string discount;
  public string name;
  public string iconImg;
  public bool isSpecial;
  public string promo;
  public int remainingDay;
  public int offerType;
  public int productType;
  public string skipId;
  public double oldPrice;
  public List<Item> items;
  public int purchaseLeft;
}
