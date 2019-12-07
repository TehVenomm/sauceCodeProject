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
			m_purchaseType = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper").CallStatic<int>("getPurchaseType", Array.Empty<object>());
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
			result = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper").CallStatic<string>("getScreenLockMode", Array.Empty<object>()).Equals("true");
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
			new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper").CallStatic("setScreenLockMode", flag ? "true" : "false");
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
			return new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper").CallStatic<int>("isAdsRemoved", Array.Empty<object>()) == 1;
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
			new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper").CallStatic("setShopMode", flag);
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
			new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper").CallStatic("quit");
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
			new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper").CallStatic("ProcessKillCommit");
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
			new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper").CallStatic("ShowInvitationCodeView", titleText, descriptionText, message);
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
			new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper").CallStatic("OpenURL", url);
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
			num = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper").CallStatic<int>("checkInstallPackage", new object[1]
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
			new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper").CallStatic("resetPackagePreferences");
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
			new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper").CallStatic("sendAdidWithUid", userId, debugFlg);
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
			new AndroidJavaClass(Property.BundleIdentifier + ".AnalyticsHelper").CallStatic("trackPageView", page);
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
			new AndroidJavaClass(Property.BundleIdentifier + ".InAppBillingHelper").CallStatic("requestMarket", productId, userId, userIdHash);
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
			new AndroidJavaClass(Property.BundleIdentifier + ".InAppBillingHelper").CallStatic("getProductDatas", productIds);
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
			new AndroidJavaClass(Property.BundleIdentifier + ".InAppBillingHelper").CallStatic("setProductNameData", datas);
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
			new AndroidJavaClass(Property.BundleIdentifier + ".InAppBillingHelper").CallStatic("setProductIdData", datas);
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
			new AndroidJavaClass(Property.BundleIdentifier + ".InAppBillingHelper").CallStatic("checkAndGivePromotionitems", productIds);
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
			new AndroidJavaClass(Property.BundleIdentifier + ".InAppBillingHelper").CallStatic("restorePurchasedItem", showErrorDialog);
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
			new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper").CallStatic("trackUserRegEventAppsFlyer", userId);
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	public static void RegisterLocalNotification(int id, string title, string body, int afterSeconds)
	{
		if ((MonoBehaviourSingleton<UserInfoManager>.I.userInfo.pushEnable & 1) != 0 && 0 < afterSeconds)
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
			string text = androidJavaClass.CallStatic<string>("GetInstallReferrerAtInstall", Array.Empty<object>());
			Debug.Log("Selected Referrer:" + text);
			if (text.Contains("&"))
			{
				string[] array = text.Split('&');
				foreach (string text2 in array)
				{
					if (text2.Contains("="))
					{
						Debug.LogError(text2);
						string[] array2 = text2.Split('=');
						if (array2.Length == 2)
						{
							if (array2[0] == "a")
							{
								userFromAttributeData.fromAffiliate = array2[1];
							}
							if (array2[0] == "p")
							{
								userFromAttributeData.fromParam = array2[1];
							}
							if (array2[0] == "c")
							{
								userFromAttributeData.fromCode = array2[1];
							}
						}
					}
				}
			}
			return userFromAttributeData;
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
			return new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper").CallStatic<bool>("CheckReferrerSendToAppBrowser", Array.Empty<object>());
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
			return apphelper.CallStatic<int>("GetDeviceAutoRotateSetting", Array.Empty<object>()) == 1;
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
