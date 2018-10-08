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
			string message = androidJavaClass.CallStatic<string>("getGoogleAccounts", new object[1]
			{
				uniqueDeviceId
			});
			result = JSONSerializer.Deserialize<GoogleAccountInfo>(message);
			return result;
		}
		catch (Exception message2)
		{
			Debug.LogError(message2);
			return result;
		}
	}

	public static string getUniqueDeviceId()
	{
		string result = "TestDevice";
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper");
			result = androidJavaClass.CallStatic<string>("getUniqueId", new object[1]
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
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("jp.colopl.gcm.RegistrarHelper");
			androidJavaClass.CallStatic("CreateRegistrationId");
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
		MonoBehaviourSingleton<FCMManager>.I.StartRegist();
	}

	public static int getNativeVersionCode()
	{
		int result = 1;
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper");
			result = androidJavaClass.CallStatic<int>("getVersionCode", new object[0]);
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
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper");
			result = androidJavaClass.CallStatic<string>("getVersionName", new object[0]);
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
		return getNativeVersionName().Replace(".", string.Empty);
	}

	public static bool isRazerPhone()
	{
		bool result = false;
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper");
			result = androidJavaClass.CallStatic<bool>("isRazerPhone", new object[0]);
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
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper");
			result = androidJavaClass.CallStatic<string>("getSystemProperty", new object[1]
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
		bool result = false;
		if (getSystemPropertys("ro.product.brand") == "razer" && getSystemPropertys("ro.razer.internal.mask") == "254" && getSystemPropertys("ro.razer.internal.api") == "1" && getSystemPropertys("ro.razer.internal.list") == "9" && getSystemPropertys("ro.razer.internal.zval") == "26")
		{
			result = true;
		}
		return result;
	}

	public static int getNativeAsset()
	{
		int result = 1;
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper");
			result = androidJavaClass.CallStatic<int>("getAsset", new object[1]
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
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper");
			result = androidJavaClass.CallStatic<int>("getAnalytics", new object[0]);
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
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper");
			androidJavaClass.CallStatic("setSidToken", token);
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
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper");
			androidJavaClass.CallStatic("setHost", host);
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
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("jp.colopl.libs.Cookie");
			androidJavaClass.CallStatic("setCookieToken", token);
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
	}

	public static string getDefaultUserAgent()
	{
		string result = string.Empty;
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass(Property.BundleIdentifier + ".NetworkHelper");
			result = androidJavaClass.CallStatic<string>("getDefaultUserAgent", new object[0]);
			return result;
		}
		catch (Exception message)
		{
			Debug.LogError(message);
			return result;
		}
	}
}
