using OnePF;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class OpenIABEventManager
{
	public static event Action billingSupportedEvent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			OpenIABEventManager.billingSupportedEvent = Delegate.Combine((Delegate)OpenIABEventManager.billingSupportedEvent, (Delegate)value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			OpenIABEventManager.billingSupportedEvent = Delegate.Remove((Delegate)OpenIABEventManager.billingSupportedEvent, (Delegate)value);
		}
	}

	public static event Action<string> billingNotSupportedEvent;

	public static event Action<Inventory> queryInventorySucceededEvent;

	public static event Action<string> queryInventoryFailedEvent;

	public static event Action<Purchase> purchaseSucceededEvent;

	public static event Action<int, string> purchaseFailedEvent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			OpenIABEventManager.purchaseFailedEvent = Delegate.Combine((Delegate)OpenIABEventManager.purchaseFailedEvent, (Delegate)value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			OpenIABEventManager.purchaseFailedEvent = Delegate.Remove((Delegate)OpenIABEventManager.purchaseFailedEvent, (Delegate)value);
		}
	}

	public static event Action<Purchase> consumePurchaseSucceededEvent;

	public static event Action<string> consumePurchaseFailedEvent;

	public static event Action<string> transactionRestoredEvent;

	public static event Action<string> restoreFailedEvent;

	public static event Action restoreSucceededEvent
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		add
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			OpenIABEventManager.restoreSucceededEvent = Delegate.Combine((Delegate)OpenIABEventManager.restoreSucceededEvent, (Delegate)value);
		}
		[MethodImpl(MethodImplOptions.Synchronized)]
		remove
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			OpenIABEventManager.restoreSucceededEvent = Delegate.Remove((Delegate)OpenIABEventManager.restoreSucceededEvent, (Delegate)value);
		}
	}

	public OpenIABEventManager()
		: this()
	{
	}

	private void Awake()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		this.get_gameObject().set_name(GetType().ToString());
		Object.DontDestroyOnLoad(this);
	}

	private void OnMapSkuFailed(string exception)
	{
		Debug.LogError((object)("SKU mapping failed: " + exception));
	}

	private void OnBillingSupported(string empty)
	{
		if (OpenIABEventManager.billingSupportedEvent != null)
		{
			OpenIABEventManager.billingSupportedEvent.Invoke();
		}
	}

	private void OnBillingNotSupported(string error)
	{
		if (OpenIABEventManager.billingNotSupportedEvent != null)
		{
			OpenIABEventManager.billingNotSupportedEvent(error);
		}
	}

	private void OnQueryInventorySucceeded(string json)
	{
		if (OpenIABEventManager.queryInventorySucceededEvent != null)
		{
			Inventory obj = new Inventory(json);
			OpenIABEventManager.queryInventorySucceededEvent(obj);
		}
	}

	private void OnQueryInventoryFailed(string error)
	{
		if (OpenIABEventManager.queryInventoryFailedEvent != null)
		{
			OpenIABEventManager.queryInventoryFailedEvent(error);
		}
	}

	private void OnPurchaseSucceeded(string json)
	{
		if (OpenIABEventManager.purchaseSucceededEvent != null)
		{
			OpenIABEventManager.purchaseSucceededEvent(new Purchase(json));
		}
	}

	private void OnPurchaseFailed(string message)
	{
		int result = -1;
		string text = "Unknown error";
		if (!string.IsNullOrEmpty(message))
		{
			string[] array = message.Split('|');
			if (array.Length >= 2)
			{
				int.TryParse(array[0], out result);
				text = array[1];
			}
			else
			{
				text = message;
			}
		}
		if (OpenIABEventManager.purchaseFailedEvent != null)
		{
			OpenIABEventManager.purchaseFailedEvent.Invoke(result, text);
		}
	}

	private void OnConsumePurchaseSucceeded(string json)
	{
		if (OpenIABEventManager.consumePurchaseSucceededEvent != null)
		{
			OpenIABEventManager.consumePurchaseSucceededEvent(new Purchase(json));
		}
	}

	private void OnConsumePurchaseFailed(string error)
	{
		if (OpenIABEventManager.consumePurchaseFailedEvent != null)
		{
			OpenIABEventManager.consumePurchaseFailedEvent(error);
		}
	}

	public void OnTransactionRestored(string sku)
	{
		if (OpenIABEventManager.transactionRestoredEvent != null)
		{
			OpenIABEventManager.transactionRestoredEvent(sku);
		}
	}

	public void OnRestoreTransactionFailed(string error)
	{
		if (OpenIABEventManager.restoreFailedEvent != null)
		{
			OpenIABEventManager.restoreFailedEvent(error);
		}
	}

	public void OnRestoreTransactionSucceeded(string message)
	{
		if (OpenIABEventManager.restoreSucceededEvent != null)
		{
			OpenIABEventManager.restoreSucceededEvent.Invoke();
		}
	}
}
