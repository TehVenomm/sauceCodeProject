using System;
using UnityEngine;

namespace GetSocialSdk.Core
{
	public static class JniExtensionMethods
	{
		public static string CallStaticStr(this AndroidJavaObject ajo, string methodName, params object[] args)
		{
			return ajo.CallStaticSafe<string>(methodName, args);
		}

		public static bool CallStaticBool(this AndroidJavaObject ajo, string methodName, params object[] args)
		{
			return ajo.CallStaticSafe<bool>(methodName, args);
		}

		public static int CallStaticInt(this AndroidJavaObject ajo, string methodName, params object[] args)
		{
			return ajo.CallStaticSafe<int>(methodName, args);
		}

		public static long CallStaticLong(this AndroidJavaObject ajo, string methodName, params object[] args)
		{
			return ajo.CallStaticSafe<long>(methodName, args);
		}

		public static float CallStaticFloat(this AndroidJavaObject ajo, string methodName, params object[] args)
		{
			return ajo.CallStaticSafe<float>(methodName, args);
		}

		public static AndroidJavaObject CallStaticAJO(this AndroidJavaObject ajo, string methodName, params object[] args)
		{
			return ajo.CallStaticSafe<AndroidJavaObject>(methodName, args);
		}

		public static void CallStaticSafe(this AndroidJavaObject ajo, string methodName, params object[] args)
		{
			try
			{
				ajo.CallStatic(methodName, args);
			}
			catch (Exception)
			{
			}
		}

		public static bool CallBool(this AndroidJavaObject ajo, string methodName, params object[] args)
		{
			return ajo.CallSafe<bool>(methodName, args);
		}

		public static int CallInt(this AndroidJavaObject ajo, string methodName, params object[] args)
		{
			return ajo.CallSafe<int>(methodName, args);
		}

		public static long CallLong(this AndroidJavaObject ajo, string methodName, params object[] args)
		{
			return ajo.CallSafe<long>(methodName, args);
		}

		public static float CallFloat(this AndroidJavaObject ajo, string methodName, params object[] args)
		{
			return ajo.CallSafe<float>(methodName, args);
		}

		public static string CallStr(this AndroidJavaObject ajo, string methodName, params object[] args)
		{
			return ajo.CallSafe<string>(methodName, args);
		}

		public static AndroidJavaObject CallAJO(this AndroidJavaObject ajo, string methodName, params object[] args)
		{
			return ajo.CallSafe<AndroidJavaObject>(methodName, args);
		}

		public static void CallSafe(this AndroidJavaObject ajo, string methodName, params object[] args)
		{
			try
			{
				ajo.Call(methodName, args);
			}
			catch (Exception)
			{
			}
		}

		public static T GetSafe<T>(this AndroidJavaObject ajo, string fieldName)
		{
			try
			{
				return ajo.Get<T>(fieldName);
				IL_000d:
				T result;
				return result;
			}
			catch (Exception exception)
			{
				HandleAndroidJavaObjectCallException<T>(ajo, fieldName, exception);
				return default(T);
				IL_002a:
				T result;
				return result;
			}
		}

		private static T CallStaticSafe<T>(this AndroidJavaObject ajo, string methodName, params object[] args)
		{
			try
			{
				return ajo.CallStatic<T>(methodName, args);
				IL_000e:
				T result;
				return result;
			}
			catch (Exception exception)
			{
				HandleAndroidJavaObjectCallException<T>(ajo, methodName, exception);
				return default(T);
				IL_002b:
				T result;
				return result;
			}
		}

		private static T CallSafe<T>(this AndroidJavaObject ajo, string methodName, params object[] args)
		{
			try
			{
				return ajo.Call<T>(methodName, args);
				IL_000e:
				T result;
				return result;
			}
			catch (Exception exception)
			{
				HandleAndroidJavaObjectCallException<T>(ajo, methodName, exception);
				return default(T);
				IL_002b:
				T result;
				return result;
			}
		}

		private static void HandleAndroidJavaObjectCallException<T>(AndroidJavaObject ajo, string methodName, Exception exception)
		{
			if (typeof(T) == typeof(AndroidJavaObject) && exception.Message.Contains("AndroidJavaObject with null ptr"))
			{
				return;
			}
		}
	}
}
