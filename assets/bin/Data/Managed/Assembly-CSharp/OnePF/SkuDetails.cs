// Decompiled with JetBrains decompiler
// Type: OnePF.SkuDetails
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace OnePF;

public class SkuDetails
{
  public string ItemType { get; private set; }

  public string Sku { get; private set; }

  public string Type { get; private set; }

  public string Price { get; private set; }

  public string Title { get; private set; }

  public string Description { get; private set; }

  public string Json { get; private set; }

  public string CurrencyCode { get; private set; }

  public string PriceValue { get; private set; }

  public SkuDetails(string jsonString)
  {
    JSON json = new JSON(jsonString);
    this.ItemType = json.ToString("itemType");
    this.Sku = json.ToString("sku");
    this.Type = json.ToString("type");
    this.Price = json.ToString("price");
    this.Title = json.ToString("title");
    this.Description = json.ToString("description");
    this.Json = json.ToString("json");
    this.CurrencyCode = json.ToString("currencyCode");
    this.PriceValue = json.ToString("priceValue");
    this.ParseFromJson();
  }

  private void ParseFromJson()
  {
    if (string.IsNullOrEmpty(this.Json))
      return;
    JSON json = new JSON(this.Json);
    if (string.IsNullOrEmpty(this.PriceValue))
      this.PriceValue = (json.ToFloat("price_amount_micros") / 1000000f).ToString();
    if (!string.IsNullOrEmpty(this.CurrencyCode))
      return;
    this.CurrencyCode = json.ToString("price_currency_code");
  }

  public override string ToString()
  {
    return $"[SkuDetails: type = {this.ItemType}, SKU = {this.Sku}, title = {this.Title}, price = {this.Price}, description = {this.Description}, priceValue={this.PriceValue}, currency={this.CurrencyCode}]";
  }
}
