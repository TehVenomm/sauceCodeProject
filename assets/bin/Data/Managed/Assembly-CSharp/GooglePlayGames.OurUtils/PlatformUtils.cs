using System;
using UnityEngine;

namespace GooglePlayGames.OurUtils
{
	public static class PlatformUtils
	{
		public static bool Supported
		{
			get
			{
				//IL_0005: Unknown result type (might be due to invalid IL or missing references)
				//IL_000a: Expected O, but got Unknown
				AndroidJavaClass val = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				AndroidJavaObject @static = val.GetStatic<AndroidJavaObject>("currentActivity");
				AndroidJavaObject val2 = @static.Call<AndroidJavaObject>("getPackageManager", new object[0]);
				AndroidJavaObject val3 = null;
				try
				{
					val3 = val2.Call<AndroidJavaObject>("getLaunchIntentForPackage", new object[1]
					{
						"com.google.android.play.games"
					});
				}
				catch (Exception)
				{
					return false;
					IL_0053:;
				}
				return val3 != null;
			}
		}
	}
}
