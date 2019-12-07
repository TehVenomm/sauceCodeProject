using UnityEngine;

public class CrashlyticsWrapper
{
	private static AndroidJavaClass cl;

	private static AndroidJavaClass Crashlytics
	{
		get
		{
			if (cl == null)
			{
				cl = new AndroidJavaClass("com.crashlytics.android.Crashlytics");
			}
			return cl;
		}
	}

	public static void SetBool(string key, bool value)
	{
		key = (key ?? "");
		Crashlytics.CallStatic("setBool", key, value);
	}

	public static void SetString(string key, string value)
	{
		key = (key ?? "");
		value = (value ?? "");
		Crashlytics.CallStatic("setString", key, value);
	}

	public static void SetInt(string key, int value)
	{
		key = (key ?? "");
		Crashlytics.CallStatic("setInt", key, value);
	}

	public static void SetUserId(int id)
	{
		Crashlytics.CallStatic("setUserEmail", id.ToString());
	}

	public static void SetUserName(string name)
	{
		name = (name ?? "");
		Crashlytics.CallStatic("setUserName", name);
	}

	public static void ReportException(string report)
	{
		report = (report ?? "");
		AndroidJavaObject androidJavaObject = new AndroidJavaObject("java.lang.Exception", report);
		Crashlytics.CallStatic("logException", androidJavaObject);
	}
}
