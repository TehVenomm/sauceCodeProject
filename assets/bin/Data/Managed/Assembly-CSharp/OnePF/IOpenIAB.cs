// Decompiled with JetBrains decompiler
// Type: OnePF.IOpenIAB
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
namespace OnePF;

public interface IOpenIAB
{
  void setGameUserId(string id);

  void setEmail(string email);

  void init(Options options, string goPayAppKey, string goPaySecret);

  void mapSku(string sku, string storeName, string storeSku);

  void unbindService();

  bool areSubscriptionsSupported();

  void queryInventory();

  void queryInventory(string[] inAppSkus);

  void purchaseProduct(string sku, string developerPayload = "");

  void purchaseSubscription(string sku, string developerPayload = "");

  void consumeProduct(Purchase purchase);

  void restoreTransactions();

  bool isDebugLog();

  void enableDebugLogging(bool enabled);

  void enableDebugLogging(bool enabled, string tag);
}
