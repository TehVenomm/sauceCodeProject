using System;
using UnityEngine;

public static class Native
{
	public const int PURCHASE_TYPE_GOOGLE = 0;

	public const int PURCHASE_TYPE_AMAZON = 1;

	public const int PURCHASE_TYPE_AU = 2;

	public const int PURCHASE_TYPE_GOPAY = 51;

	private static int m_purchaseType = -1;

	private static AndroidJavaClass apphelper;

	public static int GetPurchaseType()
	{
		if (m_purchaseType != -1)
		{
			return m_purchaseType;
		}
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			m_purchaseType = androidJavaClass.CallStatic<int>("getPurchaseType", new object[0]);
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
		return m_purchaseType;
	}

	public static void ShowIndicator()
	{
	}

	public static void HideIndicator()
	{
	}

	public static bool getScreenLockMode()
	{
		bool result = true;
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			string text = androidJavaClass.CallStatic<string>("getScreenLockMode", new object[0]);
			result = text.Equals("true");
			return result;
		}
		catch (Exception message)
		{
			Debug.LogError(message);
			return result;
		}
	}

	public static void setScreenLockMode(bool flag)
	{
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			androidJavaClass.CallStatic("setScreenLockMode", (!flag) ? "false" : "true");
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	public static bool IsAdsRemoved()
	{
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			int num = androidJavaClass.CallStatic<int>("isAdsRemoved", new object[0]);
			return num == 1;
			IL_0031:;
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
		return false;
	}

	public static void SetShopMenuButton(bool flag)
	{
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			androidJavaClass.CallStatic("setShopMode", flag);
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	public static void applicationQuit()
	{
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			androidJavaClass.CallStatic("quit");
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	public static void ProcessKillCommit()
	{
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			androidJavaClass.CallStatic("ProcessKillCommit");
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	public static void launchMyselfMarket()
	{
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			if (androidJavaClass != null && GetPurchaseType() == 0)
			{
				androidJavaClass.CallStatic("GoGooglePlayMyself");
			}
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	public static void LaunchMailerInvitation(string titleText, string descriptionText, string message)
	{
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			androidJavaClass.CallStatic("ShowInvitationCodeView", titleText, descriptionText, message);
		}
		catch (Exception message2)
		{
			Debug.LogError(message2);
		}
	}

	public static void OpenURL(string url)
	{
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			androidJavaClass.CallStatic("OpenURL", url);
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	public static bool CheckInstallPackage(string pacakg_name)
	{
		int num = 0;
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			num = androidJavaClass.CallStatic<int>("checkInstallPackage", new object[1]
			{
				pacakg_name
			});
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
		return num == 1;
	}

	public static void ResetPackagePreferences()
	{
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			androidJavaClass.CallStatic("resetPackagePreferences");
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	public static void SendIdfaOrAdidWithUid(string userId, bool debugFlg)
	{
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			androidJavaClass.CallStatic("sendAdidWithUid", userId, debugFlg);
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	public static void TrackPageView(string page)
	{
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".AnalyticsHelper");
			androidJavaClass.CallStatic("trackPageView", page);
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	public static void RequestPurchase(string productId, string userId, string userIdHash)
	{
		string text = MonoBehaviourSingleton<AccountManager>.I.account.token;
		int num = text.IndexOf('=');
		if (num >= 0)
		{
			text = text.Substring(num + 1);
		}
		NetworkNative.setSidToken(text);
		string text2 = NetworkManager.APP_HOST;
		if (text2.EndsWith("/"))
		{
			text2 = text2.Substring(0, text2.Length - 1);
		}
		NetworkNative.setHost(text2);
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".InAppBillingHelper");
			androidJavaClass.CallStatic("requestMarket", productId, userId, userIdHash);
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	public static void GetProductDatas(string productIds)
	{
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".InAppBillingHelper");
			androidJavaClass.CallStatic("getProductDatas", productIds);
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	public static void SetProductNameData(string datas)
	{
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".InAppBillingHelper");
			androidJavaClass.CallStatic("setProductNameData", datas);
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	public static void SetProductIdData(string datas)
	{
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".InAppBillingHelper");
			androidJavaClass.CallStatic("setProductIdData", datas);
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	public static void checkAndGivePromotionItems(string productIds)
	{
		string text = MonoBehaviourSingleton<AccountManager>.I.account.token;
		int num = text.IndexOf('=');
		if (num >= 0)
		{
			text = text.Substring(num + 1);
		}
		NetworkNative.setSidToken(text);
		string text2 = NetworkManager.APP_HOST;
		if (text2.EndsWith("/"))
		{
			text2 = text2.Substring(0, text2.Length - 1);
		}
		NetworkNative.setHost(text2);
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".InAppBillingHelper");
			androidJavaClass.CallStatic("checkAndGivePromotionitems", productIds);
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	public static void RestorePurchasedItem(bool showErrorDialog)
	{
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".InAppBillingHelper");
			androidJavaClass.CallStatic("restorePurchasedItem", showErrorDialog);
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	public static void TrackUserRegEventAppsFlyer(string userId)
	{
		Debug.Log("TrackUserRegEventAppsFlyer: userId=" + userId);
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			androidJavaClass.CallStatic("trackUserRegEventAppsFlyer", userId);
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	public static void RegisterLocalNotification(int id, string title, string body, int afterSeconds)
	{
		if (0 < afterSeconds)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".LocalNotificationHelper");
			if (androidJavaClass != null)
			{
				androidJavaClass.CallStatic("Register", id, title, body, afterSeconds);
			}
			else
			{
				Debug.Log("not to be found:" + Property.BundleIdentifier + ".LocalNotificationHelper");
			}
		}
	}

	public static void CancelAllLocalNotification()
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".LocalNotificationHelper");
		if (androidJavaClass != null)
		{
			androidJavaClass.CallStatic("CancelAll");
		}
		else
		{
			Debug.Log("not to be found:" + Property.BundleIdentifier + ".LocalNotificationHelper");
		}
	}

	public static UserFromAttributeData GetInstallReferrer()
	{
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			UserFromAttributeData userFromAttributeData = new UserFromAttributeData();
			string text = androidJavaClass.CallStatic<string>("GetInstallReferrerAtInstall", new object[0]);
			Debug.Log("Selected Referrer:" + text);
			if (text.Contains("&"))
			{
				string[] array = text.Split('&');
				string[] array2 = array;
				foreach (string text2 in array2)
				{
					if (text2.Contains("="))
					{
						Debug.LogError(text2);
						string[] array3 = text2.Split('=');
						if (array3.Length == 2)
						{
							if (array3[0] == "a")
							{
								userFromAttributeData.fromAffiliate = array3[1];
							}
							if (array3[0] == "p")
							{
								userFromAttributeData.fromParam = array3[1];
							}
							if (array3[0] == "c")
							{
								userFromAttributeData.fromCode = array3[1];
							}
						}
					}
				}
			}
			return userFromAttributeData;
			IL_0117:;
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
		return null;
	}

	public static bool CheckReferrerSendToAppBrowser()
	{
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			return androidJavaClass.CallStatic<bool>("CheckReferrerSendToAppBrowser", new object[0]);
			IL_002c:;
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
		return false;
	}

	public static bool GetDeviceAutoRotateSetting()
	{
		try
		{
			if (apphelper == null)
			{
				apphelper = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			}
			return apphelper.CallStatic<int>("GetDeviceAutoRotateSetting", new object[0]) == 1;
			IL_0041:;
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
		return false;
	}

	public static void getList()
	{
		try
		{
			if (apphelper == null)
			{
				apphelper = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			}
			apphelper.CallStatic<int>("showList", new object[1]
			{
				"other"
			});
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}
}
