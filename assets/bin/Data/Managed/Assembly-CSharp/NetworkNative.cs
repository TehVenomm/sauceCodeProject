// Decompiled with JetBrains decompiler
// Type: NetworkNative
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class NetworkNative
{
  public const string UNIQUEDEVICE_NUM = "e87e03526ab";

  public static NetworkNative.GoogleAccountInfo getGoogleAccounts()
  {
    NetworkNative.GoogleAccountInfo googleAccounts = new NetworkNative.GoogleAccountInfo();
    try
    {
      googleAccounts = JSONSerializer.Deserialize<NetworkNative.GoogleAccountInfo>(((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper")).CallStatic<string>(nameof (getGoogleAccounts), new object[1]
      {
        (object) NetworkNative.getUniqueDeviceId()
      }));
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    return googleAccounts;
  }

  public static string getAppStr() => AppMain.appStr;

  public static string getUniqueDeviceId()
  {
    string uniqueDeviceId = "TestDevice";
    try
    {
      uniqueDeviceId = ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper")).CallStatic<string>("getUniqueId", new object[1]
      {
        (object) "e87e03526ab"
      });
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    return uniqueDeviceId;
  }

  public static void createRegistrationId() => MonoBehaviourSingleton<FCMManager>.I.StartRegist();

  public static int getNativeVersionCode()
  {
    int nativeVersionCode = 1;
    try
    {
      nativeVersionCode = ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper")).CallStatic<int>("getVersionCode", Array.Empty<object>());
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    return nativeVersionCode;
  }

  public static string getNativeVersionName()
  {
    string nativeVersionName = "1.0.29";
    try
    {
      nativeVersionName = ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper")).CallStatic<string>("getVersionName", Array.Empty<object>());
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    return nativeVersionName;
  }

  public static Version getNativeVersionFromName()
  {
    return new Version(NetworkNative.getNativeVersionName());
  }

  public static string getNativeVersionNameRemoveDot()
  {
    return NetworkNative.getNativeVersionName().Replace(".", "");
  }

  public static bool isRazerPhone()
  {
    bool flag = false;
    try
    {
      flag = ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper")).CallStatic<bool>(nameof (isRazerPhone), Array.Empty<object>());
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    return flag;
  }

  public static string getSystemPropertys(string key)
  {
    string systemPropertys = "--";
    try
    {
      systemPropertys = ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper")).CallStatic<string>("getSystemProperty", new object[1]
      {
        (object) key
      });
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    return systemPropertys;
  }

  public static bool isRunOnRazerPhone() => false;

  public static int getNativeAsset()
  {
    int nativeAsset = 1;
    try
    {
      nativeAsset = ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper")).CallStatic<int>("getAsset", new object[1]
      {
        (object) "start"
      });
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    return nativeAsset;
  }

  public static void getNativeiOSAsset()
  {
  }

  public static int getAnalytics()
  {
    int analytics = 1;
    try
    {
      analytics = ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper")).CallStatic<int>(nameof (getAnalytics), Array.Empty<object>());
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    return analytics;
  }

  public static void setSidToken(string token)
  {
    try
    {
      ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper")).CallStatic(nameof (setSidToken), new object[1]
      {
        (object) token
      });
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  public static void setHost(string host)
  {
    try
    {
      ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper")).CallStatic(nameof (setHost), new object[1]
      {
        (object) host
      });
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  public static void setCookieToken(string token)
  {
    try
    {
      ((AndroidJavaObject) new AndroidJavaClass("jp.colopl.libs.Cookie")).CallStatic(nameof (setCookieToken), new object[1]
      {
        (object) token
      });
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
  }

  public static string getDefaultUserAgent()
  {
    string defaultUserAgent = "Android";
    try
    {
      defaultUserAgent = ((AndroidJavaObject) new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper")).CallStatic<string>(nameof (getDefaultUserAgent), Array.Empty<object>());
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ex);
    }
    return defaultUserAgent;
  }

  public class GoogleAccountInfo
  {
    public List<NetworkNative.GoogleAccount> googleAccounts = new List<NetworkNative.GoogleAccount>();
  }

  public class GoogleAccount
  {
    public string key;
    public string name;

    public GoogleAccount()
    {
    }

    public GoogleAccount(string _k, string _n)
    {
      this.key = _k;
      this.name = _n;
    }
  }
}
