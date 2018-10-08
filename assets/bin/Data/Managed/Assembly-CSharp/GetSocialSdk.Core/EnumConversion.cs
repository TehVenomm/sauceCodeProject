using System;
using UnityEngine;

namespace GetSocialSdk.Core
{
	public static class EnumConversion
	{
		public static AndroidJavaObject ToAndroidJavaObject(this ReportingReason reportingReason)
		{
			return ToAndroidJavaObject((int)reportingReason, "im.getsocial.sdk.activities.ReportingReason");
		}

		public static AndroidJavaObject ToAndroidJavaObject(this ActivitiesQuery.Filter filter)
		{
			return ToAndroidJavaObject((int)filter, "im.getsocial.sdk.activities.ActivitiesQuery$Filter");
		}

		public static AndroidJavaObject ToAndroidJavaObject(this NotificationsQuery.Filter filter)
		{
			return ToAndroidJavaObject((int)filter, "im.getsocial.sdk.pushnotifications.NotificationsQuery$Filter");
		}

		private static AndroidJavaObject ToAndroidJavaObject<T>(T type, string javaClass) where T : IConvertible
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Expected O, but got Unknown
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Expected O, but got Unknown
			AndroidJavaClass ajo = new AndroidJavaClass(javaClass);
			AndroidJavaObject val = ajo.CallStaticAJO("values");
			return new AndroidJavaClass("java.lang.reflect.Array").CallStaticAJO("get", val, type);
		}
	}
}
