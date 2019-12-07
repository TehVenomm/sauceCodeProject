using UnityEngine;

public class AndroidRuntimePermissionChecker
{
	private const string ANDROID_CONTEXT_CLASS_NAME = "com.unity3d.player.UnityPlayer";

	private static AndroidJavaObject GetActivity()
	{
		using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			return androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		}
	}

	private static bool IsAndroidMOrGreater()
	{
		using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.os.Build$VERSION"))
		{
			return androidJavaClass.GetStatic<int>("SDK_INT") >= 23;
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
		using (AndroidJavaObject androidJavaObject = GetActivity())
		{
			return androidJavaObject.Call<int>("checkSelfPermission", new object[1]
			{
				permission
			}) == 0;
		}
	}

	public static void RequestPermission(string[] permissiions)
	{
		if (IsAndroidMOrGreater())
		{
			using (AndroidJavaObject androidJavaObject = GetActivity())
			{
				androidJavaObject.Call("requestPermissions", permissiions, 0);
			}
		}
	}
}
