using System;
using System.Collections.Generic;
using UnityEngine;

public class NetworkNative
{
	public class GoogleAccountInfo
	{
		public List<GoogleAccount> googleAccounts = new List<GoogleAccount>();
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
			key = _k;
			name = _n;
		}
	}

	public const string UNIQUEDEVICE_NUM = "e87e03526ab";

	public static GoogleAccountInfo getGoogleAccounts()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		GoogleAccountInfo result = new GoogleAccountInfo();
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper");
			string uniqueDeviceId = getUniqueDeviceId();
			string message = val.CallStatic<string>("getGoogleAccounts", new object[1]
			{
				uniqueDeviceId
			});
			result = JSONSerializer.Deserialize<GoogleAccountInfo>(message);
			return result;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			return result;
		}
	}

	public static string getAppStr()
	{
		string empty = string.Empty;
		return AppMain.appStr;
	}

	public static string getUniqueDeviceId()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		string result = "TestDevice";
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper");
			result = val.CallStatic<string>("getUniqueId", new object[1]
			{
				"e87e03526ab"
			});
			return result;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			return result;
		}
	}

	public static void createRegistrationId()
	{
		MonoBehaviourSingleton<FCMManager>.I.StartRegist();
	}

	public static int getNativeVersionCode()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		int result = 1;
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper");
			result = val.CallStatic<int>("getVersionCode", new object[0]);
			return result;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			return result;
		}
	}

	public static string getNativeVersionName()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		string result = "1.0.29";
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper");
			result = val.CallStatic<string>("getVersionName", new object[0]);
			return result;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			return result;
		}
	}

	public static Version getNativeVersionFromName()
	{
		return new Version(getNativeVersionName());
	}

	public static string getNativeVersionNameRemoveDot()
	{
		return getNativeVersionName().Replace(".", string.Empty);
	}

	public static bool isRazerPhone()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		bool result = false;
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper");
			result = val.CallStatic<bool>("isRazerPhone", new object[0]);
			return result;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			return result;
		}
	}

	public static string getSystemPropertys(string key)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		string result = "--";
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper");
			result = val.CallStatic<string>("getSystemProperty", new object[1]
			{
				key
			});
			return result;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			return result;
		}
	}

	public static bool isRunOnRazerPhone()
	{
		bool result = false;
		if (getSystemPropertys("ro.product.brand") == "razer" && getSystemPropertys("ro.razer.internal.mask") == "254" && getSystemPropertys("ro.razer.internal.api") == "1" && getSystemPropertys("ro.razer.internal.list") == "9" && getSystemPropertys("ro.razer.internal.zval") == "26")
		{
			result = true;
		}
		return result;
	}

	public static int getNativeAsset()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		int result = 1;
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper");
			result = val.CallStatic<int>("getAsset", new object[1]
			{
				"start"
			});
			return result;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			return result;
		}
	}

	public static void getNativeiOSAsset()
	{
	}

	public static int getAnalytics()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		int result = 1;
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper");
			result = val.CallStatic<int>("getAnalytics", new object[0]);
			return result;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			return result;
		}
	}

	public static void setSidToken(string token)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper");
			val.CallStatic("setSidToken", new object[1]
			{
				token
			});
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public static void setHost(string host)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper");
			val.CallStatic("setHost", new object[1]
			{
				host
			});
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public static void setCookieToken(string token)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		try
		{
			AndroidJavaClass val = new AndroidJavaClass("jp.colopl.libs.Cookie");
			val.CallStatic("setCookieToken", new object[1]
			{
				token
			});
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
	}

	public static string getDefaultUserAgent()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		string result = string.Empty;
		try
		{
			AndroidJavaClass val = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper");
			result = val.CallStatic<string>("getDefaultUserAgent", new object[0]);
			return result;
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
			return result;
		}
	}
}
