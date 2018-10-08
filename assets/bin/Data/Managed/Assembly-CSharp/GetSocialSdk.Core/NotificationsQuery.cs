using System;
using UnityEngine;

namespace GetSocialSdk.Core
{
	public sealed class NotificationsQuery : IConvertableToNative
	{
		public enum Filter
		{
			NoFilter,
			Older,
			Newer
		}

		private static readonly Notification.NotificationTypes[] AllTypes = new Notification.NotificationTypes[0];

		private readonly bool? _isRead;

		private Notification.NotificationTypes[] _types = AllTypes;

		private Filter _filter;

		private string _notificationId;

		private int _limit;

		private NotificationsQuery(bool? isRead)
		{
			_isRead = isRead;
		}

		public static NotificationsQuery ReadAndUnread()
		{
			return new NotificationsQuery(null);
		}

		public static NotificationsQuery Read()
		{
			return new NotificationsQuery(true);
		}

		public static NotificationsQuery Unread()
		{
			return new NotificationsQuery(false);
		}

		public NotificationsQuery OfAllTypes()
		{
			_types = AllTypes;
			return this;
		}

		public NotificationsQuery OfTypes(params Notification.NotificationTypes[] types)
		{
			_types = types;
			return this;
		}

		public NotificationsQuery WithLimit(int limit)
		{
			_limit = limit;
			return this;
		}

		public NotificationsQuery WithFilter(Filter filter, string notificationId)
		{
			_filter = filter;
			_notificationId = notificationId;
			return this;
		}

		public AndroidJavaObject ToAjo()
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Expected O, but got Unknown
			AndroidJavaClass ajo = new AndroidJavaClass("im.getsocial.sdk.pushnotifications.NotificationsQuery");
			AndroidJavaObject val = (!_isRead.HasValue) ? ajo.CallStaticAJO("readAndUnread") : ajo.CallStaticAJO((!_isRead.Value) ? "unread" : "read");
			val.CallAJO("withLimit", _limit);
			if (_types.Length > 0)
			{
				int[] array = Array.ConvertAll(_types, (Notification.NotificationTypes type) => (int)type);
				val.CallAJO("ofTypes", array);
			}
			if (_filter != 0)
			{
				val.CallAJO("withFilter", _filter.ToAndroidJavaObject(), _notificationId);
			}
			return val;
		}
	}
}
