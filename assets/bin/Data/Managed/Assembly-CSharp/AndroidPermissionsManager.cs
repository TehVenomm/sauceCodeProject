using UnityEngine;

public class AndroidPermissionsManager
{
	private static AndroidJavaObject m_Activity;

	private static AndroidJavaObject m_PermissionService;

	private static AndroidJavaObject GetActivity()
	{
		if (m_Activity != null)
		{
			return m_Activity;
		}
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		m_Activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		return m_Activity;
	}

	private static AndroidJavaObject GetPermissionsService()
	{
		return m_PermissionService ?? (m_PermissionService = new AndroidJavaObject("com.unity3d.player.UnityAndroidPermissions"));
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
		GetPermissionsService().Call("OpenAppSetting", GetActivity());
	}

	public static void RequestPermission(string[] permissionNames, AndroidPermissionCallback callback)
	{
		GetPermissionsService().Call("RequestPermissionAsync", GetActivity(), permissionNames, callback);
	}
}
