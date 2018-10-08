using System;
using UnityEngine;

namespace GetSocialSdk.Core
{
	public sealed class NotificationsCountQuery : IConvertableToNative
	{
		private static readonly Notification.NotificationTypes[] AllTypes = new Notification.NotificationTypes[0];

		private readonly bool? _isRead;

		private Notification.NotificationTypes[] _types = AllTypes;

		private NotificationsCountQuery(bool? isRead)
		{
			_isRead = isRead;
		}

		public static NotificationsCountQuery ReadAndUnread()
		{
			return new NotificationsCountQuery(null);
		}

		public static NotificationsCountQuery Read()
		{
			return new NotificationsCountQuery(true);
		}

		public static NotificationsCountQuery Unread()
		{
			return new NotificationsCountQuery(false);
		}

		public NotificationsCountQuery OfAllTypes()
		{
			_types = AllTypes;
			return this;
		}

		public NotificationsCountQuery OfTypes(params Notification.NotificationTypes[] types)
		{
			_types = types;
			return this;
		}

		public AndroidJavaObject ToAjo()
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Expected O, but got Unknown
			AndroidJavaClass ajo = new AndroidJavaClass("im.getsocial.sdk.pushnotifications.NotificationsCountQuery");
			AndroidJavaObject val = (!_isRead.HasValue) ? ajo.CallStaticAJO("readAndUnread") : ajo.CallStaticAJO((!_isRead.Value) ? "unread" : "read");
			if (_types.Length > 0)
			{
				int[] array = Array.ConvertAll(_types, (Notification.NotificationTypes type) => (int)type);
				val.CallAJO("ofTypes", array);
			}
			return val;
		}
	}
}
