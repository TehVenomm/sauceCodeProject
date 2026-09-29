// Decompiled with JetBrains decompiler
// Type: OnePF.Inventory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace OnePF;

public class Inventory
{
  private Dictionary<string, SkuDetails> _skuMap = new Dictionary<string, SkuDetails>();
  private Dictionary<string, Purchase> _purchaseMap = new Dictionary<string, Purchase>();

  public Inventory(string json)
  {
    JSON json1 = new JSON(json);
    foreach (List<object> objectList in (List<object>) json1.fields["purchaseMap"])
      this._purchaseMap.Add(objectList[0].ToString(), new Purchase(objectList[1].ToString()));
    foreach (List<object> objectList in (List<object>) json1.fields["skuMap"])
      this._skuMap.Add(objectList[0].ToString(), new SkuDetails(objectList[1].ToString()));
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("{purchaseMap:{");
    foreach (KeyValuePair<string, Purchase> purchase in this._purchaseMap)
      stringBuilder.Append($"\"{purchase.Key}\":{{{purchase.Value.ToString()}}},");
    stringBuilder.Append("},");
    stringBuilder.Append("skuMap:{");
    foreach (KeyValuePair<string, SkuDetails> sku in this._skuMap)
      stringBuilder.Append($"\"{sku.Key}\":{{{sku.Value.ToString()}}},");
    stringBuilder.Append("}}");
    return stringBuilder.ToString();
  }

  public SkuDetails GetSkuDetails(string sku)
  {
    return !this._skuMap.ContainsKey(sku) ? (SkuDetails) null : this._skuMap[sku];
  }

  public Purchase GetPurchase(string sku)
  {
    return !this._purchaseMap.ContainsKey(sku) ? (Purchase) null : this._purchaseMap[sku];
  }

  public bool HasPurchase(string sku) => this._purchaseMap.ContainsKey(sku);

  public bool HasDetails(string sku) => this._skuMap.ContainsKey(sku);

  public void ErasePurchase(string sku)
  {
    if (!this._purchaseMap.ContainsKey(sku))
      return;
    this._purchaseMap.Remove(sku);
  }

  public List<string> GetAllOwnedSkus() => this._purchaseMap.Keys.ToList<string>();

  public List<string> GetAllOwnedSkus(string itemType)
  {
    List<string> allOwnedSkus = new List<string>();
    foreach (Purchase purchase in this._purchaseMap.Values)
    {
      if (purchase.ItemType == itemType)
        allOwnedSkus.Add(purchase.Sku);
    }
    return allOwnedSkus;
  }

  public List<Purchase> GetAllPurchases() => this._purchaseMap.Values.ToList<Purchase>();

  public List<SkuDetails> GetAllAvailableSkus() => this._skuMap.Values.ToList<SkuDetails>();

  public void AddSkuDetails(SkuDetails d) => this._skuMap.Add(d.Sku, d);

  public void AddPurchase(Purchase p) => this._purchaseMap.Add(p.Sku, p);
}
