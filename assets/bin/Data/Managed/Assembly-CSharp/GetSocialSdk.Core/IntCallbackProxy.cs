using System;
using UnityEngine;

namespace GetSocialSdk.Core
{
	internal class IntCallbackProxy : JavaInterfaceProxy
	{
		private readonly Action<int> _onSuccess;

		private readonly Action<GetSocialError> _onFailure;

		public IntCallbackProxy(Action<int> onSuccess, Action<GetSocialError> onFailure)
			: base("im.getsocial.sdk.Callback")
		{
			_onSuccess = onSuccess;
			_onFailure = onFailure;
		}

		private void onSuccess(int value)
		{
			HandleValue(value, _onSuccess);
		}

		private void onFailure(AndroidJavaObject throwable)
		{
			HandleError(throwable, _onFailure);
		}
	}
}
