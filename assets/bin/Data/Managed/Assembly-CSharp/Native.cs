// Decompiled with JetBrains decompiler
// Type: Native
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
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
    if (Native.m_purchaseType != -1)
      return Native.m_purchaseType;
    try
    {
      Native.m_purchaseType = ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper")).CallStatic<int>("getPurchaseType", Array.Empty<object>());
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    return Native.m_purchaseType;
  }

  public static void ShowIndicator()
  {
  }

  public static void HideIndicator()
  {
  }

  public static bool getScreenLockMode()
  {
    bool screenLockMode = true;
    try
    {
      screenLockMode = ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper")).CallStatic<string>(nameof (getScreenLockMode), Array.Empty<object>()).Equals("true");
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    return screenLockMode;
  }

  public static void setScreenLockMode(bool flag)
  {
    try
    {
      ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper")).CallStatic(nameof (setScreenLockMode), new object[1]
      {
        flag ? (object) "true" : (object) "false"
      });
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  public static bool IsAdsRemoved()
  {
    try
    {
      return ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper")).CallStatic<int>("isAdsRemoved", Array.Empty<object>()) == 1;
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    return false;
  }

  public static void SetShopMenuButton(bool flag)
  {
    try
    {
      ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper")).CallStatic("setShopMode", new object[1]
      {
        (object) flag
      });
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  public static void applicationQuit()
  {
    try
    {
      ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper")).CallStatic("quit", Array.Empty<object>());
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  public static void ProcessKillCommit()
  {
    try
    {
      ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper")).CallStatic(nameof (ProcessKillCommit), Array.Empty<object>());
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  public static void launchMyselfMarket()
  {
    try
    {
      AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
      if (androidJavaClass == null || Native.GetPurchaseType() != 0)
        return;
      ((AndroidJavaObject) androidJavaClass).CallStatic("GoGooglePlayMyself", Array.Empty<object>());
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  public static void LaunchMailerInvitation(
    string titleText,
    string descriptionText,
    string message)
  {
    try
    {
      ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper")).CallStatic("ShowInvitationCodeView", new object[3]
      {
        (object) titleText,
        (object) descriptionText,
        (object) message
      });
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  public static void OpenURL(string url)
  {
    try
    {
      ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper")).CallStatic(nameof (OpenURL), new object[1]
      {
        (object) url
      });
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  public static bool CheckInstallPackage(string pacakg_name)
  {
    int num = 0;
    try
    {
      num = ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper")).CallStatic<int>("checkInstallPackage", new object[1]
      {
        (object) pacakg_name
      });
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    return num == 1;
  }

  public static void ResetPackagePreferences()
  {
    try
    {
      ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper")).CallStatic("resetPackagePreferences", Array.Empty<object>());
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  public static void SendIdfaOrAdidWithUid(string userId, bool debugFlg)
  {
    try
    {
      ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper")).CallStatic("sendAdidWithUid", new object[2]
      {
        (object) userId,
        (object) debugFlg
      });
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  public static void TrackPageView(string page)
  {
    try
    {
      ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".AnalyticsHelper")).CallStatic("trackPageView", new object[1]
      {
        (object) page
      });
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  public static void RequestPurchase(string productId, string userId, string userIdHash)
  {
    string token = MonoBehaviourSingleton<AccountManager>.I.account.token;
    int num = token.IndexOf('=');
    if (num >= 0)
      token = token.Substring(num + 1);
    NetworkNative.setSidToken(token);
    string host = NetworkManager.APP_HOST;
    if (host.EndsWith("/"))
      host = host.Substring(0, host.Length - 1);
    NetworkNative.setHost(host);
    try
    {
      ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".InAppBillingHelper")).CallStatic("requestMarket", new object[3]
      {
        (object) productId,
        (object) userId,
        (object) userIdHash
      });
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  public static void GetProductDatas(string productIds)
  {
    try
    {
      ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".InAppBillingHelper")).CallStatic("getProductDatas", new object[1]
      {
        (object) productIds
      });
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  public static void SetProductNameData(string datas)
  {
    try
    {
      ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".InAppBillingHelper")).CallStatic("setProductNameData", new object[1]
      {
        (object) datas
      });
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  public static void SetProductIdData(string datas)
  {
    try
    {
      ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".InAppBillingHelper")).CallStatic("setProductIdData", new object[1]
      {
        (object) datas
      });
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  public static void checkAndGivePromotionItems(string productIds)
  {
    string token = MonoBehaviourSingleton<AccountManager>.I.account.token;
    int num = token.IndexOf('=');
    if (num >= 0)
      token = token.Substring(num + 1);
    NetworkNative.setSidToken(token);
    string host = NetworkManager.APP_HOST;
    if (host.EndsWith("/"))
      host = host.Substring(0, host.Length - 1);
    NetworkNative.setHost(host);
    try
    {
      ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".InAppBillingHelper")).CallStatic("checkAndGivePromotionitems", new object[1]
      {
        (object) productIds
      });
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  public static void RestorePurchasedItem(bool showErrorDialog)
  {
    try
    {
      ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".InAppBillingHelper")).CallStatic("restorePurchasedItem", new object[1]
      {
        (object) showErrorDialog
      });
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  public static void TrackUserRegEventAppsFlyer(string userId)
  {
    Debug.Log((object) ("TrackUserRegEventAppsFlyer: userId=" + userId));
    try
    {
      ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper")).CallStatic("trackUserRegEventAppsFlyer", new object[1]
      {
        (object) userId
      });
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  public static void RegisterLocalNotification(
    int id,
    string title,
    string body,
    int afterSeconds)
  {
    if ((MonoBehaviourSingleton<UserInfoManager>.I.userInfo.pushEnable & 1) == 0 || 0 >= afterSeconds)
      return;
    AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".LocalNotificationHelper");
    if (androidJavaClass != null)
      ((AndroidJavaObject) androidJavaClass).CallStatic("Register", new object[4]
      {
        (object) id,
        (object) title,
        (object) body,
        (object) afterSeconds
      });
    else
      Debug.Log((object) $"not to be found:{Property.BundleIdentifier}.LocalNotificationHelper");
  }

  public static void CancelAllLocalNotification()
  {
    AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".LocalNotificationHelper");
    if (androidJavaClass != null)
      ((AndroidJavaObject) androidJavaClass).CallStatic("CancelAll", Array.Empty<object>());
    else
      Debug.Log((object) $"not to be found:{Property.BundleIdentifier}.LocalNotificationHelper");
  }

  public static UserFromAttributeData GetInstallReferrer()
  {
    try
    {
      AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
      UserFromAttributeData installReferrer = new UserFromAttributeData();
      object[] objArray = Array.Empty<object>();
      string str1 = ((AndroidJavaObject) androidJavaClass).CallStatic<string>("GetInstallReferrerAtInstall", objArray);
      Debug.Log((object) ("Selected Referrer:" + str1));
      if (str1.Contains("&"))
      {
        string str2 = str1;
        char[] chArray = new char[1]{ '&' };
        foreach (string str3 in str2.Split(chArray))
        {
          if (str3.Contains("="))
          {
            Debug.LogError((object) str3);
            string[] strArray = str3.Split('=');
            if (strArray.Length == 2)
            {
              if (strArray[0] == "a")
                installReferrer.fromAffiliate = strArray[1];
              if (strArray[0] == "p")
                installReferrer.fromParam = strArray[1];
              if (strArray[0] == "c")
                installReferrer.fromCode = strArray[1];
            }
          }
        }
      }
      return installReferrer;
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    return (UserFromAttributeData) null;
  }

  public static bool CheckReferrerSendToAppBrowser()
  {
    try
    {
      return ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper")).CallStatic<bool>(nameof (CheckReferrerSendToAppBrowser), Array.Empty<object>());
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    return false;
  }

  public static bool GetDeviceAutoRotateSetting()
  {
    try
    {
      if (Native.apphelper == null)
        Native.apphelper = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
      return ((AndroidJavaObject) Native.apphelper).CallStatic<int>(nameof (GetDeviceAutoRotateSetting), Array.Empty<object>()) == 1;
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    return false;
  }

  public static void getList()
  {
    try
    {
      if (Native.apphelper == null)
        Native.apphelper = new AndroidJavaClass(Property.BundleIdentifier + ".AppHelper");
      ((AndroidJavaObject) Native.apphelper).CallStatic<int>("showList", new object[1]
      {
        (object) "other"
      });
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }
}
