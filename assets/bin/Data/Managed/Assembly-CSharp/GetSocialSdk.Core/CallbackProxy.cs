using System;
using UnityEngine;

namespace GetSocialSdk.Core
{
	internal class CallbackProxy<T> : JavaInterfaceProxy where T : IConvertableFromNative<T>, new()
	{
		private readonly Action<T> _onSuccess;

		private readonly Action<GetSocialError> _onFailure;

		public CallbackProxy(Action<T> onSuccess, Action<GetSocialError> onFailure)
			: base("im.getsocial.sdk.Callback")
		{
			_onSuccess = onSuccess;
			_onFailure = onFailure;
		}

		private void onSuccess(AndroidJavaObject resultAJO)
		{
			T value = new T().ParseFromAJO(resultAJO);
			base.HandleValue<T>(value, _onSuccess);
		}

		private void onFailure(AndroidJavaObject throwable)
		{
			HandleError(throwable, _onFailure);
		}
	}
}
