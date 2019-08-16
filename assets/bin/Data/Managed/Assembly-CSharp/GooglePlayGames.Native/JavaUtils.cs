using GooglePlayGames.OurUtils;
using System;
using System.Reflection;
using UnityEngine;

namespace GooglePlayGames.Native
{
	internal static class JavaUtils
	{
		private static ConstructorInfo IntPtrConstructor = typeof(AndroidJavaObject).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[1]
		{
			typeof(IntPtr)
		}, null);

		internal static AndroidJavaObject JavaObjectFromPointer(IntPtr jobject)
		{
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Expected O, but got Unknown
			if (jobject == IntPtr.Zero)
			{
				return null;
			}
			return IntPtrConstructor.Invoke(new object[1]
			{
				jobject
			});
		}

		internal static AndroidJavaObject NullSafeCall(this AndroidJavaObject target, string methodName, params object[] args)
		{
			try
			{
				return target.Call<AndroidJavaObject>(methodName, args);
			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("null"))
				{
					return null;
				}
				Logger.w("CallObjectMethod exception: " + ex);
				return null;
			}
		}
	}
}
