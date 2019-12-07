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
		GoogleAccountInfo result = new GoogleAccountInfo();
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper");
			string uniqueDeviceId = getUniqueDeviceId();
			result = JSONSerializer.Deserialize<GoogleAccountInfo>(androidJavaClass.CallStatic<string>("getGoogleAccounts", new object[1]
			{
				uniqueDeviceId
			}));
			return result;
		}
		catch (Exception message)
		{
			Debug.LogError(message);
			return result;
		}
	}

	public static string getAppStr()
	{
		return AppMain.appStr;
	}

	public static string getUniqueDeviceId()
	{
		string result = "TestDevice";
		try
		{
			result = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper").CallStatic<string>("getUniqueId", new object[1]
			{
				"e87e03526ab"
			});
			return result;
		}
		catch (Exception message)
		{
			Debug.LogError(message);
			return result;
		}
	}

	public static void createRegistrationId()
	{
		MonoBehaviourSingleton<FCMManager>.I.StartRegist();
	}

	public static int getNativeVersionCode()
	{
		int result = 1;
		try
		{
			result = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper").CallStatic<int>("getVersionCode", Array.Empty<object>());
			return result;
		}
		catch (Exception message)
		{
			Debug.LogError(message);
			return result;
		}
	}

	public static string getNativeVersionName()
	{
		string result = "1.0.29";
		try
		{
			result = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper").CallStatic<string>("getVersionName", Array.Empty<object>());
			return result;
		}
		catch (Exception message)
		{
			Debug.LogError(message);
			return result;
		}
	}

	public static Version getNativeVersionFromName()
	{
		return new Version(getNativeVersionName());
	}

	public static string getNativeVersionNameRemoveDot()
	{
		return getNativeVersionName().Replace(".", "");
	}

	public static bool isRazerPhone()
	{
		bool result = false;
		try
		{
			result = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper").CallStatic<bool>("isRazerPhone", Array.Empty<object>());
			return result;
		}
		catch (Exception message)
		{
			Debug.LogError(message);
			return result;
		}
	}

	public static string getSystemPropertys(string key)
	{
		string result = "--";
		try
		{
			result = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper").CallStatic<string>("getSystemProperty", new object[1]
			{
				key
			});
			return result;
		}
		catch (Exception message)
		{
			Debug.LogError(message);
			return result;
		}
	}

	public static bool isRunOnRazerPhone()
	{
		return false;
	}

	public static int getNativeAsset()
	{
		int result = 1;
		try
		{
			result = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper").CallStatic<int>("getAsset", new object[1]
			{
				"start"
			});
			return result;
		}
		catch (Exception message)
		{
			Debug.LogError(message);
			return result;
		}
	}

	public static void getNativeiOSAsset()
	{
	}

	public static int getAnalytics()
	{
		int result = 1;
		try
		{
			result = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper").CallStatic<int>("getAnalytics", Array.Empty<object>());
			return result;
		}
		catch (Exception message)
		{
			Debug.LogError(message);
			return result;
		}
	}

	public static void setSidToken(string token)
	{
		try
		{
			new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper").CallStatic("setSidToken", token);
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	public static void setHost(string host)
	{
		try
		{
			new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper").CallStatic("setHost", host);
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	public static void setCookieToken(string token)
	{
		try
		{
			new AndroidJavaClass("jp.colopl.libs.Cookie").CallStatic("setCookieToken", token);
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	public static string getDefaultUserAgent()
	{
		string result = "Android";
		try
		{
			result = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper").CallStatic<string>("getDefaultUserAgent", Array.Empty<object>());
			return result;
		}
		catch (Exception message)
		{
			Debug.LogError(message);
			return result;
		}
	}
}
