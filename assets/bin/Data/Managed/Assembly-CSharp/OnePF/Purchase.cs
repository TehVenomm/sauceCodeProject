// Decompiled with JetBrains decompiler
// Type: OnePF.Purchase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace OnePF;

public class Purchase
{
  public string ItemType { get; private set; }

  public string OrderId { get; private set; }

  public string PackageName { get; private set; }

  public string Sku { get; private set; }

  public long PurchaseTime { get; private set; }

  public int PurchaseState { get; private set; }

  public string DeveloperPayload { get; private set; }

  public string Token { get; private set; }

  public string OriginalJson { get; private set; }

  public string Signature { get; private set; }

  public string AppstoreName { get; private set; }

  public string Receipt { get; private set; }

  private Purchase()
  {
  }

  public Purchase(string jsonString)
  {
    JSON json = new JSON(jsonString);
    this.ItemType = json.ToString("itemType");
    this.OrderId = json.ToString("orderId");
    this.PackageName = json.ToString("packageName");
    this.Sku = json.ToString("sku");
    this.PurchaseTime = json.ToLong("purchaseTime");
    this.PurchaseState = json.ToInt("purchaseState");
    this.DeveloperPayload = json.ToString("developerPayload");
    this.Token = json.ToString("token");
    this.OriginalJson = json.ToString("originalJson");
    this.Signature = json.ToString("signature");
    this.AppstoreName = json.ToString("appstoreName");
    this.Receipt = json.ToString("receipt");
  }

  public static Purchase CreateFromSku(string sku) => Purchase.CreateFromSku(sku, "");

  public static Purchase CreateFromSku(string sku, string developerPayload)
  {
    return new Purchase()
    {
      Sku = sku,
      DeveloperPayload = developerPayload
    };
  }

  public override string ToString() => $"SKU:{this.Sku};{this.OriginalJson}";

  public string Serialize()
  {
    return new JSON()
    {
      ["itemType"] = ((object) this.ItemType),
      ["orderId"] = ((object) this.OrderId),
      ["packageName"] = ((object) this.PackageName),
      ["sku"] = ((object) this.Sku),
      ["purchaseTime"] = ((object) this.PurchaseTime),
      ["purchaseState"] = ((object) this.PurchaseState),
      ["developerPayload"] = ((object) this.DeveloperPayload),
      ["token"] = ((object) this.Token),
      ["originalJson"] = ((object) this.OriginalJson),
      ["signature"] = ((object) this.Signature),
      ["appstoreName"] = ((object) this.AppstoreName),
      ["receipt"] = ((object) this.Receipt)
    }.serialized;
  }
}
