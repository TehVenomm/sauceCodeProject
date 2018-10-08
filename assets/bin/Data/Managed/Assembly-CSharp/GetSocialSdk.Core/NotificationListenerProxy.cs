using System;
using UnityEngine;

namespace GetSocialSdk.Core
{
	internal class NotificationListenerProxy : JavaInterfaceProxy
	{
		private readonly Func<Notification, bool, bool> _onNotification;

		public NotificationListenerProxy(Func<Notification, bool, bool> onNotification)
			: base("im.getsocial.sdk.pushnotifications.NotificationListener")
		{
			_onNotification = onNotification;
		}

		private bool onNotificationReceived(AndroidJavaObject ajo, bool wasClicked)
		{
			return _onNotification != null && _onNotification.Invoke(new Notification().ParseFromAJO(ajo), wasClicked);
		}
	}
}
