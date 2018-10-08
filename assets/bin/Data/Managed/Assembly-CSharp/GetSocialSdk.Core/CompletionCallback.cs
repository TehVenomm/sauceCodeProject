using System;
using UnityEngine;

namespace GetSocialSdk.Core
{
	internal class CompletionCallback : JavaInterfaceProxy
	{
		private readonly Action _onSuccess;

		private readonly Action<GetSocialError> _onFailure;

		public CompletionCallback(Action onSuccess, Action<GetSocialError> onFailure)
			: base("im.getsocial.sdk.CompletionCallback")
		{
			_onSuccess = onSuccess;
			_onFailure = onFailure;
		}

		private void onSuccess()
		{
			JavaInterfaceProxy.ExecuteOnMainThread(_onSuccess);
		}

		private void onFailure(AndroidJavaObject throwable)
		{
			HandleError(throwable, _onFailure);
		}
	}
}
