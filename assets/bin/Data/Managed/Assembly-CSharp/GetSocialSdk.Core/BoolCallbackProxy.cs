using System;
using UnityEngine;

namespace GetSocialSdk.Core
{
	internal class BoolCallbackProxy : JavaInterfaceProxy
	{
		private readonly Action<bool> _onSuccess;

		private readonly Action<GetSocialError> _onFailure;

		public BoolCallbackProxy(Action<bool> onSuccess, Action<GetSocialError> onFailure)
			: base("im.getsocial.sdk.Callback")
		{
			_onSuccess = onSuccess;
			_onFailure = onFailure;
		}

		private void onSuccess(bool value)
		{
			HandleValue(value, _onSuccess);
		}

		private void onFailure(AndroidJavaObject throwable)
		{
			HandleError(throwable, _onFailure);
		}
	}
}
