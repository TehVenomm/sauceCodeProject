using GetSocialSdk.MiniJSON;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GetSocialSdk.Core
{
	public static class JniUtils
	{
		private const string JavaHashMapClass = "java.util.HashMap";

		private const string JavaArrayList = "java.util.ArrayList";

		private static AndroidJavaObject _activity;

		public static AndroidJavaObject Activity => _activity ?? (_activity = GetMainActivity());

		public static AndroidJavaObject GetMainActivity()
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Expected O, but got Unknown
			AndroidJavaClass val = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			return val.GetStatic<AndroidJavaObject>("currentActivity");
		}

		public unsafe static void RunOnUiThread(Action action)
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Expected O, but got Unknown
			Activity.CallSafe("runOnUiThread", (object)new AndroidJavaRunnable((object)action, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}

		public static bool RunOnUiThreadSafe(Action action)
		{
			try
			{
				RunOnUiThread(action);
				return true;
				IL_000d:
				bool result;
				return result;
			}
			catch (Exception)
			{
				return false;
				IL_001a:
				bool result;
				return result;
			}
		}

		public static void CheckIfClassIsCorrect(AndroidJavaObject ajo, string expectedSimpleClassName)
		{
			string classSimpleName = ajo.GetClassSimpleName();
			if (classSimpleName != expectedSimpleClassName)
			{
				throw new InvalidOperationException($"This AndroidJavaObject is not {expectedSimpleClassName}, it is {classSimpleName}!");
			}
		}

		public static AndroidJavaObject NewJavaThrowable(string message)
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Expected O, but got Unknown
			return new AndroidJavaObject("java.lang.Throwable", new object[1]
			{
				message
			});
		}

		public static bool IsJavaNull(this AndroidJavaObject ajo)
		{
			return ajo == null || ajo.GetRawObject().ToInt32() == 0;
		}

		public static AndroidJavaObject ToJavaHashMap<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Expected O, but got Unknown
			if (dictionary == null)
			{
				return null;
			}
			AndroidJavaObject val = new AndroidJavaObject("java.util.HashMap", new object[0]);
			foreach (KeyValuePair<TKey, TValue> item in dictionary)
			{
				val.CallStr("put", item.Key, item.Value);
			}
			return val;
		}

		public static Dictionary<string, string> FromJavaHashMap(this AndroidJavaObject javaHashMap)
		{
			if (javaHashMap == null)
			{
				return new Dictionary<string, string>();
			}
			if (javaHashMap.IsJavaNull())
			{
				return new Dictionary<string, string>();
			}
			int capacity = javaHashMap.CallInt("size");
			Dictionary<string, string> dictionary = new Dictionary<string, string>(capacity);
			AndroidJavaObject ajo = javaHashMap.CallAJO("keySet").CallAJO("iterator");
			while (ajo.CallBool("hasNext"))
			{
				string text = ajo.CallStr("next");
				string value = javaHashMap.CallStr("get", text);
				dictionary.Add(text, value);
			}
			javaHashMap.Dispose();
			return dictionary;
		}

		public static List<AndroidJavaObject> FromJavaList(this AndroidJavaObject javaList)
		{
			List<AndroidJavaObject> list = new List<AndroidJavaObject>();
			try
			{
				AndroidJavaObject val = javaList.CallAJO("iterator");
				try
				{
					while (val.CallBool("hasNext"))
					{
						AndroidJavaObject item = val.CallAJO("next");
						list.Add(item);
					}
					return list;
				}
				finally
				{
					((IDisposable)val)?.Dispose();
				}
			}
			finally
			{
				((IDisposable)javaList)?.Dispose();
			}
		}

		public static AndroidJavaObject ToJavaList<T>(this List<T> list)
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Expected O, but got Unknown
			if (list == null)
			{
				return null;
			}
			AndroidJavaObject javaList = new AndroidJavaObject("java.util.ArrayList", new object[0]);
			list.ForEach((Action<T>)delegate(T item)
			{
				javaList.CallBool("add", item);
			});
			return javaList;
		}

		public static GetSocialError ToGetSocialError(this AndroidJavaObject getSocialExceptionAJO)
		{
			return new GetSocialError().ParseFromAJO(getSocialExceptionAJO);
		}

		public static string GetClassName(this AndroidJavaObject ajo)
		{
			return ajo.GetJavaClass().CallStr("getName");
		}

		public static string GetClassSimpleName(this AndroidJavaObject ajo)
		{
			return ajo.GetJavaClass().CallStr("getSimpleName");
		}

		public static AndroidJavaObject GetJavaClass(this AndroidJavaObject ajo)
		{
			return ajo.CallAJO("getClass");
		}

		public static AndroidJavaObject ToJavaStringArray(this IList<string> values)
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Expected O, but got Unknown
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Expected O, but got Unknown
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Expected O, but got Unknown
			if (values == null)
			{
				return null;
			}
			AndroidJavaClass val = new AndroidJavaClass("java.lang.reflect.Array");
			AndroidJavaObject val2 = val.CallStatic<AndroidJavaObject>("newInstance", new object[2]
			{
				(object)new AndroidJavaClass("java.lang.String"),
				values.Count
			});
			for (int i = 0; i < values.Count; i++)
			{
				val.CallStatic("set", new object[3]
				{
					val2,
					i,
					(object)new AndroidJavaObject("java.lang.String", new object[1]
					{
						values[i]
					})
				});
			}
			return val2;
		}

		public static Texture2D FromAndroidBitmap(this AndroidJavaObject bitmapAJO)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Expected O, but got Unknown
			if (!bitmapAJO.IsJavaNull())
			{
				return new AndroidJavaClass("im.getsocial.sdk.internal.unity.BitmapFactory").CallStaticStr("encodeBase64", bitmapAJO).FromBase64();
			}
			return null;
		}

		public static AndroidJavaObject ToAjoBitmap(this Texture2D texture2D)
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Expected O, but got Unknown
			return new AndroidJavaClass("im.getsocial.sdk.internal.unity.BitmapFactory").CallStaticAJO("decodeBase64", texture2D.TextureToBase64());
		}
	}
}
