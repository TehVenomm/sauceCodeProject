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
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		if (m_purchaseType != -1)
		{
			return m_purchaseType;
		}
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			m_purchaseType = val.CallStatic<int>("getPurchaseType", new object[0]);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
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
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		bool result = true;
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			string text = val.CallStatic<string>("getScreenLockMode", new object[0]);
			result = text.Equals("true");
			return result;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			return result;
		}
	}

	public static void setScreenLockMode(bool flag)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			val.CallStatic("setScreenLockMode", new object[1]
			{
				(!flag) ? "false" : "true"
			});
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public static bool IsAdsRemoved()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			int num = val.CallStatic<int>("isAdsRemoved", new object[0]);
			return num == 1;
			IL_0031:;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
		return false;
	}

	public static void SetShopMenuButton(bool flag)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			val.CallStatic("setShopMode", new object[1]
			{
				flag
			});
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public static void applicationQuit()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			val.CallStatic("quit", new object[0]);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public static void ProcessKillCommit()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			val.CallStatic("ProcessKillCommit", new object[0]);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public static void launchMyselfMarket()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			if (val != null && GetPurchaseType() == 0)
			{
				val.CallStatic("GoGooglePlayMyself", new object[0]);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public static void LaunchMailerInvitation(string titleText, string descriptionText, string message)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			val.CallStatic("ShowInvitationCodeView", new object[3]
			{
				titleText,
				descriptionText,
				message
			});
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public static void OpenURL(string url)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			val.CallStatic("OpenURL", new object[1]
			{
				url
			});
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public static bool CheckInstallPackage(string pacakg_name)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		int num = 0;
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			num = val.CallStatic<int>("checkInstallPackage", new object[1]
			{
				pacakg_name
			});
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
		return num == 1;
	}

	public static void ResetPackagePreferences()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			val.CallStatic("resetPackagePreferences", new object[0]);
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public static void SendIdfaOrAdidWithUid(string userId, bool debugFlg)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			val.CallStatic("sendAdidWithUid", new object[2]
			{
				userId,
				debugFlg
			});
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public static void TrackPageView(string page)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".AnalyticsHelper");
			val.CallStatic("trackPageView", new object[1]
			{
				page
			});
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public static void RequestPurchase(string productId, string userId, string userIdHash)
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
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
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".InAppBillingHelper");
			val.CallStatic("requestMarket", new object[3]
			{
				productId,
				userId,
				userIdHash
			});
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public static void GetProductDatas(string productIds)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".InAppBillingHelper");
			val.CallStatic("getProductDatas", new object[1]
			{
				productIds
			});
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public static void SetProductNameData(string datas)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".InAppBillingHelper");
			val.CallStatic("setProductNameData", new object[1]
			{
				datas
			});
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public static void SetProductIdData(string datas)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".InAppBillingHelper");
			val.CallStatic("setProductIdData", new object[1]
			{
				datas
			});
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public static void checkAndGivePromotionItems(string productIds)
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
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
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".InAppBillingHelper");
			val.CallStatic("checkAndGivePromotionitems", new object[1]
			{
				productIds
			});
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public static void RestorePurchasedItem(bool showErrorDialog)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".InAppBillingHelper");
			val.CallStatic("restorePurchasedItem", new object[1]
			{
				showErrorDialog
			});
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public static void TrackUserRegEventAppsFlyer(string userId)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		Debug.Log((object)("TrackUserRegEventAppsFlyer: userId=" + userId));
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			val.CallStatic("trackUserRegEventAppsFlyer", new object[1]
			{
				userId
			});
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public static void RegisterLocalNotification(int id, string title, string body, int afterSeconds)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		if (0 < afterSeconds)
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".LocalNotificationHelper");
			if (val != null)
			{
				val.CallStatic("Register", new object[4]
				{
					id,
					title,
					body,
					afterSeconds
				});
			}
			else
			{
				Debug.Log((object)("not to be found:" + Property.BundleIdentifier + ".LocalNotificationHelper"));
			}
		}
	}

	public static void CancelAllLocalNotification()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".LocalNotificationHelper");
		if (val != null)
		{
			val.CallStatic("CancelAll", new object[0]);
		}
		else
		{
			Debug.Log((object)("not to be found:" + Property.BundleIdentifier + ".LocalNotificationHelper"));
		}
	}

	public static UserFromAttributeData GetInstallReferrer()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			UserFromAttributeData userFromAttributeData = new UserFromAttributeData();
			string text = val.CallStatic<string>("GetInstallReferrerAtInstall", new object[0]);
			Debug.Log((object)("Selected Referrer:" + text));
			if (text.Contains("&"))
			{
				string[] array = text.Split('&');
				string[] array2 = array;
				foreach (string text2 in array2)
				{
					if (text2.Contains("="))
					{
						Debug.LogError((object)text2);
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
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
		return null;
	}

	public static bool CheckReferrerSendToAppBrowser()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			return val.CallStatic<bool>("CheckReferrerSendToAppBrowser", new object[0]);
			IL_002c:;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
		return false;
	}

	public static bool GetDeviceAutoRotateSetting()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		try
		{
			if (apphelper == null)
			{
				apphelper = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
			}
			return apphelper.CallStatic<int>("GetDeviceAutoRotateSetting", new object[0]) == 1;
			IL_0041:;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
		return false;
	}

	public static void getList()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
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
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}
}
