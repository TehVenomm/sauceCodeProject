using UnityEngine;

public class AndroidPermissionsManager
{
	private static AndroidJavaObject m_Activity;

	private static AndroidJavaObject m_PermissionService;

	private static AndroidJavaObject GetActivity()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		if (m_Activity != null)
		{
			return m_Activity;
		}
		AndroidJavaClass val = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		m_Activity = val.GetStatic<AndroidJavaObject>("currentActivity");
		return m_Activity;
	}

	private static AndroidJavaObject GetPermissionsService()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		object obj = m_PermissionService;
		if (obj == null)
		{
			AndroidJavaObject val = new AndroidJavaObject("com.unity3d.player.UnityAndroidPermissions", new object[0]);
			obj = (object)val;
			m_PermissionService = val;
		}
		return obj;
	}

	public static bool IsPermissionGranted(string permissionName)
	{
		return GetPermissionsService().Call<bool>("IsPermissionGranted", new object[2]
		{
			GetActivity(),
			permissionName
		});
	}

	public static bool ShouldShowRequestPermission(string permissionName)
	{
		return GetPermissionsService().Call<bool>("ShouldShowRequestPermission", new object[2]
		{
			GetActivity(),
			permissionName
		});
	}

	public static void OpenAppSetting()
	{
		GetPermissionsService().Call("OpenAppSetting", new object[1]
		{
			GetActivity()
		});
	}

	public static void RequestPermission(string[] permissionNames, AndroidPermissionCallback callback)
	{
		GetPermissionsService().Call("RequestPermissionAsync", new object[3]
		{
			GetActivity(),
			permissionNames,
			callback
		});
	}
}
