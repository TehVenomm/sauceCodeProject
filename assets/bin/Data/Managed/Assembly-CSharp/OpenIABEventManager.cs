// Decompiled with JetBrains decompiler
// Type: OpenIABEventManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using OnePF;
using System;
using UnityEngine;

#nullable disable
public class OpenIABEventManager : MonoBehaviour
{
  public static event System.Action billingSupportedEvent;

  public static event Action<string> billingNotSupportedEvent;

  public static event Action<Inventory> queryInventorySucceededEvent;

  public static event Action<string> queryInventoryFailedEvent;

  public static event Action<Purchase> purchaseSucceededEvent;

  public static event Action<int, string> purchaseFailedEvent;

  public static event Action<Purchase> consumePurchaseSucceededEvent;

  public static event Action<string> consumePurchaseFailedEvent;

  public static event Action<string> transactionRestoredEvent;

  public static event Action<string> restoreFailedEvent;

  public static event System.Action restoreSucceededEvent;

  private void Awake()
  {
    ((Object) ((Component) this).gameObject).name = ((object) this).GetType().ToString();
    Object.DontDestroyOnLoad((Object) this);
  }

  private void OnMapSkuFailed(string exception)
  {
    Debug.LogError((object) ("SKU mapping failed: " + exception));
  }

  private void OnBillingSupported(string empty)
  {
    if (OpenIABEventManager.billingSupportedEvent == null)
      return;
    OpenIABEventManager.billingSupportedEvent();
  }

  private void OnBillingNotSupported(string error)
  {
    if (OpenIABEventManager.billingNotSupportedEvent == null)
      return;
    OpenIABEventManager.billingNotSupportedEvent(error);
  }

  private void OnQueryInventorySucceeded(string json)
  {
    if (OpenIABEventManager.queryInventorySucceededEvent == null)
      return;
    Inventory inventory = new Inventory(json);
    OpenIABEventManager.queryInventorySucceededEvent(inventory);
  }

  private void OnQueryInventoryFailed(string error)
  {
    if (OpenIABEventManager.queryInventoryFailedEvent == null)
      return;
    OpenIABEventManager.queryInventoryFailedEvent(error);
  }

  private void OnPurchaseSucceeded(string json)
  {
    if (OpenIABEventManager.purchaseSucceededEvent == null)
      return;
    OpenIABEventManager.purchaseSucceededEvent(new Purchase(json));
  }

  private void OnPurchaseFailed(string message)
  {
    int result = -1;
    string str = "Unknown error";
    if (!string.IsNullOrEmpty(message))
    {
      string[] strArray = message.Split('|');
      if (strArray.Length >= 2)
      {
        int.TryParse(strArray[0], out result);
        str = strArray[1];
      }
      else
        str = message;
    }
    if (OpenIABEventManager.purchaseFailedEvent == null)
      return;
    OpenIABEventManager.purchaseFailedEvent(result, str);
  }

  private void OnConsumePurchaseSucceeded(string json)
  {
    if (OpenIABEventManager.consumePurchaseSucceededEvent == null)
      return;
    OpenIABEventManager.consumePurchaseSucceededEvent(new Purchase(json));
  }

  private void OnConsumePurchaseFailed(string error)
  {
    if (OpenIABEventManager.consumePurchaseFailedEvent == null)
      return;
    OpenIABEventManager.consumePurchaseFailedEvent(error);
  }

  public void OnTransactionRestored(string sku)
  {
    if (OpenIABEventManager.transactionRestoredEvent == null)
      return;
    OpenIABEventManager.transactionRestoredEvent(sku);
  }

  public void OnRestoreTransactionFailed(string error)
  {
    if (OpenIABEventManager.restoreFailedEvent == null)
      return;
    OpenIABEventManager.restoreFailedEvent(error);
  }

  public void OnRestoreTransactionSucceeded(string message)
  {
    if (OpenIABEventManager.restoreSucceededEvent == null)
      return;
    OpenIABEventManager.restoreSucceededEvent();
  }
}
