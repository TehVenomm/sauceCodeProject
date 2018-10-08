using System;

namespace GetSocialSdk.Core
{
	internal class OnUserChangedListenerProxy : JavaInterfaceProxy
	{
		private readonly Action _onUserChanged;

		public OnUserChangedListenerProxy(Action onUserChanged)
			: base("im.getsocial.sdk.usermanagement.OnUserChangedListener")
		{
			_onUserChanged = onUserChanged;
		}

		private void onUserChanged()
		{
			JavaInterfaceProxy.ExecuteOnMainThread(_onUserChanged);
		}
	}
}
