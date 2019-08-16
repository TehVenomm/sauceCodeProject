using UnityEngine;

public class CrashlyticsWrapper
{
	private static AndroidJavaClass cl;

	private static AndroidJavaClass Crashlytics
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			if (cl == null)
			{
				cl = new AndroidJavaClass("com.crashlytics.android.Crashlytics");
			}
			return cl;
		}
	}

	public static void SetBool(string key, bool value)
	{
		key = (key ?? string.Empty);
		Crashlytics.CallStatic("setBool", new object[2]
		{
			key,
			value
		});
	}

	public static void SetString(string key, string value)
	{
		key = (key ?? string.Empty);
		value = (value ?? string.Empty);
		Crashlytics.CallStatic("setString", new object[2]
		{
			key,
			value
		});
	}

	public static void SetInt(string key, int value)
	{
		key = (key ?? string.Empty);
		Crashlytics.CallStatic("setInt", new object[2]
		{
			key,
			value
		});
	}

	public static void SetUserId(int id)
	{
		Crashlytics.CallStatic("setUserEmail", new object[1]
		{
			id.ToString()
		});
	}

	public static void SetUserName(string name)
	{
		name = (name ?? string.Empty);
		Crashlytics.CallStatic("setUserName", new object[1]
		{
			name
		});
	}

	public static void ReportException(string report)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		report = (report ?? string.Empty);
		AndroidJavaObject val = new AndroidJavaObject("java.lang.Exception", new object[1]
		{
			report
		});
		Crashlytics.CallStatic("logException", new object[1]
		{
			val
		});
	}
}
