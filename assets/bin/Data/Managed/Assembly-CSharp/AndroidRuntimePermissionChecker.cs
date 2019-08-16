using System;
using UnityEngine;

public class AndroidRuntimePermissionChecker
{
	private const string ANDROID_CONTEXT_CLASS_NAME = "com.unity3d.player.UnityPlayer";

	private static AndroidJavaObject GetActivity()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		AndroidJavaClass val = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		try
		{
			return val.GetStatic<AndroidJavaObject>("currentActivity");
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
	}

	private static bool IsAndroidMOrGreater()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		AndroidJavaClass val = new AndroidJavaClass("android.os.Build$VERSION");
		try
		{
			return val.GetStatic<int>("SDK_INT") >= 23;
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
	}

	public static bool CheckPermissions(string[] permissions)
	{
		bool result = true;
		if (IsAndroidMOrGreater() && permissions != null)
		{
			for (int i = 0; i < permissions.Length; i++)
			{
				result = CheckPermission(permissions[i]);
			}
		}
		return result;
	}

	private static bool CheckPermission(string permission)
	{
		AndroidJavaObject activity = GetActivity();
		try
		{
			return activity.Call<int>("checkSelfPermission", new object[1]
			{
				permission
			}) == 0;
		}
		finally
		{
			((IDisposable)activity)?.Dispose();
		}
	}

	public static void RequestPermission(string[] permissiions)
	{
		if (IsAndroidMOrGreater())
		{
			AndroidJavaObject activity = GetActivity();
			try
			{
				activity.Call("requestPermissions", new object[2]
				{
					permissiions,
					0
				});
			}
			finally
			{
				((IDisposable)activity)?.Dispose();
			}
		}
	}
}
